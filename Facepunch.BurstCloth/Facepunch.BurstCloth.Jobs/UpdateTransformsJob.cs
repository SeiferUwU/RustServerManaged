using Unity.Collections;
using Unity.Mathematics;
using UnityEngine.Jobs;

namespace Facepunch.BurstCloth.Jobs;

internal struct UpdateTransformsJob : IJobParallelForTransform
{
	[ReadOnly]
	[NativeMatchesParallelForLength]
	public NativeArray<BoneData> Data;

	[ReadOnly]
	[NativeMatchesParallelForLength]
	public NativeArray<BoneState> State;

	public void Execute(int index, [WriteOnly] TransformAccess transform)
	{
		ref readonly BoneData reference = ref BurstUtil.GetReadonly(in Data, index);
		ref readonly BoneState reference2 = ref BurstUtil.GetReadonly(in State, index);
		if (reference.Depth != 0)
		{
			ref readonly BoneState reference3 = ref BurstUtil.GetReadonly(in State, reference.Parent);
			RigidTransform a = math.inverse(math.RigidTransform(reference3.Rotation, reference3.Position));
			RigidTransform b = math.RigidTransform(reference2.Rotation, reference2.Position);
			RigidTransform rigidTransform = math.mul(a, b);
			transform.localRotation = rigidTransform.rot;
			transform.localPosition = rigidTransform.pos;
		}
	}
}
