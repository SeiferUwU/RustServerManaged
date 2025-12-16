using System;
using UnityEngine;

public class WaitUntilWithTimeout : CustomYieldInstruction
{
	private readonly float timeoutTime;

	private readonly Func<bool> condition;

	public string Message { get; private set; }

	public bool TimedOut { get; private set; }

	public override bool keepWaiting
	{
		get
		{
			if (condition())
			{
				return false;
			}
			if (Time.time >= timeoutTime)
			{
				TimedOut = true;
				return false;
			}
			return true;
		}
	}

	public WaitUntilWithTimeout(Func<bool> condition, float timeoutSeconds, string message = null)
	{
		this.condition = condition;
		timeoutTime = Time.time + timeoutSeconds;
		Message = message;
	}
}
