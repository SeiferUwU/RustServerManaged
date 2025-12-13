using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using Unity.Collections;
using Unity.Content;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.SceneManagement;

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
[assembly: InternalsVisibleTo("Unity.UIElements")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsGameObjectsModule")]
[assembly: InternalsVisibleTo("Unity.UIElements.Editor")]
[assembly: InternalsVisibleTo("Unity.UIElements.PlayModeTests")]
[assembly: InternalsVisibleTo("UnityEngine.UIElements.Tests")]
[assembly: InternalsVisibleTo("Unity.PerformanceTests.RuntimeTestRunner.Tests")]
[assembly: InternalsVisibleTo("Unity.UIElements.EditorTests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests")]
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
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework")]
[assembly: InternalsVisibleTo("UnityEngine.PS5VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.UI")]
[assembly: InternalsVisibleTo("Unity.ucg.QoS")]
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
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.013")]
[assembly: InternalsVisibleTo("Unity.Networking.Transport")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.012")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.010")]
[assembly: InternalsVisibleTo("Unity.Services.QoS")]
[assembly: InternalsVisibleTo("Unity.Logging")]
[assembly: InternalsVisibleTo("Unity.Entities")]
[assembly: InternalsVisibleTo("Unity.Entities.Tests")]
[assembly: InternalsVisibleTo("Unity.Collections")]
[assembly: InternalsVisibleTo("Unity.Runtime")]
[assembly: InternalsVisibleTo("Unity.Core")]
[assembly: InternalsVisibleTo("UnityEngine.Core.Runtime.Tests")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.001")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.006")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.007")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.008")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.009")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.011")]
[assembly: InternalsVisibleTo("UnityEngine.PS5Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS4VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS4Module")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterRendererModule")]
[assembly: InternalsVisibleTo("UnityEngine.ContentLoadModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityConnectModule")]
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
[assembly: UnityEngineModuleAssembly]
[assembly: InternalsVisibleTo("UnityEngine.TextRenderingModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputLegacyModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClothModule")]
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: InternalsVisibleTo("UnityEngine")]
[assembly: InternalsVisibleTo("UnityEngine.SharedInternalsModule")]
[assembly: InternalsVisibleTo("UnityEngine.CoreModule")]
[assembly: InternalsVisibleTo("UnityEngine.AIModule")]
[assembly: InternalsVisibleTo("UnityEngine.PhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.JSONSerializeModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputModule")]
[assembly: InternalsVisibleTo("UnityEngine.ARModule")]
[assembly: InternalsVisibleTo("UnityEngine.AccessibilityModule")]
[assembly: InternalsVisibleTo("UnityEngine.AndroidJNIModule")]
[assembly: InternalsVisibleTo("UnityEngine.AnimationModule")]
[assembly: InternalsVisibleTo("UnityEngine.HotReloadModule")]
[assembly: InternalsVisibleTo("UnityEngine.AudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreFontEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.GridModule")]
[assembly: InternalsVisibleTo("UnityEngine.IMGUIModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityCurlModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreTextEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestTextureModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestWWWModule")]
[assembly: InternalsVisibleTo("UnityEngine.VFXModule")]
[assembly: InternalsVisibleTo("UnityEngine.XRModule")]
[assembly: InternalsVisibleTo("UnityEngine.VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.VehiclesModule")]
[assembly: InternalsVisibleTo("UnityEngine.VideoModule")]
[assembly: InternalsVisibleTo("UnityEngine.VirtualTexturingModule")]
[assembly: InternalsVisibleTo("UnityEngine.WindModule")]
[assembly: InternalsVisibleTo("UnityEngine.SwitchModule")]
[assembly: InternalsVisibleTo("UnityEngine.XboxOneModule")]
[assembly: InternalsVisibleTo("UnityEngine.UmbraModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityTestProtocolModule")]
[assembly: InternalsVisibleTo("UnityEngine.TilemapModule")]
[assembly: InternalsVisibleTo("UnityEngine.LocalizationModule")]
[assembly: InternalsVisibleTo("UnityEngine.NVIDIAModule")]
[assembly: InternalsVisibleTo("UnityEngine.ParticleSystemModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIModule")]
[assembly: InternalsVisibleTo("UnityEngine.Physics2DModule")]
[assembly: InternalsVisibleTo("UnityEngine.ProfilerModule")]
[assembly: InternalsVisibleTo("UnityEngine.PropertiesModule")]
[assembly: InternalsVisibleTo("UnityEngine.RuntimeInitializeOnLoadManagerInitializerModule")]
[assembly: InternalsVisibleTo("UnityEngine.PerformanceReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteMaskModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteShapeModule")]
[assembly: InternalsVisibleTo("UnityEngine.StreamingModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubstanceModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubsystemsModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainPhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.ScreenCaptureModule")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace Unity.Loading;

internal enum ContentFileReservedID
{
	None,
	ResolveReferencesWithPM
}
public enum LoadingStatus
{
	InProgress,
	Completed,
	Failed
}
public struct ContentFileUnloadHandle
{
	internal JobHandle jobHandle;

	public bool IsCompleted => jobHandle.IsCompleted;

	public bool WaitForCompletion(int timeoutMs)
	{
		return ContentLoadInterface.WaitForJobCompletion(jobHandle, timeoutMs);
	}
}
public struct ContentFile
{
	internal ulong Id;

	public bool IsValid => ContentLoadInterface.ContentFile_IsHandleValid(this);

	public LoadingStatus LoadingStatus
	{
		get
		{
			ThrowIfInvalidHandle();
			return ContentLoadInterface.ContentFile_GetLoadingStatus(this);
		}
	}

	public static ContentFile GlobalTableDependency => new ContentFile
	{
		Id = 1uL
	};

	public ContentFileUnloadHandle UnloadAsync()
	{
		ThrowIfInvalidHandle();
		return ContentLoadInterface.ContentFile_UnloadAsync(this);
	}

	public UnityEngine.Object[] GetObjects()
	{
		ThrowIfNotComplete();
		return ContentLoadInterface.ContentFile_GetObjects(this);
	}

	public UnityEngine.Object GetObject(ulong localIdentifierInFile)
	{
		ThrowIfNotComplete();
		return ContentLoadInterface.ContentFile_GetObject(this, localIdentifierInFile);
	}

	private void ThrowIfInvalidHandle()
	{
		if (!IsValid)
		{
			throw new Exception("The ContentFile operation cannot be performed because the handle is invalid. Did you already unload it?");
		}
	}

	private void ThrowIfNotComplete()
	{
		switch (LoadingStatus)
		{
		case LoadingStatus.Failed:
			throw new Exception("Cannot use a failed ContentFile operation.");
		case LoadingStatus.InProgress:
			throw new Exception("This ContentFile functionality is not supported while loading is in progress");
		}
	}

	public bool WaitForCompletion(int timeoutMs)
	{
		ThrowIfInvalidHandle();
		return ContentLoadInterface.WaitForLoadCompletion(this, timeoutMs);
	}
}
public enum SceneLoadingStatus
{
	InProgress,
	WaitingForIntegrate,
	WillIntegrateNextFrame,
	Complete,
	Failed
}
public struct ContentSceneParameters
{
	[NativeName("LoadSceneMode")]
	internal LoadSceneMode m_LoadSceneMode;

	[NativeName("LocalPhysicsMode")]
	internal LocalPhysicsMode m_LocalPhysicsMode;

	[NativeName("AutoIntegrate")]
	internal bool m_AutoIntegrate;

	public LoadSceneMode loadSceneMode
	{
		get
		{
			return m_LoadSceneMode;
		}
		set
		{
			m_LoadSceneMode = value;
		}
	}

	public LocalPhysicsMode localPhysicsMode
	{
		get
		{
			return m_LocalPhysicsMode;
		}
		set
		{
			m_LocalPhysicsMode = value;
		}
	}

	public bool autoIntegrate
	{
		get
		{
			return m_AutoIntegrate;
		}
		set
		{
			m_AutoIntegrate = value;
		}
	}
}
public struct ContentSceneFile
{
	internal ulong Id;

	public Scene Scene
	{
		get
		{
			ThrowIfInvalidHandle();
			return ContentLoadInterface.ContentSceneFile_GetScene(this);
		}
	}

	public SceneLoadingStatus Status
	{
		get
		{
			ThrowIfInvalidHandle();
			return ContentLoadInterface.ContentSceneFile_GetStatus(this);
		}
	}

	public bool IsValid => ContentLoadInterface.ContentSceneFile_IsHandleValid(this);

	public void IntegrateAtEndOfFrame()
	{
		ThrowIfInvalidHandle();
		ContentLoadInterface.ContentSceneFile_IntegrateAtEndOfFrame(this);
	}

	public bool UnloadAtEndOfFrame()
	{
		ThrowIfInvalidHandle();
		return ContentLoadInterface.ContentSceneFile_UnloadAtEndOfFrame(this);
	}

	public bool WaitForLoadCompletion(int timeoutMs)
	{
		ThrowIfInvalidHandle();
		return ContentLoadInterface.ContentSceneFile_WaitForCompletion(this, timeoutMs);
	}

	private void ThrowIfInvalidHandle()
	{
		if (!IsValid)
		{
			throw new Exception("The ContentSceneFile operation cannot be performed because the handle is invalid. Did you already unload it?");
		}
	}
}
[NativeHeader("Modules/ContentLoad/Public/ContentLoadFrontend.h")]
[StaticAccessor("GetContentLoadFrontend()", StaticAccessorType.Dot)]
public static class ContentLoadInterface
{
	internal static extern float IntegrationTimeMS
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeThrows]
	internal unsafe static ContentFile LoadContentFileAsync(ContentNamespace nameSpace, string filename, void* dependencies, int dependencyCount, JobHandle dependentFence, bool useUnsafe = false)
	{
		LoadContentFileAsync_Injected(ref nameSpace, filename, dependencies, dependencyCount, ref dependentFence, useUnsafe, out var ret);
		return ret;
	}

	[NativeThrows]
	internal static ContentFileUnloadHandle ContentFile_UnloadAsync(ContentFile handle)
	{
		ContentFile_UnloadAsync_Injected(ref handle, out var ret);
		return ret;
	}

	internal static UnityEngine.Object ContentFile_GetObject(ContentFile handle, ulong localIdentifierInFile)
	{
		return ContentFile_GetObject_Injected(ref handle, localIdentifierInFile);
	}

	internal static UnityEngine.Object[] ContentFile_GetObjects(ContentFile handle)
	{
		return ContentFile_GetObjects_Injected(ref handle);
	}

	internal static LoadingStatus ContentFile_GetLoadingStatus(ContentFile handle)
	{
		return ContentFile_GetLoadingStatus_Injected(ref handle);
	}

	internal static bool ContentFile_IsHandleValid(ContentFile handle)
	{
		return ContentFile_IsHandleValid_Injected(ref handle);
	}

	internal static bool WaitForLoadCompletion(ContentFile handle, int timeoutMs)
	{
		return WaitForLoadCompletion_Injected(ref handle, timeoutMs);
	}

	internal static bool WaitForJobCompletion(JobHandle handle, int timeoutMs)
	{
		return WaitForJobCompletion_Injected(ref handle, timeoutMs);
	}

	[NativeThrows]
	internal unsafe static ContentSceneFile LoadSceneAsync(ContentNamespace nameSpace, string filename, string sceneName, ContentSceneParameters sceneParams, ContentFile* dependencies, int dependencyCount, JobHandle dependentFence)
	{
		LoadSceneAsync_Injected(ref nameSpace, filename, sceneName, ref sceneParams, dependencies, dependencyCount, ref dependentFence, out var ret);
		return ret;
	}

	internal static Scene ContentSceneFile_GetScene(ContentSceneFile handle)
	{
		ContentSceneFile_GetScene_Injected(ref handle, out var ret);
		return ret;
	}

	internal static SceneLoadingStatus ContentSceneFile_GetStatus(ContentSceneFile handle)
	{
		return ContentSceneFile_GetStatus_Injected(ref handle);
	}

	[NativeThrows]
	internal static void ContentSceneFile_IntegrateAtEndOfFrame(ContentSceneFile handle)
	{
		ContentSceneFile_IntegrateAtEndOfFrame_Injected(ref handle);
	}

	internal static bool ContentSceneFile_UnloadAtEndOfFrame(ContentSceneFile handle)
	{
		return ContentSceneFile_UnloadAtEndOfFrame_Injected(ref handle);
	}

	internal static bool ContentSceneFile_IsHandleValid(ContentSceneFile handle)
	{
		return ContentSceneFile_IsHandleValid_Injected(ref handle);
	}

	internal static bool ContentSceneFile_WaitForCompletion(ContentSceneFile handle, int timeoutMs)
	{
		return ContentSceneFile_WaitForCompletion_Injected(ref handle, timeoutMs);
	}

	public unsafe static ContentSceneFile LoadSceneAsync(ContentNamespace nameSpace, string filename, string sceneName, ContentSceneParameters sceneParams, NativeArray<ContentFile> dependencies, JobHandle dependentFence = default(JobHandle))
	{
		return LoadSceneAsync(nameSpace, filename, sceneName, sceneParams, (ContentFile*)dependencies.m_Buffer, dependencies.Length, dependentFence);
	}

	public unsafe static ContentFile LoadContentFileAsync(ContentNamespace nameSpace, string filename, NativeArray<ContentFile> dependencies, JobHandle dependentFence = default(JobHandle))
	{
		return LoadContentFileAsync(nameSpace, filename, dependencies.m_Buffer, dependencies.Length, dependentFence);
	}

	public static ContentFile[] GetContentFiles(ContentNamespace nameSpace)
	{
		return GetContentFiles_Injected(ref nameSpace);
	}

	public static ContentSceneFile[] GetSceneFiles(ContentNamespace nameSpace)
	{
		return GetSceneFiles_Injected(ref nameSpace);
	}

	public static float GetIntegrationTimeMS()
	{
		return IntegrationTimeMS;
	}

	public static void SetIntegrationTimeMS(float integrationTimeMS)
	{
		if (integrationTimeMS <= 0f)
		{
			throw new ArgumentOutOfRangeException("integrationTimeMS", "integrationTimeMS was out of range. Must be greater than zero.");
		}
		IntegrationTimeMS = integrationTimeMS;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private unsafe static extern void LoadContentFileAsync_Injected(ref ContentNamespace nameSpace, string filename, void* dependencies, int dependencyCount, ref JobHandle dependentFence, bool useUnsafe = false, out ContentFile ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void ContentFile_UnloadAsync_Injected(ref ContentFile handle, out ContentFileUnloadHandle ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern UnityEngine.Object ContentFile_GetObject_Injected(ref ContentFile handle, ulong localIdentifierInFile);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern UnityEngine.Object[] ContentFile_GetObjects_Injected(ref ContentFile handle);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern LoadingStatus ContentFile_GetLoadingStatus_Injected(ref ContentFile handle);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool ContentFile_IsHandleValid_Injected(ref ContentFile handle);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool WaitForLoadCompletion_Injected(ref ContentFile handle, int timeoutMs);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool WaitForJobCompletion_Injected(ref JobHandle handle, int timeoutMs);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private unsafe static extern void LoadSceneAsync_Injected(ref ContentNamespace nameSpace, string filename, string sceneName, ref ContentSceneParameters sceneParams, ContentFile* dependencies, int dependencyCount, ref JobHandle dependentFence, out ContentSceneFile ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void ContentSceneFile_GetScene_Injected(ref ContentSceneFile handle, out Scene ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern SceneLoadingStatus ContentSceneFile_GetStatus_Injected(ref ContentSceneFile handle);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void ContentSceneFile_IntegrateAtEndOfFrame_Injected(ref ContentSceneFile handle);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool ContentSceneFile_UnloadAtEndOfFrame_Injected(ref ContentSceneFile handle);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool ContentSceneFile_IsHandleValid_Injected(ref ContentSceneFile handle);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool ContentSceneFile_WaitForCompletion_Injected(ref ContentSceneFile handle, int timeoutMs);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern ContentFile[] GetContentFiles_Injected(ref ContentNamespace nameSpace);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern ContentSceneFile[] GetSceneFiles_Injected(ref ContentNamespace nameSpace);
}
