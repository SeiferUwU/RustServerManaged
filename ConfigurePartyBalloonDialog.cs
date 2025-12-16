using Rust.UI;
using UnityEngine;

public class ConfigurePartyBalloonDialog : UIDialog
{
	public RustInput textInput;

	public FlexibleColorPicker balloonColorPicker;

	public FlexibleColorPicker textColorPicker;

	public void OnClickedConfirm()
	{
	}

	public void OnTextChanged(string newText)
	{
	}

	public void OnColourChanged(Color newColour)
	{
	}

	public void OnTextColourChanged(Color newColour)
	{
	}
}
