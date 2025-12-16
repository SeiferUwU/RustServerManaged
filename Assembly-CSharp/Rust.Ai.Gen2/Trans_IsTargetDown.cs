using System;

namespace Rust.Ai.Gen2;

[Serializable]
public class Trans_IsTargetDown : FSMTransitionBase
{
	protected override bool EvaluateInternal()
	{
		if (!base.Senses.FindTarget(out var target))
		{
			return false;
		}
		if (!target.ToNonNpcPlayer(out var player))
		{
			return false;
		}
		return player.IsWounded();
	}
}
