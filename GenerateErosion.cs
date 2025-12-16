#define UNITY_ASSERTIONS
using System;
using System.Threading.Tasks;
using GenerateErosionJobs;
using Unity.Collections;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

public class GenerateErosion : ProceduralComponent
{
	public struct SplatPaintingData : IDisposable
	{
		public bool IsValid;

		public readonly NativeArray<float> HeightMapDelta;

		public readonly NativeArray<float> AngleMap;

		public SplatPaintingData(NativeArray<float> heightMapDelta, NativeArray<float> angleMap)
		{
			HeightMapDelta = heightMapDelta;
			AngleMap = angleMap;
			IsValid = true;
		}

		public void Dispose()
		{
			IsValid = false;
			if (HeightMapDelta.IsCreated)
			{
				HeightMapDelta.Dispose();
			}
			if (AngleMap.IsCreated)
			{
				AngleMap.Dispose();
			}
		}

		public void Dispose(JobHandle inputDeps)
		{
			IsValid = false;
			if (HeightMapDelta.IsCreated)
			{
				HeightMapDelta.Dispose(inputDeps);
			}
			if (AngleMap.IsCreated)
			{
				AngleMap.Dispose(inputDeps);
			}
		}
	}

	public static SplatPaintingData splatPaintingData;

	public override void Process(uint seed)
	{
		if (!World.Networked)
		{
			GridErosion(seed);
		}
	}

	private static int GetBatchSize(int length)
	{
		int num = length / JobsUtility.JobWorkerCount;
		if (num < 64)
		{
			return 64;
		}
		return num;
	}

	private void GridErosion(uint seed)
	{
		using (TimeWarning.New("GridErosion"))
		{
			TerrainHeightMap heightMap = TerrainMeta.HeightMap;
			heightMap.Push();
			NativeArray<short> src = heightMap.src;
			NativeArray<short> dst = heightMap.dst;
			NativeArray<float> minTerrainHeightMap = new NativeArray<float>(heightMap.src.Length, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			NativeArray<float> waterMap = new NativeArray<float>(heightMap.src.Length, Allocator.Persistent);
			NativeList<int> nativeList = new NativeList<int>(heightMap.src.Length, Allocator.Persistent);
			NativeArray<float4> fluxMap = new NativeArray<float4>(heightMap.src.Length, Allocator.Persistent);
			NativeArray<float2> velocityMap = new NativeArray<float2>(heightMap.src.Length, Allocator.Persistent);
			NativeArray<float> nativeArray = new NativeArray<float>(heightMap.src.Length, Allocator.Persistent);
			NativeArray<float> copyTarget = new NativeArray<float>(heightMap.src.Length, Allocator.Persistent);
			NativeArray<float> angleMap = new NativeArray<float>(heightMap.src.Length, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			Debug.Assert(waterMap.Length == heightMap.src.Length);
			Debug.Assert(fluxMap.Length == heightMap.src.Length);
			Debug.Assert(velocityMap.Length == heightMap.src.Length);
			Debug.Assert(nativeArray.Length == heightMap.src.Length);
			float num = TerrainMeta.Size.x / (float)heightMap.res * TerrainMeta.Size.z / (float)heightMap.res;
			float invGridCellSquareSize = 1f / num;
			float pipeLength = 1f;
			float pipeArea = 1f;
			JobHandle dependsOn = default(JobHandle);
			NativeArray<float> nativeArray2 = new NativeArray<float>(src.Length, Allocator.Persistent);
			NativeArray<float> nativeArray3 = new NativeArray<float>(nativeArray2, Allocator.Persistent);
			dependsOn = IJobParallelForBatchExtensions.Schedule(new GenerateErosionJobs.PrepareMapJob
			{
				HeightMapAsShort = src.AsReadOnly(),
				HeightMapAsFloat = nativeArray2,
				OceanIndicesWriter = nativeList.AsParallelWriter(),
				OceanLevel = WaterSystem.OceanLevel,
				TerrainPositionY = TerrainMeta.Position.y,
				TerrainSizeY = TerrainMeta.Size.y
			}, src.Length, GetBatchSize(src.Length), dependsOn);
			IJobParallelForExtensions.Schedule(new GenerateErosionJobs.CalcMinHeightMapJob
			{
				TerrainHeightMap = nativeArray2.AsReadOnly(),
				MinTerrainHeightMap = minTerrainHeightMap,
				HeightMapRes = TerrainMeta.HeightMap.res,
				TopologyMap = TerrainMeta.TopologyMap.src.AsReadOnly(),
				TopologyMapRes = TerrainMeta.TopologyMap.res,
				OceanHeight = WaterSystem.OceanLevel,
				TerrainOneOverSizeX = TerrainMeta.OneOverSize.x
			}, nativeArray2.Length, GetBatchSize(nativeArray2.Length), dependsOn).Complete();
			dependsOn = default(JobHandle);
			NativeArray<float> copyTarget2 = new NativeArray<float>(src.Length, Allocator.Persistent);
			GenerateErosionJobs.CopyArrayJob<float> jobData = new GenerateErosionJobs.CopyArrayJob<float>
			{
				CopyTarget = copyTarget2,
				CopySource = nativeArray2
			};
			dependsOn = JobHandle.CombineDependencies(job1: new GenerateErosionJobs.CopyArrayJob<float>
			{
				CopyTarget = nativeArray3,
				CopySource = nativeArray2
			}.Schedule(dependsOn), job0: jobData.Schedule(dependsOn));
			int num2 = 32;
			int num3 = 32;
			int num4 = (heightMap.res + num2 - 1) / num2;
			int num5 = (heightMap.res + num3 - 1) / num3;
			int num6 = num4 * num5;
			for (int i = 0; i < 512; i++)
			{
				GenerateErosionJobs.RefillOceanJob jobData2 = new GenerateErosionJobs.RefillOceanJob
				{
					OceanIndices = nativeList.AsReadOnly(),
					HeightMap = nativeArray2.AsReadOnly(),
					OceanLevel = WaterSystem.OceanLevel,
					WaterMap = waterMap
				};
				dependsOn = IJobParallelForExtensions.ScheduleByRef(ref jobData2, nativeList.Length, GetBatchSize(nativeList.Length), dependsOn);
				GenerateErosionJobs.WaterIncrementationJob jobData3 = new GenerateErosionJobs.WaterIncrementationJob
				{
					WaterMap = waterMap,
					WaterFillRate = 0.04f,
					DT = 0.06f
				};
				dependsOn = IJobParallelForExtensions.ScheduleByRef(ref jobData3, waterMap.Length, GetBatchSize(waterMap.Length), dependsOn);
				GenerateErosionJobs.CalculateOutputFluxJob jobData4 = new GenerateErosionJobs.CalculateOutputFluxJob
				{
					TerrainHeightMapFloatVal = nativeArray2.AsReadOnly(),
					WaterMap = waterMap.AsReadOnly(),
					FluxMap = fluxMap,
					Res = heightMap.res,
					DT = 0.06f,
					GridCellSquareSize = num,
					PipeLength = pipeLength,
					PipeArea = pipeArea
				};
				dependsOn = IJobParallelForExtensions.ScheduleByRef(ref jobData4, fluxMap.Length, GetBatchSize(fluxMap.Length), dependsOn);
				GenerateErosionJobs.AdjustWaterHeightByFluxJob jobData5 = new GenerateErosionJobs.AdjustWaterHeightByFluxJob
				{
					WaterMap = waterMap,
					VelocityMap = velocityMap,
					FluxMap = fluxMap.AsReadOnly(),
					Res = heightMap.res,
					DT = 0.06f,
					InvGridCellSquareSize = invGridCellSquareSize
				};
				dependsOn = IJobParallelForExtensions.ScheduleByRef(ref jobData5, waterMap.Length, GetBatchSize(waterMap.Length), dependsOn);
				GenerateErosionJobs.TileCalculateAngleMap jobData6 = new GenerateErosionJobs.TileCalculateAngleMap
				{
					AngleMap = angleMap,
					TerrainHeightMapSrcFloat = nativeArray2.AsReadOnly(),
					NormY = heightMap.normY,
					Res = heightMap.res,
					TileSizeX = num2,
					TileSizeZ = num3,
					NumXTiles = num4
				};
				dependsOn = IJobParallelForExtensions.ScheduleByRef(ref jobData6, num6, num6 / JobsUtility.JobWorkerCount, dependsOn);
				GenerateErosionJobs.ErosionAndDepositionJob jobData7 = new GenerateErosionJobs.ErosionAndDepositionJob
				{
					SedimentMap = nativeArray,
					MinTerrainHeightMap = minTerrainHeightMap.AsReadOnly(),
					TerrainHeightMapSrcFloat = nativeArray2.AsReadOnly(),
					TerrainHeightMapDstFloat = nativeArray3,
					WaterMap = waterMap,
					VelocityMap = velocityMap.AsReadOnly(),
					AngleMap = angleMap.AsReadOnly(),
					DT = 0.06f
				};
				dependsOn = IJobParallelForExtensions.ScheduleByRef(ref jobData7, nativeArray.Length, GetBatchSize(nativeArray.Length), dependsOn);
				GenerateErosionJobs.CopyArrayJob<float> jobData8 = new GenerateErosionJobs.CopyArrayJob<float>
				{
					CopyTarget = copyTarget,
					CopySource = nativeArray
				};
				dependsOn = IJobExtensions.ScheduleByRef(ref jobData8, dependsOn);
				GenerateErosionJobs.CopyArrayJob<float> jobData9 = new GenerateErosionJobs.CopyArrayJob<float>
				{
					CopyTarget = nativeArray2,
					CopySource = nativeArray3
				};
				GenerateErosionJobs.TransportSedimentJob jobData10 = new GenerateErosionJobs.TransportSedimentJob
				{
					SedimentMap = nativeArray,
					SedimentReadOnlyMap = copyTarget.AsReadOnly(),
					VelocityMap = velocityMap.AsReadOnly(),
					Res = heightMap.res,
					DT = 0.06f
				};
				dependsOn = JobHandle.CombineDependencies(IJobExtensions.ScheduleByRef(ref jobData9, dependsOn), IJobParallelForExtensions.ScheduleByRef(ref jobData10, nativeArray.Length, GetBatchSize(nativeArray.Length), dependsOn));
				GenerateErosionJobs.EvaporationJob jobData11 = new GenerateErosionJobs.EvaporationJob
				{
					WaterMap = waterMap,
					DT = 0.06f,
					EvaporationRate = 0.015f
				};
				dependsOn = IJobParallelForExtensions.ScheduleByRef(ref jobData11, waterMap.Length, GetBatchSize(waterMap.Length), dependsOn);
			}
			GenerateErosionJobs.CopyBackFloatHeightToShortHeightJob jobData12 = new GenerateErosionJobs.CopyBackFloatHeightToShortHeightJob
			{
				HeightMapAsFloat = nativeArray2.AsReadOnly(),
				HeightMapAsShort = dst,
				TerrainOneOverSizeY = TerrainMeta.OneOverSize.y,
				TerrainPositionY = TerrainMeta.Position.y
			};
			dependsOn = IJobParallelForExtensions.ScheduleByRef(ref jobData12, nativeArray2.Length, GetBatchSize(nativeArray2.Length), dependsOn);
			NativeArray<float> nativeArray4 = new NativeArray<float>(nativeArray2.Length, Allocator.Persistent);
			GenerateErosionJobs.PopulateDeltaHeightJob jobData13 = new GenerateErosionJobs.PopulateDeltaHeightJob
			{
				HeightMapOriginal = copyTarget2.AsReadOnly(),
				HeightMap = nativeArray2.AsReadOnly(),
				DeltaHeightMap = nativeArray4
			};
			dependsOn = IJobParallelForExtensions.ScheduleByRef(ref jobData13, nativeArray4.Length, GetBatchSize(nativeArray4.Length), dependsOn);
			minTerrainHeightMap.Dispose(dependsOn);
			waterMap.Dispose(dependsOn);
			fluxMap.Dispose(dependsOn);
			velocityMap.Dispose(dependsOn);
			nativeArray.Dispose(dependsOn);
			copyTarget.Dispose(dependsOn);
			nativeArray2.Dispose(dependsOn);
			nativeArray3.Dispose(dependsOn);
			nativeList.Dispose(dependsOn);
			copyTarget2.Dispose(dependsOn);
			dependsOn.Complete();
			heightMap.Pop();
			splatPaintingData = new SplatPaintingData(nativeArray4, angleMap);
		}
	}

	private void OnDestroy()
	{
		if (splatPaintingData.IsValid)
		{
			splatPaintingData.Dispose();
		}
	}

	private void OldErosion(uint seed)
	{
		TerrainTopologyMap topologyMap = TerrainMeta.TopologyMap;
		TerrainHeightMap heightmap = TerrainMeta.HeightMap;
		TerrainSplatMap splatmap = TerrainMeta.SplatMap;
		int erosion_res = heightmap.res;
		float[] erosion = new float[erosion_res * erosion_res];
		int deposit_res = splatmap.res;
		float[] deposit = new float[deposit_res * deposit_res];
		for (float num = TerrainMeta.Position.z; num < TerrainMeta.Position.z + TerrainMeta.Size.z; num += 10f)
		{
			for (float num2 = TerrainMeta.Position.x; num2 < TerrainMeta.Position.x + TerrainMeta.Size.x; num2 += 10f)
			{
				Vector3 worldPos = new Vector3(num2, 0f, num);
				float num3 = (worldPos.y = heightmap.GetHeight(worldPos));
				if (worldPos.y <= 15f)
				{
					continue;
				}
				Vector3 normal = heightmap.GetNormal(worldPos);
				if (normal.y <= 0.01f || normal.y >= 0.99f)
				{
					continue;
				}
				Vector2 normalized = normal.XZ2D().normalized;
				Vector2 vector = normalized;
				float num4 = 0f;
				float num5 = 0f;
				for (int i = 0; i < 300; i++)
				{
					worldPos.x += normalized.x;
					worldPos.z += normalized.y;
					if (Vector3.Angle(normalized, vector) > 90f)
					{
						break;
					}
					float num6 = TerrainMeta.NormalizeX(worldPos.x);
					float num7 = TerrainMeta.NormalizeZ(worldPos.z);
					int topology = topologyMap.GetTopology(num6, num7);
					if ((topology & 0xB4990) != 0)
					{
						break;
					}
					float height = heightmap.GetHeight(num6, num7);
					if (height > num3 + 8f)
					{
						break;
					}
					float num8 = Mathf.Min(height, num3);
					worldPos.y = Mathf.Lerp(worldPos.y, num8, 0.5f);
					normal = heightmap.GetNormal(worldPos);
					normalized = Vector2.Lerp(normalized, normal.XZ2D().normalized, 0.5f).normalized;
					num3 = num8;
					float num9 = 0f;
					float target = 0f;
					if ((topology & 0x800400) == 0)
					{
						float value = Vector3.Angle(Vector3.up, normal);
						num9 = Mathf.InverseLerp(5f, 15f, value);
						target = 1f;
						if ((topology & 0x8000) == 0)
						{
							target = num9;
						}
					}
					num4 = Mathf.MoveTowards(num4, num9, 0.05f);
					num5 = Mathf.MoveTowards(num5, target, 0.05f);
					if ((topologyMap.GetTopology(num6, num7, 10f) & 2) == 0)
					{
						int num10 = Mathf.Clamp((int)(num6 * (float)erosion_res), 0, erosion_res - 1);
						int num11 = Mathf.Clamp((int)(num7 * (float)erosion_res), 0, erosion_res - 1);
						int num12 = Mathf.Clamp((int)(num6 * (float)deposit_res), 0, deposit_res - 1);
						int num13 = Mathf.Clamp((int)(num7 * (float)deposit_res), 0, deposit_res - 1);
						erosion[num11 * erosion_res + num10] += num4;
						deposit[num13 * deposit_res + num12] += num5;
					}
				}
			}
		}
		Parallel.For(1, erosion_res - 1, delegate(int z)
		{
			for (int j = 1; j < erosion_res - 1; j++)
			{
				float t = CalculateDelta(erosion, erosion_res, j, z, 1f, 0.8f, 0.6f);
				float delta = (0f - Mathf.Lerp(0f, 0.25f, t)) * TerrainMeta.OneOverSize.y;
				heightmap.AddHeight(j, z, delta);
			}
		});
		Parallel.For(1, deposit_res - 1, delegate(int z)
		{
			for (int j = 1; j < deposit_res - 1; j++)
			{
				float splat = splatmap.GetSplat(j, z, 2);
				float splat2 = splatmap.GetSplat(j, z, 4);
				if (splat > 0.1f || splat2 > 0.1f)
				{
					float value2 = CalculateDelta(deposit, deposit_res, j, z, 1f, 0.4f, 0.2f);
					value2 = Mathf.InverseLerp(1f, 3f, value2);
					value2 = Mathf.Lerp(0f, 0.5f, value2);
					splatmap.AddSplat(j, z, 128, value2);
				}
				else
				{
					float value3 = CalculateDelta(deposit, deposit_res, j, z, 1f, 0.2f, 0.1f);
					float value4 = CalculateDelta(deposit, deposit_res, j, z, 1f, 0.8f, 0.4f);
					value3 = Mathf.InverseLerp(1f, 3f, value3);
					value4 = Mathf.InverseLerp(1f, 3f, value4);
					value3 = Mathf.Lerp(0f, 1f, value3);
					value4 = Mathf.Lerp(0f, 1f, value4);
					splatmap.AddSplat(j, z, 1, value4 * 0.5f);
					splatmap.AddSplat(j, z, 64, value3 * 0.7f);
					splatmap.AddSplat(j, z, 128, value3 * 0.5f);
				}
			}
		});
		static float CalculateDelta(float[] data, int res, int x, int z, float cntr, float side, float diag)
		{
			int num14 = x - 1;
			int num15 = x + 1;
			int num16 = z - 1;
			int num17 = z + 1;
			side /= 4f;
			diag /= 4f;
			float num18 = data[z * res + x];
			float num19 = data[z * res + num14] + data[z * res + num15] + data[num17 * res + x] + data[num17 * res + x];
			float num20 = data[num16 * res + num14] + data[num16 * res + num15] + data[num17 * res + num14] + data[num17 * res + num15];
			return cntr * num18 + side * num19 + diag * num20;
		}
	}
}
