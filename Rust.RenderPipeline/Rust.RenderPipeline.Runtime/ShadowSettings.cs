using System;
using UnityEngine;

namespace Rust.RenderPipeline.Runtime;

[Serializable]
public class ShadowSettings
{
	public enum FilterQuality
	{
		Low,
		Medium,
		High
	}

	public enum MapSize
	{
		_256 = 0x100,
		_512 = 0x200,
		_1024 = 0x400,
		_2048 = 0x800,
		_4096 = 0x1000,
		_8192 = 0x2000
	}

	[Serializable]
	public struct DirectionalLight
	{
		public MapSize atlasSize;

		[Range(1f, 4f)]
		public int cascadeCount;

		[Range(0f, 1f)]
		public float cascadeRatio1;

		[Range(0f, 1f)]
		public float cascadeRatio2;

		[Range(0f, 1f)]
		public float cascadeRatio3;

		[Range(0.001f, 1f)]
		public float cascadeFade;

		public bool softCascadeBlend;

		public Vector3 CascadeRatios => new Vector3(cascadeRatio1, cascadeRatio2, cascadeRatio3);
	}

	[Serializable]
	public struct AdditionalLight
	{
		public MapSize atlasSize;

		public FilterMode filter;
	}

	[Min(0.001f)]
	public float maxDistance = 100f;

	[Range(0.001f, 1f)]
	public float distanceFade = 0.1f;

	public DirectionalLight directional = new DirectionalLight
	{
		atlasSize = MapSize._1024,
		cascadeCount = 4,
		cascadeRatio1 = 0.1f,
		cascadeRatio2 = 0.25f,
		cascadeRatio3 = 0.5f,
		cascadeFade = 0.1f
	};

	public AdditionalLight additional = new AdditionalLight
	{
		atlasSize = MapSize._1024
	};

	public FilterQuality filterQuality = FilterQuality.Medium;

	public float DirectionalFilterSize => (float)filterQuality + 2f;

	public float AdditionalFilterSize => (float)filterQuality + 2f;
}
