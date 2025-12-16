using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace WaterLevelJobs;

[BurstCompile(FloatMode = FloatMode.Fast)]
public struct GatherWavesIndicesJobIndirect : IJob
{
	[WriteOnly]
	public NativeArray<int> WaveIndices;

	[WriteOnly]
	public NativeReference<int> WaveIndexCount;

	[Unity.Collections.ReadOnly]
	public NativeArray<int>.ReadOnly Topologies;

	[Unity.Collections.ReadOnly]
	public NativeArray<float>.ReadOnly Heights;

	[Unity.Collections.ReadOnly]
	public NativeArray<int>.ReadOnly Indices;

	[Unity.Collections.ReadOnly]
	public float WaterLevel;

	public void Execute()
	{
		int value = 0;
		for (int i = 0; i < Indices.Length; i++)
		{
			int num = Indices[i];
			bool num2 = Heights[num] < WaterLevel;
			bool flag = (Topologies[num] & 0x180) != 0;
			if (num2 && flag)
			{
				WaveIndices[value++] = num;
			}
		}
		WaveIndexCount.Value = value;
	}
}
