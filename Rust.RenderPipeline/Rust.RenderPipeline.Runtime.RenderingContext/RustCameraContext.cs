using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;

namespace Rust.RenderPipeline.Runtime.RenderingContext;

public class RustCameraContext : ContextItem
{
	public Camera Camera { get; internal set; }

	public CameraSettings CameraSettings { get; internal set; }

	public CameraBufferSettings CameraBufferSettings { get; internal set; }

	public PostFXSettings PostFXSettings { get; internal set; }

	public TextureDesc CameraTargetDescriptor { get; internal set; }

	public Vector2Int CameraBufferSize { get; internal set; }

	public Matrix4x4 ViewMatrix { get; internal set; }

	public Matrix4x4 ProjectionMatrix { get; internal set; }

	public bool UseOpaqueColorTexture { get; internal set; }

	public bool UseDepthTexture { get; internal set; }

	public bool PostProcessingActive { get; internal set; }

	public override void Reset()
	{
		Camera = null;
		CameraSettings = null;
		CameraBufferSettings = default(CameraBufferSettings);
		PostFXSettings = null;
		CameraTargetDescriptor = default(TextureDesc);
		CameraBufferSize = default(Vector2Int);
		ViewMatrix = default(Matrix4x4);
		ProjectionMatrix = default(Matrix4x4);
		UseOpaqueColorTexture = false;
		UseDepthTexture = false;
		PostProcessingActive = false;
	}
}
