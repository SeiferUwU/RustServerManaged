using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading;
using Mono.Data.Tds.Protocol;
using Mono.Security.Protocol.Ntlm;

[assembly: AssemblyDelaySign(true)]
[assembly: AssemblyKeyFile("../mono.pub")]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: AssemblyVersion("4.0.0.0")]
internal static class Consts
{
	public const string MonoVersion = "4.6.2.0";

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
namespace Mono.Data.Tds
{
	internal static class TdsCollation
	{
		public static int LCID(byte[] collation)
		{
			if (collation == null)
			{
				return -1;
			}
			return collation[0] | (collation[1] << 8) | ((collation[2] & 0xF) << 16);
		}

		public static int CollationFlags(byte[] collation)
		{
			if (collation == null)
			{
				return -1;
			}
			return (collation[2] & 0xF0) | ((collation[3] & 0xF) << 4);
		}

		public static int Version(byte[] collation)
		{
			if (collation == null)
			{
				return -1;
			}
			return collation[3] & 0xF0;
		}

		public static int SortId(byte[] collation)
		{
			if (collation == null)
			{
				return -1;
			}
			return collation[4];
		}
	}
	internal static class TdsCharset
	{
		private static Hashtable lcidCodes;

		private static Hashtable sortCodes;

		static TdsCharset()
		{
			lcidCodes = new Hashtable();
			sortCodes = new Hashtable();
			lcidCodes[1078] = 1252;
			lcidCodes[1052] = 1250;
			lcidCodes[1025] = 1256;
			lcidCodes[2049] = 1256;
			lcidCodes[3073] = 1256;
			lcidCodes[4097] = 1256;
			lcidCodes[5121] = 1256;
			lcidCodes[6145] = 1256;
			lcidCodes[7169] = 1256;
			lcidCodes[8193] = 1256;
			lcidCodes[9217] = 1256;
			lcidCodes[10241] = 1256;
			lcidCodes[11265] = 1256;
			lcidCodes[12289] = 1256;
			lcidCodes[13313] = 1256;
			lcidCodes[14337] = 1256;
			lcidCodes[15361] = 1256;
			lcidCodes[16385] = 1256;
			lcidCodes[1069] = 1252;
			lcidCodes[1059] = 1251;
			lcidCodes[1026] = 1251;
			lcidCodes[1027] = 1252;
			lcidCodes[197636] = 950;
			lcidCodes[1028] = 950;
			lcidCodes[2052] = 936;
			lcidCodes[133124] = 936;
			lcidCodes[4100] = 936;
			lcidCodes[1050] = 1250;
			lcidCodes[1029] = 1250;
			lcidCodes[1030] = 1252;
			lcidCodes[1043] = 1252;
			lcidCodes[2067] = 1252;
			lcidCodes[1033] = 1252;
			lcidCodes[2057] = 1252;
			lcidCodes[4105] = 1252;
			lcidCodes[5129] = 1252;
			lcidCodes[3081] = 1252;
			lcidCodes[6153] = 1252;
			lcidCodes[7177] = 1252;
			lcidCodes[9225] = 1252;
			lcidCodes[8201] = 1252;
			lcidCodes[1061] = 1257;
			lcidCodes[1080] = 1252;
			lcidCodes[1065] = 1256;
			lcidCodes[1035] = 1252;
			lcidCodes[1036] = 1252;
			lcidCodes[2060] = 1252;
			lcidCodes[4108] = 1252;
			lcidCodes[3084] = 1252;
			lcidCodes[5132] = 1252;
			lcidCodes[66615] = 1252;
			lcidCodes[66567] = 1252;
			lcidCodes[1031] = 1252;
			lcidCodes[2055] = 1252;
			lcidCodes[3079] = 1252;
			lcidCodes[4103] = 1252;
			lcidCodes[5127] = 1252;
			lcidCodes[1032] = 1253;
			lcidCodes[1037] = 1255;
			lcidCodes[1081] = 65001;
			lcidCodes[1038] = 1250;
			lcidCodes[4174] = 1250;
			lcidCodes[1039] = 1252;
			lcidCodes[1057] = 1252;
			lcidCodes[1040] = 1252;
			lcidCodes[2064] = 1252;
			lcidCodes[1041] = 932;
			lcidCodes[66577] = 932;
			lcidCodes[1042] = 949;
			lcidCodes[1042] = 949;
			lcidCodes[1062] = 1257;
			lcidCodes[1063] = 1257;
			lcidCodes[2087] = 1257;
			lcidCodes[1052] = 1251;
			lcidCodes[1044] = 1252;
			lcidCodes[2068] = 1252;
			lcidCodes[1045] = 1250;
			lcidCodes[2070] = 1252;
			lcidCodes[1046] = 1252;
			lcidCodes[1048] = 1250;
			lcidCodes[1049] = 1251;
			lcidCodes[2074] = 1251;
			lcidCodes[3098] = 1251;
			lcidCodes[1051] = 1250;
			lcidCodes[1060] = 1250;
			lcidCodes[2058] = 1252;
			lcidCodes[1034] = 1252;
			lcidCodes[3082] = 1252;
			lcidCodes[4106] = 1252;
			lcidCodes[5130] = 1252;
			lcidCodes[6154] = 1252;
			lcidCodes[7178] = 1252;
			lcidCodes[8202] = 1252;
			lcidCodes[9226] = 1252;
			lcidCodes[10250] = 1252;
			lcidCodes[11274] = 1252;
			lcidCodes[12298] = 1252;
			lcidCodes[13322] = 1252;
			lcidCodes[14346] = 1252;
			lcidCodes[15370] = 1252;
			lcidCodes[16394] = 1252;
			lcidCodes[1053] = 1252;
			lcidCodes[1054] = 874;
			lcidCodes[1055] = 1254;
			lcidCodes[1058] = 1251;
			lcidCodes[1056] = 1256;
			lcidCodes[1066] = 1258;
			sortCodes[30] = 437;
			sortCodes[31] = 437;
			sortCodes[32] = 437;
			sortCodes[33] = 437;
			sortCodes[34] = 437;
			sortCodes[40] = 850;
			sortCodes[41] = 850;
			sortCodes[42] = 850;
			sortCodes[43] = 850;
			sortCodes[44] = 850;
			sortCodes[49] = 850;
			sortCodes[50] = 1252;
			sortCodes[51] = 1252;
			sortCodes[52] = 1252;
			sortCodes[53] = 1252;
			sortCodes[54] = 1252;
			sortCodes[55] = 850;
			sortCodes[56] = 850;
			sortCodes[57] = 850;
			sortCodes[58] = 850;
			sortCodes[59] = 850;
			sortCodes[60] = 850;
			sortCodes[61] = 850;
			sortCodes[71] = 1252;
			sortCodes[72] = 1252;
			sortCodes[73] = 1252;
			sortCodes[74] = 1252;
			sortCodes[75] = 1252;
			sortCodes[80] = 1250;
			sortCodes[81] = 1250;
			sortCodes[82] = 1250;
			sortCodes[83] = 1250;
			sortCodes[84] = 1250;
			sortCodes[85] = 1250;
			sortCodes[86] = 1250;
			sortCodes[87] = 1250;
			sortCodes[88] = 1250;
			sortCodes[89] = 1250;
			sortCodes[90] = 1250;
			sortCodes[91] = 1250;
			sortCodes[92] = 1250;
			sortCodes[93] = 1250;
			sortCodes[94] = 1250;
			sortCodes[95] = 1250;
			sortCodes[96] = 1250;
			sortCodes[97] = 1250;
			sortCodes[98] = 1250;
			sortCodes[104] = 1251;
			sortCodes[105] = 1251;
			sortCodes[106] = 1251;
			sortCodes[107] = 1251;
			sortCodes[108] = 1251;
			sortCodes[112] = 1253;
			sortCodes[113] = 1253;
			sortCodes[114] = 1253;
			sortCodes[120] = 1253;
			sortCodes[121] = 1253;
			sortCodes[124] = 1253;
			sortCodes[128] = 1254;
			sortCodes[129] = 1254;
			sortCodes[130] = 1254;
			sortCodes[136] = 1255;
			sortCodes[137] = 1255;
			sortCodes[138] = 1255;
			sortCodes[144] = 1256;
			sortCodes[145] = 1256;
			sortCodes[146] = 1256;
			sortCodes[152] = 1257;
			sortCodes[153] = 1257;
			sortCodes[154] = 1257;
			sortCodes[155] = 1257;
			sortCodes[156] = 1257;
			sortCodes[157] = 1257;
			sortCodes[158] = 1257;
			sortCodes[159] = 1257;
			sortCodes[160] = 1257;
			sortCodes[183] = 1252;
			sortCodes[184] = 1252;
			sortCodes[185] = 1252;
			sortCodes[186] = 1252;
			sortCodes[192] = 932;
			sortCodes[193] = 932;
			sortCodes[194] = 949;
			sortCodes[195] = 949;
			sortCodes[196] = 950;
			sortCodes[197] = 950;
			sortCodes[198] = 936;
			sortCodes[199] = 936;
			sortCodes[200] = 932;
			sortCodes[201] = 949;
			sortCodes[202] = 950;
			sortCodes[203] = 936;
			sortCodes[204] = 874;
			sortCodes[205] = 874;
			sortCodes[206] = 874;
		}

		public static Encoding GetEncoding(byte[] collation)
		{
			if (TdsCollation.SortId(collation) != 0)
			{
				return GetEncodingFromSortOrder(collation);
			}
			return GetEncodingFromLCID(collation);
		}

		public static Encoding GetEncodingFromLCID(byte[] collation)
		{
			int lcid = TdsCollation.LCID(collation);
			return GetEncodingFromLCID(lcid);
		}

		public static Encoding GetEncodingFromLCID(int lcid)
		{
			if (lcidCodes[lcid] != null)
			{
				return Encoding.GetEncoding((int)lcidCodes[lcid]);
			}
			return null;
		}

		public static Encoding GetEncodingFromSortOrder(byte[] collation)
		{
			int sortId = TdsCollation.SortId(collation);
			return GetEncodingFromSortOrder(sortId);
		}

		public static Encoding GetEncodingFromSortOrder(int sortId)
		{
			if (sortCodes[sortId] != null)
			{
				return Encoding.GetEncoding((int)sortCodes[sortId]);
			}
			return null;
		}
	}
}
namespace Mono.Data.Tds.Protocol
{
	public enum TdsRpcProcId
	{
		Cursor = 1,
		CursorOpen,
		CursorPrepare,
		CursorExecute,
		CursorPrepExec,
		CursorUnprepare,
		CursorFetch,
		CursorOption,
		CursorClose,
		ExecuteSql,
		Prepare,
		Execute,
		PrepExec,
		PrepExecRpc,
		Unprepare
	}
}
namespace Mono.Data.Tds
{
	public delegate object FrameworkValueGetter(object rawValue, ref bool updated);
	public class TdsMetaParameter
	{
		public const int maxVarCharCharacters = int.MaxValue;

		public const int maxNVarCharCharacters = 1073741823;

		private TdsParameterDirection direction;

		private byte precision;

		private byte scale;

		private int size;

		private string typeName;

		private string name;

		private bool isSizeSet;

		private bool isNullable;

		private object value;

		private bool isVariableSizeType;

		private FrameworkValueGetter frameworkValueGetter;

		private object rawValue;

		private bool isUpdated;

		[CompilerGenerated]
		private static Dictionary<string, int> <>f__switch$map0;

		public TdsParameterDirection Direction
		{
			get
			{
				return direction;
			}
			set
			{
				direction = value;
			}
		}

		public string TypeName
		{
			get
			{
				return typeName;
			}
			set
			{
				typeName = value;
			}
		}

		public string ParameterName
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		public bool IsNullable
		{
			get
			{
				return isNullable;
			}
			set
			{
				isNullable = value;
			}
		}

		public object Value
		{
			get
			{
				if (frameworkValueGetter != null)
				{
					object obj = frameworkValueGetter(rawValue, ref isUpdated);
					if (isUpdated)
					{
						value = obj;
					}
				}
				if (isUpdated)
				{
					value = ResizeValue(value);
					isUpdated = false;
				}
				return value;
			}
			set
			{
				rawValue = (this.value = value);
				isUpdated = true;
			}
		}

		public object RawValue
		{
			get
			{
				return rawValue;
			}
			set
			{
				Value = value;
			}
		}

		public byte Precision
		{
			get
			{
				return precision;
			}
			set
			{
				precision = value;
			}
		}

		public byte Scale
		{
			get
			{
				if ((TypeName == "decimal" || TypeName == "numeric") && scale == 0 && !Convert.IsDBNull(Value))
				{
					int[] bits = decimal.GetBits(Convert.ToDecimal(Value));
					scale = (byte)((bits[3] >> 16) & 0xFF);
				}
				return scale;
			}
			set
			{
				scale = value;
			}
		}

		public int Size
		{
			get
			{
				return GetSize();
			}
			set
			{
				size = value;
				isUpdated = true;
				isSizeSet = true;
			}
		}

		public bool IsVariableSizeType
		{
			get
			{
				return isVariableSizeType;
			}
			set
			{
				isVariableSizeType = value;
			}
		}

		public bool IsVarNVarCharMax => TypeName == "ntext" && size >= 1073741823;

		public bool IsVarCharMax => TypeName == "text" && size >= int.MaxValue;

		public bool IsAnyVarCharMax => IsVarNVarCharMax || IsVarCharMax;

		public bool IsNonUnicodeText
		{
			get
			{
				TdsColumnType metaType = GetMetaType();
				return metaType == TdsColumnType.VarChar || metaType == TdsColumnType.BigVarChar || metaType == TdsColumnType.Text || metaType == TdsColumnType.Char || metaType == TdsColumnType.BigChar;
			}
		}

		public bool IsMoneyType
		{
			get
			{
				TdsColumnType metaType = GetMetaType();
				return metaType == TdsColumnType.Money || metaType == TdsColumnType.MoneyN || metaType == TdsColumnType.Money4 || metaType == TdsColumnType.SmallMoney;
			}
		}

		public bool IsDateTimeType
		{
			get
			{
				TdsColumnType metaType = GetMetaType();
				return metaType == TdsColumnType.DateTime || metaType == TdsColumnType.DateTime4 || metaType == TdsColumnType.DateTimeN;
			}
		}

		public bool IsTextType
		{
			get
			{
				TdsColumnType metaType = GetMetaType();
				return metaType == TdsColumnType.VarChar || metaType == TdsColumnType.BigVarChar || metaType == TdsColumnType.BigChar || metaType == TdsColumnType.Char || metaType == TdsColumnType.BigNVarChar || metaType == TdsColumnType.NChar || metaType == TdsColumnType.Text || metaType == TdsColumnType.NText;
			}
		}

		public bool IsDecimalType
		{
			get
			{
				TdsColumnType metaType = GetMetaType();
				return metaType == TdsColumnType.Decimal || metaType == TdsColumnType.Numeric;
			}
		}

		public TdsMetaParameter(string name, object value)
			: this(name, string.Empty, value)
		{
		}

		public TdsMetaParameter(string name, FrameworkValueGetter valueGetter)
			: this(name, string.Empty, null)
		{
			frameworkValueGetter = valueGetter;
		}

		public TdsMetaParameter(string name, string typeName, object value)
		{
			ParameterName = name;
			Value = value;
			TypeName = typeName;
			IsNullable = false;
		}

		public TdsMetaParameter(string name, int size, bool isNullable, byte precision, byte scale, object value)
		{
			ParameterName = name;
			Size = size;
			IsNullable = isNullable;
			Precision = precision;
			Scale = scale;
			Value = value;
		}

		public TdsMetaParameter(string name, int size, bool isNullable, byte precision, byte scale, FrameworkValueGetter valueGetter)
		{
			ParameterName = name;
			Size = size;
			IsNullable = isNullable;
			Precision = precision;
			Scale = scale;
			frameworkValueGetter = valueGetter;
		}

		private object ResizeValue(object newValue)
		{
			if (newValue == DBNull.Value || newValue == null)
			{
				return newValue;
			}
			if (!isSizeSet || size <= 0)
			{
				return newValue;
			}
			if (newValue is string text)
			{
				if ((TypeName == "nvarchar" || TypeName == "nchar" || TypeName == "xml") && text.Length > size)
				{
					return text.Substring(0, size);
				}
			}
			else if (newValue.GetType() == typeof(byte[]))
			{
				byte[] array = (byte[])newValue;
				if (array.Length > size)
				{
					byte[] array2 = new byte[size];
					Array.Copy(array, array2, size);
					return array2;
				}
			}
			return newValue;
		}

		internal string Prepare()
		{
			string text = TypeName;
			TdsColumnType tdsColumnType = TdsColumnType.Char;
			switch (text)
			{
			case "varbinary":
			{
				int actualSize = Size;
				if (actualSize <= 0)
				{
					actualSize = GetActualSize();
				}
				if (actualSize > 8000)
				{
					text = "varbinary(max)";
				}
				break;
			}
			case "datetime2":
				tdsColumnType = TdsColumnType.DateTime2;
				text = "char";
				break;
			case "datetimeoffset":
				tdsColumnType = TdsColumnType.DateTimeOffset;
				text = "char";
				break;
			}
			string arg = "@";
			if (ParameterName[0] == '@')
			{
				arg = string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder($"{arg}{ParameterName} {text}");
			if (text != null)
			{
				if (<>f__switch$map0 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(8);
					dictionary.Add("decimal", 0);
					dictionary.Add("numeric", 0);
					dictionary.Add("varchar", 1);
					dictionary.Add("varbinary", 1);
					dictionary.Add("nvarchar", 2);
					dictionary.Add("char", 3);
					dictionary.Add("nchar", 4);
					dictionary.Add("binary", 4);
					<>f__switch$map0 = dictionary;
				}
				if (<>f__switch$map0.TryGetValue(text, out var num))
				{
					switch (num)
					{
					case 0:
						stringBuilder.Append($"({(byte)((Precision != 0) ? Precision : 38)},{Scale})");
						break;
					case 1:
					{
						int actualSize = Size;
						if (actualSize <= 0)
						{
							actualSize = GetActualSize();
							if (actualSize <= 0)
							{
								actualSize = 1;
							}
						}
						stringBuilder.Append((actualSize <= 8000) ? $"({actualSize})" : "(max)");
						break;
					}
					case 2:
					{
						int num2 = ((Size >= 0) ? Size : (GetActualSize() / 2));
						stringBuilder.Append((num2 <= 0) ? "(4000)" : ((num2 <= 4000) ? $"({num2})" : "(max)"));
						break;
					}
					case 3:
					{
						int actualSize = -1;
						if (tdsColumnType != TdsColumnType.Char)
						{
							actualSize = GetDateTimeStringLength(tdsColumnType);
						}
						else if (isSizeSet)
						{
							actualSize = Size;
						}
						if (actualSize > 0)
						{
							stringBuilder.Append($"({actualSize})");
						}
						break;
					}
					case 4:
						if (isSizeSet && Size > 0)
						{
							stringBuilder.Append($"({Size})");
						}
						break;
					}
				}
			}
			return stringBuilder.ToString();
		}

		internal int GetActualSize()
		{
			if (Value == DBNull.Value || Value == null)
			{
				return 0;
			}
			switch (Value.GetType().ToString())
			{
			case "System.String":
			{
				int num = ((string)value).Length;
				if (TypeName == "nvarchar" || TypeName == "nchar" || TypeName == "ntext" || TypeName == "xml")
				{
					num *= 2;
				}
				return num;
			}
			case "System.Byte[]":
				return ((byte[])value).Length;
			default:
				return GetSize();
			}
		}

		private int GetSize()
		{
			switch (TypeName)
			{
			case "decimal":
				return 17;
			case "uniqueidentifier":
				return 16;
			case "bigint":
			case "datetime":
			case "float":
			case "money":
				return 8;
			case "datetime2":
				return GetDateTimeStringLength(TdsColumnType.DateTime2);
			case "datetimeoffset":
				return GetDateTimeStringLength(TdsColumnType.DateTimeOffset);
			case "int":
			case "real":
			case "smalldatetime":
			case "smallmoney":
				return 4;
			case "smallint":
				return 2;
			case "tinyint":
			case "bit":
				return 1;
			case "nchar":
			case "ntext":
				return size * 2;
			default:
				return size;
			}
		}

		private int GetDateTimePrecision()
		{
			int num = Precision;
			if (num == 0 || num > 7)
			{
				num = 7;
			}
			return num;
		}

		private int GetDateTimeStringLength(TdsColumnType type)
		{
			int dateTimePrecision = GetDateTimePrecision();
			int num = ((dateTimePrecision != 0) ? (20 + dateTimePrecision) : 19);
			if (type == TdsColumnType.DateTimeOffset)
			{
				num += 6;
			}
			return num;
		}

		private string GetDateTimeString(TdsColumnType type)
		{
			int dateTimePrecision = GetDateTimePrecision();
			string text = "yyyy-MM-dd'T'HH':'mm':'ss";
			if (dateTimePrecision > 0)
			{
				text += ".fffffff".Substring(0, dateTimePrecision + 1);
			}
			return type switch
			{
				TdsColumnType.DateTime2 => ((DateTime)Value).ToString(text), 
				TdsColumnType.DateTimeOffset => ((DateTimeOffset)Value).ToString(text + "zzz"), 
				_ => throw new ApplicationException("Should be unreachable"), 
			};
		}

		internal byte[] GetBytes()
		{
			byte[] result = Array.Empty<byte>();
			if (Value == DBNull.Value || Value == null)
			{
				return result;
			}
			switch (TypeName)
			{
			case "nvarchar":
			case "nchar":
			case "ntext":
			case "xml":
				return Encoding.Unicode.GetBytes((string)Value);
			case "varchar":
			case "char":
			case "text":
				return Encoding.Default.GetBytes((string)Value);
			case "datetime2":
				return Encoding.Default.GetBytes(GetDateTimeString(TdsColumnType.DateTime2));
			case "datetimeoffset":
				return Encoding.Default.GetBytes(GetDateTimeString(TdsColumnType.DateTimeOffset));
			default:
				return (byte[])Value;
			}
		}

		internal TdsColumnType GetMetaType()
		{
			switch (TypeName)
			{
			case "binary":
				return TdsColumnType.BigBinary;
			case "bit":
				if (IsNullable)
				{
					return TdsColumnType.BitN;
				}
				return TdsColumnType.Bit;
			case "bigint":
				if (IsNullable)
				{
					return TdsColumnType.IntN;
				}
				return TdsColumnType.BigInt;
			case "char":
				return TdsColumnType.BigChar;
			case "money":
				if (IsNullable)
				{
					return TdsColumnType.MoneyN;
				}
				return TdsColumnType.Money;
			case "smallmoney":
				if (IsNullable)
				{
					return TdsColumnType.MoneyN;
				}
				return TdsColumnType.SmallMoney;
			case "decimal":
				return TdsColumnType.Decimal;
			case "datetime":
				if (IsNullable)
				{
					return TdsColumnType.DateTimeN;
				}
				return TdsColumnType.DateTime;
			case "smalldatetime":
				if (IsNullable)
				{
					return TdsColumnType.DateTimeN;
				}
				return TdsColumnType.DateTime4;
			case "datetime2":
				return TdsColumnType.DateTime2;
			case "datetimeoffset":
				return TdsColumnType.DateTimeOffset;
			case "float":
				if (IsNullable)
				{
					return TdsColumnType.FloatN;
				}
				return TdsColumnType.Float8;
			case "image":
				return TdsColumnType.Image;
			case "int":
				if (IsNullable)
				{
					return TdsColumnType.IntN;
				}
				return TdsColumnType.Int4;
			case "numeric":
				return TdsColumnType.Numeric;
			case "nchar":
				return TdsColumnType.NChar;
			case "ntext":
				return TdsColumnType.NText;
			case "xml":
			case "nvarchar":
				return TdsColumnType.BigNVarChar;
			case "real":
				if (IsNullable)
				{
					return TdsColumnType.FloatN;
				}
				return TdsColumnType.Real;
			case "smallint":
				if (IsNullable)
				{
					return TdsColumnType.IntN;
				}
				return TdsColumnType.Int2;
			case "text":
				return TdsColumnType.Text;
			case "tinyint":
				if (IsNullable)
				{
					return TdsColumnType.IntN;
				}
				return TdsColumnType.Int1;
			case "uniqueidentifier":
				return TdsColumnType.UniqueIdentifier;
			case "varbinary":
				return TdsColumnType.BigVarBinary;
			case "varchar":
				return TdsColumnType.BigVarChar;
			case "sql_variant":
				return TdsColumnType.Variant;
			default:
				throw new NotSupportedException("Unknown Type : " + TypeName);
			}
		}

		public void CalculateIsVariableType()
		{
			switch (GetMetaType())
			{
			case TdsColumnType.Image:
			case TdsColumnType.Text:
			case TdsColumnType.UniqueIdentifier:
			case TdsColumnType.IntN:
			case TdsColumnType.Char:
			case TdsColumnType.NText:
			case TdsColumnType.BitN:
			case TdsColumnType.Decimal:
			case TdsColumnType.FloatN:
			case TdsColumnType.MoneyN:
			case TdsColumnType.DateTimeN:
			case TdsColumnType.BigVarBinary:
			case TdsColumnType.BigVarChar:
			case TdsColumnType.BigBinary:
			case TdsColumnType.BigChar:
			case TdsColumnType.BigNVarChar:
			case TdsColumnType.NChar:
				IsVariableSizeType = true;
				break;
			default:
				IsVariableSizeType = false;
				break;
			}
		}

		public void Validate(int index)
		{
			if ((direction == TdsParameterDirection.InputOutput || direction == TdsParameterDirection.Output) && isVariableSizeType && (Value == DBNull.Value || Value == null) && Size == 0)
			{
				throw new InvalidOperationException($"{typeName}[{index}]: the Size property should not be of size 0");
			}
		}
	}
	public class TdsMetaParameterCollection : ICollection, IEnumerable
	{
		private ArrayList list;

		public int Count => list.Count;

		public bool IsSynchronized => list.IsSynchronized;

		public TdsMetaParameter this[int index] => (TdsMetaParameter)list[index];

		public TdsMetaParameter this[string name] => this[IndexOf(name)];

		public object SyncRoot => list.SyncRoot;

		public TdsMetaParameterCollection()
		{
			list = new ArrayList();
		}

		public int Add(TdsMetaParameter value)
		{
			return list.Add(value);
		}

		public void Clear()
		{
			list.Clear();
		}

		public bool Contains(TdsMetaParameter value)
		{
			return list.Contains(value);
		}

		public void CopyTo(Array array, int index)
		{
			list.CopyTo(array, index);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return list.GetEnumerator();
		}

		public int IndexOf(TdsMetaParameter value)
		{
			return list.IndexOf(value);
		}

		public int IndexOf(string name)
		{
			for (int i = 0; i < Count; i++)
			{
				if (this[i].ParameterName.Equals(name))
				{
					return i;
				}
			}
			return -1;
		}

		public void Insert(int index, TdsMetaParameter value)
		{
			list.Insert(index, value);
		}

		public void Remove(TdsMetaParameter value)
		{
			list.Remove(value);
		}

		public void Remove(string name)
		{
			RemoveAt(IndexOf(name));
		}

		public void RemoveAt(int index)
		{
			list.RemoveAt(index);
		}
	}
	[Serializable]
	public enum TdsParameterDirection
	{
		Input,
		Output,
		InputOutput,
		ReturnValue
	}
}
namespace Mono.Data.Tds.Protocol
{
	public abstract class Tds
	{
		private TdsComm comm;

		private TdsVersion tdsVersion;

		protected internal TdsConnectionParameters connectionParms;

		protected readonly byte[] NTLMSSP_ID = new byte[8] { 78, 84, 76, 77, 83, 83, 80, 0 };

		private int packetSize;

		private string dataSource;

		private string database;

		private string originalDatabase = string.Empty;

		private string databaseProductName;

		private string databaseProductVersion;

		private int databaseMajorVersion;

		private CultureInfo locale = CultureInfo.InvariantCulture;

		private readonly int lifeTime;

		private readonly DateTime created = DateTime.Now;

		private string charset;

		private string language;

		private bool connected;

		private bool moreResults;

		private Encoding encoder;

		private bool doneProc;

		private bool pooling = true;

		private TdsDataRow currentRow;

		private TdsDataColumnCollection columns;

		private ArrayList tableNames;

		private ArrayList columnNames;

		private TdsMetaParameterCollection parameters = new TdsMetaParameterCollection();

		private bool queryInProgress;

		private int cancelsRequested;

		private int cancelsProcessed;

		private ArrayList outputParameters = new ArrayList();

		protected TdsInternalErrorCollection messages = new TdsInternalErrorCollection();

		private int recordsAffected = -1;

		private long StreamLength;

		private long StreamIndex;

		private int StreamColumnIndex;

		private bool sequentialAccess;

		private bool isRowRead;

		private bool isResultRead;

		private bool LoadInProgress;

		private byte[] collation;

		internal int poolStatus;

		protected string Charset => charset;

		protected CultureInfo Locale => locale;

		public bool DoneProc => doneProc;

		protected string Language => language;

		protected ArrayList ColumnNames => columnNames;

		public TdsDataRow ColumnValues => currentRow;

		internal TdsComm Comm => comm;

		public string Database => database;

		public string DataSource => dataSource;

		public virtual bool IsConnected
		{
			get
			{
				return connected && comm != null && comm.IsConnected();
			}
			set
			{
				connected = value;
			}
		}

		public bool Pooling
		{
			get
			{
				return pooling;
			}
			set
			{
				pooling = value;
			}
		}

		public bool MoreResults
		{
			get
			{
				return moreResults;
			}
			set
			{
				moreResults = value;
			}
		}

		public int PacketSize => packetSize;

		public int RecordsAffected
		{
			get
			{
				return recordsAffected;
			}
			set
			{
				recordsAffected = value;
			}
		}

		public string ServerVersion => databaseProductVersion;

		public TdsDataColumnCollection Columns => columns;

		public TdsVersion TdsVersion => tdsVersion;

		public ArrayList OutputParameters
		{
			get
			{
				return outputParameters;
			}
			set
			{
				outputParameters = value;
			}
		}

		protected TdsMetaParameterCollection Parameters
		{
			get
			{
				return parameters;
			}
			set
			{
				parameters = value;
			}
		}

		public bool SequentialAccess
		{
			get
			{
				return sequentialAccess;
			}
			set
			{
				sequentialAccess = value;
			}
		}

		public byte[] Collation => collation;

		public TdsVersion ServerTdsVersion => databaseMajorVersion switch
		{
			4 => TdsVersion.tds42, 
			5 => TdsVersion.tds50, 
			7 => TdsVersion.tds70, 
			8 => TdsVersion.tds80, 
			9 => TdsVersion.tds90, 
			10 => TdsVersion.tds100, 
			_ => tdsVersion, 
		};

		internal bool Expired
		{
			get
			{
				if (lifeTime == 0)
				{
					return false;
				}
				return DateTime.Now > created + TimeSpan.FromSeconds(lifeTime);
			}
		}

		public event TdsInternalErrorMessageEventHandler TdsErrorMessage;

		public event TdsInternalInfoMessageEventHandler TdsInfoMessage;

		public Tds(string dataSource, int port, int packetSize, int timeout, TdsVersion tdsVersion)
			: this(dataSource, port, packetSize, timeout, 0, tdsVersion)
		{
		}

		public Tds(string dataSource, int port, int packetSize, int timeout, int lifeTime, TdsVersion tdsVersion)
		{
			this.tdsVersion = tdsVersion;
			this.packetSize = packetSize;
			this.dataSource = dataSource;
			columns = new TdsDataColumnCollection();
			this.lifeTime = lifeTime;
			InitComm(port, timeout);
		}

		private void SkipRow()
		{
			SkipToColumnIndex(Columns.Count);
			StreamLength = 0L;
			StreamColumnIndex = 0;
			StreamIndex = 0L;
			LoadInProgress = false;
		}

		private void SkipToColumnIndex(int colIndex)
		{
			if (LoadInProgress)
			{
				EndLoad();
			}
			if (colIndex < StreamColumnIndex)
			{
				throw new Exception("Cannot Skip to a colindex less than the curr index");
			}
			while (colIndex != StreamColumnIndex)
			{
				TdsColumnType? columnType = Columns[StreamColumnIndex].ColumnType;
				if (!columnType.HasValue)
				{
					throw new Exception("Column type unset.");
				}
				if (columnType != TdsColumnType.Image && columnType != TdsColumnType.Text && columnType != TdsColumnType.NText)
				{
					GetColumnValue(columnType, outParam: false, StreamColumnIndex);
					StreamColumnIndex++;
					continue;
				}
				BeginLoad(columnType);
				Comm.Skip(StreamLength);
				StreamLength = 0L;
				EndLoad();
			}
		}

		public object GetSequentialColumnValue(int colIndex)
		{
			if (colIndex < StreamColumnIndex)
			{
				throw new InvalidOperationException("Invalid attempt tp read from column ordinal" + colIndex);
			}
			if (LoadInProgress)
			{
				EndLoad();
			}
			if (colIndex != StreamColumnIndex)
			{
				SkipToColumnIndex(colIndex);
			}
			object columnValue = GetColumnValue(Columns[colIndex].ColumnType, outParam: false, colIndex);
			StreamColumnIndex++;
			return columnValue;
		}

		public long GetSequentialColumnValue(int colIndex, long fieldIndex, byte[] buffer, int bufferIndex, int size)
		{
			if (colIndex < StreamColumnIndex)
			{
				throw new InvalidOperationException("Invalid attempt to read from column ordinal" + colIndex);
			}
			try
			{
				if (colIndex != StreamColumnIndex)
				{
					SkipToColumnIndex(colIndex);
				}
				if (!LoadInProgress)
				{
					BeginLoad(Columns[colIndex].ColumnType);
				}
				if (buffer == null)
				{
					return StreamLength;
				}
				return LoadData(fieldIndex, buffer, bufferIndex, size);
			}
			catch (IOException innerException)
			{
				connected = false;
				throw new TdsInternalException("Server closed the connection.", innerException);
			}
		}

		private void BeginLoad(TdsColumnType? colType)
		{
			if (LoadInProgress)
			{
				EndLoad();
			}
			StreamLength = 0L;
			if (!colType.HasValue)
			{
				throw new ArgumentNullException("colType");
			}
			if (!colType.HasValue)
			{
				goto IL_014a;
			}
			switch (colType.Value)
			{
			case TdsColumnType.Image:
			case TdsColumnType.Text:
			case TdsColumnType.NText:
				break;
			case TdsColumnType.BigVarBinary:
			case TdsColumnType.BigVarChar:
			case TdsColumnType.BigBinary:
			case TdsColumnType.BigChar:
				goto IL_0110;
			case TdsColumnType.VarBinary:
			case TdsColumnType.VarChar:
			case TdsColumnType.Binary:
			case TdsColumnType.Char:
			case TdsColumnType.NVarChar:
			case TdsColumnType.NChar:
				goto IL_0133;
			default:
				goto IL_014a;
			}
			if (Comm.GetByte() != 0)
			{
				Comm.Skip(24L);
				StreamLength = Comm.GetTdsInt();
			}
			else
			{
				StreamLength = -2L;
			}
			goto IL_0157;
			IL_014a:
			StreamLength = -1L;
			goto IL_0157;
			IL_0110:
			Comm.GetTdsShort();
			StreamLength = Comm.GetTdsShort();
			goto IL_0157;
			IL_0157:
			StreamIndex = 0L;
			LoadInProgress = true;
			return;
			IL_0133:
			StreamLength = Comm.GetTdsShort();
			goto IL_0157;
		}

		private void EndLoad()
		{
			if (StreamLength > 0)
			{
				Comm.Skip(StreamLength);
			}
			StreamLength = 0L;
			StreamIndex = 0L;
			StreamColumnIndex++;
			LoadInProgress = false;
		}

		private long LoadData(long fieldIndex, byte[] buffer, int bufferIndex, int size)
		{
			if (StreamLength <= 0)
			{
				return StreamLength;
			}
			if (fieldIndex < StreamIndex)
			{
				throw new InvalidOperationException($"Attempting to read at dataIndex '{fieldIndex}' is not allowed as this is less than the current position. You must read from dataIndex '{StreamIndex}' or greater.");
			}
			if (fieldIndex >= StreamLength + StreamIndex)
			{
				return 0L;
			}
			int num = (int)(fieldIndex - StreamIndex);
			Comm.Skip(num);
			StreamIndex += fieldIndex - StreamIndex;
			StreamLength -= num;
			int num2 = (int)((size <= StreamLength) ? size : StreamLength);
			byte[] bytes = Comm.GetBytes(num2, exclusiveBuffer: true);
			StreamIndex += num2 + (fieldIndex - StreamIndex);
			StreamLength -= num2;
			bytes.CopyTo(buffer, bufferIndex);
			return bytes.Length;
		}

		protected virtual void InitComm(int port, int timeout)
		{
			comm = new TdsComm(dataSource, port, packetSize, timeout, tdsVersion);
		}

		protected internal void InitExec()
		{
			moreResults = true;
			doneProc = false;
			isResultRead = false;
			isRowRead = false;
			StreamLength = 0L;
			StreamIndex = 0L;
			StreamColumnIndex = 0;
			LoadInProgress = false;
			queryInProgress = false;
			cancelsRequested = 0;
			cancelsProcessed = 0;
			recordsAffected = -1;
			messages.Clear();
			outputParameters.Clear();
		}

		public void Cancel()
		{
			if (queryInProgress && cancelsRequested == cancelsProcessed)
			{
				comm.StartPacket(TdsPacketType.Cancel);
				try
				{
					Comm.SendPacket();
				}
				catch (IOException innerException)
				{
					connected = false;
					throw new TdsInternalException("Server closed the connection.", innerException);
				}
				cancelsRequested++;
			}
		}

		public abstract bool Connect(TdsConnectionParameters connectionParameters);

		public static TdsTimeoutException CreateTimeoutException(string dataSource, string method)
		{
			string message = "Timeout expired. The timeout period elapsed prior to completion of the operation or the server is not responding.";
			return new TdsTimeoutException(0, 0, message, -2, method, dataSource, "Mono TdsClient Data Provider", 0);
		}

		public virtual void Disconnect()
		{
			try
			{
				comm.StartPacket(TdsPacketType.Logoff);
				comm.Append((byte)0);
				comm.SendPacket();
			}
			catch
			{
			}
			connected = false;
			comm.Close();
		}

		public virtual bool Reset()
		{
			database = originalDatabase;
			return true;
		}

		protected virtual bool IsValidRowCount(byte status, byte op)
		{
			return (status & 0x10) != 0;
		}

		public void Execute(string sql)
		{
			Execute(sql, null, 0, wantResults: false);
		}

		public void ExecProc(string sql)
		{
			ExecProc(sql, null, 0, wantResults: false);
		}

		public virtual void Execute(string sql, TdsMetaParameterCollection parameters, int timeout, bool wantResults)
		{
			ExecuteQuery(sql, timeout, wantResults);
		}

		public virtual void ExecProc(string sql, TdsMetaParameterCollection parameters, int timeout, bool wantResults)
		{
			ExecuteQuery($"exec {sql}", timeout, wantResults);
		}

		public virtual void ExecPrepared(string sql, TdsMetaParameterCollection parameters, int timeout, bool wantResults)
		{
			throw new NotSupportedException();
		}

		internal void ExecBulkCopyMetaData(int timeout, bool wantResults)
		{
			moreResults = true;
			try
			{
				Comm.SendPacket();
				CheckForData(timeout);
				if (!wantResults)
				{
					SkipToEnd();
				}
			}
			catch (IOException innerException)
			{
				connected = false;
				throw new TdsInternalException("Server closed the connection.", innerException);
			}
		}

		internal void ExecBulkCopy(int timeout, bool wantResults)
		{
			moreResults = true;
			try
			{
				Comm.SendPacket();
				CheckForData(timeout);
				if (!wantResults)
				{
					SkipToEnd();
				}
			}
			catch (IOException innerException)
			{
				connected = false;
				throw new TdsInternalException("Server closed the connection.", innerException);
			}
		}

		protected void ExecuteQuery(string sql, int timeout, bool wantResults)
		{
			InitExec();
			Comm.StartPacket(TdsPacketType.Query);
			Comm.Append(sql);
			try
			{
				Comm.SendPacket();
				CheckForData(timeout);
				if (!wantResults)
				{
					SkipToEnd();
				}
			}
			catch (IOException innerException)
			{
				connected = false;
				throw new TdsInternalException("Server closed the connection.", innerException);
			}
		}

		protected virtual void ExecRPC(string rpcName, TdsMetaParameterCollection parameters, int timeout, bool wantResults)
		{
			Comm.StartPacket(TdsPacketType.DBRPC);
			byte[] bytes = Comm.Encoder.GetBytes(rpcName);
			byte b = (byte)bytes.Length;
			ushort s = 0;
			ushort s2 = (ushort)(1 + b + 2);
			Comm.Append(s2);
			Comm.Append(b);
			Comm.Append(bytes);
			Comm.Append(s);
			try
			{
				Comm.SendPacket();
				CheckForData(timeout);
				if (!wantResults)
				{
					SkipToEnd();
				}
			}
			catch (IOException innerException)
			{
				connected = false;
				throw new TdsInternalException("Server closed the connection.", innerException);
			}
		}

		public bool NextResult()
		{
			if (SequentialAccess && isResultRead)
			{
				while (NextRow())
				{
				}
				isRowRead = false;
				isResultRead = false;
			}
			if (!moreResults)
			{
				return false;
			}
			bool flag = false;
			bool flag2 = false;
			while (!flag)
			{
				TdsPacketSubType tdsPacketSubType = ProcessSubPacket();
				if (flag2)
				{
					moreResults = false;
					break;
				}
				switch (tdsPacketSubType)
				{
				case TdsPacketSubType.ColumnMetadata:
				case TdsPacketSubType.ColumnInfo:
				case TdsPacketSubType.RowFormat:
				{
					byte b = Comm.Peek();
					flag = b != 164;
					if (flag && doneProc && b == 209)
					{
						flag2 = true;
						flag = false;
					}
					break;
				}
				case TdsPacketSubType.TableName:
				{
					byte b = Comm.Peek();
					flag = b != 165;
					break;
				}
				case TdsPacketSubType.ColumnDetail:
					flag = true;
					break;
				default:
					flag = !moreResults;
					break;
				}
			}
			return moreResults;
		}

		public bool NextRow()
		{
			if (SequentialAccess && isRowRead)
			{
				SkipRow();
				isRowRead = false;
			}
			bool flag = false;
			bool result = false;
			do
			{
				switch (ProcessSubPacket())
				{
				case TdsPacketSubType.Row:
					result = true;
					flag = true;
					break;
				case TdsPacketSubType.Done:
				case TdsPacketSubType.DoneProc:
				case TdsPacketSubType.DoneInProc:
					result = false;
					flag = true;
					break;
				}
			}
			while (!flag);
			return result;
		}

		public virtual string Prepare(string sql, TdsMetaParameterCollection parameters)
		{
			throw new NotSupportedException();
		}

		public void SkipToEnd()
		{
			try
			{
				while (NextResult())
				{
				}
			}
			catch (IOException innerException)
			{
				connected = false;
				throw new TdsInternalException("Server closed the connection.", innerException);
			}
		}

		public virtual void Unprepare(string statementId)
		{
			throw new NotSupportedException();
		}

		[System.MonoTODO("Is cancel enough, or do we need to drop the connection?")]
		protected void CheckForData(int timeout)
		{
			if (timeout > 0 && !comm.Poll(timeout, SelectMode.SelectRead))
			{
				Cancel();
				throw CreateTimeoutException(dataSource, "CheckForData()");
			}
		}

		protected TdsInternalInfoMessageEventArgs CreateTdsInfoMessageEvent(TdsInternalErrorCollection errors)
		{
			return new TdsInternalInfoMessageEventArgs(errors);
		}

		protected TdsInternalErrorMessageEventArgs CreateTdsErrorMessageEvent(byte theClass, int lineNumber, string message, int number, string procedure, string server, string source, byte state)
		{
			return new TdsInternalErrorMessageEventArgs(new TdsInternalError(theClass, lineNumber, message, number, procedure, server, source, state));
		}

		private Encoding GetEncodingFromColumnCollation(int lcid, int sortId)
		{
			if (sortId != 0)
			{
				return TdsCharset.GetEncodingFromSortOrder(sortId);
			}
			return TdsCharset.GetEncodingFromLCID(lcid);
		}

		protected object GetColumnValue(TdsColumnType? colType, bool outParam)
		{
			return GetColumnValue(colType, outParam, -1);
		}

		private object GetColumnValue(TdsColumnType? colType, bool outParam, int ordinal)
		{
			object obj = null;
			Encoding encoding = null;
			int lcid = 0;
			int sortId = 0;
			if (!colType.HasValue)
			{
				throw new ArgumentNullException("colType");
			}
			if (ordinal > -1 && tdsVersion > TdsVersion.tds70)
			{
				lcid = columns[ordinal].LCID.Value;
				sortId = columns[ordinal].SortOrder.Value;
			}
			if (colType.HasValue)
			{
				switch (colType.Value)
				{
				case TdsColumnType.IntN:
					if (outParam)
					{
						comm.Skip(1L);
					}
					obj = GetIntValue(colType);
					goto IL_0655;
				case TdsColumnType.Int1:
				case TdsColumnType.Int2:
				case TdsColumnType.Int4:
				case TdsColumnType.BigInt:
					obj = GetIntValue(colType);
					goto IL_0655;
				case TdsColumnType.Image:
					if (outParam)
					{
						comm.Skip(1L);
					}
					obj = GetImageValue();
					goto IL_0655;
				case TdsColumnType.Text:
					encoding = GetEncodingFromColumnCollation(lcid, sortId);
					if (outParam)
					{
						comm.Skip(1L);
					}
					obj = GetTextValue(wideChars: false, encoding);
					goto IL_0655;
				case TdsColumnType.NText:
					encoding = GetEncodingFromColumnCollation(lcid, sortId);
					if (outParam)
					{
						comm.Skip(1L);
					}
					obj = GetTextValue(wideChars: true, encoding);
					goto IL_0655;
				case TdsColumnType.VarChar:
				case TdsColumnType.Char:
					encoding = GetEncodingFromColumnCollation(lcid, sortId);
					if (outParam)
					{
						comm.Skip(1L);
					}
					obj = GetStringValue(colType, wideChars: false, outParam, encoding);
					goto IL_0655;
				case TdsColumnType.BigVarBinary:
				{
					if (outParam)
					{
						comm.Skip(1L);
					}
					int num = comm.GetTdsShort();
					obj = comm.GetBytes(num, exclusiveBuffer: true);
					goto IL_0655;
				}
				case TdsColumnType.BigBinary:
					if (outParam)
					{
						comm.Skip(2L);
					}
					obj = GetBinaryValue();
					goto IL_0655;
				case TdsColumnType.BigVarChar:
				case TdsColumnType.BigChar:
					encoding = GetEncodingFromColumnCollation(lcid, sortId);
					if (outParam)
					{
						comm.Skip(2L);
					}
					obj = GetStringValue(colType, wideChars: false, outParam, encoding);
					goto IL_0655;
				case TdsColumnType.BigNVarChar:
				case TdsColumnType.NChar:
					encoding = GetEncodingFromColumnCollation(lcid, sortId);
					if (outParam)
					{
						comm.Skip(2L);
					}
					obj = GetStringValue(colType, wideChars: true, outParam, encoding);
					goto IL_0655;
				case TdsColumnType.NVarChar:
					encoding = GetEncodingFromColumnCollation(lcid, sortId);
					if (outParam)
					{
						comm.Skip(1L);
					}
					obj = GetStringValue(colType, wideChars: true, outParam, encoding);
					goto IL_0655;
				case TdsColumnType.Real:
				case TdsColumnType.Float8:
					obj = GetFloatValue(colType);
					goto IL_0655;
				case TdsColumnType.FloatN:
					if (outParam)
					{
						comm.Skip(1L);
					}
					obj = GetFloatValue(colType);
					goto IL_0655;
				case TdsColumnType.Money:
				case TdsColumnType.SmallMoney:
					obj = GetMoneyValue(colType);
					goto IL_0655;
				case TdsColumnType.MoneyN:
					if (outParam)
					{
						comm.Skip(1L);
					}
					obj = GetMoneyValue(colType);
					goto IL_0655;
				case TdsColumnType.Decimal:
				case TdsColumnType.Numeric:
				{
					byte b;
					byte b2;
					if (outParam)
					{
						comm.Skip(1L);
						b = comm.GetByte();
						b2 = comm.GetByte();
					}
					else
					{
						b = (byte)columns[ordinal].NumericPrecision.Value;
						b2 = (byte)columns[ordinal].NumericScale.Value;
					}
					obj = GetDecimalValue(b, b2);
					if (b2 == 0 && b <= 19 && tdsVersion == TdsVersion.tds70 && !(obj is DBNull))
					{
						obj = Convert.ToInt64(obj);
					}
					goto IL_0655;
				}
				case TdsColumnType.DateTimeN:
					if (outParam)
					{
						comm.Skip(1L);
					}
					obj = GetDateTimeValue(colType);
					goto IL_0655;
				case TdsColumnType.DateTime4:
				case TdsColumnType.DateTime:
					obj = GetDateTimeValue(colType);
					goto IL_0655;
				case TdsColumnType.VarBinary:
				case TdsColumnType.Binary:
					if (outParam)
					{
						comm.Skip(1L);
					}
					obj = GetBinaryValue();
					goto IL_0655;
				case TdsColumnType.BitN:
					if (outParam)
					{
						comm.Skip(1L);
					}
					obj = ((comm.GetByte() != 0) ? ((object)(comm.GetByte() != 0)) : DBNull.Value);
					goto IL_0655;
				case TdsColumnType.Bit:
				{
					int num2 = comm.GetByte();
					obj = num2 != 0;
					goto IL_0655;
				}
				case TdsColumnType.UniqueIdentifier:
					if (comm.Peek() != 16)
					{
						comm.GetByte();
						obj = DBNull.Value;
					}
					else
					{
						if (outParam)
						{
							comm.Skip(1L);
						}
						int num = comm.GetByte() & 0xFF;
						if (num > 0)
						{
							byte[] bytes = comm.GetBytes(num, exclusiveBuffer: true);
							if (!BitConverter.IsLittleEndian)
							{
								byte[] array = new byte[num];
								for (int i = 0; i < 4; i++)
								{
									array[i] = bytes[4 - i - 1];
								}
								for (int j = 4; j < 6; j++)
								{
									array[j] = bytes[6 - (j - 4) - 1];
								}
								for (int k = 6; k < 8; k++)
								{
									array[k] = bytes[8 - (k - 6) - 1];
								}
								for (int l = 8; l < 16; l++)
								{
									array[l] = bytes[l];
								}
								Array.Copy(array, 0, bytes, 0, num);
							}
							obj = new Guid(bytes);
						}
					}
					goto IL_0655;
				case TdsColumnType.Variant:
					{
						if (outParam)
						{
							comm.Skip(4L);
						}
						obj = GetVariantValue();
						goto IL_0655;
					}
					IL_0655:
					return obj;
				}
			}
			return DBNull.Value;
		}

		private object GetBinaryValue()
		{
			object result = DBNull.Value;
			if (tdsVersion >= TdsVersion.tds70)
			{
				int tdsShort = comm.GetTdsShort();
				if (tdsShort != 65535 && tdsShort >= 0)
				{
					result = comm.GetBytes(tdsShort, exclusiveBuffer: true);
				}
			}
			else
			{
				int tdsShort = comm.GetByte() & 0xFF;
				if (tdsShort != 0)
				{
					result = comm.GetBytes(tdsShort, exclusiveBuffer: true);
				}
			}
			return result;
		}

		private object GetVariantValue()
		{
			uint tdsInt = (uint)comm.GetTdsInt();
			if (tdsInt == 0)
			{
				return DBNull.Value;
			}
			TdsColumnType tdsColumnType = (TdsColumnType)comm.GetByte();
			byte b = comm.GetByte();
			if (b != 0)
			{
				comm.Skip((int)b);
			}
			tdsInt -= (uint)(b + 2);
			if (tdsColumnType == TdsColumnType.Int1 || tdsColumnType == TdsColumnType.Int2 || tdsColumnType == TdsColumnType.Int4 || tdsColumnType == TdsColumnType.BigInt)
			{
				return GetIntValue(tdsColumnType);
			}
			comm.Skip(tdsInt);
			return DBNull.Value;
		}

		private object GetDateTimeValue(TdsColumnType? type)
		{
			int num = 0;
			if (!type.HasValue)
			{
				throw new ArgumentNullException("type");
			}
			if (type.HasValue)
			{
				switch (type.Value)
				{
				case TdsColumnType.DateTime4:
					num = 4;
					break;
				case TdsColumnType.DateTime:
					num = 8;
					break;
				case TdsColumnType.DateTimeN:
				{
					byte b = comm.Peek();
					if (b == 0 || b == 4 || b == 8)
					{
						num = comm.GetByte();
					}
					break;
				}
				}
			}
			DateTime dateTime = new DateTime(1900, 1, 1);
			object obj;
			switch (num)
			{
			case 8:
			{
				obj = dateTime.AddDays(comm.GetTdsInt());
				int tdsInt = comm.GetTdsInt();
				long num2 = (long)System.Math.Round((float)((long)tdsInt % 300L * 1000) / 300f);
				if (tdsInt != 0 || num2 != 0)
				{
					obj = ((DateTime)obj).AddSeconds(tdsInt / 300);
					obj = ((DateTime)obj).AddMilliseconds(num2);
				}
				break;
			}
			case 4:
			{
				obj = dateTime.AddDays((int)(ushort)comm.GetTdsShort());
				short tdsShort = comm.GetTdsShort();
				if (tdsShort != 0)
				{
					obj = ((DateTime)obj).AddMinutes(tdsShort);
				}
				break;
			}
			default:
				obj = DBNull.Value;
				break;
			}
			return obj;
		}

		private object GetDecimalValue(byte precision, byte scale)
		{
			if (tdsVersion < TdsVersion.tds70)
			{
				return GetDecimalValueTds50(precision, scale);
			}
			return GetDecimalValueTds70(precision, scale);
		}

		private object GetDecimalValueTds70(byte precision, byte scale)
		{
			int[] array = new int[4];
			int num = (comm.GetByte() & 0xFF) - 1;
			if (num < 0)
			{
				return DBNull.Value;
			}
			bool flag = comm.GetByte() == 1;
			if (num > 16)
			{
				throw new OverflowException();
			}
			int num2 = 0;
			int num3 = 0;
			while (num2 < num && num2 < 16)
			{
				array[num3] = comm.GetTdsInt();
				num2 += 4;
				num3++;
			}
			if (array[3] != 0)
			{
				return new TdsBigDecimal(precision, scale, !flag, array);
			}
			return new decimal(array[0], array[1], array[2], !flag, scale);
		}

		private object GetDecimalValueTds50(byte precision, byte scale)
		{
			int[] array = new int[4];
			int num = comm.GetByte() & 0xFF;
			if (num == 0)
			{
				return DBNull.Value;
			}
			byte[] bytes = comm.GetBytes(num, exclusiveBuffer: false);
			byte[] array2 = new byte[4];
			bool isNegative = bytes[0] == 1;
			if (num > 17)
			{
				throw new OverflowException();
			}
			int num2 = 1;
			int num3 = 0;
			while (num2 < num && num2 < 16)
			{
				for (int i = 0; i < 4; i++)
				{
					if (num2 + i < num)
					{
						array2[i] = bytes[num - (num2 + i)];
					}
					else
					{
						array2[i] = 0;
					}
				}
				if (!BitConverter.IsLittleEndian)
				{
					array2 = comm.Swap(array2);
				}
				array[num3] = BitConverter.ToInt32(array2, 0);
				num2 += 4;
				num3++;
			}
			if (array[3] != 0)
			{
				return new TdsBigDecimal(precision, scale, isNegative, array);
			}
			return new decimal(array[0], array[1], array[2], isNegative, scale);
		}

		private object GetFloatValue(TdsColumnType? columnType)
		{
			if (!columnType.HasValue)
			{
				throw new ArgumentNullException("columnType");
			}
			int num = 0;
			if (columnType.HasValue)
			{
				switch (columnType.Value)
				{
				case TdsColumnType.Real:
					num = 4;
					break;
				case TdsColumnType.Float8:
					num = 8;
					break;
				case TdsColumnType.FloatN:
					num = comm.GetByte();
					break;
				}
			}
			return num switch
			{
				8 => BitConverter.Int64BitsToDouble(comm.GetTdsInt64()), 
				4 => BitConverter.ToSingle(BitConverter.GetBytes(comm.GetTdsInt()), 0), 
				_ => DBNull.Value, 
			};
		}

		private object GetImageValue()
		{
			if (comm.GetByte() == 0)
			{
				return DBNull.Value;
			}
			comm.Skip(24L);
			int tdsInt = comm.GetTdsInt();
			if (tdsInt < 0)
			{
				return DBNull.Value;
			}
			return comm.GetBytes(tdsInt, exclusiveBuffer: true);
		}

		private object GetIntValue(TdsColumnType? type)
		{
			if (!type.HasValue)
			{
				throw new ArgumentNullException("type");
			}
			if (type.HasValue)
			{
				int num;
				switch (type.Value)
				{
				case TdsColumnType.BigInt:
					num = 8;
					goto IL_008e;
				case TdsColumnType.IntN:
					num = comm.GetByte();
					goto IL_008e;
				case TdsColumnType.Int4:
					num = 4;
					goto IL_008e;
				case TdsColumnType.Int2:
					num = 2;
					goto IL_008e;
				case TdsColumnType.Int1:
					{
						num = 1;
						goto IL_008e;
					}
					IL_008e:
					return num switch
					{
						8 => comm.GetTdsInt64(), 
						4 => comm.GetTdsInt(), 
						2 => comm.GetTdsShort(), 
						1 => comm.GetByte(), 
						_ => DBNull.Value, 
					};
				}
			}
			return DBNull.Value;
		}

		private object GetMoneyValue(TdsColumnType? type)
		{
			if (!type.HasValue)
			{
				throw new ArgumentNullException("type");
			}
			if (type.HasValue)
			{
				int num;
				switch (type.Value)
				{
				case TdsColumnType.Money4:
				case TdsColumnType.SmallMoney:
					num = 4;
					goto IL_007d;
				case TdsColumnType.Money:
					num = 8;
					goto IL_007d;
				case TdsColumnType.MoneyN:
					{
						num = comm.GetByte();
						goto IL_007d;
					}
					IL_007d:
					switch (num)
					{
					case 4:
					{
						int num4 = Comm.GetTdsInt();
						bool flag2 = num4 < 0;
						if (flag2)
						{
							num4 = ~(num4 - 1);
						}
						return new decimal(num4, 0, 0, flag2, 4);
					}
					case 8:
					{
						int num2 = Comm.GetTdsInt();
						int num3 = Comm.GetTdsInt();
						bool flag = num2 < 0;
						if (flag)
						{
							num2 = ~num2;
							num3 = ~(num3 - 1);
						}
						return new decimal(num3, num2, 0, flag, 4);
					}
					default:
						return DBNull.Value;
					}
				}
			}
			return DBNull.Value;
		}

		protected object GetStringValue(TdsColumnType? colType, bool wideChars, bool outputParam, Encoding encoder)
		{
			bool flag = false;
			Encoding enc = encoder;
			if (tdsVersion > TdsVersion.tds70 && outputParam && (colType == TdsColumnType.BigChar || colType == TdsColumnType.BigNVarChar || colType == TdsColumnType.BigVarChar || colType == TdsColumnType.NChar || colType == TdsColumnType.NVarChar))
			{
				byte[] bytes = Comm.GetBytes(5, exclusiveBuffer: true);
				enc = TdsCharset.GetEncoding(bytes);
				flag = true;
			}
			else
			{
				flag = tdsVersion >= TdsVersion.tds70 && (wideChars || !outputParam);
			}
			int len = ((!flag) ? (comm.GetByte() & 0xFF) : comm.GetTdsShort());
			return GetStringValue(wideChars, len, enc);
		}

		protected object GetStringValue(bool wideChars, int len, Encoding enc)
		{
			if (tdsVersion < TdsVersion.tds70 && len == 0)
			{
				return DBNull.Value;
			}
			if (len >= 0)
			{
				object obj = ((!wideChars) ? comm.GetString(len, wide: false, enc) : comm.GetString(len / 2, enc));
				if (tdsVersion < TdsVersion.tds70 && ((string)obj).Equals(" "))
				{
					obj = string.Empty;
				}
				return obj;
			}
			return DBNull.Value;
		}

		protected int GetSubPacketLength()
		{
			return comm.GetTdsShort();
		}

		private object GetTextValue(bool wideChars, Encoding encoder)
		{
			string text = null;
			byte b = comm.GetByte();
			if (b != 16)
			{
				return DBNull.Value;
			}
			comm.Skip(24L);
			int tdsInt = comm.GetTdsInt();
			if (tdsInt == 0)
			{
				return string.Empty;
			}
			text = ((!wideChars) ? comm.GetString(tdsInt, wide: false, encoder) : comm.GetString(tdsInt / 2, encoder));
			tdsInt /= 2;
			if ((byte)tdsVersion < 70 && text == " ")
			{
				text = string.Empty;
			}
			return text;
		}

		internal bool IsBlobType(TdsColumnType columnType)
		{
			return columnType == TdsColumnType.Text || columnType == TdsColumnType.Image || columnType == TdsColumnType.NText || columnType == TdsColumnType.Variant;
		}

		internal bool IsLargeType(TdsColumnType columnType)
		{
			return (byte)columnType > 128;
		}

		protected bool IsWideType(TdsColumnType columnType)
		{
			if (columnType == TdsColumnType.NText || columnType == TdsColumnType.NVarChar || columnType == TdsColumnType.NChar)
			{
				return true;
			}
			return false;
		}

		internal static bool IsFixedSizeColumn(TdsColumnType columnType)
		{
			switch (columnType)
			{
			case TdsColumnType.Int1:
			case TdsColumnType.Bit:
			case TdsColumnType.Int2:
			case TdsColumnType.Int4:
			case TdsColumnType.DateTime4:
			case TdsColumnType.Real:
			case TdsColumnType.Money:
			case TdsColumnType.DateTime:
			case TdsColumnType.Float8:
			case TdsColumnType.Money4:
			case TdsColumnType.SmallMoney:
			case TdsColumnType.BigInt:
				return true;
			default:
				return false;
			}
		}

		protected void LoadRow()
		{
			if (SequentialAccess)
			{
				if (isRowRead)
				{
					SkipRow();
				}
				isRowRead = true;
				isResultRead = true;
				return;
			}
			currentRow = new TdsDataRow();
			int num = 0;
			foreach (TdsDataColumn column in columns)
			{
				object columnValue = GetColumnValue(column.ColumnType, outParam: false, num);
				currentRow.Add(columnValue);
				if (doneProc)
				{
					outputParameters.Add(columnValue);
				}
				if (columnValue is TdsBigDecimal && currentRow.BigDecimalIndex < 0)
				{
					currentRow.BigDecimalIndex = num;
				}
				num++;
			}
		}

		internal static int LookupBufferSize(TdsColumnType columnType)
		{
			switch (columnType)
			{
			case TdsColumnType.Int1:
			case TdsColumnType.Bit:
				return 1;
			case TdsColumnType.Int2:
				return 2;
			case TdsColumnType.Int4:
			case TdsColumnType.DateTime4:
			case TdsColumnType.Real:
			case TdsColumnType.Money4:
			case TdsColumnType.SmallMoney:
				return 4;
			case TdsColumnType.Money:
			case TdsColumnType.DateTime:
			case TdsColumnType.Float8:
			case TdsColumnType.BigInt:
				return 8;
			default:
				return 0;
			}
		}

		protected internal int ProcessAuthentication()
		{
			int tdsShort = Comm.GetTdsShort();
			byte[] bytes = Comm.GetBytes(tdsShort, exclusiveBuffer: true);
			Type2Message type = new Type2Message(bytes);
			Type3Message type3Message = new Type3Message(type);
			type3Message.Domain = connectionParms.DefaultDomain;
			type3Message.Host = connectionParms.Hostname;
			type3Message.Username = connectionParms.User;
			type3Message.Password = GetPlainPassword(connectionParms.Password);
			Comm.StartPacket(TdsPacketType.SspAuth);
			Comm.Append(type3Message.GetBytes());
			try
			{
				Comm.SendPacket();
			}
			catch (IOException innerException)
			{
				connected = false;
				throw new TdsInternalException("Server closed the connection.", innerException);
			}
			return 1;
		}

		protected void ProcessColumnDetail()
		{
			int subPacketLength = GetSubPacketLength();
			byte[] array = new byte[3];
			string text = string.Empty;
			int num = 0;
			while (num < subPacketLength)
			{
				for (int i = 0; i < 3; i++)
				{
					array[i] = comm.GetByte();
				}
				num += 3;
				bool flag = (array[2] & 0x20) != 0;
				if (flag)
				{
					int num2;
					if (tdsVersion >= TdsVersion.tds70)
					{
						num2 = comm.GetByte();
						num += 2 * num2 + 1;
					}
					else
					{
						num2 = comm.GetByte();
						num += num2 + 1;
					}
					text = comm.GetString(num2);
				}
				byte index = (byte)(array[0] - 1);
				byte index2 = (byte)(array[1] - 1);
				bool flag2 = (array[2] & 4) != 0;
				TdsDataColumn tdsDataColumn = columns[index];
				tdsDataColumn.IsHidden = (array[2] & 0x10) != 0;
				tdsDataColumn.IsExpression = flag2;
				tdsDataColumn.IsKey = (array[2] & 8) != 0;
				tdsDataColumn.IsAliased = flag;
				tdsDataColumn.BaseColumnName = ((!flag) ? null : text);
				tdsDataColumn.BaseTableName = (flag2 ? null : ((string)tableNames[index2]));
			}
		}

		protected abstract void ProcessColumnInfo();

		protected void ProcessColumnNames()
		{
			columnNames = new ArrayList();
			int tdsShort = comm.GetTdsShort();
			int num = 0;
			int num2 = 0;
			while (num < tdsShort)
			{
				int num3 = comm.GetByte();
				string value = comm.GetString(num3);
				num = num + 1 + num3;
				columnNames.Add(value);
				num2++;
			}
		}

		[System.MonoTODO("Make sure counting works right, especially with multiple resultsets.")]
		protected void ProcessEndToken(TdsPacketSubType type)
		{
			byte b = Comm.GetByte();
			Comm.Skip(1L);
			byte op = comm.GetByte();
			Comm.Skip(1L);
			int tdsInt = comm.GetTdsInt();
			bool flag = IsValidRowCount(b, op);
			moreResults = (b & 1) != 0;
			bool flag2 = (b & 0x20) != 0;
			if (type != TdsPacketSubType.DoneProc)
			{
				if (type != TdsPacketSubType.Done && type != TdsPacketSubType.DoneInProc)
				{
					goto IL_00c5;
				}
			}
			else
			{
				doneProc = true;
			}
			if (flag)
			{
				if (recordsAffected == -1)
				{
					recordsAffected = tdsInt;
				}
				else
				{
					recordsAffected += tdsInt;
				}
			}
			goto IL_00c5;
			IL_00c5:
			if (moreResults)
			{
				queryInProgress = false;
			}
			if (flag2)
			{
				cancelsProcessed++;
			}
			if (messages.Count > 0 && !moreResults)
			{
				OnTdsInfoMessage(CreateTdsInfoMessageEvent(messages));
			}
		}

		protected void ProcessEnvironmentChange()
		{
			int subPacketLength = GetSubPacketLength();
			switch ((TdsEnvPacketSubType)comm.GetByte())
			{
			case TdsEnvPacketSubType.BlockSize:
			{
				int len = comm.GetByte();
				string s = comm.GetString(len);
				if (tdsVersion >= TdsVersion.tds70)
				{
					comm.Skip(subPacketLength - 2 - len * 2);
				}
				else
				{
					comm.Skip(subPacketLength - 2 - len);
				}
				packetSize = int.Parse(s);
				comm.ResizeOutBuf(packetSize);
				break;
			}
			case TdsEnvPacketSubType.CharSet:
			{
				int len = comm.GetByte();
				if (tdsVersion == TdsVersion.tds70)
				{
					SetCharset(comm.GetString(len));
					comm.Skip(subPacketLength - 2 - len * 2);
				}
				else
				{
					SetCharset(comm.GetString(len));
					comm.Skip(subPacketLength - 2 - len);
				}
				break;
			}
			case TdsEnvPacketSubType.Locale:
			{
				int len = comm.GetByte();
				int culture = 0;
				if (tdsVersion >= TdsVersion.tds70)
				{
					culture = (int)Convert.ChangeType(comm.GetString(len), typeof(int));
					comm.Skip(subPacketLength - 2 - len * 2);
				}
				else
				{
					culture = (int)Convert.ChangeType(comm.GetString(len), typeof(int));
					comm.Skip(subPacketLength - 2 - len);
				}
				locale = new CultureInfo(culture);
				break;
			}
			case TdsEnvPacketSubType.Database:
			{
				int len = comm.GetByte();
				string text = comm.GetString(len);
				len = comm.GetByte() & 0xFF;
				comm.GetString(len);
				if (originalDatabase == string.Empty)
				{
					originalDatabase = text;
				}
				database = text;
				break;
			}
			case TdsEnvPacketSubType.CollationInfo:
			{
				int len = comm.GetByte();
				collation = comm.GetBytes(len, exclusiveBuffer: true);
				int culture = TdsCollation.LCID(collation);
				locale = new CultureInfo(culture);
				SetCharset(TdsCharset.GetEncoding(collation));
				break;
			}
			default:
				comm.Skip(subPacketLength - 1);
				break;
			}
		}

		protected void ProcessLoginAck()
		{
			uint num = 0u;
			GetSubPacketLength();
			if (tdsVersion >= TdsVersion.tds70)
			{
				comm.Skip(1L);
				switch ((uint)comm.GetTdsInt())
				{
				case 7u:
					tdsVersion = TdsVersion.tds70;
					break;
				case 263u:
					tdsVersion = TdsVersion.tds80;
					break;
				case 16777329u:
					tdsVersion = TdsVersion.tds81;
					break;
				case 33556850u:
					tdsVersion = TdsVersion.tds90;
					break;
				}
			}
			if (tdsVersion >= TdsVersion.tds70)
			{
				int len = comm.GetByte();
				databaseProductName = comm.GetString(len);
				databaseMajorVersion = comm.GetByte();
				databaseProductVersion = string.Format("{0}.{1}.{2}", databaseMajorVersion.ToString("00"), comm.GetByte().ToString("00"), (256 * comm.GetByte() + comm.GetByte()).ToString("0000"));
			}
			else
			{
				comm.Skip(5L);
				short len2 = comm.GetByte();
				databaseProductName = comm.GetString(len2);
				comm.Skip(1L);
				databaseMajorVersion = comm.GetByte();
				databaseProductVersion = $"{databaseMajorVersion}.{comm.GetByte()}";
				comm.Skip(1L);
			}
			if (databaseProductName.Length > 1 && databaseProductName.IndexOf('\0') != -1)
			{
				int length = databaseProductName.IndexOf('\0');
				databaseProductName = databaseProductName.Substring(0, length);
			}
			connected = true;
		}

		protected void OnTdsErrorMessage(TdsInternalErrorMessageEventArgs e)
		{
			if (this.TdsErrorMessage != null)
			{
				this.TdsErrorMessage(this, e);
			}
		}

		protected void OnTdsInfoMessage(TdsInternalInfoMessageEventArgs e)
		{
			if (this.TdsInfoMessage != null)
			{
				this.TdsInfoMessage(this, e);
			}
			messages.Clear();
		}

		protected void ProcessMessage(TdsPacketSubType subType)
		{
			GetSubPacketLength();
			int tdsInt = comm.GetTdsInt();
			byte state = comm.GetByte();
			byte b = comm.GetByte();
			bool flag = false;
			if (subType == TdsPacketSubType.EED)
			{
				flag = b > 10;
				comm.Skip((int)comm.GetByte());
				comm.Skip(1L);
				comm.Skip(2L);
			}
			else
			{
				flag = subType == TdsPacketSubType.Error;
			}
			string message = comm.GetString(comm.GetTdsShort());
			string server = comm.GetString(comm.GetByte());
			string procedure = comm.GetString(comm.GetByte());
			byte lineNumber = comm.GetByte();
			comm.Skip(1L);
			string empty = string.Empty;
			if (flag)
			{
				OnTdsErrorMessage(CreateTdsErrorMessageEvent(b, lineNumber, message, tdsInt, procedure, server, empty, state));
			}
			else
			{
				messages.Add(new TdsInternalError(b, lineNumber, message, tdsInt, procedure, server, empty, state));
			}
		}

		protected virtual void ProcessOutputParam()
		{
			GetSubPacketLength();
			comm.GetString(comm.GetByte() & 0xFF);
			comm.Skip(5L);
			TdsColumnType value = (TdsColumnType)comm.GetByte();
			object columnValue = GetColumnValue(value, outParam: true);
			outputParameters.Add(columnValue);
		}

		protected void ProcessDynamic()
		{
			Comm.Skip(2L);
			Comm.GetByte();
			Comm.GetByte();
			Comm.GetString(Comm.GetByte());
		}

		protected virtual TdsPacketSubType ProcessSubPacket()
		{
			TdsPacketSubType tdsPacketSubType = (TdsPacketSubType)comm.GetByte();
			switch (tdsPacketSubType)
			{
			case TdsPacketSubType.Dynamic2:
				comm.Skip(comm.GetTdsInt());
				break;
			case TdsPacketSubType.AltName:
			case TdsPacketSubType.AltFormat:
			case TdsPacketSubType.Capability:
			case TdsPacketSubType.ParamFormat:
				comm.Skip(comm.GetTdsShort());
				break;
			case TdsPacketSubType.Dynamic:
				ProcessDynamic();
				break;
			case TdsPacketSubType.EnvironmentChange:
				ProcessEnvironmentChange();
				break;
			case TdsPacketSubType.Error:
			case TdsPacketSubType.Info:
			case TdsPacketSubType.EED:
				ProcessMessage(tdsPacketSubType);
				break;
			case TdsPacketSubType.Param:
				ProcessOutputParam();
				break;
			case TdsPacketSubType.LoginAck:
				ProcessLoginAck();
				break;
			case TdsPacketSubType.Authentication:
				ProcessAuthentication();
				break;
			case TdsPacketSubType.ReturnStatus:
				ProcessReturnStatus();
				break;
			case TdsPacketSubType.ProcId:
				Comm.Skip(8L);
				break;
			case TdsPacketSubType.Done:
			case TdsPacketSubType.DoneProc:
			case TdsPacketSubType.DoneInProc:
				ProcessEndToken(tdsPacketSubType);
				break;
			case TdsPacketSubType.ColumnName:
				Comm.Skip(8L);
				ProcessColumnNames();
				break;
			case TdsPacketSubType.ColumnMetadata:
			case TdsPacketSubType.ColumnInfo:
			case TdsPacketSubType.RowFormat:
				Columns.Clear();
				ProcessColumnInfo();
				break;
			case TdsPacketSubType.ColumnDetail:
				ProcessColumnDetail();
				break;
			case TdsPacketSubType.TableName:
				ProcessTableName();
				break;
			case TdsPacketSubType.ColumnOrder:
				comm.Skip(comm.GetTdsShort());
				break;
			case TdsPacketSubType.Control:
				comm.Skip(comm.GetTdsShort());
				break;
			case TdsPacketSubType.Row:
				LoadRow();
				break;
			}
			return tdsPacketSubType;
		}

		protected void ProcessTableName()
		{
			tableNames = new ArrayList();
			int tdsShort = comm.GetTdsShort();
			int num = 0;
			while (num < tdsShort)
			{
				int num2;
				if (tdsVersion >= TdsVersion.tds70)
				{
					num2 = comm.GetTdsShort();
					num += 2 * (num2 + 1);
				}
				else
				{
					num2 = comm.GetByte();
					num += num2 + 1;
				}
				tableNames.Add(comm.GetString(num2));
			}
		}

		protected void SetCharset(Encoding encoder)
		{
			comm.Encoder = encoder;
		}

		protected void SetCharset(string charset)
		{
			if (charset == null || charset.Length > 30)
			{
				charset = "iso_1";
			}
			if (this.charset == null || !(this.charset == charset))
			{
				if (charset.StartsWith("cp"))
				{
					encoder = Encoding.GetEncoding(int.Parse(charset.Substring(2)));
					this.charset = charset;
				}
				else
				{
					encoder = Encoding.GetEncoding("iso-8859-1");
					this.charset = "iso_1";
				}
				SetCharset(encoder);
			}
		}

		protected void SetLanguage(string language)
		{
			if (language == null || language.Length > 30)
			{
				language = "us_english";
			}
			this.language = language;
		}

		protected virtual void ProcessReturnStatus()
		{
			comm.Skip(4L);
		}

		public static string GetPlainPassword(SecureString secPass)
		{
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.SecureStringToGlobalAllocUnicode(secPass);
				return Marshal.PtrToStringUni(intPtr);
			}
			finally
			{
				Marshal.ZeroFreeGlobalAllocUnicode(intPtr);
			}
		}

		protected IAsyncResult BeginExecuteQueryInternal(string sql, bool wantResults, AsyncCallback callback, object state)
		{
			InitExec();
			TdsAsyncResult tdsAsyncResult = new TdsAsyncResult(callback, state);
			tdsAsyncResult.TdsAsyncState.WantResults = wantResults;
			Comm.StartPacket(TdsPacketType.Query);
			Comm.Append(sql);
			try
			{
				Comm.SendPacket();
				Comm.BeginReadPacket(OnBeginExecuteQueryCallback, tdsAsyncResult);
				return tdsAsyncResult;
			}
			catch (IOException innerException)
			{
				connected = false;
				throw new TdsInternalException("Server closed the connection.", innerException);
			}
		}

		protected void EndExecuteQueryInternal(IAsyncResult ar)
		{
			if (!ar.IsCompleted)
			{
				ar.AsyncWaitHandle.WaitOne();
			}
			TdsAsyncResult tdsAsyncResult = (TdsAsyncResult)ar;
			if (tdsAsyncResult.IsCompletedWithException)
			{
				throw tdsAsyncResult.Exception;
			}
		}

		protected void OnBeginExecuteQueryCallback(IAsyncResult ar)
		{
			TdsAsyncResult tdsAsyncResult = (TdsAsyncResult)ar.AsyncState;
			TdsAsyncState tdsAsyncState = tdsAsyncResult.TdsAsyncState;
			try
			{
				Comm.EndReadPacket(ar);
				if (!tdsAsyncState.WantResults)
				{
					SkipToEnd();
				}
			}
			catch (Exception e)
			{
				tdsAsyncResult.MarkComplete(e);
				return;
			}
			tdsAsyncResult.MarkComplete();
		}

		public virtual IAsyncResult BeginExecuteNonQuery(string sql, TdsMetaParameterCollection parameters, AsyncCallback callback, object state)
		{
			throw new NotImplementedException("should not be called!");
		}

		public virtual void EndExecuteNonQuery(IAsyncResult ar)
		{
			throw new NotImplementedException("should not be called!");
		}

		public virtual IAsyncResult BeginExecuteQuery(string sql, TdsMetaParameterCollection parameters, AsyncCallback callback, object state)
		{
			throw new NotImplementedException("should not be called!");
		}

		public virtual void EndExecuteQuery(IAsyncResult ar)
		{
			throw new NotImplementedException("should not be called!");
		}

		public virtual IAsyncResult BeginExecuteProcedure(string prolog, string epilog, string cmdText, bool IsNonQuery, TdsMetaParameterCollection parameters, AsyncCallback callback, object state)
		{
			throw new NotImplementedException("should not be called!");
		}

		public virtual void EndExecuteProcedure(IAsyncResult ar)
		{
			throw new NotImplementedException("should not be called!");
		}

		public void WaitFor(IAsyncResult ar)
		{
			if (!ar.IsCompleted)
			{
				ar.AsyncWaitHandle.WaitOne();
			}
		}

		public void CheckAndThrowException(IAsyncResult ar)
		{
			TdsAsyncResult tdsAsyncResult = (TdsAsyncResult)ar;
			if (tdsAsyncResult.IsCompleted && tdsAsyncResult.IsCompletedWithException)
			{
				throw tdsAsyncResult.Exception;
			}
		}
	}
	public sealed class Tds42 : Tds
	{
		public static readonly TdsVersion Version = TdsVersion.tds42;

		public Tds42(string server, int port)
			: this(server, port, 512, 15)
		{
		}

		public Tds42(string server, int port, int packetSize, int timeout)
			: base(server, port, packetSize, timeout, Version)
		{
		}

		public override bool Connect(TdsConnectionParameters connectionParameters)
		{
			if (IsConnected)
			{
				throw new InvalidOperationException("The connection is already open.");
			}
			SetCharset(connectionParameters.Charset);
			SetLanguage(connectionParameters.Language);
			byte pad = 0;
			byte[] b = Array.Empty<byte>();
			base.Comm.StartPacket(TdsPacketType.Logon);
			byte[] array = base.Comm.Append(connectionParameters.Hostname, 30, pad);
			base.Comm.Append((byte)((array.Length >= 30) ? 30u : ((uint)array.Length)));
			array = base.Comm.Append(connectionParameters.User, 30, pad);
			base.Comm.Append((byte)((array.Length >= 30) ? 30u : ((uint)array.Length)));
			array = base.Comm.Append(Tds.GetPlainPassword(connectionParameters.Password), 30, pad);
			base.Comm.Append((byte)((array.Length >= 30) ? 30u : ((uint)array.Length)));
			base.Comm.Append("00000116", 8, pad);
			base.Comm.Append(b, 16, pad);
			base.Comm.Append((byte)0);
			base.Comm.Append((byte)160);
			base.Comm.Append((byte)36);
			base.Comm.Append((byte)204);
			base.Comm.Append((byte)80);
			base.Comm.Append((byte)18);
			base.Comm.Append((byte)8);
			base.Comm.Append((byte)3);
			base.Comm.Append((byte)1);
			base.Comm.Append((byte)6);
			base.Comm.Append((byte)10);
			base.Comm.Append((byte)9);
			base.Comm.Append((byte)1);
			base.Comm.Append((byte)1);
			base.Comm.Append((byte)0);
			base.Comm.Append((byte)0);
			base.Comm.Append(b, 7, pad);
			array = base.Comm.Append(connectionParameters.ApplicationName, 30, pad);
			base.Comm.Append((byte)((array.Length >= 30) ? 30u : ((uint)array.Length)));
			array = base.Comm.Append(base.DataSource, 30, pad);
			base.Comm.Append((byte)((array.Length >= 30) ? 30u : ((uint)array.Length)));
			base.Comm.Append(b, 2, pad);
			array = base.Comm.Append(Tds.GetPlainPassword(connectionParameters.Password), 253, pad);
			base.Comm.Append((byte)((array.Length >= 253) ? 255u : ((uint)(array.Length + 2))));
			base.Comm.Append((byte)((byte)Version / 10));
			base.Comm.Append((byte)((byte)Version % 10));
			base.Comm.Append((byte)0);
			base.Comm.Append((byte)0);
			array = base.Comm.Append(connectionParameters.ProgName, 10, pad);
			base.Comm.Append((byte)((array.Length >= 10) ? 10u : ((uint)array.Length)));
			base.Comm.Append((byte)6);
			base.Comm.Append((byte)0);
			base.Comm.Append((byte)0);
			base.Comm.Append((byte)0);
			base.Comm.Append((byte)0);
			base.Comm.Append((byte)13);
			base.Comm.Append((byte)17);
			array = base.Comm.Append(base.Language, 30, pad);
			base.Comm.Append((byte)((array.Length >= 30) ? 30u : ((uint)array.Length)));
			base.Comm.Append((byte)1);
			base.Comm.Append((short)0);
			base.Comm.Append(b, 8, pad);
			base.Comm.Append((short)0);
			base.Comm.Append((byte)0);
			array = base.Comm.Append(base.Charset, 30, pad);
			base.Comm.Append((byte)((array.Length >= 30) ? 30u : ((uint)array.Length)));
			base.Comm.Append((byte)1);
			array = base.Comm.Append(base.PacketSize.ToString(), 6, pad);
			base.Comm.Append((byte)3);
			base.Comm.Append(b, 8, pad);
			base.Comm.SendPacket();
			base.MoreResults = true;
			SkipToEnd();
			return IsConnected;
		}

		protected override void ProcessColumnInfo()
		{
			int tdsShort = base.Comm.GetTdsShort();
			int num = 0;
			while (num < tdsShort)
			{
				byte value = 0;
				byte value2 = 0;
				int num2 = -1;
				byte[] array = new byte[4];
				for (int i = 0; i < 4; i++)
				{
					array[i] = base.Comm.GetByte();
					num++;
				}
				bool value3 = (array[2] & 1) > 0;
				bool flag = (array[2] & 0xC) > 0;
				string baseTableName = string.Empty;
				TdsColumnType tdsColumnType = (TdsColumnType)base.Comm.GetByte();
				num++;
				switch (tdsColumnType)
				{
				case TdsColumnType.Image:
				case TdsColumnType.Text:
				{
					base.Comm.Skip(4L);
					num += 4;
					int tdsShort2 = base.Comm.GetTdsShort();
					num += 2;
					baseTableName = base.Comm.GetString(tdsShort2);
					num += tdsShort2;
					num2 = int.MinValue;
					break;
				}
				case TdsColumnType.Decimal:
				case TdsColumnType.Numeric:
					num2 = base.Comm.GetByte();
					num++;
					value2 = base.Comm.GetByte();
					num++;
					value = base.Comm.GetByte();
					num++;
					break;
				default:
					if (Tds.IsFixedSizeColumn(tdsColumnType))
					{
						num2 = Tds.LookupBufferSize(tdsColumnType);
						break;
					}
					num2 = base.Comm.GetByte() & 0xFF;
					num++;
					break;
				}
				TdsDataColumn tdsDataColumn = new TdsDataColumn();
				int index = base.Columns.Add(tdsDataColumn);
				tdsDataColumn.ColumnType = tdsColumnType;
				tdsDataColumn.ColumnSize = num2;
				tdsDataColumn.ColumnName = base.ColumnNames[index] as string;
				tdsDataColumn.NumericPrecision = value2;
				tdsDataColumn.NumericScale = value;
				tdsDataColumn.IsReadOnly = !flag;
				tdsDataColumn.BaseTableName = baseTableName;
				tdsDataColumn.AllowDBNull = value3;
			}
		}
	}
	[System.MonoTODO("FIXME: Can packetsize be anything other than 512?")]
	public sealed class Tds50 : Tds
	{
		public static readonly TdsVersion Version = TdsVersion.tds50;

		private int packetSize;

		private bool isSelectQuery;

		public Tds50(string server, int port)
			: this(server, port, 512, 15)
		{
		}

		public Tds50(string server, int port, int packetSize, int timeout)
			: base(server, port, packetSize, timeout, Version)
		{
			this.packetSize = packetSize;
		}

		public string BuildExec(string sql)
		{
			if (base.Parameters == null || base.Parameters.Count == 0)
			{
				return sql;
			}
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			StringBuilder stringBuilder3 = new StringBuilder();
			int num = 0;
			foreach (TdsMetaParameter item in (IEnumerable)base.Parameters)
			{
				stringBuilder3.Append($"declare {item.Prepare()}\n");
				stringBuilder2.Append($"select {item.ParameterName}=");
				if (item.Direction == TdsParameterDirection.Input)
				{
					stringBuilder2.Append(FormatParameter(item));
				}
				else
				{
					stringBuilder2.Append("NULL");
					stringBuilder.Append(item.ParameterName);
					if (num == 0)
					{
						stringBuilder.Append("select ");
					}
					else
					{
						stringBuilder.Append(", ");
					}
					num++;
				}
				stringBuilder2.Append("\n");
			}
			return $"{stringBuilder3.ToString()}{stringBuilder2.ToString()}{sql}\n{stringBuilder.ToString()}";
		}

		public override bool Connect(TdsConnectionParameters connectionParameters)
		{
			if (IsConnected)
			{
				throw new InvalidOperationException("The connection is already open.");
			}
			byte[] b = new byte[8] { 3, 239, 101, 65, 255, 255, 255, 214 };
			byte[] b2 = new byte[8] { 0, 0, 0, 6, 72, 0, 0, 8 };
			SetCharset(connectionParameters.Charset);
			SetLanguage(connectionParameters.Language);
			byte pad = 0;
			byte[] b3 = Array.Empty<byte>();
			base.Comm.StartPacket(TdsPacketType.Logon);
			byte[] array = base.Comm.Append(connectionParameters.Hostname, 30, pad);
			base.Comm.Append((byte)((array.Length >= 30) ? 30u : ((uint)array.Length)));
			array = base.Comm.Append(connectionParameters.User, 30, pad);
			base.Comm.Append((byte)((array.Length >= 30) ? 30u : ((uint)array.Length)));
			array = base.Comm.Append(Tds.GetPlainPassword(connectionParameters.Password), 30, pad);
			base.Comm.Append((byte)((array.Length >= 30) ? 30u : ((uint)array.Length)));
			array = base.Comm.Append("37876", 30, pad);
			base.Comm.Append((byte)((array.Length >= 30) ? 30u : ((uint)array.Length)));
			base.Comm.Append((byte)3);
			base.Comm.Append((byte)1);
			base.Comm.Append((byte)6);
			base.Comm.Append((byte)10);
			base.Comm.Append((byte)9);
			base.Comm.Append((byte)1);
			base.Comm.Append((byte)1);
			base.Comm.Append((byte)0);
			base.Comm.Append((byte)0);
			base.Comm.Append(b3, 7, pad);
			array = base.Comm.Append(connectionParameters.ApplicationName, 30, pad);
			base.Comm.Append((byte)((array.Length >= 30) ? 30u : ((uint)array.Length)));
			array = base.Comm.Append(base.DataSource, 30, pad);
			base.Comm.Append((byte)((array.Length >= 30) ? 30u : ((uint)array.Length)));
			base.Comm.Append(b3, 2, pad);
			array = base.Comm.Append(Tds.GetPlainPassword(connectionParameters.Password), 253, pad);
			base.Comm.Append((byte)((array.Length >= 253) ? 255u : ((uint)(array.Length + 2))));
			base.Comm.Append((byte)5);
			base.Comm.Append((byte)0);
			base.Comm.Append((byte)0);
			base.Comm.Append((byte)0);
			array = base.Comm.Append(connectionParameters.ProgName, 10, pad);
			base.Comm.Append((byte)((array.Length >= 10) ? 10u : ((uint)array.Length)));
			base.Comm.Append((byte)6);
			base.Comm.Append((byte)0);
			base.Comm.Append((byte)0);
			base.Comm.Append((byte)0);
			base.Comm.Append((byte)0);
			base.Comm.Append((byte)13);
			base.Comm.Append((byte)17);
			array = base.Comm.Append(base.Language, 30, pad);
			base.Comm.Append((byte)((array.Length >= 30) ? 30u : ((uint)array.Length)));
			base.Comm.Append((byte)1);
			base.Comm.Append((short)0);
			base.Comm.Append(b3, 8, pad);
			base.Comm.Append((short)0);
			base.Comm.Append((byte)0);
			array = base.Comm.Append(base.Charset, 30, pad);
			base.Comm.Append((byte)((array.Length >= 30) ? 30u : ((uint)array.Length)));
			base.Comm.Append((byte)1);
			array = base.Comm.Append(packetSize.ToString(), 6, pad);
			base.Comm.Append((byte)((array.Length >= 6) ? 6u : ((uint)array.Length)));
			base.Comm.Append(b3, 8, pad);
			base.Comm.Append((byte)226);
			base.Comm.Append((short)20);
			base.Comm.Append((byte)1);
			base.Comm.Append(b);
			base.Comm.Append((byte)2);
			base.Comm.Append(b2);
			base.Comm.SendPacket();
			base.MoreResults = true;
			SkipToEnd();
			return IsConnected;
		}

		public override void ExecPrepared(string id, TdsMetaParameterCollection parameters, int timeout, bool wantResults)
		{
			base.Parameters = parameters;
			bool flag = base.Parameters != null && base.Parameters.Count > 0;
			base.Comm.StartPacket(TdsPacketType.Normal);
			base.Comm.Append((byte)231);
			base.Comm.Append((short)(id.Length + 5));
			base.Comm.Append((byte)2);
			base.Comm.Append((byte)(flag ? 1u : 0u));
			base.Comm.Append((byte)id.Length);
			base.Comm.Append(id);
			base.Comm.Append((short)0);
			if (flag)
			{
				SendParamFormat();
				SendParams();
			}
			base.MoreResults = true;
			base.Comm.SendPacket();
			CheckForData(timeout);
			if (!wantResults)
			{
				SkipToEnd();
			}
		}

		public override void Execute(string sql, TdsMetaParameterCollection parameters, int timeout, bool wantResults)
		{
			base.Parameters = parameters;
			string sql2 = BuildExec(sql);
			ExecuteQuery(sql2, timeout, wantResults);
		}

		public override void ExecProc(string commandText, TdsMetaParameterCollection parameters, int timeout, bool wantResults)
		{
			base.Parameters = parameters;
			ExecuteQuery(BuildProcedureCall(commandText), timeout, wantResults);
		}

		private string BuildProcedureCall(string procedure)
		{
			string text = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			StringBuilder stringBuilder3 = new StringBuilder();
			int num = 0;
			if (base.Parameters != null)
			{
				foreach (TdsMetaParameter item in (IEnumerable)base.Parameters)
				{
					if (item.Direction != TdsParameterDirection.Input)
					{
						if (num == 0)
						{
							stringBuilder2.Append("select ");
						}
						else
						{
							stringBuilder2.Append(", ");
						}
						stringBuilder2.Append(item.ParameterName);
						stringBuilder.Append($"declare {item.Prepare()}\n");
						if (item.Direction != TdsParameterDirection.ReturnValue)
						{
							if (item.Direction == TdsParameterDirection.InputOutput)
							{
								stringBuilder3.Append($"set {FormatParameter(item)}\n");
							}
							else
							{
								stringBuilder3.Append($"set {item.ParameterName}=NULL\n");
							}
						}
						num++;
					}
					if (item.Direction == TdsParameterDirection.ReturnValue)
					{
						text = item.ParameterName + "=";
					}
				}
			}
			text = "exec " + text;
			return $"{stringBuilder.ToString()}{stringBuilder3.ToString()}{text}{procedure} {BuildParameters()}\n{stringBuilder2.ToString()}";
		}

		private string BuildParameters()
		{
			if (base.Parameters == null || base.Parameters.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (TdsMetaParameter item in (IEnumerable)base.Parameters)
			{
				if (item.Direction != TdsParameterDirection.ReturnValue)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					if (item.Direction == TdsParameterDirection.InputOutput)
					{
						stringBuilder.Append(string.Format("{0}={0} output", item.ParameterName));
					}
					else
					{
						stringBuilder.Append(FormatParameter(item));
					}
				}
			}
			return stringBuilder.ToString();
		}

		private string FormatParameter(TdsMetaParameter parameter)
		{
			if (parameter.Direction == TdsParameterDirection.Output)
			{
				return $"{parameter.ParameterName} output";
			}
			if (parameter.Value == null || parameter.Value == DBNull.Value)
			{
				return "NULL";
			}
			switch (parameter.TypeName)
			{
			case "smalldatetime":
			case "datetime":
			{
				DateTime dateTime = (DateTime)parameter.Value;
				return string.Format(CultureInfo.InvariantCulture, "'{0:MMM dd yyyy hh:mm:ss tt}'", dateTime);
			}
			case "bigint":
			case "decimal":
			case "float":
			case "int":
			case "money":
			case "real":
			case "smallint":
			case "smallmoney":
			case "tinyint":
				return parameter.Value.ToString();
			case "nvarchar":
			case "nchar":
				return string.Format("N'{0}'", parameter.Value.ToString().Replace("'", "''"));
			case "uniqueidentifier":
				return string.Format("0x{0}", ((Guid)parameter.Value).ToString("N"));
			case "bit":
				if (parameter.Value.GetType() == typeof(bool))
				{
					return (!(bool)parameter.Value) ? "0x0" : "0x1";
				}
				return parameter.Value.ToString();
			case "image":
			case "binary":
			case "varbinary":
				return string.Format("0x{0}", BitConverter.ToString((byte[])parameter.Value).Replace("-", string.Empty).ToLower());
			default:
				return string.Format("'{0}'", parameter.Value.ToString().Replace("'", "''"));
			}
		}

		public override string Prepare(string sql, TdsMetaParameterCollection parameters)
		{
			base.Parameters = parameters;
			Random random = new Random();
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < 25; i++)
			{
				stringBuilder.Append((char)(random.Next(26) + 65));
			}
			string text = stringBuilder.ToString();
			sql = $"create proc {text} as\n{sql}";
			short s = (short)(text.Length + sql.Length + 5);
			base.Comm.StartPacket(TdsPacketType.Normal);
			base.Comm.Append((byte)231);
			base.Comm.Append(s);
			base.Comm.Append((byte)1);
			base.Comm.Append((byte)0);
			base.Comm.Append((byte)text.Length);
			base.Comm.Append(text);
			base.Comm.Append((short)sql.Length);
			base.Comm.Append(sql);
			base.Comm.SendPacket();
			base.MoreResults = true;
			SkipToEnd();
			return text;
		}

		protected override void ProcessColumnInfo()
		{
			isSelectQuery = true;
			base.Comm.GetTdsShort();
			int tdsShort = base.Comm.GetTdsShort();
			for (int i = 0; i < tdsShort; i++)
			{
				string columnName = base.Comm.GetString(base.Comm.GetByte());
				int num = base.Comm.GetByte();
				bool value = (num & 1) > 0;
				bool value2 = (num & 2) > 0;
				bool value3 = (num & 4) > 0;
				bool flag = (num & 0x10) > 0;
				bool value4 = (num & 0x20) > 0;
				bool value5 = (num & 0x40) > 0;
				base.Comm.Skip(4L);
				byte b = base.Comm.GetByte();
				bool flag2 = b == 36;
				TdsColumnType tdsColumnType = (TdsColumnType)b;
				int num2 = 0;
				byte value6 = 0;
				byte value7 = 0;
				if (tdsColumnType != TdsColumnType.Text && tdsColumnType != TdsColumnType.Image)
				{
					num2 = ((!Tds.IsFixedSizeColumn(tdsColumnType)) ? base.Comm.GetByte() : Tds.LookupBufferSize(tdsColumnType));
				}
				else
				{
					num2 = base.Comm.GetTdsInt();
					base.Comm.Skip(base.Comm.GetTdsShort());
				}
				if (tdsColumnType == TdsColumnType.Decimal || tdsColumnType == TdsColumnType.Numeric)
				{
					value6 = base.Comm.GetByte();
					value7 = base.Comm.GetByte();
				}
				base.Comm.Skip((int)base.Comm.GetByte());
				if (flag2)
				{
					base.Comm.Skip(base.Comm.GetTdsShort());
				}
				TdsDataColumn tdsDataColumn = new TdsDataColumn();
				base.Columns.Add(tdsDataColumn);
				tdsDataColumn.ColumnType = tdsColumnType;
				tdsDataColumn.ColumnName = columnName;
				tdsDataColumn.IsIdentity = value5;
				tdsDataColumn.IsRowVersion = value3;
				tdsDataColumn.ColumnType = tdsColumnType;
				tdsDataColumn.ColumnSize = num2;
				tdsDataColumn.NumericPrecision = value6;
				tdsDataColumn.NumericScale = value7;
				tdsDataColumn.IsReadOnly = !flag;
				tdsDataColumn.IsKey = value2;
				tdsDataColumn.AllowDBNull = value4;
				tdsDataColumn.IsHidden = value;
			}
		}

		private void SendParamFormat()
		{
			base.Comm.Append((byte)236);
			int num = 2 + 8 * base.Parameters.Count;
			foreach (TdsMetaParameter item in (IEnumerable)base.Parameters)
			{
				TdsColumnType metaType = item.GetMetaType();
				if (!Tds.IsFixedSizeColumn(metaType))
				{
					num++;
				}
				if (metaType == TdsColumnType.Numeric || metaType == TdsColumnType.Decimal)
				{
					num += 2;
				}
			}
			base.Comm.Append((short)num);
			base.Comm.Append((short)base.Parameters.Count);
			foreach (TdsMetaParameter item2 in (IEnumerable)base.Parameters)
			{
				string empty = string.Empty;
				string empty2 = string.Empty;
				int i = 0;
				byte b = 0;
				if (item2.IsNullable)
				{
					b |= 0x20;
				}
				if (item2.Direction == TdsParameterDirection.Output)
				{
					b |= 1;
				}
				TdsColumnType metaType = item2.GetMetaType();
				base.Comm.Append((byte)empty2.Length);
				base.Comm.Append(empty2);
				base.Comm.Append(b);
				base.Comm.Append(i);
				base.Comm.Append((byte)metaType);
				if (!Tds.IsFixedSizeColumn(metaType))
				{
					base.Comm.Append((byte)item2.Size);
				}
				if (metaType == TdsColumnType.Numeric || metaType == TdsColumnType.Decimal)
				{
					base.Comm.Append(item2.Precision);
					base.Comm.Append(item2.Scale);
				}
				base.Comm.Append((byte)empty.Length);
				base.Comm.Append(empty);
			}
		}

		private void SendParams()
		{
			base.Comm.Append((byte)215);
			foreach (TdsMetaParameter item in (IEnumerable)base.Parameters)
			{
				TdsColumnType metaType = item.GetMetaType();
				bool flag = item.Value == DBNull.Value || item.Value == null;
				if (!Tds.IsFixedSizeColumn(metaType))
				{
					base.Comm.Append((byte)item.GetActualSize());
				}
				if (!flag)
				{
					base.Comm.Append(item.Value);
				}
			}
		}

		public override void Unprepare(string statementId)
		{
			base.Comm.StartPacket(TdsPacketType.Normal);
			base.Comm.Append((byte)231);
			base.Comm.Append((short)(3 + statementId.Length));
			base.Comm.Append((byte)4);
			base.Comm.Append((byte)0);
			base.Comm.Append((byte)statementId.Length);
			base.Comm.Append(statementId);
			base.MoreResults = true;
			base.Comm.SendPacket();
			SkipToEnd();
		}

		protected override bool IsValidRowCount(byte status, byte op)
		{
			if (isSelectQuery)
			{
				return isSelectQuery = false;
			}
			if ((status & 0x40) != 0 || (status & 0x10) == 0)
			{
				return false;
			}
			return true;
		}
	}
	public class Tds70 : Tds
	{
		private static readonly decimal SMALLMONEY_MIN = -214748.3648m;

		private static readonly decimal SMALLMONEY_MAX = 214748.3647m;

		protected virtual byte[] ClientVersion => new byte[4] { 0, 0, 0, 112 };

		protected virtual byte Precision => 28;

		[Obsolete("Use the constructor that receives a lifetime parameter")]
		public Tds70(string server, int port)
			: this(server, port, 512, 15, 0)
		{
		}

		[Obsolete("Use the constructor that receives a lifetime parameter")]
		public Tds70(string server, int port, int packetSize, int timeout)
			: this(server, port, packetSize, timeout, 0, TdsVersion.tds70)
		{
		}

		[Obsolete("Use the constructor that receives a lifetime parameter")]
		public Tds70(string server, int port, int packetSize, int timeout, TdsVersion version)
			: this(server, port, packetSize, timeout, 0, version)
		{
		}

		public Tds70(string server, int port, int lifetime)
			: this(server, port, 512, 15, lifetime)
		{
		}

		public Tds70(string server, int port, int packetSize, int timeout, int lifeTime)
			: base(server, port, packetSize, timeout, lifeTime, TdsVersion.tds70)
		{
		}

		public Tds70(string server, int port, int packetSize, int timeout, int lifeTime, TdsVersion version)
			: base(server, port, packetSize, timeout, lifeTime, version)
		{
		}

		protected string BuildExec(string sql)
		{
			string arg = sql.Replace("'", "''");
			if (base.Parameters != null && base.Parameters.Count > 0)
			{
				return BuildProcedureCall($"sp_executesql N'{arg}', N'{BuildPreparedParameters()}', ");
			}
			return BuildProcedureCall($"sp_executesql N'{arg}'");
		}

		private string BuildParameters()
		{
			if (base.Parameters == null || base.Parameters.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (TdsMetaParameter item in (IEnumerable)base.Parameters)
			{
				string text = item.ParameterName;
				if (text[0] == '@')
				{
					text = text.Substring(1);
				}
				if (item.Direction != TdsParameterDirection.ReturnValue)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					if (item.Direction == TdsParameterDirection.InputOutput)
					{
						stringBuilder.AppendFormat("@{0}={0} output", text);
					}
					else
					{
						stringBuilder.Append(FormatParameter(item));
					}
				}
			}
			return stringBuilder.ToString();
		}

		private string BuildPreparedParameters()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (TdsMetaParameter item in (IEnumerable)base.Parameters)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(", ");
				}
				if (item.TypeName == "decimal")
				{
					item.Precision = ((item.Precision == 0) ? Precision : item.Precision);
				}
				stringBuilder.Append(item.Prepare());
				if (item.Direction == TdsParameterDirection.Output)
				{
					stringBuilder.Append(" output");
				}
			}
			return stringBuilder.ToString();
		}

		private string BuildPreparedQuery(string id)
		{
			return BuildProcedureCall($"sp_execute {id},");
		}

		private string BuildProcedureCall(string procedure)
		{
			string text = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			StringBuilder stringBuilder3 = new StringBuilder();
			int num = 0;
			if (base.Parameters != null)
			{
				foreach (TdsMetaParameter item in (IEnumerable)base.Parameters)
				{
					string text2 = item.ParameterName;
					if (text2[0] == '@')
					{
						text2 = text2.Substring(1);
					}
					if (item.Direction != TdsParameterDirection.Input)
					{
						if (num == 0)
						{
							stringBuilder2.Append("select ");
						}
						else
						{
							stringBuilder2.Append(", ");
						}
						stringBuilder2.Append("@" + text2);
						if (item.TypeName == "decimal")
						{
							item.Precision = ((item.Precision == 0) ? Precision : item.Precision);
						}
						stringBuilder.Append($"declare {item.Prepare()}\n");
						if (item.Direction != TdsParameterDirection.ReturnValue)
						{
							if (item.Direction == TdsParameterDirection.InputOutput)
							{
								stringBuilder3.Append($"set {FormatParameter(item)}\n");
							}
							else
							{
								stringBuilder3.Append($"set @{text2}=NULL\n");
							}
						}
						num++;
					}
					if (item.Direction == TdsParameterDirection.ReturnValue)
					{
						text = "@" + text2 + "=";
					}
				}
			}
			text = "exec " + text;
			return $"{stringBuilder.ToString()}{stringBuilder3.ToString()}{text}{procedure} {BuildParameters()}\n{stringBuilder2.ToString()}";
		}

		public override bool Connect(TdsConnectionParameters connectionParameters)
		{
			if (IsConnected)
			{
				throw new InvalidOperationException("The connection is already open.");
			}
			connectionParms = connectionParameters;
			SetLanguage(connectionParameters.Language);
			SetCharset("utf-8");
			byte[] b = Array.Empty<byte>();
			short num = 0;
			byte pad = 0;
			byte[] array = new byte[21]
			{
				6, 125, 15, 253, 255, 0, 0, 0, 0, 224,
				131, 0, 0, 104, 1, 0, 0, 9, 4, 0,
				0
			};
			byte[] array2 = new byte[21]
			{
				6, 0, 0, 0, 0, 0, 0, 0, 0, 224,
				3, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0
			};
			byte[] array3 = null;
			array3 = ((!connectionParameters.DomainLogin) ? array2 : array);
			string text = connectionParameters.User;
			string text2 = null;
			int num2 = text.IndexOf("\\");
			if (num2 == -1)
			{
				text2 = (connectionParameters.DefaultDomain = Environment.UserDomainName);
			}
			else
			{
				text2 = text.Substring(0, num2);
				text = text.Substring(num2 + 1);
				connectionParameters.DefaultDomain = text2;
				connectionParameters.User = text;
			}
			short num3 = (short)(86 + (connectionParameters.Hostname.Length + connectionParameters.ApplicationName.Length + base.DataSource.Length + connectionParameters.LibraryName.Length + base.Language.Length + connectionParameters.Database.Length + connectionParameters.AttachDBFileName.Length) * 2);
			if (connectionParameters.DomainLogin)
			{
				num = (short)(32 + (connectionParameters.Hostname.Length + text2.Length));
				num3 += num;
			}
			else
			{
				num3 += (short)((text.Length + connectionParameters.Password.Length) * 2);
			}
			int i = num3;
			base.Comm.StartPacket(TdsPacketType.Logon70);
			base.Comm.Append(i);
			base.Comm.Append(ClientVersion);
			base.Comm.Append(base.PacketSize);
			base.Comm.Append(b, 3, pad);
			base.Comm.Append(array3);
			short num4 = 86;
			base.Comm.Append(num4);
			base.Comm.Append((short)connectionParameters.Hostname.Length);
			num4 += (short)(connectionParameters.Hostname.Length * 2);
			if (connectionParameters.DomainLogin)
			{
				base.Comm.Append((short)0);
				base.Comm.Append((short)0);
				base.Comm.Append((short)0);
				base.Comm.Append((short)0);
			}
			else
			{
				base.Comm.Append(num4);
				base.Comm.Append((short)text.Length);
				num4 += (short)(text.Length * 2);
				base.Comm.Append(num4);
				base.Comm.Append((short)connectionParameters.Password.Length);
				num4 += (short)(connectionParameters.Password.Length * 2);
			}
			base.Comm.Append(num4);
			base.Comm.Append((short)connectionParameters.ApplicationName.Length);
			num4 += (short)(connectionParameters.ApplicationName.Length * 2);
			base.Comm.Append(num4);
			base.Comm.Append((short)base.DataSource.Length);
			num4 += (short)(base.DataSource.Length * 2);
			base.Comm.Append(num4);
			base.Comm.Append((short)0);
			base.Comm.Append(num4);
			base.Comm.Append((short)connectionParameters.LibraryName.Length);
			num4 += (short)(connectionParameters.LibraryName.Length * 2);
			base.Comm.Append(num4);
			base.Comm.Append((short)base.Language.Length);
			num4 += (short)(base.Language.Length * 2);
			base.Comm.Append(num4);
			base.Comm.Append((short)connectionParameters.Database.Length);
			num4 += (short)(connectionParameters.Database.Length * 2);
			base.Comm.Append((byte)0);
			base.Comm.Append((byte)0);
			base.Comm.Append((byte)0);
			base.Comm.Append((byte)0);
			base.Comm.Append((byte)0);
			base.Comm.Append((byte)0);
			base.Comm.Append(num4);
			if (connectionParameters.DomainLogin)
			{
				base.Comm.Append(num);
				num4 += num;
			}
			else
			{
				base.Comm.Append((short)0);
			}
			base.Comm.Append(num4);
			base.Comm.Append((short)connectionParameters.AttachDBFileName.Length);
			num4 += (short)(connectionParameters.AttachDBFileName.Length * 2);
			base.Comm.Append(connectionParameters.Hostname);
			if (!connectionParameters.DomainLogin)
			{
				base.Comm.Append(connectionParameters.User);
				string s = EncryptPassword(connectionParameters.Password);
				base.Comm.Append(s);
			}
			base.Comm.Append(connectionParameters.ApplicationName);
			base.Comm.Append(base.DataSource);
			base.Comm.Append(connectionParameters.LibraryName);
			base.Comm.Append(base.Language);
			base.Comm.Append(connectionParameters.Database);
			if (connectionParameters.DomainLogin)
			{
				Type1Message type1Message = new Type1Message();
				type1Message.Domain = text2;
				type1Message.Host = connectionParameters.Hostname;
				type1Message.Flags = NtlmFlags.NegotiateUnicode | NtlmFlags.NegotiateNtlm | NtlmFlags.NegotiateDomainSupplied | NtlmFlags.NegotiateWorkstationSupplied | NtlmFlags.NegotiateAlwaysSign;
				base.Comm.Append(type1Message.GetBytes());
			}
			base.Comm.Append(connectionParameters.AttachDBFileName);
			base.Comm.SendPacket();
			base.MoreResults = true;
			SkipToEnd();
			return IsConnected;
		}

		private static string EncryptPassword(SecureString secPass)
		{
			int num = 23130;
			int length = secPass.Length;
			char[] array = new char[length];
			string plainPassword = Tds.GetPlainPassword(secPass);
			for (int i = 0; i < length; i++)
			{
				int num2 = plainPassword[i] ^ num;
				int num3 = (num2 >> 4) & 0xF0F;
				int num4 = (num2 << 4) & 0xF0F0;
				array[i] = (char)(num3 | num4);
			}
			return new string(array);
		}

		public override bool Reset()
		{
			if (!base.Comm.IsConnected())
			{
				return false;
			}
			base.Comm.ResetConnection = true;
			base.Reset();
			return true;
		}

		public override void ExecPrepared(string commandText, TdsMetaParameterCollection parameters, int timeout, bool wantResults)
		{
			base.Parameters = parameters;
			ExecuteQuery(BuildPreparedQuery(commandText), timeout, wantResults);
		}

		public override void ExecProc(string commandText, TdsMetaParameterCollection parameters, int timeout, bool wantResults)
		{
			base.Parameters = parameters;
			ExecRPC(commandText, parameters, timeout, wantResults);
		}

		private void WriteRpcParameterInfo(TdsMetaParameterCollection parameters)
		{
			if (parameters == null)
			{
				return;
			}
			foreach (TdsMetaParameter item in (IEnumerable)parameters)
			{
				if (item.Direction != TdsParameterDirection.ReturnValue)
				{
					string parameterName = item.ParameterName;
					if (parameterName != null && parameterName.Length > 0 && parameterName[0] == '@')
					{
						base.Comm.Append((byte)parameterName.Length);
						base.Comm.Append(parameterName);
					}
					else
					{
						base.Comm.Append((byte)(parameterName.Length + 1));
						base.Comm.Append("@" + parameterName);
					}
					short num = 0;
					if (item.Direction != TdsParameterDirection.Input)
					{
						num |= 1;
					}
					base.Comm.Append((byte)num);
					WriteParameterInfo(item);
				}
			}
		}

		private void WritePreparedParameterInfo(TdsMetaParameterCollection parameters)
		{
			if (parameters != null)
			{
				string text = BuildPreparedParameters();
				base.Comm.Append((byte)0);
				base.Comm.Append((byte)0);
				WriteParameterInfo(new TdsMetaParameter("prep_params", (text.Length <= 4000) ? "nvarchar" : "ntext", text));
			}
		}

		protected void ExecRPC(TdsRpcProcId rpcId, string sql, TdsMetaParameterCollection parameters, int timeout, bool wantResults)
		{
			InitExec();
			base.Comm.StartPacket(TdsPacketType.Proc);
			base.Comm.Append(ushort.MaxValue);
			base.Comm.Append((ushort)rpcId);
			base.Comm.Append((short)2);
			base.Comm.Append((byte)0);
			base.Comm.Append((byte)0);
			foreach (TdsMetaParameter item in (IEnumerable)parameters)
			{
				TdsColumnType metaType = item.GetMetaType();
				if (metaType == TdsColumnType.BigNVarChar)
				{
					int actualSize = item.GetActualSize();
					if (actualSize >> 1 > 4000)
					{
						item.Size = -1;
					}
				}
			}
			TdsMetaParameter param = new TdsMetaParameter("sql", (sql.Length <= 4000) ? "nvarchar" : "ntext", sql);
			WriteParameterInfo(param);
			WritePreparedParameterInfo(parameters);
			WriteRpcParameterInfo(parameters);
			base.Comm.SendPacket();
			CheckForData(timeout);
			if (!wantResults)
			{
				SkipToEnd();
			}
		}

		protected override void ExecRPC(string rpcName, TdsMetaParameterCollection parameters, int timeout, bool wantResults)
		{
			InitExec();
			base.Comm.StartPacket(TdsPacketType.Proc);
			base.Comm.Append((short)rpcName.Length);
			base.Comm.Append(rpcName);
			base.Comm.Append((short)0);
			WriteRpcParameterInfo(parameters);
			base.Comm.SendPacket();
			CheckForData(timeout);
			if (!wantResults)
			{
				SkipToEnd();
			}
		}

		private void WriteParameterInfo(TdsMetaParameter param)
		{
			param.IsNullable = true;
			TdsColumnType tdsColumnType = param.GetMetaType();
			param.IsNullable = false;
			bool flag = false;
			int num = param.Size;
			if (num < 1)
			{
				if (num < 0)
				{
					flag = true;
				}
				num = param.GetActualSize();
			}
			if (tdsColumnType != TdsColumnType.IntN && tdsColumnType != TdsColumnType.DateTimeN && (param.Value == null || param.Value == DBNull.Value))
			{
				num = 0;
			}
			TdsColumnType tdsColumnType2 = tdsColumnType;
			switch (tdsColumnType)
			{
			case TdsColumnType.BigNVarChar:
				if (num == param.Size)
				{
					num <<= 1;
				}
				if (num >> 1 > 4000)
				{
					tdsColumnType = TdsColumnType.NText;
				}
				break;
			case TdsColumnType.BigVarChar:
				if (num > 8000)
				{
					tdsColumnType = TdsColumnType.Text;
				}
				break;
			case TdsColumnType.BigVarBinary:
				if (num > 8000)
				{
					tdsColumnType = TdsColumnType.Image;
				}
				break;
			case TdsColumnType.DateTime2:
			case TdsColumnType.DateTimeOffset:
				tdsColumnType = TdsColumnType.Char;
				break;
			}
			if (base.TdsVersion > TdsVersion.tds81 && flag)
			{
				base.Comm.Append((byte)tdsColumnType2);
				base.Comm.Append((short)(-1));
			}
			else if (base.ServerTdsVersion > TdsVersion.tds70 && tdsColumnType2 == TdsColumnType.Decimal)
			{
				base.Comm.Append((byte)108);
			}
			else
			{
				base.Comm.Append((byte)tdsColumnType);
			}
			if (IsLargeType(tdsColumnType))
			{
				base.Comm.Append((short)num);
			}
			else if (IsBlobType(tdsColumnType))
			{
				base.Comm.Append(num);
			}
			else
			{
				base.Comm.Append((byte)num);
			}
			if (param.TypeName == "decimal" || param.TypeName == "numeric")
			{
				base.Comm.Append((param.Precision == 0) ? Precision : param.Precision);
				base.Comm.Append(param.Scale);
				if (param.Value != null && param.Value != DBNull.Value && (decimal)param.Value != decimal.MaxValue && (decimal)param.Value != decimal.MinValue && (decimal)param.Value != 9223372036854775807m && (decimal)param.Value != -9223372036854775808m && (decimal)param.Value != 18446744073709551615m && (decimal)param.Value != 0m)
				{
					long num2 = (long)new decimal(System.Math.Pow(10.0, (int)param.Scale));
					long num3 = (long)((decimal)param.Value * (decimal)num2);
					param.Value = num3;
				}
			}
			if (base.Collation != null && (tdsColumnType == TdsColumnType.BigChar || tdsColumnType == TdsColumnType.BigNVarChar || tdsColumnType == TdsColumnType.BigVarChar || tdsColumnType == TdsColumnType.NChar || tdsColumnType == TdsColumnType.NVarChar || tdsColumnType == TdsColumnType.Text || tdsColumnType == TdsColumnType.NText))
			{
				base.Comm.Append(base.Collation);
			}
			num = (((tdsColumnType != TdsColumnType.BigVarChar && tdsColumnType != TdsColumnType.BigNVarChar && tdsColumnType != TdsColumnType.BigVarBinary && tdsColumnType != TdsColumnType.Image) || (param.Value != null && param.Value != DBNull.Value)) ? param.GetActualSize() : (-1));
			if (IsLargeType(tdsColumnType))
			{
				base.Comm.Append((short)num);
			}
			else if (IsBlobType(tdsColumnType))
			{
				base.Comm.Append(num);
			}
			else
			{
				base.Comm.Append((byte)num);
			}
			if (num <= 0)
			{
				return;
			}
			switch (param.TypeName)
			{
			case "money":
			{
				decimal num6 = decimal.Round((decimal)param.Value, 4);
				int[] bits2 = decimal.GetBits(num6);
				if (num6 >= 0m)
				{
					base.Comm.Append(bits2[1]);
					base.Comm.Append(bits2[0]);
				}
				else
				{
					base.Comm.Append(~bits2[1]);
					base.Comm.Append(~bits2[0] + 1);
				}
				break;
			}
			case "smallmoney":
			{
				decimal num4 = decimal.Round((decimal)param.Value, 4);
				if (num4 < SMALLMONEY_MIN || num4 > SMALLMONEY_MAX)
				{
					throw new OverflowException(string.Format(CultureInfo.InvariantCulture, "Value '{0}' is not valid for SmallMoney.  Must be between {1:N4} and {2:N4}.", num4, SMALLMONEY_MIN, SMALLMONEY_MAX));
				}
				int[] bits = decimal.GetBits(num4);
				int num5 = ((num4 > 0m) ? 1 : (-1));
				base.Comm.Append(num5 * bits[0]);
				break;
			}
			case "datetime":
				base.Comm.Append((DateTime)param.Value, 8);
				break;
			case "smalldatetime":
				base.Comm.Append((DateTime)param.Value, 4);
				break;
			case "varchar":
			case "nvarchar":
			case "char":
			case "nchar":
			case "text":
			case "ntext":
			case "datetime2":
			case "datetimeoffset":
			{
				byte[] bytes = param.GetBytes();
				base.Comm.Append(bytes);
				break;
			}
			case "uniqueidentifier":
				base.Comm.Append(((Guid)param.Value).ToByteArray());
				break;
			default:
				base.Comm.Append(param.Value);
				break;
			}
		}

		public override void Execute(string commandText, TdsMetaParameterCollection parameters, int timeout, bool wantResults)
		{
			base.Parameters = parameters;
			string sql = commandText;
			if (wantResults || (base.Parameters != null && base.Parameters.Count > 0))
			{
				sql = BuildExec(commandText);
			}
			ExecuteQuery(sql, timeout, wantResults);
		}

		private string FormatParameter(TdsMetaParameter parameter)
		{
			string text = parameter.ParameterName;
			if (text[0] == '@')
			{
				text = text.Substring(1);
			}
			if (parameter.Direction == TdsParameterDirection.Output)
			{
				return string.Format("@{0}=@{0} output", text);
			}
			if (parameter.Value == null || parameter.Value == DBNull.Value)
			{
				return $"@{text}=NULL";
			}
			string text2 = null;
			switch (parameter.TypeName)
			{
			case "smalldatetime":
			case "datetime":
			{
				DateTime dateTime = Convert.ToDateTime(parameter.Value);
				text2 = string.Format(base.Locale, "'{0:MMM dd yyyy hh:mm:ss.fff tt}'", dateTime);
				break;
			}
			case "bigint":
			case "decimal":
			case "float":
			case "int":
			case "money":
			case "real":
			case "smallint":
			case "smallmoney":
			case "tinyint":
			{
				object obj = parameter.Value;
				Type type = obj.GetType();
				if (type.IsEnum)
				{
					obj = Convert.ChangeType(obj, Type.GetTypeCode(type));
				}
				text2 = obj.ToString();
				break;
			}
			case "nvarchar":
			case "nchar":
				text2 = string.Format("N'{0}'", parameter.Value.ToString().Replace("'", "''"));
				break;
			case "uniqueidentifier":
				text2 = $"'{((Guid)parameter.Value).ToString(string.Empty)}'";
				break;
			case "bit":
				text2 = ((!(parameter.Value.GetType() == typeof(bool))) ? parameter.Value.ToString() : ((!(bool)parameter.Value) ? "0x0" : "0x1"));
				break;
			case "image":
			case "binary":
			case "varbinary":
			{
				byte[] array = (byte[])parameter.Value;
				text2 = ((array.Length != 0) ? string.Format("0x{0}", BitConverter.ToString(array).Replace("-", string.Empty).ToLower()) : "0x");
				break;
			}
			default:
				text2 = string.Format("'{0}'", parameter.Value.ToString().Replace("'", "''"));
				break;
			}
			return "@" + text + "=" + text2;
		}

		public override string Prepare(string commandText, TdsMetaParameterCollection parameters)
		{
			base.Parameters = parameters;
			TdsMetaParameterCollection tdsMetaParameterCollection = new TdsMetaParameterCollection();
			TdsMetaParameter tdsMetaParameter = new TdsMetaParameter("@Handle", "int", -1);
			tdsMetaParameter.Direction = TdsParameterDirection.Output;
			tdsMetaParameterCollection.Add(tdsMetaParameter);
			tdsMetaParameterCollection.Add(new TdsMetaParameter("@VarDecl", "nvarchar", BuildPreparedParameters()));
			tdsMetaParameterCollection.Add(new TdsMetaParameter("@Query", "nvarchar", commandText));
			ExecProc("sp_prepare", tdsMetaParameterCollection, 0, wantResults: true);
			SkipToEnd();
			return base.OutputParameters[0].ToString();
		}

		protected override void ProcessColumnInfo()
		{
			int tdsShort = base.Comm.GetTdsShort();
			for (int i = 0; i < tdsShort; i++)
			{
				byte[] array = new byte[4];
				for (int j = 0; j < 4; j++)
				{
					array[j] = base.Comm.GetByte();
				}
				bool value = (array[2] & 1) > 0;
				bool flag = (array[2] & 0xC) > 0;
				bool value2 = (array[2] & 0x10) > 0;
				bool value3 = (array[2] & 0x10) > 0;
				TdsColumnType tdsColumnType = (TdsColumnType)(base.Comm.GetByte() & 0xFF);
				byte b = 0;
				if (IsLargeType(tdsColumnType))
				{
					b = (byte)tdsColumnType;
					if (tdsColumnType != TdsColumnType.NChar)
					{
						tdsColumnType -= 128;
					}
				}
				string baseTableName = null;
				int num;
				if (!IsBlobType(tdsColumnType))
				{
					num = (Tds.IsFixedSizeColumn(tdsColumnType) ? Tds.LookupBufferSize(tdsColumnType) : ((!IsLargeType((TdsColumnType)b)) ? (base.Comm.GetByte() & 0xFF) : base.Comm.GetTdsShort()));
				}
				else
				{
					num = base.Comm.GetTdsInt();
					baseTableName = base.Comm.GetString(base.Comm.GetTdsShort());
				}
				if (IsWideType(tdsColumnType))
				{
					num /= 2;
				}
				byte b2 = 0;
				byte b3 = 0;
				if (tdsColumnType == TdsColumnType.Decimal || tdsColumnType == TdsColumnType.Numeric)
				{
					b2 = base.Comm.GetByte();
					b3 = base.Comm.GetByte();
				}
				else
				{
					b2 = GetPrecision(tdsColumnType, num);
					b3 = GetScale(tdsColumnType, num);
				}
				string columnName = base.Comm.GetString(base.Comm.GetByte());
				TdsDataColumn tdsDataColumn = new TdsDataColumn();
				base.Columns.Add(tdsDataColumn);
				tdsDataColumn.ColumnType = tdsColumnType;
				tdsDataColumn.ColumnName = columnName;
				tdsDataColumn.IsAutoIncrement = value2;
				tdsDataColumn.IsIdentity = value3;
				tdsDataColumn.ColumnSize = num;
				tdsDataColumn.NumericPrecision = b2;
				tdsDataColumn.NumericScale = b3;
				tdsDataColumn.IsReadOnly = !flag;
				tdsDataColumn.AllowDBNull = value;
				tdsDataColumn.BaseTableName = baseTableName;
				tdsDataColumn.DataTypeName = Enum.GetName(typeof(TdsColumnType), b);
			}
		}

		public override void Unprepare(string statementId)
		{
			TdsMetaParameterCollection tdsMetaParameterCollection = new TdsMetaParameterCollection();
			tdsMetaParameterCollection.Add(new TdsMetaParameter("@P1", "int", int.Parse(statementId)));
			ExecProc("sp_unprepare", tdsMetaParameterCollection, 0, wantResults: false);
		}

		protected override bool IsValidRowCount(byte status, byte op)
		{
			if ((status & 0x10) == 0 || op == 193)
			{
				return false;
			}
			return true;
		}

		protected override void ProcessReturnStatus()
		{
			int tdsInt = base.Comm.GetTdsInt();
			if (base.Parameters == null)
			{
				return;
			}
			foreach (TdsMetaParameter item in (IEnumerable)base.Parameters)
			{
				if (item.Direction == TdsParameterDirection.ReturnValue)
				{
					item.Value = tdsInt;
					break;
				}
			}
		}

		private byte GetScale(TdsColumnType type, int columnSize)
		{
			return type switch
			{
				TdsColumnType.DateTime => 3, 
				TdsColumnType.DateTime4 => 0, 
				TdsColumnType.DateTimeN => columnSize switch
				{
					4 => 0, 
					8 => 3, 
					_ => throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "Fixed scale not defined for column type '{0}' with size {1}.", type, columnSize)), 
				}, 
				_ => byte.MaxValue, 
			};
		}

		private byte GetPrecision(TdsColumnType type, int columnSize)
		{
			switch (type)
			{
			case TdsColumnType.Binary:
				return byte.MaxValue;
			case TdsColumnType.Bit:
				return byte.MaxValue;
			case TdsColumnType.Char:
				return byte.MaxValue;
			case TdsColumnType.DateTime:
				return 23;
			case TdsColumnType.DateTime4:
				return 16;
			case TdsColumnType.DateTimeN:
				switch (columnSize)
				{
				case 4:
					return 16;
				case 8:
					return 23;
				}
				break;
			case TdsColumnType.Real:
				return 7;
			case TdsColumnType.Float8:
				return 15;
			case TdsColumnType.FloatN:
				switch (columnSize)
				{
				case 4:
					return 7;
				case 8:
					return 15;
				}
				break;
			case TdsColumnType.Image:
				return byte.MaxValue;
			case TdsColumnType.Int1:
				return 3;
			case TdsColumnType.Int2:
				return 5;
			case TdsColumnType.Int4:
				return 10;
			case TdsColumnType.IntN:
				switch (columnSize)
				{
				case 1:
					return 3;
				case 2:
					return 5;
				case 4:
					return 10;
				}
				break;
			case TdsColumnType.Void:
				return 1;
			case TdsColumnType.Text:
				return byte.MaxValue;
			case TdsColumnType.UniqueIdentifier:
				return byte.MaxValue;
			case TdsColumnType.VarBinary:
				return byte.MaxValue;
			case TdsColumnType.VarChar:
				return byte.MaxValue;
			case TdsColumnType.Money:
				return 19;
			case TdsColumnType.NText:
				return byte.MaxValue;
			case TdsColumnType.NVarChar:
				return byte.MaxValue;
			case TdsColumnType.BitN:
				return byte.MaxValue;
			case TdsColumnType.MoneyN:
				switch (columnSize)
				{
				case 4:
					return 10;
				case 8:
					return 19;
				}
				break;
			case TdsColumnType.Money4:
				return 10;
			case TdsColumnType.NChar:
				return byte.MaxValue;
			case TdsColumnType.BigBinary:
				return byte.MaxValue;
			case TdsColumnType.BigVarBinary:
				return byte.MaxValue;
			case TdsColumnType.BigVarChar:
				return byte.MaxValue;
			case TdsColumnType.BigNVarChar:
				return byte.MaxValue;
			case TdsColumnType.BigChar:
				return byte.MaxValue;
			case TdsColumnType.SmallMoney:
				return 10;
			case TdsColumnType.Variant:
				return byte.MaxValue;
			case TdsColumnType.BigInt:
				return byte.MaxValue;
			}
			throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "Fixed precision not defined for column type '{0}' with size {1}.", type, columnSize));
		}

		public override IAsyncResult BeginExecuteNonQuery(string cmdText, TdsMetaParameterCollection parameters, AsyncCallback callback, object state)
		{
			base.Parameters = parameters;
			string sql = cmdText;
			if (base.Parameters != null && base.Parameters.Count > 0)
			{
				sql = BuildExec(cmdText);
			}
			return BeginExecuteQueryInternal(sql, wantResults: false, callback, state);
		}

		public override void EndExecuteNonQuery(IAsyncResult ar)
		{
			EndExecuteQueryInternal(ar);
		}

		public override IAsyncResult BeginExecuteQuery(string cmdText, TdsMetaParameterCollection parameters, AsyncCallback callback, object state)
		{
			base.Parameters = parameters;
			string sql = cmdText;
			if (base.Parameters != null && base.Parameters.Count > 0)
			{
				sql = BuildExec(cmdText);
			}
			return BeginExecuteQueryInternal(sql, wantResults: true, callback, state);
		}

		public override void EndExecuteQuery(IAsyncResult ar)
		{
			EndExecuteQueryInternal(ar);
		}

		public override IAsyncResult BeginExecuteProcedure(string prolog, string epilog, string cmdText, bool IsNonQuery, TdsMetaParameterCollection parameters, AsyncCallback callback, object state)
		{
			base.Parameters = parameters;
			string arg = BuildProcedureCall(cmdText);
			string sql = $"{prolog};{arg};{epilog};";
			return BeginExecuteQueryInternal(sql, !IsNonQuery, callback, state);
		}

		public override void EndExecuteProcedure(IAsyncResult ar)
		{
			EndExecuteQueryInternal(ar);
		}
	}
	public class Tds80 : Tds70
	{
		public static readonly TdsVersion Version = TdsVersion.tds80;

		protected override byte[] ClientVersion => new byte[4] { 0, 0, 0, 113 };

		protected override byte Precision => 38;

		[Obsolete("Use the constructor that receives a lifetime parameter")]
		public Tds80(string server, int port)
			: this(server, port, 512, 15, 0)
		{
		}

		[Obsolete("Use the constructor that receives a lifetime parameter")]
		public Tds80(string server, int port, int packetSize, int timeout)
			: base(server, port, packetSize, timeout, 0, Version)
		{
		}

		public Tds80(string server, int port, int lifetime)
			: this(server, port, 512, 15, lifetime)
		{
		}

		public Tds80(string server, int port, int packetSize, int timeout, int lifeTime)
			: base(server, port, packetSize, timeout, lifeTime, Version)
		{
		}

		public override bool Connect(TdsConnectionParameters connectionParameters)
		{
			return base.Connect(connectionParameters);
		}

		protected override void ProcessColumnInfo()
		{
			if (base.TdsVersion < TdsVersion.tds80)
			{
				base.ProcessColumnInfo();
				return;
			}
			int tdsShort = base.Comm.GetTdsShort();
			for (int i = 0; i < tdsShort; i++)
			{
				byte[] array = new byte[4];
				for (int j = 0; j < 4; j++)
				{
					array[j] = base.Comm.GetByte();
				}
				bool value = (array[2] & 1) > 0;
				bool flag = (array[2] & 0xC) > 0;
				bool value2 = (array[2] & 0x10) > 0;
				bool value3 = (array[2] & 0x10) > 0;
				TdsColumnType tdsColumnType = (TdsColumnType)(base.Comm.GetByte() & 0xFF);
				if ((byte)tdsColumnType == 239)
				{
					tdsColumnType = TdsColumnType.NChar;
				}
				TdsColumnType tdsColumnType2 = tdsColumnType;
				if (IsLargeType(tdsColumnType) && tdsColumnType != TdsColumnType.NChar)
				{
					tdsColumnType -= 128;
				}
				string baseTableName = null;
				byte[] array2 = null;
				int value4 = 0;
				int value5 = 0;
				int num = (IsBlobType(tdsColumnType) ? base.Comm.GetTdsInt() : (Tds.IsFixedSizeColumn(tdsColumnType) ? Tds.LookupBufferSize(tdsColumnType) : ((!IsLargeType(tdsColumnType2)) ? (base.Comm.GetByte() & 0xFF) : base.Comm.GetTdsShort())));
				if (tdsColumnType2 == TdsColumnType.BigChar || tdsColumnType2 == TdsColumnType.BigNVarChar || tdsColumnType2 == TdsColumnType.BigVarChar || tdsColumnType2 == TdsColumnType.NChar || tdsColumnType2 == TdsColumnType.NVarChar || tdsColumnType2 == TdsColumnType.Text || tdsColumnType2 == TdsColumnType.NText)
				{
					array2 = base.Comm.GetBytes(5, exclusiveBuffer: true);
					value4 = TdsCollation.LCID(array2);
					value5 = TdsCollation.SortId(array2);
				}
				if (IsBlobType(tdsColumnType))
				{
					baseTableName = base.Comm.GetString(base.Comm.GetTdsShort());
				}
				byte value6 = 0;
				byte value7 = 0;
				switch (tdsColumnType)
				{
				case TdsColumnType.NText:
				case TdsColumnType.NVarChar:
				case TdsColumnType.NChar:
					num /= 2;
					break;
				case TdsColumnType.Decimal:
				case TdsColumnType.Numeric:
					value6 = base.Comm.GetByte();
					value7 = base.Comm.GetByte();
					break;
				}
				string columnName = base.Comm.GetString(base.Comm.GetByte());
				TdsDataColumn tdsDataColumn = new TdsDataColumn();
				base.Columns.Add(tdsDataColumn);
				tdsDataColumn.ColumnType = tdsColumnType;
				tdsDataColumn.ColumnName = columnName;
				tdsDataColumn.IsAutoIncrement = value2;
				tdsDataColumn.IsIdentity = value3;
				tdsDataColumn.ColumnSize = num;
				tdsDataColumn.NumericPrecision = value6;
				tdsDataColumn.NumericScale = value7;
				tdsDataColumn.IsReadOnly = !flag;
				tdsDataColumn.AllowDBNull = value;
				tdsDataColumn.BaseTableName = baseTableName;
				tdsDataColumn.LCID = value4;
				tdsDataColumn.SortOrder = value5;
			}
		}

		protected override void ProcessOutputParam()
		{
			if (base.TdsVersion < TdsVersion.tds80)
			{
				base.ProcessOutputParam();
				return;
			}
			GetSubPacketLength();
			base.Comm.Skip((base.Comm.GetByte() & 0xFF) << 1);
			base.Comm.Skip(1L);
			base.Comm.Skip(4L);
			TdsColumnType value = (TdsColumnType)base.Comm.GetByte();
			object columnValue = GetColumnValue(value, outParam: true);
			base.OutputParameters.Add(columnValue);
		}

		public override void Execute(string commandText, TdsMetaParameterCollection parameters, int timeout, bool wantResults)
		{
			if (base.TdsVersion < TdsVersion.tds80)
			{
				base.Execute(commandText, parameters, timeout, wantResults);
				return;
			}
			base.Parameters = parameters;
			string sql = commandText;
			if (base.Parameters != null && base.Parameters.Count > 0)
			{
				ExecRPC(TdsRpcProcId.ExecuteSql, commandText, parameters, timeout, wantResults);
				return;
			}
			if (wantResults)
			{
				sql = BuildExec(commandText);
			}
			ExecuteQuery(sql, timeout, wantResults);
		}

		public override void ExecPrepared(string commandText, TdsMetaParameterCollection parameters, int timeout, bool wantResults)
		{
			base.Parameters = parameters;
			if (base.TdsVersion < TdsVersion.tds80 || base.Parameters == null || base.Parameters.Count < 1)
			{
				base.ExecPrepared(commandText, parameters, timeout, wantResults);
				return;
			}
			TdsMetaParameterCollection tdsMetaParameterCollection = new TdsMetaParameterCollection();
			tdsMetaParameterCollection.Add(new TdsMetaParameter("@Handle", "int", int.Parse(commandText)));
			foreach (TdsMetaParameter item in (IEnumerable)base.Parameters)
			{
				tdsMetaParameterCollection.Add(item);
			}
			ExecRPC("sp_execute", tdsMetaParameterCollection, timeout, wantResults);
		}
	}
	public class TdsBigDecimal
	{
		private bool isNegative;

		private byte precision;

		private byte scale;

		private int[] data;

		public int[] Data => data;

		public byte Precision => precision;

		public byte Scale => scale;

		public bool IsNegative => isNegative;

		public TdsBigDecimal(byte precision, byte scale, bool isNegative, int[] data)
		{
			this.isNegative = isNegative;
			this.precision = precision;
			this.scale = scale;
			this.data = data;
		}
	}
	public class TdsBulkCopy
	{
		private Tds tds;

		public TdsBulkCopy(Tds tds)
		{
			this.tds = tds;
		}

		public bool SendColumnMetaData(string colMetaData)
		{
			tds.Comm.StartPacket(TdsPacketType.Query);
			tds.Comm.Append(colMetaData);
			tds.ExecBulkCopyMetaData(30, wantResults: false);
			return true;
		}

		public bool BulkCopyStart(TdsMetaParameterCollection parameters)
		{
			tds.Comm.StartPacket(TdsPacketType.Bulk);
			tds.Comm.Append((byte)129);
			short num = 0;
			foreach (TdsMetaParameter item in (IEnumerable)parameters)
			{
				if (item.Value == null)
				{
					num++;
				}
			}
			tds.Comm.Append(num);
			if (parameters != null)
			{
				foreach (TdsMetaParameter item2 in (IEnumerable)parameters)
				{
					if (item2.Value == null)
					{
						tds.Comm.Append((short)0);
						if (item2.IsNullable)
						{
							tds.Comm.Append((short)9);
						}
						else
						{
							tds.Comm.Append((short)8);
						}
						WriteParameterInfo(item2);
						tds.Comm.Append((byte)item2.ParameterName.Length);
						tds.Comm.Append(item2.ParameterName);
					}
				}
			}
			return true;
		}

		public bool BulkCopyData(object o, bool isNewRow, int size, TdsMetaParameter parameter)
		{
			if (isNewRow)
			{
				tds.Comm.Append((byte)209);
			}
			if (o == null || o == DBNull.Value)
			{
				if (parameter.IsAnyVarCharMax)
				{
					tds.Comm.Append(Convert.ToInt64("0xFFFFFFFFFFFFFFFF", 16));
				}
				else if (parameter.IsTextType)
				{
					tds.Comm.Append(byte.MaxValue);
					tds.Comm.Append(byte.MaxValue);
				}
				else
				{
					tds.Comm.Append((byte)0);
				}
				return true;
			}
			parameter.CalculateIsVariableType();
			if (parameter.IsVariableSizeType)
			{
				if (parameter.IsAnyVarCharMax)
				{
					tds.Comm.Append(Convert.ToInt64("0xFFFFFFFFFFFFFFFE", 16));
					tds.Comm.Append(size);
				}
				else if (o.GetType() == typeof(string))
				{
					tds.Comm.Append((short)size);
				}
				else
				{
					tds.Comm.Append((byte)size);
				}
			}
			if (parameter.IsNonUnicodeText)
			{
				tds.Comm.AppendNonUnicode((string)o);
			}
			else if (parameter.IsMoneyType)
			{
				tds.Comm.AppendMoney((decimal)o, size);
			}
			else if (parameter.IsDateTimeType)
			{
				tds.Comm.Append((DateTime)o, size);
			}
			else if (parameter.IsDecimalType)
			{
				tds.Comm.AppendDecimal((decimal)o, size, parameter.Scale);
			}
			else
			{
				tds.Comm.Append(o);
			}
			if (parameter.IsAnyVarCharMax)
			{
				tds.Comm.Append(0);
			}
			return true;
		}

		public bool BulkCopyEnd()
		{
			tds.Comm.Append((byte)253);
			tds.Comm.Append((short)0);
			tds.Comm.Append((short)0);
			tds.Comm.Append(0L);
			tds.ExecBulkCopy(30, wantResults: false);
			return true;
		}

		private void WriteParameterInfo(TdsMetaParameter param)
		{
			TdsColumnType tdsColumnType = param.GetMetaType();
			int num = 0;
			num = ((param.Size != 0) ? param.Size : param.GetActualSize());
			if (tdsColumnType == TdsColumnType.BigNVarChar)
			{
				num <<= 1;
			}
			if (param.IsVarNVarCharMax)
			{
				tdsColumnType = TdsColumnType.BigNVarChar;
			}
			else if (param.IsVarCharMax)
			{
				tdsColumnType = TdsColumnType.BigVarChar;
			}
			tds.Comm.Append((byte)tdsColumnType);
			param.CalculateIsVariableType();
			if (param.IsAnyVarCharMax)
			{
				tds.Comm.Append(byte.MaxValue);
				tds.Comm.Append(byte.MaxValue);
			}
			else if (tds.IsLargeType(tdsColumnType))
			{
				tds.Comm.Append((short)num);
			}
			else if (tds.IsBlobType(tdsColumnType))
			{
				tds.Comm.Append(num);
			}
			else if (param.IsVariableSizeType)
			{
				tds.Comm.Append((byte)num);
			}
			if (param.TypeName == "decimal" || param.TypeName == "numeric")
			{
				tds.Comm.Append((byte)((param.Precision == 0) ? 29 : param.Precision));
				tds.Comm.Append(param.Scale);
			}
			if (param.IsTextType)
			{
				tds.Comm.Append((byte)9);
				tds.Comm.Append((byte)4);
				tds.Comm.Append((byte)208);
				tds.Comm.Append((byte)0);
				tds.Comm.Append((byte)52);
			}
		}
	}
	public enum TdsColumnStatus
	{
		IsExpression = 4,
		IsKey = 8,
		Hidden = 0x10,
		Rename = 0x20
	}
	public enum TdsColumnType
	{
		Binary = 45,
		Bit = 50,
		Char = 47,
		DateTime = 61,
		DateTime4 = 58,
		DateTime2 = 42,
		DateTimeOffset = 43,
		DateTimeN = 111,
		Decimal = 106,
		Real = 59,
		Float8 = 62,
		FloatN = 109,
		Image = 34,
		Int1 = 48,
		Int2 = 52,
		Int4 = 56,
		IntN = 38,
		Void = 31,
		Text = 35,
		UniqueIdentifier = 36,
		VarBinary = 37,
		VarChar = 39,
		Money = 60,
		NText = 99,
		NVarChar = 103,
		BitN = 104,
		Numeric = 108,
		MoneyN = 110,
		Money4 = 112,
		NChar = 239,
		BigBinary = 173,
		BigVarBinary = 165,
		BigVarChar = 167,
		BigNVarChar = 231,
		BigChar = 175,
		SmallMoney = 122,
		Variant = 98,
		BigInt = 127
	}
	internal sealed class TdsComm
	{
		private NetworkStream stream;

		private int packetSize;

		private TdsPacketType packetType;

		private bool connReset;

		private Encoding encoder;

		private string dataSource;

		private int commandTimeout;

		private byte[] outBuffer;

		private int outBufferLength;

		private int nextOutBufferIndex;

		private bool lsb;

		private byte[] inBuffer;

		private int inBufferLength;

		private int inBufferIndex;

		private static int headerLength = 8;

		private byte[] tmpBuf = new byte[8];

		private byte[] resBuffer = new byte[256];

		private int packetsSent;

		private int packetsReceived;

		private Socket socket;

		private TdsVersion tdsVersion;

		public int CommandTimeout
		{
			get
			{
				return commandTimeout;
			}
			set
			{
				commandTimeout = value;
			}
		}

		internal Encoding Encoder
		{
			get
			{
				return encoder;
			}
			set
			{
				encoder = value;
			}
		}

		public int PacketSize
		{
			get
			{
				return packetSize;
			}
			set
			{
				packetSize = value;
			}
		}

		public bool TdsByteOrder
		{
			get
			{
				return !lsb;
			}
			set
			{
				lsb = !value;
			}
		}

		public bool ResetConnection
		{
			get
			{
				return connReset;
			}
			set
			{
				connReset = value;
			}
		}

		public TdsComm(string dataSource, int port, int packetSize, int timeout, TdsVersion tdsVersion)
		{
			this.packetSize = packetSize;
			this.tdsVersion = tdsVersion;
			this.dataSource = dataSource;
			outBuffer = new byte[packetSize];
			inBuffer = new byte[packetSize];
			outBufferLength = packetSize;
			inBufferLength = packetSize;
			lsb = true;
			bool flag = false;
			IPEndPoint remoteEP;
			try
			{
				if (IPAddress.TryParse(this.dataSource, out var address))
				{
					remoteEP = new IPEndPoint(address, port);
				}
				else
				{
					IPHostEntry hostEntry = Dns.GetHostEntry(this.dataSource);
					remoteEP = new IPEndPoint(hostEntry.AddressList[0], port);
				}
			}
			catch (SocketException innerException)
			{
				throw new TdsInternalException("Server does not exist or connection refused.", innerException);
			}
			try
			{
				this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				IAsyncResult asyncResult = this.socket.BeginConnect(remoteEP, null, null);
				int num = timeout * 1000;
				if (timeout > 0 && !asyncResult.IsCompleted && !asyncResult.AsyncWaitHandle.WaitOne(num, exitContext: false))
				{
					throw Tds.CreateTimeoutException(dataSource, "Open()");
				}
				this.socket.EndConnect(asyncResult);
				try
				{
					this.socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, 1);
				}
				catch (SocketException)
				{
				}
				try
				{
					this.socket.NoDelay = true;
					this.socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, num);
					this.socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, num);
				}
				catch
				{
				}
				stream = new NetworkStream(this.socket, ownsSocket: true);
			}
			catch (SocketException innerException2)
			{
				flag = true;
				throw new TdsInternalException("Server does not exist or connection refused.", innerException2);
			}
			catch (Exception)
			{
				flag = true;
				throw;
			}
			finally
			{
				if (flag && this.socket != null)
				{
					try
					{
						Socket socket = this.socket;
						this.socket = null;
						socket.Close();
					}
					catch
					{
					}
				}
			}
			if (!this.socket.Connected)
			{
				throw new TdsInternalException("Server does not exist or connection refused.", null);
			}
			packetsSent = 1;
		}

		public byte[] Swap(byte[] toswap)
		{
			byte[] array = new byte[toswap.Length];
			for (int i = 0; i < toswap.Length; i++)
			{
				array[toswap.Length - i - 1] = toswap[i];
			}
			return array;
		}

		public void SendIfFull()
		{
			if (nextOutBufferIndex == outBufferLength)
			{
				SendPhysicalPacket(isLastSegment: false);
				nextOutBufferIndex = headerLength;
			}
		}

		public void SendIfFull(int reserve)
		{
			if (nextOutBufferIndex + reserve > outBufferLength)
			{
				SendPhysicalPacket(isLastSegment: false);
				nextOutBufferIndex = headerLength;
			}
		}

		public void Append(object o)
		{
			if (o == null || o == DBNull.Value)
			{
				Append((byte)0);
				return;
			}
			switch (Type.GetTypeCode(o.GetType()))
			{
			case TypeCode.Byte:
				Append((byte)o);
				return;
			case TypeCode.Boolean:
				if ((bool)o)
				{
					Append((byte)1);
				}
				else
				{
					Append((byte)0);
				}
				return;
			case TypeCode.Object:
				if (o is byte[])
				{
					Append((byte[])o);
					return;
				}
				if (o is Guid)
				{
					Append(((Guid)o).ToByteArray());
					return;
				}
				break;
			case TypeCode.Int16:
				Append((short)o);
				return;
			case TypeCode.Int32:
				Append((int)o);
				return;
			case TypeCode.String:
				Append((string)o);
				return;
			case TypeCode.Double:
				Append((double)o);
				return;
			case TypeCode.Single:
				Append((float)o);
				return;
			case TypeCode.Int64:
				Append((long)o);
				return;
			case TypeCode.Decimal:
				Append((decimal)o, 17);
				return;
			case TypeCode.DateTime:
				Append((DateTime)o, 8);
				return;
			}
			throw new InvalidOperationException($"Object Type :{o.GetType()} , not being appended");
		}

		public void Append(byte b)
		{
			SendIfFull();
			Store(nextOutBufferIndex, b);
			nextOutBufferIndex++;
		}

		public void Append(DateTime t, int bytes)
		{
			DateTime dateTime = new DateTime(1900, 1, 1);
			TimeSpan timeSpan = t - dateTime;
			int num = 0;
			int num2 = timeSpan.Days;
			int num3 = timeSpan.Hours;
			int num4 = timeSpan.Minutes;
			int num5 = timeSpan.Seconds;
			long num6 = timeSpan.Milliseconds;
			if (dateTime > t)
			{
				num2 = ((t.Hour <= 0 && t.Minute <= 0 && t.Second <= 0 && t.Millisecond <= 0) ? num2 : (num2 - 1));
				num3 = t.Hour;
				num4 = t.Minute;
				num5 = t.Second;
				num6 = t.Millisecond;
			}
			SendIfFull(bytes);
			switch (bytes)
			{
			case 8:
			{
				long num7 = (long)(num3 * 3600 + num4 * 60 + num5) * 1000L + num6;
				num = (int)(num7 * 300 / 1000);
				AppendInternal(num2);
				AppendInternal(num);
				break;
			}
			case 4:
				num = timeSpan.Hours * 60 + timeSpan.Minutes;
				AppendInternal((short)num2);
				AppendInternal((short)num);
				break;
			default:
				throw new Exception("Invalid No of bytes");
			}
		}

		public void Append(byte[] b)
		{
			Append(b, b.Length, 0);
		}

		public void Append(byte[] b, int len, byte pad)
		{
			int num = System.Math.Min(b.Length, len);
			int num2 = len - num;
			int num3 = 0;
			while (num > 0)
			{
				SendIfFull();
				int val = outBufferLength - nextOutBufferIndex;
				int num4 = System.Math.Min(val, num);
				Buffer.BlockCopy(b, num3, outBuffer, nextOutBufferIndex, num4);
				nextOutBufferIndex += num4;
				num -= num4;
				num3 += num4;
			}
			while (num2 > 0)
			{
				SendIfFull();
				int val2 = outBufferLength - nextOutBufferIndex;
				int num5 = System.Math.Min(val2, num2);
				for (int i = 0; i < num5; i++)
				{
					outBuffer[nextOutBufferIndex++] = pad;
				}
				num2 -= num5;
			}
		}

		private void AppendInternal(short s)
		{
			if (!lsb)
			{
				outBuffer[nextOutBufferIndex++] = (byte)((byte)(s >> 8) & 0xFF);
				outBuffer[nextOutBufferIndex++] = (byte)(s & 0xFF);
			}
			else
			{
				outBuffer[nextOutBufferIndex++] = (byte)(s & 0xFF);
				outBuffer[nextOutBufferIndex++] = (byte)((byte)(s >> 8) & 0xFF);
			}
		}

		public void Append(short s)
		{
			SendIfFull(2);
			AppendInternal(s);
		}

		public void Append(ushort s)
		{
			SendIfFull(2);
			AppendInternal((short)s);
		}

		private void AppendInternal(int i)
		{
			if (!lsb)
			{
				AppendInternal((short)((short)(i >> 16) & 0xFFFF));
				AppendInternal((short)(i & 0xFFFF));
			}
			else
			{
				AppendInternal((short)(i & 0xFFFF));
				AppendInternal((short)((short)(i >> 16) & 0xFFFF));
			}
		}

		public void Append(int i)
		{
			SendIfFull(4);
			AppendInternal(i);
		}

		public void Append(string s)
		{
			if (tdsVersion < TdsVersion.tds70)
			{
				Append(encoder.GetBytes(s));
				return;
			}
			for (int i = 0; i < s.Length; i++)
			{
				SendIfFull(2);
				AppendInternal((short)s[i]);
			}
		}

		public void AppendNonUnicode(string s)
		{
			if (tdsVersion < TdsVersion.tds70)
			{
				Append(encoder.GetBytes(s));
				return;
			}
			for (int i = 0; i < s.Length; i++)
			{
				SendIfFull(1);
				Append((byte)s[i]);
			}
		}

		public byte[] Append(string s, int len, byte pad)
		{
			if (s == null)
			{
				return Array.Empty<byte>();
			}
			byte[] bytes = encoder.GetBytes(s);
			Append(bytes, len, pad);
			return bytes;
		}

		public void Append(double value)
		{
			if (!lsb)
			{
				Append(Swap(BitConverter.GetBytes(value)), 8, 0);
			}
			else
			{
				Append(BitConverter.GetBytes(value), 8, 0);
			}
		}

		public void Append(float value)
		{
			if (!lsb)
			{
				Append(Swap(BitConverter.GetBytes(value)), 4, 0);
			}
			else
			{
				Append(BitConverter.GetBytes(value), 4, 0);
			}
		}

		public void Append(long l)
		{
			SendIfFull(8);
			if (!lsb)
			{
				AppendInternal((int)((int)(l >> 32) & 0xFFFFFFFFu));
				AppendInternal((int)(l & 0xFFFFFFFFu));
			}
			else
			{
				AppendInternal((int)(l & 0xFFFFFFFFu));
				AppendInternal((int)((int)(l >> 32) & 0xFFFFFFFFu));
			}
		}

		public void Append(decimal d, int bytes)
		{
			int[] bits = decimal.GetBits(d);
			byte b = (byte)((d > 0m) ? 1 : 0);
			SendIfFull(bytes);
			Append(b);
			AppendInternal(bits[0]);
			AppendInternal(bits[1]);
			AppendInternal(bits[2]);
			AppendInternal(0);
		}

		public void AppendMoney(decimal d, int size)
		{
			SendIfFull(size);
			decimal d2 = decimal.Multiply(d, 10000m);
			if (size > 4)
			{
				long num = decimal.ToInt64(d2);
				int i = (int)((num >> 32) & 0xFFFFFFFFu);
				int i2 = (int)(num & 0xFFFFFFFFu);
				AppendInternal(i);
				AppendInternal(i2);
			}
			else
			{
				int i3 = decimal.ToInt32(d2);
				AppendInternal(i3);
			}
		}

		public void AppendDecimal(decimal d, int bytes, int scale)
		{
			decimal d2 = decimal.Multiply(d, (decimal)System.Math.Pow(10.0, scale));
			decimal d3 = System.Math.Abs(decimal.Truncate(d2));
			int[] bits = decimal.GetBits(d3);
			byte b = (byte)((d > 0m) ? 1 : 0);
			SendIfFull(bytes);
			Append(b);
			AppendInternal(bits[0]);
			AppendInternal(bits[1]);
			AppendInternal(bits[2]);
			AppendInternal(0);
		}

		public void Close()
		{
			if (stream != null)
			{
				connReset = false;
				socket = null;
				try
				{
					stream.Close();
				}
				catch
				{
				}
				stream = null;
			}
		}

		public bool IsConnected()
		{
			return socket != null && socket.Connected && (!socket.Poll(0, SelectMode.SelectRead) || socket.Available != 0);
		}

		public byte GetByte()
		{
			if (inBufferIndex >= inBufferLength)
			{
				GetPhysicalPacket();
			}
			return inBuffer[inBufferIndex++];
		}

		public byte[] GetBytes(int len, bool exclusiveBuffer)
		{
			byte[] array = null;
			if (exclusiveBuffer || len > 16384)
			{
				array = new byte[len];
			}
			else
			{
				if (resBuffer.Length < len)
				{
					resBuffer = new byte[len];
				}
				array = resBuffer;
			}
			int num = 0;
			while (num < len)
			{
				if (inBufferIndex >= inBufferLength)
				{
					GetPhysicalPacket();
				}
				int num2 = inBufferLength - inBufferIndex;
				num2 = ((num2 <= len - num) ? num2 : (len - num));
				Buffer.BlockCopy(inBuffer, inBufferIndex, array, num, num2);
				num += num2;
				inBufferIndex += num2;
			}
			return array;
		}

		public string GetString(int len, Encoding enc)
		{
			if (tdsVersion >= TdsVersion.tds70)
			{
				return GetString(len, wide: true, null);
			}
			return GetString(len, wide: false, null);
		}

		public string GetString(int len)
		{
			if (tdsVersion >= TdsVersion.tds70)
			{
				return GetString(len, wide: true);
			}
			return GetString(len, wide: false);
		}

		public string GetString(int len, bool wide, Encoding enc)
		{
			if (wide)
			{
				char[] array = new char[len];
				for (int i = 0; i < len; i++)
				{
					int num = GetByte() & 0xFF;
					int num2 = GetByte() & 0xFF;
					array[i] = (char)(num | (num2 << 8));
				}
				return new string(array);
			}
			byte[] array2 = new byte[len];
			Array.Copy(GetBytes(len, exclusiveBuffer: false), array2, len);
			if (enc != null)
			{
				return enc.GetString(array2);
			}
			return encoder.GetString(array2);
		}

		public string GetString(int len, bool wide)
		{
			return GetString(len, wide, null);
		}

		public int GetNetShort()
		{
			return Ntohs(new byte[2]
			{
				GetByte(),
				GetByte()
			}, 0);
		}

		public short GetTdsShort()
		{
			byte[] array = new byte[2];
			for (int i = 0; i < 2; i++)
			{
				array[i] = GetByte();
			}
			if (!BitConverter.IsLittleEndian)
			{
				return BitConverter.ToInt16(Swap(array), 0);
			}
			return BitConverter.ToInt16(array, 0);
		}

		public int GetTdsInt()
		{
			byte[] array = new byte[4];
			for (int i = 0; i < 4; i++)
			{
				array[i] = GetByte();
			}
			if (!BitConverter.IsLittleEndian)
			{
				return BitConverter.ToInt32(Swap(array), 0);
			}
			return BitConverter.ToInt32(array, 0);
		}

		public long GetTdsInt64()
		{
			byte[] array = new byte[8];
			for (int i = 0; i < 8; i++)
			{
				array[i] = GetByte();
			}
			if (!BitConverter.IsLittleEndian)
			{
				return BitConverter.ToInt64(Swap(array), 0);
			}
			return BitConverter.ToInt64(array, 0);
		}

		private void GetPhysicalPacket()
		{
			int physicalPacketHeader = GetPhysicalPacketHeader();
			GetPhysicalPacketData(physicalPacketHeader);
		}

		private int Read(byte[] buffer, int offset, int count)
		{
			try
			{
				return stream.Read(buffer, offset, count);
			}
			catch
			{
				socket = null;
				stream.Close();
				throw;
			}
		}

		private int GetPhysicalPacketHeader()
		{
			int num;
			for (int i = 0; i < 8; i += num)
			{
				num = Read(tmpBuf, i, 8 - i);
				if (num <= 0)
				{
					socket = null;
					stream.Close();
					throw new IOException((num != 0) ? "Connection error" : "Connection lost");
				}
			}
			TdsPacketType tdsPacketType = (TdsPacketType)tmpBuf[0];
			if (tdsPacketType != TdsPacketType.Logon && tdsPacketType != TdsPacketType.Query && tdsPacketType != TdsPacketType.Reply)
			{
				throw new Exception($"Unknown packet type {tmpBuf[0]}");
			}
			int num2 = Ntohs(tmpBuf, 2) - 8;
			if (num2 >= inBuffer.Length)
			{
				inBuffer = new byte[num2];
			}
			if (num2 < 0)
			{
				throw new Exception($"Confused by a length of {num2}");
			}
			return num2;
		}

		private void GetPhysicalPacketData(int length)
		{
			int num;
			for (int i = 0; i < length; i += num)
			{
				num = Read(inBuffer, i, length - i);
				if (num <= 0)
				{
					socket = null;
					stream.Close();
					throw new IOException((num != 0) ? "Connection error" : "Connection lost");
				}
			}
			packetsReceived++;
			inBufferLength = length;
			inBufferIndex = 0;
		}

		private static int Ntohs(byte[] buf, int offset)
		{
			int num = buf[offset + 1] & 0xFF;
			int num2 = (buf[offset] & 0xFF) << 8;
			return num2 | num;
		}

		public byte Peek()
		{
			if (inBufferIndex >= inBufferLength)
			{
				GetPhysicalPacket();
			}
			return inBuffer[inBufferIndex];
		}

		public bool Poll(int seconds, SelectMode selectMode)
		{
			return Poll(socket, seconds, selectMode);
		}

		private bool Poll(Socket s, int seconds, SelectMode selectMode)
		{
			long num = seconds * 1000000;
			bool flag = false;
			while (num > int.MaxValue)
			{
				if (s.Poll(int.MaxValue, selectMode))
				{
					return true;
				}
				num -= int.MaxValue;
			}
			return s.Poll((int)num, selectMode);
		}

		internal void ResizeOutBuf(int newSize)
		{
			if (newSize != outBufferLength)
			{
				byte[] dst = new byte[newSize];
				Buffer.BlockCopy(outBuffer, 0, dst, 0, newSize);
				outBufferLength = newSize;
				outBuffer = dst;
			}
		}

		public void SendPacket()
		{
			if (packetType != TdsPacketType.Query && packetType != TdsPacketType.Proc)
			{
				connReset = false;
			}
			SendPhysicalPacket(isLastSegment: true);
			nextOutBufferIndex = 0;
			packetType = TdsPacketType.None;
			connReset = false;
			packetsSent = 1;
		}

		private void SendPhysicalPacket(bool isLastSegment)
		{
			if (nextOutBufferIndex > headerLength || packetType == TdsPacketType.Cancel)
			{
				byte value = (byte)((isLastSegment ? 1 : 0) | (connReset ? 8 : 0));
				Store(0, (byte)packetType);
				Store(1, value);
				Store(2, (short)nextOutBufferIndex);
				Store(4, 0);
				Store(5, 0);
				if (tdsVersion >= TdsVersion.tds70)
				{
					Store(6, (byte)packetsSent);
				}
				else
				{
					Store(6, 0);
				}
				Store(7, 0);
				stream.Write(outBuffer, 0, nextOutBufferIndex);
				stream.Flush();
				if (!isLastSegment && packetType == TdsPacketType.Bulk)
				{
					Thread.Sleep(100);
				}
				packetsSent++;
			}
		}

		public void Skip(long i)
		{
			while (i > 0)
			{
				GetByte();
				i--;
			}
		}

		public void StartPacket(TdsPacketType type)
		{
			if (type != TdsPacketType.Cancel && inBufferIndex != inBufferLength)
			{
				inBufferIndex = inBufferLength;
			}
			packetType = type;
			nextOutBufferIndex = headerLength;
		}

		private void Store(int index, byte value)
		{
			outBuffer[index] = value;
		}

		private void Store(int index, short value)
		{
			outBuffer[index] = (byte)((byte)(value >> 8) & 0xFF);
			outBuffer[index + 1] = (byte)((byte)(value >> 0) & 0xFF);
		}

		public IAsyncResult BeginReadPacket(AsyncCallback callback, object stateObject)
		{
			TdsAsyncResult tdsAsyncResult = new TdsAsyncResult(callback, stateObject);
			stream.BeginRead(tmpBuf, 0, 8, OnReadPacketCallback, tdsAsyncResult);
			return tdsAsyncResult;
		}

		public int EndReadPacket(IAsyncResult ar)
		{
			if (!ar.IsCompleted)
			{
				ar.AsyncWaitHandle.WaitOne();
			}
			return (int)((TdsAsyncResult)ar).ReturnValue;
		}

		public void OnReadPacketCallback(IAsyncResult socketAsyncResult)
		{
			TdsAsyncResult tdsAsyncResult = (TdsAsyncResult)socketAsyncResult.AsyncState;
			int num;
			for (int i = stream.EndRead(socketAsyncResult); i < 8; i += num)
			{
				num = Read(tmpBuf, i, 8 - i);
				if (num <= 0)
				{
					socket = null;
					stream.Close();
					throw new IOException((num != 0) ? "Connection error" : "Connection lost");
				}
			}
			TdsPacketType tdsPacketType = (TdsPacketType)tmpBuf[0];
			if (tdsPacketType != TdsPacketType.Logon && tdsPacketType != TdsPacketType.Query && tdsPacketType != TdsPacketType.Reply)
			{
				throw new Exception($"Unknown packet type {tmpBuf[0]}");
			}
			int num2 = Ntohs(tmpBuf, 2) - 8;
			if (num2 >= inBuffer.Length)
			{
				inBuffer = new byte[num2];
			}
			if (num2 < 0)
			{
				throw new Exception($"Confused by a length of {num2}");
			}
			GetPhysicalPacketData(num2);
			int num3 = num2 + 8;
			tdsAsyncResult.ReturnValue = num3;
			tdsAsyncResult.MarkComplete();
		}
	}
	public class TdsConnectionParameters
	{
		public string ApplicationName;

		public string Database;

		public string Charset;

		public string Hostname;

		public string Language;

		public string LibraryName;

		public SecureString Password;

		public bool PasswordSet;

		public string ProgName;

		public string User;

		public bool DomainLogin;

		public string DefaultDomain;

		public string AttachDBFileName;

		public TdsConnectionParameters()
		{
			Reset();
		}

		public void Reset()
		{
			ApplicationName = "Mono";
			Database = string.Empty;
			Charset = string.Empty;
			Hostname = Dns.GetHostName();
			Language = string.Empty;
			LibraryName = "Mono";
			Password = new SecureString();
			PasswordSet = false;
			ProgName = "Mono";
			User = string.Empty;
			DomainLogin = false;
			DefaultDomain = string.Empty;
			AttachDBFileName = string.Empty;
		}
	}
	public class TdsConnectionPoolManager
	{
		private Hashtable pools = Hashtable.Synchronized(new Hashtable());

		private TdsVersion version;

		public TdsConnectionPoolManager(TdsVersion version)
		{
			this.version = version;
		}

		public TdsConnectionPool GetConnectionPool(string connectionString, TdsConnectionInfo info)
		{
			TdsConnectionPool tdsConnectionPool = (TdsConnectionPool)pools[connectionString];
			if (tdsConnectionPool == null)
			{
				pools[connectionString] = new TdsConnectionPool(this, info);
				tdsConnectionPool = (TdsConnectionPool)pools[connectionString];
			}
			return tdsConnectionPool;
		}

		public TdsConnectionPool GetConnectionPool(string connectionString)
		{
			return (TdsConnectionPool)pools[connectionString];
		}

		public virtual Tds CreateConnection(TdsConnectionInfo info)
		{
			return version switch
			{
				TdsVersion.tds42 => new Tds42(info.DataSource, info.Port, info.PacketSize, info.Timeout), 
				TdsVersion.tds50 => new Tds50(info.DataSource, info.Port, info.PacketSize, info.Timeout), 
				TdsVersion.tds70 => new Tds70(info.DataSource, info.Port, info.PacketSize, info.Timeout, info.LifeTime), 
				TdsVersion.tds80 => new Tds80(info.DataSource, info.Port, info.PacketSize, info.Timeout, info.LifeTime), 
				_ => throw new NotSupportedException(), 
			};
		}

		public IDictionary GetConnectionPool()
		{
			return pools;
		}
	}
	public class TdsConnectionInfo
	{
		public string DataSource;

		public int Port;

		public int PacketSize;

		public int Timeout;

		public int LifeTime;

		public int PoolMinSize;

		public int PoolMaxSize;

		[Obsolete("Use the constructor that receives a lifetime parameter")]
		public TdsConnectionInfo(string dataSource, int port, int packetSize, int timeout, int minSize, int maxSize)
			: this(dataSource, port, packetSize, timeout, minSize, maxSize, 0)
		{
		}

		public TdsConnectionInfo(string dataSource, int port, int packetSize, int timeout, int minSize, int maxSize, int lifeTime)
		{
			DataSource = dataSource;
			Port = port;
			PacketSize = packetSize;
			Timeout = timeout;
			PoolMinSize = minSize;
			PoolMaxSize = maxSize;
			LifeTime = lifeTime;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("DataSouce: {0}\n", DataSource);
			stringBuilder.AppendFormat("Port: {0}\n", Port);
			stringBuilder.AppendFormat("PacketSize: {0}\n", PacketSize);
			stringBuilder.AppendFormat("Timeout: {0}\n", Timeout);
			stringBuilder.AppendFormat("PoolMinSize: {0}\n", PoolMinSize);
			stringBuilder.AppendFormat("PoolMaxSize: {0}", PoolMaxSize);
			return stringBuilder.ToString();
		}
	}
	public class TdsConnectionPool
	{
		private TdsConnectionInfo info;

		private bool no_pooling;

		private TdsConnectionPoolManager manager;

		private Queue available;

		private ArrayList conns;

		private int in_progress;

		public bool Pooling
		{
			get
			{
				return !no_pooling;
			}
			set
			{
				no_pooling = !value;
			}
		}

		public TdsConnectionPool(TdsConnectionPoolManager manager, TdsConnectionInfo info)
		{
			this.info = info;
			this.manager = manager;
			conns = new ArrayList(info.PoolMaxSize);
			available = new Queue(info.PoolMaxSize);
			InitializePool();
		}

		private void InitializePool()
		{
			for (int i = conns.Count; i < info.PoolMinSize; i++)
			{
				try
				{
					Tds tds = manager.CreateConnection(info);
					conns.Add(tds);
					available.Enqueue(tds);
				}
				catch
				{
				}
			}
		}

		public Tds GetConnection()
		{
			if (no_pooling)
			{
				return manager.CreateConnection(info);
			}
			Tds tds = null;
			int num = info.PoolMaxSize * 2;
			while (true)
			{
				if (tds == null)
				{
					bool flag = false;
					lock (available)
					{
						if (available.Count > 0)
						{
							tds = (Tds)available.Dequeue();
							goto IL_01be;
						}
						Monitor.Enter(conns);
						try
						{
							if (conns.Count >= info.PoolMaxSize - in_progress)
							{
								Monitor.Exit(conns);
								if (!Monitor.Wait(available, info.Timeout * 1000))
								{
									throw new InvalidOperationException("Timeout expired. The timeout period elapsed before a connection could be obtained. A possible explanation is that all the connections in the pool are in use, and the maximum pool size is reached.");
								}
								if (available.Count > 0)
								{
									tds = (Tds)available.Dequeue();
									goto IL_01be;
								}
								continue;
							}
							flag = true;
							in_progress++;
						}
						finally
						{
							Monitor.Exit(conns);
						}
					}
					if (!flag)
					{
						continue;
					}
					try
					{
						tds = manager.CreateConnection(info);
						lock (conns)
						{
							conns.Add(tds);
						}
						return tds;
					}
					finally
					{
						lock (available)
						{
							in_progress--;
						}
					}
				}
				goto IL_01be;
				IL_01be:
				bool flag2 = true;
				Exception ex = null;
				try
				{
					flag2 = !tds.IsConnected || !tds.Reset();
				}
				catch (Exception ex2)
				{
					flag2 = true;
					ex = ex2;
				}
				if (!flag2)
				{
					break;
				}
				lock (conns)
				{
					conns.Remove(tds);
				}
				tds.Disconnect();
				num--;
				if (num == 0)
				{
					throw ex;
				}
				tds = null;
			}
			return tds;
		}

		public void ReleaseConnection(Tds connection)
		{
			if (connection == null)
			{
				return;
			}
			if (no_pooling)
			{
				connection.Disconnect();
				return;
			}
			if (connection.poolStatus == 2 || connection.Expired)
			{
				lock (conns)
				{
					conns.Remove(connection);
				}
				connection.Disconnect();
				connection = null;
			}
			lock (available)
			{
				if (connection != null)
				{
					available.Enqueue(connection);
				}
				Monitor.Pulse(available);
			}
		}

		public void ResetConnectionPool()
		{
			lock (available)
			{
				lock (conns)
				{
					for (int num = conns.Count - 1; num >= 0; num--)
					{
						Tds tds = (Tds)conns[num];
						tds.poolStatus = 2;
					}
					for (int num = available.Count - 1; num >= 0; num--)
					{
						Tds tds = (Tds)available.Dequeue();
						tds.Disconnect();
						conns.Remove(tds);
					}
					available.Clear();
					InitializePool();
				}
				Monitor.PulseAll(available);
			}
		}
	}
	public class TdsDataColumn
	{
		private Hashtable properties;

		public TdsColumnType? ColumnType { get; set; }

		public string ColumnName { get; set; }

		public int? ColumnSize { get; set; }

		public int? ColumnOrdinal { get; set; }

		public bool? IsAutoIncrement { get; set; }

		public bool? IsIdentity { get; set; }

		public bool? IsRowVersion { get; set; }

		public bool? IsUnique { get; set; }

		public bool? IsHidden { get; set; }

		public bool? IsKey { get; set; }

		public bool? IsAliased { get; set; }

		public bool? IsExpression { get; set; }

		public bool? IsReadOnly { get; set; }

		public short? NumericPrecision { get; set; }

		public short? NumericScale { get; set; }

		public string BaseServerName { get; set; }

		public string BaseCatalogName { get; set; }

		public string BaseColumnName { get; set; }

		public string BaseSchemaName { get; set; }

		public string BaseTableName { get; set; }

		public bool? AllowDBNull { get; set; }

		public int? LCID { get; set; }

		public int? SortOrder { get; set; }

		public string DataTypeName { get; set; }

		public object this[string key]
		{
			get
			{
				if (properties == null)
				{
					return null;
				}
				return properties[key];
			}
			set
			{
				if (properties == null)
				{
					properties = new Hashtable();
				}
				properties[key] = value;
			}
		}

		public TdsDataColumn()
		{
			IsAutoIncrement = false;
			IsIdentity = false;
			IsRowVersion = false;
			IsUnique = false;
			IsHidden = false;
		}
	}
	public class TdsDataColumnCollection : IEnumerable
	{
		private ArrayList list;

		public TdsDataColumn this[int index]
		{
			get
			{
				return (TdsDataColumn)list[index];
			}
			set
			{
				list[index] = value;
			}
		}

		public int Count => list.Count;

		public TdsDataColumnCollection()
		{
			list = new ArrayList();
		}

		public int Add(TdsDataColumn schema)
		{
			int num = list.Add(schema);
			schema.ColumnOrdinal = num;
			return num;
		}

		public void Add(TdsDataColumnCollection columns)
		{
			foreach (TdsDataColumn column in columns)
			{
				Add(column);
			}
		}

		public IEnumerator GetEnumerator()
		{
			return list.GetEnumerator();
		}

		public void Clear()
		{
			list.Clear();
		}
	}
	public class TdsDataRow : IList, ICollection, IEnumerable
	{
		private ArrayList list;

		private int bigDecimalIndex;

		public int BigDecimalIndex
		{
			get
			{
				return bigDecimalIndex;
			}
			set
			{
				bigDecimalIndex = value;
			}
		}

		public int Count => list.Count;

		public bool IsFixedSize => false;

		public bool IsReadOnly => false;

		public bool IsSynchronized => list.IsSynchronized;

		public object SyncRoot => list.SyncRoot;

		public object this[int index]
		{
			get
			{
				if (index >= list.Count)
				{
					throw new IndexOutOfRangeException();
				}
				return list[index];
			}
			set
			{
				list[index] = value;
			}
		}

		public TdsDataRow()
		{
			list = new ArrayList();
			bigDecimalIndex = -1;
		}

		public int Add(object value)
		{
			return list.Add(value);
		}

		public void Clear()
		{
			list.Clear();
		}

		public bool Contains(object value)
		{
			return list.Contains(value);
		}

		public void CopyTo(Array array, int index)
		{
			list.CopyTo(array, index);
		}

		public void CopyTo(int index, Array array, int arrayIndex, int count)
		{
			list.CopyTo(index, array, arrayIndex, count);
		}

		public IEnumerator GetEnumerator()
		{
			return list.GetEnumerator();
		}

		public int IndexOf(object value)
		{
			return list.IndexOf(value);
		}

		public void Insert(int index, object value)
		{
			list.Insert(index, value);
		}

		public void Remove(object value)
		{
			list.Remove(value);
		}

		public void RemoveAt(int index)
		{
			list.RemoveAt(index);
		}
	}
	public enum TdsEnvPacketSubType
	{
		Database = 1,
		CharSet = 3,
		BlockSize = 4,
		Locale = 5,
		CollationInfo = 7
	}
	public sealed class TdsInternalError
	{
		private byte theClass;

		private int lineNumber;

		private string message;

		private int number;

		private string procedure;

		private string server;

		private string source;

		private byte state;

		public byte Class
		{
			get
			{
				return theClass;
			}
			set
			{
				theClass = value;
			}
		}

		public int LineNumber
		{
			get
			{
				return lineNumber;
			}
			set
			{
				lineNumber = value;
			}
		}

		public string Message
		{
			get
			{
				return message;
			}
			set
			{
				message = value;
			}
		}

		public int Number
		{
			get
			{
				return number;
			}
			set
			{
				number = value;
			}
		}

		public string Procedure
		{
			get
			{
				return procedure;
			}
			set
			{
				procedure = value;
			}
		}

		public string Server
		{
			get
			{
				return server;
			}
			set
			{
				server = value;
			}
		}

		public string Source
		{
			get
			{
				return source;
			}
			set
			{
				source = value;
			}
		}

		public byte State
		{
			get
			{
				return state;
			}
			set
			{
				state = value;
			}
		}

		public TdsInternalError(byte theClass, int lineNumber, string message, int number, string procedure, string server, string source, byte state)
		{
			this.theClass = theClass;
			this.lineNumber = lineNumber;
			this.message = message;
			this.number = number;
			this.procedure = procedure;
			this.server = server;
			this.source = source;
			this.state = state;
		}
	}
	public sealed class TdsInternalErrorCollection : IEnumerable
	{
		private ArrayList list;

		public int Count => list.Count;

		public TdsInternalError this[int index]
		{
			get
			{
				return (TdsInternalError)list[index];
			}
			set
			{
				list[index] = value;
			}
		}

		public TdsInternalErrorCollection()
		{
			list = new ArrayList();
		}

		public int Add(TdsInternalError error)
		{
			return list.Add(error);
		}

		public void Clear()
		{
			list.Clear();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return list.GetEnumerator();
		}
	}
	public sealed class TdsInternalErrorMessageEventArgs : TdsInternalInfoMessageEventArgs
	{
		public TdsInternalErrorMessageEventArgs(TdsInternalError error)
			: base(error)
		{
		}
	}
	public delegate void TdsInternalErrorMessageEventHandler(object sender, TdsInternalErrorMessageEventArgs e);
	public class TdsInternalException : SystemException
	{
		private byte theClass;

		private int lineNumber;

		private int number;

		private string procedure;

		private string server;

		private string source;

		private byte state;

		public byte Class => theClass;

		public int LineNumber => lineNumber;

		public override string Message => base.Message;

		public int Number => number;

		public string Procedure => procedure;

		public string Server => server;

		public override string Source => source;

		public byte State => state;

		internal TdsInternalException()
			: base("a TDS Exception has occurred.")
		{
		}

		internal TdsInternalException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		internal TdsInternalException(byte theClass, int lineNumber, string message, int number, string procedure, string server, string source, byte state)
			: base(message)
		{
			this.theClass = theClass;
			this.lineNumber = lineNumber;
			this.number = number;
			this.procedure = procedure;
			this.server = server;
			this.source = source;
			this.state = state;
		}

		[System.MonoTODO]
		public override void GetObjectData(SerializationInfo si, StreamingContext context)
		{
			throw new NotImplementedException();
		}
	}
	public class TdsInternalInfoMessageEventArgs : EventArgs
	{
		private TdsInternalErrorCollection errors;

		public TdsInternalErrorCollection Errors => errors;

		public byte Class => errors[0].Class;

		public int LineNumber => errors[0].LineNumber;

		public string Message => errors[0].Message;

		public int Number => errors[0].Number;

		public string Procedure => errors[0].Procedure;

		public string Server => errors[0].Server;

		public string Source => errors[0].Source;

		public byte State => errors[0].State;

		public TdsInternalInfoMessageEventArgs(TdsInternalErrorCollection errors)
		{
			this.errors = errors;
		}

		public TdsInternalInfoMessageEventArgs(TdsInternalError error)
		{
			errors = new TdsInternalErrorCollection();
			errors.Add(error);
		}

		public int Add(byte theClass, int lineNumber, string message, int number, string procedure, string server, string source, byte state)
		{
			return errors.Add(new TdsInternalError(theClass, lineNumber, message, number, procedure, server, source, state));
		}
	}
	public delegate void TdsInternalInfoMessageEventHandler(object sender, TdsInternalInfoMessageEventArgs e);
	public enum TdsPacketSubType
	{
		Capability = 226,
		Dynamic = 231,
		Dynamic2 = 163,
		EnvironmentChange = 227,
		Error = 170,
		Info = 171,
		EED = 229,
		Param = 172,
		Authentication = 237,
		LoginAck = 173,
		ReturnStatus = 121,
		ProcId = 124,
		Done = 253,
		DoneProc = 254,
		DoneInProc = 255,
		ColumnName = 160,
		ColumnInfo = 161,
		ColumnDetail = 165,
		AltName = 167,
		AltFormat = 168,
		TableName = 164,
		ColumnOrder = 169,
		Control = 174,
		Row = 209,
		ColumnMetadata = 129,
		RowFormat = 238,
		ParamFormat = 236,
		Parameters = 215
	}
	public enum TdsPacketType
	{
		None = 0,
		Query = 1,
		Logon = 2,
		Proc = 3,
		Reply = 4,
		Cancel = 6,
		Bulk = 7,
		Logon70 = 16,
		SspAuth = 17,
		Logoff = 113,
		Normal = 15,
		DBRPC = 230,
		RPC = 3
	}
	public class TdsTimeoutException : TdsInternalException
	{
		internal TdsTimeoutException(byte theClass, int lineNumber, string message, int number, string procedure, string server, string source, byte state)
			: base(theClass, lineNumber, message, number, procedure, server, source, state)
		{
		}
	}
	public enum TdsVersion
	{
		tds42 = 42,
		tds50 = 50,
		tds70 = 70,
		tds80 = 80,
		tds81 = 81,
		tds90 = 90,
		tds100 = 100
	}
}
namespace System
{
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoTODOAttribute : Attribute
	{
		private string comment;

		public string Comment => comment;

		public MonoTODOAttribute()
		{
		}

		public MonoTODOAttribute(string comment)
		{
			this.comment = comment;
		}
	}
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoDocumentationNoteAttribute : System.MonoTODOAttribute
	{
		public MonoDocumentationNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoExtensionAttribute : System.MonoTODOAttribute
	{
		public MonoExtensionAttribute(string comment)
			: base(comment)
		{
		}
	}
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : System.MonoTODOAttribute
	{
		public MonoInternalNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : System.MonoTODOAttribute
	{
		public MonoLimitationAttribute(string comment)
			: base(comment)
		{
		}
	}
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : System.MonoTODOAttribute
	{
		public MonoNotSupportedAttribute(string comment)
			: base(comment)
		{
		}
	}
}
namespace Mono.Data.Tds.Protocol
{
	internal class TdsAsyncState
	{
		private object _userState;

		private bool _wantResults;

		public object UserState
		{
			get
			{
				return _userState;
			}
			set
			{
				_userState = value;
			}
		}

		public bool WantResults
		{
			get
			{
				return _wantResults;
			}
			set
			{
				_wantResults = value;
			}
		}

		public TdsAsyncState(object userState)
		{
			_userState = userState;
		}
	}
	internal class TdsAsyncResult : IAsyncResult
	{
		private TdsAsyncState _tdsState;

		private WaitHandle _waitHandle;

		private bool _completed;

		private bool _completedSyncly;

		private AsyncCallback _userCallback;

		private object _retValue;

		private Exception _exception;

		public object AsyncState => _tdsState.UserState;

		internal TdsAsyncState TdsAsyncState => _tdsState;

		public WaitHandle AsyncWaitHandle => _waitHandle;

		public bool IsCompleted => _completed;

		public bool IsCompletedWithException => _exception != null;

		public Exception Exception => _exception;

		public bool CompletedSynchronously => _completedSyncly;

		internal object ReturnValue
		{
			get
			{
				return _retValue;
			}
			set
			{
				_retValue = value;
			}
		}

		public TdsAsyncResult(AsyncCallback userCallback, TdsAsyncState tdsState)
		{
			_tdsState = tdsState;
			_userCallback = userCallback;
			_waitHandle = new ManualResetEvent(initialState: false);
		}

		public TdsAsyncResult(AsyncCallback userCallback, object state)
		{
			_tdsState = new TdsAsyncState(state);
			_userCallback = userCallback;
			_waitHandle = new ManualResetEvent(initialState: false);
		}

		internal void MarkComplete()
		{
			_completed = true;
			_exception = null;
			((ManualResetEvent)_waitHandle).Set();
			if (_userCallback != null)
			{
				_userCallback(this);
			}
		}

		internal void MarkComplete(Exception e)
		{
			_completed = true;
			_exception = e;
			((ManualResetEvent)_waitHandle).Set();
			if (_userCallback != null)
			{
				_userCallback(this);
			}
		}
	}
}
