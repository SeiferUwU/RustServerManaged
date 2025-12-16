using UnityEngine;

namespace Rust.UI.MainMenu;

public class UI_NotificationEntry : MonoBehaviour
{
	[SerializeField]
	[Header("Icons")]
	private RustIcon _basicIcon;

	[SerializeField]
	private RustIcon _standardIcon;

	[SerializeField]
	private RustIcon _banIcon;

	[SerializeField]
	private RustIcon _warningIcon;

	[SerializeField]
	[Header("UI Elements")]
	private GameObject _linkIcon;

	[SerializeField]
	private RustButton _linkButton;

	[SerializeField]
	private RustText _notificationText;
}
