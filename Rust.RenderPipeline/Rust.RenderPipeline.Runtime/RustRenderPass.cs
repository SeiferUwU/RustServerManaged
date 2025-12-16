using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;

namespace Rust.RenderPipeline.Runtime;

public abstract class RustRenderPass : IRenderGraphRecorder
{
	public RenderPassEvent renderPassEvent { get; set; }

	public virtual void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
	{
		Debug.LogWarning("Rust Render pass (" + ToString() + " is missing an implementation of the RecordRenderGraph method!)");
	}

	public static bool operator <(RustRenderPass lhs, RustRenderPass rhs)
	{
		return lhs.renderPassEvent < rhs.renderPassEvent;
	}

	public static bool operator >(RustRenderPass lhs, RustRenderPass rhs)
	{
		return lhs.renderPassEvent > rhs.renderPassEvent;
	}

	internal static int GetRenderPassEventRange(RenderPassEvent renderPassEvent)
	{
		int num = RenderPassEventsEnumValues.values.Length;
		int num2 = 0;
		for (int i = 0; i < num; i++)
		{
			if (RenderPassEventsEnumValues.values[num2] == (int)renderPassEvent)
			{
				break;
			}
			num2++;
		}
		if (num2 >= num)
		{
			Debug.LogError("GetRenderPassEventRange: invalid renderPassEvent value cannot be found in the RenderPassEvent enumeration");
			return 0;
		}
		if (num2 + 1 >= num)
		{
			return 50;
		}
		return (int)(RenderPassEventsEnumValues.values[num2 + 1] - renderPassEvent);
	}
}
