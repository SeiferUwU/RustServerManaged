using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace TerrainHeightMapJobs;

[BurstCompile(FloatMode = FloatMode.Deterministic)]
public struct GetHeightsJobIndirect : IJob
{
	[WriteOnly]
	public NativeArray<float> Heights;

	[Unity.Collections.ReadOnly]
	public NativeArray<Vector3>.ReadOnly Pos;

	[Unity.Collections.ReadOnly]
	public NativeArray<int>.ReadOnly Indices;

	[Unity.Collections.ReadOnly]
	public NativeArray<short>.ReadOnly Data;

	[Unity.Collections.ReadOnly]
	public Vector3 TerrainPos;

	[Unity.Collections.ReadOnly]
	public float TerrainScale;

	[Unity.Collections.ReadOnly]
	public Vector2 TerrainOneOverSize;

	[Unity.Collections.ReadOnly]
	public int Res;

	public void Execute()
	{
		int num = Res - 1;
		for (int i = 0; i < Indices.Length; i++)
		{
			int index = Indices[i];
			float num2 = (Pos[index].x - TerrainPos.x) * TerrainOneOverSize.x;
			float num3 = (Pos[index].z - TerrainPos.z) * TerrainOneOverSize.y;
			float num4 = num2 * (float)num;
			float num5 = num3 * (float)num;
			int num6 = (int)num4;
			int num7 = (int)num5;
			float num8 = num4 - (float)num6;
			float num9 = num5 - (float)num7;
			num6 = ((num6 >= 0) ? num6 : 0);
			num7 = ((num7 >= 0) ? num7 : 0);
			num6 = ((num6 <= num) ? num6 : num);
			num7 = ((num7 <= num) ? num7 : num);
			int num10 = ((num4 < (float)num) ? 1 : 0);
			int num11 = ((num5 < (float)num) ? Res : 0);
			int num12 = num7 * Res + num6;
			int index2 = num12 + num10;
			int num13 = num12 + num11;
			int index3 = num13 + num10;
			float num14 = BitUtility.Short2Float(Data[num12]);
			float num15 = BitUtility.Short2Float(Data[index2]);
			float num16 = BitUtility.Short2Float(Data[num13]);
			float num17 = BitUtility.Short2Float(Data[index3]);
			float num18 = (num15 - num14) * num8 + num14;
			float num19 = ((num17 - num16) * num8 + num16 - num18) * num9 + num18;
			Heights[index] = TerrainPos.y + num19 * TerrainScale;
		}
	}
}
