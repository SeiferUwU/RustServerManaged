using System.Collections.Generic;
using System.Linq;
using Facepunch;
using Facepunch.Flexbox;
using UnityEngine;
using UnityEngine.UI;

public class FlexVirtualScroll : MonoBehaviour
{
	public interface IVisualUpdate
	{
		void OnVisualUpdate(int i, GameObject obj);
	}

	[Tooltip("Optional, we'll try to GetComponent IDataSource from this object on awake")]
	public GameObject DataSourceObject;

	public GameObject SourceObject;

	public ScrollRect ScrollRect;

	public FlexElement FlexContentRoot;

	public FlexGridsElement FlexGrid;

	public int ItemHeight;

	public int ItemsPerLine = 1;

	public int Gap;

	public FlexElement topSpacer;

	public FlexElement bottomSpacer;

	[Tooltip("Objects that are already spawned and in editor rather than being instantiated at runtime")]
	public List<GameObject> PreloadObjects = new List<GameObject>();

	private VirtualScroll.IDataSource dataSource;

	private Dictionary<int, GameObject> ActivePool = new Dictionary<int, GameObject>();

	private Stack<GameObject> InactivePool = new Stack<GameObject>();

	private FlexValue topHeight;

	private FlexValue bottomHeight;

	public int BlockHeight
	{
		get
		{
			if (FlexContentRoot != null)
			{
				return ItemHeight;
			}
			if (FlexGrid != null)
			{
				return ItemHeight;
			}
			return 0;
		}
	}

	public void Awake()
	{
		ScrollRect.onValueChanged.AddListener(OnScrollChanged);
		if (DataSourceObject != null)
		{
			SetDataSource(DataSourceObject.GetComponent<VirtualScroll.IDataSource>());
		}
	}

	public void PrewarmPool()
	{
		foreach (GameObject preloadObject in PreloadObjects)
		{
			preloadObject.SetActive(value: false);
			if (FlexContentRoot != null)
			{
				preloadObject.transform.SetParent(FlexContentRoot.transform, worldPositionStays: false);
			}
			else if (FlexGrid != null)
			{
				preloadObject.transform.SetParent(FlexGrid.transform, worldPositionStays: false);
			}
			InactivePool.Push(preloadObject);
		}
	}

	public void OnDestroy()
	{
		ScrollRect.onValueChanged.RemoveListener(OnScrollChanged);
	}

	private void OnScrollChanged(Vector2 pos)
	{
		Rebuild();
	}

	public void SetDataSource(VirtualScroll.IDataSource source, bool forceRebuild = false)
	{
		if (dataSource != source || forceRebuild)
		{
			dataSource = source;
			FullRebuild();
		}
	}

	public void FullRebuild()
	{
		int[] array = ActivePool.Keys.ToArray();
		foreach (int key in array)
		{
			Recycle(key);
		}
		Rebuild();
	}

	public void DataChanged()
	{
		foreach (KeyValuePair<int, GameObject> item in ActivePool)
		{
			dataSource.SetItemData(item.Key, item.Value);
		}
		Rebuild();
	}

	public void Rebuild()
	{
		if (dataSource == null)
		{
			return;
		}
		int itemCount = dataSource.GetItemCount();
		int num = Mathf.CeilToInt((float)itemCount / (float)ItemsPerLine);
		float size = num * BlockHeight + Mathf.Max(0, (num - 1) * Gap);
		ScrollRect.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
		float y = ScrollRect.content.anchoredPosition.y;
		float num2 = ScrollRect.content.anchoredPosition.y + ScrollRect.viewport.rect.height;
		float num3 = BlockHeight + Gap;
		int num4 = 0;
		int num5 = num - 1;
		for (int i = 0; i < num; i++)
		{
			if ((float)(i * (BlockHeight + Gap)) + (float)BlockHeight >= y - num3)
			{
				num4 = i;
				break;
			}
		}
		for (int num6 = num - 1; num6 >= 0; num6--)
		{
			if ((float)(num6 * (BlockHeight + Gap)) <= num2 + num3)
			{
				num5 = num6;
				break;
			}
		}
		int num7 = num4 * ItemsPerLine;
		int num8 = Mathf.Min((num5 + 1) * ItemsPerLine, itemCount);
		RecycleOutOfRange(num7, num8 - 1);
		for (int j = num7; j < num8; j++)
		{
			BuildItem(j);
		}
		ReorderFlexChildren(num7, num8 - 1);
		topHeight.Value = num4 * (BlockHeight + Gap);
		topHeight.HasValue = true;
		float num9 = num - (num5 + 1);
		bottomHeight.Value = num9 * (float)(BlockHeight + Gap);
		bottomHeight.HasValue = true;
		topSpacer.OverridePreferredHeight = topHeight;
		bottomSpacer.OverridePreferredHeight = bottomHeight;
		if (FlexGrid != null)
		{
			FlexGrid.SetLayoutDirty(force: true);
		}
		if (FlexContentRoot != null)
		{
			FlexContentRoot.SetLayoutDirty(force: true);
		}
	}

	public void Update()
	{
		if (!(dataSource is IVisualUpdate visualUpdate))
		{
			return;
		}
		foreach (KeyValuePair<int, GameObject> item in ActivePool)
		{
			visualUpdate.OnVisualUpdate(item.Key, item.Value);
		}
	}

	private void RecycleOutOfRange(int startVisible, float endVisible)
	{
		int[] array = (from x in ActivePool.Keys
			where x < startVisible || (float)x > endVisible
			select (x)).ToArray();
		foreach (int key in array)
		{
			Recycle(key);
		}
	}

	private void Recycle(int key)
	{
		GameObject gameObject = ActivePool[key];
		gameObject.SetActive(value: false);
		ActivePool.Remove(key);
		InactivePool.Push(gameObject);
	}

	private void BuildItem(int i)
	{
		if (i >= 0 && !ActivePool.ContainsKey(i))
		{
			GameObject item = GetItem();
			item.SetActive(value: true);
			dataSource.SetItemData(i, item);
			ActivePool[i] = item;
		}
	}

	private GameObject GetItem()
	{
		if (InactivePool.Count == 0)
		{
			GameObject gameObject = Object.Instantiate(SourceObject);
			if (FlexContentRoot != null)
			{
				gameObject.transform.SetParent(FlexContentRoot.transform, worldPositionStays: false);
			}
			if (FlexGrid != null)
			{
				gameObject.transform.SetParent(FlexGrid.transform, worldPositionStays: false);
			}
			gameObject.transform.localScale = Vector3.one;
			gameObject.SetActive(value: false);
			InactivePool.Push(gameObject);
		}
		return InactivePool.Pop();
	}

	private void ReorderFlexChildren(int startIndex, int endIndex)
	{
		int num = 0;
		topSpacer.transform.SetSiblingIndex(num++);
		for (int i = startIndex; i <= endIndex; i++)
		{
			if (ActivePool.TryGetValue(i, out var value))
			{
				value.transform.SetSiblingIndex(num++);
			}
		}
		bottomSpacer.transform.SetSiblingIndex(num++);
	}
}
