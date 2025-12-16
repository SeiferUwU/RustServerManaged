using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace GamePhysicsJobs;

[BurstCompile(FloatMode = FloatMode.Fast)]
internal struct PreProcessWaterRaysJob : IJob
{
	public NativeArray<RaycastHit> hits;

	public NativeArray<RaycastCommand> rays;

	public int maxHitsPerTrace;

	public NativeList<Vector2i> WaterIndices;

	public NativeList<Ray> WaterRays;

	public NativeArray<float> WaterMaxDists;

	public void Execute()
	{
		int num = 0;
		for (int i = 0; i < rays.Length; i++)
		{
			RaycastCommand raycastCommand = rays[i];
			if ((raycastCommand.queryParameters.layerMask & 0x10) != 0)
			{
				int endInd;
				int num2 = GamePhysicsJobs.Util.FindFreeSlot(i, in hits, maxHitsPerTrace, out endInd);
				if (num2 != endInd)
				{
					int index = num++;
					Ray value = new Ray(raycastCommand.from, raycastCommand.direction);
					WaterRays.Add(in value);
					WaterMaxDists[index] = raycastCommand.distance;
					WaterIndices.Add(new Vector2i(num2, endInd));
				}
			}
		}
	}
}
