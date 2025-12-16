using System;
using System.Collections.Generic;
using System.Linq;
using Facepunch;
using Rust.UI.MainMenu;
using Rust.Workshop;
using UnityEngine;

public class SkinViewer2 : SingletonComponent<SkinViewer2>
{
	public Camera cam;

	[SerializeField]
	private GameObject parent;

	[SerializeField]
	private GameObject modelsParent;

	[SerializeField]
	private GameObject defaultLightingRig;

	public List<WorkshopRenderSettings> renderSettings;

	private CoverImage targetImage;

	private static ItemSchema.Item[] schemaItems;

	public GameObject currentSkinGameObject { get; private set; }

	public bool IsOpen => parent.activeSelf;

	protected override void Awake()
	{
		base.Awake();
		parent.SetActive(value: false);
		UI_MenuManager.OnOpenStateChanged = (Action)Delegate.Combine(UI_MenuManager.OnOpenStateChanged, new Action(OnMenuOpenStateChanged));
	}

	protected override void OnDestroy()
	{
		UI_MenuManager.OnOpenStateChanged = (Action)Delegate.Remove(UI_MenuManager.OnOpenStateChanged, new Action(OnMenuOpenStateChanged));
	}

	public bool TrySet(CoverImage target, IPlayerItemDefinition playerItemDef, Action<bool> skinLoadCallback)
	{
		if (playerItemDef == null)
		{
			return false;
		}
		if (!ItemSkinDirectory.TryGetItemFromDefinitionID(playerItemDef.DefinitionId, out var result))
		{
			string text = playerItemDef.ItemShortName;
			if (text == "lr300.item")
			{
				text = "rifle.lr300";
			}
			if (!string.IsNullOrEmpty(text))
			{
				result = ItemManager.FindItemDefinition(text);
			}
		}
		return TrySet(target, result, playerItemDef.DefinitionId, playerItemDef.WorkshopId, skinLoadCallback);
	}

	public bool TrySet(CoverImage target, ItemDefinition itemDef, int skinID, ulong workshopID, Action<bool> skinLoadCallback)
	{
		GameObject gameObject = null;
		bool flag = false;
		foreach (WorkshopRenderSettings renderSetting in renderSettings)
		{
			if (renderSetting == null)
			{
				continue;
			}
			if ((itemDef != null && renderSetting.ItemDefinition == itemDef) || (skinID != 0 && renderSetting.itemID == skinID))
			{
				gameObject = renderSetting.gameObject;
				renderSetting.gameObject.SetActive(value: true);
				if (renderSetting.ToggleLightingRig && renderSetting.LightingRig != null)
				{
					renderSetting.LightingRig.gameObject.SetActive(value: true);
					flag = true;
				}
			}
			else
			{
				renderSetting.gameObject.SetActive(value: false);
			}
		}
		defaultLightingRig.gameObject.SetActive(!flag);
		if (gameObject == null)
		{
			skinLoadCallback?.Invoke(obj: false);
			return false;
		}
		targetImage = target;
		RefreshRenderTexture();
		parent.SetActive(value: true);
		currentSkinGameObject = gameObject;
		ItemSkinDirectory.Skin skin = ItemSkinDirectory.FindByInventoryDefinitionId(skinID);
		Rust.Workshop.WorkshopSkin.Reset(base.gameObject);
		if (workshopID != 0L)
		{
			if (skin.id == 0)
			{
				Rust.Workshop.WorkshopSkin.Apply(gameObject, workshopID, delegate(Skin skin2)
				{
					if (IsOpen)
					{
						RemoveMipMapLimit(skin2);
						skinLoadCallback?.Invoke(obj: true);
					}
				});
			}
			else if (skin.id == skinID)
			{
				(skin.invItem as ItemSkin).ApplySkin(gameObject);
				skinLoadCallback?.Invoke(obj: true);
			}
		}
		else
		{
			if (skin.id == skinID)
			{
				ItemSkin itemSkin = skin.invItem as ItemSkin;
				if (itemSkin != null)
				{
					itemSkin.ApplySkin(gameObject);
				}
			}
			skinLoadCallback?.Invoke(obj: true);
		}
		return true;
	}

	private void RemoveMipMapLimit(Skin skin)
	{
		foreach (Texture2D allTexture in skin.GetAllTextures())
		{
			if (allTexture.isReadable)
			{
				allTexture.requestedMipmapLevel = 0;
				allTexture.ignoreMipmapLimit = true;
			}
		}
	}

	public void RefreshRenderTexture()
	{
		if (targetImage == null)
		{
			return;
		}
		int num = 1920;
		int num2 = 1080;
		if (cam.targetTexture != null && cam.targetTexture.width == num && cam.targetTexture.height == num2)
		{
			targetImage.texture = cam.targetTexture;
			return;
		}
		if (cam.targetTexture != null)
		{
			cam.targetTexture.Release();
			UnityEngine.Object.Destroy(cam.targetTexture);
		}
		RenderTexture renderTexture = new RenderTexture(num, num2, 16)
		{
			useMipMap = true,
			autoGenerateMips = true,
			filterMode = FilterMode.Trilinear
		};
		cam.targetTexture = renderTexture;
		cam.pixelRect = new Rect(0f, 0f, num, num2);
		targetImage.texture = renderTexture;
	}

	public void Close()
	{
		parent.SetActive(value: false);
	}

	public static bool ShouldShow3DSkin(IPlayerItemDefinition playerItemDef)
	{
		if (!SingletonComponent<SkinViewer2>.Instance)
		{
			return false;
		}
		if (playerItemDef == null)
		{
			return false;
		}
		if (!string.IsNullOrEmpty(playerItemDef.ItemShortName))
		{
			string text = playerItemDef.ItemShortName;
			if (text == "lr300.item")
			{
				text = "rifle.lr300";
			}
			if (ItemManager.FindItemDefinition(text) != null)
			{
				return true;
			}
		}
		return SingletonComponent<SkinViewer2>.Instance.renderSettings.FirstOrDefault((WorkshopRenderSettings x) => x.itemID == playerItemDef.DefinitionId) != null;
	}

	private void OnMenuOpenStateChanged()
	{
		if (!MainMenu.IsOpen())
		{
			Close();
		}
	}

	[ClientVar(ClientAdmin = true)]
	public static void printSupportedItems(ConsoleSystem.Arg arg)
	{
		using TextTable textTable = Pool.Get<TextTable>();
		textTable.AddColumns("Item", "Name");
		foreach (WorkshopRenderSettings renderSetting in SingletonComponent<SkinViewer2>.Instance.renderSettings)
		{
			ItemDefinition itemDefinition = renderSetting.ItemDefinition;
			textTable.AddRow(itemDefinition.shortname, itemDefinition.displayName.english);
		}
		arg.ReplyWith(textTable.ToString());
	}
}
