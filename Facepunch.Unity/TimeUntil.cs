using UnityEngine;

public struct TimeUntil
{
	private float time;

	public static implicit operator float(TimeUntil ts)
	{
		return ts.time - Time.time;
	}

	public static implicit operator TimeUntil(float ts)
	{
		return new TimeUntil
		{
			time = Time.time + ts
		};
	}

	public override string ToString()
	{
		return (time - Time.time).ToString();
	}
}
