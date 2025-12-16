using System;
using System.Threading.Tasks;
using Rust;
using TerrainTexturingJobs;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

[ExecuteInEditMode]
public class TerrainTexturing : TerrainExtension
{
	public struct ShoreVectorQueryStructure
	{
		public NativeArray<float>.ReadOnly ShoreDistances;

		public float ShoreDistanceScale;

		public int ShoreMapSize;

		public float GetCoarseDistanceToShore(Vector2 uv)
		{
			int shoreMapSize = ShoreMapSize;
			int num = shoreMapSize - 1;
			float num2 = uv.x * (float)num;
			float num3 = uv.y * (float)num;
			int num4 = (int)num2;
			int num5 = (int)num3;
			float num6 = num2 - (float)num4;
			float num7 = num3 - (float)num5;
			num4 = ((num4 >= 0) ? num4 : 0);
			num5 = ((num5 >= 0) ? num5 : 0);
			num4 = ((num4 <= num) ? num4 : num);
			num5 = ((num5 <= num) ? num5 : num);
			int num8 = ((num2 < (float)num) ? 1 : 0);
			int num9 = ((num3 < (float)num) ? shoreMapSize : 0);
			int num10 = num5 * shoreMapSize + num4;
			int index = num10 + num8;
			int num11 = num10 + num9;
			int index2 = num11 + num8;
			float num12 = ShoreDistances[num10];
			float num13 = ShoreDistances[index];
			float num14 = ShoreDistances[num11];
			float num15 = ShoreDistances[index2];
			float num16 = (num13 - num12) * num6 + num12;
			return (((num15 - num14) * num6 + num14 - num16) * num7 + num16) * ShoreDistanceScale;
		}
	}

	public bool debugFoliageDisplacement;

	private bool initialized;

	private static TerrainTexturing instance;

	private int afCached;

	private int globalTextureMipmapLimitCached;

	private int anisotropicFilteringCached;

	private bool streamingMipmapsActiveCached;

	private bool billboardsFaceCameraPositionCached;

	private const int ShoreVectorDownscale = 1;

	private const int ShoreVectorBlurPasses = 1;

	private float terrainSize;

	private int shoreMapSize;

	private float shoreDistanceScale;

	private NativeArray<float> shoreDistances;

	private NativeArray<float4> shoreVectors;

	public static TerrainTexturing Instance => instance;

	public bool TexturesInitialized => initialized;

	public int ShoreMapSize => shoreMapSize;

	public ReadOnlySpan<float4> ShoreMap => shoreVectors;

	private void ReleaseBasePyramid()
	{
	}

	private void UpdateBasePyramid()
	{
	}

	private void InitializeCoarseHeightSlope()
	{
	}

	private void ReleaseCoarseHeightSlope()
	{
	}

	private void UpdateCoarseHeightSlope()
	{
	}

	private void CheckInstance()
	{
		instance = ((instance != null) ? instance : this);
	}

	private void Awake()
	{
		CheckInstance();
	}

	public override void Setup()
	{
		CheckInstance();
		InitializeShoreVector();
	}

	public override void PostSetup()
	{
		TerrainMeta component = GetComponent<TerrainMeta>();
		if (component == null || component.config == null)
		{
			Debug.LogError("[TerrainTexturing] Missing TerrainMeta or TerrainConfig not assigned.");
			return;
		}
		Shutdown();
		InitializeCoarseHeightSlope();
		GenerateShoreVector();
		InitializeWaterHeight();
		initialized = true;
	}

	private void Shutdown()
	{
		ReleaseBasePyramid();
		ReleaseCoarseHeightSlope();
		ReleaseShoreVector();
		ReleaseWaterHeight();
		initialized = false;
	}

	public void OnEnable()
	{
		CheckInstance();
	}

	private void OnDisable()
	{
		if (!Rust.Application.isQuitting)
		{
			Shutdown();
		}
	}

	private void Update()
	{
		if (initialized)
		{
			UpdateBasePyramid();
			UpdateCoarseHeightSlope();
			UpdateWaterHeight();
		}
	}

	private void InitializeShoreVector()
	{
		int num = Mathf.ClosestPowerOfTwo(terrain.terrainData.heightmapResolution) >> 1;
		int num2 = num * num;
		terrainSize = Mathf.Max(terrain.terrainData.size.x, terrain.terrainData.size.z);
		shoreMapSize = num;
		shoreDistanceScale = terrainSize / (float)shoreMapSize;
		shoreDistances = new NativeArray<float>(num * num, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		shoreVectors = new NativeArray<float4>(num * num, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		for (int i = 0; i < num2; i++)
		{
			shoreDistances[i] = 10000f;
			shoreVectors[i] = new float4(1f, 1f, 1f, 0f);
		}
	}

	private void GenerateShoreVector()
	{
		using (TimeWarning.New("GenerateShoreVector", 500))
		{
			GenerateShoreVector(out var distances, out var vectors);
			if (!shoreDistances.IsCreated)
			{
				shoreDistances = new NativeArray<float>(distances, Allocator.Persistent);
			}
			else
			{
				shoreDistances.CopyFrom(distances);
			}
			if (!shoreVectors.IsCreated)
			{
				shoreVectors = new NativeArray<float4>(vectors.Length, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			}
			for (int i = 0; i < vectors.Length; i++)
			{
				shoreVectors[i] = vectors[i];
			}
		}
	}

	private void OnDestroy()
	{
		ReleaseShoreVector();
	}

	private void ReleaseShoreVector()
	{
		NativeArrayEx.SafeDispose(ref shoreDistances);
		NativeArrayEx.SafeDispose(ref shoreVectors);
	}

	private void GenerateShoreVector(out float[] distances, out Vector4[] vectors)
	{
		using (TimeWarning.New("GenerateShoreVector"))
		{
			float upscaleCoords = terrainSize / (float)shoreMapSize;
			Vector3 terrainPosition = terrain.GetPosition();
			byte[] image = new byte[shoreMapSize * shoreMapSize];
			distances = new float[shoreMapSize * shoreMapSize];
			vectors = new Vector4[shoreMapSize * shoreMapSize];
			using (TimeWarning.New("WaterDepth"))
			{
				int i = 0;
				int num = 0;
				for (; i < shoreMapSize; i++)
				{
					int num2 = 0;
					while (num2 < shoreMapSize)
					{
						float x = ((float)num2 + 0.5f) * upscaleCoords;
						float z = ((float)i + 0.5f) * upscaleCoords;
						bool flag = WaterLevel.GetOverallWaterDepth(new Vector3(terrainPosition.x, 0f, terrainPosition.z) + new Vector3(x, 0f, z), waves: false, volumes: false) <= 0f;
						image[num] = (byte)(flag ? 255u : 0u);
						distances[num] = (flag ? 256 : 0);
						num2++;
						num++;
					}
				}
			}
			using (TimeWarning.New("DistanceField.XXX"))
			{
				DistanceField.Generate(in shoreMapSize, (byte)127, in image, ref distances);
				DistanceField.ApplyGaussianBlur(shoreMapSize, distances);
				DistanceField.GenerateVectors(in shoreMapSize, in distances, ref vectors);
			}
			using (TimeWarning.New("Parallel.For"))
			{
				if (!(TerrainMeta.TopologyMap != null) || !TerrainMeta.TopologyMap.isInitialized || !(TerrainMeta.HeightMap != null) || !TerrainMeta.HeightMap.isInitialized)
				{
					for (int j = 0; j < vectors.Length; j++)
					{
						Vector4 vector = vectors[j];
						vector.w = -1f;
						vectors[j] = vector;
					}
					return;
				}
				NativeArray<Vector3> positions = new NativeArray<Vector3>(shoreMapSize * ShoreMapSize, Allocator.TempJob);
				NativeArray<float> radii = new NativeArray<float>(shoreMapSize * shoreMapSize, Allocator.TempJob);
				NativeArray<int> topologies = new NativeArray<int>(shoreMapSize * shoreMapSize, Allocator.TempJob);
				Parallel.For(0, shoreMapSize, delegate(int num3)
				{
					float z2 = ((float)num3 + 0.5f) * upscaleCoords;
					for (int k = 0; k < shoreMapSize; k++)
					{
						float x2 = ((float)k + 0.5f) * upscaleCoords;
						Vector3 vector2 = new Vector3(terrainPosition.x, 0f, terrainPosition.z) + new Vector3(x2, 0f, z2);
						float height = TerrainMeta.HeightMap.GetHeight(vector2);
						float t = Mathf.InverseLerp(4f, 0f, height);
						float value = Mathf.Lerp(16f, 32f, t);
						positions[num3 * shoreMapSize + k] = vector2;
						radii[num3 * shoreMapSize + k] = value;
					}
				});
				TerrainMeta.TopologyMap.GetTopologiesIndirect(positions.AsReadOnly(), radii.AsReadOnly(), topologies);
				Vector4[] captureVec = vectors;
				Parallel.For(0, shoreMapSize, delegate(int num3)
				{
					for (int k = 0; k < shoreMapSize; k++)
					{
						Vector4 vector2 = captureVec[num3 * shoreMapSize + k];
						int num4 = topologies[num3 * shoreMapSize + k];
						if ((num4 & 0x180) != 0)
						{
							vector2.w = 1f;
						}
						else if ((num4 & 0x32000) != 0)
						{
							vector2.w = 2f;
						}
						else if ((num4 & 0xC000) != 0)
						{
							vector2.w = 3f;
						}
						captureVec[num3 * shoreMapSize + k] = vector2;
					}
				});
				positions.Dispose(default(JobHandle));
				radii.Dispose(default(JobHandle));
				topologies.Dispose(default(JobHandle));
			}
		}
	}

	public float GetCoarseDistanceToShore(Vector3 pos)
	{
		Vector2 uv = default(Vector2);
		uv.x = (pos.x - TerrainMeta.Position.x) * TerrainMeta.OneOverSize.x;
		uv.y = (pos.z - TerrainMeta.Position.z) * TerrainMeta.OneOverSize.z;
		return GetCoarseDistanceToShore(uv);
	}

	public ShoreVectorQueryStructure GetShoreVectorQueryStructure()
	{
		return new ShoreVectorQueryStructure
		{
			ShoreDistances = shoreDistances.AsReadOnly(),
			ShoreDistanceScale = shoreDistanceScale,
			ShoreMapSize = shoreMapSize
		};
	}

	public float GetCoarseDistanceToShore(Vector2 uv)
	{
		int num = shoreMapSize;
		int num2 = num - 1;
		float num3 = uv.x * (float)num2;
		float num4 = uv.y * (float)num2;
		int num5 = (int)num3;
		int num6 = (int)num4;
		float num7 = num3 - (float)num5;
		float num8 = num4 - (float)num6;
		num5 = ((num5 >= 0) ? num5 : 0);
		num6 = ((num6 >= 0) ? num6 : 0);
		num5 = ((num5 <= num2) ? num5 : num2);
		num6 = ((num6 <= num2) ? num6 : num2);
		int num9 = ((num3 < (float)num2) ? 1 : 0);
		int num10 = ((num4 < (float)num2) ? num : 0);
		int num11 = num6 * num + num5;
		int index = num11 + num9;
		int num12 = num11 + num10;
		int index2 = num12 + num9;
		float num13 = shoreDistances[num11];
		float num14 = shoreDistances[index];
		float num15 = shoreDistances[num12];
		float num16 = shoreDistances[index2];
		float num17 = (num14 - num13) * num7 + num13;
		return (((num16 - num15) * num7 + num15 - num17) * num8 + num17) * shoreDistanceScale;
	}

	public void GetCoarseDistancesToShoreIndirect(NativeArray<Vector2>.ReadOnly uvs, NativeArray<int>.ReadOnly indices, NativeArray<float> results)
	{
		GetCoarseDistsToShoreJobIndirect jobData = new GetCoarseDistsToShoreJobIndirect
		{
			Dists = results,
			UVs = uvs,
			Indices = indices,
			Data = shoreDistances.AsReadOnly(),
			ShoreMapSize = shoreMapSize,
			ShoreDistanceScale = shoreDistanceScale
		};
		IJobExtensions.RunByRef(ref jobData);
	}

	public (Vector3 shoreDir, float shoreDist) GetCoarseVectorToShore(Vector3 pos)
	{
		Vector2 uv = default(Vector2);
		uv.x = (pos.x - TerrainMeta.Position.x) * TerrainMeta.OneOverSize.x;
		uv.y = (pos.z - TerrainMeta.Position.z) * TerrainMeta.OneOverSize.z;
		return GetCoarseVectorToShore(uv);
	}

	public (Vector3 shoreDir, float shoreDist) GetCoarseVectorToShore(Vector2 uv)
	{
		int num = shoreMapSize;
		int num2 = num - 1;
		float num3 = uv.x * (float)num2;
		float num4 = uv.y * (float)num2;
		int num5 = (int)num3;
		int num6 = (int)num4;
		float num7 = num3 - (float)num5;
		float num8 = num4 - (float)num6;
		num5 = ((num5 >= 0) ? num5 : 0);
		num6 = ((num6 >= 0) ? num6 : 0);
		num5 = ((num5 <= num2) ? num5 : num2);
		num6 = ((num6 <= num2) ? num6 : num2);
		int num9 = ((num3 < (float)num2) ? 1 : 0);
		int num10 = ((num4 < (float)num2) ? num : 0);
		int num11 = num6 * num + num5;
		int index = num11 + num9;
		int num12 = num11 + num10;
		int index2 = num12 + num9;
		float3 xyz = shoreVectors[num11].xyz;
		float3 xyz2 = shoreVectors[index].xyz;
		float3 xyz3 = shoreVectors[num12].xyz;
		float3 xyz4 = shoreVectors[index2].xyz;
		Vector3 vector = default(Vector3);
		vector.x = (xyz2.x - xyz.x) * num7 + xyz.x;
		vector.y = (xyz2.y - xyz.y) * num7 + xyz.y;
		vector.z = (xyz2.z - xyz.z) * num7 + xyz.z;
		Vector3 vector2 = default(Vector3);
		vector2.x = (xyz4.x - xyz3.x) * num7 + xyz3.x;
		vector2.y = (xyz4.y - xyz3.y) * num7 + xyz3.y;
		vector2.z = (xyz4.z - xyz3.z) * num7 + xyz3.z;
		float x = (vector2.x - vector.x) * num8 + vector.x;
		float z = (vector2.y - vector.y) * num8 + vector.y;
		return new ValueTuple<Vector3, float>(item2: ((vector2.z - vector.z) * num8 + vector.z) * shoreDistanceScale, item1: new Vector3(x, 0f, z));
	}

	public (Vector3 shoreDir, float shoreDist) GetCoarseVectorToShore(float normX, float normY)
	{
		return GetCoarseVectorToShore(new Vector2(normX, normY));
	}

	public Vector4 GetRawShoreVector(Vector3 pos)
	{
		Vector2 uv = default(Vector2);
		uv.x = (pos.x - TerrainMeta.Position.x) * TerrainMeta.OneOverSize.x;
		uv.y = (pos.z - TerrainMeta.Position.z) * TerrainMeta.OneOverSize.z;
		return GetRawShoreVector(uv);
	}

	public Vector4 GetRawShoreVector(Vector2 uv)
	{
		int num = shoreMapSize;
		int num2 = num - 1;
		float num3 = uv.x * (float)num2;
		float num4 = uv.y * (float)num2;
		int num5 = (int)num3;
		int num6 = (int)num4;
		num5 = ((num5 >= 0) ? num5 : 0);
		num6 = ((num6 >= 0) ? num6 : 0);
		num5 = ((num5 <= num2) ? num5 : num2);
		num6 = ((num6 <= num2) ? num6 : num2);
		return shoreVectors[num6 * num + num5];
	}

	private void InitializeWaterHeight()
	{
	}

	private void ReleaseWaterHeight()
	{
	}

	private void UpdateWaterHeight()
	{
	}
}
