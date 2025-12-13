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
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngineInternal;

[assembly: InternalsVisibleTo("UnityEngine.IMGUIModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommon")]
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
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
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework.Tests")]
[assembly: InternalsVisibleTo("UnityEngine.WindModule")]
[assembly: InternalsVisibleTo("Unity.PerformanceTests.RuntimeTestRunner.Tests")]
[assembly: InternalsVisibleTo("UnityEngine.VirtualTexturingModule")]
[assembly: InternalsVisibleTo("UnityEngine.VehiclesModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.VideoModule")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.AllIn1Runner")]
[assembly: InternalsVisibleTo("Unity.Timeline")]
[assembly: InternalsVisibleTo("Assembly-CSharp-testable")]
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
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.017")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.018")]
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
[assembly: UnityEngineModuleAssembly]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.019")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.002")]
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
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.001")]
[assembly: InternalsVisibleTo("UnityEngine.NVIDIAModule")]
[assembly: InternalsVisibleTo("UnityEngine.LocalizationModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreTextEngineModule")]
[assembly: InternalsVisibleTo("Assembly-CSharp-Editor-testable")]
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.HotReloadModule")]
[assembly: InternalsVisibleTo("UnityEngine.AnimationModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.AndroidJNIModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterRendererModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreFontEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputLegacyModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
[assembly: InternalsVisibleTo("UnityEngine.GridModule")]
[assembly: InternalsVisibleTo("UnityEngine.GameCenterModule")]
[assembly: InternalsVisibleTo("UnityEngine.ImageConversionModule")]
[assembly: InternalsVisibleTo("UnityEngine.GIModule")]
[assembly: InternalsVisibleTo("UnityEngine.DirectorModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextRenderingModule")]
[assembly: InternalsVisibleTo("UnityEngine.CrashReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestModule")]
[assembly: InternalsVisibleTo("UnityEngine.DSPGraphModule")]
[assembly: InternalsVisibleTo("UnityEngine.TLSModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommonModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityConnectModule")]
[assembly: InternalsVisibleTo("UnityEngine.ContentLoadModule")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace UnityEngine
{
	public enum AssetBundleLoadResult
	{
		Success,
		Cancelled,
		NotMatchingCrc,
		FailedCache,
		NotValidAssetBundle,
		NoSerializedData,
		NotCompatible,
		AlreadyLoaded,
		FailedRead,
		FailedDecompression,
		FailedWrite,
		FailedDeleteRecompressionTarget,
		RecompressionTargetIsLoaded,
		RecompressionTargetExistsButNotArchive
	}
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadFromMemoryAsyncOperation.h")]
	[ExcludeFromPreset]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadAssetOperation.h")]
	[NativeHeader("Runtime/Scripting/ScriptingExportUtility.h")]
	[NativeHeader("Runtime/Scripting/ScriptingObjectWithIntPtrField.h")]
	[NativeHeader("Runtime/Scripting/ScriptingUtility.h")]
	[NativeHeader("AssetBundleScriptingClasses.h")]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleSaveAndLoadHelper.h")]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleUtility.h")]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadAssetUtility.h")]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadFromFileAsyncOperation.h")]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadFromManagedStreamAsyncOperation.h")]
	public class AssetBundle : Object
	{
		[Obsolete("mainAsset has been made obsolete. Please use the new AssetBundle build system introduced in 5.0 and check BuildAssetBundles documentation for details.")]
		public Object mainAsset => returnMainAsset(this);

		public extern bool isStreamedSceneAssetBundle
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetIsStreamedSceneAssetBundle")]
			get;
		}

		public static uint memoryBudgetKB
		{
			get
			{
				return AssetBundleLoadingCache.memoryBudgetKB;
			}
			set
			{
				AssetBundleLoadingCache.memoryBudgetKB = value;
			}
		}

		private AssetBundle()
		{
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("LoadMainObjectFromAssetBundle", true)]
		internal static extern Object returnMainAsset([NotNull("NullExceptionObject")] AssetBundle bundle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("UnloadAllAssetBundles")]
		public static extern void UnloadAllAssetBundles(bool unloadAllObjects);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GetAllAssetBundles")]
		internal static extern AssetBundle[] GetAllLoadedAssetBundles_Native();

		public static IEnumerable<AssetBundle> GetAllLoadedAssetBundles()
		{
			return GetAllLoadedAssetBundles_Native();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("LoadFromFileAsync")]
		internal static extern AssetBundleCreateRequest LoadFromFileAsync_Internal(string path, uint crc, ulong offset);

		public static AssetBundleCreateRequest LoadFromFileAsync(string path)
		{
			return LoadFromFileAsync_Internal(path, 0u, 0uL);
		}

		public static AssetBundleCreateRequest LoadFromFileAsync(string path, uint crc)
		{
			return LoadFromFileAsync_Internal(path, crc, 0uL);
		}

		public static AssetBundleCreateRequest LoadFromFileAsync(string path, uint crc, ulong offset)
		{
			return LoadFromFileAsync_Internal(path, crc, offset);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("LoadFromFile")]
		internal static extern AssetBundle LoadFromFile_Internal(string path, uint crc, ulong offset);

		public static AssetBundle LoadFromFile(string path)
		{
			return LoadFromFile_Internal(path, 0u, 0uL);
		}

		public static AssetBundle LoadFromFile(string path, uint crc)
		{
			return LoadFromFile_Internal(path, crc, 0uL);
		}

		public static AssetBundle LoadFromFile(string path, uint crc, ulong offset)
		{
			return LoadFromFile_Internal(path, crc, offset);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("LoadFromMemoryAsync")]
		internal static extern AssetBundleCreateRequest LoadFromMemoryAsync_Internal(byte[] binary, uint crc);

		public static AssetBundleCreateRequest LoadFromMemoryAsync(byte[] binary)
		{
			return LoadFromMemoryAsync_Internal(binary, 0u);
		}

		public static AssetBundleCreateRequest LoadFromMemoryAsync(byte[] binary, uint crc)
		{
			return LoadFromMemoryAsync_Internal(binary, crc);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("LoadFromMemory")]
		internal static extern AssetBundle LoadFromMemory_Internal(byte[] binary, uint crc);

		public static AssetBundle LoadFromMemory(byte[] binary)
		{
			return LoadFromMemory_Internal(binary, 0u);
		}

		public static AssetBundle LoadFromMemory(byte[] binary, uint crc)
		{
			return LoadFromMemory_Internal(binary, crc);
		}

		internal static void ValidateLoadFromStream(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("ManagedStream object must be non-null", "stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException("ManagedStream object must be readable (stream.CanRead must return true)", "stream");
			}
			if (!stream.CanSeek)
			{
				throw new ArgumentException("ManagedStream object must be seekable (stream.CanSeek must return true)", "stream");
			}
		}

		public static AssetBundleCreateRequest LoadFromStreamAsync(Stream stream, uint crc, uint managedReadBufferSize)
		{
			ValidateLoadFromStream(stream);
			return LoadFromStreamAsyncInternal(stream, crc, managedReadBufferSize);
		}

		public static AssetBundleCreateRequest LoadFromStreamAsync(Stream stream, uint crc)
		{
			ValidateLoadFromStream(stream);
			return LoadFromStreamAsyncInternal(stream, crc, 0u);
		}

		public static AssetBundleCreateRequest LoadFromStreamAsync(Stream stream)
		{
			ValidateLoadFromStream(stream);
			return LoadFromStreamAsyncInternal(stream, 0u, 0u);
		}

		public static AssetBundle LoadFromStream(Stream stream, uint crc, uint managedReadBufferSize)
		{
			ValidateLoadFromStream(stream);
			return LoadFromStreamInternal(stream, crc, managedReadBufferSize);
		}

		public static AssetBundle LoadFromStream(Stream stream, uint crc)
		{
			ValidateLoadFromStream(stream);
			return LoadFromStreamInternal(stream, crc, 0u);
		}

		public static AssetBundle LoadFromStream(Stream stream)
		{
			ValidateLoadFromStream(stream);
			return LoadFromStreamInternal(stream, 0u, 0u);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("LoadFromStreamAsyncInternal")]
		internal static extern AssetBundleCreateRequest LoadFromStreamAsyncInternal(Stream stream, uint crc, uint managedReadBufferSize);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("LoadFromStreamInternal")]
		internal static extern AssetBundle LoadFromStreamInternal(Stream stream, uint crc, uint managedReadBufferSize);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("Contains")]
		public extern bool Contains(string name);

		[Obsolete("Method Load has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAsset instead and check the documentation for details.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Object Load(string name)
		{
			return null;
		}

		[Obsolete("Method Load has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAsset instead and check the documentation for details.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Object Load<T>(string name)
		{
			return null;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Method Load has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAsset instead and check the documentation for details.", true)]
		private Object Load(string name, Type type)
		{
			return null;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Method LoadAsync has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAssetAsync instead and check the documentation for details.", true)]
		private AssetBundleRequest LoadAsync(string name, Type type)
		{
			return null;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Method LoadAll has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAllAssets instead and check the documentation for details.", true)]
		private Object[] LoadAll(Type type)
		{
			return null;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Method LoadAll has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAllAssets instead and check the documentation for details.", true)]
		public Object[] LoadAll()
		{
			return null;
		}

		[Obsolete("Method LoadAll has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAllAssets instead and check the documentation for details.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public T[] LoadAll<T>() where T : Object
		{
			return null;
		}

		public Object LoadAsset(string name)
		{
			return LoadAsset(name, typeof(Object));
		}

		public T LoadAsset<T>(string name) where T : Object
		{
			return (T)LoadAsset(name, typeof(T));
		}

		[TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
		public Object LoadAsset(string name, Type type)
		{
			if (name == null)
			{
				throw new NullReferenceException("The input asset name cannot be null.");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("The input asset name cannot be empty.");
			}
			if (type == null)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return LoadAsset_Internal(name, type);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
		[NativeMethod("LoadAsset_Internal")]
		[NativeThrows]
		private extern Object LoadAsset_Internal(string name, Type type);

		public AssetBundleRequest LoadAssetAsync(string name)
		{
			return LoadAssetAsync(name, typeof(Object));
		}

		public AssetBundleRequest LoadAssetAsync<T>(string name)
		{
			return LoadAssetAsync(name, typeof(T));
		}

		public AssetBundleRequest LoadAssetAsync(string name, Type type)
		{
			if (name == null)
			{
				throw new NullReferenceException("The input asset name cannot be null.");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("The input asset name cannot be empty.");
			}
			if (type == null)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return LoadAssetAsync_Internal(name, type);
		}

		public Object[] LoadAssetWithSubAssets(string name)
		{
			return LoadAssetWithSubAssets(name, typeof(Object));
		}

		internal static T[] ConvertObjects<T>(Object[] rawObjects) where T : Object
		{
			if (rawObjects == null)
			{
				return null;
			}
			T[] array = new T[rawObjects.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (T)rawObjects[i];
			}
			return array;
		}

		public T[] LoadAssetWithSubAssets<T>(string name) where T : Object
		{
			return ConvertObjects<T>(LoadAssetWithSubAssets(name, typeof(T)));
		}

		public Object[] LoadAssetWithSubAssets(string name, Type type)
		{
			if (name == null)
			{
				throw new NullReferenceException("The input asset name cannot be null.");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("The input asset name cannot be empty.");
			}
			if (type == null)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return LoadAssetWithSubAssets_Internal(name, type);
		}

		public AssetBundleRequest LoadAssetWithSubAssetsAsync(string name)
		{
			return LoadAssetWithSubAssetsAsync(name, typeof(Object));
		}

		public AssetBundleRequest LoadAssetWithSubAssetsAsync<T>(string name)
		{
			return LoadAssetWithSubAssetsAsync(name, typeof(T));
		}

		public AssetBundleRequest LoadAssetWithSubAssetsAsync(string name, Type type)
		{
			if (name == null)
			{
				throw new NullReferenceException("The input asset name cannot be null.");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("The input asset name cannot be empty.");
			}
			if (type == null)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return LoadAssetWithSubAssetsAsync_Internal(name, type);
		}

		public Object[] LoadAllAssets()
		{
			return LoadAllAssets(typeof(Object));
		}

		public T[] LoadAllAssets<T>() where T : Object
		{
			return ConvertObjects<T>(LoadAllAssets(typeof(T)));
		}

		public Object[] LoadAllAssets(Type type)
		{
			if (type == null)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return LoadAssetWithSubAssets_Internal("", type);
		}

		public AssetBundleRequest LoadAllAssetsAsync()
		{
			return LoadAllAssetsAsync(typeof(Object));
		}

		public AssetBundleRequest LoadAllAssetsAsync<T>()
		{
			return LoadAllAssetsAsync(typeof(T));
		}

		public AssetBundleRequest LoadAllAssetsAsync(Type type)
		{
			if (type == null)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return LoadAssetWithSubAssetsAsync_Internal("", type);
		}

		[Obsolete("This method is deprecated.Use GetAllAssetNames() instead.", false)]
		public string[] AllAssetNames()
		{
			return GetAllAssetNames();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("LoadAssetAsync_Internal")]
		[NativeThrows]
		private extern AssetBundleRequest LoadAssetAsync_Internal(string name, Type type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("Unload")]
		[NativeThrows]
		public extern void Unload(bool unloadAllLoadedObjects);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("UnloadAsync")]
		[NativeThrows]
		public extern AssetBundleUnloadOperation UnloadAsync(bool unloadAllLoadedObjects);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetAllAssetNames")]
		public extern string[] GetAllAssetNames();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetAllScenePaths")]
		public extern string[] GetAllScenePaths();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		[NativeMethod("LoadAssetWithSubAssets_Internal")]
		internal extern Object[] LoadAssetWithSubAssets_Internal(string name, Type type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		[NativeMethod("LoadAssetWithSubAssetsAsync_Internal")]
		private extern AssetBundleRequest LoadAssetWithSubAssetsAsync_Internal(string name, Type type);

		public static AssetBundleRecompressOperation RecompressAssetBundleAsync(string inputPath, string outputPath, BuildCompression method, uint expectedCRC = 0u, ThreadPriority priority = ThreadPriority.Low)
		{
			return RecompressAssetBundleAsync_Internal(inputPath, outputPath, method, expectedCRC, priority);
		}

		[FreeFunction("RecompressAssetBundleAsync_Internal")]
		[NativeThrows]
		internal static AssetBundleRecompressOperation RecompressAssetBundleAsync_Internal(string inputPath, string outputPath, BuildCompression method, uint expectedCRC, ThreadPriority priority)
		{
			return RecompressAssetBundleAsync_Internal_Injected(inputPath, outputPath, ref method, expectedCRC, priority);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AssetBundleRecompressOperation RecompressAssetBundleAsync_Internal_Injected(string inputPath, string outputPath, ref BuildCompression method, uint expectedCRC, ThreadPriority priority);
	}
	[StructLayout(LayoutKind.Sequential)]
	[RequiredByNativeCode]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadFromAsyncOperation.h")]
	public class AssetBundleCreateRequest : AsyncOperation
	{
		public extern AssetBundle assetBundle
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetAssetBundleBlocking")]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("SetEnableCompatibilityChecks")]
		private extern void SetEnableCompatibilityChecks(bool set);

		internal void DisableCompatibilityChecks()
		{
			SetEnableCompatibilityChecks(set: false);
		}
	}
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadingCache.h")]
	[UsedByNativeCode]
	internal static class AssetBundleLoadingCache
	{
		internal const int kMinAllowedBlockCount = 2;

		internal const int kMinAllowedMaxBlocksPerFile = 2;

		internal static extern uint maxBlocksPerFile
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal static extern uint blockCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal static extern uint blockSize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal static uint memoryBudgetKB
		{
			get
			{
				return blockCount * blockSize;
			}
			set
			{
				uint num = Math.Max(value / blockSize, 2u);
				uint num2 = Math.Max(blockCount / 4, 2u);
				if (num != blockCount || num2 != maxBlocksPerFile)
				{
					blockCount = num;
					maxBlocksPerFile = num2;
				}
			}
		}
	}
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleManifest.h")]
	public class AssetBundleManifest : Object
	{
		private AssetBundleManifest()
		{
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetAllAssetBundles")]
		public extern string[] GetAllAssetBundles();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetAllAssetBundlesWithVariant")]
		public extern string[] GetAllAssetBundlesWithVariant();

		[NativeMethod("GetAssetBundleHash")]
		public Hash128 GetAssetBundleHash(string assetBundleName)
		{
			GetAssetBundleHash_Injected(assetBundleName, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetDirectDependencies")]
		public extern string[] GetDirectDependencies(string assetBundleName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetAllDependencies")]
		public extern string[] GetAllDependencies(string assetBundleName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetAssetBundleHash_Injected(string assetBundleName, out Hash128 ret);
	}
	[StructLayout(LayoutKind.Sequential)]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleRecompressOperation.h")]
	[RequiredByNativeCode]
	public class AssetBundleRecompressOperation : AsyncOperation
	{
		public extern string humanReadableResult
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetResultStr")]
			get;
		}

		public extern string inputPath
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetInputPath")]
			get;
		}

		public extern string outputPath
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetOutputPath")]
			get;
		}

		public extern AssetBundleLoadResult result
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetResult")]
			get;
		}

		public extern bool success
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetSuccess")]
			get;
		}
	}
	[StructLayout(LayoutKind.Sequential)]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadAssetOperation.h")]
	[RequiredByNativeCode]
	public class AssetBundleRequest : ResourceRequest
	{
		public new Object asset => GetResult();

		public extern Object[] allAssets
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetAllLoadedAssets")]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetLoadedAsset")]
		protected override extern Object GetResult();
	}
	[StructLayout(LayoutKind.Sequential)]
	[RequiredByNativeCode]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleUnloadOperation.h")]
	public class AssetBundleUnloadOperation : AsyncOperation
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("WaitForCompletion")]
		public extern void WaitForCompletion();
	}
}
namespace UnityEngine.Experimental.AssetBundlePatching
{
	[NativeHeader("Modules/AssetBundle/Public/AssetBundlePatching.h")]
	public static class AssetBundleUtility
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern void PatchAssetBundles(AssetBundle[] bundles, string[] filenames);
	}
}
