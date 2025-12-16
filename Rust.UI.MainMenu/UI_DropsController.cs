using UnityEngine;

namespace Rust.UI.MainMenu;

public class UI_DropsController : FacepunchBehaviour
{
	[SerializeField]
	private GameObject _dropPrefab;

	[SerializeField]
	private Transform _dropsParent;

	[ClientVar(Saved = true)]
	public static bool show_placeholder_drop_data;

	private const int MAX_DROPS = 3;
}
