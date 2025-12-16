using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace AntiHackJobs;

[BurstCompile]
public struct BuildLayerMasksJob : IJob
{
	[WriteOnly]
	public NativeList<int> LayerMasks;

	[Unity.Collections.ReadOnly]
	internal NativeArray<AntiHack.Batch>.ReadOnly Batches;

	[Unity.Collections.ReadOnly]
	public int DefaultMask;

	[Unity.Collections.ReadOnly]
	public int NoVehiclesMask;

	public void Execute()
	{
		foreach (AntiHack.Batch batch in Batches)
		{
			int value = (batch.ExcludeVehicleLayer ? NoVehiclesMask : DefaultMask);
			for (int i = 0; i < batch.Count; i++)
			{
				LayerMasks.AddNoResize(value);
			}
		}
	}
}
