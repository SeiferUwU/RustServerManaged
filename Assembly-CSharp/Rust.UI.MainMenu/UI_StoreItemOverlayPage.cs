using System;
using Facepunch.Flexbox;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace Rust.UI.MainMenu;

public class UI_StoreItemOverlayPage : UI_Window
{
	[Serializable]
	public struct PageElement
	{
		public Translate.Phrase Name;

		[ItemSelector]
		public ItemDefinition Item;

		public bool isVideo;

		public string videoURL;

		public Sprite FullscreenSprite;

		public Sprite GallerySprite;

		[Min(0f)]
		public int VariantCount;

		public bool overrideItem;

		public Translate.Phrase ItemName;

		public Sprite ItemIcon;

		public bool UseSkinViewer;

		public Translate.Phrase GetTitle()
		{
			if (Name != null && !string.IsNullOrEmpty(Name.english))
			{
				return Name;
			}
			if (Item != null)
			{
				return Item.displayName;
			}
			return string.Empty;
		}

		public Translate.Phrase GetRedirectItemName()
		{
			if (Item != null && Item.isRedirectOf != null)
			{
				return Item.isRedirectOf.displayName;
			}
			if (overrideItem)
			{
				return ItemName;
			}
			return null;
		}

		public Sprite GetRedirectItemIcon()
		{
			if (Item != null && Item.isRedirectOf != null)
			{
				return Item.isRedirectOf.iconSprite;
			}
			if (overrideItem)
			{
				return ItemIcon;
			}
			return null;
		}
	}

	[Serializable]
	public struct PageContent
	{
		public PageElement[] Elements;
	}

	[Header("Page Content")]
	[Space]
	[SerializeField]
	private CanvasGroup bodyCanvasGroup;

	[SerializeField]
	private FlexTransition crossFadeTransition;

	[SerializeField]
	private CoverVideo coverVideo;

	[SerializeField]
	private CoverImage coverImage;

	[SerializeField]
	private UI_BackgroundAspectRatioFitter coverBackground;

	[SerializeField]
	private Canvas backButtonCanvas;

	[SerializeField]
	private GameObject textContainerGroup;

	[SerializeField]
	private RustText titleText;

	[SerializeField]
	private GameObject itemGroup;

	[SerializeField]
	private RustText itemNameText;

	[SerializeField]
	private Image itemIconImage;

	[SerializeField]
	private GameObject variantGroup;

	[SerializeField]
	private RustText variantCoutText;

	[Header("Gallery")]
	public SpriteAtlas SmallAtlas;

	[SerializeField]
	private Transform galleryParent;

	[SerializeField]
	private Canvas galleryCanvas;

	[SerializeField]
	private CanvasGroup arrowButtons;

	[SerializeField]
	private ScrollRect scrollRect;

	[SerializeField]
	private CanvasGroup leftArrow;

	[SerializeField]
	private CanvasGroup rightArrow;

	[SerializeField]
	private UI_StoreAddCartButton cartButton;

	[SerializeField]
	private GameObject ownedButton;

	[Space]
	[SerializeField]
	private bool autoCycleEnabled = true;

	[SerializeField]
	private float autoCycleInterval = 10f;

	[Header("Skin Viewer")]
	[SerializeField]
	private UI_SkinViewerControls skinViewerControls;

	[SerializeField]
	private CoverImage skinViewerImage;

	[SerializeField]
	private UI_StoreCarrouselButton carouselButtonPrefab;

	[Space]
	[SerializeField]
	private PageContent pageContent;
}
