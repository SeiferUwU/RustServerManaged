using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

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
[assembly: InternalsVisibleTo("UnityEngine.VFXModule")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud")]
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
[assembly: InternalsVisibleTo("UnityEngine.Cloud.Service")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Framework")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestWWWModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextRenderingModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputLegacyModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreFontEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreTextEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.IMGUIModule")]
[assembly: InternalsVisibleTo("UnityEngine.LocalizationModule")]
[assembly: InternalsVisibleTo("UnityEngine.NVIDIAModule")]
[assembly: InternalsVisibleTo("UnityEngine.ParticleSystemModule")]
[assembly: InternalsVisibleTo("UnityEngine.PerformanceReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.Physics2DModule")]
[assembly: InternalsVisibleTo("UnityEngine.ProfilerModule")]
[assembly: InternalsVisibleTo("UnityEngine.PropertiesModule")]
[assembly: InternalsVisibleTo("UnityEngine.RuntimeInitializeOnLoadManagerInitializerModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestTextureModule")]
[assembly: InternalsVisibleTo("UnityEngine.ScreenCaptureModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.SpriteMaskModule")]
[assembly: InternalsVisibleTo("UnityEngine.GridModule")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework.Tests")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
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
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.016")]
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
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.017")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.001")]
[assembly: InternalsVisibleTo("Unity.Core")]
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
[assembly: InternalsVisibleTo("UnityEngine.Core.Runtime.Tests")]
[assembly: InternalsVisibleTo("Unity.UIElements")]
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
[assembly: InternalsVisibleTo("UnityEngine.UIElementsGameObjectsModule")]
[assembly: InternalsVisibleTo("UnityEngine.GameCenterModule")]
[assembly: InternalsVisibleTo("UnityEngine.ImageConversionModule")]
[assembly: InternalsVisibleTo("UnityEngine.GIModule")]
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
[assembly: CompilationRelaxations(8)]
[assembly: InternalsVisibleTo("UnityEngine.HotReloadModule")]
[assembly: InternalsVisibleTo("UnityEngine.AudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClothModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
[assembly: UnityEngineModuleAssembly]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace UnityEngine
{
	public enum TouchPhase
	{
		Began,
		Moved,
		Stationary,
		Ended,
		Canceled
	}
	public enum IMECompositionMode
	{
		Auto,
		On,
		Off
	}
	public enum TouchType
	{
		Direct,
		Indirect,
		Stylus
	}
	[NativeHeader("Runtime/Input/InputBindings.h")]
	public struct Touch
	{
		private int m_FingerId;

		private Vector2 m_Position;

		private Vector2 m_RawPosition;

		private Vector2 m_PositionDelta;

		private float m_TimeDelta;

		private int m_TapCount;

		private TouchPhase m_Phase;

		private TouchType m_Type;

		private float m_Pressure;

		private float m_maximumPossiblePressure;

		private float m_Radius;

		private float m_RadiusVariance;

		private float m_AltitudeAngle;

		private float m_AzimuthAngle;

		public int fingerId
		{
			get
			{
				return m_FingerId;
			}
			set
			{
				m_FingerId = value;
			}
		}

		public Vector2 position
		{
			get
			{
				return m_Position;
			}
			set
			{
				m_Position = value;
			}
		}

		public Vector2 rawPosition
		{
			get
			{
				return m_RawPosition;
			}
			set
			{
				m_RawPosition = value;
			}
		}

		public Vector2 deltaPosition
		{
			get
			{
				return m_PositionDelta;
			}
			set
			{
				m_PositionDelta = value;
			}
		}

		public float deltaTime
		{
			get
			{
				return m_TimeDelta;
			}
			set
			{
				m_TimeDelta = value;
			}
		}

		public int tapCount
		{
			get
			{
				return m_TapCount;
			}
			set
			{
				m_TapCount = value;
			}
		}

		public TouchPhase phase
		{
			get
			{
				return m_Phase;
			}
			set
			{
				m_Phase = value;
			}
		}

		public float pressure
		{
			get
			{
				return m_Pressure;
			}
			set
			{
				m_Pressure = value;
			}
		}

		public float maximumPossiblePressure
		{
			get
			{
				return m_maximumPossiblePressure;
			}
			set
			{
				m_maximumPossiblePressure = value;
			}
		}

		public TouchType type
		{
			get
			{
				return m_Type;
			}
			set
			{
				m_Type = value;
			}
		}

		public float altitudeAngle
		{
			get
			{
				return m_AltitudeAngle;
			}
			set
			{
				m_AltitudeAngle = value;
			}
		}

		public float azimuthAngle
		{
			get
			{
				return m_AzimuthAngle;
			}
			set
			{
				m_AzimuthAngle = value;
			}
		}

		public float radius
		{
			get
			{
				return m_Radius;
			}
			set
			{
				m_Radius = value;
			}
		}

		public float radiusVariance
		{
			get
			{
				return m_RadiusVariance;
			}
			set
			{
				m_RadiusVariance = value;
			}
		}
	}
	[Flags]
	public enum PenStatus
	{
		None = 0,
		Contact = 1,
		Barrel = 2,
		Inverted = 4,
		Eraser = 8
	}
	public enum PenEventType
	{
		NoContact,
		PenDown,
		PenUp
	}
	public struct PenData
	{
		public Vector2 position;

		public Vector2 tilt;

		public PenStatus penStatus;

		public float twist;

		public float pressure;

		public PenEventType contactType;

		public Vector2 deltaPos;
	}
	public enum DeviceOrientation
	{
		Unknown,
		Portrait,
		PortraitUpsideDown,
		LandscapeLeft,
		LandscapeRight,
		FaceUp,
		FaceDown
	}
	public struct AccelerationEvent
	{
		internal float x;

		internal float y;

		internal float z;

		internal float m_TimeDelta;

		public Vector3 acceleration => new Vector3(x, y, z);

		public float deltaTime => m_TimeDelta;
	}
	[NativeHeader("Runtime/Input/GetInput.h")]
	public class Gyroscope
	{
		private int m_GyroIndex;

		public Vector3 rotationRate => rotationRate_Internal(m_GyroIndex);

		public Vector3 rotationRateUnbiased => rotationRateUnbiased_Internal(m_GyroIndex);

		public Vector3 gravity => gravity_Internal(m_GyroIndex);

		public Vector3 userAcceleration => userAcceleration_Internal(m_GyroIndex);

		public Quaternion attitude => attitude_Internal(m_GyroIndex);

		public bool enabled
		{
			get
			{
				return getEnabled_Internal(m_GyroIndex);
			}
			set
			{
				setEnabled_Internal(m_GyroIndex, value);
			}
		}

		public float updateInterval
		{
			get
			{
				return getUpdateInterval_Internal(m_GyroIndex);
			}
			set
			{
				setUpdateInterval_Internal(m_GyroIndex, value);
			}
		}

		internal Gyroscope(int index)
		{
			m_GyroIndex = index;
		}

		[FreeFunction("GetGyroRotationRate")]
		private static Vector3 rotationRate_Internal(int idx)
		{
			rotationRate_Internal_Injected(idx, out var ret);
			return ret;
		}

		[FreeFunction("GetGyroRotationRateUnbiased")]
		private static Vector3 rotationRateUnbiased_Internal(int idx)
		{
			rotationRateUnbiased_Internal_Injected(idx, out var ret);
			return ret;
		}

		[FreeFunction("GetGravity")]
		private static Vector3 gravity_Internal(int idx)
		{
			gravity_Internal_Injected(idx, out var ret);
			return ret;
		}

		[FreeFunction("GetUserAcceleration")]
		private static Vector3 userAcceleration_Internal(int idx)
		{
			userAcceleration_Internal_Injected(idx, out var ret);
			return ret;
		}

		[FreeFunction("GetAttitude")]
		private static Quaternion attitude_Internal(int idx)
		{
			attitude_Internal_Injected(idx, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("IsGyroEnabled")]
		private static extern bool getEnabled_Internal(int idx);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("SetGyroEnabled")]
		private static extern void setEnabled_Internal(int idx, bool enabled);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GetGyroUpdateInterval")]
		private static extern float getUpdateInterval_Internal(int idx);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("SetGyroUpdateInterval")]
		private static extern void setUpdateInterval_Internal(int idx, float interval);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void rotationRate_Internal_Injected(int idx, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void rotationRateUnbiased_Internal_Injected(int idx, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void gravity_Internal_Injected(int idx, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void userAcceleration_Internal_Injected(int idx, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void attitude_Internal_Injected(int idx, out Quaternion ret);
	}
	public struct LocationInfo
	{
		internal double m_Timestamp;

		internal float m_Latitude;

		internal float m_Longitude;

		internal float m_Altitude;

		internal float m_HorizontalAccuracy;

		internal float m_VerticalAccuracy;

		public float latitude => m_Latitude;

		public float longitude => m_Longitude;

		public float altitude => m_Altitude;

		public float horizontalAccuracy => m_HorizontalAccuracy;

		public float verticalAccuracy => m_VerticalAccuracy;

		public double timestamp => m_Timestamp;
	}
	public enum LocationServiceStatus
	{
		Stopped,
		Initializing,
		Running,
		Failed
	}
	[NativeHeader("Runtime/Input/LocationService.h")]
	[NativeHeader("Runtime/Input/InputBindings.h")]
	public class LocationService
	{
		internal struct HeadingInfo
		{
			public float magneticHeading;

			public float trueHeading;

			public float headingAccuracy;

			public Vector3 raw;

			public double timestamp;
		}

		public bool isEnabledByUser => IsServiceEnabledByUser();

		public LocationServiceStatus status => GetLocationStatus();

		public LocationInfo lastData
		{
			get
			{
				if (status != LocationServiceStatus.Running)
				{
					Debug.Log("Location service updates are not enabled. Check LocationService.status before querying last location.");
				}
				return GetLastLocation();
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("LocationService::IsServiceEnabledByUser")]
		internal static extern bool IsServiceEnabledByUser();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("LocationService::GetLocationStatus")]
		internal static extern LocationServiceStatus GetLocationStatus();

		[FreeFunction("LocationService::GetLastLocation")]
		internal static LocationInfo GetLastLocation()
		{
			GetLastLocation_Injected(out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("LocationService::SetDesiredAccuracy")]
		internal static extern void SetDesiredAccuracy(float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("LocationService::SetDistanceFilter")]
		internal static extern void SetDistanceFilter(float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("LocationService::StartUpdatingLocation")]
		internal static extern void StartUpdatingLocation();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("LocationService::StopUpdatingLocation")]
		internal static extern void StopUpdatingLocation();

		[FreeFunction("LocationService::GetLastHeading")]
		internal static HeadingInfo GetLastHeading()
		{
			GetLastHeading_Injected(out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("LocationService::IsHeadingUpdatesEnabled")]
		internal static extern bool IsHeadingUpdatesEnabled();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("LocationService::SetHeadingUpdatesEnabled")]
		internal static extern void SetHeadingUpdatesEnabled(bool value);

		public void Start(float desiredAccuracyInMeters, float updateDistanceInMeters)
		{
			SetDesiredAccuracy(desiredAccuracyInMeters);
			SetDistanceFilter(updateDistanceInMeters);
			StartUpdatingLocation();
		}

		public void Start(float desiredAccuracyInMeters)
		{
			Start(desiredAccuracyInMeters, 10f);
		}

		public void Start()
		{
			Start(10f, 10f);
		}

		public void Stop()
		{
			StopUpdatingLocation();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLastLocation_Injected(out LocationInfo ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLastHeading_Injected(out HeadingInfo ret);
	}
	public class Compass
	{
		public float magneticHeading => LocationService.GetLastHeading().magneticHeading;

		public float trueHeading => LocationService.GetLastHeading().trueHeading;

		public float headingAccuracy => LocationService.GetLastHeading().headingAccuracy;

		public Vector3 rawVector => LocationService.GetLastHeading().raw;

		public double timestamp => LocationService.GetLastHeading().timestamp;

		public bool enabled
		{
			get
			{
				return LocationService.IsHeadingUpdatesEnabled();
			}
			set
			{
				LocationService.SetHeadingUpdatesEnabled(value);
			}
		}
	}
	[NativeHeader("Runtime/Camera/Camera.h")]
	internal class CameraRaycastHelper
	{
		[FreeFunction("CameraScripting::RaycastTry")]
		internal static GameObject RaycastTry(Camera cam, Ray ray, float distance, int layerMask)
		{
			return RaycastTry_Injected(cam, ref ray, distance, layerMask);
		}

		[FreeFunction("CameraScripting::RaycastTry2D")]
		internal static GameObject RaycastTry2D(Camera cam, Ray ray, float distance, int layerMask)
		{
			return RaycastTry2D_Injected(cam, ref ray, distance, layerMask);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GameObject RaycastTry_Injected(Camera cam, ref Ray ray, float distance, int layerMask);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GameObject RaycastTry2D_Injected(Camera cam, ref Ray ray, float distance, int layerMask);
	}
	[NativeHeader("Runtime/Input/InputBindings.h")]
	public class Input
	{
		private static LocationService locationServiceInstance;

		private static Compass compassInstance;

		private static Gyroscope s_MainGyro;

		public static extern bool simulateMouseWithTouches
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeThrows]
		public static extern bool anyKey
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeThrows]
		public static extern bool anyKeyDown
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeThrows]
		public static extern string inputString
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeThrows]
		public static Vector3 mousePosition
		{
			get
			{
				get_mousePosition_Injected(out var ret);
				return ret;
			}
		}

		[NativeThrows]
		public static Vector2 mouseScrollDelta
		{
			get
			{
				get_mouseScrollDelta_Injected(out var ret);
				return ret;
			}
		}

		public static extern IMECompositionMode imeCompositionMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern string compositionString
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern bool imeIsSelected
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static Vector2 compositionCursorPos
		{
			get
			{
				get_compositionCursorPos_Injected(out var ret);
				return ret;
			}
			set
			{
				set_compositionCursorPos_Injected(ref value);
			}
		}

		[Obsolete("eatKeyPressOnTextFieldFocus property is deprecated, and only provided to support legacy behavior.")]
		public static extern bool eatKeyPressOnTextFieldFocus
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern bool mousePresent
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GetMousePresent")]
			get;
		}

		public static extern int penEventCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GetPenEventCount")]
			get;
		}

		public static extern int touchCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GetTouchCount")]
			get;
		}

		public static extern bool touchPressureSupported
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("IsTouchPressureSupported")]
			get;
		}

		public static extern bool stylusTouchSupported
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("IsStylusTouchSupported")]
			get;
		}

		public static extern bool touchSupported
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("IsTouchSupported")]
			get;
		}

		public static extern bool multiTouchEnabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("IsMultiTouchEnabled")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("SetMultiTouchEnabled")]
			set;
		}

		[Obsolete("isGyroAvailable property is deprecated. Please use SystemInfo.supportsGyroscope instead.")]
		public static extern bool isGyroAvailable
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("IsGyroAvailable")]
			get;
		}

		public static extern DeviceOrientation deviceOrientation
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GetDeviceOrientation")]
			get;
		}

		public static Vector3 acceleration
		{
			[FreeFunction("GetAcceleration")]
			get
			{
				get_acceleration_Injected(out var ret);
				return ret;
			}
		}

		public static extern bool compensateSensors
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("IsCompensatingSensors")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("SetCompensatingSensors")]
			set;
		}

		public static extern int accelerationEventCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GetAccelerationCount")]
			get;
		}

		public static extern bool backButtonLeavesApp
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("GetBackButtonLeavesApp")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("SetBackButtonLeavesApp")]
			set;
		}

		public static LocationService location
		{
			get
			{
				if (locationServiceInstance == null)
				{
					locationServiceInstance = new LocationService();
				}
				return locationServiceInstance;
			}
		}

		public static Compass compass
		{
			get
			{
				if (compassInstance == null)
				{
					compassInstance = new Compass();
				}
				return compassInstance;
			}
		}

		public static Gyroscope gyro
		{
			get
			{
				if (s_MainGyro == null)
				{
					s_MainGyro = new Gyroscope(GetGyroInternal());
				}
				return s_MainGyro;
			}
		}

		public static Touch[] touches
		{
			get
			{
				int num = touchCount;
				Touch[] array = new Touch[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = GetTouch(i);
				}
				return array;
			}
		}

		public static AccelerationEvent[] accelerationEvents
		{
			get
			{
				int num = accelerationEventCount;
				AccelerationEvent[] array = new AccelerationEvent[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = GetAccelerationEvent(i);
				}
				return array;
			}
		}

		public static float GetAxis(string axisName)
		{
			return InputUnsafeUtility.GetAxis(axisName);
		}

		public static float GetAxisRaw(string axisName)
		{
			return InputUnsafeUtility.GetAxisRaw(axisName);
		}

		public static bool GetButton(string buttonName)
		{
			return InputUnsafeUtility.GetButton(buttonName);
		}

		public static bool GetButtonDown(string buttonName)
		{
			return InputUnsafeUtility.GetButtonDown(buttonName);
		}

		public static bool GetButtonUp(string buttonName)
		{
			return InputUnsafeUtility.GetButtonUp(buttonName);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool GetKeyInt(KeyCode key);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool GetKeyUpInt(KeyCode key);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool GetKeyDownInt(KeyCode key);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		public static extern bool GetMouseButton(int button);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		public static extern bool GetMouseButtonDown(int button);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		public static extern bool GetMouseButtonUp(int button);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ResetInput")]
		public static extern void ResetInputAxes();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		public static extern string[] GetJoystickNames();

		[NativeThrows]
		public static Touch GetTouch(int index)
		{
			GetTouch_Injected(index, out var ret);
			return ret;
		}

		[NativeThrows]
		public static PenData GetPenEvent(int index)
		{
			GetPenEvent_Injected(index, out var ret);
			return ret;
		}

		[NativeThrows]
		public static PenData GetLastPenContactEvent()
		{
			GetLastPenContactEvent_Injected(out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		public static extern void ResetPenEvents();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		public static extern void ClearLastPenContactEvent();

		[NativeThrows]
		public static AccelerationEvent GetAccelerationEvent(int index)
		{
			GetAccelerationEvent_Injected(index, out var ret);
			return ret;
		}

		public static bool GetKey(KeyCode key)
		{
			return GetKeyInt(key);
		}

		public static bool GetKey(string name)
		{
			return InputUnsafeUtility.GetKeyString(name);
		}

		public static bool GetKeyUp(KeyCode key)
		{
			return GetKeyUpInt(key);
		}

		public static bool GetKeyUp(string name)
		{
			return InputUnsafeUtility.GetKeyUpString(name);
		}

		public static bool GetKeyDown(KeyCode key)
		{
			return GetKeyDownInt(key);
		}

		public static bool GetKeyDown(string name)
		{
			return InputUnsafeUtility.GetKeyDownString(name);
		}

		[Conditional("UNITY_EDITOR")]
		internal static void SimulateTouch(Touch touch)
		{
		}

		[FreeFunction("SimulateTouch")]
		[NativeConditional("UNITY_EDITOR")]
		[Conditional("UNITY_EDITOR")]
		private static void SimulateTouchInternal(Touch touch, long timestamp)
		{
			SimulateTouchInternal_Injected(ref touch, timestamp);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("GetGyro")]
		private static extern int GetGyroInternal();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CheckDisabled();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTouch_Injected(int index, out Touch ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPenEvent_Injected(int index, out PenData ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLastPenContactEvent_Injected(out PenData ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAccelerationEvent_Injected(int index, out AccelerationEvent ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SimulateTouchInternal_Injected(ref Touch touch, long timestamp);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private static extern void get_mousePosition_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private static extern void get_mouseScrollDelta_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private static extern void get_compositionCursorPos_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private static extern void set_compositionCursorPos_Injected(ref Vector2 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private static extern void get_acceleration_Injected(out Vector3 ret);
	}
	internal class SendMouseEvents
	{
		private struct HitInfo
		{
			public GameObject target;

			public Camera camera;

			public void SendMessage(string name)
			{
				target.SendMessage(name, null, SendMessageOptions.DontRequireReceiver);
			}

			public static implicit operator bool(HitInfo exists)
			{
				return exists.target != null && exists.camera != null;
			}

			public static bool Compare(HitInfo lhs, HitInfo rhs)
			{
				return lhs.target == rhs.target && lhs.camera == rhs.camera;
			}
		}

		public enum LeftMouseButtonState
		{
			NotPressed,
			Pressed,
			PressedThisFrame
		}

		private const int m_HitIndexGUI = 0;

		private const int m_HitIndexPhysics3D = 1;

		private const int m_HitIndexPhysics2D = 2;

		private static bool s_MouseUsed = false;

		private static readonly HitInfo[] m_LastHit = new HitInfo[3];

		private static readonly HitInfo[] m_MouseDownHit = new HitInfo[3];

		private static readonly HitInfo[] m_CurrentHit = new HitInfo[3];

		private static Camera[] m_Cameras;

		public static Func<KeyValuePair<int, Vector2>> s_GetMouseState;

		private static Vector2 s_MousePosition;

		private static bool s_MouseButtonPressedThisFrame;

		private static bool s_MouseButtonIsPressed;

		private static void UpdateMouse()
		{
			if (s_GetMouseState != null)
			{
				KeyValuePair<int, Vector2> keyValuePair = s_GetMouseState();
				s_MousePosition = keyValuePair.Value;
				s_MouseButtonPressedThisFrame = keyValuePair.Key == 2;
				s_MouseButtonIsPressed = keyValuePair.Key != 0;
			}
			else if (!Input.CheckDisabled())
			{
				s_MousePosition = Input.mousePosition;
				s_MouseButtonPressedThisFrame = Input.GetMouseButtonDown(0);
				s_MouseButtonIsPressed = Input.GetMouseButton(0);
			}
			else
			{
				s_MousePosition = default(Vector2);
				s_MouseButtonPressedThisFrame = false;
				s_MouseButtonIsPressed = false;
			}
		}

		[RequiredByNativeCode]
		private static void SetMouseMoved()
		{
			s_MouseUsed = true;
		}

		[RequiredByNativeCode]
		private static void DoSendMouseEvents(int skipRTCameras)
		{
			UpdateMouse();
			Vector2 vector = s_MousePosition;
			int allCamerasCount = Camera.allCamerasCount;
			if (m_Cameras == null || m_Cameras.Length != allCamerasCount)
			{
				m_Cameras = new Camera[allCamerasCount];
			}
			Camera.GetAllCameras(m_Cameras);
			for (int i = 0; i < m_CurrentHit.Length; i++)
			{
				m_CurrentHit[i] = default(HitInfo);
			}
			if (!s_MouseUsed)
			{
				Camera[] cameras = m_Cameras;
				foreach (Camera camera in cameras)
				{
					if (camera == null || (skipRTCameras != 0 && camera.targetTexture != null))
					{
						continue;
					}
					int targetDisplay = camera.targetDisplay;
					Vector3 vector2 = Display.RelativeMouseAt(vector);
					if (vector2 != Vector3.zero)
					{
						int num = (int)vector2.z;
						if (num != targetDisplay)
						{
							continue;
						}
						float num2 = Screen.width;
						float num3 = Screen.height;
						if (targetDisplay > 0 && targetDisplay < Display.displays.Length)
						{
							num2 = Display.displays[targetDisplay].systemWidth;
							num3 = Display.displays[targetDisplay].systemHeight;
						}
						Vector2 vector3 = new Vector2(vector2.x / num2, vector2.y / num3);
						if (vector3.x < 0f || vector3.x > 1f || vector3.y < 0f || vector3.y > 1f)
						{
							continue;
						}
					}
					else
					{
						vector2 = vector;
					}
					if (camera.pixelRect.Contains(vector2) && camera.eventMask != 0)
					{
						Ray ray = camera.ScreenPointToRay(vector2);
						float z = ray.direction.z;
						float distance = (Mathf.Approximately(0f, z) ? float.PositiveInfinity : Mathf.Abs((camera.farClipPlane - camera.nearClipPlane) / z));
						GameObject gameObject = CameraRaycastHelper.RaycastTry(camera, ray, distance, camera.cullingMask & camera.eventMask);
						if (gameObject != null)
						{
							m_CurrentHit[1].target = gameObject;
							m_CurrentHit[1].camera = camera;
						}
						else if (camera.clearFlags == CameraClearFlags.Skybox || camera.clearFlags == CameraClearFlags.Color)
						{
							m_CurrentHit[1].target = null;
							m_CurrentHit[1].camera = null;
						}
						GameObject gameObject2 = CameraRaycastHelper.RaycastTry2D(camera, ray, distance, camera.cullingMask & camera.eventMask);
						if (gameObject2 != null)
						{
							m_CurrentHit[2].target = gameObject2;
							m_CurrentHit[2].camera = camera;
						}
						else if (camera.clearFlags == CameraClearFlags.Skybox || camera.clearFlags == CameraClearFlags.Color)
						{
							m_CurrentHit[2].target = null;
							m_CurrentHit[2].camera = null;
						}
					}
				}
			}
			for (int k = 0; k < m_CurrentHit.Length; k++)
			{
				SendEvents(k, m_CurrentHit[k]);
			}
			s_MouseUsed = false;
		}

		private static void SendEvents(int i, HitInfo hit)
		{
			bool flag = s_MouseButtonPressedThisFrame;
			bool flag2 = s_MouseButtonIsPressed;
			if (flag)
			{
				if ((bool)hit)
				{
					m_MouseDownHit[i] = hit;
					m_MouseDownHit[i].SendMessage("OnMouseDown");
				}
			}
			else if (!flag2)
			{
				if ((bool)m_MouseDownHit[i])
				{
					if (HitInfo.Compare(hit, m_MouseDownHit[i]))
					{
						m_MouseDownHit[i].SendMessage("OnMouseUpAsButton");
					}
					m_MouseDownHit[i].SendMessage("OnMouseUp");
					m_MouseDownHit[i] = default(HitInfo);
				}
			}
			else if ((bool)m_MouseDownHit[i])
			{
				m_MouseDownHit[i].SendMessage("OnMouseDrag");
			}
			if (HitInfo.Compare(hit, m_LastHit[i]))
			{
				if ((bool)hit)
				{
					hit.SendMessage("OnMouseOver");
				}
			}
			else
			{
				if ((bool)m_LastHit[i])
				{
					m_LastHit[i].SendMessage("OnMouseExit");
				}
				if ((bool)hit)
				{
					hit.SendMessage("OnMouseEnter");
					hit.SendMessage("OnMouseOver");
				}
			}
			m_LastHit[i] = hit;
		}
	}
}
namespace UnityEngine.Internal
{
	[NativeHeader("Runtime/Input/InputBindings.h")]
	internal static class InputUnsafeUtility
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		internal static extern bool GetKeyString(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		[UnityEngine.Scripting.RequiredMember]
		internal unsafe static extern bool GetKeyString__Unmanaged(byte* name, int nameLen);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		internal static extern bool GetKeyUpString(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		[UnityEngine.Scripting.RequiredMember]
		internal unsafe static extern bool GetKeyUpString__Unmanaged(byte* name, int nameLen);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		internal static extern bool GetKeyDownString(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		[UnityEngine.Scripting.RequiredMember]
		internal unsafe static extern bool GetKeyDownString__Unmanaged(byte* name, int nameLen);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		internal static extern float GetAxis(string axisName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		[UnityEngine.Scripting.RequiredMember]
		internal unsafe static extern float GetAxis__Unmanaged(byte* axisName, int axisNameLen);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		internal static extern float GetAxisRaw(string axisName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[UnityEngine.Scripting.RequiredMember]
		[NativeThrows]
		internal unsafe static extern float GetAxisRaw__Unmanaged(byte* axisName, int axisNameLen);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		internal static extern bool GetButton(string buttonName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		[UnityEngine.Scripting.RequiredMember]
		internal unsafe static extern bool GetButton__Unmanaged(byte* buttonName, int buttonNameLen);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		internal static extern bool GetButtonDown(string buttonName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		[UnityEngine.Scripting.RequiredMember]
		internal unsafe static extern byte GetButtonDown__Unmanaged(byte* buttonName, int buttonNameLen);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		internal static extern bool GetButtonUp(string buttonName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[UnityEngine.Scripting.RequiredMember]
		[NativeThrows]
		internal unsafe static extern bool GetButtonUp__Unmanaged(byte* buttonName, int buttonNameLen);
	}
}
