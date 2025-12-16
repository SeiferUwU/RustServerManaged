using System;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Rust.UI;

public class RustInput : RustControl
{
	[Serializable]
	public class ChangedEvent : UnityEvent<string>
	{
	}

	public TMP_InputField InputField;

	public Image Background;

	public ChangedEvent OnSelect;

	public ChangedEvent OnValueChanged;

	public ChangedEvent OnEndEdit;

	public ChangedEvent OnSubmit;

	public RustText Placeholder => InputField.placeholder as RustText;

	public bool IsFocused => InputField.isFocused;

	public string Value => Text;

	public string Text
	{
		get
		{
			return InputField.text;
		}
		set
		{
			InputField.text = value;
		}
	}

	public override StyleColorSet CurrentStyleCollection
	{
		get
		{
			StyleColorSet currentStyleCollection = base.CurrentStyleCollection;
			if (IsFocused)
			{
				currentStyleCollection.Apply(Styles.Pressed);
			}
			if (IsDisabled)
			{
				currentStyleCollection.Apply(Styles.Disabled);
			}
			return currentStyleCollection;
		}
	}

	protected override void Awake()
	{
		InputField.onSelect.AddListener(delegate(string x)
		{
			OnSelect.Invoke(x);
		});
		InputField.onValueChanged.AddListener(delegate(string x)
		{
			OnValueChanged.Invoke(x);
		});
		InputField.onEndEdit.AddListener(delegate(string x)
		{
			OnEndEdit.Invoke(x);
		});
		InputField.onSubmit.AddListener(delegate(string x)
		{
			OnSubmit.Invoke(x);
		});
		InputField.onSelect.AddListener(delegate
		{
			ApplyStyles();
		});
		InputField.onDeselect.AddListener(delegate
		{
			ApplyStyles();
		});
		base.Awake();
	}

	protected override void ApplyStyle(StyleColorSet style)
	{
		base.ApplyStyle(style);
		if ((bool)Background)
		{
			Background.color = style.Bg;
		}
		if ((bool)InputField.textComponent)
		{
			InputField.textComponent.color = style.Fg;
		}
		if ((bool)InputField.placeholder)
		{
			(InputField.placeholder as RustText).color = style.Icon;
		}
	}
}
