using UnityEngine;

public struct RealTimeUntil
{
	private float time;

	public static implicit operator float(RealTimeUntil ts)
	{
		return ts.time - Time.realtimeSinceStartup;
	}

	public static implicit operator RealTimeUntil(float ts)
	{
		return new RealTimeUntil
		{
			time = Time.realtimeSinceStartup + ts
		};
	}

	public override string ToString()
	{
		return (time - Time.realtimeSinceStartup).ToString();
	}
}
