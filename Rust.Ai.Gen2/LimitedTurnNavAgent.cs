using System;
using System.Diagnostics;
using ConVar;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Rust.Ai.Gen2;

[SoftRequireComponent(typeof(NavMeshAgent))]
public class LimitedTurnNavAgent : EntityComponent<BaseEntity>
{
	public enum Speeds
	{
		Sneak,
		Walk,
		Jog,
		Run,
		Sprint,
		FullSprint
	}

	[SerializeField]
	private NavMeshAgent agent;

	[SerializeField]
	[Header("Speed")]
	private float sneakSpeed = 0.6f;

	[SerializeField]
	private float walkSpeed = 0.89f;

	[SerializeField]
	private float jogSpeed = 2.45f;

	[SerializeField]
	private float runSpeed = 4.4f;

	[SerializeField]
	private float sprintSpeed = 6f;

	[SerializeField]
	private float fullSprintSpeed = 9f;

	[SerializeField]
	public bool canSwim;

	[SerializeField]
	private float swimSpeed = 0.6f;

	[SerializeField]
	private float swimSprintSpeed = 0.89f;

	public ResettableFloat desiredSwimDepth = new ResettableFloat(0.7f);

	public ResettableFloat acceleration = new ResettableFloat(10f);

	public ResettableFloat deceleration = new ResettableFloat(2f);

	[SerializeField]
	private float maxTurnRadius = 2f;

	[SerializeField]
	private TerrainTopology.Enum preferedTopology = TerrainTopology.Enum.Field | TerrainTopology.Enum.Forest | TerrainTopology.Enum.Forestside | TerrainTopology.Enum.Lakeside | TerrainTopology.Enum.Mainland;

	[SerializeField]
	private TerrainBiome.Enum preferedBiome = TerrainBiome.Enum.Arid | TerrainBiome.Enum.Temperate | TerrainBiome.Enum.Tundra | TerrainBiome.Enum.Arctic;

	public const BaseEntity.Flags FLAG_IS_SWIMMING = BaseEntity.Flags.Reserved1;

	public const BaseEntity.Flags FLAG_IS_JUMPING = BaseEntity.Flags.Reserved2;

	private const float emergencyDeceleration = 10f;

	private static NavMeshPath path;

	[NonSerialized]
	public UnityEvent onPathFailed = new UnityEvent();

	private LockState movementLock = new LockState();

	private bool isNavMeshReady;

	private int? lastFrameCall;

	private static ListHashSet<LimitedTurnNavAgent> steeringComponents = new ListHashSet<LimitedTurnNavAgent>();

	[NonSerialized]
	public float currentDeviation;

	[NonSerialized]
	public bool shouldStopAtDestination = true;

	private float cachedPathLength;

	private Vector3? previousLocalPosition;

	private float curSpeed;

	private float desiredSpeed;

	public bool IsSwimming
	{
		get
		{
			return base.baseEntity.flags.HasFlag(BaseEntity.Flags.Reserved1);
		}
		private set
		{
			base.baseEntity.SetFlag(BaseEntity.Flags.Reserved1, value);
		}
	}

	public bool IsJumping
	{
		get
		{
			return base.baseEntity.flags.HasFlag(BaseEntity.Flags.Reserved2);
		}
		set
		{
			base.baseEntity.SetFlag(BaseEntity.Flags.Reserved2, value);
		}
	}

	public Vector3 NavPosition => agent.nextPosition;

	public bool isPaused => movementLock.IsLocked;

	public bool IsNavmeshReady => isNavMeshReady;

	public Vector3? lastValidDestination { get; private set; }

	public float RemainingDistance => agent.remainingDistance;

	public bool IsFollowingPath
	{
		get
		{
			if (agent.hasPath)
			{
				return agent.remainingDistance > (shouldStopAtDestination ? base.baseEntity.bounds.extents.z : maxTurnRadius);
			}
			return false;
		}
	}

	public LockState.LockHandle Pause()
	{
		if (!movementLock.IsLocked)
		{
			OnPaused();
		}
		return movementLock.AddLock();
	}

	public bool Unpause(ref LockState.LockHandle handle)
	{
		bool result = movementLock.RemoveLock(ref handle);
		if (!movementLock.IsLocked)
		{
			OnUnpaused();
		}
		return result;
	}

	public void Move(Vector3 offset)
	{
		using (TimeWarning.New("LimitedTurnNavAgent:Move"))
		{
			if (AI.logIssues && lastFrameCall.HasValue && lastFrameCall == UnityEngine.Time.frameCount)
			{
				StackTrace stackTrace = new StackTrace();
				UnityEngine.Debug.LogError("Move called multiple times in the same frame\n" + stackTrace.ToString());
			}
			agent.Move(offset);
			lastFrameCall = UnityEngine.Time.frameCount;
			if (canSwim)
			{
				Vector3 nextPosition = agent.nextPosition;
				WaterLevel.WaterInfo waterInfo = WaterLevel.GetWaterInfo(nextPosition, waves: false, volumes: false);
				IsSwimming = waterInfo.currentDepth > desiredSwimDepth.Value;
				if (IsSwimming)
				{
					nextPosition.y = base.baseEntity.transform.position.y;
					nextPosition.y = Mathf.MoveTowards(nextPosition.y, waterInfo.surfaceLevel - desiredSwimDepth.Value, 1f * UnityEngine.Time.deltaTime);
					nextPosition.y = Mathf.Max(nextPosition.y, waterInfo.terrainHeight);
					base.baseEntity.transform.position = nextPosition;
				}
				else
				{
					base.baseEntity.transform.position = agent.nextPosition;
				}
			}
			else
			{
				IsSwimming = false;
				base.baseEntity.transform.position = agent.nextPosition;
			}
		}
	}

	public void ResetPath()
	{
		using (TimeWarning.New("LimitedTurnNavAgent:ResetPath"))
		{
			shouldStopAtDestination = true;
			acceleration.Reset();
			deceleration.Reset();
			currentDeviation = 0f;
			SetSpeed(0f);
			if (agent.hasPath)
			{
				agent.ResetPath();
			}
		}
	}

	public bool CanReach(Vector3 location, bool resetPathOnFailure = false)
	{
		using (TimeWarning.New("LimitedTurnNavAgent:CanReach"))
		{
			if (!IsPositionOnNavmesh(location, out var sample))
			{
				FailPath(location, null, resetPathOnFailure);
				return false;
			}
			if (!CalculatePathCustom(sample, path))
			{
				FailPath(sample, path, resetPathOnFailure);
				return false;
			}
			bool flag = path.status == NavMeshPathStatus.PathComplete;
			if (!flag)
			{
				FailPath(sample, path, resetPathOnFailure);
			}
			else if (flag && resetPathOnFailure)
			{
				lastValidDestination = NavMeshPathEx.GetDestination(path);
			}
			return flag;
		}
	}

	public bool SetDestination(Vector3 newDestination, bool resetPathOnFailure = false)
	{
		using (TimeWarning.New("LimitedTurnNavAgent:SetDestination"))
		{
			if (shouldStopAtDestination && agent.hasPath && Vector3.Distance(agent.destination, newDestination) < 1f)
			{
				return true;
			}
			if (!CalculatePathCustom(newDestination, path))
			{
				FailPath(newDestination, path, resetPathOnFailure);
				return false;
			}
			if (path.status != NavMeshPathStatus.PathComplete)
			{
				FailPath(newDestination, path, resetPathOnFailure);
				return false;
			}
			SetPath(path);
			return true;
		}
	}

	public override void InitShared()
	{
		base.InitShared();
		if (path == null)
		{
			path = new NavMeshPath();
		}
	}

	private void OnPaused()
	{
		if (agent.enabled && agent.isOnNavMesh)
		{
			ResetPath();
		}
	}

	private void OnUnpaused()
	{
	}

	private void SetPath(NavMeshPath newPath)
	{
		using (TimeWarning.New("LimitedTurnNavAgent:SetPath"))
		{
			if (agent.path != newPath)
			{
				agent.SetPath(newPath);
			}
			cachedPathLength = NavMeshPathEx.GetPathLength(newPath);
			lastValidDestination = NavMeshPathEx.GetDestination(newPath);
		}
	}

	private void ShowFailedPath(Vector3? destination, NavMeshPath failedPath)
	{
	}

	private void FailPath(Vector3? destination, NavMeshPath failedPath = null, bool resetPathOnFailure = false)
	{
		ShowFailedPath(destination, failedPath);
		if (resetPathOnFailure)
		{
			onPathFailed.Invoke();
			ResetPath();
		}
	}

	private float GetSpeedForGait(Speeds gait)
	{
		return gait switch
		{
			Speeds.Sneak => sneakSpeed, 
			Speeds.Walk => walkSpeed, 
			Speeds.Jog => jogSpeed, 
			Speeds.Run => runSpeed, 
			Speeds.Sprint => sprintSpeed, 
			Speeds.FullSprint => fullSprintSpeed, 
			_ => walkSpeed, 
		};
	}

	public void SetSpeed(Speeds gait)
	{
		SetSpeed(GetSpeedForGait(gait));
	}

	public bool IsSpeedGTE(Speeds minGait)
	{
		return curSpeed >= GetSpeedForGait(minGait) - 0.01f;
	}

	public void SetSpeed(float speed)
	{
		desiredSpeed = speed;
	}

	public void SetSpeedRatio(float ratio, Speeds minSpeed = Speeds.Sneak, Speeds maxSpeed = Speeds.Sprint, int offset = 0)
	{
		int num = Mathf.FloorToInt(Mathf.Lerp((float)minSpeed, (float)maxSpeed, ratio));
		num = Mathf.Clamp(num + offset, (int)minSpeed, (int)maxSpeed);
		SetSpeed((Speeds)num);
	}

	private void OnEnable()
	{
		steeringComponents.TryAdd(this);
	}

	private void OnDisable()
	{
		steeringComponents.Remove(this);
	}

	public static void TickSteering()
	{
		for (int num = steeringComponents.Count - 1; num >= 0; num--)
		{
			LimitedTurnNavAgent limitedTurnNavAgent = steeringComponents[num];
			if (ObjectEx.IsUnityNull(limitedTurnNavAgent) || !limitedTurnNavAgent.baseEntity.IsValid())
			{
				steeringComponents.RemoveAt(num);
			}
			else
			{
				limitedTurnNavAgent.Tick();
			}
		}
	}

	private void Tick()
	{
		using (TimeWarning.New("LimitedTurnNavAgent:Tick"))
		{
			try
			{
				if (!AI.move)
				{
					return;
				}
				if (!isNavMeshReady)
				{
					isNavMeshReady = agent != null && agent.enabled && agent.isOnNavMesh;
					if (!isNavMeshReady)
					{
						return;
					}
					agent.updateRotation = false;
					agent.updatePosition = false;
					agent.updateUpAxis = false;
					agent.isStopped = true;
				}
				if (movementLock.IsLocked)
				{
					if (previousLocalPosition.HasValue)
					{
						curSpeed = (base.baseEntity.transform.localPosition - previousLocalPosition.Value).magnitude / UnityEngine.Time.deltaTime;
					}
					return;
				}
				if (IsSwimming && curSpeed > swimSprintSpeed)
				{
					if (AI.logIssues)
					{
						UnityEngine.Debug.LogError($"Speed is too high: {curSpeed}/{swimSprintSpeed}");
					}
					curSpeed = swimSpeed;
				}
				else if (!IsSwimming && curSpeed > fullSprintSpeed)
				{
					if (AI.logIssues)
					{
						UnityEngine.Debug.LogError($"Speed is too high: {curSpeed}/{fullSprintSpeed}");
					}
					curSpeed = fullSprintSpeed;
				}
				if (!shouldStopAtDestination || IsFollowingPath)
				{
					SteerTowardsWaypoint();
					return;
				}
				curSpeed = Mathf.Max(desiredSpeed, curSpeed - 10f * UnityEngine.Time.deltaTime);
				ResetPath();
			}
			finally
			{
				previousLocalPosition = base.baseEntity.transform.localPosition;
			}
		}
	}

	private static float GetBrakingDistance(float speed, float brakingDeceleration)
	{
		float num = speed / Mathf.Max(brakingDeceleration, 0.001f);
		return 0.5f * brakingDeceleration * num * num;
	}

	private float AdjustSpeedForSwimming(float speed)
	{
		if (!IsSwimming || speed <= 0f)
		{
			return speed;
		}
		if (!(speed < sprintSpeed))
		{
			return swimSprintSpeed;
		}
		return swimSpeed;
	}

	private void SteerTowardsWaypoint()
	{
		using (TimeWarning.New("SteerTowardsWaypoint"))
		{
			Transform transform = base.baseEntity.transform;
			Vector3 vector = (agent.steeringTarget - transform.position).normalized;
			if (Mathf.Abs(cachedPathLength - Vector3.Distance(transform.position, agent.destination)) < 5f)
			{
				vector = Quaternion.AngleAxis(currentDeviation, Vector3.up) * vector;
			}
			float num = AdjustSpeedForSwimming(desiredSpeed);
			if (shouldStopAtDestination && agent.remainingDistance - maxTurnRadius < GetBrakingDistance(curSpeed, deceleration.Value))
			{
				curSpeed = Mathf.Max(1f, curSpeed - deceleration.Value * UnityEngine.Time.deltaTime);
			}
			else if (curSpeed > num)
			{
				float num2 = (curSpeed - num) / deceleration.Value;
				float num3 = ((curSpeed > walkSpeed && num2 > 1f) ? 10f : deceleration.Value);
				curSpeed = Mathf.Max(num, curSpeed - num3 * UnityEngine.Time.deltaTime);
			}
			else if (curSpeed < num)
			{
				curSpeed = Mathf.Min(num, curSpeed + acceleration.Value * UnityEngine.Time.deltaTime);
			}
			agent.isStopped = true;
			if (!(vector.magnitude < 0.01f))
			{
				float num4 = (shouldStopAtDestination ? Mathx.RemapValClamped(agent.remainingDistance, maxTurnRadius * 2f, 0f, maxTurnRadius, 0.001f) : maxTurnRadius);
				float num5 = curSpeed / num4;
				Vector3 vector2 = Vector3.RotateTowards(transform.forward, vector, num5 * UnityEngine.Time.deltaTime, 0f);
				Vector3 offset = vector2 * (curSpeed * UnityEngine.Time.deltaTime);
				transform.rotation = Quaternion.LookRotation(vector2.WithY(0f));
				Move(offset);
			}
		}
	}

	public bool IsPositionOnNavmesh(Vector3 position, out Vector3 sample)
	{
		return SamplePosition(position, out sample, 0.5f);
	}

	public bool SampleGroundPositionWithPhysics(Vector3 position, out RaycastHit hitInfo, float maxDistance = 2f, float radius = 0f, int layerMask = 1503731969)
	{
		using (TimeWarning.New("SampleGroundPositionWithPhysics"))
		{
			Vector3 origin = position + Vector3.up * radius * 1.5f;
			float maxDistance2 = maxDistance + radius * 1.5f;
			if (!GamePhysics.TraceRealm(GamePhysics.Realm.Server, new Ray(origin, Vector3.down), radius, out hitInfo, maxDistance2, layerMask, QueryTriggerInteraction.Ignore))
			{
				hitInfo.point = position;
				return false;
			}
			if (radius > 0f && hitInfo.distance <= 0f)
			{
				hitInfo.point = position;
			}
			return true;
		}
	}

	public bool IsPositionOnFavoredTerrain(Vector3 position)
	{
		using (TimeWarning.New("IsPositionOnFavoredTerrain"))
		{
			return IsPositionAtTopologyRequirement(position, preferedTopology) && IsPositionABiomeRequirement(position, preferedBiome);
		}
	}

	public bool IsPositionAtTopologyRequirement(Vector3 position, TerrainTopology.Enum topologyRequirement)
	{
		using (TimeWarning.New("IsPositionAtTopologyRequirement"))
		{
			if (TerrainMeta.TopologyMap == null)
			{
				return false;
			}
			TerrainTopology.Enum topology = (TerrainTopology.Enum)TerrainMeta.TopologyMap.GetTopology(position);
			if ((topologyRequirement & topology) == 0)
			{
				return false;
			}
			return true;
		}
	}

	public bool IsPositionABiomeRequirement(Vector3 position, TerrainBiome.Enum biomeRequirement)
	{
		using (TimeWarning.New("IsPositionABiomeRequirement"))
		{
			if (biomeRequirement == (TerrainBiome.Enum)0)
			{
				return true;
			}
			if (TerrainMeta.BiomeMap == null)
			{
				return false;
			}
			TerrainBiome.Enum biomeMaxType = (TerrainBiome.Enum)TerrainMeta.BiomeMap.GetBiomeMaxType(position);
			if ((biomeRequirement & biomeMaxType) == 0)
			{
				return false;
			}
			return true;
		}
	}

	public bool IsInWater(Vector3 position)
	{
		using (TimeWarning.New("IsInWater"))
		{
			if (WaterLevel.GetOverallWaterDepth(position, waves: false, volumes: false) >= 0.3f)
			{
				return true;
			}
			return false;
		}
	}

	public bool SamplePosition(Vector3 position, out Vector3 sample, float maxDistance)
	{
		using (TimeWarning.New("SamplePosition"))
		{
			sample = position;
			if (!NavMesh.SamplePosition(position, out var hit, maxDistance, agent.areaMask))
			{
				return false;
			}
			sample = hit.position;
			return hit.hit;
		}
	}

	public bool Raycast(Vector3 targetPosition, out NavMeshHit hitInfo)
	{
		using (TimeWarning.New("Raycast"))
		{
			return agent.Raycast(targetPosition, out hitInfo);
		}
	}

	public bool CalculatePathCustom(Vector3 destination, NavMeshPath path)
	{
		using (TimeWarning.New("CalculatePathCustom"))
		{
			return agent.CalculatePath(destination, path);
		}
	}
}
