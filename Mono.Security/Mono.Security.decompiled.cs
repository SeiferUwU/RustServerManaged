using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mono.Math;
using Mono.Math.Prime;
using Mono.Math.Prime.Generator;
using Mono.Net.Security;
using Mono.Security.Cryptography;
using Mono.Security.X509;
using Mono.Security.X509.Extensions;
using Mono.Xml;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyCompany("MONO development team")]
[assembly: AssemblyCopyright("(c) 2003-2004 Various Authors")]
[assembly: AssemblyDescription("Mono.Security.dll")]
[assembly: AssemblyProduct("MONO CLI")]
[assembly: AssemblyTitle("Mono.Security.dll")]
[assembly: CLSCompliant(true)]
[assembly: ComVisible(false)]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: AssemblyDelaySign(true)]
[assembly: InternalsVisibleTo("System, PublicKey=00000000000000000400000000000000")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("4.0.0.0")]
[module: UnverifiableCode]
internal static class AssemblyRef
{
	internal const string SystemConfiguration = "System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	internal const string System = "System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

	public const string EcmaPublicKey = "b77a5c561934e089";

	public const string FrameworkPublicKeyFull = "00000000000000000400000000000000";

	public const string FrameworkPublicKeyFull2 = "00000000000000000400000000000000";

	public const string MicrosoftPublicKey = "b03f5f7f11d50a3a";

	public const string MicrosoftJScript = "Microsoft.JScript, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string MicrosoftVSDesigner = "Microsoft.VSDesigner, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string SystemData = "System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string SystemDesign = "System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string SystemDrawing = "System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string SystemWeb = "System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string SystemWebExtensions = "System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

	public const string SystemWindowsForms = "System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
}
internal static class Consts
{
	public const string MonoCorlibVersion = "1A5E0066-58DC-428A-B21C-0AD6CDAE2789";

	public const string MonoVersion = "6.13.0.0";

	public const string MonoCompany = "Mono development team";

	public const string MonoProduct = "Mono Common Language Infrastructure";

	public const string MonoCopyright = "(c) Various Mono authors";

	public const string FxVersion = "4.0.0.0";

	public const string FxFileVersion = "4.6.57.0";

	public const string EnvironmentVersion = "4.0.30319.42000";

	public const string VsVersion = "0.0.0.0";

	public const string VsFileVersion = "11.0.0.0";

	private const string PublicKeyToken = "b77a5c561934e089";

	public const string AssemblyI18N = "I18N, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyMicrosoft_JScript = "Microsoft.JScript, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblyMicrosoft_VisualStudio = "Microsoft.VisualStudio, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblyMicrosoft_VisualStudio_Web = "Microsoft.VisualStudio.Web, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblyMicrosoft_VSDesigner = "Microsoft.VSDesigner, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblyMono_Http = "Mono.Http, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyMono_Posix = "Mono.Posix, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyMono_Security = "Mono.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyMono_Messaging_RabbitMQ = "Mono.Messaging.RabbitMQ, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyCorlib = "mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem = "System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem_Data = "System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem_Design = "System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_DirectoryServices = "System.DirectoryServices, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Drawing = "System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Drawing_Design = "System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Messaging = "System.Messaging, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Security = "System.Security, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_ServiceProcess = "System.ServiceProcess, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Web = "System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Windows_Forms = "System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem_2_0 = "System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystemCore_3_5 = "System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem_Core = "System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string WindowsBase_3_0 = "WindowsBase, Version=3.0.0.0, PublicKeyToken=31bf3856ad364e35";

	public const string AssemblyWindowsBase = "WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

	public const string AssemblyPresentationCore_3_5 = "PresentationCore, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

	public const string AssemblyPresentationCore_4_0 = "PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

	public const string AssemblyPresentationFramework_3_5 = "PresentationFramework, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

	public const string AssemblySystemServiceModel_3_0 = "System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
}
internal sealed class Locale
{
	private Locale()
	{
	}

	public static string GetText(string msg)
	{
		return msg;
	}

	public static string GetText(string fmt, params object[] args)
	{
		return string.Format(fmt, args);
	}
}
namespace Mono.Xml
{
	[CLSCompliant(false)]
	public class MiniParser
	{
		public interface IReader
		{
			int Read();
		}

		public interface IAttrList
		{
			int Length { get; }

			bool IsEmpty { get; }

			string[] Names { get; }

			string[] Values { get; }

			string GetName(int i);

			string GetValue(int i);

			string GetValue(string name);

			void ChangeValue(string name, string newValue);
		}

		public interface IMutableAttrList : IAttrList
		{
			void Clear();

			void Add(string name, string value);

			void CopyFrom(IAttrList attrs);

			void Remove(int i);

			void Remove(string name);
		}

		public interface IHandler
		{
			void OnStartParsing(MiniParser parser);

			void OnStartElement(string name, IAttrList attrs);

			void OnEndElement(string name);

			void OnChars(string ch);

			void OnEndParsing(MiniParser parser);
		}

		public class HandlerAdapter : IHandler
		{
			public void OnStartParsing(MiniParser parser)
			{
			}

			public void OnStartElement(string name, IAttrList attrs)
			{
			}

			public void OnEndElement(string name)
			{
			}

			public void OnChars(string ch)
			{
			}

			public void OnEndParsing(MiniParser parser)
			{
			}
		}

		private enum CharKind : byte
		{
			LEFT_BR = 0,
			RIGHT_BR = 1,
			SLASH = 2,
			PI_MARK = 3,
			EQ = 4,
			AMP = 5,
			SQUOTE = 6,
			DQUOTE = 7,
			BANG = 8,
			LEFT_SQBR = 9,
			SPACE = 10,
			RIGHT_SQBR = 11,
			TAB = 12,
			CR = 13,
			EOL = 14,
			CHARS = 15,
			UNKNOWN = 31
		}

		private enum ActionCode : byte
		{
			START_ELEM = 0,
			END_ELEM = 1,
			END_NAME = 2,
			SET_ATTR_NAME = 3,
			SET_ATTR_VAL = 4,
			SEND_CHARS = 5,
			START_CDATA = 6,
			END_CDATA = 7,
			ERROR = 8,
			STATE_CHANGE = 9,
			FLUSH_CHARS_STATE_CHANGE = 10,
			ACC_CHARS_STATE_CHANGE = 11,
			ACC_CDATA = 12,
			PROC_CHAR_REF = 13,
			UNKNOWN = 15
		}

		public class AttrListImpl : IMutableAttrList, IAttrList
		{
			protected ArrayList names;

			protected ArrayList values;

			public int Length => names.Count;

			public bool IsEmpty => Length != 0;

			public string[] Names => names.ToArray(typeof(string)) as string[];

			public string[] Values => values.ToArray(typeof(string)) as string[];

			public AttrListImpl()
				: this(0)
			{
			}

			public AttrListImpl(int initialCapacity)
			{
				if (initialCapacity <= 0)
				{
					names = new ArrayList();
					values = new ArrayList();
				}
				else
				{
					names = new ArrayList(initialCapacity);
					values = new ArrayList(initialCapacity);
				}
			}

			public AttrListImpl(IAttrList attrs)
				: this(attrs?.Length ?? 0)
			{
				if (attrs != null)
				{
					CopyFrom(attrs);
				}
			}

			public string GetName(int i)
			{
				string result = null;
				if (i >= 0 && i < Length)
				{
					result = names[i] as string;
				}
				return result;
			}

			public string GetValue(int i)
			{
				string result = null;
				if (i >= 0 && i < Length)
				{
					result = values[i] as string;
				}
				return result;
			}

			public string GetValue(string name)
			{
				return GetValue(names.IndexOf(name));
			}

			public void ChangeValue(string name, string newValue)
			{
				int num = names.IndexOf(name);
				if (num >= 0 && num < Length)
				{
					values[num] = newValue;
				}
			}

			public void Clear()
			{
				names.Clear();
				values.Clear();
			}

			public void Add(string name, string value)
			{
				names.Add(name);
				values.Add(value);
			}

			public void Remove(int i)
			{
				if (i >= 0)
				{
					names.RemoveAt(i);
					values.RemoveAt(i);
				}
			}

			public void Remove(string name)
			{
				Remove(names.IndexOf(name));
			}

			public void CopyFrom(IAttrList attrs)
			{
				if (attrs != null && this == attrs)
				{
					Clear();
					int length = attrs.Length;
					for (int i = 0; i < length; i++)
					{
						Add(attrs.GetName(i), attrs.GetValue(i));
					}
				}
			}
		}

		public class XMLError : Exception
		{
			protected string descr;

			protected int line;

			protected int column;

			public int Line => line;

			public int Column => column;

			public XMLError()
				: this("Unknown")
			{
			}

			public XMLError(string descr)
				: this(descr, -1, -1)
			{
			}

			public XMLError(string descr, int line, int column)
				: base(descr)
			{
				this.descr = descr;
				this.line = line;
				this.column = column;
			}

			public override string ToString()
			{
				return $"{descr} @ (line = {line}, col = {column})";
			}
		}

		private static readonly int INPUT_RANGE = 13;

		private static readonly ushort[] tbl = new ushort[262]
		{
			2305, 43264, 63616, 10368, 6272, 14464, 18560, 22656, 26752, 34944,
			39040, 47232, 30848, 2177, 10498, 6277, 14595, 18561, 22657, 26753,
			35088, 39041, 43137, 47233, 30849, 64004, 4352, 43266, 64258, 2177,
			10369, 14465, 18561, 22657, 26753, 34945, 39041, 47233, 30849, 14597,
			2307, 10499, 6403, 18691, 22787, 26883, 35075, 39171, 43267, 47363,
			30979, 63747, 64260, 8710, 4615, 41480, 2177, 14465, 18561, 22657,
			26753, 34945, 39041, 47233, 30849, 6400, 2307, 10499, 14595, 18691,
			22787, 26883, 35075, 39171, 43267, 47363, 30979, 63747, 6400, 2177,
			10369, 14465, 18561, 22657, 26753, 34945, 39041, 43137, 47233, 30849,
			63617, 2561, 23818, 11274, 7178, 15370, 19466, 27658, 35850, 39946,
			43783, 48138, 31754, 64522, 64265, 8198, 4103, 43272, 2177, 14465,
			18561, 22657, 26753, 34945, 39041, 47233, 30849, 64265, 17163, 43276,
			2178, 10370, 6274, 14466, 22658, 26754, 34946, 39042, 47234, 30850,
			2317, 23818, 11274, 7178, 15370, 19466, 27658, 35850, 39946, 44042,
			48138, 31754, 64522, 26894, 30991, 43275, 2180, 10372, 6276, 14468,
			18564, 22660, 34948, 39044, 47236, 63620, 17163, 43276, 2178, 10370,
			6274, 14466, 22658, 26754, 34946, 39042, 47234, 30850, 63618, 9474,
			35088, 2182, 6278, 14470, 18566, 22662, 26758, 39046, 43142, 47238,
			30854, 63622, 25617, 23822, 2830, 11022, 6926, 15118, 19214, 35598,
			39694, 43790, 47886, 31502, 64270, 29713, 23823, 2831, 11023, 6927,
			15119, 19215, 27407, 35599, 39695, 43791, 47887, 64271, 38418, 6400,
			1555, 9747, 13843, 17939, 22035, 26131, 34323, 42515, 46611, 30227,
			62995, 8198, 4103, 43281, 64265, 2177, 14465, 18561, 22657, 26753,
			34945, 39041, 47233, 30849, 46858, 3090, 11282, 7186, 15378, 19474,
			23570, 27666, 35858, 39954, 44050, 31762, 64530, 3091, 11283, 7187,
			15379, 19475, 23571, 27667, 35859, 39955, 44051, 48147, 31763, 64531,
			65535, 65535
		};

		protected static string[] errors = new string[8] { "Expected element", "Invalid character in tag", "No '='", "Invalid character entity", "Invalid attr value", "Empty tag", "No end tag", "Bad entity ref" };

		protected int line;

		protected int col;

		protected int[] twoCharBuff;

		protected bool splitCData;

		public MiniParser()
		{
			twoCharBuff = new int[2];
			splitCData = false;
			Reset();
		}

		public void Reset()
		{
			line = 0;
			col = 0;
		}

		protected static bool StrEquals(string str, StringBuilder sb, int sbStart, int len)
		{
			if (len != str.Length)
			{
				return false;
			}
			for (int i = 0; i < len; i++)
			{
				if (str[i] != sb[sbStart + i])
				{
					return false;
				}
			}
			return true;
		}

		protected void FatalErr(string descr)
		{
			throw new XMLError(descr, line, col);
		}

		protected static int Xlat(int charCode, int state)
		{
			int num = state * INPUT_RANGE;
			int num2 = System.Math.Min(tbl.Length - num, INPUT_RANGE);
			while (--num2 >= 0)
			{
				ushort num3 = tbl[num];
				if (charCode == num3 >> 12)
				{
					return num3 & 0xFFF;
				}
				num++;
			}
			return 4095;
		}

		public void Parse(IReader reader, IHandler handler)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (handler == null)
			{
				handler = new HandlerAdapter();
			}
			AttrListImpl attrListImpl = new AttrListImpl();
			string text = null;
			Stack stack = new Stack();
			string text2 = null;
			line = 1;
			col = 0;
			int num = 0;
			int num2 = 0;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			int num3 = 0;
			handler.OnStartParsing(this);
			while (true)
			{
				col++;
				num = reader.Read();
				if (num == -1)
				{
					break;
				}
				int num4 = "<>/?=&'\"![ ]\t\r\n".IndexOf((char)num) & 0xF;
				switch (num4)
				{
				case 13:
					continue;
				case 12:
					num4 = 10;
					break;
				}
				if (num4 == 14)
				{
					col = 0;
					line++;
					num4 = 10;
				}
				int num5 = Xlat(num4, num2);
				num2 = num5 & 0xFF;
				if (num == 10 && (num2 == 14 || num2 == 15))
				{
					continue;
				}
				num5 >>= 8;
				if (num2 >= 128)
				{
					if (num2 == 255)
					{
						FatalErr("State dispatch error.");
					}
					else
					{
						FatalErr(errors[num2 ^ 0x80]);
					}
				}
				switch (num5)
				{
				case 9:
					break;
				case 0:
					handler.OnStartElement(text2, attrListImpl);
					if (num != 47)
					{
						stack.Push(text2);
					}
					else
					{
						handler.OnEndElement(text2);
					}
					attrListImpl.Clear();
					break;
				case 1:
				{
					text2 = stringBuilder.ToString();
					stringBuilder = new StringBuilder();
					string text6 = null;
					if (stack.Count == 0 || text2 != (text6 = stack.Pop() as string))
					{
						if (text6 == null)
						{
							FatalErr("Tag stack underflow");
						}
						else
						{
							FatalErr($"Expected end tag '{text2}' but found '{text6}'");
						}
					}
					handler.OnEndElement(text2);
					break;
				}
				case 2:
					text2 = stringBuilder.ToString();
					stringBuilder = new StringBuilder();
					if (num != 47 && num != 62)
					{
						break;
					}
					goto case 0;
				case 3:
					text = stringBuilder.ToString();
					stringBuilder = new StringBuilder();
					break;
				case 4:
					if (text == null)
					{
						FatalErr("Internal error.");
					}
					attrListImpl.Add(text, stringBuilder.ToString());
					stringBuilder = new StringBuilder();
					text = null;
					break;
				case 5:
					handler.OnChars(stringBuilder.ToString());
					stringBuilder = new StringBuilder();
					break;
				case 6:
				{
					string text5 = "CDATA[";
					flag2 = false;
					flag3 = false;
					switch (num)
					{
					case 45:
						num = reader.Read();
						if (num != 45)
						{
							FatalErr("Invalid comment");
						}
						col++;
						flag2 = true;
						twoCharBuff[0] = -1;
						twoCharBuff[1] = -1;
						break;
					default:
						flag3 = true;
						num3 = 0;
						break;
					case 91:
					{
						for (int i = 0; i < text5.Length; i++)
						{
							if (reader.Read() != text5[i])
							{
								col += i + 1;
								break;
							}
						}
						col += text5.Length;
						flag = true;
						break;
					}
					}
					break;
				}
				case 7:
				{
					int num20 = 0;
					num = 93;
					while (true)
					{
						switch (num)
						{
						case 93:
							goto IL_033d;
						default:
						{
							for (int k = 0; k < num20; k++)
							{
								stringBuilder.Append(']');
							}
							stringBuilder.Append((char)num);
							num2 = 18;
							break;
						}
						case 62:
						{
							for (int j = 0; j < num20 - 2; j++)
							{
								stringBuilder.Append(']');
							}
							flag = false;
							break;
						}
						}
						break;
						IL_033d:
						num = reader.Read();
						num20++;
					}
					col += num20;
					break;
				}
				case 8:
					FatalErr($"Error {num2}");
					break;
				case 10:
					stringBuilder = new StringBuilder();
					if (num == 60)
					{
						break;
					}
					goto case 11;
				case 11:
					stringBuilder.Append((char)num);
					break;
				case 12:
					if (flag2)
					{
						if (num == 62 && twoCharBuff[0] == 45 && twoCharBuff[1] == 45)
						{
							flag2 = false;
							num2 = 0;
						}
						else
						{
							twoCharBuff[0] = twoCharBuff[1];
							twoCharBuff[1] = num;
						}
					}
					else if (flag3)
					{
						if (num == 60 || num == 62)
						{
							num3 ^= 1;
						}
						if (num == 62 && num3 != 0)
						{
							flag3 = false;
							num2 = 0;
						}
					}
					else
					{
						if (splitCData && stringBuilder.Length > 0 && flag)
						{
							handler.OnChars(stringBuilder.ToString());
							stringBuilder = new StringBuilder();
						}
						flag = false;
						stringBuilder.Append((char)num);
					}
					break;
				case 13:
				{
					num = reader.Read();
					int num6 = col + 1;
					if (num == 35)
					{
						int num7 = 10;
						int num8 = 0;
						int num9 = 0;
						num = reader.Read();
						num6++;
						if (num == 120)
						{
							num = reader.Read();
							num6++;
							num7 = 16;
						}
						NumberStyles style = ((num7 == 16) ? NumberStyles.HexNumber : NumberStyles.Integer);
						while (true)
						{
							int num10 = -1;
							if (char.IsNumber((char)num) || "abcdef".IndexOf(char.ToLower((char)num)) != -1)
							{
								try
								{
									num10 = int.Parse(new string((char)num, 1), style);
								}
								catch (FormatException)
								{
									num10 = -1;
								}
							}
							if (num10 == -1)
							{
								break;
							}
							num8 *= num7;
							num8 += num10;
							num9++;
							num = reader.Read();
							num6++;
						}
						if (num == 59 && num9 > 0)
						{
							stringBuilder.Append((char)num8);
						}
						else
						{
							FatalErr("Bad char ref");
						}
					}
					else
					{
						string text3 = "aglmopqstu";
						string text4 = "&'\"><";
						int num11 = 0;
						int num12 = 15;
						int num13 = 0;
						int length = stringBuilder.Length;
						while (true)
						{
							if (num11 != 15)
							{
								num11 = text3.IndexOf((char)num) & 0xF;
							}
							if (num11 == 15)
							{
								FatalErr(errors[7]);
							}
							stringBuilder.Append((char)num);
							int num14 = "Ｕ㾏侏ཟｸ\ue1f4⊙\ueeff\ueeffｏ"[num11];
							int num15 = (num14 >> 4) & 0xF;
							int num16 = num14 & 0xF;
							int num17 = num14 >> 12;
							int num18 = (num14 >> 8) & 0xF;
							num = reader.Read();
							num6++;
							num11 = 15;
							if (num15 != 15 && num == text3[num15])
							{
								if (num17 < 14)
								{
									num12 = num17;
								}
								num13 = 12;
							}
							else if (num16 != 15 && num == text3[num16])
							{
								if (num18 < 14)
								{
									num12 = num18;
								}
								num13 = 8;
							}
							else if (num == 59)
							{
								if (num12 != 15 && num13 != 0 && ((num14 >> num13) & 0xF) == 14)
								{
									break;
								}
								continue;
							}
							num11 = 0;
						}
						int num19 = num6 - col - 1;
						if (num19 > 0 && num19 < 5 && (StrEquals("amp", stringBuilder, length, num19) || StrEquals("apos", stringBuilder, length, num19) || StrEquals("quot", stringBuilder, length, num19) || StrEquals("lt", stringBuilder, length, num19) || StrEquals("gt", stringBuilder, length, num19)))
						{
							stringBuilder.Length = length;
							stringBuilder.Append(text4[num12]);
						}
						else
						{
							FatalErr(errors[7]);
						}
					}
					col = num6;
					break;
				}
				default:
					FatalErr($"Unexpected action code - {num5}.");
					break;
				}
			}
			if (num2 != 0)
			{
				FatalErr("Unexpected EOF");
			}
			handler.OnEndParsing(this);
		}
	}
	[CLSCompliant(false)]
	public class SecurityParser : MiniParser, MiniParser.IHandler, MiniParser.IReader
	{
		private SecurityElement root;

		private string xmldoc;

		private int pos;

		private SecurityElement current;

		private Stack stack;

		public SecurityParser()
		{
			stack = new Stack();
		}

		public void LoadXml(string xml)
		{
			root = null;
			xmldoc = xml;
			pos = 0;
			stack.Clear();
			Parse(this, this);
		}

		public SecurityElement ToXml()
		{
			return root;
		}

		public int Read()
		{
			if (pos >= xmldoc.Length)
			{
				return -1;
			}
			return xmldoc[pos++];
		}

		public void OnStartParsing(MiniParser parser)
		{
		}

		public void OnStartElement(string name, IAttrList attrs)
		{
			SecurityElement securityElement = new SecurityElement(name);
			if (root == null)
			{
				root = securityElement;
				current = securityElement;
			}
			else
			{
				((SecurityElement)stack.Peek()).AddChild(securityElement);
			}
			stack.Push(securityElement);
			current = securityElement;
			int length = attrs.Length;
			for (int i = 0; i < length; i++)
			{
				current.AddAttribute(attrs.GetName(i), SecurityElement.Escape(attrs.GetValue(i)));
			}
		}

		public void OnEndElement(string name)
		{
			current = (SecurityElement)stack.Pop();
		}

		public void OnChars(string ch)
		{
			current.Text = SecurityElement.Escape(ch);
		}

		public void OnEndParsing(MiniParser parser)
		{
		}
	}
}
namespace Mono.Security
{
	public class ASN1
	{
		private byte m_nTag;

		private byte[] m_aValue;

		private ArrayList elist;

		public int Count
		{
			get
			{
				if (elist == null)
				{
					return 0;
				}
				return elist.Count;
			}
		}

		public byte Tag => m_nTag;

		public int Length
		{
			get
			{
				if (m_aValue != null)
				{
					return m_aValue.Length;
				}
				return 0;
			}
		}

		public byte[] Value
		{
			get
			{
				if (m_aValue == null)
				{
					GetBytes();
				}
				return (byte[])m_aValue.Clone();
			}
			set
			{
				if (value != null)
				{
					m_aValue = (byte[])value.Clone();
				}
			}
		}

		public ASN1 this[int index]
		{
			get
			{
				try
				{
					if (elist == null || index >= elist.Count)
					{
						return null;
					}
					return (ASN1)elist[index];
				}
				catch (ArgumentOutOfRangeException)
				{
					return null;
				}
			}
		}

		public ASN1()
			: this(0, null)
		{
		}

		public ASN1(byte tag)
			: this(tag, null)
		{
		}

		public ASN1(byte tag, byte[] data)
		{
			m_nTag = tag;
			m_aValue = data;
		}

		public ASN1(byte[] data)
		{
			m_nTag = data[0];
			int num = 0;
			int num2 = data[1];
			if (num2 > 128)
			{
				num = num2 - 128;
				num2 = 0;
				for (int i = 0; i < num; i++)
				{
					num2 *= 256;
					num2 += data[i + 2];
				}
			}
			else if (num2 == 128)
			{
				throw new NotSupportedException("Undefined length encoding.");
			}
			m_aValue = new byte[num2];
			Buffer.BlockCopy(data, 2 + num, m_aValue, 0, num2);
			if ((m_nTag & 0x20) == 32)
			{
				int anPos = 0;
				Decode(m_aValue, ref anPos, m_aValue.Length);
			}
		}

		private bool CompareArray(byte[] array1, byte[] array2)
		{
			bool flag = array1.Length == array2.Length;
			if (flag)
			{
				for (int i = 0; i < array1.Length; i++)
				{
					if (array1[i] != array2[i])
					{
						return false;
					}
				}
			}
			return flag;
		}

		public bool Equals(byte[] asn1)
		{
			return CompareArray(GetBytes(), asn1);
		}

		public bool CompareValue(byte[] value)
		{
			return CompareArray(m_aValue, value);
		}

		public ASN1 Add(ASN1 asn1)
		{
			if (asn1 != null)
			{
				if (elist == null)
				{
					elist = new ArrayList();
				}
				elist.Add(asn1);
			}
			return asn1;
		}

		public virtual byte[] GetBytes()
		{
			byte[] array = null;
			if (Count > 0)
			{
				int num = 0;
				ArrayList arrayList = new ArrayList();
				foreach (ASN1 item in elist)
				{
					byte[] bytes = item.GetBytes();
					arrayList.Add(bytes);
					num += bytes.Length;
				}
				array = new byte[num];
				int num2 = 0;
				for (int i = 0; i < elist.Count; i++)
				{
					byte[] array2 = (byte[])arrayList[i];
					Buffer.BlockCopy(array2, 0, array, num2, array2.Length);
					num2 += array2.Length;
				}
			}
			else if (m_aValue != null)
			{
				array = m_aValue;
			}
			int num3 = 0;
			byte[] array3;
			if (array != null)
			{
				int num4 = array.Length;
				if (num4 > 127)
				{
					if (num4 <= 255)
					{
						array3 = new byte[3 + num4];
						Buffer.BlockCopy(array, 0, array3, 3, num4);
						num3 = 129;
						array3[2] = (byte)num4;
					}
					else if (num4 <= 65535)
					{
						array3 = new byte[4 + num4];
						Buffer.BlockCopy(array, 0, array3, 4, num4);
						num3 = 130;
						array3[2] = (byte)(num4 >> 8);
						array3[3] = (byte)num4;
					}
					else if (num4 <= 16777215)
					{
						array3 = new byte[5 + num4];
						Buffer.BlockCopy(array, 0, array3, 5, num4);
						num3 = 131;
						array3[2] = (byte)(num4 >> 16);
						array3[3] = (byte)(num4 >> 8);
						array3[4] = (byte)num4;
					}
					else
					{
						array3 = new byte[6 + num4];
						Buffer.BlockCopy(array, 0, array3, 6, num4);
						num3 = 132;
						array3[2] = (byte)(num4 >> 24);
						array3[3] = (byte)(num4 >> 16);
						array3[4] = (byte)(num4 >> 8);
						array3[5] = (byte)num4;
					}
				}
				else
				{
					array3 = new byte[2 + num4];
					Buffer.BlockCopy(array, 0, array3, 2, num4);
					num3 = num4;
				}
				if (m_aValue == null)
				{
					m_aValue = array;
				}
			}
			else
			{
				array3 = new byte[2];
			}
			array3[0] = m_nTag;
			array3[1] = (byte)num3;
			return array3;
		}

		protected void Decode(byte[] asn1, ref int anPos, int anLength)
		{
			while (anPos < anLength - 1)
			{
				DecodeTLV(asn1, ref anPos, out var tag, out var length, out var content);
				if (tag != 0)
				{
					ASN1 aSN = Add(new ASN1(tag, content));
					if ((tag & 0x20) == 32)
					{
						int anPos2 = anPos;
						aSN.Decode(asn1, ref anPos2, anPos2 + length);
					}
					anPos += length;
				}
			}
		}

		protected void DecodeTLV(byte[] asn1, ref int pos, out byte tag, out int length, out byte[] content)
		{
			tag = asn1[pos++];
			length = asn1[pos++];
			if ((length & 0x80) == 128)
			{
				int num = length & 0x7F;
				length = 0;
				for (int i = 0; i < num; i++)
				{
					length = length * 256 + asn1[pos++];
				}
			}
			content = new byte[length];
			Buffer.BlockCopy(asn1, pos, content, 0, length);
		}

		public ASN1 Element(int index, byte anTag)
		{
			try
			{
				if (elist == null || index >= elist.Count)
				{
					return null;
				}
				ASN1 aSN = (ASN1)elist[index];
				if (aSN.Tag == anTag)
				{
					return aSN;
				}
				return null;
			}
			catch (ArgumentOutOfRangeException)
			{
				return null;
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Tag: {0} {1}", m_nTag.ToString("X2"), Environment.NewLine);
			stringBuilder.AppendFormat("Length: {0} {1}", Value.Length, Environment.NewLine);
			stringBuilder.Append("Value: ");
			stringBuilder.Append(Environment.NewLine);
			for (int i = 0; i < Value.Length; i++)
			{
				stringBuilder.AppendFormat("{0} ", Value[i].ToString("X2"));
				if ((i + 1) % 16 == 0)
				{
					stringBuilder.AppendFormat(Environment.NewLine);
				}
			}
			return stringBuilder.ToString();
		}

		public void SaveToFile(string filename)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			using FileStream fileStream = File.Create(filename);
			byte[] bytes = GetBytes();
			fileStream.Write(bytes, 0, bytes.Length);
		}
	}
	public static class ASN1Convert
	{
		public static ASN1 FromDateTime(DateTime dt)
		{
			if (dt.Year < 2050)
			{
				return new ASN1(23, Encoding.ASCII.GetBytes(dt.ToUniversalTime().ToString("yyMMddHHmmss", CultureInfo.InvariantCulture) + "Z"));
			}
			return new ASN1(24, Encoding.ASCII.GetBytes(dt.ToUniversalTime().ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture) + "Z"));
		}

		public static ASN1 FromInt32(int value)
		{
			byte[] bytes = Mono.Security.BitConverterLE.GetBytes(value);
			Array.Reverse(bytes);
			int i;
			for (i = 0; i < bytes.Length && bytes[i] == 0; i++)
			{
			}
			ASN1 aSN = new ASN1(2);
			switch (i)
			{
			case 0:
				aSN.Value = bytes;
				break;
			case 4:
				aSN.Value = new byte[1];
				break;
			default:
			{
				byte[] array = new byte[4 - i];
				Buffer.BlockCopy(bytes, i, array, 0, array.Length);
				aSN.Value = array;
				break;
			}
			}
			return aSN;
		}

		public static ASN1 FromOid(string oid)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			return new ASN1(CryptoConfig.EncodeOID(oid));
		}

		public static ASN1 FromUnsignedBigInteger(byte[] big)
		{
			if (big == null)
			{
				throw new ArgumentNullException("big");
			}
			if (big[0] >= 128)
			{
				int num = big.Length + 1;
				byte[] array = new byte[num];
				Buffer.BlockCopy(big, 0, array, 1, num - 1);
				big = array;
			}
			return new ASN1(2, big);
		}

		public static int ToInt32(ASN1 asn1)
		{
			if (asn1 == null)
			{
				throw new ArgumentNullException("asn1");
			}
			if (asn1.Tag != 2)
			{
				throw new FormatException("Only integer can be converted");
			}
			int num = 0;
			for (int i = 0; i < asn1.Value.Length; i++)
			{
				num = (num << 8) + asn1.Value[i];
			}
			return num;
		}

		public static string ToOid(ASN1 asn1)
		{
			if (asn1 == null)
			{
				throw new ArgumentNullException("asn1");
			}
			byte[] value = asn1.Value;
			StringBuilder stringBuilder = new StringBuilder();
			byte b = (byte)(value[0] / 40);
			byte b2 = (byte)(value[0] % 40);
			if (b > 2)
			{
				b2 += (byte)((b - 2) * 40);
				b = 2;
			}
			stringBuilder.Append(b.ToString(CultureInfo.InvariantCulture));
			stringBuilder.Append(".");
			stringBuilder.Append(b2.ToString(CultureInfo.InvariantCulture));
			ulong num = 0uL;
			for (b = 1; b < value.Length; b++)
			{
				num = (num << 7) | (byte)(value[b] & 0x7F);
				if ((value[b] & 0x80) != 128)
				{
					stringBuilder.Append(".");
					stringBuilder.Append(num.ToString(CultureInfo.InvariantCulture));
					num = 0uL;
				}
			}
			return stringBuilder.ToString();
		}

		public static DateTime ToDateTime(ASN1 time)
		{
			if (time == null)
			{
				throw new ArgumentNullException("time");
			}
			string text = Encoding.ASCII.GetString(time.Value);
			string format = null;
			switch (text.Length)
			{
			case 11:
				format = "yyMMddHHmmZ";
				break;
			case 13:
				text = ((Convert.ToInt16(text.Substring(0, 2), CultureInfo.InvariantCulture) < 50) ? ("20" + text) : ("19" + text));
				format = "yyyyMMddHHmmssZ";
				break;
			case 15:
				format = "yyyyMMddHHmmssZ";
				break;
			case 17:
			{
				string text2 = ((Convert.ToInt16(text.Substring(0, 2), CultureInfo.InvariantCulture) >= 50) ? "19" : "20");
				char c = ((text[12] == '+') ? '-' : '+');
				text = $"{text2}{text.Substring(0, 12)}{c}{text[13]}{text[14]}:{text[15]}{text[16]}";
				format = "yyyyMMddHHmmsszzz";
				break;
			}
			}
			return DateTime.ParseExact(text, format, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
		}
	}
	internal sealed class BitConverterLE
	{
		private BitConverterLE()
		{
		}

		private unsafe static byte[] GetUShortBytes(byte* bytes)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return new byte[2]
				{
					bytes[1],
					*bytes
				};
			}
			return new byte[2]
			{
				*bytes,
				bytes[1]
			};
		}

		private unsafe static byte[] GetUIntBytes(byte* bytes)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return new byte[4]
				{
					bytes[3],
					bytes[2],
					bytes[1],
					*bytes
				};
			}
			return new byte[4]
			{
				*bytes,
				bytes[1],
				bytes[2],
				bytes[3]
			};
		}

		private unsafe static byte[] GetULongBytes(byte* bytes)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return new byte[8]
				{
					bytes[7],
					bytes[6],
					bytes[5],
					bytes[4],
					bytes[3],
					bytes[2],
					bytes[1],
					*bytes
				};
			}
			return new byte[8]
			{
				*bytes,
				bytes[1],
				bytes[2],
				bytes[3],
				bytes[4],
				bytes[5],
				bytes[6],
				bytes[7]
			};
		}

		internal static byte[] GetBytes(bool value)
		{
			return new byte[1] { (byte)(value ? 1 : 0) };
		}

		internal unsafe static byte[] GetBytes(char value)
		{
			return GetUShortBytes((byte*)(&value));
		}

		internal unsafe static byte[] GetBytes(short value)
		{
			return GetUShortBytes((byte*)(&value));
		}

		internal unsafe static byte[] GetBytes(int value)
		{
			return GetUIntBytes((byte*)(&value));
		}

		internal unsafe static byte[] GetBytes(long value)
		{
			return GetULongBytes((byte*)(&value));
		}

		internal unsafe static byte[] GetBytes(ushort value)
		{
			return GetUShortBytes((byte*)(&value));
		}

		internal unsafe static byte[] GetBytes(uint value)
		{
			return GetUIntBytes((byte*)(&value));
		}

		internal unsafe static byte[] GetBytes(ulong value)
		{
			return GetULongBytes((byte*)(&value));
		}

		internal unsafe static byte[] GetBytes(float value)
		{
			return GetUIntBytes((byte*)(&value));
		}

		internal unsafe static byte[] GetBytes(double value)
		{
			return GetULongBytes((byte*)(&value));
		}

		private unsafe static void UShortFromBytes(byte* dst, byte[] src, int startIndex)
		{
			if (BitConverter.IsLittleEndian)
			{
				*dst = src[startIndex];
				dst[1] = src[startIndex + 1];
			}
			else
			{
				*dst = src[startIndex + 1];
				dst[1] = src[startIndex];
			}
		}

		private unsafe static void UIntFromBytes(byte* dst, byte[] src, int startIndex)
		{
			if (BitConverter.IsLittleEndian)
			{
				*dst = src[startIndex];
				dst[1] = src[startIndex + 1];
				dst[2] = src[startIndex + 2];
				dst[3] = src[startIndex + 3];
			}
			else
			{
				*dst = src[startIndex + 3];
				dst[1] = src[startIndex + 2];
				dst[2] = src[startIndex + 1];
				dst[3] = src[startIndex];
			}
		}

		private unsafe static void ULongFromBytes(byte* dst, byte[] src, int startIndex)
		{
			if (BitConverter.IsLittleEndian)
			{
				for (int i = 0; i < 8; i++)
				{
					dst[i] = src[startIndex + i];
				}
			}
			else
			{
				for (int j = 0; j < 8; j++)
				{
					dst[j] = src[startIndex + (7 - j)];
				}
			}
		}

		internal static bool ToBoolean(byte[] value, int startIndex)
		{
			return value[startIndex] != 0;
		}

		internal unsafe static char ToChar(byte[] value, int startIndex)
		{
			char result = default(char);
			UShortFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		internal unsafe static short ToInt16(byte[] value, int startIndex)
		{
			short result = default(short);
			UShortFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		internal unsafe static int ToInt32(byte[] value, int startIndex)
		{
			int result = default(int);
			UIntFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		internal unsafe static long ToInt64(byte[] value, int startIndex)
		{
			long result = default(long);
			ULongFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		internal unsafe static ushort ToUInt16(byte[] value, int startIndex)
		{
			ushort result = default(ushort);
			UShortFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		internal unsafe static uint ToUInt32(byte[] value, int startIndex)
		{
			uint result = default(uint);
			UIntFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		internal unsafe static ulong ToUInt64(byte[] value, int startIndex)
		{
			ulong result = default(ulong);
			ULongFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		internal unsafe static float ToSingle(byte[] value, int startIndex)
		{
			float result = default(float);
			UIntFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		internal unsafe static double ToDouble(byte[] value, int startIndex)
		{
			double result = default(double);
			ULongFromBytes((byte*)(&result), value, startIndex);
			return result;
		}
	}
	public sealed class PKCS7
	{
		public class Oid
		{
			public const string rsaEncryption = "1.2.840.113549.1.1.1";

			public const string data = "1.2.840.113549.1.7.1";

			public const string signedData = "1.2.840.113549.1.7.2";

			public const string envelopedData = "1.2.840.113549.1.7.3";

			public const string signedAndEnvelopedData = "1.2.840.113549.1.7.4";

			public const string digestedData = "1.2.840.113549.1.7.5";

			public const string encryptedData = "1.2.840.113549.1.7.6";

			public const string contentType = "1.2.840.113549.1.9.3";

			public const string messageDigest = "1.2.840.113549.1.9.4";

			public const string signingTime = "1.2.840.113549.1.9.5";

			public const string countersignature = "1.2.840.113549.1.9.6";
		}

		public class ContentInfo
		{
			private string contentType;

			private ASN1 content;

			public ASN1 ASN1 => GetASN1();

			public ASN1 Content
			{
				get
				{
					return content;
				}
				set
				{
					content = value;
				}
			}

			public string ContentType
			{
				get
				{
					return contentType;
				}
				set
				{
					contentType = value;
				}
			}

			public ContentInfo()
			{
				content = new ASN1(160);
			}

			public ContentInfo(string oid)
				: this()
			{
				contentType = oid;
			}

			public ContentInfo(byte[] data)
				: this(new ASN1(data))
			{
			}

			public ContentInfo(ASN1 asn1)
			{
				if (asn1.Tag != 48 || (asn1.Count < 1 && asn1.Count > 2))
				{
					throw new ArgumentException("Invalid ASN1");
				}
				if (asn1[0].Tag != 6)
				{
					throw new ArgumentException("Invalid contentType");
				}
				contentType = ASN1Convert.ToOid(asn1[0]);
				if (asn1.Count > 1)
				{
					if (asn1[1].Tag != 160)
					{
						throw new ArgumentException("Invalid content");
					}
					content = asn1[1];
				}
			}

			internal ASN1 GetASN1()
			{
				ASN1 aSN = new ASN1(48);
				aSN.Add(ASN1Convert.FromOid(contentType));
				if (content != null && content.Count > 0)
				{
					aSN.Add(content);
				}
				return aSN;
			}

			public byte[] GetBytes()
			{
				return GetASN1().GetBytes();
			}
		}

		public class EncryptedData
		{
			private byte _version;

			private ContentInfo _content;

			private ContentInfo _encryptionAlgorithm;

			private byte[] _encrypted;

			public ASN1 ASN1 => GetASN1();

			public ContentInfo ContentInfo => _content;

			public ContentInfo EncryptionAlgorithm => _encryptionAlgorithm;

			public byte[] EncryptedContent
			{
				get
				{
					if (_encrypted == null)
					{
						return null;
					}
					return (byte[])_encrypted.Clone();
				}
			}

			public byte Version
			{
				get
				{
					return _version;
				}
				set
				{
					_version = value;
				}
			}

			public EncryptedData()
			{
				_version = 0;
			}

			public EncryptedData(byte[] data)
				: this(new ASN1(data))
			{
			}

			public EncryptedData(ASN1 asn1)
				: this()
			{
				if (asn1.Tag != 48 || asn1.Count < 2)
				{
					throw new ArgumentException("Invalid EncryptedData");
				}
				if (asn1[0].Tag != 2)
				{
					throw new ArgumentException("Invalid version");
				}
				_version = asn1[0].Value[0];
				ASN1 aSN = asn1[1];
				if (aSN.Tag != 48)
				{
					throw new ArgumentException("missing EncryptedContentInfo");
				}
				ASN1 aSN2 = aSN[0];
				if (aSN2.Tag != 6)
				{
					throw new ArgumentException("missing EncryptedContentInfo.ContentType");
				}
				_content = new ContentInfo(ASN1Convert.ToOid(aSN2));
				ASN1 aSN3 = aSN[1];
				if (aSN3.Tag != 48)
				{
					throw new ArgumentException("missing EncryptedContentInfo.ContentEncryptionAlgorithmIdentifier");
				}
				_encryptionAlgorithm = new ContentInfo(ASN1Convert.ToOid(aSN3[0]));
				_encryptionAlgorithm.Content = aSN3[1];
				ASN1 aSN4 = aSN[2];
				if (aSN4.Tag != 128)
				{
					throw new ArgumentException("missing EncryptedContentInfo.EncryptedContent");
				}
				_encrypted = aSN4.Value;
			}

			internal ASN1 GetASN1()
			{
				return null;
			}

			public byte[] GetBytes()
			{
				return GetASN1().GetBytes();
			}
		}

		public class EnvelopedData
		{
			private byte _version;

			private ContentInfo _content;

			private ContentInfo _encryptionAlgorithm;

			private ArrayList _recipientInfos;

			private byte[] _encrypted;

			public ArrayList RecipientInfos => _recipientInfos;

			public ASN1 ASN1 => GetASN1();

			public ContentInfo ContentInfo => _content;

			public ContentInfo EncryptionAlgorithm => _encryptionAlgorithm;

			public byte[] EncryptedContent
			{
				get
				{
					if (_encrypted == null)
					{
						return null;
					}
					return (byte[])_encrypted.Clone();
				}
			}

			public byte Version
			{
				get
				{
					return _version;
				}
				set
				{
					_version = value;
				}
			}

			public EnvelopedData()
			{
				_version = 0;
				_content = new ContentInfo();
				_encryptionAlgorithm = new ContentInfo();
				_recipientInfos = new ArrayList();
			}

			public EnvelopedData(byte[] data)
				: this(new ASN1(data))
			{
			}

			public EnvelopedData(ASN1 asn1)
				: this()
			{
				if (asn1[0].Tag != 48 || asn1[0].Count < 3)
				{
					throw new ArgumentException("Invalid EnvelopedData");
				}
				if (asn1[0][0].Tag != 2)
				{
					throw new ArgumentException("Invalid version");
				}
				_version = asn1[0][0].Value[0];
				ASN1 aSN = asn1[0][1];
				if (aSN.Tag != 49)
				{
					throw new ArgumentException("missing RecipientInfos");
				}
				for (int i = 0; i < aSN.Count; i++)
				{
					ASN1 data = aSN[i];
					_recipientInfos.Add(new RecipientInfo(data));
				}
				ASN1 aSN2 = asn1[0][2];
				if (aSN2.Tag != 48)
				{
					throw new ArgumentException("missing EncryptedContentInfo");
				}
				ASN1 aSN3 = aSN2[0];
				if (aSN3.Tag != 6)
				{
					throw new ArgumentException("missing EncryptedContentInfo.ContentType");
				}
				_content = new ContentInfo(ASN1Convert.ToOid(aSN3));
				ASN1 aSN4 = aSN2[1];
				if (aSN4.Tag != 48)
				{
					throw new ArgumentException("missing EncryptedContentInfo.ContentEncryptionAlgorithmIdentifier");
				}
				_encryptionAlgorithm = new ContentInfo(ASN1Convert.ToOid(aSN4[0]));
				_encryptionAlgorithm.Content = aSN4[1];
				ASN1 aSN5 = aSN2[2];
				if (aSN5.Tag != 128)
				{
					throw new ArgumentException("missing EncryptedContentInfo.EncryptedContent");
				}
				_encrypted = aSN5.Value;
			}

			internal ASN1 GetASN1()
			{
				return new ASN1(48);
			}

			public byte[] GetBytes()
			{
				return GetASN1().GetBytes();
			}
		}

		public class RecipientInfo
		{
			private int _version;

			private string _oid;

			private byte[] _key;

			private byte[] _ski;

			private string _issuer;

			private byte[] _serial;

			public string Oid => _oid;

			public byte[] Key
			{
				get
				{
					if (_key == null)
					{
						return null;
					}
					return (byte[])_key.Clone();
				}
			}

			public byte[] SubjectKeyIdentifier
			{
				get
				{
					if (_ski == null)
					{
						return null;
					}
					return (byte[])_ski.Clone();
				}
			}

			public string Issuer => _issuer;

			public byte[] Serial
			{
				get
				{
					if (_serial == null)
					{
						return null;
					}
					return (byte[])_serial.Clone();
				}
			}

			public int Version => _version;

			public RecipientInfo()
			{
			}

			public RecipientInfo(ASN1 data)
			{
				if (data.Tag != 48)
				{
					throw new ArgumentException("Invalid RecipientInfo");
				}
				ASN1 aSN = data[0];
				if (aSN.Tag != 2)
				{
					throw new ArgumentException("missing Version");
				}
				_version = aSN.Value[0];
				ASN1 aSN2 = data[1];
				if (aSN2.Tag == 128 && _version == 3)
				{
					_ski = aSN2.Value;
				}
				else
				{
					_issuer = X501.ToString(aSN2[0]);
					_serial = aSN2[1].Value;
				}
				ASN1 aSN3 = data[2];
				_oid = ASN1Convert.ToOid(aSN3[0]);
				ASN1 aSN4 = data[3];
				_key = aSN4.Value;
			}
		}

		public class SignedData
		{
			private byte version;

			private string hashAlgorithm;

			private ContentInfo contentInfo;

			private Mono.Security.X509.X509CertificateCollection certs;

			private ArrayList crls;

			private SignerInfo signerInfo;

			private bool mda;

			private bool signed;

			public ASN1 ASN1 => GetASN1();

			public Mono.Security.X509.X509CertificateCollection Certificates => certs;

			public ContentInfo ContentInfo => contentInfo;

			public ArrayList Crls => crls;

			public string HashName
			{
				get
				{
					return hashAlgorithm;
				}
				set
				{
					hashAlgorithm = value;
					signerInfo.HashName = value;
				}
			}

			public SignerInfo SignerInfo => signerInfo;

			public byte Version
			{
				get
				{
					return version;
				}
				set
				{
					version = value;
				}
			}

			public bool UseAuthenticatedAttributes
			{
				get
				{
					return mda;
				}
				set
				{
					mda = value;
				}
			}

			public SignedData()
			{
				version = 1;
				contentInfo = new ContentInfo();
				certs = new Mono.Security.X509.X509CertificateCollection();
				crls = new ArrayList();
				signerInfo = new SignerInfo();
				mda = true;
				signed = false;
			}

			public SignedData(byte[] data)
				: this(new ASN1(data))
			{
			}

			public SignedData(ASN1 asn1)
			{
				if (asn1[0].Tag != 48 || asn1[0].Count < 4)
				{
					throw new ArgumentException("Invalid SignedData");
				}
				if (asn1[0][0].Tag != 2)
				{
					throw new ArgumentException("Invalid version");
				}
				version = asn1[0][0].Value[0];
				contentInfo = new ContentInfo(asn1[0][2]);
				int num = 3;
				certs = new Mono.Security.X509.X509CertificateCollection();
				if (asn1[0][num].Tag == 160)
				{
					for (int i = 0; i < asn1[0][num].Count; i++)
					{
						certs.Add(new Mono.Security.X509.X509Certificate(asn1[0][num][i].GetBytes()));
					}
					num++;
				}
				crls = new ArrayList();
				if (asn1[0][num].Tag == 161)
				{
					for (int j = 0; j < asn1[0][num].Count; j++)
					{
						crls.Add(asn1[0][num][j].GetBytes());
					}
					num++;
				}
				if (asn1[0][num].Count > 0)
				{
					signerInfo = new SignerInfo(asn1[0][num]);
				}
				else
				{
					signerInfo = new SignerInfo();
				}
				if (signerInfo.HashName != null)
				{
					HashName = OidToName(signerInfo.HashName);
				}
				mda = signerInfo.AuthenticatedAttributes.Count > 0;
			}

			public bool VerifySignature(AsymmetricAlgorithm aa)
			{
				if (aa == null)
				{
					return false;
				}
				RSAPKCS1SignatureDeformatter rSAPKCS1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(aa);
				rSAPKCS1SignatureDeformatter.SetHashAlgorithm(this.hashAlgorithm);
				HashAlgorithm hashAlgorithm = HashAlgorithm.Create(this.hashAlgorithm);
				byte[] signature = signerInfo.Signature;
				byte[] array = null;
				if (mda)
				{
					ASN1 aSN = new ASN1(49);
					foreach (ASN1 authenticatedAttribute in signerInfo.AuthenticatedAttributes)
					{
						aSN.Add(authenticatedAttribute);
					}
					array = hashAlgorithm.ComputeHash(aSN.GetBytes());
				}
				else
				{
					array = hashAlgorithm.ComputeHash(contentInfo.Content[0].Value);
				}
				if (array != null && signature != null)
				{
					return rSAPKCS1SignatureDeformatter.VerifySignature(array, signature);
				}
				return false;
			}

			internal string OidToName(string oid)
			{
				return oid switch
				{
					"1.3.14.3.2.26" => "SHA1", 
					"1.2.840.113549.2.2" => "MD2", 
					"1.2.840.113549.2.5" => "MD5", 
					"2.16.840.1.101.3.4.1" => "SHA256", 
					"2.16.840.1.101.3.4.2" => "SHA384", 
					"2.16.840.1.101.3.4.3" => "SHA512", 
					_ => oid, 
				};
			}

			internal ASN1 GetASN1()
			{
				ASN1 aSN = new ASN1(48);
				byte[] data = new byte[1] { version };
				aSN.Add(new ASN1(2, data));
				ASN1 aSN2 = aSN.Add(new ASN1(49));
				if (hashAlgorithm != null)
				{
					string oid = CryptoConfig.MapNameToOID(hashAlgorithm);
					aSN2.Add(AlgorithmIdentifier(oid));
				}
				ASN1 aSN3 = contentInfo.ASN1;
				aSN.Add(aSN3);
				if (!signed && hashAlgorithm != null)
				{
					if (mda)
					{
						ASN1 value = Attribute("1.2.840.113549.1.9.3", aSN3[0]);
						signerInfo.AuthenticatedAttributes.Add(value);
						byte[] data2 = HashAlgorithm.Create(hashAlgorithm).ComputeHash(aSN3[1][0].Value);
						ASN1 aSN4 = new ASN1(48);
						ASN1 value2 = Attribute("1.2.840.113549.1.9.4", aSN4.Add(new ASN1(4, data2)));
						signerInfo.AuthenticatedAttributes.Add(value2);
					}
					else
					{
						RSAPKCS1SignatureFormatter rSAPKCS1SignatureFormatter = new RSAPKCS1SignatureFormatter(signerInfo.Key);
						rSAPKCS1SignatureFormatter.SetHashAlgorithm(hashAlgorithm);
						byte[] rgbHash = HashAlgorithm.Create(hashAlgorithm).ComputeHash(aSN3[1][0].Value);
						signerInfo.Signature = rSAPKCS1SignatureFormatter.CreateSignature(rgbHash);
					}
					signed = true;
				}
				if (certs.Count > 0)
				{
					ASN1 aSN5 = aSN.Add(new ASN1(160));
					foreach (Mono.Security.X509.X509Certificate cert in certs)
					{
						aSN5.Add(new ASN1(cert.RawData));
					}
				}
				if (crls.Count > 0)
				{
					ASN1 aSN6 = aSN.Add(new ASN1(161));
					foreach (byte[] crl in crls)
					{
						aSN6.Add(new ASN1(crl));
					}
				}
				ASN1 aSN7 = aSN.Add(new ASN1(49));
				if (signerInfo.Key != null)
				{
					aSN7.Add(signerInfo.ASN1);
				}
				return aSN;
			}

			public byte[] GetBytes()
			{
				return GetASN1().GetBytes();
			}
		}

		public class SignerInfo
		{
			private byte version;

			private Mono.Security.X509.X509Certificate x509;

			private string hashAlgorithm;

			private AsymmetricAlgorithm key;

			private ArrayList authenticatedAttributes;

			private ArrayList unauthenticatedAttributes;

			private byte[] signature;

			private string issuer;

			private byte[] serial;

			private byte[] ski;

			public string IssuerName => issuer;

			public byte[] SerialNumber
			{
				get
				{
					if (serial == null)
					{
						return null;
					}
					return (byte[])serial.Clone();
				}
			}

			public byte[] SubjectKeyIdentifier
			{
				get
				{
					if (ski == null)
					{
						return null;
					}
					return (byte[])ski.Clone();
				}
			}

			public ASN1 ASN1 => GetASN1();

			public ArrayList AuthenticatedAttributes => authenticatedAttributes;

			public Mono.Security.X509.X509Certificate Certificate
			{
				get
				{
					return x509;
				}
				set
				{
					x509 = value;
				}
			}

			public string HashName
			{
				get
				{
					return hashAlgorithm;
				}
				set
				{
					hashAlgorithm = value;
				}
			}

			public AsymmetricAlgorithm Key
			{
				get
				{
					return key;
				}
				set
				{
					key = value;
				}
			}

			public byte[] Signature
			{
				get
				{
					if (signature == null)
					{
						return null;
					}
					return (byte[])signature.Clone();
				}
				set
				{
					if (value != null)
					{
						signature = (byte[])value.Clone();
					}
				}
			}

			public ArrayList UnauthenticatedAttributes => unauthenticatedAttributes;

			public byte Version
			{
				get
				{
					return version;
				}
				set
				{
					version = value;
				}
			}

			public SignerInfo()
			{
				version = 1;
				authenticatedAttributes = new ArrayList();
				unauthenticatedAttributes = new ArrayList();
			}

			public SignerInfo(byte[] data)
				: this(new ASN1(data))
			{
			}

			public SignerInfo(ASN1 asn1)
				: this()
			{
				if (asn1[0].Tag != 48 || asn1[0].Count < 5)
				{
					throw new ArgumentException("Invalid SignedData");
				}
				if (asn1[0][0].Tag != 2)
				{
					throw new ArgumentException("Invalid version");
				}
				version = asn1[0][0].Value[0];
				ASN1 aSN = asn1[0][1];
				if (aSN.Tag == 128 && version == 3)
				{
					ski = aSN.Value;
				}
				else
				{
					issuer = X501.ToString(aSN[0]);
					serial = aSN[1].Value;
				}
				ASN1 aSN2 = asn1[0][2];
				hashAlgorithm = ASN1Convert.ToOid(aSN2[0]);
				int num = 3;
				ASN1 aSN3 = asn1[0][num];
				if (aSN3.Tag == 160)
				{
					num++;
					for (int i = 0; i < aSN3.Count; i++)
					{
						authenticatedAttributes.Add(aSN3[i]);
					}
				}
				num++;
				ASN1 aSN4 = asn1[0][num++];
				if (aSN4.Tag == 4)
				{
					signature = aSN4.Value;
				}
				ASN1 aSN5 = asn1[0][num];
				if (aSN5 != null && aSN5.Tag == 161)
				{
					for (int j = 0; j < aSN5.Count; j++)
					{
						unauthenticatedAttributes.Add(aSN5[j]);
					}
				}
			}

			internal ASN1 GetASN1()
			{
				if (key == null || hashAlgorithm == null)
				{
					return null;
				}
				byte[] data = new byte[1] { version };
				ASN1 aSN = new ASN1(48);
				aSN.Add(new ASN1(2, data));
				aSN.Add(IssuerAndSerialNumber(x509));
				string oid = CryptoConfig.MapNameToOID(hashAlgorithm);
				aSN.Add(AlgorithmIdentifier(oid));
				ASN1 aSN2 = null;
				if (authenticatedAttributes.Count > 0)
				{
					aSN2 = aSN.Add(new ASN1(160));
					authenticatedAttributes.Sort(new SortedSet());
					foreach (ASN1 authenticatedAttribute in authenticatedAttributes)
					{
						aSN2.Add(authenticatedAttribute);
					}
				}
				if (key is RSA)
				{
					aSN.Add(AlgorithmIdentifier("1.2.840.113549.1.1.1"));
					if (aSN2 != null)
					{
						RSAPKCS1SignatureFormatter rSAPKCS1SignatureFormatter = new RSAPKCS1SignatureFormatter(key);
						rSAPKCS1SignatureFormatter.SetHashAlgorithm(hashAlgorithm);
						byte[] bytes = aSN2.GetBytes();
						bytes[0] = 49;
						byte[] rgbHash = HashAlgorithm.Create(hashAlgorithm).ComputeHash(bytes);
						signature = rSAPKCS1SignatureFormatter.CreateSignature(rgbHash);
					}
					aSN.Add(new ASN1(4, signature));
					if (unauthenticatedAttributes.Count > 0)
					{
						ASN1 aSN3 = aSN.Add(new ASN1(161));
						unauthenticatedAttributes.Sort(new SortedSet());
						foreach (ASN1 unauthenticatedAttribute in unauthenticatedAttributes)
						{
							aSN3.Add(unauthenticatedAttribute);
						}
					}
					return aSN;
				}
				if (key is DSA)
				{
					throw new NotImplementedException("not yet");
				}
				throw new CryptographicException("Unknown assymetric algorithm");
			}

			public byte[] GetBytes()
			{
				return GetASN1().GetBytes();
			}
		}

		internal class SortedSet : IComparer
		{
			public int Compare(object x, object y)
			{
				if (x == null)
				{
					if (y != null)
					{
						return -1;
					}
					return 0;
				}
				if (y == null)
				{
					return 1;
				}
				ASN1 obj = x as ASN1;
				ASN1 aSN = y as ASN1;
				if (obj == null || aSN == null)
				{
					throw new ArgumentException(global::Locale.GetText("Invalid objects."));
				}
				byte[] bytes = obj.GetBytes();
				byte[] bytes2 = aSN.GetBytes();
				for (int i = 0; i < bytes.Length && i != bytes2.Length; i++)
				{
					if (bytes[i] != bytes2[i])
					{
						if (bytes[i] >= bytes2[i])
						{
							return 1;
						}
						return -1;
					}
				}
				if (bytes.Length > bytes2.Length)
				{
					return 1;
				}
				if (bytes.Length < bytes2.Length)
				{
					return -1;
				}
				return 0;
			}
		}

		private PKCS7()
		{
		}

		public static ASN1 Attribute(string oid, ASN1 value)
		{
			ASN1 aSN = new ASN1(48);
			aSN.Add(ASN1Convert.FromOid(oid));
			aSN.Add(new ASN1(49)).Add(value);
			return aSN;
		}

		public static ASN1 AlgorithmIdentifier(string oid)
		{
			ASN1 aSN = new ASN1(48);
			aSN.Add(ASN1Convert.FromOid(oid));
			aSN.Add(new ASN1(5));
			return aSN;
		}

		public static ASN1 AlgorithmIdentifier(string oid, ASN1 parameters)
		{
			ASN1 aSN = new ASN1(48);
			aSN.Add(ASN1Convert.FromOid(oid));
			aSN.Add(parameters);
			return aSN;
		}

		public static ASN1 IssuerAndSerialNumber(Mono.Security.X509.X509Certificate x509)
		{
			ASN1 asn = null;
			ASN1 asn2 = null;
			ASN1 aSN = new ASN1(x509.RawData);
			int num = 0;
			bool flag = false;
			while (num < aSN[0].Count)
			{
				ASN1 aSN2 = aSN[0][num++];
				if (aSN2.Tag == 2)
				{
					asn2 = aSN2;
				}
				else if (aSN2.Tag == 48)
				{
					if (flag)
					{
						asn = aSN2;
						break;
					}
					flag = true;
				}
			}
			ASN1 aSN3 = new ASN1(48);
			aSN3.Add(asn);
			aSN3.Add(asn2);
			return aSN3;
		}
	}
	public sealed class StrongName
	{
		internal class StrongNameSignature
		{
			private byte[] hash;

			private byte[] signature;

			private uint signaturePosition;

			private uint signatureLength;

			private uint metadataPosition;

			private uint metadataLength;

			private byte cliFlag;

			private uint cliFlagPosition;

			public byte[] Hash
			{
				get
				{
					return hash;
				}
				set
				{
					hash = value;
				}
			}

			public byte[] Signature
			{
				get
				{
					return signature;
				}
				set
				{
					signature = value;
				}
			}

			public uint MetadataPosition
			{
				get
				{
					return metadataPosition;
				}
				set
				{
					metadataPosition = value;
				}
			}

			public uint MetadataLength
			{
				get
				{
					return metadataLength;
				}
				set
				{
					metadataLength = value;
				}
			}

			public uint SignaturePosition
			{
				get
				{
					return signaturePosition;
				}
				set
				{
					signaturePosition = value;
				}
			}

			public uint SignatureLength
			{
				get
				{
					return signatureLength;
				}
				set
				{
					signatureLength = value;
				}
			}

			public byte CliFlag
			{
				get
				{
					return cliFlag;
				}
				set
				{
					cliFlag = value;
				}
			}

			public uint CliFlagPosition
			{
				get
				{
					return cliFlagPosition;
				}
				set
				{
					cliFlagPosition = value;
				}
			}
		}

		internal enum StrongNameOptions
		{
			Metadata,
			Signature
		}

		private RSA rsa;

		private byte[] publicKey;

		private byte[] keyToken;

		private string tokenAlgorithm;

		public bool CanSign
		{
			get
			{
				if (rsa == null)
				{
					return false;
				}
				if (RSA is RSAManaged)
				{
					return !(rsa as RSAManaged).PublicOnly;
				}
				try
				{
					RSAParameters rSAParameters = rsa.ExportParameters(includePrivateParameters: true);
					return rSAParameters.D != null && rSAParameters.P != null && rSAParameters.Q != null;
				}
				catch (CryptographicException)
				{
					return false;
				}
			}
		}

		public RSA RSA
		{
			get
			{
				if (rsa == null)
				{
					rsa = RSA.Create();
				}
				return rsa;
			}
			set
			{
				rsa = value;
				InvalidateCache();
			}
		}

		public byte[] PublicKey
		{
			get
			{
				if (publicKey == null)
				{
					byte[] array = CryptoConvert.ToCapiKeyBlob(rsa, includePrivateKey: false);
					publicKey = new byte[32 + (rsa.KeySize >> 3)];
					publicKey[0] = array[4];
					publicKey[1] = array[5];
					publicKey[2] = array[6];
					publicKey[3] = array[7];
					publicKey[4] = 4;
					publicKey[5] = 128;
					publicKey[6] = 0;
					publicKey[7] = 0;
					byte[] bytes = Mono.Security.BitConverterLE.GetBytes(publicKey.Length - 12);
					publicKey[8] = bytes[0];
					publicKey[9] = bytes[1];
					publicKey[10] = bytes[2];
					publicKey[11] = bytes[3];
					publicKey[12] = 6;
					Buffer.BlockCopy(array, 1, publicKey, 13, publicKey.Length - 13);
					publicKey[23] = 49;
				}
				return (byte[])publicKey.Clone();
			}
		}

		public byte[] PublicKeyToken
		{
			get
			{
				if (keyToken == null)
				{
					byte[] array = PublicKey;
					if (array == null)
					{
						return null;
					}
					byte[] array2 = GetHashAlgorithm(TokenAlgorithm).ComputeHash(array);
					keyToken = new byte[8];
					Buffer.BlockCopy(array2, array2.Length - 8, keyToken, 0, 8);
					Array.Reverse(keyToken, 0, 8);
				}
				return (byte[])keyToken.Clone();
			}
		}

		public string TokenAlgorithm
		{
			get
			{
				if (tokenAlgorithm == null)
				{
					tokenAlgorithm = "SHA1";
				}
				return tokenAlgorithm;
			}
			set
			{
				string text = value.ToUpper(CultureInfo.InvariantCulture);
				if (text == "SHA1" || text == "MD5")
				{
					tokenAlgorithm = value;
					InvalidateCache();
					return;
				}
				throw new ArgumentException("Unsupported hash algorithm for token");
			}
		}

		public StrongName()
		{
		}

		public StrongName(int keySize)
		{
			rsa = new RSAManaged(keySize);
		}

		public StrongName(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (data.Length == 16)
			{
				int num = 0;
				int num2 = 0;
				while (num < data.Length)
				{
					num2 += data[num++];
				}
				if (num2 == 4)
				{
					publicKey = (byte[])data.Clone();
				}
			}
			else
			{
				RSA = CryptoConvert.FromCapiKeyBlob(data);
				if (rsa == null)
				{
					throw new ArgumentException("data isn't a correctly encoded RSA public key");
				}
			}
		}

		public StrongName(RSA rsa)
		{
			if (rsa == null)
			{
				throw new ArgumentNullException("rsa");
			}
			RSA = rsa;
		}

		private void InvalidateCache()
		{
			publicKey = null;
			keyToken = null;
		}

		private static HashAlgorithm GetHashAlgorithm(string algorithm)
		{
			return HashAlgorithm.Create(algorithm);
		}

		public byte[] GetBytes()
		{
			return CryptoConvert.ToCapiPrivateKeyBlob(RSA);
		}

		private uint RVAtoPosition(uint r, int sections, byte[] headers)
		{
			for (int i = 0; i < sections; i++)
			{
				uint num = Mono.Security.BitConverterLE.ToUInt32(headers, i * 40 + 20);
				uint num2 = Mono.Security.BitConverterLE.ToUInt32(headers, i * 40 + 12);
				int num3 = (int)Mono.Security.BitConverterLE.ToUInt32(headers, i * 40 + 8);
				if (num2 <= r && r < num2 + num3)
				{
					return num + r - num2;
				}
			}
			return 0u;
		}

		private static StrongNameSignature Error(string a)
		{
			return null;
		}

		private static byte[] ReadMore(Stream stream, byte[] a, int newSize)
		{
			int num = a.Length;
			Array.Resize(ref a, newSize);
			if (newSize <= num)
			{
				return a;
			}
			int num2 = newSize - num;
			if (stream.Read(a, num, num2) != num2)
			{
				return null;
			}
			return a;
		}

		internal StrongNameSignature StrongHash(Stream stream, StrongNameOptions options)
		{
			byte[] array = new byte[64];
			int num = 0;
			int num2 = stream.Read(array, 0, 64);
			if (num2 == 64 && array[0] == 77 && array[1] == 90)
			{
				num = Mono.Security.BitConverterLE.ToInt32(array, 60);
				if (num < 64)
				{
					return Error("peHeader_lt_64");
				}
				array = ReadMore(stream, array, num);
				if (array == null)
				{
					return Error("read_mz2_failed");
				}
			}
			else
			{
				if (num2 < 4 || array[0] != 80 || array[1] != 69 || array[2] != 0 || array[3] != 0)
				{
					return Error("read_mz_or_mzsig_failed");
				}
				stream.Position = 0L;
				array = new byte[0];
			}
			int num3 = 2;
			int num4 = 24 + num3;
			byte[] array2 = new byte[num4];
			if (stream.Read(array2, 0, num4) != num4 || array2[0] != 80 || array2[1] != 69 || array2[2] != 0 || array2[3] != 0)
			{
				return Error("read_minimumHeadersSize_or_pesig_failed");
			}
			num3 = Mono.Security.BitConverterLE.ToUInt16(array2, 20);
			if (num3 < 2)
			{
				return Error($"sizeOfOptionalHeader_lt_2 ${num3}");
			}
			int num5 = 24 + num3;
			if (num5 < 24)
			{
				return Error("headers_overflow");
			}
			array2 = ReadMore(stream, array2, num5);
			if (array2 == null)
			{
				return Error("read_pe2_failed");
			}
			uint num6 = Mono.Security.BitConverterLE.ToUInt16(array2, 24);
			int num7 = 0;
			bool flag = false;
			switch (num6)
			{
			case 523u:
				num7 = 16;
				break;
			case 263u:
				flag = true;
				break;
			default:
				return Error("bad_magic_value");
			case 267u:
				break;
			}
			uint num8 = 0u;
			if (!flag)
			{
				if (num3 >= 116 + num7 + 4)
				{
					num8 = Mono.Security.BitConverterLE.ToUInt32(array2, 116 + num7);
				}
				for (int i = 64; i < num3 && i < 68; i++)
				{
					array2[24 + i] = 0;
				}
				for (int j = 128 + num7; j < num3 && j < 136 + num7; j++)
				{
					array2[24 + j] = 0;
				}
			}
			int num9 = Mono.Security.BitConverterLE.ToUInt16(array2, 6);
			byte[] array3 = new byte[num9 * 40];
			if (stream.Read(array3, 0, array3.Length) != array3.Length)
			{
				return Error("read_section_headers_failed");
			}
			uint num10 = 0u;
			uint num11 = 0u;
			uint num12 = 0u;
			uint num13 = 0u;
			if (15 < num8 && num3 >= 216 + num7)
			{
				uint r = Mono.Security.BitConverterLE.ToUInt32(array2, 232 + num7);
				uint num14 = RVAtoPosition(r, num9, array3);
				int num15 = Mono.Security.BitConverterLE.ToInt32(array2, 236 + num7);
				byte[] array4 = new byte[num15];
				stream.Position = num14;
				if (stream.Read(array4, 0, num15) != num15)
				{
					return Error("read_cli_header_failed");
				}
				uint r2 = Mono.Security.BitConverterLE.ToUInt32(array4, 32);
				num10 = RVAtoPosition(r2, num9, array3);
				num11 = Mono.Security.BitConverterLE.ToUInt32(array4, 36);
				uint r3 = Mono.Security.BitConverterLE.ToUInt32(array4, 8);
				num12 = RVAtoPosition(r3, num9, array3);
				num13 = Mono.Security.BitConverterLE.ToUInt32(array4, 12);
			}
			StrongNameSignature strongNameSignature = new StrongNameSignature();
			strongNameSignature.SignaturePosition = num10;
			strongNameSignature.SignatureLength = num11;
			strongNameSignature.MetadataPosition = num12;
			strongNameSignature.MetadataLength = num13;
			using HashAlgorithm hashAlgorithm = HashAlgorithm.Create(TokenAlgorithm);
			if (options == StrongNameOptions.Metadata)
			{
				hashAlgorithm.Initialize();
				byte[] buffer = new byte[num13];
				stream.Position = num12;
				if (stream.Read(buffer, 0, (int)num13) != (int)num13)
				{
					return Error("read_cli_metadata_failed");
				}
				strongNameSignature.Hash = hashAlgorithm.ComputeHash(buffer);
				return strongNameSignature;
			}
			using (CryptoStream cryptoStream = new CryptoStream(Stream.Null, hashAlgorithm, CryptoStreamMode.Write))
			{
				cryptoStream.Write(array, 0, array.Length);
				cryptoStream.Write(array2, 0, array2.Length);
				cryptoStream.Write(array3, 0, array3.Length);
				for (int k = 0; k < num9; k++)
				{
					uint num16 = Mono.Security.BitConverterLE.ToUInt32(array3, k * 40 + 20);
					int num17 = Mono.Security.BitConverterLE.ToInt32(array3, k * 40 + 16);
					byte[] array5 = new byte[num17];
					stream.Position = num16;
					if (stream.Read(array5, 0, num17) != num17)
					{
						return Error("read_section_failed");
					}
					if (num16 <= num10 && num10 < (uint)((int)num16 + num17))
					{
						int num18 = (int)(num10 - num16);
						if (num18 > 0)
						{
							cryptoStream.Write(array5, 0, num18);
						}
						strongNameSignature.Signature = new byte[num11];
						Buffer.BlockCopy(array5, num18, strongNameSignature.Signature, 0, (int)num11);
						Array.Reverse(strongNameSignature.Signature);
						int num19 = (int)(num18 + num11);
						int num20 = num17 - num19;
						if (num20 > 0)
						{
							cryptoStream.Write(array5, num19, num20);
						}
					}
					else
					{
						cryptoStream.Write(array5, 0, num17);
					}
				}
			}
			strongNameSignature.Hash = hashAlgorithm.Hash;
			return strongNameSignature;
		}

		public byte[] Hash(string fileName)
		{
			using FileStream stream = File.OpenRead(fileName);
			return StrongHash(stream, StrongNameOptions.Metadata).Hash;
		}

		public bool Sign(string fileName)
		{
			StrongNameSignature strongNameSignature;
			using (FileStream stream = File.OpenRead(fileName))
			{
				strongNameSignature = StrongHash(stream, StrongNameOptions.Signature);
			}
			if (strongNameSignature.Hash == null)
			{
				return false;
			}
			byte[] array = null;
			try
			{
				RSAPKCS1SignatureFormatter rSAPKCS1SignatureFormatter = new RSAPKCS1SignatureFormatter(rsa);
				rSAPKCS1SignatureFormatter.SetHashAlgorithm(TokenAlgorithm);
				array = rSAPKCS1SignatureFormatter.CreateSignature(strongNameSignature.Hash);
				Array.Reverse(array);
			}
			catch (CryptographicException)
			{
				return false;
			}
			using (FileStream fileStream = File.OpenWrite(fileName))
			{
				fileStream.Position = strongNameSignature.SignaturePosition;
				fileStream.Write(array, 0, array.Length);
			}
			return true;
		}

		public bool Verify(string fileName)
		{
			using FileStream stream = File.OpenRead(fileName);
			return Verify(stream);
		}

		public bool Verify(Stream stream)
		{
			StrongNameSignature strongNameSignature = StrongHash(stream, StrongNameOptions.Signature);
			if (strongNameSignature.Hash == null)
			{
				return false;
			}
			try
			{
				AssemblyHashAlgorithm algorithm = AssemblyHashAlgorithm.SHA1;
				if (tokenAlgorithm == "MD5")
				{
					algorithm = AssemblyHashAlgorithm.MD5;
				}
				return Verify(rsa, algorithm, strongNameSignature.Hash, strongNameSignature.Signature);
			}
			catch (CryptographicException)
			{
				return false;
			}
		}

		private static bool Verify(RSA rsa, AssemblyHashAlgorithm algorithm, byte[] hash, byte[] signature)
		{
			RSAPKCS1SignatureDeformatter rSAPKCS1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
			switch (algorithm)
			{
			case AssemblyHashAlgorithm.MD5:
				rSAPKCS1SignatureDeformatter.SetHashAlgorithm("MD5");
				break;
			default:
				rSAPKCS1SignatureDeformatter.SetHashAlgorithm("SHA1");
				break;
			}
			return rSAPKCS1SignatureDeformatter.VerifySignature(hash, signature);
		}
	}
}
namespace Mono.Security.X509
{
	public class PKCS5
	{
		public const string pbeWithMD2AndDESCBC = "1.2.840.113549.1.5.1";

		public const string pbeWithMD5AndDESCBC = "1.2.840.113549.1.5.3";

		public const string pbeWithMD2AndRC2CBC = "1.2.840.113549.1.5.4";

		public const string pbeWithMD5AndRC2CBC = "1.2.840.113549.1.5.6";

		public const string pbeWithSHA1AndDESCBC = "1.2.840.113549.1.5.10";

		public const string pbeWithSHA1AndRC2CBC = "1.2.840.113549.1.5.11";
	}
	public class PKCS9
	{
		public const string friendlyName = "1.2.840.113549.1.9.20";

		public const string localKeyId = "1.2.840.113549.1.9.21";
	}
	internal class SafeBag
	{
		private string _bagOID;

		private ASN1 _asn1;

		public string BagOID => _bagOID;

		public ASN1 ASN1 => _asn1;

		public SafeBag(string bagOID, ASN1 asn1)
		{
			_bagOID = bagOID;
			_asn1 = asn1;
		}
	}
	public class PKCS12 : ICloneable
	{
		public class DeriveBytes
		{
			public enum Purpose
			{
				Key,
				IV,
				MAC
			}

			private static byte[] keyDiversifier = new byte[64]
			{
				1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
				1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
				1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
				1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
				1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
				1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
				1, 1, 1, 1
			};

			private static byte[] ivDiversifier = new byte[64]
			{
				2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
				2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
				2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
				2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
				2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
				2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
				2, 2, 2, 2
			};

			private static byte[] macDiversifier = new byte[64]
			{
				3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
				3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
				3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
				3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
				3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
				3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
				3, 3, 3, 3
			};

			private string _hashName;

			private int _iterations;

			private byte[] _password;

			private byte[] _salt;

			public string HashName
			{
				get
				{
					return _hashName;
				}
				set
				{
					_hashName = value;
				}
			}

			public int IterationCount
			{
				get
				{
					return _iterations;
				}
				set
				{
					_iterations = value;
				}
			}

			public byte[] Password
			{
				get
				{
					return (byte[])_password.Clone();
				}
				set
				{
					if (value == null)
					{
						_password = new byte[0];
					}
					else
					{
						_password = (byte[])value.Clone();
					}
				}
			}

			public byte[] Salt
			{
				get
				{
					return (byte[])_salt.Clone();
				}
				set
				{
					if (value != null)
					{
						_salt = (byte[])value.Clone();
					}
					else
					{
						_salt = null;
					}
				}
			}

			private void Adjust(byte[] a, int aOff, byte[] b)
			{
				int num = (b[^1] & 0xFF) + (a[aOff + b.Length - 1] & 0xFF) + 1;
				a[aOff + b.Length - 1] = (byte)num;
				num >>= 8;
				for (int num2 = b.Length - 2; num2 >= 0; num2--)
				{
					num += (b[num2] & 0xFF) + (a[aOff + num2] & 0xFF);
					a[aOff + num2] = (byte)num;
					num >>= 8;
				}
			}

			private byte[] Derive(byte[] diversifier, int n)
			{
				HashAlgorithm hashAlgorithm = PKCS1.CreateFromName(_hashName);
				int num = hashAlgorithm.HashSize >> 3;
				int num2 = 64;
				byte[] array = new byte[n];
				byte[] array2;
				if (_salt != null && _salt.Length != 0)
				{
					array2 = new byte[num2 * ((_salt.Length + num2 - 1) / num2)];
					for (int i = 0; i != array2.Length; i++)
					{
						array2[i] = _salt[i % _salt.Length];
					}
				}
				else
				{
					array2 = new byte[0];
				}
				byte[] array3;
				if (_password != null && _password.Length != 0)
				{
					array3 = new byte[num2 * ((_password.Length + num2 - 1) / num2)];
					for (int j = 0; j != array3.Length; j++)
					{
						array3[j] = _password[j % _password.Length];
					}
				}
				else
				{
					array3 = new byte[0];
				}
				byte[] array4 = new byte[array2.Length + array3.Length];
				Buffer.BlockCopy(array2, 0, array4, 0, array2.Length);
				Buffer.BlockCopy(array3, 0, array4, array2.Length, array3.Length);
				byte[] array5 = new byte[num2];
				int num3 = (n + num - 1) / num;
				for (int k = 1; k <= num3; k++)
				{
					hashAlgorithm.TransformBlock(diversifier, 0, diversifier.Length, diversifier, 0);
					hashAlgorithm.TransformFinalBlock(array4, 0, array4.Length);
					byte[] array6 = hashAlgorithm.Hash;
					hashAlgorithm.Initialize();
					for (int l = 1; l != _iterations; l++)
					{
						array6 = hashAlgorithm.ComputeHash(array6, 0, array6.Length);
					}
					for (int m = 0; m != array5.Length; m++)
					{
						array5[m] = array6[m % array6.Length];
					}
					for (int num4 = 0; num4 != array4.Length / num2; num4++)
					{
						Adjust(array4, num4 * num2, array5);
					}
					if (k == num3)
					{
						Buffer.BlockCopy(array6, 0, array, (k - 1) * num, array.Length - (k - 1) * num);
					}
					else
					{
						Buffer.BlockCopy(array6, 0, array, (k - 1) * num, array6.Length);
					}
				}
				return array;
			}

			public byte[] DeriveKey(int size)
			{
				return Derive(keyDiversifier, size);
			}

			public byte[] DeriveIV(int size)
			{
				return Derive(ivDiversifier, size);
			}

			public byte[] DeriveMAC(int size)
			{
				return Derive(macDiversifier, size);
			}
		}

		public const string pbeWithSHAAnd128BitRC4 = "1.2.840.113549.1.12.1.1";

		public const string pbeWithSHAAnd40BitRC4 = "1.2.840.113549.1.12.1.2";

		public const string pbeWithSHAAnd3KeyTripleDESCBC = "1.2.840.113549.1.12.1.3";

		public const string pbeWithSHAAnd2KeyTripleDESCBC = "1.2.840.113549.1.12.1.4";

		public const string pbeWithSHAAnd128BitRC2CBC = "1.2.840.113549.1.12.1.5";

		public const string pbeWithSHAAnd40BitRC2CBC = "1.2.840.113549.1.12.1.6";

		public const string keyBag = "1.2.840.113549.1.12.10.1.1";

		public const string pkcs8ShroudedKeyBag = "1.2.840.113549.1.12.10.1.2";

		public const string certBag = "1.2.840.113549.1.12.10.1.3";

		public const string crlBag = "1.2.840.113549.1.12.10.1.4";

		public const string secretBag = "1.2.840.113549.1.12.10.1.5";

		public const string safeContentsBag = "1.2.840.113549.1.12.10.1.6";

		public const string x509Certificate = "1.2.840.113549.1.9.22.1";

		public const string sdsiCertificate = "1.2.840.113549.1.9.22.2";

		public const string x509Crl = "1.2.840.113549.1.9.23.1";

		private const int recommendedIterationCount = 2000;

		private byte[] _password;

		private ArrayList _keyBags;

		private ArrayList _secretBags;

		private X509CertificateCollection _certs;

		private bool _keyBagsChanged;

		private bool _secretBagsChanged;

		private bool _certsChanged;

		private int _iterations;

		private ArrayList _safeBags;

		private RandomNumberGenerator _rng;

		public const int CryptoApiPasswordLimit = 32;

		private static int password_max_length = int.MaxValue;

		public string Password
		{
			set
			{
				if (_password != null)
				{
					Array.Clear(_password, 0, _password.Length);
				}
				_password = null;
				if (value == null)
				{
					return;
				}
				if (value.Length > 0)
				{
					int num = value.Length;
					int num2 = 0;
					if (num < MaximumPasswordLength)
					{
						if (value[num - 1] != 0)
						{
							num2 = 1;
						}
					}
					else
					{
						num = MaximumPasswordLength;
					}
					_password = new byte[num + num2 << 1];
					Encoding.BigEndianUnicode.GetBytes(value, 0, num, _password, 0);
				}
				else
				{
					_password = new byte[2];
				}
			}
		}

		public int IterationCount
		{
			get
			{
				return _iterations;
			}
			set
			{
				_iterations = value;
			}
		}

		public ArrayList Keys
		{
			get
			{
				if (_keyBagsChanged)
				{
					_keyBags.Clear();
					foreach (SafeBag safeBag in _safeBags)
					{
						if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.1"))
						{
							byte[] privateKey = new PKCS8.PrivateKeyInfo(safeBag.ASN1[1].Value).PrivateKey;
							switch (privateKey[0])
							{
							case 2:
								_keyBags.Add(PKCS8.PrivateKeyInfo.DecodeDSA(privateKey, default(DSAParameters)));
								break;
							case 48:
								_keyBags.Add(PKCS8.PrivateKeyInfo.DecodeRSA(privateKey));
								break;
							}
							Array.Clear(privateKey, 0, privateKey.Length);
						}
						else if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.2"))
						{
							PKCS8.EncryptedPrivateKeyInfo encryptedPrivateKeyInfo = new PKCS8.EncryptedPrivateKeyInfo(safeBag.ASN1[1].Value);
							byte[] array = Decrypt(encryptedPrivateKeyInfo.Algorithm, encryptedPrivateKeyInfo.Salt, encryptedPrivateKeyInfo.IterationCount, encryptedPrivateKeyInfo.EncryptedData);
							byte[] privateKey2 = new PKCS8.PrivateKeyInfo(array).PrivateKey;
							switch (privateKey2[0])
							{
							case 2:
								_keyBags.Add(PKCS8.PrivateKeyInfo.DecodeDSA(privateKey2, default(DSAParameters)));
								break;
							case 48:
								_keyBags.Add(PKCS8.PrivateKeyInfo.DecodeRSA(privateKey2));
								break;
							}
							Array.Clear(privateKey2, 0, privateKey2.Length);
							Array.Clear(array, 0, array.Length);
						}
					}
					_keyBagsChanged = false;
				}
				return ArrayList.ReadOnly(_keyBags);
			}
		}

		public ArrayList Secrets
		{
			get
			{
				if (_secretBagsChanged)
				{
					_secretBags.Clear();
					foreach (SafeBag safeBag in _safeBags)
					{
						if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.5"))
						{
							byte[] value = safeBag.ASN1[1].Value;
							_secretBags.Add(value);
						}
					}
					_secretBagsChanged = false;
				}
				return ArrayList.ReadOnly(_secretBags);
			}
		}

		public X509CertificateCollection Certificates
		{
			get
			{
				if (_certsChanged)
				{
					_certs.Clear();
					foreach (SafeBag safeBag in _safeBags)
					{
						if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.3"))
						{
							PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(safeBag.ASN1[1].Value);
							_certs.Add(new X509Certificate(contentInfo.Content[0].Value));
						}
					}
					_certsChanged = false;
				}
				return _certs;
			}
		}

		internal RandomNumberGenerator RNG
		{
			get
			{
				if (_rng == null)
				{
					_rng = RandomNumberGenerator.Create();
				}
				return _rng;
			}
		}

		public static int MaximumPasswordLength
		{
			get
			{
				return password_max_length;
			}
			set
			{
				if (value < 32)
				{
					throw new ArgumentOutOfRangeException(global::Locale.GetText("Maximum password length cannot be less than {0}.", 32));
				}
				password_max_length = value;
			}
		}

		public PKCS12()
		{
			_iterations = 2000;
			_keyBags = new ArrayList();
			_secretBags = new ArrayList();
			_certs = new X509CertificateCollection();
			_keyBagsChanged = false;
			_secretBagsChanged = false;
			_certsChanged = false;
			_safeBags = new ArrayList();
		}

		public PKCS12(byte[] data)
			: this()
		{
			Password = null;
			Decode(data);
		}

		public PKCS12(byte[] data, string password)
			: this()
		{
			Password = password;
			Decode(data);
		}

		public PKCS12(byte[] data, byte[] password)
			: this()
		{
			_password = password;
			Decode(data);
		}

		private void Decode(byte[] data)
		{
			ASN1 aSN = new ASN1(data);
			if (aSN.Tag != 48)
			{
				throw new ArgumentException("invalid data");
			}
			if (aSN[0].Tag != 2)
			{
				throw new ArgumentException("invalid PFX version");
			}
			PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(aSN[1]);
			if (contentInfo.ContentType != "1.2.840.113549.1.7.1")
			{
				throw new ArgumentException("invalid authenticated safe");
			}
			if (aSN.Count > 2)
			{
				ASN1 aSN2 = aSN[2];
				if (aSN2.Tag != 48)
				{
					throw new ArgumentException("invalid MAC");
				}
				ASN1 aSN3 = aSN2[0];
				if (aSN3.Tag != 48)
				{
					throw new ArgumentException("invalid MAC");
				}
				if (ASN1Convert.ToOid(aSN3[0][0]) != "1.3.14.3.2.26")
				{
					throw new ArgumentException("unsupported HMAC");
				}
				byte[] value = aSN3[1].Value;
				ASN1 aSN4 = aSN2[1];
				if (aSN4.Tag != 4)
				{
					throw new ArgumentException("missing MAC salt");
				}
				_iterations = 1;
				if (aSN2.Count > 2)
				{
					ASN1 aSN5 = aSN2[2];
					if (aSN5.Tag != 2)
					{
						throw new ArgumentException("invalid MAC iteration");
					}
					_iterations = ASN1Convert.ToInt32(aSN5);
				}
				byte[] value2 = contentInfo.Content[0].Value;
				byte[] actual = MAC(_password, aSN4.Value, _iterations, value2);
				if (!Compare(value, actual))
				{
					byte[] password = new byte[2];
					actual = MAC(password, aSN4.Value, _iterations, value2);
					if (!Compare(value, actual))
					{
						throw new CryptographicException("Invalid MAC - file may have been tampered with!");
					}
					_password = password;
				}
			}
			ASN1 aSN6 = new ASN1(contentInfo.Content[0].Value);
			for (int i = 0; i < aSN6.Count; i++)
			{
				PKCS7.ContentInfo contentInfo2 = new PKCS7.ContentInfo(aSN6[i]);
				switch (contentInfo2.ContentType)
				{
				case "1.2.840.113549.1.7.1":
				{
					ASN1 aSN8 = new ASN1(contentInfo2.Content[0].Value);
					for (int k = 0; k < aSN8.Count; k++)
					{
						ASN1 safeBag2 = aSN8[k];
						ReadSafeBag(safeBag2);
					}
					break;
				}
				case "1.2.840.113549.1.7.6":
				{
					PKCS7.EncryptedData ed = new PKCS7.EncryptedData(contentInfo2.Content[0]);
					ASN1 aSN7 = new ASN1(Decrypt(ed));
					for (int j = 0; j < aSN7.Count; j++)
					{
						ASN1 safeBag = aSN7[j];
						ReadSafeBag(safeBag);
					}
					break;
				}
				case "1.2.840.113549.1.7.3":
					throw new NotImplementedException("public key encrypted");
				default:
					throw new ArgumentException("unknown authenticatedSafe");
				}
			}
		}

		~PKCS12()
		{
			if (_password != null)
			{
				Array.Clear(_password, 0, _password.Length);
			}
			_password = null;
		}

		private bool Compare(byte[] expected, byte[] actual)
		{
			bool result = false;
			if (expected.Length == actual.Length)
			{
				for (int i = 0; i < expected.Length; i++)
				{
					if (expected[i] != actual[i])
					{
						return false;
					}
				}
				result = true;
			}
			return result;
		}

		private SymmetricAlgorithm GetSymmetricAlgorithm(string algorithmOid, byte[] salt, int iterationCount)
		{
			string text = null;
			int size = 8;
			int num = 8;
			DeriveBytes deriveBytes = new DeriveBytes();
			deriveBytes.Password = _password;
			deriveBytes.Salt = salt;
			deriveBytes.IterationCount = iterationCount;
			switch (algorithmOid)
			{
			case "1.2.840.113549.1.5.1":
				deriveBytes.HashName = "MD2";
				text = "DES";
				break;
			case "1.2.840.113549.1.5.3":
				deriveBytes.HashName = "MD5";
				text = "DES";
				break;
			case "1.2.840.113549.1.5.4":
				deriveBytes.HashName = "MD2";
				text = "RC2";
				size = 4;
				break;
			case "1.2.840.113549.1.5.6":
				deriveBytes.HashName = "MD5";
				text = "RC2";
				size = 4;
				break;
			case "1.2.840.113549.1.5.10":
				deriveBytes.HashName = "SHA1";
				text = "DES";
				break;
			case "1.2.840.113549.1.5.11":
				deriveBytes.HashName = "SHA1";
				text = "RC2";
				size = 4;
				break;
			case "1.2.840.113549.1.12.1.1":
				deriveBytes.HashName = "SHA1";
				text = "RC4";
				size = 16;
				num = 0;
				break;
			case "1.2.840.113549.1.12.1.2":
				deriveBytes.HashName = "SHA1";
				text = "RC4";
				size = 5;
				num = 0;
				break;
			case "1.2.840.113549.1.12.1.3":
				deriveBytes.HashName = "SHA1";
				text = "TripleDES";
				size = 24;
				break;
			case "1.2.840.113549.1.12.1.4":
				deriveBytes.HashName = "SHA1";
				text = "TripleDES";
				size = 16;
				break;
			case "1.2.840.113549.1.12.1.5":
				deriveBytes.HashName = "SHA1";
				text = "RC2";
				size = 16;
				break;
			case "1.2.840.113549.1.12.1.6":
				deriveBytes.HashName = "SHA1";
				text = "RC2";
				size = 5;
				break;
			default:
				throw new NotSupportedException("unknown oid " + text);
			}
			SymmetricAlgorithm symmetricAlgorithm = null;
			symmetricAlgorithm = SymmetricAlgorithm.Create(text);
			symmetricAlgorithm.Key = deriveBytes.DeriveKey(size);
			if (num > 0)
			{
				symmetricAlgorithm.IV = deriveBytes.DeriveIV(num);
				symmetricAlgorithm.Mode = CipherMode.CBC;
			}
			return symmetricAlgorithm;
		}

		public byte[] Decrypt(string algorithmOid, byte[] salt, int iterationCount, byte[] encryptedData)
		{
			SymmetricAlgorithm symmetricAlgorithm = null;
			byte[] array = null;
			try
			{
				symmetricAlgorithm = GetSymmetricAlgorithm(algorithmOid, salt, iterationCount);
				return symmetricAlgorithm.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
			}
			finally
			{
				symmetricAlgorithm?.Clear();
			}
		}

		public byte[] Decrypt(PKCS7.EncryptedData ed)
		{
			return Decrypt(ed.EncryptionAlgorithm.ContentType, ed.EncryptionAlgorithm.Content[0].Value, ASN1Convert.ToInt32(ed.EncryptionAlgorithm.Content[1]), ed.EncryptedContent);
		}

		public byte[] Encrypt(string algorithmOid, byte[] salt, int iterationCount, byte[] data)
		{
			byte[] array = null;
			using SymmetricAlgorithm symmetricAlgorithm = GetSymmetricAlgorithm(algorithmOid, salt, iterationCount);
			return symmetricAlgorithm.CreateEncryptor().TransformFinalBlock(data, 0, data.Length);
		}

		private DSAParameters GetExistingParameters(out bool found)
		{
			foreach (X509Certificate certificate in Certificates)
			{
				if (certificate.KeyAlgorithmParameters != null)
				{
					DSA dSA = certificate.DSA;
					if (dSA != null)
					{
						found = true;
						return dSA.ExportParameters(includePrivateParameters: false);
					}
				}
			}
			found = false;
			return default(DSAParameters);
		}

		private void AddPrivateKey(PKCS8.PrivateKeyInfo pki)
		{
			byte[] privateKey = pki.PrivateKey;
			try
			{
				switch (pki.Algorithm)
				{
				case "1.2.840.113549.1.1.1":
					_keyBags.Add(PKCS8.PrivateKeyInfo.DecodeRSA(privateKey));
					break;
				case "1.2.840.10040.4.1":
				{
					bool found;
					DSAParameters existingParameters = GetExistingParameters(out found);
					if (found)
					{
						_keyBags.Add(PKCS8.PrivateKeyInfo.DecodeDSA(privateKey, existingParameters));
					}
					break;
				}
				default:
					throw new CryptographicException("Unknown private key format");
				}
			}
			finally
			{
				Array.Clear(privateKey, 0, privateKey.Length);
			}
		}

		private void ReadSafeBag(ASN1 safeBag)
		{
			if (safeBag.Tag != 48)
			{
				throw new ArgumentException("invalid safeBag");
			}
			ASN1 aSN = safeBag[0];
			if (aSN.Tag != 6)
			{
				throw new ArgumentException("invalid safeBag id");
			}
			ASN1 aSN2 = safeBag[1];
			string text = ASN1Convert.ToOid(aSN);
			switch (text)
			{
			case "1.2.840.113549.1.12.10.1.1":
				AddPrivateKey(new PKCS8.PrivateKeyInfo(aSN2.Value));
				break;
			case "1.2.840.113549.1.12.10.1.2":
			{
				PKCS8.EncryptedPrivateKeyInfo encryptedPrivateKeyInfo = new PKCS8.EncryptedPrivateKeyInfo(aSN2.Value);
				byte[] array = Decrypt(encryptedPrivateKeyInfo.Algorithm, encryptedPrivateKeyInfo.Salt, encryptedPrivateKeyInfo.IterationCount, encryptedPrivateKeyInfo.EncryptedData);
				AddPrivateKey(new PKCS8.PrivateKeyInfo(array));
				Array.Clear(array, 0, array.Length);
				break;
			}
			case "1.2.840.113549.1.12.10.1.3":
			{
				PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(aSN2.Value);
				if (contentInfo.ContentType != "1.2.840.113549.1.9.22.1")
				{
					throw new NotSupportedException("unsupport certificate type");
				}
				X509Certificate value2 = new X509Certificate(contentInfo.Content[0].Value);
				_certs.Add(value2);
				break;
			}
			case "1.2.840.113549.1.12.10.1.5":
			{
				byte[] value = aSN2.Value;
				_secretBags.Add(value);
				break;
			}
			default:
				throw new ArgumentException("unknown safeBag oid");
			case "1.2.840.113549.1.12.10.1.4":
			case "1.2.840.113549.1.12.10.1.6":
				break;
			}
			if (safeBag.Count > 2)
			{
				ASN1 aSN3 = safeBag[2];
				if (aSN3.Tag != 49)
				{
					throw new ArgumentException("invalid safeBag attributes id");
				}
				for (int i = 0; i < aSN3.Count; i++)
				{
					ASN1 aSN4 = aSN3[i];
					if (aSN4.Tag != 48)
					{
						throw new ArgumentException("invalid PKCS12 attributes id");
					}
					ASN1 aSN5 = aSN4[0];
					if (aSN5.Tag != 6)
					{
						throw new ArgumentException("invalid attribute id");
					}
					string text2 = ASN1Convert.ToOid(aSN5);
					ASN1 aSN6 = aSN4[1];
					for (int j = 0; j < aSN6.Count; j++)
					{
						ASN1 aSN7 = aSN6[j];
						if (!(text2 == "1.2.840.113549.1.9.20"))
						{
							if (text2 == "1.2.840.113549.1.9.21" && aSN7.Tag != 4)
							{
								throw new ArgumentException("invalid attribute value id");
							}
						}
						else if (aSN7.Tag != 30)
						{
							throw new ArgumentException("invalid attribute value id");
						}
					}
				}
			}
			_safeBags.Add(new SafeBag(text, safeBag));
		}

		private ASN1 Pkcs8ShroudedKeyBagSafeBag(AsymmetricAlgorithm aa, IDictionary attributes)
		{
			PKCS8.PrivateKeyInfo privateKeyInfo = new PKCS8.PrivateKeyInfo();
			if (aa is RSA)
			{
				privateKeyInfo.Algorithm = "1.2.840.113549.1.1.1";
				privateKeyInfo.PrivateKey = PKCS8.PrivateKeyInfo.Encode((RSA)aa);
			}
			else
			{
				if (!(aa is DSA))
				{
					throw new CryptographicException("Unknown asymmetric algorithm {0}", aa.ToString());
				}
				privateKeyInfo.Algorithm = null;
				privateKeyInfo.PrivateKey = PKCS8.PrivateKeyInfo.Encode((DSA)aa);
			}
			PKCS8.EncryptedPrivateKeyInfo encryptedPrivateKeyInfo = new PKCS8.EncryptedPrivateKeyInfo();
			encryptedPrivateKeyInfo.Algorithm = "1.2.840.113549.1.12.1.3";
			encryptedPrivateKeyInfo.IterationCount = _iterations;
			encryptedPrivateKeyInfo.EncryptedData = Encrypt("1.2.840.113549.1.12.1.3", encryptedPrivateKeyInfo.Salt, _iterations, privateKeyInfo.GetBytes());
			ASN1 aSN = new ASN1(48);
			aSN.Add(ASN1Convert.FromOid("1.2.840.113549.1.12.10.1.2"));
			ASN1 aSN2 = new ASN1(160);
			aSN2.Add(new ASN1(encryptedPrivateKeyInfo.GetBytes()));
			aSN.Add(aSN2);
			if (attributes != null)
			{
				ASN1 aSN3 = new ASN1(49);
				IDictionaryEnumerator enumerator = attributes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Key;
					if (!(text == "1.2.840.113549.1.9.20"))
					{
						if (!(text == "1.2.840.113549.1.9.21"))
						{
							continue;
						}
						ArrayList arrayList = (ArrayList)enumerator.Value;
						if (arrayList.Count <= 0)
						{
							continue;
						}
						ASN1 aSN4 = new ASN1(48);
						aSN4.Add(ASN1Convert.FromOid("1.2.840.113549.1.9.21"));
						ASN1 aSN5 = new ASN1(49);
						foreach (byte[] item in arrayList)
						{
							ASN1 aSN6 = new ASN1(4);
							aSN6.Value = item;
							aSN5.Add(aSN6);
						}
						aSN4.Add(aSN5);
						aSN3.Add(aSN4);
						continue;
					}
					ArrayList arrayList2 = (ArrayList)enumerator.Value;
					if (arrayList2.Count <= 0)
					{
						continue;
					}
					ASN1 aSN7 = new ASN1(48);
					aSN7.Add(ASN1Convert.FromOid("1.2.840.113549.1.9.20"));
					ASN1 aSN8 = new ASN1(49);
					foreach (byte[] item2 in arrayList2)
					{
						ASN1 aSN9 = new ASN1(30);
						aSN9.Value = item2;
						aSN8.Add(aSN9);
					}
					aSN7.Add(aSN8);
					aSN3.Add(aSN7);
				}
				if (aSN3.Count > 0)
				{
					aSN.Add(aSN3);
				}
			}
			return aSN;
		}

		private ASN1 KeyBagSafeBag(AsymmetricAlgorithm aa, IDictionary attributes)
		{
			PKCS8.PrivateKeyInfo privateKeyInfo = new PKCS8.PrivateKeyInfo();
			if (aa is RSA)
			{
				privateKeyInfo.Algorithm = "1.2.840.113549.1.1.1";
				privateKeyInfo.PrivateKey = PKCS8.PrivateKeyInfo.Encode((RSA)aa);
			}
			else
			{
				if (!(aa is DSA))
				{
					throw new CryptographicException("Unknown asymmetric algorithm {0}", aa.ToString());
				}
				privateKeyInfo.Algorithm = null;
				privateKeyInfo.PrivateKey = PKCS8.PrivateKeyInfo.Encode((DSA)aa);
			}
			ASN1 aSN = new ASN1(48);
			aSN.Add(ASN1Convert.FromOid("1.2.840.113549.1.12.10.1.1"));
			ASN1 aSN2 = new ASN1(160);
			aSN2.Add(new ASN1(privateKeyInfo.GetBytes()));
			aSN.Add(aSN2);
			if (attributes != null)
			{
				ASN1 aSN3 = new ASN1(49);
				IDictionaryEnumerator enumerator = attributes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Key;
					if (!(text == "1.2.840.113549.1.9.20"))
					{
						if (!(text == "1.2.840.113549.1.9.21"))
						{
							continue;
						}
						ArrayList arrayList = (ArrayList)enumerator.Value;
						if (arrayList.Count <= 0)
						{
							continue;
						}
						ASN1 aSN4 = new ASN1(48);
						aSN4.Add(ASN1Convert.FromOid("1.2.840.113549.1.9.21"));
						ASN1 aSN5 = new ASN1(49);
						foreach (byte[] item in arrayList)
						{
							ASN1 aSN6 = new ASN1(4);
							aSN6.Value = item;
							aSN5.Add(aSN6);
						}
						aSN4.Add(aSN5);
						aSN3.Add(aSN4);
						continue;
					}
					ArrayList arrayList2 = (ArrayList)enumerator.Value;
					if (arrayList2.Count <= 0)
					{
						continue;
					}
					ASN1 aSN7 = new ASN1(48);
					aSN7.Add(ASN1Convert.FromOid("1.2.840.113549.1.9.20"));
					ASN1 aSN8 = new ASN1(49);
					foreach (byte[] item2 in arrayList2)
					{
						ASN1 aSN9 = new ASN1(30);
						aSN9.Value = item2;
						aSN8.Add(aSN9);
					}
					aSN7.Add(aSN8);
					aSN3.Add(aSN7);
				}
				if (aSN3.Count > 0)
				{
					aSN.Add(aSN3);
				}
			}
			return aSN;
		}

		private ASN1 SecretBagSafeBag(byte[] secret, IDictionary attributes)
		{
			ASN1 aSN = new ASN1(48);
			aSN.Add(ASN1Convert.FromOid("1.2.840.113549.1.12.10.1.5"));
			ASN1 asn = new ASN1(128, secret);
			aSN.Add(asn);
			if (attributes != null)
			{
				ASN1 aSN2 = new ASN1(49);
				IDictionaryEnumerator enumerator = attributes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Key;
					if (!(text == "1.2.840.113549.1.9.20"))
					{
						if (!(text == "1.2.840.113549.1.9.21"))
						{
							continue;
						}
						ArrayList arrayList = (ArrayList)enumerator.Value;
						if (arrayList.Count <= 0)
						{
							continue;
						}
						ASN1 aSN3 = new ASN1(48);
						aSN3.Add(ASN1Convert.FromOid("1.2.840.113549.1.9.21"));
						ASN1 aSN4 = new ASN1(49);
						foreach (byte[] item in arrayList)
						{
							ASN1 aSN5 = new ASN1(4);
							aSN5.Value = item;
							aSN4.Add(aSN5);
						}
						aSN3.Add(aSN4);
						aSN2.Add(aSN3);
						continue;
					}
					ArrayList arrayList2 = (ArrayList)enumerator.Value;
					if (arrayList2.Count <= 0)
					{
						continue;
					}
					ASN1 aSN6 = new ASN1(48);
					aSN6.Add(ASN1Convert.FromOid("1.2.840.113549.1.9.20"));
					ASN1 aSN7 = new ASN1(49);
					foreach (byte[] item2 in arrayList2)
					{
						ASN1 aSN8 = new ASN1(30);
						aSN8.Value = item2;
						aSN7.Add(aSN8);
					}
					aSN6.Add(aSN7);
					aSN2.Add(aSN6);
				}
				if (aSN2.Count > 0)
				{
					aSN.Add(aSN2);
				}
			}
			return aSN;
		}

		private ASN1 CertificateSafeBag(X509Certificate x509, IDictionary attributes)
		{
			ASN1 asn = new ASN1(4, x509.RawData);
			PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo();
			contentInfo.ContentType = "1.2.840.113549.1.9.22.1";
			contentInfo.Content.Add(asn);
			ASN1 aSN = new ASN1(160);
			aSN.Add(contentInfo.ASN1);
			ASN1 aSN2 = new ASN1(48);
			aSN2.Add(ASN1Convert.FromOid("1.2.840.113549.1.12.10.1.3"));
			aSN2.Add(aSN);
			if (attributes != null)
			{
				ASN1 aSN3 = new ASN1(49);
				IDictionaryEnumerator enumerator = attributes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Key;
					if (!(text == "1.2.840.113549.1.9.20"))
					{
						if (!(text == "1.2.840.113549.1.9.21"))
						{
							continue;
						}
						ArrayList arrayList = (ArrayList)enumerator.Value;
						if (arrayList.Count <= 0)
						{
							continue;
						}
						ASN1 aSN4 = new ASN1(48);
						aSN4.Add(ASN1Convert.FromOid("1.2.840.113549.1.9.21"));
						ASN1 aSN5 = new ASN1(49);
						foreach (byte[] item in arrayList)
						{
							ASN1 aSN6 = new ASN1(4);
							aSN6.Value = item;
							aSN5.Add(aSN6);
						}
						aSN4.Add(aSN5);
						aSN3.Add(aSN4);
						continue;
					}
					ArrayList arrayList2 = (ArrayList)enumerator.Value;
					if (arrayList2.Count <= 0)
					{
						continue;
					}
					ASN1 aSN7 = new ASN1(48);
					aSN7.Add(ASN1Convert.FromOid("1.2.840.113549.1.9.20"));
					ASN1 aSN8 = new ASN1(49);
					foreach (byte[] item2 in arrayList2)
					{
						ASN1 aSN9 = new ASN1(30);
						aSN9.Value = item2;
						aSN8.Add(aSN9);
					}
					aSN7.Add(aSN8);
					aSN3.Add(aSN7);
				}
				if (aSN3.Count > 0)
				{
					aSN2.Add(aSN3);
				}
			}
			return aSN2;
		}

		private byte[] MAC(byte[] password, byte[] salt, int iterations, byte[] data)
		{
			DeriveBytes deriveBytes = new DeriveBytes();
			deriveBytes.HashName = "SHA1";
			deriveBytes.Password = password;
			deriveBytes.Salt = salt;
			deriveBytes.IterationCount = iterations;
			HMACSHA1 obj = (HMACSHA1)System.Security.Cryptography.HMAC.Create();
			obj.Key = deriveBytes.DeriveMAC(20);
			return obj.ComputeHash(data, 0, data.Length);
		}

		public byte[] GetBytes()
		{
			ASN1 aSN = new ASN1(48);
			ArrayList arrayList = new ArrayList();
			foreach (SafeBag safeBag5 in _safeBags)
			{
				if (safeBag5.BagOID.Equals("1.2.840.113549.1.12.10.1.3"))
				{
					PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(safeBag5.ASN1[1].Value);
					arrayList.Add(new X509Certificate(contentInfo.Content[0].Value));
				}
			}
			ArrayList arrayList2 = new ArrayList();
			ArrayList arrayList3 = new ArrayList();
			foreach (X509Certificate certificate in Certificates)
			{
				bool flag = false;
				foreach (X509Certificate item in arrayList)
				{
					if (Compare(certificate.RawData, item.RawData))
					{
						flag = true;
					}
				}
				if (!flag)
				{
					arrayList2.Add(certificate);
				}
			}
			foreach (X509Certificate item2 in arrayList)
			{
				bool flag2 = false;
				foreach (X509Certificate certificate2 in Certificates)
				{
					if (Compare(item2.RawData, certificate2.RawData))
					{
						flag2 = true;
					}
				}
				if (!flag2)
				{
					arrayList3.Add(item2);
				}
			}
			foreach (X509Certificate item3 in arrayList3)
			{
				RemoveCertificate(item3);
			}
			foreach (X509Certificate item4 in arrayList2)
			{
				AddCertificate(item4);
			}
			if (_safeBags.Count > 0)
			{
				ASN1 aSN2 = new ASN1(48);
				foreach (SafeBag safeBag6 in _safeBags)
				{
					if (safeBag6.BagOID.Equals("1.2.840.113549.1.12.10.1.3"))
					{
						aSN2.Add(safeBag6.ASN1);
					}
				}
				if (aSN2.Count > 0)
				{
					PKCS7.ContentInfo contentInfo2 = EncryptedContentInfo(aSN2, "1.2.840.113549.1.12.1.3");
					aSN.Add(contentInfo2.ASN1);
				}
			}
			if (_safeBags.Count > 0)
			{
				ASN1 aSN3 = new ASN1(48);
				foreach (SafeBag safeBag7 in _safeBags)
				{
					if (safeBag7.BagOID.Equals("1.2.840.113549.1.12.10.1.1") || safeBag7.BagOID.Equals("1.2.840.113549.1.12.10.1.2"))
					{
						aSN3.Add(safeBag7.ASN1);
					}
				}
				if (aSN3.Count > 0)
				{
					ASN1 aSN4 = new ASN1(160);
					aSN4.Add(new ASN1(4, aSN3.GetBytes()));
					PKCS7.ContentInfo contentInfo3 = new PKCS7.ContentInfo("1.2.840.113549.1.7.1");
					contentInfo3.Content = aSN4;
					aSN.Add(contentInfo3.ASN1);
				}
			}
			if (_safeBags.Count > 0)
			{
				ASN1 aSN5 = new ASN1(48);
				foreach (SafeBag safeBag8 in _safeBags)
				{
					if (safeBag8.BagOID.Equals("1.2.840.113549.1.12.10.1.5"))
					{
						aSN5.Add(safeBag8.ASN1);
					}
				}
				if (aSN5.Count > 0)
				{
					PKCS7.ContentInfo contentInfo4 = EncryptedContentInfo(aSN5, "1.2.840.113549.1.12.1.3");
					aSN.Add(contentInfo4.ASN1);
				}
			}
			ASN1 asn = new ASN1(4, aSN.GetBytes());
			ASN1 aSN6 = new ASN1(160);
			aSN6.Add(asn);
			PKCS7.ContentInfo contentInfo5 = new PKCS7.ContentInfo("1.2.840.113549.1.7.1");
			contentInfo5.Content = aSN6;
			ASN1 aSN7 = new ASN1(48);
			if (_password != null)
			{
				byte[] array = new byte[20];
				RNG.GetBytes(array);
				byte[] data = MAC(_password, array, _iterations, contentInfo5.Content[0].Value);
				ASN1 aSN8 = new ASN1(48);
				aSN8.Add(ASN1Convert.FromOid("1.3.14.3.2.26"));
				aSN8.Add(new ASN1(5));
				ASN1 aSN9 = new ASN1(48);
				aSN9.Add(aSN8);
				aSN9.Add(new ASN1(4, data));
				aSN7.Add(aSN9);
				aSN7.Add(new ASN1(4, array));
				aSN7.Add(ASN1Convert.FromInt32(_iterations));
			}
			ASN1 asn2 = new ASN1(2, new byte[1] { 3 });
			ASN1 aSN10 = new ASN1(48);
			aSN10.Add(asn2);
			aSN10.Add(contentInfo5.ASN1);
			if (aSN7.Count > 0)
			{
				aSN10.Add(aSN7);
			}
			return aSN10.GetBytes();
		}

		private PKCS7.ContentInfo EncryptedContentInfo(ASN1 safeBags, string algorithmOid)
		{
			byte[] array = new byte[8];
			RNG.GetBytes(array);
			ASN1 aSN = new ASN1(48);
			aSN.Add(new ASN1(4, array));
			aSN.Add(ASN1Convert.FromInt32(_iterations));
			ASN1 aSN2 = new ASN1(48);
			aSN2.Add(ASN1Convert.FromOid(algorithmOid));
			aSN2.Add(aSN);
			byte[] data = Encrypt(algorithmOid, array, _iterations, safeBags.GetBytes());
			ASN1 asn = new ASN1(128, data);
			ASN1 aSN3 = new ASN1(48);
			aSN3.Add(ASN1Convert.FromOid("1.2.840.113549.1.7.1"));
			aSN3.Add(aSN2);
			aSN3.Add(asn);
			ASN1 asn2 = new ASN1(2, new byte[1]);
			ASN1 aSN4 = new ASN1(48);
			aSN4.Add(asn2);
			aSN4.Add(aSN3);
			ASN1 aSN5 = new ASN1(160);
			aSN5.Add(aSN4);
			return new PKCS7.ContentInfo("1.2.840.113549.1.7.6")
			{
				Content = aSN5
			};
		}

		public void AddCertificate(X509Certificate cert)
		{
			AddCertificate(cert, null);
		}

		public void AddCertificate(X509Certificate cert, IDictionary attributes)
		{
			bool flag = false;
			int num = 0;
			while (!flag && num < _safeBags.Count)
			{
				SafeBag safeBag = (SafeBag)_safeBags[num];
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.3"))
				{
					X509Certificate x509Certificate = new X509Certificate(new PKCS7.ContentInfo(safeBag.ASN1[1].Value).Content[0].Value);
					if (Compare(cert.RawData, x509Certificate.RawData))
					{
						flag = true;
					}
				}
				num++;
			}
			if (!flag)
			{
				_safeBags.Add(new SafeBag("1.2.840.113549.1.12.10.1.3", CertificateSafeBag(cert, attributes)));
				_certsChanged = true;
			}
		}

		public void RemoveCertificate(X509Certificate cert)
		{
			RemoveCertificate(cert, null);
		}

		public void RemoveCertificate(X509Certificate cert, IDictionary attrs)
		{
			int num = -1;
			int num2 = 0;
			while (num == -1 && num2 < _safeBags.Count)
			{
				SafeBag safeBag = (SafeBag)_safeBags[num2];
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.3"))
				{
					ASN1 aSN = safeBag.ASN1;
					X509Certificate x509Certificate = new X509Certificate(new PKCS7.ContentInfo(aSN[1].Value).Content[0].Value);
					if (Compare(cert.RawData, x509Certificate.RawData))
					{
						if (attrs != null)
						{
							if (aSN.Count == 3)
							{
								ASN1 aSN2 = aSN[2];
								int num3 = 0;
								for (int i = 0; i < aSN2.Count; i++)
								{
									ASN1 aSN3 = aSN2[i];
									string key = ASN1Convert.ToOid(aSN3[0]);
									ArrayList arrayList = (ArrayList)attrs[key];
									if (arrayList == null)
									{
										continue;
									}
									ASN1 aSN4 = aSN3[1];
									if (arrayList.Count != aSN4.Count)
									{
										continue;
									}
									int num4 = 0;
									for (int j = 0; j < aSN4.Count; j++)
									{
										ASN1 aSN5 = aSN4[j];
										byte[] expected = (byte[])arrayList[j];
										if (Compare(expected, aSN5.Value))
										{
											num4++;
										}
									}
									if (num4 == aSN4.Count)
									{
										num3++;
									}
								}
								if (num3 == aSN2.Count)
								{
									num = num2;
								}
							}
						}
						else
						{
							num = num2;
						}
					}
				}
				num2++;
			}
			if (num != -1)
			{
				_safeBags.RemoveAt(num);
				_certsChanged = true;
			}
		}

		private bool CompareAsymmetricAlgorithm(AsymmetricAlgorithm a1, AsymmetricAlgorithm a2)
		{
			if (a1.KeySize != a2.KeySize)
			{
				return false;
			}
			return a1.ToXmlString(includePrivateParameters: false) == a2.ToXmlString(includePrivateParameters: false);
		}

		public void AddPkcs8ShroudedKeyBag(AsymmetricAlgorithm aa)
		{
			AddPkcs8ShroudedKeyBag(aa, null);
		}

		public void AddPkcs8ShroudedKeyBag(AsymmetricAlgorithm aa, IDictionary attributes)
		{
			bool flag = false;
			int num = 0;
			while (!flag && num < _safeBags.Count)
			{
				SafeBag safeBag = (SafeBag)_safeBags[num];
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.2"))
				{
					PKCS8.EncryptedPrivateKeyInfo encryptedPrivateKeyInfo = new PKCS8.EncryptedPrivateKeyInfo(safeBag.ASN1[1].Value);
					byte[] array = Decrypt(encryptedPrivateKeyInfo.Algorithm, encryptedPrivateKeyInfo.Salt, encryptedPrivateKeyInfo.IterationCount, encryptedPrivateKeyInfo.EncryptedData);
					byte[] privateKey = new PKCS8.PrivateKeyInfo(array).PrivateKey;
					AsymmetricAlgorithm asymmetricAlgorithm = null;
					switch (privateKey[0])
					{
					case 2:
						asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeDSA(privateKey, default(DSAParameters));
						break;
					case 48:
						asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeRSA(privateKey);
						break;
					default:
						Array.Clear(array, 0, array.Length);
						Array.Clear(privateKey, 0, privateKey.Length);
						throw new CryptographicException("Unknown private key format");
					}
					Array.Clear(array, 0, array.Length);
					Array.Clear(privateKey, 0, privateKey.Length);
					if (CompareAsymmetricAlgorithm(aa, asymmetricAlgorithm))
					{
						flag = true;
					}
				}
				num++;
			}
			if (!flag)
			{
				_safeBags.Add(new SafeBag("1.2.840.113549.1.12.10.1.2", Pkcs8ShroudedKeyBagSafeBag(aa, attributes)));
				_keyBagsChanged = true;
			}
		}

		public void RemovePkcs8ShroudedKeyBag(AsymmetricAlgorithm aa)
		{
			int num = -1;
			int num2 = 0;
			while (num == -1 && num2 < _safeBags.Count)
			{
				SafeBag safeBag = (SafeBag)_safeBags[num2];
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.2"))
				{
					PKCS8.EncryptedPrivateKeyInfo encryptedPrivateKeyInfo = new PKCS8.EncryptedPrivateKeyInfo(safeBag.ASN1[1].Value);
					byte[] array = Decrypt(encryptedPrivateKeyInfo.Algorithm, encryptedPrivateKeyInfo.Salt, encryptedPrivateKeyInfo.IterationCount, encryptedPrivateKeyInfo.EncryptedData);
					byte[] privateKey = new PKCS8.PrivateKeyInfo(array).PrivateKey;
					AsymmetricAlgorithm asymmetricAlgorithm = null;
					switch (privateKey[0])
					{
					case 2:
						asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeDSA(privateKey, default(DSAParameters));
						break;
					case 48:
						asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeRSA(privateKey);
						break;
					default:
						Array.Clear(array, 0, array.Length);
						Array.Clear(privateKey, 0, privateKey.Length);
						throw new CryptographicException("Unknown private key format");
					}
					Array.Clear(array, 0, array.Length);
					Array.Clear(privateKey, 0, privateKey.Length);
					if (CompareAsymmetricAlgorithm(aa, asymmetricAlgorithm))
					{
						num = num2;
					}
				}
				num2++;
			}
			if (num != -1)
			{
				_safeBags.RemoveAt(num);
				_keyBagsChanged = true;
			}
		}

		public void AddKeyBag(AsymmetricAlgorithm aa)
		{
			AddKeyBag(aa, null);
		}

		public void AddKeyBag(AsymmetricAlgorithm aa, IDictionary attributes)
		{
			bool flag = false;
			int num = 0;
			while (!flag && num < _safeBags.Count)
			{
				SafeBag safeBag = (SafeBag)_safeBags[num];
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.1"))
				{
					byte[] privateKey = new PKCS8.PrivateKeyInfo(safeBag.ASN1[1].Value).PrivateKey;
					AsymmetricAlgorithm asymmetricAlgorithm = null;
					switch (privateKey[0])
					{
					case 2:
						asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeDSA(privateKey, default(DSAParameters));
						break;
					case 48:
						asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeRSA(privateKey);
						break;
					default:
						Array.Clear(privateKey, 0, privateKey.Length);
						throw new CryptographicException("Unknown private key format");
					}
					Array.Clear(privateKey, 0, privateKey.Length);
					if (CompareAsymmetricAlgorithm(aa, asymmetricAlgorithm))
					{
						flag = true;
					}
				}
				num++;
			}
			if (!flag)
			{
				_safeBags.Add(new SafeBag("1.2.840.113549.1.12.10.1.1", KeyBagSafeBag(aa, attributes)));
				_keyBagsChanged = true;
			}
		}

		public void RemoveKeyBag(AsymmetricAlgorithm aa)
		{
			int num = -1;
			int num2 = 0;
			while (num == -1 && num2 < _safeBags.Count)
			{
				SafeBag safeBag = (SafeBag)_safeBags[num2];
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.1"))
				{
					byte[] privateKey = new PKCS8.PrivateKeyInfo(safeBag.ASN1[1].Value).PrivateKey;
					AsymmetricAlgorithm asymmetricAlgorithm = null;
					switch (privateKey[0])
					{
					case 2:
						asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeDSA(privateKey, default(DSAParameters));
						break;
					case 48:
						asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeRSA(privateKey);
						break;
					default:
						Array.Clear(privateKey, 0, privateKey.Length);
						throw new CryptographicException("Unknown private key format");
					}
					Array.Clear(privateKey, 0, privateKey.Length);
					if (CompareAsymmetricAlgorithm(aa, asymmetricAlgorithm))
					{
						num = num2;
					}
				}
				num2++;
			}
			if (num != -1)
			{
				_safeBags.RemoveAt(num);
				_keyBagsChanged = true;
			}
		}

		public void AddSecretBag(byte[] secret)
		{
			AddSecretBag(secret, null);
		}

		public void AddSecretBag(byte[] secret, IDictionary attributes)
		{
			bool flag = false;
			int num = 0;
			while (!flag && num < _safeBags.Count)
			{
				SafeBag safeBag = (SafeBag)_safeBags[num];
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.5"))
				{
					byte[] value = safeBag.ASN1[1].Value;
					if (Compare(secret, value))
					{
						flag = true;
					}
				}
				num++;
			}
			if (!flag)
			{
				_safeBags.Add(new SafeBag("1.2.840.113549.1.12.10.1.5", SecretBagSafeBag(secret, attributes)));
				_secretBagsChanged = true;
			}
		}

		public void RemoveSecretBag(byte[] secret)
		{
			int num = -1;
			int num2 = 0;
			while (num == -1 && num2 < _safeBags.Count)
			{
				SafeBag safeBag = (SafeBag)_safeBags[num2];
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.5"))
				{
					byte[] value = safeBag.ASN1[1].Value;
					if (Compare(secret, value))
					{
						num = num2;
					}
				}
				num2++;
			}
			if (num != -1)
			{
				_safeBags.RemoveAt(num);
				_secretBagsChanged = true;
			}
		}

		public AsymmetricAlgorithm GetAsymmetricAlgorithm(IDictionary attrs)
		{
			foreach (SafeBag safeBag in _safeBags)
			{
				if (!safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.1") && !safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.2"))
				{
					continue;
				}
				ASN1 aSN = safeBag.ASN1;
				if (aSN.Count != 3)
				{
					continue;
				}
				ASN1 aSN2 = aSN[2];
				int num = 0;
				for (int i = 0; i < aSN2.Count; i++)
				{
					ASN1 aSN3 = aSN2[i];
					string key = ASN1Convert.ToOid(aSN3[0]);
					ArrayList arrayList = (ArrayList)attrs[key];
					if (arrayList == null)
					{
						continue;
					}
					ASN1 aSN4 = aSN3[1];
					if (arrayList.Count != aSN4.Count)
					{
						continue;
					}
					int num2 = 0;
					for (int j = 0; j < aSN4.Count; j++)
					{
						ASN1 aSN5 = aSN4[j];
						byte[] expected = (byte[])arrayList[j];
						if (Compare(expected, aSN5.Value))
						{
							num2++;
						}
					}
					if (num2 == aSN4.Count)
					{
						num++;
					}
				}
				if (num != aSN2.Count)
				{
					continue;
				}
				ASN1 aSN6 = aSN[1];
				AsymmetricAlgorithm result = null;
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.1"))
				{
					byte[] privateKey = new PKCS8.PrivateKeyInfo(aSN6.Value).PrivateKey;
					switch (privateKey[0])
					{
					case 2:
						result = PKCS8.PrivateKeyInfo.DecodeDSA(privateKey, default(DSAParameters));
						break;
					case 48:
						result = PKCS8.PrivateKeyInfo.DecodeRSA(privateKey);
						break;
					}
					Array.Clear(privateKey, 0, privateKey.Length);
				}
				else if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.2"))
				{
					PKCS8.EncryptedPrivateKeyInfo encryptedPrivateKeyInfo = new PKCS8.EncryptedPrivateKeyInfo(aSN6.Value);
					byte[] array = Decrypt(encryptedPrivateKeyInfo.Algorithm, encryptedPrivateKeyInfo.Salt, encryptedPrivateKeyInfo.IterationCount, encryptedPrivateKeyInfo.EncryptedData);
					byte[] privateKey2 = new PKCS8.PrivateKeyInfo(array).PrivateKey;
					switch (privateKey2[0])
					{
					case 2:
						result = PKCS8.PrivateKeyInfo.DecodeDSA(privateKey2, default(DSAParameters));
						break;
					case 48:
						result = PKCS8.PrivateKeyInfo.DecodeRSA(privateKey2);
						break;
					}
					Array.Clear(privateKey2, 0, privateKey2.Length);
					Array.Clear(array, 0, array.Length);
				}
				return result;
			}
			return null;
		}

		public byte[] GetSecret(IDictionary attrs)
		{
			foreach (SafeBag safeBag in _safeBags)
			{
				if (!safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.5"))
				{
					continue;
				}
				ASN1 aSN = safeBag.ASN1;
				if (aSN.Count != 3)
				{
					continue;
				}
				ASN1 aSN2 = aSN[2];
				int num = 0;
				for (int i = 0; i < aSN2.Count; i++)
				{
					ASN1 aSN3 = aSN2[i];
					string key = ASN1Convert.ToOid(aSN3[0]);
					ArrayList arrayList = (ArrayList)attrs[key];
					if (arrayList == null)
					{
						continue;
					}
					ASN1 aSN4 = aSN3[1];
					if (arrayList.Count != aSN4.Count)
					{
						continue;
					}
					int num2 = 0;
					for (int j = 0; j < aSN4.Count; j++)
					{
						ASN1 aSN5 = aSN4[j];
						byte[] expected = (byte[])arrayList[j];
						if (Compare(expected, aSN5.Value))
						{
							num2++;
						}
					}
					if (num2 == aSN4.Count)
					{
						num++;
					}
				}
				if (num == aSN2.Count)
				{
					return aSN[1].Value;
				}
			}
			return null;
		}

		public X509Certificate GetCertificate(IDictionary attrs)
		{
			foreach (SafeBag safeBag in _safeBags)
			{
				if (!safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.3"))
				{
					continue;
				}
				ASN1 aSN = safeBag.ASN1;
				if (aSN.Count != 3)
				{
					continue;
				}
				ASN1 aSN2 = aSN[2];
				int num = 0;
				for (int i = 0; i < aSN2.Count; i++)
				{
					ASN1 aSN3 = aSN2[i];
					string key = ASN1Convert.ToOid(aSN3[0]);
					ArrayList arrayList = (ArrayList)attrs[key];
					if (arrayList == null)
					{
						continue;
					}
					ASN1 aSN4 = aSN3[1];
					if (arrayList.Count != aSN4.Count)
					{
						continue;
					}
					int num2 = 0;
					for (int j = 0; j < aSN4.Count; j++)
					{
						ASN1 aSN5 = aSN4[j];
						byte[] expected = (byte[])arrayList[j];
						if (Compare(expected, aSN5.Value))
						{
							num2++;
						}
					}
					if (num2 == aSN4.Count)
					{
						num++;
					}
				}
				if (num == aSN2.Count)
				{
					return new X509Certificate(new PKCS7.ContentInfo(aSN[1].Value).Content[0].Value);
				}
			}
			return null;
		}

		public IDictionary GetAttributes(AsymmetricAlgorithm aa)
		{
			IDictionary dictionary = new Hashtable();
			foreach (SafeBag safeBag in _safeBags)
			{
				if (!safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.1") && !safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.2"))
				{
					continue;
				}
				ASN1 aSN = safeBag.ASN1;
				ASN1 aSN2 = aSN[1];
				AsymmetricAlgorithm asymmetricAlgorithm = null;
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.1"))
				{
					byte[] privateKey = new PKCS8.PrivateKeyInfo(aSN2.Value).PrivateKey;
					switch (privateKey[0])
					{
					case 2:
						asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeDSA(privateKey, default(DSAParameters));
						break;
					case 48:
						asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeRSA(privateKey);
						break;
					}
					Array.Clear(privateKey, 0, privateKey.Length);
				}
				else if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.2"))
				{
					PKCS8.EncryptedPrivateKeyInfo encryptedPrivateKeyInfo = new PKCS8.EncryptedPrivateKeyInfo(aSN2.Value);
					byte[] array = Decrypt(encryptedPrivateKeyInfo.Algorithm, encryptedPrivateKeyInfo.Salt, encryptedPrivateKeyInfo.IterationCount, encryptedPrivateKeyInfo.EncryptedData);
					byte[] privateKey2 = new PKCS8.PrivateKeyInfo(array).PrivateKey;
					switch (privateKey2[0])
					{
					case 2:
						asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeDSA(privateKey2, default(DSAParameters));
						break;
					case 48:
						asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeRSA(privateKey2);
						break;
					}
					Array.Clear(privateKey2, 0, privateKey2.Length);
					Array.Clear(array, 0, array.Length);
				}
				if (asymmetricAlgorithm == null || !CompareAsymmetricAlgorithm(asymmetricAlgorithm, aa) || aSN.Count != 3)
				{
					continue;
				}
				ASN1 aSN3 = aSN[2];
				for (int i = 0; i < aSN3.Count; i++)
				{
					ASN1 aSN4 = aSN3[i];
					string key = ASN1Convert.ToOid(aSN4[0]);
					ArrayList arrayList = new ArrayList();
					ASN1 aSN5 = aSN4[1];
					for (int j = 0; j < aSN5.Count; j++)
					{
						ASN1 aSN6 = aSN5[j];
						arrayList.Add(aSN6.Value);
					}
					dictionary.Add(key, arrayList);
				}
			}
			return dictionary;
		}

		public IDictionary GetAttributes(X509Certificate cert)
		{
			IDictionary dictionary = new Hashtable();
			foreach (SafeBag safeBag in _safeBags)
			{
				if (!safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.3"))
				{
					continue;
				}
				ASN1 aSN = safeBag.ASN1;
				X509Certificate x509Certificate = new X509Certificate(new PKCS7.ContentInfo(aSN[1].Value).Content[0].Value);
				if (!Compare(cert.RawData, x509Certificate.RawData) || aSN.Count != 3)
				{
					continue;
				}
				ASN1 aSN2 = aSN[2];
				for (int i = 0; i < aSN2.Count; i++)
				{
					ASN1 aSN3 = aSN2[i];
					string key = ASN1Convert.ToOid(aSN3[0]);
					ArrayList arrayList = new ArrayList();
					ASN1 aSN4 = aSN3[1];
					for (int j = 0; j < aSN4.Count; j++)
					{
						ASN1 aSN5 = aSN4[j];
						arrayList.Add(aSN5.Value);
					}
					dictionary.Add(key, arrayList);
				}
			}
			return dictionary;
		}

		public void SaveToFile(string filename)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			using FileStream fileStream = File.Create(filename);
			byte[] bytes = GetBytes();
			fileStream.Write(bytes, 0, bytes.Length);
		}

		public object Clone()
		{
			PKCS12 pKCS = null;
			pKCS = ((_password == null) ? new PKCS12(GetBytes()) : new PKCS12(GetBytes(), Encoding.BigEndianUnicode.GetString(_password)));
			pKCS.IterationCount = IterationCount;
			return pKCS;
		}

		private static byte[] LoadFile(string filename)
		{
			byte[] array = null;
			using FileStream fileStream = File.OpenRead(filename);
			array = new byte[fileStream.Length];
			fileStream.Read(array, 0, array.Length);
			fileStream.Close();
			return array;
		}

		public static PKCS12 LoadFromFile(string filename)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			return new PKCS12(LoadFile(filename));
		}

		public static PKCS12 LoadFromFile(string filename, string password)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			return new PKCS12(LoadFile(filename), password);
		}
	}
	public sealed class X501
	{
		private static byte[] countryName = new byte[3] { 85, 4, 6 };

		private static byte[] organizationName = new byte[3] { 85, 4, 10 };

		private static byte[] organizationalUnitName = new byte[3] { 85, 4, 11 };

		private static byte[] commonName = new byte[3] { 85, 4, 3 };

		private static byte[] localityName = new byte[3] { 85, 4, 7 };

		private static byte[] stateOrProvinceName = new byte[3] { 85, 4, 8 };

		private static byte[] streetAddress = new byte[3] { 85, 4, 9 };

		private static byte[] serialNumber = new byte[3] { 85, 4, 5 };

		private static byte[] domainComponent = new byte[10] { 9, 146, 38, 137, 147, 242, 44, 100, 1, 25 };

		private static byte[] userid = new byte[10] { 9, 146, 38, 137, 147, 242, 44, 100, 1, 1 };

		private static byte[] email = new byte[9] { 42, 134, 72, 134, 247, 13, 1, 9, 1 };

		private static byte[] dnQualifier = new byte[3] { 85, 4, 46 };

		private static byte[] title = new byte[3] { 85, 4, 12 };

		private static byte[] surname = new byte[3] { 85, 4, 4 };

		private static byte[] givenName = new byte[3] { 85, 4, 42 };

		private static byte[] initial = new byte[3] { 85, 4, 43 };

		private X501()
		{
		}

		public static string ToString(ASN1 seq)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < seq.Count; i++)
			{
				ASN1 entry = seq[i];
				AppendEntry(stringBuilder, entry, quotes: true);
				if (i < seq.Count - 1)
				{
					stringBuilder.Append(", ");
				}
			}
			return stringBuilder.ToString();
		}

		public static string ToString(ASN1 seq, bool reversed, string separator, bool quotes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (reversed)
			{
				for (int num = seq.Count - 1; num >= 0; num--)
				{
					ASN1 entry = seq[num];
					AppendEntry(stringBuilder, entry, quotes);
					if (num > 0)
					{
						stringBuilder.Append(separator);
					}
				}
			}
			else
			{
				for (int i = 0; i < seq.Count; i++)
				{
					ASN1 entry2 = seq[i];
					AppendEntry(stringBuilder, entry2, quotes);
					if (i < seq.Count - 1)
					{
						stringBuilder.Append(separator);
					}
				}
			}
			return stringBuilder.ToString();
		}

		private static void AppendEntry(StringBuilder sb, ASN1 entry, bool quotes)
		{
			for (int i = 0; i < entry.Count; i++)
			{
				ASN1 aSN = entry[i];
				ASN1 aSN2 = aSN[1];
				if (aSN2 == null)
				{
					continue;
				}
				ASN1 aSN3 = aSN[0];
				if (aSN3 == null)
				{
					continue;
				}
				if (aSN3.CompareValue(countryName))
				{
					sb.Append("C=");
				}
				else if (aSN3.CompareValue(organizationName))
				{
					sb.Append("O=");
				}
				else if (aSN3.CompareValue(organizationalUnitName))
				{
					sb.Append("OU=");
				}
				else if (aSN3.CompareValue(commonName))
				{
					sb.Append("CN=");
				}
				else if (aSN3.CompareValue(localityName))
				{
					sb.Append("L=");
				}
				else if (aSN3.CompareValue(stateOrProvinceName))
				{
					sb.Append("S=");
				}
				else if (aSN3.CompareValue(streetAddress))
				{
					sb.Append("STREET=");
				}
				else if (aSN3.CompareValue(domainComponent))
				{
					sb.Append("DC=");
				}
				else if (aSN3.CompareValue(userid))
				{
					sb.Append("UID=");
				}
				else if (aSN3.CompareValue(email))
				{
					sb.Append("E=");
				}
				else if (aSN3.CompareValue(dnQualifier))
				{
					sb.Append("dnQualifier=");
				}
				else if (aSN3.CompareValue(title))
				{
					sb.Append("T=");
				}
				else if (aSN3.CompareValue(surname))
				{
					sb.Append("SN=");
				}
				else if (aSN3.CompareValue(givenName))
				{
					sb.Append("G=");
				}
				else if (aSN3.CompareValue(initial))
				{
					sb.Append("I=");
				}
				else if (aSN3.CompareValue(serialNumber))
				{
					sb.Append("SERIALNUMBER=");
				}
				else
				{
					sb.Append("OID.");
					sb.Append(ASN1Convert.ToOid(aSN3));
					sb.Append("=");
				}
				string text = null;
				if (aSN2.Tag != 30)
				{
					text = ((aSN2.Tag != 20) ? Encoding.UTF8.GetString(aSN2.Value) : Encoding.UTF7.GetString(aSN2.Value));
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					for (int j = 1; j < aSN2.Value.Length; j += 2)
					{
						stringBuilder.Append((char)aSN2.Value[j]);
					}
					text = stringBuilder.ToString();
				}
				char[] anyOf = new char[9] { ',', '+', '"', '=', '<', '>', ';', '#', '\n' };
				if (quotes && (text.IndexOfAny(anyOf, 0, text.Length) > 0 || text.StartsWith(" ") || text.EndsWith(" ")))
				{
					text = "\"" + text.Replace("\"", "") + "\"";
				}
				sb.Append(text);
				if (i < entry.Count - 1)
				{
					sb.Append(", ");
				}
			}
		}

		private static X520.AttributeTypeAndValue GetAttributeFromOid(string attributeType)
		{
			string text = attributeType.ToUpper(CultureInfo.InvariantCulture).Trim();
			switch (text)
			{
			case "C":
				return new X520.CountryName();
			case "O":
				return new X520.OrganizationName();
			case "OU":
				return new X520.OrganizationalUnitName();
			case "CN":
				return new X520.CommonName();
			case "L":
				return new X520.LocalityName();
			case "S":
			case "ST":
				return new X520.StateOrProvinceName();
			case "E":
				return new X520.EmailAddress();
			case "DC":
				return new X520.DomainComponent();
			case "UID":
				return new X520.UserId();
			case "DNQUALIFIER":
				return new X520.DnQualifier();
			case "T":
				return new X520.Title();
			case "SN":
				return new X520.Surname();
			case "G":
				return new X520.GivenName();
			case "I":
				return new X520.Initial();
			case "SERIALNUMBER":
				return new X520.SerialNumber();
			default:
				if (text.StartsWith("OID."))
				{
					return new X520.Oid(text.Substring(4));
				}
				if (IsOid(text))
				{
					return new X520.Oid(text);
				}
				return null;
			}
		}

		private static bool IsOid(string oid)
		{
			try
			{
				return ASN1Convert.FromOid(oid).Tag == 6;
			}
			catch
			{
				return false;
			}
		}

		private static X520.AttributeTypeAndValue ReadAttribute(string value, ref int pos)
		{
			while (value[pos] == ' ' && pos < value.Length)
			{
				pos++;
			}
			int num = value.IndexOf('=', pos);
			if (num == -1)
			{
				throw new FormatException(global::Locale.GetText("No attribute found."));
			}
			string text = value.Substring(pos, num - pos);
			X520.AttributeTypeAndValue result = GetAttributeFromOid(text) ?? throw new FormatException(string.Format(global::Locale.GetText("Unknown attribute '{0}'."), text));
			pos = num + 1;
			return result;
		}

		private static bool IsHex(char c)
		{
			if (char.IsDigit(c))
			{
				return true;
			}
			char c2 = char.ToUpper(c, CultureInfo.InvariantCulture);
			if (c2 >= 'A')
			{
				return c2 <= 'F';
			}
			return false;
		}

		private static string ReadHex(string value, ref int pos)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(value[pos++]);
			stringBuilder.Append(value[pos]);
			if (pos < value.Length - 4 && value[pos + 1] == '\\' && IsHex(value[pos + 2]))
			{
				pos += 2;
				stringBuilder.Append(value[pos++]);
				stringBuilder.Append(value[pos]);
			}
			byte[] bytes = CryptoConvert.FromHex(stringBuilder.ToString());
			return Encoding.UTF8.GetString(bytes);
		}

		private static int ReadEscaped(StringBuilder sb, string value, int pos)
		{
			switch (value[pos])
			{
			case '"':
			case '#':
			case '+':
			case ',':
			case ';':
			case '<':
			case '=':
			case '>':
			case '\\':
				sb.Append(value[pos]);
				return pos;
			default:
				if (pos >= value.Length - 2)
				{
					throw new FormatException(string.Format(global::Locale.GetText("Malformed escaped value '{0}'."), value.Substring(pos)));
				}
				sb.Append(ReadHex(value, ref pos));
				return pos;
			}
		}

		private static int ReadQuoted(StringBuilder sb, string value, int pos)
		{
			int startIndex = pos;
			while (pos <= value.Length)
			{
				switch (value[pos])
				{
				case '"':
					return pos;
				case '\\':
					return ReadEscaped(sb, value, pos);
				}
				sb.Append(value[pos]);
				pos++;
			}
			throw new FormatException(string.Format(global::Locale.GetText("Malformed quoted value '{0}'."), value.Substring(startIndex)));
		}

		private static string ReadValue(string value, ref int pos)
		{
			int startIndex = pos;
			StringBuilder stringBuilder = new StringBuilder();
			while (pos < value.Length)
			{
				switch (value[pos])
				{
				case '\\':
					pos = ReadEscaped(stringBuilder, value, ++pos);
					break;
				case '"':
					pos = ReadQuoted(stringBuilder, value, ++pos);
					break;
				case ';':
				case '<':
				case '=':
				case '>':
					throw new FormatException(string.Format(global::Locale.GetText("Malformed value '{0}' contains '{1}' outside quotes."), value.Substring(startIndex), value[pos]));
				case '#':
				case '+':
					throw new NotImplementedException();
				case ',':
					pos++;
					return stringBuilder.ToString();
				default:
					stringBuilder.Append(value[pos]);
					break;
				}
				pos++;
			}
			return stringBuilder.ToString();
		}

		public static ASN1 FromString(string rdn)
		{
			if (rdn == null)
			{
				throw new ArgumentNullException("rdn");
			}
			int pos = 0;
			ASN1 aSN = new ASN1(48);
			while (pos < rdn.Length)
			{
				X520.AttributeTypeAndValue attributeTypeAndValue = ReadAttribute(rdn, ref pos);
				attributeTypeAndValue.Value = ReadValue(rdn, ref pos);
				ASN1 aSN2 = new ASN1(49);
				aSN2.Add(attributeTypeAndValue.GetASN1());
				aSN.Add(aSN2);
			}
			return aSN;
		}
	}
	public abstract class X509Builder
	{
		private const string defaultHash = "SHA1";

		private string hashName;

		public string Hash
		{
			get
			{
				return hashName;
			}
			set
			{
				if (hashName == null)
				{
					hashName = "SHA1";
				}
				else
				{
					hashName = value;
				}
			}
		}

		protected X509Builder()
		{
			hashName = "SHA1";
		}

		protected abstract ASN1 ToBeSigned(string hashName);

		protected string GetOid(string hashName)
		{
			return hashName.ToLower(CultureInfo.InvariantCulture) switch
			{
				"md2" => "1.2.840.113549.1.1.2", 
				"md4" => "1.2.840.113549.1.1.3", 
				"md5" => "1.2.840.113549.1.1.4", 
				"sha1" => "1.2.840.113549.1.1.5", 
				"sha256" => "1.2.840.113549.1.1.11", 
				"sha384" => "1.2.840.113549.1.1.12", 
				"sha512" => "1.2.840.113549.1.1.13", 
				_ => throw new NotSupportedException("Unknown hash algorithm " + hashName), 
			};
		}

		public virtual byte[] Sign(AsymmetricAlgorithm aa)
		{
			if (aa is RSA)
			{
				return Sign(aa as RSA);
			}
			if (aa is DSA)
			{
				return Sign(aa as DSA);
			}
			throw new NotSupportedException("Unknown Asymmetric Algorithm " + aa.ToString());
		}

		private byte[] Build(ASN1 tbs, string hashoid, byte[] signature)
		{
			ASN1 aSN = new ASN1(48);
			aSN.Add(tbs);
			aSN.Add(PKCS7.AlgorithmIdentifier(hashoid));
			byte[] array = new byte[signature.Length + 1];
			Buffer.BlockCopy(signature, 0, array, 1, signature.Length);
			aSN.Add(new ASN1(3, array));
			return aSN.GetBytes();
		}

		public virtual byte[] Sign(RSA key)
		{
			string oid = GetOid(hashName);
			ASN1 aSN = ToBeSigned(oid);
			byte[] rgbHash = HashAlgorithm.Create(hashName).ComputeHash(aSN.GetBytes());
			RSAPKCS1SignatureFormatter rSAPKCS1SignatureFormatter = new RSAPKCS1SignatureFormatter(key);
			rSAPKCS1SignatureFormatter.SetHashAlgorithm(hashName);
			byte[] signature = rSAPKCS1SignatureFormatter.CreateSignature(rgbHash);
			return Build(aSN, oid, signature);
		}

		public virtual byte[] Sign(DSA key)
		{
			string hashoid = "1.2.840.10040.4.3";
			ASN1 aSN = ToBeSigned(hashoid);
			HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashName);
			if (!(hashAlgorithm is SHA1))
			{
				throw new NotSupportedException("Only SHA-1 is supported for DSA");
			}
			byte[] rgbHash = hashAlgorithm.ComputeHash(aSN.GetBytes());
			DSASignatureFormatter dSASignatureFormatter = new DSASignatureFormatter(key);
			dSASignatureFormatter.SetHashAlgorithm(hashName);
			byte[] src = dSASignatureFormatter.CreateSignature(rgbHash);
			byte[] array = new byte[20];
			Buffer.BlockCopy(src, 0, array, 0, 20);
			byte[] array2 = new byte[20];
			Buffer.BlockCopy(src, 20, array2, 0, 20);
			ASN1 aSN2 = new ASN1(48);
			aSN2.Add(new ASN1(2, array));
			aSN2.Add(new ASN1(2, array2));
			return Build(aSN, hashoid, aSN2.GetBytes());
		}
	}
	public class X509Crl
	{
		public class X509CrlEntry
		{
			private byte[] sn;

			private DateTime revocationDate;

			private X509ExtensionCollection extensions;

			public byte[] SerialNumber => (byte[])sn.Clone();

			public DateTime RevocationDate => revocationDate;

			public X509ExtensionCollection Extensions => extensions;

			internal X509CrlEntry(byte[] serialNumber, DateTime revocationDate, X509ExtensionCollection extensions)
			{
				sn = serialNumber;
				this.revocationDate = revocationDate;
				if (extensions == null)
				{
					this.extensions = new X509ExtensionCollection();
				}
				else
				{
					this.extensions = extensions;
				}
			}

			internal X509CrlEntry(ASN1 entry)
			{
				sn = entry[0].Value;
				Array.Reverse(sn);
				revocationDate = ASN1Convert.ToDateTime(entry[1]);
				extensions = new X509ExtensionCollection(entry[2]);
			}

			public byte[] GetBytes()
			{
				ASN1 aSN = new ASN1(48);
				aSN.Add(new ASN1(2, sn));
				aSN.Add(ASN1Convert.FromDateTime(revocationDate));
				if (extensions.Count > 0)
				{
					aSN.Add(new ASN1(extensions.GetBytes()));
				}
				return aSN.GetBytes();
			}
		}

		private string issuer;

		private byte version;

		private DateTime thisUpdate;

		private DateTime nextUpdate;

		private ArrayList entries;

		private string signatureOID;

		private byte[] signature;

		private X509ExtensionCollection extensions;

		private byte[] encoded;

		private byte[] hash_value;

		public ArrayList Entries => ArrayList.ReadOnly(entries);

		public X509CrlEntry this[int index] => (X509CrlEntry)entries[index];

		public X509CrlEntry this[byte[] serialNumber] => GetCrlEntry(serialNumber);

		public X509ExtensionCollection Extensions => extensions;

		public byte[] Hash
		{
			get
			{
				if (hash_value == null)
				{
					byte[] bytes = new ASN1(encoded)[0].GetBytes();
					using HashAlgorithm hashAlgorithm = PKCS1.CreateFromOid(signatureOID);
					hash_value = hashAlgorithm.ComputeHash(bytes);
				}
				return hash_value;
			}
		}

		public string IssuerName => issuer;

		public DateTime NextUpdate => nextUpdate;

		public DateTime ThisUpdate => thisUpdate;

		public string SignatureAlgorithm => signatureOID;

		public byte[] Signature
		{
			get
			{
				if (signature == null)
				{
					return null;
				}
				return (byte[])signature.Clone();
			}
		}

		public byte[] RawData => (byte[])encoded.Clone();

		public byte Version => version;

		public bool IsCurrent => WasCurrent(DateTime.Now);

		public X509Crl(byte[] crl)
		{
			if (crl == null)
			{
				throw new ArgumentNullException("crl");
			}
			encoded = (byte[])crl.Clone();
			Parse(encoded);
		}

		private void Parse(byte[] crl)
		{
			string text = "Input data cannot be coded as a valid CRL.";
			try
			{
				ASN1 aSN = new ASN1(encoded);
				if (aSN.Tag != 48 || aSN.Count != 3)
				{
					throw new CryptographicException(text);
				}
				ASN1 aSN2 = aSN[0];
				if (aSN2.Tag != 48 || aSN2.Count < 3)
				{
					throw new CryptographicException(text);
				}
				int num = 0;
				if (aSN2[num].Tag == 2)
				{
					version = (byte)(aSN2[num++].Value[0] + 1);
				}
				else
				{
					version = 1;
				}
				signatureOID = ASN1Convert.ToOid(aSN2[num++][0]);
				issuer = X501.ToString(aSN2[num++]);
				thisUpdate = ASN1Convert.ToDateTime(aSN2[num++]);
				ASN1 aSN3 = aSN2[num++];
				if (aSN3.Tag == 23 || aSN3.Tag == 24)
				{
					nextUpdate = ASN1Convert.ToDateTime(aSN3);
					aSN3 = aSN2[num++];
				}
				entries = new ArrayList();
				if (aSN3 != null && aSN3.Tag == 48)
				{
					ASN1 aSN4 = aSN3;
					for (int i = 0; i < aSN4.Count; i++)
					{
						entries.Add(new X509CrlEntry(aSN4[i]));
					}
				}
				else
				{
					num--;
				}
				ASN1 aSN5 = aSN2[num];
				if (aSN5 != null && aSN5.Tag == 160 && aSN5.Count == 1)
				{
					extensions = new X509ExtensionCollection(aSN5[0]);
				}
				else
				{
					extensions = new X509ExtensionCollection(null);
				}
				string text2 = ASN1Convert.ToOid(aSN[1][0]);
				if (signatureOID != text2)
				{
					throw new CryptographicException(text + " [Non-matching signature algorithms in CRL]");
				}
				byte[] value = aSN[2].Value;
				signature = new byte[value.Length - 1];
				Buffer.BlockCopy(value, 1, signature, 0, signature.Length);
			}
			catch
			{
				throw new CryptographicException(text);
			}
		}

		public bool WasCurrent(DateTime instant)
		{
			if (nextUpdate == DateTime.MinValue)
			{
				return instant >= thisUpdate;
			}
			if (instant >= thisUpdate)
			{
				return instant <= nextUpdate;
			}
			return false;
		}

		public byte[] GetBytes()
		{
			return (byte[])encoded.Clone();
		}

		private bool Compare(byte[] array1, byte[] array2)
		{
			if (array1 == null && array2 == null)
			{
				return true;
			}
			if (array1 == null || array2 == null)
			{
				return false;
			}
			if (array1.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array1.Length; i++)
			{
				if (array1[i] != array2[i])
				{
					return false;
				}
			}
			return true;
		}

		public X509CrlEntry GetCrlEntry(X509Certificate x509)
		{
			if (x509 == null)
			{
				throw new ArgumentNullException("x509");
			}
			return GetCrlEntry(x509.SerialNumber);
		}

		public X509CrlEntry GetCrlEntry(byte[] serialNumber)
		{
			if (serialNumber == null)
			{
				throw new ArgumentNullException("serialNumber");
			}
			for (int i = 0; i < entries.Count; i++)
			{
				X509CrlEntry x509CrlEntry = (X509CrlEntry)entries[i];
				if (Compare(serialNumber, x509CrlEntry.SerialNumber))
				{
					return x509CrlEntry;
				}
			}
			return null;
		}

		public bool VerifySignature(X509Certificate x509)
		{
			if (x509 == null)
			{
				throw new ArgumentNullException("x509");
			}
			if (x509.Version >= 3)
			{
				BasicConstraintsExtension basicConstraintsExtension = null;
				X509Extension x509Extension = x509.Extensions["2.5.29.19"];
				if (x509Extension != null)
				{
					basicConstraintsExtension = new BasicConstraintsExtension(x509Extension);
					if (!basicConstraintsExtension.CertificateAuthority)
					{
						return false;
					}
				}
				x509Extension = x509.Extensions["2.5.29.15"];
				if (x509Extension != null)
				{
					KeyUsageExtension keyUsageExtension = new KeyUsageExtension(x509Extension);
					if (!keyUsageExtension.Support(KeyUsages.cRLSign) && (basicConstraintsExtension == null || !keyUsageExtension.Support(KeyUsages.digitalSignature)))
					{
						return false;
					}
				}
			}
			if (issuer != x509.SubjectName)
			{
				return false;
			}
			if (signatureOID == "1.2.840.10040.4.3")
			{
				return VerifySignature(x509.DSA);
			}
			return VerifySignature(x509.RSA);
		}

		internal bool VerifySignature(DSA dsa)
		{
			if (signatureOID != "1.2.840.10040.4.3")
			{
				throw new CryptographicException("Unsupported hash algorithm: " + signatureOID);
			}
			DSASignatureDeformatter dSASignatureDeformatter = new DSASignatureDeformatter(dsa);
			dSASignatureDeformatter.SetHashAlgorithm("SHA1");
			ASN1 aSN = new ASN1(signature);
			if (aSN == null || aSN.Count != 2)
			{
				return false;
			}
			byte[] value = aSN[0].Value;
			byte[] value2 = aSN[1].Value;
			byte[] array = new byte[40];
			int num = System.Math.Max(0, value.Length - 20);
			int dstOffset = System.Math.Max(0, 20 - value.Length);
			Buffer.BlockCopy(value, num, array, dstOffset, value.Length - num);
			int num2 = System.Math.Max(0, value2.Length - 20);
			int dstOffset2 = System.Math.Max(20, 40 - value2.Length);
			Buffer.BlockCopy(value2, num2, array, dstOffset2, value2.Length - num2);
			return dSASignatureDeformatter.VerifySignature(Hash, array);
		}

		internal bool VerifySignature(RSA rsa)
		{
			RSAPKCS1SignatureDeformatter rSAPKCS1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
			rSAPKCS1SignatureDeformatter.SetHashAlgorithm(PKCS1.HashNameFromOid(signatureOID));
			return rSAPKCS1SignatureDeformatter.VerifySignature(Hash, signature);
		}

		public bool VerifySignature(AsymmetricAlgorithm aa)
		{
			if (aa == null)
			{
				throw new ArgumentNullException("aa");
			}
			if (aa is RSA)
			{
				return VerifySignature(aa as RSA);
			}
			if (aa is DSA)
			{
				return VerifySignature(aa as DSA);
			}
			throw new NotSupportedException("Unknown Asymmetric Algorithm " + aa.ToString());
		}

		public static X509Crl CreateFromFile(string filename)
		{
			byte[] array = null;
			using (FileStream fileStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
			}
			return new X509Crl(array);
		}
	}
	public class X509Certificate : ISerializable
	{
		private ASN1 decoder;

		private byte[] m_encodedcert;

		private DateTime m_from;

		private DateTime m_until;

		private ASN1 issuer;

		private string m_issuername;

		private string m_keyalgo;

		private byte[] m_keyalgoparams;

		private ASN1 subject;

		private string m_subject;

		private byte[] m_publickey;

		private byte[] signature;

		private string m_signaturealgo;

		private byte[] m_signaturealgoparams;

		private byte[] certhash;

		private RSA _rsa;

		private DSA _dsa;

		internal const string OID_DSA = "1.2.840.10040.4.1";

		internal const string OID_RSA = "1.2.840.113549.1.1.1";

		internal const string OID_ECC = "1.2.840.10045.2.1";

		private int version;

		private byte[] serialnumber;

		private byte[] issuerUniqueID;

		private byte[] subjectUniqueID;

		private X509ExtensionCollection extensions;

		private static string encoding_error = global::Locale.GetText("Input data cannot be coded as a valid certificate.");

		public DSA DSA
		{
			get
			{
				if (m_keyalgoparams == null)
				{
					throw new CryptographicException("Missing key algorithm parameters.");
				}
				if (_dsa == null && m_keyalgo == "1.2.840.10040.4.1")
				{
					DSAParameters parameters = default(DSAParameters);
					ASN1 aSN = new ASN1(m_publickey);
					if (aSN == null || aSN.Tag != 2)
					{
						return null;
					}
					parameters.Y = GetUnsignedBigInteger(aSN.Value);
					ASN1 aSN2 = new ASN1(m_keyalgoparams);
					if (aSN2 == null || aSN2.Tag != 48 || aSN2.Count < 3)
					{
						return null;
					}
					if (aSN2[0].Tag != 2 || aSN2[1].Tag != 2 || aSN2[2].Tag != 2)
					{
						return null;
					}
					parameters.P = GetUnsignedBigInteger(aSN2[0].Value);
					parameters.Q = GetUnsignedBigInteger(aSN2[1].Value);
					parameters.G = GetUnsignedBigInteger(aSN2[2].Value);
					_dsa = new DSACryptoServiceProvider(parameters.Y.Length << 3);
					_dsa.ImportParameters(parameters);
				}
				return _dsa;
			}
			set
			{
				_dsa = value;
				if (value != null)
				{
					_rsa = null;
				}
			}
		}

		public X509ExtensionCollection Extensions => extensions;

		public byte[] Hash
		{
			get
			{
				if (certhash == null)
				{
					if (decoder == null || decoder.Count < 1)
					{
						return null;
					}
					string text = PKCS1.HashNameFromOid(m_signaturealgo, throwOnError: false);
					if (text == null)
					{
						return null;
					}
					byte[] bytes = decoder[0].GetBytes();
					using HashAlgorithm hashAlgorithm = PKCS1.CreateFromName(text);
					certhash = hashAlgorithm.ComputeHash(bytes, 0, bytes.Length);
				}
				return (byte[])certhash.Clone();
			}
		}

		public virtual string IssuerName => m_issuername;

		public virtual string KeyAlgorithm => m_keyalgo;

		public virtual byte[] KeyAlgorithmParameters
		{
			get
			{
				if (m_keyalgoparams == null)
				{
					return null;
				}
				return (byte[])m_keyalgoparams.Clone();
			}
			set
			{
				m_keyalgoparams = value;
			}
		}

		public virtual byte[] PublicKey
		{
			get
			{
				if (m_publickey == null)
				{
					return null;
				}
				return (byte[])m_publickey.Clone();
			}
		}

		public virtual RSA RSA
		{
			get
			{
				if (_rsa == null && m_keyalgo == "1.2.840.113549.1.1.1")
				{
					RSAParameters parameters = default(RSAParameters);
					ASN1 aSN = new ASN1(m_publickey);
					ASN1 aSN2 = aSN[0];
					if (aSN2 == null || aSN2.Tag != 2)
					{
						return null;
					}
					ASN1 aSN3 = aSN[1];
					if (aSN3.Tag != 2)
					{
						return null;
					}
					parameters.Modulus = GetUnsignedBigInteger(aSN2.Value);
					parameters.Exponent = aSN3.Value;
					int dwKeySize = parameters.Modulus.Length << 3;
					_rsa = new RSACryptoServiceProvider(dwKeySize);
					_rsa.ImportParameters(parameters);
				}
				return _rsa;
			}
			set
			{
				if (value != null)
				{
					_dsa = null;
				}
				_rsa = value;
			}
		}

		public virtual byte[] RawData
		{
			get
			{
				if (m_encodedcert == null)
				{
					return null;
				}
				return (byte[])m_encodedcert.Clone();
			}
		}

		public virtual byte[] SerialNumber
		{
			get
			{
				if (serialnumber == null)
				{
					return null;
				}
				return (byte[])serialnumber.Clone();
			}
		}

		public virtual byte[] Signature
		{
			get
			{
				if (signature == null)
				{
					return null;
				}
				switch (m_signaturealgo)
				{
				case "1.2.840.113549.1.1.2":
				case "1.2.840.113549.1.1.3":
				case "1.2.840.113549.1.1.4":
				case "1.2.840.113549.1.1.5":
				case "1.3.14.3.2.29":
				case "1.2.840.113549.1.1.11":
				case "1.2.840.113549.1.1.12":
				case "1.2.840.113549.1.1.13":
				case "1.3.36.3.3.1.2":
					return (byte[])signature.Clone();
				case "1.2.840.10040.4.3":
				{
					ASN1 aSN = new ASN1(signature);
					if (aSN == null || aSN.Count != 2)
					{
						return null;
					}
					byte[] value = aSN[0].Value;
					byte[] value2 = aSN[1].Value;
					byte[] array = new byte[40];
					int num = System.Math.Max(0, value.Length - 20);
					int dstOffset = System.Math.Max(0, 20 - value.Length);
					Buffer.BlockCopy(value, num, array, dstOffset, value.Length - num);
					int num2 = System.Math.Max(0, value2.Length - 20);
					int dstOffset2 = System.Math.Max(20, 40 - value2.Length);
					Buffer.BlockCopy(value2, num2, array, dstOffset2, value2.Length - num2);
					return array;
				}
				default:
					throw new CryptographicException("Unsupported hash algorithm: " + m_signaturealgo);
				}
			}
		}

		public virtual string SignatureAlgorithm => m_signaturealgo;

		public virtual byte[] SignatureAlgorithmParameters
		{
			get
			{
				if (m_signaturealgoparams == null)
				{
					return m_signaturealgoparams;
				}
				return (byte[])m_signaturealgoparams.Clone();
			}
		}

		public virtual string SubjectName => m_subject;

		public virtual DateTime ValidFrom => m_from;

		public virtual DateTime ValidUntil => m_until;

		public int Version => version;

		public bool IsCurrent => WasCurrent(DateTime.UtcNow);

		public byte[] IssuerUniqueIdentifier
		{
			get
			{
				if (issuerUniqueID == null)
				{
					return null;
				}
				return (byte[])issuerUniqueID.Clone();
			}
		}

		public byte[] SubjectUniqueIdentifier
		{
			get
			{
				if (subjectUniqueID == null)
				{
					return null;
				}
				return (byte[])subjectUniqueID.Clone();
			}
		}

		public bool IsSelfSigned
		{
			get
			{
				if (m_issuername != m_subject)
				{
					return false;
				}
				try
				{
					if (RSA != null)
					{
						return VerifySignature(RSA);
					}
					if (DSA != null)
					{
						return VerifySignature(DSA);
					}
					return false;
				}
				catch (CryptographicException)
				{
					return false;
				}
			}
		}

		private void Parse(byte[] data)
		{
			try
			{
				decoder = new ASN1(data);
				if (decoder.Tag != 48)
				{
					throw new CryptographicException(encoding_error);
				}
				if (decoder[0].Tag != 48)
				{
					throw new CryptographicException(encoding_error);
				}
				ASN1 aSN = decoder[0];
				int num = 0;
				ASN1 aSN2 = decoder[0][num];
				version = 1;
				if (aSN2.Tag == 160 && aSN2.Count > 0)
				{
					version += aSN2[0].Value[0];
					num++;
				}
				ASN1 aSN3 = decoder[0][num++];
				if (aSN3.Tag != 2)
				{
					throw new CryptographicException(encoding_error);
				}
				serialnumber = aSN3.Value;
				Array.Reverse(serialnumber, 0, serialnumber.Length);
				num++;
				issuer = aSN.Element(num++, 48);
				m_issuername = X501.ToString(issuer);
				ASN1 aSN4 = aSN.Element(num++, 48);
				ASN1 time = aSN4[0];
				m_from = ASN1Convert.ToDateTime(time);
				ASN1 time2 = aSN4[1];
				m_until = ASN1Convert.ToDateTime(time2);
				subject = aSN.Element(num++, 48);
				m_subject = X501.ToString(subject);
				ASN1 aSN5 = aSN.Element(num++, 48);
				ASN1 aSN6 = aSN5.Element(0, 48);
				ASN1 asn = aSN6.Element(0, 6);
				m_keyalgo = ASN1Convert.ToOid(asn);
				ASN1 aSN7 = aSN6[1];
				m_keyalgoparams = ((aSN6.Count > 1) ? aSN7.GetBytes() : null);
				ASN1 aSN8 = aSN5.Element(1, 3);
				int num2 = aSN8.Length - 1;
				m_publickey = new byte[num2];
				Buffer.BlockCopy(aSN8.Value, 1, m_publickey, 0, num2);
				byte[] value = decoder[2].Value;
				signature = new byte[value.Length - 1];
				Buffer.BlockCopy(value, 1, signature, 0, signature.Length);
				aSN6 = decoder[1];
				asn = aSN6.Element(0, 6);
				m_signaturealgo = ASN1Convert.ToOid(asn);
				aSN7 = aSN6[1];
				if (aSN7 != null)
				{
					m_signaturealgoparams = aSN7.GetBytes();
				}
				else
				{
					m_signaturealgoparams = null;
				}
				ASN1 aSN9 = aSN.Element(num, 129);
				if (aSN9 != null)
				{
					num++;
					issuerUniqueID = aSN9.Value;
				}
				ASN1 aSN10 = aSN.Element(num, 130);
				if (aSN10 != null)
				{
					num++;
					subjectUniqueID = aSN10.Value;
				}
				ASN1 aSN11 = aSN.Element(num, 163);
				if (aSN11 != null && aSN11.Count == 1)
				{
					extensions = new X509ExtensionCollection(aSN11[0]);
				}
				else
				{
					extensions = new X509ExtensionCollection(null);
				}
				m_encodedcert = (byte[])data.Clone();
			}
			catch (Exception inner)
			{
				throw new CryptographicException(encoding_error, inner);
			}
		}

		public X509Certificate(byte[] data)
		{
			if (data == null)
			{
				return;
			}
			if (data.Length != 0 && data[0] != 48)
			{
				try
				{
					data = PEM("CERTIFICATE", data);
				}
				catch (Exception inner)
				{
					throw new CryptographicException(encoding_error, inner);
				}
			}
			Parse(data);
		}

		private byte[] GetUnsignedBigInteger(byte[] integer)
		{
			if (integer[0] == 0)
			{
				int num = integer.Length - 1;
				byte[] array = new byte[num];
				Buffer.BlockCopy(integer, 1, array, 0, num);
				return array;
			}
			return integer;
		}

		public bool WasCurrent(DateTime instant)
		{
			if (instant > ValidFrom)
			{
				return instant <= ValidUntil;
			}
			return false;
		}

		internal bool VerifySignature(DSA dsa)
		{
			DSASignatureDeformatter dSASignatureDeformatter = new DSASignatureDeformatter(dsa);
			dSASignatureDeformatter.SetHashAlgorithm("SHA1");
			return dSASignatureDeformatter.VerifySignature(Hash, Signature);
		}

		internal bool VerifySignature(RSA rsa)
		{
			if (m_signaturealgo == "1.2.840.10040.4.3")
			{
				return false;
			}
			RSAPKCS1SignatureDeformatter rSAPKCS1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
			rSAPKCS1SignatureDeformatter.SetHashAlgorithm(PKCS1.HashNameFromOid(m_signaturealgo));
			return rSAPKCS1SignatureDeformatter.VerifySignature(Hash, Signature);
		}

		public bool VerifySignature(AsymmetricAlgorithm aa)
		{
			if (aa == null)
			{
				throw new ArgumentNullException("aa");
			}
			if (aa is RSA)
			{
				return VerifySignature(aa as RSA);
			}
			if (aa is DSA)
			{
				return VerifySignature(aa as DSA);
			}
			throw new NotSupportedException("Unknown Asymmetric Algorithm " + aa.ToString());
		}

		public bool CheckSignature(byte[] hash, string hashAlgorithm, byte[] signature)
		{
			return ((RSACryptoServiceProvider)RSA).VerifyHash(hash, hashAlgorithm, signature);
		}

		public ASN1 GetIssuerName()
		{
			return issuer;
		}

		public ASN1 GetSubjectName()
		{
			return subject;
		}

		protected X509Certificate(SerializationInfo info, StreamingContext context)
		{
			Parse((byte[])info.GetValue("raw", typeof(byte[])));
		}

		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("raw", m_encodedcert);
		}

		private static byte[] PEM(string type, byte[] data)
		{
			string text = Encoding.ASCII.GetString(data);
			string text2 = $"-----BEGIN {type}-----";
			string value = $"-----END {type}-----";
			int num = text.IndexOf(text2) + text2.Length;
			int num2 = text.IndexOf(value, num);
			return Convert.FromBase64String(text.Substring(num, num2 - num));
		}
	}
	public class X509CertificateBuilder : X509Builder
	{
		private byte version;

		private byte[] sn;

		private string issuer;

		private DateTime notBefore;

		private DateTime notAfter;

		private string subject;

		private AsymmetricAlgorithm aa;

		private byte[] issuerUniqueID;

		private byte[] subjectUniqueID;

		private X509ExtensionCollection extensions;

		public byte Version
		{
			get
			{
				return version;
			}
			set
			{
				version = value;
			}
		}

		public byte[] SerialNumber
		{
			get
			{
				return sn;
			}
			set
			{
				sn = value;
			}
		}

		public string IssuerName
		{
			get
			{
				return issuer;
			}
			set
			{
				issuer = value;
			}
		}

		public DateTime NotBefore
		{
			get
			{
				return notBefore;
			}
			set
			{
				notBefore = value;
			}
		}

		public DateTime NotAfter
		{
			get
			{
				return notAfter;
			}
			set
			{
				notAfter = value;
			}
		}

		public string SubjectName
		{
			get
			{
				return subject;
			}
			set
			{
				subject = value;
			}
		}

		public AsymmetricAlgorithm SubjectPublicKey
		{
			get
			{
				return aa;
			}
			set
			{
				aa = value;
			}
		}

		public byte[] IssuerUniqueId
		{
			get
			{
				return issuerUniqueID;
			}
			set
			{
				issuerUniqueID = value;
			}
		}

		public byte[] SubjectUniqueId
		{
			get
			{
				return subjectUniqueID;
			}
			set
			{
				subjectUniqueID = value;
			}
		}

		public X509ExtensionCollection Extensions => extensions;

		public X509CertificateBuilder()
			: this(3)
		{
		}

		public X509CertificateBuilder(byte version)
		{
			if (version > 3)
			{
				throw new ArgumentException("Invalid certificate version");
			}
			this.version = version;
			extensions = new X509ExtensionCollection();
		}

		private ASN1 SubjectPublicKeyInfo()
		{
			ASN1 aSN = new ASN1(48);
			if (aa is RSA)
			{
				aSN.Add(PKCS7.AlgorithmIdentifier("1.2.840.113549.1.1.1"));
				RSAParameters rSAParameters = (aa as RSA).ExportParameters(includePrivateParameters: false);
				ASN1 aSN2 = new ASN1(48);
				aSN2.Add(ASN1Convert.FromUnsignedBigInteger(rSAParameters.Modulus));
				aSN2.Add(ASN1Convert.FromUnsignedBigInteger(rSAParameters.Exponent));
				aSN.Add(new ASN1(UniqueIdentifier(aSN2.GetBytes())));
			}
			else
			{
				if (!(aa is DSA))
				{
					throw new NotSupportedException("Unknown Asymmetric Algorithm " + aa.ToString());
				}
				DSAParameters dSAParameters = (aa as DSA).ExportParameters(includePrivateParameters: false);
				ASN1 aSN3 = new ASN1(48);
				aSN3.Add(ASN1Convert.FromUnsignedBigInteger(dSAParameters.P));
				aSN3.Add(ASN1Convert.FromUnsignedBigInteger(dSAParameters.Q));
				aSN3.Add(ASN1Convert.FromUnsignedBigInteger(dSAParameters.G));
				aSN.Add(PKCS7.AlgorithmIdentifier("1.2.840.10040.4.1", aSN3));
				aSN.Add(new ASN1(3)).Add(ASN1Convert.FromUnsignedBigInteger(dSAParameters.Y));
			}
			return aSN;
		}

		private byte[] UniqueIdentifier(byte[] id)
		{
			ASN1 aSN = new ASN1(3);
			byte[] array = new byte[id.Length + 1];
			Buffer.BlockCopy(id, 0, array, 1, id.Length);
			aSN.Value = array;
			return aSN.GetBytes();
		}

		protected override ASN1 ToBeSigned(string oid)
		{
			ASN1 aSN = new ASN1(48);
			if (version > 1)
			{
				byte[] data = new byte[1] { (byte)(version - 1) };
				aSN.Add(new ASN1(160)).Add(new ASN1(2, data));
			}
			aSN.Add(new ASN1(2, sn));
			aSN.Add(PKCS7.AlgorithmIdentifier(oid));
			aSN.Add(X501.FromString(issuer));
			ASN1 aSN2 = aSN.Add(new ASN1(48));
			aSN2.Add(ASN1Convert.FromDateTime(notBefore));
			aSN2.Add(ASN1Convert.FromDateTime(notAfter));
			aSN.Add(X501.FromString(subject));
			aSN.Add(SubjectPublicKeyInfo());
			if (version > 1)
			{
				if (issuerUniqueID != null)
				{
					aSN.Add(new ASN1(161, UniqueIdentifier(issuerUniqueID)));
				}
				if (subjectUniqueID != null)
				{
					aSN.Add(new ASN1(161, UniqueIdentifier(subjectUniqueID)));
				}
				if (version > 2 && extensions.Count > 0)
				{
					aSN.Add(new ASN1(163, extensions.GetBytes()));
				}
			}
			return aSN;
		}
	}
	[Serializable]
	public class X509CertificateCollection : CollectionBase, IEnumerable
	{
		public class X509CertificateEnumerator : IEnumerator
		{
			private IEnumerator enumerator;

			public X509Certificate Current => (X509Certificate)enumerator.Current;

			object IEnumerator.Current => enumerator.Current;

			public X509CertificateEnumerator(X509CertificateCollection mappings)
			{
				enumerator = ((IEnumerable)mappings).GetEnumerator();
			}

			bool IEnumerator.MoveNext()
			{
				return enumerator.MoveNext();
			}

			void IEnumerator.Reset()
			{
				enumerator.Reset();
			}

			public bool MoveNext()
			{
				return enumerator.MoveNext();
			}

			public void Reset()
			{
				enumerator.Reset();
			}
		}

		public X509Certificate this[int index]
		{
			get
			{
				return (X509Certificate)base.InnerList[index];
			}
			set
			{
				base.InnerList[index] = value;
			}
		}

		public X509CertificateCollection()
		{
		}

		public X509CertificateCollection(X509Certificate[] value)
		{
			AddRange(value);
		}

		public X509CertificateCollection(X509CertificateCollection value)
		{
			AddRange(value);
		}

		public int Add(X509Certificate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return base.InnerList.Add(value);
		}

		public void AddRange(X509Certificate[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Length; i++)
			{
				base.InnerList.Add(value[i]);
			}
		}

		public void AddRange(X509CertificateCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.InnerList.Count; i++)
			{
				base.InnerList.Add(value[i]);
			}
		}

		public bool Contains(X509Certificate value)
		{
			return IndexOf(value) != -1;
		}

		public void CopyTo(X509Certificate[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		public new X509CertificateEnumerator GetEnumerator()
		{
			return new X509CertificateEnumerator(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return base.InnerList.GetEnumerator();
		}

		public override int GetHashCode()
		{
			return base.InnerList.GetHashCode();
		}

		public int IndexOf(X509Certificate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			byte[] hash = value.Hash;
			for (int i = 0; i < base.InnerList.Count; i++)
			{
				X509Certificate x509Certificate = (X509Certificate)base.InnerList[i];
				if (Compare(x509Certificate.Hash, hash))
				{
					return i;
				}
			}
			return -1;
		}

		public void Insert(int index, X509Certificate value)
		{
			base.InnerList.Insert(index, value);
		}

		public void Remove(X509Certificate value)
		{
			base.InnerList.Remove(value);
		}

		private bool Compare(byte[] array1, byte[] array2)
		{
			if (array1 == null && array2 == null)
			{
				return true;
			}
			if (array1 == null || array2 == null)
			{
				return false;
			}
			if (array1.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array1.Length; i++)
			{
				if (array1[i] != array2[i])
				{
					return false;
				}
			}
			return true;
		}
	}
	public class X509Chain
	{
		private X509CertificateCollection roots;

		private X509CertificateCollection certs;

		private X509Certificate _root;

		private X509CertificateCollection _chain;

		private X509ChainStatusFlags _status;

		public X509CertificateCollection Chain => _chain;

		public X509Certificate Root => _root;

		public X509ChainStatusFlags Status => _status;

		public X509CertificateCollection TrustAnchors
		{
			get
			{
				if (roots == null)
				{
					roots = new X509CertificateCollection();
					roots.AddRange(X509StoreManager.TrustedRootCertificates);
					return roots;
				}
				return roots;
			}
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
			set
			{
				roots = value;
			}
		}

		public X509Chain()
		{
			certs = new X509CertificateCollection();
		}

		public X509Chain(X509CertificateCollection chain)
			: this()
		{
			_chain = new X509CertificateCollection();
			_chain.AddRange(chain);
		}

		public void LoadCertificate(X509Certificate x509)
		{
			certs.Add(x509);
		}

		public void LoadCertificates(X509CertificateCollection collection)
		{
			certs.AddRange(collection);
		}

		public X509Certificate FindByIssuerName(string issuerName)
		{
			foreach (X509Certificate cert in certs)
			{
				if (cert.IssuerName == issuerName)
				{
					return cert;
				}
			}
			return null;
		}

		public bool Build(X509Certificate leaf)
		{
			_status = X509ChainStatusFlags.NoError;
			if (_chain == null)
			{
				_chain = new X509CertificateCollection();
				X509Certificate x509Certificate = leaf;
				X509Certificate potentialRoot = x509Certificate;
				while (x509Certificate != null && !x509Certificate.IsSelfSigned)
				{
					potentialRoot = x509Certificate;
					_chain.Add(x509Certificate);
					x509Certificate = FindCertificateParent(x509Certificate);
				}
				_root = FindCertificateRoot(potentialRoot);
			}
			else
			{
				int count = _chain.Count;
				if (count > 0)
				{
					if (IsParent(leaf, _chain[0]))
					{
						int i;
						for (i = 1; i < count && IsParent(_chain[i - 1], _chain[i]); i++)
						{
						}
						if (i == count)
						{
							_root = FindCertificateRoot(_chain[count - 1]);
						}
					}
				}
				else
				{
					_root = FindCertificateRoot(leaf);
				}
			}
			if (_chain != null && _status == X509ChainStatusFlags.NoError)
			{
				foreach (X509Certificate item in _chain)
				{
					if (!IsValid(item))
					{
						return false;
					}
				}
				if (!IsValid(leaf))
				{
					if (_status == X509ChainStatusFlags.NotTimeNested)
					{
						_status = X509ChainStatusFlags.NotTimeValid;
					}
					return false;
				}
				if (_root != null && !IsValid(_root))
				{
					return false;
				}
			}
			return _status == X509ChainStatusFlags.NoError;
		}

		public void Reset()
		{
			_status = X509ChainStatusFlags.NoError;
			roots = null;
			certs.Clear();
			if (_chain != null)
			{
				_chain.Clear();
			}
		}

		private bool IsValid(X509Certificate cert)
		{
			if (!cert.IsCurrent)
			{
				_status = X509ChainStatusFlags.NotTimeNested;
				return false;
			}
			_ = ServicePointManager.CheckCertificateRevocationList;
			return true;
		}

		private X509Certificate FindCertificateParent(X509Certificate child)
		{
			foreach (X509Certificate cert in certs)
			{
				if (IsParent(child, cert))
				{
					return cert;
				}
			}
			return null;
		}

		private X509Certificate FindCertificateRoot(X509Certificate potentialRoot)
		{
			if (potentialRoot == null)
			{
				_status = X509ChainStatusFlags.PartialChain;
				return null;
			}
			if (IsTrusted(potentialRoot))
			{
				return potentialRoot;
			}
			foreach (X509Certificate trustAnchor in TrustAnchors)
			{
				if (IsParent(potentialRoot, trustAnchor))
				{
					return trustAnchor;
				}
			}
			if (potentialRoot.IsSelfSigned)
			{
				_status = X509ChainStatusFlags.UntrustedRoot;
				return potentialRoot;
			}
			_status = X509ChainStatusFlags.PartialChain;
			return null;
		}

		private bool IsTrusted(X509Certificate potentialTrusted)
		{
			return TrustAnchors.Contains(potentialTrusted);
		}

		private bool IsParent(X509Certificate child, X509Certificate parent)
		{
			if (child.IssuerName != parent.SubjectName)
			{
				return false;
			}
			if (parent.Version > 2 && !IsTrusted(parent))
			{
				X509Extension x509Extension = parent.Extensions["2.5.29.19"];
				if (x509Extension != null)
				{
					if (!new BasicConstraintsExtension(x509Extension).CertificateAuthority)
					{
						_status = X509ChainStatusFlags.InvalidBasicConstraints;
					}
				}
				else
				{
					_status = X509ChainStatusFlags.InvalidBasicConstraints;
				}
			}
			if (!child.VerifySignature(parent.RSA))
			{
				_status = X509ChainStatusFlags.NotSignatureValid;
				return false;
			}
			return true;
		}
	}
	[Serializable]
	[Flags]
	public enum X509ChainStatusFlags
	{
		InvalidBasicConstraints = 0x400,
		NoError = 0,
		NotSignatureValid = 8,
		NotTimeNested = 2,
		NotTimeValid = 1,
		PartialChain = 0x10000,
		UntrustedRoot = 0x20
	}
	public class X509Extension
	{
		protected string extnOid;

		protected bool extnCritical;

		protected ASN1 extnValue;

		public ASN1 ASN1
		{
			get
			{
				ASN1 aSN = new ASN1(48);
				aSN.Add(ASN1Convert.FromOid(extnOid));
				if (extnCritical)
				{
					aSN.Add(new ASN1(1, new byte[1] { 255 }));
				}
				Encode();
				aSN.Add(extnValue);
				return aSN;
			}
		}

		public string Oid => extnOid;

		public bool Critical
		{
			get
			{
				return extnCritical;
			}
			set
			{
				extnCritical = value;
			}
		}

		public virtual string Name => extnOid;

		public ASN1 Value
		{
			get
			{
				if (extnValue == null)
				{
					Encode();
				}
				return extnValue;
			}
		}

		protected X509Extension()
		{
			extnCritical = false;
		}

		public X509Extension(ASN1 asn1)
		{
			if (asn1.Tag != 48 || asn1.Count < 2)
			{
				throw new ArgumentException(global::Locale.GetText("Invalid X.509 extension."));
			}
			if (asn1[0].Tag != 6)
			{
				throw new ArgumentException(global::Locale.GetText("Invalid X.509 extension."));
			}
			extnOid = ASN1Convert.ToOid(asn1[0]);
			extnCritical = asn1[1].Tag == 1 && asn1[1].Value[0] == byte.MaxValue;
			extnValue = asn1[asn1.Count - 1];
			if (extnValue.Tag == 4 && extnValue.Length > 0 && extnValue.Count == 0)
			{
				try
				{
					ASN1 asn2 = new ASN1(extnValue.Value);
					extnValue.Value = null;
					extnValue.Add(asn2);
				}
				catch
				{
				}
			}
			Decode();
		}

		public X509Extension(X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			if (extension.Value == null || extension.Value.Tag != 4 || extension.Value.Count != 1)
			{
				throw new ArgumentException(global::Locale.GetText("Invalid X.509 extension."));
			}
			extnOid = extension.Oid;
			extnCritical = extension.Critical;
			extnValue = extension.Value;
			Decode();
		}

		protected virtual void Decode()
		{
		}

		protected virtual void Encode()
		{
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (!(obj is X509Extension x509Extension))
			{
				return false;
			}
			if (extnCritical != x509Extension.extnCritical)
			{
				return false;
			}
			if (extnOid != x509Extension.extnOid)
			{
				return false;
			}
			if (extnValue.Length != x509Extension.extnValue.Length)
			{
				return false;
			}
			for (int i = 0; i < extnValue.Length; i++)
			{
				if (extnValue[i] != x509Extension.extnValue[i])
				{
					return false;
				}
			}
			return true;
		}

		public byte[] GetBytes()
		{
			return ASN1.GetBytes();
		}

		public override int GetHashCode()
		{
			return extnOid.GetHashCode();
		}

		private void WriteLine(StringBuilder sb, int n, int pos)
		{
			byte[] value = extnValue.Value;
			int num = pos;
			for (int i = 0; i < 8; i++)
			{
				if (i < n)
				{
					sb.Append(value[num++].ToString("X2", CultureInfo.InvariantCulture));
					sb.Append(" ");
				}
				else
				{
					sb.Append("   ");
				}
			}
			sb.Append("  ");
			num = pos;
			for (int j = 0; j < n; j++)
			{
				byte b = value[num++];
				if (b < 32)
				{
					sb.Append(".");
				}
				else
				{
					sb.Append(Convert.ToChar(b));
				}
			}
			sb.Append(Environment.NewLine);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = extnValue.Length >> 3;
			int n = extnValue.Length - (num << 3);
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				WriteLine(stringBuilder, 8, num2);
				num2 += 8;
			}
			WriteLine(stringBuilder, n, num2);
			return stringBuilder.ToString();
		}
	}
	public sealed class X509ExtensionCollection : CollectionBase, IEnumerable
	{
		private bool readOnly;

		public X509Extension this[int index] => (X509Extension)base.InnerList[index];

		public X509Extension this[string oid]
		{
			get
			{
				int num = IndexOf(oid);
				if (num == -1)
				{
					return null;
				}
				return (X509Extension)base.InnerList[num];
			}
		}

		public X509ExtensionCollection()
		{
		}

		public X509ExtensionCollection(ASN1 asn1)
			: this()
		{
			readOnly = true;
			if (asn1 != null)
			{
				if (asn1.Tag != 48)
				{
					throw new Exception("Invalid extensions format");
				}
				for (int i = 0; i < asn1.Count; i++)
				{
					X509Extension value = new X509Extension(asn1[i]);
					base.InnerList.Add(value);
				}
			}
		}

		public int Add(X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			if (readOnly)
			{
				throw new NotSupportedException("Extensions are read only");
			}
			return base.InnerList.Add(extension);
		}

		public void AddRange(X509Extension[] extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			if (readOnly)
			{
				throw new NotSupportedException("Extensions are read only");
			}
			for (int i = 0; i < extension.Length; i++)
			{
				base.InnerList.Add(extension[i]);
			}
		}

		public void AddRange(X509ExtensionCollection collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (readOnly)
			{
				throw new NotSupportedException("Extensions are read only");
			}
			for (int i = 0; i < collection.InnerList.Count; i++)
			{
				base.InnerList.Add(collection[i]);
			}
		}

		public bool Contains(X509Extension extension)
		{
			return IndexOf(extension) != -1;
		}

		public bool Contains(string oid)
		{
			return IndexOf(oid) != -1;
		}

		public void CopyTo(X509Extension[] extensions, int index)
		{
			if (extensions == null)
			{
				throw new ArgumentNullException("extensions");
			}
			base.InnerList.CopyTo(extensions, index);
		}

		public int IndexOf(X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			for (int i = 0; i < base.InnerList.Count; i++)
			{
				if (((X509Extension)base.InnerList[i]).Equals(extension))
				{
					return i;
				}
			}
			return -1;
		}

		public int IndexOf(string oid)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			for (int i = 0; i < base.InnerList.Count; i++)
			{
				if (((X509Extension)base.InnerList[i]).Oid == oid)
				{
					return i;
				}
			}
			return -1;
		}

		public void Insert(int index, X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			base.InnerList.Insert(index, extension);
		}

		public void Remove(X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			base.InnerList.Remove(extension);
		}

		public void Remove(string oid)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			int num = IndexOf(oid);
			if (num != -1)
			{
				base.InnerList.RemoveAt(num);
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return base.InnerList.GetEnumerator();
		}

		public byte[] GetBytes()
		{
			if (base.InnerList.Count < 1)
			{
				return null;
			}
			ASN1 aSN = new ASN1(48);
			for (int i = 0; i < base.InnerList.Count; i++)
			{
				X509Extension x509Extension = (X509Extension)base.InnerList[i];
				aSN.Add(x509Extension.ASN1);
			}
			return aSN.GetBytes();
		}
	}
	public class X509Store
	{
		private string _storePath;

		private X509CertificateCollection _certificates;

		private ArrayList _crls;

		private bool _crl;

		private bool _newFormat;

		private string _name;

		public X509CertificateCollection Certificates
		{
			get
			{
				if (_certificates == null)
				{
					_certificates = BuildCertificatesCollection(_storePath);
				}
				return _certificates;
			}
		}

		public ArrayList Crls
		{
			get
			{
				if (!_crl)
				{
					_crls = new ArrayList();
				}
				if (_crls == null)
				{
					_crls = BuildCrlsCollection(_storePath);
				}
				return _crls;
			}
		}

		public string Name
		{
			get
			{
				if (_name == null)
				{
					int num = _storePath.LastIndexOf(Path.DirectorySeparatorChar);
					_name = _storePath.Substring(num + 1);
				}
				return _name;
			}
		}

		internal X509Store(string path, bool crl, bool newFormat)
		{
			_storePath = path;
			_crl = crl;
			_newFormat = newFormat;
		}

		public void Clear()
		{
			ClearCertificates();
			ClearCrls();
		}

		private void ClearCertificates()
		{
			if (_certificates != null)
			{
				_certificates.Clear();
			}
			_certificates = null;
		}

		private void ClearCrls()
		{
			if (_crls != null)
			{
				_crls.Clear();
			}
			_crls = null;
		}

		public void Import(X509Certificate certificate)
		{
			CheckStore(_storePath, throwException: true);
			if (_newFormat)
			{
				ImportNewFormat(certificate);
				return;
			}
			string text = Path.Combine(_storePath, GetUniqueName(certificate));
			if (!File.Exists(text))
			{
				text = Path.Combine(_storePath, GetUniqueNameWithSerial(certificate));
				if (!File.Exists(text))
				{
					using (FileStream fileStream = File.Create(text))
					{
						byte[] rawData = certificate.RawData;
						fileStream.Write(rawData, 0, rawData.Length);
						fileStream.Close();
					}
					ClearCertificates();
				}
			}
			else
			{
				string path = Path.Combine(_storePath, GetUniqueNameWithSerial(certificate));
				if (GetUniqueNameWithSerial(LoadCertificate(text)) != GetUniqueNameWithSerial(certificate))
				{
					using (FileStream fileStream2 = File.Create(path))
					{
						byte[] rawData2 = certificate.RawData;
						fileStream2.Write(rawData2, 0, rawData2.Length);
						fileStream2.Close();
					}
					ClearCertificates();
				}
			}
			CspParameters cspParameters = new CspParameters();
			cspParameters.KeyContainerName = CryptoConvert.ToHex(certificate.Hash);
			if (_storePath.StartsWith(X509StoreManager.LocalMachinePath) || _storePath.StartsWith(X509StoreManager.NewLocalMachinePath))
			{
				cspParameters.Flags = CspProviderFlags.UseMachineKeyStore;
			}
			ImportPrivateKey(certificate, cspParameters);
		}

		public void Import(X509Crl crl)
		{
			CheckStore(_storePath, throwException: true);
			if (_newFormat)
			{
				throw new NotSupportedException();
			}
			string path = Path.Combine(_storePath, GetUniqueName(crl));
			if (!File.Exists(path))
			{
				using (FileStream fileStream = File.Create(path))
				{
					byte[] rawData = crl.RawData;
					fileStream.Write(rawData, 0, rawData.Length);
				}
				ClearCrls();
			}
		}

		public void Remove(X509Certificate certificate)
		{
			if (_newFormat)
			{
				RemoveNewFormat(certificate);
				return;
			}
			string path = Path.Combine(_storePath, GetUniqueNameWithSerial(certificate));
			if (File.Exists(path))
			{
				File.Delete(path);
				ClearCertificates();
				return;
			}
			path = Path.Combine(_storePath, GetUniqueName(certificate));
			if (File.Exists(path))
			{
				File.Delete(path);
				ClearCertificates();
			}
		}

		public void Remove(X509Crl crl)
		{
			if (_newFormat)
			{
				throw new NotSupportedException();
			}
			string path = Path.Combine(_storePath, GetUniqueName(crl));
			if (File.Exists(path))
			{
				File.Delete(path);
				ClearCrls();
			}
		}

		private void ImportNewFormat(X509Certificate certificate)
		{
			using System.Security.Cryptography.X509Certificates.X509Certificate certificate2 = new System.Security.Cryptography.X509Certificates.X509Certificate(certificate.RawData);
			long subjectNameHash = X509Helper2.GetSubjectNameHash(certificate2);
			string path = Path.Combine(_storePath, $"{subjectNameHash:x8}.0");
			if (!File.Exists(path))
			{
				using (FileStream stream = File.Create(path))
				{
					X509Helper2.ExportAsPEM(certificate2, stream, includeHumanReadableForm: true);
				}
				ClearCertificates();
			}
		}

		private void RemoveNewFormat(X509Certificate certificate)
		{
			using System.Security.Cryptography.X509Certificates.X509Certificate certificate2 = new System.Security.Cryptography.X509Certificates.X509Certificate(certificate.RawData);
			long subjectNameHash = X509Helper2.GetSubjectNameHash(certificate2);
			string path = Path.Combine(_storePath, $"{subjectNameHash:x8}.0");
			if (File.Exists(path))
			{
				File.Delete(path);
				ClearCertificates();
			}
		}

		private string GetUniqueNameWithSerial(X509Certificate certificate)
		{
			return GetUniqueName(certificate, certificate.SerialNumber);
		}

		private string GetUniqueName(X509Certificate certificate, byte[] serial = null)
		{
			byte[] array = GetUniqueName(certificate.Extensions, serial);
			string method;
			if (array == null)
			{
				method = "tbp";
				array = certificate.Hash;
			}
			else
			{
				method = "ski";
			}
			return GetUniqueName(method, array, ".cer");
		}

		private string GetUniqueName(X509Crl crl)
		{
			byte[] array = GetUniqueName(crl.Extensions);
			string method;
			if (array == null)
			{
				method = "tbp";
				array = crl.Hash;
			}
			else
			{
				method = "ski";
			}
			return GetUniqueName(method, array, ".crl");
		}

		private byte[] GetUniqueName(X509ExtensionCollection extensions, byte[] serial = null)
		{
			X509Extension x509Extension = extensions["2.5.29.14"];
			if (x509Extension == null)
			{
				return null;
			}
			SubjectKeyIdentifierExtension subjectKeyIdentifierExtension = new SubjectKeyIdentifierExtension(x509Extension);
			if (serial == null)
			{
				return subjectKeyIdentifierExtension.Identifier;
			}
			byte[] array = new byte[subjectKeyIdentifierExtension.Identifier.Length + serial.Length];
			Buffer.BlockCopy(subjectKeyIdentifierExtension.Identifier, 0, array, 0, subjectKeyIdentifierExtension.Identifier.Length);
			Buffer.BlockCopy(serial, 0, array, subjectKeyIdentifierExtension.Identifier.Length, serial.Length);
			return array;
		}

		private string GetUniqueName(string method, byte[] name, string fileExtension)
		{
			StringBuilder stringBuilder = new StringBuilder(method);
			stringBuilder.Append("-");
			foreach (byte b in name)
			{
				stringBuilder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
			}
			stringBuilder.Append(fileExtension);
			return stringBuilder.ToString();
		}

		private byte[] Load(string filename)
		{
			byte[] array = null;
			using FileStream fileStream = File.OpenRead(filename);
			array = new byte[fileStream.Length];
			fileStream.Read(array, 0, array.Length);
			fileStream.Close();
			return array;
		}

		private X509Certificate LoadCertificate(string filename)
		{
			X509Certificate x509Certificate = new X509Certificate(Load(filename));
			CspParameters cspParameters = new CspParameters();
			cspParameters.KeyContainerName = CryptoConvert.ToHex(x509Certificate.Hash);
			if (_storePath.StartsWith(X509StoreManager.LocalMachinePath) || _storePath.StartsWith(X509StoreManager.NewLocalMachinePath))
			{
				cspParameters.Flags = CspProviderFlags.UseMachineKeyStore;
			}
			KeyPairPersistence keyPairPersistence = new KeyPairPersistence(cspParameters);
			try
			{
				if (!keyPairPersistence.Load())
				{
					return x509Certificate;
				}
			}
			catch
			{
				return x509Certificate;
			}
			if (x509Certificate.RSA != null)
			{
				x509Certificate.RSA = new RSACryptoServiceProvider(cspParameters);
			}
			else if (x509Certificate.DSA != null)
			{
				x509Certificate.DSA = new DSACryptoServiceProvider(cspParameters);
			}
			return x509Certificate;
		}

		private X509Crl LoadCrl(string filename)
		{
			return new X509Crl(Load(filename));
		}

		private bool CheckStore(string path, bool throwException)
		{
			try
			{
				if (Directory.Exists(path))
				{
					return true;
				}
				Directory.CreateDirectory(path);
				return Directory.Exists(path);
			}
			catch
			{
				if (throwException)
				{
					throw;
				}
				return false;
			}
		}

		private X509CertificateCollection BuildCertificatesCollection(string storeName)
		{
			X509CertificateCollection x509CertificateCollection = new X509CertificateCollection();
			string path = Path.Combine(_storePath, storeName);
			if (!CheckStore(path, throwException: false))
			{
				return x509CertificateCollection;
			}
			string[] files = Directory.GetFiles(path, _newFormat ? "*.0" : "*.cer");
			if (files != null && files.Length != 0)
			{
				string[] array = files;
				foreach (string filename in array)
				{
					try
					{
						X509Certificate value = LoadCertificate(filename);
						x509CertificateCollection.Add(value);
					}
					catch
					{
					}
				}
			}
			return x509CertificateCollection;
		}

		private ArrayList BuildCrlsCollection(string storeName)
		{
			ArrayList arrayList = new ArrayList();
			string path = Path.Combine(_storePath, storeName);
			if (!CheckStore(path, throwException: false))
			{
				return arrayList;
			}
			string[] files = Directory.GetFiles(path, "*.crl");
			if (files != null && files.Length != 0)
			{
				string[] array = files;
				foreach (string filename in array)
				{
					try
					{
						X509Crl value = LoadCrl(filename);
						arrayList.Add(value);
					}
					catch
					{
					}
				}
			}
			return arrayList;
		}

		private void ImportPrivateKey(X509Certificate certificate, CspParameters cspParams)
		{
			if (certificate.RSA is RSACryptoServiceProvider rSACryptoServiceProvider)
			{
				if (!rSACryptoServiceProvider.PublicOnly)
				{
					RSACryptoServiceProvider rSACryptoServiceProvider2 = new RSACryptoServiceProvider(cspParams);
					rSACryptoServiceProvider2.ImportParameters(rSACryptoServiceProvider.ExportParameters(includePrivateParameters: true));
					rSACryptoServiceProvider2.PersistKeyInCsp = true;
				}
			}
			else if (certificate.RSA is RSAManaged rSAManaged)
			{
				if (!rSAManaged.PublicOnly)
				{
					RSACryptoServiceProvider rSACryptoServiceProvider3 = new RSACryptoServiceProvider(cspParams);
					rSACryptoServiceProvider3.ImportParameters(rSAManaged.ExportParameters(includePrivateParameters: true));
					rSACryptoServiceProvider3.PersistKeyInCsp = true;
				}
			}
			else if (certificate.DSA is DSACryptoServiceProvider { PublicOnly: false } dSACryptoServiceProvider)
			{
				DSACryptoServiceProvider dSACryptoServiceProvider2 = new DSACryptoServiceProvider(cspParams);
				dSACryptoServiceProvider2.ImportParameters(dSACryptoServiceProvider.ExportParameters(includePrivateParameters: true));
				dSACryptoServiceProvider2.PersistKeyInCsp = true;
			}
		}
	}
	public sealed class X509StoreManager
	{
		private static string _userPath;

		private static string _localMachinePath;

		private static string _newUserPath;

		private static string _newLocalMachinePath;

		private static X509Stores _userStore;

		private static X509Stores _machineStore;

		private static X509Stores _newUserStore;

		private static X509Stores _newMachineStore;

		internal static string CurrentUserPath
		{
			get
			{
				if (_userPath == null)
				{
					_userPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".mono");
					_userPath = Path.Combine(_userPath, "certs");
				}
				return _userPath;
			}
		}

		internal static string LocalMachinePath
		{
			get
			{
				if (_localMachinePath == null)
				{
					_localMachinePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), ".mono");
					_localMachinePath = Path.Combine(_localMachinePath, "certs");
				}
				return _localMachinePath;
			}
		}

		internal static string NewCurrentUserPath
		{
			get
			{
				if (_newUserPath == null)
				{
					_newUserPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".mono");
					_newUserPath = Path.Combine(_newUserPath, "new-certs");
				}
				return _newUserPath;
			}
		}

		internal static string NewLocalMachinePath
		{
			get
			{
				if (_newLocalMachinePath == null)
				{
					_newLocalMachinePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), ".mono");
					_newLocalMachinePath = Path.Combine(_newLocalMachinePath, "new-certs");
				}
				return _newLocalMachinePath;
			}
		}

		public static X509Stores CurrentUser
		{
			get
			{
				if (_userStore == null)
				{
					_userStore = new X509Stores(CurrentUserPath, newFormat: false);
				}
				return _userStore;
			}
		}

		public static X509Stores LocalMachine
		{
			get
			{
				if (_machineStore == null)
				{
					_machineStore = new X509Stores(LocalMachinePath, newFormat: false);
				}
				return _machineStore;
			}
		}

		public static X509Stores NewCurrentUser
		{
			get
			{
				if (_newUserStore == null)
				{
					_newUserStore = new X509Stores(NewCurrentUserPath, newFormat: true);
				}
				return _newUserStore;
			}
		}

		public static X509Stores NewLocalMachine
		{
			get
			{
				if (_newMachineStore == null)
				{
					_newMachineStore = new X509Stores(NewLocalMachinePath, newFormat: true);
				}
				return _newMachineStore;
			}
		}

		public static X509CertificateCollection IntermediateCACertificates
		{
			get
			{
				X509CertificateCollection x509CertificateCollection = new X509CertificateCollection();
				x509CertificateCollection.AddRange(CurrentUser.IntermediateCA.Certificates);
				x509CertificateCollection.AddRange(LocalMachine.IntermediateCA.Certificates);
				return x509CertificateCollection;
			}
		}

		public static ArrayList IntermediateCACrls
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				arrayList.AddRange(CurrentUser.IntermediateCA.Crls);
				arrayList.AddRange(LocalMachine.IntermediateCA.Crls);
				return arrayList;
			}
		}

		public static X509CertificateCollection TrustedRootCertificates
		{
			get
			{
				X509CertificateCollection x509CertificateCollection = new X509CertificateCollection();
				x509CertificateCollection.AddRange(CurrentUser.TrustedRoot.Certificates);
				x509CertificateCollection.AddRange(LocalMachine.TrustedRoot.Certificates);
				return x509CertificateCollection;
			}
		}

		public static ArrayList TrustedRootCACrls
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				arrayList.AddRange(CurrentUser.TrustedRoot.Crls);
				arrayList.AddRange(LocalMachine.TrustedRoot.Crls);
				return arrayList;
			}
		}

		public static X509CertificateCollection UntrustedCertificates
		{
			get
			{
				X509CertificateCollection x509CertificateCollection = new X509CertificateCollection();
				x509CertificateCollection.AddRange(CurrentUser.Untrusted.Certificates);
				x509CertificateCollection.AddRange(LocalMachine.Untrusted.Certificates);
				return x509CertificateCollection;
			}
		}

		private X509StoreManager()
		{
		}
	}
	public class X509Stores
	{
		public class Names
		{
			public const string Personal = "My";

			public const string OtherPeople = "AddressBook";

			public const string IntermediateCA = "CA";

			public const string TrustedRoot = "Trust";

			public const string Untrusted = "Disallowed";
		}

		private string _storePath;

		private bool _newFormat;

		private X509Store _personal;

		private X509Store _other;

		private X509Store _intermediate;

		private X509Store _trusted;

		private X509Store _untrusted;

		public X509Store Personal
		{
			get
			{
				if (_personal == null)
				{
					string path = Path.Combine(_storePath, "My");
					_personal = new X509Store(path, crl: false, newFormat: false);
				}
				return _personal;
			}
		}

		public X509Store OtherPeople
		{
			get
			{
				if (_other == null)
				{
					string path = Path.Combine(_storePath, "AddressBook");
					_other = new X509Store(path, crl: false, newFormat: false);
				}
				return _other;
			}
		}

		public X509Store IntermediateCA
		{
			get
			{
				if (_intermediate == null)
				{
					string path = Path.Combine(_storePath, "CA");
					_intermediate = new X509Store(path, crl: true, _newFormat);
				}
				return _intermediate;
			}
		}

		public X509Store TrustedRoot
		{
			get
			{
				if (_trusted == null)
				{
					string path = Path.Combine(_storePath, "Trust");
					_trusted = new X509Store(path, crl: true, _newFormat);
				}
				return _trusted;
			}
		}

		public X509Store Untrusted
		{
			get
			{
				if (_untrusted == null)
				{
					string path = Path.Combine(_storePath, "Disallowed");
					_untrusted = new X509Store(path, crl: false, _newFormat);
				}
				return _untrusted;
			}
		}

		internal X509Stores(string path, bool newFormat)
		{
			_storePath = path;
			_newFormat = newFormat;
		}

		public void Clear()
		{
			if (_personal != null)
			{
				_personal.Clear();
			}
			_personal = null;
			if (_other != null)
			{
				_other.Clear();
			}
			_other = null;
			if (_intermediate != null)
			{
				_intermediate.Clear();
			}
			_intermediate = null;
			if (_trusted != null)
			{
				_trusted.Clear();
			}
			_trusted = null;
			if (_untrusted != null)
			{
				_untrusted.Clear();
			}
			_untrusted = null;
		}

		public X509Store Open(string storeName, bool create)
		{
			if (storeName == null)
			{
				throw new ArgumentNullException("storeName");
			}
			string path = Path.Combine(_storePath, storeName);
			if (!create && !Directory.Exists(path))
			{
				return null;
			}
			return new X509Store(path, crl: true, newFormat: false);
		}
	}
	public class X520
	{
		public abstract class AttributeTypeAndValue
		{
			private string oid;

			private string attrValue;

			private int upperBound;

			private byte encoding;

			public string Value
			{
				get
				{
					return attrValue;
				}
				set
				{
					if (attrValue != null && attrValue.Length > upperBound)
					{
						throw new FormatException(string.Format(global::Locale.GetText("Value length bigger than upperbound ({0})."), upperBound));
					}
					attrValue = value;
				}
			}

			public ASN1 ASN1 => GetASN1();

			protected AttributeTypeAndValue(string oid, int upperBound)
			{
				this.oid = oid;
				this.upperBound = upperBound;
				encoding = byte.MaxValue;
			}

			protected AttributeTypeAndValue(string oid, int upperBound, byte encoding)
			{
				this.oid = oid;
				this.upperBound = upperBound;
				this.encoding = encoding;
			}

			internal ASN1 GetASN1(byte encoding)
			{
				byte b = encoding;
				if (b == byte.MaxValue)
				{
					b = SelectBestEncoding();
				}
				ASN1 aSN = new ASN1(48);
				aSN.Add(ASN1Convert.FromOid(oid));
				switch (b)
				{
				case 19:
					aSN.Add(new ASN1(19, Encoding.ASCII.GetBytes(attrValue)));
					break;
				case 22:
					aSN.Add(new ASN1(22, Encoding.ASCII.GetBytes(attrValue)));
					break;
				case 30:
					aSN.Add(new ASN1(30, Encoding.BigEndianUnicode.GetBytes(attrValue)));
					break;
				}
				return aSN;
			}

			internal ASN1 GetASN1()
			{
				return GetASN1(encoding);
			}

			public byte[] GetBytes(byte encoding)
			{
				return GetASN1(encoding).GetBytes();
			}

			public byte[] GetBytes()
			{
				return GetASN1().GetBytes();
			}

			private byte SelectBestEncoding()
			{
				string text = attrValue;
				foreach (char c in text)
				{
					if (c == '@' || c == '_')
					{
						return 30;
					}
					if (c > '\u007f')
					{
						return 30;
					}
				}
				return 19;
			}
		}

		public class Name : AttributeTypeAndValue
		{
			public Name()
				: base("2.5.4.41", 32768)
			{
			}
		}

		public class CommonName : AttributeTypeAndValue
		{
			public CommonName()
				: base("2.5.4.3", 64)
			{
			}
		}

		public class SerialNumber : AttributeTypeAndValue
		{
			public SerialNumber()
				: base("2.5.4.5", 64, 19)
			{
			}
		}

		public class LocalityName : AttributeTypeAndValue
		{
			public LocalityName()
				: base("2.5.4.7", 128)
			{
			}
		}

		public class StateOrProvinceName : AttributeTypeAndValue
		{
			public StateOrProvinceName()
				: base("2.5.4.8", 128)
			{
			}
		}

		public class OrganizationName : AttributeTypeAndValue
		{
			public OrganizationName()
				: base("2.5.4.10", 64)
			{
			}
		}

		public class OrganizationalUnitName : AttributeTypeAndValue
		{
			public OrganizationalUnitName()
				: base("2.5.4.11", 64)
			{
			}
		}

		public class EmailAddress : AttributeTypeAndValue
		{
			public EmailAddress()
				: base("1.2.840.113549.1.9.1", 128, 22)
			{
			}
		}

		public class DomainComponent : AttributeTypeAndValue
		{
			public DomainComponent()
				: base("0.9.2342.19200300.100.1.25", int.MaxValue, 22)
			{
			}
		}

		public class UserId : AttributeTypeAndValue
		{
			public UserId()
				: base("0.9.2342.19200300.100.1.1", 256)
			{
			}
		}

		public class Oid : AttributeTypeAndValue
		{
			public Oid(string oid)
				: base(oid, int.MaxValue)
			{
			}
		}

		public class Title : AttributeTypeAndValue
		{
			public Title()
				: base("2.5.4.12", 64)
			{
			}
		}

		public class CountryName : AttributeTypeAndValue
		{
			public CountryName()
				: base("2.5.4.6", 2, 19)
			{
			}
		}

		public class DnQualifier : AttributeTypeAndValue
		{
			public DnQualifier()
				: base("2.5.4.46", 2, 19)
			{
			}
		}

		public class Surname : AttributeTypeAndValue
		{
			public Surname()
				: base("2.5.4.4", 32768)
			{
			}
		}

		public class GivenName : AttributeTypeAndValue
		{
			public GivenName()
				: base("2.5.4.42", 16)
			{
			}
		}

		public class Initial : AttributeTypeAndValue
		{
			public Initial()
				: base("2.5.4.43", 5)
			{
			}
		}
	}
}
namespace Mono.Security.X509.Extensions
{
	public class AuthorityKeyIdentifierExtension : X509Extension
	{
		private byte[] aki;

		public override string Name => "Authority Key Identifier";

		public byte[] Identifier
		{
			get
			{
				if (aki == null)
				{
					return null;
				}
				return (byte[])aki.Clone();
			}
			set
			{
				aki = value;
			}
		}

		public AuthorityKeyIdentifierExtension()
		{
			extnOid = "2.5.29.35";
		}

		public AuthorityKeyIdentifierExtension(ASN1 asn1)
			: base(asn1)
		{
		}

		public AuthorityKeyIdentifierExtension(X509Extension extension)
			: base(extension)
		{
		}

		protected override void Decode()
		{
			ASN1 aSN = new ASN1(extnValue.Value);
			if (aSN.Tag != 48)
			{
				throw new ArgumentException("Invalid AuthorityKeyIdentifier extension");
			}
			for (int i = 0; i < aSN.Count; i++)
			{
				ASN1 aSN2 = aSN[i];
				if (aSN2.Tag == 128)
				{
					aki = aSN2.Value;
				}
			}
		}

		protected override void Encode()
		{
			ASN1 aSN = new ASN1(48);
			if (aki == null)
			{
				throw new InvalidOperationException("Invalid AuthorityKeyIdentifier extension");
			}
			aSN.Add(new ASN1(128, aki));
			extnValue = new ASN1(4);
			extnValue.Add(aSN);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (aki != null)
			{
				int i = 0;
				stringBuilder.Append("KeyID=");
				for (; i < aki.Length; i++)
				{
					stringBuilder.Append(aki[i].ToString("X2", CultureInfo.InvariantCulture));
					if (i % 2 == 1)
					{
						stringBuilder.Append(" ");
					}
				}
			}
			return stringBuilder.ToString();
		}
	}
	public class BasicConstraintsExtension : X509Extension
	{
		public const int NoPathLengthConstraint = -1;

		private bool cA;

		private int pathLenConstraint;

		public bool CertificateAuthority
		{
			get
			{
				return cA;
			}
			set
			{
				cA = value;
			}
		}

		public override string Name => "Basic Constraints";

		public int PathLenConstraint
		{
			get
			{
				return pathLenConstraint;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException(global::Locale.GetText("PathLenConstraint must be positive or -1 for none ({0}).", value));
				}
				pathLenConstraint = value;
			}
		}

		public BasicConstraintsExtension()
		{
			extnOid = "2.5.29.19";
			pathLenConstraint = -1;
		}

		public BasicConstraintsExtension(ASN1 asn1)
			: base(asn1)
		{
		}

		public BasicConstraintsExtension(X509Extension extension)
			: base(extension)
		{
		}

		protected override void Decode()
		{
			cA = false;
			pathLenConstraint = -1;
			ASN1 aSN = new ASN1(extnValue.Value);
			if (aSN.Tag != 48)
			{
				throw new ArgumentException("Invalid BasicConstraints extension");
			}
			int num = 0;
			ASN1 aSN2 = aSN[num++];
			if (aSN2 != null && aSN2.Tag == 1)
			{
				cA = aSN2.Value[0] == byte.MaxValue;
				aSN2 = aSN[num++];
			}
			if (aSN2 != null && aSN2.Tag == 2)
			{
				pathLenConstraint = ASN1Convert.ToInt32(aSN2);
			}
		}

		protected override void Encode()
		{
			ASN1 aSN = new ASN1(48);
			if (cA)
			{
				aSN.Add(new ASN1(1, new byte[1] { 255 }));
			}
			if (cA && pathLenConstraint >= 0)
			{
				aSN.Add(ASN1Convert.FromInt32(pathLenConstraint));
			}
			extnValue = new ASN1(4);
			extnValue.Add(aSN);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Subject Type=");
			stringBuilder.Append(cA ? "CA" : "End Entity");
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("Path Length Constraint=");
			if (pathLenConstraint == -1)
			{
				stringBuilder.Append("None");
			}
			else
			{
				stringBuilder.Append(pathLenConstraint.ToString(CultureInfo.InvariantCulture));
			}
			stringBuilder.Append(Environment.NewLine);
			return stringBuilder.ToString();
		}
	}
	public class CRLDistributionPointsExtension : X509Extension
	{
		public class DistributionPoint
		{
			public string Name { get; private set; }

			public ReasonFlags Reasons { get; private set; }

			public string CRLIssuer { get; private set; }

			public DistributionPoint(string dp, ReasonFlags reasons, string issuer)
			{
				Name = dp;
				Reasons = reasons;
				CRLIssuer = issuer;
			}

			public DistributionPoint(ASN1 dp)
			{
				for (int i = 0; i < dp.Count; i++)
				{
					ASN1 aSN = dp[i];
					switch (aSN.Tag)
					{
					case 160:
					{
						for (int j = 0; j < aSN.Count; j++)
						{
							ASN1 aSN2 = aSN[j];
							if (aSN2.Tag == 160)
							{
								Name = new GeneralNames(aSN2).ToString();
							}
						}
						break;
					}
					}
				}
			}
		}

		[Flags]
		public enum ReasonFlags
		{
			Unused = 0,
			KeyCompromise = 1,
			CACompromise = 2,
			AffiliationChanged = 3,
			Superseded = 4,
			CessationOfOperation = 5,
			CertificateHold = 6,
			PrivilegeWithdrawn = 7,
			AACompromise = 8
		}

		private List<DistributionPoint> dps;

		public override string Name => "CRL Distribution Points";

		public IEnumerable<DistributionPoint> DistributionPoints => dps;

		public CRLDistributionPointsExtension()
		{
			extnOid = "2.5.29.31";
			dps = new List<DistributionPoint>();
		}

		public CRLDistributionPointsExtension(ASN1 asn1)
			: base(asn1)
		{
		}

		public CRLDistributionPointsExtension(X509Extension extension)
			: base(extension)
		{
		}

		protected override void Decode()
		{
			dps = new List<DistributionPoint>();
			ASN1 aSN = new ASN1(extnValue.Value);
			if (aSN.Tag != 48)
			{
				throw new ArgumentException("Invalid CRLDistributionPoints extension");
			}
			for (int i = 0; i < aSN.Count; i++)
			{
				dps.Add(new DistributionPoint(aSN[i]));
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 1;
			foreach (DistributionPoint dp in dps)
			{
				stringBuilder.Append("[");
				stringBuilder.Append(num++);
				stringBuilder.Append("]CRL Distribution Point");
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append("\tDistribution Point Name:");
				stringBuilder.Append("\t\tFull Name:");
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append("\t\t\t");
				stringBuilder.Append(dp.Name);
				stringBuilder.Append(Environment.NewLine);
			}
			return stringBuilder.ToString();
		}
	}
	public class CertificatePoliciesExtension : X509Extension
	{
		private Hashtable policies;

		public override string Name => "Certificate Policies";

		public CertificatePoliciesExtension()
		{
			extnOid = "2.5.29.32";
			policies = new Hashtable();
		}

		public CertificatePoliciesExtension(ASN1 asn1)
			: base(asn1)
		{
		}

		public CertificatePoliciesExtension(X509Extension extension)
			: base(extension)
		{
		}

		protected override void Decode()
		{
			policies = new Hashtable();
			ASN1 aSN = new ASN1(extnValue.Value);
			if (aSN.Tag != 48)
			{
				throw new ArgumentException("Invalid CertificatePolicies extension");
			}
			for (int i = 0; i < aSN.Count; i++)
			{
				policies.Add(ASN1Convert.ToOid(aSN[i][0]), null);
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 1;
			foreach (DictionaryEntry policy in policies)
			{
				stringBuilder.Append("[");
				stringBuilder.Append(num++);
				stringBuilder.Append("]Certificate Policy:");
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append("\tPolicyIdentifier=");
				stringBuilder.Append((string)policy.Key);
				stringBuilder.Append(Environment.NewLine);
			}
			return stringBuilder.ToString();
		}
	}
	public class ExtendedKeyUsageExtension : X509Extension
	{
		private ArrayList keyPurpose;

		public ArrayList KeyPurpose => keyPurpose;

		public override string Name => "Extended Key Usage";

		public ExtendedKeyUsageExtension()
		{
			extnOid = "2.5.29.37";
			keyPurpose = new ArrayList();
		}

		public ExtendedKeyUsageExtension(ASN1 asn1)
			: base(asn1)
		{
		}

		public ExtendedKeyUsageExtension(X509Extension extension)
			: base(extension)
		{
		}

		protected override void Decode()
		{
			keyPurpose = new ArrayList();
			ASN1 aSN = new ASN1(extnValue.Value);
			if (aSN.Tag != 48)
			{
				throw new ArgumentException("Invalid ExtendedKeyUsage extension");
			}
			for (int i = 0; i < aSN.Count; i++)
			{
				keyPurpose.Add(ASN1Convert.ToOid(aSN[i]));
			}
		}

		protected override void Encode()
		{
			ASN1 aSN = new ASN1(48);
			foreach (string item in keyPurpose)
			{
				aSN.Add(ASN1Convert.FromOid(item));
			}
			extnValue = new ASN1(4);
			extnValue.Add(aSN);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string item in keyPurpose)
			{
				switch (item)
				{
				case "1.3.6.1.5.5.7.3.1":
					stringBuilder.Append("Server Authentication");
					break;
				case "1.3.6.1.5.5.7.3.2":
					stringBuilder.Append("Client Authentication");
					break;
				case "1.3.6.1.5.5.7.3.3":
					stringBuilder.Append("Code Signing");
					break;
				case "1.3.6.1.5.5.7.3.4":
					stringBuilder.Append("Email Protection");
					break;
				case "1.3.6.1.5.5.7.3.8":
					stringBuilder.Append("Time Stamping");
					break;
				case "1.3.6.1.5.5.7.3.9":
					stringBuilder.Append("OCSP Signing");
					break;
				default:
					stringBuilder.Append("unknown");
					break;
				}
				stringBuilder.AppendFormat(" ({0}){1}", item, Environment.NewLine);
			}
			return stringBuilder.ToString();
		}
	}
	internal class GeneralNames
	{
		private ArrayList rfc822Name;

		private ArrayList dnsName;

		private ArrayList directoryNames;

		private ArrayList uris;

		private ArrayList ipAddr;

		private ASN1 asn;

		public string[] RFC822
		{
			get
			{
				if (rfc822Name == null)
				{
					return new string[0];
				}
				return (string[])rfc822Name.ToArray(typeof(string));
			}
		}

		public string[] DirectoryNames
		{
			get
			{
				if (directoryNames == null)
				{
					return new string[0];
				}
				return (string[])directoryNames.ToArray(typeof(string));
			}
		}

		public string[] DNSNames
		{
			get
			{
				if (dnsName == null)
				{
					return new string[0];
				}
				return (string[])dnsName.ToArray(typeof(string));
			}
		}

		public string[] UniformResourceIdentifiers
		{
			get
			{
				if (uris == null)
				{
					return new string[0];
				}
				return (string[])uris.ToArray(typeof(string));
			}
		}

		public string[] IPAddresses
		{
			get
			{
				if (ipAddr == null)
				{
					return new string[0];
				}
				return (string[])ipAddr.ToArray(typeof(string));
			}
		}

		public GeneralNames()
		{
		}

		public GeneralNames(string[] rfc822s, string[] dnsNames, string[] ipAddresses, string[] uris)
		{
			asn = new ASN1(48);
			if (rfc822s != null)
			{
				rfc822Name = new ArrayList();
				string[] array = rfc822s;
				foreach (string s in array)
				{
					asn.Add(new ASN1(129, Encoding.ASCII.GetBytes(s)));
					rfc822Name.Add(rfc822s);
				}
			}
			if (dnsNames != null)
			{
				dnsName = new ArrayList();
				string[] array = dnsNames;
				foreach (string text in array)
				{
					asn.Add(new ASN1(130, Encoding.ASCII.GetBytes(text)));
					dnsName.Add(text);
				}
			}
			if (ipAddresses != null)
			{
				ipAddr = new ArrayList();
				string[] array = ipAddresses;
				foreach (string text2 in array)
				{
					string[] array2 = text2.Split('.', ':');
					byte[] array3 = new byte[array2.Length];
					for (int j = 0; j < array2.Length; j++)
					{
						array3[j] = byte.Parse(array2[j]);
					}
					asn.Add(new ASN1(135, array3));
					ipAddr.Add(text2);
				}
			}
			if (uris != null)
			{
				this.uris = new ArrayList();
				string[] array = uris;
				foreach (string text3 in array)
				{
					asn.Add(new ASN1(134, Encoding.ASCII.GetBytes(text3)));
					this.uris.Add(text3);
				}
			}
		}

		public GeneralNames(ASN1 sequence)
		{
			for (int i = 0; i < sequence.Count; i++)
			{
				switch (sequence[i].Tag)
				{
				case 129:
					if (rfc822Name == null)
					{
						rfc822Name = new ArrayList();
					}
					rfc822Name.Add(Encoding.ASCII.GetString(sequence[i].Value));
					break;
				case 130:
					if (dnsName == null)
					{
						dnsName = new ArrayList();
					}
					dnsName.Add(Encoding.ASCII.GetString(sequence[i].Value));
					break;
				case 132:
				case 164:
					if (directoryNames == null)
					{
						directoryNames = new ArrayList();
					}
					directoryNames.Add(X501.ToString(sequence[i][0]));
					break;
				case 134:
					if (uris == null)
					{
						uris = new ArrayList();
					}
					uris.Add(Encoding.ASCII.GetString(sequence[i].Value));
					break;
				case 135:
				{
					if (ipAddr == null)
					{
						ipAddr = new ArrayList();
					}
					byte[] value = sequence[i].Value;
					string value2 = ((value.Length == 4) ? "." : ":");
					StringBuilder stringBuilder = new StringBuilder();
					for (int j = 0; j < value.Length; j++)
					{
						stringBuilder.Append(value[j].ToString());
						if (j < value.Length - 1)
						{
							stringBuilder.Append(value2);
						}
					}
					ipAddr.Add(stringBuilder.ToString());
					if (ipAddr == null)
					{
						ipAddr = new ArrayList();
					}
					break;
				}
				}
			}
		}

		public byte[] GetBytes()
		{
			return asn.GetBytes();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (rfc822Name != null)
			{
				foreach (string item in rfc822Name)
				{
					stringBuilder.Append("RFC822 Name=");
					stringBuilder.Append(item);
					stringBuilder.Append(Environment.NewLine);
				}
			}
			if (dnsName != null)
			{
				foreach (string item2 in dnsName)
				{
					stringBuilder.Append("DNS Name=");
					stringBuilder.Append(item2);
					stringBuilder.Append(Environment.NewLine);
				}
			}
			if (directoryNames != null)
			{
				foreach (string directoryName in directoryNames)
				{
					stringBuilder.Append("Directory Address: ");
					stringBuilder.Append(directoryName);
					stringBuilder.Append(Environment.NewLine);
				}
			}
			if (uris != null)
			{
				foreach (string uri in uris)
				{
					stringBuilder.Append("URL=");
					stringBuilder.Append(uri);
					stringBuilder.Append(Environment.NewLine);
				}
			}
			if (ipAddr != null)
			{
				foreach (string item3 in ipAddr)
				{
					stringBuilder.Append("IP Address=");
					stringBuilder.Append(item3);
					stringBuilder.Append(Environment.NewLine);
				}
			}
			return stringBuilder.ToString();
		}
	}
	public class KeyAttributesExtension : X509Extension
	{
		private byte[] keyId;

		private int kubits;

		private DateTime notBefore;

		private DateTime notAfter;

		public byte[] KeyIdentifier
		{
			get
			{
				if (keyId == null)
				{
					return null;
				}
				return (byte[])keyId.Clone();
			}
		}

		public override string Name => "Key Attributes";

		public DateTime NotAfter => notAfter;

		public DateTime NotBefore => notBefore;

		public KeyAttributesExtension()
		{
			extnOid = "2.5.29.2";
		}

		public KeyAttributesExtension(ASN1 asn1)
			: base(asn1)
		{
		}

		public KeyAttributesExtension(X509Extension extension)
			: base(extension)
		{
		}

		protected override void Decode()
		{
			ASN1 aSN = new ASN1(extnValue.Value);
			if (aSN.Tag != 48)
			{
				throw new ArgumentException("Invalid KeyAttributesExtension extension");
			}
			int num = 0;
			if (num < aSN.Count)
			{
				ASN1 aSN2 = aSN[num];
				if (aSN2.Tag == 4)
				{
					num++;
					keyId = aSN2.Value;
				}
			}
			if (num < aSN.Count)
			{
				ASN1 aSN3 = aSN[num];
				if (aSN3.Tag == 3)
				{
					num++;
					int num2 = 1;
					while (num2 < aSN3.Value.Length)
					{
						kubits = (kubits << 8) + aSN3.Value[num2++];
					}
				}
			}
			if (num >= aSN.Count)
			{
				return;
			}
			ASN1 aSN4 = aSN[num];
			if (aSN4.Tag != 48)
			{
				return;
			}
			int num3 = 0;
			if (num3 < aSN4.Count)
			{
				ASN1 aSN5 = aSN4[num3];
				if (aSN5.Tag == 129)
				{
					num3++;
					notBefore = ASN1Convert.ToDateTime(aSN5);
				}
			}
			if (num3 < aSN4.Count)
			{
				ASN1 aSN6 = aSN4[num3];
				if (aSN6.Tag == 130)
				{
					notAfter = ASN1Convert.ToDateTime(aSN6);
				}
			}
		}

		public bool Support(KeyUsages usage)
		{
			int num = Convert.ToInt32(usage, CultureInfo.InvariantCulture);
			return (num & kubits) == num;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (keyId != null)
			{
				stringBuilder.Append("KeyID=");
				for (int i = 0; i < keyId.Length; i++)
				{
					stringBuilder.Append(keyId[i].ToString("X2", CultureInfo.InvariantCulture));
					if (i % 2 == 1)
					{
						stringBuilder.Append(" ");
					}
				}
				stringBuilder.Append(Environment.NewLine);
			}
			if (kubits != 0)
			{
				stringBuilder.Append("Key Usage=");
				if (Support(KeyUsages.digitalSignature))
				{
					stringBuilder.Append("Digital Signature");
				}
				if (Support(KeyUsages.nonRepudiation))
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(" , ");
					}
					stringBuilder.Append("Non-Repudiation");
				}
				if (Support(KeyUsages.keyEncipherment))
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(" , ");
					}
					stringBuilder.Append("Key Encipherment");
				}
				if (Support(KeyUsages.dataEncipherment))
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(" , ");
					}
					stringBuilder.Append("Data Encipherment");
				}
				if (Support(KeyUsages.keyAgreement))
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(" , ");
					}
					stringBuilder.Append("Key Agreement");
				}
				if (Support(KeyUsages.keyCertSign))
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(" , ");
					}
					stringBuilder.Append("Certificate Signing");
				}
				if (Support(KeyUsages.cRLSign))
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(" , ");
					}
					stringBuilder.Append("CRL Signing");
				}
				if (Support(KeyUsages.encipherOnly))
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(" , ");
					}
					stringBuilder.Append("Encipher Only ");
				}
				if (Support(KeyUsages.decipherOnly))
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(" , ");
					}
					stringBuilder.Append("Decipher Only");
				}
				stringBuilder.Append("(");
				stringBuilder.Append(kubits.ToString("X2", CultureInfo.InvariantCulture));
				stringBuilder.Append(")");
				stringBuilder.Append(Environment.NewLine);
			}
			if (notBefore != DateTime.MinValue)
			{
				stringBuilder.Append("Not Before=");
				stringBuilder.Append(notBefore.ToString(CultureInfo.CurrentUICulture));
				stringBuilder.Append(Environment.NewLine);
			}
			if (notAfter != DateTime.MinValue)
			{
				stringBuilder.Append("Not After=");
				stringBuilder.Append(notAfter.ToString(CultureInfo.CurrentUICulture));
				stringBuilder.Append(Environment.NewLine);
			}
			return stringBuilder.ToString();
		}
	}
	[Flags]
	public enum KeyUsages
	{
		digitalSignature = 0x80,
		nonRepudiation = 0x40,
		keyEncipherment = 0x20,
		dataEncipherment = 0x10,
		keyAgreement = 8,
		keyCertSign = 4,
		cRLSign = 2,
		encipherOnly = 1,
		decipherOnly = 0x800,
		none = 0
	}
	public class KeyUsageExtension : X509Extension
	{
		private int kubits;

		public KeyUsages KeyUsage
		{
			get
			{
				return (KeyUsages)kubits;
			}
			set
			{
				kubits = Convert.ToInt32(value, CultureInfo.InvariantCulture);
			}
		}

		public override string Name => "Key Usage";

		public KeyUsageExtension(ASN1 asn1)
			: base(asn1)
		{
		}

		public KeyUsageExtension(X509Extension extension)
			: base(extension)
		{
		}

		public KeyUsageExtension()
		{
			extnOid = "2.5.29.15";
		}

		protected override void Decode()
		{
			ASN1 aSN = new ASN1(extnValue.Value);
			if (aSN.Tag != 3)
			{
				throw new ArgumentException("Invalid KeyUsage extension");
			}
			int num = 1;
			while (num < aSN.Value.Length)
			{
				kubits = (kubits << 8) + aSN.Value[num++];
			}
		}

		protected override void Encode()
		{
			extnValue = new ASN1(4);
			ushort num = (ushort)kubits;
			byte b = 16;
			if (num > 0)
			{
				b = 15;
				while (b > 0 && (num & 0x8000) != 32768)
				{
					num <<= 1;
					b--;
				}
				if (kubits > 255)
				{
					b -= 8;
					extnValue.Add(new ASN1(3, new byte[3]
					{
						b,
						(byte)kubits,
						(byte)(kubits >> 8)
					}));
				}
				else
				{
					extnValue.Add(new ASN1(3, new byte[2]
					{
						b,
						(byte)kubits
					}));
				}
			}
			else
			{
				extnValue.Add(new ASN1(3, new byte[2] { 7, 0 }));
			}
		}

		public bool Support(KeyUsages usage)
		{
			int num = Convert.ToInt32(usage, CultureInfo.InvariantCulture);
			return (num & kubits) == num;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (Support(KeyUsages.digitalSignature))
			{
				stringBuilder.Append("Digital Signature");
			}
			if (Support(KeyUsages.nonRepudiation))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Non-Repudiation");
			}
			if (Support(KeyUsages.keyEncipherment))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Key Encipherment");
			}
			if (Support(KeyUsages.dataEncipherment))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Data Encipherment");
			}
			if (Support(KeyUsages.keyAgreement))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Key Agreement");
			}
			if (Support(KeyUsages.keyCertSign))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Certificate Signing");
			}
			if (Support(KeyUsages.cRLSign))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("CRL Signing");
			}
			if (Support(KeyUsages.encipherOnly))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Encipher Only ");
			}
			if (Support(KeyUsages.decipherOnly))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Decipher Only");
			}
			stringBuilder.Append("(");
			stringBuilder.Append(kubits.ToString("X2", CultureInfo.InvariantCulture));
			stringBuilder.Append(")");
			stringBuilder.Append(Environment.NewLine);
			return stringBuilder.ToString();
		}
	}
	public class NetscapeCertTypeExtension : X509Extension
	{
		[Flags]
		public enum CertTypes
		{
			SslClient = 0x80,
			SslServer = 0x40,
			Smime = 0x20,
			ObjectSigning = 0x10,
			SslCA = 4,
			SmimeCA = 2,
			ObjectSigningCA = 1
		}

		private int ctbits;

		public override string Name => "NetscapeCertType";

		public NetscapeCertTypeExtension()
		{
			extnOid = "2.16.840.1.113730.1.1";
		}

		public NetscapeCertTypeExtension(ASN1 asn1)
			: base(asn1)
		{
		}

		public NetscapeCertTypeExtension(X509Extension extension)
			: base(extension)
		{
		}

		protected override void Decode()
		{
			ASN1 aSN = new ASN1(extnValue.Value);
			if (aSN.Tag != 3)
			{
				throw new ArgumentException("Invalid NetscapeCertType extension");
			}
			int num = 1;
			while (num < aSN.Value.Length)
			{
				ctbits = (ctbits << 8) + aSN.Value[num++];
			}
		}

		public bool Support(CertTypes usage)
		{
			int num = Convert.ToInt32(usage, CultureInfo.InvariantCulture);
			return (num & ctbits) == num;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (Support(CertTypes.SslClient))
			{
				stringBuilder.Append("SSL Client Authentication");
			}
			if (Support(CertTypes.SslServer))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("SSL Server Authentication");
			}
			if (Support(CertTypes.Smime))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("SMIME");
			}
			if (Support(CertTypes.ObjectSigning))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Object Signing");
			}
			if (Support(CertTypes.SslCA))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("SSL CA");
			}
			if (Support(CertTypes.SmimeCA))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("SMIME CA");
			}
			if (Support(CertTypes.ObjectSigningCA))
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(" , ");
				}
				stringBuilder.Append("Object Signing CA");
			}
			stringBuilder.Append("(");
			stringBuilder.Append(ctbits.ToString("X2", CultureInfo.InvariantCulture));
			stringBuilder.Append(")");
			stringBuilder.Append(Environment.NewLine);
			return stringBuilder.ToString();
		}
	}
	public class PrivateKeyUsagePeriodExtension : X509Extension
	{
		private DateTime notBefore;

		private DateTime notAfter;

		public override string Name => "Private Key Usage Period";

		public PrivateKeyUsagePeriodExtension()
		{
			extnOid = "2.5.29.16";
		}

		public PrivateKeyUsagePeriodExtension(ASN1 asn1)
			: base(asn1)
		{
		}

		public PrivateKeyUsagePeriodExtension(X509Extension extension)
			: base(extension)
		{
		}

		protected override void Decode()
		{
			ASN1 aSN = new ASN1(extnValue.Value);
			if (aSN.Tag != 48)
			{
				throw new ArgumentException("Invalid PrivateKeyUsagePeriod extension");
			}
			for (int i = 0; i < aSN.Count; i++)
			{
				switch (aSN[i].Tag)
				{
				case 128:
					notBefore = ASN1Convert.ToDateTime(aSN[i]);
					break;
				case 129:
					notAfter = ASN1Convert.ToDateTime(aSN[i]);
					break;
				default:
					throw new ArgumentException("Invalid PrivateKeyUsagePeriod extension");
				}
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (notBefore != DateTime.MinValue)
			{
				stringBuilder.Append("Not Before: ");
				stringBuilder.Append(notBefore.ToString(CultureInfo.CurrentUICulture));
				stringBuilder.Append(Environment.NewLine);
			}
			if (notAfter != DateTime.MinValue)
			{
				stringBuilder.Append("Not After: ");
				stringBuilder.Append(notAfter.ToString(CultureInfo.CurrentUICulture));
				stringBuilder.Append(Environment.NewLine);
			}
			return stringBuilder.ToString();
		}
	}
	public class SubjectAltNameExtension : X509Extension
	{
		private GeneralNames _names;

		public override string Name => "Subject Alternative Name";

		public string[] RFC822 => _names.RFC822;

		public string[] DNSNames => _names.DNSNames;

		public string[] IPAddresses => _names.IPAddresses;

		public string[] UniformResourceIdentifiers => _names.UniformResourceIdentifiers;

		public SubjectAltNameExtension()
		{
			extnOid = "2.5.29.17";
			_names = new GeneralNames();
		}

		public SubjectAltNameExtension(ASN1 asn1)
			: base(asn1)
		{
		}

		public SubjectAltNameExtension(X509Extension extension)
			: base(extension)
		{
		}

		public SubjectAltNameExtension(string[] rfc822, string[] dnsNames, string[] ipAddresses, string[] uris)
		{
			_names = new GeneralNames(rfc822, dnsNames, ipAddresses, uris);
			extnValue = new ASN1(4, _names.GetBytes());
			extnOid = "2.5.29.17";
		}

		protected override void Decode()
		{
			ASN1 aSN = new ASN1(extnValue.Value);
			if (aSN.Tag != 48)
			{
				throw new ArgumentException("Invalid SubjectAltName extension");
			}
			_names = new GeneralNames(aSN);
		}

		public override string ToString()
		{
			return _names.ToString();
		}
	}
	public class SubjectKeyIdentifierExtension : X509Extension
	{
		private byte[] ski;

		public override string Name => "Subject Key Identifier";

		public byte[] Identifier
		{
			get
			{
				if (ski == null)
				{
					return null;
				}
				return (byte[])ski.Clone();
			}
			set
			{
				ski = value;
			}
		}

		public SubjectKeyIdentifierExtension()
		{
			extnOid = "2.5.29.14";
		}

		public SubjectKeyIdentifierExtension(ASN1 asn1)
			: base(asn1)
		{
		}

		public SubjectKeyIdentifierExtension(X509Extension extension)
			: base(extension)
		{
		}

		protected override void Decode()
		{
			ASN1 aSN = new ASN1(extnValue.Value);
			if (aSN.Tag != 4)
			{
				throw new ArgumentException("Invalid SubjectKeyIdentifier extension");
			}
			ski = aSN.Value;
		}

		protected override void Encode()
		{
			if (ski == null)
			{
				throw new InvalidOperationException("Invalid SubjectKeyIdentifier extension");
			}
			ASN1 asn = new ASN1(4, ski);
			extnValue = new ASN1(4);
			extnValue.Add(asn);
		}

		public override string ToString()
		{
			if (ski == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < ski.Length; i++)
			{
				stringBuilder.Append(ski[i].ToString("X2", CultureInfo.InvariantCulture));
				if (i % 2 == 1)
				{
					stringBuilder.Append(" ");
				}
			}
			return stringBuilder.ToString();
		}
	}
}
namespace Mono.Security.Protocol.Ntlm
{
	[Obsolete("Use of this API is highly discouraged, it selects legacy-mode LM/NTLM authentication, which sends your password in very weak encryption over the wire even if the server supports the more secure NTLMv2 / NTLMv2 Session. You need to use the new `Type3Message (Type2Message)' constructor to use the more secure NTLMv2 / NTLMv2 Session authentication modes. These require the Type 2 message from the server to compute the response.")]
	public class ChallengeResponse : IDisposable
	{
		private static byte[] magic = new byte[8] { 75, 71, 83, 33, 64, 35, 36, 37 };

		private static byte[] nullEncMagic = new byte[8] { 170, 211, 180, 53, 181, 20, 4, 238 };

		private bool _disposed;

		private byte[] _challenge;

		private byte[] _lmpwd;

		private byte[] _ntpwd;

		public string Password
		{
			get
			{
				return null;
			}
			set
			{
				if (_disposed)
				{
					throw new ObjectDisposedException("too late");
				}
				DES dES = DES.Create();
				dES.Mode = CipherMode.ECB;
				if (value == null || value.Length < 1)
				{
					Buffer.BlockCopy(nullEncMagic, 0, _lmpwd, 0, 8);
				}
				else
				{
					dES.Key = PasswordToKey(value, 0);
					dES.CreateEncryptor().TransformBlock(magic, 0, 8, _lmpwd, 0);
				}
				if (value == null || value.Length < 8)
				{
					Buffer.BlockCopy(nullEncMagic, 0, _lmpwd, 8, 8);
				}
				else
				{
					dES.Key = PasswordToKey(value, 7);
					dES.CreateEncryptor().TransformBlock(magic, 0, 8, _lmpwd, 8);
				}
				MD4 mD = MD4.Create();
				byte[] array = ((value == null) ? new byte[0] : Encoding.Unicode.GetBytes(value));
				byte[] array2 = mD.ComputeHash(array);
				Buffer.BlockCopy(array2, 0, _ntpwd, 0, 16);
				Array.Clear(array, 0, array.Length);
				Array.Clear(array2, 0, array2.Length);
				dES.Clear();
			}
		}

		public byte[] Challenge
		{
			get
			{
				return null;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Challenge");
				}
				if (_disposed)
				{
					throw new ObjectDisposedException("too late");
				}
				_challenge = (byte[])value.Clone();
			}
		}

		public byte[] LM
		{
			get
			{
				if (_disposed)
				{
					throw new ObjectDisposedException("too late");
				}
				return GetResponse(_lmpwd);
			}
		}

		public byte[] NT
		{
			get
			{
				if (_disposed)
				{
					throw new ObjectDisposedException("too late");
				}
				return GetResponse(_ntpwd);
			}
		}

		public ChallengeResponse()
		{
			_disposed = false;
			_lmpwd = new byte[21];
			_ntpwd = new byte[21];
		}

		public ChallengeResponse(string password, byte[] challenge)
			: this()
		{
			Password = password;
			Challenge = challenge;
		}

		~ChallengeResponse()
		{
			if (!_disposed)
			{
				Dispose();
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				Array.Clear(_lmpwd, 0, _lmpwd.Length);
				Array.Clear(_ntpwd, 0, _ntpwd.Length);
				if (_challenge != null)
				{
					Array.Clear(_challenge, 0, _challenge.Length);
				}
				_disposed = true;
			}
		}

		private byte[] GetResponse(byte[] pwd)
		{
			byte[] array = new byte[24];
			DES dES = DES.Create();
			dES.Mode = CipherMode.ECB;
			dES.Key = PrepareDESKey(pwd, 0);
			dES.CreateEncryptor().TransformBlock(_challenge, 0, 8, array, 0);
			dES.Key = PrepareDESKey(pwd, 7);
			dES.CreateEncryptor().TransformBlock(_challenge, 0, 8, array, 8);
			dES.Key = PrepareDESKey(pwd, 14);
			dES.CreateEncryptor().TransformBlock(_challenge, 0, 8, array, 16);
			return array;
		}

		private byte[] PrepareDESKey(byte[] key56bits, int position)
		{
			return new byte[8]
			{
				key56bits[position],
				(byte)((key56bits[position] << 7) | (key56bits[position + 1] >> 1)),
				(byte)((key56bits[position + 1] << 6) | (key56bits[position + 2] >> 2)),
				(byte)((key56bits[position + 2] << 5) | (key56bits[position + 3] >> 3)),
				(byte)((key56bits[position + 3] << 4) | (key56bits[position + 4] >> 4)),
				(byte)((key56bits[position + 4] << 3) | (key56bits[position + 5] >> 5)),
				(byte)((key56bits[position + 5] << 2) | (key56bits[position + 6] >> 6)),
				(byte)(key56bits[position + 6] << 1)
			};
		}

		private byte[] PasswordToKey(string password, int position)
		{
			byte[] array = new byte[7];
			int charCount = System.Math.Min(password.Length - position, 7);
			Encoding.ASCII.GetBytes(password.ToUpper(CultureInfo.CurrentCulture), position, charCount, array, 0);
			byte[] result = PrepareDESKey(array, 0);
			Array.Clear(array, 0, array.Length);
			return result;
		}
	}
	public static class ChallengeResponse2
	{
		private static byte[] magic = new byte[8] { 75, 71, 83, 33, 64, 35, 36, 37 };

		private static byte[] nullEncMagic = new byte[8] { 170, 211, 180, 53, 181, 20, 4, 238 };

		private static byte[] Compute_LM(string password, byte[] challenge)
		{
			byte[] array = new byte[21];
			DES dES = DES.Create();
			dES.Mode = CipherMode.ECB;
			if (password == null || password.Length < 1)
			{
				Buffer.BlockCopy(nullEncMagic, 0, array, 0, 8);
			}
			else
			{
				dES.Key = PasswordToKey(password, 0);
				dES.CreateEncryptor().TransformBlock(magic, 0, 8, array, 0);
			}
			if (password == null || password.Length < 8)
			{
				Buffer.BlockCopy(nullEncMagic, 0, array, 8, 8);
			}
			else
			{
				dES.Key = PasswordToKey(password, 7);
				dES.CreateEncryptor().TransformBlock(magic, 0, 8, array, 8);
			}
			dES.Clear();
			return GetResponse(challenge, array);
		}

		private static byte[] Compute_NTLM_Password(string password)
		{
			byte[] array = new byte[21];
			MD4 mD = MD4.Create();
			byte[] array2 = ((password == null) ? new byte[0] : Encoding.Unicode.GetBytes(password));
			byte[] array3 = mD.ComputeHash(array2);
			Buffer.BlockCopy(array3, 0, array, 0, 16);
			Array.Clear(array2, 0, array2.Length);
			Array.Clear(array3, 0, array3.Length);
			return array;
		}

		private static byte[] Compute_NTLM(string password, byte[] challenge)
		{
			byte[] pwd = Compute_NTLM_Password(password);
			return GetResponse(challenge, pwd);
		}

		private static void Compute_NTLMv2_Session(string password, byte[] challenge, out byte[] lm, out byte[] ntlm)
		{
			byte[] array = new byte[8];
			RandomNumberGenerator.Create().GetBytes(array);
			byte[] array2 = new byte[challenge.Length + 8];
			challenge.CopyTo(array2, 0);
			array.CopyTo(array2, challenge.Length);
			lm = new byte[24];
			array.CopyTo(lm, 0);
			byte[] array3 = MD5.Create().ComputeHash(array2);
			byte[] array4 = new byte[8];
			Array.Copy(array3, array4, 8);
			ntlm = Compute_NTLM(password, array4);
			Array.Clear(array, 0, array.Length);
			Array.Clear(array2, 0, array2.Length);
			Array.Clear(array4, 0, array4.Length);
			Array.Clear(array3, 0, array3.Length);
		}

		private static byte[] Compute_NTLMv2(Type2Message type2, string username, string password, string domain)
		{
			byte[] array = Compute_NTLM_Password(password);
			byte[] bytes = Encoding.Unicode.GetBytes(username.ToUpperInvariant());
			byte[] bytes2 = Encoding.Unicode.GetBytes(domain);
			byte[] array2 = new byte[bytes.Length + bytes2.Length];
			bytes.CopyTo(array2, 0);
			Array.Copy(bytes2, 0, array2, bytes.Length, bytes2.Length);
			HMACMD5 hMACMD = new HMACMD5(array);
			byte[] array3 = hMACMD.ComputeHash(array2);
			Array.Clear(array, 0, array.Length);
			hMACMD.Clear();
			HMACMD5 hMACMD2 = new HMACMD5(array3);
			long value = DateTime.Now.Ticks - 504911232000000000L;
			byte[] array4 = new byte[8];
			RandomNumberGenerator.Create().GetBytes(array4);
			byte[] array5 = new byte[28 + type2.TargetInfo.Length];
			array5[0] = 1;
			array5[1] = 1;
			Buffer.BlockCopy(Mono.Security.BitConverterLE.GetBytes(value), 0, array5, 8, 8);
			Buffer.BlockCopy(array4, 0, array5, 16, 8);
			Buffer.BlockCopy(type2.TargetInfo, 0, array5, 28, type2.TargetInfo.Length);
			byte[] nonce = type2.Nonce;
			byte[] array6 = new byte[nonce.Length + array5.Length];
			nonce.CopyTo(array6, 0);
			array5.CopyTo(array6, nonce.Length);
			byte[] array7 = hMACMD2.ComputeHash(array6);
			byte[] array8 = new byte[array5.Length + array7.Length];
			array7.CopyTo(array8, 0);
			array5.CopyTo(array8, array7.Length);
			Array.Clear(array3, 0, array3.Length);
			hMACMD2.Clear();
			Array.Clear(array4, 0, array4.Length);
			Array.Clear(array5, 0, array5.Length);
			Array.Clear(array6, 0, array6.Length);
			Array.Clear(array7, 0, array7.Length);
			return array8;
		}

		public static void Compute(Type2Message type2, NtlmAuthLevel level, string username, string password, string domain, out byte[] lm, out byte[] ntlm)
		{
			lm = null;
			switch (level)
			{
			case NtlmAuthLevel.LM_and_NTLM:
				lm = Compute_LM(password, type2.Nonce);
				ntlm = Compute_NTLM(password, type2.Nonce);
				break;
			case NtlmAuthLevel.LM_and_NTLM_and_try_NTLMv2_Session:
				if ((type2.Flags & NtlmFlags.NegotiateNtlm2Key) != 0)
				{
					Compute_NTLMv2_Session(password, type2.Nonce, out lm, out ntlm);
					break;
				}
				goto case NtlmAuthLevel.LM_and_NTLM;
			case NtlmAuthLevel.NTLM_only:
				if ((type2.Flags & NtlmFlags.NegotiateNtlm2Key) != 0)
				{
					Compute_NTLMv2_Session(password, type2.Nonce, out lm, out ntlm);
				}
				else
				{
					ntlm = Compute_NTLM(password, type2.Nonce);
				}
				break;
			case NtlmAuthLevel.NTLMv2_only:
				ntlm = Compute_NTLMv2(type2, username, password, domain);
				break;
			default:
				throw new InvalidOperationException();
			}
		}

		private static byte[] GetResponse(byte[] challenge, byte[] pwd)
		{
			byte[] array = new byte[24];
			DES dES = DES.Create();
			dES.Mode = CipherMode.ECB;
			dES.Key = PrepareDESKey(pwd, 0);
			dES.CreateEncryptor().TransformBlock(challenge, 0, 8, array, 0);
			dES.Key = PrepareDESKey(pwd, 7);
			dES.CreateEncryptor().TransformBlock(challenge, 0, 8, array, 8);
			dES.Key = PrepareDESKey(pwd, 14);
			dES.CreateEncryptor().TransformBlock(challenge, 0, 8, array, 16);
			return array;
		}

		private static byte[] PrepareDESKey(byte[] key56bits, int position)
		{
			return new byte[8]
			{
				key56bits[position],
				(byte)((key56bits[position] << 7) | (key56bits[position + 1] >> 1)),
				(byte)((key56bits[position + 1] << 6) | (key56bits[position + 2] >> 2)),
				(byte)((key56bits[position + 2] << 5) | (key56bits[position + 3] >> 3)),
				(byte)((key56bits[position + 3] << 4) | (key56bits[position + 4] >> 4)),
				(byte)((key56bits[position + 4] << 3) | (key56bits[position + 5] >> 5)),
				(byte)((key56bits[position + 5] << 2) | (key56bits[position + 6] >> 6)),
				(byte)(key56bits[position + 6] << 1)
			};
		}

		private static byte[] PasswordToKey(string password, int position)
		{
			byte[] array = new byte[7];
			int charCount = System.Math.Min(password.Length - position, 7);
			Encoding.ASCII.GetBytes(password.ToUpper(CultureInfo.CurrentCulture), position, charCount, array, 0);
			byte[] result = PrepareDESKey(array, 0);
			Array.Clear(array, 0, array.Length);
			return result;
		}
	}
	public abstract class MessageBase
	{
		private static byte[] header = new byte[8] { 78, 84, 76, 77, 83, 83, 80, 0 };

		private int _type;

		private NtlmFlags _flags;

		public NtlmFlags Flags
		{
			get
			{
				return _flags;
			}
			set
			{
				_flags = value;
			}
		}

		public int Type => _type;

		protected MessageBase(int messageType)
		{
			_type = messageType;
		}

		protected byte[] PrepareMessage(int messageSize)
		{
			byte[] array = new byte[messageSize];
			Buffer.BlockCopy(header, 0, array, 0, 8);
			array[8] = (byte)_type;
			array[9] = (byte)(_type >> 8);
			array[10] = (byte)(_type >> 16);
			array[11] = (byte)(_type >> 24);
			return array;
		}

		protected virtual void Decode(byte[] message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			if (message.Length < 12)
			{
				string text = global::Locale.GetText("Minimum message length is 12 bytes.");
				throw new ArgumentOutOfRangeException("message", message.Length, text);
			}
			if (!CheckHeader(message))
			{
				throw new ArgumentException(string.Format(global::Locale.GetText("Invalid Type{0} message."), _type), "message");
			}
		}

		protected bool CheckHeader(byte[] message)
		{
			for (int i = 0; i < header.Length; i++)
			{
				if (message[i] != header[i])
				{
					return false;
				}
			}
			return Mono.Security.BitConverterLE.ToUInt32(message, 8) == _type;
		}

		public abstract byte[] GetBytes();
	}
	public enum NtlmAuthLevel
	{
		LM_and_NTLM,
		LM_and_NTLM_and_try_NTLMv2_Session,
		NTLM_only,
		NTLMv2_only
	}
	[Flags]
	public enum NtlmFlags
	{
		NegotiateUnicode = 1,
		NegotiateOem = 2,
		RequestTarget = 4,
		NegotiateNtlm = 0x200,
		NegotiateDomainSupplied = 0x1000,
		NegotiateWorkstationSupplied = 0x2000,
		NegotiateAlwaysSign = 0x8000,
		NegotiateNtlm2Key = 0x80000,
		Negotiate128 = 0x20000000,
		Negotiate56 = int.MinValue
	}
	public static class NtlmSettings
	{
		private static NtlmAuthLevel defaultAuthLevel = NtlmAuthLevel.LM_and_NTLM_and_try_NTLMv2_Session;

		public static NtlmAuthLevel DefaultAuthLevel
		{
			get
			{
				return defaultAuthLevel;
			}
			set
			{
				defaultAuthLevel = value;
			}
		}
	}
	public class Type1Message : MessageBase
	{
		private string _host;

		private string _domain;

		public string Domain
		{
			get
			{
				return _domain;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (value == "")
				{
					base.Flags &= ~NtlmFlags.NegotiateDomainSupplied;
				}
				else
				{
					base.Flags |= NtlmFlags.NegotiateDomainSupplied;
				}
				_domain = value;
			}
		}

		public string Host
		{
			get
			{
				return _host;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (value == "")
				{
					base.Flags &= ~NtlmFlags.NegotiateWorkstationSupplied;
				}
				else
				{
					base.Flags |= NtlmFlags.NegotiateWorkstationSupplied;
				}
				_host = value;
			}
		}

		public Type1Message()
			: base(1)
		{
			_domain = Environment.UserDomainName;
			_host = Environment.MachineName;
			base.Flags = NtlmFlags.NegotiateUnicode | NtlmFlags.NegotiateOem | NtlmFlags.RequestTarget | NtlmFlags.NegotiateNtlm | NtlmFlags.NegotiateDomainSupplied | NtlmFlags.NegotiateWorkstationSupplied | NtlmFlags.NegotiateAlwaysSign;
		}

		public Type1Message(byte[] message)
			: base(1)
		{
			Decode(message);
		}

		protected override void Decode(byte[] message)
		{
			base.Decode(message);
			base.Flags = (NtlmFlags)Mono.Security.BitConverterLE.ToUInt32(message, 12);
			int count = Mono.Security.BitConverterLE.ToUInt16(message, 16);
			int index = Mono.Security.BitConverterLE.ToUInt16(message, 20);
			_domain = Encoding.ASCII.GetString(message, index, count);
			int count2 = Mono.Security.BitConverterLE.ToUInt16(message, 24);
			_host = Encoding.ASCII.GetString(message, 32, count2);
		}

		public override byte[] GetBytes()
		{
			short num = (short)_domain.Length;
			short num2 = (short)_host.Length;
			byte[] array = PrepareMessage(32 + num + num2);
			array[12] = (byte)base.Flags;
			array[13] = (byte)((uint)base.Flags >> 8);
			array[14] = (byte)((uint)base.Flags >> 16);
			array[15] = (byte)((uint)base.Flags >> 24);
			short num3 = (short)(32 + num2);
			array[16] = (byte)num;
			array[17] = (byte)(num >> 8);
			array[18] = array[16];
			array[19] = array[17];
			array[20] = (byte)num3;
			array[21] = (byte)(num3 >> 8);
			array[24] = (byte)num2;
			array[25] = (byte)(num2 >> 8);
			array[26] = array[24];
			array[27] = array[25];
			array[28] = 32;
			array[29] = 0;
			byte[] bytes = Encoding.ASCII.GetBytes(_host.ToUpper(CultureInfo.InvariantCulture));
			Buffer.BlockCopy(bytes, 0, array, 32, bytes.Length);
			byte[] bytes2 = Encoding.ASCII.GetBytes(_domain.ToUpper(CultureInfo.InvariantCulture));
			Buffer.BlockCopy(bytes2, 0, array, num3, bytes2.Length);
			return array;
		}
	}
	public class Type2Message : MessageBase
	{
		private byte[] _nonce;

		private string _targetName;

		private byte[] _targetInfo;

		public byte[] Nonce
		{
			get
			{
				return (byte[])_nonce.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Nonce");
				}
				if (value.Length != 8)
				{
					throw new ArgumentException(global::Locale.GetText("Invalid Nonce Length (should be 8 bytes)."), "Nonce");
				}
				_nonce = (byte[])value.Clone();
			}
		}

		public string TargetName => _targetName;

		public byte[] TargetInfo => (byte[])_targetInfo.Clone();

		public Type2Message()
			: base(2)
		{
			_nonce = new byte[8];
			RandomNumberGenerator.Create().GetBytes(_nonce);
			base.Flags = NtlmFlags.NegotiateUnicode | NtlmFlags.NegotiateNtlm | NtlmFlags.NegotiateAlwaysSign;
		}

		public Type2Message(byte[] message)
			: base(2)
		{
			_nonce = new byte[8];
			Decode(message);
		}

		~Type2Message()
		{
			if (_nonce != null)
			{
				Array.Clear(_nonce, 0, _nonce.Length);
			}
		}

		protected override void Decode(byte[] message)
		{
			base.Decode(message);
			base.Flags = (NtlmFlags)Mono.Security.BitConverterLE.ToUInt32(message, 20);
			Buffer.BlockCopy(message, 24, _nonce, 0, 8);
			ushort num = Mono.Security.BitConverterLE.ToUInt16(message, 12);
			ushort index = Mono.Security.BitConverterLE.ToUInt16(message, 16);
			if (num > 0)
			{
				if ((base.Flags & NtlmFlags.NegotiateOem) != 0)
				{
					_targetName = Encoding.ASCII.GetString(message, index, num);
				}
				else
				{
					_targetName = Encoding.Unicode.GetString(message, index, num);
				}
			}
			if (message.Length >= 48)
			{
				ushort num2 = Mono.Security.BitConverterLE.ToUInt16(message, 40);
				ushort srcOffset = Mono.Security.BitConverterLE.ToUInt16(message, 44);
				if (num2 > 0)
				{
					_targetInfo = new byte[num2];
					Buffer.BlockCopy(message, srcOffset, _targetInfo, 0, num2);
				}
			}
		}

		public override byte[] GetBytes()
		{
			byte[] array = PrepareMessage(40);
			short num = (short)array.Length;
			array[16] = (byte)num;
			array[17] = (byte)(num >> 8);
			array[20] = (byte)base.Flags;
			array[21] = (byte)((uint)base.Flags >> 8);
			array[22] = (byte)((uint)base.Flags >> 16);
			array[23] = (byte)((uint)base.Flags >> 24);
			Buffer.BlockCopy(_nonce, 0, array, 24, _nonce.Length);
			return array;
		}
	}
	public class Type3Message : MessageBase
	{
		private NtlmAuthLevel _level;

		private byte[] _challenge;

		private string _host;

		private string _domain;

		private string _username;

		private string _password;

		private Type2Message _type2;

		private byte[] _lm;

		private byte[] _nt;

		internal const string LegacyAPIWarning = "Use of this API is highly discouraged, it selects legacy-mode LM/NTLM authentication, which sends your password in very weak encryption over the wire even if the server supports the more secure NTLMv2 / NTLMv2 Session. You need to use the new `Type3Message (Type2Message)' constructor to use the more secure NTLMv2 / NTLMv2 Session authentication modes. These require the Type 2 message from the server to compute the response.";

		[Obsolete("Use NtlmSettings.DefaultAuthLevel")]
		public static NtlmAuthLevel DefaultAuthLevel
		{
			get
			{
				return NtlmSettings.DefaultAuthLevel;
			}
			set
			{
				NtlmSettings.DefaultAuthLevel = value;
			}
		}

		public NtlmAuthLevel Level
		{
			get
			{
				return _level;
			}
			set
			{
				_level = value;
			}
		}

		[Obsolete("Use of this API is highly discouraged, it selects legacy-mode LM/NTLM authentication, which sends your password in very weak encryption over the wire even if the server supports the more secure NTLMv2 / NTLMv2 Session. You need to use the new `Type3Message (Type2Message)' constructor to use the more secure NTLMv2 / NTLMv2 Session authentication modes. These require the Type 2 message from the server to compute the response.")]
		public byte[] Challenge
		{
			get
			{
				if (_challenge == null)
				{
					return null;
				}
				return (byte[])_challenge.Clone();
			}
			set
			{
				if (_type2 != null || _level != NtlmAuthLevel.LM_and_NTLM)
				{
					throw new InvalidOperationException("Refusing to use legacy-mode LM/NTLM authentication unless explicitly enabled using DefaultAuthLevel.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("Challenge");
				}
				if (value.Length != 8)
				{
					throw new ArgumentException(global::Locale.GetText("Invalid Challenge Length (should be 8 bytes)."), "Challenge");
				}
				_challenge = (byte[])value.Clone();
			}
		}

		public string Domain
		{
			get
			{
				return _domain;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (value == "")
				{
					base.Flags &= ~NtlmFlags.NegotiateDomainSupplied;
				}
				else
				{
					base.Flags |= NtlmFlags.NegotiateDomainSupplied;
				}
				_domain = value;
			}
		}

		public string Host
		{
			get
			{
				return _host;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (value == "")
				{
					base.Flags &= ~NtlmFlags.NegotiateWorkstationSupplied;
				}
				else
				{
					base.Flags |= NtlmFlags.NegotiateWorkstationSupplied;
				}
				_host = value;
			}
		}

		public string Password
		{
			get
			{
				return _password;
			}
			set
			{
				_password = value;
			}
		}

		public string Username
		{
			get
			{
				return _username;
			}
			set
			{
				_username = value;
			}
		}

		public byte[] LM => _lm;

		public byte[] NT
		{
			get
			{
				return _nt;
			}
			set
			{
				_nt = value;
			}
		}

		[Obsolete("Use of this API is highly discouraged, it selects legacy-mode LM/NTLM authentication, which sends your password in very weak encryption over the wire even if the server supports the more secure NTLMv2 / NTLMv2 Session. You need to use the new `Type3Message (Type2Message)' constructor to use the more secure NTLMv2 / NTLMv2 Session authentication modes. These require the Type 2 message from the server to compute the response.")]
		public Type3Message()
			: base(3)
		{
			if (DefaultAuthLevel != NtlmAuthLevel.LM_and_NTLM)
			{
				throw new InvalidOperationException("Refusing to use legacy-mode LM/NTLM authentication unless explicitly enabled using DefaultAuthLevel.");
			}
			_domain = Environment.UserDomainName;
			_host = Environment.MachineName;
			_username = Environment.UserName;
			_level = NtlmAuthLevel.LM_and_NTLM;
			base.Flags = NtlmFlags.NegotiateUnicode | NtlmFlags.NegotiateNtlm | NtlmFlags.NegotiateAlwaysSign;
		}

		public Type3Message(byte[] message)
			: base(3)
		{
			Decode(message);
		}

		public Type3Message(Type2Message type2)
			: base(3)
		{
			_type2 = type2;
			_level = NtlmSettings.DefaultAuthLevel;
			_challenge = (byte[])type2.Nonce.Clone();
			_domain = type2.TargetName;
			_host = Environment.MachineName;
			_username = Environment.UserName;
			base.Flags = NtlmFlags.NegotiateNtlm | NtlmFlags.NegotiateAlwaysSign;
			if ((type2.Flags & NtlmFlags.NegotiateUnicode) != 0)
			{
				base.Flags |= NtlmFlags.NegotiateUnicode;
			}
			else
			{
				base.Flags |= NtlmFlags.NegotiateOem;
			}
			if ((type2.Flags & NtlmFlags.NegotiateNtlm2Key) != 0)
			{
				base.Flags |= NtlmFlags.NegotiateNtlm2Key;
			}
		}

		~Type3Message()
		{
			if (_challenge != null)
			{
				Array.Clear(_challenge, 0, _challenge.Length);
			}
			if (_lm != null)
			{
				Array.Clear(_lm, 0, _lm.Length);
			}
			if (_nt != null)
			{
				Array.Clear(_nt, 0, _nt.Length);
			}
		}

		protected override void Decode(byte[] message)
		{
			base.Decode(message);
			_password = null;
			if (message.Length >= 64)
			{
				base.Flags = (NtlmFlags)Mono.Security.BitConverterLE.ToUInt32(message, 60);
			}
			else
			{
				base.Flags = NtlmFlags.NegotiateUnicode | NtlmFlags.NegotiateNtlm | NtlmFlags.NegotiateAlwaysSign;
			}
			int num = Mono.Security.BitConverterLE.ToUInt16(message, 12);
			int srcOffset = Mono.Security.BitConverterLE.ToUInt16(message, 16);
			_lm = new byte[num];
			Buffer.BlockCopy(message, srcOffset, _lm, 0, num);
			int num2 = Mono.Security.BitConverterLE.ToUInt16(message, 20);
			int srcOffset2 = Mono.Security.BitConverterLE.ToUInt16(message, 24);
			_nt = new byte[num2];
			Buffer.BlockCopy(message, srcOffset2, _nt, 0, num2);
			int len = Mono.Security.BitConverterLE.ToUInt16(message, 28);
			int offset = Mono.Security.BitConverterLE.ToUInt16(message, 32);
			_domain = DecodeString(message, offset, len);
			int len2 = Mono.Security.BitConverterLE.ToUInt16(message, 36);
			int offset2 = Mono.Security.BitConverterLE.ToUInt16(message, 40);
			_username = DecodeString(message, offset2, len2);
			int len3 = Mono.Security.BitConverterLE.ToUInt16(message, 44);
			int offset3 = Mono.Security.BitConverterLE.ToUInt16(message, 48);
			_host = DecodeString(message, offset3, len3);
		}

		private string DecodeString(byte[] buffer, int offset, int len)
		{
			if ((base.Flags & NtlmFlags.NegotiateUnicode) != 0)
			{
				return Encoding.Unicode.GetString(buffer, offset, len);
			}
			return Encoding.ASCII.GetString(buffer, offset, len);
		}

		private byte[] EncodeString(string text)
		{
			if (text == null)
			{
				return new byte[0];
			}
			if ((base.Flags & NtlmFlags.NegotiateUnicode) != 0)
			{
				return Encoding.Unicode.GetBytes(text);
			}
			return Encoding.ASCII.GetBytes(text);
		}

		public override byte[] GetBytes()
		{
			byte[] array = EncodeString(_domain);
			byte[] array2 = EncodeString(_username);
			byte[] array3 = EncodeString(_host);
			byte[] lm;
			byte[] ntlm;
			if (_type2 == null)
			{
				if (_level != NtlmAuthLevel.LM_and_NTLM)
				{
					throw new InvalidOperationException("Refusing to use legacy-mode LM/NTLM authentication unless explicitly enabled using DefaultAuthLevel.");
				}
				using ChallengeResponse challengeResponse = new ChallengeResponse(_password, _challenge);
				lm = challengeResponse.LM;
				ntlm = challengeResponse.NT;
			}
			else
			{
				ChallengeResponse2.Compute(_type2, _level, _username, _password, _domain, out lm, out ntlm);
			}
			int num = ((lm != null) ? lm.Length : 0);
			int num2 = ((ntlm != null) ? ntlm.Length : 0);
			byte[] array4 = PrepareMessage(64 + array.Length + array2.Length + array3.Length + num + num2);
			short num3 = (short)(64 + array.Length + array2.Length + array3.Length);
			array4[12] = (byte)num;
			array4[13] = 0;
			array4[14] = (byte)num;
			array4[15] = 0;
			array4[16] = (byte)num3;
			array4[17] = (byte)(num3 >> 8);
			short num4 = (short)(num3 + num);
			array4[20] = (byte)num2;
			array4[21] = (byte)(num2 >> 8);
			array4[22] = (byte)num2;
			array4[23] = (byte)(num2 >> 8);
			array4[24] = (byte)num4;
			array4[25] = (byte)(num4 >> 8);
			short num5 = (short)array.Length;
			short num6 = 64;
			array4[28] = (byte)num5;
			array4[29] = (byte)(num5 >> 8);
			array4[30] = array4[28];
			array4[31] = array4[29];
			array4[32] = (byte)num6;
			array4[33] = (byte)(num6 >> 8);
			short num7 = (short)array2.Length;
			short num8 = (short)(num6 + num5);
			array4[36] = (byte)num7;
			array4[37] = (byte)(num7 >> 8);
			array4[38] = array4[36];
			array4[39] = array4[37];
			array4[40] = (byte)num8;
			array4[41] = (byte)(num8 >> 8);
			short num9 = (short)array3.Length;
			short num10 = (short)(num8 + num7);
			array4[44] = (byte)num9;
			array4[45] = (byte)(num9 >> 8);
			array4[46] = array4[44];
			array4[47] = array4[45];
			array4[48] = (byte)num10;
			array4[49] = (byte)(num10 >> 8);
			short num11 = (short)array4.Length;
			array4[56] = (byte)num11;
			array4[57] = (byte)(num11 >> 8);
			int flags = (int)base.Flags;
			array4[60] = (byte)flags;
			array4[61] = (byte)((uint)flags >> 8);
			array4[62] = (byte)((uint)flags >> 16);
			array4[63] = (byte)((uint)flags >> 24);
			Buffer.BlockCopy(array, 0, array4, num6, array.Length);
			Buffer.BlockCopy(array2, 0, array4, num8, array2.Length);
			Buffer.BlockCopy(array3, 0, array4, num10, array3.Length);
			if (lm != null)
			{
				Buffer.BlockCopy(lm, 0, array4, num3, lm.Length);
				Array.Clear(lm, 0, lm.Length);
			}
			Buffer.BlockCopy(ntlm, 0, array4, num4, ntlm.Length);
			Array.Clear(ntlm, 0, ntlm.Length);
			return array4;
		}
	}
}
namespace Mono.Security.Interface
{
	public enum AlertLevel : byte
	{
		Warning = 1,
		Fatal
	}
	public enum AlertDescription : byte
	{
		CloseNotify = 0,
		UnexpectedMessage = 10,
		BadRecordMAC = 20,
		DecryptionFailed_RESERVED = 21,
		RecordOverflow = 22,
		DecompressionFailure = 30,
		HandshakeFailure = 40,
		NoCertificate_RESERVED = 41,
		BadCertificate = 42,
		UnsupportedCertificate = 43,
		CertificateRevoked = 44,
		CertificateExpired = 45,
		CertificateUnknown = 46,
		IlegalParameter = 47,
		UnknownCA = 48,
		AccessDenied = 49,
		DecodeError = 50,
		DecryptError = 51,
		ExportRestriction = 60,
		ProtocolVersion = 70,
		InsuficientSecurity = 71,
		InternalError = 80,
		UserCancelled = 90,
		NoRenegotiation = 100,
		UnsupportedExtension = 110
	}
	public class Alert
	{
		private AlertLevel level;

		private AlertDescription description;

		public AlertLevel Level => level;

		public AlertDescription Description => description;

		public string Message => GetAlertMessage(description);

		public bool IsWarning
		{
			get
			{
				if (level != AlertLevel.Warning)
				{
					return false;
				}
				return true;
			}
		}

		public bool IsCloseNotify
		{
			get
			{
				if (IsWarning && description == AlertDescription.CloseNotify)
				{
					return true;
				}
				return false;
			}
		}

		public Alert(AlertDescription description)
		{
			this.description = description;
			inferAlertLevel();
		}

		public Alert(AlertLevel level, AlertDescription description)
		{
			this.level = level;
			this.description = description;
		}

		private void inferAlertLevel()
		{
			switch (description)
			{
			case AlertDescription.CloseNotify:
			case AlertDescription.UserCancelled:
			case AlertDescription.NoRenegotiation:
				level = AlertLevel.Warning;
				break;
			default:
				level = AlertLevel.Fatal;
				break;
			}
		}

		public override string ToString()
		{
			return $"[Alert: {Level}:{Description}]";
		}

		public static string GetAlertMessage(AlertDescription description)
		{
			return "The authentication or decryption has failed.";
		}
	}
	public class ValidationResult
	{
		private bool trusted;

		private bool user_denied;

		private int error_code;

		private MonoSslPolicyErrors? policy_errors;

		public bool Trusted => trusted;

		public bool UserDenied => user_denied;

		public int ErrorCode => error_code;

		public MonoSslPolicyErrors? PolicyErrors => policy_errors;

		public ValidationResult(bool trusted, bool user_denied, int error_code, MonoSslPolicyErrors? policy_errors)
		{
			this.trusted = trusted;
			this.user_denied = user_denied;
			this.error_code = error_code;
			this.policy_errors = policy_errors;
		}

		internal ValidationResult(bool trusted, bool user_denied, int error_code)
		{
			this.trusted = trusted;
			this.user_denied = user_denied;
			this.error_code = error_code;
		}
	}
	public interface ICertificateValidator
	{
		MonoTlsSettings Settings { get; }

		bool SelectClientCertificate(string targetHost, System.Security.Cryptography.X509Certificates.X509CertificateCollection localCertificates, System.Security.Cryptography.X509Certificates.X509Certificate remoteCertificate, string[] acceptableIssuers, out System.Security.Cryptography.X509Certificates.X509Certificate clientCertificate);

		ValidationResult ValidateCertificate(string targetHost, bool serverMode, System.Security.Cryptography.X509Certificates.X509CertificateCollection certificates);
	}
	public static class CertificateValidationHelper
	{
		private const string SecurityLibrary = "/System/Library/Frameworks/Security.framework/Security";

		private static readonly bool noX509Chain;

		private static readonly bool supportsTrustAnchors;

		public static bool SupportsX509Chain => !noX509Chain;

		public static bool SupportsTrustAnchors => supportsTrustAnchors;

		static CertificateValidationHelper()
		{
			if (File.Exists("/System/Library/Frameworks/Security.framework/Security"))
			{
				noX509Chain = true;
				supportsTrustAnchors = true;
			}
			else
			{
				noX509Chain = false;
				supportsTrustAnchors = false;
			}
		}

		public static ICertificateValidator GetValidator(MonoTlsSettings settings)
		{
			return (ICertificateValidator)NoReflectionHelper.GetDefaultValidator(settings);
		}
	}
	public enum CipherAlgorithmType
	{
		None,
		Aes128,
		Aes256,
		AesGcm128,
		AesGcm256
	}
	[CLSCompliant(false)]
	public enum CipherSuiteCode : ushort
	{
		TLS_NULL_WITH_NULL_NULL = 0,
		TLS_RSA_WITH_NULL_MD5 = 1,
		TLS_RSA_WITH_NULL_SHA = 2,
		TLS_RSA_EXPORT_WITH_RC4_40_MD5 = 3,
		TLS_RSA_WITH_RC4_128_MD5 = 4,
		TLS_RSA_WITH_RC4_128_SHA = 5,
		TLS_RSA_EXPORT_WITH_RC2_CBC_40_MD5 = 6,
		TLS_RSA_WITH_IDEA_CBC_SHA = 7,
		TLS_RSA_EXPORT_WITH_DES40_CBC_SHA = 8,
		TLS_RSA_WITH_DES_CBC_SHA = 9,
		TLS_RSA_WITH_3DES_EDE_CBC_SHA = 10,
		TLS_DH_DSS_EXPORT_WITH_DES40_CBC_SHA = 11,
		TLS_DH_DSS_WITH_DES_CBC_SHA = 12,
		TLS_DH_DSS_WITH_3DES_EDE_CBC_SHA = 13,
		TLS_DH_RSA_EXPORT_WITH_DES40_CBC_SHA = 14,
		TLS_DH_RSA_WITH_DES_CBC_SHA = 15,
		TLS_DH_RSA_WITH_3DES_EDE_CBC_SHA = 16,
		TLS_DHE_DSS_EXPORT_WITH_DES40_CBC_SHA = 17,
		TLS_DHE_DSS_WITH_DES_CBC_SHA = 18,
		TLS_DHE_DSS_WITH_3DES_EDE_CBC_SHA = 19,
		TLS_DHE_RSA_EXPORT_WITH_DES40_CBC_SHA = 20,
		TLS_DHE_RSA_WITH_DES_CBC_SHA = 21,
		TLS_DHE_RSA_WITH_3DES_EDE_CBC_SHA = 22,
		TLS_DH_anon_EXPORT_WITH_RC4_40_MD5 = 23,
		TLS_DH_anon_WITH_RC4_128_MD5 = 24,
		TLS_DH_anon_EXPORT_WITH_DES40_CBC_SHA = 25,
		TLS_DH_anon_WITH_DES_CBC_SHA = 26,
		TLS_DH_anon_WITH_3DES_EDE_CBC_SHA = 27,
		TLS_RSA_WITH_AES_128_CBC_SHA = 47,
		TLS_DH_DSS_WITH_AES_128_CBC_SHA = 48,
		TLS_DH_RSA_WITH_AES_128_CBC_SHA = 49,
		TLS_DHE_DSS_WITH_AES_128_CBC_SHA = 50,
		TLS_DHE_RSA_WITH_AES_128_CBC_SHA = 51,
		TLS_DH_anon_WITH_AES_128_CBC_SHA = 52,
		TLS_RSA_WITH_AES_256_CBC_SHA = 53,
		TLS_DH_DSS_WITH_AES_256_CBC_SHA = 54,
		TLS_DH_RSA_WITH_AES_256_CBC_SHA = 55,
		TLS_DHE_DSS_WITH_AES_256_CBC_SHA = 56,
		TLS_DHE_RSA_WITH_AES_256_CBC_SHA = 57,
		TLS_DH_anon_WITH_AES_256_CBC_SHA = 58,
		TLS_RSA_WITH_CAMELLIA_128_CBC_SHA = 65,
		TLS_DH_DSS_WITH_CAMELLIA_128_CBC_SHA = 66,
		TLS_DH_RSA_WITH_CAMELLIA_128_CBC_SHA = 67,
		TLS_DHE_DSS_WITH_CAMELLIA_128_CBC_SHA = 68,
		TLS_DHE_RSA_WITH_CAMELLIA_128_CBC_SHA = 69,
		TLS_DH_anon_WITH_CAMELLIA_128_CBC_SHA = 70,
		TLS_RSA_WITH_CAMELLIA_256_CBC_SHA = 132,
		TLS_DH_DSS_WITH_CAMELLIA_256_CBC_SHA = 133,
		TLS_DH_RSA_WITH_CAMELLIA_256_CBC_SHA = 134,
		TLS_DHE_DSS_WITH_CAMELLIA_256_CBC_SHA = 135,
		TLS_DHE_RSA_WITH_CAMELLIA_256_CBC_SHA = 136,
		TLS_DH_anon_WITH_CAMELLIA_256_CBC_SHA = 137,
		TLS_RSA_WITH_CAMELLIA_128_CBC_SHA256 = 186,
		TLS_DH_DSS_WITH_CAMELLIA_128_CBC_SHA256 = 187,
		TLS_DH_RSA_WITH_CAMELLIA_128_CBC_SHA256 = 188,
		TLS_DHE_DSS_WITH_CAMELLIA_128_CBC_SHA256 = 189,
		TLS_DHE_RSA_WITH_CAMELLIA_128_CBC_SHA256 = 190,
		TLS_DH_anon_WITH_CAMELLIA_128_CBC_SHA256 = 191,
		TLS_RSA_WITH_CAMELLIA_256_CBC_SHA256 = 192,
		TLS_DH_DSS_WITH_CAMELLIA_256_CBC_SHA256 = 193,
		TLS_DH_RSA_WITH_CAMELLIA_256_CBC_SHA256 = 194,
		TLS_DHE_DSS_WITH_CAMELLIA_256_CBC_SHA256 = 195,
		TLS_DHE_RSA_WITH_CAMELLIA_256_CBC_SHA256 = 196,
		TLS_DH_anon_WITH_CAMELLIA_256_CBC_SHA256 = 197,
		TLS_RSA_WITH_SEED_CBC_SHA = 150,
		TLS_DH_DSS_WITH_SEED_CBC_SHA = 151,
		TLS_DH_RSA_WITH_SEED_CBC_SHA = 152,
		TLS_DHE_DSS_WITH_SEED_CBC_SHA = 153,
		TLS_DHE_RSA_WITH_SEED_CBC_SHA = 154,
		TLS_DH_anon_WITH_SEED_CBC_SHA = 155,
		TLS_PSK_WITH_RC4_128_SHA = 138,
		TLS_PSK_WITH_3DES_EDE_CBC_SHA = 139,
		TLS_PSK_WITH_AES_128_CBC_SHA = 140,
		TLS_PSK_WITH_AES_256_CBC_SHA = 141,
		TLS_DHE_PSK_WITH_RC4_128_SHA = 142,
		TLS_DHE_PSK_WITH_3DES_EDE_CBC_SHA = 143,
		TLS_DHE_PSK_WITH_AES_128_CBC_SHA = 144,
		TLS_DHE_PSK_WITH_AES_256_CBC_SHA = 145,
		TLS_RSA_PSK_WITH_RC4_128_SHA = 146,
		TLS_RSA_PSK_WITH_3DES_EDE_CBC_SHA = 147,
		TLS_RSA_PSK_WITH_AES_128_CBC_SHA = 148,
		TLS_RSA_PSK_WITH_AES_256_CBC_SHA = 149,
		TLS_ECDH_ECDSA_WITH_NULL_SHA = 49153,
		TLS_ECDH_ECDSA_WITH_RC4_128_SHA = 49154,
		TLS_ECDH_ECDSA_WITH_3DES_EDE_CBC_SHA = 49155,
		TLS_ECDH_ECDSA_WITH_AES_128_CBC_SHA = 49156,
		TLS_ECDH_ECDSA_WITH_AES_256_CBC_SHA = 49157,
		TLS_ECDHE_ECDSA_WITH_NULL_SHA = 49158,
		TLS_ECDHE_ECDSA_WITH_RC4_128_SHA = 49159,
		TLS_ECDHE_ECDSA_WITH_3DES_EDE_CBC_SHA = 49160,
		TLS_ECDHE_ECDSA_WITH_AES_128_CBC_SHA = 49161,
		TLS_ECDHE_ECDSA_WITH_AES_256_CBC_SHA = 49162,
		TLS_ECDH_RSA_WITH_NULL_SHA = 49163,
		TLS_ECDH_RSA_WITH_RC4_128_SHA = 49164,
		TLS_ECDH_RSA_WITH_3DES_EDE_CBC_SHA = 49165,
		TLS_ECDH_RSA_WITH_AES_128_CBC_SHA = 49166,
		TLS_ECDH_RSA_WITH_AES_256_CBC_SHA = 49167,
		TLS_ECDHE_RSA_WITH_NULL_SHA = 49168,
		TLS_ECDHE_RSA_WITH_RC4_128_SHA = 49169,
		TLS_ECDHE_RSA_WITH_3DES_EDE_CBC_SHA = 49170,
		TLS_ECDHE_RSA_WITH_AES_128_CBC_SHA = 49171,
		TLS_ECDHE_RSA_WITH_AES_256_CBC_SHA = 49172,
		TLS_ECDH_anon_WITH_NULL_SHA = 49173,
		TLS_ECDH_anon_WITH_RC4_128_SHA = 49174,
		TLS_ECDH_anon_WITH_3DES_EDE_CBC_SHA = 49175,
		TLS_ECDH_anon_WITH_AES_128_CBC_SHA = 49176,
		TLS_ECDH_anon_WITH_AES_256_CBC_SHA = 49177,
		TLS_PSK_WITH_NULL_SHA = 44,
		TLS_DHE_PSK_WITH_NULL_SHA = 45,
		TLS_RSA_PSK_WITH_NULL_SHA = 46,
		TLS_SRP_SHA_WITH_3DES_EDE_CBC_SHA = 49178,
		TLS_SRP_SHA_RSA_WITH_3DES_EDE_CBC_SHA = 49179,
		TLS_SRP_SHA_DSS_WITH_3DES_EDE_CBC_SHA = 49180,
		TLS_SRP_SHA_WITH_AES_128_CBC_SHA = 49181,
		TLS_SRP_SHA_RSA_WITH_AES_128_CBC_SHA = 49182,
		TLS_SRP_SHA_DSS_WITH_AES_128_CBC_SHA = 49183,
		TLS_SRP_SHA_WITH_AES_256_CBC_SHA = 49184,
		TLS_SRP_SHA_RSA_WITH_AES_256_CBC_SHA = 49185,
		TLS_SRP_SHA_DSS_WITH_AES_256_CBC_SHA = 49186,
		TLS_RSA_WITH_NULL_SHA256 = 59,
		TLS_RSA_WITH_AES_128_CBC_SHA256 = 60,
		TLS_RSA_WITH_AES_256_CBC_SHA256 = 61,
		TLS_DH_DSS_WITH_AES_128_CBC_SHA256 = 62,
		TLS_DH_RSA_WITH_AES_128_CBC_SHA256 = 63,
		TLS_DHE_DSS_WITH_AES_128_CBC_SHA256 = 64,
		TLS_DHE_RSA_WITH_AES_128_CBC_SHA256 = 103,
		TLS_DH_DSS_WITH_AES_256_CBC_SHA256 = 104,
		TLS_DH_RSA_WITH_AES_256_CBC_SHA256 = 105,
		TLS_DHE_DSS_WITH_AES_256_CBC_SHA256 = 106,
		TLS_DHE_RSA_WITH_AES_256_CBC_SHA256 = 107,
		TLS_DH_anon_WITH_AES_128_CBC_SHA256 = 108,
		TLS_DH_anon_WITH_AES_256_CBC_SHA256 = 109,
		TLS_RSA_WITH_AES_128_GCM_SHA256 = 156,
		TLS_RSA_WITH_AES_256_GCM_SHA384 = 157,
		TLS_DHE_RSA_WITH_AES_128_GCM_SHA256 = 158,
		TLS_DHE_RSA_WITH_AES_256_GCM_SHA384 = 159,
		TLS_DH_RSA_WITH_AES_128_GCM_SHA256 = 160,
		TLS_DH_RSA_WITH_AES_256_GCM_SHA384 = 161,
		TLS_DHE_DSS_WITH_AES_128_GCM_SHA256 = 162,
		TLS_DHE_DSS_WITH_AES_256_GCM_SHA384 = 163,
		TLS_DH_DSS_WITH_AES_128_GCM_SHA256 = 164,
		TLS_DH_DSS_WITH_AES_256_GCM_SHA384 = 165,
		TLS_DH_anon_WITH_AES_128_GCM_SHA256 = 166,
		TLS_DH_anon_WITH_AES_256_GCM_SHA384 = 167,
		TLS_ECDHE_ECDSA_WITH_AES_128_CBC_SHA256 = 49187,
		TLS_ECDHE_ECDSA_WITH_AES_256_CBC_SHA384 = 49188,
		TLS_ECDH_ECDSA_WITH_AES_128_CBC_SHA256 = 49189,
		TLS_ECDH_ECDSA_WITH_AES_256_CBC_SHA384 = 49190,
		TLS_ECDHE_RSA_WITH_AES_128_CBC_SHA256 = 49191,
		TLS_ECDHE_RSA_WITH_AES_256_CBC_SHA384 = 49192,
		TLS_ECDH_RSA_WITH_AES_128_CBC_SHA256 = 49193,
		TLS_ECDH_RSA_WITH_AES_256_CBC_SHA384 = 49194,
		TLS_ECDHE_ECDSA_WITH_AES_128_GCM_SHA256 = 49195,
		TLS_ECDHE_ECDSA_WITH_AES_256_GCM_SHA384 = 49196,
		TLS_ECDH_ECDSA_WITH_AES_128_GCM_SHA256 = 49197,
		TLS_ECDH_ECDSA_WITH_AES_256_GCM_SHA384 = 49198,
		TLS_ECDHE_RSA_WITH_AES_128_GCM_SHA256 = 49199,
		TLS_ECDHE_RSA_WITH_AES_256_GCM_SHA384 = 49200,
		TLS_ECDH_RSA_WITH_AES_128_GCM_SHA256 = 49201,
		TLS_ECDH_RSA_WITH_AES_256_GCM_SHA384 = 49202,
		TLS_PSK_WITH_AES_128_GCM_SHA256 = 168,
		TLS_PSK_WITH_AES_256_GCM_SHA384 = 169,
		TLS_DHE_PSK_WITH_AES_128_GCM_SHA256 = 170,
		TLS_DHE_PSK_WITH_AES_256_GCM_SHA384 = 171,
		TLS_RSA_PSK_WITH_AES_128_GCM_SHA256 = 172,
		TLS_RSA_PSK_WITH_AES_256_GCM_SHA384 = 173,
		TLS_PSK_WITH_AES_128_CBC_SHA256 = 174,
		TLS_PSK_WITH_AES_256_CBC_SHA384 = 175,
		TLS_PSK_WITH_NULL_SHA256 = 176,
		TLS_PSK_WITH_NULL_SHA384 = 177,
		TLS_DHE_PSK_WITH_AES_128_CBC_SHA256 = 178,
		TLS_DHE_PSK_WITH_AES_256_CBC_SHA384 = 179,
		TLS_DHE_PSK_WITH_NULL_SHA256 = 180,
		TLS_DHE_PSK_WITH_NULL_SHA384 = 181,
		TLS_RSA_PSK_WITH_AES_128_CBC_SHA256 = 182,
		TLS_RSA_PSK_WITH_AES_256_CBC_SHA384 = 183,
		TLS_RSA_PSK_WITH_NULL_SHA256 = 184,
		TLS_RSA_PSK_WITH_NULL_SHA384 = 185,
		TLS_ECDHE_PSK_WITH_RC4_128_SHA = 49203,
		TLS_ECDHE_PSK_WITH_3DES_EDE_CBC_SHA = 49204,
		TLS_ECDHE_PSK_WITH_AES_128_CBC_SHA = 49205,
		TLS_ECDHE_PSK_WITH_AES_256_CBC_SHA = 49206,
		TLS_ECDHE_PSK_WITH_AES_128_CBC_SHA256 = 49207,
		TLS_ECDHE_PSK_WITH_AES_256_CBC_SHA384 = 49208,
		TLS_ECDHE_PSK_WITH_NULL_SHA = 49209,
		TLS_ECDHE_PSK_WITH_NULL_SHA256 = 49210,
		TLS_ECDHE_PSK_WITH_NULL_SHA384 = 49211,
		TLS_EMPTY_RENEGOTIATION_INFO_SCSV = 255,
		TLS_ECDHE_ECDSA_WITH_CAMELLIA_128_CBC_SHA256 = 49266,
		TLS_ECDHE_ECDSA_WITH_CAMELLIA_256_CBC_SHA384 = 49267,
		TLS_ECDH_ECDSA_WITH_CAMELLIA_128_CBC_SHA256 = 49268,
		TLS_ECDH_ECDSA_WITH_CAMELLIA_256_CBC_SHA384 = 49269,
		TLS_ECDHE_RSA_WITH_CAMELLIA_128_CBC_SHA256 = 49270,
		TLS_ECDHE_RSA_WITH_CAMELLIA_256_CBC_SHA384 = 49271,
		TLS_ECDH_RSA_WITH_CAMELLIA_128_CBC_SHA256 = 49272,
		TLS_ECDH_RSA_WITH_CAMELLIA_256_CBC_SHA384 = 49273,
		TLS_RSA_WITH_CAMELLIA_128_GCM_SHA256 = 49274,
		TLS_RSA_WITH_CAMELLIA_256_GCM_SHA384 = 49275,
		TLS_DHE_RSA_WITH_CAMELLIA_128_GCM_SHA256 = 49276,
		TLS_DHE_RSA_WITH_CAMELLIA_256_GCM_SHA384 = 49277,
		TLS_DH_RSA_WITH_CAMELLIA_128_GCM_SHA256 = 49278,
		TLS_DH_RSA_WITH_CAMELLIA_256_GCM_SHA384 = 49279,
		TLS_DHE_DSS_WITH_CAMELLIA_128_GCM_SHA256 = 49280,
		TLS_DHE_DSS_WITH_CAMELLIA_256_GCM_SHA384 = 49281,
		TLS_DH_DSS_WITH_CAMELLIA_128_GCM_SHA256 = 49282,
		TLS_DH_DSS_WITH_CAMELLIA_256_GCM_SHA384 = 49283,
		TLS_DH_anon_WITH_CAMELLIA_128_GCM_SHA256 = 49284,
		TLS_DH_anon_WITH_CAMELLIA_256_GCM_SHA384 = 49285,
		TLS_ECDHE_ECDSA_WITH_CAMELLIA_128_GCM_SHA256 = 49286,
		TLS_ECDHE_ECDSA_WITH_CAMELLIA_256_GCM_SHA384 = 49287,
		TLS_ECDH_ECDSA_WITH_CAMELLIA_128_GCM_SHA256 = 49288,
		TLS_ECDH_ECDSA_WITH_CAMELLIA_256_GCM_SHA384 = 49289,
		TLS_ECDHE_RSA_WITH_CAMELLIA_128_GCM_SHA256 = 49290,
		TLS_ECDHE_RSA_WITH_CAMELLIA_256_GCM_SHA384 = 49291,
		TLS_ECDH_RSA_WITH_CAMELLIA_128_GCM_SHA256 = 49292,
		TLS_ECDH_RSA_WITH_CAMELLIA_256_GCM_SHA384 = 49293,
		TLS_PSK_WITH_CAMELLIA_128_GCM_SHA256 = 49294,
		TLS_PSK_WITH_CAMELLIA_256_GCM_SHA384 = 49295,
		TLS_DHE_PSK_WITH_CAMELLIA_128_GCM_SHA256 = 49296,
		TLS_DHE_PSK_WITH_CAMELLIA_256_GCM_SHA384 = 49297,
		TLS_RSA_PSK_WITH_CAMELLIA_128_GCM_SHA256 = 49298,
		TLS_RSA_PSK_WITH_CAMELLIA_256_GCM_SHA384 = 49299,
		TLS_PSK_WITH_CAMELLIA_128_CBC_SHA256 = 49300,
		TLS_PSK_WITH_CAMELLIA_256_CBC_SHA384 = 49301,
		TLS_DHE_PSK_WITH_CAMELLIA_128_CBC_SHA256 = 49302,
		TLS_DHE_PSK_WITH_CAMELLIA_256_CBC_SHA384 = 49303,
		TLS_RSA_PSK_WITH_CAMELLIA_128_CBC_SHA256 = 49304,
		TLS_RSA_PSK_WITH_CAMELLIA_256_CBC_SHA384 = 49305,
		TLS_ECDHE_PSK_WITH_CAMELLIA_128_CBC_SHA256 = 49306,
		TLS_ECDHE_PSK_WITH_CAMELLIA_256_CBC_SHA384 = 49307,
		TLS_RSA_WITH_AES_128_CCM = 49308,
		TLS_RSA_WITH_AES_256_CCM = 49309,
		TLS_DHE_RSA_WITH_AES_128_CCM = 49310,
		TLS_DHE_RSA_WITH_AES_256_CCM = 49311,
		TLS_RSA_WITH_AES_128_CCM_8 = 49312,
		TLS_RSA_WITH_AES_256_CCM_8 = 49313,
		TLS_DHE_RSA_WITH_AES_128_CCM_8 = 49314,
		TLS_DHE_RSA_WITH_AES_256_CCM_8 = 49315,
		TLS_PSK_WITH_AES_128_CCM = 49316,
		TLS_PSK_WITH_AES_256_CCM = 49317,
		TLS_DHE_PSK_WITH_AES_128_CCM = 49318,
		TLS_DHE_PSK_WITH_AES_256_CCM = 49319,
		TLS_PSK_WITH_AES_128_CCM_8 = 49320,
		TLS_PSK_WITH_AES_256_CCM_8 = 49321,
		TLS_PSK_DHE_WITH_AES_128_CCM_8 = 49322,
		TLS_PSK_DHE_WITH_AES_256_CCM_8 = 49323,
		TLS_ECDHE_RSA_WITH_CHACHA20_POLY1305_SHA256 = 52243,
		TLS_ECDHE_ECDSA_WITH_CHACHA20_POLY1305_SHA256 = 52244,
		TLS_DHE_RSA_WITH_CHACHA20_POLY1305_SHA256 = 52245,
		TLS_RSA_WITH_ESTREAM_SALSA20_SHA1 = 58384,
		TLS_RSA_WITH_SALSA20_SHA1 = 58385,
		TLS_ECDHE_RSA_WITH_ESTREAM_SALSA20_SHA1 = 58386,
		TLS_ECDHE_RSA_WITH_SALSA20_SHA1 = 58387,
		TLS_ECDHE_ECDSA_WITH_ESTREAM_SALSA20_SHA1 = 58388,
		TLS_ECDHE_ECDSA_WITH_SALSA20_SHA1 = 58389,
		TLS_PSK_WITH_ESTREAM_SALSA20_SHA1 = 58390,
		TLS_PSK_WITH_SALSA20_SHA1 = 58391,
		TLS_ECDHE_PSK_WITH_ESTREAM_SALSA20_SHA1 = 58392,
		TLS_ECDHE_PSK_WITH_SALSA20_SHA1 = 58393,
		TLS_RSA_PSK_WITH_ESTREAM_SALSA20_SHA1 = 58394,
		TLS_RSA_PSK_WITH_SALSA20_SHA1 = 58395,
		TLS_DHE_PSK_WITH_ESTREAM_SALSA20_SHA1 = 58396,
		TLS_DHE_PSK_WITH_SALSA20_SHA1 = 58397,
		TLS_DHE_RSA_WITH_ESTREAM_SALSA20_SHA1 = 58398,
		TLS_DHE_RSA_WITH_SALSA20_SHA1 = 58399,
		TLS_FALLBACK_SCSV = 22016
	}
	public enum ExchangeAlgorithmType
	{
		None,
		Dhe,
		Rsa,
		EcDhe
	}
	public enum HashAlgorithmType
	{
		None = 0,
		Md5 = 1,
		Sha1 = 2,
		Sha224 = 3,
		Sha256 = 4,
		Sha384 = 5,
		Sha512 = 6,
		Unknown = 255,
		Md5Sha1 = 254
	}
	internal delegate System.Security.Cryptography.X509Certificates.X509Certificate MonoServerCertificateSelectionCallback(object sender, string hostName);
	internal interface IMonoAuthenticationOptions
	{
		bool AllowRenegotiation { get; set; }

		RemoteCertificateValidationCallback RemoteCertificateValidationCallback { get; set; }

		SslProtocols EnabledSslProtocols { get; set; }

		EncryptionPolicy EncryptionPolicy { get; set; }

		X509RevocationMode CertificateRevocationCheckMode { get; set; }
	}
	internal interface IMonoSslClientAuthenticationOptions : IMonoAuthenticationOptions
	{
		LocalCertificateSelectionCallback LocalCertificateSelectionCallback { get; set; }

		string TargetHost { get; set; }

		System.Security.Cryptography.X509Certificates.X509CertificateCollection ClientCertificates { get; set; }
	}
	internal interface IMonoSslServerAuthenticationOptions : IMonoAuthenticationOptions
	{
		bool ClientCertificateRequired { get; set; }

		MonoServerCertificateSelectionCallback ServerCertificateSelectionCallback { get; set; }

		System.Security.Cryptography.X509Certificates.X509Certificate ServerCertificate { get; set; }
	}
	public interface IMonoSslStream : IDisposable
	{
		SslStream SslStream { get; }

		TransportContext TransportContext { get; }

		bool IsAuthenticated { get; }

		bool IsMutuallyAuthenticated { get; }

		bool IsEncrypted { get; }

		bool IsSigned { get; }

		bool IsServer { get; }

		System.Security.Authentication.CipherAlgorithmType CipherAlgorithm { get; }

		int CipherStrength { get; }

		System.Security.Authentication.HashAlgorithmType HashAlgorithm { get; }

		int HashStrength { get; }

		System.Security.Authentication.ExchangeAlgorithmType KeyExchangeAlgorithm { get; }

		int KeyExchangeStrength { get; }

		bool CanRead { get; }

		bool CanTimeout { get; }

		bool CanWrite { get; }

		long Length { get; }

		long Position { get; }

		AuthenticatedStream AuthenticatedStream { get; }

		int ReadTimeout { get; set; }

		int WriteTimeout { get; set; }

		bool CheckCertRevocationStatus { get; }

		System.Security.Cryptography.X509Certificates.X509Certificate InternalLocalCertificate { get; }

		System.Security.Cryptography.X509Certificates.X509Certificate LocalCertificate { get; }

		System.Security.Cryptography.X509Certificates.X509Certificate RemoteCertificate { get; }

		SslProtocols SslProtocol { get; }

		MonoTlsProvider Provider { get; }

		bool CanRenegotiate { get; }

		Task AuthenticateAsClientAsync(string targetHost, System.Security.Cryptography.X509Certificates.X509CertificateCollection clientCertificates, SslProtocols enabledSslProtocols, bool checkCertificateRevocation);

		Task AuthenticateAsServerAsync(System.Security.Cryptography.X509Certificates.X509Certificate serverCertificate, bool clientCertificateRequired, SslProtocols enabledSslProtocols, bool checkCertificateRevocation);

		Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken);

		Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken);

		Task ShutdownAsync();

		void SetLength(long value);

		MonoTlsConnectionInfo GetConnectionInfo();

		Task RenegotiateAsync(CancellationToken cancellationToken);
	}
	public class MonoTlsConnectionInfo
	{
		[CLSCompliant(false)]
		public CipherSuiteCode CipherSuiteCode { get; set; }

		public TlsProtocols ProtocolVersion { get; set; }

		public CipherAlgorithmType CipherAlgorithmType { get; set; }

		public HashAlgorithmType HashAlgorithmType { get; set; }

		public ExchangeAlgorithmType ExchangeAlgorithmType { get; set; }

		public string PeerDomainName { get; set; }

		public override string ToString()
		{
			return $"[MonoTlsConnectionInfo: {ProtocolVersion}:{CipherSuiteCode}]";
		}
	}
	[Flags]
	public enum MonoSslPolicyErrors
	{
		None = 0,
		RemoteCertificateNotAvailable = 1,
		RemoteCertificateNameMismatch = 2,
		RemoteCertificateChainErrors = 4
	}
	public enum MonoEncryptionPolicy
	{
		RequireEncryption,
		AllowNoEncryption,
		NoEncryption
	}
	public delegate bool MonoRemoteCertificateValidationCallback(string targetHost, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, MonoSslPolicyErrors sslPolicyErrors);
	public delegate System.Security.Cryptography.X509Certificates.X509Certificate MonoLocalCertificateSelectionCallback(string targetHost, System.Security.Cryptography.X509Certificates.X509CertificateCollection localCertificates, System.Security.Cryptography.X509Certificates.X509Certificate remoteCertificate, string[] acceptableIssuers);
	public abstract class MonoTlsProvider
	{
		public abstract Guid ID { get; }

		public abstract string Name { get; }

		public abstract bool SupportsSslStream { get; }

		public abstract bool SupportsConnectionInfo { get; }

		public abstract bool SupportsMonoExtensions { get; }

		public abstract SslProtocols SupportedProtocols { get; }

		internal virtual bool HasNativeCertificates => false;

		internal abstract bool SupportsCleanShutdown { get; }

		internal MonoTlsProvider()
		{
		}

		public abstract IMonoSslStream CreateSslStream(Stream innerStream, bool leaveInnerStreamOpen, MonoTlsSettings settings = null);
	}
	public static class MonoTlsProviderFactory
	{
		internal const int InternalVersion = 4;

		public static bool IsInitialized => NoReflectionHelper.IsInitialized;

		public static MonoTlsProvider GetProvider()
		{
			return (MonoTlsProvider)NoReflectionHelper.GetProvider();
		}

		public static void Initialize()
		{
			NoReflectionHelper.Initialize();
		}

		public static void Initialize(string provider)
		{
			NoReflectionHelper.Initialize(provider);
		}

		public static bool IsProviderSupported(string provider)
		{
			return NoReflectionHelper.IsProviderSupported(provider);
		}

		public static MonoTlsProvider GetProvider(string provider)
		{
			return (MonoTlsProvider)NoReflectionHelper.GetProvider(provider);
		}

		public static HttpWebRequest CreateHttpsRequest(Uri requestUri, MonoTlsProvider provider, MonoTlsSettings settings = null)
		{
			return NoReflectionHelper.CreateHttpsRequest(requestUri, provider, settings);
		}

		public static HttpListener CreateHttpListener(System.Security.Cryptography.X509Certificates.X509Certificate certificate, MonoTlsProvider provider = null, MonoTlsSettings settings = null)
		{
			return (HttpListener)NoReflectionHelper.CreateHttpListener(certificate, provider, settings);
		}

		public static IMonoSslStream GetMonoSslStream(SslStream stream)
		{
			return (IMonoSslStream)NoReflectionHelper.GetMonoSslStream(stream);
		}

		public static IMonoSslStream GetMonoSslStream(HttpListenerContext context)
		{
			return (IMonoSslStream)NoReflectionHelper.GetMonoSslStream(context);
		}
	}
	public sealed class MonoTlsSettings
	{
		private bool cloned;

		private bool checkCertName = true;

		private bool checkCertRevocationStatus;

		private bool? useServicePointManagerCallback;

		private bool skipSystemValidators;

		private bool callbackNeedsChain = true;

		private ICertificateValidator certificateValidator;

		private static MonoTlsSettings defaultSettings;

		public MonoRemoteCertificateValidationCallback RemoteCertificateValidationCallback { get; set; }

		public MonoLocalCertificateSelectionCallback ClientCertificateSelectionCallback { get; set; }

		public bool CheckCertificateName
		{
			get
			{
				return checkCertName;
			}
			set
			{
				checkCertName = value;
			}
		}

		public bool CheckCertificateRevocationStatus
		{
			get
			{
				return checkCertRevocationStatus;
			}
			set
			{
				checkCertRevocationStatus = value;
			}
		}

		public bool? UseServicePointManagerCallback
		{
			get
			{
				return useServicePointManagerCallback;
			}
			set
			{
				useServicePointManagerCallback = value;
			}
		}

		public bool SkipSystemValidators
		{
			get
			{
				return skipSystemValidators;
			}
			set
			{
				skipSystemValidators = value;
			}
		}

		public bool CallbackNeedsCertificateChain
		{
			get
			{
				return callbackNeedsChain;
			}
			set
			{
				callbackNeedsChain = value;
			}
		}

		public DateTime? CertificateValidationTime { get; set; }

		public System.Security.Cryptography.X509Certificates.X509CertificateCollection TrustAnchors { get; set; }

		public object UserSettings { get; set; }

		internal string[] CertificateSearchPaths { get; set; }

		internal bool SendCloseNotify { get; set; }

		public string[] ClientCertificateIssuers { get; set; }

		public bool DisallowUnauthenticatedCertificateRequest { get; set; }

		public TlsProtocols? EnabledProtocols { get; set; }

		[CLSCompliant(false)]
		public CipherSuiteCode[] EnabledCiphers { get; set; }

		public static MonoTlsSettings DefaultSettings
		{
			get
			{
				if (defaultSettings == null)
				{
					Interlocked.CompareExchange(ref defaultSettings, new MonoTlsSettings(), null);
				}
				return defaultSettings;
			}
			set
			{
				defaultSettings = value ?? new MonoTlsSettings();
			}
		}

		[Obsolete("Do not use outside System.dll!")]
		public ICertificateValidator CertificateValidator => certificateValidator;

		public MonoTlsSettings()
		{
		}

		public static MonoTlsSettings CopyDefaultSettings()
		{
			return DefaultSettings.Clone();
		}

		[Obsolete("Do not use outside System.dll!")]
		public MonoTlsSettings CloneWithValidator(ICertificateValidator validator)
		{
			if (cloned)
			{
				certificateValidator = validator;
				return this;
			}
			return new MonoTlsSettings(this)
			{
				certificateValidator = validator
			};
		}

		public MonoTlsSettings Clone()
		{
			return new MonoTlsSettings(this);
		}

		private MonoTlsSettings(MonoTlsSettings other)
		{
			RemoteCertificateValidationCallback = other.RemoteCertificateValidationCallback;
			ClientCertificateSelectionCallback = other.ClientCertificateSelectionCallback;
			checkCertName = other.checkCertName;
			checkCertRevocationStatus = other.checkCertRevocationStatus;
			UseServicePointManagerCallback = other.useServicePointManagerCallback;
			skipSystemValidators = other.skipSystemValidators;
			callbackNeedsChain = other.callbackNeedsChain;
			UserSettings = other.UserSettings;
			EnabledProtocols = other.EnabledProtocols;
			EnabledCiphers = other.EnabledCiphers;
			CertificateValidationTime = other.CertificateValidationTime;
			SendCloseNotify = other.SendCloseNotify;
			ClientCertificateIssuers = other.ClientCertificateIssuers;
			DisallowUnauthenticatedCertificateRequest = other.DisallowUnauthenticatedCertificateRequest;
			if (other.TrustAnchors != null)
			{
				TrustAnchors = new System.Security.Cryptography.X509Certificates.X509CertificateCollection(other.TrustAnchors);
			}
			if (other.CertificateSearchPaths != null)
			{
				CertificateSearchPaths = new string[other.CertificateSearchPaths.Length];
				other.CertificateSearchPaths.CopyTo(CertificateSearchPaths, 0);
			}
			cloned = true;
		}
	}
	public sealed class TlsException : Exception
	{
		private Alert alert;

		public Alert Alert => alert;

		public TlsException(Alert alert)
			: this(alert, alert.Description.ToString())
		{
		}

		public TlsException(Alert alert, string message)
			: base(message)
		{
			this.alert = alert;
		}

		public TlsException(AlertLevel level, AlertDescription description)
			: this(new Alert(level, description))
		{
		}

		public TlsException(AlertDescription description)
			: this(new Alert(description))
		{
		}

		public TlsException(AlertDescription description, string message)
			: this(new Alert(description), message)
		{
		}

		public TlsException(AlertDescription description, string format, params object[] args)
			: this(new Alert(description), string.Format(format, args))
		{
		}
	}
	public enum TlsProtocolCode : short
	{
		Tls10 = 769,
		Tls11,
		Tls12
	}
	[Flags]
	public enum TlsProtocols
	{
		Zero = 0,
		Tls10Client = 0x80,
		Tls10Server = 0x40,
		Tls10 = 0xC0,
		Tls11Client = 0x200,
		Tls11Server = 0x100,
		Tls11 = 0x300,
		Tls12Client = 0x800,
		Tls12Server = 0x400,
		Tls12 = 0xC00,
		ClientMask = 0xA80,
		ServerMask = 0x540
	}
}
namespace Mono.Security.Cryptography
{
	public class ARC4Managed : RC4, ICryptoTransform, IDisposable
	{
		private byte[] key;

		private byte[] state;

		private byte x;

		private byte y;

		private bool m_disposed;

		public override byte[] Key
		{
			get
			{
				if (KeyValue == null)
				{
					GenerateKey();
				}
				return (byte[])KeyValue.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Key");
				}
				KeyValue = (key = (byte[])value.Clone());
				KeySetup(key);
			}
		}

		public bool CanReuseTransform => false;

		public bool CanTransformMultipleBlocks => true;

		public int InputBlockSize => 1;

		public int OutputBlockSize => 1;

		public ARC4Managed()
		{
			state = new byte[256];
			m_disposed = false;
		}

		~ARC4Managed()
		{
			Dispose(disposing: true);
		}

		protected override void Dispose(bool disposing)
		{
			if (!m_disposed)
			{
				x = 0;
				y = 0;
				if (key != null)
				{
					Array.Clear(key, 0, key.Length);
					key = null;
				}
				Array.Clear(state, 0, state.Length);
				state = null;
				GC.SuppressFinalize(this);
				m_disposed = true;
			}
		}

		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgvIV)
		{
			Key = rgbKey;
			return this;
		}

		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgvIV)
		{
			Key = rgbKey;
			return CreateEncryptor();
		}

		public override void GenerateIV()
		{
			IV = new byte[0];
		}

		public override void GenerateKey()
		{
			KeyValue = KeyBuilder.Key(KeySizeValue >> 3);
		}

		private void KeySetup(byte[] key)
		{
			byte b = 0;
			byte b2 = 0;
			for (int i = 0; i < 256; i++)
			{
				state[i] = (byte)i;
			}
			x = 0;
			y = 0;
			for (int j = 0; j < 256; j++)
			{
				b2 = (byte)(key[b] + state[j] + b2);
				byte b3 = state[j];
				state[j] = state[b2];
				state[b2] = b3;
				b = (byte)((b + 1) % key.Length);
			}
		}

		private void CheckInput(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("inputOffset", "< 0");
			}
			if (inputCount < 0)
			{
				throw new ArgumentOutOfRangeException("inputCount", "< 0");
			}
			if (inputOffset > inputBuffer.Length - inputCount)
			{
				throw new ArgumentException(global::Locale.GetText("Overflow"), "inputBuffer");
			}
		}

		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			CheckInput(inputBuffer, inputOffset, inputCount);
			if (outputBuffer == null)
			{
				throw new ArgumentNullException("outputBuffer");
			}
			if (outputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("outputOffset", "< 0");
			}
			if (outputOffset > outputBuffer.Length - inputCount)
			{
				throw new ArgumentException(global::Locale.GetText("Overflow"), "outputBuffer");
			}
			return InternalTransformBlock(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset);
		}

		private int InternalTransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			for (int i = 0; i < inputCount; i++)
			{
				x++;
				y = (byte)(state[x] + y);
				byte b = state[x];
				state[x] = state[y];
				state[y] = b;
				byte b2 = (byte)(state[x] + state[y]);
				outputBuffer[outputOffset + i] = (byte)(inputBuffer[inputOffset + i] ^ state[b2]);
			}
			return inputCount;
		}

		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			CheckInput(inputBuffer, inputOffset, inputCount);
			byte[] array = new byte[inputCount];
			InternalTransformBlock(inputBuffer, inputOffset, inputCount, array, 0);
			return array;
		}
	}
	public sealed class CryptoConvert
	{
		private CryptoConvert()
		{
		}

		private static int ToInt32LE(byte[] bytes, int offset)
		{
			return (bytes[offset + 3] << 24) | (bytes[offset + 2] << 16) | (bytes[offset + 1] << 8) | bytes[offset];
		}

		private static uint ToUInt32LE(byte[] bytes, int offset)
		{
			return (uint)((bytes[offset + 3] << 24) | (bytes[offset + 2] << 16) | (bytes[offset + 1] << 8) | bytes[offset]);
		}

		private static byte[] GetBytesLE(int val)
		{
			return new byte[4]
			{
				(byte)(val & 0xFF),
				(byte)((val >> 8) & 0xFF),
				(byte)((val >> 16) & 0xFF),
				(byte)((val >> 24) & 0xFF)
			};
		}

		private static byte[] Trim(byte[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != 0)
				{
					byte[] array2 = new byte[array.Length - i];
					Buffer.BlockCopy(array, i, array2, 0, array2.Length);
					return array2;
				}
			}
			return null;
		}

		public static RSA FromCapiPrivateKeyBlob(byte[] blob)
		{
			return FromCapiPrivateKeyBlob(blob, 0);
		}

		public static RSA FromCapiPrivateKeyBlob(byte[] blob, int offset)
		{
			RSAParameters parametersFromCapiPrivateKeyBlob = GetParametersFromCapiPrivateKeyBlob(blob, offset);
			RSA rSA = null;
			try
			{
				rSA = RSA.Create();
				rSA.ImportParameters(parametersFromCapiPrivateKeyBlob);
			}
			catch (CryptographicException ex)
			{
				try
				{
					rSA = new RSACryptoServiceProvider(new CspParameters
					{
						Flags = CspProviderFlags.UseMachineKeyStore
					});
					rSA.ImportParameters(parametersFromCapiPrivateKeyBlob);
				}
				catch
				{
					throw ex;
				}
			}
			return rSA;
		}

		private static RSAParameters GetParametersFromCapiPrivateKeyBlob(byte[] blob, int offset)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (offset >= blob.Length)
			{
				throw new ArgumentException("blob is too small.");
			}
			RSAParameters result = default(RSAParameters);
			try
			{
				if (blob[offset] != 7 || blob[offset + 1] != 2 || blob[offset + 2] != 0 || blob[offset + 3] != 0 || ToUInt32LE(blob, offset + 8) != 843141970)
				{
					throw new CryptographicException("Invalid blob header");
				}
				int num = ToInt32LE(blob, offset + 12);
				byte[] array = new byte[4];
				Buffer.BlockCopy(blob, offset + 16, array, 0, 4);
				Array.Reverse(array);
				result.Exponent = Trim(array);
				int num2 = offset + 20;
				int num3 = num >> 3;
				result.Modulus = new byte[num3];
				Buffer.BlockCopy(blob, num2, result.Modulus, 0, num3);
				Array.Reverse(result.Modulus);
				num2 += num3;
				int num4 = num3 >> 1;
				result.P = new byte[num4];
				Buffer.BlockCopy(blob, num2, result.P, 0, num4);
				Array.Reverse(result.P);
				num2 += num4;
				result.Q = new byte[num4];
				Buffer.BlockCopy(blob, num2, result.Q, 0, num4);
				Array.Reverse(result.Q);
				num2 += num4;
				result.DP = new byte[num4];
				Buffer.BlockCopy(blob, num2, result.DP, 0, num4);
				Array.Reverse(result.DP);
				num2 += num4;
				result.DQ = new byte[num4];
				Buffer.BlockCopy(blob, num2, result.DQ, 0, num4);
				Array.Reverse(result.DQ);
				num2 += num4;
				result.InverseQ = new byte[num4];
				Buffer.BlockCopy(blob, num2, result.InverseQ, 0, num4);
				Array.Reverse(result.InverseQ);
				num2 += num4;
				result.D = new byte[num3];
				if (num2 + num3 + offset <= blob.Length)
				{
					Buffer.BlockCopy(blob, num2, result.D, 0, num3);
					Array.Reverse(result.D);
				}
				return result;
			}
			catch (Exception inner)
			{
				throw new CryptographicException("Invalid blob.", inner);
			}
		}

		public static DSA FromCapiPrivateKeyBlobDSA(byte[] blob)
		{
			return FromCapiPrivateKeyBlobDSA(blob, 0);
		}

		public static DSA FromCapiPrivateKeyBlobDSA(byte[] blob, int offset)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (offset >= blob.Length)
			{
				throw new ArgumentException("blob is too small.");
			}
			DSAParameters parameters = default(DSAParameters);
			try
			{
				if (blob[offset] != 7 || blob[offset + 1] != 2 || blob[offset + 2] != 0 || blob[offset + 3] != 0 || ToUInt32LE(blob, offset + 8) != 844321604)
				{
					throw new CryptographicException("Invalid blob header");
				}
				int num = ToInt32LE(blob, offset + 12) >> 3;
				int num2 = offset + 16;
				parameters.P = new byte[num];
				Buffer.BlockCopy(blob, num2, parameters.P, 0, num);
				Array.Reverse(parameters.P);
				num2 += num;
				parameters.Q = new byte[20];
				Buffer.BlockCopy(blob, num2, parameters.Q, 0, 20);
				Array.Reverse(parameters.Q);
				num2 += 20;
				parameters.G = new byte[num];
				Buffer.BlockCopy(blob, num2, parameters.G, 0, num);
				Array.Reverse(parameters.G);
				num2 += num;
				parameters.X = new byte[20];
				Buffer.BlockCopy(blob, num2, parameters.X, 0, 20);
				Array.Reverse(parameters.X);
				num2 += 20;
				parameters.Counter = ToInt32LE(blob, num2);
				num2 += 4;
				parameters.Seed = new byte[20];
				Buffer.BlockCopy(blob, num2, parameters.Seed, 0, 20);
				Array.Reverse(parameters.Seed);
				num2 += 20;
			}
			catch (Exception inner)
			{
				throw new CryptographicException("Invalid blob.", inner);
			}
			DSA dSA = null;
			try
			{
				dSA = DSA.Create();
				dSA.ImportParameters(parameters);
			}
			catch (CryptographicException ex)
			{
				try
				{
					dSA = new DSACryptoServiceProvider(new CspParameters
					{
						Flags = CspProviderFlags.UseMachineKeyStore
					});
					dSA.ImportParameters(parameters);
				}
				catch
				{
					throw ex;
				}
			}
			return dSA;
		}

		public static byte[] ToCapiPrivateKeyBlob(RSA rsa)
		{
			RSAParameters rSAParameters = rsa.ExportParameters(includePrivateParameters: true);
			int num = rSAParameters.Modulus.Length;
			byte[] array = new byte[20 + (num << 2) + (num >> 1)];
			array[0] = 7;
			array[1] = 2;
			array[5] = 36;
			array[8] = 82;
			array[9] = 83;
			array[10] = 65;
			array[11] = 50;
			byte[] bytesLE = GetBytesLE(num << 3);
			array[12] = bytesLE[0];
			array[13] = bytesLE[1];
			array[14] = bytesLE[2];
			array[15] = bytesLE[3];
			int num2 = 16;
			int num3 = rSAParameters.Exponent.Length;
			while (num3 > 0)
			{
				array[num2++] = rSAParameters.Exponent[--num3];
			}
			num2 = 20;
			byte[] modulus = rSAParameters.Modulus;
			int num4 = modulus.Length;
			Array.Reverse(modulus, 0, num4);
			Buffer.BlockCopy(modulus, 0, array, num2, num4);
			num2 += num4;
			byte[] p = rSAParameters.P;
			num4 = p.Length;
			Array.Reverse(p, 0, num4);
			Buffer.BlockCopy(p, 0, array, num2, num4);
			num2 += num4;
			byte[] q = rSAParameters.Q;
			num4 = q.Length;
			Array.Reverse(q, 0, num4);
			Buffer.BlockCopy(q, 0, array, num2, num4);
			num2 += num4;
			byte[] dP = rSAParameters.DP;
			num4 = dP.Length;
			Array.Reverse(dP, 0, num4);
			Buffer.BlockCopy(dP, 0, array, num2, num4);
			num2 += num4;
			byte[] dQ = rSAParameters.DQ;
			num4 = dQ.Length;
			Array.Reverse(dQ, 0, num4);
			Buffer.BlockCopy(dQ, 0, array, num2, num4);
			num2 += num4;
			byte[] inverseQ = rSAParameters.InverseQ;
			num4 = inverseQ.Length;
			Array.Reverse(inverseQ, 0, num4);
			Buffer.BlockCopy(inverseQ, 0, array, num2, num4);
			num2 += num4;
			byte[] d = rSAParameters.D;
			num4 = d.Length;
			Array.Reverse(d, 0, num4);
			Buffer.BlockCopy(d, 0, array, num2, num4);
			return array;
		}

		public static byte[] ToCapiPrivateKeyBlob(DSA dsa)
		{
			DSAParameters dSAParameters = dsa.ExportParameters(includePrivateParameters: true);
			int num = dSAParameters.P.Length;
			byte[] array = new byte[16 + num + 20 + num + 20 + 4 + 20];
			array[0] = 7;
			array[1] = 2;
			array[5] = 34;
			array[8] = 68;
			array[9] = 83;
			array[10] = 83;
			array[11] = 50;
			byte[] bytesLE = GetBytesLE(num << 3);
			array[12] = bytesLE[0];
			array[13] = bytesLE[1];
			array[14] = bytesLE[2];
			array[15] = bytesLE[3];
			int num2 = 16;
			byte[] p = dSAParameters.P;
			Array.Reverse(p);
			Buffer.BlockCopy(p, 0, array, num2, num);
			num2 += num;
			byte[] q = dSAParameters.Q;
			Array.Reverse(q);
			Buffer.BlockCopy(q, 0, array, num2, 20);
			num2 += 20;
			byte[] g = dSAParameters.G;
			Array.Reverse(g);
			Buffer.BlockCopy(g, 0, array, num2, num);
			num2 += num;
			byte[] x = dSAParameters.X;
			Array.Reverse(x);
			Buffer.BlockCopy(x, 0, array, num2, 20);
			num2 += 20;
			Buffer.BlockCopy(GetBytesLE(dSAParameters.Counter), 0, array, num2, 4);
			num2 += 4;
			byte[] seed = dSAParameters.Seed;
			Array.Reverse(seed);
			Buffer.BlockCopy(seed, 0, array, num2, 20);
			return array;
		}

		public static RSA FromCapiPublicKeyBlob(byte[] blob)
		{
			return FromCapiPublicKeyBlob(blob, 0);
		}

		public static RSA FromCapiPublicKeyBlob(byte[] blob, int offset)
		{
			RSAParameters parametersFromCapiPublicKeyBlob = GetParametersFromCapiPublicKeyBlob(blob, offset);
			try
			{
				RSA rSA = null;
				try
				{
					rSA = RSA.Create();
					rSA.ImportParameters(parametersFromCapiPublicKeyBlob);
				}
				catch (CryptographicException)
				{
					rSA = new RSACryptoServiceProvider(new CspParameters
					{
						Flags = CspProviderFlags.UseMachineKeyStore
					});
					rSA.ImportParameters(parametersFromCapiPublicKeyBlob);
				}
				return rSA;
			}
			catch (Exception inner)
			{
				throw new CryptographicException("Invalid blob.", inner);
			}
		}

		private static RSAParameters GetParametersFromCapiPublicKeyBlob(byte[] blob, int offset)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (offset >= blob.Length)
			{
				throw new ArgumentException("blob is too small.");
			}
			try
			{
				if (blob[offset] != 6 || blob[offset + 1] != 2 || blob[offset + 2] != 0 || blob[offset + 3] != 0 || ToUInt32LE(blob, offset + 8) != 826364754)
				{
					throw new CryptographicException("Invalid blob header");
				}
				int num = ToInt32LE(blob, offset + 12);
				RSAParameters result = new RSAParameters
				{
					Exponent = new byte[3]
				};
				result.Exponent[0] = blob[offset + 18];
				result.Exponent[1] = blob[offset + 17];
				result.Exponent[2] = blob[offset + 16];
				int srcOffset = offset + 20;
				int num2 = num >> 3;
				result.Modulus = new byte[num2];
				Buffer.BlockCopy(blob, srcOffset, result.Modulus, 0, num2);
				Array.Reverse(result.Modulus);
				return result;
			}
			catch (Exception inner)
			{
				throw new CryptographicException("Invalid blob.", inner);
			}
		}

		public static DSA FromCapiPublicKeyBlobDSA(byte[] blob)
		{
			return FromCapiPublicKeyBlobDSA(blob, 0);
		}

		public static DSA FromCapiPublicKeyBlobDSA(byte[] blob, int offset)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (offset >= blob.Length)
			{
				throw new ArgumentException("blob is too small.");
			}
			try
			{
				if (blob[offset] != 6 || blob[offset + 1] != 2 || blob[offset + 2] != 0 || blob[offset + 3] != 0 || ToUInt32LE(blob, offset + 8) != 827544388)
				{
					throw new CryptographicException("Invalid blob header");
				}
				int num = ToInt32LE(blob, offset + 12);
				DSAParameters parameters = default(DSAParameters);
				int num2 = num >> 3;
				int num3 = offset + 16;
				parameters.P = new byte[num2];
				Buffer.BlockCopy(blob, num3, parameters.P, 0, num2);
				Array.Reverse(parameters.P);
				num3 += num2;
				parameters.Q = new byte[20];
				Buffer.BlockCopy(blob, num3, parameters.Q, 0, 20);
				Array.Reverse(parameters.Q);
				num3 += 20;
				parameters.G = new byte[num2];
				Buffer.BlockCopy(blob, num3, parameters.G, 0, num2);
				Array.Reverse(parameters.G);
				num3 += num2;
				parameters.Y = new byte[num2];
				Buffer.BlockCopy(blob, num3, parameters.Y, 0, num2);
				Array.Reverse(parameters.Y);
				num3 += num2;
				parameters.Counter = ToInt32LE(blob, num3);
				num3 += 4;
				parameters.Seed = new byte[20];
				Buffer.BlockCopy(blob, num3, parameters.Seed, 0, 20);
				Array.Reverse(parameters.Seed);
				num3 += 20;
				DSA dSA = DSA.Create();
				dSA.ImportParameters(parameters);
				return dSA;
			}
			catch (Exception inner)
			{
				throw new CryptographicException("Invalid blob.", inner);
			}
		}

		public static byte[] ToCapiPublicKeyBlob(RSA rsa)
		{
			RSAParameters rSAParameters = rsa.ExportParameters(includePrivateParameters: false);
			int num = rSAParameters.Modulus.Length;
			byte[] array = new byte[20 + num];
			array[0] = 6;
			array[1] = 2;
			array[5] = 36;
			array[8] = 82;
			array[9] = 83;
			array[10] = 65;
			array[11] = 49;
			byte[] bytesLE = GetBytesLE(num << 3);
			array[12] = bytesLE[0];
			array[13] = bytesLE[1];
			array[14] = bytesLE[2];
			array[15] = bytesLE[3];
			int num2 = 16;
			int num3 = rSAParameters.Exponent.Length;
			while (num3 > 0)
			{
				array[num2++] = rSAParameters.Exponent[--num3];
			}
			num2 = 20;
			byte[] modulus = rSAParameters.Modulus;
			int num4 = modulus.Length;
			Array.Reverse(modulus, 0, num4);
			Buffer.BlockCopy(modulus, 0, array, num2, num4);
			num2 += num4;
			return array;
		}

		public static byte[] ToCapiPublicKeyBlob(DSA dsa)
		{
			DSAParameters dSAParameters = dsa.ExportParameters(includePrivateParameters: false);
			int num = dSAParameters.P.Length;
			byte[] array = new byte[16 + num + 20 + num + num + 4 + 20];
			array[0] = 6;
			array[1] = 2;
			array[5] = 34;
			array[8] = 68;
			array[9] = 83;
			array[10] = 83;
			array[11] = 49;
			byte[] bytesLE = GetBytesLE(num << 3);
			array[12] = bytesLE[0];
			array[13] = bytesLE[1];
			array[14] = bytesLE[2];
			array[15] = bytesLE[3];
			int num2 = 16;
			byte[] p = dSAParameters.P;
			Array.Reverse(p);
			Buffer.BlockCopy(p, 0, array, num2, num);
			num2 += num;
			byte[] q = dSAParameters.Q;
			Array.Reverse(q);
			Buffer.BlockCopy(q, 0, array, num2, 20);
			num2 += 20;
			byte[] g = dSAParameters.G;
			Array.Reverse(g);
			Buffer.BlockCopy(g, 0, array, num2, num);
			num2 += num;
			byte[] y = dSAParameters.Y;
			Array.Reverse(y);
			Buffer.BlockCopy(y, 0, array, num2, num);
			num2 += num;
			Buffer.BlockCopy(GetBytesLE(dSAParameters.Counter), 0, array, num2, 4);
			num2 += 4;
			byte[] seed = dSAParameters.Seed;
			Array.Reverse(seed);
			Buffer.BlockCopy(seed, 0, array, num2, 20);
			return array;
		}

		public static RSA FromCapiKeyBlob(byte[] blob)
		{
			return FromCapiKeyBlob(blob, 0);
		}

		public static RSA FromCapiKeyBlob(byte[] blob, int offset)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (offset >= blob.Length)
			{
				throw new ArgumentException("blob is too small.");
			}
			switch (blob[offset])
			{
			case 0:
				if (blob[offset + 12] == 6)
				{
					return FromCapiPublicKeyBlob(blob, offset + 12);
				}
				break;
			case 6:
				return FromCapiPublicKeyBlob(blob, offset);
			case 7:
				return FromCapiPrivateKeyBlob(blob, offset);
			}
			throw new CryptographicException("Unknown blob format.");
		}

		public static DSA FromCapiKeyBlobDSA(byte[] blob)
		{
			return FromCapiKeyBlobDSA(blob, 0);
		}

		public static DSA FromCapiKeyBlobDSA(byte[] blob, int offset)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (offset >= blob.Length)
			{
				throw new ArgumentException("blob is too small.");
			}
			return blob[offset] switch
			{
				6 => FromCapiPublicKeyBlobDSA(blob, offset), 
				7 => FromCapiPrivateKeyBlobDSA(blob, offset), 
				_ => throw new CryptographicException("Unknown blob format."), 
			};
		}

		public static byte[] ToCapiKeyBlob(AsymmetricAlgorithm keypair, bool includePrivateKey)
		{
			if (keypair == null)
			{
				throw new ArgumentNullException("keypair");
			}
			if (keypair is RSA)
			{
				return ToCapiKeyBlob((RSA)keypair, includePrivateKey);
			}
			if (keypair is DSA)
			{
				return ToCapiKeyBlob((DSA)keypair, includePrivateKey);
			}
			return null;
		}

		public static byte[] ToCapiKeyBlob(RSA rsa, bool includePrivateKey)
		{
			if (rsa == null)
			{
				throw new ArgumentNullException("rsa");
			}
			if (includePrivateKey)
			{
				return ToCapiPrivateKeyBlob(rsa);
			}
			return ToCapiPublicKeyBlob(rsa);
		}

		public static byte[] ToCapiKeyBlob(DSA dsa, bool includePrivateKey)
		{
			if (dsa == null)
			{
				throw new ArgumentNullException("dsa");
			}
			if (includePrivateKey)
			{
				return ToCapiPrivateKeyBlob(dsa);
			}
			return ToCapiPublicKeyBlob(dsa);
		}

		public static string ToHex(byte[] input)
		{
			if (input == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(input.Length * 2);
			foreach (byte b in input)
			{
				stringBuilder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}

		private static byte FromHexChar(char c)
		{
			if (c >= 'a' && c <= 'f')
			{
				return (byte)(c - 97 + 10);
			}
			if (c >= 'A' && c <= 'F')
			{
				return (byte)(c - 65 + 10);
			}
			if (c >= '0' && c <= '9')
			{
				return (byte)(c - 48);
			}
			throw new ArgumentException("invalid hex char");
		}

		public static byte[] FromHex(string hex)
		{
			if (hex == null)
			{
				return null;
			}
			if ((hex.Length & 1) == 1)
			{
				throw new ArgumentException("Length must be a multiple of 2");
			}
			byte[] array = new byte[hex.Length >> 1];
			int num = 0;
			int num2 = 0;
			while (num < array.Length)
			{
				array[num] = (byte)(FromHexChar(hex[num2++]) << 4);
				array[num++] += FromHexChar(hex[num2++]);
			}
			return array;
		}
	}
	public sealed class KeyBuilder
	{
		private static RandomNumberGenerator rng;

		private static RandomNumberGenerator Rng
		{
			get
			{
				if (rng == null)
				{
					rng = RandomNumberGenerator.Create();
				}
				return rng;
			}
		}

		private KeyBuilder()
		{
		}

		public static byte[] Key(int size)
		{
			byte[] array = new byte[size];
			Rng.GetBytes(array);
			return array;
		}

		public static byte[] IV(int size)
		{
			byte[] array = new byte[size];
			Rng.GetBytes(array);
			return array;
		}
	}
	public class BlockProcessor
	{
		private ICryptoTransform transform;

		private byte[] block;

		private int blockSize;

		private int blockCount;

		public BlockProcessor(ICryptoTransform transform)
			: this(transform, transform.InputBlockSize)
		{
		}

		public BlockProcessor(ICryptoTransform transform, int blockSize)
		{
			if (transform == null)
			{
				throw new ArgumentNullException("transform");
			}
			if (blockSize <= 0)
			{
				throw new ArgumentOutOfRangeException("blockSize");
			}
			this.transform = transform;
			this.blockSize = blockSize;
			block = new byte[blockSize];
		}

		~BlockProcessor()
		{
			Array.Clear(block, 0, blockSize);
		}

		public void Initialize()
		{
			Array.Clear(block, 0, blockSize);
			blockCount = 0;
		}

		public void Core(byte[] rgb)
		{
			Core(rgb, 0, rgb.Length);
		}

		public void Core(byte[] rgb, int ib, int cb)
		{
			int num = System.Math.Min(blockSize - blockCount, cb);
			Buffer.BlockCopy(rgb, ib, block, blockCount, num);
			blockCount += num;
			if (blockCount == blockSize)
			{
				transform.TransformBlock(block, 0, blockSize, block, 0);
				int num2 = (cb - num) / blockSize;
				for (int i = 0; i < num2; i++)
				{
					transform.TransformBlock(rgb, num + ib, blockSize, block, 0);
					num += blockSize;
				}
				blockCount = cb - num;
				if (blockCount > 0)
				{
					Buffer.BlockCopy(rgb, num + ib, block, 0, blockCount);
				}
			}
		}

		public byte[] Final()
		{
			return transform.TransformFinalBlock(block, 0, blockCount);
		}
	}
	public enum DHKeyGeneration
	{
		Random,
		Static
	}
	[Serializable]
	public struct DHParameters
	{
		public byte[] P;

		public byte[] G;

		[NonSerialized]
		public byte[] X;
	}
	public abstract class DiffieHellman : AsymmetricAlgorithm
	{
		public new static DiffieHellman Create()
		{
			return Create("Mono.Security.Cryptography.DiffieHellman");
		}

		public new static DiffieHellman Create(string algName)
		{
			return (DiffieHellman)CryptoConfig.CreateFromName(algName);
		}

		public abstract byte[] CreateKeyExchange();

		public abstract byte[] DecryptKeyExchange(byte[] keyex);

		public abstract DHParameters ExportParameters(bool includePrivate);

		public abstract void ImportParameters(DHParameters parameters);

		private byte[] GetNamedParam(SecurityElement se, string param)
		{
			SecurityElement securityElement = se.SearchForChildByTag(param);
			if (securityElement == null)
			{
				return null;
			}
			return Convert.FromBase64String(securityElement.Text);
		}

		public override void FromXmlString(string xmlString)
		{
			if (xmlString == null)
			{
				throw new ArgumentNullException("xmlString");
			}
			DHParameters parameters = default(DHParameters);
			try
			{
				SecurityParser securityParser = new SecurityParser();
				securityParser.LoadXml(xmlString);
				SecurityElement securityElement = securityParser.ToXml();
				if (securityElement.Tag != "DHKeyValue")
				{
					throw new CryptographicException();
				}
				parameters.P = GetNamedParam(securityElement, "P");
				parameters.G = GetNamedParam(securityElement, "G");
				parameters.X = GetNamedParam(securityElement, "X");
				ImportParameters(parameters);
			}
			finally
			{
				if (parameters.P != null)
				{
					Array.Clear(parameters.P, 0, parameters.P.Length);
				}
				if (parameters.G != null)
				{
					Array.Clear(parameters.G, 0, parameters.G.Length);
				}
				if (parameters.X != null)
				{
					Array.Clear(parameters.X, 0, parameters.X.Length);
				}
			}
		}

		public override string ToXmlString(bool includePrivateParameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			DHParameters dHParameters = ExportParameters(includePrivateParameters);
			try
			{
				stringBuilder.Append("<DHKeyValue>");
				stringBuilder.Append("<P>");
				stringBuilder.Append(Convert.ToBase64String(dHParameters.P));
				stringBuilder.Append("</P>");
				stringBuilder.Append("<G>");
				stringBuilder.Append(Convert.ToBase64String(dHParameters.G));
				stringBuilder.Append("</G>");
				if (includePrivateParameters)
				{
					stringBuilder.Append("<X>");
					stringBuilder.Append(Convert.ToBase64String(dHParameters.X));
					stringBuilder.Append("</X>");
				}
				stringBuilder.Append("</DHKeyValue>");
			}
			finally
			{
				Array.Clear(dHParameters.P, 0, dHParameters.P.Length);
				Array.Clear(dHParameters.G, 0, dHParameters.G.Length);
				if (dHParameters.X != null)
				{
					Array.Clear(dHParameters.X, 0, dHParameters.X.Length);
				}
			}
			return stringBuilder.ToString();
		}
	}
	public sealed class DiffieHellmanManaged : DiffieHellman
	{
		private BigInteger m_P;

		private BigInteger m_G;

		private BigInteger m_X;

		private bool m_Disposed;

		private static byte[] m_OAKLEY768 = new byte[96]
		{
			255, 255, 255, 255, 255, 255, 255, 255, 201, 15,
			218, 162, 33, 104, 194, 52, 196, 198, 98, 139,
			128, 220, 28, 209, 41, 2, 78, 8, 138, 103,
			204, 116, 2, 11, 190, 166, 59, 19, 155, 34,
			81, 74, 8, 121, 142, 52, 4, 221, 239, 149,
			25, 179, 205, 58, 67, 27, 48, 43, 10, 109,
			242, 95, 20, 55, 79, 225, 53, 109, 109, 81,
			194, 69, 228, 133, 181, 118, 98, 94, 126, 198,
			244, 76, 66, 233, 166, 58, 54, 32, 255, 255,
			255, 255, 255, 255, 255, 255
		};

		private static byte[] m_OAKLEY1024 = new byte[128]
		{
			255, 255, 255, 255, 255, 255, 255, 255, 201, 15,
			218, 162, 33, 104, 194, 52, 196, 198, 98, 139,
			128, 220, 28, 209, 41, 2, 78, 8, 138, 103,
			204, 116, 2, 11, 190, 166, 59, 19, 155, 34,
			81, 74, 8, 121, 142, 52, 4, 221, 239, 149,
			25, 179, 205, 58, 67, 27, 48, 43, 10, 109,
			242, 95, 20, 55, 79, 225, 53, 109, 109, 81,
			194, 69, 228, 133, 181, 118, 98, 94, 126, 198,
			244, 76, 66, 233, 166, 55, 237, 107, 11, 255,
			92, 182, 244, 6, 183, 237, 238, 56, 107, 251,
			90, 137, 159, 165, 174, 159, 36, 17, 124, 75,
			31, 230, 73, 40, 102, 81, 236, 230, 83, 129,
			255, 255, 255, 255, 255, 255, 255, 255
		};

		private static byte[] m_OAKLEY1536 = new byte[192]
		{
			255, 255, 255, 255, 255, 255, 255, 255, 201, 15,
			218, 162, 33, 104, 194, 52, 196, 198, 98, 139,
			128, 220, 28, 209, 41, 2, 78, 8, 138, 103,
			204, 116, 2, 11, 190, 166, 59, 19, 155, 34,
			81, 74, 8, 121, 142, 52, 4, 221, 239, 149,
			25, 179, 205, 58, 67, 27, 48, 43, 10, 109,
			242, 95, 20, 55, 79, 225, 53, 109, 109, 81,
			194, 69, 228, 133, 181, 118, 98, 94, 126, 198,
			244, 76, 66, 233, 166, 55, 237, 107, 11, 255,
			92, 182, 244, 6, 183, 237, 238, 56, 107, 251,
			90, 137, 159, 165, 174, 159, 36, 17, 124, 75,
			31, 230, 73, 40, 102, 81, 236, 228, 91, 61,
			194, 0, 124, 184, 161, 99, 191, 5, 152, 218,
			72, 54, 28, 85, 211, 154, 105, 22, 63, 168,
			253, 36, 207, 95, 131, 101, 93, 35, 220, 163,
			173, 150, 28, 98, 243, 86, 32, 133, 82, 187,
			158, 213, 41, 7, 112, 150, 150, 109, 103, 12,
			53, 78, 74, 188, 152, 4, 241, 116, 108, 8,
			202, 35, 115, 39, 255, 255, 255, 255, 255, 255,
			255, 255
		};

		public override string KeyExchangeAlgorithm => "1.2.840.113549.1.3";

		public override string SignatureAlgorithm => null;

		public DiffieHellmanManaged()
			: this(1024, 160, DHKeyGeneration.Static)
		{
		}

		public DiffieHellmanManaged(int bitLength, int l, DHKeyGeneration method)
		{
			if (bitLength < 256 || l < 0)
			{
				throw new ArgumentException();
			}
			GenerateKey(bitLength, method, out var p, out var g);
			Initialize(p, g, null, l, checkInput: false);
		}

		public DiffieHellmanManaged(byte[] p, byte[] g, byte[] x)
		{
			if (p == null || g == null)
			{
				throw new ArgumentNullException();
			}
			if (x == null)
			{
				Initialize(new BigInteger(p), new BigInteger(g), null, 0, checkInput: true);
			}
			else
			{
				Initialize(new BigInteger(p), new BigInteger(g), new BigInteger(x), 0, checkInput: true);
			}
		}

		public DiffieHellmanManaged(byte[] p, byte[] g, int l)
		{
			if (p == null || g == null)
			{
				throw new ArgumentNullException();
			}
			if (l < 0)
			{
				throw new ArgumentException();
			}
			Initialize(new BigInteger(p), new BigInteger(g), null, l, checkInput: true);
		}

		private void Initialize(BigInteger p, BigInteger g, BigInteger x, int secretLen, bool checkInput)
		{
			if (checkInput && (!p.IsProbablePrime() || g <= 0 || g >= p || (x != null && (x <= 0 || x > p - 2))))
			{
				throw new CryptographicException();
			}
			if (secretLen == 0)
			{
				secretLen = p.BitCount();
			}
			m_P = p;
			m_G = g;
			if (x == null)
			{
				BigInteger bigInteger = m_P - 1;
				m_X = BigInteger.GenerateRandom(secretLen);
				while (m_X >= bigInteger || m_X == 0u)
				{
					m_X = BigInteger.GenerateRandom(secretLen);
				}
			}
			else
			{
				m_X = x;
			}
		}

		public override byte[] CreateKeyExchange()
		{
			BigInteger bigInteger = m_G.ModPow(m_X, m_P);
			byte[] bytes = bigInteger.GetBytes();
			bigInteger.Clear();
			return bytes;
		}

		public override byte[] DecryptKeyExchange(byte[] keyEx)
		{
			BigInteger bigInteger = new BigInteger(keyEx).ModPow(m_X, m_P);
			byte[] bytes = bigInteger.GetBytes();
			bigInteger.Clear();
			return bytes;
		}

		protected override void Dispose(bool disposing)
		{
			if (!m_Disposed)
			{
				if (m_P != null)
				{
					m_P.Clear();
				}
				if (m_G != null)
				{
					m_G.Clear();
				}
				if (m_X != null)
				{
					m_X.Clear();
				}
			}
			m_Disposed = true;
		}

		public override DHParameters ExportParameters(bool includePrivateParameters)
		{
			DHParameters result = new DHParameters
			{
				P = m_P.GetBytes(),
				G = m_G.GetBytes()
			};
			if (includePrivateParameters)
			{
				result.X = m_X.GetBytes();
			}
			return result;
		}

		public override void ImportParameters(DHParameters parameters)
		{
			if (parameters.P == null)
			{
				throw new CryptographicException("Missing P value.");
			}
			if (parameters.G == null)
			{
				throw new CryptographicException("Missing G value.");
			}
			BigInteger p = new BigInteger(parameters.P);
			BigInteger g = new BigInteger(parameters.G);
			BigInteger x = null;
			if (parameters.X != null)
			{
				x = new BigInteger(parameters.X);
			}
			Initialize(p, g, x, 0, checkInput: true);
		}

		~DiffieHellmanManaged()
		{
			Dispose(disposing: false);
		}

		private void GenerateKey(int bitlen, DHKeyGeneration keygen, out BigInteger p, out BigInteger g)
		{
			if (keygen == DHKeyGeneration.Static)
			{
				switch (bitlen)
				{
				case 768:
					p = new BigInteger(m_OAKLEY768);
					break;
				case 1024:
					p = new BigInteger(m_OAKLEY1024);
					break;
				case 1536:
					p = new BigInteger(m_OAKLEY1536);
					break;
				default:
					throw new ArgumentException("Invalid bit size.");
				}
				g = new BigInteger(22u);
			}
			else
			{
				p = BigInteger.GeneratePseudoPrime(bitlen);
				g = new BigInteger(3u);
			}
		}
	}
	public class KeyPairPersistence
	{
		private static bool _userPathExists;

		private static string _userPath;

		private static bool _machinePathExists;

		private static string _machinePath;

		private CspParameters _params;

		private string _keyvalue;

		private string _filename;

		private string _container;

		private static object lockobj = new object();

		public string Filename
		{
			get
			{
				if (_filename == null)
				{
					_filename = string.Format(CultureInfo.InvariantCulture, "[{0}][{1}][{2}].xml", _params.ProviderType, ContainerName, _params.KeyNumber);
					if (UseMachineKeyStore)
					{
						_filename = Path.Combine(MachinePath, _filename);
					}
					else
					{
						_filename = Path.Combine(UserPath, _filename);
					}
				}
				return _filename;
			}
		}

		public string KeyValue
		{
			get
			{
				return _keyvalue;
			}
			set
			{
				if (CanChange)
				{
					_keyvalue = value;
				}
			}
		}

		public CspParameters Parameters => Copy(_params);

		private static string UserPath
		{
			get
			{
				lock (lockobj)
				{
					if (_userPath == null || !_userPathExists)
					{
						_userPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".mono");
						_userPath = Path.Combine(_userPath, "keypairs");
						_userPathExists = Directory.Exists(_userPath);
						if (!_userPathExists)
						{
							try
							{
								Directory.CreateDirectory(_userPath);
							}
							catch (Exception inner)
							{
								throw new CryptographicException(string.Format(global::Locale.GetText("Could not create user key store '{0}'."), _userPath), inner);
							}
							_userPathExists = true;
						}
					}
					if (!IsUserProtected(_userPath) && !ProtectUser(_userPath))
					{
						throw new IOException(string.Format(global::Locale.GetText("Could not secure user key store '{0}'."), _userPath));
					}
				}
				if (!IsUserProtected(_userPath))
				{
					throw new CryptographicException(string.Format(global::Locale.GetText("Improperly protected user's key pairs in '{0}'."), _userPath));
				}
				return _userPath;
			}
		}

		private static string MachinePath
		{
			get
			{
				lock (lockobj)
				{
					if (_machinePath == null || !_machinePathExists)
					{
						_machinePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), ".mono");
						_machinePath = Path.Combine(_machinePath, "keypairs");
						_machinePathExists = Directory.Exists(_machinePath);
						if (!_machinePathExists)
						{
							try
							{
								Directory.CreateDirectory(_machinePath);
							}
							catch (Exception inner)
							{
								throw new CryptographicException(string.Format(global::Locale.GetText("Could not create machine key store '{0}'."), _machinePath), inner);
							}
							_machinePathExists = true;
						}
					}
					if (!IsMachineProtected(_machinePath) && !ProtectMachine(_machinePath))
					{
						throw new IOException(string.Format(global::Locale.GetText("Could not secure machine key store '{0}'."), _machinePath));
					}
				}
				if (!IsMachineProtected(_machinePath))
				{
					throw new CryptographicException(string.Format(global::Locale.GetText("Improperly protected machine's key pairs in '{0}'."), _machinePath));
				}
				return _machinePath;
			}
		}

		private bool CanChange => _keyvalue == null;

		private bool UseDefaultKeyContainer => (_params.Flags & CspProviderFlags.UseDefaultKeyContainer) == CspProviderFlags.UseDefaultKeyContainer;

		private bool UseMachineKeyStore => (_params.Flags & CspProviderFlags.UseMachineKeyStore) == CspProviderFlags.UseMachineKeyStore;

		private string ContainerName
		{
			get
			{
				if (_container == null)
				{
					if (UseDefaultKeyContainer)
					{
						_container = "default";
					}
					else if (_params.KeyContainerName == null || _params.KeyContainerName.Length == 0)
					{
						_container = Guid.NewGuid().ToString();
					}
					else
					{
						byte[] bytes = Encoding.UTF8.GetBytes(_params.KeyContainerName);
						byte[] b = MD5.Create().ComputeHash(bytes);
						_container = new Guid(b).ToString();
					}
				}
				return _container;
			}
		}

		public KeyPairPersistence(CspParameters parameters)
			: this(parameters, null)
		{
		}

		public KeyPairPersistence(CspParameters parameters, string keyPair)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			_params = Copy(parameters);
			_keyvalue = keyPair;
		}

		public bool Load()
		{
			bool flag = File.Exists(Filename);
			if (flag)
			{
				using StreamReader streamReader = File.OpenText(Filename);
				FromXml(streamReader.ReadToEnd());
			}
			return flag;
		}

		public void Save()
		{
			using (FileStream stream = File.Open(Filename, FileMode.Create))
			{
				StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8);
				streamWriter.Write(ToXml());
				streamWriter.Close();
			}
			if (UseMachineKeyStore)
			{
				ProtectMachine(Filename);
			}
			else
			{
				ProtectUser(Filename);
			}
		}

		public void Remove()
		{
			File.Delete(Filename);
		}

		internal unsafe static bool _CanSecure(char* root)
		{
			return true;
		}

		internal unsafe static bool _ProtectUser(char* path)
		{
			return true;
		}

		internal unsafe static bool _ProtectMachine(char* path)
		{
			return true;
		}

		internal unsafe static bool _IsUserProtected(char* path)
		{
			return true;
		}

		internal unsafe static bool _IsMachineProtected(char* path)
		{
			return true;
		}

		private unsafe static bool CanSecure(string path)
		{
			int platform = (int)Environment.OSVersion.Platform;
			if (platform == 4 || platform == 128 || platform == 6)
			{
				return true;
			}
			fixed (char* root = path)
			{
				return _CanSecure(root);
			}
		}

		private unsafe static bool ProtectUser(string path)
		{
			if (CanSecure(path))
			{
				fixed (char* path2 = path)
				{
					return _ProtectUser(path2);
				}
			}
			return true;
		}

		private unsafe static bool ProtectMachine(string path)
		{
			if (CanSecure(path))
			{
				fixed (char* path2 = path)
				{
					return _ProtectMachine(path2);
				}
			}
			return true;
		}

		private unsafe static bool IsUserProtected(string path)
		{
			if (CanSecure(path))
			{
				fixed (char* path2 = path)
				{
					return _IsUserProtected(path2);
				}
			}
			return true;
		}

		private unsafe static bool IsMachineProtected(string path)
		{
			if (CanSecure(path))
			{
				fixed (char* path2 = path)
				{
					return _IsMachineProtected(path2);
				}
			}
			return true;
		}

		private CspParameters Copy(CspParameters p)
		{
			return new CspParameters(p.ProviderType, p.ProviderName, p.KeyContainerName)
			{
				KeyNumber = p.KeyNumber,
				Flags = p.Flags
			};
		}

		private void FromXml(string xml)
		{
			SecurityParser securityParser = new SecurityParser();
			securityParser.LoadXml(xml);
			SecurityElement securityElement = securityParser.ToXml();
			if (securityElement.Tag == "KeyPair")
			{
				SecurityElement securityElement2 = securityElement.SearchForChildByTag("KeyValue");
				if (securityElement2.Children.Count > 0)
				{
					_keyvalue = securityElement2.Children[0].ToString();
				}
			}
		}

		private string ToXml()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("<KeyPair>{0}\t<Properties>{0}\t\t<Provider ", Environment.NewLine);
			if (_params.ProviderName != null && _params.ProviderName.Length != 0)
			{
				stringBuilder.AppendFormat("Name=\"{0}\" ", _params.ProviderName);
			}
			stringBuilder.AppendFormat("Type=\"{0}\" />{1}\t\t<Container ", _params.ProviderType, Environment.NewLine);
			stringBuilder.AppendFormat("Name=\"{0}\" />{1}\t</Properties>{1}\t<KeyValue", ContainerName, Environment.NewLine);
			if (_params.KeyNumber != -1)
			{
				stringBuilder.AppendFormat(" Id=\"{0}\" ", _params.KeyNumber);
			}
			stringBuilder.AppendFormat(">{1}\t\t{0}{1}\t</KeyValue>{1}</KeyPair>{1}", KeyValue, Environment.NewLine);
			return stringBuilder.ToString();
		}
	}
	public abstract class MD2 : HashAlgorithm
	{
		protected MD2()
		{
			HashSizeValue = 128;
		}

		public new static MD2 Create()
		{
			return Create("MD2");
		}

		public new static MD2 Create(string hashName)
		{
			object obj = CryptoConfig.CreateFromName(hashName);
			if (obj == null)
			{
				obj = new MD2Managed();
			}
			return (MD2)obj;
		}
	}
	public class MD2Managed : MD2
	{
		private byte[] state;

		private byte[] checksum;

		private byte[] buffer;

		private int count;

		private byte[] x;

		private static readonly byte[] PI_SUBST = new byte[256]
		{
			41, 46, 67, 201, 162, 216, 124, 1, 61, 54,
			84, 161, 236, 240, 6, 19, 98, 167, 5, 243,
			192, 199, 115, 140, 152, 147, 43, 217, 188, 76,
			130, 202, 30, 155, 87, 60, 253, 212, 224, 22,
			103, 66, 111, 24, 138, 23, 229, 18, 190, 78,
			196, 214, 218, 158, 222, 73, 160, 251, 245, 142,
			187, 47, 238, 122, 169, 104, 121, 145, 21, 178,
			7, 63, 148, 194, 16, 137, 11, 34, 95, 33,
			128, 127, 93, 154, 90, 144, 50, 39, 53, 62,
			204, 231, 191, 247, 151, 3, 255, 25, 48, 179,
			72, 165, 181, 209, 215, 94, 146, 42, 172, 86,
			170, 198, 79, 184, 56, 210, 150, 164, 125, 182,
			118, 252, 107, 226, 156, 116, 4, 241, 69, 157,
			112, 89, 100, 113, 135, 32, 134, 91, 207, 101,
			230, 45, 168, 2, 27, 96, 37, 173, 174, 176,
			185, 246, 28, 70, 97, 105, 52, 64, 126, 15,
			85, 71, 163, 35, 221, 81, 175, 58, 195, 92,
			249, 206, 186, 197, 234, 38, 44, 83, 13, 110,
			133, 40, 132, 9, 211, 223, 205, 244, 65, 129,
			77, 82, 106, 220, 55, 200, 108, 193, 171, 250,
			36, 225, 123, 8, 12, 189, 177, 74, 120, 136,
			149, 139, 227, 99, 232, 109, 233, 203, 213, 254,
			59, 0, 29, 57, 242, 239, 183, 14, 102, 88,
			208, 228, 166, 119, 114, 248, 235, 117, 75, 10,
			49, 68, 80, 180, 143, 237, 31, 26, 219, 153,
			141, 51, 159, 17, 131, 20
		};

		private byte[] Padding(int nLength)
		{
			if (nLength > 0)
			{
				byte[] array = new byte[nLength];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (byte)nLength;
				}
				return array;
			}
			return null;
		}

		public MD2Managed()
		{
			state = new byte[16];
			checksum = new byte[16];
			buffer = new byte[16];
			x = new byte[48];
			Initialize();
		}

		public override void Initialize()
		{
			count = 0;
			Array.Clear(state, 0, 16);
			Array.Clear(checksum, 0, 16);
			Array.Clear(buffer, 0, 16);
			Array.Clear(x, 0, 48);
		}

		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			int num = count;
			count = (num + cbSize) & 0xF;
			int num2 = 16 - num;
			int i;
			if (cbSize >= num2)
			{
				Buffer.BlockCopy(array, ibStart, buffer, num, num2);
				MD2Transform(state, checksum, buffer, 0);
				for (i = num2; i + 15 < cbSize; i += 16)
				{
					MD2Transform(state, checksum, array, ibStart + i);
				}
				num = 0;
			}
			else
			{
				i = 0;
			}
			Buffer.BlockCopy(array, ibStart + i, buffer, num, cbSize - i);
		}

		protected override byte[] HashFinal()
		{
			int num = count;
			int num2 = 16 - num;
			if (num2 > 0)
			{
				HashCore(Padding(num2), 0, num2);
			}
			HashCore(checksum, 0, 16);
			byte[] result = (byte[])state.Clone();
			Initialize();
			return result;
		}

		private void MD2Transform(byte[] state, byte[] checksum, byte[] block, int index)
		{
			Buffer.BlockCopy(state, 0, x, 0, 16);
			Buffer.BlockCopy(block, index, x, 16, 16);
			for (int i = 0; i < 16; i++)
			{
				x[i + 32] = (byte)(state[i] ^ block[index + i]);
			}
			int num = 0;
			for (int j = 0; j < 18; j++)
			{
				for (int k = 0; k < 48; k++)
				{
					num = (x[k] ^= PI_SUBST[num]);
				}
				num = (num + j) & 0xFF;
			}
			Buffer.BlockCopy(x, 0, state, 0, 16);
			num = checksum[15];
			for (int l = 0; l < 16; l++)
			{
				num = (checksum[l] ^= PI_SUBST[block[index + l] ^ num]);
			}
		}
	}
	public abstract class MD4 : HashAlgorithm
	{
		protected MD4()
		{
			HashSizeValue = 128;
		}

		public new static MD4 Create()
		{
			return Create("MD4");
		}

		public new static MD4 Create(string hashName)
		{
			object obj = CryptoConfig.CreateFromName(hashName);
			if (obj == null)
			{
				obj = new MD4Managed();
			}
			return (MD4)obj;
		}
	}
	public class MD4Managed : MD4
	{
		private uint[] state;

		private byte[] buffer;

		private uint[] count;

		private uint[] x;

		private const int S11 = 3;

		private const int S12 = 7;

		private const int S13 = 11;

		private const int S14 = 19;

		private const int S21 = 3;

		private const int S22 = 5;

		private const int S23 = 9;

		private const int S24 = 13;

		private const int S31 = 3;

		private const int S32 = 9;

		private const int S33 = 11;

		private const int S34 = 15;

		private byte[] digest;

		public MD4Managed()
		{
			state = new uint[4];
			count = new uint[2];
			buffer = new byte[64];
			digest = new byte[16];
			x = new uint[16];
			Initialize();
		}

		public override void Initialize()
		{
			count[0] = 0u;
			count[1] = 0u;
			state[0] = 1732584193u;
			state[1] = 4023233417u;
			state[2] = 2562383102u;
			state[3] = 271733878u;
			Array.Clear(buffer, 0, 64);
			Array.Clear(x, 0, 16);
		}

		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			int num = (int)((count[0] >> 3) & 0x3F);
			count[0] += (uint)(cbSize << 3);
			if (count[0] < cbSize << 3)
			{
				count[1]++;
			}
			count[1] += (uint)(cbSize >> 29);
			int num2 = 64 - num;
			int i = 0;
			if (cbSize >= num2)
			{
				Buffer.BlockCopy(array, ibStart, buffer, num, num2);
				MD4Transform(state, buffer, 0);
				for (i = num2; i + 63 < cbSize; i += 64)
				{
					MD4Transform(state, array, ibStart + i);
				}
				num = 0;
			}
			Buffer.BlockCopy(array, ibStart + i, buffer, num, cbSize - i);
		}

		protected override byte[] HashFinal()
		{
			byte[] array = new byte[8];
			Encode(array, count);
			uint num = (count[0] >> 3) & 0x3F;
			int num2 = (int)((num < 56) ? (56 - num) : (120 - num));
			HashCore(Padding(num2), 0, num2);
			HashCore(array, 0, 8);
			Encode(digest, state);
			Initialize();
			return digest;
		}

		private byte[] Padding(int nLength)
		{
			if (nLength > 0)
			{
				byte[] array = new byte[nLength];
				array[0] = 128;
				return array;
			}
			return null;
		}

		private uint F(uint x, uint y, uint z)
		{
			return (x & y) | (~x & z);
		}

		private uint G(uint x, uint y, uint z)
		{
			return (x & y) | (x & z) | (y & z);
		}

		private uint H(uint x, uint y, uint z)
		{
			return x ^ y ^ z;
		}

		private uint ROL(uint x, byte n)
		{
			return (x << (int)n) | (x >> 32 - n);
		}

		private void FF(ref uint a, uint b, uint c, uint d, uint x, byte s)
		{
			a += F(b, c, d) + x;
			a = ROL(a, s);
		}

		private void GG(ref uint a, uint b, uint c, uint d, uint x, byte s)
		{
			a += G(b, c, d) + x + 1518500249;
			a = ROL(a, s);
		}

		private void HH(ref uint a, uint b, uint c, uint d, uint x, byte s)
		{
			a += H(b, c, d) + x + 1859775393;
			a = ROL(a, s);
		}

		private void Encode(byte[] output, uint[] input)
		{
			int num = 0;
			for (int i = 0; i < output.Length; i += 4)
			{
				output[i] = (byte)input[num];
				output[i + 1] = (byte)(input[num] >> 8);
				output[i + 2] = (byte)(input[num] >> 16);
				output[i + 3] = (byte)(input[num] >> 24);
				num++;
			}
		}

		private void Decode(uint[] output, byte[] input, int index)
		{
			int num = 0;
			int num2 = index;
			while (num < output.Length)
			{
				output[num] = (uint)(input[num2] | (input[num2 + 1] << 8) | (input[num2 + 2] << 16) | (input[num2 + 3] << 24));
				num++;
				num2 += 4;
			}
		}

		private void MD4Transform(uint[] state, byte[] block, int index)
		{
			uint a = state[0];
			uint a2 = state[1];
			uint a3 = state[2];
			uint a4 = state[3];
			Decode(x, block, index);
			FF(ref a, a2, a3, a4, x[0], 3);
			FF(ref a4, a, a2, a3, x[1], 7);
			FF(ref a3, a4, a, a2, x[2], 11);
			FF(ref a2, a3, a4, a, x[3], 19);
			FF(ref a, a2, a3, a4, x[4], 3);
			FF(ref a4, a, a2, a3, x[5], 7);
			FF(ref a3, a4, a, a2, x[6], 11);
			FF(ref a2, a3, a4, a, x[7], 19);
			FF(ref a, a2, a3, a4, x[8], 3);
			FF(ref a4, a, a2, a3, x[9], 7);
			FF(ref a3, a4, a, a2, x[10], 11);
			FF(ref a2, a3, a4, a, x[11], 19);
			FF(ref a, a2, a3, a4, x[12], 3);
			FF(ref a4, a, a2, a3, x[13], 7);
			FF(ref a3, a4, a, a2, x[14], 11);
			FF(ref a2, a3, a4, a, x[15], 19);
			GG(ref a, a2, a3, a4, x[0], 3);
			GG(ref a4, a, a2, a3, x[4], 5);
			GG(ref a3, a4, a, a2, x[8], 9);
			GG(ref a2, a3, a4, a, x[12], 13);
			GG(ref a, a2, a3, a4, x[1], 3);
			GG(ref a4, a, a2, a3, x[5], 5);
			GG(ref a3, a4, a, a2, x[9], 9);
			GG(ref a2, a3, a4, a, x[13], 13);
			GG(ref a, a2, a3, a4, x[2], 3);
			GG(ref a4, a, a2, a3, x[6], 5);
			GG(ref a3, a4, a, a2, x[10], 9);
			GG(ref a2, a3, a4, a, x[14], 13);
			GG(ref a, a2, a3, a4, x[3], 3);
			GG(ref a4, a, a2, a3, x[7], 5);
			GG(ref a3, a4, a, a2, x[11], 9);
			GG(ref a2, a3, a4, a, x[15], 13);
			HH(ref a, a2, a3, a4, x[0], 3);
			HH(ref a4, a, a2, a3, x[8], 9);
			HH(ref a3, a4, a, a2, x[4], 11);
			HH(ref a2, a3, a4, a, x[12], 15);
			HH(ref a, a2, a3, a4, x[2], 3);
			HH(ref a4, a, a2, a3, x[10], 9);
			HH(ref a3, a4, a, a2, x[6], 11);
			HH(ref a2, a3, a4, a, x[14], 15);
			HH(ref a, a2, a3, a4, x[1], 3);
			HH(ref a4, a, a2, a3, x[9], 9);
			HH(ref a3, a4, a, a2, x[5], 11);
			HH(ref a2, a3, a4, a, x[13], 15);
			HH(ref a, a2, a3, a4, x[3], 3);
			HH(ref a4, a, a2, a3, x[11], 9);
			HH(ref a3, a4, a, a2, x[7], 11);
			HH(ref a2, a3, a4, a, x[15], 15);
			state[0] += a;
			state[1] += a2;
			state[2] += a3;
			state[3] += a4;
		}
	}
	public sealed class PKCS1
	{
		private static byte[] emptySHA1 = new byte[20]
		{
			218, 57, 163, 238, 94, 107, 75, 13, 50, 85,
			191, 239, 149, 96, 24, 144, 175, 216, 7, 9
		};

		private static byte[] emptySHA256 = new byte[32]
		{
			227, 176, 196, 66, 152, 252, 28, 20, 154, 251,
			244, 200, 153, 111, 185, 36, 39, 174, 65, 228,
			100, 155, 147, 76, 164, 149, 153, 27, 120, 82,
			184, 85
		};

		private static byte[] emptySHA384 = new byte[48]
		{
			56, 176, 96, 167, 81, 172, 150, 56, 76, 217,
			50, 126, 177, 177, 227, 106, 33, 253, 183, 17,
			20, 190, 7, 67, 76, 12, 199, 191, 99, 246,
			225, 218, 39, 78, 222, 191, 231, 111, 101, 251,
			213, 26, 210, 241, 72, 152, 185, 91
		};

		private static byte[] emptySHA512 = new byte[64]
		{
			207, 131, 225, 53, 126, 239, 184, 189, 241, 84,
			40, 80, 214, 109, 128, 7, 214, 32, 228, 5,
			11, 87, 21, 220, 131, 244, 169, 33, 211, 108,
			233, 206, 71, 208, 209, 60, 93, 133, 242, 176,
			255, 131, 24, 210, 135, 126, 236, 47, 99, 185,
			49, 189, 71, 65, 122, 129, 165, 56, 50, 122,
			249, 39, 218, 62
		};

		private PKCS1()
		{
		}

		private static bool Compare(byte[] array1, byte[] array2)
		{
			bool flag = array1.Length == array2.Length;
			if (flag)
			{
				for (int i = 0; i < array1.Length; i++)
				{
					if (array1[i] != array2[i])
					{
						return false;
					}
				}
			}
			return flag;
		}

		private static byte[] xor(byte[] array1, byte[] array2)
		{
			byte[] array3 = new byte[array1.Length];
			for (int i = 0; i < array3.Length; i++)
			{
				array3[i] = (byte)(array1[i] ^ array2[i]);
			}
			return array3;
		}

		private static byte[] GetEmptyHash(HashAlgorithm hash)
		{
			if (hash is SHA1)
			{
				return emptySHA1;
			}
			if (hash is SHA256)
			{
				return emptySHA256;
			}
			if (hash is SHA384)
			{
				return emptySHA384;
			}
			if (hash is SHA512)
			{
				return emptySHA512;
			}
			return hash.ComputeHash((byte[])null);
		}

		public static byte[] I2OSP(int x, int size)
		{
			byte[] bytes = Mono.Security.BitConverterLE.GetBytes(x);
			Array.Reverse(bytes, 0, bytes.Length);
			return I2OSP(bytes, size);
		}

		public static byte[] I2OSP(byte[] x, int size)
		{
			byte[] array = new byte[size];
			Buffer.BlockCopy(x, 0, array, array.Length - x.Length, x.Length);
			return array;
		}

		public static byte[] OS2IP(byte[] x)
		{
			int num = 0;
			while (x[num++] == 0 && num < x.Length)
			{
			}
			num--;
			if (num > 0)
			{
				byte[] array = new byte[x.Length - num];
				Buffer.BlockCopy(x, num, array, 0, array.Length);
				return array;
			}
			return x;
		}

		public static byte[] RSAEP(RSA rsa, byte[] m)
		{
			return rsa.EncryptValue(m);
		}

		public static byte[] RSADP(RSA rsa, byte[] c)
		{
			return rsa.DecryptValue(c);
		}

		public static byte[] RSASP1(RSA rsa, byte[] m)
		{
			return rsa.DecryptValue(m);
		}

		public static byte[] RSAVP1(RSA rsa, byte[] s)
		{
			return rsa.EncryptValue(s);
		}

		public static byte[] Encrypt_OAEP(RSA rsa, HashAlgorithm hash, RandomNumberGenerator rng, byte[] M)
		{
			int num = rsa.KeySize / 8;
			int num2 = hash.HashSize / 8;
			if (M.Length > num - 2 * num2 - 2)
			{
				throw new CryptographicException("message too long");
			}
			byte[] emptyHash = GetEmptyHash(hash);
			int num3 = num - M.Length - 2 * num2 - 2;
			byte[] array = new byte[emptyHash.Length + num3 + 1 + M.Length];
			Buffer.BlockCopy(emptyHash, 0, array, 0, emptyHash.Length);
			array[emptyHash.Length + num3] = 1;
			Buffer.BlockCopy(M, 0, array, array.Length - M.Length, M.Length);
			byte[] array2 = new byte[num2];
			rng.GetBytes(array2);
			byte[] array3 = MGF1(hash, array2, num - num2 - 1);
			byte[] array4 = xor(array, array3);
			byte[] array5 = MGF1(hash, array4, num2);
			byte[] array6 = xor(array2, array5);
			byte[] array7 = new byte[array6.Length + array4.Length + 1];
			Buffer.BlockCopy(array6, 0, array7, 1, array6.Length);
			Buffer.BlockCopy(array4, 0, array7, array6.Length + 1, array4.Length);
			byte[] m = OS2IP(array7);
			return I2OSP(RSAEP(rsa, m), num);
		}

		public static byte[] Decrypt_OAEP(RSA rsa, HashAlgorithm hash, byte[] C)
		{
			int num = rsa.KeySize / 8;
			int num2 = hash.HashSize / 8;
			if (num < 2 * num2 + 2 || C.Length != num)
			{
				throw new CryptographicException("decryption error");
			}
			byte[] c = OS2IP(C);
			byte[] array = I2OSP(RSADP(rsa, c), num);
			byte[] array2 = new byte[num2];
			Buffer.BlockCopy(array, 1, array2, 0, array2.Length);
			byte[] array3 = new byte[num - num2 - 1];
			Buffer.BlockCopy(array, array.Length - array3.Length, array3, 0, array3.Length);
			byte[] array4 = MGF1(hash, array3, num2);
			byte[] mgfSeed = xor(array2, array4);
			byte[] array5 = MGF1(hash, mgfSeed, num - num2 - 1);
			byte[] array6 = xor(array3, array5);
			byte[] emptyHash = GetEmptyHash(hash);
			byte[] array7 = new byte[emptyHash.Length];
			Buffer.BlockCopy(array6, 0, array7, 0, array7.Length);
			bool flag = Compare(emptyHash, array7);
			int i;
			for (i = emptyHash.Length; array6[i] == 0; i++)
			{
			}
			int num3 = array6.Length - i - 1;
			byte[] array8 = new byte[num3];
			Buffer.BlockCopy(array6, i + 1, array8, 0, num3);
			if (array[0] != 0 || !flag || array6[i] != 1)
			{
				return null;
			}
			return array8;
		}

		public static byte[] Encrypt_v15(RSA rsa, RandomNumberGenerator rng, byte[] M)
		{
			int num = rsa.KeySize / 8;
			if (M.Length > num - 11)
			{
				throw new CryptographicException("message too long");
			}
			int num2 = System.Math.Max(8, num - M.Length - 3);
			byte[] array = new byte[num2];
			rng.GetNonZeroBytes(array);
			byte[] array2 = new byte[num];
			array2[1] = 2;
			Buffer.BlockCopy(array, 0, array2, 2, num2);
			Buffer.BlockCopy(M, 0, array2, num - M.Length, M.Length);
			byte[] m = OS2IP(array2);
			return I2OSP(RSAEP(rsa, m), num);
		}

		public static byte[] Decrypt_v15(RSA rsa, byte[] C)
		{
			int num = rsa.KeySize >> 3;
			if (num < 11 || C.Length > num)
			{
				throw new CryptographicException("decryption error");
			}
			byte[] c = OS2IP(C);
			byte[] array = I2OSP(RSADP(rsa, c), num);
			if (array[0] != 0 || array[1] != 2)
			{
				return null;
			}
			int i;
			for (i = 10; array[i] != 0 && i < array.Length; i++)
			{
			}
			if (array[i] != 0)
			{
				return null;
			}
			i++;
			byte[] array2 = new byte[array.Length - i];
			Buffer.BlockCopy(array, i, array2, 0, array2.Length);
			return array2;
		}

		public static byte[] Sign_v15(RSA rsa, HashAlgorithm hash, byte[] hashValue)
		{
			int num = rsa.KeySize >> 3;
			byte[] m = OS2IP(Encode_v15(hash, hashValue, num));
			return I2OSP(RSASP1(rsa, m), num);
		}

		internal static byte[] Sign_v15(RSA rsa, string hashName, byte[] hashValue)
		{
			using HashAlgorithm hash = CreateFromName(hashName);
			return Sign_v15(rsa, hash, hashValue);
		}

		public static bool Verify_v15(RSA rsa, HashAlgorithm hash, byte[] hashValue, byte[] signature)
		{
			return Verify_v15(rsa, hash, hashValue, signature, tryNonStandardEncoding: false);
		}

		internal static bool Verify_v15(RSA rsa, string hashName, byte[] hashValue, byte[] signature)
		{
			using HashAlgorithm hash = CreateFromName(hashName);
			return Verify_v15(rsa, hash, hashValue, signature, tryNonStandardEncoding: false);
		}

		public static bool Verify_v15(RSA rsa, HashAlgorithm hash, byte[] hashValue, byte[] signature, bool tryNonStandardEncoding)
		{
			int num = rsa.KeySize >> 3;
			byte[] s = OS2IP(signature);
			byte[] array = I2OSP(RSAVP1(rsa, s), num);
			bool flag = Compare(Encode_v15(hash, hashValue, num), array);
			if (flag || !tryNonStandardEncoding)
			{
				return flag;
			}
			if (array[0] != 0 || array[1] != 1)
			{
				return false;
			}
			int i;
			for (i = 2; i < array.Length - hashValue.Length - 1; i++)
			{
				if (array[i] != byte.MaxValue)
				{
					return false;
				}
			}
			if (array[i++] != 0)
			{
				return false;
			}
			byte[] array2 = new byte[hashValue.Length];
			Buffer.BlockCopy(array, i, array2, 0, array2.Length);
			return Compare(array2, hashValue);
		}

		public static byte[] Encode_v15(HashAlgorithm hash, byte[] hashValue, int emLength)
		{
			if (hashValue.Length != hash.HashSize >> 3)
			{
				throw new CryptographicException("bad hash length for " + hash.ToString());
			}
			byte[] array = null;
			string text = CryptoConfig.MapNameToOID(hash.ToString());
			if (text != null)
			{
				ASN1 aSN = new ASN1(48);
				aSN.Add(new ASN1(CryptoConfig.EncodeOID(text)));
				aSN.Add(new ASN1(5));
				ASN1 asn = new ASN1(4, hashValue);
				ASN1 aSN2 = new ASN1(48);
				aSN2.Add(aSN);
				aSN2.Add(asn);
				array = aSN2.GetBytes();
			}
			else
			{
				array = hashValue;
			}
			Buffer.BlockCopy(hashValue, 0, array, array.Length - hashValue.Length, hashValue.Length);
			int num = System.Math.Max(8, emLength - array.Length - 3);
			byte[] array2 = new byte[num + array.Length + 3];
			array2[1] = 1;
			for (int i = 2; i < num + 2; i++)
			{
				array2[i] = byte.MaxValue;
			}
			Buffer.BlockCopy(array, 0, array2, num + 3, array.Length);
			return array2;
		}

		public static byte[] MGF1(HashAlgorithm hash, byte[] mgfSeed, int maskLen)
		{
			if (maskLen < 0)
			{
				throw new OverflowException();
			}
			int num = mgfSeed.Length;
			int num2 = hash.HashSize >> 3;
			int num3 = maskLen / num2;
			if (maskLen % num2 != 0)
			{
				num3++;
			}
			byte[] array = new byte[num3 * num2];
			byte[] array2 = new byte[num + 4];
			int num4 = 0;
			for (int i = 0; i < num3; i++)
			{
				byte[] src = I2OSP(i, 4);
				Buffer.BlockCopy(mgfSeed, 0, array2, 0, num);
				Buffer.BlockCopy(src, 0, array2, num, 4);
				Buffer.BlockCopy(hash.ComputeHash(array2), 0, array, num4, num2);
				num4 += num2;
			}
			byte[] array3 = new byte[maskLen];
			Buffer.BlockCopy(array, 0, array3, 0, maskLen);
			return array3;
		}

		internal static string HashNameFromOid(string oid, bool throwOnError = true)
		{
			switch (oid)
			{
			case "1.2.840.113549.1.1.2":
				return "MD2";
			case "1.2.840.113549.1.1.3":
				return "MD4";
			case "1.2.840.113549.1.1.4":
				return "MD5";
			case "1.2.840.113549.1.1.5":
			case "1.3.14.3.2.29":
			case "1.2.840.10040.4.3":
				return "SHA1";
			case "1.2.840.113549.1.1.11":
				return "SHA256";
			case "1.2.840.113549.1.1.12":
				return "SHA384";
			case "1.2.840.113549.1.1.13":
				return "SHA512";
			case "1.3.36.3.3.1.2":
				return "RIPEMD160";
			default:
				if (throwOnError)
				{
					throw new CryptographicException("Unsupported hash algorithm: " + oid);
				}
				return null;
			}
		}

		internal static HashAlgorithm CreateFromOid(string oid)
		{
			return CreateFromName(HashNameFromOid(oid));
		}

		internal static HashAlgorithm CreateFromName(string name)
		{
			return HashAlgorithm.Create(name);
		}
	}
	public sealed class PKCS8
	{
		public enum KeyInfo
		{
			PrivateKey,
			EncryptedPrivateKey,
			Unknown
		}

		public class PrivateKeyInfo
		{
			private int _version;

			private string _algorithm;

			private byte[] _key;

			private ArrayList _list;

			public string Algorithm
			{
				get
				{
					return _algorithm;
				}
				set
				{
					_algorithm = value;
				}
			}

			public ArrayList Attributes => _list;

			public byte[] PrivateKey
			{
				get
				{
					if (_key == null)
					{
						return null;
					}
					return (byte[])_key.Clone();
				}
				set
				{
					if (value == null)
					{
						throw new ArgumentNullException("PrivateKey");
					}
					_key = (byte[])value.Clone();
				}
			}

			public int Version
			{
				get
				{
					return _version;
				}
				set
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("negative version");
					}
					_version = value;
				}
			}

			public PrivateKeyInfo()
			{
				_version = 0;
				_list = new ArrayList();
			}

			public PrivateKeyInfo(byte[] data)
				: this()
			{
				Decode(data);
			}

			private void Decode(byte[] data)
			{
				ASN1 aSN = new ASN1(data);
				if (aSN.Tag != 48)
				{
					throw new CryptographicException("invalid PrivateKeyInfo");
				}
				ASN1 aSN2 = aSN[0];
				if (aSN2.Tag != 2)
				{
					throw new CryptographicException("invalid version");
				}
				_version = aSN2.Value[0];
				ASN1 aSN3 = aSN[1];
				if (aSN3.Tag != 48)
				{
					throw new CryptographicException("invalid algorithm");
				}
				ASN1 aSN4 = aSN3[0];
				if (aSN4.Tag != 6)
				{
					throw new CryptographicException("missing algorithm OID");
				}
				_algorithm = ASN1Convert.ToOid(aSN4);
				ASN1 aSN5 = aSN[2];
				_key = aSN5.Value;
				if (aSN.Count > 3)
				{
					ASN1 aSN6 = aSN[3];
					for (int i = 0; i < aSN6.Count; i++)
					{
						_list.Add(aSN6[i]);
					}
				}
			}

			public byte[] GetBytes()
			{
				ASN1 aSN = new ASN1(48);
				aSN.Add(ASN1Convert.FromOid(_algorithm));
				aSN.Add(new ASN1(5));
				ASN1 aSN2 = new ASN1(48);
				aSN2.Add(new ASN1(2, new byte[1] { (byte)_version }));
				aSN2.Add(aSN);
				aSN2.Add(new ASN1(4, _key));
				if (_list.Count > 0)
				{
					ASN1 aSN3 = new ASN1(160);
					foreach (ASN1 item in _list)
					{
						aSN3.Add(item);
					}
					aSN2.Add(aSN3);
				}
				return aSN2.GetBytes();
			}

			private static byte[] RemoveLeadingZero(byte[] bigInt)
			{
				int srcOffset = 0;
				int num = bigInt.Length;
				if (bigInt[0] == 0)
				{
					srcOffset = 1;
					num--;
				}
				byte[] array = new byte[num];
				Buffer.BlockCopy(bigInt, srcOffset, array, 0, num);
				return array;
			}

			private static byte[] Normalize(byte[] bigInt, int length)
			{
				if (bigInt.Length == length)
				{
					return bigInt;
				}
				if (bigInt.Length > length)
				{
					return RemoveLeadingZero(bigInt);
				}
				byte[] array = new byte[length];
				Buffer.BlockCopy(bigInt, 0, array, length - bigInt.Length, bigInt.Length);
				return array;
			}

			public static RSA DecodeRSA(byte[] keypair)
			{
				ASN1 aSN = new ASN1(keypair);
				if (aSN.Tag != 48)
				{
					throw new CryptographicException("invalid private key format");
				}
				if (aSN[0].Tag != 2)
				{
					throw new CryptographicException("missing version");
				}
				if (aSN.Count < 9)
				{
					throw new CryptographicException("not enough key parameters");
				}
				RSAParameters parameters = new RSAParameters
				{
					Modulus = RemoveLeadingZero(aSN[1].Value)
				};
				int num = parameters.Modulus.Length;
				int length = num >> 1;
				parameters.D = Normalize(aSN[3].Value, num);
				parameters.DP = Normalize(aSN[6].Value, length);
				parameters.DQ = Normalize(aSN[7].Value, length);
				parameters.Exponent = RemoveLeadingZero(aSN[2].Value);
				parameters.InverseQ = Normalize(aSN[8].Value, length);
				parameters.P = Normalize(aSN[4].Value, length);
				parameters.Q = Normalize(aSN[5].Value, length);
				RSA rSA = null;
				try
				{
					rSA = RSA.Create();
					rSA.ImportParameters(parameters);
				}
				catch (CryptographicException)
				{
					rSA = new RSACryptoServiceProvider(new CspParameters
					{
						Flags = CspProviderFlags.UseMachineKeyStore
					});
					rSA.ImportParameters(parameters);
				}
				return rSA;
			}

			public static byte[] Encode(RSA rsa)
			{
				RSAParameters rSAParameters = rsa.ExportParameters(includePrivateParameters: true);
				ASN1 aSN = new ASN1(48);
				aSN.Add(new ASN1(2, new byte[1]));
				aSN.Add(ASN1Convert.FromUnsignedBigInteger(rSAParameters.Modulus));
				aSN.Add(ASN1Convert.FromUnsignedBigInteger(rSAParameters.Exponent));
				aSN.Add(ASN1Convert.FromUnsignedBigInteger(rSAParameters.D));
				aSN.Add(ASN1Convert.FromUnsignedBigInteger(rSAParameters.P));
				aSN.Add(ASN1Convert.FromUnsignedBigInteger(rSAParameters.Q));
				aSN.Add(ASN1Convert.FromUnsignedBigInteger(rSAParameters.DP));
				aSN.Add(ASN1Convert.FromUnsignedBigInteger(rSAParameters.DQ));
				aSN.Add(ASN1Convert.FromUnsignedBigInteger(rSAParameters.InverseQ));
				return aSN.GetBytes();
			}

			public static DSA DecodeDSA(byte[] privateKey, DSAParameters dsaParameters)
			{
				ASN1 aSN = new ASN1(privateKey);
				if (aSN.Tag != 2)
				{
					throw new CryptographicException("invalid private key format");
				}
				dsaParameters.X = Normalize(aSN.Value, 20);
				DSA dSA = DSA.Create();
				dSA.ImportParameters(dsaParameters);
				return dSA;
			}

			public static byte[] Encode(DSA dsa)
			{
				return ASN1Convert.FromUnsignedBigInteger(dsa.ExportParameters(includePrivateParameters: true).X).GetBytes();
			}

			public static byte[] Encode(AsymmetricAlgorithm aa)
			{
				if (aa is RSA)
				{
					return Encode((RSA)aa);
				}
				if (aa is DSA)
				{
					return Encode((DSA)aa);
				}
				throw new CryptographicException("Unknown asymmetric algorithm {0}", aa.ToString());
			}
		}

		public class EncryptedPrivateKeyInfo
		{
			private string _algorithm;

			private byte[] _salt;

			private int _iterations;

			private byte[] _data;

			public string Algorithm
			{
				get
				{
					return _algorithm;
				}
				set
				{
					_algorithm = value;
				}
			}

			public byte[] EncryptedData
			{
				get
				{
					if (_data != null)
					{
						return (byte[])_data.Clone();
					}
					return null;
				}
				set
				{
					_data = ((value == null) ? null : ((byte[])value.Clone()));
				}
			}

			public byte[] Salt
			{
				get
				{
					if (_salt == null)
					{
						RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
						_salt = new byte[8];
						randomNumberGenerator.GetBytes(_salt);
					}
					return (byte[])_salt.Clone();
				}
				set
				{
					_salt = (byte[])value.Clone();
				}
			}

			public int IterationCount
			{
				get
				{
					return _iterations;
				}
				set
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("IterationCount", "Negative");
					}
					_iterations = value;
				}
			}

			public EncryptedPrivateKeyInfo()
			{
			}

			public EncryptedPrivateKeyInfo(byte[] data)
				: this()
			{
				Decode(data);
			}

			private void Decode(byte[] data)
			{
				ASN1 aSN = new ASN1(data);
				if (aSN.Tag != 48)
				{
					throw new CryptographicException("invalid EncryptedPrivateKeyInfo");
				}
				ASN1 aSN2 = aSN[0];
				if (aSN2.Tag != 48)
				{
					throw new CryptographicException("invalid encryptionAlgorithm");
				}
				ASN1 aSN3 = aSN2[0];
				if (aSN3.Tag != 6)
				{
					throw new CryptographicException("invalid algorithm");
				}
				_algorithm = ASN1Convert.ToOid(aSN3);
				if (aSN2.Count > 1)
				{
					ASN1 aSN4 = aSN2[1];
					if (aSN4.Tag != 48)
					{
						throw new CryptographicException("invalid parameters");
					}
					ASN1 aSN5 = aSN4[0];
					if (aSN5.Tag != 4)
					{
						throw new CryptographicException("invalid salt");
					}
					_salt = aSN5.Value;
					ASN1 aSN6 = aSN4[1];
					if (aSN6.Tag != 2)
					{
						throw new CryptographicException("invalid iterationCount");
					}
					_iterations = ASN1Convert.ToInt32(aSN6);
				}
				ASN1 aSN7 = aSN[1];
				if (aSN7.Tag != 4)
				{
					throw new CryptographicException("invalid EncryptedData");
				}
				_data = aSN7.Value;
			}

			public byte[] GetBytes()
			{
				if (_algorithm == null)
				{
					throw new CryptographicException("No algorithm OID specified");
				}
				ASN1 aSN = new ASN1(48);
				aSN.Add(ASN1Convert.FromOid(_algorithm));
				if (_iterations > 0 || _salt != null)
				{
					ASN1 asn = new ASN1(4, _salt);
					ASN1 asn2 = ASN1Convert.FromInt32(_iterations);
					ASN1 aSN2 = new ASN1(48);
					aSN2.Add(asn);
					aSN2.Add(asn2);
					aSN.Add(aSN2);
				}
				ASN1 asn3 = new ASN1(4, _data);
				ASN1 aSN3 = new ASN1(48);
				aSN3.Add(aSN);
				aSN3.Add(asn3);
				return aSN3.GetBytes();
			}
		}

		private PKCS8()
		{
		}

		public static KeyInfo GetType(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			KeyInfo result = KeyInfo.Unknown;
			try
			{
				ASN1 aSN = new ASN1(data);
				if (aSN.Tag == 48 && aSN.Count > 0)
				{
					switch (aSN[0].Tag)
					{
					case 2:
						result = KeyInfo.PrivateKey;
						break;
					case 48:
						result = KeyInfo.EncryptedPrivateKey;
						break;
					}
				}
			}
			catch
			{
				throw new CryptographicException("invalid ASN.1 data");
			}
			return result;
		}
	}
	public abstract class RC4 : SymmetricAlgorithm
	{
		private static KeySizes[] s_legalBlockSizes = new KeySizes[1]
		{
			new KeySizes(64, 64, 0)
		};

		private static KeySizes[] s_legalKeySizes = new KeySizes[1]
		{
			new KeySizes(40, 2048, 8)
		};

		public override byte[] IV
		{
			get
			{
				return new byte[0];
			}
			set
			{
			}
		}

		public RC4()
		{
			KeySizeValue = 128;
			BlockSizeValue = 64;
			FeedbackSizeValue = BlockSizeValue;
			LegalBlockSizesValue = s_legalBlockSizes;
			LegalKeySizesValue = s_legalKeySizes;
		}

		public new static RC4 Create()
		{
			return Create("RC4");
		}

		public new static RC4 Create(string algName)
		{
			object obj = CryptoConfig.CreateFromName(algName);
			if (obj == null)
			{
				obj = new ARC4Managed();
			}
			return (RC4)obj;
		}
	}
	public class RSAManaged : RSA
	{
		public delegate void KeyGeneratedEventHandler(object sender, EventArgs e);

		private const int defaultKeySize = 1024;

		private bool isCRTpossible;

		private bool keyBlinding = true;

		private bool keypairGenerated;

		private bool m_disposed;

		private BigInteger d;

		private BigInteger p;

		private BigInteger q;

		private BigInteger dp;

		private BigInteger dq;

		private BigInteger qInv;

		private BigInteger n;

		private BigInteger e;

		public override int KeySize
		{
			get
			{
				if (m_disposed)
				{
					throw new ObjectDisposedException(global::Locale.GetText("Keypair was disposed"));
				}
				if (keypairGenerated)
				{
					int num = n.BitCount();
					if ((num & 7) != 0)
					{
						num += 8 - (num & 7);
					}
					return num;
				}
				return base.KeySize;
			}
		}

		public override string KeyExchangeAlgorithm => "RSA-PKCS1-KeyEx";

		public bool PublicOnly
		{
			get
			{
				if (keypairGenerated)
				{
					if (!(d == null))
					{
						return n == null;
					}
					return true;
				}
				return false;
			}
		}

		public override string SignatureAlgorithm => "http://www.w3.org/2000/09/xmldsig#rsa-sha1";

		public bool UseKeyBlinding
		{
			get
			{
				return keyBlinding;
			}
			set
			{
				keyBlinding = value;
			}
		}

		public bool IsCrtPossible
		{
			get
			{
				if (keypairGenerated)
				{
					return isCRTpossible;
				}
				return true;
			}
		}

		public event KeyGeneratedEventHandler KeyGenerated;

		public RSAManaged()
			: this(1024)
		{
		}

		public RSAManaged(int keySize)
		{
			LegalKeySizesValue = new KeySizes[1];
			LegalKeySizesValue[0] = new KeySizes(384, 16384, 8);
			base.KeySize = keySize;
		}

		~RSAManaged()
		{
			Dispose(disposing: false);
		}

		private void GenerateKeyPair()
		{
			int num = KeySize + 1 >> 1;
			int bits = KeySize - num;
			e = 65537u;
			do
			{
				p = BigInteger.GeneratePseudoPrime(num);
			}
			while (p % 65537u == 1);
			while (true)
			{
				q = BigInteger.GeneratePseudoPrime(bits);
				if (q % 65537u != 1 && p != q)
				{
					n = p * q;
					if (n.BitCount() == KeySize)
					{
						break;
					}
					if (p < q)
					{
						p = q;
					}
				}
			}
			BigInteger bigInteger = p - 1;
			BigInteger bigInteger2 = q - 1;
			BigInteger modulus = bigInteger * bigInteger2;
			d = e.ModInverse(modulus);
			dp = d % bigInteger;
			dq = d % bigInteger2;
			qInv = q.ModInverse(p);
			keypairGenerated = true;
			isCRTpossible = true;
			if (this.KeyGenerated != null)
			{
				this.KeyGenerated(this, null);
			}
		}

		public override byte[] DecryptValue(byte[] rgb)
		{
			if (m_disposed)
			{
				throw new ObjectDisposedException("private key");
			}
			if (!keypairGenerated)
			{
				GenerateKeyPair();
			}
			BigInteger bigInteger = new BigInteger(rgb);
			BigInteger bigInteger2 = null;
			if (keyBlinding)
			{
				bigInteger2 = BigInteger.GenerateRandom(n.BitCount());
				bigInteger = bigInteger2.ModPow(e, n) * bigInteger % n;
			}
			BigInteger bigInteger6;
			if (isCRTpossible)
			{
				BigInteger bigInteger3 = bigInteger.ModPow(dp, p);
				BigInteger bigInteger4 = bigInteger.ModPow(dq, q);
				if (bigInteger4 > bigInteger3)
				{
					BigInteger bigInteger5 = p - (bigInteger4 - bigInteger3) * qInv % p;
					bigInteger6 = bigInteger4 + q * bigInteger5;
				}
				else
				{
					BigInteger bigInteger5 = (bigInteger3 - bigInteger4) * qInv % p;
					bigInteger6 = bigInteger4 + q * bigInteger5;
				}
			}
			else
			{
				if (PublicOnly)
				{
					throw new CryptographicException(global::Locale.GetText("Missing private key to decrypt value."));
				}
				bigInteger6 = bigInteger.ModPow(d, n);
			}
			if (keyBlinding)
			{
				bigInteger6 = bigInteger6 * bigInteger2.ModInverse(n) % n;
				bigInteger2.Clear();
			}
			byte[] paddedValue = GetPaddedValue(bigInteger6, KeySize >> 3);
			bigInteger.Clear();
			bigInteger6.Clear();
			return paddedValue;
		}

		public override byte[] EncryptValue(byte[] rgb)
		{
			if (m_disposed)
			{
				throw new ObjectDisposedException("public key");
			}
			if (!keypairGenerated)
			{
				GenerateKeyPair();
			}
			BigInteger bigInteger = new BigInteger(rgb);
			BigInteger bigInteger2 = bigInteger.ModPow(e, n);
			byte[] paddedValue = GetPaddedValue(bigInteger2, KeySize >> 3);
			bigInteger.Clear();
			bigInteger2.Clear();
			return paddedValue;
		}

		public override RSAParameters ExportParameters(bool includePrivateParameters)
		{
			if (m_disposed)
			{
				throw new ObjectDisposedException(global::Locale.GetText("Keypair was disposed"));
			}
			if (!keypairGenerated)
			{
				GenerateKeyPair();
			}
			RSAParameters result = new RSAParameters
			{
				Exponent = e.GetBytes(),
				Modulus = n.GetBytes()
			};
			if (includePrivateParameters)
			{
				if (d == null)
				{
					throw new CryptographicException("Missing private key");
				}
				result.D = d.GetBytes();
				if (result.D.Length != result.Modulus.Length)
				{
					byte[] array = new byte[result.Modulus.Length];
					Buffer.BlockCopy(result.D, 0, array, array.Length - result.D.Length, result.D.Length);
					result.D = array;
				}
				if (p != null && q != null && dp != null && dq != null && qInv != null)
				{
					int length = KeySize >> 4;
					result.P = GetPaddedValue(p, length);
					result.Q = GetPaddedValue(q, length);
					result.DP = GetPaddedValue(dp, length);
					result.DQ = GetPaddedValue(dq, length);
					result.InverseQ = GetPaddedValue(qInv, length);
				}
			}
			return result;
		}

		public override void ImportParameters(RSAParameters parameters)
		{
			if (m_disposed)
			{
				throw new ObjectDisposedException(global::Locale.GetText("Keypair was disposed"));
			}
			if (parameters.Exponent == null)
			{
				throw new CryptographicException(global::Locale.GetText("Missing Exponent"));
			}
			if (parameters.Modulus == null)
			{
				throw new CryptographicException(global::Locale.GetText("Missing Modulus"));
			}
			e = new BigInteger(parameters.Exponent);
			n = new BigInteger(parameters.Modulus);
			d = (dp = (dq = (qInv = (p = (q = null)))));
			if (parameters.D != null)
			{
				d = new BigInteger(parameters.D);
			}
			if (parameters.DP != null)
			{
				dp = new BigInteger(parameters.DP);
			}
			if (parameters.DQ != null)
			{
				dq = new BigInteger(parameters.DQ);
			}
			if (parameters.InverseQ != null)
			{
				qInv = new BigInteger(parameters.InverseQ);
			}
			if (parameters.P != null)
			{
				p = new BigInteger(parameters.P);
			}
			if (parameters.Q != null)
			{
				q = new BigInteger(parameters.Q);
			}
			keypairGenerated = true;
			bool flag = p != null && q != null && dp != null;
			isCRTpossible = flag && dq != null && qInv != null;
			if (!flag)
			{
				return;
			}
			bool flag2 = n == p * q;
			if (flag2)
			{
				BigInteger bigInteger = p - 1;
				BigInteger bigInteger2 = q - 1;
				BigInteger modulus = bigInteger * bigInteger2;
				BigInteger bigInteger3 = e.ModInverse(modulus);
				flag2 = d == bigInteger3;
				if (!flag2 && isCRTpossible)
				{
					flag2 = dp == bigInteger3 % bigInteger && dq == bigInteger3 % bigInteger2 && qInv == q.ModInverse(p);
				}
			}
			if (flag2)
			{
				return;
			}
			throw new CryptographicException(global::Locale.GetText("Private/public key mismatch"));
		}

		protected override void Dispose(bool disposing)
		{
			if (!m_disposed)
			{
				if (d != null)
				{
					d.Clear();
					d = null;
				}
				if (p != null)
				{
					p.Clear();
					p = null;
				}
				if (q != null)
				{
					q.Clear();
					q = null;
				}
				if (dp != null)
				{
					dp.Clear();
					dp = null;
				}
				if (dq != null)
				{
					dq.Clear();
					dq = null;
				}
				if (qInv != null)
				{
					qInv.Clear();
					qInv = null;
				}
				if (disposing)
				{
					if (e != null)
					{
						e.Clear();
						e = null;
					}
					if (n != null)
					{
						n.Clear();
						n = null;
					}
				}
			}
			m_disposed = true;
		}

		public override string ToXmlString(bool includePrivateParameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			RSAParameters rSAParameters = ExportParameters(includePrivateParameters);
			try
			{
				stringBuilder.Append("<RSAKeyValue>");
				stringBuilder.Append("<Modulus>");
				stringBuilder.Append(Convert.ToBase64String(rSAParameters.Modulus));
				stringBuilder.Append("</Modulus>");
				stringBuilder.Append("<Exponent>");
				stringBuilder.Append(Convert.ToBase64String(rSAParameters.Exponent));
				stringBuilder.Append("</Exponent>");
				if (includePrivateParameters)
				{
					if (rSAParameters.P != null)
					{
						stringBuilder.Append("<P>");
						stringBuilder.Append(Convert.ToBase64String(rSAParameters.P));
						stringBuilder.Append("</P>");
					}
					if (rSAParameters.Q != null)
					{
						stringBuilder.Append("<Q>");
						stringBuilder.Append(Convert.ToBase64String(rSAParameters.Q));
						stringBuilder.Append("</Q>");
					}
					if (rSAParameters.DP != null)
					{
						stringBuilder.Append("<DP>");
						stringBuilder.Append(Convert.ToBase64String(rSAParameters.DP));
						stringBuilder.Append("</DP>");
					}
					if (rSAParameters.DQ != null)
					{
						stringBuilder.Append("<DQ>");
						stringBuilder.Append(Convert.ToBase64String(rSAParameters.DQ));
						stringBuilder.Append("</DQ>");
					}
					if (rSAParameters.InverseQ != null)
					{
						stringBuilder.Append("<InverseQ>");
						stringBuilder.Append(Convert.ToBase64String(rSAParameters.InverseQ));
						stringBuilder.Append("</InverseQ>");
					}
					stringBuilder.Append("<D>");
					stringBuilder.Append(Convert.ToBase64String(rSAParameters.D));
					stringBuilder.Append("</D>");
				}
				stringBuilder.Append("</RSAKeyValue>");
			}
			catch
			{
				if (rSAParameters.P != null)
				{
					Array.Clear(rSAParameters.P, 0, rSAParameters.P.Length);
				}
				if (rSAParameters.Q != null)
				{
					Array.Clear(rSAParameters.Q, 0, rSAParameters.Q.Length);
				}
				if (rSAParameters.DP != null)
				{
					Array.Clear(rSAParameters.DP, 0, rSAParameters.DP.Length);
				}
				if (rSAParameters.DQ != null)
				{
					Array.Clear(rSAParameters.DQ, 0, rSAParameters.DQ.Length);
				}
				if (rSAParameters.InverseQ != null)
				{
					Array.Clear(rSAParameters.InverseQ, 0, rSAParameters.InverseQ.Length);
				}
				if (rSAParameters.D != null)
				{
					Array.Clear(rSAParameters.D, 0, rSAParameters.D.Length);
				}
				throw;
			}
			return stringBuilder.ToString();
		}

		private byte[] GetPaddedValue(BigInteger value, int length)
		{
			byte[] bytes = value.GetBytes();
			if (bytes.Length >= length)
			{
				return bytes;
			}
			byte[] array = new byte[length];
			Buffer.BlockCopy(bytes, 0, array, length - bytes.Length, bytes.Length);
			Array.Clear(bytes, 0, bytes.Length);
			return array;
		}
	}
	internal sealed class SHAConstants
	{
		public static readonly uint[] K1 = new uint[64]
		{
			1116352408u, 1899447441u, 3049323471u, 3921009573u, 961987163u, 1508970993u, 2453635748u, 2870763221u, 3624381080u, 310598401u,
			607225278u, 1426881987u, 1925078388u, 2162078206u, 2614888103u, 3248222580u, 3835390401u, 4022224774u, 264347078u, 604807628u,
			770255983u, 1249150122u, 1555081692u, 1996064986u, 2554220882u, 2821834349u, 2952996808u, 3210313671u, 3336571891u, 3584528711u,
			113926993u, 338241895u, 666307205u, 773529912u, 1294757372u, 1396182291u, 1695183700u, 1986661051u, 2177026350u, 2456956037u,
			2730485921u, 2820302411u, 3259730800u, 3345764771u, 3516065817u, 3600352804u, 4094571909u, 275423344u, 430227734u, 506948616u,
			659060556u, 883997877u, 958139571u, 1322822218u, 1537002063u, 1747873779u, 1955562222u, 2024104815u, 2227730452u, 2361852424u,
			2428436474u, 2756734187u, 3204031479u, 3329325298u
		};

		private SHAConstants()
		{
		}
	}
	public abstract class SHA224 : HashAlgorithm
	{
		public SHA224()
		{
			HashSizeValue = 224;
		}

		public new static SHA224 Create()
		{
			return Create("SHA224");
		}

		public new static SHA224 Create(string hashName)
		{
			object obj = CryptoConfig.CreateFromName(hashName);
			if (obj == null)
			{
				obj = new SHA224Managed();
			}
			return (SHA224)obj;
		}
	}
	public class SHA224Managed : SHA224
	{
		private const int BLOCK_SIZE_BYTES = 64;

		private uint[] _H;

		private ulong count;

		private byte[] _ProcessingBuffer;

		private int _ProcessingBufferCount;

		private uint[] buff;

		public SHA224Managed()
		{
			_H = new uint[8];
			_ProcessingBuffer = new byte[64];
			buff = new uint[64];
			Initialize();
		}

		private uint Ch(uint u, uint v, uint w)
		{
			return (u & v) ^ (~u & w);
		}

		private uint Maj(uint u, uint v, uint w)
		{
			return (u & v) ^ (u & w) ^ (v & w);
		}

		private uint Ro0(uint x)
		{
			return ((x >> 7) | (x << 25)) ^ ((x >> 18) | (x << 14)) ^ (x >> 3);
		}

		private uint Ro1(uint x)
		{
			return ((x >> 17) | (x << 15)) ^ ((x >> 19) | (x << 13)) ^ (x >> 10);
		}

		private uint Sig0(uint x)
		{
			return ((x >> 2) | (x << 30)) ^ ((x >> 13) | (x << 19)) ^ ((x >> 22) | (x << 10));
		}

		private uint Sig1(uint x)
		{
			return ((x >> 6) | (x << 26)) ^ ((x >> 11) | (x << 21)) ^ ((x >> 25) | (x << 7));
		}

		protected override void HashCore(byte[] rgb, int start, int size)
		{
			State = 1;
			if (_ProcessingBufferCount != 0)
			{
				if (size < 64 - _ProcessingBufferCount)
				{
					Buffer.BlockCopy(rgb, start, _ProcessingBuffer, _ProcessingBufferCount, size);
					_ProcessingBufferCount += size;
					return;
				}
				int num = 64 - _ProcessingBufferCount;
				Buffer.BlockCopy(rgb, start, _ProcessingBuffer, _ProcessingBufferCount, num);
				ProcessBlock(_ProcessingBuffer, 0);
				_ProcessingBufferCount = 0;
				start += num;
				size -= num;
			}
			for (int num = 0; num < size - size % 64; num += 64)
			{
				ProcessBlock(rgb, start + num);
			}
			if (size % 64 != 0)
			{
				Buffer.BlockCopy(rgb, size - size % 64 + start, _ProcessingBuffer, 0, size % 64);
				_ProcessingBufferCount = size % 64;
			}
		}

		protected override byte[] HashFinal()
		{
			byte[] array = new byte[28];
			ProcessFinalBlock(_ProcessingBuffer, 0, _ProcessingBufferCount);
			for (int i = 0; i < 7; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					array[i * 4 + j] = (byte)(_H[i] >> 24 - j * 8);
				}
			}
			State = 0;
			return array;
		}

		public override void Initialize()
		{
			count = 0uL;
			_ProcessingBufferCount = 0;
			_H[0] = 3238371032u;
			_H[1] = 914150663u;
			_H[2] = 812702999u;
			_H[3] = 4144912697u;
			_H[4] = 4290775857u;
			_H[5] = 1750603025u;
			_H[6] = 1694076839u;
			_H[7] = 3204075428u;
		}

		private void ProcessBlock(byte[] inputBuffer, int inputOffset)
		{
			uint[] k = SHAConstants.K1;
			uint[] array = buff;
			count += 64uL;
			for (int i = 0; i < 16; i++)
			{
				array[i] = (uint)((inputBuffer[inputOffset + 4 * i] << 24) | (inputBuffer[inputOffset + 4 * i + 1] << 16) | (inputBuffer[inputOffset + 4 * i + 2] << 8) | inputBuffer[inputOffset + 4 * i + 3]);
			}
			for (int i = 16; i < 64; i++)
			{
				uint num = array[i - 15];
				num = ((num >> 7) | (num << 25)) ^ ((num >> 18) | (num << 14)) ^ (num >> 3);
				uint num2 = array[i - 2];
				num2 = ((num2 >> 17) | (num2 << 15)) ^ ((num2 >> 19) | (num2 << 13)) ^ (num2 >> 10);
				array[i] = num2 + array[i - 7] + num + array[i - 16];
			}
			uint num3 = _H[0];
			uint num4 = _H[1];
			uint num5 = _H[2];
			uint num6 = _H[3];
			uint num7 = _H[4];
			uint num8 = _H[5];
			uint num9 = _H[6];
			uint num10 = _H[7];
			for (int i = 0; i < 64; i++)
			{
				uint num = num10 + (((num7 >> 6) | (num7 << 26)) ^ ((num7 >> 11) | (num7 << 21)) ^ ((num7 >> 25) | (num7 << 7))) + ((num7 & num8) ^ (~num7 & num9)) + k[i] + array[i];
				uint num2 = ((num3 >> 2) | (num3 << 30)) ^ ((num3 >> 13) | (num3 << 19)) ^ ((num3 >> 22) | (num3 << 10));
				num2 += (num3 & num4) ^ (num3 & num5) ^ (num4 & num5);
				num10 = num9;
				num9 = num8;
				num8 = num7;
				num7 = num6 + num;
				num6 = num5;
				num5 = num4;
				num4 = num3;
				num3 = num + num2;
			}
			_H[0] += num3;
			_H[1] += num4;
			_H[2] += num5;
			_H[3] += num6;
			_H[4] += num7;
			_H[5] += num8;
			_H[6] += num9;
			_H[7] += num10;
		}

		private void ProcessFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			ulong num = count + (ulong)inputCount;
			int num2 = 56 - (int)(num % 64);
			if (num2 < 1)
			{
				num2 += 64;
			}
			byte[] array = new byte[inputCount + num2 + 8];
			for (int i = 0; i < inputCount; i++)
			{
				array[i] = inputBuffer[i + inputOffset];
			}
			array[inputCount] = 128;
			for (int j = inputCount + 1; j < inputCount + num2; j++)
			{
				array[j] = 0;
			}
			ulong length = num << 3;
			AddLength(length, array, inputCount + num2);
			ProcessBlock(array, 0);
			if (inputCount + num2 + 8 == 128)
			{
				ProcessBlock(array, 64);
			}
		}

		internal void AddLength(ulong length, byte[] buffer, int position)
		{
			buffer[position++] = (byte)(length >> 56);
			buffer[position++] = (byte)(length >> 48);
			buffer[position++] = (byte)(length >> 40);
			buffer[position++] = (byte)(length >> 32);
			buffer[position++] = (byte)(length >> 24);
			buffer[position++] = (byte)(length >> 16);
			buffer[position++] = (byte)(length >> 8);
			buffer[position] = (byte)length;
		}
	}
	internal abstract class SymmetricTransform : ICryptoTransform, IDisposable
	{
		protected SymmetricAlgorithm algo;

		protected bool encrypt;

		protected int BlockSizeByte;

		protected byte[] temp;

		protected byte[] temp2;

		private byte[] workBuff;

		private byte[] workout;

		protected PaddingMode padmode;

		protected int FeedBackByte;

		private bool m_disposed;

		protected bool lastBlock;

		private RandomNumberGenerator _rng;

		public virtual bool CanTransformMultipleBlocks => true;

		public virtual bool CanReuseTransform => false;

		public virtual int InputBlockSize => BlockSizeByte;

		public virtual int OutputBlockSize => BlockSizeByte;

		private bool KeepLastBlock
		{
			get
			{
				if (!encrypt && padmode != PaddingMode.None)
				{
					return padmode != PaddingMode.Zeros;
				}
				return false;
			}
		}

		public SymmetricTransform(SymmetricAlgorithm symmAlgo, bool encryption, byte[] rgbIV)
		{
			algo = symmAlgo;
			encrypt = encryption;
			BlockSizeByte = algo.BlockSize >> 3;
			rgbIV = ((rgbIV != null) ? ((byte[])rgbIV.Clone()) : KeyBuilder.IV(BlockSizeByte));
			if (rgbIV.Length < BlockSizeByte)
			{
				throw new CryptographicException(global::Locale.GetText("IV is too small ({0} bytes), it should be {1} bytes long.", rgbIV.Length, BlockSizeByte));
			}
			padmode = algo.Padding;
			temp = new byte[BlockSizeByte];
			Buffer.BlockCopy(rgbIV, 0, temp, 0, System.Math.Min(BlockSizeByte, rgbIV.Length));
			temp2 = new byte[BlockSizeByte];
			FeedBackByte = algo.FeedbackSize >> 3;
			workBuff = new byte[BlockSizeByte];
			workout = new byte[BlockSizeByte];
		}

		~SymmetricTransform()
		{
			Dispose(disposing: false);
		}

		void IDisposable.Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!m_disposed)
			{
				if (disposing)
				{
					Array.Clear(temp, 0, BlockSizeByte);
					temp = null;
					Array.Clear(temp2, 0, BlockSizeByte);
					temp2 = null;
				}
				m_disposed = true;
			}
		}

		protected virtual void Transform(byte[] input, byte[] output)
		{
			switch (algo.Mode)
			{
			case CipherMode.ECB:
				ECB(input, output);
				break;
			case CipherMode.CBC:
				CBC(input, output);
				break;
			case CipherMode.CFB:
				CFB(input, output);
				break;
			case CipherMode.OFB:
				OFB(input, output);
				break;
			case CipherMode.CTS:
				CTS(input, output);
				break;
			default:
				throw new NotImplementedException("Unkown CipherMode" + algo.Mode);
			}
		}

		protected abstract void ECB(byte[] input, byte[] output);

		protected virtual void CBC(byte[] input, byte[] output)
		{
			if (encrypt)
			{
				for (int i = 0; i < BlockSizeByte; i++)
				{
					temp[i] ^= input[i];
				}
				ECB(temp, output);
				Buffer.BlockCopy(output, 0, temp, 0, BlockSizeByte);
				return;
			}
			Buffer.BlockCopy(input, 0, temp2, 0, BlockSizeByte);
			ECB(input, output);
			for (int j = 0; j < BlockSizeByte; j++)
			{
				output[j] ^= temp[j];
			}
			Buffer.BlockCopy(temp2, 0, temp, 0, BlockSizeByte);
		}

		protected virtual void CFB(byte[] input, byte[] output)
		{
			if (encrypt)
			{
				for (int i = 0; i < BlockSizeByte; i++)
				{
					ECB(temp, temp2);
					output[i] = (byte)(temp2[0] ^ input[i]);
					Buffer.BlockCopy(temp, 1, temp, 0, BlockSizeByte - 1);
					Buffer.BlockCopy(output, i, temp, BlockSizeByte - 1, 1);
				}
				return;
			}
			for (int j = 0; j < BlockSizeByte; j++)
			{
				encrypt = true;
				ECB(temp, temp2);
				encrypt = false;
				Buffer.BlockCopy(temp, 1, temp, 0, BlockSizeByte - 1);
				Buffer.BlockCopy(input, j, temp, BlockSizeByte - 1, 1);
				output[j] = (byte)(temp2[0] ^ input[j]);
			}
		}

		protected virtual void OFB(byte[] input, byte[] output)
		{
			throw new CryptographicException("OFB isn't supported by the framework");
		}

		protected virtual void CTS(byte[] input, byte[] output)
		{
			throw new CryptographicException("CTS isn't supported by the framework");
		}

		private void CheckInput(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("inputOffset", "< 0");
			}
			if (inputCount < 0)
			{
				throw new ArgumentOutOfRangeException("inputCount", "< 0");
			}
			if (inputOffset > inputBuffer.Length - inputCount)
			{
				throw new ArgumentException("inputBuffer", global::Locale.GetText("Overflow"));
			}
		}

		public virtual int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			if (m_disposed)
			{
				throw new ObjectDisposedException("Object is disposed");
			}
			CheckInput(inputBuffer, inputOffset, inputCount);
			if (outputBuffer == null)
			{
				throw new ArgumentNullException("outputBuffer");
			}
			if (outputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("outputOffset", "< 0");
			}
			int num = outputBuffer.Length - inputCount - outputOffset;
			if (!encrypt && 0 > num && (padmode == PaddingMode.None || padmode == PaddingMode.Zeros))
			{
				throw new CryptographicException("outputBuffer", global::Locale.GetText("Overflow"));
			}
			if (KeepLastBlock)
			{
				if (0 > num + BlockSizeByte)
				{
					throw new CryptographicException("outputBuffer", global::Locale.GetText("Overflow"));
				}
			}
			else if (0 > num)
			{
				if (inputBuffer.Length - inputOffset - outputBuffer.Length != BlockSizeByte)
				{
					throw new CryptographicException("outputBuffer", global::Locale.GetText("Overflow"));
				}
				inputCount = outputBuffer.Length - outputOffset;
			}
			return InternalTransformBlock(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset);
		}

		private int InternalTransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			int num = inputOffset;
			int num2;
			if (inputCount != BlockSizeByte)
			{
				if (inputCount % BlockSizeByte != 0)
				{
					throw new CryptographicException("Invalid input block size.");
				}
				num2 = inputCount / BlockSizeByte;
			}
			else
			{
				num2 = 1;
			}
			if (KeepLastBlock)
			{
				num2--;
			}
			int num3 = 0;
			if (lastBlock)
			{
				Transform(workBuff, workout);
				Buffer.BlockCopy(workout, 0, outputBuffer, outputOffset, BlockSizeByte);
				outputOffset += BlockSizeByte;
				num3 += BlockSizeByte;
				lastBlock = false;
			}
			for (int i = 0; i < num2; i++)
			{
				Buffer.BlockCopy(inputBuffer, num, workBuff, 0, BlockSizeByte);
				Transform(workBuff, workout);
				Buffer.BlockCopy(workout, 0, outputBuffer, outputOffset, BlockSizeByte);
				num += BlockSizeByte;
				outputOffset += BlockSizeByte;
				num3 += BlockSizeByte;
			}
			if (KeepLastBlock)
			{
				Buffer.BlockCopy(inputBuffer, num, workBuff, 0, BlockSizeByte);
				lastBlock = true;
			}
			return num3;
		}

		private void Random(byte[] buffer, int start, int length)
		{
			if (_rng == null)
			{
				_rng = RandomNumberGenerator.Create();
			}
			byte[] array = new byte[length];
			_rng.GetBytes(array);
			Buffer.BlockCopy(array, 0, buffer, start, length);
		}

		private void ThrowBadPaddingException(PaddingMode padding, int length, int position)
		{
			string text = string.Format(global::Locale.GetText("Bad {0} padding."), padding);
			if (length >= 0)
			{
				text += string.Format(global::Locale.GetText(" Invalid length {0}."), length);
			}
			if (position >= 0)
			{
				text += string.Format(global::Locale.GetText(" Error found at position {0}."), position);
			}
			throw new CryptographicException(text);
		}

		protected virtual byte[] FinalEncrypt(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			int num = inputCount / BlockSizeByte * BlockSizeByte;
			int num2 = inputCount - num;
			int num3 = num;
			PaddingMode paddingMode = padmode;
			if (paddingMode == PaddingMode.PKCS7 || (uint)(paddingMode - 4) <= 1u)
			{
				num3 += BlockSizeByte;
			}
			else
			{
				if (inputCount == 0)
				{
					return new byte[0];
				}
				if (num2 != 0)
				{
					if (padmode == PaddingMode.None)
					{
						throw new CryptographicException("invalid block length");
					}
					byte[] array = new byte[num + BlockSizeByte];
					Buffer.BlockCopy(inputBuffer, inputOffset, array, 0, inputCount);
					inputBuffer = array;
					inputOffset = 0;
					inputCount = array.Length;
					num3 = inputCount;
				}
			}
			byte[] array2 = new byte[num3];
			int num4 = 0;
			while (num3 > BlockSizeByte)
			{
				InternalTransformBlock(inputBuffer, inputOffset, BlockSizeByte, array2, num4);
				inputOffset += BlockSizeByte;
				num4 += BlockSizeByte;
				num3 -= BlockSizeByte;
			}
			byte b = (byte)(BlockSizeByte - num2);
			switch (padmode)
			{
			case PaddingMode.ANSIX923:
				array2[^1] = b;
				Buffer.BlockCopy(inputBuffer, inputOffset, array2, num, num2);
				InternalTransformBlock(array2, num, BlockSizeByte, array2, num);
				break;
			case PaddingMode.ISO10126:
				Random(array2, array2.Length - b, b - 1);
				array2[^1] = b;
				Buffer.BlockCopy(inputBuffer, inputOffset, array2, num, num2);
				InternalTransformBlock(array2, num, BlockSizeByte, array2, num);
				break;
			case PaddingMode.PKCS7:
			{
				int num5 = array2.Length;
				while (--num5 >= array2.Length - b)
				{
					array2[num5] = b;
				}
				Buffer.BlockCopy(inputBuffer, inputOffset, array2, num, num2);
				InternalTransformBlock(array2, num, BlockSizeByte, array2, num);
				break;
			}
			default:
				InternalTransformBlock(inputBuffer, inputOffset, BlockSizeByte, array2, num4);
				break;
			}
			return array2;
		}

		protected virtual byte[] FinalDecrypt(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			int num = inputCount;
			int num2 = inputCount;
			if (lastBlock)
			{
				num2 += BlockSizeByte;
			}
			byte[] array = new byte[num2];
			int num3 = 0;
			while (num > 0)
			{
				int num4 = InternalTransformBlock(inputBuffer, inputOffset, BlockSizeByte, array, num3);
				inputOffset += BlockSizeByte;
				num3 += num4;
				num -= BlockSizeByte;
			}
			if (lastBlock)
			{
				Transform(workBuff, workout);
				Buffer.BlockCopy(workout, 0, array, num3, BlockSizeByte);
				num3 += BlockSizeByte;
				lastBlock = false;
			}
			byte b = (byte)((num2 > 0) ? array[num2 - 1] : 0);
			switch (padmode)
			{
			case PaddingMode.ANSIX923:
			{
				if (b == 0 || b > BlockSizeByte)
				{
					ThrowBadPaddingException(padmode, b, -1);
				}
				for (int num6 = b - 1; num6 > 0; num6--)
				{
					if (array[num2 - 1 - num6] != 0)
					{
						ThrowBadPaddingException(padmode, -1, num6);
					}
				}
				num2 -= b;
				break;
			}
			case PaddingMode.ISO10126:
				if (b == 0 || b > BlockSizeByte)
				{
					ThrowBadPaddingException(padmode, b, -1);
				}
				num2 -= b;
				break;
			case PaddingMode.PKCS7:
			{
				if (b == 0 || b > BlockSizeByte)
				{
					ThrowBadPaddingException(padmode, b, -1);
				}
				for (int num5 = b - 1; num5 > 0; num5--)
				{
					if (array[num2 - 1 - num5] != b)
					{
						ThrowBadPaddingException(padmode, -1, num5);
					}
				}
				num2 -= b;
				break;
			}
			}
			if (num2 > 0)
			{
				byte[] array2 = new byte[num2];
				Buffer.BlockCopy(array, 0, array2, 0, num2);
				Array.Clear(array, 0, array.Length);
				return array2;
			}
			return new byte[0];
		}

		public virtual byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (m_disposed)
			{
				throw new ObjectDisposedException("Object is disposed");
			}
			CheckInput(inputBuffer, inputOffset, inputCount);
			if (encrypt)
			{
				return FinalEncrypt(inputBuffer, inputOffset, inputCount);
			}
			return FinalDecrypt(inputBuffer, inputOffset, inputCount);
		}
	}
	internal class HMAC : KeyedHashAlgorithm
	{
		private HashAlgorithm hash;

		private bool hashing;

		private byte[] innerPad;

		private byte[] outerPad;

		public override byte[] Key
		{
			get
			{
				return (byte[])KeyValue.Clone();
			}
			set
			{
				if (hashing)
				{
					throw new Exception("Cannot change key during hash operation.");
				}
				if (value.Length > 64)
				{
					KeyValue = hash.ComputeHash(value);
				}
				else
				{
					KeyValue = (byte[])value.Clone();
				}
				initializePad();
			}
		}

		public HMAC()
		{
			hash = MD5.Create();
			HashSizeValue = hash.HashSize;
			byte[] array = new byte[64];
			new RNGCryptoServiceProvider().GetNonZeroBytes(array);
			KeyValue = (byte[])array.Clone();
			Initialize();
		}

		public HMAC(HashAlgorithm ha, byte[] rgbKey)
		{
			hash = ha;
			HashSizeValue = hash.HashSize;
			if (rgbKey.Length > 64)
			{
				KeyValue = hash.ComputeHash(rgbKey);
			}
			else
			{
				KeyValue = (byte[])rgbKey.Clone();
			}
			Initialize();
		}

		public override void Initialize()
		{
			hash.Initialize();
			initializePad();
			hashing = false;
		}

		protected override byte[] HashFinal()
		{
			if (!hashing)
			{
				hash.TransformBlock(innerPad, 0, innerPad.Length, innerPad, 0);
				hashing = true;
			}
			hash.TransformFinalBlock(new byte[0], 0, 0);
			byte[] array = hash.Hash;
			hash.Initialize();
			hash.TransformBlock(outerPad, 0, outerPad.Length, outerPad, 0);
			hash.TransformFinalBlock(array, 0, array.Length);
			Initialize();
			return hash.Hash;
		}

		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			if (!hashing)
			{
				hash.TransformBlock(innerPad, 0, innerPad.Length, innerPad, 0);
				hashing = true;
			}
			hash.TransformBlock(array, ibStart, cbSize, array, ibStart);
		}

		private void initializePad()
		{
			innerPad = new byte[64];
			outerPad = new byte[64];
			for (int i = 0; i < KeyValue.Length; i++)
			{
				innerPad[i] = (byte)(KeyValue[i] ^ 0x36);
				outerPad[i] = (byte)(KeyValue[i] ^ 0x5C);
			}
			for (int j = KeyValue.Length; j < 64; j++)
			{
				innerPad[j] = 54;
				outerPad[j] = 92;
			}
		}
	}
}
namespace Mono.Security.Authenticode
{
	public enum Authority
	{
		Individual,
		Commercial,
		Maximum
	}
	public class AuthenticodeBase
	{
		public const string spcIndirectDataContext = "1.3.6.1.4.1.311.2.1.4";

		private byte[] fileblock;

		private Stream fs;

		private int blockNo;

		private int blockLength;

		private int peOffset;

		private int dirSecurityOffset;

		private int dirSecuritySize;

		private int coffSymbolTableOffset;

		private bool pe64;

		internal bool PE64
		{
			get
			{
				if (blockNo < 1)
				{
					ReadFirstBlock();
				}
				return pe64;
			}
		}

		internal int PEOffset
		{
			get
			{
				if (blockNo < 1)
				{
					ReadFirstBlock();
				}
				return peOffset;
			}
		}

		internal int CoffSymbolTableOffset
		{
			get
			{
				if (blockNo < 1)
				{
					ReadFirstBlock();
				}
				return coffSymbolTableOffset;
			}
		}

		internal int SecurityOffset
		{
			get
			{
				if (blockNo < 1)
				{
					ReadFirstBlock();
				}
				return dirSecurityOffset;
			}
		}

		public AuthenticodeBase()
		{
			fileblock = new byte[4096];
		}

		internal void Open(string filename)
		{
			if (fs != null)
			{
				Close();
			}
			fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
			blockNo = 0;
		}

		internal void Open(byte[] rawdata)
		{
			if (fs != null)
			{
				Close();
			}
			fs = new MemoryStream(rawdata, writable: false);
			blockNo = 0;
		}

		internal void Close()
		{
			if (fs != null)
			{
				fs.Close();
				fs = null;
			}
		}

		internal void ReadFirstBlock()
		{
			int num = ProcessFirstBlock();
			if (num != 0)
			{
				throw new NotSupportedException(global::Locale.GetText("Cannot sign non PE files, e.g. .CAB or .MSI files (error {0}).", num));
			}
		}

		internal int ProcessFirstBlock()
		{
			if (fs == null)
			{
				return 1;
			}
			fs.Position = 0L;
			blockLength = fs.Read(fileblock, 0, fileblock.Length);
			blockNo = 1;
			if (blockLength < 64)
			{
				return 2;
			}
			if (Mono.Security.BitConverterLE.ToUInt16(fileblock, 0) != 23117)
			{
				return 3;
			}
			peOffset = Mono.Security.BitConverterLE.ToInt32(fileblock, 60);
			if (peOffset > fileblock.Length)
			{
				throw new NotSupportedException(string.Format(global::Locale.GetText("Header size too big (> {0} bytes)."), fileblock.Length));
			}
			if (peOffset > fs.Length)
			{
				return 4;
			}
			if (Mono.Security.BitConverterLE.ToUInt32(fileblock, peOffset) != 17744)
			{
				return 5;
			}
			ushort num = Mono.Security.BitConverterLE.ToUInt16(fileblock, peOffset + 24);
			pe64 = num == 523;
			if (pe64)
			{
				dirSecurityOffset = Mono.Security.BitConverterLE.ToInt32(fileblock, peOffset + 168);
				dirSecuritySize = Mono.Security.BitConverterLE.ToInt32(fileblock, peOffset + 168 + 4);
			}
			else
			{
				dirSecurityOffset = Mono.Security.BitConverterLE.ToInt32(fileblock, peOffset + 152);
				dirSecuritySize = Mono.Security.BitConverterLE.ToInt32(fileblock, peOffset + 156);
			}
			coffSymbolTableOffset = Mono.Security.BitConverterLE.ToInt32(fileblock, peOffset + 12);
			return 0;
		}

		internal byte[] GetSecurityEntry()
		{
			if (blockNo < 1)
			{
				ReadFirstBlock();
			}
			if (dirSecuritySize > 8)
			{
				byte[] array = new byte[dirSecuritySize - 8];
				fs.Position = dirSecurityOffset + 8;
				fs.Read(array, 0, array.Length);
				return array;
			}
			return null;
		}

		internal byte[] GetHash(HashAlgorithm hash)
		{
			if (blockNo < 1)
			{
				ReadFirstBlock();
			}
			fs.Position = blockLength;
			int num = 0;
			long num2;
			if (dirSecurityOffset > 0)
			{
				if (dirSecurityOffset < blockLength)
				{
					blockLength = dirSecurityOffset;
					num2 = 0L;
				}
				else
				{
					num2 = dirSecurityOffset - blockLength;
				}
			}
			else if (coffSymbolTableOffset > 0)
			{
				fileblock[PEOffset + 12] = 0;
				fileblock[PEOffset + 13] = 0;
				fileblock[PEOffset + 14] = 0;
				fileblock[PEOffset + 15] = 0;
				fileblock[PEOffset + 16] = 0;
				fileblock[PEOffset + 17] = 0;
				fileblock[PEOffset + 18] = 0;
				fileblock[PEOffset + 19] = 0;
				if (coffSymbolTableOffset < blockLength)
				{
					blockLength = coffSymbolTableOffset;
					num2 = 0L;
				}
				else
				{
					num2 = coffSymbolTableOffset - blockLength;
				}
			}
			else
			{
				num = (int)(fs.Length & 7);
				if (num > 0)
				{
					num = 8 - num;
				}
				num2 = fs.Length - blockLength;
			}
			int num3 = peOffset + 88;
			hash.TransformBlock(fileblock, 0, num3, fileblock, 0);
			num3 += 4;
			if (pe64)
			{
				hash.TransformBlock(fileblock, num3, 76, fileblock, num3);
				num3 += 84;
			}
			else
			{
				hash.TransformBlock(fileblock, num3, 60, fileblock, num3);
				num3 += 68;
			}
			if (num2 == 0L)
			{
				hash.TransformFinalBlock(fileblock, num3, blockLength - num3);
			}
			else
			{
				hash.TransformBlock(fileblock, num3, blockLength - num3, fileblock, num3);
				long num4 = num2 >> 12;
				int num5 = (int)(num2 - (num4 << 12));
				if (num5 == 0)
				{
					num4--;
					num5 = 4096;
				}
				while (num4-- > 0)
				{
					fs.Read(fileblock, 0, fileblock.Length);
					hash.TransformBlock(fileblock, 0, fileblock.Length, fileblock, 0);
				}
				if (fs.Read(fileblock, 0, num5) != num5)
				{
					return null;
				}
				if (num > 0)
				{
					hash.TransformBlock(fileblock, 0, num5, fileblock, 0);
					hash.TransformFinalBlock(new byte[num], 0, num);
				}
				else
				{
					hash.TransformFinalBlock(fileblock, 0, num5);
				}
			}
			return hash.Hash;
		}

		protected byte[] HashFile(string fileName, string hashName)
		{
			try
			{
				Open(fileName);
				HashAlgorithm hash = HashAlgorithm.Create(hashName);
				byte[] hash2 = GetHash(hash);
				Close();
				return hash2;
			}
			catch
			{
				return null;
			}
		}
	}
	public class AuthenticodeDeformatter : AuthenticodeBase
	{
		private string filename;

		private byte[] rawdata;

		private byte[] hash;

		private Mono.Security.X509.X509CertificateCollection coll;

		private ASN1 signedHash;

		private DateTime timestamp;

		private Mono.Security.X509.X509Certificate signingCertificate;

		private int reason;

		private bool trustedRoot;

		private bool trustedTimestampRoot;

		private byte[] entry;

		private Mono.Security.X509.X509Chain signerChain;

		private Mono.Security.X509.X509Chain timestampChain;

		public string FileName
		{
			get
			{
				return filename;
			}
			set
			{
				Reset();
				filename = value;
				try
				{
					CheckSignature();
				}
				catch (SecurityException)
				{
					throw;
				}
				catch
				{
					reason = 1;
				}
			}
		}

		public byte[] RawData
		{
			get
			{
				return rawdata;
			}
			set
			{
				Reset();
				rawdata = value;
				try
				{
					CheckSignature();
				}
				catch (SecurityException)
				{
					throw;
				}
				catch
				{
					reason = 1;
				}
			}
		}

		public byte[] Hash
		{
			get
			{
				if (signedHash == null)
				{
					return null;
				}
				return (byte[])signedHash.Value.Clone();
			}
		}

		public int Reason
		{
			get
			{
				if (reason == -1)
				{
					IsTrusted();
				}
				return reason;
			}
		}

		public byte[] Signature
		{
			get
			{
				if (entry == null)
				{
					return null;
				}
				return (byte[])entry.Clone();
			}
		}

		public DateTime Timestamp => timestamp;

		public Mono.Security.X509.X509CertificateCollection Certificates => coll;

		public Mono.Security.X509.X509Certificate SigningCertificate => signingCertificate;

		public AuthenticodeDeformatter()
		{
			reason = -1;
			signerChain = new Mono.Security.X509.X509Chain();
			timestampChain = new Mono.Security.X509.X509Chain();
		}

		public AuthenticodeDeformatter(string fileName)
			: this()
		{
			FileName = fileName;
		}

		public AuthenticodeDeformatter(byte[] rawData)
			: this()
		{
			RawData = rawData;
		}

		public bool IsTrusted()
		{
			if (entry == null)
			{
				reason = 1;
				return false;
			}
			if (signingCertificate == null)
			{
				reason = 7;
				return false;
			}
			if (signerChain.Root == null || !trustedRoot)
			{
				reason = 6;
				return false;
			}
			if (timestamp != DateTime.MinValue)
			{
				if (timestampChain.Root == null || !trustedTimestampRoot)
				{
					reason = 6;
					return false;
				}
				if (!signingCertificate.WasCurrent(Timestamp))
				{
					reason = 4;
					return false;
				}
			}
			else if (!signingCertificate.IsCurrent)
			{
				reason = 8;
				return false;
			}
			if (reason == -1)
			{
				reason = 0;
			}
			return true;
		}

		private bool CheckSignature()
		{
			if (filename != null)
			{
				Open(filename);
			}
			else
			{
				Open(rawdata);
			}
			entry = GetSecurityEntry();
			if (entry == null)
			{
				reason = 1;
				Close();
				return false;
			}
			PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(entry);
			if (contentInfo.ContentType != "1.2.840.113549.1.7.2")
			{
				Close();
				return false;
			}
			PKCS7.SignedData signedData = new PKCS7.SignedData(contentInfo.Content);
			if (signedData.ContentInfo.ContentType != "1.3.6.1.4.1.311.2.1.4")
			{
				Close();
				return false;
			}
			coll = signedData.Certificates;
			ASN1 content = signedData.ContentInfo.Content;
			signedHash = content[0][1][1];
			HashAlgorithm hashAlgorithm = null;
			switch (signedHash.Length)
			{
			case 16:
				hashAlgorithm = MD5.Create();
				hash = GetHash(hashAlgorithm);
				break;
			case 20:
				hashAlgorithm = SHA1.Create();
				hash = GetHash(hashAlgorithm);
				break;
			case 32:
				hashAlgorithm = SHA256.Create();
				hash = GetHash(hashAlgorithm);
				break;
			case 48:
				hashAlgorithm = SHA384.Create();
				hash = GetHash(hashAlgorithm);
				break;
			case 64:
				hashAlgorithm = SHA512.Create();
				hash = GetHash(hashAlgorithm);
				break;
			default:
				reason = 5;
				Close();
				return false;
			}
			Close();
			if (!signedHash.CompareValue(hash))
			{
				reason = 2;
			}
			byte[] value = content[0].Value;
			hashAlgorithm.Initialize();
			byte[] calculatedMessageDigest = hashAlgorithm.ComputeHash(value);
			if (VerifySignature(signedData, calculatedMessageDigest, hashAlgorithm))
			{
				return reason == 0;
			}
			return false;
		}

		private bool CompareIssuerSerial(string issuer, byte[] serial, Mono.Security.X509.X509Certificate x509)
		{
			if (issuer != x509.IssuerName)
			{
				return false;
			}
			if (serial.Length != x509.SerialNumber.Length)
			{
				return false;
			}
			int num = serial.Length;
			for (int i = 0; i < serial.Length; i++)
			{
				if (serial[i] != x509.SerialNumber[--num])
				{
					return false;
				}
			}
			return true;
		}

		private bool VerifySignature(PKCS7.SignedData sd, byte[] calculatedMessageDigest, HashAlgorithm ha)
		{
			string text = null;
			ASN1 aSN = null;
			for (int i = 0; i < sd.SignerInfo.AuthenticatedAttributes.Count; i++)
			{
				ASN1 aSN2 = (ASN1)sd.SignerInfo.AuthenticatedAttributes[i];
				switch (ASN1Convert.ToOid(aSN2[0]))
				{
				case "1.2.840.113549.1.9.3":
					text = ASN1Convert.ToOid(aSN2[1][0]);
					break;
				case "1.2.840.113549.1.9.4":
					aSN = aSN2[1][0];
					break;
				}
			}
			if (text != "1.3.6.1.4.1.311.2.1.4")
			{
				return false;
			}
			if (aSN == null)
			{
				return false;
			}
			if (!aSN.CompareValue(calculatedMessageDigest))
			{
				return false;
			}
			string str = CryptoConfig.MapNameToOID(ha.ToString());
			ASN1 aSN3 = new ASN1(49);
			foreach (ASN1 authenticatedAttribute in sd.SignerInfo.AuthenticatedAttributes)
			{
				aSN3.Add(authenticatedAttribute);
			}
			ha.Initialize();
			byte[] rgbHash = ha.ComputeHash(aSN3.GetBytes());
			byte[] signature = sd.SignerInfo.Signature;
			string issuerName = sd.SignerInfo.IssuerName;
			byte[] serialNumber = sd.SignerInfo.SerialNumber;
			foreach (Mono.Security.X509.X509Certificate item in coll)
			{
				if (CompareIssuerSerial(issuerName, serialNumber, item) && item.PublicKey.Length > signature.Length >> 3)
				{
					signingCertificate = item;
					if (((RSACryptoServiceProvider)item.RSA).VerifyHash(rgbHash, str, signature))
					{
						signerChain.LoadCertificates(coll);
						trustedRoot = signerChain.Build(item);
						break;
					}
				}
			}
			if (sd.SignerInfo.UnauthenticatedAttributes.Count == 0)
			{
				trustedTimestampRoot = true;
			}
			else
			{
				for (int j = 0; j < sd.SignerInfo.UnauthenticatedAttributes.Count; j++)
				{
					ASN1 aSN4 = (ASN1)sd.SignerInfo.UnauthenticatedAttributes[j];
					if (ASN1Convert.ToOid(aSN4[0]) == "1.2.840.113549.1.9.6")
					{
						PKCS7.SignerInfo cs = new PKCS7.SignerInfo(aSN4[1]);
						trustedTimestampRoot = VerifyCounterSignature(cs, signature);
					}
				}
			}
			if (trustedRoot)
			{
				return trustedTimestampRoot;
			}
			return false;
		}

		private bool VerifyCounterSignature(PKCS7.SignerInfo cs, byte[] signature)
		{
			if (cs.Version > 1)
			{
				return false;
			}
			string text = null;
			ASN1 aSN = null;
			for (int i = 0; i < cs.AuthenticatedAttributes.Count; i++)
			{
				ASN1 aSN2 = (ASN1)cs.AuthenticatedAttributes[i];
				switch (ASN1Convert.ToOid(aSN2[0]))
				{
				case "1.2.840.113549.1.9.3":
					text = ASN1Convert.ToOid(aSN2[1][0]);
					break;
				case "1.2.840.113549.1.9.4":
					aSN = aSN2[1][0];
					break;
				case "1.2.840.113549.1.9.5":
					timestamp = ASN1Convert.ToDateTime(aSN2[1][0]);
					break;
				}
			}
			if (text != "1.2.840.113549.1.7.1")
			{
				return false;
			}
			if (aSN == null)
			{
				return false;
			}
			string hashName = null;
			switch (aSN.Length)
			{
			case 16:
				hashName = "MD5";
				break;
			case 20:
				hashName = "SHA1";
				break;
			case 32:
				hashName = "SHA256";
				break;
			case 48:
				hashName = "SHA384";
				break;
			case 64:
				hashName = "SHA512";
				break;
			}
			HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashName);
			if (!aSN.CompareValue(hashAlgorithm.ComputeHash(signature)))
			{
				return false;
			}
			byte[] signature2 = cs.Signature;
			ASN1 aSN3 = new ASN1(49);
			foreach (ASN1 authenticatedAttribute in cs.AuthenticatedAttributes)
			{
				aSN3.Add(authenticatedAttribute);
			}
			byte[] hashValue = hashAlgorithm.ComputeHash(aSN3.GetBytes());
			string issuerName = cs.IssuerName;
			byte[] serialNumber = cs.SerialNumber;
			foreach (Mono.Security.X509.X509Certificate item in coll)
			{
				if (CompareIssuerSerial(issuerName, serialNumber, item) && item.PublicKey.Length > signature2.Length)
				{
					RSACryptoServiceProvider rSACryptoServiceProvider = (RSACryptoServiceProvider)item.RSA;
					RSAManaged rSAManaged = new RSAManaged();
					rSAManaged.ImportParameters(rSACryptoServiceProvider.ExportParameters(includePrivateParameters: false));
					if (PKCS1.Verify_v15(rSAManaged, hashAlgorithm, hashValue, signature2, tryNonStandardEncoding: true))
					{
						timestampChain.LoadCertificates(coll);
						return timestampChain.Build(item);
					}
				}
			}
			return false;
		}

		private void Reset()
		{
			filename = null;
			rawdata = null;
			entry = null;
			hash = null;
			signedHash = null;
			signingCertificate = null;
			reason = -1;
			trustedRoot = false;
			trustedTimestampRoot = false;
			signerChain.Reset();
			timestampChain.Reset();
			timestamp = DateTime.MinValue;
		}
	}
	public class AuthenticodeFormatter : AuthenticodeBase
	{
		private Authority authority;

		private Mono.Security.X509.X509CertificateCollection certs;

		private ArrayList crls;

		private string hash;

		private RSA rsa;

		private Uri timestamp;

		private ASN1 authenticode;

		private PKCS7.SignedData pkcs7;

		private string description;

		private Uri url;

		private const string signedData = "1.2.840.113549.1.7.2";

		private const string countersignature = "1.2.840.113549.1.9.6";

		private const string spcStatementType = "1.3.6.1.4.1.311.2.1.11";

		private const string spcSpOpusInfo = "1.3.6.1.4.1.311.2.1.12";

		private const string spcPelmageData = "1.3.6.1.4.1.311.2.1.15";

		private const string commercialCodeSigning = "1.3.6.1.4.1.311.2.1.22";

		private const string timestampCountersignature = "1.3.6.1.4.1.311.3.2.1";

		private static byte[] obsolete = new byte[37]
		{
			3, 1, 0, 160, 32, 162, 30, 128, 28, 0,
			60, 0, 60, 0, 60, 0, 79, 0, 98, 0,
			115, 0, 111, 0, 108, 0, 101, 0, 116, 0,
			101, 0, 62, 0, 62, 0, 62
		};

		public Authority Authority
		{
			get
			{
				return authority;
			}
			set
			{
				authority = value;
			}
		}

		public Mono.Security.X509.X509CertificateCollection Certificates => certs;

		public ArrayList Crl => crls;

		public string Hash
		{
			get
			{
				if (hash == null)
				{
					hash = "SHA1";
				}
				return hash;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Hash");
				}
				string text = value.ToUpper(CultureInfo.InvariantCulture);
				switch (text)
				{
				case "MD5":
				case "SHA1":
				case "SHA256":
				case "SHA384":
				case "SHA512":
					hash = text;
					break;
				case "SHA2":
					hash = "SHA256";
					break;
				default:
					throw new ArgumentException("Invalid Authenticode hash algorithm");
				}
			}
		}

		public RSA RSA
		{
			get
			{
				return rsa;
			}
			set
			{
				rsa = value;
			}
		}

		public Uri TimestampUrl
		{
			get
			{
				return timestamp;
			}
			set
			{
				timestamp = value;
			}
		}

		public string Description
		{
			get
			{
				return description;
			}
			set
			{
				description = value;
			}
		}

		public Uri Url
		{
			get
			{
				return url;
			}
			set
			{
				url = value;
			}
		}

		public AuthenticodeFormatter()
		{
			certs = new Mono.Security.X509.X509CertificateCollection();
			crls = new ArrayList();
			authority = Authority.Maximum;
			pkcs7 = new PKCS7.SignedData();
		}

		private ASN1 AlgorithmIdentifier(string oid)
		{
			ASN1 aSN = new ASN1(48);
			aSN.Add(ASN1Convert.FromOid(oid));
			aSN.Add(new ASN1(5));
			return aSN;
		}

		private ASN1 Attribute(string oid, ASN1 value)
		{
			ASN1 aSN = new ASN1(48);
			aSN.Add(ASN1Convert.FromOid(oid));
			aSN.Add(new ASN1(49)).Add(value);
			return aSN;
		}

		private ASN1 Opus(string description, string url)
		{
			ASN1 aSN = new ASN1(48);
			if (description != null)
			{
				aSN.Add(new ASN1(160)).Add(new ASN1(128, Encoding.BigEndianUnicode.GetBytes(description)));
			}
			if (url != null)
			{
				aSN.Add(new ASN1(161)).Add(new ASN1(128, Encoding.ASCII.GetBytes(url)));
			}
			return aSN;
		}

		private byte[] Header(byte[] fileHash, string hashAlgorithm)
		{
			string oid = CryptoConfig.MapNameToOID(hashAlgorithm);
			ASN1 aSN = new ASN1(48);
			ASN1 aSN2 = aSN.Add(new ASN1(48));
			aSN2.Add(ASN1Convert.FromOid("1.3.6.1.4.1.311.2.1.15"));
			aSN2.Add(new ASN1(48, obsolete));
			ASN1 aSN3 = aSN.Add(new ASN1(48));
			aSN3.Add(AlgorithmIdentifier(oid));
			aSN3.Add(new ASN1(4, fileHash));
			pkcs7.HashName = hashAlgorithm;
			pkcs7.Certificates.AddRange(certs);
			pkcs7.ContentInfo.ContentType = "1.3.6.1.4.1.311.2.1.4";
			pkcs7.ContentInfo.Content.Add(aSN);
			pkcs7.SignerInfo.Certificate = certs[0];
			pkcs7.SignerInfo.Key = rsa;
			ASN1 aSN4 = null;
			aSN4 = ((!(url == null)) ? Attribute("1.3.6.1.4.1.311.2.1.12", Opus(description, url.ToString())) : Attribute("1.3.6.1.4.1.311.2.1.12", Opus(description, null)));
			pkcs7.SignerInfo.AuthenticatedAttributes.Add(aSN4);
			pkcs7.GetASN1();
			return pkcs7.SignerInfo.Signature;
		}

		public ASN1 TimestampRequest(byte[] signature)
		{
			PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo("1.2.840.113549.1.7.1");
			contentInfo.Content.Add(new ASN1(4, signature));
			return PKCS7.AlgorithmIdentifier("1.3.6.1.4.1.311.3.2.1", contentInfo.ASN1);
		}

		public void ProcessTimestamp(byte[] response)
		{
			ASN1 aSN = new ASN1(Convert.FromBase64String(Encoding.ASCII.GetString(response)));
			for (int i = 0; i < aSN[1][0][3].Count; i++)
			{
				pkcs7.Certificates.Add(new Mono.Security.X509.X509Certificate(aSN[1][0][3][i].GetBytes()));
			}
			pkcs7.SignerInfo.UnauthenticatedAttributes.Add(Attribute("1.2.840.113549.1.9.6", aSN[1][0][4][0]));
		}

		private byte[] Timestamp(byte[] signature)
		{
			ASN1 aSN = TimestampRequest(signature);
			return new WebClient
			{
				Headers = 
				{
					{ "Content-Type", "application/octet-stream" },
					{ "Accept", "application/octet-stream" }
				}
			}.UploadData(data: Encoding.ASCII.GetBytes(Convert.ToBase64String(aSN.GetBytes())), address: timestamp.ToString());
		}

		private bool Save(string fileName, byte[] asn)
		{
			File.Copy(fileName, fileName + ".bak", overwrite: true);
			using (FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite))
			{
				int num;
				if (base.SecurityOffset > 0)
				{
					num = base.SecurityOffset;
				}
				else if (base.CoffSymbolTableOffset > 0)
				{
					fileStream.Seek(base.PEOffset + 12, SeekOrigin.Begin);
					for (int i = 0; i < 8; i++)
					{
						fileStream.WriteByte(0);
					}
					num = base.CoffSymbolTableOffset;
				}
				else
				{
					num = (int)fileStream.Length;
				}
				int num2 = num & 7;
				if (num2 > 0)
				{
					num2 = 8 - num2;
				}
				byte[] bytes = Mono.Security.BitConverterLE.GetBytes(num + num2);
				if (base.PE64)
				{
					fileStream.Seek(base.PEOffset + 168, SeekOrigin.Begin);
				}
				else
				{
					fileStream.Seek(base.PEOffset + 152, SeekOrigin.Begin);
				}
				fileStream.Write(bytes, 0, 4);
				int num3 = asn.Length + 8;
				int num4 = num3 & 7;
				if (num4 > 0)
				{
					num4 = 8 - num4;
				}
				bytes = Mono.Security.BitConverterLE.GetBytes(num3 + num4);
				if (base.PE64)
				{
					fileStream.Seek(base.PEOffset + 168 + 4, SeekOrigin.Begin);
				}
				else
				{
					fileStream.Seek(base.PEOffset + 156, SeekOrigin.Begin);
				}
				fileStream.Write(bytes, 0, 4);
				fileStream.Seek(num, SeekOrigin.Begin);
				if (num2 > 0)
				{
					byte[] array = new byte[num2];
					fileStream.Write(array, 0, array.Length);
				}
				fileStream.Write(bytes, 0, bytes.Length);
				bytes = Mono.Security.BitConverterLE.GetBytes((short)512);
				fileStream.Write(bytes, 0, bytes.Length);
				bytes = Mono.Security.BitConverterLE.GetBytes((short)2);
				fileStream.Write(bytes, 0, bytes.Length);
				fileStream.Write(asn, 0, asn.Length);
				if (num4 > 0)
				{
					byte[] array2 = new byte[num4];
					fileStream.Write(array2, 0, array2.Length);
				}
				fileStream.Close();
			}
			return true;
		}

		public bool Sign(string fileName)
		{
			try
			{
				Open(fileName);
				HashAlgorithm hashAlgorithm = HashAlgorithm.Create(Hash);
				byte[] fileHash = GetHash(hashAlgorithm);
				byte[] signature = Header(fileHash, Hash);
				if (timestamp != null)
				{
					byte[] response = Timestamp(signature);
					ProcessTimestamp(response);
				}
				PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo("1.2.840.113549.1.7.2");
				contentInfo.Content.Add(pkcs7.ASN1);
				authenticode = contentInfo.ASN1;
				Close();
				return Save(fileName, authenticode.GetBytes());
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
			return false;
		}

		public bool Timestamp(string fileName)
		{
			try
			{
				byte[] signature = new AuthenticodeDeformatter(fileName).Signature;
				if (signature != null)
				{
					Open(fileName);
					PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(signature);
					pkcs7 = new PKCS7.SignedData(contentInfo.Content);
					byte[] bytes = Timestamp(pkcs7.SignerInfo.Signature);
					ASN1 aSN = new ASN1(Convert.FromBase64String(Encoding.ASCII.GetString(bytes)));
					ASN1 aSN2 = new ASN1(signature);
					ASN1 aSN3 = aSN2.Element(1, 160);
					if (aSN3 == null)
					{
						return false;
					}
					ASN1 aSN4 = aSN3.Element(0, 48);
					if (aSN4 == null)
					{
						return false;
					}
					ASN1 aSN5 = aSN4.Element(3, 160);
					if (aSN5 == null)
					{
						aSN5 = new ASN1(160);
						aSN4.Add(aSN5);
					}
					for (int i = 0; i < aSN[1][0][3].Count; i++)
					{
						aSN5.Add(aSN[1][0][3][i]);
					}
					ASN1 aSN6 = aSN4[aSN4.Count - 1][0];
					ASN1 aSN7 = aSN6[aSN6.Count - 1];
					if (aSN7.Tag != 161)
					{
						aSN7 = new ASN1(161);
						aSN6.Add(aSN7);
					}
					aSN7.Add(Attribute("1.2.840.113549.1.9.6", aSN[1][0][4][0]));
					return Save(fileName, aSN2.GetBytes());
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
			return false;
		}
	}
	public class PrivateKey
	{
		private const uint magic = 2964713758u;

		private bool encrypted;

		private RSA rsa;

		private bool weak;

		private int keyType;

		public bool Encrypted => encrypted;

		public int KeyType
		{
			get
			{
				return keyType;
			}
			set
			{
				keyType = value;
			}
		}

		public RSA RSA
		{
			get
			{
				return rsa;
			}
			set
			{
				rsa = value;
			}
		}

		public bool Weak
		{
			get
			{
				if (!encrypted)
				{
					return true;
				}
				return weak;
			}
			set
			{
				weak = value;
			}
		}

		public PrivateKey()
		{
			keyType = 2;
		}

		public PrivateKey(byte[] data, string password)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (!Decode(data, password))
			{
				throw new CryptographicException(global::Locale.GetText("Invalid data and/or password"));
			}
		}

		private byte[] DeriveKey(byte[] salt, string password)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(password);
			SHA1 sHA = SHA1.Create();
			sHA.TransformBlock(salt, 0, salt.Length, salt, 0);
			sHA.TransformFinalBlock(bytes, 0, bytes.Length);
			byte[] array = new byte[16];
			Buffer.BlockCopy(sHA.Hash, 0, array, 0, 16);
			sHA.Clear();
			Array.Clear(bytes, 0, bytes.Length);
			return array;
		}

		private bool Decode(byte[] pvk, string password)
		{
			if (Mono.Security.BitConverterLE.ToUInt32(pvk, 0) != 2964713758u)
			{
				return false;
			}
			if (Mono.Security.BitConverterLE.ToUInt32(pvk, 4) != 0)
			{
				return false;
			}
			keyType = Mono.Security.BitConverterLE.ToInt32(pvk, 8);
			encrypted = Mono.Security.BitConverterLE.ToUInt32(pvk, 12) == 1;
			int num = Mono.Security.BitConverterLE.ToInt32(pvk, 16);
			int num2 = Mono.Security.BitConverterLE.ToInt32(pvk, 20);
			byte[] array = new byte[num2];
			Buffer.BlockCopy(pvk, 24 + num, array, 0, num2);
			if (num > 0)
			{
				if (password == null)
				{
					return false;
				}
				byte[] array2 = new byte[num];
				Buffer.BlockCopy(pvk, 24, array2, 0, num);
				byte[] array3 = DeriveKey(array2, password);
				RC4.Create().CreateDecryptor(array3, null).TransformBlock(array, 8, array.Length - 8, array, 8);
				try
				{
					rsa = CryptoConvert.FromCapiPrivateKeyBlob(array);
					weak = false;
				}
				catch (CryptographicException)
				{
					weak = true;
					Buffer.BlockCopy(pvk, 24 + num, array, 0, num2);
					Array.Clear(array3, 5, 11);
					RC4.Create().CreateDecryptor(array3, null).TransformBlock(array, 8, array.Length - 8, array, 8);
					rsa = CryptoConvert.FromCapiPrivateKeyBlob(array);
				}
				Array.Clear(array3, 0, array3.Length);
			}
			else
			{
				weak = true;
				rsa = CryptoConvert.FromCapiPrivateKeyBlob(array);
				Array.Clear(array, 0, array.Length);
			}
			Array.Clear(pvk, 0, pvk.Length);
			return rsa != null;
		}

		public void Save(string filename)
		{
			Save(filename, null);
		}

		public void Save(string filename, string password)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			byte[] array = null;
			FileStream fileStream = File.Open(filename, FileMode.Create, FileAccess.Write);
			try
			{
				byte[] buffer = new byte[4];
				byte[] bytes = Mono.Security.BitConverterLE.GetBytes(2964713758u);
				fileStream.Write(bytes, 0, 4);
				fileStream.Write(buffer, 0, 4);
				bytes = Mono.Security.BitConverterLE.GetBytes(keyType);
				fileStream.Write(bytes, 0, 4);
				encrypted = password != null;
				array = CryptoConvert.ToCapiPrivateKeyBlob(rsa);
				if (encrypted)
				{
					bytes = Mono.Security.BitConverterLE.GetBytes(1);
					fileStream.Write(bytes, 0, 4);
					bytes = Mono.Security.BitConverterLE.GetBytes(16);
					fileStream.Write(bytes, 0, 4);
					bytes = Mono.Security.BitConverterLE.GetBytes(array.Length);
					fileStream.Write(bytes, 0, 4);
					byte[] array2 = new byte[16];
					RC4 rC = RC4.Create();
					byte[] array3 = null;
					try
					{
						RandomNumberGenerator.Create().GetBytes(array2);
						fileStream.Write(array2, 0, array2.Length);
						array3 = DeriveKey(array2, password);
						if (Weak)
						{
							Array.Clear(array3, 5, 11);
						}
						rC.CreateEncryptor(array3, null).TransformBlock(array, 8, array.Length - 8, array, 8);
					}
					finally
					{
						Array.Clear(array2, 0, array2.Length);
						Array.Clear(array3, 0, array3.Length);
						rC.Clear();
					}
				}
				else
				{
					fileStream.Write(buffer, 0, 4);
					fileStream.Write(buffer, 0, 4);
					bytes = Mono.Security.BitConverterLE.GetBytes(array.Length);
					fileStream.Write(bytes, 0, 4);
				}
				fileStream.Write(array, 0, array.Length);
			}
			finally
			{
				Array.Clear(array, 0, array.Length);
				fileStream.Close();
			}
		}

		public static PrivateKey CreateFromFile(string filename)
		{
			return CreateFromFile(filename, null);
		}

		public static PrivateKey CreateFromFile(string filename, string password)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			byte[] array = null;
			using (FileStream fileStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
			}
			return new PrivateKey(array, password);
		}
	}
	public class SoftwarePublisherCertificate
	{
		private PKCS7.SignedData pkcs7;

		private const string header = "-----BEGIN PKCS7-----";

		private const string footer = "-----END PKCS7-----";

		public Mono.Security.X509.X509CertificateCollection Certificates => pkcs7.Certificates;

		public ArrayList Crls => pkcs7.Crls;

		public SoftwarePublisherCertificate()
		{
			pkcs7 = new PKCS7.SignedData();
			pkcs7.ContentInfo.ContentType = "1.2.840.113549.1.7.1";
		}

		public SoftwarePublisherCertificate(byte[] data)
			: this()
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(data);
			if (contentInfo.ContentType != "1.2.840.113549.1.7.2")
			{
				throw new ArgumentException(global::Locale.GetText("Unsupported ContentType"));
			}
			pkcs7 = new PKCS7.SignedData(contentInfo.Content);
		}

		public byte[] GetBytes()
		{
			PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo("1.2.840.113549.1.7.2");
			contentInfo.Content.Add(pkcs7.ASN1);
			return contentInfo.GetBytes();
		}

		public static SoftwarePublisherCertificate CreateFromFile(string filename)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			byte[] array = null;
			using (FileStream fileStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
			}
			if (array.Length < 2)
			{
				return null;
			}
			if (array[0] != 48)
			{
				try
				{
					array = PEM(array);
				}
				catch (Exception inner)
				{
					throw new CryptographicException("Invalid encoding", inner);
				}
			}
			return new SoftwarePublisherCertificate(array);
		}

		private static byte[] PEM(byte[] data)
		{
			string text = ((data[1] == 0) ? Encoding.Unicode.GetString(data) : Encoding.ASCII.GetString(data));
			int num = text.IndexOf("-----BEGIN PKCS7-----") + "-----BEGIN PKCS7-----".Length;
			int num2 = text.IndexOf("-----END PKCS7-----", num);
			return Convert.FromBase64String((num == -1 || num2 == -1) ? text : text.Substring(num, num2 - num));
		}
	}
}
namespace Mono.Math
{
	public class BigInteger
	{
		public enum Sign
		{
			Negative = -1,
			Zero,
			Positive
		}

		public sealed class ModulusRing
		{
			private BigInteger mod;

			private BigInteger constant;

			public ModulusRing(BigInteger modulus)
			{
				mod = modulus;
				uint num = mod.length << 1;
				constant = new BigInteger(Sign.Positive, num + 1);
				constant.data[num] = 1u;
				constant /= mod;
			}

			public void BarrettReduction(BigInteger x)
			{
				BigInteger bigInteger = mod;
				uint length = bigInteger.length;
				uint num = length + 1;
				uint num2 = length - 1;
				if (x.length >= length)
				{
					if (x.data.Length < x.length)
					{
						throw new IndexOutOfRangeException("x out of range");
					}
					BigInteger bigInteger2 = new BigInteger(Sign.Positive, x.length - num2 + constant.length);
					Kernel.Multiply(x.data, num2, x.length - num2, constant.data, 0u, constant.length, bigInteger2.data, 0u);
					uint length2 = ((x.length > num) ? num : x.length);
					x.length = length2;
					x.Normalize();
					BigInteger bigInteger3 = new BigInteger(Sign.Positive, num);
					Kernel.MultiplyMod2p32pmod(bigInteger2.data, (int)num, (int)(bigInteger2.length - num), bigInteger.data, 0, (int)bigInteger.length, bigInteger3.data, 0, (int)num);
					bigInteger3.Normalize();
					if (bigInteger3 <= x)
					{
						Kernel.MinusEq(x, bigInteger3);
					}
					else
					{
						BigInteger bigInteger4 = new BigInteger(Sign.Positive, num + 1);
						bigInteger4.data[num] = 1u;
						Kernel.MinusEq(bigInteger4, bigInteger3);
						Kernel.PlusEq(x, bigInteger4);
					}
					while (x >= bigInteger)
					{
						Kernel.MinusEq(x, bigInteger);
					}
				}
			}

			public BigInteger Multiply(BigInteger a, BigInteger b)
			{
				if (a == 0u || b == 0u)
				{
					return 0;
				}
				if (a > mod)
				{
					a %= mod;
				}
				if (b > mod)
				{
					b %= mod;
				}
				BigInteger bigInteger = a * b;
				BarrettReduction(bigInteger);
				return bigInteger;
			}

			public BigInteger Difference(BigInteger a, BigInteger b)
			{
				Sign sign = Kernel.Compare(a, b);
				BigInteger bigInteger;
				switch (sign)
				{
				case Sign.Zero:
					return 0;
				case Sign.Positive:
					bigInteger = a - b;
					break;
				case Sign.Negative:
					bigInteger = b - a;
					break;
				default:
					throw new Exception();
				}
				if (bigInteger >= mod)
				{
					if (bigInteger.length >= mod.length << 1)
					{
						bigInteger %= mod;
					}
					else
					{
						BarrettReduction(bigInteger);
					}
				}
				if (sign == Sign.Negative)
				{
					bigInteger = mod - bigInteger;
				}
				return bigInteger;
			}

			public BigInteger Pow(BigInteger a, BigInteger k)
			{
				BigInteger bigInteger = new BigInteger(1u);
				if (k == 0u)
				{
					return bigInteger;
				}
				BigInteger bigInteger2 = a;
				if (k.TestBit(0))
				{
					bigInteger = a;
				}
				for (int i = 1; i < k.BitCount(); i++)
				{
					bigInteger2 = Multiply(bigInteger2, bigInteger2);
					if (k.TestBit(i))
					{
						bigInteger = Multiply(bigInteger2, bigInteger);
					}
				}
				return bigInteger;
			}

			[CLSCompliant(false)]
			public BigInteger Pow(uint b, BigInteger exp)
			{
				return Pow(new BigInteger(b), exp);
			}
		}

		private sealed class Kernel
		{
			public static BigInteger AddSameSign(BigInteger bi1, BigInteger bi2)
			{
				uint num = 0u;
				uint[] data;
				uint length;
				uint[] data2;
				uint length2;
				if (bi1.length < bi2.length)
				{
					data = bi2.data;
					length = bi2.length;
					data2 = bi1.data;
					length2 = bi1.length;
				}
				else
				{
					data = bi1.data;
					length = bi1.length;
					data2 = bi2.data;
					length2 = bi2.length;
				}
				BigInteger bigInteger = new BigInteger(Sign.Positive, length + 1);
				uint[] data3 = bigInteger.data;
				ulong num2 = 0uL;
				do
				{
					num2 = (ulong)((long)data[num] + (long)data2[num]) + num2;
					data3[num] = (uint)num2;
					num2 >>= 32;
				}
				while (++num < length2);
				bool flag = num2 != 0;
				if (flag)
				{
					if (num < length)
					{
						do
						{
							flag = (data3[num] = data[num] + 1) == 0;
						}
						while (++num < length && flag);
					}
					if (flag)
					{
						data3[num] = 1u;
						num = (bigInteger.length = num + 1);
						return bigInteger;
					}
				}
				if (num < length)
				{
					do
					{
						data3[num] = data[num];
					}
					while (++num < length);
				}
				bigInteger.Normalize();
				return bigInteger;
			}

			public static BigInteger Subtract(BigInteger big, BigInteger small)
			{
				BigInteger bigInteger = new BigInteger(Sign.Positive, big.length);
				uint[] data = bigInteger.data;
				uint[] data2 = big.data;
				uint[] data3 = small.data;
				uint num = 0u;
				uint num2 = 0u;
				do
				{
					uint num3 = data3[num];
					num2 = ((((num3 += num2) < num2) | ((data[num] = data2[num] - num3) > ~num3)) ? 1u : 0u);
				}
				while (++num < small.length);
				if (num != big.length)
				{
					if (num2 == 1)
					{
						do
						{
							data[num] = data2[num] - 1;
						}
						while (data2[num++] == 0 && num < big.length);
						if (num == big.length)
						{
							goto IL_00b8;
						}
					}
					do
					{
						data[num] = data2[num];
					}
					while (++num < big.length);
				}
				goto IL_00b8;
				IL_00b8:
				bigInteger.Normalize();
				return bigInteger;
			}

			public static void MinusEq(BigInteger big, BigInteger small)
			{
				uint[] data = big.data;
				uint[] data2 = small.data;
				uint num = 0u;
				uint num2 = 0u;
				do
				{
					uint num3 = data2[num];
					num2 = ((((num3 += num2) < num2) | ((data[num] -= num3) > ~num3)) ? 1u : 0u);
				}
				while (++num < small.length);
				if (num != big.length && num2 == 1)
				{
					do
					{
						data[num]--;
					}
					while (data[num++] == 0 && num < big.length);
				}
				while (big.length != 0 && big.data[big.length - 1] == 0)
				{
					big.length--;
				}
				if (big.length == 0)
				{
					big.length++;
				}
			}

			public static void PlusEq(BigInteger bi1, BigInteger bi2)
			{
				uint num = 0u;
				bool flag = false;
				uint[] data;
				uint length;
				uint[] data2;
				uint length2;
				if (bi1.length < bi2.length)
				{
					flag = true;
					data = bi2.data;
					length = bi2.length;
					data2 = bi1.data;
					length2 = bi1.length;
				}
				else
				{
					data = bi1.data;
					length = bi1.length;
					data2 = bi2.data;
					length2 = bi2.length;
				}
				uint[] data3 = bi1.data;
				ulong num2 = 0uL;
				do
				{
					num2 += (ulong)((long)data[num] + (long)data2[num]);
					data3[num] = (uint)num2;
					num2 >>= 32;
				}
				while (++num < length2);
				bool flag2 = num2 != 0;
				if (flag2)
				{
					if (num < length)
					{
						do
						{
							flag2 = (data3[num] = data[num] + 1) == 0;
						}
						while (++num < length && flag2);
					}
					if (flag2)
					{
						data3[num] = 1u;
						num = (bi1.length = num + 1);
						return;
					}
				}
				if (flag && num < length - 1)
				{
					do
					{
						data3[num] = data[num];
					}
					while (++num < length);
				}
				bi1.length = length + 1;
				bi1.Normalize();
			}

			public static Sign Compare(BigInteger bi1, BigInteger bi2)
			{
				uint num = bi1.length;
				uint num2 = bi2.length;
				while (num != 0 && bi1.data[num - 1] == 0)
				{
					num--;
				}
				while (num2 != 0 && bi2.data[num2 - 1] == 0)
				{
					num2--;
				}
				if (num == 0 && num2 == 0)
				{
					return Sign.Zero;
				}
				if (num < num2)
				{
					return Sign.Negative;
				}
				if (num > num2)
				{
					return Sign.Positive;
				}
				uint num3 = num - 1;
				while (num3 != 0 && bi1.data[num3] == bi2.data[num3])
				{
					num3--;
				}
				if (bi1.data[num3] < bi2.data[num3])
				{
					return Sign.Negative;
				}
				if (bi1.data[num3] > bi2.data[num3])
				{
					return Sign.Positive;
				}
				return Sign.Zero;
			}

			public static uint SingleByteDivideInPlace(BigInteger n, uint d)
			{
				ulong num = 0uL;
				uint length = n.length;
				while (length-- != 0)
				{
					num <<= 32;
					num |= n.data[length];
					n.data[length] = (uint)(num / d);
					num %= d;
				}
				n.Normalize();
				return (uint)num;
			}

			public static uint DwordMod(BigInteger n, uint d)
			{
				ulong num = 0uL;
				uint length = n.length;
				while (length-- != 0)
				{
					num <<= 32;
					num |= n.data[length];
					num %= d;
				}
				return (uint)num;
			}

			public static BigInteger DwordDiv(BigInteger n, uint d)
			{
				BigInteger bigInteger = new BigInteger(Sign.Positive, n.length);
				ulong num = 0uL;
				uint length = n.length;
				while (length-- != 0)
				{
					num <<= 32;
					num |= n.data[length];
					bigInteger.data[length] = (uint)(num / d);
					num %= d;
				}
				bigInteger.Normalize();
				return bigInteger;
			}

			public static BigInteger[] DwordDivMod(BigInteger n, uint d)
			{
				BigInteger bigInteger = new BigInteger(Sign.Positive, n.length);
				ulong num = 0uL;
				uint length = n.length;
				while (length-- != 0)
				{
					num <<= 32;
					num |= n.data[length];
					bigInteger.data[length] = (uint)(num / d);
					num %= d;
				}
				bigInteger.Normalize();
				BigInteger bigInteger2 = (uint)num;
				return new BigInteger[2] { bigInteger, bigInteger2 };
			}

			public static BigInteger[] multiByteDivide(BigInteger bi1, BigInteger bi2)
			{
				if (Compare(bi1, bi2) == Sign.Negative)
				{
					return new BigInteger[2]
					{
						0,
						new BigInteger(bi1)
					};
				}
				bi1.Normalize();
				bi2.Normalize();
				if (bi2.length == 1)
				{
					return DwordDivMod(bi1, bi2.data[0]);
				}
				uint num = bi1.length + 1;
				int num2 = (int)(bi2.length + 1);
				uint num3 = 2147483648u;
				uint num4 = bi2.data[bi2.length - 1];
				int num5 = 0;
				int num6 = (int)(bi1.length - bi2.length);
				while (num3 != 0 && (num4 & num3) == 0)
				{
					num5++;
					num3 >>= 1;
				}
				BigInteger bigInteger = new BigInteger(Sign.Positive, bi1.length - bi2.length + 1);
				BigInteger bigInteger2 = bi1 << num5;
				uint[] data = bigInteger2.data;
				bi2 <<= num5;
				int num7 = (int)(num - bi2.length);
				int num8 = (int)(num - 1);
				uint num9 = bi2.data[bi2.length - 1];
				ulong num10 = bi2.data[bi2.length - 2];
				while (num7 > 0)
				{
					ulong num11 = ((ulong)data[num8] << 32) + data[num8 - 1];
					ulong num12 = num11 / num9;
					ulong num13 = num11 % num9;
					while (num12 == 4294967296L || num12 * num10 > (num13 << 32) + data[num8 - 2])
					{
						num12--;
						num13 += num9;
						if (num13 >= 4294967296L)
						{
							break;
						}
					}
					uint num14 = 0u;
					int num15 = num8 - num2 + 1;
					ulong num16 = 0uL;
					uint num17 = (uint)num12;
					do
					{
						num16 += (ulong)((long)bi2.data[num14] * (long)num17);
						uint num18 = data[num15];
						data[num15] -= (uint)(int)num16;
						num16 >>= 32;
						if (data[num15] > num18)
						{
							num16++;
						}
						num14++;
						num15++;
					}
					while (num14 < num2);
					num15 = num8 - num2 + 1;
					num14 = 0u;
					if (num16 != 0L)
					{
						num17--;
						ulong num19 = 0uL;
						do
						{
							num19 = (ulong)((long)data[num15] + (long)bi2.data[num14]) + num19;
							data[num15] = (uint)num19;
							num19 >>= 32;
							num14++;
							num15++;
						}
						while (num14 < num2);
					}
					bigInteger.data[num6--] = num17;
					num8--;
					num7--;
				}
				bigInteger.Normalize();
				bigInteger2.Normalize();
				BigInteger[] array = new BigInteger[2] { bigInteger, bigInteger2 };
				if (num5 != 0)
				{
					BigInteger[] array2 = array;
					array2[1] >>= num5;
				}
				return array;
			}

			public static BigInteger LeftShift(BigInteger bi, int n)
			{
				if (n == 0)
				{
					return new BigInteger(bi, bi.length + 1);
				}
				int num = n >> 5;
				n &= 0x1F;
				BigInteger bigInteger = new BigInteger(Sign.Positive, bi.length + 1 + (uint)num);
				uint num2 = 0u;
				uint length = bi.length;
				if (n != 0)
				{
					uint num3 = 0u;
					for (; num2 < length; num2++)
					{
						uint num4 = bi.data[num2];
						bigInteger.data[num2 + num] = (num4 << n) | num3;
						num3 = num4 >> 32 - n;
					}
					bigInteger.data[num2 + num] = num3;
				}
				else
				{
					for (; num2 < length; num2++)
					{
						bigInteger.data[num2 + num] = bi.data[num2];
					}
				}
				bigInteger.Normalize();
				return bigInteger;
			}

			public static BigInteger RightShift(BigInteger bi, int n)
			{
				if (n == 0)
				{
					return new BigInteger(bi);
				}
				int num = n >> 5;
				int num2 = n & 0x1F;
				BigInteger bigInteger = new BigInteger(Sign.Positive, (uint)((int)bi.length - num + 1));
				uint num3 = (uint)(bigInteger.data.Length - 1);
				if (num2 != 0)
				{
					uint num4 = 0u;
					while (num3-- != 0)
					{
						uint num5 = bi.data[num3 + num];
						bigInteger.data[num3] = (num5 >> n) | num4;
						num4 = num5 << 32 - n;
					}
				}
				else
				{
					while (num3-- != 0)
					{
						bigInteger.data[num3] = bi.data[num3 + num];
					}
				}
				bigInteger.Normalize();
				return bigInteger;
			}

			public static BigInteger MultiplyByDword(BigInteger n, uint f)
			{
				BigInteger bigInteger = new BigInteger(Sign.Positive, n.length + 1);
				uint num = 0u;
				ulong num2 = 0uL;
				do
				{
					num2 += (ulong)((long)n.data[num] * (long)f);
					bigInteger.data[num] = (uint)num2;
					num2 >>= 32;
				}
				while (++num < n.length);
				bigInteger.data[num] = (uint)num2;
				bigInteger.Normalize();
				return bigInteger;
			}

			public unsafe static void Multiply(uint[] x, uint xOffset, uint xLen, uint[] y, uint yOffset, uint yLen, uint[] d, uint dOffset)
			{
				fixed (uint* ptr = x)
				{
					fixed (uint* ptr2 = y)
					{
						fixed (uint* ptr3 = d)
						{
							uint* ptr4 = ptr + xOffset;
							uint* ptr5 = ptr4 + xLen;
							uint* ptr6 = ptr2 + yOffset;
							uint* ptr7 = ptr6 + yLen;
							uint* ptr8 = ptr3 + dOffset;
							while (ptr4 < ptr5)
							{
								if (*ptr4 != 0)
								{
									ulong num = 0uL;
									uint* ptr9 = ptr8;
									uint* ptr10 = ptr6;
									while (ptr10 < ptr7)
									{
										num += (ulong)((long)(*ptr4) * (long)(*ptr10) + *ptr9);
										*ptr9 = (uint)num;
										num >>= 32;
										ptr10++;
										ptr9++;
									}
									if (num != 0L)
									{
										*ptr9 = (uint)num;
									}
								}
								ptr4++;
								ptr8++;
							}
						}
					}
				}
			}

			public unsafe static void MultiplyMod2p32pmod(uint[] x, int xOffset, int xLen, uint[] y, int yOffest, int yLen, uint[] d, int dOffset, int mod)
			{
				fixed (uint* ptr = x)
				{
					fixed (uint* ptr2 = y)
					{
						fixed (uint* ptr3 = d)
						{
							uint* ptr4 = ptr + xOffset;
							uint* ptr5 = ptr4 + xLen;
							uint* ptr6 = ptr2 + yOffest;
							uint* ptr7 = ptr6 + yLen;
							uint* ptr8 = ptr3 + dOffset;
							uint* ptr9 = ptr8 + mod;
							while (ptr4 < ptr5)
							{
								if (*ptr4 != 0)
								{
									ulong num = 0uL;
									uint* ptr10 = ptr8;
									uint* ptr11 = ptr6;
									while (ptr11 < ptr7 && ptr10 < ptr9)
									{
										num += (ulong)((long)(*ptr4) * (long)(*ptr11) + *ptr10);
										*ptr10 = (uint)num;
										num >>= 32;
										ptr11++;
										ptr10++;
									}
									if (num != 0L && ptr10 < ptr9)
									{
										*ptr10 = (uint)num;
									}
								}
								ptr4++;
								ptr8++;
							}
						}
					}
				}
			}

			public unsafe static void SquarePositive(BigInteger bi, ref uint[] wkSpace)
			{
				uint[] array = wkSpace;
				wkSpace = bi.data;
				uint[] data = bi.data;
				uint length = bi.length;
				bi.data = array;
				fixed (uint* ptr = data)
				{
					fixed (uint* ptr2 = array)
					{
						uint* ptr3 = ptr2 + array.Length;
						for (uint* ptr4 = ptr2; ptr4 < ptr3; ptr4++)
						{
							*ptr4 = 0u;
						}
						uint* ptr5 = ptr;
						uint* ptr6 = ptr2;
						uint num = 0u;
						while (num < length)
						{
							if (*ptr5 != 0)
							{
								ulong num2 = 0uL;
								uint num3 = *ptr5;
								uint* ptr7 = ptr5 + 1;
								uint* ptr8 = ptr6 + 2 * num + 1;
								uint num4 = num + 1;
								while (num4 < length)
								{
									num2 += (ulong)((long)num3 * (long)(*ptr7) + *ptr8);
									*ptr8 = (uint)num2;
									num2 >>= 32;
									num4++;
									ptr8++;
									ptr7++;
								}
								if (num2 != 0L)
								{
									*ptr8 = (uint)num2;
								}
							}
							num++;
							ptr5++;
						}
						ptr6 = ptr2;
						uint num5 = 0u;
						for (; ptr6 < ptr3; ptr6++)
						{
							uint num6 = *ptr6;
							*ptr6 = (num6 << 1) | num5;
							num5 = num6 >> 31;
						}
						if (num5 != 0)
						{
							*ptr6 = num5;
						}
						ptr5 = ptr;
						ptr6 = ptr2;
						uint* ptr9 = ptr5 + length;
						while (ptr5 < ptr9)
						{
							ulong num7 = (ulong)((long)(*ptr5) * (long)(*ptr5) + *ptr6);
							*ptr6 = (uint)num7;
							num7 >>= 32;
							*(++ptr6) += (uint)(int)num7;
							if (*ptr6 < (uint)num7)
							{
								uint* ptr10 = ptr6;
								(*(++ptr10))++;
								while (*(ptr10++) == 0)
								{
									(*ptr10)++;
								}
							}
							ptr5++;
							ptr6++;
						}
						bi.length <<= 1;
						while (ptr2[bi.length - 1] == 0 && bi.length > 1)
						{
							bi.length--;
						}
					}
				}
			}

			public static BigInteger gcd(BigInteger a, BigInteger b)
			{
				BigInteger bigInteger = a;
				BigInteger bigInteger2 = b;
				BigInteger bigInteger3 = bigInteger2;
				while (bigInteger.length > 1)
				{
					bigInteger3 = bigInteger;
					bigInteger = bigInteger2 % bigInteger;
					bigInteger2 = bigInteger3;
				}
				if (bigInteger == 0u)
				{
					return bigInteger3;
				}
				uint num = bigInteger.data[0];
				uint num2 = bigInteger2 % num;
				int num3 = 0;
				while (((num2 | num) & 1) == 0)
				{
					num2 >>= 1;
					num >>= 1;
					num3++;
				}
				while (num2 != 0)
				{
					while ((num2 & 1) == 0)
					{
						num2 >>= 1;
					}
					while ((num & 1) == 0)
					{
						num >>= 1;
					}
					if (num2 >= num)
					{
						num2 = num2 - num >> 1;
					}
					else
					{
						num = num - num2 >> 1;
					}
				}
				return num << num3;
			}

			public static uint modInverse(BigInteger bi, uint modulus)
			{
				uint num = modulus;
				uint num2 = bi % modulus;
				uint num3 = 0u;
				uint num4 = 1u;
				while (true)
				{
					switch (num2)
					{
					case 1u:
						return num4;
					default:
						num3 += num / num2 * num4;
						num %= num2;
						switch (num)
						{
						case 1u:
							return modulus - num3;
						default:
							goto IL_002d;
						case 0u:
							break;
						}
						break;
					case 0u:
						break;
					}
					break;
					IL_002d:
					num4 += num2 / num * num3;
					num2 %= num;
				}
				return 0u;
			}

			public static BigInteger modInverse(BigInteger bi, BigInteger modulus)
			{
				if (modulus.length == 1)
				{
					return modInverse(bi, modulus.data[0]);
				}
				BigInteger[] array = new BigInteger[2] { 0, 1 };
				BigInteger[] array2 = new BigInteger[2];
				BigInteger[] array3 = new BigInteger[2] { 0, 0 };
				int num = 0;
				BigInteger bi2 = modulus;
				BigInteger bigInteger = bi;
				ModulusRing modulusRing = new ModulusRing(modulus);
				while (bigInteger != 0u)
				{
					if (num > 1)
					{
						BigInteger bigInteger2 = modulusRing.Difference(array[0], array[1] * array2[0]);
						array[0] = array[1];
						array[1] = bigInteger2;
					}
					BigInteger[] array4 = multiByteDivide(bi2, bigInteger);
					array2[0] = array2[1];
					array2[1] = array4[0];
					array3[0] = array3[1];
					array3[1] = array4[1];
					bi2 = bigInteger;
					bigInteger = array4[1];
					num++;
				}
				if (array3[0] != 1u)
				{
					throw new ArithmeticException("No inverse!");
				}
				return modulusRing.Difference(array[0], array[1] * array2[0]);
			}
		}

		private uint length = 1u;

		private uint[] data;

		private const uint DEFAULT_LEN = 20u;

		internal static readonly uint[] smallPrimes = new uint[783]
		{
			2u, 3u, 5u, 7u, 11u, 13u, 17u, 19u, 23u, 29u,
			31u, 37u, 41u, 43u, 47u, 53u, 59u, 61u, 67u, 71u,
			73u, 79u, 83u, 89u, 97u, 101u, 103u, 107u, 109u, 113u,
			127u, 131u, 137u, 139u, 149u, 151u, 157u, 163u, 167u, 173u,
			179u, 181u, 191u, 193u, 197u, 199u, 211u, 223u, 227u, 229u,
			233u, 239u, 241u, 251u, 257u, 263u, 269u, 271u, 277u, 281u,
			283u, 293u, 307u, 311u, 313u, 317u, 331u, 337u, 347u, 349u,
			353u, 359u, 367u, 373u, 379u, 383u, 389u, 397u, 401u, 409u,
			419u, 421u, 431u, 433u, 439u, 443u, 449u, 457u, 461u, 463u,
			467u, 479u, 487u, 491u, 499u, 503u, 509u, 521u, 523u, 541u,
			547u, 557u, 563u, 569u, 571u, 577u, 587u, 593u, 599u, 601u,
			607u, 613u, 617u, 619u, 631u, 641u, 643u, 647u, 653u, 659u,
			661u, 673u, 677u, 683u, 691u, 701u, 709u, 719u, 727u, 733u,
			739u, 743u, 751u, 757u, 761u, 769u, 773u, 787u, 797u, 809u,
			811u, 821u, 823u, 827u, 829u, 839u, 853u, 857u, 859u, 863u,
			877u, 881u, 883u, 887u, 907u, 911u, 919u, 929u, 937u, 941u,
			947u, 953u, 967u, 971u, 977u, 983u, 991u, 997u, 1009u, 1013u,
			1019u, 1021u, 1031u, 1033u, 1039u, 1049u, 1051u, 1061u, 1063u, 1069u,
			1087u, 1091u, 1093u, 1097u, 1103u, 1109u, 1117u, 1123u, 1129u, 1151u,
			1153u, 1163u, 1171u, 1181u, 1187u, 1193u, 1201u, 1213u, 1217u, 1223u,
			1229u, 1231u, 1237u, 1249u, 1259u, 1277u, 1279u, 1283u, 1289u, 1291u,
			1297u, 1301u, 1303u, 1307u, 1319u, 1321u, 1327u, 1361u, 1367u, 1373u,
			1381u, 1399u, 1409u, 1423u, 1427u, 1429u, 1433u, 1439u, 1447u, 1451u,
			1453u, 1459u, 1471u, 1481u, 1483u, 1487u, 1489u, 1493u, 1499u, 1511u,
			1523u, 1531u, 1543u, 1549u, 1553u, 1559u, 1567u, 1571u, 1579u, 1583u,
			1597u, 1601u, 1607u, 1609u, 1613u, 1619u, 1621u, 1627u, 1637u, 1657u,
			1663u, 1667u, 1669u, 1693u, 1697u, 1699u, 1709u, 1721u, 1723u, 1733u,
			1741u, 1747u, 1753u, 1759u, 1777u, 1783u, 1787u, 1789u, 1801u, 1811u,
			1823u, 1831u, 1847u, 1861u, 1867u, 1871u, 1873u, 1877u, 1879u, 1889u,
			1901u, 1907u, 1913u, 1931u, 1933u, 1949u, 1951u, 1973u, 1979u, 1987u,
			1993u, 1997u, 1999u, 2003u, 2011u, 2017u, 2027u, 2029u, 2039u, 2053u,
			2063u, 2069u, 2081u, 2083u, 2087u, 2089u, 2099u, 2111u, 2113u, 2129u,
			2131u, 2137u, 2141u, 2143u, 2153u, 2161u, 2179u, 2203u, 2207u, 2213u,
			2221u, 2237u, 2239u, 2243u, 2251u, 2267u, 2269u, 2273u, 2281u, 2287u,
			2293u, 2297u, 2309u, 2311u, 2333u, 2339u, 2341u, 2347u, 2351u, 2357u,
			2371u, 2377u, 2381u, 2383u, 2389u, 2393u, 2399u, 2411u, 2417u, 2423u,
			2437u, 2441u, 2447u, 2459u, 2467u, 2473u, 2477u, 2503u, 2521u, 2531u,
			2539u, 2543u, 2549u, 2551u, 2557u, 2579u, 2591u, 2593u, 2609u, 2617u,
			2621u, 2633u, 2647u, 2657u, 2659u, 2663u, 2671u, 2677u, 2683u, 2687u,
			2689u, 2693u, 2699u, 2707u, 2711u, 2713u, 2719u, 2729u, 2731u, 2741u,
			2749u, 2753u, 2767u, 2777u, 2789u, 2791u, 2797u, 2801u, 2803u, 2819u,
			2833u, 2837u, 2843u, 2851u, 2857u, 2861u, 2879u, 2887u, 2897u, 2903u,
			2909u, 2917u, 2927u, 2939u, 2953u, 2957u, 2963u, 2969u, 2971u, 2999u,
			3001u, 3011u, 3019u, 3023u, 3037u, 3041u, 3049u, 3061u, 3067u, 3079u,
			3083u, 3089u, 3109u, 3119u, 3121u, 3137u, 3163u, 3167u, 3169u, 3181u,
			3187u, 3191u, 3203u, 3209u, 3217u, 3221u, 3229u, 3251u, 3253u, 3257u,
			3259u, 3271u, 3299u, 3301u, 3307u, 3313u, 3319u, 3323u, 3329u, 3331u,
			3343u, 3347u, 3359u, 3361u, 3371u, 3373u, 3389u, 3391u, 3407u, 3413u,
			3433u, 3449u, 3457u, 3461u, 3463u, 3467u, 3469u, 3491u, 3499u, 3511u,
			3517u, 3527u, 3529u, 3533u, 3539u, 3541u, 3547u, 3557u, 3559u, 3571u,
			3581u, 3583u, 3593u, 3607u, 3613u, 3617u, 3623u, 3631u, 3637u, 3643u,
			3659u, 3671u, 3673u, 3677u, 3691u, 3697u, 3701u, 3709u, 3719u, 3727u,
			3733u, 3739u, 3761u, 3767u, 3769u, 3779u, 3793u, 3797u, 3803u, 3821u,
			3823u, 3833u, 3847u, 3851u, 3853u, 3863u, 3877u, 3881u, 3889u, 3907u,
			3911u, 3917u, 3919u, 3923u, 3929u, 3931u, 3943u, 3947u, 3967u, 3989u,
			4001u, 4003u, 4007u, 4013u, 4019u, 4021u, 4027u, 4049u, 4051u, 4057u,
			4073u, 4079u, 4091u, 4093u, 4099u, 4111u, 4127u, 4129u, 4133u, 4139u,
			4153u, 4157u, 4159u, 4177u, 4201u, 4211u, 4217u, 4219u, 4229u, 4231u,
			4241u, 4243u, 4253u, 4259u, 4261u, 4271u, 4273u, 4283u, 4289u, 4297u,
			4327u, 4337u, 4339u, 4349u, 4357u, 4363u, 4373u, 4391u, 4397u, 4409u,
			4421u, 4423u, 4441u, 4447u, 4451u, 4457u, 4463u, 4481u, 4483u, 4493u,
			4507u, 4513u, 4517u, 4519u, 4523u, 4547u, 4549u, 4561u, 4567u, 4583u,
			4591u, 4597u, 4603u, 4621u, 4637u, 4639u, 4643u, 4649u, 4651u, 4657u,
			4663u, 4673u, 4679u, 4691u, 4703u, 4721u, 4723u, 4729u, 4733u, 4751u,
			4759u, 4783u, 4787u, 4789u, 4793u, 4799u, 4801u, 4813u, 4817u, 4831u,
			4861u, 4871u, 4877u, 4889u, 4903u, 4909u, 4919u, 4931u, 4933u, 4937u,
			4943u, 4951u, 4957u, 4967u, 4969u, 4973u, 4987u, 4993u, 4999u, 5003u,
			5009u, 5011u, 5021u, 5023u, 5039u, 5051u, 5059u, 5077u, 5081u, 5087u,
			5099u, 5101u, 5107u, 5113u, 5119u, 5147u, 5153u, 5167u, 5171u, 5179u,
			5189u, 5197u, 5209u, 5227u, 5231u, 5233u, 5237u, 5261u, 5273u, 5279u,
			5281u, 5297u, 5303u, 5309u, 5323u, 5333u, 5347u, 5351u, 5381u, 5387u,
			5393u, 5399u, 5407u, 5413u, 5417u, 5419u, 5431u, 5437u, 5441u, 5443u,
			5449u, 5471u, 5477u, 5479u, 5483u, 5501u, 5503u, 5507u, 5519u, 5521u,
			5527u, 5531u, 5557u, 5563u, 5569u, 5573u, 5581u, 5591u, 5623u, 5639u,
			5641u, 5647u, 5651u, 5653u, 5657u, 5659u, 5669u, 5683u, 5689u, 5693u,
			5701u, 5711u, 5717u, 5737u, 5741u, 5743u, 5749u, 5779u, 5783u, 5791u,
			5801u, 5807u, 5813u, 5821u, 5827u, 5839u, 5843u, 5849u, 5851u, 5857u,
			5861u, 5867u, 5869u, 5879u, 5881u, 5897u, 5903u, 5923u, 5927u, 5939u,
			5953u, 5981u, 5987u
		};

		private const string WouldReturnNegVal = "Operation would return a negative value";

		private static RandomNumberGenerator rng;

		private static RandomNumberGenerator Rng
		{
			get
			{
				if (rng == null)
				{
					rng = RandomNumberGenerator.Create();
				}
				return rng;
			}
		}

		public BigInteger()
		{
			data = new uint[20];
			length = 20u;
		}

		[CLSCompliant(false)]
		public BigInteger(Sign sign, uint len)
		{
			data = new uint[len];
			length = len;
		}

		public BigInteger(BigInteger bi)
		{
			data = (uint[])bi.data.Clone();
			length = bi.length;
		}

		[CLSCompliant(false)]
		public BigInteger(BigInteger bi, uint len)
		{
			data = new uint[len];
			for (uint num = 0u; num < bi.length; num++)
			{
				data[num] = bi.data[num];
			}
			length = bi.length;
		}

		public BigInteger(byte[] inData)
		{
			if (inData.Length == 0)
			{
				inData = new byte[1];
			}
			length = (uint)inData.Length >> 2;
			int num = inData.Length & 3;
			if (num != 0)
			{
				length++;
			}
			data = new uint[length];
			int num2 = inData.Length - 1;
			int num3 = 0;
			while (num2 >= 3)
			{
				data[num3] = (uint)((inData[num2 - 3] << 24) | (inData[num2 - 2] << 16) | (inData[num2 - 1] << 8) | inData[num2]);
				num2 -= 4;
				num3++;
			}
			switch (num)
			{
			case 1:
				data[length - 1] = inData[0];
				break;
			case 2:
				data[length - 1] = (uint)((inData[0] << 8) | inData[1]);
				break;
			case 3:
				data[length - 1] = (uint)((inData[0] << 16) | (inData[1] << 8) | inData[2]);
				break;
			}
			Normalize();
		}

		[CLSCompliant(false)]
		public BigInteger(uint[] inData)
		{
			if (inData.Length == 0)
			{
				inData = new uint[1];
			}
			length = (uint)inData.Length;
			data = new uint[length];
			int num = (int)(length - 1);
			int num2 = 0;
			while (num >= 0)
			{
				data[num2] = inData[num];
				num--;
				num2++;
			}
			Normalize();
		}

		[CLSCompliant(false)]
		public BigInteger(uint ui)
		{
			data = new uint[1] { ui };
		}

		[CLSCompliant(false)]
		public BigInteger(ulong ul)
		{
			data = new uint[2]
			{
				(uint)ul,
				(uint)(ul >> 32)
			};
			length = 2u;
			Normalize();
		}

		[CLSCompliant(false)]
		public static implicit operator BigInteger(uint value)
		{
			return new BigInteger(value);
		}

		public static implicit operator BigInteger(int value)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			return new BigInteger((uint)value);
		}

		[CLSCompliant(false)]
		public static implicit operator BigInteger(ulong value)
		{
			return new BigInteger(value);
		}

		public static BigInteger Parse(string number)
		{
			if (number == null)
			{
				throw new ArgumentNullException("number");
			}
			int i = 0;
			int num = number.Length;
			bool flag = false;
			BigInteger bigInteger = new BigInteger(0u);
			if (number[i] == '+')
			{
				i++;
			}
			else if (number[i] == '-')
			{
				throw new FormatException("Operation would return a negative value");
			}
			for (; i < num; i++)
			{
				char c = number[i];
				switch (c)
				{
				case '\0':
					i = num;
					continue;
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					bigInteger = bigInteger * 10 + (c - 48);
					flag = true;
					continue;
				}
				if (char.IsWhiteSpace(c))
				{
					for (i++; i < num; i++)
					{
						if (!char.IsWhiteSpace(number[i]))
						{
							throw new FormatException();
						}
					}
					break;
				}
				throw new FormatException();
			}
			if (!flag)
			{
				throw new FormatException();
			}
			return bigInteger;
		}

		public static BigInteger operator +(BigInteger bi1, BigInteger bi2)
		{
			if (bi1 == 0u)
			{
				return new BigInteger(bi2);
			}
			if (bi2 == 0u)
			{
				return new BigInteger(bi1);
			}
			return Kernel.AddSameSign(bi1, bi2);
		}

		public static BigInteger operator -(BigInteger bi1, BigInteger bi2)
		{
			if (bi2 == 0u)
			{
				return new BigInteger(bi1);
			}
			if (bi1 == 0u)
			{
				throw new ArithmeticException("Operation would return a negative value");
			}
			return Kernel.Compare(bi1, bi2) switch
			{
				Sign.Zero => 0, 
				Sign.Positive => Kernel.Subtract(bi1, bi2), 
				Sign.Negative => throw new ArithmeticException("Operation would return a negative value"), 
				_ => throw new Exception(), 
			};
		}

		public static int operator %(BigInteger bi, int i)
		{
			if (i > 0)
			{
				return (int)Kernel.DwordMod(bi, (uint)i);
			}
			return (int)(0 - Kernel.DwordMod(bi, (uint)(-i)));
		}

		[CLSCompliant(false)]
		public static uint operator %(BigInteger bi, uint ui)
		{
			return Kernel.DwordMod(bi, ui);
		}

		public static BigInteger operator %(BigInteger bi1, BigInteger bi2)
		{
			return Kernel.multiByteDivide(bi1, bi2)[1];
		}

		public static BigInteger operator /(BigInteger bi, int i)
		{
			if (i > 0)
			{
				return Kernel.DwordDiv(bi, (uint)i);
			}
			throw new ArithmeticException("Operation would return a negative value");
		}

		public static BigInteger operator /(BigInteger bi1, BigInteger bi2)
		{
			return Kernel.multiByteDivide(bi1, bi2)[0];
		}

		public static BigInteger operator *(BigInteger bi1, BigInteger bi2)
		{
			if (bi1 == 0u || bi2 == 0u)
			{
				return 0;
			}
			if (bi1.data.Length < bi1.length)
			{
				throw new IndexOutOfRangeException("bi1 out of range");
			}
			if (bi2.data.Length < bi2.length)
			{
				throw new IndexOutOfRangeException("bi2 out of range");
			}
			BigInteger bigInteger = new BigInteger(Sign.Positive, bi1.length + bi2.length);
			Kernel.Multiply(bi1.data, 0u, bi1.length, bi2.data, 0u, bi2.length, bigInteger.data, 0u);
			bigInteger.Normalize();
			return bigInteger;
		}

		public static BigInteger operator *(BigInteger bi, int i)
		{
			if (i < 0)
			{
				throw new ArithmeticException("Operation would return a negative value");
			}
			return i switch
			{
				0 => 0, 
				1 => new BigInteger(bi), 
				_ => Kernel.MultiplyByDword(bi, (uint)i), 
			};
		}

		public static BigInteger operator <<(BigInteger bi1, int shiftVal)
		{
			return Kernel.LeftShift(bi1, shiftVal);
		}

		public static BigInteger operator >>(BigInteger bi1, int shiftVal)
		{
			return Kernel.RightShift(bi1, shiftVal);
		}

		public static BigInteger Add(BigInteger bi1, BigInteger bi2)
		{
			return bi1 + bi2;
		}

		public static BigInteger Subtract(BigInteger bi1, BigInteger bi2)
		{
			return bi1 - bi2;
		}

		public static int Modulus(BigInteger bi, int i)
		{
			return bi % i;
		}

		[CLSCompliant(false)]
		public static uint Modulus(BigInteger bi, uint ui)
		{
			return bi % ui;
		}

		public static BigInteger Modulus(BigInteger bi1, BigInteger bi2)
		{
			return bi1 % bi2;
		}

		public static BigInteger Divid(BigInteger bi, int i)
		{
			return bi / i;
		}

		public static BigInteger Divid(BigInteger bi1, BigInteger bi2)
		{
			return bi1 / bi2;
		}

		public static BigInteger Multiply(BigInteger bi1, BigInteger bi2)
		{
			return bi1 * bi2;
		}

		public static BigInteger Multiply(BigInteger bi, int i)
		{
			return bi * i;
		}

		public static BigInteger GenerateRandom(int bits, RandomNumberGenerator rng)
		{
			int num = bits >> 5;
			int num2 = bits & 0x1F;
			if (num2 != 0)
			{
				num++;
			}
			BigInteger bigInteger = new BigInteger(Sign.Positive, (uint)(num + 1));
			byte[] src = new byte[num << 2];
			rng.GetBytes(src);
			Buffer.BlockCopy(src, 0, bigInteger.data, 0, num << 2);
			if (num2 != 0)
			{
				uint num3 = (uint)(1 << num2 - 1);
				bigInteger.data[num - 1] |= num3;
				num3 = uint.MaxValue >> 32 - num2;
				bigInteger.data[num - 1] &= num3;
			}
			else
			{
				bigInteger.data[num - 1] |= 2147483648u;
			}
			bigInteger.Normalize();
			return bigInteger;
		}

		public static BigInteger GenerateRandom(int bits)
		{
			return GenerateRandom(bits, Rng);
		}

		public void Randomize(RandomNumberGenerator rng)
		{
			if (!(this == 0u))
			{
				int num = BitCount();
				int num2 = num >> 5;
				int num3 = num & 0x1F;
				if (num3 != 0)
				{
					num2++;
				}
				byte[] src = new byte[num2 << 2];
				rng.GetBytes(src);
				Buffer.BlockCopy(src, 0, data, 0, num2 << 2);
				if (num3 != 0)
				{
					uint num4 = (uint)(1 << num3 - 1);
					data[num2 - 1] |= num4;
					num4 = uint.MaxValue >> 32 - num3;
					data[num2 - 1] &= num4;
				}
				else
				{
					data[num2 - 1] |= 2147483648u;
				}
				Normalize();
			}
		}

		public void Randomize()
		{
			Randomize(Rng);
		}

		public int BitCount()
		{
			Normalize();
			uint num = data[length - 1];
			uint num2 = 2147483648u;
			uint num3 = 32u;
			while (num3 != 0 && (num & num2) == 0)
			{
				num3--;
				num2 >>= 1;
			}
			return (int)(num3 + (length - 1 << 5));
		}

		[CLSCompliant(false)]
		public bool TestBit(uint bitNum)
		{
			uint num = bitNum >> 5;
			byte b = (byte)(bitNum & 0x1F);
			uint num2 = (uint)(1 << (int)b);
			return (data[num] & num2) != 0;
		}

		public bool TestBit(int bitNum)
		{
			if (bitNum < 0)
			{
				throw new IndexOutOfRangeException("bitNum out of range");
			}
			uint num = (uint)bitNum >> 5;
			byte b = (byte)(bitNum & 0x1F);
			uint num2 = (uint)(1 << (int)b);
			return (data[num] | num2) == data[num];
		}

		[CLSCompliant(false)]
		public void SetBit(uint bitNum)
		{
			SetBit(bitNum, value: true);
		}

		[CLSCompliant(false)]
		public void ClearBit(uint bitNum)
		{
			SetBit(bitNum, value: false);
		}

		[CLSCompliant(false)]
		public void SetBit(uint bitNum, bool value)
		{
			uint num = bitNum >> 5;
			if (num < length)
			{
				uint num2 = (uint)(1 << (int)(bitNum & 0x1F));
				if (value)
				{
					data[num] |= num2;
				}
				else
				{
					data[num] &= ~num2;
				}
			}
		}

		public int LowestSetBit()
		{
			if (this == 0u)
			{
				return -1;
			}
			int i;
			for (i = 0; !TestBit(i); i++)
			{
			}
			return i;
		}

		public byte[] GetBytes()
		{
			if (this == 0u)
			{
				return new byte[1];
			}
			int num = BitCount();
			int num2 = num >> 3;
			if ((num & 7) != 0)
			{
				num2++;
			}
			byte[] array = new byte[num2];
			int num3 = num2 & 3;
			if (num3 == 0)
			{
				num3 = 4;
			}
			int num4 = 0;
			for (int num5 = (int)(length - 1); num5 >= 0; num5--)
			{
				uint num6 = data[num5];
				for (int num7 = num3 - 1; num7 >= 0; num7--)
				{
					array[num4 + num7] = (byte)(num6 & 0xFF);
					num6 >>= 8;
				}
				num4 += num3;
				num3 = 4;
			}
			return array;
		}

		[CLSCompliant(false)]
		public static bool operator ==(BigInteger bi1, uint ui)
		{
			if (bi1.length != 1)
			{
				bi1.Normalize();
			}
			if (bi1.length == 1)
			{
				return bi1.data[0] == ui;
			}
			return false;
		}

		[CLSCompliant(false)]
		public static bool operator !=(BigInteger bi1, uint ui)
		{
			if (bi1.length != 1)
			{
				bi1.Normalize();
			}
			if (bi1.length == 1)
			{
				return bi1.data[0] != ui;
			}
			return true;
		}

		public static bool operator ==(BigInteger bi1, BigInteger bi2)
		{
			if ((object)bi1 == bi2)
			{
				return true;
			}
			if (null == bi1 || null == bi2)
			{
				return false;
			}
			return Kernel.Compare(bi1, bi2) == Sign.Zero;
		}

		public static bool operator !=(BigInteger bi1, BigInteger bi2)
		{
			if ((object)bi1 == bi2)
			{
				return false;
			}
			if (null == bi1 || null == bi2)
			{
				return true;
			}
			return Kernel.Compare(bi1, bi2) != Sign.Zero;
		}

		public static bool operator >(BigInteger bi1, BigInteger bi2)
		{
			return Kernel.Compare(bi1, bi2) > Sign.Zero;
		}

		public static bool operator <(BigInteger bi1, BigInteger bi2)
		{
			return Kernel.Compare(bi1, bi2) < Sign.Zero;
		}

		public static bool operator >=(BigInteger bi1, BigInteger bi2)
		{
			return Kernel.Compare(bi1, bi2) >= Sign.Zero;
		}

		public static bool operator <=(BigInteger bi1, BigInteger bi2)
		{
			return Kernel.Compare(bi1, bi2) <= Sign.Zero;
		}

		public Sign Compare(BigInteger bi)
		{
			return Kernel.Compare(this, bi);
		}

		[CLSCompliant(false)]
		public string ToString(uint radix)
		{
			return ToString(radix, "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ");
		}

		[CLSCompliant(false)]
		public string ToString(uint radix, string characterSet)
		{
			if (characterSet.Length < radix)
			{
				throw new ArgumentException("charSet length less than radix", "characterSet");
			}
			if (radix == 1)
			{
				throw new ArgumentException("There is no such thing as radix one notation", "radix");
			}
			if (this == 0u)
			{
				return "0";
			}
			if (this == 1u)
			{
				return "1";
			}
			string text = "";
			BigInteger bigInteger = new BigInteger(this);
			while (bigInteger != 0u)
			{
				uint index = Kernel.SingleByteDivideInPlace(bigInteger, radix);
				text = characterSet[(int)index] + text;
			}
			return text;
		}

		private void Normalize()
		{
			while (length != 0 && data[length - 1] == 0)
			{
				length--;
			}
			if (length == 0)
			{
				length++;
			}
		}

		public void Clear()
		{
			for (int i = 0; i < length; i++)
			{
				data[i] = 0u;
			}
		}

		public override int GetHashCode()
		{
			uint num = 0u;
			for (uint num2 = 0u; num2 < length; num2++)
			{
				num ^= data[num2];
			}
			return (int)num;
		}

		public override string ToString()
		{
			return ToString(10u);
		}

		public override bool Equals(object o)
		{
			if (o == null)
			{
				return false;
			}
			if (o is int)
			{
				if ((int)o >= 0)
				{
					return this == (uint)o;
				}
				return false;
			}
			BigInteger bigInteger = o as BigInteger;
			if (bigInteger == null)
			{
				return false;
			}
			return Kernel.Compare(this, bigInteger) == Sign.Zero;
		}

		public BigInteger GCD(BigInteger bi)
		{
			return Kernel.gcd(this, bi);
		}

		public BigInteger ModInverse(BigInteger modulus)
		{
			return Kernel.modInverse(this, modulus);
		}

		public BigInteger ModPow(BigInteger exp, BigInteger n)
		{
			return new ModulusRing(n).Pow(this, exp);
		}

		public bool IsProbablePrime()
		{
			if (this <= smallPrimes[smallPrimes.Length - 1])
			{
				for (int i = 0; i < smallPrimes.Length; i++)
				{
					if (this == smallPrimes[i])
					{
						return true;
					}
				}
				return false;
			}
			for (int j = 0; j < smallPrimes.Length; j++)
			{
				if (this % smallPrimes[j] == 0)
				{
					return false;
				}
			}
			return PrimalityTests.Test(this, ConfidenceFactor.Medium);
		}

		public static BigInteger NextHighestPrime(BigInteger bi)
		{
			return new NextPrimeFinder().GenerateNewPrime(0, bi);
		}

		public static BigInteger GeneratePseudoPrime(int bits)
		{
			return new SequentialSearchPrimeGeneratorBase().GenerateNewPrime(bits);
		}

		public void Incr2()
		{
			int num = 0;
			data[0] += 2u;
			if (data[0] < 2)
			{
				data[++num]++;
				while (data[num++] == 0)
				{
					data[num]++;
				}
				if (length == (uint)num)
				{
					length++;
				}
			}
		}
	}
}
namespace Mono.Math.Prime
{
	public enum ConfidenceFactor
	{
		ExtraLow,
		Low,
		Medium,
		High,
		ExtraHigh,
		Provable
	}
	public delegate bool PrimalityTest(BigInteger bi, ConfidenceFactor confidence);
	public sealed class PrimalityTests
	{
		private PrimalityTests()
		{
		}

		private static int GetSPPRounds(BigInteger bi, ConfidenceFactor confidence)
		{
			int num = bi.BitCount();
			int num2 = ((num <= 100) ? 27 : ((num <= 150) ? 18 : ((num <= 200) ? 15 : ((num <= 250) ? 12 : ((num <= 300) ? 9 : ((num <= 350) ? 8 : ((num <= 400) ? 7 : ((num <= 500) ? 6 : ((num <= 600) ? 5 : ((num <= 800) ? 4 : ((num > 1250) ? 2 : 3)))))))))));
			switch (confidence)
			{
			case ConfidenceFactor.ExtraLow:
				num2 >>= 2;
				if (num2 == 0)
				{
					return 1;
				}
				return num2;
			case ConfidenceFactor.Low:
				num2 >>= 1;
				if (num2 == 0)
				{
					return 1;
				}
				return num2;
			case ConfidenceFactor.Medium:
				return num2;
			case ConfidenceFactor.High:
				return num2 << 1;
			case ConfidenceFactor.ExtraHigh:
				return num2 << 2;
			case ConfidenceFactor.Provable:
				throw new Exception("The Rabin-Miller test can not be executed in a way such that its results are provable");
			default:
				throw new ArgumentOutOfRangeException("confidence");
			}
		}

		public static bool Test(BigInteger n, ConfidenceFactor confidence)
		{
			if (n.BitCount() < 33)
			{
				return SmallPrimeSppTest(n, confidence);
			}
			return RabinMillerTest(n, confidence);
		}

		public static bool RabinMillerTest(BigInteger n, ConfidenceFactor confidence)
		{
			int num = n.BitCount();
			int sPPRounds = GetSPPRounds(num, confidence);
			BigInteger bigInteger = n - 1;
			int num2 = bigInteger.LowestSetBit();
			BigInteger bigInteger2 = bigInteger >> num2;
			BigInteger.ModulusRing modulusRing = new BigInteger.ModulusRing(n);
			BigInteger bigInteger3 = null;
			if (n.BitCount() > 100)
			{
				bigInteger3 = modulusRing.Pow(2u, bigInteger2);
			}
			for (int i = 0; i < sPPRounds; i++)
			{
				if (i > 0 || bigInteger3 == null)
				{
					BigInteger bigInteger4 = null;
					do
					{
						bigInteger4 = BigInteger.GenerateRandom(num);
					}
					while (bigInteger4 <= 2 && bigInteger4 >= bigInteger);
					bigInteger3 = modulusRing.Pow(bigInteger4, bigInteger2);
				}
				if (bigInteger3 == 1u)
				{
					continue;
				}
				for (int j = 0; j < num2; j++)
				{
					if (!(bigInteger3 != bigInteger))
					{
						break;
					}
					bigInteger3 = modulusRing.Pow(bigInteger3, 2);
					if (bigInteger3 == 1u)
					{
						return false;
					}
				}
				if (bigInteger3 != bigInteger)
				{
					return false;
				}
			}
			return true;
		}

		public static bool SmallPrimeSppTest(BigInteger bi, ConfidenceFactor confidence)
		{
			int sPPRounds = GetSPPRounds(bi, confidence);
			BigInteger bigInteger = bi - 1;
			int num = bigInteger.LowestSetBit();
			BigInteger exp = bigInteger >> num;
			BigInteger.ModulusRing modulusRing = new BigInteger.ModulusRing(bi);
			for (int i = 0; i < sPPRounds; i++)
			{
				BigInteger bigInteger2 = modulusRing.Pow(BigInteger.smallPrimes[i], exp);
				if (bigInteger2 == 1u)
				{
					continue;
				}
				bool flag = false;
				for (int j = 0; j < num; j++)
				{
					if (bigInteger2 == bigInteger)
					{
						flag = true;
						break;
					}
					bigInteger2 = bigInteger2 * bigInteger2 % bi;
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}
	}
}
namespace Mono.Math.Prime.Generator
{
	public class NextPrimeFinder : SequentialSearchPrimeGeneratorBase
	{
		protected override BigInteger GenerateSearchBase(int bits, object Context)
		{
			if (Context == null)
			{
				throw new ArgumentNullException("Context");
			}
			BigInteger bigInteger = new BigInteger((BigInteger)Context);
			bigInteger.SetBit(0u);
			return bigInteger;
		}
	}
	public abstract class PrimeGeneratorBase
	{
		public virtual ConfidenceFactor Confidence => ConfidenceFactor.Medium;

		public virtual PrimalityTest PrimalityTest => PrimalityTests.RabinMillerTest;

		public virtual int TrialDivisionBounds => 4000;

		protected bool PostTrialDivisionTests(BigInteger bi)
		{
			return PrimalityTest(bi, Confidence);
		}

		public abstract BigInteger GenerateNewPrime(int bits);
	}
	public class SequentialSearchPrimeGeneratorBase : PrimeGeneratorBase
	{
		protected virtual BigInteger GenerateSearchBase(int bits, object context)
		{
			BigInteger bigInteger = BigInteger.GenerateRandom(bits);
			bigInteger.SetBit(0u);
			return bigInteger;
		}

		public override BigInteger GenerateNewPrime(int bits)
		{
			return GenerateNewPrime(bits, null);
		}

		public virtual BigInteger GenerateNewPrime(int bits, object context)
		{
			BigInteger bigInteger = GenerateSearchBase(bits, context);
			uint num = bigInteger % 3234846615u;
			int trialDivisionBounds = TrialDivisionBounds;
			uint[] smallPrimes = BigInteger.smallPrimes;
			while (true)
			{
				if (num % 3 != 0 && num % 5 != 0 && num % 7 != 0 && num % 11 != 0 && num % 13 != 0 && num % 17 != 0 && num % 19 != 0 && num % 23 != 0 && num % 29 != 0)
				{
					int num2 = 10;
					while (true)
					{
						if (num2 < smallPrimes.Length && smallPrimes[num2] <= trialDivisionBounds)
						{
							if (bigInteger % smallPrimes[num2] == 0)
							{
								break;
							}
							num2++;
							continue;
						}
						if (!IsPrimeAcceptable(bigInteger, context) || !PrimalityTest(bigInteger, Confidence))
						{
							break;
						}
						return bigInteger;
					}
				}
				num += 2;
				if (num >= 3234846615u)
				{
					num -= 3234846615u;
				}
				bigInteger.Incr2();
			}
		}

		protected virtual bool IsPrimeAcceptable(BigInteger bi, object context)
		{
			return true;
		}
	}
}
