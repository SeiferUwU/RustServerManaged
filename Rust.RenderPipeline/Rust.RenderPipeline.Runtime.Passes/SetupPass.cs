using Rust.RenderPipeline.Runtime.RenderingContext;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.Rendering;

namespace Rust.RenderPipeline.Runtime.Passes;

public class SetupPass
{
	private static readonly ProfilingSampler profilingSampler = new ProfilingSampler("Setup");

	private static readonly int attachmentSizeID = Shader.PropertyToID("_CameraBufferSize");

	private static readonly int envBrdfLutId = Shader.PropertyToID("global_EnvBrdfLut");

	private static readonly int envBrdfLutTexelSizeId = Shader.PropertyToID("global_EnvBrdfLut_TexelSize");

	private static readonly int blueNoiseId = Shader.PropertyToID("global_BlueNoise");

	private static readonly int blueNoiseRcpSizeId = Shader.PropertyToID("global_BlueNoiseRcpSize");

	private static readonly int preIntegratedFgdGgxId = Shader.PropertyToID("global_PFGD_GGX");

	private static readonly int preIntegratedFgdGgxTexelSizeId = Shader.PropertyToID("global_PFGD_GGX_TexelSize");

	private TextureHandle colorAttachment;

	private TextureHandle depthAttachment;

	private Vector2Int attachmentSize;

	private Camera camera;

	private CameraClearFlags clearFlags;

	private Texture envBrdfLutTexture;

	private Texture blueNoiseTexture;

	private Texture preIntegratedFgdGgxTexture;

	private void Render(RenderGraphContext context)
	{
		context.renderContext.SetupCameraProperties(camera);
		CommandBuffer cmd = context.cmd;
		cmd.SetRenderTarget(colorAttachment, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, depthAttachment, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store);
		cmd.ClearRenderTarget(clearFlags <= CameraClearFlags.Depth, clearFlags <= CameraClearFlags.Color, (clearFlags == CameraClearFlags.Color) ? camera.backgroundColor.linear : Color.clear);
		cmd.SetGlobalVector(attachmentSizeID, new Vector4(1f / (float)attachmentSize.x, 1f / (float)attachmentSize.y, attachmentSize.x, attachmentSize.y));
		if (blueNoiseTexture != null)
		{
			cmd.SetGlobalTexture(blueNoiseId, blueNoiseTexture);
			cmd.SetGlobalFloat(blueNoiseRcpSizeId, 1f / (float)blueNoiseTexture.width);
		}
		if (preIntegratedFgdGgxTexture != null)
		{
			int width = preIntegratedFgdGgxTexture.width;
			int height = preIntegratedFgdGgxTexture.height;
			cmd.SetGlobalTexture(preIntegratedFgdGgxId, preIntegratedFgdGgxTexture);
			cmd.SetGlobalVector(preIntegratedFgdGgxTexelSizeId, new Vector4(1f / (float)width, 1f / (float)height, width, height));
		}
		if (envBrdfLutTexture != null)
		{
			int width2 = envBrdfLutTexture.width;
			int height2 = envBrdfLutTexture.height;
			cmd.SetGlobalTexture(envBrdfLutId, envBrdfLutTexture);
			cmd.SetGlobalVector(envBrdfLutTexelSizeId, new Vector4(1f / (float)width2, 1f / (float)height2, width2, height2));
		}
		context.renderContext.ExecuteCommandBuffer(cmd);
		cmd.Clear();
	}

	public static void Record(RenderGraph renderGraph, ContextContainer frameData)
	{
		RustResourceDataContext rustResourceDataContext = frameData.Get<RustResourceDataContext>();
		RustCameraContext rustCameraContext = frameData.Get<RustCameraContext>();
		RustRenderingContext rustRenderingContext = frameData.Get<RustRenderingContext>();
		SetupPass passData;
		using RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<SetupPass>("Setup", out passData, profilingSampler);
		passData.attachmentSize = rustCameraContext.CameraBufferSize;
		passData.camera = rustCameraContext.Camera;
		passData.clearFlags = rustCameraContext.Camera.clearFlags;
		RustRenderPipelineSettings.PipelineTextures pipelineTextures = rustRenderingContext.pipelineSettings.pipelineTextures;
		passData.envBrdfLutTexture = pipelineTextures.environmentBrdfLut;
		passData.blueNoiseTexture = pipelineTextures.blueNoise;
		passData.preIntegratedFgdGgxTexture = pipelineTextures.preIntegratedFgdGgx;
		if (passData.clearFlags > CameraClearFlags.Color)
		{
			passData.clearFlags = CameraClearFlags.Color;
		}
		TextureDesc textureDesc = new TextureDesc(rustCameraContext.CameraBufferSize.x, rustCameraContext.CameraBufferSize.y);
		textureDesc.colorFormat = SystemInfo.GetGraphicsFormat(rustCameraContext.CameraBufferSettings.allowHDR ? DefaultFormat.HDR : DefaultFormat.LDR);
		textureDesc.name = "Color Attachment";
		TextureDesc desc = (rustCameraContext.CameraTargetDescriptor = textureDesc);
		rustResourceDataContext.CameraColor = (passData.colorAttachment = renderGraphBuilder.WriteTexture(renderGraph.CreateTexture(in desc)));
		if (rustCameraContext.UseOpaqueColorTexture)
		{
			desc.name = "Color Copy";
			rustResourceDataContext.CameraOpaqueTexture = renderGraph.CreateTexture(in desc);
		}
		desc.depthBufferBits = DepthBits.Depth32;
		desc.name = "Depth Attachment";
		rustResourceDataContext.CameraDepth = (passData.depthAttachment = renderGraphBuilder.WriteTexture(renderGraph.CreateTexture(in desc)));
		if (rustCameraContext.UseDepthTexture)
		{
			desc.name = "Depth Copy";
			rustResourceDataContext.CameraDepthTexture = renderGraph.CreateTexture(in desc);
		}
		rustResourceDataContext.ActiveColorID = RustResourceDataContext.ActiveID.Camera;
		rustResourceDataContext.ActiveDepthID = RustResourceDataContext.ActiveID.Camera;
		renderGraphBuilder.AllowPassCulling(value: false);
		renderGraphBuilder.SetRenderFunc(delegate(SetupPass pass, RenderGraphContext context)
		{
			pass.Render(context);
		});
	}
}
