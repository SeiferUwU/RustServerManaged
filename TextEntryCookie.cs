using Rust;
using UnityEngine;
using UnityEngine.UI;

public class TextEntryCookie : MonoBehaviour
{
	public InputField control => GetComponent<InputField>();

	private void OnEnable()
	{
		string text = PlayerPrefs.GetString("TextEntryCookie_" + base.name);
		if (!string.IsNullOrEmpty(text))
		{
			control.text = text;
		}
		control.onValueChanged.Invoke(control.text);
	}

	private void OnDisable()
	{
		if (!Rust.Application.isQuitting)
		{
			PlayerPrefs.SetString("TextEntryCookie_" + base.name, control.text);
		}
	}
}
