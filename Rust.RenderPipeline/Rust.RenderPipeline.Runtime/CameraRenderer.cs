using System;
using Rust.RenderPipeline.Runtime.Passes;
using Rust.RenderPipeline.Runtime.Passes.Lighting;
using Rust.RenderPipeline.Runtime.Passes.PostProcessing;
using Rust.RenderPipeline.Runtime.RenderingContext;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.Rendering;

namespace Rust.RenderPipeline.Runtime;

public class CameraRenderer
{
	public const float RENDERSCALE_MIN = 0.1f;

	public const float RENDERSCALE_MAX = 2f;

	private static readonly CameraSettings defaultCameraSettings = new CameraSettings();

	private readonly RustRenderPipeline.PipelineMaterials pipelineMaterials;

	private RustRenderer rustRenderer;

	private LightCookieManager lightCookieManager;

	public CameraRenderer(RustRenderPipeline.PipelineMaterials pipelineMaterials)
	{
		this.pipelineMaterials = pipelineMaterials;
		LightCookieManager.Settings settings = LightCookieManager.Settings.Create();
		lightCookieManager = new LightCookieManager(ref settings);
	}

	public void Render(RenderGraph renderGraph, ScriptableRenderContext context, Camera camera, RustRenderPipelineSettings settings, RustRendererData rustRendererData)
	{
		CameraBufferSettings cameraBuffer = settings.cameraBuffer;
		PostFXSettings postFXSettings = settings.postFXSettings;
		ShadowSettings shadows = settings.shadows;
		if (rustRenderer == null)
		{
			rustRenderer = new RustRenderer();
		}
		rustRenderer.Initialize(rustRendererData);
		ProfilingSampler profilingSampler;
		CameraSettings settings2;
		if (camera.TryGetComponent<RustRenderPipelineCamera>(out var component))
		{
			profilingSampler = component.Sampler;
			settings2 = component.Settings;
		}
		else
		{
			profilingSampler = ProfilingSampler.Get(camera.cameraType);
			settings2 = defaultCameraSettings;
		}
		if (settings2.overridePostFX)
		{
			postFXSettings = settings2.postFXSettings;
		}
		bool postProcessingActive = settings.postProcessingEnabled && settings2.postProcessingEnabled && postFXSettings != null && PostFXSettings.AreApplicableTo(camera);
		bool useOpaqueColorTexture;
		bool useDepthTexture;
		if (camera.cameraType == CameraType.Reflection)
		{
			useOpaqueColorTexture = cameraBuffer.copyColorReflection;
			useDepthTexture = cameraBuffer.copyDepthReflection || settings.renderPath == RustRenderPipelineSettings.RenderPath.Deferred;
		}
		else
		{
			useOpaqueColorTexture = cameraBuffer.copyColor && settings2.copyColor;
			useDepthTexture = (cameraBuffer.copyDepth && settings2.copyDepth) || settings.renderPath == RustRenderPipelineSettings.RenderPath.Deferred;
		}
		float renderScale = settings2.GetRenderScale(cameraBuffer.renderScale);
		bool flag = !Mathf.Approximately(renderScale, 1f);
		if (!camera.TryGetCullingParameters(out var cullingParameters))
		{
			return;
		}
		cullingParameters.shadowDistance = Mathf.Min(shadows.maxDistance, camera.farClipPlane);
		CullingResults cullResults = context.Cull(ref cullingParameters);
		cameraBuffer.allowHDR &= camera.allowHDR;
		Vector2Int cameraBufferSize = default(Vector2Int);
		if (flag)
		{
			renderScale = Mathf.Clamp(renderScale, 0.1f, 2f);
			cameraBufferSize.x = (int)((float)camera.pixelWidth * renderScale);
			cameraBufferSize.y = (int)((float)camera.pixelHeight * renderScale);
		}
		else
		{
			cameraBufferSize.x = camera.pixelWidth;
			cameraBufferSize.y = camera.pixelHeight;
		}
		cameraBuffer.fxaaSettings.enabled &= settings2.allowFXAA;
		using ContextContainer contextContainer = rustRenderer.FrameData;
		RustRenderingContext rustRenderingContext = contextContainer.Create<RustRenderingContext>();
		rustRenderingContext.cullResults = cullResults;
		rustRenderingContext.pipelineSettings = settings;
		RustCameraContext rustCameraContext = contextContainer.Create<RustCameraContext>();
		rustCameraContext.Camera = camera;
		rustCameraContext.CameraSettings = settings2;
		rustCameraContext.CameraBufferSettings = cameraBuffer;
		rustCameraContext.CameraBufferSize = cameraBufferSize;
		rustCameraContext.UseOpaqueColorTexture = useOpaqueColorTexture;
		rustCameraContext.UseDepthTexture = useDepthTexture;
		rustCameraContext.PostProcessingActive = postProcessingActive;
		rustCameraContext.PostFXSettings = postFXSettings;
		RustResourceDataContext rustResourceDataContext = contextContainer.Create<RustResourceDataContext>();
		rustRenderer.AddRenderPasses();
		RenderGraphParameters parameters = new RenderGraphParameters
		{
			commandBuffer = CommandBufferPool.Get(),
			currentFrameIndex = Time.frameCount,
			executionName = profilingSampler.name,
			rendererListCulling = true,
			scriptableRenderContext = context
		};
		using (renderGraph.RecordAndExecute(in parameters))
		{
			using (new RenderGraphProfilingScope(renderGraph, profilingSampler))
			{
				rustResourceDataContext.InitFrame();
				switch (settings.renderPath)
				{
				case RustRenderPipelineSettings.RenderPath.ForwardPlus:
					ForwardPlusRenderPath(renderGraph);
					break;
				case RustRenderPipelineSettings.RenderPath.Deferred:
					DeferredRenderPath(renderGraph);
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
				rustResourceDataContext.EndFrame();
			}
		}
		context.ExecuteCommandBuffer(parameters.commandBuffer);
		context.Submit();
		CommandBufferPool.Release(parameters.commandBuffer);
	}

	private void ForwardPlusRenderPath(RenderGraph renderGraph)
	{
		ContextContainer frameData = rustRenderer.FrameData;
		RustCameraContext rustCameraContext = frameData.Get<RustCameraContext>();
		CameraRendererCopier copier = new CameraRendererCopier(pipelineMaterials.cameraRendererMaterial, rustCameraContext.Camera, rustCameraContext.CameraSettings.finalBlendMode);
		LightingPass.Record(renderGraph, frameData, lightCookieManager);
		SetupPass.Record(renderGraph, frameData);
		GeometryPass.Record(renderGraph, frameData, opaque: true);
		SkyboxPass.Record(renderGraph, frameData);
		CopyAttachmentsPass.Record(renderGraph, frameData, rustCameraContext.UseOpaqueColorTexture, rustCameraContext.UseDepthTexture, copier);
		GeometryPass.Record(renderGraph, frameData, opaque: false);
		if (rustCameraContext.PostProcessingActive)
		{
			PostFXPass.Record(renderGraph, frameData);
		}
		else
		{
			FinalPass.Record(renderGraph, frameData, copier);
		}
	}

	private void DeferredRenderPath(RenderGraph renderGraph)
	{
		ContextContainer frameData = rustRenderer.FrameData;
		RustCameraContext rustCameraContext = frameData.Get<RustCameraContext>();
		CameraRendererCopier copier = new CameraRendererCopier(pipelineMaterials.cameraRendererMaterial, rustCameraContext.Camera, rustCameraContext.CameraSettings.finalBlendMode);
		rustRenderer.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.BeforeRendering, RenderPassEvent.BeforeRenderingShadows);
		LightingPass.Record(renderGraph, frameData, lightCookieManager);
		rustRenderer.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.AfterRenderingShadows);
		SetupPass.Record(renderGraph, frameData);
		rustRenderer.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.BeforeRenderingPrePasses, RenderPassEvent.AfterRenderingPrePasses);
		rustRenderer.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.BeforeRenderingGBuffer);
		GBufferPass.Record(renderGraph, frameData);
		CopyAttachmentsPass.Record(renderGraph, frameData, copyColor: false, rustCameraContext.UseDepthTexture, copier);
		rustRenderer.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.AfterRenderingGBuffer, RenderPassEvent.BeforeRenderingDeferredIndirectLighting);
		DeferredIndirectLightingPass.Record_DrawIndirectLighting(renderGraph, frameData, pipelineMaterials.deferredIndirectLightingMaterial);
		rustRenderer.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.AfterRenderingDeferredIndirectLighting, RenderPassEvent.BeforeRenderingCombinedIndirectLighting);
		DeferredIndirectLightingPass.Record_CombineIndirectLighting(renderGraph, frameData, pipelineMaterials.deferredIndirectLightingMaterial);
		rustRenderer.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.AfterRenderingCombinedIndirectLighting, RenderPassEvent.BeforeRenderingDeferredLights);
		DeferredLightingPass.Record(renderGraph, frameData, pipelineMaterials.deferredLightingMaterial);
		rustRenderer.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.AfterRenderingDeferredLights);
		rustRenderer.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.BeforeRenderingSkybox);
		SkyboxPass.Record(renderGraph, frameData);
		rustRenderer.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.AfterRenderingSkybox);
		CopyAttachmentsPass.Record(renderGraph, frameData, rustCameraContext.UseOpaqueColorTexture, copyDepth: false, copier);
		rustRenderer.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.BeforeRenderingTransparents);
		GeometryPass.Record(renderGraph, frameData, opaque: false);
		rustRenderer.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.AfterRenderingTransparents);
		rustRenderer.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.BeforeRenderingPostProcessing);
		if (rustCameraContext.PostProcessingActive)
		{
			PostFXPass.Record(renderGraph, frameData);
		}
		else
		{
			FinalPass.Record(renderGraph, frameData, copier);
		}
		RustResourceDataContext rustResourceDataContext = frameData.Get<RustResourceDataContext>();
		rustResourceDataContext.ActiveColorID = RustResourceDataContext.ActiveID.BackBuffer;
		rustResourceDataContext.ActiveDepthID = RustResourceDataContext.ActiveID.BackBuffer;
		rustRenderer.RecordCustomRenderGraphPasses(renderGraph, RenderPassEvent.AfterRenderingPostProcessing, RenderPassEvent.AfterRendering);
	}

	public void Dispose()
	{
		rustRenderer?.Dispose();
		lightCookieManager?.Dispose();
	}
}
