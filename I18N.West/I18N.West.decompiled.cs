using System;
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
namespace I18N.West;

[Serializable]
public class CP10000 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006', '\a', '\b', '\t',
		'\n', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019', '\u001a', '\u001b', '\u001c', '\u001d',
		'\u001e', '\u001f', ' ', '!', '"', '#', '$', '%', '&', '\'',
		'(', ')', '*', '+', ',', '-', '.', '/', '0', '1',
		'2', '3', '4', '5', '6', '7', '8', '9', ':', ';',
		'<', '=', '>', '?', '@', 'A', 'B', 'C', 'D', 'E',
		'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
		'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y',
		'Z', '[', '\\', ']', '^', '_', '`', 'a', 'b', 'c',
		'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
		'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w',
		'x', 'y', 'z', '{', '|', '}', '~', '\u007f', 'Ä', 'Å',
		'Ç', 'É', 'Ñ', 'Ö', 'Ü', 'á', 'à', 'â', 'ä', 'ã',
		'å', 'ç', 'é', 'è', 'ê', 'ë', 'í', 'ì', 'î', 'ï',
		'ñ', 'ó', 'ò', 'ô', 'ö', 'õ', 'ú', 'ù', 'û', 'ü',
		'†', '°', '¢', '£', '§', '•', '¶', 'ß', '®', '©',
		'™', '\u00b4', '\u00a8', '≠', 'Æ', 'Ø', '∞', '±', '≤', '≥',
		'¥', 'µ', '∂', '∑', '∏', 'π', '∫', 'ª', 'º', 'Ω',
		'æ', 'ø', '¿', '¡', '¬', '√', 'ƒ', '≈', '∆', '«',
		'»', '…', '\u00a0', 'À', 'Ã', 'Õ', 'Œ', 'œ', '–', '—',
		'“', '”', '‘', '’', '÷', '◊', 'ÿ', 'Ÿ', '⁄', '¤',
		'‹', '›', 'ﬁ', 'ﬂ', '‡', '·', '‚', '„', '‰', 'Â',
		'Ê', 'Á', 'Ë', 'È', 'Í', 'Î', 'Ï', 'Ì', 'Ó', 'Ô',
		'\uf8ff', 'Ò', 'Ú', 'Û', 'Ù', 'ı', 'ˆ', '\u02dc', '\u00af', '\u02d8',
		'\u02d9', '\u02da', '\u00b8', '\u02dd', '\u02db', 'ˇ'
	};

	public CP10000()
		: base(10000, ToChars, "Western European (Mac)", "macintosh", "macintosh", "macintosh", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
	{
	}

	protected unsafe override void ToBytes(char* chars, int charCount, byte* bytes, int byteCount)
	{
		int charIndex = 0;
		int byteIndex = 0;
		EncoderFallbackBuffer buffer = null;
		while (charCount > 0)
		{
			int num = *(ushort*)((byte*)chars + charIndex++ * 2);
			if (num >= 128)
			{
				switch (num)
				{
				case 160:
					num = 202;
					break;
				case 161:
					num = 193;
					break;
				case 164:
					num = 219;
					break;
				case 165:
					num = 180;
					break;
				case 167:
					num = 164;
					break;
				case 168:
					num = 172;
					break;
				case 170:
					num = 187;
					break;
				case 171:
					num = 199;
					break;
				case 172:
					num = 194;
					break;
				case 174:
					num = 168;
					break;
				case 175:
					num = 248;
					break;
				case 176:
					num = 161;
					break;
				case 180:
					num = 171;
					break;
				case 182:
					num = 166;
					break;
				case 183:
					num = 225;
					break;
				case 184:
					num = 252;
					break;
				case 186:
					num = 188;
					break;
				case 187:
					num = 200;
					break;
				case 191:
					num = 192;
					break;
				case 192:
					num = 203;
					break;
				case 193:
					num = 231;
					break;
				case 194:
					num = 229;
					break;
				case 195:
					num = 204;
					break;
				case 196:
					num = 128;
					break;
				case 197:
					num = 129;
					break;
				case 198:
					num = 174;
					break;
				case 199:
					num = 130;
					break;
				case 200:
					num = 233;
					break;
				case 201:
					num = 131;
					break;
				case 202:
					num = 230;
					break;
				case 203:
					num = 232;
					break;
				case 204:
					num = 237;
					break;
				case 205:
					num = 234;
					break;
				case 206:
					num = 235;
					break;
				case 207:
					num = 236;
					break;
				case 209:
					num = 132;
					break;
				case 210:
					num = 241;
					break;
				case 211:
					num = 238;
					break;
				case 212:
					num = 239;
					break;
				case 213:
					num = 205;
					break;
				case 214:
					num = 133;
					break;
				case 216:
					num = 175;
					break;
				case 217:
					num = 244;
					break;
				case 218:
					num = 242;
					break;
				case 219:
					num = 243;
					break;
				case 220:
					num = 134;
					break;
				case 223:
					num = 167;
					break;
				case 224:
					num = 136;
					break;
				case 225:
					num = 135;
					break;
				case 226:
					num = 137;
					break;
				case 227:
					num = 139;
					break;
				case 228:
					num = 138;
					break;
				case 229:
					num = 140;
					break;
				case 230:
					num = 190;
					break;
				case 231:
					num = 141;
					break;
				case 232:
					num = 143;
					break;
				case 233:
					num = 142;
					break;
				case 234:
					num = 144;
					break;
				case 235:
					num = 145;
					break;
				case 236:
					num = 147;
					break;
				case 237:
					num = 146;
					break;
				case 238:
					num = 148;
					break;
				case 239:
					num = 149;
					break;
				case 241:
					num = 150;
					break;
				case 242:
					num = 152;
					break;
				case 243:
					num = 151;
					break;
				case 244:
					num = 153;
					break;
				case 245:
					num = 155;
					break;
				case 246:
					num = 154;
					break;
				case 247:
					num = 214;
					break;
				case 248:
					num = 191;
					break;
				case 249:
					num = 157;
					break;
				case 250:
					num = 156;
					break;
				case 251:
					num = 158;
					break;
				case 252:
					num = 159;
					break;
				case 255:
					num = 216;
					break;
				case 305:
					num = 245;
					break;
				case 338:
					num = 206;
					break;
				case 339:
					num = 207;
					break;
				case 376:
					num = 217;
					break;
				case 402:
					num = 196;
					break;
				case 710:
					num = 246;
					break;
				case 711:
					num = 255;
					break;
				case 728:
					num = 249;
					break;
				case 729:
					num = 250;
					break;
				case 730:
					num = 251;
					break;
				case 731:
					num = 254;
					break;
				case 732:
					num = 247;
					break;
				case 733:
					num = 253;
					break;
				case 960:
					num = 185;
					break;
				case 8211:
					num = 208;
					break;
				case 8212:
					num = 209;
					break;
				case 8216:
					num = 212;
					break;
				case 8217:
					num = 213;
					break;
				case 8218:
					num = 226;
					break;
				case 8220:
					num = 210;
					break;
				case 8221:
					num = 211;
					break;
				case 8222:
					num = 227;
					break;
				case 8224:
					num = 160;
					break;
				case 8225:
					num = 224;
					break;
				case 8226:
					num = 165;
					break;
				case 8230:
					num = 201;
					break;
				case 8240:
					num = 228;
					break;
				case 8249:
					num = 220;
					break;
				case 8250:
					num = 221;
					break;
				case 8260:
					num = 218;
					break;
				case 8482:
					num = 170;
					break;
				case 8486:
					num = 189;
					break;
				case 8706:
					num = 182;
					break;
				case 8710:
					num = 198;
					break;
				case 8719:
					num = 184;
					break;
				case 8721:
					num = 183;
					break;
				case 8730:
					num = 195;
					break;
				case 8734:
					num = 176;
					break;
				case 8747:
					num = 186;
					break;
				case 8776:
					num = 197;
					break;
				case 8800:
					num = 173;
					break;
				case 8804:
					num = 178;
					break;
				case 8805:
					num = 179;
					break;
				case 8984:
					num = 17;
					break;
				case 9674:
					num = 215;
					break;
				case 9830:
					num = 19;
					break;
				case 10003:
					num = 18;
					break;
				case 63743:
					num = 240;
					break;
				case 64257:
					num = 222;
					break;
				case 64258:
					num = 223;
					break;
				default:
					if (num >= 65281 && num <= 65374)
					{
						num -= 65248;
					}
					else
					{
						HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					}
					break;
				case 162:
				case 163:
				case 169:
				case 177:
				case 181:
					break;
				}
			}
			bytes[byteIndex++] = (byte)num;
			charCount--;
			byteCount--;
		}
	}
}
[Serializable]
public class ENCmacintosh : CP10000
{
}
[Serializable]
public class CP10079 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006', '\a', '\b', '\t',
		'\n', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019', '\u001a', '\u001b', '\u001c', '\u001d',
		'\u001e', '\u001f', ' ', '!', '"', '#', '$', '%', '&', '\'',
		'(', ')', '*', '+', ',', '-', '.', '/', '0', '1',
		'2', '3', '4', '5', '6', '7', '8', '9', ':', ';',
		'<', '=', '>', '?', '@', 'A', 'B', 'C', 'D', 'E',
		'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
		'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y',
		'Z', '[', '\\', ']', '^', '_', '`', 'a', 'b', 'c',
		'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
		'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w',
		'x', 'y', 'z', '{', '|', '}', '~', '\u007f', 'Ä', 'Å',
		'Ç', 'É', 'Ñ', 'Ö', 'Ü', 'á', 'à', 'â', 'ä', 'ã',
		'å', 'ç', 'é', 'è', 'ê', 'ë', 'í', 'ì', 'î', 'ï',
		'ñ', 'ó', 'ò', 'ô', 'ö', 'õ', 'ú', 'ù', 'û', 'ü',
		'Ý', '°', '¢', '£', '§', '•', '¶', 'ß', '®', '©',
		'™', '\u00b4', '\u00a8', '≠', 'Æ', 'Ø', '∞', '±', '≤', '≥',
		'¥', 'µ', '∂', '∑', '∏', 'π', '∫', 'ª', 'º', 'Ω',
		'æ', 'ø', '¿', '¡', '¬', '√', 'ƒ', '≈', '∆', '«',
		'»', '…', '\u00a0', 'À', 'Ã', 'Õ', 'Œ', 'œ', '–', '—',
		'“', '”', '‘', '’', '÷', '◊', 'ÿ', 'Ÿ', '⁄', '¤',
		'Ð', 'ð', 'Þ', 'þ', 'ý', '·', '‚', '„', '‰', 'Â',
		'Ê', 'Á', 'Ë', 'È', 'Í', 'Î', 'Ï', 'Ì', 'Ó', 'Ô',
		'\uf8ff', 'Ò', 'Ú', 'Û', 'Ù', 'ı', 'ˆ', '\u02dc', '\u00af', '\u02d8',
		'\u02d9', '\u02da', '\u00b8', '\u02dd', '\u02db', 'ˇ'
	};

	public CP10079()
		: base(10079, ToChars, "Icelandic (Mac)", "x-mac-icelandic", "x-mac-icelandic", "x-mac-icelandic", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
	{
	}

	protected unsafe override void ToBytes(char* chars, int charCount, byte* bytes, int byteCount)
	{
		int charIndex = 0;
		int byteIndex = 0;
		EncoderFallbackBuffer buffer = null;
		while (charCount > 0)
		{
			int num = *(ushort*)((byte*)chars + charIndex++ * 2);
			if (num >= 128)
			{
				switch (num)
				{
				case 160:
					num = 202;
					break;
				case 161:
					num = 193;
					break;
				case 164:
					num = 219;
					break;
				case 165:
					num = 180;
					break;
				case 167:
					num = 164;
					break;
				case 168:
					num = 172;
					break;
				case 170:
					num = 187;
					break;
				case 171:
					num = 199;
					break;
				case 172:
					num = 194;
					break;
				case 174:
					num = 168;
					break;
				case 175:
					num = 248;
					break;
				case 176:
					num = 161;
					break;
				case 180:
					num = 171;
					break;
				case 182:
					num = 166;
					break;
				case 183:
					num = 225;
					break;
				case 184:
					num = 252;
					break;
				case 186:
					num = 188;
					break;
				case 187:
					num = 200;
					break;
				case 191:
					num = 192;
					break;
				case 192:
					num = 203;
					break;
				case 193:
					num = 231;
					break;
				case 194:
					num = 229;
					break;
				case 195:
					num = 204;
					break;
				case 196:
					num = 128;
					break;
				case 197:
					num = 129;
					break;
				case 198:
					num = 174;
					break;
				case 199:
					num = 130;
					break;
				case 200:
					num = 233;
					break;
				case 201:
					num = 131;
					break;
				case 202:
					num = 230;
					break;
				case 203:
					num = 232;
					break;
				case 204:
					num = 237;
					break;
				case 205:
					num = 234;
					break;
				case 206:
					num = 235;
					break;
				case 207:
					num = 236;
					break;
				case 208:
					num = 220;
					break;
				case 209:
					num = 132;
					break;
				case 210:
					num = 241;
					break;
				case 211:
					num = 238;
					break;
				case 212:
					num = 239;
					break;
				case 213:
					num = 205;
					break;
				case 214:
					num = 133;
					break;
				case 216:
					num = 175;
					break;
				case 217:
					num = 244;
					break;
				case 218:
					num = 242;
					break;
				case 219:
					num = 243;
					break;
				case 220:
					num = 134;
					break;
				case 221:
					num = 160;
					break;
				case 223:
					num = 167;
					break;
				case 224:
					num = 136;
					break;
				case 225:
					num = 135;
					break;
				case 226:
					num = 137;
					break;
				case 227:
					num = 139;
					break;
				case 228:
					num = 138;
					break;
				case 229:
					num = 140;
					break;
				case 230:
					num = 190;
					break;
				case 231:
					num = 141;
					break;
				case 232:
					num = 143;
					break;
				case 233:
					num = 142;
					break;
				case 234:
					num = 144;
					break;
				case 235:
					num = 145;
					break;
				case 236:
					num = 147;
					break;
				case 237:
					num = 146;
					break;
				case 238:
					num = 148;
					break;
				case 239:
					num = 149;
					break;
				case 240:
					num = 221;
					break;
				case 241:
					num = 150;
					break;
				case 242:
					num = 152;
					break;
				case 243:
					num = 151;
					break;
				case 244:
					num = 153;
					break;
				case 245:
					num = 155;
					break;
				case 246:
					num = 154;
					break;
				case 247:
					num = 214;
					break;
				case 248:
					num = 191;
					break;
				case 249:
					num = 157;
					break;
				case 250:
					num = 156;
					break;
				case 251:
					num = 158;
					break;
				case 252:
					num = 159;
					break;
				case 253:
					num = 224;
					break;
				case 254:
					num = 223;
					break;
				case 255:
					num = 216;
					break;
				case 305:
					num = 245;
					break;
				case 338:
					num = 206;
					break;
				case 339:
					num = 207;
					break;
				case 376:
					num = 217;
					break;
				case 402:
					num = 196;
					break;
				case 710:
					num = 246;
					break;
				case 711:
					num = 255;
					break;
				case 728:
					num = 249;
					break;
				case 729:
					num = 250;
					break;
				case 730:
					num = 251;
					break;
				case 731:
					num = 254;
					break;
				case 732:
					num = 247;
					break;
				case 733:
					num = 253;
					break;
				case 960:
					num = 185;
					break;
				case 8211:
					num = 208;
					break;
				case 8212:
					num = 209;
					break;
				case 8216:
					num = 212;
					break;
				case 8217:
					num = 213;
					break;
				case 8218:
					num = 226;
					break;
				case 8220:
					num = 210;
					break;
				case 8221:
					num = 211;
					break;
				case 8222:
					num = 227;
					break;
				case 8226:
					num = 165;
					break;
				case 8230:
					num = 201;
					break;
				case 8240:
					num = 228;
					break;
				case 8260:
					num = 218;
					break;
				case 8482:
					num = 170;
					break;
				case 8486:
					num = 189;
					break;
				case 8706:
					num = 182;
					break;
				case 8710:
					num = 198;
					break;
				case 8719:
					num = 184;
					break;
				case 8721:
					num = 183;
					break;
				case 8730:
					num = 195;
					break;
				case 8734:
					num = 176;
					break;
				case 8747:
					num = 186;
					break;
				case 8776:
					num = 197;
					break;
				case 8800:
					num = 173;
					break;
				case 8804:
					num = 178;
					break;
				case 8805:
					num = 179;
					break;
				case 9674:
					num = 215;
					break;
				case 63743:
					num = 240;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 162:
				case 163:
				case 169:
				case 177:
				case 181:
				case 222:
					break;
				}
			}
			bytes[byteIndex++] = (byte)num;
			charCount--;
			byteCount--;
		}
	}
}
[Serializable]
public class ENCx_mac_icelandic : CP10079
{
}
[Serializable]
public class CP1250 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006', '\a', '\b', '\t',
		'\n', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019', '\u001a', '\u001b', '\u001c', '\u001d',
		'\u001e', '\u001f', ' ', '!', '"', '#', '$', '%', '&', '\'',
		'(', ')', '*', '+', ',', '-', '.', '/', '0', '1',
		'2', '3', '4', '5', '6', '7', '8', '9', ':', ';',
		'<', '=', '>', '?', '@', 'A', 'B', 'C', 'D', 'E',
		'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
		'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y',
		'Z', '[', '\\', ']', '^', '_', '`', 'a', 'b', 'c',
		'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
		'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w',
		'x', 'y', 'z', '{', '|', '}', '~', '\u007f', '€', '\u0081',
		'‚', '\u0083', '„', '…', '†', '‡', '\u0088', '‰', 'Š', '‹',
		'Ś', 'Ť', 'Ž', 'Ź', '\u0090', '‘', '’', '“', '”', '•',
		'–', '—', '\u0098', '™', 'š', '›', 'ś', 'ť', 'ž', 'ź',
		'\u00a0', 'ˇ', '\u02d8', 'Ł', '¤', 'Ą', '¦', '§', '\u00a8', '©',
		'Ş', '«', '¬', '\u00ad', '®', 'Ż', '°', '±', '\u02db', 'ł',
		'\u00b4', 'µ', '¶', '·', '\u00b8', 'ą', 'ş', '»', 'Ľ', '\u02dd',
		'ľ', 'ż', 'Ŕ', 'Á', 'Â', 'Ă', 'Ä', 'Ĺ', 'Ć', 'Ç',
		'Č', 'É', 'Ę', 'Ë', 'Ě', 'Í', 'Î', 'Ď', 'Đ', 'Ń',
		'Ň', 'Ó', 'Ô', 'Ő', 'Ö', '×', 'Ř', 'Ů', 'Ú', 'Ű',
		'Ü', 'Ý', 'Ţ', 'ß', 'ŕ', 'á', 'â', 'ă', 'ä', 'ĺ',
		'ć', 'ç', 'č', 'é', 'ę', 'ë', 'ě', 'í', 'î', 'ď',
		'đ', 'ń', 'ň', 'ó', 'ô', 'ő', 'ö', '÷', 'ř', 'ů',
		'ú', 'ű', 'ü', 'ý', 'ţ', '\u02d9'
	};

	public CP1250()
		: base(1250, ToChars, "Central European (Windows)", "iso-8859-2", "windows-1250", "windows-1250", isBrowserDisplay: true, isBrowserSave: true, isMailNewsDisplay: true, isMailNewsSave: true, 1250)
	{
	}

	protected unsafe override void ToBytes(char* chars, int charCount, byte* bytes, int byteCount)
	{
		int charIndex = 0;
		int byteIndex = 0;
		EncoderFallbackBuffer buffer = null;
		while (charCount > 0)
		{
			int num = *(ushort*)((byte*)chars + charIndex++ * 2);
			charCount--;
			if (num >= 128)
			{
				switch (num)
				{
				case 258:
					num = 195;
					break;
				case 259:
					num = 227;
					break;
				case 260:
					num = 165;
					break;
				case 261:
					num = 185;
					break;
				case 262:
					num = 198;
					break;
				case 263:
					num = 230;
					break;
				case 268:
					num = 200;
					break;
				case 269:
					num = 232;
					break;
				case 270:
					num = 207;
					break;
				case 271:
					num = 239;
					break;
				case 272:
					num = 208;
					break;
				case 273:
					num = 240;
					break;
				case 280:
					num = 202;
					break;
				case 281:
					num = 234;
					break;
				case 282:
					num = 204;
					break;
				case 283:
					num = 236;
					break;
				case 313:
					num = 197;
					break;
				case 314:
					num = 229;
					break;
				case 317:
					num = 188;
					break;
				case 318:
					num = 190;
					break;
				case 321:
					num = 163;
					break;
				case 322:
					num = 179;
					break;
				case 323:
					num = 209;
					break;
				case 324:
					num = 241;
					break;
				case 327:
					num = 210;
					break;
				case 328:
					num = 242;
					break;
				case 336:
					num = 213;
					break;
				case 337:
					num = 245;
					break;
				case 340:
					num = 192;
					break;
				case 341:
					num = 224;
					break;
				case 344:
					num = 216;
					break;
				case 345:
					num = 248;
					break;
				case 346:
					num = 140;
					break;
				case 347:
					num = 156;
					break;
				case 350:
					num = 170;
					break;
				case 351:
					num = 186;
					break;
				case 352:
					num = 138;
					break;
				case 353:
					num = 154;
					break;
				case 354:
					num = 222;
					break;
				case 355:
					num = 254;
					break;
				case 356:
					num = 141;
					break;
				case 357:
					num = 157;
					break;
				case 366:
					num = 217;
					break;
				case 367:
					num = 249;
					break;
				case 368:
					num = 219;
					break;
				case 369:
					num = 251;
					break;
				case 377:
					num = 143;
					break;
				case 378:
					num = 159;
					break;
				case 379:
					num = 175;
					break;
				case 380:
					num = 191;
					break;
				case 381:
					num = 142;
					break;
				case 382:
					num = 158;
					break;
				case 711:
					num = 161;
					break;
				case 728:
					num = 162;
					break;
				case 729:
					num = 255;
					break;
				case 731:
					num = 178;
					break;
				case 733:
					num = 189;
					break;
				case 8211:
					num = 150;
					break;
				case 8212:
					num = 151;
					break;
				case 8216:
					num = 145;
					break;
				case 8217:
					num = 146;
					break;
				case 8218:
					num = 130;
					break;
				case 8220:
					num = 147;
					break;
				case 8221:
					num = 148;
					break;
				case 8222:
					num = 132;
					break;
				case 8224:
					num = 134;
					break;
				case 8225:
					num = 135;
					break;
				case 8226:
					num = 149;
					break;
				case 8230:
					num = 133;
					break;
				case 8240:
					num = 137;
					break;
				case 8249:
					num = 139;
					break;
				case 8250:
					num = 155;
					break;
				case 8364:
					num = 128;
					break;
				case 8482:
					num = 153;
					break;
				default:
					if (num >= 65281 && num <= 65374)
					{
						num -= 65248;
						break;
					}
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					continue;
				case 129:
				case 131:
				case 136:
				case 144:
				case 152:
				case 160:
				case 164:
				case 166:
				case 167:
				case 168:
				case 169:
				case 171:
				case 172:
				case 173:
				case 174:
				case 176:
				case 177:
				case 180:
				case 181:
				case 182:
				case 183:
				case 184:
				case 187:
				case 193:
				case 194:
				case 196:
				case 199:
				case 201:
				case 203:
				case 205:
				case 206:
				case 211:
				case 212:
				case 214:
				case 215:
				case 218:
				case 220:
				case 221:
				case 223:
				case 225:
				case 226:
				case 228:
				case 231:
				case 233:
				case 235:
				case 237:
				case 238:
				case 243:
				case 244:
				case 246:
				case 247:
				case 250:
				case 252:
				case 253:
					break;
				}
			}
			bytes[byteIndex++] = (byte)num;
			byteCount--;
		}
	}
}
[Serializable]
public class ENCwindows_1250 : CP1250
{
}
[Serializable]
public class CP1252 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006', '\a', '\b', '\t',
		'\n', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019', '\u001a', '\u001b', '\u001c', '\u001d',
		'\u001e', '\u001f', ' ', '!', '"', '#', '$', '%', '&', '\'',
		'(', ')', '*', '+', ',', '-', '.', '/', '0', '1',
		'2', '3', '4', '5', '6', '7', '8', '9', ':', ';',
		'<', '=', '>', '?', '@', 'A', 'B', 'C', 'D', 'E',
		'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
		'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y',
		'Z', '[', '\\', ']', '^', '_', '`', 'a', 'b', 'c',
		'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
		'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w',
		'x', 'y', 'z', '{', '|', '}', '~', '\u007f', '€', '\u0081',
		'‚', 'ƒ', '„', '…', '†', '‡', 'ˆ', '‰', 'Š', '‹',
		'Œ', '\u008d', 'Ž', '\u008f', '\u0090', '‘', '’', '“', '”', '•',
		'–', '—', '\u02dc', '™', 'š', '›', 'œ', '\u009d', 'ž', 'Ÿ',
		'\u00a0', '¡', '¢', '£', '¤', '¥', '¦', '§', '\u00a8', '©',
		'ª', '«', '¬', '\u00ad', '®', '\u00af', '°', '±', '²', '³',
		'\u00b4', 'µ', '¶', '·', '\u00b8', '¹', 'º', '»', '¼', '½',
		'¾', '¿', 'À', 'Á', 'Â', 'Ã', 'Ä', 'Å', 'Æ', 'Ç',
		'È', 'É', 'Ê', 'Ë', 'Ì', 'Í', 'Î', 'Ï', 'Ð', 'Ñ',
		'Ò', 'Ó', 'Ô', 'Õ', 'Ö', '×', 'Ø', 'Ù', 'Ú', 'Û',
		'Ü', 'Ý', 'Þ', 'ß', 'à', 'á', 'â', 'ã', 'ä', 'å',
		'æ', 'ç', 'è', 'é', 'ê', 'ë', 'ì', 'í', 'î', 'ï',
		'ð', 'ñ', 'ò', 'ó', 'ô', 'õ', 'ö', '÷', 'ø', 'ù',
		'ú', 'û', 'ü', 'ý', 'þ', 'ÿ'
	};

	public CP1252()
		: base(1252, ToChars, "Western European (Windows)", "iso-8859-1", "Windows-1252", "Windows-1252", isBrowserDisplay: true, isBrowserSave: true, isMailNewsDisplay: true, isMailNewsSave: true, 1252)
	{
	}

	protected unsafe override void ToBytes(char* chars, int charCount, byte* bytes, int byteCount)
	{
		int charIndex = 0;
		int byteIndex = 0;
		EncoderFallbackBuffer buffer = null;
		while (charCount > 0)
		{
			int num = *(ushort*)((byte*)chars + charIndex++ * 2);
			charCount--;
			if (num >= 128)
			{
				int num2 = num;
				switch (num2)
				{
				default:
					switch (num2)
					{
					case 376:
						num = 159;
						break;
					case 381:
						num = 142;
						break;
					case 382:
						num = 158;
						break;
					case 402:
						num = 131;
						break;
					case 710:
						num = 136;
						break;
					case 732:
						num = 152;
						break;
					case 8211:
						num = 150;
						break;
					case 8212:
						num = 151;
						break;
					case 8216:
						num = 145;
						break;
					case 8217:
						num = 146;
						break;
					case 8218:
						num = 130;
						break;
					case 8220:
						num = 147;
						break;
					case 8221:
						num = 148;
						break;
					case 8222:
						num = 132;
						break;
					case 8224:
						num = 134;
						break;
					case 8225:
						num = 135;
						break;
					case 8226:
						num = 149;
						break;
					case 8230:
						num = 133;
						break;
					case 8240:
						num = 137;
						break;
					case 8249:
						num = 139;
						break;
					case 8250:
						num = 155;
						break;
					case 8364:
						num = 128;
						break;
					case 8482:
						num = 153;
						break;
					default:
						if (num >= 65281 && num <= 65374)
						{
							num -= 65248;
							break;
						}
						HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
						continue;
					case 129:
					case 141:
					case 143:
					case 144:
						break;
					}
					break;
				case 338:
					num = 140;
					break;
				case 339:
					num = 156;
					break;
				case 352:
					num = 138;
					break;
				case 353:
					num = 154;
					break;
				case 157:
				case 160:
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
				case 255:
					break;
				}
			}
			bytes[byteIndex++] = (byte)num;
			byteCount--;
		}
	}
}
[Serializable]
public class ENCwindows_1252 : CP1252
{
}
[Serializable]
public class CP1253 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006', '\a', '\b', '\t',
		'\n', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019', '\u001a', '\u001b', '\u001c', '\u001d',
		'\u001e', '\u001f', ' ', '!', '"', '#', '$', '%', '&', '\'',
		'(', ')', '*', '+', ',', '-', '.', '/', '0', '1',
		'2', '3', '4', '5', '6', '7', '8', '9', ':', ';',
		'<', '=', '>', '?', '@', 'A', 'B', 'C', 'D', 'E',
		'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
		'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y',
		'Z', '[', '\\', ']', '^', '_', '`', 'a', 'b', 'c',
		'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
		'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w',
		'x', 'y', 'z', '{', '|', '}', '~', '\u007f', '€', '\u0081',
		'‚', 'ƒ', '„', '…', '†', '‡', '\u0088', '‰', '\u008a', '‹',
		'\u008c', '\u008d', '\u008e', '\u008f', '\u0090', '‘', '’', '“', '”', '•',
		'–', '—', '\u0098', '™', '\u009a', '›', '\u009c', '\u009d', '\u009e', '\u009f',
		'\u00a0', '\u0385', 'Ά', '£', '¤', '¥', '¦', '§', '\u00a8', '©',
		'ª', '«', '¬', '\u00ad', '®', '―', '°', '±', '²', '³',
		'\u0384', 'µ', '¶', '·', 'Έ', 'Ή', 'Ί', '»', 'Ό', '½',
		'Ύ', 'Ώ', 'ΐ', 'Α', 'Β', 'Γ', 'Δ', 'Ε', 'Ζ', 'Η',
		'Θ', 'Ι', 'Κ', 'Λ', 'Μ', 'Ν', 'Ξ', 'Ο', 'Π', 'Ρ',
		'?', 'Σ', 'Τ', 'Υ', 'Φ', 'Χ', 'Ψ', 'Ω', 'Ϊ', 'Ϋ',
		'ά', 'έ', 'ή', 'ί', 'ΰ', 'α', 'β', 'γ', 'δ', 'ε',
		'ζ', 'η', 'θ', 'ι', 'κ', 'λ', 'μ', 'ν', 'ξ', 'ο',
		'π', 'ρ', 'ς', 'σ', 'τ', 'υ', 'φ', 'χ', 'ψ', 'ω',
		'ϊ', 'ϋ', 'ό', 'ύ', 'ώ', '?'
	};

	public CP1253()
		: base(1253, ToChars, "Greek (Windows)", "iso-8859-7", "windows-1253", "windows-1253", isBrowserDisplay: true, isBrowserSave: true, isMailNewsDisplay: true, isMailNewsSave: true, 1253)
	{
	}

	protected unsafe override void ToBytes(char* chars, int charCount, byte* bytes, int byteCount)
	{
		int charIndex = 0;
		int byteIndex = 0;
		EncoderFallbackBuffer buffer = null;
		while (charCount > 0)
		{
			int num = *(ushort*)((byte*)chars + charIndex++ * 2);
			if (num >= 128)
			{
				switch (num)
				{
				case 402:
					num = 131;
					break;
				case 900:
					num = 180;
					break;
				case 901:
					num = 161;
					break;
				case 902:
					num = 162;
					break;
				case 904:
					num = 184;
					break;
				case 905:
					num = 185;
					break;
				case 906:
					num = 186;
					break;
				case 908:
					num = 188;
					break;
				case 910:
				case 911:
				case 912:
				case 913:
				case 914:
				case 915:
				case 916:
				case 917:
				case 918:
				case 919:
				case 920:
				case 921:
				case 922:
				case 923:
				case 924:
				case 925:
				case 926:
				case 927:
				case 928:
				case 929:
					num -= 720;
					break;
				case 931:
				case 932:
				case 933:
				case 934:
				case 935:
				case 936:
				case 937:
				case 938:
				case 939:
				case 940:
				case 941:
				case 942:
				case 943:
				case 944:
				case 945:
				case 946:
				case 947:
				case 948:
				case 949:
				case 950:
				case 951:
				case 952:
				case 953:
				case 954:
				case 955:
				case 956:
				case 957:
				case 958:
				case 959:
				case 960:
				case 961:
				case 962:
				case 963:
				case 964:
				case 965:
				case 966:
				case 967:
				case 968:
				case 969:
				case 970:
				case 971:
				case 972:
				case 973:
				case 974:
					num -= 720;
					break;
				case 981:
					num = 246;
					break;
				case 8211:
					num = 150;
					break;
				case 8212:
					num = 151;
					break;
				case 8213:
					num = 175;
					break;
				case 8216:
					num = 145;
					break;
				case 8217:
					num = 146;
					break;
				case 8218:
					num = 130;
					break;
				case 8220:
					num = 147;
					break;
				case 8221:
					num = 148;
					break;
				case 8222:
					num = 132;
					break;
				case 8224:
					num = 134;
					break;
				case 8225:
					num = 135;
					break;
				case 8226:
					num = 149;
					break;
				case 8230:
					num = 133;
					break;
				case 8240:
					num = 137;
					break;
				case 8249:
					num = 139;
					break;
				case 8250:
					num = 155;
					break;
				case 8364:
					num = 128;
					break;
				case 8482:
					num = 153;
					break;
				default:
					if (num >= 65281 && num <= 65374)
					{
						num -= 65248;
					}
					else
					{
						HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					}
					break;
				case 129:
				case 136:
				case 138:
				case 140:
				case 141:
				case 142:
				case 143:
				case 144:
				case 152:
				case 154:
				case 156:
				case 157:
				case 158:
				case 159:
				case 160:
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
				case 176:
				case 177:
				case 178:
				case 179:
				case 181:
				case 182:
				case 183:
				case 187:
				case 189:
					break;
				}
			}
			bytes[byteIndex++] = (byte)num;
			charCount--;
			byteCount--;
		}
	}
}
[Serializable]
public class ENCwindows_1253 : CP1253
{
}
[Serializable]
public class CP28592 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006', '\a', '\b', '\t',
		'\n', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019', '\u001a', '\u001b', '\u001c', '\u001d',
		'\u001e', '\u001f', ' ', '!', '"', '#', '$', '%', '&', '\'',
		'(', ')', '*', '+', ',', '-', '.', '/', '0', '1',
		'2', '3', '4', '5', '6', '7', '8', '9', ':', ';',
		'<', '=', '>', '?', '@', 'A', 'B', 'C', 'D', 'E',
		'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
		'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y',
		'Z', '[', '\\', ']', '^', '_', '`', 'a', 'b', 'c',
		'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
		'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w',
		'x', 'y', 'z', '{', '|', '}', '~', '\u007f', '\u0080', '\u0081',
		'\u0082', '\u0083', '\u0084', '\u0085', '\u0086', '\u0087', '\u0088', '\u0089', '\u008a', '\u008b',
		'\u008c', '\u008d', '\u008e', '\u008f', '\u0090', '\u0091', '\u0092', '\u0093', '\u0094', '\u0095',
		'\u0096', '\u0097', '\u0098', '\u0099', '\u009a', '\u009b', '\u009c', '\u009d', '\u009e', '\u009f',
		'\u00a0', 'Ą', '\u02d8', 'Ł', '¤', 'Ľ', 'Ś', '§', '\u00a8', 'Š',
		'Ş', 'Ť', 'Ź', '\u00ad', 'Ž', 'Ż', '°', 'ą', '\u02db', 'ł',
		'\u00b4', 'ľ', 'ś', 'ˇ', '\u00b8', 'š', 'ş', 'ť', 'ź', '\u02dd',
		'ž', 'ż', 'Ŕ', 'Á', 'Â', 'Ă', 'Ä', 'Ĺ', 'Ć', 'Ç',
		'Č', 'É', 'Ę', 'Ë', 'Ě', 'Í', 'Î', 'Ď', 'Đ', 'Ń',
		'Ň', 'Ó', 'Ô', 'Ő', 'Ö', '×', 'Ř', 'Ů', 'Ú', 'Ű',
		'Ü', 'Ý', 'Ţ', 'ß', 'ŕ', 'á', 'â', 'ă', 'ä', 'ĺ',
		'ć', 'ç', 'č', 'é', 'ę', 'ë', 'ě', 'í', 'î', 'ď',
		'đ', 'ń', 'ň', 'ó', 'ô', 'ő', 'ö', '÷', 'ř', 'ů',
		'ú', 'ű', 'ü', 'ý', 'ţ', '\u02d9'
	};

	public CP28592()
		: base(28592, ToChars, "Central European (ISO)", "iso-8859-2", "iso-8859-2", "iso-8859-2", isBrowserDisplay: true, isBrowserSave: true, isMailNewsDisplay: true, isMailNewsSave: true, 1250)
	{
	}

	protected unsafe override void ToBytes(char* chars, int charCount, byte* bytes, int byteCount)
	{
		int charIndex = 0;
		int byteIndex = 0;
		EncoderFallbackBuffer buffer = null;
		while (charCount > 0)
		{
			int num = *(ushort*)((byte*)chars + charIndex++ * 2);
			if (num >= 161)
			{
				switch (num)
				{
				case 162:
					num = 141;
					break;
				case 165:
					num = 142;
					break;
				case 169:
					num = 136;
					break;
				case 174:
					num = 159;
					break;
				case 182:
					num = 20;
					break;
				case 258:
					num = 195;
					break;
				case 259:
					num = 227;
					break;
				case 260:
					num = 161;
					break;
				case 261:
					num = 177;
					break;
				case 262:
					num = 198;
					break;
				case 263:
					num = 230;
					break;
				case 268:
					num = 200;
					break;
				case 269:
					num = 232;
					break;
				case 270:
					num = 207;
					break;
				case 271:
					num = 239;
					break;
				case 272:
					num = 208;
					break;
				case 273:
					num = 240;
					break;
				case 280:
					num = 202;
					break;
				case 281:
					num = 234;
					break;
				case 282:
					num = 204;
					break;
				case 283:
					num = 236;
					break;
				case 313:
					num = 197;
					break;
				case 314:
					num = 229;
					break;
				case 317:
					num = 165;
					break;
				case 318:
					num = 181;
					break;
				case 321:
					num = 163;
					break;
				case 322:
					num = 179;
					break;
				case 323:
					num = 209;
					break;
				case 324:
					num = 241;
					break;
				case 327:
					num = 210;
					break;
				case 328:
					num = 242;
					break;
				case 336:
					num = 213;
					break;
				case 337:
					num = 245;
					break;
				case 340:
					num = 192;
					break;
				case 341:
					num = 224;
					break;
				case 344:
					num = 216;
					break;
				case 345:
					num = 248;
					break;
				case 346:
					num = 166;
					break;
				case 347:
					num = 182;
					break;
				case 350:
					num = 170;
					break;
				case 351:
					num = 186;
					break;
				case 352:
					num = 169;
					break;
				case 353:
					num = 185;
					break;
				case 354:
					num = 222;
					break;
				case 355:
					num = 254;
					break;
				case 356:
					num = 171;
					break;
				case 357:
					num = 187;
					break;
				case 366:
					num = 217;
					break;
				case 367:
					num = 249;
					break;
				case 368:
					num = 219;
					break;
				case 369:
					num = 251;
					break;
				case 377:
					num = 172;
					break;
				case 378:
					num = 188;
					break;
				case 379:
					num = 175;
					break;
				case 380:
					num = 191;
					break;
				case 381:
					num = 174;
					break;
				case 382:
					num = 190;
					break;
				case 711:
					num = 183;
					break;
				case 728:
					num = 162;
					break;
				case 729:
					num = 255;
					break;
				case 731:
					num = 178;
					break;
				case 733:
					num = 189;
					break;
				case 8226:
					num = 7;
					break;
				case 8252:
					num = 19;
					break;
				case 8592:
					num = 27;
					break;
				case 8593:
					num = 24;
					break;
				case 8594:
					num = 26;
					break;
				case 8595:
					num = 25;
					break;
				case 8596:
					num = 29;
					break;
				case 8597:
					num = 18;
					break;
				case 8616:
					num = 23;
					break;
				case 8735:
					num = 28;
					break;
				case 9472:
					num = 148;
					break;
				case 9474:
					num = 131;
					break;
				case 9484:
					num = 134;
					break;
				case 9488:
					num = 143;
					break;
				case 9492:
					num = 144;
					break;
				case 9496:
					num = 133;
					break;
				case 9500:
					num = 147;
					break;
				case 9508:
					num = 132;
					break;
				case 9516:
					num = 146;
					break;
				case 9524:
					num = 145;
					break;
				case 9532:
					num = 149;
					break;
				case 9552:
					num = 157;
					break;
				case 9553:
					num = 138;
					break;
				case 9556:
					num = 153;
					break;
				case 9559:
					num = 139;
					break;
				case 9562:
					num = 152;
					break;
				case 9565:
					num = 140;
					break;
				case 9568:
					num = 156;
					break;
				case 9571:
					num = 137;
					break;
				case 9574:
					num = 155;
					break;
				case 9577:
					num = 154;
					break;
				case 9580:
					num = 158;
					break;
				case 9600:
					num = 151;
					break;
				case 9604:
					num = 150;
					break;
				case 9608:
					num = 135;
					break;
				case 9617:
					num = 128;
					break;
				case 9618:
					num = 129;
					break;
				case 9619:
					num = 130;
					break;
				case 9644:
					num = 22;
					break;
				case 9650:
					num = 30;
					break;
				case 9658:
					num = 16;
					break;
				case 9660:
					num = 31;
					break;
				case 9668:
					num = 17;
					break;
				case 9675:
					num = 9;
					break;
				case 9688:
					num = 8;
					break;
				case 9689:
					num = 10;
					break;
				case 9786:
					num = 1;
					break;
				case 9787:
					num = 2;
					break;
				case 9788:
					num = 15;
					break;
				case 9792:
					num = 12;
					break;
				case 9794:
					num = 11;
					break;
				case 9824:
					num = 6;
					break;
				case 9827:
					num = 5;
					break;
				case 9829:
					num = 3;
					break;
				case 9830:
					num = 4;
					break;
				case 9834:
					num = 13;
					break;
				case 9836:
					num = 14;
					break;
				case 65512:
					num = 131;
					break;
				case 65513:
					num = 27;
					break;
				case 65514:
					num = 24;
					break;
				case 65515:
					num = 26;
					break;
				case 65516:
					num = 25;
					break;
				case 65518:
					num = 9;
					break;
				default:
					if (num >= 65281 && num <= 65374)
					{
						num -= 65248;
					}
					else
					{
						HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					}
					break;
				case 164:
				case 167:
				case 168:
				case 173:
				case 176:
				case 180:
				case 184:
				case 193:
				case 194:
				case 196:
				case 199:
				case 201:
				case 203:
				case 205:
				case 206:
				case 208:
				case 211:
				case 212:
				case 214:
				case 215:
				case 218:
				case 220:
				case 221:
				case 223:
				case 225:
				case 226:
				case 228:
				case 231:
				case 233:
				case 235:
				case 237:
				case 238:
				case 243:
				case 244:
				case 246:
				case 247:
				case 250:
				case 252:
				case 253:
					break;
				}
			}
			bytes[byteIndex++] = (byte)num;
			charCount--;
			byteCount--;
		}
	}
}
[Serializable]
public class ENCiso_8859_2 : CP28592
{
}
[Serializable]
public class CP28593 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006', '\a', '\b', '\t',
		'\n', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019', '\u001a', '\u001b', '\u001c', '\u001d',
		'\u001e', '\u001f', ' ', '!', '"', '#', '$', '%', '&', '\'',
		'(', ')', '*', '+', ',', '-', '.', '/', '0', '1',
		'2', '3', '4', '5', '6', '7', '8', '9', ':', ';',
		'<', '=', '>', '?', '@', 'A', 'B', 'C', 'D', 'E',
		'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
		'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y',
		'Z', '[', '\\', ']', '^', '_', '`', 'a', 'b', 'c',
		'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
		'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w',
		'x', 'y', 'z', '{', '|', '}', '~', '\u007f', '\u0080', '\u0081',
		'\u0082', '\u0083', '\u0084', '\u0085', '\u0086', '\u0087', '\u0088', '\u0089', '\u008a', '\u008b',
		'\u008c', '\u008d', '\u008e', '\u008f', '\u0090', '\u0091', '\u0092', '\u0093', '\u0094', '\u0095',
		'\u0096', '\u0097', '\u0098', '\u0099', '\u009a', '\u009b', '\u009c', '\u009d', '\u009e', '\u009f',
		'\u00a0', 'Ħ', '\u02d8', '£', '¤', '?', 'Ĥ', '§', '\u00a8', 'İ',
		'Ş', 'Ğ', 'Ĵ', '\u00ad', '?', 'Ż', '°', 'ħ', '²', '³',
		'\u00b4', 'µ', 'ĥ', '·', '\u00b8', 'ı', 'ş', 'ğ', 'ĵ', '½',
		'?', 'ż', 'À', 'Á', 'Â', '?', 'Ä', 'Ċ', 'Ĉ', 'Ç',
		'È', 'É', 'Ê', 'Ë', 'Ì', 'Í', 'Î', 'Ï', '?', 'Ñ',
		'Ò', 'Ó', 'Ô', 'Ġ', 'Ö', '×', 'Ĝ', 'Ù', 'Ú', 'Û',
		'Ü', 'Ŭ', 'Ŝ', 'ß', 'à', 'á', 'â', '?', 'ä', 'ċ',
		'ĉ', 'ç', 'è', 'é', 'ê', 'ë', 'ì', 'í', 'î', 'ï',
		'?', 'ñ', 'ò', 'ó', 'ô', 'ġ', 'ö', '÷', 'ĝ', 'ù',
		'ú', 'û', 'ü', 'ŭ', 'ŝ', '\u02d9'
	};

	public CP28593()
		: base(28593, ToChars, "Latin 3 (ISO)", "iso-8859-3", "iso-8859-3", "iso-8859-3", isBrowserDisplay: true, isBrowserSave: true, isMailNewsDisplay: true, isMailNewsSave: true, 28593)
	{
	}

	protected unsafe override void ToBytes(char* chars, int charCount, byte* bytes, int byteCount)
	{
		int charIndex = 0;
		int byteIndex = 0;
		EncoderFallbackBuffer buffer = null;
		while (charCount > 0)
		{
			int num = *(ushort*)((byte*)chars + charIndex++ * 2);
			if (num >= 161)
			{
				switch (num)
				{
				case 264:
					num = 198;
					break;
				case 265:
					num = 230;
					break;
				case 266:
					num = 197;
					break;
				case 267:
					num = 229;
					break;
				case 284:
					num = 216;
					break;
				case 285:
					num = 248;
					break;
				case 286:
					num = 171;
					break;
				case 287:
					num = 187;
					break;
				case 288:
					num = 213;
					break;
				case 289:
					num = 245;
					break;
				case 292:
					num = 166;
					break;
				case 293:
					num = 182;
					break;
				case 294:
					num = 161;
					break;
				case 295:
					num = 177;
					break;
				case 304:
					num = 169;
					break;
				case 305:
					num = 185;
					break;
				case 308:
					num = 172;
					break;
				case 309:
					num = 188;
					break;
				case 348:
					num = 222;
					break;
				case 349:
					num = 254;
					break;
				case 350:
					num = 170;
					break;
				case 351:
					num = 186;
					break;
				case 364:
					num = 221;
					break;
				case 365:
					num = 253;
					break;
				case 379:
					num = 175;
					break;
				case 380:
					num = 191;
					break;
				case 728:
					num = 162;
					break;
				case 729:
					num = 255;
					break;
				default:
					if (num >= 65281 && num <= 65374)
					{
						num -= 65248;
					}
					else
					{
						HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					}
					break;
				case 163:
				case 164:
				case 167:
				case 168:
				case 173:
				case 176:
				case 178:
				case 179:
				case 180:
				case 181:
				case 183:
				case 184:
				case 189:
				case 192:
				case 193:
				case 194:
				case 196:
				case 199:
				case 200:
				case 201:
				case 202:
				case 203:
				case 204:
				case 205:
				case 206:
				case 207:
				case 209:
				case 210:
				case 211:
				case 212:
				case 214:
				case 215:
				case 217:
				case 218:
				case 219:
				case 220:
				case 223:
				case 224:
				case 225:
				case 226:
				case 228:
				case 231:
				case 232:
				case 233:
				case 234:
				case 235:
				case 236:
				case 237:
				case 238:
				case 239:
				case 241:
				case 242:
				case 243:
				case 244:
				case 246:
				case 247:
				case 249:
				case 250:
				case 251:
				case 252:
					break;
				}
			}
			bytes[byteIndex++] = (byte)num;
			charCount--;
			byteCount--;
		}
	}
}
[Serializable]
public class ENCiso_8859_3 : CP28593
{
}
[Serializable]
public class CP28597 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006', '\a', '\b', '\t',
		'\n', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019', '\u001a', '\u001b', '\u001c', '\u001d',
		'\u001e', '\u001f', ' ', '!', '"', '#', '$', '%', '&', '\'',
		'(', ')', '*', '+', ',', '-', '.', '/', '0', '1',
		'2', '3', '4', '5', '6', '7', '8', '9', ':', ';',
		'<', '=', '>', '?', '@', 'A', 'B', 'C', 'D', 'E',
		'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
		'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y',
		'Z', '[', '\\', ']', '^', '_', '`', 'a', 'b', 'c',
		'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
		'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w',
		'x', 'y', 'z', '{', '|', '}', '~', '\u007f', '\u0080', '\u0081',
		'\u0082', '\u0083', '\u0084', '\u0085', '\u0086', '\u0087', '\u0088', '\u0089', '\u008a', '\u008b',
		'\u008c', '\u008d', '\u008e', '\u008f', '\u0090', '\u0091', '\u0092', '\u0093', '\u0094', '\u0095',
		'\u0096', '\u0097', '\u0098', '\u0099', '\u009a', '\u009b', '\u009c', '\u009d', '\u009e', '\u009f',
		'\u00a0', '‘', '’', '£', '€', '?', '¦', '§', '\u00a8', '©',
		'?', '«', '¬', '\u00ad', '?', '―', '°', '±', '²', '³',
		'\u00b4', '\u0385', 'Ά', '·', 'Έ', 'Ή', 'Ί', '»', 'Ό', '½',
		'Ύ', 'Ώ', 'ΐ', 'Α', 'Β', 'Γ', 'Δ', 'Ε', 'Ζ', 'Η',
		'Θ', 'Ι', 'Κ', 'Λ', 'Μ', 'Ν', 'Ξ', 'Ο', 'Π', 'Ρ',
		'?', 'Σ', 'Τ', 'Υ', 'Φ', 'Χ', 'Ψ', 'Ω', 'Ϊ', 'Ϋ',
		'ά', 'έ', 'ή', 'ί', 'ΰ', 'α', 'β', 'γ', 'δ', 'ε',
		'ζ', 'η', 'θ', 'ι', 'κ', 'λ', 'μ', 'ν', 'ξ', 'ο',
		'π', 'ρ', 'ς', 'σ', 'τ', 'υ', 'φ', 'χ', 'ψ', 'ω',
		'ϊ', 'ϋ', 'ό', 'ύ', 'ώ', '?'
	};

	public CP28597()
		: base(28597, ToChars, "Greek (ISO)", "iso-8859-7", "iso-8859-7", "iso-8859-7", isBrowserDisplay: true, isBrowserSave: true, isMailNewsDisplay: true, isMailNewsSave: true, 1253)
	{
	}

	protected unsafe override void ToBytes(char* chars, int charCount, byte* bytes, int byteCount)
	{
		int charIndex = 0;
		int byteIndex = 0;
		EncoderFallbackBuffer buffer = null;
		while (charCount > 0)
		{
			int num = *(ushort*)((byte*)chars + charIndex++ * 2);
			if (num >= 161)
			{
				switch (num)
				{
				case 901:
				case 902:
				case 903:
				case 904:
				case 905:
				case 906:
					num -= 720;
					break;
				case 908:
					num = 188;
					break;
				case 910:
				case 911:
				case 912:
				case 913:
				case 914:
				case 915:
				case 916:
				case 917:
				case 918:
				case 919:
				case 920:
				case 921:
				case 922:
				case 923:
				case 924:
				case 925:
				case 926:
				case 927:
				case 928:
				case 929:
					num -= 720;
					break;
				case 931:
				case 932:
				case 933:
				case 934:
				case 935:
				case 936:
				case 937:
				case 938:
				case 939:
				case 940:
				case 941:
				case 942:
				case 943:
				case 944:
				case 945:
				case 946:
				case 947:
				case 948:
				case 949:
				case 950:
				case 951:
				case 952:
				case 953:
				case 954:
				case 955:
				case 956:
				case 957:
				case 958:
				case 959:
				case 960:
				case 961:
				case 962:
				case 963:
				case 964:
				case 965:
				case 966:
				case 967:
				case 968:
				case 969:
				case 970:
				case 971:
				case 972:
				case 973:
				case 974:
					num -= 720;
					break;
				case 981:
					num = 246;
					break;
				case 8213:
					num = 175;
					break;
				case 8216:
					num = 161;
					break;
				case 8217:
					num = 162;
					break;
				case 8364:
					num = 164;
					break;
				default:
					if (num >= 65281 && num <= 65374)
					{
						num -= 65248;
					}
					else
					{
						HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					}
					break;
				case 163:
				case 166:
				case 167:
				case 168:
				case 169:
				case 171:
				case 172:
				case 173:
				case 176:
				case 177:
				case 178:
				case 179:
				case 180:
				case 183:
				case 187:
				case 189:
					break;
				}
			}
			bytes[byteIndex++] = (byte)num;
			charCount--;
			byteCount--;
		}
	}
}
[Serializable]
public class ENCiso_8859_7 : CP28597
{
}
[Serializable]
public class CP28605 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006', '\a', '\b', '\t',
		'\n', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019', '\u001a', '\u001b', '\u001c', '\u001d',
		'\u001e', '\u001f', ' ', '!', '"', '#', '$', '%', '&', '\'',
		'(', ')', '*', '+', ',', '-', '.', '/', '0', '1',
		'2', '3', '4', '5', '6', '7', '8', '9', ':', ';',
		'<', '=', '>', '?', '@', 'A', 'B', 'C', 'D', 'E',
		'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
		'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y',
		'Z', '[', '\\', ']', '^', '_', '`', 'a', 'b', 'c',
		'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
		'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w',
		'x', 'y', 'z', '{', '|', '}', '~', '\u007f', '\u0080', '\u0081',
		'\u0082', '\u0083', '\u0084', '\u0085', '\u0086', '\u0087', '\u0088', '\u0089', '\u008a', '\u008b',
		'\u008c', '\u008d', '\u008e', '\u008f', '\u0090', '\u0091', '\u0092', '\u0093', '\u0094', '\u0095',
		'\u0096', '\u0097', '\u0098', '\u0099', '\u009a', '\u009b', '\u009c', '\u009d', '\u009e', '\u009f',
		'\u00a0', '¡', '¢', '£', '€', '¥', 'Š', '§', 'š', '©',
		'ª', '«', '¬', '\u00ad', '®', '\u00af', '°', '±', '²', '³',
		'Ž', 'µ', '¶', '·', 'ž', '¹', 'º', '»', 'Œ', 'œ',
		'Ÿ', '¿', 'À', 'Á', 'Â', 'Ã', 'Ä', 'Å', 'Æ', 'Ç',
		'È', 'É', 'Ê', 'Ë', 'Ì', 'Í', 'Î', 'Ï', 'Ð', 'Ñ',
		'Ò', 'Ó', 'Ô', 'Õ', 'Ö', '×', 'Ø', 'Ù', 'Ú', 'Û',
		'Ü', 'Ý', 'Þ', 'ß', 'à', 'á', 'â', 'ã', 'ä', 'å',
		'æ', 'ç', 'è', 'é', 'ê', 'ë', 'ì', 'í', 'î', 'ï',
		'ð', 'ñ', 'ò', 'ó', 'ô', 'õ', 'ö', '÷', 'ø', 'ù',
		'ú', 'û', 'ü', 'ý', 'þ', 'ÿ'
	};

	public CP28605()
		: base(28605, ToChars, "Latin 9 (ISO)", "iso-8859-15", "iso-8859-15", "iso-8859-15", isBrowserDisplay: false, isBrowserSave: true, isMailNewsDisplay: true, isMailNewsSave: true, 1252)
	{
	}

	protected unsafe override void ToBytes(char* chars, int charCount, byte* bytes, int byteCount)
	{
		int charIndex = 0;
		int byteIndex = 0;
		EncoderFallbackBuffer buffer = null;
		while (charCount > 0)
		{
			int num = *(ushort*)((byte*)chars + charIndex++ * 2);
			if (num >= 164)
			{
				switch (num)
				{
				case 338:
					num = 188;
					break;
				case 339:
					num = 189;
					break;
				case 352:
					num = 166;
					break;
				case 353:
					num = 168;
					break;
				case 376:
					num = 190;
					break;
				case 381:
					num = 180;
					break;
				case 382:
					num = 184;
					break;
				case 8364:
					num = 164;
					break;
				default:
					if (num >= 65281 && num <= 65374)
					{
						num -= 65248;
					}
					else
					{
						HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					}
					break;
				case 165:
				case 167:
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
				case 181:
				case 182:
				case 183:
				case 185:
				case 186:
				case 187:
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
				case 255:
					break;
				}
			}
			bytes[byteIndex++] = (byte)num;
			charCount--;
			byteCount--;
		}
	}
}
[Serializable]
public class ENCiso_8859_15 : CP28605
{
}
[Serializable]
public class CP437 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006', '\a', '\b', '\t',
		'\n', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019', '\u001a', '\u001b', '\u001c', '\u001d',
		'\u001e', '\u001f', ' ', '!', '"', '#', '$', '%', '&', '\'',
		'(', ')', '*', '+', ',', '-', '.', '/', '0', '1',
		'2', '3', '4', '5', '6', '7', '8', '9', ':', ';',
		'<', '=', '>', '?', '@', 'A', 'B', 'C', 'D', 'E',
		'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
		'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y',
		'Z', '[', '\\', ']', '^', '_', '`', 'a', 'b', 'c',
		'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
		'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w',
		'x', 'y', 'z', '{', '|', '}', '~', '\u007f', 'Ç', 'ü',
		'é', 'â', 'ä', 'à', 'å', 'ç', 'ê', 'ë', 'è', 'ï',
		'î', 'ì', 'Ä', 'Å', 'É', 'æ', 'Æ', 'ô', 'ö', 'ò',
		'û', 'ù', 'ÿ', 'Ö', 'Ü', '¢', '£', '¥', '₧', 'ƒ',
		'á', 'í', 'ó', 'ú', 'ñ', 'Ñ', 'ª', 'º', '¿', '⌐',
		'¬', '½', '¼', '¡', '«', '»', '░', '▒', '▓', '│',
		'┤', '╡', '╢', '╖', '╕', '╣', '║', '╗', '╝', '╜',
		'╛', '┐', '└', '┴', '┬', '├', '─', '┼', '╞', '╟',
		'╚', '╔', '╩', '╦', '╠', '═', '╬', '╧', '╨', '╤',
		'╥', '╙', '╘', '╒', '╓', '╫', '╪', '┘', '┌', '█',
		'▄', '▌', '▐', '▀', 'α', 'ß', 'Γ', 'π', 'Σ', 'σ',
		'µ', 'τ', 'Φ', 'Θ', 'Ω', 'δ', '∞', 'φ', 'ε', '∩',
		'≡', '±', '≥', '≤', '⌠', '⌡', '÷', '≈', '°', '∙',
		'·', '√', 'ⁿ', '²', '■', '\u00a0'
	};

	public CP437()
		: base(437, ToChars, "OEM United States", "IBM437", "IBM437", "IBM437", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
	{
	}

	protected unsafe override void ToBytes(char* chars, int charCount, byte* bytes, int byteCount)
	{
		int charIndex = 0;
		int byteIndex = 0;
		EncoderFallbackBuffer buffer = null;
		while (charCount > 0)
		{
			int num = *(ushort*)((byte*)chars + charIndex++ * 2);
			if (num >= 128)
			{
				switch (num)
				{
				case 160:
					num = 255;
					break;
				case 161:
					num = 173;
					break;
				case 162:
					num = 155;
					break;
				case 163:
					num = 156;
					break;
				case 164:
					num = 15;
					break;
				case 165:
					num = 157;
					break;
				case 166:
					num = 221;
					break;
				case 167:
					num = 21;
					break;
				case 168:
					num = 34;
					break;
				case 169:
					num = 99;
					break;
				case 170:
					num = 166;
					break;
				case 171:
					num = 174;
					break;
				case 172:
					num = 170;
					break;
				case 173:
					num = 45;
					break;
				case 174:
					num = 114;
					break;
				case 175:
					num = 95;
					break;
				case 176:
					num = 248;
					break;
				case 177:
					num = 241;
					break;
				case 178:
					num = 253;
					break;
				case 179:
					num = 51;
					break;
				case 180:
					num = 39;
					break;
				case 181:
					num = 230;
					break;
				case 182:
					num = 20;
					break;
				case 183:
					num = 250;
					break;
				case 184:
					num = 44;
					break;
				case 185:
					num = 49;
					break;
				case 186:
					num = 167;
					break;
				case 187:
					num = 175;
					break;
				case 188:
					num = 172;
					break;
				case 189:
					num = 171;
					break;
				case 190:
					num = 95;
					break;
				case 191:
					num = 168;
					break;
				case 192:
					num = 65;
					break;
				case 193:
					num = 65;
					break;
				case 194:
					num = 65;
					break;
				case 195:
					num = 65;
					break;
				case 196:
					num = 142;
					break;
				case 197:
					num = 143;
					break;
				case 198:
					num = 146;
					break;
				case 199:
					num = 128;
					break;
				case 200:
					num = 69;
					break;
				case 201:
					num = 144;
					break;
				case 202:
					num = 69;
					break;
				case 203:
					num = 69;
					break;
				case 204:
					num = 73;
					break;
				case 205:
					num = 73;
					break;
				case 206:
					num = 73;
					break;
				case 207:
					num = 73;
					break;
				case 208:
					num = 68;
					break;
				case 209:
					num = 165;
					break;
				case 210:
					num = 79;
					break;
				case 211:
					num = 79;
					break;
				case 212:
					num = 79;
					break;
				case 213:
					num = 79;
					break;
				case 214:
					num = 153;
					break;
				case 215:
					num = 120;
					break;
				case 216:
					num = 79;
					break;
				case 217:
					num = 85;
					break;
				case 218:
					num = 85;
					break;
				case 219:
					num = 85;
					break;
				case 220:
					num = 154;
					break;
				case 221:
					num = 89;
					break;
				case 222:
					num = 95;
					break;
				case 223:
					num = 225;
					break;
				case 224:
					num = 133;
					break;
				case 225:
					num = 160;
					break;
				case 226:
					num = 131;
					break;
				case 227:
					num = 97;
					break;
				case 228:
					num = 132;
					break;
				case 229:
					num = 134;
					break;
				case 230:
					num = 145;
					break;
				case 231:
					num = 135;
					break;
				case 232:
					num = 138;
					break;
				case 233:
					num = 130;
					break;
				case 234:
					num = 136;
					break;
				case 235:
					num = 137;
					break;
				case 236:
					num = 141;
					break;
				case 237:
					num = 161;
					break;
				case 238:
					num = 140;
					break;
				case 239:
					num = 139;
					break;
				case 240:
					num = 100;
					break;
				case 241:
					num = 164;
					break;
				case 242:
					num = 149;
					break;
				case 243:
					num = 162;
					break;
				case 244:
					num = 147;
					break;
				case 245:
					num = 111;
					break;
				case 246:
					num = 148;
					break;
				case 247:
					num = 246;
					break;
				case 248:
					num = 111;
					break;
				case 249:
					num = 151;
					break;
				case 250:
					num = 163;
					break;
				case 251:
					num = 150;
					break;
				case 252:
					num = 129;
					break;
				case 253:
					num = 121;
					break;
				case 254:
					num = 95;
					break;
				case 255:
					num = 152;
					break;
				case 256:
					num = 65;
					break;
				case 257:
					num = 97;
					break;
				case 258:
					num = 65;
					break;
				case 259:
					num = 97;
					break;
				case 260:
					num = 65;
					break;
				case 261:
					num = 97;
					break;
				case 262:
					num = 67;
					break;
				case 263:
					num = 99;
					break;
				case 264:
					num = 67;
					break;
				case 265:
					num = 99;
					break;
				case 266:
					num = 67;
					break;
				case 267:
					num = 99;
					break;
				case 268:
					num = 67;
					break;
				case 269:
					num = 99;
					break;
				case 270:
					num = 68;
					break;
				case 271:
					num = 100;
					break;
				case 272:
					num = 68;
					break;
				case 273:
					num = 100;
					break;
				case 274:
					num = 69;
					break;
				case 275:
					num = 101;
					break;
				case 276:
					num = 69;
					break;
				case 277:
					num = 101;
					break;
				case 278:
					num = 69;
					break;
				case 279:
					num = 101;
					break;
				case 280:
					num = 69;
					break;
				case 281:
					num = 101;
					break;
				case 282:
					num = 69;
					break;
				case 283:
					num = 101;
					break;
				case 284:
					num = 71;
					break;
				case 285:
					num = 103;
					break;
				case 286:
					num = 71;
					break;
				case 287:
					num = 103;
					break;
				case 288:
					num = 71;
					break;
				case 289:
					num = 103;
					break;
				case 290:
					num = 71;
					break;
				case 291:
					num = 103;
					break;
				case 292:
					num = 72;
					break;
				case 293:
					num = 104;
					break;
				case 294:
					num = 72;
					break;
				case 295:
					num = 104;
					break;
				case 296:
					num = 73;
					break;
				case 297:
					num = 105;
					break;
				case 298:
					num = 73;
					break;
				case 299:
					num = 105;
					break;
				case 300:
					num = 73;
					break;
				case 301:
					num = 105;
					break;
				case 302:
					num = 73;
					break;
				case 303:
					num = 105;
					break;
				case 304:
					num = 73;
					break;
				case 305:
					num = 105;
					break;
				case 308:
					num = 74;
					break;
				case 309:
					num = 106;
					break;
				case 310:
					num = 75;
					break;
				case 311:
					num = 107;
					break;
				case 313:
					num = 76;
					break;
				case 314:
					num = 108;
					break;
				case 315:
					num = 76;
					break;
				case 316:
					num = 108;
					break;
				case 317:
					num = 76;
					break;
				case 318:
					num = 108;
					break;
				case 321:
					num = 76;
					break;
				case 322:
					num = 108;
					break;
				case 323:
					num = 78;
					break;
				case 324:
					num = 110;
					break;
				case 325:
					num = 78;
					break;
				case 326:
					num = 110;
					break;
				case 327:
					num = 78;
					break;
				case 328:
					num = 110;
					break;
				case 332:
					num = 79;
					break;
				case 333:
					num = 111;
					break;
				case 334:
					num = 79;
					break;
				case 335:
					num = 111;
					break;
				case 336:
					num = 79;
					break;
				case 337:
					num = 111;
					break;
				case 338:
					num = 79;
					break;
				case 339:
					num = 111;
					break;
				case 340:
					num = 82;
					break;
				case 341:
					num = 114;
					break;
				case 342:
					num = 82;
					break;
				case 343:
					num = 114;
					break;
				case 344:
					num = 82;
					break;
				case 345:
					num = 114;
					break;
				case 346:
					num = 83;
					break;
				case 347:
					num = 115;
					break;
				case 348:
					num = 83;
					break;
				case 349:
					num = 115;
					break;
				case 350:
					num = 83;
					break;
				case 351:
					num = 115;
					break;
				case 352:
					num = 83;
					break;
				case 353:
					num = 115;
					break;
				case 354:
					num = 84;
					break;
				case 355:
					num = 116;
					break;
				case 356:
					num = 84;
					break;
				case 357:
					num = 116;
					break;
				case 358:
					num = 84;
					break;
				case 359:
					num = 116;
					break;
				case 360:
					num = 85;
					break;
				case 361:
					num = 117;
					break;
				case 362:
					num = 85;
					break;
				case 363:
					num = 117;
					break;
				case 364:
					num = 85;
					break;
				case 365:
					num = 117;
					break;
				case 366:
					num = 85;
					break;
				case 367:
					num = 117;
					break;
				case 368:
					num = 85;
					break;
				case 369:
					num = 117;
					break;
				case 370:
					num = 85;
					break;
				case 371:
					num = 117;
					break;
				case 372:
					num = 87;
					break;
				case 373:
					num = 119;
					break;
				case 374:
					num = 89;
					break;
				case 375:
					num = 121;
					break;
				case 376:
					num = 89;
					break;
				case 377:
					num = 90;
					break;
				case 378:
					num = 122;
					break;
				case 379:
					num = 90;
					break;
				case 380:
					num = 122;
					break;
				case 381:
					num = 90;
					break;
				case 382:
					num = 122;
					break;
				case 384:
					num = 98;
					break;
				case 393:
					num = 68;
					break;
				case 401:
					num = 159;
					break;
				case 402:
					num = 159;
					break;
				case 407:
					num = 73;
					break;
				case 410:
					num = 108;
					break;
				case 415:
					num = 79;
					break;
				case 416:
					num = 79;
					break;
				case 417:
					num = 111;
					break;
				case 425:
					num = 228;
					break;
				case 427:
					num = 116;
					break;
				case 430:
					num = 84;
					break;
				case 431:
					num = 85;
					break;
				case 432:
					num = 117;
					break;
				case 438:
					num = 122;
					break;
				case 448:
					num = 124;
					break;
				case 451:
					num = 33;
					break;
				case 461:
					num = 65;
					break;
				case 462:
					num = 97;
					break;
				case 463:
					num = 73;
					break;
				case 464:
					num = 105;
					break;
				case 465:
					num = 79;
					break;
				case 466:
					num = 111;
					break;
				case 467:
					num = 85;
					break;
				case 468:
					num = 117;
					break;
				case 469:
					num = 85;
					break;
				case 470:
					num = 117;
					break;
				case 471:
					num = 85;
					break;
				case 472:
					num = 117;
					break;
				case 473:
					num = 85;
					break;
				case 474:
					num = 117;
					break;
				case 475:
					num = 85;
					break;
				case 476:
					num = 117;
					break;
				case 478:
					num = 65;
					break;
				case 479:
					num = 97;
					break;
				case 484:
					num = 71;
					break;
				case 485:
					num = 103;
					break;
				case 486:
					num = 71;
					break;
				case 487:
					num = 103;
					break;
				case 488:
					num = 75;
					break;
				case 489:
					num = 107;
					break;
				case 490:
					num = 79;
					break;
				case 491:
					num = 111;
					break;
				case 492:
					num = 79;
					break;
				case 493:
					num = 111;
					break;
				case 496:
					num = 106;
					break;
				case 609:
					num = 103;
					break;
				case 632:
					num = 237;
					break;
				case 697:
					num = 39;
					break;
				case 698:
					num = 34;
					break;
				case 700:
					num = 39;
					break;
				case 708:
					num = 94;
					break;
				case 710:
					num = 94;
					break;
				case 712:
					num = 39;
					break;
				case 713:
					num = 196;
					break;
				case 714:
					num = 39;
					break;
				case 715:
					num = 96;
					break;
				case 717:
					num = 95;
					break;
				case 730:
					num = 248;
					break;
				case 732:
					num = 126;
					break;
				case 768:
					num = 96;
					break;
				case 769:
					num = 39;
					break;
				case 770:
					num = 94;
					break;
				case 771:
					num = 126;
					break;
				case 772:
					num = 196;
					break;
				case 776:
					num = 34;
					break;
				case 778:
					num = 248;
					break;
				case 782:
					num = 34;
					break;
				case 807:
					num = 44;
					break;
				case 817:
					num = 95;
					break;
				case 818:
					num = 95;
					break;
				case 894:
					num = 59;
					break;
				case 913:
					num = 224;
					break;
				case 915:
					num = 226;
					break;
				case 916:
					num = 235;
					break;
				case 917:
					num = 238;
					break;
				case 920:
					num = 233;
					break;
				case 928:
					num = 227;
					break;
				case 931:
					num = 228;
					break;
				case 932:
					num = 231;
					break;
				case 934:
					num = 232;
					break;
				case 937:
					num = 234;
					break;
				case 945:
					num = 224;
					break;
				case 946:
					num = 225;
					break;
				case 948:
					num = 235;
					break;
				case 949:
					num = 238;
					break;
				case 956:
					num = 230;
					break;
				case 960:
					num = 227;
					break;
				case 963:
					num = 229;
					break;
				case 964:
					num = 231;
					break;
				case 966:
					num = 237;
					break;
				case 1211:
					num = 104;
					break;
				case 1417:
					num = 58;
					break;
				case 1642:
					num = 37;
					break;
				case 8192:
					num = 32;
					break;
				case 8193:
					num = 32;
					break;
				case 8194:
					num = 32;
					break;
				case 8195:
					num = 32;
					break;
				case 8196:
					num = 32;
					break;
				case 8197:
					num = 32;
					break;
				case 8198:
					num = 32;
					break;
				case 8208:
					num = 45;
					break;
				case 8209:
					num = 45;
					break;
				case 8211:
					num = 45;
					break;
				case 8212:
					num = 45;
					break;
				case 8215:
					num = 95;
					break;
				case 8216:
					num = 96;
					break;
				case 8217:
					num = 39;
					break;
				case 8218:
					num = 44;
					break;
				case 8220:
					num = 34;
					break;
				case 8221:
					num = 34;
					break;
				case 8222:
					num = 44;
					break;
				case 8224:
					num = 43;
					break;
				case 8225:
					num = 216;
					break;
				case 8226:
					num = 7;
					break;
				case 8228:
					num = 250;
					break;
				case 8230:
					num = 46;
					break;
				case 8240:
					num = 37;
					break;
				case 8242:
					num = 39;
					break;
				case 8245:
					num = 96;
					break;
				case 8249:
					num = 60;
					break;
				case 8250:
					num = 62;
					break;
				case 8252:
					num = 19;
					break;
				case 8260:
					num = 47;
					break;
				case 8304:
					num = 248;
					break;
				case 8308:
				case 8309:
				case 8310:
				case 8311:
				case 8312:
					num -= 8256;
					break;
				case 8319:
					num = 252;
					break;
				case 8320:
				case 8321:
				case 8322:
				case 8323:
				case 8324:
				case 8325:
				case 8326:
				case 8327:
				case 8328:
				case 8329:
					num -= 8272;
					break;
				case 8356:
					num = 156;
					break;
				case 8359:
					num = 158;
					break;
				case 8413:
					num = 9;
					break;
				case 8450:
					num = 67;
					break;
				case 8455:
					num = 69;
					break;
				case 8458:
					num = 103;
					break;
				case 8459:
					num = 72;
					break;
				case 8460:
					num = 72;
					break;
				case 8461:
					num = 72;
					break;
				case 8462:
					num = 104;
					break;
				case 8464:
					num = 73;
					break;
				case 8465:
					num = 73;
					break;
				case 8466:
					num = 76;
					break;
				case 8467:
					num = 108;
					break;
				case 8469:
					num = 78;
					break;
				case 8472:
					num = 80;
					break;
				case 8473:
					num = 80;
					break;
				case 8474:
					num = 81;
					break;
				case 8475:
					num = 82;
					break;
				case 8476:
					num = 82;
					break;
				case 8477:
					num = 82;
					break;
				case 8482:
					num = 84;
					break;
				case 8484:
					num = 90;
					break;
				case 8486:
					num = 234;
					break;
				case 8488:
					num = 90;
					break;
				case 8490:
					num = 75;
					break;
				case 8491:
					num = 143;
					break;
				case 8492:
					num = 66;
					break;
				case 8493:
					num = 67;
					break;
				case 8494:
					num = 101;
					break;
				case 8495:
					num = 101;
					break;
				case 8496:
					num = 69;
					break;
				case 8497:
					num = 70;
					break;
				case 8499:
					num = 77;
					break;
				case 8500:
					num = 111;
					break;
				case 8592:
					num = 27;
					break;
				case 8593:
					num = 24;
					break;
				case 8594:
					num = 26;
					break;
				case 8595:
					num = 25;
					break;
				case 8596:
					num = 29;
					break;
				case 8597:
					num = 18;
					break;
				case 8616:
					num = 23;
					break;
				case 8709:
					num = 237;
					break;
				case 8721:
					num = 228;
					break;
				case 8722:
					num = 45;
					break;
				case 8723:
					num = 241;
					break;
				case 8725:
					num = 47;
					break;
				case 8726:
					num = 92;
					break;
				case 8727:
					num = 42;
					break;
				case 8728:
					num = 248;
					break;
				case 8729:
					num = 249;
					break;
				case 8730:
					num = 251;
					break;
				case 8734:
					num = 236;
					break;
				case 8735:
					num = 28;
					break;
				case 8739:
					num = 124;
					break;
				case 8745:
					num = 239;
					break;
				case 8758:
					num = 58;
					break;
				case 8764:
					num = 126;
					break;
				case 8776:
					num = 247;
					break;
				case 8801:
					num = 240;
					break;
				case 8804:
					num = 243;
					break;
				case 8805:
					num = 242;
					break;
				case 8810:
					num = 174;
					break;
				case 8811:
					num = 175;
					break;
				case 8901:
					num = 250;
					break;
				case 8962:
					num = 127;
					break;
				case 8963:
					num = 94;
					break;
				case 8976:
					num = 169;
					break;
				case 8992:
					num = 244;
					break;
				case 8993:
					num = 245;
					break;
				case 9001:
					num = 60;
					break;
				case 9002:
					num = 62;
					break;
				case 9472:
					num = 196;
					break;
				case 9474:
					num = 179;
					break;
				case 9484:
					num = 218;
					break;
				case 9488:
					num = 191;
					break;
				case 9492:
					num = 192;
					break;
				case 9496:
					num = 217;
					break;
				case 9500:
					num = 195;
					break;
				case 9508:
					num = 180;
					break;
				case 9516:
					num = 194;
					break;
				case 9524:
					num = 193;
					break;
				case 9532:
					num = 197;
					break;
				case 9552:
					num = 205;
					break;
				case 9553:
					num = 186;
					break;
				case 9554:
					num = 213;
					break;
				case 9555:
					num = 214;
					break;
				case 9556:
					num = 201;
					break;
				case 9557:
					num = 184;
					break;
				case 9558:
					num = 183;
					break;
				case 9559:
					num = 187;
					break;
				case 9560:
					num = 212;
					break;
				case 9561:
					num = 211;
					break;
				case 9562:
					num = 200;
					break;
				case 9563:
					num = 190;
					break;
				case 9564:
					num = 189;
					break;
				case 9565:
					num = 188;
					break;
				case 9566:
					num = 198;
					break;
				case 9567:
					num = 199;
					break;
				case 9568:
					num = 204;
					break;
				case 9569:
					num = 181;
					break;
				case 9570:
					num = 182;
					break;
				case 9571:
					num = 185;
					break;
				case 9572:
					num = 209;
					break;
				case 9573:
					num = 210;
					break;
				case 9574:
					num = 203;
					break;
				case 9575:
					num = 207;
					break;
				case 9576:
					num = 208;
					break;
				case 9577:
					num = 202;
					break;
				case 9578:
					num = 216;
					break;
				case 9579:
					num = 215;
					break;
				case 9580:
					num = 206;
					break;
				case 9600:
					num = 223;
					break;
				case 9604:
					num = 220;
					break;
				case 9608:
					num = 219;
					break;
				case 9612:
					num = 221;
					break;
				case 9616:
					num = 222;
					break;
				case 9617:
					num = 176;
					break;
				case 9618:
					num = 177;
					break;
				case 9619:
					num = 178;
					break;
				case 9632:
					num = 254;
					break;
				case 9644:
					num = 22;
					break;
				case 9650:
					num = 30;
					break;
				case 9658:
					num = 16;
					break;
				case 9660:
					num = 31;
					break;
				case 9668:
					num = 17;
					break;
				case 9675:
					num = 9;
					break;
				case 9688:
					num = 8;
					break;
				case 9689:
					num = 10;
					break;
				case 9786:
					num = 1;
					break;
				case 9787:
					num = 2;
					break;
				case 9788:
					num = 15;
					break;
				case 9792:
					num = 12;
					break;
				case 9794:
					num = 11;
					break;
				case 9824:
					num = 6;
					break;
				case 9827:
					num = 5;
					break;
				case 9829:
					num = 3;
					break;
				case 9830:
					num = 4;
					break;
				case 9834:
					num = 13;
					break;
				case 9835:
					num = 14;
					break;
				case 10003:
					num = 251;
					break;
				case 10072:
					num = 124;
					break;
				case 12288:
					num = 32;
					break;
				case 12295:
					num = 9;
					break;
				case 12296:
					num = 60;
					break;
				case 12297:
					num = 62;
					break;
				case 12298:
					num = 174;
					break;
				case 12299:
					num = 175;
					break;
				case 12314:
					num = 91;
					break;
				case 12315:
					num = 93;
					break;
				case 12539:
					num = 250;
					break;
				case 65281:
				case 65282:
				case 65283:
				case 65284:
				case 65285:
				case 65286:
				case 65287:
				case 65288:
				case 65289:
				case 65290:
				case 65291:
				case 65292:
				case 65293:
				case 65294:
				case 65295:
				case 65296:
				case 65297:
				case 65298:
				case 65299:
				case 65300:
				case 65301:
				case 65302:
				case 65303:
				case 65304:
				case 65305:
				case 65306:
				case 65307:
				case 65308:
				case 65309:
				case 65310:
					num -= 65248;
					break;
				case 65312:
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
				case 65339:
				case 65340:
				case 65341:
				case 65342:
				case 65343:
				case 65344:
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
				case 65353:
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
				case 65361:
				case 65362:
				case 65363:
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
				case 65371:
				case 65372:
				case 65373:
				case 65374:
					num -= 65248;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				}
			}
			bytes[byteIndex++] = (byte)num;
			charCount--;
			byteCount--;
		}
	}
}
[Serializable]
public class ENCibm437 : CP437
{
}
[Serializable]
public class CP850 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006', '\a', '\b', '\t',
		'\n', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019', '\u001c', '\u001b', '\u007f', '\u001d',
		'\u001e', '\u001f', ' ', '!', '"', '#', '$', '%', '&', '\'',
		'(', ')', '*', '+', ',', '-', '.', '/', '0', '1',
		'2', '3', '4', '5', '6', '7', '8', '9', ':', ';',
		'<', '=', '>', '?', '@', 'A', 'B', 'C', 'D', 'E',
		'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
		'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y',
		'Z', '[', '\\', ']', '^', '_', '`', 'a', 'b', 'c',
		'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
		'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w',
		'x', 'y', 'z', '{', '|', '}', '~', '\u001a', 'Ç', 'ü',
		'é', 'â', 'ä', 'à', 'å', 'ç', 'ê', 'ë', 'è', 'ï',
		'î', 'ì', 'Ä', 'Å', 'É', 'æ', 'Æ', 'ô', 'ö', 'ò',
		'û', 'ù', 'ÿ', 'Ö', 'Ü', 'ø', '£', 'Ø', '×', 'ƒ',
		'á', 'í', 'ó', 'ú', 'ñ', 'Ñ', 'ª', 'º', '¿', '®',
		'¬', '½', '¼', '¡', '«', '»', '░', '▒', '▓', '│',
		'┤', 'Á', 'Â', 'À', '©', '╣', '║', '╗', '╝', '¢',
		'¥', '┐', '└', '┴', '┬', '├', '─', '┼', 'ã', 'Ã',
		'╚', '╔', '╩', '╦', '╠', '═', '╬', '¤', 'ð', 'Ð',
		'Ê', 'Ë', 'È', 'ı', 'Í', 'Î', 'Ï', '┘', '┌', '█',
		'▄', '¦', 'Ì', '▀', 'Ó', 'ß', 'Ô', 'Ò', 'õ', 'Õ',
		'µ', 'þ', 'Þ', 'Ú', 'Û', 'Ù', 'ý', 'Ý', '\u00af', '\u00b4',
		'\u00ad', '±', '‗', '¾', '¶', '§', '÷', '\u00b8', '°', '\u00a8',
		'·', '¹', '³', '²', '■', '\u00a0'
	};

	public CP850()
		: base(850, ToChars, "Western European (DOS)", "ibm850", "ibm850", "ibm850", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
	{
	}

	protected unsafe override void ToBytes(char* chars, int charCount, byte* bytes, int byteCount)
	{
		int charIndex = 0;
		int byteIndex = 0;
		EncoderFallbackBuffer buffer = null;
		while (charCount > 0)
		{
			int num = *(ushort*)((byte*)chars + charIndex++ * 2);
			if (num >= 26)
			{
				switch (num)
				{
				case 26:
					num = 127;
					break;
				case 28:
					num = 26;
					break;
				case 127:
					num = 28;
					break;
				case 160:
					num = 255;
					break;
				case 161:
					num = 173;
					break;
				case 162:
					num = 189;
					break;
				case 163:
					num = 156;
					break;
				case 164:
					num = 207;
					break;
				case 165:
					num = 190;
					break;
				case 166:
					num = 221;
					break;
				case 167:
					num = 245;
					break;
				case 168:
					num = 249;
					break;
				case 169:
					num = 184;
					break;
				case 170:
					num = 166;
					break;
				case 171:
					num = 174;
					break;
				case 172:
					num = 170;
					break;
				case 173:
					num = 240;
					break;
				case 174:
					num = 169;
					break;
				case 175:
					num = 238;
					break;
				case 176:
					num = 248;
					break;
				case 177:
					num = 241;
					break;
				case 178:
					num = 253;
					break;
				case 179:
					num = 252;
					break;
				case 180:
					num = 239;
					break;
				case 181:
					num = 230;
					break;
				case 182:
					num = 244;
					break;
				case 183:
					num = 250;
					break;
				case 184:
					num = 247;
					break;
				case 185:
					num = 251;
					break;
				case 186:
					num = 167;
					break;
				case 187:
					num = 175;
					break;
				case 188:
					num = 172;
					break;
				case 189:
					num = 171;
					break;
				case 190:
					num = 243;
					break;
				case 191:
					num = 168;
					break;
				case 192:
					num = 183;
					break;
				case 193:
					num = 181;
					break;
				case 194:
					num = 182;
					break;
				case 195:
					num = 199;
					break;
				case 196:
					num = 142;
					break;
				case 197:
					num = 143;
					break;
				case 198:
					num = 146;
					break;
				case 199:
					num = 128;
					break;
				case 200:
					num = 212;
					break;
				case 201:
					num = 144;
					break;
				case 202:
					num = 210;
					break;
				case 203:
					num = 211;
					break;
				case 204:
					num = 222;
					break;
				case 205:
					num = 214;
					break;
				case 206:
					num = 215;
					break;
				case 207:
					num = 216;
					break;
				case 208:
					num = 209;
					break;
				case 209:
					num = 165;
					break;
				case 210:
					num = 227;
					break;
				case 211:
					num = 224;
					break;
				case 212:
					num = 226;
					break;
				case 213:
					num = 229;
					break;
				case 214:
					num = 153;
					break;
				case 215:
					num = 158;
					break;
				case 216:
					num = 157;
					break;
				case 217:
					num = 235;
					break;
				case 218:
					num = 233;
					break;
				case 219:
					num = 234;
					break;
				case 220:
					num = 154;
					break;
				case 221:
					num = 237;
					break;
				case 222:
					num = 232;
					break;
				case 223:
					num = 225;
					break;
				case 224:
					num = 133;
					break;
				case 225:
					num = 160;
					break;
				case 226:
					num = 131;
					break;
				case 227:
					num = 198;
					break;
				case 228:
					num = 132;
					break;
				case 229:
					num = 134;
					break;
				case 230:
					num = 145;
					break;
				case 231:
					num = 135;
					break;
				case 232:
					num = 138;
					break;
				case 233:
					num = 130;
					break;
				case 234:
					num = 136;
					break;
				case 235:
					num = 137;
					break;
				case 236:
					num = 141;
					break;
				case 237:
					num = 161;
					break;
				case 238:
					num = 140;
					break;
				case 239:
					num = 139;
					break;
				case 240:
					num = 208;
					break;
				case 241:
					num = 164;
					break;
				case 242:
					num = 149;
					break;
				case 243:
					num = 162;
					break;
				case 244:
					num = 147;
					break;
				case 245:
					num = 228;
					break;
				case 246:
					num = 148;
					break;
				case 247:
					num = 246;
					break;
				case 248:
					num = 155;
					break;
				case 249:
					num = 151;
					break;
				case 250:
					num = 163;
					break;
				case 251:
					num = 150;
					break;
				case 252:
					num = 129;
					break;
				case 253:
					num = 236;
					break;
				case 254:
					num = 231;
					break;
				case 255:
					num = 152;
					break;
				case 272:
					num = 209;
					break;
				case 305:
					num = 213;
					break;
				case 402:
					num = 159;
					break;
				case 8215:
					num = 242;
					break;
				case 8226:
					num = 7;
					break;
				case 8252:
					num = 19;
					break;
				case 8254:
					num = 238;
					break;
				case 8592:
					num = 27;
					break;
				case 8593:
					num = 24;
					break;
				case 8594:
					num = 26;
					break;
				case 8595:
					num = 25;
					break;
				case 8596:
					num = 29;
					break;
				case 8597:
					num = 18;
					break;
				case 8616:
					num = 23;
					break;
				case 8735:
					num = 28;
					break;
				case 8962:
					num = 127;
					break;
				case 9472:
					num = 196;
					break;
				case 9474:
					num = 179;
					break;
				case 9484:
					num = 218;
					break;
				case 9488:
					num = 191;
					break;
				case 9492:
					num = 192;
					break;
				case 9496:
					num = 217;
					break;
				case 9500:
					num = 195;
					break;
				case 9508:
					num = 180;
					break;
				case 9516:
					num = 194;
					break;
				case 9524:
					num = 193;
					break;
				case 9532:
					num = 197;
					break;
				case 9552:
					num = 205;
					break;
				case 9553:
					num = 186;
					break;
				case 9556:
					num = 201;
					break;
				case 9559:
					num = 187;
					break;
				case 9562:
					num = 200;
					break;
				case 9565:
					num = 188;
					break;
				case 9568:
					num = 204;
					break;
				case 9571:
					num = 185;
					break;
				case 9574:
					num = 203;
					break;
				case 9577:
					num = 202;
					break;
				case 9580:
					num = 206;
					break;
				case 9600:
					num = 223;
					break;
				case 9604:
					num = 220;
					break;
				case 9608:
					num = 219;
					break;
				case 9617:
					num = 176;
					break;
				case 9618:
					num = 177;
					break;
				case 9619:
					num = 178;
					break;
				case 9632:
					num = 254;
					break;
				case 9644:
					num = 22;
					break;
				case 9650:
					num = 30;
					break;
				case 9658:
					num = 16;
					break;
				case 9660:
					num = 31;
					break;
				case 9668:
					num = 17;
					break;
				case 9675:
					num = 9;
					break;
				case 9688:
					num = 8;
					break;
				case 9689:
					num = 10;
					break;
				case 9786:
					num = 1;
					break;
				case 9787:
					num = 2;
					break;
				case 9788:
					num = 15;
					break;
				case 9792:
					num = 12;
					break;
				case 9794:
					num = 11;
					break;
				case 9824:
					num = 6;
					break;
				case 9827:
					num = 5;
					break;
				case 9829:
					num = 3;
					break;
				case 9830:
					num = 4;
					break;
				case 9834:
					num = 13;
					break;
				case 9836:
					num = 14;
					break;
				case 65512:
					num = 179;
					break;
				case 65513:
					num = 27;
					break;
				case 65514:
					num = 24;
					break;
				case 65515:
					num = 26;
					break;
				case 65516:
					num = 25;
					break;
				case 65517:
					num = 254;
					break;
				case 65518:
					num = 9;
					break;
				default:
					if (num >= 65281 && num <= 65374)
					{
						num -= 65248;
					}
					else
					{
						HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					}
					break;
				case 27:
				case 29:
				case 30:
				case 31:
				case 32:
				case 33:
				case 34:
				case 35:
				case 36:
				case 37:
				case 38:
				case 39:
				case 40:
				case 41:
				case 42:
				case 43:
				case 44:
				case 45:
				case 46:
				case 47:
				case 48:
				case 49:
				case 50:
				case 51:
				case 52:
				case 53:
				case 54:
				case 55:
				case 56:
				case 57:
				case 58:
				case 59:
				case 60:
				case 61:
				case 62:
				case 63:
				case 64:
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
				case 91:
				case 92:
				case 93:
				case 94:
				case 95:
				case 96:
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
				case 123:
				case 124:
				case 125:
				case 126:
					break;
				}
			}
			bytes[byteIndex++] = (byte)num;
			charCount--;
			byteCount--;
		}
	}
}
[Serializable]
public class ENCibm850 : CP850
{
}
[Serializable]
public class CP860 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006', '\a', '\b', '\t',
		'\n', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019', '\u001c', '\u001b', '\u007f', '\u001d',
		'\u001e', '\u001f', ' ', '!', '"', '#', '$', '%', '&', '\'',
		'(', ')', '*', '+', ',', '-', '.', '/', '0', '1',
		'2', '3', '4', '5', '6', '7', '8', '9', ':', ';',
		'<', '=', '>', '?', '@', 'A', 'B', 'C', 'D', 'E',
		'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
		'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y',
		'Z', '[', '\\', ']', '^', '_', '`', 'a', 'b', 'c',
		'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
		'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w',
		'x', 'y', 'z', '{', '|', '}', '~', '\u001a', 'Ç', 'ü',
		'é', 'â', 'ã', 'à', 'Á', 'ç', 'ê', 'Ê', 'è', 'Í',
		'Ô', 'ì', 'Ã', 'Â', 'É', 'À', 'È', 'ô', 'õ', 'ò',
		'Ú', 'ù', 'Ì', 'Õ', 'Ü', '¢', '£', 'Ù', '₧', 'Ó',
		'á', 'í', 'ó', 'ú', 'ñ', 'Ñ', 'ª', 'º', '¿', 'Ò',
		'¬', '½', '¼', '¡', '«', '»', '░', '▒', '▓', '│',
		'┤', '╡', '╢', '╖', '╕', '╣', '║', '╗', '╝', '╜',
		'╛', '┐', '└', '┴', '┬', '├', '─', '┼', '╞', '╟',
		'╚', '╔', '╩', '╦', '╠', '═', '╬', '╧', '╨', '╤',
		'╥', '╙', '╘', '╒', '╓', '╫', '╪', '┘', '┌', '█',
		'▄', '▌', '▐', '▀', 'α', 'ß', 'Γ', 'π', 'Σ', 'σ',
		'μ', 'τ', 'Φ', 'Θ', 'Ω', 'δ', '∞', 'φ', 'ε', '∩',
		'≡', '±', '≥', '≤', '⌠', '⌡', '÷', '≈', '°', '∙',
		'·', '√', 'ⁿ', '²', '■', '\u00a0'
	};

	public CP860()
		: base(860, ToChars, "Portuguese (DOS)", "ibm860", "ibm860", "ibm860", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
	{
	}

	protected unsafe override void ToBytes(char* chars, int charCount, byte* bytes, int byteCount)
	{
		int charIndex = 0;
		int byteIndex = 0;
		EncoderFallbackBuffer buffer = null;
		while (charCount > 0)
		{
			int num = *(ushort*)((byte*)chars + charIndex++ * 2);
			if (num >= 26)
			{
				switch (num)
				{
				case 26:
					num = 127;
					break;
				case 28:
					num = 26;
					break;
				case 127:
					num = 28;
					break;
				case 160:
					num = 255;
					break;
				case 161:
					num = 173;
					break;
				case 162:
					num = 155;
					break;
				case 163:
					num = 156;
					break;
				case 167:
					num = 21;
					break;
				case 170:
					num = 166;
					break;
				case 171:
					num = 174;
					break;
				case 172:
					num = 170;
					break;
				case 176:
					num = 248;
					break;
				case 177:
					num = 241;
					break;
				case 178:
					num = 253;
					break;
				case 182:
					num = 20;
					break;
				case 183:
					num = 250;
					break;
				case 186:
					num = 167;
					break;
				case 187:
					num = 175;
					break;
				case 188:
					num = 172;
					break;
				case 189:
					num = 171;
					break;
				case 191:
					num = 168;
					break;
				case 192:
					num = 145;
					break;
				case 193:
					num = 134;
					break;
				case 194:
					num = 143;
					break;
				case 195:
					num = 142;
					break;
				case 199:
					num = 128;
					break;
				case 200:
					num = 146;
					break;
				case 201:
					num = 144;
					break;
				case 202:
					num = 137;
					break;
				case 204:
					num = 152;
					break;
				case 205:
					num = 139;
					break;
				case 209:
					num = 165;
					break;
				case 210:
					num = 169;
					break;
				case 211:
					num = 159;
					break;
				case 212:
					num = 140;
					break;
				case 213:
					num = 153;
					break;
				case 217:
					num = 157;
					break;
				case 218:
					num = 150;
					break;
				case 220:
					num = 154;
					break;
				case 223:
					num = 225;
					break;
				case 224:
					num = 133;
					break;
				case 225:
					num = 160;
					break;
				case 226:
					num = 131;
					break;
				case 227:
					num = 132;
					break;
				case 231:
					num = 135;
					break;
				case 232:
					num = 138;
					break;
				case 233:
					num = 130;
					break;
				case 234:
					num = 136;
					break;
				case 236:
					num = 141;
					break;
				case 237:
					num = 161;
					break;
				case 241:
					num = 164;
					break;
				case 242:
					num = 149;
					break;
				case 243:
					num = 162;
					break;
				case 244:
					num = 147;
					break;
				case 245:
					num = 148;
					break;
				case 247:
					num = 246;
					break;
				case 249:
					num = 151;
					break;
				case 250:
					num = 163;
					break;
				case 252:
					num = 129;
					break;
				case 915:
					num = 226;
					break;
				case 920:
					num = 233;
					break;
				case 931:
					num = 228;
					break;
				case 934:
					num = 232;
					break;
				case 937:
					num = 234;
					break;
				case 945:
					num = 224;
					break;
				case 948:
					num = 235;
					break;
				case 949:
					num = 238;
					break;
				case 956:
					num = 230;
					break;
				case 960:
					num = 227;
					break;
				case 963:
					num = 229;
					break;
				case 964:
					num = 231;
					break;
				case 966:
					num = 237;
					break;
				case 8226:
					num = 7;
					break;
				case 8252:
					num = 19;
					break;
				case 8319:
					num = 252;
					break;
				case 8359:
					num = 158;
					break;
				case 8592:
					num = 27;
					break;
				case 8593:
					num = 24;
					break;
				case 8594:
					num = 26;
					break;
				case 8595:
					num = 25;
					break;
				case 8596:
					num = 29;
					break;
				case 8597:
					num = 18;
					break;
				case 8616:
					num = 23;
					break;
				case 8729:
					num = 249;
					break;
				case 8730:
					num = 251;
					break;
				case 8734:
					num = 236;
					break;
				case 8735:
					num = 28;
					break;
				case 8745:
					num = 239;
					break;
				case 8776:
					num = 247;
					break;
				case 8801:
					num = 240;
					break;
				case 8804:
					num = 243;
					break;
				case 8805:
					num = 242;
					break;
				case 8962:
					num = 127;
					break;
				case 8992:
					num = 244;
					break;
				case 8993:
					num = 245;
					break;
				case 9472:
					num = 196;
					break;
				case 9474:
					num = 179;
					break;
				case 9484:
					num = 218;
					break;
				case 9488:
					num = 191;
					break;
				case 9492:
					num = 192;
					break;
				case 9496:
					num = 217;
					break;
				case 9500:
					num = 195;
					break;
				case 9508:
					num = 180;
					break;
				case 9516:
					num = 194;
					break;
				case 9524:
					num = 193;
					break;
				case 9532:
					num = 197;
					break;
				case 9552:
					num = 205;
					break;
				case 9553:
					num = 186;
					break;
				case 9554:
					num = 213;
					break;
				case 9555:
					num = 214;
					break;
				case 9556:
					num = 201;
					break;
				case 9557:
					num = 184;
					break;
				case 9558:
					num = 183;
					break;
				case 9559:
					num = 187;
					break;
				case 9560:
					num = 212;
					break;
				case 9561:
					num = 211;
					break;
				case 9562:
					num = 200;
					break;
				case 9563:
					num = 190;
					break;
				case 9564:
					num = 189;
					break;
				case 9565:
					num = 188;
					break;
				case 9566:
					num = 198;
					break;
				case 9567:
					num = 199;
					break;
				case 9568:
					num = 204;
					break;
				case 9569:
					num = 181;
					break;
				case 9570:
					num = 182;
					break;
				case 9571:
					num = 185;
					break;
				case 9572:
					num = 209;
					break;
				case 9573:
					num = 210;
					break;
				case 9574:
					num = 203;
					break;
				case 9575:
					num = 207;
					break;
				case 9576:
					num = 208;
					break;
				case 9577:
					num = 202;
					break;
				case 9578:
					num = 216;
					break;
				case 9579:
					num = 215;
					break;
				case 9580:
					num = 206;
					break;
				case 9600:
					num = 223;
					break;
				case 9604:
					num = 220;
					break;
				case 9608:
					num = 219;
					break;
				case 9612:
					num = 221;
					break;
				case 9616:
					num = 222;
					break;
				case 9617:
					num = 176;
					break;
				case 9618:
					num = 177;
					break;
				case 9619:
					num = 178;
					break;
				case 9632:
					num = 254;
					break;
				case 9644:
					num = 22;
					break;
				case 9650:
					num = 30;
					break;
				case 9658:
					num = 16;
					break;
				case 9660:
					num = 31;
					break;
				case 9668:
					num = 17;
					break;
				case 9675:
					num = 9;
					break;
				case 9688:
					num = 8;
					break;
				case 9689:
					num = 10;
					break;
				case 9786:
					num = 1;
					break;
				case 9787:
					num = 2;
					break;
				case 9788:
					num = 15;
					break;
				case 9792:
					num = 12;
					break;
				case 9794:
					num = 11;
					break;
				case 9824:
					num = 6;
					break;
				case 9827:
					num = 5;
					break;
				case 9829:
					num = 3;
					break;
				case 9830:
					num = 4;
					break;
				case 9834:
					num = 13;
					break;
				case 9835:
					num = 14;
					break;
				case 65512:
					num = 179;
					break;
				case 65513:
					num = 27;
					break;
				case 65514:
					num = 24;
					break;
				case 65515:
					num = 26;
					break;
				case 65516:
					num = 25;
					break;
				case 65517:
					num = 254;
					break;
				case 65518:
					num = 9;
					break;
				default:
					if (num >= 65281 && num <= 65374)
					{
						num -= 65248;
					}
					else
					{
						HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					}
					break;
				case 27:
				case 29:
				case 30:
				case 31:
				case 32:
				case 33:
				case 34:
				case 35:
				case 36:
				case 37:
				case 38:
				case 39:
				case 40:
				case 41:
				case 42:
				case 43:
				case 44:
				case 45:
				case 46:
				case 47:
				case 48:
				case 49:
				case 50:
				case 51:
				case 52:
				case 53:
				case 54:
				case 55:
				case 56:
				case 57:
				case 58:
				case 59:
				case 60:
				case 61:
				case 62:
				case 63:
				case 64:
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
				case 91:
				case 92:
				case 93:
				case 94:
				case 95:
				case 96:
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
				case 123:
				case 124:
				case 125:
				case 126:
					break;
				}
			}
			bytes[byteIndex++] = (byte)num;
			charCount--;
			byteCount--;
		}
	}
}
[Serializable]
public class ENCibm860 : CP860
{
}
[Serializable]
public class CP861 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006', '\a', '\b', '\t',
		'\n', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019', '\u001c', '\u001b', '\u007f', '\u001d',
		'\u001e', '\u001f', ' ', '!', '"', '#', '$', '%', '&', '\'',
		'(', ')', '*', '+', ',', '-', '.', '/', '0', '1',
		'2', '3', '4', '5', '6', '7', '8', '9', ':', ';',
		'<', '=', '>', '?', '@', 'A', 'B', 'C', 'D', 'E',
		'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
		'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y',
		'Z', '[', '\\', ']', '^', '_', '`', 'a', 'b', 'c',
		'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
		'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w',
		'x', 'y', 'z', '{', '|', '}', '~', '\u001a', 'Ç', 'ü',
		'é', 'â', 'ä', 'à', 'å', 'ç', 'ê', 'ë', 'è', 'Ð',
		'ð', 'Þ', 'Ä', 'Å', 'É', 'æ', 'Æ', 'ô', 'ö', 'þ',
		'û', 'Ý', 'ý', 'Ö', 'Ü', 'ø', '£', 'Ø', '₧', 'ƒ',
		'á', 'í', 'ó', 'ú', 'Á', 'Í', 'Ó', 'Ú', '¿', '⌐',
		'¬', '½', '¼', '¡', '«', '»', '░', '▒', '▓', '│',
		'┤', '╡', '╢', '╖', '╕', '╣', '║', '╗', '╝', '╜',
		'╛', '┐', '└', '┴', '┬', '├', '─', '┼', '╞', '╟',
		'╚', '╔', '╩', '╦', '╠', '═', '╬', '╧', '╨', '╤',
		'╥', '╙', '╘', '╒', '╓', '╫', '╪', '┘', '┌', '█',
		'▄', '▌', '▐', '▀', 'α', 'ß', 'Γ', 'π', 'Σ', 'σ',
		'μ', 'τ', 'Φ', 'Θ', 'Ω', 'δ', '∞', 'φ', 'ε', '∩',
		'≡', '±', '≥', '≤', '⌠', '⌡', '÷', '≈', '°', '∙',
		'·', '√', 'ⁿ', '²', '■', '\u00a0'
	};

	public CP861()
		: base(861, ToChars, "Icelandic (DOS)", "ibm861", "ibm861", "ibm861", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
	{
	}

	protected unsafe override void ToBytes(char* chars, int charCount, byte* bytes, int byteCount)
	{
		int charIndex = 0;
		int byteIndex = 0;
		EncoderFallbackBuffer buffer = null;
		while (charCount > 0)
		{
			int num = *(ushort*)((byte*)chars + charIndex++ * 2);
			if (num >= 26)
			{
				switch (num)
				{
				case 26:
					num = 127;
					break;
				case 28:
					num = 26;
					break;
				case 127:
					num = 28;
					break;
				case 160:
					num = 255;
					break;
				case 161:
					num = 173;
					break;
				case 163:
					num = 156;
					break;
				case 167:
					num = 21;
					break;
				case 171:
					num = 174;
					break;
				case 172:
					num = 170;
					break;
				case 176:
					num = 248;
					break;
				case 177:
					num = 241;
					break;
				case 178:
					num = 253;
					break;
				case 182:
					num = 20;
					break;
				case 183:
					num = 250;
					break;
				case 187:
					num = 175;
					break;
				case 188:
					num = 172;
					break;
				case 189:
					num = 171;
					break;
				case 191:
					num = 168;
					break;
				case 193:
					num = 164;
					break;
				case 196:
					num = 142;
					break;
				case 197:
					num = 143;
					break;
				case 198:
					num = 146;
					break;
				case 199:
					num = 128;
					break;
				case 201:
					num = 144;
					break;
				case 205:
					num = 165;
					break;
				case 208:
					num = 139;
					break;
				case 211:
					num = 166;
					break;
				case 214:
					num = 153;
					break;
				case 216:
					num = 157;
					break;
				case 218:
					num = 167;
					break;
				case 220:
					num = 154;
					break;
				case 221:
					num = 151;
					break;
				case 222:
					num = 141;
					break;
				case 223:
					num = 225;
					break;
				case 224:
					num = 133;
					break;
				case 225:
					num = 160;
					break;
				case 226:
					num = 131;
					break;
				case 228:
					num = 132;
					break;
				case 229:
					num = 134;
					break;
				case 230:
					num = 145;
					break;
				case 231:
					num = 135;
					break;
				case 232:
					num = 138;
					break;
				case 233:
					num = 130;
					break;
				case 234:
					num = 136;
					break;
				case 235:
					num = 137;
					break;
				case 237:
					num = 161;
					break;
				case 240:
					num = 140;
					break;
				case 243:
					num = 162;
					break;
				case 244:
					num = 147;
					break;
				case 246:
					num = 148;
					break;
				case 247:
					num = 246;
					break;
				case 248:
					num = 155;
					break;
				case 250:
					num = 163;
					break;
				case 251:
					num = 150;
					break;
				case 252:
					num = 129;
					break;
				case 253:
					num = 152;
					break;
				case 254:
					num = 149;
					break;
				case 402:
					num = 159;
					break;
				case 915:
					num = 226;
					break;
				case 920:
					num = 233;
					break;
				case 931:
					num = 228;
					break;
				case 934:
					num = 232;
					break;
				case 937:
					num = 234;
					break;
				case 945:
					num = 224;
					break;
				case 948:
					num = 235;
					break;
				case 949:
					num = 238;
					break;
				case 956:
					num = 230;
					break;
				case 960:
					num = 227;
					break;
				case 963:
					num = 229;
					break;
				case 964:
					num = 231;
					break;
				case 966:
					num = 237;
					break;
				case 8226:
					num = 7;
					break;
				case 8252:
					num = 19;
					break;
				case 8319:
					num = 252;
					break;
				case 8359:
					num = 158;
					break;
				case 8592:
					num = 27;
					break;
				case 8593:
					num = 24;
					break;
				case 8594:
					num = 26;
					break;
				case 8595:
					num = 25;
					break;
				case 8596:
					num = 29;
					break;
				case 8597:
					num = 18;
					break;
				case 8616:
					num = 23;
					break;
				case 8729:
					num = 249;
					break;
				case 8730:
					num = 251;
					break;
				case 8734:
					num = 236;
					break;
				case 8735:
					num = 28;
					break;
				case 8745:
					num = 239;
					break;
				case 8776:
					num = 247;
					break;
				case 8801:
					num = 240;
					break;
				case 8804:
					num = 243;
					break;
				case 8805:
					num = 242;
					break;
				case 8962:
					num = 127;
					break;
				case 8976:
					num = 169;
					break;
				case 8992:
					num = 244;
					break;
				case 8993:
					num = 245;
					break;
				case 9472:
					num = 196;
					break;
				case 9474:
					num = 179;
					break;
				case 9484:
					num = 218;
					break;
				case 9488:
					num = 191;
					break;
				case 9492:
					num = 192;
					break;
				case 9496:
					num = 217;
					break;
				case 9500:
					num = 195;
					break;
				case 9508:
					num = 180;
					break;
				case 9516:
					num = 194;
					break;
				case 9524:
					num = 193;
					break;
				case 9532:
					num = 197;
					break;
				case 9552:
					num = 205;
					break;
				case 9553:
					num = 186;
					break;
				case 9554:
					num = 213;
					break;
				case 9555:
					num = 214;
					break;
				case 9556:
					num = 201;
					break;
				case 9557:
					num = 184;
					break;
				case 9558:
					num = 183;
					break;
				case 9559:
					num = 187;
					break;
				case 9560:
					num = 212;
					break;
				case 9561:
					num = 211;
					break;
				case 9562:
					num = 200;
					break;
				case 9563:
					num = 190;
					break;
				case 9564:
					num = 189;
					break;
				case 9565:
					num = 188;
					break;
				case 9566:
					num = 198;
					break;
				case 9567:
					num = 199;
					break;
				case 9568:
					num = 204;
					break;
				case 9569:
					num = 181;
					break;
				case 9570:
					num = 182;
					break;
				case 9571:
					num = 185;
					break;
				case 9572:
					num = 209;
					break;
				case 9573:
					num = 210;
					break;
				case 9574:
					num = 203;
					break;
				case 9575:
					num = 207;
					break;
				case 9576:
					num = 208;
					break;
				case 9577:
					num = 202;
					break;
				case 9578:
					num = 216;
					break;
				case 9579:
					num = 215;
					break;
				case 9580:
					num = 206;
					break;
				case 9600:
					num = 223;
					break;
				case 9604:
					num = 220;
					break;
				case 9608:
					num = 219;
					break;
				case 9612:
					num = 221;
					break;
				case 9616:
					num = 222;
					break;
				case 9617:
					num = 176;
					break;
				case 9618:
					num = 177;
					break;
				case 9619:
					num = 178;
					break;
				case 9632:
					num = 254;
					break;
				case 9644:
					num = 22;
					break;
				case 9650:
					num = 30;
					break;
				case 9658:
					num = 16;
					break;
				case 9660:
					num = 31;
					break;
				case 9668:
					num = 17;
					break;
				case 9675:
					num = 9;
					break;
				case 9688:
					num = 8;
					break;
				case 9689:
					num = 10;
					break;
				case 9786:
					num = 1;
					break;
				case 9787:
					num = 2;
					break;
				case 9788:
					num = 15;
					break;
				case 9792:
					num = 12;
					break;
				case 9794:
					num = 11;
					break;
				case 9824:
					num = 6;
					break;
				case 9827:
					num = 5;
					break;
				case 9829:
					num = 3;
					break;
				case 9830:
					num = 4;
					break;
				case 9834:
					num = 13;
					break;
				case 9835:
					num = 14;
					break;
				case 65512:
					num = 179;
					break;
				case 65513:
					num = 27;
					break;
				case 65514:
					num = 24;
					break;
				case 65515:
					num = 26;
					break;
				case 65516:
					num = 25;
					break;
				case 65517:
					num = 254;
					break;
				case 65518:
					num = 9;
					break;
				default:
					if (num >= 65281 && num <= 65374)
					{
						num -= 65248;
					}
					else
					{
						HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					}
					break;
				case 27:
				case 29:
				case 30:
				case 31:
				case 32:
				case 33:
				case 34:
				case 35:
				case 36:
				case 37:
				case 38:
				case 39:
				case 40:
				case 41:
				case 42:
				case 43:
				case 44:
				case 45:
				case 46:
				case 47:
				case 48:
				case 49:
				case 50:
				case 51:
				case 52:
				case 53:
				case 54:
				case 55:
				case 56:
				case 57:
				case 58:
				case 59:
				case 60:
				case 61:
				case 62:
				case 63:
				case 64:
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
				case 91:
				case 92:
				case 93:
				case 94:
				case 95:
				case 96:
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
				case 123:
				case 124:
				case 125:
				case 126:
					break;
				}
			}
			bytes[byteIndex++] = (byte)num;
			charCount--;
			byteCount--;
		}
	}
}
[Serializable]
public class ENCibm861 : CP861
{
}
[Serializable]
public class CP863 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006', '\a', '\b', '\t',
		'\n', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019', '\u001c', '\u001b', '\u007f', '\u001d',
		'\u001e', '\u001f', ' ', '!', '"', '#', '$', '%', '&', '\'',
		'(', ')', '*', '+', ',', '-', '.', '/', '0', '1',
		'2', '3', '4', '5', '6', '7', '8', '9', ':', ';',
		'<', '=', '>', '?', '@', 'A', 'B', 'C', 'D', 'E',
		'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
		'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y',
		'Z', '[', '\\', ']', '^', '_', '`', 'a', 'b', 'c',
		'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
		'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w',
		'x', 'y', 'z', '{', '|', '}', '~', '\u001a', 'Ç', 'ü',
		'é', 'â', 'Â', 'à', '¶', 'ç', 'ê', 'ë', 'è', 'ï',
		'î', '‗', 'À', '§', 'É', 'È', 'Ê', 'ô', 'Ë', 'Ï',
		'û', 'ù', '¤', 'Ô', 'Ü', '¢', '£', 'Ù', 'Û', 'ƒ',
		'¦', '\u00b4', 'ó', 'ú', '\u00a8', '\u00b8', '³', '\u00af', 'Î', '⌐',
		'¬', '½', '¼', '¾', '«', '»', '░', '▒', '▓', '│',
		'┤', '╡', '╢', '╖', '╕', '╣', '║', '╗', '╝', '╜',
		'╛', '┐', '└', '┴', '┬', '├', '─', '┼', '╞', '╟',
		'╚', '╔', '╩', '╦', '╠', '═', '╬', '╧', '╨', '╤',
		'╥', '╙', '╘', '╒', '╓', '╫', '╪', '┘', '┌', '█',
		'▄', '▌', '▐', '▀', 'α', 'ß', 'Γ', 'π', 'Σ', 'σ',
		'μ', 'τ', 'Φ', 'Θ', 'Ω', 'δ', '∞', 'φ', 'ε', '∩',
		'≡', '±', '≥', '≤', '⌠', '⌡', '÷', '≈', '°', '∙',
		'·', '√', 'ⁿ', '²', '■', '\u00a0'
	};

	public CP863()
		: base(863, ToChars, "French Canadian (DOS)", "IBM863", "IBM863", "IBM863", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
	{
	}

	protected unsafe override void ToBytes(char* chars, int charCount, byte* bytes, int byteCount)
	{
		int charIndex = 0;
		int byteIndex = 0;
		EncoderFallbackBuffer buffer = null;
		while (charCount > 0)
		{
			int num = *(ushort*)((byte*)chars + charIndex++ * 2);
			if (num >= 26)
			{
				switch (num)
				{
				case 26:
					num = 127;
					break;
				case 28:
					num = 26;
					break;
				case 127:
					num = 28;
					break;
				case 160:
					num = 255;
					break;
				case 162:
					num = 155;
					break;
				case 163:
					num = 156;
					break;
				case 164:
					num = 152;
					break;
				case 166:
					num = 160;
					break;
				case 167:
					num = 143;
					break;
				case 168:
					num = 164;
					break;
				case 171:
					num = 174;
					break;
				case 172:
					num = 170;
					break;
				case 175:
					num = 167;
					break;
				case 176:
					num = 248;
					break;
				case 177:
					num = 241;
					break;
				case 178:
					num = 253;
					break;
				case 179:
					num = 166;
					break;
				case 180:
					num = 161;
					break;
				case 182:
					num = 134;
					break;
				case 183:
					num = 250;
					break;
				case 184:
					num = 165;
					break;
				case 187:
					num = 175;
					break;
				case 188:
					num = 172;
					break;
				case 189:
					num = 171;
					break;
				case 190:
					num = 173;
					break;
				case 192:
					num = 142;
					break;
				case 194:
					num = 132;
					break;
				case 199:
					num = 128;
					break;
				case 200:
					num = 145;
					break;
				case 201:
					num = 144;
					break;
				case 202:
					num = 146;
					break;
				case 203:
					num = 148;
					break;
				case 206:
					num = 168;
					break;
				case 207:
					num = 149;
					break;
				case 212:
					num = 153;
					break;
				case 217:
					num = 157;
					break;
				case 219:
					num = 158;
					break;
				case 220:
					num = 154;
					break;
				case 223:
					num = 225;
					break;
				case 224:
					num = 133;
					break;
				case 226:
					num = 131;
					break;
				case 231:
					num = 135;
					break;
				case 232:
					num = 138;
					break;
				case 233:
					num = 130;
					break;
				case 234:
					num = 136;
					break;
				case 235:
					num = 137;
					break;
				case 238:
					num = 140;
					break;
				case 239:
					num = 139;
					break;
				case 243:
					num = 162;
					break;
				case 244:
					num = 147;
					break;
				case 247:
					num = 246;
					break;
				case 249:
					num = 151;
					break;
				case 250:
					num = 163;
					break;
				case 251:
					num = 150;
					break;
				case 252:
					num = 129;
					break;
				case 402:
					num = 159;
					break;
				case 915:
					num = 226;
					break;
				case 920:
					num = 233;
					break;
				case 931:
					num = 228;
					break;
				case 934:
					num = 232;
					break;
				case 937:
					num = 234;
					break;
				case 945:
					num = 224;
					break;
				case 948:
					num = 235;
					break;
				case 949:
					num = 238;
					break;
				case 956:
					num = 230;
					break;
				case 960:
					num = 227;
					break;
				case 963:
					num = 229;
					break;
				case 964:
					num = 231;
					break;
				case 966:
					num = 237;
					break;
				case 8215:
					num = 141;
					break;
				case 8226:
					num = 7;
					break;
				case 8252:
					num = 19;
					break;
				case 8254:
					num = 167;
					break;
				case 8319:
					num = 252;
					break;
				case 8592:
					num = 27;
					break;
				case 8593:
					num = 24;
					break;
				case 8594:
					num = 26;
					break;
				case 8595:
					num = 25;
					break;
				case 8596:
					num = 29;
					break;
				case 8597:
					num = 18;
					break;
				case 8616:
					num = 23;
					break;
				case 8729:
					num = 249;
					break;
				case 8730:
					num = 251;
					break;
				case 8734:
					num = 236;
					break;
				case 8735:
					num = 28;
					break;
				case 8745:
					num = 239;
					break;
				case 8776:
					num = 247;
					break;
				case 8801:
					num = 240;
					break;
				case 8804:
					num = 243;
					break;
				case 8805:
					num = 242;
					break;
				case 8962:
					num = 127;
					break;
				case 8976:
					num = 169;
					break;
				case 8992:
					num = 244;
					break;
				case 8993:
					num = 245;
					break;
				case 9472:
					num = 196;
					break;
				case 9474:
					num = 179;
					break;
				case 9484:
					num = 218;
					break;
				case 9488:
					num = 191;
					break;
				case 9492:
					num = 192;
					break;
				case 9496:
					num = 217;
					break;
				case 9500:
					num = 195;
					break;
				case 9508:
					num = 180;
					break;
				case 9516:
					num = 194;
					break;
				case 9524:
					num = 193;
					break;
				case 9532:
					num = 197;
					break;
				case 9552:
					num = 205;
					break;
				case 9553:
					num = 186;
					break;
				case 9554:
					num = 213;
					break;
				case 9555:
					num = 214;
					break;
				case 9556:
					num = 201;
					break;
				case 9557:
					num = 184;
					break;
				case 9558:
					num = 183;
					break;
				case 9559:
					num = 187;
					break;
				case 9560:
					num = 212;
					break;
				case 9561:
					num = 211;
					break;
				case 9562:
					num = 200;
					break;
				case 9563:
					num = 190;
					break;
				case 9564:
					num = 189;
					break;
				case 9565:
					num = 188;
					break;
				case 9566:
					num = 198;
					break;
				case 9567:
					num = 199;
					break;
				case 9568:
					num = 204;
					break;
				case 9569:
					num = 181;
					break;
				case 9570:
					num = 182;
					break;
				case 9571:
					num = 185;
					break;
				case 9572:
					num = 209;
					break;
				case 9573:
					num = 210;
					break;
				case 9574:
					num = 203;
					break;
				case 9575:
					num = 207;
					break;
				case 9576:
					num = 208;
					break;
				case 9577:
					num = 202;
					break;
				case 9578:
					num = 216;
					break;
				case 9579:
					num = 215;
					break;
				case 9580:
					num = 206;
					break;
				case 9600:
					num = 223;
					break;
				case 9604:
					num = 220;
					break;
				case 9608:
					num = 219;
					break;
				case 9612:
					num = 221;
					break;
				case 9616:
					num = 222;
					break;
				case 9617:
					num = 176;
					break;
				case 9618:
					num = 177;
					break;
				case 9619:
					num = 178;
					break;
				case 9632:
					num = 254;
					break;
				case 9644:
					num = 22;
					break;
				case 9650:
					num = 30;
					break;
				case 9658:
					num = 16;
					break;
				case 9660:
					num = 31;
					break;
				case 9668:
					num = 17;
					break;
				case 9675:
					num = 9;
					break;
				case 9688:
					num = 8;
					break;
				case 9689:
					num = 10;
					break;
				case 9786:
					num = 1;
					break;
				case 9787:
					num = 2;
					break;
				case 9788:
					num = 15;
					break;
				case 9792:
					num = 12;
					break;
				case 9794:
					num = 11;
					break;
				case 9824:
					num = 6;
					break;
				case 9827:
					num = 5;
					break;
				case 9829:
					num = 3;
					break;
				case 9830:
					num = 4;
					break;
				case 9834:
					num = 13;
					break;
				case 9835:
					num = 14;
					break;
				case 65512:
					num = 179;
					break;
				case 65513:
					num = 27;
					break;
				case 65514:
					num = 24;
					break;
				case 65515:
					num = 26;
					break;
				case 65516:
					num = 25;
					break;
				case 65517:
					num = 254;
					break;
				case 65518:
					num = 9;
					break;
				default:
					if (num >= 65281 && num <= 65374)
					{
						num -= 65248;
					}
					else
					{
						HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					}
					break;
				case 27:
				case 29:
				case 30:
				case 31:
				case 32:
				case 33:
				case 34:
				case 35:
				case 36:
				case 37:
				case 38:
				case 39:
				case 40:
				case 41:
				case 42:
				case 43:
				case 44:
				case 45:
				case 46:
				case 47:
				case 48:
				case 49:
				case 50:
				case 51:
				case 52:
				case 53:
				case 54:
				case 55:
				case 56:
				case 57:
				case 58:
				case 59:
				case 60:
				case 61:
				case 62:
				case 63:
				case 64:
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
				case 91:
				case 92:
				case 93:
				case 94:
				case 95:
				case 96:
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
				case 123:
				case 124:
				case 125:
				case 126:
					break;
				}
			}
			bytes[byteIndex++] = (byte)num;
			charCount--;
			byteCount--;
		}
	}
}
[Serializable]
public class ENCibm863 : CP863
{
}
[Serializable]
public class CP865 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006', '\a', '\b', '\t',
		'\n', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019', '\u001c', '\u001b', '\u007f', '\u001d',
		'\u001e', '\u001f', ' ', '!', '"', '#', '$', '%', '&', '\'',
		'(', ')', '*', '+', ',', '-', '.', '/', '0', '1',
		'2', '3', '4', '5', '6', '7', '8', '9', ':', ';',
		'<', '=', '>', '?', '@', 'A', 'B', 'C', 'D', 'E',
		'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
		'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y',
		'Z', '[', '\\', ']', '^', '_', '`', 'a', 'b', 'c',
		'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
		'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w',
		'x', 'y', 'z', '{', '|', '}', '~', '\u001a', 'Ç', 'ü',
		'é', 'â', 'ä', 'à', 'å', 'ç', 'ê', 'ë', 'è', 'ï',
		'î', 'ì', 'Ä', 'Å', 'É', 'æ', 'Æ', 'ô', 'ö', 'ò',
		'û', 'ù', 'ÿ', 'Ö', 'Ü', 'ø', '£', 'Ø', '₧', 'ƒ',
		'á', 'í', 'ó', 'ú', 'ñ', 'Ñ', 'ª', 'º', '¿', '⌐',
		'¬', '½', '¼', '¡', '«', '¤', '░', '▒', '▓', '│',
		'┤', '╡', '╢', '╖', '╕', '╣', '║', '╗', '╝', '╜',
		'╛', '┐', '└', '┴', '┬', '├', '─', '┼', '╞', '╟',
		'╚', '╔', '╩', '╦', '╠', '═', '╬', '╧', '╨', '╤',
		'╥', '╙', '╘', '╒', '╓', '╫', '╪', '┘', '┌', '█',
		'▄', '▌', '▐', '▀', 'α', 'ß', 'Γ', 'π', 'Σ', 'σ',
		'μ', 'τ', 'Φ', 'Θ', 'Ω', 'δ', '∞', 'φ', 'ε', '∩',
		'≡', '±', '≥', '≤', '⌠', '⌡', '÷', '≈', '°', '∙',
		'·', '√', 'ⁿ', '²', '■', '\u00a0'
	};

	public CP865()
		: base(865, ToChars, "Nordic (DOS)", "IBM863", "IBM865", "IBM865", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
	{
	}

	protected unsafe override void ToBytes(char* chars, int charCount, byte* bytes, int byteCount)
	{
		int charIndex = 0;
		int byteIndex = 0;
		EncoderFallbackBuffer buffer = null;
		while (charCount > 0)
		{
			int num = *(ushort*)((byte*)chars + charIndex++ * 2);
			if (num >= 26)
			{
				switch (num)
				{
				case 26:
					num = 127;
					break;
				case 28:
					num = 26;
					break;
				case 127:
					num = 28;
					break;
				case 160:
					num = 255;
					break;
				case 161:
					num = 173;
					break;
				case 163:
					num = 156;
					break;
				case 164:
					num = 175;
					break;
				case 167:
					num = 21;
					break;
				case 170:
					num = 166;
					break;
				case 171:
					num = 174;
					break;
				case 172:
					num = 170;
					break;
				case 176:
					num = 248;
					break;
				case 177:
					num = 241;
					break;
				case 178:
					num = 253;
					break;
				case 182:
					num = 20;
					break;
				case 183:
					num = 250;
					break;
				case 186:
					num = 167;
					break;
				case 188:
					num = 172;
					break;
				case 189:
					num = 171;
					break;
				case 191:
					num = 168;
					break;
				case 196:
					num = 142;
					break;
				case 197:
					num = 143;
					break;
				case 198:
					num = 146;
					break;
				case 199:
					num = 128;
					break;
				case 201:
					num = 144;
					break;
				case 209:
					num = 165;
					break;
				case 214:
					num = 153;
					break;
				case 216:
					num = 157;
					break;
				case 220:
					num = 154;
					break;
				case 223:
					num = 225;
					break;
				case 224:
					num = 133;
					break;
				case 225:
					num = 160;
					break;
				case 226:
					num = 131;
					break;
				case 228:
					num = 132;
					break;
				case 229:
					num = 134;
					break;
				case 230:
					num = 145;
					break;
				case 231:
					num = 135;
					break;
				case 232:
					num = 138;
					break;
				case 233:
					num = 130;
					break;
				case 234:
					num = 136;
					break;
				case 235:
					num = 137;
					break;
				case 236:
					num = 141;
					break;
				case 237:
					num = 161;
					break;
				case 238:
					num = 140;
					break;
				case 239:
					num = 139;
					break;
				case 241:
					num = 164;
					break;
				case 242:
					num = 149;
					break;
				case 243:
					num = 162;
					break;
				case 244:
					num = 147;
					break;
				case 246:
					num = 148;
					break;
				case 247:
					num = 246;
					break;
				case 248:
					num = 155;
					break;
				case 249:
					num = 151;
					break;
				case 250:
					num = 163;
					break;
				case 251:
					num = 150;
					break;
				case 252:
					num = 129;
					break;
				case 255:
					num = 152;
					break;
				case 402:
					num = 159;
					break;
				case 915:
					num = 226;
					break;
				case 920:
					num = 233;
					break;
				case 931:
					num = 228;
					break;
				case 934:
					num = 232;
					break;
				case 937:
					num = 234;
					break;
				case 945:
					num = 224;
					break;
				case 948:
					num = 235;
					break;
				case 949:
					num = 238;
					break;
				case 956:
					num = 230;
					break;
				case 960:
					num = 227;
					break;
				case 963:
					num = 229;
					break;
				case 964:
					num = 231;
					break;
				case 966:
					num = 237;
					break;
				case 8226:
					num = 7;
					break;
				case 8252:
					num = 19;
					break;
				case 8319:
					num = 252;
					break;
				case 8359:
					num = 158;
					break;
				case 8592:
					num = 27;
					break;
				case 8593:
					num = 24;
					break;
				case 8594:
					num = 26;
					break;
				case 8595:
					num = 25;
					break;
				case 8596:
					num = 29;
					break;
				case 8597:
					num = 18;
					break;
				case 8616:
					num = 23;
					break;
				case 8729:
					num = 249;
					break;
				case 8730:
					num = 251;
					break;
				case 8734:
					num = 236;
					break;
				case 8735:
					num = 28;
					break;
				case 8745:
					num = 239;
					break;
				case 8776:
					num = 247;
					break;
				case 8801:
					num = 240;
					break;
				case 8804:
					num = 243;
					break;
				case 8805:
					num = 242;
					break;
				case 8962:
					num = 127;
					break;
				case 8976:
					num = 169;
					break;
				case 8992:
					num = 244;
					break;
				case 8993:
					num = 245;
					break;
				case 9472:
					num = 196;
					break;
				case 9474:
					num = 179;
					break;
				case 9484:
					num = 218;
					break;
				case 9488:
					num = 191;
					break;
				case 9492:
					num = 192;
					break;
				case 9496:
					num = 217;
					break;
				case 9500:
					num = 195;
					break;
				case 9508:
					num = 180;
					break;
				case 9516:
					num = 194;
					break;
				case 9524:
					num = 193;
					break;
				case 9532:
					num = 197;
					break;
				case 9552:
					num = 205;
					break;
				case 9553:
					num = 186;
					break;
				case 9554:
					num = 213;
					break;
				case 9555:
					num = 214;
					break;
				case 9556:
					num = 201;
					break;
				case 9557:
					num = 184;
					break;
				case 9558:
					num = 183;
					break;
				case 9559:
					num = 187;
					break;
				case 9560:
					num = 212;
					break;
				case 9561:
					num = 211;
					break;
				case 9562:
					num = 200;
					break;
				case 9563:
					num = 190;
					break;
				case 9564:
					num = 189;
					break;
				case 9565:
					num = 188;
					break;
				case 9566:
					num = 198;
					break;
				case 9567:
					num = 199;
					break;
				case 9568:
					num = 204;
					break;
				case 9569:
					num = 181;
					break;
				case 9570:
					num = 182;
					break;
				case 9571:
					num = 185;
					break;
				case 9572:
					num = 209;
					break;
				case 9573:
					num = 210;
					break;
				case 9574:
					num = 203;
					break;
				case 9575:
					num = 207;
					break;
				case 9576:
					num = 208;
					break;
				case 9577:
					num = 202;
					break;
				case 9578:
					num = 216;
					break;
				case 9579:
					num = 215;
					break;
				case 9580:
					num = 206;
					break;
				case 9600:
					num = 223;
					break;
				case 9604:
					num = 220;
					break;
				case 9608:
					num = 219;
					break;
				case 9612:
					num = 221;
					break;
				case 9616:
					num = 222;
					break;
				case 9617:
					num = 176;
					break;
				case 9618:
					num = 177;
					break;
				case 9619:
					num = 178;
					break;
				case 9632:
					num = 254;
					break;
				case 9644:
					num = 22;
					break;
				case 9650:
					num = 30;
					break;
				case 9658:
					num = 16;
					break;
				case 9660:
					num = 31;
					break;
				case 9668:
					num = 17;
					break;
				case 9675:
					num = 9;
					break;
				case 9688:
					num = 8;
					break;
				case 9689:
					num = 10;
					break;
				case 9786:
					num = 1;
					break;
				case 9787:
					num = 2;
					break;
				case 9788:
					num = 15;
					break;
				case 9792:
					num = 12;
					break;
				case 9794:
					num = 11;
					break;
				case 9824:
					num = 6;
					break;
				case 9827:
					num = 5;
					break;
				case 9829:
					num = 3;
					break;
				case 9830:
					num = 4;
					break;
				case 9834:
					num = 13;
					break;
				case 9835:
					num = 14;
					break;
				case 65512:
					num = 179;
					break;
				case 65513:
					num = 27;
					break;
				case 65514:
					num = 24;
					break;
				case 65515:
					num = 26;
					break;
				case 65516:
					num = 25;
					break;
				case 65517:
					num = 254;
					break;
				case 65518:
					num = 9;
					break;
				default:
					if (num >= 65281 && num <= 65374)
					{
						num -= 65248;
					}
					else
					{
						HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					}
					break;
				case 27:
				case 29:
				case 30:
				case 31:
				case 32:
				case 33:
				case 34:
				case 35:
				case 36:
				case 37:
				case 38:
				case 39:
				case 40:
				case 41:
				case 42:
				case 43:
				case 44:
				case 45:
				case 46:
				case 47:
				case 48:
				case 49:
				case 50:
				case 51:
				case 52:
				case 53:
				case 54:
				case 55:
				case 56:
				case 57:
				case 58:
				case 59:
				case 60:
				case 61:
				case 62:
				case 63:
				case 64:
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
				case 91:
				case 92:
				case 93:
				case 94:
				case 95:
				case 96:
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
				case 123:
				case 124:
				case 125:
				case 126:
					break;
				}
			}
			bytes[byteIndex++] = (byte)num;
			charCount--;
			byteCount--;
		}
	}
}
[Serializable]
public class ENCibm865 : CP865
{
}
