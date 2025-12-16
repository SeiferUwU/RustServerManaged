using System;
using System.Collections.Generic;
using System.Linq;
using Facepunch;
using RTLTMPro;
using Rust.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Rust.UI;

[AddComponentMenu("Rust/UI/RustText")]
public class RustText : TextMeshProUGUI, ILocalize
{
	public bool IsLocalized;

	[LocalizationToken]
	public string Token;

	[NonSerialized]
	public Translate.Phrase Phrase;

	[FormerlySerializedAs("AutoSizeContainer")]
	public bool AutoSetWidth;

	public bool AutoSetHeight;

	public bool AutoSizeParent;

	public float MinWidth = 30f;

	public float MaxWidth = float.PositiveInfinity;

	public float MinHeight;

	public float MaxHeight = float.PositiveInfinity;

	private bool touched;

	private object[] localizationArguments;

	private static readonly FastStringBuilder inputBuilder = new FastStringBuilder(2048);

	private bool invertedAlignment;

	private float cachedWordSpacing;

	private float cachedCharacterSpacing;

	private bool spacingOverridden;

	public string LanguageToken => Token;

	public string LanguageEnglish => Translate.Get(Token);

	protected override void Awake()
	{
		base.Awake();
		if (!touched)
		{
			UpdateLocalizedText();
		}
	}

	public void SetText(string str)
	{
		SetText(str, localized: false);
	}

	public void SetText(string str, bool localized, bool forceRTLFormatting = false)
	{
		touched = true;
		IsLocalized = localized;
		if (localized || forceRTLFormatting)
		{
			str = FormatLocalizedText(str, forceRTLFormatting);
			base.text = str;
			DoAutoSize();
			FormatAlignmentAndSpacing();
		}
		else
		{
			base.text = str;
			DoAutoSize();
		}
	}

	public void SetPhrase(Translate.Phrase phrase, params object[] args)
	{
		if (phrase == null)
		{
			SetText(string.Empty);
		}
		else if (phrase.IsValid())
		{
			if (UnityEngine.Application.isPlaying)
			{
				Phrase = phrase;
			}
			IsLocalized = true;
			Token = phrase.token;
			if (args != null && args.Length != 0)
			{
				SetPhraseArguments(args);
			}
			else
			{
				UpdateLocalizedText();
			}
		}
	}

	public void SetPhraseArguments(params object[] args)
	{
		localizationArguments = args;
		UpdateLocalizedText();
	}

	public void SetToken(string token)
	{
		SetPhrase(new Translate.Phrase(token, Translate.Get(token)));
	}

	public virtual void DoAutoSize()
	{
		if (base.font == null)
		{
			Debug.LogWarning("null font: " + base.gameObject.name, base.gameObject);
			return;
		}
		if (AutoSetWidth)
		{
			RectTransform trans = (RectTransform)base.transform;
			Vector2 preferredValues = GetPreferredValues(base.text, float.PositiveInfinity, float.PositiveInfinity);
			preferredValues.x = Mathf.Clamp(preferredValues.x, MinWidth, MaxWidth);
			trans.SetWidth(preferredValues.x + m_currentFontAsset.normalSpacingOffset);
			trans.DoAutoLayout();
			if (AutoSizeParent)
			{
				RectTransform trans2 = (RectTransform)base.transform.parent;
				trans2.SetWidth(preferredValues.x);
				trans2.DoAutoLayout();
			}
		}
		if (AutoSetHeight)
		{
			RectTransform obj = (RectTransform)base.transform;
			Rect rect = obj.rect;
			Vector2 preferredValues2 = GetPreferredValues(base.text, rect.width - (m_margin.x + m_margin.z), float.PositiveInfinity);
			preferredValues2.y = Mathf.Clamp(preferredValues2.y, MinHeight, MaxHeight);
			obj.SetHeight(preferredValues2.y);
			obj.DoAutoLayout();
			if (AutoSizeParent)
			{
				RectTransform trans3 = (RectTransform)base.transform.parent;
				trans3.SetHeight(preferredValues2.y);
				trans3.DoAutoLayout();
			}
		}
	}

	public override void Rebuild(CanvasUpdate update)
	{
		if (!(this == null))
		{
			base.Rebuild(update);
		}
	}

	private string GetLocalizedText(bool forceEnglish = false)
	{
		if (Phrase != null)
		{
			if (localizationArguments != null)
			{
				return string.Format(Phrase.translated, localizationArguments);
			}
			return Phrase.translated;
		}
		string text = Translate.Get(Token, null, forceEnglish);
		if (string.IsNullOrEmpty(text))
		{
			return string.Empty;
		}
		if (localizationArguments != null)
		{
			text = string.Format(text, localizationArguments);
		}
		return text;
	}

	private void UpdateLocalizedText(bool forceEnglish = false)
	{
		if (IsLocalized)
		{
			string localizedText = GetLocalizedText(forceEnglish);
			base.text = FormatLocalizedText(localizedText);
			DoAutoSize();
			FormatAlignmentAndSpacing();
		}
	}

	public static void OnLanguageChanged()
	{
		RustText[] array = Resources.FindObjectsOfTypeAll<RustText>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].UpdateLocalizedText();
		}
	}

	public string FormatLocalizedText(string str, bool force = false)
	{
		if (Translate.CurrentLanguageIsRTL() || force)
		{
			base.isRightToLeftText = TextUtils.ContainsRTLInput(str);
			if (base.isRightToLeftText)
			{
				inputBuilder.Clear();
				RTLSupport.FixRTL(str, inputBuilder, farsi: false);
				inputBuilder.Reverse();
				str = inputBuilder.ToString();
				base.text = str;
			}
			return str;
		}
		base.isRightToLeftText = false;
		return str;
	}

	private void FormatAlignmentAndSpacing()
	{
		bool num = TextUtils.ContainsRTLInput(base.text);
		bool flag = Translate.CurrentLanguageIsRTL();
		bool num2 = num && flag;
		if (num2)
		{
			int lineCount = base.textInfo.lineCount;
			_HorizontalAlignmentOptions horizontalAlignment = GetHorizontalAlignment();
			if (lineCount >= 3 && (horizontalAlignment == _HorizontalAlignmentOptions.Left || horizontalAlignment == _HorizontalAlignmentOptions.Justified))
			{
				SetHorizontalAlignment(_HorizontalAlignmentOptions.Right);
				invertedAlignment = true;
			}
		}
		else if (invertedAlignment)
		{
			SetHorizontalAlignment(_HorizontalAlignmentOptions.Left);
			invertedAlignment = false;
		}
		if (num2)
		{
			if (!spacingOverridden && (base.wordSpacing != 0f || base.characterSpacing != 0f))
			{
				cachedWordSpacing = base.wordSpacing;
				cachedCharacterSpacing = base.characterSpacing;
				base.wordSpacing = 0f;
				base.characterSpacing = 0f;
				spacingOverridden = true;
			}
		}
		else if (spacingOverridden)
		{
			base.wordSpacing = cachedWordSpacing;
			base.characterSpacing = cachedCharacterSpacing;
			spacingOverridden = false;
		}
	}

	public _HorizontalAlignmentOptions GetHorizontalAlignment()
	{
		return (_HorizontalAlignmentOptions)(base.alignment & (TextAlignmentOptions)63);
	}

	public _VerticalAlignmentOptions GetVerticalAligment()
	{
		return (_VerticalAlignmentOptions)(base.alignment & (TextAlignmentOptions)65280);
	}

	public void SetHorizontalAlignment(_HorizontalAlignmentOptions option)
	{
		base.alignment = (TextAlignmentOptions)((int)option | (int)GetVerticalAligment());
	}

	public void SetVerticalAlignment(_VerticalAlignmentOptions option)
	{
		base.alignment = (TextAlignmentOptions)((int)option | (int)GetHorizontalAlignment());
	}

	public void HighlightText(string inputText, string query, string colorHex = "#ffff0040", bool isLocalized = false)
	{
		string markedString = GetMarkedString(inputText, query, colorHex);
		SetText(markedString, isLocalized);
	}

	public static string GetMarkedString(string input, string query, string colorHex = "#ffff0040")
	{
		if (string.IsNullOrWhiteSpace(query))
		{
			return input;
		}
		IEnumerable<string> enumerable = query.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Distinct(StringComparer.OrdinalIgnoreCase);
		List<(int, int)> obj = Pool.GetList<(int, int)>();
		foreach (string item3 in enumerable)
		{
			int startIndex = 0;
			while (true)
			{
				int num = input.IndexOf(item3, startIndex, StringComparison.OrdinalIgnoreCase);
				if (num < 0)
				{
					break;
				}
				if (!IsInsideTag(input, num))
				{
					obj.Add((num, item3.Length));
				}
				startIndex = num + item3.Length;
			}
		}
		foreach (var item4 in obj.OrderByDescending<(int, int), int>(((int startIndex, int length) m) => m.startIndex))
		{
			int item = item4.Item1;
			int item2 = item4.Item2;
			string text = input.Substring(0, item);
			string text2 = input.Substring(item, item2);
			string text3 = input.Substring(item + item2);
			input = text + "<mark=" + colorHex + ">" + text2 + "</mark>" + text3;
		}
		Pool.FreeUnmanaged(ref obj);
		return input;
	}

	public static bool IsInsideTag(string text, int index)
	{
		int num = text.LastIndexOf('<', index);
		int num2 = text.LastIndexOf('>', index);
		return num > num2;
	}
}
