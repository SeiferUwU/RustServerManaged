using System;

namespace Rust.Ai.Gen2;

[Serializable]
public class State_Growl : State_PlayAnimationRM
{
	public const string AlreadyGrowled = "AlreadyGrowled";

	public override EFSMStateStatus OnStateEnter()
	{
		base.Blackboard.Increment("AlreadyGrowled");
		return base.OnStateEnter();
	}
}
