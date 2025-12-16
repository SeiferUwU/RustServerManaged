using Rust.RenderPipeline.Runtime.RenderingContext;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.Rendering;

namespace Rust.RenderPipeline.Runtime.Passes.Lighting;

public class DeferredIndirectLightingPass
{
	private enum Pass
	{
		IndirectLighting,
		Combine
	}

	private static readonly ProfilingSampler drawIndirectLightingSampler = new ProfilingSampler("Draw Indirect Lighting");

	private static readonly ProfilingSampler combineIndirectLightingSampler = new ProfilingSampler("Combine Indirect Lighting");

	private static readonly int indirectDiffuseTextureId = Shader.PropertyToID("_IndirectDiffuseTexture");

	private static readonly int indirectSpecularTextureId = Shader.PropertyToID("_IndirectSpecularTexture");

	private static readonly int inverseViewProjectionMatrixId = Shader.PropertyToID("_InvViewProjection");

	private static readonly int gBuffer0Id = Shader.PropertyToID("_CameraGBufferTexture0");

	private static readonly int gBuffer1Id = Shader.PropertyToID("_CameraGBufferTexture1");

	private static readonly int gBuffer2Id = Shader.PropertyToID("_CameraGBufferTexture2");

	private static readonly int gBuffer3Id = Shader.PropertyToID("_CameraGBufferTexture3");

	private readonly RenderTargetIdentifier[] indirectLightingRenderTargets = new RenderTargetIdentifier[2];

	private readonly TextureHandle[] gBufferTextureHandles = new TextureHandle[RustResourceDataContext.gBufferTextureCount];

	private TextureHandle indirectDiffuseHandle;

	private TextureHandle indirectSpecularHandle;

	private TextureHandle cameraColorHandle;

	private TextureHandle cameraDepthHandle;

	private Camera camera;

	private Material material;

	private void Setup(Camera camera, Material material)
	{
		this.camera = camera;
		this.material = material;
	}

	private void DrawIndirectLighting(RenderGraphContext context)
	{
		CommandBuffer cmd = context.cmd;
		Matrix4x4 inverse = (GL.GetGPUProjectionMatrix(camera.projectionMatrix, renderIntoTexture: true) * camera.worldToCameraMatrix).inverse;
		indirectLightingRenderTargets[0] = indirectDiffuseHandle;
		indirectLightingRenderTargets[1] = indirectSpecularHandle;
		cmd.SetGlobalMatrix(inverseViewProjectionMatrixId, inverse);
		cmd.GetTemporaryRT(indirectSpecularTextureId, camera.pixelWidth, camera.pixelHeight, 0, FilterMode.Point, GraphicsFormat.R16G16B16A16_SFloat);
		cmd.SetRenderTarget(indirectLightingRenderTargets, cameraDepthHandle);
		cmd.SetGlobalTexture(gBuffer0Id, gBufferTextureHandles[0]);
		cmd.SetGlobalTexture(gBuffer1Id, gBufferTextureHandles[1]);
		cmd.SetGlobalTexture(gBuffer2Id, gBufferTextureHandles[2]);
		cmd.SetGlobalTexture(gBuffer3Id, gBufferTextureHandles[3]);
		cmd.ClearRenderTarget(RTClearFlags.Color, Color.clear, 1f, 0u);
		cmd.DrawProcedural(Matrix4x4.identity, material, 0, MeshTopology.Triangles, 3);
	}

	private void CombineIndirectLighting(RenderGraphContext context)
	{
		CommandBuffer cmd = context.cmd;
		cmd.SetRenderTarget(cameraColorHandle, cameraDepthHandle);
		cmd.SetGlobalTexture(indirectDiffuseTextureId, indirectDiffuseHandle);
		cmd.SetGlobalTexture(indirectSpecularTextureId, indirectSpecularHandle);
		cmd.DrawProcedural(Matrix4x4.identity, material, 1, MeshTopology.Triangles, 3);
	}

	private static bool SkipPass(Camera camera)
	{
		return camera.cameraType == CameraType.Reflection;
	}

	public static void Record_DrawIndirectLighting(RenderGraph renderGraph, ContextContainer frameData, Material material)
	{
		RustCameraContext rustCameraContext = frameData.Get<RustCameraContext>();
		if (SkipPass(rustCameraContext.Camera))
		{
			return;
		}
		RustResourceDataContext rustResourceDataContext = frameData.Get<RustResourceDataContext>();
		DeferredIndirectLightingPass passData;
		using RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<DeferredIndirectLightingPass>(drawIndirectLightingSampler.name, out passData, drawIndirectLightingSampler);
		TextureDesc textureDesc = new TextureDesc(rustCameraContext.CameraBufferSize.x, rustCameraContext.CameraBufferSize.y);
		textureDesc.colorFormat = SystemInfo.GetGraphicsFormat(rustCameraContext.CameraBufferSettings.allowHDR ? DefaultFormat.HDR : DefaultFormat.LDR);
		textureDesc.name = "Indirect Diffuse";
		TextureDesc desc = textureDesc;
		rustResourceDataContext.IndirectDiffuseHandle = (passData.indirectDiffuseHandle = renderGraphBuilder.WriteTexture(renderGraph.CreateTexture(in desc)));
		desc.name = "Indirect Specular";
		rustResourceDataContext.IndirectSpecularHandle = (passData.indirectSpecularHandle = renderGraphBuilder.WriteTexture(renderGraph.CreateTexture(in desc)));
		passData.Setup(rustCameraContext.Camera, material);
		for (int i = 0; i < passData.gBufferTextureHandles.Length; i++)
		{
			passData.gBufferTextureHandles[i] = renderGraphBuilder.ReadTexture(in rustResourceDataContext.GBuffer[i]);
		}
		renderGraphBuilder.ReadTexture(rustResourceDataContext.CameraDepthTexture);
		passData.cameraDepthHandle = renderGraphBuilder.ReadTexture(rustResourceDataContext.CameraDepth);
		renderGraphBuilder.SetRenderFunc(delegate(DeferredIndirectLightingPass pass, RenderGraphContext context)
		{
			pass.DrawIndirectLighting(context);
		});
	}

	public static void Record_CombineIndirectLighting(RenderGraph renderGraph, ContextContainer frameData, Material material)
	{
		Camera camera = frameData.Get<RustCameraContext>().Camera;
		if (SkipPass(camera))
		{
			return;
		}
		RustResourceDataContext rustResourceDataContext = frameData.Get<RustResourceDataContext>();
		DeferredIndirectLightingPass passData;
		using RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<DeferredIndirectLightingPass>(combineIndirectLightingSampler.name, out passData, combineIndirectLightingSampler);
		passData.Setup(camera, material);
		passData.indirectDiffuseHandle = renderGraphBuilder.ReadTexture(rustResourceDataContext.IndirectDiffuseHandle);
		passData.indirectSpecularHandle = renderGraphBuilder.ReadTexture(rustResourceDataContext.IndirectSpecularHandle);
		passData.cameraColorHandle = renderGraphBuilder.ReadWriteTexture(rustResourceDataContext.CameraColor);
		passData.cameraDepthHandle = renderGraphBuilder.ReadTexture(rustResourceDataContext.CameraDepth);
		renderGraphBuilder.SetRenderFunc(delegate(DeferredIndirectLightingPass pass, RenderGraphContext context)
		{
			pass.CombineIndirectLighting(context);
		});
	}
}
