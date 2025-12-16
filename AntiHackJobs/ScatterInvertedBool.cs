using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace AntiHackJobs;

[BurstCompile]
public struct ScatterInvertedBool : IJob
{
	[WriteOnly]
	public NativeArray<bool> To;

	[Unity.Collections.ReadOnly]
	public NativeArray<bool>.ReadOnly From;

	[Unity.Collections.ReadOnly]
	public NativeArray<int>.ReadOnly Indices;

	public void Execute()
	{
		for (int i = 0; i < Indices.Length; i++)
		{
			To[Indices[i]] = !From[i];
		}
	}
}
