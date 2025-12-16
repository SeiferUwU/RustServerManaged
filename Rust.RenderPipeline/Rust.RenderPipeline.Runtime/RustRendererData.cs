using System.Collections.Generic;
using UnityEngine;

namespace Rust.RenderPipeline.Runtime;

[CreateAssetMenu(menuName = "Rendering/Rust Render Pipeline Renderer")]
public class RustRendererData : ScriptableObject
{
	[SerializeField]
	internal List<RustRendererFeature> rendererFeatures = new List<RustRendererFeature>(10);

	public bool TryGetRendererFeature<T>(out T rendererFeature) where T : RustRendererFeature
	{
		foreach (RustRendererFeature rendererFeature2 in rendererFeatures)
		{
			if (rendererFeature2.GetType() == typeof(T))
			{
				rendererFeature = rendererFeature2 as T;
				return true;
			}
		}
		rendererFeature = null;
		return false;
	}
}
