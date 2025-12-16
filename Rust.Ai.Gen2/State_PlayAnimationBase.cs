using UnityEngine;

namespace Rust.Ai.Gen2;

public abstract class State_PlayAnimationBase : FSMStateBase
{
	[SerializeField]
	public bool FaceTarget;

	protected RootMotionPlayer.PlayServerState animState;

	public override EFSMStateStatus OnStateEnter()
	{
		if (FaceTarget && base.Senses.FindTargetPosition(out var targetPosition))
		{
			Vector3 forward = targetPosition - Owner.transform.position;
			forward.y = 0f;
			Owner.transform.rotation = Quaternion.LookRotation(forward);
		}
		return base.OnStateEnter();
	}

	public override EFSMStateStatus OnStateUpdate(float deltaTime)
	{
		if (!animState.isPlaying)
		{
			return EFSMStateStatus.Success;
		}
		return EFSMStateStatus.None;
	}

	public override void OnStateExit()
	{
		base.AnimPlayer.StopServerAndReturnToPool(ref animState);
	}
}
