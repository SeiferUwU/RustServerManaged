using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Rust/Skins/Inventory Item")]
public class SteamInventoryItem : ScriptableObject
{
	public enum Category
	{
		None,
		Clothing,
		Weapon,
		Decoration,
		Crate,
		Resource
	}

	public enum SubCategory
	{
		None,
		Shirt,
		Pants,
		Jacket,
		Hat,
		Mask,
		Footwear,
		Weapon,
		Misc,
		Crate,
		Resource,
		CrateUncraftable
	}

	public int id;

	public Sprite icon;

	public Translate.Phrase displayName;

	public Translate.Phrase displayDescription;

	[Tooltip("Images to show on the Steam store page for this item. Should all be square and hosted on https://files.facepunch.com/")]
	public string[] storeImages = Array.Empty<string>();

	[Header("Steam Inventory")]
	public Category category;

	public SubCategory subcategory;

	public SteamInventoryCategory steamCategory;

	[Tooltip("If true then this item will be placed under the Limited tab, otherwise it goes under General.")]
	public bool isLimitedTimeOffer = true;

	[Tooltip("Stop this item being broken down into cloth etc")]
	public bool PreventBreakingDown;

	[Tooltip("Set to true to allow players to delete the item from their inventory")]
	public bool IsTwitchDrop;

	[Header("Meta")]
	public string itemname;

	public ulong workshopID;

	public SteamDLCItem DlcItem;

	[Tooltip("Does nothing currently")]
	public bool forceCraftableItemDesc;

	[Tooltip("If enabled the item store will not show this as a 3d model")]
	public bool forceDisableTurntableInItemStore;

	[Tooltip("If the player owns this steam item, we will consider ourselves unlocked")]
	public SteamInventoryItem UnlockedViaSteamItem;

	public ItemDefinition itemDefinition => ItemManager.FindItemDefinition(itemname);

	public virtual bool HasUnlocked(ulong playerId)
	{
		if (DlcItem != null && DlcItem.HasLicense(playerId))
		{
			return true;
		}
		if (UnlockedViaSteamItem != null)
		{
			BasePlayer basePlayer = BasePlayer.FindByID(playerId);
			if (basePlayer != null && basePlayer.blueprints.CheckSkinOwnership(UnlockedViaSteamItem.id, basePlayer.userID))
			{
				return true;
			}
		}
		return false;
	}
}
