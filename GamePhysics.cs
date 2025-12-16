using System;
using System.Collections.Generic;
using ConVar;
using Facepunch;
using GamePhysicsJobs;
using Unity.Collections;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using UtilityJobs;

public static class GamePhysics
{
	public enum Realm
	{
		Client,
		Server
	}

	[Flags]
	public enum MasksToValidate : byte
	{
		None = 0,
		Terrain = 1,
		Water = 2,
		All = 3
	}

	public const int BufferLength = 32768;

	private static RaycastHit[] hitBuffer = new RaycastHit[32768];

	private static RaycastHit[] hitBufferB = new RaycastHit[32768];

	private static Collider[] colBuffer = new Collider[32768];

	public const int DefaultMaxResultsPerQuery = 16;

	public static bool CheckSphere(Vector3 position, float radius, int layerMask = -5, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.UseGlobal)
	{
		layerMask = HandleIgnoreCollision(position, layerMask);
		return UnityEngine.Physics.CheckSphere(position, radius, layerMask, triggerInteraction);
	}

	public static void CheckSpheres(NativeArray<Vector3>.ReadOnly pos, NativeArray<float>.ReadOnly radii, NativeArray<int>.ReadOnly layerMasks, NativeArray<bool> results, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Ignore, MasksToValidate validate = MasksToValidate.All)
	{
		using (TimeWarning.New("GamePhysics.CheckSpheres"))
		{
			NativeArray<ColliderHit> hits = new NativeArray<ColliderHit>(pos.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			OverlapSpheres(pos, radii, layerMasks, hits, 1, triggerInteraction, validate);
			CheckHitsJob jobData = new CheckHitsJob
			{
				Results = results,
				Hits = hits.AsReadOnly()
			};
			IJobExtensions.RunByRef(ref jobData);
			hits.Dispose();
		}
	}

	public static bool CheckCapsule(Vector3 start, Vector3 end, float radius, int layerMask = -5, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.UseGlobal)
	{
		layerMask = HandleIgnoreCollision((start + end) * 0.5f, layerMask);
		return UnityEngine.Physics.CheckCapsule(start, end, radius, layerMask, triggerInteraction);
	}

	public static void CheckCapsules(NativeArray<Vector3>.ReadOnly starts, NativeArray<Vector3>.ReadOnly ends, NativeArray<float>.ReadOnly radii, NativeArray<int>.ReadOnly layerMasks, NativeArray<bool> results, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Ignore, MasksToValidate validate = MasksToValidate.All, bool mitigateSpheres = true)
	{
		using (TimeWarning.New("GamePhysics.CheckCapsules"))
		{
			NativeArray<int>.ReadOnly layerMasks2 = layerMasks;
			NativeArray<int> array = default(NativeArray<int>);
			if (validate != MasksToValidate.None)
			{
				array = new NativeArray<int>(layerMasks.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				layerMasks.CopyTo(array);
				NativeArray<Vector3> results2 = new NativeArray<Vector3>(starts.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				CalcMidpoingJob jobData = new CalcMidpoingJob
				{
					Results = results2,
					From = starts,
					To = ends
				};
				IJobExtensions.RunByRef(ref jobData);
				HandleIgnoreCollision(results2.AsReadOnly(), array, validate);
				results2.Dispose();
				layerMasks2 = array.AsReadOnly();
			}
			NativeArray<OverlapCapsuleCommand> nativeArray = new NativeArray<OverlapCapsuleCommand>(starts.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			GenerateOverlapCapsuleCommandsJob jobData2 = new GenerateOverlapCapsuleCommandsJob
			{
				CapsuleCommands = nativeArray,
				From = starts,
				To = ends,
				Radiii = radii,
				LayerMasks = layerMasks2,
				TriggerInteraction = triggerInteraction,
				HitBackfaces = false,
				HitMultipleFaces = false
			};
			IJobExtensions.RunByRef(ref jobData2);
			NativeArrayEx.SafeDispose(ref array);
			NativeArray<ColliderHit> hits = new NativeArray<ColliderHit>(starts.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			if (mitigateSpheres)
			{
				MitigateSphereCapsuleCommands(nativeArray, hits, 1);
			}
			else
			{
				ExecuteOverlapCapsuleCommands(nativeArray, hits, 1);
			}
			nativeArray.Dispose();
			CheckHitsJob jobData3 = new CheckHitsJob
			{
				Results = results,
				Hits = hits.AsReadOnly()
			};
			IJobExtensions.RunByRef(ref jobData3);
			hits.Dispose();
		}
	}

	public static bool CheckOBB(OBB obb, int layerMask = -5, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.UseGlobal)
	{
		layerMask = HandleIgnoreCollision(obb.position, layerMask);
		return UnityEngine.Physics.CheckBox(obb.position, obb.extents, obb.rotation, layerMask, triggerInteraction);
	}

	public static bool CheckOBBAndEntity(OBB obb, int layerMask = -5, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.UseGlobal, BaseEntity ignoreEntity = null)
	{
		layerMask = HandleIgnoreCollision(obb.position, layerMask);
		int num = UnityEngine.Physics.OverlapBoxNonAlloc(obb.position, obb.extents, colBuffer, obb.rotation, layerMask, triggerInteraction);
		for (int i = 0; i < num; i++)
		{
			BaseEntity baseEntity = GameObjectEx.ToBaseEntity(colBuffer[i]);
			if (!(baseEntity != null) || !(ignoreEntity != null) || (baseEntity.isServer == ignoreEntity.isServer && !(baseEntity == ignoreEntity)))
			{
				return true;
			}
		}
		return false;
	}

	public static bool CheckBounds(Bounds bounds, int layerMask = -5, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.UseGlobal)
	{
		layerMask = HandleIgnoreCollision(bounds.center, layerMask);
		return UnityEngine.Physics.CheckBox(bounds.center, bounds.extents, Quaternion.identity, layerMask, triggerInteraction);
	}

	public static void CheckBounds(NativeArray<Vector3>.ReadOnly centers, NativeArray<Vector3>.ReadOnly halfExtents, NativeArray<int>.ReadOnly layerMasks, NativeArray<bool> results, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Ignore, MasksToValidate validate = MasksToValidate.All)
	{
		NativeArray<int>.ReadOnly layerMasks2 = layerMasks;
		NativeArray<int> array = default(NativeArray<int>);
		if (validate != MasksToValidate.None)
		{
			array = new NativeArray<int>(layerMasks.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			layerMasks.CopyTo(array);
			HandleIgnoreCollision(centers, array, validate);
			layerMasks2 = array.AsReadOnly();
		}
		NativeArray<OverlapBoxCommand> nativeArray = new NativeArray<OverlapBoxCommand>(centers.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
		new GenerateOverlapBoxCommandsJob
		{
			CapsuleCommands = nativeArray,
			Centers = centers,
			Extents = halfExtents,
			LayerMasks = layerMasks2,
			TriggerInteraction = triggerInteraction,
			HitBackfaces = false,
			HitMultipleFaces = false
		}.Run();
		NativeArrayEx.SafeDispose(ref array);
		NativeArray<ColliderHit> hits = new NativeArray<ColliderHit>(centers.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
		ExecuteOverlapBoxCommands(nativeArray, hits, 1);
		CheckHitsJob jobData = new CheckHitsJob
		{
			Results = results,
			Hits = hits.AsReadOnly()
		};
		IJobExtensions.RunByRef(ref jobData);
		hits.Dispose();
	}

	private static void ExecuteOverlapBoxCommands(NativeArray<OverlapBoxCommand> commands, NativeArray<ColliderHit> hits, int maxResPerCast)
	{
		if (Debug.isDebugBuild)
		{
			NativeList<int> invalidIndices = new NativeList<int>(commands.Length, Allocator.TempJob);
			ValidateOverlapBoxCommandsJob jobData = new ValidateOverlapBoxCommandsJob
			{
				InvalidIndices = invalidIndices,
				Commands = commands.AsReadOnly()
			};
			IJobExtensions.RunByRef(ref jobData);
			if (!invalidIndices.IsEmpty)
			{
				int num = invalidIndices[0];
				OverlapBoxCommand overlapBoxCommand = commands[num];
				Debug.LogError(string.Concat(string.Concat(string.Concat($"OverlapBox has {invalidIndices.Length} invalid box commands!" + $"\nFirst one was at index {num}:", $"\n\tCenter: {overlapBoxCommand.center}"), $"\n\tExtents: {overlapBoxCommand.halfExtents}"), "\nThese queries will be skipped!"));
			}
			invalidIndices.Dispose();
		}
		int batchSize = GetBatchSize(commands.Length);
		OverlapBoxCommand.ScheduleBatch(commands, hits, batchSize, maxResPerCast).Complete();
	}

	public static bool CheckInsideNonConvexMesh(Vector3 point, int layerMask = -5)
	{
		bool queriesHitBackfaces = UnityEngine.Physics.queriesHitBackfaces;
		UnityEngine.Physics.queriesHitBackfaces = true;
		int num = UnityEngine.Physics.RaycastNonAlloc(point, Vector3.up, hitBuffer, 100f, layerMask);
		int num2 = UnityEngine.Physics.RaycastNonAlloc(point, -Vector3.up, hitBufferB, 100f, layerMask);
		if (num >= hitBuffer.Length)
		{
			Debug.LogWarning("CheckInsideNonConvexMesh query is exceeding hitBuffer length.");
			return false;
		}
		if (num2 > hitBufferB.Length)
		{
			Debug.LogWarning("CheckInsideNonConvexMesh query is exceeding hitBufferB length.");
			return false;
		}
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				if (hitBuffer[i].collider == hitBufferB[j].collider)
				{
					UnityEngine.Physics.queriesHitBackfaces = queriesHitBackfaces;
					return true;
				}
			}
		}
		UnityEngine.Physics.queriesHitBackfaces = queriesHitBackfaces;
		return false;
	}

	public static bool CheckInsideAnyCollider(Vector3 point, int layerMask = -5)
	{
		if (UnityEngine.Physics.CheckSphere(point, 0f, layerMask))
		{
			return true;
		}
		if (CheckInsideNonConvexMesh(point, layerMask))
		{
			return true;
		}
		if (TerrainMeta.HeightMap != null && TerrainMeta.HeightMap.GetHeight(point) > point.y)
		{
			return true;
		}
		return false;
	}

	public static void OverlapSphere(Vector3 position, float radius, List<Collider> list, int layerMask = -5, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Ignore)
	{
		layerMask = HandleIgnoreCollision(position, layerMask);
		int count = UnityEngine.Physics.OverlapSphereNonAlloc(position, radius, colBuffer, layerMask, triggerInteraction);
		BufferToList(colBuffer, count, list);
	}

	public static void OverlapSpheres(NativeArray<Vector3>.ReadOnly positions, NativeArray<float>.ReadOnly radii, NativeArray<int>.ReadOnly layerMasks, NativeArray<ColliderHit> hits, int maxResPerCast, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Ignore, MasksToValidate validate = MasksToValidate.All)
	{
		using (TimeWarning.New("GamePhysics.OverlapSpheres"))
		{
			NativeArray<int>.ReadOnly layerMasks2 = layerMasks;
			NativeArray<int> array = default(NativeArray<int>);
			if (validate != MasksToValidate.None)
			{
				array = new NativeArray<int>(layerMasks.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				layerMasks.CopyTo(array);
				HandleIgnoreCollision(positions, array, validate);
				layerMasks2 = array.AsReadOnly();
			}
			NativeArray<OverlapSphereCommand> nativeArray = new NativeArray<OverlapSphereCommand>(positions.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			GenerateOverlapSphereCommandsJob jobData = new GenerateOverlapSphereCommandsJob
			{
				SphereCommands = nativeArray,
				Pos = positions,
				Radiii = radii,
				LayerMasks = layerMasks2,
				TriggerInteraction = triggerInteraction,
				HitBackfaces = false,
				HitMultipleFaces = false
			};
			IJobExtensions.RunByRef(ref jobData);
			NativeArrayEx.SafeDispose(ref array);
			ExecuteOverlapSphereCommands(nativeArray, hits, maxResPerCast);
			nativeArray.Dispose();
		}
	}

	private static void ExecuteOverlapSphereCommands(NativeArray<OverlapSphereCommand> commands, NativeArray<ColliderHit> hits, int maxResPerCast)
	{
		if (Debug.isDebugBuild)
		{
			NativeList<int> invalidIndices = new NativeList<int>(commands.Length, Allocator.TempJob);
			ValidateOverlapSphereCommandsJob jobData = new ValidateOverlapSphereCommandsJob
			{
				InvalidIndices = invalidIndices,
				Commands = commands.AsReadOnly()
			};
			IJobExtensions.RunByRef(ref jobData);
			if (!invalidIndices.IsEmpty)
			{
				int num = invalidIndices[0];
				OverlapSphereCommand overlapSphereCommand = commands[num];
				Debug.LogError(string.Concat(string.Concat(string.Concat($"OverlapSpheres has {invalidIndices.Length} invalid sphere commands!" + $"\nFirst one was at index {num}:", $"\n\tPos: {overlapSphereCommand.point}"), $"\n\tRadius: {overlapSphereCommand.radius}"), "\nThese queries will be skipped!"));
			}
			invalidIndices.Dispose();
		}
		int batchSize = GetBatchSize(commands.Length);
		OverlapSphereCommand.ScheduleBatch(commands, hits, batchSize, maxResPerCast).Complete();
	}

	public static void OBBSweep(OBB obb, Vector3 direction, float distance, List<RaycastHit> list, int layerMask = -5, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Ignore)
	{
		layerMask = HandleIgnoreCollision(obb.position, layerMask);
		HitBufferToList(UnityEngine.Physics.BoxCastNonAlloc(obb.position, obb.extents, direction, hitBuffer, obb.rotation, distance, layerMask, triggerInteraction), list);
	}

	public static void CapsuleSweep(Vector3 position0, Vector3 position1, float radius, Vector3 direction, float distance, List<RaycastHit> list, int layerMask = -5, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Ignore)
	{
		layerMask = HandleIgnoreCollision(position0, layerMask);
		layerMask = HandleIgnoreCollision(position1, layerMask);
		HitBufferToList(UnityEngine.Physics.CapsuleCastNonAlloc(position0, position1, radius, direction, hitBuffer, distance, layerMask, triggerInteraction), list);
	}

	public static void OverlapCapsule(Vector3 point0, Vector3 point1, float radius, List<Collider> list, int layerMask = -5, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Ignore)
	{
		layerMask = HandleIgnoreCollision(point0, layerMask);
		layerMask = HandleIgnoreCollision(point1, layerMask);
		int count = UnityEngine.Physics.OverlapCapsuleNonAlloc(point0, point1, radius, colBuffer, layerMask, triggerInteraction);
		BufferToList(colBuffer, count, list);
	}

	public static void OverlapCapsules(NativeArray<Vector3>.ReadOnly starts, NativeArray<Vector3>.ReadOnly ends, NativeArray<float>.ReadOnly radii, NativeArray<int>.ReadOnly layerMasks, NativeArray<ColliderHit> hits, int maxResPerCast, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Ignore, MasksToValidate validate = MasksToValidate.All, bool mitigateSpheres = true)
	{
		using (TimeWarning.New("GamePhysics.OverlapCapsules"))
		{
			NativeArray<int>.ReadOnly layerMasks2 = layerMasks;
			NativeArray<int> array = default(NativeArray<int>);
			if (validate != MasksToValidate.None)
			{
				array = new NativeArray<int>(layerMasks.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				layerMasks.CopyTo(array);
				HandleIgnoreCollision(starts, array, validate);
				HandleIgnoreCollision(ends, array, validate);
				layerMasks2 = array.AsReadOnly();
			}
			NativeArray<OverlapCapsuleCommand> nativeArray = new NativeArray<OverlapCapsuleCommand>(starts.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			GenerateOverlapCapsuleCommandsJob jobData = new GenerateOverlapCapsuleCommandsJob
			{
				CapsuleCommands = nativeArray,
				From = starts,
				To = ends,
				Radiii = radii,
				LayerMasks = layerMasks2,
				TriggerInteraction = triggerInteraction,
				HitBackfaces = false,
				HitMultipleFaces = false
			};
			IJobExtensions.RunByRef(ref jobData);
			NativeArrayEx.SafeDispose(ref array);
			if (mitigateSpheres)
			{
				MitigateSphereCapsuleCommands(nativeArray, hits, maxResPerCast);
			}
			else
			{
				ExecuteOverlapCapsuleCommands(nativeArray, hits, maxResPerCast);
			}
			nativeArray.Dispose();
		}
	}

	private static void MitigateSphereCapsuleCommands(NativeArray<OverlapCapsuleCommand> commands, NativeArray<ColliderHit> hits, int maxResPerCast)
	{
		NativeList<int> nativeList = new NativeList<int>(commands.Length, Allocator.TempJob);
		FindSphereCmdsInCapsuleCmdsJob jobData = new FindSphereCmdsInCapsuleCmdsJob
		{
			SphereIndices = nativeList,
			Commands = commands.AsReadOnly()
		};
		IJobExtensions.RunByRef(ref jobData);
		if (nativeList.IsEmpty)
		{
			nativeList.Dispose();
			ExecuteOverlapCapsuleCommands(commands, hits, maxResPerCast);
			return;
		}
		int num = Math.Max(nativeList.Length, commands.Length - nativeList.Length);
		NativeArray<ColliderHit> hits2 = new NativeArray<ColliderHit>(num * maxResPerCast, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
		NativeList<OverlapSphereCommand> sphereCommands = new NativeList<OverlapSphereCommand>(nativeList.Length, Allocator.TempJob);
		GenerateSphereCmdsFromCapsuleCmdsJob jobData2 = new GenerateSphereCmdsFromCapsuleCmdsJob
		{
			SphereCommands = sphereCommands,
			Commands = commands.AsReadOnly(),
			Indices = nativeList.AsReadOnly()
		};
		IJobExtensions.RunByRef(ref jobData2);
		ExecuteOverlapSphereCommands(sphereCommands.AsArray(), hits2, maxResPerCast);
		ScatterColliderHitsJob jobData3 = new ScatterColliderHitsJob
		{
			To = hits,
			From = hits2.AsReadOnly(),
			Indices = nativeList.AsReadOnly(),
			MaxHitsPerRay = maxResPerCast
		};
		IJobExtensions.RunByRef(ref jobData3);
		sphereCommands.Dispose();
		if (nativeList.Length != commands.Length)
		{
			NativeArray<bool> workBuffer = new NativeArray<bool>(commands.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			InvertIndexListJob jobData4 = new InvertIndexListJob
			{
				Indices = nativeList,
				WorkBuffer = workBuffer
			};
			IJobExtensions.RunByRef(ref jobData4);
			workBuffer.Dispose();
			NativeArray<OverlapCapsuleCommand> nativeArray = new NativeArray<OverlapCapsuleCommand>(nativeList.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			GatherJob<OverlapCapsuleCommand> jobData5 = new GatherJob<OverlapCapsuleCommand>
			{
				Results = nativeArray,
				Source = commands.AsReadOnly(),
				Indices = nativeList.AsReadOnly()
			};
			IJobExtensions.RunByRef(ref jobData5);
			ExecuteOverlapCapsuleCommands(nativeArray, hits2, maxResPerCast);
			ScatterColliderHitsJob jobData6 = new ScatterColliderHitsJob
			{
				To = hits,
				From = hits2.AsReadOnly(),
				Indices = nativeList.AsReadOnly(),
				MaxHitsPerRay = maxResPerCast
			};
			IJobExtensions.RunByRef(ref jobData6);
			nativeArray.Dispose();
		}
		hits2.Dispose();
		nativeList.Dispose();
	}

	private static void ExecuteOverlapCapsuleCommands(NativeArray<OverlapCapsuleCommand> commands, NativeArray<ColliderHit> hits, int maxResPerCast)
	{
		if (Debug.isDebugBuild)
		{
			NativeList<int> invalidIndices = new NativeList<int>(commands.Length, Allocator.TempJob);
			ValidateOverlapCapsuleCommandsJob jobData = new ValidateOverlapCapsuleCommandsJob
			{
				InvalidIndices = invalidIndices,
				Commands = commands.AsReadOnly()
			};
			IJobExtensions.RunByRef(ref jobData);
			if (!invalidIndices.IsEmpty)
			{
				int num = invalidIndices[0];
				OverlapCapsuleCommand overlapCapsuleCommand = commands[num];
				Debug.LogError(string.Concat(string.Concat(string.Concat(string.Concat($"OverlapCapsules has {invalidIndices.Length} invalid sphere commands!" + $"\nFirst one was at index {num}:", $"\n\tPoint0: {overlapCapsuleCommand.point0}"), $"\n\tPoint1: {overlapCapsuleCommand.point1}"), $"\n\tRadius: {overlapCapsuleCommand.radius}"), "\nThese queries will be skipped!"));
			}
			invalidIndices.Dispose();
		}
		int batchSize = GetBatchSize(commands.Length);
		OverlapCapsuleCommand.ScheduleBatch(commands, hits, batchSize, maxResPerCast).Complete();
	}

	public static void OverlapOBB(OBB obb, List<Collider> list, int layerMask = -5, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Ignore)
	{
		layerMask = HandleIgnoreCollision(obb.position, layerMask);
		int count = UnityEngine.Physics.OverlapBoxNonAlloc(obb.position, obb.extents, colBuffer, obb.rotation, layerMask, triggerInteraction);
		BufferToList(colBuffer, count, list);
	}

	public static void OverlapBounds(Bounds bounds, List<Collider> list, int layerMask = -5, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Ignore)
	{
		layerMask = HandleIgnoreCollision(bounds.center, layerMask);
		int count = UnityEngine.Physics.OverlapBoxNonAlloc(bounds.center, bounds.extents, colBuffer, Quaternion.identity, layerMask, triggerInteraction);
		BufferToList(colBuffer, count, list);
	}

	private static void BufferToList(Collider[] buffer, int count, List<Collider> list)
	{
		for (int i = 0; i < count; i++)
		{
			list.Add(buffer[i]);
			buffer[i] = null;
		}
	}

	public static bool CheckSphere<T>(Vector3 pos, float radius, int layerMask = -5, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Ignore) where T : Component
	{
		List<Collider> obj = Facepunch.Pool.Get<List<Collider>>();
		OverlapSphere(pos, radius, obj, layerMask, triggerInteraction);
		bool result = CheckComponent<T>(obj);
		Facepunch.Pool.FreeUnmanaged(ref obj);
		return result;
	}

	public static void CheckSpheres<T>(NativeArray<Vector3>.ReadOnly positions, NativeArray<float>.ReadOnly radii, NativeArray<int>.ReadOnly layerMasks, Span<bool> results, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Ignore, int maxResPerCast = 16, MasksToValidate validate = MasksToValidate.All) where T : Component
	{
		using (TimeWarning.New("GamePhysics.CheckSpheres<T>"))
		{
			NativeArray<ColliderHit> hits = new NativeArray<ColliderHit>(positions.Length * maxResPerCast, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			OverlapSpheres(positions, radii, layerMasks, hits, maxResPerCast, triggerInteraction, validate);
			using (TimeWarning.New("FindComponent"))
			{
				for (int i = 0; i < positions.Length; i++)
				{
					bool flag = false;
					int num = i * maxResPerCast;
					for (int j = 0; j < maxResPerCast; j++)
					{
						ColliderHit colliderHit = hits[num + j];
						if (colliderHit.instanceID == 0)
						{
							break;
						}
						if (colliderHit.collider.TryGetComponent<T>(out var _))
						{
							flag = true;
							break;
						}
					}
					results[i] = flag;
				}
				hits.Dispose();
			}
		}
	}

	public static bool CheckCapsule<T>(Vector3 start, Vector3 end, float radius, int layerMask = -5, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Ignore) where T : Component
	{
		List<Collider> obj = Facepunch.Pool.Get<List<Collider>>();
		OverlapCapsule(start, end, radius, obj, layerMask, triggerInteraction);
		bool result = CheckComponent<T>(obj);
		Facepunch.Pool.FreeUnmanaged(ref obj);
		return result;
	}

	public static bool CheckOBB<T>(OBB obb, int layerMask = -5, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Ignore) where T : Component
	{
		List<Collider> obj = Facepunch.Pool.Get<List<Collider>>();
		OverlapOBB(obb, obj, layerMask, triggerInteraction);
		bool result = CheckComponent<T>(obj);
		Facepunch.Pool.FreeUnmanaged(ref obj);
		return result;
	}

	public static bool CheckBounds<T>(Bounds bounds, int layerMask = -5, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Ignore) where T : Component
	{
		List<Collider> obj = Facepunch.Pool.Get<List<Collider>>();
		OverlapBounds(bounds, obj, layerMask, triggerInteraction);
		bool result = CheckComponent<T>(obj);
		Facepunch.Pool.FreeUnmanaged(ref obj);
		return result;
	}

	private static bool CheckComponent<T>(List<Collider> list)
	{
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].gameObject.TryGetComponent<T>(out var _))
			{
				return true;
			}
		}
		return false;
	}

	public static void OverlapSphere<T>(Vector3 position, float radius, List<T> list, int layerMask = -5, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Ignore) where T : Component
	{
		layerMask = HandleIgnoreCollision(position, layerMask);
		int count = UnityEngine.Physics.OverlapSphereNonAlloc(position, radius, colBuffer, layerMask, triggerInteraction);
		BufferToList(colBuffer, count, list);
	}

	public static void CheckCapsules<T>(NativeArray<Vector3>.ReadOnly starts, NativeArray<Vector3>.ReadOnly ends, NativeArray<float>.ReadOnly radii, NativeArray<int>.ReadOnly layerMasks, Span<bool> results, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Ignore, int maxResPerCast = 16, MasksToValidate validate = MasksToValidate.All, bool mitigateSpheres = true) where T : Component
	{
		using (TimeWarning.New("GamePhysics.CheckCapsules<T>"))
		{
			NativeArray<ColliderHit> hits = new NativeArray<ColliderHit>(starts.Length * maxResPerCast, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			OverlapCapsules(starts, ends, radii, layerMasks, hits, maxResPerCast, triggerInteraction, validate, mitigateSpheres);
			using (TimeWarning.New("FindComponent"))
			{
				for (int i = 0; i < starts.Length; i++)
				{
					bool flag = false;
					int num = i * maxResPerCast;
					for (int j = 0; j < maxResPerCast; j++)
					{
						ColliderHit colliderHit = hits[num + j];
						if (colliderHit.instanceID == 0)
						{
							break;
						}
						if (colliderHit.collider.TryGetComponent<T>(out var _))
						{
							flag = true;
							break;
						}
					}
					results[i] = flag;
				}
				hits.Dispose();
			}
		}
	}

	public static void OverlapCapsule<T>(Vector3 point0, Vector3 point1, float radius, List<T> list, int layerMask = -5, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Ignore) where T : Component
	{
		layerMask = HandleIgnoreCollision(point0, layerMask);
		layerMask = HandleIgnoreCollision(point1, layerMask);
		int count = UnityEngine.Physics.OverlapCapsuleNonAlloc(point0, point1, radius, colBuffer, layerMask, triggerInteraction);
		BufferToList(colBuffer, count, list);
	}

	public static void OverlapOBB<T>(OBB obb, List<T> list, int layerMask = -5, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Ignore) where T : Component
	{
		layerMask = HandleIgnoreCollision(obb.position, layerMask);
		int count = UnityEngine.Physics.OverlapBoxNonAlloc(obb.position, obb.extents, colBuffer, obb.rotation, layerMask, triggerInteraction);
		BufferToList(colBuffer, count, list);
	}

	public static void OverlapBounds<T>(Bounds bounds, List<T> list, int layerMask = -5, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Ignore) where T : Component
	{
		layerMask = HandleIgnoreCollision(bounds.center, layerMask);
		int count = UnityEngine.Physics.OverlapBoxNonAlloc(bounds.center, bounds.extents, colBuffer, Quaternion.identity, layerMask, triggerInteraction);
		BufferToList(colBuffer, count, list);
	}

	private static void BufferToList<T>(Collider[] buffer, int count, List<T> list) where T : Component
	{
		for (int i = 0; i < count; i++)
		{
			if (buffer[i].TryGetComponent<T>(out var component))
			{
				list.Add(component);
			}
			buffer[i] = null;
		}
	}

	private static void HitBufferToList(int count, List<RaycastHit> list)
	{
		if (count >= hitBuffer.Length)
		{
			Debug.LogWarning("Physics query is exceeding collider buffer length.");
		}
		for (int i = 0; i < count; i++)
		{
			list.Add(hitBuffer[i]);
		}
	}

	public static bool TraceRealm(Realm realm, Ray ray, float radius, out RaycastHit hitInfo, float maxDistance = float.PositiveInfinity, int layerMask = -5, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.UseGlobal, BaseEntity ignoreEntity = null)
	{
		if (Trace(ray, radius, out var hitInfo2, maxDistance, layerMask, triggerInteraction, ignoreEntity))
		{
			hitInfo = hitInfo2;
			return true;
		}
		hitInfo = default(RaycastHit);
		return false;
	}

	public static BaseNetworkable TraceRealmEntity(Realm realm, Ray ray, float radius = 0f, float maxDistance = float.PositiveInfinity, int layerMask = -5, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.UseGlobal, BaseEntity ignoreEntity = null)
	{
		if (!Trace(ray, radius, out var hitInfo, maxDistance, layerMask, triggerInteraction, ignoreEntity))
		{
			return null;
		}
		return RaycastHitEx.GetEntity(hitInfo);
	}

	public static bool Trace(Ray ray, float radius, out RaycastHit hitInfo, float maxDistance = float.PositiveInfinity, int layerMask = -5, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.UseGlobal, BaseEntity ignoreEntity = null)
	{
		List<RaycastHit> obj = Facepunch.Pool.Get<List<RaycastHit>>();
		TraceAllUnordered(ray, radius, obj, maxDistance, layerMask, triggerInteraction, ignoreEntity);
		if (obj.Count == 0)
		{
			hitInfo = default(RaycastHit);
			Facepunch.Pool.FreeUnmanaged(ref obj);
			return false;
		}
		Sort(obj);
		hitInfo = obj[0];
		Facepunch.Pool.FreeUnmanaged(ref obj);
		return true;
	}

	public static void TraceAll(Ray ray, float radius, List<RaycastHit> hits, float maxDistance = float.PositiveInfinity, int layerMask = -5, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.UseGlobal, BaseEntity ignoreEntity = null)
	{
		TraceAllUnordered(ray, radius, hits, maxDistance, layerMask, triggerInteraction, ignoreEntity);
		Sort(hits);
	}

	public static void TraceAllUnordered(Ray ray, float radius, List<RaycastHit> hits, float maxDistance = float.PositiveInfinity, int layerMask = -5, QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.UseGlobal, BaseEntity ignoreEntity = null)
	{
		int num = ((radius != 0f) ? UnityEngine.Physics.SphereCastNonAlloc(ray, radius, hitBuffer, maxDistance, layerMask, triggerInteraction) : UnityEngine.Physics.RaycastNonAlloc(ray, hitBuffer, maxDistance, layerMask, triggerInteraction));
		if (num < hitBuffer.Length && (layerMask & 0x10) != 0 && WaterSystem.Trace(ray, out var position, out var normal, maxDistance))
		{
			RaycastHit raycastHit = new RaycastHit
			{
				point = position,
				normal = normal,
				distance = (position - ray.origin).magnitude
			};
			hitBuffer[num++] = raycastHit;
		}
		if (num == 0)
		{
			return;
		}
		if (num >= hitBuffer.Length)
		{
			Debug.LogWarning("Physics query is exceeding hit buffer length.");
		}
		for (int i = 0; i < num; i++)
		{
			RaycastHit raycastHit2 = hitBuffer[i];
			if (Verify(raycastHit2, ray.origin, ignoreEntity))
			{
				hits.Add(raycastHit2);
			}
		}
	}

	public static void TraceRaysUnordered(NativeArray<RaycastCommand> rays, NativeArray<RaycastHit> hits, int maxHitsPerTrace, bool traceWater = true)
	{
		using (TimeWarning.New("GamePhysics.TraceRaysUnordered"))
		{
			int batchSize = GetBatchSize(rays.Length);
			JobHandle inputDeps = RaycastCommand.ScheduleBatch(rays, hits, batchSize, maxHitsPerTrace);
			if (traceWater)
			{
				inputDeps = TraceWaterRaysDeferred(hits, rays, maxHitsPerTrace, inputDeps);
			}
			inputDeps.Complete();
			VerifyRays(hits, rays, maxHitsPerTrace);
		}
	}

	public static JobHandle TraceWaterRaysDeferred(NativeArray<RaycastHit> hits, NativeArray<RaycastCommand> rays, int maxHitsPerTrace, JobHandle inputDeps)
	{
		using (TimeWarning.New("ScheduleTraceWaterRaysDeferred"))
		{
			if (!rays.IsCreated || rays.Length == 0)
			{
				return inputDeps;
			}
			NativeList<Vector2i> waterIndices = new NativeList<Vector2i>(rays.Length, Allocator.TempJob);
			NativeList<Ray> nativeList = new NativeList<Ray>(rays.Length, Allocator.TempJob);
			NativeArray<bool> nativeArray = new NativeArray<bool>(rays.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			NativeArray<float> nativeArray2 = new NativeArray<float>(rays.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			NativeArray<Vector3> nativeArray3 = new NativeArray<Vector3>(rays.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			NativeArray<Vector3> nativeArray4 = new NativeArray<Vector3>(rays.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			GamePhysicsJobs.PreProcessWaterRaysJob jobData = new GamePhysicsJobs.PreProcessWaterRaysJob
			{
				hits = hits,
				rays = rays,
				maxHitsPerTrace = maxHitsPerTrace,
				WaterIndices = waterIndices,
				WaterRays = nativeList,
				WaterMaxDists = nativeArray2
			};
			inputDeps = IJobExtensions.ScheduleByRef(ref jobData, inputDeps);
			inputDeps = WaterSystem.ScheduleTraceBatchDefer(nativeList, nativeArray2, nativeArray, nativeArray3, nativeArray4, inputDeps);
			GamePhysicsJobs.PostProcessWaterRaysJob jobData2 = new GamePhysicsJobs.PostProcessWaterRaysJob
			{
				hits = hits,
				rays = nativeList.AsDeferredJobArray(),
				WaterIndices = waterIndices,
				hitsSub = nativeArray,
				positionsSub = nativeArray3,
				normalsSub = nativeArray4
			};
			inputDeps = IJobExtensions.ScheduleByRef(ref jobData2, inputDeps);
			waterIndices.Dispose(inputDeps);
			nativeList.Dispose(inputDeps);
			nativeArray.Dispose(inputDeps);
			nativeArray2.Dispose(inputDeps);
			nativeArray3.Dispose(inputDeps);
			nativeArray4.Dispose(inputDeps);
			return inputDeps;
		}
	}

	public static void VerifyRays(NativeArray<RaycastHit> hits, NativeArray<RaycastCommand> rays, int maxHitsPerCast)
	{
		using (TimeWarning.New("VerifyRays"))
		{
			for (int i = 0; i < rays.Length; i++)
			{
				RaycastCommand raycastCommand = rays[i];
				int num = i * maxHitsPerCast;
				int num2 = 0;
				for (int j = 0; j < maxHitsPerCast; j++)
				{
					int index = i * maxHitsPerCast + j;
					RaycastHit raycastHit = hits[index];
					if (hits[index].normal == Vector3.zero)
					{
						break;
					}
					if (Verify(raycastHit, raycastCommand.from))
					{
						hits[num + num2++] = raycastHit;
					}
				}
				if (num2 < maxHitsPerCast)
				{
					hits[num + num2] = default(RaycastHit);
				}
			}
		}
	}

	public static void TraceRays(NativeArray<RaycastCommand> rays, NativeArray<RaycastHit> hits, int maxHitsPerTrace, bool traceWater = true)
	{
		TraceRaysUnordered(rays, hits, maxHitsPerTrace, traceWater);
		Sort(hits, rays.Length, maxHitsPerTrace);
	}

	public static void TraceSpheresUnordered(NativeArray<SpherecastCommand> spheres, NativeArray<RaycastHit> hits, int maxHitsPerTrace, bool traceWater = true)
	{
		using (TimeWarning.New("GamePhysics.TraceSpheresUnordered"))
		{
			int batchSize = GetBatchSize(spheres.Length);
			JobHandle inputDeps = SpherecastCommand.ScheduleBatch(spheres, hits, batchSize, maxHitsPerTrace);
			if (traceWater)
			{
				inputDeps = TraceWaterSpheresDeferred(hits, spheres, maxHitsPerTrace, inputDeps);
			}
			inputDeps.Complete();
			VerifySpheres(hits, spheres, maxHitsPerTrace);
		}
	}

	public static JobHandle TraceWaterSpheresDeferred(NativeArray<RaycastHit> hits, NativeArray<SpherecastCommand> spheres, int maxHitsPerTrace, JobHandle inputDeps)
	{
		using (TimeWarning.New("ScheduleTraceWaterSpheresDeferred"))
		{
			if (!spheres.IsCreated || spheres.Length == 0)
			{
				return inputDeps;
			}
			NativeList<Vector2i> waterIndices = new NativeList<Vector2i>(spheres.Length, Allocator.TempJob);
			NativeList<Ray> nativeList = new NativeList<Ray>(spheres.Length, Allocator.TempJob);
			NativeArray<bool> nativeArray = new NativeArray<bool>(spheres.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			NativeArray<float> nativeArray2 = new NativeArray<float>(spheres.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			NativeArray<Vector3> nativeArray3 = new NativeArray<Vector3>(spheres.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			NativeArray<Vector3> nativeArray4 = new NativeArray<Vector3>(spheres.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			GamePhysicsJobs.PreProcessWaterSpheresJob jobData = new GamePhysicsJobs.PreProcessWaterSpheresJob
			{
				hits = hits,
				rays = spheres,
				maxHitsPerTrace = maxHitsPerTrace,
				WaterIndices = waterIndices,
				WaterRays = nativeList,
				WaterMaxDists = nativeArray2
			};
			inputDeps = IJobExtensions.ScheduleByRef(ref jobData, inputDeps);
			inputDeps = WaterSystem.ScheduleTraceBatchDefer(nativeList, nativeArray2, nativeArray, nativeArray3, nativeArray4, inputDeps);
			GamePhysicsJobs.PostProcessWaterRaysJob jobData2 = new GamePhysicsJobs.PostProcessWaterRaysJob
			{
				hits = hits,
				rays = nativeList.AsDeferredJobArray(),
				WaterIndices = waterIndices,
				hitsSub = nativeArray,
				positionsSub = nativeArray3,
				normalsSub = nativeArray4
			};
			inputDeps = IJobExtensions.ScheduleByRef(ref jobData2, inputDeps);
			waterIndices.Dispose(inputDeps);
			nativeList.Dispose(inputDeps);
			nativeArray.Dispose(inputDeps);
			nativeArray2.Dispose(inputDeps);
			nativeArray3.Dispose(inputDeps);
			nativeArray4.Dispose(inputDeps);
			return inputDeps;
		}
	}

	public static void VerifySpheres(NativeArray<RaycastHit> hits, NativeArray<SpherecastCommand> spheres, int maxHitsPerCast)
	{
		using (TimeWarning.New("VerifySpheres"))
		{
			for (int i = 0; i < spheres.Length; i++)
			{
				SpherecastCommand spherecastCommand = spheres[i];
				int num = i * maxHitsPerCast;
				int num2 = 0;
				for (int j = 0; j < maxHitsPerCast; j++)
				{
					int index = i * maxHitsPerCast + j;
					RaycastHit raycastHit = hits[index];
					if (hits[index].normal == Vector3.zero)
					{
						break;
					}
					if (Verify(raycastHit, spherecastCommand.origin))
					{
						hits[num + num2++] = raycastHit;
					}
				}
				if (num2 < maxHitsPerCast)
				{
					hits[num + num2] = default(RaycastHit);
				}
			}
		}
	}

	public static void TraceSpheres(NativeArray<SpherecastCommand> spheres, NativeArray<RaycastHit> hits, int maxHitsPerTrace, bool traceWater = true)
	{
		TraceSpheresUnordered(spheres, hits, maxHitsPerTrace, traceWater);
		Sort(hits, spheres.Length, maxHitsPerTrace);
	}

	public static bool LineOfSightRadius(Vector3 p0, Vector3 p1, int layerMask, float radius, float padding0, float padding1, BaseEntity ignoreEntity = null)
	{
		return LineOfSightInternal(p0, p1, layerMask, radius, padding0, padding1, ignoreEntity);
	}

	public static bool LineOfSightRadius(Vector3 p0, Vector3 p1, int layerMask, float radius, float padding, BaseEntity ignoreEntity = null)
	{
		return LineOfSightInternal(p0, p1, layerMask, radius, padding, padding, ignoreEntity);
	}

	public static bool LineOfSightRadius(Vector3 p0, Vector3 p1, int layerMask, float radius, BaseEntity ignoreEntity = null)
	{
		return LineOfSightInternal(p0, p1, layerMask, radius, 0f, 0f, ignoreEntity);
	}

	public static bool LineOfSight(Vector3 p0, Vector3 p1, int layerMask, float padding0, float padding1, BaseEntity ignoreEntity = null)
	{
		return LineOfSightRadius(p0, p1, layerMask, 0f, padding0, padding1, ignoreEntity);
	}

	public static bool LineOfSight(Vector3 p0, Vector3 p1, int layerMask, float padding, BaseEntity ignoreEntity = null)
	{
		return LineOfSightRadius(p0, p1, layerMask, 0f, padding, padding, ignoreEntity);
	}

	public static bool LineOfSight(Vector3 p0, Vector3 p1, int layerMask, BaseEntity ignoreEntity = null)
	{
		return LineOfSightRadius(p0, p1, layerMask, 0f, 0f, 0f, ignoreEntity);
	}

	private static bool LineOfSightInternal(Vector3 p0, Vector3 p1, int layerMask, float radius, float padding0, float padding1, BaseEntity ignoreEntity = null)
	{
		if (!ValidBounds.TestOuterBounds(p0))
		{
			return false;
		}
		if (!ValidBounds.TestOuterBounds(p1))
		{
			return false;
		}
		Vector3 vector = p1 - p0;
		float magnitude = vector.magnitude;
		if (magnitude <= padding0 + padding1)
		{
			return true;
		}
		Vector3 vector2 = vector / magnitude;
		Ray ray = new Ray(p0 + vector2 * padding0, vector2);
		float maxDistance = magnitude - padding0 - padding1;
		bool flag;
		RaycastHit hitInfo;
		if (!ignoreEntity.IsRealNull() || (layerMask & 0x800000) != 0)
		{
			flag = Trace(ray, 0f, out hitInfo, maxDistance, layerMask, QueryTriggerInteraction.Ignore, ignoreEntity);
			if (radius > 0f && !flag)
			{
				flag = Trace(ray, radius, out hitInfo, maxDistance, layerMask, QueryTriggerInteraction.Ignore, ignoreEntity);
			}
		}
		else
		{
			flag = UnityEngine.Physics.Raycast(ray, out hitInfo, maxDistance, layerMask, QueryTriggerInteraction.Ignore);
			if (radius > 0f && !flag)
			{
				flag = UnityEngine.Physics.SphereCast(ray, radius, out hitInfo, maxDistance, layerMask, QueryTriggerInteraction.Ignore);
			}
		}
		if (!flag)
		{
			if (ConVar.Vis.lineofsight)
			{
				ConsoleNetwork.BroadcastToAllClients("ddraw.line", 60f, Color.green, p0, p1);
			}
			return true;
		}
		if (ConVar.Vis.lineofsight)
		{
			ConsoleNetwork.BroadcastToAllClients("ddraw.line", 60f, Color.red, p0, p1);
			ConsoleNetwork.BroadcastToAllClients("ddraw.text", 60f, Color.white, hitInfo.point, hitInfo.collider.name);
		}
		return false;
	}

	public static bool Verify(RaycastHit hitInfo, Vector3 rayOrigin, BaseEntity ignoreEntity = null)
	{
		Vector3 vector = hitInfo.point;
		if (hitInfo.collider is TerrainCollider && vector == Vector3.zero && hitInfo.distance == 0f)
		{
			vector = rayOrigin;
		}
		return Verify(hitInfo.collider, vector, ignoreEntity);
	}

	public static bool Verify(Collider collider, Vector3 point, BaseEntity ignoreEntity = null)
	{
		if (collider == null)
		{
			if ((bool)WaterSystem.Collision && WaterSystem.Collision.GetIgnore(point))
			{
				return false;
			}
			return true;
		}
		if (collider is TerrainCollider)
		{
			if ((bool)TerrainMeta.Collision && TerrainMeta.Collision.GetIgnore(point))
			{
				return false;
			}
			return true;
		}
		if (!ignoreEntity.IsRealNull() && CompareEntity(GameObjectEx.ToBaseEntity(collider), ignoreEntity))
		{
			return false;
		}
		return collider.enabled;
	}

	public static bool CompareEntity(BaseEntity a, BaseEntity b)
	{
		if (a.IsRealNull() || b.IsRealNull())
		{
			return false;
		}
		if (a == b)
		{
			return true;
		}
		return false;
	}

	public static int HandleIgnoreCollision(Vector3 position, int layerMask)
	{
		int num = 8388608;
		if ((layerMask & num) != 0 && (bool)TerrainMeta.Collision && TerrainMeta.Collision.GetIgnore(position))
		{
			layerMask &= ~num;
		}
		int num2 = 16;
		if ((layerMask & num2) != 0 && (bool)WaterSystem.Collision && WaterSystem.Collision.GetIgnore(position))
		{
			layerMask &= ~num2;
		}
		return layerMask;
	}

	public static void HandleIgnoreTerrain(NativeArray<Vector3>.ReadOnly positions, NativeArray<bool> hitIgnoreVolumes)
	{
		NativeArray<float> values = new NativeArray<float>(positions.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
		FillJob<float> jobData = new FillJob<float>
		{
			Values = values,
			Value = 0.01f
		};
		IJobExtensions.RunByRef(ref jobData);
		TerrainMeta.Collision.GetIgnore(positions, values.AsReadOnly(), hitIgnoreVolumes);
		values.Dispose();
	}

	public static void HandleIgnoreWater(NativeArray<Vector3>.ReadOnly positions, NativeArray<bool> hitIgnoreVolumes)
	{
		NativeArray<float> values = new NativeArray<float>(positions.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
		FillJob<float> jobData = new FillJob<float>
		{
			Values = values,
			Value = 0.01f
		};
		IJobExtensions.RunByRef(ref jobData);
		WaterSystem.Collision.GetIgnore(positions, values.AsReadOnly(), hitIgnoreVolumes);
		values.Dispose();
	}

	public static void HandleIgnoreCollision(NativeArray<Vector3>.ReadOnly positions, NativeArray<int> layerMasks, MasksToValidate validate = MasksToValidate.All)
	{
		if (validate.HasFlag(MasksToValidate.Terrain))
		{
			NativeArray<bool> hitIgnoreVolumes = new NativeArray<bool>(positions.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			HandleIgnoreTerrain(positions, hitIgnoreVolumes);
			RemoveLayerMaskJob jobData = new RemoveLayerMaskJob
			{
				LayerMasks = layerMasks,
				ShouldIgnore = hitIgnoreVolumes.AsReadOnly(),
				MaskToRemove = 8388608
			};
			IJobExtensions.RunByRef(ref jobData);
			hitIgnoreVolumes.Dispose();
		}
		if (validate.HasFlag(MasksToValidate.Water))
		{
			NativeArray<bool> hitIgnoreVolumes2 = new NativeArray<bool>(positions.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			HandleIgnoreWater(positions, hitIgnoreVolumes2);
			RemoveLayerMaskJob jobData2 = new RemoveLayerMaskJob
			{
				LayerMasks = layerMasks,
				ShouldIgnore = hitIgnoreVolumes2.AsReadOnly(),
				MaskToRemove = 16
			};
			IJobExtensions.RunByRef(ref jobData2);
			hitIgnoreVolumes2.Dispose();
		}
	}

	public static void Sort(List<RaycastHit> hits)
	{
		hits.Sort((RaycastHit a, RaycastHit b) => a.distance.CompareTo(b.distance));
	}

	public static void Sort(RaycastHit[] hits)
	{
		Array.Sort(hits, (RaycastHit a, RaycastHit b) => a.distance.CompareTo(b.distance));
	}

	public static void Sort(NativeArray<RaycastHit> hits, int queryCount, int maxHitsPerQuery)
	{
		using (TimeWarning.New("GamePhysics.Sort"))
		{
			NativeArray<int> counts = new NativeArray<int>(queryCount, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			CountRaycastHitsJobs jobData = new CountRaycastHitsJobs
			{
				Counts = counts,
				Hits = hits.AsReadOnly(),
				MaxHitsPerRay = maxHitsPerQuery
			};
			IJobExtensions.RunByRef(ref jobData);
			for (int i = 0; i < counts.Length; i++)
			{
				int num = counts[i];
				if (num > 1)
				{
					hits.GetSubArray(i * maxHitsPerQuery, num).SortJob(default(RaycastHitComparer)).Schedule()
						.Complete();
				}
			}
			counts.Dispose();
		}
	}

	public static int GetBatchSize(int count, int subdivideFactor = 4, int minBatchSize = 64)
	{
		return Math.Max(count / JobsUtility.JobWorkerCount / subdivideFactor, minBatchSize);
	}
}
