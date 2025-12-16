using System;
using System.Collections.Generic;
using ConVar;
using Facepunch;
using Oxide.Core;
using ProtoBuf;
using UnityEngine;
using UnityEngine.Events;

namespace Rust.Ai.Gen2;

public class SenseComponent : EntityComponent<BaseEntity>
{
	[Serializable]
	public struct Cone
	{
		public float halfAngle;

		public float range;

		public Cone(float halfAngle = 80f, float range = 10f)
		{
			this.halfAngle = halfAngle;
			this.range = range;
		}
	}

	public class VisibilityStatus : Facepunch.Pool.IPooled
	{
		private BaseEntity baseEntity;

		private BaseEntity targetEntity;

		public Vector3 lastKnownPosition;

		private const float waterCheckInterval = 1f;

		private double? lastTimeInWaterUpdated;

		public float timeVisible { get; private set; }

		public float timeNotVisible { get; private set; }

		public bool isVisible => timeVisible > 0f;

		public float timeWatched { get; private set; }

		public float timeNotWatched { get; private set; }

		public float timeAimedAt { get; private set; }

		public float timeNotAimedAt { get; private set; }

		public WaterLevel.WaterInfo? lastWaterInfo { get; private set; }

		public bool isInWaterCached
		{
			get
			{
				if (!targetEntity.ToNonNpcPlayer(out var player))
				{
					return false;
				}
				if (!lastWaterInfo.HasValue || !lastTimeInWaterUpdated.HasValue || UnityEngine.Time.timeAsDouble - lastTimeInWaterUpdated > 1.0)
				{
					BaseMountable entAsT;
					Vector3 vector = (BaseNetworkableEx.Is<BaseMountable>(player.GetMounted(), out entAsT) ? (Vector3.down * 0.5f) : Vector3.zero);
					lastWaterInfo = WaterLevel.GetWaterInfo(targetEntity.transform.position + vector, waves: false, volumes: false);
					lastTimeInWaterUpdated = UnityEngine.Time.timeAsDouble;
				}
				return lastWaterInfo.Value.currentDepth >= 0.3f;
			}
		}

		private void Reset()
		{
			targetEntity = null;
			baseEntity = null;
			timeWatched = 0f;
			timeNotWatched = 100f;
			timeAimedAt = 0f;
			timeNotAimedAt = 100f;
			timeVisible = 0f;
			timeNotVisible = 100f;
			lastKnownPosition = Vector3.zero;
			lastWaterInfo = null;
			lastTimeInWaterUpdated = null;
		}

		public void EnterPool()
		{
			Reset();
		}

		public void LeavePool()
		{
			Reset();
		}

		public static VisibilityStatus GetFromPool(BaseEntity baseEntity, BaseEntity targetEntity, float deltaTime, Vector3? lastKnownPositionOverride = null)
		{
			VisibilityStatus visibilityStatus = Facepunch.Pool.Get<VisibilityStatus>();
			visibilityStatus.baseEntity = baseEntity;
			visibilityStatus.targetEntity = targetEntity;
			visibilityStatus.UpdateVisibility(isVisible: true, deltaTime, lastKnownPositionOverride);
			return visibilityStatus;
		}

		private bool CheckValid()
		{
			if (!baseEntity.IsValid() || !targetEntity.IsValid())
			{
				if (AI.logIssues)
				{
					Debug.LogError($"SenseComponent:UpdateVisibility NRE: {baseEntity} {targetEntity}");
				}
				return false;
			}
			return true;
		}

		public void UpdateVisibility(bool isVisible, float deltaTime, Vector3? lastKnownPositionOverride = null)
		{
			if (!CheckValid())
			{
				return;
			}
			if (lastKnownPositionOverride.HasValue)
			{
				lastKnownPosition = lastKnownPositionOverride.Value;
			}
			else if (isVisible)
			{
				lastKnownPosition = targetEntity.transform.position;
			}
			if (isVisible)
			{
				timeNotVisible = 0f;
				timeVisible += deltaTime;
				Vector3 lhs = targetEntity.transform.forward;
				Vector3 position = targetEntity.transform.position;
				if (targetEntity.ToNonNpcPlayer(out var player))
				{
					lhs = player.eyes.HeadForward();
					position = player.eyes.position;
				}
				float num = Mathf.Acos(Vector3.Dot(lhs, (baseEntity.transform.position - position).normalized)) * 57.29578f * 2f;
				bool num2 = num < AI.watchedAngle;
				if (num2)
				{
					timeNotWatched = 0f;
					timeWatched += deltaTime;
				}
				else
				{
					timeWatched = 0f;
					timeNotWatched += deltaTime;
				}
				if (num2 && player != null && player.modelState.aiming && num < AI.aimedAtAngle && !(player.GetHeldEntity() is BaseMelee { canScareAiWhenAimed: false }))
				{
					timeNotAimedAt = 0f;
					timeAimedAt += deltaTime;
				}
				else
				{
					timeAimedAt = 0f;
					timeNotAimedAt += deltaTime;
				}
			}
			else
			{
				timeVisible = 0f;
				timeNotVisible += deltaTime;
				timeWatched = 0f;
				timeNotWatched += deltaTime;
				timeAimedAt = 0f;
				timeNotAimedAt += deltaTime;
			}
		}
	}

	[SerializeField]
	private Vector3 LongRangeVisionRectangle = new Vector3(6f, 30f, 60f);

	[SerializeField]
	private Cone ShortRangeVisionCone = new Cone(100f, 30f);

	[SerializeField]
	private float touchDistance = 6f;

	[SerializeField]
	private float noiseRangeMultiplier = 1f;

	[SerializeField]
	private float hearingRange = 50f;

	[SerializeField]
	private NPCTeam team;

	[NonSerialized]
	public ResettableFloat timeToForgetSightings = new ResettableFloat(30f);

	private const float timeToForgetNoises = 5f;

	private static HashSet<BaseEntity> entitiesUpdatedThisFrame = new HashSet<BaseEntity>();

	[ServerVar]
	public static float minRefreshIntervalSeconds = 0.2f;

	[ServerVar]
	public static float maxRefreshIntervalSeconds = 1f;

	private double? _lastTickTime;

	private double nextRefreshTime;

	private double spawnTime;

	private Dictionary<BaseEntity, double> _alliesWeAreAwareOf = new Dictionary<BaseEntity, double>(3);

	private Dictionary<BaseEntity, VisibilityStatus> entitiesWeAreAwareOf = new Dictionary<BaseEntity, VisibilityStatus>(8);

	private static readonly Dictionary<NpcNoiseIntensity, float> noiseRadii = new Dictionary<NpcNoiseIntensity, float>
	{
		{
			NpcNoiseIntensity.None,
			0f
		},
		{
			NpcNoiseIntensity.Low,
			10f
		},
		{
			NpcNoiseIntensity.Medium,
			20f
		},
		{
			NpcNoiseIntensity.High,
			50f
		}
	};

	private NpcNoiseEvent _currentNoise;

	[SerializeField]
	private float foodDetectionRange = 30f;

	private BaseEntity _nearestFood;

	[SerializeField]
	private float fireDetectionRange = 20f;

	[NonSerialized]
	public UnityEvent onFireMelee = new UnityEvent();

	private BaseEntity _nearestFire;

	private double? lastMeleeTime;

	[SerializeField]
	private float TargetingCooldown = 5f;

	private BaseEntity _target;

	private const float npcDistPenaltyToFavorTargetingPlayers = 10f;

	private double? lastTargetTime;

	private LockState lockState = new LockState();

	public float RefreshInterval
	{
		get
		{
			if (!ShouldRefreshFast)
			{
				return maxRefreshIntervalSeconds;
			}
			return minRefreshIntervalSeconds;
		}
	}

	private double LastTickTime
	{
		get
		{
			double valueOrDefault = _lastTickTime.GetValueOrDefault();
			if (!_lastTickTime.HasValue)
			{
				valueOrDefault = UnityEngine.Time.timeAsDouble;
				_lastTickTime = valueOrDefault;
				return valueOrDefault;
			}
			return valueOrDefault;
		}
		set
		{
			_lastTickTime = value;
		}
	}

	public bool HasPlayerInVicinity { get; private set; }

	public bool ShouldRefreshFast
	{
		get
		{
			if (!HasPlayerInVicinity)
			{
				if (Target != null)
				{
					return Target.IsNonNpcPlayer();
				}
				return false;
			}
			return true;
		}
	}

	public NpcNoiseEvent currentNoise => _currentNoise;

	private BaseEntity Target
	{
		get
		{
			return _target;
		}
		set
		{
			if (base.baseEntity.isServer)
			{
				BaseEntity target = _target;
				_target = value;
				if (target != _target)
				{
					base.baseEntity.SendNetworkUpdate();
				}
			}
		}
	}

	private bool ChangedTargetRecently
	{
		get
		{
			if (lastTargetTime.HasValue)
			{
				return UnityEngine.Time.timeAsDouble - lastTargetTime.Value < (double)TargetingCooldown;
			}
			return true;
		}
	}

	public void GetInitialAllies(List<BaseEntity> allies)
	{
		using PooledList<BaseEntity> pooledList = Facepunch.Pool.Get<PooledList<BaseEntity>>();
		foreach (var (baseEntity2, num2) in _alliesWeAreAwareOf)
		{
			if (!baseEntity2.IsValid() || (baseEntity2 is BaseCombatEntity baseCombatEntity && baseCombatEntity.IsDead()))
			{
				pooledList.Add(baseEntity2);
			}
			else if (!(num2 - spawnTime > (double)(maxRefreshIntervalSeconds * 2f)))
			{
				allies.Add(baseEntity2);
			}
		}
		foreach (BaseEntity item in pooledList)
		{
			_alliesWeAreAwareOf.Remove(item);
		}
	}

	public Vector3? GetLKP(BaseEntity entity)
	{
		if (GetVisibilityStatus(entity, out var status))
		{
			return status.isVisible ? entity.transform.position : status.lastKnownPosition;
		}
		return null;
	}

	public bool GetVisibilityStatus(BaseEntity entity, out VisibilityStatus status)
	{
		status = null;
		if (!CanTarget(entity))
		{
			return false;
		}
		if (!entitiesWeAreAwareOf.TryGetValue(entity, out status))
		{
			return false;
		}
		return true;
	}

	public bool Forget(BaseEntity entity)
	{
		if (!entitiesWeAreAwareOf.TryGetValue(entity, out var value))
		{
			return false;
		}
		entitiesWeAreAwareOf.Remove(entity);
		Facepunch.Pool.Free(ref value);
		return true;
	}

	public bool IsVisible(BaseEntity entity)
	{
		if (!GetVisibilityStatus(entity, out var status))
		{
			return false;
		}
		return status.isVisible;
	}

	public void GetSeenEntities(List<BaseEntity> perceivedEntities)
	{
		using (TimeWarning.New("SenseComponent:GetSeenEntities"))
		{
			foreach (BaseEntity key in entitiesWeAreAwareOf.Keys)
			{
				if (IsVisible(key))
				{
					perceivedEntities.Add(key);
				}
			}
		}
	}

	public void GetOncePerceivedEntities(List<BaseEntity> perceivedEntities)
	{
		foreach (BaseEntity key in entitiesWeAreAwareOf.Keys)
		{
			if (GetVisibilityStatus(key, out var _))
			{
				perceivedEntities.Add(key);
			}
		}
	}

	private Matrix4x4 GetEyeTransform()
	{
		return Matrix4x4.TRS(base.baseEntity.CenterPoint(), base.baseEntity.transform.rotation, Vector3.one);
	}

	public override void InitShared()
	{
		base.InitShared();
		spawnTime = UnityEngine.Time.timeAsDouble;
	}

	public override void Hurt(HitInfo hitInfo)
	{
		BaseEntity initiator = hitInfo.Initiator;
		if (!CanTarget(initiator))
		{
			return;
		}
		Vector3 vector = initiator.transform.position + Quaternion.AngleAxis(UnityEngine.Random.Range(0f, 360f), Vector3.up) * Vector3.forward * 5f;
		if (entitiesWeAreAwareOf.TryGetValue(initiator, out var value))
		{
			if (!value.isVisible && value.timeNotVisible > 3f)
			{
				value.lastKnownPosition = vector;
			}
		}
		else
		{
			VisibilityStatus fromPool = VisibilityStatus.GetFromPool(base.baseEntity, initiator, 0.01f, vector);
			entitiesWeAreAwareOf.Add(initiator, fromPool);
		}
	}

	public void Tick()
	{
		using (TimeWarning.New("SenseComponent:Tick"))
		{
			double timeAsDouble = UnityEngine.Time.timeAsDouble;
			if (timeAsDouble < nextRefreshTime)
			{
				return;
			}
			float deltaTime = (float)(timeAsDouble - LastTickTime);
			LastTickTime = timeAsDouble;
			HasPlayerInVicinity = false;
			entitiesUpdatedThisFrame.Clear();
			using (TimeWarning.New("SenseComponent:Tick:ProcessEntities"))
			{
				using PooledList<BaseEntity> pooledList = Facepunch.Pool.Get<PooledList<BaseEntity>>();
				BaseEntity.Query.Server.GetPlayersAndBrainsInSphere(base.baseEntity.transform.position, LongRangeVisionRectangle.z, pooledList, BaseEntity.Query.DistanceCheckType.None);
				foreach (BaseEntity item in pooledList)
				{
					if (!(item == base.baseEntity))
					{
						if (item.IsNonNpcPlayer())
						{
							HasPlayerInVicinity = true;
						}
						if (InSameTeam(item) && !_alliesWeAreAwareOf.ContainsKey(item))
						{
							_alliesWeAreAwareOf.Add(item, timeAsDouble);
						}
						if (CanTarget(item))
						{
							UpdateEntityVisibility(item, deltaTime);
						}
					}
				}
			}
			using (TimeWarning.New("SenseComponent:Tick:RemoveEntities"))
			{
				using PooledList<BaseEntity> pooledList2 = Facepunch.Pool.Get<PooledList<BaseEntity>>();
				foreach (var (baseEntity2, visibilityStatus2) in entitiesWeAreAwareOf)
				{
					if (!CanTarget(baseEntity2))
					{
						pooledList2.Add(baseEntity2);
					}
					else if (!visibilityStatus2.isVisible && visibilityStatus2.timeNotVisible > timeToForgetSightings.Value)
					{
						pooledList2.Add(baseEntity2);
					}
					else if (!entitiesUpdatedThisFrame.Contains(baseEntity2) && visibilityStatus2.isVisible)
					{
						entitiesWeAreAwareOf[baseEntity2].UpdateVisibility(isVisible: false, deltaTime);
					}
				}
				entitiesUpdatedThisFrame.Clear();
				foreach (BaseEntity item2 in pooledList2)
				{
					if (Target.IsValid() && Target == item2)
					{
						ClearTarget(forget: false);
					}
					Forget(item2);
				}
			}
			TickHearing(deltaTime);
			TickFoodDetection(deltaTime);
			TickFireDetection(deltaTime);
			TickTargeting(deltaTime);
			nextRefreshTime = UnityEngine.Time.timeAsDouble + (double)RefreshInterval;
		}
	}

	private void GetModifiedSenses(BaseEntity entity, out float modTouchDistance, out float modHalfAngle, out float modShortVisionRange, out Vector3 modLongVisionRectangle)
	{
		modTouchDistance = touchDistance;
		modHalfAngle = ShortRangeVisionCone.halfAngle;
		modShortVisionRange = ShortRangeVisionCone.range;
		modLongVisionRectangle = LongRangeVisionRectangle;
		if (entity.ToNonNpcPlayer(out var player))
		{
			if (player.IsDucked())
			{
				modTouchDistance = base.baseEntity.bounds.extents.z * 1.5f;
				modHalfAngle = ShortRangeVisionCone.halfAngle * 0.85f;
				modShortVisionRange = ShortRangeVisionCone.range * 0.5f;
				modLongVisionRectangle = Vector3.Scale(LongRangeVisionRectangle, new Vector3(3f, 0.5f, 0.5f));
			}
			else if (player.IsRunning())
			{
				modTouchDistance = touchDistance * 3f;
				modHalfAngle = ShortRangeVisionCone.halfAngle;
				modShortVisionRange = ShortRangeVisionCone.range * 1.3f;
				modLongVisionRectangle = LongRangeVisionRectangle * 1.15f;
			}
		}
	}

	private bool IsInAnyRange(BaseEntity entity)
	{
		using (TimeWarning.New("IsInAnyRange"))
		{
			Vector3 position = GetEyeTransform().GetPosition();
			Vector3 vector = GetEyeTransform().rotation * Vector3.forward;
			Vector3 vector2 = entity.transform.position - position;
			float magnitude = vector2.magnitude;
			GetModifiedSenses(entity, out var modTouchDistance, out var modHalfAngle, out var modShortVisionRange, out var modLongVisionRectangle);
			if (magnitude < modTouchDistance)
			{
				return true;
			}
			if (Vector3.Angle(vector, vector2.normalized) < modHalfAngle)
			{
				if (magnitude < modShortVisionRange)
				{
					return true;
				}
				if (TOD_Sky.Instance.IsDay && magnitude < modLongVisionRectangle.z && Mathf.Abs(entity.transform.position.y - position.y) < modLongVisionRectangle.y * 0.5f && Vector3.Cross(vector, entity.transform.position - position).magnitude < modLongVisionRectangle.x * 0.5f)
				{
					return true;
				}
			}
			return false;
		}
	}

	private void UpdateEntityVisibility(BaseEntity entity, float deltaTime)
	{
		bool flag = IsInAnyRange(entity);
		if (flag && entity.ToNonNpcPlayer(out var player))
		{
			using (TimeWarning.New("SenseComponent:ProcessEntity:CanSee"))
			{
				Vector3 position = GetEyeTransform().GetPosition();
				flag = base.baseEntity.CanSee(position, player.eyes.position);
			}
		}
		if (entitiesWeAreAwareOf.TryGetValue(entity, out var value))
		{
			value.UpdateVisibility(flag, deltaTime);
			entitiesUpdatedThisFrame.Add(entity);
		}
		else if (flag)
		{
			VisibilityStatus fromPool = VisibilityStatus.GetFromPool(base.baseEntity, entity, deltaTime);
			entitiesWeAreAwareOf.Add(entity, fromPool);
			entitiesUpdatedThisFrame.Add(entity);
		}
	}

	public bool InSameTeam(BaseEntity other)
	{
		if (team != null && BaseNetworkableEx.Is<SenseComponent>(other.GetComponent<SenseComponent>(), out var entAsT) && team == entAsT.team)
		{
			return true;
		}
		return base.baseEntity.InSameNpcTeam(other);
	}

	private void TickHearing(float deltaTime)
	{
		using (TimeWarning.New("SenseComponent:TickHearing"))
		{
			if (_currentNoise != null)
			{
				Facepunch.Pool.Free(ref _currentNoise);
			}
			if (noiseRangeMultiplier <= 0f)
			{
				return;
			}
			using PooledList<NpcNoiseEvent> pooledList = Facepunch.Pool.Get<PooledList<NpcNoiseEvent>>();
			SingletonComponent<NpcNoiseManager>.Instance.GetNoisesAround(base.baseEntity.transform.position, hearingRange, pooledList);
			NpcNoiseEvent npcNoiseEvent = null;
			float? num = null;
			foreach (NpcNoiseEvent item in pooledList)
			{
				if (item.Initiator == base.baseEntity || !CanTarget(item.Initiator))
				{
					continue;
				}
				float num2 = (float)(UnityEngine.Time.timeAsDouble - item.EventTime);
				if (!(num2 > 5f) && (npcNoiseEvent == null || item.Intensity >= npcNoiseEvent.Intensity))
				{
					if (!noiseRadii.TryGetValue(item.Intensity, out var value))
					{
						Debug.LogError($"Unknown noise intensity: {item.Intensity}");
					}
					else if (!(Vector3.Distance(item.Position, base.baseEntity.transform.position) > Mathf.Min(value * noiseRangeMultiplier, hearingRange)) && (npcNoiseEvent == null || item.Intensity != npcNoiseEvent.Intensity || !(num2 > num)))
					{
						npcNoiseEvent = item;
						num = num2;
					}
				}
			}
			if (npcNoiseEvent != null)
			{
				_currentNoise = Facepunch.Pool.Get<NpcNoiseEvent>();
				_currentNoise.Initiator = npcNoiseEvent.Initiator;
				_currentNoise.Position = npcNoiseEvent.Position;
				_currentNoise.Intensity = npcNoiseEvent.Intensity;
				if (!FindTarget(out var _))
				{
					TrySetTarget(npcNoiseEvent.Initiator);
				}
			}
		}
	}

	public bool ConsumeCurrentNoise()
	{
		if (_currentNoise == null)
		{
			return false;
		}
		Facepunch.Pool.Free(ref _currentNoise);
		return true;
	}

	public bool FindFood(out BaseEntity food)
	{
		if (!_nearestFood.IsValid() || _nearestFood.IsDestroyed || !SingletonComponent<NpcFoodManager>.Instance.Contains(_nearestFood))
		{
			food = null;
			return false;
		}
		food = _nearestFood;
		return true;
	}

	private void TickFoodDetection(float deltaTime)
	{
		using (TimeWarning.New("SenseComponent:TickFoodDetection"))
		{
			_nearestFood = null;
			if (foodDetectionRange <= 0f)
			{
				return;
			}
			float num = foodDetectionRange * foodDetectionRange;
			float num2 = float.MaxValue;
			using PooledList<BaseEntity> pooledList = Facepunch.Pool.Get<PooledList<BaseEntity>>();
			SingletonComponent<NpcFoodManager>.Instance.GetFoodAround(base.baseEntity.transform.position, foodDetectionRange, pooledList);
			LimitedTurnNavAgent component = base.baseEntity.GetComponent<LimitedTurnNavAgent>();
			foreach (BaseEntity item in pooledList)
			{
				if (!NpcFoodManager.IsFoodImmobile(item) || (item is BaseCorpse baseCorpse && BaseNetworkableEx.Is<HeadDispenser>(baseCorpse.GetComponent<HeadDispenser>(), out var entAsT) && BaseNetworkableEx.Is<BaseEntity>(entAsT.SourceEntity.GetEntity(), out var entAsT2) && entAsT2.InSameNpcTeam(base.baseEntity)))
				{
					continue;
				}
				if (!component.IsPositionOnNavmesh(item.transform.position, out var sample))
				{
					SingletonComponent<NpcFoodManager>.Instance.Remove(item);
					continue;
				}
				sample = item.transform.position - base.baseEntity.transform.position;
				float sqrMagnitude = sample.sqrMagnitude;
				if (sqrMagnitude < num2 && sqrMagnitude < num)
				{
					_nearestFood = item;
					num2 = sqrMagnitude;
				}
			}
		}
	}

	public bool FindFire(out BaseEntity fire)
	{
		if (!_nearestFire.IsValid() || _nearestFire.IsDestroyed || !NpcFireManager.IsOnFire(_nearestFire))
		{
			_nearestFire = null;
		}
		fire = _nearestFire;
		return fire != null;
	}

	private void TickFireDetection(float deltaTime)
	{
		using (TimeWarning.New("SenseComponent:TickFireDetection"))
		{
			if (fireDetectionRange <= 0f)
			{
				return;
			}
			if (Target != null && SingletonComponent<NpcFireManager>.Instance.DidMeleeWithFireRecently(base.baseEntity, Target, out var meleeTime) && (!lastMeleeTime.HasValue || meleeTime != lastMeleeTime.Value))
			{
				lastMeleeTime = meleeTime;
				onFireMelee.Invoke();
			}
			using PooledList<BaseEntity> pooledList = Facepunch.Pool.Get<PooledList<BaseEntity>>();
			SingletonComponent<NpcFireManager>.Instance.GetFiresAround(base.baseEntity.transform.position, fireDetectionRange, pooledList);
			BaseEntity baseEntity = null;
			float num = fireDetectionRange * fireDetectionRange;
			float num2 = float.MaxValue;
			foreach (BaseEntity item in pooledList)
			{
				float sqrMagnitude = (item.transform.position - base.baseEntity.transform.position).sqrMagnitude;
				if (sqrMagnitude < num2 && sqrMagnitude < num)
				{
					baseEntity = item;
					num2 = sqrMagnitude;
				}
			}
			if (baseEntity != null)
			{
				_nearestFire = baseEntity;
			}
		}
	}

	public LockState.LockHandle LockCurrentTarget()
	{
		return lockState.AddLock();
	}

	public bool UnlockTarget(ref LockState.LockHandle handle)
	{
		return lockState.RemoveLock(ref handle);
	}

	public bool CanTarget(BaseEntity entity)
	{
		if (!entity.IsValid())
		{
			return false;
		}
		if (entity.IsTransferProtected())
		{
			return false;
		}
		if (entity.IsDestroyed)
		{
			return false;
		}
		if (!entity.IsNonNpcPlayer() && !entity.IsNpc)
		{
			return false;
		}
		if (entity.IsNpcPlayer())
		{
			return false;
		}
		if (entity is BaseCombatEntity baseCombatEntity && baseCombatEntity.IsDead())
		{
			return false;
		}
		if (InSameTeam(entity))
		{
			return false;
		}
		if (entity is BasePlayer item)
		{
			if (AI.ignoreplayers)
			{
				return false;
			}
			if (SimpleAIMemory.PlayerIgnoreList.Contains(item))
			{
				return false;
			}
		}
		object obj = Interface.CallHook("IOnNpcTarget", this, entity);
		if (obj is bool)
		{
			return (bool)obj;
		}
		return true;
	}

	public bool FindTarget(out BaseEntity target)
	{
		if (!CanTarget(Target))
		{
			ClearTarget();
			target = null;
			return false;
		}
		target = Target;
		return target != null;
	}

	public bool FindTargetPosition(out Vector3 targetPosition)
	{
		if (!FindTarget(out var target))
		{
			targetPosition = Vector3.zero;
			return false;
		}
		targetPosition = target.transform.position;
		return true;
	}

	public bool TrySetTarget(BaseEntity newTarget, bool bypassCooldown = true)
	{
		if (lockState.IsLocked)
		{
			return false;
		}
		if (newTarget == null)
		{
			ClearTarget();
			return true;
		}
		if (newTarget == Target)
		{
			return true;
		}
		if (!CanTarget(newTarget))
		{
			return false;
		}
		if (Target != null && !bypassCooldown && ChangedTargetRecently)
		{
			return false;
		}
		lastTargetTime = UnityEngine.Time.timeAsDouble;
		Target = newTarget;
		return true;
	}

	public void ClearTarget(bool forget = true)
	{
		if (Target.IsValid())
		{
			if (forget)
			{
				Forget(Target);
			}
			lastTargetTime = null;
			Target = null;
		}
	}

	private void TickTargeting(float deltaTime)
	{
		using (TimeWarning.New("SenseComponent:TickTargeting"))
		{
			if (Target != null && !CanTarget(Target))
			{
				ClearTarget();
			}
			if (Target != null && ChangedTargetRecently)
			{
				return;
			}
			using PooledList<BaseEntity> pooledList = Facepunch.Pool.Get<PooledList<BaseEntity>>();
			GetOncePerceivedEntities(pooledList);
			if (pooledList.Count == 0)
			{
				return;
			}
			BaseEntity baseEntity = null;
			float num = float.MaxValue;
			foreach (BaseEntity item in pooledList)
			{
				if (CanTarget(item))
				{
					float num2 = base.baseEntity.SqrDistance(item);
					if (item.IsNpc)
					{
						num2 += 100f;
					}
					if (num2 < num)
					{
						num = num2;
						baseEntity = item;
					}
				}
			}
			if (baseEntity != null)
			{
				TrySetTarget(baseEntity, bypassCooldown: false);
			}
		}
	}

	public override void SaveComponent(BaseNetworkable.SaveInfo info)
	{
		base.SaveComponent(info);
		if (base.baseEntity.isServer)
		{
			info.msg.npcSensesState = Facepunch.Pool.Get<NPCSensesState>();
			if (Target != null)
			{
				info.msg.npcSensesState.targetEntityId = Target.net.ID;
			}
		}
	}

	public override void LoadComponent(BaseNetworkable.LoadInfo info)
	{
		NPCSensesState npcSensesState = info.msg.npcSensesState;
		if (npcSensesState != null)
		{
			if (npcSensesState.targetEntityId.IsValid)
			{
				if (base.baseEntity.isServer)
				{
					_target = BaseNetworkable.serverEntities.Find(npcSensesState.targetEntityId) as BaseEntity;
				}
			}
			else
			{
				_target = null;
			}
		}
		base.LoadComponent(info);
	}
}
