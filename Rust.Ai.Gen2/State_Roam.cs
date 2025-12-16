using System;
using Facepunch;
using UnityEngine;

namespace Rust.Ai.Gen2;

[Serializable]
public class State_Roam : FSMStateBase
{
	[SerializeField]
	private Vector2 distanceRange = new Vector2(10f, 20f);

	[SerializeField]
	private float homeRadius = 50f;

	[SerializeField]
	private LimitedTurnNavAgent.Speeds minSpeed;

	[SerializeField]
	private LimitedTurnNavAgent.Speeds maxSpeed = LimitedTurnNavAgent.Speeds.Sprint;

	[SerializeField]
	protected bool favourWater;

	private Vector3? spawnPosition;

	public override EFSMStateStatus OnStateEnter()
	{
		Reset();
		if (!spawnPosition.HasValue)
		{
			spawnPosition = Owner.transform.position;
		}
		if (!TrySetRoamDestination())
		{
			return EFSMStateStatus.Failure;
		}
		return base.OnStateEnter();
	}

	private bool TrySetRoamDestination()
	{
		using PooledList<Vector3> pooledList = Pool.Get<PooledList<Vector3>>();
		float num = UnityEngine.Random.Range(distanceRange.x, distanceRange.y);
		Eqs.SamplePositionsInDonutShape(base.Agent.NavPosition, pooledList, num);
		bool flag = Vector3.Distance(spawnPosition.Value, Owner.transform.position) > homeRadius;
		using Eqs.PooledScoreList pooledScoreList = Pool.Get<Eqs.PooledScoreList>();
		Vector3 normalized = (spawnPosition.Value - Owner.transform.position).normalized;
		foreach (Vector3 item2 in pooledList)
		{
			float num2 = 0f;
			if (flag)
			{
				num2 += Mathx.RemapValClamped(Vector3.Dot(normalized, (item2 - Owner.transform.position).normalized), -1f, 1f, 0f, 1f);
				if (base.Agent.IsPositionOnFavoredTerrain(item2))
				{
					num2 += 0.25f;
				}
			}
			else
			{
				num2 += UnityEngine.Random.value;
				if (base.Agent.IsPositionOnFavoredTerrain(item2))
				{
					num2 += 10f;
				}
			}
			pooledScoreList.Add((item2, num2));
		}
		pooledScoreList.SortByScoreDesc(Owner);
		foreach (var item3 in pooledScoreList)
		{
			Vector3 item = item3.pos;
			if (base.Agent.SamplePosition(item, out var sample, 10f) && (base.Agent.canSwim || !base.Agent.IsInWater(sample)) && base.Agent.SetDestination(sample))
			{
				float ratio = Mathf.InverseLerp(0f, distanceRange.y, num);
				base.Agent.SetSpeedRatio(ratio, minSpeed, maxSpeed);
				return true;
			}
		}
		return false;
	}

	public override EFSMStateStatus OnStateUpdate(float deltaTime)
	{
		if (!base.Agent.IsFollowingPath)
		{
			return EFSMStateStatus.Success;
		}
		return base.OnStateUpdate(deltaTime);
	}

	public override void OnStateExit()
	{
		base.Agent.ResetPath();
		base.OnStateExit();
	}

	private void Reset()
	{
		base.Senses.ClearTarget();
		base.Blackboard.Clear();
		if (Owner is BaseCombatEntity { healthFraction: <1f, SecondsSinceAttacked: >120f } baseCombatEntity)
		{
			baseCombatEntity.SetHealth(Owner.MaxHealth());
		}
	}
}
