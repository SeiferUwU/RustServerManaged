using UnityEngine;

[ExecuteInEditMode]
public class EnvironmentVolume : MonoBehaviour, IPrefabPreProcess
{
	public enum VolumeShape
	{
		Cube,
		Sphere,
		Capsule
	}

	private static readonly Vector3[] volumeCorners = new Vector3[8]
	{
		new Vector3(-0.5f, -0.5f, -0.5f),
		new Vector3(0.5f, -0.5f, -0.5f),
		new Vector3(0.5f, 0.5f, -0.5f),
		new Vector3(-0.5f, 0.5f, -0.5f),
		new Vector3(-0.5f, -0.5f, 0.5f),
		new Vector3(0.5f, -0.5f, 0.5f),
		new Vector3(0.5f, 0.5f, 0.5f),
		new Vector3(-0.5f, 0.5f, 0.5f)
	};

	[InspectorFlags]
	public EnvironmentType Type = EnvironmentType.Underground;

	[InspectorFlags]
	public NetworkGroupType NetworkType;

	public Vector3 Center = Vector3.zero;

	public Vector3 Size = Vector3.one;

	[field: SerializeField]
	[field: Tooltip("Controls the falloff amount of the positive axes of spatially aware volumes.")]
	public Vector3 FalloffPositive { get; private set; } = Vector3.zero;

	[field: SerializeField]
	[field: Tooltip("Controls the falloff amount of the negative axes of spatially aware volumes.")]
	public Vector3 FalloffNegative { get; private set; } = Vector3.zero;

	[field: SerializeField]
	public VolumeShape SpatialVolumeShape { get; private set; }

	public Matrix4x4 VolumeTransformation { get; private set; }

	public Matrix4x4 VolumeTransformationInverse { get; private set; }

	public Vector3 VolumePosition { get; private set; }

	public Bounds VolumeBounds { get; private set; }

	public float AmbientMultiplier { get; private set; }

	public float ReflectionMultiplier { get; private set; }

	public float CombinedMultiplier { get; private set; }

	public bool NoSunlight { get; private set; }

	public bool PropertiesCached { get; private set; }

	public Collider trigger { get; private set; }

	public bool IsSpatialVolume => (Type & EnvironmentType.SpatiallyAware) != 0;

	bool IPrefabPreProcess.CanRunDuringBundling => false;

	private void OnValidate()
	{
		PropertiesCached = false;
		UpdateVolumeTransformationAndBounds();
	}

	public void PreProcess(IPrefabProcessor preProcess, GameObject rootObj, string name, bool serverside, bool clientside, bool bundling)
	{
		if (clientside && IsSpatialVolume && !(base.gameObject == null) && GetComponent<EnvironmentVolumeLOD>() == null)
		{
			base.gameObject.AddComponent<EnvironmentVolumeLOD>();
		}
	}

	protected void Awake()
	{
		UpdateTrigger();
	}

	protected void OnEnable()
	{
		if ((bool)trigger && !trigger.enabled)
		{
			trigger.enabled = true;
		}
		NetworkVisibilityGrid.RegisterEnvironmentVolume(this);
		UpdateVolumeTransformationAndBounds();
	}

	protected void OnDisable()
	{
		if ((bool)trigger && trigger.enabled)
		{
			trigger.enabled = false;
		}
	}

	private Bounds CalculateTransformationBounds(Matrix4x4 transformationMatrix)
	{
		Vector3 vector = Vector3.positiveInfinity;
		Vector3 vector2 = Vector3.negativeInfinity;
		for (int i = 0; i < volumeCorners.Length; i++)
		{
			Vector3 rhs = transformationMatrix.MultiplyPoint3x4(volumeCorners[i]);
			vector = Vector3.Min(vector, rhs);
			vector2 = Vector3.Max(vector2, rhs);
		}
		if (IsSpatialVolume && SpatialVolumeShape == VolumeShape.Capsule)
		{
			float num = Mathf.Abs(vector2.y - vector.y) * 0.5f;
			vector.y -= num;
			vector2.y += num;
		}
		Bounds result = new Bounds(Vector3.zero, Vector3.one);
		result.SetMinMax(vector, vector2);
		return result;
	}

	public void UpdateVolumeTransformationAndBounds()
	{
		Vector3 vector = Size + new Vector3(0.001f, 0.001f, 0.001f);
		VolumeTransformation = base.transform.localToWorldMatrix * Matrix4x4.Translate(Center) * Matrix4x4.Scale(vector);
		VolumeTransformationInverse = VolumeTransformation.inverse;
		VolumePosition = VolumeTransformation.GetPosition();
		VolumeBounds = CalculateTransformationBounds(VolumeTransformation);
	}

	public void CacheVolumeProperties(EnvironmentVolumePropertiesCollection properties)
	{
		if (!PropertiesCached)
		{
			PropertiesCached = true;
			NoSunlight = (Type & EnvironmentType.NoSunlight) != 0 || (Type & EnvironmentType.TrainTunnels) != 0;
			CombinedMultiplier = AmbientMultiplier * ReflectionMultiplier;
		}
	}

	public void UpdateTrigger()
	{
		if (!trigger)
		{
			trigger = base.gameObject.GetComponent<Collider>();
		}
		if (!trigger)
		{
			trigger = base.gameObject.AddComponent<BoxCollider>();
		}
		trigger.isTrigger = true;
		BoxCollider boxCollider = trigger as BoxCollider;
		if ((bool)boxCollider)
		{
			boxCollider.center = Center;
			boxCollider.size = Size;
		}
	}
}
