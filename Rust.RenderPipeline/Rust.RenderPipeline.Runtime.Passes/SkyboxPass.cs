using Rust.RenderPipeline.Runtime.RenderingContext;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.Rendering;

namespace Rust.RenderPipeline.Runtime.Passes;

public class SkyboxPass
{
	private static readonly ProfilingSampler profilingSampler = new ProfilingSampler("Skybox");

	private Camera camera;

	private void Render(RenderGraphContext context)
	{
		context.renderContext.ExecuteCommandBuffer(context.cmd);
		context.cmd.Clear();
		context.renderContext.DrawSkybox(camera);
	}

	public static void Record(RenderGraph renderGraph, ContextContainer frameData)
	{
		RustResourceDataContext rustResourceDataContext = frameData.Get<RustResourceDataContext>();
		RustCameraContext rustCameraContext = frameData.Get<RustCameraContext>();
		if (rustCameraContext.Camera.clearFlags != CameraClearFlags.Skybox)
		{
			return;
		}
		SkyboxPass passData;
		using RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<SkyboxPass>(profilingSampler.name, out passData, profilingSampler);
		passData.camera = rustCameraContext.Camera;
		renderGraphBuilder.ReadWriteTexture(rustResourceDataContext.CameraColor);
		renderGraphBuilder.ReadTexture(rustResourceDataContext.CameraDepth);
		renderGraphBuilder.SetRenderFunc(delegate(SkyboxPass pass, RenderGraphContext context)
		{
			pass.Render(context);
		});
	}
}
