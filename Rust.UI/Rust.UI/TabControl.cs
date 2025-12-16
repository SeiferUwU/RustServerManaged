using UnityEngine;
using UnityEngine.UI;

namespace Rust.UI;

public class TabControl : RustControl
{
	public RectTransform TabContainer;

	public RectTransform PanelContainer;

	public RustButton ButtonControl;

	public float InnerPadding;

	public bool AllowCloseAll;

	public Image Background;

	public Image Foreground;

	public RustLayout Panel(string name)
	{
		return PanelContainer.Find(name)?.GetComponent<RustLayout>();
	}

	public RustButton Button(string name)
	{
		return TabContainer.Find(name)?.GetComponent<RustButton>();
	}

	public void AddTab(string name, Translate.Phrase buttontext = null, Icons icon = Icons.ExclamationSquare)
	{
		bool flag = PanelContainer.childCount == 0;
		GameObject obj = Object.Instantiate(ButtonControl.gameObject);
		obj.name = name;
		RustButton component = obj.GetComponent<RustButton>();
		component.SetParent(TabContainer);
		component.Text.AutoSizeParent = true;
		component.Text.AutoSetWidth = true;
		component.IsToggle = true;
		component.UnpressSiblings = true;
		component.PreventToggleOff = !AllowCloseAll;
		component.TabPanelTarget = PanelContainer;
		component.Icon.Icon = icon;
		if (buttontext == null)
		{
			component.Text.SetText(name);
			component.Text.DoAutoSize();
		}
		else
		{
			component.Text.SetPhrase(buttontext);
		}
		if (flag && !AllowCloseAll)
		{
			component.Toggle(value: true);
		}
		RustLayout rustLayout = Make.Container.Vertical();
		rustLayout.name = name;
		rustLayout.SetParent(PanelContainer);
		(rustLayout.transform as RectTransform).Fill(InnerPadding, InnerPadding, InnerPadding, InnerPadding);
		rustLayout.gameObject.SetActive(flag && !AllowCloseAll);
		LayoutRebuilder.MarkLayoutForRebuild(TabContainer);
	}

	protected override void ApplyStyle(StyleColorSet style)
	{
		base.ApplyStyle(style);
		if ((bool)Background)
		{
			Background.color = style.Bg;
		}
		if ((bool)Foreground)
		{
			Foreground.color = style.Fg;
		}
	}
}
