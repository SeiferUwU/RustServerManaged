using Unity.Burst;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;

namespace BasePlayerJobs;

[BurstCompile]
public struct RecacheTransforms : IJobParallelForTransform
{
	[WriteOnly]
	public NativeArray<Vector3> LocalPos;

	[WriteOnly]
	public NativeArray<Vector3> Pos;

	[WriteOnly]
	public NativeArray<Quaternion> LocalRots;

	[WriteOnly]
	public NativeArray<Quaternion> Rots;

	public void Execute(int index, TransformAccess transf)
	{
		if (transf.isValid)
		{
			LocalPos[index] = transf.localPosition;
			Pos[index] = transf.position;
			LocalRots[index] = transf.localRotation;
			Rots[index] = transf.rotation;
		}
	}
}
