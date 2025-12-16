using System;

namespace Rust.Ai.Gen2;

[Serializable]
internal class Trans_IsTargetProtectedByMount : FSMTransitionBase
{
	protected override bool EvaluateInternal()
	{
		using (TimeWarning.New("Trans_IsTargetProtectedByMount"))
		{
			if (!base.Senses.FindTarget(out var target) || !target.ToNonNpcPlayer(out var player))
			{
				return false;
			}
			BaseMountable entAsT;
			return BaseNetworkableEx.Is<BaseMountable>(player.GetMounted(), out entAsT) && entAsT.ProtectsFromAnimals;
		}
	}
}
