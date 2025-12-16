#define UNITY_ASSERTIONS
using System.Collections.Generic;
using System.Threading.Tasks;
using GenerateErosionJobs;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class GenerateErosionSplat : ProceduralComponent
{
	public override void Process(uint seed)
	{
		if (!World.Networked)
		{
			Debug.Assert(GenerateErosion.splatPaintingData.IsValid, "GenerateErosion.splatPaintingData is not populated, GenerateErosion likely missing from WorldSetup");
			if (GenerateErosion.splatPaintingData.IsValid)
			{
				PaintSplat();
			}
		}
	}

	private void PaintSplat()
	{
		Debug.Assert(GenerateErosion.splatPaintingData.HeightMapDelta.IsCreated);
		Debug.Assert(GenerateErosion.splatPaintingData.AngleMap.IsCreated);
		TerrainSplatMap splatMap = TerrainMeta.SplatMap;
		NativeArray<float> heightMapDelta = GenerateErosion.splatPaintingData.HeightMapDelta;
		NativeArray<float> angleMap = GenerateErosion.splatPaintingData.AngleMap;
		int res = TerrainMeta.HeightMap.res;
		Parallel.For(1, res - 1, delegate(int z)
		{
			for (int i = 1; i < res - 1; i++)
			{
				angleMap[z * res + i] = TerrainMeta.HeightMap.GetSlope(i, z);
			}
		});
		splatMap.Push();
		NativeHashMap<int, int> nativeHashMap = new NativeHashMap<int, int>(splatMap.num, Allocator.TempJob);
		foreach (var (key, item) in TerrainSplat.GetType2IndexDic())
		{
			nativeHashMap.Add(key, item);
		}
		JobHandle dependency = default(JobHandle);
		GenerateErosionJobs.PaintSplatJob jobData = new GenerateErosionJobs.PaintSplatJob
		{
			HeightMapDelta = heightMapDelta.AsReadOnly(),
			HeightMapRes = TerrainMeta.HeightMap.res,
			AngleMapDeg = angleMap.AsReadOnly(),
			TopologyMap = TerrainMeta.TopologyMap.src.AsReadOnly(),
			TopologyMapRes = TerrainMeta.TopologyMap.res,
			SplatMap = splatMap.dst,
			SplatMapRes = splatMap.res,
			SplatNum = splatMap.num,
			SplatType2Index = nativeHashMap.AsReadOnly(),
			TerrainOneOverSizeX = TerrainMeta.OneOverSize.x
		};
		dependency = IJobForExtensions.ScheduleByRef(ref jobData, heightMapDelta.Length, dependency);
		nativeHashMap.Dispose(dependency);
		GenerateErosion.splatPaintingData.Dispose(dependency);
		dependency.Complete();
		splatMap.Pop();
	}
}
