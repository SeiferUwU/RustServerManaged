using Unity.Mathematics;

namespace Facepunch.BurstCloth.Jobs;

internal struct BoneState
{
	public float3 Position;

	public quaternion Rotation;

	public float3 OldPosition;

	public float3 Delta;
}
