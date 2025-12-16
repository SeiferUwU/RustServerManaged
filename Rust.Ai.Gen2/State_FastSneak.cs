using System;

namespace Rust.Ai.Gen2;

[Serializable]
public class State_FastSneak : State_CircleDynamic
{
	public override EFSMStateStatus OnStateEnter()
	{
		base.Agent.deceleration.Value = 10f;
		return base.OnStateEnter();
	}

	protected override void SetSpeed(BaseEntity target, float distToTarget, float normalizedDist)
	{
		if (!target.ToNonNpcPlayer(out var player))
		{
			base.SetSpeed(target, distToTarget, normalizedDist);
		}
		else if (distToTarget > 50f)
		{
			base.Agent.SetSpeed(8.25f);
		}
		else if (player.modelState.sprinting && distToTarget < 20f)
		{
			base.Agent.SetSpeed(6.875f);
		}
		else if (player.modelState.sprinting)
		{
			base.Agent.SetSpeed(8.25f);
		}
		else if (player.modelState.ducked || player.estimatedSpeed < 0.1f)
		{
			base.Agent.SetSpeed(1.7f);
		}
		else
		{
			base.Agent.SetSpeed(3.5f);
		}
	}
}
