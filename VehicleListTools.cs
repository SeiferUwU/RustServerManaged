using System.Linq;
using Facepunch.Extend;
using Rust;
using Rust.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VehicleListTools : MonoBehaviour
{
	public GameObject categoryButton;

	public GameObject vehicleButtonPrefab;

	public Transform vehicleButtonParent;

	public RustInput searchInputText;

	internal Button lastCategory;

	public ScrollRect MainScrollRect;

	private IOrderedEnumerable<ItemDefinition> currentItems;

	private IOrderedEnumerable<ItemDefinition> allItems;

	public void OnPanelOpened()
	{
		CacheAllItems();
		Refresh();
		searchInputText.InputField.ActivateInputField();
	}

	private void OnOpenDevTools()
	{
		searchInputText.InputField.ActivateInputField();
	}

	private void CacheAllItems()
	{
		if (allItems == null)
		{
			allItems = from x in ItemManager.GetItemDefinitions()
				where x.vehicleItem
				orderby x.displayName.translated
				select x;
		}
	}

	public void Refresh()
	{
		RebuildCategories();
	}

	private void RebuildCategories()
	{
		for (int i = 0; i < categoryButton.transform.parent.childCount; i++)
		{
			Transform child = categoryButton.transform.parent.GetChild(i);
			if (!(child == categoryButton.transform))
			{
				GameManager.Destroy(child.gameObject);
			}
		}
		categoryButton.SetActive(value: true);
		foreach (IGrouping<VehicleCategory, ItemDefinition> item in from x in ItemManager.GetItemDefinitions()
			group x by x.vehicleCategory into x
			orderby x.First().vehicleCategory
			select x)
		{
			GameObject gameObject = Object.Instantiate(categoryButton);
			gameObject.transform.SetParent(categoryButton.transform.parent, worldPositionStays: false);
			gameObject.GetComponentInChildren<TextMeshProUGUI>().text = item.First().vehicleCategory.ToString();
			Button btn = gameObject.GetComponentInChildren<Button>();
			ItemDefinition[] itemArray = item.ToArray();
			btn.onClick.AddListener(delegate
			{
				if ((bool)lastCategory)
				{
					lastCategory.interactable = true;
				}
				lastCategory = btn;
				lastCategory.interactable = false;
				SwitchItemCategory(itemArray);
			});
			if (lastCategory == null)
			{
				lastCategory = btn;
				lastCategory.interactable = false;
				SwitchItemCategory(itemArray);
			}
		}
		categoryButton.SetActive(value: false);
	}

	private void SwitchItemCategory(ItemDefinition[] defs)
	{
		currentItems = defs.OrderBy((ItemDefinition x) => x.displayName.translated);
		searchInputText.Text = "";
		FilterItems(null);
		MainScrollRect.verticalNormalizedPosition = 1f;
	}

	public void FilterItems(string searchText)
	{
		if (vehicleButtonPrefab == null)
		{
			return;
		}
		vehicleButtonParent.DestroyAllChildren();
		bool flag = !string.IsNullOrEmpty(searchText);
		string value = (flag ? searchText.ToLower() : null);
		IOrderedEnumerable<ItemDefinition> obj = (flag ? allItems : currentItems);
		int num = 0;
		foreach (ItemDefinition item in obj)
		{
			if (item.vehicleItem && (!flag || item.shortname.ToLower().Contains(value)))
			{
				GameObject obj2 = Object.Instantiate(vehicleButtonPrefab, vehicleButtonParent);
				VehicleButtonTools componentInChildren = obj2.GetComponentInChildren<VehicleButtonTools>();
				componentInChildren.itemDef = item;
				componentInChildren.MainScroll = MainScrollRect;
				componentInChildren.image.sprite = item.iconSprite;
				componentInChildren.backgroundImage.enabled = !item.IsAllowed(EraRestriction.Default);
				obj2.GetComponentInChildren<TextMeshProUGUI>().text = item.displayName.translated;
				num++;
				if (num >= 160)
				{
					break;
				}
			}
		}
	}
}
