using System;
using ConVar;
using Facepunch;
using UnityEngine;

namespace Rust.Ai.Gen2;

[Serializable]
public class State_Dead : FSMStateBase, IParametrized<HitInfo>
{
	[SerializeField]
	private string deathStatName;

	[SerializeField]
	private GameObjectRef CorpsePrefab;

	[SerializeField]
	private RootMotionData staticDeathAnim;

	[SerializeField]
	private RootMotionData forwardMotionDeathAnim;

	[SerializeField]
	private float ragdollWhenAnimRemainingTimeIsBelow = 0.5f;

	private HitInfo HitInfo;

	private RootMotionPlayer.PlayServerState animState;

	private Action _startRagdollAction;

	private Action StartRagdollAction => StartRagdoll;

	public void SetParameter(HitInfo parameter)
	{
		if (HitInfo != null)
		{
			Facepunch.Pool.Free(ref HitInfo);
		}
		if (parameter == null)
		{
			Debug.LogWarning("No parameter set for death state");
		}
		HitInfo = Facepunch.Pool.Get<HitInfo>();
		HitInfo.CopyFrom(parameter);
	}

	public override EFSMStateStatus OnStateEnter()
	{
		if (HitInfo != null && HitInfo.InitiatorPlayer != null)
		{
			BasePlayer initiatorPlayer = HitInfo.InitiatorPlayer;
			initiatorPlayer.GiveAchievement("KILL_ANIMAL");
			if (!string.IsNullOrEmpty(deathStatName))
			{
				initiatorPlayer.stats.Add(deathStatName, 1, (Stats)5);
				initiatorPlayer.stats.Save();
			}
			if (Owner is BaseCombatEntity killed)
			{
				initiatorPlayer.LifeStoryKill(killed);
			}
		}
		if (!CorpsePrefab.isValid)
		{
			Owner.Kill();
			return base.OnStateEnter();
		}
		if (forwardMotionDeathAnim != null && base.Agent.IsSpeedGTE(LimitedTurnNavAgent.Speeds.Run))
		{
			animState = base.AnimPlayer.PlayServerAndTakeFromPool(forwardMotionDeathAnim);
			float num = Mathf.Max(0f, forwardMotionDeathAnim.inPlaceAnimation.length - ragdollWhenAnimRemainingTimeIsBelow);
			Owner.Invoke(StartRagdollAction, num + AI.defaultInterpolationDelay);
		}
		else if (staticDeathAnim != null && HitInfo != null && Vector3.Dot(HitInfo.attackNormal, Owner.transform.forward) < 0f)
		{
			animState = base.AnimPlayer.PlayServerAndTakeFromPool(staticDeathAnim);
			float num2 = Mathf.Max(0f, staticDeathAnim.inPlaceAnimation.length - ragdollWhenAnimRemainingTimeIsBelow);
			Owner.Invoke(StartRagdollAction, num2 + AI.defaultInterpolationDelay);
		}
		else
		{
			StartRagdoll();
		}
		return base.OnStateEnter();
	}

	private void StartRagdoll()
	{
		BaseCorpse baseCorpse = Owner.DropCorpse(CorpsePrefab.resourcePath);
		if ((bool)baseCorpse)
		{
			baseCorpse.Spawn();
			baseCorpse.TakeChildren(Owner);
		}
		Owner.Invoke(Owner.KillMessage, 0.5f);
	}

	public override void OnStateExit()
	{
		base.AnimPlayer.StopServerAndReturnToPool(ref animState);
		Facepunch.Pool.Free(ref HitInfo);
		base.OnStateExit();
	}
}
