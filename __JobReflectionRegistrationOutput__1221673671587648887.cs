using System;
using AntiHackJobs;
using BasePlayerJobs;
using CoarseQueryGridJobs;
using CompanionServer.Cameras;
using Facepunch.MarchingCubes;
using Facepunch.NativeMeshSimplification;
using GamePhysicsJobs;
using GenerateErosionJobs;
using HitBoxSystemJobs;
using Instancing;
using OceanSimulationJobs;
using ProjectileJobs;
using Rust.Water5;
using ServerOcclusionJobs;
using TerrainHeightMapJobs;
using TerrainTexturingJobs;
using TerrainTopologyMapJobs;
using TerrainWaterMapJobs;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;
using UtilityJobs;
using WaterLevelJobs;
using WaterSystemJobs;

[Unity.Jobs.DOTSCompilerGenerated]
internal class __JobReflectionRegistrationOutput__1221673671587648887
{
	public static void CreateJobReflectionData()
	{
		try
		{
			IJobExtensions.EarlyJobInit<FishShoal.FishCollisionGatherJob>();
			IJobExtensions.EarlyJobInit<FishShoal.FishCollisionProcessJob>();
			IJobParallelForExtensions.EarlyJobInit<FishShoal.FishUpdateJob>();
			IJobExtensions.EarlyJobInit<FishShoal.KillFish>();
			IJobParallelForTransformExtensions.EarlyJobInit<QueryVisJobs.ConstructCommandsJob>();
			IJobParallelForTransformExtensions.EarlyJobInit<QueryVisJobs.CheckWaterLevelVisibilityJob>();
			IJobParallelForTransformExtensions.EarlyJobInit<TransformLineRenderer.LineRendererUpdateJob>();
			IJobExtensions.EarlyJobInit<global::AddAndBlurSphereJob>();
			IJobForExtensions.EarlyJobInit<global::BoxBlur3DJob>();
			IJobExtensions.EarlyJobInit<BoxBlurCylinderJob>();
			IJobExtensions.EarlyJobInit<global::BoxBlurSphereJob>();
			IJobExtensions.EarlyJobInit<CarveAndBlurCylinderJob>();
			IJobExtensions.EarlyJobInit<global::CarveAndBlurSphereJob>();
			IJobExtensions.EarlyJobInit<global::CleanFloatingIslandsJob>();
			IJobParallelForExtensions.EarlyJobInit<Hopper.FillRaycastJob>();
			IJobExtensions.EarlyJobInit<PostCullingJob>();
			IJobExtensions.EarlyJobInit<GenerateAscSeqListJob>();
			IJobExtensions.EarlyJobInit<InvertIndexListJob>();
			IJobExtensions.EarlyJobInit<RaycastSamplePositionsJob>();
			IJobExtensions.EarlyJobInit<RaycastBufferSetupJob>();
			IJobParallelForExtensions.EarlyJobInit<RaycastRaySetupJob>();
			IJobParallelForExtensions.EarlyJobInit<RaycastRayProcessingJob>();
			IJobExtensions.EarlyJobInit<RaycastOutputCompressJob>();
			IJobExtensions.EarlyJobInit<RaycastColliderProcessingJob>();
			IJobExtensions.EarlyJobInit<GatherPlayersWithTicksJob>();
			IJobExtensions.EarlyJobInit<BuildLayerMasksJob>();
			IJobExtensions.EarlyJobInit<GatherHitIndicesJob>();
			IJobExtensions.EarlyJobInit<BuildBatchLookupMapJob>();
			IJobExtensions.EarlyJobInit<GatherNoClipBatchesJob>();
			IJobExtensions.EarlyJobInit<FindValidIndicesJob>();
			IJobExtensions.EarlyJobInit<InsideTerrainHeightsChecksJob>();
			IJobExtensions.EarlyJobInit<ScatterInvertedBool>();
			IJobForExtensions.EarlyJobInit<GenerateInsideMeshCommandsJob>();
			IJobForExtensions.EarlyJobInit<CheckInsideMeshHitsJob>();
			IJobForExtensions.EarlyJobInit<FilterInsideMeshHitsJob>();
			IJobExtensions.EarlyJobInit<PreCullingJob>();
			IJobParallelForDeferExtensions.EarlyJobInit<WaterSystemJobs.FillFalseJobDefer>();
			IJobParallelForDeferExtensions.EarlyJobInit<WaterSystemJobs.AdjustByTopologyJob>();
			IJobParallelForDeferExtensions.EarlyJobInit<OceanSimulationJobs.SmallDisplacementPlaneTraceJob>();
			IJobParallelForDeferExtensions.EarlyJobInit<OceanSimulationJobs.OceanTraceJob>();
			IJobForExtensions.EarlyJobInit<GenerateErosionJobs.PaintSplatJob>();
			IJobParallelForExtensions.EarlyJobInit<GenerateErosionJobs.PopulateDeltaHeightJob>();
			IJobParallelForExtensions.EarlyJobInit<GenerateErosionJobs.CopyBackFloatHeightToShortHeightJob>();
			IJobParallelForExtensions.EarlyJobInit<GenerateErosionJobs.EvaporationJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<GenerateErosionJobs.PrepareMapJob>();
			IJobParallelForExtensions.EarlyJobInit<GenerateErosionJobs.TransportSedimentJob>();
			IJobParallelForExtensions.EarlyJobInit<GenerateErosionJobs.CalcMinHeightMapJob>();
			IJobParallelForExtensions.EarlyJobInit<GenerateErosionJobs.ErosionAndDepositionJob>();
			IJobParallelForExtensions.EarlyJobInit<GenerateErosionJobs.TileCalculateAngleMap>();
			IJobParallelForExtensions.EarlyJobInit<GenerateErosionJobs.AdjustWaterHeightByFluxJob>();
			IJobParallelForExtensions.EarlyJobInit<GenerateErosionJobs.CalculateOutputFluxJob>();
			IJobParallelForExtensions.EarlyJobInit<GenerateErosionJobs.RefillOceanJob>();
			IJobParallelForExtensions.EarlyJobInit<GenerateErosionJobs.WaterIncrementationJob>();
			IJobExtensions.EarlyJobInit<GetCoarseDistsToShoreJobIndirect>();
			IJobExtensions.EarlyJobInit<GetHeightByPosJob>();
			IJobExtensions.EarlyJobInit<GetHeightByUVJob>();
			IJobExtensions.EarlyJobInit<GetHeightByUVJobIndirect>();
			IJobExtensions.EarlyJobInit<GetHeightByIndexJob>();
			IJobExtensions.EarlyJobInit<GetTopologyByPosJob>();
			IJobExtensions.EarlyJobInit<GetTopologyByUVJob>();
			IJobExtensions.EarlyJobInit<TerrainTopologyMapJobs.GetTopologyRadiusJob>();
			IJobParallelForExtensions.EarlyJobInit<TerrainTopologyMapJobs.GetTopologyRadiusJobIndirect>();
			IJobParallelForExtensions.EarlyJobInit<TerrainTopologyMapJobs.GetTopologyRadiusNormalizedJobIndirect>();
			IJobExtensions.EarlyJobInit<GetTopologyByIndexJob>();
			IJobExtensions.EarlyJobInit<GetTopologyByUVJobIndirect>();
			IJobExtensions.EarlyJobInit<GetHeightsFastJobIndirect>();
			IJobExtensions.EarlyJobInit<GetHeightsJob>();
			IJobExtensions.EarlyJobInit<GetHeightsJobIndirect>();
			IJobExtensions.EarlyJobInit<CheckPosRadJob>();
			IJobExtensions.EarlyJobInit<CheckPosRadBatchJob>();
			IJobExtensions.EarlyJobInit<CheckPosRadBatchJobIndirect>();
			IJobExtensions.EarlyJobInit<CheckBoundsJob>();
			IJobExtensions.EarlyJobInit<CheckBoundsJobIndirect>();
			IJobExtensions.EarlyJobInit<CheckRayJob>();
			IJobExtensions.EarlyJobInit<CalculatePathBetweenGridsJob>();
			IJobParallelForBatchExtensions.EarlyJobInit<CalculatePathsBetweenGridsJob>();
			IJobForExtensions.EarlyJobInit<CalculateSubGridSamplePointsJob>();
			IJobExtensions.EarlyJobInit<ToUVJobIndirect>();
			IJobExtensions.EarlyJobInit<GatherWavesIndicesJobIndirect>();
			IJobExtensions.EarlyJobInit<ApplyMaxHeightsJobIndirect>();
			IJobExtensions.EarlyJobInit<SelectMaxWaterLevelJobIndirect>();
			IJobExtensions.EarlyJobInit<CalcCenterJobIndirect>();
			IJobExtensions.EarlyJobInit<InitialValidateInfoJobIndirect>();
			IJobExtensions.EarlyJobInit<GatherValidInfosJobIndirect>();
			IJobExtensions.EarlyJobInit<GatherInvalidInfosJobIndirect>();
			IJobExtensions.EarlyJobInit<UpdateWaterHeightsJobIndirect>();
			IJobExtensions.EarlyJobInit<SetupHeadQueryJobIndirect>();
			IJobExtensions.EarlyJobInit<ApplyHeadQueryResultsJobIndirect>();
			IJobExtensions.EarlyJobInit<ResolveWaterInfosJobIndirect>();
			IJobExtensions.EarlyJobInit<BasePlayerJobs.UpdateWaterCache>();
			IJobExtensions.EarlyJobInit<BasePlayerJobs.GatherPosToValidateJob>();
			IJobParallelForTransformExtensions.EarlyJobInit<RecacheTransforms>();
			IJobExtensions.EarlyJobInit<CalcWaterFactorsJobIndirect>();
			IJobExtensions.EarlyJobInit<GetWaterFactorsParamsJobIndirect>();
			IJobForExtensions.EarlyJobInit<HitBoxSystemJobs.TraceAllJob>();
			IJobExtensions.EarlyJobInit<CalcMidpoingJob>();
			IJobExtensions.EarlyJobInit<CheckHitsJob>();
			IJobExtensions.EarlyJobInit<GenerateOverlapBoxCommandsJob>();
			IJobExtensions.EarlyJobInit<ValidateOverlapBoxCommandsJob>();
			IJobExtensions.EarlyJobInit<GenerateOverlapCapsuleCommandsJob>();
			IJobExtensions.EarlyJobInit<ValidateOverlapCapsuleCommandsJob>();
			IJobExtensions.EarlyJobInit<FindSphereCmdsInCapsuleCmdsJob>();
			IJobExtensions.EarlyJobInit<GenerateSphereCmdsFromCapsuleCmdsJob>();
			IJobExtensions.EarlyJobInit<GenerateOverlapSphereCommandsJob>();
			IJobExtensions.EarlyJobInit<ValidateOverlapSphereCommandsJob>();
			IJobExtensions.EarlyJobInit<RemoveLayerMaskJob>();
			IJobExtensions.EarlyJobInit<CountRaycastHitsJobs>();
			IJobExtensions.EarlyJobInit<ScatterColliderHitsJob>();
			IJobExtensions.EarlyJobInit<GamePhysicsJobs.PreProcessWaterSpheresJob>();
			IJobExtensions.EarlyJobInit<GamePhysicsJobs.PreProcessWaterRaysJob>();
			IJobExtensions.EarlyJobInit<GamePhysicsJobs.PostProcessWaterRaysJob>();
			IJobParallelForTransformExtensions.EarlyJobInit<ProjectileJobs.ReadPositionDataTransformJob>();
			IJobParallelForTransformExtensions.EarlyJobInit<ProjectileJobs.BatchUpdateVelocityEndJob>();
			IJobParallelForTransformExtensions.EarlyJobInit<ProjectileJobs.GenerateRaysJob>();
			IJobParallelForTransformExtensions.EarlyJobInit<ProjectileJobs.GenerateRaysMidstepJob>();
			IJobParallelForTransformExtensions.EarlyJobInit<ProjectileJobs.PosRotReadJob>();
			IJobExtensions.EarlyJobInit<Facepunch.NativeMeshSimplification.CopyBackJob>();
			IJobExtensions.EarlyJobInit<Facepunch.NativeMeshSimplification.PopulateArraysJob>();
			IJobExtensions.EarlyJobInit<Facepunch.NativeMeshSimplification.SimplifyMeshJob>();
			IJobParallelForExtensions.EarlyJobInit<Facepunch.MarchingCubes.BakePhysicsMeshesJob>();
			IJobExtensions.EarlyJobInit<Facepunch.MarchingCubes.CleanupDuplicateVerticesJob>();
			IJobExtensions.EarlyJobInit<Facepunch.MarchingCubes.MarchJob>();
			IJobExtensions.EarlyJobInit<Rust.Water5.GetHeightBatchedJob>();
			IJobExtensions.EarlyJobInit<Rust.Water5.GetHeightsJobIndirect>();
			IJobExtensions.EarlyJobInit<GatherJob<OverlapCapsuleCommand>>();
			IJobExtensions.EarlyJobInit<FillJob<float>>();
			IJobExtensions.EarlyJobInit<FillJob<int>>();
			IJobExtensions.EarlyJobInit<FillJob<bool>>();
			IJobExtensions.EarlyJobInit<GatherJob<Vector3>>();
			IJobExtensions.EarlyJobInit<GatherJob<float>>();
			IJobExtensions.EarlyJobInit<GenerateErosionJobs.CopyArrayJob<float>>();
			IJobExtensions.EarlyJobInit<GatherJob<int>>();
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
