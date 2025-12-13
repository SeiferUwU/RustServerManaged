using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

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
			FilePathsData = new byte[67]
			{
				0, 0, 0, 4, 0, 0, 0, 59, 92, 65,
				115, 115, 101, 116, 115, 92, 83, 116, 97, 110,
				100, 97, 114, 100, 32, 65, 115, 115, 101, 116,
				115, 92, 84, 104, 105, 114, 100, 32, 80, 97,
				114, 116, 121, 92, 66, 111, 111, 109, 108, 97,
				103, 111, 111, 110, 32, 74, 83, 79, 78, 92,
				74, 83, 79, 78, 46, 99, 115
			},
			TypesData = new byte[66]
			{
				0, 0, 0, 0, 15, 74, 83, 79, 78, 124,
				69, 120, 116, 101, 110, 115, 105, 111, 110, 115,
				0, 0, 0, 0, 10, 74, 83, 79, 78, 124,
				86, 97, 108, 117, 101, 0, 0, 0, 0, 10,
				74, 83, 79, 78, 124, 65, 114, 114, 97, 121,
				0, 0, 0, 0, 11, 74, 83, 79, 78, 124,
				79, 98, 106, 101, 99, 116
			},
			TotalFiles = 1,
			TotalTypes = 4,
			IsEditorOnly = false
		};
	}
}
namespace JSON;

public static class Extensions
{
	public static T Pop<T>(this List<T> list)
	{
		T result = list[list.Count - 1];
		list.RemoveAt(list.Count - 1);
		return result;
	}
}
public enum ValueType
{
	String,
	Number,
	Object,
	Array,
	Boolean,
	Null
}
public class Value
{
	public ValueType Type { get; private set; }

	public string Str { get; set; }

	public double Number { get; set; }

	public Object Obj { get; set; }

	public Array Array { get; set; }

	public bool Boolean { get; set; }

	public Value Parent { get; set; }

	public Value(ValueType type)
	{
		Type = type;
	}

	public Value(string str)
	{
		Type = ValueType.String;
		Str = str;
	}

	public Value(double number)
	{
		Type = ValueType.Number;
		Number = number;
	}

	public Value(Object obj)
	{
		if (obj == null)
		{
			Type = ValueType.Null;
			return;
		}
		Type = ValueType.Object;
		Obj = obj;
	}

	public Value(Array array)
	{
		Type = ValueType.Array;
		Array = array;
	}

	public Value(bool boolean)
	{
		Type = ValueType.Boolean;
		Boolean = boolean;
	}

	public Value(Value value)
	{
		Type = value.Type;
		switch (Type)
		{
		case ValueType.String:
			Str = value.Str;
			break;
		case ValueType.Boolean:
			Boolean = value.Boolean;
			break;
		case ValueType.Number:
			Number = value.Number;
			break;
		case ValueType.Object:
			if (value.Obj != null)
			{
				Obj = new Object(value.Obj);
			}
			break;
		case ValueType.Array:
			Array = new Array(value.Array);
			break;
		}
	}

	public static implicit operator Value(string str)
	{
		return new Value(str);
	}

	public static implicit operator Value(double number)
	{
		return new Value(number);
	}

	public static implicit operator Value(Object obj)
	{
		return new Value(obj);
	}

	public static implicit operator Value(Array array)
	{
		return new Value(array);
	}

	public static implicit operator Value(bool boolean)
	{
		return new Value(boolean);
	}

	public override string ToString()
	{
		switch (Type)
		{
		case ValueType.Object:
			return Obj.ToString();
		case ValueType.Array:
			return Array.ToString();
		case ValueType.Boolean:
			if (!Boolean)
			{
				return "false";
			}
			return "true";
		case ValueType.Number:
			return Number.ToString();
		case ValueType.String:
			return "\"" + Str + "\"";
		case ValueType.Null:
			return "null";
		default:
			return "null";
		}
	}
}
public class Array : IEnumerable<Value>, IEnumerable
{
	private readonly List<Value> values = new List<Value>();

	public Value this[int index]
	{
		get
		{
			return values[index];
		}
		set
		{
			values[index] = value;
		}
	}

	public int Length => values.Count;

	public Array()
	{
	}

	public Array(Array array)
	{
		values = new List<Value>();
		foreach (Value value in array.values)
		{
			values.Add(new Value(value));
		}
	}

	public void Add(Value value)
	{
		values.Add(value);
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append('[');
		foreach (Value value in values)
		{
			stringBuilder.Append(value.ToString());
			stringBuilder.Append(',');
		}
		if (values.Count > 0)
		{
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
		}
		stringBuilder.Append(']');
		return stringBuilder.ToString();
	}

	public IEnumerator<Value> GetEnumerator()
	{
		return values.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return values.GetEnumerator();
	}

	public static Array Parse(string jsonString)
	{
		return Object.Parse("{ \"array\" :" + jsonString + "}")?.GetValue("array").Array;
	}

	public void Clear()
	{
		values.Clear();
	}

	public void Remove(int index)
	{
		if (index >= 0 && index < values.Count)
		{
			values.RemoveAt(index);
		}
	}

	public static Array operator +(Array lhs, Array rhs)
	{
		Array array = new Array(lhs);
		foreach (Value value in rhs.values)
		{
			array.Add(value);
		}
		return array;
	}
}
public class Object : IEnumerable<KeyValuePair<string, Value>>, IEnumerable
{
	private enum ParsingState
	{
		Object,
		Array,
		EndObject,
		EndArray,
		Key,
		Value,
		KeyValueSeparator,
		ValueSeparator,
		String,
		Number,
		Boolean,
		Null
	}

	private readonly IDictionary<string, Value> values = new Dictionary<string, Value>();

	public Value this[string key]
	{
		get
		{
			return GetValue(key);
		}
		set
		{
			values[key] = value;
		}
	}

	public Object()
	{
	}

	public Object(Object other)
	{
		values = new Dictionary<string, Value>();
		if (other == null)
		{
			return;
		}
		foreach (KeyValuePair<string, Value> value in other.values)
		{
			values[value.Key] = new Value(value.Value);
		}
	}

	public bool ContainsKey(string key)
	{
		return values.ContainsKey(key);
	}

	public Value GetValue(string key)
	{
		values.TryGetValue(key, out var value);
		return value;
	}

	public string GetString(string key, string strDEFAULT = "")
	{
		Value value = GetValue(key);
		if (value == null)
		{
			return strDEFAULT;
		}
		return value.Str.Replace("\\/", "/");
	}

	public double GetNumber(string key, double iDefault = 0.0)
	{
		Value value = GetValue(key);
		if (value == null)
		{
			return iDefault;
		}
		if (value.Type == ValueType.Number)
		{
			return value.Number;
		}
		if (value.Type == ValueType.String)
		{
			double result = iDefault;
			if (double.TryParse(value.Str, out result))
			{
				return result;
			}
		}
		return iDefault;
	}

	public int GetInt(string key, int iDefault = 0)
	{
		return (int)GetNumber(key, iDefault);
	}

	public float GetFloat(string key, float iDefault = 0f)
	{
		return (float)GetNumber(key, iDefault);
	}

	public Object GetObject(string key)
	{
		Value value = GetValue(key);
		if (value == null)
		{
			return new Object();
		}
		return value.Obj;
	}

	public bool GetBoolean(string key, bool bDefault = false)
	{
		Value value = GetValue(key);
		if (value == null)
		{
			return bDefault;
		}
		if (value.Type == ValueType.Boolean)
		{
			return value.Boolean;
		}
		if (value.Type == ValueType.Number)
		{
			return value.Number != 0.0;
		}
		return bDefault;
	}

	public bool TryGetBoolean(string key, out bool result)
	{
		Value value = GetValue(key);
		if (value == null)
		{
			result = false;
			return false;
		}
		if (value.Type == ValueType.Boolean)
		{
			result = value.Boolean;
			return true;
		}
		if (value.Type == ValueType.Number)
		{
			result = value.Number != 0.0;
			return true;
		}
		result = false;
		return false;
	}

	public Array GetArray(string key)
	{
		Value value = GetValue(key);
		if (value == null)
		{
			return new Array();
		}
		return value.Array;
	}

	public void Add(string key, Value value)
	{
		values[key] = value;
	}

	public void Add(KeyValuePair<string, Value> pair)
	{
		values[pair.Key] = pair.Value;
	}

	public static Object Parse(string jsonString)
	{
		if (string.IsNullOrEmpty(jsonString))
		{
			return null;
		}
		Value value = null;
		List<string> list = new List<string>();
		ParsingState parsingState = ParsingState.Object;
		int num;
		for (num = 0; num < jsonString.Length; num++)
		{
			num = SkipWhitespace(jsonString, num);
			switch (parsingState)
			{
			case ParsingState.Object:
			{
				if (jsonString[num] != '{')
				{
					return Fail('{', num);
				}
				Value value3 = new Object();
				if (value != null)
				{
					value3.Parent = value;
				}
				value = value3;
				parsingState = ParsingState.Key;
				break;
			}
			case ParsingState.EndObject:
				if (jsonString[num] != '}')
				{
					return Fail('}', num);
				}
				if (value.Parent == null)
				{
					return value.Obj;
				}
				switch (value.Parent.Type)
				{
				case ValueType.Object:
					value.Parent.Obj.values[list.Pop()] = new Value(value.Obj);
					break;
				case ValueType.Array:
					value.Parent.Array.Add(new Value(value.Obj));
					break;
				default:
					return Fail("valid object", num);
				}
				value = value.Parent;
				parsingState = ParsingState.ValueSeparator;
				break;
			case ParsingState.Key:
			{
				if (jsonString[num] == '}')
				{
					num--;
					parsingState = ParsingState.EndObject;
					break;
				}
				string text2 = ParseString(jsonString, ref num);
				if (text2 == null)
				{
					return Fail("key string", num);
				}
				list.Add(text2);
				parsingState = ParsingState.KeyValueSeparator;
				break;
			}
			case ParsingState.KeyValueSeparator:
				if (jsonString[num] != ':')
				{
					return Fail(':', num);
				}
				parsingState = ParsingState.Value;
				break;
			case ParsingState.ValueSeparator:
				switch (jsonString[num])
				{
				case ',':
					parsingState = ((value.Type == ValueType.Object) ? ParsingState.Key : ParsingState.Value);
					break;
				case '}':
					parsingState = ParsingState.EndObject;
					num--;
					break;
				case ']':
					parsingState = ParsingState.EndArray;
					num--;
					break;
				default:
					return Fail(", } ]", num);
				}
				break;
			case ParsingState.Value:
			{
				char c = jsonString[num];
				if (c == '"')
				{
					parsingState = ParsingState.String;
				}
				else if (char.IsDigit(c) || c == '-')
				{
					parsingState = ParsingState.Number;
				}
				else
				{
					switch (c)
					{
					case '{':
						parsingState = ParsingState.Object;
						break;
					case '[':
						parsingState = ParsingState.Array;
						break;
					case ']':
						if (value.Type == ValueType.Array)
						{
							parsingState = ParsingState.EndArray;
							break;
						}
						return Fail("valid array", num);
					case 'f':
					case 't':
						parsingState = ParsingState.Boolean;
						break;
					case 'n':
						parsingState = ParsingState.Null;
						break;
					default:
						return Fail("beginning of value", num);
					}
				}
				num--;
				break;
			}
			case ParsingState.String:
			{
				string text = ParseString(jsonString, ref num);
				if (text == null)
				{
					return Fail("string value", num);
				}
				switch (value.Type)
				{
				case ValueType.Object:
					value.Obj.values[list.Pop()] = new Value(text);
					break;
				case ValueType.Array:
					value.Array.Add(text);
					break;
				default:
					return null;
				}
				parsingState = ParsingState.ValueSeparator;
				break;
			}
			case ParsingState.Number:
			{
				double num2 = ParseNumber(jsonString, ref num);
				if (double.IsNaN(num2))
				{
					return Fail("valid number", num);
				}
				switch (value.Type)
				{
				case ValueType.Object:
					value.Obj.values[list.Pop()] = new Value(num2);
					break;
				case ValueType.Array:
					value.Array.Add(num2);
					break;
				default:
					return null;
				}
				parsingState = ParsingState.ValueSeparator;
				break;
			}
			case ParsingState.Boolean:
				if (jsonString[num] == 't')
				{
					if (jsonString.Length < num + 4 || jsonString[num + 1] != 'r' || jsonString[num + 2] != 'u' || jsonString[num + 3] != 'e')
					{
						return Fail("true", num);
					}
					switch (value.Type)
					{
					case ValueType.Object:
						value.Obj.values[list.Pop()] = new Value(boolean: true);
						break;
					case ValueType.Array:
						value.Array.Add(new Value(boolean: true));
						break;
					default:
						return null;
					}
					num += 3;
				}
				else
				{
					if (jsonString.Length < num + 5 || jsonString[num + 1] != 'a' || jsonString[num + 2] != 'l' || jsonString[num + 3] != 's' || jsonString[num + 4] != 'e')
					{
						return Fail("false", num);
					}
					switch (value.Type)
					{
					case ValueType.Object:
						value.Obj.values[list.Pop()] = new Value(boolean: false);
						break;
					case ValueType.Array:
						value.Array.Add(new Value(boolean: false));
						break;
					default:
						return null;
					}
					num += 4;
				}
				parsingState = ParsingState.ValueSeparator;
				break;
			case ParsingState.Array:
			{
				if (jsonString[num] != '[')
				{
					return Fail('[', num);
				}
				Value value2 = new Array();
				if (value != null)
				{
					value2.Parent = value;
				}
				value = value2;
				parsingState = ParsingState.Value;
				break;
			}
			case ParsingState.EndArray:
				if (jsonString[num] != ']')
				{
					return Fail(']', num);
				}
				if (value.Parent == null)
				{
					return value.Obj;
				}
				switch (value.Parent.Type)
				{
				case ValueType.Object:
					value.Parent.Obj.values[list.Pop()] = new Value(value.Array);
					break;
				case ValueType.Array:
					value.Parent.Array.Add(new Value(value.Array));
					break;
				default:
					return Fail("valid object", num);
				}
				value = value.Parent;
				parsingState = ParsingState.ValueSeparator;
				break;
			case ParsingState.Null:
				if (jsonString[num] == 'n')
				{
					if (jsonString.Length < num + 4 || jsonString[num + 1] != 'u' || jsonString[num + 2] != 'l' || jsonString[num + 3] != 'l')
					{
						return Fail("null", num);
					}
					switch (value.Type)
					{
					case ValueType.Object:
						value.Obj.values[list.Pop()] = new Value(ValueType.Null);
						break;
					case ValueType.Array:
						value.Array.Add(new Value(ValueType.Null));
						break;
					default:
						return null;
					}
					num += 3;
				}
				parsingState = ParsingState.ValueSeparator;
				break;
			}
		}
		return null;
	}

	private static int SkipWhitespace(string str, int pos)
	{
		while (pos < str.Length && char.IsWhiteSpace(str[pos]))
		{
			pos++;
		}
		return pos;
	}

	private static string ParseString(string str, ref int startPosition)
	{
		if (str[startPosition] != '"' || startPosition + 1 >= str.Length)
		{
			Fail('"', startPosition);
			return null;
		}
		int num = str.IndexOf('"', startPosition + 1);
		if (num <= startPosition)
		{
			Fail('"', startPosition + 1);
			return null;
		}
		while (str[num - 1] == '\\')
		{
			num = str.IndexOf('"', num + 1);
			if (num <= startPosition)
			{
				Fail('"', startPosition + 1);
				return null;
			}
		}
		string result = string.Empty;
		if (num > startPosition + 1)
		{
			result = str.Substring(startPosition + 1, num - startPosition - 1);
		}
		startPosition = num;
		return result;
	}

	private static double ParseNumber(string str, ref int startPosition)
	{
		if (startPosition >= str.Length || (!char.IsDigit(str[startPosition]) && str[startPosition] != '-'))
		{
			return double.NaN;
		}
		int i;
		for (i = startPosition + 1; i < str.Length && str[i] != ',' && str[i] != ']' && str[i] != '}'; i++)
		{
		}
		if (!double.TryParse(str.Substring(startPosition, i - startPosition), NumberStyles.Float, CultureInfo.InvariantCulture, out var result))
		{
			return double.NaN;
		}
		startPosition = i - 1;
		return result;
	}

	private static Object Fail(char expected, int position)
	{
		return Fail(new string(expected, 1), position);
	}

	private static Object Fail(string expected, int position)
	{
		return null;
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append('{');
		foreach (KeyValuePair<string, Value> value in values)
		{
			stringBuilder.Append("\"" + value.Key + "\"");
			stringBuilder.Append(':');
			stringBuilder.Append(value.Value.ToString());
			stringBuilder.Append(',');
		}
		if (values.Count > 0)
		{
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
		}
		stringBuilder.Append('}');
		return stringBuilder.ToString();
	}

	public IEnumerator<KeyValuePair<string, Value>> GetEnumerator()
	{
		return values.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return values.GetEnumerator();
	}

	public void Clear()
	{
		values.Clear();
	}

	public void Remove(string key)
	{
		if (values.ContainsKey(key))
		{
			values.Remove(key);
		}
	}
}
