using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.Bindings;

[assembly: InternalsVisibleTo("UnityEngine.Cloud")]
[assembly: InternalsVisibleTo("Assembly-CSharp-firstpass-testable")]
[assembly: InternalsVisibleTo("UnityEngine.SpatialTracking")]
[assembly: InternalsVisibleTo("GoogleAR.UnityNative")]
[assembly: InternalsVisibleTo("Unity.WindowsMRAutomation")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.Editor")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.EditorTests")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.Editor")]
[assembly: InternalsVisibleTo("Assembly-CSharp-testable")]
[assembly: InternalsVisibleTo("UnityEditor.UIBuilderModule")]
[assembly: InternalsVisibleTo("Unity.UIElements")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsGameObjectsModule")]
[assembly: InternalsVisibleTo("Unity.UIElements.Editor")]
[assembly: InternalsVisibleTo("Unity.UIElements.PlayModeTests")]
[assembly: InternalsVisibleTo("UnityEngine.UIElements.Tests")]
[assembly: InternalsVisibleTo("Unity.UIElements.EditorTests")]
[assembly: InternalsVisibleTo("UnityEngine.UI")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.EditorTests")]
[assembly: InternalsVisibleTo("Unity.Networking.Transport")]
[assembly: InternalsVisibleTo("Unity.Timeline")]
[assembly: InternalsVisibleTo("Unity.PerformanceTests.RuntimeTestRunner.Tests")]
[assembly: InternalsVisibleTo("UnityEngine.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommon")]
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
[assembly: InternalsVisibleTo("UnityEngine.Purchasing")]
[assembly: InternalsVisibleTo("UnityEngine.TestRunner")]
[assembly: InternalsVisibleTo("Unity.Automation")]
[assembly: InternalsVisibleTo("Unity.Burst")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.AllIn1Runner")]
[assembly: InternalsVisibleTo("Unity.Burst.Editor")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.UnityAnalytics")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Timeline")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Framework")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework.Tests")]
[assembly: InternalsVisibleTo("Unity.DeploymentTests.Services")]
[assembly: InternalsVisibleTo("Unity.Analytics")]
[assembly: InternalsVisibleTo("Unity.ucg.QoS")]
[assembly: InternalsVisibleTo("Unity.Logging")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.015")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.016")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.017")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.018")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.019")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.020")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.021")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.014")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.022")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.024")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.001")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.005")]
[assembly: InternalsVisibleTo("Unity.Subsystem.Registration")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.023")]
[assembly: InternalsVisibleTo("Unity.Services.QoS")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.013")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.011")]
[assembly: InternalsVisibleTo("Unity.Entities")]
[assembly: InternalsVisibleTo("Unity.Entities.Tests")]
[assembly: InternalsVisibleTo("Unity.Collections")]
[assembly: InternalsVisibleTo("Unity.Runtime")]
[assembly: InternalsVisibleTo("Unity.Core")]
[assembly: InternalsVisibleTo("UnityEngine.Core.Runtime.Tests")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.001")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.012")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.006")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.007")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.008")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.009")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.010")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud.Service")]
[assembly: InternalsVisibleTo("UnityEngine.PS5VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.IMGUIModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommonModule")]
[assembly: InternalsVisibleTo("UnityEngine.TLSModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsModule")]
[assembly: InternalsVisibleTo("UnityEngine.CrashReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.DSPGraphModule")]
[assembly: InternalsVisibleTo("UnityEngine.DirectorModule")]
[assembly: InternalsVisibleTo("UnityEngine.GIModule")]
[assembly: InternalsVisibleTo("UnityEngine.ImageConversionModule")]
[assembly: InternalsVisibleTo("UnityEngine.GameCenterModule")]
[assembly: InternalsVisibleTo("UnityEngine.GridModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextRenderingModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputLegacyModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreFontEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreTextEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityConnectModule")]
[assembly: InternalsVisibleTo("UnityEngine.ContentLoadModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterRendererModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: InternalsVisibleTo("UnityEngine")]
[assembly: InternalsVisibleTo("UnityEngine.SharedInternalsModule")]
[assembly: InternalsVisibleTo("UnityEngine.CoreModule")]
[assembly: InternalsVisibleTo("UnityEngine.AIModule")]
[assembly: InternalsVisibleTo("UnityEngine.PhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.JSONSerializeModule")]
[assembly: InternalsVisibleTo("UnityEngine.Networking")]
[assembly: InternalsVisibleTo("UnityEngine.InputModule")]
[assembly: InternalsVisibleTo("UnityEngine.AccessibilityModule")]
[assembly: InternalsVisibleTo("UnityEngine.AndroidJNIModule")]
[assembly: InternalsVisibleTo("UnityEngine.AnimationModule")]
[assembly: InternalsVisibleTo("UnityEngine.HotReloadModule")]
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.AudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClothModule")]
[assembly: InternalsVisibleTo("UnityEngine.ARModule")]
[assembly: InternalsVisibleTo("UnityEngine.LocalizationModule")]
[assembly: InternalsVisibleTo("UnityEngine.NVIDIAModule")]
[assembly: InternalsVisibleTo("UnityEngine.ParticleSystemModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestTextureModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestWWWModule")]
[assembly: InternalsVisibleTo("UnityEngine.VFXModule")]
[assembly: InternalsVisibleTo("UnityEngine.XRModule")]
[assembly: InternalsVisibleTo("UnityEngine.VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.VehiclesModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.VideoModule")]
[assembly: InternalsVisibleTo("UnityEngine.WindModule")]
[assembly: InternalsVisibleTo("UnityEngine.SwitchModule")]
[assembly: InternalsVisibleTo("UnityEngine.XboxOneModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS4Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS4VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS5Module")]
[assembly: UnityEngineModuleAssembly]
[assembly: InternalsVisibleTo("UnityEngine.VirtualTexturingModule")]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: InternalsVisibleTo("UnityEngine.UnityTestProtocolModule")]
[assembly: InternalsVisibleTo("UnityEngine.UmbraModule")]
[assembly: InternalsVisibleTo("UnityEngine.PerformanceReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.Physics2DModule")]
[assembly: InternalsVisibleTo("UnityEngine.ProfilerModule")]
[assembly: InternalsVisibleTo("UnityEngine.PropertiesModule")]
[assembly: InternalsVisibleTo("UnityEngine.RuntimeInitializeOnLoadManagerInitializerModule")]
[assembly: InternalsVisibleTo("UnityEngine.ScreenCaptureModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteMaskModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityCurlModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteShapeModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubstanceModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubsystemsModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainPhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.TilemapModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsModule")]
[assembly: InternalsVisibleTo("UnityEngine.StreamingModule")]
[assembly: CompilationRelaxations(8)]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace UnityEngine;

[NativeHeader("Modules/JSONSerialize/Public/JsonUtility.bindings.h")]
public static class JsonUtility
{
	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("ToJsonInternal", true)]
	[ThreadSafe]
	private static extern string ToJsonInternal([NotNull("ArgumentNullException")] object obj, bool prettyPrint);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("FromJsonInternal", true, ThrowsException = true)]
	[ThreadSafe]
	private static extern object FromJsonInternal(string json, object objectToOverwrite, Type type);

	public static string ToJson(object obj)
	{
		return ToJson(obj, prettyPrint: false);
	}

	public static string ToJson(object obj, bool prettyPrint)
	{
		if (obj == null)
		{
			return "";
		}
		if (obj is Object && !(obj is MonoBehaviour) && !(obj is ScriptableObject))
		{
			throw new ArgumentException("JsonUtility.ToJson does not support engine types.");
		}
		return ToJsonInternal(obj, prettyPrint);
	}

	public static T FromJson<T>(string json)
	{
		return (T)FromJson(json, typeof(T));
	}

	public static object FromJson(string json, Type type)
	{
		if (string.IsNullOrEmpty(json))
		{
			return null;
		}
		if (type == null)
		{
			throw new ArgumentNullException("type");
		}
		if (type.IsAbstract || type.IsSubclassOf(typeof(Object)))
		{
			throw new ArgumentException("Cannot deserialize JSON to new instances of type '" + type.Name + ".'");
		}
		return FromJsonInternal(json, null, type);
	}

	public static void FromJsonOverwrite(string json, object objectToOverwrite)
	{
		if (!string.IsNullOrEmpty(json))
		{
			if (objectToOverwrite == null)
			{
				throw new ArgumentNullException("objectToOverwrite");
			}
			if (objectToOverwrite is Object && !(objectToOverwrite is MonoBehaviour) && !(objectToOverwrite is ScriptableObject))
			{
				throw new ArgumentException("Engine types cannot be overwritten from JSON outside of the Editor.");
			}
			FromJsonInternal(json, objectToOverwrite, objectToOverwrite.GetType());
		}
	}
}
