using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;
using UnityEngineInternal;

[assembly: InternalsVisibleTo("UnityEngine.TextCoreTextEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.WindModule")]
[assembly: InternalsVisibleTo("UnityEngine.SwitchModule")]
[assembly: InternalsVisibleTo("UnityEngine.XboxOneModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS4Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS4VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS5Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS5VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.Networking")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud.Service")]
[assembly: InternalsVisibleTo("Unity.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.VirtualTexturingModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommon")]
[assembly: InternalsVisibleTo("UnityEngine.Purchasing")]
[assembly: InternalsVisibleTo("UnityEngine.TestRunner")]
[assembly: InternalsVisibleTo("Unity.Automation")]
[assembly: InternalsVisibleTo("Unity.Burst")]
[assembly: InternalsVisibleTo("Unity.Burst.Editor")]
[assembly: InternalsVisibleTo("Unity.DeploymentTests.Services")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.UnityAnalytics")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Timeline")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Framework")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework")]
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
[assembly: InternalsVisibleTo("UnityEngine.VideoModule")]
[assembly: InternalsVisibleTo("UnityEngine.VehiclesModule")]
[assembly: InternalsVisibleTo("UnityEngine.VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.NVIDIAModule")]
[assembly: InternalsVisibleTo("UnityEngine.ParticleSystemModule")]
[assembly: InternalsVisibleTo("UnityEngine.PerformanceReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.Physics2DModule")]
[assembly: InternalsVisibleTo("UnityEngine.ProfilerModule")]
[assembly: InternalsVisibleTo("UnityEngine.PropertiesModule")]
[assembly: InternalsVisibleTo("UnityEngine.RuntimeInitializeOnLoadManagerInitializerModule")]
[assembly: InternalsVisibleTo("UnityEngine.ScreenCaptureModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteMaskModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteShapeModule")]
[assembly: InternalsVisibleTo("UnityEngine.StreamingModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubstanceModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubsystemsModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainPhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.TilemapModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UmbraModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityCurlModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityTestProtocolModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestTextureModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestWWWModule")]
[assembly: InternalsVisibleTo("UnityEngine.VFXModule")]
[assembly: InternalsVisibleTo("UnityEngine.XRModule")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework.Tests")]
[assembly: InternalsVisibleTo("UnityEngine.LocalizationModule")]
[assembly: InternalsVisibleTo("Unity.PerformanceTests.RuntimeTestRunner.Tests")]
[assembly: InternalsVisibleTo("Unity.Timeline")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.006")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.007")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.008")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.009")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.010")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.011")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.012")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.013")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.014")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.015")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.016")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.017")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.019")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.020")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.021")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.022")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.023")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.024")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.001")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.005")]
[assembly: InternalsVisibleTo("Unity.Subsystem.Registration")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.018")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.001")]
[assembly: InternalsVisibleTo("Assembly-CSharp-testable")]
[assembly: InternalsVisibleTo("Assembly-CSharp-firstpass-testable")]
[assembly: InternalsVisibleTo("UnityEngine.SpatialTracking")]
[assembly: InternalsVisibleTo("GoogleAR.UnityNative")]
[assembly: InternalsVisibleTo("Unity.WindowsMRAutomation")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.Editor")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.EditorTests")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.Editor")]
[assembly: InternalsVisibleTo("UnityEditor.UIBuilderModule")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.EditorTests")]
[assembly: InternalsVisibleTo("Unity.UIElements")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsGameObjectsModule")]
[assembly: InternalsVisibleTo("Unity.UIElements.Editor")]
[assembly: InternalsVisibleTo("Unity.UIElements.PlayModeTests")]
[assembly: InternalsVisibleTo("UnityEngine.UIElements.Tests")]
[assembly: InternalsVisibleTo("Unity.UIElements.EditorTests")]
[assembly: InternalsVisibleTo("UnityEngine.UI")]
[assembly: InternalsVisibleTo("Unity.Networking.Transport")]
[assembly: InternalsVisibleTo("Unity.ucg.QoS")]
[assembly: InternalsVisibleTo("Unity.Services.QoS")]
[assembly: InternalsVisibleTo("Unity.Logging")]
[assembly: InternalsVisibleTo("Unity.Entities")]
[assembly: InternalsVisibleTo("Unity.Entities.Tests")]
[assembly: InternalsVisibleTo("Unity.Collections")]
[assembly: InternalsVisibleTo("Unity.Runtime")]
[assembly: InternalsVisibleTo("Unity.Core")]
[assembly: InternalsVisibleTo("UnityEngine.Core.Runtime.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.AllIn1Runner")]
[assembly: InternalsVisibleTo("UnityEngine.IMGUIModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreFontEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.HotReloadModule")]
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.AnimationModule")]
[assembly: InternalsVisibleTo("UnityEngine.AndroidJNIModule")]
[assembly: InternalsVisibleTo("UnityEngine.AccessibilityModule")]
[assembly: InternalsVisibleTo("UnityEngine.ARModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputModule")]
[assembly: InternalsVisibleTo("UnityEngine.JSONSerializeModule")]
[assembly: InternalsVisibleTo("UnityEngine.PhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.AIModule")]
[assembly: InternalsVisibleTo("UnityEngine.CoreModule")]
[assembly: InternalsVisibleTo("UnityEngine.SharedInternalsModule")]
[assembly: InternalsVisibleTo("UnityEngine")]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: CompilationRelaxations(8)]
[assembly: InternalsVisibleTo("UnityEngine.AudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClothModule")]
[assembly: UnityEngineModuleAssembly]
[assembly: InternalsVisibleTo("UnityEngine.ClusterRendererModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputLegacyModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextRenderingModule")]
[assembly: InternalsVisibleTo("UnityEngine.GridModule")]
[assembly: InternalsVisibleTo("UnityEngine.GameCenterModule")]
[assembly: InternalsVisibleTo("UnityEngine.ImageConversionModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
[assembly: InternalsVisibleTo("UnityEngine.DirectorModule")]
[assembly: InternalsVisibleTo("UnityEngine.DSPGraphModule")]
[assembly: InternalsVisibleTo("UnityEngine.GIModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestModule")]
[assembly: InternalsVisibleTo("UnityEngine.TLSModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommonModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityConnectModule")]
[assembly: InternalsVisibleTo("UnityEngine.ContentLoadModule")]
[assembly: InternalsVisibleTo("UnityEngine.CrashReportingModule")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace UnityEngineInternal
{
	internal static class WebRequestUtils
	{
		private static Regex domainRegex = new Regex("^\\s*\\w+(?:\\.\\w+)+(\\/.*)?$");

		[RequiredByNativeCode]
		internal static string RedirectTo(string baseUri, string redirectUri)
		{
			Uri uri = ((redirectUri[0] != '/') ? new Uri(redirectUri, UriKind.RelativeOrAbsolute) : new Uri(redirectUri, UriKind.Relative));
			if (uri.IsAbsoluteUri)
			{
				return uri.AbsoluteUri;
			}
			Uri baseUri2 = new Uri(baseUri, UriKind.Absolute);
			Uri uri2 = new Uri(baseUri2, uri);
			return uri2.AbsoluteUri;
		}

		internal static string MakeInitialUrl(string targetUrl, string localUrl)
		{
			if (string.IsNullOrEmpty(targetUrl))
			{
				return "";
			}
			bool prependProtocol = false;
			Uri uri = new Uri(localUrl);
			Uri uri2 = null;
			if (targetUrl[0] == '/')
			{
				uri2 = new Uri(uri, targetUrl);
				prependProtocol = true;
			}
			if (uri2 == null && domainRegex.IsMatch(targetUrl))
			{
				targetUrl = uri.Scheme + "://" + targetUrl;
				prependProtocol = true;
			}
			FormatException ex = null;
			try
			{
				if (uri2 == null && targetUrl[0] != '.')
				{
					uri2 = new Uri(targetUrl);
				}
			}
			catch (FormatException ex2)
			{
				ex = ex2;
			}
			if (uri2 == null)
			{
				try
				{
					uri2 = new Uri(uri, targetUrl);
					prependProtocol = true;
				}
				catch (FormatException)
				{
					throw ex;
				}
			}
			return MakeUriString(uri2, targetUrl, prependProtocol);
		}

		internal static string MakeUriString(Uri targetUri, string targetUrl, bool prependProtocol)
		{
			if (targetUri.IsFile)
			{
				if (!targetUri.IsLoopback)
				{
					return targetUri.OriginalString;
				}
				string text = targetUri.AbsolutePath;
				if (text.Contains("%"))
				{
					if (text.Contains('+'))
					{
						string originalString = targetUri.OriginalString;
						if (!originalString.StartsWith("file:"))
						{
							return "file:///" + originalString.Replace('\\', '/');
						}
					}
					text = URLDecode(text);
				}
				if (text.Length > 0 && text[0] != '/')
				{
					text = "/" + text;
				}
				return "file://" + text;
			}
			string scheme = targetUri.Scheme;
			if (!prependProtocol && targetUrl.Length >= scheme.Length + 2 && targetUrl[scheme.Length + 1] != '/')
			{
				StringBuilder stringBuilder = new StringBuilder(scheme, targetUrl.Length);
				stringBuilder.Append(':');
				if (scheme == "jar")
				{
					string text2 = targetUri.AbsolutePath;
					if (text2.Contains("%"))
					{
						text2 = URLDecode(text2);
					}
					if (text2.StartsWith("file:/") && text2.Length > 6 && text2[6] != '/')
					{
						stringBuilder.Append("file://");
						stringBuilder.Append(text2.Substring(5));
					}
					else
					{
						stringBuilder.Append(text2);
					}
					return stringBuilder.ToString();
				}
				stringBuilder.Append(targetUri.PathAndQuery);
				stringBuilder.Append(targetUri.Fragment);
				return stringBuilder.ToString();
			}
			if (targetUrl.Contains("%"))
			{
				return targetUri.OriginalString;
			}
			return targetUri.AbsoluteUri;
		}

		private static string URLDecode(string encoded)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(encoded);
			byte[] bytes2 = WWWTranscoder.URLDecode(bytes);
			return Encoding.UTF8.GetString(bytes2);
		}
	}
}
namespace UnityEngine
{
	public class WWWForm
	{
		private List<byte[]> formData;

		private List<string> fieldNames;

		private List<string> fileNames;

		private List<string> types;

		private byte[] boundary;

		private bool containsFiles = false;

		private static byte[] dDash = DefaultEncoding.GetBytes("--");

		private static byte[] crlf = DefaultEncoding.GetBytes("\r\n");

		private static byte[] contentTypeHeader = DefaultEncoding.GetBytes("Content-Type: ");

		private static byte[] dispositionHeader = DefaultEncoding.GetBytes("Content-disposition: form-data; name=\"");

		private static byte[] endQuote = DefaultEncoding.GetBytes("\"");

		private static byte[] fileNameField = DefaultEncoding.GetBytes("; filename=\"");

		private static byte[] ampersand = DefaultEncoding.GetBytes("&");

		private static byte[] equal = DefaultEncoding.GetBytes("=");

		internal static Encoding DefaultEncoding => Encoding.ASCII;

		public Dictionary<string, string> headers
		{
			get
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				if (containsFiles)
				{
					dictionary["Content-Type"] = "multipart/form-data; boundary=\"" + Encoding.UTF8.GetString(boundary, 0, boundary.Length) + "\"";
				}
				else
				{
					dictionary["Content-Type"] = "application/x-www-form-urlencoded";
				}
				return dictionary;
			}
		}

		public byte[] data
		{
			get
			{
				using MemoryStream memoryStream = new MemoryStream(1024);
				if (containsFiles)
				{
					for (int i = 0; i < formData.Count; i++)
					{
						memoryStream.Write(crlf, 0, crlf.Length);
						memoryStream.Write(dDash, 0, dDash.Length);
						memoryStream.Write(boundary, 0, boundary.Length);
						memoryStream.Write(crlf, 0, crlf.Length);
						memoryStream.Write(contentTypeHeader, 0, contentTypeHeader.Length);
						byte[] bytes = Encoding.UTF8.GetBytes(types[i]);
						memoryStream.Write(bytes, 0, bytes.Length);
						memoryStream.Write(crlf, 0, crlf.Length);
						memoryStream.Write(dispositionHeader, 0, dispositionHeader.Length);
						string headerName = Encoding.UTF8.HeaderName;
						string text = fieldNames[i];
						if (!WWWTranscoder.SevenBitClean(text, Encoding.UTF8) || text.IndexOf("=?") > -1)
						{
							text = "=?" + headerName + "?Q?" + WWWTranscoder.QPEncode(text, Encoding.UTF8) + "?=";
						}
						byte[] bytes2 = Encoding.UTF8.GetBytes(text);
						memoryStream.Write(bytes2, 0, bytes2.Length);
						memoryStream.Write(endQuote, 0, endQuote.Length);
						if (fileNames[i] != null)
						{
							string text2 = fileNames[i];
							if (!WWWTranscoder.SevenBitClean(text2, Encoding.UTF8) || text2.IndexOf("=?") > -1)
							{
								text2 = "=?" + headerName + "?Q?" + WWWTranscoder.QPEncode(text2, Encoding.UTF8) + "?=";
							}
							byte[] bytes3 = Encoding.UTF8.GetBytes(text2);
							memoryStream.Write(fileNameField, 0, fileNameField.Length);
							memoryStream.Write(bytes3, 0, bytes3.Length);
							memoryStream.Write(endQuote, 0, endQuote.Length);
						}
						memoryStream.Write(crlf, 0, crlf.Length);
						memoryStream.Write(crlf, 0, crlf.Length);
						byte[] array = formData[i];
						memoryStream.Write(array, 0, array.Length);
					}
					memoryStream.Write(crlf, 0, crlf.Length);
					memoryStream.Write(dDash, 0, dDash.Length);
					memoryStream.Write(boundary, 0, boundary.Length);
					memoryStream.Write(dDash, 0, dDash.Length);
					memoryStream.Write(crlf, 0, crlf.Length);
				}
				else
				{
					for (int j = 0; j < formData.Count; j++)
					{
						byte[] array2 = WWWTranscoder.DataEncode(Encoding.UTF8.GetBytes(fieldNames[j]));
						byte[] toEncode = formData[j];
						byte[] array3 = WWWTranscoder.DataEncode(toEncode);
						if (j > 0)
						{
							memoryStream.Write(ampersand, 0, ampersand.Length);
						}
						memoryStream.Write(array2, 0, array2.Length);
						memoryStream.Write(equal, 0, equal.Length);
						memoryStream.Write(array3, 0, array3.Length);
					}
				}
				return memoryStream.ToArray();
			}
		}

		public WWWForm()
		{
			formData = new List<byte[]>();
			fieldNames = new List<string>();
			fileNames = new List<string>();
			types = new List<string>();
			boundary = new byte[40];
			for (int i = 0; i < 40; i++)
			{
				int num = Random.Range(48, 110);
				if (num > 57)
				{
					num += 7;
				}
				if (num > 90)
				{
					num += 6;
				}
				boundary[i] = (byte)num;
			}
		}

		public void AddField(string fieldName, string value)
		{
			AddField(fieldName, value, Encoding.UTF8);
		}

		public void AddField(string fieldName, string value, Encoding e)
		{
			fieldNames.Add(fieldName);
			fileNames.Add(null);
			formData.Add(e.GetBytes(value));
			types.Add("text/plain; charset=\"" + e.WebName + "\"");
		}

		public void AddField(string fieldName, int i)
		{
			AddField(fieldName, i.ToString());
		}

		[ExcludeFromDocs]
		public void AddBinaryData(string fieldName, byte[] contents)
		{
			AddBinaryData(fieldName, contents, null, null);
		}

		[ExcludeFromDocs]
		public void AddBinaryData(string fieldName, byte[] contents, string fileName)
		{
			AddBinaryData(fieldName, contents, fileName, null);
		}

		public void AddBinaryData(string fieldName, byte[] contents, [UnityEngine.Internal.DefaultValue("null")] string fileName, [UnityEngine.Internal.DefaultValue("null")] string mimeType)
		{
			containsFiles = true;
			bool flag = contents.Length > 8 && contents[0] == 137 && contents[1] == 80 && contents[2] == 78 && contents[3] == 71 && contents[4] == 13 && contents[5] == 10 && contents[6] == 26 && contents[7] == 10;
			if (fileName == null)
			{
				fileName = fieldName + (flag ? ".png" : ".dat");
			}
			if (mimeType == null)
			{
				mimeType = ((!flag) ? "application/octet-stream" : "image/png");
			}
			fieldNames.Add(fieldName);
			fileNames.Add(fileName);
			formData.Add(contents);
			types.Add(mimeType);
		}
	}
	[VisibleToOtherModules(new string[] { "UnityEngine.UnityWebRequestWWWModule" })]
	internal class WWWTranscoder
	{
		private static byte[] ucHexChars = WWWForm.DefaultEncoding.GetBytes("0123456789ABCDEF");

		private static byte[] lcHexChars = WWWForm.DefaultEncoding.GetBytes("0123456789abcdef");

		private static byte urlEscapeChar = 37;

		private static byte[] urlSpace = new byte[1] { 43 };

		private static byte[] dataSpace = WWWForm.DefaultEncoding.GetBytes("%20");

		private static byte[] urlForbidden = WWWForm.DefaultEncoding.GetBytes("@&;:<>=?\"'/\\!#%+$,{}|^[]`");

		private static byte qpEscapeChar = 61;

		private static byte[] qpSpace = new byte[1] { 95 };

		private static byte[] qpForbidden = WWWForm.DefaultEncoding.GetBytes("&;=?\"'%+_");

		private static byte Hex2Byte(byte[] b, int offset)
		{
			byte b2 = 0;
			for (int i = offset; i < offset + 2; i++)
			{
				b2 *= 16;
				int num = b[i];
				if (num >= 48 && num <= 57)
				{
					num -= 48;
				}
				else if (num >= 65 && num <= 75)
				{
					num -= 55;
				}
				else if (num >= 97 && num <= 102)
				{
					num -= 87;
				}
				if (num > 15)
				{
					return 63;
				}
				b2 += (byte)num;
			}
			return b2;
		}

		private static void Byte2Hex(byte b, byte[] hexChars, out byte byte0, out byte byte1)
		{
			byte0 = hexChars[b >> 4];
			byte1 = hexChars[b & 0xF];
		}

		public static string URLEncode(string toEncode)
		{
			return URLEncode(toEncode, Encoding.UTF8);
		}

		public static string URLEncode(string toEncode, Encoding e)
		{
			byte[] array = Encode(e.GetBytes(toEncode), urlEscapeChar, urlSpace, urlForbidden, uppercase: false);
			return WWWForm.DefaultEncoding.GetString(array, 0, array.Length);
		}

		public static byte[] URLEncode(byte[] toEncode)
		{
			return Encode(toEncode, urlEscapeChar, urlSpace, urlForbidden, uppercase: false);
		}

		public static string DataEncode(string toEncode)
		{
			return DataEncode(toEncode, Encoding.UTF8);
		}

		public static string DataEncode(string toEncode, Encoding e)
		{
			byte[] array = Encode(e.GetBytes(toEncode), urlEscapeChar, dataSpace, urlForbidden, uppercase: false);
			return WWWForm.DefaultEncoding.GetString(array, 0, array.Length);
		}

		public static byte[] DataEncode(byte[] toEncode)
		{
			return Encode(toEncode, urlEscapeChar, dataSpace, urlForbidden, uppercase: false);
		}

		public static string QPEncode(string toEncode)
		{
			return QPEncode(toEncode, Encoding.UTF8);
		}

		public static string QPEncode(string toEncode, Encoding e)
		{
			byte[] array = Encode(e.GetBytes(toEncode), qpEscapeChar, qpSpace, qpForbidden, uppercase: true);
			return WWWForm.DefaultEncoding.GetString(array, 0, array.Length);
		}

		public static byte[] QPEncode(byte[] toEncode)
		{
			return Encode(toEncode, qpEscapeChar, qpSpace, qpForbidden, uppercase: true);
		}

		public static byte[] Encode(byte[] input, byte escapeChar, byte[] space, byte[] forbidden, bool uppercase)
		{
			using MemoryStream memoryStream = new MemoryStream(input.Length * 2);
			for (int i = 0; i < input.Length; i++)
			{
				if (input[i] == 32)
				{
					memoryStream.Write(space, 0, space.Length);
				}
				else if (input[i] < 32 || input[i] > 126 || ByteArrayContains(forbidden, input[i]))
				{
					memoryStream.WriteByte(escapeChar);
					Byte2Hex(input[i], uppercase ? ucHexChars : lcHexChars, out var @byte, out var byte2);
					memoryStream.WriteByte(@byte);
					memoryStream.WriteByte(byte2);
				}
				else
				{
					memoryStream.WriteByte(input[i]);
				}
			}
			return memoryStream.ToArray();
		}

		private static bool ByteArrayContains(byte[] array, byte b)
		{
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				if (array[i] == b)
				{
					return true;
				}
			}
			return false;
		}

		public static string URLDecode(string toEncode)
		{
			return URLDecode(toEncode, Encoding.UTF8);
		}

		public static string URLDecode(string toEncode, Encoding e)
		{
			byte[] array = Decode(WWWForm.DefaultEncoding.GetBytes(toEncode), urlEscapeChar, urlSpace);
			return e.GetString(array, 0, array.Length);
		}

		public static byte[] URLDecode(byte[] toEncode)
		{
			return Decode(toEncode, urlEscapeChar, urlSpace);
		}

		public static string DataDecode(string toDecode)
		{
			return DataDecode(toDecode, Encoding.UTF8);
		}

		public static string DataDecode(string toDecode, Encoding e)
		{
			byte[] array = Decode(WWWForm.DefaultEncoding.GetBytes(toDecode), urlEscapeChar, dataSpace);
			return e.GetString(array, 0, array.Length);
		}

		public static byte[] DataDecode(byte[] toDecode)
		{
			return Decode(toDecode, urlEscapeChar, dataSpace);
		}

		public static string QPDecode(string toEncode)
		{
			return QPDecode(toEncode, Encoding.UTF8);
		}

		public static string QPDecode(string toEncode, Encoding e)
		{
			byte[] array = Decode(WWWForm.DefaultEncoding.GetBytes(toEncode), qpEscapeChar, qpSpace);
			return e.GetString(array, 0, array.Length);
		}

		public static byte[] QPDecode(byte[] toEncode)
		{
			return Decode(toEncode, qpEscapeChar, qpSpace);
		}

		private static bool ByteSubArrayEquals(byte[] array, int index, byte[] comperand)
		{
			if (array.Length - index < comperand.Length)
			{
				return false;
			}
			for (int i = 0; i < comperand.Length; i++)
			{
				if (array[index + i] != comperand[i])
				{
					return false;
				}
			}
			return true;
		}

		public static byte[] Decode(byte[] input, byte escapeChar, byte[] space)
		{
			using MemoryStream memoryStream = new MemoryStream(input.Length);
			for (int i = 0; i < input.Length; i++)
			{
				if (ByteSubArrayEquals(input, i, space))
				{
					i += space.Length - 1;
					memoryStream.WriteByte(32);
				}
				else if (input[i] == escapeChar && i + 2 < input.Length)
				{
					i++;
					memoryStream.WriteByte(Hex2Byte(input, i++));
				}
				else
				{
					memoryStream.WriteByte(input[i]);
				}
			}
			return memoryStream.ToArray();
		}

		public static bool SevenBitClean(string s)
		{
			return SevenBitClean(s, Encoding.UTF8);
		}

		public unsafe static bool SevenBitClean(string s, Encoding e)
		{
			if (string.IsNullOrEmpty(s))
			{
				return true;
			}
			int num = s.Length * 2;
			byte* ptr = stackalloc byte[(int)(uint)num];
			int bytes;
			fixed (char* chars = s)
			{
				bytes = e.GetBytes(chars, s.Length, ptr, num);
			}
			return SevenBitClean(ptr, bytes);
		}

		public unsafe static bool SevenBitClean(byte* input, int inputLength)
		{
			for (int i = 0; i < inputLength; i++)
			{
				if (input[i] < 32 || input[i] > 126)
				{
					return false;
				}
			}
			return true;
		}
	}
}
namespace UnityEngine.Networking
{
	[StructLayout(LayoutKind.Sequential)]
	[NativeHeader("Modules/UnityWebRequest/Public/CertificateHandler/CertificateHandlerScript.h")]
	public class CertificateHandler : IDisposable
	{
		[NonSerialized]
		internal IntPtr m_Ptr;

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create(CertificateHandler obj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsThreadSafe = true)]
		private extern void Release();

		protected CertificateHandler()
		{
			m_Ptr = Create(this);
		}

		~CertificateHandler()
		{
			Dispose();
		}

		protected virtual bool ValidateCertificate(byte[] certificateData)
		{
			return false;
		}

		[RequiredByNativeCode]
		internal bool ValidateCertificateNative(byte[] certificateData)
		{
			return ValidateCertificate(certificateData);
		}

		public void Dispose()
		{
			if (m_Ptr != IntPtr.Zero)
			{
				Release();
				m_Ptr = IntPtr.Zero;
			}
		}
	}
	[StructLayout(LayoutKind.Sequential)]
	[NativeHeader("Modules/UnityWebRequest/Public/DownloadHandler/DownloadHandler.h")]
	public class DownloadHandler : IDisposable
	{
		[NonSerialized]
		[VisibleToOtherModules]
		internal IntPtr m_Ptr;

		public bool isDone => IsDone();

		public string error => GetErrorMsg();

		public NativeArray<byte>.ReadOnly nativeData => GetNativeData().AsReadOnly();

		public byte[] data => GetData();

		public string text => GetText();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsThreadSafe = true)]
		private extern void Release();

		[VisibleToOtherModules]
		internal DownloadHandler()
		{
		}

		~DownloadHandler()
		{
			Dispose();
		}

		public virtual void Dispose()
		{
			if (m_Ptr != IntPtr.Zero)
			{
				Release();
				m_Ptr = IntPtr.Zero;
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsDone();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string GetErrorMsg();

		protected virtual NativeArray<byte> GetNativeData()
		{
			return default(NativeArray<byte>);
		}

		protected virtual byte[] GetData()
		{
			return InternalGetByteArray(this);
		}

		protected unsafe virtual string GetText()
		{
			NativeArray<byte> nativeArray = GetNativeData();
			if (nativeArray.IsCreated && nativeArray.Length > 0)
			{
				return new string((sbyte*)nativeArray.GetUnsafeReadOnlyPtr(), 0, nativeArray.Length, GetTextEncoder());
			}
			return "";
		}

		private Encoding GetTextEncoder()
		{
			string contentType = GetContentType();
			if (!string.IsNullOrEmpty(contentType))
			{
				int num = contentType.IndexOf("charset", StringComparison.OrdinalIgnoreCase);
				if (num > -1)
				{
					int num2 = contentType.IndexOf('=', num);
					if (num2 > -1)
					{
						string text = contentType.Substring(num2 + 1).Trim().Trim('\'', '"')
							.Trim();
						int num3 = text.IndexOf(';');
						if (num3 > -1)
						{
							text = text.Substring(0, num3);
						}
						try
						{
							return Encoding.GetEncoding(text);
						}
						catch (ArgumentException ex)
						{
							Debug.LogWarning($"Unsupported encoding '{text}': {ex.Message}");
						}
						catch (NotSupportedException ex2)
						{
							Debug.LogWarning($"Unsupported encoding '{text}': {ex2.Message}");
						}
					}
				}
			}
			return Encoding.UTF8;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string GetContentType();

		[RequiredByNativeCode]
		protected virtual bool ReceiveData(byte[] data, int dataLength)
		{
			return true;
		}

		[RequiredByNativeCode]
		protected virtual void ReceiveContentLengthHeader(ulong contentLength)
		{
			ReceiveContentLength((int)contentLength);
		}

		[Obsolete("Use ReceiveContentLengthHeader")]
		protected virtual void ReceiveContentLength(int contentLength)
		{
		}

		[RequiredByNativeCode]
		protected virtual void CompleteContent()
		{
		}

		[RequiredByNativeCode]
		protected virtual float GetProgress()
		{
			return 0f;
		}

		protected static T GetCheckedDownloader<T>(UnityWebRequest www) where T : DownloadHandler
		{
			if (www == null)
			{
				throw new NullReferenceException("Cannot get content from a null UnityWebRequest object");
			}
			if (!www.isDone)
			{
				throw new InvalidOperationException("Cannot get content from an unfinished UnityWebRequest object");
			}
			if (www.result == UnityWebRequest.Result.ProtocolError)
			{
				throw new InvalidOperationException(www.error);
			}
			return (T)www.downloadHandler;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[VisibleToOtherModules]
		[NativeThrows]
		internal unsafe static extern byte* InternalGetByteArray(DownloadHandler dh, out int length);

		internal static byte[] InternalGetByteArray(DownloadHandler dh)
		{
			NativeArray<byte> nativeArray = dh.GetNativeData();
			if (nativeArray.IsCreated)
			{
				return nativeArray.ToArray();
			}
			return null;
		}

		internal unsafe static NativeArray<byte> InternalGetNativeArray(DownloadHandler dh, ref NativeArray<byte> nativeArray)
		{
			int length;
			byte* bytes = InternalGetByteArray(dh, out length);
			if (nativeArray.IsCreated)
			{
				if (nativeArray.Length == length)
				{
					return nativeArray;
				}
				DisposeNativeArray(ref nativeArray);
			}
			CreateNativeArrayForNativeData(ref nativeArray, bytes, length);
			return nativeArray;
		}

		internal static void DisposeNativeArray(ref NativeArray<byte> data)
		{
			if (data.IsCreated)
			{
				data = default(NativeArray<byte>);
			}
		}

		internal unsafe static void CreateNativeArrayForNativeData(ref NativeArray<byte> data, byte* bytes, int length)
		{
			data = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(bytes, length, Allocator.Persistent);
		}
	}
	[StructLayout(LayoutKind.Sequential)]
	[NativeHeader("Modules/UnityWebRequest/Public/DownloadHandler/DownloadHandlerBuffer.h")]
	public sealed class DownloadHandlerBuffer : DownloadHandler
	{
		private NativeArray<byte> m_NativeData;

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create(DownloadHandlerBuffer obj);

		private void InternalCreateBuffer()
		{
			m_Ptr = Create(this);
		}

		public DownloadHandlerBuffer()
		{
			InternalCreateBuffer();
		}

		protected override NativeArray<byte> GetNativeData()
		{
			return DownloadHandler.InternalGetNativeArray(this, ref m_NativeData);
		}

		public override void Dispose()
		{
			DownloadHandler.DisposeNativeArray(ref m_NativeData);
			base.Dispose();
		}

		public static string GetContent(UnityWebRequest www)
		{
			return DownloadHandler.GetCheckedDownloader<DownloadHandlerBuffer>(www).text;
		}
	}
	[StructLayout(LayoutKind.Sequential)]
	[NativeHeader("Modules/UnityWebRequest/Public/DownloadHandler/DownloadHandlerScript.h")]
	public class DownloadHandlerScript : DownloadHandler
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create(DownloadHandlerScript obj);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreatePreallocated(DownloadHandlerScript obj, [Unmarshalled] byte[] preallocatedBuffer);

		private void InternalCreateScript()
		{
			m_Ptr = Create(this);
		}

		private void InternalCreateScript(byte[] preallocatedBuffer)
		{
			m_Ptr = CreatePreallocated(this, preallocatedBuffer);
		}

		public DownloadHandlerScript()
		{
			InternalCreateScript();
		}

		public DownloadHandlerScript(byte[] preallocatedBuffer)
		{
			if (preallocatedBuffer == null || preallocatedBuffer.Length < 1)
			{
				throw new ArgumentException("Cannot create a preallocated-buffer DownloadHandlerScript backed by a null or zero-length array");
			}
			InternalCreateScript(preallocatedBuffer);
		}
	}
	[StructLayout(LayoutKind.Sequential)]
	[NativeHeader("Modules/UnityWebRequest/Public/DownloadHandler/DownloadHandlerVFS.h")]
	public sealed class DownloadHandlerFile : DownloadHandler
	{
		public extern bool removeFileOnAbort
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern IntPtr Create(DownloadHandlerFile obj, string path, bool append);

		private void InternalCreateVFS(string path, bool append)
		{
			string directoryName = Path.GetDirectoryName(path);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			m_Ptr = Create(this, path, append);
		}

		public DownloadHandlerFile(string path)
		{
			InternalCreateVFS(path, append: false);
		}

		public DownloadHandlerFile(string path, bool append)
		{
			InternalCreateVFS(path, append);
		}

		protected override NativeArray<byte> GetNativeData()
		{
			throw new NotSupportedException("Raw data access is not supported");
		}

		protected override byte[] GetData()
		{
			throw new NotSupportedException("Raw data access is not supported");
		}

		protected override string GetText()
		{
			throw new NotSupportedException("String access is not supported");
		}
	}
	public interface IMultipartFormSection
	{
		string sectionName { get; }

		byte[] sectionData { get; }

		string fileName { get; }

		string contentType { get; }
	}
	public class MultipartFormDataSection : IMultipartFormSection
	{
		private string name;

		private byte[] data;

		private string content;

		public string sectionName => name;

		public byte[] sectionData => data;

		public string fileName => null;

		public string contentType => content;

		public MultipartFormDataSection(string name, byte[] data, string contentType)
		{
			if (data == null || data.Length < 1)
			{
				throw new ArgumentException("Cannot create a multipart form data section without body data");
			}
			this.name = name;
			this.data = data;
			content = contentType;
		}

		public MultipartFormDataSection(string name, byte[] data)
			: this(name, data, null)
		{
		}

		public MultipartFormDataSection(byte[] data)
			: this(null, data)
		{
		}

		public MultipartFormDataSection(string name, string data, Encoding encoding, string contentType)
		{
			if (string.IsNullOrEmpty(data))
			{
				throw new ArgumentException("Cannot create a multipart form data section without body data");
			}
			byte[] bytes = encoding.GetBytes(data);
			this.name = name;
			this.data = bytes;
			if (contentType != null && !contentType.Contains("encoding="))
			{
				contentType = contentType.Trim() + "; encoding=" + encoding.WebName;
			}
			content = contentType;
		}

		public MultipartFormDataSection(string name, string data, string contentType)
			: this(name, data, Encoding.UTF8, contentType)
		{
		}

		public MultipartFormDataSection(string name, string data)
			: this(name, data, "text/plain")
		{
		}

		public MultipartFormDataSection(string data)
			: this(null, data)
		{
		}
	}
	public class MultipartFormFileSection : IMultipartFormSection
	{
		private string name;

		private byte[] data;

		private string file;

		private string content;

		public string sectionName => name;

		public byte[] sectionData => data;

		public string fileName => file;

		public string contentType => content;

		private void Init(string name, byte[] data, string fileName, string contentType)
		{
			this.name = name;
			this.data = data;
			file = fileName;
			content = contentType;
		}

		public MultipartFormFileSection(string name, byte[] data, string fileName, string contentType)
		{
			if (data == null || data.Length < 1)
			{
				throw new ArgumentException("Cannot create a multipart form file section without body data");
			}
			if (string.IsNullOrEmpty(fileName))
			{
				fileName = "file.dat";
			}
			if (string.IsNullOrEmpty(contentType))
			{
				contentType = "application/octet-stream";
			}
			Init(name, data, fileName, contentType);
		}

		public MultipartFormFileSection(byte[] data)
			: this(null, data, null, null)
		{
		}

		public MultipartFormFileSection(string fileName, byte[] data)
			: this(null, data, fileName, null)
		{
		}

		public MultipartFormFileSection(string name, string data, Encoding dataEncoding, string fileName)
		{
			if (string.IsNullOrEmpty(data))
			{
				throw new ArgumentException("Cannot create a multipart form file section without body data");
			}
			if (dataEncoding == null)
			{
				dataEncoding = Encoding.UTF8;
			}
			byte[] bytes = dataEncoding.GetBytes(data);
			if (string.IsNullOrEmpty(fileName))
			{
				fileName = "file.txt";
			}
			if (string.IsNullOrEmpty(content))
			{
				content = "text/plain; charset=" + dataEncoding.WebName;
			}
			Init(name, bytes, fileName, content);
		}

		public MultipartFormFileSection(string data, Encoding dataEncoding, string fileName)
			: this(null, data, dataEncoding, fileName)
		{
		}

		public MultipartFormFileSection(string data, string fileName)
			: this(data, null, fileName)
		{
		}
	}
	[StructLayout(LayoutKind.Sequential)]
	[NativeHeader("UnityWebRequestScriptingClasses.h")]
	[UsedByNativeCode]
	[NativeHeader("Modules/UnityWebRequest/Public/UnityWebRequestAsyncOperation.h")]
	public class UnityWebRequestAsyncOperation : AsyncOperation
	{
		public UnityWebRequest webRequest { get; internal set; }
	}
	[StructLayout(LayoutKind.Sequential)]
	[NativeHeader("Modules/UnityWebRequest/Public/UnityWebRequest.h")]
	public class UnityWebRequest : IDisposable
	{
		internal enum UnityWebRequestMethod
		{
			Get,
			Post,
			Put,
			Head,
			Custom
		}

		internal enum UnityWebRequestError
		{
			OK,
			OKCached,
			Unknown,
			SDKError,
			UnsupportedProtocol,
			MalformattedUrl,
			CannotResolveProxy,
			CannotResolveHost,
			CannotConnectToHost,
			AccessDenied,
			GenericHttpError,
			WriteError,
			ReadError,
			OutOfMemory,
			Timeout,
			HTTPPostError,
			SSLCannotConnect,
			Aborted,
			TooManyRedirects,
			ReceivedNoData,
			SSLNotSupported,
			FailedToSendData,
			FailedToReceiveData,
			SSLCertificateError,
			SSLCipherNotAvailable,
			SSLCACertError,
			UnrecognizedContentEncoding,
			LoginFailed,
			SSLShutdownFailed,
			RedirectLimitInvalid,
			InvalidRedirect,
			CannotModifyRequest,
			HeaderNameContainsInvalidCharacters,
			HeaderValueContainsInvalidCharacters,
			CannotOverrideSystemHeaders,
			AlreadySent,
			InvalidMethod,
			NotImplemented,
			NoInternetConnection,
			DataProcessingError,
			InsecureConnectionNotAllowed
		}

		public enum Result
		{
			InProgress,
			Success,
			ConnectionError,
			ProtocolError,
			DataProcessingError
		}

		[NonSerialized]
		internal IntPtr m_Ptr;

		[NonSerialized]
		internal DownloadHandler m_DownloadHandler;

		[NonSerialized]
		internal UploadHandler m_UploadHandler;

		[NonSerialized]
		internal CertificateHandler m_CertificateHandler;

		[NonSerialized]
		internal Uri m_Uri;

		public const string kHttpVerbGET = "GET";

		public const string kHttpVerbHEAD = "HEAD";

		public const string kHttpVerbPOST = "POST";

		public const string kHttpVerbPUT = "PUT";

		public const string kHttpVerbCREATE = "CREATE";

		public const string kHttpVerbDELETE = "DELETE";

		public bool disposeCertificateHandlerOnDispose { get; set; }

		public bool disposeDownloadHandlerOnDispose { get; set; }

		public bool disposeUploadHandlerOnDispose { get; set; }

		public string method
		{
			get
			{
				return GetMethod() switch
				{
					UnityWebRequestMethod.Get => "GET", 
					UnityWebRequestMethod.Post => "POST", 
					UnityWebRequestMethod.Put => "PUT", 
					UnityWebRequestMethod.Head => "HEAD", 
					_ => GetCustomMethod(), 
				};
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException("Cannot set a UnityWebRequest's method to an empty or null string");
				}
				switch (value.ToUpper())
				{
				case "GET":
					InternalSetMethod(UnityWebRequestMethod.Get);
					break;
				case "POST":
					InternalSetMethod(UnityWebRequestMethod.Post);
					break;
				case "PUT":
					InternalSetMethod(UnityWebRequestMethod.Put);
					break;
				case "HEAD":
					InternalSetMethod(UnityWebRequestMethod.Head);
					break;
				default:
					InternalSetCustomMethod(value.ToUpper());
					break;
				}
			}
		}

		public string error
		{
			get
			{
				switch (result)
				{
				case Result.InProgress:
				case Result.Success:
					return null;
				case Result.ProtocolError:
					return $"HTTP/1.1 {responseCode} {GetHTTPStatusString(responseCode)}";
				default:
					return GetWebErrorString(GetError());
				}
			}
		}

		private extern bool use100Continue
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public bool useHttpContinue
		{
			get
			{
				return use100Continue;
			}
			set
			{
				if (!isModifiable)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent and its 100-Continue setting cannot be altered");
				}
				use100Continue = value;
			}
		}

		public string url
		{
			get
			{
				return GetUrl();
			}
			set
			{
				string localUrl = "https://localhost/";
				InternalSetUrl(WebRequestUtils.MakeInitialUrl(value, localUrl));
			}
		}

		public Uri uri
		{
			get
			{
				return new Uri(GetUrl());
			}
			set
			{
				if (!value.IsAbsoluteUri)
				{
					throw new ArgumentException("URI must be absolute");
				}
				InternalSetUrl(WebRequestUtils.MakeUriString(value, value.OriginalString, prependProtocol: false));
				m_Uri = value;
			}
		}

		public extern long responseCode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public float uploadProgress
		{
			get
			{
				if (!IsExecuting() && !isDone)
				{
					return -1f;
				}
				return GetUploadProgress();
			}
		}

		public extern bool isModifiable
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("IsModifiable")]
			get;
		}

		public bool isDone => result != Result.InProgress;

		[Obsolete("UnityWebRequest.isNetworkError is deprecated. Use (UnityWebRequest.result == UnityWebRequest.Result.ConnectionError) instead.", false)]
		public bool isNetworkError => result == Result.ConnectionError;

		[Obsolete("UnityWebRequest.isHttpError is deprecated. Use (UnityWebRequest.result == UnityWebRequest.Result.ProtocolError) instead.", false)]
		public bool isHttpError => result == Result.ProtocolError;

		public extern Result result
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetResult")]
			get;
		}

		public float downloadProgress
		{
			get
			{
				if (!IsExecuting() && !isDone)
				{
					return -1f;
				}
				return GetDownloadProgress();
			}
		}

		public extern ulong uploadedBytes
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern ulong downloadedBytes
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public int redirectLimit
		{
			get
			{
				return GetRedirectLimit();
			}
			set
			{
				SetRedirectLimitFromScripting(value);
			}
		}

		[Obsolete("HTTP/2 and many HTTP/1.1 servers don't support this; we recommend leaving it set to false (default).", false)]
		public bool chunkedTransfer
		{
			get
			{
				return GetChunked();
			}
			set
			{
				if (!isModifiable)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent and its chunked transfer encoding setting cannot be altered");
				}
				UnityWebRequestError unityWebRequestError = SetChunked(value);
				if (unityWebRequestError != UnityWebRequestError.OK)
				{
					throw new InvalidOperationException(GetWebErrorString(unityWebRequestError));
				}
			}
		}

		public UploadHandler uploadHandler
		{
			get
			{
				return m_UploadHandler;
			}
			set
			{
				if (!isModifiable)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the upload handler");
				}
				UnityWebRequestError unityWebRequestError = SetUploadHandler(value);
				if (unityWebRequestError != UnityWebRequestError.OK)
				{
					throw new InvalidOperationException(GetWebErrorString(unityWebRequestError));
				}
				m_UploadHandler = value;
			}
		}

		public DownloadHandler downloadHandler
		{
			get
			{
				return m_DownloadHandler;
			}
			set
			{
				if (!isModifiable)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the download handler");
				}
				UnityWebRequestError unityWebRequestError = SetDownloadHandler(value);
				if (unityWebRequestError != UnityWebRequestError.OK)
				{
					throw new InvalidOperationException(GetWebErrorString(unityWebRequestError));
				}
				m_DownloadHandler = value;
			}
		}

		public CertificateHandler certificateHandler
		{
			get
			{
				return m_CertificateHandler;
			}
			set
			{
				if (!isModifiable)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the certificate handler");
				}
				UnityWebRequestError unityWebRequestError = SetCertificateHandler(value);
				if (unityWebRequestError != UnityWebRequestError.OK)
				{
					throw new InvalidOperationException(GetWebErrorString(unityWebRequestError));
				}
				m_CertificateHandler = value;
			}
		}

		public int timeout
		{
			get
			{
				return GetTimeoutMsec() / 1000;
			}
			set
			{
				if (!isModifiable)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the timeout");
				}
				value = Math.Max(value, 0);
				UnityWebRequestError unityWebRequestError = SetTimeoutMsec(value * 1000);
				if (unityWebRequestError != UnityWebRequestError.OK)
				{
					throw new InvalidOperationException(GetWebErrorString(unityWebRequestError));
				}
			}
		}

		internal bool suppressErrorsToConsole
		{
			get
			{
				return GetSuppressErrorsToConsole();
			}
			set
			{
				if (!isModifiable)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the timeout");
				}
				UnityWebRequestError unityWebRequestError = SetSuppressErrorsToConsole(value);
				if (unityWebRequestError != UnityWebRequestError.OK)
				{
					throw new InvalidOperationException(GetWebErrorString(unityWebRequestError));
				}
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsThreadSafe = true)]
		[NativeConditional("ENABLE_UNITYWEBREQUEST")]
		private static extern string GetWebErrorString(UnityWebRequestError err);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[VisibleToOtherModules]
		internal static extern string GetHTTPStatusString(long responseCode);

		public static void ClearCookieCache()
		{
			ClearCookieCache(null, null);
		}

		public static void ClearCookieCache(Uri uri)
		{
			if (uri == null)
			{
				ClearCookieCache(null, null);
				return;
			}
			string host = uri.Host;
			string text = uri.AbsolutePath;
			if (text == "/")
			{
				text = null;
			}
			ClearCookieCache(host, text);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearCookieCache(string domain, string path);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		internal static extern IntPtr Create();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsThreadSafe = true)]
		private extern void Release();

		internal void InternalDestroy()
		{
			if (m_Ptr != IntPtr.Zero)
			{
				Abort();
				Release();
				m_Ptr = IntPtr.Zero;
			}
		}

		private void InternalSetDefaults()
		{
			disposeDownloadHandlerOnDispose = true;
			disposeUploadHandlerOnDispose = true;
			disposeCertificateHandlerOnDispose = true;
		}

		public UnityWebRequest()
		{
			m_Ptr = Create();
			InternalSetDefaults();
		}

		public UnityWebRequest(string url)
		{
			m_Ptr = Create();
			InternalSetDefaults();
			this.url = url;
		}

		public UnityWebRequest(Uri uri)
		{
			m_Ptr = Create();
			InternalSetDefaults();
			this.uri = uri;
		}

		public UnityWebRequest(string url, string method)
		{
			m_Ptr = Create();
			InternalSetDefaults();
			this.url = url;
			this.method = method;
		}

		public UnityWebRequest(Uri uri, string method)
		{
			m_Ptr = Create();
			InternalSetDefaults();
			this.uri = uri;
			this.method = method;
		}

		public UnityWebRequest(string url, string method, DownloadHandler downloadHandler, UploadHandler uploadHandler)
		{
			m_Ptr = Create();
			InternalSetDefaults();
			this.url = url;
			this.method = method;
			this.downloadHandler = downloadHandler;
			this.uploadHandler = uploadHandler;
		}

		public UnityWebRequest(Uri uri, string method, DownloadHandler downloadHandler, UploadHandler uploadHandler)
		{
			m_Ptr = Create();
			InternalSetDefaults();
			this.uri = uri;
			this.method = method;
			this.downloadHandler = downloadHandler;
			this.uploadHandler = uploadHandler;
		}

		~UnityWebRequest()
		{
			DisposeHandlers();
			InternalDestroy();
		}

		public void Dispose()
		{
			DisposeHandlers();
			InternalDestroy();
			GC.SuppressFinalize(this);
		}

		private void DisposeHandlers()
		{
			if (disposeDownloadHandlerOnDispose)
			{
				downloadHandler?.Dispose();
			}
			if (disposeUploadHandlerOnDispose)
			{
				uploadHandler?.Dispose();
			}
			if (disposeCertificateHandlerOnDispose)
			{
				certificateHandler?.Dispose();
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		internal extern UnityWebRequestAsyncOperation BeginWebRequest();

		[Obsolete("Use SendWebRequest.  It returns a UnityWebRequestAsyncOperation which contains a reference to the WebRequest object.", false)]
		public AsyncOperation Send()
		{
			return SendWebRequest();
		}

		public UnityWebRequestAsyncOperation SendWebRequest()
		{
			UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = BeginWebRequest();
			if (unityWebRequestAsyncOperation != null)
			{
				unityWebRequestAsyncOperation.webRequest = this;
			}
			return unityWebRequestAsyncOperation;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsThreadSafe = true)]
		public extern void Abort();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequestError SetMethod(UnityWebRequestMethod methodType);

		internal void InternalSetMethod(UnityWebRequestMethod methodType)
		{
			if (!isModifiable)
			{
				throw new InvalidOperationException("UnityWebRequest has already been sent and its request method can no longer be altered");
			}
			UnityWebRequestError unityWebRequestError = SetMethod(methodType);
			if (unityWebRequestError != UnityWebRequestError.OK)
			{
				throw new InvalidOperationException(GetWebErrorString(unityWebRequestError));
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequestError SetCustomMethod(string customMethodName);

		internal void InternalSetCustomMethod(string customMethodName)
		{
			if (!isModifiable)
			{
				throw new InvalidOperationException("UnityWebRequest has already been sent and its request method can no longer be altered");
			}
			UnityWebRequestError unityWebRequestError = SetCustomMethod(customMethodName);
			if (unityWebRequestError != UnityWebRequestError.OK)
			{
				throw new InvalidOperationException(GetWebErrorString(unityWebRequestError));
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern UnityWebRequestMethod GetMethod();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string GetCustomMethod();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequestError GetError();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string GetUrl();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequestError SetUrl(string url);

		private void InternalSetUrl(string url)
		{
			if (!isModifiable)
			{
				throw new InvalidOperationException("UnityWebRequest has already been sent and its URL cannot be altered");
			}
			UnityWebRequestError unityWebRequestError = SetUrl(url);
			if (unityWebRequestError != UnityWebRequestError.OK)
			{
				throw new InvalidOperationException(GetWebErrorString(unityWebRequestError));
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetUploadProgress();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsExecuting();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetDownloadProgress();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetRedirectLimit();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private extern void SetRedirectLimitFromScripting(int limit);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetChunked();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequestError SetChunked(bool chunked);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetRequestHeader(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("SetRequestHeader")]
		internal extern UnityWebRequestError InternalSetRequestHeader(string name, string value);

		public void SetRequestHeader(string name, string value)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("Cannot set a Request Header with a null or empty name");
			}
			if (value == null)
			{
				throw new ArgumentException("Cannot set a Request header with a null");
			}
			if (!isModifiable)
			{
				throw new InvalidOperationException("UnityWebRequest has already been sent and its request headers cannot be altered");
			}
			UnityWebRequestError unityWebRequestError = InternalSetRequestHeader(name, value);
			if (unityWebRequestError != UnityWebRequestError.OK)
			{
				throw new InvalidOperationException(GetWebErrorString(unityWebRequestError));
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetResponseHeader(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string[] GetResponseHeaderKeys();

		public Dictionary<string, string> GetResponseHeaders()
		{
			string[] responseHeaderKeys = GetResponseHeaderKeys();
			if (responseHeaderKeys == null || responseHeaderKeys.Length == 0)
			{
				return null;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>(responseHeaderKeys.Length, StringComparer.OrdinalIgnoreCase);
			for (int i = 0; i < responseHeaderKeys.Length; i++)
			{
				string responseHeader = GetResponseHeader(responseHeaderKeys[i]);
				dictionary.Add(responseHeaderKeys[i], responseHeader);
			}
			return dictionary;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequestError SetUploadHandler(UploadHandler uh);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequestError SetDownloadHandler(DownloadHandler dh);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequestError SetCertificateHandler(CertificateHandler ch);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GetTimeoutMsec();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequestError SetTimeoutMsec(int timeout);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetSuppressErrorsToConsole();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern UnityWebRequestError SetSuppressErrorsToConsole(bool suppress);

		public static UnityWebRequest Get(string uri)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerBuffer(), null);
		}

		public static UnityWebRequest Get(Uri uri)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerBuffer(), null);
		}

		public static UnityWebRequest Delete(string uri)
		{
			return new UnityWebRequest(uri, "DELETE");
		}

		public static UnityWebRequest Delete(Uri uri)
		{
			return new UnityWebRequest(uri, "DELETE");
		}

		public static UnityWebRequest Head(string uri)
		{
			return new UnityWebRequest(uri, "HEAD");
		}

		public static UnityWebRequest Head(Uri uri)
		{
			return new UnityWebRequest(uri, "HEAD");
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("UnityWebRequest.GetTexture is obsolete. Use UnityWebRequestTexture.GetTexture instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestTexture.GetTexture(*)", true)]
		public static UnityWebRequest GetTexture(string uri)
		{
			throw new NotSupportedException("UnityWebRequest.GetTexture is obsolete. Use UnityWebRequestTexture.GetTexture instead.");
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("UnityWebRequest.GetTexture is obsolete. Use UnityWebRequestTexture.GetTexture instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestTexture.GetTexture(*)", true)]
		public static UnityWebRequest GetTexture(string uri, bool nonReadable)
		{
			throw new NotSupportedException("UnityWebRequest.GetTexture is obsolete. Use UnityWebRequestTexture.GetTexture instead.");
		}

		[Obsolete("UnityWebRequest.GetAudioClip is obsolete. Use UnityWebRequestMultimedia.GetAudioClip instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestMultimedia.GetAudioClip(*)", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static UnityWebRequest GetAudioClip(string uri, AudioType audioType)
		{
			return null;
		}

		[Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static UnityWebRequest GetAssetBundle(string uri)
		{
			return null;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		public static UnityWebRequest GetAssetBundle(string uri, uint crc)
		{
			return null;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		public static UnityWebRequest GetAssetBundle(string uri, uint version, uint crc)
		{
			return null;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		public static UnityWebRequest GetAssetBundle(string uri, Hash128 hash, uint crc)
		{
			return null;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		public static UnityWebRequest GetAssetBundle(string uri, CachedAssetBundle cachedAssetBundle, uint crc)
		{
			return null;
		}

		public static UnityWebRequest Put(string uri, byte[] bodyData)
		{
			return new UnityWebRequest(uri, "PUT", new DownloadHandlerBuffer(), new UploadHandlerRaw(bodyData));
		}

		public static UnityWebRequest Put(Uri uri, byte[] bodyData)
		{
			return new UnityWebRequest(uri, "PUT", new DownloadHandlerBuffer(), new UploadHandlerRaw(bodyData));
		}

		public static UnityWebRequest Put(string uri, string bodyData)
		{
			return new UnityWebRequest(uri, "PUT", new DownloadHandlerBuffer(), new UploadHandlerRaw(Encoding.UTF8.GetBytes(bodyData)));
		}

		public static UnityWebRequest Put(Uri uri, string bodyData)
		{
			return new UnityWebRequest(uri, "PUT", new DownloadHandlerBuffer(), new UploadHandlerRaw(Encoding.UTF8.GetBytes(bodyData)));
		}

		[Obsolete("UnityWebRequest.Post with only a string data is obsolete. Use UnityWebRequest.Post with content type argument or UnityWebRequest.PostWwwForm instead (UnityUpgradable) -> [UnityEngine] UnityWebRequest.PostWwwForm(*)", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static UnityWebRequest Post(string uri, string postData)
		{
			return PostWwwForm(uri, postData);
		}

		[Obsolete("UnityWebRequest.Post with only a string data is obsolete. Use UnityWebRequest.Post with content type argument or UnityWebRequest.PostWwwForm instead (UnityUpgradable) -> [UnityEngine] UnityWebRequest.PostWwwForm(*)", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static UnityWebRequest Post(Uri uri, string postData)
		{
			return PostWwwForm(uri, postData);
		}

		public static UnityWebRequest PostWwwForm(string uri, string form)
		{
			UnityWebRequest request = new UnityWebRequest(uri, "POST");
			SetupPostWwwForm(request, form);
			return request;
		}

		public static UnityWebRequest PostWwwForm(Uri uri, string form)
		{
			UnityWebRequest request = new UnityWebRequest(uri, "POST");
			SetupPostWwwForm(request, form);
			return request;
		}

		private static void SetupPostWwwForm(UnityWebRequest request, string postData)
		{
			request.downloadHandler = new DownloadHandlerBuffer();
			if (!string.IsNullOrEmpty(postData))
			{
				byte[] array = null;
				string s = WWWTranscoder.DataEncode(postData, Encoding.UTF8);
				array = Encoding.UTF8.GetBytes(s);
				request.uploadHandler = new UploadHandlerRaw(array);
				request.uploadHandler.contentType = "application/x-www-form-urlencoded";
			}
		}

		public static UnityWebRequest Post(string uri, string postData, string contentType)
		{
			UnityWebRequest request = new UnityWebRequest(uri, "POST");
			SetupPost(request, postData, contentType);
			return request;
		}

		public static UnityWebRequest Post(Uri uri, string postData, string contentType)
		{
			UnityWebRequest request = new UnityWebRequest(uri, "POST");
			SetupPost(request, postData, contentType);
			return request;
		}

		private static void SetupPost(UnityWebRequest request, string postData, string contentType)
		{
			request.downloadHandler = new DownloadHandlerBuffer();
			if (string.IsNullOrEmpty(postData))
			{
				request.SetRequestHeader("Content-Type", contentType);
				return;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(postData);
			request.uploadHandler = new UploadHandlerRaw(bytes);
			request.uploadHandler.contentType = contentType;
		}

		public static UnityWebRequest Post(string uri, WWWForm formData)
		{
			UnityWebRequest request = new UnityWebRequest(uri, "POST");
			SetupPost(request, formData);
			return request;
		}

		public static UnityWebRequest Post(Uri uri, WWWForm formData)
		{
			UnityWebRequest request = new UnityWebRequest(uri, "POST");
			SetupPost(request, formData);
			return request;
		}

		private static void SetupPost(UnityWebRequest request, WWWForm formData)
		{
			request.downloadHandler = new DownloadHandlerBuffer();
			if (formData == null)
			{
				return;
			}
			byte[] array = null;
			array = formData.data;
			if (array.Length == 0)
			{
				array = null;
			}
			if (array != null)
			{
				request.uploadHandler = new UploadHandlerRaw(array);
			}
			Dictionary<string, string> headers = formData.headers;
			foreach (KeyValuePair<string, string> item in headers)
			{
				request.SetRequestHeader(item.Key, item.Value);
			}
		}

		public static UnityWebRequest Post(string uri, List<IMultipartFormSection> multipartFormSections)
		{
			byte[] boundary = GenerateBoundary();
			return Post(uri, multipartFormSections, boundary);
		}

		public static UnityWebRequest Post(Uri uri, List<IMultipartFormSection> multipartFormSections)
		{
			byte[] boundary = GenerateBoundary();
			return Post(uri, multipartFormSections, boundary);
		}

		public static UnityWebRequest Post(string uri, List<IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			UnityWebRequest request = new UnityWebRequest(uri, "POST");
			SetupPost(request, multipartFormSections, boundary);
			return request;
		}

		public static UnityWebRequest Post(Uri uri, List<IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			UnityWebRequest request = new UnityWebRequest(uri, "POST");
			SetupPost(request, multipartFormSections, boundary);
			return request;
		}

		private static void SetupPost(UnityWebRequest request, List<IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			request.downloadHandler = new DownloadHandlerBuffer();
			byte[] array = null;
			if (multipartFormSections != null && multipartFormSections.Count != 0)
			{
				array = SerializeFormSections(multipartFormSections, boundary);
			}
			if (array != null)
			{
				UploadHandler uploadHandler = new UploadHandlerRaw(array);
				uploadHandler.contentType = "multipart/form-data; boundary=" + Encoding.UTF8.GetString(boundary, 0, boundary.Length);
				request.uploadHandler = uploadHandler;
			}
		}

		public static UnityWebRequest Post(string uri, Dictionary<string, string> formFields)
		{
			UnityWebRequest request = new UnityWebRequest(uri, "POST");
			SetupPost(request, formFields);
			return request;
		}

		public static UnityWebRequest Post(Uri uri, Dictionary<string, string> formFields)
		{
			UnityWebRequest request = new UnityWebRequest(uri, "POST");
			SetupPost(request, formFields);
			return request;
		}

		private static void SetupPost(UnityWebRequest request, Dictionary<string, string> formFields)
		{
			request.downloadHandler = new DownloadHandlerBuffer();
			byte[] array = null;
			if (formFields != null && formFields.Count != 0)
			{
				array = SerializeSimpleForm(formFields);
			}
			if (array != null)
			{
				UploadHandler uploadHandler = new UploadHandlerRaw(array);
				uploadHandler.contentType = "application/x-www-form-urlencoded";
				request.uploadHandler = uploadHandler;
			}
		}

		public static string EscapeURL(string s)
		{
			return EscapeURL(s, Encoding.UTF8);
		}

		public static string EscapeURL(string s, Encoding e)
		{
			if (s == null)
			{
				return null;
			}
			if (s == "")
			{
				return "";
			}
			if (e == null)
			{
				return null;
			}
			byte[] bytes = e.GetBytes(s);
			byte[] bytes2 = WWWTranscoder.URLEncode(bytes);
			return e.GetString(bytes2);
		}

		public static string UnEscapeURL(string s)
		{
			return UnEscapeURL(s, Encoding.UTF8);
		}

		public static string UnEscapeURL(string s, Encoding e)
		{
			if (s == null)
			{
				return null;
			}
			if (s.IndexOf('%') == -1 && s.IndexOf('+') == -1)
			{
				return s;
			}
			byte[] bytes = e.GetBytes(s);
			byte[] bytes2 = WWWTranscoder.URLDecode(bytes);
			return e.GetString(bytes2);
		}

		public static byte[] SerializeFormSections(List<IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			if (multipartFormSections == null || multipartFormSections.Count == 0)
			{
				return null;
			}
			byte[] bytes = Encoding.UTF8.GetBytes("\r\n");
			byte[] bytes2 = WWWForm.DefaultEncoding.GetBytes("--");
			int num = 0;
			foreach (IMultipartFormSection multipartFormSection in multipartFormSections)
			{
				num += 64 + multipartFormSection.sectionData.Length;
			}
			List<byte> list = new List<byte>(num);
			foreach (IMultipartFormSection multipartFormSection2 in multipartFormSections)
			{
				string text = "form-data";
				string sectionName = multipartFormSection2.sectionName;
				string fileName = multipartFormSection2.fileName;
				string text2 = "Content-Disposition: " + text;
				if (!string.IsNullOrEmpty(sectionName))
				{
					text2 = text2 + "; name=\"" + sectionName + "\"";
				}
				if (!string.IsNullOrEmpty(fileName))
				{
					text2 = text2 + "; filename=\"" + fileName + "\"";
				}
				text2 += "\r\n";
				string contentType = multipartFormSection2.contentType;
				if (!string.IsNullOrEmpty(contentType))
				{
					text2 = text2 + "Content-Type: " + contentType + "\r\n";
				}
				list.AddRange(bytes);
				list.AddRange(bytes2);
				list.AddRange(boundary);
				list.AddRange(bytes);
				list.AddRange(Encoding.UTF8.GetBytes(text2));
				list.AddRange(bytes);
				list.AddRange(multipartFormSection2.sectionData);
			}
			list.AddRange(bytes);
			list.AddRange(bytes2);
			list.AddRange(boundary);
			list.AddRange(bytes2);
			list.AddRange(bytes);
			return list.ToArray();
		}

		public static byte[] GenerateBoundary()
		{
			byte[] array = new byte[40];
			for (int i = 0; i < 40; i++)
			{
				int num = Random.Range(48, 110);
				if (num > 57)
				{
					num += 7;
				}
				if (num > 90)
				{
					num += 6;
				}
				array[i] = (byte)num;
			}
			return array;
		}

		public static byte[] SerializeSimpleForm(Dictionary<string, string> formFields)
		{
			string text = "";
			foreach (KeyValuePair<string, string> formField in formFields)
			{
				if (text.Length > 0)
				{
					text += "&";
				}
				text = text + WWWTranscoder.DataEncode(formField.Key) + "=" + WWWTranscoder.DataEncode(formField.Value);
			}
			return Encoding.UTF8.GetBytes(text);
		}
	}
	[StructLayout(LayoutKind.Sequential)]
	[NativeHeader("Modules/UnityWebRequest/Public/UploadHandler/UploadHandler.h")]
	public class UploadHandler : IDisposable
	{
		[NonSerialized]
		internal IntPtr m_Ptr;

		public byte[] data => GetData();

		public string contentType
		{
			get
			{
				return GetContentType();
			}
			set
			{
				SetContentType(value);
			}
		}

		public float progress => GetProgress();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsThreadSafe = true)]
		private extern void Release();

		internal UploadHandler()
		{
		}

		~UploadHandler()
		{
			Dispose();
		}

		public virtual void Dispose()
		{
			if (m_Ptr != IntPtr.Zero)
			{
				Release();
				m_Ptr = IntPtr.Zero;
			}
		}

		internal virtual byte[] GetData()
		{
			return null;
		}

		internal virtual string GetContentType()
		{
			return InternalGetContentType();
		}

		internal virtual void SetContentType(string newContentType)
		{
			InternalSetContentType(newContentType);
		}

		internal virtual float GetProgress()
		{
			return InternalGetProgress();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetContentType")]
		private extern string InternalGetContentType();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("SetContentType")]
		private extern void InternalSetContentType(string newContentType);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetProgress")]
		private extern float InternalGetProgress();
	}
	[StructLayout(LayoutKind.Sequential)]
	[NativeHeader("Modules/UnityWebRequest/Public/UploadHandler/UploadHandlerRaw.h")]
	public sealed class UploadHandlerRaw : UploadHandler
	{
		private NativeArray<byte> m_Payload;

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr Create(UploadHandlerRaw self, byte* data, int dataLength);

		public UploadHandlerRaw(byte[] data)
			: this((data == null || data.Length == 0) ? default(NativeArray<byte>) : new NativeArray<byte>(data, Allocator.Persistent), transferOwnership: true)
		{
		}

		public unsafe UploadHandlerRaw(NativeArray<byte> data, bool transferOwnership)
		{
			if (!data.IsCreated || data.Length == 0)
			{
				m_Ptr = Create(this, null, 0);
				return;
			}
			if (transferOwnership)
			{
				m_Payload = data;
			}
			m_Ptr = Create(this, (byte*)data.GetUnsafeReadOnlyPtr(), data.Length);
		}

		public unsafe UploadHandlerRaw(NativeArray<byte>.ReadOnly data)
		{
			if (!data.IsCreated || data.Length == 0)
			{
				m_Ptr = Create(this, null, 0);
			}
			else if (data.Length == 0)
			{
				m_Ptr = Create(this, null, 0);
			}
			else
			{
				m_Ptr = Create(this, (byte*)data.GetUnsafeReadOnlyPtr(), data.Length);
			}
		}

		internal override byte[] GetData()
		{
			if (m_Payload.IsCreated)
			{
				return m_Payload.ToArray();
			}
			return null;
		}

		public override void Dispose()
		{
			if (m_Payload.IsCreated)
			{
				m_Payload.Dispose();
			}
			base.Dispose();
		}
	}
	[StructLayout(LayoutKind.Sequential)]
	[NativeHeader("Modules/UnityWebRequest/Public/UploadHandler/UploadHandlerFile.h")]
	public sealed class UploadHandlerFile : UploadHandler
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern IntPtr Create(UploadHandlerFile self, string filePath);

		public UploadHandlerFile(string filePath)
		{
			m_Ptr = Create(this, filePath);
		}
	}
}
