using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Rust.RenderPipeline.Runtime;

public abstract class RustRendererFeature : ScriptableObject, IDisposable
{
	[SerializeField]
	protected bool active = true;

	public bool IsActive => active;

	public abstract void Create();

	public abstract void AddRenderPasses(RustRenderer renderer);

	private void OnEnable()
	{
		if (RenderPipelineManager.currentPipeline is RustRenderPipeline && IsActive)
		{
			Create();
		}
	}

	private void OnValidate()
	{
		if (RenderPipelineManager.currentPipeline is RustRenderPipeline && IsActive)
		{
			Create();
		}
	}

	public void SetActive(bool active)
	{
		this.active = active;
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
	}
}
