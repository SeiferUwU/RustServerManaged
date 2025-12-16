using UnityEngine;

namespace Rust.UI.MainMenu;

public class UI_Popup : UI_Window
{
	[Space]
	[SerializeField]
	private Transform buttonsParent;

	[SerializeField]
	private RustText titleText;

	[SerializeField]
	private RustText messageText;

	[SerializeField]
	private RustButton buttonTemplate;

	[SerializeField]
	private RustButton[] buttons;
}
