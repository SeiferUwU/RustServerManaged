using System;
using UnityEngine;

namespace Rust.RenderPipeline.Runtime;

[CreateAssetMenu(menuName = "Rendering/Rust Post FX Settings")]
public class PostFXSettings : ScriptableObject
{
	[Serializable]
	public struct BloomSettings
	{
		public enum Mode
		{
			Additive,
			Scattering
		}

		public bool ignoreRenderScale;

		[Range(0f, 16f)]
		public int maxIterations;

		[Min(1f)]
		public int downscaleLimit;

		public bool bicubicUpsampling;

		[Min(0f)]
		public float threshold;

		[Range(0f, 1f)]
		public float thresholdKnee;

		[Min(0f)]
		public float intensity;

		public bool fadeFireflies;

		public Mode mode;

		[Range(0.05f, 0.95f)]
		public float scatter;
	}

	[Serializable]
	public struct ColorAdjustmentsSettings
	{
		public float postExposure;

		[Range(-100f, 100f)]
		public float contrast;

		[ColorUsage(false, true)]
		public Color colorFilter;

		[Range(-180f, 180f)]
		public float hueShift;

		[Range(-100f, 100f)]
		public float saturation;
	}

	[Serializable]
	public struct ChannelMixerSettings
	{
		public Vector3 red;

		public Vector3 green;

		public Vector3 blue;
	}

	[Serializable]
	public struct WhiteBalanceSettings
	{
		[Range(-100f, 100f)]
		public float temperature;

		[Range(-100f, 100f)]
		public float tint;
	}

	[Serializable]
	public struct SplitToningSettings
	{
		[ColorUsage(false)]
		public Color shadows;

		[ColorUsage(false)]
		public Color highlights;

		[Range(-100f, 100f)]
		public float balance;
	}

	[Serializable]
	public struct ToneMappingSettings
	{
		public enum Mode
		{
			None,
			ACES,
			Neutral,
			Reinhard
		}

		public Mode mode;
	}

	[Serializable]
	public struct ShadowsMidtonesHighlightsSettings
	{
		[ColorUsage(false, true)]
		public Color shadows;

		[ColorUsage(false, true)]
		public Color midtones;

		[ColorUsage(false, true)]
		public Color highlights;

		[Range(0f, 2f)]
		public float shadowsStart;

		[Range(0f, 2f)]
		public float shadowsEnd;

		[Range(0f, 2f)]
		public float highlightsStart;

		[Range(0f, 2f)]
		public float highLightsEnd;
	}

	[SerializeField]
	private Shader shader;

	[NonSerialized]
	private Material material;

	public Material Material
	{
		get
		{
			if (material == null && shader != null)
			{
				material = new Material(shader)
				{
					hideFlags = HideFlags.HideAndDontSave
				};
			}
			return material;
		}
	}

	[field: SerializeField]
	public BloomSettings Bloom { get; private set; } = new BloomSettings
	{
		scatter = 0.7f
	};

	[field: SerializeField]
	public ColorAdjustmentsSettings ColorAdjustments { get; private set; } = new ColorAdjustmentsSettings
	{
		colorFilter = Color.white
	};

	[field: SerializeField]
	public WhiteBalanceSettings WhiteBalance { get; private set; }

	[field: SerializeField]
	public SplitToningSettings SplitToning { get; private set; } = new SplitToningSettings
	{
		shadows = Color.gray,
		highlights = Color.gray
	};

	[field: SerializeField]
	public ChannelMixerSettings ChannelMixer { get; private set; } = new ChannelMixerSettings
	{
		red = Vector3.right,
		green = Vector3.up,
		blue = Vector3.forward
	};

	[field: SerializeField]
	public ShadowsMidtonesHighlightsSettings ShadowsMidtonesHighlights { get; private set; } = new ShadowsMidtonesHighlightsSettings
	{
		shadows = Color.white,
		midtones = Color.white,
		highlights = Color.white,
		shadowsEnd = 0.3f,
		highlightsStart = 0.55f,
		highLightsEnd = 1f
	};

	[field: SerializeField]
	public ToneMappingSettings ToneMapping { get; private set; }

	public static bool AreApplicableTo(Camera camera)
	{
		return camera.cameraType <= CameraType.SceneView;
	}
}
