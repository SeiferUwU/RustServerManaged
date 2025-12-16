using Rust.RenderPipeline.Runtime.RenderingContext;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.Rendering;

namespace Rust.RenderPipeline.Runtime.Passes;

public class CopyAttachmentsPass
{
	private static readonly ProfilingSampler profilingSampler = new ProfilingSampler("Copy Attachments");

	private static readonly int colorCopyID = Shader.PropertyToID("_CameraColorTexture");

	private static readonly int depthCopyID = Shader.PropertyToID("_CameraDepthTexture");

	private bool copyColor;

	private bool copyDepth;

	private CameraRendererCopier copier;

	private TextureHandle colorAttachment;

	private TextureHandle depthAttachment;

	private TextureHandle colorCopy;

	private TextureHandle depthCopy;

	private void Render(RenderGraphContext context)
	{
		CommandBuffer cmd = context.cmd;
		if (copyColor)
		{
			copier.Copy(cmd, colorAttachment, colorCopy, isDepth: false);
			cmd.SetGlobalTexture(colorCopyID, colorCopy);
		}
		if (copyDepth)
		{
			copier.Copy(cmd, depthAttachment, depthCopy, isDepth: true);
			cmd.SetGlobalTexture(depthCopyID, depthCopy);
		}
		if (CameraRendererCopier.RequiresRenderTargetResetAfterCopy)
		{
			cmd.SetRenderTarget(colorAttachment, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store, depthAttachment, RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
		}
		context.renderContext.ExecuteCommandBuffer(cmd);
		cmd.Clear();
	}

	public static void Record(RenderGraph renderGraph, ContextContainer frameData, bool copyColor, bool copyDepth, CameraRendererCopier copier)
	{
		frameData.Get<RustCameraContext>();
		if (!copyColor && !copyDepth)
		{
			return;
		}
		RustResourceDataContext rustResourceDataContext = frameData.Get<RustResourceDataContext>();
		CopyAttachmentsPass passData;
		using RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<CopyAttachmentsPass>(profilingSampler.name, out passData, profilingSampler);
		passData.copyColor = copyColor;
		passData.copyDepth = copyDepth;
		passData.copier = copier;
		passData.colorAttachment = renderGraphBuilder.ReadTexture(rustResourceDataContext.CameraColor);
		passData.depthAttachment = renderGraphBuilder.ReadTexture(rustResourceDataContext.CameraDepth);
		if (copyColor)
		{
			passData.colorCopy = renderGraphBuilder.WriteTexture(rustResourceDataContext.CameraOpaqueTexture);
		}
		if (copyDepth)
		{
			passData.depthCopy = renderGraphBuilder.WriteTexture(rustResourceDataContext.CameraDepthTexture);
		}
		renderGraphBuilder.SetRenderFunc(delegate(CopyAttachmentsPass pass, RenderGraphContext context)
		{
			pass.Render(context);
		});
	}
}
