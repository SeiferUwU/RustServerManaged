using System;
using UnityEngine;
using UnityEngine.AI;

namespace Rust.Ai.Gen2;

[Serializable]
internal class Trans_CrocHasStraightPathToTarget : FSMTransitionBase
{
	protected override bool EvaluateInternal()
	{
		using (TimeWarning.New("Trans_CrocHasStraightPathToTarget"))
		{
			if (!base.Senses.FindTarget(out var target))
			{
				return false;
			}
			Vector3 targetPosition = target.transform.position;
			if (target.IsNonNpcPlayer() && base.Agent.canSwim && base.Senses.GetVisibilityStatus(target, out var status) && status.isInWaterCached)
			{
				targetPosition = target.transform.position.WithY(status.lastWaterInfo.Value.terrainHeight);
			}
			NavMeshHit hitInfo;
			return !base.Agent.Raycast(targetPosition, out hitInfo);
		}
	}
}
