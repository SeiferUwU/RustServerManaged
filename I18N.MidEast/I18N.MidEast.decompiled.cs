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
namespace I18N.MidEast;

[Serializable]
public class CP1254 : ByteEncoding
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
		'Œ', '\u008d', '\u008e', '\u008f', '\u0090', '‘', '’', '“', '”', '•',
		'–', '—', '\u02dc', '™', 'š', '›', 'œ', '\u009d', '\u009e', 'Ÿ',
		'\u00a0', '¡', '¢', '£', '¤', '¥', '¦', '§', '\u00a8', '©',
		'ª', '«', '¬', '\u00ad', '®', '\u00af', '°', '±', '²', '³',
		'\u00b4', 'µ', '¶', '·', '\u00b8', '¹', 'º', '»', '¼', '½',
		'¾', '¿', 'À', 'Á', 'Â', 'Ã', 'Ä', 'Å', 'Æ', 'Ç',
		'È', 'É', 'Ê', 'Ë', 'Ì', 'Í', 'Î', 'Ï', 'Ğ', 'Ñ',
		'Ò', 'Ó', 'Ô', 'Õ', 'Ö', '×', 'Ø', 'Ù', 'Ú', 'Û',
		'Ü', 'İ', 'Ş', 'ß', 'à', 'á', 'â', 'ã', 'ä', 'å',
		'æ', 'ç', 'è', 'é', 'ê', 'ë', 'ì', 'í', 'î', 'ï',
		'ğ', 'ñ', 'ò', 'ó', 'ô', 'õ', 'ö', '÷', 'ø', 'ù',
		'ú', 'û', 'ü', 'ı', 'ş', 'ÿ'
	};

	public CP1254()
		: base(1254, ToChars, "Turkish (Windows)", "iso-8859-9", "windows-1254", "windows-1254", isBrowserDisplay: true, isBrowserSave: true, isMailNewsDisplay: true, isMailNewsSave: true, 1254)
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
				case 286:
					num = 208;
					break;
				case 287:
					num = 240;
					break;
				case 304:
					num = 221;
					break;
				case 305:
					num = 253;
					break;
				case 338:
					num = 140;
					break;
				case 339:
					num = 156;
					break;
				case 350:
					num = 222;
					break;
				case 351:
					num = 254;
					break;
				case 352:
					num = 138;
					break;
				case 353:
					num = 154;
					break;
				case 376:
					num = 159;
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
					}
					else
					{
						HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					}
					break;
				case 129:
				case 141:
				case 142:
				case 143:
				case 144:
				case 157:
				case 158:
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
public class ENCwindows_1254 : CP1254
{
}
[Serializable]
public class CP1255 : ByteEncoding
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
		'‚', 'ƒ', '„', '…', '†', '‡', 'ˆ', '‰', '\u008a', '‹',
		'\u008c', '\u008d', '\u008e', '\u008f', '\u0090', '‘', '’', '“', '”', '•',
		'–', '—', '\u02dc', '™', '\u009a', '›', '\u009c', '\u009d', '\u009e', '\u009f',
		'\u00a0', '¡', '¢', '£', '₪', '¥', '¦', '§', '\u00a8', '©',
		'×', '«', '¬', '\u00ad', '®', '\u00af', '°', '±', '²', '³',
		'\u00b4', 'µ', '¶', '·', '\u00b8', '¹', '÷', '»', '¼', '½',
		'¾', '¿', '\u05b0', '\u05b1', '\u05b2', '\u05b3', '\u05b4', '\u05b5', '\u05b6', '\u05b7',
		'\u05b8', '\u05b9', '?', '\u05bb', '\u05bc', '\u05bd', '־', '\u05bf', '׀', '\u05c1',
		'\u05c2', '׃', 'װ', 'ױ', 'ײ', '׳', '״', '?', '?', '?',
		'?', '?', '?', '?', 'א', 'ב', 'ג', 'ד', 'ה', 'ו',
		'ז', 'ח', 'ט', 'י', 'ך', 'כ', 'ל', 'ם', 'מ', 'ן',
		'נ', 'ס', 'ע', 'ף', 'פ', 'ץ', 'צ', 'ק', 'ר', 'ש',
		'ת', '?', '?', '\u200e', '\u200f', '?'
	};

	public CP1255()
		: base(1255, ToChars, "Hebrew (Windows)", "windows-1255", "windows-1255", "windows-1255", isBrowserDisplay: true, isBrowserSave: true, isMailNewsDisplay: true, isMailNewsSave: true, 1255)
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
				case 215:
					num = 170;
					break;
				case 247:
					num = 186;
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
				case 1456:
				case 1457:
				case 1458:
				case 1459:
				case 1460:
				case 1461:
				case 1462:
				case 1463:
				case 1464:
				case 1465:
					num -= 1264;
					break;
				case 1467:
				case 1468:
				case 1469:
				case 1470:
				case 1471:
				case 1472:
				case 1473:
				case 1474:
				case 1475:
					num -= 1264;
					break;
				case 1488:
				case 1489:
				case 1490:
				case 1491:
				case 1492:
				case 1493:
				case 1494:
				case 1495:
				case 1496:
				case 1497:
				case 1498:
				case 1499:
				case 1500:
				case 1501:
				case 1502:
				case 1503:
				case 1504:
				case 1505:
				case 1506:
				case 1507:
				case 1508:
				case 1509:
				case 1510:
				case 1511:
				case 1512:
				case 1513:
				case 1514:
					num -= 1264;
					break;
				case 1520:
				case 1521:
				case 1522:
				case 1523:
				case 1524:
					num -= 1308;
					break;
				case 8206:
					num = 253;
					break;
				case 8207:
					num = 254;
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
				case 8362:
					num = 164;
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
				case 138:
				case 140:
				case 141:
				case 142:
				case 143:
				case 144:
				case 154:
				case 156:
				case 157:
				case 158:
				case 159:
				case 160:
				case 161:
				case 162:
				case 163:
				case 165:
				case 166:
				case 167:
				case 168:
				case 169:
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
				case 187:
				case 188:
				case 189:
				case 190:
				case 191:
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
public class ENCwindows_1255 : CP1255
{
}
[Serializable]
public class CP1256 : ByteEncoding
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
		'x', 'y', 'z', '{', '|', '}', '~', '\u007f', '€', 'پ',
		'‚', 'ƒ', '„', '…', '†', '‡', 'ˆ', '‰', 'ٹ', '‹',
		'Œ', 'چ', 'ژ', 'ڈ', 'گ', '‘', '’', '“', '”', '•',
		'–', '—', 'ک', '™', 'ڑ', '›', 'œ', '\u200c', '\u200d', 'ں',
		'\u00a0', '،', '¢', '£', '¤', '¥', '¦', '§', '\u00a8', '©',
		'ھ', '«', '¬', '\u00ad', '®', '\u00af', '°', '±', '²', '³',
		'\u00b4', 'µ', '¶', '·', '\u00b8', '¹', '؛', '»', '¼', '½',
		'¾', '؟', 'ہ', 'ء', 'آ', 'أ', 'ؤ', 'إ', 'ئ', 'ا',
		'ب', 'ة', 'ت', 'ث', 'ج', 'ح', 'خ', 'د', 'ذ', 'ر',
		'ز', 'س', 'ش', 'ص', 'ض', '×', 'ط', 'ظ', 'ع', 'غ',
		'ـ', 'ف', 'ق', 'ك', 'à', 'ل', 'â', 'م', 'ن', 'ه',
		'و', 'ç', 'è', 'é', 'ê', 'ë', 'ى', 'ي', 'î', 'ï',
		'\u064b', '\u064c', '\u064d', '\u064e', 'ô', '\u064f', '\u0650', '÷', '\u0651', 'ù',
		'\u0652', 'û', 'ü', '\u200e', '\u200f', 'ے'
	};

	public CP1256()
		: base(1256, ToChars, "Arabic (Windows)", "windows-1256", "windows-1256", "windows-1256", isBrowserDisplay: true, isBrowserSave: true, isMailNewsDisplay: true, isMailNewsSave: true, 1256)
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
				case 338:
					num = 140;
					break;
				case 339:
					num = 156;
					break;
				case 402:
					num = 131;
					break;
				case 710:
					num = 136;
					break;
				case 1548:
					num = 161;
					break;
				case 1563:
					num = 186;
					break;
				case 1567:
					num = 191;
					break;
				case 1569:
				case 1570:
				case 1571:
				case 1572:
				case 1573:
				case 1574:
				case 1575:
				case 1576:
				case 1577:
				case 1578:
				case 1579:
				case 1580:
				case 1581:
				case 1582:
				case 1583:
				case 1584:
				case 1585:
				case 1586:
				case 1587:
				case 1588:
				case 1589:
				case 1590:
					num -= 1376;
					break;
				case 1591:
				case 1592:
				case 1593:
				case 1594:
					num -= 1375;
					break;
				case 1600:
				case 1601:
				case 1602:
				case 1603:
					num -= 1380;
					break;
				case 1604:
					num = 225;
					break;
				case 1605:
				case 1606:
				case 1607:
				case 1608:
					num -= 1378;
					break;
				case 1609:
					num = 236;
					break;
				case 1610:
					num = 237;
					break;
				case 1611:
				case 1612:
				case 1613:
				case 1614:
					num -= 1371;
					break;
				case 1615:
					num = 245;
					break;
				case 1616:
					num = 246;
					break;
				case 1617:
					num = 248;
					break;
				case 1618:
					num = 250;
					break;
				case 1632:
				case 1633:
				case 1634:
				case 1635:
				case 1636:
				case 1637:
				case 1638:
				case 1639:
				case 1640:
				case 1641:
					num -= 1584;
					break;
				case 1643:
					num = 44;
					break;
				case 1644:
					num = 46;
					break;
				case 1657:
					num = 138;
					break;
				case 1662:
					num = 129;
					break;
				case 1670:
					num = 141;
					break;
				case 1672:
					num = 143;
					break;
				case 1681:
					num = 154;
					break;
				case 1688:
					num = 142;
					break;
				case 1705:
					num = 152;
					break;
				case 1711:
					num = 144;
					break;
				case 1722:
					num = 159;
					break;
				case 1726:
					num = 170;
					break;
				case 1729:
					num = 192;
					break;
				case 1746:
					num = 255;
					break;
				case 1776:
				case 1777:
				case 1778:
				case 1779:
				case 1780:
				case 1781:
				case 1782:
				case 1783:
				case 1784:
				case 1785:
					num -= 1728;
					break;
				case 8204:
					num = 157;
					break;
				case 8205:
					num = 158;
					break;
				case 8206:
					num = 253;
					break;
				case 8207:
					num = 254;
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
				case 64342:
					num = 129;
					break;
				case 64344:
					num = 129;
					break;
				case 64358:
					num = 138;
					break;
				case 64360:
					num = 138;
					break;
				case 64378:
					num = 141;
					break;
				case 64380:
					num = 141;
					break;
				case 64392:
					num = 143;
					break;
				case 64394:
					num = 142;
					break;
				case 64396:
					num = 154;
					break;
				case 64398:
					num = 152;
					break;
				case 64400:
					num = 152;
					break;
				case 64402:
					num = 144;
					break;
				case 64404:
					num = 144;
					break;
				case 64414:
					num = 159;
					break;
				case 64422:
					num = 192;
					break;
				case 64424:
					num = 192;
					break;
				case 64426:
					num = 170;
					break;
				case 64428:
					num = 170;
					break;
				case 64430:
					num = 255;
					break;
				case 65136:
					num = 240;
					break;
				case 65137:
					num = 240;
					break;
				case 65138:
					num = 241;
					break;
				case 65140:
					num = 242;
					break;
				case 65142:
					num = 243;
					break;
				case 65143:
					num = 243;
					break;
				case 65144:
					num = 245;
					break;
				case 65145:
					num = 245;
					break;
				case 65146:
					num = 246;
					break;
				case 65147:
					num = 246;
					break;
				case 65148:
					num = 248;
					break;
				case 65149:
					num = 248;
					break;
				case 65150:
					num = 250;
					break;
				case 65151:
					num = 250;
					break;
				case 65152:
					num = 193;
					break;
				case 65153:
					num = 194;
					break;
				case 65154:
					num = 194;
					break;
				case 65155:
					num = 195;
					break;
				case 65156:
					num = 195;
					break;
				case 65157:
					num = 196;
					break;
				case 65158:
					num = 196;
					break;
				case 65159:
					num = 197;
					break;
				case 65160:
					num = 197;
					break;
				case 65161:
					num = 198;
					break;
				case 65162:
					num = 198;
					break;
				case 65163:
					num = 198;
					break;
				case 65164:
					num = 198;
					break;
				case 65165:
					num = 199;
					break;
				case 65166:
					num = 199;
					break;
				case 65167:
					num = 200;
					break;
				case 65168:
					num = 200;
					break;
				case 65169:
					num = 200;
					break;
				case 65170:
					num = 200;
					break;
				case 65171:
					num = 201;
					break;
				case 65172:
					num = 201;
					break;
				case 65173:
					num = 202;
					break;
				case 65174:
					num = 202;
					break;
				case 65175:
					num = 202;
					break;
				case 65176:
					num = 202;
					break;
				case 65177:
					num = 203;
					break;
				case 65178:
					num = 203;
					break;
				case 65179:
					num = 203;
					break;
				case 65180:
					num = 203;
					break;
				case 65181:
					num = 204;
					break;
				case 65182:
					num = 204;
					break;
				case 65183:
					num = 204;
					break;
				case 65184:
					num = 204;
					break;
				case 65185:
					num = 205;
					break;
				case 65186:
					num = 205;
					break;
				case 65187:
					num = 205;
					break;
				case 65188:
					num = 205;
					break;
				case 65189:
					num = 206;
					break;
				case 65190:
					num = 206;
					break;
				case 65191:
					num = 206;
					break;
				case 65192:
					num = 206;
					break;
				case 65193:
					num = 207;
					break;
				case 65194:
					num = 207;
					break;
				case 65195:
					num = 208;
					break;
				case 65196:
					num = 208;
					break;
				case 65197:
					num = 209;
					break;
				case 65198:
					num = 209;
					break;
				case 65199:
					num = 210;
					break;
				case 65200:
					num = 210;
					break;
				case 65201:
					num = 211;
					break;
				case 65202:
					num = 211;
					break;
				case 65203:
					num = 211;
					break;
				case 65204:
					num = 211;
					break;
				case 65205:
					num = 212;
					break;
				case 65206:
					num = 212;
					break;
				case 65207:
					num = 212;
					break;
				case 65208:
					num = 212;
					break;
				case 65209:
					num = 213;
					break;
				case 65210:
					num = 213;
					break;
				case 65211:
					num = 213;
					break;
				case 65212:
					num = 213;
					break;
				case 65213:
					num = 214;
					break;
				case 65214:
					num = 214;
					break;
				case 65215:
					num = 214;
					break;
				case 65216:
					num = 214;
					break;
				case 65217:
					num = 216;
					break;
				case 65218:
					num = 216;
					break;
				case 65219:
					num = 216;
					break;
				case 65220:
					num = 216;
					break;
				case 65221:
					num = 217;
					break;
				case 65222:
					num = 217;
					break;
				case 65223:
					num = 217;
					break;
				case 65224:
					num = 217;
					break;
				case 65225:
					num = 218;
					break;
				case 65226:
					num = 218;
					break;
				case 65227:
					num = 218;
					break;
				case 65228:
					num = 218;
					break;
				case 65229:
					num = 219;
					break;
				case 65230:
					num = 219;
					break;
				case 65231:
					num = 219;
					break;
				case 65232:
					num = 219;
					break;
				case 65233:
					num = 221;
					break;
				case 65234:
					num = 221;
					break;
				case 65235:
					num = 221;
					break;
				case 65236:
					num = 221;
					break;
				case 65237:
					num = 222;
					break;
				case 65238:
					num = 222;
					break;
				case 65239:
					num = 222;
					break;
				case 65240:
					num = 222;
					break;
				case 65241:
					num = 223;
					break;
				case 65242:
					num = 223;
					break;
				case 65243:
					num = 223;
					break;
				case 65244:
					num = 223;
					break;
				case 65245:
					num = 225;
					break;
				case 65246:
					num = 225;
					break;
				case 65247:
					num = 225;
					break;
				case 65248:
					num = 225;
					break;
				case 65249:
					num = 227;
					break;
				case 65250:
					num = 227;
					break;
				case 65251:
					num = 227;
					break;
				case 65252:
					num = 227;
					break;
				case 65253:
					num = 228;
					break;
				case 65254:
					num = 228;
					break;
				case 65255:
					num = 228;
					break;
				case 65256:
					num = 228;
					break;
				case 65257:
					num = 229;
					break;
				case 65258:
					num = 229;
					break;
				case 65259:
					num = 229;
					break;
				case 65260:
					num = 229;
					break;
				case 65261:
					num = 230;
					break;
				case 65262:
					num = 230;
					break;
				case 65263:
					num = 236;
					break;
				case 65264:
					num = 236;
					break;
				case 65265:
					num = 237;
					break;
				case 65266:
					num = 237;
					break;
				case 65267:
					num = 237;
					break;
				case 65268:
					num = 237;
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
				case 160:
				case 162:
				case 163:
				case 164:
				case 165:
				case 166:
				case 167:
				case 168:
				case 169:
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
				case 187:
				case 188:
				case 189:
				case 190:
				case 215:
				case 224:
				case 226:
				case 231:
				case 232:
				case 233:
				case 234:
				case 235:
				case 238:
				case 239:
				case 244:
				case 247:
				case 249:
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
public class ENCwindows_1256 : CP1256
{
}
[Serializable]
public class CP28596 : ByteEncoding
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
		'\u00a0', '\uf7c8', '\uf7c9', '\uf7ca', '¤', '\uf7cb', '\uf7cc', '\uf7cd', '\uf7ce', '\uf7cf',
		'\uf7d0', '\uf7d1', '،', '\u00ad', '\uf7d2', '\uf7d3', '\uf7d4', '\uf7d5', '\uf7d6', '\uf7d7',
		'\uf7d8', '\uf7d9', '\uf7da', '\uf7db', '\uf7dc', '\uf7dd', '\uf7de', '؛', '\uf7df', '\uf7e0',
		'\uf7e1', '؟', '\uf7e2', 'ء', 'آ', 'أ', 'ؤ', 'إ', 'ئ', 'ا',
		'ب', 'ة', 'ت', 'ث', 'ج', 'ح', 'خ', 'د', 'ذ', 'ر',
		'ز', 'س', 'ش', 'ص', 'ض', 'ط', 'ظ', 'ع', 'غ', '\uf7e3',
		'\uf7e4', '\uf7e5', '\uf7e6', '\uf7e7', 'ـ', 'ف', 'ق', 'ك', 'ل', 'م',
		'ن', 'ه', 'و', 'ى', 'ي', '\u064b', '\u064c', '\u064d', '\u064e', '\u064f',
		'\u0650', '\u0651', '\u0652', '\uf7e8', '\uf7e9', '\uf7ea', '\uf7eb', '\uf7ec', '\uf7ed', '\uf7ee',
		'\uf7ef', '\uf7f0', '\uf7f1', '\uf7f2', '\uf7f3', '\uf7f4'
	};

	public CP28596()
		: base(28596, ToChars, "Arabic (ISO)", "iso-8859-6", "iso-8859-6", "iso-8859-6", isBrowserDisplay: true, isBrowserSave: true, isMailNewsDisplay: true, isMailNewsSave: true, 1256)
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
				case 161:
					num = 33;
					break;
				case 162:
					num = 99;
					break;
				case 165:
					num = 89;
					break;
				case 166:
					num = 124;
					break;
				case 169:
					num = 67;
					break;
				case 170:
					num = 97;
					break;
				case 171:
					num = 60;
					break;
				case 174:
					num = 82;
					break;
				case 178:
					num = 50;
					break;
				case 179:
					num = 51;
					break;
				case 183:
					num = 46;
					break;
				case 184:
					num = 44;
					break;
				case 185:
					num = 49;
					break;
				case 186:
					num = 111;
					break;
				case 187:
					num = 62;
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
					num = 65;
					break;
				case 197:
					num = 65;
					break;
				case 198:
					num = 65;
					break;
				case 199:
					num = 67;
					break;
				case 200:
					num = 69;
					break;
				case 201:
					num = 69;
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
					num = 78;
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
					num = 79;
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
					num = 85;
					break;
				case 221:
					num = 89;
					break;
				case 224:
					num = 97;
					break;
				case 225:
					num = 97;
					break;
				case 226:
					num = 97;
					break;
				case 227:
					num = 97;
					break;
				case 228:
					num = 97;
					break;
				case 229:
					num = 97;
					break;
				case 230:
					num = 97;
					break;
				case 231:
					num = 99;
					break;
				case 232:
					num = 101;
					break;
				case 233:
					num = 101;
					break;
				case 234:
					num = 101;
					break;
				case 235:
					num = 101;
					break;
				case 236:
					num = 105;
					break;
				case 237:
					num = 105;
					break;
				case 238:
					num = 105;
					break;
				case 239:
					num = 105;
					break;
				case 241:
					num = 110;
					break;
				case 242:
					num = 111;
					break;
				case 243:
					num = 111;
					break;
				case 244:
					num = 111;
					break;
				case 245:
					num = 111;
					break;
				case 246:
					num = 111;
					break;
				case 248:
					num = 111;
					break;
				case 249:
					num = 117;
					break;
				case 250:
					num = 117;
					break;
				case 251:
					num = 117;
					break;
				case 252:
					num = 117;
					break;
				case 253:
					num = 121;
					break;
				case 255:
					num = 121;
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
					num = 70;
					break;
				case 402:
					num = 102;
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
				case 715:
					num = 96;
					break;
				case 717:
					num = 95;
					break;
				case 732:
					num = 126;
					break;
				case 768:
					num = 96;
					break;
				case 770:
					num = 94;
					break;
				case 771:
					num = 126;
					break;
				case 782:
					num = 34;
					break;
				case 817:
					num = 95;
					break;
				case 818:
					num = 95;
					break;
				case 1548:
					num = 172;
					break;
				case 1563:
					num = 187;
					break;
				case 1567:
					num = 191;
					break;
				case 1569:
				case 1570:
				case 1571:
				case 1572:
				case 1573:
				case 1574:
				case 1575:
				case 1576:
				case 1577:
				case 1578:
				case 1579:
				case 1580:
				case 1581:
				case 1582:
				case 1583:
				case 1584:
				case 1585:
				case 1586:
				case 1587:
				case 1588:
				case 1589:
				case 1590:
				case 1591:
				case 1592:
				case 1593:
				case 1594:
					num -= 1376;
					break;
				case 1600:
				case 1601:
				case 1602:
				case 1603:
				case 1604:
				case 1605:
				case 1606:
				case 1607:
				case 1608:
				case 1609:
				case 1610:
				case 1611:
				case 1612:
				case 1613:
				case 1614:
				case 1615:
				case 1616:
				case 1617:
				case 1618:
					num -= 1376;
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
				case 8216:
					num = 39;
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
					num = 34;
					break;
				case 8226:
					num = 46;
					break;
				case 8230:
					num = 46;
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
				case 8482:
					num = 84;
					break;
				case 63432:
					num = 161;
					break;
				case 63433:
					num = 162;
					break;
				case 63434:
					num = 163;
					break;
				case 63435:
				case 63436:
				case 63437:
				case 63438:
				case 63439:
				case 63440:
				case 63441:
					num -= 63270;
					break;
				case 63442:
				case 63443:
				case 63444:
				case 63445:
				case 63446:
				case 63447:
				case 63448:
				case 63449:
				case 63450:
				case 63451:
				case 63452:
				case 63453:
				case 63454:
					num -= 63268;
					break;
				case 63455:
					num = 188;
					break;
				case 63456:
					num = 189;
					break;
				case 63457:
					num = 190;
					break;
				case 63458:
					num = 192;
					break;
				case 63459:
				case 63460:
				case 63461:
				case 63462:
				case 63463:
					num -= 63240;
					break;
				case 63464:
				case 63465:
				case 63466:
				case 63467:
				case 63468:
				case 63469:
				case 63470:
				case 63471:
				case 63472:
				case 63473:
				case 63474:
				case 63475:
				case 63476:
					num -= 63221;
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
				case 164:
				case 173:
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
public class ENCiso_8859_6 : CP28596
{
}
[Serializable]
public class CP28598 : ByteEncoding
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
		'\u00a0', '?', '¢', '£', '¤', '¥', '¦', '§', '\u00a8', '©',
		'×', '«', '¬', '\u00ad', '®', '‾', '°', '±', '²', '³',
		'\u00b4', 'µ', '¶', '•', '\u00b8', '¹', '÷', '»', '¼', '½',
		'¾', '?', '?', '?', '?', '?', '?', '?', '?', '?',
		'?', '?', '?', '?', '?', '?', '?', '?', '?', '?',
		'?', '?', '?', '?', '?', '?', '?', '?', '?', '?',
		'?', '?', '?', '‗', 'א', 'ב', 'ג', 'ד', 'ה', 'ו',
		'ז', 'ח', 'ט', 'י', 'ך', 'כ', 'ל', 'ם', 'מ', 'ן',
		'נ', 'ס', 'ע', 'ף', 'פ', 'ץ', 'צ', 'ק', 'ר', 'ש',
		'ת', '?', '?', '?', '?', '?'
	};

	public CP28598()
		: base(28598, ToChars, "Hebrew (ISO)", "iso-8859-8", "iso-8859-8", "iso-8859-8", isBrowserDisplay: true, isBrowserSave: true, isMailNewsDisplay: true, isMailNewsSave: true, 1255)
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
				case 215:
					num = 170;
					break;
				case 247:
					num = 186;
					break;
				case 1488:
				case 1489:
				case 1490:
				case 1491:
				case 1492:
				case 1493:
				case 1494:
				case 1495:
				case 1496:
				case 1497:
				case 1498:
				case 1499:
				case 1500:
				case 1501:
				case 1502:
				case 1503:
				case 1504:
				case 1505:
				case 1506:
				case 1507:
				case 1508:
				case 1509:
				case 1510:
				case 1511:
				case 1512:
				case 1513:
				case 1514:
					num -= 1264;
					break;
				case 8215:
					num = 223;
					break;
				case 8226:
					num = 183;
					break;
				case 8254:
					num = 175;
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
				case 164:
				case 165:
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
				case 178:
				case 179:
				case 180:
				case 181:
				case 182:
				case 184:
				case 185:
				case 187:
				case 188:
				case 189:
				case 190:
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
public class ENCiso_8859_8 : CP28598
{
}
[Serializable]
public class CP28599 : ByteEncoding
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
		'\u00a0', '¡', '¢', '£', '¤', '¥', '¦', '§', '\u00a8', '©',
		'ª', '«', '¬', '\u00ad', '®', '\u00af', '°', '±', '²', '³',
		'\u00b4', 'µ', '¶', '·', '\u00b8', '¹', 'º', '»', '¼', '½',
		'¾', '¿', 'À', 'Á', 'Â', 'Ã', 'Ä', 'Å', 'Æ', 'Ç',
		'È', 'É', 'Ê', 'Ë', 'Ì', 'Í', 'Î', 'Ï', 'Ğ', 'Ñ',
		'Ò', 'Ó', 'Ô', 'Õ', 'Ö', '×', 'Ø', 'Ù', 'Ú', 'Û',
		'Ü', 'İ', 'Ş', 'ß', 'à', 'á', 'â', 'ã', 'ä', 'å',
		'æ', 'ç', 'è', 'é', 'ê', 'ë', 'ì', 'í', 'î', 'ï',
		'ğ', 'ñ', 'ò', 'ó', 'ô', 'õ', 'ö', '÷', 'ø', 'ù',
		'ú', 'û', 'ü', 'ı', 'ş', 'ÿ'
	};

	public CP28599()
		: base(28599, ToChars, "Latin 5 (ISO)", "iso-8859-9", "iso-8859-9", "iso-8859-9", isBrowserDisplay: true, isBrowserSave: true, isMailNewsDisplay: true, isMailNewsSave: true, 1254)
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
			if (num >= 208)
			{
				switch (num)
				{
				case 286:
					num = 208;
					break;
				case 287:
					num = 240;
					break;
				case 304:
					num = 221;
					break;
				case 305:
					num = 253;
					break;
				case 350:
					num = 222;
					break;
				case 351:
					num = 254;
					break;
				case 8254:
					num = 175;
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
public class ENCiso_8859_9 : CP28599
{
}
[Serializable]
public class CP38598 : ByteEncoding
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
		'\u00a0', '?', '¢', '£', '¤', '¥', '¦', '§', '\u00a8', '©',
		'×', '«', '¬', '\u00ad', '®', '‾', '°', '±', '²', '³',
		'\u00b4', 'µ', '¶', '•', '\u00b8', '¹', '÷', '»', '¼', '½',
		'¾', '?', '?', '?', '?', '?', '?', '?', '?', '?',
		'?', '?', '?', '?', '?', '?', '?', '?', '?', '?',
		'?', '?', '?', '?', '?', '?', '?', '?', '?', '?',
		'?', '?', '?', '‗', 'א', 'ב', 'ג', 'ד', 'ה', 'ו',
		'ז', 'ח', 'ט', 'י', 'ך', 'כ', 'ל', 'ם', 'מ', 'ן',
		'נ', 'ס', 'ע', 'ף', 'פ', 'ץ', 'צ', 'ק', 'ר', 'ש',
		'ת', '?', '?', '?', '?', '?'
	};

	public CP38598()
		: base(38598, ToChars, "Hebrew (ISO Alternative)", "iso-8859-8", "windows-38598", "windows-38598", isBrowserDisplay: true, isBrowserSave: true, isMailNewsDisplay: true, isMailNewsSave: true, 1255)
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
				case 215:
					num = 170;
					break;
				case 247:
					num = 186;
					break;
				case 1488:
				case 1489:
				case 1490:
				case 1491:
				case 1492:
				case 1493:
				case 1494:
				case 1495:
				case 1496:
				case 1497:
				case 1498:
				case 1499:
				case 1500:
				case 1501:
				case 1502:
				case 1503:
				case 1504:
				case 1505:
				case 1506:
				case 1507:
				case 1508:
				case 1509:
				case 1510:
				case 1511:
				case 1512:
				case 1513:
				case 1514:
					num -= 1264;
					break;
				case 8215:
					num = 223;
					break;
				case 8226:
					num = 183;
					break;
				case 8254:
					num = 175;
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
				case 164:
				case 165:
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
				case 178:
				case 179:
				case 180:
				case 181:
				case 182:
				case 184:
				case 185:
				case 187:
				case 188:
				case 189:
				case 190:
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
public class ENCwindows_38598 : CP38598
{
}
