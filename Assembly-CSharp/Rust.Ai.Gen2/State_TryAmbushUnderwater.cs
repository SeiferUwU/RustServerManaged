using System;
using Facepunch;
using UnityEngine;

namespace Rust.Ai.Gen2;

[Serializable]
public class State_TryAmbushUnderwater : FSMStateBase
{
	[SerializeField]
	private Vector2 distanceRange = new Vector2(10f, 20f);

	[SerializeField]
	private float maxDistFromDivingPoint = 50f;

	private const float desiredDepth = 3f;

	private Vector3 divePosition;

	public override EFSMStateStatus OnStateEnter()
	{
		base.Agent.desiredSwimDepth.Value = 3f;
		divePosition = Owner.transform.position;
		return FindNewUnderwaterWaitingPosition();
	}

	public override EFSMStateStatus OnStateUpdate(float deltaTime)
	{
		if (!base.Agent.IsFollowingPath)
		{
			return FindNewUnderwaterWaitingPosition();
		}
		return base.OnStateUpdate(deltaTime);
	}

	public override void OnStateExit()
	{
		base.Agent.ResetPath();
		base.Agent.desiredSwimDepth.Reset();
		base.OnStateExit();
	}

	private EFSMStateStatus FindNewUnderwaterWaitingPosition()
	{
		using PooledList<Vector3> pooledList = Pool.Get<PooledList<Vector3>>();
		float radius = UnityEngine.Random.Range(distanceRange.x, distanceRange.y);
		Eqs.SamplePositionsInDonutShape(base.Agent.NavPosition, pooledList, radius);
		if (Vector3.Distance(divePosition, Owner.transform.position) > maxDistFromDivingPoint)
		{
			using Eqs.PooledScoreList pooledScoreList = Pool.Get<Eqs.PooledScoreList>();
			Vector3 normalized = (divePosition - Owner.transform.position).normalized;
			foreach (Vector3 item2 in pooledList)
			{
				float item = Vector3.Dot(normalized, (item2 - Owner.transform.position).normalized);
				pooledScoreList.Add((item2, item));
			}
			pooledScoreList.SortByScoreDesc(Owner);
			pooledScoreList.Reorder(pooledList);
		}
		else
		{
			pooledList.Shuffle((uint)Environment.TickCount);
		}
		foreach (Vector3 item3 in pooledList)
		{
			if (base.Agent.SamplePosition(item3, out var sample, 10f) && base.Agent.IsInWater(sample) && base.Agent.SetDestination(sample))
			{
				base.Agent.SetSpeed((!base.Agent.IsSwimming) ? LimitedTurnNavAgent.Speeds.Run : LimitedTurnNavAgent.Speeds.Sneak);
				return EFSMStateStatus.None;
			}
		}
		return EFSMStateStatus.Failure;
	}
}
