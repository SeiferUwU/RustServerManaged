using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Facepunch;

[CreateAssetMenu(fileName = "Skeleton", menuName = "Facepunch/Skeleton Definition")]
public class SkeletonDefinition : ScriptableObject
{
	[Serializable]
	public struct Bone
	{
		public int Id;

		public int Depth;

		public int Parent;

		public string Name;

		public GameObject Target;

		public BoneFlag Flags;
	}

	[BurstCompile]
	private struct FindJob : IJob
	{
		public FixedString512Bytes Name;

		public NativeArray<FixedString512Bytes> Bones;

		public NativeReference<int> FoundIndex;

		public void Execute()
		{
			FixedString512Bytes a = FixedStringMethods.ToLowerAscii(ref Name);
			FoundIndex.Value = -1;
			for (int i = 0; i < Bones.Length; i++)
			{
				if (a == Bones[i])
				{
					FoundIndex.Value = i;
					break;
				}
			}
		}
	}

	[Serializable]
	public struct RagdollSettings
	{
		[Serializable]
		public struct BodySide
		{
			public int Hip;

			public int Knee;

			public int Foot;

			public int Arm;

			public int Elbow;
		}

		public int Pelvis;

		public int Head;

		public int Spine;

		public BodySide Left;

		public BodySide Right;

		public float Mass;

		public float ArmGirth;

		public float LegGirth;

		public float HeadSize;

		public PhysicMaterial Material;
	}

	public GameObject SourceObject;

	public Bone[] Bones;

	[NonSerialized]
	private NativeArray<FixedString512Bytes> NativeBoneNames;

	[NonSerialized]
	private NativeReference<int> SearchIndex;

	public RagdollSettings Ragdoll;

	private void OnDestroy()
	{
		if (NativeBoneNames.IsCreated)
		{
			NativeBoneNames.Dispose();
		}
		if (SearchIndex.IsCreated)
		{
			SearchIndex.Dispose();
		}
	}

	public Bone FindBone(string name)
	{
		FixedString512Bytes fixedString512Bytes = name;
		if (!NativeBoneNames.IsCreated)
		{
			NativeBoneNames = new NativeArray<FixedString512Bytes>(Bones.Length, Allocator.Persistent);
			for (int i = 0; i < Bones.Length; i++)
			{
				Bone bone = Bones[i];
				FixedString512Bytes fs = new FixedString512Bytes(bone.Name);
				NativeBoneNames[i] = FixedStringMethods.ToLowerAscii(ref fs);
			}
		}
		if (!SearchIndex.IsCreated)
		{
			SearchIndex = new NativeReference<int>(-1, Allocator.Persistent);
		}
		FindJob jobData = new FindJob
		{
			Name = fixedString512Bytes,
			Bones = NativeBoneNames,
			FoundIndex = SearchIndex
		};
		IJobExtensions.RunByRef(ref jobData);
		if (SearchIndex.Value != -1)
		{
			return Bones[SearchIndex.Value];
		}
		return default(Bone);
	}
}
