using UnityEngine;
using UnityEngine.Rendering;

namespace Rust.RenderPipeline.Runtime;

public readonly struct CameraRendererCopier
{
	private static readonly bool copyTextureSupported = SystemInfo.copyTextureSupport > CopyTextureSupport.None;

	private static readonly Rect fullViewRect = new Rect(0f, 0f, 1f, 1f);

	private static readonly int sourceTextureId = Shader.PropertyToID("_SourceTexture");

	private static readonly int srcBlendId = Shader.PropertyToID("_CameraSrcBlend");

	private static readonly int dstBlendId = Shader.PropertyToID("_CameraDstBlend");

	private readonly CameraSettings.FinalBlendMode finalBlendMode;

	private readonly Material material;

	private readonly Camera camera;

	public static bool RequiresRenderTargetResetAfterCopy => !copyTextureSupported;

	public Camera Camera => camera;

	public CameraRendererCopier(Material material, Camera camera, CameraSettings.FinalBlendMode finalBlendMode)
	{
		this.material = material;
		this.camera = camera;
		this.finalBlendMode = finalBlendMode;
	}

	public void Copy(CommandBuffer commandBuffer, RenderTargetIdentifier from, RenderTargetIdentifier to, bool isDepth)
	{
		if (copyTextureSupported)
		{
			commandBuffer.CopyTexture(from, to);
		}
		else
		{
			CopyByDrawing(commandBuffer, from, to, isDepth);
		}
	}

	public void CopyByDrawing(CommandBuffer commandBuffer, RenderTargetIdentifier from, RenderTargetIdentifier to, bool isDepth)
	{
		commandBuffer.SetGlobalTexture(sourceTextureId, from);
		commandBuffer.SetRenderTarget(to, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store);
		commandBuffer.SetViewport(camera.pixelRect);
		commandBuffer.DrawProcedural(Matrix4x4.identity, material, isDepth ? 1 : 0, MeshTopology.Triangles, 3);
	}

	public void CopyToCameraTarget(CommandBuffer commandBuffer, RenderTargetIdentifier from)
	{
		commandBuffer.SetGlobalFloat(srcBlendId, (float)finalBlendMode.source);
		commandBuffer.SetGlobalFloat(dstBlendId, (float)finalBlendMode.destination);
		commandBuffer.SetGlobalTexture(sourceTextureId, from);
		commandBuffer.SetRenderTarget(BuiltinRenderTextureType.CameraTarget, (finalBlendMode.destination == BlendMode.Zero && camera.rect == fullViewRect) ? RenderBufferLoadAction.DontCare : RenderBufferLoadAction.Load, RenderBufferStoreAction.Store);
		commandBuffer.SetViewport(camera.pixelRect);
		commandBuffer.DrawProcedural(Matrix4x4.identity, material, 0, MeshTopology.Triangles, 3);
		commandBuffer.SetGlobalFloat(srcBlendId, 1f);
		commandBuffer.SetGlobalFloat(dstBlendId, 0f);
	}
}
