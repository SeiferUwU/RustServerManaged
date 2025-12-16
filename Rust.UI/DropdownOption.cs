using Rust.UI;
using UnityEngine;
using UnityEngine.UI;

public class DropdownOption : MonoBehaviour
{
	public Image Image;

	public RustText Text;

	[Space]
	public StyleAsset DefaultStyle;

	public StyleAsset SelectedStyle;

	public RustButton Button;

	public void SetSelected(bool selected)
	{
		Button.Styles = (selected ? SelectedStyle : DefaultStyle);
		Button.ApplyStyles();
	}
}
