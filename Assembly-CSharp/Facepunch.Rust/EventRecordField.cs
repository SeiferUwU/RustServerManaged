using System;
using System.Buffers;
using System.IO;
using Cysharp.Text;
using UnityEngine;

namespace Facepunch.Rust;

public struct EventRecordField
{
	public string Key1;

	public string Key2;

	public string String;

	public long? Number;

	public double? Float;

	public Vector3? Vector;

	public Guid? Guid;

	public DateTime DateTime;

	public bool IsObject;

	public MemoryStream Bytes;

	public EventRecordField(string key1)
	{
		Key1 = key1;
		Key2 = null;
		String = null;
		Number = null;
		Float = null;
		Vector = null;
		Guid = null;
		IsObject = false;
		DateTime = default(DateTime);
		Bytes = null;
	}

	public EventRecordField(string key1, string key2)
	{
		Key1 = key1;
		Key2 = key2;
		String = null;
		Number = null;
		Float = null;
		Vector = null;
		Guid = null;
		IsObject = false;
		DateTime = default(DateTime);
		Bytes = null;
	}

	public void Serialize(ref Utf8ValueStringBuilder writer, AnalyticsDocumentMode format)
	{
		if (String != null)
		{
			if (IsObject)
			{
				writer.Append(String);
				return;
			}
			string text = String;
			int length = String.Length;
			for (int i = 0; i < length; i++)
			{
				char c = text[i];
				if (c == '\\' && format == AnalyticsDocumentMode.JSON)
				{
					writer.Append("\\\\");
					continue;
				}
				switch (c)
				{
				case '"':
					if (format == AnalyticsDocumentMode.JSON)
					{
						writer.Append("\\\"");
					}
					else
					{
						writer.Append("\"\"");
					}
					break;
				case '\n':
					writer.Append("\\n");
					break;
				case '\r':
					writer.Append("\\r");
					break;
				case '\t':
					writer.Append("\\t");
					break;
				default:
					writer.Append(c);
					break;
				}
			}
		}
		else if (Float.HasValue)
		{
			Span<char> destination = stackalloc char[128];
			Float.Value.TryFormat(destination, out var charsWritten);
			writer.Append((ReadOnlySpan<char>)destination.Slice(0, charsWritten));
		}
		else if (Number.HasValue)
		{
			writer.Append(Number.Value);
		}
		else if (Guid.HasValue)
		{
			writer.Append(format: new StandardFormat('N'), value: Guid.Value);
		}
		else if (Vector.HasValue)
		{
			Span<char> destination2 = stackalloc char[128];
			Vector3 value = Vector.Value;
			value.x.TryFormat(destination2, out var charsWritten2);
			writer.Append((ReadOnlySpan<char>)destination2.Slice(0, charsWritten2));
			writer.Append(',');
			value.y.TryFormat(destination2, out charsWritten2);
			writer.Append((ReadOnlySpan<char>)destination2.Slice(0, charsWritten2));
			writer.Append(',');
			value.z.TryFormat(destination2, out charsWritten2);
			writer.Append((ReadOnlySpan<char>)destination2.Slice(0, charsWritten2));
		}
		else if (DateTime != default(DateTime))
		{
			writer.Append(DateTime, StandardFormats.DateTime_ISO);
		}
		else if (Bytes != null)
		{
			Span<char> chars = stackalloc char[128];
			int num = 64;
			byte[] buffer = Bytes.GetBuffer();
			for (int j = 0; j < Bytes.Length; j += num)
			{
				int length2 = Mathf.Min(num, (int)Bytes.Length - j);
				Convert.TryToBase64Chars(new Span<byte>(buffer, j, length2), chars, out var charsWritten3);
				Span<char> span = chars.Slice(0, charsWritten3);
				writer.Append((ReadOnlySpan<char>)span);
			}
		}
	}
}
