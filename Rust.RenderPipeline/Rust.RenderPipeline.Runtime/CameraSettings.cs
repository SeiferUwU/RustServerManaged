using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Rust.RenderPipeline.Runtime;

[Serializable]
public class CameraSettings
{
	public enum RenderScaleMode
	{
		Inherit,
		Multiply,
		Override
	}

	[Serializable]
	public struct FinalBlendMode
	{
		public BlendMode source;

		public BlendMode destination;
	}

	public RustRendererData rustRendererData;

	public bool copyColor = true;

	public bool copyDepth = true;

	[RenderingLayerMaskField]
	public int renderingLayerMask = -1;

	public bool maskLights;

	public RenderScaleMode renderScaleMode;

	[Range(0.1f, 2f)]
	public float renderScale = 1f;

	public bool postProcessingEnabled = true;

	public bool overridePostFX;

	public PostFXSettings postFXSettings;

	public FinalBlendMode finalBlendMode = new FinalBlendMode
	{
		source = BlendMode.One,
		destination = BlendMode.Zero
	};

	public bool allowFXAA;

	[Tooltip("Alpha is used to store Luma by default, which makes FXAA look better. Ticking this maintains transparency in the alpha channel after color grading")]
	public bool keepAlpha;

	public float GetRenderScale(float scale)
	{
		return renderScaleMode switch
		{
			RenderScaleMode.Inherit => scale, 
			RenderScaleMode.Override => renderScale, 
			RenderScaleMode.Multiply => scale * renderScale, 
			_ => scale, 
		};
	}
}
