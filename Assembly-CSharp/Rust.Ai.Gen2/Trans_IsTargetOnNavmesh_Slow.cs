using System;
using UnityEngine;

namespace Rust.Ai.Gen2;

[Serializable]
internal class Trans_IsTargetOnNavmesh_Slow : Rust.Ai.Gen2.FSMSlowTransitionBase
{
	protected override bool EvaluateAtInterval()
	{
		using (TimeWarning.New("Trans_IsTargetOnNavmesh_Slow"))
		{
			if (!base.Senses.FindTargetPosition(out var targetPosition))
			{
				return false;
			}
			Vector3 sample;
			return base.Agent.SamplePosition(targetPosition, out sample, 2f);
		}
	}
}
