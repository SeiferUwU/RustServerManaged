using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using Facepunch.BurstCloth.Jobs;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.Serialization;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: InternalsVisibleTo("Facepunch.BurstCloth.Editor")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
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
			FilePathsData = new byte[974]
			{
				0, 0, 0, 1, 0, 0, 0, 79, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 70, 97, 99, 101, 112, 117,
				110, 99, 104, 46, 66, 117, 114, 115, 116, 67,
				108, 111, 116, 104, 92, 65, 116, 116, 114, 105,
				98, 117, 116, 101, 115, 92, 66, 117, 114, 115,
				116, 67, 108, 111, 116, 104, 67, 117, 114, 118,
				101, 82, 101, 99, 116, 65, 116, 116, 114, 105,
				98, 117, 116, 101, 46, 99, 115, 0, 0, 0,
				4, 0, 0, 0, 50, 92, 65, 115, 115, 101,
				116, 115, 92, 80, 108, 117, 103, 105, 110, 115,
				92, 70, 97, 99, 101, 112, 117, 110, 99, 104,
				46, 66, 117, 114, 115, 116, 67, 108, 111, 116,
				104, 92, 66, 117, 114, 115, 116, 67, 108, 111,
				116, 104, 46, 99, 115, 0, 0, 0, 1, 0,
				0, 0, 60, 92, 65, 115, 115, 101, 116, 115,
				92, 80, 108, 117, 103, 105, 110, 115, 92, 70,
				97, 99, 101, 112, 117, 110, 99, 104, 46, 66,
				117, 114, 115, 116, 67, 108, 111, 116, 104, 92,
				66, 117, 114, 115, 116, 67, 108, 111, 116, 104,
				67, 111, 110, 115, 116, 114, 97, 105, 110, 116,
				46, 99, 115, 0, 0, 0, 1, 0, 0, 0,
				57, 92, 65, 115, 115, 101, 116, 115, 92, 80,
				108, 117, 103, 105, 110, 115, 92, 70, 97, 99,
				101, 112, 117, 110, 99, 104, 46, 66, 117, 114,
				115, 116, 67, 108, 111, 116, 104, 92, 66, 117,
				114, 115, 116, 67, 108, 111, 116, 104, 77, 97,
				110, 97, 103, 101, 114, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 58, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 70, 97, 99, 101, 112, 117, 110, 99,
				104, 46, 66, 117, 114, 115, 116, 67, 108, 111,
				116, 104, 92, 66, 117, 114, 115, 116, 67, 108,
				111, 116, 104, 77, 97, 116, 101, 114, 105, 97,
				108, 46, 99, 115, 0, 0, 0, 2, 0, 0,
				0, 53, 92, 65, 115, 115, 101, 116, 115, 92,
				80, 108, 117, 103, 105, 110, 115, 92, 70, 97,
				99, 101, 112, 117, 110, 99, 104, 46, 66, 117,
				114, 115, 116, 67, 108, 111, 116, 104, 92, 74,
				111, 98, 115, 92, 66, 111, 110, 101, 68, 97,
				116, 97, 46, 99, 115, 0, 0, 0, 1, 0,
				0, 0, 54, 92, 65, 115, 115, 101, 116, 115,
				92, 80, 108, 117, 103, 105, 110, 115, 92, 70,
				97, 99, 101, 112, 117, 110, 99, 104, 46, 66,
				117, 114, 115, 116, 67, 108, 111, 116, 104, 92,
				74, 111, 98, 115, 92, 66, 111, 110, 101, 83,
				116, 97, 116, 101, 46, 99, 115, 0, 0, 0,
				1, 0, 0, 0, 57, 92, 65, 115, 115, 101,
				116, 115, 92, 80, 108, 117, 103, 105, 110, 115,
				92, 70, 97, 99, 101, 112, 117, 110, 99, 104,
				46, 66, 117, 114, 115, 116, 67, 108, 111, 116,
				104, 92, 74, 111, 98, 115, 92, 67, 111, 108,
				108, 105, 100, 101, 114, 68, 97, 116, 97, 46,
				99, 115, 0, 0, 0, 1, 0, 0, 0, 58,
				92, 65, 115, 115, 101, 116, 115, 92, 80, 108,
				117, 103, 105, 110, 115, 92, 70, 97, 99, 101,
				112, 117, 110, 99, 104, 46, 66, 117, 114, 115,
				116, 67, 108, 111, 116, 104, 92, 74, 111, 98,
				115, 92, 67, 111, 108, 108, 105, 100, 101, 114,
				83, 116, 97, 116, 101, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 67, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 70, 97, 99, 101, 112, 117, 110, 99,
				104, 46, 66, 117, 114, 115, 116, 67, 108, 111,
				116, 104, 92, 74, 111, 98, 115, 92, 73, 110,
				105, 116, 105, 97, 108, 105, 122, 101, 66, 111,
				110, 101, 83, 116, 97, 116, 101, 74, 111, 98,
				46, 99, 115, 0, 0, 0, 1, 0, 0, 0,
				71, 92, 65, 115, 115, 101, 116, 115, 92, 80,
				108, 117, 103, 105, 110, 115, 92, 70, 97, 99,
				101, 112, 117, 110, 99, 104, 46, 66, 117, 114,
				115, 116, 67, 108, 111, 116, 104, 92, 74, 111,
				98, 115, 92, 73, 110, 105, 116, 105, 97, 108,
				105, 122, 101, 67, 111, 108, 108, 105, 100, 101,
				114, 83, 116, 97, 116, 101, 74, 111, 98, 46,
				99, 115, 0, 0, 0, 3, 0, 0, 0, 65,
				92, 65, 115, 115, 101, 116, 115, 92, 80, 108,
				117, 103, 105, 110, 115, 92, 70, 97, 99, 101,
				112, 117, 110, 99, 104, 46, 66, 117, 114, 115,
				116, 67, 108, 111, 116, 104, 92, 74, 111, 98,
				115, 92, 83, 105, 109, 117, 108, 97, 116, 101,
				80, 111, 115, 105, 116, 105, 111, 110, 115, 74,
				111, 98, 46, 99, 115, 0, 0, 0, 1, 0,
				0, 0, 69, 92, 65, 115, 115, 101, 116, 115,
				92, 80, 108, 117, 103, 105, 110, 115, 92, 70,
				97, 99, 101, 112, 117, 110, 99, 104, 46, 66,
				117, 114, 115, 116, 67, 108, 111, 116, 104, 92,
				74, 111, 98, 115, 92, 85, 112, 100, 97, 116,
				101, 83, 107, 105, 110, 67, 111, 110, 115, 116,
				114, 97, 105, 110, 116, 115, 74, 111, 98, 46,
				99, 115, 0, 0, 0, 1, 0, 0, 0, 64,
				92, 65, 115, 115, 101, 116, 115, 92, 80, 108,
				117, 103, 105, 110, 115, 92, 70, 97, 99, 101,
				112, 117, 110, 99, 104, 46, 66, 117, 114, 115,
				116, 67, 108, 111, 116, 104, 92, 74, 111, 98,
				115, 92, 85, 112, 100, 97, 116, 101, 84, 114,
				97, 110, 115, 102, 111, 114, 109, 115, 74, 111,
				98, 46, 99, 115
			},
			TypesData = new byte[924]
			{
				0, 0, 0, 0, 49, 70, 97, 99, 101, 112,
				117, 110, 99, 104, 46, 66, 117, 114, 115, 116,
				67, 108, 111, 116, 104, 124, 66, 117, 114, 115,
				116, 67, 108, 111, 116, 104, 67, 117, 114, 118,
				101, 82, 101, 99, 116, 65, 116, 116, 114, 105,
				98, 117, 116, 101, 0, 0, 0, 0, 33, 70,
				97, 99, 101, 112, 117, 110, 99, 104, 46, 66,
				117, 114, 115, 116, 67, 108, 111, 116, 104, 124,
				83, 112, 104, 101, 114, 101, 80, 97, 114, 97,
				109, 115, 0, 0, 0, 0, 35, 70, 97, 99,
				101, 112, 117, 110, 99, 104, 46, 66, 117, 114,
				115, 116, 67, 108, 111, 116, 104, 124, 83, 107,
				105, 110, 67, 111, 110, 115, 116, 114, 97, 105,
				110, 116, 0, 0, 0, 0, 31, 70, 97, 99,
				101, 112, 117, 110, 99, 104, 46, 66, 117, 114,
				115, 116, 67, 108, 111, 116, 104, 124, 66, 117,
				114, 115, 116, 67, 108, 111, 116, 104, 0, 0,
				0, 0, 37, 70, 97, 99, 101, 112, 117, 110,
				99, 104, 46, 66, 117, 114, 115, 116, 67, 108,
				111, 116, 104, 46, 66, 117, 114, 115, 116, 67,
				108, 111, 116, 104, 124, 67, 104, 97, 105, 110,
				0, 0, 0, 0, 41, 70, 97, 99, 101, 112,
				117, 110, 99, 104, 46, 66, 117, 114, 115, 116,
				67, 108, 111, 116, 104, 124, 66, 117, 114, 115,
				116, 67, 108, 111, 116, 104, 67, 111, 110, 115,
				116, 114, 97, 105, 110, 116, 0, 0, 0, 0,
				38, 70, 97, 99, 101, 112, 117, 110, 99, 104,
				46, 66, 117, 114, 115, 116, 67, 108, 111, 116,
				104, 124, 66, 117, 114, 115, 116, 67, 108, 111,
				116, 104, 77, 97, 110, 97, 103, 101, 114, 0,
				0, 0, 0, 39, 70, 97, 99, 101, 112, 117,
				110, 99, 104, 46, 66, 117, 114, 115, 116, 67,
				108, 111, 116, 104, 124, 66, 117, 114, 115, 116,
				67, 108, 111, 116, 104, 77, 97, 116, 101, 114,
				105, 97, 108, 0, 0, 0, 0, 34, 70, 97,
				99, 101, 112, 117, 110, 99, 104, 46, 66, 117,
				114, 115, 116, 67, 108, 111, 116, 104, 46, 74,
				111, 98, 115, 124, 66, 111, 110, 101, 68, 97,
				116, 97, 0, 0, 0, 0, 44, 70, 97, 99,
				101, 112, 117, 110, 99, 104, 46, 66, 117, 114,
				115, 116, 67, 108, 111, 116, 104, 46, 74, 111,
				98, 115, 124, 82, 111, 116, 97, 116, 105, 111,
				110, 67, 111, 110, 115, 116, 114, 97, 105, 110,
				116, 0, 0, 0, 0, 35, 70, 97, 99, 101,
				112, 117, 110, 99, 104, 46, 66, 117, 114, 115,
				116, 67, 108, 111, 116, 104, 46, 74, 111, 98,
				115, 124, 66, 111, 110, 101, 83, 116, 97, 116,
				101, 0, 0, 0, 0, 44, 70, 97, 99, 101,
				112, 117, 110, 99, 104, 46, 66, 117, 114, 115,
				116, 67, 108, 111, 116, 104, 46, 74, 111, 98,
				115, 124, 83, 112, 104, 101, 114, 101, 67, 111,
				108, 108, 105, 100, 101, 114, 68, 97, 116, 97,
				0, 0, 0, 0, 45, 70, 97, 99, 101, 112,
				117, 110, 99, 104, 46, 66, 117, 114, 115, 116,
				67, 108, 111, 116, 104, 46, 74, 111, 98, 115,
				124, 83, 112, 104, 101, 114, 101, 67, 111, 108,
				108, 105, 100, 101, 114, 83, 116, 97, 116, 101,
				0, 0, 0, 0, 48, 70, 97, 99, 101, 112,
				117, 110, 99, 104, 46, 66, 117, 114, 115, 116,
				67, 108, 111, 116, 104, 46, 74, 111, 98, 115,
				124, 73, 110, 105, 116, 105, 97, 108, 105, 122,
				101, 66, 111, 110, 101, 83, 116, 97, 116, 101,
				74, 111, 98, 0, 0, 0, 0, 58, 70, 97,
				99, 101, 112, 117, 110, 99, 104, 46, 66, 117,
				114, 115, 116, 67, 108, 111, 116, 104, 46, 74,
				111, 98, 115, 124, 73, 110, 105, 116, 105, 97,
				108, 105, 122, 101, 83, 112, 104, 101, 114, 101,
				67, 111, 108, 108, 105, 100, 101, 114, 83, 116,
				97, 116, 101, 74, 111, 98, 0, 0, 0, 0,
				46, 70, 97, 99, 101, 112, 117, 110, 99, 104,
				46, 66, 117, 114, 115, 116, 67, 108, 111, 116,
				104, 46, 74, 111, 98, 115, 124, 83, 105, 109,
				117, 108, 97, 116, 101, 80, 111, 115, 105, 116,
				105, 111, 110, 115, 74, 111, 98, 0, 0, 0,
				0, 36, 70, 97, 99, 101, 112, 117, 110, 99,
				104, 46, 66, 117, 114, 115, 116, 67, 108, 111,
				116, 104, 46, 74, 111, 98, 115, 46, 124, 76,
				101, 114, 112, 83, 116, 97, 116, 101, 0, 0,
				0, 0, 36, 70, 97, 99, 101, 112, 117, 110,
				99, 104, 46, 66, 117, 114, 115, 116, 67, 108,
				111, 116, 104, 46, 74, 111, 98, 115, 46, 124,
				67, 111, 108, 108, 105, 115, 105, 111, 110, 0,
				0, 0, 0, 50, 70, 97, 99, 101, 112, 117,
				110, 99, 104, 46, 66, 117, 114, 115, 116, 67,
				108, 111, 116, 104, 46, 74, 111, 98, 115, 124,
				85, 112, 100, 97, 116, 101, 83, 107, 105, 110,
				67, 111, 110, 115, 116, 114, 97, 105, 110, 116,
				115, 74, 111, 98, 0, 0, 0, 0, 45, 70,
				97, 99, 101, 112, 117, 110, 99, 104, 46, 66,
				117, 114, 115, 116, 67, 108, 111, 116, 104, 46,
				74, 111, 98, 115, 124, 85, 112, 100, 97, 116,
				101, 84, 114, 97, 110, 115, 102, 111, 114, 109,
				115, 74, 111, 98
			},
			TotalFiles = 14,
			TotalTypes = 20,
			IsEditorOnly = false
		};
	}
}
namespace Facepunch.BurstCloth
{
	public class BurstClothCurveRectAttribute : PropertyAttribute
	{
		public readonly Rect Rect;

		public readonly Color Color = Color.green;

		public BurstClothCurveRectAttribute()
		{
			Rect = new Rect(0f, 0f, 1f, 1f);
		}

		public BurstClothCurveRectAttribute(Rect rect)
		{
			Rect = rect;
		}

		public BurstClothCurveRectAttribute(float x, float y, float width, float height)
		{
			Rect = new Rect(x, y, width, height);
		}

		public BurstClothCurveRectAttribute(Rect rect, Color color)
		{
			Rect = rect;
			Color = color;
		}

		public BurstClothCurveRectAttribute(float x, float y, float width, float height, Color color)
		{
			Rect = new Rect(x, y, width, height);
			Color = color;
		}
	}
	public struct SphereParams
	{
		public Transform Transform;

		public Vector3 Point;

		public float Radius;
	}
	[Serializable]
	internal struct SkinConstraint
	{
		public float3 WorldSkinPosition;

		public float3 WorldSkinNormal;

		public float3 WorldEscapeNormal;

		public float3 LocalSkinPosition;

		public float3 LocalSkinNormal;

		public float3 LocalEscapeNormal;

		public float BackstopRadius;

		public float BackstopInset;
	}
	public class BurstCloth : MonoBehaviour
	{
		[Serializable]
		internal struct Chain
		{
			public List<Transform> Transforms;
		}

		internal const int TickRateMultiplier = 30;

		internal const int MaximumTickRate = 120;

		private static readonly List<SphereParams> SphereParamsShared = new List<SphereParams>(32);

		[Header("Structure")]
		public Transform[] RootBones;

		[Tooltip("You only need to set this value if this is a ViewModel prefab")]
		public Transform ViewModelRootTransform;

		public bool SiblingConstraints = true;

		[Range(0f, 1f)]
		public float LengthModifier = 1f;

		[Header("Simulation")]
		public BurstClothMaterial Material;

		public Vector3 Gravity = new Vector3(0f, -10f, 0f);

		public Transform SimulationSpace;

		[Range(0f, 1f)]
		public float RootMotionStrength = 1f;

		public float MaxOriginDelta = 1f;

		[Tooltip("This will be used as the default for all bones without a length constraint given by BurstClothConstraint")]
		public Vector2 DefaultLengthConstraint = new Vector2(0f, 1f);

		[Tooltip("If the controller becomes culled, bones will return to their original rigged positions")]
		public bool ResetBonesOnDisable;

		[Header("Collision")]
		public bool EnableCollisions;

		public float CollisionRadius;

		public float CollisionDirectionAdjustment;

		public float CollisionFilteringScale = 3f;

		[BurstClothCurveRect(0f, 0f, 1f, 1f)]
		public AnimationCurve CollisionRadiusCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);

		[Header("Performance")]
		public bool EnableSimulation = true;

		[Range(30f, 120f)]
		public int TickRate = 120;

		[Range(1f, 16f)]
		public int MaxTicksPerFrame = 4;

		[Range(1f, 16f)]
		public int ConstraintIterationCount = 2;

		[SerializeField]
		[HideInInspector]
		internal List<Chain> _chains;

		[SerializeField]
		[HideInInspector]
		internal List<BoneData> _boneDataOriginal;

		[SerializeField]
		[HideInInspector]
		internal List<RotationConstraint> _rotationConstraintsDataOriginal;

		private Vector3 _origin;

		private Quaternion _rotation;

		private Quaternion _prevRotation;

		private Quaternion _rotationDelta;

		private Vector3 _up;

		private Vector3 _simulationSpaceDelta;

		private Transform _prevSimulationSpace;

		private Vector3? _prevSimulationOrigin;

		private Vector3 _originDelta;

		private Vector3? _prevOrigin;

		private bool _originDirty;

		private float _accumulator;

		private int _boneCount;

		private int _maxBoneDepth;

		private Dictionary<Transform, int> _boneToIndex;

		private TransformAccessArray _boneTransforms;

		private NativeArray<BoneData> _boneData;

		private NativeArray<BoneState> _boneState;

		private NativeArray<RotationConstraint> _rotationConstraints;

		[SerializeField]
		[HideInInspector]
		private List<string> _skinConstraintTargetsIds;

		private Transform[] _skinConstraintTargetCache;

		[SerializeField]
		[HideInInspector]
		internal List<SkinConstraint> _skinConstraintsOriginal;

		private NativeArray<SkinConstraint> _skinConstraints;

		private bool _skinAccessArrayDirty;

		private TransformAccessArray _skinConstraintsTransformAccessArray;

		private TransformAccessArray _sphereColliderTransforms;

		private NativeArray<SphereColliderData> _sphereColliderData;

		private NativeArray<SphereColliderState> _sphereColliderState;

		protected virtual Transform GetSkeletonBone(string boneId)
		{
			return null;
		}

		protected void Initialize()
		{
			if (Material == null)
			{
				Material = BurstClothMaterial.DefaultMaterial;
			}
			if (_chains == null || _chains.Count == 0)
			{
				_chains = new List<Chain>();
				Transform[] rootBones = RootBones;
				foreach (Transform obj in rootBones)
				{
					List<Transform> list = new List<Transform>();
					Transform transform = obj;
					while (transform != null)
					{
						list.Add(transform);
						if (transform.childCount == 0)
						{
							break;
						}
						transform = transform.GetChild(0);
					}
					if (list.Count >= 2)
					{
						_chains.Add(new Chain
						{
							Transforms = list
						});
					}
				}
			}
			SetupManagedData();
			if (_boneDataOriginal != null && _boneDataOriginal.Count != 0)
			{
				return;
			}
			_boneDataOriginal = new List<BoneData>();
			_rotationConstraintsDataOriginal = new List<RotationConstraint>();
			_skinConstraintTargetsIds = new List<string>();
			_skinConstraintsOriginal = new List<SkinConstraint>();
			Vector3 localScale = Vector3.one;
			if (ViewModelRootTransform != null)
			{
				localScale = ViewModelRootTransform.localScale;
				ViewModelRootTransform.localScale = Vector3.one;
			}
			(Vector3 Origin, Quaternion Rotation, Vector3 Up) originTransform = GetOriginTransform();
			Vector3 item = originTransform.Origin;
			Quaternion item2 = originTransform.Rotation;
			Vector3 item3 = originTransform.Up;
			Quaternion quaternion = Quaternion.Inverse(item2);
			int num = 0;
			for (int j = 0; j < _chains.Count; j++)
			{
				List<Transform> transforms = _chains[j].Transforms;
				for (int k = 0; k < transforms.Count; k++)
				{
					Transform transform2 = transforms[k];
					Vector3 position = transform2.position;
					Vector3 vector = Vector3.Project(position - item, item3) + item;
					(Transform, int) siblingAndIndex = GetSiblingAndIndex(j - 1, k);
					Transform item4 = siblingAndIndex.Item1;
					int item5 = siblingAndIndex.Item2;
					(Vector3, float) boneLocalPositionAndLength = GetBoneLocalPositionAndLength(transform2, item4);
					Vector3 item6 = boneLocalPositionAndLength.Item1;
					float item7 = boneLocalPositionAndLength.Item2;
					(Transform, int) siblingAndIndex2 = GetSiblingAndIndex(j + 1, k);
					Transform item8 = siblingAndIndex2.Item1;
					int item9 = siblingAndIndex2.Item2;
					(Vector3, float) boneLocalPositionAndLength2 = GetBoneLocalPositionAndLength(transform2, item8);
					Vector3 item10 = boneLocalPositionAndLength2.Item1;
					float item11 = boneLocalPositionAndLength2.Item2;
					float num2 = (float)k / (float)_maxBoneDepth;
					BurstClothConstraint component;
					BurstClothConstraint burstClothConstraint = (transform2.TryGetComponent<BurstClothConstraint>(out component) ? component : null);
					float magnitude = (position - transform2.parent.position).magnitude;
					int rotationConstraintIndex = -1;
					int skinConstraintIndex = -1;
					if (burstClothConstraint != null)
					{
						bool constrainRotateX = burstClothConstraint.ConstrainRotateX;
						bool constrainRotateY = burstClothConstraint.ConstrainRotateY;
						bool constrainRotateZ = burstClothConstraint.ConstrainRotateZ;
						if (constrainRotateX || constrainRotateY || constrainRotateZ)
						{
							RotationConstraint item12 = new RotationConstraint(new bool3(constrainRotateX, constrainRotateY, constrainRotateZ), math.radians(burstClothConstraint.RotateX), math.radians(burstClothConstraint.RotateY), math.radians(burstClothConstraint.RotateZ));
							_rotationConstraintsDataOriginal.Add(item12);
							rotationConstraintIndex = _rotationConstraintsDataOriginal.Count - 1;
						}
						if (burstClothConstraint.ExtraSkinConstraint && !string.IsNullOrEmpty(burstClothConstraint.SkeletonBoneName))
						{
							_skinConstraintTargetsIds.Add(burstClothConstraint.SkeletonBoneName);
							SkinConstraint item13 = new SkinConstraint
							{
								LocalSkinPosition = float3.zero,
								LocalSkinNormal = burstClothConstraint.SkinNormal,
								LocalEscapeNormal = burstClothConstraint.EscapeNormal,
								BackstopRadius = burstClothConstraint.BackstopRadius,
								BackstopInset = burstClothConstraint.BackstopInset
							};
							_skinConstraintsOriginal.Add(item13);
							skinConstraintIndex = _skinConstraintsOriginal.Count - 1;
						}
					}
					_boneDataOriginal.Add(new BoneData
					{
						Depth = k,
						Parent = ((k > 0) ? (num - 1) : (-1)),
						Child = ((k < transforms.Count - 1) ? (num + 1) : (-1)),
						Length = magnitude,
						NormalFromParent = transform2.parent.InverseTransformPoint(position).normalized,
						LeftSibling = item5,
						LengthToLeft = item7,
						NormalFromLeft = math.normalize(item6),
						RightSibling = item9,
						NormalFromRight = math.normalize(item10),
						LengthToRight = item11,
						LocalPosition = transform2.parent.InverseTransformPoint(position),
						LocalRotation = transform2.localRotation,
						DirectionFromOrigin = (quaternion * (position - vector)).normalized,
						CollisionRadius = CollisionRadius * CollisionRadiusCurve.Evaluate(num2),
						DampingInv = 1f - Material.GetDamping(num2),
						RotationConstraintIndex = rotationConstraintIndex,
						SkinConstraintIndex = skinConstraintIndex,
						LengthConstraint = (((object)burstClothConstraint != null && burstClothConstraint.ConstrainLength) ? ((float2)burstClothConstraint.Length) : ((float2)DefaultLengthConstraint)) * magnitude,
						LengthRelaxation = Material.GetLengthRelaxation(num2),
						ShapeRelaxation = Material.GetShapeRelaxation(num2),
						SiblingRelaxation = Material.GetSiblingRelaxation(num2)
					});
					num++;
				}
			}
			if (ViewModelRootTransform != null)
			{
				ViewModelRootTransform.localScale = localScale;
			}
		}

		protected virtual void Awake()
		{
			Initialize();
		}

		protected virtual void OnEnable()
		{
			BurstClothManager.Get().Instances.Add(this);
			_originDirty = true;
		}

		protected void ResetBoneTransforms(bool resetBoneStates = false)
		{
			if (_boneDataOriginal == null || _boneDataOriginal.Count == 0)
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < _chains.Count; i++)
			{
				Chain chain = _chains[i];
				for (int j = 0; j < chain.Transforms.Count; j++)
				{
					BoneData boneData = _boneDataOriginal[num];
					num++;
					if (boneData.Depth != 0)
					{
						Transform transform = chain.Transforms[j];
						transform.localPosition = boneData.LocalPosition;
						transform.localRotation = boneData.LocalRotation;
						if (resetBoneStates && _boneState.IsCreated)
						{
							transform.GetPositionAndRotation(out var position, out var rotation);
							_boneState[num - 1] = new BoneState
							{
								Position = position,
								OldPosition = position,
								Rotation = rotation
							};
						}
					}
				}
			}
		}

		protected virtual void OnDisable()
		{
			if (ResetBonesOnDisable)
			{
				ResetBoneTransforms();
			}
			_accumulator = 0f;
			_boneCount = 0;
			_maxBoneDepth = 0;
			_boneToIndex = null;
			if (_boneTransforms.isCreated)
			{
				_boneTransforms.Dispose();
			}
			if (_boneData.IsCreated)
			{
				_boneData.Dispose();
			}
			if (_rotationConstraints.IsCreated)
			{
				_rotationConstraints.Dispose();
			}
			if (_boneState.IsCreated)
			{
				_boneState.Dispose();
			}
			if (_sphereColliderData.IsCreated)
			{
				_sphereColliderData.Dispose();
			}
			if (_sphereColliderState.IsCreated)
			{
				_sphereColliderState.Dispose();
			}
			if (_sphereColliderTransforms.isCreated)
			{
				_sphereColliderTransforms.Dispose();
			}
			if (_skinConstraints.IsCreated)
			{
				_skinConstraints.Dispose();
			}
			if (_skinConstraintsTransformAccessArray.isCreated)
			{
				_skinConstraintsTransformAccessArray.Dispose();
			}
			BurstClothManager.Get().Instances.Remove(this);
		}

		protected internal virtual void FrameUpdate()
		{
			Vector3 lhs = _rotation * Vector3.forward;
			(Vector3, Quaternion, Vector3) originTransform = GetOriginTransform();
			_origin = originTransform.Item1;
			_rotation = originTransform.Item2;
			_up = originTransform.Item3;
			Vector3 rhs = _rotation * Vector3.forward;
			if (Vector3.Dot(lhs, rhs) < 0.2f)
			{
				ResetBoneTransforms(resetBoneStates: true);
				_originDirty = true;
			}
			if ((object)SimulationSpace != null && SimulationSpace == null)
			{
				SimulationSpace = null;
			}
			Quaternion normalized = Quaternion.Slerp(_rotation, _prevRotation, 1f - RootMotionStrength).normalized;
			_rotationDelta = (_rotation * Quaternion.Inverse(normalized)).normalized;
			Vector3 vector = SimulationSpace?.position ?? Vector3.zero;
			if (_originDirty)
			{
				_simulationSpaceDelta = Vector3.zero;
				_rotationDelta = Quaternion.identity;
				_prevSimulationOrigin = vector;
			}
			else
			{
				_simulationSpaceDelta += (_prevSimulationOrigin.HasValue ? (vector - _prevSimulationOrigin.Value) : Vector3.zero);
				_prevSimulationOrigin = vector;
			}
			if ((object)SimulationSpace != _prevSimulationSpace)
			{
				_simulationSpaceDelta = Vector3.zero;
			}
			_prevSimulationSpace = SimulationSpace;
			if (_originDirty)
			{
				_originDelta = Vector3.zero;
				_prevOrigin = _origin;
			}
			else
			{
				_originDelta += (_prevOrigin.HasValue ? (_origin - _prevOrigin.Value - _simulationSpaceDelta) : Vector3.zero);
				_prevOrigin = _origin;
			}
			_originDirty = false;
		}

		private void VerifySkinConstraintTransformCache()
		{
			if (_skinConstraintTargetsIds == null)
			{
				return;
			}
			if (_skinConstraintTargetCache == null && _skinConstraintTargetsIds.Count > 0)
			{
				_skinConstraintTargetCache = new Transform[_skinConstraintTargetsIds.Count];
			}
			for (int i = 0; i < _skinConstraintTargetsIds.Count; i++)
			{
				Transform transform = _skinConstraintTargetCache[i];
				if (transform == null)
				{
					_skinAccessArrayDirty = true;
					transform = GetSkeletonBone(_skinConstraintTargetsIds[i]);
				}
				_skinConstraintTargetCache[i] = transform;
			}
		}

		private void PopulateSkinConstraintTransformAccessArray()
		{
			if (_skinConstraintTargetCache == null || (_skinConstraintsTransformAccessArray.isCreated && !_skinAccessArrayDirty))
			{
				return;
			}
			if (!_skinConstraintsTransformAccessArray.isCreated || _skinConstraintsTransformAccessArray.capacity < _skinConstraintTargetCache.Length)
			{
				if (_skinConstraintsTransformAccessArray.isCreated)
				{
					_skinConstraintsTransformAccessArray.Dispose();
				}
				_skinConstraintsTransformAccessArray = new TransformAccessArray(_skinConstraintTargetCache.Length);
			}
			_skinConstraintsTransformAccessArray.SetTransforms(_skinConstraintTargetCache);
			_skinAccessArrayDirty = false;
		}

		public virtual JobHandle SimulateSystem()
		{
			SetupData();
			_accumulator += Time.deltaTime;
			float num = 1f / (float)TickRate;
			int num2 = Mathf.Min(Mathf.FloorToInt(_accumulator / num), MaxTicksPerFrame);
			if (num2 == 0)
			{
				return default(JobHandle);
			}
			_accumulator -= (float)num2 * num;
			UpdateSkinConstraintsJob jobData = new UpdateSkinConstraintsJob
			{
				SkinConstraints = _skinConstraints
			};
			InitializeBoneStateJob jobData2 = new InitializeBoneStateJob
			{
				Data = _boneData,
				State = _boneState
			};
			InitializeSphereColliderStateJob jobData3 = new InitializeSphereColliderStateJob
			{
				Data = _sphereColliderData,
				State = _sphereColliderState
			};
			SimulatePositionsJob jobData4 = new SimulatePositionsJob
			{
				ColliderData = _sphereColliderData,
				ColliderState = _sphereColliderState,
				RotationConstraints = _rotationConstraints,
				SkinConstraints = _skinConstraints,
				BoneData = _boneData,
				BoneStates = _boneState,
				TickRate = TickRate,
				DeltaTime = num,
				IterationCount = num2,
				ConstraintIterationCount = ConstraintIterationCount,
				SimulationSpaceDelta = _simulationSpaceDelta,
				OriginRotationDelta = _rotationDelta,
				OriginDelta = ((_originDelta.sqrMagnitude < MaxOriginDelta * MaxOriginDelta) ? (_originDelta * (1f - RootMotionStrength)) : _originDelta),
				Gravity = ((Gravity.sqrMagnitude < 0.01f) ? new Vector3(0f, -0.01f, 0f) : Gravity),
				SiblingConstraints = SiblingConstraints,
				LengthModifier = LengthModifier,
				EnableCollisions = EnableCollisions,
				Origin = _origin,
				Rotation = _rotation,
				Up = _up,
				CollisionProjectionYOffset = CollisionDirectionAdjustment,
				CollisionFilteringScale = CollisionFilteringScale,
				Radius = 10f
			};
			_prevRotation = _rotation;
			_originDelta = Vector3.zero;
			_simulationSpaceDelta = Vector3.zero;
			UpdateTransformsJob jobData5 = new UpdateTransformsJob
			{
				Data = _boneData,
				State = _boneState
			};
			JobHandle job = default(JobHandle);
			if (_skinConstraintsTransformAccessArray.isCreated)
			{
				job = jobData.ScheduleReadOnly(_skinConstraintsTransformAccessArray, math.max(_boneTransforms.length / math.max(JobsUtility.JobWorkerCount, 1), 32));
			}
			JobHandle job2 = jobData2.ScheduleReadOnly(_boneTransforms, math.max(_boneTransforms.length / math.max(JobsUtility.JobWorkerCount, 1), 32));
			JobHandle job3 = jobData3.ScheduleReadOnly(_sphereColliderTransforms, math.max(_sphereColliderTransforms.length / math.max(JobsUtility.JobWorkerCount, 1), 32));
			JobHandle dependsOn = JobHandle.CombineDependencies(job, job2, job3);
			return IJobParallelForTransformExtensions.Schedule(dependsOn: jobData4.Schedule(dependsOn), jobData: jobData5, transforms: _boneTransforms);
		}

		private void SetupData()
		{
			SetupManagedData();
			SetupNativeData();
		}

		private void SetupManagedData()
		{
			if (_boneCount == 0 || _maxBoneDepth == 0)
			{
				_boneCount = _chains.Sum((Chain c) => c.Transforms.Count);
				_maxBoneDepth = _chains.Max((Chain c) => c.Transforms.Count);
			}
			if (_boneToIndex == null)
			{
				_boneToIndex = new Dictionary<Transform, int>();
				BuildBoneToIndexMap(_boneToIndex);
			}
			VerifySkinConstraintTransformCache();
		}

		private void SetupNativeData()
		{
			if (!_boneTransforms.isCreated)
			{
				_boneTransforms = new TransformAccessArray(_boneCount);
				foreach (Chain chain in _chains)
				{
					foreach (Transform transform in chain.Transforms)
					{
						_boneTransforms.Add(transform);
					}
				}
			}
			if (!_boneData.IsCreated)
			{
				_boneData = new NativeArray<BoneData>(_boneCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				int num = 0;
				foreach (BoneData item in _boneDataOriginal)
				{
					_boneData[num++] = item;
				}
			}
			if (!_rotationConstraints.IsCreated)
			{
				_rotationConstraints = new NativeArray<RotationConstraint>(_rotationConstraintsDataOriginal.Count, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				int num2 = 0;
				foreach (RotationConstraint item2 in _rotationConstraintsDataOriginal)
				{
					_rotationConstraints[num2++] = item2;
				}
			}
			if (!_boneState.IsCreated)
			{
				_boneState = new NativeArray<BoneState>(_boneCount, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				int num3 = 0;
				foreach (Chain chain2 in _chains)
				{
					foreach (Transform transform2 in chain2.Transforms)
					{
						Vector3 position = transform2.position;
						_boneState[num3++] = new BoneState
						{
							Position = position,
							Rotation = transform2.rotation,
							OldPosition = position
						};
					}
				}
			}
			if (!_sphereColliderTransforms.isCreated || !_sphereColliderData.IsCreated)
			{
				SphereParamsShared.Clear();
				GatherColliders(SphereParamsShared);
				_sphereColliderTransforms = new TransformAccessArray(SphereParamsShared.Count);
				_sphereColliderData = new NativeArray<SphereColliderData>(SphereParamsShared.Count, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				for (int i = 0; i < SphereParamsShared.Count; i++)
				{
					SphereParams sphereParams = SphereParamsShared[i];
					_sphereColliderTransforms.Add(sphereParams.Transform);
					_sphereColliderData[i] = new SphereColliderData
					{
						Radius = sphereParams.Radius,
						RadiusSqr = sphereParams.Radius * sphereParams.Radius,
						LocalPosition = sphereParams.Point
					};
				}
				SphereParamsShared.Clear();
			}
			if (!_sphereColliderState.IsCreated)
			{
				_sphereColliderState = new NativeArray<SphereColliderState>(_sphereColliderData.Length, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			}
			if (!_skinConstraints.IsCreated)
			{
				_skinConstraints = new NativeArray<SkinConstraint>(_skinConstraintsOriginal.Count, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				for (int j = 0; j < _skinConstraintsOriginal.Count; j++)
				{
					_skinConstraints[j] = _skinConstraintsOriginal[j];
				}
			}
			PopulateSkinConstraintTransformAccessArray();
		}

		protected virtual (Vector3 Origin, Quaternion Rotation, Vector3 Up) GetOriginTransform()
		{
			return (Origin: base.transform.position, Rotation: base.transform.rotation, Up: base.transform.up);
		}

		protected virtual void GatherColliders(List<SphereParams> sphereColliders)
		{
		}

		private static (Vector3, float) GetBoneLocalPositionAndLength(Transform parent, Transform child)
		{
			if (child == null)
			{
				return (Vector3.zero, 0f);
			}
			Vector3 item = child.InverseTransformPoint(parent.position);
			return (item, item.magnitude);
		}

		private (Transform, int) GetSiblingAndIndex(int chainIndex, int depth)
		{
			if (chainIndex < 0 || chainIndex >= _chains.Count)
			{
				return (null, -1);
			}
			List<Transform> transforms = _chains[chainIndex].Transforms;
			if (depth < 0 || depth >= transforms.Count)
			{
				return (null, -1);
			}
			Transform transform = transforms[depth];
			return (transform, _boneToIndex[transform]);
		}

		private void BuildBoneToIndexMap(Dictionary<Transform, int> boneToIndex)
		{
			boneToIndex.Clear();
			int num = 0;
			foreach (Chain chain in _chains)
			{
				foreach (Transform transform in chain.Transforms)
				{
					boneToIndex.Add(transform, num++);
				}
			}
		}

		protected void UpdateCachedMaterialData()
		{
			bool flag = false;
			if (_chains != null)
			{
				foreach (Chain chain in _chains)
				{
					foreach (Transform transform in chain.Transforms)
					{
						if (transform == null)
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						break;
					}
				}
			}
			if (flag)
			{
				_chains.Clear();
			}
			if (!Application.isPlaying || !_boneData.IsCreated)
			{
				return;
			}
			if (Material == null)
			{
				Material = BurstClothMaterial.DefaultMaterial;
			}
			int num = 0;
			for (int i = 0; i < _chains.Count; i++)
			{
				List<Transform> transforms = _chains[i].Transforms;
				for (int j = 0; j < transforms.Count; j++)
				{
					BoneData value = _boneDataOriginal[num];
					float num2 = (float)j / (float)_maxBoneDepth;
					value.CollisionRadius = CollisionRadius * CollisionRadiusCurve.Evaluate(num2);
					value.DampingInv = 1f - Material.GetDamping(num2);
					value.LengthRelaxation = Material.GetLengthRelaxation(num2);
					value.ShapeRelaxation = Material.GetShapeRelaxation(num2);
					value.SiblingRelaxation = Material.GetSiblingRelaxation(num2);
					_boneDataOriginal[num] = value;
					_boneData[num] = value;
					num++;
				}
			}
		}
	}
	public class BurstClothConstraint : MonoBehaviour
	{
		public bool ConstrainRotateX;

		public Vector2 RotateX;

		public bool ConstrainRotateY;

		public Vector2 RotateY;

		public bool ConstrainRotateZ;

		public Vector2 RotateZ;

		public bool ConstrainLength;

		public Vector2 Length;

		public bool ExtraSkinConstraint;

		public Transform SkeletonBone;

		[HideInInspector]
		public string SkeletonBoneName;

		public Vector3 SkinNormal;

		public Vector3 EscapeNormal;

		public float BackstopInset;

		public float BackstopRadius;

		private void OnValidate()
		{
			if (SkeletonBone != null)
			{
				SkeletonBoneName = SkeletonBone.name;
			}
		}
	}
	public class BurstClothManager : MonoBehaviour
	{
		private static BurstClothManager _instance;

		internal readonly List<BurstCloth> Instances = new List<BurstCloth>();

		public void LateUpdate()
		{
			int num = 0;
			foreach (BurstCloth instance in Instances)
			{
				if (instance.EnableSimulation)
				{
					instance.FrameUpdate();
					num++;
				}
			}
			NativeArray<JobHandle> jobs = new NativeArray<JobHandle>(num, Allocator.Temp);
			int num2 = 0;
			foreach (BurstCloth instance2 in Instances)
			{
				if (instance2.EnableSimulation)
				{
					jobs[num2++] = instance2.SimulateSystem();
				}
			}
			JobHandle.CompleteAll(jobs);
		}

		public static BurstClothManager Get()
		{
			if ((bool)_instance)
			{
				return _instance;
			}
			GameObject obj = new GameObject("BurstClothManager");
			UnityEngine.Object.DontDestroyOnLoad(obj);
			_instance = obj.AddComponent<BurstClothManager>();
			return _instance;
		}
	}
	[CreateAssetMenu(fileName = "BurstClothMaterial", menuName = "Facepunch.BurstCloth/Material")]
	public class BurstClothMaterial : ScriptableObject
	{
		[Header("Relaxation\n [0,1]\tUnder-tuned - generally gives best results\n[1, 1.5]\tOver-tuned - can work well with low iteration counts")]
		[SerializeField]
		[Range(0.25f, 1.5f)]
		[Tooltip("Relaxation factor on alignment with parent")]
		private float lengthRelaxation = 1f;

		[SerializeField]
		[Range(0f, 15f)]
		[Tooltip("Speed at which a bone returns to its original orientation")]
		private float shapeRelaxation;

		[SerializeField]
		[Range(0f, 1.5f)]
		[Tooltip("Relaxation factor on alignment with siblings (very low values work best)")]
		private float siblingRelaxation;

		[SerializeField]
		[Range(0f, 0.99f)]
		[Tooltip("Controls how quickly movements come to rest.")]
		private float _damping = 0.2f;

		[Space]
		[Header("Depth Weighting Curves")]
		[SerializeField]
		[BurstClothCurveRect(0f, 0f, 1f, 1f)]
		[Tooltip("Multiplier for tweaking damping according to depth in the chain.")]
		private AnimationCurve _dampingCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 1f);

		[FormerlySerializedAs("_lengthWeightCurve")]
		[SerializeField]
		[BurstClothCurveRect(0f, 0f, 1f, 1f)]
		[Tooltip("Multiplier for tweaking length weight according to depth in the chain.")]
		private AnimationCurve _lengthRelaxationCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);

		[SerializeField]
		[BurstClothCurveRect(0f, 0f, 1f, 1f)]
		[Tooltip("Multiplier for tweaking shape weight according to depth in the chain.")]
		private AnimationCurve _shapeRelaxationCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);

		[SerializeField]
		[BurstClothCurveRect(0f, 0f, 1f, 1f)]
		[Tooltip("Multiplier for tweaking sibling constraint weight according to depth in the chain.")]
		private AnimationCurve _siblingConstraintWeightCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);

		[Space]
		[Header("Debug - can hurt editor performance")]
		[SerializeField]
		private bool _forceSyncInEditor;

		private static BurstClothMaterial _defaultMaterial;

		public bool ForceSyncInEditor => _forceSyncInEditor;

		public static BurstClothMaterial DefaultMaterial
		{
			get
			{
				if (_defaultMaterial == null)
				{
					_defaultMaterial = ScriptableObject.CreateInstance<BurstClothMaterial>();
				}
				_defaultMaterial.name = "BurstCloth_Default";
				return _defaultMaterial;
			}
		}

		public float GetLengthRelaxation(float t)
		{
			return lengthRelaxation * _lengthRelaxationCurve.Evaluate(t);
		}

		public float GetShapeRelaxation(float t)
		{
			return shapeRelaxation * _shapeRelaxationCurve.Evaluate(t);
		}

		public float GetSiblingRelaxation(float t)
		{
			return siblingRelaxation * _siblingConstraintWeightCurve.Evaluate(t);
		}

		public float GetDamping(float t)
		{
			return _damping * _dampingCurve.Evaluate(t);
		}
	}
}
namespace Facepunch.BurstCloth.Jobs
{
	[Serializable]
	internal struct BoneData
	{
		public int Depth;

		public int Parent;

		public int Child;

		public float Length;

		public float3 NormalFromParent;

		public int LeftSibling;

		public float3 NormalFromLeft;

		public float LengthToLeft;

		public int RightSibling;

		public float3 NormalFromRight;

		public float LengthToRight;

		public float3 LocalPosition;

		public quaternion LocalRotation;

		public float3 DirectionFromOrigin;

		public float CollisionRadius;

		public float DampingInv;

		public float LengthRelaxation;

		public float ShapeRelaxation;

		public float SiblingRelaxation;

		public int RotationConstraintIndex;

		public int SkinConstraintIndex;

		public float2 LengthConstraint;
	}
	[Serializable]
	internal struct RotationConstraint
	{
		public bool3 Constraints;

		public float2 X;

		public float2 Y;

		public float2 Z;

		public RotationConstraint(bool3 constraints, float2 x, float2 y, float2 z)
		{
			Constraints = constraints;
			X = x;
			Y = y;
			Z = z;
		}
	}
	internal struct BoneState
	{
		public float3 Position;

		public quaternion Rotation;

		public float3 OldPosition;

		public float3 Delta;
	}
	internal struct SphereColliderData
	{
		public float Radius;

		public float RadiusSqr;

		public float3 LocalPosition;
	}
	internal struct SphereColliderState
	{
		public float3 Position;
	}
	internal struct InitializeBoneStateJob : IJobParallelForTransform
	{
		[Unity.Collections.ReadOnly]
		[NativeMatchesParallelForLength]
		public NativeArray<BoneData> Data;

		[WriteOnly]
		[NativeMatchesParallelForLength]
		public NativeArray<BoneState> State;

		public void Execute(int index, [Unity.Collections.ReadOnly] TransformAccess transform)
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
	[BurstCompile]
	internal struct InitializeSphereColliderStateJob : IJobParallelForTransform
	{
		[Unity.Collections.ReadOnly]
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
	internal struct SimulatePositionsJob : IJob
	{
		private struct LerpState
		{
			public float3 Start;

			public float3 End;
		}

		private readonly struct Collision
		{
			public readonly bool Exists;

			public readonly float3 TargetToResolve;

			public Collision(bool exists, float3 targetToResolve)
			{
				Exists = exists;
				TargetToResolve = targetToResolve;
			}
		}

		[Unity.Collections.ReadOnly]
		public NativeArray<SphereColliderData> ColliderData;

		[Unity.Collections.ReadOnly]
		public NativeArray<SphereColliderState> ColliderState;

		[Unity.Collections.ReadOnly]
		public NativeArray<RotationConstraint> RotationConstraints;

		[Unity.Collections.ReadOnly]
		public NativeArray<SkinConstraint> SkinConstraints;

		[Unity.Collections.ReadOnly]
		public NativeArray<BoneData> BoneData;

		public NativeArray<BoneState> BoneStates;

		public int TickRate;

		public float DeltaTime;

		public int IterationCount;

		public int ConstraintIterationCount;

		public quaternion OriginRotationDelta;

		public float3 SimulationSpaceDelta;

		public float3 OriginDelta;

		public float3 Gravity;

		public float LengthModifier;

		public bool SiblingConstraints;

		public bool EnableCollisions;

		public float3 Origin;

		public quaternion Rotation;

		public float3 Up;

		public float Radius;

		public float CollisionProjectionYOffset;

		public float CollisionFilteringScale;

		public void Execute()
		{
			NativeArray<LerpState> nativeArray = new NativeArray<LerpState>(BoneStates.Length, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			bool flag = EnableCollisions && ColliderData.Length > 0;
			NativeArray<Collision> array = new NativeArray<Collision>(BoneStates.Length, Allocator.Temp);
			AdjustSimulationSpace();
			for (int i = 0; i < BoneStates.Length; i++)
			{
				ref readonly BoneState reference = ref BurstUtil.GetReadonly(in BoneStates, i);
				nativeArray[i] = new LerpState
				{
					Start = reference.OldPosition,
					End = reference.Position
				};
			}
			for (int j = 0; j < IterationCount; j++)
			{
				float s = ((float)j + 1f) / (float)IterationCount;
				for (int k = 0; k < BoneData.Length; k++)
				{
					ref readonly BoneData reference2 = ref BurstUtil.GetReadonly(in BoneData, k);
					ref BoneState reference3 = ref BurstUtil.Get(in BoneStates, k);
					if (reference2.Depth == 0)
					{
						reference3.OldPosition = reference3.Position;
						reference3.Position = math.lerp(nativeArray[k].Start, nativeArray[k].End, s);
					}
					else
					{
						VerletIntegrate(ref reference3, Gravity, DeltaTime, reference2.DampingInv);
					}
				}
				if (flag)
				{
					PopulateCollisions(array);
				}
				for (int l = 0; l < ConstraintIterationCount; l++)
				{
					for (int m = 0; m < BoneData.Length; m++)
					{
						ApplyRotationConstraints(in BurstUtil.GetReadonly(in BoneData, m), ref BurstUtil.Get(in BoneStates, m));
					}
					for (int n = 0; n < BoneData.Length; n++)
					{
						ref readonly BoneData data = ref BurstUtil.GetReadonly(in BoneData, n);
						ref BoneState state = ref BurstUtil.Get(in BoneStates, n);
						ref readonly Collision collision = ref BurstUtil.GetReadonly(in array, n);
						ApplyLinearConstraints(in data, ref state, in BoneStates, in collision);
					}
					for (int num = 0; num < BoneData.Length; num++)
					{
						if (BurstUtil.GetReadonly(in BoneData, num).Depth != 0)
						{
							ref BoneState reference4 = ref BurstUtil.Get(in BoneStates, num);
							reference4.Position += reference4.Delta;
							reference4.Delta = float3.zero;
						}
					}
				}
				for (int num2 = 0; num2 < BoneData.Length; num2++)
				{
					ApplyRotationConstraints(in BurstUtil.GetReadonly(in BoneData, num2), ref BurstUtil.Get(in BoneStates, num2));
				}
			}
			nativeArray.Dispose();
		}

		private void AdjustSimulationSpace()
		{
			for (int i = 0; i < BoneData.Length; i++)
			{
				ref readonly BoneData reference = ref BurstUtil.GetReadonly(in BoneData, i);
				ref BoneState reference2 = ref BurstUtil.Get(in BoneStates, i);
				float3 @float = reference2.Position - Origin;
				float3 float2 = @float - math.rotate(OriginRotationDelta, @float);
				if (reference.Depth > 0)
				{
					reference2.Position += SimulationSpaceDelta + OriginDelta - float2;
				}
				reference2.OldPosition += SimulationSpaceDelta + OriginDelta - float2;
			}
		}

		private void ApplyLinearConstraints(in BoneData data, ref BoneState state, in NativeArray<BoneState> workBoneState, in Collision collision)
		{
			if (data.Depth == 0)
			{
				return;
			}
			ref BoneState reference = ref BurstUtil.Get(in workBoneState, data.Parent);
			float3 zero = float3.zero;
			int num = 0;
			float3 zero2 = float3.zero;
			int num2 = 0;
			if (data.SkinConstraintIndex >= 0)
			{
				ref readonly SkinConstraint reference2 = ref BurstUtil.GetReadonly(in SkinConstraints, data.SkinConstraintIndex);
				float3 @float = reference2.WorldSkinPosition - reference2.WorldSkinNormal * reference2.BackstopInset;
				if (math.distancesq(state.Position, @float) <= reference2.BackstopRadius * reference2.BackstopRadius)
				{
					float3 float2 = math.normalize(state.Position - @float);
					float3 float3 = Slerp(float2, reference2.WorldEscapeNormal, math.select(0.2f, 1f, math.dot(float2, reference2.WorldEscapeNormal) >= 0f));
					float3 float4 = @float + float3 * reference2.BackstopRadius;
					float3 float5 = state.Position - float4;
					zero += 0.5f * data.LengthRelaxation * -float5;
					num++;
				}
			}
			if (EnableCollisions && collision.Exists)
			{
				float3 targetToResolve = collision.TargetToResolve;
				float3 float6 = state.Position - targetToResolve;
				zero += 0.5f * data.LengthRelaxation * -float6;
				num++;
			}
			if (data.LengthRelaxation > 0f)
			{
				float num3 = math.clamp(math.distance(state.Position, reference.Position), data.LengthConstraint.x, data.LengthConstraint.y);
				float3 float7 = reference.Position + math.rotate(reference.Rotation, data.NormalFromParent * num3 * LengthModifier);
				float3 float8 = state.Position - float7;
				float3 float9 = math.select(0.5f, 1f, data.Depth == 1) * data.LengthRelaxation * -float8;
				zero += float9;
				num++;
				zero2 -= float9;
				num2++;
			}
			state.Delta += zero / ((float)num + 1.1754944E-38f);
			if (data.Depth > 1)
			{
				reference.Delta += zero2 / ((float)num2 + 1.1754944E-38f);
			}
			if (SiblingConstraints)
			{
				float3 zero3 = float3.zero;
				int num4 = 0;
				if (data.LeftSibling >= 0)
				{
					ref BoneState reference3 = ref BurstUtil.Get(in workBoneState, data.LeftSibling);
					float3 float10 = reference3.Position + math.rotate(math.slerp(reference3.Rotation, state.Rotation, 0.5f), data.NormalFromLeft * data.LengthToLeft);
					float3 float11 = state.Position - float10;
					float3 float12 = 0.5f * data.SiblingRelaxation * -float11;
					zero3 += float12;
					num4++;
					reference3.Delta -= float12;
				}
				state.Delta += zero3 / ((float)num4 + 1.1754944E-38f);
			}
		}

		private static void VerletIntegrate(ref BoneState state, float3 acceleration, float dt, float damping)
		{
			float3 position = state.Position;
			state.Position += (state.Position - state.OldPosition) * damping + acceleration * dt * dt;
			state.OldPosition = position;
		}

		private void PopulateCollisions(NativeArray<Collision> collisions)
		{
			for (int i = 0; i < BoneStates.Length; i++)
			{
				ref readonly BoneState reference = ref BurstUtil.GetReadonly(in BoneStates, i);
				ref readonly BoneData reference2 = ref BurstUtil.GetReadonly(in BoneData, i);
				float3 @float = math.project(reference.Position - Origin, Up) + Origin + Up * CollisionProjectionYOffset;
				float3 float2 = math.normalizesafe(reference.Position - @float);
				float3 float3 = math.mul(Rotation, reference2.DirectionFromOrigin);
				float3 float4 = Slerp(float3, float2, math.max(math.dot(float2, float3), 0f));
				float3 origin = @float + float4 * Radius;
				float3 x = reference.Position - origin;
				float3 direction = math.normalizesafe(x);
				float maxDistance = math.length(x);
				bool hit;
				float3 float5 = RayMarch(in origin, in direction, reference2.CollisionRadius, maxDistance, out hit);
				if (math.distancesq(float5, reference.Position) > reference2.CollisionRadius * CollisionFilteringScale * reference2.CollisionRadius * CollisionFilteringScale)
				{
					hit = false;
				}
				collisions[i] = new Collision(hit, float5);
			}
		}

		private void ApplyRotationConstraints(in BoneData data, ref BoneState state)
		{
			if (data.Depth != 0 && data.Parent >= 0)
			{
				ref readonly BoneData reference = ref BurstUtil.GetReadonly(in BoneData, data.Parent);
				ref BoneState reference2 = ref BurstUtil.Get(in BoneStates, data.Parent);
				quaternion rotation = state.Rotation;
				if (data.Child >= 0)
				{
					ref readonly BoneData reference3 = ref BurstUtil.GetReadonly(in BoneData, data.Child);
					ref BoneState reference4 = ref BurstUtil.Get(in BoneStates, data.Child);
					rotation = math.mul(rotation, FromToRotation(reference3.LocalPosition, math.mul(math.conjugate(rotation), reference4.Position - state.Position)));
				}
				else
				{
					rotation = math.mul(rotation, FromToRotation(reference.LocalPosition, math.mul(math.conjugate(rotation), state.Position - reference2.Position)));
				}
				_ = SiblingConstraints;
				quaternion q = math.mul(reference2.Rotation, data.LocalRotation);
				rotation = math.slerp(rotation, q, math.saturate(DeltaTime * data.ShapeRelaxation));
				rotation = ApplyAngleLimitsConstraint(in data, in RotationConstraints, rotation, reference2.Rotation);
				state.Rotation = math.normalize(rotation);
			}
		}

		private static quaternion ApplyAngleLimitsConstraint(in BoneData data, in NativeArray<RotationConstraint> rotationConstraints, quaternion rotation, quaternion parentRotation)
		{
			if (data.RotationConstraintIndex == -1)
			{
				return rotation;
			}
			ref readonly RotationConstraint reference = ref BurstUtil.GetReadonly(in rotationConstraints, data.RotationConstraintIndex);
			float3 xyz = ToEuler(math.mul(math.conjugate(parentRotation), rotation));
			if (reference.Constraints.x)
			{
				xyz.x = ClampAngle(xyz.x, reference.X.x, reference.X.y);
			}
			if (reference.Constraints.y)
			{
				xyz.y = ClampAngle(xyz.y, reference.Y.x, reference.Y.y);
			}
			if (reference.Constraints.z)
			{
				xyz.z = ClampAngle(xyz.z, reference.Z.x, reference.Z.y);
			}
			return math.mul(parentRotation, quaternion.Euler(xyz));
		}

		private float3 RayMarch(in float3 origin, in float3 direction, float radius, float maxDistance, out bool hit)
		{
			hit = false;
			float num = 0f;
			for (int i = 0; i < 8; i++)
			{
				float3 position = origin + direction * num;
				float num2 = Scene(in position) - radius;
				if (num2 < 0.0001f)
				{
					hit = true;
					return position + direction * num2;
				}
				num += num2;
				if (num > maxDistance)
				{
					break;
				}
			}
			return origin + direction * maxDistance;
		}

		private float Scene(in float3 position)
		{
			float num = float.MaxValue;
			for (int i = 0; i < ColliderData.Length; i++)
			{
				ref readonly SphereColliderData reference = ref BurstUtil.GetReadonly(in ColliderData, i);
				num = Union(num, SphereSignedDistance(in position, in BurstUtil.GetReadonly(in ColliderState, i).Position, in reference.Radius));
			}
			return num;
		}

		private static float Union(float d1, float d2)
		{
			return math.min(d1, d2);
		}

		private static float SphereSignedDistance(in float3 p, in float3 pSphere, in float r)
		{
			return math.length(p - pSphere) - r;
		}

		private static float Capsule(in float3 p, in float3 a, in float3 b, float r)
		{
			float3 @float = p - a;
			float3 float2 = b - a;
			float num = math.clamp(math.dot(@float, float2) / math.dot(float2, float2), 0f, 1f);
			return math.length(@float - float2 * num) - r;
		}

		private static float3 Slerp(float3 a, float3 b, float t)
		{
			return Vector3.Slerp(a, b, t);
		}

		private static quaternion FromToRotation(float3 from, float3 to)
		{
			float angle = math.acos(math.clamp(math.dot(math.normalizesafe(from), math.normalizesafe(to)), -1f, 1f));
			return quaternion.AxisAngle(math.normalizesafe(math.cross(from, to)), angle);
		}

		private static float ClampAngle(float current, float min, float max)
		{
			float num = math.abs((min - max + 180f) % 360f - 180f) * 0.5f;
			float target = min + num;
			float num2 = math.abs(DeltaAngle(current, target)) - num;
			if (num2 > 0f)
			{
				current = MoveTowardsAngle(current, target, num2);
			}
			return current;
		}

		private static float MoveTowardsAngle(float current, float target, float maxDelta)
		{
			float num = DeltaAngle(current, target);
			if (0f - maxDelta < num && num < maxDelta)
			{
				return target;
			}
			target = current + num;
			return MoveTowards(current, target, maxDelta);
		}

		private static float MoveTowards(float current, float target, float maxDelta)
		{
			if (!(math.abs(target - current) <= maxDelta))
			{
				return current + math.sign(target - current) * maxDelta;
			}
			return target;
		}

		private static float DeltaAngle(float current, float target)
		{
			float num = Repeat(target - current, 360f);
			if ((double)num > 180.0)
			{
				num -= 360f;
			}
			return num;
		}

		private static float Repeat(float t, float length)
		{
			return math.clamp(t - math.floor(t / length) * length, 0f, length);
		}

		private static float3 ToEuler(quaternion q, math.RotationOrder order = math.RotationOrder.ZXY)
		{
			float4 value = q.value;
			float4 @float = value * value.wwww * new float4(2f);
			float4 float2 = value * value.yzxw * new float4(2f);
			float4 float3 = value * value;
			float3 euler = new float3(0f);
			switch (order)
			{
			case math.RotationOrder.ZYX:
			{
				float num5 = float2.z + @float.y;
				if (num5 * num5 < 0.99999595f)
				{
					float y13 = 0f - float2.x + @float.z;
					float x13 = float3.x + float3.w - float3.y - float3.z;
					float y14 = 0f - float2.y + @float.x;
					float x14 = float3.z + float3.w - float3.y - float3.x;
					euler = new float3(math.atan2(y13, x13), math.asin(num5), math.atan2(y14, x14));
				}
				else
				{
					num5 = math.clamp(num5, -1f, 1f);
					float4 float8 = new float4(float2.z, @float.y, float2.y, @float.x);
					float y15 = 2f * (float8.x * float8.w + float8.y * float8.z);
					float x15 = math.csum(float8 * float8 * new float4(-1f, 1f, -1f, 1f));
					euler = new float3(math.atan2(y15, x15), math.asin(num5), 0f);
				}
				break;
			}
			case math.RotationOrder.ZXY:
			{
				float num3 = float2.y - @float.x;
				if (num3 * num3 < 0.99999595f)
				{
					float y7 = float2.x + @float.z;
					float x7 = float3.y + float3.w - float3.x - float3.z;
					float y8 = float2.z + @float.y;
					float x8 = float3.z + float3.w - float3.x - float3.y;
					euler = new float3(math.atan2(y7, x7), 0f - math.asin(num3), math.atan2(y8, x8));
				}
				else
				{
					num3 = math.clamp(num3, -1f, 1f);
					float4 float6 = new float4(float2.z, @float.y, float2.y, @float.x);
					float y9 = 2f * (float6.x * float6.w + float6.y * float6.z);
					float x9 = math.csum(float6 * float6 * new float4(-1f, 1f, -1f, 1f));
					euler = new float3(math.atan2(y9, x9), 0f - math.asin(num3), 0f);
				}
				break;
			}
			case math.RotationOrder.YXZ:
			{
				float num6 = float2.y + @float.x;
				if (num6 * num6 < 0.99999595f)
				{
					float y16 = 0f - float2.z + @float.y;
					float x16 = float3.z + float3.w - float3.x - float3.y;
					float y17 = 0f - float2.x + @float.z;
					float x17 = float3.y + float3.w - float3.z - float3.x;
					euler = new float3(math.atan2(y16, x16), math.asin(num6), math.atan2(y17, x17));
				}
				else
				{
					num6 = math.clamp(num6, -1f, 1f);
					float4 float9 = new float4(float2.x, @float.z, float2.y, @float.x);
					float y18 = 2f * (float9.x * float9.w + float9.y * float9.z);
					float x18 = math.csum(float9 * float9 * new float4(-1f, 1f, -1f, 1f));
					euler = new float3(math.atan2(y18, x18), math.asin(num6), 0f);
				}
				break;
			}
			case math.RotationOrder.YZX:
			{
				float num2 = float2.x - @float.z;
				if (num2 * num2 < 0.99999595f)
				{
					float y4 = float2.z + @float.y;
					float x4 = float3.x + float3.w - float3.z - float3.y;
					float y5 = float2.y + @float.x;
					float x5 = float3.y + float3.w - float3.x - float3.z;
					euler = new float3(math.atan2(y4, x4), 0f - math.asin(num2), math.atan2(y5, x5));
				}
				else
				{
					num2 = math.clamp(num2, -1f, 1f);
					float4 float5 = new float4(float2.x, @float.z, float2.y, @float.x);
					float y6 = 2f * (float5.x * float5.w + float5.y * float5.z);
					float x6 = math.csum(float5 * float5 * new float4(-1f, 1f, -1f, 1f));
					euler = new float3(math.atan2(y6, x6), 0f - math.asin(num2), 0f);
				}
				break;
			}
			case math.RotationOrder.XZY:
			{
				float num4 = float2.x + @float.z;
				if (num4 * num4 < 0.99999595f)
				{
					float y10 = 0f - float2.y + @float.x;
					float x10 = float3.y + float3.w - float3.z - float3.x;
					float y11 = 0f - float2.z + @float.y;
					float x11 = float3.x + float3.w - float3.y - float3.z;
					euler = new float3(math.atan2(y10, x10), math.asin(num4), math.atan2(y11, x11));
				}
				else
				{
					num4 = math.clamp(num4, -1f, 1f);
					float4 float7 = new float4(float2.x, @float.z, float2.z, @float.y);
					float y12 = 2f * (float7.x * float7.w + float7.y * float7.z);
					float x12 = math.csum(float7 * float7 * new float4(-1f, 1f, -1f, 1f));
					euler = new float3(math.atan2(y12, x12), math.asin(num4), 0f);
				}
				break;
			}
			case math.RotationOrder.XYZ:
			{
				float num = float2.z - @float.y;
				if (num * num < 0.99999595f)
				{
					float y = float2.y + @float.x;
					float x = float3.z + float3.w - float3.y - float3.x;
					float y2 = float2.x + @float.z;
					float x2 = float3.x + float3.w - float3.y - float3.z;
					euler = new float3(math.atan2(y, x), 0f - math.asin(num), math.atan2(y2, x2));
				}
				else
				{
					num = math.clamp(num, -1f, 1f);
					float4 float4 = new float4(float2.z, @float.y, float2.x, @float.z);
					float y3 = 2f * (float4.x * float4.w + float4.y * float4.z);
					float x3 = math.csum(float4 * float4 * new float4(-1f, 1f, -1f, 1f));
					euler = new float3(math.atan2(y3, x3), 0f - math.asin(num), 0f);
				}
				break;
			}
			}
			return EulerReorderBack(euler, order);
		}

		private static float3 EulerReorderBack(float3 euler, math.RotationOrder order)
		{
			return order switch
			{
				math.RotationOrder.XZY => euler.xzy, 
				math.RotationOrder.YZX => euler.zxy, 
				math.RotationOrder.YXZ => euler.yxz, 
				math.RotationOrder.ZXY => euler.yzx, 
				math.RotationOrder.ZYX => euler.zyx, 
				_ => euler, 
			};
		}
	}
	[BurstCompile]
	internal struct UpdateSkinConstraintsJob : IJobParallelForTransform
	{
		[NativeMatchesParallelForLength]
		public NativeArray<SkinConstraint> SkinConstraints;

		public void Execute(int index, [Unity.Collections.ReadOnly] TransformAccess transform)
		{
			if (transform.isValid)
			{
				ref SkinConstraint reference = ref BurstUtil.Get(in SkinConstraints, index);
				float4x4 a = transform.localToWorldMatrix;
				reference.WorldSkinNormal = math.normalize(math.rotate(a, reference.LocalSkinNormal).xyz);
				reference.WorldEscapeNormal = math.normalize(math.rotate(a, reference.LocalEscapeNormal).xyz);
				reference.WorldSkinPosition = math.mul(a, new float4(reference.LocalSkinPosition, 1f)).xyz;
			}
		}
	}
	internal struct UpdateTransformsJob : IJobParallelForTransform
	{
		[Unity.Collections.ReadOnly]
		[NativeMatchesParallelForLength]
		public NativeArray<BoneData> Data;

		[Unity.Collections.ReadOnly]
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
}
[Unity.Jobs.DOTSCompilerGenerated]
internal class __JobReflectionRegistrationOutput__7727290552441277134
{
	public static void CreateJobReflectionData()
	{
		try
		{
			IJobParallelForTransformExtensions.EarlyJobInit<InitializeBoneStateJob>();
			IJobParallelForTransformExtensions.EarlyJobInit<InitializeSphereColliderStateJob>();
			IJobExtensions.EarlyJobInit<SimulatePositionsJob>();
			IJobParallelForTransformExtensions.EarlyJobInit<UpdateSkinConstraintsJob>();
			IJobParallelForTransformExtensions.EarlyJobInit<UpdateTransformsJob>();
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
