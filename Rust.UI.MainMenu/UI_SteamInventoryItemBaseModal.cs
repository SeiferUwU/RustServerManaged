using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rust.UI.MainMenu;

public class UI_SteamInventoryItemBaseModal : UI_Window
{
	[Serializable]
	private struct Tag
	{
		public string Name;

		public GameObject GameObject;
	}

	[SerializeField]
	protected HttpImage iconImage;

	[SerializeField]
	protected RustText nameText;

	[SerializeField]
	protected RustText descText;

	[SerializeField]
	protected RustText itemTypeText;

	[SerializeField]
	private UI_BackgroundAspectRatioFitter background;

	[SerializeField]
	[Header("Marketable Tag")]
	private GameObject marketablePriceGroup;

	[SerializeField]
	private GameObject marketableLockedGroup;

	[SerializeField]
	protected RustText daysLeftText;

	[SerializeField]
	protected RustText priceText;

	[SerializeField]
	private List<Tag> tagDefinitions = new List<Tag>();

	[Header("Skin Viewer")]
	[SerializeField]
	private CoverImage skinViewerImage;

	[SerializeField]
	private GameObject icon2D;

	[SerializeField]
	private GameObject icon3D;

	[SerializeField]
	private GameObject skinFullscreenButton;

	[SerializeField]
	private GameObject loadingOverlay;

	[SerializeField]
	private Color loadingColor;

	[SerializeField]
	private AnimationCurve loadingCompletePunchCurve;
}
