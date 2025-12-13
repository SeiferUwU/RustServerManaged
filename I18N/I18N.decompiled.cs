using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;

[assembly: AssemblyDelaySign(true)]
[assembly: AssemblyKeyFile("../../mono.pub")]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: AssemblyVersion("2.0.5.0")]
[module: UnverifiableCode]
internal static class Consts
{
	public const string MonoVersion = "2.6.5.0";

	public const string MonoCompany = "MONO development team";

	public const string MonoProduct = "MONO Common language infrastructure";

	public const string MonoCopyright = "(c) various MONO Authors";

	public const string FxVersion = "2.0.5.0";

	public const string VsVersion = "9.0.0.0";

	public const string FxFileVersion = "3.0.40818.0";

	public const string VsFileVersion = "9.0.50727.42";

	public const string AssemblyI18N = "I18N, Version=2.0.5.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyMicrosoft_VisualStudio = "Microsoft.VisualStudio, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblyMicrosoft_VisualStudio_Web = "Microsoft.VisualStudio.Web, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblyMicrosoft_VSDesigner = "Microsoft.VSDesigner, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblyMono_Http = "Mono.Http, Version=2.0.5.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyMono_Posix = "Mono.Posix, Version=2.0.5.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyMono_Security = "Mono.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyMono_Messaging_RabbitMQ = "Mono.Messaging.RabbitMQ, Version=2.0.5.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756";

	public const string AssemblyCorlib = "mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem = "System, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem_Data = "System.Data, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem_Design = "System.Design, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_DirectoryServices = "System.DirectoryServices, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Drawing = "System.Drawing, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Drawing_Design = "System.Drawing.Design, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Messaging = "System.Messaging, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Security = "System.Security, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_ServiceProcess = "System.ServiceProcess, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Web = "System.Web, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

	public const string AssemblySystem_Windows_Forms = "System.Windows.Forms, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

	public const string AssemblySystem_Core = "System.Core, Version=2.0.5.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
}
namespace I18N.Common;

[Serializable]
public abstract class ByteEncoding : MonoEncoding
{
	protected char[] toChars;

	protected string encodingName;

	protected string bodyName;

	protected string headerName;

	protected string webName;

	protected bool isBrowserDisplay;

	protected bool isBrowserSave;

	protected bool isMailNewsDisplay;

	protected bool isMailNewsSave;

	protected int windowsCodePage;

	private static byte[] isNormalized;

	private static byte[] isNormalizedComputed;

	private static byte[] normalization_bytes;

	public override bool IsSingleByte => true;

	public override string BodyName => bodyName;

	public override string EncodingName => encodingName;

	public override string HeaderName => headerName;

	public override bool IsBrowserDisplay => isBrowserDisplay;

	public override bool IsBrowserSave => isBrowserSave;

	public override bool IsMailNewsDisplay => isMailNewsDisplay;

	public override bool IsMailNewsSave => isMailNewsSave;

	public override string WebName => webName;

	public override int WindowsCodePage => windowsCodePage;

	protected ByteEncoding(int codePage, char[] toChars, string encodingName, string bodyName, string headerName, string webName, bool isBrowserDisplay, bool isBrowserSave, bool isMailNewsDisplay, bool isMailNewsSave, int windowsCodePage)
		: base(codePage)
	{
		if (toChars.Length != 256)
		{
			throw new ArgumentException("toChars");
		}
		this.toChars = toChars;
		this.encodingName = encodingName;
		this.bodyName = bodyName;
		this.headerName = headerName;
		this.webName = webName;
		this.isBrowserDisplay = isBrowserDisplay;
		this.isBrowserSave = isBrowserSave;
		this.isMailNewsDisplay = isMailNewsDisplay;
		this.isMailNewsSave = isMailNewsSave;
		this.windowsCodePage = windowsCodePage;
	}

	public override bool IsAlwaysNormalized(NormalizationForm form)
	{
		if (form != NormalizationForm.FormC)
		{
			return false;
		}
		if (isNormalized == null)
		{
			isNormalized = new byte[8192];
		}
		if (isNormalizedComputed == null)
		{
			isNormalizedComputed = new byte[8192];
		}
		if (normalization_bytes == null)
		{
			normalization_bytes = new byte[256];
			lock (normalization_bytes)
			{
				for (int i = 0; i < 256; i++)
				{
					normalization_bytes[i] = (byte)i;
				}
			}
		}
		byte b = (byte)(1 << CodePage % 8);
		if ((isNormalizedComputed[CodePage / 8] & b) == 0)
		{
			Encoding encoding = Clone() as Encoding;
			encoding.DecoderFallback = new DecoderReplacementFallback(string.Empty);
			string text = encoding.GetString(normalization_bytes);
			if (text != text.Normalize(form))
			{
				isNormalized[CodePage / 8] |= b;
			}
			isNormalizedComputed[CodePage / 8] |= b;
		}
		return (isNormalized[CodePage / 8] & b) == 0;
	}

	public override int GetByteCount(string s)
	{
		if (s == null)
		{
			throw new ArgumentNullException("s");
		}
		return s.Length;
	}

	public unsafe override int GetByteCountImpl(char* chars, int count)
	{
		return count;
	}

	protected unsafe abstract void ToBytes(char* chars, int charCount, byte* bytes, int byteCount);

	protected unsafe virtual void ToBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
	{
		//IL_0027->IL002e: Incompatible stack types: I vs Ref
		//IL_0046->IL004e: Incompatible stack types: I vs Ref
		if (charCount == 0 || bytes.Length == byteIndex)
		{
			return;
		}
		fixed (char* ptr = &(chars != null && chars.Length != 0 ? ref chars[0] : ref *(char*)null))
		{
			fixed (byte* ptr2 = &(bytes != null && bytes.Length != 0 ? ref bytes[0] : ref *(byte*)null))
			{
				ToBytes((char*)((byte*)ptr + charIndex * 2), charCount, ptr2 + byteIndex, bytes.Length - byteIndex);
			}
		}
	}

	public unsafe override int GetBytesImpl(char* chars, int charCount, byte* bytes, int byteCount)
	{
		ToBytes(chars, charCount, bytes, byteCount);
		return charCount;
	}

	public override int GetCharCount(byte[] bytes, int index, int count)
	{
		if (bytes == null)
		{
			throw new ArgumentNullException("bytes");
		}
		if (index < 0 || index > bytes.Length)
		{
			throw new ArgumentOutOfRangeException("index", Strings.GetString("ArgRange_Array"));
		}
		if (count < 0 || count > bytes.Length - index)
		{
			throw new ArgumentOutOfRangeException("count", Strings.GetString("ArgRange_Array"));
		}
		return count;
	}

	public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
	{
		if (bytes == null)
		{
			throw new ArgumentNullException("bytes");
		}
		if (chars == null)
		{
			throw new ArgumentNullException("chars");
		}
		if (byteIndex < 0 || byteIndex > bytes.Length)
		{
			throw new ArgumentOutOfRangeException("byteIndex", Strings.GetString("ArgRange_Array"));
		}
		if (byteCount < 0 || byteCount > bytes.Length - byteIndex)
		{
			throw new ArgumentOutOfRangeException("byteCount", Strings.GetString("ArgRange_Array"));
		}
		if (charIndex < 0 || charIndex > chars.Length)
		{
			throw new ArgumentOutOfRangeException("charIndex", Strings.GetString("ArgRange_Array"));
		}
		if (chars.Length - charIndex < byteCount)
		{
			throw new ArgumentException(Strings.GetString("Arg_InsufficientSpace"));
		}
		int num = byteCount;
		char[] array = toChars;
		while (num-- > 0)
		{
			chars[charIndex++] = array[bytes[byteIndex++]];
		}
		return byteCount;
	}

	public override int GetMaxByteCount(int charCount)
	{
		if (charCount < 0)
		{
			throw new ArgumentOutOfRangeException("charCount", Strings.GetString("ArgRange_NonNegative"));
		}
		return charCount;
	}

	public override int GetMaxCharCount(int byteCount)
	{
		if (byteCount < 0)
		{
			throw new ArgumentOutOfRangeException("byteCount", Strings.GetString("ArgRange_NonNegative"));
		}
		return byteCount;
	}

	public unsafe override string GetString(byte[] bytes, int index, int count)
	{
		//IL_0086->IL008d: Incompatible stack types: I vs Ref
		//IL_00ba->IL00c6: Incompatible stack types: I vs Ref
		if (bytes == null)
		{
			throw new ArgumentNullException("bytes");
		}
		if (index < 0 || index > bytes.Length)
		{
			throw new ArgumentOutOfRangeException("index", Strings.GetString("ArgRange_Array"));
		}
		if (count < 0 || count > bytes.Length - index)
		{
			throw new ArgumentOutOfRangeException("count", Strings.GetString("ArgRange_Array"));
		}
		if (count == 0)
		{
			return string.Empty;
		}
		string text = new string('\0', count);
		fixed (byte* ptr = &(bytes != null && bytes.Length != 0 ? ref bytes[0] : ref *(byte*)null))
		{
			fixed (char* ptr2 = text)
			{
				fixed (char* ptr3 = &(toChars != null && toChars.Length != 0 ? ref toChars[0] : ref *(char*)null))
				{
					byte* ptr4 = ptr + index;
					char* ptr5 = ptr2;
					while (count-- != 0)
					{
						*(ptr5++) = ptr3[(int)(*(ptr4++))];
					}
				}
			}
		}
		return text;
	}

	public override string GetString(byte[] bytes)
	{
		if (bytes == null)
		{
			throw new ArgumentNullException("bytes");
		}
		return GetString(bytes, 0, bytes.Length);
	}
}
public sealed class Handlers
{
	public static readonly string[] List = new string[173]
	{
		"I18N.CJK.CP932", "I18N.CJK.CP936", "I18N.CJK.CP949", "I18N.CJK.CP950", "I18N.CJK.CP50220", "I18N.CJK.CP50221", "I18N.CJK.CP50222", "I18N.CJK.CP51932", "I18N.CJK.CP51949", "I18N.CJK.CP54936",
		"I18N.CJK.ENCbig5", "I18N.CJK.ENCgb2312", "I18N.CJK.ENCshift_jis", "I18N.CJK.ENCiso_2022_jp", "I18N.CJK.ENCeuc_jp", "I18N.CJK.ENCeuc_kr", "I18N.CJK.ENCuhc", "I18N.CJK.ENCgb18030", "I18N.MidEast.CP1254", "I18N.MidEast.ENCwindows_1254",
		"I18N.MidEast.CP1255", "I18N.MidEast.ENCwindows_1255", "I18N.MidEast.CP1256", "I18N.MidEast.ENCwindows_1256", "I18N.MidEast.CP28596", "I18N.MidEast.ENCiso_8859_6", "I18N.MidEast.CP28598", "I18N.MidEast.ENCiso_8859_8", "I18N.MidEast.CP28599", "I18N.MidEast.ENCiso_8859_9",
		"I18N.MidEast.CP38598", "I18N.MidEast.ENCwindows_38598", "I18N.Other.CP1251", "I18N.Other.ENCwindows_1251", "I18N.Other.CP1257", "I18N.Other.ENCwindows_1257", "I18N.Other.CP1258", "I18N.Other.ENCwindows_1258", "I18N.Other.CP20866", "I18N.Other.ENCkoi8_r",
		"I18N.Other.CP21866", "I18N.Other.ENCkoi8_u", "I18N.Other.CP28594", "I18N.Other.ENCiso_8859_4", "I18N.Other.CP28595", "I18N.Other.ENCiso_8859_5", "I18N.Other.ISCIIEncoding", "I18N.Other.CP57002", "I18N.Other.CP57003", "I18N.Other.CP57004",
		"I18N.Other.CP57005", "I18N.Other.CP57006", "I18N.Other.CP57007", "I18N.Other.CP57008", "I18N.Other.CP57009", "I18N.Other.CP57010", "I18N.Other.CP57011", "I18N.Other.ENCx_iscii_de", "I18N.Other.ENCx_iscii_be", "I18N.Other.ENCx_iscii_ta",
		"I18N.Other.ENCx_iscii_te", "I18N.Other.ENCx_iscii_as", "I18N.Other.ENCx_iscii_or", "I18N.Other.ENCx_iscii_ka", "I18N.Other.ENCx_iscii_ma", "I18N.Other.ENCx_iscii_gu", "I18N.Other.ENCx_iscii_pa", "I18N.Other.CP874", "I18N.Other.ENCwindows_874", "I18N.Rare.CP1026",
		"I18N.Rare.ENCibm1026", "I18N.Rare.CP1047", "I18N.Rare.ENCibm1047", "I18N.Rare.CP1140", "I18N.Rare.ENCibm01140", "I18N.Rare.CP1141", "I18N.Rare.ENCibm01141", "I18N.Rare.CP1142", "I18N.Rare.ENCibm01142", "I18N.Rare.CP1143",
		"I18N.Rare.ENCibm01143", "I18N.Rare.CP1144", "I18N.Rare.ENCibm1144", "I18N.Rare.CP1145", "I18N.Rare.ENCibm1145", "I18N.Rare.CP1146", "I18N.Rare.ENCibm1146", "I18N.Rare.CP1147", "I18N.Rare.ENCibm1147", "I18N.Rare.CP1148",
		"I18N.Rare.ENCibm1148", "I18N.Rare.CP1149", "I18N.Rare.ENCibm1149", "I18N.Rare.CP20273", "I18N.Rare.ENCibm273", "I18N.Rare.CP20277", "I18N.Rare.ENCibm277", "I18N.Rare.CP20278", "I18N.Rare.ENCibm278", "I18N.Rare.CP20280",
		"I18N.Rare.ENCibm280", "I18N.Rare.CP20284", "I18N.Rare.ENCibm284", "I18N.Rare.CP20285", "I18N.Rare.ENCibm285", "I18N.Rare.CP20290", "I18N.Rare.ENCibm290", "I18N.Rare.CP20297", "I18N.Rare.ENCibm297", "I18N.Rare.CP20420",
		"I18N.Rare.ENCibm420", "I18N.Rare.CP20424", "I18N.Rare.ENCibm424", "I18N.Rare.CP20871", "I18N.Rare.ENCibm871", "I18N.Rare.CP21025", "I18N.Rare.ENCibm1025", "I18N.Rare.CP37", "I18N.Rare.ENCibm037", "I18N.Rare.CP500",
		"I18N.Rare.ENCibm500", "I18N.Rare.CP708", "I18N.Rare.ENCasmo_708", "I18N.Rare.CP852", "I18N.Rare.ENCibm852", "I18N.Rare.CP855", "I18N.Rare.ENCibm855", "I18N.Rare.CP857", "I18N.Rare.ENCibm857", "I18N.Rare.CP858",
		"I18N.Rare.ENCibm00858", "I18N.Rare.CP862", "I18N.Rare.ENCibm862", "I18N.Rare.CP864", "I18N.Rare.ENCibm864", "I18N.Rare.CP866", "I18N.Rare.ENCibm866", "I18N.Rare.CP869", "I18N.Rare.ENCibm869", "I18N.Rare.CP870",
		"I18N.Rare.ENCibm870", "I18N.Rare.CP875", "I18N.Rare.ENCibm875", "I18N.West.CP10000", "I18N.West.ENCmacintosh", "I18N.West.CP10079", "I18N.West.ENCx_mac_icelandic", "I18N.West.CP1250", "I18N.West.ENCwindows_1250", "I18N.West.CP1252",
		"I18N.West.ENCwindows_1252", "I18N.West.CP1253", "I18N.West.ENCwindows_1253", "I18N.West.CP28592", "I18N.West.ENCiso_8859_2", "I18N.West.CP28593", "I18N.West.ENCiso_8859_3", "I18N.West.CP28597", "I18N.West.ENCiso_8859_7", "I18N.West.CP28605",
		"I18N.West.ENCiso_8859_15", "I18N.West.CP437", "I18N.West.ENCibm437", "I18N.West.CP850", "I18N.West.ENCibm850", "I18N.West.CP860", "I18N.West.ENCibm860", "I18N.West.CP861", "I18N.West.ENCibm861", "I18N.West.CP863",
		"I18N.West.ENCibm863", "I18N.West.CP865", "I18N.West.ENCibm865"
	};

	private static Hashtable aliases;

	public static string GetAlias(string name)
	{
		if (aliases == null)
		{
			BuildHash();
		}
		return aliases[name] as string;
	}

	private static void BuildHash()
	{
		aliases = new Hashtable(new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer());
		aliases.Add("arabic", "iso_8859_6");
		aliases.Add("csISOLatinArabic", "iso_8859_6");
		aliases.Add("ECMA_114", "iso_8859_6");
		aliases.Add("ISO_8859_6:1987", "iso_8859_6");
		aliases.Add("iso_ir_127", "iso_8859_6");
		aliases.Add("cp1256", "windows_1256");
		aliases.Add("csISOLatin4", "iso_8859_4");
		aliases.Add("ISO_8859_4:1988", "iso_8859_4");
		aliases.Add("iso_ir_110", "iso_8859_4");
		aliases.Add("l4", "iso_8859_4");
		aliases.Add("latin4", "iso_8859_4");
		aliases.Add("cp852", "ibm852");
		aliases.Add("csISOLatin2", "iso_8859_2");
		aliases.Add("iso_8859_2:1987", "iso_8859_2");
		aliases.Add("iso8859_2", "iso_8859_2");
		aliases.Add("iso_ir_101", "iso_8859_2");
		aliases.Add("l2", "iso_8859_2");
		aliases.Add("latin2", "iso_8859_2");
		aliases.Add("x-cp1250", "windows_1250");
		aliases.Add("chinese", "gb2312");
		aliases.Add("CN-GB", "gb2312");
		aliases.Add("csGB2312", "gb2312");
		aliases.Add("csGB231280", "gb2312");
		aliases.Add("csISO58GB231280", "gb2312");
		aliases.Add("GB_2312_80", "gb2312");
		aliases.Add("GB231280", "gb2312");
		aliases.Add("GB2312_80", "gb2312");
		aliases.Add("GBK", "gb2312");
		aliases.Add("iso_ir_58", "gb2312");
		aliases.Add("cn-big5", "big5");
		aliases.Add("csbig5", "big5");
		aliases.Add("x-x-big5", "big5");
		aliases.Add("cp866", "ibm866");
		aliases.Add("csISOLatin5", "iso_8859_5");
		aliases.Add("csISOLatinCyrillic", "iso_8859_5");
		aliases.Add("cyrillic", "iso_8859_5");
		aliases.Add("ISO_8859_5:1988", "iso_8859_5");
		aliases.Add("iso_ir_144", "iso_8859_5");
		aliases.Add("l5", "iso_8859_5");
		aliases.Add("csKOI8R", "koi8_r");
		aliases.Add("koi", "koi8_r");
		aliases.Add("koi8", "koi8_r");
		aliases.Add("koi8r", "koi8_r");
		aliases.Add("koi8ru", "koi8_u");
		aliases.Add("x-cp1251", "windows_1251");
		aliases.Add("csISOLatinGreek", "iso_8859_7");
		aliases.Add("ECMA_118", "iso_8859_7");
		aliases.Add("ELOT_928", "iso_8859_7");
		aliases.Add("greek", "iso_8859_7");
		aliases.Add("greek8", "iso_8859_7");
		aliases.Add("ISO_8859_7:1987", "iso_8859_7");
		aliases.Add("iso_ir_126", "iso_8859_7");
		aliases.Add("csISOLatinHebrew", "iso_8859_8");
		aliases.Add("hebrew", "iso_8859_8");
		aliases.Add("ISO_8859_8:1988", "iso_8859_8");
		aliases.Add("iso_ir_138", "iso_8859_8");
		aliases.Add("csShiftJIS", "shift_jis");
		aliases.Add("csWindows31J", "shift_jis");
		aliases.Add("ms_Kanji", "shift_jis");
		aliases.Add("shift-jis", "shift_jis");
		aliases.Add("x-ms-cp932", "shift_jis");
		aliases.Add("x-sjis", "shift_jis");
		aliases.Add("csISOLatin3", "iso_8859_3");
		aliases.Add("ISO_8859_3:1988", "iso_8859_3");
		aliases.Add("iso_ir_109", "iso_8859_3");
		aliases.Add("l3", "iso_8859_3");
		aliases.Add("latin3", "iso_8859_3");
		aliases.Add("csISOLatin9", "iso_8859_15");
		aliases.Add("l9", "iso_8859_15");
		aliases.Add("latin9", "iso_8859_15");
		aliases.Add("cp437", "ibm437");
		aliases.Add("csPC8", "ibm437");
		aliases.Add("CodePage437", "ibm437");
		aliases.Add("DOS_874", "windows_874");
		aliases.Add("iso_8859_11", "windows_874");
		aliases.Add("TIS_620", "windows_874");
	}
}
public class Manager
{
	private const string hex = "0123456789abcdef";

	private static Manager manager;

	private Hashtable handlers;

	private Hashtable active;

	private Hashtable assemblies;

	private static readonly object lockobj = new object();

	public static Manager PrimaryManager
	{
		get
		{
			lock (lockobj)
			{
				if (manager == null)
				{
					manager = new Manager();
				}
				return manager;
			}
		}
	}

	private Manager()
	{
		handlers = new Hashtable(CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);
		active = new Hashtable(16);
		assemblies = new Hashtable(8);
		LoadClassList();
	}

	private static string Normalize(string name)
	{
		return name.ToLower(CultureInfo.InvariantCulture).Replace('-', '_');
	}

	public Encoding GetEncoding(int codePage)
	{
		return Instantiate("CP" + codePage) as Encoding;
	}

	public Encoding GetEncoding(string name)
	{
		if (name == null)
		{
			return null;
		}
		string text = name;
		name = Normalize(name);
		Encoding encoding = Instantiate("ENC" + name) as Encoding;
		if (encoding == null)
		{
			encoding = Instantiate(name) as Encoding;
		}
		if (encoding == null)
		{
			string alias = Handlers.GetAlias(name);
			if (alias != null)
			{
				encoding = Instantiate("ENC" + alias) as Encoding;
				if (encoding == null)
				{
					encoding = Instantiate(alias) as Encoding;
				}
			}
		}
		if (encoding == null)
		{
			return null;
		}
		if (text.IndexOf('_') > 0 && encoding.WebName.IndexOf('-') > 0)
		{
			return null;
		}
		if (text.IndexOf('-') > 0 && encoding.WebName.IndexOf('_') > 0)
		{
			return null;
		}
		return encoding;
	}

	public CultureInfo GetCulture(int culture, bool useUserOverride)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("0123456789abcdef"[(culture >> 12) & 0xF]);
		stringBuilder.Append("0123456789abcdef"[(culture >> 8) & 0xF]);
		stringBuilder.Append("0123456789abcdef"[(culture >> 4) & 0xF]);
		stringBuilder.Append("0123456789abcdef"[culture & 0xF]);
		string text = stringBuilder.ToString();
		if (useUserOverride)
		{
			object obj = Instantiate("CIDO" + text);
			if (obj != null)
			{
				return obj as CultureInfo;
			}
		}
		return Instantiate("CID" + text) as CultureInfo;
	}

	public CultureInfo GetCulture(string name, bool useUserOverride)
	{
		if (name == null)
		{
			return null;
		}
		name = Normalize(name);
		if (useUserOverride)
		{
			object obj = Instantiate("CNO" + name.ToString());
			if (obj != null)
			{
				return obj as CultureInfo;
			}
		}
		return Instantiate("CN" + name.ToString()) as CultureInfo;
	}

	internal object Instantiate(string name)
	{
		lock (this)
		{
			object obj = active[name];
			if (obj != null)
			{
				return obj;
			}
			string text = (string)handlers[name];
			if (text == null)
			{
				return null;
			}
			Assembly assembly = (Assembly)assemblies[text];
			if ((object)assembly == null)
			{
				try
				{
					AssemblyName name2 = typeof(Manager).Assembly.GetName();
					name2.Name = text;
					assembly = Assembly.Load(name2);
				}
				catch (SystemException)
				{
					assembly = null;
				}
				if ((object)assembly == null)
				{
					return null;
				}
				assemblies[text] = assembly;
			}
			Type type = assembly.GetType(text + "." + name, throwOnError: false, ignoreCase: true);
			if ((object)type == null)
			{
				return null;
			}
			try
			{
				obj = type.InvokeMember(string.Empty, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, null, null, null, null);
			}
			catch (MissingMethodException)
			{
				return null;
			}
			catch (SecurityException)
			{
				return null;
			}
			active.Add(name, obj);
			return obj;
		}
	}

	private void LoadClassList()
	{
		FileStream file;
		try
		{
			file = Assembly.GetExecutingAssembly().GetFile("I18N-handlers.def");
			if (file == null)
			{
				LoadInternalClasses();
				return;
			}
		}
		catch (FileLoadException)
		{
			LoadInternalClasses();
			return;
		}
		StreamReader streamReader = new StreamReader(file);
		string text;
		while ((text = streamReader.ReadLine()) != null)
		{
			if (text.Length == 0 || text[0] == '#')
			{
				continue;
			}
			int num = text.LastIndexOf('.');
			if (num != -1)
			{
				string key = text.Substring(num + 1);
				if (!handlers.Contains(key))
				{
					handlers.Add(key, text.Substring(0, num));
				}
			}
		}
		streamReader.Close();
	}

	private void LoadInternalClasses()
	{
		string[] list = Handlers.List;
		foreach (string text in list)
		{
			int num = text.LastIndexOf('.');
			if (num != -1)
			{
				string key = text.Substring(num + 1);
				if (!handlers.Contains(key))
				{
					handlers.Add(key, text.Substring(0, num));
				}
			}
		}
	}
}
[Serializable]
public abstract class MonoEncoding : Encoding
{
	private readonly int win_code_page;

	public override int WindowsCodePage => (win_code_page == 0) ? base.WindowsCodePage : win_code_page;

	public MonoEncoding(int codePage)
		: this(codePage, 0)
	{
	}

	public MonoEncoding(int codePage, int windowsCodePage)
		: base(codePage)
	{
		win_code_page = windowsCodePage;
	}

	public unsafe void HandleFallback(ref EncoderFallbackBuffer buffer, char* chars, ref int charIndex, ref int charCount, byte* bytes, ref int byteIndex, ref int byteCount)
	{
		//IL_00c3->IL00ca: Incompatible stack types: I vs Ref
		if (buffer == null)
		{
			buffer = base.EncoderFallback.CreateFallbackBuffer();
		}
		if (char.IsSurrogate(*(char*)((byte*)chars + charIndex * 2)) && charCount > 0 && char.IsSurrogate(*(char*)((byte*)chars + (charIndex + 1) * 2)))
		{
			buffer.Fallback(*(char*)((byte*)chars + charIndex * 2), *(char*)((byte*)chars + (charIndex + 1) * 2), charIndex);
			charIndex++;
			charCount--;
		}
		else
		{
			buffer.Fallback(*(char*)((byte*)chars + charIndex * 2), charIndex);
		}
		char[] array = new char[buffer.Remaining];
		int num = 0;
		while (buffer.Remaining > 0)
		{
			array[num++] = buffer.GetNextChar();
		}
		fixed (char* chars2 = &(array != null && array.Length != 0 ? ref array[0] : ref *(char*)null))
		{
			byteIndex += GetBytes(chars2, array.Length, bytes + byteIndex, byteCount);
		}
	}

	public unsafe override int GetByteCount(char[] chars, int index, int count)
	{
		//IL_007a->IL0081: Incompatible stack types: I vs Ref
		if (chars == null)
		{
			throw new ArgumentNullException("chars");
		}
		if (index < 0 || index > chars.Length)
		{
			throw new ArgumentOutOfRangeException("index", Strings.GetString("ArgRange_Array"));
		}
		if (count < 0 || count > chars.Length - index)
		{
			throw new ArgumentOutOfRangeException("count", Strings.GetString("ArgRange_Array"));
		}
		if (count == 0)
		{
			return 0;
		}
		fixed (char* ptr = &(chars != null && chars.Length != 0 ? ref chars[0] : ref *(char*)null))
		{
			return GetByteCountImpl((char*)((byte*)ptr + index * 2), count);
		}
	}

	public unsafe override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
	{
		//IL_00d6->IL00dd: Incompatible stack types: I vs Ref
		//IL_00f5->IL00fd: Incompatible stack types: I vs Ref
		if (chars == null)
		{
			throw new ArgumentNullException("chars");
		}
		if (bytes == null)
		{
			throw new ArgumentNullException("bytes");
		}
		if (charIndex < 0 || charIndex > chars.Length)
		{
			throw new ArgumentOutOfRangeException("charIndex", Strings.GetString("ArgRange_Array"));
		}
		if (charCount < 0 || charCount > chars.Length - charIndex)
		{
			throw new ArgumentOutOfRangeException("charCount", Strings.GetString("ArgRange_Array"));
		}
		if (byteIndex < 0 || byteIndex > bytes.Length)
		{
			throw new ArgumentOutOfRangeException("byteIndex", Strings.GetString("ArgRange_Array"));
		}
		if (bytes.Length - byteIndex < charCount)
		{
			throw new ArgumentException(Strings.GetString("Arg_InsufficientSpace"), "bytes");
		}
		if (charCount == 0)
		{
			return 0;
		}
		fixed (char* ptr = &(chars != null && chars.Length != 0 ? ref chars[0] : ref *(char*)null))
		{
			fixed (byte* ptr2 = &(bytes != null && bytes.Length != 0 ? ref bytes[0] : ref *(byte*)null))
			{
				return GetBytesImpl((char*)((byte*)ptr + charIndex * 2), charCount, ptr2 + byteIndex, bytes.Length - byteIndex);
			}
		}
	}

	public unsafe override int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
	{
		//IL_00f4->IL00fc: Incompatible stack types: I vs Ref
		if (s == null)
		{
			throw new ArgumentNullException("s");
		}
		if (bytes == null)
		{
			throw new ArgumentNullException("bytes");
		}
		if (charIndex < 0 || charIndex > s.Length)
		{
			throw new ArgumentOutOfRangeException("charIndex", Strings.GetString("ArgRange_StringIndex"));
		}
		if (charCount < 0 || charCount > s.Length - charIndex)
		{
			throw new ArgumentOutOfRangeException("charCount", Strings.GetString("ArgRange_StringRange"));
		}
		if (byteIndex < 0 || byteIndex > bytes.Length)
		{
			throw new ArgumentOutOfRangeException("byteIndex", Strings.GetString("ArgRange_Array"));
		}
		if (bytes.Length - byteIndex < charCount)
		{
			throw new ArgumentException(Strings.GetString("Arg_InsufficientSpace"), "bytes");
		}
		if (charCount == 0 || bytes.Length == byteIndex)
		{
			return 0;
		}
		fixed (char* ptr = s)
		{
			fixed (byte* ptr2 = &(bytes != null && bytes.Length != 0 ? ref bytes[0] : ref *(byte*)null))
			{
				return GetBytesImpl((char*)((byte*)ptr + charIndex * 2), charCount, ptr2 + byteIndex, bytes.Length - byteIndex);
			}
		}
	}

	public unsafe override int GetByteCount(char* chars, int count)
	{
		return GetByteCountImpl(chars, count);
	}

	public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
	{
		return GetBytesImpl(chars, charCount, bytes, byteCount);
	}

	public unsafe abstract int GetByteCountImpl(char* chars, int charCount);

	public unsafe abstract int GetBytesImpl(char* chars, int charCount, byte* bytes, int byteCount);
}
public abstract class MonoEncoder : Encoder
{
	private MonoEncoding encoding;

	public MonoEncoder(MonoEncoding encoding)
	{
		this.encoding = encoding;
	}

	public unsafe override int GetByteCount(char[] chars, int index, int count, bool refresh)
	{
		//IL_007a->IL0081: Incompatible stack types: I vs Ref
		if (chars == null)
		{
			throw new ArgumentNullException("chars");
		}
		if (index < 0 || index > chars.Length)
		{
			throw new ArgumentOutOfRangeException("index", Strings.GetString("ArgRange_Array"));
		}
		if (count < 0 || count > chars.Length - index)
		{
			throw new ArgumentOutOfRangeException("count", Strings.GetString("ArgRange_Array"));
		}
		if (count == 0)
		{
			return 0;
		}
		fixed (char* ptr = &(chars != null && chars.Length != 0 ? ref chars[0] : ref *(char*)null))
		{
			return GetByteCountImpl((char*)((byte*)ptr + index * 2), count, refresh);
		}
	}

	public unsafe override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, bool flush)
	{
		//IL_00d6->IL00dd: Incompatible stack types: I vs Ref
		//IL_00f5->IL00fd: Incompatible stack types: I vs Ref
		if (chars == null)
		{
			throw new ArgumentNullException("chars");
		}
		if (bytes == null)
		{
			throw new ArgumentNullException("bytes");
		}
		if (charIndex < 0 || charIndex > chars.Length)
		{
			throw new ArgumentOutOfRangeException("charIndex", Strings.GetString("ArgRange_Array"));
		}
		if (charCount < 0 || charCount > chars.Length - charIndex)
		{
			throw new ArgumentOutOfRangeException("charCount", Strings.GetString("ArgRange_Array"));
		}
		if (byteIndex < 0 || byteIndex > bytes.Length)
		{
			throw new ArgumentOutOfRangeException("byteIndex", Strings.GetString("ArgRange_Array"));
		}
		if (bytes.Length - byteIndex < charCount)
		{
			throw new ArgumentException(Strings.GetString("Arg_InsufficientSpace"), "bytes");
		}
		if (charCount == 0)
		{
			return 0;
		}
		fixed (char* ptr = &(chars != null && chars.Length != 0 ? ref chars[0] : ref *(char*)null))
		{
			fixed (byte* ptr2 = &(bytes != null && bytes.Length != 0 ? ref bytes[0] : ref *(byte*)null))
			{
				return GetBytesImpl((char*)((byte*)ptr + charIndex * 2), charCount, ptr2 + byteIndex, bytes.Length - byteIndex, flush);
			}
		}
	}

	public unsafe abstract int GetByteCountImpl(char* chars, int charCount, bool refresh);

	public unsafe abstract int GetBytesImpl(char* chars, int charCount, byte* bytes, int byteCount, bool refresh);

	public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, bool flush)
	{
		return GetBytesImpl(chars, charCount, bytes, byteCount, flush);
	}

	public unsafe void HandleFallback(char* chars, ref int charIndex, ref int charCount, byte* bytes, ref int byteIndex, ref int byteCount)
	{
		EncoderFallbackBuffer buffer = base.FallbackBuffer;
		encoding.HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
	}
}
public sealed class Strings
{
	public static string GetString(string tag)
	{
		return tag switch
		{
			"ArgRange_Array" => "Argument index is out of array range.", 
			"Arg_InsufficientSpace" => "Insufficient space in the argument array.", 
			"ArgRange_NonNegative" => "Non-negative value is expected.", 
			"NotSupp_MissingCodeTable" => "This encoding is not supported. Code table is missing.", 
			"ArgRange_StringIndex" => "String index is out of range.", 
			"ArgRange_StringRange" => "String length is out of range.", 
			_ => throw new ArgumentException($"Unexpected error tag name:  {tag}"), 
		};
	}
}
