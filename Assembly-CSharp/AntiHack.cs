using System;
using System.Collections.Generic;
using AntiHackJobs;
using ConVar;
using Epic.OnlineServices.Reports;
using Facepunch;
using Facepunch.Rust;
using Oxide.Core;
using Rust;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UtilityJobs;

public static class AntiHack
{
	public struct Batch
	{
		public int PlayerIndex;

		public int Count;

		public bool Force;

		public bool ExcludeVehicleLayer;
	}

	private class GroupedLog : Facepunch.Pool.IPooled
	{
		public float firstLogTime;

		public string playerName;

		public AntiHackType antiHackType;

		public string message;

		public Vector3 averagePos;

		public int num;

		public GroupedLog()
		{
		}

		public GroupedLog(string playerName, AntiHackType antiHackType, string message, Vector3 pos)
		{
			SetInitial(playerName, antiHackType, message, pos);
		}

		public void EnterPool()
		{
			firstLogTime = 0f;
			playerName = string.Empty;
			antiHackType = AntiHackType.None;
			averagePos = Vector3.zero;
			num = 0;
		}

		public void LeavePool()
		{
		}

		public void SetInitial(string playerName, AntiHackType antiHackType, string message, Vector3 pos)
		{
			firstLogTime = UnityEngine.Time.unscaledTime;
			this.playerName = playerName;
			this.antiHackType = antiHackType;
			this.message = message;
			averagePos = pos;
			num = 1;
		}

		public bool TryGroup(string playerName, AntiHackType antiHackType, string message, Vector3 pos, float maxDistance)
		{
			if (antiHackType != this.antiHackType || playerName != this.playerName || message != this.message)
			{
				return false;
			}
			if (Vector3.SqrMagnitude(averagePos - pos) > maxDistance * maxDistance)
			{
				return false;
			}
			Vector3 vector = averagePos * num;
			averagePos = (vector + pos) / (num + 1);
			num++;
			return true;
		}
	}

	private const int movement_mask = 1503731969;

	private const int vehicle_mask = 8192;

	private const int grounded_mask = 1503764737;

	private const int player_mask = 131072;

	private static Collider[] buffer = new Collider[4];

	private static Dictionary<ulong, int> kicks = new Dictionary<ulong, int>();

	private static Dictionary<ulong, int> bans = new Dictionary<ulong, int>();

	private const float LOG_GROUP_SECONDS = 60f;

	private static Queue<GroupedLog> groupedLogs = new Queue<GroupedLog>();

	private static NativeArray<float> DeltaTimes;

	private static NativeArray<bool> FindIndexWorkBuffer;

	private static NativeList<int> ValidIndexAccum1;

	private static NativeList<int> ValidIndexAccum2;

	private static NativeList<int> InvalidIndices;

	private static NativeList<Vector3> From;

	private static NativeList<Vector3> To;

	private static NativeList<Batch> Batches;

	private static NativeList<int> LayerMasks;

	private static NativeArray<float> PlayerRadii;

	private static NativeList<int> ToOverlapIndices;

	private static NativeList<Matrix4x4> Matrices;

	private static NativeList<Vector3> ToOverlapFrom;

	private static NativeList<Vector3> ToOverlapTo;

	private static NativeList<int> ToOverlapLayerMasks;

	private static NativeList<int> RaycastIndices;

	private static NativeList<RaycastCommand> RaycastRays;

	private static NativeList<SpherecastCommand> RaycastSpheres;

	private static NativeList<int> TraceIndices;

	private static NativeList<RaycastCommand> TraceRays;

	private static NativeList<SpherecastCommand> TraceSpheres;

	private static NativeArray<RaycastHit> RaycastHits;

	private static NativeArray<ColliderHit> ColliderHits;

	private static NativeArray<bool> TerrainIgnoreVolumeHits;

	private static NativeArray<int> QueryToBatchMap;

	private static BufferList<Collider> Colliders;

	public static RaycastHit isInsideRayHit;

	private static RaycastHit[] isInsideMeshRaycastHits = new RaycastHit[64];

	public static bool TestNoClipping(BasePlayer ply, Vector3 oldPos, Vector3 newPos, float radius, float backtracking, out Collider col, bool vehicleLayer = false, BaseEntity ignoreEntity = null, bool forceCast = false)
	{
		int num = 1503731969;
		if (!vehicleLayer)
		{
			num &= -8193;
		}
		Vector3 normalized = (newPos - oldPos).normalized;
		Vector3 vector = oldPos - normalized * backtracking;
		float magnitude = (newPos - vector).magnitude;
		Ray ray = new Ray(vector, normalized);
		if (GamePhysics.CheckCapsule(oldPos, newPos, radius, num, QueryTriggerInteraction.Ignore))
		{
			List<Collider> obj = Facepunch.Pool.Get<List<Collider>>();
			GamePhysics.OverlapCapsule(oldPos, newPos, radius, obj, num);
			bool recheck = false;
			bool recheckTerrain = false;
			for (int i = 0; i < obj.Count; i++)
			{
				Collider collider = obj[i];
				if (IsColliderBlocking(collider, ply, forceCast, ignoreEntity, ref recheck, ref recheckTerrain))
				{
					col = collider;
					Facepunch.Pool.FreeUnmanaged(ref obj);
					return true;
				}
			}
			Facepunch.Pool.FreeUnmanaged(ref obj);
			if (recheck || recheckTerrain)
			{
				if (!recheckTerrain && ignoreEntity == null)
				{
					RaycastHit hitInfo;
					bool result = UnityEngine.Physics.Raycast(ray, out hitInfo, magnitude + radius, num, QueryTriggerInteraction.Ignore) || UnityEngine.Physics.SphereCast(ray, radius, out hitInfo, magnitude, num, QueryTriggerInteraction.Ignore);
					col = hitInfo.collider;
					return result;
				}
				RaycastHit hitInfo2;
				bool result2 = GamePhysics.Trace(ray, 0f, out hitInfo2, magnitude + radius, num, QueryTriggerInteraction.Ignore, ignoreEntity) || GamePhysics.Trace(ray, radius, out hitInfo2, magnitude, num, QueryTriggerInteraction.Ignore, ignoreEntity);
				col = hitInfo2.collider;
				return result2;
			}
		}
		col = null;
		return false;
	}

	private static bool IsColliderBlocking(Collider collider, BasePlayer ply, bool forceCast, BaseEntity ignoreEntity, ref bool recheck, ref bool recheckTerrain)
	{
		if (collider is TerrainCollider)
		{
			recheckTerrain = true;
			return false;
		}
		if (((int)collider.excludeLayers & 0x1000) == 4096)
		{
			return false;
		}
		BaseEntity baseEntity = GameObjectEx.ToBaseEntity(collider);
		if (((1 << collider.gameObject.layer) & 0x2000) > 0)
		{
			if (baseEntity is HotAirBalloon && UnityEngine.Time.time - ply.unparentTime <= 5f)
			{
				return false;
			}
			recheck = true;
			return false;
		}
		if (GamePhysics.CompareEntity(baseEntity, ignoreEntity))
		{
			return false;
		}
		if (forceCast)
		{
			recheck = true;
			return false;
		}
		if (baseEntity != null && baseEntity.ShouldUseCastNoClipChecks())
		{
			recheck = true;
			return false;
		}
		if (ply.GetParentEntity() is ElevatorLift)
		{
			recheck = true;
			return false;
		}
		return true;
	}

	public static void TestAreNoClipping(ReadOnlySpan<BasePlayer> players, NativeArray<Vector3>.ReadOnly fromPos, NativeArray<Vector3>.ReadOnly toPos, NativeArray<Batch>.ReadOnly batches, NativeList<int> foundIndices, Span<Collider> foundColls)
	{
		using (TimeWarning.New("TestAreNoClipping"))
		{
			float noclip_backtracking = ConVar.AntiHack.noclip_backtracking;
			float num = BasePlayer.NoClipRadius(ConVar.AntiHack.noclip_margin);
			NativeListEx.Expand(ref LayerMasks, fromPos.Length, copyContents: false);
			BuildLayerMasksJob jobData = new BuildLayerMasksJob
			{
				LayerMasks = LayerMasks,
				Batches = batches,
				DefaultMask = 1503731969,
				NoVehiclesMask = 1503723777
			};
			IJobExtensions.RunByRef(ref jobData);
			NativeArrayEx.Expand(ref PlayerRadii, fromPos.Length, NativeArrayOptions.UninitializedMemory, copyContents: false);
			FillJob<float> jobData2 = new FillJob<float>
			{
				Values = PlayerRadii,
				Value = num
			};
			IJobExtensions.RunByRef(ref jobData2);
			NativeArrayEx.Expand(ref TerrainIgnoreVolumeHits, fromPos.Length, NativeArrayOptions.UninitializedMemory, copyContents: false);
			GamePhysics.CheckCapsules(fromPos, toPos, PlayerRadii.AsReadOnly(), LayerMasks.AsReadOnly(), TerrainIgnoreVolumeHits, QueryTriggerInteraction.Ignore, GamePhysics.MasksToValidate.Terrain);
			NativeListEx.Expand(ref ToOverlapIndices, fromPos.Length, copyContents: false);
			GatherHitIndicesJob jobData3 = new GatherHitIndicesJob
			{
				Results = ToOverlapIndices,
				Hits = TerrainIgnoreVolumeHits.GetSubArray(0, fromPos.Length).AsReadOnly()
			};
			IJobExtensions.RunByRef(ref jobData3);
			if (ToOverlapIndices.IsEmpty)
			{
				return;
			}
			NativeArrayEx.Expand(ref QueryToBatchMap, fromPos.Length, NativeArrayOptions.UninitializedMemory, copyContents: false);
			BuildBatchLookupMapJob jobData4 = new BuildBatchLookupMapJob
			{
				Lookup = QueryToBatchMap,
				Batches = batches
			};
			IJobExtensions.RunByRef(ref jobData4);
			ToOverlapFrom.Resize(ToOverlapIndices.Length, NativeArrayOptions.UninitializedMemory);
			GatherJob<Vector3> jobData5 = new GatherJob<Vector3>
			{
				Results = ToOverlapFrom.AsArray(),
				Source = fromPos,
				Indices = ToOverlapIndices.AsReadOnly()
			};
			IJobExtensions.RunByRef(ref jobData5);
			ToOverlapTo.Resize(ToOverlapIndices.Length, NativeArrayOptions.UninitializedMemory);
			GatherJob<Vector3> jobData6 = new GatherJob<Vector3>
			{
				Results = ToOverlapTo.AsArray(),
				Source = toPos,
				Indices = ToOverlapIndices.AsReadOnly()
			};
			IJobExtensions.RunByRef(ref jobData6);
			ToOverlapLayerMasks.Resize(ToOverlapIndices.Length, NativeArrayOptions.UninitializedMemory);
			GatherJob<int> jobData7 = new GatherJob<int>
			{
				Results = ToOverlapLayerMasks.AsArray(),
				Source = LayerMasks.AsReadOnly(),
				Indices = ToOverlapIndices.AsReadOnly()
			};
			IJobExtensions.RunByRef(ref jobData7);
			NativeArrayEx.Expand(ref ColliderHits, ToOverlapLayerMasks.Length * 16, NativeArrayOptions.UninitializedMemory, copyContents: false);
			GamePhysics.OverlapCapsules(ToOverlapFrom.AsReadOnly(), ToOverlapTo.AsReadOnly(), PlayerRadii.GetSubArray(0, ToOverlapLayerMasks.Length).AsReadOnly(), ToOverlapLayerMasks.AsReadOnly(), ColliderHits, 16, QueryTriggerInteraction.Ignore, GamePhysics.MasksToValidate.Terrain);
			NativeListEx.Expand(ref TraceIndices, ToOverlapIndices.Length, copyContents: false);
			NativeListEx.Expand(ref TraceRays, ToOverlapIndices.Length, copyContents: false);
			NativeListEx.Expand(ref RaycastIndices, ToOverlapIndices.Length, copyContents: false);
			NativeListEx.Expand(ref RaycastRays, ToOverlapIndices.Length, copyContents: false);
			using (TimeWarning.New("FilterOverlapResults"))
			{
				bool flag = false;
				for (int i = 0; i < ToOverlapIndices.Length; i++)
				{
					int num2 = ToOverlapIndices[i];
					int index = QueryToBatchMap[num2];
					Batch batch = batches[index];
					if (flag)
					{
						if ((object)foundColls[batch.PlayerIndex] != null)
						{
							continue;
						}
						flag = false;
					}
					BasePlayer ply = players[batch.PlayerIndex];
					bool force = batch.Force;
					bool recheck = false;
					bool recheckTerrain = false;
					Collider collider = null;
					for (int j = 0; j < 16; j++)
					{
						int index2 = i * 16 + j;
						ColliderHit colliderHit = ColliderHits[index2];
						if (colliderHit.instanceID == 0)
						{
							break;
						}
						Collider collider2 = colliderHit.collider;
						if (IsColliderBlocking(collider2, ply, force, null, ref recheck, ref recheckTerrain))
						{
							flag = true;
							collider = collider2;
							break;
						}
					}
					int value = batch.PlayerIndex;
					if (flag)
					{
						foundIndices.Add(in value);
						foundColls[value] = collider;
					}
					else if (recheck || recheckTerrain)
					{
						Vector3 vector = ToOverlapFrom[i];
						Vector3 vector2 = ToOverlapTo[i];
						Vector3 normalized = (vector2 - vector).normalized;
						Vector3 vector3 = vector - normalized * noclip_backtracking;
						float magnitude = (vector2 - vector3).magnitude;
						new Ray(vector3, normalized);
						int layerMask = ToOverlapLayerMasks[i];
						QueryParameters queryParameters = new QueryParameters(layerMask, hitMultipleFaces: false, QueryTriggerInteraction.Ignore);
						RaycastCommand value2 = new RaycastCommand(vector3, normalized, queryParameters, magnitude + num);
						if (recheckTerrain)
						{
							TraceIndices.AddNoResize(num2);
							TraceRays.AddNoResize(value2);
						}
						else
						{
							RaycastIndices.AddNoResize(num2);
							RaycastRays.AddNoResize(value2);
						}
					}
				}
			}
			if (!TraceIndices.IsEmpty)
			{
				NativeArrayEx.Expand(ref RaycastHits, TraceIndices.Length * 16, NativeArrayOptions.UninitializedMemory, copyContents: false);
				GamePhysics.TraceRays(TraceRays.AsArray(), RaycastHits, 16, traceWater: false);
				using (TimeWarning.New("GatherRays"))
				{
					int length = 0;
					NativeListEx.Expand(ref TraceSpheres, TraceIndices.Length, copyContents: false);
					for (int k = 0; k < TraceIndices.Length; k++)
					{
						if (!RecordNoclip(RaycastHits[k * 16], TraceIndices[k], batches, foundIndices, foundColls))
						{
							TraceIndices[length++] = TraceIndices[k];
							RaycastCommand raycastCommand = TraceRays[k];
							TraceSpheres.Add(new SpherecastCommand(raycastCommand.from, num, raycastCommand.direction, raycastCommand.queryParameters, raycastCommand.distance - num));
						}
					}
					TraceIndices.Resize(length, NativeArrayOptions.UninitializedMemory);
				}
				if (!TraceIndices.IsEmpty)
				{
					GamePhysics.TraceSpheres(TraceSpheres.AsArray(), RaycastHits, 16, traceWater: false);
					using (TimeWarning.New("GatherSpheres"))
					{
						for (int l = 0; l < TraceIndices.Length; l++)
						{
							RecordNoclip(RaycastHits[l * 16], TraceIndices[l], batches, foundIndices, foundColls);
						}
					}
				}
			}
			if (RaycastIndices.IsEmpty)
			{
				return;
			}
			if (!TraceIndices.IsEmpty)
			{
				using (TimeWarning.New("SkipDupeRaycasts"))
				{
					int length2 = 0;
					for (int m = 0; m < RaycastIndices.Length; m++)
					{
						int num3 = RaycastIndices[m];
						int index3 = QueryToBatchMap[num3];
						if ((object)foundColls[batches[index3].PlayerIndex] == null)
						{
							int index4 = length2++;
							RaycastIndices[index4] = num3;
							RaycastRays[index4] = RaycastRays[m];
						}
					}
					RaycastIndices.Resize(length2, NativeArrayOptions.UninitializedMemory);
					RaycastRays.Resize(length2, NativeArrayOptions.UninitializedMemory);
				}
			}
			if (!RaycastIndices.IsEmpty)
			{
				using (TimeWarning.New("RayCasts"))
				{
					NativeArrayEx.Expand(ref RaycastHits, RaycastIndices.Length, NativeArrayOptions.UninitializedMemory, copyContents: false);
					RaycastCommand.ScheduleBatch(RaycastRays.AsArray(), RaycastHits, 1).Complete();
					int length3 = 0;
					NativeListEx.Expand(ref RaycastSpheres, RaycastIndices.Length, copyContents: false);
					for (int n = 0; n < RaycastIndices.Length; n++)
					{
						if (!RecordNoclip(RaycastHits[n], RaycastIndices[n], batches, foundIndices, foundColls))
						{
							RaycastIndices[length3++] = RaycastIndices[n];
							RaycastCommand raycastCommand2 = RaycastRays[n];
							SpherecastCommand value3 = new SpherecastCommand(raycastCommand2.from, num, raycastCommand2.direction, raycastCommand2.queryParameters, raycastCommand2.distance - num);
							RaycastSpheres.AddNoResize(value3);
						}
					}
					RaycastIndices.Resize(length3, NativeArrayOptions.UninitializedMemory);
				}
			}
			if (RaycastIndices.IsEmpty)
			{
				return;
			}
			using (TimeWarning.New("SphereCasts"))
			{
				SpherecastCommand.ScheduleBatch(RaycastSpheres.AsArray(), RaycastHits, 1).Complete();
				for (int num4 = 0; num4 < RaycastIndices.Length; num4++)
				{
					RecordNoclip(RaycastHits[num4], RaycastIndices[num4], batches, foundIndices, foundColls);
				}
			}
		}
		static bool RecordNoclip(RaycastHit hit, int queryIndex, NativeArray<Batch>.ReadOnly readOnly, NativeList<int> nativeList, Span<Collider> span)
		{
			bool num5 = hit.colliderInstanceID != 0;
			if (num5)
			{
				int index5 = QueryToBatchMap[queryIndex];
				int value4 = readOnly[index5].PlayerIndex;
				if ((object)span[value4] == null)
				{
					nativeList.Add(in value4);
					span[value4] = hit.collider;
				}
			}
			return num5;
		}
	}

	public static void Cycle()
	{
		float num = UnityEngine.Time.unscaledTime - 60f;
		if (groupedLogs.Count <= 0)
		{
			return;
		}
		GroupedLog groupedLog = groupedLogs.Peek();
		while (groupedLog.firstLogTime <= num)
		{
			GroupedLog obj = groupedLogs.Dequeue();
			LogToConsole(obj.playerName, obj.antiHackType, $"{obj.message} (x{obj.num})", obj.averagePos);
			Facepunch.Pool.Free(ref obj);
			if (groupedLogs.Count != 0)
			{
				groupedLog = groupedLogs.Peek();
				continue;
			}
			break;
		}
	}

	public static void ResetTimer(BasePlayer ply)
	{
		ply.lastViolationTime = UnityEngine.Time.realtimeSinceStartup;
		ply.lastMovementViolationTime = UnityEngine.Time.realtimeSinceStartup;
	}

	public static bool ShouldIgnore(BasePlayer ply)
	{
		using (TimeWarning.New("AntiHack.ShouldIgnore"))
		{
			if (ply.IsFlying)
			{
				ply.lastAdminCheatTime = UnityEngine.Time.realtimeSinceStartup;
			}
			else if ((ply.IsAdmin || ply.IsDeveloper) && ply.lastAdminCheatTime == 0f)
			{
				ply.lastAdminCheatTime = UnityEngine.Time.realtimeSinceStartup;
			}
			if (ply.IsAdmin)
			{
				if (ConVar.AntiHack.userlevel < 1)
				{
					return true;
				}
				if (ConVar.AntiHack.admincheat && ply.UsedAdminCheat())
				{
					return true;
				}
			}
			if (ply.IsDeveloper)
			{
				if (ConVar.AntiHack.userlevel < 2)
				{
					return true;
				}
				if (ConVar.AntiHack.admincheat && ply.UsedAdminCheat())
				{
					return true;
				}
			}
			if (ply.IsSpectating())
			{
				return true;
			}
			if (ply.isInvisible)
			{
				return true;
			}
			return false;
		}
	}

	internal static bool ValidateMove(BasePlayer ply, TickInterpolator ticks, float deltaTime, in BasePlayer.CachedState initialState)
	{
		using (TimeWarning.New("AntiHack.ValidateMove"))
		{
			if (ShouldIgnore(ply))
			{
				return true;
			}
			bool flag = deltaTime > ConVar.AntiHack.maxdeltatime;
			bool flag2 = deltaTime < ConVar.AntiHack.tick_buffer_server_lag_threshold && ConVar.AntiHack.tick_buffer_preventions && (float)ply.rawTickCount >= ConVar.AntiHack.tick_buffer_reject_threshold * (float)Player.tickrate_cl;
			if (IsNoClipping(ply, ticks, deltaTime, out var collider))
			{
				if (flag)
				{
					return false;
				}
				Facepunch.Rust.Analytics.Azure.OnNoclipViolation(ply, ticks.CurrentPoint, ticks.EndPoint, ticks.Count, collider);
				AddViolation(ply, AntiHackType.NoClip, ConVar.AntiHack.noclip_penalty * ticks.Length, collider.gameObject);
				if (ConVar.AntiHack.noclip_reject)
				{
					return false;
				}
			}
			if (IsSpeeding(ply, ticks, deltaTime, in initialState))
			{
				if (flag)
				{
					return false;
				}
				Facepunch.Rust.Analytics.Azure.OnSpeedhackViolation(ply, ticks.CurrentPoint, ticks.EndPoint, ticks.Count);
				AddViolation(ply, AntiHackType.SpeedHack, ConVar.AntiHack.speedhack_penalty * ticks.Length);
				if (ConVar.AntiHack.speedhack_reject)
				{
					return false;
				}
			}
			if (IsFlying(ply, ticks, deltaTime, in initialState))
			{
				if (flag)
				{
					return false;
				}
				Facepunch.Rust.Analytics.Azure.OnFlyhackViolation(ply, ticks.CurrentPoint, ticks.EndPoint, ticks.Count);
				AddViolation(ply, AntiHackType.FlyHack, ConVar.AntiHack.flyhack_penalty * ticks.Length);
				if (ConVar.AntiHack.flyhack_reject && !(ply.lastGroundedPosition == default(Vector3)) && Vector3.Distance(ply.lastGroundedPosition, ply.transform.position) <= 10f)
				{
					Collider col;
					bool num = TestNoClipping(ply, ply.transform.position, ply.lastGroundedPosition, BasePlayer.NoClipRadius(ConVar.AntiHack.noclip_margin), ConVar.AntiHack.noclip_backtracking, out col);
					Vector3 start = ply.lastGroundedPosition + new Vector3(0f, BasePlayer.GetRadius(), 0f);
					Vector3 end = ply.lastGroundedPosition + new Vector3(0f, ply.GetHeight() - BasePlayer.GetRadius(), 0f);
					if (!num && !UnityEngine.Physics.CheckCapsule(start, end, BasePlayer.GetRadius(), 1537286401))
					{
						ply.MovePosition(ply.lastGroundedPosition);
						ply.ClientRPC(RpcTarget.Player("ForcePositionTo", ply), ply.transform.position);
						ply.violationLevel = 0f;
					}
				}
			}
			if (flag2)
			{
				Log(ply, AntiHackType.Ticks, $"Player had too many ticks buffered ({ply.rawTickCount})", logToAnalytics: false);
				Facepunch.Rust.Analytics.Azure.OnTickViolation(ply, ticks.CurrentPoint, ticks.EndPoint, ticks.Count);
				return false;
			}
			if (ConVar.AntiHack.serverside_fall_damage)
			{
				bool num2 = ply.transform.parent == null;
				Matrix4x4 matrix4x = (num2 ? Matrix4x4.identity : ply.transform.parent.localToWorldMatrix);
				Vector3 oldPos = (num2 ? ticks.StartPoint : matrix4x.MultiplyPoint3x4(ticks.StartPoint));
				Vector3 newPos = (num2 ? ticks.EndPoint : matrix4x.MultiplyPoint3x4(ticks.EndPoint));
				TestServerSideFallDamage(ply, oldPos, newPos, deltaTime);
			}
			return true;
		}
	}

	public static void InitInternalState(int initCap)
	{
		DisposeInternalState();
		DeltaTimes = new NativeArray<float>(initCap, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		FindIndexWorkBuffer = new NativeArray<bool>(initCap, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		ValidIndexAccum1 = new NativeList<int>(initCap, Allocator.Persistent);
		ValidIndexAccum2 = new NativeList<int>(initCap, Allocator.Persistent);
		InvalidIndices = new NativeList<int>(initCap, Allocator.Persistent);
		From = new NativeList<Vector3>(initCap, Allocator.Persistent);
		To = new NativeList<Vector3>(initCap, Allocator.Persistent);
		Batches = new NativeList<Batch>(initCap, Allocator.Persistent);
		LayerMasks = new NativeList<int>(initCap, Allocator.Persistent);
		PlayerRadii = new NativeArray<float>(initCap, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		ToOverlapIndices = new NativeList<int>(initCap, Allocator.Persistent);
		Matrices = new NativeList<Matrix4x4>(initCap, Allocator.Persistent);
		ToOverlapFrom = new NativeList<Vector3>(initCap, Allocator.Persistent);
		ToOverlapTo = new NativeList<Vector3>(initCap, Allocator.Persistent);
		ToOverlapLayerMasks = new NativeList<int>(initCap, Allocator.Persistent);
		RaycastIndices = new NativeList<int>(initCap, Allocator.Persistent);
		RaycastRays = new NativeList<RaycastCommand>(initCap, Allocator.Persistent);
		RaycastSpheres = new NativeList<SpherecastCommand>(initCap, Allocator.Persistent);
		TraceIndices = new NativeList<int>(initCap, Allocator.Persistent);
		TraceRays = new NativeList<RaycastCommand>(initCap, Allocator.Persistent);
		TraceSpheres = new NativeList<SpherecastCommand>(initCap, Allocator.Persistent);
		RaycastHits = new NativeArray<RaycastHit>(initCap, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		ColliderHits = new NativeArray<ColliderHit>(initCap, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		TerrainIgnoreVolumeHits = new NativeArray<bool>(initCap, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		QueryToBatchMap = new NativeArray<int>(initCap, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		Colliders = new BufferList<Collider>(initCap);
	}

	public static void DisposeInternalState()
	{
		NativeArrayEx.SafeDispose(ref DeltaTimes);
		NativeArrayEx.SafeDispose(ref FindIndexWorkBuffer);
		NativeListEx.SafeDispose(ref ValidIndexAccum1);
		NativeListEx.SafeDispose(ref ValidIndexAccum2);
		NativeListEx.SafeDispose(ref InvalidIndices);
		NativeListEx.SafeDispose(ref From);
		NativeListEx.SafeDispose(ref To);
		NativeListEx.SafeDispose(ref Batches);
		NativeListEx.SafeDispose(ref LayerMasks);
		NativeArrayEx.SafeDispose(ref PlayerRadii);
		NativeListEx.SafeDispose(ref ToOverlapIndices);
		NativeListEx.SafeDispose(ref Matrices);
		NativeListEx.SafeDispose(ref ToOverlapFrom);
		NativeListEx.SafeDispose(ref ToOverlapTo);
		NativeListEx.SafeDispose(ref ToOverlapLayerMasks);
		NativeListEx.SafeDispose(ref RaycastIndices);
		NativeListEx.SafeDispose(ref RaycastRays);
		NativeListEx.SafeDispose(ref RaycastSpheres);
		NativeListEx.SafeDispose(ref TraceIndices);
		NativeListEx.SafeDispose(ref TraceRays);
		NativeListEx.SafeDispose(ref TraceSpheres);
		NativeArrayEx.SafeDispose(ref RaycastHits);
		NativeArrayEx.SafeDispose(ref ColliderHits);
		NativeArrayEx.SafeDispose(ref TerrainIgnoreVolumeHits);
		NativeArrayEx.SafeDispose(ref QueryToBatchMap);
		Colliders = null;
	}

	internal static void ValidateMoves(ReadOnlySpan<BasePlayer> players, TickInterpolatorCache.ReadOnlyState tickCache, NativeArray<BasePlayer.CachedState> stateCache, NativeArray<int>.ReadOnly indices, NativeArray<BasePlayer.PositionChange> results)
	{
		using (TimeWarning.New("AntiHack.ValidateMoves"))
		{
			NativeArrayEx.Expand(ref DeltaTimes, players.Length, NativeArrayOptions.UninitializedMemory, copyContents: false);
			NativeListEx.Expand(ref ValidIndexAccum1, indices.Length, copyContents: false);
			NativeListEx.Expand(ref ValidIndexAccum2, indices.Length, copyContents: false);
			ValidIndexAccum1.Clear();
			ValidIndexAccum2.Clear();
			using (TimeWarning.New("ShouldIgnore"))
			{
				foreach (int item in indices)
				{
					int value = item;
					BasePlayer basePlayer = players[value];
					if (ShouldIgnore(basePlayer))
					{
						results[value] = BasePlayer.PositionChange.Valid;
						continue;
					}
					ValidIndexAccum1.Add(in value);
					DeltaTimes[value] = basePlayer.TickDeltaTime;
				}
			}
			NativeListEx.Expand(ref InvalidIndices, ValidIndexAccum1.Length, copyContents: false);
			InvalidIndices.Clear();
			if (Colliders.Capacity < players.Length)
			{
				Colliders.Resize(players.Length);
			}
			AreNoClipping(players, tickCache, DeltaTimes.AsReadOnly(), ValidIndexAccum1.AsReadOnly(), InvalidIndices, Colliders.Buffer);
			NativeArrayEx.Expand(ref FindIndexWorkBuffer, players.Length, NativeArrayOptions.UninitializedMemory, copyContents: false);
			FindValidIndicesJob jobData = new FindValidIndicesJob
			{
				ValidIndices = ValidIndexAccum2,
				WorkBuffer = FindIndexWorkBuffer,
				InvalidIndices = InvalidIndices.AsReadOnly(),
				AllIndices = ValidIndexAccum1.AsReadOnly()
			};
			IJobExtensions.RunByRef(ref jobData);
			using (TimeWarning.New("NoClipRejections"))
			{
				foreach (int invalidIndex in InvalidIndices)
				{
					if (DeltaTimes[invalidIndex] > ConVar.AntiHack.maxdeltatime)
					{
						results[invalidIndex] = BasePlayer.PositionChange.Invalid;
						continue;
					}
					BasePlayer obj = players[invalidIndex];
					Vector3 startPoint = TickInterpolatorCache.GetStartPoint(tickCache, invalidIndex);
					Vector3 endPoint = TickInterpolatorCache.GetEndPoint(tickCache, invalidIndex);
					TickInterpolatorCache.PlayerInfo playerInfo = tickCache.Infos[invalidIndex];
					Facepunch.Rust.Analytics.Azure.OnNoclipViolation(obj, startPoint, endPoint, playerInfo.Count, Colliders[invalidIndex]);
					AddViolation(obj, AntiHackType.NoClip, ConVar.AntiHack.noclip_penalty * playerInfo.Length, Colliders[invalidIndex].gameObject);
					if (ConVar.AntiHack.noclip_reject)
					{
						results[invalidIndex] = BasePlayer.PositionChange.Invalid;
					}
				}
			}
			Array.Clear(Colliders.Buffer, 0, Colliders.Capacity);
			NativeList<int> validIndexAccum = ValidIndexAccum2;
			NativeList<int> validIndexAccum2 = ValidIndexAccum1;
			ValidIndexAccum1 = validIndexAccum;
			ValidIndexAccum2 = validIndexAccum2;
			ValidIndexAccum2.Clear();
			InvalidIndices.Clear();
			foreach (int item2 in ValidIndexAccum1)
			{
				int value2 = item2;
				BasePlayer obj2 = players[value2];
				float deltaTime = DeltaTimes[value2];
				BasePlayer.CachedState initialState = stateCache[value2];
				TickInterpolator tickInterpolator = obj2.TickInterpolator;
				if (IsSpeeding(obj2, tickInterpolator, deltaTime, in initialState))
				{
					InvalidIndices.Add(in value2);
				}
				else
				{
					ValidIndexAccum2.Add(in value2);
				}
			}
			using (TimeWarning.New("IsSpeedingRejections"))
			{
				foreach (int invalidIndex2 in InvalidIndices)
				{
					if (DeltaTimes[invalidIndex2] > ConVar.AntiHack.maxdeltatime)
					{
						results[invalidIndex2] = BasePlayer.PositionChange.Invalid;
						continue;
					}
					BasePlayer obj3 = players[invalidIndex2];
					Vector3 startPoint2 = TickInterpolatorCache.GetStartPoint(tickCache, invalidIndex2);
					Vector3 endPoint2 = TickInterpolatorCache.GetEndPoint(tickCache, invalidIndex2);
					TickInterpolatorCache.PlayerInfo playerInfo2 = tickCache.Infos[invalidIndex2];
					Facepunch.Rust.Analytics.Azure.OnSpeedhackViolation(obj3, startPoint2, endPoint2, playerInfo2.Count);
					AddViolation(obj3, AntiHackType.SpeedHack, ConVar.AntiHack.speedhack_penalty * playerInfo2.Length);
					if (ConVar.AntiHack.speedhack_reject)
					{
						results[invalidIndex2] = BasePlayer.PositionChange.Invalid;
					}
				}
			}
			NativeList<int> validIndexAccum3 = ValidIndexAccum2;
			validIndexAccum2 = ValidIndexAccum1;
			ValidIndexAccum1 = validIndexAccum3;
			ValidIndexAccum2 = validIndexAccum2;
			ValidIndexAccum2.Clear();
			InvalidIndices.Clear();
			foreach (int item3 in ValidIndexAccum1)
			{
				int value3 = item3;
				BasePlayer obj4 = players[value3];
				float deltaTime2 = DeltaTimes[value3];
				BasePlayer.CachedState initialState2 = stateCache[value3];
				TickInterpolator tickInterpolator2 = obj4.TickInterpolator;
				if (IsFlying(obj4, tickInterpolator2, deltaTime2, in initialState2))
				{
					InvalidIndices.Add(in value3);
				}
				else
				{
					ValidIndexAccum2.Add(in value3);
				}
			}
			using (TimeWarning.New("IsFlyingRejections"))
			{
				foreach (int invalidIndex3 in InvalidIndices)
				{
					int value4 = invalidIndex3;
					if (DeltaTimes[value4] > ConVar.AntiHack.maxdeltatime)
					{
						results[value4] = BasePlayer.PositionChange.Invalid;
						continue;
					}
					BasePlayer basePlayer2 = players[value4];
					Vector3 startPoint3 = TickInterpolatorCache.GetStartPoint(tickCache, value4);
					Vector3 endPoint3 = TickInterpolatorCache.GetEndPoint(tickCache, value4);
					TickInterpolatorCache.PlayerInfo playerInfo3 = tickCache.Infos[value4];
					Facepunch.Rust.Analytics.Azure.OnFlyhackViolation(basePlayer2, startPoint3, endPoint3, playerInfo3.Count);
					AddViolation(basePlayer2, AntiHackType.FlyHack, ConVar.AntiHack.flyhack_penalty * playerInfo3.Length);
					if (!ConVar.AntiHack.flyhack_reject)
					{
						continue;
					}
					if (basePlayer2.lastGroundedPosition == default(Vector3))
					{
						ValidIndexAccum2.Add(in value4);
					}
					else if (Vector3.Distance(basePlayer2.lastGroundedPosition, basePlayer2.transform.position) <= 10f)
					{
						Collider col;
						bool num = TestNoClipping(basePlayer2, basePlayer2.transform.position, basePlayer2.lastGroundedPosition, BasePlayer.NoClipRadius(ConVar.AntiHack.noclip_margin), ConVar.AntiHack.noclip_backtracking, out col);
						Vector3 start = basePlayer2.lastGroundedPosition + new Vector3(0f, BasePlayer.GetRadius(), 0f);
						Vector3 end = basePlayer2.lastGroundedPosition + new Vector3(0f, basePlayer2.GetHeight() - BasePlayer.GetRadius(), 0f);
						if (!num && !UnityEngine.Physics.CheckCapsule(start, end, BasePlayer.GetRadius(), 1537286401))
						{
							basePlayer2.MovePosition(basePlayer2.lastGroundedPosition);
							basePlayer2.ClientRPC(RpcTarget.Player("ForcePositionTo", basePlayer2), basePlayer2.transform.position);
							basePlayer2.violationLevel = 0f;
						}
					}
				}
			}
			NativeList<int> validIndexAccum4 = ValidIndexAccum2;
			validIndexAccum2 = ValidIndexAccum1;
			ValidIndexAccum1 = validIndexAccum4;
			ValidIndexAccum2 = validIndexAccum2;
			ValidIndexAccum2.Clear();
			InvalidIndices.Clear();
			using (TimeWarning.New("TickOverflowValidation"))
			{
				foreach (int item4 in ValidIndexAccum1)
				{
					int value5 = item4;
					BasePlayer basePlayer3 = players[value5];
					if (DeltaTimes[value5] < ConVar.AntiHack.tick_buffer_server_lag_threshold && ConVar.AntiHack.tick_buffer_preventions && (float)basePlayer3.rawTickCount >= ConVar.AntiHack.tick_buffer_reject_threshold * (float)Player.tickrate_cl)
					{
						Log(basePlayer3, AntiHackType.Ticks, $"Player had too many ticks buffered ({basePlayer3.rawTickCount})", logToAnalytics: false);
						Vector3 startPoint4 = TickInterpolatorCache.GetStartPoint(tickCache, value5);
						Vector3 endPoint4 = TickInterpolatorCache.GetEndPoint(tickCache, value5);
						Facepunch.Rust.Analytics.Azure.OnTickViolation(basePlayer3, startPoint4, endPoint4, tickCache.Infos[value5].Count);
						results[value5] = BasePlayer.PositionChange.Invalid;
					}
					else
					{
						ValidIndexAccum2.Add(in value5);
					}
				}
			}
			NativeList<int> validIndexAccum5 = ValidIndexAccum2;
			validIndexAccum2 = ValidIndexAccum1;
			ValidIndexAccum1 = validIndexAccum5;
			ValidIndexAccum2 = validIndexAccum2;
			ValidIndexAccum2.Clear();
			if (ConVar.AntiHack.serverside_fall_damage)
			{
				using (TimeWarning.New("ServerSideFallDamageScope"))
				{
					foreach (int item5 in ValidIndexAccum1)
					{
						BasePlayer basePlayer4 = players[item5];
						float deltaTime3 = DeltaTimes[item5];
						Vector3 startPoint5 = TickInterpolatorCache.GetStartPoint(tickCache, item5);
						Vector3 endPoint5 = TickInterpolatorCache.GetEndPoint(tickCache, item5);
						bool num2 = basePlayer4.transform.parent == null;
						Matrix4x4 matrix4x = (num2 ? Matrix4x4.identity : basePlayer4.transform.parent.localToWorldMatrix);
						Vector3 oldPos = (num2 ? startPoint5 : matrix4x.MultiplyPoint3x4(startPoint5));
						Vector3 newPos = (num2 ? endPoint5 : matrix4x.MultiplyPoint3x4(endPoint5));
						TestServerSideFallDamage(basePlayer4, oldPos, newPos, deltaTime3);
					}
				}
			}
			using (TimeWarning.New("MarkPositionsValid"))
			{
				foreach (int item6 in ValidIndexAccum1)
				{
					results[item6] = BasePlayer.PositionChange.Valid;
				}
			}
		}
	}

	public static void ValidateAgainstTerrain(PlayerCache playerCache)
	{
		using (TimeWarning.New("ValidateAgainstTerrain"))
		{
			int num = UnityEngine.Time.frameCount % ConVar.AntiHack.terrain_timeslice;
			foreach (BasePlayer validPlayer in playerCache.ValidPlayers)
			{
				int num2 = (int)(validPlayer.net.ID.Value % (ulong)ConVar.AntiHack.terrain_timeslice);
				if (num == num2 && !ShouldIgnore(validPlayer))
				{
					bool flag = false;
					if (IsInsideTerrain(validPlayer))
					{
						flag = true;
						AddViolation(validPlayer, AntiHackType.InsideTerrain, ConVar.AntiHack.terrain_penalty);
					}
					else if (ConVar.AntiHack.terrain_check_geometry && IsInsideMesh(validPlayer.eyes.position))
					{
						flag = true;
						AddViolation(validPlayer, AntiHackType.InsideGeometry, ConVar.AntiHack.terrain_penalty);
						Log(validPlayer, AntiHackType.InsideGeometry, "Seems to be clipped inside " + isInsideRayHit.collider.name);
					}
					if (flag && ConVar.AntiHack.terrain_kill)
					{
						Facepunch.Rust.Analytics.Azure.OnTerrainHackViolation(validPlayer);
						validPlayer.Hurt(1000f, DamageType.Suicide, validPlayer, useProtection: false);
					}
				}
			}
		}
	}

	public static void ValidateEyeHistory(BasePlayer ply)
	{
		using (TimeWarning.New("AntiHack.ValidateEyeHistory"))
		{
			for (int i = 0; i < ply.eyeHistory.Count; i++)
			{
				Vector3 vector = ply.eyeHistory[i];
				if (ply.tickHistory.Distance(ply, vector) > ConVar.AntiHack.eye_history_forgiveness)
				{
					AddViolation(ply, AntiHackType.EyeHack, ConVar.AntiHack.eye_history_penalty);
					Facepunch.Rust.Analytics.Azure.OnEyehackViolation(ply, vector);
				}
			}
			ply.eyeHistory.Clear();
		}
	}

	public static bool IsInsideTerrain(BasePlayer ply)
	{
		return TestInsideTerrain(ply.transform.position);
	}

	public static bool TestInsideTerrain(Vector3 pos)
	{
		using (TimeWarning.New("AntiHack.TestInsideTerrain"))
		{
			if (!TerrainMeta.Terrain)
			{
				return false;
			}
			if (!TerrainMeta.HeightMap)
			{
				return false;
			}
			if (!TerrainMeta.Collision)
			{
				return false;
			}
			float terrain_padding = ConVar.AntiHack.terrain_padding;
			float height = TerrainMeta.HeightMap.GetHeight(pos);
			if (pos.y > height - terrain_padding)
			{
				return false;
			}
			float num = TerrainMeta.Position.y + TerrainMeta.Terrain.SampleHeight(pos);
			if (pos.y > num - terrain_padding)
			{
				return false;
			}
			if (TerrainMeta.Collision.GetIgnore(pos))
			{
				return false;
			}
			return true;
		}
	}

	public static void TestInsideTerrain(NativeArray<Vector3>.ReadOnly posi, NativeArray<bool> results)
	{
		using (TimeWarning.New("AntiHack.TestInsideTerrain"))
		{
			if (!TerrainMeta.Terrain || !TerrainMeta.HeightMap || !TerrainMeta.Collision)
			{
				for (int i = 0; i < results.Length; i++)
				{
					results[i] = false;
				}
				return;
			}
			NativeArray<float> results2 = new NativeArray<float>(posi.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			JobHandle heights = TerrainMeta.HeightMap.GetHeights(posi, results2);
			JobHandle.ScheduleBatchedJobs();
			NativeArray<float> nativeArray = new NativeArray<float>(posi.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			UnityEngine.Terrain terrain = TerrainMeta.Terrain;
			for (int j = 0; j < posi.Length; j++)
			{
				nativeArray[j] = TerrainMeta.Position.y + terrain.SampleHeight(posi[j]);
			}
			heights.Complete();
			NativeList<int> indicesToCheck = new NativeList<int>(posi.Length, Allocator.TempJob);
			NativeArray<Vector3> posiToCheck = new NativeArray<Vector3>(posi.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			NativeArray<float> radiiToCheck = new NativeArray<float>(posi.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
			InsideTerrainHeightsChecksJob jobData = new InsideTerrainHeightsChecksJob
			{
				Results = results,
				IndicesToCheck = indicesToCheck,
				PosiToCheck = posiToCheck,
				RadiiToCheck = radiiToCheck,
				Posi = posi,
				HeightMapHeights = results2.AsReadOnly(),
				TerrainHeights = nativeArray.AsReadOnly(),
				TerrainPadding = ConVar.AntiHack.terrain_padding,
				RadiusToCheck = 0.01f
			};
			IJobExtensions.RunByRef(ref jobData);
			results2.Dispose();
			nativeArray.Dispose();
			if (!indicesToCheck.IsEmpty)
			{
				NativeArray<Vector3> subArray = posiToCheck.GetSubArray(0, indicesToCheck.Length);
				NativeArray<float> subArray2 = radiiToCheck.GetSubArray(0, indicesToCheck.Length);
				NativeArray<bool> results3 = new NativeArray<bool>(indicesToCheck.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				TerrainMeta.Collision.GetIgnore(subArray.AsReadOnly(), subArray2.AsReadOnly(), results3);
				ScatterInvertedBool jobData2 = new ScatterInvertedBool
				{
					To = results,
					From = results3.AsReadOnly(),
					Indices = indicesToCheck.AsReadOnly()
				};
				IJobExtensions.RunByRef(ref jobData2);
				results3.Dispose();
			}
			radiiToCheck.Dispose();
			posiToCheck.Dispose();
			indicesToCheck.Dispose();
		}
	}

	public static bool IsInsideMesh(Vector3 pos)
	{
		if (ConVar.AntiHack.mesh_inside_check_distance <= 0f)
		{
			return false;
		}
		bool queriesHitBackfaces = UnityEngine.Physics.queriesHitBackfaces;
		if (ConVar.AntiHack.use_legacy_mesh_inside_check)
		{
			UnityEngine.Physics.queriesHitBackfaces = true;
			if (UnityEngine.Physics.Raycast(pos, Vector3.up, out isInsideRayHit, ConVar.AntiHack.mesh_inside_check_distance, 65536))
			{
				UnityEngine.Physics.queriesHitBackfaces = queriesHitBackfaces;
				return Vector3.Dot(Vector3.up, isInsideRayHit.normal) > 0f;
			}
			UnityEngine.Physics.queriesHitBackfaces = queriesHitBackfaces;
			return false;
		}
		UnityEngine.Physics.queriesHitBackfaces = true;
		int num = UnityEngine.Physics.RaycastNonAlloc(pos, Vector3.up, isInsideMeshRaycastHits, ConVar.AntiHack.mesh_inside_check_distance, 65536);
		UnityEngine.Physics.queriesHitBackfaces = queriesHitBackfaces;
		SortHitsByDistance(isInsideMeshRaycastHits, num);
		Collider collider = null;
		for (int i = 0; i < num; i++)
		{
			RaycastHit raycastHit = isInsideMeshRaycastHits[i];
			if (raycastHit.collider.TryGetComponent<ColliderInfo>(out var component) && component.HasFlag(ColliderInfo.Flags.AllowBuildInsideMesh))
			{
				continue;
			}
			if (Vector3.Dot(Vector3.up, raycastHit.normal) > 0f)
			{
				if (collider != raycastHit.collider)
				{
					isInsideRayHit = raycastHit;
					return true;
				}
			}
			else
			{
				collider = raycastHit.collider;
			}
		}
		return false;
	}

	public static void AreInsideMesh(NativeArray<Vector3>.ReadOnly posi, NativeArray<bool> results)
	{
		if (ConVar.AntiHack.mesh_inside_check_distance <= 0f)
		{
			for (int i = 0; i < results.Length; i++)
			{
				results[i] = false;
			}
			return;
		}
		NativeArray<RaycastCommand> commands = new NativeArray<RaycastCommand>(posi.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
		GenerateInsideMeshCommandsJob jobData = new GenerateInsideMeshCommandsJob
		{
			Commands = commands,
			Posi = posi,
			Distance = ConVar.AntiHack.mesh_inside_check_distance
		};
		int batchSize = GamePhysics.GetBatchSize(posi.Length);
		IJobForExtensions.ScheduleParallel(jobData, posi.Length, batchSize, default(JobHandle)).Complete();
		NativeArray<RaycastHit> results2 = new NativeArray<RaycastHit>(posi.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
		int batchSize2 = GamePhysics.GetBatchSize(commands.Length);
		RaycastCommand.ScheduleBatch(commands, results2, batchSize2).Complete();
		commands.Dispose();
		IJobForExtensions.ScheduleParallel(new CheckInsideMeshHitsJob
		{
			Results = results,
			Hits = results2.AsReadOnly()
		}, posi.Length, batchSize, default(JobHandle)).Complete();
		results2.Dispose();
	}

	public static void AreInsideMesh(NativeArray<Vector3>.ReadOnly posi, NativeArray<RaycastHit> hits)
	{
		if (ConVar.AntiHack.mesh_inside_check_distance <= 0f)
		{
			for (int i = 0; i < hits.Length; i++)
			{
				hits[i] = default(RaycastHit);
			}
			return;
		}
		NativeArray<RaycastCommand> commands = new NativeArray<RaycastCommand>(posi.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
		GenerateInsideMeshCommandsJob jobData = new GenerateInsideMeshCommandsJob
		{
			Commands = commands,
			Posi = posi,
			Distance = ConVar.AntiHack.mesh_inside_check_distance
		};
		int batchSize = GamePhysics.GetBatchSize(posi.Length);
		IJobForExtensions.ScheduleParallel(jobData, posi.Length, batchSize, default(JobHandle)).Complete();
		int batchSize2 = GamePhysics.GetBatchSize(commands.Length);
		RaycastCommand.ScheduleBatch(commands, hits, batchSize2).Complete();
		commands.Dispose();
		IJobForExtensions.ScheduleParallel(new FilterInsideMeshHitsJob
		{
			Hits = hits
		}, posi.Length, batchSize, default(JobHandle)).Complete();
	}

	private static void SortHitsByDistance(RaycastHit[] hits, int maxLength)
	{
		for (int i = 0; i < maxLength - 1; i++)
		{
			int num = i;
			for (int j = i + 1; j < maxLength; j++)
			{
				if (hits[j].distance < hits[num].distance)
				{
					num = j;
				}
			}
			if (num != i)
			{
				RaycastHit raycastHit = hits[i];
				hits[i] = hits[num];
				hits[num] = raycastHit;
			}
		}
	}

	public static bool IsNoClipping(BasePlayer ply, TickInterpolator ticks, float deltaTime, out Collider collider)
	{
		collider = null;
		using (TimeWarning.New("AntiHack.IsNoClipping"))
		{
			ply.vehiclePauseTime = Mathf.Max(0f, ply.vehiclePauseTime - deltaTime);
			ply.forceCastTime = Mathf.Max(0f, ply.forceCastTime - deltaTime);
			if (ConVar.AntiHack.noclip_protection <= 0)
			{
				return false;
			}
			ticks.Reset();
			if (!ticks.HasNext())
			{
				return false;
			}
			bool flag = ply.transform.parent == null;
			Matrix4x4 matrix4x = (flag ? Matrix4x4.identity : ply.transform.parent.localToWorldMatrix);
			Vector3 vector = (flag ? ticks.StartPoint : matrix4x.MultiplyPoint3x4(ticks.StartPoint));
			Vector3 vector2 = (flag ? ticks.EndPoint : matrix4x.MultiplyPoint3x4(ticks.EndPoint));
			Vector3 vector3 = BasePlayer.NoClipOffset();
			float radius = BasePlayer.NoClipRadius(ConVar.AntiHack.noclip_margin);
			float noclip_backtracking = ConVar.AntiHack.noclip_backtracking;
			bool vehicleLayer = ply.vehiclePauseTime <= 0f && !ply.isMounted;
			bool forceCast = ply.forceCastTime > 0f;
			int num = ConVar.AntiHack.noclip_protection;
			if (deltaTime < ConVar.AntiHack.tick_buffer_server_lag_threshold && ConVar.AntiHack.tick_buffer_preventions && (float)ply.rawTickCount >= ConVar.AntiHack.tick_buffer_noclip_threshold * (float)Player.tickrate_cl)
			{
				num = Mathf.Min(2, ConVar.AntiHack.noclip_protection);
			}
			if (num >= 3)
			{
				float b = Mathf.Max(ConVar.AntiHack.noclip_stepsize, 0.1f);
				int num2 = Mathf.Max(ConVar.AntiHack.noclip_maxsteps, 1);
				b = Mathf.Max(ticks.Length / (float)num2, b);
				while (ticks.MoveNext(b))
				{
					vector2 = (flag ? ticks.CurrentPoint : matrix4x.MultiplyPoint3x4(ticks.CurrentPoint));
					if (TestNoClipping(ply, vector + vector3, vector2 + vector3, radius, noclip_backtracking, out collider, vehicleLayer, null, forceCast))
					{
						return true;
					}
					vector = vector2;
				}
			}
			else if (TestNoClipping(ply, vector + vector3, vector2 + vector3, radius, noclip_backtracking, out collider, vehicleLayer, null, forceCast))
			{
				return true;
			}
			return false;
		}
	}

	public static void AreNoClipping(ReadOnlySpan<BasePlayer> players, TickInterpolatorCache.ReadOnlyState tickCache, NativeArray<float>.ReadOnly deltaTimes, NativeArray<int>.ReadOnly indices, NativeList<int> foundIndices, Span<Collider> colliders)
	{
		using (TimeWarning.New("AntiHack.AreNoClipping"))
		{
			foreach (int item in indices)
			{
				BasePlayer basePlayer = players[item];
				float num = deltaTimes[item];
				basePlayer.vehiclePauseTime = Mathf.Max(0f, basePlayer.vehiclePauseTime - num);
				basePlayer.forceCastTime = Mathf.Max(0f, basePlayer.forceCastTime - num);
			}
			if (ConVar.AntiHack.noclip_protection <= 0)
			{
				return;
			}
			int num2 = Mathf.Max(ConVar.AntiHack.noclip_maxsteps, 1);
			using (TimeWarning.New("GatherBatches"))
			{
				NativeListEx.Expand(ref ToOverlapIndices, indices.Length, copyContents: false);
				GatherPlayersWithTicksJob jobData = new GatherPlayersWithTicksJob
				{
					ValidIndices = ToOverlapIndices,
					TickCache = tickCache,
					Indices = indices
				};
				IJobExtensions.RunByRef(ref jobData);
				using (TimeWarning.New("GatherPlayerInfo"))
				{
					NativeListEx.Expand(ref Batches, ToOverlapIndices.Length, copyContents: false);
					NativeListEx.Expand(ref Matrices, ToOverlapIndices.Length, copyContents: false);
					foreach (int toOverlapIndex in ToOverlapIndices)
					{
						BasePlayer basePlayer2 = players[toOverlapIndex];
						Transform parent = basePlayer2.transform.parent;
						Matrix4x4 value = ((parent == null) ? Matrix4x4.zero : parent.localToWorldMatrix);
						Matrices.AddNoResize(value);
						bool excludeVehicleLayer = basePlayer2.vehiclePauseTime <= 0f && !basePlayer2.isMounted;
						bool force = basePlayer2.forceCastTime > 0f;
						Batches.AddNoResize(new Batch
						{
							PlayerIndex = toOverlapIndex,
							Count = (int)basePlayer2.rawTickCount,
							Force = force,
							ExcludeVehicleLayer = excludeVehicleLayer
						});
					}
				}
				NativeListEx.Expand(ref From, ToOverlapIndices.Length * num2, copyContents: false);
				NativeListEx.Expand(ref To, ToOverlapIndices.Length * num2, copyContents: false);
				GatherNoClipBatchesJob jobData2 = new GatherNoClipBatchesJob
				{
					From = From,
					To = To,
					Batches = Batches.AsArray(),
					TickCache = tickCache,
					Indices = ToOverlapIndices.AsReadOnly(),
					Matrices = Matrices.AsReadOnly(),
					DeltaTimes = deltaTimes,
					MaxSteps = num2,
					DefaultStepSize = Mathf.Max(ConVar.AntiHack.noclip_stepsize, 0.1f),
					DefaultProtection = ConVar.AntiHack.noclip_protection,
					MaxTickCount = ConVar.AntiHack.tick_buffer_noclip_threshold * (float)Player.tickrate_cl,
					LagThreshold = ConVar.AntiHack.tick_buffer_server_lag_threshold,
					TickBufferPrevention = ConVar.AntiHack.tick_buffer_preventions
				};
				IJobExtensions.RunByRef(ref jobData2);
			}
			foundIndices.Clear();
			if (!Batches.IsEmpty)
			{
				TestAreNoClipping(players, From.AsReadOnly(), To.AsReadOnly(), Batches.AsReadOnly(), foundIndices, colliders);
			}
		}
	}

	internal static bool IsSpeeding(BasePlayer ply, TickInterpolator ticks, float deltaTime, in BasePlayer.CachedState initialState)
	{
		using (TimeWarning.New("AntiHack.IsSpeeding"))
		{
			ply.speedhackPauseTime = Mathf.Max(0f, ply.speedhackPauseTime - deltaTime);
			ply.speedhackExtraSpeedTime = Mathf.Max(0f, ply.speedhackExtraSpeedTime - deltaTime);
			if (ConVar.AntiHack.speedhack_protection <= 0)
			{
				return false;
			}
			bool num = ply.transform.parent == null;
			Matrix4x4 matrix4x = (num ? Matrix4x4.identity : ply.transform.parent.localToWorldMatrix);
			Vector3 vector = (num ? ticks.StartPoint : matrix4x.MultiplyPoint3x4(ticks.StartPoint));
			Vector3 obj = (num ? ticks.EndPoint : matrix4x.MultiplyPoint3x4(ticks.EndPoint));
			float running = 1f;
			float ducking = 0f;
			float crawling = 0f;
			bool flag = false;
			if (ConVar.AntiHack.speedhack_protection >= 2)
			{
				bool flag2 = ply.IsRunning();
				bool flag3 = ply.IsDucked();
				flag = initialState.IsSwimming;
				bool num2 = ply.IsCrawling();
				running = (flag2 ? 1f : 0f);
				ducking = ((flag3 || flag) ? 1f : 0f);
				crawling = (num2 ? 1f : 0f);
			}
			float speed = ply.GetSpeed(running, ducking, crawling, initialState.IsSwimming);
			Vector3 v = obj - vector;
			float num3 = ((flag && ConVar.AntiHack.speedhack_protection >= 3) ? v.magnitude : v.Magnitude2D());
			float num4 = deltaTime * speed;
			if (!flag && num3 > num4)
			{
				Vector3 v2 = (TerrainMeta.HeightMap ? TerrainMeta.HeightMap.GetNormal(vector) : Vector3.up);
				float num5 = Mathf.Max(0f, Vector3.Dot(v2.XZ3D(), v.XZ3D())) * ConVar.AntiHack.speedhack_slopespeed * deltaTime;
				num3 = Mathf.Max(0f, num3 - num5);
			}
			float num6 = Mathf.Max((ply.speedhackPauseTime > 0f) ? ConVar.AntiHack.speedhack_forgiveness_inertia : ConVar.AntiHack.speedhack_forgiveness, 0.1f);
			float num7 = num6 + Mathf.Max(ConVar.AntiHack.speedhack_forgiveness, 0.1f);
			ply.speedhackDistance = Mathf.Clamp(ply.speedhackDistance, 0f - num7, num7);
			float num8 = ((ply.speedhackExtraSpeedTime > 0f) ? (ply.speedhackExtraSpeed * deltaTime) : 0f);
			ply.speedhackDistance = Mathf.Clamp(ply.speedhackDistance - num4 - num8, 0f - num7, num7);
			if (ply.speedhackDistance > num6)
			{
				return true;
			}
			ply.speedhackDistance = Mathf.Clamp(ply.speedhackDistance + num3, 0f - num7, num7);
			if (ply.speedhackDistance > num6)
			{
				return true;
			}
			return false;
		}
	}

	internal static bool IsFlying(BasePlayer ply, TickInterpolator ticks, float deltaTime, in BasePlayer.CachedState initialState)
	{
		using (TimeWarning.New("AntiHack.IsFlying"))
		{
			ply.flyhackPauseTime = Mathf.Max(0f, ply.flyhackPauseTime - deltaTime);
			if (ConVar.AntiHack.flyhack_protection <= 0)
			{
				return false;
			}
			ticks.Reset();
			if (!ticks.HasNext())
			{
				return false;
			}
			bool flag = ply.transform.parent == null;
			Matrix4x4 matrix4x = (flag ? Matrix4x4.identity : ply.transform.parent.localToWorldMatrix);
			Vector3 oldPos = (flag ? ticks.StartPoint : matrix4x.MultiplyPoint3x4(ticks.StartPoint));
			Vector3 newPos = (flag ? ticks.EndPoint : matrix4x.MultiplyPoint3x4(ticks.EndPoint));
			BasePlayer.CachedState? playerState = null;
			if (ConVar.AntiHack.flyhack_usecachedstate)
			{
				playerState = initialState;
			}
			if (ConVar.AntiHack.flyhack_protection >= 3)
			{
				float b = Mathf.Max(ConVar.AntiHack.flyhack_stepsize, 0.1f);
				int num = Mathf.Max(ConVar.AntiHack.flyhack_maxsteps, 1);
				b = Mathf.Max(ticks.Length / (float)num, b);
				while (ticks.MoveNext(b))
				{
					newPos = (flag ? ticks.CurrentPoint : matrix4x.MultiplyPoint3x4(ticks.CurrentPoint));
					if (TestFlying(ply, oldPos, newPos, verifyGrounded: true, in playerState))
					{
						return true;
					}
					playerState = null;
					oldPos = newPos;
				}
			}
			else if (ConVar.AntiHack.flyhack_protection >= 2)
			{
				if (TestFlying(ply, oldPos, newPos, verifyGrounded: true, in playerState))
				{
					return true;
				}
			}
			else if (TestFlying(ply, oldPos, newPos, verifyGrounded: false, in playerState))
			{
				return true;
			}
			return false;
		}
	}

	internal static bool TestFlying(BasePlayer ply, Vector3 oldPos, Vector3 newPos, bool verifyGrounded, in BasePlayer.CachedState? playerState)
	{
		bool isInAir = ply.isInAir;
		if (!ply.isInAir)
		{
			ply.lastGroundedPosition = oldPos;
		}
		ply.isInAir = false;
		ply.isOnPlayer = false;
		if (verifyGrounded)
		{
			float flyhack_extrusion = ConVar.AntiHack.flyhack_extrusion;
			Vector3 vector = (oldPos + newPos) * 0.5f;
			if (!ply.OnLadder())
			{
				bool num;
				if (!playerState.HasValue)
				{
					num = WaterLevel.Test(vector - new Vector3(0f, flyhack_extrusion, 0f), waves: true, volumes: true, ply);
				}
				else
				{
					BasePlayer.CachedState value = playerState.Value;
					num = IsInWaterCached(in value.WaterInfo, oldPos - new Vector3(0f, flyhack_extrusion, 0f), ply);
				}
				if (num)
				{
					if (ply.waterDelay <= 0f)
					{
						ply.waterDelay = 0.3f;
					}
				}
				else if ((EnvironmentManager.Get(vector) & EnvironmentType.Elevator) == 0)
				{
					float flyhack_margin = ConVar.AntiHack.flyhack_margin;
					float radius = BasePlayer.GetRadius();
					float height = BasePlayer.GetHeight(ducked: false);
					Vector3 vector2 = vector + new Vector3(0f, radius - flyhack_extrusion, 0f);
					Vector3 vector3 = vector + new Vector3(0f, height - radius, 0f);
					float radius2 = radius - flyhack_margin;
					ply.isInAir = !UnityEngine.Physics.CheckCapsule(vector2, vector3, radius2, 1503764737, QueryTriggerInteraction.Ignore);
					if (ply.isInAir)
					{
						int num2 = UnityEngine.Physics.OverlapCapsuleNonAlloc(vector2, vector3, radius2, buffer, 131072, QueryTriggerInteraction.Ignore);
						for (int i = 0; i < num2; i++)
						{
							BasePlayer basePlayer = GameObjectEx.ToBaseEntity(buffer[i].gameObject) as BasePlayer;
							if (!(basePlayer == null) && !(basePlayer == ply) && !basePlayer.isInAir && !basePlayer.isOnPlayer && !basePlayer.TriggeredAntiHack() && !basePlayer.IsSleeping())
							{
								ply.isOnPlayer = true;
								ply.isInAir = false;
								break;
							}
						}
						for (int j = 0; j < buffer.Length; j++)
						{
							buffer[j] = null;
						}
					}
				}
			}
		}
		else
		{
			ply.isInAir = !ply.OnLadder() && !ply.IsSwimming() && !ply.IsOnGround();
		}
		if (ply.isInAir)
		{
			bool flag = false;
			Vector3 v = newPos - oldPos;
			float num3 = Mathf.Abs(v.y);
			float num4 = v.Magnitude2D();
			if (v.y >= 0f)
			{
				ply.flyhackDistanceVertical += v.y;
				flag = true;
			}
			if (num3 < num4)
			{
				ply.flyhackDistanceHorizontal += num4;
				flag = true;
			}
			if (flag)
			{
				float num5 = Mathf.Max((ply.flyhackPauseTime > 0f) ? ConVar.AntiHack.flyhack_forgiveness_vertical_inertia : ConVar.AntiHack.flyhack_forgiveness_vertical, 0f);
				float num6 = BasePlayer.GetJumpHeight() + num5;
				if (ply.flyhackDistanceVertical > num6)
				{
					return true;
				}
				float num7 = Mathf.Max((ply.flyhackPauseTime > 0f) ? ConVar.AntiHack.flyhack_forgiveness_horizontal_inertia : ConVar.AntiHack.flyhack_forgiveness_horizontal, 0f);
				float num8 = 5f + num7;
				if (ply.flyhackDistanceHorizontal > num8)
				{
					return true;
				}
			}
		}
		else
		{
			if (isInAir)
			{
				ply.lastInAirTime = UnityEngine.Time.realtimeSinceStartup;
			}
			ply.flyhackDistanceVertical = 0f;
			ply.flyhackDistanceHorizontal = 0f;
		}
		return false;
		static bool IsInWaterCached(in WaterLevel.WaterInfo cachedInfo, Vector3 adjustedPos, BasePlayer player)
		{
			if (!cachedInfo.isValid)
			{
				return WaterLevel.Test(in cachedInfo, volumes: true, adjustedPos, player);
			}
			return true;
		}
	}

	public static bool TestServerSideFallDamage(BasePlayer ply, Vector3 oldPos, Vector3 newPos, float deltaTime)
	{
		if (ply.waterDelay >= 0f)
		{
			ply.waterDelay -= deltaTime;
		}
		if (ply.isInAir)
		{
			Vector3 vector = newPos - oldPos;
			if (vector.y < 0f)
			{
				if (ply.timeInAir == 0f)
				{
					ply.initialVelocity = ply.estimatedVelocity;
					ply.fallingDistance = ply.GetHeight();
					ply.timeInAir = 1f;
				}
				ply.timeInAir += deltaTime;
				ply.fallingDistance += vector.y;
				if (ply.estimatedVelocity.y < ply.fallingVelocity)
				{
					ply.fallingVelocity = ply.estimatedVelocity.y;
				}
				ply.fallingVelocity = ply.estimatedVelocity.y;
			}
		}
		else if (ply.waterDelay <= 0f)
		{
			if (ply.OnLadder() || ply.IsSwimming())
			{
				ResetServerFall(ply);
				return false;
			}
			float num = 0f - Mathf.Sqrt(Mathf.Abs(0f - ply.initialVelocity.magnitude * ply.initialVelocity.magnitude + 2f * UnityEngine.Physics.gravity.y * ply.fallingDistance) * 1.4f);
			if (ply.fallingVelocity < 0f || (num < 0f && ply.timeInAir > 0f))
			{
				float num2 = Mathf.Max(Mathf.Abs(num), Mathf.Abs(ply.fallingVelocity));
				ply.ApplyFallDamageFromVelocity(0f - num2);
				ResetServerFall(ply);
			}
		}
		return false;
	}

	public static void ResetServerFall(BasePlayer ply)
	{
		ply.fallingVelocity = 0f;
		ply.fallingDistance = 0f;
		ply.timeInAir = 0f;
		ply.initialVelocity = default(Vector3);
	}

	public static bool TestIsBuildingInsideSomething(Construction.Target target, Vector3 deployPos)
	{
		if (ConVar.AntiHack.build_inside_check <= 0)
		{
			return false;
		}
		foreach (MonumentInfo monument in TerrainMeta.Path.Monuments)
		{
			if (monument.IsInBounds(deployPos))
			{
				return false;
			}
		}
		if (IsInsideMesh(deployPos) && IsInsideMesh(target.ray.origin))
		{
			LogToConsoleBatched(target.player, AntiHackType.InsideGeometry, "Tried to build while clipped inside " + isInsideRayHit.collider.name, 25f);
			if (ConVar.AntiHack.build_inside_check > 1)
			{
				return true;
			}
		}
		return false;
	}

	public static void FadeViolations(BasePlayer ply, float deltaTime)
	{
		if (UnityEngine.Time.realtimeSinceStartup - ply.lastViolationTime > ConVar.AntiHack.relaxationpause)
		{
			ply.violationLevel = Mathf.Max(0f, ply.violationLevel - ConVar.AntiHack.relaxationrate * deltaTime);
		}
	}

	public static void EnforceViolations(BasePlayer ply)
	{
		if (ConVar.AntiHack.enforcementlevel > 0 && ply.violationLevel > ConVar.AntiHack.maxviolation)
		{
			if (ConVar.AntiHack.debuglevel >= 1)
			{
				LogToConsole(ply, ply.lastViolationType, "Enforcing (violation of " + ply.violationLevel + ")");
			}
			string reason = ply.lastViolationType.ToString() + " Violation Level " + ply.violationLevel;
			if (ConVar.AntiHack.enforcementlevel > 1)
			{
				Kick(ply, reason);
			}
			else
			{
				Kick(ply, reason);
			}
		}
	}

	public static void Log(BasePlayer ply, AntiHackType type, string message, bool logToAnalytics = true)
	{
		if (ConVar.AntiHack.debuglevel > 1)
		{
			LogToConsole(ply, type, message);
		}
		if (logToAnalytics)
		{
			Facepunch.Rust.Analytics.Azure.OnAntihackViolation(ply, type, message);
		}
		LogToEAC(ply, type, message);
	}

	public static void LogToConsoleBatched(BasePlayer ply, AntiHackType type, string message, float maxDistance)
	{
		string playerName = ply.ToString();
		Vector3 position = ply.transform.position;
		foreach (GroupedLog groupedLog2 in groupedLogs)
		{
			if (groupedLog2.TryGroup(playerName, type, message, position, maxDistance))
			{
				return;
			}
		}
		GroupedLog groupedLog = Facepunch.Pool.Get<GroupedLog>();
		groupedLog.SetInitial(playerName, type, message, position);
		groupedLogs.Enqueue(groupedLog);
	}

	private static void LogToConsole(BasePlayer ply, AntiHackType type, string message)
	{
		Debug.LogWarning(ply?.ToString() + " " + type.ToString() + ": " + message + " at " + ply.transform.position.ToString());
	}

	private static void LogToConsole(string plyName, AntiHackType type, string message, Vector3 pos)
	{
		string[] obj = new string[7]
		{
			plyName,
			" ",
			type.ToString(),
			": ",
			message,
			" at ",
			null
		};
		Vector3 vector = pos;
		obj[6] = vector.ToString();
		Debug.LogWarning(string.Concat(obj));
	}

	private static void LogToEAC(BasePlayer ply, AntiHackType type, string message)
	{
		if (ConVar.AntiHack.reporting)
		{
			EACServer.SendPlayerBehaviorReport(PlayerReportsCategory.Exploiting, ply.UserIDString, type.ToString() + ": " + message);
		}
	}

	public static void AddViolation(BasePlayer ply, AntiHackType type, float amount, GameObject gameObject = null)
	{
		if (Interface.CallHook("OnPlayerViolation", ply, type, amount, gameObject) != null)
		{
			return;
		}
		using (TimeWarning.New("AntiHack.AddViolation"))
		{
			ply.lastViolationType = type;
			ply.lastViolationTime = UnityEngine.Time.realtimeSinceStartup;
			ply.violationLevel += amount;
			if (type == AntiHackType.NoClip || type == AntiHackType.FlyHack || type == AntiHackType.SpeedHack || type == AntiHackType.InsideGeometry || type == AntiHackType.InsideTerrain || type == AntiHackType.Ticks)
			{
				ply.lastMovementViolationTime = UnityEngine.Time.realtimeSinceStartup;
			}
			if ((ConVar.AntiHack.debuglevel >= 2 && amount > 0f) || (ConVar.AntiHack.debuglevel >= 3 && type != AntiHackType.NoClip) || ConVar.AntiHack.debuglevel >= 4)
			{
				string text = "Added violation of " + amount + " in frame " + UnityEngine.Time.frameCount + " (now has " + ply.violationLevel + ")";
				if (gameObject != null)
				{
					text = text + " " + gameObject.name;
					BaseEntity baseEntity = GameObjectEx.ToBaseEntity(gameObject);
					if (baseEntity != null)
					{
						text = text + " (entity: " + baseEntity.ShortPrefabName + ")";
					}
				}
				LogToConsole(ply, type, text);
			}
			EnforceViolations(ply);
		}
	}

	public static void Kick(BasePlayer ply, string reason)
	{
		AddRecord(ply, kicks);
		ply.Kick(reason);
	}

	public static void Ban(BasePlayer ply, string reason)
	{
		AddRecord(ply, bans);
		ConsoleSystem.Run(ConsoleSystem.Option.Server, "ban", ply.userID.Get(), reason);
	}

	private static void AddRecord(BasePlayer ply, Dictionary<ulong, int> records)
	{
		if (records.ContainsKey(ply.userID))
		{
			records[ply.userID]++;
		}
		else
		{
			records.Add(ply.userID, 1);
		}
	}

	public static int GetKickRecord(BasePlayer ply)
	{
		return GetRecord(ply, kicks);
	}

	public static int GetBanRecord(BasePlayer ply)
	{
		return GetRecord(ply, bans);
	}

	private static int GetRecord(BasePlayer ply, Dictionary<ulong, int> records)
	{
		if (!records.ContainsKey(ply.userID))
		{
			return 0;
		}
		return records[ply.userID];
	}
}
