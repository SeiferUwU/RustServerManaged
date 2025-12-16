using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Rust.UI;

public class RustOption : RustControl
{
	[Serializable]
	public class ChangedEvent : UnityEvent<Option>
	{
	}

	public RustButton Left;

	public RustButton Right;

	public RustText Text;

	public Option[] Options;

	private int CurrentOption;

	public ChangedEvent OnChanged;

	public Option Value => Options[CurrentOption];

	protected override void Awake()
	{
		base.Awake();
		Left.OnPressed.AddListener(OnLeft);
		Right.OnPressed.AddListener(OnRight);
	}

	public void SetOptions(Option[] options)
	{
		Options = options;
		SetOptionDefault();
	}

	public void SetOption(int i)
	{
		i += Options.Length;
		i %= Options.Length;
		CurrentOption = i;
		Text.SetPhrase(Options[i].Label);
	}

	public void SetOptionDefault()
	{
		if (Options == null || Options.Length == 0)
		{
			return;
		}
		for (int i = 0; i < Options.Length; i++)
		{
			if (Options[i].Default)
			{
				SetOption(i);
				return;
			}
		}
		SetOption(0);
	}

	public void OnLeft()
	{
		SetOption(CurrentOption - 1);
		OnChanged?.Invoke(Value);
	}

	public void OnRight()
	{
		SetOption(CurrentOption + 1);
		OnChanged?.Invoke(Value);
	}

	protected override void ApplyStyle(StyleColorSet style)
	{
		if ((bool)Text)
		{
			Text.color = style.Fg;
		}
		Image component = GetComponent<Image>();
		if ((bool)component)
		{
			component.color = style.Bg;
		}
	}
}
