using Coffee.UIEffects;
using UnityEngine;

namespace Rust.UI;

public struct StyleColorSet
{
	public bool UseGradient;

	public UIGradient.Direction GradientDirection;

	public Color BgStart;

	public Color BgEnd;

	public float BgOffset;

	public Color Bg;

	public Color Fg;

	public Color Icon;

	public StyleColorSet(StyleAsset.Group baseColors)
	{
		UseGradient = baseColors.UseGradient;
		GradientDirection = baseColors.GradientDirection;
		BgStart = baseColors.BgStart;
		BgEnd = baseColors.BgEnd;
		BgOffset = baseColors.BgOffset;
		Bg = baseColors.Bg;
		Fg = baseColors.Fg;
		Icon = baseColors.Icon;
	}

	public void Apply(StyleAsset.OverrideGroup overrideColors)
	{
		if (overrideColors.OverrideBg)
		{
			Bg = overrideColors.Bg;
		}
		if (overrideColors.OverrideFg)
		{
			Fg = overrideColors.Fg;
		}
		if (overrideColors.OverrideIcon)
		{
			Icon = overrideColors.Icon;
		}
		UseGradient = overrideColors.UseGradient;
		GradientDirection = overrideColors.GradientDirection;
		BgStart = overrideColors.BgStart;
		BgEnd = overrideColors.BgEnd;
		BgOffset = overrideColors.BgOffset;
	}
}
