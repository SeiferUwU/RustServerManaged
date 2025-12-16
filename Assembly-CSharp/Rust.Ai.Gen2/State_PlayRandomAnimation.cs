using System;
using UnityEngine;

namespace Rust.Ai.Gen2;

[Serializable]
public class State_PlayRandomAnimation : State_PlayAnimationBase
{
	[SerializeField]
	public AnimationClip[] animations;

	public override EFSMStateStatus OnStateEnter()
	{
		EFSMStateStatus result = base.OnStateEnter();
		animState = base.AnimPlayer.PlayServerAndTakeFromPool(ArrayEx.GetRandom(animations));
		return result;
	}
}
