using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace TerrainTexturingJobs;

[BurstCompile(FloatMode = FloatMode.Fast)]
public struct GetCoarseDistsToShoreJobIndirect : IJob
{
	[WriteOnly]
	public NativeArray<float> Dists;

	[Unity.Collections.ReadOnly]
	public NativeArray<Vector2>.ReadOnly UVs;

	[Unity.Collections.ReadOnly]
	public NativeArray<int>.ReadOnly Indices;

	[Unity.Collections.ReadOnly]
	public NativeArray<float>.ReadOnly Data;

	[Unity.Collections.ReadOnly]
	public int ShoreMapSize;

	[Unity.Collections.ReadOnly]
	public float ShoreDistanceScale;

	public void Execute()
	{
		int shoreMapSize = ShoreMapSize;
		int num = shoreMapSize - 1;
		for (int i = 0; i < Indices.Length; i++)
		{
			int index = Indices[i];
			Vector2 vector = UVs[index];
			float num2 = vector.x * (float)num;
			float num3 = vector.y * (float)num;
			int num4 = (int)num2;
			int num5 = (int)num3;
			float num6 = num2 - (float)num4;
			float num7 = num3 - (float)num5;
			num4 = ((num4 >= 0) ? num4 : 0);
			num5 = ((num5 >= 0) ? num5 : 0);
			num4 = ((num4 <= num) ? num4 : num);
			num5 = ((num5 <= num) ? num5 : num);
			int num8 = ((num2 < (float)num) ? 1 : 0);
			int num9 = ((num3 < (float)num) ? shoreMapSize : 0);
			int num10 = num5 * shoreMapSize + num4;
			int index2 = num10 + num8;
			int num11 = num10 + num9;
			int index3 = num11 + num8;
			float num12 = Data[num10];
			float num13 = Data[index2];
			float num14 = Data[num11];
			float num15 = Data[index3];
			float num16 = (num13 - num12) * num6 + num12;
			float num17 = (num15 - num14) * num6 + num14;
			Dists[index] = ((num17 - num16) * num7 + num16) * ShoreDistanceScale;
		}
	}
}
