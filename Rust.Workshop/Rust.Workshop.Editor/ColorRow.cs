using Rust.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Rust.Workshop.Editor;

public class ColorRow : MaterialRow
{
	public Button Reset;

	public RustSlider Red;

	public RustSlider Green;

	public RustSlider Blue;

	public GameObject ColourPickerGameObject;

	private Color defaultColour;

	private IWorkshopColourPicker colourPicker;

	private TimeSince disableSlidersOnChanged;

	private TimeSince disableColourPickerOnChanged;

	public bool IsDefault
	{
		get
		{
			if (defaultColour.r == Red.Value && Green.Value == defaultColour.g)
			{
				return Blue.Value == defaultColour.b;
			}
			return false;
		}
	}

	public void Update()
	{
		Reset.gameObject.SetActive(!IsDefault);
	}

	public override void Read(Material source, Material def)
	{
		Color color = source.GetColor(ParamName);
		Red.Value = color.r * 255f;
		Green.Value = color.g * 255f;
		Blue.Value = color.b * 255f;
		defaultColour = def.GetColor(ParamName);
		base.Editor.SetColor(ParamName, color);
	}

	public void ResetToDefault()
	{
		Red.Value = defaultColour.r * 255f;
		Green.Value = defaultColour.g * 255f;
		Blue.Value = defaultColour.b * 255f;
		base.Editor.SetColor(ParamName, defaultColour);
	}

	public void OnChanged()
	{
		if (!((float)disableSlidersOnChanged < 0.1f))
		{
			disableColourPickerOnChanged = 0f;
			Color val = new Color(Red.Value / 255f, Green.Value / 255f, Blue.Value / 255f);
			base.Editor.SetColor(ParamName, val);
		}
	}
}
