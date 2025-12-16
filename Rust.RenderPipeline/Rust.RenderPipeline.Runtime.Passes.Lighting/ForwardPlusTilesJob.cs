using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace Rust.RenderPipeline.Runtime.Passes.Lighting;

[BurstCompile(FloatPrecision.Standard, FloatMode.Fast)]
public struct ForwardPlusTilesJob : IJobFor
{
	[ReadOnly]
	public NativeArray<float4> lightBounds;

	[WriteOnly]
	[NativeDisableParallelForRestriction]
	public NativeArray<int> tileData;

	public int additionalLightCount;

	public float2 tileScreenUVSize;

	public int maxLightsPerTile;

	public int tilesPerRow;

	public int tileDataSize;

	public void Execute(int tileIndex)
	{
		int num = tileIndex / tilesPerRow;
		int num2 = tileIndex - num * tilesPerRow;
		float4 @float = math.float4(num2, num, num2 + 1, num + 1) * tileScreenUVSize.xyxy;
		int num3 = tileIndex * tileDataSize;
		int num4 = num3;
		int num5 = 0;
		for (int i = 0; i < additionalLightCount; i++)
		{
			float4 float2 = lightBounds[i];
			if (math.all(math.float4(float2.xy, @float.xy) <= math.float4(@float.zw, float2.zw)))
			{
				tileData[++num4] = i;
				if (++num5 >= maxLightsPerTile)
				{
					break;
				}
			}
		}
		tileData[num3] = num5;
	}
}
