using System;
using System.Collections.Generic;
using Facepunch;
using Facepunch.Extend;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Rust.UI;

public class Dropdown : RustControl, IPointerClickHandler, IEventSystemHandler
{
	[Serializable]
	public class ChangedEvent : UnityEvent<Option>
	{
	}

	public bool useLegacyMenu = true;

	public RustIcon Icon;

	public RustText Text;

	public RustIcon OptionIcon;

	public Option[] Options;

	public RectTransform dropdown;

	public RectTransform optionParent;

	public DropdownOption optionPrefab;

	private readonly List<DropdownOption> dropdownOptions = new List<DropdownOption>();

	private int currentOption;

	public ChangedEvent OnChanged;

	public UnityEvent OnOpened;

	public Option Value => Options[currentOption];

	public void SetOptions(Option[] options)
	{
		Options = options;
		SetOptionDefault();
	}

	protected void SetOptionFromUser(Option option)
	{
		SetOption(option);
		OnChanged?.Invoke(option);
	}

	public void SetOption(int i)
	{
		i += Options.Length;
		i %= Options.Length;
		currentOption = i;
		Text.SetPhrase(Options[i].Label);
		if (OptionIcon != null)
		{
			OptionIcon.Icon = Options[i].Icon;
		}
	}

	public void SetOption(Option option)
	{
		for (int i = 0; i < Options.Length; i++)
		{
			if (Options[i].Value == option.Value)
			{
				SetOption(i);
				break;
			}
		}
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
		SetOption(currentOption - 1);
		OnChanged?.Invoke(Value);
	}

	public void OnRight()
	{
		SetOption(currentOption + 1);
		OnChanged?.Invoke(Value);
	}

	protected override void ApplyStyle(StyleColorSet style)
	{
		if ((bool)Text)
		{
			Text.color = style.Fg;
		}
		if ((bool)Icon)
		{
			Icon.color = style.Icon;
		}
		if ((bool)OptionIcon)
		{
			OptionIcon.color = style.Fg;
		}
		Image component = GetComponent<Image>();
		if ((bool)component)
		{
			component.color = style.Bg;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		OpenMenu();
		OnOpened?.Invoke();
	}

	public void OpenMenu()
	{
		if (Options.Length != 0)
		{
			if (useLegacyMenu)
			{
				Menu menu = Make.Menu(Options, Value);
				menu.OnSelected.AddListener(SetOptionFromUser);
				menu.Popup(base.rectTransform);
			}
			else
			{
				OpenDropdown();
			}
		}
	}

	public void OpenDropdown()
	{
		dropdown.SetActive(active: true);
		RectTransform obj = (RectTransform)base.transform;
		float num = (float)Screen.height / 3f;
		if (obj.position.y <= num)
		{
			dropdown.pivot = new Vector2(0.5f, 0f);
			dropdown.anchoredPosition = dropdown.anchoredPosition.WithY(-6f);
		}
		else
		{
			dropdown.pivot = new Vector2(0.5f, 1f);
			dropdown.anchoredPosition = dropdown.anchoredPosition.WithY(-46f);
		}
		for (int i = 0; i < Options.Length; i++)
		{
			dropdownOptions[i].SetSelected(i == currentOption);
		}
	}

	public void PopulateMenu()
	{
		optionParent.DestroyAllChildren();
		dropdownOptions.Clear();
		for (int i = 0; i < Options.Length; i++)
		{
			DropdownOption dropdownOption = UnityEngine.Object.Instantiate(optionPrefab, optionParent);
			Option option = Options[i];
			DropdownOption component = dropdownOption.GetComponent<DropdownOption>();
			if (component != null && component.Image != null && component.Text != null)
			{
				component.Text.SetPhrase(option.Label);
				component.SetSelected(i == currentOption);
				component.Button.OnReleased.AddListener(delegate
				{
					SetOptionFromUser(option);
				});
			}
			dropdownOption.gameObject.SetActive(value: true);
			dropdownOptions.Add(component);
		}
	}
}
