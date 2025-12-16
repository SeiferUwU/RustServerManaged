using System;
using System.Collections.Generic;
using Rust.RenderPipeline.Runtime.RenderingContext;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.Rendering;

namespace Rust.RenderPipeline.Runtime.Passes.Lighting;

public class DeferredLightingPass
{
	private enum ShaderPasses
	{
		StencilVolume,
		Lighting,
		DirectionalLighting
	}

	private const float STENCIL_SHAPE_GUARD = 1.06067f;

	private static readonly ProfilingSampler profilingSampler = new ProfilingSampler("Deferred Lighting");

	private static readonly int gBuffer0Id = Shader.PropertyToID("_CameraGBufferTexture0");

	private static readonly int gBuffer1Id = Shader.PropertyToID("_CameraGBufferTexture1");

	private static readonly int gBuffer2Id = Shader.PropertyToID("_CameraGBufferTexture2");

	private static readonly int inverseViewProjId = Shader.PropertyToID("_InverseViewProj");

	private static readonly int spotLightScaleId = Shader.PropertyToID("_SpotLightScale");

	private static readonly int spotLightBiasId = Shader.PropertyToID("_SpotLightBias");

	private static readonly int spotLightGuardId = Shader.PropertyToID("_SpotLightGuard");

	private static readonly int lightColorId = Shader.PropertyToID("_LightColor");

	private static readonly int lightPositionAndRangeId = Shader.PropertyToID("_LightPositionAndRange");

	private static readonly int lightDirectionAndMaskId = Shader.PropertyToID("_LightDirectionAndMask");

	private static readonly int lightSpotAngleId = Shader.PropertyToID("_LightSpotAngles");

	private static readonly int lightShadowDataId = Shader.PropertyToID("_LightShadowData");

	private static readonly int deferredLightIndex = Shader.PropertyToID("_DeferredLightIndex");

	private readonly TextureHandle[] gBufferTextureHandles = new TextureHandle[RustResourceDataContext.gBufferTextureCount];

	private CullingResults cullingResults;

	private Mesh icoSphere;

	private Mesh icoHemisphere;

	private Material deferredLightingMaterial;

	private Camera camera;

	private CameraSettings cameraSettings;

	private Dictionary<Light, Vector4> lightShadowData;

	private int mainLightIndex;

	private static Mesh CreateSphereMesh()
	{
		Vector3[] vertices = new Vector3[42]
		{
			new Vector3(0f, 0f, -1.07f),
			new Vector3(0.174f, -0.535f, -0.91f),
			new Vector3(-0.455f, -0.331f, -0.91f),
			new Vector3(0.562f, 0f, -0.91f),
			new Vector3(-0.455f, 0.331f, -0.91f),
			new Vector3(0.174f, 0.535f, -0.91f),
			new Vector3(-0.281f, -0.865f, -0.562f),
			new Vector3(0.736f, -0.535f, -0.562f),
			new Vector3(0.296f, -0.91f, -0.468f),
			new Vector3(-0.91f, 0f, -0.562f),
			new Vector3(-0.774f, -0.562f, -0.478f),
			new Vector3(0f, -1.07f, 0f),
			new Vector3(-0.629f, -0.865f, 0f),
			new Vector3(0.629f, -0.865f, 0f),
			new Vector3(-1.017f, -0.331f, 0f),
			new Vector3(0.957f, 0f, -0.478f),
			new Vector3(0.736f, 0.535f, -0.562f),
			new Vector3(1.017f, -0.331f, 0f),
			new Vector3(1.017f, 0.331f, 0f),
			new Vector3(-0.296f, -0.91f, 0.478f),
			new Vector3(0.281f, -0.865f, 0.562f),
			new Vector3(0.774f, -0.562f, 0.478f),
			new Vector3(-0.736f, -0.535f, 0.562f),
			new Vector3(0.91f, 0f, 0.562f),
			new Vector3(0.455f, -0.331f, 0.91f),
			new Vector3(-0.174f, -0.535f, 0.91f),
			new Vector3(0.629f, 0.865f, 0f),
			new Vector3(0.774f, 0.562f, 0.478f),
			new Vector3(0.455f, 0.331f, 0.91f),
			new Vector3(0f, 0f, 1.07f),
			new Vector3(-0.562f, 0f, 0.91f),
			new Vector3(-0.957f, 0f, 0.478f),
			new Vector3(0.281f, 0.865f, 0.562f),
			new Vector3(-0.174f, 0.535f, 0.91f),
			new Vector3(0.296f, 0.91f, -0.478f),
			new Vector3(-1.017f, 0.331f, 0f),
			new Vector3(-0.736f, 0.535f, 0.562f),
			new Vector3(-0.296f, 0.91f, 0.478f),
			new Vector3(0f, 1.07f, 0f),
			new Vector3(-0.281f, 0.865f, -0.562f),
			new Vector3(-0.774f, 0.562f, -0.478f),
			new Vector3(-0.629f, 0.865f, 0f)
		};
		int[] triangles = new int[240]
		{
			0, 1, 2, 0, 3, 1, 2, 4, 0, 0,
			5, 3, 0, 4, 5, 1, 6, 2, 3, 7,
			1, 1, 8, 6, 1, 7, 8, 9, 4, 2,
			2, 6, 10, 10, 9, 2, 8, 11, 6, 6,
			12, 10, 11, 12, 6, 7, 13, 8, 8, 13,
			11, 10, 14, 9, 10, 12, 14, 3, 15, 7,
			5, 16, 3, 3, 16, 15, 15, 17, 7, 17,
			13, 7, 16, 18, 15, 15, 18, 17, 11, 19,
			12, 13, 20, 11, 11, 20, 19, 17, 21, 13,
			13, 21, 20, 12, 19, 22, 12, 22, 14, 17,
			23, 21, 18, 23, 17, 21, 24, 20, 23, 24,
			21, 20, 25, 19, 19, 25, 22, 24, 25, 20,
			26, 18, 16, 18, 27, 23, 26, 27, 18, 28,
			24, 23, 27, 28, 23, 24, 29, 25, 28, 29,
			24, 25, 30, 22, 25, 29, 30, 14, 22, 31,
			22, 30, 31, 32, 28, 27, 26, 32, 27, 33,
			29, 28, 30, 29, 33, 33, 28, 32, 34, 26,
			16, 5, 34, 16, 14, 31, 35, 14, 35, 9,
			31, 30, 36, 30, 33, 36, 35, 31, 36, 37,
			33, 32, 36, 33, 37, 38, 32, 26, 34, 38,
			26, 38, 37, 32, 5, 39, 34, 39, 38, 34,
			4, 39, 5, 9, 40, 4, 9, 35, 40, 4,
			40, 39, 35, 36, 41, 41, 36, 37, 41, 37,
			38, 40, 35, 41, 40, 41, 39, 41, 38, 39
		};
		return new Mesh
		{
			indexFormat = IndexFormat.UInt16,
			vertices = vertices,
			triangles = triangles,
			hideFlags = HideFlags.HideAndDontSave
		};
	}

	private static Mesh CreateHemisphereMesh()
	{
		Vector3[] vertices = new Vector3[42]
		{
			new Vector3(0f, 0f, 0f),
			new Vector3(1f, 0f, 0f),
			new Vector3(0.92388f, 0.382683f, 0f),
			new Vector3(0.707107f, 0.707107f, 0f),
			new Vector3(0.382683f, 0.92388f, 0f),
			new Vector3(-0f, 1f, 0f),
			new Vector3(-0.382684f, 0.92388f, 0f),
			new Vector3(-0.707107f, 0.707107f, 0f),
			new Vector3(-0.92388f, 0.382683f, 0f),
			new Vector3(-1f, -0f, 0f),
			new Vector3(-0.92388f, -0.382683f, 0f),
			new Vector3(-0.707107f, -0.707107f, 0f),
			new Vector3(-0.382683f, -0.92388f, 0f),
			new Vector3(0f, -1f, 0f),
			new Vector3(0.382684f, -0.923879f, 0f),
			new Vector3(0.707107f, -0.707107f, 0f),
			new Vector3(0.92388f, -0.382683f, 0f),
			new Vector3(0f, 0f, 1f),
			new Vector3(0.707107f, 0f, 0.707107f),
			new Vector3(0f, -0.707107f, 0.707107f),
			new Vector3(0f, 0.707107f, 0.707107f),
			new Vector3(-0.707107f, 0f, 0.707107f),
			new Vector3(0.816497f, -0.408248f, 0.408248f),
			new Vector3(0.408248f, -0.408248f, 0.816497f),
			new Vector3(0.408248f, -0.816497f, 0.408248f),
			new Vector3(0.408248f, 0.816497f, 0.408248f),
			new Vector3(0.408248f, 0.408248f, 0.816497f),
			new Vector3(0.816497f, 0.408248f, 0.408248f),
			new Vector3(-0.816497f, 0.408248f, 0.408248f),
			new Vector3(-0.408248f, 0.408248f, 0.816497f),
			new Vector3(-0.408248f, 0.816497f, 0.408248f),
			new Vector3(-0.408248f, -0.816497f, 0.408248f),
			new Vector3(-0.408248f, -0.408248f, 0.816497f),
			new Vector3(-0.816497f, -0.408248f, 0.408248f),
			new Vector3(0f, -0.92388f, 0.382683f),
			new Vector3(0.92388f, 0f, 0.382683f),
			new Vector3(0f, -0.382683f, 0.92388f),
			new Vector3(0.382683f, 0f, 0.92388f),
			new Vector3(0f, 0.92388f, 0.382683f),
			new Vector3(0f, 0.382683f, 0.92388f),
			new Vector3(-0.92388f, 0f, 0.382683f),
			new Vector3(-0.382683f, 0f, 0.92388f)
		};
		int[] triangles = new int[240]
		{
			0, 2, 1, 0, 3, 2, 0, 4, 3, 0,
			5, 4, 0, 6, 5, 0, 7, 6, 0, 8,
			7, 0, 9, 8, 0, 10, 9, 0, 11, 10,
			0, 12, 11, 0, 13, 12, 0, 14, 13, 0,
			15, 14, 0, 16, 15, 0, 1, 16, 22, 23,
			24, 25, 26, 27, 28, 29, 30, 31, 32, 33,
			14, 24, 34, 35, 22, 16, 36, 23, 37, 2,
			27, 35, 38, 25, 4, 37, 26, 39, 6, 30,
			38, 40, 28, 8, 39, 29, 41, 10, 33, 40,
			34, 31, 12, 41, 32, 36, 15, 22, 24, 18,
			23, 22, 19, 24, 23, 3, 25, 27, 20, 26,
			25, 18, 27, 26, 7, 28, 30, 21, 29, 28,
			20, 30, 29, 11, 31, 33, 19, 32, 31, 21,
			33, 32, 13, 14, 34, 15, 24, 14, 19, 34,
			24, 1, 35, 16, 18, 22, 35, 15, 16, 22,
			17, 36, 37, 19, 23, 36, 18, 37, 23, 1,
			2, 35, 3, 27, 2, 18, 35, 27, 5, 38,
			4, 20, 25, 38, 3, 4, 25, 17, 37, 39,
			18, 26, 37, 20, 39, 26, 5, 6, 38, 7,
			30, 6, 20, 38, 30, 9, 40, 8, 21, 28,
			40, 7, 8, 28, 17, 39, 41, 20, 29, 39,
			21, 41, 29, 9, 10, 40, 11, 33, 10, 21,
			40, 33, 13, 34, 12, 19, 31, 34, 11, 12,
			31, 17, 41, 36, 21, 32, 41, 19, 36, 32
		};
		return new Mesh
		{
			indexFormat = IndexFormat.UInt16,
			vertices = vertices,
			triangles = triangles,
			hideFlags = HideFlags.HideAndDontSave
		};
	}

	private void Setup(Material deferredLightingMaterial, CullingResults cullingResults, ShadowSettings shadowSettings, int renderingLayerMask, Camera camera, CameraSettings cameraSettings)
	{
		this.deferredLightingMaterial = deferredLightingMaterial;
		this.cullingResults = cullingResults;
		this.camera = camera;
		this.cameraSettings = cameraSettings;
		if (icoSphere == null)
		{
			icoSphere = CreateSphereMesh();
		}
		if (icoHemisphere == null)
		{
			icoHemisphere = CreateHemisphereMesh();
		}
	}

	private void Render(RenderGraphContext context)
	{
		NativeArray<VisibleLight> visibleLights = cullingResults.visibleLights;
		CommandBuffer cmd = context.cmd;
		mainLightIndex = LightingPass.GetMainLightIndex(visibleLights);
		cmd.SetGlobalMatrix(value: (GL.GetGPUProjectionMatrix(camera.projectionMatrix, renderIntoTexture: true) * camera.worldToCameraMatrix).inverse, nameID: inverseViewProjId);
		cmd.SetGlobalTexture(gBuffer0Id, gBufferTextureHandles[0]);
		cmd.SetGlobalTexture(gBuffer1Id, gBufferTextureHandles[1]);
		cmd.SetGlobalTexture(gBuffer2Id, gBufferTextureHandles[2]);
		RenderPointLight(context, visibleLights);
		RenderSpotLight(context, visibleLights);
		RenderDirectionalLight(context, visibleLights);
		context.renderContext.ExecuteCommandBuffer(context.cmd);
		context.cmd.Clear();
	}

	private void RenderPointLight(RenderGraphContext context, NativeArray<VisibleLight> visibleLights)
	{
		CommandBuffer cmd = context.cmd;
		for (int i = 0; i < visibleLights.Length; i++)
		{
			VisibleLight visibleLight = visibleLights[i];
			if (visibleLight.lightType == LightType.Point && (visibleLight.light.renderingLayerMask & cameraSettings.renderingLayerMask) != 0)
			{
				Vector3 vector = visibleLight.localToWorldMatrix.GetColumn(3);
				Matrix4x4 matrix = new Matrix4x4(new Vector4(visibleLight.range, 0f, 0f, 0f), new Vector4(0f, visibleLight.range, 0f, 0f), new Vector4(0f, 0f, visibleLight.range, 0f), new Vector4(vector.x, vector.y, vector.z, 1f));
				if (!lightShadowData.TryGetValue(visibleLight.light, out var value))
				{
					value = Vector4.zero;
				}
				AdditionalLightData additionalLightData = AdditionalLightData.CreatePointLight(ref visibleLight, visibleLight.light, value);
				cmd.BeginSample("Point Light");
				cmd.SetGlobalVector(lightColorId, additionalLightData.color);
				cmd.SetGlobalVector(lightPositionAndRangeId, additionalLightData.position);
				cmd.SetGlobalVector(lightDirectionAndMaskId, additionalLightData.directionAndMask);
				cmd.SetGlobalVector(lightSpotAngleId, additionalLightData.spotAngle);
				cmd.SetGlobalVector(lightShadowDataId, additionalLightData.shadowData);
				cmd.SetGlobalInt(deferredLightIndex, (mainLightIndex == -1) ? i : (i - 1));
				cmd.DrawMesh(icoSphere, matrix, deferredLightingMaterial, 0, 0);
				cmd.DrawMesh(icoSphere, matrix, deferredLightingMaterial, 0, 1);
				cmd.EndSample("Point Light");
			}
		}
	}

	private void RenderSpotLight(RenderGraphContext context, NativeArray<VisibleLight> visibleLights)
	{
		CommandBuffer cmd = context.cmd;
		cmd.EnableShaderKeyword("_SPOT");
		for (int i = 0; i < visibleLights.Length; i++)
		{
			VisibleLight visibleLight = visibleLights[i];
			if (visibleLight.lightType == LightType.Spot && (visibleLight.light.renderingLayerMask & cameraSettings.renderingLayerMask) != 0)
			{
				float f = MathF.PI / 180f * visibleLight.spotAngle * 0.5f;
				float num = Mathf.Cos(f);
				float num2 = Mathf.Sin(f);
				float num3 = Mathf.Lerp(1f, 1.06067f, num2);
				if (!lightShadowData.TryGetValue(visibleLight.light, out var value))
				{
					value = Vector4.zero;
				}
				AdditionalLightData additionalLightData = AdditionalLightData.CreateSpotLight(ref visibleLight, visibleLight.light, value);
				_ = GL.GetGPUProjectionMatrix(Matrix4x4.Perspective(visibleLight.spotAngle, 1f, 0.001f, visibleLight.range), renderIntoTexture: true) * visibleLight.localToWorldMatrix.inverse;
				cmd.BeginSample("Spot Light");
				cmd.SetGlobalColor(lightColorId, additionalLightData.color);
				cmd.SetGlobalVector(lightPositionAndRangeId, additionalLightData.position);
				cmd.SetGlobalVector(lightDirectionAndMaskId, additionalLightData.directionAndMask);
				cmd.SetGlobalVector(lightSpotAngleId, additionalLightData.spotAngle);
				cmd.SetGlobalVector(lightShadowDataId, additionalLightData.shadowData);
				cmd.SetGlobalVector(spotLightScaleId, new Vector4(num2, num2, 1f - num, visibleLight.range));
				cmd.SetGlobalVector(spotLightBiasId, new Vector4(0f, 0f, num, 0f));
				cmd.SetGlobalVector(spotLightGuardId, new Vector4(num3, num3, num3, num * visibleLight.range));
				cmd.SetGlobalInt(deferredLightIndex, (mainLightIndex == -1) ? i : (i - 1));
				cmd.DrawMesh(icoHemisphere, visibleLight.localToWorldMatrix, deferredLightingMaterial, 0, 0);
				cmd.DrawMesh(icoHemisphere, visibleLight.localToWorldMatrix, deferredLightingMaterial, 0, 1);
				cmd.EndSample("Spot Light");
			}
		}
		cmd.DisableShaderKeyword("_SPOT");
	}

	private void RenderDirectionalLight(RenderGraphContext context, NativeArray<VisibleLight> visibleLights)
	{
		CommandBuffer cmd = context.cmd;
		cmd.EnableShaderKeyword("_DIRECTIONAL");
		for (int i = 0; i < visibleLights.Length; i++)
		{
			VisibleLight visibleLight = visibleLights[i];
			if (visibleLight.lightType == LightType.Directional && (visibleLight.light.renderingLayerMask & cameraSettings.renderingLayerMask) != 0)
			{
				if (!lightShadowData.TryGetValue(visibleLight.light, out var value))
				{
					value = Vector4.zero;
				}
				DirectionalLightData directionalLightData = new DirectionalLightData(ref visibleLight, visibleLight.light, value);
				cmd.BeginSample("Directional Light");
				cmd.SetGlobalColor(lightColorId, directionalLightData.color);
				cmd.SetGlobalVector(lightDirectionAndMaskId, directionalLightData.directionAndMask);
				cmd.SetGlobalVector(lightShadowDataId, directionalLightData.shadowData);
				cmd.DrawProcedural(Matrix4x4.identity, deferredLightingMaterial, 2, MeshTopology.Triangles, 3);
				cmd.EndSample("Directional Light");
			}
		}
		cmd.DisableShaderKeyword("_DIRECTIONAL");
	}

	public static void Record(RenderGraph renderGraph, ContextContainer frameData, Material deferredLightingMaterial)
	{
		RustResourceDataContext rustResourceDataContext = frameData.Get<RustResourceDataContext>();
		RustCameraContext rustCameraContext = frameData.Get<RustCameraContext>();
		RustRenderingContext rustRenderingContext = frameData.Get<RustRenderingContext>();
		CameraSettings cameraSettings = rustCameraContext.CameraSettings;
		ShadowSettings shadows = rustRenderingContext.pipelineSettings.shadows;
		DeferredLightingPass passData;
		using RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<DeferredLightingPass>(profilingSampler.name, out passData);
		passData.Setup(deferredLightingMaterial, rustRenderingContext.cullResults, shadows, rustCameraContext.CameraSettings.renderingLayerMask, rustCameraContext.Camera, cameraSettings);
		passData.lightShadowData = rustResourceDataContext.LightShadowData;
		for (int i = 0; i < rustResourceDataContext.GBuffer.Length - 1; i++)
		{
			passData.gBufferTextureHandles[i] = renderGraphBuilder.ReadTexture(in rustResourceDataContext.GBuffer[i]);
		}
		passData.gBufferTextureHandles[3] = renderGraphBuilder.ReadWriteTexture(in rustResourceDataContext.GBuffer[3]);
		renderGraphBuilder.ReadWriteTexture(rustResourceDataContext.CameraDepth);
		renderGraphBuilder.ReadTexture(rustResourceDataContext.CameraDepthTexture);
		renderGraphBuilder.ReadTexture(rustResourceDataContext.MainShadowsTexture);
		renderGraphBuilder.ReadTexture(rustResourceDataContext.AdditionalShadowsTexture);
		renderGraphBuilder.ReadComputeBuffer(rustResourceDataContext.ShadowMatricesBuffer);
		renderGraphBuilder.ReadComputeBuffer(rustResourceDataContext.ShadowCascadesBuffer);
		renderGraphBuilder.ReadComputeBuffer(rustResourceDataContext.AdditionalShadowDataBuffer);
		renderGraphBuilder.SetRenderFunc(delegate(DeferredLightingPass pass, RenderGraphContext context)
		{
			pass.Render(context);
		});
	}
}
