using UnityEngine;

public struct TimeUntilWithDuration
{
	private float time;

	public float Duration;

	public static implicit operator float(TimeUntilWithDuration ts)
	{
		return ts.time - Time.time;
	}

	public static implicit operator TimeUntilWithDuration(float ts)
	{
		return new TimeUntilWithDuration
		{
			time = Time.time + ts,
			Duration = ts
		};
	}

	public override string ToString()
	{
		return time.ToString();
	}
}
