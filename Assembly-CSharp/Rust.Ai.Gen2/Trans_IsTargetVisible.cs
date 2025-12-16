using System;

namespace Rust.Ai.Gen2;

[Serializable]
public class Trans_IsTargetVisible : FSMTransitionBase
{
	protected override bool EvaluateInternal()
	{
		if (!base.Senses.FindTarget(out var target))
		{
			return false;
		}
		if (!base.Senses.GetVisibilityStatus(target, out var status))
		{
			return false;
		}
		return status.isVisible;
	}
}
