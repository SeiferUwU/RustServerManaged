using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

[assembly: InternalsVisibleTo("UnityEngine.AndroidJNIModule")]
[assembly: InternalsVisibleTo("UnityEngine.XRModule")]
[assembly: InternalsVisibleTo("UnityEngine.VFXModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestWWWModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestTextureModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityTestProtocolModule")]
[assembly: InternalsVisibleTo("UnityEngine.UmbraModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIModule")]
[assembly: InternalsVisibleTo("UnityEngine.TilemapModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainPhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityCurlModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubsystemsModule")]
[assembly: InternalsVisibleTo("UnityEngine.VehiclesModule")]
[assembly: InternalsVisibleTo("UnityEngine.VirtualTexturingModule")]
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommon")]
[assembly: InternalsVisibleTo("UnityEngine.Analytics")]
[assembly: InternalsVisibleTo("Unity.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud.Service")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud")]
[assembly: InternalsVisibleTo("UnityEngine.VideoModule")]
[assembly: InternalsVisibleTo("UnityEngine.Networking")]
[assembly: InternalsVisibleTo("UnityEngine.PS5Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS4VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS4Module")]
[assembly: InternalsVisibleTo("UnityEngine.XboxOneModule")]
[assembly: InternalsVisibleTo("UnityEngine.SwitchModule")]
[assembly: InternalsVisibleTo("UnityEngine.WindModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS5VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubstanceModule")]
[assembly: InternalsVisibleTo("UnityEngine.StreamingModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteShapeModule")]
[assembly: InternalsVisibleTo("UnityEngine.DirectorModule")]
[assembly: InternalsVisibleTo("UnityEngine.DSPGraphModule")]
[assembly: InternalsVisibleTo("UnityEngine.CrashReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestModule")]
[assembly: InternalsVisibleTo("UnityEngine.TLSModule")]
[assembly: InternalsVisibleTo("UnityEngine.GIModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommonModule")]
[assembly: InternalsVisibleTo("UnityEngine.ContentLoadModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterRendererModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClothModule")]
[assembly: InternalsVisibleTo("UnityEngine.AudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityConnectModule")]
[assembly: InternalsVisibleTo("UnityEngine.ImageConversionModule")]
[assembly: InternalsVisibleTo("UnityEngine.GameCenterModule")]
[assembly: InternalsVisibleTo("UnityEngine.GridModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteMaskModule")]
[assembly: InternalsVisibleTo("UnityEngine.ScreenCaptureModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.InputLegacyModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextRenderingModule")]
[assembly: InternalsVisibleTo("UnityEngine.Purchasing")]
[assembly: InternalsVisibleTo("UnityEngine.HotReloadModule")]
[assembly: InternalsVisibleTo("UnityEngine.TestRunner")]
[assembly: InternalsVisibleTo("Unity.Burst")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.012")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.011")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.010")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.009")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.008")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.007")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.013")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.006")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.001")]
[assembly: InternalsVisibleTo("UnityEngine.Core.Runtime.Tests")]
[assembly: InternalsVisibleTo("Unity.Core")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.005")]
[assembly: InternalsVisibleTo("Unity.Runtime")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.014")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.016")]
[assembly: InternalsVisibleTo("Unity.Subsystem.Registration")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.001")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.015")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.024")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.022")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.021")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.020")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.019")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.018")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.017")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.023")]
[assembly: InternalsVisibleTo("Unity.Collections")]
[assembly: InternalsVisibleTo("Unity.Entities.Tests")]
[assembly: InternalsVisibleTo("Unity.Entities")]
[assembly: InternalsVisibleTo("Assembly-CSharp-firstpass-testable")]
[assembly: InternalsVisibleTo("Assembly-CSharp-testable")]
[assembly: InternalsVisibleTo("Unity.Timeline")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.AllIn1Runner")]
[assembly: InternalsVisibleTo("Unity.PerformanceTests.RuntimeTestRunner.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework.Tests")]
[assembly: InternalsVisibleTo("UnityEngine.SpatialTracking")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Framework")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Timeline")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.UnityAnalytics")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests")]
[assembly: InternalsVisibleTo("Unity.DeploymentTests.Services")]
[assembly: InternalsVisibleTo("Unity.Burst.Editor")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests")]
[assembly: InternalsVisibleTo("GoogleAR.UnityNative")]
[assembly: InternalsVisibleTo("Unity.WindowsMRAutomation")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.Editor")]
[assembly: InternalsVisibleTo("Unity.Logging")]
[assembly: InternalsVisibleTo("Unity.Services.QoS")]
[assembly: InternalsVisibleTo("Unity.ucg.QoS")]
[assembly: InternalsVisibleTo("Unity.Networking.Transport")]
[assembly: InternalsVisibleTo("UnityEngine.UI")]
[assembly: InternalsVisibleTo("Unity.UIElements.EditorTests")]
[assembly: InternalsVisibleTo("UnityEngine.UIElements.Tests")]
[assembly: InternalsVisibleTo("Unity.UIElements.PlayModeTests")]
[assembly: InternalsVisibleTo("Unity.UIElements.Editor")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsGameObjectsModule")]
[assembly: InternalsVisibleTo("Unity.UIElements")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.EditorTests")]
[assembly: InternalsVisibleTo("UnityEditor.UIBuilderModule")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.Editor")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.EditorTests")]
[assembly: InternalsVisibleTo("Unity.Automation")]
[assembly: InternalsVisibleTo("UnityEngine.AnimationModule")]
[assembly: InternalsVisibleTo("UnityEngine.ARModule")]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: InternalsVisibleTo("UnityEngine")]
[assembly: InternalsVisibleTo("UnityEngine.SharedInternalsModule")]
[assembly: InternalsVisibleTo("UnityEngine.CoreModule")]
[assembly: InternalsVisibleTo("UnityEngine.AccessibilityModule")]
[assembly: InternalsVisibleTo("UnityEngine.PhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.JSONSerializeModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputModule")]
[assembly: UnityEngineModuleAssembly]
[assembly: InternalsVisibleTo("UnityEngine.AIModule")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace UnityEngine
{
	[NativeHeader("UnityAnalyticsScriptingClasses.h")]
	[NativeHeader("Modules/UnityAnalytics/RemoteSettings/RemoteSettings.h")]
	public static class RemoteSettings
	{
		public delegate void UpdatedEventHandler();

		public static event UpdatedEventHandler Updated;

		public static event Action BeforeFetchFromServer;

		public static event Action<bool, bool, int> Completed;

		[RequiredByNativeCode]
		internal static void RemoteSettingsUpdated(bool wasLastUpdatedFromServer)
		{
			RemoteSettings.Updated?.Invoke();
		}

		[RequiredByNativeCode]
		internal static void RemoteSettingsBeforeFetchFromServer()
		{
			RemoteSettings.BeforeFetchFromServer?.Invoke();
		}

		[RequiredByNativeCode]
		internal static void RemoteSettingsUpdateCompleted(bool wasLastUpdatedFromServer, bool settingsChanged, int response)
		{
			RemoteSettings.Completed?.Invoke(wasLastUpdatedFromServer, settingsChanged, response);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Calling CallOnUpdate() is not necessary any more and should be removed. Use RemoteSettingsUpdated instead", true)]
		public static void CallOnUpdate()
		{
			throw new NotSupportedException("Calling CallOnUpdate() is not necessary any more and should be removed.");
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ForceUpdate();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool WasLastUpdatedFromServer();

		[ExcludeFromDocs]
		public static int GetInt(string key)
		{
			return GetInt(key, 0);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetInt(string key, [UnityEngine.Internal.DefaultValue("0")] int defaultValue);

		[ExcludeFromDocs]
		public static long GetLong(string key)
		{
			return GetLong(key, 0L);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetLong(string key, [UnityEngine.Internal.DefaultValue("0")] long defaultValue);

		[ExcludeFromDocs]
		public static float GetFloat(string key)
		{
			return GetFloat(key, 0f);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetFloat(string key, [UnityEngine.Internal.DefaultValue("0.0F")] float defaultValue);

		[ExcludeFromDocs]
		public static string GetString(string key)
		{
			return GetString(key, "");
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetString(string key, [UnityEngine.Internal.DefaultValue("\"\"")] string defaultValue);

		[ExcludeFromDocs]
		public static bool GetBool(string key)
		{
			return GetBool(key, defaultValue: false);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetBool(string key, [UnityEngine.Internal.DefaultValue("false")] bool defaultValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HasKey(string key);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetCount();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetKeys();

		public static T GetObject<T>(string key = "")
		{
			return (T)GetObject(typeof(T), key);
		}

		public static object GetObject(Type type, string key = "")
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type.IsAbstract || type.IsSubclassOf(typeof(Object)))
			{
				throw new ArgumentException("Cannot deserialize to new instances of type '" + type.Name + ".'");
			}
			return GetAsScriptingObject(type, null, key);
		}

		public static object GetObject(string key, object defaultValue)
		{
			if (defaultValue == null)
			{
				throw new ArgumentNullException("defaultValue");
			}
			Type type = defaultValue.GetType();
			if (type.IsAbstract || type.IsSubclassOf(typeof(Object)))
			{
				throw new ArgumentException("Cannot deserialize to new instances of type '" + type.Name + ".'");
			}
			return GetAsScriptingObject(type, defaultValue, key);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object GetAsScriptingObject(Type t, object defaultValue, string key);

		public static IDictionary<string, object> GetDictionary(string key = "")
		{
			UseSafeLock();
			IDictionary<string, object> dictionary = RemoteConfigSettingsHelper.GetDictionary(GetSafeTopMap(), key);
			ReleaseSafeLock();
			return dictionary;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void UseSafeLock();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ReleaseSafeLock();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetSafeTopMap();
	}
	[StructLayout(LayoutKind.Sequential)]
	[ExcludeFromDocs]
	[NativeHeader("UnityAnalyticsScriptingClasses.h")]
	[NativeHeader("Modules/UnityAnalytics/RemoteSettings/RemoteSettings.h")]
	public class RemoteConfigSettings : IDisposable
	{
		[NonSerialized]
		internal IntPtr m_Ptr;

		public event Action<bool> Updated;

		private RemoteConfigSettings()
		{
		}

		public RemoteConfigSettings(string configKey)
		{
			m_Ptr = Internal_Create(this, configKey);
			this.Updated = null;
		}

		~RemoteConfigSettings()
		{
			Destroy();
		}

		private void Destroy()
		{
			if (m_Ptr != IntPtr.Zero)
			{
				Internal_Destroy(m_Ptr);
				m_Ptr = IntPtr.Zero;
			}
		}

		public void Dispose()
		{
			Destroy();
			GC.SuppressFinalize(this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr Internal_Create(RemoteConfigSettings rcs, string configKey);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		internal static extern void Internal_Destroy(IntPtr ptr);

		[RequiredByNativeCode]
		internal static void RemoteConfigSettingsUpdated(RemoteConfigSettings rcs, bool wasLastUpdatedFromServer)
		{
			rcs.Updated?.Invoke(wasLastUpdatedFromServer);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool QueueConfig(string name, object param, int ver = 1, string prefix = "");

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool SendDeviceInfoInConfigRequest();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void AddSessionTag(string tag);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ForceUpdate();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool WasLastUpdatedFromServer();

		[ExcludeFromDocs]
		public int GetInt(string key)
		{
			return GetInt(key, 0);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetInt(string key, [UnityEngine.Internal.DefaultValue("0")] int defaultValue);

		[ExcludeFromDocs]
		public long GetLong(string key)
		{
			return GetLong(key, 0L);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern long GetLong(string key, [UnityEngine.Internal.DefaultValue("0")] long defaultValue);

		[ExcludeFromDocs]
		public float GetFloat(string key)
		{
			return GetFloat(key, 0f);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetFloat(string key, [UnityEngine.Internal.DefaultValue("0.0F")] float defaultValue);

		[ExcludeFromDocs]
		public string GetString(string key)
		{
			return GetString(key, "");
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetString(string key, [UnityEngine.Internal.DefaultValue("\"\"")] string defaultValue);

		[ExcludeFromDocs]
		public bool GetBool(string key)
		{
			return GetBool(key, defaultValue: false);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetBool(string key, [UnityEngine.Internal.DefaultValue("false")] bool defaultValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasKey(string key);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetCount();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string[] GetKeys();

		public T GetObject<T>(string key = "")
		{
			return (T)GetObject(typeof(T), key);
		}

		public object GetObject(Type type, string key = "")
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type.IsAbstract || type.IsSubclassOf(typeof(Object)))
			{
				throw new ArgumentException("Cannot deserialize to new instances of type '" + type.Name + ".'");
			}
			return GetAsScriptingObject(type, null, key);
		}

		public object GetObject(string key, object defaultValue)
		{
			if (defaultValue == null)
			{
				throw new ArgumentNullException("defaultValue");
			}
			Type type = defaultValue.GetType();
			if (type.IsAbstract || type.IsSubclassOf(typeof(Object)))
			{
				throw new ArgumentException("Cannot deserialize to new instances of type '" + type.Name + ".'");
			}
			return GetAsScriptingObject(type, defaultValue, key);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern object GetAsScriptingObject(Type t, object defaultValue, string key);

		public IDictionary<string, object> GetDictionary(string key = "")
		{
			UseSafeLock();
			IDictionary<string, object> dictionary = RemoteConfigSettingsHelper.GetDictionary(GetSafeTopMap(), key);
			ReleaseSafeLock();
			return dictionary;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void UseSafeLock();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void ReleaseSafeLock();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetSafeTopMap();
	}
	internal static class RemoteConfigSettingsHelper
	{
		[RequiredByNativeCode]
		internal enum Tag
		{
			kUnknown,
			kIntVal,
			kInt64Val,
			kUInt64Val,
			kDoubleVal,
			kBoolVal,
			kStringVal,
			kArrayVal,
			kMixedArrayVal,
			kMapVal,
			kMaxTags
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetSafeMap(IntPtr m, string key);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string[] GetSafeMapKeys(IntPtr m);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Tag[] GetSafeMapTypes(IntPtr m);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long GetSafeNumber(IntPtr m, string key, long defaultValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern float GetSafeFloat(IntPtr m, string key, float defaultValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetSafeBool(IntPtr m, string key, bool defaultValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetSafeStringValue(IntPtr m, string key, string defaultValue);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetSafeArray(IntPtr m, string key);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long GetSafeArraySize(IntPtr a);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetSafeArrayArray(IntPtr a, long i);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetSafeArrayMap(IntPtr a, long i);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Tag GetSafeArrayType(IntPtr a, long i);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long GetSafeNumberArray(IntPtr a, long i);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern float GetSafeArrayFloat(IntPtr a, long i);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetSafeArrayBool(IntPtr a, long i);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetSafeArrayStringValue(IntPtr a, long i);

		public static IDictionary<string, object> GetDictionary(IntPtr m, string key)
		{
			if (m == IntPtr.Zero)
			{
				return null;
			}
			if (!string.IsNullOrEmpty(key))
			{
				m = GetSafeMap(m, key);
				if (m == IntPtr.Zero)
				{
					return null;
				}
			}
			return GetDictionary(m);
		}

		internal static IDictionary<string, object> GetDictionary(IntPtr m)
		{
			if (m == IntPtr.Zero)
			{
				return null;
			}
			IDictionary<string, object> dictionary = new Dictionary<string, object>();
			Tag[] safeMapTypes = GetSafeMapTypes(m);
			string[] safeMapKeys = GetSafeMapKeys(m);
			for (int i = 0; i < safeMapKeys.Length; i++)
			{
				SetDictKeyType(m, dictionary, safeMapKeys[i], safeMapTypes[i]);
			}
			return dictionary;
		}

		internal static object GetArrayArrayEntries(IntPtr a, long i)
		{
			return GetArrayEntries(GetSafeArrayArray(a, i));
		}

		internal static IDictionary<string, object> GetArrayMapEntries(IntPtr a, long i)
		{
			return GetDictionary(GetSafeArrayMap(a, i));
		}

		internal static T[] GetArrayEntriesType<T>(IntPtr a, long size, Func<IntPtr, long, T> f)
		{
			T[] array = new T[size];
			for (long num = 0L; num < size; num++)
			{
				array[num] = f(a, num);
			}
			return array;
		}

		internal static object GetArrayEntries(IntPtr a)
		{
			long safeArraySize = GetSafeArraySize(a);
			if (safeArraySize == 0)
			{
				return null;
			}
			switch (GetSafeArrayType(a, 0L))
			{
			case Tag.kIntVal:
			case Tag.kInt64Val:
				return GetArrayEntriesType(a, safeArraySize, GetSafeNumberArray);
			case Tag.kDoubleVal:
				return GetArrayEntriesType(a, safeArraySize, GetSafeArrayFloat);
			case Tag.kBoolVal:
				return GetArrayEntriesType(a, safeArraySize, GetSafeArrayBool);
			case Tag.kStringVal:
				return GetArrayEntriesType(a, safeArraySize, GetSafeArrayStringValue);
			case Tag.kArrayVal:
				return GetArrayEntriesType(a, safeArraySize, GetArrayArrayEntries);
			case Tag.kMapVal:
				return GetArrayEntriesType(a, safeArraySize, GetArrayMapEntries);
			default:
				return null;
			}
		}

		internal static object GetMixedArrayEntries(IntPtr a)
		{
			long safeArraySize = GetSafeArraySize(a);
			if (safeArraySize == 0)
			{
				return null;
			}
			object[] array = new object[safeArraySize];
			for (long num = 0L; num < safeArraySize; num++)
			{
				switch (GetSafeArrayType(a, num))
				{
				case Tag.kIntVal:
				case Tag.kInt64Val:
					array[num] = GetSafeNumberArray(a, num);
					break;
				case Tag.kDoubleVal:
					array[num] = GetSafeArrayFloat(a, num);
					break;
				case Tag.kBoolVal:
					array[num] = GetSafeArrayBool(a, num);
					break;
				case Tag.kStringVal:
					array[num] = GetSafeArrayStringValue(a, num);
					break;
				case Tag.kArrayVal:
					array[num] = GetArrayArrayEntries(a, num);
					break;
				case Tag.kMapVal:
					array[num] = GetArrayMapEntries(a, num);
					break;
				}
			}
			return array;
		}

		internal static void SetDictKeyType(IntPtr m, IDictionary<string, object> dict, string key, Tag tag)
		{
			switch (tag)
			{
			case Tag.kIntVal:
			case Tag.kInt64Val:
				dict[key] = GetSafeNumber(m, key, 0L);
				break;
			case Tag.kDoubleVal:
				dict[key] = GetSafeFloat(m, key, 0f);
				break;
			case Tag.kBoolVal:
				dict[key] = GetSafeBool(m, key, defaultValue: false);
				break;
			case Tag.kStringVal:
				dict[key] = GetSafeStringValue(m, key, "");
				break;
			case Tag.kArrayVal:
				dict[key] = GetArrayEntries(GetSafeArray(m, key));
				break;
			case Tag.kMixedArrayVal:
				dict[key] = GetMixedArrayEntries(GetSafeArray(m, key));
				break;
			case Tag.kMapVal:
				dict[key] = GetDictionary(GetSafeMap(m, key));
				break;
			case Tag.kUInt64Val:
				break;
			}
		}
	}
}
namespace UnityEngine.Analytics
{
	[ExcludeFromDocs]
	[NativeHeader("Modules/UnityAnalytics/ContinuousEvent/Manager.h")]
	[NativeHeader("Modules/UnityAnalytics/Public/UnityAnalytics.h")]
	[RequiredByNativeCode]
	public class ContinuousEvent
	{
		public static AnalyticsResult RegisterCollector<T>(string metricName, Func<T> del) where T : struct, IComparable<T>, IEquatable<T>
		{
			if (string.IsNullOrEmpty(metricName))
			{
				throw new ArgumentException("Cannot set metric name to an empty or null string");
			}
			if (!IsInitialized())
			{
				return AnalyticsResult.NotInitialized;
			}
			return InternalRegisterCollector(typeof(T).ToString(), metricName, del);
		}

		public static AnalyticsResult SetEventHistogramThresholds<T>(string eventName, int count, T[] data, int ver = 1, string prefix = "") where T : struct, IComparable<T>, IEquatable<T>
		{
			if (string.IsNullOrEmpty(eventName))
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			if (!IsInitialized())
			{
				return AnalyticsResult.NotInitialized;
			}
			return InternalSetEventHistogramThresholds(typeof(T).ToString(), eventName, count, data, ver, prefix);
		}

		public static AnalyticsResult SetCustomEventHistogramThresholds<T>(string eventName, int count, T[] data) where T : struct, IComparable<T>, IEquatable<T>
		{
			if (string.IsNullOrEmpty(eventName))
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			if (!IsInitialized())
			{
				return AnalyticsResult.NotInitialized;
			}
			return InternalSetCustomEventHistogramThresholds(typeof(T).ToString(), eventName, count, data);
		}

		public static AnalyticsResult ConfigureCustomEvent(string customEventName, string metricName, float interval, float period, bool enabled = true)
		{
			if (string.IsNullOrEmpty(customEventName))
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			if (!IsInitialized())
			{
				return AnalyticsResult.NotInitialized;
			}
			return InternalConfigureCustomEvent(customEventName, metricName, interval, period, enabled);
		}

		public static AnalyticsResult ConfigureEvent(string eventName, string metricName, float interval, float period, bool enabled = true, int ver = 1, string prefix = "")
		{
			if (string.IsNullOrEmpty(eventName))
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			if (!IsInitialized())
			{
				return AnalyticsResult.NotInitialized;
			}
			return InternalConfigureEvent(eventName, metricName, interval, period, enabled, ver, prefix);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("::GetUnityAnalytics().GetContinuousEventManager()", StaticAccessorType.Dot)]
		private static extern AnalyticsResult InternalRegisterCollector(string type, string metricName, object collector);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("::GetUnityAnalytics().GetContinuousEventManager()", StaticAccessorType.Dot)]
		private static extern AnalyticsResult InternalSetEventHistogramThresholds(string type, string eventName, int count, object data, int ver, string prefix);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("::GetUnityAnalytics().GetContinuousEventManager()", StaticAccessorType.Dot)]
		private static extern AnalyticsResult InternalSetCustomEventHistogramThresholds(string type, string eventName, int count, object data);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("::GetUnityAnalytics().GetContinuousEventManager()", StaticAccessorType.Dot)]
		private static extern AnalyticsResult InternalConfigureCustomEvent(string customEventName, string metricName, float interval, float period, bool enabled);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("::GetUnityAnalytics().GetContinuousEventManager()", StaticAccessorType.Dot)]
		private static extern AnalyticsResult InternalConfigureEvent(string eventName, string metricName, float interval, float period, bool enabled, int ver, string prefix);

		internal static bool IsInitialized()
		{
			return Analytics.IsInitialized();
		}
	}
	[RequiredByNativeCode]
	public enum AnalyticsSessionState
	{
		kSessionStopped,
		kSessionStarted,
		kSessionPaused,
		kSessionResumed
	}
	[RequiredByNativeCode]
	[NativeHeader("UnityAnalyticsScriptingClasses.h")]
	[NativeHeader("Modules/UnityAnalytics/Public/UnityAnalytics.h")]
	public static class AnalyticsSessionInfo
	{
		public delegate void SessionStateChanged(AnalyticsSessionState sessionState, long sessionId, long sessionElapsedTime, bool sessionChanged);

		public delegate void IdentityTokenChanged(string token);

		public static extern AnalyticsSessionState sessionState
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetPlayerSessionState")]
			get;
		}

		public static extern long sessionId
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetPlayerSessionId")]
			get;
		}

		public static extern long sessionCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetPlayerSessionCount")]
			get;
		}

		public static extern long sessionElapsedTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetPlayerSessionElapsedTime")]
			get;
		}

		public static extern bool sessionFirstRun
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetPlayerSessionFirstRun", false, true)]
			get;
		}

		public static extern string userId
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetUserId")]
			get;
		}

		public static string customUserId
		{
			get
			{
				if (!Analytics.IsInitialized())
				{
					return null;
				}
				return customUserIdInternal;
			}
			set
			{
				if (Analytics.IsInitialized())
				{
					customUserIdInternal = value;
				}
			}
		}

		public static string customDeviceId
		{
			get
			{
				if (!Analytics.IsInitialized())
				{
					return null;
				}
				return customDeviceIdInternal;
			}
			set
			{
				if (Analytics.IsInitialized())
				{
					customDeviceIdInternal = value;
				}
			}
		}

		public static string identityToken
		{
			get
			{
				if (!Analytics.IsInitialized())
				{
					return null;
				}
				return identityTokenInternal;
			}
		}

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern string identityTokenInternal
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetIdentityToken")]
			get;
		}

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern string customUserIdInternal
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetCustomUserId")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("SetCustomUserId")]
			set;
		}

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern string customDeviceIdInternal
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetCustomDeviceId")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("SetCustomDeviceId")]
			set;
		}

		public static event SessionStateChanged sessionStateChanged;

		public static event IdentityTokenChanged identityTokenChanged;

		[RequiredByNativeCode]
		internal static void CallSessionStateChanged(AnalyticsSessionState sessionState, long sessionId, long sessionElapsedTime, bool sessionChanged)
		{
			AnalyticsSessionInfo.sessionStateChanged?.Invoke(sessionState, sessionId, sessionElapsedTime, sessionChanged);
		}

		[RequiredByNativeCode]
		internal static void CallIdentityTokenChanged(string token)
		{
			AnalyticsSessionInfo.identityTokenChanged?.Invoke(token);
		}
	}
	[StructLayout(LayoutKind.Sequential)]
	[NativeHeader("Modules/UnityAnalytics/Public/Events/UserCustomEvent.h")]
	internal class CustomEventData : IDisposable
	{
		[NonSerialized]
		internal IntPtr m_Ptr;

		private CustomEventData()
		{
		}

		public CustomEventData(string name)
		{
			m_Ptr = Internal_Create(this, name);
		}

		~CustomEventData()
		{
			Destroy();
		}

		private void Destroy()
		{
			if (m_Ptr != IntPtr.Zero)
			{
				Internal_Destroy(m_Ptr);
				m_Ptr = IntPtr.Zero;
			}
		}

		public void Dispose()
		{
			Destroy();
			GC.SuppressFinalize(this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr Internal_Create(CustomEventData ced, string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		internal static extern void Internal_Destroy(IntPtr ptr);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool AddString(string key, string value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool AddInt32(string key, int value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool AddUInt32(string key, uint value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool AddInt64(string key, long value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool AddUInt64(string key, ulong value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool AddBool(string key, bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool AddDouble(string key, double value);

		public bool AddDictionary(IDictionary<string, object> eventData)
		{
			foreach (KeyValuePair<string, object> eventDatum in eventData)
			{
				string key = eventDatum.Key;
				object value = eventDatum.Value;
				if (value == null)
				{
					AddString(key, "null");
					continue;
				}
				Type type = value.GetType();
				if (type == typeof(string))
				{
					AddString(key, (string)value);
					continue;
				}
				if (type == typeof(char))
				{
					AddString(key, char.ToString((char)value));
					continue;
				}
				if (type == typeof(sbyte))
				{
					AddInt32(key, (sbyte)value);
					continue;
				}
				if (type == typeof(byte))
				{
					AddInt32(key, (byte)value);
					continue;
				}
				if (type == typeof(short))
				{
					AddInt32(key, (short)value);
					continue;
				}
				if (type == typeof(ushort))
				{
					AddUInt32(key, (ushort)value);
					continue;
				}
				if (type == typeof(int))
				{
					AddInt32(key, (int)value);
					continue;
				}
				if (type == typeof(uint))
				{
					AddUInt32(eventDatum.Key, (uint)value);
					continue;
				}
				if (type == typeof(long))
				{
					AddInt64(key, (long)value);
					continue;
				}
				if (type == typeof(ulong))
				{
					AddUInt64(key, (ulong)value);
					continue;
				}
				if (type == typeof(bool))
				{
					AddBool(key, (bool)value);
					continue;
				}
				if (type == typeof(float))
				{
					AddDouble(key, (double)Convert.ToDecimal((float)value));
					continue;
				}
				if (type == typeof(double))
				{
					AddDouble(key, (double)value);
					continue;
				}
				if (type == typeof(decimal))
				{
					AddDouble(key, (double)Convert.ToDecimal((decimal)value));
					continue;
				}
				if (type.IsValueType)
				{
					AddString(key, value.ToString());
					continue;
				}
				throw new ArgumentException($"Invalid type: {type} passed");
			}
			return true;
		}
	}
	[StructLayout(LayoutKind.Sequential)]
	[NativeHeader("Modules/UnityAnalytics/Public/UnityAnalytics.h")]
	[NativeHeader("Modules/UnityConnect/UnityConnectSettings.h")]
	[NativeHeader("Modules/UnityAnalytics/Public/Events/UserCustomEvent.h")]
	public static class Analytics
	{
		public static bool initializeOnStartup
		{
			get
			{
				if (!IsInitialized())
				{
					return false;
				}
				return initializeOnStartupInternal;
			}
			set
			{
				if (IsInitialized())
				{
					initializeOnStartupInternal = value;
				}
			}
		}

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern bool initializeOnStartupInternal
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetInitializeOnStartup")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("SetInitializeOnStartup")]
			set;
		}

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern bool enabledInternal
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetEnabled")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("SetEnabled")]
			set;
		}

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern bool playerOptedOutInternal
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetPlayerOptedOut")]
			get;
		}

		[StaticAccessor("GetUnityConnectSettings()", StaticAccessorType.Dot)]
		private static extern string eventUrlInternal
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetEventUrl")]
			get;
		}

		[StaticAccessor("GetUnityConnectSettings()", StaticAccessorType.Dot)]
		private static extern string configUrlInternal
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetConfigUrl")]
			get;
		}

		[StaticAccessor("GetUnityConnectSettings()", StaticAccessorType.Dot)]
		private static extern string dashboardUrlInternal
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetDashboardUrl")]
			get;
		}

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern bool limitUserTrackingInternal
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetLimitUserTracking")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("SetLimitUserTracking")]
			set;
		}

		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern bool deviceStatsEnabledInternal
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetDeviceStatsEnabled")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("SetDeviceStatsEnabled")]
			set;
		}

		public static bool playerOptedOut
		{
			get
			{
				if (!IsInitialized())
				{
					return false;
				}
				return playerOptedOutInternal;
			}
		}

		public static string eventUrl
		{
			get
			{
				if (!IsInitialized())
				{
					return string.Empty;
				}
				return eventUrlInternal;
			}
		}

		public static string dashboardUrl
		{
			get
			{
				if (!IsInitialized())
				{
					return string.Empty;
				}
				return dashboardUrlInternal;
			}
		}

		public static string configUrl
		{
			get
			{
				if (!IsInitialized())
				{
					return string.Empty;
				}
				return configUrlInternal;
			}
		}

		public static bool limitUserTracking
		{
			get
			{
				if (!IsInitialized())
				{
					return false;
				}
				return limitUserTrackingInternal;
			}
			set
			{
				if (IsInitialized())
				{
					limitUserTrackingInternal = value;
				}
			}
		}

		public static bool deviceStatsEnabled
		{
			get
			{
				if (!IsInitialized())
				{
					return false;
				}
				return deviceStatsEnabledInternal;
			}
			set
			{
				if (IsInitialized())
				{
					deviceStatsEnabledInternal = value;
				}
			}
		}

		public static bool enabled
		{
			get
			{
				if (!IsInitialized())
				{
					return false;
				}
				return enabledInternal;
			}
			set
			{
				if (IsInitialized())
				{
					enabledInternal = value;
				}
			}
		}

		public static AnalyticsResult ResumeInitialization()
		{
			if (!IsInitialized())
			{
				return AnalyticsResult.NotInitialized;
			}
			return ResumeInitializationInternal();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("ResumeInitialization")]
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern AnalyticsResult ResumeInitializationInternal();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		internal static extern bool IsInitialized();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("FlushEvents")]
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern bool FlushArchivedEvents();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern AnalyticsResult Transaction(string productId, double amount, string currency, string receiptPurchaseData, string signature, bool usingIAPService);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern AnalyticsResult SendCustomEventName(string customEventName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		private static extern AnalyticsResult SendCustomEvent(CustomEventData eventData);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		internal static extern AnalyticsResult IsCustomEventWithLimitEnabled(string customEventName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		internal static extern AnalyticsResult EnableCustomEventWithLimit(string customEventName, bool enable);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		internal static extern AnalyticsResult IsEventWithLimitEnabled(string eventName, int ver, string prefix);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		internal static extern AnalyticsResult EnableEventWithLimit(string eventName, bool enable, int ver, string prefix);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		internal static extern AnalyticsResult RegisterEventWithLimit(string eventName, int maxEventPerHour, int maxItems, string vendorKey, int ver, string prefix, string assemblyInfo, bool notifyServer);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		internal static extern AnalyticsResult RegisterEventsWithLimit(string[] eventName, int maxEventPerHour, int maxItems, string vendorKey, int ver, string prefix, string assemblyInfo, bool notifyServer);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		[ThreadSafe]
		internal static extern AnalyticsResult SendEventWithLimit(string eventName, object parameters, int ver, string prefix);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		[ThreadSafe]
		internal static extern AnalyticsResult SetEventWithLimitEndPoint(string eventName, string endPoint, int ver, string prefix);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		[ThreadSafe]
		internal static extern AnalyticsResult SetEventWithLimitPriority(string eventName, AnalyticsEventPriority eventPriority, int ver, string prefix);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		[StaticAccessor("GetUnityAnalytics()", StaticAccessorType.Dot)]
		internal static extern bool QueueEvent(string eventName, object parameters, int ver, string prefix);

		public static AnalyticsResult FlushEvents()
		{
			if (!IsInitialized())
			{
				return AnalyticsResult.NotInitialized;
			}
			return (!FlushArchivedEvents()) ? AnalyticsResult.NotInitialized : AnalyticsResult.Ok;
		}

		[Obsolete("SetUserId is no longer supported", true)]
		public static AnalyticsResult SetUserId(string userId)
		{
			if (string.IsNullOrEmpty(userId))
			{
				throw new ArgumentException("Cannot set userId to an empty or null string");
			}
			return AnalyticsResult.InvalidData;
		}

		[Obsolete("SetUserGender is no longer supported", true)]
		public static AnalyticsResult SetUserGender(Gender gender)
		{
			return AnalyticsResult.InvalidData;
		}

		[Obsolete("SetUserBirthYear is no longer supported", true)]
		public static AnalyticsResult SetUserBirthYear(int birthYear)
		{
			return AnalyticsResult.InvalidData;
		}

		[Obsolete("SendUserInfoEvent is no longer supported", true)]
		private static AnalyticsResult SendUserInfoEvent(object param)
		{
			return AnalyticsResult.InvalidData;
		}

		public static AnalyticsResult Transaction(string productId, decimal amount, string currency)
		{
			return Transaction(productId, amount, currency, null, null, usingIAPService: false);
		}

		public static AnalyticsResult Transaction(string productId, decimal amount, string currency, string receiptPurchaseData, string signature)
		{
			return Transaction(productId, amount, currency, receiptPurchaseData, signature, usingIAPService: false);
		}

		public static AnalyticsResult Transaction(string productId, decimal amount, string currency, string receiptPurchaseData, string signature, bool usingIAPService)
		{
			if (string.IsNullOrEmpty(productId))
			{
				throw new ArgumentException("Cannot set productId to an empty or null string");
			}
			if (string.IsNullOrEmpty(currency))
			{
				throw new ArgumentException("Cannot set currency to an empty or null string");
			}
			if (!IsInitialized())
			{
				return AnalyticsResult.NotInitialized;
			}
			if (receiptPurchaseData == null)
			{
				receiptPurchaseData = string.Empty;
			}
			if (signature == null)
			{
				signature = string.Empty;
			}
			return Transaction(productId, Convert.ToDouble(amount), currency, receiptPurchaseData, signature, usingIAPService);
		}

		public static AnalyticsResult CustomEvent(string customEventName)
		{
			if (string.IsNullOrEmpty(customEventName))
			{
				throw new ArgumentException("Cannot set custom event name to an empty or null string");
			}
			if (!IsInitialized())
			{
				return AnalyticsResult.NotInitialized;
			}
			return SendCustomEventName(customEventName);
		}

		public static AnalyticsResult CustomEvent(string customEventName, Vector3 position)
		{
			if (string.IsNullOrEmpty(customEventName))
			{
				throw new ArgumentException("Cannot set custom event name to an empty or null string");
			}
			if (!IsInitialized())
			{
				return AnalyticsResult.NotInitialized;
			}
			CustomEventData customEventData = new CustomEventData(customEventName);
			customEventData.AddDouble("x", (double)Convert.ToDecimal(position.x));
			customEventData.AddDouble("y", (double)Convert.ToDecimal(position.y));
			customEventData.AddDouble("z", (double)Convert.ToDecimal(position.z));
			AnalyticsResult result = SendCustomEvent(customEventData);
			customEventData.Dispose();
			return result;
		}

		public static AnalyticsResult CustomEvent(string customEventName, IDictionary<string, object> eventData)
		{
			if (string.IsNullOrEmpty(customEventName))
			{
				throw new ArgumentException("Cannot set custom event name to an empty or null string");
			}
			if (!IsInitialized())
			{
				return AnalyticsResult.NotInitialized;
			}
			if (eventData == null)
			{
				return SendCustomEventName(customEventName);
			}
			CustomEventData customEventData = new CustomEventData(customEventName);
			AnalyticsResult result = AnalyticsResult.InvalidData;
			try
			{
				customEventData.AddDictionary(eventData);
				result = SendCustomEvent(customEventData);
			}
			finally
			{
				customEventData.Dispose();
			}
			return result;
		}

		public static AnalyticsResult EnableCustomEvent(string customEventName, bool enabled)
		{
			if (string.IsNullOrEmpty(customEventName))
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			if (!IsInitialized())
			{
				return AnalyticsResult.NotInitialized;
			}
			return EnableCustomEventWithLimit(customEventName, enabled);
		}

		public static AnalyticsResult IsCustomEventEnabled(string customEventName)
		{
			if (string.IsNullOrEmpty(customEventName))
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			if (!IsInitialized())
			{
				return AnalyticsResult.NotInitialized;
			}
			return IsCustomEventWithLimitEnabled(customEventName);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static AnalyticsResult RegisterEvent(string eventName, int maxEventPerHour, int maxItems, string vendorKey = "", string prefix = "")
		{
			string empty = string.Empty;
			empty = Assembly.GetCallingAssembly().FullName;
			return RegisterEvent(eventName, maxEventPerHour, maxItems, vendorKey, 1, prefix, empty);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public static AnalyticsResult RegisterEvent(string eventName, int maxEventPerHour, int maxItems, string vendorKey, int ver, string prefix = "")
		{
			string empty = string.Empty;
			empty = Assembly.GetCallingAssembly().FullName;
			return RegisterEvent(eventName, maxEventPerHour, maxItems, vendorKey, ver, prefix, empty);
		}

		private static AnalyticsResult RegisterEvent(string eventName, int maxEventPerHour, int maxItems, string vendorKey, int ver, string prefix, string assemblyInfo)
		{
			if (string.IsNullOrEmpty(eventName))
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			if (!IsInitialized())
			{
				return AnalyticsResult.NotInitialized;
			}
			return RegisterEventWithLimit(eventName, maxEventPerHour, maxItems, vendorKey, ver, prefix, assemblyInfo, notifyServer: true);
		}

		public static AnalyticsResult SendEvent(string eventName, object parameters, int ver = 1, string prefix = "")
		{
			if (string.IsNullOrEmpty(eventName))
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			if (parameters == null)
			{
				throw new ArgumentException("Cannot set parameters to null");
			}
			if (!IsInitialized())
			{
				return AnalyticsResult.NotInitialized;
			}
			return SendEventWithLimit(eventName, parameters, ver, prefix);
		}

		public static AnalyticsResult SetEventEndPoint(string eventName, string endPoint, int ver = 1, string prefix = "")
		{
			if (string.IsNullOrEmpty(eventName))
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			if (endPoint == null)
			{
				throw new ArgumentException("Cannot set parameters to null");
			}
			if (!IsInitialized())
			{
				return AnalyticsResult.NotInitialized;
			}
			return SetEventWithLimitEndPoint(eventName, endPoint, ver, prefix);
		}

		public static AnalyticsResult SetEventPriority(string eventName, AnalyticsEventPriority eventPriority, int ver = 1, string prefix = "")
		{
			if (string.IsNullOrEmpty(eventName))
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			if (!IsInitialized())
			{
				return AnalyticsResult.NotInitialized;
			}
			return SetEventWithLimitPriority(eventName, eventPriority, ver, prefix);
		}

		public static AnalyticsResult EnableEvent(string eventName, bool enabled, int ver = 1, string prefix = "")
		{
			if (string.IsNullOrEmpty(eventName))
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			if (!IsInitialized())
			{
				return AnalyticsResult.NotInitialized;
			}
			return EnableEventWithLimit(eventName, enabled, ver, prefix);
		}

		public static AnalyticsResult IsEventEnabled(string eventName, int ver = 1, string prefix = "")
		{
			if (string.IsNullOrEmpty(eventName))
			{
				throw new ArgumentException("Cannot set event name to an empty or null string");
			}
			if (!IsInitialized())
			{
				return AnalyticsResult.NotInitialized;
			}
			return IsEventWithLimitEnabled(eventName, ver, prefix);
		}
	}
	public enum Gender
	{
		Male,
		Female,
		Unknown
	}
	public enum AnalyticsResult
	{
		Ok,
		NotInitialized,
		AnalyticsDisabled,
		TooManyItems,
		SizeLimitReached,
		TooManyRequests,
		InvalidData,
		UnsupportedPlatform
	}
	[Flags]
	public enum AnalyticsEventPriority
	{
		FlushQueueFlag = 1,
		CacheImmediatelyFlag = 2,
		AllowInStopModeFlag = 4,
		SendImmediateFlag = 8,
		NoCachingFlag = 0x10,
		NoRetryFlag = 0x20,
		NormalPriorityEvent = 0,
		NormalPriorityEvent_WithCaching = 2,
		NormalPriorityEvent_NoRetryNoCaching = 0x30,
		HighPriorityEvent = 1,
		HighPriorityEvent_InStopMode = 5,
		HighestPriorityEvent = 9,
		HighestPriorityEvent_NoRetryNoCaching = 0x31
	}
}
