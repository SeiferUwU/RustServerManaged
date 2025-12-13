using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

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
[assembly: InternalsVisibleTo("UnityEngine.SpriteShapeModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteMaskModule")]
[assembly: InternalsVisibleTo("UnityEngine.ScreenCaptureModule")]
[assembly: InternalsVisibleTo("UnityEngine.RuntimeInitializeOnLoadManagerInitializerModule")]
[assembly: InternalsVisibleTo("UnityEngine.PropertiesModule")]
[assembly: InternalsVisibleTo("UnityEngine.ProfilerModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.Physics2DModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestWWWModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestTextureModule")]
[assembly: InternalsVisibleTo("UnityEngine.PerformanceReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.ParticleSystemModule")]
[assembly: InternalsVisibleTo("UnityEngine.NVIDIAModule")]
[assembly: InternalsVisibleTo("UnityEngine.AudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.ClothModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterRendererModule")]
[assembly: InternalsVisibleTo("UnityEngine.ContentLoadModule")]
[assembly: InternalsVisibleTo("UnityEngine.LocalizationModule")]
[assembly: InternalsVisibleTo("UnityEngine.IMGUIModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreTextEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreFontEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputLegacyModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextRenderingModule")]
[assembly: InternalsVisibleTo("UnityEngine.GridModule")]
[assembly: InternalsVisibleTo("UnityEngine.GameCenterModule")]
[assembly: InternalsVisibleTo("UnityEngine.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.ImageConversionModule")]
[assembly: InternalsVisibleTo("UnityEngine.DirectorModule")]
[assembly: InternalsVisibleTo("UnityEngine.DSPGraphModule")]
[assembly: InternalsVisibleTo("UnityEngine.CrashReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestModule")]
[assembly: InternalsVisibleTo("UnityEngine.TLSModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommonModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityConnectModule")]
[assembly: InternalsVisibleTo("UnityEngine.GIModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommon")]
[assembly: InternalsVisibleTo("UnityEngine.Purchasing")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.011")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.010")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.009")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.008")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.007")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.006")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.001")]
[assembly: InternalsVisibleTo("UnityEngine.Core.Runtime.Tests")]
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
[assembly: InternalsVisibleTo("Unity.Runtime")]
[assembly: InternalsVisibleTo("Unity.Collections")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.012")]
[assembly: InternalsVisibleTo("Unity.Entities.Tests")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.013")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.015")]
[assembly: InternalsVisibleTo("Unity.Subsystem.Registration")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.001")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.024")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.023")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.022")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.021")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.020")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.019")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.018")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.017")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.016")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.014")]
[assembly: InternalsVisibleTo("Unity.Entities")]
[assembly: InternalsVisibleTo("Unity.Core")]
[assembly: InternalsVisibleTo("Unity.Services.QoS")]
[assembly: InternalsVisibleTo("Unity.Timeline")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.AllIn1Runner")]
[assembly: InternalsVisibleTo("Unity.Logging")]
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
[assembly: InternalsVisibleTo("Assembly-CSharp-testable")]
[assembly: InternalsVisibleTo("Assembly-CSharp-firstpass-testable")]
[assembly: InternalsVisibleTo("Unity.PerformanceTests.RuntimeTestRunner.Tests")]
[assembly: InternalsVisibleTo("GoogleAR.UnityNative")]
[assembly: InternalsVisibleTo("Unity.ucg.QoS")]
[assembly: InternalsVisibleTo("Unity.Networking.Transport")]
[assembly: InternalsVisibleTo("UnityEngine.UI")]
[assembly: InternalsVisibleTo("UnityEngine.SpatialTracking")]
[assembly: InternalsVisibleTo("UnityEngine.UIElements.Tests")]
[assembly: InternalsVisibleTo("Unity.UIElements.PlayModeTests")]
[assembly: InternalsVisibleTo("Unity.UIElements.Editor")]
[assembly: InternalsVisibleTo("Unity.UIElements.EditorTests")]
[assembly: InternalsVisibleTo("Unity.UIElements")]
[assembly: InternalsVisibleTo("Unity.WindowsMRAutomation")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.Editor")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsGameObjectsModule")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.EditorTests")]
[assembly: UnityEngineModuleAssembly]
[assembly: InternalsVisibleTo("UnityEditor.UIBuilderModule")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.EditorTests")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.Editor")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace UnityEngine;

public enum RigidbodyConstraints
{
	None = 0,
	FreezePositionX = 2,
	FreezePositionY = 4,
	FreezePositionZ = 8,
	FreezeRotationX = 16,
	FreezeRotationY = 32,
	FreezeRotationZ = 64,
	FreezePosition = 14,
	FreezeRotation = 112,
	FreezeAll = 126
}
public enum ForceMode
{
	Force = 0,
	Acceleration = 5,
	Impulse = 1,
	VelocityChange = 2
}
public enum JointProjectionMode
{
	None,
	PositionAndRotation,
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("JointProjectionMode.PositionOnly is no longer supported", true)]
	PositionOnly
}
[Flags]
public enum MeshColliderCookingOptions
{
	None = 0,
	[Obsolete("No longer used because the problem this was trying to solve is gone since Unity 2018.3", true)]
	InflateConvexMesh = 1,
	CookForFasterSimulation = 2,
	EnableMeshCleaning = 4,
	WeldColocatedVertices = 8,
	UseFastMidphase = 0x10
}
public struct WheelFrictionCurve
{
	private float m_ExtremumSlip;

	private float m_ExtremumValue;

	private float m_AsymptoteSlip;

	private float m_AsymptoteValue;

	private float m_Stiffness;

	public float extremumSlip
	{
		get
		{
			return m_ExtremumSlip;
		}
		set
		{
			m_ExtremumSlip = value;
		}
	}

	public float extremumValue
	{
		get
		{
			return m_ExtremumValue;
		}
		set
		{
			m_ExtremumValue = value;
		}
	}

	public float asymptoteSlip
	{
		get
		{
			return m_AsymptoteSlip;
		}
		set
		{
			m_AsymptoteSlip = value;
		}
	}

	public float asymptoteValue
	{
		get
		{
			return m_AsymptoteValue;
		}
		set
		{
			m_AsymptoteValue = value;
		}
	}

	public float stiffness
	{
		get
		{
			return m_Stiffness;
		}
		set
		{
			m_Stiffness = value;
		}
	}
}
public struct SoftJointLimit
{
	private float m_Limit;

	private float m_Bounciness;

	private float m_ContactDistance;

	public float limit
	{
		get
		{
			return m_Limit;
		}
		set
		{
			m_Limit = value;
		}
	}

	public float bounciness
	{
		get
		{
			return m_Bounciness;
		}
		set
		{
			m_Bounciness = value;
		}
	}

	public float contactDistance
	{
		get
		{
			return m_ContactDistance;
		}
		set
		{
			m_ContactDistance = value;
		}
	}

	[Obsolete("Spring has been moved to SoftJointLimitSpring class in Unity 5", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public float spring
	{
		get
		{
			return 0f;
		}
		set
		{
		}
	}

	[Obsolete("Damper has been moved to SoftJointLimitSpring class in Unity 5", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public float damper
	{
		get
		{
			return 0f;
		}
		set
		{
		}
	}

	[Obsolete("Use SoftJointLimit.bounciness instead", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public float bouncyness
	{
		get
		{
			return m_Bounciness;
		}
		set
		{
			m_Bounciness = value;
		}
	}
}
public struct SoftJointLimitSpring
{
	private float m_Spring;

	private float m_Damper;

	public float spring
	{
		get
		{
			return m_Spring;
		}
		set
		{
			m_Spring = value;
		}
	}

	public float damper
	{
		get
		{
			return m_Damper;
		}
		set
		{
			m_Damper = value;
		}
	}
}
public struct JointDrive
{
	private float m_PositionSpring;

	private float m_PositionDamper;

	private float m_MaximumForce;

	private int m_UseAcceleration;

	public float positionSpring
	{
		get
		{
			return m_PositionSpring;
		}
		set
		{
			m_PositionSpring = value;
		}
	}

	public float positionDamper
	{
		get
		{
			return m_PositionDamper;
		}
		set
		{
			m_PositionDamper = value;
		}
	}

	public float maximumForce
	{
		get
		{
			return m_MaximumForce;
		}
		set
		{
			m_MaximumForce = value;
		}
	}

	public bool useAcceleration
	{
		get
		{
			return m_UseAcceleration == 1;
		}
		set
		{
			m_UseAcceleration = (value ? 1 : 0);
		}
	}

	[Obsolete("JointDriveMode is obsolete")]
	public JointDriveMode mode
	{
		get
		{
			return JointDriveMode.None;
		}
		set
		{
		}
	}
}
public enum RigidbodyInterpolation
{
	None,
	Interpolate,
	Extrapolate
}
public struct JointMotor
{
	private float m_TargetVelocity;

	private float m_Force;

	private int m_FreeSpin;

	public float targetVelocity
	{
		get
		{
			return m_TargetVelocity;
		}
		set
		{
			m_TargetVelocity = value;
		}
	}

	public float force
	{
		get
		{
			return m_Force;
		}
		set
		{
			m_Force = value;
		}
	}

	public bool freeSpin
	{
		get
		{
			return m_FreeSpin == 1;
		}
		set
		{
			m_FreeSpin = (value ? 1 : 0);
		}
	}
}
public struct JointSpring
{
	public float spring;

	public float damper;

	public float targetPosition;
}
public struct JointLimits
{
	private float m_Min;

	private float m_Max;

	private float m_Bounciness;

	private float m_BounceMinVelocity;

	private float m_ContactDistance;

	[Obsolete("minBounce and maxBounce are replaced by a single JointLimits.bounciness for both limit ends.", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public float minBounce;

	[Obsolete("minBounce and maxBounce are replaced by a single JointLimits.bounciness for both limit ends.", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public float maxBounce;

	public float min
	{
		get
		{
			return m_Min;
		}
		set
		{
			m_Min = value;
		}
	}

	public float max
	{
		get
		{
			return m_Max;
		}
		set
		{
			m_Max = value;
		}
	}

	public float bounciness
	{
		get
		{
			return m_Bounciness;
		}
		set
		{
			m_Bounciness = value;
		}
	}

	public float bounceMinVelocity
	{
		get
		{
			return m_BounceMinVelocity;
		}
		set
		{
			m_BounceMinVelocity = value;
		}
	}

	public float contactDistance
	{
		get
		{
			return m_ContactDistance;
		}
		set
		{
			m_ContactDistance = value;
		}
	}
}
[StructLayout(LayoutKind.Sequential)]
[RequiredByNativeCode]
public class ControllerColliderHit
{
	internal CharacterController m_Controller;

	internal Collider m_Collider;

	internal Vector3 m_Point;

	internal Vector3 m_Normal;

	internal Vector3 m_MoveDirection;

	internal float m_MoveLength;

	internal int m_Push;

	public CharacterController controller => m_Controller;

	public Collider collider => m_Collider;

	public Rigidbody rigidbody => m_Collider.attachedRigidbody;

	public GameObject gameObject => m_Collider.gameObject;

	public Transform transform => m_Collider.transform;

	public Vector3 point => m_Point;

	public Vector3 normal => m_Normal;

	public Vector3 moveDirection => m_MoveDirection;

	public float moveLength => m_MoveLength;

	private bool push
	{
		get
		{
			return m_Push != 0;
		}
		set
		{
			m_Push = (value ? 1 : 0);
		}
	}
}
public enum PhysicMaterialCombine
{
	Average = 0,
	Minimum = 2,
	Multiply = 1,
	Maximum = 3
}
public class Collision
{
	private ContactPairHeader m_Header;

	private ContactPair m_Pair;

	private bool m_Flipped;

	private ContactPoint[] m_LegacyContacts = null;

	public Vector3 impulse => m_Pair.ImpulseSum;

	public Vector3 relativeVelocity => m_Flipped ? m_Header.m_RelativeVelocity : (-m_Header.m_RelativeVelocity);

	public Rigidbody rigidbody => body as Rigidbody;

	public ArticulationBody articulationBody => body as ArticulationBody;

	public Component body => m_Flipped ? m_Header.Body : m_Header.OtherBody;

	public Collider collider => m_Flipped ? m_Pair.Collider : m_Pair.OtherCollider;

	public Transform transform => (rigidbody != null) ? rigidbody.transform : collider.transform;

	public GameObject gameObject => (body != null) ? body.gameObject : collider.gameObject;

	internal bool Flipped
	{
		get
		{
			return m_Flipped;
		}
		set
		{
			m_Flipped = value;
		}
	}

	public int contactCount => (int)m_Pair.m_NbPoints;

	public ContactPoint[] contacts
	{
		get
		{
			if (m_LegacyContacts == null)
			{
				m_LegacyContacts = new ContactPoint[m_Pair.m_NbPoints];
				m_Pair.ExtractContactsArray(m_LegacyContacts, m_Flipped);
			}
			return m_LegacyContacts;
		}
	}

	[Obsolete("Use Collision.relativeVelocity instead. (UnityUpgradable) -> relativeVelocity", false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public Vector3 impactForceSum => Vector3.zero;

	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Will always return zero.", true)]
	public Vector3 frictionForceSum => Vector3.zero;

	[Obsolete("Please use Collision.rigidbody, Collision.transform or Collision.collider instead", false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public Component other => (body != null) ? body : collider;

	public Collision()
	{
		m_Header = default(ContactPairHeader);
		m_Pair = default(ContactPair);
		m_Flipped = false;
		m_LegacyContacts = null;
	}

	internal Collision(in ContactPairHeader header, in ContactPair pair, bool flipped)
	{
		m_LegacyContacts = new ContactPoint[pair.m_NbPoints];
		pair.ExtractContactsArray(m_LegacyContacts, flipped);
		m_Header = header;
		m_Pair = pair;
		m_Flipped = flipped;
	}

	internal void Reuse(in ContactPairHeader header, in ContactPair pair)
	{
		m_Header = header;
		m_Pair = pair;
		m_LegacyContacts = null;
		m_Flipped = false;
	}

	public unsafe ContactPoint GetContact(int index)
	{
		if (index < 0 || index >= contactCount)
		{
			throw new ArgumentOutOfRangeException($"Cannot get contact at index {index}. There are {contactCount} contact(s).");
		}
		if (m_LegacyContacts != null)
		{
			return m_LegacyContacts[index];
		}
		float num = (m_Flipped ? (-1f) : 1f);
		ContactPairPoint* contactPoint_Internal = m_Pair.GetContactPoint_Internal(index);
		return new ContactPoint(contactPoint_Internal->m_Position, contactPoint_Internal->m_Normal * num, contactPoint_Internal->m_Impulse, contactPoint_Internal->m_Separation, m_Flipped ? m_Pair.OtherColliderInstanceID : m_Pair.ColliderInstanceID, m_Flipped ? m_Pair.ColliderInstanceID : m_Pair.OtherColliderInstanceID);
	}

	public int GetContacts(ContactPoint[] contacts)
	{
		if (contacts == null)
		{
			throw new NullReferenceException("Cannot get contacts as the provided array is NULL.");
		}
		if (m_LegacyContacts != null)
		{
			int num = Mathf.Min(m_LegacyContacts.Length, contacts.Length);
			Array.Copy(m_LegacyContacts, contacts, num);
			return num;
		}
		return m_Pair.ExtractContactsArray(contacts, m_Flipped);
	}

	public int GetContacts(List<ContactPoint> contacts)
	{
		if (contacts == null)
		{
			throw new NullReferenceException("Cannot get contacts as the provided list is NULL.");
		}
		contacts.Clear();
		if (m_LegacyContacts != null)
		{
			contacts.AddRange(m_LegacyContacts);
			return m_LegacyContacts.Length;
		}
		int nbPoints = (int)m_Pair.m_NbPoints;
		if (nbPoints == 0)
		{
			return 0;
		}
		if (contacts.Capacity < nbPoints)
		{
			contacts.Capacity = nbPoints;
		}
		return m_Pair.ExtractContacts(contacts, m_Flipped);
	}

	[Obsolete("Do not use Collision.GetEnumerator(), enumerate using non-allocating array returned by Collision.GetContacts() or enumerate using Collision.GetContact(index) instead.", false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual IEnumerator GetEnumerator()
	{
		return contacts.GetEnumerator();
	}
}
public enum CollisionFlags
{
	None = 0,
	Sides = 1,
	Above = 2,
	Below = 4,
	CollidedSides = 1,
	CollidedAbove = 2,
	CollidedBelow = 4
}
public enum QueryTriggerInteraction
{
	UseGlobal,
	Ignore,
	Collide
}
public enum CollisionDetectionMode
{
	Discrete,
	Continuous,
	ContinuousDynamic,
	ContinuousSpeculative
}
public enum ConfigurableJointMotion
{
	Locked,
	Limited,
	Free
}
public enum RotationDriveMode
{
	XYAndZ,
	Slerp
}
public enum ArticulationJointType
{
	FixedJoint,
	PrismaticJoint,
	RevoluteJoint,
	SphericalJoint
}
public enum ArticulationDofLock
{
	LockedMotion,
	LimitedMotion,
	FreeMotion
}
public enum ArticulationDriveType
{
	Force,
	Acceleration,
	Target,
	Velocity
}
[NativeHeader("Modules/Physics/ArticulationBody.h")]
public struct ArticulationDrive
{
	public float lowerLimit;

	public float upperLimit;

	public float stiffness;

	public float damping;

	public float forceLimit;

	public float target;

	public float targetVelocity;

	public ArticulationDriveType driveType;
}
[NativeHeader("Modules/Physics/ArticulationBody.h")]
public struct ArticulationReducedSpace
{
	private unsafe fixed float x[3];

	public int dofCount;

	public unsafe float this[int i]
	{
		get
		{
			if (i < 0 || i >= dofCount)
			{
				throw new IndexOutOfRangeException();
			}
			return x[i];
		}
		set
		{
			if (i < 0 || i >= dofCount)
			{
				throw new IndexOutOfRangeException();
			}
			x[i] = value;
		}
	}

	public unsafe ArticulationReducedSpace(float a)
	{
		x[0] = a;
		dofCount = 1;
	}

	public unsafe ArticulationReducedSpace(float a, float b)
	{
		x[0] = a;
		x[1] = b;
		dofCount = 2;
	}

	public unsafe ArticulationReducedSpace(float a, float b, float c)
	{
		x[0] = a;
		x[1] = b;
		x[2] = c;
		dofCount = 3;
	}
}
[NativeHeader("Modules/Physics/ArticulationBody.h")]
public struct ArticulationJacobian
{
	private int rowsCount;

	private int colsCount;

	private List<float> matrixData;

	public float this[int row, int col]
	{
		get
		{
			if (row < 0 || row >= rowsCount)
			{
				throw new IndexOutOfRangeException();
			}
			if (col < 0 || col >= colsCount)
			{
				throw new IndexOutOfRangeException();
			}
			return matrixData[row * colsCount + col];
		}
		set
		{
			if (row < 0 || row >= rowsCount)
			{
				throw new IndexOutOfRangeException();
			}
			if (col < 0 || col >= colsCount)
			{
				throw new IndexOutOfRangeException();
			}
			matrixData[row * colsCount + col] = value;
		}
	}

	public int rows
	{
		get
		{
			return rowsCount;
		}
		set
		{
			rowsCount = value;
		}
	}

	public int columns
	{
		get
		{
			return colsCount;
		}
		set
		{
			colsCount = value;
		}
	}

	public List<float> elements
	{
		get
		{
			return matrixData;
		}
		set
		{
			matrixData = value;
		}
	}

	public ArticulationJacobian(int rows, int cols)
	{
		rowsCount = rows;
		colsCount = cols;
		matrixData = new List<float>(rows * cols);
		for (int i = 0; i < rows * cols; i++)
		{
			matrixData.Add(0f);
		}
	}
}
public enum ArticulationDriveAxis
{
	X,
	Y,
	Z
}
[NativeHeader("Modules/Physics/ArticulationBody.h")]
[NativeClass("Unity::ArticulationBody")]
public class ArticulationBody : Behaviour
{
	public extern ArticulationJointType jointType
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public Vector3 anchorPosition
	{
		get
		{
			get_anchorPosition_Injected(out var ret);
			return ret;
		}
		set
		{
			set_anchorPosition_Injected(ref value);
		}
	}

	public Vector3 parentAnchorPosition
	{
		get
		{
			get_parentAnchorPosition_Injected(out var ret);
			return ret;
		}
		set
		{
			set_parentAnchorPosition_Injected(ref value);
		}
	}

	public Quaternion anchorRotation
	{
		get
		{
			get_anchorRotation_Injected(out var ret);
			return ret;
		}
		set
		{
			set_anchorRotation_Injected(ref value);
		}
	}

	public Quaternion parentAnchorRotation
	{
		get
		{
			get_parentAnchorRotation_Injected(out var ret);
			return ret;
		}
		set
		{
			set_parentAnchorRotation_Injected(ref value);
		}
	}

	public extern bool isRoot
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern bool matchAnchors
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern ArticulationDofLock linearLockX
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern ArticulationDofLock linearLockY
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern ArticulationDofLock linearLockZ
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern ArticulationDofLock swingYLock
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern ArticulationDofLock swingZLock
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern ArticulationDofLock twistLock
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public ArticulationDrive xDrive
	{
		get
		{
			get_xDrive_Injected(out var ret);
			return ret;
		}
		set
		{
			set_xDrive_Injected(ref value);
		}
	}

	public ArticulationDrive yDrive
	{
		get
		{
			get_yDrive_Injected(out var ret);
			return ret;
		}
		set
		{
			set_yDrive_Injected(ref value);
		}
	}

	public ArticulationDrive zDrive
	{
		get
		{
			get_zDrive_Injected(out var ret);
			return ret;
		}
		set
		{
			set_zDrive_Injected(ref value);
		}
	}

	public extern bool immovable
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool useGravity
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float linearDamping
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float angularDamping
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float jointFriction
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public LayerMask excludeLayers
	{
		get
		{
			get_excludeLayers_Injected(out var ret);
			return ret;
		}
		set
		{
			set_excludeLayers_Injected(ref value);
		}
	}

	public LayerMask includeLayers
	{
		get
		{
			get_includeLayers_Injected(out var ret);
			return ret;
		}
		set
		{
			set_includeLayers_Injected(ref value);
		}
	}

	public Vector3 velocity
	{
		get
		{
			get_velocity_Injected(out var ret);
			return ret;
		}
		set
		{
			set_velocity_Injected(ref value);
		}
	}

	public Vector3 angularVelocity
	{
		get
		{
			get_angularVelocity_Injected(out var ret);
			return ret;
		}
		set
		{
			set_angularVelocity_Injected(ref value);
		}
	}

	public extern float mass
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool automaticCenterOfMass
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public Vector3 centerOfMass
	{
		get
		{
			get_centerOfMass_Injected(out var ret);
			return ret;
		}
		set
		{
			set_centerOfMass_Injected(ref value);
		}
	}

	public Vector3 worldCenterOfMass
	{
		get
		{
			get_worldCenterOfMass_Injected(out var ret);
			return ret;
		}
	}

	public extern bool automaticInertiaTensor
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public Vector3 inertiaTensor
	{
		get
		{
			get_inertiaTensor_Injected(out var ret);
			return ret;
		}
		set
		{
			set_inertiaTensor_Injected(ref value);
		}
	}

	public Quaternion inertiaTensorRotation
	{
		get
		{
			get_inertiaTensorRotation_Injected(out var ret);
			return ret;
		}
		set
		{
			set_inertiaTensorRotation_Injected(ref value);
		}
	}

	public extern float sleepThreshold
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern int solverIterations
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern int solverVelocityIterations
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float maxAngularVelocity
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float maxLinearVelocity
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float maxJointVelocity
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float maxDepenetrationVelocity
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public ArticulationReducedSpace jointPosition
	{
		get
		{
			get_jointPosition_Injected(out var ret);
			return ret;
		}
		set
		{
			set_jointPosition_Injected(ref value);
		}
	}

	public ArticulationReducedSpace jointVelocity
	{
		get
		{
			get_jointVelocity_Injected(out var ret);
			return ret;
		}
		set
		{
			set_jointVelocity_Injected(ref value);
		}
	}

	public ArticulationReducedSpace jointAcceleration
	{
		get
		{
			get_jointAcceleration_Injected(out var ret);
			return ret;
		}
		[Obsolete("Setting joint accelerations is not supported in forward kinematics. To have inverse dynamics take acceleration into account, use GetJointForcesForAcceleration instead", true)]
		set
		{
			set_jointAcceleration_Injected(ref value);
		}
	}

	public ArticulationReducedSpace jointForce
	{
		get
		{
			get_jointForce_Injected(out var ret);
			return ret;
		}
		set
		{
			set_jointForce_Injected(ref value);
		}
	}

	public ArticulationReducedSpace driveForce
	{
		get
		{
			get_driveForce_Injected(out var ret);
			return ret;
		}
	}

	public extern int dofCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern int index
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetBodyIndex")]
		get;
	}

	public extern CollisionDetectionMode collisionDetectionMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[Obsolete("computeParentAnchor has been renamed to matchAnchors (UnityUpgradable) -> matchAnchors")]
	public bool computeParentAnchor
	{
		get
		{
			return matchAnchors;
		}
		set
		{
			matchAnchors = value;
		}
	}

	public Vector3 GetAccumulatedForce([UnityEngine.Internal.DefaultValue("Time.fixedDeltaTime")] float step)
	{
		GetAccumulatedForce_Injected(step, out var ret);
		return ret;
	}

	[ExcludeFromDocs]
	public Vector3 GetAccumulatedForce()
	{
		return GetAccumulatedForce(Time.fixedDeltaTime);
	}

	public Vector3 GetAccumulatedTorque([UnityEngine.Internal.DefaultValue("Time.fixedDeltaTime")] float step)
	{
		GetAccumulatedTorque_Injected(step, out var ret);
		return ret;
	}

	[ExcludeFromDocs]
	public Vector3 GetAccumulatedTorque()
	{
		return GetAccumulatedTorque(Time.fixedDeltaTime);
	}

	public void AddForce(Vector3 force, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode)
	{
		AddForce_Injected(ref force, mode);
	}

	[ExcludeFromDocs]
	public void AddForce(Vector3 force)
	{
		AddForce(force, ForceMode.Force);
	}

	public void AddRelativeForce(Vector3 force, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode)
	{
		AddRelativeForce_Injected(ref force, mode);
	}

	[ExcludeFromDocs]
	public void AddRelativeForce(Vector3 force)
	{
		AddRelativeForce(force, ForceMode.Force);
	}

	public void AddTorque(Vector3 torque, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode)
	{
		AddTorque_Injected(ref torque, mode);
	}

	[ExcludeFromDocs]
	public void AddTorque(Vector3 torque)
	{
		AddTorque(torque, ForceMode.Force);
	}

	public void AddRelativeTorque(Vector3 torque, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode)
	{
		AddRelativeTorque_Injected(ref torque, mode);
	}

	[ExcludeFromDocs]
	public void AddRelativeTorque(Vector3 torque)
	{
		AddRelativeTorque(torque, ForceMode.Force);
	}

	public void AddForceAtPosition(Vector3 force, Vector3 position, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode)
	{
		AddForceAtPosition_Injected(ref force, ref position, mode);
	}

	[ExcludeFromDocs]
	public void AddForceAtPosition(Vector3 force, Vector3 position)
	{
		AddForceAtPosition(force, position, ForceMode.Force);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void ResetCenterOfMass();

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void ResetInertiaTensor();

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void Sleep();

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern bool IsSleeping();

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void WakeUp();

	public void TeleportRoot(Vector3 position, Quaternion rotation)
	{
		TeleportRoot_Injected(ref position, ref rotation);
	}

	public Vector3 GetClosestPoint(Vector3 point)
	{
		GetClosestPoint_Injected(ref point, out var ret);
		return ret;
	}

	public Vector3 GetRelativePointVelocity(Vector3 relativePoint)
	{
		GetRelativePointVelocity_Injected(ref relativePoint, out var ret);
		return ret;
	}

	public Vector3 GetPointVelocity(Vector3 worldPoint)
	{
		GetPointVelocity_Injected(ref worldPoint, out var ret);
		return ret;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("GetDenseJacobian")]
	private extern int GetDenseJacobian_Internal(ref ArticulationJacobian jacobian);

	public int GetDenseJacobian(ref ArticulationJacobian jacobian)
	{
		if (jacobian.elements == null)
		{
			jacobian.elements = new List<float>();
		}
		return GetDenseJacobian_Internal(ref jacobian);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern int GetJointPositions(List<float> positions);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void SetJointPositions(List<float> positions);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern int GetJointVelocities(List<float> velocities);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void SetJointVelocities(List<float> velocities);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern int GetJointAccelerations(List<float> accelerations);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern int GetJointForces(List<float> forces);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void SetJointForces(List<float> forces);

	public ArticulationReducedSpace GetJointForcesForAcceleration(ArticulationReducedSpace acceleration)
	{
		GetJointForcesForAcceleration_Injected(ref acceleration, out var ret);
		return ret;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern int GetDriveForces(List<float> forces);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern int GetJointGravityForces(List<float> forces);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern int GetJointCoriolisCentrifugalForces(List<float> forces);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern int GetJointExternalForces(List<float> forces, float step);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern int GetDriveTargets(List<float> targets);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void SetDriveTargets(List<float> targets);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern int GetDriveTargetVelocities(List<float> targetVelocities);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void SetDriveTargetVelocities(List<float> targetVelocities);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern int GetDofStartIndices(List<int> dofStartIndices);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void SetDriveTarget(ArticulationDriveAxis axis, float value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void SetDriveTargetVelocity(ArticulationDriveAxis axis, float value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void SetDriveLimits(ArticulationDriveAxis axis, float lower, float upper);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void SetDriveStiffness(ArticulationDriveAxis axis, float value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void SetDriveDamping(ArticulationDriveAxis axis, float value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void SetDriveForceLimit(ArticulationDriveAxis axis, float value);

	public void SnapAnchorToClosestContact()
	{
		if ((bool)base.transform.parent)
		{
			ArticulationBody componentInParent = base.transform.parent.GetComponentInParent<ArticulationBody>();
			while ((bool)componentInParent && !componentInParent.enabled)
			{
				componentInParent = componentInParent.transform.parent.GetComponentInParent<ArticulationBody>();
			}
			if ((bool)componentInParent)
			{
				Vector3 vector = componentInParent.worldCenterOfMass;
				Vector3 closestPoint = GetClosestPoint(vector);
				anchorPosition = base.transform.InverseTransformPoint(closestPoint);
				anchorRotation = Quaternion.FromToRotation(Vector3.right, base.transform.InverseTransformDirection(vector - closestPoint).normalized);
			}
		}
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[Obsolete("Setting joint accelerations is not supported in forward kinematics. To have inverse dynamics take acceleration into account, use GetJointForcesForAcceleration instead", true)]
	public extern void SetJointAccelerations(List<float> accelerations);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_anchorPosition_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_anchorPosition_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_parentAnchorPosition_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_parentAnchorPosition_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_anchorRotation_Injected(out Quaternion ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_anchorRotation_Injected(ref Quaternion value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_parentAnchorRotation_Injected(out Quaternion ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_parentAnchorRotation_Injected(ref Quaternion value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_xDrive_Injected(out ArticulationDrive ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_xDrive_Injected(ref ArticulationDrive value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_yDrive_Injected(out ArticulationDrive ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_yDrive_Injected(ref ArticulationDrive value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_zDrive_Injected(out ArticulationDrive ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_zDrive_Injected(ref ArticulationDrive value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_excludeLayers_Injected(out LayerMask ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_excludeLayers_Injected(ref LayerMask value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_includeLayers_Injected(out LayerMask ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_includeLayers_Injected(ref LayerMask value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetAccumulatedForce_Injected([UnityEngine.Internal.DefaultValue("Time.fixedDeltaTime")] float step, out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetAccumulatedTorque_Injected([UnityEngine.Internal.DefaultValue("Time.fixedDeltaTime")] float step, out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void AddForce_Injected(ref Vector3 force, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void AddRelativeForce_Injected(ref Vector3 force, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void AddTorque_Injected(ref Vector3 torque, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void AddRelativeTorque_Injected(ref Vector3 torque, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void AddForceAtPosition_Injected(ref Vector3 force, ref Vector3 position, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_velocity_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_velocity_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_angularVelocity_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_angularVelocity_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_centerOfMass_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_centerOfMass_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_worldCenterOfMass_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_inertiaTensor_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_inertiaTensor_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_inertiaTensorRotation_Injected(out Quaternion ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_inertiaTensorRotation_Injected(ref Quaternion value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_jointPosition_Injected(out ArticulationReducedSpace ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_jointPosition_Injected(ref ArticulationReducedSpace value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_jointVelocity_Injected(out ArticulationReducedSpace ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_jointVelocity_Injected(ref ArticulationReducedSpace value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_jointAcceleration_Injected(out ArticulationReducedSpace ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_jointAcceleration_Injected(ref ArticulationReducedSpace value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_jointForce_Injected(out ArticulationReducedSpace ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_jointForce_Injected(ref ArticulationReducedSpace value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_driveForce_Injected(out ArticulationReducedSpace ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void TeleportRoot_Injected(ref Vector3 position, ref Quaternion rotation);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetClosestPoint_Injected(ref Vector3 point, out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetRelativePointVelocity_Injected(ref Vector3 relativePoint, out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetPointVelocity_Injected(ref Vector3 worldPoint, out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetJointForcesForAcceleration_Injected(ref ArticulationReducedSpace acceleration, out ArticulationReducedSpace ret);
}
[NativeHeader("Modules/Physics/PhysicsManager.h")]
[StaticAccessor("GetPhysicsManager()", StaticAccessorType.Dot)]
public class Physics
{
	public delegate void ContactEventDelegate(PhysicsScene scene, NativeArray<ContactPairHeader>.ReadOnly headerArray);

	internal const float k_MaxFloatMinusEpsilon = 3.4028233E+38f;

	public const int IgnoreRaycastLayer = 4;

	public const int DefaultRaycastLayers = -5;

	public const int AllLayers = -1;

	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Please use Physics.IgnoreRaycastLayer instead. (UnityUpgradable) -> IgnoreRaycastLayer", true)]
	public const int kIgnoreRaycastLayer = 4;

	[Obsolete("Please use Physics.DefaultRaycastLayers instead. (UnityUpgradable) -> DefaultRaycastLayers", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public const int kDefaultRaycastLayers = -5;

	[Obsolete("Please use Physics.AllLayers instead. (UnityUpgradable) -> AllLayers", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public const int kAllLayers = -1;

	private static readonly Collision s_ReusableCollision = new Collision();

	public static Vector3 gravity
	{
		[ThreadSafe]
		get
		{
			get_gravity_Injected(out var ret);
			return ret;
		}
		set
		{
			set_gravity_Injected(ref value);
		}
	}

	public static extern float defaultContactOffset
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public static extern float sleepThreshold
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public static extern bool queriesHitTriggers
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public static extern bool queriesHitBackfaces
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public static extern float bounceThreshold
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public static extern float defaultMaxDepenetrationVelocity
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public static extern int defaultSolverIterations
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public static extern int defaultSolverVelocityIterations
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public static extern SimulationMode simulationMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public static extern float defaultMaxAngularSpeed
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public static extern bool improvedPatchFriction
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public static extern bool invokeCollisionCallbacks
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("DefaultPhysicsSceneHandle", true, TargetType.Function, true)]
	public static PhysicsScene defaultPhysicsScene
	{
		get
		{
			get_defaultPhysicsScene_Injected(out var ret);
			return ret;
		}
	}

	public static extern bool autoSyncTransforms
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public static extern bool reuseCollisionCallbacks
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysicsManager()")]
	public static extern float interCollisionDistance
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetClothInterCollisionDistance")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("SetClothInterCollisionDistance")]
		set;
	}

	[StaticAccessor("GetPhysicsManager()")]
	public static extern float interCollisionStiffness
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetClothInterCollisionStiffness")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("SetClothInterCollisionStiffness")]
		set;
	}

	[StaticAccessor("GetPhysicsManager()")]
	public static extern bool interCollisionSettingsToggle
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetClothInterCollisionSettingsToggle")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("SetClothInterCollisionSettingsToggle")]
		set;
	}

	public static Vector3 clothGravity
	{
		[ThreadSafe]
		get
		{
			get_clothGravity_Injected(out var ret);
			return ret;
		}
		set
		{
			set_clothGravity_Injected(ref value);
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Use Physics.defaultContactOffset or Collider.contactOffset instead.", true)]
	public static float minPenetrationForPenalty
	{
		get
		{
			return 0f;
		}
		set
		{
		}
	}

	[Obsolete("Please use bounceThreshold instead. (UnityUpgradable) -> bounceThreshold")]
	public static float bounceTreshold
	{
		get
		{
			return bounceThreshold;
		}
		set
		{
			bounceThreshold = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("The sleepVelocity is no longer supported. Use sleepThreshold. Note that sleepThreshold is energy but not velocity.", true)]
	public static float sleepVelocity
	{
		get
		{
			return 0f;
		}
		set
		{
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("The sleepAngularVelocity is no longer supported. Use sleepThreshold. Note that sleepThreshold is energy but not velocity.", true)]
	public static float sleepAngularVelocity
	{
		get
		{
			return 0f;
		}
		set
		{
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Use Rigidbody.maxAngularVelocity instead.", true)]
	public static float maxAngularVelocity
	{
		get
		{
			return 0f;
		}
		set
		{
		}
	}

	[Obsolete("Please use Physics.defaultSolverIterations instead. (UnityUpgradable) -> defaultSolverIterations")]
	public static int solverIterationCount
	{
		get
		{
			return defaultSolverIterations;
		}
		set
		{
			defaultSolverIterations = value;
		}
	}

	[Obsolete("Please use Physics.defaultSolverVelocityIterations instead. (UnityUpgradable) -> defaultSolverVelocityIterations")]
	public static int solverVelocityIterationCount
	{
		get
		{
			return defaultSolverVelocityIterations;
		}
		set
		{
			defaultSolverVelocityIterations = value;
		}
	}

	[Obsolete("penetrationPenaltyForce has no effect.", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static float penetrationPenaltyForce
	{
		get
		{
			return 0f;
		}
		set
		{
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Physics.autoSimulation has been replaced by Physics.simulationMode", false)]
	public static bool autoSimulation
	{
		get
		{
			return simulationMode != SimulationMode.Script;
		}
		set
		{
			simulationMode = ((!value) ? SimulationMode.Script : SimulationMode.FixedUpdate);
		}
	}

	public static event Action<PhysicsScene, NativeArray<ModifiableContactPair>> ContactModifyEvent;

	public static event Action<PhysicsScene, NativeArray<ModifiableContactPair>> ContactModifyEventCCD;

	public static event ContactEventDelegate ContactEvent;

	[RequiredByNativeCode]
	private unsafe static void OnSceneContactModify(PhysicsScene scene, IntPtr buffer, int count, bool isCCD)
	{
		NativeArray<ModifiableContactPair> arg = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<ModifiableContactPair>(buffer.ToPointer(), count, Allocator.None);
		if (!isCCD)
		{
			Physics.ContactModifyEvent?.Invoke(scene, arg);
		}
		else
		{
			Physics.ContactModifyEventCCD?.Invoke(scene, arg);
		}
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern void IgnoreCollision([NotNull("NullExceptionObject")] Collider collider1, [NotNull("NullExceptionObject")] Collider collider2, [UnityEngine.Internal.DefaultValue("true")] bool ignore);

	[ExcludeFromDocs]
	public static void IgnoreCollision(Collider collider1, Collider collider2)
	{
		IgnoreCollision(collider1, collider2, ignore: true);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeName("IgnoreCollision")]
	public static extern void IgnoreLayerCollision(int layer1, int layer2, [UnityEngine.Internal.DefaultValue("true")] bool ignore);

	[ExcludeFromDocs]
	public static void IgnoreLayerCollision(int layer1, int layer2)
	{
		IgnoreLayerCollision(layer1, layer2, ignore: true);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern bool GetIgnoreLayerCollision(int layer1, int layer2);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern bool GetIgnoreCollision([NotNull("NullExceptionObject")] Collider collider1, [NotNull("NullExceptionObject")] Collider collider2);

	public static bool Raycast(Vector3 origin, Vector3 direction, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		return defaultPhysicsScene.Raycast(origin, direction, maxDistance, layerMask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance, int layerMask)
	{
		return defaultPhysicsScene.Raycast(origin, direction, maxDistance, layerMask);
	}

	[ExcludeFromDocs]
	public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance)
	{
		return defaultPhysicsScene.Raycast(origin, direction, maxDistance);
	}

	[ExcludeFromDocs]
	public static bool Raycast(Vector3 origin, Vector3 direction)
	{
		return defaultPhysicsScene.Raycast(origin, direction);
	}

	public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
	{
		return defaultPhysicsScene.Raycast(origin, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
	}

	[RequiredByNativeCode]
	[ExcludeFromDocs]
	public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask)
	{
		return defaultPhysicsScene.Raycast(origin, direction, out hitInfo, maxDistance, layerMask);
	}

	[ExcludeFromDocs]
	public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance)
	{
		return defaultPhysicsScene.Raycast(origin, direction, out hitInfo, maxDistance);
	}

	[ExcludeFromDocs]
	public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo)
	{
		return defaultPhysicsScene.Raycast(origin, direction, out hitInfo);
	}

	public static bool Raycast(Ray ray, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		return defaultPhysicsScene.Raycast(ray.origin, ray.direction, maxDistance, layerMask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public static bool Raycast(Ray ray, float maxDistance, int layerMask)
	{
		return defaultPhysicsScene.Raycast(ray.origin, ray.direction, maxDistance, layerMask);
	}

	[ExcludeFromDocs]
	public static bool Raycast(Ray ray, float maxDistance)
	{
		return defaultPhysicsScene.Raycast(ray.origin, ray.direction, maxDistance);
	}

	[ExcludeFromDocs]
	public static bool Raycast(Ray ray)
	{
		return defaultPhysicsScene.Raycast(ray.origin, ray.direction);
	}

	public static bool Raycast(Ray ray, out RaycastHit hitInfo, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		return defaultPhysicsScene.Raycast(ray.origin, ray.direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance, int layerMask)
	{
		return Raycast(ray.origin, ray.direction, out hitInfo, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance)
	{
		return defaultPhysicsScene.Raycast(ray.origin, ray.direction, out hitInfo, maxDistance);
	}

	[ExcludeFromDocs]
	public static bool Raycast(Ray ray, out RaycastHit hitInfo)
	{
		return defaultPhysicsScene.Raycast(ray.origin, ray.direction, out hitInfo);
	}

	public static bool Linecast(Vector3 start, Vector3 end, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		Vector3 direction = end - start;
		return defaultPhysicsScene.Raycast(start, direction, direction.magnitude, layerMask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public static bool Linecast(Vector3 start, Vector3 end, int layerMask)
	{
		return Linecast(start, end, layerMask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static bool Linecast(Vector3 start, Vector3 end)
	{
		return Linecast(start, end, -5, QueryTriggerInteraction.UseGlobal);
	}

	public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		Vector3 direction = end - start;
		return defaultPhysicsScene.Raycast(start, direction, out hitInfo, direction.magnitude, layerMask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo, int layerMask)
	{
		return Linecast(start, end, out hitInfo, layerMask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo)
	{
		return Linecast(start, end, out hitInfo, -5, QueryTriggerInteraction.UseGlobal);
	}

	public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		RaycastHit hitInfo;
		return defaultPhysicsScene.CapsuleCast(point1, point2, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance, int layerMask)
	{
		return CapsuleCast(point1, point2, radius, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance)
	{
		return CapsuleCast(point1, point2, radius, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction)
	{
		return CapsuleCast(point1, point2, radius, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
	}

	public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		return defaultPhysicsScene.CapsuleCast(point1, point2, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask)
	{
		return CapsuleCast(point1, point2, radius, direction, out hitInfo, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance)
	{
		return CapsuleCast(point1, point2, radius, direction, out hitInfo, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo)
	{
		return CapsuleCast(point1, point2, radius, direction, out hitInfo, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
	}

	public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		return defaultPhysicsScene.SphereCast(origin, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask)
	{
		return SphereCast(origin, radius, direction, out hitInfo, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance)
	{
		return SphereCast(origin, radius, direction, out hitInfo, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo)
	{
		return SphereCast(origin, radius, direction, out hitInfo, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
	}

	public static bool SphereCast(Ray ray, float radius, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		RaycastHit hitInfo;
		return SphereCast(ray.origin, radius, ray.direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public static bool SphereCast(Ray ray, float radius, float maxDistance, int layerMask)
	{
		return SphereCast(ray, radius, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static bool SphereCast(Ray ray, float radius, float maxDistance)
	{
		return SphereCast(ray, radius, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static bool SphereCast(Ray ray, float radius)
	{
		return SphereCast(ray, radius, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
	}

	public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		return SphereCast(ray.origin, radius, ray.direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo, float maxDistance, int layerMask)
	{
		return SphereCast(ray, radius, out hitInfo, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo, float maxDistance)
	{
		return SphereCast(ray, radius, out hitInfo, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo)
	{
		return SphereCast(ray, radius, out hitInfo, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
	}

	public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, [UnityEngine.Internal.DefaultValue("Quaternion.identity")] Quaternion orientation, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		RaycastHit hitInfo;
		return defaultPhysicsScene.BoxCast(center, halfExtents, direction, out hitInfo, orientation, maxDistance, layerMask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance, int layerMask)
	{
		return BoxCast(center, halfExtents, direction, orientation, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance)
	{
		return BoxCast(center, halfExtents, direction, orientation, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation)
	{
		return BoxCast(center, halfExtents, direction, orientation, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction)
	{
		return BoxCast(center, halfExtents, direction, Quaternion.identity, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
	}

	public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, [UnityEngine.Internal.DefaultValue("Quaternion.identity")] Quaternion orientation, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		return defaultPhysicsScene.BoxCast(center, halfExtents, direction, out hitInfo, orientation, maxDistance, layerMask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, Quaternion orientation, float maxDistance, int layerMask)
	{
		return BoxCast(center, halfExtents, direction, out hitInfo, orientation, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, Quaternion orientation, float maxDistance)
	{
		return BoxCast(center, halfExtents, direction, out hitInfo, orientation, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, Quaternion orientation)
	{
		return BoxCast(center, halfExtents, direction, out hitInfo, orientation, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo)
	{
		return BoxCast(center, halfExtents, direction, out hitInfo, Quaternion.identity, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
	}

	[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()", StaticAccessorType.Dot)]
	[NativeName("RaycastAll")]
	private static RaycastHit[] Internal_RaycastAll(PhysicsScene physicsScene, Ray ray, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
	{
		return Internal_RaycastAll_Injected(ref physicsScene, ref ray, maxDistance, mask, queryTriggerInteraction);
	}

	public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		float magnitude = direction.magnitude;
		if (magnitude > float.Epsilon)
		{
			Vector3 direction2 = direction / magnitude;
			return Internal_RaycastAll(ray: new Ray(origin, direction2), physicsScene: defaultPhysicsScene, maxDistance: maxDistance, mask: layerMask, queryTriggerInteraction: queryTriggerInteraction);
		}
		return new RaycastHit[0];
	}

	[ExcludeFromDocs]
	public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, float maxDistance, int layerMask)
	{
		return RaycastAll(origin, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, float maxDistance)
	{
		return RaycastAll(origin, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction)
	{
		return RaycastAll(origin, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
	}

	public static RaycastHit[] RaycastAll(Ray ray, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		return RaycastAll(ray.origin, ray.direction, maxDistance, layerMask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	[RequiredByNativeCode]
	public static RaycastHit[] RaycastAll(Ray ray, float maxDistance, int layerMask)
	{
		return RaycastAll(ray.origin, ray.direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static RaycastHit[] RaycastAll(Ray ray, float maxDistance)
	{
		return RaycastAll(ray.origin, ray.direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static RaycastHit[] RaycastAll(Ray ray)
	{
		return RaycastAll(ray.origin, ray.direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
	}

	public static int RaycastNonAlloc(Ray ray, RaycastHit[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		return defaultPhysicsScene.Raycast(ray.origin, ray.direction, results, maxDistance, layerMask, queryTriggerInteraction);
	}

	[RequiredByNativeCode]
	[ExcludeFromDocs]
	public static int RaycastNonAlloc(Ray ray, RaycastHit[] results, float maxDistance, int layerMask)
	{
		return defaultPhysicsScene.Raycast(ray.origin, ray.direction, results, maxDistance, layerMask);
	}

	[ExcludeFromDocs]
	public static int RaycastNonAlloc(Ray ray, RaycastHit[] results, float maxDistance)
	{
		return defaultPhysicsScene.Raycast(ray.origin, ray.direction, results, maxDistance);
	}

	[ExcludeFromDocs]
	public static int RaycastNonAlloc(Ray ray, RaycastHit[] results)
	{
		return defaultPhysicsScene.Raycast(ray.origin, ray.direction, results);
	}

	public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		return defaultPhysicsScene.Raycast(origin, direction, results, maxDistance, layerMask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results, float maxDistance, int layerMask)
	{
		return defaultPhysicsScene.Raycast(origin, direction, results, maxDistance, layerMask);
	}

	[ExcludeFromDocs]
	public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results, float maxDistance)
	{
		return defaultPhysicsScene.Raycast(origin, direction, results, maxDistance);
	}

	[ExcludeFromDocs]
	public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results)
	{
		return defaultPhysicsScene.Raycast(origin, direction, results);
	}

	[NativeName("CapsuleCastAll")]
	[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()", StaticAccessorType.Dot)]
	private static RaycastHit[] Query_CapsuleCastAll(PhysicsScene physicsScene, Vector3 p0, Vector3 p1, float radius, Vector3 direction, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
	{
		return Query_CapsuleCastAll_Injected(ref physicsScene, ref p0, ref p1, radius, ref direction, maxDistance, mask, queryTriggerInteraction);
	}

	public static RaycastHit[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius, Vector3 direction, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		float magnitude = direction.magnitude;
		if (magnitude > float.Epsilon)
		{
			Vector3 direction2 = direction / magnitude;
			return Query_CapsuleCastAll(defaultPhysicsScene, point1, point2, radius, direction2, maxDistance, layerMask, queryTriggerInteraction);
		}
		return new RaycastHit[0];
	}

	[ExcludeFromDocs]
	public static RaycastHit[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance, int layerMask)
	{
		return CapsuleCastAll(point1, point2, radius, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static RaycastHit[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance)
	{
		return CapsuleCastAll(point1, point2, radius, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static RaycastHit[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius, Vector3 direction)
	{
		return CapsuleCastAll(point1, point2, radius, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
	}

	[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()", StaticAccessorType.Dot)]
	[NativeName("SphereCastAll")]
	private static RaycastHit[] Query_SphereCastAll(PhysicsScene physicsScene, Vector3 origin, float radius, Vector3 direction, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
	{
		return Query_SphereCastAll_Injected(ref physicsScene, ref origin, radius, ref direction, maxDistance, mask, queryTriggerInteraction);
	}

	public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		float magnitude = direction.magnitude;
		if (magnitude > float.Epsilon)
		{
			Vector3 direction2 = direction / magnitude;
			return Query_SphereCastAll(defaultPhysicsScene, origin, radius, direction2, maxDistance, layerMask, queryTriggerInteraction);
		}
		return new RaycastHit[0];
	}

	[ExcludeFromDocs]
	public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction, float maxDistance, int layerMask)
	{
		return SphereCastAll(origin, radius, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction, float maxDistance)
	{
		return SphereCastAll(origin, radius, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction)
	{
		return SphereCastAll(origin, radius, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
	}

	public static RaycastHit[] SphereCastAll(Ray ray, float radius, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		return SphereCastAll(ray.origin, radius, ray.direction, maxDistance, layerMask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public static RaycastHit[] SphereCastAll(Ray ray, float radius, float maxDistance, int layerMask)
	{
		return SphereCastAll(ray, radius, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static RaycastHit[] SphereCastAll(Ray ray, float radius, float maxDistance)
	{
		return SphereCastAll(ray, radius, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static RaycastHit[] SphereCastAll(Ray ray, float radius)
	{
		return SphereCastAll(ray, radius, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
	}

	[NativeName("OverlapCapsule")]
	[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()", StaticAccessorType.Dot)]
	private static Collider[] OverlapCapsule_Internal(PhysicsScene physicsScene, Vector3 point0, Vector3 point1, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
	{
		return OverlapCapsule_Internal_Injected(ref physicsScene, ref point0, ref point1, radius, layerMask, queryTriggerInteraction);
	}

	public static Collider[] OverlapCapsule(Vector3 point0, Vector3 point1, float radius, [UnityEngine.Internal.DefaultValue("AllLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		return OverlapCapsule_Internal(defaultPhysicsScene, point0, point1, radius, layerMask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public static Collider[] OverlapCapsule(Vector3 point0, Vector3 point1, float radius, int layerMask)
	{
		return OverlapCapsule(point0, point1, radius, layerMask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static Collider[] OverlapCapsule(Vector3 point0, Vector3 point1, float radius)
	{
		return OverlapCapsule(point0, point1, radius, -1, QueryTriggerInteraction.UseGlobal);
	}

	[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()", StaticAccessorType.Dot)]
	[NativeName("OverlapSphere")]
	private static Collider[] OverlapSphere_Internal(PhysicsScene physicsScene, Vector3 position, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
	{
		return OverlapSphere_Internal_Injected(ref physicsScene, ref position, radius, layerMask, queryTriggerInteraction);
	}

	public static Collider[] OverlapSphere(Vector3 position, float radius, [UnityEngine.Internal.DefaultValue("AllLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		return OverlapSphere_Internal(defaultPhysicsScene, position, radius, layerMask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public static Collider[] OverlapSphere(Vector3 position, float radius, int layerMask)
	{
		return OverlapSphere(position, radius, layerMask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static Collider[] OverlapSphere(Vector3 position, float radius)
	{
		return OverlapSphere(position, radius, -1, QueryTriggerInteraction.UseGlobal);
	}

	[NativeName("Simulate")]
	internal static void Simulate_Internal(PhysicsScene physicsScene, float step)
	{
		Simulate_Internal_Injected(ref physicsScene, step);
	}

	public static void Simulate(float step)
	{
		if (simulationMode != SimulationMode.Script)
		{
			Debug.LogWarning("Physics.Simulate(...) was called but simulation mode is not set to Script. You should set simulation mode to Script first before calling this function therefore the simulation was not run.");
		}
		else
		{
			Simulate_Internal(defaultPhysicsScene, step);
		}
	}

	[NativeName("InterpolateBodies")]
	internal static void InterpolateBodies_Internal(PhysicsScene physicsScene)
	{
		InterpolateBodies_Internal_Injected(ref physicsScene);
	}

	[NativeName("ResetInterpolatedTransformPosition")]
	internal static void ResetInterpolationPoses_Internal(PhysicsScene physicsScene)
	{
		ResetInterpolationPoses_Internal_Injected(ref physicsScene);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern void SyncTransforms();

	[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
	[NativeName("ComputePenetration")]
	private static bool Query_ComputePenetration([NotNull("ArgumentNullException")] Collider colliderA, Vector3 positionA, Quaternion rotationA, [NotNull("ArgumentNullException")] Collider colliderB, Vector3 positionB, Quaternion rotationB, ref Vector3 direction, ref float distance)
	{
		return Query_ComputePenetration_Injected(colliderA, ref positionA, ref rotationA, colliderB, ref positionB, ref rotationB, ref direction, ref distance);
	}

	public static bool ComputePenetration(Collider colliderA, Vector3 positionA, Quaternion rotationA, Collider colliderB, Vector3 positionB, Quaternion rotationB, out Vector3 direction, out float distance)
	{
		direction = Vector3.zero;
		distance = 0f;
		return Query_ComputePenetration(colliderA, positionA, rotationA, colliderB, positionB, rotationB, ref direction, ref distance);
	}

	[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
	[NativeName("ClosestPoint")]
	private static Vector3 Query_ClosestPoint([NotNull("ArgumentNullException")] Collider collider, Vector3 position, Quaternion rotation, Vector3 point)
	{
		Query_ClosestPoint_Injected(collider, ref position, ref rotation, ref point, out var ret);
		return ret;
	}

	public static Vector3 ClosestPoint(Vector3 point, Collider collider, Vector3 position, Quaternion rotation)
	{
		return Query_ClosestPoint(collider, position, rotation, point);
	}

	public static int OverlapSphereNonAlloc(Vector3 position, float radius, Collider[] results, [UnityEngine.Internal.DefaultValue("AllLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		return defaultPhysicsScene.OverlapSphere(position, radius, results, layerMask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public static int OverlapSphereNonAlloc(Vector3 position, float radius, Collider[] results, int layerMask)
	{
		return OverlapSphereNonAlloc(position, radius, results, layerMask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static int OverlapSphereNonAlloc(Vector3 position, float radius, Collider[] results)
	{
		return OverlapSphereNonAlloc(position, radius, results, -1, QueryTriggerInteraction.UseGlobal);
	}

	[NativeName("SphereTest")]
	[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
	private static bool CheckSphere_Internal(PhysicsScene physicsScene, Vector3 position, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
	{
		return CheckSphere_Internal_Injected(ref physicsScene, ref position, radius, layerMask, queryTriggerInteraction);
	}

	public static bool CheckSphere(Vector3 position, float radius, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		return CheckSphere_Internal(defaultPhysicsScene, position, radius, layerMask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public static bool CheckSphere(Vector3 position, float radius, int layerMask)
	{
		return CheckSphere(position, radius, layerMask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static bool CheckSphere(Vector3 position, float radius)
	{
		return CheckSphere(position, radius, -5, QueryTriggerInteraction.UseGlobal);
	}

	public static int CapsuleCastNonAlloc(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		return defaultPhysicsScene.CapsuleCast(point1, point2, radius, direction, results, maxDistance, layerMask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public static int CapsuleCastNonAlloc(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results, float maxDistance, int layerMask)
	{
		return CapsuleCastNonAlloc(point1, point2, radius, direction, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static int CapsuleCastNonAlloc(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results, float maxDistance)
	{
		return CapsuleCastNonAlloc(point1, point2, radius, direction, results, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static int CapsuleCastNonAlloc(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results)
	{
		return CapsuleCastNonAlloc(point1, point2, radius, direction, results, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
	}

	public static int SphereCastNonAlloc(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		return defaultPhysicsScene.SphereCast(origin, radius, direction, results, maxDistance, layerMask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public static int SphereCastNonAlloc(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results, float maxDistance, int layerMask)
	{
		return SphereCastNonAlloc(origin, radius, direction, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static int SphereCastNonAlloc(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results, float maxDistance)
	{
		return SphereCastNonAlloc(origin, radius, direction, results, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static int SphereCastNonAlloc(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results)
	{
		return SphereCastNonAlloc(origin, radius, direction, results, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
	}

	public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		return SphereCastNonAlloc(ray.origin, radius, ray.direction, results, maxDistance, layerMask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results, float maxDistance, int layerMask)
	{
		return SphereCastNonAlloc(ray, radius, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results, float maxDistance)
	{
		return SphereCastNonAlloc(ray, radius, results, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results)
	{
		return SphereCastNonAlloc(ray, radius, results, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
	}

	[NativeName("CapsuleTest")]
	[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
	private static bool CheckCapsule_Internal(PhysicsScene physicsScene, Vector3 start, Vector3 end, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
	{
		return CheckCapsule_Internal_Injected(ref physicsScene, ref start, ref end, radius, layerMask, queryTriggerInteraction);
	}

	public static bool CheckCapsule(Vector3 start, Vector3 end, float radius, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		return CheckCapsule_Internal(defaultPhysicsScene, start, end, radius, layerMask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public static bool CheckCapsule(Vector3 start, Vector3 end, float radius, int layerMask)
	{
		return CheckCapsule(start, end, radius, layerMask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static bool CheckCapsule(Vector3 start, Vector3 end, float radius)
	{
		return CheckCapsule(start, end, radius, -5, QueryTriggerInteraction.UseGlobal);
	}

	[NativeName("BoxTest")]
	[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
	private static bool CheckBox_Internal(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Quaternion orientation, int layermask, QueryTriggerInteraction queryTriggerInteraction)
	{
		return CheckBox_Internal_Injected(ref physicsScene, ref center, ref halfExtents, ref orientation, layermask, queryTriggerInteraction);
	}

	public static bool CheckBox(Vector3 center, Vector3 halfExtents, [UnityEngine.Internal.DefaultValue("Quaternion.identity")] Quaternion orientation, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layermask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		return CheckBox_Internal(defaultPhysicsScene, center, halfExtents, orientation, layermask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public static bool CheckBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, int layerMask)
	{
		return CheckBox(center, halfExtents, orientation, layerMask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static bool CheckBox(Vector3 center, Vector3 halfExtents, Quaternion orientation)
	{
		return CheckBox(center, halfExtents, orientation, -5, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static bool CheckBox(Vector3 center, Vector3 halfExtents)
	{
		return CheckBox(center, halfExtents, Quaternion.identity, -5, QueryTriggerInteraction.UseGlobal);
	}

	[NativeName("OverlapBox")]
	[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
	private static Collider[] OverlapBox_Internal(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Quaternion orientation, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
	{
		return OverlapBox_Internal_Injected(ref physicsScene, ref center, ref halfExtents, ref orientation, layerMask, queryTriggerInteraction);
	}

	public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents, [UnityEngine.Internal.DefaultValue("Quaternion.identity")] Quaternion orientation, [UnityEngine.Internal.DefaultValue("AllLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		return OverlapBox_Internal(defaultPhysicsScene, center, halfExtents, orientation, layerMask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, int layerMask)
	{
		return OverlapBox(center, halfExtents, orientation, layerMask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents, Quaternion orientation)
	{
		return OverlapBox(center, halfExtents, orientation, -1, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents)
	{
		return OverlapBox(center, halfExtents, Quaternion.identity, -1, QueryTriggerInteraction.UseGlobal);
	}

	public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results, [UnityEngine.Internal.DefaultValue("Quaternion.identity")] Quaternion orientation, [UnityEngine.Internal.DefaultValue("AllLayers")] int mask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		return defaultPhysicsScene.OverlapBox(center, halfExtents, results, orientation, mask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results, Quaternion orientation, int mask)
	{
		return OverlapBoxNonAlloc(center, halfExtents, results, orientation, mask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results, Quaternion orientation)
	{
		return OverlapBoxNonAlloc(center, halfExtents, results, orientation, -1, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results)
	{
		return OverlapBoxNonAlloc(center, halfExtents, results, Quaternion.identity, -1, QueryTriggerInteraction.UseGlobal);
	}

	public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, [UnityEngine.Internal.DefaultValue("Quaternion.identity")] Quaternion orientation, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		return defaultPhysicsScene.BoxCast(center, halfExtents, direction, results, orientation, maxDistance, layerMask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, Quaternion orientation)
	{
		return BoxCastNonAlloc(center, halfExtents, direction, results, orientation, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, Quaternion orientation, float maxDistance)
	{
		return BoxCastNonAlloc(center, halfExtents, direction, results, orientation, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, Quaternion orientation, float maxDistance, int layerMask)
	{
		return BoxCastNonAlloc(center, halfExtents, direction, results, orientation, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results)
	{
		return BoxCastNonAlloc(center, halfExtents, direction, results, Quaternion.identity, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
	}

	[NativeName("BoxCastAll")]
	[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
	private static RaycastHit[] Internal_BoxCastAll(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
	{
		return Internal_BoxCastAll_Injected(ref physicsScene, ref center, ref halfExtents, ref direction, ref orientation, maxDistance, layerMask, queryTriggerInteraction);
	}

	public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction, [UnityEngine.Internal.DefaultValue("Quaternion.identity")] Quaternion orientation, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		float magnitude = direction.magnitude;
		if (magnitude > float.Epsilon)
		{
			Vector3 direction2 = direction / magnitude;
			return Internal_BoxCastAll(defaultPhysicsScene, center, halfExtents, direction2, orientation, maxDistance, layerMask, queryTriggerInteraction);
		}
		return new RaycastHit[0];
	}

	[ExcludeFromDocs]
	public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance, int layerMask)
	{
		return BoxCastAll(center, halfExtents, direction, orientation, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance)
	{
		return BoxCastAll(center, halfExtents, direction, orientation, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation)
	{
		return BoxCastAll(center, halfExtents, direction, orientation, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction)
	{
		return BoxCastAll(center, halfExtents, direction, Quaternion.identity, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
	}

	public static int OverlapCapsuleNonAlloc(Vector3 point0, Vector3 point1, float radius, Collider[] results, [UnityEngine.Internal.DefaultValue("AllLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		return defaultPhysicsScene.OverlapCapsule(point0, point1, radius, results, layerMask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public static int OverlapCapsuleNonAlloc(Vector3 point0, Vector3 point1, float radius, Collider[] results, int layerMask)
	{
		return OverlapCapsuleNonAlloc(point0, point1, radius, results, layerMask, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public static int OverlapCapsuleNonAlloc(Vector3 point0, Vector3 point1, float radius, Collider[] results)
	{
		return OverlapCapsuleNonAlloc(point0, point1, radius, results, -1, QueryTriggerInteraction.UseGlobal);
	}

	[NativeName("RebuildBroadphaseRegions")]
	[StaticAccessor("GetPhysicsManager()")]
	private static void Internal_RebuildBroadphaseRegions(Bounds bounds, int subdivisions)
	{
		Internal_RebuildBroadphaseRegions_Injected(ref bounds, subdivisions);
	}

	public static void RebuildBroadphaseRegions(Bounds worldBounds, int subdivisions)
	{
		if (subdivisions < 1 || subdivisions > 16)
		{
			throw new ArgumentException("Physics.RebuildBroadphaseRegions requires the subdivisions to be greater than zero and less than 17.");
		}
		if (worldBounds.extents.x <= 0f || worldBounds.extents.y <= 0f || worldBounds.extents.z <= 0f)
		{
			throw new ArgumentException("Physics.RebuildBroadphaseRegions requires the world bounds to be non-empty, and have positive extents.");
		}
		Internal_RebuildBroadphaseRegions(worldBounds, subdivisions);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[StaticAccessor("GetPhysicsManager()")]
	[ThreadSafe]
	public static extern void BakeMesh(int meshID, bool convex, MeshColliderCookingOptions cookingOptions);

	public static void BakeMesh(int meshID, bool convex)
	{
		BakeMesh(meshID, convex, MeshColliderCookingOptions.CookForFasterSimulation | MeshColliderCookingOptions.EnableMeshCleaning | MeshColliderCookingOptions.WeldColocatedVertices | MeshColliderCookingOptions.UseFastMidphase);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[StaticAccessor("PhysicsManager", StaticAccessorType.DoubleColon)]
	internal static extern Collider ResolveShapeToCollider(IntPtr shapePtr);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[StaticAccessor("PhysicsManager", StaticAccessorType.DoubleColon)]
	internal static extern Component ResolveActorToComponent(IntPtr actorPtr);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[StaticAccessor("PhysicsManager", StaticAccessorType.DoubleColon)]
	[ThreadSafe]
	internal static extern int ResolveShapeToInstanceID(IntPtr shapePtr);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[StaticAccessor("PhysicsManager", StaticAccessorType.DoubleColon)]
	[ThreadSafe]
	internal static extern int ResolveActorToInstanceID(IntPtr actorPtr);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[StaticAccessor("PhysicsManager", StaticAccessorType.DoubleColon)]
	internal static extern Collider GetColliderByInstanceID(int instanceID);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[StaticAccessor("PhysicsManager", StaticAccessorType.DoubleColon)]
	internal static extern Component GetBodyByInstanceID(int instanceID);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[StaticAccessor("PhysicsManager", StaticAccessorType.DoubleColon)]
	[ThreadSafe]
	internal static extern uint TranslateTriangleIndex(IntPtr shapePtr, uint rawIndex);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[ThreadSafe]
	[StaticAccessor("PhysicsManager", StaticAccessorType.DoubleColon)]
	internal static extern uint TranslateTriangleIndexFromID(int instanceID, uint faceIndex);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[StaticAccessor("PhysicsManager", StaticAccessorType.DoubleColon)]
	[ThreadSafe]
	internal static extern bool IsShapeTrigger(IntPtr shapePtr);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[StaticAccessor("PhysicsManager", StaticAccessorType.DoubleColon)]
	private static extern void SendOnCollisionEnter(Component component, Collision collision);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[StaticAccessor("PhysicsManager", StaticAccessorType.DoubleColon)]
	private static extern void SendOnCollisionStay(Component component, Collision collision);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[StaticAccessor("PhysicsManager", StaticAccessorType.DoubleColon)]
	private static extern void SendOnCollisionExit(Component component, Collision collision);

	[StaticAccessor("PhysicsManager", StaticAccessorType.DoubleColon)]
	[ThreadSafe]
	internal static Vector3 GetActorLinearVelocity(IntPtr actorPtr)
	{
		GetActorLinearVelocity_Injected(actorPtr, out var ret);
		return ret;
	}

	[StaticAccessor("PhysicsManager", StaticAccessorType.DoubleColon)]
	[ThreadSafe]
	internal static Vector3 GetActorAngularVelocity(IntPtr actorPtr)
	{
		GetActorAngularVelocity_Injected(actorPtr, out var ret);
		return ret;
	}

	[RequiredByNativeCode]
	private unsafe static void OnSceneContact(PhysicsScene scene, IntPtr buffer, int count)
	{
		if (count == 0)
		{
			return;
		}
		NativeArray<ContactPairHeader> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<ContactPairHeader>(buffer.ToPointer(), count, Allocator.None);
		try
		{
			Physics.ContactEvent?.Invoke(scene, nativeArray.AsReadOnly());
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
		finally
		{
			ReportContacts(nativeArray.AsReadOnly());
		}
	}

	private static void ReportContacts(NativeArray<ContactPairHeader>.ReadOnly array)
	{
		if (!invokeCollisionCallbacks)
		{
			return;
		}
		for (int i = 0; i < array.Length; i++)
		{
			ContactPairHeader header = array[i];
			if (header.HasRemovedBody)
			{
				continue;
			}
			for (int j = 0; j < header.m_NbPairs; j++)
			{
				ref readonly ContactPair contactPair = ref header.GetContactPair(j);
				if (!contactPair.HasRemovedCollider)
				{
					Component body = header.Body;
					Component otherBody = header.OtherBody;
					Component component = ((body != null) ? body : contactPair.Collider);
					Component component2 = ((otherBody != null) ? otherBody : contactPair.OtherCollider);
					if (contactPair.IsCollisionEnter)
					{
						SendOnCollisionEnter(component, GetCollisionToReport(in header, in contactPair, flipped: false));
						SendOnCollisionEnter(component2, GetCollisionToReport(in header, in contactPair, flipped: true));
					}
					if (contactPair.IsCollisionStay)
					{
						SendOnCollisionStay(component, GetCollisionToReport(in header, in contactPair, flipped: false));
						SendOnCollisionStay(component2, GetCollisionToReport(in header, in contactPair, flipped: true));
					}
					if (contactPair.IsCollisionExit)
					{
						SendOnCollisionExit(component, GetCollisionToReport(in header, in contactPair, flipped: false));
						SendOnCollisionExit(component2, GetCollisionToReport(in header, in contactPair, flipped: true));
					}
				}
			}
		}
	}

	private static Collision GetCollisionToReport(in ContactPairHeader header, in ContactPair pair, bool flipped)
	{
		if (reuseCollisionCallbacks)
		{
			s_ReusableCollision.Reuse(in header, in pair);
			s_ReusableCollision.Flipped = flipped;
			return s_ReusableCollision;
		}
		return new Collision(in header, in pair, flipped);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void get_gravity_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void set_gravity_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void get_defaultPhysicsScene_Injected(out PhysicsScene ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern RaycastHit[] Internal_RaycastAll_Injected(ref PhysicsScene physicsScene, ref Ray ray, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern RaycastHit[] Query_CapsuleCastAll_Injected(ref PhysicsScene physicsScene, ref Vector3 p0, ref Vector3 p1, float radius, ref Vector3 direction, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern RaycastHit[] Query_SphereCastAll_Injected(ref PhysicsScene physicsScene, ref Vector3 origin, float radius, ref Vector3 direction, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern Collider[] OverlapCapsule_Internal_Injected(ref PhysicsScene physicsScene, ref Vector3 point0, ref Vector3 point1, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern Collider[] OverlapSphere_Internal_Injected(ref PhysicsScene physicsScene, ref Vector3 position, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Simulate_Internal_Injected(ref PhysicsScene physicsScene, float step);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void InterpolateBodies_Internal_Injected(ref PhysicsScene physicsScene);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void ResetInterpolationPoses_Internal_Injected(ref PhysicsScene physicsScene);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool Query_ComputePenetration_Injected(Collider colliderA, ref Vector3 positionA, ref Quaternion rotationA, Collider colliderB, ref Vector3 positionB, ref Quaternion rotationB, ref Vector3 direction, ref float distance);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Query_ClosestPoint_Injected(Collider collider, ref Vector3 position, ref Quaternion rotation, ref Vector3 point, out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void get_clothGravity_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void set_clothGravity_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool CheckSphere_Internal_Injected(ref PhysicsScene physicsScene, ref Vector3 position, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool CheckCapsule_Internal_Injected(ref PhysicsScene physicsScene, ref Vector3 start, ref Vector3 end, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool CheckBox_Internal_Injected(ref PhysicsScene physicsScene, ref Vector3 center, ref Vector3 halfExtents, ref Quaternion orientation, int layermask, QueryTriggerInteraction queryTriggerInteraction);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern Collider[] OverlapBox_Internal_Injected(ref PhysicsScene physicsScene, ref Vector3 center, ref Vector3 halfExtents, ref Quaternion orientation, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern RaycastHit[] Internal_BoxCastAll_Injected(ref PhysicsScene physicsScene, ref Vector3 center, ref Vector3 halfExtents, ref Vector3 direction, ref Quaternion orientation, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Internal_RebuildBroadphaseRegions_Injected(ref Bounds bounds, int subdivisions);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void GetActorLinearVelocity_Injected(IntPtr actorPtr, out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void GetActorAngularVelocity_Injected(IntPtr actorPtr, out Vector3 ret);
}
public struct ModifiableContactPair
{
	private IntPtr actor;

	private IntPtr otherActor;

	private IntPtr shape;

	private IntPtr otherShape;

	public Quaternion rotation;

	public Vector3 position;

	public Quaternion otherRotation;

	public Vector3 otherPosition;

	private int numContacts;

	private IntPtr contacts;

	public int colliderInstanceID => Physics.ResolveShapeToInstanceID(shape);

	public int otherColliderInstanceID => Physics.ResolveShapeToInstanceID(otherShape);

	public int bodyInstanceID => Physics.ResolveActorToInstanceID(actor);

	public int otherBodyInstanceID => Physics.ResolveActorToInstanceID(otherActor);

	public Vector3 bodyVelocity => Physics.GetActorLinearVelocity(actor);

	public Vector3 bodyAngularVelocity => Physics.GetActorAngularVelocity(actor);

	public Vector3 otherBodyVelocity => Physics.GetActorLinearVelocity(otherActor);

	public Vector3 otherBodyAngularVelocity => Physics.GetActorAngularVelocity(otherActor);

	public int contactCount => numContacts;

	public unsafe ModifiableMassProperties massProperties
	{
		get
		{
			return GetContactPatch()->massProperties;
		}
		set
		{
			ModifiableContactPatch* contactPatch = GetContactPatch();
			contactPatch->massProperties = value;
			byte* internalFlags = &contactPatch->internalFlags;
			*internalFlags |= 8;
		}
	}

	public unsafe Vector3 GetPoint(int i)
	{
		return GetContact(i)->contact;
	}

	public unsafe void SetPoint(int i, Vector3 v)
	{
		GetContact(i)->contact = v;
	}

	public unsafe Vector3 GetNormal(int i)
	{
		return GetContact(i)->normal;
	}

	public unsafe void SetNormal(int i, Vector3 normal)
	{
		GetContact(i)->normal = normal;
		byte* internalFlags = &GetContactPatch()->internalFlags;
		*internalFlags |= 0x40;
	}

	public unsafe float GetSeparation(int i)
	{
		return GetContact(i)->separation;
	}

	public unsafe void SetSeparation(int i, float separation)
	{
		GetContact(i)->separation = separation;
	}

	public unsafe Vector3 GetTargetVelocity(int i)
	{
		return GetContact(i)->targetVelocity;
	}

	public unsafe void SetTargetVelocity(int i, Vector3 velocity)
	{
		GetContact(i)->targetVelocity = velocity;
		byte* internalFlags = &GetContactPatch()->internalFlags;
		*internalFlags |= 0x10;
	}

	public unsafe float GetBounciness(int i)
	{
		return GetContact(i)->restitution;
	}

	public unsafe void SetBounciness(int i, float bounciness)
	{
		GetContact(i)->restitution = bounciness;
		byte* internalFlags = &GetContactPatch()->internalFlags;
		*internalFlags |= 0x40;
	}

	public unsafe float GetStaticFriction(int i)
	{
		return GetContact(i)->staticFriction;
	}

	public unsafe void SetStaticFriction(int i, float staticFriction)
	{
		GetContact(i)->staticFriction = staticFriction;
		byte* internalFlags = &GetContactPatch()->internalFlags;
		*internalFlags |= 0x40;
	}

	public unsafe float GetDynamicFriction(int i)
	{
		return GetContact(i)->dynamicFriction;
	}

	public unsafe void SetDynamicFriction(int i, float dynamicFriction)
	{
		GetContact(i)->dynamicFriction = dynamicFriction;
		byte* internalFlags = &GetContactPatch()->internalFlags;
		*internalFlags |= 0x40;
	}

	public unsafe float GetMaxImpulse(int i)
	{
		return GetContact(i)->maxImpulse;
	}

	public unsafe void SetMaxImpulse(int i, float value)
	{
		GetContact(i)->maxImpulse = value;
		byte* internalFlags = &GetContactPatch()->internalFlags;
		*internalFlags |= 0x20;
	}

	public void IgnoreContact(int i)
	{
		SetMaxImpulse(i, 0f);
	}

	public unsafe uint GetFaceIndex(int i)
	{
		if ((GetContactPatch()->internalFlags & 1) != 0)
		{
			IntPtr intPtr = new IntPtr(contacts.ToInt64() + numContacts * sizeof(ModifiableContact) + (numContacts + i) * 4);
			uint rawIndex = *(uint*)(void*)intPtr;
			return Physics.TranslateTriangleIndex(otherShape, rawIndex);
		}
		return uint.MaxValue;
	}

	private unsafe ModifiableContact* GetContact(int index)
	{
		IntPtr intPtr = new IntPtr(contacts.ToInt64() + index * sizeof(ModifiableContact));
		return (ModifiableContact*)(void*)intPtr;
	}

	private unsafe ModifiableContactPatch* GetContactPatch()
	{
		IntPtr intPtr = new IntPtr(contacts.ToInt64() - numContacts * sizeof(ModifiableContactPatch));
		return (ModifiableContactPatch*)(void*)intPtr;
	}
}
public struct ModifiableMassProperties
{
	public float inverseMassScale;

	public float inverseInertiaScale;

	public float otherInverseMassScale;

	public float otherInverseInertiaScale;
}
internal struct ModifiableContact
{
	public Vector3 contact;

	public float separation;

	public Vector3 targetVelocity;

	public float maxImpulse;

	public Vector3 normal;

	public float restitution;

	public uint materialFlags;

	public ushort materialIndex;

	public ushort otherMaterialIndex;

	public float staticFriction;

	public float dynamicFriction;
}
internal struct ModifiableContactPatch
{
	public enum Flags
	{
		HasFaceIndices = 1,
		HasModifiedMassRatios = 8,
		HasTargetVelocity = 0x10,
		HasMaxImpulse = 0x20,
		RegeneratePatches = 0x40
	}

	public ModifiableMassProperties massProperties;

	public Vector3 normal;

	public float restitution;

	public float dynamicFriction;

	public float staticFriction;

	public byte startContactIndex;

	public byte contactCount;

	public byte materialFlags;

	public byte internalFlags;

	public ushort materialIndex;

	public ushort otherMaterialIndex;
}
[NativeHeader("Modules/Physics/PhysicMaterial.h")]
public class PhysicMaterial : Object
{
	public extern float bounciness
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float dynamicFriction
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float staticFriction
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern PhysicMaterialCombine frictionCombine
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern PhysicMaterialCombine bounceCombine
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[Obsolete("Use PhysicMaterial.bounciness instead (UnityUpgradable) -> bounciness")]
	public float bouncyness
	{
		get
		{
			return bounciness;
		}
		set
		{
			bounciness = value;
		}
	}

	[Obsolete("Anisotropic friction is no longer supported since Unity 5.0.", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public Vector3 frictionDirection2
	{
		get
		{
			return Vector3.zero;
		}
		set
		{
		}
	}

	[Obsolete("Anisotropic friction is no longer supported since Unity 5.0.", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public float dynamicFriction2
	{
		get
		{
			return 0f;
		}
		set
		{
		}
	}

	[Obsolete("Anisotropic friction is no longer supported since Unity 5.0.", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public float staticFriction2
	{
		get
		{
			return 0f;
		}
		set
		{
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Anisotropic friction is no longer supported since Unity 5.0.", true)]
	public Vector3 frictionDirection
	{
		get
		{
			return Vector3.zero;
		}
		set
		{
		}
	}

	public PhysicMaterial()
	{
		Internal_CreateDynamicsMaterial(this, "DynamicMaterial");
	}

	public PhysicMaterial(string name)
	{
		Internal_CreateDynamicsMaterial(this, name);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Internal_CreateDynamicsMaterial([Writable] PhysicMaterial mat, string name);
}
[NativeHeader("PhysicsScriptingClasses.h")]
[NativeHeader("Modules/Physics/RaycastHit.h")]
[NativeHeader("Runtime/Interfaces/IRaycast.h")]
[UsedByNativeCode]
public struct RaycastHit
{
	[NativeName("point")]
	internal Vector3 m_Point;

	[NativeName("normal")]
	internal Vector3 m_Normal;

	[NativeName("faceID")]
	internal uint m_FaceID;

	[NativeName("distance")]
	internal float m_Distance;

	[NativeName("uv")]
	internal Vector2 m_UV;

	[NativeName("collider")]
	internal int m_Collider;

	public Collider collider => Object.FindObjectFromInstanceID(m_Collider) as Collider;

	public int colliderInstanceID => m_Collider;

	public Vector3 point
	{
		get
		{
			return m_Point;
		}
		set
		{
			m_Point = value;
		}
	}

	public Vector3 normal
	{
		get
		{
			return m_Normal;
		}
		set
		{
			m_Normal = value;
		}
	}

	public Vector3 barycentricCoordinate
	{
		get
		{
			return new Vector3(1f - (m_UV.y + m_UV.x), m_UV.x, m_UV.y);
		}
		set
		{
			m_UV = value;
		}
	}

	public float distance
	{
		get
		{
			return m_Distance;
		}
		set
		{
			m_Distance = value;
		}
	}

	public int triangleIndex => (int)m_FaceID;

	public Vector2 textureCoord => CalculateRaycastTexCoord(m_Collider, m_UV, m_Point, m_FaceID, 0);

	public Vector2 textureCoord2 => CalculateRaycastTexCoord(m_Collider, m_UV, m_Point, m_FaceID, 1);

	public Transform transform
	{
		get
		{
			Rigidbody rigidbody = this.rigidbody;
			if (rigidbody != null)
			{
				return rigidbody.transform;
			}
			if (collider != null)
			{
				return collider.transform;
			}
			return null;
		}
	}

	public Rigidbody rigidbody => (collider != null) ? collider.attachedRigidbody : null;

	public ArticulationBody articulationBody => (collider != null) ? collider.attachedArticulationBody : null;

	public Vector2 lightmapCoord
	{
		get
		{
			Vector2 result = CalculateRaycastTexCoord(m_Collider, m_UV, m_Point, m_FaceID, 1);
			if (collider.GetComponent<Renderer>() != null)
			{
				Vector4 lightmapScaleOffset = collider.GetComponent<Renderer>().lightmapScaleOffset;
				result.x = result.x * lightmapScaleOffset.x + lightmapScaleOffset.z;
				result.y = result.y * lightmapScaleOffset.y + lightmapScaleOffset.w;
			}
			return result;
		}
	}

	[Obsolete("Use textureCoord2 instead. (UnityUpgradable) -> textureCoord2")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public Vector2 textureCoord1 => textureCoord2;

	[NativeMethod("CalculateRaycastTexCoord", true, true)]
	private static Vector2 CalculateRaycastTexCoord(int colliderInstanceID, Vector2 uv, Vector3 pos, uint face, int textcoord)
	{
		CalculateRaycastTexCoord_Injected(colliderInstanceID, ref uv, ref pos, face, textcoord, out var ret);
		return ret;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void CalculateRaycastTexCoord_Injected(int colliderInstanceID, ref Vector2 uv, ref Vector3 pos, uint face, int textcoord, out Vector2 ret);
}
[NativeHeader("Modules/Physics/Rigidbody.h")]
[RequireComponent(typeof(Transform))]
public class Rigidbody : Component
{
	public Vector3 velocity
	{
		get
		{
			get_velocity_Injected(out var ret);
			return ret;
		}
		set
		{
			set_velocity_Injected(ref value);
		}
	}

	public Vector3 angularVelocity
	{
		get
		{
			get_angularVelocity_Injected(out var ret);
			return ret;
		}
		set
		{
			set_angularVelocity_Injected(ref value);
		}
	}

	public extern float drag
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float angularDrag
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float mass
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool useGravity
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float maxDepenetrationVelocity
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool isKinematic
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool freezeRotation
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern RigidbodyConstraints constraints
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern CollisionDetectionMode collisionDetectionMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool automaticCenterOfMass
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public Vector3 centerOfMass
	{
		get
		{
			get_centerOfMass_Injected(out var ret);
			return ret;
		}
		set
		{
			set_centerOfMass_Injected(ref value);
		}
	}

	public Vector3 worldCenterOfMass
	{
		get
		{
			get_worldCenterOfMass_Injected(out var ret);
			return ret;
		}
	}

	public extern bool automaticInertiaTensor
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public Quaternion inertiaTensorRotation
	{
		get
		{
			get_inertiaTensorRotation_Injected(out var ret);
			return ret;
		}
		set
		{
			set_inertiaTensorRotation_Injected(ref value);
		}
	}

	public Vector3 inertiaTensor
	{
		get
		{
			get_inertiaTensor_Injected(out var ret);
			return ret;
		}
		set
		{
			set_inertiaTensor_Injected(ref value);
		}
	}

	public extern bool detectCollisions
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public Vector3 position
	{
		get
		{
			get_position_Injected(out var ret);
			return ret;
		}
		set
		{
			set_position_Injected(ref value);
		}
	}

	public Quaternion rotation
	{
		get
		{
			get_rotation_Injected(out var ret);
			return ret;
		}
		set
		{
			set_rotation_Injected(ref value);
		}
	}

	public extern RigidbodyInterpolation interpolation
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern int solverIterations
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float sleepThreshold
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float maxAngularVelocity
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float maxLinearVelocity
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern int solverVelocityIterations
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public LayerMask excludeLayers
	{
		get
		{
			get_excludeLayers_Injected(out var ret);
			return ret;
		}
		set
		{
			set_excludeLayers_Injected(ref value);
		}
	}

	public LayerMask includeLayers
	{
		get
		{
			get_includeLayers_Injected(out var ret);
			return ret;
		}
		set
		{
			set_includeLayers_Injected(ref value);
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("The sleepVelocity is no longer supported. Use sleepThreshold. Note that sleepThreshold is energy but not velocity.", true)]
	public float sleepVelocity
	{
		get
		{
			return 0f;
		}
		set
		{
		}
	}

	[Obsolete("The sleepAngularVelocity is no longer supported. Use sleepThreshold to specify energy.", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public float sleepAngularVelocity
	{
		get
		{
			return 0f;
		}
		set
		{
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Cone friction is no longer supported.", true)]
	public bool useConeFriction
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Please use Rigidbody.solverIterations instead. (UnityUpgradable) -> solverIterations")]
	public int solverIterationCount
	{
		get
		{
			return solverIterations;
		}
		set
		{
			solverIterations = value;
		}
	}

	[Obsolete("Please use Rigidbody.solverVelocityIterations instead. (UnityUpgradable) -> solverVelocityIterations")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public int solverVelocityIterationCount
	{
		get
		{
			return solverVelocityIterations;
		}
		set
		{
			solverVelocityIterations = value;
		}
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void SetDensity(float density);

	public void MovePosition(Vector3 position)
	{
		MovePosition_Injected(ref position);
	}

	public void MoveRotation(Quaternion rot)
	{
		MoveRotation_Injected(ref rot);
	}

	public void Move(Vector3 position, Quaternion rotation)
	{
		Move_Injected(ref position, ref rotation);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void Sleep();

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern bool IsSleeping();

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void WakeUp();

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void ResetCenterOfMass();

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void ResetInertiaTensor();

	public Vector3 GetRelativePointVelocity(Vector3 relativePoint)
	{
		GetRelativePointVelocity_Injected(ref relativePoint, out var ret);
		return ret;
	}

	public Vector3 GetPointVelocity(Vector3 worldPoint)
	{
		GetPointVelocity_Injected(ref worldPoint, out var ret);
		return ret;
	}

	public Vector3 GetAccumulatedForce([UnityEngine.Internal.DefaultValue("Time.fixedDeltaTime")] float step)
	{
		GetAccumulatedForce_Injected(step, out var ret);
		return ret;
	}

	[ExcludeFromDocs]
	public Vector3 GetAccumulatedForce()
	{
		return GetAccumulatedForce(Time.fixedDeltaTime);
	}

	public Vector3 GetAccumulatedTorque([UnityEngine.Internal.DefaultValue("Time.fixedDeltaTime")] float step)
	{
		GetAccumulatedTorque_Injected(step, out var ret);
		return ret;
	}

	[ExcludeFromDocs]
	public Vector3 GetAccumulatedTorque()
	{
		return GetAccumulatedTorque(Time.fixedDeltaTime);
	}

	public void AddForce(Vector3 force, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode)
	{
		AddForce_Injected(ref force, mode);
	}

	[ExcludeFromDocs]
	public void AddForce(Vector3 force)
	{
		AddForce(force, ForceMode.Force);
	}

	public void AddForce(float x, float y, float z, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode)
	{
		AddForce(new Vector3(x, y, z), mode);
	}

	[ExcludeFromDocs]
	public void AddForce(float x, float y, float z)
	{
		AddForce(new Vector3(x, y, z), ForceMode.Force);
	}

	public void AddRelativeForce(Vector3 force, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode)
	{
		AddRelativeForce_Injected(ref force, mode);
	}

	[ExcludeFromDocs]
	public void AddRelativeForce(Vector3 force)
	{
		AddRelativeForce(force, ForceMode.Force);
	}

	public void AddRelativeForce(float x, float y, float z, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode)
	{
		AddRelativeForce(new Vector3(x, y, z), mode);
	}

	[ExcludeFromDocs]
	public void AddRelativeForce(float x, float y, float z)
	{
		AddRelativeForce(new Vector3(x, y, z), ForceMode.Force);
	}

	public void AddTorque(Vector3 torque, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode)
	{
		AddTorque_Injected(ref torque, mode);
	}

	[ExcludeFromDocs]
	public void AddTorque(Vector3 torque)
	{
		AddTorque(torque, ForceMode.Force);
	}

	public void AddTorque(float x, float y, float z, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode)
	{
		AddTorque(new Vector3(x, y, z), mode);
	}

	[ExcludeFromDocs]
	public void AddTorque(float x, float y, float z)
	{
		AddTorque(new Vector3(x, y, z), ForceMode.Force);
	}

	public void AddRelativeTorque(Vector3 torque, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode)
	{
		AddRelativeTorque_Injected(ref torque, mode);
	}

	[ExcludeFromDocs]
	public void AddRelativeTorque(Vector3 torque)
	{
		AddRelativeTorque(torque, ForceMode.Force);
	}

	public void AddRelativeTorque(float x, float y, float z, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode)
	{
		AddRelativeTorque(new Vector3(x, y, z), mode);
	}

	[ExcludeFromDocs]
	public void AddRelativeTorque(float x, float y, float z)
	{
		AddRelativeTorque(x, y, z, ForceMode.Force);
	}

	public void AddForceAtPosition(Vector3 force, Vector3 position, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode)
	{
		AddForceAtPosition_Injected(ref force, ref position, mode);
	}

	[ExcludeFromDocs]
	public void AddForceAtPosition(Vector3 force, Vector3 position)
	{
		AddForceAtPosition(force, position, ForceMode.Force);
	}

	public void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius, [UnityEngine.Internal.DefaultValue("0.0f")] float upwardsModifier, [UnityEngine.Internal.DefaultValue("ForceMode.Force)")] ForceMode mode)
	{
		AddExplosionForce_Injected(explosionForce, ref explosionPosition, explosionRadius, upwardsModifier, mode);
	}

	[ExcludeFromDocs]
	public void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius, float upwardsModifier)
	{
		AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier, ForceMode.Force);
	}

	[ExcludeFromDocs]
	public void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius)
	{
		AddExplosionForce(explosionForce, explosionPosition, explosionRadius, 0f, ForceMode.Force);
	}

	[NativeName("ClosestPointOnBounds")]
	private void Internal_ClosestPointOnBounds(Vector3 point, ref Vector3 outPos, ref float distance)
	{
		Internal_ClosestPointOnBounds_Injected(ref point, ref outPos, ref distance);
	}

	public Vector3 ClosestPointOnBounds(Vector3 position)
	{
		float distance = 0f;
		Vector3 outPos = Vector3.zero;
		Internal_ClosestPointOnBounds(position, ref outPos, ref distance);
		return outPos;
	}

	private RaycastHit SweepTest(Vector3 direction, float maxDistance, QueryTriggerInteraction queryTriggerInteraction, ref bool hasHit)
	{
		SweepTest_Injected(ref direction, maxDistance, queryTriggerInteraction, ref hasHit, out var ret);
		return ret;
	}

	public bool SweepTest(Vector3 direction, out RaycastHit hitInfo, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		float magnitude = direction.magnitude;
		if (magnitude > float.Epsilon)
		{
			Vector3 direction2 = direction / magnitude;
			bool hasHit = false;
			hitInfo = SweepTest(direction2, maxDistance, queryTriggerInteraction, ref hasHit);
			return hasHit;
		}
		hitInfo = default(RaycastHit);
		return false;
	}

	[ExcludeFromDocs]
	public bool SweepTest(Vector3 direction, out RaycastHit hitInfo, float maxDistance)
	{
		return SweepTest(direction, out hitInfo, maxDistance, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public bool SweepTest(Vector3 direction, out RaycastHit hitInfo)
	{
		return SweepTest(direction, out hitInfo, float.PositiveInfinity, QueryTriggerInteraction.UseGlobal);
	}

	[NativeName("SweepTestAll")]
	private RaycastHit[] Internal_SweepTestAll(Vector3 direction, float maxDistance, QueryTriggerInteraction queryTriggerInteraction)
	{
		return Internal_SweepTestAll_Injected(ref direction, maxDistance, queryTriggerInteraction);
	}

	public RaycastHit[] SweepTestAll(Vector3 direction, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		float magnitude = direction.magnitude;
		if (magnitude > float.Epsilon)
		{
			Vector3 direction2 = direction / magnitude;
			return Internal_SweepTestAll(direction2, maxDistance, queryTriggerInteraction);
		}
		return new RaycastHit[0];
	}

	[ExcludeFromDocs]
	public RaycastHit[] SweepTestAll(Vector3 direction, float maxDistance)
	{
		return SweepTestAll(direction, maxDistance, QueryTriggerInteraction.UseGlobal);
	}

	[ExcludeFromDocs]
	public RaycastHit[] SweepTestAll(Vector3 direction)
	{
		return SweepTestAll(direction, float.PositiveInfinity, QueryTriggerInteraction.UseGlobal);
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Use Rigidbody.maxAngularVelocity instead.")]
	public void SetMaxAngularVelocity(float a)
	{
		maxAngularVelocity = a;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_velocity_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_velocity_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_angularVelocity_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_angularVelocity_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_centerOfMass_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_centerOfMass_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_worldCenterOfMass_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_inertiaTensorRotation_Injected(out Quaternion ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_inertiaTensorRotation_Injected(ref Quaternion value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_inertiaTensor_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_inertiaTensor_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_position_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_position_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_rotation_Injected(out Quaternion ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_rotation_Injected(ref Quaternion value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void MovePosition_Injected(ref Vector3 position);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void MoveRotation_Injected(ref Quaternion rot);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void Move_Injected(ref Vector3 position, ref Quaternion rotation);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetRelativePointVelocity_Injected(ref Vector3 relativePoint, out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetPointVelocity_Injected(ref Vector3 worldPoint, out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_excludeLayers_Injected(out LayerMask ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_excludeLayers_Injected(ref LayerMask value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_includeLayers_Injected(out LayerMask ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_includeLayers_Injected(ref LayerMask value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetAccumulatedForce_Injected([UnityEngine.Internal.DefaultValue("Time.fixedDeltaTime")] float step, out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetAccumulatedTorque_Injected([UnityEngine.Internal.DefaultValue("Time.fixedDeltaTime")] float step, out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void AddForce_Injected(ref Vector3 force, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void AddRelativeForce_Injected(ref Vector3 force, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void AddTorque_Injected(ref Vector3 torque, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void AddRelativeTorque_Injected(ref Vector3 torque, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void AddForceAtPosition_Injected(ref Vector3 force, ref Vector3 position, [UnityEngine.Internal.DefaultValue("ForceMode.Force")] ForceMode mode);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void AddExplosionForce_Injected(float explosionForce, ref Vector3 explosionPosition, float explosionRadius, [UnityEngine.Internal.DefaultValue("0.0f")] float upwardsModifier, [UnityEngine.Internal.DefaultValue("ForceMode.Force)")] ForceMode mode);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void Internal_ClosestPointOnBounds_Injected(ref Vector3 point, ref Vector3 outPos, ref float distance);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void SweepTest_Injected(ref Vector3 direction, float maxDistance, QueryTriggerInteraction queryTriggerInteraction, ref bool hasHit, out RaycastHit ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern RaycastHit[] Internal_SweepTestAll_Injected(ref Vector3 direction, float maxDistance, QueryTriggerInteraction queryTriggerInteraction);
}
[NativeHeader("Modules/Physics/Collider.h")]
[RequireComponent(typeof(Transform))]
[RequiredByNativeCode]
public class Collider : Component
{
	public extern bool enabled
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern Rigidbody attachedRigidbody
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetRigidbody")]
		get;
	}

	public extern ArticulationBody attachedArticulationBody
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetArticulationBody")]
		get;
	}

	public extern bool isTrigger
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float contactOffset
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public Bounds bounds
	{
		get
		{
			get_bounds_Injected(out var ret);
			return ret;
		}
	}

	public extern bool hasModifiableContacts
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool providesContacts
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern int layerOverridePriority
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public LayerMask excludeLayers
	{
		get
		{
			get_excludeLayers_Injected(out var ret);
			return ret;
		}
		set
		{
			set_excludeLayers_Injected(ref value);
		}
	}

	public LayerMask includeLayers
	{
		get
		{
			get_includeLayers_Injected(out var ret);
			return ret;
		}
		set
		{
			set_includeLayers_Injected(ref value);
		}
	}

	[NativeMethod("Material")]
	public extern PhysicMaterial sharedMaterial
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern PhysicMaterial material
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetClonedMaterial")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("SetMaterial")]
		set;
	}

	public Vector3 ClosestPoint(Vector3 position)
	{
		ClosestPoint_Injected(ref position, out var ret);
		return ret;
	}

	private RaycastHit Raycast(Ray ray, float maxDistance, ref bool hasHit)
	{
		Raycast_Injected(ref ray, maxDistance, ref hasHit, out var ret);
		return ret;
	}

	public bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance)
	{
		bool hasHit = false;
		hitInfo = Raycast(ray, maxDistance, ref hasHit);
		return hasHit;
	}

	[NativeName("ClosestPointOnBounds")]
	private void Internal_ClosestPointOnBounds(Vector3 point, ref Vector3 outPos, ref float distance)
	{
		Internal_ClosestPointOnBounds_Injected(ref point, ref outPos, ref distance);
	}

	public Vector3 ClosestPointOnBounds(Vector3 position)
	{
		float distance = 0f;
		Vector3 outPos = Vector3.zero;
		Internal_ClosestPointOnBounds(position, ref outPos, ref distance);
		return outPos;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void ClosestPoint_Injected(ref Vector3 position, out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_bounds_Injected(out Bounds ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_excludeLayers_Injected(out LayerMask ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_excludeLayers_Injected(ref LayerMask value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_includeLayers_Injected(out LayerMask ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_includeLayers_Injected(ref LayerMask value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void Raycast_Injected(ref Ray ray, float maxDistance, ref bool hasHit, out RaycastHit ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void Internal_ClosestPointOnBounds_Injected(ref Vector3 point, ref Vector3 outPos, ref float distance);
}
[NativeHeader("Modules/Physics/CharacterController.h")]
public class CharacterController : Collider
{
	public Vector3 velocity
	{
		get
		{
			get_velocity_Injected(out var ret);
			return ret;
		}
	}

	public extern bool isGrounded
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("IsGrounded")]
		get;
	}

	public extern CollisionFlags collisionFlags
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern float radius
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float height
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public Vector3 center
	{
		get
		{
			get_center_Injected(out var ret);
			return ret;
		}
		set
		{
			set_center_Injected(ref value);
		}
	}

	public extern float slopeLimit
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float stepOffset
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float skinWidth
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float minMoveDistance
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool detectCollisions
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool enableOverlapRecovery
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public bool SimpleMove(Vector3 speed)
	{
		return SimpleMove_Injected(ref speed);
	}

	public CollisionFlags Move(Vector3 motion)
	{
		return Move_Injected(ref motion);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern bool SimpleMove_Injected(ref Vector3 speed);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern CollisionFlags Move_Injected(ref Vector3 motion);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_velocity_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_center_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_center_Injected(ref Vector3 value);
}
[RequiredByNativeCode]
[NativeHeader("Modules/Physics/MeshCollider.h")]
[NativeHeader("Runtime/Graphics/Mesh/Mesh.h")]
public class MeshCollider : Collider
{
	public extern Mesh sharedMesh
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool convex
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern MeshColliderCookingOptions cookingOptions
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Configuring smooth sphere collisions is no longer needed.", true)]
	public bool smoothSphereCollisions
	{
		get
		{
			return true;
		}
		set
		{
		}
	}

	[Obsolete("MeshCollider.skinWidth is no longer used.")]
	public float skinWidth
	{
		get
		{
			return 0f;
		}
		set
		{
		}
	}

	[Obsolete("MeshCollider.inflateMesh is no longer supported. The new cooking algorithm doesn't need inflation to be used.")]
	public bool inflateMesh
	{
		get
		{
			return false;
		}
		set
		{
		}
	}
}
[RequiredByNativeCode]
[NativeHeader("Modules/Physics/CapsuleCollider.h")]
public class CapsuleCollider : Collider
{
	public Vector3 center
	{
		get
		{
			get_center_Injected(out var ret);
			return ret;
		}
		set
		{
			set_center_Injected(ref value);
		}
	}

	public extern float radius
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float height
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern int direction
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	internal Vector2 GetGlobalExtents()
	{
		GetGlobalExtents_Injected(out var ret);
		return ret;
	}

	internal Matrix4x4 CalculateTransform()
	{
		CalculateTransform_Injected(out var ret);
		return ret;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_center_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_center_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetGlobalExtents_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void CalculateTransform_Injected(out Matrix4x4 ret);
}
[RequiredByNativeCode]
[NativeHeader("Modules/Physics/BoxCollider.h")]
public class BoxCollider : Collider
{
	public Vector3 center
	{
		get
		{
			get_center_Injected(out var ret);
			return ret;
		}
		set
		{
			set_center_Injected(ref value);
		}
	}

	public Vector3 size
	{
		get
		{
			get_size_Injected(out var ret);
			return ret;
		}
		set
		{
			set_size_Injected(ref value);
		}
	}

	[Obsolete("Use BoxCollider.size instead. (UnityUpgradable) -> size")]
	public Vector3 extents
	{
		get
		{
			return size * 0.5f;
		}
		set
		{
			size = value * 2f;
		}
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_center_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_center_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_size_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_size_Injected(ref Vector3 value);
}
[RequiredByNativeCode]
[NativeHeader("Modules/Physics/SphereCollider.h")]
public class SphereCollider : Collider
{
	public Vector3 center
	{
		get
		{
			get_center_Injected(out var ret);
			return ret;
		}
		set
		{
			set_center_Injected(ref value);
		}
	}

	public extern float radius
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_center_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_center_Injected(ref Vector3 value);
}
[RequireComponent(typeof(Rigidbody))]
[NativeHeader("Modules/Physics/ConstantForce.h")]
public class ConstantForce : Behaviour
{
	public Vector3 force
	{
		get
		{
			get_force_Injected(out var ret);
			return ret;
		}
		set
		{
			set_force_Injected(ref value);
		}
	}

	public Vector3 relativeForce
	{
		get
		{
			get_relativeForce_Injected(out var ret);
			return ret;
		}
		set
		{
			set_relativeForce_Injected(ref value);
		}
	}

	public Vector3 torque
	{
		get
		{
			get_torque_Injected(out var ret);
			return ret;
		}
		set
		{
			set_torque_Injected(ref value);
		}
	}

	public Vector3 relativeTorque
	{
		get
		{
			get_relativeTorque_Injected(out var ret);
			return ret;
		}
		set
		{
			set_relativeTorque_Injected(ref value);
		}
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_force_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_force_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_relativeForce_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_relativeForce_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_torque_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_torque_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_relativeTorque_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_relativeTorque_Injected(ref Vector3 value);
}
[RequireComponent(typeof(Rigidbody))]
[NativeHeader("Modules/Physics/Joint.h")]
[NativeClass("Unity::Joint")]
public class Joint : Component
{
	public extern Rigidbody connectedBody
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern ArticulationBody connectedArticulationBody
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public Vector3 axis
	{
		get
		{
			get_axis_Injected(out var ret);
			return ret;
		}
		set
		{
			set_axis_Injected(ref value);
		}
	}

	public Vector3 anchor
	{
		get
		{
			get_anchor_Injected(out var ret);
			return ret;
		}
		set
		{
			set_anchor_Injected(ref value);
		}
	}

	public Vector3 connectedAnchor
	{
		get
		{
			get_connectedAnchor_Injected(out var ret);
			return ret;
		}
		set
		{
			set_connectedAnchor_Injected(ref value);
		}
	}

	public extern bool autoConfigureConnectedAnchor
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float breakForce
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float breakTorque
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool enableCollision
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool enablePreprocessing
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float massScale
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float connectedMassScale
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public Vector3 currentForce
	{
		get
		{
			Vector3 linearForce = Vector3.zero;
			Vector3 angularForce = Vector3.zero;
			GetCurrentForces(ref linearForce, ref angularForce);
			return linearForce;
		}
	}

	public Vector3 currentTorque
	{
		get
		{
			Vector3 linearForce = Vector3.zero;
			Vector3 angularForce = Vector3.zero;
			GetCurrentForces(ref linearForce, ref angularForce);
			return angularForce;
		}
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetCurrentForces(ref Vector3 linearForce, ref Vector3 angularForce);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_axis_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_axis_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_anchor_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_anchor_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_connectedAnchor_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_connectedAnchor_Injected(ref Vector3 value);
}
[NativeClass("Unity::HingeJoint")]
[NativeHeader("Modules/Physics/HingeJoint.h")]
public class HingeJoint : Joint
{
	public JointMotor motor
	{
		get
		{
			get_motor_Injected(out var ret);
			return ret;
		}
		set
		{
			set_motor_Injected(ref value);
		}
	}

	public JointLimits limits
	{
		get
		{
			get_limits_Injected(out var ret);
			return ret;
		}
		set
		{
			set_limits_Injected(ref value);
		}
	}

	public JointSpring spring
	{
		get
		{
			get_spring_Injected(out var ret);
			return ret;
		}
		set
		{
			set_spring_Injected(ref value);
		}
	}

	public extern bool useMotor
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool useLimits
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool extendedLimits
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool useSpring
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float velocity
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern float angle
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern bool useAcceleration
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_motor_Injected(out JointMotor ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_motor_Injected(ref JointMotor value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_limits_Injected(out JointLimits ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_limits_Injected(ref JointLimits value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_spring_Injected(out JointSpring ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_spring_Injected(ref JointSpring value);
}
[NativeHeader("Modules/Physics/SpringJoint.h")]
[NativeClass("Unity::SpringJoint")]
public class SpringJoint : Joint
{
	public extern float spring
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float damper
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float minDistance
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float maxDistance
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float tolerance
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}
}
[NativeHeader("Modules/Physics/FixedJoint.h")]
[NativeClass("Unity::FixedJoint")]
public class FixedJoint : Joint
{
}
[NativeClass("Unity::CharacterJoint")]
[NativeHeader("Modules/Physics/CharacterJoint.h")]
public class CharacterJoint : Joint
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("TargetRotation not in use for Unity 5 and assumed disabled.", true)]
	public Quaternion targetRotation;

	[Obsolete("TargetAngularVelocity not in use for Unity 5 and assumed disabled.", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public Vector3 targetAngularVelocity;

	[Obsolete("RotationDrive not in use for Unity 5 and assumed disabled.", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public JointDrive rotationDrive;

	public Vector3 swingAxis
	{
		get
		{
			get_swingAxis_Injected(out var ret);
			return ret;
		}
		set
		{
			set_swingAxis_Injected(ref value);
		}
	}

	public SoftJointLimitSpring twistLimitSpring
	{
		get
		{
			get_twistLimitSpring_Injected(out var ret);
			return ret;
		}
		set
		{
			set_twistLimitSpring_Injected(ref value);
		}
	}

	public SoftJointLimitSpring swingLimitSpring
	{
		get
		{
			get_swingLimitSpring_Injected(out var ret);
			return ret;
		}
		set
		{
			set_swingLimitSpring_Injected(ref value);
		}
	}

	public SoftJointLimit lowTwistLimit
	{
		get
		{
			get_lowTwistLimit_Injected(out var ret);
			return ret;
		}
		set
		{
			set_lowTwistLimit_Injected(ref value);
		}
	}

	public SoftJointLimit highTwistLimit
	{
		get
		{
			get_highTwistLimit_Injected(out var ret);
			return ret;
		}
		set
		{
			set_highTwistLimit_Injected(ref value);
		}
	}

	public SoftJointLimit swing1Limit
	{
		get
		{
			get_swing1Limit_Injected(out var ret);
			return ret;
		}
		set
		{
			set_swing1Limit_Injected(ref value);
		}
	}

	public SoftJointLimit swing2Limit
	{
		get
		{
			get_swing2Limit_Injected(out var ret);
			return ret;
		}
		set
		{
			set_swing2Limit_Injected(ref value);
		}
	}

	public extern bool enableProjection
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float projectionDistance
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float projectionAngle
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_swingAxis_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_swingAxis_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_twistLimitSpring_Injected(out SoftJointLimitSpring ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_twistLimitSpring_Injected(ref SoftJointLimitSpring value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_swingLimitSpring_Injected(out SoftJointLimitSpring ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_swingLimitSpring_Injected(ref SoftJointLimitSpring value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_lowTwistLimit_Injected(out SoftJointLimit ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_lowTwistLimit_Injected(ref SoftJointLimit value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_highTwistLimit_Injected(out SoftJointLimit ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_highTwistLimit_Injected(ref SoftJointLimit value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_swing1Limit_Injected(out SoftJointLimit ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_swing1Limit_Injected(ref SoftJointLimit value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_swing2Limit_Injected(out SoftJointLimit ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_swing2Limit_Injected(ref SoftJointLimit value);
}
[NativeClass("Unity::ConfigurableJoint")]
[NativeHeader("Modules/Physics/ConfigurableJoint.h")]
public class ConfigurableJoint : Joint
{
	public Vector3 secondaryAxis
	{
		get
		{
			get_secondaryAxis_Injected(out var ret);
			return ret;
		}
		set
		{
			set_secondaryAxis_Injected(ref value);
		}
	}

	public extern ConfigurableJointMotion xMotion
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern ConfigurableJointMotion yMotion
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern ConfigurableJointMotion zMotion
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern ConfigurableJointMotion angularXMotion
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern ConfigurableJointMotion angularYMotion
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern ConfigurableJointMotion angularZMotion
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public SoftJointLimitSpring linearLimitSpring
	{
		get
		{
			get_linearLimitSpring_Injected(out var ret);
			return ret;
		}
		set
		{
			set_linearLimitSpring_Injected(ref value);
		}
	}

	public SoftJointLimitSpring angularXLimitSpring
	{
		get
		{
			get_angularXLimitSpring_Injected(out var ret);
			return ret;
		}
		set
		{
			set_angularXLimitSpring_Injected(ref value);
		}
	}

	public SoftJointLimitSpring angularYZLimitSpring
	{
		get
		{
			get_angularYZLimitSpring_Injected(out var ret);
			return ret;
		}
		set
		{
			set_angularYZLimitSpring_Injected(ref value);
		}
	}

	public SoftJointLimit linearLimit
	{
		get
		{
			get_linearLimit_Injected(out var ret);
			return ret;
		}
		set
		{
			set_linearLimit_Injected(ref value);
		}
	}

	public SoftJointLimit lowAngularXLimit
	{
		get
		{
			get_lowAngularXLimit_Injected(out var ret);
			return ret;
		}
		set
		{
			set_lowAngularXLimit_Injected(ref value);
		}
	}

	public SoftJointLimit highAngularXLimit
	{
		get
		{
			get_highAngularXLimit_Injected(out var ret);
			return ret;
		}
		set
		{
			set_highAngularXLimit_Injected(ref value);
		}
	}

	public SoftJointLimit angularYLimit
	{
		get
		{
			get_angularYLimit_Injected(out var ret);
			return ret;
		}
		set
		{
			set_angularYLimit_Injected(ref value);
		}
	}

	public SoftJointLimit angularZLimit
	{
		get
		{
			get_angularZLimit_Injected(out var ret);
			return ret;
		}
		set
		{
			set_angularZLimit_Injected(ref value);
		}
	}

	public Vector3 targetPosition
	{
		get
		{
			get_targetPosition_Injected(out var ret);
			return ret;
		}
		set
		{
			set_targetPosition_Injected(ref value);
		}
	}

	public Vector3 targetVelocity
	{
		get
		{
			get_targetVelocity_Injected(out var ret);
			return ret;
		}
		set
		{
			set_targetVelocity_Injected(ref value);
		}
	}

	public JointDrive xDrive
	{
		get
		{
			get_xDrive_Injected(out var ret);
			return ret;
		}
		set
		{
			set_xDrive_Injected(ref value);
		}
	}

	public JointDrive yDrive
	{
		get
		{
			get_yDrive_Injected(out var ret);
			return ret;
		}
		set
		{
			set_yDrive_Injected(ref value);
		}
	}

	public JointDrive zDrive
	{
		get
		{
			get_zDrive_Injected(out var ret);
			return ret;
		}
		set
		{
			set_zDrive_Injected(ref value);
		}
	}

	public Quaternion targetRotation
	{
		get
		{
			get_targetRotation_Injected(out var ret);
			return ret;
		}
		set
		{
			set_targetRotation_Injected(ref value);
		}
	}

	public Vector3 targetAngularVelocity
	{
		get
		{
			get_targetAngularVelocity_Injected(out var ret);
			return ret;
		}
		set
		{
			set_targetAngularVelocity_Injected(ref value);
		}
	}

	public extern RotationDriveMode rotationDriveMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public JointDrive angularXDrive
	{
		get
		{
			get_angularXDrive_Injected(out var ret);
			return ret;
		}
		set
		{
			set_angularXDrive_Injected(ref value);
		}
	}

	public JointDrive angularYZDrive
	{
		get
		{
			get_angularYZDrive_Injected(out var ret);
			return ret;
		}
		set
		{
			set_angularYZDrive_Injected(ref value);
		}
	}

	public JointDrive slerpDrive
	{
		get
		{
			get_slerpDrive_Injected(out var ret);
			return ret;
		}
		set
		{
			set_slerpDrive_Injected(ref value);
		}
	}

	public extern JointProjectionMode projectionMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float projectionDistance
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float projectionAngle
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool configuredInWorldSpace
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool swapBodies
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_secondaryAxis_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_secondaryAxis_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_linearLimitSpring_Injected(out SoftJointLimitSpring ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_linearLimitSpring_Injected(ref SoftJointLimitSpring value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_angularXLimitSpring_Injected(out SoftJointLimitSpring ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_angularXLimitSpring_Injected(ref SoftJointLimitSpring value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_angularYZLimitSpring_Injected(out SoftJointLimitSpring ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_angularYZLimitSpring_Injected(ref SoftJointLimitSpring value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_linearLimit_Injected(out SoftJointLimit ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_linearLimit_Injected(ref SoftJointLimit value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_lowAngularXLimit_Injected(out SoftJointLimit ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_lowAngularXLimit_Injected(ref SoftJointLimit value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_highAngularXLimit_Injected(out SoftJointLimit ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_highAngularXLimit_Injected(ref SoftJointLimit value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_angularYLimit_Injected(out SoftJointLimit ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_angularYLimit_Injected(ref SoftJointLimit value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_angularZLimit_Injected(out SoftJointLimit ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_angularZLimit_Injected(ref SoftJointLimit value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_targetPosition_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_targetPosition_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_targetVelocity_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_targetVelocity_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_xDrive_Injected(out JointDrive ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_xDrive_Injected(ref JointDrive value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_yDrive_Injected(out JointDrive ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_yDrive_Injected(ref JointDrive value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_zDrive_Injected(out JointDrive ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_zDrive_Injected(ref JointDrive value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_targetRotation_Injected(out Quaternion ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_targetRotation_Injected(ref Quaternion value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_targetAngularVelocity_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_targetAngularVelocity_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_angularXDrive_Injected(out JointDrive ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_angularXDrive_Injected(ref JointDrive value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_angularYZDrive_Injected(out JointDrive ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_angularYZDrive_Injected(ref JointDrive value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_slerpDrive_Injected(out JointDrive ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_slerpDrive_Injected(ref JointDrive value);
}
[UsedByNativeCode]
[NativeHeader("Modules/Physics/MessageParameters.h")]
public struct ContactPoint
{
	internal Vector3 m_Point;

	internal Vector3 m_Normal;

	internal Vector3 m_Impulse;

	internal int m_ThisColliderInstanceID;

	internal int m_OtherColliderInstanceID;

	internal float m_Separation;

	public Vector3 point => m_Point;

	public Vector3 normal => m_Normal;

	public Vector3 impulse => m_Impulse;

	public Collider thisCollider => Physics.GetColliderByInstanceID(m_ThisColliderInstanceID);

	public Collider otherCollider => Physics.GetColliderByInstanceID(m_OtherColliderInstanceID);

	public float separation => m_Separation;

	internal ContactPoint(Vector3 point, Vector3 normal, Vector3 impulse, float separation, int thisInstanceID, int otherInstenceID)
	{
		m_Point = point;
		m_Normal = normal;
		m_Impulse = impulse;
		m_Separation = separation;
		m_ThisColliderInstanceID = thisInstanceID;
		m_OtherColliderInstanceID = otherInstenceID;
	}
}
[NativeHeader("Modules/Physics/Public/PhysicsSceneHandle.h")]
public struct PhysicsScene : IEquatable<PhysicsScene>
{
	private int m_Handle;

	public override string ToString()
	{
		return UnityString.Format("({0})", m_Handle);
	}

	public static bool operator ==(PhysicsScene lhs, PhysicsScene rhs)
	{
		return lhs.m_Handle == rhs.m_Handle;
	}

	public static bool operator !=(PhysicsScene lhs, PhysicsScene rhs)
	{
		return lhs.m_Handle != rhs.m_Handle;
	}

	public override int GetHashCode()
	{
		return m_Handle;
	}

	public override bool Equals(object other)
	{
		if (!(other is PhysicsScene physicsScene))
		{
			return false;
		}
		return m_Handle == physicsScene.m_Handle;
	}

	public bool Equals(PhysicsScene other)
	{
		return m_Handle == other.m_Handle;
	}

	public bool IsValid()
	{
		return IsValid_Internal(this);
	}

	[NativeMethod("IsPhysicsSceneValid")]
	[StaticAccessor("GetPhysicsManager()", StaticAccessorType.Dot)]
	private static bool IsValid_Internal(PhysicsScene physicsScene)
	{
		return IsValid_Internal_Injected(ref physicsScene);
	}

	public bool IsEmpty()
	{
		if (IsValid())
		{
			return IsEmpty_Internal(this);
		}
		throw new InvalidOperationException("Cannot check if physics scene is empty as it is invalid.");
	}

	[NativeMethod("IsPhysicsWorldEmpty")]
	[StaticAccessor("GetPhysicsManager()", StaticAccessorType.Dot)]
	private static bool IsEmpty_Internal(PhysicsScene physicsScene)
	{
		return IsEmpty_Internal_Injected(ref physicsScene);
	}

	public void Simulate(float step)
	{
		if (IsValid())
		{
			if (this == Physics.defaultPhysicsScene && Physics.simulationMode != SimulationMode.Script)
			{
				Debug.LogWarning("PhysicsScene.Simulate(...) was called but simulation mode is not set to Script. You should set simulation mode to Script first before calling this function therefore the simulation was not run.");
			}
			else
			{
				Physics.Simulate_Internal(this, step);
			}
			return;
		}
		throw new InvalidOperationException("Cannot simulate the physics scene as it is invalid.");
	}

	public void InterpolateBodies()
	{
		if (!IsValid())
		{
			throw new InvalidOperationException("Cannot interpolate the physics scene as it is invalid.");
		}
		if (this == Physics.defaultPhysicsScene)
		{
			Debug.LogWarning("PhysicsScene.InterpolateBodies() was called on the default Physics Scene. This is done automatically and the call will be ignored");
		}
		else
		{
			Physics.InterpolateBodies_Internal(this);
		}
	}

	public void ResetInterpolationPoses()
	{
		if (!IsValid())
		{
			throw new InvalidOperationException("Cannot reset poses of the physics scene as it is invalid.");
		}
		if (this == Physics.defaultPhysicsScene)
		{
			Debug.LogWarning("PhysicsScene.ResetInterpolationPoses() was called on the default Physics Scene. This is done automatically and the call will be ignored");
		}
		else
		{
			Physics.ResetInterpolationPoses_Internal(this);
		}
	}

	public bool Raycast(Vector3 origin, Vector3 direction, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [UnityEngine.Internal.DefaultValue("Physics.DefaultRaycastLayers")] int layerMask = -5, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
	{
		float magnitude = direction.magnitude;
		if (magnitude > float.Epsilon)
		{
			Vector3 direction2 = direction / magnitude;
			return Internal_RaycastTest(ray: new Ray(origin, direction2), physicsScene: this, maxDistance: maxDistance, layerMask: layerMask, queryTriggerInteraction: queryTriggerInteraction);
		}
		return false;
	}

	[NativeName("RaycastTest")]
	[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()", StaticAccessorType.Dot)]
	private static bool Internal_RaycastTest(PhysicsScene physicsScene, Ray ray, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
	{
		return Internal_RaycastTest_Injected(ref physicsScene, ref ray, maxDistance, layerMask, queryTriggerInteraction);
	}

	public bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [UnityEngine.Internal.DefaultValue("Physics.DefaultRaycastLayers")] int layerMask = -5, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
	{
		hitInfo = default(RaycastHit);
		float magnitude = direction.magnitude;
		if (magnitude > float.Epsilon)
		{
			Vector3 direction2 = direction / magnitude;
			return Internal_Raycast(ray: new Ray(origin, direction2), physicsScene: this, maxDistance: maxDistance, hit: ref hitInfo, layerMask: layerMask, queryTriggerInteraction: queryTriggerInteraction);
		}
		return false;
	}

	[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()", StaticAccessorType.Dot)]
	[NativeName("Raycast")]
	private static bool Internal_Raycast(PhysicsScene physicsScene, Ray ray, float maxDistance, ref RaycastHit hit, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
	{
		return Internal_Raycast_Injected(ref physicsScene, ref ray, maxDistance, ref hit, layerMask, queryTriggerInteraction);
	}

	public int Raycast(Vector3 origin, Vector3 direction, RaycastHit[] raycastHits, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [UnityEngine.Internal.DefaultValue("Physics.DefaultRaycastLayers")] int layerMask = -5, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
	{
		float magnitude = direction.magnitude;
		if (magnitude > float.Epsilon)
		{
			return Internal_RaycastNonAlloc(ray: new Ray(origin, direction.normalized), physicsScene: this, raycastHits: raycastHits, maxDistance: maxDistance, mask: layerMask, queryTriggerInteraction: queryTriggerInteraction);
		}
		return 0;
	}

	[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
	[NativeName("RaycastNonAlloc")]
	private static int Internal_RaycastNonAlloc(PhysicsScene physicsScene, Ray ray, [Unmarshalled] RaycastHit[] raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
	{
		return Internal_RaycastNonAlloc_Injected(ref physicsScene, ref ray, raycastHits, maxDistance, mask, queryTriggerInteraction);
	}

	[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()", StaticAccessorType.Dot)]
	[NativeName("CapsuleCast")]
	private static bool Query_CapsuleCast(PhysicsScene physicsScene, Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance, ref RaycastHit hitInfo, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
	{
		return Query_CapsuleCast_Injected(ref physicsScene, ref point1, ref point2, radius, ref direction, maxDistance, ref hitInfo, layerMask, queryTriggerInteraction);
	}

	private static bool Internal_CapsuleCast(PhysicsScene physicsScene, Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
	{
		float magnitude = direction.magnitude;
		hitInfo = default(RaycastHit);
		if (magnitude > float.Epsilon)
		{
			Vector3 direction2 = direction / magnitude;
			return Query_CapsuleCast(physicsScene, point1, point2, radius, direction2, maxDistance, ref hitInfo, layerMask, queryTriggerInteraction);
		}
		return false;
	}

	public bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
	{
		return Internal_CapsuleCast(this, point1, point2, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
	}

	[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
	[NativeName("CapsuleCastNonAlloc")]
	private static int Internal_CapsuleCastNonAlloc(PhysicsScene physicsScene, Vector3 p0, Vector3 p1, float radius, Vector3 direction, [Unmarshalled] RaycastHit[] raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
	{
		return Internal_CapsuleCastNonAlloc_Injected(ref physicsScene, ref p0, ref p1, radius, ref direction, raycastHits, maxDistance, mask, queryTriggerInteraction);
	}

	public int CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
	{
		float magnitude = direction.magnitude;
		if (magnitude > float.Epsilon)
		{
			return Internal_CapsuleCastNonAlloc(this, point1, point2, radius, direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}
		return 0;
	}

	[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
	[NativeName("OverlapCapsuleNonAlloc")]
	private static int OverlapCapsuleNonAlloc_Internal(PhysicsScene physicsScene, Vector3 point0, Vector3 point1, float radius, [Unmarshalled] Collider[] results, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
	{
		return OverlapCapsuleNonAlloc_Internal_Injected(ref physicsScene, ref point0, ref point1, radius, results, layerMask, queryTriggerInteraction);
	}

	public int OverlapCapsule(Vector3 point0, Vector3 point1, float radius, Collider[] results, [UnityEngine.Internal.DefaultValue("AllLayers")] int layerMask = -1, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
	{
		return OverlapCapsuleNonAlloc_Internal(this, point0, point1, radius, results, layerMask, queryTriggerInteraction);
	}

	[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()", StaticAccessorType.Dot)]
	[NativeName("SphereCast")]
	private static bool Query_SphereCast(PhysicsScene physicsScene, Vector3 origin, float radius, Vector3 direction, float maxDistance, ref RaycastHit hitInfo, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
	{
		return Query_SphereCast_Injected(ref physicsScene, ref origin, radius, ref direction, maxDistance, ref hitInfo, layerMask, queryTriggerInteraction);
	}

	private static bool Internal_SphereCast(PhysicsScene physicsScene, Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
	{
		float magnitude = direction.magnitude;
		hitInfo = default(RaycastHit);
		if (magnitude > float.Epsilon)
		{
			Vector3 direction2 = direction / magnitude;
			return Query_SphereCast(physicsScene, origin, radius, direction2, maxDistance, ref hitInfo, layerMask, queryTriggerInteraction);
		}
		return false;
	}

	public bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
	{
		return Internal_SphereCast(this, origin, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
	}

	[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
	[NativeName("SphereCastNonAlloc")]
	private static int Internal_SphereCastNonAlloc(PhysicsScene physicsScene, Vector3 origin, float radius, Vector3 direction, [Unmarshalled] RaycastHit[] raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
	{
		return Internal_SphereCastNonAlloc_Injected(ref physicsScene, ref origin, radius, ref direction, raycastHits, maxDistance, mask, queryTriggerInteraction);
	}

	public int SphereCast(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
	{
		float magnitude = direction.magnitude;
		if (magnitude > float.Epsilon)
		{
			return Internal_SphereCastNonAlloc(this, origin, radius, direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}
		return 0;
	}

	[NativeName("OverlapSphereNonAlloc")]
	[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
	private static int OverlapSphereNonAlloc_Internal(PhysicsScene physicsScene, Vector3 position, float radius, [Unmarshalled] Collider[] results, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
	{
		return OverlapSphereNonAlloc_Internal_Injected(ref physicsScene, ref position, radius, results, layerMask, queryTriggerInteraction);
	}

	public int OverlapSphere(Vector3 position, float radius, Collider[] results, [UnityEngine.Internal.DefaultValue("AllLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
	{
		return OverlapSphereNonAlloc_Internal(this, position, radius, results, layerMask, queryTriggerInteraction);
	}

	[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()", StaticAccessorType.Dot)]
	[NativeName("BoxCast")]
	private static bool Query_BoxCast(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance, ref RaycastHit outHit, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
	{
		return Query_BoxCast_Injected(ref physicsScene, ref center, ref halfExtents, ref direction, ref orientation, maxDistance, ref outHit, layerMask, queryTriggerInteraction);
	}

	private static bool Internal_BoxCast(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Quaternion orientation, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
	{
		float magnitude = direction.magnitude;
		hitInfo = default(RaycastHit);
		if (magnitude > float.Epsilon)
		{
			Vector3 direction2 = direction / magnitude;
			return Query_BoxCast(physicsScene, center, halfExtents, direction2, orientation, maxDistance, ref hitInfo, layerMask, queryTriggerInteraction);
		}
		return false;
	}

	public bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, [UnityEngine.Internal.DefaultValue("Quaternion.identity")] Quaternion orientation, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
	{
		return Internal_BoxCast(this, center, halfExtents, orientation, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo)
	{
		return Internal_BoxCast(this, center, halfExtents, Quaternion.identity, direction, out hitInfo, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
	}

	[NativeName("OverlapBoxNonAlloc")]
	[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
	private static int OverlapBoxNonAlloc_Internal(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, [Unmarshalled] Collider[] results, Quaternion orientation, int mask, QueryTriggerInteraction queryTriggerInteraction)
	{
		return OverlapBoxNonAlloc_Internal_Injected(ref physicsScene, ref center, ref halfExtents, results, ref orientation, mask, queryTriggerInteraction);
	}

	public int OverlapBox(Vector3 center, Vector3 halfExtents, Collider[] results, [UnityEngine.Internal.DefaultValue("Quaternion.identity")] Quaternion orientation, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
	{
		return OverlapBoxNonAlloc_Internal(this, center, halfExtents, results, orientation, layerMask, queryTriggerInteraction);
	}

	[ExcludeFromDocs]
	public int OverlapBox(Vector3 center, Vector3 halfExtents, Collider[] results)
	{
		return OverlapBoxNonAlloc_Internal(this, center, halfExtents, results, Quaternion.identity, -5, QueryTriggerInteraction.UseGlobal);
	}

	[NativeName("BoxCastNonAlloc")]
	[StaticAccessor("GetPhysicsManager().GetPhysicsQuery()")]
	private static int Internal_BoxCastNonAlloc(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Vector3 direction, [Unmarshalled] RaycastHit[] raycastHits, Quaternion orientation, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
	{
		return Internal_BoxCastNonAlloc_Injected(ref physicsScene, ref center, ref halfExtents, ref direction, raycastHits, ref orientation, maxDistance, mask, queryTriggerInteraction);
	}

	public int BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, [UnityEngine.Internal.DefaultValue("Quaternion.identity")] Quaternion orientation, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [UnityEngine.Internal.DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
	{
		float magnitude = direction.magnitude;
		if (magnitude > float.Epsilon)
		{
			return Internal_BoxCastNonAlloc(this, center, halfExtents, direction, results, orientation, maxDistance, layerMask, queryTriggerInteraction);
		}
		return 0;
	}

	[ExcludeFromDocs]
	public int BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results)
	{
		return BoxCast(center, halfExtents, direction, results, Quaternion.identity);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool IsValid_Internal_Injected(ref PhysicsScene physicsScene);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool IsEmpty_Internal_Injected(ref PhysicsScene physicsScene);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool Internal_RaycastTest_Injected(ref PhysicsScene physicsScene, ref Ray ray, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool Internal_Raycast_Injected(ref PhysicsScene physicsScene, ref Ray ray, float maxDistance, ref RaycastHit hit, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int Internal_RaycastNonAlloc_Injected(ref PhysicsScene physicsScene, ref Ray ray, RaycastHit[] raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool Query_CapsuleCast_Injected(ref PhysicsScene physicsScene, ref Vector3 point1, ref Vector3 point2, float radius, ref Vector3 direction, float maxDistance, ref RaycastHit hitInfo, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int Internal_CapsuleCastNonAlloc_Injected(ref PhysicsScene physicsScene, ref Vector3 p0, ref Vector3 p1, float radius, ref Vector3 direction, RaycastHit[] raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int OverlapCapsuleNonAlloc_Internal_Injected(ref PhysicsScene physicsScene, ref Vector3 point0, ref Vector3 point1, float radius, Collider[] results, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool Query_SphereCast_Injected(ref PhysicsScene physicsScene, ref Vector3 origin, float radius, ref Vector3 direction, float maxDistance, ref RaycastHit hitInfo, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int Internal_SphereCastNonAlloc_Injected(ref PhysicsScene physicsScene, ref Vector3 origin, float radius, ref Vector3 direction, RaycastHit[] raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int OverlapSphereNonAlloc_Internal_Injected(ref PhysicsScene physicsScene, ref Vector3 position, float radius, Collider[] results, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool Query_BoxCast_Injected(ref PhysicsScene physicsScene, ref Vector3 center, ref Vector3 halfExtents, ref Vector3 direction, ref Quaternion orientation, float maxDistance, ref RaycastHit outHit, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int OverlapBoxNonAlloc_Internal_Injected(ref PhysicsScene physicsScene, ref Vector3 center, ref Vector3 halfExtents, Collider[] results, ref Quaternion orientation, int mask, QueryTriggerInteraction queryTriggerInteraction);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int Internal_BoxCastNonAlloc_Injected(ref PhysicsScene physicsScene, ref Vector3 center, ref Vector3 halfExtents, ref Vector3 direction, RaycastHit[] raycastHits, ref Quaternion orientation, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction);
}
public static class PhysicsSceneExtensions
{
	public static PhysicsScene GetPhysicsScene(this Scene scene)
	{
		if (!scene.IsValid())
		{
			throw new ArgumentException("Cannot get physics scene; Unity scene is invalid.", "scene");
		}
		PhysicsScene physicsScene_Internal = GetPhysicsScene_Internal(scene);
		if (physicsScene_Internal.IsValid())
		{
			return physicsScene_Internal;
		}
		throw new Exception("The physics scene associated with the Unity scene is invalid.");
	}

	[NativeMethod("GetPhysicsSceneFromUnityScene")]
	[StaticAccessor("GetPhysicsManager()", StaticAccessorType.Dot)]
	private static PhysicsScene GetPhysicsScene_Internal(Scene scene)
	{
		GetPhysicsScene_Internal_Injected(ref scene, out var ret);
		return ret;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void GetPhysicsScene_Internal_Injected(ref Scene scene, out PhysicsScene ret);
}
public enum SimulationMode
{
	FixedUpdate,
	Update,
	Script
}
[Flags]
[Obsolete("JointDriveMode is no longer supported")]
public enum JointDriveMode
{
	[Obsolete("JointDriveMode.None is no longer supported")]
	None = 0,
	[Obsolete("JointDriveMode.Position is no longer supported")]
	Position = 1,
	[Obsolete("JointDriveMode.Velocity is no longer supported")]
	Velocity = 2,
	[Obsolete("JointDriveMode.PositionAndvelocity is no longer supported")]
	PositionAndVelocity = 3
}
public readonly struct ContactPairHeader
{
	internal readonly int m_BodyID;

	internal readonly int m_OtherBodyID;

	internal readonly IntPtr m_StartPtr;

	internal readonly uint m_NbPairs;

	internal readonly CollisionPairHeaderFlags m_Flags;

	internal readonly Vector3 m_RelativeVelocity;

	public int BodyInstanceID => m_BodyID;

	public int OtherBodyInstanceID => m_OtherBodyID;

	public Component Body => Physics.GetBodyByInstanceID(m_BodyID);

	public Component OtherBody => Physics.GetBodyByInstanceID(m_OtherBodyID);

	public int PairCount => (int)m_NbPairs;

	internal bool HasRemovedBody => (m_Flags & CollisionPairHeaderFlags.RemovedActor) != 0 || (m_Flags & CollisionPairHeaderFlags.RemovedOtherActor) != 0;

	public unsafe ref readonly ContactPair GetContactPair(int index)
	{
		return ref *GetContactPair_Internal(index);
	}

	internal unsafe ContactPair* GetContactPair_Internal(int index)
	{
		if (index >= m_NbPairs)
		{
			throw new IndexOutOfRangeException("Invalid ContactPair index. Index should be greater than 0 and less than ContactPairHeader.PairCount");
		}
		return (ContactPair*)(m_StartPtr.ToInt64() + index * sizeof(ContactPair));
	}
}
[UsedByNativeCode]
public readonly struct ContactPair
{
	private const uint c_InvalidFaceIndex = uint.MaxValue;

	internal readonly int m_ColliderID;

	internal readonly int m_OtherColliderID;

	internal readonly IntPtr m_StartPtr;

	internal readonly uint m_NbPoints;

	internal readonly CollisionPairFlags m_Flags;

	internal readonly CollisionPairEventFlags m_Events;

	internal readonly Vector3 m_ImpulseSum;

	public int ColliderInstanceID => m_ColliderID;

	public int OtherColliderInstanceID => m_OtherColliderID;

	public Collider Collider => (m_ColliderID == 0) ? null : Physics.GetColliderByInstanceID(m_ColliderID);

	public Collider OtherCollider => (m_OtherColliderID == 0) ? null : Physics.GetColliderByInstanceID(m_OtherColliderID);

	public int ContactCount => (int)m_NbPoints;

	public Vector3 ImpulseSum => m_ImpulseSum;

	public bool IsCollisionEnter => (m_Events & CollisionPairEventFlags.NotifyTouchFound) != 0;

	public bool IsCollisionExit => (m_Events & CollisionPairEventFlags.NotifyTouchLost) != 0;

	public bool IsCollisionStay => (m_Events & CollisionPairEventFlags.NotifyTouchPersists) != 0;

	internal bool HasRemovedCollider => (m_Flags & CollisionPairFlags.RemovedShape) != 0 || (m_Flags & CollisionPairFlags.RemovedOtherShape) != 0;

	internal int ExtractContacts(List<ContactPoint> managedContainer, bool flipped)
	{
		return ExtractContacts_Injected(ref this, managedContainer, flipped);
	}

	internal int ExtractContactsArray([Unmarshalled] ContactPoint[] managedContainer, bool flipped)
	{
		return ExtractContactsArray_Injected(ref this, managedContainer, flipped);
	}

	public void CopyToNativeArray(NativeArray<ContactPairPoint> buffer)
	{
		int num = Mathf.Min(buffer.Length, ContactCount);
		for (int i = 0; i < num; i++)
		{
			buffer[i] = GetContactPoint(i);
		}
	}

	public unsafe ref readonly ContactPairPoint GetContactPoint(int index)
	{
		return ref *GetContactPoint_Internal(index);
	}

	public unsafe uint GetContactPointFaceIndex(int contactIndex)
	{
		uint internalFaceIndex = GetContactPoint_Internal(contactIndex)->m_InternalFaceIndex0;
		uint internalFaceIndex2 = GetContactPoint_Internal(contactIndex)->m_InternalFaceIndex1;
		if (internalFaceIndex != uint.MaxValue)
		{
			return Physics.TranslateTriangleIndexFromID(m_ColliderID, internalFaceIndex);
		}
		if (internalFaceIndex2 != uint.MaxValue)
		{
			return Physics.TranslateTriangleIndexFromID(m_OtherColliderID, internalFaceIndex2);
		}
		return uint.MaxValue;
	}

	internal unsafe ContactPairPoint* GetContactPoint_Internal(int index)
	{
		if (index >= m_NbPoints)
		{
			throw new IndexOutOfRangeException("Invalid ContactPairPoint index. Index should be greater than 0 and less than ContactPair.ContactCount");
		}
		return (ContactPairPoint*)(m_StartPtr.ToInt64() + index * sizeof(ContactPairPoint));
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int ExtractContacts_Injected(ref ContactPair _unity_self, List<ContactPoint> managedContainer, bool flipped);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int ExtractContactsArray_Injected(ref ContactPair _unity_self, ContactPoint[] managedContainer, bool flipped);
}
public readonly struct ContactPairPoint
{
	internal readonly Vector3 m_Position;

	internal readonly float m_Separation;

	internal readonly Vector3 m_Normal;

	internal readonly uint m_InternalFaceIndex0;

	internal readonly Vector3 m_Impulse;

	internal readonly uint m_InternalFaceIndex1;

	public Vector3 Position => m_Position;

	public float Separation => m_Separation;

	public Vector3 Normal => m_Normal;

	public Vector3 Impulse => m_Impulse;
}
internal enum CollisionPairHeaderFlags : ushort
{
	RemovedActor = 1,
	RemovedOtherActor
}
internal enum CollisionPairFlags : ushort
{
	RemovedShape = 1,
	RemovedOtherShape = 2,
	ActorPairHasFirstTouch = 4,
	ActorPairLostTouch = 8,
	InternalHasImpulses = 0x10,
	InternalContactsAreFlipped = 0x20
}
internal enum CollisionPairEventFlags : ushort
{
	SolveContacts = 1,
	ModifyContacts = 2,
	NotifyTouchFound = 4,
	NotifyTouchPersists = 8,
	NotifyTouchLost = 16,
	NotifyTouchCCD = 32,
	NotifyThresholdForceFound = 64,
	NotifyThresholdForcePersists = 128,
	NotifyThresholdForceLost = 256,
	NotifyContactPoint = 512,
	DetectDiscreteContact = 1024,
	DetectCCDContact = 2048,
	PreSolverVelocity = 4096,
	PostSolverVelocity = 8192,
	ContactEventPose = 16384,
	NextFree = 32768,
	ContactDefault = 1025,
	TriggerDefault = 1044
}
public struct QueryParameters
{
	public int layerMask;

	public bool hitMultipleFaces;

	public QueryTriggerInteraction hitTriggers;

	public bool hitBackfaces;

	public static QueryParameters Default => new QueryParameters(-5, hitMultipleFaces: false, QueryTriggerInteraction.UseGlobal, hitBackfaces: false);

	public QueryParameters(int layerMask = -5, bool hitMultipleFaces = false, QueryTriggerInteraction hitTriggers = QueryTriggerInteraction.UseGlobal, bool hitBackfaces = false)
	{
		this.layerMask = layerMask;
		this.hitMultipleFaces = hitMultipleFaces;
		this.hitTriggers = hitTriggers;
		this.hitBackfaces = hitBackfaces;
	}
}
public struct ColliderHit
{
	private int m_ColliderInstanceID;

	public int instanceID => m_ColliderInstanceID;

	public Collider collider => Object.FindObjectFromInstanceID(instanceID) as Collider;
}
[NativeHeader("Modules/Physics/BatchCommands/RaycastCommand.h")]
[NativeHeader("Runtime/Jobs/ScriptBindings/JobsBindingsTypes.h")]
public struct RaycastCommand
{
	public QueryParameters queryParameters;

	public Vector3 from { get; set; }

	public Vector3 direction { get; set; }

	public PhysicsScene physicsScene { get; set; }

	public float distance { get; set; }

	[Obsolete("maxHits property was moved to be a part of RaycastCommand.ScheduleBatch.", false)]
	public int maxHits
	{
		get
		{
			return 1;
		}
		set
		{
		}
	}

	[Obsolete("Layer Mask is now a part of QueryParameters struct", false)]
	public int layerMask
	{
		get
		{
			return queryParameters.layerMask;
		}
		set
		{
			queryParameters.layerMask = value;
		}
	}

	public RaycastCommand(Vector3 from, Vector3 direction, QueryParameters queryParameters, float distance = float.MaxValue)
	{
		this.from = from;
		this.direction = direction;
		physicsScene = Physics.defaultPhysicsScene;
		this.distance = distance;
		this.queryParameters = queryParameters;
	}

	public RaycastCommand(PhysicsScene physicsScene, Vector3 from, Vector3 direction, QueryParameters queryParameters, float distance = float.MaxValue)
	{
		this.from = from;
		this.direction = direction;
		this.physicsScene = physicsScene;
		this.distance = distance;
		this.queryParameters = queryParameters;
	}

	public unsafe static JobHandle ScheduleBatch(NativeArray<RaycastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob, int maxHits, JobHandle dependsOn = default(JobHandle))
	{
		if (maxHits < 1)
		{
			Debug.LogWarning("maxHits should be greater than 0.");
			return default(JobHandle);
		}
		if (results.Length < maxHits * commands.Length)
		{
			Debug.LogWarning("The supplied results buffer is too small, there should be at least maxHits space per each command in the batch.");
			return default(JobHandle);
		}
		BatchQueryJob<RaycastCommand, RaycastHit> output = new BatchQueryJob<RaycastCommand, RaycastHit>(commands, results);
		JobsUtility.JobScheduleParameters parameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf(ref output), BatchQueryJobStruct<BatchQueryJob<RaycastCommand, RaycastHit>>.Initialize(), dependsOn, ScheduleMode.Batched);
		return ScheduleRaycastBatch(ref parameters, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(commands), commands.Length, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(results), results.Length, minCommandsPerJob, maxHits);
	}

	public static JobHandle ScheduleBatch(NativeArray<RaycastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob, JobHandle dependsOn = default(JobHandle))
	{
		return ScheduleBatch(commands, results, minCommandsPerJob, 1, dependsOn);
	}

	[FreeFunction("ScheduleRaycastCommandBatch", ThrowsException = true)]
	private unsafe static JobHandle ScheduleRaycastBatch(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, int maxHits)
	{
		ScheduleRaycastBatch_Injected(ref parameters, commands, commandLen, result, resultLen, minCommandsPerJob, maxHits, out var ret);
		return ret;
	}

	[Obsolete("This struct signature is no longer supported. Use struct with a QueryParameters instead", false)]
	public RaycastCommand(Vector3 from, Vector3 direction, float distance = float.MaxValue, int layerMask = -5, int maxHits = 1)
	{
		this.from = from;
		this.direction = direction;
		physicsScene = Physics.defaultPhysicsScene;
		queryParameters = QueryParameters.Default;
		this.distance = distance;
		this.layerMask = layerMask;
	}

	[Obsolete("This struct signature is no longer supported. Use struct with a QueryParameters instead", false)]
	public RaycastCommand(PhysicsScene physicsScene, Vector3 from, Vector3 direction, float distance = float.MaxValue, int layerMask = -5, int maxHits = 1)
	{
		this.from = from;
		this.direction = direction;
		this.physicsScene = physicsScene;
		queryParameters = QueryParameters.Default;
		this.distance = distance;
		this.layerMask = layerMask;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private unsafe static extern void ScheduleRaycastBatch_Injected(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, int maxHits, out JobHandle ret);
}
[NativeHeader("Runtime/Jobs/ScriptBindings/JobsBindingsTypes.h")]
[NativeHeader("Modules/Physics/BatchCommands/SpherecastCommand.h")]
public struct SpherecastCommand
{
	public QueryParameters queryParameters;

	public Vector3 origin { get; set; }

	public float radius { get; set; }

	public Vector3 direction { get; set; }

	public float distance { get; set; }

	public PhysicsScene physicsScene { get; set; }

	[Obsolete("Layer Mask is now a part of QueryParameters struct", false)]
	public int layerMask
	{
		get
		{
			return queryParameters.layerMask;
		}
		set
		{
			queryParameters.layerMask = value;
		}
	}

	public SpherecastCommand(Vector3 origin, float radius, Vector3 direction, QueryParameters queryParameters, float distance = float.MaxValue)
	{
		this.origin = origin;
		this.direction = direction;
		this.radius = radius;
		this.distance = distance;
		physicsScene = Physics.defaultPhysicsScene;
		this.queryParameters = queryParameters;
	}

	public SpherecastCommand(PhysicsScene physicsScene, Vector3 origin, float radius, Vector3 direction, QueryParameters queryParameters, float distance = float.MaxValue)
	{
		this.origin = origin;
		this.direction = direction;
		this.radius = radius;
		this.distance = distance;
		this.physicsScene = physicsScene;
		this.queryParameters = queryParameters;
	}

	public unsafe static JobHandle ScheduleBatch(NativeArray<SpherecastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob, int maxHits, JobHandle dependsOn = default(JobHandle))
	{
		if (maxHits < 1)
		{
			Debug.LogWarning("maxHits should be greater than 0.");
			return default(JobHandle);
		}
		if (results.Length < maxHits * commands.Length)
		{
			Debug.LogWarning("The supplied results buffer is too small, there should be at least maxHits space per each command in the batch.");
			return default(JobHandle);
		}
		BatchQueryJob<SpherecastCommand, RaycastHit> output = new BatchQueryJob<SpherecastCommand, RaycastHit>(commands, results);
		JobsUtility.JobScheduleParameters parameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf(ref output), BatchQueryJobStruct<BatchQueryJob<SpherecastCommand, RaycastHit>>.Initialize(), dependsOn, ScheduleMode.Batched);
		return ScheduleSpherecastBatch(ref parameters, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(commands), commands.Length, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(results), results.Length, minCommandsPerJob, maxHits);
	}

	public static JobHandle ScheduleBatch(NativeArray<SpherecastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob, JobHandle dependsOn = default(JobHandle))
	{
		return ScheduleBatch(commands, results, minCommandsPerJob, 1, dependsOn);
	}

	[FreeFunction("ScheduleSpherecastCommandBatch", ThrowsException = true)]
	private unsafe static JobHandle ScheduleSpherecastBatch(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, int maxHits)
	{
		ScheduleSpherecastBatch_Injected(ref parameters, commands, commandLen, result, resultLen, minCommandsPerJob, maxHits, out var ret);
		return ret;
	}

	[Obsolete("This struct signature is no longer supported. Use struct with a QueryParameters instead", false)]
	public SpherecastCommand(Vector3 origin, float radius, Vector3 direction, float distance = float.MaxValue, int layerMask = -5)
	{
		this.origin = origin;
		this.direction = direction;
		this.radius = radius;
		this.distance = distance;
		physicsScene = Physics.defaultPhysicsScene;
		queryParameters = QueryParameters.Default;
		this.layerMask = layerMask;
	}

	[Obsolete("This struct signature is no longer supported. Use struct with a QueryParameters instead", false)]
	public SpherecastCommand(PhysicsScene physicsScene, Vector3 origin, float radius, Vector3 direction, float distance = float.MaxValue, int layerMask = -5)
	{
		this.origin = origin;
		this.direction = direction;
		this.radius = radius;
		this.distance = distance;
		this.physicsScene = physicsScene;
		queryParameters = QueryParameters.Default;
		this.layerMask = layerMask;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private unsafe static extern void ScheduleSpherecastBatch_Injected(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, int maxHits, out JobHandle ret);
}
[NativeHeader("Modules/Physics/BatchCommands/CapsulecastCommand.h")]
[NativeHeader("Runtime/Jobs/ScriptBindings/JobsBindingsTypes.h")]
public struct CapsulecastCommand
{
	public QueryParameters queryParameters;

	public Vector3 point1 { get; set; }

	public Vector3 point2 { get; set; }

	public float radius { get; set; }

	public Vector3 direction { get; set; }

	public float distance { get; set; }

	public PhysicsScene physicsScene { get; set; }

	[Obsolete("Layer Mask is now a part of QueryParameters struct", false)]
	public int layerMask
	{
		get
		{
			return queryParameters.layerMask;
		}
		set
		{
			queryParameters.layerMask = value;
		}
	}

	public CapsulecastCommand(Vector3 p1, Vector3 p2, float radius, Vector3 direction, QueryParameters queryParameters, float distance = float.MaxValue)
	{
		point1 = p1;
		point2 = p2;
		this.direction = direction;
		this.radius = radius;
		this.distance = distance;
		physicsScene = Physics.defaultPhysicsScene;
		this.queryParameters = queryParameters;
	}

	public CapsulecastCommand(PhysicsScene physicsScene, Vector3 p1, Vector3 p2, float radius, Vector3 direction, QueryParameters queryParameters, float distance = float.MaxValue)
	{
		point1 = p1;
		point2 = p2;
		this.direction = direction;
		this.radius = radius;
		this.distance = distance;
		this.physicsScene = physicsScene;
		this.queryParameters = queryParameters;
	}

	public unsafe static JobHandle ScheduleBatch(NativeArray<CapsulecastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob, int maxHits, JobHandle dependsOn = default(JobHandle))
	{
		if (maxHits < 1)
		{
			Debug.LogWarning("maxHits should be greater than 0.");
			return default(JobHandle);
		}
		if (results.Length < maxHits * commands.Length)
		{
			Debug.LogWarning("The supplied results buffer is too small, there should be at least maxHits space per each command in the batch.");
			return default(JobHandle);
		}
		BatchQueryJob<CapsulecastCommand, RaycastHit> output = new BatchQueryJob<CapsulecastCommand, RaycastHit>(commands, results);
		JobsUtility.JobScheduleParameters parameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf(ref output), BatchQueryJobStruct<BatchQueryJob<CapsulecastCommand, RaycastHit>>.Initialize(), dependsOn, ScheduleMode.Batched);
		return ScheduleCapsulecastBatch(ref parameters, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(commands), commands.Length, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(results), results.Length, minCommandsPerJob, maxHits);
	}

	public static JobHandle ScheduleBatch(NativeArray<CapsulecastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob, JobHandle dependsOn = default(JobHandle))
	{
		return ScheduleBatch(commands, results, minCommandsPerJob, 1, dependsOn);
	}

	[FreeFunction("ScheduleCapsulecastCommandBatch", ThrowsException = true)]
	private unsafe static JobHandle ScheduleCapsulecastBatch(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, int maxHits)
	{
		ScheduleCapsulecastBatch_Injected(ref parameters, commands, commandLen, result, resultLen, minCommandsPerJob, maxHits, out var ret);
		return ret;
	}

	[Obsolete("This struct signature is no longer supported. Use struct with a QueryParameters instead", false)]
	public CapsulecastCommand(Vector3 p1, Vector3 p2, float radius, Vector3 direction, float distance = float.MaxValue, int layerMask = -5)
	{
		point1 = p1;
		point2 = p2;
		this.direction = direction;
		this.radius = radius;
		this.distance = distance;
		physicsScene = Physics.defaultPhysicsScene;
		queryParameters = QueryParameters.Default;
		this.layerMask = layerMask;
	}

	[Obsolete("This struct signature is no longer supported. Use struct with a QueryParameters instead", false)]
	public CapsulecastCommand(PhysicsScene physicsScene, Vector3 p1, Vector3 p2, float radius, Vector3 direction, float distance = float.MaxValue, int layerMask = -5)
	{
		point1 = p1;
		point2 = p2;
		this.direction = direction;
		this.radius = radius;
		this.distance = distance;
		this.physicsScene = physicsScene;
		queryParameters = QueryParameters.Default;
		this.layerMask = layerMask;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private unsafe static extern void ScheduleCapsulecastBatch_Injected(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, int maxHits, out JobHandle ret);
}
[NativeHeader("Runtime/Jobs/ScriptBindings/JobsBindingsTypes.h")]
[NativeHeader("Modules/Physics/BatchCommands/BoxcastCommand.h")]
public struct BoxcastCommand
{
	public QueryParameters queryParameters;

	public Vector3 center { get; set; }

	public Vector3 halfExtents { get; set; }

	public Quaternion orientation { get; set; }

	public Vector3 direction { get; set; }

	public float distance { get; set; }

	public PhysicsScene physicsScene { get; set; }

	[Obsolete("Layer Mask is now a part of QueryParameters struct", false)]
	public int layerMask
	{
		get
		{
			return queryParameters.layerMask;
		}
		set
		{
			queryParameters.layerMask = value;
		}
	}

	public BoxcastCommand(Vector3 center, Vector3 halfExtents, Quaternion orientation, Vector3 direction, QueryParameters queryParameters, float distance = float.MaxValue)
	{
		this.center = center;
		this.halfExtents = halfExtents;
		this.orientation = orientation;
		this.direction = direction;
		this.distance = distance;
		physicsScene = Physics.defaultPhysicsScene;
		this.queryParameters = queryParameters;
	}

	public BoxcastCommand(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Quaternion orientation, Vector3 direction, QueryParameters queryParameters, float distance = float.MaxValue)
	{
		this.center = center;
		this.halfExtents = halfExtents;
		this.orientation = orientation;
		this.direction = direction;
		this.distance = distance;
		this.physicsScene = physicsScene;
		this.queryParameters = queryParameters;
	}

	public unsafe static JobHandle ScheduleBatch(NativeArray<BoxcastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob, int maxHits, JobHandle dependsOn = default(JobHandle))
	{
		if (maxHits < 1)
		{
			Debug.LogWarning("maxHits should be greater than 0.");
			return default(JobHandle);
		}
		if (results.Length < maxHits * commands.Length)
		{
			Debug.LogWarning("The supplied results buffer is too small, there should be at least maxHits space per each command in the batch.");
			return default(JobHandle);
		}
		BatchQueryJob<BoxcastCommand, RaycastHit> output = new BatchQueryJob<BoxcastCommand, RaycastHit>(commands, results);
		JobsUtility.JobScheduleParameters parameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf(ref output), BatchQueryJobStruct<BatchQueryJob<BoxcastCommand, RaycastHit>>.Initialize(), dependsOn, ScheduleMode.Batched);
		return ScheduleBoxcastBatch(ref parameters, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(commands), commands.Length, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(results), results.Length, minCommandsPerJob, maxHits);
	}

	public static JobHandle ScheduleBatch(NativeArray<BoxcastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob, JobHandle dependsOn = default(JobHandle))
	{
		return ScheduleBatch(commands, results, minCommandsPerJob, 1, dependsOn);
	}

	[FreeFunction("ScheduleBoxcastCommandBatch", ThrowsException = true)]
	private unsafe static JobHandle ScheduleBoxcastBatch(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, int maxHits)
	{
		ScheduleBoxcastBatch_Injected(ref parameters, commands, commandLen, result, resultLen, minCommandsPerJob, maxHits, out var ret);
		return ret;
	}

	[Obsolete("This struct signature is no longer supported. Use struct with a QueryParameters instead", false)]
	public BoxcastCommand(Vector3 center, Vector3 halfExtents, Quaternion orientation, Vector3 direction, float distance = float.MaxValue, int layerMask = -5)
	{
		this.center = center;
		this.halfExtents = halfExtents;
		this.orientation = orientation;
		this.direction = direction;
		this.distance = distance;
		physicsScene = Physics.defaultPhysicsScene;
		queryParameters = QueryParameters.Default;
		this.layerMask = layerMask;
	}

	[Obsolete("This struct signature is no longer supported. Use struct with a QueryParameters instead", false)]
	public BoxcastCommand(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Quaternion orientation, Vector3 direction, float distance = float.MaxValue, int layerMask = -5)
	{
		this.center = center;
		this.halfExtents = halfExtents;
		this.orientation = orientation;
		this.direction = direction;
		this.distance = distance;
		this.physicsScene = physicsScene;
		queryParameters = QueryParameters.Default;
		this.layerMask = layerMask;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private unsafe static extern void ScheduleBoxcastBatch_Injected(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, int maxHits, out JobHandle ret);
}
[NativeHeader("Runtime/Jobs/ScriptBindings/JobsBindingsTypes.h")]
[NativeHeader("Modules/Physics/BatchCommands/ClosestPointCommand.h")]
public struct ClosestPointCommand
{
	public Vector3 point { get; set; }

	public int colliderInstanceID { get; set; }

	public Vector3 position { get; set; }

	public Quaternion rotation { get; set; }

	public Vector3 scale { get; set; }

	public ClosestPointCommand(Vector3 point, int colliderInstanceID, Vector3 position, Quaternion rotation, Vector3 scale)
	{
		this.point = point;
		this.colliderInstanceID = colliderInstanceID;
		this.position = position;
		this.rotation = rotation;
		this.scale = scale;
	}

	public ClosestPointCommand(Vector3 point, Collider collider, Vector3 position, Quaternion rotation, Vector3 scale)
	{
		this.point = point;
		colliderInstanceID = collider.GetInstanceID();
		this.position = position;
		this.rotation = rotation;
		this.scale = scale;
	}

	public unsafe static JobHandle ScheduleBatch(NativeArray<ClosestPointCommand> commands, NativeArray<Vector3> results, int minCommandsPerJob, JobHandle dependsOn = default(JobHandle))
	{
		BatchQueryJob<ClosestPointCommand, Vector3> output = new BatchQueryJob<ClosestPointCommand, Vector3>(commands, results);
		JobsUtility.JobScheduleParameters parameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf(ref output), BatchQueryJobStruct<BatchQueryJob<ClosestPointCommand, Vector3>>.Initialize(), dependsOn, ScheduleMode.Batched);
		return ScheduleClosestPointCommandBatch(ref parameters, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(commands), commands.Length, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(results), results.Length, minCommandsPerJob);
	}

	[FreeFunction("ScheduleClosestPointCommandBatch", ThrowsException = true)]
	private unsafe static JobHandle ScheduleClosestPointCommandBatch(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob)
	{
		ScheduleClosestPointCommandBatch_Injected(ref parameters, commands, commandLen, result, resultLen, minCommandsPerJob, out var ret);
		return ret;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private unsafe static extern void ScheduleClosestPointCommandBatch_Injected(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, out JobHandle ret);
}
[NativeHeader("Modules/Physics/BatchCommands/OverlapSphereCommand.h")]
public struct OverlapSphereCommand
{
	public QueryParameters queryParameters;

	public Vector3 point { get; set; }

	public float radius { get; set; }

	public PhysicsScene physicsScene { get; set; }

	public OverlapSphereCommand(Vector3 point, float radius, QueryParameters queryParameters)
	{
		this.point = point;
		this.radius = radius;
		this.queryParameters = queryParameters;
		physicsScene = Physics.defaultPhysicsScene;
	}

	public OverlapSphereCommand(PhysicsScene physicsScene, Vector3 point, float radius, QueryParameters queryParameters)
	{
		this.physicsScene = physicsScene;
		this.point = point;
		this.radius = radius;
		this.queryParameters = queryParameters;
	}

	public unsafe static JobHandle ScheduleBatch(NativeArray<OverlapSphereCommand> commands, NativeArray<ColliderHit> results, int minCommandsPerJob, int maxHits, JobHandle dependsOn = default(JobHandle))
	{
		if (maxHits < 1)
		{
			Debug.LogWarning("maxHits should be greater than 0.");
			return default(JobHandle);
		}
		if (results.Length < maxHits * commands.Length)
		{
			Debug.LogWarning("The supplied results buffer is too small, there should be at least maxHits space per each command in the batch.");
			return default(JobHandle);
		}
		BatchQueryJob<OverlapSphereCommand, ColliderHit> output = new BatchQueryJob<OverlapSphereCommand, ColliderHit>(commands, results);
		JobsUtility.JobScheduleParameters parameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf(ref output), BatchQueryJobStruct<BatchQueryJob<OverlapSphereCommand, ColliderHit>>.Initialize(), dependsOn, ScheduleMode.Batched);
		return ScheduleOverlapSphereBatch(ref parameters, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(commands), commands.Length, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(results), results.Length, minCommandsPerJob, maxHits);
	}

	[FreeFunction("ScheduleOverlapSphereCommandBatch", ThrowsException = true)]
	private unsafe static JobHandle ScheduleOverlapSphereBatch(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, int maxHits)
	{
		ScheduleOverlapSphereBatch_Injected(ref parameters, commands, commandLen, result, resultLen, minCommandsPerJob, maxHits, out var ret);
		return ret;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private unsafe static extern void ScheduleOverlapSphereBatch_Injected(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, int maxHits, out JobHandle ret);
}
[NativeHeader("Modules/Physics/BatchCommands/OverlapBoxCommand.h")]
public struct OverlapBoxCommand
{
	public QueryParameters queryParameters;

	public Vector3 center { get; set; }

	public Vector3 halfExtents { get; set; }

	public Quaternion orientation { get; set; }

	public PhysicsScene physicsScene { get; set; }

	public OverlapBoxCommand(Vector3 center, Vector3 halfExtents, Quaternion orientation, QueryParameters queryParameters)
	{
		this.center = center;
		this.halfExtents = halfExtents;
		this.orientation = orientation;
		this.queryParameters = queryParameters;
		physicsScene = Physics.defaultPhysicsScene;
	}

	public OverlapBoxCommand(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Quaternion orientation, QueryParameters queryParameters)
	{
		this.physicsScene = physicsScene;
		this.center = center;
		this.halfExtents = halfExtents;
		this.orientation = orientation;
		this.queryParameters = queryParameters;
	}

	public unsafe static JobHandle ScheduleBatch(NativeArray<OverlapBoxCommand> commands, NativeArray<ColliderHit> results, int minCommandsPerJob, int maxHits, JobHandle dependsOn = default(JobHandle))
	{
		if (maxHits < 1)
		{
			Debug.LogWarning("maxHits should be greater than 0.");
			return default(JobHandle);
		}
		if (results.Length < maxHits * commands.Length)
		{
			Debug.LogWarning("The supplied results buffer is too small, there should be at least maxHits space per each command in the batch.");
			return default(JobHandle);
		}
		BatchQueryJob<OverlapBoxCommand, ColliderHit> output = new BatchQueryJob<OverlapBoxCommand, ColliderHit>(commands, results);
		JobsUtility.JobScheduleParameters parameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf(ref output), BatchQueryJobStruct<BatchQueryJob<OverlapBoxCommand, ColliderHit>>.Initialize(), dependsOn, ScheduleMode.Batched);
		return ScheduleOverlapBoxBatch(ref parameters, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(commands), commands.Length, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(results), results.Length, minCommandsPerJob, maxHits);
	}

	[FreeFunction("ScheduleOverlapBoxCommandBatch", ThrowsException = true)]
	private unsafe static JobHandle ScheduleOverlapBoxBatch(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, int maxHits)
	{
		ScheduleOverlapBoxBatch_Injected(ref parameters, commands, commandLen, result, resultLen, minCommandsPerJob, maxHits, out var ret);
		return ret;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private unsafe static extern void ScheduleOverlapBoxBatch_Injected(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, int maxHits, out JobHandle ret);
}
[NativeHeader("Modules/Physics/BatchCommands/OverlapCapsuleCommand.h")]
public struct OverlapCapsuleCommand
{
	public QueryParameters queryParameters;

	public Vector3 point0 { get; set; }

	public Vector3 point1 { get; set; }

	public float radius { get; set; }

	public PhysicsScene physicsScene { get; set; }

	public OverlapCapsuleCommand(Vector3 point0, Vector3 point1, float radius, QueryParameters queryParameters)
	{
		this.point0 = point0;
		this.point1 = point1;
		this.radius = radius;
		this.queryParameters = queryParameters;
		physicsScene = Physics.defaultPhysicsScene;
	}

	public OverlapCapsuleCommand(PhysicsScene physicsScene, Vector3 point0, Vector3 point1, float radius, QueryParameters queryParameters)
	{
		this.physicsScene = physicsScene;
		this.point0 = point0;
		this.point1 = point1;
		this.radius = radius;
		this.queryParameters = queryParameters;
	}

	public unsafe static JobHandle ScheduleBatch(NativeArray<OverlapCapsuleCommand> commands, NativeArray<ColliderHit> results, int minCommandsPerJob, int maxHits, JobHandle dependsOn = default(JobHandle))
	{
		if (maxHits < 1)
		{
			Debug.LogWarning("maxHits should be greater than 0.");
			return default(JobHandle);
		}
		if (results.Length < maxHits * commands.Length)
		{
			Debug.LogWarning("The supplied results buffer is too small, there should be at least maxHits space per each command in the batch.");
			return default(JobHandle);
		}
		BatchQueryJob<OverlapCapsuleCommand, ColliderHit> output = new BatchQueryJob<OverlapCapsuleCommand, ColliderHit>(commands, results);
		JobsUtility.JobScheduleParameters parameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf(ref output), BatchQueryJobStruct<BatchQueryJob<OverlapCapsuleCommand, ColliderHit>>.Initialize(), dependsOn, ScheduleMode.Batched);
		return ScheduleOverlapCapsuleBatch(ref parameters, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(commands), commands.Length, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks(results), results.Length, minCommandsPerJob, maxHits);
	}

	[FreeFunction("ScheduleOverlapCapsuleCommandBatch", ThrowsException = true)]
	private unsafe static JobHandle ScheduleOverlapCapsuleBatch(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, int maxHits)
	{
		ScheduleOverlapCapsuleBatch_Injected(ref parameters, commands, commandLen, result, resultLen, minCommandsPerJob, maxHits, out var ret);
		return ret;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private unsafe static extern void ScheduleOverlapCapsuleBatch_Injected(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, int maxHits, out JobHandle ret);
}
