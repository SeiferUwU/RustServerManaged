using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using I18N.Common;

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
namespace I18N.CJK;

[Serializable]
public class CP932 : MonoEncoding
{
	private const int SHIFTJIS_CODE_PAGE = 932;

	public override string BodyName => "iso-2022-jp";

	public override string EncodingName => "Japanese (Shift-JIS)";

	public override string HeaderName => "iso-2022-jp";

	public override bool IsBrowserDisplay => true;

	public override bool IsBrowserSave => true;

	public override bool IsMailNewsDisplay => true;

	public override bool IsMailNewsSave => true;

	public override string WebName => "shift_jis";

	public override int WindowsCodePage => 932;

	public CP932()
		: base(932)
	{
	}

	public unsafe override int GetByteCountImpl(char* chars, int count)
	{
		int num = 0;
		int num2 = 0;
		byte[] cjkToJis = JISConvert.Convert.cjkToJis;
		byte[] extraToJis = JISConvert.Convert.extraToJis;
		while (count > 0)
		{
			int num3 = *(ushort*)((byte*)chars + num++ * 2);
			count--;
			num2++;
			if (num3 < 128)
			{
				continue;
			}
			if (num3 < 256)
			{
				if (num3 == 162 || num3 == 163 || num3 == 167 || num3 == 168 || num3 == 172 || num3 == 176 || num3 == 177 || num3 == 180 || num3 == 182 || num3 == 215 || num3 == 247)
				{
					num2++;
				}
			}
			else if (num3 >= 913 && num3 <= 1105)
			{
				num2++;
			}
			else if (num3 >= 8208 && num3 <= 40869)
			{
				int num4 = (num3 - 8208) * 2;
				num4 = cjkToJis[num4] | (cjkToJis[num4 + 1] << 8);
				if (num4 >= 256)
				{
					num2++;
				}
			}
			else if (num3 >= 57344 && num3 <= 59223)
			{
				num2++;
			}
			else if (num3 >= 65281 && num3 <= 65519)
			{
				int num4 = (num3 - 65281) * 2;
				num4 = extraToJis[num4] | (extraToJis[num4 + 1] << 8);
				if (num4 >= 256)
				{
					num2++;
				}
			}
		}
		return num2;
	}

	public unsafe override int GetBytesImpl(char* chars, int charCount, byte* bytes, int byteCount)
	{
		int charIndex = 0;
		int num = 0;
		EncoderFallbackBuffer buffer = null;
		int byteIndex = num;
		int num2 = byteCount;
		byte[] cjkToJis = JISConvert.Convert.cjkToJis;
		byte[] greekToJis = JISConvert.Convert.greekToJis;
		byte[] extraToJis = JISConvert.Convert.extraToJis;
		while (charCount > 0)
		{
			int num3 = *(ushort*)((byte*)chars + charIndex++ * 2);
			charCount--;
			if (byteIndex >= num2)
			{
				throw new ArgumentException(Strings.GetString("Arg_InsufficientSpace"), "bytes");
			}
			if (num3 < 128)
			{
				bytes[byteIndex++] = (byte)num3;
				continue;
			}
			if (num3 < 256)
			{
				switch (num3)
				{
				case 162:
				case 163:
				case 167:
				case 168:
				case 172:
				case 176:
				case 177:
				case 180:
				case 182:
				case 215:
				case 247:
					if (byteIndex + 1 >= num2)
					{
						throw new ArgumentException(Strings.GetString("Arg_InsufficientSpace"), "bytes");
					}
					switch (num3)
					{
					case 162:
						bytes[byteIndex++] = 129;
						bytes[byteIndex++] = 145;
						break;
					case 163:
						bytes[byteIndex++] = 129;
						bytes[byteIndex++] = 146;
						break;
					case 167:
						bytes[byteIndex++] = 129;
						bytes[byteIndex++] = 152;
						break;
					case 168:
						bytes[byteIndex++] = 129;
						bytes[byteIndex++] = 78;
						break;
					case 172:
						bytes[byteIndex++] = 129;
						bytes[byteIndex++] = 202;
						break;
					case 176:
						bytes[byteIndex++] = 129;
						bytes[byteIndex++] = 139;
						break;
					case 177:
						bytes[byteIndex++] = 129;
						bytes[byteIndex++] = 125;
						break;
					case 180:
						bytes[byteIndex++] = 129;
						bytes[byteIndex++] = 76;
						break;
					case 182:
						bytes[byteIndex++] = 129;
						bytes[byteIndex++] = 247;
						break;
					case 215:
						bytes[byteIndex++] = 129;
						bytes[byteIndex++] = 126;
						break;
					case 247:
						bytes[byteIndex++] = 129;
						bytes[byteIndex++] = 128;
						break;
					}
					break;
				case 165:
					bytes[byteIndex++] = 92;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				}
				continue;
			}
			int num4;
			if (num3 >= 913 && num3 <= 1105)
			{
				num4 = (num3 - 913) * 2;
				num4 = greekToJis[num4] | (greekToJis[num4 + 1] << 8);
			}
			else if (num3 >= 8208 && num3 <= 40869)
			{
				num4 = (num3 - 8208) * 2;
				num4 = cjkToJis[num4] | (cjkToJis[num4 + 1] << 8);
			}
			else if (num3 >= 57344 && num3 <= 59223)
			{
				int num5 = num3 - 57344;
				num4 = (num5 / 188 << 8) + num5 % 188 + 61504;
				if (num4 % 256 >= 127)
				{
					num4++;
				}
			}
			else if (num3 < 65281 || num3 > 65376)
			{
				num4 = ((num3 >= 65376 && num3 <= 65440) ? (num3 - 65376 + 160) : 0);
			}
			else
			{
				num4 = (num3 - 65281) * 2;
				num4 = extraToJis[num4] | (extraToJis[num4 + 1] << 8);
			}
			if (num4 == 0)
			{
				HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
				continue;
			}
			if (num4 < 256)
			{
				bytes[byteIndex++] = (byte)num4;
				continue;
			}
			if (byteIndex + 1 >= num2)
			{
				throw new ArgumentException(Strings.GetString("Arg_InsufficientSpace"), "bytes");
			}
			if (num4 < 32768)
			{
				num4 -= 256;
				num3 = num4 / 188;
				num4 = num4 % 188 + 64;
				if (num4 >= 127)
				{
					num4++;
				}
				if (num3 < 31)
				{
					bytes[byteIndex++] = (byte)(num3 + 129);
				}
				else
				{
					bytes[byteIndex++] = (byte)(num3 - 31 + 224);
				}
				bytes[byteIndex++] = (byte)num4;
			}
			else if (num4 >= 61504 && num4 <= 63996)
			{
				bytes[byteIndex++] = (byte)(num4 / 256);
				bytes[byteIndex++] = (byte)(num4 % 256);
			}
			else
			{
				bytes[byteIndex++] = 63;
				bytes[byteIndex++] = 63;
			}
		}
		return byteIndex - num;
	}

	public override int GetCharCount(byte[] bytes, int index, int count)
	{
		return new CP932Decoder(JISConvert.Convert).GetCharCount(bytes, index, count, refresh: true);
	}

	public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
	{
		return new CP932Decoder(JISConvert.Convert).GetChars(bytes, byteIndex, byteCount, chars, charIndex, refresh: true);
	}

	public override int GetMaxByteCount(int charCount)
	{
		if (charCount < 0)
		{
			throw new ArgumentOutOfRangeException("charCount", Strings.GetString("ArgRange_NonNegative"));
		}
		return charCount * 2;
	}

	public override int GetMaxCharCount(int byteCount)
	{
		if (byteCount < 0)
		{
			throw new ArgumentOutOfRangeException("byteCount", Strings.GetString("ArgRange_NonNegative"));
		}
		return byteCount;
	}

	public override Decoder GetDecoder()
	{
		return new CP932Decoder(JISConvert.Convert);
	}
}
internal sealed class CP932Decoder : DbcsEncoding.DbcsDecoder
{
	private new JISConvert convert;

	private int last_byte_count;

	private int last_byte_chars;

	public CP932Decoder(JISConvert convert)
		: base(null)
	{
		this.convert = convert;
	}

	public override int GetCharCount(byte[] bytes, int index, int count)
	{
		return GetCharCount(bytes, index, count, refresh: false);
	}

	public override int GetCharCount(byte[] bytes, int index, int count, bool refresh)
	{
		CheckRange(bytes, index, count);
		int num = 0;
		int num2 = last_byte_count;
		while (count > 0)
		{
			int num3 = bytes[index++];
			count--;
			if (num2 == 0)
			{
				if ((num3 >= 129 && num3 <= 159) || (num3 >= 224 && num3 <= 239))
				{
					num2 = num3;
				}
				num++;
			}
			else
			{
				num2 = 0;
			}
		}
		if (refresh)
		{
			if (num2 != 0)
			{
				num++;
			}
			last_byte_count = 0;
		}
		else
		{
			last_byte_count = num2;
		}
		return num;
	}

	public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
	{
		return GetChars(bytes, byteIndex, byteCount, chars, charIndex, refresh: false);
	}

	public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool refresh)
	{
		CheckRange(bytes, byteIndex, byteCount, chars, charIndex);
		int num = charIndex;
		int num2 = chars.Length;
		int num3 = last_byte_chars;
		byte[] jisx0208ToUnicode = convert.jisx0208ToUnicode;
		while (byteCount > 0)
		{
			int num4 = bytes[byteIndex++];
			byteCount--;
			int num5;
			switch (num3)
			{
			case 0:
				if (num >= num2)
				{
					throw new ArgumentException(Strings.GetString("Arg_InsufficientSpace"), "chars");
				}
				if ((num4 >= 129 && num4 <= 159) || (num4 >= 224 && num4 <= 239))
				{
					num3 = num4;
				}
				else if (num4 < 128)
				{
					chars[num++] = (char)num4;
				}
				else if (num4 >= 161 && num4 <= 223)
				{
					chars[num++] = (char)(num4 - 161 + 65377);
				}
				else
				{
					chars[num++] = '?';
				}
				continue;
			case 129:
			case 130:
			case 131:
			case 132:
			case 133:
			case 134:
			case 135:
			case 136:
			case 137:
			case 138:
			case 139:
			case 140:
			case 141:
			case 142:
			case 143:
			case 144:
			case 145:
			case 146:
			case 147:
			case 148:
			case 149:
			case 150:
			case 151:
			case 152:
			case 153:
			case 154:
			case 155:
			case 156:
			case 157:
			case 158:
			case 159:
				num5 = (num3 - 129) * 188;
				break;
			default:
				if (num3 >= 240 && num3 <= 252 && num4 <= 252)
				{
					num5 = 57344 + (num3 - 240) * 188 + num4;
					if (num4 > 127)
					{
						num5--;
					}
				}
				else
				{
					num5 = (num3 - 224 + 31) * 188;
				}
				break;
			}
			num3 = 0;
			if (num4 >= 64 && num4 <= 126)
			{
				num5 += num4 - 64;
			}
			else
			{
				if (num4 < 128 || num4 > 252)
				{
					chars[num++] = '?';
					continue;
				}
				num5 += num4 - 128 + 63;
			}
			num5 *= 2;
			num5 = jisx0208ToUnicode[num5] | (jisx0208ToUnicode[num5 + 1] << 8);
			if (num5 != 0)
			{
				chars[num++] = (char)num5;
			}
			else
			{
				chars[num++] = '?';
			}
		}
		if (refresh)
		{
			if (num3 != 0)
			{
				chars[num++] = '・';
			}
			last_byte_chars = 0;
		}
		else
		{
			last_byte_chars = num3;
		}
		return num - charIndex;
	}
}
[Serializable]
public class ENCshift_jis : CP932
{
}
[Serializable]
public class CP50220 : ISO2022JPEncoding
{
	public override string EncodingName => "Japanese (JIS)";

	public CP50220()
		: base(50220, allow1ByteKana: false, allowShiftIO: false)
	{
	}
}
[Serializable]
public class CP50221 : ISO2022JPEncoding
{
	public override string EncodingName => "Japanese (JIS-Allow 1 byte Kana)";

	public CP50221()
		: base(50221, allow1ByteKana: true, allowShiftIO: false)
	{
	}
}
[Serializable]
public class CP50222 : ISO2022JPEncoding
{
	public override string EncodingName => "Japanese (JIS-Allow 1 byte Kana - SO/SI)";

	public CP50222()
		: base(50222, allow1ByteKana: true, allowShiftIO: true)
	{
	}
}
[Serializable]
public class ISO2022JPEncoding : MonoEncoding
{
	private readonly bool allow_1byte_kana;

	private readonly bool allow_shift_io;

	public override string BodyName => "iso-2022-jp";

	public override string HeaderName => "iso-2022-jp";

	public override string WebName => "csISO2022JP";

	public ISO2022JPEncoding(int codePage, bool allow1ByteKana, bool allowShiftIO)
		: base(codePage, 932)
	{
		allow_1byte_kana = allow1ByteKana;
		allow_shift_io = allowShiftIO;
	}

	public override int GetMaxByteCount(int charCount)
	{
		return charCount / 2 * 5 + 4;
	}

	public override int GetMaxCharCount(int byteCount)
	{
		return byteCount;
	}

	public override int GetByteCount(char[] chars, int charIndex, int charCount)
	{
		return new ISO2022JPEncoder(this, allow_1byte_kana, allow_shift_io).GetByteCount(chars, charIndex, charCount, refresh: true);
	}

	public unsafe override int GetByteCountImpl(char* chars, int count)
	{
		return new ISO2022JPEncoder(this, allow_1byte_kana, allow_shift_io).GetByteCountImpl(chars, count, flush: true);
	}

	public unsafe override int GetBytesImpl(char* chars, int charCount, byte* bytes, int byteCount)
	{
		return new ISO2022JPEncoder(this, allow_1byte_kana, allow_shift_io).GetBytesImpl(chars, charCount, bytes, byteCount, flush: true);
	}

	public override int GetCharCount(byte[] bytes, int index, int count)
	{
		return new ISO2022JPDecoder(allow_1byte_kana, allow_shift_io).GetCharCount(bytes, index, count);
	}

	public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
	{
		return new ISO2022JPDecoder(allow_1byte_kana, allow_shift_io).GetChars(bytes, byteIndex, byteCount, chars, charIndex);
	}
}
internal enum ISO2022JPMode
{
	ASCII,
	JISX0208,
	JISX0201
}
internal class ISO2022JPEncoder : MonoEncoder
{
	private static JISConvert convert = JISConvert.Convert;

	private readonly bool allow_1byte_kana;

	private readonly bool allow_shift_io;

	private ISO2022JPMode m;

	private bool shifted_in_count;

	private bool shifted_in_conv;

	private static readonly char[] full_width_map = new char[65]
	{
		'\0', '。', '「', '」', '、', '・', 'ヲ', 'ァ', 'ィ', 'ゥ',
		'ェ', 'ォ', 'ャ', 'ュ', 'ョ', 'ッ', 'ー', 'ア', 'イ', 'ウ',
		'エ', 'オ', 'カ', 'キ', 'ク', 'ケ', 'コ', 'サ', 'シ', 'ス',
		'セ', 'ソ', 'タ', 'チ', 'ツ', 'テ', 'ト', 'ド', 'ナ', 'ニ',
		'ヌ', 'ネ', 'ハ', 'ヒ', 'フ', 'ヘ', 'ホ', 'マ', 'ミ', 'ム',
		'メ', 'モ', 'ヤ', 'ユ', 'ヨ', 'ラ', 'リ', 'ル', 'レ', 'ロ',
		'ワ', 'ヱ', 'ン', '\u309b', '\u309c'
	};

	public ISO2022JPEncoder(MonoEncoding owner, bool allow1ByteKana, bool allowShiftIO)
		: base(owner)
	{
		allow_1byte_kana = allow1ByteKana;
		allow_shift_io = allowShiftIO;
	}

	public unsafe override int GetByteCountImpl(char* chars, int charCount, bool flush)
	{
		int num = 0;
		int num2 = 0;
		for (int i = num; i < charCount; i++)
		{
			char c = *(char*)((byte*)chars + i * 2);
			if (!allow_1byte_kana && c >= '｠' && c <= 'ﾠ')
			{
				c = full_width_map[c - 65376];
			}
			int num3;
			if (c >= '‐' && c <= '龥')
			{
				if (shifted_in_count)
				{
					shifted_in_count = false;
					num2++;
				}
				if (m != ISO2022JPMode.JISX0208)
				{
					num2 += 3;
				}
				m = ISO2022JPMode.JISX0208;
				num3 = (c - 8208) * 2;
				num3 = convert.cjkToJis[num3] | (convert.cjkToJis[num3 + 1] << 8);
			}
			else if (c >= '！' && c <= '｠')
			{
				if (shifted_in_count)
				{
					shifted_in_count = false;
					num2++;
				}
				if (m != ISO2022JPMode.JISX0208)
				{
					num2 += 3;
				}
				m = ISO2022JPMode.JISX0208;
				num3 = (c - 65281) * 2;
				num3 = convert.extraToJis[num3] | (convert.extraToJis[num3 + 1] << 8);
			}
			else if (c >= '｠' && c <= 'ﾠ')
			{
				if (allow_shift_io)
				{
					if (!shifted_in_count)
					{
						num2++;
						shifted_in_count = true;
					}
				}
				else if (m != ISO2022JPMode.JISX0201)
				{
					num2 += 3;
					m = ISO2022JPMode.JISX0201;
				}
				num3 = c - 65376 + 160;
			}
			else
			{
				if (c >= '\u0080')
				{
					continue;
				}
				if (shifted_in_count)
				{
					shifted_in_count = false;
					num2++;
				}
				if (m != ISO2022JPMode.ASCII)
				{
					num2 += 3;
				}
				m = ISO2022JPMode.ASCII;
				num3 = c;
			}
			num2 = ((num3 <= 256) ? (num2 + 1) : (num2 + 2));
		}
		if (flush)
		{
			if (shifted_in_count)
			{
				shifted_in_count = false;
				num2++;
			}
			if (m != ISO2022JPMode.ASCII)
			{
				num2 += 3;
			}
			m = ISO2022JPMode.ASCII;
		}
		return num2;
	}

	private unsafe void SwitchMode(byte* bytes, ref int byteIndex, ref int byteCount, ref ISO2022JPMode cur, ISO2022JPMode next)
	{
		if (cur != next)
		{
			if (byteCount <= 3)
			{
				throw new ArgumentOutOfRangeException("Insufficient byte buffer.");
			}
			bytes[byteIndex++] = 27;
			switch (next)
			{
			case ISO2022JPMode.JISX0201:
				bytes[byteIndex++] = 40;
				bytes[byteIndex++] = 73;
				break;
			case ISO2022JPMode.JISX0208:
				bytes[byteIndex++] = 36;
				bytes[byteIndex++] = 66;
				break;
			default:
				bytes[byteIndex++] = 40;
				bytes[byteIndex++] = 66;
				break;
			}
			cur = next;
		}
	}

	public unsafe override int GetBytesImpl(char* chars, int charCount, byte* bytes, int byteCount, bool flush)
	{
		int num = 0;
		int byteIndex = 0;
		int num2 = byteIndex;
		int num3 = num + charCount;
		for (int i = num; i < num3; i++, charCount--)
		{
			char c = *(char*)((byte*)chars + i * 2);
			if (!allow_1byte_kana && c >= '｠' && c <= 'ﾠ')
			{
				c = full_width_map[c - 65376];
			}
			int num4;
			if (c >= '‐' && c <= '龥')
			{
				if (shifted_in_conv)
				{
					bytes[byteIndex++] = 15;
					shifted_in_conv = false;
					byteCount--;
				}
				ISO2022JPMode iSO2022JPMode = m;
				if (iSO2022JPMode != ISO2022JPMode.JISX0208)
				{
					SwitchMode(bytes, ref byteIndex, ref byteCount, ref m, ISO2022JPMode.JISX0208);
				}
				num4 = (c - 8208) * 2;
				num4 = convert.cjkToJis[num4] | (convert.cjkToJis[num4 + 1] << 8);
			}
			else if (c >= '！' && c <= '｠')
			{
				if (shifted_in_conv)
				{
					bytes[byteIndex++] = 15;
					shifted_in_conv = false;
					byteCount--;
				}
				ISO2022JPMode iSO2022JPMode = m;
				if (iSO2022JPMode != ISO2022JPMode.JISX0208)
				{
					SwitchMode(bytes, ref byteIndex, ref byteCount, ref m, ISO2022JPMode.JISX0208);
				}
				num4 = (c - 65281) * 2;
				num4 = convert.extraToJis[num4] | (convert.extraToJis[num4 + 1] << 8);
			}
			else if (c >= '｠' && c <= 'ﾠ')
			{
				if (allow_shift_io)
				{
					if (!shifted_in_conv)
					{
						bytes[byteIndex++] = 14;
						shifted_in_conv = true;
						byteCount--;
					}
				}
				else
				{
					ISO2022JPMode iSO2022JPMode = m;
					if (iSO2022JPMode != ISO2022JPMode.JISX0201)
					{
						SwitchMode(bytes, ref byteIndex, ref byteCount, ref m, ISO2022JPMode.JISX0201);
					}
				}
				num4 = c - 65344;
			}
			else
			{
				if (c >= '\u0080')
				{
					HandleFallback(chars, ref i, ref charCount, bytes, ref byteIndex, ref byteCount);
					continue;
				}
				if (shifted_in_conv)
				{
					bytes[byteIndex++] = 15;
					shifted_in_conv = false;
					byteCount--;
				}
				SwitchMode(bytes, ref byteIndex, ref byteCount, ref m, ISO2022JPMode.ASCII);
				num4 = c;
			}
			if (num4 > 256)
			{
				num4 -= 256;
				bytes[byteIndex++] = (byte)(num4 / 94 + 33);
				bytes[byteIndex++] = (byte)(num4 % 94 + 33);
				byteCount -= 2;
			}
			else
			{
				bytes[byteIndex++] = (byte)num4;
				byteCount--;
			}
		}
		if (flush)
		{
			if (shifted_in_conv)
			{
				bytes[byteIndex++] = 15;
				shifted_in_conv = false;
				byteCount--;
			}
			if (m != ISO2022JPMode.ASCII)
			{
				SwitchMode(bytes, ref byteIndex, ref byteCount, ref m, ISO2022JPMode.ASCII);
			}
		}
		return byteIndex - num2;
	}

	public override void Reset()
	{
		m = ISO2022JPMode.ASCII;
		shifted_in_conv = (shifted_in_count = false);
	}
}
internal class ISO2022JPDecoder : Decoder
{
	private static JISConvert convert = JISConvert.Convert;

	private readonly bool allow_shift_io;

	private ISO2022JPMode m;

	private bool shifted_in_conv;

	private bool shifted_in_count;

	public ISO2022JPDecoder(bool allow1ByteKana, bool allowShiftIO)
	{
		allow_shift_io = allowShiftIO;
	}

	public override int GetCharCount(byte[] bytes, int index, int count)
	{
		int num = 0;
		int num2 = index + count;
		for (int i = index; i < num2; i++)
		{
			if (allow_shift_io)
			{
				switch (bytes[i])
				{
				case 15:
					shifted_in_count = false;
					continue;
				case 14:
					shifted_in_count = true;
					continue;
				}
			}
			if (bytes[i] != 27)
			{
				if (!shifted_in_count && m == ISO2022JPMode.JISX0208)
				{
					if (i + 1 == num2)
					{
						break;
					}
					num++;
					i++;
				}
				else
				{
					num++;
				}
				continue;
			}
			if (i + 2 >= num2)
			{
				break;
			}
			i++;
			bool flag = false;
			if (bytes[i] == 36)
			{
				flag = true;
			}
			else
			{
				if (bytes[i] != 40)
				{
					num += 2;
					continue;
				}
				flag = false;
			}
			i++;
			if (bytes[i] == 66)
			{
				m = (flag ? ISO2022JPMode.JISX0208 : ISO2022JPMode.ASCII);
			}
			else if (bytes[i] == 74)
			{
				m = ISO2022JPMode.ASCII;
			}
			else if (bytes[i] == 73)
			{
				m = ISO2022JPMode.JISX0201;
			}
			else
			{
				num += 3;
			}
		}
		return num;
	}

	private int ToChar(int value)
	{
		value <<= 1;
		return (value + 1 < convert.jisx0208ToUnicode.Length && value >= 0) ? (convert.jisx0208ToUnicode[value] | (convert.jisx0208ToUnicode[value + 1] << 8)) : (-1);
	}

	public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
	{
		int num = charIndex;
		int num2 = byteIndex + byteCount;
		for (int i = byteIndex; i < num2 && charIndex < chars.Length; i++)
		{
			if (allow_shift_io)
			{
				switch (bytes[i])
				{
				case 15:
					shifted_in_conv = false;
					continue;
				case 14:
					shifted_in_conv = true;
					continue;
				}
			}
			if (bytes[i] != 27)
			{
				if (shifted_in_conv || m == ISO2022JPMode.JISX0201)
				{
					if (bytes[i] < 96)
					{
						chars[charIndex++] = (char)(bytes[i] + 65344);
					}
					else
					{
						chars[charIndex++] = '?';
					}
				}
				else if (m == ISO2022JPMode.JISX0208)
				{
					if (i + 1 == num2)
					{
						break;
					}
					int num3 = (bytes[i] - 1 >> 1) + ((bytes[i] > 94) ? 177 : 113);
					int num4 = bytes[i + 1] + (((bytes[i] & 1) == 0) ? 126 : 32);
					int num5 = (num3 - 129) * 188;
					num5 += num4 - 65;
					int num6 = ToChar(num5);
					if (num6 < 0)
					{
						chars[charIndex++] = '?';
					}
					else
					{
						chars[charIndex++] = (char)num6;
					}
					i++;
				}
				else if (bytes[i] > 160 && bytes[i] < 224)
				{
					chars[charIndex++] = (char)(bytes[i] - 160 + 65376);
				}
				else
				{
					chars[charIndex++] = (char)bytes[i];
				}
				continue;
			}
			if (i + 2 >= num2)
			{
				break;
			}
			i++;
			bool flag = false;
			if (bytes[i] == 36)
			{
				flag = true;
			}
			else
			{
				if (bytes[i] != 40)
				{
					chars[charIndex++] = '\u001b';
					chars[charIndex++] = (char)bytes[i];
					continue;
				}
				flag = false;
			}
			i++;
			if (bytes[i] == 66)
			{
				m = (flag ? ISO2022JPMode.JISX0208 : ISO2022JPMode.ASCII);
				continue;
			}
			if (bytes[i] == 74)
			{
				m = ISO2022JPMode.ASCII;
				continue;
			}
			if (bytes[i] == 73)
			{
				m = ISO2022JPMode.JISX0201;
				continue;
			}
			chars[charIndex++] = '\u001b';
			chars[charIndex++] = (char)bytes[i - 1];
			chars[charIndex++] = (char)bytes[i];
		}
		return charIndex - num;
	}

	public override void Reset()
	{
		m = ISO2022JPMode.ASCII;
		shifted_in_count = (shifted_in_conv = false);
	}
}
[Serializable]
public class ENCiso_2022_jp : CP50220
{
}
[Serializable]
public class CP51932 : MonoEncoding
{
	private const int EUC_JP_CODE_PAGE = 51932;

	public override string BodyName => "euc-jp";

	public override string EncodingName => "Japanese (EUC)";

	public override string HeaderName => "euc-jp";

	public override bool IsBrowserDisplay => true;

	public override bool IsBrowserSave => true;

	public override bool IsMailNewsDisplay => true;

	public override bool IsMailNewsSave => true;

	public override string WebName => "euc-jp";

	public CP51932()
		: base(51932, 932)
	{
	}

	public override int GetByteCount(char[] chars, int index, int length)
	{
		return new CP51932Encoder(this).GetByteCount(chars, index, length, refresh: true);
	}

	public unsafe override int GetByteCountImpl(char* chars, int count)
	{
		return new CP51932Encoder(this).GetByteCountImpl(chars, count, refresh: true);
	}

	public unsafe override int GetBytesImpl(char* chars, int charCount, byte* bytes, int byteCount)
	{
		return new CP51932Encoder(this).GetBytesImpl(chars, charCount, bytes, byteCount, refresh: true);
	}

	public override int GetCharCount(byte[] bytes, int index, int count)
	{
		return new CP51932Decoder().GetCharCount(bytes, index, count, refresh: true);
	}

	public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
	{
		return new CP51932Decoder().GetChars(bytes, byteIndex, byteCount, chars, charIndex, refresh: true);
	}

	public override int GetMaxByteCount(int charCount)
	{
		if (charCount < 0)
		{
			throw new ArgumentOutOfRangeException("charCount", Strings.GetString("ArgRange_NonNegative"));
		}
		return charCount * 3;
	}

	public override int GetMaxCharCount(int byteCount)
	{
		if (byteCount < 0)
		{
			throw new ArgumentOutOfRangeException("byteCount", Strings.GetString("ArgRange_NonNegative"));
		}
		return byteCount;
	}

	public override Encoder GetEncoder()
	{
		return new CP51932Encoder(this);
	}

	public override Decoder GetDecoder()
	{
		return new CP51932Decoder();
	}
}
public class CP51932Encoder : MonoEncoder
{
	public CP51932Encoder(MonoEncoding encoding)
		: base(encoding)
	{
	}

	public unsafe override int GetByteCountImpl(char* chars, int count, bool refresh)
	{
		int num = 0;
		int num2 = 0;
		byte[] cjkToJis = JISConvert.Convert.cjkToJis;
		byte[] extraToJis = JISConvert.Convert.extraToJis;
		while (count > 0)
		{
			int num3 = *(ushort*)((byte*)chars + num++ * 2);
			count--;
			num2++;
			if (num3 < 128)
			{
				continue;
			}
			if (num3 < 256)
			{
				if (num3 == 162 || num3 == 163 || num3 == 167 || num3 == 168 || num3 == 172 || num3 == 176 || num3 == 177 || num3 == 180 || num3 == 182 || num3 == 215 || num3 == 247)
				{
					num2++;
				}
			}
			else if (num3 >= 913 && num3 <= 1105)
			{
				num2++;
			}
			else if (num3 >= 8208 && num3 <= 40869)
			{
				int num4 = (num3 - 8208) * 2;
				num4 = cjkToJis[num4] | (cjkToJis[num4 + 1] << 8);
				if (num4 >= 256)
				{
					num2++;
				}
			}
			else if (num3 >= 65281 && num3 < 65376)
			{
				int num4 = (num3 - 65281) * 2;
				num4 = extraToJis[num4] | (extraToJis[num4 + 1] << 8);
				if (num4 >= 256)
				{
					num2++;
				}
			}
			else if (num3 >= 65376 && num3 <= 65440)
			{
				num2++;
			}
		}
		return num2;
	}

	public unsafe override int GetBytesImpl(char* chars, int charCount, byte* bytes, int byteCount, bool refresh)
	{
		int charIndex = 0;
		int num = 0;
		int byteIndex = num;
		int num2 = byteCount;
		byte[] cjkToJis = JISConvert.Convert.cjkToJis;
		byte[] greekToJis = JISConvert.Convert.greekToJis;
		byte[] extraToJis = JISConvert.Convert.extraToJis;
		while (charCount > 0)
		{
			int num3 = *(ushort*)((byte*)chars + charIndex * 2);
			if (byteIndex >= num2)
			{
				throw new ArgumentException(Strings.GetString("Arg_InsufficientSpace"), "bytes");
			}
			if (num3 < 128)
			{
				bytes[byteIndex++] = (byte)num3;
			}
			else
			{
				int num4;
				if (num3 >= 913 && num3 <= 1105)
				{
					num4 = (num3 - 913) * 2;
					num4 = greekToJis[num4] | (greekToJis[num4 + 1] << 8);
				}
				else if (num3 >= 8208 && num3 <= 40869)
				{
					num4 = (num3 - 8208) * 2;
					num4 = cjkToJis[num4] | (cjkToJis[num4 + 1] << 8);
				}
				else if (num3 < 65281 || num3 > 65376)
				{
					num4 = ((num3 >= 65376 && num3 <= 65440) ? (num3 - 65376 + 36512) : 0);
				}
				else
				{
					num4 = (num3 - 65281) * 2;
					num4 = extraToJis[num4] | (extraToJis[num4 + 1] << 8);
				}
				if (num4 == 0)
				{
					HandleFallback(chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
				}
				else if (num4 < 256)
				{
					bytes[byteIndex++] = (byte)num4;
				}
				else
				{
					if (byteIndex + 1 >= num2)
					{
						throw new ArgumentException(Strings.GetString("Arg_InsufficientSpace"), "bytes");
					}
					if (num4 < 32768)
					{
						num4 -= 256;
						bytes[byteIndex++] = (byte)(num4 / 94 + 161);
						bytes[byteIndex++] = (byte)(num4 % 94 + 161);
					}
					else
					{
						bytes[byteIndex++] = 142;
						bytes[byteIndex++] = (byte)(num4 - 36352);
					}
				}
			}
			charIndex++;
			charCount--;
		}
		return byteIndex - num;
	}
}
internal class CP51932Decoder : DbcsEncoding.DbcsDecoder
{
	private int last_count;

	private int last_bytes;

	public CP51932Decoder()
		: base(null)
	{
	}

	public override int GetCharCount(byte[] bytes, int index, int count)
	{
		return GetCharCount(bytes, index, count, refresh: false);
	}

	public override int GetCharCount(byte[] bytes, int index, int count, bool refresh)
	{
		CheckRange(bytes, index, count);
		int num = 0;
		byte[] jisx0208ToUnicode = JISConvert.Convert.jisx0208ToUnicode;
		byte[] jisx0212ToUnicode = JISConvert.Convert.jisx0212ToUnicode;
		int num2 = 0;
		int num3 = 0;
		int num4 = last_count;
		while (count > 0)
		{
			num3 = bytes[index++];
			count--;
			switch (num4)
			{
			case 0:
				if (num3 == 143)
				{
					if (num3 != 0)
					{
						num4 = 0;
						num2++;
					}
					else
					{
						num4 = num3;
					}
					continue;
				}
				if (num3 <= 127)
				{
					num2++;
					continue;
				}
				switch (num3)
				{
				case 142:
					num4 = num3;
					break;
				case 161:
				case 162:
				case 163:
				case 164:
				case 165:
				case 166:
				case 167:
				case 168:
				case 169:
				case 170:
				case 171:
				case 172:
				case 173:
				case 174:
				case 175:
				case 176:
				case 177:
				case 178:
				case 179:
				case 180:
				case 181:
				case 182:
				case 183:
				case 184:
				case 185:
				case 186:
				case 187:
				case 188:
				case 189:
				case 190:
				case 191:
				case 192:
				case 193:
				case 194:
				case 195:
				case 196:
				case 197:
				case 198:
				case 199:
				case 200:
				case 201:
				case 202:
				case 203:
				case 204:
				case 205:
				case 206:
				case 207:
				case 208:
				case 209:
				case 210:
				case 211:
				case 212:
				case 213:
				case 214:
				case 215:
				case 216:
				case 217:
				case 218:
				case 219:
				case 220:
				case 221:
				case 222:
				case 223:
				case 224:
				case 225:
				case 226:
				case 227:
				case 228:
				case 229:
				case 230:
				case 231:
				case 232:
				case 233:
				case 234:
				case 235:
				case 236:
				case 237:
				case 238:
				case 239:
				case 240:
				case 241:
				case 242:
				case 243:
				case 244:
				case 245:
				case 246:
				case 247:
				case 248:
				case 249:
				case 250:
				case 251:
				case 252:
				case 253:
				case 254:
					num4 = num3;
					break;
				default:
					num2++;
					break;
				}
				continue;
			case 142:
				if (num3 >= 161 && num3 <= 223)
				{
					num = (num3 - 64) | (num4 + 113 << 8);
					num2++;
				}
				else
				{
					num2++;
				}
				num4 = 0;
				continue;
			case 143:
				num4 = num3;
				continue;
			}
			num = (num4 - 161) * 94;
			num4 = 0;
			if (num3 >= 161 && num3 <= 254)
			{
				num += num3 - 161;
				num *= 2;
				num = jisx0208ToUnicode[num] | (jisx0208ToUnicode[num + 1] << 8);
				if (num == 0)
				{
					num = jisx0212ToUnicode[num] | (jisx0212ToUnicode[num + 1] << 8);
				}
				num2 = ((num == 0) ? (num2 + 1) : (num2 + 1));
			}
			else
			{
				num4 = 0;
				num2++;
			}
		}
		if (refresh && num4 != 0)
		{
			num2++;
		}
		else
		{
			last_count = num4;
		}
		return num2;
	}

	public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
	{
		return GetChars(bytes, byteIndex, byteCount, chars, charIndex, refresh: false);
	}

	public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool refresh)
	{
		CheckRange(bytes, byteIndex, byteCount, chars, charIndex);
		int num = charIndex;
		int num2 = chars.Length;
		int num3 = last_bytes;
		byte[] jisx0208ToUnicode = JISConvert.Convert.jisx0208ToUnicode;
		byte[] jisx0212ToUnicode = JISConvert.Convert.jisx0212ToUnicode;
		while (byteCount > 0)
		{
			int num4 = bytes[byteIndex++];
			byteCount--;
			int num5;
			switch (num3)
			{
			case 0:
				if (num4 == 143)
				{
					if (num4 != 0)
					{
						num3 = 0;
						if (num >= num2)
						{
							throw Insufficient();
						}
						chars[num++] = '・';
					}
					else
					{
						num3 = num4;
					}
					continue;
				}
				if (num4 <= 127)
				{
					if (num >= num2)
					{
						throw Insufficient();
					}
					chars[num++] = (char)num4;
					continue;
				}
				switch (num4)
				{
				case 142:
					num3 = num4;
					break;
				case 161:
				case 162:
				case 163:
				case 164:
				case 165:
				case 166:
				case 167:
				case 168:
				case 169:
				case 170:
				case 171:
				case 172:
				case 173:
				case 174:
				case 175:
				case 176:
				case 177:
				case 178:
				case 179:
				case 180:
				case 181:
				case 182:
				case 183:
				case 184:
				case 185:
				case 186:
				case 187:
				case 188:
				case 189:
				case 190:
				case 191:
				case 192:
				case 193:
				case 194:
				case 195:
				case 196:
				case 197:
				case 198:
				case 199:
				case 200:
				case 201:
				case 202:
				case 203:
				case 204:
				case 205:
				case 206:
				case 207:
				case 208:
				case 209:
				case 210:
				case 211:
				case 212:
				case 213:
				case 214:
				case 215:
				case 216:
				case 217:
				case 218:
				case 219:
				case 220:
				case 221:
				case 222:
				case 223:
				case 224:
				case 225:
				case 226:
				case 227:
				case 228:
				case 229:
				case 230:
				case 231:
				case 232:
				case 233:
				case 234:
				case 235:
				case 236:
				case 237:
				case 238:
				case 239:
				case 240:
				case 241:
				case 242:
				case 243:
				case 244:
				case 245:
				case 246:
				case 247:
				case 248:
				case 249:
				case 250:
				case 251:
				case 252:
				case 253:
				case 254:
					num3 = num4;
					break;
				default:
					if (num >= num2)
					{
						throw Insufficient();
					}
					chars[num++] = '・';
					break;
				}
				continue;
			case 142:
				if (num4 >= 161 && num4 <= 223)
				{
					num5 = (num4 - 64) | (num3 + 113 << 8);
					if (num >= num2)
					{
						throw Insufficient();
					}
					chars[num++] = (char)num5;
				}
				else
				{
					if (num >= num2)
					{
						throw Insufficient();
					}
					chars[num++] = '・';
				}
				num3 = 0;
				continue;
			case 143:
				num3 = num4;
				continue;
			}
			num5 = (num3 - 161) * 94;
			num3 = 0;
			if (num4 >= 161 && num4 <= 254)
			{
				num5 += num4 - 161;
				num5 *= 2;
				num5 = jisx0208ToUnicode[num5] | (jisx0208ToUnicode[num5 + 1] << 8);
				if (num5 == 0)
				{
					num5 = jisx0212ToUnicode[num5] | (jisx0212ToUnicode[num5 + 1] << 8);
				}
				if (num >= num2)
				{
					throw Insufficient();
				}
				if (num5 != 0)
				{
					chars[num++] = (char)num5;
				}
				else
				{
					chars[num++] = '・';
				}
			}
			else
			{
				num3 = 0;
				if (num >= num2)
				{
					throw Insufficient();
				}
				chars[num++] = '・';
			}
		}
		if (refresh && num3 != 0)
		{
			if (num >= num2)
			{
				throw Insufficient();
			}
			chars[num++] = '・';
		}
		else
		{
			last_bytes = num3;
		}
		return num - charIndex;
	}

	private Exception Insufficient()
	{
		throw new ArgumentException(Strings.GetString("Arg_InsufficientSpace"), "chars");
	}
}
[Serializable]
public class ENCeuc_jp : CP51932
{
}
[Serializable]
internal class CP936 : DbcsEncoding
{
	private const int GB2312_CODE_PAGE = 936;

	public override string BodyName => "gb2312";

	public override string EncodingName => "Chinese Simplified (GB2312)";

	public override string HeaderName => "gb2312";

	public override bool IsBrowserDisplay => true;

	public override bool IsBrowserSave => true;

	public override bool IsMailNewsDisplay => true;

	public override bool IsMailNewsSave => true;

	public override string WebName => "gb2312";

	public CP936()
		: base(936)
	{
	}

	internal override DbcsConvert GetConvert()
	{
		return DbcsConvert.Gb2312;
	}

	public unsafe override int GetByteCountImpl(char* chars, int count)
	{
		DbcsConvert convert = GetConvert();
		int num = 0;
		int num2 = 0;
		while (count-- > 0)
		{
			char c = *(char*)((byte*)chars + num++ * 2);
			if (c <= '\u0080' || c == 'ÿ')
			{
				num2++;
				continue;
			}
			byte b = convert.u2n[c * 2 + 1];
			byte b2 = convert.u2n[c * 2];
			if (b != 0 || b2 != 0)
			{
				num2 += 2;
			}
		}
		return num2;
	}

	public unsafe override int GetBytesImpl(char* chars, int charCount, byte* bytes, int byteCount)
	{
		DbcsConvert convert = GetConvert();
		int charIndex = 0;
		int byteIndex = 0;
		EncoderFallbackBuffer buffer = null;
		int num = byteIndex;
		while (charCount-- > 0)
		{
			char c = *(char*)((byte*)chars + charIndex++ * 2);
			if (c <= '\u0080' || c == 'ÿ')
			{
				bytes[byteIndex++] = (byte)c;
				continue;
			}
			byte b = convert.u2n[c * 2 + 1];
			byte b2 = convert.u2n[c * 2];
			if (b == 0 && b2 == 0)
			{
				HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
				continue;
			}
			bytes[byteIndex++] = b;
			bytes[byteIndex++] = b2;
		}
		return byteIndex - num;
	}

	public override int GetCharCount(byte[] bytes, int index, int count)
	{
		return GetDecoder().GetCharCount(bytes, index, count);
	}

	public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
	{
		return GetDecoder().GetChars(bytes, byteIndex, byteCount, chars, charIndex);
	}

	public override Decoder GetDecoder()
	{
		return new CP936Decoder(GetConvert());
	}
}
internal sealed class CP936Decoder : DbcsEncoding.DbcsDecoder
{
	private int last_byte_count;

	private int last_byte_bytes;

	public CP936Decoder(DbcsConvert convert)
		: base(convert)
	{
	}

	public override int GetCharCount(byte[] bytes, int index, int count)
	{
		return GetCharCount(bytes, index, count, refresh: false);
	}

	public override int GetCharCount(byte[] bytes, int index, int count, bool refresh)
	{
		CheckRange(bytes, index, count);
		int num = last_byte_count;
		last_byte_count = 0;
		int num2 = 0;
		while (count-- > 0)
		{
			int num3 = bytes[index++];
			if (num == 0)
			{
				if (num3 <= 128 || num3 == 255)
				{
					num2++;
				}
				else
				{
					num = num3;
				}
			}
			else
			{
				num2++;
				num = 0;
			}
		}
		if (num != 0)
		{
			if (refresh)
			{
				num2++;
				last_byte_count = 0;
			}
			else
			{
				last_byte_count = num;
			}
		}
		return num2;
	}

	public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
	{
		return GetChars(bytes, byteIndex, byteCount, chars, charIndex, refresh: false);
	}

	public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool refresh)
	{
		CheckRange(bytes, byteIndex, byteCount, chars, charIndex);
		int num = charIndex;
		int num2 = last_byte_bytes;
		last_byte_bytes = 0;
		while (byteCount-- > 0)
		{
			int num3 = bytes[byteIndex++];
			if (num2 == 0)
			{
				if (num3 <= 128 || num3 == 255)
				{
					chars[charIndex++] = (char)num3;
				}
				else if (num3 >= 129 && num3 < 255)
				{
					num2 = num3;
				}
				continue;
			}
			int num4 = ((num2 - 129) * 191 + num3 - 64) * 2;
			char c = ((num4 >= 0 && num4 < convert.n2u.Length) ? ((char)(convert.n2u[num4] + convert.n2u[num4 + 1] * 256)) : '\0');
			if (c == '\0')
			{
				chars[charIndex++] = '?';
			}
			else
			{
				chars[charIndex++] = c;
			}
			num2 = 0;
		}
		if (num2 != 0)
		{
			if (refresh)
			{
				chars[charIndex++] = '?';
				last_byte_bytes = 0;
			}
			else
			{
				last_byte_bytes = num2;
			}
		}
		return charIndex - num;
	}
}
[Serializable]
internal class ENCgb2312 : CP936
{
}
[Serializable]
internal class CP949 : KoreanEncoding
{
	private const int UHC_CODE_PAGE = 949;

	public override string BodyName => "ks_c_5601-1987";

	public override string EncodingName => "Korean (UHC)";

	public override string HeaderName => "ks_c_5601-1987";

	public override string WebName => "ks_c_5601-1987";

	public CP949()
		: base(949, useUHC: true)
	{
	}
}
[Serializable]
internal class CP51949 : KoreanEncoding
{
	private const int EUCKR_CODE_PAGE = 51949;

	public override string BodyName => "euc-kr";

	public override string EncodingName => "Korean (EUC)";

	public override string HeaderName => "euc-kr";

	public override string WebName => "euc-kr";

	public CP51949()
		: base(51949, useUHC: false)
	{
	}
}
[Serializable]
internal class KoreanEncoding : DbcsEncoding
{
	private sealed class KoreanDecoder : DbcsDecoder
	{
		private bool useUHC;

		private int last_byte_count;

		private int last_byte_conv;

		public KoreanDecoder(DbcsConvert convert, bool useUHC)
			: base(convert)
		{
			this.useUHC = useUHC;
		}

		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return GetCharCount(bytes, index, count, refresh: false);
		}

		public override int GetCharCount(byte[] bytes, int index, int count, bool refresh)
		{
			CheckRange(bytes, index, count);
			int num = last_byte_count;
			last_byte_count = 0;
			int num2 = 0;
			while (count-- > 0)
			{
				int num3 = bytes[index++];
				if (num == 0)
				{
					if (num3 <= 128 || num3 == 255)
					{
						num2++;
					}
					else
					{
						num = num3;
					}
					continue;
				}
				char c;
				if (useUHC && num < 161)
				{
					int num4 = 8836 + (num - 129) * 178;
					num4 = ((num3 >= 65 && num3 <= 90) ? (num4 + (num3 - 65)) : ((num3 >= 97 && num3 <= 122) ? (num4 + (num3 - 97 + 26)) : ((num3 < 129 || num3 > 254) ? (-1) : (num4 + (num3 - 129 + 52)))));
					c = ((num4 >= 0 && num4 * 2 <= convert.n2u.Length) ? ((char)(convert.n2u[num4 * 2] + convert.n2u[num4 * 2 + 1] * 256)) : '\0');
				}
				else if (useUHC && num <= 198 && num3 < 161)
				{
					int num5 = 14532 + (num - 161) * 84;
					num5 = ((num3 >= 65 && num3 <= 90) ? (num5 + (num3 - 65)) : ((num3 >= 97 && num3 <= 122) ? (num5 + (num3 - 97 + 26)) : ((num3 < 129 || num3 > 160) ? (-1) : (num5 + (num3 - 129 + 52)))));
					c = ((num5 >= 0 && num5 * 2 <= convert.n2u.Length) ? ((char)(convert.n2u[num5 * 2] + convert.n2u[num5 * 2 + 1] * 256)) : '\0');
				}
				else if (num3 >= 161 && num3 <= 254)
				{
					int num6 = ((num - 161) * 94 + num3 - 161) * 2;
					c = ((num6 >= 0 && num6 < convert.n2u.Length) ? ((char)(convert.n2u[num6] + convert.n2u[num6 + 1] * 256)) : '\0');
				}
				else
				{
					c = '\0';
				}
				num2 = ((c != 0) ? (num2 + 1) : (num2 + 1));
				num = 0;
			}
			if (num != 0)
			{
				if (refresh)
				{
					num2++;
					last_byte_count = 0;
				}
				else
				{
					last_byte_count = num;
				}
			}
			return num2;
		}

		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			return GetChars(bytes, byteIndex, byteCount, chars, charIndex, refresh: false);
		}

		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool refresh)
		{
			CheckRange(bytes, byteIndex, byteCount, chars, charIndex);
			int num = charIndex;
			int num2 = last_byte_conv;
			last_byte_conv = 0;
			while (byteCount-- > 0)
			{
				int num3 = bytes[byteIndex++];
				if (num2 == 0)
				{
					if (num3 <= 128 || num3 == 255)
					{
						chars[charIndex++] = (char)num3;
					}
					else
					{
						num2 = num3;
					}
					continue;
				}
				char c;
				if (useUHC && num2 < 161)
				{
					int num4 = 8836 + (num2 - 129) * 178;
					num4 = ((num3 >= 65 && num3 <= 90) ? (num4 + (num3 - 65)) : ((num3 >= 97 && num3 <= 122) ? (num4 + (num3 - 97 + 26)) : ((num3 < 129 || num3 > 254) ? (-1) : (num4 + (num3 - 129 + 52)))));
					c = ((num4 >= 0 && num4 * 2 <= convert.n2u.Length) ? ((char)(convert.n2u[num4 * 2] + convert.n2u[num4 * 2 + 1] * 256)) : '\0');
				}
				else if (useUHC && num2 <= 198 && num3 < 161)
				{
					int num5 = 14532 + (num2 - 161) * 84;
					num5 = ((num3 >= 65 && num3 <= 90) ? (num5 + (num3 - 65)) : ((num3 >= 97 && num3 <= 122) ? (num5 + (num3 - 97 + 26)) : ((num3 < 129 || num3 > 160) ? (-1) : (num5 + (num3 - 129 + 52)))));
					c = ((num5 >= 0 && num5 * 2 <= convert.n2u.Length) ? ((char)(convert.n2u[num5 * 2] + convert.n2u[num5 * 2 + 1] * 256)) : '\0');
				}
				else if (num3 >= 161 && num3 <= 254)
				{
					int num6 = ((num2 - 161) * 94 + num3 - 161) * 2;
					c = ((num6 >= 0 && num6 < convert.n2u.Length) ? ((char)(convert.n2u[num6] + convert.n2u[num6 + 1] * 256)) : '\0');
				}
				else
				{
					c = '\0';
				}
				if (c == '\0')
				{
					chars[charIndex++] = '?';
				}
				else
				{
					chars[charIndex++] = c;
				}
				num2 = 0;
			}
			if (num2 != 0)
			{
				if (refresh)
				{
					chars[charIndex++] = '?';
					last_byte_conv = 0;
				}
				else
				{
					last_byte_conv = num2;
				}
			}
			return charIndex - num;
		}
	}

	private bool useUHC;

	public KoreanEncoding(int codepage, bool useUHC)
		: base(codepage, 949)
	{
		this.useUHC = useUHC;
	}

	internal override DbcsConvert GetConvert()
	{
		return DbcsConvert.KS;
	}

	public unsafe override int GetByteCountImpl(char* chars, int count)
	{
		int num = 0;
		int num2 = 0;
		DbcsConvert convert = GetConvert();
		while (count-- > 0)
		{
			char c = *(char*)((byte*)chars + num++ * 2);
			if (c <= '\u0080' || c == 'ÿ')
			{
				num2++;
				continue;
			}
			byte b = convert.u2n[c * 2];
			byte b2 = convert.u2n[c * 2 + 1];
			num2 = ((b != 0 || b2 != 0) ? (num2 + 2) : (num2 + 1));
		}
		return num2;
	}

	public unsafe override int GetBytesImpl(char* chars, int charCount, byte* bytes, int byteCount)
	{
		int charIndex = 0;
		int byteIndex = 0;
		DbcsConvert convert = GetConvert();
		EncoderFallbackBuffer buffer = null;
		int num = byteIndex;
		while (charCount-- > 0)
		{
			char c = *(char*)((byte*)chars + charIndex++ * 2);
			if (c <= '\u0080' || c == 'ÿ')
			{
				bytes[byteIndex++] = (byte)c;
				continue;
			}
			byte b = convert.u2n[c * 2];
			byte b2 = convert.u2n[c * 2 + 1];
			if (b == 0 && b2 == 0)
			{
				HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
				continue;
			}
			bytes[byteIndex++] = b;
			bytes[byteIndex++] = b2;
		}
		return byteIndex - num;
	}

	public override int GetCharCount(byte[] bytes, int index, int count)
	{
		return GetDecoder().GetCharCount(bytes, index, count);
	}

	public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
	{
		return GetDecoder().GetChars(bytes, byteIndex, byteCount, chars, charIndex);
	}

	public override Decoder GetDecoder()
	{
		return new KoreanDecoder(GetConvert(), useUHC);
	}
}
[Serializable]
internal class ENCuhc : CP949
{
}
[Serializable]
internal class ENCeuc_kr : CP51949
{
}
[Serializable]
internal class CP950 : DbcsEncoding
{
	private sealed class CP950Decoder : DbcsDecoder
	{
		private int last_byte_count;

		private int last_byte_conv;

		public CP950Decoder(DbcsConvert convert)
			: base(convert)
		{
		}

		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return GetCharCount(bytes, index, count, refresh: false);
		}

		public override int GetCharCount(byte[] bytes, int index, int count, bool refresh)
		{
			CheckRange(bytes, index, count);
			int num = last_byte_count;
			last_byte_count = 0;
			int num2 = 0;
			while (count-- > 0)
			{
				int num3 = bytes[index++];
				if (num == 0)
				{
					if (num3 > 128)
					{
						switch (num3)
						{
						case 255:
							break;
						default:
							num2++;
							count--;
							continue;
						case 161:
						case 162:
						case 163:
						case 164:
						case 165:
						case 166:
						case 167:
						case 168:
						case 169:
						case 170:
						case 171:
						case 172:
						case 173:
						case 174:
						case 175:
						case 176:
						case 177:
						case 178:
						case 179:
						case 180:
						case 181:
						case 182:
						case 183:
						case 184:
						case 185:
						case 186:
						case 187:
						case 188:
						case 189:
						case 190:
						case 191:
						case 192:
						case 193:
						case 194:
						case 195:
						case 196:
						case 197:
						case 198:
						case 199:
						case 200:
						case 201:
						case 202:
						case 203:
						case 204:
						case 205:
						case 206:
						case 207:
						case 208:
						case 209:
						case 210:
						case 211:
						case 212:
						case 213:
						case 214:
						case 215:
						case 216:
						case 217:
						case 218:
						case 219:
						case 220:
						case 221:
						case 222:
						case 223:
						case 224:
						case 225:
						case 226:
						case 227:
						case 228:
						case 229:
						case 230:
						case 231:
						case 232:
						case 233:
						case 234:
						case 235:
						case 236:
						case 237:
						case 238:
						case 239:
						case 240:
						case 241:
						case 242:
						case 243:
						case 244:
						case 245:
						case 246:
						case 247:
						case 248:
						case 249:
							num = num3;
							continue;
						}
					}
					num2++;
				}
				else
				{
					int num4 = ((num - 161) * 191 + num3 - 64) * 2;
					num2 = ((num4 >= 0 && num4 <= convert.n2u.Length && (ushort)(convert.n2u[num4] + convert.n2u[num4 + 1] * 256) != 0) ? (num2 + 1) : (num2 + 1));
					num = 0;
				}
			}
			if (num != 0)
			{
				if (refresh)
				{
					num2++;
				}
				else
				{
					last_byte_count = num;
				}
			}
			return num2;
		}

		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			return GetChars(bytes, byteIndex, byteCount, chars, charIndex, refresh: false);
		}

		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool refresh)
		{
			CheckRange(bytes, byteIndex, byteCount, chars, charIndex);
			int num = charIndex;
			int num2 = last_byte_conv;
			last_byte_conv = 0;
			while (byteCount-- > 0)
			{
				int num3 = bytes[byteIndex++];
				if (num2 == 0)
				{
					if (num3 > 128)
					{
						switch (num3)
						{
						case 255:
							break;
						default:
							chars[charIndex++] = '?';
							byteCount--;
							continue;
						case 161:
						case 162:
						case 163:
						case 164:
						case 165:
						case 166:
						case 167:
						case 168:
						case 169:
						case 170:
						case 171:
						case 172:
						case 173:
						case 174:
						case 175:
						case 176:
						case 177:
						case 178:
						case 179:
						case 180:
						case 181:
						case 182:
						case 183:
						case 184:
						case 185:
						case 186:
						case 187:
						case 188:
						case 189:
						case 190:
						case 191:
						case 192:
						case 193:
						case 194:
						case 195:
						case 196:
						case 197:
						case 198:
						case 199:
						case 200:
						case 201:
						case 202:
						case 203:
						case 204:
						case 205:
						case 206:
						case 207:
						case 208:
						case 209:
						case 210:
						case 211:
						case 212:
						case 213:
						case 214:
						case 215:
						case 216:
						case 217:
						case 218:
						case 219:
						case 220:
						case 221:
						case 222:
						case 223:
						case 224:
						case 225:
						case 226:
						case 227:
						case 228:
						case 229:
						case 230:
						case 231:
						case 232:
						case 233:
						case 234:
						case 235:
						case 236:
						case 237:
						case 238:
						case 239:
						case 240:
						case 241:
						case 242:
						case 243:
						case 244:
						case 245:
						case 246:
						case 247:
						case 248:
						case 249:
							num2 = num3;
							continue;
						}
					}
					chars[charIndex++] = (char)num3;
				}
				else
				{
					int num4 = ((num2 - 161) * 191 + num3 - 64) * 2;
					char c = ((num4 >= 0 && num4 <= convert.n2u.Length) ? ((char)(convert.n2u[num4] + convert.n2u[num4 + 1] * 256)) : '\0');
					if (c == '\0')
					{
						chars[charIndex++] = '?';
					}
					else
					{
						chars[charIndex++] = c;
					}
					num2 = 0;
				}
			}
			if (num2 != 0)
			{
				if (refresh)
				{
					chars[charIndex++] = '?';
				}
				else
				{
					last_byte_conv = num2;
				}
			}
			return charIndex - num;
		}
	}

	private const int BIG5_CODE_PAGE = 950;

	public override string BodyName => "big5";

	public override string EncodingName => "Chinese Traditional (Big5)";

	public override string HeaderName => "big5";

	public override string WebName => "big5";

	public CP950()
		: base(950)
	{
	}

	internal override DbcsConvert GetConvert()
	{
		return DbcsConvert.Big5;
	}

	public unsafe override int GetByteCountImpl(char* chars, int count)
	{
		DbcsConvert convert = GetConvert();
		int num = 0;
		int num2 = 0;
		while (count-- > 0)
		{
			char c = *(char*)((byte*)chars + num++ * 2);
			if (c <= '\u0080' || c == 'ÿ')
			{
				num2++;
				continue;
			}
			byte b = convert.u2n[c * 2 + 1];
			byte b2 = convert.u2n[c * 2];
			num2 = ((b != 0 || b2 != 0) ? (num2 + 2) : (num2 + 1));
		}
		return num2;
	}

	public unsafe override int GetBytesImpl(char* chars, int charCount, byte* bytes, int byteCount)
	{
		DbcsConvert convert = GetConvert();
		int charIndex = 0;
		int byteIndex = 0;
		EncoderFallbackBuffer buffer = null;
		int num = byteIndex;
		while (charCount-- > 0)
		{
			char c = *(char*)((byte*)chars + charIndex++ * 2);
			if (c <= '\u0080' || c == 'ÿ')
			{
				bytes[byteIndex++] = (byte)c;
				continue;
			}
			byte b = convert.u2n[c * 2 + 1];
			byte b2 = convert.u2n[c * 2];
			if (b == 0 && b2 == 0)
			{
				HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
				continue;
			}
			bytes[byteIndex++] = b;
			bytes[byteIndex++] = b2;
		}
		return byteIndex - num;
	}

	public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
	{
		return GetDecoder().GetChars(bytes, byteIndex, byteCount, chars, charIndex);
	}

	public override Decoder GetDecoder()
	{
		return new CP950Decoder(GetConvert());
	}
}
[Serializable]
internal class ENCbig5 : CP950
{
}
internal sealed class CodeTable : IDisposable
{
	private Stream stream;

	public CodeTable(string name)
	{
		stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
		if (stream == null)
		{
			throw new NotSupportedException(string.Format(Strings.GetString("NotSupp_MissingCodeTable"), name));
		}
	}

	public void Dispose()
	{
		if (stream != null)
		{
			stream.Close();
			stream = null;
		}
	}

	public byte[] GetSection(int num)
	{
		if (stream == null)
		{
			return null;
		}
		long num2 = 0L;
		long length = stream.Length;
		byte[] array = new byte[8];
		int num4;
		for (; num2 + 8 <= length; num2 += 8 + num4)
		{
			stream.Position = num2;
			if (stream.Read(array, 0, 8) != 8)
			{
				break;
			}
			int num3 = array[0] | (array[1] << 8) | (array[2] << 16) | (array[3] << 24);
			num4 = array[4] | (array[5] << 8) | (array[6] << 16) | (array[7] << 24);
			if (num3 == num)
			{
				byte[] array2 = new byte[num4];
				if (stream.Read(array2, 0, num4) != num4)
				{
					break;
				}
				return array2;
			}
		}
		return null;
	}
}
[Serializable]
internal abstract class DbcsEncoding : MonoEncoding
{
	internal abstract class DbcsDecoder : Decoder
	{
		protected DbcsConvert convert;

		public DbcsDecoder(DbcsConvert convert)
		{
			this.convert = convert;
		}

		internal void CheckRange(byte[] bytes, int index, int count)
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
		}

		internal void CheckRange(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
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
			if (byteCount < 0 || byteIndex + byteCount > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteCount", Strings.GetString("ArgRange_Array"));
			}
			if (charIndex < 0 || charIndex > chars.Length)
			{
				throw new ArgumentOutOfRangeException("charIndex", Strings.GetString("ArgRange_Array"));
			}
		}
	}

	public override bool IsBrowserDisplay => true;

	public override bool IsBrowserSave => true;

	public override bool IsMailNewsDisplay => true;

	public override bool IsMailNewsSave => true;

	public DbcsEncoding(int codePage)
		: this(codePage, 0)
	{
	}

	public DbcsEncoding(int codePage, int windowsCodePage)
		: base(codePage, windowsCodePage)
	{
	}

	internal abstract DbcsConvert GetConvert();

	public override int GetByteCount(char[] chars, int index, int count)
	{
		if (chars == null)
		{
			throw new ArgumentNullException("chars");
		}
		if (index < 0 || index > chars.Length)
		{
			throw new ArgumentOutOfRangeException("index", Strings.GetString("ArgRange_Array"));
		}
		if (count < 0 || index + count > chars.Length)
		{
			throw new ArgumentOutOfRangeException("count", Strings.GetString("ArgRange_Array"));
		}
		byte[] bytes = new byte[count * 2];
		return GetBytes(chars, index, count, bytes, 0);
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
		if (count < 0 || index + count > bytes.Length)
		{
			throw new ArgumentOutOfRangeException("count", Strings.GetString("ArgRange_Array"));
		}
		char[] chars = new char[count];
		return GetChars(bytes, index, count, chars, 0);
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
		if (byteCount < 0 || byteIndex + byteCount > bytes.Length)
		{
			throw new ArgumentOutOfRangeException("byteCount", Strings.GetString("ArgRange_Array"));
		}
		if (charIndex < 0 || charIndex > chars.Length)
		{
			throw new ArgumentOutOfRangeException("charIndex", Strings.GetString("ArgRange_Array"));
		}
		return 0;
	}

	public override int GetMaxByteCount(int charCount)
	{
		if (charCount < 0)
		{
			throw new ArgumentOutOfRangeException("charCount", Strings.GetString("ArgRange_NonNegative"));
		}
		return charCount * 2;
	}

	public override int GetMaxCharCount(int byteCount)
	{
		if (byteCount < 0)
		{
			throw new ArgumentOutOfRangeException("byteCount", Strings.GetString("ArgRange_NonNegative"));
		}
		return byteCount;
	}
}
internal class DbcsConvert
{
	public byte[] n2u;

	public byte[] u2n;

	internal static readonly DbcsConvert Gb2312 = new DbcsConvert("gb2312.table");

	internal static readonly DbcsConvert Big5 = new DbcsConvert("big5.table");

	internal static readonly DbcsConvert KS = new DbcsConvert("ks.table");

	internal DbcsConvert(string fileName)
	{
		using CodeTable codeTable = new CodeTable(fileName);
		n2u = codeTable.GetSection(1);
		u2n = codeTable.GetSection(2);
	}
}
[Serializable]
internal class ENCgb18030 : GB18030Encoding
{
}
[Serializable]
public class CP54936 : GB18030Encoding
{
}
[Serializable]
public class GB18030Encoding : MonoEncoding
{
	public override string EncodingName => "Chinese Simplified (GB18030)";

	public override string HeaderName => "GB18030";

	public override string BodyName => "GB18030";

	public override string WebName => "GB18030";

	public override bool IsMailNewsDisplay => true;

	public override bool IsMailNewsSave => true;

	public override bool IsBrowserDisplay => true;

	public override bool IsBrowserSave => true;

	public GB18030Encoding()
		: base(54936, 936)
	{
	}

	public override int GetMaxByteCount(int len)
	{
		return len * 4;
	}

	public override int GetMaxCharCount(int len)
	{
		return len;
	}

	public override int GetByteCount(char[] chars, int index, int length)
	{
		return new GB18030Encoder(this).GetByteCount(chars, index, length, refresh: true);
	}

	public unsafe override int GetByteCountImpl(char* chars, int count)
	{
		return new GB18030Encoder(this).GetByteCountImpl(chars, count, refresh: true);
	}

	public unsafe override int GetBytesImpl(char* chars, int charCount, byte* bytes, int byteCount)
	{
		return new GB18030Encoder(this).GetBytesImpl(chars, charCount, bytes, byteCount, refresh: true);
	}

	public override int GetCharCount(byte[] bytes, int start, int len)
	{
		return new GB18030Decoder().GetCharCount(bytes, start, len);
	}

	public override int GetChars(byte[] bytes, int byteIdx, int srclen, char[] chars, int charIdx)
	{
		return new GB18030Decoder().GetChars(bytes, byteIdx, srclen, chars, charIdx);
	}

	public override Encoder GetEncoder()
	{
		return new GB18030Encoder(this);
	}

	public override Decoder GetDecoder()
	{
		return new GB18030Decoder();
	}
}
internal class GB18030Decoder : DbcsEncoding.DbcsDecoder
{
	private static DbcsConvert gb2312 = DbcsConvert.Gb2312;

	public GB18030Decoder()
		: base(null)
	{
	}

	public override int GetCharCount(byte[] bytes, int start, int len)
	{
		CheckRange(bytes, start, len);
		int num = start + len;
		int num2 = 0;
		while (start < num)
		{
			if (bytes[start] < 128)
			{
				num2++;
				start++;
				continue;
			}
			if (bytes[start] == 128)
			{
				num2++;
				start++;
				continue;
			}
			if (bytes[start] == byte.MaxValue)
			{
				num2++;
				start++;
				continue;
			}
			if (start + 1 >= num)
			{
				num2++;
				break;
			}
			byte b = bytes[start + 1];
			if (b == 127 || b == byte.MaxValue)
			{
				num2++;
				start += 2;
			}
			else if (48 <= b && b <= 57)
			{
				if (start + 3 >= num)
				{
					num2 += ((start + 3 != num) ? 2 : 3);
					break;
				}
				long num3 = GB18030Source.FromGBX(bytes, start);
				if (num3 < 0)
				{
					num2++;
					start -= (int)num3;
				}
				else if (num3 >= 65536)
				{
					num2 += 2;
					start += 4;
				}
				else
				{
					num2++;
					start += 4;
				}
			}
			else
			{
				start += 2;
				num2++;
			}
		}
		return num2;
	}

	public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
	{
		CheckRange(bytes, byteIndex, byteCount, chars, charIndex);
		int num = byteIndex + byteCount;
		int num2 = charIndex;
		while (byteIndex < num)
		{
			if (bytes[byteIndex] < 128)
			{
				chars[charIndex++] = (char)bytes[byteIndex++];
				continue;
			}
			if (bytes[byteIndex] == 128)
			{
				chars[charIndex++] = '€';
				byteIndex++;
				continue;
			}
			if (bytes[byteIndex] == byte.MaxValue)
			{
				chars[charIndex++] = '?';
				byteIndex++;
				continue;
			}
			if (byteIndex + 1 >= num)
			{
				break;
			}
			byte b = bytes[byteIndex + 1];
			if (b == 127 || b == byte.MaxValue)
			{
				chars[charIndex++] = '?';
				byteIndex += 2;
			}
			else if (48 <= b && b <= 57)
			{
				if (byteIndex + 3 >= num)
				{
					break;
				}
				long num3 = GB18030Source.FromGBX(bytes, byteIndex);
				if (num3 < 0)
				{
					chars[charIndex++] = '?';
					byteIndex -= (int)num3;
				}
				else if (num3 >= 65536)
				{
					num3 -= 65536;
					chars[charIndex++] = (char)(num3 / 1024 + 55296);
					chars[charIndex++] = (char)(num3 % 1024 + 56320);
					byteIndex += 4;
				}
				else
				{
					chars[charIndex++] = (char)num3;
					byteIndex += 4;
				}
			}
			else
			{
				byte b2 = bytes[byteIndex];
				int num4 = ((b2 - 129) * 191 + b - 64) * 2;
				char c = ((num4 >= 0 && num4 < gb2312.n2u.Length) ? ((char)(gb2312.n2u[num4] + gb2312.n2u[num4 + 1] * 256)) : '\0');
				if (c == '\0')
				{
					chars[charIndex++] = '?';
				}
				else
				{
					chars[charIndex++] = c;
				}
				byteIndex += 2;
			}
		}
		return charIndex - num2;
	}
}
internal class GB18030Encoder : MonoEncoder
{
	private static DbcsConvert gb2312 = DbcsConvert.Gb2312;

	private char incomplete_byte_count;

	private char incomplete_bytes;

	public GB18030Encoder(MonoEncoding owner)
		: base(owner)
	{
	}

	public unsafe override int GetByteCountImpl(char* chars, int count, bool refresh)
	{
		int num = 0;
		int num2 = 0;
		while (num < count)
		{
			char c = *(char*)((byte*)chars + num * 2);
			if (c < '\u0080')
			{
				num2++;
				num++;
				continue;
			}
			if (char.IsSurrogate(c))
			{
				if (num + 1 == count)
				{
					incomplete_byte_count = c;
					num++;
				}
				else
				{
					num2 += 4;
					num += 2;
				}
				continue;
			}
			if (c < '\u0080' || c == 'ÿ')
			{
				num2++;
				num++;
				continue;
			}
			byte b = gb2312.u2n[c * 2 + 1];
			byte b2 = gb2312.u2n[c * 2];
			if (b != 0 && b2 != 0)
			{
				num2 += 2;
				num++;
			}
			else
			{
				long num3 = GB18030Source.FromUCS(c);
				num2 = ((num3 >= 0) ? (num2 + 4) : (num2 + 1));
				num++;
			}
		}
		if (refresh)
		{
			if (incomplete_byte_count != 0)
			{
				num2++;
			}
			incomplete_byte_count = '\0';
		}
		return num2;
	}

	public unsafe override int GetBytesImpl(char* chars, int charCount, byte* bytes, int byteCount, bool refresh)
	{
		int charIndex = 0;
		int byteIndex = 0;
		int num = charIndex + charCount;
		int num2 = byteIndex;
		char c = incomplete_bytes;
		while (charIndex < num)
		{
			if (incomplete_bytes == '\0')
			{
				c = *(char*)((byte*)chars + charIndex++ * 2);
			}
			else
			{
				incomplete_bytes = '\0';
			}
			if (c < '\u0080')
			{
				bytes[byteIndex++] = (byte)c;
				continue;
			}
			if (char.IsSurrogate(c))
			{
				if (charIndex == num)
				{
					incomplete_bytes = c;
					break;
				}
				char c2 = *(char*)((byte*)chars + charIndex++ * 2);
				if (!char.IsSurrogate(c2))
				{
					HandleFallback(chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					continue;
				}
				int cp = (c - 55296) * 1024 + c2 - 56320;
				GB18030Source.Unlinear(bytes + byteIndex, GB18030Source.FromUCSSurrogate(cp));
				byteIndex += 4;
				continue;
			}
			if (c <= '\u0080' || c == 'ÿ')
			{
				bytes[byteIndex++] = (byte)c;
				continue;
			}
			byte b = gb2312.u2n[c * 2 + 1];
			byte b2 = gb2312.u2n[c * 2];
			if (b != 0 && b2 != 0)
			{
				bytes[byteIndex++] = b;
				bytes[byteIndex++] = b2;
				continue;
			}
			long num3 = GB18030Source.FromUCS(c);
			if (num3 < 0)
			{
				bytes[byteIndex++] = 63;
				continue;
			}
			GB18030Source.Unlinear(bytes + byteIndex, num3);
			byteIndex += 4;
		}
		if (refresh)
		{
			if (incomplete_bytes != 0)
			{
				bytes[byteIndex++] = 63;
			}
			incomplete_bytes = '\0';
		}
		return byteIndex - num2;
	}
}
internal class GB18030Source
{
	private class GB18030Map
	{
		public readonly int UStart;

		public readonly int UEnd;

		public readonly long GStart;

		public readonly long GEnd;

		public readonly bool Dummy;

		public GB18030Map(int ustart, int uend, long gstart, long gend, bool dummy)
		{
			UStart = ustart;
			UEnd = uend;
			GStart = gstart;
			GEnd = gend;
			Dummy = dummy;
		}
	}

	private unsafe static readonly byte* gbx2uni;

	private unsafe static readonly byte* uni2gbx;

	private static readonly int gbx2uniSize;

	private static readonly int uni2gbxSize;

	private static readonly long gbxBase;

	private static readonly long gbxSuppBase;

	private static readonly GB18030Map[] ranges;

	private GB18030Source()
	{
	}

	unsafe static GB18030Source()
	{
		gbxBase = FromGBXRaw(129, 48, 129, 48, supp: false);
		gbxSuppBase = FromGBXRaw(144, 48, 129, 48, supp: false);
		ranges = new GB18030Map[14]
		{
			new GB18030Map(1106, 8207, FromGBXRaw(129, 48, 211, 48, supp: false), FromGBXRaw(129, 54, 165, 49, supp: false), dummy: false),
			new GB18030Map(9795, 11904, FromGBXRaw(129, 55, 168, 57, supp: false), FromGBXRaw(129, 56, 253, 56, supp: false), dummy: false),
			new GB18030Map(13851, 14615, FromGBXRaw(130, 48, 166, 51, supp: false), FromGBXRaw(130, 48, 242, 55, supp: false), dummy: false),
			new GB18030Map(15585, 16469, FromGBXRaw(130, 49, 212, 56, supp: false), FromGBXRaw(130, 50, 175, 50, supp: false), dummy: false),
			new GB18030Map(16736, 17206, FromGBXRaw(130, 50, 201, 55, supp: false), FromGBXRaw(130, 50, 248, 55, supp: false), dummy: false),
			new GB18030Map(17623, 17995, FromGBXRaw(130, 51, 163, 57, supp: false), FromGBXRaw(130, 51, 201, 49, supp: false), dummy: false),
			new GB18030Map(18318, 18758, FromGBXRaw(130, 51, 232, 56, supp: false), FromGBXRaw(130, 52, 150, 56, supp: false), dummy: false),
			new GB18030Map(18872, 19574, FromGBXRaw(130, 52, 161, 49, supp: false), FromGBXRaw(130, 52, 231, 51, supp: false), dummy: false),
			new GB18030Map(19968, 40869, 0L, 0L, dummy: true),
			new GB18030Map(40870, 55295, FromGBXRaw(130, 53, 143, 51, supp: false), FromGBXRaw(131, 54, 199, 56, supp: false), dummy: false),
			new GB18030Map(55296, 59243, 0L, 0L, dummy: true),
			new GB18030Map(59493, 63787, FromGBXRaw(131, 54, 208, 48, supp: false), FromGBXRaw(132, 48, 133, 52, supp: false), dummy: false),
			new GB18030Map(64042, 65071, FromGBXRaw(132, 48, 156, 56, supp: false), FromGBXRaw(132, 49, 133, 55, supp: false), dummy: false),
			new GB18030Map(65510, 65535, FromGBXRaw(132, 49, 162, 52, supp: false), FromGBXRaw(132, 49, 164, 57, supp: false), dummy: false)
		};
		MethodInfo method = typeof(Assembly).GetMethod("GetManifestResourceInternal", BindingFlags.Instance | BindingFlags.NonPublic);
		int num = 0;
		Module module = null;
		IntPtr intPtr = (IntPtr)method.Invoke(Assembly.GetExecutingAssembly(), new object[3] { "gb18030.table", num, module });
		if (intPtr != IntPtr.Zero)
		{
			gbx2uni = (byte*)(void*)intPtr;
			gbx2uniSize = (*gbx2uni << 24) + (gbx2uni[1] << 16) + (gbx2uni[2] << 8) + gbx2uni[3];
			gbx2uni += 4;
			uni2gbx = gbx2uni + gbx2uniSize;
			uni2gbxSize = (*uni2gbx << 24) + (uni2gbx[1] << 16) + (uni2gbx[2] << 8) + uni2gbx[3];
			uni2gbx += 4;
		}
	}

	public unsafe static void Unlinear(byte[] bytes, int start, long gbx)
	{
		//IL_0015->IL001c: Incompatible stack types: I vs Ref
		fixed (byte* ptr = &(bytes != null && bytes.Length != 0 ? ref bytes[0] : ref *(byte*)null))
		{
			Unlinear(ptr + start, gbx);
		}
	}

	public unsafe static void Unlinear(byte* bytes, long gbx)
	{
		bytes[3] = (byte)(gbx % 10 + 48);
		gbx /= 10;
		bytes[2] = (byte)(gbx % 126 + 129);
		gbx /= 126;
		bytes[1] = (byte)(gbx % 10 + 48);
		gbx /= 10;
		*bytes = (byte)(gbx + 129);
	}

	public static long FromGBX(byte[] bytes, int start)
	{
		byte b = bytes[start];
		byte b2 = bytes[start + 1];
		byte b3 = bytes[start + 2];
		byte b4 = bytes[start + 3];
		if (b < 129 || b == byte.MaxValue)
		{
			return -1L;
		}
		if (b2 < 48 || b2 > 57)
		{
			return -2L;
		}
		if (b3 < 129 || b3 == byte.MaxValue)
		{
			return -3L;
		}
		if (b4 < 48 || b4 > 57)
		{
			return -4L;
		}
		if (b >= 144)
		{
			return FromGBXRaw(b, b2, b3, b4, supp: true);
		}
		long num = FromGBXRaw(b, b2, b3, b4, supp: false);
		long num2 = 0L;
		long num3 = 0L;
		for (int i = 0; i < ranges.Length; i++)
		{
			GB18030Map gB18030Map = ranges[i];
			if (num < gB18030Map.GStart)
			{
				return ToUcsRaw((int)(num - num3 + num2));
			}
			if (num <= gB18030Map.GEnd)
			{
				return num - gbxBase - gB18030Map.GStart + gB18030Map.UStart;
			}
			if (gB18030Map.GStart != 0L)
			{
				num2 += gB18030Map.GStart - num3;
				num3 = gB18030Map.GEnd + 1;
			}
		}
		throw new SystemException($"GB18030 INTERNAL ERROR (should not happen): GBX {b:x02} {b2:x02} {b3:x02} {b4:x02}");
	}

	public static long FromUCSSurrogate(int cp)
	{
		return cp + gbxSuppBase;
	}

	public static long FromUCS(int cp)
	{
		long num = 0L;
		long num2 = 128L;
		for (int i = 0; i < ranges.Length; i++)
		{
			GB18030Map gB18030Map = ranges[i];
			if (cp < gB18030Map.UStart)
			{
				return ToGbxRaw((int)(cp - num2 + num));
			}
			if (cp <= gB18030Map.UEnd)
			{
				return cp - gB18030Map.UStart + gB18030Map.GStart;
			}
			if (gB18030Map.GStart != 0L)
			{
				num += gB18030Map.UStart - num2;
				num2 = gB18030Map.UEnd + 1;
			}
		}
		throw new SystemException($"GB18030 INTERNAL ERROR (should not happen): UCS {cp:x06}");
	}

	private static long FromGBXRaw(byte b1, byte b2, byte b3, byte b4, bool supp)
	{
		return (((b1 - ((!supp) ? 129 : 144)) * 10 + (b2 - 48)) * 126 + (b3 - 129)) * 10 + b4 - 48 + (supp ? 65536 : 0);
	}

	private unsafe static int ToUcsRaw(int idx)
	{
		return gbx2uni[idx * 2] * 256 + gbx2uni[idx * 2 + 1];
	}

	private unsafe static long ToGbxRaw(int idx)
	{
		if (idx < 0 || idx * 2 + 1 >= uni2gbxSize)
		{
			return -1L;
		}
		return gbxBase + uni2gbx[idx * 2] * 256 + (int)uni2gbx[idx * 2 + 1];
	}
}
internal sealed class JISConvert
{
	private const int JISX0208_To_Unicode = 1;

	private const int JISX0212_To_Unicode = 2;

	private const int CJK_To_JIS = 3;

	private const int Greek_To_JIS = 4;

	private const int Extra_To_JIS = 5;

	public byte[] jisx0208ToUnicode;

	public byte[] jisx0212ToUnicode;

	public byte[] cjkToJis;

	public byte[] greekToJis;

	public byte[] extraToJis;

	private static JISConvert convert;

	private static readonly object lockobj = new object();

	public static JISConvert Convert
	{
		get
		{
			lock (lockobj)
			{
				if (convert != null)
				{
					return convert;
				}
				convert = new JISConvert();
				return convert;
			}
		}
	}

	private JISConvert()
	{
		CodeTable codeTable = new CodeTable("jis.table");
		jisx0208ToUnicode = codeTable.GetSection(1);
		jisx0212ToUnicode = codeTable.GetSection(2);
		cjkToJis = codeTable.GetSection(3);
		greekToJis = codeTable.GetSection(4);
		extraToJis = codeTable.GetSection(5);
		codeTable.Dispose();
	}
}
