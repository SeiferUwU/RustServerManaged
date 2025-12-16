using Rust.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Rust.Workshop.Editor;

public class FloatRow : MaterialRow
{
	public Button Reset;

	public RustSlider Slider;

	private float Default;

	public bool IsDefault => Default == Slider.Value;

	public void Update()
	{
		Reset.gameObject.SetActive(!IsDefault);
	}

	public override void Read(Material source, Material def)
	{
		float value = source.GetFloat(ParamName);
		Slider.Value = value;
		Default = def.GetFloat(ParamName);
		base.Editor.SetFloat(ParamName, value);
	}

	public void ResetToDefault()
	{
		Slider.Value = Default;
		base.Editor.SetFloat(ParamName, Default);
	}

	public void OnChanged()
	{
		base.Editor.SetFloat(ParamName, Slider.Value);
	}
}
