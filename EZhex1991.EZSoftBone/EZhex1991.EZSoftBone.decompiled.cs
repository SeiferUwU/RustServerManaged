using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

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
			FilePathsData = new byte[480]
			{
				0, 0, 0, 1, 0, 0, 0, 69, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 69, 90, 83, 111, 102, 116,
				66, 111, 110, 101, 92, 82, 117, 110, 116, 105,
				109, 101, 92, 65, 116, 116, 114, 105, 98, 117,
				116, 101, 115, 92, 69, 90, 67, 117, 114, 118,
				101, 82, 101, 99, 116, 65, 116, 116, 114, 105,
				98, 117, 116, 101, 46, 99, 115, 0, 0, 0,
				1, 0, 0, 0, 72, 92, 65, 115, 115, 101,
				116, 115, 92, 80, 108, 117, 103, 105, 110, 115,
				92, 69, 90, 83, 111, 102, 116, 66, 111, 110,
				101, 92, 82, 117, 110, 116, 105, 109, 101, 92,
				65, 116, 116, 114, 105, 98, 117, 116, 101, 115,
				92, 69, 90, 78, 101, 115, 116, 101, 100, 69,
				100, 105, 116, 111, 114, 65, 116, 116, 114, 105,
				98, 117, 116, 101, 46, 99, 115, 0, 0, 0,
				3, 0, 0, 0, 48, 92, 65, 115, 115, 101,
				116, 115, 92, 80, 108, 117, 103, 105, 110, 115,
				92, 69, 90, 83, 111, 102, 116, 66, 111, 110,
				101, 92, 82, 117, 110, 116, 105, 109, 101, 92,
				69, 90, 83, 111, 102, 116, 66, 111, 110, 101,
				46, 99, 115, 0, 0, 0, 1, 0, 0, 0,
				60, 92, 65, 115, 115, 101, 116, 115, 92, 80,
				108, 117, 103, 105, 110, 115, 92, 69, 90, 83,
				111, 102, 116, 66, 111, 110, 101, 92, 82, 117,
				110, 116, 105, 109, 101, 92, 69, 90, 83, 111,
				102, 116, 66, 111, 110, 101, 67, 111, 108, 108,
				105, 100, 101, 114, 66, 97, 115, 101, 46, 99,
				115, 0, 0, 0, 1, 0, 0, 0, 64, 92,
				65, 115, 115, 101, 116, 115, 92, 80, 108, 117,
				103, 105, 110, 115, 92, 69, 90, 83, 111, 102,
				116, 66, 111, 110, 101, 92, 82, 117, 110, 116,
				105, 109, 101, 92, 69, 90, 83, 111, 102, 116,
				66, 111, 110, 101, 67, 111, 108, 108, 105, 100,
				101, 114, 67, 121, 108, 105, 110, 100, 101, 114,
				46, 99, 115, 0, 0, 0, 1, 0, 0, 0,
				56, 92, 65, 115, 115, 101, 116, 115, 92, 80,
				108, 117, 103, 105, 110, 115, 92, 69, 90, 83,
				111, 102, 116, 66, 111, 110, 101, 92, 82, 117,
				110, 116, 105, 109, 101, 92, 69, 90, 83, 111,
				102, 116, 66, 111, 110, 101, 77, 97, 116, 101,
				114, 105, 97, 108, 46, 99, 115, 0, 0, 0,
				1, 0, 0, 0, 55, 92, 65, 115, 115, 101,
				116, 115, 92, 80, 108, 117, 103, 105, 110, 115,
				92, 69, 90, 83, 111, 102, 116, 66, 111, 110,
				101, 92, 82, 117, 110, 116, 105, 109, 101, 92,
				69, 90, 83, 111, 102, 116, 66, 111, 110, 101,
				85, 116, 105, 108, 105, 116, 121, 46, 99, 115
			},
			TypesData = new byte[405]
			{
				0, 0, 0, 0, 41, 69, 90, 104, 101, 120,
				49, 57, 57, 49, 46, 69, 90, 83, 111, 102,
				116, 66, 111, 110, 101, 124, 69, 90, 67, 117,
				114, 118, 101, 82, 101, 99, 116, 65, 116, 116,
				114, 105, 98, 117, 116, 101, 0, 0, 0, 0,
				44, 69, 90, 104, 101, 120, 49, 57, 57, 49,
				46, 69, 90, 83, 111, 102, 116, 66, 111, 110,
				101, 124, 69, 90, 78, 101, 115, 116, 101, 100,
				69, 100, 105, 116, 111, 114, 65, 116, 116, 114,
				105, 98, 117, 116, 101, 0, 0, 0, 0, 31,
				69, 90, 104, 101, 120, 49, 57, 57, 49, 46,
				69, 90, 83, 111, 102, 116, 66, 111, 110, 101,
				124, 69, 90, 83, 111, 102, 116, 66, 111, 110,
				101, 0, 0, 0, 0, 41, 69, 90, 104, 101,
				120, 49, 57, 57, 49, 46, 69, 90, 83, 111,
				102, 116, 66, 111, 110, 101, 46, 69, 90, 83,
				111, 102, 116, 66, 111, 110, 101, 124, 66, 111,
				110, 101, 67, 104, 97, 105, 110, 0, 0, 0,
				0, 36, 69, 90, 104, 101, 120, 49, 57, 57,
				49, 46, 69, 90, 83, 111, 102, 116, 66, 111,
				110, 101, 46, 69, 90, 83, 111, 102, 116, 66,
				111, 110, 101, 124, 66, 111, 110, 101, 0, 0,
				0, 0, 43, 69, 90, 104, 101, 120, 49, 57,
				57, 49, 46, 69, 90, 83, 111, 102, 116, 66,
				111, 110, 101, 124, 69, 90, 83, 111, 102, 116,
				66, 111, 110, 101, 67, 111, 108, 108, 105, 100,
				101, 114, 66, 97, 115, 101, 0, 0, 0, 0,
				47, 69, 90, 104, 101, 120, 49, 57, 57, 49,
				46, 69, 90, 83, 111, 102, 116, 66, 111, 110,
				101, 124, 69, 90, 83, 111, 102, 116, 66, 111,
				110, 101, 67, 111, 108, 108, 105, 100, 101, 114,
				67, 121, 108, 105, 110, 100, 101, 114, 0, 0,
				0, 0, 39, 69, 90, 104, 101, 120, 49, 57,
				57, 49, 46, 69, 90, 83, 111, 102, 116, 66,
				111, 110, 101, 124, 69, 90, 83, 111, 102, 116,
				66, 111, 110, 101, 77, 97, 116, 101, 114, 105,
				97, 108, 0, 0, 0, 0, 38, 69, 90, 104,
				101, 120, 49, 57, 57, 49, 46, 69, 90, 83,
				111, 102, 116, 66, 111, 110, 101, 124, 69, 90,
				83, 111, 102, 116, 66, 111, 110, 101, 85, 116,
				105, 108, 105, 116, 121
			},
			TotalFiles = 7,
			TotalTypes = 9,
			IsEditorOnly = false
		};
	}
}
namespace EZhex1991.EZSoftBone;

public class EZCurveRectAttribute : PropertyAttribute
{
	public Rect rect;

	public Color color = Color.green;

	public EZCurveRectAttribute()
	{
		rect = new Rect(0f, 0f, 1f, 1f);
	}

	public EZCurveRectAttribute(Rect rect)
	{
		this.rect = rect;
	}

	public EZCurveRectAttribute(float x, float y, float width, float height)
	{
		rect = new Rect(x, y, width, height);
	}

	public EZCurveRectAttribute(Rect rect, Color color)
	{
		this.rect = rect;
		this.color = color;
	}

	public EZCurveRectAttribute(float x, float y, float width, float height, Color color)
	{
		rect = new Rect(x, y, width, height);
		this.color = color;
	}
}
public class EZNestedEditorAttribute : PropertyAttribute
{
}
public delegate Vector3 CustomForce(float normalizedLength, Transform forceSpace);
public class EZSoftBone : MonoBehaviour, IClientComponent, IPrefabPreProcess
{
	public enum UnificationMode
	{
		None,
		Rooted,
		Unified
	}

	public enum DeltaTimeMode
	{
		DeltaTime,
		SmoothDeltaTime,
		UnscaledDeltaTime,
		Constant
	}

	[Serializable]
	private class BoneChain
	{
		public Bone[] childBones;

		public BoneChain(Transform origin, Transform systemSpace, Transform transform, List<Transform> endBones, int startDepth)
		{
			List<Bone> list = new List<Bone>();
			int num = 0;
			float num2 = 0f;
			Transform transform2 = null;
			do
			{
				float num3 = ((transform2 != null) ? Vector3.Distance(transform2.position, transform.position) : 0f);
				if (num >= startDepth)
				{
					num2 += num3;
				}
				Bone item = new Bone(systemSpace, transform, num++, num2);
				list.Add(item);
				if (transform.childCount == 0)
				{
					break;
				}
				Transform child = transform.GetChild(0);
				if (!child.gameObject.activeSelf)
				{
					break;
				}
				transform2 = transform;
				transform = child;
			}
			while (!endBones.Contains(transform));
			childBones = list.ToArray();
			Bone[] array = childBones;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetTreeLength(num2);
			}
		}

		public void InitializeParents()
		{
			Bone bone = null;
			Bone[] array = childBones;
			foreach (Bone bone2 in array)
			{
				if (bone != null)
				{
					bone.childBone = bone2;
				}
				bone2.parentBone = bone;
				bone = bone2;
			}
		}
	}

	[Serializable]
	private class Bone
	{
		[NonSerialized]
		public Bone parentBone;

		public Vector3 localPosition;

		public Quaternion localRotation;

		[NonSerialized]
		public Bone childBone;

		[NonSerialized]
		public Bone leftBone;

		public Vector3 leftPosition;

		[NonSerialized]
		public Bone rightBone;

		public Vector3 rightPosition;

		public Transform transform;

		public Vector3 worldPosition;

		[NonSerialized]
		public Transform systemSpace;

		public Vector3 systemPosition;

		public int depth;

		public float boneLength;

		public float treeLength;

		public float normalizedLength;

		public float length;

		public float lengthToLeft;

		public float lengthToRight;

		public float radius;

		public float damping;

		public float stiffness;

		public float resistance;

		public float slackness;

		public Vector3 speed;

		public Bone(Transform systemSpace, Transform transform, int depth, float boneLength)
		{
			this.transform = transform;
			this.systemSpace = systemSpace;
			worldPosition = transform.position;
			systemPosition = ((systemSpace == null) ? worldPosition : systemSpace.InverseTransformPoint(worldPosition));
			localPosition = transform.localPosition;
			localRotation = transform.localRotation;
			length = localPosition.magnitude;
			this.depth = depth;
			this.boneLength = boneLength;
		}

		public void SetTreeLength()
		{
			SetTreeLength(treeLength);
		}

		public void SetTreeLength(float treeLength)
		{
			this.treeLength = treeLength;
			normalizedLength = ((treeLength == 0f) ? 0f : (boneLength / treeLength));
		}

		public void SetLeftSibling(Bone left)
		{
			if (left != this && left != rightBone)
			{
				leftBone = left;
				if (leftPosition == default(Vector3))
				{
					leftPosition = transform.InverseTransformPoint(left.worldPosition);
					lengthToLeft = leftPosition.magnitude;
				}
			}
		}

		public void SetRightSibling(Bone right)
		{
			if (right != this && right != leftBone)
			{
				rightBone = right;
				if (rightPosition == default(Vector3))
				{
					rightPosition = transform.InverseTransformPoint(right.worldPosition);
					lengthToRight = rightPosition.magnitude;
				}
			}
		}

		public void Inflate(float baseRadius, AnimationCurve radiusCurve)
		{
			radius = radiusCurve.Evaluate(normalizedLength) * baseRadius;
		}

		public void Inflate(float baseRadius, AnimationCurve radiusCurve, EZSoftBoneMaterial material)
		{
			radius = radiusCurve.Evaluate(normalizedLength) * baseRadius;
			damping = material.GetDamping(normalizedLength);
			stiffness = material.GetStiffness(normalizedLength);
			resistance = material.GetResistance(normalizedLength);
			slackness = material.GetSlackness(normalizedLength);
		}

		public void RevertTransforms(int startDepth)
		{
			if (depth > startDepth)
			{
				transform.localPosition = localPosition;
				transform.localRotation = localRotation;
			}
		}

		public void UpdateTransform(bool siblingRotationConstraints, int startDepth)
		{
			if (depth > startDepth)
			{
				if (childBone != null)
				{
					transform.rotation *= Quaternion.FromToRotation(childBone.localPosition, transform.InverseTransformVector(childBone.worldPosition - worldPosition));
					if (siblingRotationConstraints)
					{
						if (leftBone != null && rightBone != null)
						{
							Vector3 fromDirection = leftPosition;
							Vector3 toDirection = transform.InverseTransformVector(leftBone.worldPosition - worldPosition);
							Quaternion a = Quaternion.FromToRotation(fromDirection, toDirection);
							Vector3 fromDirection2 = rightPosition;
							Vector3 toDirection2 = transform.InverseTransformVector(rightBone.worldPosition - worldPosition);
							Quaternion b = Quaternion.FromToRotation(fromDirection2, toDirection2);
							transform.rotation *= Quaternion.Lerp(a, b, 0.5f);
						}
						else if (leftBone != null)
						{
							Vector3 fromDirection3 = leftPosition;
							Vector3 toDirection3 = transform.InverseTransformVector(leftBone.worldPosition - worldPosition);
							Quaternion quaternion = Quaternion.FromToRotation(fromDirection3, toDirection3);
							transform.rotation *= quaternion;
						}
						else if (rightBone != null)
						{
							Vector3 fromDirection4 = rightPosition;
							Vector3 toDirection4 = transform.InverseTransformVector(rightBone.worldPosition - worldPosition);
							Quaternion quaternion2 = Quaternion.FromToRotation(fromDirection4, toDirection4);
							transform.rotation *= quaternion2;
						}
					}
				}
				transform.position = worldPosition;
			}
			if ((object)systemSpace != null)
			{
				systemPosition = systemSpace.InverseTransformPoint(worldPosition);
			}
		}

		public void SetRestState()
		{
			worldPosition = transform.position;
			systemPosition = ((systemSpace == null) ? worldPosition : systemSpace.InverseTransformPoint(worldPosition));
			speed = Vector3.zero;
		}

		public void UpdateSpace()
		{
			if ((object)systemSpace != null)
			{
				worldPosition = systemSpace.TransformPoint(systemPosition);
			}
		}
	}

	public const float DeltaTime_Min = 1E-06f;

	public const int MaxInstanceCount = 12;

	private static float CurrentFrame;

	private static int InstanceNumber;

	[FormerlySerializedAs("m_RootBones")]
	public List<Transform> rootBones;

	[FormerlySerializedAs("m_EndBones")]
	public List<Transform> endBones;

	[SerializeField]
	[FormerlySerializedAs("m_Material")]
	private EZSoftBoneMaterial _material;

	[FormerlySerializedAs("m_Start_Depth")]
	public int startDepth;

	public int stiffnessDepth;

	[FormerlySerializedAs("m_SiblingConstraints")]
	public UnificationMode siblingConstraints;

	[FormerlySerializedAs("m_LengthUnification")]
	public UnificationMode lengthUnification;

	[FormerlySerializedAs("m_SiblingRotationConstraints")]
	public bool siblingRotationConstraints = true;

	[FormerlySerializedAs("m_ClosedSiblings")]
	public bool closedSiblings;

	[FormerlySerializedAs("m_CollisionLayers")]
	public LayerMask collisionLayers = 1;

	[FormerlySerializedAs("m_Radius")]
	public float radius;

	[FormerlySerializedAs("m_RadiusCurve")]
	[EZCurveRect(0f, 0f, 1f, 1f)]
	public AnimationCurve radiusCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);

	public float deadZoneRadius = 0.1f;

	public float deadZoneExtent = 10f;

	[FormerlySerializedAs("m_DeltaTimeMode")]
	public DeltaTimeMode deltaTimeMode;

	[FormerlySerializedAs("m_ConstantDeltaTime")]
	public float constantDeltaTime = 0.03f;

	[FormerlySerializedAs("m_Iterations")]
	[Range(1f, 10f)]
	public int iterations = 1;

	[FormerlySerializedAs("m_SleepThreshold")]
	public float sleepThreshold = 0.005f;

	[FormerlySerializedAs("m_Gravity")]
	public Vector3 gravity;

	[SerializeField]
	[FormerlySerializedAs("m_SimulateSpace")]
	private Transform _simulateSpace;

	public float globalRadius;

	public Vector3 globalForce;

	public bool collisionEnabled = true;

	[SerializeField]
	private BoneChain[] structures;

	private Vector3 origin;

	private Vector3 prevOrigin;

	private Vector3 originDelta;

	private bool simulationSpaceChanged;

	private Capsule deadZoneCapsule;

	private bool isVisible = true;

	private Vector3 forceDirection;

	public EZSoftBoneMaterial sharedMaterial
	{
		get
		{
			if (_material == null)
			{
				_material = EZSoftBoneMaterial.defaultMaterial;
			}
			return _material;
		}
		set
		{
			_material = value;
		}
	}

	public Transform simulateSpace
	{
		get
		{
			return _simulateSpace;
		}
		set
		{
			if ((object)_simulateSpace != value)
			{
				if (!value)
				{
					value = null;
				}
				_simulateSpace = value;
				simulationSpaceChanged = true;
				UpdateSimulationSpace(_simulateSpace);
			}
		}
	}

	bool IPrefabPreProcess.CanRunDuringBundling => false;

	private void Awake()
	{
		if (_simulateSpace == null)
		{
			_simulateSpace = null;
		}
		InitStructures();
		UpdateSimulationSpace(_simulateSpace);
	}

	private void OnEnable()
	{
		origin = (prevOrigin = base.transform.position);
		originDelta = Vector3.zero;
		collisionEnabled = true;
		SetRestState();
		PushBonesOutOfDeadZone();
	}

	private void OnDisable()
	{
		RevertTransforms(startDepth);
	}

	private void LateUpdate()
	{
		if (!isVisible)
		{
			return;
		}
		if (CurrentFrame != Time.time)
		{
			CurrentFrame = Time.time;
			InstanceNumber = 0;
		}
		if (InstanceNumber++ < 12)
		{
			if ((object)_simulateSpace != null && _simulateSpace == null)
			{
				simulateSpace = null;
			}
			prevOrigin = origin;
			origin = ((_simulateSpace != null) ? (_simulateSpace.transform.rotation * _simulateSpace.InverseTransformPoint(base.transform.position)) : base.transform.position);
			if (simulationSpaceChanged)
			{
				prevOrigin = origin;
				simulationSpaceChanged = false;
			}
			originDelta = origin - prevOrigin;
			switch (deltaTimeMode)
			{
			case DeltaTimeMode.DeltaTime:
				UpdateStructures(Time.deltaTime);
				break;
			case DeltaTimeMode.SmoothDeltaTime:
				UpdateStructures(Time.smoothDeltaTime);
				break;
			case DeltaTimeMode.UnscaledDeltaTime:
				UpdateStructures(Time.unscaledDeltaTime);
				break;
			case DeltaTimeMode.Constant:
				UpdateStructures(constantDeltaTime);
				break;
			}
			UpdateTransforms();
		}
	}

	public void RevertTransforms()
	{
		RevertTransforms(startDepth);
	}

	public void RevertTransforms(int startDepth)
	{
		BoneChain[] array = structures;
		for (int i = 0; i < array.Length; i++)
		{
			Bone[] childBones = array[i].childBones;
			for (int j = 0; j < childBones.Length; j++)
			{
				childBones[j].RevertTransforms(startDepth);
			}
		}
	}

	public void InitStructures(bool force = false)
	{
		if (force)
		{
			structures = null;
		}
		if (structures == null || structures.Length == 0)
		{
			CreateBones();
		}
		SetTreeLength();
		RefreshRadius();
		SetParentBones();
		SetSiblings();
	}

	public void SetRestState()
	{
		BoneChain[] array = structures;
		for (int i = 0; i < array.Length; i++)
		{
			Bone[] childBones = array[i].childBones;
			for (int j = 0; j < childBones.Length; j++)
			{
				childBones[j].SetRestState();
			}
		}
	}

	private void CreateBones()
	{
		structures = Array.Empty<BoneChain>();
		if (rootBones == null || rootBones.Count == 0)
		{
			return;
		}
		List<BoneChain> list = new List<BoneChain>();
		foreach (Transform rootBone in rootBones)
		{
			if (!(rootBone == null))
			{
				BoneChain item = new BoneChain(base.transform, _simulateSpace, rootBone, endBones, startDepth);
				list.Add(item);
			}
		}
		structures = list.ToArray();
	}

	private void SetParentBones()
	{
		BoneChain[] array = structures;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].InitializeParents();
		}
	}

	private void SetSiblings()
	{
		if (siblingConstraints == UnificationMode.None || structures == null || structures.Length == 0)
		{
			return;
		}
		int num = structures.Max((BoneChain c) => c.childBones.Length);
		int num2 = structures.Length;
		for (int num3 = 0; num3 < num; num3++)
		{
			for (int num4 = 0; num4 < num2; num4++)
			{
				Bone bone = null;
				Bone bone2 = null;
				if (num4 > 0 && HasBoneAtDepth(structures[num4 - 1], num3))
				{
					bone = structures[num4 - 1].childBones[num3];
				}
				if (num4 < num2 - 1 && HasBoneAtDepth(structures[num4 + 1], num3))
				{
					bone2 = structures[num4 + 1].childBones[num3];
				}
				Bone bone3 = structures[num4].childBones[num3];
				if (bone != null)
				{
					bone3.SetLeftSibling(bone);
				}
				if (bone2 != null)
				{
					bone3.SetRightSibling(bone2);
				}
			}
			if (closedSiblings && num2 > 2 && HasBoneAtDepth(structures[0], num3) && HasBoneAtDepth(structures[num2 - 1], num3))
			{
				Bone bone4 = structures[0].childBones[num3];
				Bone bone5 = structures[num2 - 1].childBones[num3];
				bone4.SetLeftSibling(bone5);
				bone5.SetRightSibling(bone4);
			}
		}
		static bool HasBoneAtDepth(BoneChain chain, int depth)
		{
			return depth < chain.childBones.Length;
		}
	}

	private void SetTreeLength()
	{
		if (lengthUnification == UnificationMode.Rooted)
		{
			BoneChain[] array = structures;
			for (int i = 0; i < array.Length; i++)
			{
				Bone[] childBones = array[i].childBones;
				for (int j = 0; j < childBones.Length; j++)
				{
					childBones[j].SetTreeLength();
				}
			}
		}
		else
		{
			if (lengthUnification != UnificationMode.Unified)
			{
				return;
			}
			float num = 0f;
			BoneChain[] array = structures;
			for (int i = 0; i < array.Length; i++)
			{
				Bone[] childBones = array[i].childBones;
				foreach (Bone bone in childBones)
				{
					num = Mathf.Max(num, bone.treeLength);
				}
			}
			array = structures;
			for (int i = 0; i < array.Length; i++)
			{
				Bone[] childBones = array[i].childBones;
				for (int j = 0; j < childBones.Length; j++)
				{
					childBones[j].SetTreeLength(num);
				}
			}
		}
	}

	public void RefreshRadius()
	{
		globalRadius = base.transform.lossyScale.Abs().Max() * radius;
		BoneChain[] array = structures;
		for (int i = 0; i < array.Length; i++)
		{
			Bone[] childBones = array[i].childBones;
			for (int j = 0; j < childBones.Length; j++)
			{
				childBones[j].Inflate(globalRadius, radiusCurve);
			}
		}
	}

	private void UpdateStructures(float deltaTime)
	{
		if (deltaTime <= 1E-06f)
		{
			return;
		}
		globalRadius = base.transform.lossyScale.Abs().Max() * radius;
		for (int i = 0; i < structures.Length; i++)
		{
			BoneChain boneChain = structures[i];
			for (int j = 0; j < boneChain.childBones.Length; j++)
			{
				boneChain.childBones[j].Inflate(globalRadius, radiusCurve, sharedMaterial);
			}
			if (_simulateSpace != null)
			{
				for (int k = 0; k < boneChain.childBones.Length; k++)
				{
					boneChain.childBones[k].UpdateSpace();
				}
			}
		}
		globalForce = gravity;
		forceDirection = globalForce.normalized;
		deadZoneCapsule = new Capsule(base.transform.position, deadZoneRadius, deadZoneExtent);
		deltaTime /= (float)iterations;
		for (int l = 0; l < iterations; l++)
		{
			for (int m = 0; m < structures.Length; m++)
			{
				BoneChain boneChain2 = structures[m];
				for (int n = 0; n < boneChain2.childBones.Length; n++)
				{
					Bone bone = boneChain2.childBones[n];
					UpdateBones(bone, deltaTime);
				}
			}
		}
	}

	private void UpdateBones(Bone bone, float deltaTime)
	{
		if (bone.depth > startDepth)
		{
			Vector3 vector2;
			Vector3 vector = (vector2 = bone.worldPosition);
			bone.speed += globalForce * (1f - bone.resistance) / iterations;
			bone.speed *= 1f - bone.damping;
			if (bone.speed.sqrMagnitude > sleepThreshold)
			{
				vector2 += bone.speed * deltaTime;
			}
			Vector3 b;
			if (bone.parentBone.depth > stiffnessDepth)
			{
				Vector3 vector3 = bone.parentBone.worldPosition - bone.parentBone.transform.position;
				b = bone.parentBone.transform.TransformPoint(bone.localPosition) + vector3;
				vector2 = Vector3.Lerp(vector2, b, bone.stiffness / (float)iterations);
			}
			Vector3 normalized = (vector2 - bone.parentBone.worldPosition).normalized;
			b = bone.parentBone.worldPosition + normalized * bone.length;
			float num = 1f;
			if (siblingConstraints != UnificationMode.None && bone.depth >= stiffnessDepth)
			{
				if (bone.leftBone != null)
				{
					Vector3 normalized2 = (vector2 - bone.leftBone.worldPosition).normalized;
					b += (bone.leftBone.worldPosition + normalized2 * bone.lengthToLeft) * 1f;
					num += 1f;
				}
				if (bone.rightBone != null)
				{
					Vector3 normalized3 = (vector2 - bone.rightBone.worldPosition).normalized;
					b += (bone.rightBone.worldPosition + normalized3 * bone.lengthToRight) * 1f;
					num += 1f;
				}
			}
			b /= num;
			vector2 = Vector3.Lerp(b, vector2, bone.slackness / (float)iterations);
			if (collisionEnabled && bone.radius > 0f && deadZoneRadius > 0f)
			{
				Vector3 vector4 = vector + originDelta;
				(vector2 - vector4).ToDirectionAndMagnitude(out var direction, out var magnitude);
				Ray ray = new Ray(vector4, direction);
				if (deadZoneCapsule.Trace(ray, out var hit, bone.radius, magnitude))
				{
					vector2 = ray.origin + ray.direction * hit.distance + hit.normal * (magnitude - hit.distance) * 0.75f;
				}
			}
			bone.speed = (bone.speed + (vector2 - vector) / deltaTime) * 0.5f;
			bone.worldPosition = vector2;
		}
		else
		{
			bone.worldPosition = bone.transform.position;
		}
	}

	private void UpdateTransforms()
	{
		for (int i = 0; i < structures.Length; i++)
		{
			BoneChain boneChain = structures[i];
			for (int j = 0; j < boneChain.childBones.Length; j++)
			{
				boneChain.childBones[j].UpdateTransform(siblingRotationConstraints, startDepth);
			}
		}
	}

	private void UpdateSimulationSpace(Transform transform)
	{
		for (int i = 0; i < structures.Length; i++)
		{
			BoneChain boneChain = structures[i];
			for (int j = 0; j < boneChain.childBones.Length; j++)
			{
				Bone bone = boneChain.childBones[j];
				bone.systemSpace = transform;
				if ((object)_simulateSpace != null)
				{
					bone.systemPosition = _simulateSpace.InverseTransformPoint(bone.worldPosition);
				}
			}
		}
	}

	public void PushBonesOutOfDeadZone()
	{
		Vector3 position = base.transform.position;
		BoneChain[] array = structures;
		for (int i = 0; i < array.Length; i++)
		{
			Bone[] childBones = array[i].childBones;
			foreach (Bone bone in childBones)
			{
				Vector3 worldPosition = bone.worldPosition;
				Vector3 vector = position.WithY(worldPosition.y);
				(worldPosition - vector).ToDirectionAndMagnitude(out var direction, out var magnitude);
				float num = deadZoneRadius + bone.radius;
				if (magnitude < num)
				{
					Vector3 worldPosition2 = vector + direction * num * 1.5f;
					bone.worldPosition = worldPosition2;
					bone.systemPosition = ((bone.systemSpace == null) ? bone.worldPosition : bone.systemSpace.InverseTransformPoint(bone.worldPosition));
				}
			}
		}
	}

	public void PlayerPreviewVisibility(bool isVisible)
	{
		this.isVisible = isVisible;
	}

	public void PreProcess(IPrefabProcessor preProcess, GameObject rootObj, string name, bool serverside, bool clientside, bool bundling)
	{
		InitStructures(force: true);
	}
}
public abstract class EZSoftBoneColliderBase : MonoBehaviour
{
	public static ListHashSet<EZSoftBoneColliderBase> EnabledColliders = new ListHashSet<EZSoftBoneColliderBase>();

	protected virtual void OnEnable()
	{
		if (!EnabledColliders.Contains(this))
		{
			EnabledColliders.Add(this);
		}
	}

	protected virtual void OnDisable()
	{
		EnabledColliders.Remove(this);
	}

	public abstract void Collide(ref Vector3 position, float spacing);
}
public class EZSoftBoneColliderCylinder : EZSoftBoneColliderBase
{
	[SerializeField]
	private float m_Margin;

	[SerializeField]
	private bool m_InsideMode;

	public float margin
	{
		get
		{
			return m_Margin;
		}
		set
		{
			m_Margin = value;
		}
	}

	public bool insideMode
	{
		get
		{
			return m_InsideMode;
		}
		set
		{
			m_InsideMode = value;
		}
	}

	public override void Collide(ref Vector3 position, float spacing)
	{
		if (insideMode)
		{
			EZSoftBoneUtility.PointInsideCylinder(ref position, base.transform, spacing + margin);
		}
		else
		{
			EZSoftBoneUtility.PointOutsideCylinder(ref position, base.transform, spacing + margin);
		}
	}
}
[CreateAssetMenu(fileName = "SBMat", menuName = "EZSoftBone/SBMaterial")]
public class EZSoftBoneMaterial : ScriptableObject
{
	[SerializeField]
	[Range(0f, 1f)]
	private float m_Damping = 0.2f;

	[SerializeField]
	[EZCurveRect(0f, 0f, 1f, 1f)]
	private AnimationCurve m_DampingCurve = AnimationCurve.EaseInOut(0f, 0.5f, 1f, 1f);

	[SerializeField]
	[Range(0f, 1f)]
	private float m_Stiffness = 0.1f;

	[SerializeField]
	[EZCurveRect(0f, 0f, 1f, 1f)]
	private AnimationCurve m_StiffnessCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);

	[SerializeField]
	[Range(0f, 1f)]
	private float m_Resistance = 0.9f;

	[SerializeField]
	[EZCurveRect(0f, 0f, 1f, 1f)]
	private AnimationCurve m_ResistanceCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);

	[SerializeField]
	[Range(0f, 1f)]
	private float m_Slackness = 0.1f;

	[SerializeField]
	[EZCurveRect(0f, 0f, 1f, 1f)]
	private AnimationCurve m_SlacknessCurve = AnimationCurve.Linear(0f, 1f, 1f, 0.8f);

	private static EZSoftBoneMaterial m_DefaultMaterial;

	public float damping
	{
		get
		{
			return m_Damping;
		}
		set
		{
			m_Damping = Mathf.Clamp01(value);
		}
	}

	public AnimationCurve dampingCurve => m_DampingCurve;

	public float stiffness
	{
		get
		{
			return m_Stiffness;
		}
		set
		{
			m_Stiffness = Mathf.Clamp01(value);
		}
	}

	public AnimationCurve stiffnessCurve => m_StiffnessCurve;

	public float resistance
	{
		get
		{
			return m_Resistance;
		}
		set
		{
			m_Resistance = Mathf.Clamp01(value);
		}
	}

	public AnimationCurve resistanceCurve => m_ResistanceCurve;

	public float slackness
	{
		get
		{
			return m_Slackness;
		}
		set
		{
			m_Slackness = Mathf.Clamp01(value);
		}
	}

	public AnimationCurve slacknessCurve => m_SlacknessCurve;

	public static EZSoftBoneMaterial defaultMaterial
	{
		get
		{
			if (m_DefaultMaterial == null)
			{
				m_DefaultMaterial = ScriptableObject.CreateInstance<EZSoftBoneMaterial>();
			}
			m_DefaultMaterial.name = "SBMat_Default";
			return m_DefaultMaterial;
		}
	}

	public float GetDamping(float t)
	{
		return damping * dampingCurve.Evaluate(t);
	}

	public float GetStiffness(float t)
	{
		return stiffness * stiffnessCurve.Evaluate(t);
	}

	public float GetResistance(float t)
	{
		return resistance * resistanceCurve.Evaluate(t);
	}

	public float GetSlackness(float t)
	{
		return slackness * slacknessCurve.Evaluate(t);
	}
}
public static class EZSoftBoneUtility
{
	internal static Vector3 Abs(this Vector3 v)
	{
		return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
	}

	public static float Max(this Vector3 v)
	{
		return Mathf.Max(v.x, Mathf.Max(v.y, v.z));
	}

	public static bool Contains(this LayerMask mask, int layer)
	{
		return ((int)mask | (1 << layer)) == (int)mask;
	}

	public static void GetCapsuleParams(CapsuleCollider collider, out Vector3 center0, out Vector3 center1, out float radius)
	{
		Vector3 vector = collider.transform.lossyScale.Abs();
		radius = collider.radius;
		center0 = (center1 = collider.center);
		float num = collider.height * 0.5f;
		switch (collider.direction)
		{
		case 0:
			radius *= Mathf.Max(vector.y, vector.z);
			num = Mathf.Max(0f, num - radius / vector.x);
			center0.x -= num;
			center1.x += num;
			break;
		case 1:
			radius *= Mathf.Max(vector.x, vector.z);
			num = Mathf.Max(0f, num - radius / vector.y);
			center0.y -= num;
			center1.y += num;
			break;
		case 2:
			radius *= Mathf.Max(vector.x, vector.y);
			num = Mathf.Max(0f, num - radius / vector.z);
			center0.z -= num;
			center1.z += num;
			break;
		}
		center0 = collider.transform.TransformPoint(center0);
		center1 = collider.transform.TransformPoint(center1);
	}

	public static void GetCylinderParams(Transform transform, out Vector3 center, out Vector3 direction, out float radius, out float height)
	{
		Vector3 vector = transform.lossyScale.Abs();
		center = transform.position;
		direction = transform.up;
		radius = Mathf.Max(vector.x, vector.z) * 0.5f;
		height = vector.y;
	}

	public static void PointOutsideSphere(ref Vector3 position, SphereCollider collider, float spacing)
	{
		Vector3 v = collider.transform.lossyScale.Abs();
		float num = collider.radius * v.Max();
		PointOutsideSphere(ref position, collider.transform.TransformPoint(collider.center), num + spacing);
	}

	private static void PointOutsideSphere(ref Vector3 position, Vector3 spherePosition, float radius)
	{
		Vector3 vector = position - spherePosition;
		if (vector.magnitude < radius)
		{
			position = spherePosition + vector.normalized * radius;
		}
	}

	public static void PointInsideSphere(ref Vector3 position, SphereCollider collider, float spacing)
	{
		PointInsideSphere(ref position, collider.transform.TransformPoint(collider.center), collider.radius - spacing);
	}

	private static void PointInsideSphere(ref Vector3 position, Vector3 spherePosition, float radius)
	{
		Vector3 vector = position - spherePosition;
		if (vector.magnitude > radius)
		{
			position = spherePosition + vector.normalized * radius;
		}
	}

	public static void PointOutsideCapsule(ref Vector3 position, CapsuleCollider collider, float spacing)
	{
		GetCapsuleParams(collider, out var center, out var center2, out var radius);
		PointOutsideCapsule(ref position, center, center2, radius + spacing);
	}

	public static void PointOutsideCapsule(ref Vector3 position, Vector3 center0, Vector3 center1, float radius)
	{
		Vector3 vector = center1 - center0;
		Vector3 vector2 = position - center0;
		float num = Vector3.Dot(vector, vector2);
		if (num <= 0f)
		{
			PointOutsideSphere(ref position, center0, radius);
			return;
		}
		if (num >= vector.sqrMagnitude)
		{
			PointOutsideSphere(ref position, center1, radius);
			return;
		}
		Vector3 vector3 = vector2 - Vector3.Project(vector2, vector);
		float num2 = radius - vector3.magnitude;
		if (num2 > 0f)
		{
			position += vector3.normalized * num2;
		}
	}

	public static void PointInsideCapsule(ref Vector3 position, CapsuleCollider collider, float spacing)
	{
		GetCapsuleParams(collider, out var center, out var center2, out var radius);
		PointInsideCapsule(ref position, center, center2, radius - spacing);
	}

	private static void PointInsideCapsule(ref Vector3 position, Vector3 center0, Vector3 center1, float radius)
	{
		Vector3 vector = center1 - center0;
		Vector3 vector2 = position - center0;
		float num = Vector3.Dot(vector, vector2);
		if (num <= 0f)
		{
			PointInsideSphere(ref position, center0, radius);
			return;
		}
		if (num >= vector.sqrMagnitude)
		{
			PointInsideSphere(ref position, center1, radius);
			return;
		}
		Vector3 vector3 = vector2 - Vector3.Project(vector2, vector);
		float num2 = radius - vector3.magnitude;
		if (num2 < 0f)
		{
			position += vector3.normalized * num2;
		}
	}

	public static void PointOutsideCylinder(ref Vector3 position, Transform transform, float spacing)
	{
		GetCylinderParams(transform, out var center, out var direction, out var radius, out var height);
		PointOutsideCylinder(ref position, center, direction, radius + spacing, height + spacing);
	}

	private static void PointOutsideCylinder(ref Vector3 position, Vector3 center, Vector3 direction, float radius, float height)
	{
		Vector3 vector = position - center;
		Vector3 vector2 = Vector3.Project(vector, direction);
		float num = height - vector2.magnitude;
		if (!(num > 0f))
		{
			return;
		}
		Vector3 vector3 = vector - vector2;
		float num2 = radius - vector3.magnitude;
		if (num2 > 0f)
		{
			if (num2 < num)
			{
				position += vector3.normalized * num2;
			}
			else
			{
				position += vector2.normalized * num;
			}
		}
	}

	public static void PointInsideCylinder(ref Vector3 position, Transform transform, float spacing)
	{
		GetCylinderParams(transform, out var center, out var direction, out var radius, out var height);
		PointInsideCylinder(ref position, center, direction, radius - spacing, height - spacing);
	}

	private static void PointInsideCylinder(ref Vector3 position, Vector3 center, Vector3 direction, float radius, float height)
	{
		Vector3 vector = position - center;
		Vector3 vector2 = Vector3.Project(vector, direction);
		float num = height - vector2.magnitude;
		Vector3 vector3 = vector - vector2;
		float num2 = radius - vector3.magnitude;
		if (num < 0f || num2 < 0f)
		{
			if (num2 < num)
			{
				position += vector3.normalized * num2;
			}
			else
			{
				position += vector2.normalized * num;
			}
		}
	}

	public static void PointOutsideBox(ref Vector3 position, BoxCollider collider, float spacing)
	{
		Vector3 position2 = collider.transform.InverseTransformPoint(position) - collider.center;
		PointOutsideBox(ref position2, collider.size.Abs() / 2f + collider.transform.InverseTransformVector(Vector3.one * spacing).Abs());
		position = collider.transform.TransformPoint(collider.center + position2);
	}

	public static void PointOutsideBox(ref Vector3 position, Vector3 boxSize)
	{
		Vector3 vector = position.Abs();
		if (!(vector.x < boxSize.x) || !(vector.y < boxSize.y) || !(vector.z < boxSize.z))
		{
			return;
		}
		Vector3 vector2 = (vector - boxSize).Abs();
		if (vector2.x < vector2.y)
		{
			if (vector2.x < vector2.z)
			{
				position.x = Mathf.Sign(position.x) * boxSize.x;
			}
			else
			{
				position.z = Mathf.Sign(position.z) * boxSize.z;
			}
		}
		else if (vector2.y < vector2.z)
		{
			position.y = Mathf.Sign(position.y) * boxSize.y;
		}
		else
		{
			position.z = Mathf.Sign(position.z) * boxSize.z;
		}
	}

	public static void PointInsideBox(ref Vector3 position, BoxCollider collider, float spacing)
	{
		Vector3 position2 = collider.transform.InverseTransformPoint(position) - collider.center;
		PointInsideBox(ref position2, collider.size.Abs() / 2f - collider.transform.InverseTransformVector(Vector3.one * spacing).Abs());
		position = collider.transform.TransformPoint(collider.center + position2);
	}

	private static void PointInsideBox(ref Vector3 position, Vector3 boxSize)
	{
		Vector3 vector = position.Abs();
		if (vector.x > boxSize.x)
		{
			position.x = Mathf.Sign(position.x) * boxSize.x;
		}
		if (vector.y > boxSize.y)
		{
			position.y = Mathf.Sign(position.y) * boxSize.y;
		}
		if (vector.z > boxSize.z)
		{
			position.z = Mathf.Sign(position.z) * boxSize.z;
		}
	}

	public static void PointOutsideCollider(ref Vector3 position, Collider collider, float spacing)
	{
		Vector3 vector = collider.ClosestPoint(position);
		if (position == vector)
		{
			Vector3 vector2 = position - collider.bounds.center;
			UnityEngine.Debug.DrawLine(collider.bounds.center, vector, Color.red);
			position = vector + vector2.normalized * spacing;
			return;
		}
		Vector3 vector3 = position - vector;
		if (vector3.magnitude < spacing)
		{
			position = vector + vector3.normalized * spacing;
		}
	}

	public static void DrawGizmosArrow(Vector3 startPoint, Vector3 direction, float halfWidth, Vector3 normal)
	{
		Vector3 vector = Vector3.Cross(direction, normal).normalized * halfWidth;
		Vector3[] array = new Vector3[8];
		array[0] = startPoint + vector * 0.5f;
		array[1] = array[0] + direction * 0.5f;
		array[2] = array[1] + vector * 0.5f;
		array[3] = startPoint + direction;
		array[4] = startPoint - vector + direction * 0.5f;
		array[5] = array[4] + vector * 0.5f;
		array[6] = startPoint - vector * 0.5f;
		array[7] = array[0];
		DrawGizmosPolyLine(array);
	}

	public static void DrawGizmosPolyLine(params Vector3[] vertices)
	{
		for (int i = 0; i < vertices.Length - 1; i++)
		{
			Gizmos.DrawLine(vertices[i], vertices[i + 1]);
		}
	}
}
