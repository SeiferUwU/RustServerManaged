using System;
using UnityEngine;

namespace Rust.Ai.Gen2;

[Serializable]
public class State_PlayAnimationRM : State_PlayAnimationBase
{
	[SerializeField]
	public RootMotionData Animation;

	public override EFSMStateStatus OnStateEnter()
	{
		EFSMStateStatus result = base.OnStateEnter();
		animState = base.AnimPlayer.PlayServerAndTakeFromPool(GetAnimation());
		return result;
	}

	protected virtual RootMotionData GetAnimation()
	{
		return Animation;
	}
}
