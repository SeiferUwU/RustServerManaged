#define TRACE
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SQLite;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Transactions;
using System.Xml;

[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblySourceTimeStamp(null)]
[assembly: AssemblyTitle("System.Data.SQLite Core")]
[assembly: AssemblyFileVersion("1.0.97.0")]
[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: AssemblySourceId(null)]
[assembly: AssemblyConfiguration("Release")]
[assembly: AssemblyDescription("ADO.NET Data Provider for SQLite")]
[assembly: AssemblyCompany("https://system.data.sqlite.org/")]
[assembly: AssemblyProduct("System.Data.SQLite")]
[assembly: AssemblyCopyright("Public Domain")]
[assembly: InternalsVisibleTo("System.Data.SQLite.Linq, PublicKey=002400000480000094000000060200000024000052534131000400000100010005a288de5687c4e1b621ddff5d844727418956997f475eb829429e411aff3e93f97b70de698b972640925bdd44280df0a25a843266973704137cbb0e7441c1fe7cae4e2440ae91ab8cde3933febcb1ac48dd33b40e13c421d8215c18a4349a436dd499e3c385cc683015f886f6c10bd90115eb2bd61b67750839e3a19941dc9c")]
[assembly: ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
[assembly: CLSCompliant(true)]
[assembly: ComVisible(false)]
[assembly: NeutralResourcesLanguage("en")]
[assembly: AllowPartiallyTrustedCallers]
[assembly: AssemblyVersion("1.0.97.0")]
namespace System.Data.SQLite
{
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblySourceIdAttribute : Attribute
	{
		private string sourceId;

		public string SourceId => sourceId;

		public AssemblySourceIdAttribute(string value)
		{
			sourceId = value;
		}
	}
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblySourceTimeStampAttribute : Attribute
	{
		private string sourceTimeStamp;

		public string SourceTimeStamp => sourceTimeStamp;

		public AssemblySourceTimeStampAttribute(string value)
		{
			sourceTimeStamp = value;
		}
	}
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void SQLiteLogCallback(IntPtr pUserData, int errorCode, IntPtr pMessage);
	public abstract class SQLiteConvert
	{
		private const DbType FallbackDefaultDbType = DbType.Object;

		private const string FullFormat = "yyyy-MM-ddTHH:mm:ss.fffffffK";

		private static readonly string FallbackDefaultTypeName = string.Empty;

		protected static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		private static readonly double OleAutomationEpochAsJulianDay = 2415018.5;

		private static readonly long MinimumJd = computeJD(DateTime.MinValue);

		private static readonly long MaximumJd = computeJD(DateTime.MaxValue);

		private static string[] _datetimeFormats = new string[31]
		{
			"THHmmssK", "THHmmK", "HH:mm:ss.FFFFFFFK", "HH:mm:ssK", "HH:mmK", "yyyy-MM-dd HH:mm:ss.FFFFFFFK", "yyyy-MM-dd HH:mm:ssK", "yyyy-MM-dd HH:mmK", "yyyy-MM-ddTHH:mm:ss.FFFFFFFK", "yyyy-MM-ddTHH:mmK",
			"yyyy-MM-ddTHH:mm:ssK", "yyyyMMddHHmmssK", "yyyyMMddHHmmK", "yyyyMMddTHHmmssFFFFFFFK", "THHmmss", "THHmm", "HH:mm:ss.FFFFFFF", "HH:mm:ss", "HH:mm", "yyyy-MM-dd HH:mm:ss.FFFFFFF",
			"yyyy-MM-dd HH:mm:ss", "yyyy-MM-dd HH:mm", "yyyy-MM-ddTHH:mm:ss.FFFFFFF", "yyyy-MM-ddTHH:mm", "yyyy-MM-ddTHH:mm:ss", "yyyyMMddHHmmss", "yyyyMMddHHmm", "yyyyMMddTHHmmssFFFFFFF", "yyyy-MM-dd", "yyyyMMdd",
			"yy-MM-dd"
		};

		private static readonly string _datetimeFormatUtc = _datetimeFormats[5];

		private static readonly string _datetimeFormatLocal = _datetimeFormats[19];

		private static Encoding _utf8 = new UTF8Encoding();

		internal SQLiteDateFormats _datetimeFormat;

		internal DateTimeKind _datetimeKind;

		internal string _datetimeFormatString;

		private static Type[] _affinitytotype = new Type[8]
		{
			typeof(object),
			typeof(long),
			typeof(double),
			typeof(string),
			typeof(byte[]),
			typeof(object),
			typeof(DateTime),
			typeof(object)
		};

		private static DbType[] _typetodbtype = new DbType[19]
		{
			DbType.Object,
			DbType.Binary,
			DbType.Object,
			DbType.Boolean,
			DbType.SByte,
			DbType.SByte,
			DbType.Byte,
			DbType.Int16,
			DbType.UInt16,
			DbType.Int32,
			DbType.UInt32,
			DbType.Int64,
			DbType.UInt64,
			DbType.Single,
			DbType.Double,
			DbType.Decimal,
			DbType.DateTime,
			DbType.Object,
			DbType.String
		};

		private static int[] _dbtypetocolumnsize = new int[26]
		{
			2147483647, 2147483647, 1, 1, 8, 8, 8, 8, 8, 16,
			2, 4, 8, 2147483647, 1, 4, 2147483647, 8, 2, 4,
			8, 8, 2147483647, 2147483647, 2147483647, 2147483647
		};

		private static object[] _dbtypetonumericprecision = new object[26]
		{
			DBNull.Value,
			DBNull.Value,
			3,
			DBNull.Value,
			19,
			DBNull.Value,
			DBNull.Value,
			53,
			53,
			DBNull.Value,
			5,
			10,
			19,
			DBNull.Value,
			3,
			24,
			DBNull.Value,
			DBNull.Value,
			5,
			10,
			19,
			53,
			DBNull.Value,
			DBNull.Value,
			DBNull.Value,
			DBNull.Value
		};

		private static object[] _dbtypetonumericscale = new object[26]
		{
			DBNull.Value,
			DBNull.Value,
			0,
			DBNull.Value,
			4,
			DBNull.Value,
			DBNull.Value,
			DBNull.Value,
			DBNull.Value,
			DBNull.Value,
			0,
			0,
			0,
			DBNull.Value,
			0,
			DBNull.Value,
			DBNull.Value,
			DBNull.Value,
			0,
			0,
			0,
			0,
			DBNull.Value,
			DBNull.Value,
			DBNull.Value,
			DBNull.Value
		};

		private static Type[] _dbtypeToType = new Type[26]
		{
			typeof(string),
			typeof(byte[]),
			typeof(byte),
			typeof(bool),
			typeof(decimal),
			typeof(DateTime),
			typeof(DateTime),
			typeof(decimal),
			typeof(double),
			typeof(Guid),
			typeof(short),
			typeof(int),
			typeof(long),
			typeof(object),
			typeof(sbyte),
			typeof(float),
			typeof(string),
			typeof(DateTime),
			typeof(ushort),
			typeof(uint),
			typeof(ulong),
			typeof(double),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string)
		};

		private static TypeAffinity[] _typecodeAffinities = new TypeAffinity[19]
		{
			TypeAffinity.Null,
			TypeAffinity.Blob,
			TypeAffinity.Null,
			TypeAffinity.Int64,
			TypeAffinity.Int64,
			TypeAffinity.Int64,
			TypeAffinity.Int64,
			TypeAffinity.Int64,
			TypeAffinity.Int64,
			TypeAffinity.Int64,
			TypeAffinity.Int64,
			TypeAffinity.Int64,
			TypeAffinity.Int64,
			TypeAffinity.Double,
			TypeAffinity.Double,
			TypeAffinity.Double,
			TypeAffinity.DateTime,
			TypeAffinity.Null,
			TypeAffinity.Text
		};

		private static object _syncRoot = new object();

		private static SQLiteDbTypeMap _typeNames = null;

		internal SQLiteConvert(SQLiteDateFormats fmt, DateTimeKind kind, string fmtString)
		{
			_datetimeFormat = fmt;
			_datetimeKind = kind;
			_datetimeFormatString = fmtString;
		}

		public static byte[] ToUTF8(string sourceText)
		{
			int num = _utf8.GetByteCount(sourceText) + 1;
			byte[] array = new byte[num];
			num = _utf8.GetBytes(sourceText, 0, sourceText.Length, array, 0);
			array[num] = 0;
			return array;
		}

		public byte[] ToUTF8(DateTime dateTimeValue)
		{
			return ToUTF8(ToString(dateTimeValue));
		}

		public virtual string ToString(IntPtr nativestring, int nativestringlen)
		{
			return UTF8ToString(nativestring, nativestringlen);
		}

		public static string UTF8ToString(IntPtr nativestring, int nativestringlen)
		{
			if (nativestring == IntPtr.Zero || nativestringlen == 0)
			{
				return string.Empty;
			}
			if (nativestringlen < 0)
			{
				nativestringlen = 0;
				while (Marshal.ReadByte(nativestring, nativestringlen) != 0)
				{
					nativestringlen++;
				}
				if (nativestringlen == 0)
				{
					return string.Empty;
				}
			}
			byte[] array = new byte[nativestringlen];
			Marshal.Copy(nativestring, array, 0, nativestringlen);
			return _utf8.GetString(array, 0, nativestringlen);
		}

		private static bool isValidJd(long jd)
		{
			if (jd >= MinimumJd)
			{
				return jd <= MaximumJd;
			}
			return false;
		}

		private static long DoubleToJd(double julianDay)
		{
			return (long)(julianDay * 86400000.0);
		}

		private static double JdToDouble(long jd)
		{
			return (double)jd / 86400000.0;
		}

		private static DateTime computeYMD(long jd, DateTime? badValue)
		{
			if (!isValidJd(jd))
			{
				if (!badValue.HasValue)
				{
					throw new ArgumentException("Not a supported Julian Day value.");
				}
				return badValue.Value;
			}
			int num = (int)((jd + 43200000) / 86400000);
			int num2 = (int)(((double)num - 1867216.25) / 36524.25);
			num2 = num + 1 + num2 - num2 / 4;
			int num3 = num2 + 1524;
			int num4 = (int)(((double)num3 - 122.1) / 365.25);
			int num5 = 36525 * num4 / 100;
			int num6 = (int)((double)(num3 - num5) / 30.6001);
			int num7 = (int)(30.6001 * (double)num6);
			int day = num3 - num5 - num7;
			int num8 = ((num6 < 14) ? (num6 - 1) : (num6 - 13));
			int year = ((num8 > 2) ? (num4 - 4716) : (num4 - 4715));
			try
			{
				return new DateTime(year, num8, day);
			}
			catch
			{
				if (!badValue.HasValue)
				{
					throw;
				}
				return badValue.Value;
			}
		}

		private static DateTime computeHMS(long jd, DateTime? badValue)
		{
			if (!isValidJd(jd))
			{
				if (!badValue.HasValue)
				{
					throw new ArgumentException("Not a supported Julian Day value.");
				}
				return badValue.Value;
			}
			int num = (int)((jd + 43200000) % 86400000);
			decimal num2 = (decimal)num / 1000.0m;
			num = (int)num2;
			int millisecond = (int)((num2 - (decimal)num) * 1000.0m);
			num2 -= (decimal)num;
			int num3 = num / 3600;
			num -= num3 * 3600;
			int num4 = num / 60;
			num2 += (decimal)(num - num4 * 60);
			int second = (int)num2;
			try
			{
				DateTime minValue = DateTime.MinValue;
				return new DateTime(minValue.Year, minValue.Month, minValue.Day, num3, num4, second, millisecond);
			}
			catch
			{
				if (!badValue.HasValue)
				{
					throw;
				}
				return badValue.Value;
			}
		}

		private static long computeJD(DateTime dateTime)
		{
			int num = dateTime.Year;
			int num2 = dateTime.Month;
			int day = dateTime.Day;
			if (num2 <= 2)
			{
				num--;
				num2 += 12;
			}
			int num3 = num / 100;
			int num4 = 2 - num3 + num3 / 4;
			int num5 = 36525 * (num + 4716) / 100;
			int num6 = 306001 * (num2 + 1) / 10000;
			long num7 = (long)(((double)(num5 + num6 + day + num4) - 1524.5) * 86400000.0);
			return num7 + (dateTime.Hour * 3600000 + dateTime.Minute * 60000 + dateTime.Second * 1000 + dateTime.Millisecond);
		}

		public DateTime ToDateTime(string dateText)
		{
			return ToDateTime(dateText, _datetimeFormat, _datetimeKind, _datetimeFormatString);
		}

		public static DateTime ToDateTime(string dateText, SQLiteDateFormats format, DateTimeKind kind, string formatString)
		{
			switch (format)
			{
			case SQLiteDateFormats.Ticks:
				return ToDateTime(Convert.ToInt64(dateText, CultureInfo.InvariantCulture), kind);
			case SQLiteDateFormats.JulianDay:
				return ToDateTime(Convert.ToDouble(dateText, CultureInfo.InvariantCulture), kind);
			case SQLiteDateFormats.UnixEpoch:
				return ToDateTime(Convert.ToInt32(dateText, CultureInfo.InvariantCulture), kind);
			case SQLiteDateFormats.InvariantCulture:
				if (formatString != null)
				{
					return DateTime.SpecifyKind(DateTime.ParseExact(dateText, formatString, DateTimeFormatInfo.InvariantInfo, (kind == DateTimeKind.Utc) ? DateTimeStyles.AdjustToUniversal : DateTimeStyles.None), kind);
				}
				return DateTime.SpecifyKind(DateTime.Parse(dateText, DateTimeFormatInfo.InvariantInfo, (kind == DateTimeKind.Utc) ? DateTimeStyles.AdjustToUniversal : DateTimeStyles.None), kind);
			case SQLiteDateFormats.CurrentCulture:
				if (formatString != null)
				{
					return DateTime.SpecifyKind(DateTime.ParseExact(dateText, formatString, DateTimeFormatInfo.CurrentInfo, (kind == DateTimeKind.Utc) ? DateTimeStyles.AdjustToUniversal : DateTimeStyles.None), kind);
				}
				return DateTime.SpecifyKind(DateTime.Parse(dateText, DateTimeFormatInfo.CurrentInfo, (kind == DateTimeKind.Utc) ? DateTimeStyles.AdjustToUniversal : DateTimeStyles.None), kind);
			default:
				if (formatString != null)
				{
					return DateTime.SpecifyKind(DateTime.ParseExact(dateText, formatString, DateTimeFormatInfo.InvariantInfo, (kind == DateTimeKind.Utc) ? DateTimeStyles.AdjustToUniversal : DateTimeStyles.None), kind);
				}
				return DateTime.SpecifyKind(DateTime.ParseExact(dateText, _datetimeFormats, DateTimeFormatInfo.InvariantInfo, (kind == DateTimeKind.Utc) ? DateTimeStyles.AdjustToUniversal : DateTimeStyles.None), kind);
			}
		}

		public DateTime ToDateTime(double julianDay)
		{
			return ToDateTime(julianDay, _datetimeKind);
		}

		public static DateTime ToDateTime(double julianDay, DateTimeKind kind)
		{
			long jd = DoubleToJd(julianDay);
			DateTime dateTime = computeYMD(jd, null);
			DateTime dateTime2 = computeHMS(jd, null);
			return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime2.Hour, dateTime2.Minute, dateTime2.Second, dateTime2.Millisecond, kind);
		}

		internal static DateTime ToDateTime(int seconds, DateTimeKind kind)
		{
			DateTime unixEpoch = UnixEpoch;
			return DateTime.SpecifyKind(unixEpoch.AddSeconds(seconds), kind);
		}

		internal static DateTime ToDateTime(long ticks, DateTimeKind kind)
		{
			return new DateTime(ticks, kind);
		}

		public static double ToJulianDay(DateTime value)
		{
			return JdToDouble(computeJD(value));
		}

		public static long ToUnixEpoch(DateTime value)
		{
			return value.Subtract(UnixEpoch).Ticks / 10000000;
		}

		private static string GetDateTimeKindFormat(DateTimeKind kind, string formatString)
		{
			if (formatString != null)
			{
				return formatString;
			}
			if (kind != DateTimeKind.Utc)
			{
				return _datetimeFormatLocal;
			}
			return _datetimeFormatUtc;
		}

		public string ToString(DateTime dateValue)
		{
			return ToString(dateValue, _datetimeFormat, _datetimeKind, _datetimeFormatString);
		}

		public static string ToString(DateTime dateValue, SQLiteDateFormats format, DateTimeKind kind, string formatString)
		{
			switch (format)
			{
			case SQLiteDateFormats.Ticks:
				return dateValue.Ticks.ToString(CultureInfo.InvariantCulture);
			case SQLiteDateFormats.JulianDay:
				return ToJulianDay(dateValue).ToString(CultureInfo.InvariantCulture);
			case SQLiteDateFormats.UnixEpoch:
				return (dateValue.Subtract(UnixEpoch).Ticks / 10000000).ToString();
			case SQLiteDateFormats.InvariantCulture:
				return dateValue.ToString((formatString != null) ? formatString : "yyyy-MM-ddTHH:mm:ss.fffffffK", CultureInfo.InvariantCulture);
			case SQLiteDateFormats.CurrentCulture:
				return dateValue.ToString((formatString != null) ? formatString : "yyyy-MM-ddTHH:mm:ss.fffffffK", CultureInfo.CurrentCulture);
			default:
				if (dateValue.Kind != DateTimeKind.Unspecified)
				{
					return dateValue.ToString(GetDateTimeKindFormat(dateValue.Kind, formatString), CultureInfo.InvariantCulture);
				}
				return DateTime.SpecifyKind(dateValue, kind).ToString(GetDateTimeKindFormat(kind, formatString), CultureInfo.InvariantCulture);
			}
		}

		internal DateTime ToDateTime(IntPtr ptr, int len)
		{
			return ToDateTime(ToString(ptr, len));
		}

		public static string[] Split(string source, char separator)
		{
			char[] array = new char[2] { '"', separator };
			char[] array2 = new char[1] { '"' };
			int startIndex = 0;
			List<string> list = new List<string>();
			while (source.Length > 0)
			{
				startIndex = source.IndexOfAny(array, startIndex);
				if (startIndex == -1)
				{
					break;
				}
				if (source[startIndex] == array[0])
				{
					startIndex = source.IndexOfAny(array2, startIndex + 1);
					if (startIndex == -1)
					{
						break;
					}
					startIndex++;
					continue;
				}
				string text = source.Substring(0, startIndex).Trim();
				if (text.Length > 1 && text[0] == array2[0] && text[text.Length - 1] == text[0])
				{
					text = text.Substring(1, text.Length - 2);
				}
				source = source.Substring(startIndex + 1).Trim();
				if (text.Length > 0)
				{
					list.Add(text);
				}
				startIndex = 0;
			}
			if (source.Length > 0)
			{
				string text = source.Trim();
				if (text.Length > 1 && text[0] == array2[0] && text[text.Length - 1] == text[0])
				{
					text = text.Substring(1, text.Length - 2);
				}
				list.Add(text);
			}
			string[] array3 = new string[list.Count];
			list.CopyTo(array3, 0);
			return array3;
		}

		internal static string[] NewSplit(string value, char separator, bool keepQuote, ref string error)
		{
			if (separator == '\\' || separator == '"')
			{
				error = "separator character cannot be the escape or quote characters";
				return null;
			}
			if (value == null)
			{
				error = "string value to split cannot be null";
				return null;
			}
			int length = value.Length;
			if (length == 0)
			{
				return new string[0];
			}
			List<string> list = new List<string>();
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			bool flag = false;
			bool flag2 = false;
			while (num < length)
			{
				char c = value[num++];
				if (flag)
				{
					if (c != '\\' && c != '"' && c != separator)
					{
						stringBuilder.Append('\\');
					}
					stringBuilder.Append(c);
					flag = false;
					continue;
				}
				switch (c)
				{
				case '\\':
					flag = true;
					continue;
				case '"':
					if (keepQuote)
					{
						stringBuilder.Append(c);
					}
					flag2 = !flag2;
					continue;
				}
				if (c == separator)
				{
					if (flag2)
					{
						stringBuilder.Append(c);
						continue;
					}
					list.Add(stringBuilder.ToString());
					stringBuilder.Length = 0;
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			if (flag || flag2)
			{
				error = "unbalanced escape or quote character found";
				return null;
			}
			if (stringBuilder.Length > 0)
			{
				list.Add(stringBuilder.ToString());
			}
			return list.ToArray();
		}

		public static string ToStringWithProvider(object obj, IFormatProvider provider)
		{
			if (obj == null)
			{
				return null;
			}
			if (obj is string)
			{
				return (string)obj;
			}
			if (obj is IConvertible convertible)
			{
				return convertible.ToString(provider);
			}
			return obj.ToString();
		}

		internal static bool ToBoolean(object obj, IFormatProvider provider, bool viaFramework)
		{
			if (obj == null)
			{
				return false;
			}
			TypeCode typeCode = Type.GetTypeCode(obj.GetType());
			switch (typeCode)
			{
			case TypeCode.Empty:
			case TypeCode.DBNull:
				return false;
			case TypeCode.Boolean:
				return (bool)obj;
			case TypeCode.Char:
				if ((char)obj == '\0')
				{
					return false;
				}
				return true;
			case TypeCode.SByte:
				if ((sbyte)obj == 0)
				{
					return false;
				}
				return true;
			case TypeCode.Byte:
				if ((byte)obj == 0)
				{
					return false;
				}
				return true;
			case TypeCode.Int16:
				if ((short)obj == 0)
				{
					return false;
				}
				return true;
			case TypeCode.UInt16:
				if ((ushort)obj == 0)
				{
					return false;
				}
				return true;
			case TypeCode.Int32:
				if ((int)obj == 0)
				{
					return false;
				}
				return true;
			case TypeCode.UInt32:
				if ((uint)obj == 0)
				{
					return false;
				}
				return true;
			case TypeCode.Int64:
				if ((long)obj == 0)
				{
					return false;
				}
				return true;
			case TypeCode.UInt64:
				if ((ulong)obj == 0)
				{
					return false;
				}
				return true;
			case TypeCode.Single:
				if ((float)obj == 0f)
				{
					return false;
				}
				return true;
			case TypeCode.Double:
				if ((double)obj == 0.0)
				{
					return false;
				}
				return true;
			case TypeCode.Decimal:
				if (!((decimal)obj != 0m))
				{
					return false;
				}
				return true;
			case TypeCode.String:
				if (!viaFramework)
				{
					return ToBoolean(ToStringWithProvider(obj, provider));
				}
				return Convert.ToBoolean(obj, provider);
			default:
				throw new SQLiteException(string.Format(CultureInfo.CurrentCulture, "Cannot convert type {0} to boolean", new object[1] { typeCode }));
			}
		}

		public static bool ToBoolean(object source)
		{
			if (source is bool)
			{
				return (bool)source;
			}
			return ToBoolean(ToStringWithProvider(source, CultureInfo.InvariantCulture));
		}

		public static bool ToBoolean(string source)
		{
			if (string.Compare(source, bool.TrueString, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			if (string.Compare(source, bool.FalseString, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return false;
			}
			switch (source.ToLower(CultureInfo.InvariantCulture))
			{
			case "yes":
			case "y":
			case "1":
			case "on":
				return true;
			case "no":
			case "n":
			case "0":
			case "off":
				return false;
			default:
				throw new ArgumentException("source");
			}
		}

		internal static Type SQLiteTypeToType(SQLiteType t)
		{
			if (t.Type == DbType.Object)
			{
				return _affinitytotype[(int)t.Affinity];
			}
			return DbTypeToType(t.Type);
		}

		internal static DbType TypeToDbType(Type typ)
		{
			TypeCode typeCode = Type.GetTypeCode(typ);
			if (typeCode == TypeCode.Object)
			{
				if ((object)typ == typeof(byte[]))
				{
					return DbType.Binary;
				}
				if ((object)typ == typeof(Guid))
				{
					return DbType.Guid;
				}
				return DbType.String;
			}
			return _typetodbtype[(int)typeCode];
		}

		internal static int DbTypeToColumnSize(DbType typ)
		{
			return _dbtypetocolumnsize[(int)typ];
		}

		internal static object DbTypeToNumericPrecision(DbType typ)
		{
			return _dbtypetonumericprecision[(int)typ];
		}

		internal static object DbTypeToNumericScale(DbType typ)
		{
			return _dbtypetonumericscale[(int)typ];
		}

		private static string GetDefaultTypeName(SQLiteConnection connection)
		{
			SQLiteConnectionFlags sQLiteConnectionFlags = connection?.Flags ?? SQLiteConnectionFlags.None;
			if ((sQLiteConnectionFlags & SQLiteConnectionFlags.NoConvertSettings) == SQLiteConnectionFlags.NoConvertSettings)
			{
				return FallbackDefaultTypeName;
			}
			string name = "Use_SQLiteConvert_DefaultTypeName";
			object value = null;
			string text = null;
			if (connection == null || !connection.TryGetCachedSetting(name, text, out value))
			{
				try
				{
					value = UnsafeNativeMethods.GetSettingValue(name, text);
					if (value == null)
					{
						value = FallbackDefaultTypeName;
					}
				}
				finally
				{
					connection?.SetCachedSetting(name, value);
				}
			}
			return SettingValueToString(value);
		}

		private static void DefaultTypeNameWarning(DbType dbType, SQLiteConnectionFlags flags, string typeName)
		{
			if ((flags & SQLiteConnectionFlags.TraceWarning) == SQLiteConnectionFlags.TraceWarning)
			{
				Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "WARNING: Type mapping failed, returning default name \"{0}\" for type {1}.", new object[2] { typeName, dbType }));
			}
		}

		private static void DefaultDbTypeWarning(string typeName, SQLiteConnectionFlags flags, DbType? dbType)
		{
			if (!string.IsNullOrEmpty(typeName) && (flags & SQLiteConnectionFlags.TraceWarning) == SQLiteConnectionFlags.TraceWarning)
			{
				Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "WARNING: Type mapping failed, returning default type {0} for name \"{1}\".", new object[2] { dbType, typeName }));
			}
		}

		internal static string DbTypeToTypeName(SQLiteConnection connection, DbType dbType, SQLiteConnectionFlags flags)
		{
			string text = null;
			if (connection != null)
			{
				flags |= connection.Flags;
				if ((flags & SQLiteConnectionFlags.UseConnectionTypes) == SQLiteConnectionFlags.UseConnectionTypes)
				{
					SQLiteDbTypeMap typeNames = connection._typeNames;
					if (typeNames != null && typeNames.TryGetValue(dbType, out var value))
					{
						return value.typeName;
					}
				}
				text = connection.DefaultTypeName;
			}
			if ((flags & SQLiteConnectionFlags.NoGlobalTypes) == SQLiteConnectionFlags.NoGlobalTypes)
			{
				if (text != null)
				{
					return text;
				}
				text = GetDefaultTypeName(connection);
				DefaultTypeNameWarning(dbType, flags, text);
				return text;
			}
			lock (_syncRoot)
			{
				if (_typeNames == null)
				{
					_typeNames = GetSQLiteDbTypeMap();
				}
				if (_typeNames.TryGetValue(dbType, out var value2))
				{
					return value2.typeName;
				}
			}
			if (text != null)
			{
				return text;
			}
			text = GetDefaultTypeName(connection);
			DefaultTypeNameWarning(dbType, flags, text);
			return text;
		}

		internal static Type DbTypeToType(DbType typ)
		{
			return _dbtypeToType[(int)typ];
		}

		internal static TypeAffinity TypeToAffinity(Type typ)
		{
			TypeCode typeCode = Type.GetTypeCode(typ);
			if (typeCode == TypeCode.Object)
			{
				if ((object)typ == typeof(byte[]) || (object)typ == typeof(Guid))
				{
					return TypeAffinity.Blob;
				}
				return TypeAffinity.Text;
			}
			return _typecodeAffinities[(int)typeCode];
		}

		private static SQLiteDbTypeMap GetSQLiteDbTypeMap()
		{
			return new SQLiteDbTypeMap(new SQLiteDbTypeMapping[72]
			{
				new SQLiteDbTypeMapping("BIGINT", DbType.Int64, newPrimary: false),
				new SQLiteDbTypeMapping("BIGUINT", DbType.UInt64, newPrimary: false),
				new SQLiteDbTypeMapping("BINARY", DbType.Binary, newPrimary: false),
				new SQLiteDbTypeMapping("BIT", DbType.Boolean, newPrimary: true),
				new SQLiteDbTypeMapping("BLOB", DbType.Binary, newPrimary: true),
				new SQLiteDbTypeMapping("BOOL", DbType.Boolean, newPrimary: false),
				new SQLiteDbTypeMapping("BOOLEAN", DbType.Boolean, newPrimary: false),
				new SQLiteDbTypeMapping("CHAR", DbType.AnsiStringFixedLength, newPrimary: true),
				new SQLiteDbTypeMapping("CLOB", DbType.String, newPrimary: false),
				new SQLiteDbTypeMapping("COUNTER", DbType.Int64, newPrimary: false),
				new SQLiteDbTypeMapping("CURRENCY", DbType.Decimal, newPrimary: false),
				new SQLiteDbTypeMapping("DATE", DbType.DateTime, newPrimary: false),
				new SQLiteDbTypeMapping("DATETIME", DbType.DateTime, newPrimary: true),
				new SQLiteDbTypeMapping("DECIMAL", DbType.Decimal, newPrimary: true),
				new SQLiteDbTypeMapping("DOUBLE", DbType.Double, newPrimary: false),
				new SQLiteDbTypeMapping("FLOAT", DbType.Double, newPrimary: false),
				new SQLiteDbTypeMapping("GENERAL", DbType.Binary, newPrimary: false),
				new SQLiteDbTypeMapping("GUID", DbType.Guid, newPrimary: false),
				new SQLiteDbTypeMapping("IDENTITY", DbType.Int64, newPrimary: false),
				new SQLiteDbTypeMapping("IMAGE", DbType.Binary, newPrimary: false),
				new SQLiteDbTypeMapping("INT", DbType.Int32, newPrimary: true),
				new SQLiteDbTypeMapping("INT8", DbType.SByte, newPrimary: false),
				new SQLiteDbTypeMapping("INT16", DbType.Int16, newPrimary: false),
				new SQLiteDbTypeMapping("INT32", DbType.Int32, newPrimary: false),
				new SQLiteDbTypeMapping("INT64", DbType.Int64, newPrimary: false),
				new SQLiteDbTypeMapping("INTEGER", DbType.Int64, newPrimary: true),
				new SQLiteDbTypeMapping("INTEGER8", DbType.SByte, newPrimary: false),
				new SQLiteDbTypeMapping("INTEGER16", DbType.Int16, newPrimary: false),
				new SQLiteDbTypeMapping("INTEGER32", DbType.Int32, newPrimary: false),
				new SQLiteDbTypeMapping("INTEGER64", DbType.Int64, newPrimary: false),
				new SQLiteDbTypeMapping("LOGICAL", DbType.Boolean, newPrimary: false),
				new SQLiteDbTypeMapping("LONG", DbType.Int64, newPrimary: false),
				new SQLiteDbTypeMapping("LONGCHAR", DbType.String, newPrimary: false),
				new SQLiteDbTypeMapping("LONGTEXT", DbType.String, newPrimary: false),
				new SQLiteDbTypeMapping("LONGVARCHAR", DbType.String, newPrimary: false),
				new SQLiteDbTypeMapping("MEMO", DbType.String, newPrimary: false),
				new SQLiteDbTypeMapping("MONEY", DbType.Decimal, newPrimary: false),
				new SQLiteDbTypeMapping("NCHAR", DbType.StringFixedLength, newPrimary: true),
				new SQLiteDbTypeMapping("NOTE", DbType.String, newPrimary: false),
				new SQLiteDbTypeMapping("NTEXT", DbType.String, newPrimary: false),
				new SQLiteDbTypeMapping("NUMBER", DbType.Decimal, newPrimary: false),
				new SQLiteDbTypeMapping("NUMERIC", DbType.Decimal, newPrimary: false),
				new SQLiteDbTypeMapping("NVARCHAR", DbType.String, newPrimary: true),
				new SQLiteDbTypeMapping("OLEOBJECT", DbType.Binary, newPrimary: false),
				new SQLiteDbTypeMapping("RAW", DbType.Binary, newPrimary: false),
				new SQLiteDbTypeMapping("REAL", DbType.Double, newPrimary: true),
				new SQLiteDbTypeMapping("SINGLE", DbType.Single, newPrimary: true),
				new SQLiteDbTypeMapping("SMALLDATE", DbType.DateTime, newPrimary: false),
				new SQLiteDbTypeMapping("SMALLINT", DbType.Int16, newPrimary: true),
				new SQLiteDbTypeMapping("SMALLUINT", DbType.UInt16, newPrimary: true),
				new SQLiteDbTypeMapping("STRING", DbType.String, newPrimary: false),
				new SQLiteDbTypeMapping("TEXT", DbType.String, newPrimary: false),
				new SQLiteDbTypeMapping("TIME", DbType.DateTime, newPrimary: false),
				new SQLiteDbTypeMapping("TIMESTAMP", DbType.DateTime, newPrimary: false),
				new SQLiteDbTypeMapping("TINYINT", DbType.Byte, newPrimary: true),
				new SQLiteDbTypeMapping("TINYSINT", DbType.SByte, newPrimary: true),
				new SQLiteDbTypeMapping("UINT", DbType.UInt32, newPrimary: true),
				new SQLiteDbTypeMapping("UINT8", DbType.Byte, newPrimary: false),
				new SQLiteDbTypeMapping("UINT16", DbType.UInt16, newPrimary: false),
				new SQLiteDbTypeMapping("UINT32", DbType.UInt32, newPrimary: false),
				new SQLiteDbTypeMapping("UINT64", DbType.UInt64, newPrimary: false),
				new SQLiteDbTypeMapping("ULONG", DbType.UInt64, newPrimary: false),
				new SQLiteDbTypeMapping("UNIQUEIDENTIFIER", DbType.Guid, newPrimary: true),
				new SQLiteDbTypeMapping("UNSIGNEDINTEGER", DbType.UInt64, newPrimary: true),
				new SQLiteDbTypeMapping("UNSIGNEDINTEGER8", DbType.Byte, newPrimary: false),
				new SQLiteDbTypeMapping("UNSIGNEDINTEGER16", DbType.UInt16, newPrimary: false),
				new SQLiteDbTypeMapping("UNSIGNEDINTEGER32", DbType.UInt32, newPrimary: false),
				new SQLiteDbTypeMapping("UNSIGNEDINTEGER64", DbType.UInt64, newPrimary: false),
				new SQLiteDbTypeMapping("VARBINARY", DbType.Binary, newPrimary: false),
				new SQLiteDbTypeMapping("VARCHAR", DbType.AnsiString, newPrimary: true),
				new SQLiteDbTypeMapping("VARCHAR2", DbType.AnsiString, newPrimary: false),
				new SQLiteDbTypeMapping("YESNO", DbType.Boolean, newPrimary: false)
			});
		}

		internal static bool IsStringDbType(DbType type)
		{
			switch (type)
			{
			case DbType.AnsiString:
			case DbType.String:
			case DbType.AnsiStringFixedLength:
			case DbType.StringFixedLength:
				return true;
			default:
				return false;
			}
		}

		private static string SettingValueToString(object value)
		{
			if (value is string)
			{
				return (string)value;
			}
			return value?.ToString();
		}

		private static DbType GetDefaultDbType(SQLiteConnection connection)
		{
			SQLiteConnectionFlags sQLiteConnectionFlags = connection?.Flags ?? SQLiteConnectionFlags.None;
			if ((sQLiteConnectionFlags & SQLiteConnectionFlags.NoConvertSettings) == SQLiteConnectionFlags.NoConvertSettings)
			{
				return DbType.Object;
			}
			bool flag = false;
			string name = "Use_SQLiteConvert_DefaultDbType";
			object value = null;
			string text = null;
			if (connection == null || !connection.TryGetCachedSetting(name, text, out value))
			{
				value = UnsafeNativeMethods.GetSettingValue(name, text);
				if (value == null)
				{
					value = DbType.Object;
				}
			}
			else
			{
				flag = true;
			}
			try
			{
				if (!(value is DbType))
				{
					value = SQLiteConnection.TryParseEnum(typeof(DbType), SettingValueToString(value), ignoreCase: true);
					if (!(value is DbType))
					{
						value = DbType.Object;
					}
				}
				return (DbType)value;
			}
			finally
			{
				if (!flag)
				{
					connection?.SetCachedSetting(name, value);
				}
			}
		}

		internal static bool LooksLikeNull(string text)
		{
			return text == null;
		}

		internal static bool LooksLikeInt64(string text)
		{
			if (!long.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var result))
			{
				return false;
			}
			return string.Equals(result.ToString(CultureInfo.InvariantCulture), text, StringComparison.Ordinal);
		}

		internal static bool LooksLikeDouble(string text)
		{
			if (!double.TryParse(text, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var result))
			{
				return false;
			}
			return string.Equals(result.ToString(CultureInfo.InvariantCulture), text, StringComparison.Ordinal);
		}

		internal static bool LooksLikeDateTime(SQLiteConvert convert, string text)
		{
			if (convert == null)
			{
				return false;
			}
			try
			{
				DateTime dateTime = convert.ToDateTime(text);
				if (string.Equals(convert.ToString(dateTime), text, StringComparison.Ordinal))
				{
					return true;
				}
			}
			catch
			{
			}
			return false;
		}

		internal static DbType TypeNameToDbType(SQLiteConnection connection, string typeName, SQLiteConnectionFlags flags)
		{
			DbType? dbType = null;
			if (connection != null)
			{
				flags |= connection.Flags;
				if ((flags & SQLiteConnectionFlags.UseConnectionTypes) == SQLiteConnectionFlags.UseConnectionTypes)
				{
					SQLiteDbTypeMap typeNames = connection._typeNames;
					if (typeNames != null && typeName != null)
					{
						if (typeNames.TryGetValue(typeName, out var value))
						{
							return value.dataType;
						}
						int num = typeName.IndexOf('(');
						if (num > 0 && typeNames.TryGetValue(typeName.Substring(0, num).TrimEnd(new char[0]), out value))
						{
							return value.dataType;
						}
					}
				}
				dbType = connection.DefaultDbType;
			}
			if ((flags & SQLiteConnectionFlags.NoGlobalTypes) == SQLiteConnectionFlags.NoGlobalTypes)
			{
				if (dbType.HasValue)
				{
					return dbType.Value;
				}
				dbType = GetDefaultDbType(connection);
				DefaultDbTypeWarning(typeName, flags, dbType);
				return dbType.Value;
			}
			lock (_syncRoot)
			{
				if (_typeNames == null)
				{
					_typeNames = GetSQLiteDbTypeMap();
				}
				if (typeName != null)
				{
					if (_typeNames.TryGetValue(typeName, out var value2))
					{
						return value2.dataType;
					}
					int num2 = typeName.IndexOf('(');
					if (num2 > 0 && _typeNames.TryGetValue(typeName.Substring(0, num2).TrimEnd(new char[0]), out value2))
					{
						return value2.dataType;
					}
				}
			}
			if (dbType.HasValue)
			{
				return dbType.Value;
			}
			dbType = GetDefaultDbType(connection);
			DefaultDbTypeWarning(typeName, flags, dbType);
			return dbType.Value;
		}
	}
	internal abstract class SQLiteBase : SQLiteConvert, IDisposable
	{
		internal const int COR_E_EXCEPTION = -2146233088;

		private bool disposed;

		private static string[] _errorMessages = new string[29]
		{
			"not an error", "SQL logic error or missing database", "internal logic error", "access permission denied", "callback requested query abort", "database is locked", "database table is locked", "out of memory", "attempt to write a readonly database", "interrupted",
			"disk I/O error", "database disk image is malformed", "unknown operation", "database or disk is full", "unable to open database file", "locking protocol", "table contains no data", "database schema has changed", "string or blob too big", "constraint failed",
			"datatype mismatch", "library routine called out of sequence", "large file support is disabled", "authorization denied", "auxiliary database format error", "bind or column index out of range", "file is encrypted or is not a database", "notification message", "warning message"
		};

		internal abstract string Version { get; }

		internal abstract int VersionNumber { get; }

		internal abstract long LastInsertRowId { get; }

		internal abstract int Changes { get; }

		internal abstract long MemoryUsed { get; }

		internal abstract long MemoryHighwater { get; }

		internal abstract bool OwnHandle { get; }

		internal abstract bool AutoCommit { get; }

		internal SQLiteBase(SQLiteDateFormats fmt, DateTimeKind kind, string fmtString)
			: base(fmt, kind, fmtString)
		{
		}

		internal abstract SQLiteErrorCode SetMemoryStatus(bool value);

		internal abstract SQLiteErrorCode ReleaseMemory();

		internal abstract SQLiteErrorCode Shutdown();

		internal abstract bool IsOpen();

		internal abstract void Open(string strFilename, SQLiteConnectionFlags connectionFlags, SQLiteOpenFlagsEnum openFlags, int maxPoolSize, bool usePool);

		internal abstract void Close(bool canThrow);

		internal abstract void SetTimeout(int nTimeoutMS);

		internal abstract string GetLastError();

		internal abstract string GetLastError(string defValue);

		internal abstract void ClearPool();

		internal abstract int CountPool();

		internal abstract SQLiteStatement Prepare(SQLiteConnection cnn, string strSql, SQLiteStatement previous, uint timeoutMS, ref string strRemain);

		internal abstract bool Step(SQLiteStatement stmt);

		internal abstract bool IsReadOnly(SQLiteStatement stmt);

		internal abstract SQLiteErrorCode Reset(SQLiteStatement stmt);

		internal abstract void Cancel();

		internal abstract void BindFunction(SQLiteFunctionAttribute functionAttribute, SQLiteFunction function, SQLiteConnectionFlags flags);

		internal abstract void Bind_Double(SQLiteStatement stmt, SQLiteConnectionFlags flags, int index, double value);

		internal abstract void Bind_Int32(SQLiteStatement stmt, SQLiteConnectionFlags flags, int index, int value);

		internal abstract void Bind_UInt32(SQLiteStatement stmt, SQLiteConnectionFlags flags, int index, uint value);

		internal abstract void Bind_Int64(SQLiteStatement stmt, SQLiteConnectionFlags flags, int index, long value);

		internal abstract void Bind_UInt64(SQLiteStatement stmt, SQLiteConnectionFlags flags, int index, ulong value);

		internal abstract void Bind_Text(SQLiteStatement stmt, SQLiteConnectionFlags flags, int index, string value);

		internal abstract void Bind_Blob(SQLiteStatement stmt, SQLiteConnectionFlags flags, int index, byte[] blobData);

		internal abstract void Bind_DateTime(SQLiteStatement stmt, SQLiteConnectionFlags flags, int index, DateTime dt);

		internal abstract void Bind_Null(SQLiteStatement stmt, SQLiteConnectionFlags flags, int index);

		internal abstract int Bind_ParamCount(SQLiteStatement stmt, SQLiteConnectionFlags flags);

		internal abstract string Bind_ParamName(SQLiteStatement stmt, SQLiteConnectionFlags flags, int index);

		internal abstract int Bind_ParamIndex(SQLiteStatement stmt, SQLiteConnectionFlags flags, string paramName);

		internal abstract int ColumnCount(SQLiteStatement stmt);

		internal abstract string ColumnName(SQLiteStatement stmt, int index);

		internal abstract TypeAffinity ColumnAffinity(SQLiteStatement stmt, int index);

		internal abstract string ColumnType(SQLiteStatement stmt, int index, ref TypeAffinity nAffinity);

		internal abstract int ColumnIndex(SQLiteStatement stmt, string columnName);

		internal abstract string ColumnOriginalName(SQLiteStatement stmt, int index);

		internal abstract string ColumnDatabaseName(SQLiteStatement stmt, int index);

		internal abstract string ColumnTableName(SQLiteStatement stmt, int index);

		internal abstract void ColumnMetaData(string dataBase, string table, string column, ref string dataType, ref string collateSequence, ref bool notNull, ref bool primaryKey, ref bool autoIncrement);

		internal abstract void GetIndexColumnExtendedInfo(string database, string index, string column, ref int sortMode, ref int onError, ref string collationSequence);

		internal abstract double GetDouble(SQLiteStatement stmt, int index);

		internal abstract sbyte GetSByte(SQLiteStatement stmt, int index);

		internal abstract byte GetByte(SQLiteStatement stmt, int index);

		internal abstract short GetInt16(SQLiteStatement stmt, int index);

		internal abstract ushort GetUInt16(SQLiteStatement stmt, int index);

		internal abstract int GetInt32(SQLiteStatement stmt, int index);

		internal abstract uint GetUInt32(SQLiteStatement stmt, int index);

		internal abstract long GetInt64(SQLiteStatement stmt, int index);

		internal abstract ulong GetUInt64(SQLiteStatement stmt, int index);

		internal abstract string GetText(SQLiteStatement stmt, int index);

		internal abstract long GetBytes(SQLiteStatement stmt, int index, int nDataoffset, byte[] bDest, int nStart, int nLength);

		internal abstract long GetChars(SQLiteStatement stmt, int index, int nDataoffset, char[] bDest, int nStart, int nLength);

		internal abstract DateTime GetDateTime(SQLiteStatement stmt, int index);

		internal abstract bool IsNull(SQLiteStatement stmt, int index);

		internal abstract void CreateCollation(string strCollation, SQLiteCollation func, SQLiteCollation func16);

		internal abstract void CreateFunction(string strFunction, int nArgs, bool needCollSeq, SQLiteCallback func, SQLiteCallback funcstep, SQLiteFinalCallback funcfinal);

		internal abstract CollationSequence GetCollationSequence(SQLiteFunction func, IntPtr context);

		internal abstract int ContextCollateCompare(CollationEncodingEnum enc, IntPtr context, string s1, string s2);

		internal abstract int ContextCollateCompare(CollationEncodingEnum enc, IntPtr context, char[] c1, char[] c2);

		internal abstract int AggregateCount(IntPtr context);

		internal abstract IntPtr AggregateContext(IntPtr context);

		internal abstract long GetParamValueBytes(IntPtr ptr, int nDataOffset, byte[] bDest, int nStart, int nLength);

		internal abstract double GetParamValueDouble(IntPtr ptr);

		internal abstract int GetParamValueInt32(IntPtr ptr);

		internal abstract long GetParamValueInt64(IntPtr ptr);

		internal abstract string GetParamValueText(IntPtr ptr);

		internal abstract TypeAffinity GetParamValueType(IntPtr ptr);

		internal abstract void ReturnBlob(IntPtr context, byte[] value);

		internal abstract void ReturnDouble(IntPtr context, double value);

		internal abstract void ReturnError(IntPtr context, string value);

		internal abstract void ReturnInt32(IntPtr context, int value);

		internal abstract void ReturnInt64(IntPtr context, long value);

		internal abstract void ReturnNull(IntPtr context);

		internal abstract void ReturnText(IntPtr context, string value);

		internal abstract void CreateModule(SQLiteModule module, SQLiteConnectionFlags flags);

		internal abstract void DisposeModule(SQLiteModule module, SQLiteConnectionFlags flags);

		internal abstract SQLiteErrorCode DeclareVirtualTable(SQLiteModule module, string strSql, ref string error);

		internal abstract SQLiteErrorCode DeclareVirtualFunction(SQLiteModule module, int argumentCount, string name, ref string error);

		internal abstract void SetLoadExtension(bool bOnOff);

		internal abstract void LoadExtension(string fileName, string procName);

		internal abstract void SetExtendedResultCodes(bool bOnOff);

		internal abstract SQLiteErrorCode ResultCode();

		internal abstract SQLiteErrorCode ExtendedResultCode();

		internal abstract void LogMessage(SQLiteErrorCode iErrCode, string zMessage);

		internal abstract void SetPassword(byte[] passwordBytes);

		internal abstract void ChangePassword(byte[] newPasswordBytes);

		internal abstract void SetAuthorizerHook(SQLiteAuthorizerCallback func);

		internal abstract void SetUpdateHook(SQLiteUpdateCallback func);

		internal abstract void SetCommitHook(SQLiteCommitCallback func);

		internal abstract void SetTraceCallback(SQLiteTraceCallback func);

		internal abstract void SetRollbackHook(SQLiteRollbackCallback func);

		internal abstract SQLiteErrorCode SetLogCallback(SQLiteLogCallback func);

		internal abstract bool IsInitialized();

		internal abstract int GetCursorForTable(SQLiteStatement stmt, int database, int rootPage);

		internal abstract long GetRowIdForCursor(SQLiteStatement stmt, int cursor);

		internal abstract object GetValue(SQLiteStatement stmt, SQLiteConnectionFlags flags, int index, SQLiteType typ);

		internal abstract SQLiteErrorCode FileControl(string zDbName, int op, IntPtr pArg);

		internal abstract SQLiteBackup InitializeBackup(SQLiteConnection destCnn, string destName, string sourceName);

		internal abstract bool StepBackup(SQLiteBackup backup, int nPage, ref bool retry);

		internal abstract int RemainingBackup(SQLiteBackup backup);

		internal abstract int PageCountBackup(SQLiteBackup backup);

		internal abstract void FinishBackup(SQLiteBackup backup);

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		private void CheckDisposed()
		{
			if (disposed)
			{
				throw new ObjectDisposedException(typeof(SQLiteBase).Name);
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				disposed = true;
			}
		}

		~SQLiteBase()
		{
			Dispose(disposing: false);
		}

		protected static string FallbackGetErrorString(SQLiteErrorCode rc)
		{
			if (_errorMessages == null)
			{
				return null;
			}
			int num = (int)rc;
			if (num < 0 || num >= _errorMessages.Length)
			{
				num = 1;
			}
			return _errorMessages[num];
		}

		internal static string GetLastError(SQLiteConnectionHandle hdl, IntPtr db)
		{
			if (hdl == null || db == IntPtr.Zero)
			{
				return "null connection or database handle";
			}
			string result = null;
			try
			{
			}
			finally
			{
				lock (hdl)
				{
					result = ((hdl.IsInvalid || hdl.IsClosed) ? "closed or invalid connection handle" : SQLiteConvert.UTF8ToString(UnsafeNativeMethods.sqlite3_errmsg(db), -1));
				}
			}
			GC.KeepAlive(hdl);
			return result;
		}

		internal static void FinishBackup(SQLiteConnectionHandle hdl, IntPtr backup)
		{
			if (hdl == null || backup == IntPtr.Zero)
			{
				return;
			}
			try
			{
			}
			finally
			{
				lock (hdl)
				{
					SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_backup_finish(backup);
					if (sQLiteErrorCode != SQLiteErrorCode.Ok)
					{
						throw new SQLiteException(sQLiteErrorCode, null);
					}
				}
			}
		}

		internal static void FinalizeStatement(SQLiteConnectionHandle hdl, IntPtr stmt)
		{
			if (hdl == null || stmt == IntPtr.Zero)
			{
				return;
			}
			try
			{
			}
			finally
			{
				lock (hdl)
				{
					SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_finalize(stmt);
					if (sQLiteErrorCode != SQLiteErrorCode.Ok)
					{
						throw new SQLiteException(sQLiteErrorCode, null);
					}
				}
			}
		}

		internal static void CloseConnection(SQLiteConnectionHandle hdl, IntPtr db)
		{
			if (hdl == null || db == IntPtr.Zero)
			{
				return;
			}
			try
			{
			}
			finally
			{
				lock (hdl)
				{
					ResetConnection(hdl, db, canThrow: false);
					SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_close(db);
					if (sQLiteErrorCode != SQLiteErrorCode.Ok)
					{
						throw new SQLiteException(sQLiteErrorCode, GetLastError(hdl, db));
					}
				}
			}
		}

		internal static void CloseConnectionV2(SQLiteConnectionHandle hdl, IntPtr db)
		{
			if (hdl == null || db == IntPtr.Zero)
			{
				return;
			}
			try
			{
			}
			finally
			{
				lock (hdl)
				{
					ResetConnection(hdl, db, canThrow: false);
					SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_close_v2(db);
					if (sQLiteErrorCode != SQLiteErrorCode.Ok)
					{
						throw new SQLiteException(sQLiteErrorCode, GetLastError(hdl, db));
					}
				}
			}
		}

		internal static bool ResetConnection(SQLiteConnectionHandle hdl, IntPtr db, bool canThrow)
		{
			if (hdl == null || db == IntPtr.Zero)
			{
				return false;
			}
			bool result = false;
			try
			{
			}
			finally
			{
				lock (hdl)
				{
					if (canThrow && hdl.IsInvalid)
					{
						throw new InvalidOperationException("The connection handle is invalid.");
					}
					if (canThrow && hdl.IsClosed)
					{
						throw new InvalidOperationException("The connection handle is closed.");
					}
					if (!hdl.IsInvalid && !hdl.IsClosed)
					{
						IntPtr errMsg = IntPtr.Zero;
						do
						{
							errMsg = UnsafeNativeMethods.sqlite3_next_stmt(db, errMsg);
							if (errMsg != IntPtr.Zero)
							{
								SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_reset(errMsg);
							}
						}
						while (errMsg != IntPtr.Zero);
						if (IsAutocommit(hdl, db))
						{
							result = true;
						}
						else
						{
							SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_exec(db, SQLiteConvert.ToUTF8("ROLLBACK"), IntPtr.Zero, IntPtr.Zero, ref errMsg);
							if (sQLiteErrorCode == SQLiteErrorCode.Ok)
							{
								result = true;
							}
							else if (canThrow)
							{
								throw new SQLiteException(sQLiteErrorCode, GetLastError(hdl, db));
							}
						}
					}
				}
			}
			GC.KeepAlive(hdl);
			return result;
		}

		internal static bool IsAutocommit(SQLiteConnectionHandle hdl, IntPtr db)
		{
			if (hdl == null || db == IntPtr.Zero)
			{
				return false;
			}
			bool result = false;
			try
			{
			}
			finally
			{
				lock (hdl)
				{
					if (!hdl.IsInvalid && !hdl.IsClosed)
					{
						result = UnsafeNativeMethods.sqlite3_get_autocommit(db) == 1;
					}
				}
			}
			GC.KeepAlive(hdl);
			return result;
		}
	}
	internal class SQLite3 : SQLiteBase
	{
		internal const string PublicKey = "002400000480000094000000060200000024000052534131000400000100010005a288de5687c4e1b621ddff5d844727418956997f475eb829429e411aff3e93f97b70de698b972640925bdd44280df0a25a843266973704137cbb0e7441c1fe7cae4e2440ae91ab8cde3933febcb1ac48dd33b40e13c421d8215c18a4349a436dd499e3c385cc683015f886f6c10bd90115eb2bd61b67750839e3a19941dc9c";

		internal const string DesignerVersion = "1.0.97.0";

		private static object syncRoot = new object();

		protected internal SQLiteConnectionHandle _sql;

		protected string _fileName;

		protected bool _usePool;

		protected int _poolVersion;

		private bool _buildingSchema;

		protected List<SQLiteFunction> _functions;

		protected Dictionary<string, SQLiteModule> _modules;

		private bool disposed;

		private static bool? have_errstr = null;

		private static bool? have_stmt_readonly = null;

		private static bool? forceLogPrepare = null;

		internal override string Version => SQLiteVersion;

		internal override int VersionNumber => SQLiteVersionNumber;

		internal static string DefineConstants
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				IList<string> optionList = SQLiteDefineConstants.OptionList;
				if (optionList != null)
				{
					foreach (string item in optionList)
					{
						if (item != null)
						{
							if (stringBuilder.Length > 0)
							{
								stringBuilder.Append(' ');
							}
							stringBuilder.Append(item);
						}
					}
				}
				return stringBuilder.ToString();
			}
		}

		internal static string SQLiteVersion => SQLiteConvert.UTF8ToString(UnsafeNativeMethods.sqlite3_libversion(), -1);

		internal static int SQLiteVersionNumber => UnsafeNativeMethods.sqlite3_libversion_number();

		internal static string SQLiteSourceId => SQLiteConvert.UTF8ToString(UnsafeNativeMethods.sqlite3_sourceid(), -1);

		internal static string SQLiteCompileOptions
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				int num = 0;
				IntPtr intPtr = UnsafeNativeMethods.sqlite3_compileoption_get(num++);
				while (intPtr != IntPtr.Zero)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(' ');
					}
					stringBuilder.Append(SQLiteConvert.UTF8ToString(intPtr, -1));
					intPtr = UnsafeNativeMethods.sqlite3_compileoption_get(num++);
				}
				return stringBuilder.ToString();
			}
		}

		internal static string InteropVersion => null;

		internal static string InteropSourceId => null;

		internal static string InteropCompileOptions => null;

		internal override bool AutoCommit => SQLiteBase.IsAutocommit(_sql, _sql);

		internal override long LastInsertRowId => UnsafeNativeMethods.sqlite3_last_insert_rowid(_sql);

		internal override int Changes => UnsafeNativeMethods.sqlite3_changes(_sql);

		internal override long MemoryUsed => StaticMemoryUsed;

		internal static long StaticMemoryUsed => UnsafeNativeMethods.sqlite3_memory_used();

		internal override long MemoryHighwater => StaticMemoryHighwater;

		internal static long StaticMemoryHighwater => UnsafeNativeMethods.sqlite3_memory_highwater(0);

		internal override bool OwnHandle
		{
			get
			{
				if (_sql == null)
				{
					throw new SQLiteException("no connection handle available");
				}
				return _sql.OwnHandle;
			}
		}

		internal SQLite3(SQLiteDateFormats fmt, DateTimeKind kind, string fmtString, IntPtr db, string fileName, bool ownHandle)
			: base(fmt, kind, fmtString)
		{
			if (db != IntPtr.Zero)
			{
				_sql = new SQLiteConnectionHandle(db, ownHandle);
				_fileName = fileName;
				SQLiteConnection.OnChanged(null, new ConnectionEventArgs(SQLiteConnectionEventType.NewCriticalHandle, null, null, null, null, _sql, fileName, new object[6] { fmt, kind, fmtString, db, fileName, ownHandle }));
			}
		}

		private void CheckDisposed()
		{
			if (disposed)
			{
				throw new ObjectDisposedException(typeof(SQLite3).Name);
			}
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!disposed)
				{
					DisposeModules();
					Close(canThrow: false);
				}
			}
			finally
			{
				base.Dispose(disposing);
				disposed = true;
			}
		}

		private void DisposeModules()
		{
			if (_modules == null)
			{
				return;
			}
			foreach (KeyValuePair<string, SQLiteModule> module in _modules)
			{
				module.Value?.Dispose();
			}
			_modules.Clear();
		}

		internal override void Close(bool canThrow)
		{
			if (_sql == null)
			{
				return;
			}
			if (!_sql.OwnHandle)
			{
				_sql = null;
				return;
			}
			if (_usePool)
			{
				if (SQLiteBase.ResetConnection(_sql, _sql, canThrow))
				{
					DisposeModules();
					SQLiteConnectionPool.Add(_fileName, _sql, _poolVersion);
				}
			}
			else
			{
				_sql.Dispose();
			}
			_sql = null;
		}

		internal override void Cancel()
		{
			try
			{
			}
			finally
			{
				UnsafeNativeMethods.sqlite3_interrupt(_sql);
			}
		}

		internal override void BindFunction(SQLiteFunctionAttribute functionAttribute, SQLiteFunction function, SQLiteConnectionFlags flags)
		{
			SQLiteFunction.BindFunction(this, functionAttribute, function, flags);
			if (_functions == null)
			{
				_functions = new List<SQLiteFunction>();
			}
			_functions.Add(function);
		}

		internal override SQLiteErrorCode SetMemoryStatus(bool value)
		{
			return StaticSetMemoryStatus(value);
		}

		internal static SQLiteErrorCode StaticSetMemoryStatus(bool value)
		{
			return UnsafeNativeMethods.sqlite3_config_int(SQLiteConfigOpsEnum.SQLITE_CONFIG_MEMSTATUS, value ? 1 : 0);
		}

		internal override SQLiteErrorCode ReleaseMemory()
		{
			return UnsafeNativeMethods.sqlite3_db_release_memory(_sql);
		}

		internal static SQLiteErrorCode StaticReleaseMemory(int nBytes, bool reset, bool compact, ref int nFree, ref bool resetOk, ref uint nLargest)
		{
			SQLiteErrorCode sQLiteErrorCode = SQLiteErrorCode.Ok;
			int num = UnsafeNativeMethods.sqlite3_release_memory(nBytes);
			uint largest = 0u;
			bool flag = false;
			if (sQLiteErrorCode == SQLiteErrorCode.Ok && reset)
			{
				sQLiteErrorCode = UnsafeNativeMethods.sqlite3_win32_reset_heap();
				if (sQLiteErrorCode == SQLiteErrorCode.Ok)
				{
					flag = true;
				}
			}
			if (sQLiteErrorCode == SQLiteErrorCode.Ok && compact)
			{
				sQLiteErrorCode = UnsafeNativeMethods.sqlite3_win32_compact_heap(ref largest);
			}
			nFree = num;
			nLargest = largest;
			resetOk = flag;
			return sQLiteErrorCode;
		}

		internal override SQLiteErrorCode Shutdown()
		{
			return StaticShutdown(directories: false);
		}

		internal static SQLiteErrorCode StaticShutdown(bool directories)
		{
			SQLiteErrorCode sQLiteErrorCode = SQLiteErrorCode.Ok;
			if (directories)
			{
				if (sQLiteErrorCode == SQLiteErrorCode.Ok)
				{
					sQLiteErrorCode = UnsafeNativeMethods.sqlite3_win32_set_directory(1u, null);
				}
				if (sQLiteErrorCode == SQLiteErrorCode.Ok)
				{
					sQLiteErrorCode = UnsafeNativeMethods.sqlite3_win32_set_directory(2u, null);
				}
			}
			if (sQLiteErrorCode == SQLiteErrorCode.Ok)
			{
				sQLiteErrorCode = UnsafeNativeMethods.sqlite3_shutdown();
			}
			return sQLiteErrorCode;
		}

		internal override bool IsOpen()
		{
			if (_sql != null && !_sql.IsInvalid)
			{
				return !_sql.IsClosed;
			}
			return false;
		}

		internal override void Open(string strFilename, SQLiteConnectionFlags connectionFlags, SQLiteOpenFlagsEnum openFlags, int maxPoolSize, bool usePool)
		{
			if (_sql != null)
			{
				Close(canThrow: true);
			}
			if (_sql != null)
			{
				throw new SQLiteException("connection handle is still active");
			}
			_usePool = usePool;
			_fileName = strFilename;
			if (usePool)
			{
				_sql = SQLiteConnectionPool.Remove(strFilename, maxPoolSize, out _poolVersion);
			}
			if (_sql == null)
			{
				try
				{
				}
				finally
				{
					IntPtr db = IntPtr.Zero;
					SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_open_v2(SQLiteConvert.ToUTF8(strFilename), ref db, openFlags, IntPtr.Zero);
					if (sQLiteErrorCode != SQLiteErrorCode.Ok)
					{
						throw new SQLiteException(sQLiteErrorCode, null);
					}
					_sql = new SQLiteConnectionHandle(db, ownHandle: true);
				}
				lock (_sql)
				{
				}
				SQLiteConnection.OnChanged(null, new ConnectionEventArgs(SQLiteConnectionEventType.NewCriticalHandle, null, null, null, null, _sql, strFilename, new object[5] { strFilename, connectionFlags, openFlags, maxPoolSize, usePool }));
			}
			if ((connectionFlags & SQLiteConnectionFlags.NoBindFunctions) != SQLiteConnectionFlags.NoBindFunctions)
			{
				if (_functions == null)
				{
					_functions = new List<SQLiteFunction>();
				}
				_functions.AddRange(new List<SQLiteFunction>(SQLiteFunction.BindFunctions(this, connectionFlags)));
			}
			SetTimeout(0);
			GC.KeepAlive(_sql);
		}

		internal override void ClearPool()
		{
			SQLiteConnectionPool.ClearPool(_fileName);
		}

		internal override int CountPool()
		{
			Dictionary<string, int> counts = null;
			int openCount = 0;
			int closeCount = 0;
			int totalCount = 0;
			SQLiteConnectionPool.GetCounts(_fileName, ref counts, ref openCount, ref closeCount, ref totalCount);
			return totalCount;
		}

		internal override void SetTimeout(int nTimeoutMS)
		{
			IntPtr intPtr = _sql;
			if (intPtr == IntPtr.Zero)
			{
				throw new SQLiteException("no connection handle available");
			}
			SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_busy_timeout(intPtr, nTimeoutMS);
			if (sQLiteErrorCode != SQLiteErrorCode.Ok)
			{
				throw new SQLiteException(sQLiteErrorCode, GetLastError());
			}
		}

		internal override bool Step(SQLiteStatement stmt)
		{
			Random random = null;
			uint tickCount = (uint)Environment.TickCount;
			uint num = (uint)(stmt._command._commandTimeout * 1000);
			while (true)
			{
				SQLiteErrorCode sQLiteErrorCode;
				try
				{
				}
				finally
				{
					sQLiteErrorCode = UnsafeNativeMethods.sqlite3_step(stmt._sqlite_stmt);
				}
				switch (sQLiteErrorCode)
				{
				case SQLiteErrorCode.Ok:
					continue;
				case SQLiteErrorCode.Row:
					return true;
				case SQLiteErrorCode.Done:
					return false;
				}
				SQLiteErrorCode sQLiteErrorCode2 = Reset(stmt);
				switch (sQLiteErrorCode2)
				{
				case SQLiteErrorCode.Ok:
					throw new SQLiteException(sQLiteErrorCode, GetLastError());
				case SQLiteErrorCode.Busy:
				case SQLiteErrorCode.Locked:
					if (stmt._command != null)
					{
						if (random == null)
						{
							random = new Random();
						}
						if ((uint)(Environment.TickCount - (int)tickCount) > num)
						{
							throw new SQLiteException(sQLiteErrorCode2, GetLastError());
						}
						Thread.Sleep(random.Next(1, 150));
					}
					break;
				}
			}
		}

		internal static string GetErrorString(SQLiteErrorCode rc)
		{
			try
			{
				if (!have_errstr.HasValue)
				{
					int sQLiteVersionNumber = SQLiteVersionNumber;
					have_errstr = sQLiteVersionNumber >= 3007015;
				}
				if (have_errstr.Value)
				{
					IntPtr intPtr = UnsafeNativeMethods.sqlite3_errstr(rc);
					if (intPtr != IntPtr.Zero)
					{
						return Marshal.PtrToStringAnsi(intPtr);
					}
				}
			}
			catch (EntryPointNotFoundException)
			{
			}
			return SQLiteBase.FallbackGetErrorString(rc);
		}

		internal override bool IsReadOnly(SQLiteStatement stmt)
		{
			try
			{
				if (!have_stmt_readonly.HasValue)
				{
					int sQLiteVersionNumber = SQLiteVersionNumber;
					have_stmt_readonly = sQLiteVersionNumber >= 3007004;
				}
				if (have_stmt_readonly.Value)
				{
					return UnsafeNativeMethods.sqlite3_stmt_readonly(stmt._sqlite_stmt) != 0;
				}
			}
			catch (EntryPointNotFoundException)
			{
			}
			return false;
		}

		internal override SQLiteErrorCode Reset(SQLiteStatement stmt)
		{
			SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_reset(stmt._sqlite_stmt);
			switch (sQLiteErrorCode)
			{
			case SQLiteErrorCode.Schema:
			{
				string strRemain = null;
				using (SQLiteStatement sQLiteStatement = Prepare(null, stmt._sqlStatement, null, (uint)(stmt._command._commandTimeout * 1000), ref strRemain))
				{
					stmt._sqlite_stmt.Dispose();
					stmt._sqlite_stmt = sQLiteStatement._sqlite_stmt;
					sQLiteStatement._sqlite_stmt = null;
					stmt.BindParameters();
				}
				return SQLiteErrorCode.Unknown;
			}
			case SQLiteErrorCode.Busy:
			case SQLiteErrorCode.Locked:
				return sQLiteErrorCode;
			default:
				throw new SQLiteException(sQLiteErrorCode, GetLastError());
			case SQLiteErrorCode.Ok:
				return sQLiteErrorCode;
			}
		}

		internal override string GetLastError()
		{
			return GetLastError(null);
		}

		internal override string GetLastError(string defValue)
		{
			string text = SQLiteBase.GetLastError(_sql, _sql);
			if (string.IsNullOrEmpty(text))
			{
				text = defValue;
			}
			return text;
		}

		private static bool ForceLogPrepare()
		{
			lock (syncRoot)
			{
				if (!forceLogPrepare.HasValue)
				{
					if (UnsafeNativeMethods.GetSettingValue("SQLite_ForceLogPrepare", null) != null)
					{
						forceLogPrepare = true;
					}
					else
					{
						forceLogPrepare = false;
					}
				}
				return forceLogPrepare.Value;
			}
		}

		internal override SQLiteStatement Prepare(SQLiteConnection cnn, string strSql, SQLiteStatement previous, uint timeoutMS, ref string strRemain)
		{
			if (!string.IsNullOrEmpty(strSql))
			{
				string text = cnn?._baseSchemaName;
				if (!string.IsNullOrEmpty(text))
				{
					strSql = strSql.Replace(string.Format(CultureInfo.InvariantCulture, "[{0}].", new object[1] { text }), string.Empty);
					strSql = strSql.Replace(string.Format(CultureInfo.InvariantCulture, "{0}.", new object[1] { text }), string.Empty);
				}
			}
			SQLiteConnectionFlags sQLiteConnectionFlags = cnn?.Flags ?? SQLiteConnectionFlags.Default;
			if (ForceLogPrepare() || (sQLiteConnectionFlags & SQLiteConnectionFlags.LogPrepare) == SQLiteConnectionFlags.LogPrepare)
			{
				if (strSql == null || strSql.Length == 0 || strSql.Trim().Length == 0)
				{
					SQLiteLog.LogMessage("Preparing {<nothing>}...");
				}
				else
				{
					SQLiteLog.LogMessage(string.Format(CultureInfo.CurrentCulture, "Preparing {{{0}}}...", new object[1] { strSql }));
				}
			}
			IntPtr zero = IntPtr.Zero;
			IntPtr ptrRemain = IntPtr.Zero;
			int nativestringlen = 0;
			SQLiteErrorCode sQLiteErrorCode = SQLiteErrorCode.Schema;
			int num = 0;
			int num2 = cnn?._prepareRetries ?? 3;
			byte[] array = SQLiteConvert.ToUTF8(strSql);
			string text2 = null;
			SQLiteStatement sQLiteStatement = null;
			Random random = null;
			uint tickCount = (uint)Environment.TickCount;
			GCHandle gCHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			IntPtr pSql = gCHandle.AddrOfPinnedObject();
			SQLiteStatementHandle sQLiteStatementHandle = null;
			try
			{
				while ((sQLiteErrorCode == SQLiteErrorCode.Schema || sQLiteErrorCode == SQLiteErrorCode.Locked || sQLiteErrorCode == SQLiteErrorCode.Busy) && num < num2)
				{
					try
					{
					}
					finally
					{
						zero = IntPtr.Zero;
						ptrRemain = IntPtr.Zero;
						sQLiteErrorCode = UnsafeNativeMethods.sqlite3_prepare_v2(_sql, pSql, array.Length - 1, ref zero, ref ptrRemain);
						nativestringlen = -1;
						if (sQLiteErrorCode == SQLiteErrorCode.Ok && zero != IntPtr.Zero)
						{
							sQLiteStatementHandle?.Dispose();
							sQLiteStatementHandle = new SQLiteStatementHandle(_sql, zero);
						}
					}
					if (sQLiteStatementHandle != null)
					{
						SQLiteConnection.OnChanged(null, new ConnectionEventArgs(SQLiteConnectionEventType.NewCriticalHandle, null, null, null, null, sQLiteStatementHandle, strSql, new object[4] { cnn, strSql, previous, timeoutMS }));
					}
					switch (sQLiteErrorCode)
					{
					case SQLiteErrorCode.Schema:
						num++;
						break;
					case SQLiteErrorCode.Error:
						if (string.Compare(GetLastError(), "near \"TYPES\": syntax error", StringComparison.OrdinalIgnoreCase) == 0)
						{
							int num3 = strSql.IndexOf(';');
							if (num3 == -1)
							{
								num3 = strSql.Length - 1;
							}
							text2 = strSql.Substring(0, num3 + 1);
							strSql = strSql.Substring(num3 + 1);
							strRemain = "";
							while (sQLiteStatement == null && strSql.Length > 0)
							{
								sQLiteStatement = Prepare(cnn, strSql, previous, timeoutMS, ref strRemain);
								strSql = strRemain;
							}
							sQLiteStatement?.SetTypes(text2);
							return sQLiteStatement;
						}
						if (_buildingSchema || string.Compare(GetLastError(), 0, "no such table: TEMP.SCHEMA", 0, 26, StringComparison.OrdinalIgnoreCase) != 0)
						{
							break;
						}
						strRemain = "";
						_buildingSchema = true;
						try
						{
							if (((IServiceProvider)SQLiteFactory.Instance).GetService(typeof(ISQLiteSchemaExtensions)) is ISQLiteSchemaExtensions iSQLiteSchemaExtensions)
							{
								iSQLiteSchemaExtensions.BuildTempSchema(cnn);
							}
							while (sQLiteStatement == null && strSql.Length > 0)
							{
								sQLiteStatement = Prepare(cnn, strSql, previous, timeoutMS, ref strRemain);
								strSql = strRemain;
							}
							return sQLiteStatement;
						}
						finally
						{
							_buildingSchema = false;
						}
					case SQLiteErrorCode.Busy:
					case SQLiteErrorCode.Locked:
						if (random == null)
						{
							random = new Random();
						}
						if ((uint)(Environment.TickCount - (int)tickCount) > timeoutMS)
						{
							throw new SQLiteException(sQLiteErrorCode, GetLastError());
						}
						Thread.Sleep(random.Next(1, 150));
						break;
					}
				}
				if (sQLiteErrorCode != SQLiteErrorCode.Ok)
				{
					throw new SQLiteException(sQLiteErrorCode, GetLastError());
				}
				strRemain = SQLiteConvert.UTF8ToString(ptrRemain, nativestringlen);
				if (sQLiteStatementHandle != null)
				{
					sQLiteStatement = new SQLiteStatement(this, sQLiteConnectionFlags, sQLiteStatementHandle, strSql.Substring(0, strSql.Length - strRemain.Length), previous);
				}
				return sQLiteStatement;
			}
			finally
			{
				gCHandle.Free();
			}
		}

		protected static void LogBind(SQLiteStatementHandle handle, int index)
		{
			IntPtr intPtr = handle;
			SQLiteLog.LogMessage(string.Format(CultureInfo.CurrentCulture, "Binding statement {0} paramter #{1} as NULL...", new object[2] { intPtr, index }));
		}

		protected static void LogBind(SQLiteStatementHandle handle, int index, ValueType value)
		{
			IntPtr intPtr = handle;
			SQLiteLog.LogMessage($"Binding statement {intPtr} paramter #{index} as type {value.GetType()} with value {{{value}}}...");
		}

		private static string FormatDateTime(DateTime value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(value.ToString("yyyy-MM-ddTHH:mm:ss.FFFFFFFK"));
			stringBuilder.Append(' ');
			stringBuilder.Append(value.Kind);
			stringBuilder.Append(' ');
			stringBuilder.Append(value.Ticks);
			return stringBuilder.ToString();
		}

		protected static void LogBind(SQLiteStatementHandle handle, int index, DateTime value)
		{
			IntPtr intPtr = handle;
			SQLiteLog.LogMessage($"Binding statement {intPtr} paramter #{index} as type {typeof(DateTime)} with value {{{FormatDateTime(value)}}}...");
		}

		protected static void LogBind(SQLiteStatementHandle handle, int index, string value)
		{
			IntPtr intPtr = handle;
			SQLiteLog.LogMessage(string.Format("Binding statement {0} paramter #{1} as type {2} with value {{{3}}}...", intPtr, index, typeof(string), (value != null) ? value : "<null>"));
		}

		private static string ToHexadecimalString(byte[] array)
		{
			if (array == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(array.Length * 2);
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				stringBuilder.Append(array[i].ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		protected static void LogBind(SQLiteStatementHandle handle, int index, byte[] value)
		{
			IntPtr intPtr = handle;
			SQLiteLog.LogMessage(string.Format("Binding statement {0} paramter #{1} as type {2} with value {{{3}}}...", intPtr, index, typeof(byte[]), (value != null) ? ToHexadecimalString(value) : "<null>"));
		}

		internal override void Bind_Double(SQLiteStatement stmt, SQLiteConnectionFlags flags, int index, double value)
		{
			SQLiteStatementHandle sqlite_stmt = stmt._sqlite_stmt;
			if ((flags & SQLiteConnectionFlags.LogBind) == SQLiteConnectionFlags.LogBind)
			{
				LogBind(sqlite_stmt, index, value);
			}
			SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_bind_double(sqlite_stmt, index, value);
			if (sQLiteErrorCode != SQLiteErrorCode.Ok)
			{
				throw new SQLiteException(sQLiteErrorCode, GetLastError());
			}
		}

		internal override void Bind_Int32(SQLiteStatement stmt, SQLiteConnectionFlags flags, int index, int value)
		{
			SQLiteStatementHandle sqlite_stmt = stmt._sqlite_stmt;
			if ((flags & SQLiteConnectionFlags.LogBind) == SQLiteConnectionFlags.LogBind)
			{
				LogBind(sqlite_stmt, index, value);
			}
			SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_bind_int(sqlite_stmt, index, value);
			if (sQLiteErrorCode != SQLiteErrorCode.Ok)
			{
				throw new SQLiteException(sQLiteErrorCode, GetLastError());
			}
		}

		internal override void Bind_UInt32(SQLiteStatement stmt, SQLiteConnectionFlags flags, int index, uint value)
		{
			SQLiteStatementHandle sqlite_stmt = stmt._sqlite_stmt;
			if ((flags & SQLiteConnectionFlags.LogBind) == SQLiteConnectionFlags.LogBind)
			{
				LogBind(sqlite_stmt, index, value);
			}
			SQLiteErrorCode sQLiteErrorCode;
			if ((flags & SQLiteConnectionFlags.BindUInt32AsInt64) == SQLiteConnectionFlags.BindUInt32AsInt64)
			{
				long value2 = value;
				sQLiteErrorCode = UnsafeNativeMethods.sqlite3_bind_int64(sqlite_stmt, index, value2);
			}
			else
			{
				sQLiteErrorCode = UnsafeNativeMethods.sqlite3_bind_uint(sqlite_stmt, index, value);
			}
			if (sQLiteErrorCode != SQLiteErrorCode.Ok)
			{
				throw new SQLiteException(sQLiteErrorCode, GetLastError());
			}
		}

		internal override void Bind_Int64(SQLiteStatement stmt, SQLiteConnectionFlags flags, int index, long value)
		{
			SQLiteStatementHandle sqlite_stmt = stmt._sqlite_stmt;
			if ((flags & SQLiteConnectionFlags.LogBind) == SQLiteConnectionFlags.LogBind)
			{
				LogBind(sqlite_stmt, index, value);
			}
			SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_bind_int64(sqlite_stmt, index, value);
			if (sQLiteErrorCode != SQLiteErrorCode.Ok)
			{
				throw new SQLiteException(sQLiteErrorCode, GetLastError());
			}
		}

		internal override void Bind_UInt64(SQLiteStatement stmt, SQLiteConnectionFlags flags, int index, ulong value)
		{
			SQLiteStatementHandle sqlite_stmt = stmt._sqlite_stmt;
			if ((flags & SQLiteConnectionFlags.LogBind) == SQLiteConnectionFlags.LogBind)
			{
				LogBind(sqlite_stmt, index, value);
			}
			SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_bind_uint64(sqlite_stmt, index, value);
			if (sQLiteErrorCode != SQLiteErrorCode.Ok)
			{
				throw new SQLiteException(sQLiteErrorCode, GetLastError());
			}
		}

		internal override void Bind_Text(SQLiteStatement stmt, SQLiteConnectionFlags flags, int index, string value)
		{
			SQLiteStatementHandle sqlite_stmt = stmt._sqlite_stmt;
			if ((flags & SQLiteConnectionFlags.LogBind) == SQLiteConnectionFlags.LogBind)
			{
				LogBind(sqlite_stmt, index, value);
			}
			byte[] array = SQLiteConvert.ToUTF8(value);
			if ((flags & SQLiteConnectionFlags.LogBind) == SQLiteConnectionFlags.LogBind)
			{
				LogBind(sqlite_stmt, index, array);
			}
			SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_bind_text(sqlite_stmt, index, array, array.Length - 1, (IntPtr)(-1));
			if (sQLiteErrorCode != SQLiteErrorCode.Ok)
			{
				throw new SQLiteException(sQLiteErrorCode, GetLastError());
			}
		}

		internal override void Bind_DateTime(SQLiteStatement stmt, SQLiteConnectionFlags flags, int index, DateTime dt)
		{
			SQLiteStatementHandle sqlite_stmt = stmt._sqlite_stmt;
			if ((flags & SQLiteConnectionFlags.LogBind) == SQLiteConnectionFlags.LogBind)
			{
				LogBind(sqlite_stmt, index, dt);
			}
			if ((flags & SQLiteConnectionFlags.BindDateTimeWithKind) == SQLiteConnectionFlags.BindDateTimeWithKind && _datetimeKind != DateTimeKind.Unspecified && dt.Kind != DateTimeKind.Unspecified && dt.Kind != _datetimeKind)
			{
				if (_datetimeKind == DateTimeKind.Utc)
				{
					dt = dt.ToUniversalTime();
				}
				else if (_datetimeKind == DateTimeKind.Local)
				{
					dt = dt.ToLocalTime();
				}
			}
			switch (_datetimeFormat)
			{
			case SQLiteDateFormats.Ticks:
			{
				long ticks = dt.Ticks;
				if ((flags & SQLiteConnectionFlags.LogBind) == SQLiteConnectionFlags.LogBind)
				{
					LogBind(sqlite_stmt, index, ticks);
				}
				SQLiteErrorCode sQLiteErrorCode4 = UnsafeNativeMethods.sqlite3_bind_int64(sqlite_stmt, index, ticks);
				if (sQLiteErrorCode4 != SQLiteErrorCode.Ok)
				{
					throw new SQLiteException(sQLiteErrorCode4, GetLastError());
				}
				break;
			}
			case SQLiteDateFormats.JulianDay:
			{
				double num = SQLiteConvert.ToJulianDay(dt);
				if ((flags & SQLiteConnectionFlags.LogBind) == SQLiteConnectionFlags.LogBind)
				{
					LogBind(sqlite_stmt, index, num);
				}
				SQLiteErrorCode sQLiteErrorCode2 = UnsafeNativeMethods.sqlite3_bind_double(sqlite_stmt, index, num);
				if (sQLiteErrorCode2 != SQLiteErrorCode.Ok)
				{
					throw new SQLiteException(sQLiteErrorCode2, GetLastError());
				}
				break;
			}
			case SQLiteDateFormats.UnixEpoch:
			{
				long num2 = Convert.ToInt64(dt.Subtract(SQLiteConvert.UnixEpoch).TotalSeconds);
				if ((flags & SQLiteConnectionFlags.LogBind) == SQLiteConnectionFlags.LogBind)
				{
					LogBind(sqlite_stmt, index, num2);
				}
				SQLiteErrorCode sQLiteErrorCode3 = UnsafeNativeMethods.sqlite3_bind_int64(sqlite_stmt, index, num2);
				if (sQLiteErrorCode3 != SQLiteErrorCode.Ok)
				{
					throw new SQLiteException(sQLiteErrorCode3, GetLastError());
				}
				break;
			}
			default:
			{
				byte[] array = ToUTF8(dt);
				if ((flags & SQLiteConnectionFlags.LogBind) == SQLiteConnectionFlags.LogBind)
				{
					LogBind(sqlite_stmt, index, array);
				}
				SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_bind_text(sqlite_stmt, index, array, array.Length - 1, (IntPtr)(-1));
				if (sQLiteErrorCode != SQLiteErrorCode.Ok)
				{
					throw new SQLiteException(sQLiteErrorCode, GetLastError());
				}
				break;
			}
			}
		}

		internal override void Bind_Blob(SQLiteStatement stmt, SQLiteConnectionFlags flags, int index, byte[] blobData)
		{
			SQLiteStatementHandle sqlite_stmt = stmt._sqlite_stmt;
			if ((flags & SQLiteConnectionFlags.LogBind) == SQLiteConnectionFlags.LogBind)
			{
				LogBind(sqlite_stmt, index, blobData);
			}
			SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_bind_blob(sqlite_stmt, index, blobData, blobData.Length, (IntPtr)(-1));
			if (sQLiteErrorCode != SQLiteErrorCode.Ok)
			{
				throw new SQLiteException(sQLiteErrorCode, GetLastError());
			}
		}

		internal override void Bind_Null(SQLiteStatement stmt, SQLiteConnectionFlags flags, int index)
		{
			SQLiteStatementHandle sqlite_stmt = stmt._sqlite_stmt;
			if ((flags & SQLiteConnectionFlags.LogBind) == SQLiteConnectionFlags.LogBind)
			{
				LogBind(sqlite_stmt, index);
			}
			SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_bind_null(sqlite_stmt, index);
			if (sQLiteErrorCode != SQLiteErrorCode.Ok)
			{
				throw new SQLiteException(sQLiteErrorCode, GetLastError());
			}
		}

		internal override int Bind_ParamCount(SQLiteStatement stmt, SQLiteConnectionFlags flags)
		{
			SQLiteStatementHandle sqlite_stmt = stmt._sqlite_stmt;
			int num = UnsafeNativeMethods.sqlite3_bind_parameter_count(sqlite_stmt);
			if ((flags & SQLiteConnectionFlags.LogBind) == SQLiteConnectionFlags.LogBind)
			{
				IntPtr intPtr = sqlite_stmt;
				SQLiteLog.LogMessage(string.Format(CultureInfo.CurrentCulture, "Statement {0} paramter count is {1}.", new object[2] { intPtr, num }));
			}
			return num;
		}

		internal override string Bind_ParamName(SQLiteStatement stmt, SQLiteConnectionFlags flags, int index)
		{
			SQLiteStatementHandle sqlite_stmt = stmt._sqlite_stmt;
			string text = SQLiteConvert.UTF8ToString(UnsafeNativeMethods.sqlite3_bind_parameter_name(sqlite_stmt, index), -1);
			if ((flags & SQLiteConnectionFlags.LogBind) == SQLiteConnectionFlags.LogBind)
			{
				IntPtr intPtr = sqlite_stmt;
				SQLiteLog.LogMessage(string.Format(CultureInfo.CurrentCulture, "Statement {0} paramter #{1} name is {{{2}}}.", new object[3] { intPtr, index, text }));
			}
			return text;
		}

		internal override int Bind_ParamIndex(SQLiteStatement stmt, SQLiteConnectionFlags flags, string paramName)
		{
			SQLiteStatementHandle sqlite_stmt = stmt._sqlite_stmt;
			int num = UnsafeNativeMethods.sqlite3_bind_parameter_index(sqlite_stmt, SQLiteConvert.ToUTF8(paramName));
			if ((flags & SQLiteConnectionFlags.LogBind) == SQLiteConnectionFlags.LogBind)
			{
				IntPtr intPtr = sqlite_stmt;
				SQLiteLog.LogMessage(string.Format(CultureInfo.CurrentCulture, "Statement {0} paramter index of name {{{1}}} is #{2}.", new object[3] { intPtr, paramName, num }));
			}
			return num;
		}

		internal override int ColumnCount(SQLiteStatement stmt)
		{
			return UnsafeNativeMethods.sqlite3_column_count(stmt._sqlite_stmt);
		}

		internal override string ColumnName(SQLiteStatement stmt, int index)
		{
			IntPtr intPtr = UnsafeNativeMethods.sqlite3_column_name(stmt._sqlite_stmt, index);
			if (intPtr == IntPtr.Zero)
			{
				throw new SQLiteException(SQLiteErrorCode.NoMem, GetLastError());
			}
			return SQLiteConvert.UTF8ToString(intPtr, -1);
		}

		internal override TypeAffinity ColumnAffinity(SQLiteStatement stmt, int index)
		{
			return UnsafeNativeMethods.sqlite3_column_type(stmt._sqlite_stmt, index);
		}

		internal override string ColumnType(SQLiteStatement stmt, int index, ref TypeAffinity nAffinity)
		{
			int nativestringlen = -1;
			IntPtr intPtr = UnsafeNativeMethods.sqlite3_column_decltype(stmt._sqlite_stmt, index);
			nAffinity = ColumnAffinity(stmt, index);
			if (intPtr != IntPtr.Zero)
			{
				return SQLiteConvert.UTF8ToString(intPtr, nativestringlen);
			}
			string[] typeDefinitions = stmt.TypeDefinitions;
			if (typeDefinitions != null && index < typeDefinitions.Length && typeDefinitions[index] != null)
			{
				return typeDefinitions[index];
			}
			return string.Empty;
		}

		internal override int ColumnIndex(SQLiteStatement stmt, string columnName)
		{
			int num = ColumnCount(stmt);
			for (int i = 0; i < num; i++)
			{
				if (string.Compare(columnName, ColumnName(stmt, i), StringComparison.OrdinalIgnoreCase) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		internal override string ColumnOriginalName(SQLiteStatement stmt, int index)
		{
			return SQLiteConvert.UTF8ToString(UnsafeNativeMethods.sqlite3_column_origin_name(stmt._sqlite_stmt, index), -1);
		}

		internal override string ColumnDatabaseName(SQLiteStatement stmt, int index)
		{
			return SQLiteConvert.UTF8ToString(UnsafeNativeMethods.sqlite3_column_database_name(stmt._sqlite_stmt, index), -1);
		}

		internal override string ColumnTableName(SQLiteStatement stmt, int index)
		{
			return SQLiteConvert.UTF8ToString(UnsafeNativeMethods.sqlite3_column_table_name(stmt._sqlite_stmt, index), -1);
		}

		internal override void ColumnMetaData(string dataBase, string table, string column, ref string dataType, ref string collateSequence, ref bool notNull, ref bool primaryKey, ref bool autoIncrement)
		{
			IntPtr ptrDataType = IntPtr.Zero;
			IntPtr ptrCollSeq = IntPtr.Zero;
			int notNull2 = 0;
			int primaryKey2 = 0;
			int autoInc = 0;
			int nativestringlen = -1;
			int nativestringlen2 = -1;
			SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_table_column_metadata(_sql, SQLiteConvert.ToUTF8(dataBase), SQLiteConvert.ToUTF8(table), SQLiteConvert.ToUTF8(column), ref ptrDataType, ref ptrCollSeq, ref notNull2, ref primaryKey2, ref autoInc);
			if (sQLiteErrorCode != SQLiteErrorCode.Ok)
			{
				throw new SQLiteException(sQLiteErrorCode, GetLastError());
			}
			dataType = SQLiteConvert.UTF8ToString(ptrDataType, nativestringlen);
			collateSequence = SQLiteConvert.UTF8ToString(ptrCollSeq, nativestringlen2);
			notNull = notNull2 == 1;
			primaryKey = primaryKey2 == 1;
			autoIncrement = autoInc == 1;
		}

		internal override double GetDouble(SQLiteStatement stmt, int index)
		{
			return UnsafeNativeMethods.sqlite3_column_double(stmt._sqlite_stmt, index);
		}

		internal override sbyte GetSByte(SQLiteStatement stmt, int index)
		{
			return (sbyte)(GetInt32(stmt, index) & 0xFF);
		}

		internal override byte GetByte(SQLiteStatement stmt, int index)
		{
			return (byte)(GetInt32(stmt, index) & 0xFF);
		}

		internal override short GetInt16(SQLiteStatement stmt, int index)
		{
			return (short)(GetInt32(stmt, index) & 0xFFFF);
		}

		internal override ushort GetUInt16(SQLiteStatement stmt, int index)
		{
			return (ushort)(GetInt32(stmt, index) & 0xFFFF);
		}

		internal override int GetInt32(SQLiteStatement stmt, int index)
		{
			return UnsafeNativeMethods.sqlite3_column_int(stmt._sqlite_stmt, index);
		}

		internal override uint GetUInt32(SQLiteStatement stmt, int index)
		{
			return (uint)GetInt32(stmt, index);
		}

		internal override long GetInt64(SQLiteStatement stmt, int index)
		{
			return UnsafeNativeMethods.sqlite3_column_int64(stmt._sqlite_stmt, index);
		}

		internal override ulong GetUInt64(SQLiteStatement stmt, int index)
		{
			return (ulong)GetInt64(stmt, index);
		}

		internal override string GetText(SQLiteStatement stmt, int index)
		{
			return SQLiteConvert.UTF8ToString(UnsafeNativeMethods.sqlite3_column_text(stmt._sqlite_stmt, index), UnsafeNativeMethods.sqlite3_column_bytes(stmt._sqlite_stmt, index));
		}

		internal override DateTime GetDateTime(SQLiteStatement stmt, int index)
		{
			if (_datetimeFormat == SQLiteDateFormats.Ticks)
			{
				return SQLiteConvert.ToDateTime(GetInt64(stmt, index), _datetimeKind);
			}
			if (_datetimeFormat == SQLiteDateFormats.JulianDay)
			{
				return SQLiteConvert.ToDateTime(GetDouble(stmt, index), _datetimeKind);
			}
			if (_datetimeFormat == SQLiteDateFormats.UnixEpoch)
			{
				return SQLiteConvert.ToDateTime(GetInt32(stmt, index), _datetimeKind);
			}
			return ToDateTime(UnsafeNativeMethods.sqlite3_column_text(stmt._sqlite_stmt, index), UnsafeNativeMethods.sqlite3_column_bytes(stmt._sqlite_stmt, index));
		}

		internal override long GetBytes(SQLiteStatement stmt, int index, int nDataOffset, byte[] bDest, int nStart, int nLength)
		{
			int num = UnsafeNativeMethods.sqlite3_column_bytes(stmt._sqlite_stmt, index);
			if (bDest == null)
			{
				return num;
			}
			int num2 = nLength;
			if (num2 + nStart > bDest.Length)
			{
				num2 = bDest.Length - nStart;
			}
			if (num2 + nDataOffset > num)
			{
				num2 = num - nDataOffset;
			}
			if (num2 > 0)
			{
				Marshal.Copy((IntPtr)(UnsafeNativeMethods.sqlite3_column_blob(stmt._sqlite_stmt, index).ToInt64() + nDataOffset), bDest, nStart, num2);
			}
			else
			{
				num2 = 0;
			}
			return num2;
		}

		internal override long GetChars(SQLiteStatement stmt, int index, int nDataOffset, char[] bDest, int nStart, int nLength)
		{
			int num = nLength;
			string text = GetText(stmt, index);
			int length = text.Length;
			if (bDest == null)
			{
				return length;
			}
			if (num + nStart > bDest.Length)
			{
				num = bDest.Length - nStart;
			}
			if (num + nDataOffset > length)
			{
				num = length - nDataOffset;
			}
			if (num > 0)
			{
				text.CopyTo(nDataOffset, bDest, nStart, num);
			}
			else
			{
				num = 0;
			}
			return num;
		}

		internal override bool IsNull(SQLiteStatement stmt, int index)
		{
			return ColumnAffinity(stmt, index) == TypeAffinity.Null;
		}

		internal override int AggregateCount(IntPtr context)
		{
			return UnsafeNativeMethods.sqlite3_aggregate_count(context);
		}

		internal override void CreateFunction(string strFunction, int nArgs, bool needCollSeq, SQLiteCallback func, SQLiteCallback funcstep, SQLiteFinalCallback funcfinal)
		{
			SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_create_function(_sql, SQLiteConvert.ToUTF8(strFunction), nArgs, 4, IntPtr.Zero, func, funcstep, funcfinal);
			if (sQLiteErrorCode == SQLiteErrorCode.Ok)
			{
				sQLiteErrorCode = UnsafeNativeMethods.sqlite3_create_function(_sql, SQLiteConvert.ToUTF8(strFunction), nArgs, 1, IntPtr.Zero, func, funcstep, funcfinal);
			}
			if (sQLiteErrorCode != SQLiteErrorCode.Ok)
			{
				throw new SQLiteException(sQLiteErrorCode, GetLastError());
			}
		}

		internal override void CreateCollation(string strCollation, SQLiteCollation func, SQLiteCollation func16)
		{
			SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_create_collation(_sql, SQLiteConvert.ToUTF8(strCollation), 2, IntPtr.Zero, func16);
			if (sQLiteErrorCode == SQLiteErrorCode.Ok)
			{
				sQLiteErrorCode = UnsafeNativeMethods.sqlite3_create_collation(_sql, SQLiteConvert.ToUTF8(strCollation), 1, IntPtr.Zero, func);
			}
			if (sQLiteErrorCode != SQLiteErrorCode.Ok)
			{
				throw new SQLiteException(sQLiteErrorCode, GetLastError());
			}
		}

		internal override int ContextCollateCompare(CollationEncodingEnum enc, IntPtr context, string s1, string s2)
		{
			throw new NotImplementedException();
		}

		internal override int ContextCollateCompare(CollationEncodingEnum enc, IntPtr context, char[] c1, char[] c2)
		{
			throw new NotImplementedException();
		}

		internal override CollationSequence GetCollationSequence(SQLiteFunction func, IntPtr context)
		{
			throw new NotImplementedException();
		}

		internal override long GetParamValueBytes(IntPtr p, int nDataOffset, byte[] bDest, int nStart, int nLength)
		{
			int num = UnsafeNativeMethods.sqlite3_value_bytes(p);
			if (bDest == null)
			{
				return num;
			}
			int num2 = nLength;
			if (num2 + nStart > bDest.Length)
			{
				num2 = bDest.Length - nStart;
			}
			if (num2 + nDataOffset > num)
			{
				num2 = num - nDataOffset;
			}
			if (num2 > 0)
			{
				Marshal.Copy((IntPtr)(UnsafeNativeMethods.sqlite3_value_blob(p).ToInt64() + nDataOffset), bDest, nStart, num2);
			}
			else
			{
				num2 = 0;
			}
			return num2;
		}

		internal override double GetParamValueDouble(IntPtr ptr)
		{
			return UnsafeNativeMethods.sqlite3_value_double(ptr);
		}

		internal override int GetParamValueInt32(IntPtr ptr)
		{
			return UnsafeNativeMethods.sqlite3_value_int(ptr);
		}

		internal override long GetParamValueInt64(IntPtr ptr)
		{
			return UnsafeNativeMethods.sqlite3_value_int64(ptr);
		}

		internal override string GetParamValueText(IntPtr ptr)
		{
			return SQLiteConvert.UTF8ToString(UnsafeNativeMethods.sqlite3_value_text(ptr), UnsafeNativeMethods.sqlite3_value_bytes(ptr));
		}

		internal override TypeAffinity GetParamValueType(IntPtr ptr)
		{
			return UnsafeNativeMethods.sqlite3_value_type(ptr);
		}

		internal override void ReturnBlob(IntPtr context, byte[] value)
		{
			UnsafeNativeMethods.sqlite3_result_blob(context, value, value.Length, (IntPtr)(-1));
		}

		internal override void ReturnDouble(IntPtr context, double value)
		{
			UnsafeNativeMethods.sqlite3_result_double(context, value);
		}

		internal override void ReturnError(IntPtr context, string value)
		{
			UnsafeNativeMethods.sqlite3_result_error(context, SQLiteConvert.ToUTF8(value), value.Length);
		}

		internal override void ReturnInt32(IntPtr context, int value)
		{
			UnsafeNativeMethods.sqlite3_result_int(context, value);
		}

		internal override void ReturnInt64(IntPtr context, long value)
		{
			UnsafeNativeMethods.sqlite3_result_int64(context, value);
		}

		internal override void ReturnNull(IntPtr context)
		{
			UnsafeNativeMethods.sqlite3_result_null(context);
		}

		internal override void ReturnText(IntPtr context, string value)
		{
			byte[] array = SQLiteConvert.ToUTF8(value);
			UnsafeNativeMethods.sqlite3_result_text(context, SQLiteConvert.ToUTF8(value), array.Length - 1, (IntPtr)(-1));
		}

		internal override void CreateModule(SQLiteModule module, SQLiteConnectionFlags flags)
		{
			if (module == null)
			{
				throw new ArgumentNullException("module");
			}
			if ((flags & SQLiteConnectionFlags.NoLogModule) != SQLiteConnectionFlags.NoLogModule)
			{
				module.LogErrors = (flags & SQLiteConnectionFlags.LogModuleError) == SQLiteConnectionFlags.LogModuleError;
				module.LogExceptions = (flags & SQLiteConnectionFlags.LogModuleException) == SQLiteConnectionFlags.LogModuleException;
			}
			if (_sql == null)
			{
				throw new SQLiteException("connection has an invalid handle");
			}
			SetLoadExtension(bOnOff: true);
			LoadExtension("sqlite3", "sqlite3_vtshim_init");
			if (module.CreateDisposableModule(_sql))
			{
				if (_modules == null)
				{
					_modules = new Dictionary<string, SQLiteModule>();
				}
				_modules.Add(module.Name, module);
				if (_usePool)
				{
					_usePool = false;
				}
				return;
			}
			throw new SQLiteException(GetLastError());
		}

		internal override void DisposeModule(SQLiteModule module, SQLiteConnectionFlags flags)
		{
			if (module == null)
			{
				throw new ArgumentNullException("module");
			}
			module.Dispose();
		}

		internal override IntPtr AggregateContext(IntPtr context)
		{
			return UnsafeNativeMethods.sqlite3_aggregate_context(context, 1);
		}

		internal override SQLiteErrorCode DeclareVirtualTable(SQLiteModule module, string strSql, ref string error)
		{
			if (_sql == null)
			{
				error = "connection has an invalid handle";
				return SQLiteErrorCode.Error;
			}
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = SQLiteString.Utf8IntPtrFromString(strSql);
				SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_declare_vtab(_sql, intPtr);
				if (sQLiteErrorCode == SQLiteErrorCode.Ok && module != null)
				{
					module.Declared = true;
				}
				if (sQLiteErrorCode != SQLiteErrorCode.Ok)
				{
					error = GetLastError();
				}
				return sQLiteErrorCode;
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					SQLiteMemory.Free(intPtr);
					intPtr = IntPtr.Zero;
				}
			}
		}

		internal override SQLiteErrorCode DeclareVirtualFunction(SQLiteModule module, int argumentCount, string name, ref string error)
		{
			if (_sql == null)
			{
				error = "connection has an invalid handle";
				return SQLiteErrorCode.Error;
			}
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = SQLiteString.Utf8IntPtrFromString(name);
				SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_overload_function(_sql, intPtr, argumentCount);
				if (sQLiteErrorCode != SQLiteErrorCode.Ok)
				{
					error = GetLastError();
				}
				return sQLiteErrorCode;
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					SQLiteMemory.Free(intPtr);
					intPtr = IntPtr.Zero;
				}
			}
		}

		internal override void SetLoadExtension(bool bOnOff)
		{
			SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_enable_load_extension(_sql, bOnOff ? (-1) : 0);
			if (sQLiteErrorCode != SQLiteErrorCode.Ok)
			{
				throw new SQLiteException(sQLiteErrorCode, GetLastError());
			}
		}

		internal override void LoadExtension(string fileName, string procName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			IntPtr pError = IntPtr.Zero;
			try
			{
				byte[] bytes = Encoding.UTF8.GetBytes(fileName + '\0');
				byte[] procName2 = null;
				if (procName != null)
				{
					procName2 = Encoding.UTF8.GetBytes(procName + '\0');
				}
				SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_load_extension(_sql, bytes, procName2, ref pError);
				if (sQLiteErrorCode != SQLiteErrorCode.Ok)
				{
					throw new SQLiteException(sQLiteErrorCode, SQLiteConvert.UTF8ToString(pError, -1));
				}
			}
			finally
			{
				if (pError != IntPtr.Zero)
				{
					UnsafeNativeMethods.sqlite3_free(pError);
					pError = IntPtr.Zero;
				}
			}
		}

		internal override void SetExtendedResultCodes(bool bOnOff)
		{
			SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_extended_result_codes(_sql, bOnOff ? (-1) : 0);
			if (sQLiteErrorCode != SQLiteErrorCode.Ok)
			{
				throw new SQLiteException(sQLiteErrorCode, GetLastError());
			}
		}

		internal override SQLiteErrorCode ResultCode()
		{
			return UnsafeNativeMethods.sqlite3_errcode(_sql);
		}

		internal override SQLiteErrorCode ExtendedResultCode()
		{
			return UnsafeNativeMethods.sqlite3_extended_errcode(_sql);
		}

		internal override void LogMessage(SQLiteErrorCode iErrCode, string zMessage)
		{
			StaticLogMessage(iErrCode, zMessage);
		}

		internal static void StaticLogMessage(SQLiteErrorCode iErrCode, string zMessage)
		{
			UnsafeNativeMethods.sqlite3_log(iErrCode, SQLiteConvert.ToUTF8(zMessage));
		}

		internal override void SetPassword(byte[] passwordBytes)
		{
			SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_key(_sql, passwordBytes, passwordBytes.Length);
			if (sQLiteErrorCode != SQLiteErrorCode.Ok)
			{
				throw new SQLiteException(sQLiteErrorCode, GetLastError());
			}
		}

		internal override void ChangePassword(byte[] newPasswordBytes)
		{
			SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_rekey(_sql, newPasswordBytes, (newPasswordBytes != null) ? newPasswordBytes.Length : 0);
			if (sQLiteErrorCode != SQLiteErrorCode.Ok)
			{
				throw new SQLiteException(sQLiteErrorCode, GetLastError());
			}
		}

		internal override void SetAuthorizerHook(SQLiteAuthorizerCallback func)
		{
			UnsafeNativeMethods.sqlite3_set_authorizer(_sql, func, IntPtr.Zero);
		}

		internal override void SetUpdateHook(SQLiteUpdateCallback func)
		{
			UnsafeNativeMethods.sqlite3_update_hook(_sql, func, IntPtr.Zero);
		}

		internal override void SetCommitHook(SQLiteCommitCallback func)
		{
			UnsafeNativeMethods.sqlite3_commit_hook(_sql, func, IntPtr.Zero);
		}

		internal override void SetTraceCallback(SQLiteTraceCallback func)
		{
			UnsafeNativeMethods.sqlite3_trace(_sql, func, IntPtr.Zero);
		}

		internal override void SetRollbackHook(SQLiteRollbackCallback func)
		{
			UnsafeNativeMethods.sqlite3_rollback_hook(_sql, func, IntPtr.Zero);
		}

		internal override SQLiteErrorCode SetLogCallback(SQLiteLogCallback func)
		{
			return UnsafeNativeMethods.sqlite3_config_log(SQLiteConfigOpsEnum.SQLITE_CONFIG_LOG, func, IntPtr.Zero);
		}

		internal override SQLiteBackup InitializeBackup(SQLiteConnection destCnn, string destName, string sourceName)
		{
			if (destCnn == null)
			{
				throw new ArgumentNullException("destCnn");
			}
			if (destName == null)
			{
				throw new ArgumentNullException("destName");
			}
			if (sourceName == null)
			{
				throw new ArgumentNullException("sourceName");
			}
			if (!(destCnn._sql is SQLite3 { _sql: var sql }))
			{
				throw new ArgumentException("Destination connection has no wrapper.", "destCnn");
			}
			if (sql == null)
			{
				throw new ArgumentException("Destination connection has an invalid handle.", "destCnn");
			}
			SQLiteConnectionHandle sql2 = _sql;
			if (sql2 == null)
			{
				throw new InvalidOperationException("Source connection has an invalid handle.");
			}
			byte[] zDestName = SQLiteConvert.ToUTF8(destName);
			byte[] zSourceName = SQLiteConvert.ToUTF8(sourceName);
			SQLiteBackupHandle sQLiteBackupHandle = null;
			try
			{
			}
			finally
			{
				IntPtr intPtr = UnsafeNativeMethods.sqlite3_backup_init(sql, zDestName, sql2, zSourceName);
				if (intPtr == IntPtr.Zero)
				{
					SQLiteErrorCode sQLiteErrorCode = ResultCode();
					if (sQLiteErrorCode != SQLiteErrorCode.Ok)
					{
						throw new SQLiteException(sQLiteErrorCode, GetLastError());
					}
					throw new SQLiteException("failed to initialize backup");
				}
				sQLiteBackupHandle = new SQLiteBackupHandle(sql, intPtr);
			}
			SQLiteConnection.OnChanged(null, new ConnectionEventArgs(SQLiteConnectionEventType.NewCriticalHandle, null, null, null, null, sQLiteBackupHandle, null, new object[3] { destCnn, destName, sourceName }));
			return new SQLiteBackup(this, sQLiteBackupHandle, sql, zDestName, sql2, zSourceName);
		}

		internal override bool StepBackup(SQLiteBackup backup, int nPage, ref bool retry)
		{
			retry = false;
			if (backup == null)
			{
				throw new ArgumentNullException("backup");
			}
			SQLiteBackupHandle sqlite_backup = backup._sqlite_backup;
			if (sqlite_backup == null)
			{
				throw new InvalidOperationException("Backup object has an invalid handle.");
			}
			IntPtr intPtr = sqlite_backup;
			if (intPtr == IntPtr.Zero)
			{
				throw new InvalidOperationException("Backup object has an invalid handle pointer.");
			}
			SQLiteErrorCode sQLiteErrorCode = (backup._stepResult = UnsafeNativeMethods.sqlite3_backup_step(intPtr, nPage));
			switch (sQLiteErrorCode)
			{
			case SQLiteErrorCode.Ok:
				return true;
			case SQLiteErrorCode.Busy:
				retry = true;
				return true;
			case SQLiteErrorCode.Locked:
				retry = true;
				return true;
			case SQLiteErrorCode.Done:
				return false;
			default:
				throw new SQLiteException(sQLiteErrorCode, GetLastError());
			}
		}

		internal override int RemainingBackup(SQLiteBackup backup)
		{
			if (backup == null)
			{
				throw new ArgumentNullException("backup");
			}
			SQLiteBackupHandle sqlite_backup = backup._sqlite_backup;
			if (sqlite_backup == null)
			{
				throw new InvalidOperationException("Backup object has an invalid handle.");
			}
			IntPtr intPtr = sqlite_backup;
			if (intPtr == IntPtr.Zero)
			{
				throw new InvalidOperationException("Backup object has an invalid handle pointer.");
			}
			return UnsafeNativeMethods.sqlite3_backup_remaining(intPtr);
		}

		internal override int PageCountBackup(SQLiteBackup backup)
		{
			if (backup == null)
			{
				throw new ArgumentNullException("backup");
			}
			SQLiteBackupHandle sqlite_backup = backup._sqlite_backup;
			if (sqlite_backup == null)
			{
				throw new InvalidOperationException("Backup object has an invalid handle.");
			}
			IntPtr intPtr = sqlite_backup;
			if (intPtr == IntPtr.Zero)
			{
				throw new InvalidOperationException("Backup object has an invalid handle pointer.");
			}
			return UnsafeNativeMethods.sqlite3_backup_pagecount(intPtr);
		}

		internal override void FinishBackup(SQLiteBackup backup)
		{
			if (backup == null)
			{
				throw new ArgumentNullException("backup");
			}
			SQLiteBackupHandle sqlite_backup = backup._sqlite_backup;
			if (sqlite_backup == null)
			{
				throw new InvalidOperationException("Backup object has an invalid handle.");
			}
			IntPtr intPtr = sqlite_backup;
			if (intPtr == IntPtr.Zero)
			{
				throw new InvalidOperationException("Backup object has an invalid handle pointer.");
			}
			SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_backup_finish(intPtr);
			sqlite_backup.SetHandleAsInvalid();
			if (sQLiteErrorCode != SQLiteErrorCode.Ok && sQLiteErrorCode != backup._stepResult)
			{
				throw new SQLiteException(sQLiteErrorCode, GetLastError());
			}
		}

		internal override bool IsInitialized()
		{
			return StaticIsInitialized();
		}

		internal static bool StaticIsInitialized()
		{
			lock (syncRoot)
			{
				bool enabled = SQLiteLog.Enabled;
				SQLiteLog.Enabled = false;
				try
				{
					SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_config_none(SQLiteConfigOpsEnum.SQLITE_CONFIG_NONE);
					return sQLiteErrorCode == SQLiteErrorCode.Misuse;
				}
				finally
				{
					SQLiteLog.Enabled = enabled;
				}
			}
		}

		internal override object GetValue(SQLiteStatement stmt, SQLiteConnectionFlags flags, int index, SQLiteType typ)
		{
			TypeAffinity typeAffinity = typ.Affinity;
			if (typeAffinity == TypeAffinity.Null)
			{
				return DBNull.Value;
			}
			Type type = null;
			if (typ.Type != DbType.Object)
			{
				type = SQLiteConvert.SQLiteTypeToType(typ);
				typeAffinity = SQLiteConvert.TypeToAffinity(type);
			}
			if ((flags & SQLiteConnectionFlags.GetAllAsText) == SQLiteConnectionFlags.GetAllAsText)
			{
				return GetText(stmt, index);
			}
			switch (typeAffinity)
			{
			case TypeAffinity.Blob:
			{
				if (typ.Type == DbType.Guid && typ.Affinity == TypeAffinity.Text)
				{
					return new Guid(GetText(stmt, index));
				}
				int num = (int)GetBytes(stmt, index, 0, null, 0, 0);
				byte[] array = new byte[num];
				GetBytes(stmt, index, 0, array, 0, num);
				if (typ.Type == DbType.Guid && num == 16)
				{
					return new Guid(array);
				}
				return array;
			}
			case TypeAffinity.DateTime:
				return GetDateTime(stmt, index);
			case TypeAffinity.Double:
				if ((object)type == null)
				{
					return GetDouble(stmt, index);
				}
				return Convert.ChangeType(GetDouble(stmt, index), type, null);
			case TypeAffinity.Int64:
				if ((object)type == null)
				{
					return GetInt64(stmt, index);
				}
				if ((object)type == typeof(sbyte))
				{
					return GetSByte(stmt, index);
				}
				if ((object)type == typeof(byte))
				{
					return GetByte(stmt, index);
				}
				if ((object)type == typeof(short))
				{
					return GetInt16(stmt, index);
				}
				if ((object)type == typeof(ushort))
				{
					return GetUInt16(stmt, index);
				}
				if ((object)type == typeof(int))
				{
					return GetInt32(stmt, index);
				}
				if ((object)type == typeof(uint))
				{
					return GetUInt32(stmt, index);
				}
				if ((object)type == typeof(ulong))
				{
					return GetUInt64(stmt, index);
				}
				return Convert.ChangeType(GetInt64(stmt, index), type, null);
			default:
				return GetText(stmt, index);
			}
		}

		internal override int GetCursorForTable(SQLiteStatement stmt, int db, int rootPage)
		{
			return -1;
		}

		internal override long GetRowIdForCursor(SQLiteStatement stmt, int cursor)
		{
			return 0L;
		}

		internal override void GetIndexColumnExtendedInfo(string database, string index, string column, ref int sortMode, ref int onError, ref string collationSequence)
		{
			sortMode = 0;
			onError = 2;
			collationSequence = "BINARY";
		}

		internal override SQLiteErrorCode FileControl(string zDbName, int op, IntPtr pArg)
		{
			return UnsafeNativeMethods.sqlite3_file_control(_sql, (zDbName != null) ? SQLiteConvert.ToUTF8(zDbName) : null, op, pArg);
		}
	}
	internal sealed class SQLite3_UTF16 : SQLite3
	{
		private bool disposed;

		internal SQLite3_UTF16(SQLiteDateFormats fmt, DateTimeKind kind, string fmtString, IntPtr db, string fileName, bool ownHandle)
			: base(fmt, kind, fmtString, db, fileName, ownHandle)
		{
		}

		private void CheckDisposed()
		{
			if (disposed)
			{
				throw new ObjectDisposedException(typeof(SQLite3_UTF16).Name);
			}
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				_ = disposed;
			}
			finally
			{
				base.Dispose(disposing);
				disposed = true;
			}
		}

		public override string ToString(IntPtr b, int nbytelen)
		{
			CheckDisposed();
			return UTF16ToString(b, nbytelen);
		}

		public static string UTF16ToString(IntPtr b, int nbytelen)
		{
			if (nbytelen == 0 || b == IntPtr.Zero)
			{
				return "";
			}
			if (nbytelen == -1)
			{
				return Marshal.PtrToStringUni(b);
			}
			return Marshal.PtrToStringUni(b, nbytelen / 2);
		}

		internal override void Open(string strFilename, SQLiteConnectionFlags connectionFlags, SQLiteOpenFlagsEnum openFlags, int maxPoolSize, bool usePool)
		{
			if (_sql != null)
			{
				Close(canThrow: true);
			}
			if (_sql != null)
			{
				throw new SQLiteException("connection handle is still active");
			}
			_usePool = usePool;
			_fileName = strFilename;
			if (usePool)
			{
				_sql = SQLiteConnectionPool.Remove(strFilename, maxPoolSize, out _poolVersion);
			}
			if (_sql == null)
			{
				try
				{
				}
				finally
				{
					IntPtr db = IntPtr.Zero;
					if ((openFlags & SQLiteOpenFlagsEnum.Create) != SQLiteOpenFlagsEnum.Create && !File.Exists(strFilename))
					{
						throw new SQLiteException(SQLiteErrorCode.CantOpen, strFilename);
					}
					SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_open16(strFilename, ref db);
					if (sQLiteErrorCode != SQLiteErrorCode.Ok)
					{
						throw new SQLiteException(sQLiteErrorCode, null);
					}
					_sql = new SQLiteConnectionHandle(db, ownHandle: true);
				}
				lock (_sql)
				{
				}
				SQLiteConnection.OnChanged(null, new ConnectionEventArgs(SQLiteConnectionEventType.NewCriticalHandle, null, null, null, null, _sql, strFilename, new object[5] { strFilename, connectionFlags, openFlags, maxPoolSize, usePool }));
			}
			if ((connectionFlags & SQLiteConnectionFlags.NoBindFunctions) != SQLiteConnectionFlags.NoBindFunctions)
			{
				if (_functions == null)
				{
					_functions = new List<SQLiteFunction>();
				}
				_functions.AddRange(new List<SQLiteFunction>(SQLiteFunction.BindFunctions(this, connectionFlags)));
			}
			SetTimeout(0);
			GC.KeepAlive(_sql);
		}

		internal override void Bind_DateTime(SQLiteStatement stmt, SQLiteConnectionFlags flags, int index, DateTime dt)
		{
			switch (_datetimeFormat)
			{
			case SQLiteDateFormats.Ticks:
			case SQLiteDateFormats.JulianDay:
			case SQLiteDateFormats.UnixEpoch:
				base.Bind_DateTime(stmt, flags, index, dt);
				return;
			}
			if ((flags & SQLiteConnectionFlags.LogBind) == SQLiteConnectionFlags.LogBind)
			{
				SQLiteStatementHandle handle = stmt?._sqlite_stmt;
				SQLite3.LogBind(handle, index, dt);
			}
			Bind_Text(stmt, flags, index, ToString(dt));
		}

		internal override void Bind_Text(SQLiteStatement stmt, SQLiteConnectionFlags flags, int index, string value)
		{
			SQLiteStatementHandle sqlite_stmt = stmt._sqlite_stmt;
			if ((flags & SQLiteConnectionFlags.LogBind) == SQLiteConnectionFlags.LogBind)
			{
				SQLite3.LogBind(sqlite_stmt, index, value);
			}
			SQLiteErrorCode sQLiteErrorCode = UnsafeNativeMethods.sqlite3_bind_text16(sqlite_stmt, index, value, value.Length * 2, (IntPtr)(-1));
			if (sQLiteErrorCode != SQLiteErrorCode.Ok)
			{
				throw new SQLiteException(sQLiteErrorCode, GetLastError());
			}
		}

		internal override DateTime GetDateTime(SQLiteStatement stmt, int index)
		{
			if (_datetimeFormat == SQLiteDateFormats.Ticks)
			{
				return SQLiteConvert.ToDateTime(GetInt64(stmt, index), _datetimeKind);
			}
			if (_datetimeFormat == SQLiteDateFormats.JulianDay)
			{
				return SQLiteConvert.ToDateTime(GetDouble(stmt, index), _datetimeKind);
			}
			if (_datetimeFormat == SQLiteDateFormats.UnixEpoch)
			{
				return SQLiteConvert.ToDateTime(GetInt32(stmt, index), _datetimeKind);
			}
			return ToDateTime(GetText(stmt, index));
		}

		internal override string ColumnName(SQLiteStatement stmt, int index)
		{
			IntPtr intPtr = UnsafeNativeMethods.sqlite3_column_name16(stmt._sqlite_stmt, index);
			if (intPtr == IntPtr.Zero)
			{
				throw new SQLiteException(SQLiteErrorCode.NoMem, GetLastError());
			}
			return UTF16ToString(intPtr, -1);
		}

		internal override string GetText(SQLiteStatement stmt, int index)
		{
			return UTF16ToString(UnsafeNativeMethods.sqlite3_column_text16(stmt._sqlite_stmt, index), UnsafeNativeMethods.sqlite3_column_bytes16(stmt._sqlite_stmt, index));
		}

		internal override string ColumnOriginalName(SQLiteStatement stmt, int index)
		{
			return UTF16ToString(UnsafeNativeMethods.sqlite3_column_origin_name16(stmt._sqlite_stmt, index), -1);
		}

		internal override string ColumnDatabaseName(SQLiteStatement stmt, int index)
		{
			return UTF16ToString(UnsafeNativeMethods.sqlite3_column_database_name16(stmt._sqlite_stmt, index), -1);
		}

		internal override string ColumnTableName(SQLiteStatement stmt, int index)
		{
			return UTF16ToString(UnsafeNativeMethods.sqlite3_column_table_name16(stmt._sqlite_stmt, index), -1);
		}

		internal override string GetParamValueText(IntPtr ptr)
		{
			return UTF16ToString(UnsafeNativeMethods.sqlite3_value_text16(ptr), UnsafeNativeMethods.sqlite3_value_bytes16(ptr));
		}

		internal override void ReturnError(IntPtr context, string value)
		{
			UnsafeNativeMethods.sqlite3_result_error16(context, value, value.Length * 2);
		}

		internal override void ReturnText(IntPtr context, string value)
		{
			UnsafeNativeMethods.sqlite3_result_text16(context, value, value.Length * 2, (IntPtr)(-1));
		}
	}
	internal sealed class SQLiteBackup : IDisposable
	{
		internal SQLiteBase _sql;

		internal SQLiteBackupHandle _sqlite_backup;

		internal IntPtr _destDb;

		internal byte[] _zDestName;

		internal IntPtr _sourceDb;

		internal byte[] _zSourceName;

		internal SQLiteErrorCode _stepResult;

		private bool disposed;

		internal SQLiteBackup(SQLiteBase sqlbase, SQLiteBackupHandle backup, IntPtr destDb, byte[] zDestName, IntPtr sourceDb, byte[] zSourceName)
		{
			_sql = sqlbase;
			_sqlite_backup = backup;
			_destDb = destDb;
			_zDestName = zDestName;
			_sourceDb = sourceDb;
			_zSourceName = zSourceName;
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		private void CheckDisposed()
		{
			if (disposed)
			{
				throw new ObjectDisposedException(typeof(SQLiteBackup).Name);
			}
		}

		private void Dispose(bool disposing)
		{
			if (disposed)
			{
				return;
			}
			if (disposing)
			{
				if (_sqlite_backup != null)
				{
					_sqlite_backup.Dispose();
					_sqlite_backup = null;
				}
				_zSourceName = null;
				_sourceDb = IntPtr.Zero;
				_zDestName = null;
				_destDb = IntPtr.Zero;
				_sql = null;
			}
			disposed = true;
		}

		~SQLiteBackup()
		{
			Dispose(disposing: false);
		}
	}
	public interface ISQLiteSchemaExtensions
	{
		void BuildTempSchema(SQLiteConnection connection);
	}
	[Flags]
	internal enum SQLiteOpenFlagsEnum
	{
		None = 0,
		ReadOnly = 1,
		ReadWrite = 2,
		Create = 4,
		Uri = 0x40,
		SharedCache = 0x1000000,
		Default = 6
	}
	[Flags]
	public enum SQLiteConnectionFlags
	{
		None = 0,
		LogPrepare = 1,
		LogPreBind = 2,
		LogBind = 4,
		LogCallbackException = 8,
		LogBackup = 0x10,
		NoExtensionFunctions = 0x20,
		BindUInt32AsInt64 = 0x40,
		BindAllAsText = 0x80,
		GetAllAsText = 0x100,
		NoLoadExtension = 0x200,
		NoCreateModule = 0x400,
		NoBindFunctions = 0x800,
		NoLogModule = 0x1000,
		LogModuleError = 0x2000,
		LogModuleException = 0x4000,
		TraceWarning = 0x8000,
		ConvertInvariantText = 0x10000,
		BindInvariantText = 0x20000,
		NoConnectionPool = 0x40000,
		UseConnectionPool = 0x80000,
		UseConnectionTypes = 0x100000,
		NoGlobalTypes = 0x200000,
		StickyHasRows = 0x400000,
		StrictEnlistment = 0x800000,
		MapIsolationLevels = 0x1000000,
		DetectTextAffinity = 0x2000000,
		DetectStringType = 0x4000000,
		NoConvertSettings = 0x8000000,
		BindDateTimeWithKind = 0x10000000,
		BindAndGetAllAsText = 0x180,
		ConvertAndBindInvariantText = 0x30000,
		BindAndGetAllAsInvariantText = 0x20180,
		ConvertAndBindAndGetAllAsInvariantText = 0x30180,
		LogAll = 0x601F,
		Default = 0x4008,
		DefaultAndLogAll = 0x601F
	}
	internal enum SQLiteConfigOpsEnum
	{
		SQLITE_CONFIG_NONE = 0,
		SQLITE_CONFIG_SINGLETHREAD = 1,
		SQLITE_CONFIG_MULTITHREAD = 2,
		SQLITE_CONFIG_SERIALIZED = 3,
		SQLITE_CONFIG_MALLOC = 4,
		SQLITE_CONFIG_GETMALLOC = 5,
		SQLITE_CONFIG_SCRATCH = 6,
		SQLITE_CONFIG_PAGECACHE = 7,
		SQLITE_CONFIG_HEAP = 8,
		SQLITE_CONFIG_MEMSTATUS = 9,
		SQLITE_CONFIG_MUTEX = 10,
		SQLITE_CONFIG_GETMUTEX = 11,
		SQLITE_CONFIG_LOOKASIDE = 13,
		SQLITE_CONFIG_PCACHE = 14,
		SQLITE_CONFIG_GETPCACHE = 15,
		SQLITE_CONFIG_LOG = 16,
		SQLITE_CONFIG_URI = 17,
		SQLITE_CONFIG_PCACHE2 = 18,
		SQLITE_CONFIG_GETPCACHE2 = 19,
		SQLITE_CONFIG_COVERING_INDEX_SCAN = 20,
		SQLITE_CONFIG_SQLLOG = 21,
		SQLITE_CONFIG_MMAP_SIZE = 22,
		SQLITE_CONFIG_WIN32_HEAPSIZE = 23
	}
	[Designer("SQLite.Designer.SQLiteCommandDesigner, SQLite.Designer, Version=1.0.97.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139")]
	[ToolboxItem(true)]
	public sealed class SQLiteCommand : DbCommand, ICloneable
	{
		private static readonly string DefaultConnectionString = "Data Source=:memory:;";

		private string _commandText;

		private SQLiteConnection _cnn;

		private int _version;

		private WeakReference _activeReader;

		internal int _commandTimeout;

		private bool _designTimeVisible;

		private UpdateRowSource _updateRowSource;

		private SQLiteParameterCollection _parameterCollection;

		internal List<SQLiteStatement> _statementList;

		internal string _remainingText;

		private SQLiteTransaction _transaction;

		private bool disposed;

		[RefreshProperties(RefreshProperties.All)]
		[Editor("Microsoft.VSDesigner.Data.SQL.Design.SqlCommandTextEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public override string CommandText
		{
			get
			{
				CheckDisposed();
				return _commandText;
			}
			set
			{
				CheckDisposed();
				if (!(_commandText == value))
				{
					if (_activeReader != null && _activeReader.IsAlive)
					{
						throw new InvalidOperationException("Cannot set CommandText while a DataReader is active");
					}
					ClearCommands();
					_commandText = value;
					_ = _cnn;
				}
			}
		}

		[DefaultValue(30)]
		public override int CommandTimeout
		{
			get
			{
				CheckDisposed();
				return _commandTimeout;
			}
			set
			{
				CheckDisposed();
				_commandTimeout = value;
			}
		}

		[DefaultValue(CommandType.Text)]
		[RefreshProperties(RefreshProperties.All)]
		public override CommandType CommandType
		{
			get
			{
				CheckDisposed();
				return CommandType.Text;
			}
			set
			{
				CheckDisposed();
				if (value != CommandType.Text)
				{
					throw new NotSupportedException();
				}
			}
		}

		[DefaultValue(null)]
		[Editor("Microsoft.VSDesigner.Data.Design.DbConnectionEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new SQLiteConnection Connection
		{
			get
			{
				CheckDisposed();
				return _cnn;
			}
			set
			{
				CheckDisposed();
				if (_activeReader != null && _activeReader.IsAlive)
				{
					throw new InvalidOperationException("Cannot set Connection while a DataReader is active");
				}
				if (_cnn != null)
				{
					ClearCommands();
				}
				_cnn = value;
				if (_cnn != null)
				{
					_version = _cnn._version;
				}
			}
		}

		protected override DbConnection DbConnection
		{
			get
			{
				return Connection;
			}
			set
			{
				Connection = (SQLiteConnection)value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new SQLiteParameterCollection Parameters
		{
			get
			{
				CheckDisposed();
				return _parameterCollection;
			}
		}

		protected override DbParameterCollection DbParameterCollection => Parameters;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new SQLiteTransaction Transaction
		{
			get
			{
				CheckDisposed();
				return _transaction;
			}
			set
			{
				CheckDisposed();
				if (_cnn != null)
				{
					if (_activeReader != null && _activeReader.IsAlive)
					{
						throw new InvalidOperationException("Cannot set Transaction while a DataReader is active");
					}
					if (value != null && value._cnn != _cnn)
					{
						throw new ArgumentException("Transaction is not associated with the command's connection");
					}
					_transaction = value;
				}
				else
				{
					if (value != null)
					{
						Connection = value.Connection;
					}
					_transaction = value;
				}
			}
		}

		protected override DbTransaction DbTransaction
		{
			get
			{
				return Transaction;
			}
			set
			{
				Transaction = (SQLiteTransaction)value;
			}
		}

		[DefaultValue(UpdateRowSource.None)]
		public override UpdateRowSource UpdatedRowSource
		{
			get
			{
				CheckDisposed();
				return _updateRowSource;
			}
			set
			{
				CheckDisposed();
				_updateRowSource = value;
			}
		}

		[DefaultValue(true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignOnly(true)]
		[Browsable(false)]
		public override bool DesignTimeVisible
		{
			get
			{
				CheckDisposed();
				return _designTimeVisible;
			}
			set
			{
				CheckDisposed();
				_designTimeVisible = value;
				TypeDescriptor.Refresh(this);
			}
		}

		public SQLiteCommand()
			: this(null, null)
		{
		}

		public SQLiteCommand(string commandText)
			: this(commandText, null, null)
		{
		}

		public SQLiteCommand(string commandText, SQLiteConnection connection)
			: this(commandText, connection, null)
		{
		}

		public SQLiteCommand(SQLiteConnection connection)
			: this(null, connection, null)
		{
		}

		private SQLiteCommand(SQLiteCommand source)
			: this(source.CommandText, source.Connection, source.Transaction)
		{
			CommandTimeout = source.CommandTimeout;
			DesignTimeVisible = source.DesignTimeVisible;
			UpdatedRowSource = source.UpdatedRowSource;
			foreach (SQLiteParameter item in source._parameterCollection)
			{
				Parameters.Add(item.Clone());
			}
		}

		public SQLiteCommand(string commandText, SQLiteConnection connection, SQLiteTransaction transaction)
		{
			_commandTimeout = 30;
			_parameterCollection = new SQLiteParameterCollection(this);
			_designTimeVisible = true;
			_updateRowSource = UpdateRowSource.None;
			if (commandText != null)
			{
				CommandText = commandText;
			}
			if (connection != null)
			{
				DbConnection = connection;
				_commandTimeout = connection.DefaultTimeout;
			}
			if (transaction != null)
			{
				Transaction = transaction;
			}
			SQLiteConnection.OnChanged(connection, new ConnectionEventArgs(SQLiteConnectionEventType.NewCommand, null, transaction, this, null, null, null, null));
		}

		[Conditional("CHECK_STATE")]
		internal static void Check(SQLiteCommand command)
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			command.CheckDisposed();
		}

		private void CheckDisposed()
		{
			if (disposed)
			{
				throw new ObjectDisposedException(typeof(SQLiteCommand).Name);
			}
		}

		protected override void Dispose(bool disposing)
		{
			SQLiteConnection.OnChanged(_cnn, new ConnectionEventArgs(SQLiteConnectionEventType.DisposingCommand, null, _transaction, this, null, null, null, new object[2] { disposing, disposed }));
			bool flag = false;
			try
			{
				if (disposed || !disposing)
				{
					return;
				}
				SQLiteDataReader sQLiteDataReader = null;
				if (_activeReader != null)
				{
					try
					{
						sQLiteDataReader = _activeReader.Target as SQLiteDataReader;
					}
					catch (InvalidOperationException)
					{
					}
				}
				if (sQLiteDataReader != null)
				{
					sQLiteDataReader._disposeCommand = true;
					_activeReader = null;
					flag = true;
				}
				else
				{
					Connection = null;
					_parameterCollection.Clear();
					_commandText = null;
				}
			}
			finally
			{
				if (!flag)
				{
					base.Dispose(disposing);
					disposed = true;
				}
			}
		}

		internal static SQLiteConnectionFlags GetFlags(SQLiteCommand command)
		{
			try
			{
				if (command != null)
				{
					SQLiteConnection cnn = command._cnn;
					if (cnn != null)
					{
						return cnn.Flags;
					}
				}
			}
			catch (ObjectDisposedException)
			{
			}
			return SQLiteConnectionFlags.Default;
		}

		private void DisposeStatements()
		{
			if (_statementList != null)
			{
				int count = _statementList.Count;
				for (int i = 0; i < count; i++)
				{
					_statementList[i]?.Dispose();
				}
				_statementList = null;
			}
		}

		internal void ClearCommands()
		{
			if (_activeReader != null)
			{
				SQLiteDataReader sQLiteDataReader = null;
				try
				{
					sQLiteDataReader = _activeReader.Target as SQLiteDataReader;
				}
				catch (InvalidOperationException)
				{
				}
				sQLiteDataReader?.Close();
				_activeReader = null;
			}
			DisposeStatements();
			_parameterCollection.Unbind();
		}

		internal SQLiteStatement BuildNextCommand()
		{
			SQLiteStatement sQLiteStatement = null;
			try
			{
				if (_cnn != null && _cnn._sql != null)
				{
					if (_statementList == null)
					{
						_remainingText = _commandText;
					}
					sQLiteStatement = _cnn._sql.Prepare(_cnn, _remainingText, (_statementList == null) ? null : _statementList[_statementList.Count - 1], (uint)(_commandTimeout * 1000), ref _remainingText);
					if (sQLiteStatement != null)
					{
						sQLiteStatement._command = this;
						if (_statementList == null)
						{
							_statementList = new List<SQLiteStatement>();
						}
						_statementList.Add(sQLiteStatement);
						_parameterCollection.MapParameters(sQLiteStatement);
						sQLiteStatement.BindParameters();
					}
				}
				return sQLiteStatement;
			}
			catch (Exception)
			{
				if (sQLiteStatement != null)
				{
					if (_statementList != null && _statementList.Contains(sQLiteStatement))
					{
						_statementList.Remove(sQLiteStatement);
					}
					sQLiteStatement.Dispose();
				}
				_remainingText = null;
				throw;
			}
		}

		internal SQLiteStatement GetStatement(int index)
		{
			if (_statementList == null)
			{
				return BuildNextCommand();
			}
			if (index == _statementList.Count)
			{
				if (!string.IsNullOrEmpty(_remainingText))
				{
					return BuildNextCommand();
				}
				return null;
			}
			SQLiteStatement sQLiteStatement = _statementList[index];
			sQLiteStatement.BindParameters();
			return sQLiteStatement;
		}

		public override void Cancel()
		{
			CheckDisposed();
			if (_activeReader != null && _activeReader.Target is SQLiteDataReader sQLiteDataReader)
			{
				sQLiteDataReader.Cancel();
			}
		}

		protected override DbParameter CreateDbParameter()
		{
			return CreateParameter();
		}

		public new SQLiteParameter CreateParameter()
		{
			CheckDisposed();
			return new SQLiteParameter(this);
		}

		private void InitializeForReader()
		{
			if (_activeReader != null && _activeReader.IsAlive)
			{
				throw new InvalidOperationException("DataReader already active on this command");
			}
			if (_cnn == null)
			{
				throw new InvalidOperationException("No connection associated with this command");
			}
			if (_cnn.State != ConnectionState.Open)
			{
				throw new InvalidOperationException("Database is not open");
			}
			if (_cnn._version != _version)
			{
				_version = _cnn._version;
				ClearCommands();
			}
			_parameterCollection.MapParameters(null);
		}

		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			return ExecuteReader(behavior);
		}

		public static object Execute(string commandText, SQLiteExecuteType executeType, string connectionString, params object[] args)
		{
			return Execute(commandText, executeType, CommandBehavior.Default, connectionString, args);
		}

		public static object Execute(string commandText, SQLiteExecuteType executeType, CommandBehavior commandBehavior, string connectionString, params object[] args)
		{
			SQLiteConnection sQLiteConnection = null;
			try
			{
				if (connectionString == null)
				{
					connectionString = DefaultConnectionString;
				}
				using (sQLiteConnection = new SQLiteConnection(connectionString))
				{
					sQLiteConnection.Open();
					using SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();
					sQLiteCommand.CommandText = commandText;
					if (args != null)
					{
						foreach (object obj in args)
						{
							SQLiteParameter sQLiteParameter = obj as SQLiteParameter;
							if (sQLiteParameter == null)
							{
								sQLiteParameter = sQLiteCommand.CreateParameter();
								sQLiteParameter.DbType = DbType.Object;
								sQLiteParameter.Value = obj;
							}
							sQLiteCommand.Parameters.Add(sQLiteParameter);
						}
					}
					switch (executeType)
					{
					case SQLiteExecuteType.NonQuery:
						return sQLiteCommand.ExecuteNonQuery(commandBehavior);
					case SQLiteExecuteType.Scalar:
						return sQLiteCommand.ExecuteScalar(commandBehavior);
					case SQLiteExecuteType.Reader:
					{
						bool flag = true;
						try
						{
							return sQLiteCommand.ExecuteReader(commandBehavior | CommandBehavior.CloseConnection);
						}
						catch
						{
							flag = false;
							throw;
						}
						finally
						{
							if (flag)
							{
								sQLiteConnection._noDispose = true;
							}
						}
					}
					case SQLiteExecuteType.None:
						break;
					}
				}
			}
			finally
			{
				if (sQLiteConnection != null)
				{
					sQLiteConnection._noDispose = false;
				}
			}
			return null;
		}

		public new SQLiteDataReader ExecuteReader(CommandBehavior behavior)
		{
			CheckDisposed();
			InitializeForReader();
			SQLiteDataReader sQLiteDataReader = new SQLiteDataReader(this, behavior);
			_activeReader = new WeakReference(sQLiteDataReader, trackResurrection: false);
			return sQLiteDataReader;
		}

		public new SQLiteDataReader ExecuteReader()
		{
			CheckDisposed();
			return ExecuteReader(CommandBehavior.Default);
		}

		internal void ClearDataReader()
		{
			_activeReader = null;
		}

		public override int ExecuteNonQuery()
		{
			CheckDisposed();
			return ExecuteNonQuery(CommandBehavior.Default);
		}

		public int ExecuteNonQuery(CommandBehavior behavior)
		{
			CheckDisposed();
			using SQLiteDataReader sQLiteDataReader = ExecuteReader(behavior | CommandBehavior.SingleRow | CommandBehavior.SingleResult);
			while (sQLiteDataReader.NextResult())
			{
			}
			return sQLiteDataReader.RecordsAffected;
		}

		public override object ExecuteScalar()
		{
			CheckDisposed();
			return ExecuteScalar(CommandBehavior.Default);
		}

		public object ExecuteScalar(CommandBehavior behavior)
		{
			CheckDisposed();
			using (SQLiteDataReader sQLiteDataReader = ExecuteReader(behavior | CommandBehavior.SingleRow | CommandBehavior.SingleResult))
			{
				if (sQLiteDataReader.Read())
				{
					return sQLiteDataReader[0];
				}
			}
			return null;
		}

		public override void Prepare()
		{
			CheckDisposed();
		}

		public object Clone()
		{
			CheckDisposed();
			return new SQLiteCommand(this);
		}
	}
	public sealed class SQLiteCommandBuilder : DbCommandBuilder
	{
		private bool disposed;

		public new SQLiteDataAdapter DataAdapter
		{
			get
			{
				CheckDisposed();
				return (SQLiteDataAdapter)base.DataAdapter;
			}
			set
			{
				CheckDisposed();
				base.DataAdapter = value;
			}
		}

		[Browsable(false)]
		public override CatalogLocation CatalogLocation
		{
			get
			{
				CheckDisposed();
				return base.CatalogLocation;
			}
			set
			{
				CheckDisposed();
				base.CatalogLocation = value;
			}
		}

		[Browsable(false)]
		public override string CatalogSeparator
		{
			get
			{
				CheckDisposed();
				return base.CatalogSeparator;
			}
			set
			{
				CheckDisposed();
				base.CatalogSeparator = value;
			}
		}

		[Browsable(false)]
		[DefaultValue("[")]
		public override string QuotePrefix
		{
			get
			{
				CheckDisposed();
				return base.QuotePrefix;
			}
			set
			{
				CheckDisposed();
				base.QuotePrefix = value;
			}
		}

		[Browsable(false)]
		public override string QuoteSuffix
		{
			get
			{
				CheckDisposed();
				return base.QuoteSuffix;
			}
			set
			{
				CheckDisposed();
				base.QuoteSuffix = value;
			}
		}

		[Browsable(false)]
		public override string SchemaSeparator
		{
			get
			{
				CheckDisposed();
				return base.SchemaSeparator;
			}
			set
			{
				CheckDisposed();
				base.SchemaSeparator = value;
			}
		}

		public SQLiteCommandBuilder()
			: this(null)
		{
		}

		public SQLiteCommandBuilder(SQLiteDataAdapter adp)
		{
			QuotePrefix = "[";
			QuoteSuffix = "]";
			DataAdapter = adp;
		}

		private void CheckDisposed()
		{
			if (disposed)
			{
				throw new ObjectDisposedException(typeof(SQLiteCommandBuilder).Name);
			}
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				_ = disposed;
			}
			finally
			{
				base.Dispose(disposing);
				disposed = true;
			}
		}

		protected override void ApplyParameterInfo(DbParameter parameter, DataRow row, StatementType statementType, bool whereClause)
		{
			SQLiteParameter sQLiteParameter = (SQLiteParameter)parameter;
			sQLiteParameter.DbType = (DbType)row[SchemaTableColumn.ProviderType];
		}

		protected override string GetParameterName(string parameterName)
		{
			return string.Format(CultureInfo.InvariantCulture, "@{0}", new object[1] { parameterName });
		}

		protected override string GetParameterName(int parameterOrdinal)
		{
			return string.Format(CultureInfo.InvariantCulture, "@param{0}", new object[1] { parameterOrdinal });
		}

		protected override string GetParameterPlaceholder(int parameterOrdinal)
		{
			return GetParameterName(parameterOrdinal);
		}

		protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
		{
			if (adapter == base.DataAdapter)
			{
				((SQLiteDataAdapter)adapter).RowUpdating -= RowUpdatingEventHandler;
			}
			else
			{
				((SQLiteDataAdapter)adapter).RowUpdating += RowUpdatingEventHandler;
			}
		}

		private void RowUpdatingEventHandler(object sender, RowUpdatingEventArgs e)
		{
			RowUpdatingHandler(e);
		}

		public new SQLiteCommand GetDeleteCommand()
		{
			CheckDisposed();
			return (SQLiteCommand)base.GetDeleteCommand();
		}

		public new SQLiteCommand GetDeleteCommand(bool useColumnsForParameterNames)
		{
			CheckDisposed();
			return (SQLiteCommand)base.GetDeleteCommand(useColumnsForParameterNames);
		}

		public new SQLiteCommand GetUpdateCommand()
		{
			CheckDisposed();
			return (SQLiteCommand)base.GetUpdateCommand();
		}

		public new SQLiteCommand GetUpdateCommand(bool useColumnsForParameterNames)
		{
			CheckDisposed();
			return (SQLiteCommand)base.GetUpdateCommand(useColumnsForParameterNames);
		}

		public new SQLiteCommand GetInsertCommand()
		{
			CheckDisposed();
			return (SQLiteCommand)base.GetInsertCommand();
		}

		public new SQLiteCommand GetInsertCommand(bool useColumnsForParameterNames)
		{
			CheckDisposed();
			return (SQLiteCommand)base.GetInsertCommand(useColumnsForParameterNames);
		}

		public override string QuoteIdentifier(string unquotedIdentifier)
		{
			CheckDisposed();
			if (string.IsNullOrEmpty(QuotePrefix) || string.IsNullOrEmpty(QuoteSuffix) || string.IsNullOrEmpty(unquotedIdentifier))
			{
				return unquotedIdentifier;
			}
			return QuotePrefix + unquotedIdentifier.Replace(QuoteSuffix, QuoteSuffix + QuoteSuffix) + QuoteSuffix;
		}

		public override string UnquoteIdentifier(string quotedIdentifier)
		{
			CheckDisposed();
			if (string.IsNullOrEmpty(QuotePrefix) || string.IsNullOrEmpty(QuoteSuffix) || string.IsNullOrEmpty(quotedIdentifier))
			{
				return quotedIdentifier;
			}
			if (!quotedIdentifier.StartsWith(QuotePrefix, StringComparison.OrdinalIgnoreCase) || !quotedIdentifier.EndsWith(QuoteSuffix, StringComparison.OrdinalIgnoreCase))
			{
				return quotedIdentifier;
			}
			return quotedIdentifier.Substring(QuotePrefix.Length, quotedIdentifier.Length - (QuotePrefix.Length + QuoteSuffix.Length)).Replace(QuoteSuffix + QuoteSuffix, QuoteSuffix);
		}

		protected override DataTable GetSchemaTable(DbCommand sourceCommand)
		{
			using IDataReader dataReader = sourceCommand.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo);
			DataTable schemaTable = dataReader.GetSchemaTable();
			if (HasSchemaPrimaryKey(schemaTable))
			{
				ResetIsUniqueSchemaColumn(schemaTable);
			}
			return schemaTable;
		}

		private bool HasSchemaPrimaryKey(DataTable schema)
		{
			DataColumn column = schema.Columns[SchemaTableColumn.IsKey];
			foreach (DataRow row in schema.Rows)
			{
				if ((bool)row[column])
				{
					return true;
				}
			}
			return false;
		}

		private void ResetIsUniqueSchemaColumn(DataTable schema)
		{
			DataColumn column = schema.Columns[SchemaTableColumn.IsUnique];
			DataColumn column2 = schema.Columns[SchemaTableColumn.IsKey];
			foreach (DataRow row in schema.Rows)
			{
				if (!(bool)row[column2])
				{
					row[column] = false;
				}
			}
			schema.AcceptChanges();
		}
	}
	public class ConnectionEventArgs : EventArgs
	{
		public readonly SQLiteConnectionEventType EventType;

		public readonly StateChangeEventArgs EventArgs;

		public readonly IDbTransaction Transaction;

		public readonly IDbCommand Command;

		public readonly IDataReader DataReader;

		public readonly CriticalHandle CriticalHandle;

		public readonly string Text;

		public readonly object Data;

		internal ConnectionEventArgs(SQLiteConnectionEventType eventType, StateChangeEventArgs eventArgs, IDbTransaction transaction, IDbCommand command, IDataReader dataReader, CriticalHandle criticalHandle, string text, object data)
		{
			EventType = eventType;
			EventArgs = eventArgs;
			Transaction = transaction;
			Command = command;
			DataReader = dataReader;
			CriticalHandle = criticalHandle;
			Text = text;
			Data = data;
		}
	}
	public delegate void SQLiteConnectionEventHandler(object sender, ConnectionEventArgs e);
	public sealed class SQLiteConnection : DbConnection, ICloneable, IDisposable
	{
		internal const DbType BadDbType = (DbType)(-1);

		internal const string DefaultBaseSchemaName = "sqlite_default_schema";

		private const string MemoryFileName = ":memory:";

		internal const IsolationLevel DeferredIsolationLevel = IsolationLevel.ReadCommitted;

		internal const IsolationLevel ImmediateIsolationLevel = IsolationLevel.Serializable;

		private const SQLiteConnectionFlags FallbackDefaultFlags = SQLiteConnectionFlags.Default;

		private const SQLiteSynchronousEnum DefaultSynchronous = SQLiteSynchronousEnum.Default;

		private const SQLiteJournalModeEnum DefaultJournalMode = SQLiteJournalModeEnum.Default;

		private const IsolationLevel DefaultIsolationLevel = IsolationLevel.Serializable;

		internal const SQLiteDateFormats DefaultDateTimeFormat = SQLiteDateFormats.ISO8601;

		internal const DateTimeKind DefaultDateTimeKind = DateTimeKind.Unspecified;

		internal const string DefaultDateTimeFormatString = null;

		private const string DefaultDataSource = null;

		private const string DefaultUri = null;

		private const string DefaultFullUri = null;

		private const string DefaultHexPassword = null;

		private const string DefaultPassword = null;

		private const int DefaultVersion = 3;

		private const int DefaultPageSize = 1024;

		private const int DefaultMaxPageCount = 0;

		private const int DefaultCacheSize = 2000;

		private const int DefaultMaxPoolSize = 100;

		private const int DefaultConnectionTimeout = 30;

		private const bool DefaultNoSharedFlags = false;

		private const bool DefaultFailIfMissing = false;

		private const bool DefaultReadOnly = false;

		internal const bool DefaultBinaryGUID = true;

		private const bool DefaultUseUTF16Encoding = false;

		private const bool DefaultToFullPath = true;

		private const bool DefaultPooling = false;

		private const bool DefaultLegacyFormat = false;

		private const bool DefaultForeignKeys = false;

		private const bool DefaultEnlist = true;

		private const bool DefaultSetDefaults = true;

		internal const int DefaultPrepareRetries = 3;

		private const int SQLITE_FCNTL_CHUNK_SIZE = 6;

		private const int SQLITE_FCNTL_WIN32_AV_RETRY = 9;

		private const string _dataDirectory = "|DataDirectory|";

		private const string _masterdb = "sqlite_master";

		private const string _tempmasterdb = "sqlite_temp_master";

		private static readonly Assembly _assembly = typeof(SQLiteConnection).Assembly;

		private static readonly object _syncRoot = new object();

		private static SQLiteConnectionFlags _sharedFlags;

		private static int _versionNumber;

		private ConnectionState _connectionState;

		private string _connectionString;

		internal int _transactionLevel;

		internal bool _noDispose;

		private bool _disposing;

		private IsolationLevel _defaultIsolation;

		internal SQLiteEnlistment _enlistment;

		internal SQLiteDbTypeMap _typeNames;

		internal SQLiteBase _sql;

		private string _dataSource;

		private byte[] _password;

		internal string _baseSchemaName;

		private SQLiteConnectionFlags _flags;

		private Dictionary<string, object> _cachedSettings;

		private DbType? _defaultDbType;

		private string _defaultTypeName;

		private int _defaultTimeout = 30;

		internal int _prepareRetries = 3;

		private bool _parseViaFramework;

		internal bool _binaryGuid;

		internal int _version;

		private SQLiteAuthorizerCallback _authorizerCallback;

		private SQLiteUpdateCallback _updateCallback;

		private SQLiteCommitCallback _commitCallback;

		private SQLiteTraceCallback _traceCallback;

		private SQLiteRollbackCallback _rollbackCallback;

		private bool disposed;

		public static ISQLiteConnectionPool ConnectionPool
		{
			get
			{
				return SQLiteConnectionPool.GetConnectionPool();
			}
			set
			{
				SQLiteConnectionPool.SetConnectionPool(value);
			}
		}

		public int PoolCount
		{
			get
			{
				if (_sql == null)
				{
					return 0;
				}
				return _sql.CountPool();
			}
		}

		[Editor("SQLite.Designer.SQLiteConnectionStringEditor, SQLite.Designer, Version=1.0.97.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue("")]
		public override string ConnectionString
		{
			get
			{
				CheckDisposed();
				return _connectionString;
			}
			set
			{
				CheckDisposed();
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				if (_connectionState != ConnectionState.Closed)
				{
					throw new InvalidOperationException();
				}
				_connectionString = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string DataSource
		{
			get
			{
				CheckDisposed();
				return _dataSource;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Database
		{
			get
			{
				CheckDisposed();
				return "main";
			}
		}

		public int DefaultTimeout
		{
			get
			{
				CheckDisposed();
				return _defaultTimeout;
			}
			set
			{
				CheckDisposed();
				_defaultTimeout = value;
			}
		}

		public int PrepareRetries
		{
			get
			{
				CheckDisposed();
				return _prepareRetries;
			}
			set
			{
				CheckDisposed();
				_prepareRetries = value;
			}
		}

		public bool ParseViaFramework
		{
			get
			{
				CheckDisposed();
				return _parseViaFramework;
			}
			set
			{
				CheckDisposed();
				_parseViaFramework = value;
			}
		}

		public SQLiteConnectionFlags Flags
		{
			get
			{
				CheckDisposed();
				return _flags;
			}
			set
			{
				CheckDisposed();
				_flags = value;
			}
		}

		public DbType? DefaultDbType
		{
			get
			{
				CheckDisposed();
				return _defaultDbType;
			}
			set
			{
				CheckDisposed();
				_defaultDbType = value;
			}
		}

		public string DefaultTypeName
		{
			get
			{
				CheckDisposed();
				return _defaultTypeName;
			}
			set
			{
				CheckDisposed();
				_defaultTypeName = value;
			}
		}

		public bool OwnHandle
		{
			get
			{
				CheckDisposed();
				if (_sql == null)
				{
					throw new InvalidOperationException("Database connection not valid for checking handle.");
				}
				return _sql.OwnHandle;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string ServerVersion
		{
			get
			{
				CheckDisposed();
				return SQLiteVersion;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public long LastInsertRowId
		{
			get
			{
				CheckDisposed();
				if (_sql == null)
				{
					throw new InvalidOperationException("Database connection not valid for getting last insert rowid.");
				}
				return _sql.LastInsertRowId;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Changes
		{
			get
			{
				CheckDisposed();
				if (_sql == null)
				{
					throw new InvalidOperationException("Database connection not valid for getting number of changes.");
				}
				return _sql.Changes;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool AutoCommit
		{
			get
			{
				CheckDisposed();
				if (_sql == null)
				{
					throw new InvalidOperationException("Database connection not valid for getting autocommit mode.");
				}
				return _sql.AutoCommit;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public long MemoryUsed
		{
			get
			{
				CheckDisposed();
				if (_sql == null)
				{
					throw new InvalidOperationException("Database connection not valid for getting memory used.");
				}
				return _sql.MemoryUsed;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public long MemoryHighwater
		{
			get
			{
				CheckDisposed();
				if (_sql == null)
				{
					throw new InvalidOperationException("Database connection not valid for getting maximum memory used.");
				}
				return _sql.MemoryHighwater;
			}
		}

		public static string DefineConstants => SQLite3.DefineConstants;

		public static string SQLiteVersion => SQLite3.SQLiteVersion;

		public static string SQLiteSourceId => SQLite3.SQLiteSourceId;

		public static string SQLiteCompileOptions => SQLite3.SQLiteCompileOptions;

		public static string InteropVersion => SQLite3.InteropVersion;

		public static string InteropSourceId => SQLite3.InteropSourceId;

		public static string InteropCompileOptions => SQLite3.InteropCompileOptions;

		public static string ProviderVersion
		{
			get
			{
				if ((object)_assembly == null)
				{
					return null;
				}
				return _assembly.GetName().Version.ToString();
			}
		}

		public static string ProviderSourceId
		{
			get
			{
				if ((object)_assembly == null)
				{
					return null;
				}
				string text = null;
				if (_assembly.IsDefined(typeof(AssemblySourceIdAttribute), inherit: false))
				{
					AssemblySourceIdAttribute assemblySourceIdAttribute = (AssemblySourceIdAttribute)_assembly.GetCustomAttributes(typeof(AssemblySourceIdAttribute), inherit: false)[0];
					text = assemblySourceIdAttribute.SourceId;
				}
				string text2 = null;
				if (_assembly.IsDefined(typeof(AssemblySourceTimeStampAttribute), inherit: false))
				{
					AssemblySourceTimeStampAttribute assemblySourceTimeStampAttribute = (AssemblySourceTimeStampAttribute)_assembly.GetCustomAttributes(typeof(AssemblySourceTimeStampAttribute), inherit: false)[0];
					text2 = assemblySourceTimeStampAttribute.SourceTimeStamp;
				}
				if (text != null || text2 != null)
				{
					if (text == null)
					{
						text = "0000000000000000000000000000000000000000";
					}
					if (text2 == null)
					{
						text2 = "0000-00-00 00:00:00 UTC";
					}
					return $"{text} {text2}";
				}
				return null;
			}
		}

		public static SQLiteConnectionFlags DefaultFlags
		{
			get
			{
				object obj = TryParseEnum(typeof(SQLiteConnectionFlags), UnsafeNativeMethods.GetSettingValue("DefaultFlags_SQLiteConnection", null), ignoreCase: true);
				if (obj is SQLiteConnectionFlags)
				{
					return (SQLiteConnectionFlags)obj;
				}
				return SQLiteConnectionFlags.Default;
			}
		}

		public static SQLiteConnectionFlags SharedFlags
		{
			get
			{
				lock (_syncRoot)
				{
					return _sharedFlags;
				}
			}
			set
			{
				lock (_syncRoot)
				{
					_sharedFlags = value;
				}
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ConnectionState State
		{
			get
			{
				CheckDisposed();
				return _connectionState;
			}
		}

		protected override DbProviderFactory DbProviderFactory => SQLiteFactory.Instance;

		private static event SQLiteConnectionEventHandler _handlers;

		private event SQLiteAuthorizerEventHandler _authorizerHandler;

		private event SQLiteUpdateEventHandler _updateHandler;

		private event SQLiteCommitHandler _commitHandler;

		private event SQLiteTraceEventHandler _traceHandler;

		private event EventHandler _rollbackHandler;

		public override event StateChangeEventHandler StateChange;

		public static event SQLiteConnectionEventHandler Changed
		{
			add
			{
				lock (_syncRoot)
				{
					SQLiteConnection._handlers = (SQLiteConnectionEventHandler)Delegate.Remove(SQLiteConnection._handlers, value);
					SQLiteConnection._handlers = (SQLiteConnectionEventHandler)Delegate.Combine(SQLiteConnection._handlers, value);
				}
			}
			remove
			{
				lock (_syncRoot)
				{
					SQLiteConnection._handlers = (SQLiteConnectionEventHandler)Delegate.Remove(SQLiteConnection._handlers, value);
				}
			}
		}

		public event SQLiteAuthorizerEventHandler Authorize
		{
			add
			{
				CheckDisposed();
				if (this._authorizerHandler == null)
				{
					_authorizerCallback = AuthorizerCallback;
					if (_sql != null)
					{
						_sql.SetAuthorizerHook(_authorizerCallback);
					}
				}
				this._authorizerHandler = (SQLiteAuthorizerEventHandler)Delegate.Combine(this._authorizerHandler, value);
			}
			remove
			{
				CheckDisposed();
				this._authorizerHandler = (SQLiteAuthorizerEventHandler)Delegate.Remove(this._authorizerHandler, value);
				if (this._authorizerHandler == null)
				{
					if (_sql != null)
					{
						_sql.SetAuthorizerHook(null);
					}
					_authorizerCallback = null;
				}
			}
		}

		public event SQLiteUpdateEventHandler Update
		{
			add
			{
				CheckDisposed();
				if (this._updateHandler == null)
				{
					_updateCallback = UpdateCallback;
					if (_sql != null)
					{
						_sql.SetUpdateHook(_updateCallback);
					}
				}
				this._updateHandler = (SQLiteUpdateEventHandler)Delegate.Combine(this._updateHandler, value);
			}
			remove
			{
				CheckDisposed();
				this._updateHandler = (SQLiteUpdateEventHandler)Delegate.Remove(this._updateHandler, value);
				if (this._updateHandler == null)
				{
					if (_sql != null)
					{
						_sql.SetUpdateHook(null);
					}
					_updateCallback = null;
				}
			}
		}

		public event SQLiteCommitHandler Commit
		{
			add
			{
				CheckDisposed();
				if (this._commitHandler == null)
				{
					_commitCallback = CommitCallback;
					if (_sql != null)
					{
						_sql.SetCommitHook(_commitCallback);
					}
				}
				this._commitHandler = (SQLiteCommitHandler)Delegate.Combine(this._commitHandler, value);
			}
			remove
			{
				CheckDisposed();
				this._commitHandler = (SQLiteCommitHandler)Delegate.Remove(this._commitHandler, value);
				if (this._commitHandler == null)
				{
					if (_sql != null)
					{
						_sql.SetCommitHook(null);
					}
					_commitCallback = null;
				}
			}
		}

		public event SQLiteTraceEventHandler Trace
		{
			add
			{
				CheckDisposed();
				if (this._traceHandler == null)
				{
					_traceCallback = TraceCallback;
					if (_sql != null)
					{
						_sql.SetTraceCallback(_traceCallback);
					}
				}
				this._traceHandler = (SQLiteTraceEventHandler)Delegate.Combine(this._traceHandler, value);
			}
			remove
			{
				CheckDisposed();
				this._traceHandler = (SQLiteTraceEventHandler)Delegate.Remove(this._traceHandler, value);
				if (this._traceHandler == null)
				{
					if (_sql != null)
					{
						_sql.SetTraceCallback(null);
					}
					_traceCallback = null;
				}
			}
		}

		public event EventHandler RollBack
		{
			add
			{
				CheckDisposed();
				if (this._rollbackHandler == null)
				{
					_rollbackCallback = RollbackCallback;
					if (_sql != null)
					{
						_sql.SetRollbackHook(_rollbackCallback);
					}
				}
				this._rollbackHandler = (EventHandler)Delegate.Combine(this._rollbackHandler, value);
			}
			remove
			{
				CheckDisposed();
				this._rollbackHandler = (EventHandler)Delegate.Remove(this._rollbackHandler, value);
				if (this._rollbackHandler == null)
				{
					if (_sql != null)
					{
						_sql.SetRollbackHook(null);
					}
					_rollbackCallback = null;
				}
			}
		}

		public SQLiteConnection()
			: this((string)null)
		{
		}

		public SQLiteConnection(string connectionString)
			: this(connectionString, parseViaFramework: false)
		{
		}

		internal SQLiteConnection(IntPtr db, string fileName, bool ownHandle)
			: this()
		{
			_sql = new SQLite3(SQLiteDateFormats.ISO8601, DateTimeKind.Unspecified, null, db, fileName, ownHandle);
			_flags = SQLiteConnectionFlags.None;
			_connectionState = ((db != IntPtr.Zero) ? ConnectionState.Open : ConnectionState.Closed);
			_connectionString = null;
		}

		public SQLiteConnection(string connectionString, bool parseViaFramework)
		{
			_noDispose = false;
			UnsafeNativeMethods.Initialize();
			SQLiteLog.Initialize();
			lock (_syncRoot)
			{
				if (_versionNumber == 0)
				{
					_versionNumber = SQLite3.SQLiteVersionNumber;
					if (_versionNumber >= 3007014)
					{
						SQLiteConnectionHandle.closeConnection = SQLiteBase.CloseConnectionV2;
					}
				}
			}
			_cachedSettings = new Dictionary<string, object>(new TypeNameStringComparer());
			_typeNames = new SQLiteDbTypeMap();
			_parseViaFramework = parseViaFramework;
			_flags = SQLiteConnectionFlags.Default;
			_defaultDbType = null;
			_defaultTypeName = null;
			_connectionState = ConnectionState.Closed;
			_connectionString = null;
			if (connectionString != null)
			{
				ConnectionString = connectionString;
			}
		}

		public SQLiteConnection(SQLiteConnection connection)
			: this(connection.ConnectionString, connection.ParseViaFramework)
		{
			if (connection.State != ConnectionState.Open)
			{
				return;
			}
			Open();
			using DataTable dataTable = connection.GetSchema("Catalogs");
			foreach (DataRow row in dataTable.Rows)
			{
				string strA = row[0].ToString();
				if (string.Compare(strA, "main", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(strA, "temp", StringComparison.OrdinalIgnoreCase) != 0)
				{
					using SQLiteCommand sQLiteCommand = CreateCommand();
					sQLiteCommand.CommandText = string.Format(CultureInfo.InvariantCulture, "ATTACH DATABASE '{0}' AS [{1}]", new object[2]
					{
						row[1],
						row[0]
					});
					sQLiteCommand.ExecuteNonQuery();
				}
			}
		}

		internal static void OnChanged(SQLiteConnection connection, ConnectionEventArgs e)
		{
			if (connection == null || connection.CanRaiseEvents)
			{
				SQLiteConnectionEventHandler sQLiteConnectionEventHandler;
				lock (_syncRoot)
				{
					sQLiteConnectionEventHandler = ((SQLiteConnection._handlers == null) ? null : (SQLiteConnection._handlers.Clone() as SQLiteConnectionEventHandler));
				}
				sQLiteConnectionEventHandler?.Invoke(connection, e);
			}
		}

		public static object CreateHandle(IntPtr nativeHandle)
		{
			SQLiteConnectionHandle sQLiteConnectionHandle;
			try
			{
			}
			finally
			{
				sQLiteConnectionHandle = ((nativeHandle != IntPtr.Zero) ? new SQLiteConnectionHandle(nativeHandle, ownHandle: true) : null);
			}
			if (sQLiteConnectionHandle != null)
			{
				OnChanged(null, new ConnectionEventArgs(SQLiteConnectionEventType.NewCriticalHandle, null, null, null, null, sQLiteConnectionHandle, null, new object[1] { nativeHandle }));
			}
			return sQLiteConnectionHandle;
		}

		public void BackupDatabase(SQLiteConnection destination, string destinationName, string sourceName, int pages, SQLiteBackupCallback callback, int retryMilliseconds)
		{
			CheckDisposed();
			if (_connectionState != ConnectionState.Open)
			{
				throw new InvalidOperationException("Source database is not open.");
			}
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (destination._connectionState != ConnectionState.Open)
			{
				throw new ArgumentException("Destination database is not open.", "destination");
			}
			if (destinationName == null)
			{
				throw new ArgumentNullException("destinationName");
			}
			if (sourceName == null)
			{
				throw new ArgumentNullException("sourceName");
			}
			SQLiteBase sql = _sql;
			if (sql == null)
			{
				throw new InvalidOperationException("Connection object has an invalid handle.");
			}
			SQLiteBackup sQLiteBackup = null;
			try
			{
				sQLiteBackup = sql.InitializeBackup(destination, destinationName, sourceName);
				bool retry = false;
				while (sql.StepBackup(sQLiteBackup, pages, ref retry) && (callback == null || callback(this, sourceName, destination, destinationName, pages, sql.RemainingBackup(sQLiteBackup), sql.PageCountBackup(sQLiteBackup), retry)))
				{
					if (retry && retryMilliseconds >= 0)
					{
						Thread.Sleep(retryMilliseconds);
					}
					if (pages == 0)
					{
						break;
					}
				}
			}
			catch (Exception ex)
			{
				if ((_flags & SQLiteConnectionFlags.LogBackup) == SQLiteConnectionFlags.LogBackup)
				{
					SQLiteLog.LogMessage(string.Format(CultureInfo.CurrentCulture, "Caught exception while backing up database: {0}", new object[1] { ex }));
				}
				throw;
			}
			finally
			{
				if (sQLiteBackup != null)
				{
					sql.FinishBackup(sQLiteBackup);
				}
			}
		}

		public int ClearCachedSettings()
		{
			CheckDisposed();
			int result = -1;
			if (_cachedSettings != null)
			{
				result = _cachedSettings.Count;
				_cachedSettings.Clear();
			}
			return result;
		}

		internal bool TryGetCachedSetting(string name, string @default, out object value)
		{
			if (name == null || _cachedSettings == null)
			{
				value = @default;
				return false;
			}
			return _cachedSettings.TryGetValue(name, out value);
		}

		internal void SetCachedSetting(string name, object value)
		{
			if (name != null && _cachedSettings != null)
			{
				_cachedSettings[name] = value;
			}
		}

		public int ClearTypeMappings()
		{
			CheckDisposed();
			int result = -1;
			if (_typeNames != null)
			{
				result = _typeNames.Clear();
			}
			return result;
		}

		public Dictionary<string, object> GetTypeMappings()
		{
			CheckDisposed();
			Dictionary<string, object> dictionary = null;
			if (_typeNames != null)
			{
				dictionary = new Dictionary<string, object>(_typeNames.Count, _typeNames.Comparer);
				foreach (KeyValuePair<string, SQLiteDbTypeMapping> typeName in _typeNames)
				{
					SQLiteDbTypeMapping value = typeName.Value;
					object obj = null;
					object obj2 = null;
					object obj3 = null;
					if (value != null)
					{
						obj = value.typeName;
						obj2 = value.dataType;
						obj3 = value.primary;
					}
					dictionary.Add(typeName.Key, new object[3] { obj, obj2, obj3 });
				}
			}
			return dictionary;
		}

		public int AddTypeMapping(string typeName, DbType dataType, bool primary)
		{
			CheckDisposed();
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			int num = -1;
			if (_typeNames != null)
			{
				num = 0;
				if (primary && _typeNames.ContainsKey(dataType))
				{
					num += (_typeNames.Remove(dataType) ? 1 : 0);
				}
				if (_typeNames.ContainsKey(typeName))
				{
					num += (_typeNames.Remove(typeName) ? 1 : 0);
				}
				_typeNames.Add(new SQLiteDbTypeMapping(typeName, dataType, primary));
			}
			return num;
		}

		public void BindFunction(SQLiteFunctionAttribute functionAttribute, SQLiteFunction function)
		{
			CheckDisposed();
			if (_sql == null)
			{
				throw new InvalidOperationException("Database connection not valid for binding functions.");
			}
			_sql.BindFunction(functionAttribute, function, _flags);
		}

		[Conditional("CHECK_STATE")]
		internal static void Check(SQLiteConnection connection)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			connection.CheckDisposed();
			if (connection._connectionState != ConnectionState.Open)
			{
				throw new InvalidOperationException("The connection is not open.");
			}
			if (!(connection._sql is SQLite3 { _sql: var sql }))
			{
				throw new InvalidOperationException("The connection handle wrapper is null.");
			}
			if (sql == null)
			{
				throw new InvalidOperationException("The connection handle is null.");
			}
			if (sql.IsInvalid)
			{
				throw new InvalidOperationException("The connection handle is invalid.");
			}
			if (sql.IsClosed)
			{
				throw new InvalidOperationException("The connection handle is closed.");
			}
		}

		internal static SortedList<string, string> ParseConnectionString(string connectionString, bool parseViaFramework, bool allowNameOnly)
		{
			if (!parseViaFramework)
			{
				return ParseConnectionString(connectionString, allowNameOnly);
			}
			return ParseConnectionStringViaFramework(connectionString, strict: false);
		}

		private void SetupSQLiteBase(SortedList<string, string> opts)
		{
			object obj = TryParseEnum(typeof(SQLiteDateFormats), FindKey(opts, "DateTimeFormat", SQLiteDateFormats.ISO8601.ToString()), ignoreCase: true);
			SQLiteDateFormats fmt = ((!(obj is SQLiteDateFormats)) ? SQLiteDateFormats.ISO8601 : ((SQLiteDateFormats)obj));
			obj = TryParseEnum(typeof(DateTimeKind), FindKey(opts, "DateTimeKind", DateTimeKind.Unspecified.ToString()), ignoreCase: true);
			DateTimeKind kind = ((obj is DateTimeKind) ? ((DateTimeKind)obj) : DateTimeKind.Unspecified);
			string fmtString = FindKey(opts, "DateTimeFormatString", null);
			if (SQLiteConvert.ToBoolean(FindKey(opts, "UseUTF16Encoding", false.ToString())))
			{
				_sql = new SQLite3_UTF16(fmt, kind, fmtString, IntPtr.Zero, null, ownHandle: false);
			}
			else
			{
				_sql = new SQLite3(fmt, kind, fmtString, IntPtr.Zero, null, ownHandle: false);
			}
		}

		public new void Dispose()
		{
			if (!_noDispose)
			{
				base.Dispose();
			}
		}

		private void CheckDisposed()
		{
			if (disposed)
			{
				throw new ObjectDisposedException(typeof(SQLiteConnection).Name);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if ((_flags & SQLiteConnectionFlags.TraceWarning) == SQLiteConnectionFlags.TraceWarning && _noDispose)
			{
				System.Diagnostics.Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "WARNING: Disposing of connection \"{0}\" with the no-dispose flag set.", new object[1] { _connectionString }));
			}
			_disposing = true;
			try
			{
				if (!disposed)
				{
					Close();
				}
			}
			finally
			{
				base.Dispose(disposing);
				disposed = true;
			}
		}

		public object Clone()
		{
			CheckDisposed();
			return new SQLiteConnection(this);
		}

		public static void CreateFile(string databaseFileName)
		{
			FileStream fileStream = File.Create(databaseFileName);
			fileStream.Close();
		}

		internal void OnStateChange(ConnectionState newState, ref StateChangeEventArgs eventArgs)
		{
			ConnectionState connectionState = _connectionState;
			_connectionState = newState;
			if (StateChange != null && newState != connectionState)
			{
				StateChangeEventArgs e = new StateChangeEventArgs(connectionState, newState);
				StateChange(this, e);
				eventArgs = e;
			}
		}

		private static IsolationLevel GetFallbackDefaultIsolationLevel()
		{
			return IsolationLevel.Serializable;
		}

		internal IsolationLevel GetDefaultIsolationLevel()
		{
			return _defaultIsolation;
		}

		[Obsolete("Use one of the standard BeginTransaction methods, this one will be removed soon")]
		public SQLiteTransaction BeginTransaction(IsolationLevel isolationLevel, bool deferredLock)
		{
			CheckDisposed();
			return (SQLiteTransaction)BeginDbTransaction((!deferredLock) ? IsolationLevel.Serializable : IsolationLevel.ReadCommitted);
		}

		[Obsolete("Use one of the standard BeginTransaction methods, this one will be removed soon")]
		public SQLiteTransaction BeginTransaction(bool deferredLock)
		{
			CheckDisposed();
			return (SQLiteTransaction)BeginDbTransaction((!deferredLock) ? IsolationLevel.Serializable : IsolationLevel.ReadCommitted);
		}

		public new SQLiteTransaction BeginTransaction(IsolationLevel isolationLevel)
		{
			CheckDisposed();
			return (SQLiteTransaction)BeginDbTransaction(isolationLevel);
		}

		public new SQLiteTransaction BeginTransaction()
		{
			CheckDisposed();
			return (SQLiteTransaction)BeginDbTransaction(_defaultIsolation);
		}

		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
		{
			if (_connectionState != ConnectionState.Open)
			{
				throw new InvalidOperationException();
			}
			if (isolationLevel == IsolationLevel.Unspecified)
			{
				isolationLevel = _defaultIsolation;
			}
			isolationLevel = GetEffectiveIsolationLevel(isolationLevel);
			if (isolationLevel != IsolationLevel.Serializable && isolationLevel != IsolationLevel.ReadCommitted)
			{
				throw new ArgumentException("isolationLevel");
			}
			SQLiteTransaction sQLiteTransaction = new SQLiteTransaction(this, isolationLevel != IsolationLevel.Serializable);
			OnChanged(this, new ConnectionEventArgs(SQLiteConnectionEventType.NewTransaction, null, sQLiteTransaction, null, null, null, null, null));
			return sQLiteTransaction;
		}

		public override void ChangeDatabase(string databaseName)
		{
			CheckDisposed();
			OnChanged(this, new ConnectionEventArgs(SQLiteConnectionEventType.ChangeDatabase, null, null, null, null, null, databaseName, null));
			throw new NotImplementedException();
		}

		public override void Close()
		{
			CheckDisposed();
			OnChanged(this, new ConnectionEventArgs(SQLiteConnectionEventType.Closing, null, null, null, null, null, null, null));
			if (_sql != null)
			{
				if (_enlistment != null)
				{
					SQLiteConnection sQLiteConnection = new SQLiteConnection();
					sQLiteConnection._sql = _sql;
					sQLiteConnection._transactionLevel = _transactionLevel;
					sQLiteConnection._enlistment = _enlistment;
					sQLiteConnection._connectionState = _connectionState;
					sQLiteConnection._version = _version;
					sQLiteConnection._enlistment._transaction._cnn = sQLiteConnection;
					sQLiteConnection._enlistment._disposeConnection = true;
					_sql = null;
					_enlistment = null;
				}
				if (_sql != null)
				{
					_sql.Close(!_disposing);
					_sql = null;
				}
				_transactionLevel = 0;
			}
			StateChangeEventArgs eventArgs = null;
			OnStateChange(ConnectionState.Closed, ref eventArgs);
			OnChanged(this, new ConnectionEventArgs(SQLiteConnectionEventType.Closed, eventArgs, null, null, null, null, null, null));
		}

		public static void ClearPool(SQLiteConnection connection)
		{
			if (connection._sql != null)
			{
				connection._sql.ClearPool();
			}
		}

		public static void ClearAllPools()
		{
			SQLiteConnectionPool.ClearAllPools();
		}

		public new SQLiteCommand CreateCommand()
		{
			CheckDisposed();
			return new SQLiteCommand(this);
		}

		protected override DbCommand CreateDbCommand()
		{
			return CreateCommand();
		}

		internal static string MapUriPath(string path)
		{
			if (path.StartsWith("file://", StringComparison.OrdinalIgnoreCase))
			{
				return path.Substring(7);
			}
			if (path.StartsWith("file:", StringComparison.OrdinalIgnoreCase))
			{
				return path.Substring(5);
			}
			if (path.StartsWith("/", StringComparison.OrdinalIgnoreCase))
			{
				return path;
			}
			throw new InvalidOperationException("Invalid connection string: invalid URI");
		}

		private static SortedList<string, string> ParseConnectionString(string connectionString, bool allowNameOnly)
		{
			SortedList<string, string> sortedList = new SortedList<string, string>(StringComparer.OrdinalIgnoreCase);
			string error = null;
			string[] array = ((UnsafeNativeMethods.GetSettingValue("No_SQLiteConnectionNewParser", null) == null) ? SQLiteConvert.NewSplit(connectionString, ';', keepQuote: true, ref error) : SQLiteConvert.Split(connectionString, ';'));
			if (array == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Invalid ConnectionString format, cannot parse: {0}", new object[1] { (error != null) ? error : "could not split connection string into properties" }));
			}
			int num = ((array != null) ? array.Length : 0);
			for (int i = 0; i < num; i++)
			{
				if (array[i] == null)
				{
					continue;
				}
				array[i] = array[i].Trim();
				if (array[i].Length == 0)
				{
					continue;
				}
				int num2 = array[i].IndexOf('=');
				if (num2 != -1)
				{
					sortedList.Add(UnwrapString(array[i].Substring(0, num2).Trim()), UnwrapString(array[i].Substring(num2 + 1).Trim()));
					continue;
				}
				if (allowNameOnly)
				{
					sortedList.Add(UnwrapString(array[i].Trim()), string.Empty);
					continue;
				}
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Invalid ConnectionString format for part \"{0}\", no equal sign found", new object[1] { array[i] }));
			}
			return sortedList;
		}

		private static SortedList<string, string> ParseConnectionStringViaFramework(string connectionString, bool strict)
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder();
			dbConnectionStringBuilder.ConnectionString = connectionString;
			SortedList<string, string> sortedList = new SortedList<string, string>(StringComparer.OrdinalIgnoreCase);
			foreach (string key in dbConnectionStringBuilder.Keys)
			{
				object obj = dbConnectionStringBuilder[key];
				string value = null;
				if (obj is string)
				{
					value = (string)obj;
				}
				else
				{
					if (strict)
					{
						throw new ArgumentException("connection property value is not a string", key);
					}
					if (obj != null)
					{
						value = obj.ToString();
					}
				}
				sortedList.Add(key, value);
			}
			return sortedList;
		}

		public override void EnlistTransaction(Transaction transaction)
		{
			CheckDisposed();
			if (_enlistment == null || !(transaction == _enlistment._scope))
			{
				if (_enlistment != null)
				{
					throw new ArgumentException("Already enlisted in a transaction");
				}
				if (_transactionLevel > 0 && transaction != null)
				{
					throw new ArgumentException("Unable to enlist in transaction, a local transaction already exists");
				}
				if (transaction == null)
				{
					throw new ArgumentNullException("Unable to enlist in transaction, it is null");
				}
				bool flag = (_flags & SQLiteConnectionFlags.StrictEnlistment) == SQLiteConnectionFlags.StrictEnlistment;
				_enlistment = new SQLiteEnlistment(this, transaction, GetFallbackDefaultIsolationLevel(), flag, flag);
				OnChanged(this, new ConnectionEventArgs(SQLiteConnectionEventType.EnlistTransaction, null, null, null, null, null, null, new object[1] { _enlistment }));
			}
		}

		internal static string FindKey(SortedList<string, string> items, string key, string defValue)
		{
			if (string.IsNullOrEmpty(key))
			{
				return defValue;
			}
			if (items.TryGetValue(key, out var value))
			{
				return value;
			}
			if (items.TryGetValue(key.Replace(" ", string.Empty), out value))
			{
				return value;
			}
			return defValue;
		}

		internal static object TryParseEnum(Type type, string value, bool ignoreCase)
		{
			if (!string.IsNullOrEmpty(value))
			{
				try
				{
					return Enum.Parse(type, value, ignoreCase);
				}
				catch
				{
				}
			}
			return null;
		}

		private static bool TryParseByte(string value, NumberStyles style, out byte result)
		{
			return byte.TryParse(value, style, null, out result);
		}

		public void EnableExtensions(bool enable)
		{
			CheckDisposed();
			if (_sql == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Database connection not valid for {0} extensions.", new object[1] { enable ? "enabling" : "disabling" }));
			}
			if ((_flags & SQLiteConnectionFlags.NoLoadExtension) == SQLiteConnectionFlags.NoLoadExtension)
			{
				throw new SQLiteException("Loading extensions is disabled for this database connection.");
			}
			_sql.SetLoadExtension(enable);
		}

		public void LoadExtension(string fileName)
		{
			CheckDisposed();
			LoadExtension(fileName, null);
		}

		public void LoadExtension(string fileName, string procName)
		{
			CheckDisposed();
			if (_sql == null)
			{
				throw new InvalidOperationException("Database connection not valid for loading extensions.");
			}
			if ((_flags & SQLiteConnectionFlags.NoLoadExtension) == SQLiteConnectionFlags.NoLoadExtension)
			{
				throw new SQLiteException("Loading extensions is disabled for this database connection.");
			}
			_sql.LoadExtension(fileName, procName);
		}

		public void CreateModule(SQLiteModule module)
		{
			CheckDisposed();
			if (_sql == null)
			{
				throw new InvalidOperationException("Database connection not valid for creating modules.");
			}
			if ((_flags & SQLiteConnectionFlags.NoCreateModule) == SQLiteConnectionFlags.NoCreateModule)
			{
				throw new SQLiteException("Creating modules is disabled for this database connection.");
			}
			_sql.CreateModule(module, _flags);
		}

		internal static byte[] FromHexString(string text)
		{
			string error = null;
			return FromHexString(text, ref error);
		}

		internal static string ToHexString(byte[] array)
		{
			if (array == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				stringBuilder.AppendFormat("{0:x2}", array[i]);
			}
			return stringBuilder.ToString();
		}

		private static byte[] FromHexString(string text, ref string error)
		{
			if (text == null)
			{
				error = "string is null";
				return null;
			}
			if (text.Length % 2 != 0)
			{
				error = "string contains an odd number of characters";
				return null;
			}
			byte[] array = new byte[text.Length / 2];
			for (int i = 0; i < text.Length; i += 2)
			{
				string text2 = text.Substring(i, 2);
				if (!TryParseByte(text2, NumberStyles.HexNumber, out array[i / 2]))
				{
					error = string.Format(CultureInfo.CurrentCulture, "string contains \"{0}\", which cannot be converted to a byte value", new object[1] { text2 });
					return null;
				}
			}
			return array;
		}

		private bool GetDefaultPooling()
		{
			bool flag = false;
			if (flag)
			{
				if ((_flags & SQLiteConnectionFlags.NoConnectionPool) == SQLiteConnectionFlags.NoConnectionPool)
				{
					flag = false;
				}
				if ((_flags & SQLiteConnectionFlags.UseConnectionPool) == SQLiteConnectionFlags.UseConnectionPool)
				{
					flag = true;
				}
			}
			else
			{
				if ((_flags & SQLiteConnectionFlags.UseConnectionPool) == SQLiteConnectionFlags.UseConnectionPool)
				{
					flag = true;
				}
				if ((_flags & SQLiteConnectionFlags.NoConnectionPool) == SQLiteConnectionFlags.NoConnectionPool)
				{
					flag = false;
				}
			}
			return flag;
		}

		private IsolationLevel GetEffectiveIsolationLevel(IsolationLevel isolationLevel)
		{
			if ((_flags & SQLiteConnectionFlags.MapIsolationLevels) != SQLiteConnectionFlags.MapIsolationLevels)
			{
				return isolationLevel;
			}
			switch (isolationLevel)
			{
			case IsolationLevel.Unspecified:
			case IsolationLevel.Chaos:
			case IsolationLevel.ReadUncommitted:
			case IsolationLevel.ReadCommitted:
				return IsolationLevel.ReadCommitted;
			case IsolationLevel.RepeatableRead:
			case IsolationLevel.Serializable:
			case IsolationLevel.Snapshot:
				return IsolationLevel.Serializable;
			default:
				return GetFallbackDefaultIsolationLevel();
			}
		}

		public override void Open()
		{
			CheckDisposed();
			OnChanged(this, new ConnectionEventArgs(SQLiteConnectionEventType.Opening, null, null, null, null, null, null, null));
			if (_connectionState != ConnectionState.Closed)
			{
				throw new InvalidOperationException();
			}
			Close();
			SortedList<string, string> sortedList = ParseConnectionString(_connectionString, _parseViaFramework, allowNameOnly: false);
			OnChanged(this, new ConnectionEventArgs(SQLiteConnectionEventType.ConnectionString, null, null, null, null, null, _connectionString, new object[1] { sortedList }));
			object obj = TryParseEnum(typeof(SQLiteConnectionFlags), FindKey(sortedList, "Flags", DefaultFlags.ToString()), ignoreCase: true);
			_flags = ((obj is SQLiteConnectionFlags) ? ((SQLiteConnectionFlags)obj) : DefaultFlags);
			if (!SQLiteConvert.ToBoolean(FindKey(sortedList, "NoSharedFlags", false.ToString())))
			{
				lock (_syncRoot)
				{
					_flags |= _sharedFlags;
				}
			}
			obj = TryParseEnum(typeof(DbType), FindKey(sortedList, "DefaultDbType", null), ignoreCase: true);
			_defaultDbType = ((obj is DbType) ? new DbType?((DbType)obj) : ((DbType?)null));
			if (_defaultDbType.HasValue && _defaultDbType.Value == (DbType)(-1))
			{
				_defaultDbType = null;
			}
			_defaultTypeName = FindKey(sortedList, "DefaultTypeName", null);
			bool flag = false;
			bool flag2 = false;
			if (Convert.ToInt32(FindKey(sortedList, "Version", 3.ToString()), CultureInfo.InvariantCulture) != 3)
			{
				throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, "Only SQLite Version {0} is supported at this time", new object[1] { 3 }));
			}
			string text = FindKey(sortedList, "Data Source", null);
			if (string.IsNullOrEmpty(text))
			{
				text = FindKey(sortedList, "Uri", null);
				if (string.IsNullOrEmpty(text))
				{
					text = FindKey(sortedList, "FullUri", null);
					if (string.IsNullOrEmpty(text))
					{
						throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Data Source cannot be empty.  Use {0} to open an in-memory database", new object[1] { ":memory:" }));
					}
					flag2 = true;
				}
				else
				{
					text = MapUriPath(text);
					flag = true;
				}
			}
			bool flag3 = string.Compare(text, ":memory:", StringComparison.OrdinalIgnoreCase) == 0;
			if ((_flags & SQLiteConnectionFlags.TraceWarning) == SQLiteConnectionFlags.TraceWarning && !flag && !flag2 && !flag3 && !string.IsNullOrEmpty(text) && text.StartsWith("\\", StringComparison.OrdinalIgnoreCase) && !text.StartsWith("\\\\", StringComparison.OrdinalIgnoreCase))
			{
				System.Diagnostics.Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "WARNING: Detected a possibly malformed UNC database file name \"{0}\" that may have originally started with two backslashes; however, four leading backslashes may be required, e.g.: \"Data Source=\\\\\\{0};\"", new object[1] { text }));
			}
			if (!flag2)
			{
				if (flag3)
				{
					text = ":memory:";
				}
				else
				{
					bool toFullPath = SQLiteConvert.ToBoolean(FindKey(sortedList, "ToFullPath", true.ToString()));
					text = ExpandFileName(text, toFullPath);
				}
			}
			try
			{
				bool usePool = SQLiteConvert.ToBoolean(FindKey(sortedList, "Pooling", GetDefaultPooling().ToString()));
				int maxPoolSize = Convert.ToInt32(FindKey(sortedList, "Max Pool Size", 100.ToString()), CultureInfo.InvariantCulture);
				_defaultTimeout = Convert.ToInt32(FindKey(sortedList, "Default Timeout", 30.ToString()), CultureInfo.InvariantCulture);
				_prepareRetries = Convert.ToInt32(FindKey(sortedList, "PrepareRetries", 3.ToString()), CultureInfo.InvariantCulture);
				obj = TryParseEnum(typeof(IsolationLevel), FindKey(sortedList, "Default IsolationLevel", IsolationLevel.Serializable.ToString()), ignoreCase: true);
				_defaultIsolation = ((obj is IsolationLevel) ? ((IsolationLevel)obj) : IsolationLevel.Serializable);
				_defaultIsolation = GetEffectiveIsolationLevel(_defaultIsolation);
				if (_defaultIsolation != IsolationLevel.Serializable && _defaultIsolation != IsolationLevel.ReadCommitted)
				{
					throw new NotSupportedException("Invalid Default IsolationLevel specified");
				}
				_baseSchemaName = FindKey(sortedList, "BaseSchemaName", "sqlite_default_schema");
				if (_sql == null)
				{
					SetupSQLiteBase(sortedList);
				}
				SQLiteOpenFlagsEnum sQLiteOpenFlagsEnum = SQLiteOpenFlagsEnum.None;
				if (!SQLiteConvert.ToBoolean(FindKey(sortedList, "FailIfMissing", false.ToString())))
				{
					sQLiteOpenFlagsEnum |= SQLiteOpenFlagsEnum.Create;
				}
				if (SQLiteConvert.ToBoolean(FindKey(sortedList, "Read Only", false.ToString())))
				{
					sQLiteOpenFlagsEnum |= SQLiteOpenFlagsEnum.ReadOnly;
					sQLiteOpenFlagsEnum &= ~SQLiteOpenFlagsEnum.Create;
				}
				else
				{
					sQLiteOpenFlagsEnum |= SQLiteOpenFlagsEnum.ReadWrite;
				}
				if (flag2)
				{
					sQLiteOpenFlagsEnum |= SQLiteOpenFlagsEnum.Uri;
				}
				_sql.Open(text, _flags, sQLiteOpenFlagsEnum, maxPoolSize, usePool);
				_binaryGuid = SQLiteConvert.ToBoolean(FindKey(sortedList, "BinaryGUID", true.ToString()));
				string text2 = FindKey(sortedList, "HexPassword", null);
				if (text2 != null)
				{
					string error = null;
					byte[] array = FromHexString(text2, ref error);
					if (array == null)
					{
						throw new FormatException(string.Format(CultureInfo.CurrentCulture, "Cannot parse 'HexPassword' property value into byte values: {0}", new object[1] { error }));
					}
					_sql.SetPassword(array);
				}
				else
				{
					string text3 = FindKey(sortedList, "Password", null);
					if (text3 != null)
					{
						_sql.SetPassword(Encoding.UTF8.GetBytes(text3));
					}
					else if (_password != null)
					{
						_sql.SetPassword(_password);
					}
				}
				_password = null;
				if (!flag2)
				{
					_dataSource = Path.GetFileNameWithoutExtension(text);
				}
				else
				{
					_dataSource = text;
				}
				_version++;
				ConnectionState connectionState = _connectionState;
				_connectionState = ConnectionState.Open;
				try
				{
					string source = FindKey(sortedList, "SetDefaults", true.ToString());
					if (SQLiteConvert.ToBoolean(source))
					{
						using SQLiteCommand sQLiteCommand = CreateCommand();
						int num;
						if (!flag2 && !flag3)
						{
							source = FindKey(sortedList, "Page Size", 1024.ToString());
							num = Convert.ToInt32(source, CultureInfo.InvariantCulture);
							if (num != 1024)
							{
								sQLiteCommand.CommandText = string.Format(CultureInfo.InvariantCulture, "PRAGMA page_size={0}", new object[1] { num });
								sQLiteCommand.ExecuteNonQuery();
							}
						}
						source = FindKey(sortedList, "Max Page Count", 0.ToString());
						num = Convert.ToInt32(source, CultureInfo.InvariantCulture);
						if (num != 0)
						{
							sQLiteCommand.CommandText = string.Format(CultureInfo.InvariantCulture, "PRAGMA max_page_count={0}", new object[1] { num });
							sQLiteCommand.ExecuteNonQuery();
						}
						source = FindKey(sortedList, "Legacy Format", false.ToString());
						bool flag4 = SQLiteConvert.ToBoolean(source);
						if (flag4)
						{
							sQLiteCommand.CommandText = string.Format(CultureInfo.InvariantCulture, "PRAGMA legacy_file_format={0}", new object[1] { flag4 ? "ON" : "OFF" });
							sQLiteCommand.ExecuteNonQuery();
						}
						source = FindKey(sortedList, "Synchronous", SQLiteSynchronousEnum.Default.ToString());
						obj = TryParseEnum(typeof(SQLiteSynchronousEnum), source, ignoreCase: true);
						if (!(obj is SQLiteSynchronousEnum) || (SQLiteSynchronousEnum)obj != SQLiteSynchronousEnum.Default)
						{
							sQLiteCommand.CommandText = string.Format(CultureInfo.InvariantCulture, "PRAGMA synchronous={0}", new object[1] { source });
							sQLiteCommand.ExecuteNonQuery();
						}
						source = FindKey(sortedList, "Cache Size", 2000.ToString());
						num = Convert.ToInt32(source, CultureInfo.InvariantCulture);
						if (num != 2000)
						{
							sQLiteCommand.CommandText = string.Format(CultureInfo.InvariantCulture, "PRAGMA cache_size={0}", new object[1] { num });
							sQLiteCommand.ExecuteNonQuery();
						}
						source = FindKey(sortedList, "Journal Mode", SQLiteJournalModeEnum.Default.ToString());
						obj = TryParseEnum(typeof(SQLiteJournalModeEnum), source, ignoreCase: true);
						if (!(obj is SQLiteJournalModeEnum) || (SQLiteJournalModeEnum)obj != SQLiteJournalModeEnum.Default)
						{
							sQLiteCommand.CommandText = string.Format(CultureInfo.InvariantCulture, "PRAGMA journal_mode={0}", new object[1] { source });
							sQLiteCommand.ExecuteNonQuery();
						}
						source = FindKey(sortedList, "Foreign Keys", false.ToString());
						flag4 = SQLiteConvert.ToBoolean(source);
						if (flag4)
						{
							sQLiteCommand.CommandText = string.Format(CultureInfo.InvariantCulture, "PRAGMA foreign_keys={0}", new object[1] { flag4 ? "ON" : "OFF" });
							sQLiteCommand.ExecuteNonQuery();
						}
					}
					if (this._authorizerHandler != null)
					{
						_sql.SetAuthorizerHook(_authorizerCallback);
					}
					if (this._commitHandler != null)
					{
						_sql.SetCommitHook(_commitCallback);
					}
					if (this._updateHandler != null)
					{
						_sql.SetUpdateHook(_updateCallback);
					}
					if (this._rollbackHandler != null)
					{
						_sql.SetRollbackHook(_rollbackCallback);
					}
					Transaction current = Transaction.Current;
					if (current != null && SQLiteConvert.ToBoolean(FindKey(sortedList, "Enlist", true.ToString())))
					{
						EnlistTransaction(current);
					}
					_connectionState = connectionState;
					StateChangeEventArgs eventArgs = null;
					OnStateChange(ConnectionState.Open, ref eventArgs);
					OnChanged(this, new ConnectionEventArgs(SQLiteConnectionEventType.Opened, eventArgs, null, null, null, null, null, null));
				}
				catch
				{
					_connectionState = connectionState;
					throw;
				}
			}
			catch (SQLiteException)
			{
				Close();
				throw;
			}
		}

		public SQLiteConnection OpenAndReturn()
		{
			CheckDisposed();
			Open();
			return this;
		}

		public void Cancel()
		{
			CheckDisposed();
			if (_sql == null)
			{
				throw new InvalidOperationException("Database connection not valid for query cancellation.");
			}
			_sql.Cancel();
		}

		public static void GetMemoryStatistics(ref IDictionary<string, long> statistics)
		{
			if (statistics == null)
			{
				statistics = new Dictionary<string, long>();
			}
			statistics["MemoryUsed"] = SQLite3.StaticMemoryUsed;
			statistics["MemoryHighwater"] = SQLite3.StaticMemoryHighwater;
		}

		public void ReleaseMemory()
		{
			CheckDisposed();
			if (_sql == null)
			{
				throw new InvalidOperationException("Database connection not valid for releasing memory.");
			}
			SQLiteErrorCode sQLiteErrorCode = _sql.ReleaseMemory();
			if (sQLiteErrorCode != SQLiteErrorCode.Ok)
			{
				throw new SQLiteException(sQLiteErrorCode, _sql.GetLastError("Could not release connection memory."));
			}
		}

		public static SQLiteErrorCode ReleaseMemory(int nBytes, bool reset, bool compact, ref int nFree, ref bool resetOk, ref uint nLargest)
		{
			return SQLite3.StaticReleaseMemory(nBytes, reset, compact, ref nFree, ref resetOk, ref nLargest);
		}

		public static SQLiteErrorCode SetMemoryStatus(bool value)
		{
			return SQLite3.StaticSetMemoryStatus(value);
		}

		public SQLiteErrorCode Shutdown()
		{
			CheckDisposed();
			if (_sql == null)
			{
				throw new InvalidOperationException("Database connection not valid for shutdown.");
			}
			_sql.Close(canThrow: true);
			return _sql.Shutdown();
		}

		public static void Shutdown(bool directories, bool noThrow)
		{
			SQLiteErrorCode sQLiteErrorCode = SQLite3.StaticShutdown(directories);
			if (sQLiteErrorCode != SQLiteErrorCode.Ok && !noThrow)
			{
				throw new SQLiteException(sQLiteErrorCode, null);
			}
		}

		public void SetExtendedResultCodes(bool bOnOff)
		{
			CheckDisposed();
			if (_sql != null)
			{
				_sql.SetExtendedResultCodes(bOnOff);
			}
		}

		public SQLiteErrorCode ResultCode()
		{
			CheckDisposed();
			if (_sql == null)
			{
				throw new InvalidOperationException("Database connection not valid for getting result code.");
			}
			return _sql.ResultCode();
		}

		public SQLiteErrorCode ExtendedResultCode()
		{
			CheckDisposed();
			if (_sql == null)
			{
				throw new InvalidOperationException("Database connection not valid for getting extended result code.");
			}
			return _sql.ExtendedResultCode();
		}

		public void LogMessage(SQLiteErrorCode iErrCode, string zMessage)
		{
			CheckDisposed();
			if (_sql == null)
			{
				throw new InvalidOperationException("Database connection not valid for logging message.");
			}
			_sql.LogMessage(iErrCode, zMessage);
		}

		public void LogMessage(int iErrCode, string zMessage)
		{
			CheckDisposed();
			if (_sql == null)
			{
				throw new InvalidOperationException("Database connection not valid for logging message.");
			}
			_sql.LogMessage((SQLiteErrorCode)iErrCode, zMessage);
		}

		public void ChangePassword(string newPassword)
		{
			CheckDisposed();
			ChangePassword(string.IsNullOrEmpty(newPassword) ? null : Encoding.UTF8.GetBytes(newPassword));
		}

		public void ChangePassword(byte[] newPassword)
		{
			CheckDisposed();
			if (_connectionState != ConnectionState.Open)
			{
				throw new InvalidOperationException("Database must be opened before changing the password.");
			}
			_sql.ChangePassword(newPassword);
		}

		public void SetPassword(string databasePassword)
		{
			CheckDisposed();
			SetPassword(string.IsNullOrEmpty(databasePassword) ? null : Encoding.UTF8.GetBytes(databasePassword));
		}

		public void SetPassword(byte[] databasePassword)
		{
			CheckDisposed();
			if (_connectionState != ConnectionState.Closed)
			{
				throw new InvalidOperationException("Password can only be set before the database is opened.");
			}
			if (databasePassword != null && databasePassword.Length == 0)
			{
				databasePassword = null;
			}
			_password = databasePassword;
		}

		public SQLiteErrorCode SetAvRetry(ref int count, ref int interval)
		{
			CheckDisposed();
			if (_connectionState != ConnectionState.Open)
			{
				throw new InvalidOperationException("Database must be opened before changing the AV retry parameters.");
			}
			IntPtr intPtr = IntPtr.Zero;
			SQLiteErrorCode sQLiteErrorCode;
			try
			{
				intPtr = Marshal.AllocHGlobal(8);
				Marshal.WriteInt32(intPtr, 0, count);
				Marshal.WriteInt32(intPtr, 4, interval);
				sQLiteErrorCode = _sql.FileControl(null, 9, intPtr);
				if (sQLiteErrorCode == SQLiteErrorCode.Ok)
				{
					count = Marshal.ReadInt32(intPtr, 0);
					interval = Marshal.ReadInt32(intPtr, 4);
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return sQLiteErrorCode;
		}

		public SQLiteErrorCode SetChunkSize(int size)
		{
			CheckDisposed();
			if (_connectionState != ConnectionState.Open)
			{
				throw new InvalidOperationException("Database must be opened before changing the chunk size.");
			}
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.AllocHGlobal(4);
				Marshal.WriteInt32(intPtr, 0, size);
				return _sql.FileControl(null, 6, intPtr);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		private static string UnwrapString(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			int length = value.Length;
			if ((value[0] == '\'' && value[length - 1] == '\'') || (value[0] == '"' && value[length - 1] == '"'))
			{
				return value.Substring(1, length - 2);
			}
			return value;
		}

		private string ExpandFileName(string sourceFile, bool toFullPath)
		{
			if (string.IsNullOrEmpty(sourceFile))
			{
				return sourceFile;
			}
			if (sourceFile.StartsWith("|DataDirectory|", StringComparison.OrdinalIgnoreCase))
			{
				string text = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
				if (string.IsNullOrEmpty(text))
				{
					text = AppDomain.CurrentDomain.BaseDirectory;
				}
				if (sourceFile.Length > "|DataDirectory|".Length && (sourceFile["|DataDirectory|".Length] == Path.DirectorySeparatorChar || sourceFile["|DataDirectory|".Length] == Path.AltDirectorySeparatorChar))
				{
					sourceFile = sourceFile.Remove("|DataDirectory|".Length, 1);
				}
				sourceFile = Path.Combine(text, sourceFile.Substring("|DataDirectory|".Length));
			}
			if (toFullPath)
			{
				sourceFile = Path.GetFullPath(sourceFile);
			}
			return sourceFile;
		}

		public override DataTable GetSchema()
		{
			CheckDisposed();
			return GetSchema("MetaDataCollections", null);
		}

		public override DataTable GetSchema(string collectionName)
		{
			CheckDisposed();
			return GetSchema(collectionName, new string[0]);
		}

		public override DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			CheckDisposed();
			if (_connectionState != ConnectionState.Open)
			{
				throw new InvalidOperationException();
			}
			string[] array = new string[5];
			if (restrictionValues == null)
			{
				restrictionValues = new string[0];
			}
			restrictionValues.CopyTo(array, 0);
			switch (collectionName.ToUpper(CultureInfo.InvariantCulture))
			{
			case "METADATACOLLECTIONS":
				return Schema_MetaDataCollections();
			case "DATASOURCEINFORMATION":
				return Schema_DataSourceInformation();
			case "DATATYPES":
				return Schema_DataTypes();
			case "COLUMNS":
			case "TABLECOLUMNS":
				return Schema_Columns(array[0], array[2], array[3]);
			case "INDEXES":
				return Schema_Indexes(array[0], array[2], array[3]);
			case "TRIGGERS":
				return Schema_Triggers(array[0], array[2], array[3]);
			case "INDEXCOLUMNS":
				return Schema_IndexColumns(array[0], array[2], array[3], array[4]);
			case "TABLES":
				return Schema_Tables(array[0], array[2], array[3]);
			case "VIEWS":
				return Schema_Views(array[0], array[2]);
			case "VIEWCOLUMNS":
				return Schema_ViewColumns(array[0], array[2], array[3]);
			case "FOREIGNKEYS":
				return Schema_ForeignKeys(array[0], array[2], array[3]);
			case "CATALOGS":
				return Schema_Catalogs(array[0]);
			case "RESERVEDWORDS":
				return Schema_ReservedWords();
			default:
				throw new NotSupportedException();
			}
		}

		private static DataTable Schema_ReservedWords()
		{
			DataTable dataTable = new DataTable("ReservedWords");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("ReservedWord", typeof(string));
			dataTable.Columns.Add("MaximumVersion", typeof(string));
			dataTable.Columns.Add("MinimumVersion", typeof(string));
			dataTable.BeginLoadData();
			string[] array = SR.Keywords.Split(new char[1] { ',' });
			foreach (string value in array)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow[0] = value;
				dataTable.Rows.Add(dataRow);
			}
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		private static DataTable Schema_MetaDataCollections()
		{
			DataTable dataTable = new DataTable("MetaDataCollections");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("CollectionName", typeof(string));
			dataTable.Columns.Add("NumberOfRestrictions", typeof(int));
			dataTable.Columns.Add("NumberOfIdentifierParts", typeof(int));
			dataTable.BeginLoadData();
			StringReader stringReader = new StringReader(SR.MetaDataCollections);
			dataTable.ReadXml(stringReader);
			stringReader.Close();
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		private DataTable Schema_DataSourceInformation()
		{
			DataTable dataTable = new DataTable("DataSourceInformation");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add(DbMetaDataColumnNames.CompositeIdentifierSeparatorPattern, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.DataSourceProductName, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.DataSourceProductVersion, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.DataSourceProductVersionNormalized, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.GroupByBehavior, typeof(int));
			dataTable.Columns.Add(DbMetaDataColumnNames.IdentifierPattern, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.IdentifierCase, typeof(int));
			dataTable.Columns.Add(DbMetaDataColumnNames.OrderByColumnsInSelect, typeof(bool));
			dataTable.Columns.Add(DbMetaDataColumnNames.ParameterMarkerFormat, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.ParameterMarkerPattern, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.ParameterNameMaxLength, typeof(int));
			dataTable.Columns.Add(DbMetaDataColumnNames.ParameterNamePattern, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.QuotedIdentifierPattern, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.QuotedIdentifierCase, typeof(int));
			dataTable.Columns.Add(DbMetaDataColumnNames.StatementSeparatorPattern, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.StringLiteralPattern, typeof(string));
			dataTable.Columns.Add(DbMetaDataColumnNames.SupportedJoinOperators, typeof(int));
			dataTable.BeginLoadData();
			DataRow dataRow = dataTable.NewRow();
			dataRow.ItemArray = new object[17]
			{
				null, "SQLite", _sql.Version, _sql.Version, 3, "(^\\[\\p{Lo}\\p{Lu}\\p{Ll}_@#][\\p{Lo}\\p{Lu}\\p{Ll}\\p{Nd}@$#_]*$)|(^\\[[^\\]\\0]|\\]\\]+\\]$)|(^\\\"[^\\\"\\0]|\\\"\\\"+\\\"$)", 1, false, "{0}", "@[\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}_@#][\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}\\p{Nd}\\uff3f_@#\\$]*(?=\\s+|$)",
				255, "^[\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}_@#][\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}\\p{Nd}\\uff3f_@#\\$]*(?=\\s+|$)", "(([^\\[]|\\]\\])*)", 1, ";", "'(([^']|'')*)'", 15
			};
			dataTable.Rows.Add(dataRow);
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		private DataTable Schema_Columns(string strCatalog, string strTable, string strColumn)
		{
			DataTable dataTable = new DataTable("Columns");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("TABLE_CATALOG", typeof(string));
			dataTable.Columns.Add("TABLE_SCHEMA", typeof(string));
			dataTable.Columns.Add("TABLE_NAME", typeof(string));
			dataTable.Columns.Add("COLUMN_NAME", typeof(string));
			dataTable.Columns.Add("COLUMN_GUID", typeof(Guid));
			dataTable.Columns.Add("COLUMN_PROPID", typeof(long));
			dataTable.Columns.Add("ORDINAL_POSITION", typeof(int));
			dataTable.Columns.Add("COLUMN_HASDEFAULT", typeof(bool));
			dataTable.Columns.Add("COLUMN_DEFAULT", typeof(string));
			dataTable.Columns.Add("COLUMN_FLAGS", typeof(long));
			dataTable.Columns.Add("IS_NULLABLE", typeof(bool));
			dataTable.Columns.Add("DATA_TYPE", typeof(string));
			dataTable.Columns.Add("TYPE_GUID", typeof(Guid));
			dataTable.Columns.Add("CHARACTER_MAXIMUM_LENGTH", typeof(int));
			dataTable.Columns.Add("CHARACTER_OCTET_LENGTH", typeof(int));
			dataTable.Columns.Add("NUMERIC_PRECISION", typeof(int));
			dataTable.Columns.Add("NUMERIC_SCALE", typeof(int));
			dataTable.Columns.Add("DATETIME_PRECISION", typeof(long));
			dataTable.Columns.Add("CHARACTER_SET_CATALOG", typeof(string));
			dataTable.Columns.Add("CHARACTER_SET_SCHEMA", typeof(string));
			dataTable.Columns.Add("CHARACTER_SET_NAME", typeof(string));
			dataTable.Columns.Add("COLLATION_CATALOG", typeof(string));
			dataTable.Columns.Add("COLLATION_SCHEMA", typeof(string));
			dataTable.Columns.Add("COLLATION_NAME", typeof(string));
			dataTable.Columns.Add("DOMAIN_CATALOG", typeof(string));
			dataTable.Columns.Add("DOMAIN_NAME", typeof(string));
			dataTable.Columns.Add("DESCRIPTION", typeof(string));
			dataTable.Columns.Add("PRIMARY_KEY", typeof(bool));
			dataTable.Columns.Add("EDM_TYPE", typeof(string));
			dataTable.Columns.Add("AUTOINCREMENT", typeof(bool));
			dataTable.Columns.Add("UNIQUE", typeof(bool));
			dataTable.BeginLoadData();
			if (string.IsNullOrEmpty(strCatalog))
			{
				strCatalog = "main";
			}
			string text = ((string.Compare(strCatalog, "temp", StringComparison.OrdinalIgnoreCase) == 0) ? "sqlite_temp_master" : "sqlite_master");
			using (SQLiteCommand sQLiteCommand = new SQLiteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{1}] WHERE [type] LIKE 'table' OR [type] LIKE 'view'", new object[2] { strCatalog, text }), this))
			{
				using SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader();
				while (sQLiteDataReader.Read())
				{
					if (!string.IsNullOrEmpty(strTable) && string.Compare(strTable, sQLiteDataReader.GetString(2), StringComparison.OrdinalIgnoreCase) != 0)
					{
						continue;
					}
					try
					{
						using SQLiteCommand sQLiteCommand2 = new SQLiteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{1}]", new object[2]
						{
							strCatalog,
							sQLiteDataReader.GetString(2)
						}), this);
						using SQLiteDataReader sQLiteDataReader2 = sQLiteCommand2.ExecuteReader(CommandBehavior.SchemaOnly);
						using DataTable dataTable2 = sQLiteDataReader2.GetSchemaTable(wantUniqueInfo: true, wantDefaultValue: true);
						foreach (DataRow row in dataTable2.Rows)
						{
							if (string.Compare(row[SchemaTableColumn.ColumnName].ToString(), strColumn, StringComparison.OrdinalIgnoreCase) == 0 || strColumn == null)
							{
								DataRow dataRow2 = dataTable.NewRow();
								dataRow2["NUMERIC_PRECISION"] = row[SchemaTableColumn.NumericPrecision];
								dataRow2["NUMERIC_SCALE"] = row[SchemaTableColumn.NumericScale];
								dataRow2["TABLE_NAME"] = sQLiteDataReader.GetString(2);
								dataRow2["COLUMN_NAME"] = row[SchemaTableColumn.ColumnName];
								dataRow2["TABLE_CATALOG"] = strCatalog;
								dataRow2["ORDINAL_POSITION"] = row[SchemaTableColumn.ColumnOrdinal];
								dataRow2["COLUMN_HASDEFAULT"] = row[SchemaTableOptionalColumn.DefaultValue] != DBNull.Value;
								dataRow2["COLUMN_DEFAULT"] = row[SchemaTableOptionalColumn.DefaultValue];
								dataRow2["IS_NULLABLE"] = row[SchemaTableColumn.AllowDBNull];
								dataRow2["DATA_TYPE"] = row["DataTypeName"].ToString().ToLower(CultureInfo.InvariantCulture);
								dataRow2["EDM_TYPE"] = SQLiteConvert.DbTypeToTypeName(this, (DbType)row[SchemaTableColumn.ProviderType], _flags).ToString().ToLower(CultureInfo.InvariantCulture);
								dataRow2["CHARACTER_MAXIMUM_LENGTH"] = row[SchemaTableColumn.ColumnSize];
								dataRow2["TABLE_SCHEMA"] = row[SchemaTableColumn.BaseSchemaName];
								dataRow2["PRIMARY_KEY"] = row[SchemaTableColumn.IsKey];
								dataRow2["AUTOINCREMENT"] = row[SchemaTableOptionalColumn.IsAutoIncrement];
								dataRow2["COLLATION_NAME"] = row["CollationType"];
								dataRow2["UNIQUE"] = row[SchemaTableColumn.IsUnique];
								dataTable.Rows.Add(dataRow2);
							}
						}
					}
					catch (SQLiteException)
					{
					}
				}
			}
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		private DataTable Schema_Indexes(string strCatalog, string strTable, string strIndex)
		{
			DataTable dataTable = new DataTable("Indexes");
			List<int> list = new List<int>();
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("TABLE_CATALOG", typeof(string));
			dataTable.Columns.Add("TABLE_SCHEMA", typeof(string));
			dataTable.Columns.Add("TABLE_NAME", typeof(string));
			dataTable.Columns.Add("INDEX_CATALOG", typeof(string));
			dataTable.Columns.Add("INDEX_SCHEMA", typeof(string));
			dataTable.Columns.Add("INDEX_NAME", typeof(string));
			dataTable.Columns.Add("PRIMARY_KEY", typeof(bool));
			dataTable.Columns.Add("UNIQUE", typeof(bool));
			dataTable.Columns.Add("CLUSTERED", typeof(bool));
			dataTable.Columns.Add("TYPE", typeof(int));
			dataTable.Columns.Add("FILL_FACTOR", typeof(int));
			dataTable.Columns.Add("INITIAL_SIZE", typeof(int));
			dataTable.Columns.Add("NULLS", typeof(int));
			dataTable.Columns.Add("SORT_BOOKMARKS", typeof(bool));
			dataTable.Columns.Add("AUTO_UPDATE", typeof(bool));
			dataTable.Columns.Add("NULL_COLLATION", typeof(int));
			dataTable.Columns.Add("ORDINAL_POSITION", typeof(int));
			dataTable.Columns.Add("COLUMN_NAME", typeof(string));
			dataTable.Columns.Add("COLUMN_GUID", typeof(Guid));
			dataTable.Columns.Add("COLUMN_PROPID", typeof(long));
			dataTable.Columns.Add("COLLATION", typeof(short));
			dataTable.Columns.Add("CARDINALITY", typeof(decimal));
			dataTable.Columns.Add("PAGES", typeof(int));
			dataTable.Columns.Add("FILTER_CONDITION", typeof(string));
			dataTable.Columns.Add("INTEGRATED", typeof(bool));
			dataTable.Columns.Add("INDEX_DEFINITION", typeof(string));
			dataTable.BeginLoadData();
			if (string.IsNullOrEmpty(strCatalog))
			{
				strCatalog = "main";
			}
			string text = ((string.Compare(strCatalog, "temp", StringComparison.OrdinalIgnoreCase) == 0) ? "sqlite_temp_master" : "sqlite_master");
			using (SQLiteCommand sQLiteCommand = new SQLiteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{1}] WHERE [type] LIKE 'table'", new object[2] { strCatalog, text }), this))
			{
				using SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader();
				while (sQLiteDataReader.Read())
				{
					bool flag = false;
					list.Clear();
					if (!string.IsNullOrEmpty(strTable) && string.Compare(sQLiteDataReader.GetString(2), strTable, StringComparison.OrdinalIgnoreCase) != 0)
					{
						continue;
					}
					try
					{
						using SQLiteCommand sQLiteCommand2 = new SQLiteCommand(string.Format(CultureInfo.InvariantCulture, "PRAGMA [{0}].table_info([{1}])", new object[2]
						{
							strCatalog,
							sQLiteDataReader.GetString(2)
						}), this);
						using SQLiteDataReader sQLiteDataReader2 = sQLiteCommand2.ExecuteReader();
						while (sQLiteDataReader2.Read())
						{
							if (sQLiteDataReader2.GetInt32(5) != 0)
							{
								list.Add(sQLiteDataReader2.GetInt32(0));
								if (string.Compare(sQLiteDataReader2.GetString(2), "INTEGER", StringComparison.OrdinalIgnoreCase) == 0)
								{
									flag = true;
								}
							}
						}
					}
					catch (SQLiteException)
					{
					}
					if (list.Count == 1 && flag)
					{
						DataRow dataRow = dataTable.NewRow();
						dataRow["TABLE_CATALOG"] = strCatalog;
						dataRow["TABLE_NAME"] = sQLiteDataReader.GetString(2);
						dataRow["INDEX_CATALOG"] = strCatalog;
						dataRow["PRIMARY_KEY"] = true;
						dataRow["INDEX_NAME"] = string.Format(CultureInfo.InvariantCulture, "{1}_PK_{0}", new object[2]
						{
							sQLiteDataReader.GetString(2),
							text
						});
						dataRow["UNIQUE"] = true;
						if (string.Compare((string)dataRow["INDEX_NAME"], strIndex, StringComparison.OrdinalIgnoreCase) == 0 || strIndex == null)
						{
							dataTable.Rows.Add(dataRow);
						}
						list.Clear();
					}
					try
					{
						using SQLiteCommand sQLiteCommand3 = new SQLiteCommand(string.Format(CultureInfo.InvariantCulture, "PRAGMA [{0}].index_list([{1}])", new object[2]
						{
							strCatalog,
							sQLiteDataReader.GetString(2)
						}), this);
						using SQLiteDataReader sQLiteDataReader3 = sQLiteCommand3.ExecuteReader();
						while (sQLiteDataReader3.Read())
						{
							if (string.Compare(sQLiteDataReader3.GetString(1), strIndex, StringComparison.OrdinalIgnoreCase) != 0 && strIndex != null)
							{
								continue;
							}
							DataRow dataRow = dataTable.NewRow();
							dataRow["TABLE_CATALOG"] = strCatalog;
							dataRow["TABLE_NAME"] = sQLiteDataReader.GetString(2);
							dataRow["INDEX_CATALOG"] = strCatalog;
							dataRow["INDEX_NAME"] = sQLiteDataReader3.GetString(1);
							dataRow["UNIQUE"] = SQLiteConvert.ToBoolean(sQLiteDataReader3.GetValue(2), CultureInfo.InvariantCulture, viaFramework: false);
							dataRow["PRIMARY_KEY"] = false;
							using (SQLiteCommand sQLiteCommand4 = new SQLiteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{2}] WHERE [type] LIKE 'index' AND [name] LIKE '{1}'", new object[3]
							{
								strCatalog,
								sQLiteDataReader3.GetString(1).Replace("'", "''"),
								text
							}), this))
							{
								using SQLiteDataReader sQLiteDataReader4 = sQLiteCommand4.ExecuteReader();
								if (sQLiteDataReader4.Read() && !sQLiteDataReader4.IsDBNull(4))
								{
									dataRow["INDEX_DEFINITION"] = sQLiteDataReader4.GetString(4);
								}
							}
							if (list.Count > 0 && sQLiteDataReader3.GetString(1).StartsWith("sqlite_autoindex_" + sQLiteDataReader.GetString(2), StringComparison.InvariantCultureIgnoreCase))
							{
								using SQLiteCommand sQLiteCommand5 = new SQLiteCommand(string.Format(CultureInfo.InvariantCulture, "PRAGMA [{0}].index_info([{1}])", new object[2]
								{
									strCatalog,
									sQLiteDataReader3.GetString(1)
								}), this);
								using SQLiteDataReader sQLiteDataReader5 = sQLiteCommand5.ExecuteReader();
								int num = 0;
								while (sQLiteDataReader5.Read())
								{
									if (!list.Contains(sQLiteDataReader5.GetInt32(1)))
									{
										num = 0;
										break;
									}
									num++;
								}
								if (num == list.Count)
								{
									dataRow["PRIMARY_KEY"] = true;
									list.Clear();
								}
							}
							dataTable.Rows.Add(dataRow);
						}
					}
					catch (SQLiteException)
					{
					}
				}
			}
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		private DataTable Schema_Triggers(string catalog, string table, string triggerName)
		{
			DataTable dataTable = new DataTable("Triggers");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("TABLE_CATALOG", typeof(string));
			dataTable.Columns.Add("TABLE_SCHEMA", typeof(string));
			dataTable.Columns.Add("TABLE_NAME", typeof(string));
			dataTable.Columns.Add("TRIGGER_NAME", typeof(string));
			dataTable.Columns.Add("TRIGGER_DEFINITION", typeof(string));
			dataTable.BeginLoadData();
			if (string.IsNullOrEmpty(table))
			{
				table = null;
			}
			if (string.IsNullOrEmpty(catalog))
			{
				catalog = "main";
			}
			string text = ((string.Compare(catalog, "temp", StringComparison.OrdinalIgnoreCase) == 0) ? "sqlite_temp_master" : "sqlite_master");
			using (SQLiteCommand sQLiteCommand = new SQLiteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT [type], [name], [tbl_name], [rootpage], [sql], [rowid] FROM [{0}].[{1}] WHERE [type] LIKE 'trigger'", new object[2] { catalog, text }), this))
			{
				using SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader();
				while (sQLiteDataReader.Read())
				{
					if ((string.Compare(sQLiteDataReader.GetString(1), triggerName, StringComparison.OrdinalIgnoreCase) == 0 || triggerName == null) && (table == null || string.Compare(table, sQLiteDataReader.GetString(2), StringComparison.OrdinalIgnoreCase) == 0))
					{
						DataRow dataRow = dataTable.NewRow();
						dataRow["TABLE_CATALOG"] = catalog;
						dataRow["TABLE_NAME"] = sQLiteDataReader.GetString(2);
						dataRow["TRIGGER_NAME"] = sQLiteDataReader.GetString(1);
						dataRow["TRIGGER_DEFINITION"] = sQLiteDataReader.GetString(4);
						dataTable.Rows.Add(dataRow);
					}
				}
			}
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		private DataTable Schema_Tables(string strCatalog, string strTable, string strType)
		{
			DataTable dataTable = new DataTable("Tables");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("TABLE_CATALOG", typeof(string));
			dataTable.Columns.Add("TABLE_SCHEMA", typeof(string));
			dataTable.Columns.Add("TABLE_NAME", typeof(string));
			dataTable.Columns.Add("TABLE_TYPE", typeof(string));
			dataTable.Columns.Add("TABLE_ID", typeof(long));
			dataTable.Columns.Add("TABLE_ROOTPAGE", typeof(int));
			dataTable.Columns.Add("TABLE_DEFINITION", typeof(string));
			dataTable.BeginLoadData();
			if (string.IsNullOrEmpty(strCatalog))
			{
				strCatalog = "main";
			}
			string text = ((string.Compare(strCatalog, "temp", StringComparison.OrdinalIgnoreCase) == 0) ? "sqlite_temp_master" : "sqlite_master");
			using (SQLiteCommand sQLiteCommand = new SQLiteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT [type], [name], [tbl_name], [rootpage], [sql], [rowid] FROM [{0}].[{1}] WHERE [type] LIKE 'table'", new object[2] { strCatalog, text }), this))
			{
				using SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader();
				while (sQLiteDataReader.Read())
				{
					string text2 = sQLiteDataReader.GetString(0);
					if (string.Compare(sQLiteDataReader.GetString(2), 0, "SQLITE_", 0, 7, StringComparison.OrdinalIgnoreCase) == 0)
					{
						text2 = "SYSTEM_TABLE";
					}
					if ((string.Compare(strType, text2, StringComparison.OrdinalIgnoreCase) == 0 || strType == null) && (string.Compare(sQLiteDataReader.GetString(2), strTable, StringComparison.OrdinalIgnoreCase) == 0 || strTable == null))
					{
						DataRow dataRow = dataTable.NewRow();
						dataRow["TABLE_CATALOG"] = strCatalog;
						dataRow["TABLE_NAME"] = sQLiteDataReader.GetString(2);
						dataRow["TABLE_TYPE"] = text2;
						dataRow["TABLE_ID"] = sQLiteDataReader.GetInt64(5);
						dataRow["TABLE_ROOTPAGE"] = sQLiteDataReader.GetInt32(3);
						dataRow["TABLE_DEFINITION"] = sQLiteDataReader.GetString(4);
						dataTable.Rows.Add(dataRow);
					}
				}
			}
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		private DataTable Schema_Views(string strCatalog, string strView)
		{
			DataTable dataTable = new DataTable("Views");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("TABLE_CATALOG", typeof(string));
			dataTable.Columns.Add("TABLE_SCHEMA", typeof(string));
			dataTable.Columns.Add("TABLE_NAME", typeof(string));
			dataTable.Columns.Add("VIEW_DEFINITION", typeof(string));
			dataTable.Columns.Add("CHECK_OPTION", typeof(bool));
			dataTable.Columns.Add("IS_UPDATABLE", typeof(bool));
			dataTable.Columns.Add("DESCRIPTION", typeof(string));
			dataTable.Columns.Add("DATE_CREATED", typeof(DateTime));
			dataTable.Columns.Add("DATE_MODIFIED", typeof(DateTime));
			dataTable.BeginLoadData();
			if (string.IsNullOrEmpty(strCatalog))
			{
				strCatalog = "main";
			}
			string text = ((string.Compare(strCatalog, "temp", StringComparison.OrdinalIgnoreCase) == 0) ? "sqlite_temp_master" : "sqlite_master");
			using (SQLiteCommand sQLiteCommand = new SQLiteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{1}] WHERE [type] LIKE 'view'", new object[2] { strCatalog, text }), this))
			{
				using SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader();
				while (sQLiteDataReader.Read())
				{
					if (string.Compare(sQLiteDataReader.GetString(1), strView, StringComparison.OrdinalIgnoreCase) == 0 || string.IsNullOrEmpty(strView))
					{
						string text2 = sQLiteDataReader.GetString(4).Replace('\r', ' ').Replace('\n', ' ')
							.Replace('\t', ' ');
						int num = CultureInfo.InvariantCulture.CompareInfo.IndexOf(text2, " AS ", CompareOptions.IgnoreCase);
						if (num > -1)
						{
							text2 = text2.Substring(num + 4).Trim();
							DataRow dataRow = dataTable.NewRow();
							dataRow["TABLE_CATALOG"] = strCatalog;
							dataRow["TABLE_NAME"] = sQLiteDataReader.GetString(2);
							dataRow["IS_UPDATABLE"] = false;
							dataRow["VIEW_DEFINITION"] = text2;
							dataTable.Rows.Add(dataRow);
						}
					}
				}
			}
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		private DataTable Schema_Catalogs(string strCatalog)
		{
			DataTable dataTable = new DataTable("Catalogs");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("CATALOG_NAME", typeof(string));
			dataTable.Columns.Add("DESCRIPTION", typeof(string));
			dataTable.Columns.Add("ID", typeof(long));
			dataTable.BeginLoadData();
			using (SQLiteCommand sQLiteCommand = new SQLiteCommand("PRAGMA database_list", this))
			{
				using SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader();
				while (sQLiteDataReader.Read())
				{
					if (string.Compare(sQLiteDataReader.GetString(1), strCatalog, StringComparison.OrdinalIgnoreCase) == 0 || strCatalog == null)
					{
						DataRow dataRow = dataTable.NewRow();
						dataRow["CATALOG_NAME"] = sQLiteDataReader.GetString(1);
						dataRow["DESCRIPTION"] = sQLiteDataReader.GetString(2);
						dataRow["ID"] = sQLiteDataReader.GetInt64(0);
						dataTable.Rows.Add(dataRow);
					}
				}
			}
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		private DataTable Schema_DataTypes()
		{
			DataTable dataTable = new DataTable("DataTypes");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("TypeName", typeof(string));
			dataTable.Columns.Add("ProviderDbType", typeof(int));
			dataTable.Columns.Add("ColumnSize", typeof(long));
			dataTable.Columns.Add("CreateFormat", typeof(string));
			dataTable.Columns.Add("CreateParameters", typeof(string));
			dataTable.Columns.Add("DataType", typeof(string));
			dataTable.Columns.Add("IsAutoIncrementable", typeof(bool));
			dataTable.Columns.Add("IsBestMatch", typeof(bool));
			dataTable.Columns.Add("IsCaseSensitive", typeof(bool));
			dataTable.Columns.Add("IsFixedLength", typeof(bool));
			dataTable.Columns.Add("IsFixedPrecisionScale", typeof(bool));
			dataTable.Columns.Add("IsLong", typeof(bool));
			dataTable.Columns.Add("IsNullable", typeof(bool));
			dataTable.Columns.Add("IsSearchable", typeof(bool));
			dataTable.Columns.Add("IsSearchableWithLike", typeof(bool));
			dataTable.Columns.Add("IsLiteralSupported", typeof(bool));
			dataTable.Columns.Add("LiteralPrefix", typeof(string));
			dataTable.Columns.Add("LiteralSuffix", typeof(string));
			dataTable.Columns.Add("IsUnsigned", typeof(bool));
			dataTable.Columns.Add("MaximumScale", typeof(short));
			dataTable.Columns.Add("MinimumScale", typeof(short));
			dataTable.Columns.Add("IsConcurrencyType", typeof(bool));
			dataTable.BeginLoadData();
			StringReader stringReader = new StringReader(SR.DataTypes);
			dataTable.ReadXml(stringReader);
			stringReader.Close();
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		private DataTable Schema_IndexColumns(string strCatalog, string strTable, string strIndex, string strColumn)
		{
			DataTable dataTable = new DataTable("IndexColumns");
			List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("CONSTRAINT_CATALOG", typeof(string));
			dataTable.Columns.Add("CONSTRAINT_SCHEMA", typeof(string));
			dataTable.Columns.Add("CONSTRAINT_NAME", typeof(string));
			dataTable.Columns.Add("TABLE_CATALOG", typeof(string));
			dataTable.Columns.Add("TABLE_SCHEMA", typeof(string));
			dataTable.Columns.Add("TABLE_NAME", typeof(string));
			dataTable.Columns.Add("COLUMN_NAME", typeof(string));
			dataTable.Columns.Add("ORDINAL_POSITION", typeof(int));
			dataTable.Columns.Add("INDEX_NAME", typeof(string));
			dataTable.Columns.Add("COLLATION_NAME", typeof(string));
			dataTable.Columns.Add("SORT_MODE", typeof(string));
			dataTable.Columns.Add("CONFLICT_OPTION", typeof(int));
			if (string.IsNullOrEmpty(strCatalog))
			{
				strCatalog = "main";
			}
			string text = ((string.Compare(strCatalog, "temp", StringComparison.OrdinalIgnoreCase) == 0) ? "sqlite_temp_master" : "sqlite_master");
			dataTable.BeginLoadData();
			using (SQLiteCommand sQLiteCommand = new SQLiteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{1}] WHERE [type] LIKE 'table'", new object[2] { strCatalog, text }), this))
			{
				using SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader();
				while (sQLiteDataReader.Read())
				{
					bool flag = false;
					list.Clear();
					if (!string.IsNullOrEmpty(strTable) && string.Compare(sQLiteDataReader.GetString(2), strTable, StringComparison.OrdinalIgnoreCase) != 0)
					{
						continue;
					}
					try
					{
						using SQLiteCommand sQLiteCommand2 = new SQLiteCommand(string.Format(CultureInfo.InvariantCulture, "PRAGMA [{0}].table_info([{1}])", new object[2]
						{
							strCatalog,
							sQLiteDataReader.GetString(2)
						}), this);
						using SQLiteDataReader sQLiteDataReader2 = sQLiteCommand2.ExecuteReader();
						while (sQLiteDataReader2.Read())
						{
							if (sQLiteDataReader2.GetInt32(5) == 1)
							{
								list.Add(new KeyValuePair<int, string>(sQLiteDataReader2.GetInt32(0), sQLiteDataReader2.GetString(1)));
								if (string.Compare(sQLiteDataReader2.GetString(2), "INTEGER", StringComparison.OrdinalIgnoreCase) == 0)
								{
									flag = true;
								}
							}
						}
					}
					catch (SQLiteException)
					{
					}
					if (list.Count == 1 && flag)
					{
						DataRow dataRow = dataTable.NewRow();
						dataRow["CONSTRAINT_CATALOG"] = strCatalog;
						dataRow["CONSTRAINT_NAME"] = string.Format(CultureInfo.InvariantCulture, "{1}_PK_{0}", new object[2]
						{
							sQLiteDataReader.GetString(2),
							text
						});
						dataRow["TABLE_CATALOG"] = strCatalog;
						dataRow["TABLE_NAME"] = sQLiteDataReader.GetString(2);
						dataRow["COLUMN_NAME"] = list[0].Value;
						dataRow["INDEX_NAME"] = dataRow["CONSTRAINT_NAME"];
						dataRow["ORDINAL_POSITION"] = 0;
						dataRow["COLLATION_NAME"] = "BINARY";
						dataRow["SORT_MODE"] = "ASC";
						dataRow["CONFLICT_OPTION"] = 2;
						if (string.IsNullOrEmpty(strIndex) || string.Compare(strIndex, (string)dataRow["INDEX_NAME"], StringComparison.OrdinalIgnoreCase) == 0)
						{
							dataTable.Rows.Add(dataRow);
						}
					}
					using SQLiteCommand sQLiteCommand3 = new SQLiteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{2}] WHERE [type] LIKE 'index' AND [tbl_name] LIKE '{1}'", new object[3]
					{
						strCatalog,
						sQLiteDataReader.GetString(2).Replace("'", "''"),
						text
					}), this);
					using SQLiteDataReader sQLiteDataReader3 = sQLiteCommand3.ExecuteReader();
					while (sQLiteDataReader3.Read())
					{
						int num = 0;
						if (!string.IsNullOrEmpty(strIndex) && string.Compare(strIndex, sQLiteDataReader3.GetString(1), StringComparison.OrdinalIgnoreCase) != 0)
						{
							continue;
						}
						try
						{
							using SQLiteCommand sQLiteCommand4 = new SQLiteCommand(string.Format(CultureInfo.InvariantCulture, "PRAGMA [{0}].index_info([{1}])", new object[2]
							{
								strCatalog,
								sQLiteDataReader3.GetString(1)
							}), this);
							using SQLiteDataReader sQLiteDataReader4 = sQLiteCommand4.ExecuteReader();
							while (sQLiteDataReader4.Read())
							{
								DataRow dataRow = dataTable.NewRow();
								dataRow["CONSTRAINT_CATALOG"] = strCatalog;
								dataRow["CONSTRAINT_NAME"] = sQLiteDataReader3.GetString(1);
								dataRow["TABLE_CATALOG"] = strCatalog;
								dataRow["TABLE_NAME"] = sQLiteDataReader3.GetString(2);
								dataRow["COLUMN_NAME"] = sQLiteDataReader4.GetString(2);
								dataRow["INDEX_NAME"] = sQLiteDataReader3.GetString(1);
								dataRow["ORDINAL_POSITION"] = num;
								string collationSequence = null;
								int sortMode = 0;
								int onError = 0;
								_sql.GetIndexColumnExtendedInfo(strCatalog, sQLiteDataReader3.GetString(1), sQLiteDataReader4.GetString(2), ref sortMode, ref onError, ref collationSequence);
								if (!string.IsNullOrEmpty(collationSequence))
								{
									dataRow["COLLATION_NAME"] = collationSequence;
								}
								dataRow["SORT_MODE"] = ((sortMode == 0) ? "ASC" : "DESC");
								dataRow["CONFLICT_OPTION"] = onError;
								num++;
								if (string.IsNullOrEmpty(strColumn) || string.Compare(strColumn, dataRow["COLUMN_NAME"].ToString(), StringComparison.OrdinalIgnoreCase) == 0)
								{
									dataTable.Rows.Add(dataRow);
								}
							}
						}
						catch (SQLiteException)
						{
						}
					}
				}
			}
			dataTable.EndLoadData();
			dataTable.AcceptChanges();
			return dataTable;
		}

		private DataTable Schema_ViewColumns(string strCatalog, string strView, string strColumn)
		{
			DataTable dataTable = new DataTable("ViewColumns");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("VIEW_CATALOG", typeof(string));
			dataTable.Columns.Add("VIEW_SCHEMA", typeof(string));
			dataTable.Columns.Add("VIEW_NAME", typeof(string));
			dataTable.Columns.Add("VIEW_COLUMN_NAME", typeof(string));
			dataTable.Columns.Add("TABLE_CATALOG", typeof(string));
			dataTable.Columns.Add("TABLE_SCHEMA", typeof(string));
			dataTable.Columns.Add("TABLE_NAME", typeof(string));
			dataTable.Columns.Add("COLUMN_NAME", typeof(string));
			dataTable.Columns.Add("ORDINAL_POSITION", typeof(int));
			dataTable.Columns.Add("COLUMN_HASDEFAULT", typeof(bool));
			dataTable.Columns.Add("COLUMN_DEFAULT", typeof(string));
			dataTable.Columns.Add("COLUMN_FLAGS", typeof(long));
			dataTable.Columns.Add("IS_NULLABLE", typeof(bool));
			dataTable.Columns.Add("DATA_TYPE", typeof(string));
			dataTable.Columns.Add("CHARACTER_MAXIMUM_LENGTH", typeof(int));
			dataTable.Columns.Add("NUMERIC_PRECISION", typeof(int));
			dataTable.Columns.Add("NUMERIC_SCALE", typeof(int));
			dataTable.Columns.Add("DATETIME_PRECISION", typeof(long));
			dataTable.Columns.Add("CHARACTER_SET_CATALOG", typeof(string));
			dataTable.Columns.Add("CHARACTER_SET_SCHEMA", typeof(string));
			dataTable.Columns.Add("CHARACTER_SET_NAME", typeof(string));
			dataTable.Columns.Add("COLLATION_CATALOG", typeof(string));
			dataTable.Columns.Add("COLLATION_SCHEMA", typeof(string));
			dataTable.Columns.Add("COLLATION_NAME", typeof(string));
			dataTable.Columns.Add("PRIMARY_KEY", typeof(bool));
			dataTable.Columns.Add("EDM_TYPE", typeof(string));
			dataTable.Columns.Add("AUTOINCREMENT", typeof(bool));
			dataTable.Columns.Add("UNIQUE", typeof(bool));
			if (string.IsNullOrEmpty(strCatalog))
			{
				strCatalog = "main";
			}
			string text = ((string.Compare(strCatalog, "temp", StringComparison.OrdinalIgnoreCase) == 0) ? "sqlite_temp_master" : "sqlite_master");
			dataTable.BeginLoadData();
			using (SQLiteCommand sQLiteCommand = new SQLiteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{1}] WHERE [type] LIKE 'view'", new object[2] { strCatalog, text }), this))
			{
				using SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader();
				while (sQLiteDataReader.Read())
				{
					if (!string.IsNullOrEmpty(strView) && string.Compare(strView, sQLiteDataReader.GetString(2), StringComparison.OrdinalIgnoreCase) != 0)
					{
						continue;
					}
					using SQLiteCommand sQLiteCommand2 = new SQLiteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{1}]", new object[2]
					{
						strCatalog,
						sQLiteDataReader.GetString(2)
					}), this);
					string text2 = sQLiteDataReader.GetString(4).Replace('\r', ' ').Replace('\n', ' ')
						.Replace('\t', ' ');
					int num = CultureInfo.InvariantCulture.CompareInfo.IndexOf(text2, " AS ", CompareOptions.IgnoreCase);
					if (num < 0)
					{
						continue;
					}
					text2 = text2.Substring(num + 4);
					using SQLiteCommand sQLiteCommand3 = new SQLiteCommand(text2, this);
					using SQLiteDataReader sQLiteDataReader2 = sQLiteCommand2.ExecuteReader(CommandBehavior.SchemaOnly);
					using SQLiteDataReader sQLiteDataReader3 = sQLiteCommand3.ExecuteReader(CommandBehavior.SchemaOnly);
					using DataTable dataTable2 = sQLiteDataReader2.GetSchemaTable(wantUniqueInfo: false, wantDefaultValue: false);
					using DataTable dataTable3 = sQLiteDataReader3.GetSchemaTable(wantUniqueInfo: false, wantDefaultValue: false);
					for (num = 0; num < dataTable3.Rows.Count; num++)
					{
						DataRow dataRow = dataTable2.Rows[num];
						DataRow dataRow2 = dataTable3.Rows[num];
						if (string.Compare(dataRow[SchemaTableColumn.ColumnName].ToString(), strColumn, StringComparison.OrdinalIgnoreCase) == 0 || strColumn == null)
						{
							DataRow dataRow3 = dataTable.NewRow();
							dataRow3["VIEW_CATALOG"] = strCatalog;
							dataRow3["VIEW_NAME"] = sQLiteDataReader.GetString(2);
							dataRow3["TABLE_CATALOG"] = strCatalog;
							dataRow3["TABLE_SCHEMA"] = dataRow2[SchemaTableColumn.BaseSchemaName];
							dataRow3["TABLE_NAME"] = dataRow2[SchemaTableColumn.BaseTableName];
							dataRow3["COLUMN_NAME"] = dataRow2[SchemaTableColumn.BaseColumnName];
							dataRow3["VIEW_COLUMN_NAME"] = dataRow[SchemaTableColumn.ColumnName];
							dataRow3["COLUMN_HASDEFAULT"] = dataRow[SchemaTableOptionalColumn.DefaultValue] != DBNull.Value;
							dataRow3["COLUMN_DEFAULT"] = dataRow[SchemaTableOptionalColumn.DefaultValue];
							dataRow3["ORDINAL_POSITION"] = dataRow[SchemaTableColumn.ColumnOrdinal];
							dataRow3["IS_NULLABLE"] = dataRow[SchemaTableColumn.AllowDBNull];
							dataRow3["DATA_TYPE"] = dataRow["DataTypeName"];
							dataRow3["EDM_TYPE"] = SQLiteConvert.DbTypeToTypeName(this, (DbType)dataRow[SchemaTableColumn.ProviderType], _flags).ToString().ToLower(CultureInfo.InvariantCulture);
							dataRow3["CHARACTER_MAXIMUM_LENGTH"] = dataRow[SchemaTableColumn.ColumnSize];
							dataRow3["TABLE_SCHEMA"] = dataRow[SchemaTableColumn.BaseSchemaName];
							dataRow3["PRIMARY_KEY"] = dataRow[SchemaTableColumn.IsKey];
							dataRow3["AUTOINCREMENT"] = dataRow[SchemaTableOptionalColumn.IsAutoIncrement];
							dataRow3["COLLATION_NAME"] = dataRow["CollationType"];
							dataRow3["UNIQUE"] = dataRow[SchemaTableColumn.IsUnique];
							dataTable.Rows.Add(dataRow3);
						}
					}
				}
			}
			dataTable.EndLoadData();
			dataTable.AcceptChanges();
			return dataTable;
		}

		private DataTable Schema_ForeignKeys(string strCatalog, string strTable, string strKeyName)
		{
			DataTable dataTable = new DataTable("ForeignKeys");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add("CONSTRAINT_CATALOG", typeof(string));
			dataTable.Columns.Add("CONSTRAINT_SCHEMA", typeof(string));
			dataTable.Columns.Add("CONSTRAINT_NAME", typeof(string));
			dataTable.Columns.Add("TABLE_CATALOG", typeof(string));
			dataTable.Columns.Add("TABLE_SCHEMA", typeof(string));
			dataTable.Columns.Add("TABLE_NAME", typeof(string));
			dataTable.Columns.Add("CONSTRAINT_TYPE", typeof(string));
			dataTable.Columns.Add("IS_DEFERRABLE", typeof(bool));
			dataTable.Columns.Add("INITIALLY_DEFERRED", typeof(bool));
			dataTable.Columns.Add("FKEY_ID", typeof(int));
			dataTable.Columns.Add("FKEY_FROM_COLUMN", typeof(string));
			dataTable.Columns.Add("FKEY_FROM_ORDINAL_POSITION", typeof(int));
			dataTable.Columns.Add("FKEY_TO_CATALOG", typeof(string));
			dataTable.Columns.Add("FKEY_TO_SCHEMA", typeof(string));
			dataTable.Columns.Add("FKEY_TO_TABLE", typeof(string));
			dataTable.Columns.Add("FKEY_TO_COLUMN", typeof(string));
			dataTable.Columns.Add("FKEY_ON_UPDATE", typeof(string));
			dataTable.Columns.Add("FKEY_ON_DELETE", typeof(string));
			dataTable.Columns.Add("FKEY_MATCH", typeof(string));
			if (string.IsNullOrEmpty(strCatalog))
			{
				strCatalog = "main";
			}
			string text = ((string.Compare(strCatalog, "temp", StringComparison.OrdinalIgnoreCase) == 0) ? "sqlite_temp_master" : "sqlite_master");
			dataTable.BeginLoadData();
			using (SQLiteCommand sQLiteCommand = new SQLiteCommand(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}].[{1}] WHERE [type] LIKE 'table'", new object[2] { strCatalog, text }), this))
			{
				using SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader();
				while (sQLiteDataReader.Read())
				{
					if (!string.IsNullOrEmpty(strTable) && string.Compare(strTable, sQLiteDataReader.GetString(2), StringComparison.OrdinalIgnoreCase) != 0)
					{
						continue;
					}
					try
					{
						using SQLiteCommandBuilder sQLiteCommandBuilder = new SQLiteCommandBuilder();
						using SQLiteCommand sQLiteCommand2 = new SQLiteCommand(string.Format(CultureInfo.InvariantCulture, "PRAGMA [{0}].foreign_key_list([{1}])", new object[2]
						{
							strCatalog,
							sQLiteDataReader.GetString(2)
						}), this);
						using SQLiteDataReader sQLiteDataReader2 = sQLiteCommand2.ExecuteReader();
						while (sQLiteDataReader2.Read())
						{
							DataRow dataRow = dataTable.NewRow();
							dataRow["CONSTRAINT_CATALOG"] = strCatalog;
							dataRow["CONSTRAINT_NAME"] = string.Format(CultureInfo.InvariantCulture, "FK_{0}_{1}_{2}", new object[3]
							{
								sQLiteDataReader[2],
								sQLiteDataReader2.GetInt32(0),
								sQLiteDataReader2.GetInt32(1)
							});
							dataRow["TABLE_CATALOG"] = strCatalog;
							dataRow["TABLE_NAME"] = sQLiteCommandBuilder.UnquoteIdentifier(sQLiteDataReader.GetString(2));
							dataRow["CONSTRAINT_TYPE"] = "FOREIGN KEY";
							dataRow["IS_DEFERRABLE"] = false;
							dataRow["INITIALLY_DEFERRED"] = false;
							dataRow["FKEY_ID"] = sQLiteDataReader2[0];
							dataRow["FKEY_FROM_COLUMN"] = sQLiteCommandBuilder.UnquoteIdentifier(sQLiteDataReader2[3].ToString());
							dataRow["FKEY_TO_CATALOG"] = strCatalog;
							dataRow["FKEY_TO_TABLE"] = sQLiteCommandBuilder.UnquoteIdentifier(sQLiteDataReader2[2].ToString());
							dataRow["FKEY_TO_COLUMN"] = sQLiteCommandBuilder.UnquoteIdentifier(sQLiteDataReader2[4].ToString());
							dataRow["FKEY_FROM_ORDINAL_POSITION"] = sQLiteDataReader2[1];
							dataRow["FKEY_ON_UPDATE"] = ((sQLiteDataReader2.FieldCount > 5) ? sQLiteDataReader2[5] : string.Empty);
							dataRow["FKEY_ON_DELETE"] = ((sQLiteDataReader2.FieldCount > 6) ? sQLiteDataReader2[6] : string.Empty);
							dataRow["FKEY_MATCH"] = ((sQLiteDataReader2.FieldCount > 7) ? sQLiteDataReader2[7] : string.Empty);
							if (string.IsNullOrEmpty(strKeyName) || string.Compare(strKeyName, dataRow["CONSTRAINT_NAME"].ToString(), StringComparison.OrdinalIgnoreCase) == 0)
							{
								dataTable.Rows.Add(dataRow);
							}
						}
					}
					catch (SQLiteException)
					{
					}
				}
			}
			dataTable.EndLoadData();
			dataTable.AcceptChanges();
			return dataTable;
		}

		private SQLiteAuthorizerReturnCode AuthorizerCallback(IntPtr pUserData, SQLiteAuthorizerActionCode actionCode, IntPtr pArgument1, IntPtr pArgument2, IntPtr pDatabase, IntPtr pAuthContext)
		{
			AuthorizerEventArgs e = new AuthorizerEventArgs(pUserData, actionCode, SQLiteConvert.UTF8ToString(pArgument1, -1), SQLiteConvert.UTF8ToString(pArgument2, -1), SQLiteConvert.UTF8ToString(pDatabase, -1), SQLiteConvert.UTF8ToString(pAuthContext, -1), SQLiteAuthorizerReturnCode.Ok);
			if (this._authorizerHandler != null)
			{
				this._authorizerHandler(this, e);
			}
			return e.ReturnCode;
		}

		private void UpdateCallback(IntPtr puser, int type, IntPtr database, IntPtr table, long rowid)
		{
			this._updateHandler(this, new UpdateEventArgs(SQLiteConvert.UTF8ToString(database, -1), SQLiteConvert.UTF8ToString(table, -1), (UpdateEventType)type, rowid));
		}

		private void TraceCallback(IntPtr puser, IntPtr statement)
		{
			this._traceHandler(this, new TraceEventArgs(SQLiteConvert.UTF8ToString(statement, -1)));
		}

		private int CommitCallback(IntPtr parg)
		{
			CommitEventArgs e = new CommitEventArgs();
			this._commitHandler(this, e);
			if (!e.AbortTransaction)
			{
				return 0;
			}
			return 1;
		}

		private void RollbackCallback(IntPtr parg)
		{
			this._rollbackHandler(this, EventArgs.Empty);
		}
	}
	public enum SynchronizationModes
	{
		Normal,
		Full,
		Off
	}
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate SQLiteAuthorizerReturnCode SQLiteAuthorizerCallback(IntPtr pUserData, SQLiteAuthorizerActionCode actionCode, IntPtr pArgument1, IntPtr pArgument2, IntPtr pDatabase, IntPtr pAuthContext);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void SQLiteUpdateCallback(IntPtr puser, int type, IntPtr database, IntPtr table, long rowid);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate int SQLiteCommitCallback(IntPtr puser);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void SQLiteTraceCallback(IntPtr puser, IntPtr statement);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void SQLiteRollbackCallback(IntPtr puser);
	public delegate void SQLiteAuthorizerEventHandler(object sender, AuthorizerEventArgs e);
	public delegate void SQLiteCommitHandler(object sender, CommitEventArgs e);
	public delegate void SQLiteUpdateEventHandler(object sender, UpdateEventArgs e);
	public delegate void SQLiteTraceEventHandler(object sender, TraceEventArgs e);
	public delegate bool SQLiteBackupCallback(SQLiteConnection source, string sourceName, SQLiteConnection destination, string destinationName, int pages, int remainingPages, int totalPages, bool retry);
	public class AuthorizerEventArgs : EventArgs
	{
		public readonly IntPtr UserData;

		public readonly SQLiteAuthorizerActionCode ActionCode;

		public readonly string Argument1;

		public readonly string Argument2;

		public readonly string Database;

		public readonly string Context;

		public SQLiteAuthorizerReturnCode ReturnCode;

		private AuthorizerEventArgs()
		{
			UserData = IntPtr.Zero;
			ActionCode = SQLiteAuthorizerActionCode.None;
			Argument1 = null;
			Argument2 = null;
			Database = null;
			Context = null;
			ReturnCode = SQLiteAuthorizerReturnCode.Ok;
		}

		internal AuthorizerEventArgs(IntPtr pUserData, SQLiteAuthorizerActionCode actionCode, string argument1, string argument2, string database, string context, SQLiteAuthorizerReturnCode returnCode)
			: this()
		{
			UserData = pUserData;
			ActionCode = actionCode;
			Argument1 = argument1;
			Argument2 = argument2;
			Database = database;
			Context = context;
			ReturnCode = returnCode;
		}
	}
	public enum UpdateEventType
	{
		Delete = 9,
		Insert = 18,
		Update = 23
	}
	public class UpdateEventArgs : EventArgs
	{
		public readonly string Database;

		public readonly string Table;

		public readonly UpdateEventType Event;

		public readonly long RowId;

		internal UpdateEventArgs(string database, string table, UpdateEventType eventType, long rowid)
		{
			Database = database;
			Table = table;
			Event = eventType;
			RowId = rowid;
		}
	}
	public class CommitEventArgs : EventArgs
	{
		public bool AbortTransaction;

		internal CommitEventArgs()
		{
		}
	}
	public class TraceEventArgs : EventArgs
	{
		public readonly string Statement;

		internal TraceEventArgs(string statement)
		{
			Statement = statement;
		}
	}
	public interface ISQLiteConnectionPool
	{
		void GetCounts(string fileName, ref Dictionary<string, int> counts, ref int openCount, ref int closeCount, ref int totalCount);

		void ClearPool(string fileName);

		void ClearAllPools();

		void Add(string fileName, object handle, int version);

		object Remove(string fileName, int maxPoolSize, out int version);
	}
	internal static class SQLiteConnectionPool
	{
		private sealed class PoolQueue
		{
			internal readonly Queue<WeakReference> Queue = new Queue<WeakReference>();

			internal int PoolVersion;

			internal int MaxPoolSize;

			internal PoolQueue(int version, int maxSize)
			{
				PoolVersion = version;
				MaxPoolSize = maxSize;
			}
		}

		private static readonly object _syncRoot = new object();

		private static ISQLiteConnectionPool _connectionPool = null;

		private static SortedList<string, PoolQueue> _queueList = new SortedList<string, PoolQueue>(StringComparer.OrdinalIgnoreCase);

		private static int _poolVersion = 1;

		private static int _poolOpened = 0;

		private static int _poolClosed = 0;

		internal static void GetCounts(string fileName, ref Dictionary<string, int> counts, ref int openCount, ref int closeCount, ref int totalCount)
		{
			ISQLiteConnectionPool connectionPool = GetConnectionPool();
			if (connectionPool != null)
			{
				connectionPool.GetCounts(fileName, ref counts, ref openCount, ref closeCount, ref totalCount);
				return;
			}
			lock (_syncRoot)
			{
				openCount = _poolOpened;
				closeCount = _poolClosed;
				if (counts == null)
				{
					counts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
				}
				if (fileName != null)
				{
					if (_queueList.TryGetValue(fileName, out var value))
					{
						int num = value.Queue?.Count ?? 0;
						counts.Add(fileName, num);
						totalCount += num;
					}
					return;
				}
				foreach (KeyValuePair<string, PoolQueue> queue in _queueList)
				{
					if (queue.Value != null)
					{
						int num2 = queue.Value.Queue?.Count ?? 0;
						counts.Add(queue.Key, num2);
						totalCount += num2;
					}
				}
			}
		}

		internal static void ClearPool(string fileName)
		{
			ISQLiteConnectionPool connectionPool = GetConnectionPool();
			if (connectionPool != null)
			{
				connectionPool.ClearPool(fileName);
				return;
			}
			lock (_syncRoot)
			{
				if (!_queueList.TryGetValue(fileName, out var value))
				{
					return;
				}
				value.PoolVersion++;
				Queue<WeakReference> queue = value.Queue;
				if (queue == null)
				{
					return;
				}
				while (queue.Count > 0)
				{
					WeakReference weakReference = queue.Dequeue();
					if (weakReference != null)
					{
						SQLiteConnectionHandle sQLiteConnectionHandle = weakReference.Target as SQLiteConnectionHandle;
						sQLiteConnectionHandle?.Dispose();
						GC.KeepAlive(sQLiteConnectionHandle);
					}
				}
			}
		}

		internal static void ClearAllPools()
		{
			ISQLiteConnectionPool connectionPool = GetConnectionPool();
			if (connectionPool != null)
			{
				connectionPool.ClearAllPools();
				return;
			}
			lock (_syncRoot)
			{
				foreach (KeyValuePair<string, PoolQueue> queue2 in _queueList)
				{
					if (queue2.Value == null)
					{
						continue;
					}
					Queue<WeakReference> queue = queue2.Value.Queue;
					while (queue.Count > 0)
					{
						WeakReference weakReference = queue.Dequeue();
						if (weakReference != null)
						{
							SQLiteConnectionHandle sQLiteConnectionHandle = weakReference.Target as SQLiteConnectionHandle;
							sQLiteConnectionHandle?.Dispose();
							GC.KeepAlive(sQLiteConnectionHandle);
						}
					}
					if (_poolVersion <= queue2.Value.PoolVersion)
					{
						_poolVersion = queue2.Value.PoolVersion + 1;
					}
				}
				_queueList.Clear();
			}
		}

		internal static void Add(string fileName, SQLiteConnectionHandle handle, int version)
		{
			ISQLiteConnectionPool connectionPool = GetConnectionPool();
			if (connectionPool != null)
			{
				connectionPool.Add(fileName, handle, version);
				return;
			}
			lock (_syncRoot)
			{
				if (_queueList.TryGetValue(fileName, out var value) && version == value.PoolVersion)
				{
					ResizePool(value, add: true);
					Queue<WeakReference> queue = value.Queue;
					if (queue == null)
					{
						return;
					}
					queue.Enqueue(new WeakReference(handle, trackResurrection: false));
					Interlocked.Increment(ref _poolClosed);
				}
				else
				{
					handle.Close();
				}
				GC.KeepAlive(handle);
			}
		}

		internal static SQLiteConnectionHandle Remove(string fileName, int maxPoolSize, out int version)
		{
			ISQLiteConnectionPool connectionPool = GetConnectionPool();
			if (connectionPool != null)
			{
				return connectionPool.Remove(fileName, maxPoolSize, out version) as SQLiteConnectionHandle;
			}
			int version2;
			Queue<WeakReference> queue;
			lock (_syncRoot)
			{
				version = _poolVersion;
				if (!_queueList.TryGetValue(fileName, out var value))
				{
					value = new PoolQueue(_poolVersion, maxPoolSize);
					_queueList.Add(fileName, value);
					return null;
				}
				version2 = (version = value.PoolVersion);
				value.MaxPoolSize = maxPoolSize;
				ResizePool(value, add: false);
				queue = value.Queue;
				if (queue == null)
				{
					return null;
				}
				_queueList.Remove(fileName);
				queue = new Queue<WeakReference>(queue);
			}
			try
			{
				while (queue.Count > 0)
				{
					WeakReference weakReference = queue.Dequeue();
					if (weakReference == null || !(weakReference.Target is SQLiteConnectionHandle sQLiteConnectionHandle))
					{
						continue;
					}
					GC.SuppressFinalize(sQLiteConnectionHandle);
					try
					{
						GC.WaitForPendingFinalizers();
						if (!sQLiteConnectionHandle.IsInvalid && !sQLiteConnectionHandle.IsClosed)
						{
							Interlocked.Increment(ref _poolOpened);
							return sQLiteConnectionHandle;
						}
					}
					finally
					{
						GC.ReRegisterForFinalize(sQLiteConnectionHandle);
					}
					GC.KeepAlive(sQLiteConnectionHandle);
				}
			}
			finally
			{
				lock (_syncRoot)
				{
					bool flag;
					if (_queueList.TryGetValue(fileName, out var value2))
					{
						flag = false;
					}
					else
					{
						flag = true;
						value2 = new PoolQueue(version2, maxPoolSize);
					}
					Queue<WeakReference> queue2 = value2.Queue;
					while (queue.Count > 0)
					{
						queue2.Enqueue(queue.Dequeue());
					}
					ResizePool(value2, add: false);
					if (flag)
					{
						_queueList.Add(fileName, value2);
					}
				}
			}
			return null;
		}

		internal static ISQLiteConnectionPool GetConnectionPool()
		{
			lock (_syncRoot)
			{
				return _connectionPool;
			}
		}

		internal static void SetConnectionPool(ISQLiteConnectionPool connectionPool)
		{
			lock (_syncRoot)
			{
				_connectionPool = connectionPool;
			}
		}

		private static void ResizePool(PoolQueue queue, bool add)
		{
			int num = queue.MaxPoolSize;
			if (add && num > 0)
			{
				num--;
			}
			Queue<WeakReference> queue2 = queue.Queue;
			if (queue2 == null)
			{
				return;
			}
			while (queue2.Count > num)
			{
				WeakReference weakReference = queue2.Dequeue();
				if (weakReference != null)
				{
					SQLiteConnectionHandle sQLiteConnectionHandle = weakReference.Target as SQLiteConnectionHandle;
					sQLiteConnectionHandle?.Dispose();
					GC.KeepAlive(sQLiteConnectionHandle);
				}
			}
		}
	}
	[DefaultMember("Item")]
	[DefaultProperty("DataSource")]
	public sealed class SQLiteConnectionStringBuilder : DbConnectionStringBuilder
	{
		private Hashtable _properties;

		[Browsable(true)]
		[DefaultValue(3)]
		public int Version
		{
			get
			{
				TryGetValue("version", out var value);
				return Convert.ToInt32(value, CultureInfo.CurrentCulture);
			}
			set
			{
				if (value != 3)
				{
					throw new NotSupportedException();
				}
				this["version"] = value;
			}
		}

		[DefaultValue(SynchronizationModes.Normal)]
		[Browsable(true)]
		[DisplayName("Synchronous")]
		public SynchronizationModes SyncMode
		{
			get
			{
				TryGetValue("synchronous", out var value);
				if (value is string)
				{
					return (SynchronizationModes)TypeDescriptor.GetConverter(typeof(SynchronizationModes)).ConvertFrom(value);
				}
				return (SynchronizationModes)value;
			}
			set
			{
				this["synchronous"] = value;
			}
		}

		[DisplayName("Use UTF-16 Encoding")]
		[Browsable(true)]
		[DefaultValue(false)]
		public bool UseUTF16Encoding
		{
			get
			{
				TryGetValue("useutf16encoding", out var value);
				return SQLiteConvert.ToBoolean(value);
			}
			set
			{
				this["useutf16encoding"] = value;
			}
		}

		[DefaultValue(false)]
		[Browsable(true)]
		public bool Pooling
		{
			get
			{
				TryGetValue("pooling", out var value);
				return SQLiteConvert.ToBoolean(value);
			}
			set
			{
				this["pooling"] = value;
			}
		}

		[Browsable(true)]
		[DefaultValue(true)]
		[DisplayName("Binary GUID")]
		public bool BinaryGUID
		{
			get
			{
				TryGetValue("binaryguid", out var value);
				return SQLiteConvert.ToBoolean(value);
			}
			set
			{
				this["binaryguid"] = value;
			}
		}

		[Browsable(true)]
		[DefaultValue("")]
		[DisplayName("Data Source")]
		public string DataSource
		{
			get
			{
				TryGetValue("data source", out var value);
				return value.ToString();
			}
			set
			{
				this["data source"] = value;
			}
		}

		[Browsable(true)]
		[DefaultValue(null)]
		[DisplayName("URI")]
		public string Uri
		{
			get
			{
				TryGetValue("uri", out var value);
				return value.ToString();
			}
			set
			{
				this["uri"] = value;
			}
		}

		[Browsable(true)]
		[DefaultValue(null)]
		[DisplayName("Full URI")]
		public string FullUri
		{
			get
			{
				TryGetValue("fulluri", out var value);
				return value.ToString();
			}
			set
			{
				this["fulluri"] = value;
			}
		}

		[DefaultValue(30)]
		[Browsable(true)]
		[DisplayName("Default Timeout")]
		public int DefaultTimeout
		{
			get
			{
				TryGetValue("default timeout", out var value);
				return Convert.ToInt32(value, CultureInfo.CurrentCulture);
			}
			set
			{
				this["default timeout"] = value;
			}
		}

		[Browsable(true)]
		[DefaultValue(3)]
		[DisplayName("Prepare Retries")]
		public int PrepareRetries
		{
			get
			{
				TryGetValue("prepareretries", out var value);
				return Convert.ToInt32(value, CultureInfo.CurrentCulture);
			}
			set
			{
				this["prepareretries"] = value;
			}
		}

		[DefaultValue(true)]
		[Browsable(true)]
		public bool Enlist
		{
			get
			{
				TryGetValue("enlist", out var value);
				return SQLiteConvert.ToBoolean(value);
			}
			set
			{
				this["enlist"] = value;
			}
		}

		[DefaultValue(false)]
		[DisplayName("Fail If Missing")]
		[Browsable(true)]
		public bool FailIfMissing
		{
			get
			{
				TryGetValue("failifmissing", out var value);
				return SQLiteConvert.ToBoolean(value);
			}
			set
			{
				this["failifmissing"] = value;
			}
		}

		[DefaultValue(false)]
		[DisplayName("Legacy Format")]
		[Browsable(true)]
		public bool LegacyFormat
		{
			get
			{
				TryGetValue("legacy format", out var value);
				return SQLiteConvert.ToBoolean(value);
			}
			set
			{
				this["legacy format"] = value;
			}
		}

		[DisplayName("Read Only")]
		[Browsable(true)]
		[DefaultValue(false)]
		public bool ReadOnly
		{
			get
			{
				TryGetValue("read only", out var value);
				return SQLiteConvert.ToBoolean(value);
			}
			set
			{
				this["read only"] = value;
			}
		}

		[PasswordPropertyText(true)]
		[DefaultValue("")]
		[Browsable(true)]
		public string Password
		{
			get
			{
				TryGetValue("password", out var value);
				return value.ToString();
			}
			set
			{
				this["password"] = value;
			}
		}

		[DisplayName("Hexadecimal Password")]
		[PasswordPropertyText(true)]
		[DefaultValue(null)]
		[Browsable(true)]
		public byte[] HexPassword
		{
			get
			{
				if (TryGetValue("hexpassword", out var value))
				{
					if (value is string)
					{
						return SQLiteConnection.FromHexString((string)value);
					}
					if (value != null)
					{
						return (byte[])value;
					}
				}
				return null;
			}
			set
			{
				this["hexpassword"] = SQLiteConnection.ToHexString(value);
			}
		}

		[DisplayName("Page Size")]
		[Browsable(true)]
		[DefaultValue(1024)]
		public int PageSize
		{
			get
			{
				TryGetValue("page size", out var value);
				return Convert.ToInt32(value, CultureInfo.CurrentCulture);
			}
			set
			{
				this["page size"] = value;
			}
		}

		[DefaultValue(0)]
		[DisplayName("Maximum Page Count")]
		[Browsable(true)]
		public int MaxPageCount
		{
			get
			{
				TryGetValue("max page count", out var value);
				return Convert.ToInt32(value, CultureInfo.CurrentCulture);
			}
			set
			{
				this["max page count"] = value;
			}
		}

		[DefaultValue(2000)]
		[Browsable(true)]
		[DisplayName("Cache Size")]
		public int CacheSize
		{
			get
			{
				TryGetValue("cache size", out var value);
				return Convert.ToInt32(value, CultureInfo.CurrentCulture);
			}
			set
			{
				this["cache size"] = value;
			}
		}

		[Browsable(true)]
		[DefaultValue(SQLiteDateFormats.ISO8601)]
		[DisplayName("DateTime Format")]
		public SQLiteDateFormats DateTimeFormat
		{
			get
			{
				if (TryGetValue("datetimeformat", out var value))
				{
					if (value is SQLiteDateFormats)
					{
						return (SQLiteDateFormats)value;
					}
					if (value != null)
					{
						return (SQLiteDateFormats)TypeDescriptor.GetConverter(typeof(SQLiteDateFormats)).ConvertFrom(value);
					}
				}
				return SQLiteDateFormats.ISO8601;
			}
			set
			{
				this["datetimeformat"] = value;
			}
		}

		[Browsable(true)]
		[DisplayName("DateTime Kind")]
		[DefaultValue(DateTimeKind.Unspecified)]
		public DateTimeKind DateTimeKind
		{
			get
			{
				if (TryGetValue("datetimekind", out var value))
				{
					if (value is DateTimeKind)
					{
						return (DateTimeKind)value;
					}
					if (value != null)
					{
						return (DateTimeKind)TypeDescriptor.GetConverter(typeof(DateTimeKind)).ConvertFrom(value);
					}
				}
				return DateTimeKind.Unspecified;
			}
			set
			{
				this["datetimekind"] = value;
			}
		}

		[DisplayName("DateTime Format String")]
		[Browsable(true)]
		[DefaultValue(null)]
		public string DateTimeFormatString
		{
			get
			{
				if (TryGetValue("datetimeformatstring", out var value))
				{
					if (value is string)
					{
						return (string)value;
					}
					if (value != null)
					{
						return value.ToString();
					}
				}
				return null;
			}
			set
			{
				this["datetimeformatstring"] = value;
			}
		}

		[Browsable(true)]
		[DisplayName("Base Schema Name")]
		[DefaultValue("sqlite_default_schema")]
		public string BaseSchemaName
		{
			get
			{
				if (TryGetValue("baseschemaname", out var value))
				{
					if (value is string)
					{
						return (string)value;
					}
					if (value != null)
					{
						return value.ToString();
					}
				}
				return null;
			}
			set
			{
				this["baseschemaname"] = value;
			}
		}

		[DefaultValue(SQLiteJournalModeEnum.Default)]
		[DisplayName("Journal Mode")]
		[Browsable(true)]
		public SQLiteJournalModeEnum JournalMode
		{
			get
			{
				TryGetValue("journal mode", out var value);
				if (value is string)
				{
					return (SQLiteJournalModeEnum)TypeDescriptor.GetConverter(typeof(SQLiteJournalModeEnum)).ConvertFrom(value);
				}
				return (SQLiteJournalModeEnum)value;
			}
			set
			{
				this["journal mode"] = value;
			}
		}

		[Browsable(true)]
		[DisplayName("Default Isolation Level")]
		[DefaultValue(IsolationLevel.Serializable)]
		public IsolationLevel DefaultIsolationLevel
		{
			get
			{
				TryGetValue("default isolationlevel", out var value);
				if (value is string)
				{
					return (IsolationLevel)TypeDescriptor.GetConverter(typeof(IsolationLevel)).ConvertFrom(value);
				}
				return (IsolationLevel)value;
			}
			set
			{
				this["default isolationlevel"] = value;
			}
		}

		[DisplayName("Default Database Type")]
		[Browsable(true)]
		[DefaultValue((DbType)(-1))]
		public DbType DefaultDbType
		{
			get
			{
				if (TryGetValue("defaultdbtype", out var value))
				{
					if (value is string)
					{
						return (DbType)TypeDescriptor.GetConverter(typeof(DbType)).ConvertFrom(value);
					}
					if (value != null)
					{
						return (DbType)value;
					}
				}
				return (DbType)(-1);
			}
			set
			{
				this["defaultdbtype"] = value;
			}
		}

		[DisplayName("Default Type Name")]
		[Browsable(true)]
		[DefaultValue(null)]
		public string DefaultTypeName
		{
			get
			{
				TryGetValue("defaulttypename", out var value);
				return value.ToString();
			}
			set
			{
				this["defaulttypename"] = value;
			}
		}

		[DisplayName("Foreign Keys")]
		[Browsable(true)]
		[DefaultValue(false)]
		public bool ForeignKeys
		{
			get
			{
				TryGetValue("foreign keys", out var value);
				return SQLiteConvert.ToBoolean(value);
			}
			set
			{
				this["foreign keys"] = value;
			}
		}

		[DefaultValue(SQLiteConnectionFlags.Default)]
		[Browsable(true)]
		public SQLiteConnectionFlags Flags
		{
			get
			{
				if (TryGetValue("flags", out var value))
				{
					if (value is SQLiteConnectionFlags)
					{
						return (SQLiteConnectionFlags)value;
					}
					if (value != null)
					{
						return (SQLiteConnectionFlags)TypeDescriptor.GetConverter(typeof(SQLiteConnectionFlags)).ConvertFrom(value);
					}
				}
				return SQLiteConnectionFlags.Default;
			}
			set
			{
				this["flags"] = value;
			}
		}

		[DefaultValue(true)]
		[DisplayName("Set Defaults")]
		[Browsable(true)]
		public bool SetDefaults
		{
			get
			{
				TryGetValue("setdefaults", out var value);
				return SQLiteConvert.ToBoolean(value);
			}
			set
			{
				this["setdefaults"] = value;
			}
		}

		[Browsable(true)]
		[DefaultValue(true)]
		[DisplayName("To Full Path")]
		public bool ToFullPath
		{
			get
			{
				TryGetValue("tofullpath", out var value);
				return SQLiteConvert.ToBoolean(value);
			}
			set
			{
				this["tofullpath"] = value;
			}
		}

		[Browsable(true)]
		[DisplayName("No Shared Flags")]
		[DefaultValue(false)]
		public bool NoSharedFlags
		{
			get
			{
				TryGetValue("nosharedflags", out var value);
				return SQLiteConvert.ToBoolean(value);
			}
			set
			{
				this["nosharedflags"] = value;
			}
		}

		public SQLiteConnectionStringBuilder()
		{
			Initialize(null);
		}

		public SQLiteConnectionStringBuilder(string connectionString)
		{
			Initialize(connectionString);
		}

		private void Initialize(string cnnString)
		{
			_properties = new Hashtable(StringComparer.OrdinalIgnoreCase);
			try
			{
				base.GetProperties(_properties);
			}
			catch (NotImplementedException)
			{
				FallbackGetProperties(_properties);
			}
			if (!string.IsNullOrEmpty(cnnString))
			{
				base.ConnectionString = cnnString;
			}
		}

		public override bool TryGetValue(string keyword, out object value)
		{
			bool flag = base.TryGetValue(keyword, out value);
			if (!_properties.ContainsKey(keyword))
			{
				return flag;
			}
			if (!(_properties[keyword] is PropertyDescriptor propertyDescriptor))
			{
				return flag;
			}
			if (flag)
			{
				if ((object)propertyDescriptor.PropertyType == typeof(bool))
				{
					value = SQLiteConvert.ToBoolean(value);
				}
				else if ((object)propertyDescriptor.PropertyType != typeof(byte[]))
				{
					value = TypeDescriptor.GetConverter(propertyDescriptor.PropertyType).ConvertFrom(value);
				}
			}
			else if (propertyDescriptor.Attributes[typeof(DefaultValueAttribute)] is DefaultValueAttribute defaultValueAttribute)
			{
				value = defaultValueAttribute.Value;
				flag = true;
			}
			return flag;
		}

		private void FallbackGetProperties(Hashtable propertyList)
		{
			foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(this, noCustomTypeDesc: true))
			{
				if (property.Name != "ConnectionString" && !propertyList.ContainsKey(property.DisplayName))
				{
					propertyList.Add(property.DisplayName, property);
				}
			}
		}
	}
	public enum TypeAffinity
	{
		Uninitialized = 0,
		Int64 = 1,
		Double = 2,
		Text = 3,
		Blob = 4,
		Null = 5,
		DateTime = 10,
		None = 11
	}
	public enum SQLiteConnectionEventType
	{
		Invalid = -1,
		Unknown,
		Opening,
		ConnectionString,
		Opened,
		ChangeDatabase,
		NewTransaction,
		EnlistTransaction,
		NewCommand,
		NewDataReader,
		NewCriticalHandle,
		Closing,
		Closed,
		DisposingCommand,
		DisposingDataReader,
		ClosingDataReader
	}
	public enum SQLiteDateFormats
	{
		Ticks = 0,
		ISO8601 = 1,
		JulianDay = 2,
		UnixEpoch = 3,
		InvariantCulture = 4,
		CurrentCulture = 5,
		Default = 1
	}
	public enum SQLiteJournalModeEnum
	{
		Default = -1,
		Delete,
		Persist,
		Off,
		Truncate,
		Memory,
		Wal
	}
	internal enum SQLiteSynchronousEnum
	{
		Default = -1,
		Off,
		Normal,
		Full
	}
	public enum SQLiteExecuteType
	{
		None = 0,
		NonQuery = 1,
		Scalar = 2,
		Reader = 3,
		Default = 1
	}
	public enum SQLiteAuthorizerActionCode
	{
		None = -1,
		Copy,
		CreateIndex,
		CreateTable,
		CreateTempIndex,
		CreateTempTable,
		CreateTempTrigger,
		CreateTempView,
		CreateTrigger,
		CreateView,
		Delete,
		DropIndex,
		DropTable,
		DropTempIndex,
		DropTempTable,
		DropTempTrigger,
		DropTempView,
		DropTrigger,
		DropView,
		Insert,
		Pragma,
		Read,
		Select,
		Transaction,
		Update,
		Attach,
		Detach,
		AlterTable,
		Reindex,
		Analyze,
		CreateVtable,
		DropVtable,
		Function,
		Savepoint,
		Recursive
	}
	public enum SQLiteAuthorizerReturnCode
	{
		Ok,
		Deny,
		Ignore
	}
	internal sealed class SQLiteType
	{
		internal DbType Type;

		internal TypeAffinity Affinity;

		public SQLiteType()
		{
		}

		public SQLiteType(TypeAffinity affinity, DbType type)
			: this()
		{
			Affinity = affinity;
			Type = type;
		}
	}
	internal sealed class SQLiteDbTypeMap : Dictionary<string, SQLiteDbTypeMapping>
	{
		private Dictionary<DbType, SQLiteDbTypeMapping> reverse;

		public SQLiteDbTypeMap()
			: base((IEqualityComparer<string>)new TypeNameStringComparer())
		{
			reverse = new Dictionary<DbType, SQLiteDbTypeMapping>();
		}

		public SQLiteDbTypeMap(IEnumerable<SQLiteDbTypeMapping> collection)
			: this()
		{
			Add(collection);
		}

		public new int Clear()
		{
			int num = 0;
			if (reverse != null)
			{
				num += reverse.Count;
				reverse.Clear();
			}
			num += base.Count;
			base.Clear();
			return num;
		}

		public void Add(IEnumerable<SQLiteDbTypeMapping> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			foreach (SQLiteDbTypeMapping item in collection)
			{
				Add(item);
			}
		}

		public void Add(SQLiteDbTypeMapping item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (item.typeName == null)
			{
				throw new ArgumentException("item type name cannot be null");
			}
			Add(item.typeName, item);
			if (item.primary)
			{
				reverse.Add(item.dataType, item);
			}
		}

		public bool ContainsKey(DbType key)
		{
			if (reverse == null)
			{
				return false;
			}
			return reverse.ContainsKey(key);
		}

		public bool TryGetValue(DbType key, out SQLiteDbTypeMapping value)
		{
			if (reverse == null)
			{
				value = null;
				return false;
			}
			return reverse.TryGetValue(key, out value);
		}

		public bool Remove(DbType key)
		{
			if (reverse == null)
			{
				return false;
			}
			return reverse.Remove(key);
		}
	}
	internal sealed class SQLiteDbTypeMapping
	{
		internal string typeName;

		internal DbType dataType;

		internal bool primary;

		internal SQLiteDbTypeMapping(string newTypeName, DbType newDataType, bool newPrimary)
		{
			typeName = newTypeName;
			dataType = newDataType;
			primary = newPrimary;
		}
	}
	internal sealed class TypeNameStringComparer : IEqualityComparer<string>
	{
		public bool Equals(string left, string right)
		{
			return string.Equals(left, right, StringComparison.OrdinalIgnoreCase);
		}

		public int GetHashCode(string value)
		{
			if (value != null)
			{
				return value.ToLowerInvariant().GetHashCode();
			}
			throw new ArgumentNullException("value");
		}
	}
	[Designer("Microsoft.VSDesigner.Data.VS.SqlDataAdapterDesigner, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("RowUpdated")]
	[ToolboxItem("SQLite.Designer.SQLiteDataAdapterToolboxItem, SQLite.Designer, Version=1.0.97.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139")]
	public sealed class SQLiteDataAdapter : DbDataAdapter
	{
		private bool disposeSelect = true;

		private static object _updatingEventPH = new object();

		private static object _updatedEventPH = new object();

		private bool disposed;

		[DefaultValue(null)]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new SQLiteCommand SelectCommand
		{
			get
			{
				CheckDisposed();
				return (SQLiteCommand)base.SelectCommand;
			}
			set
			{
				CheckDisposed();
				base.SelectCommand = value;
			}
		}

		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(null)]
		public new SQLiteCommand InsertCommand
		{
			get
			{
				CheckDisposed();
				return (SQLiteCommand)base.InsertCommand;
			}
			set
			{
				CheckDisposed();
				base.InsertCommand = value;
			}
		}

		[DefaultValue(null)]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new SQLiteCommand UpdateCommand
		{
			get
			{
				CheckDisposed();
				return (SQLiteCommand)base.UpdateCommand;
			}
			set
			{
				CheckDisposed();
				base.UpdateCommand = value;
			}
		}

		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(null)]
		public new SQLiteCommand DeleteCommand
		{
			get
			{
				CheckDisposed();
				return (SQLiteCommand)base.DeleteCommand;
			}
			set
			{
				CheckDisposed();
				base.DeleteCommand = value;
			}
		}

		public event EventHandler<RowUpdatingEventArgs> RowUpdating
		{
			add
			{
				CheckDisposed();
				EventHandler<RowUpdatingEventArgs> eventHandler = (EventHandler<RowUpdatingEventArgs>)base.Events[_updatingEventPH];
				if (eventHandler != null && value.Target is DbCommandBuilder)
				{
					EventHandler<RowUpdatingEventArgs> eventHandler2 = (EventHandler<RowUpdatingEventArgs>)FindBuilder(eventHandler);
					if (eventHandler2 != null)
					{
						base.Events.RemoveHandler(_updatingEventPH, eventHandler2);
					}
				}
				base.Events.AddHandler(_updatingEventPH, value);
			}
			remove
			{
				CheckDisposed();
				base.Events.RemoveHandler(_updatingEventPH, value);
			}
		}

		public event EventHandler<RowUpdatedEventArgs> RowUpdated
		{
			add
			{
				CheckDisposed();
				base.Events.AddHandler(_updatedEventPH, value);
			}
			remove
			{
				CheckDisposed();
				base.Events.RemoveHandler(_updatedEventPH, value);
			}
		}

		public SQLiteDataAdapter()
		{
		}

		public SQLiteDataAdapter(SQLiteCommand cmd)
		{
			SelectCommand = cmd;
			disposeSelect = false;
		}

		public SQLiteDataAdapter(string commandText, SQLiteConnection connection)
		{
			SelectCommand = new SQLiteCommand(commandText, connection);
		}

		public SQLiteDataAdapter(string commandText, string connectionString)
			: this(commandText, connectionString, parseViaFramework: false)
		{
		}

		public SQLiteDataAdapter(string commandText, string connectionString, bool parseViaFramework)
		{
			SQLiteConnection connection = new SQLiteConnection(connectionString, parseViaFramework);
			SelectCommand = new SQLiteCommand(commandText, connection);
		}

		private void CheckDisposed()
		{
			if (disposed)
			{
				throw new ObjectDisposedException(typeof(SQLiteDataAdapter).Name);
			}
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!disposed && disposing)
				{
					if (disposeSelect && SelectCommand != null)
					{
						SelectCommand.Dispose();
						SelectCommand = null;
					}
					if (InsertCommand != null)
					{
						InsertCommand.Dispose();
						InsertCommand = null;
					}
					if (UpdateCommand != null)
					{
						UpdateCommand.Dispose();
						UpdateCommand = null;
					}
					if (DeleteCommand != null)
					{
						DeleteCommand.Dispose();
						DeleteCommand = null;
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
				disposed = true;
			}
		}

		internal static Delegate FindBuilder(MulticastDelegate mcd)
		{
			if ((object)mcd != null)
			{
				Delegate[] invocationList = mcd.GetInvocationList();
				for (int i = 0; i < invocationList.Length; i++)
				{
					if (invocationList[i].Target is DbCommandBuilder)
					{
						return invocationList[i];
					}
				}
			}
			return null;
		}

		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			if (base.Events[_updatingEventPH] is EventHandler<RowUpdatingEventArgs> eventHandler)
			{
				eventHandler(this, value);
			}
		}

		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			if (base.Events[_updatedEventPH] is EventHandler<RowUpdatedEventArgs> eventHandler)
			{
				eventHandler(this, value);
			}
		}
	}
	public sealed class SQLiteDataReader : DbDataReader
	{
		private sealed class ColumnParent : IEqualityComparer<ColumnParent>
		{
			public string DatabaseName;

			public string TableName;

			public string ColumnName;

			public ColumnParent()
			{
			}

			public ColumnParent(string databaseName, string tableName, string columnName)
				: this()
			{
				DatabaseName = databaseName;
				TableName = tableName;
				ColumnName = columnName;
			}

			public bool Equals(ColumnParent x, ColumnParent y)
			{
				if (x == null && y == null)
				{
					return true;
				}
				if (x == null || y == null)
				{
					return false;
				}
				if (!string.Equals(x.DatabaseName, y.DatabaseName, StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
				if (!string.Equals(x.TableName, y.TableName, StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
				if (!string.Equals(x.ColumnName, y.ColumnName, StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
				return true;
			}

			public int GetHashCode(ColumnParent obj)
			{
				int num = 0;
				if (obj != null && obj.DatabaseName != null)
				{
					num ^= obj.DatabaseName.GetHashCode();
				}
				if (obj != null && obj.TableName != null)
				{
					num ^= obj.TableName.GetHashCode();
				}
				if (obj != null && obj.ColumnName != null)
				{
					num ^= obj.ColumnName.GetHashCode();
				}
				return num;
			}
		}

		private SQLiteCommand _command;

		private SQLiteConnectionFlags _flags;

		private int _activeStatementIndex;

		private SQLiteStatement _activeStatement;

		private int _readingState;

		private int _rowsAffected;

		private int _fieldCount;

		private int _stepCount;

		private Dictionary<string, int> _fieldIndexes;

		private SQLiteType[] _fieldTypeArray;

		private CommandBehavior _commandBehavior;

		internal bool _disposeCommand;

		internal bool _throwOnDisposed;

		private SQLiteKeyReader _keyInfo;

		internal int _version;

		private string _baseSchemaName;

		private bool disposed;

		public override int Depth
		{
			get
			{
				CheckDisposed();
				CheckClosed();
				return 0;
			}
		}

		public override int FieldCount
		{
			get
			{
				CheckDisposed();
				CheckClosed();
				if (_keyInfo == null)
				{
					return _fieldCount;
				}
				return _fieldCount + _keyInfo.Count;
			}
		}

		public int StepCount
		{
			get
			{
				CheckDisposed();
				CheckClosed();
				return _stepCount;
			}
		}

		private int PrivateVisibleFieldCount => _fieldCount;

		public override int VisibleFieldCount
		{
			get
			{
				CheckDisposed();
				CheckClosed();
				return PrivateVisibleFieldCount;
			}
		}

		public override bool HasRows
		{
			get
			{
				CheckDisposed();
				CheckClosed();
				if ((_flags & SQLiteConnectionFlags.StickyHasRows) == SQLiteConnectionFlags.StickyHasRows)
				{
					if (_readingState == 1)
					{
						return _stepCount > 0;
					}
					return true;
				}
				return _readingState != 1;
			}
		}

		public override bool IsClosed
		{
			get
			{
				CheckDisposed();
				return _command == null;
			}
		}

		public override int RecordsAffected
		{
			get
			{
				CheckDisposed();
				return _rowsAffected;
			}
		}

		public override object this[string name]
		{
			get
			{
				CheckDisposed();
				return GetValue(GetOrdinal(name));
			}
		}

		public override object this[int i]
		{
			get
			{
				CheckDisposed();
				return GetValue(i);
			}
		}

		internal SQLiteDataReader(SQLiteCommand cmd, CommandBehavior behave)
		{
			_throwOnDisposed = true;
			_command = cmd;
			_version = _command.Connection._version;
			_baseSchemaName = _command.Connection._baseSchemaName;
			_commandBehavior = behave;
			_activeStatementIndex = -1;
			_rowsAffected = -1;
			RefreshFlags();
			SQLiteConnection.OnChanged(GetConnection(this), new ConnectionEventArgs(SQLiteConnectionEventType.NewDataReader, null, null, _command, this, null, null, new object[1] { behave }));
			if (_command != null)
			{
				NextResult();
			}
		}

		private void CheckDisposed()
		{
			if (disposed && _throwOnDisposed)
			{
				throw new ObjectDisposedException(typeof(SQLiteDataReader).Name);
			}
		}

		protected override void Dispose(bool disposing)
		{
			SQLiteConnection.OnChanged(GetConnection(this), new ConnectionEventArgs(SQLiteConnectionEventType.DisposingDataReader, null, null, _command, this, null, null, new object[9] { disposing, disposed, _commandBehavior, _readingState, _rowsAffected, _stepCount, _fieldCount, _disposeCommand, _throwOnDisposed }));
			try
			{
				if (!disposed)
				{
					_throwOnDisposed = false;
				}
			}
			finally
			{
				base.Dispose(disposing);
				disposed = true;
			}
		}

		internal void Cancel()
		{
			_version = 0;
		}

		public override void Close()
		{
			CheckDisposed();
			SQLiteConnection.OnChanged(GetConnection(this), new ConnectionEventArgs(SQLiteConnectionEventType.ClosingDataReader, null, null, _command, this, null, null, new object[7] { _commandBehavior, _readingState, _rowsAffected, _stepCount, _fieldCount, _disposeCommand, _throwOnDisposed }));
			try
			{
				if (_command != null)
				{
					try
					{
						try
						{
							if (_version != 0)
							{
								try
								{
									while (NextResult())
									{
									}
								}
								catch (SQLiteException)
								{
								}
							}
							_command.ClearDataReader();
						}
						finally
						{
							if ((_commandBehavior & CommandBehavior.CloseConnection) != CommandBehavior.Default && _command.Connection != null)
							{
								_command.Connection.Close();
							}
						}
					}
					finally
					{
						if (_disposeCommand)
						{
							_command.Dispose();
						}
					}
				}
				_command = null;
				_activeStatement = null;
				_fieldIndexes = null;
				_fieldTypeArray = null;
			}
			finally
			{
				if (_keyInfo != null)
				{
					_keyInfo.Dispose();
					_keyInfo = null;
				}
			}
		}

		private void CheckClosed()
		{
			if (_throwOnDisposed)
			{
				if (_command == null)
				{
					throw new InvalidOperationException("DataReader has been closed");
				}
				if (_version == 0)
				{
					throw new SQLiteException("Execution was aborted by the user");
				}
				SQLiteConnection connection = _command.Connection;
				if (connection._version != _version || connection.State != ConnectionState.Open)
				{
					throw new InvalidOperationException("Connection was closed, statement was terminated");
				}
			}
		}

		private void CheckValidRow()
		{
			if (_readingState != 0)
			{
				throw new InvalidOperationException("No current row");
			}
		}

		public override IEnumerator GetEnumerator()
		{
			CheckDisposed();
			return new DbEnumerator((IDataReader)this, (_commandBehavior & CommandBehavior.CloseConnection) == CommandBehavior.CloseConnection);
		}

		public void RefreshFlags()
		{
			CheckDisposed();
			_flags = SQLiteCommand.GetFlags(_command);
		}

		private void VerifyForGet()
		{
			CheckClosed();
			CheckValidRow();
		}

		private TypeAffinity VerifyType(int i, DbType typ)
		{
			TypeAffinity affinity = GetSQLiteType(_flags, i).Affinity;
			switch (affinity)
			{
			case TypeAffinity.Int64:
				switch (typ)
				{
				case DbType.Int64:
					return affinity;
				case DbType.Int32:
					return affinity;
				case DbType.Int16:
					return affinity;
				case DbType.Byte:
					return affinity;
				case DbType.SByte:
					return affinity;
				case DbType.Boolean:
					return affinity;
				case DbType.DateTime:
					return affinity;
				case DbType.Double:
					return affinity;
				case DbType.Single:
					return affinity;
				case DbType.Decimal:
					return affinity;
				}
				break;
			case TypeAffinity.Double:
				switch (typ)
				{
				case DbType.Double:
					return affinity;
				case DbType.Single:
					return affinity;
				case DbType.Decimal:
					return affinity;
				case DbType.DateTime:
					return affinity;
				}
				break;
			case TypeAffinity.Text:
				switch (typ)
				{
				case DbType.String:
					return affinity;
				case DbType.Guid:
					return affinity;
				case DbType.DateTime:
					return affinity;
				case DbType.Decimal:
					return affinity;
				}
				break;
			case TypeAffinity.Blob:
				switch (typ)
				{
				case DbType.Guid:
					return affinity;
				case DbType.Binary:
					return affinity;
				case DbType.String:
					return affinity;
				}
				break;
			}
			throw new InvalidCastException();
		}

		public override bool GetBoolean(int i)
		{
			CheckDisposed();
			VerifyForGet();
			if (i >= PrivateVisibleFieldCount && _keyInfo != null)
			{
				return _keyInfo.GetBoolean(i - PrivateVisibleFieldCount);
			}
			VerifyType(i, DbType.Boolean);
			return Convert.ToBoolean(GetValue(i), CultureInfo.CurrentCulture);
		}

		public override byte GetByte(int i)
		{
			CheckDisposed();
			VerifyForGet();
			if (i >= PrivateVisibleFieldCount && _keyInfo != null)
			{
				return _keyInfo.GetByte(i - PrivateVisibleFieldCount);
			}
			VerifyType(i, DbType.Byte);
			return Convert.ToByte(_activeStatement._sql.GetInt32(_activeStatement, i));
		}

		public override long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			CheckDisposed();
			VerifyForGet();
			if (i >= PrivateVisibleFieldCount && _keyInfo != null)
			{
				return _keyInfo.GetBytes(i - PrivateVisibleFieldCount, fieldOffset, buffer, bufferoffset, length);
			}
			VerifyType(i, DbType.Binary);
			return _activeStatement._sql.GetBytes(_activeStatement, i, (int)fieldOffset, buffer, bufferoffset, length);
		}

		public override char GetChar(int i)
		{
			CheckDisposed();
			VerifyForGet();
			if (i >= PrivateVisibleFieldCount && _keyInfo != null)
			{
				return _keyInfo.GetChar(i - PrivateVisibleFieldCount);
			}
			VerifyType(i, DbType.SByte);
			return Convert.ToChar(_activeStatement._sql.GetInt32(_activeStatement, i));
		}

		public override long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			CheckDisposed();
			VerifyForGet();
			if (i >= PrivateVisibleFieldCount && _keyInfo != null)
			{
				return _keyInfo.GetChars(i - PrivateVisibleFieldCount, fieldoffset, buffer, bufferoffset, length);
			}
			VerifyType(i, DbType.String);
			return _activeStatement._sql.GetChars(_activeStatement, i, (int)fieldoffset, buffer, bufferoffset, length);
		}

		public override string GetDataTypeName(int i)
		{
			CheckDisposed();
			if (i >= PrivateVisibleFieldCount && _keyInfo != null)
			{
				return _keyInfo.GetDataTypeName(i - PrivateVisibleFieldCount);
			}
			TypeAffinity nAffinity = TypeAffinity.Uninitialized;
			return _activeStatement._sql.ColumnType(_activeStatement, i, ref nAffinity);
		}

		public override DateTime GetDateTime(int i)
		{
			CheckDisposed();
			VerifyForGet();
			if (i >= PrivateVisibleFieldCount && _keyInfo != null)
			{
				return _keyInfo.GetDateTime(i - PrivateVisibleFieldCount);
			}
			VerifyType(i, DbType.DateTime);
			return _activeStatement._sql.GetDateTime(_activeStatement, i);
		}

		public override decimal GetDecimal(int i)
		{
			CheckDisposed();
			VerifyForGet();
			if (i >= PrivateVisibleFieldCount && _keyInfo != null)
			{
				return _keyInfo.GetDecimal(i - PrivateVisibleFieldCount);
			}
			VerifyType(i, DbType.Decimal);
			return decimal.Parse(_activeStatement._sql.GetText(_activeStatement, i), NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, CultureInfo.InvariantCulture);
		}

		public override double GetDouble(int i)
		{
			CheckDisposed();
			VerifyForGet();
			if (i >= PrivateVisibleFieldCount && _keyInfo != null)
			{
				return _keyInfo.GetDouble(i - PrivateVisibleFieldCount);
			}
			VerifyType(i, DbType.Double);
			return _activeStatement._sql.GetDouble(_activeStatement, i);
		}

		public override Type GetFieldType(int i)
		{
			CheckDisposed();
			if (i >= PrivateVisibleFieldCount && _keyInfo != null)
			{
				return _keyInfo.GetFieldType(i - PrivateVisibleFieldCount);
			}
			return SQLiteConvert.SQLiteTypeToType(GetSQLiteType(_flags, i));
		}

		public override float GetFloat(int i)
		{
			CheckDisposed();
			VerifyForGet();
			if (i >= PrivateVisibleFieldCount && _keyInfo != null)
			{
				return _keyInfo.GetFloat(i - PrivateVisibleFieldCount);
			}
			VerifyType(i, DbType.Single);
			return Convert.ToSingle(_activeStatement._sql.GetDouble(_activeStatement, i));
		}

		public override Guid GetGuid(int i)
		{
			CheckDisposed();
			VerifyForGet();
			if (i >= PrivateVisibleFieldCount && _keyInfo != null)
			{
				return _keyInfo.GetGuid(i - PrivateVisibleFieldCount);
			}
			TypeAffinity typeAffinity = VerifyType(i, DbType.Guid);
			if (typeAffinity == TypeAffinity.Blob)
			{
				byte[] array = new byte[16];
				_activeStatement._sql.GetBytes(_activeStatement, i, 0, array, 0, 16);
				return new Guid(array);
			}
			return new Guid(_activeStatement._sql.GetText(_activeStatement, i));
		}

		public override short GetInt16(int i)
		{
			CheckDisposed();
			VerifyForGet();
			if (i >= PrivateVisibleFieldCount && _keyInfo != null)
			{
				return _keyInfo.GetInt16(i - PrivateVisibleFieldCount);
			}
			VerifyType(i, DbType.Int16);
			return Convert.ToInt16(_activeStatement._sql.GetInt32(_activeStatement, i));
		}

		public override int GetInt32(int i)
		{
			CheckDisposed();
			VerifyForGet();
			if (i >= PrivateVisibleFieldCount && _keyInfo != null)
			{
				return _keyInfo.GetInt32(i - PrivateVisibleFieldCount);
			}
			VerifyType(i, DbType.Int32);
			return _activeStatement._sql.GetInt32(_activeStatement, i);
		}

		public override long GetInt64(int i)
		{
			CheckDisposed();
			VerifyForGet();
			if (i >= PrivateVisibleFieldCount && _keyInfo != null)
			{
				return _keyInfo.GetInt64(i - PrivateVisibleFieldCount);
			}
			VerifyType(i, DbType.Int64);
			return _activeStatement._sql.GetInt64(_activeStatement, i);
		}

		public override string GetName(int i)
		{
			CheckDisposed();
			if (i >= PrivateVisibleFieldCount && _keyInfo != null)
			{
				return _keyInfo.GetName(i - PrivateVisibleFieldCount);
			}
			return _activeStatement._sql.ColumnName(_activeStatement, i);
		}

		public override int GetOrdinal(string name)
		{
			CheckDisposed();
			_ = _throwOnDisposed;
			if (_fieldIndexes == null)
			{
				_fieldIndexes = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
			}
			if (!_fieldIndexes.TryGetValue(name, out var value))
			{
				value = _activeStatement._sql.ColumnIndex(_activeStatement, name);
				if (value == -1 && _keyInfo != null)
				{
					value = _keyInfo.GetOrdinal(name);
					if (value > -1)
					{
						value += PrivateVisibleFieldCount;
					}
				}
				_fieldIndexes.Add(name, value);
			}
			return value;
		}

		public override DataTable GetSchemaTable()
		{
			CheckDisposed();
			return GetSchemaTable(wantUniqueInfo: true, wantDefaultValue: false);
		}

		private static void GetStatementColumnParents(SQLiteBase sql, SQLiteStatement stmt, int fieldCount, ref Dictionary<ColumnParent, List<int>> parentToColumns, ref Dictionary<int, ColumnParent> columnToParent)
		{
			if (parentToColumns == null)
			{
				parentToColumns = new Dictionary<ColumnParent, List<int>>(new ColumnParent());
			}
			if (columnToParent == null)
			{
				columnToParent = new Dictionary<int, ColumnParent>();
			}
			for (int i = 0; i < fieldCount; i++)
			{
				string databaseName = sql.ColumnDatabaseName(stmt, i);
				string tableName = sql.ColumnTableName(stmt, i);
				string columnName = sql.ColumnOriginalName(stmt, i);
				ColumnParent key = new ColumnParent(databaseName, tableName, null);
				ColumnParent value = new ColumnParent(databaseName, tableName, columnName);
				if (!parentToColumns.TryGetValue(key, out var value2))
				{
					parentToColumns.Add(key, new List<int>(new int[1] { i }));
				}
				else if (value2 != null)
				{
					value2.Add(i);
				}
				else
				{
					parentToColumns[key] = new List<int>(new int[1] { i });
				}
				columnToParent.Add(i, value);
			}
		}

		private static int CountParents(Dictionary<ColumnParent, List<int>> parentToColumns)
		{
			int num = 0;
			if (parentToColumns != null)
			{
				foreach (ColumnParent key in parentToColumns.Keys)
				{
					if (key != null)
					{
						string tableName = key.TableName;
						if (!string.IsNullOrEmpty(tableName))
						{
							num++;
						}
					}
				}
			}
			return num;
		}

		internal DataTable GetSchemaTable(bool wantUniqueInfo, bool wantDefaultValue)
		{
			CheckClosed();
			_ = _throwOnDisposed;
			Dictionary<ColumnParent, List<int>> parentToColumns = null;
			Dictionary<int, ColumnParent> columnToParent = null;
			GetStatementColumnParents(_command.Connection._sql, _activeStatement, _fieldCount, ref parentToColumns, ref columnToParent);
			DataTable dataTable = new DataTable("SchemaTable");
			DataTable dataTable2 = null;
			string text = "";
			string text2 = "";
			string text3 = "";
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.Add(SchemaTableColumn.ColumnName, typeof(string));
			dataTable.Columns.Add(SchemaTableColumn.ColumnOrdinal, typeof(int));
			dataTable.Columns.Add(SchemaTableColumn.ColumnSize, typeof(int));
			dataTable.Columns.Add(SchemaTableColumn.NumericPrecision, typeof(int));
			dataTable.Columns.Add(SchemaTableColumn.NumericScale, typeof(int));
			dataTable.Columns.Add(SchemaTableColumn.IsUnique, typeof(bool));
			dataTable.Columns.Add(SchemaTableColumn.IsKey, typeof(bool));
			dataTable.Columns.Add(SchemaTableOptionalColumn.BaseServerName, typeof(string));
			dataTable.Columns.Add(SchemaTableOptionalColumn.BaseCatalogName, typeof(string));
			dataTable.Columns.Add(SchemaTableColumn.BaseColumnName, typeof(string));
			dataTable.Columns.Add(SchemaTableColumn.BaseSchemaName, typeof(string));
			dataTable.Columns.Add(SchemaTableColumn.BaseTableName, typeof(string));
			dataTable.Columns.Add(SchemaTableColumn.DataType, typeof(Type));
			dataTable.Columns.Add(SchemaTableColumn.AllowDBNull, typeof(bool));
			dataTable.Columns.Add(SchemaTableColumn.ProviderType, typeof(int));
			dataTable.Columns.Add(SchemaTableColumn.IsAliased, typeof(bool));
			dataTable.Columns.Add(SchemaTableColumn.IsExpression, typeof(bool));
			dataTable.Columns.Add(SchemaTableOptionalColumn.IsAutoIncrement, typeof(bool));
			dataTable.Columns.Add(SchemaTableOptionalColumn.IsRowVersion, typeof(bool));
			dataTable.Columns.Add(SchemaTableOptionalColumn.IsHidden, typeof(bool));
			dataTable.Columns.Add(SchemaTableColumn.IsLong, typeof(bool));
			dataTable.Columns.Add(SchemaTableOptionalColumn.IsReadOnly, typeof(bool));
			dataTable.Columns.Add(SchemaTableOptionalColumn.ProviderSpecificDataType, typeof(Type));
			dataTable.Columns.Add(SchemaTableOptionalColumn.DefaultValue, typeof(object));
			dataTable.Columns.Add("DataTypeName", typeof(string));
			dataTable.Columns.Add("CollationType", typeof(string));
			dataTable.BeginLoadData();
			for (int i = 0; i < _fieldCount; i++)
			{
				SQLiteType sQLiteType = GetSQLiteType(_flags, i);
				DataRow dataRow = dataTable.NewRow();
				DbType type = sQLiteType.Type;
				dataRow[SchemaTableColumn.ColumnName] = GetName(i);
				dataRow[SchemaTableColumn.ColumnOrdinal] = i;
				dataRow[SchemaTableColumn.ColumnSize] = SQLiteConvert.DbTypeToColumnSize(type);
				dataRow[SchemaTableColumn.NumericPrecision] = SQLiteConvert.DbTypeToNumericPrecision(type);
				dataRow[SchemaTableColumn.NumericScale] = SQLiteConvert.DbTypeToNumericScale(type);
				dataRow[SchemaTableColumn.ProviderType] = sQLiteType.Type;
				dataRow[SchemaTableColumn.IsLong] = false;
				dataRow[SchemaTableColumn.AllowDBNull] = true;
				dataRow[SchemaTableOptionalColumn.IsReadOnly] = false;
				dataRow[SchemaTableOptionalColumn.IsRowVersion] = false;
				dataRow[SchemaTableColumn.IsUnique] = false;
				dataRow[SchemaTableColumn.IsKey] = false;
				dataRow[SchemaTableOptionalColumn.IsAutoIncrement] = false;
				dataRow[SchemaTableColumn.DataType] = GetFieldType(i);
				dataRow[SchemaTableOptionalColumn.IsHidden] = false;
				dataRow[SchemaTableColumn.BaseSchemaName] = _baseSchemaName;
				text3 = columnToParent[i].ColumnName;
				if (!string.IsNullOrEmpty(text3))
				{
					dataRow[SchemaTableColumn.BaseColumnName] = text3;
				}
				dataRow[SchemaTableColumn.IsExpression] = string.IsNullOrEmpty(text3);
				dataRow[SchemaTableColumn.IsAliased] = string.Compare(GetName(i), text3, StringComparison.OrdinalIgnoreCase) != 0;
				string tableName = columnToParent[i].TableName;
				if (!string.IsNullOrEmpty(tableName))
				{
					dataRow[SchemaTableColumn.BaseTableName] = tableName;
				}
				tableName = columnToParent[i].DatabaseName;
				if (!string.IsNullOrEmpty(tableName))
				{
					dataRow[SchemaTableOptionalColumn.BaseCatalogName] = tableName;
				}
				string dataType = null;
				if (!string.IsNullOrEmpty(text3))
				{
					string collateSequence = null;
					bool notNull = false;
					bool primaryKey = false;
					bool autoIncrement = false;
					_command.Connection._sql.ColumnMetaData((string)dataRow[SchemaTableOptionalColumn.BaseCatalogName], (string)dataRow[SchemaTableColumn.BaseTableName], text3, ref dataType, ref collateSequence, ref notNull, ref primaryKey, ref autoIncrement);
					if (notNull || primaryKey)
					{
						dataRow[SchemaTableColumn.AllowDBNull] = false;
					}
					dataRow[SchemaTableColumn.IsKey] = primaryKey && CountParents(parentToColumns) <= 1;
					dataRow[SchemaTableOptionalColumn.IsAutoIncrement] = autoIncrement;
					dataRow["CollationType"] = collateSequence;
					string[] array = dataType.Split(new char[1] { '(' });
					if (array.Length > 1)
					{
						dataType = array[0];
						array = array[1].Split(new char[1] { ')' });
						if (array.Length > 1)
						{
							array = array[0].Split(',', '.');
							if (sQLiteType.Type == DbType.Binary || SQLiteConvert.IsStringDbType(sQLiteType.Type))
							{
								dataRow[SchemaTableColumn.ColumnSize] = Convert.ToInt32(array[0], CultureInfo.InvariantCulture);
							}
							else
							{
								dataRow[SchemaTableColumn.NumericPrecision] = Convert.ToInt32(array[0], CultureInfo.InvariantCulture);
								if (array.Length > 1)
								{
									dataRow[SchemaTableColumn.NumericScale] = Convert.ToInt32(array[1], CultureInfo.InvariantCulture);
								}
							}
						}
					}
					if (wantDefaultValue)
					{
						using SQLiteCommand sQLiteCommand = new SQLiteCommand(string.Format(CultureInfo.InvariantCulture, "PRAGMA [{0}].TABLE_INFO([{1}])", new object[2]
						{
							dataRow[SchemaTableOptionalColumn.BaseCatalogName],
							dataRow[SchemaTableColumn.BaseTableName]
						}), _command.Connection);
						using DbDataReader dbDataReader = sQLiteCommand.ExecuteReader();
						while (dbDataReader.Read())
						{
							if (string.Compare((string)dataRow[SchemaTableColumn.BaseColumnName], dbDataReader.GetString(1), StringComparison.OrdinalIgnoreCase) == 0)
							{
								if (!dbDataReader.IsDBNull(4))
								{
									dataRow[SchemaTableOptionalColumn.DefaultValue] = dbDataReader[4];
								}
								break;
							}
						}
					}
					if (wantUniqueInfo)
					{
						if ((string)dataRow[SchemaTableOptionalColumn.BaseCatalogName] != text || (string)dataRow[SchemaTableColumn.BaseTableName] != text2)
						{
							text = (string)dataRow[SchemaTableOptionalColumn.BaseCatalogName];
							text2 = (string)dataRow[SchemaTableColumn.BaseTableName];
							dataTable2 = _command.Connection.GetSchema("Indexes", new string[4]
							{
								(string)dataRow[SchemaTableOptionalColumn.BaseCatalogName],
								null,
								(string)dataRow[SchemaTableColumn.BaseTableName],
								null
							});
						}
						foreach (DataRow row in dataTable2.Rows)
						{
							DataTable schema = _command.Connection.GetSchema("IndexColumns", new string[5]
							{
								(string)dataRow[SchemaTableOptionalColumn.BaseCatalogName],
								null,
								(string)dataRow[SchemaTableColumn.BaseTableName],
								(string)row["INDEX_NAME"],
								null
							});
							foreach (DataRow row2 in schema.Rows)
							{
								if (string.Compare((string)row2["COLUMN_NAME"], text3, StringComparison.OrdinalIgnoreCase) == 0)
								{
									if (parentToColumns.Count == 1 && schema.Rows.Count == 1 && !(bool)dataRow[SchemaTableColumn.AllowDBNull])
									{
										dataRow[SchemaTableColumn.IsUnique] = row["UNIQUE"];
									}
									if (schema.Rows.Count == 1 && (bool)row["PRIMARY_KEY"] && !string.IsNullOrEmpty(dataType) && string.Compare(dataType, "integer", StringComparison.OrdinalIgnoreCase) != 0)
									{
									}
									break;
								}
							}
						}
					}
					if (string.IsNullOrEmpty(dataType))
					{
						TypeAffinity nAffinity = TypeAffinity.Uninitialized;
						dataType = _activeStatement._sql.ColumnType(_activeStatement, i, ref nAffinity);
					}
					if (!string.IsNullOrEmpty(dataType))
					{
						dataRow["DataTypeName"] = dataType;
					}
				}
				dataTable.Rows.Add(dataRow);
			}
			if (_keyInfo != null)
			{
				_keyInfo.AppendSchemaTable(dataTable);
			}
			dataTable.AcceptChanges();
			dataTable.EndLoadData();
			return dataTable;
		}

		public override string GetString(int i)
		{
			CheckDisposed();
			VerifyForGet();
			if (i >= PrivateVisibleFieldCount && _keyInfo != null)
			{
				return _keyInfo.GetString(i - PrivateVisibleFieldCount);
			}
			VerifyType(i, DbType.String);
			return _activeStatement._sql.GetText(_activeStatement, i);
		}

		public override object GetValue(int i)
		{
			CheckDisposed();
			VerifyForGet();
			if (i >= PrivateVisibleFieldCount && _keyInfo != null)
			{
				return _keyInfo.GetValue(i - PrivateVisibleFieldCount);
			}
			SQLiteType sQLiteType = GetSQLiteType(_flags, i);
			if ((_flags & SQLiteConnectionFlags.DetectTextAffinity) == SQLiteConnectionFlags.DetectTextAffinity && (sQLiteType == null || sQLiteType.Affinity == TypeAffinity.Text))
			{
				sQLiteType = GetSQLiteType(sQLiteType, _activeStatement._sql.GetText(_activeStatement, i));
			}
			else if ((_flags & SQLiteConnectionFlags.DetectStringType) == SQLiteConnectionFlags.DetectStringType && (sQLiteType == null || SQLiteConvert.IsStringDbType(sQLiteType.Type)))
			{
				sQLiteType = GetSQLiteType(sQLiteType, _activeStatement._sql.GetText(_activeStatement, i));
			}
			return _activeStatement._sql.GetValue(_activeStatement, _flags, i, sQLiteType);
		}

		public override int GetValues(object[] values)
		{
			CheckDisposed();
			int num = FieldCount;
			if (values.Length < num)
			{
				num = values.Length;
			}
			for (int i = 0; i < num; i++)
			{
				values[i] = GetValue(i);
			}
			return num;
		}

		public NameValueCollection GetValues()
		{
			CheckDisposed();
			if (_activeStatement == null || _activeStatement._sql == null)
			{
				throw new InvalidOperationException();
			}
			int privateVisibleFieldCount = PrivateVisibleFieldCount;
			NameValueCollection nameValueCollection = new NameValueCollection(privateVisibleFieldCount);
			for (int i = 0; i < privateVisibleFieldCount; i++)
			{
				string name = _activeStatement._sql.ColumnName(_activeStatement, i);
				string text = _activeStatement._sql.GetText(_activeStatement, i);
				nameValueCollection.Add(name, text);
			}
			return nameValueCollection;
		}

		public override bool IsDBNull(int i)
		{
			CheckDisposed();
			VerifyForGet();
			if (i >= PrivateVisibleFieldCount && _keyInfo != null)
			{
				return _keyInfo.IsDBNull(i - PrivateVisibleFieldCount);
			}
			return _activeStatement._sql.IsNull(_activeStatement, i);
		}

		public override bool NextResult()
		{
			CheckDisposed();
			CheckClosed();
			_ = _throwOnDisposed;
			SQLiteStatement sQLiteStatement = null;
			bool flag = (_commandBehavior & CommandBehavior.SchemaOnly) != 0;
			int num;
			while (true)
			{
				if (sQLiteStatement == null && _activeStatement != null && _activeStatement._sql != null && _activeStatement._sql.IsOpen())
				{
					if (!flag)
					{
						_activeStatement._sql.Reset(_activeStatement);
					}
					if ((_commandBehavior & CommandBehavior.SingleResult) != CommandBehavior.Default)
					{
						while (true)
						{
							sQLiteStatement = _command.GetStatement(_activeStatementIndex + 1);
							if (sQLiteStatement == null)
							{
								break;
							}
							_activeStatementIndex++;
							if (!flag && sQLiteStatement._sql.Step(sQLiteStatement))
							{
								_stepCount++;
							}
							if (sQLiteStatement._sql.ColumnCount(sQLiteStatement) == 0)
							{
								int changes = 0;
								bool readOnly = false;
								if (!sQLiteStatement.TryGetChanges(ref changes, ref readOnly))
								{
									return false;
								}
								if (!readOnly)
								{
									if (_rowsAffected == -1)
									{
										_rowsAffected = 0;
									}
									_rowsAffected += changes;
								}
							}
							if (!flag)
							{
								sQLiteStatement._sql.Reset(sQLiteStatement);
							}
						}
						return false;
					}
				}
				sQLiteStatement = _command.GetStatement(_activeStatementIndex + 1);
				if (sQLiteStatement == null)
				{
					return false;
				}
				if (_readingState < 1)
				{
					_readingState = 1;
				}
				_activeStatementIndex++;
				num = sQLiteStatement._sql.ColumnCount(sQLiteStatement);
				if (flag && num != 0)
				{
					break;
				}
				if (!flag && sQLiteStatement._sql.Step(sQLiteStatement))
				{
					_stepCount++;
					_readingState = -1;
					break;
				}
				if (num == 0)
				{
					int changes2 = 0;
					bool readOnly2 = false;
					if (sQLiteStatement.TryGetChanges(ref changes2, ref readOnly2))
					{
						if (!readOnly2)
						{
							if (_rowsAffected == -1)
							{
								_rowsAffected = 0;
							}
							_rowsAffected += changes2;
						}
						if (!flag)
						{
							sQLiteStatement._sql.Reset(sQLiteStatement);
						}
						continue;
					}
					return false;
				}
				_readingState = 1;
				break;
			}
			_activeStatement = sQLiteStatement;
			_fieldCount = num;
			_fieldIndexes = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
			_fieldTypeArray = new SQLiteType[PrivateVisibleFieldCount];
			if ((_commandBehavior & CommandBehavior.KeyInfo) != CommandBehavior.Default)
			{
				LoadKeyInfo();
			}
			return true;
		}

		private static SQLiteConnection GetConnection(SQLiteDataReader dataReader)
		{
			try
			{
				if (dataReader != null)
				{
					SQLiteCommand command = dataReader._command;
					if (command != null)
					{
						SQLiteConnection connection = command.Connection;
						if (connection != null)
						{
							return connection;
						}
					}
				}
			}
			catch (ObjectDisposedException)
			{
			}
			return null;
		}

		private SQLiteType GetSQLiteType(SQLiteType oldType, string text)
		{
			if (SQLiteConvert.LooksLikeNull(text))
			{
				return new SQLiteType(TypeAffinity.Null, DbType.Object);
			}
			if (SQLiteConvert.LooksLikeInt64(text))
			{
				return new SQLiteType(TypeAffinity.Int64, DbType.Int64);
			}
			if (SQLiteConvert.LooksLikeDouble(text))
			{
				return new SQLiteType(TypeAffinity.Double, DbType.Double);
			}
			if (_activeStatement != null && SQLiteConvert.LooksLikeDateTime(_activeStatement._sql, text))
			{
				return new SQLiteType(TypeAffinity.DateTime, DbType.DateTime);
			}
			return oldType;
		}

		private SQLiteType GetSQLiteType(SQLiteConnectionFlags flags, int i)
		{
			SQLiteType sQLiteType = _fieldTypeArray[i];
			if (sQLiteType == null)
			{
				sQLiteType = (_fieldTypeArray[i] = new SQLiteType());
			}
			if (sQLiteType.Affinity == TypeAffinity.Uninitialized)
			{
				sQLiteType.Type = SQLiteConvert.TypeNameToDbType(GetConnection(this), _activeStatement._sql.ColumnType(_activeStatement, i, ref sQLiteType.Affinity), flags);
			}
			else
			{
				sQLiteType.Affinity = _activeStatement._sql.ColumnAffinity(_activeStatement, i);
			}
			return sQLiteType;
		}

		public override bool Read()
		{
			CheckDisposed();
			CheckClosed();
			_ = _throwOnDisposed;
			if ((_commandBehavior & CommandBehavior.SchemaOnly) != CommandBehavior.Default)
			{
				return false;
			}
			if (_readingState == -1)
			{
				_readingState = 0;
				return true;
			}
			if (_readingState == 0)
			{
				if ((_commandBehavior & CommandBehavior.SingleRow) == 0 && _activeStatement._sql.Step(_activeStatement))
				{
					_stepCount++;
					if (_keyInfo != null)
					{
						_keyInfo.Reset();
					}
					return true;
				}
				_readingState = 1;
			}
			return false;
		}

		private void LoadKeyInfo()
		{
			if (_keyInfo != null)
			{
				_keyInfo.Dispose();
				_keyInfo = null;
			}
			_keyInfo = new SQLiteKeyReader(_command.Connection, this, _activeStatement);
		}
	}
	internal static class SQLiteDefineConstants
	{
		public static readonly IList<string> OptionList = new List<string>(new string[14]
		{
			"INTEROP_CODEC", "INTEROP_EXTENSION_FUNCTIONS", "INTEROP_VIRTUAL_TABLE", "NET_35", "PRELOAD_NATIVE_LIBRARY", "SQLITE_STANDARD", "THROW_ON_DISPOSED", "TRACE", "TRACE_PRELOAD", "TRACE_SHARED",
			"TRACE_WARNING", "USE_PREPARE_V2", "WINDOWS", null
		});
	}
	[Serializable]
	public sealed class SQLiteException : DbException, ISerializable
	{
		private SQLiteErrorCode _errorCode;

		public SQLiteErrorCode ResultCode => _errorCode;

		public override int ErrorCode => (int)_errorCode;

		private SQLiteException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			_errorCode = (SQLiteErrorCode)info.GetInt32("errorCode");
		}

		public SQLiteException(SQLiteErrorCode errorCode, string message)
			: base(GetStockErrorMessage(errorCode, message))
		{
			_errorCode = errorCode;
		}

		public SQLiteException(string message)
			: this(SQLiteErrorCode.Unknown, message)
		{
		}

		public SQLiteException()
		{
		}

		public SQLiteException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info?.AddValue("errorCode", _errorCode);
			base.GetObjectData(info, context);
		}

		private static string GetErrorString(SQLiteErrorCode errorCode)
		{
			BindingFlags invokeAttr = BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.InvokeMethod;
			return typeof(SQLite3).InvokeMember("GetErrorString", invokeAttr, null, null, new object[1] { errorCode }) as string;
		}

		private static string GetStockErrorMessage(SQLiteErrorCode errorCode, string message)
		{
			return $"{GetErrorString(errorCode)}{Environment.NewLine}{message}".Trim();
		}
	}
	public enum SQLiteErrorCode
	{
		Unknown = -1,
		Ok = 0,
		Error = 1,
		Internal = 2,
		Perm = 3,
		Abort = 4,
		Busy = 5,
		Locked = 6,
		NoMem = 7,
		ReadOnly = 8,
		Interrupt = 9,
		IoErr = 10,
		Corrupt = 11,
		NotFound = 12,
		Full = 13,
		CantOpen = 14,
		Protocol = 15,
		Empty = 16,
		Schema = 17,
		TooBig = 18,
		Constraint = 19,
		Mismatch = 20,
		Misuse = 21,
		NoLfs = 22,
		Auth = 23,
		Format = 24,
		Range = 25,
		NotADb = 26,
		Notice = 27,
		Warning = 28,
		Row = 100,
		Done = 101,
		NonExtendedMask = 255
	}
	public sealed class SQLiteFactory : DbProviderFactory, IDisposable, IServiceProvider
	{
		private bool disposed;

		public static readonly SQLiteFactory Instance;

		private static readonly string DefaultTypeName;

		private static readonly BindingFlags DefaultBindingFlags;

		private static Type _dbProviderServicesType;

		private static object _sqliteServices;

		public event SQLiteLogEventHandler Log
		{
			add
			{
				CheckDisposed();
				SQLiteLog.Log += value;
			}
			remove
			{
				CheckDisposed();
				SQLiteLog.Log -= value;
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		private void CheckDisposed()
		{
			if (disposed)
			{
				throw new ObjectDisposedException(typeof(SQLiteFactory).Name);
			}
		}

		private void Dispose(bool disposing)
		{
			if (!disposed)
			{
				disposed = true;
			}
		}

		~SQLiteFactory()
		{
			Dispose(disposing: false);
		}

		public override DbCommand CreateCommand()
		{
			CheckDisposed();
			return new SQLiteCommand();
		}

		public override DbCommandBuilder CreateCommandBuilder()
		{
			CheckDisposed();
			return new SQLiteCommandBuilder();
		}

		public override DbConnection CreateConnection()
		{
			CheckDisposed();
			return new SQLiteConnection();
		}

		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			CheckDisposed();
			return new SQLiteConnectionStringBuilder();
		}

		public override DbDataAdapter CreateDataAdapter()
		{
			CheckDisposed();
			return new SQLiteDataAdapter();
		}

		public override DbParameter CreateParameter()
		{
			CheckDisposed();
			return new SQLiteParameter();
		}

		static SQLiteFactory()
		{
			Instance = new SQLiteFactory();
			DefaultTypeName = "System.Data.SQLite.Linq.SQLiteProviderServices, System.Data.SQLite.Linq, Version={0}, Culture=neutral, PublicKeyToken=db937bc2d44ff139";
			DefaultBindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic;
			UnsafeNativeMethods.Initialize();
			SQLiteLog.Initialize();
			string text = "3.5.0.0";
			_dbProviderServicesType = Type.GetType(string.Format(CultureInfo.InvariantCulture, "System.Data.Common.DbProviderServices, System.Data.Entity, Version={0}, Culture=neutral, PublicKeyToken=b77a5c561934e089", new object[1] { text }), throwOnError: false);
		}

		object IServiceProvider.GetService(Type serviceType)
		{
			if ((object)serviceType == typeof(ISQLiteSchemaExtensions) || ((object)_dbProviderServicesType != null && (object)serviceType == _dbProviderServicesType))
			{
				return GetSQLiteProviderServicesInstance();
			}
			return null;
		}

		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		private object GetSQLiteProviderServicesInstance()
		{
			if (_sqliteServices == null)
			{
				string settingValue = UnsafeNativeMethods.GetSettingValue("TypeName_SQLiteProviderServices", null);
				Version version = GetType().Assembly.GetName().Version;
				settingValue = ((settingValue == null) ? string.Format(CultureInfo.InvariantCulture, DefaultTypeName, new object[1] { version }) : string.Format(CultureInfo.InvariantCulture, settingValue, new object[1] { version }));
				Type type = Type.GetType(settingValue, throwOnError: false);
				if ((object)type != null)
				{
					FieldInfo field = type.GetField("Instance", DefaultBindingFlags);
					if ((object)field != null)
					{
						_sqliteServices = field.GetValue(null);
					}
				}
			}
			return _sqliteServices;
		}
	}
	public abstract class SQLiteFunction : IDisposable
	{
		private class AggregateData
		{
			internal int _count = 1;

			internal object _data;
		}

		internal SQLiteBase _base;

		private Dictionary<IntPtr, AggregateData> _contextDataList;

		private SQLiteConnectionFlags _flags;

		private SQLiteCallback _InvokeFunc;

		private SQLiteCallback _StepFunc;

		private SQLiteFinalCallback _FinalFunc;

		private SQLiteCollation _CompareFunc;

		private SQLiteCollation _CompareFunc16;

		internal IntPtr _context;

		private static List<SQLiteFunctionAttribute> _registeredFunctions;

		private bool disposed;

		public SQLiteConvert SQLiteConvert
		{
			get
			{
				CheckDisposed();
				return _base;
			}
		}

		protected SQLiteFunction()
		{
			_contextDataList = new Dictionary<IntPtr, AggregateData>();
		}

		protected SQLiteFunction(SQLiteDateFormats format, DateTimeKind kind, string formatString, bool utf16)
			: this()
		{
			if (utf16)
			{
				_base = new SQLite3_UTF16(format, kind, formatString, IntPtr.Zero, null, ownHandle: false);
			}
			else
			{
				_base = new SQLite3(format, kind, formatString, IntPtr.Zero, null, ownHandle: false);
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		private void CheckDisposed()
		{
			if (disposed)
			{
				throw new ObjectDisposedException(typeof(SQLiteFunction).Name);
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposed)
			{
				return;
			}
			if (disposing)
			{
				foreach (KeyValuePair<IntPtr, AggregateData> contextData in _contextDataList)
				{
					if (contextData.Value._data is IDisposable disposable)
					{
						disposable.Dispose();
					}
				}
				_contextDataList.Clear();
				_contextDataList = null;
				_flags = SQLiteConnectionFlags.None;
				_InvokeFunc = null;
				_StepFunc = null;
				_FinalFunc = null;
				_CompareFunc = null;
				_base = null;
			}
			disposed = true;
		}

		~SQLiteFunction()
		{
			Dispose(disposing: false);
		}

		public virtual object Invoke(object[] args)
		{
			CheckDisposed();
			return null;
		}

		public virtual void Step(object[] args, int stepNumber, ref object contextData)
		{
			CheckDisposed();
		}

		public virtual object Final(object contextData)
		{
			CheckDisposed();
			return null;
		}

		public virtual int Compare(string param1, string param2)
		{
			CheckDisposed();
			return 0;
		}

		internal object[] ConvertParams(int nArgs, IntPtr argsptr)
		{
			object[] array = new object[nArgs];
			IntPtr[] array2 = new IntPtr[nArgs];
			Marshal.Copy(argsptr, array2, 0, nArgs);
			for (int i = 0; i < nArgs; i++)
			{
				switch (_base.GetParamValueType(array2[i]))
				{
				case TypeAffinity.Null:
					array[i] = DBNull.Value;
					break;
				case TypeAffinity.Int64:
					array[i] = _base.GetParamValueInt64(array2[i]);
					break;
				case TypeAffinity.Double:
					array[i] = _base.GetParamValueDouble(array2[i]);
					break;
				case TypeAffinity.Text:
					array[i] = _base.GetParamValueText(array2[i]);
					break;
				case TypeAffinity.Blob:
				{
					int num = (int)_base.GetParamValueBytes(array2[i], 0, null, 0, 0);
					byte[] array3 = new byte[num];
					_base.GetParamValueBytes(array2[i], 0, array3, 0, num);
					array[i] = array3;
					break;
				}
				case TypeAffinity.DateTime:
					array[i] = _base.ToDateTime(_base.GetParamValueText(array2[i]));
					break;
				}
			}
			return array;
		}

		private void SetReturnValue(IntPtr context, object returnValue)
		{
			if (returnValue == null || returnValue == DBNull.Value)
			{
				_base.ReturnNull(context);
				return;
			}
			Type type = returnValue.GetType();
			if ((object)type == typeof(DateTime))
			{
				_base.ReturnText(context, _base.ToString((DateTime)returnValue));
				return;
			}
			if (returnValue is Exception ex)
			{
				_base.ReturnError(context, ex.Message);
				return;
			}
			switch (SQLiteConvert.TypeToAffinity(type))
			{
			case TypeAffinity.Null:
				_base.ReturnNull(context);
				break;
			case TypeAffinity.Int64:
				_base.ReturnInt64(context, Convert.ToInt64(returnValue, CultureInfo.CurrentCulture));
				break;
			case TypeAffinity.Double:
				_base.ReturnDouble(context, Convert.ToDouble(returnValue, CultureInfo.CurrentCulture));
				break;
			case TypeAffinity.Text:
				_base.ReturnText(context, returnValue.ToString());
				break;
			case TypeAffinity.Blob:
				_base.ReturnBlob(context, (byte[])returnValue);
				break;
			}
		}

		internal void ScalarCallback(IntPtr context, int nArgs, IntPtr argsptr)
		{
			try
			{
				_context = context;
				SetReturnValue(context, Invoke(ConvertParams(nArgs, argsptr)));
			}
			catch (Exception ex)
			{
				try
				{
					if ((_flags & SQLiteConnectionFlags.LogCallbackException) == SQLiteConnectionFlags.LogCallbackException)
					{
						SQLiteLog.LogMessage(-2146233088, string.Format(CultureInfo.CurrentCulture, "Caught exception in \"Invoke\" method: {0}", new object[1] { ex }));
					}
				}
				catch
				{
				}
			}
		}

		internal int CompareCallback(IntPtr ptr, int len1, IntPtr ptr1, int len2, IntPtr ptr2)
		{
			try
			{
				return Compare(SQLiteConvert.UTF8ToString(ptr1, len1), SQLiteConvert.UTF8ToString(ptr2, len2));
			}
			catch (Exception ex)
			{
				try
				{
					if ((_flags & SQLiteConnectionFlags.LogCallbackException) == SQLiteConnectionFlags.LogCallbackException)
					{
						SQLiteLog.LogMessage(-2146233088, string.Format(CultureInfo.CurrentCulture, "Caught exception in \"Compare\" (UTF8) method: {0}", new object[1] { ex }));
					}
				}
				catch
				{
				}
			}
			if (_base != null && _base.IsOpen())
			{
				_base.Cancel();
			}
			return 0;
		}

		internal int CompareCallback16(IntPtr ptr, int len1, IntPtr ptr1, int len2, IntPtr ptr2)
		{
			try
			{
				return Compare(SQLite3_UTF16.UTF16ToString(ptr1, len1), SQLite3_UTF16.UTF16ToString(ptr2, len2));
			}
			catch (Exception ex)
			{
				try
				{
					if ((_flags & SQLiteConnectionFlags.LogCallbackException) == SQLiteConnectionFlags.LogCallbackException)
					{
						SQLiteLog.LogMessage(-2146233088, string.Format(CultureInfo.CurrentCulture, "Caught exception in \"Compare\" (UTF16) method: {0}", new object[1] { ex }));
					}
				}
				catch
				{
				}
			}
			if (_base != null && _base.IsOpen())
			{
				_base.Cancel();
			}
			return 0;
		}

		internal void StepCallback(IntPtr context, int nArgs, IntPtr argsptr)
		{
			try
			{
				AggregateData value = null;
				if (_base != null)
				{
					IntPtr key = _base.AggregateContext(context);
					if (_contextDataList != null && !_contextDataList.TryGetValue(key, out value))
					{
						value = new AggregateData();
						_contextDataList[key] = value;
					}
				}
				if (value == null)
				{
					value = new AggregateData();
				}
				try
				{
					_context = context;
					Step(ConvertParams(nArgs, argsptr), value._count, ref value._data);
				}
				finally
				{
					value._count++;
				}
			}
			catch (Exception ex)
			{
				try
				{
					if ((_flags & SQLiteConnectionFlags.LogCallbackException) == SQLiteConnectionFlags.LogCallbackException)
					{
						SQLiteLog.LogMessage(-2146233088, string.Format(CultureInfo.CurrentCulture, "Caught exception in \"Step\" method: {1}", new object[1] { ex }));
					}
				}
				catch
				{
				}
			}
		}

		internal void FinalCallback(IntPtr context)
		{
			try
			{
				object obj = null;
				if (_base != null)
				{
					IntPtr key = _base.AggregateContext(context);
					if (_contextDataList != null && _contextDataList.TryGetValue(key, out var value))
					{
						obj = value._data;
						_contextDataList.Remove(key);
					}
				}
				try
				{
					_context = context;
					SetReturnValue(context, Final(obj));
				}
				finally
				{
					if (obj is IDisposable disposable)
					{
						disposable.Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				try
				{
					if ((_flags & SQLiteConnectionFlags.LogCallbackException) == SQLiteConnectionFlags.LogCallbackException)
					{
						SQLiteLog.LogMessage(-2146233088, string.Format(CultureInfo.CurrentCulture, "Caught exception in \"Final\" method: {1}", new object[1] { ex }));
					}
				}
				catch
				{
				}
			}
		}

		[FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.PathDiscovery)]
		static SQLiteFunction()
		{
			_registeredFunctions = new List<SQLiteFunctionAttribute>();
			try
			{
				if (UnsafeNativeMethods.GetSettingValue("No_SQLiteFunctions", null) != null)
				{
					return;
				}
				Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
				int num = assemblies.Length;
				AssemblyName name = Assembly.GetExecutingAssembly().GetName();
				for (int i = 0; i < num; i++)
				{
					bool flag = false;
					Type[] types;
					try
					{
						AssemblyName[] referencedAssemblies = assemblies[i].GetReferencedAssemblies();
						int num2 = referencedAssemblies.Length;
						for (int j = 0; j < num2; j++)
						{
							if (referencedAssemblies[j].Name == name.Name)
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							continue;
						}
						types = assemblies[i].GetTypes();
						goto IL_00a2;
					}
					catch (ReflectionTypeLoadException ex)
					{
						types = ex.Types;
						goto IL_00a2;
					}
					IL_00a2:
					int num3 = types.Length;
					for (int k = 0; k < num3; k++)
					{
						if ((object)types[k] == null)
						{
							continue;
						}
						object[] customAttributes = types[k].GetCustomAttributes(typeof(SQLiteFunctionAttribute), inherit: false);
						int num4 = customAttributes.Length;
						for (int l = 0; l < num4; l++)
						{
							if (customAttributes[l] is SQLiteFunctionAttribute sQLiteFunctionAttribute)
							{
								sQLiteFunctionAttribute.InstanceType = types[k];
								_registeredFunctions.Add(sQLiteFunctionAttribute);
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		public static void RegisterFunction(Type typ)
		{
			object[] customAttributes = typ.GetCustomAttributes(typeof(SQLiteFunctionAttribute), inherit: false);
			int num = customAttributes.Length;
			for (int i = 0; i < num; i++)
			{
				if (customAttributes[i] is SQLiteFunctionAttribute sQLiteFunctionAttribute)
				{
					sQLiteFunctionAttribute.InstanceType = typ;
					_registeredFunctions.Add(sQLiteFunctionAttribute);
				}
			}
		}

		internal static IEnumerable<SQLiteFunction> BindFunctions(SQLiteBase sqlbase, SQLiteConnectionFlags flags)
		{
			List<SQLiteFunction> list = new List<SQLiteFunction>();
			foreach (SQLiteFunctionAttribute registeredFunction in _registeredFunctions)
			{
				SQLiteFunction sQLiteFunction = (SQLiteFunction)Activator.CreateInstance(registeredFunction.InstanceType);
				BindFunction(sqlbase, registeredFunction, sQLiteFunction, flags);
				list.Add(sQLiteFunction);
			}
			return list;
		}

		internal static void BindFunction(SQLiteBase sqliteBase, SQLiteFunctionAttribute functionAttribute, SQLiteFunction function, SQLiteConnectionFlags flags)
		{
			if (sqliteBase == null)
			{
				throw new ArgumentNullException("sqliteBase");
			}
			if (functionAttribute == null)
			{
				throw new ArgumentNullException("functionAttribute");
			}
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}
			FunctionType funcType = functionAttribute.FuncType;
			function._base = sqliteBase;
			function._flags = flags;
			function._InvokeFunc = ((funcType == FunctionType.Scalar) ? new SQLiteCallback(function.ScalarCallback) : null);
			function._StepFunc = ((funcType == FunctionType.Aggregate) ? new SQLiteCallback(function.StepCallback) : null);
			function._FinalFunc = ((funcType == FunctionType.Aggregate) ? new SQLiteFinalCallback(function.FinalCallback) : null);
			function._CompareFunc = ((funcType == FunctionType.Collation) ? new SQLiteCollation(function.CompareCallback) : null);
			function._CompareFunc16 = ((funcType == FunctionType.Collation) ? new SQLiteCollation(function.CompareCallback16) : null);
			string name = functionAttribute.Name;
			if (funcType != FunctionType.Collation)
			{
				bool needCollSeq = function is SQLiteFunctionEx;
				sqliteBase.CreateFunction(name, functionAttribute.Arguments, needCollSeq, function._InvokeFunc, function._StepFunc, function._FinalFunc);
			}
			else
			{
				sqliteBase.CreateCollation(name, function._CompareFunc, function._CompareFunc16);
			}
		}
	}
	public class SQLiteFunctionEx : SQLiteFunction
	{
		private bool disposed;

		protected CollationSequence GetCollationSequence()
		{
			return _base.GetCollationSequence(this, _context);
		}

		private void CheckDisposed()
		{
			if (disposed)
			{
				throw new ObjectDisposedException(typeof(SQLiteFunctionEx).Name);
			}
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				_ = disposed;
			}
			finally
			{
				base.Dispose(disposing);
				disposed = true;
			}
		}
	}
	public enum FunctionType
	{
		Scalar,
		Aggregate,
		Collation
	}
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void SQLiteCallback(IntPtr context, int argc, IntPtr argv);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void SQLiteFinalCallback(IntPtr context);
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate int SQLiteCollation(IntPtr puser, int len1, IntPtr pv1, int len2, IntPtr pv2);
	public enum CollationTypeEnum
	{
		Binary = 1,
		NoCase = 2,
		Reverse = 3,
		Custom = 0
	}
	public enum CollationEncodingEnum
	{
		UTF8 = 1,
		UTF16LE,
		UTF16BE
	}
	public struct CollationSequence
	{
		public string Name;

		public CollationTypeEnum Type;

		public CollationEncodingEnum Encoding;

		internal SQLiteFunction _func;

		public int Compare(string s1, string s2)
		{
			return _func._base.ContextCollateCompare(Encoding, _func._context, s1, s2);
		}

		public int Compare(char[] c1, char[] c2)
		{
			return _func._base.ContextCollateCompare(Encoding, _func._context, c1, c2);
		}
	}
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public sealed class SQLiteFunctionAttribute : Attribute
	{
		private string _name;

		private int _argumentCount;

		private FunctionType _functionType;

		private Type _instanceType;

		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}

		public int Arguments
		{
			get
			{
				return _argumentCount;
			}
			set
			{
				_argumentCount = value;
			}
		}

		public FunctionType FuncType
		{
			get
			{
				return _functionType;
			}
			set
			{
				_functionType = value;
			}
		}

		internal Type InstanceType
		{
			get
			{
				return _instanceType;
			}
			set
			{
				_instanceType = value;
			}
		}

		public SQLiteFunctionAttribute()
			: this(string.Empty, -1, FunctionType.Scalar)
		{
		}

		public SQLiteFunctionAttribute(string name, int argumentCount, FunctionType functionType)
		{
			_name = name;
			_argumentCount = argumentCount;
			_functionType = functionType;
			_instanceType = null;
		}
	}
	internal sealed class SQLiteKeyReader : IDisposable
	{
		private struct KeyInfo
		{
			internal string databaseName;

			internal string tableName;

			internal string columnName;

			internal int database;

			internal int rootPage;

			internal int cursor;

			internal KeyQuery query;

			internal int column;
		}

		private sealed class KeyQuery : IDisposable
		{
			private SQLiteCommand _command;

			internal SQLiteDataReader _reader;

			private bool disposed;

			internal bool IsValid
			{
				set
				{
					if (value)
					{
						throw new ArgumentException();
					}
					if (_reader != null)
					{
						_reader.Dispose();
						_reader = null;
					}
				}
			}

			internal KeyQuery(SQLiteConnection cnn, string database, string table, params string[] columns)
			{
				using (SQLiteCommandBuilder sQLiteCommandBuilder = new SQLiteCommandBuilder())
				{
					_command = cnn.CreateCommand();
					for (int i = 0; i < columns.Length; i++)
					{
						columns[i] = sQLiteCommandBuilder.QuoteIdentifier(columns[i]);
					}
				}
				_command.CommandText = string.Format(CultureInfo.InvariantCulture, "SELECT {0} FROM [{1}].[{2}] WHERE ROWID = ?", new object[3]
				{
					string.Join(",", columns),
					database,
					table
				});
				_command.Parameters.AddWithValue(null, 0L);
			}

			internal void Sync(long rowid)
			{
				IsValid = false;
				_command.Parameters[0].Value = rowid;
				_reader = _command.ExecuteReader();
				_reader.Read();
			}

			public void Dispose()
			{
				Dispose(disposing: true);
				GC.SuppressFinalize(this);
			}

			private void CheckDisposed()
			{
				if (disposed)
				{
					throw new ObjectDisposedException(typeof(KeyQuery).Name);
				}
			}

			private void Dispose(bool disposing)
			{
				if (disposed)
				{
					return;
				}
				if (disposing)
				{
					IsValid = false;
					if (_command != null)
					{
						_command.Dispose();
					}
					_command = null;
				}
				disposed = true;
			}

			~KeyQuery()
			{
				Dispose(disposing: false);
			}
		}

		private KeyInfo[] _keyInfo;

		private SQLiteStatement _stmt;

		private bool _isValid;

		private bool disposed;

		internal int Count
		{
			get
			{
				if (_keyInfo != null)
				{
					return _keyInfo.Length;
				}
				return 0;
			}
		}

		internal SQLiteKeyReader(SQLiteConnection cnn, SQLiteDataReader reader, SQLiteStatement stmt)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			Dictionary<string, List<string>> dictionary2 = new Dictionary<string, List<string>>();
			List<KeyInfo> list = new List<KeyInfo>();
			_stmt = stmt;
			using (DataTable dataTable = cnn.GetSchema("Catalogs"))
			{
				foreach (DataRow row in dataTable.Rows)
				{
					dictionary.Add((string)row["CATALOG_NAME"], Convert.ToInt32(row["ID"], CultureInfo.InvariantCulture));
				}
			}
			using (DataTable dataTable2 = reader.GetSchemaTable(wantUniqueInfo: false, wantDefaultValue: false))
			{
				foreach (DataRow row2 in dataTable2.Rows)
				{
					if (row2[SchemaTableOptionalColumn.BaseCatalogName] != DBNull.Value)
					{
						string key = (string)row2[SchemaTableOptionalColumn.BaseCatalogName];
						string item = (string)row2[SchemaTableColumn.BaseTableName];
						List<string> list2;
						if (!dictionary2.ContainsKey(key))
						{
							list2 = new List<string>();
							dictionary2.Add(key, list2);
						}
						else
						{
							list2 = dictionary2[key];
						}
						if (!list2.Contains(item))
						{
							list2.Add(item);
						}
					}
				}
				foreach (KeyValuePair<string, List<string>> item2 in dictionary2)
				{
					for (int i = 0; i < item2.Value.Count; i++)
					{
						string text = item2.Value[i];
						DataRow dataRow3 = null;
						using DataTable dataTable3 = cnn.GetSchema("Indexes", new string[3] { item2.Key, null, text });
						for (int j = 0; j < 2; j++)
						{
							if (dataRow3 != null)
							{
								break;
							}
							foreach (DataRow row3 in dataTable3.Rows)
							{
								if (j == 0 && (bool)row3["PRIMARY_KEY"])
								{
									dataRow3 = row3;
									break;
								}
								if (j == 1 && (bool)row3["UNIQUE"])
								{
									dataRow3 = row3;
									break;
								}
							}
						}
						if (dataRow3 == null)
						{
							item2.Value.RemoveAt(i);
							i--;
							continue;
						}
						using DataTable dataTable4 = cnn.GetSchema("Tables", new string[3] { item2.Key, null, text });
						int database = dictionary[item2.Key];
						int rootPage = Convert.ToInt32(dataTable4.Rows[0]["TABLE_ROOTPAGE"], CultureInfo.InvariantCulture);
						int cursorForTable = stmt._sql.GetCursorForTable(stmt, database, rootPage);
						using DataTable dataTable5 = cnn.GetSchema("IndexColumns", new string[4]
						{
							item2.Key,
							null,
							text,
							(string)dataRow3["INDEX_NAME"]
						});
						KeyQuery query = null;
						List<string> list3 = new List<string>();
						for (int k = 0; k < dataTable5.Rows.Count; k++)
						{
							bool flag = true;
							foreach (DataRow row4 in dataTable2.Rows)
							{
								if (!row4.IsNull(SchemaTableColumn.BaseColumnName) && (string)row4[SchemaTableColumn.BaseColumnName] == (string)dataTable5.Rows[k]["COLUMN_NAME"] && (string)row4[SchemaTableColumn.BaseTableName] == text && (string)row4[SchemaTableOptionalColumn.BaseCatalogName] == item2.Key)
								{
									dataTable5.Rows.RemoveAt(k);
									k--;
									flag = false;
									break;
								}
							}
							if (flag)
							{
								list3.Add((string)dataTable5.Rows[k]["COLUMN_NAME"]);
							}
						}
						if ((string)dataRow3["INDEX_NAME"] != "sqlite_master_PK_" + text && list3.Count > 0)
						{
							string[] array = new string[list3.Count];
							list3.CopyTo(array);
							query = new KeyQuery(cnn, item2.Key, text, array);
						}
						for (int l = 0; l < dataTable5.Rows.Count; l++)
						{
							string columnName = (string)dataTable5.Rows[l]["COLUMN_NAME"];
							list.Add(new KeyInfo
							{
								rootPage = rootPage,
								cursor = cursorForTable,
								database = database,
								databaseName = item2.Key,
								tableName = text,
								columnName = columnName,
								query = query,
								column = l
							});
						}
					}
				}
			}
			_keyInfo = new KeyInfo[list.Count];
			list.CopyTo(_keyInfo);
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		private void CheckDisposed()
		{
			if (disposed)
			{
				throw new ObjectDisposedException(typeof(SQLiteKeyReader).Name);
			}
		}

		private void Dispose(bool disposing)
		{
			if (disposed)
			{
				return;
			}
			if (disposing)
			{
				_stmt = null;
				if (_keyInfo != null)
				{
					for (int i = 0; i < _keyInfo.Length; i++)
					{
						if (_keyInfo[i].query != null)
						{
							_keyInfo[i].query.Dispose();
						}
					}
					_keyInfo = null;
				}
			}
			disposed = true;
		}

		~SQLiteKeyReader()
		{
			Dispose(disposing: false);
		}

		internal void Sync(int i)
		{
			Sync();
			if (_keyInfo[i].cursor == -1)
			{
				throw new InvalidCastException();
			}
		}

		internal void Sync()
		{
			if (_isValid)
			{
				return;
			}
			KeyQuery keyQuery = null;
			for (int i = 0; i < _keyInfo.Length; i++)
			{
				if (_keyInfo[i].query == null || _keyInfo[i].query != keyQuery)
				{
					keyQuery = _keyInfo[i].query;
					keyQuery?.Sync(_stmt._sql.GetRowIdForCursor(_stmt, _keyInfo[i].cursor));
				}
			}
			_isValid = true;
		}

		internal void Reset()
		{
			_isValid = false;
			if (_keyInfo == null)
			{
				return;
			}
			for (int i = 0; i < _keyInfo.Length; i++)
			{
				if (_keyInfo[i].query != null)
				{
					_keyInfo[i].query.IsValid = false;
				}
			}
		}

		internal string GetDataTypeName(int i)
		{
			Sync();
			if (_keyInfo[i].query != null)
			{
				return _keyInfo[i].query._reader.GetDataTypeName(_keyInfo[i].column);
			}
			return "integer";
		}

		internal Type GetFieldType(int i)
		{
			Sync();
			if (_keyInfo[i].query != null)
			{
				return _keyInfo[i].query._reader.GetFieldType(_keyInfo[i].column);
			}
			return typeof(long);
		}

		internal string GetName(int i)
		{
			return _keyInfo[i].columnName;
		}

		internal int GetOrdinal(string name)
		{
			for (int i = 0; i < _keyInfo.Length; i++)
			{
				if (string.Compare(name, _keyInfo[i].columnName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		internal bool GetBoolean(int i)
		{
			Sync(i);
			if (_keyInfo[i].query != null)
			{
				return _keyInfo[i].query._reader.GetBoolean(_keyInfo[i].column);
			}
			throw new InvalidCastException();
		}

		internal byte GetByte(int i)
		{
			Sync(i);
			if (_keyInfo[i].query != null)
			{
				return _keyInfo[i].query._reader.GetByte(_keyInfo[i].column);
			}
			throw new InvalidCastException();
		}

		internal long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			Sync(i);
			if (_keyInfo[i].query != null)
			{
				return _keyInfo[i].query._reader.GetBytes(_keyInfo[i].column, fieldOffset, buffer, bufferoffset, length);
			}
			throw new InvalidCastException();
		}

		internal char GetChar(int i)
		{
			Sync(i);
			if (_keyInfo[i].query != null)
			{
				return _keyInfo[i].query._reader.GetChar(_keyInfo[i].column);
			}
			throw new InvalidCastException();
		}

		internal long GetChars(int i, long fieldOffset, char[] buffer, int bufferoffset, int length)
		{
			Sync(i);
			if (_keyInfo[i].query != null)
			{
				return _keyInfo[i].query._reader.GetChars(_keyInfo[i].column, fieldOffset, buffer, bufferoffset, length);
			}
			throw new InvalidCastException();
		}

		internal DateTime GetDateTime(int i)
		{
			Sync(i);
			if (_keyInfo[i].query != null)
			{
				return _keyInfo[i].query._reader.GetDateTime(_keyInfo[i].column);
			}
			throw new InvalidCastException();
		}

		internal decimal GetDecimal(int i)
		{
			Sync(i);
			if (_keyInfo[i].query != null)
			{
				return _keyInfo[i].query._reader.GetDecimal(_keyInfo[i].column);
			}
			throw new InvalidCastException();
		}

		internal double GetDouble(int i)
		{
			Sync(i);
			if (_keyInfo[i].query != null)
			{
				return _keyInfo[i].query._reader.GetDouble(_keyInfo[i].column);
			}
			throw new InvalidCastException();
		}

		internal float GetFloat(int i)
		{
			Sync(i);
			if (_keyInfo[i].query != null)
			{
				return _keyInfo[i].query._reader.GetFloat(_keyInfo[i].column);
			}
			throw new InvalidCastException();
		}

		internal Guid GetGuid(int i)
		{
			Sync(i);
			if (_keyInfo[i].query != null)
			{
				return _keyInfo[i].query._reader.GetGuid(_keyInfo[i].column);
			}
			throw new InvalidCastException();
		}

		internal short GetInt16(int i)
		{
			Sync(i);
			if (_keyInfo[i].query != null)
			{
				return _keyInfo[i].query._reader.GetInt16(_keyInfo[i].column);
			}
			long rowIdForCursor = _stmt._sql.GetRowIdForCursor(_stmt, _keyInfo[i].cursor);
			if (rowIdForCursor == 0)
			{
				throw new InvalidCastException();
			}
			return Convert.ToInt16(rowIdForCursor);
		}

		internal int GetInt32(int i)
		{
			Sync(i);
			if (_keyInfo[i].query != null)
			{
				return _keyInfo[i].query._reader.GetInt32(_keyInfo[i].column);
			}
			long rowIdForCursor = _stmt._sql.GetRowIdForCursor(_stmt, _keyInfo[i].cursor);
			if (rowIdForCursor == 0)
			{
				throw new InvalidCastException();
			}
			return Convert.ToInt32(rowIdForCursor);
		}

		internal long GetInt64(int i)
		{
			Sync(i);
			if (_keyInfo[i].query != null)
			{
				return _keyInfo[i].query._reader.GetInt64(_keyInfo[i].column);
			}
			long rowIdForCursor = _stmt._sql.GetRowIdForCursor(_stmt, _keyInfo[i].cursor);
			if (rowIdForCursor == 0)
			{
				throw new InvalidCastException();
			}
			return Convert.ToInt64(rowIdForCursor);
		}

		internal string GetString(int i)
		{
			Sync(i);
			if (_keyInfo[i].query != null)
			{
				return _keyInfo[i].query._reader.GetString(_keyInfo[i].column);
			}
			throw new InvalidCastException();
		}

		internal object GetValue(int i)
		{
			if (_keyInfo[i].cursor == -1)
			{
				return DBNull.Value;
			}
			Sync(i);
			if (_keyInfo[i].query != null)
			{
				return _keyInfo[i].query._reader.GetValue(_keyInfo[i].column);
			}
			if (IsDBNull(i))
			{
				return DBNull.Value;
			}
			return GetInt64(i);
		}

		internal bool IsDBNull(int i)
		{
			if (_keyInfo[i].cursor == -1)
			{
				return true;
			}
			Sync(i);
			if (_keyInfo[i].query != null)
			{
				return _keyInfo[i].query._reader.IsDBNull(_keyInfo[i].column);
			}
			return _stmt._sql.GetRowIdForCursor(_stmt, _keyInfo[i].cursor) == 0;
		}

		internal void AppendSchemaTable(DataTable tbl)
		{
			KeyQuery keyQuery = null;
			for (int i = 0; i < _keyInfo.Length; i++)
			{
				if (_keyInfo[i].query != null && _keyInfo[i].query == keyQuery)
				{
					continue;
				}
				keyQuery = _keyInfo[i].query;
				if (keyQuery == null)
				{
					DataRow dataRow = tbl.NewRow();
					dataRow[SchemaTableColumn.ColumnName] = _keyInfo[i].columnName;
					dataRow[SchemaTableColumn.ColumnOrdinal] = tbl.Rows.Count;
					dataRow[SchemaTableColumn.ColumnSize] = 8;
					dataRow[SchemaTableColumn.NumericPrecision] = 255;
					dataRow[SchemaTableColumn.NumericScale] = 255;
					dataRow[SchemaTableColumn.ProviderType] = DbType.Int64;
					dataRow[SchemaTableColumn.IsLong] = false;
					dataRow[SchemaTableColumn.AllowDBNull] = false;
					dataRow[SchemaTableOptionalColumn.IsReadOnly] = false;
					dataRow[SchemaTableOptionalColumn.IsRowVersion] = false;
					dataRow[SchemaTableColumn.IsUnique] = false;
					dataRow[SchemaTableColumn.IsKey] = true;
					dataRow[SchemaTableColumn.DataType] = typeof(long);
					dataRow[SchemaTableOptionalColumn.IsHidden] = true;
					dataRow[SchemaTableColumn.BaseColumnName] = _keyInfo[i].columnName;
					dataRow[SchemaTableColumn.IsExpression] = false;
					dataRow[SchemaTableColumn.IsAliased] = false;
					dataRow[SchemaTableColumn.BaseTableName] = _keyInfo[i].tableName;
					dataRow[SchemaTableOptionalColumn.BaseCatalogName] = _keyInfo[i].databaseName;
					dataRow[SchemaTableOptionalColumn.IsAutoIncrement] = true;
					dataRow["DataTypeName"] = "integer";
					tbl.Rows.Add(dataRow);
					continue;
				}
				keyQuery.Sync(0L);
				using DataTable dataTable = keyQuery._reader.GetSchemaTable();
				foreach (DataRow row in dataTable.Rows)
				{
					object[] itemArray = row.ItemArray;
					DataRow dataRow3 = tbl.Rows.Add(itemArray);
					dataRow3[SchemaTableOptionalColumn.IsHidden] = true;
					dataRow3[SchemaTableColumn.ColumnOrdinal] = tbl.Rows.Count - 1;
				}
			}
		}
	}
	public class LogEventArgs : EventArgs
	{
		public readonly object ErrorCode;

		public readonly string Message;

		public readonly object Data;

		internal LogEventArgs(IntPtr pUserData, object errorCode, string message, object data)
		{
			ErrorCode = errorCode;
			Message = message;
			Data = data;
		}
	}
	public delegate void SQLiteLogEventHandler(object sender, LogEventArgs e);
	public static class SQLiteLog
	{
		private static object syncRoot = new object();

		private static EventHandler _domainUnload;

		private static SQLiteLogEventHandler _defaultHandler;

		private static SQLiteLogCallback _callback;

		private static SQLiteBase _sql;

		private static bool _enabled;

		public static bool Enabled
		{
			get
			{
				lock (syncRoot)
				{
					return _enabled;
				}
			}
			set
			{
				lock (syncRoot)
				{
					_enabled = value;
				}
			}
		}

		private static event SQLiteLogEventHandler _handlers;

		public static event SQLiteLogEventHandler Log
		{
			add
			{
				lock (syncRoot)
				{
					SQLiteLog._handlers = (SQLiteLogEventHandler)Delegate.Remove(SQLiteLog._handlers, value);
					SQLiteLog._handlers = (SQLiteLogEventHandler)Delegate.Combine(SQLiteLog._handlers, value);
				}
			}
			remove
			{
				lock (syncRoot)
				{
					SQLiteLog._handlers = (SQLiteLogEventHandler)Delegate.Remove(SQLiteLog._handlers, value);
				}
			}
		}

		public static void Initialize()
		{
			if (SQLite3.StaticIsInitialized() || (!AppDomain.CurrentDomain.IsDefaultAppDomain() && UnsafeNativeMethods.GetSettingValue("Force_SQLiteLog", null) == null))
			{
				return;
			}
			lock (syncRoot)
			{
				if (_domainUnload == null)
				{
					_domainUnload = DomainUnload;
					AppDomain.CurrentDomain.DomainUnload += _domainUnload;
				}
				if (_sql == null)
				{
					_sql = new SQLite3(SQLiteDateFormats.ISO8601, DateTimeKind.Unspecified, null, IntPtr.Zero, null, ownHandle: false);
				}
				if (_callback == null)
				{
					_callback = LogCallback;
					SQLiteErrorCode sQLiteErrorCode = _sql.SetLogCallback(_callback);
					if (sQLiteErrorCode != SQLiteErrorCode.Ok)
					{
						throw new SQLiteException(sQLiteErrorCode, "Failed to initialize logging.");
					}
				}
				_enabled = true;
				AddDefaultHandler();
			}
		}

		private static void DomainUnload(object sender, EventArgs e)
		{
			lock (syncRoot)
			{
				RemoveDefaultHandler();
				_enabled = false;
				if (_sql != null)
				{
					SQLiteErrorCode sQLiteErrorCode = _sql.Shutdown();
					if (sQLiteErrorCode != SQLiteErrorCode.Ok)
					{
						throw new SQLiteException(sQLiteErrorCode, "Failed to shutdown interface.");
					}
					sQLiteErrorCode = _sql.SetLogCallback(null);
					if (sQLiteErrorCode != SQLiteErrorCode.Ok)
					{
						throw new SQLiteException(sQLiteErrorCode, "Failed to shutdown logging.");
					}
				}
				if (_callback != null)
				{
					_callback = null;
				}
				if (_domainUnload != null)
				{
					AppDomain.CurrentDomain.DomainUnload -= _domainUnload;
					_domainUnload = null;
				}
			}
		}

		public static void LogMessage(string message)
		{
			LogMessage(null, message);
		}

		public static void LogMessage(SQLiteErrorCode errorCode, string message)
		{
			LogMessage((object)errorCode, message);
		}

		public static void LogMessage(int errorCode, string message)
		{
			LogMessage((object)errorCode, message);
		}

		private static void LogMessage(object errorCode, string message)
		{
			bool enabled;
			SQLiteLogEventHandler sQLiteLogEventHandler;
			lock (syncRoot)
			{
				enabled = _enabled;
				sQLiteLogEventHandler = ((SQLiteLog._handlers == null) ? null : (SQLiteLog._handlers.Clone() as SQLiteLogEventHandler));
			}
			if (enabled)
			{
				sQLiteLogEventHandler?.Invoke(null, new LogEventArgs(IntPtr.Zero, errorCode, message, null));
			}
		}

		private static void InitializeDefaultHandler()
		{
			lock (syncRoot)
			{
				if (_defaultHandler == null)
				{
					_defaultHandler = LogEventHandler;
				}
			}
		}

		public static void AddDefaultHandler()
		{
			InitializeDefaultHandler();
			Log += _defaultHandler;
		}

		public static void RemoveDefaultHandler()
		{
			InitializeDefaultHandler();
			Log -= _defaultHandler;
		}

		private static void LogCallback(IntPtr pUserData, int errorCode, IntPtr pMessage)
		{
			bool enabled;
			SQLiteLogEventHandler sQLiteLogEventHandler;
			lock (syncRoot)
			{
				enabled = _enabled;
				sQLiteLogEventHandler = ((SQLiteLog._handlers == null) ? null : (SQLiteLog._handlers.Clone() as SQLiteLogEventHandler));
			}
			if (enabled)
			{
				sQLiteLogEventHandler?.Invoke(null, new LogEventArgs(pUserData, errorCode, SQLiteConvert.UTF8ToString(pMessage, -1), null));
			}
		}

		private static void LogEventHandler(object sender, LogEventArgs e)
		{
			if (e == null)
			{
				return;
			}
			string message = e.Message;
			if (message == null)
			{
				message = "<null>";
			}
			else
			{
				message = message.Trim();
				if (message.Length == 0)
				{
					message = "<empty>";
				}
			}
			object errorCode = e.ErrorCode;
			string text = "error";
			if (errorCode is SQLiteErrorCode || errorCode is int)
			{
				SQLiteErrorCode sQLiteErrorCode = (SQLiteErrorCode)(int)errorCode;
				switch (sQLiteErrorCode & SQLiteErrorCode.NonExtendedMask)
				{
				case SQLiteErrorCode.Ok:
					text = "message";
					break;
				case SQLiteErrorCode.Notice:
					text = "notice";
					break;
				case SQLiteErrorCode.Warning:
					text = "warning";
					break;
				case SQLiteErrorCode.Row:
				case SQLiteErrorCode.Done:
					text = "data";
					break;
				}
			}
			else if (errorCode == null)
			{
				text = "trace";
			}
			if (errorCode != null && !object.ReferenceEquals(errorCode, string.Empty))
			{
				Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "SQLite {0} ({1}): {2}", new object[3] { text, errorCode, message }));
			}
			else
			{
				Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "SQLite {0}: {1}", new object[2] { text, message }));
			}
		}
	}
	public static class SQLiteMetaDataCollectionNames
	{
		public static readonly string Catalogs = "Catalogs";

		public static readonly string Columns = "Columns";

		public static readonly string Indexes = "Indexes";

		public static readonly string IndexColumns = "IndexColumns";

		public static readonly string Tables = "Tables";

		public static readonly string Views = "Views";

		public static readonly string ViewColumns = "ViewColumns";

		public static readonly string ForeignKeys = "ForeignKeys";

		public static readonly string Triggers = "Triggers";
	}
	public sealed class SQLiteParameter : DbParameter, ICloneable
	{
		private const DbType UnknownDbType = (DbType)(-1);

		private IDbCommand _command;

		internal DbType _dbType;

		private DataRowVersion _rowVersion;

		private object _objValue;

		private string _sourceColumn;

		private string _parameterName;

		private int _dataSize;

		private bool _nullable;

		private bool _nullMapping;

		public IDbCommand Command
		{
			get
			{
				return _command;
			}
			set
			{
				_command = value;
			}
		}

		public override bool IsNullable
		{
			get
			{
				return _nullable;
			}
			set
			{
				_nullable = value;
			}
		}

		[RefreshProperties(RefreshProperties.All)]
		[DbProviderSpecificTypeProperty(true)]
		public override DbType DbType
		{
			get
			{
				if (_dbType == (DbType)(-1))
				{
					if (_objValue != null && _objValue != DBNull.Value)
					{
						return SQLiteConvert.TypeToDbType(_objValue.GetType());
					}
					return DbType.String;
				}
				return _dbType;
			}
			set
			{
				_dbType = value;
			}
		}

		public override ParameterDirection Direction
		{
			get
			{
				return ParameterDirection.Input;
			}
			set
			{
				if (value != ParameterDirection.Input)
				{
					throw new NotSupportedException();
				}
			}
		}

		public override string ParameterName
		{
			get
			{
				return _parameterName;
			}
			set
			{
				_parameterName = value;
			}
		}

		[DefaultValue(0)]
		public override int Size
		{
			get
			{
				return _dataSize;
			}
			set
			{
				_dataSize = value;
			}
		}

		public override string SourceColumn
		{
			get
			{
				return _sourceColumn;
			}
			set
			{
				_sourceColumn = value;
			}
		}

		public override bool SourceColumnNullMapping
		{
			get
			{
				return _nullMapping;
			}
			set
			{
				_nullMapping = value;
			}
		}

		public override DataRowVersion SourceVersion
		{
			get
			{
				return _rowVersion;
			}
			set
			{
				_rowVersion = value;
			}
		}

		[TypeConverter(typeof(StringConverter))]
		[RefreshProperties(RefreshProperties.All)]
		public override object Value
		{
			get
			{
				return _objValue;
			}
			set
			{
				_objValue = value;
				if (_dbType == (DbType)(-1) && _objValue != null && _objValue != DBNull.Value)
				{
					_dbType = SQLiteConvert.TypeToDbType(_objValue.GetType());
				}
			}
		}

		internal SQLiteParameter(IDbCommand command)
			: this()
		{
			_command = command;
		}

		public SQLiteParameter()
			: this(null, (DbType)(-1), 0, null, DataRowVersion.Current)
		{
		}

		public SQLiteParameter(string parameterName)
			: this(parameterName, (DbType)(-1), 0, null, DataRowVersion.Current)
		{
		}

		public SQLiteParameter(string parameterName, object value)
			: this(parameterName, (DbType)(-1), 0, null, DataRowVersion.Current)
		{
			Value = value;
		}

		public SQLiteParameter(string parameterName, DbType dbType)
			: this(parameterName, dbType, 0, null, DataRowVersion.Current)
		{
		}

		public SQLiteParameter(string parameterName, DbType dbType, string sourceColumn)
			: this(parameterName, dbType, 0, sourceColumn, DataRowVersion.Current)
		{
		}

		public SQLiteParameter(string parameterName, DbType dbType, string sourceColumn, DataRowVersion rowVersion)
			: this(parameterName, dbType, 0, sourceColumn, rowVersion)
		{
		}

		public SQLiteParameter(DbType dbType)
			: this(null, dbType, 0, null, DataRowVersion.Current)
		{
		}

		public SQLiteParameter(DbType dbType, object value)
			: this(null, dbType, 0, null, DataRowVersion.Current)
		{
			Value = value;
		}

		public SQLiteParameter(DbType dbType, string sourceColumn)
			: this(null, dbType, 0, sourceColumn, DataRowVersion.Current)
		{
		}

		public SQLiteParameter(DbType dbType, string sourceColumn, DataRowVersion rowVersion)
			: this(null, dbType, 0, sourceColumn, rowVersion)
		{
		}

		public SQLiteParameter(string parameterName, DbType parameterType, int parameterSize)
			: this(parameterName, parameterType, parameterSize, null, DataRowVersion.Current)
		{
		}

		public SQLiteParameter(string parameterName, DbType parameterType, int parameterSize, string sourceColumn)
			: this(parameterName, parameterType, parameterSize, sourceColumn, DataRowVersion.Current)
		{
		}

		public SQLiteParameter(string parameterName, DbType parameterType, int parameterSize, string sourceColumn, DataRowVersion rowVersion)
		{
			_parameterName = parameterName;
			_dbType = parameterType;
			_sourceColumn = sourceColumn;
			_rowVersion = rowVersion;
			_dataSize = parameterSize;
			_nullable = true;
		}

		private SQLiteParameter(SQLiteParameter source)
			: this(source.ParameterName, source._dbType, 0, source.Direction, source.IsNullable, 0, 0, source.SourceColumn, source.SourceVersion, source.Value)
		{
			_nullMapping = source._nullMapping;
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SQLiteParameter(string parameterName, DbType parameterType, int parameterSize, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion rowVersion, object value)
			: this(parameterName, parameterType, parameterSize, sourceColumn, rowVersion)
		{
			Direction = direction;
			IsNullable = isNullable;
			Value = value;
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SQLiteParameter(string parameterName, DbType parameterType, int parameterSize, ParameterDirection direction, byte precision, byte scale, string sourceColumn, DataRowVersion rowVersion, bool sourceColumnNullMapping, object value)
			: this(parameterName, parameterType, parameterSize, sourceColumn, rowVersion)
		{
			Direction = direction;
			SourceColumnNullMapping = sourceColumnNullMapping;
			Value = value;
		}

		public SQLiteParameter(DbType parameterType, int parameterSize)
			: this(null, parameterType, parameterSize, null, DataRowVersion.Current)
		{
		}

		public SQLiteParameter(DbType parameterType, int parameterSize, string sourceColumn)
			: this(null, parameterType, parameterSize, sourceColumn, DataRowVersion.Current)
		{
		}

		public SQLiteParameter(DbType parameterType, int parameterSize, string sourceColumn, DataRowVersion rowVersion)
			: this(null, parameterType, parameterSize, sourceColumn, rowVersion)
		{
		}

		public override void ResetDbType()
		{
			_dbType = (DbType)(-1);
		}

		public object Clone()
		{
			return new SQLiteParameter(this);
		}
	}
	[Editor("Microsoft.VSDesigner.Data.Design.DBParametersEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ListBindable(false)]
	public sealed class SQLiteParameterCollection : DbParameterCollection
	{
		private SQLiteCommand _command;

		private List<SQLiteParameter> _parameterList;

		private bool _unboundFlag;

		public override bool IsSynchronized => false;

		public override bool IsFixedSize => false;

		public override bool IsReadOnly => false;

		public override object SyncRoot => null;

		public override int Count => _parameterList.Count;

		public new SQLiteParameter this[string parameterName]
		{
			get
			{
				return (SQLiteParameter)GetParameter(parameterName);
			}
			set
			{
				SetParameter(parameterName, value);
			}
		}

		public new SQLiteParameter this[int index]
		{
			get
			{
				return (SQLiteParameter)GetParameter(index);
			}
			set
			{
				SetParameter(index, value);
			}
		}

		internal SQLiteParameterCollection(SQLiteCommand cmd)
		{
			_command = cmd;
			_parameterList = new List<SQLiteParameter>();
			_unboundFlag = true;
		}

		public override IEnumerator GetEnumerator()
		{
			return _parameterList.GetEnumerator();
		}

		public SQLiteParameter Add(string parameterName, DbType parameterType, int parameterSize, string sourceColumn)
		{
			SQLiteParameter sQLiteParameter = new SQLiteParameter(parameterName, parameterType, parameterSize, sourceColumn);
			Add(sQLiteParameter);
			return sQLiteParameter;
		}

		public SQLiteParameter Add(string parameterName, DbType parameterType, int parameterSize)
		{
			SQLiteParameter sQLiteParameter = new SQLiteParameter(parameterName, parameterType, parameterSize);
			Add(sQLiteParameter);
			return sQLiteParameter;
		}

		public SQLiteParameter Add(string parameterName, DbType parameterType)
		{
			SQLiteParameter sQLiteParameter = new SQLiteParameter(parameterName, parameterType);
			Add(sQLiteParameter);
			return sQLiteParameter;
		}

		public int Add(SQLiteParameter parameter)
		{
			int num = -1;
			if (!string.IsNullOrEmpty(parameter.ParameterName))
			{
				num = IndexOf(parameter.ParameterName);
			}
			if (num == -1)
			{
				num = _parameterList.Count;
				_parameterList.Add(parameter);
			}
			SetParameter(num, parameter);
			return num;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int Add(object value)
		{
			return Add((SQLiteParameter)value);
		}

		public SQLiteParameter AddWithValue(string parameterName, object value)
		{
			SQLiteParameter sQLiteParameter = new SQLiteParameter(parameterName, value);
			Add(sQLiteParameter);
			return sQLiteParameter;
		}

		public void AddRange(SQLiteParameter[] values)
		{
			int num = values.Length;
			for (int i = 0; i < num; i++)
			{
				Add(values[i]);
			}
		}

		public override void AddRange(Array values)
		{
			int length = values.Length;
			for (int i = 0; i < length; i++)
			{
				Add((SQLiteParameter)values.GetValue(i));
			}
		}

		public override void Clear()
		{
			_unboundFlag = true;
			_parameterList.Clear();
		}

		public override bool Contains(string parameterName)
		{
			return IndexOf(parameterName) != -1;
		}

		public override bool Contains(object value)
		{
			return _parameterList.Contains((SQLiteParameter)value);
		}

		public override void CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		protected override DbParameter GetParameter(string parameterName)
		{
			return GetParameter(IndexOf(parameterName));
		}

		protected override DbParameter GetParameter(int index)
		{
			return _parameterList[index];
		}

		public override int IndexOf(string parameterName)
		{
			int count = _parameterList.Count;
			for (int i = 0; i < count; i++)
			{
				if (string.Compare(parameterName, _parameterList[i].ParameterName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		public override int IndexOf(object value)
		{
			return _parameterList.IndexOf((SQLiteParameter)value);
		}

		public override void Insert(int index, object value)
		{
			_unboundFlag = true;
			_parameterList.Insert(index, (SQLiteParameter)value);
		}

		public override void Remove(object value)
		{
			_unboundFlag = true;
			_parameterList.Remove((SQLiteParameter)value);
		}

		public override void RemoveAt(string parameterName)
		{
			RemoveAt(IndexOf(parameterName));
		}

		public override void RemoveAt(int index)
		{
			_unboundFlag = true;
			_parameterList.RemoveAt(index);
		}

		protected override void SetParameter(string parameterName, DbParameter value)
		{
			SetParameter(IndexOf(parameterName), value);
		}

		protected override void SetParameter(int index, DbParameter value)
		{
			_unboundFlag = true;
			_parameterList[index] = (SQLiteParameter)value;
		}

		internal void Unbind()
		{
			_unboundFlag = true;
		}

		internal void MapParameters(SQLiteStatement activeStatement)
		{
			if (!_unboundFlag || _parameterList.Count == 0 || _command._statementList == null)
			{
				return;
			}
			int num = 0;
			int num2 = -1;
			foreach (SQLiteParameter parameter in _parameterList)
			{
				num2++;
				string text = parameter.ParameterName;
				if (text == null)
				{
					text = string.Format(CultureInfo.InvariantCulture, ";{0}", new object[1] { num });
					num++;
				}
				bool flag = false;
				int num3 = ((activeStatement != null) ? 1 : _command._statementList.Count);
				SQLiteStatement sQLiteStatement = activeStatement;
				for (int i = 0; i < num3; i++)
				{
					flag = false;
					if (sQLiteStatement == null)
					{
						sQLiteStatement = _command._statementList[i];
					}
					if (sQLiteStatement._paramNames != null && sQLiteStatement.MapParameter(text, parameter))
					{
						flag = true;
					}
					sQLiteStatement = null;
				}
				if (flag)
				{
					continue;
				}
				text = string.Format(CultureInfo.InvariantCulture, ";{0}", new object[1] { num2 });
				sQLiteStatement = activeStatement;
				for (int i = 0; i < num3; i++)
				{
					if (sQLiteStatement == null)
					{
						sQLiteStatement = _command._statementList[i];
					}
					if (sQLiteStatement._paramNames != null && sQLiteStatement.MapParameter(text, parameter))
					{
						flag = true;
					}
					sQLiteStatement = null;
				}
			}
			if (activeStatement == null)
			{
				_unboundFlag = false;
			}
		}
	}
	internal sealed class SQLiteStatement : IDisposable
	{
		internal SQLiteBase _sql;

		internal string _sqlStatement;

		internal SQLiteStatementHandle _sqlite_stmt;

		internal int _unnamedParameters;

		internal string[] _paramNames;

		internal SQLiteParameter[] _paramValues;

		internal SQLiteCommand _command;

		private SQLiteConnectionFlags _flags;

		private string[] _types;

		private bool disposed;

		internal string[] TypeDefinitions => _types;

		internal SQLiteStatement(SQLiteBase sqlbase, SQLiteConnectionFlags flags, SQLiteStatementHandle stmt, string strCommand, SQLiteStatement previous)
		{
			_sql = sqlbase;
			_sqlite_stmt = stmt;
			_sqlStatement = strCommand;
			_flags = flags;
			int num = 0;
			int num2 = _sql.Bind_ParamCount(this, _flags);
			if (num2 <= 0)
			{
				return;
			}
			if (previous != null)
			{
				num = previous._unnamedParameters;
			}
			_paramNames = new string[num2];
			_paramValues = new SQLiteParameter[num2];
			for (int i = 0; i < num2; i++)
			{
				string text = _sql.Bind_ParamName(this, _flags, i + 1);
				if (string.IsNullOrEmpty(text))
				{
					text = string.Format(CultureInfo.InvariantCulture, ";{0}", new object[1] { num });
					num++;
					_unnamedParameters++;
				}
				_paramNames[i] = text;
				_paramValues[i] = null;
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		private void CheckDisposed()
		{
			if (disposed)
			{
				throw new ObjectDisposedException(typeof(SQLiteStatement).Name);
			}
		}

		private void Dispose(bool disposing)
		{
			if (disposed)
			{
				return;
			}
			if (disposing)
			{
				if (_sqlite_stmt != null)
				{
					_sqlite_stmt.Dispose();
					_sqlite_stmt = null;
				}
				_paramNames = null;
				_paramValues = null;
				_sql = null;
				_sqlStatement = null;
			}
			disposed = true;
		}

		~SQLiteStatement()
		{
			Dispose(disposing: false);
		}

		internal bool TryGetChanges(ref int changes, ref bool readOnly)
		{
			if (_sql != null && _sql.IsOpen())
			{
				changes = _sql.Changes;
				readOnly = _sql.IsReadOnly(this);
				return true;
			}
			return false;
		}

		internal bool MapParameter(string s, SQLiteParameter p)
		{
			if (_paramNames == null)
			{
				return false;
			}
			int num = 0;
			if (s.Length > 0 && ":$@;".IndexOf(s[0]) == -1)
			{
				num = 1;
			}
			int num2 = _paramNames.Length;
			for (int i = 0; i < num2; i++)
			{
				if (string.Compare(_paramNames[i], num, s, 0, Math.Max(_paramNames[i].Length - num, s.Length), StringComparison.OrdinalIgnoreCase) == 0)
				{
					_paramValues[i] = p;
					return true;
				}
			}
			return false;
		}

		internal void BindParameters()
		{
			if (_paramNames != null)
			{
				int num = _paramNames.Length;
				for (int i = 0; i < num; i++)
				{
					BindParameter(i + 1, _paramValues[i]);
				}
			}
		}

		private void BindParameter(int index, SQLiteParameter param)
		{
			if (param == null)
			{
				throw new SQLiteException("Insufficient parameters supplied to the command");
			}
			object value = param.Value;
			DbType dbType = param.DbType;
			if (value != null && dbType == DbType.Object)
			{
				dbType = SQLiteConvert.TypeToDbType(value.GetType());
			}
			if ((_flags & SQLiteConnectionFlags.LogPreBind) == SQLiteConnectionFlags.LogPreBind)
			{
				IntPtr intPtr = _sqlite_stmt;
				SQLiteLog.LogMessage($"Binding statement {intPtr} paramter #{index} with database type {dbType} and raw value {{{value}}}...");
			}
			if (value == null || Convert.IsDBNull(value))
			{
				_sql.Bind_Null(this, _flags, index);
				return;
			}
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			bool flag = (_flags & SQLiteConnectionFlags.BindInvariantText) == SQLiteConnectionFlags.BindInvariantText;
			if ((_flags & SQLiteConnectionFlags.BindAllAsText) == SQLiteConnectionFlags.BindAllAsText)
			{
				if (value is DateTime)
				{
					_sql.Bind_DateTime(this, _flags, index, (DateTime)value);
				}
				else
				{
					_sql.Bind_Text(this, _flags, index, flag ? SQLiteConvert.ToStringWithProvider(value, invariantCulture) : value.ToString());
				}
				return;
			}
			CultureInfo provider = CultureInfo.CurrentCulture;
			if ((_flags & SQLiteConnectionFlags.ConvertInvariantText) == SQLiteConnectionFlags.ConvertInvariantText)
			{
				provider = invariantCulture;
			}
			switch (dbType)
			{
			case DbType.Date:
			case DbType.DateTime:
			case DbType.Time:
				_sql.Bind_DateTime(this, _flags, index, (value is string) ? _sql.ToDateTime((string)value) : Convert.ToDateTime(value, provider));
				break;
			case DbType.Boolean:
				_sql.Bind_Int32(this, _flags, index, SQLiteConvert.ToBoolean(value, provider, viaFramework: true) ? 1 : 0);
				break;
			case DbType.SByte:
				_sql.Bind_Int32(this, _flags, index, Convert.ToSByte(value, provider));
				break;
			case DbType.Int16:
				_sql.Bind_Int32(this, _flags, index, Convert.ToInt16(value, provider));
				break;
			case DbType.Int32:
				_sql.Bind_Int32(this, _flags, index, Convert.ToInt32(value, provider));
				break;
			case DbType.Int64:
				_sql.Bind_Int64(this, _flags, index, Convert.ToInt64(value, provider));
				break;
			case DbType.Byte:
				_sql.Bind_UInt32(this, _flags, index, Convert.ToByte(value, provider));
				break;
			case DbType.UInt16:
				_sql.Bind_UInt32(this, _flags, index, Convert.ToUInt16(value, provider));
				break;
			case DbType.UInt32:
				_sql.Bind_UInt32(this, _flags, index, Convert.ToUInt32(value, provider));
				break;
			case DbType.UInt64:
				_sql.Bind_UInt64(this, _flags, index, Convert.ToUInt64(value, provider));
				break;
			case DbType.Currency:
			case DbType.Double:
			case DbType.Single:
				_sql.Bind_Double(this, _flags, index, Convert.ToDouble(value, provider));
				break;
			case DbType.Binary:
				_sql.Bind_Blob(this, _flags, index, (byte[])value);
				break;
			case DbType.Guid:
				if (_command.Connection._binaryGuid)
				{
					_sql.Bind_Blob(this, _flags, index, ((Guid)value).ToByteArray());
				}
				else
				{
					_sql.Bind_Text(this, _flags, index, flag ? SQLiteConvert.ToStringWithProvider(value, invariantCulture) : value.ToString());
				}
				break;
			case DbType.Decimal:
				_sql.Bind_Text(this, _flags, index, Convert.ToDecimal(value, provider).ToString(invariantCulture));
				break;
			default:
				_sql.Bind_Text(this, _flags, index, flag ? SQLiteConvert.ToStringWithProvider(value, invariantCulture) : value.ToString());
				break;
			}
		}

		internal void SetTypes(string typedefs)
		{
			int num = typedefs.IndexOf("TYPES", 0, StringComparison.OrdinalIgnoreCase);
			if (num == -1)
			{
				throw new ArgumentOutOfRangeException();
			}
			string[] array = typedefs.Substring(num + 6).Replace(" ", "").Replace(";", "")
				.Replace("\"", "")
				.Replace("[", "")
				.Replace("]", "")
				.Replace("`", "")
				.Split(',', '\r', '\n', '\t');
			for (int i = 0; i < array.Length; i++)
			{
				if (string.IsNullOrEmpty(array[i]))
				{
					array[i] = null;
				}
			}
			_types = array;
		}
	}
	public sealed class SQLiteTransaction : DbTransaction
	{
		internal SQLiteConnection _cnn;

		internal int _version;

		private IsolationLevel _level;

		private bool disposed;

		public new SQLiteConnection Connection
		{
			get
			{
				CheckDisposed();
				return _cnn;
			}
		}

		protected override DbConnection DbConnection => Connection;

		public override IsolationLevel IsolationLevel
		{
			get
			{
				CheckDisposed();
				return _level;
			}
		}

		internal SQLiteTransaction(SQLiteConnection connection, bool deferredLock)
		{
			_cnn = connection;
			_version = _cnn._version;
			_level = (deferredLock ? IsolationLevel.ReadCommitted : IsolationLevel.Serializable);
			if (_cnn._transactionLevel++ != 0)
			{
				return;
			}
			try
			{
				using SQLiteCommand sQLiteCommand = _cnn.CreateCommand();
				if (!deferredLock)
				{
					sQLiteCommand.CommandText = "BEGIN IMMEDIATE";
				}
				else
				{
					sQLiteCommand.CommandText = "BEGIN";
				}
				sQLiteCommand.ExecuteNonQuery();
			}
			catch (SQLiteException)
			{
				_cnn._transactionLevel--;
				_cnn = null;
				throw;
			}
		}

		private void CheckDisposed()
		{
			if (disposed)
			{
				throw new ObjectDisposedException(typeof(SQLiteTransaction).Name);
			}
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!disposed && disposing && IsValid(throwError: false))
				{
					IssueRollback(throwError: false);
				}
			}
			finally
			{
				base.Dispose(disposing);
				disposed = true;
			}
		}

		public override void Commit()
		{
			CheckDisposed();
			IsValid(throwError: true);
			if (_cnn._transactionLevel - 1 == 0)
			{
				using SQLiteCommand sQLiteCommand = _cnn.CreateCommand();
				sQLiteCommand.CommandText = "COMMIT";
				sQLiteCommand.ExecuteNonQuery();
			}
			_cnn._transactionLevel--;
			_cnn = null;
		}

		public override void Rollback()
		{
			CheckDisposed();
			IsValid(throwError: true);
			IssueRollback(throwError: true);
		}

		internal void IssueRollback(bool throwError)
		{
			SQLiteConnection sQLiteConnection = Interlocked.Exchange(ref _cnn, null);
			if (sQLiteConnection == null)
			{
				return;
			}
			try
			{
				using SQLiteCommand sQLiteCommand = sQLiteConnection.CreateCommand();
				sQLiteCommand.CommandText = "ROLLBACK";
				sQLiteCommand.ExecuteNonQuery();
			}
			catch
			{
				if (throwError)
				{
					throw;
				}
			}
			sQLiteConnection._transactionLevel = 0;
		}

		internal bool IsValid(bool throwError)
		{
			if (_cnn == null)
			{
				if (throwError)
				{
					throw new ArgumentNullException("No connection associated with this transaction");
				}
				return false;
			}
			if (_cnn._version != _version)
			{
				if (throwError)
				{
					throw new SQLiteException("The connection was closed and re-opened, changes were already rolled back");
				}
				return false;
			}
			if (_cnn.State != ConnectionState.Open)
			{
				if (throwError)
				{
					throw new SQLiteException("Connection was closed");
				}
				return false;
			}
			if (_cnn._transactionLevel == 0 || _cnn._sql.AutoCommit)
			{
				_cnn._transactionLevel = 0;
				if (throwError)
				{
					throw new SQLiteException("No transaction is active on this connection");
				}
				return false;
			}
			return true;
		}
	}
	[SuppressUnmanagedCodeSecurity]
	internal static class UnsafeNativeMethods
	{
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SQLiteErrorCode xCreate(IntPtr pDb, IntPtr pAux, int argc, IntPtr argv, ref IntPtr pVtab, ref IntPtr pError);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SQLiteErrorCode xConnect(IntPtr pDb, IntPtr pAux, int argc, IntPtr argv, ref IntPtr pVtab, ref IntPtr pError);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SQLiteErrorCode xBestIndex(IntPtr pVtab, IntPtr pIndex);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SQLiteErrorCode xDisconnect(IntPtr pVtab);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SQLiteErrorCode xDestroy(IntPtr pVtab);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SQLiteErrorCode xOpen(IntPtr pVtab, ref IntPtr pCursor);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SQLiteErrorCode xClose(IntPtr pCursor);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SQLiteErrorCode xFilter(IntPtr pCursor, int idxNum, IntPtr idxStr, int argc, IntPtr argv);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SQLiteErrorCode xNext(IntPtr pCursor);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int xEof(IntPtr pCursor);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SQLiteErrorCode xColumn(IntPtr pCursor, IntPtr pContext, int index);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SQLiteErrorCode xRowId(IntPtr pCursor, ref long rowId);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SQLiteErrorCode xUpdate(IntPtr pVtab, int argc, IntPtr argv, ref long rowId);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SQLiteErrorCode xBegin(IntPtr pVtab);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SQLiteErrorCode xSync(IntPtr pVtab);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SQLiteErrorCode xCommit(IntPtr pVtab);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SQLiteErrorCode xRollback(IntPtr pVtab);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int xFindFunction(IntPtr pVtab, int nArg, IntPtr zName, ref SQLiteCallback callback, ref IntPtr pUserData);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SQLiteErrorCode xRename(IntPtr pVtab, IntPtr zNew);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SQLiteErrorCode xSavepoint(IntPtr pVtab, int iSavepoint);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SQLiteErrorCode xRelease(IntPtr pVtab, int iSavepoint);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate SQLiteErrorCode xRollbackTo(IntPtr pVtab, int iSavepoint);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void xDestroyModule(IntPtr pClientData);

		internal struct sqlite3_module
		{
			public int iVersion;

			public xCreate xCreate;

			public xConnect xConnect;

			public xBestIndex xBestIndex;

			public xDisconnect xDisconnect;

			public xDestroy xDestroy;

			public xOpen xOpen;

			public xClose xClose;

			public xFilter xFilter;

			public xNext xNext;

			public xEof xEof;

			public xColumn xColumn;

			public xRowId xRowId;

			public xUpdate xUpdate;

			public xBegin xBegin;

			public xSync xSync;

			public xCommit xCommit;

			public xRollback xRollback;

			public xFindFunction xFindFunction;

			public xRename xRename;

			public xSavepoint xSavepoint;

			public xRelease xRelease;

			public xRollbackTo xRollbackTo;
		}

		internal struct sqlite3_vtab
		{
			public IntPtr pModule;

			public int nRef;

			public IntPtr zErrMsg;
		}

		internal struct sqlite3_vtab_cursor
		{
			public IntPtr pVTab;
		}

		internal struct sqlite3_index_constraint
		{
			public int iColumn;

			public SQLiteIndexConstraintOp op;

			public byte usable;

			public int iTermOffset;

			public sqlite3_index_constraint(SQLiteIndexConstraint constraint)
			{
				this = default(sqlite3_index_constraint);
				if (constraint != null)
				{
					iColumn = constraint.iColumn;
					op = constraint.op;
					usable = constraint.usable;
					iTermOffset = constraint.iTermOffset;
				}
			}
		}

		internal struct sqlite3_index_orderby
		{
			public int iColumn;

			public byte desc;

			public sqlite3_index_orderby(SQLiteIndexOrderBy orderBy)
			{
				this = default(sqlite3_index_orderby);
				if (orderBy != null)
				{
					iColumn = orderBy.iColumn;
					desc = orderBy.desc;
				}
			}
		}

		internal struct sqlite3_index_constraint_usage
		{
			public int argvIndex;

			public byte omit;

			public sqlite3_index_constraint_usage(SQLiteIndexConstraintUsage constraintUsage)
			{
				this = default(sqlite3_index_constraint_usage);
				if (constraintUsage != null)
				{
					argvIndex = constraintUsage.argvIndex;
					omit = constraintUsage.omit;
				}
			}
		}

		internal struct sqlite3_index_info
		{
			public int nConstraint;

			public IntPtr aConstraint;

			public int nOrderBy;

			public IntPtr aOrderBy;

			public IntPtr aConstraintUsage;

			public int idxNum;

			public string idxStr;

			public int needToFreeIdxStr;

			public int orderByConsumed;

			public double estimatedCost;
		}

		internal const string SQLITE_DLL = "sqlite3";

		private static readonly string DllFileExtension;

		private static readonly string ConfigFileExtension;

		private static readonly string XmlConfigFileName;

		private static readonly object staticSyncRoot;

		private static Dictionary<string, string> processorArchitecturePlatforms;

		private static readonly string PROCESSOR_ARCHITECTURE;

		private static string _SQLiteNativeModuleFileName;

		private static IntPtr _SQLiteNativeModuleHandle;

		static UnsafeNativeMethods()
		{
			DllFileExtension = ".dll";
			ConfigFileExtension = ".config";
			XmlConfigFileName = typeof(UnsafeNativeMethods).Namespace + DllFileExtension + ConfigFileExtension;
			staticSyncRoot = new object();
			PROCESSOR_ARCHITECTURE = "PROCESSOR_ARCHITECTURE";
			_SQLiteNativeModuleFileName = null;
			_SQLiteNativeModuleHandle = IntPtr.Zero;
			Initialize();
		}

		internal static void Initialize()
		{
			if (GetSettingValue("No_PreLoadSQLite", null) != null)
			{
				return;
			}
			lock (staticSyncRoot)
			{
				if (processorArchitecturePlatforms == null)
				{
					processorArchitecturePlatforms = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
					processorArchitecturePlatforms.Add("x86", "Win32");
					processorArchitecturePlatforms.Add("AMD64", "x64");
					processorArchitecturePlatforms.Add("IA64", "Itanium");
					processorArchitecturePlatforms.Add("ARM", "WinCE");
				}
				if (_SQLiteNativeModuleHandle == IntPtr.Zero)
				{
					string baseDirectory = null;
					string processorArchitecture = null;
					SearchForDirectory(ref baseDirectory, ref processorArchitecture);
					PreLoadSQLiteDll(baseDirectory, processorArchitecture, ref _SQLiteNativeModuleFileName, ref _SQLiteNativeModuleHandle);
				}
			}
		}

		private static string GetXmlConfigFileName()
		{
			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			string text = Path.Combine(baseDirectory, XmlConfigFileName);
			if (File.Exists(text))
			{
				return text;
			}
			baseDirectory = GetAssemblyDirectory();
			text = Path.Combine(baseDirectory, XmlConfigFileName);
			if (File.Exists(text))
			{
				return text;
			}
			return null;
		}

		internal static string GetSettingValue(string name, string @default)
		{
			if (name == null)
			{
				return @default;
			}
			string text = null;
			bool flag = true;
			if (Environment.GetEnvironmentVariable("No_Expand") != null)
			{
				flag = false;
			}
			else if (Environment.GetEnvironmentVariable($"No_Expand_{name}") != null)
			{
				flag = false;
			}
			text = Environment.GetEnvironmentVariable(name);
			if (flag && !string.IsNullOrEmpty(text))
			{
				text = Environment.ExpandEnvironmentVariables(text);
			}
			if (text != null)
			{
				return text;
			}
			try
			{
				string xmlConfigFileName = GetXmlConfigFileName();
				if (xmlConfigFileName == null)
				{
					return @default;
				}
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(xmlConfigFileName);
				if (xmlDocument.SelectSingleNode($"/configuration/appSettings/add[@key='{name}']") is XmlElement xmlElement)
				{
					if (xmlElement.HasAttribute("value"))
					{
						text = xmlElement.GetAttribute("value");
					}
					if (flag && !string.IsNullOrEmpty(text))
					{
						text = Environment.ExpandEnvironmentVariables(text);
					}
					if (text != null)
					{
						return text;
					}
				}
			}
			catch (Exception ex)
			{
				try
				{
					Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Native library pre-loader failed to get setting \"{0}\" value: {1}", new object[2] { name, ex }));
				}
				catch
				{
				}
			}
			return @default;
		}

		private static string ListToString(IList<string> list)
		{
			if (list == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string item in list)
			{
				if (item != null)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(' ');
					}
					stringBuilder.Append(item);
				}
			}
			return stringBuilder.ToString();
		}

		private static int CheckForArchitecturesAndPlatforms(string directory, ref List<string> matches)
		{
			int num = 0;
			if (matches == null)
			{
				matches = new List<string>();
			}
			lock (staticSyncRoot)
			{
				if (!string.IsNullOrEmpty(directory) && processorArchitecturePlatforms != null)
				{
					foreach (KeyValuePair<string, string> processorArchitecturePlatform in processorArchitecturePlatforms)
					{
						if (Directory.Exists(Path.Combine(directory, processorArchitecturePlatform.Key)))
						{
							matches.Add(processorArchitecturePlatform.Key);
							num++;
						}
						string value = processorArchitecturePlatform.Value;
						if (value != null && Directory.Exists(Path.Combine(directory, value)))
						{
							matches.Add(value);
							num++;
						}
					}
				}
			}
			return num;
		}

		private static bool CheckAssemblyCodeBase(Assembly assembly, ref string fileName)
		{
			try
			{
				if ((object)assembly == null)
				{
					return false;
				}
				string codeBase = assembly.CodeBase;
				if (string.IsNullOrEmpty(codeBase))
				{
					return false;
				}
				Uri uri = new Uri(codeBase);
				string localPath = uri.LocalPath;
				if (!File.Exists(localPath))
				{
					return false;
				}
				string directoryName = Path.GetDirectoryName(localPath);
				string path = Path.Combine(directoryName, XmlConfigFileName);
				if (File.Exists(path))
				{
					fileName = localPath;
					return true;
				}
				List<string> matches = null;
				if (CheckForArchitecturesAndPlatforms(directoryName, ref matches) > 0)
				{
					fileName = localPath;
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				try
				{
					Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Native library pre-loader failed to check code base for currently executing assembly: {0}", new object[1] { ex }));
				}
				catch
				{
				}
			}
			return false;
		}

		private static string GetAssemblyDirectory()
		{
			try
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				if ((object)executingAssembly == null)
				{
					return null;
				}
				string fileName = null;
				if (!CheckAssemblyCodeBase(executingAssembly, ref fileName))
				{
					fileName = executingAssembly.Location;
				}
				if (string.IsNullOrEmpty(fileName))
				{
					return null;
				}
				string directoryName = Path.GetDirectoryName(fileName);
				if (string.IsNullOrEmpty(directoryName))
				{
					return null;
				}
				return directoryName;
			}
			catch (Exception ex)
			{
				try
				{
					Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Native library pre-loader failed to get directory for currently executing assembly: {0}", new object[1] { ex }));
				}
				catch
				{
				}
			}
			return null;
		}

		[DllImport("kernel32", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true, ThrowOnUnmappableChar = true)]
		private static extern IntPtr LoadLibrary(string fileName);

		private static bool SearchForDirectory(ref string baseDirectory, ref string processorArchitecture)
		{
			if (GetSettingValue("PreLoadSQLite_NoSearchForDirectory", null) != null)
			{
				return false;
			}
			string[] array = new string[2]
			{
				GetAssemblyDirectory(),
				AppDomain.CurrentDomain.BaseDirectory
			};
			string[] array2 = new string[2]
			{
				GetProcessorArchitecture(),
				GetPlatformName(null)
			};
			string[] array3 = array;
			foreach (string text in array3)
			{
				if (text == null)
				{
					continue;
				}
				string[] array4 = array2;
				foreach (string text2 in array4)
				{
					if (text2 != null)
					{
						string path = FixUpDllFileName(Path.Combine(Path.Combine(text, text2), "sqlite3"));
						if (File.Exists(path))
						{
							baseDirectory = text;
							processorArchitecture = text2;
							return true;
						}
					}
				}
			}
			return false;
		}

		private static string GetBaseDirectory()
		{
			string settingValue = GetSettingValue("PreLoadSQLite_BaseDirectory", null);
			if (settingValue != null)
			{
				return settingValue;
			}
			if (GetSettingValue("PreLoadSQLite_UseAssemblyDirectory", null) != null)
			{
				settingValue = GetAssemblyDirectory();
				if (settingValue != null)
				{
					return settingValue;
				}
			}
			return AppDomain.CurrentDomain.BaseDirectory;
		}

		private static string FixUpDllFileName(string fileName)
		{
			if (!string.IsNullOrEmpty(fileName))
			{
				PlatformID platform = Environment.OSVersion.Platform;
				if ((platform == PlatformID.Win32S || platform == PlatformID.Win32Windows || platform == PlatformID.Win32NT || platform == PlatformID.WinCE) && !fileName.EndsWith(DllFileExtension, StringComparison.OrdinalIgnoreCase))
				{
					return fileName + DllFileExtension;
				}
			}
			return fileName;
		}

		private static string GetProcessorArchitecture()
		{
			string settingValue = GetSettingValue("PreLoadSQLite_ProcessorArchitecture", null);
			if (settingValue != null)
			{
				return settingValue;
			}
			settingValue = GetSettingValue(PROCESSOR_ARCHITECTURE, null);
			if (IntPtr.Size == 4 && string.Equals(settingValue, "AMD64", StringComparison.OrdinalIgnoreCase))
			{
				settingValue = "x86";
			}
			return settingValue;
		}

		private static string GetPlatformName(string processorArchitecture)
		{
			if (processorArchitecture == null)
			{
				processorArchitecture = GetProcessorArchitecture();
			}
			if (string.IsNullOrEmpty(processorArchitecture))
			{
				return null;
			}
			lock (staticSyncRoot)
			{
				if (processorArchitecturePlatforms == null)
				{
					return null;
				}
				if (processorArchitecturePlatforms.TryGetValue(processorArchitecture, out var value))
				{
					return value;
				}
			}
			return null;
		}

		private static bool PreLoadSQLiteDll(string baseDirectory, string processorArchitecture, ref string nativeModuleFileName, ref IntPtr nativeModuleHandle)
		{
			if (baseDirectory == null)
			{
				baseDirectory = GetBaseDirectory();
			}
			if (baseDirectory == null)
			{
				return false;
			}
			string path = FixUpDllFileName(Path.Combine(baseDirectory, "sqlite3"));
			if (File.Exists(path))
			{
				return false;
			}
			if (processorArchitecture == null)
			{
				processorArchitecture = GetProcessorArchitecture();
			}
			if (processorArchitecture == null)
			{
				return false;
			}
			path = FixUpDllFileName(Path.Combine(Path.Combine(baseDirectory, processorArchitecture), "sqlite3"));
			if (!File.Exists(path))
			{
				string platformName = GetPlatformName(processorArchitecture);
				if (platformName == null)
				{
					return false;
				}
				path = FixUpDllFileName(Path.Combine(Path.Combine(baseDirectory, platformName), "sqlite3"));
				if (!File.Exists(path))
				{
					return false;
				}
			}
			try
			{
				try
				{
					Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Native library pre-loader is trying to load native SQLite library \"{0}\"...", new object[1] { path }));
				}
				catch
				{
				}
				nativeModuleFileName = path;
				nativeModuleHandle = LoadLibrary(path);
				return nativeModuleHandle != IntPtr.Zero;
			}
			catch (Exception ex)
			{
				try
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Native library pre-loader failed to load native SQLite library \"{0}\" (getLastError = {1}): {2}", new object[3] { path, lastWin32Error, ex }));
				}
				catch
				{
				}
			}
			return false;
		}

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_close(IntPtr db);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_close_v2(IntPtr db);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_create_function(IntPtr db, byte[] strName, int nArgs, int nType, IntPtr pvUser, SQLiteCallback func, SQLiteCallback fstep, SQLiteFinalCallback ffinal);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_finalize(IntPtr stmt);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_backup_finish(IntPtr backup);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_reset(IntPtr stmt);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_bind_parameter_name(IntPtr stmt, int index);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_database_name(IntPtr stmt, int index);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_database_name16(IntPtr stmt, int index);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_decltype(IntPtr stmt, int index);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_decltype16(IntPtr stmt, int index);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_name(IntPtr stmt, int index);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_name16(IntPtr stmt, int index);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_origin_name(IntPtr stmt, int index);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_origin_name16(IntPtr stmt, int index);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_table_name(IntPtr stmt, int index);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_table_name16(IntPtr stmt, int index);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_text(IntPtr stmt, int index);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_text16(IntPtr stmt, int index);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_errmsg(IntPtr db);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_prepare(IntPtr db, IntPtr pSql, int nBytes, ref IntPtr stmt, ref IntPtr ptrRemain);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_prepare_v2(IntPtr db, IntPtr pSql, int nBytes, ref IntPtr stmt, ref IntPtr ptrRemain);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_table_column_metadata(IntPtr db, byte[] dbName, byte[] tblName, byte[] colName, ref IntPtr ptrDataType, ref IntPtr ptrCollSeq, ref int notNull, ref int primaryKey, ref int autoInc);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_value_text(IntPtr p);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_value_text16(IntPtr p);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_libversion();

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_libversion_number();

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_sourceid();

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_compileoption_used(IntPtr zOptName);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_compileoption_get(int N);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_enable_shared_cache(int enable);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_enable_load_extension(IntPtr db, int enable);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_load_extension(IntPtr db, byte[] fileName, byte[] procName, ref IntPtr pError);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_overload_function(IntPtr db, IntPtr zName, int nArgs);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		internal static extern SQLiteErrorCode sqlite3_win32_set_directory(uint type, string value);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_win32_reset_heap();

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_win32_compact_heap(ref uint largest);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_malloc(int n);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_realloc(IntPtr p, int n);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_free(IntPtr p);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_open_v2(byte[] utf8Filename, ref IntPtr db, SQLiteOpenFlagsEnum flags, IntPtr vfs);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		internal static extern SQLiteErrorCode sqlite3_open16(string fileName, ref IntPtr db);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_interrupt(IntPtr db);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern long sqlite3_last_insert_rowid(IntPtr db);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_changes(IntPtr db);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern long sqlite3_memory_used();

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern long sqlite3_memory_highwater(int resetFlag);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_shutdown();

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_busy_timeout(IntPtr db, int ms);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_bind_blob(IntPtr stmt, int index, byte[] value, int nSize, IntPtr nTransient);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_bind_double(IntPtr stmt, int index, double value);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_bind_int(IntPtr stmt, int index, int value);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "sqlite3_bind_int")]
		internal static extern SQLiteErrorCode sqlite3_bind_uint(IntPtr stmt, int index, uint value);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_bind_int64(IntPtr stmt, int index, long value);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "sqlite3_bind_int64")]
		internal static extern SQLiteErrorCode sqlite3_bind_uint64(IntPtr stmt, int index, ulong value);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_bind_null(IntPtr stmt, int index);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_bind_text(IntPtr stmt, int index, byte[] value, int nlen, IntPtr pvReserved);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_bind_parameter_count(IntPtr stmt);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_bind_parameter_index(IntPtr stmt, byte[] strName);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_column_count(IntPtr stmt);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_step(IntPtr stmt);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_stmt_readonly(IntPtr stmt);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern double sqlite3_column_double(IntPtr stmt, int index);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_column_int(IntPtr stmt, int index);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern long sqlite3_column_int64(IntPtr stmt, int index);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_column_blob(IntPtr stmt, int index);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_column_bytes(IntPtr stmt, int index);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_column_bytes16(IntPtr stmt, int index);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern TypeAffinity sqlite3_column_type(IntPtr stmt, int index);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_create_collation(IntPtr db, byte[] strName, int nType, IntPtr pvUser, SQLiteCollation func);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_aggregate_count(IntPtr context);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_value_blob(IntPtr p);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_value_bytes(IntPtr p);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_value_bytes16(IntPtr p);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern double sqlite3_value_double(IntPtr p);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_value_int(IntPtr p);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern long sqlite3_value_int64(IntPtr p);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern TypeAffinity sqlite3_value_type(IntPtr p);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_result_blob(IntPtr context, byte[] value, int nSize, IntPtr pvReserved);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_result_double(IntPtr context, double value);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_result_error(IntPtr context, byte[] strErr, int nLen);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_result_error_code(IntPtr context, SQLiteErrorCode value);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_result_error_toobig(IntPtr context);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_result_error_nomem(IntPtr context);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_result_value(IntPtr context, IntPtr value);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_result_zeroblob(IntPtr context, int nLen);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_result_int(IntPtr context, int value);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_result_int64(IntPtr context, long value);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_result_null(IntPtr context);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_result_text(IntPtr context, byte[] value, int nLen, IntPtr pvReserved);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_aggregate_context(IntPtr context, int nBytes);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		internal static extern SQLiteErrorCode sqlite3_bind_text16(IntPtr stmt, int index, string value, int nlen, IntPtr pvReserved);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		internal static extern void sqlite3_result_error16(IntPtr context, string strName, int nLen);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		internal static extern void sqlite3_result_text16(IntPtr context, string strName, int nLen, IntPtr pvReserved);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_key(IntPtr db, byte[] key, int keylen);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_rekey(IntPtr db, byte[] key, int keylen);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_set_authorizer(IntPtr db, SQLiteAuthorizerCallback func, IntPtr pvUser);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_update_hook(IntPtr db, SQLiteUpdateCallback func, IntPtr pvUser);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_commit_hook(IntPtr db, SQLiteCommitCallback func, IntPtr pvUser);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_trace(IntPtr db, SQLiteTraceCallback func, IntPtr pvUser);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "sqlite3_config")]
		internal static extern SQLiteErrorCode sqlite3_config_none(SQLiteConfigOpsEnum op);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "sqlite3_config")]
		internal static extern SQLiteErrorCode sqlite3_config_int(SQLiteConfigOpsEnum op, int value);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl, EntryPoint = "sqlite3_config")]
		internal static extern SQLiteErrorCode sqlite3_config_log(SQLiteConfigOpsEnum op, SQLiteLogCallback func, IntPtr pvUser);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_rollback_hook(IntPtr db, SQLiteRollbackCallback func, IntPtr pvUser);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_db_handle(IntPtr stmt);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_db_release_memory(IntPtr db);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_db_filename(IntPtr db, IntPtr dbName);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_next_stmt(IntPtr db, IntPtr stmt);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_exec(IntPtr db, byte[] strSql, IntPtr pvCallback, IntPtr pvParam, ref IntPtr errMsg);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_release_memory(int nBytes);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_get_autocommit(IntPtr db);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_extended_result_codes(IntPtr db, int onoff);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_errcode(IntPtr db);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_extended_errcode(IntPtr db);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_errstr(SQLiteErrorCode rc);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_log(SQLiteErrorCode iErrCode, byte[] zFormat);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_file_control(IntPtr db, byte[] zDbName, int op, IntPtr pArg);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_backup_init(IntPtr destDb, byte[] zDestName, IntPtr sourceDb, byte[] zSourceName);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_backup_step(IntPtr backup, int nPage);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_backup_remaining(IntPtr backup);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern int sqlite3_backup_pagecount(IntPtr backup);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern SQLiteErrorCode sqlite3_declare_vtab(IntPtr db, IntPtr zSQL);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_mprintf(IntPtr format, __arglist);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern IntPtr sqlite3_create_disposable_module(IntPtr db, IntPtr name, ref sqlite3_module module, IntPtr pClientData, xDestroyModule xDestroy);

		[DllImport("sqlite3", CallingConvention = CallingConvention.Cdecl)]
		internal static extern void sqlite3_dispose_module(IntPtr pModule);
	}
	internal sealed class SQLiteConnectionHandle : CriticalHandle
	{
		internal delegate void CloseConnectionCallback(SQLiteConnectionHandle hdl, IntPtr db);

		internal static CloseConnectionCallback closeConnection = SQLiteBase.CloseConnection;

		private bool ownHandle;

		public bool OwnHandle => ownHandle;

		public override bool IsInvalid => handle == IntPtr.Zero;

		public static implicit operator IntPtr(SQLiteConnectionHandle db)
		{
			return db?.handle ?? IntPtr.Zero;
		}

		internal SQLiteConnectionHandle(IntPtr db, bool ownHandle)
			: this(ownHandle)
		{
			this.ownHandle = ownHandle;
			SetHandle(db);
		}

		private SQLiteConnectionHandle(bool ownHandle)
			: base(IntPtr.Zero)
		{
		}

		protected override bool ReleaseHandle()
		{
			if (!ownHandle)
			{
				return true;
			}
			try
			{
				IntPtr intPtr = Interlocked.Exchange(ref handle, IntPtr.Zero);
				if (intPtr != IntPtr.Zero)
				{
					closeConnection(this, intPtr);
				}
			}
			catch (SQLiteException)
			{
			}
			finally
			{
				SetHandleAsInvalid();
			}
			return true;
		}
	}
	internal sealed class SQLiteStatementHandle : CriticalHandle
	{
		private SQLiteConnectionHandle cnn;

		public override bool IsInvalid => handle == IntPtr.Zero;

		public static implicit operator IntPtr(SQLiteStatementHandle stmt)
		{
			return stmt?.handle ?? IntPtr.Zero;
		}

		internal SQLiteStatementHandle(SQLiteConnectionHandle cnn, IntPtr stmt)
			: this()
		{
			this.cnn = cnn;
			SetHandle(stmt);
		}

		private SQLiteStatementHandle()
			: base(IntPtr.Zero)
		{
		}

		protected override bool ReleaseHandle()
		{
			try
			{
				IntPtr intPtr = Interlocked.Exchange(ref handle, IntPtr.Zero);
				if (intPtr != IntPtr.Zero)
				{
					SQLiteBase.FinalizeStatement(cnn, intPtr);
				}
			}
			catch (SQLiteException)
			{
			}
			finally
			{
				SetHandleAsInvalid();
			}
			return true;
		}
	}
	internal sealed class SQLiteBackupHandle : CriticalHandle
	{
		private SQLiteConnectionHandle cnn;

		public override bool IsInvalid => handle == IntPtr.Zero;

		public static implicit operator IntPtr(SQLiteBackupHandle backup)
		{
			return backup?.handle ?? IntPtr.Zero;
		}

		internal SQLiteBackupHandle(SQLiteConnectionHandle cnn, IntPtr backup)
			: this()
		{
			this.cnn = cnn;
			SetHandle(backup);
		}

		private SQLiteBackupHandle()
			: base(IntPtr.Zero)
		{
		}

		protected override bool ReleaseHandle()
		{
			try
			{
				IntPtr intPtr = Interlocked.Exchange(ref handle, IntPtr.Zero);
				if (intPtr != IntPtr.Zero)
				{
					SQLiteBase.FinishBackup(cnn, intPtr);
				}
			}
			catch (SQLiteException)
			{
			}
			finally
			{
				SetHandleAsInvalid();
			}
			return true;
		}
	}
	[DebuggerNonUserCode]
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	internal sealed class SR
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("System.Data.SQLite.SR", typeof(SR).Assembly);
					resourceMan = resourceManager;
				}
				return resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return resourceCulture;
			}
			set
			{
				resourceCulture = value;
			}
		}

		internal static string DataTypes => ResourceManager.GetString("DataTypes", resourceCulture);

		internal static string Keywords => ResourceManager.GetString("Keywords", resourceCulture);

		internal static string MetaDataCollections => ResourceManager.GetString("MetaDataCollections", resourceCulture);

		internal SR()
		{
		}
	}
	internal sealed class SQLiteEnlistment : IDisposable, IEnlistmentNotification
	{
		internal SQLiteTransaction _transaction;

		internal Transaction _scope;

		internal bool _disposeConnection;

		private bool disposed;

		internal SQLiteEnlistment(SQLiteConnection cnn, Transaction scope, IsolationLevel defaultIsolationLevel, bool throwOnUnavailable, bool throwOnUnsupported)
		{
			_transaction = cnn.BeginTransaction(GetSystemDataIsolationLevel(cnn, scope, defaultIsolationLevel, throwOnUnavailable, throwOnUnsupported));
			_scope = scope;
			_scope.EnlistVolatile(this, EnlistmentOptions.None);
		}

		private IsolationLevel GetSystemDataIsolationLevel(SQLiteConnection connection, Transaction transaction, IsolationLevel defaultIsolationLevel, bool throwOnUnavailable, bool throwOnUnsupported)
		{
			if (transaction == null)
			{
				if (connection != null)
				{
					return connection.GetDefaultIsolationLevel();
				}
				if (throwOnUnavailable)
				{
					throw new InvalidOperationException("isolation level is unavailable");
				}
				return defaultIsolationLevel;
			}
			System.Transactions.IsolationLevel isolationLevel = transaction.IsolationLevel;
			switch (isolationLevel)
			{
			case System.Transactions.IsolationLevel.Unspecified:
				return IsolationLevel.Unspecified;
			case System.Transactions.IsolationLevel.Chaos:
				return IsolationLevel.Chaos;
			case System.Transactions.IsolationLevel.ReadUncommitted:
				return IsolationLevel.ReadUncommitted;
			case System.Transactions.IsolationLevel.ReadCommitted:
				return IsolationLevel.ReadCommitted;
			case System.Transactions.IsolationLevel.RepeatableRead:
				return IsolationLevel.RepeatableRead;
			case System.Transactions.IsolationLevel.Serializable:
				return IsolationLevel.Serializable;
			case System.Transactions.IsolationLevel.Snapshot:
				return IsolationLevel.Snapshot;
			default:
				if (throwOnUnsupported)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "unsupported isolation level {0}", new object[1] { isolationLevel }));
				}
				return defaultIsolationLevel;
			}
		}

		private void Cleanup(SQLiteConnection cnn)
		{
			if (_disposeConnection)
			{
				cnn.Dispose();
			}
			_transaction = null;
			_scope = null;
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		private void CheckDisposed()
		{
			if (disposed)
			{
				throw new ObjectDisposedException(typeof(SQLiteEnlistment).Name);
			}
		}

		private void Dispose(bool disposing)
		{
			if (disposed)
			{
				return;
			}
			if (disposing)
			{
				if (_transaction != null)
				{
					_transaction.Dispose();
					_transaction = null;
				}
				if (_scope != null)
				{
					_scope = null;
				}
			}
			disposed = true;
		}

		~SQLiteEnlistment()
		{
			Dispose(disposing: false);
		}

		public void Commit(Enlistment enlistment)
		{
			CheckDisposed();
			SQLiteConnection connection = _transaction.Connection;
			connection._enlistment = null;
			try
			{
				_transaction.IsValid(throwError: true);
				_transaction.Connection._transactionLevel = 1;
				_transaction.Commit();
				enlistment.Done();
			}
			finally
			{
				Cleanup(connection);
			}
		}

		public void InDoubt(Enlistment enlistment)
		{
			CheckDisposed();
			enlistment.Done();
		}

		public void Prepare(PreparingEnlistment preparingEnlistment)
		{
			CheckDisposed();
			if (!_transaction.IsValid(throwError: false))
			{
				preparingEnlistment.ForceRollback();
			}
			else
			{
				preparingEnlistment.Prepared();
			}
		}

		public void Rollback(Enlistment enlistment)
		{
			CheckDisposed();
			SQLiteConnection connection = _transaction.Connection;
			connection._enlistment = null;
			try
			{
				_transaction.Rollback();
				enlistment.Done();
			}
			finally
			{
				Cleanup(connection);
			}
		}
	}
	public interface ISQLiteNativeHandle
	{
		IntPtr NativeHandle { get; }
	}
	public sealed class SQLiteContext : ISQLiteNativeHandle
	{
		private IntPtr pContext;

		public IntPtr NativeHandle => pContext;

		internal SQLiteContext(IntPtr pContext)
		{
			this.pContext = pContext;
		}

		public void SetNull()
		{
			if (pContext == IntPtr.Zero)
			{
				throw new InvalidOperationException();
			}
			UnsafeNativeMethods.sqlite3_result_null(pContext);
		}

		public void SetDouble(double value)
		{
			if (pContext == IntPtr.Zero)
			{
				throw new InvalidOperationException();
			}
			UnsafeNativeMethods.sqlite3_result_double(pContext, value);
		}

		public void SetInt(int value)
		{
			if (pContext == IntPtr.Zero)
			{
				throw new InvalidOperationException();
			}
			UnsafeNativeMethods.sqlite3_result_int(pContext, value);
		}

		public void SetInt64(long value)
		{
			if (pContext == IntPtr.Zero)
			{
				throw new InvalidOperationException();
			}
			UnsafeNativeMethods.sqlite3_result_int64(pContext, value);
		}

		public void SetString(string value)
		{
			if (pContext == IntPtr.Zero)
			{
				throw new InvalidOperationException();
			}
			byte[] utf8BytesFromString = SQLiteString.GetUtf8BytesFromString(value);
			if (utf8BytesFromString == null)
			{
				throw new ArgumentNullException("value");
			}
			UnsafeNativeMethods.sqlite3_result_text(pContext, utf8BytesFromString, utf8BytesFromString.Length, (IntPtr)(-1));
		}

		public void SetError(string value)
		{
			if (pContext == IntPtr.Zero)
			{
				throw new InvalidOperationException();
			}
			byte[] utf8BytesFromString = SQLiteString.GetUtf8BytesFromString(value);
			if (utf8BytesFromString == null)
			{
				throw new ArgumentNullException("value");
			}
			UnsafeNativeMethods.sqlite3_result_error(pContext, utf8BytesFromString, utf8BytesFromString.Length);
		}

		public void SetErrorCode(SQLiteErrorCode value)
		{
			if (pContext == IntPtr.Zero)
			{
				throw new InvalidOperationException();
			}
			UnsafeNativeMethods.sqlite3_result_error_code(pContext, value);
		}

		public void SetErrorTooBig()
		{
			if (pContext == IntPtr.Zero)
			{
				throw new InvalidOperationException();
			}
			UnsafeNativeMethods.sqlite3_result_error_toobig(pContext);
		}

		public void SetErrorNoMemory()
		{
			if (pContext == IntPtr.Zero)
			{
				throw new InvalidOperationException();
			}
			UnsafeNativeMethods.sqlite3_result_error_nomem(pContext);
		}

		public void SetBlob(byte[] value)
		{
			if (pContext == IntPtr.Zero)
			{
				throw new InvalidOperationException();
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			UnsafeNativeMethods.sqlite3_result_blob(pContext, value, value.Length, (IntPtr)(-1));
		}

		public void SetZeroBlob(int value)
		{
			if (pContext == IntPtr.Zero)
			{
				throw new InvalidOperationException();
			}
			UnsafeNativeMethods.sqlite3_result_zeroblob(pContext, value);
		}

		public void SetValue(SQLiteValue value)
		{
			if (pContext == IntPtr.Zero)
			{
				throw new InvalidOperationException();
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			UnsafeNativeMethods.sqlite3_result_value(pContext, value.NativeHandle);
		}
	}
	public sealed class SQLiteValue : ISQLiteNativeHandle
	{
		private IntPtr pValue;

		private bool persisted;

		private object value;

		public IntPtr NativeHandle => pValue;

		public bool Persisted => persisted;

		public object Value
		{
			get
			{
				if (!persisted)
				{
					throw new InvalidOperationException("value was not persisted");
				}
				return value;
			}
		}

		private SQLiteValue(IntPtr pValue)
		{
			this.pValue = pValue;
		}

		private void PreventNativeAccess()
		{
			pValue = IntPtr.Zero;
		}

		internal static SQLiteValue[] ArrayFromSizeAndIntPtr(int argc, IntPtr argv)
		{
			if (argc < 0)
			{
				return null;
			}
			if (argv == IntPtr.Zero)
			{
				return null;
			}
			SQLiteValue[] array = new SQLiteValue[argc];
			int num = 0;
			int num2 = 0;
			while (num < array.Length)
			{
				IntPtr intPtr = SQLiteMarshal.ReadIntPtr(argv, num2);
				array[num] = ((intPtr != IntPtr.Zero) ? new SQLiteValue(intPtr) : null);
				num++;
				num2 += IntPtr.Size;
			}
			return array;
		}

		public TypeAffinity GetTypeAffinity()
		{
			if (pValue == IntPtr.Zero)
			{
				return TypeAffinity.None;
			}
			return UnsafeNativeMethods.sqlite3_value_type(pValue);
		}

		public int GetBytes()
		{
			if (pValue == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.sqlite3_value_bytes(pValue);
		}

		public int GetInt()
		{
			if (pValue == IntPtr.Zero)
			{
				return 0;
			}
			return UnsafeNativeMethods.sqlite3_value_int(pValue);
		}

		public long GetInt64()
		{
			if (pValue == IntPtr.Zero)
			{
				return 0L;
			}
			return UnsafeNativeMethods.sqlite3_value_int64(pValue);
		}

		public double GetDouble()
		{
			if (pValue == IntPtr.Zero)
			{
				return 0.0;
			}
			return UnsafeNativeMethods.sqlite3_value_double(pValue);
		}

		public string GetString()
		{
			if (pValue == IntPtr.Zero)
			{
				return null;
			}
			int length = UnsafeNativeMethods.sqlite3_value_bytes(pValue);
			IntPtr intPtr = UnsafeNativeMethods.sqlite3_value_text(pValue);
			return SQLiteString.StringFromUtf8IntPtr(intPtr, length);
		}

		public byte[] GetBlob()
		{
			if (pValue == IntPtr.Zero)
			{
				return null;
			}
			return SQLiteBytes.FromIntPtr(UnsafeNativeMethods.sqlite3_value_blob(pValue), GetBytes());
		}

		public bool Persist()
		{
			switch (GetTypeAffinity())
			{
			case TypeAffinity.Uninitialized:
				value = null;
				PreventNativeAccess();
				return persisted = true;
			case TypeAffinity.Int64:
				value = GetInt64();
				PreventNativeAccess();
				return persisted = true;
			case TypeAffinity.Double:
				value = GetDouble();
				PreventNativeAccess();
				return persisted = true;
			case TypeAffinity.Text:
				value = GetString();
				PreventNativeAccess();
				return persisted = true;
			case TypeAffinity.Blob:
				value = GetBytes();
				PreventNativeAccess();
				return persisted = true;
			case TypeAffinity.Null:
				value = DBNull.Value;
				PreventNativeAccess();
				return persisted = true;
			default:
				return false;
			}
		}
	}
	public enum SQLiteIndexConstraintOp : byte
	{
		EqualTo = 2,
		GreaterThan = 4,
		LessThanOrEqualTo = 8,
		LessThan = 0x10,
		GreaterThanOrEqualTo = 0x20,
		Match = 0x40
	}
	public sealed class SQLiteIndexConstraint
	{
		public int iColumn;

		public SQLiteIndexConstraintOp op;

		public byte usable;

		public int iTermOffset;

		internal SQLiteIndexConstraint(UnsafeNativeMethods.sqlite3_index_constraint constraint)
			: this(constraint.iColumn, constraint.op, constraint.usable, constraint.iTermOffset)
		{
		}

		private SQLiteIndexConstraint(int iColumn, SQLiteIndexConstraintOp op, byte usable, int iTermOffset)
		{
			this.iColumn = iColumn;
			this.op = op;
			this.usable = usable;
			this.iTermOffset = iTermOffset;
		}
	}
	public sealed class SQLiteIndexOrderBy
	{
		public int iColumn;

		public byte desc;

		internal SQLiteIndexOrderBy(UnsafeNativeMethods.sqlite3_index_orderby orderBy)
			: this(orderBy.iColumn, orderBy.desc)
		{
		}

		private SQLiteIndexOrderBy(int iColumn, byte desc)
		{
			this.iColumn = iColumn;
			this.desc = desc;
		}
	}
	public sealed class SQLiteIndexConstraintUsage
	{
		public int argvIndex;

		public byte omit;

		internal SQLiteIndexConstraintUsage()
		{
		}

		internal SQLiteIndexConstraintUsage(UnsafeNativeMethods.sqlite3_index_constraint_usage constraintUsage)
			: this(constraintUsage.argvIndex, constraintUsage.omit)
		{
		}

		private SQLiteIndexConstraintUsage(int argvIndex, byte omit)
		{
			this.argvIndex = argvIndex;
			this.omit = omit;
		}
	}
	public sealed class SQLiteIndexInputs
	{
		private SQLiteIndexConstraint[] constraints;

		private SQLiteIndexOrderBy[] orderBys;

		public SQLiteIndexConstraint[] Constraints => constraints;

		public SQLiteIndexOrderBy[] OrderBys => orderBys;

		internal SQLiteIndexInputs(int nConstraint, int nOrderBy)
		{
			constraints = new SQLiteIndexConstraint[nConstraint];
			orderBys = new SQLiteIndexOrderBy[nOrderBy];
		}
	}
	public sealed class SQLiteIndexOutputs
	{
		private SQLiteIndexConstraintUsage[] constraintUsages;

		private int indexNumber;

		private string indexString;

		private int needToFreeIndexString;

		private int orderByConsumed;

		private double? estimatedCost;

		private long? estimatedRows;

		public SQLiteIndexConstraintUsage[] ConstraintUsages => constraintUsages;

		public int IndexNumber
		{
			get
			{
				return indexNumber;
			}
			set
			{
				indexNumber = value;
			}
		}

		public string IndexString
		{
			get
			{
				return indexString;
			}
			set
			{
				indexString = value;
			}
		}

		public int NeedToFreeIndexString
		{
			get
			{
				return needToFreeIndexString;
			}
			set
			{
				needToFreeIndexString = value;
			}
		}

		public int OrderByConsumed
		{
			get
			{
				return orderByConsumed;
			}
			set
			{
				orderByConsumed = value;
			}
		}

		public double? EstimatedCost
		{
			get
			{
				return estimatedCost;
			}
			set
			{
				estimatedCost = value;
			}
		}

		public long? EstimatedRows
		{
			get
			{
				return estimatedRows;
			}
			set
			{
				estimatedRows = value;
			}
		}

		internal SQLiteIndexOutputs(int nConstraint)
		{
			constraintUsages = new SQLiteIndexConstraintUsage[nConstraint];
			for (int i = 0; i < nConstraint; i++)
			{
				constraintUsages[i] = new SQLiteIndexConstraintUsage();
			}
		}

		public bool CanUseEstimatedRows()
		{
			if (UnsafeNativeMethods.sqlite3_libversion_number() >= 3008002)
			{
				return true;
			}
			return false;
		}
	}
	public sealed class SQLiteIndex
	{
		private SQLiteIndexInputs inputs;

		private SQLiteIndexOutputs outputs;

		public SQLiteIndexInputs Inputs => inputs;

		public SQLiteIndexOutputs Outputs => outputs;

		internal SQLiteIndex(int nConstraint, int nOrderBy)
		{
			inputs = new SQLiteIndexInputs(nConstraint, nOrderBy);
			outputs = new SQLiteIndexOutputs(nConstraint);
		}

		internal static void FromIntPtr(IntPtr pIndex, ref SQLiteIndex index)
		{
			if (!(pIndex == IntPtr.Zero))
			{
				int offset = 0;
				int num = SQLiteMarshal.ReadInt32(pIndex, offset);
				offset = SQLiteMarshal.NextOffsetOf(offset, 4, IntPtr.Size);
				IntPtr pointer = SQLiteMarshal.ReadIntPtr(pIndex, offset);
				offset = SQLiteMarshal.NextOffsetOf(offset, IntPtr.Size, 4);
				int num2 = SQLiteMarshal.ReadInt32(pIndex, offset);
				offset = SQLiteMarshal.NextOffsetOf(offset, 4, IntPtr.Size);
				IntPtr pointer2 = SQLiteMarshal.ReadIntPtr(pIndex, offset);
				index = new SQLiteIndex(num, num2);
				Type typeFromHandle = typeof(UnsafeNativeMethods.sqlite3_index_constraint);
				int num3 = Marshal.SizeOf(typeFromHandle);
				for (int i = 0; i < num; i++)
				{
					IntPtr ptr = SQLiteMarshal.IntPtrForOffset(pointer, i * num3);
					UnsafeNativeMethods.sqlite3_index_constraint constraint = (UnsafeNativeMethods.sqlite3_index_constraint)Marshal.PtrToStructure(ptr, typeFromHandle);
					index.Inputs.Constraints[i] = new SQLiteIndexConstraint(constraint);
				}
				Type typeFromHandle2 = typeof(UnsafeNativeMethods.sqlite3_index_orderby);
				int num4 = Marshal.SizeOf(typeFromHandle2);
				for (int j = 0; j < num2; j++)
				{
					IntPtr ptr2 = SQLiteMarshal.IntPtrForOffset(pointer2, j * num4);
					UnsafeNativeMethods.sqlite3_index_orderby orderBy = (UnsafeNativeMethods.sqlite3_index_orderby)Marshal.PtrToStructure(ptr2, typeFromHandle2);
					index.Inputs.OrderBys[j] = new SQLiteIndexOrderBy(orderBy);
				}
			}
		}

		internal static void ToIntPtr(SQLiteIndex index, IntPtr pIndex)
		{
			if (index == null || index.Inputs == null || index.Inputs.Constraints == null || index.Outputs == null || index.Outputs.ConstraintUsages == null || pIndex == IntPtr.Zero)
			{
				return;
			}
			int offset = 0;
			int num = SQLiteMarshal.ReadInt32(pIndex, offset);
			if (num == index.Inputs.Constraints.Length && num == index.Outputs.ConstraintUsages.Length)
			{
				offset = SQLiteMarshal.NextOffsetOf(offset, 4, IntPtr.Size);
				offset = SQLiteMarshal.NextOffsetOf(offset, IntPtr.Size, 4);
				offset = SQLiteMarshal.NextOffsetOf(offset, 4, IntPtr.Size);
				offset = SQLiteMarshal.NextOffsetOf(offset, IntPtr.Size, 4);
				IntPtr pointer = SQLiteMarshal.ReadIntPtr(pIndex, offset);
				int num2 = Marshal.SizeOf(typeof(UnsafeNativeMethods.sqlite3_index_constraint_usage));
				for (int i = 0; i < num; i++)
				{
					UnsafeNativeMethods.sqlite3_index_constraint_usage sqlite3_index_constraint_usage = new UnsafeNativeMethods.sqlite3_index_constraint_usage(index.Outputs.ConstraintUsages[i]);
					Marshal.StructureToPtr((object)sqlite3_index_constraint_usage, SQLiteMarshal.IntPtrForOffset(pointer, i * num2), fDeleteOld: false);
				}
				offset = SQLiteMarshal.NextOffsetOf(offset, IntPtr.Size, 4);
				SQLiteMarshal.WriteInt32(pIndex, offset, index.Outputs.IndexNumber);
				offset = SQLiteMarshal.NextOffsetOf(offset, 4, IntPtr.Size);
				SQLiteMarshal.WriteIntPtr(pIndex, offset, SQLiteString.Utf8IntPtrFromString(index.Outputs.IndexString));
				offset = SQLiteMarshal.NextOffsetOf(offset, IntPtr.Size, 4);
				SQLiteMarshal.WriteInt32(pIndex, offset, 1);
				offset = SQLiteMarshal.NextOffsetOf(offset, 4, 4);
				SQLiteMarshal.WriteInt32(pIndex, offset, index.Outputs.OrderByConsumed);
				offset = SQLiteMarshal.NextOffsetOf(offset, 4, 8);
				if (index.Outputs.EstimatedCost.HasValue)
				{
					SQLiteMarshal.WriteDouble(pIndex, offset, index.Outputs.EstimatedCost.GetValueOrDefault());
				}
				if (index.Outputs.CanUseEstimatedRows() && index.Outputs.EstimatedRows.HasValue)
				{
					SQLiteMarshal.WriteInt64(pIndex, offset, index.Outputs.EstimatedRows.GetValueOrDefault());
				}
			}
		}
	}
	public class SQLiteVirtualTable : ISQLiteNativeHandle, IDisposable
	{
		private const int ModuleNameIndex = 0;

		private const int DatabaseNameIndex = 1;

		private const int TableNameIndex = 2;

		private string[] arguments;

		private SQLiteIndex index;

		private IntPtr nativeHandle;

		private bool disposed;

		public virtual string[] Arguments
		{
			get
			{
				CheckDisposed();
				return arguments;
			}
		}

		public virtual string ModuleName
		{
			get
			{
				CheckDisposed();
				string[] array = Arguments;
				if (array != null && array.Length > 0)
				{
					return array[0];
				}
				return null;
			}
		}

		public virtual string DatabaseName
		{
			get
			{
				CheckDisposed();
				string[] array = Arguments;
				if (array != null && array.Length > 1)
				{
					return array[1];
				}
				return null;
			}
		}

		public virtual string TableName
		{
			get
			{
				CheckDisposed();
				string[] array = Arguments;
				if (array != null && array.Length > 2)
				{
					return array[2];
				}
				return null;
			}
		}

		public virtual SQLiteIndex Index
		{
			get
			{
				CheckDisposed();
				return index;
			}
		}

		public virtual IntPtr NativeHandle
		{
			get
			{
				CheckDisposed();
				return nativeHandle;
			}
			internal set
			{
				nativeHandle = value;
			}
		}

		public SQLiteVirtualTable(string[] arguments)
		{
			this.arguments = arguments;
		}

		public virtual bool BestIndex(SQLiteIndex index)
		{
			CheckDisposed();
			this.index = index;
			return true;
		}

		public virtual bool Rename(string name)
		{
			CheckDisposed();
			if (arguments != null && arguments.Length > 2)
			{
				arguments[2] = name;
				return true;
			}
			return false;
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		private void CheckDisposed()
		{
			if (disposed)
			{
				throw new ObjectDisposedException(typeof(SQLiteVirtualTable).Name);
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				disposed = true;
			}
		}

		~SQLiteVirtualTable()
		{
			Dispose(disposing: false);
		}
	}
	public class SQLiteVirtualTableCursor : ISQLiteNativeHandle, IDisposable
	{
		protected static readonly int InvalidRowIndex;

		private int rowIndex;

		private SQLiteVirtualTable table;

		private int indexNumber;

		private string indexString;

		private SQLiteValue[] values;

		private IntPtr nativeHandle;

		private bool disposed;

		public virtual SQLiteVirtualTable Table
		{
			get
			{
				CheckDisposed();
				return table;
			}
		}

		public virtual int IndexNumber
		{
			get
			{
				CheckDisposed();
				return indexNumber;
			}
		}

		public virtual string IndexString
		{
			get
			{
				CheckDisposed();
				return indexString;
			}
		}

		public virtual SQLiteValue[] Values
		{
			get
			{
				CheckDisposed();
				return values;
			}
		}

		public virtual IntPtr NativeHandle
		{
			get
			{
				CheckDisposed();
				return nativeHandle;
			}
			internal set
			{
				nativeHandle = value;
			}
		}

		public SQLiteVirtualTableCursor(SQLiteVirtualTable table)
			: this()
		{
			this.table = table;
		}

		private SQLiteVirtualTableCursor()
		{
			rowIndex = InvalidRowIndex;
		}

		protected virtual int TryPersistValues(SQLiteValue[] values)
		{
			int num = 0;
			if (values != null)
			{
				foreach (SQLiteValue sQLiteValue in values)
				{
					if (sQLiteValue != null && sQLiteValue.Persist())
					{
						num++;
					}
				}
			}
			return num;
		}

		public virtual void Filter(int indexNumber, string indexString, SQLiteValue[] values)
		{
			CheckDisposed();
			if (values != null && TryPersistValues(values) != values.Length)
			{
				throw new SQLiteException("failed to persist one or more values");
			}
			this.indexNumber = indexNumber;
			this.indexString = indexString;
			this.values = values;
		}

		public virtual int GetRowIndex()
		{
			return rowIndex;
		}

		public virtual void NextRowIndex()
		{
			rowIndex++;
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		private void CheckDisposed()
		{
			if (disposed)
			{
				throw new ObjectDisposedException(typeof(SQLiteVirtualTableCursor).Name);
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				disposed = true;
			}
		}

		~SQLiteVirtualTableCursor()
		{
			Dispose(disposing: false);
		}
	}
	public interface ISQLiteNativeModule
	{
		SQLiteErrorCode xCreate(IntPtr pDb, IntPtr pAux, int argc, IntPtr argv, ref IntPtr pVtab, ref IntPtr pError);

		SQLiteErrorCode xConnect(IntPtr pDb, IntPtr pAux, int argc, IntPtr argv, ref IntPtr pVtab, ref IntPtr pError);

		SQLiteErrorCode xBestIndex(IntPtr pVtab, IntPtr pIndex);

		SQLiteErrorCode xDisconnect(IntPtr pVtab);

		SQLiteErrorCode xDestroy(IntPtr pVtab);

		SQLiteErrorCode xOpen(IntPtr pVtab, ref IntPtr pCursor);

		SQLiteErrorCode xClose(IntPtr pCursor);

		SQLiteErrorCode xFilter(IntPtr pCursor, int idxNum, IntPtr idxStr, int argc, IntPtr argv);

		SQLiteErrorCode xNext(IntPtr pCursor);

		int xEof(IntPtr pCursor);

		SQLiteErrorCode xColumn(IntPtr pCursor, IntPtr pContext, int index);

		SQLiteErrorCode xRowId(IntPtr pCursor, ref long rowId);

		SQLiteErrorCode xUpdate(IntPtr pVtab, int argc, IntPtr argv, ref long rowId);

		SQLiteErrorCode xBegin(IntPtr pVtab);

		SQLiteErrorCode xSync(IntPtr pVtab);

		SQLiteErrorCode xCommit(IntPtr pVtab);

		SQLiteErrorCode xRollback(IntPtr pVtab);

		int xFindFunction(IntPtr pVtab, int nArg, IntPtr zName, ref SQLiteCallback callback, ref IntPtr pClientData);

		SQLiteErrorCode xRename(IntPtr pVtab, IntPtr zNew);

		SQLiteErrorCode xSavepoint(IntPtr pVtab, int iSavepoint);

		SQLiteErrorCode xRelease(IntPtr pVtab, int iSavepoint);

		SQLiteErrorCode xRollbackTo(IntPtr pVtab, int iSavepoint);
	}
	public interface ISQLiteManagedModule
	{
		bool Declared { get; }

		string Name { get; }

		SQLiteErrorCode Create(SQLiteConnection connection, IntPtr pClientData, string[] arguments, ref SQLiteVirtualTable table, ref string error);

		SQLiteErrorCode Connect(SQLiteConnection connection, IntPtr pClientData, string[] arguments, ref SQLiteVirtualTable table, ref string error);

		SQLiteErrorCode BestIndex(SQLiteVirtualTable table, SQLiteIndex index);

		SQLiteErrorCode Disconnect(SQLiteVirtualTable table);

		SQLiteErrorCode Destroy(SQLiteVirtualTable table);

		SQLiteErrorCode Open(SQLiteVirtualTable table, ref SQLiteVirtualTableCursor cursor);

		SQLiteErrorCode Close(SQLiteVirtualTableCursor cursor);

		SQLiteErrorCode Filter(SQLiteVirtualTableCursor cursor, int indexNumber, string indexString, SQLiteValue[] values);

		SQLiteErrorCode Next(SQLiteVirtualTableCursor cursor);

		bool Eof(SQLiteVirtualTableCursor cursor);

		SQLiteErrorCode Column(SQLiteVirtualTableCursor cursor, SQLiteContext context, int index);

		SQLiteErrorCode RowId(SQLiteVirtualTableCursor cursor, ref long rowId);

		SQLiteErrorCode Update(SQLiteVirtualTable table, SQLiteValue[] values, ref long rowId);

		SQLiteErrorCode Begin(SQLiteVirtualTable table);

		SQLiteErrorCode Sync(SQLiteVirtualTable table);

		SQLiteErrorCode Commit(SQLiteVirtualTable table);

		SQLiteErrorCode Rollback(SQLiteVirtualTable table);

		bool FindFunction(SQLiteVirtualTable table, int argumentCount, string name, ref SQLiteFunction function, ref IntPtr pClientData);

		SQLiteErrorCode Rename(SQLiteVirtualTable table, string newName);

		SQLiteErrorCode Savepoint(SQLiteVirtualTable table, int savepoint);

		SQLiteErrorCode Release(SQLiteVirtualTable table, int savepoint);

		SQLiteErrorCode RollbackTo(SQLiteVirtualTable table, int savepoint);
	}
	internal static class SQLiteMemory
	{
		public static IntPtr Allocate(int size)
		{
			return UnsafeNativeMethods.sqlite3_malloc(size);
		}

		public static int Size(IntPtr pMemory)
		{
			return 0;
		}

		public static void Free(IntPtr pMemory)
		{
			UnsafeNativeMethods.sqlite3_free(pMemory);
		}
	}
	internal static class SQLiteString
	{
		private static int ThirtyBits = 1073741823;

		private static readonly Encoding Utf8Encoding = Encoding.UTF8;

		public static byte[] GetUtf8BytesFromString(string value)
		{
			if (value == null)
			{
				return null;
			}
			return Utf8Encoding.GetBytes(value);
		}

		public static string GetStringFromUtf8Bytes(byte[] bytes)
		{
			if (bytes == null)
			{
				return null;
			}
			return Utf8Encoding.GetString(bytes);
		}

		public static int ProbeForUtf8ByteLength(IntPtr pValue, int limit)
		{
			int i = 0;
			if (pValue != IntPtr.Zero && limit > 0)
			{
				for (; Marshal.ReadByte(pValue, i) != 0 && i < limit; i++)
				{
				}
			}
			return i;
		}

		public static string StringFromUtf8IntPtr(IntPtr pValue)
		{
			return StringFromUtf8IntPtr(pValue, ProbeForUtf8ByteLength(pValue, ThirtyBits));
		}

		public static string StringFromUtf8IntPtr(IntPtr pValue, int length)
		{
			if (pValue == IntPtr.Zero)
			{
				return null;
			}
			if (length > 0)
			{
				byte[] array = new byte[length];
				Marshal.Copy(pValue, array, 0, length);
				return GetStringFromUtf8Bytes(array);
			}
			return string.Empty;
		}

		public static IntPtr Utf8IntPtrFromString(string value)
		{
			if (value == null)
			{
				return IntPtr.Zero;
			}
			IntPtr zero = IntPtr.Zero;
			byte[] utf8BytesFromString = GetUtf8BytesFromString(value);
			if (utf8BytesFromString == null)
			{
				return IntPtr.Zero;
			}
			int num = utf8BytesFromString.Length;
			zero = SQLiteMemory.Allocate(num + 1);
			if (zero == IntPtr.Zero)
			{
				return IntPtr.Zero;
			}
			Marshal.Copy(utf8BytesFromString, 0, zero, num);
			Marshal.WriteByte(zero, num, 0);
			return zero;
		}

		public static string[] StringArrayFromUtf8SizeAndIntPtr(int argc, IntPtr argv)
		{
			if (argc < 0)
			{
				return null;
			}
			if (argv == IntPtr.Zero)
			{
				return null;
			}
			string[] array = new string[argc];
			int num = 0;
			int num2 = 0;
			while (num < array.Length)
			{
				IntPtr intPtr = SQLiteMarshal.ReadIntPtr(argv, num2);
				array[num] = ((intPtr != IntPtr.Zero) ? StringFromUtf8IntPtr(intPtr) : null);
				num++;
				num2 += IntPtr.Size;
			}
			return array;
		}

		public static IntPtr[] Utf8IntPtrArrayFromStringArray(string[] values)
		{
			if (values == null)
			{
				return null;
			}
			IntPtr[] array = new IntPtr[values.Length];
			for (int i = 0; i < array.Length; i++)
			{
				ref IntPtr reference = ref array[i];
				reference = Utf8IntPtrFromString(values[i]);
			}
			return array;
		}
	}
	internal static class SQLiteBytes
	{
		public static byte[] FromIntPtr(IntPtr pValue, int length)
		{
			if (pValue == IntPtr.Zero)
			{
				return null;
			}
			if (length == 0)
			{
				return new byte[0];
			}
			byte[] array = new byte[length];
			Marshal.Copy(pValue, array, 0, length);
			return array;
		}

		public static IntPtr ToIntPtr(byte[] value)
		{
			if (value == null)
			{
				return IntPtr.Zero;
			}
			int num = value.Length;
			if (num == 0)
			{
				return IntPtr.Zero;
			}
			IntPtr intPtr = SQLiteMemory.Allocate(num);
			if (intPtr == IntPtr.Zero)
			{
				return IntPtr.Zero;
			}
			Marshal.Copy(value, 0, intPtr, num);
			return intPtr;
		}
	}
	internal static class SQLiteMarshal
	{
		public static IntPtr IntPtrForOffset(IntPtr pointer, int offset)
		{
			return new IntPtr(pointer.ToInt64() + offset);
		}

		public static int RoundUp(int size, int alignment)
		{
			int num = alignment - 1;
			return (size + num) & ~num;
		}

		public static int NextOffsetOf(int offset, int size, int alignment)
		{
			return RoundUp(offset + size, alignment);
		}

		public static int ReadInt32(IntPtr pointer, int offset)
		{
			return Marshal.ReadInt32(pointer, offset);
		}

		public static double ReadDouble(IntPtr pointer, int offset)
		{
			return BitConverter.Int64BitsToDouble(Marshal.ReadInt64(pointer, offset));
		}

		public static IntPtr ReadIntPtr(IntPtr pointer, int offset)
		{
			return Marshal.ReadIntPtr(pointer, offset);
		}

		public static void WriteInt32(IntPtr pointer, int offset, int value)
		{
			Marshal.WriteInt32(pointer, offset, value);
		}

		public static void WriteInt64(IntPtr pointer, int offset, long value)
		{
			Marshal.WriteInt64(pointer, offset, value);
		}

		public static void WriteDouble(IntPtr pointer, int offset, double value)
		{
			Marshal.WriteInt64(pointer, offset, BitConverter.DoubleToInt64Bits(value));
		}

		public static void WriteIntPtr(IntPtr pointer, int offset, IntPtr value)
		{
			Marshal.WriteIntPtr(pointer, offset, value);
		}

		public static int GetHashCode(object value, bool identity)
		{
			if (identity)
			{
				return RuntimeHelpers.GetHashCode(value);
			}
			return value?.GetHashCode() ?? 0;
		}
	}
	public abstract class SQLiteModule : ISQLiteManagedModule, IDisposable
	{
		private sealed class SQLiteNativeModule : ISQLiteNativeModule, IDisposable
		{
			private const bool DefaultLogErrors = true;

			private const bool DefaultLogExceptions = true;

			private const string ModuleNotAvailableErrorMessage = "native module implementation not available";

			private SQLiteModule module;

			private bool disposed;

			public SQLiteNativeModule(SQLiteModule module)
			{
				this.module = module;
			}

			private static SQLiteErrorCode ModuleNotAvailableTableError(IntPtr pVtab)
			{
				SetTableError(null, pVtab, logErrors: true, logExceptions: true, "native module implementation not available");
				return SQLiteErrorCode.Error;
			}

			private static SQLiteErrorCode ModuleNotAvailableCursorError(IntPtr pCursor)
			{
				SetCursorError(null, pCursor, logErrors: true, logExceptions: true, "native module implementation not available");
				return SQLiteErrorCode.Error;
			}

			public SQLiteErrorCode xCreate(IntPtr pDb, IntPtr pAux, int argc, IntPtr argv, ref IntPtr pVtab, ref IntPtr pError)
			{
				if (module == null)
				{
					pError = SQLiteString.Utf8IntPtrFromString("native module implementation not available");
					return SQLiteErrorCode.Error;
				}
				return module.xCreate(pDb, pAux, argc, argv, ref pVtab, ref pError);
			}

			public SQLiteErrorCode xConnect(IntPtr pDb, IntPtr pAux, int argc, IntPtr argv, ref IntPtr pVtab, ref IntPtr pError)
			{
				if (module == null)
				{
					pError = SQLiteString.Utf8IntPtrFromString("native module implementation not available");
					return SQLiteErrorCode.Error;
				}
				return module.xConnect(pDb, pAux, argc, argv, ref pVtab, ref pError);
			}

			public SQLiteErrorCode xBestIndex(IntPtr pVtab, IntPtr pIndex)
			{
				if (module == null)
				{
					return ModuleNotAvailableTableError(pVtab);
				}
				return module.xBestIndex(pVtab, pIndex);
			}

			public SQLiteErrorCode xDisconnect(IntPtr pVtab)
			{
				if (module == null)
				{
					return ModuleNotAvailableTableError(pVtab);
				}
				return module.xDisconnect(pVtab);
			}

			public SQLiteErrorCode xDestroy(IntPtr pVtab)
			{
				if (module == null)
				{
					return ModuleNotAvailableTableError(pVtab);
				}
				return module.xDestroy(pVtab);
			}

			public SQLiteErrorCode xOpen(IntPtr pVtab, ref IntPtr pCursor)
			{
				if (module == null)
				{
					return ModuleNotAvailableTableError(pVtab);
				}
				return module.xOpen(pVtab, ref pCursor);
			}

			public SQLiteErrorCode xClose(IntPtr pCursor)
			{
				if (module == null)
				{
					return ModuleNotAvailableCursorError(pCursor);
				}
				return module.xClose(pCursor);
			}

			public SQLiteErrorCode xFilter(IntPtr pCursor, int idxNum, IntPtr idxStr, int argc, IntPtr argv)
			{
				if (module == null)
				{
					return ModuleNotAvailableCursorError(pCursor);
				}
				return module.xFilter(pCursor, idxNum, idxStr, argc, argv);
			}

			public SQLiteErrorCode xNext(IntPtr pCursor)
			{
				if (module == null)
				{
					return ModuleNotAvailableCursorError(pCursor);
				}
				return module.xNext(pCursor);
			}

			public int xEof(IntPtr pCursor)
			{
				if (module == null)
				{
					ModuleNotAvailableCursorError(pCursor);
					return 1;
				}
				return module.xEof(pCursor);
			}

			public SQLiteErrorCode xColumn(IntPtr pCursor, IntPtr pContext, int index)
			{
				if (module == null)
				{
					return ModuleNotAvailableCursorError(pCursor);
				}
				return module.xColumn(pCursor, pContext, index);
			}

			public SQLiteErrorCode xRowId(IntPtr pCursor, ref long rowId)
			{
				if (module == null)
				{
					return ModuleNotAvailableCursorError(pCursor);
				}
				return module.xRowId(pCursor, ref rowId);
			}

			public SQLiteErrorCode xUpdate(IntPtr pVtab, int argc, IntPtr argv, ref long rowId)
			{
				if (module == null)
				{
					return ModuleNotAvailableTableError(pVtab);
				}
				return module.xUpdate(pVtab, argc, argv, ref rowId);
			}

			public SQLiteErrorCode xBegin(IntPtr pVtab)
			{
				if (module == null)
				{
					return ModuleNotAvailableTableError(pVtab);
				}
				return module.xBegin(pVtab);
			}

			public SQLiteErrorCode xSync(IntPtr pVtab)
			{
				if (module == null)
				{
					return ModuleNotAvailableTableError(pVtab);
				}
				return module.xSync(pVtab);
			}

			public SQLiteErrorCode xCommit(IntPtr pVtab)
			{
				if (module == null)
				{
					return ModuleNotAvailableTableError(pVtab);
				}
				return module.xCommit(pVtab);
			}

			public SQLiteErrorCode xRollback(IntPtr pVtab)
			{
				if (module == null)
				{
					return ModuleNotAvailableTableError(pVtab);
				}
				return module.xRollback(pVtab);
			}

			public int xFindFunction(IntPtr pVtab, int nArg, IntPtr zName, ref SQLiteCallback callback, ref IntPtr pClientData)
			{
				if (module == null)
				{
					ModuleNotAvailableTableError(pVtab);
					return 0;
				}
				return module.xFindFunction(pVtab, nArg, zName, ref callback, ref pClientData);
			}

			public SQLiteErrorCode xRename(IntPtr pVtab, IntPtr zNew)
			{
				if (module == null)
				{
					return ModuleNotAvailableTableError(pVtab);
				}
				return module.xRename(pVtab, zNew);
			}

			public SQLiteErrorCode xSavepoint(IntPtr pVtab, int iSavepoint)
			{
				if (module == null)
				{
					return ModuleNotAvailableTableError(pVtab);
				}
				return module.xSavepoint(pVtab, iSavepoint);
			}

			public SQLiteErrorCode xRelease(IntPtr pVtab, int iSavepoint)
			{
				if (module == null)
				{
					return ModuleNotAvailableTableError(pVtab);
				}
				return module.xRelease(pVtab, iSavepoint);
			}

			public SQLiteErrorCode xRollbackTo(IntPtr pVtab, int iSavepoint)
			{
				if (module == null)
				{
					return ModuleNotAvailableTableError(pVtab);
				}
				return module.xRollbackTo(pVtab, iSavepoint);
			}

			public void Dispose()
			{
				Dispose(disposing: true);
				GC.SuppressFinalize(this);
			}

			private void CheckDisposed()
			{
				if (disposed)
				{
					throw new ObjectDisposedException(typeof(SQLiteNativeModule).Name);
				}
			}

			private void Dispose(bool disposing)
			{
				if (!disposed)
				{
					if (module != null)
					{
						module = null;
					}
					disposed = true;
				}
			}

			~SQLiteNativeModule()
			{
				Dispose(disposing: false);
			}
		}

		private static readonly int DefaultModuleVersion = 2;

		private UnsafeNativeMethods.sqlite3_module nativeModule;

		private UnsafeNativeMethods.xDestroyModule destroyModule;

		private IntPtr disposableModule;

		private Dictionary<IntPtr, SQLiteVirtualTable> tables;

		private Dictionary<IntPtr, SQLiteVirtualTableCursor> cursors;

		private Dictionary<string, SQLiteFunction> functions;

		private bool logErrors;

		private bool logExceptions;

		private bool declared;

		private string name;

		private bool disposed;

		protected virtual bool LogErrorsNoThrow
		{
			get
			{
				return logErrors;
			}
			set
			{
				logErrors = value;
			}
		}

		protected virtual bool LogExceptionsNoThrow
		{
			get
			{
				return logExceptions;
			}
			set
			{
				logExceptions = value;
			}
		}

		public virtual bool LogErrors
		{
			get
			{
				CheckDisposed();
				return LogErrorsNoThrow;
			}
			set
			{
				CheckDisposed();
				LogErrorsNoThrow = value;
			}
		}

		public virtual bool LogExceptions
		{
			get
			{
				CheckDisposed();
				return LogExceptionsNoThrow;
			}
			set
			{
				CheckDisposed();
				LogExceptionsNoThrow = value;
			}
		}

		public virtual bool Declared
		{
			get
			{
				CheckDisposed();
				return declared;
			}
			internal set
			{
				declared = value;
			}
		}

		public virtual string Name
		{
			get
			{
				CheckDisposed();
				return name;
			}
		}

		public SQLiteModule(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.name = name;
			tables = new Dictionary<IntPtr, SQLiteVirtualTable>();
			cursors = new Dictionary<IntPtr, SQLiteVirtualTableCursor>();
			functions = new Dictionary<string, SQLiteFunction>();
		}

		internal bool CreateDisposableModule(IntPtr pDb)
		{
			if (disposableModule != IntPtr.Zero)
			{
				return true;
			}
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = SQLiteString.Utf8IntPtrFromString(name);
				UnsafeNativeMethods.sqlite3_module module = AllocateNativeModule();
				destroyModule = xDestroyModule;
				disposableModule = UnsafeNativeMethods.sqlite3_create_disposable_module(pDb, intPtr, ref module, IntPtr.Zero, destroyModule);
				return disposableModule != IntPtr.Zero;
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					SQLiteMemory.Free(intPtr);
					intPtr = IntPtr.Zero;
				}
			}
		}

		private void xDestroyModule(IntPtr pClientData)
		{
			disposableModule = IntPtr.Zero;
		}

		private UnsafeNativeMethods.sqlite3_module AllocateNativeModule()
		{
			return AllocateNativeModule(GetNativeModuleImpl());
		}

		private UnsafeNativeMethods.sqlite3_module AllocateNativeModule(ISQLiteNativeModule module)
		{
			nativeModule = default(UnsafeNativeMethods.sqlite3_module);
			nativeModule.iVersion = DefaultModuleVersion;
			if (module != null)
			{
				nativeModule.xCreate = module.xCreate;
				nativeModule.xConnect = module.xConnect;
				nativeModule.xBestIndex = module.xBestIndex;
				nativeModule.xDisconnect = module.xDisconnect;
				nativeModule.xDestroy = module.xDestroy;
				nativeModule.xOpen = module.xOpen;
				nativeModule.xClose = module.xClose;
				nativeModule.xFilter = module.xFilter;
				nativeModule.xNext = module.xNext;
				nativeModule.xEof = module.xEof;
				nativeModule.xColumn = module.xColumn;
				nativeModule.xRowId = module.xRowId;
				nativeModule.xUpdate = module.xUpdate;
				nativeModule.xBegin = module.xBegin;
				nativeModule.xSync = module.xSync;
				nativeModule.xCommit = module.xCommit;
				nativeModule.xRollback = module.xRollback;
				nativeModule.xFindFunction = module.xFindFunction;
				nativeModule.xRename = module.xRename;
				nativeModule.xSavepoint = module.xSavepoint;
				nativeModule.xRelease = module.xRelease;
				nativeModule.xRollbackTo = module.xRollbackTo;
			}
			else
			{
				nativeModule.xCreate = xCreate;
				nativeModule.xConnect = xConnect;
				nativeModule.xBestIndex = xBestIndex;
				nativeModule.xDisconnect = xDisconnect;
				nativeModule.xDestroy = xDestroy;
				nativeModule.xOpen = xOpen;
				nativeModule.xClose = xClose;
				nativeModule.xFilter = xFilter;
				nativeModule.xNext = xNext;
				nativeModule.xEof = xEof;
				nativeModule.xColumn = xColumn;
				nativeModule.xRowId = xRowId;
				nativeModule.xUpdate = xUpdate;
				nativeModule.xBegin = xBegin;
				nativeModule.xSync = xSync;
				nativeModule.xCommit = xCommit;
				nativeModule.xRollback = xRollback;
				nativeModule.xFindFunction = xFindFunction;
				nativeModule.xRename = xRename;
				nativeModule.xSavepoint = xSavepoint;
				nativeModule.xRelease = xRelease;
				nativeModule.xRollbackTo = xRollbackTo;
			}
			return nativeModule;
		}

		private UnsafeNativeMethods.sqlite3_module CopyNativeModule(UnsafeNativeMethods.sqlite3_module module)
		{
			return new UnsafeNativeMethods.sqlite3_module
			{
				iVersion = module.iVersion,
				xCreate = ((module.xCreate != null) ? module.xCreate : new UnsafeNativeMethods.xCreate(xCreate)).Invoke,
				xConnect = ((module.xConnect != null) ? module.xConnect : new UnsafeNativeMethods.xConnect(xConnect)).Invoke,
				xBestIndex = ((module.xBestIndex != null) ? module.xBestIndex : new UnsafeNativeMethods.xBestIndex(xBestIndex)).Invoke,
				xDisconnect = ((module.xDisconnect != null) ? module.xDisconnect : new UnsafeNativeMethods.xDisconnect(xDisconnect)).Invoke,
				xDestroy = ((module.xDestroy != null) ? module.xDestroy : new UnsafeNativeMethods.xDestroy(xDestroy)).Invoke,
				xOpen = ((module.xOpen != null) ? module.xOpen : new UnsafeNativeMethods.xOpen(xOpen)).Invoke,
				xClose = ((module.xClose != null) ? module.xClose : new UnsafeNativeMethods.xClose(xClose)).Invoke,
				xFilter = ((module.xFilter != null) ? module.xFilter : new UnsafeNativeMethods.xFilter(xFilter)).Invoke,
				xNext = ((module.xNext != null) ? module.xNext : new UnsafeNativeMethods.xNext(xNext)).Invoke,
				xEof = ((module.xEof != null) ? module.xEof : new UnsafeNativeMethods.xEof(xEof)).Invoke,
				xColumn = ((module.xColumn != null) ? module.xColumn : new UnsafeNativeMethods.xColumn(xColumn)).Invoke,
				xRowId = ((module.xRowId != null) ? module.xRowId : new UnsafeNativeMethods.xRowId(xRowId)).Invoke,
				xUpdate = ((module.xUpdate != null) ? module.xUpdate : new UnsafeNativeMethods.xUpdate(xUpdate)).Invoke,
				xBegin = ((module.xBegin != null) ? module.xBegin : new UnsafeNativeMethods.xBegin(xBegin)).Invoke,
				xSync = ((module.xSync != null) ? module.xSync : new UnsafeNativeMethods.xSync(xSync)).Invoke,
				xCommit = ((module.xCommit != null) ? module.xCommit : new UnsafeNativeMethods.xCommit(xCommit)).Invoke,
				xRollback = ((module.xRollback != null) ? module.xRollback : new UnsafeNativeMethods.xRollback(xRollback)).Invoke,
				xFindFunction = ((module.xFindFunction != null) ? module.xFindFunction : new UnsafeNativeMethods.xFindFunction(xFindFunction)).Invoke,
				xRename = ((module.xRename != null) ? module.xRename : new UnsafeNativeMethods.xRename(xRename)).Invoke,
				xSavepoint = ((module.xSavepoint != null) ? module.xSavepoint : new UnsafeNativeMethods.xSavepoint(xSavepoint)).Invoke,
				xRelease = ((module.xRelease != null) ? module.xRelease : new UnsafeNativeMethods.xRelease(xRelease)).Invoke,
				xRollbackTo = ((module.xRollbackTo != null) ? module.xRollbackTo : new UnsafeNativeMethods.xRollbackTo(xRollbackTo)).Invoke
			};
		}

		private SQLiteErrorCode CreateOrConnect(bool create, IntPtr pDb, IntPtr pAux, int argc, IntPtr argv, ref IntPtr pVtab, ref IntPtr pError)
		{
			try
			{
				string fileName = SQLiteString.StringFromUtf8IntPtr(UnsafeNativeMethods.sqlite3_db_filename(pDb, IntPtr.Zero));
				using SQLiteConnection connection = new SQLiteConnection(pDb, fileName, ownHandle: false);
				SQLiteVirtualTable table = null;
				string error = null;
				if ((create && Create(connection, pAux, SQLiteString.StringArrayFromUtf8SizeAndIntPtr(argc, argv), ref table, ref error) == SQLiteErrorCode.Ok) || (!create && Connect(connection, pAux, SQLiteString.StringArrayFromUtf8SizeAndIntPtr(argc, argv), ref table, ref error) == SQLiteErrorCode.Ok))
				{
					if (table != null)
					{
						pVtab = TableToIntPtr(table);
						return SQLiteErrorCode.Ok;
					}
					pError = SQLiteString.Utf8IntPtrFromString("no table was created");
				}
				else
				{
					pError = SQLiteString.Utf8IntPtrFromString(error);
				}
			}
			catch (Exception ex)
			{
				pError = SQLiteString.Utf8IntPtrFromString(ex.ToString());
			}
			return SQLiteErrorCode.Error;
		}

		private SQLiteErrorCode DestroyOrDisconnect(bool destroy, IntPtr pVtab)
		{
			try
			{
				SQLiteVirtualTable sQLiteVirtualTable = TableFromIntPtr(pVtab);
				if (sQLiteVirtualTable != null && ((destroy && Destroy(sQLiteVirtualTable) == SQLiteErrorCode.Ok) || (!destroy && Disconnect(sQLiteVirtualTable) == SQLiteErrorCode.Ok)))
				{
					if (tables != null)
					{
						tables.Remove(pVtab);
					}
					return SQLiteErrorCode.Ok;
				}
			}
			catch (Exception ex)
			{
				try
				{
					if (LogExceptionsNoThrow)
					{
						SQLiteLog.LogMessage(-2146233088, string.Format(CultureInfo.CurrentCulture, "Caught exception in \"{0}\" method: {1}", new object[2]
						{
							destroy ? "xDestroy" : "xDisconnect",
							ex
						}));
					}
				}
				catch
				{
				}
			}
			finally
			{
				FreeTable(pVtab);
			}
			return SQLiteErrorCode.Error;
		}

		private static bool SetTableError(SQLiteModule module, IntPtr pVtab, bool logErrors, bool logExceptions, string error)
		{
			try
			{
				if (logErrors)
				{
					SQLiteLog.LogMessage(SQLiteErrorCode.Error, string.Format(CultureInfo.CurrentCulture, "Virtual table error: {0}", new object[1] { error }));
				}
			}
			catch
			{
			}
			bool flag = false;
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				if (pVtab == IntPtr.Zero)
				{
					return false;
				}
				int offset = 0;
				offset = SQLiteMarshal.NextOffsetOf(offset, IntPtr.Size, 4);
				offset = SQLiteMarshal.NextOffsetOf(offset, 4, IntPtr.Size);
				IntPtr intPtr2 = SQLiteMarshal.ReadIntPtr(pVtab, offset);
				if (intPtr2 != IntPtr.Zero)
				{
					SQLiteMemory.Free(intPtr2);
					intPtr2 = IntPtr.Zero;
					SQLiteMarshal.WriteIntPtr(pVtab, offset, intPtr2);
				}
				if (error == null)
				{
					return true;
				}
				intPtr = SQLiteString.Utf8IntPtrFromString(error);
				SQLiteMarshal.WriteIntPtr(pVtab, offset, intPtr);
				flag = true;
			}
			catch (Exception ex)
			{
				try
				{
					if (logExceptions)
					{
						SQLiteLog.LogMessage(-2146233088, string.Format(CultureInfo.CurrentCulture, "Caught exception in \"SetTableError\" method: {0}", new object[1] { ex }));
					}
				}
				catch
				{
				}
			}
			finally
			{
				if (!flag && intPtr != IntPtr.Zero)
				{
					SQLiteMemory.Free(intPtr);
					intPtr = IntPtr.Zero;
				}
			}
			return flag;
		}

		private static bool SetTableError(SQLiteModule module, SQLiteVirtualTable table, bool logErrors, bool logExceptions, string error)
		{
			if (table == null)
			{
				return false;
			}
			IntPtr nativeHandle = table.NativeHandle;
			if (nativeHandle == IntPtr.Zero)
			{
				return false;
			}
			return SetTableError(module, nativeHandle, logErrors, logExceptions, error);
		}

		private static bool SetCursorError(SQLiteModule module, IntPtr pCursor, bool logErrors, bool logExceptions, string error)
		{
			if (pCursor == IntPtr.Zero)
			{
				return false;
			}
			IntPtr intPtr = TableFromCursor(module, pCursor);
			if (intPtr == IntPtr.Zero)
			{
				return false;
			}
			return SetTableError(module, intPtr, logErrors, logExceptions, error);
		}

		private static bool SetCursorError(SQLiteModule module, SQLiteVirtualTableCursor cursor, bool logErrors, bool logExceptions, string error)
		{
			if (cursor == null)
			{
				return false;
			}
			IntPtr nativeHandle = cursor.NativeHandle;
			if (nativeHandle == IntPtr.Zero)
			{
				return false;
			}
			return SetCursorError(module, nativeHandle, logErrors, logExceptions, error);
		}

		protected virtual ISQLiteNativeModule GetNativeModuleImpl()
		{
			return null;
		}

		protected virtual ISQLiteNativeModule CreateNativeModuleImpl()
		{
			return new SQLiteNativeModule(this);
		}

		protected virtual IntPtr AllocateTable()
		{
			int size = Marshal.SizeOf(typeof(UnsafeNativeMethods.sqlite3_vtab));
			return SQLiteMemory.Allocate(size);
		}

		protected virtual void ZeroTable(IntPtr pVtab)
		{
			if (!(pVtab == IntPtr.Zero))
			{
				int offset = 0;
				SQLiteMarshal.WriteIntPtr(pVtab, offset, IntPtr.Zero);
				offset = SQLiteMarshal.NextOffsetOf(offset, IntPtr.Size, 4);
				SQLiteMarshal.WriteInt32(pVtab, offset, 0);
				offset = SQLiteMarshal.NextOffsetOf(offset, 4, IntPtr.Size);
				SQLiteMarshal.WriteIntPtr(pVtab, offset, IntPtr.Zero);
			}
		}

		protected virtual void FreeTable(IntPtr pVtab)
		{
			SetTableError(pVtab, null);
			SQLiteMemory.Free(pVtab);
		}

		protected virtual IntPtr AllocateCursor()
		{
			int size = Marshal.SizeOf(typeof(UnsafeNativeMethods.sqlite3_vtab_cursor));
			return SQLiteMemory.Allocate(size);
		}

		protected virtual void FreeCursor(IntPtr pCursor)
		{
			SQLiteMemory.Free(pCursor);
		}

		private static IntPtr TableFromCursor(SQLiteModule module, IntPtr pCursor)
		{
			if (pCursor == IntPtr.Zero)
			{
				return IntPtr.Zero;
			}
			return Marshal.ReadIntPtr(pCursor);
		}

		protected virtual IntPtr TableFromCursor(IntPtr pCursor)
		{
			return TableFromCursor(this, pCursor);
		}

		protected virtual SQLiteVirtualTable TableFromIntPtr(IntPtr pVtab)
		{
			if (pVtab == IntPtr.Zero)
			{
				SetTableError(pVtab, "invalid native table");
				return null;
			}
			if (tables != null && tables.TryGetValue(pVtab, out var value))
			{
				return value;
			}
			SetTableError(pVtab, string.Format(CultureInfo.CurrentCulture, "managed table for {0} not found", new object[1] { pVtab }));
			return null;
		}

		protected virtual IntPtr TableToIntPtr(SQLiteVirtualTable table)
		{
			if (table == null || tables == null)
			{
				return IntPtr.Zero;
			}
			IntPtr intPtr = IntPtr.Zero;
			bool flag = false;
			try
			{
				intPtr = AllocateTable();
				if (intPtr != IntPtr.Zero)
				{
					ZeroTable(intPtr);
					table.NativeHandle = intPtr;
					tables.Add(intPtr, table);
					flag = true;
				}
			}
			finally
			{
				if (!flag && intPtr != IntPtr.Zero)
				{
					FreeTable(intPtr);
					intPtr = IntPtr.Zero;
				}
			}
			return intPtr;
		}

		protected virtual SQLiteVirtualTableCursor CursorFromIntPtr(IntPtr pVtab, IntPtr pCursor)
		{
			if (pCursor == IntPtr.Zero)
			{
				SetTableError(pVtab, "invalid native cursor");
				return null;
			}
			if (cursors != null && cursors.TryGetValue(pCursor, out var value))
			{
				return value;
			}
			SetTableError(pVtab, string.Format(CultureInfo.CurrentCulture, "managed cursor for {0} not found", new object[1] { pCursor }));
			return null;
		}

		protected virtual IntPtr CursorToIntPtr(SQLiteVirtualTableCursor cursor)
		{
			if (cursor == null || cursors == null)
			{
				return IntPtr.Zero;
			}
			IntPtr intPtr = IntPtr.Zero;
			bool flag = false;
			try
			{
				intPtr = AllocateCursor();
				if (intPtr != IntPtr.Zero)
				{
					cursor.NativeHandle = intPtr;
					cursors.Add(intPtr, cursor);
					flag = true;
				}
			}
			finally
			{
				if (!flag && intPtr != IntPtr.Zero)
				{
					FreeCursor(intPtr);
					intPtr = IntPtr.Zero;
				}
			}
			return intPtr;
		}

		protected virtual string GetFunctionKey(int argumentCount, string name, SQLiteFunction function)
		{
			return $"{argumentCount}:{name}";
		}

		protected virtual SQLiteErrorCode DeclareTable(SQLiteConnection connection, string sql, ref string error)
		{
			if (connection == null)
			{
				error = "invalid connection";
				return SQLiteErrorCode.Error;
			}
			SQLiteBase sql2 = connection._sql;
			if (sql2 == null)
			{
				error = "connection has invalid handle";
				return SQLiteErrorCode.Error;
			}
			if (sql == null)
			{
				error = "invalid SQL statement";
				return SQLiteErrorCode.Error;
			}
			return sql2.DeclareVirtualTable(this, sql, ref error);
		}

		protected virtual SQLiteErrorCode DeclareFunction(SQLiteConnection connection, int argumentCount, string name, ref string error)
		{
			if (connection == null)
			{
				error = "invalid connection";
				return SQLiteErrorCode.Error;
			}
			SQLiteBase sql = connection._sql;
			if (sql == null)
			{
				error = "connection has invalid handle";
				return SQLiteErrorCode.Error;
			}
			return sql.DeclareVirtualFunction(this, argumentCount, name, ref error);
		}

		protected virtual bool SetTableError(IntPtr pVtab, string error)
		{
			return SetTableError(this, pVtab, LogErrorsNoThrow, LogExceptionsNoThrow, error);
		}

		protected virtual bool SetTableError(SQLiteVirtualTable table, string error)
		{
			return SetTableError(this, table, LogErrorsNoThrow, LogExceptionsNoThrow, error);
		}

		protected virtual bool SetCursorError(SQLiteVirtualTableCursor cursor, string error)
		{
			return SetCursorError(this, cursor, LogErrorsNoThrow, LogExceptionsNoThrow, error);
		}

		protected virtual bool SetEstimatedCost(SQLiteIndex index, double? estimatedCost)
		{
			if (index == null || index.Outputs == null)
			{
				return false;
			}
			index.Outputs.EstimatedCost = estimatedCost;
			return true;
		}

		protected virtual bool SetEstimatedCost(SQLiteIndex index)
		{
			return SetEstimatedCost(index, null);
		}

		protected virtual bool SetEstimatedRows(SQLiteIndex index, long? estimatedRows)
		{
			if (index == null || index.Outputs == null)
			{
				return false;
			}
			index.Outputs.EstimatedRows = estimatedRows;
			return true;
		}

		protected virtual bool SetEstimatedRows(SQLiteIndex index)
		{
			return SetEstimatedRows(index, null);
		}

		private SQLiteErrorCode xCreate(IntPtr pDb, IntPtr pAux, int argc, IntPtr argv, ref IntPtr pVtab, ref IntPtr pError)
		{
			return CreateOrConnect(create: true, pDb, pAux, argc, argv, ref pVtab, ref pError);
		}

		private SQLiteErrorCode xConnect(IntPtr pDb, IntPtr pAux, int argc, IntPtr argv, ref IntPtr pVtab, ref IntPtr pError)
		{
			return CreateOrConnect(create: false, pDb, pAux, argc, argv, ref pVtab, ref pError);
		}

		private SQLiteErrorCode xBestIndex(IntPtr pVtab, IntPtr pIndex)
		{
			try
			{
				SQLiteVirtualTable sQLiteVirtualTable = TableFromIntPtr(pVtab);
				if (sQLiteVirtualTable != null)
				{
					SQLiteIndex index = null;
					SQLiteIndex.FromIntPtr(pIndex, ref index);
					if (BestIndex(sQLiteVirtualTable, index) == SQLiteErrorCode.Ok)
					{
						SQLiteIndex.ToIntPtr(index, pIndex);
						return SQLiteErrorCode.Ok;
					}
				}
			}
			catch (Exception ex)
			{
				SetTableError(pVtab, ex.ToString());
			}
			return SQLiteErrorCode.Error;
		}

		private SQLiteErrorCode xDisconnect(IntPtr pVtab)
		{
			return DestroyOrDisconnect(destroy: false, pVtab);
		}

		private SQLiteErrorCode xDestroy(IntPtr pVtab)
		{
			return DestroyOrDisconnect(destroy: true, pVtab);
		}

		private SQLiteErrorCode xOpen(IntPtr pVtab, ref IntPtr pCursor)
		{
			try
			{
				SQLiteVirtualTable sQLiteVirtualTable = TableFromIntPtr(pVtab);
				if (sQLiteVirtualTable != null)
				{
					SQLiteVirtualTableCursor cursor = null;
					if (Open(sQLiteVirtualTable, ref cursor) == SQLiteErrorCode.Ok)
					{
						if (cursor != null)
						{
							pCursor = CursorToIntPtr(cursor);
							if (pCursor != IntPtr.Zero)
							{
								return SQLiteErrorCode.Ok;
							}
							SetTableError(pVtab, "no native cursor was created");
						}
						else
						{
							SetTableError(pVtab, "no managed cursor was created");
						}
					}
				}
			}
			catch (Exception ex)
			{
				SetTableError(pVtab, ex.ToString());
			}
			return SQLiteErrorCode.Error;
		}

		private SQLiteErrorCode xClose(IntPtr pCursor)
		{
			IntPtr pVtab = IntPtr.Zero;
			try
			{
				pVtab = TableFromCursor(pCursor);
				SQLiteVirtualTableCursor sQLiteVirtualTableCursor = CursorFromIntPtr(pVtab, pCursor);
				if (sQLiteVirtualTableCursor != null && Close(sQLiteVirtualTableCursor) == SQLiteErrorCode.Ok)
				{
					if (cursors != null)
					{
						cursors.Remove(pCursor);
					}
					return SQLiteErrorCode.Ok;
				}
			}
			catch (Exception ex)
			{
				SetTableError(pVtab, ex.ToString());
			}
			finally
			{
				FreeCursor(pCursor);
			}
			return SQLiteErrorCode.Error;
		}

		private SQLiteErrorCode xFilter(IntPtr pCursor, int idxNum, IntPtr idxStr, int argc, IntPtr argv)
		{
			IntPtr pVtab = IntPtr.Zero;
			try
			{
				pVtab = TableFromCursor(pCursor);
				SQLiteVirtualTableCursor sQLiteVirtualTableCursor = CursorFromIntPtr(pVtab, pCursor);
				if (sQLiteVirtualTableCursor != null && Filter(sQLiteVirtualTableCursor, idxNum, SQLiteString.StringFromUtf8IntPtr(idxStr), SQLiteValue.ArrayFromSizeAndIntPtr(argc, argv)) == SQLiteErrorCode.Ok)
				{
					return SQLiteErrorCode.Ok;
				}
			}
			catch (Exception ex)
			{
				SetTableError(pVtab, ex.ToString());
			}
			return SQLiteErrorCode.Error;
		}

		private SQLiteErrorCode xNext(IntPtr pCursor)
		{
			IntPtr pVtab = IntPtr.Zero;
			try
			{
				pVtab = TableFromCursor(pCursor);
				SQLiteVirtualTableCursor sQLiteVirtualTableCursor = CursorFromIntPtr(pVtab, pCursor);
				if (sQLiteVirtualTableCursor != null && Next(sQLiteVirtualTableCursor) == SQLiteErrorCode.Ok)
				{
					return SQLiteErrorCode.Ok;
				}
			}
			catch (Exception ex)
			{
				SetTableError(pVtab, ex.ToString());
			}
			return SQLiteErrorCode.Error;
		}

		private int xEof(IntPtr pCursor)
		{
			IntPtr pVtab = IntPtr.Zero;
			try
			{
				pVtab = TableFromCursor(pCursor);
				SQLiteVirtualTableCursor sQLiteVirtualTableCursor = CursorFromIntPtr(pVtab, pCursor);
				if (sQLiteVirtualTableCursor != null)
				{
					return Eof(sQLiteVirtualTableCursor) ? 1 : 0;
				}
			}
			catch (Exception ex)
			{
				SetTableError(pVtab, ex.ToString());
			}
			return 1;
		}

		private SQLiteErrorCode xColumn(IntPtr pCursor, IntPtr pContext, int index)
		{
			IntPtr pVtab = IntPtr.Zero;
			try
			{
				pVtab = TableFromCursor(pCursor);
				SQLiteVirtualTableCursor sQLiteVirtualTableCursor = CursorFromIntPtr(pVtab, pCursor);
				if (sQLiteVirtualTableCursor != null)
				{
					SQLiteContext context = new SQLiteContext(pContext);
					return Column(sQLiteVirtualTableCursor, context, index);
				}
			}
			catch (Exception ex)
			{
				SetTableError(pVtab, ex.ToString());
			}
			return SQLiteErrorCode.Error;
		}

		private SQLiteErrorCode xRowId(IntPtr pCursor, ref long rowId)
		{
			IntPtr pVtab = IntPtr.Zero;
			try
			{
				pVtab = TableFromCursor(pCursor);
				SQLiteVirtualTableCursor sQLiteVirtualTableCursor = CursorFromIntPtr(pVtab, pCursor);
				if (sQLiteVirtualTableCursor != null)
				{
					return RowId(sQLiteVirtualTableCursor, ref rowId);
				}
			}
			catch (Exception ex)
			{
				SetTableError(pVtab, ex.ToString());
			}
			return SQLiteErrorCode.Error;
		}

		private SQLiteErrorCode xUpdate(IntPtr pVtab, int argc, IntPtr argv, ref long rowId)
		{
			try
			{
				SQLiteVirtualTable sQLiteVirtualTable = TableFromIntPtr(pVtab);
				if (sQLiteVirtualTable != null)
				{
					return Update(sQLiteVirtualTable, SQLiteValue.ArrayFromSizeAndIntPtr(argc, argv), ref rowId);
				}
			}
			catch (Exception ex)
			{
				SetTableError(pVtab, ex.ToString());
			}
			return SQLiteErrorCode.Error;
		}

		private SQLiteErrorCode xBegin(IntPtr pVtab)
		{
			try
			{
				SQLiteVirtualTable sQLiteVirtualTable = TableFromIntPtr(pVtab);
				if (sQLiteVirtualTable != null)
				{
					return Begin(sQLiteVirtualTable);
				}
			}
			catch (Exception ex)
			{
				SetTableError(pVtab, ex.ToString());
			}
			return SQLiteErrorCode.Error;
		}

		private SQLiteErrorCode xSync(IntPtr pVtab)
		{
			try
			{
				SQLiteVirtualTable sQLiteVirtualTable = TableFromIntPtr(pVtab);
				if (sQLiteVirtualTable != null)
				{
					return Sync(sQLiteVirtualTable);
				}
			}
			catch (Exception ex)
			{
				SetTableError(pVtab, ex.ToString());
			}
			return SQLiteErrorCode.Error;
		}

		private SQLiteErrorCode xCommit(IntPtr pVtab)
		{
			try
			{
				SQLiteVirtualTable sQLiteVirtualTable = TableFromIntPtr(pVtab);
				if (sQLiteVirtualTable != null)
				{
					return Commit(sQLiteVirtualTable);
				}
			}
			catch (Exception ex)
			{
				SetTableError(pVtab, ex.ToString());
			}
			return SQLiteErrorCode.Error;
		}

		private SQLiteErrorCode xRollback(IntPtr pVtab)
		{
			try
			{
				SQLiteVirtualTable sQLiteVirtualTable = TableFromIntPtr(pVtab);
				if (sQLiteVirtualTable != null)
				{
					return Rollback(sQLiteVirtualTable);
				}
			}
			catch (Exception ex)
			{
				SetTableError(pVtab, ex.ToString());
			}
			return SQLiteErrorCode.Error;
		}

		private int xFindFunction(IntPtr pVtab, int nArg, IntPtr zName, ref SQLiteCallback callback, ref IntPtr pClientData)
		{
			try
			{
				SQLiteVirtualTable sQLiteVirtualTable = TableFromIntPtr(pVtab);
				if (sQLiteVirtualTable != null)
				{
					string text = SQLiteString.StringFromUtf8IntPtr(zName);
					SQLiteFunction function = null;
					if (FindFunction(sQLiteVirtualTable, nArg, text, ref function, ref pClientData))
					{
						if (function != null)
						{
							string functionKey = GetFunctionKey(nArg, text, function);
							functions[functionKey] = function;
							callback = function.ScalarCallback;
							return 1;
						}
						SetTableError(pVtab, "no function was created");
					}
				}
			}
			catch (Exception ex)
			{
				SetTableError(pVtab, ex.ToString());
			}
			return 0;
		}

		private SQLiteErrorCode xRename(IntPtr pVtab, IntPtr zNew)
		{
			try
			{
				SQLiteVirtualTable sQLiteVirtualTable = TableFromIntPtr(pVtab);
				if (sQLiteVirtualTable != null)
				{
					return Rename(sQLiteVirtualTable, SQLiteString.StringFromUtf8IntPtr(zNew));
				}
			}
			catch (Exception ex)
			{
				SetTableError(pVtab, ex.ToString());
			}
			return SQLiteErrorCode.Error;
		}

		private SQLiteErrorCode xSavepoint(IntPtr pVtab, int iSavepoint)
		{
			try
			{
				SQLiteVirtualTable sQLiteVirtualTable = TableFromIntPtr(pVtab);
				if (sQLiteVirtualTable != null)
				{
					return Savepoint(sQLiteVirtualTable, iSavepoint);
				}
			}
			catch (Exception ex)
			{
				SetTableError(pVtab, ex.ToString());
			}
			return SQLiteErrorCode.Error;
		}

		private SQLiteErrorCode xRelease(IntPtr pVtab, int iSavepoint)
		{
			try
			{
				SQLiteVirtualTable sQLiteVirtualTable = TableFromIntPtr(pVtab);
				if (sQLiteVirtualTable != null)
				{
					return Release(sQLiteVirtualTable, iSavepoint);
				}
			}
			catch (Exception ex)
			{
				SetTableError(pVtab, ex.ToString());
			}
			return SQLiteErrorCode.Error;
		}

		private SQLiteErrorCode xRollbackTo(IntPtr pVtab, int iSavepoint)
		{
			try
			{
				SQLiteVirtualTable sQLiteVirtualTable = TableFromIntPtr(pVtab);
				if (sQLiteVirtualTable != null)
				{
					return RollbackTo(sQLiteVirtualTable, iSavepoint);
				}
			}
			catch (Exception ex)
			{
				SetTableError(pVtab, ex.ToString());
			}
			return SQLiteErrorCode.Error;
		}

		public abstract SQLiteErrorCode Create(SQLiteConnection connection, IntPtr pClientData, string[] arguments, ref SQLiteVirtualTable table, ref string error);

		public abstract SQLiteErrorCode Connect(SQLiteConnection connection, IntPtr pClientData, string[] arguments, ref SQLiteVirtualTable table, ref string error);

		public abstract SQLiteErrorCode BestIndex(SQLiteVirtualTable table, SQLiteIndex index);

		public abstract SQLiteErrorCode Disconnect(SQLiteVirtualTable table);

		public abstract SQLiteErrorCode Destroy(SQLiteVirtualTable table);

		public abstract SQLiteErrorCode Open(SQLiteVirtualTable table, ref SQLiteVirtualTableCursor cursor);

		public abstract SQLiteErrorCode Close(SQLiteVirtualTableCursor cursor);

		public abstract SQLiteErrorCode Filter(SQLiteVirtualTableCursor cursor, int indexNumber, string indexString, SQLiteValue[] values);

		public abstract SQLiteErrorCode Next(SQLiteVirtualTableCursor cursor);

		public abstract bool Eof(SQLiteVirtualTableCursor cursor);

		public abstract SQLiteErrorCode Column(SQLiteVirtualTableCursor cursor, SQLiteContext context, int index);

		public abstract SQLiteErrorCode RowId(SQLiteVirtualTableCursor cursor, ref long rowId);

		public abstract SQLiteErrorCode Update(SQLiteVirtualTable table, SQLiteValue[] values, ref long rowId);

		public abstract SQLiteErrorCode Begin(SQLiteVirtualTable table);

		public abstract SQLiteErrorCode Sync(SQLiteVirtualTable table);

		public abstract SQLiteErrorCode Commit(SQLiteVirtualTable table);

		public abstract SQLiteErrorCode Rollback(SQLiteVirtualTable table);

		public abstract bool FindFunction(SQLiteVirtualTable table, int argumentCount, string name, ref SQLiteFunction function, ref IntPtr pClientData);

		public abstract SQLiteErrorCode Rename(SQLiteVirtualTable table, string newName);

		public abstract SQLiteErrorCode Savepoint(SQLiteVirtualTable table, int savepoint);

		public abstract SQLiteErrorCode Release(SQLiteVirtualTable table, int savepoint);

		public abstract SQLiteErrorCode RollbackTo(SQLiteVirtualTable table, int savepoint);

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		private void CheckDisposed()
		{
			if (disposed)
			{
				throw new ObjectDisposedException(typeof(SQLiteModule).Name);
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposed)
			{
				return;
			}
			if (disposing && functions != null)
			{
				functions.Clear();
			}
			try
			{
				if (disposableModule != IntPtr.Zero)
				{
					UnsafeNativeMethods.sqlite3_dispose_module(disposableModule);
					disposableModule = IntPtr.Zero;
				}
			}
			catch (Exception ex)
			{
				try
				{
					if (LogExceptionsNoThrow)
					{
						SQLiteLog.LogMessage(-2146233088, string.Format(CultureInfo.CurrentCulture, "Caught exception in \"Dispose\" method: {0}", new object[1] { ex }));
					}
				}
				catch
				{
				}
			}
			disposed = true;
		}

		~SQLiteModule()
		{
			Dispose(disposing: false);
		}
	}
	public class SQLiteModuleNoop : SQLiteModule
	{
		private Dictionary<string, SQLiteErrorCode> resultCodes;

		private bool disposed;

		public SQLiteModuleNoop(string name)
			: base(name)
		{
			resultCodes = new Dictionary<string, SQLiteErrorCode>();
		}

		protected virtual SQLiteErrorCode GetDefaultResultCode()
		{
			return SQLiteErrorCode.Ok;
		}

		protected virtual bool ResultCodeToEofResult(SQLiteErrorCode resultCode)
		{
			if (resultCode != SQLiteErrorCode.Ok)
			{
				return true;
			}
			return false;
		}

		protected virtual bool ResultCodeToFindFunctionResult(SQLiteErrorCode resultCode)
		{
			if (resultCode != SQLiteErrorCode.Ok)
			{
				return false;
			}
			return true;
		}

		protected virtual SQLiteErrorCode GetMethodResultCode(string methodName)
		{
			if (methodName == null || resultCodes == null)
			{
				return GetDefaultResultCode();
			}
			if (resultCodes != null && resultCodes.TryGetValue(methodName, out var value))
			{
				return value;
			}
			return GetDefaultResultCode();
		}

		protected virtual bool SetMethodResultCode(string methodName, SQLiteErrorCode resultCode)
		{
			if (methodName == null || resultCodes == null)
			{
				return false;
			}
			resultCodes[methodName] = resultCode;
			return true;
		}

		public override SQLiteErrorCode Create(SQLiteConnection connection, IntPtr pClientData, string[] arguments, ref SQLiteVirtualTable table, ref string error)
		{
			CheckDisposed();
			return GetMethodResultCode("Create");
		}

		public override SQLiteErrorCode Connect(SQLiteConnection connection, IntPtr pClientData, string[] arguments, ref SQLiteVirtualTable table, ref string error)
		{
			CheckDisposed();
			return GetMethodResultCode("Connect");
		}

		public override SQLiteErrorCode BestIndex(SQLiteVirtualTable table, SQLiteIndex index)
		{
			CheckDisposed();
			return GetMethodResultCode("BestIndex");
		}

		public override SQLiteErrorCode Disconnect(SQLiteVirtualTable table)
		{
			CheckDisposed();
			return GetMethodResultCode("Disconnect");
		}

		public override SQLiteErrorCode Destroy(SQLiteVirtualTable table)
		{
			CheckDisposed();
			return GetMethodResultCode("Destroy");
		}

		public override SQLiteErrorCode Open(SQLiteVirtualTable table, ref SQLiteVirtualTableCursor cursor)
		{
			CheckDisposed();
			return GetMethodResultCode("Open");
		}

		public override SQLiteErrorCode Close(SQLiteVirtualTableCursor cursor)
		{
			CheckDisposed();
			return GetMethodResultCode("Close");
		}

		public override SQLiteErrorCode Filter(SQLiteVirtualTableCursor cursor, int indexNumber, string indexString, SQLiteValue[] values)
		{
			CheckDisposed();
			return GetMethodResultCode("Filter");
		}

		public override SQLiteErrorCode Next(SQLiteVirtualTableCursor cursor)
		{
			CheckDisposed();
			return GetMethodResultCode("Next");
		}

		public override bool Eof(SQLiteVirtualTableCursor cursor)
		{
			CheckDisposed();
			return ResultCodeToEofResult(GetMethodResultCode("Eof"));
		}

		public override SQLiteErrorCode Column(SQLiteVirtualTableCursor cursor, SQLiteContext context, int index)
		{
			CheckDisposed();
			return GetMethodResultCode("Column");
		}

		public override SQLiteErrorCode RowId(SQLiteVirtualTableCursor cursor, ref long rowId)
		{
			CheckDisposed();
			return GetMethodResultCode("RowId");
		}

		public override SQLiteErrorCode Update(SQLiteVirtualTable table, SQLiteValue[] values, ref long rowId)
		{
			CheckDisposed();
			return GetMethodResultCode("Update");
		}

		public override SQLiteErrorCode Begin(SQLiteVirtualTable table)
		{
			CheckDisposed();
			return GetMethodResultCode("Begin");
		}

		public override SQLiteErrorCode Sync(SQLiteVirtualTable table)
		{
			CheckDisposed();
			return GetMethodResultCode("Sync");
		}

		public override SQLiteErrorCode Commit(SQLiteVirtualTable table)
		{
			CheckDisposed();
			return GetMethodResultCode("Commit");
		}

		public override SQLiteErrorCode Rollback(SQLiteVirtualTable table)
		{
			CheckDisposed();
			return GetMethodResultCode("Rollback");
		}

		public override bool FindFunction(SQLiteVirtualTable table, int argumentCount, string name, ref SQLiteFunction function, ref IntPtr pClientData)
		{
			CheckDisposed();
			return ResultCodeToFindFunctionResult(GetMethodResultCode("FindFunction"));
		}

		public override SQLiteErrorCode Rename(SQLiteVirtualTable table, string newName)
		{
			CheckDisposed();
			return GetMethodResultCode("Rename");
		}

		public override SQLiteErrorCode Savepoint(SQLiteVirtualTable table, int savepoint)
		{
			CheckDisposed();
			return GetMethodResultCode("Savepoint");
		}

		public override SQLiteErrorCode Release(SQLiteVirtualTable table, int savepoint)
		{
			CheckDisposed();
			return GetMethodResultCode("Release");
		}

		public override SQLiteErrorCode RollbackTo(SQLiteVirtualTable table, int savepoint)
		{
			CheckDisposed();
			return GetMethodResultCode("RollbackTo");
		}

		private void CheckDisposed()
		{
			if (disposed)
			{
				throw new ObjectDisposedException(typeof(SQLiteModuleNoop).Name);
			}
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				_ = disposed;
			}
			finally
			{
				base.Dispose(disposing);
				disposed = true;
			}
		}
	}
	public class SQLiteModuleCommon : SQLiteModuleNoop
	{
		private static readonly string declareSql = string.Format(CultureInfo.CurrentCulture, "CREATE TABLE {0}(x);", new object[1] { typeof(SQLiteModuleCommon).Name });

		private bool objectIdentity;

		private bool disposed;

		public SQLiteModuleCommon(string name)
			: this(name, objectIdentity: false)
		{
		}

		public SQLiteModuleCommon(string name, bool objectIdentity)
			: base(name)
		{
			this.objectIdentity = objectIdentity;
		}

		protected virtual string GetSqlForDeclareTable()
		{
			return declareSql;
		}

		protected virtual SQLiteErrorCode CursorTypeMismatchError(SQLiteVirtualTableCursor cursor, Type type)
		{
			if ((object)type != null)
			{
				SetCursorError(cursor, $"not a \"{type}\" cursor");
			}
			else
			{
				SetCursorError(cursor, "cursor type mismatch");
			}
			return SQLiteErrorCode.Error;
		}

		protected virtual string GetStringFromObject(SQLiteVirtualTableCursor cursor, object value)
		{
			if (value == null)
			{
				return null;
			}
			if (value is string)
			{
				return (string)value;
			}
			return value.ToString();
		}

		protected virtual long MakeRowId(int rowIndex, int hashCode)
		{
			long num = rowIndex;
			num <<= 32;
			return num | (uint)hashCode;
		}

		protected virtual long GetRowIdFromObject(SQLiteVirtualTableCursor cursor, object value)
		{
			int rowIndex = cursor?.GetRowIndex() ?? 0;
			int hashCode = SQLiteMarshal.GetHashCode(value, objectIdentity);
			return MakeRowId(rowIndex, hashCode);
		}

		private void CheckDisposed()
		{
			if (disposed)
			{
				throw new ObjectDisposedException(typeof(SQLiteModuleCommon).Name);
			}
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				_ = disposed;
			}
			finally
			{
				base.Dispose(disposing);
				disposed = true;
			}
		}
	}
	public class SQLiteVirtualTableCursorEnumerator : SQLiteVirtualTableCursor, IEnumerator
	{
		private IEnumerator enumerator;

		private bool endOfEnumerator;

		private bool disposed;

		public virtual object Current
		{
			get
			{
				CheckDisposed();
				CheckClosed();
				if (enumerator == null)
				{
					return null;
				}
				return enumerator.Current;
			}
		}

		public virtual bool EndOfEnumerator
		{
			get
			{
				CheckDisposed();
				CheckClosed();
				return endOfEnumerator;
			}
		}

		public virtual bool IsOpen
		{
			get
			{
				CheckDisposed();
				return enumerator != null;
			}
		}

		public SQLiteVirtualTableCursorEnumerator(SQLiteVirtualTable table, IEnumerator enumerator)
			: base(table)
		{
			this.enumerator = enumerator;
			endOfEnumerator = true;
		}

		public virtual bool MoveNext()
		{
			CheckDisposed();
			CheckClosed();
			if (enumerator == null)
			{
				return false;
			}
			endOfEnumerator = !enumerator.MoveNext();
			if (!endOfEnumerator)
			{
				NextRowIndex();
			}
			return !endOfEnumerator;
		}

		public virtual void Reset()
		{
			CheckDisposed();
			CheckClosed();
			if (enumerator != null)
			{
				enumerator.Reset();
			}
		}

		public virtual void Close()
		{
			if (enumerator != null)
			{
				enumerator = null;
			}
		}

		public virtual void CheckClosed()
		{
			CheckDisposed();
			if (!IsOpen)
			{
				throw new InvalidOperationException("virtual table cursor is closed");
			}
		}

		private void CheckDisposed()
		{
			if (disposed)
			{
				throw new ObjectDisposedException(typeof(SQLiteVirtualTableCursorEnumerator).Name);
			}
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!disposed)
				{
					Close();
				}
			}
			finally
			{
				base.Dispose(disposing);
				disposed = true;
			}
		}
	}
	public class SQLiteModuleEnumerable : SQLiteModuleCommon
	{
		private IEnumerable enumerable;

		private bool objectIdentity;

		private bool disposed;

		public SQLiteModuleEnumerable(string name, IEnumerable enumerable)
			: this(name, enumerable, objectIdentity: false)
		{
		}

		public SQLiteModuleEnumerable(string name, IEnumerable enumerable, bool objectIdentity)
			: base(name)
		{
			if (enumerable == null)
			{
				throw new ArgumentNullException("enumerable");
			}
			this.enumerable = enumerable;
			this.objectIdentity = objectIdentity;
		}

		protected virtual SQLiteErrorCode CursorEndOfEnumeratorError(SQLiteVirtualTableCursor cursor)
		{
			SetCursorError(cursor, "already hit end of enumerator");
			return SQLiteErrorCode.Error;
		}

		public override SQLiteErrorCode Create(SQLiteConnection connection, IntPtr pClientData, string[] arguments, ref SQLiteVirtualTable table, ref string error)
		{
			CheckDisposed();
			if (DeclareTable(connection, GetSqlForDeclareTable(), ref error) == SQLiteErrorCode.Ok)
			{
				table = new SQLiteVirtualTable(arguments);
				return SQLiteErrorCode.Ok;
			}
			return SQLiteErrorCode.Error;
		}

		public override SQLiteErrorCode Connect(SQLiteConnection connection, IntPtr pClientData, string[] arguments, ref SQLiteVirtualTable table, ref string error)
		{
			CheckDisposed();
			if (DeclareTable(connection, GetSqlForDeclareTable(), ref error) == SQLiteErrorCode.Ok)
			{
				table = new SQLiteVirtualTable(arguments);
				return SQLiteErrorCode.Ok;
			}
			return SQLiteErrorCode.Error;
		}

		public override SQLiteErrorCode BestIndex(SQLiteVirtualTable table, SQLiteIndex index)
		{
			CheckDisposed();
			if (!table.BestIndex(index))
			{
				SetTableError(table, string.Format(CultureInfo.CurrentCulture, "failed to select best index for virtual table \"{0}\"", new object[1] { table.TableName }));
				return SQLiteErrorCode.Error;
			}
			return SQLiteErrorCode.Ok;
		}

		public override SQLiteErrorCode Disconnect(SQLiteVirtualTable table)
		{
			CheckDisposed();
			table.Dispose();
			return SQLiteErrorCode.Ok;
		}

		public override SQLiteErrorCode Destroy(SQLiteVirtualTable table)
		{
			CheckDisposed();
			table.Dispose();
			return SQLiteErrorCode.Ok;
		}

		public override SQLiteErrorCode Open(SQLiteVirtualTable table, ref SQLiteVirtualTableCursor cursor)
		{
			CheckDisposed();
			cursor = new SQLiteVirtualTableCursorEnumerator(table, enumerable.GetEnumerator());
			return SQLiteErrorCode.Ok;
		}

		public override SQLiteErrorCode Close(SQLiteVirtualTableCursor cursor)
		{
			CheckDisposed();
			if (!(cursor is SQLiteVirtualTableCursorEnumerator sQLiteVirtualTableCursorEnumerator))
			{
				return CursorTypeMismatchError(cursor, typeof(SQLiteVirtualTableCursorEnumerator));
			}
			sQLiteVirtualTableCursorEnumerator.Close();
			return SQLiteErrorCode.Ok;
		}

		public override SQLiteErrorCode Filter(SQLiteVirtualTableCursor cursor, int indexNumber, string indexString, SQLiteValue[] values)
		{
			CheckDisposed();
			if (!(cursor is SQLiteVirtualTableCursorEnumerator sQLiteVirtualTableCursorEnumerator))
			{
				return CursorTypeMismatchError(cursor, typeof(SQLiteVirtualTableCursorEnumerator));
			}
			sQLiteVirtualTableCursorEnumerator.Filter(indexNumber, indexString, values);
			sQLiteVirtualTableCursorEnumerator.Reset();
			sQLiteVirtualTableCursorEnumerator.MoveNext();
			return SQLiteErrorCode.Ok;
		}

		public override SQLiteErrorCode Next(SQLiteVirtualTableCursor cursor)
		{
			CheckDisposed();
			if (!(cursor is SQLiteVirtualTableCursorEnumerator sQLiteVirtualTableCursorEnumerator))
			{
				return CursorTypeMismatchError(cursor, typeof(SQLiteVirtualTableCursorEnumerator));
			}
			if (sQLiteVirtualTableCursorEnumerator.EndOfEnumerator)
			{
				return CursorEndOfEnumeratorError(cursor);
			}
			sQLiteVirtualTableCursorEnumerator.MoveNext();
			return SQLiteErrorCode.Ok;
		}

		public override bool Eof(SQLiteVirtualTableCursor cursor)
		{
			CheckDisposed();
			if (!(cursor is SQLiteVirtualTableCursorEnumerator sQLiteVirtualTableCursorEnumerator))
			{
				return ResultCodeToEofResult(CursorTypeMismatchError(cursor, typeof(SQLiteVirtualTableCursorEnumerator)));
			}
			return sQLiteVirtualTableCursorEnumerator.EndOfEnumerator;
		}

		public override SQLiteErrorCode Column(SQLiteVirtualTableCursor cursor, SQLiteContext context, int index)
		{
			CheckDisposed();
			if (!(cursor is SQLiteVirtualTableCursorEnumerator sQLiteVirtualTableCursorEnumerator))
			{
				return CursorTypeMismatchError(cursor, typeof(SQLiteVirtualTableCursorEnumerator));
			}
			if (sQLiteVirtualTableCursorEnumerator.EndOfEnumerator)
			{
				return CursorEndOfEnumeratorError(cursor);
			}
			object current = sQLiteVirtualTableCursorEnumerator.Current;
			if (current != null)
			{
				context.SetString(GetStringFromObject(cursor, current));
			}
			else
			{
				context.SetNull();
			}
			return SQLiteErrorCode.Ok;
		}

		public override SQLiteErrorCode RowId(SQLiteVirtualTableCursor cursor, ref long rowId)
		{
			CheckDisposed();
			if (!(cursor is SQLiteVirtualTableCursorEnumerator sQLiteVirtualTableCursorEnumerator))
			{
				return CursorTypeMismatchError(cursor, typeof(SQLiteVirtualTableCursorEnumerator));
			}
			if (sQLiteVirtualTableCursorEnumerator.EndOfEnumerator)
			{
				return CursorEndOfEnumeratorError(cursor);
			}
			object current = sQLiteVirtualTableCursorEnumerator.Current;
			rowId = GetRowIdFromObject(cursor, current);
			return SQLiteErrorCode.Ok;
		}

		public override SQLiteErrorCode Update(SQLiteVirtualTable table, SQLiteValue[] values, ref long rowId)
		{
			CheckDisposed();
			SetTableError(table, string.Format(CultureInfo.CurrentCulture, "virtual table \"{0}\" is read-only", new object[1] { table.TableName }));
			return SQLiteErrorCode.Error;
		}

		public override SQLiteErrorCode Rename(SQLiteVirtualTable table, string newName)
		{
			CheckDisposed();
			if (!table.Rename(newName))
			{
				SetTableError(table, string.Format(CultureInfo.CurrentCulture, "failed to rename virtual table from \"{0}\" to \"{1}\"", new object[2] { table.TableName, newName }));
				return SQLiteErrorCode.Error;
			}
			return SQLiteErrorCode.Ok;
		}

		private void CheckDisposed()
		{
			if (disposed)
			{
				throw new ObjectDisposedException(typeof(SQLiteModuleEnumerable).Name);
			}
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				_ = disposed;
			}
			finally
			{
				base.Dispose(disposing);
				disposed = true;
			}
		}
	}
}
namespace System.Data.SQLite.Generic
{
	public class SQLiteVirtualTableCursorEnumerator<T> : SQLiteVirtualTableCursorEnumerator, IEnumerator<T>, IDisposable, IEnumerator
	{
		private IEnumerator<T> enumerator;

		private bool disposed;

		T IEnumerator<T>.Current
		{
			get
			{
				CheckDisposed();
				CheckClosed();
				if (enumerator == null)
				{
					return default(T);
				}
				return enumerator.Current;
			}
		}

		public SQLiteVirtualTableCursorEnumerator(SQLiteVirtualTable table, IEnumerator<T> enumerator)
			: base(table, enumerator)
		{
			this.enumerator = enumerator;
		}

		public override void Close()
		{
			if (enumerator != null)
			{
				enumerator = null;
			}
			base.Close();
		}

		private void CheckDisposed()
		{
			if (disposed)
			{
				throw new ObjectDisposedException(typeof(SQLiteVirtualTableCursorEnumerator<T>).Name);
			}
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!disposed)
				{
					Close();
				}
			}
			finally
			{
				base.Dispose(disposing);
				disposed = true;
			}
		}
	}
	public class SQLiteModuleEnumerable<T> : SQLiteModuleEnumerable
	{
		private IEnumerable<T> enumerable;

		private bool disposed;

		public SQLiteModuleEnumerable(string name, IEnumerable<T> enumerable)
			: base(name, enumerable)
		{
			this.enumerable = enumerable;
		}

		public override SQLiteErrorCode Open(SQLiteVirtualTable table, ref SQLiteVirtualTableCursor cursor)
		{
			CheckDisposed();
			cursor = new SQLiteVirtualTableCursorEnumerator<T>(table, enumerable.GetEnumerator());
			return SQLiteErrorCode.Ok;
		}

		public override SQLiteErrorCode Column(SQLiteVirtualTableCursor cursor, SQLiteContext context, int index)
		{
			CheckDisposed();
			if (!(cursor is SQLiteVirtualTableCursorEnumerator<T> sQLiteVirtualTableCursorEnumerator))
			{
				return CursorTypeMismatchError(cursor, typeof(SQLiteVirtualTableCursorEnumerator));
			}
			if (sQLiteVirtualTableCursorEnumerator.EndOfEnumerator)
			{
				return CursorEndOfEnumeratorError(cursor);
			}
			T current = ((IEnumerator<T>)sQLiteVirtualTableCursorEnumerator).Current;
			if (current != null)
			{
				context.SetString(GetStringFromObject(cursor, current));
			}
			else
			{
				context.SetNull();
			}
			return SQLiteErrorCode.Ok;
		}

		private void CheckDisposed()
		{
			if (disposed)
			{
				throw new ObjectDisposedException(typeof(SQLiteModuleEnumerable<T>).Name);
			}
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				_ = disposed;
			}
			finally
			{
				base.Dispose(disposing);
				disposed = true;
			}
		}
	}
}
