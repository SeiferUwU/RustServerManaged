using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rust.UI;

public class RustWrapper : RustControl
{
	public RustText Text;

	public RectTransform Canvas;

	protected override void ApplyStyle(StyleColorSet style)
	{
		base.ApplyStyle(style);
		if (Text != null)
		{
			Text.color = style.Fg;
		}
		Image component = GetComponent<Image>();
		if ((bool)component)
		{
			component.color = style.Bg;
		}
	}

	public void Add(UIBehaviour ui)
	{
		ui.transform.SetParent(Canvas.transform, worldPositionStays: false);
		if (ui is RustControl rustControl)
		{
			rustControl.FormField = this;
		}
	}

	public void AddFlex()
	{
		Add(Make.Flex());
	}
}
