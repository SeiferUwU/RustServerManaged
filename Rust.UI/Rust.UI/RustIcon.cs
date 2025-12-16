using TMPro;
using UnityEngine;

namespace Rust.UI;

[AddComponentMenu("Rust/UI/RustIcon")]
public class RustIcon : TextMeshProUGUI
{
	[SerializeField]
	private Icons _icon;

	public Icons Icon
	{
		get
		{
			return _icon;
		}
		set
		{
			_icon = value;
			base.text = char.ConvertFromUtf32((int)value);
		}
	}

	public void SetIcon(Icons newIcon)
	{
		Icon = newIcon;
	}
}
