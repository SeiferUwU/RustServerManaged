using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.Scripting.APIUpdating;

namespace Rust.RenderPipeline.Runtime;

[MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
public interface IRenderGraphRecorder
{
	void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData);
}
