using UnityEngine;
using UnityEngine.Rendering;

namespace Rust.RenderPipeline.Runtime;

[CreateAssetMenu(menuName = "Rendering/Rust Render Pipeline Asset")]
public class RustRenderPipelineAsset : RenderPipelineAsset
{
	[SerializeField]
	private RustRendererData defaultRendererData;

	[SerializeField]
	private RustRenderPipelineSettings settings;

	protected override UnityEngine.Rendering.RenderPipeline CreatePipeline()
	{
		return new RustRenderPipeline(defaultRendererData, settings);
	}
}
