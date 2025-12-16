using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace GamePhysicsJobs;

[BurstCompile(FloatMode = FloatMode.Fast)]
public struct GenerateSphereCmdsFromCapsuleCmdsJob : IJob
{
	[WriteOnly]
	public NativeList<OverlapSphereCommand> SphereCommands;

	[Unity.Collections.ReadOnly]
	public NativeArray<OverlapCapsuleCommand>.ReadOnly Commands;

	[Unity.Collections.ReadOnly]
	public NativeArray<int>.ReadOnly Indices;

	public void Execute()
	{
		foreach (int index in Indices)
		{
			OverlapCapsuleCommand overlapCapsuleCommand = Commands[index];
			OverlapSphereCommand value = new OverlapSphereCommand(overlapCapsuleCommand.point0, overlapCapsuleCommand.radius, overlapCapsuleCommand.queryParameters);
			SphereCommands.AddNoResize(value);
		}
	}
}
