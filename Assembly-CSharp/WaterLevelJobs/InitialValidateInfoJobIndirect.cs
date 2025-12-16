using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace WaterLevelJobs;

[BurstCompile(FloatMode = FloatMode.Fast)]
public struct InitialValidateInfoJobIndirect : IJob
{
	[WriteOnly]
	public NativeArray<WaterLevel.WaterInfo> Results;

	[Unity.Collections.ReadOnly]
	public NativeArray<UnityEngine.Vector3>.ReadOnly Starts;

	[Unity.Collections.ReadOnly]
	public NativeArray<UnityEngine.Vector3>.ReadOnly Ends;

	[Unity.Collections.ReadOnly]
	public NativeArray<float>.ReadOnly Radii;

	[Unity.Collections.ReadOnly]
	public NativeArray<float>.ReadOnly WaterHeights;

	[Unity.Collections.ReadOnly]
	public NativeArray<float>.ReadOnly TerrainHeights;

	[Unity.Collections.ReadOnly]
	public NativeArray<int>.ReadOnly Indices;

	public void Execute()
	{
		for (int i = 0; i < Indices.Length; i++)
		{
			int index = Indices[i];
			Results[index] = WaterLevel.InitialValidate(Starts[index], Ends[index], Radii[index], WaterHeights[index], TerrainHeights[index]);
		}
	}
}
