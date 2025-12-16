using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Facepunch;

public static class KeyCombos
{
	public static bool TryParse(ref string name, out List<KeyCode> keys)
	{
		if (string.IsNullOrWhiteSpace(name) || name.Length < 5 || !name.StartsWith("[") || !name.EndsWith("]") || !name.Contains("+"))
		{
			keys = null;
			return false;
		}
		string[] array = name.Substring(1, name.Length - 2).ToLowerInvariant().Split('+');
		List<KeyCode> list = new List<KeyCode>(array.Length);
		List<string> list2 = new List<string>(array.Length);
		string[] array2 = array;
		foreach (string text in array2)
		{
			string text2 = text;
			if (text.Length == 1 && char.IsDigit(text[0]))
			{
				text2 = "alpha" + text;
			}
			if (text2 == "mousewheelup" || text2 == "mousewheeldown")
			{
				list2.Add(text2);
				continue;
			}
			if (!Enum.TryParse<KeyCode>(text2, ignoreCase: true, out var result))
			{
				keys = null;
				return false;
			}
			list.Add(result);
			string text3 = result.ToString().ToLowerInvariant();
			list2.Add(text3.StartsWith("alpha") ? text3.Replace("alpha", "") : text3);
		}
		name = "[" + string.Join("+", list2) + "]";
		keys = list;
		return true;
	}

	public static void RegisterButton(string name, List<KeyCode> keys)
	{
		if (string.IsNullOrWhiteSpace(name) || keys == null)
		{
			return;
		}
		bool usesMouseWheelUp = name.Contains("mousewheelup");
		bool usesMouseWheelDown = name.Contains("mousewheeldown");
		if ((keys.Count <= 1 && !usesMouseWheelUp && !usesMouseWheelDown) || Input.HasButton(name))
		{
			return;
		}
		Input.AddButton(name, KeyCode.None, delegate
		{
			foreach (KeyCode key in keys)
			{
				if (!UnityEngine.Input.GetKey(key))
				{
					return false;
				}
				if (!IsFunctionKey(key) && !KeyBinding.IsOpen && (NeedsKeyboard.AnyActive() || HudMenuInput.AnyActive()))
				{
					return false;
				}
				if (IsMouseButton(key) && NeedsMouseButtons.AnyActive())
				{
					return false;
				}
				if (usesMouseWheelUp && UnityEngine.Input.GetAxis("Mouse ScrollWheel") <= 0f)
				{
					return false;
				}
				if (usesMouseWheelDown && UnityEngine.Input.GetAxis("Mouse ScrollWheel") >= 0f)
				{
					return false;
				}
			}
			return true;
		});
	}

	private static bool IsFunctionKey(KeyCode keyCode)
	{
		if (keyCode >= KeyCode.F1)
		{
			return keyCode <= KeyCode.F15;
		}
		return false;
	}

	private static bool IsMouseButton(KeyCode keyCode)
	{
		if (keyCode >= KeyCode.Mouse0)
		{
			return keyCode <= KeyCode.Mouse6;
		}
		return false;
	}

	public static string FormatHeldKeys(IReadOnlyList<string> heldKeys)
	{
		if (heldKeys == null || heldKeys.Count == 0)
		{
			return null;
		}
		if (heldKeys.Count > 1)
		{
			IEnumerable<string> values = from k in heldKeys
				select k.ToLowerInvariant() into k
				select (!k.StartsWith("alpha")) ? k : k.Replace("alpha", "");
			return "[" + string.Join("+", values) + "]";
		}
		return heldKeys[0];
	}
}
