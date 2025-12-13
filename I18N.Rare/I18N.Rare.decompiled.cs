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
namespace I18N.Rare;

[Serializable]
public class CP866 : ByteEncoding
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
		'x', 'y', 'z', '{', '|', '}', '~', '\u001a', 'А', 'Б',
		'В', 'Г', 'Д', 'Е', 'Ж', 'З', 'И', 'Й', 'К', 'Л',
		'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х',
		'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я',
		'а', 'б', 'в', 'г', 'д', 'е', 'ж', 'з', 'и', 'й',
		'к', 'л', 'м', 'н', 'о', 'п', '░', '▒', '▓', '│',
		'┤', '╡', '╢', '╖', '╕', '╣', '║', '╗', '╝', '╜',
		'╛', '┐', '└', '┴', '┬', '├', '─', '┼', '╞', '╟',
		'╚', '╔', '╩', '╦', '╠', '═', '╬', '╧', '╨', '╤',
		'╥', '╙', '╘', '╒', '╓', '╫', '╪', '┘', '┌', '█',
		'▄', '▌', '▐', '▀', 'р', 'с', 'т', 'у', 'ф', 'х',
		'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я',
		'Ё', 'ё', 'Є', 'є', 'Ї', 'ї', 'Ў', 'ў', '°', '∙',
		'·', '√', '№', '¤', '■', '\u00a0'
	};

	public CP866()
		: base(866, ToChars, "Russian (DOS)", "ibm866", "ibm866", "ibm866", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1251)
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
				case 164:
					num = 253;
					break;
				case 167:
					num = 21;
					break;
				case 176:
					num = 248;
					break;
				case 182:
					num = 20;
					break;
				case 183:
					num = 250;
					break;
				case 1025:
					num = 240;
					break;
				case 1028:
					num = 242;
					break;
				case 1031:
					num = 244;
					break;
				case 1038:
					num = 246;
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
					num -= 912;
					break;
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
					num = 241;
					break;
				case 1108:
					num = 243;
					break;
				case 1111:
					num = 245;
					break;
				case 1118:
					num = 247;
					break;
				case 8226:
					num = 7;
					break;
				case 8252:
					num = 19;
					break;
				case 8470:
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
public class ENCibm866 : CP866
{
}
[Serializable]
public class CP1026 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
		'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
		'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
		'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
		'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
		'\u0014', '\u0015', '\u009e', '\u001a', ' ', '\u00a0', 'â', 'ä', 'à', 'á',
		'ã', 'å', '{', 'ñ', 'Ç', '.', '<', '(', '+', '!',
		'&', 'é', 'ê', 'ë', 'è', 'í', 'î', 'ï', 'ì', 'ß',
		'Ğ', 'İ', '*', ')', ';', '^', '-', '/', 'Â', 'Ä',
		'À', 'Á', 'Ã', 'Å', '[', 'Ñ', 'ş', ',', '%', '_',
		'>', '?', 'ø', 'É', 'Ê', 'Ë', 'È', 'Í', 'Î', 'Ï',
		'Ì', 'ı', ':', 'Ö', 'Ş', '\'', '=', 'Ü', 'Ø', 'a',
		'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', '«', '»',
		'}', '`', '¦', '±', '°', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', 'ª', 'º', 'æ', '\u00b8', 'Æ', '¤',
		'µ', 'ö', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'¡', '¿', ']', '$', '@', '®', '¢', '£', '¥', '·',
		'©', '§', '¶', '¼', '½', '¾', '¬', '|', '\u00af', '\u00a8',
		'\u00b4', '×', 'ç', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
		'H', 'I', '\u00ad', 'ô', '~', 'ò', 'ó', 'õ', 'ğ', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', '¹', 'û',
		'\\', 'ù', 'ú', 'ÿ', 'ü', '÷', 'S', 'T', 'U', 'V',
		'W', 'X', 'Y', 'Z', '²', 'Ô', '#', 'Ò', 'Ó', 'Õ',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'³', 'Û', '"', 'Ù', 'Ú', '\u009f'
	};

	public CP1026()
		: base(1026, ToChars, "IBM EBCDIC (Turkish)", "ibm1026", "ibm1026", "ibm1026", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1254)
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
			if (num >= 4)
			{
				switch (num)
				{
				case 4:
					num = 55;
					break;
				case 5:
					num = 45;
					break;
				case 6:
					num = 46;
					break;
				case 7:
					num = 47;
					break;
				case 8:
					num = 22;
					break;
				case 9:
					num = 5;
					break;
				case 10:
					num = 37;
					break;
				case 20:
					num = 60;
					break;
				case 21:
					num = 61;
					break;
				case 22:
					num = 50;
					break;
				case 23:
					num = 38;
					break;
				case 26:
					num = 63;
					break;
				case 27:
					num = 39;
					break;
				case 32:
					num = 64;
					break;
				case 33:
					num = 79;
					break;
				case 34:
					num = 252;
					break;
				case 35:
					num = 236;
					break;
				case 36:
					num = 173;
					break;
				case 37:
					num = 108;
					break;
				case 38:
					num = 80;
					break;
				case 39:
					num = 125;
					break;
				case 40:
					num = 77;
					break;
				case 41:
					num = 93;
					break;
				case 42:
					num = 92;
					break;
				case 43:
					num = 78;
					break;
				case 44:
					num = 107;
					break;
				case 45:
					num = 96;
					break;
				case 46:
					num = 75;
					break;
				case 47:
					num = 97;
					break;
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
					num += 192;
					break;
				case 58:
					num = 122;
					break;
				case 59:
					num = 94;
					break;
				case 60:
					num = 76;
					break;
				case 61:
					num = 126;
					break;
				case 62:
					num = 110;
					break;
				case 63:
					num = 111;
					break;
				case 64:
					num = 174;
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
					num += 128;
					break;
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
					num += 135;
					break;
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
					num += 143;
					break;
				case 91:
					num = 104;
					break;
				case 92:
					num = 220;
					break;
				case 93:
					num = 172;
					break;
				case 94:
					num = 95;
					break;
				case 95:
					num = 109;
					break;
				case 96:
					num = 141;
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
					num += 32;
					break;
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
					num += 39;
					break;
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
					num += 47;
					break;
				case 123:
					num = 72;
					break;
				case 124:
					num = 187;
					break;
				case 125:
					num = 140;
					break;
				case 126:
					num = 204;
					break;
				case 127:
					num = 7;
					break;
				case 128:
				case 129:
				case 130:
				case 131:
				case 132:
					num -= 96;
					break;
				case 133:
					num = 21;
					break;
				case 134:
					num = 6;
					break;
				case 135:
					num = 23;
					break;
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
					num -= 96;
					break;
				case 141:
					num = 9;
					break;
				case 142:
					num = 10;
					break;
				case 143:
					num = 27;
					break;
				case 144:
					num = 48;
					break;
				case 145:
					num = 49;
					break;
				case 146:
					num = 26;
					break;
				case 147:
				case 148:
				case 149:
				case 150:
					num -= 96;
					break;
				case 151:
					num = 8;
					break;
				case 152:
				case 153:
				case 154:
				case 155:
					num -= 96;
					break;
				case 156:
					num = 4;
					break;
				case 157:
					num = 20;
					break;
				case 158:
					num = 62;
					break;
				case 159:
					num = 255;
					break;
				case 160:
					num = 65;
					break;
				case 161:
					num = 170;
					break;
				case 162:
					num = 176;
					break;
				case 163:
					num = 177;
					break;
				case 164:
					num = 159;
					break;
				case 165:
					num = 178;
					break;
				case 166:
					num = 142;
					break;
				case 167:
					num = 181;
					break;
				case 168:
					num = 189;
					break;
				case 169:
					num = 180;
					break;
				case 170:
					num = 154;
					break;
				case 171:
					num = 138;
					break;
				case 172:
					num = 186;
					break;
				case 173:
					num = 202;
					break;
				case 174:
					num = 175;
					break;
				case 175:
					num = 188;
					break;
				case 176:
					num = 144;
					break;
				case 177:
					num = 143;
					break;
				case 178:
					num = 234;
					break;
				case 179:
					num = 250;
					break;
				case 180:
					num = 190;
					break;
				case 181:
					num = 160;
					break;
				case 183:
					num = 179;
					break;
				case 184:
					num = 157;
					break;
				case 185:
					num = 218;
					break;
				case 186:
					num = 155;
					break;
				case 187:
					num = 139;
					break;
				case 188:
					num = 183;
					break;
				case 189:
					num = 184;
					break;
				case 190:
					num = 185;
					break;
				case 191:
					num = 171;
					break;
				case 192:
					num = 100;
					break;
				case 193:
					num = 101;
					break;
				case 194:
					num = 98;
					break;
				case 195:
					num = 102;
					break;
				case 196:
					num = 99;
					break;
				case 197:
					num = 103;
					break;
				case 198:
					num = 158;
					break;
				case 199:
					num = 74;
					break;
				case 200:
					num = 116;
					break;
				case 201:
					num = 113;
					break;
				case 202:
					num = 114;
					break;
				case 203:
					num = 115;
					break;
				case 204:
					num = 120;
					break;
				case 205:
					num = 117;
					break;
				case 206:
					num = 118;
					break;
				case 207:
					num = 119;
					break;
				case 209:
					num = 105;
					break;
				case 210:
					num = 237;
					break;
				case 211:
					num = 238;
					break;
				case 212:
					num = 235;
					break;
				case 213:
					num = 239;
					break;
				case 214:
					num = 123;
					break;
				case 215:
					num = 191;
					break;
				case 216:
					num = 128;
					break;
				case 217:
					num = 253;
					break;
				case 218:
					num = 254;
					break;
				case 219:
					num = 251;
					break;
				case 220:
					num = 127;
					break;
				case 223:
					num = 89;
					break;
				case 224:
					num = 68;
					break;
				case 225:
					num = 69;
					break;
				case 226:
					num = 66;
					break;
				case 227:
					num = 70;
					break;
				case 228:
					num = 67;
					break;
				case 229:
					num = 71;
					break;
				case 230:
					num = 156;
					break;
				case 231:
					num = 192;
					break;
				case 232:
					num = 84;
					break;
				case 233:
					num = 81;
					break;
				case 234:
					num = 82;
					break;
				case 235:
					num = 83;
					break;
				case 236:
					num = 88;
					break;
				case 237:
					num = 85;
					break;
				case 238:
					num = 86;
					break;
				case 239:
					num = 87;
					break;
				case 241:
					num = 73;
					break;
				case 242:
					num = 205;
					break;
				case 243:
					num = 206;
					break;
				case 244:
					num = 203;
					break;
				case 245:
					num = 207;
					break;
				case 246:
					num = 161;
					break;
				case 247:
					num = 225;
					break;
				case 248:
					num = 112;
					break;
				case 249:
					num = 221;
					break;
				case 250:
					num = 222;
					break;
				case 251:
					num = 219;
					break;
				case 252:
					num = 224;
					break;
				case 255:
					num = 223;
					break;
				case 286:
					num = 90;
					break;
				case 287:
					num = 208;
					break;
				case 304:
					num = 91;
					break;
				case 305:
					num = 121;
					break;
				case 350:
					num = 124;
					break;
				case 351:
					num = 106;
					break;
				case 8254:
					num = 188;
					break;
				case 65281:
					num = 79;
					break;
				case 65282:
					num = 252;
					break;
				case 65283:
					num = 236;
					break;
				case 65284:
					num = 173;
					break;
				case 65285:
					num = 108;
					break;
				case 65286:
					num = 80;
					break;
				case 65287:
					num = 125;
					break;
				case 65288:
					num = 77;
					break;
				case 65289:
					num = 93;
					break;
				case 65290:
					num = 92;
					break;
				case 65291:
					num = 78;
					break;
				case 65292:
					num = 107;
					break;
				case 65293:
					num = 96;
					break;
				case 65294:
					num = 75;
					break;
				case 65295:
					num = 97;
					break;
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
					num -= 65056;
					break;
				case 65306:
					num = 122;
					break;
				case 65307:
					num = 94;
					break;
				case 65308:
					num = 76;
					break;
				case 65309:
					num = 126;
					break;
				case 65310:
					num = 110;
					break;
				case 65311:
					num = 111;
					break;
				case 65312:
					num = 174;
					break;
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
					num -= 65120;
					break;
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
					num -= 65113;
					break;
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
					num -= 65105;
					break;
				case 65339:
					num = 104;
					break;
				case 65340:
					num = 220;
					break;
				case 65341:
					num = 172;
					break;
				case 65342:
					num = 95;
					break;
				case 65343:
					num = 109;
					break;
				case 65344:
					num = 141;
					break;
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
				case 65353:
					num -= 65216;
					break;
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
				case 65361:
				case 65362:
					num -= 65209;
					break;
				case 65363:
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
					num -= 65201;
					break;
				case 65371:
					num = 72;
					break;
				case 65372:
					num = 187;
					break;
				case 65373:
					num = 140;
					break;
				case 65374:
					num = 204;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 24:
				case 25:
				case 28:
				case 29:
				case 30:
				case 31:
				case 182:
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
public class ENCibm1026 : CP1026
{
}
[Serializable]
public class CP869 : ByteEncoding
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
		'?', '?', '?', '?', 'Ά', '?', '·', '¬', '¦', '‘',
		'’', 'Έ', '―', 'Ή', 'Ί', 'Ϊ', 'Ό', '?', '?', 'Ύ',
		'Ϋ', '©', 'Ώ', '²', '³', 'ά', '£', 'έ', 'ή', 'ί',
		'ϊ', 'ΐ', 'ό', 'ύ', 'Α', 'Β', 'Γ', 'Δ', 'Ε', 'Ζ',
		'Η', '½', 'Θ', 'Ι', '«', '»', '░', '▒', '▓', '│',
		'┤', 'Κ', 'Λ', 'Μ', 'Ν', '╣', '║', '╗', '╝', 'Ξ',
		'Ο', '┐', '└', '┴', '┬', '├', '─', '┼', 'Π', 'Ρ',
		'╚', '╔', '╩', '╦', '╠', '═', '╬', 'Σ', 'Τ', 'Υ',
		'Φ', 'Χ', 'Ψ', 'Ω', 'α', 'β', 'γ', '┘', '┌', '█',
		'▄', 'δ', 'ε', '▀', 'ζ', 'η', 'θ', 'ι', 'κ', 'λ',
		'μ', 'ν', 'ξ', 'ο', 'π', 'ρ', 'σ', 'ς', 'τ', '\u00b4',
		'\u00ad', '±', 'υ', 'φ', 'χ', '§', 'ψ', '\u0385', '°', '\u00a8',
		'ω', 'ϋ', 'ΰ', 'ώ', '■', '\u00a0'
	};

	public CP869()
		: base(869, ToChars, "Greek (DOS)", "ibm869", "ibm869", "ibm869", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1253)
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
				case 163:
					num = 156;
					break;
				case 166:
					num = 138;
					break;
				case 167:
					num = 245;
					break;
				case 168:
					num = 249;
					break;
				case 169:
					num = 151;
					break;
				case 171:
					num = 174;
					break;
				case 172:
					num = 137;
					break;
				case 173:
					num = 240;
					break;
				case 176:
					num = 248;
					break;
				case 177:
					num = 241;
					break;
				case 178:
					num = 153;
					break;
				case 179:
					num = 154;
					break;
				case 180:
					num = 239;
					break;
				case 182:
					num = 20;
					break;
				case 183:
					num = 136;
					break;
				case 187:
					num = 175;
					break;
				case 189:
					num = 171;
					break;
				case 901:
					num = 247;
					break;
				case 902:
					num = 134;
					break;
				case 903:
					num = 136;
					break;
				case 904:
					num = 141;
					break;
				case 905:
					num = 143;
					break;
				case 906:
					num = 144;
					break;
				case 908:
					num = 146;
					break;
				case 910:
					num = 149;
					break;
				case 911:
					num = 152;
					break;
				case 912:
					num = 161;
					break;
				case 913:
				case 914:
				case 915:
				case 916:
				case 917:
				case 918:
				case 919:
					num -= 749;
					break;
				case 920:
					num = 172;
					break;
				case 921:
					num = 173;
					break;
				case 922:
				case 923:
				case 924:
				case 925:
					num -= 741;
					break;
				case 926:
					num = 189;
					break;
				case 927:
					num = 190;
					break;
				case 928:
					num = 198;
					break;
				case 929:
					num = 199;
					break;
				case 931:
				case 932:
				case 933:
				case 934:
				case 935:
				case 936:
				case 937:
					num -= 724;
					break;
				case 938:
					num = 145;
					break;
				case 939:
					num = 150;
					break;
				case 940:
					num = 155;
					break;
				case 941:
					num = 157;
					break;
				case 942:
					num = 158;
					break;
				case 943:
					num = 159;
					break;
				case 944:
					num = 252;
					break;
				case 945:
					num = 214;
					break;
				case 946:
					num = 215;
					break;
				case 947:
					num = 216;
					break;
				case 948:
					num = 221;
					break;
				case 949:
					num = 222;
					break;
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
					num -= 726;
					break;
				case 962:
					num = 237;
					break;
				case 963:
					num = 236;
					break;
				case 964:
					num = 238;
					break;
				case 965:
					num = 242;
					break;
				case 966:
					num = 243;
					break;
				case 967:
					num = 244;
					break;
				case 968:
					num = 246;
					break;
				case 969:
					num = 250;
					break;
				case 970:
					num = 160;
					break;
				case 971:
					num = 251;
					break;
				case 972:
					num = 162;
					break;
				case 973:
					num = 163;
					break;
				case 974:
					num = 253;
					break;
				case 981:
					num = 243;
					break;
				case 8213:
					num = 142;
					break;
				case 8216:
					num = 139;
					break;
				case 8217:
					num = 140;
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
public class ENCibm869 : CP869
{
}
[Serializable]
public class CP870 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
		'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
		'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
		'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
		'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
		'\u0014', '\u0015', '\u009e', '\u001a', ' ', '\u00a0', 'â', 'ä', 'ţ', 'á',
		'ă', 'č', 'ç', 'ć', '[', '.', '<', '(', '+', '!',
		'&', 'é', 'ę', 'ë', 'ů', 'í', 'î', 'ľ', 'ĺ', 'ß',
		']', '$', '*', ')', ';', '^', '-', '/', 'Â', 'Ä',
		'\u02dd', 'Á', 'Ă', 'Č', 'Ç', 'Ć', '|', ',', '%', '_',
		'>', '?', 'ˇ', 'É', 'Ę', 'Ë', 'Ů', 'Í', 'Î', 'Ľ',
		'Ĺ', '`', ':', '#', '@', '\'', '=', '"', '\u02d8', 'a',
		'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'ś', 'ň',
		'đ', 'ý', 'ř', 'ş', '°', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', 'ł', 'ń', 'š', '\u00b8', '\u02db', '¤',
		'ą', '~', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'Ś', 'Ň', 'Đ', 'Ý', 'Ř', 'Ş', '\u02d9', 'Ą', 'ż', 'Ţ',
		'Ż', '§', 'ž', 'ź', 'Ž', 'Ź', 'Ł', 'Ń', 'Š', '\u00a8',
		'\u00b4', '×', '{', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
		'H', 'I', '\u00ad', 'ô', 'ö', 'ŕ', 'ó', 'ő', '}', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'Ě', 'ű',
		'ü', 'ť', 'ú', 'ě', '\\', '÷', 'S', 'T', 'U', 'V',
		'W', 'X', 'Y', 'Z', 'ď', 'Ô', 'Ö', 'Ŕ', 'Ó', 'Ő',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'Ď', 'Ű', 'Ü', 'Ť', 'Ú', '\u009f'
	};

	public CP870()
		: base(870, ToChars, "IBM EBCDIC (Latin 2)", "ibm870", "ibm870", "ibm870", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1250)
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
			if (num >= 4)
			{
				switch (num)
				{
				case 4:
					num = 55;
					break;
				case 5:
					num = 45;
					break;
				case 6:
					num = 46;
					break;
				case 7:
					num = 47;
					break;
				case 8:
					num = 22;
					break;
				case 9:
					num = 5;
					break;
				case 10:
					num = 37;
					break;
				case 20:
					num = 60;
					break;
				case 21:
					num = 61;
					break;
				case 22:
					num = 50;
					break;
				case 23:
					num = 38;
					break;
				case 26:
					num = 63;
					break;
				case 27:
					num = 39;
					break;
				case 32:
					num = 64;
					break;
				case 33:
					num = 79;
					break;
				case 34:
					num = 127;
					break;
				case 35:
					num = 123;
					break;
				case 36:
					num = 91;
					break;
				case 37:
					num = 108;
					break;
				case 38:
					num = 80;
					break;
				case 39:
					num = 125;
					break;
				case 40:
					num = 77;
					break;
				case 41:
					num = 93;
					break;
				case 42:
					num = 92;
					break;
				case 43:
					num = 78;
					break;
				case 44:
					num = 107;
					break;
				case 45:
					num = 96;
					break;
				case 46:
					num = 75;
					break;
				case 47:
					num = 97;
					break;
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
					num += 192;
					break;
				case 58:
					num = 122;
					break;
				case 59:
					num = 94;
					break;
				case 60:
					num = 76;
					break;
				case 61:
					num = 126;
					break;
				case 62:
					num = 110;
					break;
				case 63:
					num = 111;
					break;
				case 64:
					num = 124;
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
					num += 128;
					break;
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
					num += 135;
					break;
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
					num += 143;
					break;
				case 91:
					num = 74;
					break;
				case 92:
					num = 224;
					break;
				case 93:
					num = 90;
					break;
				case 94:
					num = 95;
					break;
				case 95:
					num = 109;
					break;
				case 96:
					num = 121;
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
					num += 32;
					break;
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
					num += 39;
					break;
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
					num += 47;
					break;
				case 123:
					num = 192;
					break;
				case 124:
					num = 106;
					break;
				case 125:
					num = 208;
					break;
				case 126:
					num = 161;
					break;
				case 127:
					num = 7;
					break;
				case 128:
				case 129:
				case 130:
				case 131:
				case 132:
					num -= 96;
					break;
				case 133:
					num = 21;
					break;
				case 134:
					num = 6;
					break;
				case 135:
					num = 23;
					break;
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
					num -= 96;
					break;
				case 141:
					num = 9;
					break;
				case 142:
					num = 10;
					break;
				case 143:
					num = 27;
					break;
				case 144:
					num = 48;
					break;
				case 145:
					num = 49;
					break;
				case 146:
					num = 26;
					break;
				case 147:
				case 148:
				case 149:
				case 150:
					num -= 96;
					break;
				case 151:
					num = 8;
					break;
				case 152:
				case 153:
				case 154:
				case 155:
					num -= 96;
					break;
				case 156:
					num = 4;
					break;
				case 157:
					num = 20;
					break;
				case 158:
					num = 62;
					break;
				case 159:
					num = 255;
					break;
				case 160:
					num = 65;
					break;
				case 164:
					num = 159;
					break;
				case 167:
					num = 181;
					break;
				case 168:
					num = 189;
					break;
				case 173:
					num = 202;
					break;
				case 176:
					num = 144;
					break;
				case 180:
					num = 190;
					break;
				case 184:
					num = 157;
					break;
				case 193:
					num = 101;
					break;
				case 194:
					num = 98;
					break;
				case 196:
					num = 99;
					break;
				case 199:
					num = 104;
					break;
				case 201:
					num = 113;
					break;
				case 203:
					num = 115;
					break;
				case 205:
					num = 117;
					break;
				case 206:
					num = 118;
					break;
				case 208:
					num = 172;
					break;
				case 211:
					num = 238;
					break;
				case 212:
					num = 235;
					break;
				case 214:
					num = 236;
					break;
				case 215:
					num = 191;
					break;
				case 218:
					num = 254;
					break;
				case 220:
					num = 252;
					break;
				case 221:
					num = 173;
					break;
				case 223:
					num = 89;
					break;
				case 225:
					num = 69;
					break;
				case 226:
					num = 66;
					break;
				case 228:
					num = 67;
					break;
				case 231:
					num = 72;
					break;
				case 233:
					num = 81;
					break;
				case 235:
					num = 83;
					break;
				case 237:
					num = 85;
					break;
				case 238:
					num = 86;
					break;
				case 243:
					num = 206;
					break;
				case 244:
					num = 203;
					break;
				case 246:
					num = 204;
					break;
				case 247:
					num = 225;
					break;
				case 250:
					num = 222;
					break;
				case 252:
					num = 220;
					break;
				case 253:
					num = 141;
					break;
				case 258:
					num = 102;
					break;
				case 259:
					num = 70;
					break;
				case 260:
					num = 177;
					break;
				case 261:
					num = 160;
					break;
				case 262:
					num = 105;
					break;
				case 263:
					num = 73;
					break;
				case 268:
					num = 103;
					break;
				case 269:
					num = 71;
					break;
				case 270:
					num = 250;
					break;
				case 271:
					num = 234;
					break;
				case 272:
					num = 172;
					break;
				case 273:
					num = 140;
					break;
				case 280:
					num = 114;
					break;
				case 281:
					num = 82;
					break;
				case 282:
					num = 218;
					break;
				case 283:
					num = 223;
					break;
				case 313:
					num = 120;
					break;
				case 314:
					num = 88;
					break;
				case 317:
					num = 119;
					break;
				case 318:
					num = 87;
					break;
				case 321:
					num = 186;
					break;
				case 322:
					num = 154;
					break;
				case 323:
					num = 187;
					break;
				case 324:
					num = 155;
					break;
				case 327:
					num = 171;
					break;
				case 328:
					num = 139;
					break;
				case 336:
					num = 239;
					break;
				case 337:
					num = 207;
					break;
				case 340:
					num = 237;
					break;
				case 341:
					num = 205;
					break;
				case 344:
					num = 174;
					break;
				case 345:
					num = 142;
					break;
				case 346:
					num = 170;
					break;
				case 347:
					num = 138;
					break;
				case 350:
					num = 175;
					break;
				case 351:
					num = 143;
					break;
				case 352:
					num = 188;
					break;
				case 353:
					num = 156;
					break;
				case 354:
					num = 179;
					break;
				case 355:
					num = 68;
					break;
				case 356:
					num = 253;
					break;
				case 357:
					num = 221;
					break;
				case 366:
					num = 116;
					break;
				case 367:
					num = 84;
					break;
				case 368:
					num = 251;
					break;
				case 369:
					num = 219;
					break;
				case 377:
					num = 185;
					break;
				case 378:
					num = 183;
					break;
				case 379:
					num = 180;
					break;
				case 380:
					num = 178;
					break;
				case 381:
					num = 184;
					break;
				case 382:
					num = 182;
					break;
				case 711:
					num = 112;
					break;
				case 728:
					num = 128;
					break;
				case 729:
					num = 176;
					break;
				case 731:
					num = 158;
					break;
				case 733:
					num = 100;
					break;
				case 65281:
					num = 79;
					break;
				case 65282:
					num = 127;
					break;
				case 65283:
					num = 123;
					break;
				case 65284:
					num = 91;
					break;
				case 65285:
					num = 108;
					break;
				case 65286:
					num = 80;
					break;
				case 65287:
					num = 125;
					break;
				case 65288:
					num = 77;
					break;
				case 65289:
					num = 93;
					break;
				case 65290:
					num = 92;
					break;
				case 65291:
					num = 78;
					break;
				case 65292:
					num = 107;
					break;
				case 65293:
					num = 96;
					break;
				case 65294:
					num = 75;
					break;
				case 65295:
					num = 97;
					break;
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
					num -= 65056;
					break;
				case 65306:
					num = 122;
					break;
				case 65307:
					num = 94;
					break;
				case 65308:
					num = 76;
					break;
				case 65309:
					num = 126;
					break;
				case 65310:
					num = 110;
					break;
				case 65311:
					num = 111;
					break;
				case 65312:
					num = 124;
					break;
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
					num -= 65120;
					break;
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
					num -= 65113;
					break;
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
					num -= 65105;
					break;
				case 65339:
					num = 74;
					break;
				case 65340:
					num = 224;
					break;
				case 65341:
					num = 90;
					break;
				case 65342:
					num = 95;
					break;
				case 65343:
					num = 109;
					break;
				case 65344:
					num = 121;
					break;
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
				case 65353:
					num -= 65216;
					break;
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
				case 65361:
				case 65362:
					num -= 65209;
					break;
				case 65363:
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
					num -= 65201;
					break;
				case 65371:
					num = 192;
					break;
				case 65372:
					num = 106;
					break;
				case 65373:
					num = 208;
					break;
				case 65374:
					num = 161;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 24:
				case 25:
				case 28:
				case 29:
				case 30:
				case 31:
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
public class ENCibm870 : CP870
{
}
[Serializable]
public class CP875 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
		'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
		'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
		'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
		'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
		'\u0014', '\u0015', '\u009e', '\u001a', ' ', 'Α', 'Β', 'Γ', 'Δ', 'Ε',
		'Ζ', 'Η', 'Θ', 'Ι', '[', '.', '<', '(', '+', '!',
		'&', 'Κ', 'Λ', 'Μ', 'Ν', 'Ξ', 'Ο', 'Π', 'Ρ', 'Σ',
		']', '$', '*', ')', ';', '^', '-', '/', 'Τ', 'Υ',
		'Φ', 'Χ', 'Ψ', 'Ω', 'Ϊ', 'Ϋ', '|', ',', '%', '_',
		'>', '?', '\u00a8', 'Ά', 'Έ', 'Ή', '\u00a0', 'Ί', 'Ό', 'Ύ',
		'Ώ', '`', ':', '#', '@', '\'', '=', '"', '\u0385', 'a',
		'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'α', 'β',
		'γ', 'δ', 'ε', 'ζ', '°', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', 'η', 'θ', 'ι', 'κ', 'λ', 'μ',
		'\u00b4', '~', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'ν', 'ξ', 'ο', 'π', 'ρ', 'σ', '£', 'ά', 'έ', 'ή',
		'ϊ', 'ί', 'ό', 'ύ', 'ϋ', 'ώ', 'ς', 'τ', 'υ', 'φ',
		'χ', 'ψ', '{', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
		'H', 'I', '\u00ad', 'ω', 'ΐ', 'ΰ', '‘', '―', '}', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', '±', '½',
		'?', '·', '’', '¦', '\\', '?', 'S', 'T', 'U', 'V',
		'W', 'X', 'Y', 'Z', '²', '§', '?', '?', '«', '¬',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'³', '©', '?', '?', '»', '\u009f'
	};

	public CP875()
		: base(875, ToChars, "IBM EBCDIC (Greek)", "ibm875", "ibm875", "ibm875", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1253)
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
			if (num >= 4)
			{
				switch (num)
				{
				case 4:
					num = 55;
					break;
				case 5:
					num = 45;
					break;
				case 6:
					num = 46;
					break;
				case 7:
					num = 47;
					break;
				case 8:
					num = 22;
					break;
				case 9:
					num = 5;
					break;
				case 10:
					num = 37;
					break;
				case 20:
					num = 60;
					break;
				case 21:
					num = 61;
					break;
				case 22:
					num = 50;
					break;
				case 23:
					num = 38;
					break;
				case 26:
					num = 63;
					break;
				case 27:
					num = 39;
					break;
				case 32:
					num = 64;
					break;
				case 33:
					num = 79;
					break;
				case 34:
					num = 127;
					break;
				case 35:
					num = 123;
					break;
				case 36:
					num = 91;
					break;
				case 37:
					num = 108;
					break;
				case 38:
					num = 80;
					break;
				case 39:
					num = 125;
					break;
				case 40:
					num = 77;
					break;
				case 41:
					num = 93;
					break;
				case 42:
					num = 92;
					break;
				case 43:
					num = 78;
					break;
				case 44:
					num = 107;
					break;
				case 45:
					num = 96;
					break;
				case 46:
					num = 75;
					break;
				case 47:
					num = 97;
					break;
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
					num += 192;
					break;
				case 58:
					num = 122;
					break;
				case 59:
					num = 94;
					break;
				case 60:
					num = 76;
					break;
				case 61:
					num = 126;
					break;
				case 62:
					num = 110;
					break;
				case 63:
					num = 111;
					break;
				case 64:
					num = 124;
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
					num += 128;
					break;
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
					num += 135;
					break;
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
					num += 143;
					break;
				case 91:
					num = 74;
					break;
				case 92:
					num = 224;
					break;
				case 93:
					num = 90;
					break;
				case 94:
					num = 95;
					break;
				case 95:
					num = 109;
					break;
				case 96:
					num = 121;
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
					num += 32;
					break;
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
					num += 39;
					break;
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
					num += 47;
					break;
				case 123:
					num = 192;
					break;
				case 124:
					num = 106;
					break;
				case 125:
					num = 208;
					break;
				case 126:
					num = 161;
					break;
				case 127:
					num = 7;
					break;
				case 128:
				case 129:
				case 130:
				case 131:
				case 132:
					num -= 96;
					break;
				case 133:
					num = 21;
					break;
				case 134:
					num = 6;
					break;
				case 135:
					num = 23;
					break;
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
					num -= 96;
					break;
				case 141:
					num = 9;
					break;
				case 142:
					num = 10;
					break;
				case 143:
					num = 27;
					break;
				case 144:
					num = 48;
					break;
				case 145:
					num = 49;
					break;
				case 146:
					num = 26;
					break;
				case 147:
				case 148:
				case 149:
				case 150:
					num -= 96;
					break;
				case 151:
					num = 8;
					break;
				case 152:
				case 153:
				case 154:
				case 155:
					num -= 96;
					break;
				case 156:
					num = 4;
					break;
				case 157:
					num = 20;
					break;
				case 158:
					num = 62;
					break;
				case 159:
					num = 255;
					break;
				case 160:
					num = 116;
					break;
				case 163:
					num = 176;
					break;
				case 166:
					num = 223;
					break;
				case 167:
					num = 235;
					break;
				case 168:
					num = 112;
					break;
				case 169:
					num = 251;
					break;
				case 171:
					num = 238;
					break;
				case 172:
					num = 239;
					break;
				case 173:
					num = 202;
					break;
				case 176:
					num = 144;
					break;
				case 177:
					num = 218;
					break;
				case 178:
					num = 234;
					break;
				case 179:
					num = 250;
					break;
				case 180:
					num = 160;
					break;
				case 183:
					num = 221;
					break;
				case 187:
					num = 254;
					break;
				case 189:
					num = 219;
					break;
				case 901:
					num = 128;
					break;
				case 902:
					num = 113;
					break;
				case 903:
					num = 221;
					break;
				case 904:
					num = 114;
					break;
				case 905:
					num = 115;
					break;
				case 906:
					num = 117;
					break;
				case 908:
					num = 118;
					break;
				case 910:
					num = 119;
					break;
				case 911:
					num = 120;
					break;
				case 912:
					num = 204;
					break;
				case 913:
				case 914:
				case 915:
				case 916:
				case 917:
				case 918:
				case 919:
				case 920:
				case 921:
					num -= 848;
					break;
				case 922:
				case 923:
				case 924:
				case 925:
				case 926:
				case 927:
				case 928:
				case 929:
					num -= 841;
					break;
				case 931:
					num = 89;
					break;
				case 932:
				case 933:
				case 934:
				case 935:
				case 936:
				case 937:
				case 938:
				case 939:
					num -= 834;
					break;
				case 940:
					num = 177;
					break;
				case 941:
					num = 178;
					break;
				case 942:
					num = 179;
					break;
				case 943:
					num = 181;
					break;
				case 944:
					num = 205;
					break;
				case 945:
				case 946:
				case 947:
				case 948:
				case 949:
				case 950:
					num -= 807;
					break;
				case 951:
				case 952:
				case 953:
				case 954:
				case 955:
				case 956:
					num -= 797;
					break;
				case 957:
				case 958:
				case 959:
				case 960:
				case 961:
					num -= 787;
					break;
				case 962:
					num = 186;
					break;
				case 963:
					num = 175;
					break;
				case 964:
				case 965:
				case 966:
				case 967:
				case 968:
					num -= 777;
					break;
				case 969:
					num = 203;
					break;
				case 970:
					num = 180;
					break;
				case 971:
					num = 184;
					break;
				case 972:
					num = 182;
					break;
				case 973:
					num = 183;
					break;
				case 974:
					num = 185;
					break;
				case 981:
					num = 189;
					break;
				case 8213:
					num = 207;
					break;
				case 8216:
					num = 206;
					break;
				case 8217:
					num = 222;
					break;
				case 65281:
					num = 79;
					break;
				case 65282:
					num = 127;
					break;
				case 65283:
					num = 123;
					break;
				case 65284:
					num = 91;
					break;
				case 65285:
					num = 108;
					break;
				case 65286:
					num = 80;
					break;
				case 65287:
					num = 125;
					break;
				case 65288:
					num = 77;
					break;
				case 65289:
					num = 93;
					break;
				case 65290:
					num = 92;
					break;
				case 65291:
					num = 78;
					break;
				case 65292:
					num = 107;
					break;
				case 65293:
					num = 96;
					break;
				case 65294:
					num = 75;
					break;
				case 65295:
					num = 97;
					break;
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
					num -= 65056;
					break;
				case 65306:
					num = 122;
					break;
				case 65307:
					num = 94;
					break;
				case 65308:
					num = 76;
					break;
				case 65309:
					num = 126;
					break;
				case 65310:
					num = 110;
					break;
				case 65311:
					num = 111;
					break;
				case 65312:
					num = 124;
					break;
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
					num -= 65120;
					break;
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
					num -= 65113;
					break;
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
					num -= 65105;
					break;
				case 65339:
					num = 74;
					break;
				case 65340:
					num = 224;
					break;
				case 65341:
					num = 90;
					break;
				case 65342:
					num = 95;
					break;
				case 65343:
					num = 109;
					break;
				case 65344:
					num = 121;
					break;
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
				case 65353:
					num -= 65216;
					break;
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
				case 65361:
				case 65362:
					num -= 65209;
					break;
				case 65363:
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
					num -= 65201;
					break;
				case 65371:
					num = 192;
					break;
				case 65372:
					num = 106;
					break;
				case 65373:
					num = 208;
					break;
				case 65374:
					num = 161;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 24:
				case 25:
				case 28:
				case 29:
				case 30:
				case 31:
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
public class ENCibm875 : CP875
{
}
[Serializable]
public class CP1047 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
		'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
		'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
		'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
		'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
		'\u0014', '\u0015', '\u009e', '\u001a', ' ', '\u00a0', 'â', 'ä', 'à', 'á',
		'ã', 'å', 'ç', 'ñ', '¢', '.', '<', '(', '+', '|',
		'&', 'é', 'ê', 'ë', 'è', 'í', 'î', 'ï', 'ì', 'ß',
		'!', '$', '*', ')', ';', '^', '-', '/', 'Â', 'Ä',
		'À', 'Á', 'Ã', 'Å', 'Ç', 'Ñ', '¦', ',', '%', '_',
		'>', '?', 'ø', 'É', 'Ê', 'Ë', 'È', 'Í', 'Î', 'Ï',
		'Ì', '`', ':', '#', '@', '\'', '=', '"', 'Ø', 'a',
		'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', '«', '»',
		'ð', 'ý', 'þ', '±', '°', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', 'ª', 'º', 'æ', '\u00b8', 'Æ', '¤',
		'µ', '~', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'¡', '¿', 'Ð', '[', 'Þ', '®', '¬', '£', '¥', '·',
		'©', '§', '¶', '¼', '½', '¾', 'Ý', '\u00a8', '\u00af', ']',
		'\u00b4', '×', '{', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
		'H', 'I', '\u00ad', 'ô', 'ö', 'ò', 'ó', 'õ', '}', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', '¹', 'û',
		'ü', 'ù', 'ú', 'ÿ', '\\', '÷', 'S', 'T', 'U', 'V',
		'W', 'X', 'Y', 'Z', '²', 'Ô', 'Ö', 'Ò', 'Ó', 'Õ',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'³', 'Û', 'Ü', 'Ù', 'Ú', '\u009f'
	};

	public CP1047()
		: base(1047, ToChars, "IBM EBCDIC (Open Systems Latin 1)", "ibm1047", "ibm1047", "ibm1047", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
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
			if (num >= 4)
			{
				switch (num)
				{
				case 4:
					num = 55;
					break;
				case 5:
					num = 45;
					break;
				case 6:
					num = 46;
					break;
				case 7:
					num = 47;
					break;
				case 8:
					num = 22;
					break;
				case 9:
					num = 5;
					break;
				case 10:
					num = 37;
					break;
				case 20:
					num = 60;
					break;
				case 21:
					num = 61;
					break;
				case 22:
					num = 50;
					break;
				case 23:
					num = 38;
					break;
				case 26:
					num = 63;
					break;
				case 27:
					num = 39;
					break;
				case 32:
					num = 64;
					break;
				case 33:
					num = 90;
					break;
				case 34:
					num = 127;
					break;
				case 35:
					num = 123;
					break;
				case 36:
					num = 91;
					break;
				case 37:
					num = 108;
					break;
				case 38:
					num = 80;
					break;
				case 39:
					num = 125;
					break;
				case 40:
					num = 77;
					break;
				case 41:
					num = 93;
					break;
				case 42:
					num = 92;
					break;
				case 43:
					num = 78;
					break;
				case 44:
					num = 107;
					break;
				case 45:
					num = 96;
					break;
				case 46:
					num = 75;
					break;
				case 47:
					num = 97;
					break;
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
					num += 192;
					break;
				case 58:
					num = 122;
					break;
				case 59:
					num = 94;
					break;
				case 60:
					num = 76;
					break;
				case 61:
					num = 126;
					break;
				case 62:
					num = 110;
					break;
				case 63:
					num = 111;
					break;
				case 64:
					num = 124;
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
					num += 128;
					break;
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
					num += 135;
					break;
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
					num += 143;
					break;
				case 91:
					num = 173;
					break;
				case 92:
					num = 224;
					break;
				case 93:
					num = 189;
					break;
				case 94:
					num = 95;
					break;
				case 95:
					num = 109;
					break;
				case 96:
					num = 121;
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
					num += 32;
					break;
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
					num += 39;
					break;
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
					num += 47;
					break;
				case 123:
					num = 192;
					break;
				case 124:
					num = 79;
					break;
				case 125:
					num = 208;
					break;
				case 126:
					num = 161;
					break;
				case 127:
					num = 7;
					break;
				case 128:
				case 129:
				case 130:
				case 131:
				case 132:
					num -= 96;
					break;
				case 133:
					num = 21;
					break;
				case 134:
					num = 6;
					break;
				case 135:
					num = 23;
					break;
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
					num -= 96;
					break;
				case 141:
					num = 9;
					break;
				case 142:
					num = 10;
					break;
				case 143:
					num = 27;
					break;
				case 144:
					num = 48;
					break;
				case 145:
					num = 49;
					break;
				case 146:
					num = 26;
					break;
				case 147:
				case 148:
				case 149:
				case 150:
					num -= 96;
					break;
				case 151:
					num = 8;
					break;
				case 152:
				case 153:
				case 154:
				case 155:
					num -= 96;
					break;
				case 156:
					num = 4;
					break;
				case 157:
					num = 20;
					break;
				case 158:
					num = 62;
					break;
				case 159:
					num = 255;
					break;
				case 160:
					num = 65;
					break;
				case 161:
					num = 170;
					break;
				case 162:
					num = 74;
					break;
				case 163:
					num = 177;
					break;
				case 164:
					num = 159;
					break;
				case 165:
					num = 178;
					break;
				case 166:
					num = 106;
					break;
				case 167:
					num = 181;
					break;
				case 168:
					num = 187;
					break;
				case 169:
					num = 180;
					break;
				case 170:
					num = 154;
					break;
				case 171:
					num = 138;
					break;
				case 172:
					num = 176;
					break;
				case 173:
					num = 202;
					break;
				case 174:
					num = 175;
					break;
				case 175:
					num = 188;
					break;
				case 176:
					num = 144;
					break;
				case 177:
					num = 143;
					break;
				case 178:
					num = 234;
					break;
				case 179:
					num = 250;
					break;
				case 180:
					num = 190;
					break;
				case 181:
					num = 160;
					break;
				case 183:
					num = 179;
					break;
				case 184:
					num = 157;
					break;
				case 185:
					num = 218;
					break;
				case 186:
					num = 155;
					break;
				case 187:
					num = 139;
					break;
				case 188:
					num = 183;
					break;
				case 189:
					num = 184;
					break;
				case 190:
					num = 185;
					break;
				case 191:
					num = 171;
					break;
				case 192:
					num = 100;
					break;
				case 193:
					num = 101;
					break;
				case 194:
					num = 98;
					break;
				case 195:
					num = 102;
					break;
				case 196:
					num = 99;
					break;
				case 197:
					num = 103;
					break;
				case 198:
					num = 158;
					break;
				case 199:
					num = 104;
					break;
				case 200:
					num = 116;
					break;
				case 201:
					num = 113;
					break;
				case 202:
					num = 114;
					break;
				case 203:
					num = 115;
					break;
				case 204:
					num = 120;
					break;
				case 205:
					num = 117;
					break;
				case 206:
					num = 118;
					break;
				case 207:
					num = 119;
					break;
				case 208:
					num = 172;
					break;
				case 209:
					num = 105;
					break;
				case 210:
					num = 237;
					break;
				case 211:
					num = 238;
					break;
				case 212:
					num = 235;
					break;
				case 213:
					num = 239;
					break;
				case 214:
					num = 236;
					break;
				case 215:
					num = 191;
					break;
				case 216:
					num = 128;
					break;
				case 217:
					num = 253;
					break;
				case 218:
					num = 254;
					break;
				case 219:
					num = 251;
					break;
				case 220:
					num = 252;
					break;
				case 221:
					num = 186;
					break;
				case 222:
					num = 174;
					break;
				case 223:
					num = 89;
					break;
				case 224:
					num = 68;
					break;
				case 225:
					num = 69;
					break;
				case 226:
					num = 66;
					break;
				case 227:
					num = 70;
					break;
				case 228:
					num = 67;
					break;
				case 229:
					num = 71;
					break;
				case 230:
					num = 156;
					break;
				case 231:
					num = 72;
					break;
				case 232:
					num = 84;
					break;
				case 233:
					num = 81;
					break;
				case 234:
					num = 82;
					break;
				case 235:
					num = 83;
					break;
				case 236:
					num = 88;
					break;
				case 237:
					num = 85;
					break;
				case 238:
					num = 86;
					break;
				case 239:
					num = 87;
					break;
				case 240:
					num = 140;
					break;
				case 241:
					num = 73;
					break;
				case 242:
					num = 205;
					break;
				case 243:
					num = 206;
					break;
				case 244:
					num = 203;
					break;
				case 245:
					num = 207;
					break;
				case 246:
					num = 204;
					break;
				case 247:
					num = 225;
					break;
				case 248:
					num = 112;
					break;
				case 249:
					num = 221;
					break;
				case 250:
					num = 222;
					break;
				case 251:
					num = 219;
					break;
				case 252:
					num = 220;
					break;
				case 253:
					num = 141;
					break;
				case 254:
					num = 142;
					break;
				case 255:
					num = 223;
					break;
				case 8254:
					num = 188;
					break;
				case 65281:
					num = 90;
					break;
				case 65282:
					num = 127;
					break;
				case 65283:
					num = 123;
					break;
				case 65284:
					num = 91;
					break;
				case 65285:
					num = 108;
					break;
				case 65286:
					num = 80;
					break;
				case 65287:
					num = 125;
					break;
				case 65288:
					num = 77;
					break;
				case 65289:
					num = 93;
					break;
				case 65290:
					num = 92;
					break;
				case 65291:
					num = 78;
					break;
				case 65292:
					num = 107;
					break;
				case 65293:
					num = 96;
					break;
				case 65294:
					num = 75;
					break;
				case 65295:
					num = 97;
					break;
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
					num -= 65056;
					break;
				case 65306:
					num = 122;
					break;
				case 65307:
					num = 94;
					break;
				case 65308:
					num = 76;
					break;
				case 65309:
					num = 126;
					break;
				case 65310:
					num = 110;
					break;
				case 65311:
					num = 111;
					break;
				case 65312:
					num = 124;
					break;
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
					num -= 65120;
					break;
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
					num -= 65113;
					break;
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
					num -= 65105;
					break;
				case 65339:
					num = 173;
					break;
				case 65340:
					num = 224;
					break;
				case 65341:
					num = 189;
					break;
				case 65342:
					num = 95;
					break;
				case 65343:
					num = 109;
					break;
				case 65344:
					num = 121;
					break;
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
				case 65353:
					num -= 65216;
					break;
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
				case 65361:
				case 65362:
					num -= 65209;
					break;
				case 65363:
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
					num -= 65201;
					break;
				case 65371:
					num = 192;
					break;
				case 65372:
					num = 79;
					break;
				case 65373:
					num = 208;
					break;
				case 65374:
					num = 161;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 24:
				case 25:
				case 28:
				case 29:
				case 30:
				case 31:
				case 182:
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
public class ENCibm1047 : CP1047
{
}
[Serializable]
public class CP1140 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
		'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
		'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
		'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
		'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
		'\u0014', '\u0015', '\u009e', '\u001a', ' ', '\u00a0', 'â', 'ä', 'à', 'á',
		'ã', 'å', 'ç', 'ñ', '¢', '.', '<', '(', '+', '|',
		'&', 'é', 'ê', 'ë', 'è', 'í', 'î', 'ï', 'ì', 'ß',
		'!', '$', '*', ')', ';', '¬', '-', '/', 'Â', 'Ä',
		'À', 'Á', 'Ã', 'Å', 'Ç', 'Ñ', '¦', ',', '%', '_',
		'>', '?', 'ø', 'É', 'Ê', 'Ë', 'È', 'Í', 'Î', 'Ï',
		'Ì', '`', ':', '#', '@', '\'', '=', '"', 'Ø', 'a',
		'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', '«', '»',
		'ð', 'ý', 'þ', '±', '°', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', 'ª', 'º', 'æ', '\u00b8', 'Æ', '€',
		'µ', '~', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'¡', '¿', 'Ð', 'Ý', 'Þ', '®', '^', '£', '¥', '·',
		'©', '§', '¶', '¼', '½', '¾', '[', ']', '\u00af', '\u00a8',
		'\u00b4', '×', '{', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
		'H', 'I', '\u00ad', 'ô', 'ö', 'ò', 'ó', 'õ', '}', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', '¹', 'û',
		'ü', 'ù', 'ú', 'ÿ', '\\', '÷', 'S', 'T', 'U', 'V',
		'W', 'X', 'Y', 'Z', '²', 'Ô', 'Ö', 'Ò', 'Ó', 'Õ',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'³', 'Û', 'Ü', 'Ù', 'Ú', '\u009f'
	};

	public CP1140()
		: base(1140, ToChars, "IBM EBCDIC (US-Canada with Euro)", "IBM01140", "IBM01140", "IBM01140", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
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
			if (num >= 4)
			{
				switch (num)
				{
				case 4:
					num = 55;
					break;
				case 5:
					num = 45;
					break;
				case 6:
					num = 46;
					break;
				case 7:
					num = 47;
					break;
				case 8:
					num = 22;
					break;
				case 9:
					num = 5;
					break;
				case 10:
					num = 37;
					break;
				case 20:
					num = 60;
					break;
				case 21:
					num = 61;
					break;
				case 22:
					num = 50;
					break;
				case 23:
					num = 38;
					break;
				case 26:
					num = 63;
					break;
				case 27:
					num = 39;
					break;
				case 32:
					num = 64;
					break;
				case 33:
					num = 90;
					break;
				case 34:
					num = 127;
					break;
				case 35:
					num = 123;
					break;
				case 36:
					num = 91;
					break;
				case 37:
					num = 108;
					break;
				case 38:
					num = 80;
					break;
				case 39:
					num = 125;
					break;
				case 40:
					num = 77;
					break;
				case 41:
					num = 93;
					break;
				case 42:
					num = 92;
					break;
				case 43:
					num = 78;
					break;
				case 44:
					num = 107;
					break;
				case 45:
					num = 96;
					break;
				case 46:
					num = 75;
					break;
				case 47:
					num = 97;
					break;
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
					num += 192;
					break;
				case 58:
					num = 122;
					break;
				case 59:
					num = 94;
					break;
				case 60:
					num = 76;
					break;
				case 61:
					num = 126;
					break;
				case 62:
					num = 110;
					break;
				case 63:
					num = 111;
					break;
				case 64:
					num = 124;
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
					num += 128;
					break;
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
					num += 135;
					break;
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
					num += 143;
					break;
				case 91:
					num = 186;
					break;
				case 92:
					num = 224;
					break;
				case 93:
					num = 187;
					break;
				case 94:
					num = 176;
					break;
				case 95:
					num = 109;
					break;
				case 96:
					num = 121;
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
					num += 32;
					break;
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
					num += 39;
					break;
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
					num += 47;
					break;
				case 123:
					num = 192;
					break;
				case 124:
					num = 79;
					break;
				case 125:
					num = 208;
					break;
				case 126:
					num = 161;
					break;
				case 127:
					num = 7;
					break;
				case 128:
				case 129:
				case 130:
				case 131:
				case 132:
					num -= 96;
					break;
				case 133:
					num = 21;
					break;
				case 134:
					num = 6;
					break;
				case 135:
					num = 23;
					break;
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
					num -= 96;
					break;
				case 141:
					num = 9;
					break;
				case 142:
					num = 10;
					break;
				case 143:
					num = 27;
					break;
				case 144:
					num = 48;
					break;
				case 145:
					num = 49;
					break;
				case 146:
					num = 26;
					break;
				case 147:
				case 148:
				case 149:
				case 150:
					num -= 96;
					break;
				case 151:
					num = 8;
					break;
				case 152:
				case 153:
				case 154:
				case 155:
					num -= 96;
					break;
				case 156:
					num = 4;
					break;
				case 157:
					num = 20;
					break;
				case 158:
					num = 62;
					break;
				case 159:
					num = 255;
					break;
				case 160:
					num = 65;
					break;
				case 161:
					num = 170;
					break;
				case 162:
					num = 74;
					break;
				case 163:
					num = 177;
					break;
				case 165:
					num = 178;
					break;
				case 166:
					num = 106;
					break;
				case 167:
					num = 181;
					break;
				case 168:
					num = 189;
					break;
				case 169:
					num = 180;
					break;
				case 170:
					num = 154;
					break;
				case 171:
					num = 138;
					break;
				case 172:
					num = 95;
					break;
				case 173:
					num = 202;
					break;
				case 174:
					num = 175;
					break;
				case 175:
					num = 188;
					break;
				case 176:
					num = 144;
					break;
				case 177:
					num = 143;
					break;
				case 178:
					num = 234;
					break;
				case 179:
					num = 250;
					break;
				case 180:
					num = 190;
					break;
				case 181:
					num = 160;
					break;
				case 183:
					num = 179;
					break;
				case 184:
					num = 157;
					break;
				case 185:
					num = 218;
					break;
				case 186:
					num = 155;
					break;
				case 187:
					num = 139;
					break;
				case 188:
					num = 183;
					break;
				case 189:
					num = 184;
					break;
				case 190:
					num = 185;
					break;
				case 191:
					num = 171;
					break;
				case 192:
					num = 100;
					break;
				case 193:
					num = 101;
					break;
				case 194:
					num = 98;
					break;
				case 195:
					num = 102;
					break;
				case 196:
					num = 99;
					break;
				case 197:
					num = 103;
					break;
				case 198:
					num = 158;
					break;
				case 199:
					num = 104;
					break;
				case 200:
					num = 116;
					break;
				case 201:
					num = 113;
					break;
				case 202:
					num = 114;
					break;
				case 203:
					num = 115;
					break;
				case 204:
					num = 120;
					break;
				case 205:
					num = 117;
					break;
				case 206:
					num = 118;
					break;
				case 207:
					num = 119;
					break;
				case 208:
					num = 172;
					break;
				case 209:
					num = 105;
					break;
				case 210:
					num = 237;
					break;
				case 211:
					num = 238;
					break;
				case 212:
					num = 235;
					break;
				case 213:
					num = 239;
					break;
				case 214:
					num = 236;
					break;
				case 215:
					num = 191;
					break;
				case 216:
					num = 128;
					break;
				case 217:
					num = 253;
					break;
				case 218:
					num = 254;
					break;
				case 219:
					num = 251;
					break;
				case 220:
					num = 252;
					break;
				case 221:
					num = 173;
					break;
				case 222:
					num = 174;
					break;
				case 223:
					num = 89;
					break;
				case 224:
					num = 68;
					break;
				case 225:
					num = 69;
					break;
				case 226:
					num = 66;
					break;
				case 227:
					num = 70;
					break;
				case 228:
					num = 67;
					break;
				case 229:
					num = 71;
					break;
				case 230:
					num = 156;
					break;
				case 231:
					num = 72;
					break;
				case 232:
					num = 84;
					break;
				case 233:
					num = 81;
					break;
				case 234:
					num = 82;
					break;
				case 235:
					num = 83;
					break;
				case 236:
					num = 88;
					break;
				case 237:
					num = 85;
					break;
				case 238:
					num = 86;
					break;
				case 239:
					num = 87;
					break;
				case 240:
					num = 140;
					break;
				case 241:
					num = 73;
					break;
				case 242:
					num = 205;
					break;
				case 243:
					num = 206;
					break;
				case 244:
					num = 203;
					break;
				case 245:
					num = 207;
					break;
				case 246:
					num = 204;
					break;
				case 247:
					num = 225;
					break;
				case 248:
					num = 112;
					break;
				case 249:
					num = 221;
					break;
				case 250:
					num = 222;
					break;
				case 251:
					num = 219;
					break;
				case 252:
					num = 220;
					break;
				case 253:
					num = 141;
					break;
				case 254:
					num = 142;
					break;
				case 255:
					num = 223;
					break;
				case 8254:
					num = 188;
					break;
				case 8364:
					num = 159;
					break;
				case 65281:
					num = 90;
					break;
				case 65282:
					num = 127;
					break;
				case 65283:
					num = 123;
					break;
				case 65284:
					num = 91;
					break;
				case 65285:
					num = 108;
					break;
				case 65286:
					num = 80;
					break;
				case 65287:
					num = 125;
					break;
				case 65288:
					num = 77;
					break;
				case 65289:
					num = 93;
					break;
				case 65290:
					num = 92;
					break;
				case 65291:
					num = 78;
					break;
				case 65292:
					num = 107;
					break;
				case 65293:
					num = 96;
					break;
				case 65294:
					num = 75;
					break;
				case 65295:
					num = 97;
					break;
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
					num -= 65056;
					break;
				case 65306:
					num = 122;
					break;
				case 65307:
					num = 94;
					break;
				case 65308:
					num = 76;
					break;
				case 65309:
					num = 126;
					break;
				case 65310:
					num = 110;
					break;
				case 65311:
					num = 111;
					break;
				case 65312:
					num = 124;
					break;
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
					num -= 65120;
					break;
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
					num -= 65113;
					break;
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
					num -= 65105;
					break;
				case 65339:
					num = 186;
					break;
				case 65340:
					num = 224;
					break;
				case 65341:
					num = 187;
					break;
				case 65342:
					num = 176;
					break;
				case 65343:
					num = 109;
					break;
				case 65344:
					num = 121;
					break;
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
				case 65353:
					num -= 65216;
					break;
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
				case 65361:
				case 65362:
					num -= 65209;
					break;
				case 65363:
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
					num -= 65201;
					break;
				case 65371:
					num = 192;
					break;
				case 65372:
					num = 79;
					break;
				case 65373:
					num = 208;
					break;
				case 65374:
					num = 161;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 24:
				case 25:
				case 28:
				case 29:
				case 30:
				case 31:
				case 182:
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
public class ENCibm01140 : CP1140
{
}
[Serializable]
public class CP1141 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
		'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
		'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
		'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
		'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
		'\u0014', '\u0015', '\u009e', '\u001a', ' ', '\u00a0', 'â', '{', 'à', 'á',
		'ã', 'å', 'ç', 'ñ', 'Ä', '.', '<', '(', '+', '!',
		'&', 'é', 'ê', 'ë', 'è', 'í', 'î', 'ï', 'ì', '~',
		'Ü', '$', '*', ')', ';', '^', '-', '/', 'Â', '[',
		'À', 'Á', 'Ã', 'Å', 'Ç', 'Ñ', 'ö', ',', '%', '_',
		'>', '?', 'ø', 'É', 'Ê', 'Ë', 'È', 'Í', 'Î', 'Ï',
		'Ì', '`', ':', '#', '§', '\'', '=', '"', 'Ø', 'a',
		'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', '«', '»',
		'ð', 'ý', 'þ', '±', '°', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', 'ª', 'º', 'æ', '\u00b8', 'Æ', '€',
		'µ', 'ß', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'¡', '¿', 'Ð', 'Ý', 'Þ', '®', '¢', '£', '¥', '·',
		'©', '@', '¶', '¼', '½', '¾', '¬', '|', '\u00af', '\u00a8',
		'\u00b4', '×', 'ä', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
		'H', 'I', '\u00ad', 'ô', '¦', 'ò', 'ó', 'õ', 'ü', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', '¹', 'û',
		'}', 'ù', 'ú', 'ÿ', 'Ö', '÷', 'S', 'T', 'U', 'V',
		'W', 'X', 'Y', 'Z', '²', 'Ô', '\\', 'Ò', 'Ó', 'Õ',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'³', 'Û', ']', 'Ù', 'Ú', '\u009f'
	};

	public CP1141()
		: base(1141, ToChars, "IBM EBCDIC (Germany with Euro)", "IBM01141", "IBM01141", "IBM01141", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
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
			if (num >= 4)
			{
				switch (num)
				{
				case 4:
					num = 55;
					break;
				case 5:
					num = 45;
					break;
				case 6:
					num = 46;
					break;
				case 7:
					num = 47;
					break;
				case 8:
					num = 22;
					break;
				case 9:
					num = 5;
					break;
				case 10:
					num = 37;
					break;
				case 20:
					num = 60;
					break;
				case 21:
					num = 61;
					break;
				case 22:
					num = 50;
					break;
				case 23:
					num = 38;
					break;
				case 26:
					num = 63;
					break;
				case 27:
					num = 39;
					break;
				case 32:
					num = 64;
					break;
				case 33:
					num = 79;
					break;
				case 34:
					num = 127;
					break;
				case 35:
					num = 123;
					break;
				case 36:
					num = 91;
					break;
				case 37:
					num = 108;
					break;
				case 38:
					num = 80;
					break;
				case 39:
					num = 125;
					break;
				case 40:
					num = 77;
					break;
				case 41:
					num = 93;
					break;
				case 42:
					num = 92;
					break;
				case 43:
					num = 78;
					break;
				case 44:
					num = 107;
					break;
				case 45:
					num = 96;
					break;
				case 46:
					num = 75;
					break;
				case 47:
					num = 97;
					break;
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
					num += 192;
					break;
				case 58:
					num = 122;
					break;
				case 59:
					num = 94;
					break;
				case 60:
					num = 76;
					break;
				case 61:
					num = 126;
					break;
				case 62:
					num = 110;
					break;
				case 63:
					num = 111;
					break;
				case 64:
					num = 181;
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
					num += 128;
					break;
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
					num += 135;
					break;
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
					num += 143;
					break;
				case 91:
					num = 99;
					break;
				case 92:
					num = 236;
					break;
				case 93:
					num = 252;
					break;
				case 94:
					num = 95;
					break;
				case 95:
					num = 109;
					break;
				case 96:
					num = 121;
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
					num += 32;
					break;
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
					num += 39;
					break;
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
					num += 47;
					break;
				case 123:
					num = 67;
					break;
				case 124:
					num = 187;
					break;
				case 125:
					num = 220;
					break;
				case 126:
					num = 89;
					break;
				case 127:
					num = 7;
					break;
				case 128:
				case 129:
				case 130:
				case 131:
				case 132:
					num -= 96;
					break;
				case 133:
					num = 21;
					break;
				case 134:
					num = 6;
					break;
				case 135:
					num = 23;
					break;
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
					num -= 96;
					break;
				case 141:
					num = 9;
					break;
				case 142:
					num = 10;
					break;
				case 143:
					num = 27;
					break;
				case 144:
					num = 48;
					break;
				case 145:
					num = 49;
					break;
				case 146:
					num = 26;
					break;
				case 147:
				case 148:
				case 149:
				case 150:
					num -= 96;
					break;
				case 151:
					num = 8;
					break;
				case 152:
				case 153:
				case 154:
				case 155:
					num -= 96;
					break;
				case 156:
					num = 4;
					break;
				case 157:
					num = 20;
					break;
				case 158:
					num = 62;
					break;
				case 159:
					num = 255;
					break;
				case 160:
					num = 65;
					break;
				case 161:
					num = 170;
					break;
				case 162:
					num = 176;
					break;
				case 163:
					num = 177;
					break;
				case 165:
					num = 178;
					break;
				case 166:
					num = 204;
					break;
				case 167:
					num = 124;
					break;
				case 168:
					num = 189;
					break;
				case 169:
					num = 180;
					break;
				case 170:
					num = 154;
					break;
				case 171:
					num = 138;
					break;
				case 172:
					num = 186;
					break;
				case 173:
					num = 202;
					break;
				case 174:
					num = 175;
					break;
				case 175:
					num = 188;
					break;
				case 176:
					num = 144;
					break;
				case 177:
					num = 143;
					break;
				case 178:
					num = 234;
					break;
				case 179:
					num = 250;
					break;
				case 180:
					num = 190;
					break;
				case 181:
					num = 160;
					break;
				case 183:
					num = 179;
					break;
				case 184:
					num = 157;
					break;
				case 185:
					num = 218;
					break;
				case 186:
					num = 155;
					break;
				case 187:
					num = 139;
					break;
				case 188:
					num = 183;
					break;
				case 189:
					num = 184;
					break;
				case 190:
					num = 185;
					break;
				case 191:
					num = 171;
					break;
				case 192:
					num = 100;
					break;
				case 193:
					num = 101;
					break;
				case 194:
					num = 98;
					break;
				case 195:
					num = 102;
					break;
				case 196:
					num = 74;
					break;
				case 197:
					num = 103;
					break;
				case 198:
					num = 158;
					break;
				case 199:
					num = 104;
					break;
				case 200:
					num = 116;
					break;
				case 201:
					num = 113;
					break;
				case 202:
					num = 114;
					break;
				case 203:
					num = 115;
					break;
				case 204:
					num = 120;
					break;
				case 205:
					num = 117;
					break;
				case 206:
					num = 118;
					break;
				case 207:
					num = 119;
					break;
				case 208:
					num = 172;
					break;
				case 209:
					num = 105;
					break;
				case 210:
					num = 237;
					break;
				case 211:
					num = 238;
					break;
				case 212:
					num = 235;
					break;
				case 213:
					num = 239;
					break;
				case 214:
					num = 224;
					break;
				case 215:
					num = 191;
					break;
				case 216:
					num = 128;
					break;
				case 217:
					num = 253;
					break;
				case 218:
					num = 254;
					break;
				case 219:
					num = 251;
					break;
				case 220:
					num = 90;
					break;
				case 221:
					num = 173;
					break;
				case 222:
					num = 174;
					break;
				case 223:
					num = 161;
					break;
				case 224:
					num = 68;
					break;
				case 225:
					num = 69;
					break;
				case 226:
					num = 66;
					break;
				case 227:
					num = 70;
					break;
				case 228:
					num = 192;
					break;
				case 229:
					num = 71;
					break;
				case 230:
					num = 156;
					break;
				case 231:
					num = 72;
					break;
				case 232:
					num = 84;
					break;
				case 233:
					num = 81;
					break;
				case 234:
					num = 82;
					break;
				case 235:
					num = 83;
					break;
				case 236:
					num = 88;
					break;
				case 237:
					num = 85;
					break;
				case 238:
					num = 86;
					break;
				case 239:
					num = 87;
					break;
				case 240:
					num = 140;
					break;
				case 241:
					num = 73;
					break;
				case 242:
					num = 205;
					break;
				case 243:
					num = 206;
					break;
				case 244:
					num = 203;
					break;
				case 245:
					num = 207;
					break;
				case 246:
					num = 106;
					break;
				case 247:
					num = 225;
					break;
				case 248:
					num = 112;
					break;
				case 249:
					num = 221;
					break;
				case 250:
					num = 222;
					break;
				case 251:
					num = 219;
					break;
				case 252:
					num = 208;
					break;
				case 253:
					num = 141;
					break;
				case 254:
					num = 142;
					break;
				case 255:
					num = 223;
					break;
				case 8254:
					num = 188;
					break;
				case 8364:
					num = 159;
					break;
				case 65281:
					num = 79;
					break;
				case 65282:
					num = 127;
					break;
				case 65283:
					num = 123;
					break;
				case 65284:
					num = 91;
					break;
				case 65285:
					num = 108;
					break;
				case 65286:
					num = 80;
					break;
				case 65287:
					num = 125;
					break;
				case 65288:
					num = 77;
					break;
				case 65289:
					num = 93;
					break;
				case 65290:
					num = 92;
					break;
				case 65291:
					num = 78;
					break;
				case 65292:
					num = 107;
					break;
				case 65293:
					num = 96;
					break;
				case 65294:
					num = 75;
					break;
				case 65295:
					num = 97;
					break;
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
					num -= 65056;
					break;
				case 65306:
					num = 122;
					break;
				case 65307:
					num = 94;
					break;
				case 65308:
					num = 76;
					break;
				case 65309:
					num = 126;
					break;
				case 65310:
					num = 110;
					break;
				case 65311:
					num = 111;
					break;
				case 65312:
					num = 181;
					break;
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
					num -= 65120;
					break;
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
					num -= 65113;
					break;
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
					num -= 65105;
					break;
				case 65339:
					num = 99;
					break;
				case 65340:
					num = 236;
					break;
				case 65341:
					num = 252;
					break;
				case 65342:
					num = 95;
					break;
				case 65343:
					num = 109;
					break;
				case 65344:
					num = 121;
					break;
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
				case 65353:
					num -= 65216;
					break;
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
				case 65361:
				case 65362:
					num -= 65209;
					break;
				case 65363:
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
					num -= 65201;
					break;
				case 65371:
					num = 67;
					break;
				case 65372:
					num = 187;
					break;
				case 65373:
					num = 220;
					break;
				case 65374:
					num = 89;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 24:
				case 25:
				case 28:
				case 29:
				case 30:
				case 31:
				case 182:
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
public class ENCibm01141 : CP1141
{
}
[Serializable]
public class CP1142 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
		'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
		'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
		'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
		'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
		'\u0014', '\u0015', '\u009e', '\u001a', ' ', '\u00a0', 'â', 'ä', 'à', 'á',
		'ã', '}', 'ç', 'ñ', '#', '.', '<', '(', '+', '!',
		'&', 'é', 'ê', 'ë', 'è', 'í', 'î', 'ï', 'ì', 'ß',
		'€', 'Å', '*', ')', ';', '^', '-', '/', 'Â', 'Ä',
		'À', 'Á', 'Ã', '$', 'Ç', 'Ñ', 'ø', ',', '%', '_',
		'>', '?', '¦', 'É', 'Ê', 'Ë', 'È', 'Í', 'Î', 'Ï',
		'Ì', '`', ':', 'Æ', 'Ø', '\'', '=', '"', '@', 'a',
		'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', '«', '»',
		'ð', 'ý', 'þ', '±', '°', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', 'ª', 'º', '{', '\u00b8', '[', ']',
		'µ', 'ü', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'¡', '¿', 'Ð', 'Ý', 'Þ', '®', '¢', '£', '¥', '·',
		'©', '§', '¶', '¼', '½', '¾', '¬', '|', '\u00af', '\u00a8',
		'\u00b4', '×', 'æ', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
		'H', 'I', '\u00ad', 'ô', 'ö', 'ò', 'ó', 'õ', 'å', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', '¹', 'û',
		'~', 'ù', 'ú', 'ÿ', '\\', '÷', 'S', 'T', 'U', 'V',
		'W', 'X', 'Y', 'Z', '²', 'Ô', 'Ö', 'Ò', 'Ó', 'Õ',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'³', 'Û', 'Ü', 'Ù', 'Ú', '\u009f'
	};

	public CP1142()
		: base(1142, ToChars, "IBM EBCDIC (Denmark/Norway with Euro)", "IBM01142", "IBM01142", "IBM01142", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
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
			if (num >= 4)
			{
				switch (num)
				{
				case 4:
					num = 55;
					break;
				case 5:
					num = 45;
					break;
				case 6:
					num = 46;
					break;
				case 7:
					num = 47;
					break;
				case 8:
					num = 22;
					break;
				case 9:
					num = 5;
					break;
				case 10:
					num = 37;
					break;
				case 20:
					num = 60;
					break;
				case 21:
					num = 61;
					break;
				case 22:
					num = 50;
					break;
				case 23:
					num = 38;
					break;
				case 26:
					num = 63;
					break;
				case 27:
					num = 39;
					break;
				case 32:
					num = 64;
					break;
				case 33:
					num = 79;
					break;
				case 34:
					num = 127;
					break;
				case 35:
					num = 74;
					break;
				case 36:
					num = 103;
					break;
				case 37:
					num = 108;
					break;
				case 38:
					num = 80;
					break;
				case 39:
					num = 125;
					break;
				case 40:
					num = 77;
					break;
				case 41:
					num = 93;
					break;
				case 42:
					num = 92;
					break;
				case 43:
					num = 78;
					break;
				case 44:
					num = 107;
					break;
				case 45:
					num = 96;
					break;
				case 46:
					num = 75;
					break;
				case 47:
					num = 97;
					break;
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
					num += 192;
					break;
				case 58:
					num = 122;
					break;
				case 59:
					num = 94;
					break;
				case 60:
					num = 76;
					break;
				case 61:
					num = 126;
					break;
				case 62:
					num = 110;
					break;
				case 63:
					num = 111;
					break;
				case 64:
					num = 128;
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
					num += 128;
					break;
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
					num += 135;
					break;
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
					num += 143;
					break;
				case 91:
					num = 158;
					break;
				case 92:
					num = 224;
					break;
				case 93:
					num = 159;
					break;
				case 94:
					num = 95;
					break;
				case 95:
					num = 109;
					break;
				case 96:
					num = 121;
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
					num += 32;
					break;
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
					num += 39;
					break;
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
					num += 47;
					break;
				case 123:
					num = 156;
					break;
				case 124:
					num = 187;
					break;
				case 125:
					num = 71;
					break;
				case 126:
					num = 220;
					break;
				case 127:
					num = 7;
					break;
				case 128:
				case 129:
				case 130:
				case 131:
				case 132:
					num -= 96;
					break;
				case 133:
					num = 21;
					break;
				case 134:
					num = 6;
					break;
				case 135:
					num = 23;
					break;
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
					num -= 96;
					break;
				case 141:
					num = 9;
					break;
				case 142:
					num = 10;
					break;
				case 143:
					num = 27;
					break;
				case 144:
					num = 48;
					break;
				case 145:
					num = 49;
					break;
				case 146:
					num = 26;
					break;
				case 147:
				case 148:
				case 149:
				case 150:
					num -= 96;
					break;
				case 151:
					num = 8;
					break;
				case 152:
				case 153:
				case 154:
				case 155:
					num -= 96;
					break;
				case 156:
					num = 4;
					break;
				case 157:
					num = 20;
					break;
				case 158:
					num = 62;
					break;
				case 159:
					num = 255;
					break;
				case 160:
					num = 65;
					break;
				case 161:
					num = 170;
					break;
				case 162:
					num = 176;
					break;
				case 163:
					num = 177;
					break;
				case 165:
					num = 178;
					break;
				case 166:
					num = 112;
					break;
				case 167:
					num = 181;
					break;
				case 168:
					num = 189;
					break;
				case 169:
					num = 180;
					break;
				case 170:
					num = 154;
					break;
				case 171:
					num = 138;
					break;
				case 172:
					num = 186;
					break;
				case 173:
					num = 202;
					break;
				case 174:
					num = 175;
					break;
				case 175:
					num = 188;
					break;
				case 176:
					num = 144;
					break;
				case 177:
					num = 143;
					break;
				case 178:
					num = 234;
					break;
				case 179:
					num = 250;
					break;
				case 180:
					num = 190;
					break;
				case 181:
					num = 160;
					break;
				case 183:
					num = 179;
					break;
				case 184:
					num = 157;
					break;
				case 185:
					num = 218;
					break;
				case 186:
					num = 155;
					break;
				case 187:
					num = 139;
					break;
				case 188:
					num = 183;
					break;
				case 189:
					num = 184;
					break;
				case 190:
					num = 185;
					break;
				case 191:
					num = 171;
					break;
				case 192:
					num = 100;
					break;
				case 193:
					num = 101;
					break;
				case 194:
					num = 98;
					break;
				case 195:
					num = 102;
					break;
				case 196:
					num = 99;
					break;
				case 197:
					num = 91;
					break;
				case 198:
					num = 123;
					break;
				case 199:
					num = 104;
					break;
				case 200:
					num = 116;
					break;
				case 201:
					num = 113;
					break;
				case 202:
					num = 114;
					break;
				case 203:
					num = 115;
					break;
				case 204:
					num = 120;
					break;
				case 205:
					num = 117;
					break;
				case 206:
					num = 118;
					break;
				case 207:
					num = 119;
					break;
				case 208:
					num = 172;
					break;
				case 209:
					num = 105;
					break;
				case 210:
					num = 237;
					break;
				case 211:
					num = 238;
					break;
				case 212:
					num = 235;
					break;
				case 213:
					num = 239;
					break;
				case 214:
					num = 236;
					break;
				case 215:
					num = 191;
					break;
				case 216:
					num = 124;
					break;
				case 217:
					num = 253;
					break;
				case 218:
					num = 254;
					break;
				case 219:
					num = 251;
					break;
				case 220:
					num = 252;
					break;
				case 221:
					num = 173;
					break;
				case 222:
					num = 174;
					break;
				case 223:
					num = 89;
					break;
				case 224:
					num = 68;
					break;
				case 225:
					num = 69;
					break;
				case 226:
					num = 66;
					break;
				case 227:
					num = 70;
					break;
				case 228:
					num = 67;
					break;
				case 229:
					num = 208;
					break;
				case 230:
					num = 192;
					break;
				case 231:
					num = 72;
					break;
				case 232:
					num = 84;
					break;
				case 233:
					num = 81;
					break;
				case 234:
					num = 82;
					break;
				case 235:
					num = 83;
					break;
				case 236:
					num = 88;
					break;
				case 237:
					num = 85;
					break;
				case 238:
					num = 86;
					break;
				case 239:
					num = 87;
					break;
				case 240:
					num = 140;
					break;
				case 241:
					num = 73;
					break;
				case 242:
					num = 205;
					break;
				case 243:
					num = 206;
					break;
				case 244:
					num = 203;
					break;
				case 245:
					num = 207;
					break;
				case 246:
					num = 204;
					break;
				case 247:
					num = 225;
					break;
				case 248:
					num = 106;
					break;
				case 249:
					num = 221;
					break;
				case 250:
					num = 222;
					break;
				case 251:
					num = 219;
					break;
				case 252:
					num = 161;
					break;
				case 253:
					num = 141;
					break;
				case 254:
					num = 142;
					break;
				case 255:
					num = 223;
					break;
				case 8254:
					num = 188;
					break;
				case 8364:
					num = 90;
					break;
				case 65281:
					num = 79;
					break;
				case 65282:
					num = 127;
					break;
				case 65283:
					num = 74;
					break;
				case 65284:
					num = 103;
					break;
				case 65285:
					num = 108;
					break;
				case 65286:
					num = 80;
					break;
				case 65287:
					num = 125;
					break;
				case 65288:
					num = 77;
					break;
				case 65289:
					num = 93;
					break;
				case 65290:
					num = 92;
					break;
				case 65291:
					num = 78;
					break;
				case 65292:
					num = 107;
					break;
				case 65293:
					num = 96;
					break;
				case 65294:
					num = 75;
					break;
				case 65295:
					num = 97;
					break;
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
					num -= 65056;
					break;
				case 65306:
					num = 122;
					break;
				case 65307:
					num = 94;
					break;
				case 65308:
					num = 76;
					break;
				case 65309:
					num = 126;
					break;
				case 65310:
					num = 110;
					break;
				case 65311:
					num = 111;
					break;
				case 65312:
					num = 128;
					break;
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
					num -= 65120;
					break;
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
					num -= 65113;
					break;
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
					num -= 65105;
					break;
				case 65339:
					num = 158;
					break;
				case 65340:
					num = 224;
					break;
				case 65341:
					num = 159;
					break;
				case 65342:
					num = 95;
					break;
				case 65343:
					num = 109;
					break;
				case 65344:
					num = 121;
					break;
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
				case 65353:
					num -= 65216;
					break;
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
				case 65361:
				case 65362:
					num -= 65209;
					break;
				case 65363:
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
					num -= 65201;
					break;
				case 65371:
					num = 156;
					break;
				case 65372:
					num = 187;
					break;
				case 65373:
					num = 71;
					break;
				case 65374:
					num = 220;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 24:
				case 25:
				case 28:
				case 29:
				case 30:
				case 31:
				case 182:
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
public class ENCibm01142 : CP1142
{
}
[Serializable]
public class CP1143 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
		'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
		'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
		'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
		'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
		'\u0014', '\u0015', '\u009e', '\u001a', ' ', '\u00a0', 'â', '{', 'à', 'á',
		'ã', '}', 'ç', 'ñ', '§', '.', '<', '(', '+', '!',
		'&', '`', 'ê', 'ë', 'è', 'í', 'î', 'ï', 'ì', 'ß',
		'€', 'Å', '*', ')', ';', '^', '-', '/', 'Â', '#',
		'À', 'Á', 'Ã', '$', 'Ç', 'Ñ', 'ö', ',', '%', '_',
		'>', '?', 'ø', '\\', 'Ê', 'Ë', 'È', 'Í', 'Î', 'Ï',
		'Ì', 'é', ':', 'Ä', 'Ö', '\'', '=', '"', 'Ø', 'a',
		'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', '«', '»',
		'ð', 'ý', 'þ', '±', '°', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', 'ª', 'º', 'æ', '\u00b8', 'Æ', ']',
		'µ', 'ü', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'¡', '¿', 'Ð', 'Ý', 'Þ', '®', '¢', '£', '¥', '·',
		'©', '[', '¶', '¼', '½', '¾', '¬', '|', '\u00af', '\u00a8',
		'\u00b4', '×', 'ä', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
		'H', 'I', '\u00ad', 'ô', '¦', 'ò', 'ó', 'õ', 'å', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', '¹', 'û',
		'~', 'ù', 'ú', 'ÿ', 'É', '÷', 'S', 'T', 'U', 'V',
		'W', 'X', 'Y', 'Z', '²', 'Ô', '@', 'Ò', 'Ó', 'Õ',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'³', 'Û', 'Ü', 'Ù', 'Ú', '\u009f'
	};

	public CP1143()
		: base(1143, ToChars, "IBM EBCDIC (Finland/Sweden with Euro)", "IBM01143", "IBM01143", "IBM01143", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
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
			if (num >= 4)
			{
				switch (num)
				{
				case 4:
					num = 55;
					break;
				case 5:
					num = 45;
					break;
				case 6:
					num = 46;
					break;
				case 7:
					num = 47;
					break;
				case 8:
					num = 22;
					break;
				case 9:
					num = 5;
					break;
				case 10:
					num = 37;
					break;
				case 20:
					num = 60;
					break;
				case 21:
					num = 61;
					break;
				case 22:
					num = 50;
					break;
				case 23:
					num = 38;
					break;
				case 26:
					num = 63;
					break;
				case 27:
					num = 39;
					break;
				case 32:
					num = 64;
					break;
				case 33:
					num = 79;
					break;
				case 34:
					num = 127;
					break;
				case 35:
					num = 99;
					break;
				case 36:
					num = 103;
					break;
				case 37:
					num = 108;
					break;
				case 38:
					num = 80;
					break;
				case 39:
					num = 125;
					break;
				case 40:
					num = 77;
					break;
				case 41:
					num = 93;
					break;
				case 42:
					num = 92;
					break;
				case 43:
					num = 78;
					break;
				case 44:
					num = 107;
					break;
				case 45:
					num = 96;
					break;
				case 46:
					num = 75;
					break;
				case 47:
					num = 97;
					break;
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
					num += 192;
					break;
				case 58:
					num = 122;
					break;
				case 59:
					num = 94;
					break;
				case 60:
					num = 76;
					break;
				case 61:
					num = 126;
					break;
				case 62:
					num = 110;
					break;
				case 63:
					num = 111;
					break;
				case 64:
					num = 236;
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
					num += 128;
					break;
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
					num += 135;
					break;
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
					num += 143;
					break;
				case 91:
					num = 181;
					break;
				case 92:
					num = 113;
					break;
				case 93:
					num = 159;
					break;
				case 94:
					num = 95;
					break;
				case 95:
					num = 109;
					break;
				case 96:
					num = 81;
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
					num += 32;
					break;
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
					num += 39;
					break;
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
					num += 47;
					break;
				case 123:
					num = 67;
					break;
				case 124:
					num = 187;
					break;
				case 125:
					num = 71;
					break;
				case 126:
					num = 220;
					break;
				case 127:
					num = 7;
					break;
				case 128:
				case 129:
				case 130:
				case 131:
				case 132:
					num -= 96;
					break;
				case 133:
					num = 21;
					break;
				case 134:
					num = 6;
					break;
				case 135:
					num = 23;
					break;
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
					num -= 96;
					break;
				case 141:
					num = 9;
					break;
				case 142:
					num = 10;
					break;
				case 143:
					num = 27;
					break;
				case 144:
					num = 48;
					break;
				case 145:
					num = 49;
					break;
				case 146:
					num = 26;
					break;
				case 147:
				case 148:
				case 149:
				case 150:
					num -= 96;
					break;
				case 151:
					num = 8;
					break;
				case 152:
				case 153:
				case 154:
				case 155:
					num -= 96;
					break;
				case 156:
					num = 4;
					break;
				case 157:
					num = 20;
					break;
				case 158:
					num = 62;
					break;
				case 159:
					num = 255;
					break;
				case 160:
					num = 65;
					break;
				case 161:
					num = 170;
					break;
				case 162:
					num = 176;
					break;
				case 163:
					num = 177;
					break;
				case 165:
					num = 178;
					break;
				case 166:
					num = 204;
					break;
				case 167:
					num = 74;
					break;
				case 168:
					num = 189;
					break;
				case 169:
					num = 180;
					break;
				case 170:
					num = 154;
					break;
				case 171:
					num = 138;
					break;
				case 172:
					num = 186;
					break;
				case 173:
					num = 202;
					break;
				case 174:
					num = 175;
					break;
				case 175:
					num = 188;
					break;
				case 176:
					num = 144;
					break;
				case 177:
					num = 143;
					break;
				case 178:
					num = 234;
					break;
				case 179:
					num = 250;
					break;
				case 180:
					num = 190;
					break;
				case 181:
					num = 160;
					break;
				case 183:
					num = 179;
					break;
				case 184:
					num = 157;
					break;
				case 185:
					num = 218;
					break;
				case 186:
					num = 155;
					break;
				case 187:
					num = 139;
					break;
				case 188:
					num = 183;
					break;
				case 189:
					num = 184;
					break;
				case 190:
					num = 185;
					break;
				case 191:
					num = 171;
					break;
				case 192:
					num = 100;
					break;
				case 193:
					num = 101;
					break;
				case 194:
					num = 98;
					break;
				case 195:
					num = 102;
					break;
				case 196:
					num = 123;
					break;
				case 197:
					num = 91;
					break;
				case 198:
					num = 158;
					break;
				case 199:
					num = 104;
					break;
				case 200:
					num = 116;
					break;
				case 201:
					num = 224;
					break;
				case 202:
					num = 114;
					break;
				case 203:
					num = 115;
					break;
				case 204:
					num = 120;
					break;
				case 205:
					num = 117;
					break;
				case 206:
					num = 118;
					break;
				case 207:
					num = 119;
					break;
				case 208:
					num = 172;
					break;
				case 209:
					num = 105;
					break;
				case 210:
					num = 237;
					break;
				case 211:
					num = 238;
					break;
				case 212:
					num = 235;
					break;
				case 213:
					num = 239;
					break;
				case 214:
					num = 124;
					break;
				case 215:
					num = 191;
					break;
				case 216:
					num = 128;
					break;
				case 217:
					num = 253;
					break;
				case 218:
					num = 254;
					break;
				case 219:
					num = 251;
					break;
				case 220:
					num = 252;
					break;
				case 221:
					num = 173;
					break;
				case 222:
					num = 174;
					break;
				case 223:
					num = 89;
					break;
				case 224:
					num = 68;
					break;
				case 225:
					num = 69;
					break;
				case 226:
					num = 66;
					break;
				case 227:
					num = 70;
					break;
				case 228:
					num = 192;
					break;
				case 229:
					num = 208;
					break;
				case 230:
					num = 156;
					break;
				case 231:
					num = 72;
					break;
				case 232:
					num = 84;
					break;
				case 233:
					num = 121;
					break;
				case 234:
					num = 82;
					break;
				case 235:
					num = 83;
					break;
				case 236:
					num = 88;
					break;
				case 237:
					num = 85;
					break;
				case 238:
					num = 86;
					break;
				case 239:
					num = 87;
					break;
				case 240:
					num = 140;
					break;
				case 241:
					num = 73;
					break;
				case 242:
					num = 205;
					break;
				case 243:
					num = 206;
					break;
				case 244:
					num = 203;
					break;
				case 245:
					num = 207;
					break;
				case 246:
					num = 106;
					break;
				case 247:
					num = 225;
					break;
				case 248:
					num = 112;
					break;
				case 249:
					num = 221;
					break;
				case 250:
					num = 222;
					break;
				case 251:
					num = 219;
					break;
				case 252:
					num = 161;
					break;
				case 253:
					num = 141;
					break;
				case 254:
					num = 142;
					break;
				case 255:
					num = 223;
					break;
				case 8254:
					num = 188;
					break;
				case 8364:
					num = 90;
					break;
				case 65281:
					num = 79;
					break;
				case 65282:
					num = 127;
					break;
				case 65283:
					num = 99;
					break;
				case 65284:
					num = 103;
					break;
				case 65285:
					num = 108;
					break;
				case 65286:
					num = 80;
					break;
				case 65287:
					num = 125;
					break;
				case 65288:
					num = 77;
					break;
				case 65289:
					num = 93;
					break;
				case 65290:
					num = 92;
					break;
				case 65291:
					num = 78;
					break;
				case 65292:
					num = 107;
					break;
				case 65293:
					num = 96;
					break;
				case 65294:
					num = 75;
					break;
				case 65295:
					num = 97;
					break;
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
					num -= 65056;
					break;
				case 65306:
					num = 122;
					break;
				case 65307:
					num = 94;
					break;
				case 65308:
					num = 76;
					break;
				case 65309:
					num = 126;
					break;
				case 65310:
					num = 110;
					break;
				case 65311:
					num = 111;
					break;
				case 65312:
					num = 236;
					break;
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
					num -= 65120;
					break;
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
					num -= 65113;
					break;
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
					num -= 65105;
					break;
				case 65339:
					num = 181;
					break;
				case 65340:
					num = 113;
					break;
				case 65341:
					num = 159;
					break;
				case 65342:
					num = 95;
					break;
				case 65343:
					num = 109;
					break;
				case 65344:
					num = 81;
					break;
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
				case 65353:
					num -= 65216;
					break;
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
				case 65361:
				case 65362:
					num -= 65209;
					break;
				case 65363:
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
					num -= 65201;
					break;
				case 65371:
					num = 67;
					break;
				case 65372:
					num = 187;
					break;
				case 65373:
					num = 71;
					break;
				case 65374:
					num = 220;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 24:
				case 25:
				case 28:
				case 29:
				case 30:
				case 31:
				case 182:
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
public class ENCibm01143 : CP1143
{
}
[Serializable]
public class CP1144 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
		'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
		'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
		'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
		'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
		'\u0014', '\u0015', '\u009e', '\u001a', ' ', '\u00a0', 'â', 'ä', '{', 'á',
		'ã', 'å', '\\', 'ñ', '°', '.', '<', '(', '+', '!',
		'&', ']', 'ê', 'ë', '}', 'í', 'î', 'ï', '~', 'ß',
		'é', '$', '*', ')', ';', '^', '-', '/', 'Â', 'Ä',
		'À', 'Á', 'Ã', 'Å', 'Ç', 'Ñ', 'ò', ',', '%', '_',
		'>', '?', 'ø', 'É', 'Ê', 'Ë', 'È', 'Í', 'Î', 'Ï',
		'Ì', 'ù', ':', '£', '§', '\'', '=', '"', 'Ø', 'a',
		'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', '«', '»',
		'ð', 'ý', 'þ', '±', '[', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', 'ª', 'º', 'æ', '\u00b8', 'Æ', '€',
		'µ', 'ì', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'¡', '¿', 'Ð', 'Ý', 'Þ', '®', '¢', '#', '¥', '·',
		'©', '@', '¶', '¼', '½', '¾', '¬', '|', '\u00af', '\u00a8',
		'\u00b4', '×', 'à', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
		'H', 'I', '\u00ad', 'ô', 'ö', '¦', 'ó', 'õ', 'è', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', '¹', 'û',
		'ü', '`', 'ú', 'ÿ', 'ç', '÷', 'S', 'T', 'U', 'V',
		'W', 'X', 'Y', 'Z', '²', 'Ô', 'Ö', 'Ò', 'Ó', 'Õ',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'³', 'Û', 'Ü', 'Ù', 'Ú', '\u009f'
	};

	public CP1144()
		: base(1144, ToChars, "IBM EBCDIC (Italy with Euro)", "ibm1144", "ibm1144", "ibm1144", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
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
			if (num >= 4)
			{
				switch (num)
				{
				case 4:
					num = 55;
					break;
				case 5:
					num = 45;
					break;
				case 6:
					num = 46;
					break;
				case 7:
					num = 47;
					break;
				case 8:
					num = 22;
					break;
				case 9:
					num = 5;
					break;
				case 10:
					num = 37;
					break;
				case 20:
					num = 60;
					break;
				case 21:
					num = 61;
					break;
				case 22:
					num = 50;
					break;
				case 23:
					num = 38;
					break;
				case 26:
					num = 63;
					break;
				case 27:
					num = 39;
					break;
				case 32:
					num = 64;
					break;
				case 33:
					num = 79;
					break;
				case 34:
					num = 127;
					break;
				case 35:
					num = 177;
					break;
				case 36:
					num = 91;
					break;
				case 37:
					num = 108;
					break;
				case 38:
					num = 80;
					break;
				case 39:
					num = 125;
					break;
				case 40:
					num = 77;
					break;
				case 41:
					num = 93;
					break;
				case 42:
					num = 92;
					break;
				case 43:
					num = 78;
					break;
				case 44:
					num = 107;
					break;
				case 45:
					num = 96;
					break;
				case 46:
					num = 75;
					break;
				case 47:
					num = 97;
					break;
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
					num += 192;
					break;
				case 58:
					num = 122;
					break;
				case 59:
					num = 94;
					break;
				case 60:
					num = 76;
					break;
				case 61:
					num = 126;
					break;
				case 62:
					num = 110;
					break;
				case 63:
					num = 111;
					break;
				case 64:
					num = 181;
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
					num += 128;
					break;
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
					num += 135;
					break;
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
					num += 143;
					break;
				case 91:
					num = 144;
					break;
				case 92:
					num = 72;
					break;
				case 93:
					num = 81;
					break;
				case 94:
					num = 95;
					break;
				case 95:
					num = 109;
					break;
				case 96:
					num = 221;
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
					num += 32;
					break;
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
					num += 39;
					break;
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
					num += 47;
					break;
				case 123:
					num = 68;
					break;
				case 124:
					num = 187;
					break;
				case 125:
					num = 84;
					break;
				case 126:
					num = 88;
					break;
				case 127:
					num = 7;
					break;
				case 128:
				case 129:
				case 130:
				case 131:
				case 132:
					num -= 96;
					break;
				case 133:
					num = 21;
					break;
				case 134:
					num = 6;
					break;
				case 135:
					num = 23;
					break;
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
					num -= 96;
					break;
				case 141:
					num = 9;
					break;
				case 142:
					num = 10;
					break;
				case 143:
					num = 27;
					break;
				case 144:
					num = 48;
					break;
				case 145:
					num = 49;
					break;
				case 146:
					num = 26;
					break;
				case 147:
				case 148:
				case 149:
				case 150:
					num -= 96;
					break;
				case 151:
					num = 8;
					break;
				case 152:
				case 153:
				case 154:
				case 155:
					num -= 96;
					break;
				case 156:
					num = 4;
					break;
				case 157:
					num = 20;
					break;
				case 158:
					num = 62;
					break;
				case 159:
					num = 255;
					break;
				case 160:
					num = 65;
					break;
				case 161:
					num = 170;
					break;
				case 162:
					num = 176;
					break;
				case 163:
					num = 123;
					break;
				case 165:
					num = 178;
					break;
				case 166:
					num = 205;
					break;
				case 167:
					num = 124;
					break;
				case 168:
					num = 189;
					break;
				case 169:
					num = 180;
					break;
				case 170:
					num = 154;
					break;
				case 171:
					num = 138;
					break;
				case 172:
					num = 186;
					break;
				case 173:
					num = 202;
					break;
				case 174:
					num = 175;
					break;
				case 175:
					num = 188;
					break;
				case 176:
					num = 74;
					break;
				case 177:
					num = 143;
					break;
				case 178:
					num = 234;
					break;
				case 179:
					num = 250;
					break;
				case 180:
					num = 190;
					break;
				case 181:
					num = 160;
					break;
				case 183:
					num = 179;
					break;
				case 184:
					num = 157;
					break;
				case 185:
					num = 218;
					break;
				case 186:
					num = 155;
					break;
				case 187:
					num = 139;
					break;
				case 188:
					num = 183;
					break;
				case 189:
					num = 184;
					break;
				case 190:
					num = 185;
					break;
				case 191:
					num = 171;
					break;
				case 192:
					num = 100;
					break;
				case 193:
					num = 101;
					break;
				case 194:
					num = 98;
					break;
				case 195:
					num = 102;
					break;
				case 196:
					num = 99;
					break;
				case 197:
					num = 103;
					break;
				case 198:
					num = 158;
					break;
				case 199:
					num = 104;
					break;
				case 200:
					num = 116;
					break;
				case 201:
					num = 113;
					break;
				case 202:
					num = 114;
					break;
				case 203:
					num = 115;
					break;
				case 204:
					num = 120;
					break;
				case 205:
					num = 117;
					break;
				case 206:
					num = 118;
					break;
				case 207:
					num = 119;
					break;
				case 208:
					num = 172;
					break;
				case 209:
					num = 105;
					break;
				case 210:
					num = 237;
					break;
				case 211:
					num = 238;
					break;
				case 212:
					num = 235;
					break;
				case 213:
					num = 239;
					break;
				case 214:
					num = 236;
					break;
				case 215:
					num = 191;
					break;
				case 216:
					num = 128;
					break;
				case 217:
					num = 253;
					break;
				case 218:
					num = 254;
					break;
				case 219:
					num = 251;
					break;
				case 220:
					num = 252;
					break;
				case 221:
					num = 173;
					break;
				case 222:
					num = 174;
					break;
				case 223:
					num = 89;
					break;
				case 224:
					num = 192;
					break;
				case 225:
					num = 69;
					break;
				case 226:
					num = 66;
					break;
				case 227:
					num = 70;
					break;
				case 228:
					num = 67;
					break;
				case 229:
					num = 71;
					break;
				case 230:
					num = 156;
					break;
				case 231:
					num = 224;
					break;
				case 232:
					num = 208;
					break;
				case 233:
					num = 90;
					break;
				case 234:
					num = 82;
					break;
				case 235:
					num = 83;
					break;
				case 236:
					num = 161;
					break;
				case 237:
					num = 85;
					break;
				case 238:
					num = 86;
					break;
				case 239:
					num = 87;
					break;
				case 240:
					num = 140;
					break;
				case 241:
					num = 73;
					break;
				case 242:
					num = 106;
					break;
				case 243:
					num = 206;
					break;
				case 244:
					num = 203;
					break;
				case 245:
					num = 207;
					break;
				case 246:
					num = 204;
					break;
				case 247:
					num = 225;
					break;
				case 248:
					num = 112;
					break;
				case 249:
					num = 121;
					break;
				case 250:
					num = 222;
					break;
				case 251:
					num = 219;
					break;
				case 252:
					num = 220;
					break;
				case 253:
					num = 141;
					break;
				case 254:
					num = 142;
					break;
				case 255:
					num = 223;
					break;
				case 8254:
					num = 188;
					break;
				case 8364:
					num = 159;
					break;
				case 65281:
					num = 79;
					break;
				case 65282:
					num = 127;
					break;
				case 65283:
					num = 177;
					break;
				case 65284:
					num = 91;
					break;
				case 65285:
					num = 108;
					break;
				case 65286:
					num = 80;
					break;
				case 65287:
					num = 125;
					break;
				case 65288:
					num = 77;
					break;
				case 65289:
					num = 93;
					break;
				case 65290:
					num = 92;
					break;
				case 65291:
					num = 78;
					break;
				case 65292:
					num = 107;
					break;
				case 65293:
					num = 96;
					break;
				case 65294:
					num = 75;
					break;
				case 65295:
					num = 97;
					break;
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
					num -= 65056;
					break;
				case 65306:
					num = 122;
					break;
				case 65307:
					num = 94;
					break;
				case 65308:
					num = 76;
					break;
				case 65309:
					num = 126;
					break;
				case 65310:
					num = 110;
					break;
				case 65311:
					num = 111;
					break;
				case 65312:
					num = 181;
					break;
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
					num -= 65120;
					break;
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
					num -= 65113;
					break;
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
					num -= 65105;
					break;
				case 65339:
					num = 144;
					break;
				case 65340:
					num = 72;
					break;
				case 65341:
					num = 81;
					break;
				case 65342:
					num = 95;
					break;
				case 65343:
					num = 109;
					break;
				case 65344:
					num = 221;
					break;
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
				case 65353:
					num -= 65216;
					break;
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
				case 65361:
				case 65362:
					num -= 65209;
					break;
				case 65363:
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
					num -= 65201;
					break;
				case 65371:
					num = 68;
					break;
				case 65372:
					num = 187;
					break;
				case 65373:
					num = 84;
					break;
				case 65374:
					num = 88;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 24:
				case 25:
				case 28:
				case 29:
				case 30:
				case 31:
				case 182:
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
public class ENCibm1144 : CP1144
{
}
[Serializable]
public class CP1145 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
		'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
		'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
		'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
		'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
		'\u0014', '\u0015', '\u009e', '\u001a', ' ', '\u00a0', 'â', 'ä', 'à', 'á',
		'ã', 'å', 'ç', '¦', '[', '.', '<', '(', '+', '|',
		'&', 'é', 'ê', 'ë', 'è', 'í', 'î', 'ï', 'ì', 'ß',
		']', '$', '*', ')', ';', '¬', '-', '/', 'Â', 'Ä',
		'À', 'Á', 'Ã', 'Å', 'Ç', '#', 'ñ', ',', '%', '_',
		'>', '?', 'ø', 'É', 'Ê', 'Ë', 'È', 'Í', 'Î', 'Ï',
		'Ì', '`', ':', 'Ñ', '@', '\'', '=', '"', 'Ø', 'a',
		'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', '«', '»',
		'ð', 'ý', 'þ', '±', '°', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', 'ª', 'º', 'æ', '\u00b8', 'Æ', '€',
		'µ', '\u00a8', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'¡', '¿', 'Ð', 'Ý', 'Þ', '®', '¢', '£', '¥', '·',
		'©', '§', '¶', '¼', '½', '¾', '^', '!', '\u00af', '~',
		'\u00b4', '×', '{', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
		'H', 'I', '\u00ad', 'ô', 'ö', 'ò', 'ó', 'õ', '}', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', '¹', 'û',
		'ü', 'ù', 'ú', 'ÿ', '\\', '÷', 'S', 'T', 'U', 'V',
		'W', 'X', 'Y', 'Z', '²', 'Ô', 'Ö', 'Ò', 'Ó', 'Õ',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'³', 'Û', 'Ü', 'Ù', 'Ú', '\u009f'
	};

	public CP1145()
		: base(1145, ToChars, "IBM EBCDIC (Latin America/Spain with Euro)", "ibm1145", "ibm1145", "ibm1145", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
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
			if (num >= 4)
			{
				switch (num)
				{
				case 4:
					num = 55;
					break;
				case 5:
					num = 45;
					break;
				case 6:
					num = 46;
					break;
				case 7:
					num = 47;
					break;
				case 8:
					num = 22;
					break;
				case 9:
					num = 5;
					break;
				case 10:
					num = 37;
					break;
				case 20:
					num = 60;
					break;
				case 21:
					num = 61;
					break;
				case 22:
					num = 50;
					break;
				case 23:
					num = 38;
					break;
				case 26:
					num = 63;
					break;
				case 27:
					num = 39;
					break;
				case 32:
					num = 64;
					break;
				case 33:
					num = 187;
					break;
				case 34:
					num = 127;
					break;
				case 35:
					num = 105;
					break;
				case 36:
					num = 91;
					break;
				case 37:
					num = 108;
					break;
				case 38:
					num = 80;
					break;
				case 39:
					num = 125;
					break;
				case 40:
					num = 77;
					break;
				case 41:
					num = 93;
					break;
				case 42:
					num = 92;
					break;
				case 43:
					num = 78;
					break;
				case 44:
					num = 107;
					break;
				case 45:
					num = 96;
					break;
				case 46:
					num = 75;
					break;
				case 47:
					num = 97;
					break;
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
					num += 192;
					break;
				case 58:
					num = 122;
					break;
				case 59:
					num = 94;
					break;
				case 60:
					num = 76;
					break;
				case 61:
					num = 126;
					break;
				case 62:
					num = 110;
					break;
				case 63:
					num = 111;
					break;
				case 64:
					num = 124;
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
					num += 128;
					break;
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
					num += 135;
					break;
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
					num += 143;
					break;
				case 91:
					num = 74;
					break;
				case 92:
					num = 224;
					break;
				case 93:
					num = 90;
					break;
				case 94:
					num = 186;
					break;
				case 95:
					num = 109;
					break;
				case 96:
					num = 121;
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
					num += 32;
					break;
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
					num += 39;
					break;
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
					num += 47;
					break;
				case 123:
					num = 192;
					break;
				case 124:
					num = 79;
					break;
				case 125:
					num = 208;
					break;
				case 126:
					num = 189;
					break;
				case 127:
					num = 7;
					break;
				case 128:
				case 129:
				case 130:
				case 131:
				case 132:
					num -= 96;
					break;
				case 133:
					num = 21;
					break;
				case 134:
					num = 6;
					break;
				case 135:
					num = 23;
					break;
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
					num -= 96;
					break;
				case 141:
					num = 9;
					break;
				case 142:
					num = 10;
					break;
				case 143:
					num = 27;
					break;
				case 144:
					num = 48;
					break;
				case 145:
					num = 49;
					break;
				case 146:
					num = 26;
					break;
				case 147:
				case 148:
				case 149:
				case 150:
					num -= 96;
					break;
				case 151:
					num = 8;
					break;
				case 152:
				case 153:
				case 154:
				case 155:
					num -= 96;
					break;
				case 156:
					num = 4;
					break;
				case 157:
					num = 20;
					break;
				case 158:
					num = 62;
					break;
				case 159:
					num = 255;
					break;
				case 160:
					num = 65;
					break;
				case 161:
					num = 170;
					break;
				case 162:
					num = 176;
					break;
				case 163:
					num = 177;
					break;
				case 165:
					num = 178;
					break;
				case 166:
					num = 73;
					break;
				case 167:
					num = 181;
					break;
				case 168:
					num = 161;
					break;
				case 169:
					num = 180;
					break;
				case 170:
					num = 154;
					break;
				case 171:
					num = 138;
					break;
				case 172:
					num = 95;
					break;
				case 173:
					num = 202;
					break;
				case 174:
					num = 175;
					break;
				case 175:
					num = 188;
					break;
				case 176:
					num = 144;
					break;
				case 177:
					num = 143;
					break;
				case 178:
					num = 234;
					break;
				case 179:
					num = 250;
					break;
				case 180:
					num = 190;
					break;
				case 181:
					num = 160;
					break;
				case 183:
					num = 179;
					break;
				case 184:
					num = 157;
					break;
				case 185:
					num = 218;
					break;
				case 186:
					num = 155;
					break;
				case 187:
					num = 139;
					break;
				case 188:
					num = 183;
					break;
				case 189:
					num = 184;
					break;
				case 190:
					num = 185;
					break;
				case 191:
					num = 171;
					break;
				case 192:
					num = 100;
					break;
				case 193:
					num = 101;
					break;
				case 194:
					num = 98;
					break;
				case 195:
					num = 102;
					break;
				case 196:
					num = 99;
					break;
				case 197:
					num = 103;
					break;
				case 198:
					num = 158;
					break;
				case 199:
					num = 104;
					break;
				case 200:
					num = 116;
					break;
				case 201:
					num = 113;
					break;
				case 202:
					num = 114;
					break;
				case 203:
					num = 115;
					break;
				case 204:
					num = 120;
					break;
				case 205:
					num = 117;
					break;
				case 206:
					num = 118;
					break;
				case 207:
					num = 119;
					break;
				case 208:
					num = 172;
					break;
				case 209:
					num = 123;
					break;
				case 210:
					num = 237;
					break;
				case 211:
					num = 238;
					break;
				case 212:
					num = 235;
					break;
				case 213:
					num = 239;
					break;
				case 214:
					num = 236;
					break;
				case 215:
					num = 191;
					break;
				case 216:
					num = 128;
					break;
				case 217:
					num = 253;
					break;
				case 218:
					num = 254;
					break;
				case 219:
					num = 251;
					break;
				case 220:
					num = 252;
					break;
				case 221:
					num = 173;
					break;
				case 222:
					num = 174;
					break;
				case 223:
					num = 89;
					break;
				case 224:
					num = 68;
					break;
				case 225:
					num = 69;
					break;
				case 226:
					num = 66;
					break;
				case 227:
					num = 70;
					break;
				case 228:
					num = 67;
					break;
				case 229:
					num = 71;
					break;
				case 230:
					num = 156;
					break;
				case 231:
					num = 72;
					break;
				case 232:
					num = 84;
					break;
				case 233:
					num = 81;
					break;
				case 234:
					num = 82;
					break;
				case 235:
					num = 83;
					break;
				case 236:
					num = 88;
					break;
				case 237:
					num = 85;
					break;
				case 238:
					num = 86;
					break;
				case 239:
					num = 87;
					break;
				case 240:
					num = 140;
					break;
				case 241:
					num = 106;
					break;
				case 242:
					num = 205;
					break;
				case 243:
					num = 206;
					break;
				case 244:
					num = 203;
					break;
				case 245:
					num = 207;
					break;
				case 246:
					num = 204;
					break;
				case 247:
					num = 225;
					break;
				case 248:
					num = 112;
					break;
				case 249:
					num = 221;
					break;
				case 250:
					num = 222;
					break;
				case 251:
					num = 219;
					break;
				case 252:
					num = 220;
					break;
				case 253:
					num = 141;
					break;
				case 254:
					num = 142;
					break;
				case 255:
					num = 223;
					break;
				case 8254:
					num = 188;
					break;
				case 8364:
					num = 159;
					break;
				case 65281:
					num = 187;
					break;
				case 65282:
					num = 127;
					break;
				case 65283:
					num = 105;
					break;
				case 65284:
					num = 91;
					break;
				case 65285:
					num = 108;
					break;
				case 65286:
					num = 80;
					break;
				case 65287:
					num = 125;
					break;
				case 65288:
					num = 77;
					break;
				case 65289:
					num = 93;
					break;
				case 65290:
					num = 92;
					break;
				case 65291:
					num = 78;
					break;
				case 65292:
					num = 107;
					break;
				case 65293:
					num = 96;
					break;
				case 65294:
					num = 75;
					break;
				case 65295:
					num = 97;
					break;
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
					num -= 65056;
					break;
				case 65306:
					num = 122;
					break;
				case 65307:
					num = 94;
					break;
				case 65308:
					num = 76;
					break;
				case 65309:
					num = 126;
					break;
				case 65310:
					num = 110;
					break;
				case 65311:
					num = 111;
					break;
				case 65312:
					num = 124;
					break;
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
					num -= 65120;
					break;
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
					num -= 65113;
					break;
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
					num -= 65105;
					break;
				case 65339:
					num = 74;
					break;
				case 65340:
					num = 224;
					break;
				case 65341:
					num = 90;
					break;
				case 65342:
					num = 186;
					break;
				case 65343:
					num = 109;
					break;
				case 65344:
					num = 121;
					break;
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
				case 65353:
					num -= 65216;
					break;
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
				case 65361:
				case 65362:
					num -= 65209;
					break;
				case 65363:
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
					num -= 65201;
					break;
				case 65371:
					num = 192;
					break;
				case 65372:
					num = 79;
					break;
				case 65373:
					num = 208;
					break;
				case 65374:
					num = 189;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 24:
				case 25:
				case 28:
				case 29:
				case 30:
				case 31:
				case 182:
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
public class ENCibm1145 : CP1145
{
}
[Serializable]
public class CP1146 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
		'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
		'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
		'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
		'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
		'\u0014', '\u0015', '\u009e', '\u001a', ' ', '\u00a0', 'â', 'ä', 'à', 'á',
		'ã', 'å', 'ç', 'ñ', '$', '.', '<', '(', '+', '|',
		'&', 'é', 'ê', 'ë', 'è', 'í', 'î', 'ï', 'ì', 'ß',
		'!', '£', '*', ')', ';', '¬', '-', '/', 'Â', 'Ä',
		'À', 'Á', 'Ã', 'Å', 'Ç', 'Ñ', '¦', ',', '%', '_',
		'>', '?', 'ø', 'É', 'Ê', 'Ë', 'È', 'Í', 'Î', 'Ï',
		'Ì', '`', ':', '#', '@', '\'', '=', '"', 'Ø', 'a',
		'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', '«', '»',
		'ð', 'ý', 'þ', '±', '°', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', 'ª', 'º', 'æ', '\u00b8', 'Æ', '€',
		'µ', '\u00af', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'¡', '¿', 'Ð', 'Ý', 'Þ', '®', '¢', '[', '¥', '·',
		'©', '§', '¶', '¼', '½', '¾', '^', ']', '~', '\u00a8',
		'\u00b4', '×', '{', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
		'H', 'I', '\u00ad', 'ô', 'ö', 'ò', 'ó', 'õ', '}', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', '¹', 'û',
		'ü', 'ù', 'ú', 'ÿ', '\\', '÷', 'S', 'T', 'U', 'V',
		'W', 'X', 'Y', 'Z', '²', 'Ô', 'Ö', 'Ò', 'Ó', 'Õ',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'³', 'Û', 'Ü', 'Ù', 'Ú', '\u009f'
	};

	public CP1146()
		: base(1146, ToChars, "IBM EBCDIC (United Kingdom with Euro)", "ibm1146", "ibm1146", "ibm1146", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
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
			if (num >= 4)
			{
				switch (num)
				{
				case 4:
					num = 55;
					break;
				case 5:
					num = 45;
					break;
				case 6:
					num = 46;
					break;
				case 7:
					num = 47;
					break;
				case 8:
					num = 22;
					break;
				case 9:
					num = 5;
					break;
				case 10:
					num = 37;
					break;
				case 20:
					num = 60;
					break;
				case 21:
					num = 61;
					break;
				case 22:
					num = 50;
					break;
				case 23:
					num = 38;
					break;
				case 26:
					num = 63;
					break;
				case 27:
					num = 39;
					break;
				case 32:
					num = 64;
					break;
				case 33:
					num = 90;
					break;
				case 34:
					num = 127;
					break;
				case 35:
					num = 123;
					break;
				case 36:
					num = 74;
					break;
				case 37:
					num = 108;
					break;
				case 38:
					num = 80;
					break;
				case 39:
					num = 125;
					break;
				case 40:
					num = 77;
					break;
				case 41:
					num = 93;
					break;
				case 42:
					num = 92;
					break;
				case 43:
					num = 78;
					break;
				case 44:
					num = 107;
					break;
				case 45:
					num = 96;
					break;
				case 46:
					num = 75;
					break;
				case 47:
					num = 97;
					break;
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
					num += 192;
					break;
				case 58:
					num = 122;
					break;
				case 59:
					num = 94;
					break;
				case 60:
					num = 76;
					break;
				case 61:
					num = 126;
					break;
				case 62:
					num = 110;
					break;
				case 63:
					num = 111;
					break;
				case 64:
					num = 124;
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
					num += 128;
					break;
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
					num += 135;
					break;
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
					num += 143;
					break;
				case 91:
					num = 177;
					break;
				case 92:
					num = 224;
					break;
				case 93:
					num = 187;
					break;
				case 94:
					num = 186;
					break;
				case 95:
					num = 109;
					break;
				case 96:
					num = 121;
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
					num += 32;
					break;
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
					num += 39;
					break;
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
					num += 47;
					break;
				case 123:
					num = 192;
					break;
				case 124:
					num = 79;
					break;
				case 125:
					num = 208;
					break;
				case 126:
					num = 188;
					break;
				case 127:
					num = 7;
					break;
				case 128:
				case 129:
				case 130:
				case 131:
				case 132:
					num -= 96;
					break;
				case 133:
					num = 21;
					break;
				case 134:
					num = 6;
					break;
				case 135:
					num = 23;
					break;
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
					num -= 96;
					break;
				case 141:
					num = 9;
					break;
				case 142:
					num = 10;
					break;
				case 143:
					num = 27;
					break;
				case 144:
					num = 48;
					break;
				case 145:
					num = 49;
					break;
				case 146:
					num = 26;
					break;
				case 147:
				case 148:
				case 149:
				case 150:
					num -= 96;
					break;
				case 151:
					num = 8;
					break;
				case 152:
				case 153:
				case 154:
				case 155:
					num -= 96;
					break;
				case 156:
					num = 4;
					break;
				case 157:
					num = 20;
					break;
				case 158:
					num = 62;
					break;
				case 159:
					num = 255;
					break;
				case 160:
					num = 65;
					break;
				case 161:
					num = 170;
					break;
				case 162:
					num = 176;
					break;
				case 163:
					num = 91;
					break;
				case 165:
					num = 178;
					break;
				case 166:
					num = 106;
					break;
				case 167:
					num = 181;
					break;
				case 168:
					num = 189;
					break;
				case 169:
					num = 180;
					break;
				case 170:
					num = 154;
					break;
				case 171:
					num = 138;
					break;
				case 172:
					num = 95;
					break;
				case 173:
					num = 202;
					break;
				case 174:
					num = 175;
					break;
				case 175:
					num = 161;
					break;
				case 176:
					num = 144;
					break;
				case 177:
					num = 143;
					break;
				case 178:
					num = 234;
					break;
				case 179:
					num = 250;
					break;
				case 180:
					num = 190;
					break;
				case 181:
					num = 160;
					break;
				case 183:
					num = 179;
					break;
				case 184:
					num = 157;
					break;
				case 185:
					num = 218;
					break;
				case 186:
					num = 155;
					break;
				case 187:
					num = 139;
					break;
				case 188:
					num = 183;
					break;
				case 189:
					num = 184;
					break;
				case 190:
					num = 185;
					break;
				case 191:
					num = 171;
					break;
				case 192:
					num = 100;
					break;
				case 193:
					num = 101;
					break;
				case 194:
					num = 98;
					break;
				case 195:
					num = 102;
					break;
				case 196:
					num = 99;
					break;
				case 197:
					num = 103;
					break;
				case 198:
					num = 158;
					break;
				case 199:
					num = 104;
					break;
				case 200:
					num = 116;
					break;
				case 201:
					num = 113;
					break;
				case 202:
					num = 114;
					break;
				case 203:
					num = 115;
					break;
				case 204:
					num = 120;
					break;
				case 205:
					num = 117;
					break;
				case 206:
					num = 118;
					break;
				case 207:
					num = 119;
					break;
				case 208:
					num = 172;
					break;
				case 209:
					num = 105;
					break;
				case 210:
					num = 237;
					break;
				case 211:
					num = 238;
					break;
				case 212:
					num = 235;
					break;
				case 213:
					num = 239;
					break;
				case 214:
					num = 236;
					break;
				case 215:
					num = 191;
					break;
				case 216:
					num = 128;
					break;
				case 217:
					num = 253;
					break;
				case 218:
					num = 254;
					break;
				case 219:
					num = 251;
					break;
				case 220:
					num = 252;
					break;
				case 221:
					num = 173;
					break;
				case 222:
					num = 174;
					break;
				case 223:
					num = 89;
					break;
				case 224:
					num = 68;
					break;
				case 225:
					num = 69;
					break;
				case 226:
					num = 66;
					break;
				case 227:
					num = 70;
					break;
				case 228:
					num = 67;
					break;
				case 229:
					num = 71;
					break;
				case 230:
					num = 156;
					break;
				case 231:
					num = 72;
					break;
				case 232:
					num = 84;
					break;
				case 233:
					num = 81;
					break;
				case 234:
					num = 82;
					break;
				case 235:
					num = 83;
					break;
				case 236:
					num = 88;
					break;
				case 237:
					num = 85;
					break;
				case 238:
					num = 86;
					break;
				case 239:
					num = 87;
					break;
				case 240:
					num = 140;
					break;
				case 241:
					num = 73;
					break;
				case 242:
					num = 205;
					break;
				case 243:
					num = 206;
					break;
				case 244:
					num = 203;
					break;
				case 245:
					num = 207;
					break;
				case 246:
					num = 204;
					break;
				case 247:
					num = 225;
					break;
				case 248:
					num = 112;
					break;
				case 249:
					num = 221;
					break;
				case 250:
					num = 222;
					break;
				case 251:
					num = 219;
					break;
				case 252:
					num = 220;
					break;
				case 253:
					num = 141;
					break;
				case 254:
					num = 142;
					break;
				case 255:
					num = 223;
					break;
				case 8254:
					num = 161;
					break;
				case 8364:
					num = 159;
					break;
				case 65281:
					num = 90;
					break;
				case 65282:
					num = 127;
					break;
				case 65283:
					num = 123;
					break;
				case 65284:
					num = 74;
					break;
				case 65285:
					num = 108;
					break;
				case 65286:
					num = 80;
					break;
				case 65287:
					num = 125;
					break;
				case 65288:
					num = 77;
					break;
				case 65289:
					num = 93;
					break;
				case 65290:
					num = 92;
					break;
				case 65291:
					num = 78;
					break;
				case 65292:
					num = 107;
					break;
				case 65293:
					num = 96;
					break;
				case 65294:
					num = 75;
					break;
				case 65295:
					num = 97;
					break;
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
					num -= 65056;
					break;
				case 65306:
					num = 122;
					break;
				case 65307:
					num = 94;
					break;
				case 65308:
					num = 76;
					break;
				case 65309:
					num = 126;
					break;
				case 65310:
					num = 110;
					break;
				case 65311:
					num = 111;
					break;
				case 65312:
					num = 124;
					break;
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
					num -= 65120;
					break;
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
					num -= 65113;
					break;
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
					num -= 65105;
					break;
				case 65339:
					num = 177;
					break;
				case 65340:
					num = 224;
					break;
				case 65341:
					num = 187;
					break;
				case 65342:
					num = 186;
					break;
				case 65343:
					num = 109;
					break;
				case 65344:
					num = 121;
					break;
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
				case 65353:
					num -= 65216;
					break;
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
				case 65361:
				case 65362:
					num -= 65209;
					break;
				case 65363:
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
					num -= 65201;
					break;
				case 65371:
					num = 192;
					break;
				case 65372:
					num = 79;
					break;
				case 65373:
					num = 208;
					break;
				case 65374:
					num = 188;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 24:
				case 25:
				case 28:
				case 29:
				case 30:
				case 31:
				case 182:
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
public class ENCibm1146 : CP1146
{
}
[Serializable]
public class CP1147 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
		'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
		'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
		'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
		'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
		'\u0014', '\u0015', '\u009e', '\u001a', ' ', '\u00a0', 'â', 'ä', '@', 'á',
		'ã', 'å', '\\', 'ñ', '°', '.', '<', '(', '+', '!',
		'&', '{', 'ê', 'ë', '}', 'í', 'î', 'ï', 'ì', 'ß',
		'§', '$', '*', ')', ';', '^', '-', '/', 'Â', 'Ä',
		'À', 'Á', 'Ã', 'Å', 'Ç', 'Ñ', 'ù', ',', '%', '_',
		'>', '?', 'ø', 'É', 'Ê', 'Ë', 'È', 'Í', 'Î', 'Ï',
		'Ì', 'µ', ':', '£', 'à', '\'', '=', '"', 'Ø', 'a',
		'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', '«', '»',
		'ð', 'ý', 'þ', '±', '[', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', 'ª', 'º', 'æ', '\u00b8', 'Æ', '€',
		'`', '\u00a8', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'¡', '¿', 'Ð', 'Ý', 'Þ', '®', '¢', '#', '¥', '·',
		'©', ']', '¶', '¼', '½', '¾', '¬', '|', '\u00af', '~',
		'\u00b4', '×', 'é', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
		'H', 'I', '\u00ad', 'ô', 'ö', 'ò', 'ó', 'õ', 'è', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', '¹', 'û',
		'ü', '¦', 'ú', 'ÿ', 'ç', '÷', 'S', 'T', 'U', 'V',
		'W', 'X', 'Y', 'Z', '²', 'Ô', 'Ö', 'Ò', 'Ó', 'Õ',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'³', 'Û', 'Ü', 'Ù', 'Ú', '\u009f'
	};

	public CP1147()
		: base(1147, ToChars, "IBM EBCDIC (France with Euro)", "ibm1147", "ibm1147", "ibm1147", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
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
			if (num >= 4)
			{
				switch (num)
				{
				case 4:
					num = 55;
					break;
				case 5:
					num = 45;
					break;
				case 6:
					num = 46;
					break;
				case 7:
					num = 47;
					break;
				case 8:
					num = 22;
					break;
				case 9:
					num = 5;
					break;
				case 10:
					num = 37;
					break;
				case 20:
					num = 60;
					break;
				case 21:
					num = 61;
					break;
				case 22:
					num = 50;
					break;
				case 23:
					num = 38;
					break;
				case 26:
					num = 63;
					break;
				case 27:
					num = 39;
					break;
				case 32:
					num = 64;
					break;
				case 33:
					num = 79;
					break;
				case 34:
					num = 127;
					break;
				case 35:
					num = 177;
					break;
				case 36:
					num = 91;
					break;
				case 37:
					num = 108;
					break;
				case 38:
					num = 80;
					break;
				case 39:
					num = 125;
					break;
				case 40:
					num = 77;
					break;
				case 41:
					num = 93;
					break;
				case 42:
					num = 92;
					break;
				case 43:
					num = 78;
					break;
				case 44:
					num = 107;
					break;
				case 45:
					num = 96;
					break;
				case 46:
					num = 75;
					break;
				case 47:
					num = 97;
					break;
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
					num += 192;
					break;
				case 58:
					num = 122;
					break;
				case 59:
					num = 94;
					break;
				case 60:
					num = 76;
					break;
				case 61:
					num = 126;
					break;
				case 62:
					num = 110;
					break;
				case 63:
					num = 111;
					break;
				case 64:
					num = 68;
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
					num += 128;
					break;
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
					num += 135;
					break;
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
					num += 143;
					break;
				case 91:
					num = 144;
					break;
				case 92:
					num = 72;
					break;
				case 93:
					num = 181;
					break;
				case 94:
					num = 95;
					break;
				case 95:
					num = 109;
					break;
				case 96:
					num = 160;
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
					num += 32;
					break;
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
					num += 39;
					break;
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
					num += 47;
					break;
				case 123:
					num = 81;
					break;
				case 124:
					num = 187;
					break;
				case 125:
					num = 84;
					break;
				case 126:
					num = 189;
					break;
				case 127:
					num = 7;
					break;
				case 128:
				case 129:
				case 130:
				case 131:
				case 132:
					num -= 96;
					break;
				case 133:
					num = 21;
					break;
				case 134:
					num = 6;
					break;
				case 135:
					num = 23;
					break;
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
					num -= 96;
					break;
				case 141:
					num = 9;
					break;
				case 142:
					num = 10;
					break;
				case 143:
					num = 27;
					break;
				case 144:
					num = 48;
					break;
				case 145:
					num = 49;
					break;
				case 146:
					num = 26;
					break;
				case 147:
				case 148:
				case 149:
				case 150:
					num -= 96;
					break;
				case 151:
					num = 8;
					break;
				case 152:
				case 153:
				case 154:
				case 155:
					num -= 96;
					break;
				case 156:
					num = 4;
					break;
				case 157:
					num = 20;
					break;
				case 158:
					num = 62;
					break;
				case 159:
					num = 255;
					break;
				case 160:
					num = 65;
					break;
				case 161:
					num = 170;
					break;
				case 162:
					num = 176;
					break;
				case 163:
					num = 123;
					break;
				case 165:
					num = 178;
					break;
				case 166:
					num = 221;
					break;
				case 167:
					num = 90;
					break;
				case 168:
					num = 161;
					break;
				case 169:
					num = 180;
					break;
				case 170:
					num = 154;
					break;
				case 171:
					num = 138;
					break;
				case 172:
					num = 186;
					break;
				case 173:
					num = 202;
					break;
				case 174:
					num = 175;
					break;
				case 175:
					num = 188;
					break;
				case 176:
					num = 74;
					break;
				case 177:
					num = 143;
					break;
				case 178:
					num = 234;
					break;
				case 179:
					num = 250;
					break;
				case 180:
					num = 190;
					break;
				case 181:
					num = 121;
					break;
				case 183:
					num = 179;
					break;
				case 184:
					num = 157;
					break;
				case 185:
					num = 218;
					break;
				case 186:
					num = 155;
					break;
				case 187:
					num = 139;
					break;
				case 188:
					num = 183;
					break;
				case 189:
					num = 184;
					break;
				case 190:
					num = 185;
					break;
				case 191:
					num = 171;
					break;
				case 192:
					num = 100;
					break;
				case 193:
					num = 101;
					break;
				case 194:
					num = 98;
					break;
				case 195:
					num = 102;
					break;
				case 196:
					num = 99;
					break;
				case 197:
					num = 103;
					break;
				case 198:
					num = 158;
					break;
				case 199:
					num = 104;
					break;
				case 200:
					num = 116;
					break;
				case 201:
					num = 113;
					break;
				case 202:
					num = 114;
					break;
				case 203:
					num = 115;
					break;
				case 204:
					num = 120;
					break;
				case 205:
					num = 117;
					break;
				case 206:
					num = 118;
					break;
				case 207:
					num = 119;
					break;
				case 208:
					num = 172;
					break;
				case 209:
					num = 105;
					break;
				case 210:
					num = 237;
					break;
				case 211:
					num = 238;
					break;
				case 212:
					num = 235;
					break;
				case 213:
					num = 239;
					break;
				case 214:
					num = 236;
					break;
				case 215:
					num = 191;
					break;
				case 216:
					num = 128;
					break;
				case 217:
					num = 253;
					break;
				case 218:
					num = 254;
					break;
				case 219:
					num = 251;
					break;
				case 220:
					num = 252;
					break;
				case 221:
					num = 173;
					break;
				case 222:
					num = 174;
					break;
				case 223:
					num = 89;
					break;
				case 224:
					num = 124;
					break;
				case 225:
					num = 69;
					break;
				case 226:
					num = 66;
					break;
				case 227:
					num = 70;
					break;
				case 228:
					num = 67;
					break;
				case 229:
					num = 71;
					break;
				case 230:
					num = 156;
					break;
				case 231:
					num = 224;
					break;
				case 232:
					num = 208;
					break;
				case 233:
					num = 192;
					break;
				case 234:
					num = 82;
					break;
				case 235:
					num = 83;
					break;
				case 236:
					num = 88;
					break;
				case 237:
					num = 85;
					break;
				case 238:
					num = 86;
					break;
				case 239:
					num = 87;
					break;
				case 240:
					num = 140;
					break;
				case 241:
					num = 73;
					break;
				case 242:
					num = 205;
					break;
				case 243:
					num = 206;
					break;
				case 244:
					num = 203;
					break;
				case 245:
					num = 207;
					break;
				case 246:
					num = 204;
					break;
				case 247:
					num = 225;
					break;
				case 248:
					num = 112;
					break;
				case 249:
					num = 106;
					break;
				case 250:
					num = 222;
					break;
				case 251:
					num = 219;
					break;
				case 252:
					num = 220;
					break;
				case 253:
					num = 141;
					break;
				case 254:
					num = 142;
					break;
				case 255:
					num = 223;
					break;
				case 8254:
					num = 188;
					break;
				case 8364:
					num = 159;
					break;
				case 65281:
					num = 79;
					break;
				case 65282:
					num = 127;
					break;
				case 65283:
					num = 177;
					break;
				case 65284:
					num = 91;
					break;
				case 65285:
					num = 108;
					break;
				case 65286:
					num = 80;
					break;
				case 65287:
					num = 125;
					break;
				case 65288:
					num = 77;
					break;
				case 65289:
					num = 93;
					break;
				case 65290:
					num = 92;
					break;
				case 65291:
					num = 78;
					break;
				case 65292:
					num = 107;
					break;
				case 65293:
					num = 96;
					break;
				case 65294:
					num = 75;
					break;
				case 65295:
					num = 97;
					break;
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
					num -= 65056;
					break;
				case 65306:
					num = 122;
					break;
				case 65307:
					num = 94;
					break;
				case 65308:
					num = 76;
					break;
				case 65309:
					num = 126;
					break;
				case 65310:
					num = 110;
					break;
				case 65311:
					num = 111;
					break;
				case 65312:
					num = 68;
					break;
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
					num -= 65120;
					break;
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
					num -= 65113;
					break;
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
					num -= 65105;
					break;
				case 65339:
					num = 144;
					break;
				case 65340:
					num = 72;
					break;
				case 65341:
					num = 181;
					break;
				case 65342:
					num = 95;
					break;
				case 65343:
					num = 109;
					break;
				case 65344:
					num = 160;
					break;
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
				case 65353:
					num -= 65216;
					break;
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
				case 65361:
				case 65362:
					num -= 65209;
					break;
				case 65363:
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
					num -= 65201;
					break;
				case 65371:
					num = 81;
					break;
				case 65372:
					num = 187;
					break;
				case 65373:
					num = 84;
					break;
				case 65374:
					num = 189;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 24:
				case 25:
				case 28:
				case 29:
				case 30:
				case 31:
				case 182:
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
public class ENCibm1147 : CP1147
{
}
[Serializable]
public class CP1148 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
		'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
		'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
		'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
		'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
		'\u0014', '\u0015', '\u009e', '\u001a', ' ', '\u00a0', 'â', 'ä', 'à', 'á',
		'ã', 'å', 'ç', 'ñ', '[', '.', '<', '(', '+', '!',
		'&', 'é', 'ê', 'ë', 'è', 'í', 'î', 'ï', 'ì', 'ß',
		']', '$', '*', ')', ';', '^', '-', '/', 'Â', 'Ä',
		'À', 'Á', 'Ã', 'Å', 'Ç', 'Ñ', '¦', ',', '%', '_',
		'>', '?', 'ø', 'É', 'Ê', 'Ë', 'È', 'Í', 'Î', 'Ï',
		'Ì', '`', ':', '#', '@', '\'', '=', '"', 'Ø', 'a',
		'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', '«', '»',
		'ð', 'ý', 'þ', '±', '°', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', 'ª', 'º', 'æ', '\u00b8', 'Æ', '€',
		'µ', '~', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'¡', '¿', 'Ð', 'Ý', 'Þ', '®', '¢', '£', '¥', '·',
		'©', '§', '¶', '¼', '½', '¾', '¬', '|', '\u00af', '\u00a8',
		'\u00b4', '×', '{', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
		'H', 'I', '\u00ad', 'ô', 'ö', 'ò', 'ó', 'õ', '}', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', '¹', 'û',
		'ü', 'ù', 'ú', 'ÿ', '\\', '÷', 'S', 'T', 'U', 'V',
		'W', 'X', 'Y', 'Z', '²', 'Ô', 'Ö', 'Ò', 'Ó', 'Õ',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'³', 'Û', 'Ü', 'Ù', 'Ú', '\u009f'
	};

	public CP1148()
		: base(1148, ToChars, "IBM EBCDIC (International with Euro)", "ibm1148", "ibm1148", "ibm1148", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
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
			if (num >= 4)
			{
				switch (num)
				{
				case 4:
					num = 55;
					break;
				case 5:
					num = 45;
					break;
				case 6:
					num = 46;
					break;
				case 7:
					num = 47;
					break;
				case 8:
					num = 22;
					break;
				case 9:
					num = 5;
					break;
				case 10:
					num = 37;
					break;
				case 20:
					num = 60;
					break;
				case 21:
					num = 61;
					break;
				case 22:
					num = 50;
					break;
				case 23:
					num = 38;
					break;
				case 26:
					num = 63;
					break;
				case 27:
					num = 39;
					break;
				case 32:
					num = 64;
					break;
				case 33:
					num = 79;
					break;
				case 34:
					num = 127;
					break;
				case 35:
					num = 123;
					break;
				case 36:
					num = 91;
					break;
				case 37:
					num = 108;
					break;
				case 38:
					num = 80;
					break;
				case 39:
					num = 125;
					break;
				case 40:
					num = 77;
					break;
				case 41:
					num = 93;
					break;
				case 42:
					num = 92;
					break;
				case 43:
					num = 78;
					break;
				case 44:
					num = 107;
					break;
				case 45:
					num = 96;
					break;
				case 46:
					num = 75;
					break;
				case 47:
					num = 97;
					break;
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
					num += 192;
					break;
				case 58:
					num = 122;
					break;
				case 59:
					num = 94;
					break;
				case 60:
					num = 76;
					break;
				case 61:
					num = 126;
					break;
				case 62:
					num = 110;
					break;
				case 63:
					num = 111;
					break;
				case 64:
					num = 124;
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
					num += 128;
					break;
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
					num += 135;
					break;
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
					num += 143;
					break;
				case 91:
					num = 74;
					break;
				case 92:
					num = 224;
					break;
				case 93:
					num = 90;
					break;
				case 94:
					num = 95;
					break;
				case 95:
					num = 109;
					break;
				case 96:
					num = 121;
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
					num += 32;
					break;
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
					num += 39;
					break;
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
					num += 47;
					break;
				case 123:
					num = 192;
					break;
				case 124:
					num = 187;
					break;
				case 125:
					num = 208;
					break;
				case 126:
					num = 161;
					break;
				case 127:
					num = 7;
					break;
				case 128:
				case 129:
				case 130:
				case 131:
				case 132:
					num -= 96;
					break;
				case 133:
					num = 21;
					break;
				case 134:
					num = 6;
					break;
				case 135:
					num = 23;
					break;
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
					num -= 96;
					break;
				case 141:
					num = 9;
					break;
				case 142:
					num = 10;
					break;
				case 143:
					num = 27;
					break;
				case 144:
					num = 48;
					break;
				case 145:
					num = 49;
					break;
				case 146:
					num = 26;
					break;
				case 147:
				case 148:
				case 149:
				case 150:
					num -= 96;
					break;
				case 151:
					num = 8;
					break;
				case 152:
				case 153:
				case 154:
				case 155:
					num -= 96;
					break;
				case 156:
					num = 4;
					break;
				case 157:
					num = 20;
					break;
				case 158:
					num = 62;
					break;
				case 159:
					num = 255;
					break;
				case 160:
					num = 65;
					break;
				case 161:
					num = 170;
					break;
				case 162:
					num = 176;
					break;
				case 163:
					num = 177;
					break;
				case 165:
					num = 178;
					break;
				case 166:
					num = 106;
					break;
				case 167:
					num = 181;
					break;
				case 168:
					num = 189;
					break;
				case 169:
					num = 180;
					break;
				case 170:
					num = 154;
					break;
				case 171:
					num = 138;
					break;
				case 172:
					num = 186;
					break;
				case 173:
					num = 202;
					break;
				case 174:
					num = 175;
					break;
				case 175:
					num = 188;
					break;
				case 176:
					num = 144;
					break;
				case 177:
					num = 143;
					break;
				case 178:
					num = 234;
					break;
				case 179:
					num = 250;
					break;
				case 180:
					num = 190;
					break;
				case 181:
					num = 160;
					break;
				case 183:
					num = 179;
					break;
				case 184:
					num = 157;
					break;
				case 185:
					num = 218;
					break;
				case 186:
					num = 155;
					break;
				case 187:
					num = 139;
					break;
				case 188:
					num = 183;
					break;
				case 189:
					num = 184;
					break;
				case 190:
					num = 185;
					break;
				case 191:
					num = 171;
					break;
				case 192:
					num = 100;
					break;
				case 193:
					num = 101;
					break;
				case 194:
					num = 98;
					break;
				case 195:
					num = 102;
					break;
				case 196:
					num = 99;
					break;
				case 197:
					num = 103;
					break;
				case 198:
					num = 158;
					break;
				case 199:
					num = 104;
					break;
				case 200:
					num = 116;
					break;
				case 201:
					num = 113;
					break;
				case 202:
					num = 114;
					break;
				case 203:
					num = 115;
					break;
				case 204:
					num = 120;
					break;
				case 205:
					num = 117;
					break;
				case 206:
					num = 118;
					break;
				case 207:
					num = 119;
					break;
				case 208:
					num = 172;
					break;
				case 209:
					num = 105;
					break;
				case 210:
					num = 237;
					break;
				case 211:
					num = 238;
					break;
				case 212:
					num = 235;
					break;
				case 213:
					num = 239;
					break;
				case 214:
					num = 236;
					break;
				case 215:
					num = 191;
					break;
				case 216:
					num = 128;
					break;
				case 217:
					num = 253;
					break;
				case 218:
					num = 254;
					break;
				case 219:
					num = 251;
					break;
				case 220:
					num = 252;
					break;
				case 221:
					num = 173;
					break;
				case 222:
					num = 174;
					break;
				case 223:
					num = 89;
					break;
				case 224:
					num = 68;
					break;
				case 225:
					num = 69;
					break;
				case 226:
					num = 66;
					break;
				case 227:
					num = 70;
					break;
				case 228:
					num = 67;
					break;
				case 229:
					num = 71;
					break;
				case 230:
					num = 156;
					break;
				case 231:
					num = 72;
					break;
				case 232:
					num = 84;
					break;
				case 233:
					num = 81;
					break;
				case 234:
					num = 82;
					break;
				case 235:
					num = 83;
					break;
				case 236:
					num = 88;
					break;
				case 237:
					num = 85;
					break;
				case 238:
					num = 86;
					break;
				case 239:
					num = 87;
					break;
				case 240:
					num = 140;
					break;
				case 241:
					num = 73;
					break;
				case 242:
					num = 205;
					break;
				case 243:
					num = 206;
					break;
				case 244:
					num = 203;
					break;
				case 245:
					num = 207;
					break;
				case 246:
					num = 204;
					break;
				case 247:
					num = 225;
					break;
				case 248:
					num = 112;
					break;
				case 249:
					num = 221;
					break;
				case 250:
					num = 222;
					break;
				case 251:
					num = 219;
					break;
				case 252:
					num = 220;
					break;
				case 253:
					num = 141;
					break;
				case 254:
					num = 142;
					break;
				case 255:
					num = 223;
					break;
				case 8254:
					num = 188;
					break;
				case 8364:
					num = 159;
					break;
				case 65281:
					num = 79;
					break;
				case 65282:
					num = 127;
					break;
				case 65283:
					num = 123;
					break;
				case 65284:
					num = 91;
					break;
				case 65285:
					num = 108;
					break;
				case 65286:
					num = 80;
					break;
				case 65287:
					num = 125;
					break;
				case 65288:
					num = 77;
					break;
				case 65289:
					num = 93;
					break;
				case 65290:
					num = 92;
					break;
				case 65291:
					num = 78;
					break;
				case 65292:
					num = 107;
					break;
				case 65293:
					num = 96;
					break;
				case 65294:
					num = 75;
					break;
				case 65295:
					num = 97;
					break;
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
					num -= 65056;
					break;
				case 65306:
					num = 122;
					break;
				case 65307:
					num = 94;
					break;
				case 65308:
					num = 76;
					break;
				case 65309:
					num = 126;
					break;
				case 65310:
					num = 110;
					break;
				case 65311:
					num = 111;
					break;
				case 65312:
					num = 124;
					break;
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
					num -= 65120;
					break;
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
					num -= 65113;
					break;
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
					num -= 65105;
					break;
				case 65339:
					num = 74;
					break;
				case 65340:
					num = 224;
					break;
				case 65341:
					num = 90;
					break;
				case 65342:
					num = 95;
					break;
				case 65343:
					num = 109;
					break;
				case 65344:
					num = 121;
					break;
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
				case 65353:
					num -= 65216;
					break;
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
				case 65361:
				case 65362:
					num -= 65209;
					break;
				case 65363:
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
					num -= 65201;
					break;
				case 65371:
					num = 192;
					break;
				case 65372:
					num = 187;
					break;
				case 65373:
					num = 208;
					break;
				case 65374:
					num = 161;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 24:
				case 25:
				case 28:
				case 29:
				case 30:
				case 31:
				case 182:
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
public class ENCibm1148 : CP1148
{
}
[Serializable]
public class CP1149 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
		'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
		'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
		'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
		'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
		'\u0014', '\u0015', '\u009e', '\u001a', ' ', '\u00a0', 'â', 'ä', 'à', 'á',
		'ã', 'å', 'ç', 'ñ', 'Þ', '.', '<', '(', '+', '!',
		'&', 'é', 'ê', 'ë', 'è', 'í', 'î', 'ï', 'ì', 'ß',
		'Æ', '$', '*', ')', ';', 'Ö', '-', '/', 'Â', 'Ä',
		'À', 'Á', 'Ã', 'Å', 'Ç', 'Ñ', '¦', ',', '%', '_',
		'>', '?', 'ø', 'É', 'Ê', 'Ë', 'È', 'Í', 'Î', 'Ï',
		'Ì', 'ð', ':', '#', 'Ð', '\'', '=', '"', 'Ø', 'a',
		'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', '«', '»',
		'`', 'ý', '{', '±', '°', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', 'ª', 'º', '}', '\u00b8', ']', '€',
		'µ', 'ö', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'¡', '¿', '@', 'Ý', '[', '®', '¢', '£', '¥', '·',
		'©', '§', '¶', '¼', '½', '¾', '¬', '|', '\u00af', '\u00a8',
		'\\', '×', 'þ', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
		'H', 'I', '\u00ad', 'ô', '~', 'ò', 'ó', 'õ', 'æ', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', '¹', 'û',
		'ü', 'ù', 'ú', 'ÿ', '\u00b4', '÷', 'S', 'T', 'U', 'V',
		'W', 'X', 'Y', 'Z', '²', 'Ô', '^', 'Ò', 'Ó', 'Õ',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'³', 'Û', 'Ü', 'Ù', 'Ú', '\u009f'
	};

	public CP1149()
		: base(1149, ToChars, "IBM EBCDIC (Icelandic with Euro)", "ibm1149", "ibm1149", "ibm1149", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
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
			if (num >= 4)
			{
				switch (num)
				{
				case 4:
					num = 55;
					break;
				case 5:
					num = 45;
					break;
				case 6:
					num = 46;
					break;
				case 7:
					num = 47;
					break;
				case 8:
					num = 22;
					break;
				case 9:
					num = 5;
					break;
				case 10:
					num = 37;
					break;
				case 20:
					num = 60;
					break;
				case 21:
					num = 61;
					break;
				case 22:
					num = 50;
					break;
				case 23:
					num = 38;
					break;
				case 26:
					num = 63;
					break;
				case 27:
					num = 39;
					break;
				case 32:
					num = 64;
					break;
				case 33:
					num = 79;
					break;
				case 34:
					num = 127;
					break;
				case 35:
					num = 123;
					break;
				case 36:
					num = 91;
					break;
				case 37:
					num = 108;
					break;
				case 38:
					num = 80;
					break;
				case 39:
					num = 125;
					break;
				case 40:
					num = 77;
					break;
				case 41:
					num = 93;
					break;
				case 42:
					num = 92;
					break;
				case 43:
					num = 78;
					break;
				case 44:
					num = 107;
					break;
				case 45:
					num = 96;
					break;
				case 46:
					num = 75;
					break;
				case 47:
					num = 97;
					break;
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
					num += 192;
					break;
				case 58:
					num = 122;
					break;
				case 59:
					num = 94;
					break;
				case 60:
					num = 76;
					break;
				case 61:
					num = 126;
					break;
				case 62:
					num = 110;
					break;
				case 63:
					num = 111;
					break;
				case 64:
					num = 172;
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
					num += 128;
					break;
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
					num += 135;
					break;
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
					num += 143;
					break;
				case 91:
					num = 174;
					break;
				case 92:
					num = 190;
					break;
				case 93:
					num = 158;
					break;
				case 94:
					num = 236;
					break;
				case 95:
					num = 109;
					break;
				case 96:
					num = 140;
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
					num += 32;
					break;
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
					num += 39;
					break;
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
					num += 47;
					break;
				case 123:
					num = 142;
					break;
				case 124:
					num = 187;
					break;
				case 125:
					num = 156;
					break;
				case 126:
					num = 204;
					break;
				case 127:
					num = 7;
					break;
				case 128:
				case 129:
				case 130:
				case 131:
				case 132:
					num -= 96;
					break;
				case 133:
					num = 21;
					break;
				case 134:
					num = 6;
					break;
				case 135:
					num = 23;
					break;
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
					num -= 96;
					break;
				case 141:
					num = 9;
					break;
				case 142:
					num = 10;
					break;
				case 143:
					num = 27;
					break;
				case 144:
					num = 48;
					break;
				case 145:
					num = 49;
					break;
				case 146:
					num = 26;
					break;
				case 147:
				case 148:
				case 149:
				case 150:
					num -= 96;
					break;
				case 151:
					num = 8;
					break;
				case 152:
				case 153:
				case 154:
				case 155:
					num -= 96;
					break;
				case 156:
					num = 4;
					break;
				case 157:
					num = 20;
					break;
				case 158:
					num = 62;
					break;
				case 159:
					num = 255;
					break;
				case 160:
					num = 65;
					break;
				case 161:
					num = 170;
					break;
				case 162:
					num = 176;
					break;
				case 163:
					num = 177;
					break;
				case 165:
					num = 178;
					break;
				case 166:
					num = 106;
					break;
				case 167:
					num = 181;
					break;
				case 168:
					num = 189;
					break;
				case 169:
					num = 180;
					break;
				case 170:
					num = 154;
					break;
				case 171:
					num = 138;
					break;
				case 172:
					num = 186;
					break;
				case 173:
					num = 202;
					break;
				case 174:
					num = 175;
					break;
				case 175:
					num = 188;
					break;
				case 176:
					num = 144;
					break;
				case 177:
					num = 143;
					break;
				case 178:
					num = 234;
					break;
				case 179:
					num = 250;
					break;
				case 180:
					num = 224;
					break;
				case 181:
					num = 160;
					break;
				case 183:
					num = 179;
					break;
				case 184:
					num = 157;
					break;
				case 185:
					num = 218;
					break;
				case 186:
					num = 155;
					break;
				case 187:
					num = 139;
					break;
				case 188:
					num = 183;
					break;
				case 189:
					num = 184;
					break;
				case 190:
					num = 185;
					break;
				case 191:
					num = 171;
					break;
				case 192:
					num = 100;
					break;
				case 193:
					num = 101;
					break;
				case 194:
					num = 98;
					break;
				case 195:
					num = 102;
					break;
				case 196:
					num = 99;
					break;
				case 197:
					num = 103;
					break;
				case 198:
					num = 90;
					break;
				case 199:
					num = 104;
					break;
				case 200:
					num = 116;
					break;
				case 201:
					num = 113;
					break;
				case 202:
					num = 114;
					break;
				case 203:
					num = 115;
					break;
				case 204:
					num = 120;
					break;
				case 205:
					num = 117;
					break;
				case 206:
					num = 118;
					break;
				case 207:
					num = 119;
					break;
				case 208:
					num = 124;
					break;
				case 209:
					num = 105;
					break;
				case 210:
					num = 237;
					break;
				case 211:
					num = 238;
					break;
				case 212:
					num = 235;
					break;
				case 213:
					num = 239;
					break;
				case 214:
					num = 95;
					break;
				case 215:
					num = 191;
					break;
				case 216:
					num = 128;
					break;
				case 217:
					num = 253;
					break;
				case 218:
					num = 254;
					break;
				case 219:
					num = 251;
					break;
				case 220:
					num = 252;
					break;
				case 221:
					num = 173;
					break;
				case 222:
					num = 74;
					break;
				case 223:
					num = 89;
					break;
				case 224:
					num = 68;
					break;
				case 225:
					num = 69;
					break;
				case 226:
					num = 66;
					break;
				case 227:
					num = 70;
					break;
				case 228:
					num = 67;
					break;
				case 229:
					num = 71;
					break;
				case 230:
					num = 208;
					break;
				case 231:
					num = 72;
					break;
				case 232:
					num = 84;
					break;
				case 233:
					num = 81;
					break;
				case 234:
					num = 82;
					break;
				case 235:
					num = 83;
					break;
				case 236:
					num = 88;
					break;
				case 237:
					num = 85;
					break;
				case 238:
					num = 86;
					break;
				case 239:
					num = 87;
					break;
				case 240:
					num = 121;
					break;
				case 241:
					num = 73;
					break;
				case 242:
					num = 205;
					break;
				case 243:
					num = 206;
					break;
				case 244:
					num = 203;
					break;
				case 245:
					num = 207;
					break;
				case 246:
					num = 161;
					break;
				case 247:
					num = 225;
					break;
				case 248:
					num = 112;
					break;
				case 249:
					num = 221;
					break;
				case 250:
					num = 222;
					break;
				case 251:
					num = 219;
					break;
				case 252:
					num = 220;
					break;
				case 253:
					num = 141;
					break;
				case 254:
					num = 192;
					break;
				case 255:
					num = 223;
					break;
				case 8254:
					num = 188;
					break;
				case 8364:
					num = 159;
					break;
				case 65281:
					num = 79;
					break;
				case 65282:
					num = 127;
					break;
				case 65283:
					num = 123;
					break;
				case 65284:
					num = 91;
					break;
				case 65285:
					num = 108;
					break;
				case 65286:
					num = 80;
					break;
				case 65287:
					num = 125;
					break;
				case 65288:
					num = 77;
					break;
				case 65289:
					num = 93;
					break;
				case 65290:
					num = 92;
					break;
				case 65291:
					num = 78;
					break;
				case 65292:
					num = 107;
					break;
				case 65293:
					num = 96;
					break;
				case 65294:
					num = 75;
					break;
				case 65295:
					num = 97;
					break;
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
					num -= 65056;
					break;
				case 65306:
					num = 122;
					break;
				case 65307:
					num = 94;
					break;
				case 65308:
					num = 76;
					break;
				case 65309:
					num = 126;
					break;
				case 65310:
					num = 110;
					break;
				case 65311:
					num = 111;
					break;
				case 65312:
					num = 172;
					break;
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
					num -= 65120;
					break;
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
					num -= 65113;
					break;
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
					num -= 65105;
					break;
				case 65339:
					num = 174;
					break;
				case 65340:
					num = 190;
					break;
				case 65341:
					num = 158;
					break;
				case 65342:
					num = 236;
					break;
				case 65343:
					num = 109;
					break;
				case 65344:
					num = 140;
					break;
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
				case 65353:
					num -= 65216;
					break;
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
				case 65361:
				case 65362:
					num -= 65209;
					break;
				case 65363:
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
					num -= 65201;
					break;
				case 65371:
					num = 142;
					break;
				case 65372:
					num = 187;
					break;
				case 65373:
					num = 156;
					break;
				case 65374:
					num = 204;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 24:
				case 25:
				case 28:
				case 29:
				case 30:
				case 31:
				case 182:
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
public class ENCibm1149 : CP1149
{
}
[Serializable]
public class CP20273 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
		'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
		'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
		'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
		'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
		'\u0014', '\u0015', '\u009e', '\u001a', ' ', '\u00a0', 'â', '{', 'à', 'á',
		'ã', 'å', 'ç', 'ñ', 'Ä', '.', '<', '(', '+', '!',
		'&', 'é', 'ê', 'ë', 'è', 'í', 'î', 'ï', 'ì', '~',
		'Ü', '$', '*', ')', ';', '^', '-', '/', 'Â', '[',
		'À', 'Á', 'Ã', 'Å', 'Ç', 'Ñ', 'ö', ',', '%', '_',
		'>', '?', 'ø', 'É', 'Ê', 'Ë', 'È', 'Í', 'Î', 'Ï',
		'Ì', '`', ':', '#', '§', '\'', '=', '"', 'Ø', 'a',
		'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', '«', '»',
		'ð', 'ý', 'þ', '±', '°', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', 'ª', 'º', 'æ', '\u00b8', 'Æ', '¤',
		'µ', 'ß', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'¡', '¿', 'Ð', 'Ý', 'Þ', '®', '¢', '£', '¥', '·',
		'©', '@', '¶', '¼', '½', '¾', '¬', '|', '\u00af', '\u00a8',
		'\u00b4', '×', 'ä', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
		'H', 'I', '\u00ad', 'ô', '¦', 'ò', 'ó', 'õ', 'ü', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', '¹', 'û',
		'}', 'ù', 'ú', 'ÿ', 'Ö', '÷', 'S', 'T', 'U', 'V',
		'W', 'X', 'Y', 'Z', '²', 'Ô', '\\', 'Ò', 'Ó', 'Õ',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'³', 'Û', ']', 'Ù', 'Ú', '\u009f'
	};

	public CP20273()
		: base(20273, ToChars, "IBM EBCDIC (Germany)", "IBM273", "IBM273", "IBM273", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
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
			if (num >= 4)
			{
				switch (num)
				{
				case 4:
					num = 55;
					break;
				case 5:
					num = 45;
					break;
				case 6:
					num = 46;
					break;
				case 7:
					num = 47;
					break;
				case 8:
					num = 22;
					break;
				case 9:
					num = 5;
					break;
				case 10:
					num = 37;
					break;
				case 20:
					num = 60;
					break;
				case 21:
					num = 61;
					break;
				case 22:
					num = 50;
					break;
				case 23:
					num = 38;
					break;
				case 26:
					num = 63;
					break;
				case 27:
					num = 39;
					break;
				case 32:
					num = 64;
					break;
				case 33:
					num = 79;
					break;
				case 34:
					num = 127;
					break;
				case 35:
					num = 123;
					break;
				case 36:
					num = 91;
					break;
				case 37:
					num = 108;
					break;
				case 38:
					num = 80;
					break;
				case 39:
					num = 125;
					break;
				case 40:
					num = 77;
					break;
				case 41:
					num = 93;
					break;
				case 42:
					num = 92;
					break;
				case 43:
					num = 78;
					break;
				case 44:
					num = 107;
					break;
				case 45:
					num = 96;
					break;
				case 46:
					num = 75;
					break;
				case 47:
					num = 97;
					break;
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
					num += 192;
					break;
				case 58:
					num = 122;
					break;
				case 59:
					num = 94;
					break;
				case 60:
					num = 76;
					break;
				case 61:
					num = 126;
					break;
				case 62:
					num = 110;
					break;
				case 63:
					num = 111;
					break;
				case 64:
					num = 181;
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
					num += 128;
					break;
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
					num += 135;
					break;
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
					num += 143;
					break;
				case 91:
					num = 99;
					break;
				case 92:
					num = 236;
					break;
				case 93:
					num = 252;
					break;
				case 94:
					num = 95;
					break;
				case 95:
					num = 109;
					break;
				case 96:
					num = 121;
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
					num += 32;
					break;
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
					num += 39;
					break;
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
					num += 47;
					break;
				case 123:
					num = 67;
					break;
				case 124:
					num = 187;
					break;
				case 125:
					num = 220;
					break;
				case 126:
					num = 89;
					break;
				case 127:
					num = 7;
					break;
				case 128:
				case 129:
				case 130:
				case 131:
				case 132:
					num -= 96;
					break;
				case 133:
					num = 21;
					break;
				case 134:
					num = 6;
					break;
				case 135:
					num = 23;
					break;
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
					num -= 96;
					break;
				case 141:
					num = 9;
					break;
				case 142:
					num = 10;
					break;
				case 143:
					num = 27;
					break;
				case 144:
					num = 48;
					break;
				case 145:
					num = 49;
					break;
				case 146:
					num = 26;
					break;
				case 147:
				case 148:
				case 149:
				case 150:
					num -= 96;
					break;
				case 151:
					num = 8;
					break;
				case 152:
				case 153:
				case 154:
				case 155:
					num -= 96;
					break;
				case 156:
					num = 4;
					break;
				case 157:
					num = 20;
					break;
				case 158:
					num = 62;
					break;
				case 159:
					num = 255;
					break;
				case 160:
					num = 65;
					break;
				case 161:
					num = 170;
					break;
				case 162:
					num = 176;
					break;
				case 163:
					num = 177;
					break;
				case 164:
					num = 159;
					break;
				case 165:
					num = 178;
					break;
				case 166:
					num = 204;
					break;
				case 167:
					num = 124;
					break;
				case 168:
					num = 189;
					break;
				case 169:
					num = 180;
					break;
				case 170:
					num = 154;
					break;
				case 171:
					num = 138;
					break;
				case 172:
					num = 186;
					break;
				case 173:
					num = 202;
					break;
				case 174:
					num = 175;
					break;
				case 175:
					num = 188;
					break;
				case 176:
					num = 144;
					break;
				case 177:
					num = 143;
					break;
				case 178:
					num = 234;
					break;
				case 179:
					num = 250;
					break;
				case 180:
					num = 190;
					break;
				case 181:
					num = 160;
					break;
				case 183:
					num = 179;
					break;
				case 184:
					num = 157;
					break;
				case 185:
					num = 218;
					break;
				case 186:
					num = 155;
					break;
				case 187:
					num = 139;
					break;
				case 188:
					num = 183;
					break;
				case 189:
					num = 184;
					break;
				case 190:
					num = 185;
					break;
				case 191:
					num = 171;
					break;
				case 192:
					num = 100;
					break;
				case 193:
					num = 101;
					break;
				case 194:
					num = 98;
					break;
				case 195:
					num = 102;
					break;
				case 196:
					num = 74;
					break;
				case 197:
					num = 103;
					break;
				case 198:
					num = 158;
					break;
				case 199:
					num = 104;
					break;
				case 200:
					num = 116;
					break;
				case 201:
					num = 113;
					break;
				case 202:
					num = 114;
					break;
				case 203:
					num = 115;
					break;
				case 204:
					num = 120;
					break;
				case 205:
					num = 117;
					break;
				case 206:
					num = 118;
					break;
				case 207:
					num = 119;
					break;
				case 208:
					num = 172;
					break;
				case 209:
					num = 105;
					break;
				case 210:
					num = 237;
					break;
				case 211:
					num = 238;
					break;
				case 212:
					num = 235;
					break;
				case 213:
					num = 239;
					break;
				case 214:
					num = 224;
					break;
				case 215:
					num = 191;
					break;
				case 216:
					num = 128;
					break;
				case 217:
					num = 253;
					break;
				case 218:
					num = 254;
					break;
				case 219:
					num = 251;
					break;
				case 220:
					num = 90;
					break;
				case 221:
					num = 173;
					break;
				case 222:
					num = 174;
					break;
				case 223:
					num = 161;
					break;
				case 224:
					num = 68;
					break;
				case 225:
					num = 69;
					break;
				case 226:
					num = 66;
					break;
				case 227:
					num = 70;
					break;
				case 228:
					num = 192;
					break;
				case 229:
					num = 71;
					break;
				case 230:
					num = 156;
					break;
				case 231:
					num = 72;
					break;
				case 232:
					num = 84;
					break;
				case 233:
					num = 81;
					break;
				case 234:
					num = 82;
					break;
				case 235:
					num = 83;
					break;
				case 236:
					num = 88;
					break;
				case 237:
					num = 85;
					break;
				case 238:
					num = 86;
					break;
				case 239:
					num = 87;
					break;
				case 240:
					num = 140;
					break;
				case 241:
					num = 73;
					break;
				case 242:
					num = 205;
					break;
				case 243:
					num = 206;
					break;
				case 244:
					num = 203;
					break;
				case 245:
					num = 207;
					break;
				case 246:
					num = 106;
					break;
				case 247:
					num = 225;
					break;
				case 248:
					num = 112;
					break;
				case 249:
					num = 221;
					break;
				case 250:
					num = 222;
					break;
				case 251:
					num = 219;
					break;
				case 252:
					num = 208;
					break;
				case 253:
					num = 141;
					break;
				case 254:
					num = 142;
					break;
				case 255:
					num = 223;
					break;
				case 272:
					num = 172;
					break;
				case 8254:
					num = 188;
					break;
				case 65281:
					num = 79;
					break;
				case 65282:
					num = 127;
					break;
				case 65283:
					num = 123;
					break;
				case 65284:
					num = 91;
					break;
				case 65285:
					num = 108;
					break;
				case 65286:
					num = 80;
					break;
				case 65287:
					num = 125;
					break;
				case 65288:
					num = 77;
					break;
				case 65289:
					num = 93;
					break;
				case 65290:
					num = 92;
					break;
				case 65291:
					num = 78;
					break;
				case 65292:
					num = 107;
					break;
				case 65293:
					num = 96;
					break;
				case 65294:
					num = 75;
					break;
				case 65295:
					num = 97;
					break;
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
					num -= 65056;
					break;
				case 65306:
					num = 122;
					break;
				case 65307:
					num = 94;
					break;
				case 65308:
					num = 76;
					break;
				case 65309:
					num = 126;
					break;
				case 65310:
					num = 110;
					break;
				case 65311:
					num = 111;
					break;
				case 65312:
					num = 181;
					break;
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
					num -= 65120;
					break;
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
					num -= 65113;
					break;
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
					num -= 65105;
					break;
				case 65339:
					num = 99;
					break;
				case 65340:
					num = 236;
					break;
				case 65341:
					num = 252;
					break;
				case 65342:
					num = 95;
					break;
				case 65343:
					num = 109;
					break;
				case 65344:
					num = 121;
					break;
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
				case 65353:
					num -= 65216;
					break;
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
				case 65361:
				case 65362:
					num -= 65209;
					break;
				case 65363:
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
					num -= 65201;
					break;
				case 65371:
					num = 67;
					break;
				case 65372:
					num = 187;
					break;
				case 65373:
					num = 220;
					break;
				case 65374:
					num = 89;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 24:
				case 25:
				case 28:
				case 29:
				case 30:
				case 31:
				case 182:
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
public class ENCibm273 : CP20273
{
}
[Serializable]
public class CP20277 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
		'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
		'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
		'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
		'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
		'\u0014', '\u0015', '\u009e', '\u001a', ' ', '\u00a0', 'â', 'ä', 'à', 'á',
		'ã', '}', 'ç', 'ñ', '#', '.', '<', '(', '+', '!',
		'&', 'é', 'ê', 'ë', 'è', 'í', 'î', 'ï', 'ì', 'ß',
		'¤', 'Å', '*', ')', ';', '^', '-', '/', 'Â', 'Ä',
		'À', 'Á', 'Ã', '$', 'Ç', 'Ñ', 'ø', ',', '%', '_',
		'>', '?', '¦', 'É', 'Ê', 'Ë', 'È', 'Í', 'Î', 'Ï',
		'Ì', '`', ':', 'Æ', 'Ø', '\'', '=', '"', '@', 'a',
		'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', '«', '»',
		'ð', 'ý', 'þ', '±', '°', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', 'ª', 'º', '{', '\u00b8', '[', ']',
		'µ', 'ü', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'¡', '¿', 'Ð', 'Ý', 'Þ', '®', '¢', '£', '¥', '·',
		'©', '§', '¶', '¼', '½', '¾', '¬', '|', '\u00af', '\u00a8',
		'\u00b4', '×', 'æ', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
		'H', 'I', '\u00ad', 'ô', 'ö', 'ò', 'ó', 'õ', 'å', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', '¹', 'û',
		'~', 'ù', 'ú', 'ÿ', '\\', '÷', 'S', 'T', 'U', 'V',
		'W', 'X', 'Y', 'Z', '²', 'Ô', 'Ö', 'Ò', 'Ó', 'Õ',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'³', 'Û', 'Ü', 'Ù', 'Ú', '\u009f'
	};

	public CP20277()
		: base(20277, ToChars, "IBM EBCDIC (Denmark/Norway)", "IBM277", "IBM277", "IBM277", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
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
			if (num >= 4)
			{
				switch (num)
				{
				case 4:
					num = 55;
					break;
				case 5:
					num = 45;
					break;
				case 6:
					num = 46;
					break;
				case 7:
					num = 47;
					break;
				case 8:
					num = 22;
					break;
				case 9:
					num = 5;
					break;
				case 10:
					num = 37;
					break;
				case 20:
					num = 60;
					break;
				case 21:
					num = 61;
					break;
				case 22:
					num = 50;
					break;
				case 23:
					num = 38;
					break;
				case 26:
					num = 63;
					break;
				case 27:
					num = 39;
					break;
				case 32:
					num = 64;
					break;
				case 33:
					num = 79;
					break;
				case 34:
					num = 127;
					break;
				case 35:
					num = 74;
					break;
				case 36:
					num = 103;
					break;
				case 37:
					num = 108;
					break;
				case 38:
					num = 80;
					break;
				case 39:
					num = 125;
					break;
				case 40:
					num = 77;
					break;
				case 41:
					num = 93;
					break;
				case 42:
					num = 92;
					break;
				case 43:
					num = 78;
					break;
				case 44:
					num = 107;
					break;
				case 45:
					num = 96;
					break;
				case 46:
					num = 75;
					break;
				case 47:
					num = 97;
					break;
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
					num += 192;
					break;
				case 58:
					num = 122;
					break;
				case 59:
					num = 94;
					break;
				case 60:
					num = 76;
					break;
				case 61:
					num = 126;
					break;
				case 62:
					num = 110;
					break;
				case 63:
					num = 111;
					break;
				case 64:
					num = 128;
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
					num += 128;
					break;
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
					num += 135;
					break;
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
					num += 143;
					break;
				case 91:
					num = 158;
					break;
				case 92:
					num = 224;
					break;
				case 93:
					num = 159;
					break;
				case 94:
					num = 95;
					break;
				case 95:
					num = 109;
					break;
				case 96:
					num = 121;
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
					num += 32;
					break;
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
					num += 39;
					break;
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
					num += 47;
					break;
				case 123:
					num = 156;
					break;
				case 124:
					num = 187;
					break;
				case 125:
					num = 71;
					break;
				case 126:
					num = 220;
					break;
				case 127:
					num = 7;
					break;
				case 128:
				case 129:
				case 130:
				case 131:
				case 132:
					num -= 96;
					break;
				case 133:
					num = 21;
					break;
				case 134:
					num = 6;
					break;
				case 135:
					num = 23;
					break;
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
					num -= 96;
					break;
				case 141:
					num = 9;
					break;
				case 142:
					num = 10;
					break;
				case 143:
					num = 27;
					break;
				case 144:
					num = 48;
					break;
				case 145:
					num = 49;
					break;
				case 146:
					num = 26;
					break;
				case 147:
				case 148:
				case 149:
				case 150:
					num -= 96;
					break;
				case 151:
					num = 8;
					break;
				case 152:
				case 153:
				case 154:
				case 155:
					num -= 96;
					break;
				case 156:
					num = 4;
					break;
				case 157:
					num = 20;
					break;
				case 158:
					num = 62;
					break;
				case 159:
					num = 255;
					break;
				case 160:
					num = 65;
					break;
				case 161:
					num = 170;
					break;
				case 162:
					num = 176;
					break;
				case 163:
					num = 177;
					break;
				case 164:
					num = 90;
					break;
				case 165:
					num = 178;
					break;
				case 166:
					num = 112;
					break;
				case 167:
					num = 181;
					break;
				case 168:
					num = 189;
					break;
				case 169:
					num = 180;
					break;
				case 170:
					num = 154;
					break;
				case 171:
					num = 138;
					break;
				case 172:
					num = 186;
					break;
				case 173:
					num = 202;
					break;
				case 174:
					num = 175;
					break;
				case 175:
					num = 188;
					break;
				case 176:
					num = 144;
					break;
				case 177:
					num = 143;
					break;
				case 178:
					num = 234;
					break;
				case 179:
					num = 250;
					break;
				case 180:
					num = 190;
					break;
				case 181:
					num = 160;
					break;
				case 183:
					num = 179;
					break;
				case 184:
					num = 157;
					break;
				case 185:
					num = 218;
					break;
				case 186:
					num = 155;
					break;
				case 187:
					num = 139;
					break;
				case 188:
					num = 183;
					break;
				case 189:
					num = 184;
					break;
				case 190:
					num = 185;
					break;
				case 191:
					num = 171;
					break;
				case 192:
					num = 100;
					break;
				case 193:
					num = 101;
					break;
				case 194:
					num = 98;
					break;
				case 195:
					num = 102;
					break;
				case 196:
					num = 99;
					break;
				case 197:
					num = 91;
					break;
				case 198:
					num = 123;
					break;
				case 199:
					num = 104;
					break;
				case 200:
					num = 116;
					break;
				case 201:
					num = 113;
					break;
				case 202:
					num = 114;
					break;
				case 203:
					num = 115;
					break;
				case 204:
					num = 120;
					break;
				case 205:
					num = 117;
					break;
				case 206:
					num = 118;
					break;
				case 207:
					num = 119;
					break;
				case 208:
					num = 172;
					break;
				case 209:
					num = 105;
					break;
				case 210:
					num = 237;
					break;
				case 211:
					num = 238;
					break;
				case 212:
					num = 235;
					break;
				case 213:
					num = 239;
					break;
				case 214:
					num = 236;
					break;
				case 215:
					num = 191;
					break;
				case 216:
					num = 124;
					break;
				case 217:
					num = 253;
					break;
				case 218:
					num = 254;
					break;
				case 219:
					num = 251;
					break;
				case 220:
					num = 252;
					break;
				case 221:
					num = 173;
					break;
				case 222:
					num = 174;
					break;
				case 223:
					num = 89;
					break;
				case 224:
					num = 68;
					break;
				case 225:
					num = 69;
					break;
				case 226:
					num = 66;
					break;
				case 227:
					num = 70;
					break;
				case 228:
					num = 67;
					break;
				case 229:
					num = 208;
					break;
				case 230:
					num = 192;
					break;
				case 231:
					num = 72;
					break;
				case 232:
					num = 84;
					break;
				case 233:
					num = 81;
					break;
				case 234:
					num = 82;
					break;
				case 235:
					num = 83;
					break;
				case 236:
					num = 88;
					break;
				case 237:
					num = 85;
					break;
				case 238:
					num = 86;
					break;
				case 239:
					num = 87;
					break;
				case 240:
					num = 140;
					break;
				case 241:
					num = 73;
					break;
				case 242:
					num = 205;
					break;
				case 243:
					num = 206;
					break;
				case 244:
					num = 203;
					break;
				case 245:
					num = 207;
					break;
				case 246:
					num = 204;
					break;
				case 247:
					num = 225;
					break;
				case 248:
					num = 106;
					break;
				case 249:
					num = 221;
					break;
				case 250:
					num = 222;
					break;
				case 251:
					num = 219;
					break;
				case 252:
					num = 161;
					break;
				case 253:
					num = 141;
					break;
				case 254:
					num = 142;
					break;
				case 255:
					num = 223;
					break;
				case 272:
					num = 172;
					break;
				case 8254:
					num = 188;
					break;
				case 65281:
					num = 79;
					break;
				case 65282:
					num = 127;
					break;
				case 65283:
					num = 74;
					break;
				case 65284:
					num = 103;
					break;
				case 65285:
					num = 108;
					break;
				case 65286:
					num = 80;
					break;
				case 65287:
					num = 125;
					break;
				case 65288:
					num = 77;
					break;
				case 65289:
					num = 93;
					break;
				case 65290:
					num = 92;
					break;
				case 65291:
					num = 78;
					break;
				case 65292:
					num = 107;
					break;
				case 65293:
					num = 96;
					break;
				case 65294:
					num = 75;
					break;
				case 65295:
					num = 97;
					break;
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
					num -= 65056;
					break;
				case 65306:
					num = 122;
					break;
				case 65307:
					num = 94;
					break;
				case 65308:
					num = 76;
					break;
				case 65309:
					num = 126;
					break;
				case 65310:
					num = 110;
					break;
				case 65311:
					num = 111;
					break;
				case 65312:
					num = 128;
					break;
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
					num -= 65120;
					break;
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
					num -= 65113;
					break;
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
					num -= 65105;
					break;
				case 65339:
					num = 158;
					break;
				case 65340:
					num = 224;
					break;
				case 65341:
					num = 159;
					break;
				case 65342:
					num = 95;
					break;
				case 65343:
					num = 109;
					break;
				case 65344:
					num = 121;
					break;
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
				case 65353:
					num -= 65216;
					break;
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
				case 65361:
				case 65362:
					num -= 65209;
					break;
				case 65363:
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
					num -= 65201;
					break;
				case 65371:
					num = 156;
					break;
				case 65372:
					num = 187;
					break;
				case 65373:
					num = 71;
					break;
				case 65374:
					num = 220;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 24:
				case 25:
				case 28:
				case 29:
				case 30:
				case 31:
				case 182:
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
public class ENCibm277 : CP20277
{
}
[Serializable]
public class CP20278 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
		'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
		'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
		'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
		'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
		'\u0014', '\u0015', '\u009e', '\u001a', ' ', '\u00a0', 'â', '{', 'à', 'á',
		'ã', '}', 'ç', 'ñ', '§', '.', '<', '(', '+', '!',
		'&', '`', 'ê', 'ë', 'è', 'í', 'î', 'ï', 'ì', 'ß',
		'¤', 'Å', '*', ')', ';', '^', '-', '/', 'Â', '#',
		'À', 'Á', 'Ã', '$', 'Ç', 'Ñ', 'ö', ',', '%', '_',
		'>', '?', 'ø', '\\', 'Ê', 'Ë', 'È', 'Í', 'Î', 'Ï',
		'Ì', 'é', ':', 'Ä', 'Ö', '\'', '=', '"', 'Ø', 'a',
		'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', '«', '»',
		'ð', 'ý', 'þ', '±', '°', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', 'ª', 'º', 'æ', '\u00b8', 'Æ', ']',
		'µ', 'ü', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'¡', '¿', 'Ð', 'Ý', 'Þ', '®', '¢', '£', '¥', '·',
		'©', '[', '¶', '¼', '½', '¾', '¬', '|', '\u00af', '\u00a8',
		'\u00b4', '×', 'ä', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
		'H', 'I', '\u00ad', 'ô', '¦', 'ò', 'ó', 'õ', 'å', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', '¹', 'û',
		'~', 'ù', 'ú', 'ÿ', 'É', '÷', 'S', 'T', 'U', 'V',
		'W', 'X', 'Y', 'Z', '²', 'Ô', '@', 'Ò', 'Ó', 'Õ',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'³', 'Û', 'Ü', 'Ù', 'Ú', '\u009f'
	};

	public CP20278()
		: base(20278, ToChars, "IBM EBCDIC (Finland/Sweden)", "IBM278", "IBM278", "IBM278", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
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
			if (num >= 4)
			{
				switch (num)
				{
				case 4:
					num = 55;
					break;
				case 5:
					num = 45;
					break;
				case 6:
					num = 46;
					break;
				case 7:
					num = 47;
					break;
				case 8:
					num = 22;
					break;
				case 9:
					num = 5;
					break;
				case 10:
					num = 37;
					break;
				case 20:
					num = 60;
					break;
				case 21:
					num = 61;
					break;
				case 22:
					num = 50;
					break;
				case 23:
					num = 38;
					break;
				case 26:
					num = 63;
					break;
				case 27:
					num = 39;
					break;
				case 32:
					num = 64;
					break;
				case 33:
					num = 79;
					break;
				case 34:
					num = 127;
					break;
				case 35:
					num = 99;
					break;
				case 36:
					num = 103;
					break;
				case 37:
					num = 108;
					break;
				case 38:
					num = 80;
					break;
				case 39:
					num = 125;
					break;
				case 40:
					num = 77;
					break;
				case 41:
					num = 93;
					break;
				case 42:
					num = 92;
					break;
				case 43:
					num = 78;
					break;
				case 44:
					num = 107;
					break;
				case 45:
					num = 96;
					break;
				case 46:
					num = 75;
					break;
				case 47:
					num = 97;
					break;
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
					num += 192;
					break;
				case 58:
					num = 122;
					break;
				case 59:
					num = 94;
					break;
				case 60:
					num = 76;
					break;
				case 61:
					num = 126;
					break;
				case 62:
					num = 110;
					break;
				case 63:
					num = 111;
					break;
				case 64:
					num = 236;
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
					num += 128;
					break;
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
					num += 135;
					break;
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
					num += 143;
					break;
				case 91:
					num = 181;
					break;
				case 92:
					num = 113;
					break;
				case 93:
					num = 159;
					break;
				case 94:
					num = 95;
					break;
				case 95:
					num = 109;
					break;
				case 96:
					num = 81;
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
					num += 32;
					break;
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
					num += 39;
					break;
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
					num += 47;
					break;
				case 123:
					num = 67;
					break;
				case 124:
					num = 187;
					break;
				case 125:
					num = 71;
					break;
				case 126:
					num = 220;
					break;
				case 127:
					num = 7;
					break;
				case 128:
				case 129:
				case 130:
				case 131:
				case 132:
					num -= 96;
					break;
				case 133:
					num = 21;
					break;
				case 134:
					num = 6;
					break;
				case 135:
					num = 23;
					break;
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
					num -= 96;
					break;
				case 141:
					num = 9;
					break;
				case 142:
					num = 10;
					break;
				case 143:
					num = 27;
					break;
				case 144:
					num = 48;
					break;
				case 145:
					num = 49;
					break;
				case 146:
					num = 26;
					break;
				case 147:
				case 148:
				case 149:
				case 150:
					num -= 96;
					break;
				case 151:
					num = 8;
					break;
				case 152:
				case 153:
				case 154:
				case 155:
					num -= 96;
					break;
				case 156:
					num = 4;
					break;
				case 157:
					num = 20;
					break;
				case 158:
					num = 62;
					break;
				case 159:
					num = 255;
					break;
				case 160:
					num = 65;
					break;
				case 161:
					num = 170;
					break;
				case 162:
					num = 176;
					break;
				case 163:
					num = 177;
					break;
				case 164:
					num = 90;
					break;
				case 165:
					num = 178;
					break;
				case 166:
					num = 204;
					break;
				case 167:
					num = 74;
					break;
				case 168:
					num = 189;
					break;
				case 169:
					num = 180;
					break;
				case 170:
					num = 154;
					break;
				case 171:
					num = 138;
					break;
				case 172:
					num = 186;
					break;
				case 173:
					num = 202;
					break;
				case 174:
					num = 175;
					break;
				case 175:
					num = 188;
					break;
				case 176:
					num = 144;
					break;
				case 177:
					num = 143;
					break;
				case 178:
					num = 234;
					break;
				case 179:
					num = 250;
					break;
				case 180:
					num = 190;
					break;
				case 181:
					num = 160;
					break;
				case 183:
					num = 179;
					break;
				case 184:
					num = 157;
					break;
				case 185:
					num = 218;
					break;
				case 186:
					num = 155;
					break;
				case 187:
					num = 139;
					break;
				case 188:
					num = 183;
					break;
				case 189:
					num = 184;
					break;
				case 190:
					num = 185;
					break;
				case 191:
					num = 171;
					break;
				case 192:
					num = 100;
					break;
				case 193:
					num = 101;
					break;
				case 194:
					num = 98;
					break;
				case 195:
					num = 102;
					break;
				case 196:
					num = 123;
					break;
				case 197:
					num = 91;
					break;
				case 198:
					num = 158;
					break;
				case 199:
					num = 104;
					break;
				case 200:
					num = 116;
					break;
				case 201:
					num = 224;
					break;
				case 202:
					num = 114;
					break;
				case 203:
					num = 115;
					break;
				case 204:
					num = 120;
					break;
				case 205:
					num = 117;
					break;
				case 206:
					num = 118;
					break;
				case 207:
					num = 119;
					break;
				case 208:
					num = 172;
					break;
				case 209:
					num = 105;
					break;
				case 210:
					num = 237;
					break;
				case 211:
					num = 238;
					break;
				case 212:
					num = 235;
					break;
				case 213:
					num = 239;
					break;
				case 214:
					num = 124;
					break;
				case 215:
					num = 191;
					break;
				case 216:
					num = 128;
					break;
				case 217:
					num = 253;
					break;
				case 218:
					num = 254;
					break;
				case 219:
					num = 251;
					break;
				case 220:
					num = 252;
					break;
				case 221:
					num = 173;
					break;
				case 222:
					num = 174;
					break;
				case 223:
					num = 89;
					break;
				case 224:
					num = 68;
					break;
				case 225:
					num = 69;
					break;
				case 226:
					num = 66;
					break;
				case 227:
					num = 70;
					break;
				case 228:
					num = 192;
					break;
				case 229:
					num = 208;
					break;
				case 230:
					num = 156;
					break;
				case 231:
					num = 72;
					break;
				case 232:
					num = 84;
					break;
				case 233:
					num = 121;
					break;
				case 234:
					num = 82;
					break;
				case 235:
					num = 83;
					break;
				case 236:
					num = 88;
					break;
				case 237:
					num = 85;
					break;
				case 238:
					num = 86;
					break;
				case 239:
					num = 87;
					break;
				case 240:
					num = 140;
					break;
				case 241:
					num = 73;
					break;
				case 242:
					num = 205;
					break;
				case 243:
					num = 206;
					break;
				case 244:
					num = 203;
					break;
				case 245:
					num = 207;
					break;
				case 246:
					num = 106;
					break;
				case 247:
					num = 225;
					break;
				case 248:
					num = 112;
					break;
				case 249:
					num = 221;
					break;
				case 250:
					num = 222;
					break;
				case 251:
					num = 219;
					break;
				case 252:
					num = 161;
					break;
				case 253:
					num = 141;
					break;
				case 254:
					num = 142;
					break;
				case 255:
					num = 223;
					break;
				case 272:
					num = 172;
					break;
				case 8254:
					num = 188;
					break;
				case 65281:
					num = 79;
					break;
				case 65282:
					num = 127;
					break;
				case 65283:
					num = 99;
					break;
				case 65284:
					num = 103;
					break;
				case 65285:
					num = 108;
					break;
				case 65286:
					num = 80;
					break;
				case 65287:
					num = 125;
					break;
				case 65288:
					num = 77;
					break;
				case 65289:
					num = 93;
					break;
				case 65290:
					num = 92;
					break;
				case 65291:
					num = 78;
					break;
				case 65292:
					num = 107;
					break;
				case 65293:
					num = 96;
					break;
				case 65294:
					num = 75;
					break;
				case 65295:
					num = 97;
					break;
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
					num -= 65056;
					break;
				case 65306:
					num = 122;
					break;
				case 65307:
					num = 94;
					break;
				case 65308:
					num = 76;
					break;
				case 65309:
					num = 126;
					break;
				case 65310:
					num = 110;
					break;
				case 65311:
					num = 111;
					break;
				case 65312:
					num = 236;
					break;
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
					num -= 65120;
					break;
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
					num -= 65113;
					break;
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
					num -= 65105;
					break;
				case 65339:
					num = 181;
					break;
				case 65340:
					num = 113;
					break;
				case 65341:
					num = 159;
					break;
				case 65342:
					num = 95;
					break;
				case 65343:
					num = 109;
					break;
				case 65344:
					num = 81;
					break;
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
				case 65353:
					num -= 65216;
					break;
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
				case 65361:
				case 65362:
					num -= 65209;
					break;
				case 65363:
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
					num -= 65201;
					break;
				case 65371:
					num = 67;
					break;
				case 65372:
					num = 187;
					break;
				case 65373:
					num = 71;
					break;
				case 65374:
					num = 220;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 24:
				case 25:
				case 28:
				case 29:
				case 30:
				case 31:
				case 182:
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
public class ENCibm278 : CP20278
{
}
[Serializable]
public class CP20280 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
		'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
		'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
		'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
		'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
		'\u0014', '\u0015', '\u009e', '\u001a', ' ', '\u00a0', 'â', 'ä', '{', 'á',
		'ã', 'å', '\\', 'ñ', '°', '.', '<', '(', '+', '!',
		'&', ']', 'ê', 'ë', '}', 'í', 'î', 'ï', '~', 'ß',
		'é', '$', '*', ')', ';', '^', '-', '/', 'Â', 'Ä',
		'À', 'Á', 'Ã', 'Å', 'Ç', 'Ñ', 'ò', ',', '%', '_',
		'>', '?', 'ø', 'É', 'Ê', 'Ë', 'È', 'Í', 'Î', 'Ï',
		'Ì', 'ù', ':', '£', '§', '\'', '=', '"', 'Ø', 'a',
		'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', '«', '»',
		'ð', 'ý', 'þ', '±', '[', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', 'ª', 'º', 'æ', '\u00b8', 'Æ', '¤',
		'µ', 'ì', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'¡', '¿', 'Ð', 'Ý', 'Þ', '®', '¢', '#', '¥', '·',
		'©', '@', '¶', '¼', '½', '¾', '¬', '|', '\u00af', '\u00a8',
		'\u00b4', '×', 'à', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
		'H', 'I', '\u00ad', 'ô', 'ö', '¦', 'ó', 'õ', 'è', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', '¹', 'û',
		'ü', '`', 'ú', 'ÿ', 'ç', '÷', 'S', 'T', 'U', 'V',
		'W', 'X', 'Y', 'Z', '²', 'Ô', 'Ö', 'Ò', 'Ó', 'Õ',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'³', 'Û', 'Ü', 'Ù', 'Ú', '\u009f'
	};

	public CP20280()
		: base(20280, ToChars, "IBM EBCDIC (Italy)", "IBM280", "IBM280", "IBM280", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
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
			if (num >= 4)
			{
				switch (num)
				{
				case 4:
					num = 55;
					break;
				case 5:
					num = 45;
					break;
				case 6:
					num = 46;
					break;
				case 7:
					num = 47;
					break;
				case 8:
					num = 22;
					break;
				case 9:
					num = 5;
					break;
				case 10:
					num = 37;
					break;
				case 20:
					num = 60;
					break;
				case 21:
					num = 61;
					break;
				case 22:
					num = 50;
					break;
				case 23:
					num = 38;
					break;
				case 26:
					num = 63;
					break;
				case 27:
					num = 39;
					break;
				case 32:
					num = 64;
					break;
				case 33:
					num = 79;
					break;
				case 34:
					num = 127;
					break;
				case 35:
					num = 177;
					break;
				case 36:
					num = 91;
					break;
				case 37:
					num = 108;
					break;
				case 38:
					num = 80;
					break;
				case 39:
					num = 125;
					break;
				case 40:
					num = 77;
					break;
				case 41:
					num = 93;
					break;
				case 42:
					num = 92;
					break;
				case 43:
					num = 78;
					break;
				case 44:
					num = 107;
					break;
				case 45:
					num = 96;
					break;
				case 46:
					num = 75;
					break;
				case 47:
					num = 97;
					break;
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
					num += 192;
					break;
				case 58:
					num = 122;
					break;
				case 59:
					num = 94;
					break;
				case 60:
					num = 76;
					break;
				case 61:
					num = 126;
					break;
				case 62:
					num = 110;
					break;
				case 63:
					num = 111;
					break;
				case 64:
					num = 181;
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
					num += 128;
					break;
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
					num += 135;
					break;
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
					num += 143;
					break;
				case 91:
					num = 144;
					break;
				case 92:
					num = 72;
					break;
				case 93:
					num = 81;
					break;
				case 94:
					num = 95;
					break;
				case 95:
					num = 109;
					break;
				case 96:
					num = 221;
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
					num += 32;
					break;
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
					num += 39;
					break;
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
					num += 47;
					break;
				case 123:
					num = 68;
					break;
				case 124:
					num = 187;
					break;
				case 125:
					num = 84;
					break;
				case 126:
					num = 88;
					break;
				case 127:
					num = 7;
					break;
				case 128:
				case 129:
				case 130:
				case 131:
				case 132:
					num -= 96;
					break;
				case 133:
					num = 21;
					break;
				case 134:
					num = 6;
					break;
				case 135:
					num = 23;
					break;
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
					num -= 96;
					break;
				case 141:
					num = 9;
					break;
				case 142:
					num = 10;
					break;
				case 143:
					num = 27;
					break;
				case 144:
					num = 48;
					break;
				case 145:
					num = 49;
					break;
				case 146:
					num = 26;
					break;
				case 147:
				case 148:
				case 149:
				case 150:
					num -= 96;
					break;
				case 151:
					num = 8;
					break;
				case 152:
				case 153:
				case 154:
				case 155:
					num -= 96;
					break;
				case 156:
					num = 4;
					break;
				case 157:
					num = 20;
					break;
				case 158:
					num = 62;
					break;
				case 159:
					num = 255;
					break;
				case 160:
					num = 65;
					break;
				case 161:
					num = 170;
					break;
				case 162:
					num = 176;
					break;
				case 163:
					num = 123;
					break;
				case 164:
					num = 159;
					break;
				case 165:
					num = 178;
					break;
				case 166:
					num = 205;
					break;
				case 167:
					num = 124;
					break;
				case 168:
					num = 189;
					break;
				case 169:
					num = 180;
					break;
				case 170:
					num = 154;
					break;
				case 171:
					num = 138;
					break;
				case 172:
					num = 186;
					break;
				case 173:
					num = 202;
					break;
				case 174:
					num = 175;
					break;
				case 175:
					num = 188;
					break;
				case 176:
					num = 74;
					break;
				case 177:
					num = 143;
					break;
				case 178:
					num = 234;
					break;
				case 179:
					num = 250;
					break;
				case 180:
					num = 190;
					break;
				case 181:
					num = 160;
					break;
				case 183:
					num = 179;
					break;
				case 184:
					num = 157;
					break;
				case 185:
					num = 218;
					break;
				case 186:
					num = 155;
					break;
				case 187:
					num = 139;
					break;
				case 188:
					num = 183;
					break;
				case 189:
					num = 184;
					break;
				case 190:
					num = 185;
					break;
				case 191:
					num = 171;
					break;
				case 192:
					num = 100;
					break;
				case 193:
					num = 101;
					break;
				case 194:
					num = 98;
					break;
				case 195:
					num = 102;
					break;
				case 196:
					num = 99;
					break;
				case 197:
					num = 103;
					break;
				case 198:
					num = 158;
					break;
				case 199:
					num = 104;
					break;
				case 200:
					num = 116;
					break;
				case 201:
					num = 113;
					break;
				case 202:
					num = 114;
					break;
				case 203:
					num = 115;
					break;
				case 204:
					num = 120;
					break;
				case 205:
					num = 117;
					break;
				case 206:
					num = 118;
					break;
				case 207:
					num = 119;
					break;
				case 208:
					num = 172;
					break;
				case 209:
					num = 105;
					break;
				case 210:
					num = 237;
					break;
				case 211:
					num = 238;
					break;
				case 212:
					num = 235;
					break;
				case 213:
					num = 239;
					break;
				case 214:
					num = 236;
					break;
				case 215:
					num = 191;
					break;
				case 216:
					num = 128;
					break;
				case 217:
					num = 253;
					break;
				case 218:
					num = 254;
					break;
				case 219:
					num = 251;
					break;
				case 220:
					num = 252;
					break;
				case 221:
					num = 173;
					break;
				case 222:
					num = 174;
					break;
				case 223:
					num = 89;
					break;
				case 224:
					num = 192;
					break;
				case 225:
					num = 69;
					break;
				case 226:
					num = 66;
					break;
				case 227:
					num = 70;
					break;
				case 228:
					num = 67;
					break;
				case 229:
					num = 71;
					break;
				case 230:
					num = 156;
					break;
				case 231:
					num = 224;
					break;
				case 232:
					num = 208;
					break;
				case 233:
					num = 90;
					break;
				case 234:
					num = 82;
					break;
				case 235:
					num = 83;
					break;
				case 236:
					num = 161;
					break;
				case 237:
					num = 85;
					break;
				case 238:
					num = 86;
					break;
				case 239:
					num = 87;
					break;
				case 240:
					num = 140;
					break;
				case 241:
					num = 73;
					break;
				case 242:
					num = 106;
					break;
				case 243:
					num = 206;
					break;
				case 244:
					num = 203;
					break;
				case 245:
					num = 207;
					break;
				case 246:
					num = 204;
					break;
				case 247:
					num = 225;
					break;
				case 248:
					num = 112;
					break;
				case 249:
					num = 121;
					break;
				case 250:
					num = 222;
					break;
				case 251:
					num = 219;
					break;
				case 252:
					num = 220;
					break;
				case 253:
					num = 141;
					break;
				case 254:
					num = 142;
					break;
				case 255:
					num = 223;
					break;
				case 272:
					num = 172;
					break;
				case 8254:
					num = 188;
					break;
				case 65281:
					num = 79;
					break;
				case 65282:
					num = 127;
					break;
				case 65283:
					num = 177;
					break;
				case 65284:
					num = 91;
					break;
				case 65285:
					num = 108;
					break;
				case 65286:
					num = 80;
					break;
				case 65287:
					num = 125;
					break;
				case 65288:
					num = 77;
					break;
				case 65289:
					num = 93;
					break;
				case 65290:
					num = 92;
					break;
				case 65291:
					num = 78;
					break;
				case 65292:
					num = 107;
					break;
				case 65293:
					num = 96;
					break;
				case 65294:
					num = 75;
					break;
				case 65295:
					num = 97;
					break;
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
					num -= 65056;
					break;
				case 65306:
					num = 122;
					break;
				case 65307:
					num = 94;
					break;
				case 65308:
					num = 76;
					break;
				case 65309:
					num = 126;
					break;
				case 65310:
					num = 110;
					break;
				case 65311:
					num = 111;
					break;
				case 65312:
					num = 181;
					break;
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
					num -= 65120;
					break;
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
					num -= 65113;
					break;
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
					num -= 65105;
					break;
				case 65339:
					num = 144;
					break;
				case 65340:
					num = 72;
					break;
				case 65341:
					num = 81;
					break;
				case 65342:
					num = 95;
					break;
				case 65343:
					num = 109;
					break;
				case 65344:
					num = 221;
					break;
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
				case 65353:
					num -= 65216;
					break;
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
				case 65361:
				case 65362:
					num -= 65209;
					break;
				case 65363:
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
					num -= 65201;
					break;
				case 65371:
					num = 68;
					break;
				case 65372:
					num = 187;
					break;
				case 65373:
					num = 84;
					break;
				case 65374:
					num = 88;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 24:
				case 25:
				case 28:
				case 29:
				case 30:
				case 31:
				case 182:
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
public class ENCibm280 : CP20280
{
}
[Serializable]
public class CP20284 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
		'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
		'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
		'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
		'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
		'\u0014', '\u0015', '\u009e', '\u001a', ' ', '\u00a0', 'â', 'ä', 'à', 'á',
		'ã', 'å', 'ç', '¦', '[', '.', '<', '(', '+', '|',
		'&', 'é', 'ê', 'ë', 'è', 'í', 'î', 'ï', 'ì', 'ß',
		']', '$', '*', ')', ';', '¬', '-', '/', 'Â', 'Ä',
		'À', 'Á', 'Ã', 'Å', 'Ç', '#', 'ñ', ',', '%', '_',
		'>', '?', 'ø', 'É', 'Ê', 'Ë', 'È', 'Í', 'Î', 'Ï',
		'Ì', '`', ':', 'Ñ', '@', '\'', '=', '"', 'Ø', 'a',
		'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', '«', '»',
		'ð', 'ý', 'þ', '±', '°', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', 'ª', 'º', 'æ', '\u00b8', 'Æ', '¤',
		'µ', '\u00a8', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'¡', '¿', 'Ð', 'Ý', 'Þ', '®', '¢', '£', '¥', '·',
		'©', '§', '¶', '¼', '½', '¾', '^', '!', '\u00af', '~',
		'\u00b4', '×', '{', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
		'H', 'I', '\u00ad', 'ô', 'ö', 'ò', 'ó', 'õ', '}', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', '¹', 'û',
		'ü', 'ù', 'ú', 'ÿ', '\\', '÷', 'S', 'T', 'U', 'V',
		'W', 'X', 'Y', 'Z', '²', 'Ô', 'Ö', 'Ò', 'Ó', 'Õ',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'³', 'Û', 'Ü', 'Ù', 'Ú', '\u009f'
	};

	public CP20284()
		: base(20284, ToChars, "IBM EBCDIC (Latin America/Spain)", "IBM284", "IBM284", "IBM284", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
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
			if (num >= 4)
			{
				switch (num)
				{
				case 4:
					num = 55;
					break;
				case 5:
					num = 45;
					break;
				case 6:
					num = 46;
					break;
				case 7:
					num = 47;
					break;
				case 8:
					num = 22;
					break;
				case 9:
					num = 5;
					break;
				case 10:
					num = 37;
					break;
				case 20:
					num = 60;
					break;
				case 21:
					num = 61;
					break;
				case 22:
					num = 50;
					break;
				case 23:
					num = 38;
					break;
				case 26:
					num = 63;
					break;
				case 27:
					num = 39;
					break;
				case 32:
					num = 64;
					break;
				case 33:
					num = 187;
					break;
				case 34:
					num = 127;
					break;
				case 35:
					num = 105;
					break;
				case 36:
					num = 91;
					break;
				case 37:
					num = 108;
					break;
				case 38:
					num = 80;
					break;
				case 39:
					num = 125;
					break;
				case 40:
					num = 77;
					break;
				case 41:
					num = 93;
					break;
				case 42:
					num = 92;
					break;
				case 43:
					num = 78;
					break;
				case 44:
					num = 107;
					break;
				case 45:
					num = 96;
					break;
				case 46:
					num = 75;
					break;
				case 47:
					num = 97;
					break;
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
					num += 192;
					break;
				case 58:
					num = 122;
					break;
				case 59:
					num = 94;
					break;
				case 60:
					num = 76;
					break;
				case 61:
					num = 126;
					break;
				case 62:
					num = 110;
					break;
				case 63:
					num = 111;
					break;
				case 64:
					num = 124;
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
					num += 128;
					break;
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
					num += 135;
					break;
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
					num += 143;
					break;
				case 91:
					num = 74;
					break;
				case 92:
					num = 224;
					break;
				case 93:
					num = 90;
					break;
				case 94:
					num = 186;
					break;
				case 95:
					num = 109;
					break;
				case 96:
					num = 121;
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
					num += 32;
					break;
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
					num += 39;
					break;
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
					num += 47;
					break;
				case 123:
					num = 192;
					break;
				case 124:
					num = 79;
					break;
				case 125:
					num = 208;
					break;
				case 126:
					num = 189;
					break;
				case 127:
					num = 7;
					break;
				case 128:
				case 129:
				case 130:
				case 131:
				case 132:
					num -= 96;
					break;
				case 133:
					num = 21;
					break;
				case 134:
					num = 6;
					break;
				case 135:
					num = 23;
					break;
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
					num -= 96;
					break;
				case 141:
					num = 9;
					break;
				case 142:
					num = 10;
					break;
				case 143:
					num = 27;
					break;
				case 144:
					num = 48;
					break;
				case 145:
					num = 49;
					break;
				case 146:
					num = 26;
					break;
				case 147:
				case 148:
				case 149:
				case 150:
					num -= 96;
					break;
				case 151:
					num = 8;
					break;
				case 152:
				case 153:
				case 154:
				case 155:
					num -= 96;
					break;
				case 156:
					num = 4;
					break;
				case 157:
					num = 20;
					break;
				case 158:
					num = 62;
					break;
				case 159:
					num = 255;
					break;
				case 160:
					num = 65;
					break;
				case 161:
					num = 170;
					break;
				case 162:
					num = 176;
					break;
				case 163:
					num = 177;
					break;
				case 164:
					num = 159;
					break;
				case 165:
					num = 178;
					break;
				case 166:
					num = 73;
					break;
				case 167:
					num = 181;
					break;
				case 168:
					num = 161;
					break;
				case 169:
					num = 180;
					break;
				case 170:
					num = 154;
					break;
				case 171:
					num = 138;
					break;
				case 172:
					num = 95;
					break;
				case 173:
					num = 202;
					break;
				case 174:
					num = 175;
					break;
				case 175:
					num = 188;
					break;
				case 176:
					num = 144;
					break;
				case 177:
					num = 143;
					break;
				case 178:
					num = 234;
					break;
				case 179:
					num = 250;
					break;
				case 180:
					num = 190;
					break;
				case 181:
					num = 160;
					break;
				case 183:
					num = 179;
					break;
				case 184:
					num = 157;
					break;
				case 185:
					num = 218;
					break;
				case 186:
					num = 155;
					break;
				case 187:
					num = 139;
					break;
				case 188:
					num = 183;
					break;
				case 189:
					num = 184;
					break;
				case 190:
					num = 185;
					break;
				case 191:
					num = 171;
					break;
				case 192:
					num = 100;
					break;
				case 193:
					num = 101;
					break;
				case 194:
					num = 98;
					break;
				case 195:
					num = 102;
					break;
				case 196:
					num = 99;
					break;
				case 197:
					num = 103;
					break;
				case 198:
					num = 158;
					break;
				case 199:
					num = 104;
					break;
				case 200:
					num = 116;
					break;
				case 201:
					num = 113;
					break;
				case 202:
					num = 114;
					break;
				case 203:
					num = 115;
					break;
				case 204:
					num = 120;
					break;
				case 205:
					num = 117;
					break;
				case 206:
					num = 118;
					break;
				case 207:
					num = 119;
					break;
				case 208:
					num = 172;
					break;
				case 209:
					num = 123;
					break;
				case 210:
					num = 237;
					break;
				case 211:
					num = 238;
					break;
				case 212:
					num = 235;
					break;
				case 213:
					num = 239;
					break;
				case 214:
					num = 236;
					break;
				case 215:
					num = 191;
					break;
				case 216:
					num = 128;
					break;
				case 217:
					num = 253;
					break;
				case 218:
					num = 254;
					break;
				case 219:
					num = 251;
					break;
				case 220:
					num = 252;
					break;
				case 221:
					num = 173;
					break;
				case 222:
					num = 174;
					break;
				case 223:
					num = 89;
					break;
				case 224:
					num = 68;
					break;
				case 225:
					num = 69;
					break;
				case 226:
					num = 66;
					break;
				case 227:
					num = 70;
					break;
				case 228:
					num = 67;
					break;
				case 229:
					num = 71;
					break;
				case 230:
					num = 156;
					break;
				case 231:
					num = 72;
					break;
				case 232:
					num = 84;
					break;
				case 233:
					num = 81;
					break;
				case 234:
					num = 82;
					break;
				case 235:
					num = 83;
					break;
				case 236:
					num = 88;
					break;
				case 237:
					num = 85;
					break;
				case 238:
					num = 86;
					break;
				case 239:
					num = 87;
					break;
				case 240:
					num = 140;
					break;
				case 241:
					num = 106;
					break;
				case 242:
					num = 205;
					break;
				case 243:
					num = 206;
					break;
				case 244:
					num = 203;
					break;
				case 245:
					num = 207;
					break;
				case 246:
					num = 204;
					break;
				case 247:
					num = 225;
					break;
				case 248:
					num = 112;
					break;
				case 249:
					num = 221;
					break;
				case 250:
					num = 222;
					break;
				case 251:
					num = 219;
					break;
				case 252:
					num = 220;
					break;
				case 253:
					num = 141;
					break;
				case 254:
					num = 142;
					break;
				case 255:
					num = 223;
					break;
				case 272:
					num = 172;
					break;
				case 8254:
					num = 188;
					break;
				case 65281:
					num = 187;
					break;
				case 65282:
					num = 127;
					break;
				case 65283:
					num = 105;
					break;
				case 65284:
					num = 91;
					break;
				case 65285:
					num = 108;
					break;
				case 65286:
					num = 80;
					break;
				case 65287:
					num = 125;
					break;
				case 65288:
					num = 77;
					break;
				case 65289:
					num = 93;
					break;
				case 65290:
					num = 92;
					break;
				case 65291:
					num = 78;
					break;
				case 65292:
					num = 107;
					break;
				case 65293:
					num = 96;
					break;
				case 65294:
					num = 75;
					break;
				case 65295:
					num = 97;
					break;
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
					num -= 65056;
					break;
				case 65306:
					num = 122;
					break;
				case 65307:
					num = 94;
					break;
				case 65308:
					num = 76;
					break;
				case 65309:
					num = 126;
					break;
				case 65310:
					num = 110;
					break;
				case 65311:
					num = 111;
					break;
				case 65312:
					num = 124;
					break;
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
					num -= 65120;
					break;
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
					num -= 65113;
					break;
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
					num -= 65105;
					break;
				case 65339:
					num = 74;
					break;
				case 65340:
					num = 224;
					break;
				case 65341:
					num = 90;
					break;
				case 65342:
					num = 186;
					break;
				case 65343:
					num = 109;
					break;
				case 65344:
					num = 121;
					break;
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
				case 65353:
					num -= 65216;
					break;
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
				case 65361:
				case 65362:
					num -= 65209;
					break;
				case 65363:
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
					num -= 65201;
					break;
				case 65371:
					num = 192;
					break;
				case 65372:
					num = 79;
					break;
				case 65373:
					num = 208;
					break;
				case 65374:
					num = 189;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 24:
				case 25:
				case 28:
				case 29:
				case 30:
				case 31:
				case 182:
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
public class ENCibm284 : CP20284
{
}
[Serializable]
public class CP20285 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
		'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
		'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
		'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
		'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
		'\u0014', '\u0015', '\u009e', '\u001a', ' ', '\u00a0', 'â', 'ä', 'à', 'á',
		'ã', 'å', 'ç', 'ñ', '$', '.', '<', '(', '+', '|',
		'&', 'é', 'ê', 'ë', 'è', 'í', 'î', 'ï', 'ì', 'ß',
		'!', '£', '*', ')', ';', '¬', '-', '/', 'Â', 'Ä',
		'À', 'Á', 'Ã', 'Å', 'Ç', 'Ñ', '¦', ',', '%', '_',
		'>', '?', 'ø', 'É', 'Ê', 'Ë', 'È', 'Í', 'Î', 'Ï',
		'Ì', '`', ':', '#', '@', '\'', '=', '"', 'Ø', 'a',
		'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', '«', '»',
		'ð', 'ý', 'þ', '±', '°', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', 'ª', 'º', 'æ', '\u00b8', 'Æ', '¤',
		'µ', '\u00af', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'¡', '¿', 'Ð', 'Ý', 'Þ', '®', '¢', '[', '¥', '·',
		'©', '§', '¶', '¼', '½', '¾', '^', ']', '~', '\u00a8',
		'\u00b4', '×', '{', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
		'H', 'I', '\u00ad', 'ô', 'ö', 'ò', 'ó', 'õ', '}', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', '¹', 'û',
		'ü', 'ù', 'ú', 'ÿ', '\\', '÷', 'S', 'T', 'U', 'V',
		'W', 'X', 'Y', 'Z', '²', 'Ô', 'Ö', 'Ò', 'Ó', 'Õ',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'³', 'Û', 'Ü', 'Ù', 'Ú', '\u009f'
	};

	public CP20285()
		: base(20285, ToChars, "IBM EBCDIC (United Kingdom)", "IBM285", "IBM285", "IBM285", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
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
			if (num >= 4)
			{
				switch (num)
				{
				case 4:
					num = 55;
					break;
				case 5:
					num = 45;
					break;
				case 6:
					num = 46;
					break;
				case 7:
					num = 47;
					break;
				case 8:
					num = 22;
					break;
				case 9:
					num = 5;
					break;
				case 10:
					num = 37;
					break;
				case 20:
					num = 60;
					break;
				case 21:
					num = 61;
					break;
				case 22:
					num = 50;
					break;
				case 23:
					num = 38;
					break;
				case 26:
					num = 63;
					break;
				case 27:
					num = 39;
					break;
				case 32:
					num = 64;
					break;
				case 33:
					num = 90;
					break;
				case 34:
					num = 127;
					break;
				case 35:
					num = 123;
					break;
				case 36:
					num = 74;
					break;
				case 37:
					num = 108;
					break;
				case 38:
					num = 80;
					break;
				case 39:
					num = 125;
					break;
				case 40:
					num = 77;
					break;
				case 41:
					num = 93;
					break;
				case 42:
					num = 92;
					break;
				case 43:
					num = 78;
					break;
				case 44:
					num = 107;
					break;
				case 45:
					num = 96;
					break;
				case 46:
					num = 75;
					break;
				case 47:
					num = 97;
					break;
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
					num += 192;
					break;
				case 58:
					num = 122;
					break;
				case 59:
					num = 94;
					break;
				case 60:
					num = 76;
					break;
				case 61:
					num = 126;
					break;
				case 62:
					num = 110;
					break;
				case 63:
					num = 111;
					break;
				case 64:
					num = 124;
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
					num += 128;
					break;
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
					num += 135;
					break;
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
					num += 143;
					break;
				case 91:
					num = 177;
					break;
				case 92:
					num = 224;
					break;
				case 93:
					num = 187;
					break;
				case 94:
					num = 186;
					break;
				case 95:
					num = 109;
					break;
				case 96:
					num = 121;
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
					num += 32;
					break;
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
					num += 39;
					break;
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
					num += 47;
					break;
				case 123:
					num = 192;
					break;
				case 124:
					num = 79;
					break;
				case 125:
					num = 208;
					break;
				case 126:
					num = 188;
					break;
				case 127:
					num = 7;
					break;
				case 128:
				case 129:
				case 130:
				case 131:
				case 132:
					num -= 96;
					break;
				case 133:
					num = 21;
					break;
				case 134:
					num = 6;
					break;
				case 135:
					num = 23;
					break;
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
					num -= 96;
					break;
				case 141:
					num = 9;
					break;
				case 142:
					num = 10;
					break;
				case 143:
					num = 27;
					break;
				case 144:
					num = 48;
					break;
				case 145:
					num = 49;
					break;
				case 146:
					num = 26;
					break;
				case 147:
				case 148:
				case 149:
				case 150:
					num -= 96;
					break;
				case 151:
					num = 8;
					break;
				case 152:
				case 153:
				case 154:
				case 155:
					num -= 96;
					break;
				case 156:
					num = 4;
					break;
				case 157:
					num = 20;
					break;
				case 158:
					num = 62;
					break;
				case 159:
					num = 255;
					break;
				case 160:
					num = 65;
					break;
				case 161:
					num = 170;
					break;
				case 162:
					num = 176;
					break;
				case 163:
					num = 91;
					break;
				case 164:
					num = 159;
					break;
				case 165:
					num = 178;
					break;
				case 166:
					num = 106;
					break;
				case 167:
					num = 181;
					break;
				case 168:
					num = 189;
					break;
				case 169:
					num = 180;
					break;
				case 170:
					num = 154;
					break;
				case 171:
					num = 138;
					break;
				case 172:
					num = 95;
					break;
				case 173:
					num = 202;
					break;
				case 174:
					num = 175;
					break;
				case 175:
					num = 161;
					break;
				case 176:
					num = 144;
					break;
				case 177:
					num = 143;
					break;
				case 178:
					num = 234;
					break;
				case 179:
					num = 250;
					break;
				case 180:
					num = 190;
					break;
				case 181:
					num = 160;
					break;
				case 183:
					num = 179;
					break;
				case 184:
					num = 157;
					break;
				case 185:
					num = 218;
					break;
				case 186:
					num = 155;
					break;
				case 187:
					num = 139;
					break;
				case 188:
					num = 183;
					break;
				case 189:
					num = 184;
					break;
				case 190:
					num = 185;
					break;
				case 191:
					num = 171;
					break;
				case 192:
					num = 100;
					break;
				case 193:
					num = 101;
					break;
				case 194:
					num = 98;
					break;
				case 195:
					num = 102;
					break;
				case 196:
					num = 99;
					break;
				case 197:
					num = 103;
					break;
				case 198:
					num = 158;
					break;
				case 199:
					num = 104;
					break;
				case 200:
					num = 116;
					break;
				case 201:
					num = 113;
					break;
				case 202:
					num = 114;
					break;
				case 203:
					num = 115;
					break;
				case 204:
					num = 120;
					break;
				case 205:
					num = 117;
					break;
				case 206:
					num = 118;
					break;
				case 207:
					num = 119;
					break;
				case 208:
					num = 172;
					break;
				case 209:
					num = 105;
					break;
				case 210:
					num = 237;
					break;
				case 211:
					num = 238;
					break;
				case 212:
					num = 235;
					break;
				case 213:
					num = 239;
					break;
				case 214:
					num = 236;
					break;
				case 215:
					num = 191;
					break;
				case 216:
					num = 128;
					break;
				case 217:
					num = 253;
					break;
				case 218:
					num = 254;
					break;
				case 219:
					num = 251;
					break;
				case 220:
					num = 252;
					break;
				case 221:
					num = 173;
					break;
				case 222:
					num = 174;
					break;
				case 223:
					num = 89;
					break;
				case 224:
					num = 68;
					break;
				case 225:
					num = 69;
					break;
				case 226:
					num = 66;
					break;
				case 227:
					num = 70;
					break;
				case 228:
					num = 67;
					break;
				case 229:
					num = 71;
					break;
				case 230:
					num = 156;
					break;
				case 231:
					num = 72;
					break;
				case 232:
					num = 84;
					break;
				case 233:
					num = 81;
					break;
				case 234:
					num = 82;
					break;
				case 235:
					num = 83;
					break;
				case 236:
					num = 88;
					break;
				case 237:
					num = 85;
					break;
				case 238:
					num = 86;
					break;
				case 239:
					num = 87;
					break;
				case 240:
					num = 140;
					break;
				case 241:
					num = 73;
					break;
				case 242:
					num = 205;
					break;
				case 243:
					num = 206;
					break;
				case 244:
					num = 203;
					break;
				case 245:
					num = 207;
					break;
				case 246:
					num = 204;
					break;
				case 247:
					num = 225;
					break;
				case 248:
					num = 112;
					break;
				case 249:
					num = 221;
					break;
				case 250:
					num = 222;
					break;
				case 251:
					num = 219;
					break;
				case 252:
					num = 220;
					break;
				case 253:
					num = 141;
					break;
				case 254:
					num = 142;
					break;
				case 255:
					num = 223;
					break;
				case 272:
					num = 172;
					break;
				case 8254:
					num = 161;
					break;
				case 65281:
					num = 90;
					break;
				case 65282:
					num = 127;
					break;
				case 65283:
					num = 123;
					break;
				case 65284:
					num = 74;
					break;
				case 65285:
					num = 108;
					break;
				case 65286:
					num = 80;
					break;
				case 65287:
					num = 125;
					break;
				case 65288:
					num = 77;
					break;
				case 65289:
					num = 93;
					break;
				case 65290:
					num = 92;
					break;
				case 65291:
					num = 78;
					break;
				case 65292:
					num = 107;
					break;
				case 65293:
					num = 96;
					break;
				case 65294:
					num = 75;
					break;
				case 65295:
					num = 97;
					break;
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
					num -= 65056;
					break;
				case 65306:
					num = 122;
					break;
				case 65307:
					num = 94;
					break;
				case 65308:
					num = 76;
					break;
				case 65309:
					num = 126;
					break;
				case 65310:
					num = 110;
					break;
				case 65311:
					num = 111;
					break;
				case 65312:
					num = 124;
					break;
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
					num -= 65120;
					break;
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
					num -= 65113;
					break;
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
					num -= 65105;
					break;
				case 65339:
					num = 177;
					break;
				case 65340:
					num = 224;
					break;
				case 65341:
					num = 187;
					break;
				case 65342:
					num = 186;
					break;
				case 65343:
					num = 109;
					break;
				case 65344:
					num = 121;
					break;
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
				case 65353:
					num -= 65216;
					break;
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
				case 65361:
				case 65362:
					num -= 65209;
					break;
				case 65363:
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
					num -= 65201;
					break;
				case 65371:
					num = 192;
					break;
				case 65372:
					num = 79;
					break;
				case 65373:
					num = 208;
					break;
				case 65374:
					num = 188;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 24:
				case 25:
				case 28:
				case 29:
				case 30:
				case 31:
				case 182:
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
public class ENCibm285 : CP20285
{
}
[Serializable]
public class CP20290 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
		'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
		'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
		'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
		'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
		'\u0014', '\u0015', '\u009e', '\u001a', ' ', '｡', '｢', '｣', '､', '･',
		'ｦ', 'ｧ', 'ｨ', 'ｩ', '£', '.', '<', '(', '+', '|',
		'&', 'ｪ', 'ｫ', 'ｬ', 'ｭ', 'ｮ', 'ｯ', '?', 'ｰ', '?',
		'!', '¥', '*', ')', ';', '¬', '-', '/', 'a', 'b',
		'c', 'd', 'e', 'f', 'g', 'h', '?', ',', '%', '_',
		'>', '?', '[', 'i', 'j', 'k', 'l', 'm', 'n', 'o',
		'p', '`', ':', '#', '@', '\'', '=', '"', ']', 'ｱ',
		'ｲ', 'ｳ', 'ｴ', 'ｵ', 'ｶ', 'ｷ', 'ｸ', 'ｹ', 'ｺ', 'q',
		'ｻ', 'ｼ', 'ｽ', 'ｾ', 'ｿ', 'ﾀ', 'ﾁ', 'ﾂ', 'ﾃ', 'ﾄ',
		'ﾅ', 'ﾆ', 'ﾇ', 'ﾈ', 'ﾉ', 'r', '?', 'ﾊ', 'ﾋ', 'ﾌ',
		'~', '‾', 'ﾍ', 'ﾎ', 'ﾏ', 'ﾐ', 'ﾑ', 'ﾒ', 'ﾓ', 'ﾔ',
		'ﾕ', 's', 'ﾖ', 'ﾗ', 'ﾘ', 'ﾙ', '^', '¢', '\\', 't',
		'u', 'v', 'w', 'x', 'y', 'z', 'ﾚ', 'ﾛ', 'ﾜ', 'ﾝ',
		'ﾞ', 'ﾟ', '{', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
		'H', 'I', '?', '?', '?', '?', '?', '?', '}', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', '?', '?',
		'?', '?', '?', '?', '$', '?', 'S', 'T', 'U', 'V',
		'W', 'X', 'Y', 'Z', '?', '?', '?', '?', '?', '?',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'?', '?', '?', '?', '?', '\u009f'
	};

	public CP20290()
		: base(20290, ToChars, "IBM EBCDIC (Japanese Katakana Extended)", "IBM290", "IBM290", "IBM290", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 932)
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
			if (num >= 4)
			{
				switch (num)
				{
				case 4:
					num = 55;
					break;
				case 5:
					num = 45;
					break;
				case 6:
					num = 46;
					break;
				case 7:
					num = 47;
					break;
				case 8:
					num = 22;
					break;
				case 9:
					num = 5;
					break;
				case 10:
					num = 37;
					break;
				case 20:
					num = 60;
					break;
				case 21:
					num = 61;
					break;
				case 22:
					num = 50;
					break;
				case 23:
					num = 38;
					break;
				case 26:
					num = 63;
					break;
				case 27:
					num = 39;
					break;
				case 32:
					num = 64;
					break;
				case 33:
					num = 90;
					break;
				case 34:
					num = 127;
					break;
				case 35:
					num = 123;
					break;
				case 36:
					num = 224;
					break;
				case 37:
					num = 108;
					break;
				case 38:
					num = 80;
					break;
				case 39:
					num = 125;
					break;
				case 40:
					num = 77;
					break;
				case 41:
					num = 93;
					break;
				case 42:
					num = 92;
					break;
				case 43:
					num = 78;
					break;
				case 44:
					num = 107;
					break;
				case 45:
					num = 96;
					break;
				case 46:
					num = 75;
					break;
				case 47:
					num = 97;
					break;
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
					num += 192;
					break;
				case 58:
					num = 122;
					break;
				case 59:
					num = 94;
					break;
				case 60:
					num = 76;
					break;
				case 61:
					num = 126;
					break;
				case 62:
					num = 110;
					break;
				case 63:
					num = 111;
					break;
				case 64:
					num = 124;
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
					num += 128;
					break;
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
					num += 135;
					break;
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
					num += 143;
					break;
				case 91:
					num = 112;
					break;
				case 92:
					num = 178;
					break;
				case 93:
					num = 128;
					break;
				case 94:
					num = 176;
					break;
				case 95:
					num = 109;
					break;
				case 96:
					num = 121;
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
					num++;
					break;
				case 105:
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
					num += 8;
					break;
				case 113:
					num = 139;
					break;
				case 114:
					num = 155;
					break;
				case 115:
					num = 171;
					break;
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
					num += 63;
					break;
				case 123:
					num = 192;
					break;
				case 124:
					num = 79;
					break;
				case 125:
					num = 208;
					break;
				case 126:
					num = 160;
					break;
				case 127:
					num = 7;
					break;
				case 128:
				case 129:
				case 130:
				case 131:
				case 132:
					num -= 96;
					break;
				case 133:
					num = 21;
					break;
				case 134:
					num = 6;
					break;
				case 135:
					num = 23;
					break;
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
					num -= 96;
					break;
				case 141:
					num = 9;
					break;
				case 142:
					num = 10;
					break;
				case 143:
					num = 27;
					break;
				case 144:
					num = 48;
					break;
				case 145:
					num = 49;
					break;
				case 146:
					num = 26;
					break;
				case 147:
				case 148:
				case 149:
				case 150:
					num -= 96;
					break;
				case 151:
					num = 8;
					break;
				case 152:
				case 153:
				case 154:
				case 155:
					num -= 96;
					break;
				case 156:
					num = 4;
					break;
				case 157:
					num = 20;
					break;
				case 158:
					num = 62;
					break;
				case 159:
					num = 255;
					break;
				case 162:
					num = 177;
					break;
				case 163:
					num = 74;
					break;
				case 165:
					num = 91;
					break;
				case 172:
					num = 95;
					break;
				case 8254:
					num = 161;
					break;
				case 65281:
					num = 90;
					break;
				case 65282:
					num = 127;
					break;
				case 65283:
					num = 123;
					break;
				case 65284:
					num = 224;
					break;
				case 65285:
					num = 108;
					break;
				case 65286:
					num = 80;
					break;
				case 65287:
					num = 125;
					break;
				case 65288:
					num = 77;
					break;
				case 65289:
					num = 93;
					break;
				case 65290:
					num = 92;
					break;
				case 65291:
					num = 78;
					break;
				case 65292:
					num = 107;
					break;
				case 65293:
					num = 96;
					break;
				case 65294:
					num = 75;
					break;
				case 65295:
					num = 97;
					break;
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
					num -= 65056;
					break;
				case 65306:
					num = 122;
					break;
				case 65307:
					num = 94;
					break;
				case 65308:
					num = 76;
					break;
				case 65309:
					num = 126;
					break;
				case 65310:
					num = 110;
					break;
				case 65311:
					num = 111;
					break;
				case 65312:
					num = 124;
					break;
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
					num -= 65120;
					break;
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
					num -= 65113;
					break;
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
					num -= 65105;
					break;
				case 65339:
					num = 112;
					break;
				case 65340:
					num = 178;
					break;
				case 65341:
					num = 128;
					break;
				case 65342:
					num = 176;
					break;
				case 65343:
					num = 109;
					break;
				case 65344:
					num = 121;
					break;
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
					num -= 65247;
					break;
				case 65353:
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
					num -= 65240;
					break;
				case 65361:
					num = 139;
					break;
				case 65362:
					num = 155;
					break;
				case 65363:
					num = 171;
					break;
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
					num -= 65185;
					break;
				case 65371:
					num = 192;
					break;
				case 65372:
					num = 79;
					break;
				case 65373:
					num = 208;
					break;
				case 65374:
					num = 160;
					break;
				case 65377:
				case 65378:
				case 65379:
				case 65380:
				case 65381:
				case 65382:
				case 65383:
				case 65384:
				case 65385:
					num -= 65312;
					break;
				case 65386:
				case 65387:
				case 65388:
				case 65389:
				case 65390:
				case 65391:
					num -= 65305;
					break;
				case 65392:
					num = 88;
					break;
				case 65393:
				case 65394:
				case 65395:
				case 65396:
				case 65397:
				case 65398:
				case 65399:
				case 65400:
				case 65401:
				case 65402:
					num -= 65264;
					break;
				case 65403:
				case 65404:
				case 65405:
				case 65406:
				case 65407:
				case 65408:
				case 65409:
				case 65410:
				case 65411:
				case 65412:
				case 65413:
				case 65414:
				case 65415:
				case 65416:
				case 65417:
					num -= 65263;
					break;
				case 65418:
					num = 157;
					break;
				case 65419:
					num = 158;
					break;
				case 65420:
					num = 159;
					break;
				case 65421:
				case 65422:
				case 65423:
				case 65424:
				case 65425:
				case 65426:
				case 65427:
				case 65428:
				case 65429:
					num -= 65259;
					break;
				case 65430:
				case 65431:
				case 65432:
				case 65433:
					num -= 65258;
					break;
				case 65434:
				case 65435:
				case 65436:
				case 65437:
				case 65438:
				case 65439:
					num -= 65248;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 24:
				case 25:
				case 28:
				case 29:
				case 30:
				case 31:
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
public class ENCibm290 : CP20290
{
}
[Serializable]
public class CP20297 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
		'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
		'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
		'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
		'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
		'\u0014', '\u0015', '\u009e', '\u001a', ' ', '\u00a0', 'â', 'ä', '@', 'á',
		'ã', 'å', '\\', 'ñ', '°', '.', '<', '(', '+', '!',
		'&', '{', 'ê', 'ë', '}', 'í', 'î', 'ï', 'ì', 'ß',
		'§', '$', '*', ')', ';', '^', '-', '/', 'Â', 'Ä',
		'À', 'Á', 'Ã', 'Å', 'Ç', 'Ñ', 'ù', ',', '%', '_',
		'>', '?', 'ø', 'É', 'Ê', 'Ë', 'È', 'Í', 'Î', 'Ï',
		'Ì', 'µ', ':', '£', 'à', '\'', '=', '"', 'Ø', 'a',
		'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', '«', '»',
		'ð', 'ý', 'þ', '±', '[', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', 'ª', 'º', 'æ', '\u00b8', 'Æ', '¤',
		'`', '\u00a8', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'¡', '¿', 'Ð', 'Ý', 'Þ', '®', '¢', '#', '¥', '·',
		'©', ']', '¶', '¼', '½', '¾', '¬', '|', '\u00af', '~',
		'\u00b4', '×', 'é', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
		'H', 'I', '\u00ad', 'ô', 'ö', 'ò', 'ó', 'õ', 'è', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', '¹', 'û',
		'ü', '¦', 'ú', 'ÿ', 'ç', '÷', 'S', 'T', 'U', 'V',
		'W', 'X', 'Y', 'Z', '²', 'Ô', 'Ö', 'Ò', 'Ó', 'Õ',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'³', 'Û', 'Ü', 'Ù', 'Ú', '\u009f'
	};

	public CP20297()
		: base(20297, ToChars, "IBM EBCDIC (France)", "IBM297", "IBM297", "IBM297", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
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
			if (num >= 4)
			{
				switch (num)
				{
				case 4:
					num = 55;
					break;
				case 5:
					num = 45;
					break;
				case 6:
					num = 46;
					break;
				case 7:
					num = 47;
					break;
				case 8:
					num = 22;
					break;
				case 9:
					num = 5;
					break;
				case 10:
					num = 37;
					break;
				case 20:
					num = 60;
					break;
				case 21:
					num = 61;
					break;
				case 22:
					num = 50;
					break;
				case 23:
					num = 38;
					break;
				case 26:
					num = 63;
					break;
				case 27:
					num = 39;
					break;
				case 32:
					num = 64;
					break;
				case 33:
					num = 79;
					break;
				case 34:
					num = 127;
					break;
				case 35:
					num = 177;
					break;
				case 36:
					num = 91;
					break;
				case 37:
					num = 108;
					break;
				case 38:
					num = 80;
					break;
				case 39:
					num = 125;
					break;
				case 40:
					num = 77;
					break;
				case 41:
					num = 93;
					break;
				case 42:
					num = 92;
					break;
				case 43:
					num = 78;
					break;
				case 44:
					num = 107;
					break;
				case 45:
					num = 96;
					break;
				case 46:
					num = 75;
					break;
				case 47:
					num = 97;
					break;
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
					num += 192;
					break;
				case 58:
					num = 122;
					break;
				case 59:
					num = 94;
					break;
				case 60:
					num = 76;
					break;
				case 61:
					num = 126;
					break;
				case 62:
					num = 110;
					break;
				case 63:
					num = 111;
					break;
				case 64:
					num = 68;
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
					num += 128;
					break;
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
					num += 135;
					break;
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
					num += 143;
					break;
				case 91:
					num = 144;
					break;
				case 92:
					num = 72;
					break;
				case 93:
					num = 181;
					break;
				case 94:
					num = 95;
					break;
				case 95:
					num = 109;
					break;
				case 96:
					num = 160;
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
					num += 32;
					break;
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
					num += 39;
					break;
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
					num += 47;
					break;
				case 123:
					num = 81;
					break;
				case 124:
					num = 187;
					break;
				case 125:
					num = 84;
					break;
				case 126:
					num = 189;
					break;
				case 127:
					num = 7;
					break;
				case 128:
				case 129:
				case 130:
				case 131:
				case 132:
					num -= 96;
					break;
				case 133:
					num = 21;
					break;
				case 134:
					num = 6;
					break;
				case 135:
					num = 23;
					break;
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
					num -= 96;
					break;
				case 141:
					num = 9;
					break;
				case 142:
					num = 10;
					break;
				case 143:
					num = 27;
					break;
				case 144:
					num = 48;
					break;
				case 145:
					num = 49;
					break;
				case 146:
					num = 26;
					break;
				case 147:
				case 148:
				case 149:
				case 150:
					num -= 96;
					break;
				case 151:
					num = 8;
					break;
				case 152:
				case 153:
				case 154:
				case 155:
					num -= 96;
					break;
				case 156:
					num = 4;
					break;
				case 157:
					num = 20;
					break;
				case 158:
					num = 62;
					break;
				case 159:
					num = 255;
					break;
				case 160:
					num = 65;
					break;
				case 161:
					num = 170;
					break;
				case 162:
					num = 176;
					break;
				case 163:
					num = 123;
					break;
				case 164:
					num = 159;
					break;
				case 165:
					num = 178;
					break;
				case 166:
					num = 221;
					break;
				case 167:
					num = 90;
					break;
				case 168:
					num = 161;
					break;
				case 169:
					num = 180;
					break;
				case 170:
					num = 154;
					break;
				case 171:
					num = 138;
					break;
				case 172:
					num = 186;
					break;
				case 173:
					num = 202;
					break;
				case 174:
					num = 175;
					break;
				case 175:
					num = 188;
					break;
				case 176:
					num = 74;
					break;
				case 177:
					num = 143;
					break;
				case 178:
					num = 234;
					break;
				case 179:
					num = 250;
					break;
				case 180:
					num = 190;
					break;
				case 181:
					num = 121;
					break;
				case 183:
					num = 179;
					break;
				case 184:
					num = 157;
					break;
				case 185:
					num = 218;
					break;
				case 186:
					num = 155;
					break;
				case 187:
					num = 139;
					break;
				case 188:
					num = 183;
					break;
				case 189:
					num = 184;
					break;
				case 190:
					num = 185;
					break;
				case 191:
					num = 171;
					break;
				case 192:
					num = 100;
					break;
				case 193:
					num = 101;
					break;
				case 194:
					num = 98;
					break;
				case 195:
					num = 102;
					break;
				case 196:
					num = 99;
					break;
				case 197:
					num = 103;
					break;
				case 198:
					num = 158;
					break;
				case 199:
					num = 104;
					break;
				case 200:
					num = 116;
					break;
				case 201:
					num = 113;
					break;
				case 202:
					num = 114;
					break;
				case 203:
					num = 115;
					break;
				case 204:
					num = 120;
					break;
				case 205:
					num = 117;
					break;
				case 206:
					num = 118;
					break;
				case 207:
					num = 119;
					break;
				case 208:
					num = 172;
					break;
				case 209:
					num = 105;
					break;
				case 210:
					num = 237;
					break;
				case 211:
					num = 238;
					break;
				case 212:
					num = 235;
					break;
				case 213:
					num = 239;
					break;
				case 214:
					num = 236;
					break;
				case 215:
					num = 191;
					break;
				case 216:
					num = 128;
					break;
				case 217:
					num = 253;
					break;
				case 218:
					num = 254;
					break;
				case 219:
					num = 251;
					break;
				case 220:
					num = 252;
					break;
				case 221:
					num = 173;
					break;
				case 222:
					num = 174;
					break;
				case 223:
					num = 89;
					break;
				case 224:
					num = 124;
					break;
				case 225:
					num = 69;
					break;
				case 226:
					num = 66;
					break;
				case 227:
					num = 70;
					break;
				case 228:
					num = 67;
					break;
				case 229:
					num = 71;
					break;
				case 230:
					num = 156;
					break;
				case 231:
					num = 224;
					break;
				case 232:
					num = 208;
					break;
				case 233:
					num = 192;
					break;
				case 234:
					num = 82;
					break;
				case 235:
					num = 83;
					break;
				case 236:
					num = 88;
					break;
				case 237:
					num = 85;
					break;
				case 238:
					num = 86;
					break;
				case 239:
					num = 87;
					break;
				case 240:
					num = 140;
					break;
				case 241:
					num = 73;
					break;
				case 242:
					num = 205;
					break;
				case 243:
					num = 206;
					break;
				case 244:
					num = 203;
					break;
				case 245:
					num = 207;
					break;
				case 246:
					num = 204;
					break;
				case 247:
					num = 225;
					break;
				case 248:
					num = 112;
					break;
				case 249:
					num = 106;
					break;
				case 250:
					num = 222;
					break;
				case 251:
					num = 219;
					break;
				case 252:
					num = 220;
					break;
				case 253:
					num = 141;
					break;
				case 254:
					num = 142;
					break;
				case 255:
					num = 223;
					break;
				case 272:
					num = 172;
					break;
				case 8254:
					num = 188;
					break;
				case 65281:
					num = 79;
					break;
				case 65282:
					num = 127;
					break;
				case 65283:
					num = 177;
					break;
				case 65284:
					num = 91;
					break;
				case 65285:
					num = 108;
					break;
				case 65286:
					num = 80;
					break;
				case 65287:
					num = 125;
					break;
				case 65288:
					num = 77;
					break;
				case 65289:
					num = 93;
					break;
				case 65290:
					num = 92;
					break;
				case 65291:
					num = 78;
					break;
				case 65292:
					num = 107;
					break;
				case 65293:
					num = 96;
					break;
				case 65294:
					num = 75;
					break;
				case 65295:
					num = 97;
					break;
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
					num -= 65056;
					break;
				case 65306:
					num = 122;
					break;
				case 65307:
					num = 94;
					break;
				case 65308:
					num = 76;
					break;
				case 65309:
					num = 126;
					break;
				case 65310:
					num = 110;
					break;
				case 65311:
					num = 111;
					break;
				case 65312:
					num = 68;
					break;
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
					num -= 65120;
					break;
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
					num -= 65113;
					break;
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
					num -= 65105;
					break;
				case 65339:
					num = 144;
					break;
				case 65340:
					num = 72;
					break;
				case 65341:
					num = 181;
					break;
				case 65342:
					num = 95;
					break;
				case 65343:
					num = 109;
					break;
				case 65344:
					num = 160;
					break;
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
				case 65353:
					num -= 65216;
					break;
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
				case 65361:
				case 65362:
					num -= 65209;
					break;
				case 65363:
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
					num -= 65201;
					break;
				case 65371:
					num = 81;
					break;
				case 65372:
					num = 187;
					break;
				case 65373:
					num = 84;
					break;
				case 65374:
					num = 189;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 24:
				case 25:
				case 28:
				case 29:
				case 30:
				case 31:
				case 182:
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
public class ENCibm297 : CP20297
{
}
[Serializable]
public class CP20420 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
		'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
		'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
		'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
		'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
		'\u0014', '\u0015', '\u009e', '\u001a', ' ', '\u00a0', '\u0651', 'ﹽ', 'ـ', '\u200b',
		'ء', 'آ', 'ﺂ', 'أ', '¢', '.', '<', '(', '+', '|',
		'&', 'ﺄ', 'ؤ', '?', '?', 'ئ', 'ا', 'ﺎ', 'ب', 'ﺑ',
		'!', '$', '*', ')', ';', '¬', '-', '/', 'ة', 'ت',
		'ﺗ', 'ث', 'ﺛ', 'ج', 'ﺟ', 'ح', '¦', ',', '%', '_',
		'>', '?', 'ﺣ', 'خ', 'ﺧ', 'د', 'ذ', 'ر', 'ز', 'س',
		'ﺳ', '،', ':', '#', '@', '\'', '=', '"', 'ش', 'a',
		'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'ﺷ', 'ص',
		'ﺻ', 'ض', 'ﺿ', 'ط', 'ظ', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', 'ع', 'ﻊ', 'ﻋ', 'ﻌ', 'غ', 'ﻎ',
		'ﻏ', '÷', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'ﻐ', 'ف', 'ﻓ', 'ق', 'ﻗ', 'ك', 'ﻛ', 'ل', 'ﻵ', 'ﻶ',
		'ﻷ', 'ﻸ', '?', '?', 'ﻻ', 'ﻼ', 'ﻟ', 'م', 'ﻣ', 'ن',
		'ﻧ', 'ه', '؛', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
		'H', 'I', '\u00ad', 'ﻫ', '?', 'ﻬ', '?', 'و', '؟', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'ى', 'ﻰ',
		'ي', 'ﻲ', 'ﻳ', '٠', '×', '?', 'S', 'T', 'U', 'V',
		'W', 'X', 'Y', 'Z', '١', '٢', '?', '٣', '٤', '٥',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'?', '٦', '٧', '٨', '٩', '\u009f'
	};

	public CP20420()
		: base(20420, ToChars, "IBM EBCDIC (Arabic)", "IBM420", "IBM420", "IBM420", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1256)
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
			if (num >= 4)
			{
				switch (num)
				{
				case 4:
					num = 55;
					break;
				case 5:
					num = 45;
					break;
				case 6:
					num = 46;
					break;
				case 7:
					num = 47;
					break;
				case 8:
					num = 22;
					break;
				case 9:
					num = 5;
					break;
				case 10:
					num = 37;
					break;
				case 20:
					num = 60;
					break;
				case 21:
					num = 61;
					break;
				case 22:
					num = 50;
					break;
				case 23:
					num = 38;
					break;
				case 26:
					num = 63;
					break;
				case 27:
					num = 39;
					break;
				case 32:
					num = 64;
					break;
				case 33:
					num = 90;
					break;
				case 34:
					num = 127;
					break;
				case 35:
					num = 123;
					break;
				case 36:
					num = 91;
					break;
				case 37:
					num = 108;
					break;
				case 38:
					num = 80;
					break;
				case 39:
					num = 125;
					break;
				case 40:
					num = 77;
					break;
				case 41:
					num = 93;
					break;
				case 42:
					num = 92;
					break;
				case 43:
					num = 78;
					break;
				case 44:
					num = 107;
					break;
				case 45:
					num = 96;
					break;
				case 46:
					num = 75;
					break;
				case 47:
					num = 97;
					break;
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
					num += 192;
					break;
				case 58:
					num = 122;
					break;
				case 59:
					num = 94;
					break;
				case 60:
					num = 76;
					break;
				case 61:
					num = 126;
					break;
				case 62:
					num = 110;
					break;
				case 63:
					num = 111;
					break;
				case 64:
					num = 124;
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
					num += 128;
					break;
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
					num += 135;
					break;
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
					num += 143;
					break;
				case 95:
					num = 109;
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
					num += 32;
					break;
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
					num += 39;
					break;
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
					num += 47;
					break;
				case 124:
					num = 79;
					break;
				case 127:
					num = 7;
					break;
				case 128:
				case 129:
				case 130:
				case 131:
				case 132:
					num -= 96;
					break;
				case 133:
					num = 21;
					break;
				case 134:
					num = 6;
					break;
				case 135:
					num = 23;
					break;
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
					num -= 96;
					break;
				case 141:
					num = 9;
					break;
				case 142:
					num = 10;
					break;
				case 143:
					num = 27;
					break;
				case 144:
					num = 48;
					break;
				case 145:
					num = 49;
					break;
				case 146:
					num = 26;
					break;
				case 147:
				case 148:
				case 149:
				case 150:
					num -= 96;
					break;
				case 151:
					num = 8;
					break;
				case 152:
				case 153:
				case 154:
				case 155:
					num -= 96;
					break;
				case 156:
					num = 4;
					break;
				case 157:
					num = 20;
					break;
				case 158:
					num = 62;
					break;
				case 159:
					num = 255;
					break;
				case 160:
					num = 65;
					break;
				case 162:
					num = 74;
					break;
				case 166:
					num = 106;
					break;
				case 172:
					num = 95;
					break;
				case 173:
					num = 202;
					break;
				case 215:
					num = 224;
					break;
				case 247:
					num = 161;
					break;
				case 1548:
					num = 121;
					break;
				case 1563:
					num = 192;
					break;
				case 1567:
					num = 208;
					break;
				case 1569:
					num = 70;
					break;
				case 1570:
					num = 71;
					break;
				case 1571:
					num = 73;
					break;
				case 1572:
					num = 82;
					break;
				case 1573:
					num = 86;
					break;
				case 1574:
					num = 85;
					break;
				case 1575:
					num = 86;
					break;
				case 1576:
					num = 88;
					break;
				case 1577:
					num = 98;
					break;
				case 1578:
					num = 99;
					break;
				case 1579:
					num = 101;
					break;
				case 1580:
					num = 103;
					break;
				case 1581:
					num = 105;
					break;
				case 1582:
					num = 113;
					break;
				case 1583:
				case 1584:
				case 1585:
				case 1586:
				case 1587:
					num -= 1468;
					break;
				case 1588:
					num = 128;
					break;
				case 1589:
					num = 139;
					break;
				case 1590:
					num = 141;
					break;
				case 1591:
					num = 143;
					break;
				case 1592:
					num = 144;
					break;
				case 1593:
					num = 154;
					break;
				case 1594:
					num = 158;
					break;
				case 1600:
					num = 68;
					break;
				case 1601:
					num = 171;
					break;
				case 1602:
					num = 173;
					break;
				case 1603:
					num = 175;
					break;
				case 1604:
					num = 177;
					break;
				case 1605:
					num = 187;
					break;
				case 1606:
					num = 189;
					break;
				case 1607:
					num = 191;
					break;
				case 1608:
					num = 207;
					break;
				case 1609:
					num = 218;
					break;
				case 1610:
					num = 220;
					break;
				case 1617:
					num = 66;
					break;
				case 1632:
					num = 223;
					break;
				case 1633:
					num = 234;
					break;
				case 1634:
					num = 235;
					break;
				case 1635:
					num = 237;
					break;
				case 1636:
					num = 238;
					break;
				case 1637:
					num = 239;
					break;
				case 1638:
				case 1639:
				case 1640:
				case 1641:
					num -= 1387;
					break;
				case 1642:
					num = 108;
					break;
				case 1643:
					num = 107;
					break;
				case 1644:
					num = 75;
					break;
				case 1645:
					num = 92;
					break;
				case 8203:
					num = 69;
					break;
				case 65148:
					num = 66;
					break;
				case 65149:
					num = 67;
					break;
				case 65152:
				case 65153:
				case 65154:
				case 65155:
					num -= 65082;
					break;
				case 65156:
					num = 81;
					break;
				case 65157:
					num = 82;
					break;
				case 65158:
					num = 82;
					break;
				case 65159:
					num = 86;
					break;
				case 65160:
					num = 87;
					break;
				case 65163:
					num = 85;
					break;
				case 65164:
				case 65165:
				case 65166:
				case 65167:
					num -= 65079;
					break;
				case 65168:
					num = 88;
					break;
				case 65169:
					num = 89;
					break;
				case 65170:
					num = 89;
					break;
				case 65171:
					num = 98;
					break;
				case 65172:
					num = 98;
					break;
				case 65173:
					num = 99;
					break;
				case 65174:
					num = 99;
					break;
				case 65175:
					num = 100;
					break;
				case 65176:
					num = 100;
					break;
				case 65177:
					num = 101;
					break;
				case 65178:
					num = 101;
					break;
				case 65179:
					num = 102;
					break;
				case 65180:
					num = 102;
					break;
				case 65181:
					num = 103;
					break;
				case 65182:
					num = 103;
					break;
				case 65183:
					num = 104;
					break;
				case 65184:
					num = 104;
					break;
				case 65185:
					num = 105;
					break;
				case 65186:
					num = 105;
					break;
				case 65187:
					num = 112;
					break;
				case 65188:
					num = 112;
					break;
				case 65189:
					num = 113;
					break;
				case 65190:
					num = 113;
					break;
				case 65191:
					num = 114;
					break;
				case 65192:
					num = 114;
					break;
				case 65193:
					num = 115;
					break;
				case 65194:
					num = 115;
					break;
				case 65195:
					num = 116;
					break;
				case 65196:
					num = 116;
					break;
				case 65197:
					num = 117;
					break;
				case 65198:
					num = 117;
					break;
				case 65199:
					num = 118;
					break;
				case 65200:
					num = 118;
					break;
				case 65201:
					num = 119;
					break;
				case 65202:
					num = 119;
					break;
				case 65203:
					num = 120;
					break;
				case 65204:
					num = 120;
					break;
				case 65205:
					num = 128;
					break;
				case 65206:
					num = 128;
					break;
				case 65207:
					num = 138;
					break;
				case 65208:
					num = 138;
					break;
				case 65209:
					num = 139;
					break;
				case 65210:
					num = 139;
					break;
				case 65211:
					num = 140;
					break;
				case 65212:
					num = 140;
					break;
				case 65213:
					num = 141;
					break;
				case 65214:
					num = 141;
					break;
				case 65215:
					num = 142;
					break;
				case 65216:
					num = 142;
					break;
				case 65217:
					num = 143;
					break;
				case 65218:
					num = 143;
					break;
				case 65219:
					num = 143;
					break;
				case 65220:
					num = 143;
					break;
				case 65221:
					num = 144;
					break;
				case 65222:
					num = 144;
					break;
				case 65223:
					num = 144;
					break;
				case 65224:
					num = 144;
					break;
				case 65225:
				case 65226:
				case 65227:
				case 65228:
				case 65229:
				case 65230:
				case 65231:
					num -= 65071;
					break;
				case 65232:
					num = 170;
					break;
				case 65233:
					num = 171;
					break;
				case 65234:
					num = 171;
					break;
				case 65235:
					num = 172;
					break;
				case 65236:
					num = 172;
					break;
				case 65237:
					num = 173;
					break;
				case 65238:
					num = 173;
					break;
				case 65239:
					num = 174;
					break;
				case 65240:
					num = 174;
					break;
				case 65241:
					num = 175;
					break;
				case 65242:
					num = 175;
					break;
				case 65243:
					num = 176;
					break;
				case 65244:
					num = 176;
					break;
				case 65245:
					num = 177;
					break;
				case 65246:
					num = 177;
					break;
				case 65247:
					num = 186;
					break;
				case 65248:
					num = 186;
					break;
				case 65249:
					num = 187;
					break;
				case 65250:
					num = 187;
					break;
				case 65251:
					num = 188;
					break;
				case 65252:
					num = 188;
					break;
				case 65253:
					num = 189;
					break;
				case 65254:
					num = 189;
					break;
				case 65255:
					num = 190;
					break;
				case 65256:
					num = 190;
					break;
				case 65257:
					num = 191;
					break;
				case 65258:
					num = 191;
					break;
				case 65259:
					num = 203;
					break;
				case 65260:
					num = 205;
					break;
				case 65261:
					num = 207;
					break;
				case 65262:
					num = 207;
					break;
				case 65263:
				case 65264:
				case 65265:
				case 65266:
				case 65267:
					num -= 65045;
					break;
				case 65268:
					num = 222;
					break;
				case 65269:
				case 65270:
				case 65271:
				case 65272:
					num -= 65091;
					break;
				case 65273:
					num = 184;
					break;
				case 65274:
					num = 185;
					break;
				case 65275:
					num = 184;
					break;
				case 65276:
					num = 185;
					break;
				case 65281:
					num = 90;
					break;
				case 65282:
					num = 127;
					break;
				case 65283:
					num = 123;
					break;
				case 65284:
					num = 91;
					break;
				case 65285:
					num = 108;
					break;
				case 65286:
					num = 80;
					break;
				case 65287:
					num = 125;
					break;
				case 65288:
					num = 77;
					break;
				case 65289:
					num = 93;
					break;
				case 65290:
					num = 92;
					break;
				case 65291:
					num = 78;
					break;
				case 65292:
					num = 107;
					break;
				case 65293:
					num = 96;
					break;
				case 65294:
					num = 75;
					break;
				case 65295:
					num = 97;
					break;
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
					num -= 65056;
					break;
				case 65306:
					num = 122;
					break;
				case 65307:
					num = 94;
					break;
				case 65308:
					num = 76;
					break;
				case 65309:
					num = 126;
					break;
				case 65310:
					num = 110;
					break;
				case 65311:
					num = 111;
					break;
				case 65312:
					num = 124;
					break;
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
					num -= 65120;
					break;
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
					num -= 65113;
					break;
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
					num -= 65105;
					break;
				case 65343:
					num = 109;
					break;
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
				case 65353:
					num -= 65216;
					break;
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
				case 65361:
				case 65362:
					num -= 65209;
					break;
				case 65363:
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
					num -= 65201;
					break;
				case 65372:
					num = 79;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 24:
				case 25:
				case 28:
				case 29:
				case 30:
				case 31:
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
public class ENCibm420 : CP20420
{
}
[Serializable]
public class CP20424 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
		'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
		'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
		'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
		'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
		'\u0014', '\u0015', '\u009e', '\u001a', ' ', 'א', 'ב', 'ג', 'ד', 'ה',
		'ו', 'ז', 'ח', 'ט', '¢', '.', '<', '(', '+', '|',
		'&', 'י', 'ך', 'כ', 'ל', 'ם', 'מ', 'ן', 'נ', 'ס',
		'!', '$', '*', ')', ';', '¬', '-', '/', 'ע', 'ף',
		'פ', 'ץ', 'צ', 'ק', 'ר', 'ש', '¦', ',', '%', '_',
		'>', '?', '?', 'ת', '?', '?', '\u00a0', '?', '?', '?',
		'‗', '`', ':', '#', '@', '\'', '=', '"', '?', 'a',
		'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', '«', '»',
		'?', '?', '?', '±', '°', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', '?', '?', '?', '\u00b8', '?', '¤',
		'µ', '~', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'?', '?', '?', '?', '?', '®', '^', '£', '¥', '•',
		'©', '§', '¶', '¼', '½', '¾', '[', ']', '‾', '\u00a8',
		'\u00b4', '×', '{', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
		'H', 'I', '\u00ad', '?', '?', '?', '?', '?', '}', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', '¹', '?',
		'?', '?', '?', '?', '\\', '÷', 'S', 'T', 'U', 'V',
		'W', 'X', 'Y', 'Z', '²', '?', '?', '?', '?', '?',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'³', '?', '?', '?', '?', '\u009f'
	};

	public CP20424()
		: base(20424, ToChars, "IBM EBCDIC (Hebrew)", "IBM424", "IBM424", "IBM424", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1255)
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
			if (num >= 4)
			{
				switch (num)
				{
				case 4:
					num = 55;
					break;
				case 5:
					num = 45;
					break;
				case 6:
					num = 46;
					break;
				case 7:
					num = 47;
					break;
				case 8:
					num = 22;
					break;
				case 9:
					num = 5;
					break;
				case 10:
					num = 37;
					break;
				case 20:
					num = 60;
					break;
				case 21:
					num = 61;
					break;
				case 22:
					num = 50;
					break;
				case 23:
					num = 38;
					break;
				case 26:
					num = 63;
					break;
				case 27:
					num = 39;
					break;
				case 32:
					num = 64;
					break;
				case 33:
					num = 90;
					break;
				case 34:
					num = 127;
					break;
				case 35:
					num = 123;
					break;
				case 36:
					num = 91;
					break;
				case 37:
					num = 108;
					break;
				case 38:
					num = 80;
					break;
				case 39:
					num = 125;
					break;
				case 40:
					num = 77;
					break;
				case 41:
					num = 93;
					break;
				case 42:
					num = 92;
					break;
				case 43:
					num = 78;
					break;
				case 44:
					num = 107;
					break;
				case 45:
					num = 96;
					break;
				case 46:
					num = 75;
					break;
				case 47:
					num = 97;
					break;
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
					num += 192;
					break;
				case 58:
					num = 122;
					break;
				case 59:
					num = 94;
					break;
				case 60:
					num = 76;
					break;
				case 61:
					num = 126;
					break;
				case 62:
					num = 110;
					break;
				case 63:
					num = 111;
					break;
				case 64:
					num = 124;
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
					num += 128;
					break;
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
					num += 135;
					break;
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
					num += 143;
					break;
				case 91:
					num = 186;
					break;
				case 92:
					num = 224;
					break;
				case 93:
					num = 187;
					break;
				case 94:
					num = 176;
					break;
				case 95:
					num = 109;
					break;
				case 96:
					num = 121;
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
					num += 32;
					break;
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
					num += 39;
					break;
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
					num += 47;
					break;
				case 123:
					num = 192;
					break;
				case 124:
					num = 79;
					break;
				case 125:
					num = 208;
					break;
				case 126:
					num = 161;
					break;
				case 127:
					num = 7;
					break;
				case 128:
				case 129:
				case 130:
				case 131:
				case 132:
					num -= 96;
					break;
				case 133:
					num = 21;
					break;
				case 134:
					num = 6;
					break;
				case 135:
					num = 23;
					break;
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
					num -= 96;
					break;
				case 141:
					num = 9;
					break;
				case 142:
					num = 10;
					break;
				case 143:
					num = 27;
					break;
				case 144:
					num = 48;
					break;
				case 145:
					num = 49;
					break;
				case 146:
					num = 26;
					break;
				case 147:
				case 148:
				case 149:
				case 150:
					num -= 96;
					break;
				case 151:
					num = 8;
					break;
				case 152:
				case 153:
				case 154:
				case 155:
					num -= 96;
					break;
				case 156:
					num = 4;
					break;
				case 157:
					num = 20;
					break;
				case 158:
					num = 62;
					break;
				case 159:
					num = 255;
					break;
				case 160:
					num = 116;
					break;
				case 162:
					num = 74;
					break;
				case 163:
					num = 177;
					break;
				case 164:
					num = 159;
					break;
				case 165:
					num = 178;
					break;
				case 166:
					num = 106;
					break;
				case 167:
					num = 181;
					break;
				case 168:
					num = 189;
					break;
				case 169:
					num = 180;
					break;
				case 171:
					num = 138;
					break;
				case 172:
					num = 95;
					break;
				case 173:
					num = 202;
					break;
				case 174:
					num = 175;
					break;
				case 176:
					num = 144;
					break;
				case 177:
					num = 143;
					break;
				case 178:
					num = 234;
					break;
				case 179:
					num = 250;
					break;
				case 180:
					num = 190;
					break;
				case 181:
					num = 160;
					break;
				case 184:
					num = 157;
					break;
				case 185:
					num = 218;
					break;
				case 187:
					num = 139;
					break;
				case 188:
					num = 183;
					break;
				case 189:
					num = 184;
					break;
				case 190:
					num = 185;
					break;
				case 215:
					num = 191;
					break;
				case 247:
					num = 225;
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
					num -= 1423;
					break;
				case 1497:
				case 1498:
				case 1499:
				case 1500:
				case 1501:
				case 1502:
				case 1503:
				case 1504:
				case 1505:
					num -= 1416;
					break;
				case 1506:
				case 1507:
				case 1508:
				case 1509:
				case 1510:
				case 1511:
				case 1512:
				case 1513:
					num -= 1408;
					break;
				case 1514:
					num = 113;
					break;
				case 8215:
					num = 120;
					break;
				case 8226:
					num = 179;
					break;
				case 8254:
					num = 188;
					break;
				case 65281:
					num = 90;
					break;
				case 65282:
					num = 127;
					break;
				case 65283:
					num = 123;
					break;
				case 65284:
					num = 91;
					break;
				case 65285:
					num = 108;
					break;
				case 65286:
					num = 80;
					break;
				case 65287:
					num = 125;
					break;
				case 65288:
					num = 77;
					break;
				case 65289:
					num = 93;
					break;
				case 65290:
					num = 92;
					break;
				case 65291:
					num = 78;
					break;
				case 65292:
					num = 107;
					break;
				case 65293:
					num = 96;
					break;
				case 65294:
					num = 75;
					break;
				case 65295:
					num = 97;
					break;
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
					num -= 65056;
					break;
				case 65306:
					num = 122;
					break;
				case 65307:
					num = 94;
					break;
				case 65308:
					num = 76;
					break;
				case 65309:
					num = 126;
					break;
				case 65310:
					num = 110;
					break;
				case 65311:
					num = 111;
					break;
				case 65312:
					num = 124;
					break;
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
					num -= 65120;
					break;
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
					num -= 65113;
					break;
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
					num -= 65105;
					break;
				case 65339:
					num = 186;
					break;
				case 65340:
					num = 224;
					break;
				case 65341:
					num = 187;
					break;
				case 65342:
					num = 176;
					break;
				case 65343:
					num = 109;
					break;
				case 65344:
					num = 121;
					break;
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
				case 65353:
					num -= 65216;
					break;
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
				case 65361:
				case 65362:
					num -= 65209;
					break;
				case 65363:
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
					num -= 65201;
					break;
				case 65371:
					num = 192;
					break;
				case 65372:
					num = 79;
					break;
				case 65373:
					num = 208;
					break;
				case 65374:
					num = 161;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 24:
				case 25:
				case 28:
				case 29:
				case 30:
				case 31:
				case 182:
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
public class ENCibm424 : CP20424
{
}
[Serializable]
public class CP20871 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
		'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
		'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
		'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
		'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
		'\u0014', '\u0015', '\u009e', '\u001a', ' ', '\u00a0', 'â', 'ä', 'à', 'á',
		'ã', 'å', 'ç', 'ñ', 'Þ', '.', '<', '(', '+', '!',
		'&', 'é', 'ê', 'ë', 'è', 'í', 'î', 'ï', 'ì', 'ß',
		'Æ', '$', '*', ')', ';', 'Ö', '-', '/', 'Â', 'Ä',
		'À', 'Á', 'Ã', 'Å', 'Ç', 'Ñ', '¦', ',', '%', '_',
		'>', '?', 'ø', 'É', 'Ê', 'Ë', 'È', 'Í', 'Î', 'Ï',
		'Ì', 'ð', ':', '#', 'Ð', '\'', '=', '"', 'Ø', 'a',
		'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', '«', '»',
		'`', 'ý', '{', '±', '°', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', 'ª', 'º', '}', '\u00b8', ']', '¤',
		'µ', 'ö', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'¡', '¿', '@', 'Ý', '[', '®', '¢', '£', '¥', '·',
		'©', '§', '¶', '¼', '½', '¾', '¬', '|', '\u00af', '\u00a8',
		'\\', '×', 'þ', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
		'H', 'I', '\u00ad', 'ô', '~', 'ò', 'ó', 'õ', 'æ', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', '¹', 'û',
		'ü', 'ù', 'ú', 'ÿ', '\u00b4', '÷', 'S', 'T', 'U', 'V',
		'W', 'X', 'Y', 'Z', '²', 'Ô', '^', 'Ò', 'Ó', 'Õ',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'³', 'Û', 'Ü', 'Ù', 'Ú', '\u009f'
	};

	public CP20871()
		: base(20871, ToChars, "IBM EBCDIC (Icelandic)", "IBM871", "IBM871", "IBM871", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
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
			if (num >= 4)
			{
				switch (num)
				{
				case 4:
					num = 55;
					break;
				case 5:
					num = 45;
					break;
				case 6:
					num = 46;
					break;
				case 7:
					num = 47;
					break;
				case 8:
					num = 22;
					break;
				case 9:
					num = 5;
					break;
				case 10:
					num = 37;
					break;
				case 20:
					num = 60;
					break;
				case 21:
					num = 61;
					break;
				case 22:
					num = 50;
					break;
				case 23:
					num = 38;
					break;
				case 26:
					num = 63;
					break;
				case 27:
					num = 39;
					break;
				case 32:
					num = 64;
					break;
				case 33:
					num = 79;
					break;
				case 34:
					num = 127;
					break;
				case 35:
					num = 123;
					break;
				case 36:
					num = 91;
					break;
				case 37:
					num = 108;
					break;
				case 38:
					num = 80;
					break;
				case 39:
					num = 125;
					break;
				case 40:
					num = 77;
					break;
				case 41:
					num = 93;
					break;
				case 42:
					num = 92;
					break;
				case 43:
					num = 78;
					break;
				case 44:
					num = 107;
					break;
				case 45:
					num = 96;
					break;
				case 46:
					num = 75;
					break;
				case 47:
					num = 97;
					break;
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
					num += 192;
					break;
				case 58:
					num = 122;
					break;
				case 59:
					num = 94;
					break;
				case 60:
					num = 76;
					break;
				case 61:
					num = 126;
					break;
				case 62:
					num = 110;
					break;
				case 63:
					num = 111;
					break;
				case 64:
					num = 172;
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
					num += 128;
					break;
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
					num += 135;
					break;
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
					num += 143;
					break;
				case 91:
					num = 174;
					break;
				case 92:
					num = 190;
					break;
				case 93:
					num = 158;
					break;
				case 94:
					num = 236;
					break;
				case 95:
					num = 109;
					break;
				case 96:
					num = 140;
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
					num += 32;
					break;
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
					num += 39;
					break;
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
					num += 47;
					break;
				case 123:
					num = 142;
					break;
				case 124:
					num = 187;
					break;
				case 125:
					num = 156;
					break;
				case 126:
					num = 204;
					break;
				case 127:
					num = 7;
					break;
				case 128:
				case 129:
				case 130:
				case 131:
				case 132:
					num -= 96;
					break;
				case 133:
					num = 21;
					break;
				case 134:
					num = 6;
					break;
				case 135:
					num = 23;
					break;
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
					num -= 96;
					break;
				case 141:
					num = 9;
					break;
				case 142:
					num = 10;
					break;
				case 143:
					num = 27;
					break;
				case 144:
					num = 48;
					break;
				case 145:
					num = 49;
					break;
				case 146:
					num = 26;
					break;
				case 147:
				case 148:
				case 149:
				case 150:
					num -= 96;
					break;
				case 151:
					num = 8;
					break;
				case 152:
				case 153:
				case 154:
				case 155:
					num -= 96;
					break;
				case 156:
					num = 4;
					break;
				case 157:
					num = 20;
					break;
				case 158:
					num = 62;
					break;
				case 159:
					num = 255;
					break;
				case 160:
					num = 65;
					break;
				case 161:
					num = 170;
					break;
				case 162:
					num = 176;
					break;
				case 163:
					num = 177;
					break;
				case 164:
					num = 159;
					break;
				case 165:
					num = 178;
					break;
				case 166:
					num = 106;
					break;
				case 167:
					num = 181;
					break;
				case 168:
					num = 189;
					break;
				case 169:
					num = 180;
					break;
				case 170:
					num = 154;
					break;
				case 171:
					num = 138;
					break;
				case 172:
					num = 186;
					break;
				case 173:
					num = 202;
					break;
				case 174:
					num = 175;
					break;
				case 175:
					num = 188;
					break;
				case 176:
					num = 144;
					break;
				case 177:
					num = 143;
					break;
				case 178:
					num = 234;
					break;
				case 179:
					num = 250;
					break;
				case 180:
					num = 224;
					break;
				case 181:
					num = 160;
					break;
				case 183:
					num = 179;
					break;
				case 184:
					num = 157;
					break;
				case 185:
					num = 218;
					break;
				case 186:
					num = 155;
					break;
				case 187:
					num = 139;
					break;
				case 188:
					num = 183;
					break;
				case 189:
					num = 184;
					break;
				case 190:
					num = 185;
					break;
				case 191:
					num = 171;
					break;
				case 192:
					num = 100;
					break;
				case 193:
					num = 101;
					break;
				case 194:
					num = 98;
					break;
				case 195:
					num = 102;
					break;
				case 196:
					num = 99;
					break;
				case 197:
					num = 103;
					break;
				case 198:
					num = 90;
					break;
				case 199:
					num = 104;
					break;
				case 200:
					num = 116;
					break;
				case 201:
					num = 113;
					break;
				case 202:
					num = 114;
					break;
				case 203:
					num = 115;
					break;
				case 204:
					num = 120;
					break;
				case 205:
					num = 117;
					break;
				case 206:
					num = 118;
					break;
				case 207:
					num = 119;
					break;
				case 208:
					num = 124;
					break;
				case 209:
					num = 105;
					break;
				case 210:
					num = 237;
					break;
				case 211:
					num = 238;
					break;
				case 212:
					num = 235;
					break;
				case 213:
					num = 239;
					break;
				case 214:
					num = 95;
					break;
				case 215:
					num = 191;
					break;
				case 216:
					num = 128;
					break;
				case 217:
					num = 253;
					break;
				case 218:
					num = 254;
					break;
				case 219:
					num = 251;
					break;
				case 220:
					num = 252;
					break;
				case 221:
					num = 173;
					break;
				case 222:
					num = 74;
					break;
				case 223:
					num = 89;
					break;
				case 224:
					num = 68;
					break;
				case 225:
					num = 69;
					break;
				case 226:
					num = 66;
					break;
				case 227:
					num = 70;
					break;
				case 228:
					num = 67;
					break;
				case 229:
					num = 71;
					break;
				case 230:
					num = 208;
					break;
				case 231:
					num = 72;
					break;
				case 232:
					num = 84;
					break;
				case 233:
					num = 81;
					break;
				case 234:
					num = 82;
					break;
				case 235:
					num = 83;
					break;
				case 236:
					num = 88;
					break;
				case 237:
					num = 85;
					break;
				case 238:
					num = 86;
					break;
				case 239:
					num = 87;
					break;
				case 240:
					num = 121;
					break;
				case 241:
					num = 73;
					break;
				case 242:
					num = 205;
					break;
				case 243:
					num = 206;
					break;
				case 244:
					num = 203;
					break;
				case 245:
					num = 207;
					break;
				case 246:
					num = 161;
					break;
				case 247:
					num = 225;
					break;
				case 248:
					num = 112;
					break;
				case 249:
					num = 221;
					break;
				case 250:
					num = 222;
					break;
				case 251:
					num = 219;
					break;
				case 252:
					num = 220;
					break;
				case 253:
					num = 141;
					break;
				case 254:
					num = 192;
					break;
				case 255:
					num = 223;
					break;
				case 272:
					num = 124;
					break;
				case 8254:
					num = 188;
					break;
				case 65281:
					num = 79;
					break;
				case 65282:
					num = 127;
					break;
				case 65283:
					num = 123;
					break;
				case 65284:
					num = 91;
					break;
				case 65285:
					num = 108;
					break;
				case 65286:
					num = 80;
					break;
				case 65287:
					num = 125;
					break;
				case 65288:
					num = 77;
					break;
				case 65289:
					num = 93;
					break;
				case 65290:
					num = 92;
					break;
				case 65291:
					num = 78;
					break;
				case 65292:
					num = 107;
					break;
				case 65293:
					num = 96;
					break;
				case 65294:
					num = 75;
					break;
				case 65295:
					num = 97;
					break;
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
					num -= 65056;
					break;
				case 65306:
					num = 122;
					break;
				case 65307:
					num = 94;
					break;
				case 65308:
					num = 76;
					break;
				case 65309:
					num = 126;
					break;
				case 65310:
					num = 110;
					break;
				case 65311:
					num = 111;
					break;
				case 65312:
					num = 172;
					break;
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
					num -= 65120;
					break;
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
					num -= 65113;
					break;
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
					num -= 65105;
					break;
				case 65339:
					num = 174;
					break;
				case 65340:
					num = 190;
					break;
				case 65341:
					num = 158;
					break;
				case 65342:
					num = 236;
					break;
				case 65343:
					num = 109;
					break;
				case 65344:
					num = 140;
					break;
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
				case 65353:
					num -= 65216;
					break;
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
				case 65361:
				case 65362:
					num -= 65209;
					break;
				case 65363:
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
					num -= 65201;
					break;
				case 65371:
					num = 142;
					break;
				case 65372:
					num = 187;
					break;
				case 65373:
					num = 156;
					break;
				case 65374:
					num = 204;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 24:
				case 25:
				case 28:
				case 29:
				case 30:
				case 31:
				case 182:
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
public class ENCibm871 : CP20871
{
}
[Serializable]
public class CP21025 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
		'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
		'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
		'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
		'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
		'\u0014', '\u0015', '\u009e', '\u001a', ' ', '\u00a0', 'ђ', 'ѓ', 'ё', 'є',
		'ѕ', 'і', 'ї', 'ј', '[', '.', '<', '(', '+', '!',
		'&', 'љ', 'њ', 'ћ', 'ќ', 'ў', 'џ', 'Ъ', '№', 'Ђ',
		']', '$', '*', ')', ';', '^', '-', '/', 'Ѓ', 'Ё',
		'Є', 'Ѕ', 'І', 'Ї', 'Ј', 'Љ', '|', ',', '%', '_',
		'>', '?', 'Њ', 'Ћ', 'Ќ', '\u00ad', 'Ў', 'Џ', 'ю', 'а',
		'б', '`', ':', '#', '@', '\'', '=', '"', 'ц', 'a',
		'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'д', 'е',
		'ф', 'г', 'х', 'и', 'й', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', 'к', 'л', 'м', 'н', 'о', 'п',
		'я', '~', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'р', 'с', 'т', 'у', 'ж', 'в', 'ь', 'ы', 'з', 'ш',
		'э', 'щ', 'ч', 'ъ', 'Ю', 'А', 'Б', 'Ц', 'Д', 'Е',
		'Ф', 'Г', '{', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
		'H', 'I', 'Х', 'И', 'Й', 'К', 'Л', 'М', '}', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'Н', 'О',
		'П', 'Я', 'Р', 'С', '\\', '§', 'S', 'T', 'U', 'V',
		'W', 'X', 'Y', 'Z', 'Т', 'У', 'Ж', 'В', 'Ь', 'Ы',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'З', 'Ш', 'Э', 'Щ', 'Ч', '\u009f'
	};

	public CP21025()
		: base(21025, ToChars, "IBM EBCDIC (Cyrillic - Serbian, Bulgarian)", "IBM1025", "IBM1025", "IBM1025", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1257)
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
			if (num >= 4)
			{
				switch (num)
				{
				case 4:
					num = 55;
					break;
				case 5:
					num = 45;
					break;
				case 6:
					num = 46;
					break;
				case 7:
					num = 47;
					break;
				case 8:
					num = 22;
					break;
				case 9:
					num = 5;
					break;
				case 10:
					num = 37;
					break;
				case 20:
					num = 60;
					break;
				case 21:
					num = 61;
					break;
				case 22:
					num = 50;
					break;
				case 23:
					num = 38;
					break;
				case 26:
					num = 63;
					break;
				case 27:
					num = 39;
					break;
				case 32:
					num = 64;
					break;
				case 33:
					num = 79;
					break;
				case 34:
					num = 127;
					break;
				case 35:
					num = 123;
					break;
				case 36:
					num = 91;
					break;
				case 37:
					num = 108;
					break;
				case 38:
					num = 80;
					break;
				case 39:
					num = 125;
					break;
				case 40:
					num = 77;
					break;
				case 41:
					num = 93;
					break;
				case 42:
					num = 92;
					break;
				case 43:
					num = 78;
					break;
				case 44:
					num = 107;
					break;
				case 45:
					num = 96;
					break;
				case 46:
					num = 75;
					break;
				case 47:
					num = 97;
					break;
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
					num += 192;
					break;
				case 58:
					num = 122;
					break;
				case 59:
					num = 94;
					break;
				case 60:
					num = 76;
					break;
				case 61:
					num = 126;
					break;
				case 62:
					num = 110;
					break;
				case 63:
					num = 111;
					break;
				case 64:
					num = 124;
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
					num += 128;
					break;
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
					num += 135;
					break;
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
					num += 143;
					break;
				case 91:
					num = 74;
					break;
				case 92:
					num = 224;
					break;
				case 93:
					num = 90;
					break;
				case 94:
					num = 95;
					break;
				case 95:
					num = 109;
					break;
				case 96:
					num = 121;
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
					num += 32;
					break;
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
					num += 39;
					break;
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
					num += 47;
					break;
				case 123:
					num = 192;
					break;
				case 124:
					num = 106;
					break;
				case 125:
					num = 208;
					break;
				case 126:
					num = 161;
					break;
				case 127:
					num = 7;
					break;
				case 128:
				case 129:
				case 130:
				case 131:
				case 132:
					num -= 96;
					break;
				case 133:
					num = 21;
					break;
				case 134:
					num = 6;
					break;
				case 135:
					num = 23;
					break;
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
					num -= 96;
					break;
				case 141:
					num = 9;
					break;
				case 142:
					num = 10;
					break;
				case 143:
					num = 27;
					break;
				case 144:
					num = 48;
					break;
				case 145:
					num = 49;
					break;
				case 146:
					num = 26;
					break;
				case 147:
				case 148:
				case 149:
				case 150:
					num -= 96;
					break;
				case 151:
					num = 8;
					break;
				case 152:
				case 153:
				case 154:
				case 155:
					num -= 96;
					break;
				case 156:
					num = 4;
					break;
				case 157:
					num = 20;
					break;
				case 158:
					num = 62;
					break;
				case 159:
					num = 255;
					break;
				case 160:
					num = 65;
					break;
				case 167:
					num = 225;
					break;
				case 173:
					num = 115;
					break;
				case 1025:
					num = 99;
					break;
				case 1026:
					num = 89;
					break;
				case 1027:
					num = 98;
					break;
				case 1028:
				case 1029:
				case 1030:
				case 1031:
				case 1032:
				case 1033:
					num -= 928;
					break;
				case 1034:
					num = 112;
					break;
				case 1035:
					num = 113;
					break;
				case 1036:
					num = 114;
					break;
				case 1038:
					num = 116;
					break;
				case 1039:
					num = 117;
					break;
				case 1040:
					num = 185;
					break;
				case 1041:
					num = 186;
					break;
				case 1042:
					num = 237;
					break;
				case 1043:
					num = 191;
					break;
				case 1044:
					num = 188;
					break;
				case 1045:
					num = 189;
					break;
				case 1046:
					num = 236;
					break;
				case 1047:
					num = 250;
					break;
				case 1048:
				case 1049:
				case 1050:
				case 1051:
				case 1052:
					num -= 845;
					break;
				case 1053:
					num = 218;
					break;
				case 1054:
					num = 219;
					break;
				case 1055:
					num = 220;
					break;
				case 1056:
					num = 222;
					break;
				case 1057:
					num = 223;
					break;
				case 1058:
					num = 234;
					break;
				case 1059:
					num = 235;
					break;
				case 1060:
					num = 190;
					break;
				case 1061:
					num = 202;
					break;
				case 1062:
					num = 187;
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
					num = 87;
					break;
				case 1067:
					num = 239;
					break;
				case 1068:
					num = 238;
					break;
				case 1069:
					num = 252;
					break;
				case 1070:
					num = 184;
					break;
				case 1071:
					num = 221;
					break;
				case 1072:
					num = 119;
					break;
				case 1073:
					num = 120;
					break;
				case 1074:
					num = 175;
					break;
				case 1075:
					num = 141;
					break;
				case 1076:
					num = 138;
					break;
				case 1077:
					num = 139;
					break;
				case 1078:
					num = 174;
					break;
				case 1079:
					num = 178;
					break;
				case 1080:
					num = 143;
					break;
				case 1081:
					num = 144;
					break;
				case 1082:
				case 1083:
				case 1084:
				case 1085:
				case 1086:
				case 1087:
					num -= 928;
					break;
				case 1088:
				case 1089:
				case 1090:
				case 1091:
					num -= 918;
					break;
				case 1092:
					num = 140;
					break;
				case 1093:
					num = 142;
					break;
				case 1094:
					num = 128;
					break;
				case 1095:
					num = 182;
					break;
				case 1096:
					num = 179;
					break;
				case 1097:
					num = 181;
					break;
				case 1098:
					num = 183;
					break;
				case 1099:
					num = 177;
					break;
				case 1100:
					num = 176;
					break;
				case 1101:
					num = 180;
					break;
				case 1102:
					num = 118;
					break;
				case 1103:
					num = 160;
					break;
				case 1105:
					num = 68;
					break;
				case 1106:
					num = 66;
					break;
				case 1107:
					num = 67;
					break;
				case 1108:
				case 1109:
				case 1110:
				case 1111:
				case 1112:
					num -= 1039;
					break;
				case 1113:
				case 1114:
				case 1115:
				case 1116:
					num -= 1032;
					break;
				case 1118:
					num = 85;
					break;
				case 1119:
					num = 86;
					break;
				case 8470:
					num = 88;
					break;
				case 65281:
					num = 79;
					break;
				case 65282:
					num = 127;
					break;
				case 65283:
					num = 123;
					break;
				case 65284:
					num = 91;
					break;
				case 65285:
					num = 108;
					break;
				case 65286:
					num = 80;
					break;
				case 65287:
					num = 125;
					break;
				case 65288:
					num = 77;
					break;
				case 65289:
					num = 93;
					break;
				case 65290:
					num = 92;
					break;
				case 65291:
					num = 78;
					break;
				case 65292:
					num = 107;
					break;
				case 65293:
					num = 96;
					break;
				case 65294:
					num = 75;
					break;
				case 65295:
					num = 97;
					break;
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
					num -= 65056;
					break;
				case 65306:
					num = 122;
					break;
				case 65307:
					num = 94;
					break;
				case 65308:
					num = 76;
					break;
				case 65309:
					num = 126;
					break;
				case 65310:
					num = 110;
					break;
				case 65311:
					num = 111;
					break;
				case 65312:
					num = 124;
					break;
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
					num -= 65120;
					break;
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
					num -= 65113;
					break;
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
					num -= 65105;
					break;
				case 65339:
					num = 74;
					break;
				case 65340:
					num = 224;
					break;
				case 65341:
					num = 90;
					break;
				case 65342:
					num = 95;
					break;
				case 65343:
					num = 109;
					break;
				case 65344:
					num = 121;
					break;
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
				case 65353:
					num -= 65216;
					break;
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
				case 65361:
				case 65362:
					num -= 65209;
					break;
				case 65363:
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
					num -= 65201;
					break;
				case 65371:
					num = 192;
					break;
				case 65372:
					num = 106;
					break;
				case 65373:
					num = 208;
					break;
				case 65374:
					num = 161;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 24:
				case 25:
				case 28:
				case 29:
				case 30:
				case 31:
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
public class ENCibm1025 : CP21025
{
}
[Serializable]
public class CP37 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
		'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
		'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
		'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
		'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
		'\u0014', '\u0015', '\u009e', '\u001a', ' ', '\u00a0', 'â', 'ä', 'à', 'á',
		'ã', 'å', 'ç', 'ñ', '¢', '.', '<', '(', '+', '|',
		'&', 'é', 'ê', 'ë', 'è', 'í', 'î', 'ï', 'ì', 'ß',
		'!', '$', '*', ')', ';', '¬', '-', '/', 'Â', 'Ä',
		'À', 'Á', 'Ã', 'Å', 'Ç', 'Ñ', '¦', ',', '%', '_',
		'>', '?', 'ø', 'É', 'Ê', 'Ë', 'È', 'Í', 'Î', 'Ï',
		'Ì', '`', ':', '#', '@', '\'', '=', '"', 'Ø', 'a',
		'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', '«', '»',
		'ð', 'ý', 'þ', '±', '°', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', 'ª', 'º', 'æ', '\u00b8', 'Æ', '¤',
		'µ', '~', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'¡', '¿', 'Ð', 'Ý', 'Þ', '®', '^', '£', '¥', '·',
		'©', '§', '¶', '¼', '½', '¾', '[', ']', '\u00af', '\u00a8',
		'\u00b4', '×', '{', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
		'H', 'I', '\u00ad', 'ô', 'ö', 'ò', 'ó', 'õ', '}', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', '¹', 'û',
		'ü', 'ù', 'ú', 'ÿ', '\\', '÷', 'S', 'T', 'U', 'V',
		'W', 'X', 'Y', 'Z', '²', 'Ô', 'Ö', 'Ò', 'Ó', 'Õ',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'³', 'Û', 'Ü', 'Ù', 'Ú', '\u009f'
	};

	public CP37()
		: base(37, ToChars, "IBM EBCDIC (US-Canada)", "IBM037", "IBM037", "IBM037", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
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
			if (num >= 4)
			{
				switch (num)
				{
				case 4:
					num = 55;
					break;
				case 5:
					num = 45;
					break;
				case 6:
					num = 46;
					break;
				case 7:
					num = 47;
					break;
				case 8:
					num = 22;
					break;
				case 9:
					num = 5;
					break;
				case 10:
					num = 37;
					break;
				case 20:
					num = 60;
					break;
				case 21:
					num = 61;
					break;
				case 22:
					num = 50;
					break;
				case 23:
					num = 38;
					break;
				case 26:
					num = 63;
					break;
				case 27:
					num = 39;
					break;
				case 32:
					num = 64;
					break;
				case 33:
					num = 90;
					break;
				case 34:
					num = 127;
					break;
				case 35:
					num = 123;
					break;
				case 36:
					num = 91;
					break;
				case 37:
					num = 108;
					break;
				case 38:
					num = 80;
					break;
				case 39:
					num = 125;
					break;
				case 40:
					num = 77;
					break;
				case 41:
					num = 93;
					break;
				case 42:
					num = 92;
					break;
				case 43:
					num = 78;
					break;
				case 44:
					num = 107;
					break;
				case 45:
					num = 96;
					break;
				case 46:
					num = 75;
					break;
				case 47:
					num = 97;
					break;
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
					num += 192;
					break;
				case 58:
					num = 122;
					break;
				case 59:
					num = 94;
					break;
				case 60:
					num = 76;
					break;
				case 61:
					num = 126;
					break;
				case 62:
					num = 110;
					break;
				case 63:
					num = 111;
					break;
				case 64:
					num = 124;
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
					num += 128;
					break;
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
					num += 135;
					break;
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
					num += 143;
					break;
				case 91:
					num = 186;
					break;
				case 92:
					num = 224;
					break;
				case 93:
					num = 187;
					break;
				case 94:
					num = 176;
					break;
				case 95:
					num = 109;
					break;
				case 96:
					num = 121;
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
					num += 32;
					break;
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
					num += 39;
					break;
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
					num += 47;
					break;
				case 123:
					num = 192;
					break;
				case 124:
					num = 79;
					break;
				case 125:
					num = 208;
					break;
				case 126:
					num = 161;
					break;
				case 127:
					num = 7;
					break;
				case 128:
				case 129:
				case 130:
				case 131:
				case 132:
					num -= 96;
					break;
				case 133:
					num = 21;
					break;
				case 134:
					num = 6;
					break;
				case 135:
					num = 23;
					break;
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
					num -= 96;
					break;
				case 141:
					num = 9;
					break;
				case 142:
					num = 10;
					break;
				case 143:
					num = 27;
					break;
				case 144:
					num = 48;
					break;
				case 145:
					num = 49;
					break;
				case 146:
					num = 26;
					break;
				case 147:
				case 148:
				case 149:
				case 150:
					num -= 96;
					break;
				case 151:
					num = 8;
					break;
				case 152:
				case 153:
				case 154:
				case 155:
					num -= 96;
					break;
				case 156:
					num = 4;
					break;
				case 157:
					num = 20;
					break;
				case 158:
					num = 62;
					break;
				case 159:
					num = 255;
					break;
				case 160:
					num = 65;
					break;
				case 161:
					num = 170;
					break;
				case 162:
					num = 74;
					break;
				case 163:
					num = 177;
					break;
				case 164:
					num = 159;
					break;
				case 165:
					num = 178;
					break;
				case 166:
					num = 106;
					break;
				case 167:
					num = 181;
					break;
				case 168:
					num = 189;
					break;
				case 169:
					num = 180;
					break;
				case 170:
					num = 154;
					break;
				case 171:
					num = 138;
					break;
				case 172:
					num = 95;
					break;
				case 173:
					num = 202;
					break;
				case 174:
					num = 175;
					break;
				case 175:
					num = 188;
					break;
				case 176:
					num = 144;
					break;
				case 177:
					num = 143;
					break;
				case 178:
					num = 234;
					break;
				case 179:
					num = 250;
					break;
				case 180:
					num = 190;
					break;
				case 181:
					num = 160;
					break;
				case 183:
					num = 179;
					break;
				case 184:
					num = 157;
					break;
				case 185:
					num = 218;
					break;
				case 186:
					num = 155;
					break;
				case 187:
					num = 139;
					break;
				case 188:
					num = 183;
					break;
				case 189:
					num = 184;
					break;
				case 190:
					num = 185;
					break;
				case 191:
					num = 171;
					break;
				case 192:
					num = 100;
					break;
				case 193:
					num = 101;
					break;
				case 194:
					num = 98;
					break;
				case 195:
					num = 102;
					break;
				case 196:
					num = 99;
					break;
				case 197:
					num = 103;
					break;
				case 198:
					num = 158;
					break;
				case 199:
					num = 104;
					break;
				case 200:
					num = 116;
					break;
				case 201:
					num = 113;
					break;
				case 202:
					num = 114;
					break;
				case 203:
					num = 115;
					break;
				case 204:
					num = 120;
					break;
				case 205:
					num = 117;
					break;
				case 206:
					num = 118;
					break;
				case 207:
					num = 119;
					break;
				case 208:
					num = 172;
					break;
				case 209:
					num = 105;
					break;
				case 210:
					num = 237;
					break;
				case 211:
					num = 238;
					break;
				case 212:
					num = 235;
					break;
				case 213:
					num = 239;
					break;
				case 214:
					num = 236;
					break;
				case 215:
					num = 191;
					break;
				case 216:
					num = 128;
					break;
				case 217:
					num = 253;
					break;
				case 218:
					num = 254;
					break;
				case 219:
					num = 251;
					break;
				case 220:
					num = 252;
					break;
				case 221:
					num = 173;
					break;
				case 222:
					num = 174;
					break;
				case 223:
					num = 89;
					break;
				case 224:
					num = 68;
					break;
				case 225:
					num = 69;
					break;
				case 226:
					num = 66;
					break;
				case 227:
					num = 70;
					break;
				case 228:
					num = 67;
					break;
				case 229:
					num = 71;
					break;
				case 230:
					num = 156;
					break;
				case 231:
					num = 72;
					break;
				case 232:
					num = 84;
					break;
				case 233:
					num = 81;
					break;
				case 234:
					num = 82;
					break;
				case 235:
					num = 83;
					break;
				case 236:
					num = 88;
					break;
				case 237:
					num = 85;
					break;
				case 238:
					num = 86;
					break;
				case 239:
					num = 87;
					break;
				case 240:
					num = 140;
					break;
				case 241:
					num = 73;
					break;
				case 242:
					num = 205;
					break;
				case 243:
					num = 206;
					break;
				case 244:
					num = 203;
					break;
				case 245:
					num = 207;
					break;
				case 246:
					num = 204;
					break;
				case 247:
					num = 225;
					break;
				case 248:
					num = 112;
					break;
				case 249:
					num = 221;
					break;
				case 250:
					num = 222;
					break;
				case 251:
					num = 219;
					break;
				case 252:
					num = 220;
					break;
				case 253:
					num = 141;
					break;
				case 254:
					num = 142;
					break;
				case 255:
					num = 223;
					break;
				case 272:
					num = 172;
					break;
				case 8254:
					num = 188;
					break;
				case 65281:
					num = 90;
					break;
				case 65282:
					num = 127;
					break;
				case 65283:
					num = 123;
					break;
				case 65284:
					num = 91;
					break;
				case 65285:
					num = 108;
					break;
				case 65286:
					num = 80;
					break;
				case 65287:
					num = 125;
					break;
				case 65288:
					num = 77;
					break;
				case 65289:
					num = 93;
					break;
				case 65290:
					num = 92;
					break;
				case 65291:
					num = 78;
					break;
				case 65292:
					num = 107;
					break;
				case 65293:
					num = 96;
					break;
				case 65294:
					num = 75;
					break;
				case 65295:
					num = 97;
					break;
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
					num -= 65056;
					break;
				case 65306:
					num = 122;
					break;
				case 65307:
					num = 94;
					break;
				case 65308:
					num = 76;
					break;
				case 65309:
					num = 126;
					break;
				case 65310:
					num = 110;
					break;
				case 65311:
					num = 111;
					break;
				case 65312:
					num = 124;
					break;
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
					num -= 65120;
					break;
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
					num -= 65113;
					break;
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
					num -= 65105;
					break;
				case 65339:
					num = 186;
					break;
				case 65340:
					num = 224;
					break;
				case 65341:
					num = 187;
					break;
				case 65342:
					num = 176;
					break;
				case 65343:
					num = 109;
					break;
				case 65344:
					num = 121;
					break;
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
				case 65353:
					num -= 65216;
					break;
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
				case 65361:
				case 65362:
					num -= 65209;
					break;
				case 65363:
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
					num -= 65201;
					break;
				case 65371:
					num = 192;
					break;
				case 65372:
					num = 79;
					break;
				case 65373:
					num = 208;
					break;
				case 65374:
					num = 161;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 24:
				case 25:
				case 28:
				case 29:
				case 30:
				case 31:
				case 182:
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
public class ENCibm037 : CP37
{
}
[Serializable]
public class CP500 : ByteEncoding
{
	private static readonly char[] ToChars = new char[256]
	{
		'\0', '\u0001', '\u0002', '\u0003', '\u009c', '\t', '\u0086', '\u007f', '\u0097', '\u008d',
		'\u008e', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013',
		'\u009d', '\u0085', '\b', '\u0087', '\u0018', '\u0019', '\u0092', '\u008f', '\u001c', '\u001d',
		'\u001e', '\u001f', '\u0080', '\u0081', '\u0082', '\u0083', '\u0084', '\n', '\u0017', '\u001b',
		'\u0088', '\u0089', '\u008a', '\u008b', '\u008c', '\u0005', '\u0006', '\a', '\u0090', '\u0091',
		'\u0016', '\u0093', '\u0094', '\u0095', '\u0096', '\u0004', '\u0098', '\u0099', '\u009a', '\u009b',
		'\u0014', '\u0015', '\u009e', '\u001a', ' ', '\u00a0', 'â', 'ä', 'à', 'á',
		'ã', 'å', 'ç', 'ñ', '[', '.', '<', '(', '+', '!',
		'&', 'é', 'ê', 'ë', 'è', 'í', 'î', 'ï', 'ì', 'ß',
		']', '$', '*', ')', ';', '^', '-', '/', 'Â', 'Ä',
		'À', 'Á', 'Ã', 'Å', 'Ç', 'Ñ', '¦', ',', '%', '_',
		'>', '?', 'ø', 'É', 'Ê', 'Ë', 'È', 'Í', 'Î', 'Ï',
		'Ì', '`', ':', '#', '@', '\'', '=', '"', 'Ø', 'a',
		'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', '«', '»',
		'ð', 'ý', 'þ', '±', '°', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', 'ª', 'º', 'æ', '\u00b8', 'Æ', '¤',
		'µ', '~', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
		'¡', '¿', 'Ð', 'Ý', 'Þ', '®', '¢', '£', '¥', '·',
		'©', '§', '¶', '¼', '½', '¾', '¬', '|', '\u00af', '\u00a8',
		'\u00b4', '×', '{', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
		'H', 'I', '\u00ad', 'ô', 'ö', 'ò', 'ó', 'õ', '}', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', '¹', 'û',
		'ü', 'ù', 'ú', 'ÿ', '\\', '÷', 'S', 'T', 'U', 'V',
		'W', 'X', 'Y', 'Z', '²', 'Ô', 'Ö', 'Ò', 'Ó', 'Õ',
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'³', 'Û', 'Ü', 'Ù', 'Ú', '\u009f'
	};

	public CP500()
		: base(500, ToChars, "IBM EBCDIC (International)", "IBM500", "IBM500", "IBM500", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
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
			if (num >= 4)
			{
				switch (num)
				{
				case 4:
					num = 55;
					break;
				case 5:
					num = 45;
					break;
				case 6:
					num = 46;
					break;
				case 7:
					num = 47;
					break;
				case 8:
					num = 22;
					break;
				case 9:
					num = 5;
					break;
				case 10:
					num = 37;
					break;
				case 20:
					num = 60;
					break;
				case 21:
					num = 61;
					break;
				case 22:
					num = 50;
					break;
				case 23:
					num = 38;
					break;
				case 26:
					num = 63;
					break;
				case 27:
					num = 39;
					break;
				case 32:
					num = 64;
					break;
				case 33:
					num = 79;
					break;
				case 34:
					num = 127;
					break;
				case 35:
					num = 123;
					break;
				case 36:
					num = 91;
					break;
				case 37:
					num = 108;
					break;
				case 38:
					num = 80;
					break;
				case 39:
					num = 125;
					break;
				case 40:
					num = 77;
					break;
				case 41:
					num = 93;
					break;
				case 42:
					num = 92;
					break;
				case 43:
					num = 78;
					break;
				case 44:
					num = 107;
					break;
				case 45:
					num = 96;
					break;
				case 46:
					num = 75;
					break;
				case 47:
					num = 97;
					break;
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
					num += 192;
					break;
				case 58:
					num = 122;
					break;
				case 59:
					num = 94;
					break;
				case 60:
					num = 76;
					break;
				case 61:
					num = 126;
					break;
				case 62:
					num = 110;
					break;
				case 63:
					num = 111;
					break;
				case 64:
					num = 124;
					break;
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
					num += 128;
					break;
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
					num += 135;
					break;
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
					num += 143;
					break;
				case 91:
					num = 74;
					break;
				case 92:
					num = 224;
					break;
				case 93:
					num = 90;
					break;
				case 94:
					num = 95;
					break;
				case 95:
					num = 109;
					break;
				case 96:
					num = 121;
					break;
				case 97:
				case 98:
				case 99:
				case 100:
				case 101:
				case 102:
				case 103:
				case 104:
				case 105:
					num += 32;
					break;
				case 106:
				case 107:
				case 108:
				case 109:
				case 110:
				case 111:
				case 112:
				case 113:
				case 114:
					num += 39;
					break;
				case 115:
				case 116:
				case 117:
				case 118:
				case 119:
				case 120:
				case 121:
				case 122:
					num += 47;
					break;
				case 123:
					num = 192;
					break;
				case 124:
					num = 187;
					break;
				case 125:
					num = 208;
					break;
				case 126:
					num = 161;
					break;
				case 127:
					num = 7;
					break;
				case 128:
				case 129:
				case 130:
				case 131:
				case 132:
					num -= 96;
					break;
				case 133:
					num = 21;
					break;
				case 134:
					num = 6;
					break;
				case 135:
					num = 23;
					break;
				case 136:
				case 137:
				case 138:
				case 139:
				case 140:
					num -= 96;
					break;
				case 141:
					num = 9;
					break;
				case 142:
					num = 10;
					break;
				case 143:
					num = 27;
					break;
				case 144:
					num = 48;
					break;
				case 145:
					num = 49;
					break;
				case 146:
					num = 26;
					break;
				case 147:
				case 148:
				case 149:
				case 150:
					num -= 96;
					break;
				case 151:
					num = 8;
					break;
				case 152:
				case 153:
				case 154:
				case 155:
					num -= 96;
					break;
				case 156:
					num = 4;
					break;
				case 157:
					num = 20;
					break;
				case 158:
					num = 62;
					break;
				case 159:
					num = 255;
					break;
				case 160:
					num = 65;
					break;
				case 161:
					num = 170;
					break;
				case 162:
					num = 176;
					break;
				case 163:
					num = 177;
					break;
				case 164:
					num = 159;
					break;
				case 165:
					num = 178;
					break;
				case 166:
					num = 106;
					break;
				case 167:
					num = 181;
					break;
				case 168:
					num = 189;
					break;
				case 169:
					num = 180;
					break;
				case 170:
					num = 154;
					break;
				case 171:
					num = 138;
					break;
				case 172:
					num = 186;
					break;
				case 173:
					num = 202;
					break;
				case 174:
					num = 175;
					break;
				case 175:
					num = 188;
					break;
				case 176:
					num = 144;
					break;
				case 177:
					num = 143;
					break;
				case 178:
					num = 234;
					break;
				case 179:
					num = 250;
					break;
				case 180:
					num = 190;
					break;
				case 181:
					num = 160;
					break;
				case 183:
					num = 179;
					break;
				case 184:
					num = 157;
					break;
				case 185:
					num = 218;
					break;
				case 186:
					num = 155;
					break;
				case 187:
					num = 139;
					break;
				case 188:
					num = 183;
					break;
				case 189:
					num = 184;
					break;
				case 190:
					num = 185;
					break;
				case 191:
					num = 171;
					break;
				case 192:
					num = 100;
					break;
				case 193:
					num = 101;
					break;
				case 194:
					num = 98;
					break;
				case 195:
					num = 102;
					break;
				case 196:
					num = 99;
					break;
				case 197:
					num = 103;
					break;
				case 198:
					num = 158;
					break;
				case 199:
					num = 104;
					break;
				case 200:
					num = 116;
					break;
				case 201:
					num = 113;
					break;
				case 202:
					num = 114;
					break;
				case 203:
					num = 115;
					break;
				case 204:
					num = 120;
					break;
				case 205:
					num = 117;
					break;
				case 206:
					num = 118;
					break;
				case 207:
					num = 119;
					break;
				case 208:
					num = 172;
					break;
				case 209:
					num = 105;
					break;
				case 210:
					num = 237;
					break;
				case 211:
					num = 238;
					break;
				case 212:
					num = 235;
					break;
				case 213:
					num = 239;
					break;
				case 214:
					num = 236;
					break;
				case 215:
					num = 191;
					break;
				case 216:
					num = 128;
					break;
				case 217:
					num = 253;
					break;
				case 218:
					num = 254;
					break;
				case 219:
					num = 251;
					break;
				case 220:
					num = 252;
					break;
				case 221:
					num = 173;
					break;
				case 222:
					num = 174;
					break;
				case 223:
					num = 89;
					break;
				case 224:
					num = 68;
					break;
				case 225:
					num = 69;
					break;
				case 226:
					num = 66;
					break;
				case 227:
					num = 70;
					break;
				case 228:
					num = 67;
					break;
				case 229:
					num = 71;
					break;
				case 230:
					num = 156;
					break;
				case 231:
					num = 72;
					break;
				case 232:
					num = 84;
					break;
				case 233:
					num = 81;
					break;
				case 234:
					num = 82;
					break;
				case 235:
					num = 83;
					break;
				case 236:
					num = 88;
					break;
				case 237:
					num = 85;
					break;
				case 238:
					num = 86;
					break;
				case 239:
					num = 87;
					break;
				case 240:
					num = 140;
					break;
				case 241:
					num = 73;
					break;
				case 242:
					num = 205;
					break;
				case 243:
					num = 206;
					break;
				case 244:
					num = 203;
					break;
				case 245:
					num = 207;
					break;
				case 246:
					num = 204;
					break;
				case 247:
					num = 225;
					break;
				case 248:
					num = 112;
					break;
				case 249:
					num = 221;
					break;
				case 250:
					num = 222;
					break;
				case 251:
					num = 219;
					break;
				case 252:
					num = 220;
					break;
				case 253:
					num = 141;
					break;
				case 254:
					num = 142;
					break;
				case 255:
					num = 223;
					break;
				case 272:
					num = 172;
					break;
				case 8254:
					num = 188;
					break;
				case 65281:
					num = 79;
					break;
				case 65282:
					num = 127;
					break;
				case 65283:
					num = 123;
					break;
				case 65284:
					num = 91;
					break;
				case 65285:
					num = 108;
					break;
				case 65286:
					num = 80;
					break;
				case 65287:
					num = 125;
					break;
				case 65288:
					num = 77;
					break;
				case 65289:
					num = 93;
					break;
				case 65290:
					num = 92;
					break;
				case 65291:
					num = 78;
					break;
				case 65292:
					num = 107;
					break;
				case 65293:
					num = 96;
					break;
				case 65294:
					num = 75;
					break;
				case 65295:
					num = 97;
					break;
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
					num -= 65056;
					break;
				case 65306:
					num = 122;
					break;
				case 65307:
					num = 94;
					break;
				case 65308:
					num = 76;
					break;
				case 65309:
					num = 126;
					break;
				case 65310:
					num = 110;
					break;
				case 65311:
					num = 111;
					break;
				case 65312:
					num = 124;
					break;
				case 65313:
				case 65314:
				case 65315:
				case 65316:
				case 65317:
				case 65318:
				case 65319:
				case 65320:
				case 65321:
					num -= 65120;
					break;
				case 65322:
				case 65323:
				case 65324:
				case 65325:
				case 65326:
				case 65327:
				case 65328:
				case 65329:
				case 65330:
					num -= 65113;
					break;
				case 65331:
				case 65332:
				case 65333:
				case 65334:
				case 65335:
				case 65336:
				case 65337:
				case 65338:
					num -= 65105;
					break;
				case 65339:
					num = 74;
					break;
				case 65340:
					num = 224;
					break;
				case 65341:
					num = 90;
					break;
				case 65342:
					num = 95;
					break;
				case 65343:
					num = 109;
					break;
				case 65344:
					num = 121;
					break;
				case 65345:
				case 65346:
				case 65347:
				case 65348:
				case 65349:
				case 65350:
				case 65351:
				case 65352:
				case 65353:
					num -= 65216;
					break;
				case 65354:
				case 65355:
				case 65356:
				case 65357:
				case 65358:
				case 65359:
				case 65360:
				case 65361:
				case 65362:
					num -= 65209;
					break;
				case 65363:
				case 65364:
				case 65365:
				case 65366:
				case 65367:
				case 65368:
				case 65369:
				case 65370:
					num -= 65201;
					break;
				case 65371:
					num = 192;
					break;
				case 65372:
					num = 187;
					break;
				case 65373:
					num = 208;
					break;
				case 65374:
					num = 161;
					break;
				default:
					HandleFallback(ref buffer, chars, ref charIndex, ref charCount, bytes, ref byteIndex, ref byteCount);
					break;
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 24:
				case 25:
				case 28:
				case 29:
				case 30:
				case 31:
				case 182:
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
public class ENCibm500 : CP500
{
}
[Serializable]
public class CP708 : ByteEncoding
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
		'\u00a0', '?', '?', '?', '¤', '?', '?', '?', '?', '?',
		'?', '?', '،', '\u00ad', '?', '?', '?', '?', '?', '?',
		'?', '?', '?', '?', '?', '?', '?', '؛', '?', '?',
		'?', '؟', '?', 'ء', 'آ', 'أ', 'ؤ', 'إ', 'ئ', 'ا',
		'ب', 'ة', 'ت', 'ث', 'ج', 'ح', 'خ', 'د', 'ذ', 'ر',
		'ز', 'س', 'ش', 'ص', 'ض', 'ط', 'ظ', 'ع', 'غ', '?',
		'?', '?', '?', '?', 'ـ', 'ف', 'ق', 'ك', 'ل', 'م',
		'ن', 'ه', 'و', 'ى', 'ي', '\u064b', '\u064c', '\u064d', '\u064e', '\u064f',
		'\u0650', '\u0651', '\u0652', '?', '?', '?', '?', '?', '?', '?',
		'?', '?', '?', '?', '?', '?'
	};

	public CP708()
		: base(708, ToChars, "Arabic (ASMO 708)", "iso-8859-6", "asmo-708", "asmo-708", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1256)
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
				case 1642:
					num = 37;
					break;
				case 1643:
					num = 44;
					break;
				case 1644:
					num = 46;
					break;
				case 1645:
					num = 42;
					break;
				case 65136:
					num = 235;
					break;
				case 65137:
					num = 235;
					break;
				case 65138:
					num = 236;
					break;
				case 65140:
					num = 237;
					break;
				case 65142:
					num = 238;
					break;
				case 65143:
					num = 238;
					break;
				case 65144:
					num = 239;
					break;
				case 65145:
					num = 239;
					break;
				case 65146:
					num = 240;
					break;
				case 65147:
					num = 240;
					break;
				case 65148:
					num = 241;
					break;
				case 65149:
					num = 241;
					break;
				case 65150:
					num = 242;
					break;
				case 65151:
					num = 242;
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
					num = 215;
					break;
				case 65218:
					num = 215;
					break;
				case 65219:
					num = 215;
					break;
				case 65220:
					num = 215;
					break;
				case 65221:
					num = 216;
					break;
				case 65222:
					num = 216;
					break;
				case 65223:
					num = 216;
					break;
				case 65224:
					num = 216;
					break;
				case 65225:
					num = 217;
					break;
				case 65226:
					num = 217;
					break;
				case 65227:
					num = 217;
					break;
				case 65228:
					num = 217;
					break;
				case 65229:
					num = 218;
					break;
				case 65230:
					num = 218;
					break;
				case 65231:
					num = 218;
					break;
				case 65232:
					num = 218;
					break;
				case 65233:
					num = 225;
					break;
				case 65234:
					num = 225;
					break;
				case 65235:
					num = 225;
					break;
				case 65236:
					num = 225;
					break;
				case 65237:
					num = 226;
					break;
				case 65238:
					num = 226;
					break;
				case 65239:
					num = 226;
					break;
				case 65240:
					num = 226;
					break;
				case 65241:
					num = 227;
					break;
				case 65242:
					num = 227;
					break;
				case 65243:
					num = 227;
					break;
				case 65244:
					num = 227;
					break;
				case 65245:
					num = 228;
					break;
				case 65246:
					num = 228;
					break;
				case 65247:
					num = 228;
					break;
				case 65248:
					num = 228;
					break;
				case 65249:
					num = 229;
					break;
				case 65250:
					num = 229;
					break;
				case 65251:
					num = 229;
					break;
				case 65252:
					num = 229;
					break;
				case 65253:
					num = 230;
					break;
				case 65254:
					num = 230;
					break;
				case 65255:
					num = 230;
					break;
				case 65256:
					num = 230;
					break;
				case 65257:
					num = 231;
					break;
				case 65258:
					num = 231;
					break;
				case 65259:
					num = 231;
					break;
				case 65260:
					num = 231;
					break;
				case 65261:
					num = 232;
					break;
				case 65262:
					num = 232;
					break;
				case 65263:
					num = 233;
					break;
				case 65264:
					num = 233;
					break;
				case 65265:
					num = 234;
					break;
				case 65266:
					num = 234;
					break;
				case 65267:
					num = 234;
					break;
				case 65268:
					num = 234;
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
public class ENCasmo_708 : CP708
{
}
[Serializable]
public class CP852 : ByteEncoding
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
		'é', 'â', 'ä', 'ů', 'ć', 'ç', 'ł', 'ë', 'Ő', 'ő',
		'î', 'Ź', 'Ä', 'Ć', 'É', 'Ĺ', 'ĺ', 'ô', 'ö', 'Ľ',
		'ľ', 'Ś', 'ś', 'Ö', 'Ü', 'Ť', 'ť', 'Ł', '×', 'č',
		'á', 'í', 'ó', 'ú', 'Ą', 'ą', 'Ž', 'ž', 'Ę', 'ę',
		'?', 'ź', 'Č', 'ş', '«', '»', '░', '▒', '▓', '│',
		'┤', 'Á', 'Â', 'Ě', 'Ş', '╣', '║', '╗', '╝', 'Ż',
		'ż', '┐', '└', '┴', '┬', '├', '─', '┼', 'Ă', 'ă',
		'╚', '╔', '╩', '╦', '╠', '═', '╬', '¤', 'đ', 'Đ',
		'Ď', 'Ë', 'ď', 'Ň', 'Í', 'Î', 'ě', '┘', '┌', '█',
		'▄', 'Ţ', 'Ů', '▀', 'Ó', 'ß', 'Ô', 'Ń', 'ń', 'ň',
		'Š', 'š', 'Ŕ', 'Ú', 'ŕ', 'Ű', 'ý', 'Ý', 'ţ', '\u00b4',
		'\u00ad', '\u02dd', '\u02db', 'ˇ', '\u02d8', '§', '÷', '\u00b8', '°', '\u00a8',
		'\u02d9', 'ű', 'Ř', 'ř', '■', '\u00a0'
	};

	public CP852()
		: base(852, ToChars, "Central European (DOS)", "ibm852", "ibm852", "ibm852", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1250)
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
				case 164:
					num = 207;
					break;
				case 167:
					num = 245;
					break;
				case 168:
					num = 249;
					break;
				case 171:
					num = 174;
					break;
				case 173:
					num = 240;
					break;
				case 176:
					num = 248;
					break;
				case 180:
					num = 239;
					break;
				case 182:
					num = 20;
					break;
				case 184:
					num = 247;
					break;
				case 187:
					num = 175;
					break;
				case 193:
					num = 181;
					break;
				case 194:
					num = 182;
					break;
				case 196:
					num = 142;
					break;
				case 199:
					num = 128;
					break;
				case 201:
					num = 144;
					break;
				case 203:
					num = 211;
					break;
				case 205:
					num = 214;
					break;
				case 206:
					num = 215;
					break;
				case 208:
					num = 209;
					break;
				case 211:
					num = 224;
					break;
				case 212:
					num = 226;
					break;
				case 214:
					num = 153;
					break;
				case 215:
					num = 158;
					break;
				case 218:
					num = 233;
					break;
				case 220:
					num = 154;
					break;
				case 221:
					num = 237;
					break;
				case 223:
					num = 225;
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
				case 231:
					num = 135;
					break;
				case 233:
					num = 130;
					break;
				case 235:
					num = 137;
					break;
				case 237:
					num = 161;
					break;
				case 238:
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
				case 250:
					num = 163;
					break;
				case 252:
					num = 129;
					break;
				case 253:
					num = 236;
					break;
				case 258:
					num = 198;
					break;
				case 259:
					num = 199;
					break;
				case 260:
					num = 164;
					break;
				case 261:
					num = 165;
					break;
				case 262:
					num = 143;
					break;
				case 263:
					num = 134;
					break;
				case 268:
					num = 172;
					break;
				case 269:
					num = 159;
					break;
				case 270:
					num = 210;
					break;
				case 271:
					num = 212;
					break;
				case 272:
					num = 209;
					break;
				case 273:
					num = 208;
					break;
				case 280:
					num = 168;
					break;
				case 281:
					num = 169;
					break;
				case 282:
					num = 183;
					break;
				case 283:
					num = 216;
					break;
				case 313:
					num = 145;
					break;
				case 314:
					num = 146;
					break;
				case 317:
					num = 149;
					break;
				case 318:
					num = 150;
					break;
				case 321:
					num = 157;
					break;
				case 322:
					num = 136;
					break;
				case 323:
					num = 227;
					break;
				case 324:
					num = 228;
					break;
				case 327:
					num = 213;
					break;
				case 328:
					num = 229;
					break;
				case 336:
					num = 138;
					break;
				case 337:
					num = 139;
					break;
				case 340:
					num = 232;
					break;
				case 341:
					num = 234;
					break;
				case 344:
					num = 252;
					break;
				case 345:
					num = 253;
					break;
				case 346:
					num = 151;
					break;
				case 347:
					num = 152;
					break;
				case 350:
					num = 184;
					break;
				case 351:
					num = 173;
					break;
				case 352:
					num = 230;
					break;
				case 353:
					num = 231;
					break;
				case 354:
					num = 221;
					break;
				case 355:
					num = 238;
					break;
				case 356:
					num = 155;
					break;
				case 357:
					num = 156;
					break;
				case 366:
					num = 222;
					break;
				case 367:
					num = 133;
					break;
				case 368:
					num = 235;
					break;
				case 369:
					num = 251;
					break;
				case 377:
					num = 141;
					break;
				case 378:
					num = 171;
					break;
				case 379:
					num = 189;
					break;
				case 380:
					num = 190;
					break;
				case 381:
					num = 166;
					break;
				case 382:
					num = 167;
					break;
				case 711:
					num = 243;
					break;
				case 728:
					num = 244;
					break;
				case 729:
					num = 250;
					break;
				case 731:
					num = 242;
					break;
				case 733:
					num = 241;
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
public class ENCibm852 : CP852
{
}
[Serializable]
public class CP855 : ByteEncoding
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
		'x', 'y', 'z', '{', '|', '}', '~', '\u001a', 'ђ', 'Ђ',
		'ѓ', 'Ѓ', 'ё', 'Ё', 'є', 'Є', 'ѕ', 'Ѕ', 'і', 'І',
		'ї', 'Ї', 'ј', 'Ј', 'љ', 'Љ', 'њ', 'Њ', 'ћ', 'Ћ',
		'ќ', 'Ќ', 'ў', 'Ў', 'џ', 'Џ', 'ю', 'Ю', 'ъ', 'Ъ',
		'а', 'А', 'б', 'Б', 'ц', 'Ц', 'д', 'Д', 'е', 'Е',
		'ф', 'Ф', 'г', 'Г', '«', '»', '░', '▒', '▓', '│',
		'┤', 'х', 'Х', 'и', 'И', '╣', '║', '╗', '╝', 'й',
		'Й', '┐', '└', '┴', '┬', '├', '─', '┼', 'к', 'К',
		'╚', '╔', '╩', '╦', '╠', '═', '╬', '¤', 'л', 'Л',
		'м', 'М', 'н', 'Н', 'о', 'О', 'п', '┘', '┌', '█',
		'▄', 'П', 'я', '▀', 'Я', 'р', 'Р', 'с', 'С', 'т',
		'Т', 'у', 'У', 'ж', 'Ж', 'в', 'В', 'ь', 'Ь', '№',
		'\u00ad', 'ы', 'Ы', 'з', 'З', 'ш', 'Ш', 'э', 'Э', 'щ',
		'Щ', 'ч', 'Ч', '§', '■', '\u00a0'
	};

	public CP855()
		: base(855, ToChars, "Cyrillic (DOS)", "ibm855", "ibm855", "ibm855", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1251)
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
				case 164:
					num = 207;
					break;
				case 167:
					num = 253;
					break;
				case 171:
					num = 174;
					break;
				case 173:
					num = 240;
					break;
				case 182:
					num = 20;
					break;
				case 187:
					num = 175;
					break;
				case 1025:
					num = 133;
					break;
				case 1026:
					num = 129;
					break;
				case 1027:
					num = 131;
					break;
				case 1028:
					num = 135;
					break;
				case 1029:
					num = 137;
					break;
				case 1030:
					num = 139;
					break;
				case 1031:
					num = 141;
					break;
				case 1032:
					num = 143;
					break;
				case 1033:
					num = 145;
					break;
				case 1034:
					num = 147;
					break;
				case 1035:
					num = 149;
					break;
				case 1036:
					num = 151;
					break;
				case 1038:
					num = 153;
					break;
				case 1039:
					num = 155;
					break;
				case 1040:
					num = 161;
					break;
				case 1041:
					num = 163;
					break;
				case 1042:
					num = 236;
					break;
				case 1043:
					num = 173;
					break;
				case 1044:
					num = 167;
					break;
				case 1045:
					num = 169;
					break;
				case 1046:
					num = 234;
					break;
				case 1047:
					num = 244;
					break;
				case 1048:
					num = 184;
					break;
				case 1049:
					num = 190;
					break;
				case 1050:
					num = 199;
					break;
				case 1051:
					num = 209;
					break;
				case 1052:
					num = 211;
					break;
				case 1053:
					num = 213;
					break;
				case 1054:
					num = 215;
					break;
				case 1055:
					num = 221;
					break;
				case 1056:
					num = 226;
					break;
				case 1057:
					num = 228;
					break;
				case 1058:
					num = 230;
					break;
				case 1059:
					num = 232;
					break;
				case 1060:
					num = 171;
					break;
				case 1061:
					num = 182;
					break;
				case 1062:
					num = 165;
					break;
				case 1063:
					num = 252;
					break;
				case 1064:
					num = 246;
					break;
				case 1065:
					num = 250;
					break;
				case 1066:
					num = 159;
					break;
				case 1067:
					num = 242;
					break;
				case 1068:
					num = 238;
					break;
				case 1069:
					num = 248;
					break;
				case 1070:
					num = 157;
					break;
				case 1071:
					num = 224;
					break;
				case 1072:
					num = 160;
					break;
				case 1073:
					num = 162;
					break;
				case 1074:
					num = 235;
					break;
				case 1075:
					num = 172;
					break;
				case 1076:
					num = 166;
					break;
				case 1077:
					num = 168;
					break;
				case 1078:
					num = 233;
					break;
				case 1079:
					num = 243;
					break;
				case 1080:
					num = 183;
					break;
				case 1081:
					num = 189;
					break;
				case 1082:
					num = 198;
					break;
				case 1083:
					num = 208;
					break;
				case 1084:
					num = 210;
					break;
				case 1085:
					num = 212;
					break;
				case 1086:
					num = 214;
					break;
				case 1087:
					num = 216;
					break;
				case 1088:
					num = 225;
					break;
				case 1089:
					num = 227;
					break;
				case 1090:
					num = 229;
					break;
				case 1091:
					num = 231;
					break;
				case 1092:
					num = 170;
					break;
				case 1093:
					num = 181;
					break;
				case 1094:
					num = 164;
					break;
				case 1095:
					num = 251;
					break;
				case 1096:
					num = 245;
					break;
				case 1097:
					num = 249;
					break;
				case 1098:
					num = 158;
					break;
				case 1099:
					num = 241;
					break;
				case 1100:
					num = 237;
					break;
				case 1101:
					num = 247;
					break;
				case 1102:
					num = 156;
					break;
				case 1103:
					num = 222;
					break;
				case 1105:
					num = 132;
					break;
				case 1106:
					num = 128;
					break;
				case 1107:
					num = 130;
					break;
				case 1108:
					num = 134;
					break;
				case 1109:
					num = 136;
					break;
				case 1110:
					num = 138;
					break;
				case 1111:
					num = 140;
					break;
				case 1112:
					num = 142;
					break;
				case 1113:
					num = 144;
					break;
				case 1114:
					num = 146;
					break;
				case 1115:
					num = 148;
					break;
				case 1116:
					num = 150;
					break;
				case 1118:
					num = 152;
					break;
				case 1119:
					num = 154;
					break;
				case 8226:
					num = 7;
					break;
				case 8252:
					num = 19;
					break;
				case 8470:
					num = 239;
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
public class ENCibm855 : CP855
{
}
[Serializable]
public class CP857 : ByteEncoding
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
		'î', 'ı', 'Ä', 'Å', 'É', 'æ', 'Æ', 'ô', 'ö', 'ò',
		'û', 'ù', 'İ', 'Ö', 'Ü', 'ø', '£', 'Ø', 'Ş', 'ş',
		'á', 'í', 'ó', 'ú', 'ñ', 'Ñ', 'Ğ', 'ğ', '¿', '®',
		'¬', '½', '¼', '¡', '«', '»', '░', '▒', '▓', '│',
		'┤', 'Á', 'Â', 'À', '©', '╣', '║', '╗', '╝', '¢',
		'¥', '┐', '└', '┴', '┬', '├', '─', '┼', 'ã', 'Ã',
		'╚', '╔', '╩', '╦', '╠', '═', '╬', '¤', 'º', 'ª',
		'Ê', 'Ë', 'È', '?', 'Í', 'Î', 'Ï', '┘', '┌', '█',
		'▄', '¦', 'Ì', '▀', 'Ó', 'ß', 'Ô', 'Ò', 'õ', 'Õ',
		'µ', '?', '×', 'Ú', 'Û', 'Ù', 'ì', 'ÿ', '\u00af', '\u00b4',
		'\u00ad', '±', '?', '¾', '¶', '§', '÷', '\u00b8', '°', '\u00a8',
		'·', '¹', '³', '²', '■', '\u00a0'
	};

	public CP857()
		: base(857, ToChars, "Turkish (DOS)", "ibm857", "ibm857", "ibm857", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1254)
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
					num = 209;
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
					num = 208;
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
					num = 232;
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
				case 255:
					num = 237;
					break;
				case 286:
					num = 166;
					break;
				case 287:
					num = 167;
					break;
				case 304:
					num = 152;
					break;
				case 305:
					num = 141;
					break;
				case 350:
					num = 158;
					break;
				case 351:
					num = 159;
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
				case 236:
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
public class ENCibm857 : CP857
{
}
[Serializable]
public class CP858 : ByteEncoding
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
		'Ê', 'Ë', 'È', '€', 'Í', 'Î', 'Ï', '┘', '┌', '█',
		'▄', '¦', 'Ì', '▀', 'Ó', 'ß', 'Ô', 'Ò', 'õ', 'Õ',
		'µ', 'þ', 'Þ', 'Ú', 'Û', 'Ù', 'ý', 'Ý', '\u00af', '\u00b4',
		'\u00ad', '±', '‗', '¾', '¶', '§', '÷', '\u00b8', '°', '\u00a8',
		'·', '¹', '³', '²', '■', '\u00a0'
	};

	public CP858()
		: base(858, ToChars, "Western European (DOS with Euro)", "IBM00858", "IBM00858", "IBM00858", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1252)
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
				case 8364:
					num = 213;
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
public class ENCibm00858 : CP858
{
}
[Serializable]
public class CP862 : ByteEncoding
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
		'x', 'y', 'z', '{', '|', '}', '~', '\u001a', 'א', 'ב',
		'ג', 'ד', 'ה', 'ו', 'ז', 'ח', 'ט', 'י', 'ך', 'כ',
		'ל', 'ם', 'מ', 'ן', 'נ', 'ס', 'ע', 'ף', 'פ', 'ץ',
		'צ', 'ק', 'ר', 'ש', 'ת', '¢', '£', '¥', '₧', 'ƒ',
		'á', 'í', 'ó', 'ú', 'ñ', 'Ñ', 'ª', 'º', '¿', '⌐',
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

	public CP862()
		: base(862, ToChars, "Hebrew (DOS)", "ibm862", "ibm861", "ibm862", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1255)
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
				case 165:
					num = 157;
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
				case 209:
					num = 165;
					break;
				case 223:
					num = 225;
					break;
				case 225:
					num = 160;
					break;
				case 237:
					num = 161;
					break;
				case 241:
					num = 164;
					break;
				case 243:
					num = 162;
					break;
				case 247:
					num = 246;
					break;
				case 250:
					num = 163;
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
					num -= 1360;
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
public class ENCibm862 : CP862
{
}
[Serializable]
public class CP864 : ByteEncoding
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
		'x', 'y', 'z', '{', '|', '}', '~', '\u001a', '°', '·',
		'∙', '√', '▒', '─', '│', '┼', '┤', '┬', '├', '┴',
		'┐', '┌', '└', '┘', 'β', '∞', 'φ', '±', '½', '¼',
		'≈', '«', '»', 'ﻷ', 'ﻸ', '?', '?', 'ﻻ', 'ﻼ', '\u200b',
		'\u00a0', '\u00ad', 'ﺂ', '£', '¤', 'ﺄ', '?', '?', 'ﺎ', 'ﺏ',
		'ﺕ', 'ﺙ', '،', 'ﺝ', 'ﺡ', 'ﺥ', '٠', '١', '٢', '٣',
		'٤', '٥', '٦', '٧', '٨', '٩', 'ﻑ', '؛', 'ﺱ', 'ﺵ',
		'ﺹ', '؟', '¢', 'ﺀ', 'ﺁ', 'ﺃ', 'ﺅ', 'ﻊ', 'ﺋ', 'ﺍ',
		'ﺑ', 'ﺓ', 'ﺗ', 'ﺛ', 'ﺟ', 'ﺣ', 'ﺧ', 'ﺩ', 'ﺫ', 'ﺭ',
		'ﺯ', 'ﺳ', 'ﺷ', 'ﺻ', 'ﺿ', 'ﻃ', 'ﻇ', 'ﻋ', 'ﻏ', '¦',
		'¬', '÷', '×', 'ﻉ', 'ـ', 'ﻓ', 'ﻗ', 'ﻛ', 'ﻟ', 'ﻣ',
		'ﻧ', 'ﻫ', 'ﻭ', 'ﻯ', 'ﻳ', 'ﺽ', 'ﻌ', 'ﻎ', 'ﻍ', 'ﻡ',
		'ﹽ', 'ﹼ', 'ﻥ', 'ﻩ', 'ﻬ', 'ﻰ', 'ﻲ', 'ﻐ', 'ﻕ', 'ﻵ',
		'ﻶ', 'ﻝ', 'ﻙ', 'ﻱ', '■', '?'
	};

	public CP864()
		: base(864, ToChars, "Arabic (DOS)", "ibm864", "ibm864", "ibm864", isBrowserDisplay: false, isBrowserSave: false, isMailNewsDisplay: false, isMailNewsSave: false, 1256)
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
				int num2 = num;
				switch (num2)
				{
				default:
					switch (num2)
					{
					case 946:
						num = 144;
						break;
					case 966:
						num = 146;
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
						num -= 1376;
						break;
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
						num -= 1376;
						break;
					case 1610:
						num = 253;
						break;
					case 1617:
						num = 241;
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
						num -= 1456;
						break;
					case 1642:
						num = 37;
						break;
					case 1643:
						num = 44;
						break;
					case 1644:
						num = 46;
						break;
					case 1645:
						num = 42;
						break;
					case 8203:
						num = 159;
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
					case 8729:
						num = 130;
						break;
					case 8730:
						num = 131;
						break;
					case 8734:
						num = 145;
						break;
					case 8735:
						num = 28;
						break;
					case 8776:
						num = 150;
						break;
					case 8962:
						num = 127;
						break;
					case 9472:
						num = 133;
						break;
					case 9474:
						num = 134;
						break;
					case 9484:
						num = 141;
						break;
					case 9488:
						num = 140;
						break;
					case 9492:
						num = 142;
						break;
					case 9496:
						num = 143;
						break;
					case 9500:
						num = 138;
						break;
					case 9508:
						num = 136;
						break;
					case 9516:
						num = 137;
						break;
					case 9524:
						num = 139;
						break;
					case 9532:
						num = 135;
						break;
					case 9552:
						num = 5;
						break;
					case 9553:
						num = 6;
						break;
					case 9556:
						num = 13;
						break;
					case 9559:
						num = 12;
						break;
					case 9562:
						num = 14;
						break;
					case 9565:
						num = 15;
						break;
					case 9568:
						num = 10;
						break;
					case 9571:
						num = 8;
						break;
					case 9574:
						num = 9;
						break;
					case 9577:
						num = 11;
						break;
					case 9580:
						num = 7;
						break;
					case 9618:
						num = 132;
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
					case 9786:
						num = 1;
						break;
					case 9788:
						num = 4;
						break;
					case 9834:
						num = 2;
						break;
					case 9836:
						num = 3;
						break;
					case 65148:
						num = 241;
						break;
					case 65149:
						num = 240;
						break;
					case 65152:
						num = 193;
						break;
					case 65153:
						num = 194;
						break;
					case 65154:
						num = 162;
						break;
					case 65155:
						num = 195;
						break;
					case 65156:
						num = 165;
						break;
					case 65157:
						num = 196;
						break;
					case 65158:
						num = 196;
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
						num = 168;
						break;
					case 65167:
						num = 169;
						break;
					case 65168:
						num = 169;
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
						num = 170;
						break;
					case 65174:
						num = 170;
						break;
					case 65175:
						num = 202;
						break;
					case 65176:
						num = 202;
						break;
					case 65177:
						num = 171;
						break;
					case 65178:
						num = 171;
						break;
					case 65179:
						num = 203;
						break;
					case 65180:
						num = 203;
						break;
					case 65181:
						num = 173;
						break;
					case 65182:
						num = 173;
						break;
					case 65183:
						num = 204;
						break;
					case 65184:
						num = 204;
						break;
					case 65185:
						num = 174;
						break;
					case 65186:
						num = 174;
						break;
					case 65187:
						num = 205;
						break;
					case 65188:
						num = 205;
						break;
					case 65189:
						num = 175;
						break;
					case 65190:
						num = 175;
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
						num = 188;
						break;
					case 65202:
						num = 188;
						break;
					case 65203:
						num = 211;
						break;
					case 65204:
						num = 211;
						break;
					case 65205:
						num = 189;
						break;
					case 65206:
						num = 189;
						break;
					case 65207:
						num = 212;
						break;
					case 65208:
						num = 212;
						break;
					case 65209:
						num = 190;
						break;
					case 65210:
						num = 190;
						break;
					case 65211:
						num = 213;
						break;
					case 65212:
						num = 213;
						break;
					case 65213:
						num = 235;
						break;
					case 65214:
						num = 235;
						break;
					case 65215:
						num = 214;
						break;
					case 65216:
						num = 214;
						break;
					case 65217:
						num = 215;
						break;
					case 65218:
						num = 215;
						break;
					case 65219:
						num = 215;
						break;
					case 65220:
						num = 215;
						break;
					case 65221:
						num = 216;
						break;
					case 65222:
						num = 216;
						break;
					case 65223:
						num = 216;
						break;
					case 65224:
						num = 216;
						break;
					case 65225:
						num = 223;
						break;
					case 65226:
						num = 197;
						break;
					case 65227:
						num = 217;
						break;
					case 65228:
						num = 236;
						break;
					case 65229:
						num = 238;
						break;
					case 65230:
						num = 237;
						break;
					case 65231:
						num = 218;
						break;
					case 65232:
						num = 247;
						break;
					case 65233:
						num = 186;
						break;
					case 65234:
						num = 186;
						break;
					case 65235:
						num = 225;
						break;
					case 65236:
						num = 225;
						break;
					case 65237:
						num = 248;
						break;
					case 65238:
						num = 248;
						break;
					case 65239:
						num = 226;
						break;
					case 65240:
						num = 226;
						break;
					case 65241:
						num = 252;
						break;
					case 65242:
						num = 252;
						break;
					case 65243:
						num = 227;
						break;
					case 65244:
						num = 227;
						break;
					case 65245:
						num = 251;
						break;
					case 65246:
						num = 251;
						break;
					case 65247:
						num = 228;
						break;
					case 65248:
						num = 228;
						break;
					case 65249:
						num = 239;
						break;
					case 65250:
						num = 239;
						break;
					case 65251:
						num = 229;
						break;
					case 65252:
						num = 229;
						break;
					case 65253:
						num = 242;
						break;
					case 65254:
						num = 242;
						break;
					case 65255:
						num = 230;
						break;
					case 65256:
						num = 230;
						break;
					case 65257:
						num = 243;
						break;
					case 65258:
						num = 243;
						break;
					case 65259:
						num = 231;
						break;
					case 65260:
						num = 244;
						break;
					case 65261:
						num = 232;
						break;
					case 65262:
						num = 232;
						break;
					case 65263:
						num = 233;
						break;
					case 65264:
						num = 245;
						break;
					case 65265:
						num = 253;
						break;
					case 65266:
						num = 246;
						break;
					case 65267:
						num = 234;
						break;
					case 65268:
						num = 234;
						break;
					case 65269:
						num = 249;
						break;
					case 65270:
						num = 250;
						break;
					case 65271:
						num = 153;
						break;
					case 65272:
						num = 154;
						break;
					case 65275:
						num = 157;
						break;
					case 65276:
						num = 158;
						break;
					case 65512:
						num = 134;
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
					break;
				case 26:
					num = 127;
					break;
				case 28:
					num = 26;
					break;
				case 127:
					num = 28;
					break;
				case 162:
					num = 192;
					break;
				case 166:
					num = 219;
					break;
				case 167:
					num = 21;
					break;
				case 171:
					num = 151;
					break;
				case 172:
					num = 220;
					break;
				case 173:
					num = 161;
					break;
				case 176:
					num = 128;
					break;
				case 177:
					num = 147;
					break;
				case 182:
					num = 20;
					break;
				case 183:
					num = 129;
					break;
				case 187:
					num = 152;
					break;
				case 188:
					num = 149;
					break;
				case 189:
					num = 148;
					break;
				case 215:
					num = 222;
					break;
				case 247:
					num = 221;
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
				case 160:
				case 163:
				case 164:
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
public class ENCibm864 : CP864
{
}
