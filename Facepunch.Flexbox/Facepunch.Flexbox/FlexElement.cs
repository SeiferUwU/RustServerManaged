using System;
using System.Collections.Generic;
using UnityEngine;

namespace Facepunch.Flexbox;

[ExecuteAlways]
[RequireComponent(typeof(RectTransform))]
[DisallowMultipleComponent]
public class FlexElement : FlexElementBase
{
	private struct ChildSizingParameters
	{
		public float Size;

		public float MinSize;

		public float MaxSize;

		public bool IsFlexible;

		public float Scale;
	}

	private static readonly List<IFlexNode> SizingChildren = new List<IFlexNode>();

	[Tooltip("The direction to layout children in. This determines which axis is the main axis.")]
	public FlexDirection FlexDirection;

	[Tooltip("Where to start laying out children on the main axis.")]
	public FlexJustify JustifyContent;

	[Tooltip("How to align child flex elements on the cross axis.")]
	public FlexAlign AlignItems = FlexAlign.Stretch;

	[Tooltip("Spacing to add from this elements borders to where children are laid out.")]
	public FlexPadding Padding;

	[Min(0f)]
	[Tooltip("Spacing to add between each child flex item.")]
	public float Gap;

	private ChildSizingParameters[] _childSizes = Array.Empty<ChildSizingParameters>();

	private bool IsHorizontal
	{
		get
		{
			if (FlexDirection != FlexDirection.Row)
			{
				return FlexDirection == FlexDirection.RowReverse;
			}
			return true;
		}
	}

	protected override bool IsReversed
	{
		get
		{
			if (FlexDirection != FlexDirection.RowReverse)
			{
				return FlexDirection == FlexDirection.ColumnReverse;
			}
			return true;
		}
	}

	protected override void MeasureHorizontalImpl()
	{
		if (IsHorizontal)
		{
			MeasureMainAxis();
		}
		else
		{
			MeasureCrossAxis();
		}
	}

	protected override void LayoutHorizontalImpl(float maxWidth, float maxHeight)
	{
		if (IsHorizontal)
		{
			LayoutMainAxis(maxWidth, maxHeight);
		}
		else
		{
			LayoutCrossAxis(maxWidth, maxHeight);
		}
	}

	protected override void MeasureVerticalImpl()
	{
		if (IsHorizontal)
		{
			MeasureCrossAxis();
		}
		else
		{
			MeasureMainAxis();
		}
	}

	protected override void LayoutVerticalImpl(float maxWidth, float maxHeight)
	{
		if (IsHorizontal)
		{
			LayoutCrossAxis(maxWidth, maxHeight);
		}
		else
		{
			LayoutMainAxis(maxWidth, maxHeight);
		}
	}

	private void MeasureMainAxis()
	{
		bool isHorizontal = IsHorizontal;
		ref float reference = ref FlexElementBase.Pick(isHorizontal, ref PrefWidth, ref PrefHeight);
		float num = (isHorizontal ? (Padding.left + Padding.right) : (Padding.top + Padding.bottom));
		float num2 = 0f;
		bool flag = true;
		foreach (IFlexNode child in Children)
		{
			if (child.IsDirty)
			{
				if (isHorizontal)
				{
					child.MeasureHorizontal();
				}
				else
				{
					child.MeasureVertical();
				}
			}
			child.GetScale(out var scaleX, out var scaleY);
			child.GetPreferredSize(out var preferredWidth, out var preferredHeight);
			float num3 = (flag ? 0f : Gap);
			num2 = ((!isHorizontal) ? (num2 + (preferredHeight * scaleY + num3)) : (num2 + (preferredWidth * scaleX + num3)));
			if (flag)
			{
				flag = false;
			}
		}
		FlexLength flexLength = (isHorizontal ? MinWidth : MinHeight);
		FlexLength flexLength2 = (isHorizontal ? MaxWidth : MaxHeight);
		float b = ((Basis.HasValue && Basis.Unit == FlexUnit.Pixels) ? Basis.Value : 0f);
		float a = ((flexLength.HasValue && flexLength.Unit == FlexUnit.Pixels) ? flexLength.Value : 0f);
		float max = ((flexLength2.HasValue && flexLength2.Unit == FlexUnit.Pixels) ? flexLength2.Value : float.PositiveInfinity);
		reference = Mathf.Clamp(num2 + num, Mathf.Max(a, b), max);
		if (IsAbsolute)
		{
			Rect rect = ((RectTransform)base.transform).rect;
			if (isHorizontal && !AutoSizeX)
			{
				reference = rect.width;
			}
			else if (!isHorizontal && !AutoSizeY)
			{
				reference = rect.height;
			}
		}
	}

	private void LayoutMainAxis(float maxWidth, float maxHeight)
	{
		bool isHorizontal = IsHorizontal;
		bool isReversed = IsReversed;
		float innerSize = (isHorizontal ? (maxWidth - Padding.left - Padding.right) : (maxHeight - Padding.top - Padding.bottom));
		int num = Mathf.Max(Children.Count - 1, 0);
		float fillValue = innerSize - Gap * (float)num;
		SizingChildren.Clear();
		if (_childSizes.Length < Children.Count)
		{
			Array.Resize(ref _childSizes, Children.Count);
		}
		int num2 = 0;
		int num3 = 0;
		float num4 = 0f;
		bool flag = true;
		for (int i = 0; i < Children.Count; i++)
		{
			IFlexNode flexNode = Children[i];
			ref ChildSizingParameters reference = ref _childSizes[i];
			float num5 = FlexElementBase.CalculateLengthValue(isHorizontal ? flexNode.MinWidth : flexNode.MinHeight, fillValue, 0f);
			float num6 = FlexElementBase.CalculateLengthValue(isHorizontal ? flexNode.MaxWidth : flexNode.MaxHeight, fillValue, float.PositiveInfinity);
			bool flag2 = num5 < num6;
			flexNode.GetPreferredSize(out var preferredWidth, out var preferredHeight);
			float defaultValue = (isHorizontal ? preferredWidth : preferredHeight);
			flexNode.GetScale(out var scaleX, out var scaleY);
			float num7 = (isHorizontal ? scaleX : scaleY);
			num2 += flexNode.Grow;
			num3 += flexNode.Shrink;
			float num8 = (reference.Size = Mathf.Clamp(FlexElementBase.CalculateLengthValue(flexNode.Basis, fillValue, defaultValue), num5, num6));
			reference.MinSize = num5;
			reference.MaxSize = num6;
			reference.IsFlexible = flag2;
			reference.Scale = num7;
			SizingChildren.Add(flag2 ? flexNode : null);
			num4 += num8 * num7;
			if (flag)
			{
				flag = false;
			}
			else
			{
				num4 += Gap;
			}
		}
		float growthAllowance = Mathf.Max(innerSize - num4, 0f);
		float shrinkAllowance = Mathf.Max(num4 - innerSize, 0f);
		while (SizingChildren.Exists((IFlexNode n) => n != null))
		{
			int growSum = num2;
			int shrinkSum = num3;
			for (int num9 = 0; num9 < SizingChildren.Count; num9++)
			{
				IFlexNode child = SizingChildren[num9];
				if (child != null)
				{
					ref ChildSizingParameters reference2 = ref _childSizes[num9];
					bool flag3 = true;
					if (growthAllowance > 0f && child.Grow > 0 && reference2.IsFlexible)
					{
						flag3 = TakeGrowth(ref reference2.Size, reference2.MaxSize, reference2.Scale);
					}
					else if (shrinkAllowance > 0f && child.Shrink > 0 && reference2.IsFlexible)
					{
						flag3 = TakeShrink(ref reference2.Size, reference2.MinSize, reference2.Scale);
					}
					if (flag3)
					{
						SizingChildren[num9] = null;
					}
				}
				bool TakeGrowth(ref float value, float maxValue, float scale)
				{
					float max = (float)child.Grow / (float)growSum * growthAllowance;
					float num16 = Mathf.Clamp(maxValue - value, 0f, max);
					value += ((scale > 0f) ? (num16 / scale) : 0f);
					growthAllowance -= num16;
					growSum -= child.Grow;
					return num16 <= float.Epsilon;
				}
				bool TakeShrink(ref float value, float minValue, float scale)
				{
					float max = (float)child.Shrink / (float)shrinkSum * shrinkAllowance;
					float num16 = Mathf.Clamp(value - minValue, 0f, max);
					value -= ((scale > 0f) ? (num16 / scale) : 0f);
					shrinkAllowance -= num16;
					shrinkSum -= child.Shrink;
					return num16 <= float.Epsilon;
				}
			}
		}
		float actualMainSize = Gap * (float)num;
		for (int num10 = 0; num10 < Children.Count; num10++)
		{
			actualMainSize += _childSizes[num10].Size * _childSizes[num10].Scale;
		}
		actualMainSize = Mathf.Min(actualMainSize, innerSize);
		float num11 = 0f;
		float extraOffset = 0f;
		if (JustifyContent == FlexJustify.SpaceBetween && num > 0)
		{
			num11 = (innerSize - actualMainSize) / (float)num;
			actualMainSize = innerSize;
		}
		else if (JustifyContent == FlexJustify.SpaceAround)
		{
			num11 = (innerSize - actualMainSize) / (float)(num + 1);
			extraOffset = num11 / 2f;
			actualMainSize = innerSize;
		}
		else if (JustifyContent == FlexJustify.SpaceEvenly)
		{
			num11 = (extraOffset = (innerSize - actualMainSize) / (float)(num + 2));
			actualMainSize = innerSize;
		}
		float num12 = Gap + num11;
		float num13 = GetMainAxisStart(isHorizontal, isReversed);
		for (int num14 = 0; num14 < Children.Count; num14++)
		{
			IFlexNode flexNode2 = Children[num14];
			ref ChildSizingParameters reference3 = ref _childSizes[num14];
			if (isHorizontal)
			{
				flexNode2.LayoutHorizontal(reference3.Size, float.PositiveInfinity);
			}
			else
			{
				flexNode2.LayoutVertical(float.PositiveInfinity, reference3.Size);
			}
			RectTransform rectTransform = flexNode2.Transform;
			Vector2 sizeDelta = rectTransform.sizeDelta;
			rectTransform.sizeDelta = (isHorizontal ? new Vector2(reference3.Size, sizeDelta.y) : new Vector2(sizeDelta.x, reference3.Size));
			Vector2 anchoredPosition = rectTransform.anchoredPosition;
			rectTransform.anchoredPosition = (isHorizontal ? new Vector2(num13, anchoredPosition.y) : new Vector2(anchoredPosition.x, num13));
			float num15 = reference3.Size * reference3.Scale;
			num13 += (isHorizontal ? (num15 + num12) : (0f - num15 - num12));
		}
		float GetMainAxisStart(bool flag4, bool flag5)
		{
			switch (JustifyContent)
			{
			case FlexJustify.Start:
			case FlexJustify.SpaceBetween:
			case FlexJustify.SpaceAround:
			case FlexJustify.SpaceEvenly:
				if (!flag4)
				{
					return 0f - (flag5 ? (innerSize - actualMainSize + Padding.top + extraOffset) : (Padding.top + extraOffset));
				}
				if (!flag5)
				{
					return Padding.left + extraOffset;
				}
				return innerSize - actualMainSize + Padding.left + extraOffset;
			case FlexJustify.End:
				if (!flag4)
				{
					return 0f - (flag5 ? Padding.top : (innerSize - actualMainSize + Padding.top));
				}
				if (!flag5)
				{
					return innerSize - actualMainSize + Padding.left;
				}
				return Padding.left;
			case FlexJustify.Center:
				if (!flag4)
				{
					return 0f - (innerSize - actualMainSize) / 2f - Padding.top;
				}
				return (innerSize - actualMainSize) / 2f + Padding.left;
			default:
				throw new NotSupportedException(JustifyContent.ToString());
			}
		}
	}

	private void MeasureCrossAxis()
	{
		bool isHorizontal = IsHorizontal;
		ref float reference = ref FlexElementBase.Pick(isHorizontal, ref PrefHeight, ref PrefWidth);
		float num = (isHorizontal ? (Padding.top + Padding.bottom) : (Padding.left + Padding.right));
		float num2 = 0f;
		foreach (IFlexNode child in Children)
		{
			if (child.IsDirty)
			{
				if (isHorizontal)
				{
					child.MeasureVertical();
				}
				else
				{
					child.MeasureHorizontal();
				}
			}
			child.GetScale(out var scaleX, out var scaleY);
			child.GetPreferredSize(out var preferredWidth, out var preferredHeight);
			num2 = ((!isHorizontal) ? Mathf.Max(num2, preferredWidth * scaleX) : Mathf.Max(num2, preferredHeight * scaleY));
		}
		if (IsAbsolute && !AutoSizeY && isHorizontal)
		{
			reference = ((RectTransform)base.transform).rect.height;
			return;
		}
		if (IsAbsolute && !AutoSizeX && !isHorizontal)
		{
			reference = ((RectTransform)base.transform).rect.width;
			return;
		}
		FlexLength flexLength = (isHorizontal ? MinHeight : MinWidth);
		FlexLength flexLength2 = (isHorizontal ? MaxHeight : MaxWidth);
		float min = ((flexLength.HasValue && flexLength.Unit == FlexUnit.Pixels) ? flexLength.Value : 0f);
		float max = ((flexLength2.HasValue && flexLength2.Unit == FlexUnit.Pixels) ? flexLength2.Value : float.PositiveInfinity);
		reference = Mathf.Clamp(num2 + num, min, max);
	}

	private void LayoutCrossAxis(float maxWidth, float maxHeight)
	{
		bool isHorizontal = IsHorizontal;
		float innerSize = (isHorizontal ? (maxHeight - Padding.top - Padding.bottom) : (maxWidth - Padding.left - Padding.right));
		foreach (IFlexNode child in Children)
		{
			child.GetScale(out var scaleX, out var scaleY);
			child.GetPreferredSize(out var preferredWidth, out var preferredHeight);
			float num = (isHorizontal ? scaleY : scaleX);
			float num2 = ((num > 0f) ? (innerSize / num) : 0f);
			FlexAlign valueOrDefault = child.AlignSelf.GetValueOrDefault(AlignItems);
			float min = FlexElementBase.CalculateLengthValue(isHorizontal ? child.MinHeight : child.MinWidth, num2, 0f);
			float max = FlexElementBase.CalculateLengthValue(isHorizontal ? child.MaxHeight : child.MaxWidth, num2, float.PositiveInfinity);
			float num3 = (isHorizontal ? preferredHeight : preferredWidth);
			float num4 = Mathf.Clamp((valueOrDefault == FlexAlign.Stretch) ? num2 : num3, min, max);
			float num5 = (isHorizontal ? float.PositiveInfinity : num4);
			float num6 = (isHorizontal ? num4 : float.PositiveInfinity);
			if (isHorizontal)
			{
				child.LayoutVertical(num5, num6);
			}
			else
			{
				child.LayoutHorizontal(num5, num6);
			}
			float num7 = GetCrossAxis(valueOrDefault, isHorizontal, num5 * scaleX, num6 * scaleY);
			RectTransform rectTransform = child.Transform;
			Vector2 sizeDelta = rectTransform.sizeDelta;
			rectTransform.sizeDelta = (isHorizontal ? new Vector2(sizeDelta.x, num4) : new Vector2(num4, sizeDelta.y));
			Vector2 anchoredPosition = rectTransform.anchoredPosition;
			rectTransform.anchoredPosition = (isHorizontal ? new Vector2(anchoredPosition.x, num7) : new Vector2(num7, anchoredPosition.y));
		}
		float GetCrossAxis(FlexAlign align, bool flag, float childWidth, float childHeight)
		{
			switch (align)
			{
			case FlexAlign.Start:
			case FlexAlign.Stretch:
				if (!flag)
				{
					return Padding.left;
				}
				return 0f - Padding.top;
			case FlexAlign.End:
				if (!flag)
				{
					return innerSize + Padding.left - childWidth;
				}
				return 0f - innerSize - Padding.top + childHeight;
			case FlexAlign.Center:
				if (!flag)
				{
					return innerSize / 2f - childWidth / 2f + Padding.left;
				}
				return 0f - (innerSize / 2f - childHeight / 2f + Padding.top);
			default:
				throw new NotSupportedException(AlignItems.ToString());
			}
		}
	}
}
