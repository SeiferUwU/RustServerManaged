using Unity.Collections;
using UnityEngine.Jobs;

namespace Facepunch.BurstCloth.Jobs;

internal struct InitializeBoneStateJob : IJobParallelForTransform
{
	[ReadOnly]
	[NativeMatchesParallelForLength]
	public NativeArray<BoneData> Data;

	[WriteOnly]
	[NativeMatchesParallelForLength]
	public NativeArray<BoneState> State;

	public void Execute(int index, [ReadOnly] TransformAccess transform)
	{
		ref readonly BoneData reference = ref BurstUtil.GetReadonly(in Data, index);
		ref BoneState reference2 = ref BurstUtil.Get(in State, index);
		if (reference.Depth == 0)
		{
			reference2.OldPosition = reference2.Position;
			reference2.Position = transform.position;
		}
		reference2.Rotation = transform.rotation;
	}
}
