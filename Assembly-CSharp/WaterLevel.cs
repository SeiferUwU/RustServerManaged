using System;
using System.Collections.Generic;
using Facepunch;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UtilityJobs;
using WaterLevelJobs;

public static class WaterLevel
{
	public struct WaterInfo
	{
		public bool isValid;

		public float currentDepth;

		public float overallDepth;

		public float surfaceLevel;

		public float terrainHeight;

		public bool artificalWater;

		public int topology;
	}

	public const float InvalidWaterHeight = -1000f;

	private static NativeReference<int> CounterRef;

	private static NativeArray<Vector3> Centers;

	private static NativeArray<float> WaterHeights;

	private static NativeArray<float> TerrainHeights;

	private static NativeArray<int> Indices;

	private static NativeArray<bool> GetIgnoreResults;

	private static NativeArray<Vector3> GetIgnoreHeadStarts;

	private static NativeArray<float> GetIgnoreHeadRadii;

	private static NativeArray<Vector2> UVs;

	private static NativeArray<int> Topologies;

	private static NativeArray<float> ShoreDists;

	private static NativeArray<float> WaveHeights;

	public static float Factor(Vector3 start, Vector3 end, float radius, bool waves, bool volumes, BaseEntity forEntity = null)
	{
		using (TimeWarning.New("WaterLevel.Factor"))
		{
			return Factor(GetWaterInfo(start, end, radius, waves, volumes, forEntity), start, end, radius);
		}
	}

	public static float Factor(in WaterInfo info, Vector3 start, Vector3 end, float radius)
	{
		if (!info.isValid)
		{
			return 0f;
		}
		return Mathf.InverseLerp(Mathf.Min(start.y, end.y) - radius, Mathf.Max(start.y, end.y) + radius, info.surfaceLevel);
	}

	public static float Factor(Bounds bounds, bool waves, bool volumes, BaseEntity forEntity = null)
	{
		using (TimeWarning.New("WaterLevel.Factor"))
		{
			if (bounds.size == Vector3.zero)
			{
				bounds.size = new Vector3(0.1f, 0.1f, 0.1f);
			}
			WaterInfo waterInfo = GetWaterInfo(bounds, waves, volumes, forEntity);
			return waterInfo.isValid ? Mathf.InverseLerp(bounds.min.y, bounds.max.y, waterInfo.surfaceLevel) : 0f;
		}
	}

	public static float Factor(in WaterInfo info, Bounds bounds)
	{
		if (bounds.size == Vector3.zero)
		{
			bounds.size = new Vector3(0.1f, 0.1f, 0.1f);
		}
		if (!info.isValid)
		{
			return 0f;
		}
		return Mathf.InverseLerp(bounds.min.y, bounds.max.y, info.surfaceLevel);
	}

	public static bool Test(Vector3 pos, bool waves, bool volumes, BaseEntity forEntity = null)
	{
		using (TimeWarning.New("WaterLevel.Test"))
		{
			return GetWaterInfo(pos, waves, volumes, forEntity).isValid;
		}
	}

	public static bool Test(in WaterInfo info, bool volumes, Vector3 pos, BaseEntity forEntity = null)
	{
		bool flag = pos.y >= info.terrainHeight - 1f && pos.y <= info.surfaceLevel;
		if (!flag && volumes)
		{
			flag = GetWaterInfoFromVolumes(pos, forEntity).isValid;
		}
		return flag;
	}

	public static (float, float) GetWaterAndTerrainSurface(Vector3 pos, bool waves, bool volumes, BaseEntity forEntity = null)
	{
		using (TimeWarning.New("WaterLevel.GetWaterDepth"))
		{
			WaterInfo waterInfo = GetWaterInfo(pos, waves, volumes, forEntity);
			return (waterInfo.surfaceLevel, waterInfo.terrainHeight);
		}
	}

	public static float GetWaterOrTerrainSurface(Vector3 pos, bool waves, bool volumes, BaseEntity forEntity = null)
	{
		using (TimeWarning.New("WaterLevel.GetWaterDepth"))
		{
			WaterInfo waterInfo = GetWaterInfo(pos, waves, volumes, forEntity);
			return Mathf.Max(waterInfo.surfaceLevel, waterInfo.terrainHeight);
		}
	}

	public static float GetWaterSurface(Vector3 pos, bool waves, bool volumes, BaseEntity forEntity = null)
	{
		using (TimeWarning.New("WaterLevel.GetWaterDepth"))
		{
			return GetWaterInfo(pos, waves, volumes, forEntity).surfaceLevel;
		}
	}

	public static float GetWaterDepth(Vector3 pos, bool waves, bool volumes, BaseEntity forEntity = null)
	{
		using (TimeWarning.New("WaterLevel.GetWaterDepth"))
		{
			return GetWaterInfo(pos, waves, volumes, forEntity).currentDepth;
		}
	}

	public static float GetOverallWaterDepth(Vector3 pos, bool waves, bool volumes, BaseEntity forEntity = null)
	{
		using (TimeWarning.New("WaterLevel.GetOverallWaterDepth"))
		{
			return GetWaterInfo(pos, waves, volumes, forEntity).overallDepth;
		}
	}

	public static Vector3 GetWaterFlowDirection(Vector3 worldPosition)
	{
		if (TerrainMeta.WaterFlowMap == null)
		{
			return Vector3.zero;
		}
		return TerrainMeta.WaterFlowMap.GetFlowDirection(worldPosition);
	}

	public static Vector3 GetWaterNormal(Vector3 pos)
	{
		return Vector3.up;
	}

	public static WaterInfo GetBuoyancyWaterInfo(Vector3 pos, Vector2 posUV, float terrainHeight, float waterHeight, bool doDeepwaterChecks, BaseEntity forEntity)
	{
		using (TimeWarning.New("WaterLevel.GetWaterInfo"))
		{
			WaterInfo result = default(WaterInfo);
			if (pos.y > waterHeight)
			{
				return GetWaterInfoFromVolumes(pos, forEntity);
			}
			bool flag = pos.y < terrainHeight - 1f;
			if (flag)
			{
				return GetWaterInfoFromVolumes(pos, forEntity);
			}
			bool flag2 = doDeepwaterChecks && (pos.y < waterHeight - 10f || TerrainMeta.OutOfBounds(pos));
			int num = (TerrainMeta.TopologyMap ? TerrainMeta.TopologyMap.GetTopologyFast(posUV) : 0);
			if ((flag || flag2 || (num & 0x3C180) == 0) && (bool)WaterSystem.Collision && WaterSystem.Collision.GetIgnore(pos))
			{
				return result;
			}
			if (flag2 && Physics.Raycast(pos, Vector3.up, out var hitInfo, 5f, 16, QueryTriggerInteraction.Collide))
			{
				waterHeight = Mathf.Min(waterHeight, hitInfo.collider.bounds.max.y);
			}
			result.isValid = true;
			result.currentDepth = Mathf.Max(0f, waterHeight - pos.y);
			result.overallDepth = Mathf.Max(0f, waterHeight - terrainHeight);
			result.surfaceLevel = waterHeight;
			result.terrainHeight = terrainHeight;
			result.topology = num;
			return result;
		}
	}

	public static WaterInfo GetWaterInfo(Vector3 pos, bool waves, bool volumes, BaseEntity forEntity = null)
	{
		using (TimeWarning.New("WaterLevel.GetWaterInfo"))
		{
			WaterInfo result = default(WaterInfo);
			float num = GetWaterLevel(pos, waves);
			float num2 = (((bool)TerrainMeta.HeightMap && TerrainMeta.HeightMap.isInitialized) ? TerrainMeta.HeightMap.GetHeight(pos) : 0f);
			result.isValid = true;
			if (pos.y > num)
			{
				result.isValid = false;
			}
			else if (pos.y < num2 - 1f)
			{
				result.isValid = false;
			}
			bool flag = false;
			if (!result.isValid && volumes)
			{
				result = GetWaterInfoFromVolumes(pos, forEntity);
				if (result.isValid)
				{
					flag = true;
					num = result.surfaceLevel;
				}
			}
			if (result.isValid && (bool)WaterSystem.Collision && WaterSystem.Collision.GetIgnore(pos))
			{
				result.isValid = false;
				num = -1000f;
			}
			result.currentDepth = Mathf.Max(0f, num - pos.y);
			if (!flag)
			{
				result.overallDepth = Mathf.Max(0f, num - num2);
			}
			result.surfaceLevel = num;
			result.terrainHeight = num2;
			return result;
		}
	}

	public static WaterInfo GetWaterInfo(Bounds bounds, bool waves, bool volumes, BaseEntity forEntity = null)
	{
		using (TimeWarning.New("WaterLevel.GetWaterInfo"))
		{
			WaterInfo result = default(WaterInfo);
			float num = GetWaterLevel(bounds.center, waves);
			float num2 = (TerrainMeta.HeightMap ? TerrainMeta.HeightMap.GetHeight(bounds.center) : 0f);
			result.isValid = true;
			if (bounds.min.y > num)
			{
				result.isValid = false;
			}
			else if (bounds.max.y < num2 - 1f)
			{
				result.isValid = false;
			}
			if (!result.isValid && volumes)
			{
				result = GetWaterInfoFromVolumes(bounds, forEntity);
				if (result.isValid)
				{
					num = result.surfaceLevel;
				}
			}
			if (result.isValid && (bool)WaterSystem.Collision && WaterSystem.Collision.GetIgnore(bounds))
			{
				result.isValid = false;
				num = -1000f;
			}
			result.currentDepth = Mathf.Max(0f, num - bounds.min.y);
			result.overallDepth = Mathf.Max(0f, num - num2);
			result.surfaceLevel = num;
			result.terrainHeight = num2;
			return result;
		}
	}

	public static void InitInternalState(int initCap)
	{
		DisposeInternalState();
		CounterRef = new NativeReference<int>(Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		Centers = new NativeArray<Vector3>(initCap, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		WaterHeights = new NativeArray<float>(initCap, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		TerrainHeights = new NativeArray<float>(initCap, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		Indices = new NativeArray<int>(initCap, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		GetIgnoreResults = new NativeArray<bool>(initCap, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		GetIgnoreHeadStarts = new NativeArray<Vector3>(initCap, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		GetIgnoreHeadRadii = new NativeArray<float>(initCap, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		UVs = new NativeArray<Vector2>(initCap, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		Topologies = new NativeArray<int>(initCap, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		ShoreDists = new NativeArray<float>(initCap, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		WaveHeights = new NativeArray<float>(initCap, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
	}

	public static void DisposeInternalState()
	{
		NativeReferenceEx.SafeDispose(ref CounterRef);
		NativeArrayEx.SafeDispose(ref Centers);
		NativeArrayEx.SafeDispose(ref WaterHeights);
		NativeArrayEx.SafeDispose(ref TerrainHeights);
		NativeArrayEx.SafeDispose(ref Indices);
		NativeArrayEx.SafeDispose(ref GetIgnoreResults);
		NativeArrayEx.SafeDispose(ref GetIgnoreHeadStarts);
		NativeArrayEx.SafeDispose(ref GetIgnoreHeadRadii);
		NativeArrayEx.SafeDispose(ref UVs);
		NativeArrayEx.SafeDispose(ref Topologies);
		NativeArrayEx.SafeDispose(ref ShoreDists);
		NativeArrayEx.SafeDispose(ref WaveHeights);
	}

	public static void GetWaterInfos(NativeArray<Vector3>.ReadOnly starts, NativeArray<Vector3>.ReadOnly ends, NativeArray<float>.ReadOnly radii, ReadOnlySpan<BaseEntity> entities, NativeArray<int>.ReadOnly indices, bool waves, bool volumes, NativeArray<WaterInfo> results)
	{
		using (TimeWarning.New("GetWaterInfos"))
		{
			NativeArrayEx.Expand(ref Centers, starts.Length, NativeArrayOptions.UninitializedMemory, copyContents: false);
			CalcCenterJobIndirect jobData = new CalcCenterJobIndirect
			{
				Results = Centers,
				Starts = starts,
				Ends = ends,
				Indices = indices
			};
			IJobExtensions.RunByRef(ref jobData);
			NativeArrayEx.Expand(ref WaterHeights, starts.Length, NativeArrayOptions.UninitializedMemory, copyContents: false);
			GetWaterLevels(Centers.AsReadOnly(), indices, waves, WaterHeights);
			NativeArrayEx.Expand(ref TerrainHeights, starts.Length, NativeArrayOptions.UninitializedMemory, copyContents: false);
			TerrainMeta.HeightMap?.GetHeightsIndirect(Centers.AsReadOnly(), indices, TerrainHeights);
			NativeArrayEx.Expand(ref Indices, starts.Length, NativeArrayOptions.UninitializedMemory, copyContents: false);
			InitialValidateInfoJobIndirect jobData2 = new InitialValidateInfoJobIndirect
			{
				Results = results,
				Starts = starts,
				Ends = ends,
				Radii = radii,
				WaterHeights = WaterHeights.AsReadOnly(),
				TerrainHeights = TerrainHeights.AsReadOnly(),
				Indices = indices
			};
			IJobExtensions.RunByRef(ref jobData2);
			if (volumes)
			{
				using (TimeWarning.New("WaterTestFromVolumes"))
				{
					GatherInvalidInfosJobIndirect jobData3 = new GatherInvalidInfosJobIndirect
					{
						InvalidIndices = Indices,
						InvalidIndexCount = CounterRef,
						Infos = results.AsReadOnly(),
						Indices = indices
					};
					IJobExtensions.RunByRef(ref jobData3);
					int value = CounterRef.Value;
					if (value > 0)
					{
						NativeArray<int> source = Indices.GetSubArray(0, value);
						BaseEntity.WaterTestFromVolumesIndirect(entities, starts, ends, radii, source, results);
						UpdateWaterHeightsJobIndirect jobData4 = new UpdateWaterHeightsJobIndirect
						{
							WaterHeights = WaterHeights,
							Infos = results,
							Indices = source
						};
						IJobExtensions.RunByRef(ref jobData4);
					}
				}
			}
			if ((bool)WaterSystem.Collision)
			{
				using (TimeWarning.New("WaterSystem.Collision"))
				{
					GatherValidInfosJobIndirect jobData5 = new GatherValidInfosJobIndirect
					{
						ValidIndices = Indices,
						ValidIndexCount = CounterRef,
						Infos = results.AsReadOnly(),
						Indices = indices
					};
					IJobExtensions.RunByRef(ref jobData5);
					int value2 = CounterRef.Value;
					if (value2 > 0)
					{
						using (TimeWarning.New("WaterSystem.Collision.Entity"))
						{
							NativeArray<int> subArray = Indices.GetSubArray(0, value2);
							NativeArrayEx.Expand(ref GetIgnoreResults, starts.Length, NativeArrayOptions.UninitializedMemory, copyContents: false);
							WaterSystem.Collision.GetIgnoreIndirect(starts, ends, radii, subArray.AsReadOnly(), GetIgnoreResults);
							NativeArrayEx.Expand(ref GetIgnoreHeadStarts, starts.Length, NativeArrayOptions.UninitializedMemory, copyContents: false);
							NativeArrayEx.Expand(ref GetIgnoreHeadRadii, starts.Length, NativeArrayOptions.UninitializedMemory, copyContents: false);
							SetupHeadQueryJobIndirect jobData6 = new SetupHeadQueryJobIndirect
							{
								Indices = subArray,
								QueryIndexCount = CounterRef,
								QueryStarts = GetIgnoreHeadStarts,
								QueryRadii = GetIgnoreHeadRadii,
								ValidInfos = GetIgnoreResults.AsReadOnly(),
								Starts = starts,
								Ends = ends,
								Radii = radii
							};
							IJobExtensions.RunByRef(ref jobData6);
							int value3 = CounterRef.Value;
							if (value3 > 0)
							{
								using (TimeWarning.New("WaterSystem.Collision.Head"))
								{
									NativeArray<int> subArray2 = Indices.GetSubArray(0, value3);
									WaterSystem.Collision.GetIgnoreIndirect(GetIgnoreHeadStarts.AsReadOnly(), GetIgnoreHeadRadii.AsReadOnly(), subArray2.AsReadOnly(), GetIgnoreResults);
									ApplyHeadQueryResultsJobIndirect jobData7 = new ApplyHeadQueryResultsJobIndirect
									{
										WaterHeights = WaterHeights,
										Infos = results,
										Indices = subArray2.AsReadOnly(),
										ValidInfos = GetIgnoreResults.AsReadOnly(),
										Starts = GetIgnoreHeadStarts.AsReadOnly()
									};
									IJobExtensions.RunByRef(ref jobData7);
								}
							}
						}
					}
				}
			}
			ResolveWaterInfosJobIndirect jobData8 = new ResolveWaterInfosJobIndirect
			{
				Infos = results,
				Starts = starts,
				Ends = ends,
				Radii = radii,
				WaterHeights = WaterHeights.AsReadOnly(),
				TerrainHeights = TerrainHeights.AsReadOnly(),
				Indices = indices
			};
			IJobExtensions.RunByRef(ref jobData8);
		}
	}

	public static WaterInfo GetWaterInfo(Vector3 start, Vector3 end, float radius, bool waves, bool volumes, BaseEntity forEntity = null)
	{
		using (TimeWarning.New("WaterLevel.GetWaterInfo"))
		{
			WaterInfo result = default(WaterInfo);
			Vector3 vector = (start + end) * 0.5f;
			float num = Mathf.Min(start.y, end.y) - radius;
			float num2 = Mathf.Max(start.y, end.y) + radius;
			float num3 = GetWaterLevel(vector, waves);
			float num4 = (TerrainMeta.HeightMap ? TerrainMeta.HeightMap.GetHeight(vector) : 0f);
			result.isValid = true;
			if (num > num3)
			{
				result.isValid = false;
			}
			else if (num2 < num4 - 1f)
			{
				result.isValid = false;
			}
			if (result.isValid && num4 >= num3 + 0.015f)
			{
				result.isValid = false;
			}
			if (!result.isValid && volumes)
			{
				result = GetWaterInfoFromVolumes(start, end, radius, forEntity);
				if (result.isValid)
				{
					num3 = result.surfaceLevel;
				}
			}
			if (result.isValid && (bool)WaterSystem.Collision && WaterSystem.Collision.GetIgnore(start, end, radius))
			{
				Vector3 pos = vector.WithY(Mathf.Lerp(num, num2, 0.75f));
				if (!WaterSystem.Collision.GetIgnore(pos))
				{
					num3 = Mathf.Min(num3, pos.y);
				}
				else
				{
					result.isValid = false;
					num3 = -1000f;
				}
			}
			result.currentDepth = Mathf.Max(0f, num3 - num);
			result.overallDepth = Mathf.Max(0f, num3 - num4);
			result.surfaceLevel = num3;
			result.terrainHeight = num4;
			return result;
		}
	}

	public static WaterInfo GetWaterInfo(Camera cam, bool waves, bool volumes, BaseEntity forEntity = null)
	{
		using (TimeWarning.New("WaterLevel.GetWaterInfo"))
		{
			waves = waves && WaterSystem.Instance != null;
			float num = WaterSystem.OceanLevel;
			if (waves)
			{
				num += WaterSystem.Instance.oceanSimulation.MinLevel();
			}
			if (cam.transform.position.y < num - 1f)
			{
				return GetWaterInfo(cam.transform.position, waves, volumes, forEntity);
			}
			return GetWaterInfo(cam.transform.position - Vector3.up, waves, volumes, forEntity);
		}
	}

	public static float GetWaterLevel(Vector3 pos, bool waves)
	{
		waves = waves && WaterSystem.Instance != null;
		float normX = TerrainMeta.NormalizeX(pos.x);
		float normZ = TerrainMeta.NormalizeZ(pos.z);
		float num = (TerrainMeta.WaterMap ? TerrainMeta.WaterMap.GetHeight(normX, normZ) : TerrainMeta.Position.y);
		float num2 = WaterSystem.OceanLevel;
		if (waves)
		{
			num2 += WaterSystem.Instance.oceanSimulation.MaxLevel();
		}
		if (num < num2 && (!TerrainMeta.TopologyMap || TerrainMeta.TopologyMap.GetTopology(normX, normZ, 384)))
		{
			float num3 = WaterSystem.OceanLevel;
			if (waves)
			{
				num3 += WaterSystem.Instance.oceanSimulation.GetHeight(pos);
			}
			return Mathf.Max(num, num3);
		}
		return num;
	}

	public static float RaycastWaterColliders(Vector3 pos)
	{
		if (!Physics.Raycast(pos.WithY(TerrainMeta.Max.y), Vector3.down, out var hitInfo, TerrainMeta.Size.y, 16, QueryTriggerInteraction.Collide))
		{
			return WaterSystem.OceanLevel;
		}
		return hitInfo.point.y;
	}

	public static void GetWaterLevels(NativeArray<Vector3>.ReadOnly positions, NativeArray<int>.ReadOnly indices, bool waves, NativeArray<float> heights)
	{
		using (TimeWarning.New("WaterLevels"))
		{
			waves = waves && WaterSystem.Instance != null;
			float num = WaterSystem.OceanLevel;
			if (waves)
			{
				num += WaterSystem.Instance.oceanSimulation.MaxLevel();
			}
			NativeArrayEx.Expand(ref UVs, positions.Length, NativeArrayOptions.UninitializedMemory, copyContents: false);
			NativeArray<Vector2> subArray = UVs.GetSubArray(0, positions.Length);
			ToUVJobIndirect jobData = new ToUVJobIndirect
			{
				UV = subArray,
				Pos = positions,
				Indices = indices,
				TerrainPos = TerrainMeta.Position.XZ2D(),
				TerrainOneOverSize = TerrainMeta.OneOverSize.XZ2D()
			};
			IJobExtensions.RunByRef(ref jobData);
			if ((bool)TerrainMeta.WaterMap)
			{
				TerrainMeta.WaterMap.GetHeightsIndirect(subArray.AsReadOnly(), indices, heights);
			}
			else
			{
				FillJob<float> jobData2 = new FillJob<float>
				{
					Values = heights,
					Value = TerrainMeta.Position.y
				};
				IJobExtensions.RunByRef(ref jobData2);
			}
			NativeArrayEx.Expand(ref Topologies, positions.Length, NativeArrayOptions.UninitializedMemory, copyContents: false);
			NativeArray<int> subArray2 = Topologies.GetSubArray(0, positions.Length);
			if ((bool)TerrainMeta.TopologyMap)
			{
				TerrainMeta.TopologyMap.GetTopologiesIndirect(subArray.AsReadOnly(), indices, subArray2);
			}
			else
			{
				FillJob<int> jobData3 = new FillJob<int>
				{
					Values = subArray2,
					Value = 384
				};
				IJobExtensions.RunByRef(ref jobData3);
			}
			if (!waves)
			{
				ApplyMaxHeightsJobIndirect jobData4 = new ApplyMaxHeightsJobIndirect
				{
					Heights = heights,
					Topologies = subArray2.AsReadOnly(),
					Indices = indices,
					WaterLevel = num,
					OceanLevel = WaterSystem.OceanLevel
				};
				IJobExtensions.RunByRef(ref jobData4);
				return;
			}
			NativeArrayEx.Expand(ref Indices, positions.Length, NativeArrayOptions.UninitializedMemory, copyContents: false);
			GatherWavesIndicesJobIndirect jobData5 = new GatherWavesIndicesJobIndirect
			{
				WaveIndices = Indices,
				WaveIndexCount = CounterRef,
				Topologies = subArray2.AsReadOnly(),
				Heights = heights.AsReadOnly(),
				Indices = indices,
				WaterLevel = num
			};
			IJobExtensions.RunByRef(ref jobData5);
			int value = CounterRef.Value;
			if (value == 0)
			{
				return;
			}
			using (TimeWarning.New("Waves"))
			{
				NativeArrayEx.Expand(ref TerrainHeights, positions.Length, NativeArrayOptions.UninitializedMemory, copyContents: false);
				NativeArrayEx.Expand(ref ShoreDists, positions.Length, NativeArrayOptions.UninitializedMemory, copyContents: false);
				NativeArrayEx.Expand(ref WaveHeights, positions.Length, NativeArrayOptions.UninitializedMemory, copyContents: false);
				NativeArray<int>.ReadOnly indices2 = Indices.GetSubArray(0, value).AsReadOnly();
				if ((bool)TerrainMeta.HeightMap)
				{
					TerrainMeta.HeightMap.GetHeightsFastIndirect(subArray.AsReadOnly(), indices2, TerrainHeights);
				}
				else
				{
					FillJob<float> jobData6 = new FillJob<float>
					{
						Values = TerrainHeights,
						Value = 0f
					};
					IJobExtensions.RunByRef(ref jobData6);
				}
				if ((bool)TerrainTexturing.Instance)
				{
					TerrainTexturing.Instance.GetCoarseDistancesToShoreIndirect(subArray.AsReadOnly(), indices2, ShoreDists);
				}
				else
				{
					FillJob<float> jobData7 = new FillJob<float>
					{
						Values = ShoreDists,
						Value = 0f
					};
					IJobExtensions.RunByRef(ref jobData7);
				}
				WaterSystem.Instance.oceanSimulation.GetHeightsIndirect(positions, ShoreDists.AsReadOnly(), TerrainHeights.AsReadOnly(), indices2, WaveHeights);
				SelectMaxWaterLevelJobIndirect jobData8 = new SelectMaxWaterLevelJobIndirect
				{
					Heights = heights,
					DynamicHeights = WaveHeights.AsReadOnly(),
					Indices = indices2,
					OceanLevel = WaterSystem.OceanLevel
				};
				IJobExtensions.RunByRef(ref jobData8);
			}
		}
	}

	private static WaterInfo GetWaterInfoFromVolumes(Bounds bounds, BaseEntity forEntity)
	{
		WaterInfo info = default(WaterInfo);
		if (forEntity == null)
		{
			List<WaterVolume> obj = Pool.Get<List<WaterVolume>>();
			Vis.Components(new OBB(bounds), obj, 262144);
			using (List<WaterVolume>.Enumerator enumerator = obj.GetEnumerator())
			{
				while (enumerator.MoveNext() && !enumerator.Current.Test(bounds, out info))
				{
				}
			}
			Pool.FreeUnmanaged(ref obj);
			return info;
		}
		forEntity.WaterTestFromVolumes(bounds, out info);
		return info;
	}

	private static WaterInfo GetWaterInfoFromVolumes(Vector3 pos, BaseEntity forEntity)
	{
		WaterInfo info = default(WaterInfo);
		if (forEntity == null)
		{
			List<WaterVolume> obj = Pool.Get<List<WaterVolume>>();
			Vis.Components(pos, 0.1f, obj, 262144);
			foreach (WaterVolume item in obj)
			{
				if (item.Test(pos, out info))
				{
					info.artificalWater = !item.naturalSource;
					break;
				}
			}
			Pool.FreeUnmanaged(ref obj);
			return info;
		}
		forEntity.WaterTestFromVolumes(pos, out info);
		return info;
	}

	private static WaterInfo GetWaterInfoFromVolumes(Vector3 start, Vector3 end, float radius, BaseEntity forEntity)
	{
		WaterInfo info = default(WaterInfo);
		if (forEntity == null)
		{
			List<WaterVolume> obj = Pool.Get<List<WaterVolume>>();
			Vis.Components(start, end, radius, obj, 262144);
			using (List<WaterVolume>.Enumerator enumerator = obj.GetEnumerator())
			{
				while (enumerator.MoveNext() && !enumerator.Current.Test(start, end, radius, out info))
				{
				}
			}
			Pool.FreeUnmanaged(ref obj);
			return info;
		}
		forEntity.WaterTestFromVolumes(start, end, radius, out info);
		return info;
	}

	public static WaterInfo InitialValidate(Vector3 start, Vector3 end, float radius, float waterHeight, float terrainHeight)
	{
		WaterInfo result = new WaterInfo
		{
			isValid = true
		};
		float num = Mathf.Min(start.y, end.y) - radius;
		float num2 = Mathf.Max(start.y, end.y) + radius;
		if (num > waterHeight)
		{
			result.isValid = false;
		}
		else if (num2 < terrainHeight - 1f)
		{
			result.isValid = false;
		}
		return result;
	}
}
