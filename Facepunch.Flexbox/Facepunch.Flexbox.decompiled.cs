using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using Facepunch.Flexbox.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyVersion("0.0.0.0")]
[CompilerGenerated]
[EditorBrowsable(EditorBrowsableState.Never)]
[GeneratedCode("Unity.MonoScriptGenerator.MonoScriptInfoGenerator", null)]
internal class UnitySourceGeneratedAssemblyMonoScriptTypes_v1
{
	private struct MonoScriptData
	{
		public byte[] FilePathsData;

		public byte[] TypesData;

		public int TotalTypes;

		public int TotalFiles;

		public bool IsEditorOnly;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static MonoScriptData Get()
	{
		return new MonoScriptData
		{
			FilePathsData = new byte[936]
			{
				0, 0, 0, 1, 0, 0, 0, 50, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 70, 97, 99, 101, 112, 117,
				110, 99, 104, 46, 70, 108, 101, 120, 98, 111,
				120, 92, 70, 108, 101, 120, 65, 108, 105, 103,
				110, 83, 101, 108, 102, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 52, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 70, 97, 99, 101, 112, 117, 110, 99,
				104, 46, 70, 108, 101, 120, 98, 111, 120, 92,
				70, 108, 101, 120, 65, 115, 112, 101, 99, 116,
				82, 97, 116, 105, 111, 46, 99, 115, 0, 0,
				0, 2, 0, 0, 0, 56, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 70, 97, 99, 101, 112, 117, 110, 99,
				104, 46, 70, 108, 101, 120, 98, 111, 120, 92,
				70, 108, 101, 120, 67, 104, 105, 108, 100, 69,
				110, 117, 109, 101, 114, 97, 98, 108, 101, 46,
				99, 115, 0, 0, 0, 2, 0, 0, 0, 55,
				92, 65, 115, 115, 101, 116, 115, 92, 80, 108,
				117, 103, 105, 110, 115, 92, 70, 97, 99, 101,
				112, 117, 110, 99, 104, 46, 70, 108, 101, 120,
				98, 111, 120, 92, 70, 108, 101, 120, 67, 111,
				108, 117, 109, 110, 115, 69, 108, 101, 109, 101,
				110, 116, 46, 99, 115, 0, 0, 0, 2, 0,
				0, 0, 48, 92, 65, 115, 115, 101, 116, 115,
				92, 80, 108, 117, 103, 105, 110, 115, 92, 70,
				97, 99, 101, 112, 117, 110, 99, 104, 46, 70,
				108, 101, 120, 98, 111, 120, 92, 70, 108, 101,
				120, 69, 108, 101, 109, 101, 110, 116, 46, 99,
				115, 0, 0, 0, 1, 0, 0, 0, 52, 92,
				65, 115, 115, 101, 116, 115, 92, 80, 108, 117,
				103, 105, 110, 115, 92, 70, 97, 99, 101, 112,
				117, 110, 99, 104, 46, 70, 108, 101, 120, 98,
				111, 120, 92, 70, 108, 101, 120, 69, 108, 101,
				109, 101, 110, 116, 66, 97, 115, 101, 46, 99,
				115, 0, 0, 0, 1, 0, 0, 0, 57, 92,
				65, 115, 115, 101, 116, 115, 92, 80, 108, 117,
				103, 105, 110, 115, 92, 70, 97, 99, 101, 112,
				117, 110, 99, 104, 46, 70, 108, 101, 120, 98,
				111, 120, 92, 70, 108, 101, 120, 71, 114, 97,
				112, 104, 105, 99, 84, 114, 97, 110, 115, 102,
				111, 114, 109, 46, 99, 115, 0, 0, 0, 1,
				0, 0, 0, 54, 92, 65, 115, 115, 101, 116,
				115, 92, 80, 108, 117, 103, 105, 110, 115, 92,
				70, 97, 99, 101, 112, 117, 110, 99, 104, 46,
				70, 108, 101, 120, 98, 111, 120, 92, 70, 108,
				101, 120, 76, 97, 121, 111, 117, 116, 77, 97,
				110, 97, 103, 101, 114, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 47, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 70, 97, 99, 101, 112, 117, 110, 99,
				104, 46, 70, 108, 101, 120, 98, 111, 120, 92,
				70, 108, 101, 120, 76, 101, 110, 103, 116, 104,
				46, 99, 115, 0, 0, 0, 1, 0, 0, 0,
				48, 92, 65, 115, 115, 101, 116, 115, 92, 80,
				108, 117, 103, 105, 110, 115, 92, 70, 97, 99,
				101, 112, 117, 110, 99, 104, 46, 70, 108, 101,
				120, 98, 111, 120, 92, 70, 108, 101, 120, 80,
				97, 100, 100, 105, 110, 103, 46, 99, 115, 0,
				0, 0, 1, 0, 0, 0, 53, 92, 65, 115,
				115, 101, 116, 115, 92, 80, 108, 117, 103, 105,
				110, 115, 92, 70, 97, 99, 101, 112, 117, 110,
				99, 104, 46, 70, 108, 101, 120, 98, 111, 120,
				92, 70, 108, 101, 120, 83, 99, 111, 112, 101,
				100, 85, 112, 100, 97, 116, 101, 46, 99, 115,
				0, 0, 0, 1, 0, 0, 0, 45, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 70, 97, 99, 101, 112, 117,
				110, 99, 104, 46, 70, 108, 101, 120, 98, 111,
				120, 92, 70, 108, 101, 120, 84, 101, 120, 116,
				46, 99, 115, 0, 0, 0, 2, 0, 0, 0,
				51, 92, 65, 115, 115, 101, 116, 115, 92, 80,
				108, 117, 103, 105, 110, 115, 92, 70, 97, 99,
				101, 112, 117, 110, 99, 104, 46, 70, 108, 101,
				120, 98, 111, 120, 92, 70, 108, 101, 120, 84,
				114, 97, 110, 115, 105, 116, 105, 111, 110, 46,
				99, 115, 0, 0, 0, 1, 0, 0, 0, 48,
				92, 65, 115, 115, 101, 116, 115, 92, 80, 108,
				117, 103, 105, 110, 115, 92, 70, 97, 99, 101,
				112, 117, 110, 99, 104, 46, 70, 108, 101, 120,
				98, 111, 120, 92, 70, 108, 101, 120, 85, 116,
				105, 108, 105, 116, 121, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 46, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 70, 97, 99, 101, 112, 117, 110, 99,
				104, 46, 70, 108, 101, 120, 98, 111, 120, 92,
				70, 108, 101, 120, 86, 97, 108, 117, 101, 46,
				99, 115, 0, 0, 0, 1, 0, 0, 0, 46,
				92, 65, 115, 115, 101, 116, 115, 92, 80, 108,
				117, 103, 105, 110, 115, 92, 70, 97, 99, 101,
				112, 117, 110, 99, 104, 46, 70, 108, 101, 120,
				98, 111, 120, 92, 73, 70, 108, 101, 120, 78,
				111, 100, 101, 46, 99, 115
			},
			TypesData = new byte[796]
			{
				0, 0, 0, 0, 31, 70, 97, 99, 101, 112,
				117, 110, 99, 104, 46, 70, 108, 101, 120, 98,
				111, 120, 124, 70, 108, 101, 120, 65, 108, 105,
				103, 110, 83, 101, 108, 102, 0, 0, 0, 0,
				33, 70, 97, 99, 101, 112, 117, 110, 99, 104,
				46, 70, 108, 101, 120, 98, 111, 120, 124, 70,
				108, 101, 120, 65, 115, 112, 101, 99, 116, 82,
				97, 116, 105, 111, 0, 0, 0, 0, 37, 70,
				97, 99, 101, 112, 117, 110, 99, 104, 46, 70,
				108, 101, 120, 98, 111, 120, 124, 70, 108, 101,
				120, 67, 104, 105, 108, 100, 69, 110, 117, 109,
				101, 114, 97, 98, 108, 101, 0, 0, 0, 0,
				37, 70, 97, 99, 101, 112, 117, 110, 99, 104,
				46, 70, 108, 101, 120, 98, 111, 120, 124, 70,
				108, 101, 120, 67, 104, 105, 108, 100, 69, 110,
				117, 109, 101, 114, 97, 116, 111, 114, 0, 0,
				0, 0, 36, 70, 97, 99, 101, 112, 117, 110,
				99, 104, 46, 70, 108, 101, 120, 98, 111, 120,
				124, 70, 108, 101, 120, 67, 111, 108, 117, 109,
				110, 115, 69, 108, 101, 109, 101, 110, 116, 0,
				0, 0, 0, 53, 70, 97, 99, 101, 112, 117,
				110, 99, 104, 46, 70, 108, 101, 120, 98, 111,
				120, 46, 70, 108, 101, 120, 67, 111, 108, 117,
				109, 110, 115, 69, 108, 101, 109, 101, 110, 116,
				124, 67, 111, 108, 117, 109, 110, 80, 97, 114,
				97, 109, 101, 116, 101, 114, 115, 0, 0, 0,
				0, 29, 70, 97, 99, 101, 112, 117, 110, 99,
				104, 46, 70, 108, 101, 120, 98, 111, 120, 124,
				70, 108, 101, 120, 69, 108, 101, 109, 101, 110,
				116, 0, 0, 0, 0, 51, 70, 97, 99, 101,
				112, 117, 110, 99, 104, 46, 70, 108, 101, 120,
				98, 111, 120, 46, 70, 108, 101, 120, 69, 108,
				101, 109, 101, 110, 116, 124, 67, 104, 105, 108,
				100, 83, 105, 122, 105, 110, 103, 80, 97, 114,
				97, 109, 101, 116, 101, 114, 115, 0, 0, 0,
				0, 33, 70, 97, 99, 101, 112, 117, 110, 99,
				104, 46, 70, 108, 101, 120, 98, 111, 120, 124,
				70, 108, 101, 120, 69, 108, 101, 109, 101, 110,
				116, 66, 97, 115, 101, 0, 0, 0, 0, 38,
				70, 97, 99, 101, 112, 117, 110, 99, 104, 46,
				70, 108, 101, 120, 98, 111, 120, 124, 70, 108,
				101, 120, 71, 114, 97, 112, 104, 105, 99, 84,
				114, 97, 110, 115, 102, 111, 114, 109, 0, 0,
				0, 0, 35, 70, 97, 99, 101, 112, 117, 110,
				99, 104, 46, 70, 108, 101, 120, 98, 111, 120,
				124, 70, 108, 101, 120, 76, 97, 121, 111, 117,
				116, 77, 97, 110, 97, 103, 101, 114, 0, 0,
				0, 0, 28, 70, 97, 99, 101, 112, 117, 110,
				99, 104, 46, 70, 108, 101, 120, 98, 111, 120,
				124, 70, 108, 101, 120, 76, 101, 110, 103, 116,
				104, 0, 0, 0, 0, 29, 70, 97, 99, 101,
				112, 117, 110, 99, 104, 46, 70, 108, 101, 120,
				98, 111, 120, 124, 70, 108, 101, 120, 80, 97,
				100, 100, 105, 110, 103, 0, 0, 0, 0, 34,
				70, 97, 99, 101, 112, 117, 110, 99, 104, 46,
				70, 108, 101, 120, 98, 111, 120, 124, 70, 108,
				101, 120, 83, 99, 111, 112, 101, 100, 85, 112,
				100, 97, 116, 101, 0, 0, 0, 0, 26, 70,
				97, 99, 101, 112, 117, 110, 99, 104, 46, 70,
				108, 101, 120, 98, 111, 120, 124, 70, 108, 101,
				120, 84, 101, 120, 116, 0, 0, 0, 0, 32,
				70, 97, 99, 101, 112, 117, 110, 99, 104, 46,
				70, 108, 101, 120, 98, 111, 120, 124, 70, 108,
				101, 120, 84, 114, 97, 110, 115, 105, 116, 105,
				111, 110, 0, 0, 0, 0, 43, 70, 97, 99,
				101, 112, 117, 110, 99, 104, 46, 70, 108, 101,
				120, 98, 111, 120, 46, 70, 108, 101, 120, 84,
				114, 97, 110, 115, 105, 116, 105, 111, 110, 124,
				68, 101, 102, 105, 110, 105, 116, 105, 111, 110,
				0, 0, 0, 0, 37, 70, 97, 99, 101, 112,
				117, 110, 99, 104, 46, 70, 108, 101, 120, 98,
				111, 120, 46, 85, 116, 105, 108, 105, 116, 121,
				124, 70, 108, 101, 120, 85, 116, 105, 108, 105,
				116, 121, 0, 0, 0, 0, 27, 70, 97, 99,
				101, 112, 117, 110, 99, 104, 46, 70, 108, 101,
				120, 98, 111, 120, 124, 70, 108, 101, 120, 86,
				97, 108, 117, 101, 0, 0, 0, 0, 27, 70,
				97, 99, 101, 112, 117, 110, 99, 104, 46, 70,
				108, 101, 120, 98, 111, 120, 124, 73, 70, 108,
				101, 120, 78, 111, 100, 101
			},
			TotalFiles = 16,
			TotalTypes = 20,
			IsEditorOnly = false
		};
	}
}
namespace Facepunch.Flexbox
{
	public enum FlexDirection
	{
		Row,
		RowReverse,
		Column,
		ColumnReverse
	}
	public enum FlexJustify
	{
		Start,
		End,
		Center,
		SpaceBetween,
		SpaceAround,
		SpaceEvenly
	}
	public enum FlexAlign
	{
		Start,
		End,
		Center,
		Stretch
	}
	[Serializable]
	public struct FlexAlignSelf
	{
		public bool HasValue;

		public FlexAlign Value;

		internal FlexAlign GetValueOrDefault(FlexAlign defaultValue)
		{
			if (!HasValue)
			{
				return defaultValue;
			}
			return Value;
		}
	}
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	[DisallowMultipleComponent]
	public class FlexAspectRatio : MonoBehaviour, IFlexNode
	{
		[Tooltip("Controls the initial size of the element before factoring in grow/shrink.")]
		public FlexLength Basis;

		[Min(0f)]
		[Tooltip("How much this flex element should grow relative to its siblings.")]
		public int Grow = 1;

		[Min(0f)]
		[Tooltip("How much this flex element should shrink relative to its siblings.")]
		public int Shrink = 1;

		[Tooltip("Optionally override the parent's cross axis alignment for this element.")]
		public FlexAlignSelf AlignSelf;

		[Tooltip("The minimum allowed dimensions of this flex element.")]
		public FlexLength MinWidth;

		[Tooltip("The minimum allowed dimensions of this flex element.")]
		public FlexLength MaxWidth;

		[Tooltip("The maximum allowed dimensions of this flex element.")]
		public FlexLength MinHeight;

		[Tooltip("The maximum allowed dimensions of this flex element.")]
		public FlexLength MaxHeight;

		[Tooltip("The aspect ratio to constrain to - X:Y.")]
		public Vector2 AspectRatio = new Vector2(16f, 9f);

		private float _preferredWidth;

		private float _preferredHeight;

		RectTransform IFlexNode.Transform => (RectTransform)base.transform;

		bool IFlexNode.IsActive => base.isActiveAndEnabled;

		bool IFlexNode.IsAbsolute => false;

		bool IFlexNode.IsDirty => true;

		FlexLength IFlexNode.MinWidth => MinWidth;

		FlexLength IFlexNode.MaxWidth => MaxWidth;

		FlexLength IFlexNode.MinHeight => MinHeight;

		FlexLength IFlexNode.MaxHeight => MaxHeight;

		FlexLength IFlexNode.Basis => Basis;

		int IFlexNode.Grow => Grow;

		int IFlexNode.Shrink => Shrink;

		FlexAlignSelf IFlexNode.AlignSelf => AlignSelf;

		protected void OnEnable()
		{
			SetLayoutDirty();
		}

		protected void OnDisable()
		{
			SetLayoutDirty();
		}

		public void SetLayoutDirty()
		{
			Transform parent = base.transform.parent;
			if (parent != null && parent.TryGetComponent<IFlexNode>(out var component) && component.IsActive)
			{
				component.SetLayoutDirty();
			}
		}

		void IFlexNode.SetupTransform()
		{
			RectTransform obj = (RectTransform)base.transform;
			obj.localRotation = Quaternion.identity;
			obj.pivot = new Vector2(0f, 1f);
			obj.anchorMin = new Vector2(0f, 1f);
			obj.anchorMax = new Vector2(0f, 1f);
		}

		void IFlexNode.SetLayoutDirty(bool force)
		{
			if (force || base.isActiveAndEnabled)
			{
				SetLayoutDirty();
			}
		}

		void IFlexNode.MeasureHorizontal()
		{
			_preferredWidth = ((MinWidth.HasValue && MinWidth.Unit == FlexUnit.Pixels) ? MinWidth.Value : 1f);
			_preferredHeight = ((MinHeight.HasValue && MinHeight.Unit == FlexUnit.Pixels) ? MinHeight.Value : 1f);
		}

		void IFlexNode.LayoutHorizontal(float maxWidth, float maxHeight)
		{
		}

		void IFlexNode.MeasureVertical()
		{
			float num = ((AspectRatio.x > 0f && AspectRatio.y > 0f) ? (AspectRatio.x / AspectRatio.y) : 1f);
			_preferredHeight = ((RectTransform)base.transform).sizeDelta.x / num;
		}

		void IFlexNode.LayoutVertical(float maxWidth, float maxHeight)
		{
		}

		void IFlexNode.GetScale(out float scaleX, out float scaleY)
		{
			Vector3 localScale = ((RectTransform)base.transform).localScale;
			scaleX = localScale.x;
			scaleY = localScale.y;
		}

		void IFlexNode.GetPreferredSize(out float preferredWidth, out float preferredHeight)
		{
			preferredWidth = _preferredWidth;
			preferredHeight = _preferredHeight;
		}
	}
	internal struct FlexChildEnumerable : IEnumerable<IFlexNode>, IEnumerable
	{
		private readonly FlexElementBase _parent;

		private readonly bool _reversed;

		public FlexChildEnumerable(FlexElementBase parent, bool reversed)
		{
			_parent = parent;
			_reversed = reversed;
		}

		public FlexChildEnumerator GetEnumerator()
		{
			return new FlexChildEnumerator(_parent, _reversed);
		}

		IEnumerator<IFlexNode> IEnumerable<IFlexNode>.GetEnumerator()
		{
			throw new NotSupportedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotSupportedException();
		}
	}
	internal struct FlexChildEnumerator : IEnumerator<IFlexNode>, IEnumerator, IDisposable
	{
		private readonly Transform _parent;

		private readonly int _childCount;

		private readonly bool _reversed;

		private int _index;

		public IFlexNode Current { get; private set; }

		object IEnumerator.Current => Current;

		public FlexChildEnumerator(FlexElementBase parent, bool reversed)
		{
			_parent = parent.transform;
			_childCount = _parent.childCount;
			_reversed = reversed;
			_index = (reversed ? (_childCount - 1) : 0);
			Current = null;
		}

		public bool MoveNext()
		{
			IFlexNode component;
			while (true)
			{
				if (_reversed ? (_index < 0) : (_index >= _childCount))
				{
					Current = null;
					return false;
				}
				if (_parent.GetChild(_index).gameObject.TryGetComponent<IFlexNode>(out component) && component.IsActive && !component.IsAbsolute)
				{
					break;
				}
				_index += ((!_reversed) ? 1 : (-1));
			}
			Current = component;
			_index += ((!_reversed) ? 1 : (-1));
			return true;
		}

		public void Reset()
		{
		}

		public void Dispose()
		{
		}
	}
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	[DisallowMultipleComponent]
	public class FlexColumnsElement : FlexElementBase
	{
		private struct ColumnParameters
		{
			public float Height;

			public float Offset;
		}

		[Tooltip("Spacing to add from this elements borders to where children are laid out.")]
		public FlexPadding Padding;

		[Min(0f)]
		[Tooltip("Spacing to add between each child flex item.")]
		public float Gap;

		[Tooltip("Enable this to use a fixed number of columns.")]
		public bool FixedColumnCount;

		[Min(1f)]
		[Tooltip("The number of columns to use when using a fixed number of columns.")]
		public int ColumnCount = 1;

		[Min(1f)]
		[Tooltip("The minimum width of each column when not using a fixed number of columns.")]
		[FormerlySerializedAs("ColumnWidth")]
		public int ColumnMinWidth = 100;

		private int _calculatedColumnCount;

		private ColumnParameters[] _columnParams = Array.Empty<ColumnParameters>();

		protected override void MeasureHorizontalImpl()
		{
			float num = 0f;
			float scaleY;
			if (FixedColumnCount && ColumnCount > 0)
			{
				int num2 = Mathf.Min(ColumnCount, Children.Count);
				Span<float> span = stackalloc float[num2];
				int num3 = 0;
				foreach (IFlexNode child in Children)
				{
					if (child.IsDirty)
					{
						child.MeasureHorizontal();
					}
					child.GetScale(out var scaleX, out scaleY);
					child.GetPreferredSize(out var preferredWidth, out scaleY);
					span[num3] = Mathf.Max(span[num3], preferredWidth * scaleX);
					num3++;
					if (num3 >= num2)
					{
						num3 = 0;
					}
				}
				bool flag = true;
				for (int i = 0; i < num2; i++)
				{
					float num4 = (flag ? 0f : Gap);
					num += span[i] + num4;
					if (flag)
					{
						flag = false;
					}
				}
			}
			else
			{
				bool flag2 = true;
				foreach (IFlexNode child2 in Children)
				{
					if (child2.IsDirty)
					{
						child2.MeasureHorizontal();
					}
					child2.GetScale(out var scaleX2, out scaleY);
					child2.GetPreferredSize(out var preferredWidth2, out scaleY);
					float num5 = (flag2 ? 0f : Gap);
					num += preferredWidth2 * scaleX2 + num5;
					if (flag2)
					{
						flag2 = false;
					}
				}
			}
			float b = ((Basis.HasValue && Basis.Unit == FlexUnit.Pixels) ? Basis.Value : 0f);
			float a = ((MinWidth.HasValue && MinWidth.Unit == FlexUnit.Pixels) ? MinWidth.Value : 0f);
			float max = ((MaxWidth.HasValue && MaxWidth.Unit == FlexUnit.Pixels) ? MaxWidth.Value : float.PositiveInfinity);
			float num6 = Padding.left + Padding.right;
			PrefWidth = Mathf.Clamp(num + num6, Mathf.Max(a, b), max);
		}

		protected override void LayoutHorizontalImpl(float maxWidth, float maxHeight)
		{
			float num = maxWidth - Padding.left - Padding.right;
			_calculatedColumnCount = (FixedColumnCount ? ColumnCount : Mathf.Max(Mathf.FloorToInt((num + Gap) / ((float)ColumnMinWidth + Gap)), 1));
			int num2 = Mathf.Max(_calculatedColumnCount - 1, 0);
			float num3 = (num - Gap * (float)num2) / (float)_calculatedColumnCount;
			int num4 = 0;
			foreach (IFlexNode child in Children)
			{
				float min = FlexElementBase.CalculateLengthValue(child.MinWidth, num, 0f);
				float max = FlexElementBase.CalculateLengthValue(child.MaxWidth, num, float.PositiveInfinity);
				float num5 = Mathf.Clamp(num3, min, max);
				child.LayoutHorizontal(num5, float.PositiveInfinity);
				RectTransform rectTransform = child.Transform;
				rectTransform.sizeDelta = new Vector2(num5, rectTransform.sizeDelta.y);
				Vector2 anchoredPosition = rectTransform.anchoredPosition;
				rectTransform.anchoredPosition = new Vector2(Padding.left + (num3 + Gap) * (float)num4, anchoredPosition.y);
				num4++;
				if (num4 >= _calculatedColumnCount)
				{
					num4 = 0;
				}
			}
		}

		protected override void MeasureVerticalImpl()
		{
			EnsureColumnParamsSize();
			for (int i = 0; i < _calculatedColumnCount; i++)
			{
				_columnParams[i].Height = 0f;
			}
			int num = 0;
			bool flag = true;
			foreach (IFlexNode child in Children)
			{
				if (child.IsDirty)
				{
					child.MeasureVertical();
				}
				child.GetScale(out var scaleX, out var scaleY);
				child.GetPreferredSize(out scaleX, out var preferredHeight);
				float num2 = (flag ? 0f : Gap);
				_columnParams[num].Height += preferredHeight * scaleY + num2;
				num++;
				if (num >= _calculatedColumnCount)
				{
					num = 0;
					flag = false;
				}
			}
			float b = ((Basis.HasValue && Basis.Unit == FlexUnit.Pixels) ? Basis.Value : 0f);
			float a = ((MinHeight.HasValue && MinHeight.Unit == FlexUnit.Pixels) ? MinHeight.Value : 0f);
			float max = ((MaxHeight.HasValue && MaxHeight.Unit == FlexUnit.Pixels) ? MaxHeight.Value : float.PositiveInfinity);
			float num3 = 0f;
			for (int j = 0; j < _calculatedColumnCount; j++)
			{
				float height = _columnParams[j].Height;
				if (height > num3)
				{
					num3 = height;
				}
			}
			float num4 = Padding.top + Padding.bottom;
			PrefHeight = Mathf.Clamp(num3 + num4, Mathf.Max(a, b), max);
		}

		protected override void LayoutVerticalImpl(float maxWidth, float maxHeight)
		{
			float fillValue = maxHeight - Padding.top - Padding.bottom;
			EnsureColumnParamsSize();
			for (int i = 0; i < _calculatedColumnCount; i++)
			{
				_columnParams[i].Offset = 0f;
			}
			int num = 0;
			foreach (IFlexNode child in Children)
			{
				ref ColumnParameters reference = ref _columnParams[num];
				float min = FlexElementBase.CalculateLengthValue(child.MinHeight, fillValue, 0f);
				float max = FlexElementBase.CalculateLengthValue(child.MaxHeight, fillValue, float.PositiveInfinity);
				child.GetPreferredSize(out var _, out var preferredHeight);
				float num2 = Mathf.Clamp(preferredHeight, min, max);
				child.LayoutVertical(float.PositiveInfinity, num2);
				RectTransform rectTransform = child.Transform;
				rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, num2);
				rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 0f - (Padding.top + reference.Offset));
				reference.Offset += num2 + Gap;
				num++;
				if (num >= _calculatedColumnCount)
				{
					num = 0;
				}
			}
		}

		private void EnsureColumnParamsSize()
		{
			if (_columnParams.Length < _calculatedColumnCount)
			{
				Array.Resize(ref _columnParams, _calculatedColumnCount);
			}
		}
	}
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
	public abstract class FlexElementBase : UIBehaviour, IFlexNode
	{
		[Tooltip("Controls the initial size of the element before factoring in grow/shrink.")]
		public FlexLength Basis;

		[Min(0f)]
		[Tooltip("How much this flex element should grow relative to its siblings.")]
		public int Grow;

		[Min(0f)]
		[Tooltip("How much this flex element should shrink relative to its siblings.")]
		public int Shrink = 1;

		[Tooltip("Optionally override the parent's cross axis alignment for this element.")]
		public FlexAlignSelf AlignSelf;

		[Tooltip("The minimum allowed dimensions of this flex element.")]
		public FlexLength MinWidth;

		[Tooltip("The minimum allowed dimensions of this flex element.")]
		public FlexLength MaxWidth;

		[Tooltip("The maximum allowed dimensions of this flex element.")]
		public FlexLength MinHeight;

		[Tooltip("The maximum allowed dimensions of this flex element.")]
		public FlexLength MaxHeight;

		[Tooltip("Overrides for the preferred dimensions of this flex element. Useful for things like images which would normally have a preferred size of zero.")]
		public FlexValue OverridePreferredWidth;

		[Tooltip("Overrides for the preferred dimensions of this flex element. Useful for things like images which would normally have a preferred size of zero.")]
		public FlexValue OverridePreferredHeight;

		[Tooltip("Absolute elements act as the root container for any number of flex elements.")]
		public bool IsAbsolute;

		[Tooltip("Automatically resize an absolute element to match the size of its children.")]
		public bool AutoSizeX;

		[Tooltip("Automatically resize an absolute element to match the size of its children.")]
		public bool AutoSizeY;

		protected bool IsDirty;

		protected bool IsDoingLayout;

		protected float PrefWidth;

		protected float PrefHeight;

		protected readonly List<IFlexNode> Children = new List<IFlexNode>();

		protected virtual bool IsReversed => false;

		RectTransform IFlexNode.Transform => (RectTransform)base.transform;

		bool IFlexNode.IsActive => IsActive();

		bool IFlexNode.IsAbsolute => IsAbsolute;

		bool IFlexNode.IsDirty => IsDirty;

		FlexLength IFlexNode.MinWidth => MinWidth;

		FlexLength IFlexNode.MaxWidth => MaxWidth;

		FlexLength IFlexNode.MinHeight => MinHeight;

		FlexLength IFlexNode.MaxHeight => MaxHeight;

		FlexLength IFlexNode.Basis => Basis;

		int IFlexNode.Grow => Grow;

		int IFlexNode.Shrink => Shrink;

		FlexAlignSelf IFlexNode.AlignSelf => AlignSelf;

		internal void PerformLayout()
		{
			RectTransform rectTransform = (RectTransform)base.transform;
			Rect rect = rectTransform.rect;
			float width = rect.width;
			float height = rect.height;
			bool flag = !IsAbsolute && FlexUtility.IsPrefabRoot(base.gameObject);
			bool flag2 = (IsAbsolute && AutoSizeX) || flag;
			bool flag3 = (IsAbsolute && AutoSizeY) || flag;
			((IFlexNode)this).MeasureHorizontal();
			((IFlexNode)this).LayoutHorizontal(flag2 ? PrefWidth : width, flag3 ? PrefHeight : height);
			((IFlexNode)this).MeasureVertical();
			((IFlexNode)this).LayoutVertical(flag2 ? PrefWidth : width, flag3 ? PrefHeight : height);
			IsDoingLayout = true;
			try
			{
				if (flag2)
				{
					rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, PrefWidth);
				}
				if (flag3)
				{
					rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, PrefHeight);
				}
			}
			finally
			{
				IsDoingLayout = false;
			}
		}

		public void SetLayoutDirty(bool force = false)
		{
			if (force || (!IsDoingLayout && IsActive()))
			{
				IsDirty = true;
				Transform parent = base.transform.parent;
				if (IsAbsolute || parent == null || !parent.TryGetComponent<IFlexNode>(out var component))
				{
					FlexLayoutManager.EnqueueLayout(this);
				}
				else
				{
					component.SetLayoutDirty(force);
				}
			}
		}

		protected abstract void MeasureHorizontalImpl();

		protected abstract void LayoutHorizontalImpl(float maxWidth, float maxHeight);

		protected abstract void MeasureVerticalImpl();

		protected abstract void LayoutVerticalImpl(float maxWidth, float maxHeight);

		void IFlexNode.SetupTransform()
		{
			if (!IsAbsolute)
			{
				RectTransform obj = (RectTransform)base.transform;
				obj.localRotation = Quaternion.identity;
				obj.pivot = new Vector2(0f, 1f);
				obj.anchorMin = new Vector2(0f, 1f);
				obj.anchorMax = new Vector2(0f, 1f);
			}
		}

		void IFlexNode.MeasureHorizontal()
		{
			Children.Clear();
			foreach (IFlexNode item in new FlexChildEnumerable(this, IsReversed))
			{
				Children.Add(item);
				item.SetupTransform();
			}
			MeasureHorizontalImpl();
		}

		void IFlexNode.LayoutHorizontal(float maxWidth, float maxHeight)
		{
			IsDoingLayout = true;
			try
			{
				LayoutHorizontalImpl(maxWidth, maxHeight);
			}
			finally
			{
				IsDoingLayout = false;
			}
		}

		void IFlexNode.MeasureVertical()
		{
			MeasureVerticalImpl();
		}

		void IFlexNode.LayoutVertical(float maxWidth, float maxHeight)
		{
			IsDoingLayout = true;
			try
			{
				LayoutVerticalImpl(maxWidth, maxHeight);
				IsDirty = false;
			}
			finally
			{
				IsDoingLayout = false;
			}
		}

		void IFlexNode.GetScale(out float scaleX, out float scaleY)
		{
			Vector3 localScale = ((RectTransform)base.transform).localScale;
			scaleX = localScale.x;
			scaleY = localScale.y;
		}

		void IFlexNode.GetPreferredSize(out float preferredWidth, out float preferredHeight)
		{
			preferredWidth = Mathf.Clamp(OverridePreferredWidth.GetOrDefault(PrefWidth), MinWidth.GetValueOrDefault(0f), MaxWidth.GetValueOrDefault(float.PositiveInfinity));
			preferredHeight = Mathf.Clamp(OverridePreferredHeight.GetOrDefault(PrefHeight), MinHeight.GetValueOrDefault(0f), MaxHeight.GetValueOrDefault(float.PositiveInfinity));
		}

		protected override void OnEnable()
		{
			SetLayoutDirty(force: true);
		}

		protected override void OnDisable()
		{
			SetLayoutDirty(force: true);
		}

		protected override void OnRectTransformDimensionsChange()
		{
			SetLayoutDirty();
		}

		protected override void OnBeforeTransformParentChanged()
		{
			SetLayoutDirty();
		}

		protected override void OnTransformParentChanged()
		{
			SetLayoutDirty();
		}

		protected virtual void OnTransformChildrenChanged()
		{
			SetLayoutDirty();
		}

		protected static ref T Pick<T>(bool value, ref T ifTrue, ref T ifFalse)
		{
			if (value)
			{
				return ref ifTrue;
			}
			return ref ifFalse;
		}

		protected static float CalculateLengthValue(in FlexLength length, float fillValue, float defaultValue)
		{
			if (!length.HasValue)
			{
				return defaultValue;
			}
			if (length.Unit != FlexUnit.Percent)
			{
				return length.Value;
			}
			return length.Value / 100f * fillValue;
		}
	}
	[RequireComponent(typeof(RectTransform))]
	[DisallowMultipleComponent]
	[ExecuteAlways]
	public class FlexGraphicTransform : UIBehaviour, IMeshModifier
	{
		[Range(0f, 1f)]
		public float OriginX = 0.5f;

		[Range(0f, 1f)]
		public float OriginY = 0.5f;

		public float TranslateX;

		public float TranslateY;

		public float ScaleX = 1f;

		public float ScaleY = 1f;

		public float Rotate;

		private static readonly Dictionary<TextMeshProUGUI, FlexGraphicTransform> TextMeshProTransformers;

		private static readonly List<TMP_SubMeshUI> SubMeshUIs;

		private static readonly List<Mesh> Meshes;

		private static readonly VertexHelper VertexHelper;

		private static readonly List<Vector3> Vertices;

		private static readonly List<int> Indices;

		private static readonly List<Color32> Colors;

		private static readonly List<Vector2> Uv0;

		private static readonly List<Vector2> Uv1;

		private static readonly List<Vector3> Normals;

		private static readonly List<Vector4> Tangents;

		private FlexGraphicTransform _parent;

		private RectTransform _rt;

		private Graphic _graphic;

		private TextMeshProUGUI _textMeshPro;

		private CanvasRenderer _canvasRenderer;

		private Matrix4x4 transformationMatrix
		{
			get
			{
				Vector2 vector = (new Vector2(OriginX, OriginY) - _rt.pivot) * _rt.rect.size;
				Matrix4x4 matrix4x = Matrix4x4.Translate(new Vector3(vector.x, vector.y, 0f));
				Matrix4x4 matrix4x2 = Matrix4x4.TRS(new Vector3(TranslateX, TranslateY, 0f), Quaternion.Euler(0f, 0f, Rotate), new Vector3(ScaleX, ScaleY, 1f));
				Matrix4x4 matrix4x3 = matrix4x * matrix4x2 * matrix4x.inverse;
				if (!(_parent != null))
				{
					return matrix4x3;
				}
				return _parent.transformationMatrix * matrix4x3;
			}
		}

		static FlexGraphicTransform()
		{
			TextMeshProTransformers = new Dictionary<TextMeshProUGUI, FlexGraphicTransform>();
			SubMeshUIs = new List<TMP_SubMeshUI>();
			Meshes = new List<Mesh>();
			VertexHelper = new VertexHelper();
			Vertices = new List<Vector3>();
			Indices = new List<int>();
			Colors = new List<Color32>();
			Uv0 = new List<Vector2>();
			Uv1 = new List<Vector2>();
			Normals = new List<Vector3>();
			Tangents = new List<Vector4>();
			TMPro_EventManager.TEXT_CHANGED_EVENT.Add(delegate(UnityEngine.Object obj)
			{
				if (obj is TextMeshProUGUI key && TextMeshProTransformers.TryGetValue(key, out var value))
				{
					value.ModifyTextMeshPro();
				}
			});
		}

		protected override void Awake()
		{
			base.Awake();
			UpdateParent();
			_rt = GetComponent<RectTransform>();
			_graphic = GetComponent<Graphic>();
			_textMeshPro = GetComponent<TextMeshProUGUI>();
			_canvasRenderer = GetComponent<CanvasRenderer>();
		}

		private void UpdateParent()
		{
			_parent = ((base.transform.parent != null) ? base.transform.parent.GetComponent<FlexGraphicTransform>() : null);
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			if (_textMeshPro != null)
			{
				TextMeshProTransformers.Add(_textMeshPro, this);
			}
			SetVerticesDirty();
		}

		protected override void OnDisable()
		{
			if (_textMeshPro != null)
			{
				TextMeshProTransformers.Remove(_textMeshPro);
			}
			SetVerticesDirty();
			base.OnDisable();
		}

		protected override void OnDidApplyAnimationProperties()
		{
			SetVerticesDirty();
			base.OnDidApplyAnimationProperties();
		}

		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			UpdateParent();
		}

		public void SetVerticesDirty()
		{
			if (_textMeshPro != null && _textMeshPro.textInfo?.meshInfo != null)
			{
				TMP_MeshInfo[] meshInfo = _textMeshPro.textInfo.meshInfo;
				for (int i = 0; i < meshInfo.Length; i++)
				{
					TMP_MeshInfo tMP_MeshInfo = meshInfo[i];
					Mesh mesh = tMP_MeshInfo.mesh;
					if (mesh != null)
					{
						mesh.Clear();
						mesh.vertices = tMP_MeshInfo.vertices;
						mesh.uv = tMP_MeshInfo.uvs0;
						mesh.uv2 = tMP_MeshInfo.uvs2;
						mesh.colors32 = tMP_MeshInfo.colors32;
						mesh.normals = tMP_MeshInfo.normals;
						mesh.tangents = tMP_MeshInfo.tangents;
						mesh.triangles = tMP_MeshInfo.triangles;
					}
				}
				if (_canvasRenderer != null)
				{
					_canvasRenderer.SetMesh(_textMeshPro.mesh);
					_textMeshPro.GetComponentsInChildren(includeInactive: false, SubMeshUIs);
					foreach (TMP_SubMeshUI subMeshUI in SubMeshUIs)
					{
						subMeshUI.canvasRenderer.SetMesh(subMeshUI.mesh);
					}
					SubMeshUIs.Clear();
				}
				_textMeshPro.havePropertiesChanged = true;
			}
			else if (_graphic != null)
			{
				_graphic.SetVerticesDirty();
			}
			foreach (Transform item in base.transform)
			{
				if (item.TryGetComponent<FlexGraphicTransform>(out var component) && component.isActiveAndEnabled)
				{
					component.SetVerticesDirty();
				}
			}
		}

		public void ModifyMesh(Mesh mesh)
		{
			using VertexHelper vertexHelper = new VertexHelper(mesh);
			ModifyMesh(vertexHelper);
			vertexHelper.FillMesh(mesh);
		}

		public void ModifyMesh(VertexHelper vh)
		{
			if (_rt == null)
			{
				_rt = GetComponent<RectTransform>();
			}
			Matrix4x4 matrix4x = transformationMatrix;
			UIVertex vertex = default(UIVertex);
			int currentVertCount = vh.currentVertCount;
			for (int i = 0; i < currentVertCount; i++)
			{
				vh.PopulateUIVertex(ref vertex, i);
				vertex.position = matrix4x.MultiplyPoint(vertex.position);
				vh.SetUIVertex(vertex, i);
			}
		}

		private void ModifyTextMeshPro()
		{
			if (_textMeshPro == null || !base.isActiveAndEnabled)
			{
				return;
			}
			Meshes.Clear();
			TMP_MeshInfo[] meshInfo = _textMeshPro.textInfo.meshInfo;
			for (int i = 0; i < meshInfo.Length; i++)
			{
				TMP_MeshInfo tMP_MeshInfo = meshInfo[i];
				Meshes.Add(tMP_MeshInfo.mesh);
			}
			foreach (Mesh mesh in Meshes)
			{
				if (mesh != null)
				{
					CopyIntoVertexHelper(mesh);
					ModifyMesh(VertexHelper);
					VertexHelper.FillMesh(mesh);
				}
			}
			if (_canvasRenderer != null)
			{
				_canvasRenderer.SetMesh(_textMeshPro.mesh);
				GetComponentsInChildren(includeInactive: false, SubMeshUIs);
				foreach (TMP_SubMeshUI subMeshUI in SubMeshUIs)
				{
					subMeshUI.canvasRenderer.SetMesh(subMeshUI.mesh);
				}
				SubMeshUIs.Clear();
			}
			Meshes.Clear();
		}

		private static void CopyIntoVertexHelper(Mesh mesh)
		{
			VertexHelper.Clear();
			mesh.GetVertices(Vertices);
			mesh.GetIndices(Indices, 0);
			mesh.GetColors(Colors);
			mesh.GetUVs(0, Uv0);
			mesh.GetUVs(1, Uv1);
			mesh.GetNormals(Normals);
			mesh.GetTangents(Tangents);
			for (int i = 0; i < Vertices.Count; i++)
			{
				VertexHelper.AddVert(Vertices[i], Colors[i], Uv0[i], Uv1[i], Normals[i], Tangents[i]);
			}
			for (int j = 0; j < Indices.Count; j += 3)
			{
				VertexHelper.AddTriangle(Indices[j], Indices[j + 1], Indices[j + 2]);
			}
		}
	}
	[ExecuteAlways]
	[DefaultExecutionOrder(-100)]
	public class FlexLayoutManager : MonoBehaviour
	{
		internal static readonly HashSet<FlexElementBase> ActiveScopedUpdates = new HashSet<FlexElementBase>();

		private static readonly List<FlexElementBase> DirtyElements = new List<FlexElementBase>();

		private static readonly List<FlexElementBase> UpdatingElements = new List<FlexElementBase>();

		public static FlexLayoutManager Instance { get; private set; }

		public void OnEnable()
		{
			if (Instance != null)
			{
				UnityEngine.Debug.LogWarning("Cannot have multiple FlexLayoutManager!", this);
			}
			else
			{
				Instance = this;
			}
		}

		public void OnDisable()
		{
			if (Instance == this)
			{
				Instance = null;
			}
		}

		public void LateUpdate()
		{
			FlushQueue();
		}

		private static bool IsServerEnvironment()
		{
			return true;
		}

		public static void EnqueueLayout(FlexElementBase element)
		{
			if (IsServerEnvironment() || element == null)
			{
				return;
			}
			if (Instance == null)
			{
				UnityEngine.Debug.LogWarning("There is no FlexLayoutManager!");
				return;
			}
			if (!Instance.isActiveAndEnabled)
			{
				UnityEngine.Debug.LogWarning("FlexLayoutManager is not active!");
			}
			if (!DirtyElements.Contains(element) && !ActiveScopedUpdates.Contains(element))
			{
				DirtyElements.Add(element);
			}
		}

		internal static void LayoutImmediate(FlexElementBase element)
		{
			if (!(element == null) && element.IsAbsolute)
			{
				DirtyElements.Remove(element);
				element.PerformLayout();
			}
		}

		private static void FlushQueue()
		{
			if (DirtyElements.Count == 0)
			{
				return;
			}
			UpdatingElements.AddRange(DirtyElements);
			DirtyElements.Clear();
			try
			{
				foreach (FlexElementBase updatingElement in UpdatingElements)
				{
					if (updatingElement != null)
					{
						updatingElement.PerformLayout();
					}
				}
			}
			finally
			{
				UpdatingElements.Clear();
			}
		}
	}
	[Serializable]
	public struct FlexLength
	{
		public bool HasValue;

		public float Value;

		public FlexUnit Unit;

		public float GetValueOrDefault(float defaultValue)
		{
			if (!HasValue || Unit != FlexUnit.Pixels)
			{
				return defaultValue;
			}
			return Value;
		}
	}
	public enum FlexUnit
	{
		[InspectorName("px")]
		Pixels,
		[InspectorName("%")]
		Percent
	}
	[Serializable]
	public struct FlexPadding
	{
		public float left;

		public float right;

		public float top;

		public float bottom;

		public FlexPadding(float value)
		{
			left = (right = (top = (bottom = value)));
		}

		public FlexPadding(float left, float right, float top, float bottom)
		{
			this.left = left;
			this.right = right;
			this.top = top;
			this.bottom = bottom;
		}
	}
	public readonly struct FlexScopedUpdate : IDisposable
	{
		private readonly FlexElementBase _element;

		public FlexScopedUpdate(FlexElementBase element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (!element.IsAbsolute)
			{
				throw new ArgumentException("Scoped updates can only be started for absolute flex elements.");
			}
			if (!FlexLayoutManager.ActiveScopedUpdates.Add(element))
			{
				throw new InvalidOperationException("A scoped update is already active for this flex element.");
			}
			_element = element;
		}

		public void Dispose()
		{
			FlexLayoutManager.ActiveScopedUpdates.Remove(_element);
			FlexLayoutManager.LayoutImmediate(_element);
		}
	}
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	[DisallowMultipleComponent]
	public class FlexText : TextMeshProUGUI, IFlexNode
	{
		[Tooltip("Controls the initial size of the element before factoring in grow/shrink.")]
		public FlexLength Basis;

		[Min(0f)]
		[Tooltip("How much this flex element should grow relative to its siblings.")]
		public int Grow = 1;

		[Min(0f)]
		[Tooltip("How much this flex element should shrink relative to its siblings.")]
		public int Shrink = 1;

		[Tooltip("Optionally override the parent's cross axis alignment for this element.")]
		public FlexAlignSelf AlignSelf;

		[Tooltip("The minimum allowed dimensions of this flex element.")]
		public FlexLength MinWidth;

		[Tooltip("The minimum allowed dimensions of this flex element.")]
		public FlexLength MaxWidth;

		[Tooltip("The maximum allowed dimensions of this flex element.")]
		public FlexLength MinHeight;

		[Tooltip("The maximum allowed dimensions of this flex element.")]
		public FlexLength MaxHeight;

		private bool _isDirty;

		private float _preferredWidth;

		private float _preferredHeight;

		RectTransform IFlexNode.Transform => (RectTransform)base.transform;

		bool IFlexNode.IsActive => base.isActiveAndEnabled;

		bool IFlexNode.IsAbsolute => false;

		bool IFlexNode.IsDirty => _isDirty;

		FlexLength IFlexNode.MinWidth => MinWidth;

		FlexLength IFlexNode.MaxWidth => MaxWidth;

		FlexLength IFlexNode.MinHeight => MinHeight;

		FlexLength IFlexNode.MaxHeight => MaxHeight;

		FlexLength IFlexNode.Basis => Basis;

		int IFlexNode.Grow => Grow;

		int IFlexNode.Shrink => Shrink;

		FlexAlignSelf IFlexNode.AlignSelf => AlignSelf;

		protected override void OnEnable()
		{
			base.OnEnable();
			SetLayoutDirty();
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			SetLayoutDirty();
		}

		public override void SetLayoutDirty()
		{
			_isDirty = true;
			base.SetLayoutDirty();
			Transform parent = base.transform.parent;
			if (parent != null && parent.TryGetComponent<IFlexNode>(out var component) && component.IsActive)
			{
				component.SetLayoutDirty();
			}
		}

		void IFlexNode.SetupTransform()
		{
			RectTransform obj = (RectTransform)base.transform;
			obj.localRotation = Quaternion.identity;
			obj.pivot = new Vector2(0f, 1f);
			obj.anchorMin = new Vector2(0f, 1f);
			obj.anchorMax = new Vector2(0f, 1f);
		}

		void IFlexNode.SetLayoutDirty(bool force)
		{
			if (force || IsActive())
			{
				SetLayoutDirty();
			}
		}

		void IFlexNode.MeasureHorizontal()
		{
			float valueOrDefault = MaxWidth.GetValueOrDefault(float.PositiveInfinity);
			float valueOrDefault2 = MaxHeight.GetValueOrDefault(float.PositiveInfinity);
			Vector2 preferredValues = GetPreferredValues(valueOrDefault, valueOrDefault2);
			_preferredWidth = Mathf.Max(Mathf.Min(preferredValues.x, valueOrDefault), MinWidth.GetValueOrDefault(0f));
			_preferredHeight = Mathf.Max(Mathf.Min(preferredValues.y, valueOrDefault2), MinHeight.GetValueOrDefault(0f));
		}

		void IFlexNode.LayoutHorizontal(float maxWidth, float maxHeight)
		{
		}

		void IFlexNode.MeasureVertical()
		{
			Vector2 sizeDelta = ((RectTransform)base.transform).sizeDelta;
			float valueOrDefault = MaxWidth.GetValueOrDefault(float.PositiveInfinity);
			float valueOrDefault2 = MaxHeight.GetValueOrDefault(float.PositiveInfinity);
			Vector2 preferredValues = GetPreferredValues(sizeDelta.x, valueOrDefault2);
			_preferredWidth = Mathf.Max(Mathf.Min(preferredValues.x, valueOrDefault), MinWidth.GetValueOrDefault(0f));
			_preferredHeight = Mathf.Max(Mathf.Min(preferredValues.y, valueOrDefault2), MinHeight.GetValueOrDefault(0f));
		}

		void IFlexNode.LayoutVertical(float maxWidth, float maxHeight)
		{
			_isDirty = false;
		}

		void IFlexNode.GetScale(out float scaleX, out float scaleY)
		{
			Vector3 localScale = ((RectTransform)base.transform).localScale;
			scaleX = localScale.x;
			scaleY = localScale.y;
		}

		void IFlexNode.GetPreferredSize(out float preferredWidth, out float preferredHeight)
		{
			preferredWidth = _preferredWidth;
			preferredHeight = _preferredHeight;
		}
	}
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
	[Serializable]
	public struct FlexValue
	{
		public bool HasValue;

		public float Value;

		public float GetOrDefault(float defaultValue)
		{
			if (!HasValue)
			{
				return defaultValue;
			}
			return Value;
		}
	}
	public interface IFlexNode
	{
		RectTransform Transform { get; }

		bool IsActive { get; }

		bool IsAbsolute { get; }

		bool IsDirty { get; }

		FlexLength MinWidth { get; }

		FlexLength MaxWidth { get; }

		FlexLength MinHeight { get; }

		FlexLength MaxHeight { get; }

		int Grow { get; }

		int Shrink { get; }

		FlexLength Basis { get; }

		FlexAlignSelf AlignSelf { get; }

		void SetupTransform();

		void SetLayoutDirty(bool force = false);

		void MeasureHorizontal();

		void LayoutHorizontal(float maxWidth, float maxHeight);

		void MeasureVertical();

		void LayoutVertical(float maxWidth, float maxHeight);

		void GetScale(out float scaleX, out float scaleY);

		void GetPreferredSize(out float preferredWidth, out float preferredHeight);
	}
}
namespace Facepunch.Flexbox.Utility
{
	public static class FlexUtility
	{
		public static bool IsPrefabRoot(GameObject gameObject)
		{
			return false;
		}
	}
}
