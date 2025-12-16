using System.Collections.Generic;
using Coffee.UIEffects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Rust.UI;

[AddComponentMenu("Rust/UI/Button")]
public class RustButton : RustControl, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerClickHandler, ILayoutElement
{
	public Image Background;

	public RustText Text;

	public RustText SubText;

	public RustIcon Icon;

	public Image IconImage;

	public UnityEvent OnPressed;

	public UnityEvent OnReleased;

	public UnityEvent OnHovered;

	public UnityEvent OnHoverEnd;

	public UnityEvent OnToggleEnabled;

	public UnityEvent OnToggleDisabled;

	public UnityEvent<bool> OnToggleChanged;

	public bool IsToggle;

	public bool UnpressSiblings;

	public bool PreventToggleOff;

	public Transform TabPanelTarget;

	public Vector4 TextMargin = new Vector4(0f, 0f, 0f, 0f);

	public Vector4 TextMarginNoIcon = new Vector4(0f, 0f, 0f, 0f);

	private UIGradient _gradient;

	private readonly List<int> _pendingTweenIds = new List<int>();

	private UIGradient gradient
	{
		get
		{
			if (_gradient == null)
			{
				_gradient = Background.gameObject.GetComponent<UIGradient>();
			}
			return _gradient;
		}
		set
		{
			_gradient = value;
		}
	}

	public bool Value
	{
		get
		{
			return IsPressed;
		}
		set
		{
			if (value != Value)
			{
				if (value)
				{
					CurrentState |= State.Pressed;
				}
				else
				{
					CurrentState &= ~State.Pressed;
				}
				ApplyStyles();
			}
		}
	}

	public bool AutoSize
	{
		get
		{
			return Text.AutoSizeParent;
		}
		set
		{
			Text.AutoSizeParent = value;
		}
	}

	public float minWidth
	{
		get
		{
			if (!Text)
			{
				return 0f;
			}
			return Text.rectTransform.rect.width;
		}
	}

	public float preferredWidth
	{
		get
		{
			if (!Text)
			{
				return 0f;
			}
			return Text.rectTransform.rect.width;
		}
	}

	public float flexibleWidth
	{
		get
		{
			if (!Text)
			{
				return 0f;
			}
			return Text.rectTransform.rect.width;
		}
	}

	public float minHeight
	{
		get
		{
			if (!Text)
			{
				return 0f;
			}
			return Text.minHeight;
		}
	}

	public float preferredHeight
	{
		get
		{
			if (!Text)
			{
				return 0f;
			}
			return Text.preferredHeight;
		}
	}

	public float flexibleHeight
	{
		get
		{
			if (!Text)
			{
				return 0f;
			}
			return Text.flexibleHeight;
		}
	}

	public int layoutPriority
	{
		get
		{
			if (!Text)
			{
				return 0;
			}
			return Text.layoutPriority;
		}
	}

	public void Press()
	{
		if (IsDisabled)
		{
			return;
		}
		if (IsToggle)
		{
			if (!PreventToggleOff || !Value)
			{
				Toggle(!Value);
			}
		}
		else
		{
			Toggle(value: true, forced: true);
		}
	}

	public void UnPress()
	{
		if (IsDisabled)
		{
			return;
		}
		if (IsToggle)
		{
			if (PreventToggleOff && Value)
			{
				return;
			}
			Toggle(!Value);
		}
		else
		{
			Toggle(value: false, forced: true);
		}
		ApplyStyles();
	}

	public virtual void OnPointerDown(PointerEventData eventData)
	{
		if (!IsToggle)
		{
			Press();
		}
	}

	public virtual void OnPointerUp(PointerEventData eventData)
	{
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			if (IsToggle)
			{
				Press();
			}
			else
			{
				UnPress();
			}
		}
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
		base.OnPointerEnter(eventData);
		OnHovered?.Invoke();
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
		base.OnPointerExit(eventData);
		OnHoverEnd?.Invoke();
		if (!IsToggle)
		{
			CurrentState &= ~State.Pressed;
			ApplyStyles();
		}
	}

	public void SetToggleTrue(bool fireEvents = true)
	{
		Toggle(value: true, forced: false, fireEvents);
	}

	public void SetToggleTrueForced(bool fireEvents = true)
	{
		Toggle(value: true, forced: true, fireEvents);
	}

	public void SetToggleFalse(bool fireEvents = true)
	{
		Toggle(value: false, forced: false, fireEvents);
	}

	public void SetToggleVisualOff()
	{
		if (Value)
		{
			CurrentState &= ~State.Pressed;
			ApplyStyles();
			Value = false;
		}
	}

	public void SetToggleVisualOn()
	{
		if (!Value)
		{
			CurrentState &= State.Pressed;
			ApplyStyles();
			Value = true;
		}
	}

	public void Toggle(bool value, bool forced = false, bool fireEvents = true)
	{
		if (value)
		{
			if (UnpressSiblings)
			{
				DoUnpressSiblings();
			}
			if (Value && !forced)
			{
				return;
			}
			CurrentState |= State.Pressed;
			if (fireEvents)
			{
				OnPressed.Invoke();
				OnToggleEnabled.Invoke();
				OnToggleChanged.Invoke(arg0: true);
			}
			ToggleTabPanel(onOff: true);
		}
		else
		{
			if (!Value && !forced)
			{
				return;
			}
			CurrentState &= ~State.Pressed;
			if (fireEvents)
			{
				OnReleased.Invoke();
				OnToggleDisabled.Invoke();
				OnToggleChanged.Invoke(arg0: false);
			}
			ToggleTabPanel(onOff: false);
		}
		ApplyStyles();
	}

	private void ToggleTabPanel(bool onOff)
	{
		if (!(TabPanelTarget == null))
		{
			Transform transform = TabPanelTarget.Find(base.gameObject.name);
			if (!(transform == null))
			{
				transform.gameObject.SetActive(onOff);
			}
		}
	}

	public void DoUnpressSiblings()
	{
		foreach (Transform item in base.transform.parent)
		{
			if (!(item == base.transform))
			{
				RustButton component = item.GetComponent<RustButton>();
				if (!(component == null))
				{
					component.Toggle(value: false);
				}
			}
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (!IsToggle && Value)
		{
			CurrentState &= ~State.Pressed;
			ApplyStyles();
		}
		if (!IsToggle && CurrentState.HasFlag(State.Hovered))
		{
			CurrentState &= ~State.Hovered;
			ApplyStyles();
		}
	}

	protected override void ApplyStyle(StyleColorSet style)
	{
		if (_styles.ShouldTween)
		{
			foreach (int pendingTweenId in _pendingTweenIds)
			{
				LeanTween.cancel(pendingTweenId);
			}
			_pendingTweenIds.Clear();
		}
		if (Background != null)
		{
			if (style.UseGradient)
			{
				if (gradient == null)
				{
					gradient = Background.gameObject.AddComponent<UIGradient>();
				}
				gradient.enabled = true;
				gradient.direction = style.GradientDirection;
				SetGradientColors(gradient, gradient.color1, style.BgStart, gradient.color2, style.BgEnd, gradient.offset, style.BgOffset);
				gradient.offset = style.BgOffset;
				Background.color = Color.white;
			}
			else
			{
				UIGradient uIGradient = gradient;
				Color color = (((object)uIGradient != null && uIGradient.enabled) ? gradient.color1 : Background.color);
				if (gradient != null)
				{
					gradient.enabled = false;
				}
				SetColor(Background, color, style.Bg);
			}
		}
		if (Text != null)
		{
			SetColor(Text, Text.color, style.Fg);
		}
		if (SubText != null)
		{
			SetColor(SubText, SubText.color, Color.Lerp(style.Bg, style.Fg, 0.5f));
		}
		if (Icon != null)
		{
			SetColor(Icon, Icon.color, style.Icon);
		}
		if (IconImage != null)
		{
			SetColor(IconImage, IconImage.color, style.Icon);
		}
		if (Text != null)
		{
			Vector4 vector = (((!Icon || Icon.Icon == Icons.None) && !IconImage) ? TextMarginNoIcon : TextMargin);
			if (Text.margin != vector)
			{
				Text.margin = vector;
				Text.DoAutoSize();
			}
			if (SubText != null && SubText.margin != vector)
			{
				SubText.margin = vector;
				SubText.DoAutoSize();
			}
		}
		void SetColor(Graphic graphic, Color from, Color target)
		{
			if (_styles.ShouldTween)
			{
				LTDescr lTDescr = LeanTween.color(graphic.gameObject, target, _styles.Transition.Duration).setFromColor(from).setOnUpdateColor(delegate(Color col)
				{
					graphic.color = col;
				});
				if (_styles.Transition.Ease == LeanTweenType.animationCurve)
				{
					lTDescr.setEase(_styles.Transition.Curve);
				}
				else
				{
					lTDescr.setEase(_styles.Transition.Ease);
				}
				_pendingTweenIds.Add(lTDescr.uniqueId);
			}
			else
			{
				graphic.color = target;
			}
		}
		void SetGradientColors(UIGradient gradient, Color fromStart, Color toStart, Color fromEnd, Color toEnd, float fromOffset, float toOffset)
		{
			if (!(gradient == null))
			{
				if (_styles.ShouldTween)
				{
					LTDescr lTDescr = LeanTween.value(gradient.gameObject, 0f, 1f, _styles.Transition.Duration).setOnUpdate(delegate(float t)
					{
						gradient.color1 = Color.Lerp(fromStart, toStart, t);
						gradient.color2 = Color.Lerp(fromEnd, toEnd, t);
						gradient.offset = Mathf.Lerp(fromOffset, toOffset, t);
					});
					if (_styles.Transition.Ease == LeanTweenType.animationCurve)
					{
						lTDescr.setEase(_styles.Transition.Curve);
					}
					else
					{
						lTDescr.setEase(_styles.Transition.Ease);
					}
				}
				else
				{
					gradient.color1 = toStart;
					gradient.color2 = toEnd;
					gradient.offset = toOffset;
				}
			}
		}
	}

	public void CalculateLayoutInputHorizontal()
	{
		if (Text != null)
		{
			Text.CalculateLayoutInputHorizontal();
		}
	}

	public void CalculateLayoutInputVertical()
	{
		if (Text != null)
		{
			Text.CalculateLayoutInputVertical();
		}
	}
}
