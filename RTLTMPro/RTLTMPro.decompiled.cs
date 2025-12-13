using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using TMPro;
using UnityEngine;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyVersion("0.0.0.0")]
[CompilerGenerated]
[EditorBrowsable(EditorBrowsableState.Never)]
[GeneratedCode("Unity.MonoScriptGenerator.MonoScriptInfoGenerator", null)]
internal class UnitySourceGeneratedAssemblyMonoScriptTypes_v1
{
	private struct MonoScriptData
	{
		public byte[] FilePathsData;

		public byte[] TypesData;

		public int TotalTypes;

		public int TotalFiles;

		public bool IsEditorOnly;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static MonoScriptData Get()
	{
		return new MonoScriptData
		{
			FilePathsData = new byte[1040]
			{
				0, 0, 0, 1, 0, 0, 0, 77, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 82, 117, 115, 116, 46, 85,
				73, 92, 65, 114, 97, 98, 105, 99, 83, 117,
				112, 112, 111, 114, 116, 92, 82, 84, 76, 84,
				77, 80, 114, 111, 92, 83, 99, 114, 105, 112,
				116, 115, 92, 82, 117, 110, 116, 105, 109, 101,
				92, 67, 104, 97, 114, 51, 50, 85, 116, 105,
				108, 115, 46, 99, 115, 0, 0, 0, 1, 0,
				0, 0, 83, 92, 65, 115, 115, 101, 116, 115,
				92, 80, 108, 117, 103, 105, 110, 115, 92, 82,
				117, 115, 116, 46, 85, 73, 92, 65, 114, 97,
				98, 105, 99, 83, 117, 112, 112, 111, 114, 116,
				92, 82, 84, 76, 84, 77, 80, 114, 111, 92,
				83, 99, 114, 105, 112, 116, 115, 92, 82, 117,
				110, 116, 105, 109, 101, 92, 70, 97, 115, 116,
				83, 116, 114, 105, 110, 103, 66, 117, 105, 108,
				100, 101, 114, 46, 99, 115, 0, 0, 0, 1,
				0, 0, 0, 76, 92, 65, 115, 115, 101, 116,
				115, 92, 80, 108, 117, 103, 105, 110, 115, 92,
				82, 117, 115, 116, 46, 85, 73, 92, 65, 114,
				97, 98, 105, 99, 83, 117, 112, 112, 111, 114,
				116, 92, 82, 84, 76, 84, 77, 80, 114, 111,
				92, 83, 99, 114, 105, 112, 116, 115, 92, 82,
				117, 110, 116, 105, 109, 101, 92, 71, 108, 121,
				112, 104, 70, 105, 120, 101, 114, 46, 99, 115,
				0, 0, 0, 1, 0, 0, 0, 76, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 82, 117, 115, 116, 46, 85,
				73, 92, 65, 114, 97, 98, 105, 99, 83, 117,
				112, 112, 111, 114, 116, 92, 82, 84, 76, 84,
				77, 80, 114, 111, 92, 83, 99, 114, 105, 112,
				116, 115, 92, 82, 117, 110, 116, 105, 109, 101,
				92, 71, 108, 121, 112, 104, 84, 97, 98, 108,
				101, 46, 99, 115, 0, 0, 0, 1, 0, 0,
				0, 79, 92, 65, 115, 115, 101, 116, 115, 92,
				80, 108, 117, 103, 105, 110, 115, 92, 82, 117,
				115, 116, 46, 85, 73, 92, 65, 114, 97, 98,
				105, 99, 83, 117, 112, 112, 111, 114, 116, 92,
				82, 84, 76, 84, 77, 80, 114, 111, 92, 83,
				99, 114, 105, 112, 116, 115, 92, 82, 117, 110,
				116, 105, 109, 101, 92, 76, 105, 103, 97, 116,
				117, 114, 101, 70, 105, 120, 101, 114, 46, 99,
				115, 0, 0, 0, 2, 0, 0, 0, 79, 92,
				65, 115, 115, 101, 116, 115, 92, 80, 108, 117,
				103, 105, 110, 115, 92, 82, 117, 115, 116, 46,
				85, 73, 92, 65, 114, 97, 98, 105, 99, 83,
				117, 112, 112, 111, 114, 116, 92, 82, 84, 76,
				84, 77, 80, 114, 111, 92, 83, 99, 114, 105,
				112, 116, 115, 92, 82, 117, 110, 116, 105, 109,
				101, 92, 82, 105, 99, 104, 84, 101, 120, 116,
				70, 105, 120, 101, 114, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 76, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 82, 117, 115, 116, 46, 85, 73, 92,
				65, 114, 97, 98, 105, 99, 83, 117, 112, 112,
				111, 114, 116, 92, 82, 84, 76, 84, 77, 80,
				114, 111, 92, 83, 99, 114, 105, 112, 116, 115,
				92, 82, 117, 110, 116, 105, 109, 101, 92, 82,
				84, 76, 83, 117, 112, 112, 111, 114, 116, 46,
				99, 115, 0, 0, 0, 1, 0, 0, 0, 80,
				92, 65, 115, 115, 101, 116, 115, 92, 80, 108,
				117, 103, 105, 110, 115, 92, 82, 117, 115, 116,
				46, 85, 73, 92, 65, 114, 97, 98, 105, 99,
				83, 117, 112, 112, 111, 114, 116, 92, 82, 84,
				76, 84, 77, 80, 114, 111, 92, 83, 99, 114,
				105, 112, 116, 115, 92, 82, 117, 110, 116, 105,
				109, 101, 92, 82, 84, 76, 84, 101, 120, 116,
				77, 101, 115, 104, 80, 114, 111, 46, 99, 115,
				0, 0, 0, 1, 0, 0, 0, 82, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 82, 117, 115, 116, 46, 85,
				73, 92, 65, 114, 97, 98, 105, 99, 83, 117,
				112, 112, 111, 114, 116, 92, 82, 84, 76, 84,
				77, 80, 114, 111, 92, 83, 99, 114, 105, 112,
				116, 115, 92, 82, 117, 110, 116, 105, 109, 101,
				92, 82, 84, 76, 84, 101, 120, 116, 77, 101,
				115, 104, 80, 114, 111, 51, 68, 46, 99, 115,
				0, 0, 0, 1, 0, 0, 0, 79, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 82, 117, 115, 116, 46, 85,
				73, 92, 65, 114, 97, 98, 105, 99, 83, 117,
				112, 112, 111, 114, 116, 92, 82, 84, 76, 84,
				77, 80, 114, 111, 92, 83, 99, 114, 105, 112,
				116, 115, 92, 82, 117, 110, 116, 105, 109, 101,
				92, 84, 97, 115, 104, 107, 101, 101, 108, 70,
				105, 120, 101, 114, 46, 99, 115, 0, 0, 0,
				1, 0, 0, 0, 82, 92, 65, 115, 115, 101,
				116, 115, 92, 80, 108, 117, 103, 105, 110, 115,
				92, 82, 117, 115, 116, 46, 85, 73, 92, 65,
				114, 97, 98, 105, 99, 83, 117, 112, 112, 111,
				114, 116, 92, 82, 84, 76, 84, 77, 80, 114,
				111, 92, 83, 99, 114, 105, 112, 116, 115, 92,
				82, 117, 110, 116, 105, 109, 101, 92, 84, 97,
				115, 104, 107, 101, 101, 108, 76, 111, 99, 97,
				116, 105, 111, 110, 46, 99, 115, 0, 0, 0,
				1, 0, 0, 0, 75, 92, 65, 115, 115, 101,
				116, 115, 92, 80, 108, 117, 103, 105, 110, 115,
				92, 82, 117, 115, 116, 46, 85, 73, 92, 65,
				114, 97, 98, 105, 99, 83, 117, 112, 112, 111,
				114, 116, 92, 82, 84, 76, 84, 77, 80, 114,
				111, 92, 83, 99, 114, 105, 112, 116, 115, 92,
				82, 117, 110, 116, 105, 109, 101, 92, 84, 101,
				120, 116, 85, 116, 105, 108, 115, 46, 99, 115
			},
			TypesData = new byte[351]
			{
				0, 0, 0, 0, 20, 82, 84, 76, 84, 77,
				80, 114, 111, 124, 67, 104, 97, 114, 51, 50,
				85, 116, 105, 108, 115, 0, 0, 0, 0, 26,
				82, 84, 76, 84, 77, 80, 114, 111, 124, 70,
				97, 115, 116, 83, 116, 114, 105, 110, 103, 66,
				117, 105, 108, 100, 101, 114, 0, 0, 0, 0,
				19, 82, 84, 76, 84, 77, 80, 114, 111, 124,
				71, 108, 121, 112, 104, 70, 105, 120, 101, 114,
				0, 0, 0, 0, 19, 82, 84, 76, 84, 77,
				80, 114, 111, 124, 71, 108, 121, 112, 104, 84,
				97, 98, 108, 101, 0, 0, 0, 0, 22, 82,
				84, 76, 84, 77, 80, 114, 111, 124, 76, 105,
				103, 97, 116, 117, 114, 101, 70, 105, 120, 101,
				114, 0, 0, 0, 0, 22, 82, 84, 76, 84,
				77, 80, 114, 111, 124, 82, 105, 99, 104, 84,
				101, 120, 116, 70, 105, 120, 101, 114, 0, 0,
				0, 0, 26, 82, 84, 76, 84, 77, 80, 114,
				111, 46, 82, 105, 99, 104, 84, 101, 120, 116,
				70, 105, 120, 101, 114, 124, 84, 97, 103, 0,
				0, 0, 0, 19, 82, 84, 76, 84, 77, 80,
				114, 111, 124, 82, 84, 76, 83, 117, 112, 112,
				111, 114, 116, 0, 0, 0, 0, 23, 82, 84,
				76, 84, 77, 80, 114, 111, 124, 82, 84, 76,
				84, 101, 120, 116, 77, 101, 115, 104, 80, 114,
				111, 0, 0, 0, 0, 25, 82, 84, 76, 84,
				77, 80, 114, 111, 124, 82, 84, 76, 84, 101,
				120, 116, 77, 101, 115, 104, 80, 114, 111, 51,
				68, 0, 0, 0, 0, 22, 82, 84, 76, 84,
				77, 80, 114, 111, 124, 84, 97, 115, 104, 107,
				101, 101, 108, 70, 105, 120, 101, 114, 0, 0,
				0, 0, 25, 82, 84, 76, 84, 77, 80, 114,
				111, 124, 84, 97, 115, 104, 107, 101, 101, 108,
				76, 111, 99, 97, 116, 105, 111, 110, 0, 0,
				0, 0, 18, 82, 84, 76, 84, 77, 80, 114,
				111, 124, 84, 101, 120, 116, 85, 116, 105, 108,
				115
			},
			TotalFiles = 12,
			TotalTypes = 13,
			IsEditorOnly = false
		};
	}
}
namespace RTLTMPro;

public static class Char32Utils
{
	public static bool IsUnicode16Char(int ch)
	{
		return ch < 65535;
	}

	public static bool IsRTLCharacter(int ch)
	{
		if (!IsUnicode16Char(ch))
		{
			return false;
		}
		return TextUtils.IsRTLCharacter((char)ch);
	}

	public static bool IsEnglishLetter(int ch)
	{
		if (!IsUnicode16Char(ch))
		{
			return false;
		}
		return TextUtils.IsEnglishLetter((char)ch);
	}

	public static bool IsNumber(int ch, bool preserveNumbers, bool farsi)
	{
		if (!IsUnicode16Char(ch))
		{
			return false;
		}
		return TextUtils.IsNumber((char)ch, preserveNumbers, farsi);
	}

	public static bool IsSymbol(int ch)
	{
		if (!IsUnicode16Char(ch))
		{
			return false;
		}
		return char.IsSymbol((char)ch);
	}

	public static bool IsLetter(int ch)
	{
		if (!IsUnicode16Char(ch))
		{
			return false;
		}
		return char.IsLetter((char)ch);
	}

	public static bool IsPunctuation(int ch)
	{
		if (!IsUnicode16Char(ch))
		{
			return false;
		}
		return char.IsPunctuation((char)ch);
	}

	public static bool IsWhiteSpace(int ch)
	{
		if (!IsUnicode16Char(ch))
		{
			return false;
		}
		return char.IsWhiteSpace((char)ch);
	}
}
public class FastStringBuilder
{
	private int length;

	private int[] array;

	private int capacity;

	public int Length
	{
		get
		{
			return length;
		}
		set
		{
			if (value <= length)
			{
				length = value;
			}
		}
	}

	public FastStringBuilder(int capacity)
	{
		if (capacity < 0)
		{
			throw new ArgumentOutOfRangeException("capacity");
		}
		this.capacity = capacity;
		array = new int[capacity];
	}

	public FastStringBuilder(string text)
		: this(text, text.Length)
	{
	}

	public FastStringBuilder(string text, int capacity)
		: this(capacity)
	{
		SetValue(text);
	}

	public static implicit operator string(FastStringBuilder x)
	{
		return x.ToString();
	}

	public static implicit operator FastStringBuilder(string x)
	{
		return new FastStringBuilder(x);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public int Get(int index)
	{
		return array[index];
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Set(int index, int ch)
	{
		array[index] = ch;
	}

	public void SetValue(string text)
	{
		int num = 0;
		length = text.Length;
		EnsureCapacity(length, keepValues: false);
		for (int i = 0; i < text.Length; i++)
		{
			int num2 = char.ConvertToUtf32(text, i);
			if (num2 > 65535)
			{
				i++;
			}
			array[num++] = num2;
		}
		length = num;
	}

	public void SetValue(FastStringBuilder other)
	{
		EnsureCapacity(other.length, keepValues: false);
		Copy(other.array, array);
		length = other.length;
	}

	public void Append(int ch)
	{
		length++;
		if (capacity < length)
		{
			EnsureCapacity(length, keepValues: true);
		}
		array[length - 1] = ch;
	}

	public void Append(char ch)
	{
		length++;
		if (capacity < length)
		{
			EnsureCapacity(length, keepValues: true);
		}
		array[length - 1] = ch;
	}

	public void Insert(int pos, FastStringBuilder str, int offset, int count)
	{
		if (str == this)
		{
			throw new InvalidOperationException("You cannot pass the same string builder to insert");
		}
		if (count != 0)
		{
			length += count;
			EnsureCapacity(length, keepValues: true);
			for (int num = length - count - 1; num >= pos; num--)
			{
				array[num + count] = array[num];
			}
			for (int i = 0; i < count; i++)
			{
				array[pos + i] = str.array[offset + i];
			}
		}
	}

	public void Insert(int pos, FastStringBuilder str)
	{
		Insert(pos, str, 0, str.length);
	}

	public void Insert(int pos, int ch)
	{
		length++;
		EnsureCapacity(length, keepValues: true);
		for (int num = length - 2; num >= pos; num--)
		{
			array[num + 1] = array[num];
		}
		array[pos] = ch;
	}

	public void RemoveAll(int character)
	{
		int num = 0;
		for (int i = 0; i < length; i++)
		{
			if (array[i] != character)
			{
				array[num] = array[i];
				num++;
			}
		}
		length = num;
	}

	public void Remove(int start, int length)
	{
		for (int i = start; i < this.length - length; i++)
		{
			array[i] = array[i + length];
		}
		this.length -= length;
	}

	public void Reverse(int startIndex, int length)
	{
		for (int i = 0; i < length / 2; i++)
		{
			int num = startIndex + i;
			int num2 = startIndex + length - i - 1;
			int num3 = array[num];
			int num4 = array[num2];
			array[num] = num4;
			array[num2] = num3;
		}
	}

	public void Reverse()
	{
		Reverse(0, length);
	}

	public void Substring(FastStringBuilder output, int start, int length)
	{
		output.length = 0;
		for (int i = 0; i < length; i++)
		{
			output.Append(array[start + i]);
		}
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < length; i++)
		{
			stringBuilder.Append(char.ConvertFromUtf32(array[i]));
		}
		return stringBuilder.ToString();
	}

	public string ToDebugString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < length; i++)
		{
			stringBuilder.Append("\\");
			stringBuilder.Append(array[i].ToString("X"));
		}
		return stringBuilder.ToString();
	}

	public void Replace(int oldChar, int newChar)
	{
		for (int i = 0; i < length; i++)
		{
			if (array[i] == oldChar)
			{
				array[i] = newChar;
			}
		}
	}

	public void Replace(FastStringBuilder oldStr, FastStringBuilder newStr)
	{
		for (int i = 0; i < length; i++)
		{
			bool flag = true;
			for (int j = 0; j < oldStr.Length; j++)
			{
				if (array[i + j] != oldStr.Get(j))
				{
					flag = false;
					break;
				}
			}
			if (!flag)
			{
				continue;
			}
			if (oldStr.Length == newStr.Length)
			{
				for (int k = 0; k < oldStr.Length; k++)
				{
					array[i + k] = newStr.Get(k);
				}
			}
			else if (oldStr.Length < newStr.Length)
			{
				int num = newStr.Length - oldStr.Length;
				length += num;
				EnsureCapacity(length, keepValues: true);
				for (int num2 = length - num - 1; num2 >= i + oldStr.Length; num2--)
				{
					array[num2 + num] = array[num2];
				}
				for (int l = 0; l < newStr.Length; l++)
				{
					array[i + l] = newStr.Get(l);
				}
			}
			else
			{
				int num3 = oldStr.Length - newStr.Length;
				for (int m = i + num3; m < length - num3; m++)
				{
					array[m] = array[m + num3];
				}
				for (int n = 0; n < newStr.Length; n++)
				{
					array[i + n] = newStr.Get(n);
				}
				length -= num3;
			}
			i += newStr.Length;
		}
	}

	public void Clear()
	{
		length = 0;
	}

	private void EnsureCapacity(int cap, bool keepValues)
	{
		if (capacity < cap)
		{
			if (capacity == 0)
			{
				capacity = 1;
			}
			while (capacity < cap)
			{
				capacity *= 2;
			}
			if (keepValues)
			{
				int[] dst = new int[capacity];
				Copy(array, dst);
				array = dst;
			}
			else
			{
				array = new int[capacity];
			}
		}
	}

	private static void Copy(int[] src, int[] dst)
	{
		for (int i = 0; i < src.Length; i++)
		{
			dst[i] = src[i];
		}
	}
}
public static class GlyphFixer
{
	public static Dictionary<char, char> EnglishToFarsiNumberMap = new Dictionary<char, char>
	{
		['0'] = '۰',
		['1'] = '۱',
		['2'] = '۲',
		['3'] = '۳',
		['4'] = '۴',
		['5'] = '۵',
		['6'] = '۶',
		['7'] = '۷',
		['8'] = '۸',
		['9'] = '۹'
	};

	public static Dictionary<char, char> EnglishToHinduNumberMap = new Dictionary<char, char>
	{
		['0'] = '٠',
		['1'] = '١',
		['2'] = '٢',
		['3'] = '٣',
		['4'] = '٤',
		['5'] = '٥',
		['6'] = '٦',
		['7'] = '٧',
		['8'] = '٨',
		['9'] = '٩'
	};

	public static void Fix(FastStringBuilder input, FastStringBuilder output, bool preserveNumbers, bool farsi, bool fixTextTags)
	{
		FixYah(input, farsi);
		output.SetValue(input);
		for (int i = 0; i < input.Length; i++)
		{
			bool flag = false;
			int num = input.Get(i);
			if (num == 1604 && i < input.Length - 1)
			{
				flag = HandleSpecialLam(input, output, i);
				if (flag)
				{
					num = output.Get(i);
				}
			}
			if (num == 1600 || num == 8204)
			{
				continue;
			}
			if (num < 65535 && TextUtils.IsGlyphFixedArabicCharacter((char)num))
			{
				char c = GlyphTable.Convert((char)num);
				if (IsMiddleLetter(input, i))
				{
					output.Set(i, (ushort)(c + 3));
				}
				else if (IsFinishingLetter(input, i))
				{
					output.Set(i, (ushort)(c + 1));
				}
				else if (IsLeadingLetter(input, i))
				{
					output.Set(i, (ushort)(c + 2));
				}
				else
				{
					output.Set(i, c);
				}
			}
			if (flag)
			{
				i++;
			}
		}
		if (!preserveNumbers)
		{
			if (fixTextTags)
			{
				FixNumbersOutsideOfTags(output, farsi);
			}
			else
			{
				FixNumbers(output, farsi);
			}
		}
	}

	public static void FixYah(FastStringBuilder text, bool farsi)
	{
		for (int i = 0; i < text.Length; i++)
		{
			if (farsi && text.Get(i) == 1610)
			{
				text.Set(i, 1740);
			}
			else if (!farsi && text.Get(i) == 1740)
			{
				text.Set(i, 1610);
			}
		}
	}

	private static bool HandleSpecialLam(FastStringBuilder input, FastStringBuilder output, int i)
	{
		bool flag;
		switch (input.Get(i + 1))
		{
		case 1573:
			output.Set(i, 65271);
			flag = true;
			break;
		case 1575:
			output.Set(i, 65273);
			flag = true;
			break;
		case 1571:
			output.Set(i, 65269);
			flag = true;
			break;
		case 1570:
			output.Set(i, 65267);
			flag = true;
			break;
		default:
			flag = false;
			break;
		}
		if (flag)
		{
			output.Set(i + 1, 65535);
		}
		return flag;
	}

	public static void FixNumbers(FastStringBuilder text, bool farsi)
	{
		text.Replace(48, farsi ? 1776 : 1632);
		text.Replace(49, farsi ? 1777 : 1633);
		text.Replace(50, farsi ? 1778 : 1634);
		text.Replace(51, farsi ? 1779 : 1635);
		text.Replace(52, farsi ? 1780 : 1636);
		text.Replace(53, farsi ? 1781 : 1637);
		text.Replace(54, farsi ? 1782 : 1638);
		text.Replace(55, farsi ? 1783 : 1639);
		text.Replace(56, farsi ? 1784 : 1640);
		text.Replace(57, farsi ? 1785 : 1641);
	}

	public static void FixNumbersOutsideOfTags(FastStringBuilder text, bool farsi)
	{
		HashSet<char> hashSet = new HashSet<char>(EnglishToFarsiNumberMap.Keys);
		for (int i = 0; i < text.Length; i++)
		{
			int num = text.Get(i);
			if (num == 60)
			{
				bool flag = false;
				for (int j = i + 1; j < text.Length; j++)
				{
					int num2 = text.Get(j);
					if (j != i + 1 || num2 != 32)
					{
						switch (num2)
						{
						case 62:
							i = j;
							flag = true;
							break;
						default:
							continue;
						case 60:
							break;
						}
					}
					break;
				}
				if (flag)
				{
					continue;
				}
			}
			if (hashSet.Contains((char)num))
			{
				text.Set(i, farsi ? EnglishToFarsiNumberMap[(char)num] : EnglishToHinduNumberMap[(char)num]);
			}
		}
	}

	private static bool IsLeadingLetter(FastStringBuilder letters, int index)
	{
		int num = letters.Get(index);
		int num2 = 0;
		if (index != 0)
		{
			num2 = letters.Get(index - 1);
		}
		int num3 = 0;
		if (index < letters.Length - 1)
		{
			num3 = letters.Get(index + 1);
		}
		bool num4 = index == 0 || (num2 < 65535 && !TextUtils.IsGlyphFixedArabicCharacter((char)num2)) || num2 == 1569 || num2 == 1570 || num2 == 1571 || num2 == 1573 || num2 == 1572 || num2 == 1575 || num2 == 1583 || num2 == 1584 || num2 == 1585 || num2 == 1586 || num2 == 1688 || num2 == 1608 || num2 == 65153 || num2 == 65155 || num2 == 65159 || num2 == 65157 || num2 == 65165 || num2 == 65152 || num2 == 65193 || num2 == 65195 || num2 == 65197 || num2 == 65199 || num2 == 64394 || num2 == 65261 || num2 == 8204;
		bool flag = num != 32 && num != 1569 && num != 1571 && num != 1573 && num != 1570 && num != 1572 && num != 1575 && num != 1583 && num != 1584 && num != 1585 && num != 1586 && num != 1688 && num != 1608 && num != 8204;
		bool flag2 = index < letters.Length - 1 && num3 < 65535 && TextUtils.IsGlyphFixedArabicCharacter((char)num3) && num3 != 1569 && num3 != 8204;
		return num4 && flag && flag2;
	}

	private static bool IsFinishingLetter(FastStringBuilder letters, int index)
	{
		int num = letters.Get(index);
		int num2 = 0;
		if (index != 0)
		{
			num2 = letters.Get(index - 1);
		}
		bool num3 = index != 0 && num2 != 32 && num2 != 1569 && num2 != 1570 && num2 != 1571 && num2 != 1573 && num2 != 1572 && num2 != 1575 && num2 != 1583 && num2 != 1584 && num2 != 1585 && num2 != 1586 && num2 != 1688 && num2 != 1608 && num2 != 65152 && num2 != 65153 && num2 != 65155 && num2 != 65159 && num2 != 65157 && num2 != 65165 && num2 != 65193 && num2 != 65195 && num2 != 65197 && num2 != 65199 && num2 != 64394 && num2 != 65261 && num2 != 8204 && num2 < 65535 && TextUtils.IsGlyphFixedArabicCharacter((char)num2);
		bool flag = num != 32 && num != 8204 && num != 1569;
		return num3 && flag;
	}

	private static bool IsMiddleLetter(FastStringBuilder letters, int index)
	{
		int num = letters.Get(index);
		int num2 = 0;
		if (index != 0)
		{
			num2 = letters.Get(index - 1);
		}
		int num3 = 0;
		if (index < letters.Length - 1)
		{
			num3 = letters.Get(index + 1);
		}
		bool flag = index != 0 && num != 1569 && num != 1570 && num != 1571 && num != 1573 && num != 1572 && num != 1575 && num != 1583 && num != 1584 && num != 1585 && num != 1586 && num != 1688 && num != 1608 && num != 8204;
		bool flag2 = index != 0 && num2 != 65152 && num2 != 65153 && num2 != 65155 && num2 != 65159 && num2 != 65157 && num2 != 1575 && num2 != 1583 && num2 != 1584 && num2 != 1585 && num2 != 1586 && num2 != 1688 && num2 != 1608 && num2 != 1570 && num2 != 1571 && num2 != 1573 && num2 != 1572 && num2 != 1569 && num2 != 65165 && num2 != 65193 && num2 != 65195 && num2 != 65197 && num2 != 65199 && num2 != 64394 && num2 != 65261 && num2 != 8204 && num2 < 65535 && TextUtils.IsGlyphFixedArabicCharacter((char)num2);
		return index < letters.Length - 1 && num3 < 65535 && TextUtils.IsGlyphFixedArabicCharacter((char)num3) && num3 != 8204 && num3 != 1569 && num3 != 65152 && flag2 && flag;
	}
}
public static class GlyphTable
{
	private static readonly Dictionary<char, char> MapList;

	static GlyphTable()
	{
		string[] names = Enum.GetNames(typeof(ArabicIsolatedLetters));
		MapList = new Dictionary<char, char>(names.Length);
		string[] array = names;
		foreach (string value in array)
		{
			MapList.Add((char)(int)Enum.Parse(typeof(ArabicGeneralLetters), value), (char)(int)Enum.Parse(typeof(ArabicIsolatedLetters), value));
		}
	}

	public static char Convert(char toBeConverted)
	{
		if (!MapList.TryGetValue(toBeConverted, out var value))
		{
			return toBeConverted;
		}
		return value;
	}
}
public static class LigatureFixer
{
	private static readonly List<int> LtrTextHolder = new List<int>(512);

	private static readonly List<int> TagTextHolder = new List<int>(512);

	private static readonly Dictionary<char, char> MirroredCharsMap = new Dictionary<char, char>
	{
		['('] = ')',
		[')'] = '(',
		['»'] = '«',
		['«'] = '»'
	};

	private static readonly HashSet<char> MirroredCharsSet = new HashSet<char>(MirroredCharsMap.Keys);

	private static void FlushBufferToOutput(List<int> buffer, FastStringBuilder output)
	{
		for (int i = 0; i < buffer.Count; i++)
		{
			output.Append(buffer[buffer.Count - 1 - i]);
		}
		buffer.Clear();
	}

	public static void Fix(FastStringBuilder input, FastStringBuilder output, bool farsi, bool fixTextTags, bool preserveNumbers)
	{
		LtrTextHolder.Clear();
		TagTextHolder.Clear();
		bool flag = false;
		for (int num = input.Length - 1; num >= 0; num--)
		{
			int num2 = input.Get(num);
			if (fixTextTags)
			{
				if (num2 == 62)
				{
					flag = true;
					TagTextHolder.Add(num2);
					continue;
				}
				if (flag)
				{
					TagTextHolder.Add(num2);
					if (num2 == 60)
					{
						flag = false;
						FlushBufferToOutput(LtrTextHolder, output);
						FlushBufferToOutput(TagTextHolder, output);
					}
					continue;
				}
			}
			if (Char32Utils.IsPunctuation(num2) || Char32Utils.IsSymbol(num2))
			{
				if (MirroredCharsSet.Contains((char)num2))
				{
					bool num3 = num == 0;
					bool num4 = num == input.Length - 1;
					int ch = 0;
					if (!num4)
					{
						ch = input.Get(num + 1);
					}
					int ch2 = 0;
					if (!num3)
					{
						ch2 = input.Get(num - 1);
					}
					bool num5 = Char32Utils.IsRTLCharacter(ch2);
					bool flag2 = Char32Utils.IsRTLCharacter(ch);
					if (num5 || flag2)
					{
						num2 = MirroredCharsMap[(char)num2];
					}
				}
				LtrTextHolder.Add(num2);
			}
			else if (Char32Utils.IsEnglishLetter(num2) || Char32Utils.IsNumber(num2, preserveNumbers, farsi))
			{
				LtrTextHolder.Add(num2);
			}
			else if ((num2 >= 55296 && num2 <= 56319) || (num2 >= 56320 && num2 <= 57343))
			{
				LtrTextHolder.Add(num2);
			}
			else
			{
				FlushBufferToOutput(LtrTextHolder, output);
				if (num2 != 65535 && num2 != 8204)
				{
					output.Append(num2);
				}
			}
		}
		FlushBufferToOutput(LtrTextHolder, output);
	}
}
public static class RichTextFixer
{
	public enum TagType
	{
		None,
		Opening,
		Closing,
		SelfContained
	}

	public struct Tag
	{
		public int Start;

		public int End;

		public int HashCode;

		public TagType Type;

		public Tag(int start, int end, TagType type, int hashCode)
		{
			Type = type;
			Start = start;
			End = end;
			HashCode = hashCode;
		}
	}

	public static void Fix(FastStringBuilder text)
	{
		int num = 0;
		while (num < text.Length)
		{
			FindTag(text, num, out var tag);
			if (tag.Type != TagType.None)
			{
				text.Reverse(tag.Start, tag.End - tag.Start + 1);
				num = tag.End;
				num++;
				continue;
			}
			break;
		}
	}

	public static void FindTag(FastStringBuilder str, int start, out Tag tag)
	{
		int num = start;
		while (num < str.Length)
		{
			if (str.Get(num) != 60)
			{
				num++;
				continue;
			}
			bool flag = true;
			tag.HashCode = 0;
			for (int i = num + 1; i < str.Length; i++)
			{
				int num2 = str.Get(i);
				if (flag)
				{
					if (Char32Utils.IsLetter(num2))
					{
						if (tag.HashCode == 0)
						{
							tag.HashCode = num2.GetHashCode();
						}
						else
						{
							tag.HashCode = (tag.HashCode * 397) ^ num2.GetHashCode();
						}
					}
					else if (tag.HashCode != 0)
					{
						flag = false;
					}
				}
				if (i == num + 1 && num2 == 32)
				{
					break;
				}
				switch (num2)
				{
				case 62:
					tag.Start = num;
					tag.End = i;
					if (str.Get(i - 1) == 47)
					{
						tag.Type = TagType.SelfContained;
					}
					else if (str.Get(num + 1) == 47)
					{
						tag.Type = TagType.Closing;
					}
					else
					{
						tag.Type = TagType.Opening;
					}
					return;
				default:
					continue;
				case 60:
					break;
				}
				break;
			}
			num++;
		}
		tag.Start = 0;
		tag.End = 0;
		tag.Type = TagType.None;
		tag.HashCode = 0;
	}
}
public static class RTLSupport
{
	public const int DefaultBufferSize = 2048;

	private static FastStringBuilder inputBuilder;

	private static FastStringBuilder glyphFixerOutput;

	static RTLSupport()
	{
		inputBuilder = new FastStringBuilder(2048);
		glyphFixerOutput = new FastStringBuilder(2048);
	}

	public static void FixRTL(string input, FastStringBuilder output, bool farsi = true, bool fixTextTags = true, bool preserveNumbers = true)
	{
		inputBuilder.SetValue(input);
		TashkeelFixer.RemoveTashkeel(inputBuilder);
		GlyphFixer.Fix(inputBuilder, glyphFixerOutput, preserveNumbers, farsi, fixTextTags);
		TashkeelFixer.RestoreTashkeel(glyphFixerOutput);
		TashkeelFixer.FixShaddaCombinations(glyphFixerOutput);
		LigatureFixer.Fix(glyphFixerOutput, output, farsi, fixTextTags, preserveNumbers);
		if (fixTextTags)
		{
			RichTextFixer.Fix(output);
		}
		inputBuilder.Clear();
	}
}
[ExecuteInEditMode]
public class RTLTextMeshPro : TextMeshProUGUI
{
	[SerializeField]
	protected bool preserveNumbers;

	[SerializeField]
	protected bool farsi = true;

	[SerializeField]
	[TextArea(3, 10)]
	protected string originalText;

	[SerializeField]
	protected bool fixTags = true;

	[SerializeField]
	protected bool forceFix;

	protected readonly FastStringBuilder finalText = new FastStringBuilder(2048);

	public new string text
	{
		get
		{
			return base.text;
		}
		set
		{
			if (!(originalText == value))
			{
				originalText = value;
				UpdateText();
			}
		}
	}

	public string OriginalText => originalText;

	public bool PreserveNumbers
	{
		get
		{
			return preserveNumbers;
		}
		set
		{
			if (preserveNumbers != value)
			{
				preserveNumbers = value;
				base.havePropertiesChanged = true;
			}
		}
	}

	public bool Farsi
	{
		get
		{
			return farsi;
		}
		set
		{
			if (farsi != value)
			{
				farsi = value;
				base.havePropertiesChanged = true;
			}
		}
	}

	public bool FixTags
	{
		get
		{
			return fixTags;
		}
		set
		{
			if (fixTags != value)
			{
				fixTags = value;
				base.havePropertiesChanged = true;
			}
		}
	}

	public bool ForceFix
	{
		get
		{
			return forceFix;
		}
		set
		{
			if (forceFix != value)
			{
				forceFix = value;
				base.havePropertiesChanged = true;
			}
		}
	}

	protected void Update()
	{
		if (base.havePropertiesChanged)
		{
			UpdateText();
		}
	}

	public void UpdateText()
	{
		if (originalText == null)
		{
			originalText = "";
		}
		if (!ForceFix && !TextUtils.ContainsRTLInput(originalText))
		{
			base.isRightToLeftText = false;
			base.text = originalText;
		}
		else
		{
			base.isRightToLeftText = true;
			base.text = GetFixedText(originalText);
		}
		base.havePropertiesChanged = true;
	}

	private string GetFixedText(string input)
	{
		if (string.IsNullOrEmpty(input))
		{
			return input;
		}
		finalText.Clear();
		RTLSupport.FixRTL(input, finalText, farsi, fixTags, preserveNumbers);
		finalText.Reverse();
		return finalText.ToString();
	}
}
[ExecuteInEditMode]
public class RTLTextMeshPro3D : TextMeshPro
{
	[SerializeField]
	protected bool preserveNumbers;

	[SerializeField]
	protected bool farsi = true;

	[SerializeField]
	[TextArea(3, 10)]
	protected string originalText;

	[SerializeField]
	protected bool fixTags = true;

	[SerializeField]
	protected bool forceFix;

	protected readonly FastStringBuilder finalText = new FastStringBuilder(2048);

	public new string text
	{
		get
		{
			return base.text;
		}
		set
		{
			if (!(originalText == value))
			{
				originalText = value;
				UpdateText();
			}
		}
	}

	public string OriginalText => originalText;

	public bool PreserveNumbers
	{
		get
		{
			return preserveNumbers;
		}
		set
		{
			if (preserveNumbers != value)
			{
				preserveNumbers = value;
				base.havePropertiesChanged = true;
			}
		}
	}

	public bool Farsi
	{
		get
		{
			return farsi;
		}
		set
		{
			if (farsi != value)
			{
				farsi = value;
				base.havePropertiesChanged = true;
			}
		}
	}

	public bool FixTags
	{
		get
		{
			return fixTags;
		}
		set
		{
			if (fixTags != value)
			{
				fixTags = value;
				base.havePropertiesChanged = true;
			}
		}
	}

	protected bool ForceFix
	{
		get
		{
			return forceFix;
		}
		set
		{
			if (forceFix != value)
			{
				forceFix = value;
				base.havePropertiesChanged = true;
			}
		}
	}

	protected void Update()
	{
		if (base.havePropertiesChanged)
		{
			UpdateText();
		}
	}

	public void UpdateText()
	{
		if (originalText == null)
		{
			originalText = "";
		}
		if (!ForceFix && !TextUtils.ContainsRTLInput(originalText))
		{
			base.isRightToLeftText = false;
			base.text = originalText;
		}
		else
		{
			base.isRightToLeftText = true;
			base.text = GetFixedText(originalText);
		}
		base.havePropertiesChanged = true;
	}

	private string GetFixedText(string input)
	{
		if (string.IsNullOrEmpty(input))
		{
			return input;
		}
		finalText.Clear();
		RTLSupport.FixRTL(input, finalText, farsi, fixTags, preserveNumbers);
		finalText.Reverse();
		return finalText.ToString();
	}
}
public static class TashkeelFixer
{
	private static readonly List<TashkeelLocation> TashkeelLocations = new List<TashkeelLocation>(100);

	private static readonly string ShaddaDammatan = new string(new char[2] { '\u0651', '\u064c' });

	private static readonly string ShaddaKasratan = new string(new char[2] { '\u0651', '\u064d' });

	private static readonly string ShaddaSuperscriptAlef = new string(new char[2] { '\u0651', '\u0670' });

	private static readonly string ShaddaFatha = new string(new char[2] { '\u0651', '\u064e' });

	private static readonly string ShaddaDamma = new string(new char[2] { '\u0651', '\u064f' });

	private static readonly string ShaddaKasra = new string(new char[2] { '\u0651', '\u0650' });

	private static readonly string ShaddaWithFathaIsolatedForm = 'ﱠ'.ToString();

	private static readonly string ShaddaWithDammaIsolatedForm = 'ﱡ'.ToString();

	private static readonly string ShaddaWithKasraIsolatedForm = 'ﱢ'.ToString();

	private static readonly string ShaddaWithDammatanIsolatedForm = 'ﱞ'.ToString();

	private static readonly string ShaddaWithKasratanIsolatedForm = 'ﱟ'.ToString();

	private static readonly string ShaddaWithSuperscriptAlefIsolatedForm = 'ﱣ'.ToString();

	private static readonly HashSet<char> TashkeelCharactersSet = new HashSet<char>
	{
		'\u064b', '\u064c', '\u064d', '\u064e', '\u064f', '\u0650', '\u0651', '\u0652', '\u0653', '\u0670',
		'ﱞ', 'ﱟ', 'ﱠ', 'ﱡ', 'ﱢ', 'ﱣ'
	};

	private static readonly Dictionary<char, char> ShaddaCombinationMap = new Dictionary<char, char>
	{
		['\u064c'] = 'ﱞ',
		['\u064d'] = 'ﱟ',
		['\u064e'] = 'ﱠ',
		['\u064f'] = 'ﱡ',
		['\u0650'] = 'ﱢ',
		['\u0670'] = 'ﱣ'
	};

	public static void RemoveTashkeel(FastStringBuilder input)
	{
		TashkeelLocations.Clear();
		int num = 0;
		for (int i = 0; i < input.Length; i++)
		{
			int num2 = input.Get(i);
			if (Char32Utils.IsUnicode16Char(num2) && TashkeelCharactersSet.Contains((char)num2))
			{
				TashkeelLocations.Add(new TashkeelLocation((TashkeelCharacters)num2, i));
				continue;
			}
			input.Set(num, num2);
			num++;
		}
		input.Length = num;
	}

	public static void RestoreTashkeel(FastStringBuilder letters)
	{
		foreach (TashkeelLocation tashkeelLocation in TashkeelLocations)
		{
			letters.Insert(tashkeelLocation.Position, tashkeelLocation.Tashkeel);
		}
	}

	public static void FixShaddaCombinations(FastStringBuilder input)
	{
		int num = 0;
		int num2 = 0;
		while (num2 < input.Length)
		{
			int num3 = input.Get(num2);
			int num4 = ((num2 < input.Length - 1) ? input.Get(num2 + 1) : 0);
			if (num3 == 1617 && ShaddaCombinationMap.ContainsKey((char)num4))
			{
				input.Set(num, ShaddaCombinationMap[(char)num4]);
				num++;
				num2 += 2;
			}
			else
			{
				input.Set(num, num3);
				num++;
				num2++;
			}
		}
		input.Length = num;
	}
}
public struct TashkeelLocation
{
	public char Tashkeel { get; set; }

	public int Position { get; set; }

	public TashkeelLocation(TashkeelCharacters tashkeel, int position)
	{
		this = default(TashkeelLocation);
		Tashkeel = (char)tashkeel;
		Position = position;
	}
}
public static class TextUtils
{
	private const char LowerCaseA = 'a';

	private const char UpperCaseA = 'A';

	private const char LowerCaseZ = 'z';

	private const char UpperCaseZ = 'Z';

	private const char HebrewLow = '\u0591';

	private const char HebrewHigh = '״';

	private const char ArabicBaseBlockLow = '\u0600';

	private const char ArabicBaseBlockHigh = 'ۿ';

	private const char ArabicExtendedABlockLow = 'ࢠ';

	private const char ArabicExtendedABlockHigh = '\u08ff';

	private const char ArabicExtendedBBlockLow = 'ࡰ';

	private const char ArabicExtendedBBlockHigh = '\u089f';

	private const char ArabicPresentationFormsABlockLow = 'ﭐ';

	private const char ArabicPresentationFormsABlockHigh = '﷿';

	private const char ArabicPresentationFormsBBlockLow = 'ﹰ';

	private const char ArabicPresentationFormsBBlockHigh = '\ufeff';

	private static readonly HashSet<char> TagDelimiters = new HashSet<char> { '<', '[', '>', ']' };

	public static bool IsPunctuation(char ch)
	{
		throw new NotImplementedException();
	}

	public static bool IsNumber(char ch, bool preserveNumbers, bool farsi)
	{
		if (preserveNumbers)
		{
			return IsEnglishNumber(ch);
		}
		if (farsi)
		{
			return IsFarsiNumber(ch);
		}
		return IsHinduNumber(ch);
	}

	public static bool IsEnglishNumber(char ch)
	{
		if (ch >= '0')
		{
			return ch <= '9';
		}
		return false;
	}

	public static bool IsFarsiNumber(char ch)
	{
		if (ch >= '۰')
		{
			return ch <= '۹';
		}
		return false;
	}

	public static bool IsHinduNumber(char ch)
	{
		if (ch >= '٠')
		{
			return ch <= '٩';
		}
		return false;
	}

	public static bool IsEnglishLetter(char ch)
	{
		if (ch < 'A' || ch > 'Z')
		{
			if (ch >= 'a')
			{
				return ch <= 'z';
			}
			return false;
		}
		return true;
	}

	public static bool IsHebrewCharacter(char ch)
	{
		if (ch >= '\u0591')
		{
			return ch <= '״';
		}
		return false;
	}

	public static bool IsArabicCharacter(char ch)
	{
		if ((ch < '\u0600' || ch > 'ۿ') && (ch < 'ࢠ' || ch > '\u08ff') && (ch < 'ࡰ' || ch > '\u089f') && (ch < 'ﭐ' || ch > '﷿'))
		{
			if (ch >= 'ﹰ')
			{
				return ch <= '\ufeff';
			}
			return false;
		}
		return true;
	}

	public static bool IsRTLCharacter(char ch)
	{
		if (IsHebrewCharacter(ch))
		{
			return true;
		}
		if (IsArabicCharacter(ch))
		{
			return true;
		}
		return false;
	}

	public static bool IsGlyphFixedArabicCharacter(char ch)
	{
		if (ch >= 'ﺀ' && ch <= 'ﺃ')
		{
			return true;
		}
		if (ch >= 'ﺁ' && ch <= 'ﺂ')
		{
			return true;
		}
		if (ch >= 'ﺃ' && ch <= 'ﺄ')
		{
			return true;
		}
		if (ch >= 'ﺅ' && ch <= 'ﺆ')
		{
			return true;
		}
		if (ch >= 'ﺇ' && ch <= 'ﺈ')
		{
			return true;
		}
		if (ch >= 'ﺉ' && ch <= 'ﺌ')
		{
			return true;
		}
		if (ch >= 'ﺍ' && ch <= 'ﺐ')
		{
			return true;
		}
		if (ch >= 'ﺏ' && ch <= 'ﺒ')
		{
			return true;
		}
		if (ch >= 'ﺓ' && ch <= 'ﺔ')
		{
			return true;
		}
		if (ch >= 'ﺕ' && ch <= 'ﺘ')
		{
			return true;
		}
		if (ch >= 'ﺙ' && ch <= 'ﺜ')
		{
			return true;
		}
		if (ch >= 'ﺝ' && ch <= 'ﺠ')
		{
			return true;
		}
		if (ch >= 'ﺡ' && ch <= 'ﺤ')
		{
			return true;
		}
		if (ch >= 'ﺥ' && ch <= 'ﺨ')
		{
			return true;
		}
		if (ch >= 'ﺩ' && ch <= 'ﺪ')
		{
			return true;
		}
		if (ch >= 'ﺫ' && ch <= 'ﺬ')
		{
			return true;
		}
		if (ch >= 'ﺭ' && ch <= 'ﺮ')
		{
			return true;
		}
		if (ch >= 'ﺯ' && ch <= 'ﺰ')
		{
			return true;
		}
		if (ch >= 'ﺱ' && ch <= 'ﺴ')
		{
			return true;
		}
		if (ch >= 'ﺵ' && ch <= 'ﺸ')
		{
			return true;
		}
		if (ch >= 'ﺹ' && ch <= 'ﺼ')
		{
			return true;
		}
		if (ch >= 'ﺽ' && ch <= 'ﻀ')
		{
			return true;
		}
		if (ch >= 'ﻁ' && ch <= 'ﻄ')
		{
			return true;
		}
		if (ch >= 'ﻅ' && ch <= 'ﻈ')
		{
			return true;
		}
		if (ch >= 'ﻉ' && ch <= 'ﻌ')
		{
			return true;
		}
		if (ch >= 'ﻍ' && ch <= 'ﻐ')
		{
			return true;
		}
		if (ch >= 'ﻑ' && ch <= 'ﻔ')
		{
			return true;
		}
		if (ch >= 'ﻕ' && ch <= 'ﻘ')
		{
			return true;
		}
		if (ch >= 'ﻙ' && ch <= 'ﻜ')
		{
			return true;
		}
		if (ch >= 'ﻝ' && ch <= 'ﻠ')
		{
			return true;
		}
		if (ch >= 'ﻡ' && ch <= 'ﻤ')
		{
			return true;
		}
		if (ch >= 'ﻥ' && ch <= 'ﻨ')
		{
			return true;
		}
		if (ch >= 'ﻩ' && ch <= 'ﻬ')
		{
			return true;
		}
		if (ch >= 'ﻭ' && ch <= 'ﻮ')
		{
			return true;
		}
		if (ch >= 'ﻯ' && ch <= 'ﻰ')
		{
			return true;
		}
		if (ch >= 'ﻱ' && ch <= 'ﻴ')
		{
			return true;
		}
		if (ch >= 'ﭖ' && ch <= 'ﭙ')
		{
			return true;
		}
		if (ch >= 'ﯼ' && ch <= 'ﯿ')
		{
			return true;
		}
		if (ch >= 'ﭺ' && ch <= 'ﭽ')
		{
			return true;
		}
		if (ch >= 'ﮊ' && ch <= 'ﮋ')
		{
			return true;
		}
		if (ch >= 'ﮒ' && ch <= 'ﮕ')
		{
			return true;
		}
		if (ch >= 'ﮎ' && ch <= 'ﮑ')
		{
			return true;
		}
		switch (ch)
		{
		case 'ﻳ':
			return true;
		case 'ﻵ':
			return true;
		case 'ﻷ':
			return true;
		case 'ﻹ':
			return true;
		case 'ء':
		case 'آ':
		case 'أ':
		case 'ؤ':
		case 'إ':
		case 'ئ':
		case 'ا':
		case 'ب':
		case 'ة':
		case 'ت':
		case 'ث':
		case 'ج':
		case 'ح':
		case 'خ':
		case 'د':
		case 'ذ':
		case 'ر':
		case 'ز':
		case 'س':
		case 'ش':
		case 'ص':
		case 'ض':
		case 'ط':
		case 'ظ':
		case 'ع':
		case 'غ':
		case 'ـ':
		case 'ف':
		case 'ق':
		case 'ك':
		case 'ل':
		case 'م':
		case 'ن':
		case 'ه':
		case 'و':
		case 'ى':
		case 'ي':
		case 'پ':
		case 'چ':
		case 'ژ':
		case 'ک':
		case 'گ':
		case 'ی':
		case '\u200c':
			return true;
		default:
			return false;
		}
	}

	public static bool ContainsRTLInput(string input)
	{
		if (string.IsNullOrEmpty(input))
		{
			return false;
		}
		bool flag = false;
		foreach (char c in input)
		{
			switch (c)
			{
			case '<':
			case '[':
				flag = true;
				continue;
			case '>':
			case ']':
				flag = false;
				continue;
			}
			if (!flag)
			{
				if (c >= '\u0590' && c <= '\u08ff')
				{
					return true;
				}
				if (IsRTLCharacter(c))
				{
					return true;
				}
			}
		}
		return false;
	}
}
public enum ArabicGeneralLetters
{
	Hamza = 1569,
	AlefMaddaAbove = 1570,
	AlefHamzaAbove = 1571,
	WawHamzaAbove = 1572,
	AlefHamzaBelow = 1573,
	YehHamzaAbove = 1574,
	Alef = 1575,
	Beh = 1576,
	TehMarbuta = 1577,
	Teh = 1578,
	Theh = 1579,
	Jeem = 1580,
	Hah = 1581,
	Khah = 1582,
	Dal = 1583,
	Thal = 1584,
	Reh = 1585,
	Zain = 1586,
	Seen = 1587,
	Sheen = 1588,
	Sad = 1589,
	Dad = 1590,
	Tah = 1591,
	Zah = 1592,
	Ain = 1593,
	Ghain = 1594,
	Feh = 1601,
	Qaf = 1602,
	Kaf = 1603,
	Lam = 1604,
	Meem = 1605,
	Noon = 1606,
	Heh = 1607,
	Waw = 1608,
	AlefMaksura = 1609,
	Yeh = 1610,
	FarsiYeh = 1740,
	Peh = 1662,
	TCheh = 1670,
	Jeh = 1688,
	Keheh = 1705,
	Gaf = 1711,
	Tatweel = 1600
}
internal enum ArabicIsolatedLetters
{
	Hamza = 65152,
	AlefMaddaAbove = 65153,
	AlefHamzaAbove = 65155,
	WawHamzaAbove = 65157,
	AlefHamzaBelow = 65159,
	YehHamzaAbove = 65161,
	Alef = 65165,
	Beh = 65167,
	TehMarbuta = 65171,
	Teh = 65173,
	Theh = 65177,
	Jeem = 65181,
	Hah = 65185,
	Khah = 65189,
	Dal = 65193,
	Thal = 65195,
	Reh = 65197,
	Zain = 65199,
	Seen = 65201,
	Sheen = 65205,
	Sad = 65209,
	Dad = 65213,
	Tah = 65217,
	Zah = 65221,
	Ain = 65225,
	Ghain = 65229,
	Feh = 65233,
	Qaf = 65237,
	Kaf = 65241,
	Lam = 65245,
	Meem = 65249,
	Noon = 65253,
	Heh = 65257,
	Waw = 65261,
	AlefMaksura = 65263,
	Yeh = 65265,
	FarsiYeh = 64508,
	Peh = 64342,
	TCheh = 64378,
	Jeh = 64394,
	Keheh = 64398,
	Gaf = 64402
}
public enum EnglishNumbers
{
	Zero = 48,
	One,
	Two,
	Three,
	Four,
	Five,
	Six,
	Seven,
	Eight,
	Nine
}
public enum FarsiNumbers
{
	Zero = 1776,
	One,
	Two,
	Three,
	Four,
	Five,
	Six,
	Seven,
	Eight,
	Nine
}
public enum HinduNumbers
{
	Zero = 1632,
	One,
	Two,
	Three,
	Four,
	Five,
	Six,
	Seven,
	Eight,
	Nine
}
public enum SpecialCharacters
{
	ZeroWidthNoJoiner = 8204
}
public enum TashkeelCharacters
{
	Fathan = 1611,
	Dammatan = 1612,
	Kasratan = 1613,
	Fatha = 1614,
	Damma = 1615,
	Kasra = 1616,
	Shadda = 1617,
	Sukun = 1618,
	MaddahAbove = 1619,
	SuperscriptAlef = 1648,
	ShaddaWithDammatanIsolatedForm = 64606,
	ShaddaWithKasratanIsolatedForm = 64607,
	ShaddaWithFathaIsolatedForm = 64608,
	ShaddaWithDammaIsolatedForm = 64609,
	ShaddaWithKasraIsolatedForm = 64610,
	ShaddaWithSuperscriptAlefIsolatedForm = 64611
}
