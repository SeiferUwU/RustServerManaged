using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Rust.UI;

public class RustToggle : RustControl, IPointerDownHandler, IEventSystemHandler
{
	[Serializable]
	public class ChangedEvent : UnityEvent<bool>
	{
	}

	public StyleAsset StyleOff;

	public Image Handle;

	public RustText TextOn;

	public RustText TextOff;

	public float TextAlphaWhenDisabled;

	public bool Value;

	public float SliderWidth = 0.3f;

	public ChangedEvent OnChanged;

	public UnityEvent OnToggledOn;

	public UnityEvent OnToggledOff;

	private Coroutine anim;

	public override StyleAsset Styles
	{
		get
		{
			if (!Value)
			{
				return StyleOff;
			}
			return base.Styles;
		}
	}

	public void SetValue(bool value)
	{
		if (value != Value)
		{
			Value = value;
			ApplyStyles();
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Value = !Value;
		OnChanged?.Invoke(Value);
		if (Value)
		{
			OnToggledOn?.Invoke();
		}
		else
		{
			OnToggledOff?.Invoke();
		}
		if (anim != null)
		{
			StopCoroutine(anim);
		}
		anim = StartCoroutine(AnimateTo());
	}

	private IEnumerator AnimateTo()
	{
		Vector2 handleMin = new Vector2(Handle.rectTransform.anchorMin.x, (!Value) ? 0f : (1f - SliderWidth));
		Vector2 handleMax = new Vector2(Handle.rectTransform.anchorMax.x, (!Value) ? SliderWidth : 1f);
		float animTime = 0.15f;
		float time = 0f;
		Image bg = GetComponent<Image>();
		for (; time < animTime; time += Time.unscaledDeltaTime)
		{
			StyleColorSet currentStyleCollection = CurrentStyleCollection;
			float num = time / animTime;
			TextOn.color = Color.Lerp(TextOn.color, currentStyleCollection.Fg.WithAlpha(Value ? 1f : TextAlphaWhenDisabled), num);
			TextOff.color = Color.Lerp(TextOff.color, currentStyleCollection.Fg.WithAlpha((!Value) ? 1f : TextAlphaWhenDisabled), num);
			Handle.color = Color.Lerp(Handle.color, currentStyleCollection.Fg.WithAlpha(1f - TextAlphaWhenDisabled), num);
			bg.color = Color.Lerp(bg.color, currentStyleCollection.Bg, num);
			Vector2 anchorMax = Handle.rectTransform.anchorMax;
			anchorMax.x = Mathf.Lerp(handleMax.x, handleMax.y, num * (2f - num));
			Handle.rectTransform.anchorMax = anchorMax;
			Vector2 anchorMin = Handle.rectTransform.anchorMin;
			anchorMin.x = Mathf.Lerp(handleMin.x, handleMin.y, num * (2f - num));
			Handle.rectTransform.anchorMin = anchorMin;
			yield return null;
		}
		anim = null;
		ApplyStyles();
	}

	public override void ApplyStyles()
	{
		if (anim == null && !(Styles == null))
		{
			Vector2 anchorMin = Handle.rectTransform.anchorMin;
			anchorMin.x = ((!Value) ? 0f : (1f - SliderWidth));
			Handle.rectTransform.anchorMin = anchorMin;
			Vector2 anchorMax = Handle.rectTransform.anchorMax;
			anchorMax.x = ((!Value) ? SliderWidth : 1f);
			Handle.rectTransform.anchorMax = anchorMax;
			StyleColorSet currentStyleCollection = CurrentStyleCollection;
			GetComponent<Image>().color = currentStyleCollection.Bg;
			TextOn.rectTransform.anchorMax = TextOn.rectTransform.anchorMax.X(1f - SliderWidth);
			TextOff.rectTransform.anchorMin = TextOn.rectTransform.anchorMin.X(SliderWidth);
			TextOn.color = currentStyleCollection.Fg.WithAlpha(Value ? 1f : TextAlphaWhenDisabled);
			TextOff.color = currentStyleCollection.Fg.WithAlpha((!Value) ? 1f : TextAlphaWhenDisabled);
			Handle.color = currentStyleCollection.Fg.WithAlpha(1f - TextAlphaWhenDisabled);
		}
	}
}
