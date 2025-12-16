using System;
using Facepunch;
using UnityEngine;

namespace Rust.Ai.Gen2;

[Serializable]
public class State_FleeToHide : State_Flee
{
	public const string HitDuringChargeKey = "HitDuringCharge";

	private bool clockWise;

	public override EFSMStateStatus OnStateEnter()
	{
		base.Blackboard.Remove("HitDuringCharge");
		if (!base.Senses.FindTargetPosition(out var targetPosition))
		{
			return EFSMStateStatus.Success;
		}
		Vector3 rhs = (targetPosition - Owner.transform.position).NormalizeXZ();
		clockWise = Vector3.Dot(Owner.transform.right, rhs) > 0f;
		return base.OnStateEnter();
	}

	protected override EFSMStateStatus MoveAwayFromTarget()
	{
		if (!base.Senses.FindTargetPosition(out var targetPosition))
		{
			return EFSMStateStatus.Success;
		}
		float magnitude = (Owner.transform.position - targetPosition).magnitude;
		Vector3 vector = Owner.transform.forward;
		float num = 15f;
		if (magnitude > 6f)
		{
			vector = (Owner.transform.position - targetPosition).NormalizeXZ();
			num = 55f;
		}
		vector = Quaternion.AngleAxis(num * (clockWise ? 1f : (-1f)), Vector3.up) * vector;
		using PooledList<Vector3> pooledList = Pool.Get<PooledList<Vector3>>();
		Eqs.SamplePositionsInDonutShape(base.Agent.NavPosition, pooledList, distance);
		using Eqs.PooledScoreList pooledScoreList = Pool.Get<Eqs.PooledScoreList>();
		foreach (Vector3 item3 in pooledList)
		{
			float item = Vector3.Dot(vector, (item3 - Owner.transform.position).normalized);
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
