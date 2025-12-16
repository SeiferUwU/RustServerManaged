using System;

namespace Rust.Ai.Gen2;

[Serializable]
internal class Trans_IsSwimming : FSMTransitionBase
{
	protected override bool EvaluateInternal()
	{
		using (TimeWarning.New("Trans_IsSwimming"))
		{
			return base.Agent.canSwim && base.Agent.IsSwimming;
		}
	}
}
