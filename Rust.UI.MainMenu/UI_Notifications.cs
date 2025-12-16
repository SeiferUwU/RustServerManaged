using UnityEngine;

namespace Rust.UI.MainMenu;

public class UI_Notifications : UI_Window
{
	[SerializeField]
	[Header("Prefab & Container")]
	private GameObject _entryPrefab;

	[SerializeField]
	private RectTransform _contentRoot;

	[SerializeField]
	private GameObject _noNotifications;

	[SerializeField]
	private GameObject _circle;

	[SerializeField]
	private StyleAsset _regularStyle;

	[SerializeField]
	private StyleAsset _seenStyle;
}
