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
using Unity.Jobs;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

[assembly: InternalsVisibleTo("UnityEngine.UmbraModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIModule")]
[assembly: InternalsVisibleTo("UnityEngine.TilemapModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainPhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubsystemsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityCurlModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubstanceModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteShapeModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteMaskModule")]
[assembly: InternalsVisibleTo("UnityEngine.ScreenCaptureModule")]
[assembly: InternalsVisibleTo("UnityEngine.RuntimeInitializeOnLoadManagerInitializerModule")]
[assembly: InternalsVisibleTo("UnityEngine.PropertiesModule")]
[assembly: InternalsVisibleTo("UnityEngine.ProfilerModule")]
[assembly: InternalsVisibleTo("UnityEngine.Physics2DModule")]
[assembly: InternalsVisibleTo("UnityEngine.StreamingModule")]
[assembly: InternalsVisibleTo("UnityEngine.PerformanceReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityTestProtocolModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.Networking")]
[assembly: InternalsVisibleTo("UnityEngine.PS5VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS5Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS4VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS4Module")]
[assembly: InternalsVisibleTo("UnityEngine.XboxOneModule")]
[assembly: InternalsVisibleTo("UnityEngine.SwitchModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.WindModule")]
[assembly: InternalsVisibleTo("UnityEngine.VideoModule")]
[assembly: InternalsVisibleTo("UnityEngine.VehiclesModule")]
[assembly: InternalsVisibleTo("UnityEngine.VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.XRModule")]
[assembly: InternalsVisibleTo("UnityEngine.VFXModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestWWWModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestTextureModule")]
[assembly: InternalsVisibleTo("UnityEngine.VirtualTexturingModule")]
[assembly: InternalsVisibleTo("UnityEngine.ParticleSystemModule")]
[assembly: InternalsVisibleTo("UnityEngine.NVIDIAModule")]
[assembly: InternalsVisibleTo("UnityEngine.LocalizationModule")]
[assembly: InternalsVisibleTo("UnityEngine.AudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.HotReloadModule")]
[assembly: InternalsVisibleTo("UnityEngine.AnimationModule")]
[assembly: InternalsVisibleTo("UnityEngine.AndroidJNIModule")]
[assembly: InternalsVisibleTo("UnityEngine.AccessibilityModule")]
[assembly: InternalsVisibleTo("UnityEngine.ARModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClothModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputModule")]
[assembly: InternalsVisibleTo("UnityEngine.PhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.AIModule")]
[assembly: InternalsVisibleTo("UnityEngine.CoreModule")]
[assembly: InternalsVisibleTo("UnityEngine.SharedInternalsModule")]
[assembly: InternalsVisibleTo("UnityEngine")]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: InternalsVisibleTo("UnityEngine.JSONSerializeModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterRendererModule")]
[assembly: InternalsVisibleTo("UnityEngine.ContentLoadModule")]
[assembly: InternalsVisibleTo("UnityEngine.IMGUIModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreTextEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreFontEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputLegacyModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextRenderingModule")]
[assembly: InternalsVisibleTo("UnityEngine.GridModule")]
[assembly: InternalsVisibleTo("UnityEngine.GameCenterModule")]
[assembly: InternalsVisibleTo("UnityEngine.ImageConversionModule")]
[assembly: InternalsVisibleTo("UnityEngine.GIModule")]
[assembly: InternalsVisibleTo("UnityEngine.DirectorModule")]
[assembly: InternalsVisibleTo("UnityEngine.DSPGraphModule")]
[assembly: InternalsVisibleTo("UnityEngine.CrashReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestModule")]
[assembly: InternalsVisibleTo("UnityEngine.TLSModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommonModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityConnectModule")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud.Service")]
[assembly: InternalsVisibleTo("Unity.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.Analytics")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.012")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.011")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.010")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.009")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.008")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.007")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.006")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.013")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.001")]
[assembly: InternalsVisibleTo("UnityEngine.Core.Runtime.Tests")]
[assembly: InternalsVisibleTo("Unity.Core")]
[assembly: InternalsVisibleTo("Unity.Runtime")]
[assembly: InternalsVisibleTo("Unity.Collections")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.014")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.015")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.016")]
[assembly: InternalsVisibleTo("Unity.AI.Navigation.Editor")]
[assembly: InternalsVisibleTo("Unity.AI.HeightMesh.Tests")]
[assembly: UnityEngineModuleAssembly]
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
[assembly: InternalsVisibleTo("Unity.Entities.Tests")]
[assembly: CompilationRelaxations(8)]
[assembly: InternalsVisibleTo("Unity.Entities")]
[assembly: InternalsVisibleTo("Unity.Services.QoS")]
[assembly: InternalsVisibleTo("Unity.PerformanceTests.RuntimeTestRunner.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Framework")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Timeline")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.UnityAnalytics")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.AllIn1Runner")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests")]
[assembly: InternalsVisibleTo("Unity.Burst.Editor")]
[assembly: InternalsVisibleTo("Unity.Burst")]
[assembly: InternalsVisibleTo("Unity.Automation")]
[assembly: InternalsVisibleTo("UnityEngine.TestRunner")]
[assembly: InternalsVisibleTo("UnityEngine.Purchasing")]
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommon")]
[assembly: InternalsVisibleTo("Unity.DeploymentTests.Services")]
[assembly: InternalsVisibleTo("Unity.Timeline")]
[assembly: InternalsVisibleTo("Assembly-CSharp-testable")]
[assembly: InternalsVisibleTo("Assembly-CSharp-firstpass-testable")]
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
[assembly: InternalsVisibleTo("Unity.2D.Sprite.Editor")]
[assembly: InternalsVisibleTo("Unity.WindowsMRAutomation")]
[assembly: InternalsVisibleTo("GoogleAR.UnityNative")]
[assembly: InternalsVisibleTo("UnityEngine.SpatialTracking")]
[assembly: InternalsVisibleTo("Unity.Logging")]
[assembly: InternalsVisibleTo("Unity.AI.Navigation.Updater")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace UnityEngine.Experimental.AI
{
	public struct PolygonId : IEquatable<PolygonId>
	{
		internal ulong polyRef;

		public bool IsNull()
		{
			return polyRef == 0;
		}

		public static bool operator ==(PolygonId x, PolygonId y)
		{
			return x.polyRef == y.polyRef;
		}

		public static bool operator !=(PolygonId x, PolygonId y)
		{
			return x.polyRef != y.polyRef;
		}

		public override int GetHashCode()
		{
			return polyRef.GetHashCode();
		}

		public bool Equals(PolygonId rhs)
		{
			return rhs == this;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is PolygonId))
			{
				return false;
			}
			PolygonId polygonId = (PolygonId)obj;
			return polygonId == this;
		}
	}
	public struct NavMeshLocation
	{
		public PolygonId polygon { get; }

		public Vector3 position { get; }

		internal NavMeshLocation(Vector3 position, PolygonId polygon)
		{
			this.position = position;
			this.polygon = polygon;
		}
	}
	[Flags]
	public enum PathQueryStatus
	{
		Failure = int.MinValue,
		Success = 0x40000000,
		InProgress = 0x20000000,
		StatusDetailMask = 0xFFFFFF,
		WrongMagic = 1,
		WrongVersion = 2,
		OutOfMemory = 4,
		InvalidParam = 8,
		BufferTooSmall = 0x10,
		OutOfNodes = 0x20,
		PartialResult = 0x40
	}
	public enum NavMeshPolyTypes
	{
		Ground,
		OffMeshConnection
	}
	[StaticAccessor("NavMeshWorldBindings", StaticAccessorType.DoubleColon)]
	public struct NavMeshWorld
	{
		internal IntPtr world;

		public bool IsValid()
		{
			return world != IntPtr.Zero;
		}

		public static NavMeshWorld GetDefaultWorld()
		{
			GetDefaultWorld_Injected(out var ret);
			return ret;
		}

		private static void AddDependencyInternal(IntPtr navmesh, JobHandle handle)
		{
			AddDependencyInternal_Injected(navmesh, ref handle);
		}

		public void AddDependency(JobHandle job)
		{
			AddDependencyInternal(world, job);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDefaultWorld_Injected(out NavMeshWorld ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddDependencyInternal_Injected(IntPtr navmesh, ref JobHandle handle);
	}
	[StaticAccessor("NavMeshQueryBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Math/Matrix4x4.h")]
	[NativeHeader("Modules/AI/Public/NavMeshBindingTypes.h")]
	[NativeHeader("Modules/AI/NavMeshExperimental.bindings.h")]
	[NativeContainer]
	public struct NavMeshQuery : IDisposable
	{
		[NativeDisableUnsafePtrRestriction]
		internal IntPtr m_NavMeshQuery;

		public NavMeshQuery(NavMeshWorld world, Allocator allocator, int pathNodePoolSize = 0)
		{
			m_NavMeshQuery = Create(world, pathNodePoolSize);
			UnsafeUtility.LeakRecord(m_NavMeshQuery, LeakCategory.NavMeshQuery, 0);
		}

		public void Dispose()
		{
			UnsafeUtility.LeakErase(m_NavMeshQuery, LeakCategory.NavMeshQuery);
			Destroy(m_NavMeshQuery);
			m_NavMeshQuery = IntPtr.Zero;
		}

		private static IntPtr Create(NavMeshWorld world, int nodePoolSize)
		{
			return Create_Injected(ref world, nodePoolSize);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy(IntPtr navMeshQuery);

		public unsafe PathQueryStatus BeginFindPath(NavMeshLocation start, NavMeshLocation end, int areaMask = -1, NativeArray<float> costs = default(NativeArray<float>))
		{
			void* costs2 = ((costs.Length > 0) ? costs.GetUnsafePtr() : null);
			return BeginFindPath(m_NavMeshQuery, start, end, areaMask, costs2);
		}

		public PathQueryStatus UpdateFindPath(int iterations, out int iterationsPerformed)
		{
			return UpdateFindPath(m_NavMeshQuery, iterations, out iterationsPerformed);
		}

		public PathQueryStatus EndFindPath(out int pathSize)
		{
			return EndFindPath(m_NavMeshQuery, out pathSize);
		}

		public unsafe int GetPathResult(NativeSlice<PolygonId> path)
		{
			return GetPathResult(m_NavMeshQuery, path.GetUnsafePtr(), path.Length);
		}

		[ThreadSafe]
		private unsafe static PathQueryStatus BeginFindPath(IntPtr navMeshQuery, NavMeshLocation start, NavMeshLocation end, int areaMask, void* costs)
		{
			return BeginFindPath_Injected(navMeshQuery, ref start, ref end, areaMask, costs);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		private static extern PathQueryStatus UpdateFindPath(IntPtr navMeshQuery, int iterations, out int iterationsPerformed);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		private static extern PathQueryStatus EndFindPath(IntPtr navMeshQuery, out int pathSize);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		private unsafe static extern int GetPathResult(IntPtr navMeshQuery, void* path, int maxPath);

		[ThreadSafe]
		private static bool IsValidPolygon(IntPtr navMeshQuery, PolygonId polygon)
		{
			return IsValidPolygon_Injected(navMeshQuery, ref polygon);
		}

		public bool IsValid(PolygonId polygon)
		{
			return polygon.polyRef != 0L && IsValidPolygon(m_NavMeshQuery, polygon);
		}

		public bool IsValid(NavMeshLocation location)
		{
			return IsValid(location.polygon);
		}

		[ThreadSafe]
		private static int GetAgentTypeIdForPolygon(IntPtr navMeshQuery, PolygonId polygon)
		{
			return GetAgentTypeIdForPolygon_Injected(navMeshQuery, ref polygon);
		}

		public int GetAgentTypeIdForPolygon(PolygonId polygon)
		{
			return GetAgentTypeIdForPolygon(m_NavMeshQuery, polygon);
		}

		[ThreadSafe]
		private static bool IsPositionInPolygon(IntPtr navMeshQuery, Vector3 position, PolygonId polygon)
		{
			return IsPositionInPolygon_Injected(navMeshQuery, ref position, ref polygon);
		}

		[ThreadSafe]
		private static PathQueryStatus GetClosestPointOnPoly(IntPtr navMeshQuery, PolygonId polygon, Vector3 position, out Vector3 nearest)
		{
			return GetClosestPointOnPoly_Injected(navMeshQuery, ref polygon, ref position, out nearest);
		}

		public NavMeshLocation CreateLocation(Vector3 position, PolygonId polygon)
		{
			Vector3 nearest;
			PathQueryStatus closestPointOnPoly = GetClosestPointOnPoly(m_NavMeshQuery, polygon, position, out nearest);
			return ((closestPointOnPoly & PathQueryStatus.Success) != 0) ? new NavMeshLocation(nearest, polygon) : default(NavMeshLocation);
		}

		[ThreadSafe]
		private static NavMeshLocation MapLocation(IntPtr navMeshQuery, Vector3 position, Vector3 extents, int agentTypeID, int areaMask = -1)
		{
			MapLocation_Injected(navMeshQuery, ref position, ref extents, agentTypeID, areaMask, out var ret);
			return ret;
		}

		public NavMeshLocation MapLocation(Vector3 position, Vector3 extents, int agentTypeID, int areaMask = -1)
		{
			return MapLocation(m_NavMeshQuery, position, extents, agentTypeID, areaMask);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		private unsafe static extern void MoveLocations(IntPtr navMeshQuery, void* locations, void* targets, void* areaMasks, int count);

		public unsafe void MoveLocations(NativeSlice<NavMeshLocation> locations, NativeSlice<Vector3> targets, NativeSlice<int> areaMasks)
		{
			MoveLocations(m_NavMeshQuery, locations.GetUnsafePtr(), targets.GetUnsafeReadOnlyPtr(), areaMasks.GetUnsafeReadOnlyPtr(), locations.Length);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		private unsafe static extern void MoveLocationsInSameAreas(IntPtr navMeshQuery, void* locations, void* targets, int count, int areaMask);

		public unsafe void MoveLocationsInSameAreas(NativeSlice<NavMeshLocation> locations, NativeSlice<Vector3> targets, int areaMask = -1)
		{
			MoveLocationsInSameAreas(m_NavMeshQuery, locations.GetUnsafePtr(), targets.GetUnsafeReadOnlyPtr(), locations.Length, areaMask);
		}

		[ThreadSafe]
		private static NavMeshLocation MoveLocation(IntPtr navMeshQuery, NavMeshLocation location, Vector3 target, int areaMask)
		{
			MoveLocation_Injected(navMeshQuery, ref location, ref target, areaMask, out var ret);
			return ret;
		}

		public NavMeshLocation MoveLocation(NavMeshLocation location, Vector3 target, int areaMask = -1)
		{
			return MoveLocation(m_NavMeshQuery, location, target, areaMask);
		}

		[ThreadSafe]
		private static bool GetPortalPoints(IntPtr navMeshQuery, PolygonId polygon, PolygonId neighbourPolygon, out Vector3 left, out Vector3 right)
		{
			return GetPortalPoints_Injected(navMeshQuery, ref polygon, ref neighbourPolygon, out left, out right);
		}

		public bool GetPortalPoints(PolygonId polygon, PolygonId neighbourPolygon, out Vector3 left, out Vector3 right)
		{
			return GetPortalPoints(m_NavMeshQuery, polygon, neighbourPolygon, out left, out right);
		}

		[ThreadSafe]
		private static Matrix4x4 PolygonLocalToWorldMatrix(IntPtr navMeshQuery, PolygonId polygon)
		{
			PolygonLocalToWorldMatrix_Injected(navMeshQuery, ref polygon, out var ret);
			return ret;
		}

		public Matrix4x4 PolygonLocalToWorldMatrix(PolygonId polygon)
		{
			return PolygonLocalToWorldMatrix(m_NavMeshQuery, polygon);
		}

		[ThreadSafe]
		private static Matrix4x4 PolygonWorldToLocalMatrix(IntPtr navMeshQuery, PolygonId polygon)
		{
			PolygonWorldToLocalMatrix_Injected(navMeshQuery, ref polygon, out var ret);
			return ret;
		}

		public Matrix4x4 PolygonWorldToLocalMatrix(PolygonId polygon)
		{
			return PolygonWorldToLocalMatrix(m_NavMeshQuery, polygon);
		}

		[ThreadSafe]
		private static NavMeshPolyTypes GetPolygonType(IntPtr navMeshQuery, PolygonId polygon)
		{
			return GetPolygonType_Injected(navMeshQuery, ref polygon);
		}

		public NavMeshPolyTypes GetPolygonType(PolygonId polygon)
		{
			return GetPolygonType(m_NavMeshQuery, polygon);
		}

		[ThreadSafe]
		private unsafe static PathQueryStatus Raycast(IntPtr navMeshQuery, NavMeshLocation start, Vector3 targetPosition, int areaMask, void* costs, out NavMeshHit hit, void* path, out int pathCount, int maxPath)
		{
			return Raycast_Injected(navMeshQuery, ref start, ref targetPosition, areaMask, costs, out hit, path, out pathCount, maxPath);
		}

		public unsafe PathQueryStatus Raycast(out NavMeshHit hit, NavMeshLocation start, Vector3 targetPosition, int areaMask = -1, NativeArray<float> costs = default(NativeArray<float>))
		{
			void* costs2 = ((costs.Length == 32) ? costs.GetUnsafePtr() : null);
			int pathCount;
			PathQueryStatus pathQueryStatus = Raycast(m_NavMeshQuery, start, targetPosition, areaMask, costs2, out hit, null, out pathCount, 0);
			return pathQueryStatus & ~PathQueryStatus.BufferTooSmall;
		}

		public unsafe PathQueryStatus Raycast(out NavMeshHit hit, NativeSlice<PolygonId> path, out int pathCount, NavMeshLocation start, Vector3 targetPosition, int areaMask = -1, NativeArray<float> costs = default(NativeArray<float>))
		{
			void* costs2 = ((costs.Length == 32) ? costs.GetUnsafePtr() : null);
			void* ptr = ((path.Length > 0) ? path.GetUnsafePtr() : null);
			int maxPath = ((ptr != null) ? path.Length : 0);
			return Raycast(m_NavMeshQuery, start, targetPosition, areaMask, costs2, out hit, ptr, out pathCount, maxPath);
		}

		[ThreadSafe]
		private unsafe static PathQueryStatus GetEdgesAndNeighbors(IntPtr navMeshQuery, PolygonId node, int maxVerts, int maxNei, void* verts, void* neighbors, void* edgeIndices, out int vertCount, out int neighborsCount)
		{
			return GetEdgesAndNeighbors_Injected(navMeshQuery, ref node, maxVerts, maxNei, verts, neighbors, edgeIndices, out vertCount, out neighborsCount);
		}

		public unsafe PathQueryStatus GetEdgesAndNeighbors(PolygonId node, NativeSlice<Vector3> edgeVertices, NativeSlice<PolygonId> neighbors, NativeSlice<byte> edgeIndices, out int verticesCount, out int neighborsCount)
		{
			void* verts = ((edgeVertices.Length > 0) ? edgeVertices.GetUnsafePtr() : null);
			void* neighbors2 = ((neighbors.Length > 0) ? neighbors.GetUnsafePtr() : null);
			void* edgeIndices2 = ((edgeIndices.Length > 0) ? edgeIndices.GetUnsafePtr() : null);
			int length = edgeVertices.Length;
			int maxNei = ((neighbors.Length > 0) ? neighbors.Length : edgeIndices.Length);
			return GetEdgesAndNeighbors(m_NavMeshQuery, node, length, maxNei, verts, neighbors2, edgeIndices2, out verticesCount, out neighborsCount);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create_Injected(ref NavMeshWorld world, int nodePoolSize);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern PathQueryStatus BeginFindPath_Injected(IntPtr navMeshQuery, ref NavMeshLocation start, ref NavMeshLocation end, int areaMask, void* costs);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsValidPolygon_Injected(IntPtr navMeshQuery, ref PolygonId polygon);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetAgentTypeIdForPolygon_Injected(IntPtr navMeshQuery, ref PolygonId polygon);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsPositionInPolygon_Injected(IntPtr navMeshQuery, ref Vector3 position, ref PolygonId polygon);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PathQueryStatus GetClosestPointOnPoly_Injected(IntPtr navMeshQuery, ref PolygonId polygon, ref Vector3 position, out Vector3 nearest);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MapLocation_Injected(IntPtr navMeshQuery, ref Vector3 position, ref Vector3 extents, int agentTypeID, int areaMask = -1, out NavMeshLocation ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MoveLocation_Injected(IntPtr navMeshQuery, ref NavMeshLocation location, ref Vector3 target, int areaMask, out NavMeshLocation ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetPortalPoints_Injected(IntPtr navMeshQuery, ref PolygonId polygon, ref PolygonId neighbourPolygon, out Vector3 left, out Vector3 right);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PolygonLocalToWorldMatrix_Injected(IntPtr navMeshQuery, ref PolygonId polygon, out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PolygonWorldToLocalMatrix_Injected(IntPtr navMeshQuery, ref PolygonId polygon, out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern NavMeshPolyTypes GetPolygonType_Injected(IntPtr navMeshQuery, ref PolygonId polygon);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern PathQueryStatus Raycast_Injected(IntPtr navMeshQuery, ref NavMeshLocation start, ref Vector3 targetPosition, int areaMask, void* costs, out NavMeshHit hit, void* path, out int pathCount, int maxPath);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern PathQueryStatus GetEdgesAndNeighbors_Injected(IntPtr navMeshQuery, ref PolygonId node, int maxVerts, int maxNei, void* verts, void* neighbors, void* edgeIndices, out int vertCount, out int neighborsCount);
	}
}
namespace UnityEngine.AI
{
	[NativeHeader("Modules/AI/Builder/NavMeshBuilder.bindings.h")]
	[StaticAccessor("NavMeshBuilderBindings", StaticAccessorType.DoubleColon)]
	public static class NavMeshBuilder
	{
		public static void CollectSources(Bounds includedWorldBounds, int includedLayerMask, NavMeshCollectGeometry geometry, int defaultArea, bool generateLinksByDefault, List<NavMeshBuildMarkup> markups, bool includeOnlyMarkedObjects, List<NavMeshBuildSource> results)
		{
			if (markups == null)
			{
				throw new ArgumentNullException("markups");
			}
			if (results == null)
			{
				throw new ArgumentNullException("results");
			}
			includedWorldBounds.extents = Vector3.Max(includedWorldBounds.extents, 0.001f * Vector3.one);
			NavMeshBuildSource[] collection = CollectSourcesInternal(includedLayerMask, includedWorldBounds, null, useBounds: true, geometry, defaultArea, generateLinksByDefault, markups.ToArray(), includeOnlyMarkedObjects);
			results.Clear();
			results.AddRange(collection);
		}

		public static void CollectSources(Bounds includedWorldBounds, int includedLayerMask, NavMeshCollectGeometry geometry, int defaultArea, List<NavMeshBuildMarkup> markups, List<NavMeshBuildSource> results)
		{
			CollectSources(includedWorldBounds, includedLayerMask, geometry, defaultArea, generateLinksByDefault: false, markups, includeOnlyMarkedObjects: false, results);
		}

		public static void CollectSources(Transform root, int includedLayerMask, NavMeshCollectGeometry geometry, int defaultArea, bool generateLinksByDefault, List<NavMeshBuildMarkup> markups, bool includeOnlyMarkedObjects, List<NavMeshBuildSource> results)
		{
			if (markups == null)
			{
				throw new ArgumentNullException("markups");
			}
			if (results == null)
			{
				throw new ArgumentNullException("results");
			}
			NavMeshBuildSource[] collection = CollectSourcesInternal(includedLayerMask, default(Bounds), root, useBounds: false, geometry, defaultArea, generateLinksByDefault, markups.ToArray(), includeOnlyMarkedObjects);
			results.Clear();
			results.AddRange(collection);
		}

		public static void CollectSources(Transform root, int includedLayerMask, NavMeshCollectGeometry geometry, int defaultArea, List<NavMeshBuildMarkup> markups, List<NavMeshBuildSource> results)
		{
			CollectSources(root, includedLayerMask, geometry, defaultArea, generateLinksByDefault: false, markups, includeOnlyMarkedObjects: false, results);
		}

		private static NavMeshBuildSource[] CollectSourcesInternal(int includedLayerMask, Bounds includedWorldBounds, Transform root, bool useBounds, NavMeshCollectGeometry geometry, int defaultArea, bool generateLinksByDefault, NavMeshBuildMarkup[] markups, bool includeOnlyMarkedObjects)
		{
			return CollectSourcesInternal_Injected(includedLayerMask, ref includedWorldBounds, root, useBounds, geometry, defaultArea, generateLinksByDefault, markups, includeOnlyMarkedObjects);
		}

		public static NavMeshData BuildNavMeshData(NavMeshBuildSettings buildSettings, List<NavMeshBuildSource> sources, Bounds localBounds, Vector3 position, Quaternion rotation)
		{
			if (sources == null)
			{
				throw new ArgumentNullException("sources");
			}
			NavMeshData navMeshData = new NavMeshData(buildSettings.agentTypeID)
			{
				position = position,
				rotation = rotation
			};
			UpdateNavMeshDataListInternal(navMeshData, buildSettings, sources, localBounds);
			return navMeshData;
		}

		public static bool UpdateNavMeshData(NavMeshData data, NavMeshBuildSettings buildSettings, List<NavMeshBuildSource> sources, Bounds localBounds)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (sources == null)
			{
				throw new ArgumentNullException("sources");
			}
			return UpdateNavMeshDataListInternal(data, buildSettings, sources, localBounds);
		}

		private static bool UpdateNavMeshDataListInternal(NavMeshData data, NavMeshBuildSettings buildSettings, object sources, Bounds localBounds)
		{
			return UpdateNavMeshDataListInternal_Injected(data, ref buildSettings, sources, ref localBounds);
		}

		public static AsyncOperation UpdateNavMeshDataAsync(NavMeshData data, NavMeshBuildSettings buildSettings, List<NavMeshBuildSource> sources, Bounds localBounds)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (sources == null)
			{
				throw new ArgumentNullException("sources");
			}
			return UpdateNavMeshDataAsyncListInternal(data, buildSettings, sources, localBounds);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("GetNavMeshManager().GetNavMeshBuildManager()", StaticAccessorType.Arrow)]
		[NativeMethod("Purge")]
		[NativeHeader("Modules/AI/NavMeshManager.h")]
		public static extern void Cancel(NavMeshData data);

		private static AsyncOperation UpdateNavMeshDataAsyncListInternal(NavMeshData data, NavMeshBuildSettings buildSettings, object sources, Bounds localBounds)
		{
			return UpdateNavMeshDataAsyncListInternal_Injected(data, ref buildSettings, sources, ref localBounds);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern NavMeshBuildSource[] CollectSourcesInternal_Injected(int includedLayerMask, ref Bounds includedWorldBounds, Transform root, bool useBounds, NavMeshCollectGeometry geometry, int defaultArea, bool generateLinksByDefault, NavMeshBuildMarkup[] markups, bool includeOnlyMarkedObjects);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool UpdateNavMeshDataListInternal_Injected(NavMeshData data, ref NavMeshBuildSettings buildSettings, object sources, ref Bounds localBounds);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AsyncOperation UpdateNavMeshDataAsyncListInternal_Injected(NavMeshData data, ref NavMeshBuildSettings buildSettings, object sources, ref Bounds localBounds);
	}
	[MovedFrom("UnityEngine")]
	public enum ObstacleAvoidanceType
	{
		NoObstacleAvoidance,
		LowQualityObstacleAvoidance,
		MedQualityObstacleAvoidance,
		GoodQualityObstacleAvoidance,
		HighQualityObstacleAvoidance
	}
	[MovedFrom("UnityEngine")]
	[NativeHeader("Modules/AI/Components/NavMeshAgent.bindings.h")]
	[NativeHeader("Modules/AI/NavMesh/NavMesh.bindings.h")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.ai.navigation@1.1/manual/NavMeshAgent.html")]
	public sealed class NavMeshAgent : Behaviour
	{
		public Vector3 destination
		{
			get
			{
				get_destination_Injected(out var ret);
				return ret;
			}
			set
			{
				set_destination_Injected(ref value);
			}
		}

		public extern float stoppingDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
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

		[NativeProperty("Position")]
		public Vector3 nextPosition
		{
			get
			{
				get_nextPosition_Injected(out var ret);
				return ret;
			}
			set
			{
				set_nextPosition_Injected(ref value);
			}
		}

		public Vector3 steeringTarget
		{
			get
			{
				get_steeringTarget_Injected(out var ret);
				return ret;
			}
		}

		public Vector3 desiredVelocity
		{
			get
			{
				get_desiredVelocity_Injected(out var ret);
				return ret;
			}
		}

		public extern float remainingDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float baseOffset
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool isOnOffMeshLink
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("IsOnOffMeshLink")]
			get;
		}

		public OffMeshLinkData currentOffMeshLinkData => GetCurrentOffMeshLinkDataInternal();

		public OffMeshLinkData nextOffMeshLinkData => GetNextOffMeshLinkDataInternal();

		public extern bool autoTraverseOffMeshLink
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool autoBraking
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool autoRepath
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool hasPath
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("HasPath")]
			get;
		}

		public extern bool pathPending
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("PathPending")]
			get;
		}

		public extern bool isPathStale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("IsPathStale")]
			get;
		}

		public extern NavMeshPathStatus pathStatus
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("EndPositionOfCurrentPath")]
		public Vector3 pathEndPosition
		{
			get
			{
				get_pathEndPosition_Injected(out var ret);
				return ret;
			}
		}

		public extern bool isStopped
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("NavMeshAgentScriptBindings::GetIsStopped", HasExplicitThis = true)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("NavMeshAgentScriptBindings::SetIsStopped", HasExplicitThis = true)]
			set;
		}

		public NavMeshPath path
		{
			get
			{
				NavMeshPath result = new NavMeshPath();
				CopyPathTo(result);
				return result;
			}
			set
			{
				if (value == null)
				{
					throw new NullReferenceException();
				}
				SetPath(value);
			}
		}

		public Object navMeshOwner => GetOwnerInternal();

		public extern int agentTypeID
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("Use areaMask instead.")]
		public int walkableMask
		{
			get
			{
				return areaMask;
			}
			set
			{
				areaMask = value;
			}
		}

		public extern int areaMask
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float speed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float angularSpeed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float acceleration
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool updatePosition
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool updateRotation
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool updateUpAxis
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
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

		public extern ObstacleAvoidanceType obstacleAvoidanceType
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int avoidancePriority
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool isOnNavMesh
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("InCrowdSystem")]
			get;
		}

		public bool SetDestination(Vector3 target)
		{
			return SetDestination_Injected(ref target);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ActivateCurrentOffMeshLink(bool activated);

		[FreeFunction("NavMeshAgentScriptBindings::GetCurrentOffMeshLinkDataInternal", HasExplicitThis = true)]
		internal OffMeshLinkData GetCurrentOffMeshLinkDataInternal()
		{
			GetCurrentOffMeshLinkDataInternal_Injected(out var ret);
			return ret;
		}

		[FreeFunction("NavMeshAgentScriptBindings::GetNextOffMeshLinkDataInternal", HasExplicitThis = true)]
		internal OffMeshLinkData GetNextOffMeshLinkDataInternal()
		{
			GetNextOffMeshLinkDataInternal_Injected(out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CompleteOffMeshLink();

		public bool Warp(Vector3 newPosition)
		{
			return Warp_Injected(ref newPosition);
		}

		public void Move(Vector3 offset)
		{
			Move_Injected(ref offset);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[Obsolete("Set isStopped to true instead.")]
		public extern void Stop();

		[Obsolete("Set isStopped to true instead.")]
		public void Stop(bool stopUpdates)
		{
			Stop();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[Obsolete("Set isStopped to false instead.")]
		public extern void Resume();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ResetPath();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SetPath([NotNull("ArgumentNullException")] NavMeshPath path);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("CopyPath")]
		internal extern void CopyPathTo([NotNull("ArgumentNullException")] NavMeshPath path);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("DistanceToEdge")]
		public extern bool FindClosestEdge(out NavMeshHit hit);

		public bool Raycast(Vector3 targetPosition, out NavMeshHit hit)
		{
			return Raycast_Injected(ref targetPosition, out hit);
		}

		public bool CalculatePath(Vector3 targetPosition, NavMeshPath path)
		{
			path.ClearCorners();
			return CalculatePathInternal(targetPosition, path);
		}

		[FreeFunction("NavMeshAgentScriptBindings::CalculatePathInternal", HasExplicitThis = true)]
		private bool CalculatePathInternal(Vector3 targetPosition, [NotNull("ArgumentNullException")] NavMeshPath path)
		{
			return CalculatePathInternal_Injected(ref targetPosition, path);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SamplePathPosition(int areaMask, float maxDistance, out NavMeshHit hit);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[Obsolete("Use SetAreaCost instead.")]
		[NativeMethod("SetAreaCost")]
		public extern void SetLayerCost(int layer, float cost);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[Obsolete("Use GetAreaCost instead.")]
		[NativeMethod("GetAreaCost")]
		public extern float GetLayerCost(int layer);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetAreaCost(int areaIndex, float areaCost);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetAreaCost(int areaIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetCurrentPolygonOwner")]
		private extern Object GetOwnerInternal();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool SetDestination_Injected(ref Vector3 target);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_destination_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_destination_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_velocity_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_velocity_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_nextPosition_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_nextPosition_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_steeringTarget_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_desiredVelocity_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetCurrentOffMeshLinkDataInternal_Injected(out OffMeshLinkData ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetNextOffMeshLinkDataInternal_Injected(out OffMeshLinkData ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_pathEndPosition_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Warp_Injected(ref Vector3 newPosition);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Move_Injected(ref Vector3 offset);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Raycast_Injected(ref Vector3 targetPosition, out NavMeshHit hit);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool CalculatePathInternal_Injected(ref Vector3 targetPosition, NavMeshPath path);
	}
	[MovedFrom("UnityEngine")]
	public enum NavMeshObstacleShape
	{
		Capsule,
		Box
	}
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.ai.navigation@1.1/manual/NavMeshObstacle.html")]
	[NativeHeader("Modules/AI/Components/NavMeshObstacle.bindings.h")]
	[MovedFrom("UnityEngine")]
	public sealed class NavMeshObstacle : Behaviour
	{
		public extern float height
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float radius
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
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

		public extern bool carving
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool carveOnlyStationary
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("MoveThreshold")]
		public extern float carvingMoveThreshold
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("TimeToStationary")]
		public extern float carvingTimeToStationary
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern NavMeshObstacleShape shape
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

		public Vector3 size
		{
			[FreeFunction("NavMeshObstacleScriptBindings::GetSize", HasExplicitThis = true)]
			get
			{
				get_size_Injected(out var ret);
				return ret;
			}
			[FreeFunction("NavMeshObstacleScriptBindings::SetSize", HasExplicitThis = true)]
			set
			{
				set_size_Injected(ref value);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("NavMeshObstacleScriptBindings::FitExtents", HasExplicitThis = true)]
		internal extern void FitExtents();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_velocity_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_velocity_Injected(ref Vector3 value);

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
	[MovedFrom("UnityEngine")]
	public enum OffMeshLinkType
	{
		LinkTypeManual,
		LinkTypeDropDown,
		LinkTypeJumpAcross
	}
	[NativeHeader("Modules/AI/Components/OffMeshLink.bindings.h")]
	[MovedFrom("UnityEngine")]
	public struct OffMeshLinkData
	{
		internal int m_Valid;

		internal int m_Activated;

		internal int m_InstanceID;

		internal OffMeshLinkType m_LinkType;

		internal Vector3 m_StartPos;

		internal Vector3 m_EndPos;

		public bool valid => m_Valid != 0;

		public bool activated => m_Activated != 0;

		public OffMeshLinkType linkType => m_LinkType;

		public Vector3 startPos => m_StartPos;

		public Vector3 endPos => m_EndPos;

		public OffMeshLink offMeshLink => GetOffMeshLinkInternal(m_InstanceID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("OffMeshLinkScriptBindings::GetOffMeshLinkInternal")]
		internal static extern OffMeshLink GetOffMeshLinkInternal(int instanceID);
	}
	[MovedFrom("UnityEngine")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.ai.navigation@1.1/manual/OffMeshLink.html")]
	public sealed class OffMeshLink : Behaviour
	{
		public extern bool activated
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool occupied
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float costOverride
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool biDirectional
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("Use area instead.")]
		public int navMeshLayer
		{
			get
			{
				return area;
			}
			set
			{
				area = value;
			}
		}

		public extern int area
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool autoUpdatePositions
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Transform startTransform
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Transform endTransform
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void UpdatePositions();
	}
	[MovedFrom("UnityEngine")]
	public struct NavMeshHit
	{
		private Vector3 m_Position;

		private Vector3 m_Normal;

		private float m_Distance;

		private int m_Mask;

		private int m_Hit;

		public Vector3 position
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

		public int mask
		{
			get
			{
				return m_Mask;
			}
			set
			{
				m_Mask = value;
			}
		}

		public bool hit
		{
			get
			{
				return m_Hit != 0;
			}
			set
			{
				m_Hit = (value ? 1 : 0);
			}
		}
	}
	[UsedByNativeCode]
	[MovedFrom("UnityEngine")]
	public struct NavMeshTriangulation
	{
		public Vector3[] vertices;

		public int[] indices;

		public int[] areas;

		[Obsolete("Use areas instead.")]
		public int[] layers => areas;
	}
	[NativeHeader("Modules/AI/NavMesh/NavMesh.bindings.h")]
	public sealed class NavMeshData : Object
	{
		public Bounds sourceBounds
		{
			get
			{
				get_sourceBounds_Injected(out var ret);
				return ret;
			}
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

		internal extern bool hasHeightMeshData
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("HasHeightMeshData")]
			get;
		}

		internal NavMeshBuildSettings buildSettings
		{
			get
			{
				get_buildSettings_Injected(out var ret);
				return ret;
			}
		}

		public NavMeshData()
		{
			Internal_Create(this, 0);
		}

		public NavMeshData(int agentTypeID)
		{
			Internal_Create(this, agentTypeID);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("NavMeshDataBindings", StaticAccessorType.DoubleColon)]
		private static extern void Internal_Create([Writable] NavMeshData mono, int agentTypeID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_sourceBounds_Injected(out Bounds ret);

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
		[SpecialName]
		private extern void get_buildSettings_Injected(out NavMeshBuildSettings ret);
	}
	public struct NavMeshDataInstance
	{
		public bool valid => id != 0 && NavMesh.IsValidNavMeshDataHandle(id);

		internal int id { get; set; }

		public Object owner
		{
			get
			{
				return NavMesh.InternalGetOwner(id);
			}
			set
			{
				int ownerID = ((value != null) ? value.GetInstanceID() : 0);
				if (!NavMesh.InternalSetOwner(id, ownerID))
				{
					Debug.LogError("Cannot set 'owner' on an invalid NavMeshDataInstance");
				}
			}
		}

		public void Remove()
		{
			NavMesh.RemoveNavMeshDataInternal(id);
		}
	}
	public struct NavMeshLinkData
	{
		private Vector3 m_StartPosition;

		private Vector3 m_EndPosition;

		private float m_CostModifier;

		private int m_Bidirectional;

		private float m_Width;

		private int m_Area;

		private int m_AgentTypeID;

		public Vector3 startPosition
		{
			get
			{
				return m_StartPosition;
			}
			set
			{
				m_StartPosition = value;
			}
		}

		public Vector3 endPosition
		{
			get
			{
				return m_EndPosition;
			}
			set
			{
				m_EndPosition = value;
			}
		}

		public float costModifier
		{
			get
			{
				return m_CostModifier;
			}
			set
			{
				m_CostModifier = value;
			}
		}

		public bool bidirectional
		{
			get
			{
				return m_Bidirectional != 0;
			}
			set
			{
				m_Bidirectional = (value ? 1 : 0);
			}
		}

		public float width
		{
			get
			{
				return m_Width;
			}
			set
			{
				m_Width = value;
			}
		}

		public int area
		{
			get
			{
				return m_Area;
			}
			set
			{
				m_Area = value;
			}
		}

		public int agentTypeID
		{
			get
			{
				return m_AgentTypeID;
			}
			set
			{
				m_AgentTypeID = value;
			}
		}
	}
	public struct NavMeshLinkInstance
	{
		public bool valid => id != 0 && NavMesh.IsValidLinkHandle(id);

		internal int id { get; set; }

		public Object owner
		{
			get
			{
				return NavMesh.InternalGetLinkOwner(id);
			}
			set
			{
				int ownerID = ((value != null) ? value.GetInstanceID() : 0);
				if (!NavMesh.InternalSetLinkOwner(id, ownerID))
				{
					Debug.LogError("Cannot set 'owner' on an invalid NavMeshLinkInstance");
				}
			}
		}

		public void Remove()
		{
			NavMesh.RemoveLinkInternal(id);
		}
	}
	public struct NavMeshQueryFilter
	{
		private const int k_AreaCostElementCount = 32;

		internal float[] costs { get; private set; }

		public int areaMask { get; set; }

		public int agentTypeID { get; set; }

		public float GetAreaCost(int areaIndex)
		{
			if (costs == null)
			{
				if (areaIndex < 0 || areaIndex >= 32)
				{
					string message = $"The valid range is [0:{31}]";
					throw new IndexOutOfRangeException(message);
				}
				return 1f;
			}
			return costs[areaIndex];
		}

		public void SetAreaCost(int areaIndex, float cost)
		{
			if (costs == null)
			{
				costs = new float[32];
				for (int i = 0; i < 32; i++)
				{
					costs[i] = 1f;
				}
			}
			costs[areaIndex] = cost;
		}
	}
	[NativeHeader("Modules/AI/NavMeshManager.h")]
	[MovedFrom("UnityEngine")]
	[StaticAccessor("NavMeshBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/AI/NavMesh/NavMesh.bindings.h")]
	public static class NavMesh
	{
		public delegate void OnNavMeshPreUpdate();

		public const int AllAreas = -1;

		public static OnNavMeshPreUpdate onPreUpdate;

		[StaticAccessor("GetNavMeshManager()")]
		public static extern float avoidancePredictionTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("GetNavMeshManager()")]
		public static extern int pathfindingIterationsPerFrame
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[RequiredByNativeCode]
		private static void Internal_CallOnNavMeshPreUpdate()
		{
			if (onPreUpdate != null)
			{
				onPreUpdate();
			}
		}

		public static bool Raycast(Vector3 sourcePosition, Vector3 targetPosition, out NavMeshHit hit, int areaMask)
		{
			return Raycast_Injected(ref sourcePosition, ref targetPosition, out hit, areaMask);
		}

		public static bool CalculatePath(Vector3 sourcePosition, Vector3 targetPosition, int areaMask, NavMeshPath path)
		{
			path.ClearCorners();
			return CalculatePathInternal(sourcePosition, targetPosition, areaMask, path);
		}

		private static bool CalculatePathInternal(Vector3 sourcePosition, Vector3 targetPosition, int areaMask, NavMeshPath path)
		{
			return CalculatePathInternal_Injected(ref sourcePosition, ref targetPosition, areaMask, path);
		}

		public static bool FindClosestEdge(Vector3 sourcePosition, out NavMeshHit hit, int areaMask)
		{
			return FindClosestEdge_Injected(ref sourcePosition, out hit, areaMask);
		}

		public static bool SamplePosition(Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int areaMask)
		{
			return SamplePosition_Injected(ref sourcePosition, out hit, maxDistance, areaMask);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[Obsolete("Use SetAreaCost instead.")]
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[NativeName("SetAreaCost")]
		public static extern void SetLayerCost(int layer, float cost);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[Obsolete("Use GetAreaCost instead.")]
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[NativeName("GetAreaCost")]
		public static extern float GetLayerCost(int layer);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetAreaFromName")]
		[Obsolete("Use GetAreaFromName instead.")]
		[StaticAccessor("GetNavMeshProjectSettings()")]
		public static extern int GetNavMeshLayerFromName(string layerName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[NativeName("SetAreaCost")]
		public static extern void SetAreaCost(int areaIndex, float cost);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[NativeName("GetAreaCost")]
		public static extern float GetAreaCost(int areaIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("GetNavMeshProjectSettings()")]
		[NativeName("GetAreaFromName")]
		public static extern int GetAreaFromName(string areaName);

		public static NavMeshTriangulation CalculateTriangulation()
		{
			CalculateTriangulation_Injected(out var ret);
			return ret;
		}

		[Obsolete("use NavMesh.CalculateTriangulation() instead.")]
		public static void Triangulate(out Vector3[] vertices, out int[] indices)
		{
			NavMeshTriangulation navMeshTriangulation = CalculateTriangulation();
			vertices = navMeshTriangulation.vertices;
			indices = navMeshTriangulation.indices;
		}

		[Obsolete("AddOffMeshLinks has no effect and is deprecated.")]
		public static void AddOffMeshLinks()
		{
		}

		[Obsolete("RestoreNavMesh has no effect and is deprecated.")]
		public static void RestoreNavMesh()
		{
		}

		public static NavMeshDataInstance AddNavMeshData(NavMeshData navMeshData)
		{
			if (navMeshData == null)
			{
				throw new ArgumentNullException("navMeshData");
			}
			return new NavMeshDataInstance
			{
				id = AddNavMeshDataInternal(navMeshData)
			};
		}

		public static NavMeshDataInstance AddNavMeshData(NavMeshData navMeshData, Vector3 position, Quaternion rotation)
		{
			if (navMeshData == null)
			{
				throw new ArgumentNullException("navMeshData");
			}
			return new NavMeshDataInstance
			{
				id = AddNavMeshDataTransformedInternal(navMeshData, position, rotation)
			};
		}

		public static void RemoveNavMeshData(NavMeshDataInstance handle)
		{
			RemoveNavMeshDataInternal(handle.id);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("GetNavMeshManager()")]
		[NativeName("IsValidSurfaceID")]
		internal static extern bool IsValidNavMeshDataHandle(int handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("GetNavMeshManager()")]
		internal static extern bool IsValidLinkHandle(int handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Object InternalGetOwner(int dataID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("GetNavMeshManager()")]
		[NativeName("SetSurfaceUserID")]
		internal static extern bool InternalSetOwner(int dataID, int ownerID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Object InternalGetLinkOwner(int linkID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("SetLinkUserID")]
		[StaticAccessor("GetNavMeshManager()")]
		internal static extern bool InternalSetLinkOwner(int linkID, int ownerID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("LoadData")]
		[StaticAccessor("GetNavMeshManager()")]
		internal static extern int AddNavMeshDataInternal(NavMeshData navMeshData);

		[NativeName("LoadData")]
		[StaticAccessor("GetNavMeshManager()")]
		internal static int AddNavMeshDataTransformedInternal(NavMeshData navMeshData, Vector3 position, Quaternion rotation)
		{
			return AddNavMeshDataTransformedInternal_Injected(navMeshData, ref position, ref rotation);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("UnloadData")]
		[StaticAccessor("GetNavMeshManager()")]
		internal static extern void RemoveNavMeshDataInternal(int handle);

		public static NavMeshLinkInstance AddLink(NavMeshLinkData link)
		{
			return new NavMeshLinkInstance
			{
				id = AddLinkInternal(link, Vector3.zero, Quaternion.identity)
			};
		}

		public static NavMeshLinkInstance AddLink(NavMeshLinkData link, Vector3 position, Quaternion rotation)
		{
			return new NavMeshLinkInstance
			{
				id = AddLinkInternal(link, position, rotation)
			};
		}

		public static void RemoveLink(NavMeshLinkInstance handle)
		{
			RemoveLinkInternal(handle.id);
		}

		[NativeName("AddLink")]
		[StaticAccessor("GetNavMeshManager()")]
		internal static int AddLinkInternal(NavMeshLinkData link, Vector3 position, Quaternion rotation)
		{
			return AddLinkInternal_Injected(ref link, ref position, ref rotation);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("GetNavMeshManager()")]
		[NativeName("RemoveLink")]
		internal static extern void RemoveLinkInternal(int handle);

		public static bool SamplePosition(Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, NavMeshQueryFilter filter)
		{
			return SamplePositionFilter(sourcePosition, out hit, maxDistance, filter.agentTypeID, filter.areaMask);
		}

		private static bool SamplePositionFilter(Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int type, int mask)
		{
			return SamplePositionFilter_Injected(ref sourcePosition, out hit, maxDistance, type, mask);
		}

		public static bool FindClosestEdge(Vector3 sourcePosition, out NavMeshHit hit, NavMeshQueryFilter filter)
		{
			return FindClosestEdgeFilter(sourcePosition, out hit, filter.agentTypeID, filter.areaMask);
		}

		private static bool FindClosestEdgeFilter(Vector3 sourcePosition, out NavMeshHit hit, int type, int mask)
		{
			return FindClosestEdgeFilter_Injected(ref sourcePosition, out hit, type, mask);
		}

		public static bool Raycast(Vector3 sourcePosition, Vector3 targetPosition, out NavMeshHit hit, NavMeshQueryFilter filter)
		{
			return RaycastFilter(sourcePosition, targetPosition, out hit, filter.agentTypeID, filter.areaMask);
		}

		private static bool RaycastFilter(Vector3 sourcePosition, Vector3 targetPosition, out NavMeshHit hit, int type, int mask)
		{
			return RaycastFilter_Injected(ref sourcePosition, ref targetPosition, out hit, type, mask);
		}

		public static bool CalculatePath(Vector3 sourcePosition, Vector3 targetPosition, NavMeshQueryFilter filter, NavMeshPath path)
		{
			path.ClearCorners();
			return CalculatePathFilterInternal(sourcePosition, targetPosition, path, filter.agentTypeID, filter.areaMask, filter.costs);
		}

		private static bool CalculatePathFilterInternal(Vector3 sourcePosition, Vector3 targetPosition, NavMeshPath path, int type, int mask, float[] costs)
		{
			return CalculatePathFilterInternal_Injected(ref sourcePosition, ref targetPosition, path, type, mask, costs);
		}

		[StaticAccessor("GetNavMeshProjectSettings()")]
		public static NavMeshBuildSettings CreateSettings()
		{
			CreateSettings_Injected(out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("GetNavMeshProjectSettings()")]
		public static extern void RemoveSettings(int agentTypeID);

		public static NavMeshBuildSettings GetSettingsByID(int agentTypeID)
		{
			GetSettingsByID_Injected(agentTypeID, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("GetNavMeshProjectSettings()")]
		public static extern int GetSettingsCount();

		public static NavMeshBuildSettings GetSettingsByIndex(int index)
		{
			GetSettingsByIndex_Injected(index, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetSettingsNameFromID(int agentTypeID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("CleanupAfterCarving")]
		[StaticAccessor("GetNavMeshManager()")]
		public static extern void RemoveAllNavMeshData();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Raycast_Injected(ref Vector3 sourcePosition, ref Vector3 targetPosition, out NavMeshHit hit, int areaMask);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CalculatePathInternal_Injected(ref Vector3 sourcePosition, ref Vector3 targetPosition, int areaMask, NavMeshPath path);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool FindClosestEdge_Injected(ref Vector3 sourcePosition, out NavMeshHit hit, int areaMask);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SamplePosition_Injected(ref Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int areaMask);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CalculateTriangulation_Injected(out NavMeshTriangulation ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddNavMeshDataTransformedInternal_Injected(NavMeshData navMeshData, ref Vector3 position, ref Quaternion rotation);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddLinkInternal_Injected(ref NavMeshLinkData link, ref Vector3 position, ref Quaternion rotation);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SamplePositionFilter_Injected(ref Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int type, int mask);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool FindClosestEdgeFilter_Injected(ref Vector3 sourcePosition, out NavMeshHit hit, int type, int mask);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RaycastFilter_Injected(ref Vector3 sourcePosition, ref Vector3 targetPosition, out NavMeshHit hit, int type, int mask);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CalculatePathFilterInternal_Injected(ref Vector3 sourcePosition, ref Vector3 targetPosition, NavMeshPath path, int type, int mask, float[] costs);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateSettings_Injected(out NavMeshBuildSettings ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSettingsByID_Injected(int agentTypeID, out NavMeshBuildSettings ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSettingsByIndex_Injected(int index, out NavMeshBuildSettings ret);
	}
	[MovedFrom("UnityEngine")]
	public enum NavMeshPathStatus
	{
		PathComplete,
		PathPartial,
		PathInvalid
	}
	[StructLayout(LayoutKind.Sequential)]
	[MovedFrom("UnityEngine")]
	[NativeHeader("Modules/AI/NavMeshPath.bindings.h")]
	public sealed class NavMeshPath
	{
		internal IntPtr m_Ptr;

		internal Vector3[] m_Corners;

		public Vector3[] corners
		{
			get
			{
				CalculateCorners();
				return m_Corners;
			}
		}

		public extern NavMeshPathStatus status
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public NavMeshPath()
		{
			m_Ptr = InitializeNavMeshPath();
		}

		~NavMeshPath()
		{
			DestroyNavMeshPath(m_Ptr);
			m_Ptr = IntPtr.Zero;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("NavMeshPathScriptBindings::InitializeNavMeshPath")]
		private static extern IntPtr InitializeNavMeshPath();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("NavMeshPathScriptBindings::DestroyNavMeshPath", IsThreadSafe = true)]
		private static extern void DestroyNavMeshPath(IntPtr ptr);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("NavMeshPathScriptBindings::GetCornersNonAlloc", HasExplicitThis = true)]
		public extern int GetCornersNonAlloc([Out] Vector3[] results);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("NavMeshPathScriptBindings::CalculateCornersInternal", HasExplicitThis = true)]
		private extern Vector3[] CalculateCornersInternal();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("NavMeshPathScriptBindings::ClearCornersInternal", HasExplicitThis = true)]
		private extern void ClearCornersInternal();

		public void ClearCorners()
		{
			ClearCornersInternal();
			m_Corners = null;
		}

		private void CalculateCorners()
		{
			if (m_Corners == null)
			{
				m_Corners = CalculateCornersInternal();
			}
		}
	}
	[Flags]
	public enum NavMeshBuildDebugFlags
	{
		None = 0,
		InputGeometry = 1,
		Voxels = 2,
		Regions = 4,
		RawContours = 8,
		SimplifiedContours = 0x10,
		PolygonMeshes = 0x20,
		PolygonMeshesDetail = 0x40,
		All = 0x7F
	}
	public enum NavMeshBuildSourceShape
	{
		Mesh,
		Terrain,
		Box,
		Sphere,
		Capsule,
		ModifierBox
	}
	public enum NavMeshCollectGeometry
	{
		RenderMeshes,
		PhysicsColliders
	}
	[UsedByNativeCode]
	[NativeHeader("Modules/AI/Public/NavMeshBindingTypes.h")]
	public struct NavMeshBuildSource
	{
		private Matrix4x4 m_Transform;

		private Vector3 m_Size;

		private NavMeshBuildSourceShape m_Shape;

		private int m_Area;

		private int m_InstanceID;

		private int m_ComponentID;

		private int m_GenerateLinks;

		public Matrix4x4 transform
		{
			get
			{
				return m_Transform;
			}
			set
			{
				m_Transform = value;
			}
		}

		public Vector3 size
		{
			get
			{
				return m_Size;
			}
			set
			{
				m_Size = value;
			}
		}

		public NavMeshBuildSourceShape shape
		{
			get
			{
				return m_Shape;
			}
			set
			{
				m_Shape = value;
			}
		}

		public int area
		{
			get
			{
				return m_Area;
			}
			set
			{
				m_Area = value;
			}
		}

		public bool generateLinks
		{
			get
			{
				return m_GenerateLinks != 0;
			}
			set
			{
				m_GenerateLinks = (value ? 1 : 0);
			}
		}

		public Object sourceObject
		{
			get
			{
				return InternalGetObject(m_InstanceID);
			}
			set
			{
				m_InstanceID = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		public Component component
		{
			get
			{
				return InternalGetComponent(m_ComponentID);
			}
			set
			{
				m_ComponentID = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("NavMeshBuildSource", StaticAccessorType.DoubleColon)]
		private static extern Component InternalGetComponent(int instanceID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("NavMeshBuildSource", StaticAccessorType.DoubleColon)]
		private static extern Object InternalGetObject(int instanceID);
	}
	[NativeHeader("Modules/AI/Public/NavMeshBindingTypes.h")]
	public struct NavMeshBuildMarkup
	{
		private int m_OverrideArea;

		private int m_Area;

		private int m_InheritIgnoreFromBuild;

		private int m_IgnoreFromBuild;

		private int m_OverrideGenerateLinks;

		private int m_GenerateLinks;

		private int m_InstanceID;

		private int m_IgnoreChildren;

		public bool overrideArea
		{
			get
			{
				return m_OverrideArea != 0;
			}
			set
			{
				m_OverrideArea = (value ? 1 : 0);
			}
		}

		public int area
		{
			get
			{
				return m_Area;
			}
			set
			{
				m_Area = value;
			}
		}

		public bool overrideIgnore
		{
			get
			{
				return m_InheritIgnoreFromBuild == 0;
			}
			set
			{
				m_InheritIgnoreFromBuild = ((!value) ? 1 : 0);
			}
		}

		public bool ignoreFromBuild
		{
			get
			{
				return m_IgnoreFromBuild != 0;
			}
			set
			{
				m_IgnoreFromBuild = (value ? 1 : 0);
			}
		}

		public bool overrideGenerateLinks
		{
			get
			{
				return m_OverrideGenerateLinks != 0;
			}
			set
			{
				m_OverrideGenerateLinks = (value ? 1 : 0);
			}
		}

		public bool generateLinks
		{
			get
			{
				return m_GenerateLinks != 0;
			}
			set
			{
				m_GenerateLinks = (value ? 1 : 0);
			}
		}

		public bool applyToChildren
		{
			get
			{
				return m_IgnoreChildren == 0;
			}
			set
			{
				m_IgnoreChildren = ((!value) ? 1 : 0);
			}
		}

		public Transform root
		{
			get
			{
				return InternalGetRootGO(m_InstanceID);
			}
			set
			{
				m_InstanceID = ((value != null) ? value.GetInstanceID() : 0);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("NavMeshBuildMarkup", StaticAccessorType.DoubleColon)]
		private static extern Transform InternalGetRootGO(int instanceID);
	}
	[NativeHeader("Modules/AI/Public/NavMeshBuildSettings.h")]
	public struct NavMeshBuildSettings
	{
		private int m_AgentTypeID;

		private float m_AgentRadius;

		private float m_AgentHeight;

		private float m_AgentSlope;

		private float m_AgentClimb;

		private float m_LedgeDropHeight;

		private float m_MaxJumpAcrossDistance;

		private float m_MinRegionArea;

		private int m_OverrideVoxelSize;

		private float m_VoxelSize;

		private int m_OverrideTileSize;

		private int m_TileSize;

		private int m_BuildHeightMesh;

		private uint m_MaxJobWorkers;

		private int m_PreserveTilesOutsideBounds;

		private NavMeshBuildDebugSettings m_Debug;

		public int agentTypeID
		{
			get
			{
				return m_AgentTypeID;
			}
			set
			{
				m_AgentTypeID = value;
			}
		}

		public float agentRadius
		{
			get
			{
				return m_AgentRadius;
			}
			set
			{
				m_AgentRadius = value;
			}
		}

		public float agentHeight
		{
			get
			{
				return m_AgentHeight;
			}
			set
			{
				m_AgentHeight = value;
			}
		}

		public float agentSlope
		{
			get
			{
				return m_AgentSlope;
			}
			set
			{
				m_AgentSlope = value;
			}
		}

		public float agentClimb
		{
			get
			{
				return m_AgentClimb;
			}
			set
			{
				m_AgentClimb = value;
			}
		}

		public float ledgeDropHeight
		{
			get
			{
				return m_LedgeDropHeight;
			}
			set
			{
				m_LedgeDropHeight = value;
			}
		}

		public float maxJumpAcrossDistance
		{
			get
			{
				return m_MaxJumpAcrossDistance;
			}
			set
			{
				m_MaxJumpAcrossDistance = value;
			}
		}

		public float minRegionArea
		{
			get
			{
				return m_MinRegionArea;
			}
			set
			{
				m_MinRegionArea = value;
			}
		}

		public bool overrideVoxelSize
		{
			get
			{
				return m_OverrideVoxelSize != 0;
			}
			set
			{
				m_OverrideVoxelSize = (value ? 1 : 0);
			}
		}

		public float voxelSize
		{
			get
			{
				return m_VoxelSize;
			}
			set
			{
				m_VoxelSize = value;
			}
		}

		public bool overrideTileSize
		{
			get
			{
				return m_OverrideTileSize != 0;
			}
			set
			{
				m_OverrideTileSize = (value ? 1 : 0);
			}
		}

		public int tileSize
		{
			get
			{
				return m_TileSize;
			}
			set
			{
				m_TileSize = value;
			}
		}

		public uint maxJobWorkers
		{
			get
			{
				return m_MaxJobWorkers;
			}
			set
			{
				m_MaxJobWorkers = value;
			}
		}

		public bool preserveTilesOutsideBounds
		{
			get
			{
				return m_PreserveTilesOutsideBounds != 0;
			}
			set
			{
				m_PreserveTilesOutsideBounds = (value ? 1 : 0);
			}
		}

		public bool buildHeightMesh
		{
			get
			{
				return m_BuildHeightMesh != 0;
			}
			set
			{
				m_BuildHeightMesh = (value ? 1 : 0);
			}
		}

		public NavMeshBuildDebugSettings debug
		{
			get
			{
				return m_Debug;
			}
			set
			{
				m_Debug = value;
			}
		}

		public string[] ValidationReport(Bounds buildBounds)
		{
			return InternalValidationReport(this, buildBounds);
		}

		[NativeHeader("Modules/AI/Public/NavMeshBuildSettings.h")]
		[FreeFunction]
		private static string[] InternalValidationReport(NavMeshBuildSettings buildSettings, Bounds buildBounds)
		{
			return InternalValidationReport_Injected(ref buildSettings, ref buildBounds);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] InternalValidationReport_Injected(ref NavMeshBuildSettings buildSettings, ref Bounds buildBounds);
	}
	[NativeHeader("Modules/AI/Public/NavMeshBuildDebugSettings.h")]
	public struct NavMeshBuildDebugSettings
	{
		private byte m_Flags;

		public NavMeshBuildDebugFlags flags
		{
			get
			{
				return (NavMeshBuildDebugFlags)m_Flags;
			}
			set
			{
				m_Flags = (byte)value;
			}
		}
	}
}
