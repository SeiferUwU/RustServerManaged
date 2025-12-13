using System;
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
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

[assembly: InternalsVisibleTo("UnityEngine.TilemapModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainPhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubsystemsModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubstanceModule")]
[assembly: InternalsVisibleTo("UnityEngine.StreamingModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteShapeModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteMaskModule")]
[assembly: InternalsVisibleTo("UnityEngine.RuntimeInitializeOnLoadManagerInitializerModule")]
[assembly: InternalsVisibleTo("UnityEngine.PropertiesModule")]
[assembly: InternalsVisibleTo("UnityEngine.ProfilerModule")]
[assembly: InternalsVisibleTo("UnityEngine.Physics2DModule")]
[assembly: InternalsVisibleTo("UnityEngine.PerformanceReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.ParticleSystemModule")]
[assembly: InternalsVisibleTo("UnityEngine.NVIDIAModule")]
[assembly: InternalsVisibleTo("UnityEngine.ScreenCaptureModule")]
[assembly: InternalsVisibleTo("UnityEngine.LocalizationModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityCurlModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS5Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS4VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS4Module")]
[assembly: InternalsVisibleTo("UnityEngine.XboxOneModule")]
[assembly: InternalsVisibleTo("UnityEngine.SwitchModule")]
[assembly: InternalsVisibleTo("UnityEngine.VirtualTexturingModule")]
[assembly: InternalsVisibleTo("UnityEngine.VideoModule")]
[assembly: InternalsVisibleTo("UnityEngine.UmbraModule")]
[assembly: InternalsVisibleTo("UnityEngine.VehiclesModule")]
[assembly: InternalsVisibleTo("UnityEngine.XRModule")]
[assembly: InternalsVisibleTo("UnityEngine.VFXModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestWWWModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestTextureModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityTestProtocolModule")]
[assembly: InternalsVisibleTo("UnityEngine.VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS5VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.IMGUIModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreFontEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.HotReloadModule")]
[assembly: InternalsVisibleTo("UnityEngine.AnimationModule")]
[assembly: InternalsVisibleTo("UnityEngine.AndroidJNIModule")]
[assembly: InternalsVisibleTo("UnityEngine.AccessibilityModule")]
[assembly: InternalsVisibleTo("UnityEngine.ARModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputModule")]
[assembly: InternalsVisibleTo("UnityEngine.JSONSerializeModule")]
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.PhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.CoreModule")]
[assembly: InternalsVisibleTo("UnityEngine.SharedInternalsModule")]
[assembly: InternalsVisibleTo("UnityEngine")]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: CompilationRelaxations(8)]
[assembly: InternalsVisibleTo("UnityEngine.AIModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreTextEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.AudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputLegacyModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextRenderingModule")]
[assembly: InternalsVisibleTo("UnityEngine.GridModule")]
[assembly: InternalsVisibleTo("UnityEngine.GameCenterModule")]
[assembly: InternalsVisibleTo("UnityEngine.ImageConversionModule")]
[assembly: InternalsVisibleTo("UnityEngine.GIModule")]
[assembly: InternalsVisibleTo("UnityEngine.DirectorModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClothModule")]
[assembly: InternalsVisibleTo("UnityEngine.DSPGraphModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestModule")]
[assembly: InternalsVisibleTo("UnityEngine.TLSModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommonModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityConnectModule")]
[assembly: InternalsVisibleTo("UnityEngine.ContentLoadModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterRendererModule")]
[assembly: InternalsVisibleTo("UnityEngine.CrashReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.Networking")]
[assembly: InternalsVisibleTo("UnityEngine.WindModule")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud.Service")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.009")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.008")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.007")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.006")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.010")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.002")]
[assembly: InternalsVisibleTo("UnityEngine.Core.Runtime.Tests")]
[assembly: InternalsVisibleTo("Unity.Core")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud")]
[assembly: InternalsVisibleTo("Unity.Collections")]
[assembly: InternalsVisibleTo("Unity.Entities.Tests")]
[assembly: InternalsVisibleTo("Unity.Entities")]
[assembly: InternalsVisibleTo("Unity.Logging")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.001")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.011")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.012")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.013")]
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
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.015")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.014")]
[assembly: InternalsVisibleTo("Unity.Services.QoS")]
[assembly: InternalsVisibleTo("Unity.ucg.QoS")]
[assembly: InternalsVisibleTo("Unity.Runtime")]
[assembly: InternalsVisibleTo("UnityEngine.UI")]
[assembly: InternalsVisibleTo("Unity.PerformanceTests.RuntimeTestRunner.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework")]
[assembly: InternalsVisibleTo("Unity.Networking.Transport")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Framework")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Timeline")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.UnityAnalytics")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.AllIn1Runner")]
[assembly: InternalsVisibleTo("Unity.DeploymentTests.Services")]
[assembly: InternalsVisibleTo("Unity.Burst")]
[assembly: InternalsVisibleTo("Unity.Automation")]
[assembly: InternalsVisibleTo("UnityEngine.TestRunner")]
[assembly: InternalsVisibleTo("UnityEngine.Purchasing")]
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommon")]
[assembly: InternalsVisibleTo("UnityEngine.Analytics")]
[assembly: InternalsVisibleTo("Unity.Analytics")]
[assembly: InternalsVisibleTo("Unity.Burst.Editor")]
[assembly: InternalsVisibleTo("Unity.Timeline")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests")]
[assembly: InternalsVisibleTo("Assembly-CSharp-firstpass-testable")]
[assembly: InternalsVisibleTo("Unity.UIElements.EditorTests")]
[assembly: InternalsVisibleTo("UnityEngine.UIElements.Tests")]
[assembly: InternalsVisibleTo("Unity.UIElements.PlayModeTests")]
[assembly: InternalsVisibleTo("Unity.UIElements.Editor")]
[assembly: InternalsVisibleTo("Assembly-CSharp-testable")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsGameObjectsModule")]
[assembly: InternalsVisibleTo("Unity.UIElements")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.EditorTests")]
[assembly: UnityEngineModuleAssembly]
[assembly: InternalsVisibleTo("UnityEngine.SpatialTracking")]
[assembly: InternalsVisibleTo("UnityEditor.UIBuilderModule")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.Editor")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.EditorTests")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.Editor")]
[assembly: InternalsVisibleTo("Unity.WindowsMRAutomation")]
[assembly: InternalsVisibleTo("GoogleAR.UnityNative")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace UnityEngine;

[NativeHeader("Modules/Physics2D/Public/PhysicsSceneHandle2D.h")]
public struct PhysicsScene2D : IEquatable<PhysicsScene2D>
{
	private int m_Handle;

	public override string ToString()
	{
		return UnityString.Format("({0})", m_Handle);
	}

	public static bool operator ==(PhysicsScene2D lhs, PhysicsScene2D rhs)
	{
		return lhs.m_Handle == rhs.m_Handle;
	}

	public static bool operator !=(PhysicsScene2D lhs, PhysicsScene2D rhs)
	{
		return lhs.m_Handle != rhs.m_Handle;
	}

	public override int GetHashCode()
	{
		return m_Handle;
	}

	public override bool Equals(object other)
	{
		if (!(other is PhysicsScene2D physicsScene2D))
		{
			return false;
		}
		return m_Handle == physicsScene2D.m_Handle;
	}

	public bool Equals(PhysicsScene2D other)
	{
		return m_Handle == other.m_Handle;
	}

	public bool IsValid()
	{
		return IsValid_Internal(this);
	}

	[StaticAccessor("GetPhysicsManager2D()", StaticAccessorType.Arrow)]
	[NativeMethod("IsPhysicsSceneValid")]
	private static bool IsValid_Internal(PhysicsScene2D physicsScene)
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

	[StaticAccessor("GetPhysicsManager2D()", StaticAccessorType.Arrow)]
	[NativeMethod("IsPhysicsWorldEmpty")]
	private static bool IsEmpty_Internal(PhysicsScene2D physicsScene)
	{
		return IsEmpty_Internal_Injected(ref physicsScene);
	}

	public bool Simulate(float step)
	{
		if (IsValid())
		{
			return Physics2D.Simulate_Internal(this, step);
		}
		throw new InvalidOperationException("Cannot simulate the physics scene as it is invalid.");
	}

	public RaycastHit2D Linecast(Vector2 start, Vector2 end, [UnityEngine.Internal.DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return Linecast_Internal(this, start, end, contactFilter);
	}

	public RaycastHit2D Linecast(Vector2 start, Vector2 end, ContactFilter2D contactFilter)
	{
		return Linecast_Internal(this, start, end, contactFilter);
	}

	[NativeMethod("Linecast_Binding")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static RaycastHit2D Linecast_Internal(PhysicsScene2D physicsScene, Vector2 start, Vector2 end, ContactFilter2D contactFilter)
	{
		Linecast_Internal_Injected(ref physicsScene, ref start, ref end, ref contactFilter, out var ret);
		return ret;
	}

	public int Linecast(Vector2 start, Vector2 end, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return LinecastArray_Internal(this, start, end, contactFilter, results);
	}

	public int Linecast(Vector2 start, Vector2 end, ContactFilter2D contactFilter, RaycastHit2D[] results)
	{
		return LinecastArray_Internal(this, start, end, contactFilter, results);
	}

	[NativeMethod("LinecastArray_Binding")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static int LinecastArray_Internal(PhysicsScene2D physicsScene, Vector2 start, Vector2 end, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")][Unmarshalled] RaycastHit2D[] results)
	{
		return LinecastArray_Internal_Injected(ref physicsScene, ref start, ref end, ref contactFilter, results);
	}

	public int Linecast(Vector2 start, Vector2 end, ContactFilter2D contactFilter, List<RaycastHit2D> results)
	{
		return LinecastNonAllocList_Internal(this, start, end, contactFilter, results);
	}

	[NativeMethod("LinecastList_Binding")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static int LinecastNonAllocList_Internal(PhysicsScene2D physicsScene, Vector2 start, Vector2 end, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<RaycastHit2D> results)
	{
		return LinecastNonAllocList_Internal_Injected(ref physicsScene, ref start, ref end, ref contactFilter, results);
	}

	public RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, [UnityEngine.Internal.DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return Raycast_Internal(this, origin, direction, distance, contactFilter);
	}

	public RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter)
	{
		return Raycast_Internal(this, origin, direction, distance, contactFilter);
	}

	[NativeMethod("Raycast_Binding")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static RaycastHit2D Raycast_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter)
	{
		Raycast_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, ref contactFilter, out var ret);
		return ret;
	}

	public int Raycast(Vector2 origin, Vector2 direction, float distance, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return RaycastArray_Internal(this, origin, direction, distance, contactFilter, results);
	}

	public int Raycast(Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter, RaycastHit2D[] results)
	{
		return RaycastArray_Internal(this, origin, direction, distance, contactFilter, results);
	}

	[NativeMethod("RaycastArray_Binding")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static int RaycastArray_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")][Unmarshalled] RaycastHit2D[] results)
	{
		return RaycastArray_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, ref contactFilter, results);
	}

	public int Raycast(Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter, List<RaycastHit2D> results)
	{
		return RaycastList_Internal(this, origin, direction, distance, contactFilter, results);
	}

	[NativeMethod("RaycastList_Binding")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static int RaycastList_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<RaycastHit2D> results)
	{
		return RaycastList_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, ref contactFilter, results);
	}

	public RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, [UnityEngine.Internal.DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return CircleCast_Internal(this, origin, radius, direction, distance, contactFilter);
	}

	public RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, ContactFilter2D contactFilter)
	{
		return CircleCast_Internal(this, origin, radius, direction, distance, contactFilter);
	}

	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("CircleCast_Binding")]
	private static RaycastHit2D CircleCast_Internal(PhysicsScene2D physicsScene, Vector2 origin, float radius, Vector2 direction, float distance, ContactFilter2D contactFilter)
	{
		CircleCast_Internal_Injected(ref physicsScene, ref origin, radius, ref direction, distance, ref contactFilter, out var ret);
		return ret;
	}

	public int CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return CircleCastArray_Internal(this, origin, radius, direction, distance, contactFilter, results);
	}

	public int CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, ContactFilter2D contactFilter, RaycastHit2D[] results)
	{
		return CircleCastArray_Internal(this, origin, radius, direction, distance, contactFilter, results);
	}

	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("CircleCastArray_Binding")]
	private static int CircleCastArray_Internal(PhysicsScene2D physicsScene, Vector2 origin, float radius, Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")][Unmarshalled] RaycastHit2D[] results)
	{
		return CircleCastArray_Internal_Injected(ref physicsScene, ref origin, radius, ref direction, distance, ref contactFilter, results);
	}

	public int CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, ContactFilter2D contactFilter, List<RaycastHit2D> results)
	{
		return CircleCastList_Internal(this, origin, radius, direction, distance, contactFilter, results);
	}

	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("CircleCastList_Binding")]
	private static int CircleCastList_Internal(PhysicsScene2D physicsScene, Vector2 origin, float radius, Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<RaycastHit2D> results)
	{
		return CircleCastList_Internal_Injected(ref physicsScene, ref origin, radius, ref direction, distance, ref contactFilter, results);
	}

	public RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, [UnityEngine.Internal.DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return BoxCast_Internal(this, origin, size, angle, direction, distance, contactFilter);
	}

	public RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter)
	{
		return BoxCast_Internal(this, origin, size, angle, direction, distance, contactFilter);
	}

	[NativeMethod("BoxCast_Binding")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static RaycastHit2D BoxCast_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter)
	{
		BoxCast_Internal_Injected(ref physicsScene, ref origin, ref size, angle, ref direction, distance, ref contactFilter, out var ret);
		return ret;
	}

	public int BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return BoxCastArray_Internal(this, origin, size, angle, direction, distance, contactFilter, results);
	}

	public int BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, RaycastHit2D[] results)
	{
		return BoxCastArray_Internal(this, origin, size, angle, direction, distance, contactFilter, results);
	}

	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("BoxCastArray_Binding")]
	private static int BoxCastArray_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")][Unmarshalled] RaycastHit2D[] results)
	{
		return BoxCastArray_Internal_Injected(ref physicsScene, ref origin, ref size, angle, ref direction, distance, ref contactFilter, results);
	}

	public int BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, List<RaycastHit2D> results)
	{
		return BoxCastList_Internal(this, origin, size, angle, direction, distance, contactFilter, results);
	}

	[NativeMethod("BoxCastList_Binding")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static int BoxCastList_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<RaycastHit2D> results)
	{
		return BoxCastList_Internal_Injected(ref physicsScene, ref origin, ref size, angle, ref direction, distance, ref contactFilter, results);
	}

	public RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, [UnityEngine.Internal.DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return CapsuleCast_Internal(this, origin, size, capsuleDirection, angle, direction, distance, contactFilter);
	}

	public RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter)
	{
		return CapsuleCast_Internal(this, origin, size, capsuleDirection, angle, direction, distance, contactFilter);
	}

	[NativeMethod("CapsuleCast_Binding")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static RaycastHit2D CapsuleCast_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter)
	{
		CapsuleCast_Internal_Injected(ref physicsScene, ref origin, ref size, capsuleDirection, angle, ref direction, distance, ref contactFilter, out var ret);
		return ret;
	}

	public int CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return CapsuleCastArray_Internal(this, origin, size, capsuleDirection, angle, direction, distance, contactFilter, results);
	}

	public int CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, RaycastHit2D[] results)
	{
		return CapsuleCastArray_Internal(this, origin, size, capsuleDirection, angle, direction, distance, contactFilter, results);
	}

	[NativeMethod("CapsuleCastArray_Binding")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static int CapsuleCastArray_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, [Unmarshalled][NotNull("ArgumentNullException")] RaycastHit2D[] results)
	{
		return CapsuleCastArray_Internal_Injected(ref physicsScene, ref origin, ref size, capsuleDirection, angle, ref direction, distance, ref contactFilter, results);
	}

	public int CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, List<RaycastHit2D> results)
	{
		return CapsuleCastList_Internal(this, origin, size, capsuleDirection, angle, direction, distance, contactFilter, results);
	}

	[NativeMethod("CapsuleCastList_Binding")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static int CapsuleCastList_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<RaycastHit2D> results)
	{
		return CapsuleCastList_Internal_Injected(ref physicsScene, ref origin, ref size, capsuleDirection, angle, ref direction, distance, ref contactFilter, results);
	}

	public RaycastHit2D GetRayIntersection(Ray ray, float distance, [UnityEngine.Internal.DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
	{
		return GetRayIntersection_Internal(this, ray.origin, ray.direction, distance, layerMask);
	}

	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("GetRayIntersection_Binding")]
	private static RaycastHit2D GetRayIntersection_Internal(PhysicsScene2D physicsScene, Vector3 origin, Vector3 direction, float distance, int layerMask)
	{
		GetRayIntersection_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, layerMask, out var ret);
		return ret;
	}

	public int GetRayIntersection(Ray ray, float distance, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
	{
		return GetRayIntersectionArray_Internal(this, ray.origin, ray.direction, distance, layerMask, results);
	}

	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("GetRayIntersectionArray_Binding")]
	private static int GetRayIntersectionArray_Internal(PhysicsScene2D physicsScene, Vector3 origin, Vector3 direction, float distance, int layerMask, [NotNull("ArgumentNullException")][Unmarshalled] RaycastHit2D[] results)
	{
		return GetRayIntersectionArray_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, layerMask, results);
	}

	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("GetRayIntersectionList_Binding")]
	private static int GetRayIntersectionList_Internal(PhysicsScene2D physicsScene, Vector3 origin, Vector3 direction, float distance, int layerMask, [NotNull("ArgumentNullException")] List<RaycastHit2D> results)
	{
		return GetRayIntersectionList_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, layerMask, results);
	}

	public Collider2D OverlapPoint(Vector2 point, [UnityEngine.Internal.DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapPoint_Internal(this, point, contactFilter);
	}

	public Collider2D OverlapPoint(Vector2 point, ContactFilter2D contactFilter)
	{
		return OverlapPoint_Internal(this, point, contactFilter);
	}

	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("OverlapPoint_Binding")]
	private static Collider2D OverlapPoint_Internal(PhysicsScene2D physicsScene, Vector2 point, ContactFilter2D contactFilter)
	{
		return OverlapPoint_Internal_Injected(ref physicsScene, ref point, ref contactFilter);
	}

	public int OverlapPoint(Vector2 point, Collider2D[] results, [UnityEngine.Internal.DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapPointArray_Internal(this, point, contactFilter, results);
	}

	public int OverlapPoint(Vector2 point, ContactFilter2D contactFilter, Collider2D[] results)
	{
		return OverlapPointArray_Internal(this, point, contactFilter, results);
	}

	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("OverlapPointArray_Binding")]
	private static int OverlapPointArray_Internal(PhysicsScene2D physicsScene, Vector2 point, ContactFilter2D contactFilter, [Unmarshalled][NotNull("ArgumentNullException")] Collider2D[] results)
	{
		return OverlapPointArray_Internal_Injected(ref physicsScene, ref point, ref contactFilter, results);
	}

	public int OverlapPoint(Vector2 point, ContactFilter2D contactFilter, List<Collider2D> results)
	{
		return OverlapPointList_Internal(this, point, contactFilter, results);
	}

	[NativeMethod("OverlapPointList_Binding")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static int OverlapPointList_Internal(PhysicsScene2D physicsScene, Vector2 point, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<Collider2D> results)
	{
		return OverlapPointList_Internal_Injected(ref physicsScene, ref point, ref contactFilter, results);
	}

	public Collider2D OverlapCircle(Vector2 point, float radius, [UnityEngine.Internal.DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapCircle_Internal(this, point, radius, contactFilter);
	}

	public Collider2D OverlapCircle(Vector2 point, float radius, ContactFilter2D contactFilter)
	{
		return OverlapCircle_Internal(this, point, radius, contactFilter);
	}

	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("OverlapCircle_Binding")]
	private static Collider2D OverlapCircle_Internal(PhysicsScene2D physicsScene, Vector2 point, float radius, ContactFilter2D contactFilter)
	{
		return OverlapCircle_Internal_Injected(ref physicsScene, ref point, radius, ref contactFilter);
	}

	public int OverlapCircle(Vector2 point, float radius, Collider2D[] results, [UnityEngine.Internal.DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapCircleArray_Internal(this, point, radius, contactFilter, results);
	}

	public int OverlapCircle(Vector2 point, float radius, ContactFilter2D contactFilter, Collider2D[] results)
	{
		return OverlapCircleArray_Internal(this, point, radius, contactFilter, results);
	}

	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("OverlapCircleArray_Binding")]
	private static int OverlapCircleArray_Internal(PhysicsScene2D physicsScene, Vector2 point, float radius, ContactFilter2D contactFilter, [Unmarshalled][NotNull("ArgumentNullException")] Collider2D[] results)
	{
		return OverlapCircleArray_Internal_Injected(ref physicsScene, ref point, radius, ref contactFilter, results);
	}

	public int OverlapCircle(Vector2 point, float radius, ContactFilter2D contactFilter, List<Collider2D> results)
	{
		return OverlapCircleList_Internal(this, point, radius, contactFilter, results);
	}

	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("OverlapCircleList_Binding")]
	private static int OverlapCircleList_Internal(PhysicsScene2D physicsScene, Vector2 point, float radius, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<Collider2D> results)
	{
		return OverlapCircleList_Internal_Injected(ref physicsScene, ref point, radius, ref contactFilter, results);
	}

	public Collider2D OverlapBox(Vector2 point, Vector2 size, float angle, [UnityEngine.Internal.DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapBox_Internal(this, point, size, angle, contactFilter);
	}

	public Collider2D OverlapBox(Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter)
	{
		return OverlapBox_Internal(this, point, size, angle, contactFilter);
	}

	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("OverlapBox_Binding")]
	private static Collider2D OverlapBox_Internal(PhysicsScene2D physicsScene, Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter)
	{
		return OverlapBox_Internal_Injected(ref physicsScene, ref point, ref size, angle, ref contactFilter);
	}

	public int OverlapBox(Vector2 point, Vector2 size, float angle, Collider2D[] results, [UnityEngine.Internal.DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapBoxArray_Internal(this, point, size, angle, contactFilter, results);
	}

	public int OverlapBox(Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter, Collider2D[] results)
	{
		return OverlapBoxArray_Internal(this, point, size, angle, contactFilter, results);
	}

	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("OverlapBoxArray_Binding")]
	private static int OverlapBoxArray_Internal(PhysicsScene2D physicsScene, Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter, [Unmarshalled][NotNull("ArgumentNullException")] Collider2D[] results)
	{
		return OverlapBoxArray_Internal_Injected(ref physicsScene, ref point, ref size, angle, ref contactFilter, results);
	}

	public int OverlapBox(Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter, List<Collider2D> results)
	{
		return OverlapBoxList_Internal(this, point, size, angle, contactFilter, results);
	}

	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("OverlapBoxList_Binding")]
	private static int OverlapBoxList_Internal(PhysicsScene2D physicsScene, Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<Collider2D> results)
	{
		return OverlapBoxList_Internal_Injected(ref physicsScene, ref point, ref size, angle, ref contactFilter, results);
	}

	public Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, [UnityEngine.Internal.DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapAreaToBoxArray_Internal(pointA, pointB, contactFilter);
	}

	public Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter)
	{
		return OverlapAreaToBoxArray_Internal(pointA, pointB, contactFilter);
	}

	private Collider2D OverlapAreaToBoxArray_Internal(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter)
	{
		Vector2 point = (pointA + pointB) * 0.5f;
		Vector2 size = new Vector2(Mathf.Abs(pointA.x - pointB.x), Math.Abs(pointA.y - pointB.y));
		return OverlapBox(point, size, 0f, contactFilter);
	}

	public int OverlapArea(Vector2 pointA, Vector2 pointB, Collider2D[] results, [UnityEngine.Internal.DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapAreaToBoxArray_Internal(pointA, pointB, contactFilter, results);
	}

	public int OverlapArea(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter, Collider2D[] results)
	{
		return OverlapAreaToBoxArray_Internal(pointA, pointB, contactFilter, results);
	}

	private int OverlapAreaToBoxArray_Internal(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter, Collider2D[] results)
	{
		Vector2 point = (pointA + pointB) * 0.5f;
		Vector2 size = new Vector2(Mathf.Abs(pointA.x - pointB.x), Math.Abs(pointA.y - pointB.y));
		return OverlapBox(point, size, 0f, contactFilter, results);
	}

	public int OverlapArea(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter, List<Collider2D> results)
	{
		return OverlapAreaToBoxList_Internal(pointA, pointB, contactFilter, results);
	}

	private int OverlapAreaToBoxList_Internal(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter, List<Collider2D> results)
	{
		Vector2 point = (pointA + pointB) * 0.5f;
		Vector2 size = new Vector2(Mathf.Abs(pointA.x - pointB.x), Math.Abs(pointA.y - pointB.y));
		return OverlapBox(point, size, 0f, contactFilter, results);
	}

	public Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, [UnityEngine.Internal.DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapCapsule_Internal(this, point, size, direction, angle, contactFilter);
	}

	public Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter)
	{
		return OverlapCapsule_Internal(this, point, size, direction, angle, contactFilter);
	}

	[NativeMethod("OverlapCapsule_Binding")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static Collider2D OverlapCapsule_Internal(PhysicsScene2D physicsScene, Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter)
	{
		return OverlapCapsule_Internal_Injected(ref physicsScene, ref point, ref size, direction, angle, ref contactFilter);
	}

	public int OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, Collider2D[] results, [UnityEngine.Internal.DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapCapsuleArray_Internal(this, point, size, direction, angle, contactFilter, results);
	}

	public int OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter, Collider2D[] results)
	{
		return OverlapCapsuleArray_Internal(this, point, size, direction, angle, contactFilter, results);
	}

	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("OverlapCapsuleArray_Binding")]
	private static int OverlapCapsuleArray_Internal(PhysicsScene2D physicsScene, Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter, [Unmarshalled][NotNull("ArgumentNullException")] Collider2D[] results)
	{
		return OverlapCapsuleArray_Internal_Injected(ref physicsScene, ref point, ref size, direction, angle, ref contactFilter, results);
	}

	public int OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter, List<Collider2D> results)
	{
		return OverlapCapsuleList_Internal(this, point, size, direction, angle, contactFilter, results);
	}

	[NativeMethod("OverlapCapsuleList_Binding")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static int OverlapCapsuleList_Internal(PhysicsScene2D physicsScene, Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<Collider2D> results)
	{
		return OverlapCapsuleList_Internal_Injected(ref physicsScene, ref point, ref size, direction, angle, ref contactFilter, results);
	}

	public static int OverlapCollider(Collider2D collider, Collider2D[] results, [UnityEngine.Internal.DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapColliderArray_Internal(collider, contactFilter, results);
	}

	public static int OverlapCollider(Collider2D collider, ContactFilter2D contactFilter, Collider2D[] results)
	{
		return OverlapColliderArray_Internal(collider, contactFilter, results);
	}

	[NativeMethod("OverlapColliderArray_Binding")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static int OverlapColliderArray_Internal([NotNull("ArgumentNullException")] Collider2D collider, ContactFilter2D contactFilter, [Unmarshalled][NotNull("ArgumentNullException")] Collider2D[] results)
	{
		return OverlapColliderArray_Internal_Injected(collider, ref contactFilter, results);
	}

	public static int OverlapCollider(Collider2D collider, ContactFilter2D contactFilter, List<Collider2D> results)
	{
		return OverlapColliderList_Internal(collider, contactFilter, results);
	}

	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("OverlapColliderList_Binding")]
	private static int OverlapColliderList_Internal([NotNull("ArgumentNullException")] Collider2D collider, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<Collider2D> results)
	{
		return OverlapColliderList_Internal_Injected(collider, ref contactFilter, results);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool IsValid_Internal_Injected(ref PhysicsScene2D physicsScene);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool IsEmpty_Internal_Injected(ref PhysicsScene2D physicsScene);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Linecast_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 start, ref Vector2 end, ref ContactFilter2D contactFilter, out RaycastHit2D ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int LinecastArray_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 start, ref Vector2 end, ref ContactFilter2D contactFilter, RaycastHit2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int LinecastNonAllocList_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 start, ref Vector2 end, ref ContactFilter2D contactFilter, List<RaycastHit2D> results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Raycast_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, out RaycastHit2D ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int RaycastArray_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, RaycastHit2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int RaycastList_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, List<RaycastHit2D> results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void CircleCast_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, float radius, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, out RaycastHit2D ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int CircleCastArray_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, float radius, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, RaycastHit2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int CircleCastList_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, float radius, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, List<RaycastHit2D> results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void BoxCast_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, ref Vector2 size, float angle, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, out RaycastHit2D ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int BoxCastArray_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, ref Vector2 size, float angle, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, RaycastHit2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int BoxCastList_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, ref Vector2 size, float angle, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, List<RaycastHit2D> results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void CapsuleCast_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, ref Vector2 size, CapsuleDirection2D capsuleDirection, float angle, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, out RaycastHit2D ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int CapsuleCastArray_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, ref Vector2 size, CapsuleDirection2D capsuleDirection, float angle, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, RaycastHit2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int CapsuleCastList_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, ref Vector2 size, CapsuleDirection2D capsuleDirection, float angle, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, List<RaycastHit2D> results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void GetRayIntersection_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector3 origin, ref Vector3 direction, float distance, int layerMask, out RaycastHit2D ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int GetRayIntersectionArray_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector3 origin, ref Vector3 direction, float distance, int layerMask, RaycastHit2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int GetRayIntersectionList_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector3 origin, ref Vector3 direction, float distance, int layerMask, List<RaycastHit2D> results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern Collider2D OverlapPoint_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int OverlapPointArray_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, ref ContactFilter2D contactFilter, Collider2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int OverlapPointList_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, ref ContactFilter2D contactFilter, List<Collider2D> results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern Collider2D OverlapCircle_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, float radius, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int OverlapCircleArray_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, float radius, ref ContactFilter2D contactFilter, Collider2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int OverlapCircleList_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, float radius, ref ContactFilter2D contactFilter, List<Collider2D> results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern Collider2D OverlapBox_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, ref Vector2 size, float angle, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int OverlapBoxArray_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, ref Vector2 size, float angle, ref ContactFilter2D contactFilter, Collider2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int OverlapBoxList_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, ref Vector2 size, float angle, ref ContactFilter2D contactFilter, List<Collider2D> results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern Collider2D OverlapCapsule_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, ref Vector2 size, CapsuleDirection2D direction, float angle, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int OverlapCapsuleArray_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, ref Vector2 size, CapsuleDirection2D direction, float angle, ref ContactFilter2D contactFilter, Collider2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int OverlapCapsuleList_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, ref Vector2 size, CapsuleDirection2D direction, float angle, ref ContactFilter2D contactFilter, List<Collider2D> results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int OverlapColliderArray_Internal_Injected(Collider2D collider, ref ContactFilter2D contactFilter, Collider2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int OverlapColliderList_Internal_Injected(Collider2D collider, ref ContactFilter2D contactFilter, List<Collider2D> results);
}
public static class PhysicsSceneExtensions2D
{
	public static PhysicsScene2D GetPhysicsScene2D(this Scene scene)
	{
		if (!scene.IsValid())
		{
			throw new ArgumentException("Cannot get physics scene; Unity scene is invalid.", "scene");
		}
		PhysicsScene2D physicsScene_Internal = GetPhysicsScene_Internal(scene);
		if (physicsScene_Internal.IsValid())
		{
			return physicsScene_Internal;
		}
		throw new Exception("The physics scene associated with the Unity scene is invalid.");
	}

	[NativeMethod("GetPhysicsSceneFromUnityScene")]
	[StaticAccessor("GetPhysicsManager2D()", StaticAccessorType.Arrow)]
	private static PhysicsScene2D GetPhysicsScene_Internal(Scene scene)
	{
		GetPhysicsScene_Internal_Injected(ref scene, out var ret);
		return ret;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void GetPhysicsScene_Internal_Injected(ref Scene scene, out PhysicsScene2D ret);
}
[StaticAccessor("GetPhysicsManager2D()", StaticAccessorType.Arrow)]
[NativeHeader("Physics2DScriptingClasses.h")]
[NativeHeader("Modules/Physics2D/PhysicsManager2D.h")]
[NativeHeader("Physics2DScriptingClasses.h")]
public class Physics2D
{
	[Flags]
	internal enum GizmoOptions
	{
		AllColliders = 1,
		CollidersOutlined = 2,
		CollidersFilled = 4,
		CollidersSleeping = 8,
		ColliderContacts = 0x10,
		ColliderBounds = 0x20
	}

	public const int IgnoreRaycastLayer = 4;

	public const int DefaultRaycastLayers = -5;

	public const int AllLayers = -1;

	public const int MaxPolygonShapeVertices = 8;

	private static List<Rigidbody2D> m_LastDisabledRigidbody2D = new List<Rigidbody2D>();

	public static PhysicsScene2D defaultPhysicsScene => default(PhysicsScene2D);

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern int velocityIterations
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern int positionIterations
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static Vector2 gravity
	{
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

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern bool queriesHitTriggers
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern bool queriesStartInColliders
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern bool callbacksOnDisable
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern bool reuseCollisionCallbacks
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern bool autoSyncTransforms
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern SimulationMode2D simulationMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static PhysicsJobOptions2D jobOptions
	{
		get
		{
			get_jobOptions_Injected(out var ret);
			return ret;
		}
		set
		{
			set_jobOptions_Injected(ref value);
		}
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern float velocityThreshold
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern float maxLinearCorrection
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern float maxAngularCorrection
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern float maxTranslationSpeed
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern float maxRotationSpeed
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern float defaultContactOffset
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern float baumgarteScale
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern float baumgarteTOIScale
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern float timeToSleep
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern float linearSleepTolerance
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern float angularSleepTolerance
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Physics2D.raycastsHitTriggers is deprecated. Use Physics2D.queriesHitTriggers instead. (UnityUpgradable) -> queriesHitTriggers", true)]
	public static bool raycastsHitTriggers
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
	[Obsolete("Physics2D.raycastsStartInColliders is deprecated. Use Physics2D.queriesStartInColliders instead. (UnityUpgradable) -> queriesStartInColliders", true)]
	public static bool raycastsStartInColliders
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	[Obsolete("Physics2D.deleteStopsCallbacks is deprecated.(UnityUpgradable) -> changeStopsCallbacks", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static bool deleteStopsCallbacks
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	[Obsolete("Physics2D.changeStopsCallbacks is deprecated and will always return false.", false)]
	public static bool changeStopsCallbacks
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	[Obsolete("Physics2D.minPenetrationForPenalty is deprecated. Use Physics2D.defaultContactOffset instead. (UnityUpgradable) -> defaultContactOffset", false)]
	public static float minPenetrationForPenalty
	{
		get
		{
			return defaultContactOffset;
		}
		set
		{
			defaultContactOffset = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Physics2D.autoSimulation is deprecated. Use Physics2D.simulationMode instead.", false)]
	public static bool autoSimulation
	{
		get
		{
			return simulationMode != SimulationMode2D.Script;
		}
		set
		{
			simulationMode = ((!value) ? SimulationMode2D.Script : SimulationMode2D.FixedUpdate);
		}
	}

	[Obsolete("Physics2D.colliderAwakeColor is deprecated. This options has been moved to 2D Preferences.", true)]
	public static Color colliderAwakeColor
	{
		get
		{
			return Color.magenta;
		}
		set
		{
		}
	}

	[Obsolete("Physics2D.colliderAsleepColor is deprecated. This options has been moved to 2D Preferences.", true)]
	public static Color colliderAsleepColor
	{
		get
		{
			return Color.magenta;
		}
		set
		{
		}
	}

	[Obsolete("Physics2D.colliderContactColor is deprecated. This options has been moved to 2D Preferences.", true)]
	public static Color colliderContactColor
	{
		get
		{
			return Color.magenta;
		}
		set
		{
		}
	}

	[Obsolete("Physics2D.colliderAABBColor is deprecated. All Physics 2D colors moved to Preferences. This is now known as 'Collider Bounds Color'.", true)]
	public static Color colliderAABBColor
	{
		get
		{
			return Color.magenta;
		}
		set
		{
		}
	}

	[Obsolete("Physics2D.contactArrowScale is deprecated. This options has been moved to 2D Preferences.", true)]
	public static float contactArrowScale
	{
		get
		{
			return 0.2f;
		}
		set
		{
		}
	}

	[Obsolete("Physics2D.alwaysShowColliders is deprecated. It is no longer available in the Editor or Builds.", true)]
	public static bool alwaysShowColliders { get; set; }

	[Obsolete("Physics2D.showCollidersFilled is deprecated. It is no longer available in the Editor or Builds.", true)]
	public static bool showCollidersFilled { get; set; }

	[Obsolete("Physics2D.showColliderSleep is deprecated. It is no longer available in the Editor or Builds.", true)]
	public static bool showColliderSleep { get; set; }

	[Obsolete("Physics2D.showColliderContacts is deprecated. It is no longer available in the Editor or Builds.", true)]
	public static bool showColliderContacts { get; set; }

	[Obsolete("Physics2D.showColliderAABB is deprecated. It is no longer available in the Editor or Builds.", true)]
	public static bool showColliderAABB
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	public static bool Simulate(float step)
	{
		return Simulate_Internal(defaultPhysicsScene, step);
	}

	[NativeMethod("Simulate_Binding")]
	internal static bool Simulate_Internal(PhysicsScene2D physicsScene, float step)
	{
		return Simulate_Internal_Injected(ref physicsScene, step);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern void SyncTransforms();

	[ExcludeFromDocs]
	public static void IgnoreCollision([Writable] Collider2D collider1, [Writable] Collider2D collider2)
	{
		IgnoreCollision(collider1, collider2, ignore: true);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("IgnoreCollision_Binding")]
	[StaticAccessor("PhysicsScene2D", StaticAccessorType.DoubleColon)]
	public static extern void IgnoreCollision([NotNull("ArgumentNullException")][Writable] Collider2D collider1, [NotNull("ArgumentNullException")][Writable] Collider2D collider2, [UnityEngine.Internal.DefaultValue("true")] bool ignore);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[StaticAccessor("PhysicsScene2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("GetIgnoreCollision_Binding")]
	public static extern bool GetIgnoreCollision([NotNull("ArgumentNullException")][Writable] Collider2D collider1, [NotNull("ArgumentNullException")][Writable] Collider2D collider2);

	[ExcludeFromDocs]
	public static void IgnoreLayerCollision(int layer1, int layer2)
	{
		IgnoreLayerCollision(layer1, layer2, ignore: true);
	}

	public static void IgnoreLayerCollision(int layer1, int layer2, bool ignore)
	{
		if (layer1 < 0 || layer1 > 31)
		{
			throw new ArgumentOutOfRangeException("layer1 is out of range. Layer numbers must be in the range 0 to 31.");
		}
		if (layer2 < 0 || layer2 > 31)
		{
			throw new ArgumentOutOfRangeException("layer2 is out of range. Layer numbers must be in the range 0 to 31.");
		}
		IgnoreLayerCollision_Internal(layer1, layer2, ignore);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("IgnoreLayerCollision")]
	[StaticAccessor("GetPhysics2DSettings()")]
	private static extern void IgnoreLayerCollision_Internal(int layer1, int layer2, bool ignore);

	public static bool GetIgnoreLayerCollision(int layer1, int layer2)
	{
		if (layer1 < 0 || layer1 > 31)
		{
			throw new ArgumentOutOfRangeException("layer1 is out of range. Layer numbers must be in the range 0 to 31.");
		}
		if (layer2 < 0 || layer2 > 31)
		{
			throw new ArgumentOutOfRangeException("layer2 is out of range. Layer numbers must be in the range 0 to 31.");
		}
		return GetIgnoreLayerCollision_Internal(layer1, layer2);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[StaticAccessor("GetPhysics2DSettings()")]
	[NativeMethod("GetIgnoreLayerCollision")]
	private static extern bool GetIgnoreLayerCollision_Internal(int layer1, int layer2);

	public static void SetLayerCollisionMask(int layer, int layerMask)
	{
		if (layer < 0 || layer > 31)
		{
			throw new ArgumentOutOfRangeException("layer1 is out of range. Layer numbers must be in the range 0 to 31.");
		}
		SetLayerCollisionMask_Internal(layer, layerMask);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[StaticAccessor("GetPhysics2DSettings()")]
	[NativeMethod("SetLayerCollisionMask")]
	private static extern void SetLayerCollisionMask_Internal(int layer, int layerMask);

	public static int GetLayerCollisionMask(int layer)
	{
		if (layer < 0 || layer > 31)
		{
			throw new ArgumentOutOfRangeException("layer1 is out of range. Layer numbers must be in the range 0 to 31.");
		}
		return GetLayerCollisionMask_Internal(layer);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("GetLayerCollisionMask")]
	[StaticAccessor("GetPhysics2DSettings()")]
	private static extern int GetLayerCollisionMask_Internal(int layer);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	public static extern bool IsTouching([Writable][NotNull("ArgumentNullException")] Collider2D collider1, [NotNull("ArgumentNullException")][Writable] Collider2D collider2);

	public static bool IsTouching([Writable] Collider2D collider1, [Writable] Collider2D collider2, ContactFilter2D contactFilter)
	{
		return IsTouching_TwoCollidersWithFilter(collider1, collider2, contactFilter);
	}

	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("IsTouching")]
	private static bool IsTouching_TwoCollidersWithFilter([NotNull("ArgumentNullException")][Writable] Collider2D collider1, [NotNull("ArgumentNullException")][Writable] Collider2D collider2, ContactFilter2D contactFilter)
	{
		return IsTouching_TwoCollidersWithFilter_Injected(collider1, collider2, ref contactFilter);
	}

	public static bool IsTouching([Writable] Collider2D collider, ContactFilter2D contactFilter)
	{
		return IsTouching_SingleColliderWithFilter(collider, contactFilter);
	}

	[NativeMethod("IsTouching")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static bool IsTouching_SingleColliderWithFilter([NotNull("ArgumentNullException")][Writable] Collider2D collider, ContactFilter2D contactFilter)
	{
		return IsTouching_SingleColliderWithFilter_Injected(collider, ref contactFilter);
	}

	[ExcludeFromDocs]
	public static bool IsTouchingLayers([Writable] Collider2D collider)
	{
		return IsTouchingLayers(collider, -1);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	public static extern bool IsTouchingLayers([NotNull("ArgumentNullException")][Writable] Collider2D collider, [UnityEngine.Internal.DefaultValue("Physics2D.AllLayers")] int layerMask);

	public static ColliderDistance2D Distance([Writable] Collider2D colliderA, [Writable] Collider2D colliderB)
	{
		if (colliderA == null)
		{
			throw new ArgumentNullException("ColliderA cannot be NULL.");
		}
		if (colliderB == null)
		{
			throw new ArgumentNullException("ColliderB cannot be NULL.");
		}
		if (colliderA == colliderB)
		{
			throw new ArgumentException("Cannot calculate the distance between the same collider.");
		}
		return Distance_Internal(colliderA, colliderB);
	}

	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("Distance")]
	private static ColliderDistance2D Distance_Internal([NotNull("ArgumentNullException")][Writable] Collider2D colliderA, [NotNull("ArgumentNullException")][Writable] Collider2D colliderB)
	{
		Distance_Internal_Injected(colliderA, colliderB, out var ret);
		return ret;
	}

	public static Vector2 ClosestPoint(Vector2 position, Collider2D collider)
	{
		if (collider == null)
		{
			throw new ArgumentNullException("Collider cannot be NULL.");
		}
		return ClosestPoint_Collider(position, collider);
	}

	public static Vector2 ClosestPoint(Vector2 position, Rigidbody2D rigidbody)
	{
		if (rigidbody == null)
		{
			throw new ArgumentNullException("Rigidbody cannot be NULL.");
		}
		return ClosestPoint_Rigidbody(position, rigidbody);
	}

	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("ClosestPoint")]
	private static Vector2 ClosestPoint_Collider(Vector2 position, [NotNull("ArgumentNullException")] Collider2D collider)
	{
		ClosestPoint_Collider_Injected(ref position, collider, out var ret);
		return ret;
	}

	[NativeMethod("ClosestPoint")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static Vector2 ClosestPoint_Rigidbody(Vector2 position, [NotNull("ArgumentNullException")] Rigidbody2D rigidbody)
	{
		ClosestPoint_Rigidbody_Injected(ref position, rigidbody, out var ret);
		return ret;
	}

	[ExcludeFromDocs]
	public static RaycastHit2D Linecast(Vector2 start, Vector2 end)
	{
		return defaultPhysicsScene.Linecast(start, end);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D Linecast(Vector2 start, Vector2 end, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return defaultPhysicsScene.Linecast(start, end, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D Linecast(Vector2 start, Vector2 end, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return defaultPhysicsScene.Linecast(start, end, contactFilter);
	}

	public static RaycastHit2D Linecast(Vector2 start, Vector2 end, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return defaultPhysicsScene.Linecast(start, end, contactFilter);
	}

	public static int Linecast(Vector2 start, Vector2 end, ContactFilter2D contactFilter, RaycastHit2D[] results)
	{
		return defaultPhysicsScene.Linecast(start, end, contactFilter, results);
	}

	public static int Linecast(Vector2 start, Vector2 end, ContactFilter2D contactFilter, List<RaycastHit2D> results)
	{
		return defaultPhysicsScene.Linecast(start, end, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return LinecastAll_Internal(defaultPhysicsScene, start, end, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return LinecastAll_Internal(defaultPhysicsScene, start, end, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return LinecastAll_Internal(defaultPhysicsScene, start, end, contactFilter);
	}

	public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return LinecastAll_Internal(defaultPhysicsScene, start, end, contactFilter);
	}

	[NativeMethod("LinecastAll_Binding")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static RaycastHit2D[] LinecastAll_Internal(PhysicsScene2D physicsScene, Vector2 start, Vector2 end, ContactFilter2D contactFilter)
	{
		return LinecastAll_Internal_Injected(ref physicsScene, ref start, ref end, ref contactFilter);
	}

	[ExcludeFromDocs]
	public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results)
	{
		return defaultPhysicsScene.Linecast(start, end, results);
	}

	[ExcludeFromDocs]
	public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return defaultPhysicsScene.Linecast(start, end, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return defaultPhysicsScene.Linecast(start, end, contactFilter, results);
	}

	public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return defaultPhysicsScene.Linecast(start, end, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction)
	{
		return defaultPhysicsScene.Raycast(origin, direction, float.PositiveInfinity);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance)
	{
		return defaultPhysicsScene.Raycast(origin, direction, distance);
	}

	[RequiredByNativeCode]
	[ExcludeFromDocs]
	public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter);
	}

	public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static int Raycast(Vector2 origin, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
	{
		return defaultPhysicsScene.Raycast(origin, direction, float.PositiveInfinity, contactFilter, results);
	}

	public static int Raycast(Vector2 origin, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance)
	{
		return defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter, results);
	}

	public static int Raycast(Vector2 origin, Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity)
	{
		return defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results)
	{
		return defaultPhysicsScene.Raycast(origin, direction, float.PositiveInfinity, results);
	}

	[ExcludeFromDocs]
	public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, float distance)
	{
		return defaultPhysicsScene.Raycast(origin, direction, distance, results);
	}

	[ExcludeFromDocs]
	public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter, results);
	}

	public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return RaycastAll_Internal(defaultPhysicsScene, origin, direction, float.PositiveInfinity, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return RaycastAll_Internal(defaultPhysicsScene, origin, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return RaycastAll_Internal(defaultPhysicsScene, origin, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return RaycastAll_Internal(defaultPhysicsScene, origin, direction, distance, contactFilter);
	}

	public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return RaycastAll_Internal(defaultPhysicsScene, origin, direction, distance, contactFilter);
	}

	[NativeMethod("RaycastAll_Binding")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static RaycastHit2D[] RaycastAll_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter)
	{
		return RaycastAll_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, ref contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction)
	{
		return defaultPhysicsScene.CircleCast(origin, radius, direction, float.PositiveInfinity);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance)
	{
		return defaultPhysicsScene.CircleCast(origin, radius, direction, distance);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return defaultPhysicsScene.CircleCast(origin, radius, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return defaultPhysicsScene.CircleCast(origin, radius, direction, distance, contactFilter);
	}

	public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return defaultPhysicsScene.CircleCast(origin, radius, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static int CircleCast(Vector2 origin, float radius, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
	{
		return defaultPhysicsScene.CircleCast(origin, radius, direction, float.PositiveInfinity, contactFilter, results);
	}

	public static int CircleCast(Vector2 origin, float radius, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance)
	{
		return defaultPhysicsScene.CircleCast(origin, radius, direction, distance, contactFilter, results);
	}

	public static int CircleCast(Vector2 origin, float radius, Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity)
	{
		return defaultPhysicsScene.CircleCast(origin, radius, direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return CircleCastAll_Internal(defaultPhysicsScene, origin, radius, direction, float.PositiveInfinity, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, float distance)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return CircleCastAll_Internal(defaultPhysicsScene, origin, radius, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return CircleCastAll_Internal(defaultPhysicsScene, origin, radius, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return CircleCastAll_Internal(defaultPhysicsScene, origin, radius, direction, distance, contactFilter);
	}

	public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return CircleCastAll_Internal(defaultPhysicsScene, origin, radius, direction, distance, contactFilter);
	}

	[NativeMethod("CircleCastAll_Binding")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static RaycastHit2D[] CircleCastAll_Internal(PhysicsScene2D physicsScene, Vector2 origin, float radius, Vector2 direction, float distance, ContactFilter2D contactFilter)
	{
		return CircleCastAll_Internal_Injected(ref physicsScene, ref origin, radius, ref direction, distance, ref contactFilter);
	}

	[ExcludeFromDocs]
	public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results)
	{
		return defaultPhysicsScene.CircleCast(origin, radius, direction, float.PositiveInfinity, results);
	}

	[ExcludeFromDocs]
	public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, float distance)
	{
		return defaultPhysicsScene.CircleCast(origin, radius, direction, distance, results);
	}

	[ExcludeFromDocs]
	public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return defaultPhysicsScene.CircleCast(origin, radius, direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return defaultPhysicsScene.CircleCast(origin, radius, direction, distance, contactFilter, results);
	}

	public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return defaultPhysicsScene.CircleCast(origin, radius, direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction)
	{
		return defaultPhysicsScene.BoxCast(origin, size, angle, direction, float.PositiveInfinity);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance)
	{
		return defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, contactFilter);
	}

	public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("Physics2D.AllLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static int BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
	{
		return defaultPhysicsScene.BoxCast(origin, size, angle, direction, float.PositiveInfinity, contactFilter, results);
	}

	public static int BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance)
	{
		return defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, contactFilter, results);
	}

	public static int BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity)
	{
		return defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return BoxCastAll_Internal(defaultPhysicsScene, origin, size, angle, direction, float.PositiveInfinity, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return BoxCastAll_Internal(defaultPhysicsScene, origin, size, angle, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return BoxCastAll_Internal(defaultPhysicsScene, origin, size, angle, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return BoxCastAll_Internal(defaultPhysicsScene, origin, size, angle, direction, distance, contactFilter);
	}

	public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return BoxCastAll_Internal(defaultPhysicsScene, origin, size, angle, direction, distance, contactFilter);
	}

	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("BoxCastAll_Binding")]
	private static RaycastHit2D[] BoxCastAll_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter)
	{
		return BoxCastAll_Internal_Injected(ref physicsScene, ref origin, ref size, angle, ref direction, distance, ref contactFilter);
	}

	[ExcludeFromDocs]
	public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results)
	{
		return defaultPhysicsScene.BoxCast(origin, size, angle, direction, float.PositiveInfinity, results);
	}

	[ExcludeFromDocs]
	public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, float distance)
	{
		return defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, results);
	}

	[ExcludeFromDocs]
	public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, contactFilter, results);
	}

	public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction)
	{
		return defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, float.PositiveInfinity);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance)
	{
		return defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, contactFilter);
	}

	public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static int CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
	{
		return defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, float.PositiveInfinity, contactFilter, results);
	}

	public static int CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance)
	{
		return defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, contactFilter, results);
	}

	public static int CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity)
	{
		return defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return CapsuleCastAll_Internal(defaultPhysicsScene, origin, size, capsuleDirection, angle, direction, float.PositiveInfinity, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return CapsuleCastAll_Internal(defaultPhysicsScene, origin, size, capsuleDirection, angle, direction, distance, contactFilter);
	}

	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("CapsuleCastAll_Binding")]
	private static RaycastHit2D[] CapsuleCastAll_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter)
	{
		return CapsuleCastAll_Internal_Injected(ref physicsScene, ref origin, ref size, capsuleDirection, angle, ref direction, distance, ref contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return CapsuleCastAll_Internal(defaultPhysicsScene, origin, size, capsuleDirection, angle, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return CapsuleCastAll_Internal(defaultPhysicsScene, origin, size, capsuleDirection, angle, direction, distance, contactFilter);
	}

	public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return CapsuleCastAll_Internal(defaultPhysicsScene, origin, size, capsuleDirection, angle, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, RaycastHit2D[] results)
	{
		return defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, float.PositiveInfinity, results);
	}

	[ExcludeFromDocs]
	public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, RaycastHit2D[] results, float distance)
	{
		return defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, results);
	}

	[ExcludeFromDocs]
	public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, contactFilter, results);
	}

	public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D GetRayIntersection(Ray ray)
	{
		return defaultPhysicsScene.GetRayIntersection(ray, float.PositiveInfinity);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D GetRayIntersection(Ray ray, float distance)
	{
		return defaultPhysicsScene.GetRayIntersection(ray, distance);
	}

	public static RaycastHit2D GetRayIntersection(Ray ray, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask)
	{
		return defaultPhysicsScene.GetRayIntersection(ray, distance, layerMask);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] GetRayIntersectionAll(Ray ray)
	{
		return GetRayIntersectionAll_Internal(defaultPhysicsScene, ray.origin, ray.direction, float.PositiveInfinity, -5);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] GetRayIntersectionAll(Ray ray, float distance)
	{
		return GetRayIntersectionAll_Internal(defaultPhysicsScene, ray.origin, ray.direction, distance, -5);
	}

	[RequiredByNativeCode]
	public static RaycastHit2D[] GetRayIntersectionAll(Ray ray, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask)
	{
		return GetRayIntersectionAll_Internal(defaultPhysicsScene, ray.origin, ray.direction, distance, layerMask);
	}

	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("GetRayIntersectionAll_Binding")]
	private static RaycastHit2D[] GetRayIntersectionAll_Internal(PhysicsScene2D physicsScene, Vector3 origin, Vector3 direction, float distance, int layerMask)
	{
		return GetRayIntersectionAll_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, layerMask);
	}

	[ExcludeFromDocs]
	public static int GetRayIntersectionNonAlloc(Ray ray, RaycastHit2D[] results)
	{
		return defaultPhysicsScene.GetRayIntersection(ray, float.PositiveInfinity, results);
	}

	[ExcludeFromDocs]
	public static int GetRayIntersectionNonAlloc(Ray ray, RaycastHit2D[] results, float distance)
	{
		return defaultPhysicsScene.GetRayIntersection(ray, distance, results);
	}

	[RequiredByNativeCode]
	public static int GetRayIntersectionNonAlloc(Ray ray, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask)
	{
		return defaultPhysicsScene.GetRayIntersection(ray, distance, results, layerMask);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapPoint(Vector2 point)
	{
		return defaultPhysicsScene.OverlapPoint(point);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapPoint(Vector2 point, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return defaultPhysicsScene.OverlapPoint(point, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapPoint(Vector2 point, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return defaultPhysicsScene.OverlapPoint(point, contactFilter);
	}

	public static Collider2D OverlapPoint(Vector2 point, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return defaultPhysicsScene.OverlapPoint(point, contactFilter);
	}

	public static int OverlapPoint(Vector2 point, ContactFilter2D contactFilter, [Unmarshalled] Collider2D[] results)
	{
		return defaultPhysicsScene.OverlapPoint(point, contactFilter, results);
	}

	public static int OverlapPoint(Vector2 point, ContactFilter2D contactFilter, List<Collider2D> results)
	{
		return defaultPhysicsScene.OverlapPoint(point, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapPointAll(Vector2 point)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapPointAll_Internal(defaultPhysicsScene, point, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapPointAll(Vector2 point, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapPointAll_Internal(defaultPhysicsScene, point, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapPointAll(Vector2 point, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return OverlapPointAll_Internal(defaultPhysicsScene, point, contactFilter);
	}

	public static Collider2D[] OverlapPointAll(Vector2 point, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return OverlapPointAll_Internal(defaultPhysicsScene, point, contactFilter);
	}

	[NativeMethod("OverlapPointAll_Binding")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static Collider2D[] OverlapPointAll_Internal(PhysicsScene2D physicsScene, Vector2 point, ContactFilter2D contactFilter)
	{
		return OverlapPointAll_Internal_Injected(ref physicsScene, ref point, ref contactFilter);
	}

	[ExcludeFromDocs]
	public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results)
	{
		return defaultPhysicsScene.OverlapPoint(point, results);
	}

	[ExcludeFromDocs]
	public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return defaultPhysicsScene.OverlapPoint(point, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return defaultPhysicsScene.OverlapPoint(point, contactFilter, results);
	}

	public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return defaultPhysicsScene.OverlapPoint(point, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapCircle(Vector2 point, float radius)
	{
		return defaultPhysicsScene.OverlapCircle(point, radius);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapCircle(Vector2 point, float radius, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return defaultPhysicsScene.OverlapCircle(point, radius, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapCircle(Vector2 point, float radius, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return defaultPhysicsScene.OverlapCircle(point, radius, contactFilter);
	}

	public static Collider2D OverlapCircle(Vector2 point, float radius, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return defaultPhysicsScene.OverlapCircle(point, radius, contactFilter);
	}

	public static int OverlapCircle(Vector2 point, float radius, ContactFilter2D contactFilter, Collider2D[] results)
	{
		return defaultPhysicsScene.OverlapCircle(point, radius, contactFilter, results);
	}

	public static int OverlapCircle(Vector2 point, float radius, ContactFilter2D contactFilter, List<Collider2D> results)
	{
		return defaultPhysicsScene.OverlapCircle(point, radius, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapCircleAll(Vector2 point, float radius)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapCircleAll_Internal(defaultPhysicsScene, point, radius, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapCircleAll(Vector2 point, float radius, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapCircleAll_Internal(defaultPhysicsScene, point, radius, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapCircleAll(Vector2 point, float radius, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return OverlapCircleAll_Internal(defaultPhysicsScene, point, radius, contactFilter);
	}

	public static Collider2D[] OverlapCircleAll(Vector2 point, float radius, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return OverlapCircleAll_Internal(defaultPhysicsScene, point, radius, contactFilter);
	}

	[NativeMethod("OverlapCircleAll_Binding")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static Collider2D[] OverlapCircleAll_Internal(PhysicsScene2D physicsScene, Vector2 point, float radius, ContactFilter2D contactFilter)
	{
		return OverlapCircleAll_Internal_Injected(ref physicsScene, ref point, radius, ref contactFilter);
	}

	[ExcludeFromDocs]
	public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results)
	{
		return defaultPhysicsScene.OverlapCircle(point, radius, results);
	}

	[ExcludeFromDocs]
	public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return defaultPhysicsScene.OverlapCircle(point, radius, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return defaultPhysicsScene.OverlapCircle(point, radius, contactFilter, results);
	}

	public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return defaultPhysicsScene.OverlapCircle(point, radius, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapBox(Vector2 point, Vector2 size, float angle)
	{
		return defaultPhysicsScene.OverlapBox(point, size, angle);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapBox(Vector2 point, Vector2 size, float angle, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return defaultPhysicsScene.OverlapBox(point, size, angle, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapBox(Vector2 point, Vector2 size, float angle, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return defaultPhysicsScene.OverlapBox(point, size, angle, contactFilter);
	}

	public static Collider2D OverlapBox(Vector2 point, Vector2 size, float angle, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return defaultPhysicsScene.OverlapBox(point, size, angle, contactFilter);
	}

	public static int OverlapBox(Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter, Collider2D[] results)
	{
		return defaultPhysicsScene.OverlapBox(point, size, angle, contactFilter, results);
	}

	public static int OverlapBox(Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter, List<Collider2D> results)
	{
		return defaultPhysicsScene.OverlapBox(point, size, angle, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapBoxAll(Vector2 point, Vector2 size, float angle)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapBoxAll_Internal(defaultPhysicsScene, point, size, angle, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapBoxAll(Vector2 point, Vector2 size, float angle, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapBoxAll_Internal(defaultPhysicsScene, point, size, angle, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapBoxAll(Vector2 point, Vector2 size, float angle, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return OverlapBoxAll_Internal(defaultPhysicsScene, point, size, angle, contactFilter);
	}

	public static Collider2D[] OverlapBoxAll(Vector2 point, Vector2 size, float angle, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return OverlapBoxAll_Internal(defaultPhysicsScene, point, size, angle, contactFilter);
	}

	[NativeMethod("OverlapBoxAll_Binding")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static Collider2D[] OverlapBoxAll_Internal(PhysicsScene2D physicsScene, Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter)
	{
		return OverlapBoxAll_Internal_Injected(ref physicsScene, ref point, ref size, angle, ref contactFilter);
	}

	[ExcludeFromDocs]
	public static int OverlapBoxNonAlloc(Vector2 point, Vector2 size, float angle, Collider2D[] results)
	{
		return defaultPhysicsScene.OverlapBox(point, size, angle, results);
	}

	[ExcludeFromDocs]
	public static int OverlapBoxNonAlloc(Vector2 point, Vector2 size, float angle, Collider2D[] results, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return defaultPhysicsScene.OverlapBox(point, size, angle, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int OverlapBoxNonAlloc(Vector2 point, Vector2 size, float angle, Collider2D[] results, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return defaultPhysicsScene.OverlapBox(point, size, angle, contactFilter, results);
	}

	public static int OverlapBoxNonAlloc(Vector2 point, Vector2 size, float angle, Collider2D[] results, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return defaultPhysicsScene.OverlapBox(point, size, angle, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB)
	{
		return defaultPhysicsScene.OverlapArea(pointA, pointB);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return defaultPhysicsScene.OverlapArea(pointA, pointB, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return defaultPhysicsScene.OverlapArea(pointA, pointB, contactFilter);
	}

	public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return defaultPhysicsScene.OverlapArea(pointA, pointB, contactFilter);
	}

	public static int OverlapArea(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter, Collider2D[] results)
	{
		return defaultPhysicsScene.OverlapArea(pointA, pointB, contactFilter, results);
	}

	public static int OverlapArea(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter, List<Collider2D> results)
	{
		return defaultPhysicsScene.OverlapArea(pointA, pointB, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB)
	{
		return OverlapAreaAllToBox_Internal(pointA, pointB, -5, float.NegativeInfinity, float.PositiveInfinity);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB, int layerMask)
	{
		return OverlapAreaAllToBox_Internal(pointA, pointB, layerMask, float.NegativeInfinity, float.PositiveInfinity);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB, int layerMask, float minDepth)
	{
		return OverlapAreaAllToBox_Internal(pointA, pointB, layerMask, minDepth, float.PositiveInfinity);
	}

	public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		return OverlapAreaAllToBox_Internal(pointA, pointB, layerMask, minDepth, maxDepth);
	}

	private static Collider2D[] OverlapAreaAllToBox_Internal(Vector2 pointA, Vector2 pointB, int layerMask, float minDepth, float maxDepth)
	{
		Vector2 point = (pointA + pointB) * 0.5f;
		Vector2 size = new Vector2(Mathf.Abs(pointA.x - pointB.x), Math.Abs(pointA.y - pointB.y));
		return OverlapBoxAll(point, size, 0f, layerMask, minDepth, maxDepth);
	}

	[ExcludeFromDocs]
	public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results)
	{
		return defaultPhysicsScene.OverlapArea(pointA, pointB, results);
	}

	[ExcludeFromDocs]
	public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return defaultPhysicsScene.OverlapArea(pointA, pointB, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return defaultPhysicsScene.OverlapArea(pointA, pointB, contactFilter, results);
	}

	public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return defaultPhysicsScene.OverlapArea(pointA, pointB, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle)
	{
		return defaultPhysicsScene.OverlapCapsule(point, size, direction, angle);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, contactFilter);
	}

	public static Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, contactFilter);
	}

	public static int OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter, Collider2D[] results)
	{
		return defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, contactFilter, results);
	}

	public static int OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter, List<Collider2D> results)
	{
		return defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapCapsuleAll(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapCapsuleAll_Internal(defaultPhysicsScene, point, size, direction, angle, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapCapsuleAll(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapCapsuleAll_Internal(defaultPhysicsScene, point, size, direction, angle, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapCapsuleAll(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return OverlapCapsuleAll_Internal(defaultPhysicsScene, point, size, direction, angle, contactFilter);
	}

	public static Collider2D[] OverlapCapsuleAll(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return OverlapCapsuleAll_Internal(defaultPhysicsScene, point, size, direction, angle, contactFilter);
	}

	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("OverlapCapsuleAll_Binding")]
	private static Collider2D[] OverlapCapsuleAll_Internal(PhysicsScene2D physicsScene, Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter)
	{
		return OverlapCapsuleAll_Internal_Injected(ref physicsScene, ref point, ref size, direction, angle, ref contactFilter);
	}

	[ExcludeFromDocs]
	public static int OverlapCapsuleNonAlloc(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, Collider2D[] results)
	{
		return defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, results);
	}

	[ExcludeFromDocs]
	public static int OverlapCapsuleNonAlloc(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, Collider2D[] results, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int OverlapCapsuleNonAlloc(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, Collider2D[] results, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, contactFilter, results);
	}

	public static int OverlapCapsuleNonAlloc(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, Collider2D[] results, [UnityEngine.Internal.DefaultValue("DefaultRaycastLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, contactFilter, results);
	}

	public static int OverlapCollider(Collider2D collider, ContactFilter2D contactFilter, Collider2D[] results)
	{
		return PhysicsScene2D.OverlapCollider(collider, contactFilter, results);
	}

	public static int OverlapCollider(Collider2D collider, ContactFilter2D contactFilter, List<Collider2D> results)
	{
		return PhysicsScene2D.OverlapCollider(collider, contactFilter, results);
	}

	public static int GetContacts(Collider2D collider1, Collider2D collider2, ContactFilter2D contactFilter, ContactPoint2D[] contacts)
	{
		return GetColliderColliderContactsArray(collider1, collider2, contactFilter, contacts);
	}

	public static int GetContacts(Collider2D collider, ContactPoint2D[] contacts)
	{
		return GetColliderContactsArray(collider, default(ContactFilter2D).NoFilter(), contacts);
	}

	public static int GetContacts(Collider2D collider, ContactFilter2D contactFilter, ContactPoint2D[] contacts)
	{
		return GetColliderContactsArray(collider, contactFilter, contacts);
	}

	public static int GetContacts(Collider2D collider, Collider2D[] colliders)
	{
		return GetColliderContactsCollidersOnlyArray(collider, default(ContactFilter2D).NoFilter(), colliders);
	}

	public static int GetContacts(Collider2D collider, ContactFilter2D contactFilter, Collider2D[] colliders)
	{
		return GetColliderContactsCollidersOnlyArray(collider, contactFilter, colliders);
	}

	public static int GetContacts(Rigidbody2D rigidbody, ContactPoint2D[] contacts)
	{
		return GetRigidbodyContactsArray(rigidbody, default(ContactFilter2D).NoFilter(), contacts);
	}

	public static int GetContacts(Rigidbody2D rigidbody, ContactFilter2D contactFilter, ContactPoint2D[] contacts)
	{
		return GetRigidbodyContactsArray(rigidbody, contactFilter, contacts);
	}

	public static int GetContacts(Rigidbody2D rigidbody, Collider2D[] colliders)
	{
		return GetRigidbodyContactsCollidersOnlyArray(rigidbody, default(ContactFilter2D).NoFilter(), colliders);
	}

	public static int GetContacts(Rigidbody2D rigidbody, ContactFilter2D contactFilter, Collider2D[] colliders)
	{
		return GetRigidbodyContactsCollidersOnlyArray(rigidbody, contactFilter, colliders);
	}

	[NativeMethod("GetColliderContactsArray_Binding")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static int GetColliderContactsArray([NotNull("ArgumentNullException")] Collider2D collider, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")][Unmarshalled] ContactPoint2D[] results)
	{
		return GetColliderContactsArray_Injected(collider, ref contactFilter, results);
	}

	[NativeMethod("GetColliderColliderContactsArray_Binding")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static int GetColliderColliderContactsArray([NotNull("ArgumentNullException")] Collider2D collider1, [NotNull("ArgumentNullException")] Collider2D collider2, ContactFilter2D contactFilter, [Unmarshalled][NotNull("ArgumentNullException")] ContactPoint2D[] results)
	{
		return GetColliderColliderContactsArray_Injected(collider1, collider2, ref contactFilter, results);
	}

	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("GetRigidbodyContactsArray_Binding")]
	private static int GetRigidbodyContactsArray([NotNull("ArgumentNullException")] Rigidbody2D rigidbody, ContactFilter2D contactFilter, [Unmarshalled][NotNull("ArgumentNullException")] ContactPoint2D[] results)
	{
		return GetRigidbodyContactsArray_Injected(rigidbody, ref contactFilter, results);
	}

	[NativeMethod("GetColliderContactsCollidersOnlyArray_Binding")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static int GetColliderContactsCollidersOnlyArray([NotNull("ArgumentNullException")] Collider2D collider, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")][Unmarshalled] Collider2D[] results)
	{
		return GetColliderContactsCollidersOnlyArray_Injected(collider, ref contactFilter, results);
	}

	[NativeMethod("GetRigidbodyContactsCollidersOnlyArray_Binding")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static int GetRigidbodyContactsCollidersOnlyArray([NotNull("ArgumentNullException")] Rigidbody2D rigidbody, ContactFilter2D contactFilter, [Unmarshalled][NotNull("ArgumentNullException")] Collider2D[] results)
	{
		return GetRigidbodyContactsCollidersOnlyArray_Injected(rigidbody, ref contactFilter, results);
	}

	public static int GetContacts(Collider2D collider1, Collider2D collider2, ContactFilter2D contactFilter, List<ContactPoint2D> contacts)
	{
		return GetColliderColliderContactsList(collider1, collider2, contactFilter, contacts);
	}

	public static int GetContacts(Collider2D collider, List<ContactPoint2D> contacts)
	{
		return GetColliderContactsList(collider, default(ContactFilter2D).NoFilter(), contacts);
	}

	public static int GetContacts(Collider2D collider, ContactFilter2D contactFilter, List<ContactPoint2D> contacts)
	{
		return GetColliderContactsList(collider, contactFilter, contacts);
	}

	public static int GetContacts(Collider2D collider, List<Collider2D> colliders)
	{
		return GetColliderContactsCollidersOnlyList(collider, default(ContactFilter2D).NoFilter(), colliders);
	}

	public static int GetContacts(Collider2D collider, ContactFilter2D contactFilter, List<Collider2D> colliders)
	{
		return GetColliderContactsCollidersOnlyList(collider, contactFilter, colliders);
	}

	public static int GetContacts(Rigidbody2D rigidbody, List<ContactPoint2D> contacts)
	{
		return GetRigidbodyContactsList(rigidbody, default(ContactFilter2D).NoFilter(), contacts);
	}

	public static int GetContacts(Rigidbody2D rigidbody, ContactFilter2D contactFilter, List<ContactPoint2D> contacts)
	{
		return GetRigidbodyContactsList(rigidbody, contactFilter, contacts);
	}

	public static int GetContacts(Rigidbody2D rigidbody, List<Collider2D> colliders)
	{
		return GetRigidbodyContactsCollidersOnlyList(rigidbody, default(ContactFilter2D).NoFilter(), colliders);
	}

	public static int GetContacts(Rigidbody2D rigidbody, ContactFilter2D contactFilter, List<Collider2D> colliders)
	{
		return GetRigidbodyContactsCollidersOnlyList(rigidbody, contactFilter, colliders);
	}

	[NativeMethod("GetColliderContactsList_Binding")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static int GetColliderContactsList([NotNull("ArgumentNullException")] Collider2D collider, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<ContactPoint2D> results)
	{
		return GetColliderContactsList_Injected(collider, ref contactFilter, results);
	}

	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("GetColliderColliderContactsList_Binding")]
	private static int GetColliderColliderContactsList([NotNull("ArgumentNullException")] Collider2D collider1, [NotNull("ArgumentNullException")] Collider2D collider2, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<ContactPoint2D> results)
	{
		return GetColliderColliderContactsList_Injected(collider1, collider2, ref contactFilter, results);
	}

	[NativeMethod("GetRigidbodyContactsList_Binding")]
	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	private static int GetRigidbodyContactsList([NotNull("ArgumentNullException")] Rigidbody2D rigidbody, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<ContactPoint2D> results)
	{
		return GetRigidbodyContactsList_Injected(rigidbody, ref contactFilter, results);
	}

	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("GetColliderContactsCollidersOnlyList_Binding")]
	private static int GetColliderContactsCollidersOnlyList([NotNull("ArgumentNullException")] Collider2D collider, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<Collider2D> results)
	{
		return GetColliderContactsCollidersOnlyList_Injected(collider, ref contactFilter, results);
	}

	[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
	[NativeMethod("GetRigidbodyContactsCollidersOnlyList_Binding")]
	private static int GetRigidbodyContactsCollidersOnlyList([NotNull("ArgumentNullException")] Rigidbody2D rigidbody, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<Collider2D> results)
	{
		return GetRigidbodyContactsCollidersOnlyList_Injected(rigidbody, ref contactFilter, results);
	}

	internal static void SetEditorDragMovement(bool dragging, GameObject[] objs)
	{
		foreach (Rigidbody2D item in m_LastDisabledRigidbody2D)
		{
			if (item != null)
			{
				item.SetDragBehaviour(dragged: false);
			}
		}
		m_LastDisabledRigidbody2D.Clear();
		if (!dragging)
		{
			return;
		}
		foreach (GameObject gameObject in objs)
		{
			Rigidbody2D[] componentsInChildren = gameObject.GetComponentsInChildren<Rigidbody2D>(includeInactive: false);
			Rigidbody2D[] array = componentsInChildren;
			foreach (Rigidbody2D rigidbody2D in array)
			{
				m_LastDisabledRigidbody2D.Add(rigidbody2D);
				rigidbody2D.SetDragBehaviour(dragged: true);
			}
		}
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void get_gravity_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void set_gravity_Injected(ref Vector2 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void get_jobOptions_Injected(out PhysicsJobOptions2D ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void set_jobOptions_Injected(ref PhysicsJobOptions2D value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool Simulate_Internal_Injected(ref PhysicsScene2D physicsScene, float step);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool IsTouching_TwoCollidersWithFilter_Injected([Writable] Collider2D collider1, [Writable] Collider2D collider2, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool IsTouching_SingleColliderWithFilter_Injected([Writable] Collider2D collider, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Distance_Internal_Injected([Writable] Collider2D colliderA, [Writable] Collider2D colliderB, out ColliderDistance2D ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void ClosestPoint_Collider_Injected(ref Vector2 position, Collider2D collider, out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void ClosestPoint_Rigidbody_Injected(ref Vector2 position, Rigidbody2D rigidbody, out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern RaycastHit2D[] LinecastAll_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 start, ref Vector2 end, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern RaycastHit2D[] RaycastAll_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern RaycastHit2D[] CircleCastAll_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, float radius, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern RaycastHit2D[] BoxCastAll_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, ref Vector2 size, float angle, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern RaycastHit2D[] CapsuleCastAll_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 origin, ref Vector2 size, CapsuleDirection2D capsuleDirection, float angle, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern RaycastHit2D[] GetRayIntersectionAll_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector3 origin, ref Vector3 direction, float distance, int layerMask);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern Collider2D[] OverlapPointAll_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern Collider2D[] OverlapCircleAll_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, float radius, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern Collider2D[] OverlapBoxAll_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, ref Vector2 size, float angle, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern Collider2D[] OverlapCapsuleAll_Internal_Injected(ref PhysicsScene2D physicsScene, ref Vector2 point, ref Vector2 size, CapsuleDirection2D direction, float angle, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int GetColliderContactsArray_Injected(Collider2D collider, ref ContactFilter2D contactFilter, ContactPoint2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int GetColliderColliderContactsArray_Injected(Collider2D collider1, Collider2D collider2, ref ContactFilter2D contactFilter, ContactPoint2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int GetRigidbodyContactsArray_Injected(Rigidbody2D rigidbody, ref ContactFilter2D contactFilter, ContactPoint2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int GetColliderContactsCollidersOnlyArray_Injected(Collider2D collider, ref ContactFilter2D contactFilter, Collider2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int GetRigidbodyContactsCollidersOnlyArray_Injected(Rigidbody2D rigidbody, ref ContactFilter2D contactFilter, Collider2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int GetColliderContactsList_Injected(Collider2D collider, ref ContactFilter2D contactFilter, List<ContactPoint2D> results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int GetColliderColliderContactsList_Injected(Collider2D collider1, Collider2D collider2, ref ContactFilter2D contactFilter, List<ContactPoint2D> results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int GetRigidbodyContactsList_Injected(Rigidbody2D rigidbody, ref ContactFilter2D contactFilter, List<ContactPoint2D> results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int GetColliderContactsCollidersOnlyList_Injected(Collider2D collider, ref ContactFilter2D contactFilter, List<Collider2D> results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int GetRigidbodyContactsCollidersOnlyList_Injected(Rigidbody2D rigidbody, ref ContactFilter2D contactFilter, List<Collider2D> results);
}
public enum SimulationMode2D
{
	FixedUpdate,
	Update,
	Script
}
public enum CapsuleDirection2D
{
	Vertical,
	Horizontal
}
[Flags]
public enum RigidbodyConstraints2D
{
	None = 0,
	FreezePositionX = 1,
	FreezePositionY = 2,
	FreezeRotation = 4,
	FreezePosition = 3,
	FreezeAll = 7
}
public enum RigidbodyInterpolation2D
{
	None,
	Interpolate,
	Extrapolate
}
public enum RigidbodySleepMode2D
{
	NeverSleep,
	StartAwake,
	StartAsleep
}
public enum CollisionDetectionMode2D
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Enum member CollisionDetectionMode2D.None has been deprecated. Use CollisionDetectionMode2D.Discrete instead (UnityUpgradable) -> Discrete", true)]
	None = 0,
	Discrete = 0,
	Continuous = 1
}
public enum RigidbodyType2D
{
	Dynamic,
	Kinematic,
	Static
}
public enum ForceMode2D
{
	Force,
	Impulse
}
public enum ColliderErrorState2D
{
	None,
	NoShapes,
	RemovedShapes
}
public enum JointLimitState2D
{
	Inactive,
	LowerLimit,
	UpperLimit,
	EqualLimits
}
public enum JointBreakAction2D
{
	Ignore,
	CallbackOnly,
	Disable,
	Destroy
}
public enum EffectorSelection2D
{
	Rigidbody,
	Collider
}
public enum EffectorForceMode2D
{
	Constant,
	InverseLinear,
	InverseSquared
}
public enum PhysicsShapeType2D
{
	Circle,
	Capsule,
	Polygon,
	Edges
}
[UsedByNativeCode]
[NativeHeader(Header = "Modules/Physics2D/Public/PhysicsScripting2D.h")]
public struct PhysicsShape2D
{
	private PhysicsShapeType2D m_ShapeType;

	private float m_Radius;

	private int m_VertexStartIndex;

	private int m_VertexCount;

	private int m_UseAdjacentStart;

	private int m_UseAdjacentEnd;

	private Vector2 m_AdjacentStart;

	private Vector2 m_AdjacentEnd;

	public PhysicsShapeType2D shapeType
	{
		get
		{
			return m_ShapeType;
		}
		set
		{
			m_ShapeType = value;
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
			if (value < 0f)
			{
				throw new ArgumentOutOfRangeException("radius cannot be negative.");
			}
			if (float.IsNaN(value) || float.IsInfinity(value))
			{
				throw new ArgumentException("radius contains an invalid value.");
			}
			m_Radius = value;
		}
	}

	public int vertexStartIndex
	{
		get
		{
			return m_VertexStartIndex;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("vertexStartIndex cannot be negative.");
			}
			m_VertexStartIndex = value;
		}
	}

	public int vertexCount
	{
		get
		{
			return m_VertexCount;
		}
		set
		{
			if (value < 1)
			{
				throw new ArgumentOutOfRangeException("vertexCount cannot be less than one.");
			}
			m_VertexCount = value;
		}
	}

	public bool useAdjacentStart
	{
		get
		{
			return m_UseAdjacentStart != 0;
		}
		set
		{
			m_UseAdjacentStart = (value ? 1 : 0);
		}
	}

	public bool useAdjacentEnd
	{
		get
		{
			return m_UseAdjacentEnd != 0;
		}
		set
		{
			m_UseAdjacentEnd = (value ? 1 : 0);
		}
	}

	public Vector2 adjacentStart
	{
		get
		{
			return m_AdjacentStart;
		}
		set
		{
			if (float.IsNaN(value.x) || float.IsNaN(value.y) || float.IsInfinity(value.x) || float.IsInfinity(value.y))
			{
				throw new ArgumentException("adjacentStart contains an invalid value.");
			}
			m_AdjacentStart = value;
		}
	}

	public Vector2 adjacentEnd
	{
		get
		{
			return m_AdjacentEnd;
		}
		set
		{
			if (float.IsNaN(value.x) || float.IsNaN(value.y) || float.IsInfinity(value.x) || float.IsInfinity(value.y))
			{
				throw new ArgumentException("adjacentEnd contains an invalid value.");
			}
			m_AdjacentEnd = value;
		}
	}
}
public class PhysicsShapeGroup2D
{
	[NativeHeader(Header = "Modules/Physics2D/Public/PhysicsScripting2D.h")]
	internal struct GroupState
	{
		[NativeName("shapesList")]
		public List<PhysicsShape2D> m_Shapes;

		[NativeName("verticesList")]
		public List<Vector2> m_Vertices;

		[NativeName("localToWorld")]
		public Matrix4x4 m_LocalToWorld;

		public void ClearGeometry()
		{
			m_Shapes.Clear();
			m_Vertices.Clear();
		}
	}

	internal GroupState m_GroupState;

	private const float MinVertexSeparation = 0.0025f;

	internal List<Vector2> groupVertices => m_GroupState.m_Vertices;

	internal List<PhysicsShape2D> groupShapes => m_GroupState.m_Shapes;

	public int shapeCount => m_GroupState.m_Shapes.Count;

	public int vertexCount => m_GroupState.m_Vertices.Count;

	public Matrix4x4 localToWorldMatrix
	{
		get
		{
			return m_GroupState.m_LocalToWorld;
		}
		set
		{
			m_GroupState.m_LocalToWorld = value;
		}
	}

	public PhysicsShapeGroup2D([UnityEngine.Internal.DefaultValue("1")] int shapeCapacity = 1, [UnityEngine.Internal.DefaultValue("8")] int vertexCapacity = 8)
	{
		m_GroupState = new GroupState
		{
			m_Shapes = new List<PhysicsShape2D>(shapeCapacity),
			m_Vertices = new List<Vector2>(vertexCapacity),
			m_LocalToWorld = Matrix4x4.identity
		};
	}

	public void Clear()
	{
		m_GroupState.ClearGeometry();
		m_GroupState.m_LocalToWorld = Matrix4x4.identity;
	}

	public void Add(PhysicsShapeGroup2D physicsShapeGroup)
	{
		if (physicsShapeGroup == null)
		{
			throw new ArgumentNullException("Cannot merge a NULL PhysicsShapeGroup2D.");
		}
		if (physicsShapeGroup == this)
		{
			throw new ArgumentException("Cannot merge a PhysicsShapeGroup2D with itself.");
		}
		if (physicsShapeGroup.shapeCount == 0)
		{
			return;
		}
		int count = groupShapes.Count;
		int count2 = m_GroupState.m_Vertices.Count;
		groupShapes.AddRange(physicsShapeGroup.groupShapes);
		groupVertices.AddRange(physicsShapeGroup.groupVertices);
		if (count > 0)
		{
			for (int i = count; i < m_GroupState.m_Shapes.Count; i++)
			{
				PhysicsShape2D value = m_GroupState.m_Shapes[i];
				value.vertexStartIndex += count2;
				m_GroupState.m_Shapes[i] = value;
			}
		}
	}

	public void GetShapeData(List<PhysicsShape2D> shapes, List<Vector2> vertices)
	{
		shapes.AddRange(groupShapes);
		vertices.AddRange(groupVertices);
	}

	public void GetShapeData(NativeArray<PhysicsShape2D> shapes, NativeArray<Vector2> vertices)
	{
		if (!shapes.IsCreated || shapes.Length != shapeCount)
		{
			throw new ArgumentException($"Cannot get shape data as the native shapes array length must be identical to the current custom shape count of {shapeCount}.", "shapes");
		}
		if (!vertices.IsCreated || vertices.Length != vertexCount)
		{
			throw new ArgumentException($"Cannot get shape data as the native vertices array length must be identical to the current custom vertex count of {shapeCount}.", "vertices");
		}
		for (int i = 0; i < shapeCount; i++)
		{
			shapes[i] = m_GroupState.m_Shapes[i];
		}
		for (int j = 0; j < vertexCount; j++)
		{
			vertices[j] = m_GroupState.m_Vertices[j];
		}
	}

	public void GetShapeVertices(int shapeIndex, List<Vector2> vertices)
	{
		PhysicsShape2D shape = GetShape(shapeIndex);
		int num = shape.vertexCount;
		vertices.Clear();
		if (vertices.Capacity < num)
		{
			vertices.Capacity = num;
		}
		List<Vector2> list = groupVertices;
		int vertexStartIndex = shape.vertexStartIndex;
		for (int i = 0; i < num; i++)
		{
			vertices.Add(list[vertexStartIndex++]);
		}
	}

	public Vector2 GetShapeVertex(int shapeIndex, int vertexIndex)
	{
		int num = GetShape(shapeIndex).vertexStartIndex + vertexIndex;
		if (num < 0 || num >= groupVertices.Count)
		{
			throw new ArgumentOutOfRangeException($"Cannot get shape-vertex at index {num}. There are {shapeCount} shape-vertices.");
		}
		return groupVertices[num];
	}

	public void SetShapeVertex(int shapeIndex, int vertexIndex, Vector2 vertex)
	{
		int num = GetShape(shapeIndex).vertexStartIndex + vertexIndex;
		if (num < 0 || num >= groupVertices.Count)
		{
			throw new ArgumentOutOfRangeException($"Cannot set shape-vertex at index {num}. There are {shapeCount} shape-vertices.");
		}
		groupVertices[num] = vertex;
	}

	public void SetShapeRadius(int shapeIndex, float radius)
	{
		PhysicsShape2D shape = GetShape(shapeIndex);
		switch (shape.shapeType)
		{
		case PhysicsShapeType2D.Circle:
			if (radius <= 0f)
			{
				throw new ArgumentException($"Circle radius {radius} must be greater than zero.");
			}
			break;
		case PhysicsShapeType2D.Capsule:
			if (radius <= 1E-05f)
			{
				throw new ArgumentException($"Capsule radius: {radius} is too small.");
			}
			break;
		case PhysicsShapeType2D.Polygon:
		case PhysicsShapeType2D.Edges:
			radius = Mathf.Max(0f, radius);
			break;
		}
		shape.radius = radius;
		groupShapes[shapeIndex] = shape;
	}

	public void SetShapeAdjacentVertices(int shapeIndex, bool useAdjacentStart, bool useAdjacentEnd, Vector2 adjacentStart, Vector2 adjacentEnd)
	{
		if (shapeIndex < 0 || shapeIndex >= shapeCount)
		{
			throw new ArgumentOutOfRangeException($"Cannot set shape adjacent vertices at index {shapeIndex}. There are {shapeCount} shapes(s).");
		}
		PhysicsShape2D value = groupShapes[shapeIndex];
		if (value.shapeType != PhysicsShapeType2D.Edges)
		{
			throw new InvalidOperationException($"Cannot set shape adjacent vertices at index {shapeIndex}. The shape must be of type {PhysicsShapeType2D.Edges} but it is of typee {value.shapeType}.");
		}
		value.useAdjacentStart = useAdjacentStart;
		value.useAdjacentEnd = useAdjacentEnd;
		value.adjacentStart = adjacentStart;
		value.adjacentEnd = adjacentEnd;
		groupShapes[shapeIndex] = value;
	}

	public void DeleteShape(int shapeIndex)
	{
		if (shapeIndex < 0 || shapeIndex >= shapeCount)
		{
			throw new ArgumentOutOfRangeException($"Cannot delete shape at index {shapeIndex}. There are {shapeCount} shapes(s).");
		}
		PhysicsShape2D physicsShape2D = groupShapes[shapeIndex];
		int num = physicsShape2D.vertexCount;
		groupShapes.RemoveAt(shapeIndex);
		groupVertices.RemoveRange(physicsShape2D.vertexStartIndex, num);
		while (shapeIndex < groupShapes.Count)
		{
			PhysicsShape2D value = m_GroupState.m_Shapes[shapeIndex];
			value.vertexStartIndex -= num;
			m_GroupState.m_Shapes[shapeIndex++] = value;
		}
	}

	public PhysicsShape2D GetShape(int shapeIndex)
	{
		if (shapeIndex < 0 || shapeIndex >= shapeCount)
		{
			throw new ArgumentOutOfRangeException($"Cannot get shape at index {shapeIndex}. There are {shapeCount} shapes(s).");
		}
		return groupShapes[shapeIndex];
	}

	public int AddCircle(Vector2 center, float radius)
	{
		if (radius <= 0f)
		{
			throw new ArgumentException($"radius {radius} must be greater than zero.");
		}
		int count = groupVertices.Count;
		groupVertices.Add(center);
		groupShapes.Add(new PhysicsShape2D
		{
			shapeType = PhysicsShapeType2D.Circle,
			radius = radius,
			vertexStartIndex = count,
			vertexCount = 1
		});
		return groupShapes.Count - 1;
	}

	public int AddCapsule(Vector2 vertex0, Vector2 vertex1, float radius)
	{
		if (radius <= 1E-05f)
		{
			throw new ArgumentException($"radius: {radius} is too small.");
		}
		int count = groupVertices.Count;
		groupVertices.Add(vertex0);
		groupVertices.Add(vertex1);
		groupShapes.Add(new PhysicsShape2D
		{
			shapeType = PhysicsShapeType2D.Capsule,
			radius = radius,
			vertexStartIndex = count,
			vertexCount = 2
		});
		return groupShapes.Count - 1;
	}

	public int AddBox(Vector2 center, Vector2 size, [UnityEngine.Internal.DefaultValue("0f")] float angle = 0f, [UnityEngine.Internal.DefaultValue("0f")] float edgeRadius = 0f)
	{
		if (size.x <= 0.0025f || size.y <= 0.0025f)
		{
			throw new ArgumentException($"size: {size} is too small. Vertex need to be separated by at least {0.0025f}");
		}
		edgeRadius = Mathf.Max(0f, edgeRadius);
		angle *= MathF.PI / 180f;
		float cos = Mathf.Cos(angle);
		float sin = Mathf.Sin(angle);
		Vector2 vector = size * 0.5f;
		Vector2 item = center + Rotate(cos, sin, -vector);
		Vector2 item2 = center + Rotate(cos, sin, new Vector2(vector.x, 0f - vector.y));
		Vector2 item3 = center + Rotate(cos, sin, vector);
		Vector2 item4 = center + Rotate(cos, sin, new Vector2(0f - vector.x, vector.y));
		int count = groupVertices.Count;
		groupVertices.Add(item);
		groupVertices.Add(item2);
		groupVertices.Add(item3);
		groupVertices.Add(item4);
		groupShapes.Add(new PhysicsShape2D
		{
			shapeType = PhysicsShapeType2D.Polygon,
			radius = edgeRadius,
			vertexStartIndex = count,
			vertexCount = 4
		});
		return groupShapes.Count - 1;
		static Vector2 Rotate(float num, float num2, Vector2 value)
		{
			return new Vector2(num * value.x - num2 * value.y, num2 * value.x + num * value.y);
		}
	}

	public int AddPolygon(List<Vector2> vertices)
	{
		int count = vertices.Count;
		if (count < 3 || count > 8)
		{
			throw new ArgumentException($"Vertex Count {count} must be >= 3 and <= {8}.");
		}
		float num = 6.25E-06f;
		for (int i = 1; i < count; i++)
		{
			Vector2 vector = vertices[i - 1];
			Vector2 vector2 = vertices[i];
			if ((vector2 - vector).sqrMagnitude <= num)
			{
				throw new ArgumentException($"vertices: {vector} and {vector2} are too close. Vertices need to be separated by at least {num}");
			}
		}
		int count2 = groupVertices.Count;
		groupVertices.AddRange(vertices);
		groupShapes.Add(new PhysicsShape2D
		{
			shapeType = PhysicsShapeType2D.Polygon,
			radius = 0f,
			vertexStartIndex = count2,
			vertexCount = count
		});
		return groupShapes.Count - 1;
	}

	public int AddEdges(List<Vector2> vertices, [UnityEngine.Internal.DefaultValue("0f")] float edgeRadius = 0f)
	{
		return AddEdges(vertices, useAdjacentStart: false, useAdjacentEnd: false, Vector2.zero, Vector2.zero, edgeRadius);
	}

	public int AddEdges(List<Vector2> vertices, bool useAdjacentStart, bool useAdjacentEnd, Vector2 adjacentStart, Vector2 adjacentEnd, [UnityEngine.Internal.DefaultValue("0f")] float edgeRadius = 0f)
	{
		int count = vertices.Count;
		if (count < 2)
		{
			throw new ArgumentOutOfRangeException($"Vertex Count {count} must be >= 2.");
		}
		edgeRadius = Mathf.Max(0f, edgeRadius);
		int count2 = groupVertices.Count;
		groupVertices.AddRange(vertices);
		groupShapes.Add(new PhysicsShape2D
		{
			shapeType = PhysicsShapeType2D.Edges,
			radius = edgeRadius,
			vertexStartIndex = count2,
			vertexCount = count,
			useAdjacentStart = useAdjacentStart,
			useAdjacentEnd = useAdjacentEnd,
			adjacentStart = adjacentStart,
			adjacentEnd = adjacentEnd
		});
		return groupShapes.Count - 1;
	}
}
public struct ColliderDistance2D
{
	private Vector2 m_PointA;

	private Vector2 m_PointB;

	private Vector2 m_Normal;

	private float m_Distance;

	private int m_IsValid;

	public Vector2 pointA
	{
		get
		{
			return m_PointA;
		}
		set
		{
			m_PointA = value;
		}
	}

	public Vector2 pointB
	{
		get
		{
			return m_PointB;
		}
		set
		{
			m_PointB = value;
		}
	}

	public Vector2 normal => m_Normal;

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

	public bool isOverlapped => m_Distance < 0f;

	public bool isValid
	{
		get
		{
			return m_IsValid != 0;
		}
		set
		{
			m_IsValid = (value ? 1 : 0);
		}
	}
}
[Serializable]
[NativeClass("ContactFilter", "struct ContactFilter;")]
[NativeHeader("Modules/Physics2D/Public/Collider2D.h")]
[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
public struct ContactFilter2D
{
	[NativeName("m_UseTriggers")]
	public bool useTriggers;

	[NativeName("m_UseLayerMask")]
	public bool useLayerMask;

	[NativeName("m_UseDepth")]
	public bool useDepth;

	[NativeName("m_UseOutsideDepth")]
	public bool useOutsideDepth;

	[NativeName("m_UseNormalAngle")]
	public bool useNormalAngle;

	[NativeName("m_UseOutsideNormalAngle")]
	public bool useOutsideNormalAngle;

	[NativeName("m_LayerMask")]
	public LayerMask layerMask;

	[NativeName("m_MinDepth")]
	public float minDepth;

	[NativeName("m_MaxDepth")]
	public float maxDepth;

	[NativeName("m_MinNormalAngle")]
	public float minNormalAngle;

	[NativeName("m_MaxNormalAngle")]
	public float maxNormalAngle;

	public const float NormalAngleUpperLimit = 359.9999f;

	public bool isFiltering => !useTriggers || useLayerMask || useDepth || useNormalAngle;

	public ContactFilter2D NoFilter()
	{
		useTriggers = true;
		useLayerMask = false;
		layerMask = -1;
		useDepth = false;
		useOutsideDepth = false;
		minDepth = float.NegativeInfinity;
		maxDepth = float.PositiveInfinity;
		useNormalAngle = false;
		useOutsideNormalAngle = false;
		minNormalAngle = 0f;
		maxNormalAngle = 359.9999f;
		return this;
	}

	private void CheckConsistency()
	{
		CheckConsistency_Injected(ref this);
	}

	public void ClearLayerMask()
	{
		useLayerMask = false;
	}

	public void SetLayerMask(LayerMask layerMask)
	{
		this.layerMask = layerMask;
		useLayerMask = true;
	}

	public void ClearDepth()
	{
		useDepth = false;
	}

	public void SetDepth(float minDepth, float maxDepth)
	{
		this.minDepth = minDepth;
		this.maxDepth = maxDepth;
		useDepth = true;
		CheckConsistency();
	}

	public void ClearNormalAngle()
	{
		useNormalAngle = false;
	}

	public void SetNormalAngle(float minNormalAngle, float maxNormalAngle)
	{
		this.minNormalAngle = minNormalAngle;
		this.maxNormalAngle = maxNormalAngle;
		useNormalAngle = true;
		CheckConsistency();
	}

	public bool IsFilteringTrigger([Writable] Collider2D collider)
	{
		return !useTriggers && collider.isTrigger;
	}

	public bool IsFilteringLayerMask(GameObject obj)
	{
		return useLayerMask && ((int)layerMask & (1 << obj.layer)) == 0;
	}

	public bool IsFilteringDepth(GameObject obj)
	{
		if (!useDepth)
		{
			return false;
		}
		if (minDepth > maxDepth)
		{
			float num = minDepth;
			minDepth = maxDepth;
			maxDepth = num;
		}
		float z = obj.transform.position.z;
		bool flag = z < minDepth || z > maxDepth;
		if (useOutsideDepth)
		{
			return !flag;
		}
		return flag;
	}

	public bool IsFilteringNormalAngle(Vector2 normal)
	{
		return IsFilteringNormalAngle_Injected(ref this, ref normal);
	}

	public bool IsFilteringNormalAngle(float angle)
	{
		return IsFilteringNormalAngleUsingAngle(angle);
	}

	private bool IsFilteringNormalAngleUsingAngle(float angle)
	{
		return IsFilteringNormalAngleUsingAngle_Injected(ref this, angle);
	}

	internal static ContactFilter2D CreateLegacyFilter(int layerMask, float minDepth, float maxDepth)
	{
		ContactFilter2D result = default(ContactFilter2D);
		result.useTriggers = Physics2D.queriesHitTriggers;
		result.SetLayerMask(layerMask);
		result.SetDepth(minDepth, maxDepth);
		return result;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void CheckConsistency_Injected(ref ContactFilter2D _unity_self);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool IsFilteringNormalAngle_Injected(ref ContactFilter2D _unity_self, ref Vector2 normal);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool IsFilteringNormalAngleUsingAngle_Injected(ref ContactFilter2D _unity_self, float angle);
}
[StructLayout(LayoutKind.Sequential)]
[RequiredByNativeCode]
public class Collision2D
{
	internal int m_Collider;

	internal int m_OtherCollider;

	internal int m_Rigidbody;

	internal int m_OtherRigidbody;

	internal Vector2 m_RelativeVelocity;

	internal int m_Enabled;

	internal int m_ContactCount;

	internal ContactPoint2D[] m_ReusedContacts;

	internal ContactPoint2D[] m_LegacyContacts;

	public Collider2D collider => Object.FindObjectFromInstanceID(m_Collider) as Collider2D;

	public Collider2D otherCollider => Object.FindObjectFromInstanceID(m_OtherCollider) as Collider2D;

	public Rigidbody2D rigidbody => Object.FindObjectFromInstanceID(m_Rigidbody) as Rigidbody2D;

	public Rigidbody2D otherRigidbody => Object.FindObjectFromInstanceID(m_OtherRigidbody) as Rigidbody2D;

	public Transform transform => (rigidbody != null) ? rigidbody.transform : collider.transform;

	public GameObject gameObject => (rigidbody != null) ? rigidbody.gameObject : collider.gameObject;

	public Vector2 relativeVelocity => m_RelativeVelocity;

	public bool enabled => m_Enabled == 1;

	public ContactPoint2D[] contacts
	{
		get
		{
			if (m_LegacyContacts == null)
			{
				m_LegacyContacts = new ContactPoint2D[m_ContactCount];
				Array.Copy(m_ReusedContacts, m_LegacyContacts, m_ContactCount);
			}
			return m_LegacyContacts;
		}
	}

	public int contactCount => m_ContactCount;

	private ContactPoint2D[] GetContacts_Internal()
	{
		return (m_LegacyContacts == null) ? m_ReusedContacts : m_LegacyContacts;
	}

	public ContactPoint2D GetContact(int index)
	{
		if (index < 0 || index >= m_ContactCount)
		{
			throw new ArgumentOutOfRangeException($"Cannot get contact at index {index}. There are {m_ContactCount} contact(s).");
		}
		return GetContacts_Internal()[index];
	}

	public int GetContacts(ContactPoint2D[] contacts)
	{
		if (contacts == null)
		{
			throw new NullReferenceException("Cannot get contacts as the provided array is NULL.");
		}
		int num = Mathf.Min(m_ContactCount, contacts.Length);
		Array.Copy(GetContacts_Internal(), contacts, num);
		return num;
	}

	public int GetContacts(List<ContactPoint2D> contacts)
	{
		if (contacts == null)
		{
			throw new NullReferenceException("Cannot get contacts as the provided list is NULL.");
		}
		contacts.Clear();
		ContactPoint2D[] contacts_Internal = GetContacts_Internal();
		for (int i = 0; i < m_ContactCount; i++)
		{
			contacts.Add(contacts_Internal[i]);
		}
		return m_ContactCount;
	}
}
[NativeHeader("Modules/Physics2D/Public/PhysicsScripting2D.h")]
[NativeClass("ScriptingContactPoint2D", "struct ScriptingContactPoint2D;")]
[RequiredByNativeCode(Optional = false, GenerateProxy = true)]
public struct ContactPoint2D
{
	[NativeName("point")]
	private Vector2 m_Point;

	[NativeName("normal")]
	private Vector2 m_Normal;

	[NativeName("relativeVelocity")]
	private Vector2 m_RelativeVelocity;

	[NativeName("separation")]
	private float m_Separation;

	[NativeName("normalImpulse")]
	private float m_NormalImpulse;

	[NativeName("tangentImpulse")]
	private float m_TangentImpulse;

	[NativeName("collider")]
	private int m_Collider;

	[NativeName("otherCollider")]
	private int m_OtherCollider;

	[NativeName("rigidbody")]
	private int m_Rigidbody;

	[NativeName("otherRigidbody")]
	private int m_OtherRigidbody;

	[NativeName("enabled")]
	private int m_Enabled;

	public Vector2 point => m_Point;

	public Vector2 normal => m_Normal;

	public float separation => m_Separation;

	public float normalImpulse => m_NormalImpulse;

	public float tangentImpulse => m_TangentImpulse;

	public Vector2 relativeVelocity => m_RelativeVelocity;

	public Collider2D collider => Object.FindObjectFromInstanceID(m_Collider) as Collider2D;

	public Collider2D otherCollider => Object.FindObjectFromInstanceID(m_OtherCollider) as Collider2D;

	public Rigidbody2D rigidbody => Object.FindObjectFromInstanceID(m_Rigidbody) as Rigidbody2D;

	public Rigidbody2D otherRigidbody => Object.FindObjectFromInstanceID(m_OtherRigidbody) as Rigidbody2D;

	public bool enabled => m_Enabled == 1;
}
public struct JointAngleLimits2D
{
	private float m_LowerAngle;

	private float m_UpperAngle;

	public float min
	{
		get
		{
			return m_LowerAngle;
		}
		set
		{
			m_LowerAngle = value;
		}
	}

	public float max
	{
		get
		{
			return m_UpperAngle;
		}
		set
		{
			m_UpperAngle = value;
		}
	}
}
public struct JointTranslationLimits2D
{
	private float m_LowerTranslation;

	private float m_UpperTranslation;

	public float min
	{
		get
		{
			return m_LowerTranslation;
		}
		set
		{
			m_LowerTranslation = value;
		}
	}

	public float max
	{
		get
		{
			return m_UpperTranslation;
		}
		set
		{
			m_UpperTranslation = value;
		}
	}
}
public struct JointMotor2D
{
	private float m_MotorSpeed;

	private float m_MaximumMotorTorque;

	public float motorSpeed
	{
		get
		{
			return m_MotorSpeed;
		}
		set
		{
			m_MotorSpeed = value;
		}
	}

	public float maxMotorTorque
	{
		get
		{
			return m_MaximumMotorTorque;
		}
		set
		{
			m_MaximumMotorTorque = value;
		}
	}
}
public struct JointSuspension2D
{
	private float m_DampingRatio;

	private float m_Frequency;

	private float m_Angle;

	public float dampingRatio
	{
		get
		{
			return m_DampingRatio;
		}
		set
		{
			m_DampingRatio = value;
		}
	}

	public float frequency
	{
		get
		{
			return m_Frequency;
		}
		set
		{
			m_Frequency = value;
		}
	}

	public float angle
	{
		get
		{
			return m_Angle;
		}
		set
		{
			m_Angle = value;
		}
	}
}
[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
[NativeClass("RaycastHit2D", "struct RaycastHit2D;")]
[NativeHeader("Runtime/Interfaces/IPhysics2D.h")]
public struct RaycastHit2D
{
	[NativeName("centroid")]
	private Vector2 m_Centroid;

	[NativeName("point")]
	private Vector2 m_Point;

	[NativeName("normal")]
	private Vector2 m_Normal;

	[NativeName("distance")]
	private float m_Distance;

	[NativeName("fraction")]
	private float m_Fraction;

	[NativeName("collider")]
	private int m_Collider;

	public Vector2 centroid
	{
		get
		{
			return m_Centroid;
		}
		set
		{
			m_Centroid = value;
		}
	}

	public Vector2 point
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

	public Vector2 normal
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

	public float fraction
	{
		get
		{
			return m_Fraction;
		}
		set
		{
			m_Fraction = value;
		}
	}

	public Collider2D collider => Object.FindObjectFromInstanceID(m_Collider) as Collider2D;

	public Rigidbody2D rigidbody => (collider != null) ? collider.attachedRigidbody : null;

	public Transform transform
	{
		get
		{
			Rigidbody2D rigidbody2D = rigidbody;
			if (rigidbody2D != null)
			{
				return rigidbody2D.transform;
			}
			if (collider != null)
			{
				return collider.transform;
			}
			return null;
		}
	}

	public static implicit operator bool(RaycastHit2D hit)
	{
		return hit.collider != null;
	}

	public int CompareTo(RaycastHit2D other)
	{
		if (collider == null)
		{
			return 1;
		}
		if (other.collider == null)
		{
			return -1;
		}
		return fraction.CompareTo(other.fraction);
	}
}
[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
[NativeClass("PhysicsJobOptions2D", "struct PhysicsJobOptions2D;")]
[NativeHeader("Modules/Physics2D/Public/Physics2DSettings.h")]
public struct PhysicsJobOptions2D
{
	private bool m_UseMultithreading;

	private bool m_UseConsistencySorting;

	private int m_InterpolationPosesPerJob;

	private int m_NewContactsPerJob;

	private int m_CollideContactsPerJob;

	private int m_ClearFlagsPerJob;

	private int m_ClearBodyForcesPerJob;

	private int m_SyncDiscreteFixturesPerJob;

	private int m_SyncContinuousFixturesPerJob;

	private int m_FindNearestContactsPerJob;

	private int m_UpdateTriggerContactsPerJob;

	private int m_IslandSolverCostThreshold;

	private int m_IslandSolverBodyCostScale;

	private int m_IslandSolverContactCostScale;

	private int m_IslandSolverJointCostScale;

	private int m_IslandSolverBodiesPerJob;

	private int m_IslandSolverContactsPerJob;

	public bool useMultithreading
	{
		get
		{
			return m_UseMultithreading;
		}
		set
		{
			m_UseMultithreading = value;
		}
	}

	public bool useConsistencySorting
	{
		get
		{
			return m_UseConsistencySorting;
		}
		set
		{
			m_UseConsistencySorting = value;
		}
	}

	public int interpolationPosesPerJob
	{
		get
		{
			return m_InterpolationPosesPerJob;
		}
		set
		{
			m_InterpolationPosesPerJob = value;
		}
	}

	public int newContactsPerJob
	{
		get
		{
			return m_NewContactsPerJob;
		}
		set
		{
			m_NewContactsPerJob = value;
		}
	}

	public int collideContactsPerJob
	{
		get
		{
			return m_CollideContactsPerJob;
		}
		set
		{
			m_CollideContactsPerJob = value;
		}
	}

	public int clearFlagsPerJob
	{
		get
		{
			return m_ClearFlagsPerJob;
		}
		set
		{
			m_ClearFlagsPerJob = value;
		}
	}

	public int clearBodyForcesPerJob
	{
		get
		{
			return m_ClearBodyForcesPerJob;
		}
		set
		{
			m_ClearBodyForcesPerJob = value;
		}
	}

	public int syncDiscreteFixturesPerJob
	{
		get
		{
			return m_SyncDiscreteFixturesPerJob;
		}
		set
		{
			m_SyncDiscreteFixturesPerJob = value;
		}
	}

	public int syncContinuousFixturesPerJob
	{
		get
		{
			return m_SyncContinuousFixturesPerJob;
		}
		set
		{
			m_SyncContinuousFixturesPerJob = value;
		}
	}

	public int findNearestContactsPerJob
	{
		get
		{
			return m_FindNearestContactsPerJob;
		}
		set
		{
			m_FindNearestContactsPerJob = value;
		}
	}

	public int updateTriggerContactsPerJob
	{
		get
		{
			return m_UpdateTriggerContactsPerJob;
		}
		set
		{
			m_UpdateTriggerContactsPerJob = value;
		}
	}

	public int islandSolverCostThreshold
	{
		get
		{
			return m_IslandSolverCostThreshold;
		}
		set
		{
			m_IslandSolverCostThreshold = value;
		}
	}

	public int islandSolverBodyCostScale
	{
		get
		{
			return m_IslandSolverBodyCostScale;
		}
		set
		{
			m_IslandSolverBodyCostScale = value;
		}
	}

	public int islandSolverContactCostScale
	{
		get
		{
			return m_IslandSolverContactCostScale;
		}
		set
		{
			m_IslandSolverContactCostScale = value;
		}
	}

	public int islandSolverJointCostScale
	{
		get
		{
			return m_IslandSolverJointCostScale;
		}
		set
		{
			m_IslandSolverJointCostScale = value;
		}
	}

	public int islandSolverBodiesPerJob
	{
		get
		{
			return m_IslandSolverBodiesPerJob;
		}
		set
		{
			m_IslandSolverBodiesPerJob = value;
		}
	}

	public int islandSolverContactsPerJob
	{
		get
		{
			return m_IslandSolverContactsPerJob;
		}
		set
		{
			m_IslandSolverContactsPerJob = value;
		}
	}
}
[RequireComponent(typeof(Transform))]
[NativeHeader("Modules/Physics2D/Public/Rigidbody2D.h")]
public sealed class Rigidbody2D : Component
{
	public Vector2 position
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

	public extern float rotation
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public Vector2 velocity
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

	public extern float angularVelocity
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool useAutoMass
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

	[NativeMethod("Material")]
	public extern PhysicsMaterial2D sharedMaterial
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public Vector2 centerOfMass
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

	public Vector2 worldCenterOfMass
	{
		get
		{
			get_worldCenterOfMass_Injected(out var ret);
			return ret;
		}
	}

	public extern float inertia
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
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

	public extern float gravityScale
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern RigidbodyType2D bodyType
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("SetBodyType_Binding")]
		set;
	}

	public extern bool useFullKinematicContacts
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public bool isKinematic
	{
		get
		{
			return bodyType == RigidbodyType2D.Kinematic;
		}
		set
		{
			bodyType = (value ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic);
		}
	}

	[Obsolete("'fixedAngle' is no longer supported. Use constraints instead.", false)]
	[NativeMethod("FreezeRotation")]
	public extern bool fixedAngle
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

	public extern RigidbodyConstraints2D constraints
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool simulated
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("SetSimulated_Binding")]
		set;
	}

	public extern RigidbodyInterpolation2D interpolation
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern RigidbodySleepMode2D sleepMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern CollisionDetectionMode2D collisionDetectionMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern int attachedColliderCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public Vector2 totalForce
	{
		get
		{
			get_totalForce_Injected(out var ret);
			return ret;
		}
		set
		{
			set_totalForce_Injected(ref value);
		}
	}

	public extern float totalTorque
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

	public void SetRotation(float angle)
	{
		SetRotation_Angle(angle);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("SetRotation")]
	private extern void SetRotation_Angle(float angle);

	public void SetRotation(Quaternion rotation)
	{
		SetRotation_Quaternion(rotation);
	}

	[NativeMethod("SetRotation")]
	private void SetRotation_Quaternion(Quaternion rotation)
	{
		SetRotation_Quaternion_Injected(ref rotation);
	}

	public void MovePosition(Vector2 position)
	{
		MovePosition_Injected(ref position);
	}

	public void MoveRotation(float angle)
	{
		MoveRotation_Angle(angle);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("MoveRotation")]
	private extern void MoveRotation_Angle(float angle);

	public void MoveRotation(Quaternion rotation)
	{
		MoveRotation_Quaternion(rotation);
	}

	[NativeMethod("MoveRotation")]
	private void MoveRotation_Quaternion(Quaternion rotation)
	{
		MoveRotation_Quaternion_Injected(ref rotation);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal extern void SetDragBehaviour(bool dragged);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern bool IsSleeping();

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern bool IsAwake();

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void Sleep();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("Wake")]
	public extern void WakeUp();

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern bool IsTouching([Writable][NotNull("ArgumentNullException")] Collider2D collider);

	public bool IsTouching([Writable] Collider2D collider, ContactFilter2D contactFilter)
	{
		return IsTouching_OtherColliderWithFilter_Internal(collider, contactFilter);
	}

	[NativeMethod("IsTouching")]
	private bool IsTouching_OtherColliderWithFilter_Internal([Writable][NotNull("ArgumentNullException")] Collider2D collider, ContactFilter2D contactFilter)
	{
		return IsTouching_OtherColliderWithFilter_Internal_Injected(collider, ref contactFilter);
	}

	public bool IsTouching(ContactFilter2D contactFilter)
	{
		return IsTouching_AnyColliderWithFilter_Internal(contactFilter);
	}

	[NativeMethod("IsTouching")]
	private bool IsTouching_AnyColliderWithFilter_Internal(ContactFilter2D contactFilter)
	{
		return IsTouching_AnyColliderWithFilter_Internal_Injected(ref contactFilter);
	}

	[ExcludeFromDocs]
	public bool IsTouchingLayers()
	{
		return IsTouchingLayers(-1);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern bool IsTouchingLayers([UnityEngine.Internal.DefaultValue("Physics2D.AllLayers")] int layerMask);

	public bool OverlapPoint(Vector2 point)
	{
		return OverlapPoint_Injected(ref point);
	}

	public ColliderDistance2D Distance([Writable] Collider2D collider)
	{
		if (collider == null)
		{
			throw new ArgumentNullException("Collider cannot be null.");
		}
		if (collider.attachedRigidbody == this)
		{
			throw new ArgumentException("The collider cannot be attached to the Rigidbody2D being searched.");
		}
		return Distance_Internal(collider);
	}

	[NativeMethod("Distance")]
	private ColliderDistance2D Distance_Internal([NotNull("ArgumentNullException")][Writable] Collider2D collider)
	{
		Distance_Internal_Injected(collider, out var ret);
		return ret;
	}

	public Vector2 ClosestPoint(Vector2 position)
	{
		return Physics2D.ClosestPoint(position, this);
	}

	[ExcludeFromDocs]
	public void AddForce(Vector2 force)
	{
		AddForce(force, ForceMode2D.Force);
	}

	public void AddForce(Vector2 force, [UnityEngine.Internal.DefaultValue("ForceMode2D.Force")] ForceMode2D mode)
	{
		AddForce_Injected(ref force, mode);
	}

	[ExcludeFromDocs]
	public void AddRelativeForce(Vector2 relativeForce)
	{
		AddRelativeForce(relativeForce, ForceMode2D.Force);
	}

	public void AddRelativeForce(Vector2 relativeForce, [UnityEngine.Internal.DefaultValue("ForceMode2D.Force")] ForceMode2D mode)
	{
		AddRelativeForce_Injected(ref relativeForce, mode);
	}

	[ExcludeFromDocs]
	public void AddForceAtPosition(Vector2 force, Vector2 position)
	{
		AddForceAtPosition(force, position, ForceMode2D.Force);
	}

	public void AddForceAtPosition(Vector2 force, Vector2 position, [UnityEngine.Internal.DefaultValue("ForceMode2D.Force")] ForceMode2D mode)
	{
		AddForceAtPosition_Injected(ref force, ref position, mode);
	}

	[ExcludeFromDocs]
	public void AddTorque(float torque)
	{
		AddTorque(torque, ForceMode2D.Force);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void AddTorque(float torque, [UnityEngine.Internal.DefaultValue("ForceMode2D.Force")] ForceMode2D mode);

	public Vector2 GetPoint(Vector2 point)
	{
		GetPoint_Injected(ref point, out var ret);
		return ret;
	}

	public Vector2 GetRelativePoint(Vector2 relativePoint)
	{
		GetRelativePoint_Injected(ref relativePoint, out var ret);
		return ret;
	}

	public Vector2 GetVector(Vector2 vector)
	{
		GetVector_Injected(ref vector, out var ret);
		return ret;
	}

	public Vector2 GetRelativeVector(Vector2 relativeVector)
	{
		GetRelativeVector_Injected(ref relativeVector, out var ret);
		return ret;
	}

	public Vector2 GetPointVelocity(Vector2 point)
	{
		GetPointVelocity_Injected(ref point, out var ret);
		return ret;
	}

	public Vector2 GetRelativePointVelocity(Vector2 relativePoint)
	{
		GetRelativePointVelocity_Injected(ref relativePoint, out var ret);
		return ret;
	}

	public int OverlapCollider(ContactFilter2D contactFilter, [Out] Collider2D[] results)
	{
		return OverlapColliderArray_Internal(contactFilter, results);
	}

	[NativeMethod("OverlapColliderArray_Binding")]
	private int OverlapColliderArray_Internal(ContactFilter2D contactFilter, [NotNull("ArgumentNullException")][Unmarshalled] Collider2D[] results)
	{
		return OverlapColliderArray_Internal_Injected(ref contactFilter, results);
	}

	public int OverlapCollider(ContactFilter2D contactFilter, List<Collider2D> results)
	{
		return OverlapColliderList_Internal(contactFilter, results);
	}

	[NativeMethod("OverlapColliderList_Binding")]
	private int OverlapColliderList_Internal(ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<Collider2D> results)
	{
		return OverlapColliderList_Internal_Injected(ref contactFilter, results);
	}

	public int GetContacts(ContactPoint2D[] contacts)
	{
		return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), contacts);
	}

	public int GetContacts(List<ContactPoint2D> contacts)
	{
		return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), contacts);
	}

	public int GetContacts(ContactFilter2D contactFilter, ContactPoint2D[] contacts)
	{
		return Physics2D.GetContacts(this, contactFilter, contacts);
	}

	public int GetContacts(ContactFilter2D contactFilter, List<ContactPoint2D> contacts)
	{
		return Physics2D.GetContacts(this, contactFilter, contacts);
	}

	public int GetContacts(Collider2D[] colliders)
	{
		return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), colliders);
	}

	public int GetContacts(List<Collider2D> colliders)
	{
		return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), colliders);
	}

	public int GetContacts(ContactFilter2D contactFilter, Collider2D[] colliders)
	{
		return Physics2D.GetContacts(this, contactFilter, colliders);
	}

	public int GetContacts(ContactFilter2D contactFilter, List<Collider2D> colliders)
	{
		return Physics2D.GetContacts(this, contactFilter, colliders);
	}

	public int GetAttachedColliders([Out] Collider2D[] results)
	{
		return GetAttachedCollidersArray_Internal(results);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("GetAttachedCollidersArray_Binding")]
	private extern int GetAttachedCollidersArray_Internal([NotNull("ArgumentNullException")][Unmarshalled] Collider2D[] results);

	public int GetAttachedColliders(List<Collider2D> results)
	{
		return GetAttachedCollidersList_Internal(results);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("GetAttachedCollidersList_Binding")]
	private extern int GetAttachedCollidersList_Internal([NotNull("ArgumentNullException")] List<Collider2D> results);

	[ExcludeFromDocs]
	public int Cast(Vector2 direction, RaycastHit2D[] results)
	{
		return CastArray_Internal(direction, float.PositiveInfinity, results);
	}

	public int Cast(Vector2 direction, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance)
	{
		return CastArray_Internal(direction, distance, results);
	}

	[NativeMethod("CastArray_Binding")]
	private int CastArray_Internal(Vector2 direction, float distance, [Unmarshalled][NotNull("ArgumentNullException")] RaycastHit2D[] results)
	{
		return CastArray_Internal_Injected(ref direction, distance, results);
	}

	public int Cast(Vector2 direction, List<RaycastHit2D> results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity)
	{
		return CastList_Internal(direction, distance, results);
	}

	[NativeMethod("CastList_Binding")]
	private int CastList_Internal(Vector2 direction, float distance, [NotNull("ArgumentNullException")] List<RaycastHit2D> results)
	{
		return CastList_Internal_Injected(ref direction, distance, results);
	}

	[ExcludeFromDocs]
	public int Cast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
	{
		return CastFilteredArray_Internal(direction, float.PositiveInfinity, contactFilter, results);
	}

	public int Cast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance)
	{
		return CastFilteredArray_Internal(direction, distance, contactFilter, results);
	}

	[NativeMethod("CastFilteredArray_Binding")]
	private int CastFilteredArray_Internal(Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")][Unmarshalled] RaycastHit2D[] results)
	{
		return CastFilteredArray_Internal_Injected(ref direction, distance, ref contactFilter, results);
	}

	public int Cast(Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance)
	{
		return CastFilteredList_Internal(direction, distance, contactFilter, results);
	}

	[NativeMethod("CastFilteredList_Binding")]
	private int CastFilteredList_Internal(Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<RaycastHit2D> results)
	{
		return CastFilteredList_Internal_Injected(ref direction, distance, ref contactFilter, results);
	}

	public int GetShapes(PhysicsShapeGroup2D physicsShapeGroup)
	{
		return GetShapes_Internal(ref physicsShapeGroup.m_GroupState);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("GetShapes_Binding")]
	private extern int GetShapes_Internal(ref PhysicsShapeGroup2D.GroupState physicsShapeGroupState);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_position_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_position_Injected(ref Vector2 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void SetRotation_Quaternion_Injected(ref Quaternion rotation);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void MovePosition_Injected(ref Vector2 position);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void MoveRotation_Quaternion_Injected(ref Quaternion rotation);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_velocity_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_velocity_Injected(ref Vector2 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_centerOfMass_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_centerOfMass_Injected(ref Vector2 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_worldCenterOfMass_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_totalForce_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_totalForce_Injected(ref Vector2 value);

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
	private extern bool IsTouching_OtherColliderWithFilter_Internal_Injected([Writable] Collider2D collider, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern bool IsTouching_AnyColliderWithFilter_Internal_Injected(ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern bool OverlapPoint_Injected(ref Vector2 point);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void Distance_Internal_Injected([Writable] Collider2D collider, out ColliderDistance2D ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void AddForce_Injected(ref Vector2 force, [UnityEngine.Internal.DefaultValue("ForceMode2D.Force")] ForceMode2D mode);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void AddRelativeForce_Injected(ref Vector2 relativeForce, [UnityEngine.Internal.DefaultValue("ForceMode2D.Force")] ForceMode2D mode);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void AddForceAtPosition_Injected(ref Vector2 force, ref Vector2 position, [UnityEngine.Internal.DefaultValue("ForceMode2D.Force")] ForceMode2D mode);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetPoint_Injected(ref Vector2 point, out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetRelativePoint_Injected(ref Vector2 relativePoint, out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetVector_Injected(ref Vector2 vector, out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetRelativeVector_Injected(ref Vector2 relativeVector, out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetPointVelocity_Injected(ref Vector2 point, out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetRelativePointVelocity_Injected(ref Vector2 relativePoint, out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern int OverlapColliderArray_Internal_Injected(ref ContactFilter2D contactFilter, Collider2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern int OverlapColliderList_Internal_Injected(ref ContactFilter2D contactFilter, List<Collider2D> results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern int CastArray_Internal_Injected(ref Vector2 direction, float distance, RaycastHit2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern int CastList_Internal_Injected(ref Vector2 direction, float distance, List<RaycastHit2D> results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern int CastFilteredArray_Internal_Injected(ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, RaycastHit2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern int CastFilteredList_Internal_Injected(ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, List<RaycastHit2D> results);
}
[RequireComponent(typeof(Transform))]
[NativeHeader("Modules/Physics2D/Public/Collider2D.h")]
[RequiredByNativeCode(Optional = true)]
public class Collider2D : Behaviour
{
	public extern float density
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool isTrigger
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool usedByEffector
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool usedByComposite
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern CompositeCollider2D composite
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public Vector2 offset
	{
		get
		{
			get_offset_Injected(out var ret);
			return ret;
		}
		set
		{
			set_offset_Injected(ref value);
		}
	}

	public extern Rigidbody2D attachedRigidbody
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetAttachedRigidbody_Binding")]
		get;
	}

	public extern int shapeCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public Bounds bounds
	{
		get
		{
			get_bounds_Injected(out var ret);
			return ret;
		}
	}

	public extern ColliderErrorState2D errorState
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	internal extern bool compositeCapable
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetCompositeCapable_Binding")]
		get;
	}

	public extern PhysicsMaterial2D sharedMaterial
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetMaterial")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("SetMaterial")]
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

	public LayerMask forceSendLayers
	{
		get
		{
			get_forceSendLayers_Injected(out var ret);
			return ret;
		}
		set
		{
			set_forceSendLayers_Injected(ref value);
		}
	}

	public LayerMask forceReceiveLayers
	{
		get
		{
			get_forceReceiveLayers_Injected(out var ret);
			return ret;
		}
		set
		{
			set_forceReceiveLayers_Injected(ref value);
		}
	}

	public LayerMask contactCaptureLayers
	{
		get
		{
			get_contactCaptureLayers_Injected(out var ret);
			return ret;
		}
		set
		{
			set_contactCaptureLayers_Injected(ref value);
		}
	}

	public LayerMask callbackLayers
	{
		get
		{
			get_callbackLayers_Injected(out var ret);
			return ret;
		}
		set
		{
			set_callbackLayers_Injected(ref value);
		}
	}

	public extern float friction
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern float bounciness
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("CreateMesh_Binding")]
	public extern Mesh CreateMesh(bool useBodyPosition, bool useBodyRotation);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("GetShapeHash_Binding")]
	public extern uint GetShapeHash();

	public int GetShapes(PhysicsShapeGroup2D physicsShapeGroup)
	{
		return GetShapes_Internal(ref physicsShapeGroup.m_GroupState, 0, shapeCount);
	}

	public int GetShapes(PhysicsShapeGroup2D physicsShapeGroup, int shapeIndex, [UnityEngine.Internal.DefaultValue("1")] int shapeCount = 1)
	{
		int num = this.shapeCount;
		if (shapeIndex < 0 || shapeIndex >= num || shapeCount < 1 || shapeIndex + shapeCount > num)
		{
			throw new ArgumentOutOfRangeException($"Cannot get shape range from {shapeIndex} to {shapeIndex + shapeCount - 1} as Collider2D only has {num} shape(s).");
		}
		return GetShapes_Internal(ref physicsShapeGroup.m_GroupState, shapeIndex, shapeCount);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("GetShapes_Binding")]
	private extern int GetShapes_Internal(ref PhysicsShapeGroup2D.GroupState physicsShapeGroupState, int shapeIndex, int shapeCount);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern bool IsTouching([NotNull("ArgumentNullException")][Writable] Collider2D collider);

	public bool IsTouching([Writable] Collider2D collider, ContactFilter2D contactFilter)
	{
		return IsTouching_OtherColliderWithFilter(collider, contactFilter);
	}

	[NativeMethod("IsTouching")]
	private bool IsTouching_OtherColliderWithFilter([Writable][NotNull("ArgumentNullException")] Collider2D collider, ContactFilter2D contactFilter)
	{
		return IsTouching_OtherColliderWithFilter_Injected(collider, ref contactFilter);
	}

	public bool IsTouching(ContactFilter2D contactFilter)
	{
		return IsTouching_AnyColliderWithFilter(contactFilter);
	}

	[NativeMethod("IsTouching")]
	private bool IsTouching_AnyColliderWithFilter(ContactFilter2D contactFilter)
	{
		return IsTouching_AnyColliderWithFilter_Injected(ref contactFilter);
	}

	[ExcludeFromDocs]
	public bool IsTouchingLayers()
	{
		return IsTouchingLayers(-1);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern bool IsTouchingLayers([UnityEngine.Internal.DefaultValue("Physics2D.AllLayers")] int layerMask);

	public bool OverlapPoint(Vector2 point)
	{
		return OverlapPoint_Injected(ref point);
	}

	public ColliderDistance2D Distance([Writable] Collider2D collider)
	{
		return Physics2D.Distance(this, collider);
	}

	public int OverlapCollider(ContactFilter2D contactFilter, Collider2D[] results)
	{
		return PhysicsScene2D.OverlapCollider(this, contactFilter, results);
	}

	public int OverlapCollider(ContactFilter2D contactFilter, List<Collider2D> results)
	{
		return PhysicsScene2D.OverlapCollider(this, contactFilter, results);
	}

	public int GetContacts(ContactPoint2D[] contacts)
	{
		return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), contacts);
	}

	public int GetContacts(List<ContactPoint2D> contacts)
	{
		return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), contacts);
	}

	public int GetContacts(ContactFilter2D contactFilter, ContactPoint2D[] contacts)
	{
		return Physics2D.GetContacts(this, contactFilter, contacts);
	}

	public int GetContacts(ContactFilter2D contactFilter, List<ContactPoint2D> contacts)
	{
		return Physics2D.GetContacts(this, contactFilter, contacts);
	}

	public int GetContacts(Collider2D[] colliders)
	{
		return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), colliders);
	}

	public int GetContacts(List<Collider2D> colliders)
	{
		return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), colliders);
	}

	public int GetContacts(ContactFilter2D contactFilter, Collider2D[] colliders)
	{
		return Physics2D.GetContacts(this, contactFilter, colliders);
	}

	public int GetContacts(ContactFilter2D contactFilter, List<Collider2D> colliders)
	{
		return Physics2D.GetContacts(this, contactFilter, colliders);
	}

	[ExcludeFromDocs]
	public int Cast(Vector2 direction, RaycastHit2D[] results)
	{
		ContactFilter2D contactFilter = default(ContactFilter2D);
		contactFilter.useTriggers = Physics2D.queriesHitTriggers;
		contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(base.gameObject.layer));
		return CastArray_Internal(direction, float.PositiveInfinity, contactFilter, ignoreSiblingColliders: true, results);
	}

	[ExcludeFromDocs]
	public int Cast(Vector2 direction, RaycastHit2D[] results, float distance)
	{
		ContactFilter2D contactFilter = default(ContactFilter2D);
		contactFilter.useTriggers = Physics2D.queriesHitTriggers;
		contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(base.gameObject.layer));
		return CastArray_Internal(direction, distance, contactFilter, ignoreSiblingColliders: true, results);
	}

	public int Cast(Vector2 direction, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("true")] bool ignoreSiblingColliders)
	{
		ContactFilter2D contactFilter = default(ContactFilter2D);
		contactFilter.useTriggers = Physics2D.queriesHitTriggers;
		contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(base.gameObject.layer));
		return CastArray_Internal(direction, distance, contactFilter, ignoreSiblingColliders, results);
	}

	[ExcludeFromDocs]
	public int Cast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
	{
		return CastArray_Internal(direction, float.PositiveInfinity, contactFilter, ignoreSiblingColliders: true, results);
	}

	[ExcludeFromDocs]
	public int Cast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, float distance)
	{
		return CastArray_Internal(direction, distance, contactFilter, ignoreSiblingColliders: true, results);
	}

	public int Cast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("true")] bool ignoreSiblingColliders)
	{
		return CastArray_Internal(direction, distance, contactFilter, ignoreSiblingColliders, results);
	}

	[NativeMethod("CastArray_Binding")]
	private int CastArray_Internal(Vector2 direction, float distance, ContactFilter2D contactFilter, bool ignoreSiblingColliders, [Unmarshalled][NotNull("ArgumentNullException")] RaycastHit2D[] results)
	{
		return CastArray_Internal_Injected(ref direction, distance, ref contactFilter, ignoreSiblingColliders, results);
	}

	public int Cast(Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity, [UnityEngine.Internal.DefaultValue("true")] bool ignoreSiblingColliders = true)
	{
		return CastList_Internal(direction, distance, contactFilter, ignoreSiblingColliders, results);
	}

	[NativeMethod("CastList_Binding")]
	private int CastList_Internal(Vector2 direction, float distance, ContactFilter2D contactFilter, bool ignoreSiblingColliders, [NotNull("ArgumentNullException")] List<RaycastHit2D> results)
	{
		return CastList_Internal_Injected(ref direction, distance, ref contactFilter, ignoreSiblingColliders, results);
	}

	[ExcludeFromDocs]
	public int Raycast(Vector2 direction, RaycastHit2D[] results)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-1, float.NegativeInfinity, float.PositiveInfinity);
		return RaycastArray_Internal(direction, float.PositiveInfinity, contactFilter, results);
	}

	[ExcludeFromDocs]
	public int Raycast(Vector2 direction, RaycastHit2D[] results, float distance)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-1, float.NegativeInfinity, float.PositiveInfinity);
		return RaycastArray_Internal(direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public int Raycast(Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return RaycastArray_Internal(direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public int Raycast(Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return RaycastArray_Internal(direction, distance, contactFilter, results);
	}

	public int Raycast(Vector2 direction, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance, [UnityEngine.Internal.DefaultValue("Physics2D.AllLayers")] int layerMask, [UnityEngine.Internal.DefaultValue("-Mathf.Infinity")] float minDepth, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return RaycastArray_Internal(direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public int Raycast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
	{
		return RaycastArray_Internal(direction, float.PositiveInfinity, contactFilter, results);
	}

	public int Raycast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance)
	{
		return RaycastArray_Internal(direction, distance, contactFilter, results);
	}

	[NativeMethod("RaycastArray_Binding")]
	private int RaycastArray_Internal(Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")][Unmarshalled] RaycastHit2D[] results)
	{
		return RaycastArray_Internal_Injected(ref direction, distance, ref contactFilter, results);
	}

	public int Raycast(Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [UnityEngine.Internal.DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity)
	{
		return RaycastList_Internal(direction, distance, contactFilter, results);
	}

	[NativeMethod("RaycastList_Binding")]
	private int RaycastList_Internal(Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull("ArgumentNullException")] List<RaycastHit2D> results)
	{
		return RaycastList_Internal_Injected(ref direction, distance, ref contactFilter, results);
	}

	public Vector2 ClosestPoint(Vector2 position)
	{
		return Physics2D.ClosestPoint(position, this);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_offset_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_offset_Injected(ref Vector2 value);

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
	[SpecialName]
	private extern void get_forceSendLayers_Injected(out LayerMask ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_forceSendLayers_Injected(ref LayerMask value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_forceReceiveLayers_Injected(out LayerMask ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_forceReceiveLayers_Injected(ref LayerMask value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_contactCaptureLayers_Injected(out LayerMask ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_contactCaptureLayers_Injected(ref LayerMask value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_callbackLayers_Injected(out LayerMask ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_callbackLayers_Injected(ref LayerMask value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern bool IsTouching_OtherColliderWithFilter_Injected([Writable] Collider2D collider, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern bool IsTouching_AnyColliderWithFilter_Injected(ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern bool OverlapPoint_Injected(ref Vector2 point);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern int CastArray_Internal_Injected(ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, bool ignoreSiblingColliders, RaycastHit2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern int CastList_Internal_Injected(ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, bool ignoreSiblingColliders, List<RaycastHit2D> results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern int RaycastArray_Internal_Injected(ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, RaycastHit2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern int RaycastList_Internal_Injected(ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, List<RaycastHit2D> results);
}
[NativeHeader("Modules/Physics2D/Public/CustomCollider2D.h")]
public sealed class CustomCollider2D : Collider2D
{
	[NativeMethod("CustomShapeCount_Binding")]
	public extern int customShapeCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	[NativeMethod("CustomVertexCount_Binding")]
	public extern int customVertexCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public int GetCustomShapes(PhysicsShapeGroup2D physicsShapeGroup)
	{
		int num = customShapeCount;
		if (num > 0)
		{
			return GetCustomShapes_Internal(ref physicsShapeGroup.m_GroupState, 0, num);
		}
		physicsShapeGroup.Clear();
		return 0;
	}

	public int GetCustomShapes(PhysicsShapeGroup2D physicsShapeGroup, int shapeIndex, [UnityEngine.Internal.DefaultValue("1")] int shapeCount = 1)
	{
		int num = customShapeCount;
		if (shapeIndex < 0 || shapeIndex >= num || shapeCount < 1 || shapeIndex + shapeCount > num)
		{
			throw new ArgumentOutOfRangeException($"Cannot get shape range from {shapeIndex} to {shapeIndex + shapeCount - 1} as CustomCollider2D only has {num} shape(s).");
		}
		return GetCustomShapes_Internal(ref physicsShapeGroup.m_GroupState, shapeIndex, shapeCount);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("GetCustomShapes_Binding")]
	private extern int GetCustomShapes_Internal(ref PhysicsShapeGroup2D.GroupState physicsShapeGroupState, int shapeIndex, int shapeCount);

	public unsafe int GetCustomShapes(NativeArray<PhysicsShape2D> shapes, NativeArray<Vector2> vertices)
	{
		if (!shapes.IsCreated || shapes.Length != customShapeCount)
		{
			throw new ArgumentException($"Cannot get custom shapes as the native shapes array length must be identical to the current custom shape count of {customShapeCount}.", "shapes");
		}
		if (!vertices.IsCreated || vertices.Length != customVertexCount)
		{
			throw new ArgumentException($"Cannot get custom shapes as the native vertices array length must be identical to the current custom vertex count of {customVertexCount}.", "vertices");
		}
		return GetCustomShapesNative_Internal((IntPtr)shapes.GetUnsafeReadOnlyPtr(), shapes.Length, (IntPtr)vertices.GetUnsafeReadOnlyPtr(), vertices.Length);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("GetCustomShapesAllNative_Binding")]
	private extern int GetCustomShapesNative_Internal(IntPtr shapesPtr, int shapeCount, IntPtr verticesPtr, int vertexCount);

	public void SetCustomShapes(PhysicsShapeGroup2D physicsShapeGroup)
	{
		if (physicsShapeGroup.shapeCount > 0)
		{
			SetCustomShapesAll_Internal(ref physicsShapeGroup.m_GroupState);
		}
		else
		{
			ClearCustomShapes();
		}
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("SetCustomShapesAll_Binding")]
	private extern void SetCustomShapesAll_Internal(ref PhysicsShapeGroup2D.GroupState physicsShapeGroupState);

	public unsafe void SetCustomShapes(NativeArray<PhysicsShape2D> shapes, NativeArray<Vector2> vertices)
	{
		if (!shapes.IsCreated || shapes.Length == 0)
		{
			throw new ArgumentException("Cannot set custom shapes as the native shapes array is empty.", "shapes");
		}
		if (!vertices.IsCreated || vertices.Length == 0)
		{
			throw new ArgumentException("Cannot set custom shapes as the native vertices array is empty.", "vertices");
		}
		SetCustomShapesNative_Internal((IntPtr)shapes.GetUnsafeReadOnlyPtr(), shapes.Length, (IntPtr)vertices.GetUnsafeReadOnlyPtr(), vertices.Length);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("SetCustomShapesAllNative_Binding", ThrowsException = true)]
	private extern void SetCustomShapesNative_Internal(IntPtr shapesPtr, int shapeCount, IntPtr verticesPtr, int vertexCount);

	public void SetCustomShape(PhysicsShapeGroup2D physicsShapeGroup, int srcShapeIndex, int dstShapeIndex)
	{
		if (srcShapeIndex < 0 || srcShapeIndex >= physicsShapeGroup.shapeCount)
		{
			throw new ArgumentOutOfRangeException($"Cannot set custom shape at {srcShapeIndex} as the shape group only has {physicsShapeGroup.shapeCount} shape(s).");
		}
		PhysicsShape2D shape = physicsShapeGroup.GetShape(srcShapeIndex);
		if (shape.vertexStartIndex < 0 || shape.vertexStartIndex >= physicsShapeGroup.vertexCount || shape.vertexCount < 1 || shape.vertexStartIndex + shape.vertexCount > physicsShapeGroup.vertexCount)
		{
			throw new ArgumentOutOfRangeException($"Cannot set custom shape at {srcShapeIndex} as its shape indices are out of the available vertices ranges.");
		}
		if (dstShapeIndex < 0 || dstShapeIndex >= customShapeCount)
		{
			throw new ArgumentOutOfRangeException($"Cannot set custom shape at destination {dstShapeIndex} as CustomCollider2D only has {customShapeCount} custom shape(s). The destination index must be within the existing shape range.");
		}
		SetCustomShape_Internal(ref physicsShapeGroup.m_GroupState, srcShapeIndex, dstShapeIndex);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("SetCustomShape_Binding")]
	private extern void SetCustomShape_Internal(ref PhysicsShapeGroup2D.GroupState physicsShapeGroupState, int srcShapeIndex, int dstShapeIndex);

	public unsafe void SetCustomShape(NativeArray<PhysicsShape2D> shapes, NativeArray<Vector2> vertices, int srcShapeIndex, int dstShapeIndex)
	{
		if (!shapes.IsCreated || shapes.Length == 0)
		{
			throw new ArgumentException("Cannot set custom shapes as the native shapes array is empty.", "shapes");
		}
		if (!vertices.IsCreated || vertices.Length == 0)
		{
			throw new ArgumentException("Cannot set custom shapes as the native vertices array is empty.", "vertices");
		}
		if (srcShapeIndex < 0 || srcShapeIndex >= shapes.Length)
		{
			throw new ArgumentOutOfRangeException($"Cannot set custom shape at {srcShapeIndex} as the shape native array only has {shapes.Length} shape(s).");
		}
		PhysicsShape2D physicsShape2D = shapes[srcShapeIndex];
		if (physicsShape2D.vertexStartIndex < 0 || physicsShape2D.vertexStartIndex >= vertices.Length || physicsShape2D.vertexCount < 1 || physicsShape2D.vertexStartIndex + physicsShape2D.vertexCount > vertices.Length)
		{
			throw new ArgumentOutOfRangeException($"Cannot set custom shape at {srcShapeIndex} as its shape indices are out of the available vertices ranges.");
		}
		if (dstShapeIndex < 0 || dstShapeIndex >= customShapeCount)
		{
			throw new ArgumentOutOfRangeException($"Cannot set custom shape at destination {dstShapeIndex} as CustomCollider2D only has {customShapeCount} custom shape(s). The destination index must be within the existing shape range.");
		}
		SetCustomShapeNative_Internal((IntPtr)shapes.GetUnsafeReadOnlyPtr(), shapes.Length, (IntPtr)vertices.GetUnsafeReadOnlyPtr(), vertices.Length, srcShapeIndex, dstShapeIndex);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("SetCustomShapeNative_Binding", ThrowsException = true)]
	private extern void SetCustomShapeNative_Internal(IntPtr shapesPtr, int shapeCount, IntPtr verticesPtr, int vertexCount, int srcShapeIndex, int dstShapeIndex);

	public void ClearCustomShapes(int shapeIndex, int shapeCount)
	{
		int num = customShapeCount;
		if (shapeIndex < 0 || shapeIndex >= num)
		{
			throw new ArgumentOutOfRangeException($"Cannot clear custom shape(s) at index {shapeIndex} as the CustomCollider2D only has {num} shape(s).");
		}
		if (shapeIndex + shapeCount < 0 || shapeIndex + shapeCount > customShapeCount)
		{
			throw new ArgumentOutOfRangeException($"Cannot clear custom shape(s) in the range (index {shapeIndex}, count {shapeCount}) as this range is outside range of the existing {customShapeCount} shape(s).");
		}
		ClearCustomShapes_Internal(shapeIndex, shapeCount);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("ClearCustomShapes_Binding")]
	private extern void ClearCustomShapes_Internal(int shapeIndex, int shapeCount);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("ClearCustomShapes_Binding")]
	public extern void ClearCustomShapes();
}
[NativeHeader("Modules/Physics2D/Public/CircleCollider2D.h")]
public sealed class CircleCollider2D : Collider2D
{
	public extern float radius
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[Obsolete("CircleCollider2D.center has been deprecated. Use CircleCollider2D.offset instead (UnityUpgradable) -> offset", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public Vector2 center
	{
		get
		{
			return Vector2.zero;
		}
		set
		{
		}
	}
}
[NativeHeader("Modules/Physics2D/Public/CapsuleCollider2D.h")]
public sealed class CapsuleCollider2D : Collider2D
{
	public Vector2 size
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

	public extern CapsuleDirection2D direction
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_size_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_size_Injected(ref Vector2 value);
}
[NativeHeader("Modules/Physics2D/Public/EdgeCollider2D.h")]
public sealed class EdgeCollider2D : Collider2D
{
	public extern float edgeRadius
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern int edgeCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern int pointCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern Vector2[] points
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool useAdjacentStartPoint
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool useAdjacentEndPoint
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public Vector2 adjacentStartPoint
	{
		get
		{
			get_adjacentStartPoint_Injected(out var ret);
			return ret;
		}
		set
		{
			set_adjacentStartPoint_Injected(ref value);
		}
	}

	public Vector2 adjacentEndPoint
	{
		get
		{
			get_adjacentEndPoint_Injected(out var ret);
			return ret;
		}
		set
		{
			set_adjacentEndPoint_Injected(ref value);
		}
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void Reset();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("GetPoints_Binding")]
	public extern int GetPoints([NotNull("ArgumentNullException")] List<Vector2> points);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("SetPoints_Binding")]
	public extern bool SetPoints([NotNull("ArgumentNullException")] List<Vector2> points);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_adjacentStartPoint_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_adjacentStartPoint_Injected(ref Vector2 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_adjacentEndPoint_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_adjacentEndPoint_Injected(ref Vector2 value);
}
[NativeHeader("Modules/Physics2D/Public/BoxCollider2D.h")]
public sealed class BoxCollider2D : Collider2D
{
	public Vector2 size
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

	public extern float edgeRadius
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool autoTiling
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("BoxCollider2D.center has been deprecated. Use BoxCollider2D.offset instead (UnityUpgradable) -> offset", true)]
	public Vector2 center
	{
		get
		{
			return Vector2.zero;
		}
		set
		{
		}
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_size_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_size_Injected(ref Vector2 value);
}
[NativeHeader("Modules/Physics2D/Public/PolygonCollider2D.h")]
public sealed class PolygonCollider2D : Collider2D
{
	public extern bool useDelaunayMesh
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool autoTiling
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern Vector2[] points
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetPoints_Binding")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("SetPoints_Binding")]
		set;
	}

	public extern int pathCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("GetPointCount")]
	public extern int GetTotalPointCount();

	public Vector2[] GetPath(int index)
	{
		if (index >= pathCount)
		{
			throw new ArgumentOutOfRangeException($"Path {index} does not exist.");
		}
		if (index < 0)
		{
			throw new ArgumentOutOfRangeException($"Path {index} does not exist; negative path index is invalid.");
		}
		return GetPath_Internal(index);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("GetPath_Binding")]
	private extern Vector2[] GetPath_Internal(int index);

	public void SetPath(int index, Vector2[] points)
	{
		if (index < 0)
		{
			throw new ArgumentOutOfRangeException($"Negative path index {index} is invalid.");
		}
		SetPath_Internal(index, points);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("SetPath_Binding")]
	private extern void SetPath_Internal(int index, [NotNull("ArgumentNullException")] Vector2[] points);

	public int GetPath(int index, List<Vector2> points)
	{
		if (index < 0 || index >= pathCount)
		{
			throw new ArgumentOutOfRangeException("index", $"Path index {index} must be in the range of 0 to {pathCount - 1}.");
		}
		if (points == null)
		{
			throw new ArgumentNullException("points");
		}
		return GetPathList_Internal(index, points);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("GetPathList_Binding")]
	private extern int GetPathList_Internal(int index, [NotNull("ArgumentNullException")] List<Vector2> points);

	public void SetPath(int index, List<Vector2> points)
	{
		if (index < 0)
		{
			throw new ArgumentOutOfRangeException($"Negative path index {index} is invalid.");
		}
		SetPathList_Internal(index, points);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("SetPathList_Binding")]
	private extern void SetPathList_Internal(int index, [NotNull("ArgumentNullException")] List<Vector2> points);

	[ExcludeFromDocs]
	public void CreatePrimitive(int sides)
	{
		CreatePrimitive(sides, Vector2.one, Vector2.zero);
	}

	[ExcludeFromDocs]
	public void CreatePrimitive(int sides, Vector2 scale)
	{
		CreatePrimitive(sides, scale, Vector2.zero);
	}

	public void CreatePrimitive(int sides, [UnityEngine.Internal.DefaultValue("Vector2.one")] Vector2 scale, [UnityEngine.Internal.DefaultValue("Vector2.zero")] Vector2 offset)
	{
		if (sides < 3)
		{
			Debug.LogWarning("Cannot create a 2D polygon primitive collider with less than two sides.", this);
		}
		else if (!(scale.x > 0f) || !(scale.y > 0f))
		{
			Debug.LogWarning("Cannot create a 2D polygon primitive collider with an axis scale less than or equal to zero.", this);
		}
		else
		{
			CreatePrimitive_Internal(sides, scale, offset, autoRefresh: true);
		}
	}

	[NativeMethod("CreatePrimitive")]
	private void CreatePrimitive_Internal(int sides, [UnityEngine.Internal.DefaultValue("Vector2.one")] Vector2 scale, [UnityEngine.Internal.DefaultValue("Vector2.zero")] Vector2 offset, bool autoRefresh)
	{
		CreatePrimitive_Internal_Injected(sides, ref scale, ref offset, autoRefresh);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void CreatePrimitive_Internal_Injected(int sides, [UnityEngine.Internal.DefaultValue("Vector2.one")] ref Vector2 scale, [UnityEngine.Internal.DefaultValue("Vector2.zero")] ref Vector2 offset, bool autoRefresh);
}
[RequireComponent(typeof(Rigidbody2D))]
[NativeHeader("Modules/Physics2D/Public/CompositeCollider2D.h")]
public sealed class CompositeCollider2D : Collider2D
{
	public enum GeometryType
	{
		Outlines,
		Polygons
	}

	public enum GenerationType
	{
		Synchronous,
		Manual
	}

	public extern GeometryType geometryType
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern GenerationType generationType
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool useDelaunayMesh
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float vertexDistance
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float edgeRadius
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float offsetDistance
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern int pathCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern int pointCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void GenerateGeometry();

	public int GetPathPointCount(int index)
	{
		int num = pathCount - 1;
		if (index < 0 || index > num)
		{
			throw new ArgumentOutOfRangeException("index", $"Path index {index} must be in the range of 0 to {num}.");
		}
		return GetPathPointCount_Internal(index);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("GetPathPointCount_Binding")]
	private extern int GetPathPointCount_Internal(int index);

	public int GetPath(int index, Vector2[] points)
	{
		if (index < 0 || index >= pathCount)
		{
			throw new ArgumentOutOfRangeException("index", $"Path index {index} must be in the range of 0 to {pathCount - 1}.");
		}
		if (points == null)
		{
			throw new ArgumentNullException("points");
		}
		return GetPathArray_Internal(index, points);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("GetPathArray_Binding")]
	private extern int GetPathArray_Internal(int index, [Unmarshalled][NotNull("ArgumentNullException")] Vector2[] points);

	public int GetPath(int index, List<Vector2> points)
	{
		if (index < 0 || index >= pathCount)
		{
			throw new ArgumentOutOfRangeException("index", $"Path index {index} must be in the range of 0 to {pathCount - 1}.");
		}
		if (points == null)
		{
			throw new ArgumentNullException("points");
		}
		return GetPathList_Internal(index, points);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("GetPathList_Binding")]
	private extern int GetPathList_Internal(int index, [NotNull("ArgumentNullException")] List<Vector2> points);
}
[NativeHeader("Modules/Physics2D/Joint2D.h")]
[RequireComponent(typeof(Transform), typeof(Rigidbody2D))]
public class Joint2D : Behaviour
{
	public extern Rigidbody2D attachedRigidbody
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern Rigidbody2D connectedBody
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

	public extern JointBreakAction2D breakAction
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public Vector2 reactionForce
	{
		[NativeMethod("GetReactionForceFixedTime")]
		get
		{
			get_reactionForce_Injected(out var ret);
			return ret;
		}
	}

	public extern float reactionTorque
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetReactionTorqueFixedTime")]
		get;
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Joint2D.collideConnected has been deprecated. Use Joint2D.enableCollision instead (UnityUpgradable) -> enableCollision", true)]
	public bool collideConnected
	{
		get
		{
			return enableCollision;
		}
		set
		{
			enableCollision = value;
		}
	}

	public Vector2 GetReactionForce(float timeStep)
	{
		GetReactionForce_Injected(timeStep, out var ret);
		return ret;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern float GetReactionTorque(float timeStep);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_reactionForce_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetReactionForce_Injected(float timeStep, out Vector2 ret);
}
[NativeHeader("Modules/Physics2D/AnchoredJoint2D.h")]
public class AnchoredJoint2D : Joint2D
{
	public Vector2 anchor
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

	public Vector2 connectedAnchor
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

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_anchor_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_anchor_Injected(ref Vector2 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_connectedAnchor_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_connectedAnchor_Injected(ref Vector2 value);
}
[NativeHeader("Modules/Physics2D/SpringJoint2D.h")]
public sealed class SpringJoint2D : AnchoredJoint2D
{
	public extern bool autoConfigureDistance
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float distance
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float dampingRatio
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float frequency
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}
}
[NativeHeader("Modules/Physics2D/DistanceJoint2D.h")]
public sealed class DistanceJoint2D : AnchoredJoint2D
{
	public extern bool autoConfigureDistance
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float distance
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool maxDistanceOnly
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}
}
[NativeHeader("Modules/Physics2D/FrictionJoint2D.h")]
public sealed class FrictionJoint2D : AnchoredJoint2D
{
	public extern float maxForce
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float maxTorque
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}
}
[NativeHeader("Modules/Physics2D/HingeJoint2D.h")]
public sealed class HingeJoint2D : AnchoredJoint2D
{
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

	public JointMotor2D motor
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

	public JointAngleLimits2D limits
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

	public extern JointLimitState2D limitState
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern float referenceAngle
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern float jointAngle
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern float jointSpeed
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern float GetMotorTorque(float timeStep);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_motor_Injected(out JointMotor2D ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_motor_Injected(ref JointMotor2D value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_limits_Injected(out JointAngleLimits2D ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_limits_Injected(ref JointAngleLimits2D value);
}
[NativeHeader("Modules/Physics2D/RelativeJoint2D.h")]
public sealed class RelativeJoint2D : Joint2D
{
	public extern float maxForce
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float maxTorque
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float correctionScale
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool autoConfigureOffset
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public Vector2 linearOffset
	{
		get
		{
			get_linearOffset_Injected(out var ret);
			return ret;
		}
		set
		{
			set_linearOffset_Injected(ref value);
		}
	}

	public extern float angularOffset
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public Vector2 target
	{
		get
		{
			get_target_Injected(out var ret);
			return ret;
		}
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_linearOffset_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_linearOffset_Injected(ref Vector2 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_target_Injected(out Vector2 ret);
}
[NativeHeader("Modules/Physics2D/SliderJoint2D.h")]
public sealed class SliderJoint2D : AnchoredJoint2D
{
	public extern bool autoConfigureAngle
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float angle
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
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

	public JointMotor2D motor
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

	public JointTranslationLimits2D limits
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

	public extern JointLimitState2D limitState
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern float referenceAngle
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern float jointTranslation
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern float jointSpeed
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern float GetMotorForce(float timeStep);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_motor_Injected(out JointMotor2D ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_motor_Injected(ref JointMotor2D value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_limits_Injected(out JointTranslationLimits2D ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_limits_Injected(ref JointTranslationLimits2D value);
}
[NativeHeader("Modules/Physics2D/TargetJoint2D.h")]
public sealed class TargetJoint2D : Joint2D
{
	public Vector2 anchor
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

	public Vector2 target
	{
		get
		{
			get_target_Injected(out var ret);
			return ret;
		}
		set
		{
			set_target_Injected(ref value);
		}
	}

	public extern bool autoConfigureTarget
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float maxForce
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float dampingRatio
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float frequency
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_anchor_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_anchor_Injected(ref Vector2 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_target_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_target_Injected(ref Vector2 value);
}
[NativeHeader("Modules/Physics2D/FixedJoint2D.h")]
public sealed class FixedJoint2D : AnchoredJoint2D
{
	public extern float dampingRatio
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float frequency
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float referenceAngle
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}
}
[NativeHeader("Modules/Physics2D/WheelJoint2D.h")]
public sealed class WheelJoint2D : AnchoredJoint2D
{
	public JointSuspension2D suspension
	{
		get
		{
			get_suspension_Injected(out var ret);
			return ret;
		}
		set
		{
			set_suspension_Injected(ref value);
		}
	}

	public extern bool useMotor
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public JointMotor2D motor
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

	public extern float jointTranslation
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern float jointLinearSpeed
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern float jointSpeed
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetJointAngularSpeed")]
		get;
	}

	public extern float jointAngle
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern float GetMotorTorque(float timeStep);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_suspension_Injected(out JointSuspension2D ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_suspension_Injected(ref JointSuspension2D value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_motor_Injected(out JointMotor2D ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_motor_Injected(ref JointMotor2D value);
}
[NativeHeader("Modules/Physics2D/Effector2D.h")]
public class Effector2D : Behaviour
{
	public extern bool useColliderMask
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern int colliderMask
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	internal extern bool requiresCollider
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	internal extern bool designedForTrigger
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	internal extern bool designedForNonTrigger
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}
}
[NativeHeader("Modules/Physics2D/AreaEffector2D.h")]
public class AreaEffector2D : Effector2D
{
	public extern float forceAngle
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool useGlobalAngle
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float forceMagnitude
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float forceVariation
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
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

	public extern EffectorSelection2D forceTarget
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[Obsolete("AreaEffector2D.forceDirection has been deprecated. Use AreaEffector2D.forceAngle instead (UnityUpgradable) -> forceAngle", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public float forceDirection
	{
		get
		{
			return forceAngle;
		}
		set
		{
			forceAngle = value;
		}
	}
}
[NativeHeader("Modules/Physics2D/BuoyancyEffector2D.h")]
public class BuoyancyEffector2D : Effector2D
{
	public extern float surfaceLevel
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float density
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float linearDrag
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

	public extern float flowAngle
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float flowMagnitude
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float flowVariation
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}
}
[NativeHeader("Modules/Physics2D/PointEffector2D.h")]
public class PointEffector2D : Effector2D
{
	public extern float forceMagnitude
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float forceVariation
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float distanceScale
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
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

	public extern EffectorSelection2D forceSource
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern EffectorSelection2D forceTarget
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern EffectorForceMode2D forceMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}
}
[NativeHeader("Modules/Physics2D/PlatformEffector2D.h")]
public class PlatformEffector2D : Effector2D
{
	public extern bool useOneWay
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool useOneWayGrouping
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool useSideFriction
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool useSideBounce
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float surfaceArc
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float sideArc
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float rotationalOffset
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[Obsolete("PlatformEffector2D.oneWay has been deprecated. Use PlatformEffector2D.useOneWay instead (UnityUpgradable) -> useOneWay", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public bool oneWay
	{
		get
		{
			return useOneWay;
		}
		set
		{
			useOneWay = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("PlatformEffector2D.sideFriction has been deprecated. Use PlatformEffector2D.useSideFriction instead (UnityUpgradable) -> useSideFriction", true)]
	public bool sideFriction
	{
		get
		{
			return useSideFriction;
		}
		set
		{
			useSideFriction = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("PlatformEffector2D.sideBounce has been deprecated. Use PlatformEffector2D.useSideBounce instead (UnityUpgradable) -> useSideBounce", true)]
	public bool sideBounce
	{
		get
		{
			return useSideBounce;
		}
		set
		{
			useSideBounce = value;
		}
	}

	[Obsolete("PlatformEffector2D.sideAngleVariance has been deprecated. Use PlatformEffector2D.sideArc instead (UnityUpgradable) -> sideArc", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public float sideAngleVariance
	{
		get
		{
			return sideArc;
		}
		set
		{
			sideArc = value;
		}
	}
}
[NativeHeader("Modules/Physics2D/SurfaceEffector2D.h")]
public class SurfaceEffector2D : Effector2D
{
	public extern float speed
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float speedVariation
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float forceScale
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool useContactForce
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool useFriction
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool useBounce
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}
}
[NativeHeader("Modules/Physics2D/PhysicsUpdateBehaviour2D.h")]
public class PhysicsUpdateBehaviour2D : Behaviour
{
}
[RequireComponent(typeof(Rigidbody2D))]
[NativeHeader("Modules/Physics2D/ConstantForce2D.h")]
public sealed class ConstantForce2D : PhysicsUpdateBehaviour2D
{
	public Vector2 force
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

	public Vector2 relativeForce
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

	public extern float torque
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_force_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_force_Injected(ref Vector2 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_relativeForce_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_relativeForce_Injected(ref Vector2 value);
}
[NativeHeader("Modules/Physics2D/Public/PhysicsMaterial2D.h")]
public sealed class PhysicsMaterial2D : Object
{
	public extern float bounciness
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float friction
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public PhysicsMaterial2D()
	{
		Create_Internal(this, null);
	}

	public PhysicsMaterial2D(string name)
	{
		Create_Internal(this, name);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("Create_Binding")]
	private static extern void Create_Internal([Writable] PhysicsMaterial2D scriptMaterial, string name);
}
