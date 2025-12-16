using Rust.UI;
using Rust.Workshop;
using UnityEngine;

public class WorkshopItemSelectionButton : MonoBehaviour
{
	public RustButton Button;

	[HideInInspector]
	public string ItemIdentifier;

	[HideInInspector]
	public WorkshopItemEditor Editor;

	private void OnEnable()
	{
		if (Button != null)
		{
			Button.OnToggleEnabled.AddListener(SelectItem);
		}
	}

	private void OnDisable()
	{
		if (Button != null)
		{
			Button.OnToggleEnabled.RemoveListener(SelectItem);
		}
	}

	public void SetItemType(string itemIdentifier, WorkshopItemEditor editor)
	{
		ItemIdentifier = itemIdentifier;
		Editor = editor;
		Button.Text.text = ItemIdentifier;
	}

	private void SelectItem()
	{
		if (!(Editor == null))
		{
			Editor.SelectItemType(ItemIdentifier);
		}
	}
}
