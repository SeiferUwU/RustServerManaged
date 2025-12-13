using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

[assembly: InternalsVisibleTo("Unity.PerformanceTests.RuntimeTestRunner.Tests")]
[assembly: InternalsVisibleTo("Unity.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud.Service")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud")]
[assembly: InternalsVisibleTo("UnityEngine.Networking")]
[assembly: InternalsVisibleTo("UnityEngine.PS5VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS5Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS4VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS4Module")]
[assembly: InternalsVisibleTo("UnityEngine.XboxOneModule")]
[assembly: InternalsVisibleTo("UnityEngine.SwitchModule")]
[assembly: InternalsVisibleTo("UnityEngine.WindModule")]
[assembly: InternalsVisibleTo("UnityEngine.VirtualTexturingModule")]
[assembly: InternalsVisibleTo("UnityEngine.VideoModule")]
[assembly: InternalsVisibleTo("UnityEngine.VehiclesModule")]
[assembly: InternalsVisibleTo("UnityEngine.VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.XRModule")]
[assembly: InternalsVisibleTo("UnityEngine.VFXModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestWWWModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestTextureModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityTestProtocolModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityCurlModule")]
[assembly: InternalsVisibleTo("UnityEngine.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.UmbraModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommon")]
[assembly: InternalsVisibleTo("UnityEngine.Purchasing")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.EditorTests")]
[assembly: InternalsVisibleTo("UnityEditor.UIBuilderModule")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.Editor")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.EditorTests")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.Editor")]
[assembly: InternalsVisibleTo("Unity.WindowsMRAutomation")]
[assembly: InternalsVisibleTo("GoogleAR.UnityNative")]
[assembly: InternalsVisibleTo("UnityEngine.SpatialTracking")]
[assembly: InternalsVisibleTo("Assembly-CSharp-firstpass-testable")]
[assembly: InternalsVisibleTo("Assembly-CSharp-testable")]
[assembly: InternalsVisibleTo("Unity.Timeline")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.AllIn1Runner")]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Framework")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Timeline")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.UnityAnalytics")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests")]
[assembly: InternalsVisibleTo("Unity.DeploymentTests.Services")]
[assembly: InternalsVisibleTo("Unity.Burst.Editor")]
[assembly: InternalsVisibleTo("Unity.Burst")]
[assembly: InternalsVisibleTo("Unity.Automation")]
[assembly: InternalsVisibleTo("UnityEngine.TestRunner")]
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsModule")]
[assembly: InternalsVisibleTo("UnityEngine.TilemapModule")]
[assembly: InternalsVisibleTo("Unity.UIElements")]
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
[assembly: InternalsVisibleTo("UnityEngine.CrashReportingModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.AnimationModule")]
[assembly: InternalsVisibleTo("UnityEngine.DSPGraphModule")]
[assembly: InternalsVisibleTo("UnityEngine.DirectorModule")]
[assembly: InternalsVisibleTo("UnityEngine.GIModule")]
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
[assembly: UnityEngineModuleAssembly]
[assembly: InternalsVisibleTo("UnityEngine.TextRenderingModule")]
[assembly: InternalsVisibleTo("UnityEngine.GridModule")]
[assembly: InternalsVisibleTo("UnityEngine.GameCenterModule")]
[assembly: InternalsVisibleTo("UnityEngine.ImageConversionModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsGameObjectsModule")]
[assembly: CompilationRelaxations(8)]
[assembly: InternalsVisibleTo("Unity.UIElements.PlayModeTests")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.006")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.001")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.024")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.023")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.007")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.008")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.022")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.021")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.020")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.009")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.010")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.011")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.012")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.013")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.014")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.019")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.018")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.017")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.016")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.005")]
[assembly: InternalsVisibleTo("Unity.Subsystem.Registration")]
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
[assembly: InternalsVisibleTo("Unity.UIElements.Editor")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.015")]
[assembly: InternalsVisibleTo("Unity.Runtime")]
[assembly: InternalsVisibleTo("Unity.Core")]
[assembly: InternalsVisibleTo("UnityEngine.Core.Runtime.Tests")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.001")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.002")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace UnityEngine.XR
{
	[RequiredByNativeCode]
	[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputTrackingFacade.h")]
	[NativeConditional("ENABLE_VR")]
	[StaticAccessor("XRInputTrackingFacade::Get()", StaticAccessorType.Dot)]
	public static class InputTracking
	{
		private enum TrackingStateEventType
		{
			NodeAdded,
			NodeRemoved,
			TrackingAcquired,
			TrackingLost
		}

		[NativeConditional("ENABLE_VR")]
		[Obsolete("This API is obsolete, and should no longer be used. Please use the TrackedPoseDriver in the Legacy Input Helpers package for controlling a camera in XR.")]
		public static extern bool disablePositionalTracking
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetPositionalTrackingDisabled")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("SetPositionalTrackingDisabled")]
			set;
		}

		public static event Action<XRNodeState> trackingAcquired;

		public static event Action<XRNodeState> trackingLost;

		public static event Action<XRNodeState> nodeAdded;

		public static event Action<XRNodeState> nodeRemoved;

		[RequiredByNativeCode]
		private static void InvokeTrackingEvent(TrackingStateEventType eventType, XRNode nodeType, long uniqueID, bool tracked)
		{
			Action<XRNodeState> action = null;
			XRNodeState obj = new XRNodeState
			{
				uniqueID = (ulong)uniqueID,
				nodeType = nodeType,
				tracked = tracked
			};
			((Action<XRNodeState>)(eventType switch
			{
				TrackingStateEventType.TrackingAcquired => InputTracking.trackingAcquired, 
				TrackingStateEventType.TrackingLost => InputTracking.trackingLost, 
				TrackingStateEventType.NodeAdded => InputTracking.nodeAdded, 
				TrackingStateEventType.NodeRemoved => InputTracking.nodeRemoved, 
				_ => throw new ArgumentException("TrackingEventHandler - Invalid EventType: " + eventType), 
			}))?.Invoke(obj);
		}

		[NativeConditional("ENABLE_VR", "Vector3f::zero")]
		[Obsolete("This API is obsolete, and should no longer be used. Please use InputDevice.TryGetFeatureValue with the CommonUsages.devicePosition usage instead.")]
		public static Vector3 GetLocalPosition(XRNode node)
		{
			GetLocalPosition_Injected(node, out var ret);
			return ret;
		}

		[Obsolete("This API is obsolete, and should no longer be used. Please use InputDevice.TryGetFeatureValue with the CommonUsages.deviceRotation usage instead.")]
		[NativeConditional("ENABLE_VR", "Quaternionf::identity()")]
		public static Quaternion GetLocalRotation(XRNode node)
		{
			GetLocalRotation_Injected(node, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[Obsolete("This API is obsolete, and should no longer be used. Please use XRInputSubsystem.TryRecenter() instead.")]
		[NativeConditional("ENABLE_VR")]
		public static extern void Recenter();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[Obsolete("This API is obsolete, and should no longer be used. Please use InputDevice.name with the device associated with that tracking data instead.")]
		[NativeConditional("ENABLE_VR")]
		public static extern string GetNodeName(ulong uniqueId);

		public static void GetNodeStates(List<XRNodeState> nodeStates)
		{
			if (nodeStates == null)
			{
				throw new ArgumentNullException("nodeStates");
			}
			nodeStates.Clear();
			GetNodeStates_Internal(nodeStates);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeConditional("ENABLE_VR")]
		private static extern void GetNodeStates_Internal([NotNull("ArgumentNullException")] List<XRNodeState> nodeStates);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputTracking.h")]
		[StaticAccessor("XRInputTracking::Get()", StaticAccessorType.Dot)]
		internal static extern ulong GetDeviceIdAtXRNode(XRNode node);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("XRInputTracking::Get()", StaticAccessorType.Dot)]
		[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputTracking.h")]
		internal static extern void GetDeviceIdsAtXRNode_Internal(XRNode node, [NotNull("ArgumentNullException")] List<ulong> deviceIds);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalPosition_Injected(XRNode node, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalRotation_Injected(XRNode node, out Quaternion ret);
	}
	public enum XRNode
	{
		LeftEye,
		RightEye,
		CenterEye,
		Head,
		LeftHand,
		RightHand,
		GameController,
		TrackingReference,
		HardwareTracker
	}
	[Flags]
	internal enum AvailableTrackingData
	{
		None = 0,
		PositionAvailable = 1,
		RotationAvailable = 2,
		VelocityAvailable = 4,
		AngularVelocityAvailable = 8,
		AccelerationAvailable = 0x10,
		AngularAccelerationAvailable = 0x20
	}
	[UsedByNativeCode]
	public struct XRNodeState
	{
		private XRNode m_Type;

		private AvailableTrackingData m_AvailableFields;

		private Vector3 m_Position;

		private Quaternion m_Rotation;

		private Vector3 m_Velocity;

		private Vector3 m_AngularVelocity;

		private Vector3 m_Acceleration;

		private Vector3 m_AngularAcceleration;

		private int m_Tracked;

		private ulong m_UniqueID;

		public ulong uniqueID
		{
			get
			{
				return m_UniqueID;
			}
			set
			{
				m_UniqueID = value;
			}
		}

		public XRNode nodeType
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

		public bool tracked
		{
			get
			{
				return m_Tracked == 1;
			}
			set
			{
				m_Tracked = (value ? 1 : 0);
			}
		}

		public Vector3 position
		{
			set
			{
				m_Position = value;
				m_AvailableFields |= AvailableTrackingData.PositionAvailable;
			}
		}

		public Quaternion rotation
		{
			set
			{
				m_Rotation = value;
				m_AvailableFields |= AvailableTrackingData.RotationAvailable;
			}
		}

		public Vector3 velocity
		{
			set
			{
				m_Velocity = value;
				m_AvailableFields |= AvailableTrackingData.VelocityAvailable;
			}
		}

		public Vector3 angularVelocity
		{
			set
			{
				m_AngularVelocity = value;
				m_AvailableFields |= AvailableTrackingData.AngularVelocityAvailable;
			}
		}

		public Vector3 acceleration
		{
			set
			{
				m_Acceleration = value;
				m_AvailableFields |= AvailableTrackingData.AccelerationAvailable;
			}
		}

		public Vector3 angularAcceleration
		{
			set
			{
				m_AngularAcceleration = value;
				m_AvailableFields |= AvailableTrackingData.AngularAccelerationAvailable;
			}
		}

		public bool TryGetPosition(out Vector3 position)
		{
			return TryGet(m_Position, AvailableTrackingData.PositionAvailable, out position);
		}

		public bool TryGetRotation(out Quaternion rotation)
		{
			return TryGet(m_Rotation, AvailableTrackingData.RotationAvailable, out rotation);
		}

		public bool TryGetVelocity(out Vector3 velocity)
		{
			return TryGet(m_Velocity, AvailableTrackingData.VelocityAvailable, out velocity);
		}

		public bool TryGetAngularVelocity(out Vector3 angularVelocity)
		{
			return TryGet(m_AngularVelocity, AvailableTrackingData.AngularVelocityAvailable, out angularVelocity);
		}

		public bool TryGetAcceleration(out Vector3 acceleration)
		{
			return TryGet(m_Acceleration, AvailableTrackingData.AccelerationAvailable, out acceleration);
		}

		public bool TryGetAngularAcceleration(out Vector3 angularAcceleration)
		{
			return TryGet(m_AngularAcceleration, AvailableTrackingData.AngularAccelerationAvailable, out angularAcceleration);
		}

		private bool TryGet(Vector3 inValue, AvailableTrackingData availabilityFlag, out Vector3 outValue)
		{
			if ((m_AvailableFields & availabilityFlag) > AvailableTrackingData.None)
			{
				outValue = inValue;
				return true;
			}
			outValue = Vector3.zero;
			return false;
		}

		private bool TryGet(Quaternion inValue, AvailableTrackingData availabilityFlag, out Quaternion outValue)
		{
			if ((m_AvailableFields & availabilityFlag) > AvailableTrackingData.None)
			{
				outValue = inValue;
				return true;
			}
			outValue = Quaternion.identity;
			return false;
		}
	}
	[NativeConditional("ENABLE_VR")]
	public struct HapticCapabilities : IEquatable<HapticCapabilities>
	{
		private uint m_NumChannels;

		private bool m_SupportsImpulse;

		private bool m_SupportsBuffer;

		private uint m_BufferFrequencyHz;

		private uint m_BufferMaxSize;

		private uint m_BufferOptimalSize;

		public uint numChannels
		{
			get
			{
				return m_NumChannels;
			}
			internal set
			{
				m_NumChannels = value;
			}
		}

		public bool supportsImpulse
		{
			get
			{
				return m_SupportsImpulse;
			}
			internal set
			{
				m_SupportsImpulse = value;
			}
		}

		public bool supportsBuffer
		{
			get
			{
				return m_SupportsBuffer;
			}
			internal set
			{
				m_SupportsBuffer = value;
			}
		}

		public uint bufferFrequencyHz
		{
			get
			{
				return m_BufferFrequencyHz;
			}
			internal set
			{
				m_BufferFrequencyHz = value;
			}
		}

		public uint bufferMaxSize
		{
			get
			{
				return m_BufferMaxSize;
			}
			internal set
			{
				m_BufferMaxSize = value;
			}
		}

		public uint bufferOptimalSize
		{
			get
			{
				return m_BufferOptimalSize;
			}
			internal set
			{
				m_BufferOptimalSize = value;
			}
		}

		public override bool Equals(object obj)
		{
			if (!(obj is HapticCapabilities))
			{
				return false;
			}
			return Equals((HapticCapabilities)obj);
		}

		public bool Equals(HapticCapabilities other)
		{
			return numChannels == other.numChannels && supportsImpulse == other.supportsImpulse && supportsBuffer == other.supportsBuffer && bufferFrequencyHz == other.bufferFrequencyHz && bufferMaxSize == other.bufferMaxSize && bufferOptimalSize == other.bufferOptimalSize;
		}

		public override int GetHashCode()
		{
			return numChannels.GetHashCode() ^ (supportsImpulse.GetHashCode() << 1) ^ (supportsBuffer.GetHashCode() >> 1) ^ (bufferFrequencyHz.GetHashCode() << 2) ^ (bufferMaxSize.GetHashCode() >> 2) ^ (bufferOptimalSize.GetHashCode() << 3);
		}

		public static bool operator ==(HapticCapabilities a, HapticCapabilities b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(HapticCapabilities a, HapticCapabilities b)
		{
			return !(a == b);
		}
	}
	internal enum InputFeatureType : uint
	{
		Custom = 0u,
		Binary = 1u,
		DiscreteStates = 2u,
		Axis1D = 3u,
		Axis2D = 4u,
		Axis3D = 5u,
		Rotation = 6u,
		Hand = 7u,
		Bone = 8u,
		Eyes = 9u,
		kUnityXRInputFeatureTypeInvalid = uint.MaxValue
	}
	internal enum ConnectionChangeType : uint
	{
		Connected,
		Disconnected,
		ConfigChange
	}
	public enum InputDeviceRole : uint
	{
		Unknown,
		Generic,
		LeftHanded,
		RightHanded,
		GameController,
		TrackingReference,
		HardwareTracker,
		LegacyController
	}
	[Flags]
	public enum InputDeviceCharacteristics : uint
	{
		None = 0u,
		HeadMounted = 1u,
		Camera = 2u,
		HeldInHand = 4u,
		HandTracking = 8u,
		EyeTracking = 0x10u,
		TrackedDevice = 0x20u,
		Controller = 0x40u,
		TrackingReference = 0x80u,
		Left = 0x100u,
		Right = 0x200u,
		Simulated6DOF = 0x400u
	}
	[Flags]
	public enum InputTrackingState : uint
	{
		None = 0u,
		Position = 1u,
		Rotation = 2u,
		Velocity = 4u,
		AngularVelocity = 8u,
		Acceleration = 0x10u,
		AngularAcceleration = 0x20u,
		All = 0x3Fu
	}
	[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputDevices.h")]
	[RequiredByNativeCode]
	[NativeConditional("ENABLE_VR")]
	public struct InputFeatureUsage : IEquatable<InputFeatureUsage>
	{
		internal string m_Name;

		[NativeName("m_FeatureType")]
		internal InputFeatureType m_InternalType;

		public string name
		{
			get
			{
				return m_Name;
			}
			internal set
			{
				m_Name = value;
			}
		}

		internal InputFeatureType internalType
		{
			get
			{
				return m_InternalType;
			}
			set
			{
				m_InternalType = value;
			}
		}

		public Type type => m_InternalType switch
		{
			InputFeatureType.Custom => typeof(byte[]), 
			InputFeatureType.Binary => typeof(bool), 
			InputFeatureType.DiscreteStates => typeof(uint), 
			InputFeatureType.Axis1D => typeof(float), 
			InputFeatureType.Axis2D => typeof(Vector2), 
			InputFeatureType.Axis3D => typeof(Vector3), 
			InputFeatureType.Rotation => typeof(Quaternion), 
			InputFeatureType.Hand => typeof(Hand), 
			InputFeatureType.Bone => typeof(Bone), 
			InputFeatureType.Eyes => typeof(Eyes), 
			_ => throw new InvalidCastException("No valid managed type for unknown native type."), 
		};

		internal InputFeatureUsage(string name, InputFeatureType type)
		{
			m_Name = name;
			m_InternalType = type;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is InputFeatureUsage))
			{
				return false;
			}
			return Equals((InputFeatureUsage)obj);
		}

		public bool Equals(InputFeatureUsage other)
		{
			return name == other.name && internalType == other.internalType;
		}

		public override int GetHashCode()
		{
			return name.GetHashCode() ^ (internalType.GetHashCode() << 1);
		}

		public static bool operator ==(InputFeatureUsage a, InputFeatureUsage b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(InputFeatureUsage a, InputFeatureUsage b)
		{
			return !(a == b);
		}

		public InputFeatureUsage<T> As<T>()
		{
			if (type != typeof(T))
			{
				throw new ArgumentException("InputFeatureUsage type does not match out variable type.");
			}
			return new InputFeatureUsage<T>(name);
		}
	}
	public struct InputFeatureUsage<T> : IEquatable<InputFeatureUsage<T>>
	{
		public string name { get; set; }

		private Type usageType => typeof(T);

		public InputFeatureUsage(string usageName)
		{
			name = usageName;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is InputFeatureUsage<T>))
			{
				return false;
			}
			return Equals((InputFeatureUsage<T>)obj);
		}

		public bool Equals(InputFeatureUsage<T> other)
		{
			return name == other.name;
		}

		public override int GetHashCode()
		{
			return name.GetHashCode();
		}

		public static bool operator ==(InputFeatureUsage<T> a, InputFeatureUsage<T> b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(InputFeatureUsage<T> a, InputFeatureUsage<T> b)
		{
			return !(a == b);
		}

		public static explicit operator InputFeatureUsage(InputFeatureUsage<T> self)
		{
			InputFeatureType inputFeatureType = InputFeatureType.kUnityXRInputFeatureTypeInvalid;
			Type type = self.usageType;
			if (type == typeof(bool))
			{
				inputFeatureType = InputFeatureType.Binary;
			}
			else if (type == typeof(uint))
			{
				inputFeatureType = InputFeatureType.DiscreteStates;
			}
			else if (type == typeof(float))
			{
				inputFeatureType = InputFeatureType.Axis1D;
			}
			else if (type == typeof(Vector2))
			{
				inputFeatureType = InputFeatureType.Axis2D;
			}
			else if (type == typeof(Vector3))
			{
				inputFeatureType = InputFeatureType.Axis3D;
			}
			else if (type == typeof(Quaternion))
			{
				inputFeatureType = InputFeatureType.Rotation;
			}
			else if (type == typeof(Hand))
			{
				inputFeatureType = InputFeatureType.Hand;
			}
			else if (type == typeof(Bone))
			{
				inputFeatureType = InputFeatureType.Bone;
			}
			else if (type == typeof(Eyes))
			{
				inputFeatureType = InputFeatureType.Eyes;
			}
			else if (type == typeof(byte[]))
			{
				inputFeatureType = InputFeatureType.Custom;
			}
			else if (type.IsEnum)
			{
				inputFeatureType = InputFeatureType.DiscreteStates;
			}
			if (inputFeatureType != InputFeatureType.kUnityXRInputFeatureTypeInvalid)
			{
				return new InputFeatureUsage(self.name, inputFeatureType);
			}
			throw new InvalidCastException("No valid InputFeatureType for " + self.name + ".");
		}
	}
	public static class CommonUsages
	{
		public static InputFeatureUsage<bool> isTracked = new InputFeatureUsage<bool>("IsTracked");

		public static InputFeatureUsage<bool> primaryButton = new InputFeatureUsage<bool>("PrimaryButton");

		public static InputFeatureUsage<bool> primaryTouch = new InputFeatureUsage<bool>("PrimaryTouch");

		public static InputFeatureUsage<bool> secondaryButton = new InputFeatureUsage<bool>("SecondaryButton");

		public static InputFeatureUsage<bool> secondaryTouch = new InputFeatureUsage<bool>("SecondaryTouch");

		public static InputFeatureUsage<bool> gripButton = new InputFeatureUsage<bool>("GripButton");

		public static InputFeatureUsage<bool> triggerButton = new InputFeatureUsage<bool>("TriggerButton");

		public static InputFeatureUsage<bool> menuButton = new InputFeatureUsage<bool>("MenuButton");

		public static InputFeatureUsage<bool> primary2DAxisClick = new InputFeatureUsage<bool>("Primary2DAxisClick");

		public static InputFeatureUsage<bool> primary2DAxisTouch = new InputFeatureUsage<bool>("Primary2DAxisTouch");

		public static InputFeatureUsage<bool> secondary2DAxisClick = new InputFeatureUsage<bool>("Secondary2DAxisClick");

		public static InputFeatureUsage<bool> secondary2DAxisTouch = new InputFeatureUsage<bool>("Secondary2DAxisTouch");

		public static InputFeatureUsage<bool> userPresence = new InputFeatureUsage<bool>("UserPresence");

		public static InputFeatureUsage<InputTrackingState> trackingState = new InputFeatureUsage<InputTrackingState>("TrackingState");

		public static InputFeatureUsage<float> batteryLevel = new InputFeatureUsage<float>("BatteryLevel");

		public static InputFeatureUsage<float> trigger = new InputFeatureUsage<float>("Trigger");

		public static InputFeatureUsage<float> grip = new InputFeatureUsage<float>("Grip");

		public static InputFeatureUsage<Vector2> primary2DAxis = new InputFeatureUsage<Vector2>("Primary2DAxis");

		public static InputFeatureUsage<Vector2> secondary2DAxis = new InputFeatureUsage<Vector2>("Secondary2DAxis");

		public static InputFeatureUsage<Vector3> devicePosition = new InputFeatureUsage<Vector3>("DevicePosition");

		public static InputFeatureUsage<Vector3> leftEyePosition = new InputFeatureUsage<Vector3>("LeftEyePosition");

		public static InputFeatureUsage<Vector3> rightEyePosition = new InputFeatureUsage<Vector3>("RightEyePosition");

		public static InputFeatureUsage<Vector3> centerEyePosition = new InputFeatureUsage<Vector3>("CenterEyePosition");

		public static InputFeatureUsage<Vector3> colorCameraPosition = new InputFeatureUsage<Vector3>("CameraPosition");

		public static InputFeatureUsage<Vector3> deviceVelocity = new InputFeatureUsage<Vector3>("DeviceVelocity");

		public static InputFeatureUsage<Vector3> deviceAngularVelocity = new InputFeatureUsage<Vector3>("DeviceAngularVelocity");

		public static InputFeatureUsage<Vector3> leftEyeVelocity = new InputFeatureUsage<Vector3>("LeftEyeVelocity");

		public static InputFeatureUsage<Vector3> leftEyeAngularVelocity = new InputFeatureUsage<Vector3>("LeftEyeAngularVelocity");

		public static InputFeatureUsage<Vector3> rightEyeVelocity = new InputFeatureUsage<Vector3>("RightEyeVelocity");

		public static InputFeatureUsage<Vector3> rightEyeAngularVelocity = new InputFeatureUsage<Vector3>("RightEyeAngularVelocity");

		public static InputFeatureUsage<Vector3> centerEyeVelocity = new InputFeatureUsage<Vector3>("CenterEyeVelocity");

		public static InputFeatureUsage<Vector3> centerEyeAngularVelocity = new InputFeatureUsage<Vector3>("CenterEyeAngularVelocity");

		public static InputFeatureUsage<Vector3> colorCameraVelocity = new InputFeatureUsage<Vector3>("CameraVelocity");

		public static InputFeatureUsage<Vector3> colorCameraAngularVelocity = new InputFeatureUsage<Vector3>("CameraAngularVelocity");

		public static InputFeatureUsage<Vector3> deviceAcceleration = new InputFeatureUsage<Vector3>("DeviceAcceleration");

		public static InputFeatureUsage<Vector3> deviceAngularAcceleration = new InputFeatureUsage<Vector3>("DeviceAngularAcceleration");

		public static InputFeatureUsage<Vector3> leftEyeAcceleration = new InputFeatureUsage<Vector3>("LeftEyeAcceleration");

		public static InputFeatureUsage<Vector3> leftEyeAngularAcceleration = new InputFeatureUsage<Vector3>("LeftEyeAngularAcceleration");

		public static InputFeatureUsage<Vector3> rightEyeAcceleration = new InputFeatureUsage<Vector3>("RightEyeAcceleration");

		public static InputFeatureUsage<Vector3> rightEyeAngularAcceleration = new InputFeatureUsage<Vector3>("RightEyeAngularAcceleration");

		public static InputFeatureUsage<Vector3> centerEyeAcceleration = new InputFeatureUsage<Vector3>("CenterEyeAcceleration");

		public static InputFeatureUsage<Vector3> centerEyeAngularAcceleration = new InputFeatureUsage<Vector3>("CenterEyeAngularAcceleration");

		public static InputFeatureUsage<Vector3> colorCameraAcceleration = new InputFeatureUsage<Vector3>("CameraAcceleration");

		public static InputFeatureUsage<Vector3> colorCameraAngularAcceleration = new InputFeatureUsage<Vector3>("CameraAngularAcceleration");

		public static InputFeatureUsage<Quaternion> deviceRotation = new InputFeatureUsage<Quaternion>("DeviceRotation");

		public static InputFeatureUsage<Quaternion> leftEyeRotation = new InputFeatureUsage<Quaternion>("LeftEyeRotation");

		public static InputFeatureUsage<Quaternion> rightEyeRotation = new InputFeatureUsage<Quaternion>("RightEyeRotation");

		public static InputFeatureUsage<Quaternion> centerEyeRotation = new InputFeatureUsage<Quaternion>("CenterEyeRotation");

		public static InputFeatureUsage<Quaternion> colorCameraRotation = new InputFeatureUsage<Quaternion>("CameraRotation");

		public static InputFeatureUsage<Hand> handData = new InputFeatureUsage<Hand>("HandData");

		public static InputFeatureUsage<Eyes> eyesData = new InputFeatureUsage<Eyes>("EyesData");

		[Obsolete("CommonUsages.dPad is not used by any XR platform and will be removed.")]
		public static InputFeatureUsage<Vector2> dPad = new InputFeatureUsage<Vector2>("DPad");

		[Obsolete("CommonUsages.indexFinger is not used by any XR platform and will be removed.")]
		public static InputFeatureUsage<float> indexFinger = new InputFeatureUsage<float>("IndexFinger");

		[Obsolete("CommonUsages.MiddleFinger is not used by any XR platform and will be removed.")]
		public static InputFeatureUsage<float> middleFinger = new InputFeatureUsage<float>("MiddleFinger");

		[Obsolete("CommonUsages.RingFinger is not used by any XR platform and will be removed.")]
		public static InputFeatureUsage<float> ringFinger = new InputFeatureUsage<float>("RingFinger");

		[Obsolete("CommonUsages.PinkyFinger is not used by any XR platform and will be removed.")]
		public static InputFeatureUsage<float> pinkyFinger = new InputFeatureUsage<float>("PinkyFinger");

		[Obsolete("CommonUsages.thumbrest is Oculus only, and is being moved to their package. Please use OculusUsages.thumbrest. These will still function until removed.")]
		public static InputFeatureUsage<bool> thumbrest = new InputFeatureUsage<bool>("Thumbrest");

		[Obsolete("CommonUsages.indexTouch is Oculus only, and is being moved to their package.  Please use OculusUsages.indexTouch. These will still function until removed.")]
		public static InputFeatureUsage<float> indexTouch = new InputFeatureUsage<float>("IndexTouch");

		[Obsolete("CommonUsages.thumbTouch is Oculus only, and is being moved to their package.  Please use OculusUsages.thumbTouch. These will still function until removed.")]
		public static InputFeatureUsage<float> thumbTouch = new InputFeatureUsage<float>("ThumbTouch");
	}
	[NativeConditional("ENABLE_VR")]
	[UsedByNativeCode]
	public struct InputDevice : IEquatable<InputDevice>
	{
		private static List<XRInputSubsystem> s_InputSubsystemCache;

		private ulong m_DeviceId;

		private bool m_Initialized;

		private ulong deviceId => m_Initialized ? m_DeviceId : ulong.MaxValue;

		public XRInputSubsystem subsystem
		{
			get
			{
				if (s_InputSubsystemCache == null)
				{
					s_InputSubsystemCache = new List<XRInputSubsystem>();
				}
				if (m_Initialized)
				{
					uint num = (uint)(m_DeviceId >> 32);
					SubsystemManager.GetSubsystems(s_InputSubsystemCache);
					for (int i = 0; i < s_InputSubsystemCache.Count; i++)
					{
						if (num == s_InputSubsystemCache[i].GetIndex())
						{
							return s_InputSubsystemCache[i];
						}
					}
				}
				return null;
			}
		}

		public bool isValid => IsValidId() && InputDevices.IsDeviceValid(m_DeviceId);

		public string name => IsValidId() ? InputDevices.GetDeviceName(m_DeviceId) : null;

		[Obsolete("This API has been marked as deprecated and will be removed in future versions. Please use InputDevice.characteristics instead.")]
		public InputDeviceRole role => IsValidId() ? InputDevices.GetDeviceRole(m_DeviceId) : InputDeviceRole.Unknown;

		public string manufacturer => IsValidId() ? InputDevices.GetDeviceManufacturer(m_DeviceId) : null;

		public string serialNumber => IsValidId() ? InputDevices.GetDeviceSerialNumber(m_DeviceId) : null;

		public InputDeviceCharacteristics characteristics => IsValidId() ? InputDevices.GetDeviceCharacteristics(m_DeviceId) : InputDeviceCharacteristics.None;

		internal InputDevice(ulong deviceId)
		{
			m_DeviceId = deviceId;
			m_Initialized = true;
		}

		private bool IsValidId()
		{
			return deviceId != ulong.MaxValue;
		}

		public bool SendHapticImpulse(uint channel, float amplitude, float duration = 1f)
		{
			if (!IsValidId())
			{
				return false;
			}
			if (amplitude < 0f)
			{
				throw new ArgumentException("Amplitude of SendHapticImpulse cannot be negative.");
			}
			if (duration < 0f)
			{
				throw new ArgumentException("Duration of SendHapticImpulse cannot be negative.");
			}
			return InputDevices.SendHapticImpulse(m_DeviceId, channel, amplitude, duration);
		}

		public bool SendHapticBuffer(uint channel, byte[] buffer)
		{
			if (!IsValidId())
			{
				return false;
			}
			return InputDevices.SendHapticBuffer(m_DeviceId, channel, buffer);
		}

		public bool TryGetHapticCapabilities(out HapticCapabilities capabilities)
		{
			if (CheckValidAndSetDefault<HapticCapabilities>(out capabilities))
			{
				return InputDevices.TryGetHapticCapabilities(m_DeviceId, out capabilities);
			}
			return false;
		}

		public void StopHaptics()
		{
			if (IsValidId())
			{
				InputDevices.StopHaptics(m_DeviceId);
			}
		}

		public bool TryGetFeatureUsages(List<InputFeatureUsage> featureUsages)
		{
			if (IsValidId())
			{
				return InputDevices.TryGetFeatureUsages(m_DeviceId, featureUsages);
			}
			return false;
		}

		public bool TryGetFeatureValue(InputFeatureUsage<bool> usage, out bool value)
		{
			if (CheckValidAndSetDefault<bool>(out value))
			{
				return InputDevices.TryGetFeatureValue_bool(m_DeviceId, usage.name, out value);
			}
			return false;
		}

		public bool TryGetFeatureValue(InputFeatureUsage<uint> usage, out uint value)
		{
			if (CheckValidAndSetDefault<uint>(out value))
			{
				return InputDevices.TryGetFeatureValue_UInt32(m_DeviceId, usage.name, out value);
			}
			return false;
		}

		public bool TryGetFeatureValue(InputFeatureUsage<float> usage, out float value)
		{
			if (CheckValidAndSetDefault<float>(out value))
			{
				return InputDevices.TryGetFeatureValue_float(m_DeviceId, usage.name, out value);
			}
			return false;
		}

		public bool TryGetFeatureValue(InputFeatureUsage<Vector2> usage, out Vector2 value)
		{
			if (CheckValidAndSetDefault<Vector2>(out value))
			{
				return InputDevices.TryGetFeatureValue_Vector2f(m_DeviceId, usage.name, out value);
			}
			return false;
		}

		public bool TryGetFeatureValue(InputFeatureUsage<Vector3> usage, out Vector3 value)
		{
			if (CheckValidAndSetDefault<Vector3>(out value))
			{
				return InputDevices.TryGetFeatureValue_Vector3f(m_DeviceId, usage.name, out value);
			}
			return false;
		}

		public bool TryGetFeatureValue(InputFeatureUsage<Quaternion> usage, out Quaternion value)
		{
			if (CheckValidAndSetDefault<Quaternion>(out value))
			{
				return InputDevices.TryGetFeatureValue_Quaternionf(m_DeviceId, usage.name, out value);
			}
			return false;
		}

		public bool TryGetFeatureValue(InputFeatureUsage<Hand> usage, out Hand value)
		{
			if (CheckValidAndSetDefault<Hand>(out value))
			{
				return InputDevices.TryGetFeatureValue_XRHand(m_DeviceId, usage.name, out value);
			}
			return false;
		}

		public bool TryGetFeatureValue(InputFeatureUsage<Bone> usage, out Bone value)
		{
			if (CheckValidAndSetDefault<Bone>(out value))
			{
				return InputDevices.TryGetFeatureValue_XRBone(m_DeviceId, usage.name, out value);
			}
			return false;
		}

		public bool TryGetFeatureValue(InputFeatureUsage<Eyes> usage, out Eyes value)
		{
			if (CheckValidAndSetDefault<Eyes>(out value))
			{
				return InputDevices.TryGetFeatureValue_XREyes(m_DeviceId, usage.name, out value);
			}
			return false;
		}

		public bool TryGetFeatureValue(InputFeatureUsage<byte[]> usage, byte[] value)
		{
			if (IsValidId())
			{
				return InputDevices.TryGetFeatureValue_Custom(m_DeviceId, usage.name, value);
			}
			return false;
		}

		public bool TryGetFeatureValue(InputFeatureUsage<InputTrackingState> usage, out InputTrackingState value)
		{
			if (IsValidId())
			{
				uint value2 = 0u;
				if (InputDevices.TryGetFeatureValue_UInt32(m_DeviceId, usage.name, out value2))
				{
					value = (InputTrackingState)value2;
					return true;
				}
			}
			value = InputTrackingState.None;
			return false;
		}

		public bool TryGetFeatureValue(InputFeatureUsage<bool> usage, DateTime time, out bool value)
		{
			if (CheckValidAndSetDefault<bool>(out value))
			{
				return InputDevices.TryGetFeatureValueAtTime_bool(m_DeviceId, usage.name, TimeConverter.LocalDateTimeToUnixTimeMilliseconds(time), out value);
			}
			return false;
		}

		public bool TryGetFeatureValue(InputFeatureUsage<uint> usage, DateTime time, out uint value)
		{
			if (CheckValidAndSetDefault<uint>(out value))
			{
				return InputDevices.TryGetFeatureValueAtTime_UInt32(m_DeviceId, usage.name, TimeConverter.LocalDateTimeToUnixTimeMilliseconds(time), out value);
			}
			return false;
		}

		public bool TryGetFeatureValue(InputFeatureUsage<float> usage, DateTime time, out float value)
		{
			if (CheckValidAndSetDefault<float>(out value))
			{
				return InputDevices.TryGetFeatureValueAtTime_float(m_DeviceId, usage.name, TimeConverter.LocalDateTimeToUnixTimeMilliseconds(time), out value);
			}
			return false;
		}

		public bool TryGetFeatureValue(InputFeatureUsage<Vector2> usage, DateTime time, out Vector2 value)
		{
			if (CheckValidAndSetDefault<Vector2>(out value))
			{
				return InputDevices.TryGetFeatureValueAtTime_Vector2f(m_DeviceId, usage.name, TimeConverter.LocalDateTimeToUnixTimeMilliseconds(time), out value);
			}
			return false;
		}

		public bool TryGetFeatureValue(InputFeatureUsage<Vector3> usage, DateTime time, out Vector3 value)
		{
			if (CheckValidAndSetDefault<Vector3>(out value))
			{
				return InputDevices.TryGetFeatureValueAtTime_Vector3f(m_DeviceId, usage.name, TimeConverter.LocalDateTimeToUnixTimeMilliseconds(time), out value);
			}
			return false;
		}

		public bool TryGetFeatureValue(InputFeatureUsage<Quaternion> usage, DateTime time, out Quaternion value)
		{
			if (CheckValidAndSetDefault<Quaternion>(out value))
			{
				return InputDevices.TryGetFeatureValueAtTime_Quaternionf(m_DeviceId, usage.name, TimeConverter.LocalDateTimeToUnixTimeMilliseconds(time), out value);
			}
			return false;
		}

		public bool TryGetFeatureValue(InputFeatureUsage<InputTrackingState> usage, DateTime time, out InputTrackingState value)
		{
			if (IsValidId())
			{
				uint value2 = 0u;
				if (InputDevices.TryGetFeatureValueAtTime_UInt32(m_DeviceId, usage.name, TimeConverter.LocalDateTimeToUnixTimeMilliseconds(time), out value2))
				{
					value = (InputTrackingState)value2;
					return true;
				}
			}
			value = InputTrackingState.None;
			return false;
		}

		private bool CheckValidAndSetDefault<T>(out T value)
		{
			value = default(T);
			return IsValidId();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is InputDevice))
			{
				return false;
			}
			return Equals((InputDevice)obj);
		}

		public bool Equals(InputDevice other)
		{
			return deviceId == other.deviceId;
		}

		public override int GetHashCode()
		{
			return deviceId.GetHashCode();
		}

		public static bool operator ==(InputDevice a, InputDevice b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(InputDevice a, InputDevice b)
		{
			return !(a == b);
		}
	}
	internal static class TimeConverter
	{
		private static readonly DateTime s_Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		public static DateTime now => DateTime.Now;

		public static long LocalDateTimeToUnixTimeMilliseconds(DateTime date)
		{
			return Convert.ToInt64((date.ToUniversalTime() - s_Epoch).TotalMilliseconds);
		}

		public static DateTime UnixTimeMillisecondsToLocalDateTime(long unixTimeInMilliseconds)
		{
			DateTime dateTime = s_Epoch;
			return dateTime.AddMilliseconds(unixTimeInMilliseconds).ToLocalTime();
		}
	}
	public enum HandFinger
	{
		Thumb,
		Index,
		Middle,
		Ring,
		Pinky
	}
	[NativeHeader("Modules/XR/XRPrefix.h")]
	[RequiredByNativeCode]
	[StaticAccessor("XRInputDevices::Get()", StaticAccessorType.Dot)]
	[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputDevices.h")]
	[NativeHeader("XRScriptingClasses.h")]
	[NativeConditional("ENABLE_VR")]
	public struct Hand : IEquatable<Hand>
	{
		private ulong m_DeviceId;

		private uint m_FeatureIndex;

		internal ulong deviceId => m_DeviceId;

		internal uint featureIndex => m_FeatureIndex;

		public bool TryGetRootBone(out Bone boneOut)
		{
			return Hand_TryGetRootBone(this, out boneOut);
		}

		private static bool Hand_TryGetRootBone(Hand hand, out Bone boneOut)
		{
			return Hand_TryGetRootBone_Injected(ref hand, out boneOut);
		}

		public bool TryGetFingerBones(HandFinger finger, List<Bone> bonesOut)
		{
			if (bonesOut == null)
			{
				throw new ArgumentNullException("bonesOut");
			}
			return Hand_TryGetFingerBonesAsList(this, finger, bonesOut);
		}

		private static bool Hand_TryGetFingerBonesAsList(Hand hand, HandFinger finger, [NotNull("ArgumentNullException")] List<Bone> bonesOut)
		{
			return Hand_TryGetFingerBonesAsList_Injected(ref hand, finger, bonesOut);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Hand))
			{
				return false;
			}
			return Equals((Hand)obj);
		}

		public bool Equals(Hand other)
		{
			return deviceId == other.deviceId && featureIndex == other.featureIndex;
		}

		public override int GetHashCode()
		{
			return deviceId.GetHashCode() ^ (featureIndex.GetHashCode() << 1);
		}

		public static bool operator ==(Hand a, Hand b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(Hand a, Hand b)
		{
			return !(a == b);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Hand_TryGetRootBone_Injected(ref Hand hand, out Bone boneOut);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Hand_TryGetFingerBonesAsList_Injected(ref Hand hand, HandFinger finger, List<Bone> bonesOut);
	}
	internal enum EyeSide
	{
		Left,
		Right
	}
	[NativeConditional("ENABLE_VR")]
	[NativeHeader("Modules/XR/XRPrefix.h")]
	[NativeHeader("XRScriptingClasses.h")]
	[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputDevices.h")]
	[StaticAccessor("XRInputDevices::Get()", StaticAccessorType.Dot)]
	[RequiredByNativeCode]
	public struct Eyes : IEquatable<Eyes>
	{
		private ulong m_DeviceId;

		private uint m_FeatureIndex;

		internal ulong deviceId => m_DeviceId;

		internal uint featureIndex => m_FeatureIndex;

		public bool TryGetLeftEyePosition(out Vector3 position)
		{
			return Eyes_TryGetEyePosition(this, EyeSide.Left, out position);
		}

		public bool TryGetRightEyePosition(out Vector3 position)
		{
			return Eyes_TryGetEyePosition(this, EyeSide.Right, out position);
		}

		public bool TryGetLeftEyeRotation(out Quaternion rotation)
		{
			return Eyes_TryGetEyeRotation(this, EyeSide.Left, out rotation);
		}

		public bool TryGetRightEyeRotation(out Quaternion rotation)
		{
			return Eyes_TryGetEyeRotation(this, EyeSide.Right, out rotation);
		}

		private static bool Eyes_TryGetEyePosition(Eyes eyes, EyeSide chirality, out Vector3 position)
		{
			return Eyes_TryGetEyePosition_Injected(ref eyes, chirality, out position);
		}

		private static bool Eyes_TryGetEyeRotation(Eyes eyes, EyeSide chirality, out Quaternion rotation)
		{
			return Eyes_TryGetEyeRotation_Injected(ref eyes, chirality, out rotation);
		}

		public bool TryGetFixationPoint(out Vector3 fixationPoint)
		{
			return Eyes_TryGetFixationPoint(this, out fixationPoint);
		}

		private static bool Eyes_TryGetFixationPoint(Eyes eyes, out Vector3 fixationPoint)
		{
			return Eyes_TryGetFixationPoint_Injected(ref eyes, out fixationPoint);
		}

		public bool TryGetLeftEyeOpenAmount(out float openAmount)
		{
			return Eyes_TryGetEyeOpenAmount(this, EyeSide.Left, out openAmount);
		}

		public bool TryGetRightEyeOpenAmount(out float openAmount)
		{
			return Eyes_TryGetEyeOpenAmount(this, EyeSide.Right, out openAmount);
		}

		private static bool Eyes_TryGetEyeOpenAmount(Eyes eyes, EyeSide chirality, out float openAmount)
		{
			return Eyes_TryGetEyeOpenAmount_Injected(ref eyes, chirality, out openAmount);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Eyes))
			{
				return false;
			}
			return Equals((Eyes)obj);
		}

		public bool Equals(Eyes other)
		{
			return deviceId == other.deviceId && featureIndex == other.featureIndex;
		}

		public override int GetHashCode()
		{
			return deviceId.GetHashCode() ^ (featureIndex.GetHashCode() << 1);
		}

		public static bool operator ==(Eyes a, Eyes b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(Eyes a, Eyes b)
		{
			return !(a == b);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Eyes_TryGetEyePosition_Injected(ref Eyes eyes, EyeSide chirality, out Vector3 position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Eyes_TryGetEyeRotation_Injected(ref Eyes eyes, EyeSide chirality, out Quaternion rotation);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Eyes_TryGetFixationPoint_Injected(ref Eyes eyes, out Vector3 fixationPoint);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Eyes_TryGetEyeOpenAmount_Injected(ref Eyes eyes, EyeSide chirality, out float openAmount);
	}
	[RequiredByNativeCode]
	[NativeConditional("ENABLE_VR")]
	[NativeHeader("Modules/XR/XRPrefix.h")]
	[NativeHeader("XRScriptingClasses.h")]
	[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputDevices.h")]
	[StaticAccessor("XRInputDevices::Get()", StaticAccessorType.Dot)]
	public struct Bone : IEquatable<Bone>
	{
		private ulong m_DeviceId;

		private uint m_FeatureIndex;

		internal ulong deviceId => m_DeviceId;

		internal uint featureIndex => m_FeatureIndex;

		public bool TryGetPosition(out Vector3 position)
		{
			return Bone_TryGetPosition(this, out position);
		}

		private static bool Bone_TryGetPosition(Bone bone, out Vector3 position)
		{
			return Bone_TryGetPosition_Injected(ref bone, out position);
		}

		public bool TryGetRotation(out Quaternion rotation)
		{
			return Bone_TryGetRotation(this, out rotation);
		}

		private static bool Bone_TryGetRotation(Bone bone, out Quaternion rotation)
		{
			return Bone_TryGetRotation_Injected(ref bone, out rotation);
		}

		public bool TryGetParentBone(out Bone parentBone)
		{
			return Bone_TryGetParentBone(this, out parentBone);
		}

		private static bool Bone_TryGetParentBone(Bone bone, out Bone parentBone)
		{
			return Bone_TryGetParentBone_Injected(ref bone, out parentBone);
		}

		public bool TryGetChildBones(List<Bone> childBones)
		{
			return Bone_TryGetChildBones(this, childBones);
		}

		private static bool Bone_TryGetChildBones(Bone bone, [NotNull("ArgumentNullException")] List<Bone> childBones)
		{
			return Bone_TryGetChildBones_Injected(ref bone, childBones);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Bone))
			{
				return false;
			}
			return Equals((Bone)obj);
		}

		public bool Equals(Bone other)
		{
			return deviceId == other.deviceId && featureIndex == other.featureIndex;
		}

		public override int GetHashCode()
		{
			return deviceId.GetHashCode() ^ (featureIndex.GetHashCode() << 1);
		}

		public static bool operator ==(Bone a, Bone b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(Bone a, Bone b)
		{
			return !(a == b);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Bone_TryGetPosition_Injected(ref Bone bone, out Vector3 position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Bone_TryGetRotation_Injected(ref Bone bone, out Quaternion rotation);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Bone_TryGetParentBone_Injected(ref Bone bone, out Bone parentBone);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Bone_TryGetChildBones_Injected(ref Bone bone, List<Bone> childBones);
	}
	[StructLayout(LayoutKind.Sequential)]
	[StaticAccessor("XRInputDevices::Get()", StaticAccessorType.Dot)]
	[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputDevices.h")]
	[NativeConditional("ENABLE_VR")]
	[UsedByNativeCode]
	public class InputDevices
	{
		private static List<InputDevice> s_InputDeviceList;

		public static event Action<InputDevice> deviceConnected;

		public static event Action<InputDevice> deviceDisconnected;

		public static event Action<InputDevice> deviceConfigChanged;

		public static InputDevice GetDeviceAtXRNode(XRNode node)
		{
			ulong deviceIdAtXRNode = InputTracking.GetDeviceIdAtXRNode(node);
			return new InputDevice(deviceIdAtXRNode);
		}

		public static void GetDevicesAtXRNode(XRNode node, List<InputDevice> inputDevices)
		{
			if (inputDevices == null)
			{
				throw new ArgumentNullException("inputDevices");
			}
			List<ulong> list = new List<ulong>();
			InputTracking.GetDeviceIdsAtXRNode_Internal(node, list);
			inputDevices.Clear();
			foreach (ulong item2 in list)
			{
				InputDevice item = new InputDevice(item2);
				if (item.isValid)
				{
					inputDevices.Add(item);
				}
			}
		}

		public static void GetDevices(List<InputDevice> inputDevices)
		{
			if (inputDevices == null)
			{
				throw new ArgumentNullException("inputDevices");
			}
			inputDevices.Clear();
			GetDevices_Internal(inputDevices);
		}

		[Obsolete("This API has been marked as deprecated and will be removed in future versions. Please use InputDevices.GetDevicesWithCharacteristics instead.")]
		public static void GetDevicesWithRole(InputDeviceRole role, List<InputDevice> inputDevices)
		{
			if (inputDevices == null)
			{
				throw new ArgumentNullException("inputDevices");
			}
			if (s_InputDeviceList == null)
			{
				s_InputDeviceList = new List<InputDevice>();
			}
			GetDevices_Internal(s_InputDeviceList);
			inputDevices.Clear();
			foreach (InputDevice s_InputDevice in s_InputDeviceList)
			{
				if (s_InputDevice.role == role)
				{
					inputDevices.Add(s_InputDevice);
				}
			}
		}

		public static void GetDevicesWithCharacteristics(InputDeviceCharacteristics desiredCharacteristics, List<InputDevice> inputDevices)
		{
			if (inputDevices == null)
			{
				throw new ArgumentNullException("inputDevices");
			}
			if (s_InputDeviceList == null)
			{
				s_InputDeviceList = new List<InputDevice>();
			}
			GetDevices_Internal(s_InputDeviceList);
			inputDevices.Clear();
			foreach (InputDevice s_InputDevice in s_InputDeviceList)
			{
				if ((s_InputDevice.characteristics & desiredCharacteristics) == desiredCharacteristics)
				{
					inputDevices.Add(s_InputDevice);
				}
			}
		}

		[RequiredByNativeCode]
		private static void InvokeConnectionEvent(ulong deviceId, ConnectionChangeType change)
		{
			switch (change)
			{
			case ConnectionChangeType.Connected:
				if (InputDevices.deviceConnected != null)
				{
					InputDevices.deviceConnected(new InputDevice(deviceId));
				}
				break;
			case ConnectionChangeType.Disconnected:
				if (InputDevices.deviceDisconnected != null)
				{
					InputDevices.deviceDisconnected(new InputDevice(deviceId));
				}
				break;
			case ConnectionChangeType.ConfigChange:
				if (InputDevices.deviceConfigChanged != null)
				{
					InputDevices.deviceConfigChanged(new InputDevice(deviceId));
				}
				break;
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDevices_Internal([NotNull("ArgumentNullException")] List<InputDevice> inputDevices);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool SendHapticImpulse(ulong deviceId, uint channel, float amplitude, float duration);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool SendHapticBuffer(ulong deviceId, uint channel, [NotNull("ArgumentNullException")] byte[] buffer);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetHapticCapabilities(ulong deviceId, out HapticCapabilities capabilities);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void StopHaptics(ulong deviceId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureUsages(ulong deviceId, [NotNull("ArgumentNullException")] List<InputFeatureUsage> featureUsages);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValue_bool(ulong deviceId, string usage, out bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValue_UInt32(ulong deviceId, string usage, out uint value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValue_float(ulong deviceId, string usage, out float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValue_Vector2f(ulong deviceId, string usage, out Vector2 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValue_Vector3f(ulong deviceId, string usage, out Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValue_Quaternionf(ulong deviceId, string usage, out Quaternion value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValue_Custom(ulong deviceId, string usage, [Out] byte[] value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValueAtTime_bool(ulong deviceId, string usage, long time, out bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValueAtTime_UInt32(ulong deviceId, string usage, long time, out uint value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValueAtTime_float(ulong deviceId, string usage, long time, out float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValueAtTime_Vector2f(ulong deviceId, string usage, long time, out Vector2 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValueAtTime_Vector3f(ulong deviceId, string usage, long time, out Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValueAtTime_Quaternionf(ulong deviceId, string usage, long time, out Quaternion value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValue_XRHand(ulong deviceId, string usage, out Hand value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValue_XRBone(ulong deviceId, string usage, out Bone value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool TryGetFeatureValue_XREyes(ulong deviceId, string usage, out Eyes value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsDeviceValid(ulong deviceId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetDeviceName(ulong deviceId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetDeviceManufacturer(ulong deviceId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetDeviceSerialNumber(ulong deviceId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern InputDeviceCharacteristics GetDeviceCharacteristics(ulong deviceId);

		internal static InputDeviceRole GetDeviceRole(ulong deviceId)
		{
			InputDeviceCharacteristics deviceCharacteristics = GetDeviceCharacteristics(deviceId);
			if ((deviceCharacteristics & (InputDeviceCharacteristics.HeadMounted | InputDeviceCharacteristics.TrackedDevice)) == (InputDeviceCharacteristics.HeadMounted | InputDeviceCharacteristics.TrackedDevice))
			{
				return InputDeviceRole.Generic;
			}
			if ((deviceCharacteristics & (InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.TrackedDevice | InputDeviceCharacteristics.Left)) == (InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.TrackedDevice | InputDeviceCharacteristics.Left))
			{
				return InputDeviceRole.LeftHanded;
			}
			if ((deviceCharacteristics & (InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.TrackedDevice | InputDeviceCharacteristics.Right)) == (InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.TrackedDevice | InputDeviceCharacteristics.Right))
			{
				return InputDeviceRole.RightHanded;
			}
			if ((deviceCharacteristics & InputDeviceCharacteristics.Controller) == InputDeviceCharacteristics.Controller)
			{
				return InputDeviceRole.GameController;
			}
			if ((deviceCharacteristics & (InputDeviceCharacteristics.TrackedDevice | InputDeviceCharacteristics.TrackingReference)) == (InputDeviceCharacteristics.TrackedDevice | InputDeviceCharacteristics.TrackingReference))
			{
				return InputDeviceRole.TrackingReference;
			}
			if ((deviceCharacteristics & InputDeviceCharacteristics.TrackedDevice) == InputDeviceCharacteristics.TrackedDevice)
			{
				return InputDeviceRole.HardwareTracker;
			}
			return InputDeviceRole.Unknown;
		}
	}
	[NativeConditional("ENABLE_XR")]
	[NativeHeader("Modules/XR/XRPrefix.h")]
	[UsedByNativeCode]
	[NativeType(Header = "Modules/XR/Subsystems/Display/XRDisplaySubsystem.h")]
	public class XRDisplaySubsystem : IntegratedSubsystem<XRDisplaySubsystemDescriptor>
	{
		[Flags]
		public enum FoveatedRenderingFlags
		{
			None = 0,
			GazeAllowed = 1
		}

		public enum LateLatchNode
		{
			Head,
			LeftHand,
			RightHand
		}

		[Flags]
		public enum TextureLayout
		{
			Texture2DArray = 1,
			SingleTexture2D = 2,
			SeparateTexture2Ds = 4
		}

		public enum ReprojectionMode
		{
			Unspecified,
			PositionAndOrientation,
			OrientationOnly,
			None
		}

		[NativeHeader("Modules/XR/Subsystems/Display/XRDisplaySubsystem.bindings.h")]
		public struct XRRenderParameter
		{
			public Matrix4x4 view;

			public Matrix4x4 projection;

			public Rect viewport;

			public Mesh occlusionMesh;

			public int textureArraySlice;

			public Matrix4x4 previousView;

			public bool isPreviousViewValid;
		}

		[NativeHeader("Modules/XR/Subsystems/Display/XRDisplaySubsystem.bindings.h")]
		[NativeHeader("Runtime/Graphics/RenderTextureDesc.h")]
		[NativeHeader("Runtime/Graphics/CommandBuffer/RenderingCommandBuffer.h")]
		public struct XRRenderPass
		{
			private IntPtr displaySubsystemInstance;

			public int renderPassIndex;

			public RenderTargetIdentifier renderTarget;

			public RenderTextureDescriptor renderTargetDesc;

			public bool hasMotionVectorPass;

			public RenderTargetIdentifier motionVectorRenderTarget;

			public RenderTextureDescriptor motionVectorRenderTargetDesc;

			public bool shouldFillOutDepth;

			public int cullingPassIndex;

			public IntPtr foveatedRenderingInfo;

			[NativeConditional("ENABLE_XR")]
			[NativeMethod(Name = "XRRenderPassScriptApi::GetRenderParameter", IsFreeFunction = true, HasExplicitThis = true, ThrowsException = true)]
			public void GetRenderParameter(Camera camera, int renderParameterIndex, out XRRenderParameter renderParameter)
			{
				GetRenderParameter_Injected(ref this, camera, renderParameterIndex, out renderParameter);
			}

			[NativeMethod(Name = "XRRenderPassScriptApi::GetRenderParameterCount", IsFreeFunction = true, HasExplicitThis = true)]
			[NativeConditional("ENABLE_XR")]
			public int GetRenderParameterCount()
			{
				return GetRenderParameterCount_Injected(ref this);
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetRenderParameter_Injected(ref XRRenderPass _unity_self, Camera camera, int renderParameterIndex, out XRRenderParameter renderParameter);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetRenderParameterCount_Injected(ref XRRenderPass _unity_self);
		}

		[NativeHeader("Runtime/Graphics/RenderTexture.h")]
		[NativeHeader("Modules/XR/Subsystems/Display/XRDisplaySubsystem.bindings.h")]
		public struct XRBlitParams
		{
			public RenderTexture srcTex;

			public int srcTexArraySlice;

			public Rect srcRect;

			public Rect destRect;

			public IntPtr foveatedRenderingInfo;

			public bool srcHdrEncoded;

			public ColorGamut srcHdrColorGamut;

			public int srcHdrMaxLuminance;
		}

		[NativeHeader("Modules/XR/Subsystems/Display/XRDisplaySubsystem.bindings.h")]
		public struct XRMirrorViewBlitDesc
		{
			private IntPtr displaySubsystemInstance;

			public bool nativeBlitAvailable;

			public bool nativeBlitInvalidStates;

			public int blitParamsCount;

			[NativeMethod(Name = "XRMirrorViewBlitDescScriptApi::GetBlitParameter", IsFreeFunction = true, HasExplicitThis = true)]
			[NativeConditional("ENABLE_XR")]
			public void GetBlitParameter(int blitParameterIndex, out XRBlitParams blitParameter)
			{
				GetBlitParameter_Injected(ref this, blitParameterIndex, out blitParameter);
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetBlitParameter_Injected(ref XRMirrorViewBlitDesc _unity_self, int blitParameterIndex, out XRBlitParams blitParameter);
		}

		private HDROutputSettings m_HDROutputSettings;

		[Obsolete("singlePassRenderingDisabled{get;set;} is deprecated. Use textureLayout and supportedTextureLayouts instead.", false)]
		public bool singlePassRenderingDisabled
		{
			get
			{
				return (textureLayout & TextureLayout.Texture2DArray) == 0;
			}
			set
			{
				if (value)
				{
					textureLayout = TextureLayout.SeparateTexture2Ds;
				}
				else if ((supportedTextureLayouts & TextureLayout.Texture2DArray) > (TextureLayout)0)
				{
					textureLayout = TextureLayout.Texture2DArray;
				}
			}
		}

		public extern bool displayOpaque
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool contentProtectionEnabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float scaleOfAllViewports
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float scaleOfAllRenderTargets
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float zNear
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float zFar
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool sRGB
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float occlusionMaskScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float foveatedRenderingLevel
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern FoveatedRenderingFlags foveatedRenderingFlags
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern TextureLayout textureLayout
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern TextureLayout supportedTextureLayouts
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern ReprojectionMode reprojectionMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool disableLegacyRenderer
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public HDROutputSettings hdrOutputSettings
		{
			get
			{
				if (m_HDROutputSettings == null)
				{
					m_HDROutputSettings = new HDROutputSettings(-1);
				}
				return m_HDROutputSettings;
			}
		}

		public event Action<bool> displayFocusChanged;

		[RequiredByNativeCode]
		private void InvokeDisplayFocusChanged(bool focus)
		{
			if (this.displayFocusChanged != null)
			{
				this.displayFocusChanged(focus);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void MarkTransformLateLatched(Transform transform, LateLatchNode nodeType);

		public void SetFocusPlane(Vector3 point, Vector3 normal, Vector3 velocity)
		{
			SetFocusPlane_Injected(ref point, ref normal, ref velocity);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetMSAALevel(int level);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetRenderPassCount();

		public void GetRenderPass(int renderPassIndex, out XRRenderPass renderPass)
		{
			if (!Internal_TryGetRenderPass(renderPassIndex, out renderPass))
			{
				throw new IndexOutOfRangeException("renderPassIndex");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("TryGetRenderPass")]
		private extern bool Internal_TryGetRenderPass(int renderPassIndex, out XRRenderPass renderPass);

		public void EndRecordingIfLateLatched(Camera camera)
		{
			if (!Internal_TryEndRecordingIfLateLatched(camera) && camera == null)
			{
				throw new ArgumentNullException("camera");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("TryEndRecordingIfLateLatched")]
		private extern bool Internal_TryEndRecordingIfLateLatched(Camera camera);

		public void BeginRecordingIfLateLatched(Camera camera)
		{
			if (!Internal_TryBeginRecordingIfLateLatched(camera) && camera == null)
			{
				throw new ArgumentNullException("camera");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("TryBeginRecordingIfLateLatched")]
		private extern bool Internal_TryBeginRecordingIfLateLatched(Camera camera);

		public void GetCullingParameters(Camera camera, int cullingPassIndex, out ScriptableCullingParameters scriptableCullingParameters)
		{
			if (!Internal_TryGetCullingParams(camera, cullingPassIndex, out scriptableCullingParameters))
			{
				if (camera == null)
				{
					throw new ArgumentNullException("camera");
				}
				throw new IndexOutOfRangeException("cullingPassIndex");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeHeader("Runtime/Graphics/ScriptableRenderLoop/ScriptableCulling.h")]
		[NativeMethod("TryGetCullingParams")]
		private extern bool Internal_TryGetCullingParams(Camera camera, int cullingPassIndex, out ScriptableCullingParameters scriptableCullingParameters);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("TryGetAppGPUTimeLastFrame")]
		public extern bool TryGetAppGPUTimeLastFrame(out float gpuTimeLastFrame);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("TryGetCompositorGPUTimeLastFrame")]
		public extern bool TryGetCompositorGPUTimeLastFrame(out float gpuTimeLastFrameCompositor);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("TryGetDroppedFrameCount")]
		public extern bool TryGetDroppedFrameCount(out int droppedFrameCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("TryGetFramePresentCount")]
		public extern bool TryGetFramePresentCount(out int framePresentCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("TryGetDisplayRefreshRate")]
		public extern bool TryGetDisplayRefreshRate(out float displayRefreshRate);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("TryGetMotionToPhoton")]
		public extern bool TryGetMotionToPhoton(out float motionToPhoton);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeConditional("ENABLE_XR")]
		[NativeMethod(Name = "UnityXRRenderTextureIdToRenderTexture", IsThreadSafe = false)]
		public extern RenderTexture GetRenderTexture(uint unityXrRenderTextureId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeConditional("ENABLE_XR")]
		[NativeMethod(Name = "GetTextureForRenderPass", IsThreadSafe = false)]
		public extern RenderTexture GetRenderTextureForRenderPass(int renderPass);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeConditional("ENABLE_XR")]
		[NativeMethod(Name = "GetSharedDepthTextureForRenderPass", IsThreadSafe = false)]
		public extern RenderTexture GetSharedDepthTextureForRenderPass(int renderPass);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeConditional("ENABLE_XR")]
		[NativeMethod(Name = "GetPreferredMirrorViewBlitMode", IsThreadSafe = false)]
		public extern int GetPreferredMirrorBlitMode();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "SetPreferredMirrorViewBlitMode", IsThreadSafe = false)]
		[NativeConditional("ENABLE_XR")]
		public extern void SetPreferredMirrorBlitMode(int blitMode);

		[Obsolete("GetMirrorViewBlitDesc(RenderTexture, out XRMirrorViewBlitDesc) is deprecated. Use GetMirrorViewBlitDesc(RenderTexture, out XRMirrorViewBlitDesc, int) instead.", false)]
		public bool GetMirrorViewBlitDesc(RenderTexture mirrorRt, out XRMirrorViewBlitDesc outDesc)
		{
			return GetMirrorViewBlitDesc(mirrorRt, out outDesc, -1);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "QueryMirrorViewBlitDesc", IsThreadSafe = false)]
		[NativeConditional("ENABLE_XR")]
		public extern bool GetMirrorViewBlitDesc(RenderTexture mirrorRt, out XRMirrorViewBlitDesc outDesc, int mode);

		[Obsolete("AddGraphicsThreadMirrorViewBlit(CommandBuffer, bool) is deprecated. Use AddGraphicsThreadMirrorViewBlit(CommandBuffer, bool, int) instead.", false)]
		public bool AddGraphicsThreadMirrorViewBlit(CommandBuffer cmd, bool allowGraphicsStateInvalidate)
		{
			return AddGraphicsThreadMirrorViewBlit(cmd, allowGraphicsStateInvalidate, -1);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "AddGraphicsThreadMirrorViewBlit", IsThreadSafe = false)]
		[NativeHeader("Runtime/Graphics/CommandBuffer/RenderingCommandBuffer.h")]
		[NativeConditional("ENABLE_XR")]
		public extern bool AddGraphicsThreadMirrorViewBlit(CommandBuffer cmd, bool allowGraphicsStateInvalidate, int mode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFocusPlane_Injected(ref Vector3 point, ref Vector3 normal, ref Vector3 velocity);
	}
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct XRMirrorViewBlitMode
	{
		public const int Default = 0;

		public const int LeftEye = -1;

		public const int RightEye = -2;

		public const int SideBySide = -3;

		public const int SideBySideOcclusionMesh = -4;

		public const int Distort = -5;

		public const int None = -6;
	}
	[NativeType(Header = "Modules/XR/Subsystems/Display/XRDisplaySubsystemDescriptor.h")]
	[NativeHeader("Modules/XR/XRPrefix.h")]
	public struct XRMirrorViewBlitModeDesc
	{
		public int blitMode;

		public string blitModeDesc;
	}
	[NativeType(Header = "Modules/XR/Subsystems/Display/XRDisplaySubsystemDescriptor.h")]
	[UsedByNativeCode]
	public class XRDisplaySubsystemDescriptor : IntegratedSubsystemDescriptor<XRDisplaySubsystem>
	{
		[NativeConditional("ENABLE_XR")]
		public extern bool disablesLegacyVr
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeConditional("ENABLE_XR")]
		public extern bool enableBackBufferMSAA
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeConditional("ENABLE_XR")]
		[NativeMethod("TryGetAvailableMirrorModeCount")]
		public extern int GetAvailableMirrorBlitModeCount();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeConditional("ENABLE_XR")]
		[NativeMethod("TryGetMirrorModeByIndex")]
		public extern void GetMirrorBlitModeByIndex(int index, out XRMirrorViewBlitModeDesc mode);
	}
	public enum TrackingOriginModeFlags
	{
		Unknown = 0,
		Device = 1,
		Floor = 2,
		TrackingReference = 4,
		Unbounded = 8
	}
	[NativeType(Header = "Modules/XR/Subsystems/Input/XRInputSubsystem.h")]
	[NativeConditional("ENABLE_XR")]
	[UsedByNativeCode]
	public class XRInputSubsystem : IntegratedSubsystem<XRInputSubsystemDescriptor>
	{
		private List<ulong> m_DeviceIdsCache;

		public event Action<XRInputSubsystem> trackingOriginUpdated;

		public event Action<XRInputSubsystem> boundaryChanged;

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern uint GetIndex();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool TryRecenter();

		public bool TryGetInputDevices(List<InputDevice> devices)
		{
			if (devices == null)
			{
				throw new ArgumentNullException("devices");
			}
			devices.Clear();
			if (m_DeviceIdsCache == null)
			{
				m_DeviceIdsCache = new List<ulong>();
			}
			m_DeviceIdsCache.Clear();
			TryGetDeviceIds_AsList(m_DeviceIdsCache);
			for (int i = 0; i < m_DeviceIdsCache.Count; i++)
			{
				devices.Add(new InputDevice(m_DeviceIdsCache[i]));
			}
			return true;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool TrySetTrackingOriginMode(TrackingOriginModeFlags origin);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern TrackingOriginModeFlags GetTrackingOriginMode();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern TrackingOriginModeFlags GetSupportedTrackingOriginModes();

		public bool TryGetBoundaryPoints(List<Vector3> boundaryPoints)
		{
			if (boundaryPoints == null)
			{
				throw new ArgumentNullException("boundaryPoints");
			}
			return TryGetBoundaryPoints_AsList(boundaryPoints);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool TryGetBoundaryPoints_AsList(List<Vector3> boundaryPoints);

		[RequiredByNativeCode(GenerateProxy = true)]
		private static void InvokeTrackingOriginUpdatedEvent(IntPtr internalPtr)
		{
			IntegratedSubsystem integratedSubsystemByPtr = SubsystemManager.GetIntegratedSubsystemByPtr(internalPtr);
			if (integratedSubsystemByPtr is XRInputSubsystem { trackingOriginUpdated: not null } xRInputSubsystem)
			{
				xRInputSubsystem.trackingOriginUpdated(xRInputSubsystem);
			}
		}

		[RequiredByNativeCode(GenerateProxy = true)]
		private static void InvokeBoundaryChangedEvent(IntPtr internalPtr)
		{
			IntegratedSubsystem integratedSubsystemByPtr = SubsystemManager.GetIntegratedSubsystemByPtr(internalPtr);
			if (integratedSubsystemByPtr is XRInputSubsystem { boundaryChanged: not null } xRInputSubsystem)
			{
				xRInputSubsystem.boundaryChanged(xRInputSubsystem);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void TryGetDeviceIds_AsList(List<ulong> deviceIds);
	}
	[UsedByNativeCode]
	[NativeType(Header = "Modules/XR/Subsystems/Input/XRInputSubsystemDescriptor.h")]
	[NativeConditional("ENABLE_XR")]
	[NativeHeader("Modules/XR/XRPrefix.h")]
	public class XRInputSubsystemDescriptor : IntegratedSubsystemDescriptor<XRInputSubsystem>
	{
		[NativeConditional("ENABLE_XR")]
		public extern bool disablesLegacyInput
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
	}
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	[UsedByNativeCode]
	public struct MeshId : IEquatable<MeshId>
	{
		private static MeshId s_InvalidId = default(MeshId);

		private ulong m_SubId1;

		private ulong m_SubId2;

		public static MeshId InvalidId => s_InvalidId;

		public override string ToString()
		{
			return string.Format("{0}-{1}", m_SubId1.ToString("X16"), m_SubId2.ToString("X16"));
		}

		public override int GetHashCode()
		{
			return m_SubId1.GetHashCode() ^ m_SubId2.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return obj is MeshId && Equals((MeshId)obj);
		}

		public bool Equals(MeshId other)
		{
			return m_SubId1 == other.m_SubId1 && m_SubId2 == other.m_SubId2;
		}

		public static bool operator ==(MeshId id1, MeshId id2)
		{
			return id1.m_SubId1 == id2.m_SubId1 && id1.m_SubId2 == id2.m_SubId2;
		}

		public static bool operator !=(MeshId id1, MeshId id2)
		{
			return id1.m_SubId1 != id2.m_SubId1 || id1.m_SubId2 != id2.m_SubId2;
		}
	}
	[RequiredByNativeCode]
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	public enum MeshGenerationStatus
	{
		Success,
		InvalidMeshId,
		GenerationAlreadyInProgress,
		Canceled,
		UnknownError
	}
	internal static class HashCodeHelper
	{
		private const int k_HashCodeMultiplier = 486187739;

		public static int Combine(int hash1, int hash2)
		{
			return hash1 * 486187739 + hash2;
		}

		public static int Combine(int hash1, int hash2, int hash3)
		{
			return Combine(Combine(hash1, hash2), hash3);
		}

		public static int Combine(int hash1, int hash2, int hash3, int hash4)
		{
			return Combine(Combine(hash1, hash2, hash3), hash4);
		}

		public static int Combine(int hash1, int hash2, int hash3, int hash4, int hash5)
		{
			return Combine(Combine(hash1, hash2, hash3, hash4), hash5);
		}

		public static int Combine(int hash1, int hash2, int hash3, int hash4, int hash5, int hash6)
		{
			return Combine(Combine(hash1, hash2, hash3, hash4, hash5), hash6);
		}

		public static int Combine(int hash1, int hash2, int hash3, int hash4, int hash5, int hash6, int hash7)
		{
			return Combine(Combine(hash1, hash2, hash3, hash4, hash5, hash6), hash7);
		}

		public static int Combine(int hash1, int hash2, int hash3, int hash4, int hash5, int hash6, int hash7, int hash8)
		{
			return Combine(Combine(hash1, hash2, hash3, hash4, hash5, hash6, hash7), hash8);
		}
	}
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	[RequiredByNativeCode]
	public struct MeshGenerationResult : IEquatable<MeshGenerationResult>
	{
		public MeshId MeshId { get; }

		public Mesh Mesh { get; }

		public MeshCollider MeshCollider { get; }

		public MeshGenerationStatus Status { get; }

		public MeshVertexAttributes Attributes { get; }

		public ulong Timestamp { get; }

		public Vector3 Position { get; }

		public Quaternion Rotation { get; }

		public Vector3 Scale { get; }

		public override bool Equals(object obj)
		{
			if (!(obj is MeshGenerationResult))
			{
				return false;
			}
			return Equals((MeshGenerationResult)obj);
		}

		public bool Equals(MeshGenerationResult other)
		{
			return MeshId.Equals(other.MeshId) && Mesh.Equals(other.Mesh) && MeshCollider.Equals(other.MeshCollider) && Status == other.Status && Attributes == other.Attributes && Position.Equals(other.Position) && Rotation.Equals(other.Rotation) && Scale.Equals(other.Scale);
		}

		public static bool operator ==(MeshGenerationResult lhs, MeshGenerationResult rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(MeshGenerationResult lhs, MeshGenerationResult rhs)
		{
			return !lhs.Equals(rhs);
		}

		public override int GetHashCode()
		{
			return HashCodeHelper.Combine(MeshId.GetHashCode(), Mesh.GetHashCode(), MeshCollider.GetHashCode(), ((int)Status).GetHashCode(), ((int)Attributes).GetHashCode(), Position.GetHashCode(), Rotation.GetHashCode(), Scale.GetHashCode());
		}
	}
	[UsedByNativeCode]
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	[Flags]
	public enum MeshVertexAttributes
	{
		None = 0,
		Normals = 1,
		Tangents = 2,
		UVs = 4,
		Colors = 8
	}
	[UsedByNativeCode]
	[Flags]
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	public enum MeshGenerationOptions
	{
		None = 0,
		ConsumeTransform = 1
	}
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	[UsedByNativeCode]
	public enum MeshChangeState
	{
		Added,
		Updated,
		Removed,
		Unchanged
	}
	[UsedByNativeCode]
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	public struct MeshInfo : IEquatable<MeshInfo>
	{
		public MeshId MeshId { get; set; }

		public MeshChangeState ChangeState { get; set; }

		public int PriorityHint { get; set; }

		public override bool Equals(object obj)
		{
			if (!(obj is MeshInfo))
			{
				return false;
			}
			return Equals((MeshInfo)obj);
		}

		public bool Equals(MeshInfo other)
		{
			return MeshId.Equals(other.MeshId) && ChangeState.Equals(other.ChangeState) && PriorityHint.Equals(other.PriorityHint);
		}

		public static bool operator ==(MeshInfo lhs, MeshInfo rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(MeshInfo lhs, MeshInfo rhs)
		{
			return !lhs.Equals(rhs);
		}

		public override int GetHashCode()
		{
			return HashCodeHelper.Combine(MeshId.GetHashCode(), ((int)ChangeState).GetHashCode(), PriorityHint.GetHashCode());
		}
	}
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshBindings.h")]
	[UsedByNativeCode]
	public readonly struct MeshTransform : IEquatable<MeshTransform>
	{
		public MeshId MeshId { get; }

		public ulong Timestamp { get; }

		public Vector3 Position { get; }

		public Quaternion Rotation { get; }

		public Vector3 Scale { get; }

		public MeshTransform(in MeshId meshId, ulong timestamp, in Vector3 position, in Quaternion rotation, in Vector3 scale)
		{
			MeshId = meshId;
			Timestamp = timestamp;
			Position = position;
			Rotation = rotation;
			Scale = scale;
		}

		public override bool Equals(object obj)
		{
			return obj is MeshTransform other && Equals(other);
		}

		public bool Equals(MeshTransform other)
		{
			return MeshId.Equals(other.MeshId) && Timestamp == other.Timestamp && Position.Equals(other.Position) && Rotation.Equals(other.Rotation) && Scale.Equals(other.Scale);
		}

		public static bool operator ==(MeshTransform lhs, MeshTransform rhs)
		{
			return lhs.Equals(rhs);
		}

		public static bool operator !=(MeshTransform lhs, MeshTransform rhs)
		{
			return !lhs.Equals(rhs);
		}

		public override int GetHashCode()
		{
			return HashCodeHelper.Combine(MeshId.GetHashCode(), Timestamp.GetHashCode(), Position.GetHashCode(), Rotation.GetHashCode(), Scale.GetHashCode());
		}
	}
	[NativeHeader("Modules/XR/XRPrefix.h")]
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshingSubsystem.h")]
	[NativeConditional("ENABLE_XR")]
	[UsedByNativeCode]
	public class XRMeshSubsystem : IntegratedSubsystem<XRMeshSubsystemDescriptor>
	{
		[NativeConditional("ENABLE_XR")]
		private readonly struct MeshTransformList : IDisposable
		{
			private readonly IntPtr m_Self;

			public int Count => GetLength(m_Self);

			public IntPtr Data => GetData(m_Self);

			public MeshTransformList(IntPtr self)
			{
				m_Self = self;
			}

			public void Dispose()
			{
				Dispose(m_Self);
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("UnityXRMeshTransformList_get_Length")]
			private static extern int GetLength(IntPtr self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("UnityXRMeshTransformList_get_Data")]
			private static extern IntPtr GetData(IntPtr self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("UnityXRMeshTransformList_Dispose")]
			private static extern void Dispose(IntPtr self);
		}

		public extern float meshDensity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public bool TryGetMeshInfos(List<MeshInfo> meshInfosOut)
		{
			if (meshInfosOut == null)
			{
				throw new ArgumentNullException("meshInfosOut");
			}
			return GetMeshInfosAsList(meshInfosOut);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetMeshInfosAsList(List<MeshInfo> meshInfos);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern MeshInfo[] GetMeshInfosAsFixedArray();

		public void GenerateMeshAsync(MeshId meshId, Mesh mesh, MeshCollider meshCollider, MeshVertexAttributes attributes, Action<MeshGenerationResult> onMeshGenerationComplete)
		{
			GenerateMeshAsync(meshId, mesh, meshCollider, attributes, onMeshGenerationComplete, MeshGenerationOptions.None);
		}

		public void GenerateMeshAsync(MeshId meshId, Mesh mesh, MeshCollider meshCollider, MeshVertexAttributes attributes, Action<MeshGenerationResult> onMeshGenerationComplete, MeshGenerationOptions options)
		{
			GenerateMeshAsync_Injected(ref meshId, mesh, meshCollider, attributes, onMeshGenerationComplete, options);
		}

		[RequiredByNativeCode]
		private void InvokeMeshReadyDelegate(MeshGenerationResult result, Action<MeshGenerationResult> onMeshGenerationComplete)
		{
			onMeshGenerationComplete?.Invoke(result);
		}

		public bool SetBoundingVolume(Vector3 origin, Vector3 extents)
		{
			return SetBoundingVolume_Injected(ref origin, ref extents);
		}

		public unsafe NativeArray<MeshTransform> GetUpdatedMeshTransforms(Allocator allocator)
		{
			using MeshTransformList meshTransformList = new MeshTransformList(GetUpdatedMeshTransforms());
			NativeArray<MeshTransform> nativeArray = new NativeArray<MeshTransform>(meshTransformList.Count, allocator, NativeArrayOptions.UninitializedMemory);
			UnsafeUtility.MemCpy(nativeArray.GetUnsafePtr(), meshTransformList.Data.ToPointer(), meshTransformList.Count * sizeof(MeshTransform));
			return nativeArray;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetUpdatedMeshTransforms();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GenerateMeshAsync_Injected(ref MeshId meshId, Mesh mesh, MeshCollider meshCollider, MeshVertexAttributes attributes, Action<MeshGenerationResult> onMeshGenerationComplete, MeshGenerationOptions options);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool SetBoundingVolume_Injected(ref Vector3 origin, ref Vector3 extents);
	}
	[NativeType(Header = "Modules/XR/Subsystems/Planes/XRMeshSubsystemDescriptor.h")]
	[NativeHeader("Modules/XR/XRPrefix.h")]
	[UsedByNativeCode]
	public class XRMeshSubsystemDescriptor : IntegratedSubsystemDescriptor<XRMeshSubsystem>
	{
	}
}
namespace UnityEngine.XR.Provider
{
	public static class XRStats
	{
		public static bool TryGetStat(IntegratedSubsystem xrSubsystem, string tag, out float value)
		{
			return TryGetStat_Internal(xrSubsystem.m_Ptr, tag, out value);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeHeader("Modules/XR/Stats/XRStats.h")]
		[NativeConditional("ENABLE_XR")]
		[StaticAccessor("XRStats::Get()", StaticAccessorType.Dot)]
		[NativeMethod("TryGetStatByName_Internal")]
		private static extern bool TryGetStat_Internal(IntPtr ptr, string tag, out float value);
	}
}
