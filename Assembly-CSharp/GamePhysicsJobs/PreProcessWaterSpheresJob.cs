using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace GamePhysicsJobs;

[BurstCompile(FloatMode = FloatMode.Fast)]
internal struct PreProcessWaterSpheresJob : IJob
{
	public NativeArray<RaycastHit> hits;

	public NativeArray<SpherecastCommand> rays;

	public int maxHitsPerTrace;

	public NativeList<Vector2i> WaterIndices;

	public NativeList<Ray> WaterRays;

	public NativeArray<float> WaterMaxDists;

	public void Execute()
	{
		int num = 0;
		for (int i = 0; i < rays.Length; i++)
		{
			SpherecastCommand spherecastCommand = rays[i];
			if ((spherecastCommand.queryParameters.layerMask & 0x10) != 0)
			{
				int endInd;
				int num2 = GamePhysicsJobs.Util.FindFreeSlot(i, in hits, maxHitsPerTrace, out endInd);
				if (num2 != endInd)
				{
					int index = num++;
					Ray value = new Ray(spherecastCommand.origin, spherecastCommand.direction);
					WaterRays.Add(in value);
					WaterMaxDists[index] = spherecastCommand.distance;
					WaterIndices.Add(new Vector2i(num2, endInd));
				}
			}
		}
	}
}
