using UnityEngine.Rendering;

namespace Rust.RenderPipeline.Runtime.RenderingContext;

public class RustRenderingContext : ContextItem
{
	public RustRenderPipelineSettings pipelineSettings;

	public CullingResults cullResults;

	public PerObjectData perObjectData;

	public override void Reset()
	{
		pipelineSettings = null;
		cullResults = default(CullingResults);
		perObjectData = PerObjectData.None;
	}
}
