using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace OceanSimulationJobs;

[BurstCompile(FloatMode = FloatMode.Fast)]
internal struct SmallDisplacementPlaneTraceJob : IJobParallelForDefer
{
	public Plane SeaPlane;

	[Unity.Collections.ReadOnly]
	public NativeList<Ray> Rays;

	public NativeArray<float>.ReadOnly MaxDists;

	[WriteOnly]
	public NativeArray<bool> HitResults;

	[WriteOnly]
	public NativeArray<Vector3> HitPositions;

	public void Execute(int index)
	{
		Ray ray = Rays[index];
		float num = MaxDists[index];
		bool value = false;
		Vector3 value2 = Vector3.zero;
		if (SeaPlane.Raycast(ray, out var enter) && enter < num)
		{
			value = true;
			value2 = ray.GetPoint(enter);
		}
		HitResults[index] = value;
		HitPositions[index] = value2;
	}
}
