using System;
using Facepunch;
using UnityEngine;

namespace Rust.Ai.Gen2;

[Serializable]
public class State_Flee : FSMStateBase
{
	[SerializeField]
	public float desiredDistance = 50f;

	[SerializeField]
	public float distance = 20f;

	[SerializeField]
	private LimitedTurnNavAgent.Speeds speed = LimitedTurnNavAgent.Speeds.Sprint;

	[SerializeField]
	private int maxAttempts = 3;

	private int attempts;

	protected float startDistance;

	public override EFSMStateStatus OnStateEnter()
	{
		base.Blackboard.Remove("HitByFire");
		if (!base.Senses.FindTargetPosition(out var targetPosition))
		{
			return EFSMStateStatus.Success;
		}
		attempts = 0;
		base.Agent.SetSpeed(speed);
		base.Agent.shouldStopAtDestination = false;
		startDistance = Vector3.Distance(Owner.transform.position, targetPosition);
		return MoveAwayFromTarget();
	}

	public override void OnStateExit()
	{
		base.Agent.ResetPath();
		base.OnStateExit();
	}

	public override EFSMStateStatus OnStateUpdate(float deltaTime)
	{
		if (base.Agent.IsFollowingPath)
		{
			return base.OnStateUpdate(deltaTime);
		}
		if (!base.Senses.FindTargetPosition(out var targetPosition))
		{
			return EFSMStateStatus.Success;
		}
		if (Vector3.Distance(targetPosition, Owner.transform.position) > desiredDistance + startDistance)
		{
			return EFSMStateStatus.Success;
		}
		attempts++;
		if (attempts >= maxAttempts)
		{
			return EFSMStateStatus.Success;
		}
		return MoveAwayFromTarget();
	}

	protected virtual EFSMStateStatus MoveAwayFromTarget()
	{
		if (!base.Senses.FindTargetPosition(out var targetPosition))
		{
			return EFSMStateStatus.Success;
		}
		using PooledList<Vector3> pooledList = Pool.Get<PooledList<Vector3>>();
		Eqs.SamplePositionsInDonutShape(base.Agent.NavPosition, pooledList, distance);
		using Eqs.PooledScoreList pooledScoreList = Pool.Get<Eqs.PooledScoreList>();
		Vector3 lhs = (Owner.transform.position - targetPosition).NormalizeXZ();
		foreach (Vector3 item3 in pooledList)
		{
			float item = Vector3.Dot(lhs, (item3 - Owner.transform.position).normalized);
			pooledScoreList.Add((item3, item));
		}
		pooledScoreList.SortByScoreDesc(Owner);
		foreach (var item4 in pooledScoreList)
		{
			Vector3 item2 = item4.pos;
			if (base.Agent.SamplePosition(item2, out var sample, 10f) && (base.Agent.canSwim || !base.Agent.IsInWater(sample)) && base.Agent.SetDestination(sample))
			{
				return EFSMStateStatus.None;
			}
		}
		return EFSMStateStatus.Failure;
	}
}
