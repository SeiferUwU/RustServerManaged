using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Facepunch;
using Facepunch.Extend;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyVersion("0.0.0.0")]
[CompilerGenerated]
[EditorBrowsable(EditorBrowsableState.Never)]
[GeneratedCode("Unity.MonoScriptGenerator.MonoScriptInfoGenerator", null)]
internal class UnitySourceGeneratedAssemblyMonoScriptTypes_v1
{
	private struct MonoScriptData
	{
		public byte[] FilePathsData;

		public byte[] TypesData;

		public int TotalTypes;

		public int TotalFiles;

		public bool IsEditorOnly;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static MonoScriptData Get()
	{
		return new MonoScriptData
		{
			FilePathsData = new byte[362]
			{
				0, 0, 0, 1, 0, 0, 0, 46, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 70, 97, 99, 101, 112, 117,
				110, 99, 104, 46, 83, 107, 101, 108, 101, 116,
				111, 110, 92, 83, 107, 101, 108, 101, 116, 111,
				110, 46, 99, 115, 0, 0, 0, 1, 0, 0,
				0, 56, 92, 65, 115, 115, 101, 116, 115, 92,
				80, 108, 117, 103, 105, 110, 115, 92, 70, 97,
				99, 101, 112, 117, 110, 99, 104, 46, 83, 107,
				101, 108, 101, 116, 111, 110, 92, 83, 107, 101,
				108, 101, 116, 111, 110, 65, 116, 116, 97, 99,
				104, 109, 101, 110, 116, 46, 99, 115, 0, 0,
				0, 5, 0, 0, 0, 56, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 70, 97, 99, 101, 112, 117, 110, 99,
				104, 46, 83, 107, 101, 108, 101, 116, 111, 110,
				92, 83, 107, 101, 108, 101, 116, 111, 110, 68,
				101, 102, 105, 110, 105, 116, 105, 111, 110, 46,
				99, 115, 0, 0, 0, 1, 0, 0, 0, 53,
				92, 65, 115, 115, 101, 116, 115, 92, 80, 108,
				117, 103, 105, 110, 115, 92, 70, 97, 99, 101,
				112, 117, 110, 99, 104, 46, 83, 107, 101, 108,
				101, 116, 111, 110, 92, 83, 107, 101, 108, 101,
				116, 111, 110, 82, 97, 103, 100, 111, 108, 108,
				46, 99, 115, 0, 0, 0, 1, 0, 0, 0,
				50, 92, 65, 115, 115, 101, 116, 115, 92, 80,
				108, 117, 103, 105, 110, 115, 92, 70, 97, 99,
				101, 112, 117, 110, 99, 104, 46, 83, 107, 101,
				108, 101, 116, 111, 110, 92, 83, 107, 101, 108,
				101, 116, 111, 110, 83, 107, 105, 110, 46, 99,
				115, 0, 0, 0, 1, 0, 0, 0, 53, 92,
				65, 115, 115, 101, 116, 115, 92, 80, 108, 117,
				103, 105, 110, 115, 92, 70, 97, 99, 101, 112,
				117, 110, 99, 104, 46, 83, 107, 101, 108, 101,
				116, 111, 110, 92, 83, 107, 101, 108, 101, 116,
				111, 110, 83, 107, 105, 110, 76, 111, 100, 46,
				99, 115
			},
			TypesData = new byte[347]
			{
				0, 0, 0, 0, 18, 70, 97, 99, 101, 112,
				117, 110, 99, 104, 124, 83, 107, 101, 108, 101,
				116, 111, 110, 0, 0, 0, 0, 28, 70, 97,
				99, 101, 112, 117, 110, 99, 104, 124, 83, 107,
				101, 108, 101, 116, 111, 110, 65, 116, 116, 97,
				99, 104, 109, 101, 110, 116, 0, 0, 0, 0,
				28, 70, 97, 99, 101, 112, 117, 110, 99, 104,
				124, 83, 107, 101, 108, 101, 116, 111, 110, 68,
				101, 102, 105, 110, 105, 116, 105, 111, 110, 0,
				0, 0, 0, 33, 70, 97, 99, 101, 112, 117,
				110, 99, 104, 46, 83, 107, 101, 108, 101, 116,
				111, 110, 68, 101, 102, 105, 110, 105, 116, 105,
				111, 110, 124, 66, 111, 110, 101, 0, 0, 0,
				0, 36, 70, 97, 99, 101, 112, 117, 110, 99,
				104, 46, 83, 107, 101, 108, 101, 116, 111, 110,
				68, 101, 102, 105, 110, 105, 116, 105, 111, 110,
				124, 70, 105, 110, 100, 74, 111, 98, 0, 0,
				0, 0, 44, 70, 97, 99, 101, 112, 117, 110,
				99, 104, 46, 83, 107, 101, 108, 101, 116, 111,
				110, 68, 101, 102, 105, 110, 105, 116, 105, 111,
				110, 124, 82, 97, 103, 100, 111, 108, 108, 83,
				101, 116, 116, 105, 110, 103, 115, 0, 0, 0,
				0, 38, 70, 97, 99, 101, 112, 117, 110, 99,
				104, 46, 83, 107, 101, 108, 101, 116, 111, 110,
				68, 101, 102, 105, 110, 105, 116, 105, 111, 110,
				43, 124, 66, 111, 100, 121, 83, 105, 100, 101,
				0, 0, 0, 0, 25, 70, 97, 99, 101, 112,
				117, 110, 99, 104, 124, 83, 107, 101, 108, 101,
				116, 111, 110, 82, 97, 103, 100, 111, 108, 108,
				0, 0, 0, 0, 22, 70, 97, 99, 101, 112,
				117, 110, 99, 104, 124, 83, 107, 101, 108, 101,
				116, 111, 110, 83, 107, 105, 110, 0, 0, 0,
				0, 25, 70, 97, 99, 101, 112, 117, 110, 99,
				104, 124, 83, 107, 101, 108, 101, 116, 111, 110,
				83, 107, 105, 110, 76, 111, 100
			},
			TotalFiles = 6,
			TotalTypes = 10,
			IsEditorOnly = false
		};
	}
}
namespace Facepunch;

[Flags]
public enum BoneFlag
{
	Left = 1,
	Middle = 2,
	Right = 4,
	Pelvis = 8,
	Hip = 0x10,
	Knee = 0x20,
	Foot = 0x40,
	Arm = 0x80,
	Elbow = 0x100,
	Spine = 0x200,
	Head = 0x400,
	Eye = 0x800,
	Finger = 0x1000,
	Thumb = 0x2000
}
[AddComponentMenu("Facepunch/Skeleton")]
public class Skeleton : MonoBehaviour
{
	public SkeletonDefinition Source;

	public GameObject[] Bones;

	public Transform GetTransform(int id)
	{
		if (id < 0)
		{
			return null;
		}
		if (!(Bones[id] != null))
		{
			return null;
		}
		return Bones[id].transform;
	}

	public Transform GetTransformSlow(string name)
	{
		using (TimeWarning.New("GetTransformSlow"))
		{
			SkeletonDefinition.Bone bone = Source.FindBone(name);
			if (!string.IsNullOrEmpty(bone.Name))
			{
				return GetTransform(bone.Id);
			}
			if (Bones != null && Bones.Length != 0)
			{
				using (TimeWarning.New("RecurisveSearch"))
				{
					for (int i = 0; i < Bones.Length; i++)
					{
						Transform transform = Bones[i].transform.FindChildRecursive(name);
						if (transform != null)
						{
							return transform;
						}
					}
				}
			}
			return null;
		}
	}

	public int GetBoneId(string boneName)
	{
		for (int i = 0; i < Source.Bones.Length; i++)
		{
			if (string.Equals(Source.Bones[i].Name, boneName, StringComparison.InvariantCultureIgnoreCase))
			{
				return Source.Bones[i].Id;
			}
		}
		return -1;
	}

	public IEnumerable<Transform> GetTransforms()
	{
		return Source.Bones.Select((SkeletonDefinition.Bone x) => GetTransform(x.Id));
	}

	public GameObject GetGameObject(int id)
	{
		if (id < 0)
		{
			return null;
		}
		return Bones[id];
	}

	public void CopyTo(Skeleton to)
	{
		if (to.Source != Source)
		{
			throw new ArgumentException("Trying to copy transforms between different skeletons, source :" + to.gameObject.name + " to :" + to.gameObject.name);
		}
		if (to.Bones.Length != Bones.Length)
		{
			throw new ArgumentException("Bone arrays are different sizes, skeleton might need rebuilding");
		}
		for (int i = 0; i < Bones.Length; i++)
		{
			if (!(Bones[i] == null) && !(to.Bones[i] == null))
			{
				Bones[i].transform.GetPositionAndRotation(out var position, out var rotation);
				to.Bones[i].transform.SetPositionAndRotation(position, rotation);
			}
		}
	}

	public void CopyFrom(Vector3[] sourceBonePos, Quaternion[] sourceBoneRot, bool localSpace)
	{
		if (sourceBonePos.Length != sourceBoneRot.Length)
		{
			throw new ArgumentException("Bone pos and rot arrays are different sizes");
		}
		if (sourceBonePos.Length != Bones.Length)
		{
			throw new ArgumentException("Bone arrays are different sizes, skeleton might need rebuilding");
		}
		for (int i = 0; i < sourceBonePos.Length; i++)
		{
			if (!(Bones[i] == null))
			{
				_ = ref sourceBonePos[i];
				if (localSpace)
				{
					Bones[i].transform.SetLocalPositionAndRotation(sourceBonePos[i], sourceBoneRot[i]);
				}
				else
				{
					Bones[i].transform.SetPositionAndRotation(sourceBonePos[i], sourceBoneRot[i]);
				}
			}
		}
	}

	public void CopySkeletonSkins(SkeletonSkin[] renderers, LODGroup lodGroup = null)
	{
		SkinnedMeshRenderer[] array = new SkinnedMeshRenderer[renderers.Length];
		for (int i = 0; i < renderers.Length; i++)
		{
			array[i] = CopySkin(renderers[i]);
		}
		if (lodGroup != null)
		{
			CopyLodGroup(lodGroup, renderers, array);
		}
	}

	private void CopyLodGroup(LODGroup lodGroup, SkeletonSkin[] renderers, SkinnedMeshRenderer[] created)
	{
		LOD[] lODs = lodGroup.GetLODs();
		for (int i = 0; i < lODs.Length; i++)
		{
			LOD lOD = lODs[i];
			for (int j = 0; j < lOD.renderers.Length; j++)
			{
				int num = LookupRendererIndex(renderers, lOD.renderers[j]);
				lOD.renderers[j] = ((num >= 0) ? created[num] : null);
			}
		}
		LODGroup lODGroup = base.gameObject.AddComponent<LODGroup>();
		lODGroup.size = lodGroup.size;
		lODGroup.animateCrossFading = lodGroup.animateCrossFading;
		lODGroup.fadeMode = lodGroup.fadeMode;
		lODGroup.enabled = lodGroup.enabled;
		lODGroup.SetLODs(lODs);
	}

	private int LookupRendererIndex(SkeletonSkin[] renderers, Renderer renderer)
	{
		for (int i = 0; i < renderers.Length; i++)
		{
			if (renderers[i].SkinnedMeshRenderer == renderer)
			{
				return i;
			}
		}
		return -1;
	}

	private SkinnedMeshRenderer CopySkin(SkeletonSkin renderer)
	{
		GameObject gameObject = new GameObject();
		gameObject.transform.parent = base.transform;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.identity;
		gameObject.transform.localScale = Vector3.one;
		gameObject.layer = base.gameObject.layer;
		return renderer.DuplicateAndRetarget(gameObject, this);
	}

	public void CopySkeletonAttachments(SkeletonAttachment[] attachments)
	{
		for (int i = 0; i < attachments.Length; i++)
		{
			CopyAttachment(attachments[i]);
		}
	}

	private void CopyAttachment(SkeletonAttachment skeletonAttachment)
	{
		Skeleton componentInParent = skeletonAttachment.GetComponentInParent<Skeleton>();
		if (componentInParent == null)
		{
			throw new Exception("SkeletonAttachment has no Skeleton");
		}
		if (Source != componentInParent.Source)
		{
			throw new Exception("SkeletonAttachment uses different Skeleton");
		}
		int num = componentInParent.ReverseLookupBone(skeletonAttachment.transform.parent.gameObject);
		if (num < 0)
		{
			throw new Exception("SkeletonAttachment attached to bone that isn't in skeleton");
		}
		skeletonAttachment.CopyTo(Bones[num]);
	}

	private int ReverseLookupBone(GameObject bone)
	{
		return Array.IndexOf(Bones, bone);
	}

	public void Rebuild()
	{
		Bones = GetBones(base.gameObject, Source);
	}

	public static GameObject[] GetBones(GameObject root, SkeletonDefinition skeletonDef)
	{
		Transform[] allChildren = root.GetComponentsInChildren<Transform>();
		return skeletonDef.Bones.Select((SkeletonDefinition.Bone bone) => FindBoneImpl(root, in bone, allChildren)).ToArray();
	}

	private static GameObject FindBoneImpl(GameObject root, in SkeletonDefinition.Bone bone, Transform[] allChildren)
	{
		int num = 0;
		Transform transform = null;
		foreach (Transform transform2 in allChildren)
		{
			if (string.Equals(transform2.name, bone.Name, StringComparison.OrdinalIgnoreCase))
			{
				num++;
				transform = transform2;
			}
		}
		if (num == 1)
		{
			return transform.gameObject;
		}
		if (num > 1)
		{
			UnityEngine.Debug.LogWarning(root.name + ": Multiple bones named " + bone.Name, root);
			return null;
		}
		return null;
	}
}
public class SkeletonAttachment : MonoBehaviour
{
	public void CopyTo(GameObject parent)
	{
		GameObject gameObject = new GameObject();
		gameObject.transform.parent = parent.transform;
		gameObject.transform.localPosition = base.transform.localPosition;
		gameObject.transform.localRotation = base.transform.localRotation;
		gameObject.transform.localScale = base.transform.localScale;
		MeshFilter component = GetComponent<MeshFilter>();
		if ((bool)component)
		{
			gameObject.AddComponent<MeshFilter>().sharedMesh = component.sharedMesh;
		}
		MeshRenderer component2 = GetComponent<MeshRenderer>();
		if ((bool)component2)
		{
			gameObject.AddComponent<MeshRenderer>().sharedMaterials = component2.sharedMaterials;
		}
	}
}
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
public class SkeletonRagdoll : Skeleton
{
}
[AddComponentMenu("Facepunch/SkeletonSkin")]
[ExecuteInEditMode]
public class SkeletonSkin : MonoBehaviour
{
	public SkinnedMeshRenderer SkinnedMeshRenderer;

	public SkeletonDefinition SkeletonDefinition;

	public int LOD;

	public int RootBone;

	public int[] TargetBones;

	public Transform[] BoneTransforms;

	public void Retarget(Skeleton target)
	{
		if (TargetBones == null || BoneTransforms == null)
		{
			return;
		}
		SkinnedMeshRenderer.rootBone = target.GetTransform(RootBone);
		bool flag = SkeletonDefinition != target.Source;
		for (int i = 0; i < BoneTransforms.Length; i++)
		{
			Transform transform = (flag ? target.GetTransformSlow(SkeletonDefinition.Bones[TargetBones[i]].Name) : target.GetTransform(TargetBones[i]));
			if (transform == null)
			{
				UnityEngine.Debug.LogWarning("Skeleton retarget didn't find a bone: " + SkeletonDefinition.Bones[TargetBones[i]].Name);
			}
			BoneTransforms[i] = transform;
		}
		SkinnedMeshRenderer.bones = BoneTransforms;
		for (int j = 0; j < BoneTransforms.Length; j++)
		{
			BoneTransforms[j] = null;
		}
	}

	public SkinnedMeshRenderer DuplicateAndRetarget(GameObject host, Skeleton target)
	{
		SkinnedMeshRenderer skinnedMeshRenderer = host.AddComponent<SkinnedMeshRenderer>();
		skinnedMeshRenderer.receiveShadows = SkinnedMeshRenderer.receiveShadows;
		skinnedMeshRenderer.skinnedMotionVectors = SkinnedMeshRenderer.skinnedMotionVectors;
		skinnedMeshRenderer.motionVectorGenerationMode = SkinnedMeshRenderer.motionVectorGenerationMode;
		skinnedMeshRenderer.updateWhenOffscreen = SkinnedMeshRenderer.updateWhenOffscreen;
		skinnedMeshRenderer.localBounds = SkinnedMeshRenderer.localBounds;
		skinnedMeshRenderer.shadowCastingMode = SkinnedMeshRenderer.shadowCastingMode;
		skinnedMeshRenderer.sharedMesh = SkinnedMeshRenderer.sharedMesh;
		skinnedMeshRenderer.sharedMaterials = SkinnedMeshRenderer.sharedMaterials;
		skinnedMeshRenderer.rootBone = target.GetTransform(RootBone);
		Transform[] array = new Transform[TargetBones.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = target.GetTransform(TargetBones[i]);
		}
		skinnedMeshRenderer.bones = array;
		return skinnedMeshRenderer;
	}
}
[AddComponentMenu("Facepunch/Skeleton Skin LOD")]
[RequireComponent(typeof(LODGroup))]
[ExecuteInEditMode]
public class SkeletonSkinLod : MonoBehaviour
{
	private static LOD[] emptyLOD = new LOD[1];

	public LODGroup LODGroup;

	[Range(0f, 1f)]
	public float LodRange0 = 0.15f;

	[Range(0f, 1f)]
	public float LodRange1 = 0.05f;

	[Range(0f, 1f)]
	public float LodRange2 = 0.02f;

	[Range(0f, 1f)]
	public float LodRange3 = 0.003f;

	private LOD[] LODs;

	private void Awake()
	{
		SetUpLODArray();
	}

	private void SetUpLODArray()
	{
		if (LODs == null)
		{
			LODs = new LOD[5];
			LODs[0] = new LOD(LodRange0, new Renderer[32]);
			LODs[1] = new LOD(LodRange1, new Renderer[32]);
			LODs[2] = new LOD(LodRange2, new Renderer[32]);
			LODs[3] = new LOD(LodRange3, new Renderer[32]);
			LODs[4] = new LOD(LodRange3 * 0.5f, new Renderer[32]);
		}
	}

	public void AddRenderer(int lod, Renderer r)
	{
		if (lod == -1)
		{
			return;
		}
		if (LODs == null)
		{
			SetUpLODArray();
		}
		for (int i = 0; i < 32 && !(LODs[lod].renderers[i] == r); i++)
		{
			if (LODs[lod].renderers[i] == null)
			{
				LODs[lod].renderers[i] = r;
				if (lod == 4)
				{
					LODs[4].screenRelativeTransitionHeight = LodRange3 * 0.5f;
					LODs[3].screenRelativeTransitionHeight = LodRange2 * 0.5f;
				}
				break;
			}
		}
	}

	public void Clear()
	{
		if (LODGroup == null || LODs == null)
		{
			return;
		}
		if (LODs == null)
		{
			SetUpLODArray();
		}
		for (int i = 0; i < LODs.Length; i++)
		{
			for (int j = 0; j < 32; j++)
			{
				LODs[i].renderers[j] = null;
			}
		}
		LODGroup.SetLODs(emptyLOD);
		LODs[4].screenRelativeTransitionHeight = LodRange3 * 0.999f;
		LODs[3].screenRelativeTransitionHeight = LodRange3;
	}

	public void Rebuild()
	{
		if (!(LODGroup == null))
		{
			if (LODs == null)
			{
				SetUpLODArray();
			}
			LODGroup.SetLODs(LODs);
		}
	}

	public void SoftRebuild()
	{
		if (!(LODGroup == null))
		{
			if (LODs == null)
			{
				SetUpLODArray();
				return;
			}
			LODs[0] = new LOD(LodRange0, LODs[0].renderers);
			LODs[1] = new LOD(LodRange1, LODs[1].renderers);
			LODs[2] = new LOD(LodRange2, LODs[2].renderers);
			LODs[3] = new LOD(LodRange3, LODs[3].renderers);
			LODs[4] = new LOD(LodRange3 * 0.5f, LODs[4].renderers);
			LODGroup.SetLODs(LODs);
		}
	}
}
[Unity.Jobs.DOTSCompilerGenerated]
internal class __JobReflectionRegistrationOutput__17411244733757682161
{
	public static void CreateJobReflectionData()
	{
		try
		{
			IJobExtensions.EarlyJobInit<SkeletonDefinition.FindJob>();
		}
		catch (Exception ex)
		{
			EarlyInitHelpers.JobReflectionDataCreationFailed(ex);
		}
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
	public static void EarlyInit()
	{
		CreateJobReflectionData();
	}
}
