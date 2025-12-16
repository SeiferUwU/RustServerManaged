using System;

namespace Rust.Ai.Gen2;

[Serializable]
internal class Trans_IsInWaterSlow : Rust.Ai.Gen2.FSMSlowTransitionBase
{
	protected override bool EvaluateAtInterval()
	{
		using (TimeWarning.New("Trans_IsInWaterSlow"))
		{
			if (base.Agent.canSwim)
			{
				return base.Agent.IsSwimming;
			}
			return WaterLevel.GetWaterDepth(Owner.transform.position, waves: false, volumes: false) >= 0.3f;
		}
	}
}
