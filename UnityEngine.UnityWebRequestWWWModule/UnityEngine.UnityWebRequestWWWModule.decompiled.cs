using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Networking;

[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestWWWModule")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.UnityAnalytics")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Timeline")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Framework")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework.Tests")]
[assembly: InternalsVisibleTo("Unity.PerformanceTests.RuntimeTestRunner.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.AllIn1Runner")]
[assembly: InternalsVisibleTo("Unity.Timeline")]
[assembly: InternalsVisibleTo("Assembly-CSharp-testable")]
[assembly: InternalsVisibleTo("Assembly-CSharp-firstpass-testable")]
[assembly: InternalsVisibleTo("UnityEngine.SpatialTracking")]
[assembly: InternalsVisibleTo("GoogleAR.UnityNative")]
[assembly: InternalsVisibleTo("Unity.WindowsMRAutomation")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.Editor")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.EditorTests")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.Editor")]
[assembly: InternalsVisibleTo("UnityEditor.UIBuilderModule")]
[assembly: InternalsVisibleTo("Unity.DeploymentTests.Services")]
[assembly: InternalsVisibleTo("Unity.Burst.Editor")]
[assembly: InternalsVisibleTo("Unity.Burst")]
[assembly: InternalsVisibleTo("Unity.Automation")]
[assembly: InternalsVisibleTo("UnityEngine.VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.VehiclesModule")]
[assembly: InternalsVisibleTo("UnityEngine.VideoModule")]
[assembly: InternalsVisibleTo("UnityEngine.VirtualTexturingModule")]
[assembly: InternalsVisibleTo("UnityEngine.WindModule")]
[assembly: InternalsVisibleTo("UnityEngine.SwitchModule")]
[assembly: InternalsVisibleTo("UnityEngine.XboxOneModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS4Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS4VRModule")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.EditorTests")]
[assembly: InternalsVisibleTo("UnityEngine.PS5Module")]
[assembly: InternalsVisibleTo("UnityEngine.Networking")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud.Service")]
[assembly: InternalsVisibleTo("Unity.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommon")]
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
[assembly: InternalsVisibleTo("UnityEngine.Purchasing")]
[assembly: InternalsVisibleTo("UnityEngine.TestRunner")]
[assembly: InternalsVisibleTo("UnityEngine.PS5VRModule")]
[assembly: InternalsVisibleTo("Unity.UIElements")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsGameObjectsModule")]
[assembly: InternalsVisibleTo("Unity.UIElements.Editor")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.011")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.012")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.013")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.014")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.015")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.016")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.017")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.018")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.019")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.010")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.020")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.022")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.023")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.024")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.001")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.005")]
[assembly: InternalsVisibleTo("Unity.Subsystem.Registration")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.021")]
[assembly: InternalsVisibleTo("UnityEngine.XRModule")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.009")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.007")]
[assembly: InternalsVisibleTo("Unity.UIElements.PlayModeTests")]
[assembly: InternalsVisibleTo("UnityEngine.UIElements.Tests")]
[assembly: InternalsVisibleTo("Unity.UIElements.EditorTests")]
[assembly: InternalsVisibleTo("UnityEngine.UI")]
[assembly: InternalsVisibleTo("Unity.Networking.Transport")]
[assembly: InternalsVisibleTo("Unity.ucg.QoS")]
[assembly: InternalsVisibleTo("Unity.Services.QoS")]
[assembly: InternalsVisibleTo("Unity.Logging")]
[assembly: InternalsVisibleTo("Unity.Entities")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.008")]
[assembly: InternalsVisibleTo("Unity.Entities.Tests")]
[assembly: InternalsVisibleTo("Unity.Runtime")]
[assembly: InternalsVisibleTo("Unity.Core")]
[assembly: InternalsVisibleTo("UnityEngine.Core.Runtime.Tests")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.001")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.006")]
[assembly: InternalsVisibleTo("Unity.Collections")]
[assembly: InternalsVisibleTo("UnityEngine.VFXModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.DirectorModule")]
[assembly: InternalsVisibleTo("UnityEngine.DSPGraphModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestModule")]
[assembly: InternalsVisibleTo("UnityEngine.TLSModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommonModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityConnectModule")]
[assembly: InternalsVisibleTo("UnityEngine.ContentLoadModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterRendererModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClothModule")]
[assembly: InternalsVisibleTo("UnityEngine.AudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestTextureModule")]
[assembly: InternalsVisibleTo("UnityEngine.HotReloadModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.GIModule")]
[assembly: InternalsVisibleTo("UnityEngine.ImageConversionModule")]
[assembly: InternalsVisibleTo("UnityEngine.CrashReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.GridModule")]
[assembly: UnityEngineModuleAssembly]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityTestProtocolModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityCurlModule")]
[assembly: InternalsVisibleTo("UnityEngine.UmbraModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIModule")]
[assembly: InternalsVisibleTo("UnityEngine.TilemapModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainPhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainModule")]
[assembly: InternalsVisibleTo("UnityEngine.GameCenterModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubstanceModule")]
[assembly: InternalsVisibleTo("UnityEngine.StreamingModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteShapeModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteMaskModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubsystemsModule")]
[assembly: InternalsVisibleTo("UnityEngine.RuntimeInitializeOnLoadManagerInitializerModule")]
[assembly: InternalsVisibleTo("UnityEngine.PropertiesModule")]
[assembly: InternalsVisibleTo("UnityEngine.ProfilerModule")]
[assembly: InternalsVisibleTo("UnityEngine.Physics2DModule")]
[assembly: InternalsVisibleTo("UnityEngine.PerformanceReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.ParticleSystemModule")]
[assembly: InternalsVisibleTo("UnityEngine.NVIDIAModule")]
[assembly: InternalsVisibleTo("UnityEngine.LocalizationModule")]
[assembly: InternalsVisibleTo("UnityEngine.IMGUIModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreTextEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreFontEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.ScreenCaptureModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputLegacyModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextRenderingModule")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace UnityEngine
{
	[Obsolete("Use UnityWebRequest, a fully featured replacement which is more efficient and has additional features")]
	public class WWW : CustomYieldInstruction, IDisposable
	{
		private UnityWebRequest _uwr;

		private AssetBundle _assetBundle;

		private Dictionary<string, string> _responseHeaders;

		public AssetBundle assetBundle
		{
			get
			{
				if (_assetBundle == null)
				{
					if (!WaitUntilDoneIfPossible())
					{
						return null;
					}
					if (_uwr.result == UnityWebRequest.Result.ConnectionError)
					{
						return null;
					}
					if (_uwr.downloadHandler is DownloadHandlerAssetBundle downloadHandlerAssetBundle)
					{
						_assetBundle = downloadHandlerAssetBundle.assetBundle;
					}
					else
					{
						byte[] array = bytes;
						if (array == null)
						{
							return null;
						}
						_assetBundle = AssetBundle.LoadFromMemory(array);
					}
				}
				return _assetBundle;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Obsolete msg (UnityUpgradable) -> * UnityEngine.WWW.GetAudioClip()", true)]
		public Object audioClip => null;

		public byte[] bytes
		{
			get
			{
				if (!WaitUntilDoneIfPossible())
				{
					return new byte[0];
				}
				if (_uwr.result == UnityWebRequest.Result.ConnectionError)
				{
					return new byte[0];
				}
				DownloadHandler downloadHandler = _uwr.downloadHandler;
				if (downloadHandler == null)
				{
					return new byte[0];
				}
				return downloadHandler.data;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Obsolete msg (UnityUpgradable) -> * UnityEngine.WWW.GetMovieTexture()", true)]
		public Object movie => null;

		[Obsolete("WWW.size is obsolete. Please use WWW.bytesDownloaded instead")]
		public int size => bytesDownloaded;

		public int bytesDownloaded => (int)_uwr.downloadedBytes;

		public string error
		{
			get
			{
				if (!_uwr.isDone)
				{
					return null;
				}
				if (_uwr.result == UnityWebRequest.Result.ConnectionError)
				{
					return _uwr.error;
				}
				if (_uwr.responseCode >= 400)
				{
					string hTTPStatusString = UnityWebRequest.GetHTTPStatusString(_uwr.responseCode);
					return $"{_uwr.responseCode} {hTTPStatusString}";
				}
				return null;
			}
		}

		public bool isDone => _uwr.isDone;

		public float progress
		{
			get
			{
				float num = _uwr.downloadProgress;
				if (num < 0f)
				{
					num = 0f;
				}
				return num;
			}
		}

		public Dictionary<string, string> responseHeaders
		{
			get
			{
				if (!isDone)
				{
					return new Dictionary<string, string>();
				}
				if (_responseHeaders == null)
				{
					_responseHeaders = _uwr.GetResponseHeaders();
					if (_responseHeaders != null)
					{
						string hTTPStatusString = UnityWebRequest.GetHTTPStatusString(_uwr.responseCode);
						_responseHeaders["STATUS"] = $"HTTP/1.1 {_uwr.responseCode} {hTTPStatusString}";
					}
					else
					{
						_responseHeaders = new Dictionary<string, string>();
					}
				}
				return _responseHeaders;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Please use WWW.text instead. (UnityUpgradable) -> text", true)]
		public string data => text;

		public string text
		{
			get
			{
				if (!WaitUntilDoneIfPossible())
				{
					return "";
				}
				if (_uwr.result == UnityWebRequest.Result.ConnectionError)
				{
					return "";
				}
				DownloadHandler downloadHandler = _uwr.downloadHandler;
				if (downloadHandler == null)
				{
					return "";
				}
				return downloadHandler.text;
			}
		}

		public Texture2D texture => CreateTextureFromDownloadedData(markNonReadable: false);

		public Texture2D textureNonReadable => CreateTextureFromDownloadedData(markNonReadable: true);

		public ThreadPriority threadPriority { get; set; }

		public float uploadProgress
		{
			get
			{
				float num = _uwr.uploadProgress;
				if (num < 0f)
				{
					num = 0f;
				}
				return num;
			}
		}

		public string url => _uwr.url;

		public override bool keepWaiting => _uwr != null && !_uwr.isDone;

		public static string EscapeURL(string s)
		{
			return EscapeURL(s, Encoding.UTF8);
		}

		public static string EscapeURL(string s, Encoding e)
		{
			return UnityWebRequest.EscapeURL(s, e);
		}

		public static string UnEscapeURL(string s)
		{
			return UnEscapeURL(s, Encoding.UTF8);
		}

		public static string UnEscapeURL(string s, Encoding e)
		{
			return UnityWebRequest.UnEscapeURL(s, e);
		}

		public static WWW LoadFromCacheOrDownload(string url, int version)
		{
			return LoadFromCacheOrDownload(url, version, 0u);
		}

		public static WWW LoadFromCacheOrDownload(string url, int version, uint crc)
		{
			Hash128 hash = new Hash128(0u, 0u, 0u, (uint)version);
			return LoadFromCacheOrDownload(url, hash, crc);
		}

		public static WWW LoadFromCacheOrDownload(string url, Hash128 hash)
		{
			return LoadFromCacheOrDownload(url, hash, 0u);
		}

		public static WWW LoadFromCacheOrDownload(string url, Hash128 hash, uint crc)
		{
			return new WWW(url, "", hash, crc);
		}

		public static WWW LoadFromCacheOrDownload(string url, CachedAssetBundle cachedBundle, uint crc = 0u)
		{
			return new WWW(url, cachedBundle.name, cachedBundle.hash, crc);
		}

		public WWW(string url)
		{
			_uwr = UnityWebRequest.Get(url);
			_uwr.SendWebRequest();
		}

		public WWW(string url, WWWForm form)
		{
			_uwr = UnityWebRequest.Post(url, form);
			_uwr.chunkedTransfer = false;
			_uwr.SendWebRequest();
		}

		public WWW(string url, byte[] postData)
		{
			_uwr = new UnityWebRequest(url, "POST");
			_uwr.chunkedTransfer = false;
			UploadHandler uploadHandler = new UploadHandlerRaw(postData)
			{
				contentType = "application/x-www-form-urlencoded"
			};
			_uwr.uploadHandler = uploadHandler;
			_uwr.downloadHandler = new DownloadHandlerBuffer();
			_uwr.SendWebRequest();
		}

		[Obsolete("This overload is deprecated. Use UnityEngine.WWW.WWW(string, byte[], System.Collections.Generic.Dictionary<string, string>) instead.")]
		public WWW(string url, byte[] postData, Hashtable headers)
		{
			_uwr = new UnityWebRequest(url, (postData == null) ? "GET" : "POST");
			_uwr.chunkedTransfer = false;
			UploadHandler uploadHandler = new UploadHandlerRaw(postData)
			{
				contentType = "application/x-www-form-urlencoded"
			};
			_uwr.uploadHandler = uploadHandler;
			_uwr.downloadHandler = new DownloadHandlerBuffer();
			foreach (object key in headers.Keys)
			{
				_uwr.SetRequestHeader((string)key, (string)headers[key]);
			}
			_uwr.SendWebRequest();
		}

		public WWW(string url, byte[] postData, Dictionary<string, string> headers)
		{
			_uwr = new UnityWebRequest(url, (postData == null) ? "GET" : "POST");
			_uwr.chunkedTransfer = false;
			UploadHandler uploadHandler = new UploadHandlerRaw(postData)
			{
				contentType = "application/x-www-form-urlencoded"
			};
			_uwr.uploadHandler = uploadHandler;
			_uwr.downloadHandler = new DownloadHandlerBuffer();
			foreach (KeyValuePair<string, string> header in headers)
			{
				_uwr.SetRequestHeader(header.Key, header.Value);
			}
			_uwr.SendWebRequest();
		}

		internal WWW(string url, string name, Hash128 hash, uint crc)
		{
			_uwr = UnityWebRequestAssetBundle.GetAssetBundle(url, new CachedAssetBundle(name, hash), crc);
			_uwr.SendWebRequest();
		}

		private Texture2D CreateTextureFromDownloadedData(bool markNonReadable)
		{
			if (!WaitUntilDoneIfPossible())
			{
				return new Texture2D(2, 2);
			}
			if (_uwr.result == UnityWebRequest.Result.ConnectionError)
			{
				return null;
			}
			DownloadHandler downloadHandler = _uwr.downloadHandler;
			if (downloadHandler == null)
			{
				return null;
			}
			Texture2D texture2D = new Texture2D(2, 2);
			texture2D.LoadImage(downloadHandler.data, markNonReadable);
			return texture2D;
		}

		public void LoadImageIntoTexture(Texture2D texture)
		{
			if (!WaitUntilDoneIfPossible())
			{
				return;
			}
			if (_uwr.result == UnityWebRequest.Result.ConnectionError)
			{
				Debug.LogError("Cannot load image: download failed");
				return;
			}
			DownloadHandler downloadHandler = _uwr.downloadHandler;
			if (downloadHandler == null)
			{
				Debug.LogError("Cannot load image: internal error");
			}
			else
			{
				texture.LoadImage(downloadHandler.data, markNonReadable: false);
			}
		}

		public void Dispose()
		{
			if (_uwr != null)
			{
				_uwr.Dispose();
				_uwr = null;
			}
		}

		internal Object GetAudioClipInternal(bool threeD, bool stream, bool compressed, AudioType audioType)
		{
			return WebRequestWWW.InternalCreateAudioClipUsingDH(_uwr.downloadHandler, _uwr.url, stream, compressed, audioType);
		}

		public AudioClip GetAudioClip()
		{
			return GetAudioClip(threeD: true, stream: false, AudioType.UNKNOWN);
		}

		public AudioClip GetAudioClip(bool threeD)
		{
			return GetAudioClip(threeD, stream: false, AudioType.UNKNOWN);
		}

		public AudioClip GetAudioClip(bool threeD, bool stream)
		{
			return GetAudioClip(threeD, stream, AudioType.UNKNOWN);
		}

		public AudioClip GetAudioClip(bool threeD, bool stream, AudioType audioType)
		{
			return (AudioClip)GetAudioClipInternal(threeD, stream, compressed: false, audioType);
		}

		public AudioClip GetAudioClipCompressed()
		{
			return GetAudioClipCompressed(threeD: false, AudioType.UNKNOWN);
		}

		public AudioClip GetAudioClipCompressed(bool threeD)
		{
			return GetAudioClipCompressed(threeD, AudioType.UNKNOWN);
		}

		public AudioClip GetAudioClipCompressed(bool threeD, AudioType audioType)
		{
			return (AudioClip)GetAudioClipInternal(threeD, stream: false, compressed: true, audioType);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("MovieTexture is deprecated. Use VideoPlayer instead.", false)]
		public MovieTexture GetMovieTexture()
		{
			throw new Exception("MovieTexture has been removed from Unity. Use VideoPlayer instead.");
		}

		private bool WaitUntilDoneIfPossible()
		{
			if (_uwr.isDone)
			{
				return true;
			}
			if (url.StartsWith("file://", StringComparison.OrdinalIgnoreCase))
			{
				while (!_uwr.isDone)
				{
				}
				return true;
			}
			Debug.LogError("You are trying to load data from a www stream which has not completed the download yet.\nYou need to yield the download or wait until isDone returns true.");
			return false;
		}
	}
	public static class WWWAudioExtensions
	{
		[Obsolete("WWWAudioExtensions.GetAudioClip extension method has been replaced by WWW.GetAudioClip instance method. (UnityUpgradable) -> WWW.GetAudioClip()", true)]
		public static AudioClip GetAudioClip(this WWW www)
		{
			return www.GetAudioClip();
		}

		[Obsolete("WWWAudioExtensions.GetAudioClip extension method has been replaced by WWW.GetAudioClip instance method. (UnityUpgradable) -> WWW.GetAudioClip([mscorlib] System.Boolean)", true)]
		public static AudioClip GetAudioClip(this WWW www, bool threeD)
		{
			return www.GetAudioClip(threeD);
		}

		[Obsolete("WWWAudioExtensions.GetAudioClip extension method has been replaced by WWW.GetAudioClip instance method. (UnityUpgradable) -> WWW.GetAudioClip([mscorlib] System.Boolean, [mscorlib] System.Boolean)", true)]
		public static AudioClip GetAudioClip(this WWW www, bool threeD, bool stream)
		{
			return www.GetAudioClip(threeD, stream);
		}

		[Obsolete("WWWAudioExtensions.GetAudioClip extension method has been replaced by WWW.GetAudioClip instance method. (UnityUpgradable) -> WWW.GetAudioClip([mscorlib] System.Boolean, [mscorlib] System.Boolean, UnityEngine.AudioType)", true)]
		public static AudioClip GetAudioClip(this WWW www, bool threeD, bool stream, AudioType audioType)
		{
			return www.GetAudioClip(threeD, stream, audioType);
		}

		[Obsolete("WWWAudioExtensions.GetAudioClipCompressed extension method has been replaced by WWW.GetAudioClipCompressed instance method. (UnityUpgradable) -> WWW.GetAudioClipCompressed()", true)]
		public static AudioClip GetAudioClipCompressed(this WWW www)
		{
			return www.GetAudioClipCompressed();
		}

		[Obsolete("WWWAudioExtensions.GetAudioClipCompressed extension method has been replaced by WWW.GetAudioClipCompressed instance method. (UnityUpgradable) -> WWW.GetAudioClipCompressed([mscorlib] System.Boolean)", true)]
		public static AudioClip GetAudioClipCompressed(this WWW www, bool threeD)
		{
			return www.GetAudioClipCompressed(threeD);
		}

		[Obsolete("WWWAudioExtensions.GetAudioClipCompressed extension method has been replaced by WWW.GetAudioClipCompressed instance method. (UnityUpgradable) -> WWW.GetAudioClipCompressed([mscorlib] System.Boolean, UnityEngine.AudioType)", true)]
		public static AudioClip GetAudioClipCompressed(this WWW www, bool threeD, AudioType audioType)
		{
			return www.GetAudioClipCompressed(threeD, audioType);
		}

		[Obsolete("WWWAudioExtensions.GetMovieTexture extension method has been replaced by WWW.GetMovieTexture instance method. (UnityUpgradable) -> WWW.GetMovieTexture()", true)]
		public static MovieTexture GetMovieTexture(this WWW www)
		{
			return www.GetMovieTexture();
		}
	}
}
namespace UnityEngine.Networking
{
	[NativeHeader("Modules/UnityWebRequestAudio/Public/DownloadHandlerAudioClip.h")]
	internal static class WebRequestWWW
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("UnityWebRequestCreateAudioClip")]
		internal static extern AudioClip InternalCreateAudioClipUsingDH(DownloadHandler dh, string url, bool stream, bool compressed, AudioType audioType);
	}
}
