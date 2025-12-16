using System.Diagnostics;
using UnityEngine.Experimental.Rendering.RenderGraphModule;

namespace Rust.RenderPipeline.Runtime.Passes;

public class GizmosPass
{
	[Conditional("UNITY_EDITOR")]
	public static void Record(RenderGraph renderGraph, ContextContainer frameData, CameraRendererCopier copier)
	{
	}
}
