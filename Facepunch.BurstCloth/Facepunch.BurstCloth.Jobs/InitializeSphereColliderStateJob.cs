using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine.Jobs;

namespace Facepunch.BurstCloth.Jobs;

[BurstCompile]
internal struct InitializeSphereColliderStateJob : IJobParallelForTransform
{
	[ReadOnly]
	[NativeMatchesParallelForLength]
	public NativeArray<SphereColliderData> Data;

	[WriteOnly]
	[NativeMatchesParallelForLength]
	public NativeArray<SphereColliderState> State;

	public void Execute(int index, TransformAccess transform)
	{
		ref readonly SphereColliderData reference = ref BurstUtil.GetReadonly(in Data, index);
		ref SphereColliderState reference2 = ref BurstUtil.Get(in State, index);
		float3 @float = transform.position;
		quaternion q = transform.rotation;
		reference2.Position = @float + math.mul(q, reference.LocalPosition);
	}
}
