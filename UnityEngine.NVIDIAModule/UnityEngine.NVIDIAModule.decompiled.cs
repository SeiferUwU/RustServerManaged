using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

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
[assembly: InternalsVisibleTo("Unity.PerformanceTests.RuntimeTestRunner.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.AllIn1Runner")]
[assembly: InternalsVisibleTo("Unity.Timeline")]
[assembly: InternalsVisibleTo("Assembly-CSharp-testable")]
[assembly: InternalsVisibleTo("Assembly-CSharp-firstpass-testable")]
[assembly: InternalsVisibleTo("UnityEngine.SpatialTracking")]
[assembly: InternalsVisibleTo("GoogleAR.UnityNative")]
[assembly: InternalsVisibleTo("Unity.WindowsMRAutomation")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.Editor")]
[assembly: InternalsVisibleTo("UnityEngine.Purchasing")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.EditorTests")]
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
[assembly: InternalsVisibleTo("UnityEngine.Analytics")]
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
[assembly: InternalsVisibleTo("UnityEngine.PS4Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS4VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS5Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS5VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.Networking")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud.Service")]
[assembly: InternalsVisibleTo("Unity.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommon")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.Editor")]
[assembly: InternalsVisibleTo("UnityEditor.UIBuilderModule")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.EditorTests")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.010")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.011")]
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
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.009")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.008")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.007")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.006")]
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
[assembly: InternalsVisibleTo("UnityEngine.UnityTestProtocolModule")]
[assembly: InternalsVisibleTo("Unity.Logging")]
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
[assembly: InternalsVisibleTo("Unity.Entities")]
[assembly: InternalsVisibleTo("UnityEngine.UnityCurlModule")]
[assembly: InternalsVisibleTo("UnityEngine.UmbraModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.HotReloadModule")]
[assembly: InternalsVisibleTo("UnityEngine.CrashReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.AnimationModule")]
[assembly: InternalsVisibleTo("UnityEngine.AccessibilityModule")]
[assembly: InternalsVisibleTo("UnityEngine.ARModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputModule")]
[assembly: InternalsVisibleTo("UnityEngine.JSONSerializeModule")]
[assembly: InternalsVisibleTo("UnityEngine.PhysicsModule")]
[assembly: UnityEngineModuleAssembly]
[assembly: InternalsVisibleTo("UnityEngine.CoreModule")]
[assembly: InternalsVisibleTo("UnityEngine.SharedInternalsModule")]
[assembly: InternalsVisibleTo("UnityEngine")]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: CompilationRelaxations(8)]
[assembly: InternalsVisibleTo("UnityEngine.AndroidJNIModule")]
[assembly: InternalsVisibleTo("UnityEngine.DSPGraphModule")]
[assembly: InternalsVisibleTo("UnityEngine.AIModule")]
[assembly: InternalsVisibleTo("UnityEngine.GIModule")]
[assembly: InternalsVisibleTo("UnityEngine.TilemapModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainPhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubsystemsModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubstanceModule")]
[assembly: InternalsVisibleTo("UnityEngine.StreamingModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteShapeModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.GridModule")]
[assembly: InternalsVisibleTo("UnityEngine.GameCenterModule")]
[assembly: InternalsVisibleTo("UnityEngine.ImageConversionModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIModule")]
[assembly: InternalsVisibleTo("UnityEngine.DirectorModule")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace UnityEngine.NVIDIA;

[NativeHeader("Modules/NVIDIA/NVPlugins.h")]
public static class NVUnityPlugin
{
	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern bool Load();

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern bool IsLoaded();
}
[Flags]
public enum DLSSFeatureFlags
{
	None = 0,
	IsHDR = 1,
	MVLowRes = 2,
	MVJittered = 4,
	DepthInverted = 8,
	DoSharpening = 0x10
}
public enum DLSSQuality
{
	MaximumQuality = 2,
	Balanced = 1,
	MaximumPerformance = 0,
	UltraPerformance = 3
}
internal struct InitDeviceCmdData
{
	private IntPtr m_ProjectId;

	private IntPtr m_EngineVersion;

	private IntPtr m_AppDir;

	public IntPtr projectId
	{
		get
		{
			return m_ProjectId;
		}
		set
		{
			m_ProjectId = value;
		}
	}

	public IntPtr engineVersion
	{
		get
		{
			return m_EngineVersion;
		}
		set
		{
			m_EngineVersion = value;
		}
	}

	public IntPtr appDir
	{
		get
		{
			return m_AppDir;
		}
		set
		{
			m_AppDir = value;
		}
	}
}
public struct DLSSCommandInitializationData
{
	private uint m_InputRTWidth;

	private uint m_InputRTHeight;

	private uint m_OutputRTWidth;

	private uint m_OutputRTHeight;

	private DLSSQuality m_Quality;

	private DLSSFeatureFlags m_Flags;

	private uint m_FeatureSlot;

	public uint inputRTWidth
	{
		get
		{
			return m_InputRTWidth;
		}
		set
		{
			m_InputRTWidth = value;
		}
	}

	public uint inputRTHeight
	{
		get
		{
			return m_InputRTHeight;
		}
		set
		{
			m_InputRTHeight = value;
		}
	}

	public uint outputRTWidth
	{
		get
		{
			return m_OutputRTWidth;
		}
		set
		{
			m_OutputRTWidth = value;
		}
	}

	public uint outputRTHeight
	{
		get
		{
			return m_OutputRTHeight;
		}
		set
		{
			m_OutputRTHeight = value;
		}
	}

	public DLSSQuality quality
	{
		get
		{
			return m_Quality;
		}
		set
		{
			m_Quality = value;
		}
	}

	public DLSSFeatureFlags featureFlags
	{
		get
		{
			return m_Flags;
		}
		set
		{
			m_Flags = value;
		}
	}

	internal uint featureSlot
	{
		get
		{
			return m_FeatureSlot;
		}
		set
		{
			m_FeatureSlot = value;
		}
	}

	public void SetFlag(DLSSFeatureFlags flag, bool value)
	{
		if (value)
		{
			m_Flags |= flag;
		}
		else
		{
			m_Flags &= ~flag;
		}
	}

	public bool GetFlag(DLSSFeatureFlags flag)
	{
		return (m_Flags & flag) != 0;
	}
}
public struct DLSSTextureTable
{
	public Texture colorInput { get; set; }

	public Texture colorOutput { get; set; }

	public Texture depth { get; set; }

	public Texture motionVectors { get; set; }

	public Texture transparencyMask { get; set; }

	public Texture exposureTexture { get; set; }

	public Texture biasColorMask { get; set; }
}
public struct DLSSCommandExecutionData
{
	internal enum Textures
	{
		ColorInput,
		ColorOutput,
		Depth,
		MotionVectors,
		TransparencyMask,
		ExposureTexture,
		BiasColorMask
	}

	private int m_Reset;

	private float m_Sharpness;

	private float m_MVScaleX;

	private float m_MVScaleY;

	private float m_JitterOffsetX;

	private float m_JitterOffsetY;

	private float m_PreExposure;

	private uint m_SubrectOffsetX;

	private uint m_SubrectOffsetY;

	private uint m_SubrectWidth;

	private uint m_SubrectHeight;

	private uint m_InvertXAxis;

	private uint m_InvertYAxis;

	private uint m_FeatureSlot;

	public int reset
	{
		get
		{
			return m_Reset;
		}
		set
		{
			m_Reset = value;
		}
	}

	public float sharpness
	{
		get
		{
			return m_Sharpness;
		}
		set
		{
			m_Sharpness = value;
		}
	}

	public float mvScaleX
	{
		get
		{
			return m_MVScaleX;
		}
		set
		{
			m_MVScaleX = value;
		}
	}

	public float mvScaleY
	{
		get
		{
			return m_MVScaleY;
		}
		set
		{
			m_MVScaleY = value;
		}
	}

	public float jitterOffsetX
	{
		get
		{
			return m_JitterOffsetX;
		}
		set
		{
			m_JitterOffsetX = value;
		}
	}

	public float jitterOffsetY
	{
		get
		{
			return m_JitterOffsetY;
		}
		set
		{
			m_JitterOffsetY = value;
		}
	}

	public float preExposure
	{
		get
		{
			return m_PreExposure;
		}
		set
		{
			m_PreExposure = value;
		}
	}

	public uint subrectOffsetX
	{
		get
		{
			return m_SubrectOffsetX;
		}
		set
		{
			m_SubrectOffsetX = value;
		}
	}

	public uint subrectOffsetY
	{
		get
		{
			return m_SubrectOffsetY;
		}
		set
		{
			m_SubrectOffsetY = value;
		}
	}

	public uint subrectWidth
	{
		get
		{
			return m_SubrectWidth;
		}
		set
		{
			m_SubrectWidth = value;
		}
	}

	public uint subrectHeight
	{
		get
		{
			return m_SubrectHeight;
		}
		set
		{
			m_SubrectHeight = value;
		}
	}

	public uint invertXAxis
	{
		get
		{
			return m_InvertXAxis;
		}
		set
		{
			m_InvertXAxis = value;
		}
	}

	public uint invertYAxis
	{
		get
		{
			return m_InvertYAxis;
		}
		set
		{
			m_InvertYAxis = value;
		}
	}

	internal uint featureSlot
	{
		get
		{
			return m_FeatureSlot;
		}
		set
		{
			m_FeatureSlot = value;
		}
	}
}
public readonly struct OptimalDLSSSettingsData
{
	private readonly uint m_OutRenderWidth;

	private readonly uint m_OutRenderHeight;

	private readonly float m_Sharpness;

	private readonly uint m_MaxWidth;

	private readonly uint m_MaxHeight;

	private readonly uint m_MinWidth;

	private readonly uint m_MinHeight;

	public uint outRenderWidth => m_OutRenderWidth;

	public uint outRenderHeight => m_OutRenderHeight;

	public float sharpness => m_Sharpness;

	public uint maxWidth => m_MaxWidth;

	public uint maxHeight => m_MaxHeight;

	public uint minWidth => m_MinWidth;

	public uint minHeight => m_MinHeight;
}
public readonly struct DLSSDebugFeatureInfos
{
	private readonly bool m_ValidFeature;

	private readonly uint m_FeatureSlot;

	private readonly DLSSCommandExecutionData m_ExecData;

	private readonly DLSSCommandInitializationData m_InitData;

	public bool validFeature => m_ValidFeature;

	public uint featureSlot => m_FeatureSlot;

	public DLSSCommandExecutionData execData => m_ExecData;

	public DLSSCommandInitializationData initData => m_InitData;
}
internal struct GraphicsDeviceDebugInfo
{
	public uint NVDeviceVersion;

	public uint NGXVersion;

	public unsafe DLSSDebugFeatureInfos* dlssInfos;

	public uint dlssInfosCount;
}
internal class NativeData<T> : IDisposable where T : struct
{
	private IntPtr m_MarshalledValue = IntPtr.Zero;

	public T Value = new T();

	public unsafe IntPtr Ptr
	{
		get
		{
			UnsafeUtility.CopyStructureToPtr(ref Value, m_MarshalledValue.ToPointer());
			return m_MarshalledValue;
		}
	}

	public NativeData()
	{
		m_MarshalledValue = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(T)));
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (m_MarshalledValue != IntPtr.Zero)
		{
			Marshal.FreeHGlobal(m_MarshalledValue);
			m_MarshalledValue = IntPtr.Zero;
		}
	}

	~NativeData()
	{
		Dispose(disposing: false);
	}
}
internal class NativeStr : IDisposable
{
	private string m_Str = null;

	private IntPtr m_MarshalledString = IntPtr.Zero;

	public string Str
	{
		set
		{
			m_Str = value;
			Dispose();
			if (value != null)
			{
				m_MarshalledString = Marshal.StringToHGlobalUni(m_Str);
			}
		}
	}

	public IntPtr Ptr => m_MarshalledString;

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (m_MarshalledString != IntPtr.Zero)
		{
			Marshal.FreeHGlobal(m_MarshalledString);
			m_MarshalledString = IntPtr.Zero;
		}
	}

	~NativeStr()
	{
		Dispose(disposing: false);
	}
}
internal class InitDeviceContext
{
	private NativeStr m_ProjectId = new NativeStr();

	private NativeStr m_EngineVersion = new NativeStr();

	private NativeStr m_AppDir = new NativeStr();

	private NativeData<InitDeviceCmdData> m_InitData = new NativeData<InitDeviceCmdData>();

	public InitDeviceContext(string projectId, string engineVersion, string appDir)
	{
		m_ProjectId.Str = projectId;
		m_EngineVersion.Str = engineVersion;
		m_AppDir.Str = appDir;
	}

	internal IntPtr GetInitCmdPtr()
	{
		m_InitData.Value.projectId = m_ProjectId.Ptr;
		m_InitData.Value.engineVersion = m_EngineVersion.Ptr;
		m_InitData.Value.appDir = m_AppDir.Ptr;
		return m_InitData.Ptr;
	}
}
public class DLSSContext
{
	private NativeData<DLSSCommandInitializationData> m_InitData = new NativeData<DLSSCommandInitializationData>();

	private NativeData<DLSSCommandExecutionData> m_ExecData = new NativeData<DLSSCommandExecutionData>();

	public ref readonly DLSSCommandInitializationData initData => ref m_InitData.Value;

	public ref DLSSCommandExecutionData executeData => ref m_ExecData.Value;

	internal uint featureSlot => initData.featureSlot;

	internal DLSSContext()
	{
	}

	internal void Init(DLSSCommandInitializationData initSettings, uint featureSlot)
	{
		m_InitData.Value = initSettings;
		m_InitData.Value.featureSlot = featureSlot;
	}

	internal void Reset()
	{
		m_InitData.Value = default(DLSSCommandInitializationData);
		m_ExecData.Value = default(DLSSCommandExecutionData);
	}

	internal IntPtr GetInitCmdPtr()
	{
		return m_InitData.Ptr;
	}

	internal IntPtr GetExecuteCmdPtr()
	{
		m_ExecData.Value.featureSlot = featureSlot;
		return m_ExecData.Ptr;
	}
}
public class GraphicsDeviceDebugView
{
	internal uint m_ViewId = 0u;

	internal uint m_DeviceVersion = 0u;

	internal uint m_NgxVersion = 0u;

	internal DLSSDebugFeatureInfos[] m_DlssDebugFeatures = null;

	public uint deviceVersion => m_DeviceVersion;

	public uint ngxVersion => m_NgxVersion;

	public IEnumerable<DLSSDebugFeatureInfos> dlssFeatureInfos
	{
		get
		{
			IEnumerable<DLSSDebugFeatureInfos> result;
			if (m_DlssDebugFeatures != null)
			{
				IEnumerable<DLSSDebugFeatureInfos> dlssDebugFeatures = m_DlssDebugFeatures;
				result = dlssDebugFeatures;
			}
			else
			{
				result = Enumerable.Empty<DLSSDebugFeatureInfos>();
			}
			return result;
		}
	}

	internal GraphicsDeviceDebugView(uint viewId)
	{
		m_ViewId = viewId;
	}
}
public enum GraphicsDeviceFeature
{
	DLSS
}
internal enum PluginEvent
{
	DestroyFeature,
	DLSSExecute,
	DLSSInit
}
public class GraphicsDevice
{
	private static string s_DefaultProjectID = "231313132";

	private static string s_DefaultAppDir = ".\\";

	private static GraphicsDevice sGraphicsDeviceInstance = null;

	private InitDeviceContext m_InitDeviceContext = null;

	private Stack<DLSSContext> s_ContextObjectPool = new Stack<DLSSContext>();

	public static GraphicsDevice device => sGraphicsDeviceInstance;

	public static uint version => NVUP_GetDeviceVersion();

	private GraphicsDevice(string projectId, string engineVersion, string appDir)
	{
		m_InitDeviceContext = new InitDeviceContext(projectId, engineVersion, appDir);
	}

	private bool Initialize()
	{
		return NVUP_InitApi(m_InitDeviceContext.GetInitCmdPtr());
	}

	private void Shutdown()
	{
		NVUP_ShutdownApi();
	}

	~GraphicsDevice()
	{
		Shutdown();
	}

	private void InsertEventCall(CommandBuffer cmd, PluginEvent pluginEvent, IntPtr ptr)
	{
		cmd.IssuePluginEventAndData(NVUP_GetRenderEventCallback(), (int)(pluginEvent + NVUP_GetBaseEventId()), ptr);
	}

	private static GraphicsDevice InternalCreate(string appIdOrProjectId, string engineVersion, string appDir)
	{
		if (sGraphicsDeviceInstance != null)
		{
			sGraphicsDeviceInstance.Shutdown();
			sGraphicsDeviceInstance.Initialize();
			return sGraphicsDeviceInstance;
		}
		GraphicsDevice graphicsDevice = new GraphicsDevice(appIdOrProjectId, engineVersion, appDir);
		if (graphicsDevice.Initialize())
		{
			sGraphicsDeviceInstance = graphicsDevice;
			return graphicsDevice;
		}
		return null;
	}

	private static int CreateSetTextureUserData(int featureId, int textureSlot, bool clearTextureTable)
	{
		int num = featureId & 0xFFFF;
		int num2 = textureSlot & 0x7FFF;
		int num3 = (clearTextureTable ? 1 : 0);
		return (num << 16) | (num2 << 1) | num3;
	}

	private void SetTexture(CommandBuffer cmd, DLSSContext dlssContext, DLSSCommandExecutionData.Textures textureSlot, Texture texture, bool clearTextureTable = false)
	{
		if (!(texture == null))
		{
			uint userData = (uint)CreateSetTextureUserData((int)dlssContext.featureSlot, (int)textureSlot, clearTextureTable);
			cmd.IssuePluginCustomTextureUpdateV2(NVUP_GetSetTextureEventCallback(), texture, userData);
		}
	}

	internal GraphicsDeviceDebugInfo GetDebugInfo(uint debugViewId)
	{
		GraphicsDeviceDebugInfo data = default(GraphicsDeviceDebugInfo);
		NVUP_GetGraphicsDeviceDebugInfo(debugViewId, out data);
		return data;
	}

	internal uint CreateDebugViewId()
	{
		return NVUP_CreateDebugView();
	}

	internal void DeleteDebugViewId(uint debugViewId)
	{
		NVUP_DeleteDebugView(debugViewId);
	}

	public static GraphicsDevice CreateGraphicsDevice()
	{
		return InternalCreate(s_DefaultProjectID, Application.unityVersion, s_DefaultAppDir);
	}

	public static GraphicsDevice CreateGraphicsDevice(string projectID)
	{
		return InternalCreate(projectID, Application.unityVersion, s_DefaultAppDir);
	}

	public static GraphicsDevice CreateGraphicsDevice(string projectID, string appDir)
	{
		return InternalCreate(projectID, Application.unityVersion, appDir);
	}

	public bool IsFeatureAvailable(GraphicsDeviceFeature featureID)
	{
		return NVUP_IsFeatureAvailable(featureID);
	}

	public DLSSContext CreateFeature(CommandBuffer cmd, in DLSSCommandInitializationData initSettings)
	{
		if (!IsFeatureAvailable(GraphicsDeviceFeature.DLSS))
		{
			return null;
		}
		DLSSContext dLSSContext = null;
		dLSSContext = ((s_ContextObjectPool.Count != 0) ? s_ContextObjectPool.Pop() : new DLSSContext());
		dLSSContext.Init(initSettings, NVUP_CreateFeatureSlot());
		InsertEventCall(cmd, PluginEvent.DLSSInit, dLSSContext.GetInitCmdPtr());
		return dLSSContext;
	}

	public void DestroyFeature(CommandBuffer cmd, DLSSContext dlssContext)
	{
		InsertEventCall(cmd, PluginEvent.DestroyFeature, new IntPtr(dlssContext.featureSlot));
		dlssContext.Reset();
		s_ContextObjectPool.Push(dlssContext);
	}

	public void ExecuteDLSS(CommandBuffer cmd, DLSSContext dlssContext, in DLSSTextureTable textures)
	{
		SetTexture(cmd, dlssContext, DLSSCommandExecutionData.Textures.ColorInput, textures.colorInput, clearTextureTable: true);
		SetTexture(cmd, dlssContext, DLSSCommandExecutionData.Textures.ColorOutput, textures.colorOutput);
		SetTexture(cmd, dlssContext, DLSSCommandExecutionData.Textures.Depth, textures.depth);
		SetTexture(cmd, dlssContext, DLSSCommandExecutionData.Textures.MotionVectors, textures.motionVectors);
		SetTexture(cmd, dlssContext, DLSSCommandExecutionData.Textures.TransparencyMask, textures.transparencyMask);
		SetTexture(cmd, dlssContext, DLSSCommandExecutionData.Textures.ExposureTexture, textures.exposureTexture);
		SetTexture(cmd, dlssContext, DLSSCommandExecutionData.Textures.BiasColorMask, textures.biasColorMask);
		InsertEventCall(cmd, PluginEvent.DLSSExecute, dlssContext.GetExecuteCmdPtr());
	}

	public bool GetOptimalSettings(uint targetWidth, uint targetHeight, DLSSQuality quality, out OptimalDLSSSettingsData optimalSettings)
	{
		return NVUP_GetOptimalSettings(targetWidth, targetHeight, quality, out optimalSettings);
	}

	public GraphicsDeviceDebugView CreateDebugView()
	{
		return new GraphicsDeviceDebugView(CreateDebugViewId());
	}

	public unsafe void UpdateDebugView(GraphicsDeviceDebugView debugView)
	{
		if (debugView != null)
		{
			GraphicsDeviceDebugInfo debugInfo = GetDebugInfo(debugView.m_ViewId);
			debugView.m_DeviceVersion = debugInfo.NVDeviceVersion;
			debugView.m_NgxVersion = debugInfo.NGXVersion;
			if (debugView.m_DlssDebugFeatures == null || debugInfo.dlssInfosCount != debugView.m_DlssDebugFeatures.Length)
			{
				debugView.m_DlssDebugFeatures = new DLSSDebugFeatureInfos[debugInfo.dlssInfosCount];
			}
			for (int i = 0; i < debugInfo.dlssInfosCount; i++)
			{
				debugView.m_DlssDebugFeatures[i] = debugInfo.dlssInfos[i];
			}
		}
	}

	public void DeleteDebugView(GraphicsDeviceDebugView debugView)
	{
		if (debugView != null)
		{
			DeleteDebugViewId(debugView.m_ViewId);
		}
	}

	[DllImport("NVUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
	private static extern bool NVUP_InitApi(IntPtr initData);

	[DllImport("NVUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
	private static extern void NVUP_ShutdownApi();

	[DllImport("NVUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
	private static extern bool NVUP_IsFeatureAvailable(GraphicsDeviceFeature featureID);

	[DllImport("NVUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
	private static extern bool NVUP_GetOptimalSettings(uint inTargetWidth, uint inTargetHeight, DLSSQuality inPerfVQuality, out OptimalDLSSSettingsData data);

	[DllImport("NVUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
	private static extern IntPtr NVUP_GetRenderEventCallback();

	[DllImport("NVUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
	private static extern IntPtr NVUP_GetSetTextureEventCallback();

	[DllImport("NVUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
	private static extern uint NVUP_CreateFeatureSlot();

	[DllImport("NVUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
	private static extern uint NVUP_GetDeviceVersion();

	[DllImport("NVUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
	private static extern uint NVUP_CreateDebugView();

	[DllImport("NVUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
	private static extern void NVUP_GetGraphicsDeviceDebugInfo(uint debugViewId, out GraphicsDeviceDebugInfo data);

	[DllImport("NVUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
	private static extern void NVUP_DeleteDebugView(uint debugViewId);

	[DllImport("NVUnityPlugin", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
	private static extern int NVUP_GetBaseEventId();
}
