using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.Rendering;

namespace Rust.RenderPipeline.Runtime;

public class RustRenderPipeline : UnityEngine.Rendering.RenderPipeline
{
	public readonly struct PipelineMaterials
	{
		public readonly Material cameraRendererMaterial;

		public readonly Material cameraDebuggerMaterial;

		public readonly Material deferredLightingMaterial;

		public readonly Material deferredIndirectLightingMaterial;

		public PipelineMaterials(RustRenderPipelineSettings.PipelineShaders pipelineShaders)
		{
			cameraRendererMaterial = CoreUtils.CreateEngineMaterial(pipelineShaders.cameraRendererShader);
			cameraDebuggerMaterial = CoreUtils.CreateEngineMaterial(pipelineShaders.cameraDebuggerShader);
			deferredLightingMaterial = CoreUtils.CreateEngineMaterial(pipelineShaders.deferredLightingShader);
			deferredIndirectLightingMaterial = CoreUtils.CreateEngineMaterial(pipelineShaders.deferredIndirectLightingShader);
		}

		public void Dispose()
		{
			CoreUtils.Destroy(cameraRendererMaterial);
			CoreUtils.Destroy(cameraDebuggerMaterial);
			CoreUtils.Destroy(deferredLightingMaterial);
			CoreUtils.Destroy(deferredIndirectLightingMaterial);
		}
	}

	private readonly RenderGraph renderGraph = new RenderGraph("Rust SRP Render Graph");

	private readonly CameraRenderer cameraRenderer;

	private readonly RustRenderPipelineSettings settings;

	private readonly PipelineMaterials pipelineMaterials;

	private readonly RustRendererData defaultRendererData;

	public RustRenderPipeline(RustRendererData defaultRendererData, RustRenderPipelineSettings settings)
	{
		this.settings = settings;
		this.defaultRendererData = defaultRendererData;
		GraphicsSettings.useScriptableRenderPipelineBatching = settings.useSrpBatching;
		GraphicsSettings.lightsUseLinearIntensity = true;
		pipelineMaterials = new PipelineMaterials(settings.pipelineShaders);
		Blitter.Initialize(settings.pipelineShaders.coreBlit, settings.pipelineShaders.coreBlitColorAndDepth);
		cameraRenderer = new CameraRenderer(pipelineMaterials);
	}

	protected override void Render(ScriptableRenderContext context, List<Camera> cameras)
	{
		UnityEngine.Rendering.RenderPipeline.BeginContextRendering(context, cameras);
		foreach (Camera camera in cameras)
		{
			RustRenderPipelineCamera component = camera.GetComponent<RustRenderPipelineCamera>();
			RustRendererData rustRendererData = ((component == null || component.Settings.rustRendererData == null) ? defaultRendererData : component.Settings.rustRendererData);
			UnityEngine.Rendering.RenderPipeline.BeginCameraRendering(context, camera);
			cameraRenderer.Render(renderGraph, context, camera, settings, rustRendererData);
			UnityEngine.Rendering.RenderPipeline.EndCameraRendering(context, camera);
		}
		renderGraph.EndFrame();
		UnityEngine.Rendering.RenderPipeline.EndContextRendering(context, cameras);
	}

	protected override void Render(ScriptableRenderContext context, Camera[] cameras)
	{
	}

	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
		cameraRenderer.Dispose();
		pipelineMaterials.Dispose();
		renderGraph.Cleanup();
		Blitter.Cleanup();
	}
}
