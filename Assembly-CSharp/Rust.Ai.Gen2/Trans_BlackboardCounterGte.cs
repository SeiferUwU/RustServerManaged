using System;
using UnityEngine;

namespace Rust.Ai.Gen2;

[Serializable]
internal class Trans_BlackboardCounterGte : FSMTransitionBase
{
	[SerializeField]
	public string Key;

	[SerializeField]
	public int MinValue;

	private BlackboardComponent _blackboard;

	private BlackboardComponent Blackboard => _blackboard ?? (_blackboard = Owner.GetComponent<BlackboardComponent>());

	protected override bool EvaluateInternal()
	{
		using (TimeWarning.New("Trans_HasBlackboardBool"))
		{
			if (!Blackboard.Count(Key, out var count))
			{
				return false;
			}
			return count >= MinValue;
		}
	}

	public override string GetName()
	{
		return $"{base.GetName()} {Key} >= {MinValue}";
	}
}
