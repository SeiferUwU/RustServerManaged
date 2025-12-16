using System;
using System.Collections.Generic;
using Rust.RenderPipeline.Runtime.RenderingContext;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.Rendering;

namespace Rust.RenderPipeline.Runtime.Passes.Lighting;

public class LightingPass
{
	public const int MAX_DIRECTIONAL_LIGHT_COUNT = 4;

	public const int MAX_ADDITIONAL_LIGHT_COUNT = 128;

	private static readonly ProfilingSampler profilingSampler = new ProfilingSampler("Lighting");

	private static readonly DirectionalLightData[] directionalLightData = new DirectionalLightData[4];

	private static readonly AdditionalLightData[] additionalLightData = new AdditionalLightData[128];

	private static readonly int dirLightCountId = Shader.PropertyToID("_DirectionalLightCount");

	private static readonly int directionalLightDataId = Shader.PropertyToID("_DirectionalLightData");

	private static readonly int additionalLightCountId = Shader.PropertyToID("_AdditionalLightCount");

	private static readonly int additionalLightDataId = Shader.PropertyToID("_AdditionalLightData");

	private static readonly int tilesId = Shader.PropertyToID("_ForwardPlusTiles");

	private static readonly int tileSettingsId = Shader.PropertyToID("_ForwardPlusTileSettings");

	private CullingResults cullingResults;

	private readonly Shadows shadows = new Shadows();

	private int directionalLightCount;

	private int additionalLightCount;

	private ComputeBufferHandle directionalLightDataBuffer;

	private ComputeBufferHandle additionalLightDataBuffer;

	private ComputeBufferHandle tilesBuffer;

	private Vector2 screenUvToTileCoordinates;

	private Vector2Int tileCount;

	private int maxLightsPerTile;

	private int tileDataSize;

	private int maxTileDataSize;

	private NativeArray<float4> lightBounds;

	private NativeArray<int> tileData;

	private JobHandle forwardPlusJobHandle;

	private readonly Dictionary<Light, Vector4> lightShadowData = new Dictionary<Light, Vector4>();

	private LightCookieManager lightCookieManager;

	private int mainLightIndex;

	private int TileCount => tileCount.x * tileCount.y;

	private void Setup(CullingResults cullingResults, Vector2Int attachmentSize, ForwardPlusSettings forwardPlusSettings, ShadowSettings shadowSettings, int renderingLayerMask)
	{
		this.cullingResults = cullingResults;
		shadows.Setup(cullingResults, shadowSettings);
		lightBounds = new NativeArray<float4>(128, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
		maxLightsPerTile = ((forwardPlusSettings.maxLightsPerTile <= 0) ? 31 : forwardPlusSettings.maxLightsPerTile);
		maxTileDataSize = (tileDataSize = maxLightsPerTile + 1);
		float num = ((forwardPlusSettings.tileSize <= ForwardPlusSettings.TileSize.Default) ? 64f : ((float)forwardPlusSettings.tileSize));
		screenUvToTileCoordinates.x = (float)attachmentSize.x / num;
		screenUvToTileCoordinates.y = (float)attachmentSize.y / num;
		tileCount.x = Mathf.CeilToInt(screenUvToTileCoordinates.x);
		tileCount.y = Mathf.CeilToInt(screenUvToTileCoordinates.y);
		SetupLights(renderingLayerMask);
	}

	private void SetupForwardPlus(int lightIndex, ref VisibleLight visibleLight)
	{
		Rect screenRect = visibleLight.screenRect;
		lightBounds[lightIndex] = math.float4(screenRect.xMin, screenRect.yMin, screenRect.xMax, screenRect.yMax);
	}

	public static int GetMainLightIndex(NativeArray<VisibleLight> visibleLights)
	{
		int length = visibleLights.Length;
		if (length == 0)
		{
			return -1;
		}
		Light sun = RenderSettings.sun;
		int result = -1;
		float num = 0f;
		for (int i = 0; i < length; i++)
		{
			ref VisibleLight reference = ref visibleLights.UnsafeElementAtMutable(i);
			Light light = reference.light;
			if (light == null)
			{
				break;
			}
			if (reference.lightType == LightType.Directional)
			{
				if (light == sun)
				{
					return i;
				}
				if (light.intensity > num)
				{
					num = light.intensity;
					result = i;
				}
			}
		}
		return result;
	}

	private void SetupLights(int renderingLayerMask)
	{
		NativeArray<VisibleLight> visibleLights = cullingResults.visibleLights;
		int num = Mathf.Min(maxLightsPerTile, visibleLights.Length);
		tileDataSize = num + 1;
		directionalLightCount = (additionalLightCount = 0);
		lightShadowData.Clear();
		mainLightIndex = GetMainLightIndex(visibleLights);
		for (int i = 0; i < visibleLights.Length; i++)
		{
			VisibleLight visibleLight = visibleLights[i];
			Light light = visibleLight.light;
			if ((light.renderingLayerMask & renderingLayerMask) == 0)
			{
				continue;
			}
			switch (visibleLight.lightType)
			{
			case LightType.Directional:
				if (directionalLightCount < 4)
				{
					lightShadowData[light] = shadows.ReserveDirectionalShadows(light, i);
					directionalLightData[directionalLightCount++] = new DirectionalLightData(ref visibleLight, light, lightShadowData[light]);
				}
				break;
			case LightType.Point:
				if (additionalLightCount < 128)
				{
					lightShadowData[light] = shadows.ReserveAdditionalShadows(light, i);
					SetupForwardPlus(additionalLightCount, ref visibleLight);
					additionalLightData[additionalLightCount++] = AdditionalLightData.CreatePointLight(ref visibleLight, light, lightShadowData[light]);
				}
				break;
			case LightType.Spot:
				if (additionalLightCount < 128)
				{
					lightShadowData[light] = shadows.ReserveAdditionalShadows(light, i);
					SetupForwardPlus(additionalLightCount, ref visibleLight);
					additionalLightData[additionalLightCount++] = AdditionalLightData.CreateSpotLight(ref visibleLight, light, lightShadowData[light]);
				}
				break;
			}
		}
		tileData = new NativeArray<int>(TileCount * tileDataSize, Allocator.TempJob);
		forwardPlusJobHandle = new ForwardPlusTilesJob
		{
			lightBounds = lightBounds,
			tileData = tileData,
			additionalLightCount = additionalLightCount,
			tileScreenUVSize = math.float2(1f / screenUvToTileCoordinates.x, 1f / screenUvToTileCoordinates.y),
			maxLightsPerTile = num,
			tilesPerRow = tileCount.x,
			tileDataSize = tileDataSize
		}.ScheduleParallel(TileCount, tileCount.x, default(JobHandle));
	}

	private void Render(RenderGraphContext context)
	{
		CommandBuffer cmd = context.cmd;
		cmd.SetGlobalInt(dirLightCountId, directionalLightCount);
		cmd.SetBufferData(directionalLightDataBuffer, directionalLightData, 0, 0, directionalLightCount);
		cmd.SetGlobalBuffer(directionalLightDataId, directionalLightDataBuffer);
		cmd.SetGlobalInt(additionalLightCountId, additionalLightCount);
		cmd.SetBufferData(additionalLightDataBuffer, additionalLightData, 0, 0, additionalLightCount);
		cmd.SetGlobalBuffer(additionalLightDataId, additionalLightDataBuffer);
		shadows.Render(context);
		forwardPlusJobHandle.Complete();
		cmd.SetBufferData(tilesBuffer, tileData, 0, 0, tileData.Length);
		cmd.SetGlobalBuffer(tilesId, tilesBuffer);
		cmd.SetGlobalVector(tileSettingsId, new Vector4(screenUvToTileCoordinates.x, screenUvToTileCoordinates.y, tileCount.x.ReinterpretAsFloat(), tileDataSize.ReinterpretAsFloat()));
		NativeArray<VisibleLight> visibleLights = cullingResults.visibleLights;
		lightCookieManager?.Setup(cmd, ref visibleLights, mainLightIndex, additionalLightCount);
		context.renderContext.ExecuteCommandBuffer(cmd);
		cmd.Clear();
		lightBounds.Dispose();
		tileData.Dispose();
	}

	public static void Record(RenderGraph renderGraph, ContextContainer frameData, LightCookieManager lightCookieManager)
	{
		RustResourceDataContext rustResourceDataContext = frameData.Get<RustResourceDataContext>();
		RustCameraContext rustCameraContext = frameData.Get<RustCameraContext>();
		RustRenderingContext rustRenderingContext = frameData.Get<RustRenderingContext>();
		CameraSettings cameraSettings = rustCameraContext.CameraSettings;
		ShadowSettings shadowSettings = rustRenderingContext.pipelineSettings.shadows;
		ForwardPlusSettings forwardPlus = rustRenderingContext.pipelineSettings.forwardPlus;
		int renderingLayerMask = (cameraSettings.maskLights ? cameraSettings.renderingLayerMask : (-1));
		LightingPass passData;
		RenderGraphBuilder builder = renderGraph.AddRenderPass<LightingPass>("Lighting", out passData, profilingSampler);
		try
		{
			passData.Setup(rustRenderingContext.cullResults, rustCameraContext.CameraBufferSize, forwardPlus, shadowSettings, renderingLayerMask);
			LightingPass lightingPass = passData;
			ComputeBufferDesc desc = new ComputeBufferDesc
			{
				name = "Directional Light Data",
				count = 4,
				stride = 48
			};
			lightingPass.directionalLightDataBuffer = builder.WriteComputeBuffer(renderGraph.CreateComputeBuffer(in desc));
			LightingPass lightingPass2 = passData;
			desc = new ComputeBufferDesc
			{
				name = "Additional Light Data",
				count = 128,
				stride = 80
			};
			lightingPass2.additionalLightDataBuffer = builder.WriteComputeBuffer(renderGraph.CreateComputeBuffer(in desc));
			LightingPass lightingPass3 = passData;
			desc = new ComputeBufferDesc
			{
				name = "Forward+ Tiles",
				count = passData.TileCount * passData.maxTileDataSize,
				stride = 4
			};
			lightingPass3.tilesBuffer = builder.WriteComputeBuffer(renderGraph.CreateComputeBuffer(in desc));
			builder.SetRenderFunc(delegate(LightingPass pass, RenderGraphContext context)
			{
				pass.Render(context);
			});
			passData.shadows.InitResources(renderGraph, builder, frameData);
			passData.lightCookieManager = lightCookieManager;
			builder.AllowPassCulling(value: false);
			rustResourceDataContext.DirectionalLightDataBuffer = passData.directionalLightDataBuffer;
			rustResourceDataContext.AdditionalLightDataBuffer = passData.additionalLightDataBuffer;
			rustResourceDataContext.LightTilesBuffer = passData.tilesBuffer;
			rustResourceDataContext.LightShadowData = passData.lightShadowData;
		}
		finally
		{
			((IDisposable)builder/*cast due to .constrained prefix*/).Dispose();
		}
	}
}
