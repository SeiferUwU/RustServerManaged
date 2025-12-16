using Rust.RenderPipeline.Runtime.RenderingContext;
using UnityEngine;
using UnityEngine.Rendering;

namespace Rust.RenderPipeline.Runtime.Passes.PostProcessing;

public struct PostFXBlitter
{
	private static readonly Rect fullViewRect = new Rect(0f, 0f, 1f, 1f);

	private static readonly int fxSourceId = Shader.PropertyToID("_PostFXSource");

	private static readonly int finalSrcBlendId = Shader.PropertyToID("_FinalSrcBlend");

	private static readonly int finalDstBlendId = Shader.PropertyToID("_FinalDstBlend");

	private readonly Camera camera;

	private readonly CameraSettings cameraSettings;

	private readonly PostFXSettings postFXSettings;

	public PostFXBlitter(ContextContainer frameData)
	{
		RustCameraContext rustCameraContext = frameData.Get<RustCameraContext>();
		camera = rustCameraContext.Camera;
		cameraSettings = rustCameraContext.CameraSettings;
		postFXSettings = rustCameraContext.PostFXSettings;
	}

	public void Draw(CommandBuffer commandBuffer, RenderTargetIdentifier to, PostFXPass.Pass pass)
	{
		commandBuffer.SetRenderTarget(to, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store);
		commandBuffer.DrawProcedural(Matrix4x4.identity, postFXSettings.Material, (int)pass, MeshTopology.Triangles, 3);
	}

	public void Draw(CommandBuffer commandBuffer, RenderTargetIdentifier from, RenderTargetIdentifier to, PostFXPass.Pass pass)
	{
		commandBuffer.SetGlobalTexture(fxSourceId, from);
		commandBuffer.SetRenderTarget(to, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store);
		commandBuffer.DrawProcedural(Matrix4x4.identity, postFXSettings.Material, (int)pass, MeshTopology.Triangles, 3);
	}

	public void DrawFinal(CommandBuffer commandBuffer, RenderTargetIdentifier from, PostFXPass.Pass pass)
	{
		commandBuffer.SetGlobalFloat(finalSrcBlendId, (float)cameraSettings.finalBlendMode.source);
		commandBuffer.SetGlobalFloat(finalDstBlendId, (float)cameraSettings.finalBlendMode.destination);
		commandBuffer.SetGlobalTexture(fxSourceId, from);
		commandBuffer.SetRenderTarget(BuiltinRenderTextureType.CameraTarget, (cameraSettings.finalBlendMode.destination == BlendMode.Zero && camera.rect == fullViewRect) ? RenderBufferLoadAction.DontCare : RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
		commandBuffer.SetViewport(camera.pixelRect);
		commandBuffer.DrawProcedural(Matrix4x4.identity, postFXSettings.Material, (int)pass, MeshTopology.Triangles, 3);
	}
}
