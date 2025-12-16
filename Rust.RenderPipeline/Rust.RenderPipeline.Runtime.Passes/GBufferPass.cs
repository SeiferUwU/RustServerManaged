using System;
using Rust.RenderPipeline.Runtime.RenderingContext;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RendererUtils;

namespace Rust.RenderPipeline.Runtime.Passes;

public class GBufferPass
{
	private static readonly ProfilingSampler profilingSampler = new ProfilingSampler("GBuffer");

	private static readonly ShaderTagId[] shaderTagIds = new ShaderTagId[2]
	{
		new ShaderTagId("RustGBuffer"),
		new ShaderTagId("Deferred")
	};

	private readonly RenderTargetIdentifier[] gBufferRenderTargetIdentifiers = new RenderTargetIdentifier[RustResourceDataContext.gBufferTextureCount];

	private RendererListHandle renderListHandle;

	private readonly TextureHandle[] gBufferTextureHandles = new TextureHandle[RustResourceDataContext.gBufferTextureCount];

	private TextureHandle depthAttachment;

	private Camera camera;

	private void Render(RenderGraphContext context)
	{
		context.renderContext.SetupCameraProperties(camera);
		CommandBuffer cmd = context.cmd;
		for (int i = 0; i < gBufferRenderTargetIdentifiers.Length; i++)
		{
			gBufferRenderTargetIdentifiers[i] = gBufferTextureHandles[i];
		}
		cmd.SetRenderTarget(gBufferRenderTargetIdentifiers, depthAttachment);
		cmd.ClearRenderTarget(clearDepth: true, clearColor: true, Color.clear);
		context.cmd.DrawRendererList(renderListHandle);
		cmd.SetRenderTarget(gBufferRenderTargetIdentifiers[3], depthAttachment);
		context.renderContext.ExecuteCommandBuffer(context.cmd);
		context.cmd.Clear();
	}

	public static void Record(RenderGraph renderGraph, ContextContainer frameData)
	{
		RustRenderingContext rustRenderingContext = frameData.Get<RustRenderingContext>();
		RustResourceDataContext rustResourceDataContext = frameData.Get<RustResourceDataContext>();
		RustCameraContext rustCameraContext = frameData.Get<RustCameraContext>();
		GBufferPass passData;
		RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<GBufferPass>(profilingSampler.name, out passData, profilingSampler);
		try
		{
			rustRenderingContext.perObjectData = PerObjectData.LightProbe | PerObjectData.ReflectionProbes;
			GBufferPass gBufferPass = passData;
			RendererListDesc desc = new RendererListDesc(shaderTagIds, rustRenderingContext.cullResults, rustCameraContext.Camera)
			{
				sortingCriteria = SortingCriteria.CommonOpaque,
				rendererConfiguration = rustRenderingContext.perObjectData,
				renderQueueRange = RenderQueueRange.opaque,
				renderingLayerMask = (uint)rustCameraContext.CameraSettings.renderingLayerMask
			};
			gBufferPass.renderListHandle = renderGraphBuilder.UseRendererList(renderGraph.CreateRendererList(in desc));
			passData.camera = rustCameraContext.Camera;
			passData.depthAttachment = renderGraphBuilder.ReadWriteTexture(rustResourceDataContext.CameraDepth);
			TextureDesc textureDesc = new TextureDesc(rustCameraContext.CameraBufferSize.x, rustCameraContext.CameraBufferSize.y);
			textureDesc.name = "GBuffer0";
			textureDesc.colorFormat = SystemInfo.GetGraphicsFormat(DefaultFormat.LDR);
			TextureDesc desc2 = textureDesc;
			rustResourceDataContext.GBuffer[0] = (passData.gBufferTextureHandles[0] = renderGraphBuilder.WriteTexture(renderGraph.CreateTexture(in desc2)));
			desc2.name = "GBuffer1";
			rustResourceDataContext.GBuffer[1] = (passData.gBufferTextureHandles[1] = renderGraphBuilder.WriteTexture(renderGraph.CreateTexture(in desc2)));
			desc2.name = "GBuffer2";
			desc2.colorFormat = GraphicsFormat.A2B10G10R10_UNormPack32;
			rustResourceDataContext.GBuffer[2] = (passData.gBufferTextureHandles[2] = renderGraphBuilder.WriteTexture(renderGraph.CreateTexture(in desc2)));
			desc2.name = "GBuffer3";
			rustResourceDataContext.GBuffer[3] = (passData.gBufferTextureHandles[3] = renderGraphBuilder.ReadWriteTexture(rustResourceDataContext.CameraColor));
			renderGraphBuilder.SetRenderFunc(delegate(GBufferPass pass, RenderGraphContext context)
			{
				pass.Render(context);
			});
		}
		finally
		{
			((IDisposable)renderGraphBuilder/*cast due to .constrained prefix*/).Dispose();
		}
	}
}
