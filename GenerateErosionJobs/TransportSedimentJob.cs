using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace GenerateErosionJobs;

[BurstCompile(FloatMode = FloatMode.Deterministic)]
internal struct TransportSedimentJob : IJobParallelFor
{
	public NativeArray<float> SedimentMap;

	public NativeArray<float>.ReadOnly SedimentReadOnlyMap;

	public NativeArray<float2>.ReadOnly VelocityMap;

	public int Res;

	public float DT;

	public void Execute(int index)
	{
		int num = index % Res;
		int num2 = index / Res;
		float2 @float = VelocityMap[index];
		int x = (int)((float)num - DT * @float.x);
		int x2 = (int)((float)num2 - DT * @float.y);
		x = math.clamp(x, 0, Res - 1);
		x2 = math.clamp(x2, 0, Res - 1);
		SedimentMap[index] = SedimentReadOnlyMap[x2 * Res + x];
	}
}
