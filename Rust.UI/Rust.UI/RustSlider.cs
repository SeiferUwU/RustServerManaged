using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Rust.UI;

public class RustSlider : RustControl, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IDragHandler, IInitializePotentialDragHandler
{
	[Serializable]
	public class ChangedEvent : UnityEvent<float>
	{
	}

	public RustInput NumberInput;

	public Image SliderCanvas;

	public string DecimalFormat = "0.00";

	public float MinValue;

	public float MaxValue;

	public bool Integer;

	public float ValueInternal;

	public ChangedEvent OnChanged;

	protected float lastCallbackValue;

	public virtual float Value
	{
		get
		{
			return ValueInternal;
		}
		set
		{
			value = Mathf.Clamp(value, MinValue, MaxValue);
			if (Integer)
			{
				value = Mathf.Round(value);
			}
			if (ValueInternal != value)
			{
				ValueInternal = value;
			}
			string valueText = GetValueText(value);
			if (NumberInput != null && !NumberInput.IsFocused && NumberInput.Text != valueText)
			{
				NumberInput.Text = valueText;
			}
			SliderCanvas.fillAmount = ValueNormalized;
			if (lastCallbackValue != value)
			{
				lastCallbackValue = value;
				OnChanged?.Invoke(value);
			}
		}
	}

	public float ValueNormalized
	{
		get
		{
			return Mathf.InverseLerp(MinValue, MaxValue, Value);
		}
		set
		{
			Value = MinValue + (MaxValue - MinValue) * value;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		if (NumberInput != null)
		{
			NumberInput.OnValueChanged.AddListener(TextChanged);
			NumberInput.OnEndEdit.AddListener(TextChanged);
		}
	}

	public string GetValueText(float value)
	{
		if (!Integer)
		{
			return string.Format("{0:" + DecimalFormat + "}", value);
		}
		return $"{(int)value}";
	}

	public void TextChanged(string text)
	{
		if (float.TryParse(text, out var result))
		{
			Value = result;
		}
	}

	public void SliderChanged(float slider)
	{
		Value = slider;
	}

	protected override void ApplyStyle(StyleColorSet style)
	{
		base.ApplyStyle(style);
		SliderCanvas.color = style.Fg;
		Image component = GetComponent<Image>();
		if (component != null)
		{
			component.color = style.Bg;
		}
		if (NumberInput != null && NumberInput.Placeholder != null)
		{
			NumberInput.Placeholder.color = style.Icon;
			NumberInput.InputField.textComponent.color = style.Icon;
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (!IsDisabled)
		{
			UpdateDrag(eventData.position, eventData.pressEventCamera);
			CurrentState |= State.Pressed;
			RustControl.IsDragging = true;
			ApplyStyles();
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (!IsDisabled)
		{
			CurrentState &= ~State.Pressed;
			RustControl.IsDragging = false;
			ApplyStyles();
		}
	}

	public void OnInitializePotentialDrag(PointerEventData eventData)
	{
		eventData.useDragThreshold = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!IsDisabled)
		{
			UpdateDrag(eventData.position, eventData.pressEventCamera);
		}
	}

	private void UpdateDrag(Vector2 pos, Camera cam)
	{
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(SliderCanvas.rectTransform, pos, cam, out var localPoint))
		{
			localPoint -= SliderCanvas.rectTransform.rect.position;
			localPoint.x /= SliderCanvas.rectTransform.rect.width;
			ValueNormalized = localPoint.x;
		}
	}
}
