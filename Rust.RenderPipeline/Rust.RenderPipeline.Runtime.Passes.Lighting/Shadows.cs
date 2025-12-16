using Rust.RenderPipeline.Runtime.RenderingContext;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.Rendering;

namespace Rust.RenderPipeline.Runtime.Passes.Lighting;

public class Shadows
{
	private struct ShadowedDirectionalLight
	{
		public int visibleLightIndex;

		public float slopeScaleBias;

		public float nearPlaneOffset;

		public float normalBias;
	}

	private struct ShadowedAdditionalLight
	{
		public int visibleLightIndex;

		public float slopeScaleBias;

		public float normalBias;

		public bool isPoint;
	}

	private const float ROOT_TWO = 1.4142137f;

	private const int MAX_SHADOWED_DIRECTIONAL_LIGHT_COUNT = 4;

	private const int MAX_SHADOWED_ADDITIONAL_LIGHT_COUNT = 16;

	private const int MAX_CASCADES = 4;

	private static readonly int directionalShadowAtlasId = Shader.PropertyToID("_DirectionalShadowAtlas");

	private static readonly int directionalShadowMatricesId = Shader.PropertyToID("_DirectionalShadowMatrices");

	private static readonly int directionalShadowCascadesId = Shader.PropertyToID("_DirectionalShadowCascades");

	private static readonly int cascadeCountId = Shader.PropertyToID("_CascadeCount");

	private static readonly int additionalShadowAtlasId = Shader.PropertyToID("_AdditionalShadowAtlas");

	private static readonly int additionalShadowDataId = Shader.PropertyToID("_AdditionalShadowData");

	private static readonly int shadowAtlasSizeId = Shader.PropertyToID("_ShadowAtlasSize");

	private static readonly int shadowDistanceFadeId = Shader.PropertyToID("_ShadowDistanceFade");

	private static readonly int shadowPancakingId = Shader.PropertyToID("_ShadowPancaking");

	private static readonly int unityLightShadowBiasId = Shader.PropertyToID("unity_LightShadowBias");

	private static readonly DirectionalShadowCascade[] directionalShadowCascades = new DirectionalShadowCascade[4];

	private static readonly Matrix4x4[] directionalShadowMatrices = new Matrix4x4[16];

	private static readonly AdditionalShadowData[] additionalShadowData = new AdditionalShadowData[16];

	private static readonly GlobalKeyword[] filterQualityKeywords = new GlobalKeyword[2]
	{
		GlobalKeyword.Create("_SHADOW_FILTER_MEDIUM"),
		GlobalKeyword.Create("_SHADOW_FILTER_HIGH")
	};

	private static readonly GlobalKeyword softCascadeBlendKeyword = GlobalKeyword.Create("_SOFT_CASCADE_BLEND");

	private readonly ShadowedDirectionalLight[] shadowedDirectionalLights = new ShadowedDirectionalLight[4];

	private readonly ShadowedAdditionalLight[] shadowedAdditionalLights = new ShadowedAdditionalLight[16];

	private CommandBuffer commandBuffer;

	private TextureHandle directionalAtlas;

	private TextureHandle additionalAtlas;

	private ComputeBufferHandle directionalShadowCascadesBuffer;

	private ComputeBufferHandle directionalShadowMatricesBuffer;

	private ComputeBufferHandle additionalShadowDataBuffer;

	private ScriptableRenderContext context;

	private CullingResults cullingResults;

	private ShadowSettings settings;

	private int shadowedDirectionalLightCount;

	private int shadowedAdditionalLightCount;

	private Vector4 atlasSizes;

	public void Setup(CullingResults cullingResults, ShadowSettings settings)
	{
		this.cullingResults = cullingResults;
		this.settings = settings;
		shadowedDirectionalLightCount = (shadowedAdditionalLightCount = 0);
	}

	public void Render(RenderGraphContext context)
	{
		commandBuffer = context.cmd;
		this.context = context.renderContext;
		if (shadowedDirectionalLightCount > 0)
		{
			RenderDirectionalShadows();
		}
		if (shadowedAdditionalLightCount > 0)
		{
			RenderAdditionalShadows();
		}
		SetKeywords(filterQualityKeywords, (int)(settings.filterQuality - 1));
		commandBuffer.SetGlobalBuffer(directionalShadowCascadesId, directionalShadowCascadesBuffer);
		commandBuffer.SetGlobalBuffer(directionalShadowMatricesId, directionalShadowMatricesBuffer);
		commandBuffer.SetGlobalBuffer(additionalShadowDataId, additionalShadowDataBuffer);
		commandBuffer.SetGlobalTexture(directionalShadowAtlasId, directionalAtlas);
		commandBuffer.SetGlobalTexture(additionalShadowAtlasId, additionalAtlas);
		commandBuffer.SetGlobalInt(cascadeCountId, (shadowedDirectionalLightCount > 0) ? settings.directional.cascadeCount : 0);
		float num = 1f - settings.directional.cascadeFade;
		commandBuffer.SetGlobalVector(shadowDistanceFadeId, new Vector4(1f / settings.maxDistance, 1f / settings.distanceFade, 1f / (1f - num * num)));
		commandBuffer.SetGlobalVector(shadowAtlasSizeId, atlasSizes);
		ExecuteCommandBuffer();
	}

	public Vector3 ReserveDirectionalShadows(Light light, int visibleLightIndex)
	{
		if (shadowedDirectionalLightCount < 4 && light.shadows != LightShadows.None && light.shadowStrength > 0f && cullingResults.GetShadowCasterBounds(visibleLightIndex, out var _))
		{
			shadowedDirectionalLights[shadowedDirectionalLightCount] = new ShadowedDirectionalLight
			{
				visibleLightIndex = visibleLightIndex,
				slopeScaleBias = light.shadowBias,
				nearPlaneOffset = light.shadowNearPlane,
				normalBias = light.shadowNormalBias
			};
			return new Vector3(light.shadowStrength, settings.directional.cascadeCount * shadowedDirectionalLightCount++, 1f + light.shadowNormalBias);
		}
		return Vector3.zero;
	}

	public Vector4 ReserveAdditionalShadows(Light light, int visibleLightIndex)
	{
		if (light.shadows == LightShadows.None || light.shadowStrength <= 0f)
		{
			return new Vector4(0f, 0f, 0f, -1f);
		}
		bool flag = light.type == LightType.Point;
		int num = shadowedAdditionalLightCount + ((!flag) ? 1 : 6);
		if (num > 16 || !cullingResults.GetShadowCasterBounds(visibleLightIndex, out var _))
		{
			return new Vector4(0f - light.shadowStrength, 0f, 0f, -1f);
		}
		shadowedAdditionalLights[shadowedAdditionalLightCount] = new ShadowedAdditionalLight
		{
			visibleLightIndex = visibleLightIndex,
			slopeScaleBias = light.shadowBias,
			normalBias = 1f + light.shadowNormalBias,
			isPoint = flag
		};
		Vector4 result = new Vector4(light.shadowStrength, shadowedAdditionalLightCount, flag ? 1f : 0f, -1f);
		shadowedAdditionalLightCount = num;
		return result;
	}

	private void RenderDirectionalShadows()
	{
		int atlasSize = (int)settings.directional.atlasSize;
		atlasSizes.x = atlasSize;
		atlasSizes.y = 1f / (float)atlasSize;
		commandBuffer.SetRenderTarget(directionalAtlas, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store);
		commandBuffer.ClearRenderTarget(clearDepth: true, clearColor: false, Color.clear);
		commandBuffer.SetGlobalFloat(shadowPancakingId, 1f);
		commandBuffer.BeginSample("Directional Shadows");
		ExecuteCommandBuffer();
		int num = shadowedDirectionalLightCount * settings.directional.cascadeCount;
		int num2 = ((num <= 1) ? 1 : ((num <= 4) ? 2 : 4));
		int tileSize = atlasSize / num2;
		for (int i = 0; i < shadowedDirectionalLightCount; i++)
		{
			RenderDirectionalShadows(i, num2, tileSize);
		}
		commandBuffer.SetBufferData(directionalShadowCascadesBuffer, directionalShadowCascades, 0, 0, settings.directional.cascadeCount);
		commandBuffer.SetBufferData(directionalShadowMatricesBuffer, directionalShadowMatrices, 0, 0, shadowedDirectionalLightCount * settings.directional.cascadeCount);
		commandBuffer.SetKeyword(in softCascadeBlendKeyword, settings.directional.softCascadeBlend);
		commandBuffer.EndSample("Directional Shadows");
		ExecuteCommandBuffer();
	}

	private void RenderDirectionalShadows(int index, int split, int tileSize)
	{
		ShadowedDirectionalLight shadowedDirectionalLight = shadowedDirectionalLights[index];
		ShadowDrawingSettings shadowDrawingSettings = new ShadowDrawingSettings(cullingResults, shadowedDirectionalLight.visibleLightIndex, BatchCullingProjectionType.Orthographic);
		shadowDrawingSettings.useRenderingLayerMaskTest = true;
		ShadowDrawingSettings shadowDrawingSettings2 = shadowDrawingSettings;
		int cascadeCount = settings.directional.cascadeCount;
		int num = index * cascadeCount;
		Vector3 cascadeRatios = settings.directional.CascadeRatios;
		float shadowCascadeBlendCullingFactor = Mathf.Max(0f, 0.8f - settings.directional.cascadeFade);
		float scale = 1f / (float)split;
		for (int i = 0; i < cascadeCount; i++)
		{
			cullingResults.ComputeDirectionalShadowMatricesAndCullingPrimitives(shadowedDirectionalLight.visibleLightIndex, i, cascadeCount, cascadeRatios, tileSize, shadowedDirectionalLight.nearPlaneOffset, out var viewMatrix, out var projMatrix, out var shadowSplitData);
			shadowSplitData.shadowCascadeBlendCullingFactor = shadowCascadeBlendCullingFactor;
			shadowDrawingSettings2.splitData = shadowSplitData;
			if (index == 0)
			{
				directionalShadowCascades[i] = new DirectionalShadowCascade(shadowSplitData.cullingSphere, tileSize, settings.DirectionalFilterSize);
			}
			int num2 = num + i;
			directionalShadowMatrices[num2] = ConvertToAtlasMatrix(projMatrix * viewMatrix, SetTileViewport(num2, split, tileSize), scale);
			commandBuffer.SetViewProjectionMatrices(viewMatrix, projMatrix);
			commandBuffer.SetGlobalDepthBias(1f, 1f);
			float z = projMatrix.MultiplyVector(new Vector3(0f, 0f, shadowedDirectionalLight.slopeScaleBias * directionalShadowCascades[i].data.y)).z;
			commandBuffer.SetGlobalVector(unityLightShadowBiasId, new Vector4(z, 1f, shadowedDirectionalLight.normalBias * directionalShadowCascades[i].data.y, 0f));
			ExecuteCommandBuffer();
			context.DrawShadows(ref shadowDrawingSettings2);
			commandBuffer.SetGlobalDepthBias(0f, 0f);
		}
	}

	private void RenderAdditionalShadows()
	{
		int atlasSize = (int)settings.additional.atlasSize;
		atlasSizes.z = atlasSize;
		atlasSizes.w = 1f / (float)atlasSize;
		commandBuffer.SetRenderTarget(additionalAtlas, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store);
		commandBuffer.ClearRenderTarget(clearDepth: true, clearColor: false, Color.clear);
		commandBuffer.SetGlobalFloat(shadowPancakingId, 0f);
		commandBuffer.BeginSample("Additional Shadows");
		ExecuteCommandBuffer();
		int num = shadowedAdditionalLightCount;
		int num2 = ((num <= 1) ? 1 : ((num <= 4) ? 2 : 4));
		int tileSize = atlasSize / num2;
		int num3 = 0;
		while (num3 < shadowedAdditionalLightCount)
		{
			if (shadowedAdditionalLights[num3].isPoint)
			{
				RenderPointShadows(num3, num2, tileSize);
				num3 += 6;
			}
			else
			{
				RenderSpotShadows(num3, num2, tileSize);
				num3++;
			}
		}
		commandBuffer.SetBufferData(additionalShadowDataBuffer, additionalShadowData, 0, 0, shadowedAdditionalLightCount);
		commandBuffer.EndSample("Additional Shadows");
		ExecuteCommandBuffer();
	}

	private void RenderSpotShadows(int index, int split, int tileSize)
	{
		ShadowedAdditionalLight shadowedAdditionalLight = shadowedAdditionalLights[index];
		ShadowDrawingSettings shadowDrawingSettings = new ShadowDrawingSettings(cullingResults, shadowedAdditionalLight.visibleLightIndex, BatchCullingProjectionType.Perspective);
		shadowDrawingSettings.useRenderingLayerMaskTest = true;
		ShadowDrawingSettings shadowDrawingSettings2 = shadowDrawingSettings;
		cullingResults.ComputeSpotShadowMatricesAndCullingPrimitives(shadowedAdditionalLight.visibleLightIndex, out var viewMatrix, out var projMatrix, out var shadowSplitData);
		shadowDrawingSettings2.splitData = shadowSplitData;
		float num = 2f / ((float)tileSize * projMatrix.m00) * settings.AdditionalFilterSize;
		float bias = shadowedAdditionalLight.normalBias * num * 1.4142137f;
		Vector2 offset = SetTileViewport(index, split, tileSize);
		float scale = 1f / (float)split;
		additionalShadowData[index] = new AdditionalShadowData(offset, scale, bias, atlasSizes.w * 0.5f, ConvertToAtlasMatrix(projMatrix * viewMatrix, offset, scale));
		commandBuffer.SetViewProjectionMatrices(viewMatrix, projMatrix);
		commandBuffer.SetGlobalDepthBias(1f, 1f);
		float z = projMatrix.MultiplyVector(new Vector3(0f, 0f, shadowedAdditionalLight.slopeScaleBias * num * 1.4142137f)).z;
		commandBuffer.SetGlobalVector(unityLightShadowBiasId, new Vector4(z, 0f, 0f, 0f));
		ExecuteCommandBuffer();
		context.DrawShadows(ref shadowDrawingSettings2);
		commandBuffer.SetGlobalDepthBias(0f, 0f);
	}

	private void RenderPointShadows(int index, int split, int tileSize)
	{
		ShadowedAdditionalLight shadowedAdditionalLight = shadowedAdditionalLights[index];
		ShadowDrawingSettings shadowDrawingSettings = new ShadowDrawingSettings(cullingResults, shadowedAdditionalLight.visibleLightIndex, BatchCullingProjectionType.Perspective);
		shadowDrawingSettings.useRenderingLayerMaskTest = true;
		ShadowDrawingSettings shadowDrawingSettings2 = shadowDrawingSettings;
		float num = 2f / (float)tileSize * ((float)settings.additional.filter + 1f);
		float num2 = shadowedAdditionalLight.normalBias * num * 1.4142137f;
		float scale = 1f / (float)split;
		for (int i = 0; i < 6; i++)
		{
			float fovBias = Mathf.Atan(1f + num2 + num) * 57.29578f * 2f - 90f;
			cullingResults.ComputePointShadowMatricesAndCullingPrimitives(shadowedAdditionalLight.visibleLightIndex, (CubemapFace)i, fovBias, out var viewMatrix, out var projMatrix, out var shadowSplitData);
			viewMatrix.m11 = 0f - viewMatrix.m11;
			viewMatrix.m12 = 0f - viewMatrix.m12;
			viewMatrix.m13 = 0f - viewMatrix.m13;
			shadowDrawingSettings2.splitData = shadowSplitData;
			int num3 = index + i;
			Vector2 offset = SetTileViewport(num3, split, tileSize);
			additionalShadowData[num3] = new AdditionalShadowData(offset, scale, num2, atlasSizes.w * 0.5f, ConvertToAtlasMatrix(projMatrix * viewMatrix, offset, scale));
			commandBuffer.SetViewProjectionMatrices(viewMatrix, projMatrix);
			commandBuffer.SetGlobalDepthBias(1f, 1f);
			float z = projMatrix.MultiplyVector(new Vector3(0f, 0f, shadowedAdditionalLight.slopeScaleBias * num * 1.4142137f)).z;
			commandBuffer.SetGlobalVector(unityLightShadowBiasId, new Vector4(z, 0f, 0f, 0f));
			ExecuteCommandBuffer();
			context.DrawShadows(ref shadowDrawingSettings2);
			commandBuffer.SetGlobalDepthBias(0f, 0f);
		}
	}

	private void SetKeywords(GlobalKeyword[] keywords, int enabledIndex)
	{
		for (int i = 0; i < keywords.Length; i++)
		{
			commandBuffer.SetKeyword(in keywords[i], i == enabledIndex);
		}
	}

	private Vector2 SetTileViewport(int index, int split, float tileSize)
	{
		Vector2 result = new Vector2(index % split, index / split);
		commandBuffer.SetViewport(new Rect(result.x * tileSize, result.y * tileSize, tileSize, tileSize));
		return result;
	}

	private Matrix4x4 ConvertToAtlasMatrix(Matrix4x4 m, Vector2 offset, float scale)
	{
		if (SystemInfo.usesReversedZBuffer)
		{
			m.m20 = 0f - m.m20;
			m.m21 = 0f - m.m21;
			m.m22 = 0f - m.m22;
			m.m23 = 0f - m.m23;
		}
		m.m00 = (0.5f * (m.m00 + m.m30) + offset.x * m.m30) * scale;
		m.m01 = (0.5f * (m.m01 + m.m31) + offset.x * m.m31) * scale;
		m.m02 = (0.5f * (m.m02 + m.m32) + offset.x * m.m32) * scale;
		m.m03 = (0.5f * (m.m03 + m.m33) + offset.x * m.m33) * scale;
		m.m10 = (0.5f * (m.m10 + m.m30) + offset.y * m.m30) * scale;
		m.m11 = (0.5f * (m.m11 + m.m31) + offset.y * m.m31) * scale;
		m.m12 = (0.5f * (m.m12 + m.m32) + offset.y * m.m32) * scale;
		m.m13 = (0.5f * (m.m13 + m.m33) + offset.y * m.m33) * scale;
		m.m20 = 0.5f * (m.m20 + m.m30);
		m.m21 = 0.5f * (m.m21 + m.m31);
		m.m22 = 0.5f * (m.m22 + m.m32);
		m.m23 = 0.5f * (m.m23 + m.m33);
		return m;
	}

	public void InitResources(RenderGraph renderGraph, RenderGraphBuilder builder, ContextContainer frameData)
	{
		int atlasSize = (int)settings.directional.atlasSize;
		TextureDesc textureDesc = new TextureDesc(atlasSize, atlasSize);
		textureDesc.depthBufferBits = DepthBits.Depth32;
		textureDesc.isShadowMap = true;
		textureDesc.name = "Directional Shadow Atlas";
		TextureDesc desc = textureDesc;
		directionalAtlas = ((shadowedDirectionalLightCount > 0) ? builder.WriteTexture(renderGraph.CreateTexture(in desc)) : renderGraph.defaultResources.defaultShadowTexture);
		directionalShadowCascadesBuffer = builder.WriteComputeBuffer(renderGraph.CreateComputeBuffer(new ComputeBufferDesc
		{
			name = "Shadow Cascades",
			stride = 32,
			count = 4
		}));
		directionalShadowMatricesBuffer = builder.WriteComputeBuffer(renderGraph.CreateComputeBuffer(new ComputeBufferDesc
		{
			name = "Directional Shadow Matrices",
			stride = 64,
			count = 16
		}));
		atlasSize = (int)settings.additional.atlasSize;
		desc.width = (desc.height = atlasSize);
		desc.name = "Additional Shadow Atlas";
		additionalAtlas = ((shadowedAdditionalLightCount > 0) ? builder.WriteTexture(renderGraph.CreateTexture(in desc)) : renderGraph.defaultResources.defaultShadowTexture);
		additionalShadowDataBuffer = builder.WriteComputeBuffer(renderGraph.CreateComputeBuffer(new ComputeBufferDesc
		{
			name = "Additional Shadow Data",
			stride = 80,
			count = 16
		}));
		RustResourceDataContext rustResourceDataContext = frameData.Get<RustResourceDataContext>();
		rustResourceDataContext.MainShadowsTexture = directionalAtlas;
		rustResourceDataContext.AdditionalShadowsTexture = additionalAtlas;
		rustResourceDataContext.ShadowCascadesBuffer = directionalShadowCascadesBuffer;
		rustResourceDataContext.ShadowMatricesBuffer = directionalShadowMatricesBuffer;
		rustResourceDataContext.AdditionalShadowDataBuffer = additionalShadowDataBuffer;
	}

	private void ExecuteCommandBuffer()
	{
		context.ExecuteCommandBuffer(commandBuffer);
		commandBuffer.Clear();
	}
}
