using Development.Attributes;
using Rust.UI.MainMenu;
using UnityEngine;

[ResetStaticFields]
public static class MainMenu
{
	private static Canvas _canvas;

	public static Canvas Canvas
	{
		get
		{
			if (SingletonComponent<UI_MenuManager>.Instance == null)
			{
				return null;
			}
			if (_canvas == null)
			{
				_canvas = SingletonComponent<UI_MenuManager>.Instance.GetComponent<Canvas>();
			}
			return _canvas;
		}
	}

	public static bool IsOpen()
	{
		return UI_MenuManager.IsOpen;
	}
}
