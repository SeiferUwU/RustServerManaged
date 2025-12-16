using Rust.RenderPipeline.Runtime.RenderingContext;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.Rendering;

namespace Rust.RenderPipeline.Runtime.Passes;

public class FinalPass
{
	private static readonly ProfilingSampler profilingSampler = new ProfilingSampler("Final");

	private CameraRendererCopier copier;

	private TextureHandle colorAttachment;

	private void Render(RenderGraphContext context)
	{
		CommandBuffer cmd = context.cmd;
		copier.CopyToCameraTarget(cmd, colorAttachment);
		context.renderContext.ExecuteCommandBuffer(cmd);
		cmd.Clear();
	}

	public static void Record(RenderGraph renderGraph, ContextContainer frameData, CameraRendererCopier copier)
	{
		RustResourceDataContext rustResourceDataContext = frameData.Get<RustResourceDataContext>();
		FinalPass passData;
		using RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<FinalPass>("Final", out passData, profilingSampler);
		passData.copier = copier;
		passData.colorAttachment = renderGraphBuilder.ReadTexture(rustResourceDataContext.CameraColor);
		renderGraphBuilder.SetRenderFunc(delegate(FinalPass pass, RenderGraphContext context)
		{
			pass.Render(context);
		});
	}
}
