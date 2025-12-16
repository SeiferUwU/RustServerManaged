using System;
using Facepunch;
using UnityEngine;

namespace Rust.Ai.Gen2;

[Serializable]
public class State_GoBackToWater : State_MoveToTarget
{
	private Vector3 nearestWaterPoint;

	public override EFSMStateStatus OnStateEnter()
	{
		if (base.Agent.IsSwimming)
		{
			return EFSMStateStatus.Success;
		}
		using (TimeWarning.New("State_GoBackToWater GetCoarseVectorToShore and GetHeight"))
		{
			(Vector3 shoreDir, float shoreDist) coarseVectorToShore = TerrainTexturing.Instance.GetCoarseVectorToShore(Owner.transform.position);
			Vector3 item = coarseVectorToShore.shoreDir;
			float item2 = coarseVectorToShore.shoreDist;
			Vector3 vector = item * item2;
			Vector3 vector2 = Owner.transform.position + vector.normalized * (vector.magnitude + 10f);
			vector2.y = TerrainMeta.HeightMap.GetHeight(vector2);
			using PooledList<Vector3> pooledList = Pool.Get<PooledList<Vector3>>();
			Eqs.SamplePositionsInDonutShape(vector2, pooledList);
			pooledList.Shuffle((uint)Environment.TickCount);
			nearestWaterPoint = vector2;
			foreach (Vector3 item3 in pooledList)
			{
				if (base.Agent.SamplePosition(item3, out var sample, 10f) && base.Agent.IsInWater(sample))
				{
					nearestWaterPoint = sample;
					break;
				}
			}
		}
		return base.OnStateEnter();
	}

	protected override bool GetMoveDestination(out Vector3 destination)
	{
		destination = nearestWaterPoint;
		return true;
	}
}
