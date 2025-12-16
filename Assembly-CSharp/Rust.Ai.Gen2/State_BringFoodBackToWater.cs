using System;
using UnityEngine;

namespace Rust.Ai.Gen2;

[Serializable]
public class State_BringFoodBackToWater : State_GoBackToWater
{
	private BaseCorpse corpse;

	private Action _updateCorpsePositionAction;

	private Action UpdateCorpsePositionAction => OnStateFixedUpdate;

	public override EFSMStateStatus OnStateEnter()
	{
		if (!base.Senses.FindFood(out var food))
		{
			return EFSMStateStatus.Failure;
		}
		if (!(food is BaseCorpse baseCorpse))
		{
			return EFSMStateStatus.Failure;
		}
		if (!SingletonComponent<NpcFoodManager>.Instance.Remove(baseCorpse))
		{
			return EFSMStateStatus.Failure;
		}
		Owner.transform.forward = (baseCorpse.transform.position - Owner.transform.position).NormalizeXZ();
		corpse = baseCorpse;
		corpse.SetFlag(BaseEntity.Flags.Reserved3, b: true);
		Owner.InvokeRepeatingFixedTime(UpdateCorpsePositionAction);
		return base.OnStateEnter();
	}

	public override EFSMStateStatus OnStateUpdate(float deltaTime)
	{
		if (!corpse.IsValid() || corpse.IsDead())
		{
			return EFSMStateStatus.Failure;
		}
		return base.OnStateUpdate(deltaTime);
	}

	private void OnStateFixedUpdate()
	{
		if (corpse.IsValid() && !corpse.IsDead())
		{
			Rigidbody component = corpse.GetComponent<Rigidbody>();
			if ((object)component != null)
			{
				component.MovePosition(Owner.transform.position + Owner.transform.forward * 1.6f + Owner.transform.up * 0.6f);
				component.velocity = Vector3.zero;
				component.angularVelocity = Vector3.zero;
			}
		}
	}

	public override void OnStateExit()
	{
		base.OnStateExit();
		if (corpse.IsValid())
		{
			corpse.SetFlag(BaseEntity.Flags.Reserved3, b: false);
		}
		Owner.CancelInvokeFixedTime(UpdateCorpsePositionAction);
		corpse = null;
	}
}
