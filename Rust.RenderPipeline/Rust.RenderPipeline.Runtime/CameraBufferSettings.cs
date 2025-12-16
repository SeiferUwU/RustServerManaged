using System;
using UnityEngine;

namespace Rust.RenderPipeline.Runtime;

[Serializable]
public struct CameraBufferSettings
{
	[Serializable]
	public struct FxaaSettings
	{
		public enum Quality
		{
			Low,
			Medium,
			High
		}

		public bool enabled;

		public Quality quality;

		[Range(0.0312f, 0.0833f)]
		public float fixedThreshold;

		[Range(0.063f, 0.333f)]
		public float relativeThreshold;

		[Range(0f, 1f)]
		public float subpixelBlending;
	}

	public enum BicubicRescalingMode
	{
		Off,
		UpOnly,
		UpAndDown
	}

	public bool allowHDR;

	public bool copyColor;

	public bool copyColorReflection;

	public bool copyDepth;

	public bool copyDepthReflection;

	[Range(0.1f, 2f)]
	public float renderScale;

	public BicubicRescalingMode bicubicRescaling;

	public FxaaSettings fxaaSettings;
}
