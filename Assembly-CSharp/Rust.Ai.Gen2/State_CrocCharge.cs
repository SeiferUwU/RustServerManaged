using System;
using UnityEngine;

namespace Rust.Ai.Gen2;

[Serializable]
public class State_CrocCharge : State_MoveToTarget
{
	private const float maxChargeDuration = 6f;

	private float remainingChargeDuration;

	private Action _reallowChargingAction;

	private Action _surpriseAction;

	private Action _resetSurpriseAction;

	private double nextSurpriseTime;

	private Action ReallowChargingAction => ResetStamina;

	private Action SurpriseAction => Surprise;

	private Action ResetSurpriseAction => ResetSurprise;

	public override EFSMStateStatus OnStateEnter()
	{
		ResetStamina();
		ResetSurprise();
		return base.OnStateEnter();
	}

	private void ResetStamina()
	{
		remainingChargeDuration = 6f;
	}

	private void ResetSurprise()
	{
		float num = UnityEngine.Random.Range(3f, 6f);
		Owner.Invoke(SurpriseAction, num);
		Owner.Invoke(ResetSurpriseAction, num + UnityEngine.Random.Range(3f, 6f));
	}

	private void Surprise()
	{
		nextSurpriseTime = Time.timeAsDouble + (double)UnityEngine.Random.Range(3f, 6f);
	}

	public override EFSMStateStatus OnStateUpdate(float deltaTime)
	{
		if (Owner is BaseCombatEntity baseCombatEntity)
		{
			if (!base.Senses.FindTarget(out var target))
			{
				return EFSMStateStatus.Success;
			}
			base.Agent.acceleration.Value = accelerationOverride;
			float num = Mathx.RemapValClamped(baseCombatEntity.healthFraction, 1f, 0.3f, 0f, 1f);
			bool flag = false;
			BasePlayer player;
			if (base.Agent.IsSwimming)
			{
				flag = true;
				base.Agent.acceleration.Value = 2.5f;
				num = 1f;
			}
			else if (baseCombatEntity.lastAttackedTime > 0f && Time.time < baseCombatEntity.lastAttackedTime + 0.5f)
			{
				flag = true;
				num = 1f;
			}
			else if (Time.timeAsDouble > nextSurpriseTime && Time.timeAsDouble < nextSurpriseTime + 0.5)
			{
				num = 1f;
			}
			else if (base.Agent.RemainingDistance < 4f)
			{
				flag = true;
				num = 1f;
			}
			else if (target.ToNonNpcPlayer(out player) && player.modelState.sprinting && Vector3.Dot(player.estimatedVelocity.normalized, Owner.transform.forward) > 0.5f)
			{
				base.Agent.acceleration.Value = 2f;
				num = 1f;
			}
			if (!flag && num >= 1f)
			{
				float num2 = remainingChargeDuration;
				remainingChargeDuration -= deltaTime;
				if (num2 > 0f && remainingChargeDuration <= 0f)
				{
					Owner.Invoke(ReallowChargingAction, 6f);
				}
			}
			if (!flag && remainingChargeDuration <= 0f)
			{
				num = Mathf.Min(num, 0.3f);
			}
			base.Agent.SetSpeedRatio(num, speed, LimitedTurnNavAgent.Speeds.FullSprint);
			if (base.Senses.GetVisibilityStatus(target, out var status) && status.isInWaterCached)
			{
				base.Agent.desiredSwimDepth.Value = Mathf.Max(base.Agent.desiredSwimDepth.DefaultValue, status.lastWaterInfo.Value.currentDepth + 1f);
			}
		}
		return base.OnStateUpdate(deltaTime);
	}

	protected override bool GetMoveDestination(out Vector3 destination)
	{
		destination = default(Vector3);
		if (!base.Senses.FindTarget(out var target))
		{
			return false;
		}
		if (target.IsNonNpcPlayer() && base.Agent.canSwim && base.Senses.GetVisibilityStatus(target, out var status) && status.isInWaterCached)
		{
			destination = target.transform.position.WithY(status.lastWaterInfo.Value.terrainHeight);
			return true;
		}
		return base.GetMoveDestination(out destination);
	}
}
