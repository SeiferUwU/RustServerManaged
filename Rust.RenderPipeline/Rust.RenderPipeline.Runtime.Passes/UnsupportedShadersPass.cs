using System.Diagnostics;
using UnityEngine.Experimental.Rendering.RenderGraphModule;

namespace Rust.RenderPipeline.Runtime.Passes;

public class UnsupportedShadersPass
{
	[Conditional("UNITY_EDITOR")]
	public static void Record(RenderGraph renderGraph, ContextContainer frameData)
	{
	}
}
