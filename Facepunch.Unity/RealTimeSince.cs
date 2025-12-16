using UnityEngine;

public struct RealTimeSince
{
	private float time;

	public static implicit operator float(RealTimeSince ts)
	{
		return Time.realtimeSinceStartup - ts.time;
	}

	public static implicit operator RealTimeSince(float ts)
	{
		return new RealTimeSince
		{
			time = Time.realtimeSinceStartup - ts
		};
	}

	public override string ToString()
	{
		return (Time.realtimeSinceStartup - time).ToString();
	}
}
