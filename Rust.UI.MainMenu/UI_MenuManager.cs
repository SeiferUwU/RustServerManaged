using System;
using System.Collections.Generic;
using Development.Attributes;
using UnityEngine;

namespace Rust.UI.MainMenu;

[ResetStaticFields]
public class UI_MenuManager : SingletonComponent<UI_MenuManager>
{
	[SerializeField]
	private UI_Popup _genericPopupPrefab;

	[SerializeField]
	private Transform _genericPopupParent;

	[Header("Drops")]
	[SerializeField]
	private UI_DropsController _dropsController;

	[Header("Background Image Settings")]
	[SerializeField]
	private CanvasGroup _homeVideoOverlay;

	[SerializeField]
	private float _homeVideoOverlayAlpha = 1f;

	[SerializeField]
	private float _otherPageVideoOverlayAlpha = 0.98f;

	[SerializeField]
	private CanvasGroup _pageBackgroundOverlay;

	[SerializeField]
	private float _pageBackgroundOverlayAlpha = 0.98f;

	public static Action OnOpenStateChanged;

	private static bool _isOpen = true;

	public List<GameObject> HideInMenu = new List<GameObject>();

	public List<GameObject> HideInGame = new List<GameObject>();

	private UI_MenuNavigationGroup _navGroup;

	public Transform GenericPopupParent => _genericPopupParent;

	public static bool IsOpen => _isOpen;
}
