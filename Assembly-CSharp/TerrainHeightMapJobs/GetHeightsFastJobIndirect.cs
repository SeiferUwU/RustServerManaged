using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace TerrainHeightMapJobs;

[BurstCompile(FloatMode = FloatMode.Deterministic)]
public struct GetHeightsFastJobIndirect : IJob
{
	[WriteOnly]
	public NativeArray<float> Heights;

	[Unity.Collections.ReadOnly]
	public NativeArray<UnityEngine.Vector2>.ReadOnly UVs;

	[Unity.Collections.ReadOnly]
	public NativeArray<int>.ReadOnly Indices;

	[Unity.Collections.ReadOnly]
	public NativeArray<short>.ReadOnly Data;

	[Unity.Collections.ReadOnly]
	public int Res;

	[Unity.Collections.ReadOnly]
	public float TerrainPos;

	[Unity.Collections.ReadOnly]
	public float TerrainScale;

	public void Execute()
	{
		int num = Res - 1;
		for (int i = 0; i < Indices.Length; i++)
		{
			int index = Indices[i];
			float num2 = UVs[index].x * (float)num;
			float num3 = UVs[index].y * (float)num;
			int num4 = (int)num2;
			int num5 = (int)num3;
			float num6 = num2 - (float)num4;
			float num7 = num3 - (float)num5;
			num4 = ((num4 >= 0) ? num4 : 0);
			num5 = ((num5 >= 0) ? num5 : 0);
			num4 = ((num4 <= num) ? num4 : num);
			num5 = ((num5 <= num) ? num5 : num);
			int num8 = ((num2 < (float)num) ? 1 : 0);
			int num9 = ((num3 < (float)num) ? Res : 0);
			int num10 = num5 * Res + num4;
			int index2 = num10 + num8;
			int num11 = num10 + num9;
			int index3 = num11 + num8;
			float num12 = BitUtility.Short2Float(Data[num10]);
			float num13 = BitUtility.Short2Float(Data[index2]);
			float num14 = BitUtility.Short2Float(Data[num11]);
			float num15 = BitUtility.Short2Float(Data[index3]);
			float num16 = (num13 - num12) * num6 + num12;
			float num17 = ((num15 - num14) * num6 + num14 - num16) * num7 + num16;
			Heights[index] = TerrainPos + num17 * TerrainScale;
		}
	}
}
