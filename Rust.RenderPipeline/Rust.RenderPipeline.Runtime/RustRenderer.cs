using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.Rendering;

namespace Rust.RenderPipeline.Runtime;

public sealed class RustRenderer : IDisposable
{
	private static readonly ProfilingSampler addRenderPassesSampler = new ProfilingSampler("Add Render Passes");

	private readonly List<RustRenderPass> activeRenderPassQueue = new List<RustRenderPass>(32);

	private readonly List<RustRendererFeature> rendererFeatures = new List<RustRendererFeature>(10);

	public ContextContainer FrameData { get; private set; } = new ContextContainer();

	public void Initialize(RustRendererData data)
	{
		rendererFeatures.Clear();
		foreach (RustRendererFeature rendererFeature in data.rendererFeatures)
		{
			if (!(rendererFeature == null))
			{
				rendererFeature.Create();
				rendererFeatures.Add(rendererFeature);
			}
		}
		activeRenderPassQueue.Clear();
	}

	public void EnqueuePass(RustRenderPass rustRenderPass)
	{
		activeRenderPassQueue.Add(rustRenderPass);
	}

	internal void AddRenderPasses()
	{
		foreach (RustRendererFeature rendererFeature in rendererFeatures)
		{
			if (rendererFeature.IsActive)
			{
				rendererFeature.AddRenderPasses(this);
			}
		}
	}

	internal static void SortStable(List<RustRenderPass> list)
	{
		for (int i = 1; i < list.Count; i++)
		{
			RustRenderPass rustRenderPass = list[i];
			int num = i - 1;
			while (num >= 0 && rustRenderPass < list[num])
			{
				list[num + 1] = list[num];
				num--;
			}
			list[num + 1] = rustRenderPass;
		}
	}

	internal void RecordCustomRenderGraphPassesInEventRange(RenderGraph renderGraph, RenderPassEvent eventStart, RenderPassEvent eventEnd)
	{
		if (eventStart == eventEnd)
		{
			return;
		}
		foreach (RustRenderPass item in activeRenderPassQueue)
		{
			if (item.renderPassEvent >= eventStart && item.renderPassEvent < eventEnd)
			{
				item.RecordRenderGraph(renderGraph, FrameData);
			}
		}
	}

	internal void RecordCustomRenderGraphPasses(RenderGraph renderGraph, RenderPassEvent startInjectionPoint, RenderPassEvent endInjectionPoint)
	{
		int renderPassEventRange = RustRenderPass.GetRenderPassEventRange(endInjectionPoint);
		RecordCustomRenderGraphPassesInEventRange(renderGraph, startInjectionPoint, endInjectionPoint + renderPassEventRange);
	}

	internal void RecordCustomRenderGraphPasses(RenderGraph renderGraph, RenderPassEvent injectionPoint)
	{
		RecordCustomRenderGraphPasses(renderGraph, injectionPoint, injectionPoint);
	}

	public void Dispose()
	{
		foreach (RustRendererFeature rendererFeature in rendererFeatures)
		{
			if (!(rendererFeature == null))
			{
				try
				{
					rendererFeature.Dispose();
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
			}
		}
	}
}
