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
namespace I18N.Other;

[Serializable]
public class CP1251 : ByteEncoding
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
		'x', 'y', 'z', '{', '|', '}', '~', '\u007f', 'Ђ', 'Ѓ',
		'‚', 'ѓ', '„', '…', '†', '‡', '€', '‰', 'Љ', '‹',
		'Њ', 'Ќ', 'Ћ', 'Џ', 'ђ', '‘', '’', '“', '”', '•',
		'–', '—', '\u0098', '™', 'љ', '›', 'њ', 'ќ', 'ћ', 'џ',
		'\u00a0', 'Ў', 'ў', 'Ј', '¤', 'Ґ', '¦', '§', 'Ё', '©',
		'Є', '«', '¬', '\u00ad', '®', 'Ї', '°', '±', 'І', 'і',
		'ґ', 'µ', '¶', '·', 'ё', '№', 'є', '»', 'ј', 'Ѕ',
		'ѕ', 'ї', 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ж', 'З',
		'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С',
		'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы',
		'Ь', 'Э', 'Ю', 'Я', 'а', 'б', 'в', 'г', 'д', 'е',
		'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п',
		'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ',
		'ъ', 'ы', 'ь', 'э', 'ю', 'я'
	};

	public CP1251()
		: base(1251, ToChars, "Cyrillic (Windows)", "koi8-r", "windows-1251", "windows-1251", isBrowserDisplay: true, isBrowserSave: true, isMailNewsDisplay: true, isMailNewsSave: true, 1251)
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
				case 1025:
					num = 168;
					break;
				case 1026:
					num = 128;
					break;
				case 1027:
					num = 129;
					break;
				case 1028:
					num = 170;
					break;
				case 1029:
					num = 189;
					break;
				case 1030:
					num = 178;
					break;
				case 1031:
					num = 175;
					break;
				case 1032:
					num = 163;
					break;
				case 1033:
					num = 138;
					break;
				case 1034:
					num = 140;
					break;
				case 1035:
					num = 142;
					break;
				case 1036:
					num = 141;
					break;
				case 1038:
					num = 161;
					break;
				case 1039:
					num = 143;
					break;
				case 1040:
				case 1041:
				case 1042:
				case 1043:
				case 1044:
				case 1045:
				case 1046:
				case 1047:
				case 1048:
				case 1049:
				case 1050:
				case 1051:
				case 1052:
				case 1053:
				case 1054:
				case 1055:
				case 1056:
				case 1057:
				case 1058:
				case 1059:
				case 1060:
				case 1061:
				case 1062:
				case 1063:
				case 1064:
				case 1065:
				case 1066:
				case 1067:
				case 1068:
				case 1069:
				case 1070:
				case 1071:
				case 1072:
				case 1073:
				case 1074:
				case 1075:
				case 1076:
				case 1077:
				case 1078:
				case 1079:
				case 1080:
				case 1081:
				case 1082:
				case 1083:
				case 1084:
				case 1085:
				case 1086:
				case 1087:
				case 1088:
				case 1089:
				case 1090:
				case 1091:
				case 1092:
				case 1093:
				case 1094:
				case 1095:
				case 1096:
				case 1097:
				case 1098:
				case 1099:
				case 1100:
				case 1101:
				case 1102:
				case 1103:
					num -= 848;
					break;
				case 1105:
					num = 184;
					break;
				case 1106:
					num = 144;
					break;
				case 1107:
					num = 131;
					break;
				case 1108:
					num = 186;
					break;
				case 1109:
					num = 190;
					break;
				case 1110:
					num = 179;
					break;
				case 1111:
					num = 191;
					break;
				case 1112:
					num = 188;
					break;
				case 1113:
					num = 154;
					break;
				case 1114:
					num = 156;
					break;
				case 1115:
					num = 158;
					break;
				case 1116:
					num = 157;
					break;
				case 1118:
					num = 162;
					break;
				case 1119:
					num = 159;
					break;
				case 1168:
					num = 165;
					break;
				case 1169:
					num = 180;
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
					num = 136;
					break;
				case 8470:
					num = 185;
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
				case 152:
				case 160:
				case 164:
				case 166:
				case 167:
				case 169:
				case 171:
				case 172:
				case 173:
				case 174:
				case 176:
				case 177:
				case 181:
				case 182:
				case 183:
				case 187:
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
public class ENCwindows_1251 : CP1251
{
}
[Serializable]
public class CP1257 : ByteEncoding
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
		'‚', '\u0083', '„', '…', '†', '‡', '\u0088', '‰', '\u008a', '‹',
		'\u008c', '\u00a8', 'ˇ', '\u00b8', '\u0090', '‘', '’', '“', '”', '•',
		'–', '—', '\u0098', '™', '\u009a', '›', '\u009c', '\u00af', '\u02db', '\u009f',
		'\u00a0', '?', '¢', '£', '¤', '?', '¦', '§', 'Ø', '©',
		'Ŗ', '«', '¬', '\u00ad', '®', 'Æ', '°', '±', '²', '³',
		'\u00b4', 'µ', '¶', '·', 'ø', '¹', 'ŗ', '»', '¼', '½',
		'¾', 'æ', 'Ą', 'Į', 'Ā', 'Ć', 'Ä', 'Å', 'Ę', 'Ē',
		'Č', 'É', 'Ź', 'Ė', 'Ģ', 'Ķ', 'Ī', 'Ļ', 'Š', 'Ń',
		'Ņ', 'Ó', 'Ō', 'Õ', 'Ö', '×', 'Ų', 'Ł', 'Ś', 'Ū',
		'Ü', 'Ż', 'Ž', 'ß', 'ą', 'į', 'ā', 'ć', 'ä', 'å',
		'ę', 'ē', 'č', 'é', 'ź', 'ė', 'ģ', 'ķ', 'ī', 'ļ',
		'š', 'ń', 'ņ', 'ó', 'ō', 'õ', 'ö', '÷', 'ų', 'ł',
		'ś', 'ū', 'ü', 'ż', 'ž', '\u02d9'
	};

	public CP1257()
		: base(1257, ToChars, "Baltic (Windows)", "iso-8859-4", "windows-1257", "windows-1257", isBrowserDisplay: true, isBrowserSave: true, isMailNewsDisplay: true, isMailNewsSave: true, 1257)
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
				case 168:
					num = 141;
					break;
				case 175:
					num = 157;
					break;
				case 184:
					num = 143;
					break;
				case 198:
					num = 175;
					break;
				case 216:
					num = 168;
					break;
				case 230:
					num = 191;
					break;
				case 248:
					num = 184;
					break;
				case 256:
					num = 194;
					break;
				case 257:
					num = 226;
					break;
				case 260:
					num = 192;
					break;
				case 261:
					num = 224;
					break;
				case 262:
					num = 195;
					break;
				case 263:
					num = 227;
					break;
				case 268:
					num = 200;
					break;
				case 269:
					num = 232;
					break;
				case 274:
					num = 199;
					break;
				case 275:
					num = 231;
					break;
				case 278:
					num = 203;
					break;
				case 279:
					num = 235;
					break;
				case 280:
					num = 198;
					break;
				case 281:
					num = 230;
					break;
				case 290:
					num = 204;
					break;
				case 291:
					num = 236;
					break;
				case 298:
					num = 206;
					break;
				case 299:
					num = 238;
					break;
				case 302:
					num = 193;
					break;
				case 303:
					num = 225;
					break;
				case 310:
					num = 205;
					break;
				case 311:
					num = 237;
					break;
				case 315:
					num = 207;
					break;
				case 316:
					num = 239;
					break;
				case 321:
					num = 217;
					break;
				case 322:
					num = 249;
					break;
				case 323:
					num = 209;
					break;
				case 324:
					num = 241;
					break;
				case 325:
					num = 210;
					break;
				case 326:
					num = 242;
					break;
				case 332:
					num = 212;
					break;
				case 333:
					num = 244;
					break;
				case 342:
					num = 170;
					break;
				case 343:
					num = 186;
					break;
				case 346:
					num = 218;
					break;
				case 347:
					num = 250;
					break;
				case 352:
					num = 208;
					break;
				case 353:
					num = 240;
					break;
				case 362:
					num = 219;
					break;
				case 363:
					num = 251;
					break;
				case 370:
					num = 216;
					break;
				case 371:
					num = 248;
					break;
				case 377:
					num = 202;
					break;
				case 378:
					num = 234;
					break;
				case 379:
					num = 221;
					break;
				case 380:
					num = 253;
					break;
				case 381:
					num = 222;
					break;
				case 382:
					num = 254;
					break;
				case 711:
					num = 142;
					break;
				case 729:
					num = 255;
					break;
				case 731:
					num = 158;
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
				case 131:
				case 136:
				case 138:
				case 140:
				case 144:
				case 152:
				case 154:
				case 156:
				case 159:
				case 160:
				case 162:
				case 163:
				case 164:
				case 166:
				case 167:
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
				case 183:
				case 185:
				case 187:
				case 188:
				case 189:
				case 190:
				case 196:
				case 197:
				case 201:
				case 211:
				case 213:
				case 214:
				case 215:
				case 220:
				case 223:
				case 228:
				case 229:
				case 233:
				case 243:
				case 245:
				case 246:
				case 247:
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
public class ENCwindows_1257 : CP1257
{
}
[Serializable]
public class CP1258 : ByteEncoding
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
		'Œ', '\u008d', '\u008e', '\u008f', '\u0090', '‘', '’', '“', '”', '•',
		'–', '—', '\u02dc', '™', '\u009a', '›', 'œ', '\u009d', '\u009e', 'Ÿ',
		'\u00a0', '¡', '¢', '£', '¤', '¥', '¦', '§', '\u00a8', '©',
		'ª', '«', '¬', '\u00ad', '®', '\u00af', '°', '±', '²', '³',
		'\u00b4', 'µ', '¶', '·', '\u00b8', '¹', 'º', '»', '¼', '½',
		'¾', '¿', 'À', 'Á', 'Â', 'Ă', 'Ä', 'Å', 'Æ', 'Ç',
		'È', 'É', 'Ê', 'Ë', '\u0300', 'Í', 'Î', 'Ï', 'Đ', 'Ñ',
		'\u0309', 'Ó', 'Ô', 'Ơ', 'Ö', '×', 'Ø', 'Ù', 'Ú', 'Û',
		'Ü', 'Ư', '\u0303', 'ß', 'à', 'á', 'â', 'ă', 'ä', 'å',
		'æ', 'ç', 'è', 'é', 'ê', 'ë', '\u0301', 'í', 'î', 'ï',
		'đ', 'ñ', '\u0323', 'ó', 'ô', 'ơ', 'ö', '÷', 'ø', 'ù',
		'ú', 'û', 'ü', 'ư', '₫', 'ÿ'
	};

	public CP1258()
		: base(1258, ToChars, "Vietnamese (Windows)", "windows-1258", "windows-1258", "windows-1258", isBrowserDisplay: true, isBrowserSave: true, isMailNewsDisplay: true, isMailNewsSave: true, 1258)
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
				case 258:
					num = 195;
					break;
				case 259:
					num = 227;
					break;
				case 272:
					num = 208;
					break;
				case 273:
					num = 240;
					break;
				case 338:
					num = 140;
					break;
				case 339:
					num = 156;
					break;
				case 376:
					num = 159;
					break;
				case 402:
					num = 131;
					break;
				case 416:
					num = 213;
					break;
				case 417:
					num = 245;
					break;
				case 431:
					num = 221;
					break;
				case 432:
					num = 253;
					break;
				case 710:
					num = 136;
					break;
				case 732:
					num = 152;
					break;
				case 768:
					num = 204;
					break;
				case 769:
					num = 236;
					break;
				case 771:
					num = 222;
					break;
				case 777:
					num = 210;
					break;
				case 803:
					num = 242;
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
				case 8363:
					num = 254;
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
				case 141:
				case 142:
				case 143:
				case 144:
				case 154:
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
				case 196:
				case 197:
				case 198:
				case 199:
				case 200:
				case 201:
				case 202:
				case 203:
				case 205:
				case 206:
				case 207:
				case 209:
				case 211:
				case 212:
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
				case 228:
				case 229:
				case 230:
				case 231:
				case 232:
				case 233:
				case 234:
				case 235:
				case 237:
				case 238:
				case 239:
				case 241:
				case 243:
				case 244:
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
public class ENCwindows_1258 : CP1258
{
}
[Serializable]
public class CP20866 : ByteEncoding
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
		'x', 'y', 'z', '{', '|', '}', '~', '\u007f', '─', '│',
		'┌', '┐', '└', '┘', '├', '┤', '┬', '┴', '┼', '▀',
		'▄', '█', '▌', '▐', '░', '▒', '▓', '⌠', '■', '∙',
		'√', '≈', '≤', '≥', '\u00a0', '⌡', '°', '²', '·', '÷',
		'═', '║', '╒', 'ё', '╓', '╔', '╕', '╖', '╗', '╘',
		'╙', '╚', '╛', '╜', '╝', '╞', '╟', '╠', '╡', 'Ё',
		'╢', '╣', '╤', '╥', '╦', '╧', '╨', '╩', '╪', '╫',
		'╬', '©', 'ю', 'а', 'б', 'ц', 'д', 'е', 'ф', 'г',
		'х', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'я',
		'р', 'с', 'т', 'у', 'ж', 'в', 'ь', 'ы', 'з', 'ш',
		'э', 'щ', 'ч', 'ъ', 'Ю', 'А', 'Б', 'Ц', 'Д', 'Е',
		'Ф', 'Г', 'Х', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О',
		'П', 'Я', 'Р', 'С', 'Т', 'У', 'Ж', 'В', 'Ь', 'Ы',
		'З', 'Ш', 'Э', 'Щ', 'Ч', 'Ъ'
	};

	public CP20866()
		: base(20866, ToChars, "Cyrillic (KOI8-R)", "koi8-r", "koi8-r", "koi8-r", isBrowserDisplay: true, isBrowserSave: true, isMailNewsDisplay: true, isMailNewsSave: true, 1251)
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
					num = 154;
					break;
				case 169:
					num = 191;
					break;
				case 176:
					num = 156;
					break;
				case 178:
					num = 157;
					break;
				case 183:
					num = 158;
					break;
				case 247:
					num = 159;
					break;
				case 1025:
					num = 179;
					break;
				case 1040:
					num = 225;
					break;
				case 1041:
					num = 226;
					break;
				case 1042:
					num = 247;
					break;
				case 1043:
					num = 231;
					break;
				case 1044:
					num = 228;
					break;
				case 1045:
					num = 229;
					break;
				case 1046:
					num = 246;
					break;
				case 1047:
					num = 250;
					break;
				case 1048:
				case 1049:
				case 1050:
				case 1051:
				case 1052:
				case 1053:
				case 1054:
				case 1055:
					num -= 815;
					break;
				case 1056:
				case 1057:
				case 1058:
				case 1059:
					num -= 814;
					break;
				case 1060:
					num = 230;
					break;
				case 1061:
					num = 232;
					break;
				case 1062:
					num = 227;
					break;
				case 1063:
					num = 254;
					break;
				case 1064:
					num = 251;
					break;
				case 1065:
					num = 253;
					break;
				case 1066:
					num = 255;
					break;
				case 1067:
					num = 249;
					break;
				case 1068:
					num = 248;
					break;
				case 1069:
					num = 252;
					break;
				case 1070:
					num = 224;
					break;
				case 1071:
					num = 241;
					break;
				case 1072:
					num = 193;
					break;
				case 1073:
					num = 194;
					break;
				case 1074:
					num = 215;
					break;
				case 1075:
					num = 199;
					break;
				case 1076:
					num = 196;
					break;
				case 1077:
					num = 197;
					break;
				case 1078:
					num = 214;
					break;
				case 1079:
					num = 218;
					break;
				case 1080:
				case 1081:
				case 1082:
				case 1083:
				case 1084:
				case 1085:
				case 1086:
				case 1087:
					num -= 879;
					break;
				case 1088:
				case 1089:
				case 1090:
				case 1091:
					num -= 878;
					break;
				case 1092:
					num = 198;
					break;
				case 1093:
					num = 200;
					break;
				case 1094:
					num = 195;
					break;
				case 1095:
					num = 222;
					break;
				case 1096:
					num = 219;
					break;
				case 1097:
					num = 221;
					break;
				case 1098:
					num = 223;
					break;
				case 1099:
					num = 217;
					break;
				case 1100:
					num = 216;
					break;
				case 1101:
					num = 220;
					break;
				case 1102:
					num = 192;
					break;
				case 1103:
					num = 209;
					break;
				case 1105:
					num = 163;
					break;
				case 8729:
					num = 149;
					break;
				case 8730:
					num = 150;
					break;
				case 8776:
					num = 151;
					break;
				case 8804:
					num = 152;
					break;
				case 8805:
					num = 153;
					break;
				case 8992:
					num = 147;
					break;
				case 8993:
					num = 155;
					break;
				case 9472:
					num = 128;
					break;
				case 9474:
					num = 129;
					break;
				case 9484:
					num = 130;
					break;
				case 9488:
					num = 131;
					break;
				case 9492:
					num = 132;
					break;
				case 9496:
					num = 133;
					break;
				case 9500:
					num = 134;
					break;
				case 9508:
					num = 135;
					break;
				case 9516:
					num = 136;
					break;
				case 9524:
					num = 137;
					break;
				case 9532:
					num = 138;
					break;
				case 9552:
					num = 160;
					break;
				case 9553:
					num = 161;
					break;
				case 9554:
					num = 162;
					break;
				case 9555:
				case 9556:
				case 9557:
				case 9558:
				case 9559:
				case 9560:
				case 9561:
				case 9562:
				case 9563:
				case 9564:
				case 9565:
				case 9566:
				case 9567:
				case 9568:
				case 9569:
					num -= 9391;
					break;
				case 9570:
				case 9571:
				case 9572:
				case 9573:
				case 9574:
				case 9575:
				case 9576:
				case 9577:
				case 9578:
				case 9579:
				case 9580:
					num -= 9390;
					break;
				case 9600:
					num = 139;
					break;
				case 9604:
					num = 140;
					break;
				case 9608:
					num = 141;
					break;
				case 9612:
					num = 142;
					break;
				case 9616:
				case 9617:
				case 9618:
				case 9619:
					num -= 9473;
					break;
				case 9632:
					num = 148;
					break;
				case 65512:
					num = 129;
					break;
				case 65517:
					num = 148;
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
				}
			}
			bytes[byteIndex++] = (byte)num;
			charCount--;
			byteCount--;
		}
	}
}
[Serializable]
public class ENCkoi8_r : CP20866
{
}
[Serializable]
public class CP21866 : ByteEncoding
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
		'x', 'y', 'z', '{', '|', '}', '~', '\u007f', '─', '│',
		'┌', '┐', '└', '┘', '├', '┤', '┬', '┴', '┼', '▀',
		'▄', '█', '▌', '▐', '░', '▒', '▓', '⌠', '■', '∙',
		'√', '≈', '≤', '≥', '\u00a0', '⌡', '°', '²', '·', '÷',
		'═', '║', '╒', 'ё', 'є', '╔', 'і', 'ї', '╗', '╘',
		'╙', '╚', '╛', 'ґ', '╝', '╞', '╟', '╠', '╡', 'Ё',
		'Є', '╣', 'І', 'Ї', '╦', '╧', '╨', '╩', '╪', 'Ґ',
		'╬', '©', 'ю', 'а', 'б', 'ц', 'д', 'е', 'ф', 'г',
		'х', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'я',
		'р', 'с', 'т', 'у', 'ж', 'в', 'ь', 'ы', 'з', 'ш',
		'э', 'щ', 'ч', 'ъ', 'Ю', 'А', 'Б', 'Ц', 'Д', 'Е',
		'Ф', 'Г', 'Х', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О',
		'П', 'Я', 'Р', 'С', 'Т', 'У', 'Ж', 'В', 'Ь', 'Ы',
		'З', 'Ш', 'Э', 'Щ', 'Ч', 'Ъ'
	};

	public CP21866()
		: base(21866, ToChars, "Ukrainian (KOI8-U)", "koi8-u", "koi8-u", "koi8-u", isBrowserDisplay: true, isBrowserSave: true, isMailNewsDisplay: true, isMailNewsSave: true, 1251)
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
					num = 154;
					break;
				case 169:
					num = 191;
					break;
				case 176:
					num = 156;
					break;
				case 178:
					num = 157;
					break;
				case 183:
					num = 158;
					break;
				case 247:
					num = 159;
					break;
				case 1025:
					num = 179;
					break;
				case 1028:
					num = 180;
					break;
				case 1030:
					num = 182;
					break;
				case 1031:
					num = 183;
					break;
				case 1040:
					num = 225;
					break;
				case 1041:
					num = 226;
					break;
				case 1042:
					num = 247;
					break;
				case 1043:
					num = 231;
					break;
				case 1044:
					num = 228;
					break;
				case 1045:
					num = 229;
					break;
				case 1046:
					num = 246;
					break;
				case 1047:
					num = 250;
					break;
				case 1048:
				case 1049:
				case 1050:
				case 1051:
				case 1052:
				case 1053:
				case 1054:
				case 1055:
					num -= 815;
					break;
				case 1056:
				case 1057:
				case 1058:
				case 1059:
					num -= 814;
					break;
				case 1060:
					num = 230;
					break;
				case 1061:
					num = 232;
					break;
				case 1062:
					num = 227;
					break;
				case 1063:
					num = 254;
					break;
				case 1064:
					num = 251;
					break;
				case 1065:
					num = 253;
					break;
				case 1066:
					num = 255;
					break;
				case 1067:
					num = 249;
					break;
				case 1068:
					num = 248;
					break;
				case 1069:
					num = 252;
					break;
				case 1070:
					num = 224;
					break;
				case 1071:
					num = 241;
					break;
				case 1072:
					num = 193;
					break;
				case 1073:
					num = 194;
					break;
				case 1074:
					num = 215;
					break;
				case 1075:
					num = 199;
					break;
				case 1076:
					num = 196;
					break;
				case 1077:
					num = 197;
					break;
				case 1078:
					num = 214;
					break;
				case 1079:
					num = 218;
					break;
				case 1080:
				case 1081:
				case 1082:
				case 1083:
				case 1084:
				case 1085:
				case 1086:
				case 1087:
					num -= 879;
					break;
				case 1088:
				case 1089:
				case 1090:
				case 1091:
					num -= 878;
					break;
				case 1092:
					num = 198;
					break;
				case 1093:
					num = 200;
					break;
				case 1094:
					num = 195;
					break;
				case 1095:
					num = 222;
					break;
				case 1096:
					num = 219;
					break;
				case 1097:
					num = 221;
					break;
				case 1098:
					num = 223;
					break;
				case 1099:
					num = 217;
					break;
				case 1100:
					num = 216;
					break;
				case 1101:
					num = 220;
					break;
				case 1102:
					num = 192;
					break;
				case 1103:
					num = 209;
					break;
				case 1105:
					num = 163;
					break;
				case 1108:
					num = 164;
					break;
				case 1110:
					num = 166;
					break;
				case 1111:
					num = 167;
					break;
				case 1168:
					num = 189;
					break;
				case 1169:
					num = 173;
					break;
				case 8729:
					num = 149;
					break;
				case 8730:
					num = 150;
					break;
				case 8776:
					num = 151;
					break;
				case 8804:
					num = 152;
					break;
				case 8805:
					num = 153;
					break;
				case 8992:
					num = 147;
					break;
				case 8993:
					num = 155;
					break;
				case 9472:
					num = 128;
					break;
				case 9474:
					num = 129;
					break;
				case 9484:
					num = 130;
					break;
				case 9488:
					num = 131;
					break;
				case 9492:
					num = 132;
					break;
				case 9496:
					num = 133;
					break;
				case 9500:
					num = 134;
					break;
				case 9508:
					num = 135;
					break;
				case 9516:
					num = 136;
					break;
				case 9524:
					num = 137;
					break;
				case 9532:
					num = 138;
					break;
				case 9552:
					num = 160;
					break;
				case 9553:
					num = 161;
					break;
				case 9554:
					num = 162;
					break;
				case 9556:
					num = 165;
					break;
				case 9559:
				case 9560:
				case 9561:
				case 9562:
				case 9563:
					num -= 9391;
					break;
				case 9565:
				case 9566:
				case 9567:
				case 9568:
				case 9569:
					num -= 9391;
					break;
				case 9571:
					num = 181;
					break;
				case 9574:
				case 9575:
				case 9576:
				case 9577:
				case 9578:
					num -= 9390;
					break;
				case 9580:
					num = 190;
					break;
				case 9600:
					num = 139;
					break;
				case 9604:
					num = 140;
					break;
				case 9608:
					num = 141;
					break;
				case 9612:
					num = 142;
					break;
				case 9616:
				case 9617:
				case 9618:
				case 9619:
					num -= 9473;
					break;
				case 9632:
					num = 148;
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
				}
			}
			bytes[byteIndex++] = (byte)num;
			charCount--;
			byteCount--;
		}
	}
}
[Serializable]
public class ENCkoi8_u : CP21866
{
}
[Serializable]
public class CP28594 : ByteEncoding
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
		'\u00a0', 'Ą', 'ĸ', 'Ŗ', '¤', 'Ĩ', 'Ļ', '§', '\u00a8', 'Š',
		'Ē', 'Ģ', 'Ŧ', '\u00ad', 'Ž', '\u00af', '°', 'ą', '\u02db', 'ŗ',
		'\u00b4', 'ĩ', 'ļ', 'ˇ', '\u00b8', 'š', 'ē', 'ģ', 'ŧ', 'Ŋ',
		'ž', 'ŋ', 'Ā', 'Á', 'Â', 'Ã', 'Ä', 'Å', 'Æ', 'Į',
		'Č', 'É', 'Ę', 'Ë', 'Ė', 'Í', 'Î', 'Ī', 'Đ', 'Ņ',
		'Ō', 'Ķ', 'Ô', 'Õ', 'Ö', '×', 'Ø', 'Ų', 'Ú', 'Û',
		'Ü', 'Ũ', 'Ū', 'ß', 'ā', 'á', 'â', 'ã', 'ä', 'å',
		'æ', 'į', 'č', 'é', 'ę', 'ë', 'ė', 'í', 'î', 'ī',
		'đ', 'ņ', 'ō', 'ķ', 'ô', 'õ', 'ö', '÷', 'ø', 'ų',
		'ú', 'û', 'ü', 'ũ', 'ū', '\u02d9'
	};

	public CP28594()
		: base(28594, ToChars, "Baltic (ISO)", "iso-8859-4", "iso-8859-4", "iso-8859-4", isBrowserDisplay: true, isBrowserSave: true, isMailNewsDisplay: true, isMailNewsSave: true, 1257)
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
				case 256:
					num = 192;
					break;
				case 257:
					num = 224;
					break;
				case 260:
					num = 161;
					break;
				case 261:
					num = 177;
					break;
				case 268:
					num = 200;
					break;
				case 269:
					num = 232;
					break;
				case 272:
					num = 208;
					break;
				case 273:
					num = 240;
					break;
				case 274:
					num = 170;
					break;
				case 275:
					num = 186;
					break;
				case 278:
					num = 204;
					break;
				case 279:
					num = 236;
					break;
				case 280:
					num = 202;
					break;
				case 281:
					num = 234;
					break;
				case 290:
					num = 171;
					break;
				case 291:
					num = 187;
					break;
				case 296:
					num = 165;
					break;
				case 297:
					num = 181;
					break;
				case 298:
					num = 207;
					break;
				case 299:
					num = 239;
					break;
				case 302:
					num = 199;
					break;
				case 303:
					num = 231;
					break;
				case 310:
					num = 211;
					break;
				case 311:
					num = 243;
					break;
				case 312:
					num = 162;
					break;
				case 315:
					num = 166;
					break;
				case 316:
					num = 182;
					break;
				case 325:
					num = 209;
					break;
				case 326:
					num = 241;
					break;
				case 330:
					num = 189;
					break;
				case 331:
					num = 191;
					break;
				case 332:
					num = 210;
					break;
				case 333:
					num = 242;
					break;
				case 342:
					num = 163;
					break;
				case 343:
					num = 179;
					break;
				case 352:
					num = 169;
					break;
				case 353:
					num = 185;
					break;
				case 358:
					num = 172;
					break;
				case 359:
					num = 188;
					break;
				case 360:
					num = 221;
					break;
				case 361:
					num = 253;
					break;
				case 362:
					num = 222;
					break;
				case 363:
					num = 254;
					break;
				case 370:
					num = 217;
					break;
				case 371:
					num = 249;
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
				case 729:
					num = 255;
					break;
				case 731:
					num = 178;
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
				case 175:
				case 176:
				case 180:
				case 184:
				case 193:
				case 194:
				case 195:
				case 196:
				case 197:
				case 198:
				case 201:
				case 203:
				case 205:
				case 206:
				case 212:
				case 213:
				case 214:
				case 215:
				case 216:
				case 218:
				case 219:
				case 220:
				case 223:
				case 225:
				case 226:
				case 227:
				case 228:
				case 229:
				case 230:
				case 233:
				case 235:
				case 237:
				case 238:
				case 244:
				case 245:
				case 246:
				case 247:
				case 248:
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
public class ENCiso_8859_4 : CP28594
{
}
[Serializable]
public class CP28595 : ByteEncoding
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
		'\u00a0', 'Ё', 'Ђ', 'Ѓ', 'Є', 'Ѕ', 'І', 'Ї', 'Ј', 'Љ',
		'Њ', 'Ћ', 'Ќ', '\u00ad', 'Ў', 'Џ', 'А', 'Б', 'В', 'Г',
		'Д', 'Е', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н',
		'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч',
		'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я', 'а', 'б',
		'в', 'г', 'д', 'е', 'ж', 'з', 'и', 'й', 'к', 'л',
		'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х',
		'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я',
		'№', 'ё', 'ђ', 'ѓ', 'є', 'ѕ', 'і', 'ї', 'ј', 'љ',
		'њ', 'ћ', 'ќ', '§', 'ў', 'џ'
	};

	public CP28595()
		: base(28595, ToChars, "Cyrillic (ISO)", "iso-8859-5", "iso-8859-5", "iso-8859-5", isBrowserDisplay: true, isBrowserSave: true, isMailNewsDisplay: true, isMailNewsSave: true, 1251)
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
				case 167:
					num = 253;
					break;
				case 1025:
				case 1026:
				case 1027:
				case 1028:
				case 1029:
				case 1030:
				case 1031:
				case 1032:
				case 1033:
				case 1034:
				case 1035:
				case 1036:
					num -= 864;
					break;
				case 1038:
				case 1039:
				case 1040:
				case 1041:
				case 1042:
				case 1043:
				case 1044:
				case 1045:
				case 1046:
				case 1047:
				case 1048:
				case 1049:
				case 1050:
				case 1051:
				case 1052:
				case 1053:
				case 1054:
				case 1055:
				case 1056:
				case 1057:
				case 1058:
				case 1059:
				case 1060:
				case 1061:
				case 1062:
				case 1063:
				case 1064:
				case 1065:
				case 1066:
				case 1067:
				case 1068:
				case 1069:
				case 1070:
				case 1071:
				case 1072:
				case 1073:
				case 1074:
				case 1075:
				case 1076:
				case 1077:
				case 1078:
				case 1079:
				case 1080:
				case 1081:
				case 1082:
				case 1083:
				case 1084:
				case 1085:
				case 1086:
				case 1087:
				case 1088:
				case 1089:
				case 1090:
				case 1091:
				case 1092:
				case 1093:
				case 1094:
				case 1095:
				case 1096:
				case 1097:
				case 1098:
				case 1099:
				case 1100:
				case 1101:
				case 1102:
				case 1103:
					num -= 864;
					break;
				case 1105:
				case 1106:
				case 1107:
				case 1108:
				case 1109:
				case 1110:
				case 1111:
				case 1112:
				case 1113:
				case 1114:
				case 1115:
				case 1116:
					num -= 864;
					break;
				case 1118:
					num = 254;
					break;
				case 1119:
					num = 255;
					break;
				case 8470:
					num = 240;
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
public class ENCiso_8859_5 : CP28595
{
}
[Serializable]
public abstract class ISCIIEncoding : MonoEncoding
{
	private int shift;

	private string encodingName;

	private string webName;

	public override string BodyName => webName;

	public override string EncodingName => encodingName;

	public override string HeaderName => webName;

	public override string WebName => webName;

	protected ISCIIEncoding(int codePage, int shift, string encodingName, string webName)
		: base(codePage)
	{
		this.shift = shift;
		this.encodingName = encodingName;
		this.webName = webName;
	}

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
		if (count < 0 || count > chars.Length - index)
		{
			throw new ArgumentOutOfRangeException("count", Strings.GetString("ArgRange_Array"));
		}
		return count;
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
		int num = 0;
		int num2 = 0;
		char c = (char)shift;
		char c2 = (char)(shift + 127);
		while (count-- > 0)
		{
			char c3 = *(char*)((byte*)chars + num++ * 2);
			num2 = ((c3 >= '\u0080') ? ((c3 >= c && c3 <= c2) ? (num2 + 1) : ((c3 < '！' || c3 > '～') ? (num2 + 1) : (num2 + 1))) : (num2 + 1));
			count--;
		}
		return num2;
	}

	public unsafe override int GetBytesImpl(char* chars, int charCount, byte* bytes, int byteCount)
	{
		EncoderFallbackBuffer buffer = null;
		int charIndex = 0;
		int num = 0;
		if (chars == null)
		{
			throw new ArgumentNullException("chars");
		}
		if (bytes == null)
		{
			throw new ArgumentNullException("bytes");
		}
		int byteIndex = num;
		char c = (char)shift;
		char c2 = (char)(shift + 127);
		while (charCount-- > 0)
		{
			char c3 = *(char*)((byte*)chars + charIndex++ * 2);
			if (c3 < '\u0080')
			{
				bytes[byteIndex++] = (byte)c3;
			}
			else if (c3 >= c && c3 <= c2)
			{
				bytes[byteIndex++] = (byte)(c3 - c + 128);
			}
			else
			{
				if (c3 < '！' || c3 > '～')
				{
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					continue;
				}
				bytes[byteIndex++] = (byte)(c3 - 65248);
			}
			byteCount--;
		}
		return byteIndex - num;
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
			throw new ArgumentException(Strings.GetString("Arg_InsufficientSpace"), "chars");
		}
		int num = byteCount;
		int num2 = shift - 128;
		while (num-- > 0)
		{
			int num3 = bytes[byteIndex++];
			if (num3 < 128)
			{
				chars[charIndex++] = (char)num3;
			}
			else
			{
				chars[charIndex++] = (char)(num3 + num2);
			}
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
}
[Serializable]
public class CP57002 : ISCIIEncoding
{
	public CP57002()
		: base(57002, 2304, "ISCII Devanagari", "x-iscii-de")
	{
	}
}
[Serializable]
public class CP57003 : ISCIIEncoding
{
	public CP57003()
		: base(57003, 2432, "ISCII Bengali", "x-iscii-be")
	{
	}
}
[Serializable]
public class CP57004 : ISCIIEncoding
{
	public CP57004()
		: base(57004, 2944, "ISCII Tamil", "x-iscii-ta")
	{
	}
}
[Serializable]
public class CP57005 : ISCIIEncoding
{
	public CP57005()
		: base(57005, 2944, "ISCII Telugu", "x-iscii-te")
	{
	}
}
[Serializable]
public class CP57006 : ISCIIEncoding
{
	public CP57006()
		: base(57006, 3456, "ISCII Assamese", "x-iscii-as")
	{
	}
}
[Serializable]
public class CP57007 : ISCIIEncoding
{
	public CP57007()
		: base(57007, 2816, "ISCII Oriya", "x-iscii-or")
	{
	}
}
[Serializable]
public class CP57008 : ISCIIEncoding
{
	public CP57008()
		: base(57008, 3200, "ISCII Kannada", "x-iscii-ka")
	{
	}
}
[Serializable]
public class CP57009 : ISCIIEncoding
{
	public CP57009()
		: base(57009, 3328, "ISCII Malayalam", "x-iscii-ma")
	{
	}
}
[Serializable]
public class CP57010 : ISCIIEncoding
{
	public CP57010()
		: base(57010, 2688, "ISCII Gujarati", "x-iscii-gu")
	{
	}
}
[Serializable]
public class CP57011 : ISCIIEncoding
{
	public CP57011()
		: base(57011, 2560, "ISCII Punjabi", "x-iscii-pa")
	{
	}
}
[Serializable]
public class ENCx_iscii_de : CP57002
{
}
[Serializable]
public class ENCx_iscii_be : CP57003
{
}
[Serializable]
public class ENCx_iscii_ta : CP57004
{
}
[Serializable]
public class ENCx_iscii_te : CP57005
{
}
[Serializable]
public class ENCx_iscii_as : CP57006
{
}
[Serializable]
public class ENCx_iscii_or : CP57007
{
}
[Serializable]
public class ENCx_iscii_ka : CP57008
{
}
[Serializable]
public class ENCx_iscii_ma : CP57009
{
}
[Serializable]
public class ENCx_iscii_gu : CP57010
{
}
[Serializable]
public class ENCx_iscii_pa : CP57011
{
}
[Serializable]
public class CP874 : ByteEncoding
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
		'x', 'y', 'z', '{', '|', '}', '~', '\u001a', '?', '?',
		'?', '?', '?', '?', '?', '?', '?', '?', '?', '?',
		'?', '?', '?', '?', '?', '?', '?', '?', '?', '?',
		'?', '?', '?', '?', '?', '?', '?', '?', '?', '?',
		'\u0e48', 'ก', 'ข', 'ฃ', 'ค', 'ฅ', 'ฆ', 'ง', 'จ', 'ฉ',
		'ช', 'ซ', 'ฌ', 'ญ', 'ฎ', 'ฏ', 'ฐ', 'ฑ', 'ฒ', 'ณ',
		'ด', 'ต', 'ถ', 'ท', 'ธ', 'น', 'บ', 'ป', 'ผ', 'ฝ',
		'พ', 'ฟ', 'ภ', 'ม', 'ย', 'ร', 'ฤ', 'ล', 'ฦ', 'ว',
		'ศ', 'ษ', 'ส', 'ห', 'ฬ', 'อ', 'ฮ', 'ฯ', 'ะ', '\u0e31',
		'า', 'ำ', '\u0e34', '\u0e35', '\u0e36', '\u0e37', '\u0e38', '\u0e39', '\u0e3a', '\u0e49',
		'\u0e4a', '\u0e4b', '\u0e4c', '฿', 'เ', 'แ', 'โ', 'ใ', 'ไ', 'ๅ',
		'ๆ', '\u0e47', '\u0e48', '\u0e49', '\u0e4a', '\u0e4b', '\u0e4c', '\u0e4d', '\u0e4e', '๏',
		'๐', '๑', '๒', '๓', '๔', '๕', '๖', '๗', '๘', '๙',
		'๚', '๛', '¢', '¬', '¦', '\u00a0'
	};

	public CP874()
		: base(874, ToChars, "Thai (Windows)", "windows-874", "windows-874", "windows-874", isBrowserDisplay: true, isBrowserSave: true, isMailNewsDisplay: true, isMailNewsSave: true, 874)
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
					num = 252;
					break;
				case 166:
					num = 254;
					break;
				case 172:
					num = 253;
					break;
				case 3585:
				case 3586:
				case 3587:
				case 3588:
				case 3589:
				case 3590:
				case 3591:
				case 3592:
				case 3593:
				case 3594:
				case 3595:
				case 3596:
				case 3597:
				case 3598:
				case 3599:
				case 3600:
				case 3601:
				case 3602:
				case 3603:
				case 3604:
				case 3605:
				case 3606:
				case 3607:
				case 3608:
				case 3609:
				case 3610:
				case 3611:
				case 3612:
				case 3613:
				case 3614:
				case 3615:
				case 3616:
				case 3617:
				case 3618:
				case 3619:
				case 3620:
				case 3621:
				case 3622:
				case 3623:
				case 3624:
				case 3625:
				case 3626:
				case 3627:
				case 3628:
				case 3629:
				case 3630:
				case 3631:
				case 3632:
				case 3633:
				case 3634:
				case 3635:
				case 3636:
				case 3637:
				case 3638:
				case 3639:
				case 3640:
				case 3641:
				case 3642:
					num -= 3424;
					break;
				case 3647:
				case 3648:
				case 3649:
				case 3650:
				case 3651:
				case 3652:
				case 3653:
				case 3654:
				case 3655:
				case 3656:
				case 3657:
				case 3658:
				case 3659:
				case 3660:
				case 3661:
				case 3662:
				case 3663:
				case 3664:
				case 3665:
				case 3666:
				case 3667:
				case 3668:
				case 3669:
				case 3670:
				case 3671:
				case 3672:
				case 3673:
				case 3674:
				case 3675:
					num -= 3424;
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
public class ENCwindows_874 : CP874
{
}
