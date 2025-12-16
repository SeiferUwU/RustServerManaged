using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Facepunch.Flexbox;

public class FlexTransition : FacepunchBehaviour
{
	public enum TransitionProperty
	{
		PaddingLeft = 0,
		PaddingRight = 1,
		PaddingTop = 2,
		PaddingBottom = 3,
		Gap = 4,
		MinWidth = 5,
		MinHeight = 6,
		MaxWidth = 7,
		MaxHeight = 8,
		ScaleX = 100,
		ScaleY = 101,
		ImageColor = 102,
		TextColor = 103,
		CanvasAlpha = 104,
		RotationZ = 105,
		ScaleXY = 106,
		TransformTranslateX = 200,
		TransformTranslateY = 201,
		TransformScaleX = 202,
		TransformScaleY = 203,
		TransformRotate = 204,
		TranslateX = 205,
		TranslateY = 206
	}

	[Serializable]
	public struct Definition
	{
		public TransitionProperty Property;

		public UnityEngine.Object Object;

		public float FromFloat;

		public float ToFloat;

		public Color FromColor;

		public Color ToColor;

		[Min(0f)]
		public float Duration;

		public LeanTweenType Ease;

		public AnimationCurve Curve;
	}

	public Definition[] Transitions;

	[SerializeField]
	private bool _playOnAwake;

	private readonly List<int> _pendingIds = new List<int>();

	private bool _currentState;

	private bool _hasSwitchedState;

	public void Awake()
	{
		if (!_hasSwitchedState)
		{
			SwitchState(enabled: false, animate: false);
		}
	}

	public void Start()
	{
		if (_playOnAwake)
		{
			PlayOneOff();
		}
	}

	public void SwitchState(bool enabled, bool animate)
	{
		_currentState = enabled;
		_hasSwitchedState = true;
		if (Transitions == null || Transitions.Length == 0)
		{
			return;
		}
		foreach (int pendingId in _pendingIds)
		{
			LeanTween.cancel(pendingId);
		}
		_pendingIds.Clear();
		for (int i = 0; i < Transitions.Length; i++)
		{
			LTDescr lTDescr = RunTransitionImpl(in Transitions[i], animate);
			if (lTDescr != null)
			{
				_pendingIds.Add(lTDescr.uniqueId);
			}
		}
	}

	public void SwitchState(bool enabled)
	{
		SwitchState(enabled, animate: true);
	}

	public void ToggleState()
	{
		SwitchState(!_currentState);
	}

	public void PlayOneOff()
	{
		_currentState = true;
		_hasSwitchedState = true;
		if (Transitions == null || Transitions.Length == 0)
		{
			return;
		}
		foreach (int pendingId in _pendingIds)
		{
			LeanTween.cancel(pendingId);
		}
		_pendingIds.Clear();
		for (int i = 0; i < Transitions.Length; i++)
		{
			LTDescr lTDescr = RunTransitionImpl(in Transitions[i], animate: true);
			if (lTDescr != null)
			{
				_pendingIds.Add(lTDescr.uniqueId);
			}
		}
	}

	public void PlayPop()
	{
		if (Transitions == null || Transitions.Length == 0)
		{
			return;
		}
		_hasSwitchedState = true;
		_currentState = true;
		foreach (int pendingId in _pendingIds)
		{
			LeanTween.cancel(pendingId);
		}
		_pendingIds.Clear();
		float num = 0f;
		for (int i = 0; i < Transitions.Length; i++)
		{
			LTDescr lTDescr = RunTransitionImpl(in Transitions[i], animate: true);
			if (lTDescr != null)
			{
				_pendingIds.Add(lTDescr.uniqueId);
				num = Mathf.Max(num, Transitions[i].Duration);
			}
		}
		Invoke(RestoreState, num);
	}

	private void RestoreState()
	{
		SwitchState(enabled: false, animate: true);
	}

	private LTDescr RunTransitionImpl(in Definition transition, bool animate)
	{
		LTDescr lTDescr = null;
		switch (transition.Property)
		{
		case TransitionProperty.ScaleX:
		{
			FlexElement flexElement = transition.Object as FlexElement;
			if (flexElement == null)
			{
				break;
			}
			float num3 = (_currentState ? transition.ToFloat : transition.FromFloat);
			if (animate)
			{
				lTDescr = LeanTween.scaleX(flexElement.gameObject, num3, transition.Duration).setOnUpdate(delegate(float value, object obj)
				{
					if (obj is FlexElement flexElement4)
					{
						flexElement4.SetLayoutDirty();
					}
				}, flexElement);
			}
			else
			{
				Vector3 localScale = flexElement.transform.localScale;
				localScale.x = num3;
				flexElement.transform.localScale = localScale;
				flexElement.SetLayoutDirty();
			}
			break;
		}
		case TransitionProperty.ScaleY:
		{
			FlexElement flexElement3 = transition.Object as FlexElement;
			if (flexElement3 == null)
			{
				break;
			}
			float num13 = (_currentState ? transition.ToFloat : transition.FromFloat);
			if (animate)
			{
				lTDescr = LeanTween.scaleY(flexElement3.gameObject, num13, transition.Duration).setOnUpdate(delegate(float value, object obj)
				{
					FlexElement flexElement4 = (FlexElement)obj;
					if (flexElement4 != null)
					{
						flexElement4.SetLayoutDirty();
					}
				}, flexElement3);
			}
			else
			{
				Vector3 localScale3 = flexElement3.transform.localScale;
				localScale3.y = num13;
				flexElement3.transform.localScale = localScale3;
				flexElement3.SetLayoutDirty();
			}
			break;
		}
		case TransitionProperty.ScaleXY:
		{
			FlexElement flexElement2 = transition.Object as FlexElement;
			if (flexElement2 == null)
			{
				break;
			}
			float num8 = (_currentState ? transition.ToFloat : transition.FromFloat);
			if (animate)
			{
				lTDescr = LeanTween.scale(flexElement2.gameObject, new Vector3(num8, num8, flexElement2.transform.localScale.z), transition.Duration).setOnUpdate(delegate(Vector3 value, object obj)
				{
					if (obj is FlexElement flexElement4)
					{
						flexElement4.SetLayoutDirty();
					}
				}, flexElement2);
			}
			else
			{
				Vector3 localScale2 = flexElement2.transform.localScale;
				localScale2.x = num8;
				localScale2.y = num8;
				flexElement2.transform.localScale = localScale2;
				flexElement2.SetLayoutDirty();
			}
			break;
		}
		case TransitionProperty.ImageColor:
		{
			Image image = transition.Object as Image;
			if (image == null)
			{
				break;
			}
			Color color3 = image.color;
			Color color4 = (_currentState ? transition.ToColor : transition.FromColor);
			if (animate)
			{
				lTDescr = LeanTween.value(image.gameObject, color3, color4, transition.Duration).setOnUpdateParam(image).setOnUpdateColor(delegate(Color value, object obj)
				{
					if (obj is Image image2)
					{
						image2.color = value;
					}
				});
			}
			else
			{
				image.color = color4;
			}
			break;
		}
		case TransitionProperty.TextColor:
		{
			TMP_Text tMP_Text = transition.Object as TMP_Text;
			if (tMP_Text == null)
			{
				break;
			}
			Color color = tMP_Text.color;
			Color color2 = (_currentState ? transition.ToColor : transition.FromColor);
			if (animate)
			{
				lTDescr = LeanTween.value(tMP_Text.gameObject, color, color2, transition.Duration).setOnUpdateParam(tMP_Text).setOnUpdateColor(delegate(Color value, object state)
				{
					if (state is TMP_Text tMP_Text2)
					{
						tMP_Text2.color = value;
					}
				});
			}
			else
			{
				tMP_Text.color = color2;
			}
			break;
		}
		case TransitionProperty.CanvasAlpha:
		{
			CanvasGroup canvasGroup = transition.Object as CanvasGroup;
			if (!(canvasGroup == null))
			{
				float num10 = (_currentState ? transition.ToFloat : transition.FromFloat);
				if (animate)
				{
					lTDescr = LeanTween.alphaCanvas(canvasGroup, num10, transition.Duration).setEase(transition.Ease);
				}
				else
				{
					canvasGroup.alpha = num10;
				}
			}
			break;
		}
		case TransitionProperty.RotationZ:
		{
			Transform transform = transition.Object as Transform;
			if (!(transform == null))
			{
				float num2 = (_currentState ? transition.ToFloat : transition.FromFloat);
				if (animate)
				{
					lTDescr = LeanTween.rotateZ(transform.gameObject, num2, transition.Duration);
					break;
				}
				Vector3 eulerAngles = transform.eulerAngles;
				eulerAngles.z = num2;
				transform.localEulerAngles = eulerAngles;
			}
			break;
		}
		case TransitionProperty.TransformTranslateX:
		{
			FlexGraphicTransform flexGraphicTransform = transition.Object as FlexGraphicTransform;
			if (flexGraphicTransform == null)
			{
				break;
			}
			float translateX = flexGraphicTransform.TranslateX;
			float num4 = (_currentState ? transition.ToFloat : transition.FromFloat);
			if (animate)
			{
				lTDescr = LeanTween.value(flexGraphicTransform.gameObject, translateX, num4, transition.Duration).setOnUpdateParam(flexGraphicTransform).setOnUpdateObject(delegate(float value, object state)
				{
					if (state is FlexGraphicTransform flexGraphicTransform6)
					{
						flexGraphicTransform6.TranslateX = value;
						flexGraphicTransform6.SetVerticesDirty();
					}
				});
			}
			else
			{
				flexGraphicTransform.TranslateX = num4;
				flexGraphicTransform.SetVerticesDirty();
			}
			break;
		}
		case TransitionProperty.TransformTranslateY:
		{
			FlexGraphicTransform flexGraphicTransform5 = transition.Object as FlexGraphicTransform;
			if (flexGraphicTransform5 == null)
			{
				break;
			}
			float translateY = flexGraphicTransform5.TranslateY;
			float num12 = (_currentState ? transition.ToFloat : transition.FromFloat);
			if (animate)
			{
				lTDescr = LeanTween.value(flexGraphicTransform5.gameObject, translateY, num12, transition.Duration).setOnUpdateParam(flexGraphicTransform5).setOnUpdateObject(delegate(float value, object state)
				{
					if (state is FlexGraphicTransform flexGraphicTransform6)
					{
						flexGraphicTransform6.TranslateY = value;
						flexGraphicTransform6.SetVerticesDirty();
					}
				});
			}
			else
			{
				flexGraphicTransform5.TranslateY = num12;
				flexGraphicTransform5.SetVerticesDirty();
			}
			break;
		}
		case TransitionProperty.TranslateY:
		{
			Transform transform2 = transition.Object as Transform;
			if (transform2 == null)
			{
				break;
			}
			float y = transform2.localPosition.y;
			float num6 = (_currentState ? transition.ToFloat : transition.FromFloat);
			if (animate)
			{
				lTDescr = LeanTween.value(transform2.gameObject, y, num6, transition.Duration).setEase(transition.Ease).setOnUpdateParam(transform2)
					.setOnUpdateObject(delegate(float value, object state)
					{
						if (state is Transform { localPosition: var localPosition3 } transform4)
						{
							localPosition3.y = value;
							transform4.localPosition = localPosition3;
						}
					});
			}
			else
			{
				Vector3 localPosition = transform2.localPosition;
				localPosition.y = num6;
				transform2.localPosition = localPosition;
			}
			break;
		}
		case TransitionProperty.TranslateX:
		{
			Transform transform3 = transition.Object as Transform;
			if (transform3 == null)
			{
				break;
			}
			float x = transform3.localPosition.x;
			float num9 = (_currentState ? transition.ToFloat : transition.FromFloat);
			if (animate)
			{
				lTDescr = LeanTween.value(transform3.gameObject, x, num9, transition.Duration).setEase(transition.Ease).setOnUpdateParam(transform3)
					.setOnUpdateObject(delegate(float value, object state)
					{
						if (state is Transform { localPosition: var localPosition3 } transform4)
						{
							localPosition3.x = value;
							transform4.localPosition = localPosition3;
						}
					});
			}
			else
			{
				Vector3 localPosition2 = transform3.localPosition;
				localPosition2.x = num9;
				transform3.localPosition = localPosition2;
			}
			break;
		}
		case TransitionProperty.TransformScaleX:
		{
			FlexGraphicTransform flexGraphicTransform4 = transition.Object as FlexGraphicTransform;
			if (flexGraphicTransform4 == null)
			{
				break;
			}
			float scaleX = flexGraphicTransform4.ScaleX;
			float num11 = (_currentState ? transition.ToFloat : transition.FromFloat);
			if (animate)
			{
				lTDescr = LeanTween.value(flexGraphicTransform4.gameObject, scaleX, num11, transition.Duration).setOnUpdateParam(flexGraphicTransform4).setOnUpdateObject(delegate(float value, object state)
				{
					if (state is FlexGraphicTransform flexGraphicTransform6)
					{
						flexGraphicTransform6.ScaleX = value;
						flexGraphicTransform6.SetVerticesDirty();
					}
				});
			}
			else
			{
				flexGraphicTransform4.ScaleX = num11;
				flexGraphicTransform4.SetVerticesDirty();
			}
			break;
		}
		case TransitionProperty.TransformScaleY:
		{
			FlexGraphicTransform flexGraphicTransform2 = transition.Object as FlexGraphicTransform;
			if (flexGraphicTransform2 == null)
			{
				break;
			}
			float scaleY = flexGraphicTransform2.ScaleY;
			float num5 = (_currentState ? transition.ToFloat : transition.FromFloat);
			if (animate)
			{
				lTDescr = LeanTween.value(flexGraphicTransform2.gameObject, scaleY, num5, transition.Duration).setOnUpdateParam(flexGraphicTransform2).setOnUpdateObject(delegate(float value, object state)
				{
					if (state is FlexGraphicTransform flexGraphicTransform6)
					{
						flexGraphicTransform6.ScaleY = value;
						flexGraphicTransform6.SetVerticesDirty();
					}
				});
			}
			else
			{
				flexGraphicTransform2.ScaleY = num5;
				flexGraphicTransform2.SetVerticesDirty();
			}
			break;
		}
		case TransitionProperty.TransformRotate:
		{
			FlexGraphicTransform flexGraphicTransform3 = transition.Object as FlexGraphicTransform;
			if (flexGraphicTransform3 == null)
			{
				break;
			}
			float rotate = flexGraphicTransform3.Rotate;
			float num7 = (_currentState ? transition.ToFloat : transition.FromFloat);
			if (animate)
			{
				lTDescr = LeanTween.value(flexGraphicTransform3.gameObject, rotate, num7, transition.Duration).setOnUpdateParam(flexGraphicTransform3).setOnUpdateObject(delegate(float value, object state)
				{
					if (state is FlexGraphicTransform flexGraphicTransform6)
					{
						flexGraphicTransform6.Rotate = value;
						flexGraphicTransform6.SetVerticesDirty();
					}
				});
			}
			else
			{
				flexGraphicTransform3.Rotate = num7;
				flexGraphicTransform3.SetVerticesDirty();
			}
			break;
		}
		default:
		{
			FlexElement element = transition.Object as FlexElement;
			if (element == null)
			{
				break;
			}
			TransitionProperty property = transition.Property;
			float num = (_currentState ? transition.ToFloat : transition.FromFloat);
			if (animate)
			{
				lTDescr = LeanTween.value(element.gameObject, Property(element, property), num, transition.Duration).setOnUpdate(delegate(float newValue, object _)
				{
					if (element != null)
					{
						Property(element, property) = newValue;
						element.SetLayoutDirty();
					}
				}, this);
			}
			else
			{
				Property(element, property) = num;
				element.SetLayoutDirty();
			}
			break;
		}
		}
		if (lTDescr != null)
		{
			if (transition.Ease == LeanTweenType.animationCurve)
			{
				lTDescr.setEase(transition.Curve);
			}
			else
			{
				lTDescr.setEase(transition.Ease);
			}
		}
		return lTDescr;
	}

	private static ref float Property(FlexElement element, TransitionProperty property)
	{
		return property switch
		{
			TransitionProperty.PaddingLeft => ref element.Padding.left, 
			TransitionProperty.PaddingRight => ref element.Padding.right, 
			TransitionProperty.PaddingTop => ref element.Padding.top, 
			TransitionProperty.PaddingBottom => ref element.Padding.bottom, 
			TransitionProperty.Gap => ref element.Gap, 
			TransitionProperty.MinWidth => ref element.MinWidth.Value, 
			TransitionProperty.MinHeight => ref element.MinHeight.Value, 
			TransitionProperty.MaxWidth => ref element.MaxWidth.Value, 
			TransitionProperty.MaxHeight => ref element.MaxHeight.Value, 
			_ => throw new NotSupportedException(string.Format("{0} {1}", "TransitionProperty", property)), 
		};
	}

	public float GetTransitionTime()
	{
		float num = 0f;
		Definition[] transitions = Transitions;
		for (int i = 0; i < transitions.Length; i++)
		{
			Definition definition = transitions[i];
			if (definition.Duration > num)
			{
				num = definition.Duration;
			}
		}
		return num;
	}
}
