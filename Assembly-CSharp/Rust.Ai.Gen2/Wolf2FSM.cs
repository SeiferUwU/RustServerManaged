using UnityEngine;

namespace Rust.Ai.Gen2;

public class Wolf2FSM : FSMComponent
{
	public State_PlayRandomAnimation randomIdle = new State_PlayRandomAnimation();

	public State_Roam roam = new State_Roam();

	public State_Howl howl = new State_Howl();

	public State_CircleDynamic approach = new State_CircleDynamic();

	public State_Bark bark = new State_Bark();

	public State_Growl growlFire = new State_Growl();

	public State_ApproachFire approachFire = new State_ApproachFire();

	public State_FleeFire fleeFire = new State_FleeFire();

	public State_MoveToTarget charge = new State_MoveToTarget();

	public State_Attack attack = new State_Attack();

	public State_PlayAnimationRM leapAway = new State_PlayAnimationRM();

	public State_Circle reacCircle = new State_Circle();

	public State_CircleDynamic fastApproach = new State_CircleDynamic();

	public State_WolfHurt hurt = new State_WolfHurt();

	public State_Intimidated intimidated = new State_Intimidated();

	public State_Flee flee = new State_Flee();

	public State_Flee fleeForHowl = new State_Flee();

	public State_Dead dead = new State_Dead();

	public State_ApproachFood approachFood = new State_ApproachFood();

	public State_EatFood eatFood = new State_EatFood();

	public State_PlayAnimationRM growlFood = new State_PlayAnimationRM();

	public State_PlayAnimLoop sleep = new State_PlayAnimLoop();

	public State_AttackUnreachable attackUnreachable = new State_AttackUnreachable();

	private Trans_Triggerable_HitInfo HurtTrans;

	private Trans_Triggerable_HitInfo DeathTrans;

	private Trans_Triggerable<BaseEntity> AllyGotHurtNearby;

	private Trans_Triggerable<BaseEntity> HowlTrans;

	private Trans_Triggerable<BaseEntity> BarkTrans;

	public override void InitShared()
	{
		if (base.baseEntity.isServer)
		{
			State_Nothing state_Nothing = new State_Nothing
			{
				Name = "WaitForNavMesh"
			};
			State_Circle state_Circle = new State_Circle
			{
				radius = 2f,
				speed = LimitedTurnNavAgent.Speeds.Sprint,
				Name = "Combo circle"
			};
			State_MoveToTarget state_MoveToTarget = new State_MoveToTarget
			{
				speed = LimitedTurnNavAgent.Speeds.Walk,
				decelerationOverride = 6f,
				Name = "Step forward"
			};
			State_MoveToLastReachablePointNearTarget state_MoveToLastReachablePointNearTarget = new State_MoveToLastReachablePointNearTarget
			{
				speed = LimitedTurnNavAgent.Speeds.Sprint,
				succeedWhenDestinationIsReached = true,
				Name = "Go to last destination"
			};
			FSMStateBase fSMStateBase = leapAway.Clone();
			fSMStateBase.Name = "Leap away unreachable";
			State_Flee state_Flee = new State_Flee
			{
				distance = 8f,
				desiredDistance = 16f,
				Name = "Flee fire after attack"
			};
			FSMStateBase fSMStateBase2 = state_Circle.Clone();
			fSMStateBase2.Name = "Circle short fire";
			FSMStateBase fSMStateBase3 = charge.Clone();
			fSMStateBase3.Name = "Charge fire";
			FSMStateBase fSMStateBase4 = attack.Clone();
			fSMStateBase4.Name = "Attack fire";
			DeathTrans = new Trans_Triggerable_HitInfo();
			HurtTrans = new Trans_Triggerable_HitInfo();
			Trans_Triggerable PathFailedTrans = new Trans_Triggerable();
			base.baseEntity.GetComponent<LimitedTurnNavAgent>().onPathFailed.AddListener(delegate
			{
				PathFailedTrans.Trigger();
			});
			Trans_Triggerable FireMeleeTrans = new Trans_Triggerable();
			base.baseEntity.GetComponent<SenseComponent>().onFireMelee.AddListener(delegate
			{
				FireMeleeTrans.Trigger();
			});
			Trans_Triggerable EncounterEndTrans = new Trans_Triggerable();
			base.baseEntity.GetComponent<NPCEncounterTimer>().onShouldGiveUp.AddListener(delegate
			{
				EncounterEndTrans.Trigger();
			});
			BarkTrans = new Trans_Triggerable<BaseEntity>();
			AllyGotHurtNearby = new Trans_Triggerable<BaseEntity>();
			HowlTrans = new Trans_Triggerable<BaseEntity>();
			State_Nothing state_Nothing2 = new State_Nothing();
			state_Nothing2.Name = "Root";
			State_Nothing state_Nothing3 = new State_Nothing
			{
				Name = "Alive"
			};
			State_Nothing state_Nothing4 = new State_Nothing
			{
				Name = "OnNavmesh"
			};
			State_Nothing state_Nothing5 = new State_Nothing
			{
				Name = "Food"
			};
			State_Nothing state_Nothing6 = new State_Nothing
			{
				Name = "Roaming"
			};
			State_Nothing state_Nothing7 = new State_Nothing
			{
				Name = "Has target"
			};
			State_Nothing state_Nothing8 = new State_Nothing
			{
				Name = "Not hurt"
			};
			State_Nothing state_Nothing9 = new State_Nothing
			{
				Name = "No fire"
			};
			State_Nothing state_Nothing10 = new State_Nothing
			{
				Name = "Reachable"
			};
			State_Nothing state_Nothing11 = new State_Nothing
			{
				Name = "Unreachable"
			};
			State_Nothing state_Nothing12 = new State_Nothing
			{
				Name = "Fire"
			};
			State_Nothing state_Nothing13 = new State_Nothing
			{
				Name = "Fire melee reac"
			};
			State_Nothing state_Nothing14 = new State_Nothing
			{
				Name = "Ready to help"
			};
			State_Nothing state_Nothing15 = new State_Nothing
			{
				Name = "Fire entry"
			};
			State_Nothing state_Nothing16 = new State_Nothing
			{
				Name = "Combat entry"
			};
			State_Nothing state_Nothing17 = new State_Nothing
			{
				Name = "Random post idle wait"
			};
			state_Nothing2.AddChildren(state_Nothing3.AddTickTransition(dead, DeathTrans).AddChildren(state_Nothing.AddTickTransition(roam, new Rust.Ai.Gen2.Trans_IsNavmeshReady()), state_Nothing4.AddTickTransition(state_Nothing, new Rust.Ai.Gen2.Trans_IsNavmeshReady
			{
				Inverted = true
			}).AddChildren(state_Nothing8.AddTickTransition(hurt, HurtTrans).AddChildren(state_Nothing6.AddTickTransition(dead, PathFailedTrans).AddTickTransition(approach, HowlTrans).AddTickTransition(state_Nothing16, new Rust.Ai.Gen2.Trans_HasTarget())
				.AddTickTransition(approachFood, new Rust.Ai.Gen2.Trans_SeesFood())
				.AddChildren(roam.AddEndTransition(sleep, new Trans_RandomChance
				{
					Chance = 0.25f
				}).AddEndTransition(randomIdle), sleep.AddEndTransition(roam), randomIdle.AddEndTransition(state_Nothing17), state_Nothing17.AddTickTransition(roam, new Trans_ElapsedTimeRandomized
				{
					MinDuration = 0.0,
					MaxDuration = 3.0
				})), state_Nothing7.AddTickTransition(roam, new Rust.Ai.Gen2.Trans_HasTarget
			{
				Inverted = true
			}).AddTickTransition(flee, EncounterEndTrans).AddTickTransition(flee, new Rust.Ai.Gen2.Trans_TargetIsInSafeZone())
				.AddChildren(state_Nothing10.AddTickTransition(flee, new Rust.Ai.Gen2.Trans_IsInWaterSlow() | new Rust.Ai.Gen2.Trans_IsTargetInWater()).AddTickTransition(state_MoveToLastReachablePointNearTarget, PathFailedTrans).AddChildren(state_Nothing9.AddTickTransition(state_Nothing15, new Rust.Ai.Gen2.Trans_TargetIsNearFire
				{
					onlySeeFireWhenClose = true
				}).AddChildren(state_Nothing16.AddTickTransition(howl, new Rust.Ai.Gen2.Trans_HasBlackboardBool
				{
					Key = "WolfNearbyAlreadyHowled",
					Inverted = true
				}).AddTickTransition(approach, new Trans_AlwaysValid()), state_Nothing14.AddTickTransition(flee, new Trans_And
				{
					AllyGotHurtNearby,
					new Rust.Ai.Gen2.Trans_TargetIsNearFire()
				}).AddTickTransition(fastApproach, AllyGotHurtNearby).AddTickTransition(charge, BarkTrans)
					.AddChildren(howl.AddTickTransition(approach, new Trans_TargetInRange
					{
						Range = 12f
					}).AddEndTransition(approach), approach.AddTickBranchingTrans(charge, new Trans_TargetInRange
					{
						Range = 12f
					}, bark, new Rust.Ai.Gen2.Trans_HasBlackboardBool
					{
						Key = "WolfNearbyAlreadyBarked",
						Inverted = true
					}).AddTickTransition(approachFood, new Trans_And
					{
						new Rust.Ai.Gen2.Trans_SeesFood(),
						new Rust.Ai.Gen2.Trans_HasBlackboardBool
						{
							Key = "TriedToApproachUnreachableFood",
							Inverted = true
						}
					})), bark.AddTickTransition(charge, new Trans_TargetInRange
				{
					Range = 2f
				}).AddEndTransition(charge), charge.AddTickTransition(fastApproach, AllyGotHurtNearby).AddTickTransition(attack, new Trans_TargetInRange
				{
					Range = 2f
				}).AddTickTransition(approach, new Trans_ElapsedTime
				{
					Duration = 5.0
				})
					.AddFailureTransition(state_MoveToLastReachablePointNearTarget), attack.AddEndTransition(leapAway, new Trans_TargetInFront
				{
					Angle = 120f,
					Inverted = true
				}).AddEndTransition(state_Circle), leapAway.AddEndTransition(state_Circle), state_Circle.AddTickTransition(charge, new Trans_ElapsedTimeRandomized
				{
					MinDuration = 0.75,
					MaxDuration = 1.5
				}).AddEndTransition(charge), reacCircle.AddTickTransition(reacCircle, AllyGotHurtNearby).AddTickTransition(charge, new Trans_ElapsedTimeRandomized
				{
					MinDuration = 2.0,
					MaxDuration = 4.0
				}).AddEndTransition(charge), fastApproach.AddTickTransition(reacCircle, new Trans_TargetInRange
				{
					Range = reacCircle.radius + 5f
				}).AddTickTransition(charge, BarkTrans), fleeForHowl.AddEndTransition(howl)), state_Nothing12.AddTickTransition(flee, PathFailedTrans).AddTickTransition(flee, AllyGotHurtNearby).AddChildren(state_Nothing15.AddTickTransition(intimidated, new Trans_TargetInRange
				{
					Range = 12f
				}).AddTickTransition(growlFire, new Rust.Ai.Gen2.Trans_HasBlackboardBool
				{
					Key = "AlreadyGrowled",
					Inverted = true
				}).AddTickTransition(approachFire, new Trans_AlwaysValid()), state_Nothing13.AddTickBranchingTrans(intimidated, FireMeleeTrans, growlFire, new Trans_RandomChance
				{
					Chance = 0.75f
				}).AddChildren(approachFire.AddTickTransition(fSMStateBase2, new Trans_TargetInRange
				{
					Range = 5f
				}).AddTickTransition(fastApproach, new Rust.Ai.Gen2.Trans_TargetIsNearFire
				{
					Inverted = true
				}).AddTickTransition(fastApproach, new Trans_TargetInRange
				{
					Range = 21f,
					Inverted = true
				}), state_MoveToTarget.AddTickTransition(fSMStateBase2, new Trans_TargetInRange
				{
					Range = 5f
				}).AddTickTransition(approachFire, new Trans_ElapsedTimeRandomized
				{
					MinDuration = 1.0,
					MaxDuration = 3.0
				})), growlFire.AddTickTransition(intimidated, FireMeleeTrans).AddTickTransition(fSMStateBase2, new Trans_TargetInRange
				{
					Range = 5f
				}).AddEndTransition(approachFire), fSMStateBase2.AddTickTransition(fSMStateBase3, new Trans_ElapsedTimeRandomized
				{
					MinDuration = 0.5,
					MaxDuration = 1.25
				}).AddEndTransition(fSMStateBase3), fSMStateBase3.AddTickTransition(fSMStateBase4, new Trans_TargetInRange
				{
					Range = 2f
				}), fSMStateBase4.AddEndTransition(state_Flee), intimidated.AddEndTransition(fleeFire), fleeFire.AddEndTransition(state_MoveToTarget), state_Flee.AddEndTransition(state_MoveToTarget))), state_Nothing11.AddChildren(state_MoveToLastReachablePointNearTarget.AddFailureTransition(flee).AddTickTransition(flee, FireMeleeTrans).AddTickTransition(charge, new Rust.Ai.Gen2.Trans_CanReachTarget_Slow())
					.AddEndTransition(charge, new Rust.Ai.Gen2.Trans_CanReachTarget_Slow())
					.AddEndTransition(attackUnreachable)
					.AddEndTransition(flee), fSMStateBase.AddEndTransition(state_MoveToLastReachablePointNearTarget)), flee.AddTickTransition(dead, PathFailedTrans).AddEndTransition(fastApproach, new Trans_TargetInRange
				{
					Range = flee.desiredDistance
				}).AddEndTransition(roam)), state_Nothing5.AddTickTransition(state_Nothing15, new Rust.Ai.Gen2.Trans_TargetIsNearFire
			{
				onlySeeFireWhenClose = true
			}).AddTickTransition(approach, HowlTrans).AddTickTransition(fastApproach, AllyGotHurtNearby)
				.AddTickTransition(charge, BarkTrans)
				.AddTickTransition(roam, new Rust.Ai.Gen2.Trans_SeesFood
				{
					Inverted = true
				})
				.AddChildren(approachFood.AddTickTransition(growlFood, new Trans_TargetInRange
				{
					Range = 12f
				}).AddFailureTransition(roam).AddEndTransition(eatFood), eatFood.AddTickTransition(growlFood, new Trans_TargetInRange
				{
					Range = 12f
				}).AddFailureTransition(roam).AddEndTransition(roam), growlFood.AddTickTransition(bark, new Trans_TargetInRange
				{
					Range = 5f
				}).AddEndTransition(bark, new Trans_TargetInRange
				{
					Range = 12f
				}).AddEndTransition(approachFood))), hurt.AddEndTransition(flee, new Rust.Ai.Gen2.Trans_IsHealthBelowPercentage()).AddEndTransition(flee, new Rust.Ai.Gen2.Trans_HasBlackboardBool
			{
				Key = "HitByFire"
			}).AddEndTransition(flee, new Rust.Ai.Gen2.Trans_TargetIsNearFire())
				.AddEndTransition(flee, new Trans_TargetInRange
				{
					Range = 50f,
					Inverted = true
				})
				.AddEndTransition(fleeForHowl, new Rust.Ai.Gen2.Trans_InitialAlliesNotFighting())
				.AddEndTransition(charge, new Trans_And
				{
					new Trans_RandomChance
					{
						Chance = 0.5f
					},
					new Trans_TargetInRange
					{
						Range = 12f
					}
				})
				.AddEndTransition(reacCircle, new Trans_TargetInRange
				{
					Range = reacCircle.radius + 5f
				})
				.AddEndTransition(fastApproach)), attackUnreachable.AddFailureTransition(flee).AddEndTransition(flee, new Rust.Ai.Gen2.Trans_TargetIsNearFire()).AddEndTransition(fSMStateBase)), dead);
			SetState(state_Nothing);
			Run();
		}
	}

	public override void Hurt(HitInfo hitInfo)
	{
		if (GetComponent<SenseComponent>().CanTarget(hitInfo.Initiator) && (hitInfo.Initiator.IsNonNpcPlayer() || !(Random.value > 0.5f)))
		{
			HurtTrans.Trigger(hitInfo);
			if (base.CurrentState != hurt && base.CurrentState != dead)
			{
				ForceTickOnTheNextUpdate();
			}
		}
	}

	public void Intimidate(BaseEntity target)
	{
		AllyGotHurtNearby.Trigger(target);
	}

	public void Howl(BaseEntity target)
	{
		HowlTrans.Trigger(target);
	}

	public void Bark(BaseEntity target)
	{
		BarkTrans.Trigger(target);
	}

	public override bool OnDied(HitInfo hitInfo)
	{
		DeathTrans.Trigger(hitInfo);
		return false;
	}
}
