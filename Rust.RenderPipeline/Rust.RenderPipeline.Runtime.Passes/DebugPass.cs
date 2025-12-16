using System.Diagnostics;
using Rust.RenderPipeline.Runtime.RenderingContext;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.Rendering;

namespace Rust.RenderPipeline.Runtime.Passes;

public class DebugPass
{
	private static readonly ProfilingSampler sampler = new ProfilingSampler("Debug");

	public TextureHandle[] gBufferHandles;

	public TextureHandle indirectDiffuseHandle;

	public TextureHandle indirectSpecularHandle;

	[Conditional("DEVELOPMENT_BUILD")]
	[Conditional("UNITY_EDITOR")]
	public static void Record(RenderGraph renderGraph, ContextContainer frameData)
	{
		RustCameraContext rustCameraContext = frameData.Get<RustCameraContext>();
		RustResourceDataContext rustResourceDataContext = frameData.Get<RustResourceDataContext>();
		if (!CameraDebugger.IsActive || rustCameraContext.Camera.cameraType > CameraType.SceneView)
		{
			return;
		}
		DebugPass passData;
		using RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<DebugPass>(sampler.name, out passData, sampler);
		passData.gBufferHandles = rustResourceDataContext.GBuffer;
		passData.indirectDiffuseHandle = rustResourceDataContext.IndirectDiffuseHandle;
		passData.indirectSpecularHandle = rustResourceDataContext.IndirectSpecularHandle;
		TextureHandle[] gBuffer = rustResourceDataContext.GBuffer;
		for (int i = 0; i < gBuffer.Length; i++)
		{
			TextureHandle input = gBuffer[i];
			renderGraphBuilder.ReadTexture(in input);
		}
		renderGraphBuilder.ReadTexture(rustResourceDataContext.IndirectDiffuseHandle);
		renderGraphBuilder.ReadTexture(rustResourceDataContext.IndirectSpecularHandle);
		renderGraphBuilder.ReadComputeBuffer(rustResourceDataContext.LightTilesBuffer);
		renderGraphBuilder.SetRenderFunc<DebugPass>(delegate
		{
		});
	}
}
