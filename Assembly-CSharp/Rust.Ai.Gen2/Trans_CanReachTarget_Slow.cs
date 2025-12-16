using System;

namespace Rust.Ai.Gen2;

[Serializable]
internal class Trans_CanReachTarget_Slow : Rust.Ai.Gen2.FSMSlowTransitionBase
{
	protected override bool EvaluateAtInterval()
	{
		using (TimeWarning.New("Trans_CanReachTarget_Slow"))
		{
			if (!base.Senses.FindTargetPosition(out var targetPosition))
			{
				return false;
			}
			return base.Agent.CanReach(targetPosition);
		}
	}
}
