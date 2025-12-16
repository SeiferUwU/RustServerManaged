using System;
using Facepunch.Models;
using UnityEngine;

namespace Rust.UI.MainMenu;

[Serializable]
public struct ItemStoreTakeover
{
	public Translate.Phrase NameOverride;

	public Translate.Phrase SubtitleOverride;

	public Translate.Phrase HeaderPhrase;

	public Sprite IconOverride;

	public Sprite IconPortraitOverride;

	public Sprite IconSquareOverride;

	public string ImageURL;

	public string VideoURL;

	public UI_StoreItemOverlayPage PagePrefab;

	public UI_StoreItemTile TilePrefabOverride;

	public global::SteamInventoryItem Item;

	[Tooltip("Will be used if you don't have an Item definition (DLCs)")]
	public int ItemId;

	public readonly int GetItemID()
	{
		if (!(Item != null))
		{
			return ItemId;
		}
		return Item.id;
	}

	public ItemStoreTakeover(StoreFeaturing storeFeaturing)
	{
		NameOverride = storeFeaturing.TitleText;
		SubtitleOverride = storeFeaturing.SubtitleText;
		HeaderPhrase = storeFeaturing.HeaderText;
		ImageURL = storeFeaturing.ImageUrl;
		VideoURL = storeFeaturing.VideoUrl;
		ItemId = storeFeaturing.ItemID;
		IconOverride = null;
		IconPortraitOverride = null;
		IconSquareOverride = null;
		PagePrefab = null;
		TilePrefabOverride = null;
		Item = null;
	}

	public readonly bool IsValid()
	{
		return GetItemID() != 0;
	}

	public void OverridesWith(ItemStoreTakeover other)
	{
		if (!string.IsNullOrEmpty(other.NameOverride.translated))
		{
			NameOverride = other.NameOverride;
		}
		if (!string.IsNullOrEmpty(other.SubtitleOverride.translated))
		{
			SubtitleOverride = other.SubtitleOverride;
		}
		if (other.HeaderPhrase != null && !string.IsNullOrEmpty(other.HeaderPhrase.translated))
		{
			HeaderPhrase = other.HeaderPhrase;
		}
		if (other.IconOverride != null)
		{
			IconOverride = other.IconOverride;
		}
		if (other.IconPortraitOverride != null)
		{
			IconPortraitOverride = other.IconPortraitOverride;
		}
		if (other.IconSquareOverride != null)
		{
			IconSquareOverride = other.IconSquareOverride;
		}
		if (!string.IsNullOrEmpty(other.ImageURL))
		{
			ImageURL = other.ImageURL;
		}
		if (!string.IsNullOrEmpty(other.VideoURL))
		{
			VideoURL = other.VideoURL;
		}
		if (other.PagePrefab != null)
		{
			PagePrefab = other.PagePrefab;
		}
		if (other.TilePrefabOverride != null)
		{
			TilePrefabOverride = other.TilePrefabOverride;
		}
		if (other.Item != null)
		{
			Item = other.Item;
		}
		if (other.ItemId != 0)
		{
			ItemId = other.ItemId;
		}
	}

	public Sprite GetBestIconForRect(float width, float height)
	{
		float num = width / height;
		bool flag = num > 1.15f;
		bool flag2 = num < 0.8f;
		if (flag)
		{
			return IconOverride;
		}
		if (flag2)
		{
			if (IconPortraitOverride != null)
			{
				return IconPortraitOverride;
			}
			return IconOverride;
		}
		if (IconSquareOverride != null)
		{
			return IconSquareOverride;
		}
		return IconOverride;
	}
}
