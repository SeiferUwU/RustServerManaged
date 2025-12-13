using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;

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
[assembly: InternalsVisibleTo("Unity.UI.Builder.EditorTests")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests")]
[assembly: InternalsVisibleTo("Unity.UIElements")]
[assembly: InternalsVisibleTo("Unity.DeploymentTests.Services")]
[assembly: InternalsVisibleTo("Unity.Burst")]
[assembly: InternalsVisibleTo("UnityEngine.VideoModule")]
[assembly: InternalsVisibleTo("UnityEngine.VirtualTexturingModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommon")]
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
[assembly: InternalsVisibleTo("UnityEngine.Purchasing")]
[assembly: InternalsVisibleTo("UnityEngine.TestRunner")]
[assembly: InternalsVisibleTo("Unity.Automation")]
[assembly: InternalsVisibleTo("Unity.Burst.Editor")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsGameObjectsModule")]
[assembly: InternalsVisibleTo("Unity.UIElements.Editor")]
[assembly: InternalsVisibleTo("Unity.UIElements.PlayModeTests")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.012")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.013")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.014")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.015")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.016")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.017")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.018")]
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
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.011")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.010")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.009")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.008")]
[assembly: InternalsVisibleTo("UnityEngine.UIElements.Tests")]
[assembly: InternalsVisibleTo("Unity.UIElements.EditorTests")]
[assembly: InternalsVisibleTo("UnityEngine.UI")]
[assembly: InternalsVisibleTo("Unity.Networking.Transport")]
[assembly: InternalsVisibleTo("Unity.ucg.QoS")]
[assembly: InternalsVisibleTo("Unity.Services.QoS")]
[assembly: InternalsVisibleTo("Unity.Logging")]
[assembly: InternalsVisibleTo("Unity.Entities")]
[assembly: InternalsVisibleTo("Unity.Entities.Tests")]
[assembly: InternalsVisibleTo("UnityEngine.VehiclesModule")]
[assembly: InternalsVisibleTo("Unity.Collections")]
[assembly: InternalsVisibleTo("Unity.Core")]
[assembly: InternalsVisibleTo("UnityEngine.Core.Runtime.Tests")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.001")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.006")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.007")]
[assembly: InternalsVisibleTo("Unity.Runtime")]
[assembly: InternalsVisibleTo("UnityEngine.VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.XRModule")]
[assembly: InternalsVisibleTo("UnityEngine.VFXModule")]
[assembly: InternalsVisibleTo("UnityEngine.DirectorModule")]
[assembly: InternalsVisibleTo("UnityEngine.DSPGraphModule")]
[assembly: InternalsVisibleTo("UnityEngine.CrashReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestModule")]
[assembly: InternalsVisibleTo("UnityEngine.TLSModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommonModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityConnectModule")]
[assembly: InternalsVisibleTo("UnityEngine.ContentLoadModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterRendererModule")]
[assembly: UnityEngineModuleAssembly]
[assembly: InternalsVisibleTo("UnityEngine.ClothModule")]
[assembly: InternalsVisibleTo("UnityEngine.AudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.GIModule")]
[assembly: InternalsVisibleTo("UnityEngine.HotReloadModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.AnimationModule")]
[assembly: InternalsVisibleTo("UnityEngine.ImageConversionModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
[assembly: InternalsVisibleTo("UnityEngine.GridModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestWWWModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestTextureModule")]
[assembly: InternalsVisibleTo("UnityEngine.GameCenterModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityTestProtocolModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityCurlModule")]
[assembly: InternalsVisibleTo("UnityEngine.UmbraModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIModule")]
[assembly: InternalsVisibleTo("UnityEngine.TilemapModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainPhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubsystemsModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubstanceModule")]
[assembly: InternalsVisibleTo("UnityEngine.StreamingModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteMaskModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextRenderingModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputLegacyModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreFontEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreTextEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.IMGUIModule")]
[assembly: InternalsVisibleTo("UnityEngine.LocalizationModule")]
[assembly: InternalsVisibleTo("UnityEngine.NVIDIAModule")]
[assembly: InternalsVisibleTo("UnityEngine.PerformanceReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.Physics2DModule")]
[assembly: InternalsVisibleTo("UnityEngine.ProfilerModule")]
[assembly: InternalsVisibleTo("UnityEngine.PropertiesModule")]
[assembly: InternalsVisibleTo("UnityEngine.RuntimeInitializeOnLoadManagerInitializerModule")]
[assembly: InternalsVisibleTo("UnityEngine.ScreenCaptureModule")]
[assembly: InternalsVisibleTo("UnityEngine.ParticleSystemModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteShapeModule")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace UnityEngine;

[NativeHeader("Modules/ImageConversion/ScriptBindings/ImageConversion.bindings.h")]
public static class ImageConversion
{
	public static bool EnableLegacyPngGammaRuntimeLoadBehavior
	{
		get
		{
			return GetEnableLegacyPngGammaRuntimeLoadBehavior();
		}
		set
		{
			SetEnableLegacyPngGammaRuntimeLoadBehavior(value);
		}
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(Name = "ImageConversionBindings::GetEnableLegacyPngGammaRuntimeLoadBehavior", IsFreeFunction = true, ThrowsException = false)]
	private static extern bool GetEnableLegacyPngGammaRuntimeLoadBehavior();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(Name = "ImageConversionBindings::SetEnableLegacyPngGammaRuntimeLoadBehavior", IsFreeFunction = true, ThrowsException = false)]
	private static extern void SetEnableLegacyPngGammaRuntimeLoadBehavior(bool enable);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(Name = "ImageConversionBindings::EncodeToTGA", IsFreeFunction = true, ThrowsException = true)]
	public static extern byte[] EncodeToTGA(this Texture2D tex);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(Name = "ImageConversionBindings::EncodeToPNG", IsFreeFunction = true, ThrowsException = true)]
	public static extern byte[] EncodeToPNG(this Texture2D tex);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(Name = "ImageConversionBindings::EncodeToJPG", IsFreeFunction = true, ThrowsException = true)]
	public static extern byte[] EncodeToJPG(this Texture2D tex, int quality);

	public static byte[] EncodeToJPG(this Texture2D tex)
	{
		return tex.EncodeToJPG(75);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(Name = "ImageConversionBindings::EncodeToEXR", IsFreeFunction = true, ThrowsException = true)]
	public static extern byte[] EncodeToEXR(this Texture2D tex, Texture2D.EXRFlags flags);

	public static byte[] EncodeToEXR(this Texture2D tex)
	{
		return tex.EncodeToEXR(Texture2D.EXRFlags.None);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(Name = "ImageConversionBindings::LoadImage", IsFreeFunction = true)]
	public static extern bool LoadImage([NotNull("ArgumentNullException")] this Texture2D tex, byte[] data, bool markNonReadable);

	public static bool LoadImage(this Texture2D tex, byte[] data)
	{
		return tex.LoadImage(data, markNonReadable: false);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("ImageConversionBindings::EncodeArrayToTGA", true)]
	public static extern byte[] EncodeArrayToTGA(Array array, GraphicsFormat format, uint width, uint height, uint rowBytes = 0u);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("ImageConversionBindings::EncodeArrayToPNG", true)]
	public static extern byte[] EncodeArrayToPNG(Array array, GraphicsFormat format, uint width, uint height, uint rowBytes = 0u);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("ImageConversionBindings::EncodeArrayToJPG", true)]
	public static extern byte[] EncodeArrayToJPG(Array array, GraphicsFormat format, uint width, uint height, uint rowBytes = 0u, int quality = 75);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("ImageConversionBindings::EncodeArrayToEXR", true)]
	public static extern byte[] EncodeArrayToEXR(Array array, GraphicsFormat format, uint width, uint height, uint rowBytes = 0u, Texture2D.EXRFlags flags = Texture2D.EXRFlags.None);

	public unsafe static NativeArray<byte> EncodeNativeArrayToTGA<T>(NativeArray<T> input, GraphicsFormat format, uint width, uint height, uint rowBytes = 0u) where T : struct
	{
		int sizeInBytes = input.Length * UnsafeUtility.SizeOf<T>();
		void* dataPointer = UnsafeEncodeNativeArrayToTGA(NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(input), ref sizeInBytes, format, width, height, rowBytes);
		return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(dataPointer, sizeInBytes, Allocator.Persistent);
	}

	public unsafe static NativeArray<byte> EncodeNativeArrayToPNG<T>(NativeArray<T> input, GraphicsFormat format, uint width, uint height, uint rowBytes = 0u) where T : struct
	{
		int sizeInBytes = input.Length * UnsafeUtility.SizeOf<T>();
		void* dataPointer = UnsafeEncodeNativeArrayToPNG(NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(input), ref sizeInBytes, format, width, height, rowBytes);
		return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(dataPointer, sizeInBytes, Allocator.Persistent);
	}

	public unsafe static NativeArray<byte> EncodeNativeArrayToJPG<T>(NativeArray<T> input, GraphicsFormat format, uint width, uint height, uint rowBytes = 0u, int quality = 75) where T : struct
	{
		int sizeInBytes = input.Length * UnsafeUtility.SizeOf<T>();
		void* dataPointer = UnsafeEncodeNativeArrayToJPG(NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(input), ref sizeInBytes, format, width, height, rowBytes, quality);
		return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(dataPointer, sizeInBytes, Allocator.Persistent);
	}

	public unsafe static NativeArray<byte> EncodeNativeArrayToEXR<T>(NativeArray<T> input, GraphicsFormat format, uint width, uint height, uint rowBytes = 0u, Texture2D.EXRFlags flags = Texture2D.EXRFlags.None) where T : struct
	{
		int sizeInBytes = input.Length * UnsafeUtility.SizeOf<T>();
		void* dataPointer = UnsafeEncodeNativeArrayToEXR(NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(input), ref sizeInBytes, format, width, height, rowBytes, flags);
		return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(dataPointer, sizeInBytes, Allocator.Persistent);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("ImageConversionBindings::UnsafeEncodeNativeArrayToTGA", true)]
	private unsafe static extern void* UnsafeEncodeNativeArrayToTGA(void* array, ref int sizeInBytes, GraphicsFormat format, uint width, uint height, uint rowBytes = 0u);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("ImageConversionBindings::UnsafeEncodeNativeArrayToPNG", true)]
	private unsafe static extern void* UnsafeEncodeNativeArrayToPNG(void* array, ref int sizeInBytes, GraphicsFormat format, uint width, uint height, uint rowBytes = 0u);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("ImageConversionBindings::UnsafeEncodeNativeArrayToJPG", true)]
	private unsafe static extern void* UnsafeEncodeNativeArrayToJPG(void* array, ref int sizeInBytes, GraphicsFormat format, uint width, uint height, uint rowBytes = 0u, int quality = 75);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("ImageConversionBindings::UnsafeEncodeNativeArrayToEXR", true)]
	private unsafe static extern void* UnsafeEncodeNativeArrayToEXR(void* array, ref int sizeInBytes, GraphicsFormat format, uint width, uint height, uint rowBytes = 0u, Texture2D.EXRFlags flags = Texture2D.EXRFlags.None);
}
