using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace GamePhysicsJobs;

[BurstCompile]
public struct CountRaycastHitsJobs : IJob
{
	public NativeArray<int> Counts;

	[Unity.Collections.ReadOnly]
	public NativeArray<RaycastHit>.ReadOnly Hits;

	[Unity.Collections.ReadOnly]
	public int MaxHitsPerRay;

	public void Execute()
	{
		for (int i = 0; i < Counts.Length; i++)
		{
			int num = 0;
			for (int j = 0; j < MaxHitsPerRay; j++)
			{
				int index = i * MaxHitsPerRay + j;
				if (Hits[index].normal == Vector3.zero)
				{
					break;
				}
				num++;
			}
			Counts[i] = num;
		}
	}
}
