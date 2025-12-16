using System;
using Coffee.UIEffects;
using UnityEngine;

namespace Rust.UI;

[CreateAssetMenu(menuName = "Rust/UI/StyleAsset")]
public class StyleAsset : ScriptableObject
{
	[Serializable]
	public class TweenGroup
	{
		public float Duration;

		public LeanTweenType Ease;

		public AnimationCurve Curve;
	}

	[Serializable]
	public class Group
	{
		public bool UseGradient;

		public UIGradient.Direction GradientDirection = UIGradient.Direction.Vertical;

		public Color BgStart = Color.black;

		public Color BgEnd = Color.black;

		[Range(-1f, 1f)]
		public float BgOffset = 0.5f;

		public Color Bg = Color.black;

		public Color Fg = Color.white;

		public Color Icon = Color.white;
	}

	[Serializable]
	public class OverrideGroup : Group
	{
		public bool OverrideBg = true;

		public bool OverrideFg = true;

		public bool OverrideIcon = true;
	}

	[SerializeField]
	private bool Tween;

	public TweenGroup Transition;

	public Group Normal;

	public OverrideGroup Hovered;

	public OverrideGroup Pressed;

	public OverrideGroup Disabled;

	public bool ShouldTween
	{
		get
		{
			if (Application.isPlaying)
			{
				return Tween;
			}
			return false;
		}
	}
}
