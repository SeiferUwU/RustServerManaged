using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace AntiHackJobs;

[BurstCompile]
public struct GatherPlayersWithTicksJob : IJob
{
	[WriteOnly]
	public NativeList<int> ValidIndices;

	[Unity.Collections.ReadOnly]
	public TickInterpolatorCache.ReadOnlyState TickCache;

	[Unity.Collections.ReadOnly]
	public NativeArray<int>.ReadOnly Indices;

	public void Execute()
	{
		foreach (int index in Indices)
		{
			if (TickCache.Infos[index].Count > 0)
			{
				ValidIndices.AddNoResize(index);
			}
		}
	}
}
