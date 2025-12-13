using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.TerrainUtils;

[assembly: InternalsVisibleTo("UnityEngine.UIModule")]
[assembly: InternalsVisibleTo("UnityEngine.TilemapModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainPhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubsystemsModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubstanceModule")]
[assembly: InternalsVisibleTo("UnityEngine.StreamingModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteShapeModule")]
[assembly: InternalsVisibleTo("UnityEngine.ScreenCaptureModule")]
[assembly: InternalsVisibleTo("UnityEngine.RuntimeInitializeOnLoadManagerInitializerModule")]
[assembly: InternalsVisibleTo("UnityEngine.PropertiesModule")]
[assembly: InternalsVisibleTo("UnityEngine.ProfilerModule")]
[assembly: InternalsVisibleTo("UnityEngine.Physics2DModule")]
[assembly: InternalsVisibleTo("UnityEngine.PerformanceReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.ParticleSystemModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteMaskModule")]
[assembly: InternalsVisibleTo("UnityEngine.NVIDIAModule")]
[assembly: InternalsVisibleTo("UnityEngine.UmbraModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityTestProtocolModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS5Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS4VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS4Module")]
[assembly: InternalsVisibleTo("UnityEngine.XboxOneModule")]
[assembly: InternalsVisibleTo("UnityEngine.SwitchModule")]
[assembly: InternalsVisibleTo("UnityEngine.WindModule")]
[assembly: InternalsVisibleTo("UnityEngine.VirtualTexturingModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityCurlModule")]
[assembly: InternalsVisibleTo("UnityEngine.VideoModule")]
[assembly: InternalsVisibleTo("UnityEngine.VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.XRModule")]
[assembly: InternalsVisibleTo("UnityEngine.VFXModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestWWWModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestTextureModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.VehiclesModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS5VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.LocalizationModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreTextEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.HotReloadModule")]
[assembly: InternalsVisibleTo("UnityEngine.AnimationModule")]
[assembly: InternalsVisibleTo("UnityEngine.AndroidJNIModule")]
[assembly: InternalsVisibleTo("UnityEngine.AccessibilityModule")]
[assembly: InternalsVisibleTo("UnityEngine.ARModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputModule")]
[assembly: InternalsVisibleTo("UnityEngine.AudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.JSONSerializeModule")]
[assembly: InternalsVisibleTo("UnityEngine.AIModule")]
[assembly: InternalsVisibleTo("UnityEngine.CoreModule")]
[assembly: InternalsVisibleTo("UnityEngine.SharedInternalsModule")]
[assembly: InternalsVisibleTo("UnityEngine")]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: CompilationRelaxations(8)]
[assembly: InternalsVisibleTo("UnityEngine.PhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.IMGUIModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClothModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterRendererModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreFontEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputLegacyModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextRenderingModule")]
[assembly: InternalsVisibleTo("UnityEngine.GridModule")]
[assembly: InternalsVisibleTo("UnityEngine.GameCenterModule")]
[assembly: InternalsVisibleTo("UnityEngine.ImageConversionModule")]
[assembly: InternalsVisibleTo("UnityEngine.GIModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
[assembly: InternalsVisibleTo("UnityEngine.DirectorModule")]
[assembly: InternalsVisibleTo("UnityEngine.CrashReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestModule")]
[assembly: InternalsVisibleTo("UnityEngine.TLSModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommonModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityConnectModule")]
[assembly: InternalsVisibleTo("UnityEngine.ContentLoadModule")]
[assembly: InternalsVisibleTo("UnityEngine.DSPGraphModule")]
[assembly: InternalsVisibleTo("UnityEngine.Networking")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud.Service")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.010")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.009")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.008")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.007")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.006")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.011")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.001")]
[assembly: InternalsVisibleTo("UnityEngine.Core.Runtime.Tests")]
[assembly: InternalsVisibleTo("Unity.Core")]
[assembly: InternalsVisibleTo("Unity.Runtime")]
[assembly: InternalsVisibleTo("Unity.Collections")]
[assembly: InternalsVisibleTo("Unity.Entities.Tests")]
[assembly: InternalsVisibleTo("Unity.Entities")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.002")]
[assembly: InternalsVisibleTo("Unity.Logging")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.012")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.014")]
[assembly: InternalsVisibleTo("Unity.Subsystem.Registration")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.001")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.024")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.013")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.023")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.021")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.020")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.019")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.018")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.017")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.016")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.015")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.022")]
[assembly: InternalsVisibleTo("Unity.Services.QoS")]
[assembly: InternalsVisibleTo("Unity.ucg.QoS")]
[assembly: InternalsVisibleTo("Unity.Networking.Transport")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Framework")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Timeline")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.UnityAnalytics")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests")]
[assembly: InternalsVisibleTo("Unity.DeploymentTests.Services")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework.Tests")]
[assembly: InternalsVisibleTo("Unity.Burst.Editor")]
[assembly: InternalsVisibleTo("Unity.Automation")]
[assembly: InternalsVisibleTo("UnityEngine.TestRunner")]
[assembly: InternalsVisibleTo("UnityEngine.Purchasing")]
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommon")]
[assembly: InternalsVisibleTo("UnityEngine.Analytics")]
[assembly: InternalsVisibleTo("Unity.Analytics")]
[assembly: InternalsVisibleTo("Unity.Burst")]
[assembly: InternalsVisibleTo("Unity.PerformanceTests.RuntimeTestRunner.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.AllIn1Runner")]
[assembly: InternalsVisibleTo("Unity.Timeline")]
[assembly: InternalsVisibleTo("UnityEngine.UI")]
[assembly: InternalsVisibleTo("Unity.UIElements.EditorTests")]
[assembly: InternalsVisibleTo("UnityEngine.UIElements.Tests")]
[assembly: InternalsVisibleTo("Unity.UIElements.PlayModeTests")]
[assembly: InternalsVisibleTo("Unity.UIElements.Editor")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsGameObjectsModule")]
[assembly: InternalsVisibleTo("Unity.UIElements")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.EditorTests")]
[assembly: InternalsVisibleTo("UnityEditor.UIBuilderModule")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.Editor")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.EditorTests")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.Editor")]
[assembly: InternalsVisibleTo("Unity.WindowsMRAutomation")]
[assembly: InternalsVisibleTo("GoogleAR.UnityNative")]
[assembly: InternalsVisibleTo("UnityEngine.SpatialTracking")]
[assembly: InternalsVisibleTo("Assembly-CSharp-firstpass-testable")]
[assembly: InternalsVisibleTo("Assembly-CSharp-testable")]
[assembly: UnityEngineModuleAssembly]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace UnityEngine
{
	[Flags]
	public enum TerrainChangedFlags
	{
		Heightmap = 1,
		TreeInstances = 2,
		DelayedHeightmapUpdate = 4,
		FlushEverythingImmediately = 8,
		RemoveDirtyDetailsImmediately = 0x10,
		HeightmapResolution = 0x20,
		Holes = 0x40,
		DelayedHolesUpdate = 0x80,
		WillBeDestroyed = 0x100
	}
	[Flags]
	public enum TerrainRenderFlags
	{
		[Obsolete("TerrainRenderFlags.heightmap is obsolete, use TerrainRenderFlags.Heightmap instead. (UnityUpgradable) -> Heightmap")]
		heightmap = 1,
		[Obsolete("TerrainRenderFlags.trees is obsolete, use TerrainRenderFlags.Trees instead. (UnityUpgradable) -> Trees")]
		trees = 2,
		[Obsolete("TerrainRenderFlags.details is obsolete, use TerrainRenderFlags.Details instead. (UnityUpgradable) -> Details")]
		details = 4,
		[Obsolete("TerrainRenderFlags.all is obsolete, use TerrainRenderFlags.All instead. (UnityUpgradable) -> All")]
		all = 7,
		Heightmap = 1,
		Trees = 2,
		Details = 4,
		All = 7
	}
	[UsedByNativeCode]
	[StaticAccessor("GetITerrainManager()", StaticAccessorType.Arrow)]
	[NativeHeader("TerrainScriptingClasses.h")]
	[NativeHeader("Runtime/Interfaces/ITerrainManager.h")]
	[NativeHeader("Modules/Terrain/Public/Terrain.h")]
	public sealed class Terrain : Behaviour
	{
		[Obsolete("Enum type MaterialType is not used any more.", false)]
		public enum MaterialType
		{
			BuiltInStandard,
			BuiltInLegacyDiffuse,
			BuiltInLegacySpecular,
			Custom
		}

		public extern TerrainData terrainData
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float treeDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float treeBillboardDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float treeCrossFadeLength
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int treeMaximumFullLODCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float detailObjectDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float detailObjectDensity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float heightmapPixelError
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int heightmapMaximumLOD
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int heightmapMinimumLODSimplification
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float basemapDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("StaticLightmapIndexInt")]
		public extern int lightmapIndex
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("DynamicLightmapIndexInt")]
		public extern int realtimeLightmapIndex
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("StaticLightmapST")]
		public Vector4 lightmapScaleOffset
		{
			get
			{
				get_lightmapScaleOffset_Injected(out var ret);
				return ret;
			}
			set
			{
				set_lightmapScaleOffset_Injected(ref value);
			}
		}

		[NativeProperty("DynamicLightmapST")]
		public Vector4 realtimeLightmapScaleOffset
		{
			get
			{
				get_realtimeLightmapScaleOffset_Injected(out var ret);
				return ret;
			}
			set
			{
				set_realtimeLightmapScaleOffset_Injected(ref value);
			}
		}

		[NativeProperty("FreeUnusedRenderingResourcesObsolete")]
		[Obsolete("Terrain.freeUnusedRenderingResources is obsolete; use keepUnusedRenderingResources instead.")]
		public extern bool freeUnusedRenderingResources
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("KeepUnusedRenderingResources")]
		public extern bool keepUnusedRenderingResources
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern ShadowCastingMode shadowCastingMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern ReflectionProbeUsage reflectionProbeUsage
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Material materialTemplate
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool drawHeightmap
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool allowAutoConnect
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int groupingID
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool drawInstanced
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool enableHeightmapRayTracing
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern RenderTexture normalmapTexture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("TryGetNormalMapTexture")]
			get;
		}

		public extern bool drawTreesAndFoliage
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 patchBoundsMultiplier
		{
			get
			{
				get_patchBoundsMultiplier_Injected(out var ret);
				return ret;
			}
			set
			{
				set_patchBoundsMultiplier_Injected(ref value);
			}
		}

		public extern float treeLODBiasMultiplier
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool collectDetailPatches
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool ignoreQualitySettings
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern TerrainRenderFlags editorRenderFlags
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern TreeMotionVectorModeOverride treeMotionVectorModeOverride
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool preserveTreePrototypeLayers
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[StaticAccessor("Terrain", StaticAccessorType.DoubleColon)]
		public static extern GraphicsFormat heightmapFormat
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static TextureFormat heightmapTextureFormat => GraphicsFormatUtility.GetTextureFormat(heightmapFormat);

		public static RenderTextureFormat heightmapRenderTextureFormat => GraphicsFormatUtility.GetRenderTextureFormat(heightmapFormat);

		[StaticAccessor("Terrain", StaticAccessorType.DoubleColon)]
		public static extern GraphicsFormat normalmapFormat
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static TextureFormat normalmapTextureFormat => GraphicsFormatUtility.GetTextureFormat(normalmapFormat);

		public static RenderTextureFormat normalmapRenderTextureFormat => GraphicsFormatUtility.GetRenderTextureFormat(normalmapFormat);

		[StaticAccessor("Terrain", StaticAccessorType.DoubleColon)]
		public static extern GraphicsFormat holesFormat
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static RenderTextureFormat holesRenderTextureFormat => GraphicsFormatUtility.GetRenderTextureFormat(holesFormat);

		[StaticAccessor("Terrain", StaticAccessorType.DoubleColon)]
		public static extern GraphicsFormat compressedHolesFormat
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static TextureFormat compressedHolesTextureFormat => GraphicsFormatUtility.GetTextureFormat(compressedHolesFormat);

		public static extern Terrain activeTerrain
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("ActiveTerrainsScriptingArray")]
		public static extern Terrain[] activeTerrains
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Terrain leftNeighbor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Terrain rightNeighbor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Terrain topNeighbor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Terrain bottomNeighbor
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern uint renderingLayerMask
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("splatmapDistance is deprecated, please use basemapDistance instead. (UnityUpgradable) -> basemapDistance", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float splatmapDistance
		{
			get
			{
				return basemapDistance;
			}
			set
			{
				basemapDistance = value;
			}
		}

		[Obsolete("castShadows is deprecated, please use shadowCastingMode instead.")]
		public bool castShadows
		{
			get
			{
				return shadowCastingMode != ShadowCastingMode.Off;
			}
			set
			{
				shadowCastingMode = (value ? ShadowCastingMode.TwoSided : ShadowCastingMode.Off);
			}
		}

		[Obsolete("Property materialType is not used any more. Set materialTemplate directly.", false)]
		public MaterialType materialType
		{
			get
			{
				return MaterialType.Custom;
			}
			set
			{
			}
		}

		[Obsolete("Property legacySpecular is not used any more. Set materialTemplate directly.", false)]
		public Color legacySpecular
		{
			get
			{
				return Color.gray;
			}
			set
			{
			}
		}

		[Obsolete("Property legacyShininess is not used any more. Set materialTemplate directly.", false)]
		public float legacyShininess
		{
			get
			{
				return 5f / 64f;
			}
			set
			{
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetKeepUnusedCameraRenderingResources(int cameraInstanceID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetKeepUnusedCameraRenderingResources(int cameraInstanceID, bool keepUnused);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetClosestReflectionProbes(List<ReflectionProbeBlendInfo> result);

		public float SampleHeight(Vector3 worldPosition)
		{
			return SampleHeight_Injected(ref worldPosition);
		}

		public void AddTreeInstance(TreeInstance instance)
		{
			AddTreeInstance_Injected(ref instance);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetNeighbors(Terrain left, Terrain top, Terrain right, Terrain bottom);

		public Vector3 GetPosition()
		{
			GetPosition_Injected(out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Flush();

		internal void RemoveTrees(Vector2 position, float radius, int prototypeIndex)
		{
			RemoveTrees_Injected(ref position, radius, prototypeIndex);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("CopySplatMaterialCustomProps")]
		public extern void SetSplatMaterialPropertyBlock(MaterialPropertyBlock properties);

		public void GetSplatMaterialPropertyBlock(MaterialPropertyBlock dest)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			Internal_GetSplatMaterialPropertyBlock(dest);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetSplatMaterialCustomProps")]
		private extern void Internal_GetSplatMaterialPropertyBlock(MaterialPropertyBlock dest);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetConnectivityDirty();

		public static void GetActiveTerrains(List<Terrain> terrainList)
		{
			Internal_FillActiveTerrainList(terrainList);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_FillActiveTerrainList([NotNull("ArgumentNullException")] object terrainList);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[UsedByNativeCode]
		public static extern GameObject CreateTerrainGameObject(TerrainData assignTerrain);

		[Obsolete("Use TerrainData.SyncHeightmap to notify all Terrain instances using the TerrainData.", false)]
		public void ApplyDelayedHeightmapModification()
		{
			terrainData?.SyncHeightmap();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_lightmapScaleOffset_Injected(out Vector4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_lightmapScaleOffset_Injected(ref Vector4 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_realtimeLightmapScaleOffset_Injected(out Vector4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_realtimeLightmapScaleOffset_Injected(ref Vector4 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_patchBoundsMultiplier_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_patchBoundsMultiplier_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float SampleHeight_Injected(ref Vector3 worldPosition);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AddTreeInstance_Injected(ref TreeInstance instance);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPosition_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RemoveTrees_Injected(ref Vector2 position, float radius, int prototypeIndex);
	}
	public static class TerrainExtensions
	{
		public static void UpdateGIMaterials(this Terrain terrain)
		{
			if (terrain.terrainData == null)
			{
				throw new ArgumentException("Invalid terrainData.");
			}
			UpdateGIMaterialsForTerrain(terrain.GetInstanceID(), new Rect(0f, 0f, 1f, 1f));
		}

		public static void UpdateGIMaterials(this Terrain terrain, int x, int y, int width, int height)
		{
			if (terrain.terrainData == null)
			{
				throw new ArgumentException("Invalid terrainData.");
			}
			float num = terrain.terrainData.alphamapWidth;
			float num2 = terrain.terrainData.alphamapHeight;
			UpdateGIMaterialsForTerrain(terrain.GetInstanceID(), new Rect((float)x / num, (float)y / num2, (float)width / num, (float)height / num2));
		}

		[FreeFunction]
		[NativeConditional("INCLUDE_DYNAMIC_GI && ENABLE_RUNTIME_GI")]
		internal static void UpdateGIMaterialsForTerrain(int terrainInstanceID, Rect uvBounds)
		{
			UpdateGIMaterialsForTerrain_Injected(terrainInstanceID, ref uvBounds);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateGIMaterialsForTerrain_Injected(int terrainInstanceID, ref Rect uvBounds);
	}
	[NativeHeader("Modules/Terrain/Public/Tree.h")]
	[ExcludeFromPreset]
	public sealed class Tree : Component
	{
		[NativeProperty("TreeData")]
		public extern ScriptableObject data
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool hasSpeedTreeWind
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("HasSpeedTreeWind")]
			get;
		}
	}
	internal sealed class SpeedTreeWindAsset : Object
	{
		private SpeedTreeWindAsset()
		{
		}
	}
	[MovedFrom("UnityEngine.Experimental.TerrainAPI")]
	public static class TerrainCallbacks
	{
		public delegate void HeightmapChangedCallback(Terrain terrain, RectInt heightRegion, bool synched);

		public delegate void TextureChangedCallback(Terrain terrain, string textureName, RectInt texelRegion, bool synched);

		public static event HeightmapChangedCallback heightmapChanged;

		public static event TextureChangedCallback textureChanged;

		[RequiredByNativeCode]
		internal static void InvokeHeightmapChangedCallback(TerrainData terrainData, RectInt heightRegion, bool synched)
		{
			if (TerrainCallbacks.heightmapChanged != null)
			{
				Terrain[] users = terrainData.users;
				foreach (Terrain terrain in users)
				{
					TerrainCallbacks.heightmapChanged(terrain, heightRegion, synched);
				}
			}
		}

		[RequiredByNativeCode]
		internal static void InvokeTextureChangedCallback(TerrainData terrainData, string textureName, RectInt texelRegion, bool synched)
		{
			if (TerrainCallbacks.textureChanged != null)
			{
				Terrain[] users = terrainData.users;
				foreach (Terrain terrain in users)
				{
					TerrainCallbacks.textureChanged(terrain, textureName, texelRegion, synched);
				}
			}
		}
	}
	[StructLayout(LayoutKind.Sequential)]
	[UsedByNativeCode]
	public sealed class TreePrototype
	{
		internal GameObject m_Prefab;

		internal float m_BendFactor;

		internal int m_NavMeshLod;

		public GameObject prefab
		{
			get
			{
				return m_Prefab;
			}
			set
			{
				m_Prefab = value;
			}
		}

		public float bendFactor
		{
			get
			{
				return m_BendFactor;
			}
			set
			{
				m_BendFactor = value;
			}
		}

		public int navMeshLod
		{
			get
			{
				return m_NavMeshLod;
			}
			set
			{
				m_NavMeshLod = value;
			}
		}

		public TreePrototype()
		{
		}

		public TreePrototype(TreePrototype other)
		{
			prefab = other.prefab;
			bendFactor = other.bendFactor;
			navMeshLod = other.navMeshLod;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as TreePrototype);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		private bool Equals(TreePrototype other)
		{
			if (other == null)
			{
				return false;
			}
			if (other == this)
			{
				return true;
			}
			if (GetType() != other.GetType())
			{
				return false;
			}
			return prefab == other.prefab && bendFactor == other.bendFactor && navMeshLod == other.navMeshLod;
		}

		internal bool Validate(out string errorMessage)
		{
			return ValidateTreePrototype(this, out errorMessage);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::ValidateTreePrototype")]
		internal static extern bool ValidateTreePrototype([NotNull("ArgumentNullException")] TreePrototype prototype, out string errorMessage);
	}
	public enum DetailRenderMode
	{
		GrassBillboard,
		VertexLit,
		Grass
	}
	public enum DetailScatterMode
	{
		CoverageMode,
		InstanceCountMode
	}
	public enum TreeMotionVectorModeOverride
	{
		CameraMotionOnly,
		PerObjectMotion,
		ForceNoMotion,
		InheritFromPrototype
	}
	[StructLayout(LayoutKind.Sequential)]
	[NativeHeader("TerrainScriptingClasses.h")]
	[NativeHeader("Modules/Terrain/Public/TerrainDataScriptingInterface.h")]
	[UsedByNativeCode]
	public sealed class DetailPrototype
	{
		internal static readonly Color DefaultHealthColor = new Color(0.2627451f, 83f / 85f, 14f / 85f, 1f);

		internal static readonly Color DefaultDryColor = new Color(41f / 51f, 0.7372549f, 0.101960786f, 1f);

		internal GameObject m_Prototype = null;

		internal Texture2D m_PrototypeTexture = null;

		internal Color m_HealthyColor = DefaultHealthColor;

		internal Color m_DryColor = DefaultDryColor;

		internal float m_MinWidth = 1f;

		internal float m_MaxWidth = 2f;

		internal float m_MinHeight = 1f;

		internal float m_MaxHeight = 2f;

		internal int m_NoiseSeed = 0;

		internal float m_NoiseSpread = 0.1f;

		internal float m_Density = 1f;

		internal float m_HoleEdgePadding = 0f;

		internal int m_RenderMode = 2;

		internal int m_UsePrototypeMesh = 0;

		internal int m_UseInstancing = 0;

		internal int m_UseDensityScaling = 0;

		internal float m_AlignToGround = 0f;

		internal float m_PositionJitter = 0f;

		internal float m_TargetCoverage = 1f;

		public GameObject prototype
		{
			get
			{
				return m_Prototype;
			}
			set
			{
				m_Prototype = value;
			}
		}

		public Texture2D prototypeTexture
		{
			get
			{
				return m_PrototypeTexture;
			}
			set
			{
				m_PrototypeTexture = value;
			}
		}

		public float minWidth
		{
			get
			{
				return m_MinWidth;
			}
			set
			{
				m_MinWidth = value;
			}
		}

		public float maxWidth
		{
			get
			{
				return m_MaxWidth;
			}
			set
			{
				m_MaxWidth = value;
			}
		}

		public float minHeight
		{
			get
			{
				return m_MinHeight;
			}
			set
			{
				m_MinHeight = value;
			}
		}

		public float maxHeight
		{
			get
			{
				return m_MaxHeight;
			}
			set
			{
				m_MaxHeight = value;
			}
		}

		public int noiseSeed
		{
			get
			{
				return m_NoiseSeed;
			}
			set
			{
				m_NoiseSeed = value;
			}
		}

		public float noiseSpread
		{
			get
			{
				return m_NoiseSpread;
			}
			set
			{
				m_NoiseSpread = value;
			}
		}

		public float density
		{
			get
			{
				return m_Density;
			}
			set
			{
				m_Density = value;
			}
		}

		[Obsolete("bendFactor has no effect and is deprecated.", false)]
		public float bendFactor
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float holeEdgePadding
		{
			get
			{
				return m_HoleEdgePadding;
			}
			set
			{
				m_HoleEdgePadding = value;
			}
		}

		public Color healthyColor
		{
			get
			{
				return m_HealthyColor;
			}
			set
			{
				m_HealthyColor = value;
			}
		}

		public Color dryColor
		{
			get
			{
				return m_DryColor;
			}
			set
			{
				m_DryColor = value;
			}
		}

		public DetailRenderMode renderMode
		{
			get
			{
				return (DetailRenderMode)m_RenderMode;
			}
			set
			{
				m_RenderMode = (int)value;
			}
		}

		public bool usePrototypeMesh
		{
			get
			{
				return m_UsePrototypeMesh != 0;
			}
			set
			{
				m_UsePrototypeMesh = (value ? 1 : 0);
			}
		}

		public bool useInstancing
		{
			get
			{
				return m_UseInstancing != 0;
			}
			set
			{
				m_UseInstancing = (value ? 1 : 0);
			}
		}

		public float targetCoverage
		{
			get
			{
				return m_TargetCoverage;
			}
			set
			{
				m_TargetCoverage = value;
			}
		}

		public bool useDensityScaling
		{
			get
			{
				return m_UseDensityScaling != 0;
			}
			set
			{
				m_UseDensityScaling = (value ? 1 : 0);
			}
		}

		public float alignToGround
		{
			get
			{
				return m_AlignToGround;
			}
			set
			{
				m_AlignToGround = value;
			}
		}

		public float positionJitter
		{
			get
			{
				return m_PositionJitter;
			}
			set
			{
				m_PositionJitter = value;
			}
		}

		public DetailPrototype()
		{
		}

		public DetailPrototype(DetailPrototype other)
		{
			m_Prototype = other.m_Prototype;
			m_PrototypeTexture = other.m_PrototypeTexture;
			m_HealthyColor = other.m_HealthyColor;
			m_DryColor = other.m_DryColor;
			m_MinWidth = other.m_MinWidth;
			m_MaxWidth = other.m_MaxWidth;
			m_MinHeight = other.m_MinHeight;
			m_MaxHeight = other.m_MaxHeight;
			m_NoiseSeed = other.m_NoiseSeed;
			m_NoiseSpread = other.m_NoiseSpread;
			m_Density = other.m_Density;
			m_HoleEdgePadding = other.m_HoleEdgePadding;
			m_RenderMode = other.m_RenderMode;
			m_UsePrototypeMesh = other.m_UsePrototypeMesh;
			m_UseInstancing = other.m_UseInstancing;
			m_UseDensityScaling = other.m_UseDensityScaling;
			m_AlignToGround = other.m_AlignToGround;
			m_PositionJitter = other.m_PositionJitter;
			m_TargetCoverage = other.m_TargetCoverage;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as DetailPrototype);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		private bool Equals(DetailPrototype other)
		{
			if (other == null)
			{
				return false;
			}
			if (other == this)
			{
				return true;
			}
			if (GetType() != other.GetType())
			{
				return false;
			}
			return m_Prototype == other.m_Prototype && m_PrototypeTexture == other.m_PrototypeTexture && m_HealthyColor == other.m_HealthyColor && m_DryColor == other.m_DryColor && m_MinWidth == other.m_MinWidth && m_MaxWidth == other.m_MaxWidth && m_MinHeight == other.m_MinHeight && m_MaxHeight == other.m_MaxHeight && m_NoiseSeed == other.m_NoiseSeed && m_NoiseSpread == other.m_NoiseSpread && m_Density == other.m_Density && m_HoleEdgePadding == other.m_HoleEdgePadding && m_RenderMode == other.m_RenderMode && m_UsePrototypeMesh == other.m_UsePrototypeMesh && m_UseInstancing == other.m_UseInstancing && m_TargetCoverage == other.m_TargetCoverage && m_UseDensityScaling == other.m_UseDensityScaling;
		}

		public bool Validate()
		{
			string errorMessage;
			return ValidateDetailPrototype(this, out errorMessage);
		}

		public bool Validate(out string errorMessage)
		{
			return ValidateDetailPrototype(this, out errorMessage);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::ValidateDetailPrototype")]
		internal static extern bool ValidateDetailPrototype([NotNull("ArgumentNullException")] DetailPrototype prototype, out string errorMessage);

		internal static bool IsModeSupportedByRenderPipeline(DetailRenderMode renderMode, bool useInstancing, out string errorMessage)
		{
			if (GraphicsSettings.currentRenderPipeline != null)
			{
				if (renderMode == DetailRenderMode.GrassBillboard && GraphicsSettings.currentRenderPipeline.terrainDetailGrassBillboardShader == null)
				{
					errorMessage = "The current render pipeline does not support Billboard details. Details will not be rendered.";
					return false;
				}
				if (renderMode == DetailRenderMode.VertexLit && !useInstancing && GraphicsSettings.currentRenderPipeline.terrainDetailLitShader == null)
				{
					errorMessage = "The current render pipeline does not support VertexLit details. Details will be rendered using the default shader.";
					return false;
				}
				if (renderMode == DetailRenderMode.Grass && GraphicsSettings.currentRenderPipeline.terrainDetailGrassShader == null)
				{
					errorMessage = "The current render pipeline does not support Grass details. Details will be rendered using the default shader without alpha test and animation.";
					return false;
				}
			}
			errorMessage = string.Empty;
			return true;
		}
	}
	[StructLayout(LayoutKind.Sequential)]
	[UsedByNativeCode]
	public sealed class SplatPrototype
	{
		internal Texture2D m_Texture;

		internal Texture2D m_NormalMap;

		internal Vector2 m_TileSize = new Vector2(15f, 15f);

		internal Vector2 m_TileOffset = new Vector2(0f, 0f);

		internal Vector4 m_SpecularMetallic = new Vector4(0f, 0f, 0f, 0f);

		internal float m_Smoothness = 0f;

		public Texture2D texture
		{
			get
			{
				return m_Texture;
			}
			set
			{
				m_Texture = value;
			}
		}

		public Texture2D normalMap
		{
			get
			{
				return m_NormalMap;
			}
			set
			{
				m_NormalMap = value;
			}
		}

		public Vector2 tileSize
		{
			get
			{
				return m_TileSize;
			}
			set
			{
				m_TileSize = value;
			}
		}

		public Vector2 tileOffset
		{
			get
			{
				return m_TileOffset;
			}
			set
			{
				m_TileOffset = value;
			}
		}

		public Color specular
		{
			get
			{
				return new Color(m_SpecularMetallic.x, m_SpecularMetallic.y, m_SpecularMetallic.z);
			}
			set
			{
				m_SpecularMetallic.x = value.r;
				m_SpecularMetallic.y = value.g;
				m_SpecularMetallic.z = value.b;
			}
		}

		public float metallic
		{
			get
			{
				return m_SpecularMetallic.w;
			}
			set
			{
				m_SpecularMetallic.w = value;
			}
		}

		public float smoothness
		{
			get
			{
				return m_Smoothness;
			}
			set
			{
				m_Smoothness = value;
			}
		}
	}
	[UsedByNativeCode]
	public struct TreeInstance
	{
		public Vector3 position;

		public float widthScale;

		public float heightScale;

		public float rotation;

		public Color32 color;

		public Color32 lightmapColor;

		public int prototypeIndex;

		internal float temporaryDistance;
	}
	[UsedByNativeCode]
	public struct PatchExtents
	{
		internal float m_min;

		internal float m_max;

		public float min
		{
			get
			{
				return m_min;
			}
			set
			{
				m_min = value;
			}
		}

		public float max
		{
			get
			{
				return m_max;
			}
			set
			{
				m_max = value;
			}
		}
	}
	public enum TerrainHeightmapSyncControl
	{
		None,
		HeightOnly,
		HeightAndLod
	}
	[UsedByNativeCode]
	public struct DetailInstanceTransform
	{
		public float posX;

		public float posY;

		public float posZ;

		public float scaleXZ;

		public float scaleY;

		public float rotationY;
	}
	[NativeHeader("TerrainScriptingClasses.h")]
	[NativeHeader("Modules/Terrain/Public/TerrainDataScriptingInterface.h")]
	[UsedByNativeCode]
	public sealed class TerrainData : Object
	{
		private enum BoundaryValueType
		{
			MaxHeightmapRes,
			MinDetailResPerPatch,
			MaxDetailResPerPatch,
			MaxDetailPatchCount,
			MaxCoveragePerRes,
			MinAlphamapRes,
			MaxAlphamapRes,
			MinBaseMapRes,
			MaxBaseMapRes
		}

		private const string k_ScriptingInterfaceName = "TerrainDataScriptingInterface";

		private const string k_ScriptingInterfacePrefix = "TerrainDataScriptingInterface::";

		private const string k_HeightmapPrefix = "GetHeightmap().";

		private const string k_DetailDatabasePrefix = "GetDetailDatabase().";

		private const string k_TreeDatabasePrefix = "GetTreeDatabase().";

		private const string k_SplatDatabasePrefix = "GetSplatDatabase().";

		internal static readonly int k_MaximumResolution = GetBoundaryValue(BoundaryValueType.MaxHeightmapRes);

		internal static readonly int k_MinimumDetailResolutionPerPatch = GetBoundaryValue(BoundaryValueType.MinDetailResPerPatch);

		internal static readonly int k_MaximumDetailResolutionPerPatch = GetBoundaryValue(BoundaryValueType.MaxDetailResPerPatch);

		internal static readonly int k_MaximumDetailPatchCount = GetBoundaryValue(BoundaryValueType.MaxDetailPatchCount);

		internal static readonly int k_MinimumAlphamapResolution = GetBoundaryValue(BoundaryValueType.MinAlphamapRes);

		internal static readonly int k_MaximumAlphamapResolution = GetBoundaryValue(BoundaryValueType.MaxAlphamapRes);

		internal static readonly int k_MinimumBaseMapResolution = GetBoundaryValue(BoundaryValueType.MinBaseMapRes);

		internal static readonly int k_MaximumBaseMapResolution = GetBoundaryValue(BoundaryValueType.MaxBaseMapRes);

		[Obsolete("Please use heightmapResolution instead. (UnityUpgradable) -> heightmapResolution", false)]
		public int heightmapWidth => heightmapResolution;

		[Obsolete("Please use heightmapResolution instead. (UnityUpgradable) -> heightmapResolution", false)]
		public int heightmapHeight => heightmapResolution;

		public extern RenderTexture heightmapTexture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetHeightmap().GetHeightmapTexture")]
			get;
		}

		public int heightmapResolution
		{
			get
			{
				return internalHeightmapResolution;
			}
			set
			{
				int num = value;
				if (value < 0 || value > k_MaximumResolution)
				{
					Debug.LogWarning("heightmapResolution is clamped to the range of [0, " + k_MaximumResolution + "].");
					num = Math.Min(k_MaximumResolution, Math.Max(value, 0));
				}
				internalHeightmapResolution = num;
			}
		}

		private extern int internalHeightmapResolution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetHeightmap().GetResolution")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetHeightmap().SetResolution")]
			set;
		}

		public Vector3 heightmapScale
		{
			[NativeName("GetHeightmap().GetScale")]
			get
			{
				get_heightmapScale_Injected(out var ret);
				return ret;
			}
		}

		public Texture holesTexture
		{
			get
			{
				if (IsHolesTextureCompressed())
				{
					return GetCompressedHolesTexture();
				}
				return GetHolesTexture();
			}
		}

		public extern bool enableHolesTextureCompression
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetHeightmap().GetEnableHolesTextureCompression")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetHeightmap().SetEnableHolesTextureCompression")]
			set;
		}

		internal RenderTexture holesRenderTexture => GetHolesTexture();

		public int holesResolution => heightmapResolution - 1;

		public Vector3 size
		{
			[NativeName("GetHeightmap().GetSize")]
			get
			{
				get_size_Injected(out var ret);
				return ret;
			}
			[NativeName("GetHeightmap().SetSize")]
			set
			{
				set_size_Injected(ref value);
			}
		}

		public Bounds bounds
		{
			[NativeName("GetHeightmap().CalculateBounds")]
			get
			{
				get_bounds_Injected(out var ret);
				return ret;
			}
		}

		[Obsolete("Terrain thickness is no longer required by the physics engine. Set appropriate continuous collision detection modes to fast moving bodies.")]
		public float thickness
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public extern float wavingGrassStrength
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetDetailDatabase().GetWavingGrassStrength")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("TerrainDataScriptingInterface::SetWavingGrassStrength", HasExplicitThis = true)]
			set;
		}

		public extern float wavingGrassAmount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetDetailDatabase().GetWavingGrassAmount")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("TerrainDataScriptingInterface::SetWavingGrassAmount", HasExplicitThis = true)]
			set;
		}

		public extern float wavingGrassSpeed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetDetailDatabase().GetWavingGrassSpeed")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("TerrainDataScriptingInterface::SetWavingGrassSpeed", HasExplicitThis = true)]
			set;
		}

		public Color wavingGrassTint
		{
			[NativeName("GetDetailDatabase().GetWavingGrassTint")]
			get
			{
				get_wavingGrassTint_Injected(out var ret);
				return ret;
			}
			[FreeFunction("TerrainDataScriptingInterface::SetWavingGrassTint", HasExplicitThis = true)]
			set
			{
				set_wavingGrassTint_Injected(ref value);
			}
		}

		public extern int detailWidth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetDetailDatabase().GetWidth")]
			get;
		}

		public extern int detailHeight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetDetailDatabase().GetHeight")]
			get;
		}

		public extern int maxDetailScatterPerRes
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetDetailDatabase().GetMaximumScatterPerRes")]
			get;
		}

		public extern int detailPatchCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetDetailDatabase().GetPatchCount")]
			get;
		}

		public extern int detailResolution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetDetailDatabase().GetResolution")]
			get;
		}

		public extern int detailResolutionPerPatch
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetDetailDatabase().GetResolutionPerPatch")]
			get;
		}

		public extern DetailScatterMode detailScatterMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetDetailDatabase().GetDetailScatterMode")]
			get;
		}

		public extern DetailPrototype[] detailPrototypes
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("TerrainDataScriptingInterface::GetDetailPrototypes", HasExplicitThis = true)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("TerrainDataScriptingInterface::SetDetailPrototypes", HasExplicitThis = true, ThrowsException = true)]
			[param: Unmarshalled]
			set;
		}

		public TreeInstance[] treeInstances
		{
			get
			{
				return Internal_GetTreeInstances();
			}
			set
			{
				SetTreeInstances(value, snapToHeightmap: false);
			}
		}

		public extern int treeInstanceCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetTreeDatabase().GetInstances().size")]
			get;
		}

		public extern TreePrototype[] treePrototypes
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("TerrainDataScriptingInterface::GetTreePrototypes", HasExplicitThis = true)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("TerrainDataScriptingInterface::SetTreePrototypes", HasExplicitThis = true, ThrowsException = true)]
			[param: Unmarshalled]
			set;
		}

		public extern int alphamapLayers
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetSplatDatabase().GetSplatCount")]
			get;
		}

		public int alphamapResolution
		{
			get
			{
				return Internal_alphamapResolution;
			}
			set
			{
				int internal_alphamapResolution = value;
				if (value < k_MinimumAlphamapResolution || value > k_MaximumAlphamapResolution)
				{
					Debug.LogWarning("alphamapResolution is clamped to the range of [" + k_MinimumAlphamapResolution + ", " + k_MaximumAlphamapResolution + "].");
					internal_alphamapResolution = Math.Min(k_MaximumAlphamapResolution, Math.Max(value, k_MinimumAlphamapResolution));
				}
				Internal_alphamapResolution = internal_alphamapResolution;
			}
		}

		private extern int Internal_alphamapResolution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetSplatDatabase().GetAlphamapResolution")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetSplatDatabase().SetAlphamapResolution")]
			set;
		}

		public int alphamapWidth => alphamapResolution;

		public int alphamapHeight => alphamapResolution;

		public int baseMapResolution
		{
			get
			{
				return Internal_baseMapResolution;
			}
			set
			{
				int internal_baseMapResolution = value;
				if (value < k_MinimumBaseMapResolution || value > k_MaximumBaseMapResolution)
				{
					Debug.LogWarning("baseMapResolution is clamped to the range of [" + k_MinimumBaseMapResolution + ", " + k_MaximumBaseMapResolution + "].");
					internal_baseMapResolution = Math.Min(k_MaximumBaseMapResolution, Math.Max(value, k_MinimumBaseMapResolution));
				}
				Internal_baseMapResolution = internal_baseMapResolution;
			}
		}

		private extern int Internal_baseMapResolution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetSplatDatabase().GetBaseMapResolution")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetSplatDatabase().SetBaseMapResolution")]
			set;
		}

		public extern int alphamapTextureCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetSplatDatabase().GetAlphaTextureCount")]
			get;
		}

		public Texture2D[] alphamapTextures
		{
			get
			{
				Texture2D[] array = new Texture2D[alphamapTextureCount];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = GetAlphamapTexture(i);
				}
				return array;
			}
		}

		[Obsolete("Please use the terrainLayers API instead.", false)]
		public extern SplatPrototype[] splatPrototypes
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("TerrainDataScriptingInterface::GetSplatPrototypes", HasExplicitThis = true)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("TerrainDataScriptingInterface::SetSplatPrototypes", HasExplicitThis = true, ThrowsException = true)]
			[param: Unmarshalled]
			set;
		}

		public extern TerrainLayer[] terrainLayers
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("TerrainDataScriptingInterface::GetTerrainLayers", HasExplicitThis = true)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("TerrainDataScriptingInterface::SetTerrainLayers", HasExplicitThis = true)]
			[param: Unmarshalled]
			set;
		}

		internal extern TextureFormat atlasFormat
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetDetailDatabase().GetAtlasTexture()->GetTextureFormat")]
			get;
		}

		internal extern Terrain[] users
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		private static bool SupportsCopyTextureBetweenRTAndTexture => (SystemInfo.copyTextureSupport & (CopyTextureSupport.TextureToRT | CopyTextureSupport.RTToTexture)) == (CopyTextureSupport.TextureToRT | CopyTextureSupport.RTToTexture);

		public static string AlphamapTextureName => "alphamap";

		public static string HolesTextureName => "holes";

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		[StaticAccessor("TerrainDataScriptingInterface", StaticAccessorType.DoubleColon)]
		private static extern int GetBoundaryValue(BoundaryValueType type);

		public TerrainData()
		{
			Internal_Create(this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::Create")]
		private static extern void Internal_Create([Writable] TerrainData terrainData);

		[Obsolete("Please use DirtyHeightmapRegion instead.", false)]
		public void UpdateDirtyRegion(int x, int y, int width, int height, bool syncHeightmapTextureImmediately)
		{
			DirtyHeightmapRegion(new RectInt(x, y, width, height), syncHeightmapTextureImmediately ? TerrainHeightmapSyncControl.HeightOnly : TerrainHeightmapSyncControl.None);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetHeightmap().IsHolesTextureCompressed")]
		internal extern bool IsHolesTextureCompressed();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetHeightmap().GetHolesTexture")]
		internal extern RenderTexture GetHolesTexture();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetHeightmap().GetCompressedHolesTexture")]
		internal extern Texture2D GetCompressedHolesTexture();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetHeightmap().GetHeight")]
		public extern float GetHeight(int x, int y);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetHeightmap().GetInterpolatedHeight")]
		public extern float GetInterpolatedHeight(float x, float y);

		public float[,] GetInterpolatedHeights(float xBase, float yBase, int xCount, int yCount, float xInterval, float yInterval)
		{
			if (xCount <= 0)
			{
				throw new ArgumentOutOfRangeException("xCount");
			}
			if (yCount <= 0)
			{
				throw new ArgumentOutOfRangeException("yCount");
			}
			float[,] array = new float[yCount, xCount];
			Internal_GetInterpolatedHeights(array, xCount, 0, 0, xBase, yBase, xCount, yCount, xInterval, yInterval);
			return array;
		}

		public void GetInterpolatedHeights(float[,] results, int resultXOffset, int resultYOffset, float xBase, float yBase, int xCount, int yCount, float xInterval, float yInterval)
		{
			if (results == null)
			{
				throw new ArgumentNullException("results");
			}
			if (xCount <= 0)
			{
				throw new ArgumentOutOfRangeException("xCount");
			}
			if (yCount <= 0)
			{
				throw new ArgumentOutOfRangeException("yCount");
			}
			if (resultXOffset < 0 || resultXOffset + xCount > results.GetLength(1))
			{
				throw new ArgumentOutOfRangeException("resultXOffset");
			}
			if (resultYOffset < 0 || resultYOffset + yCount > results.GetLength(0))
			{
				throw new ArgumentOutOfRangeException("resultYOffset");
			}
			Internal_GetInterpolatedHeights(results, results.GetLength(1), resultXOffset, resultYOffset, xBase, yBase, xCount, yCount, xInterval, yInterval);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::GetInterpolatedHeights", HasExplicitThis = true)]
		private extern void Internal_GetInterpolatedHeights([Unmarshalled] float[,] results, int resultXDimension, int resultXOffset, int resultYOffset, float xBase, float yBase, int xCount, int yCount, float xInterval, float yInterval);

		public float[,] GetHeights(int xBase, int yBase, int width, int height)
		{
			if (xBase < 0 || yBase < 0 || xBase + width < 0 || yBase + height < 0 || xBase + width > heightmapResolution || yBase + height > heightmapResolution)
			{
				throw new ArgumentException("Trying to access out-of-bounds terrain height information.");
			}
			return Internal_GetHeights(xBase, yBase, width, height);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::GetHeights", HasExplicitThis = true)]
		private extern float[,] Internal_GetHeights(int xBase, int yBase, int width, int height);

		public void SetHeights(int xBase, int yBase, float[,] heights)
		{
			if (heights == null)
			{
				throw new NullReferenceException();
			}
			if (xBase + heights.GetLength(1) > heightmapResolution || xBase + heights.GetLength(1) < 0 || yBase + heights.GetLength(0) < 0 || xBase < 0 || yBase < 0 || yBase + heights.GetLength(0) > heightmapResolution)
			{
				throw new ArgumentException(UnityString.Format("X or Y base out of bounds. Setting up to {0}x{1} while map size is {2}x{2}", xBase + heights.GetLength(1), yBase + heights.GetLength(0), heightmapResolution));
			}
			Internal_SetHeights(xBase, yBase, heights.GetLength(1), heights.GetLength(0), heights);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::SetHeights", HasExplicitThis = true)]
		private extern void Internal_SetHeights(int xBase, int yBase, int width, int height, float[,] heights);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::GetPatchMinMaxHeights", HasExplicitThis = true)]
		public extern PatchExtents[] GetPatchMinMaxHeights();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::OverrideMinMaxPatchHeights", HasExplicitThis = true)]
		public extern void OverrideMinMaxPatchHeights(PatchExtents[] minMaxHeights);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::GetMaximumHeightError", HasExplicitThis = true)]
		public extern float[] GetMaximumHeightError();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::OverrideMaximumHeightError", HasExplicitThis = true)]
		public extern void OverrideMaximumHeightError(float[] maxError);

		public void SetHeightsDelayLOD(int xBase, int yBase, float[,] heights)
		{
			if (heights == null)
			{
				throw new ArgumentNullException("heights");
			}
			int length = heights.GetLength(0);
			int length2 = heights.GetLength(1);
			if (xBase < 0 || xBase + length2 < 0 || xBase + length2 > heightmapResolution)
			{
				throw new ArgumentException(UnityString.Format("X out of bounds - trying to set {0}-{1} but the terrain ranges from 0-{2}", xBase, xBase + length2, heightmapResolution));
			}
			if (yBase < 0 || yBase + length < 0 || yBase + length > heightmapResolution)
			{
				throw new ArgumentException(UnityString.Format("Y out of bounds - trying to set {0}-{1} but the terrain ranges from 0-{2}", yBase, yBase + length, heightmapResolution));
			}
			Internal_SetHeightsDelayLOD(xBase, yBase, length2, length, heights);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::SetHeightsDelayLOD", HasExplicitThis = true)]
		private extern void Internal_SetHeightsDelayLOD(int xBase, int yBase, int width, int height, float[,] heights);

		public bool IsHole(int x, int y)
		{
			if (x < 0 || x >= holesResolution || y < 0 || y >= holesResolution)
			{
				throw new ArgumentException("Trying to access out-of-bounds terrain holes information.");
			}
			return Internal_IsHole(x, y);
		}

		public bool[,] GetHoles(int xBase, int yBase, int width, int height)
		{
			if (xBase < 0 || yBase < 0 || width <= 0 || height <= 0 || xBase + width > holesResolution || yBase + height > holesResolution)
			{
				throw new ArgumentException("Trying to access out-of-bounds terrain holes information.");
			}
			return Internal_GetHoles(xBase, yBase, width, height);
		}

		public void SetHoles(int xBase, int yBase, bool[,] holes)
		{
			if (holes == null)
			{
				throw new ArgumentNullException("holes");
			}
			int length = holes.GetLength(0);
			int length2 = holes.GetLength(1);
			if (xBase < 0 || xBase + length2 > holesResolution)
			{
				throw new ArgumentException(UnityString.Format("X out of bounds - trying to set {0}-{1} but the terrain ranges from 0-{2}", xBase, xBase + length2, holesResolution));
			}
			if (yBase < 0 || yBase + length > holesResolution)
			{
				throw new ArgumentException(UnityString.Format("Y out of bounds - trying to set {0}-{1} but the terrain ranges from 0-{2}", yBase, yBase + length, holesResolution));
			}
			Internal_SetHoles(xBase, yBase, holes.GetLength(1), holes.GetLength(0), holes);
		}

		public void SetHolesDelayLOD(int xBase, int yBase, bool[,] holes)
		{
			if (holes == null)
			{
				throw new ArgumentNullException("holes");
			}
			int length = holes.GetLength(0);
			int length2 = holes.GetLength(1);
			if (xBase < 0 || xBase + length2 > holesResolution)
			{
				throw new ArgumentException(UnityString.Format("X out of bounds - trying to set {0}-{1} but the terrain ranges from 0-{2}", xBase, xBase + length2, holesResolution));
			}
			if (yBase < 0 || yBase + length > holesResolution)
			{
				throw new ArgumentException(UnityString.Format("Y out of bounds - trying to set {0}-{1} but the terrain ranges from 0-{2}", yBase, yBase + length, holesResolution));
			}
			Internal_SetHolesDelayLOD(xBase, yBase, length2, length, holes);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::SetHoles", HasExplicitThis = true)]
		private extern void Internal_SetHoles(int xBase, int yBase, int width, int height, bool[,] holes);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::GetHoles", HasExplicitThis = true)]
		private extern bool[,] Internal_GetHoles(int xBase, int yBase, int width, int height);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::IsHole", HasExplicitThis = true)]
		private extern bool Internal_IsHole(int x, int y);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::SetHolesDelayLOD", HasExplicitThis = true)]
		private extern void Internal_SetHolesDelayLOD(int xBase, int yBase, int width, int height, bool[,] holes);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetHeightmap().GetSteepness")]
		public extern float GetSteepness(float x, float y);

		[NativeName("GetHeightmap().GetInterpolatedNormal")]
		public Vector3 GetInterpolatedNormal(float x, float y)
		{
			GetInterpolatedNormal_Injected(x, y, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetHeightmap().GetAdjustedSize")]
		internal extern int GetAdjustedSize(int size);

		public void SetDetailResolution(int detailResolution, int resolutionPerPatch)
		{
			if (detailResolution < 0)
			{
				Debug.LogWarning("detailResolution must not be negative.");
				detailResolution = 0;
			}
			if (resolutionPerPatch < k_MinimumDetailResolutionPerPatch || resolutionPerPatch > k_MaximumDetailResolutionPerPatch)
			{
				Debug.LogWarning("resolutionPerPatch is clamped to the range of [" + k_MinimumDetailResolutionPerPatch + ", " + k_MaximumDetailResolutionPerPatch + "].");
				resolutionPerPatch = Math.Min(k_MaximumDetailResolutionPerPatch, Math.Max(resolutionPerPatch, k_MinimumDetailResolutionPerPatch));
			}
			int num = detailResolution / resolutionPerPatch;
			if (num > k_MaximumDetailPatchCount)
			{
				Debug.LogWarning("Patch count (detailResolution / resolutionPerPatch) is clamped to the range of [0, " + k_MaximumDetailPatchCount + "].");
				num = Math.Min(k_MaximumDetailPatchCount, Math.Max(num, 0));
			}
			Internal_SetDetailResolution(num, resolutionPerPatch);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetDetailDatabase().SetDetailResolution")]
		private extern void Internal_SetDetailResolution(int patchCount, int resolutionPerPatch);

		public void SetDetailScatterMode(DetailScatterMode scatterMode)
		{
			Internal_SetDetailScatterMode(scatterMode);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetDetailDatabase().SetDetailScatterMode")]
		private extern void Internal_SetDetailScatterMode(DetailScatterMode scatterMode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetDetailDatabase().ResetDirtyDetails")]
		internal extern void ResetDirtyDetails();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::RefreshPrototypes", HasExplicitThis = true)]
		public extern void RefreshPrototypes();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::GetSupportedLayers", HasExplicitThis = true)]
		public extern int[] GetSupportedLayers(int xBase, int yBase, int totalWidth, int totalHeight);

		public int[] GetSupportedLayers(Vector2Int positionBase, Vector2Int size)
		{
			return GetSupportedLayers(positionBase.x, positionBase.y, size.x, size.y);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::GetDetailLayer", HasExplicitThis = true)]
		public extern int[,] GetDetailLayer(int xBase, int yBase, int width, int height, int layer);

		public int[,] GetDetailLayer(Vector2Int positionBase, Vector2Int size, int layer)
		{
			return GetDetailLayer(positionBase.x, positionBase.y, size.x, size.y, layer);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::ComputeDetailInstanceTransforms", HasExplicitThis = true)]
		public extern DetailInstanceTransform[] ComputeDetailInstanceTransforms(int patchX, int patchY, int layer, float density, out Bounds bounds);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::ComputeDetailCoverage", HasExplicitThis = true)]
		public extern float ComputeDetailCoverage(int detailPrototypeIndex);

		public void SetDetailLayer(int xBase, int yBase, int layer, int[,] details)
		{
			Internal_SetDetailLayer(xBase, yBase, details.GetLength(1), details.GetLength(0), layer, details);
		}

		public void SetDetailLayer(Vector2Int basePosition, int layer, int[,] details)
		{
			SetDetailLayer(basePosition.x, basePosition.y, layer, details);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::SetDetailLayer", HasExplicitThis = true)]
		private extern void Internal_SetDetailLayer(int xBase, int yBase, int totalWidth, int totalHeight, int detailIndex, int[,] data);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetTreeDatabase().GetInstances")]
		private extern TreeInstance[] Internal_GetTreeInstances();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::SetTreeInstances", HasExplicitThis = true)]
		public extern void SetTreeInstances([NotNull("ArgumentNullException")] TreeInstance[] instances, bool snapToHeightmap);

		public TreeInstance GetTreeInstance(int index)
		{
			if (index < 0 || index >= treeInstanceCount)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return Internal_GetTreeInstance(index);
		}

		[FreeFunction("TerrainDataScriptingInterface::GetTreeInstance", HasExplicitThis = true)]
		private TreeInstance Internal_GetTreeInstance(int index)
		{
			Internal_GetTreeInstance_Injected(index, out var ret);
			return ret;
		}

		[NativeThrows]
		[FreeFunction("TerrainDataScriptingInterface::SetTreeInstance", HasExplicitThis = true)]
		public void SetTreeInstance(int index, TreeInstance instance)
		{
			SetTreeInstance_Injected(index, ref instance);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetTreeDatabase().RemoveTreePrototype")]
		internal extern void RemoveTreePrototype(int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetDetailDatabase().RemoveDetailPrototype")]
		public extern void RemoveDetailPrototype(int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetTreeDatabase().NeedUpgradeScaledPrototypes")]
		internal extern bool NeedUpgradeScaledTreePrototypes();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::UpgradeScaledTreePrototype", HasExplicitThis = true)]
		internal extern void UpgradeScaledTreePrototype();

		public float[,,] GetAlphamaps(int x, int y, int width, int height)
		{
			if (x < 0 || y < 0 || width < 0 || height < 0)
			{
				throw new ArgumentException("Invalid argument for GetAlphaMaps");
			}
			return Internal_GetAlphamaps(x, y, width, height);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::GetAlphamaps", HasExplicitThis = true)]
		private extern float[,,] Internal_GetAlphamaps(int x, int y, int width, int height);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[RequiredByNativeCode]
		[NativeName("GetSplatDatabase().GetAlphamapResolution")]
		internal extern float GetAlphamapResolutionInternal();

		public void SetAlphamaps(int x, int y, float[,,] map)
		{
			if (map.GetLength(2) != alphamapLayers)
			{
				throw new Exception(UnityString.Format("Float array size wrong (layers should be {0})", alphamapLayers));
			}
			Internal_SetAlphamaps(x, y, map.GetLength(1), map.GetLength(0), map);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::SetAlphamaps", HasExplicitThis = true)]
		private extern void Internal_SetAlphamaps(int x, int y, int width, int height, float[,,] map);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetSplatDatabase().SetBaseMapsDirty")]
		public extern void SetBaseMapDirty();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetSplatDatabase().GetAlphaTexture")]
		public extern Texture2D GetAlphamapTexture(int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetTreeDatabase().AddTree")]
		internal extern void AddTree(ref TreeInstance tree);

		[NativeName("GetTreeDatabase().RemoveTrees")]
		internal int RemoveTrees(Vector2 position, float radius, int prototypeIndex)
		{
			return RemoveTrees_Injected(ref position, radius, prototypeIndex);
		}

		[NativeName("GetHeightmap().CopyHeightmapFromActiveRenderTexture")]
		private void Internal_CopyActiveRenderTextureToHeightmap(RectInt rect, int destX, int destY, TerrainHeightmapSyncControl syncControl)
		{
			Internal_CopyActiveRenderTextureToHeightmap_Injected(ref rect, destX, destY, syncControl);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetHeightmap().DirtyHeightmapRegion")]
		private extern void Internal_DirtyHeightmapRegion(int x, int y, int width, int height, TerrainHeightmapSyncControl syncControl);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetHeightmap().SyncHeightmapGPUModifications")]
		public extern void SyncHeightmap();

		[NativeName("GetHeightmap().CopyHolesFromActiveRenderTexture")]
		private void Internal_CopyActiveRenderTextureToHoles(RectInt rect, int destX, int destY, bool allowDelayedCPUSync)
		{
			Internal_CopyActiveRenderTextureToHoles_Injected(ref rect, destX, destY, allowDelayedCPUSync);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetHeightmap().DirtyHolesRegion")]
		private extern void Internal_DirtyHolesRegion(int x, int y, int width, int height, bool allowDelayedCPUSync);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetHeightmap().SyncHolesGPUModifications")]
		private extern void Internal_SyncHoles();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetSplatDatabase().MarkDirtyRegion")]
		private extern void Internal_MarkAlphamapDirtyRegion(int alphamapIndex, int x, int y, int width, int height);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetSplatDatabase().ClearDirtyRegion")]
		private extern void Internal_ClearAlphamapDirtyRegion(int alphamapIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetSplatDatabase().SyncGPUModifications")]
		private extern void Internal_SyncAlphamaps();

		public void CopyActiveRenderTextureToHeightmap(RectInt sourceRect, Vector2Int dest, TerrainHeightmapSyncControl syncControl)
		{
			RenderTexture active = RenderTexture.active;
			if (active == null)
			{
				throw new InvalidOperationException("Active RenderTexture is null.");
			}
			if (sourceRect.x < 0 || sourceRect.y < 0 || sourceRect.xMax > active.width || sourceRect.yMax > active.height)
			{
				throw new ArgumentOutOfRangeException("sourceRect");
			}
			if (dest.x < 0 || dest.x + sourceRect.width > heightmapResolution)
			{
				throw new ArgumentOutOfRangeException("dest.x");
			}
			if (dest.y < 0 || dest.y + sourceRect.height > heightmapResolution)
			{
				throw new ArgumentOutOfRangeException("dest.y");
			}
			Internal_CopyActiveRenderTextureToHeightmap(sourceRect, dest.x, dest.y, syncControl);
			TerrainCallbacks.InvokeHeightmapChangedCallback(this, new RectInt(dest.x, dest.y, sourceRect.width, sourceRect.height), syncControl == TerrainHeightmapSyncControl.HeightAndLod);
		}

		public void DirtyHeightmapRegion(RectInt region, TerrainHeightmapSyncControl syncControl)
		{
			int num = heightmapResolution;
			if (region.x < 0 || region.x >= num)
			{
				throw new ArgumentOutOfRangeException("region.x");
			}
			if (region.width <= 0 || region.xMax > num)
			{
				throw new ArgumentOutOfRangeException("region.width");
			}
			if (region.y < 0 || region.y >= num)
			{
				throw new ArgumentOutOfRangeException("region.y");
			}
			if (region.height <= 0 || region.yMax > num)
			{
				throw new ArgumentOutOfRangeException("region.height");
			}
			Internal_DirtyHeightmapRegion(region.x, region.y, region.width, region.height, syncControl);
			TerrainCallbacks.InvokeHeightmapChangedCallback(this, region, syncControl == TerrainHeightmapSyncControl.HeightAndLod);
		}

		public void CopyActiveRenderTextureToTexture(string textureName, int textureIndex, RectInt sourceRect, Vector2Int dest, bool allowDelayedCPUSync)
		{
			if (string.IsNullOrEmpty(textureName))
			{
				throw new ArgumentNullException("textureName");
			}
			RenderTexture active = RenderTexture.active;
			if (active == null)
			{
				throw new InvalidOperationException("Active RenderTexture is null.");
			}
			int num = 0;
			int num2 = 0;
			if (textureName == HolesTextureName)
			{
				if (textureIndex != 0)
				{
					throw new ArgumentOutOfRangeException("textureIndex");
				}
				if (active == holesTexture)
				{
					throw new ArgumentException("source", "Active RenderTexture cannot be holesTexture.");
				}
				num = (num2 = holesResolution);
			}
			else
			{
				if (!(textureName == AlphamapTextureName))
				{
					throw new ArgumentException("Unrecognized terrain texture name: \"" + textureName + "\"");
				}
				if (textureIndex < 0 || textureIndex >= alphamapTextureCount)
				{
					throw new ArgumentOutOfRangeException("textureIndex");
				}
				num = (num2 = alphamapResolution);
			}
			if (sourceRect.x < 0 || sourceRect.y < 0 || sourceRect.xMax > active.width || sourceRect.yMax > active.height)
			{
				throw new ArgumentOutOfRangeException("sourceRect");
			}
			if (dest.x < 0 || dest.x + sourceRect.width > num)
			{
				throw new ArgumentOutOfRangeException("dest.x");
			}
			if (dest.y < 0 || dest.y + sourceRect.height > num2)
			{
				throw new ArgumentOutOfRangeException("dest.y");
			}
			if (textureName == HolesTextureName)
			{
				Internal_CopyActiveRenderTextureToHoles(sourceRect, dest.x, dest.y, allowDelayedCPUSync);
				return;
			}
			Texture2D alphamapTexture = GetAlphamapTexture(textureIndex);
			allowDelayedCPUSync = allowDelayedCPUSync && SupportsCopyTextureBetweenRTAndTexture;
			if (allowDelayedCPUSync)
			{
				if (alphamapTexture.mipmapCount > 1)
				{
					RenderTextureDescriptor desc = new RenderTextureDescriptor(alphamapTexture.width, alphamapTexture.height, active.graphicsFormat, active.depthStencilFormat);
					desc.sRGB = false;
					desc.useMipMap = true;
					desc.autoGenerateMips = false;
					RenderTexture temporary = RenderTexture.GetTemporary(desc);
					if (!temporary.IsCreated())
					{
						temporary.Create();
					}
					Graphics.CopyTexture(alphamapTexture, 0, 0, temporary, 0, 0);
					Graphics.CopyTexture(active, 0, 0, sourceRect.x, sourceRect.y, sourceRect.width, sourceRect.height, temporary, 0, 0, dest.x, dest.y);
					temporary.GenerateMips();
					Graphics.CopyTexture(temporary, alphamapTexture);
					RenderTexture.ReleaseTemporary(temporary);
				}
				else
				{
					Graphics.CopyTexture(active, 0, 0, sourceRect.x, sourceRect.y, sourceRect.width, sourceRect.height, alphamapTexture, 0, 0, dest.x, dest.y);
				}
				Internal_MarkAlphamapDirtyRegion(textureIndex, dest.x, dest.y, sourceRect.width, sourceRect.height);
			}
			else
			{
				alphamapTexture.ReadPixels(new Rect(sourceRect.x, sourceRect.y, sourceRect.width, sourceRect.height), dest.x, dest.y);
				alphamapTexture.Apply(updateMipmaps: true);
				Internal_ClearAlphamapDirtyRegion(textureIndex);
			}
			TerrainCallbacks.InvokeTextureChangedCallback(this, textureName, new RectInt(dest.x, dest.y, sourceRect.width, sourceRect.height), !allowDelayedCPUSync);
		}

		public void DirtyTextureRegion(string textureName, RectInt region, bool allowDelayedCPUSync)
		{
			if (string.IsNullOrEmpty(textureName))
			{
				throw new ArgumentNullException("textureName");
			}
			int num = 0;
			if (textureName == AlphamapTextureName)
			{
				num = alphamapResolution;
			}
			else
			{
				if (!(textureName == HolesTextureName))
				{
					throw new ArgumentException("Unrecognized terrain texture name: \"" + textureName + "\"");
				}
				num = holesResolution;
			}
			if (region.x < 0 || region.x >= num)
			{
				throw new ArgumentOutOfRangeException("region.x");
			}
			if (region.width <= 0 || region.xMax > num)
			{
				throw new ArgumentOutOfRangeException("region.width");
			}
			if (region.y < 0 || region.y >= num)
			{
				throw new ArgumentOutOfRangeException("region.y");
			}
			if (region.height <= 0 || region.yMax > num)
			{
				throw new ArgumentOutOfRangeException("region.height");
			}
			if (textureName == HolesTextureName)
			{
				Internal_DirtyHolesRegion(region.x, region.y, region.width, region.height, allowDelayedCPUSync);
				return;
			}
			Internal_MarkAlphamapDirtyRegion(-1, region.x, region.y, region.width, region.height);
			if (!allowDelayedCPUSync)
			{
				SyncTexture(textureName);
			}
			else
			{
				TerrainCallbacks.InvokeTextureChangedCallback(this, textureName, region, synched: false);
			}
		}

		public void SyncTexture(string textureName)
		{
			if (string.IsNullOrEmpty(textureName))
			{
				throw new ArgumentNullException("textureName");
			}
			if (textureName == AlphamapTextureName)
			{
				Internal_SyncAlphamaps();
				return;
			}
			if (textureName == HolesTextureName)
			{
				if (IsHolesTextureCompressed())
				{
					throw new InvalidOperationException("Holes texture is compressed. Compressed holes texture can not be read back from GPU. Use TerrainData.enableHolesTextureCompression to disable holes texture compression.");
				}
				Internal_SyncHoles();
				return;
			}
			throw new ArgumentException("Unrecognized terrain texture name: \"" + textureName + "\"");
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_heightmapScale_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_size_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_size_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_bounds_Injected(out Bounds ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetInterpolatedNormal_Injected(float x, float y, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_wavingGrassTint_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_wavingGrassTint_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetTreeInstance_Injected(int index, out TreeInstance ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTreeInstance_Injected(int index, ref TreeInstance instance);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int RemoveTrees_Injected(ref Vector2 position, float radius, int prototypeIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_CopyActiveRenderTextureToHeightmap_Injected(ref RectInt rect, int destX, int destY, TerrainHeightmapSyncControl syncControl);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_CopyActiveRenderTextureToHoles_Injected(ref RectInt rect, int destX, int destY, bool allowDelayedCPUSync);
	}
	[StructLayout(LayoutKind.Sequential)]
	[UsedByNativeCode]
	[NativeHeader("Modules/Terrain/Public/TerrainLayerScriptingInterface.h")]
	[NativeHeader("TerrainScriptingClasses.h")]
	public sealed class TerrainLayer : Object
	{
		public extern Texture2D diffuseTexture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Texture2D normalMapTexture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Texture2D maskMapTexture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector2 tileSize
		{
			get
			{
				get_tileSize_Injected(out var ret);
				return ret;
			}
			set
			{
				set_tileSize_Injected(ref value);
			}
		}

		public Vector2 tileOffset
		{
			get
			{
				get_tileOffset_Injected(out var ret);
				return ret;
			}
			set
			{
				set_tileOffset_Injected(ref value);
			}
		}

		[NativeProperty("SpecularColor")]
		public Color specular
		{
			get
			{
				get_specular_Injected(out var ret);
				return ret;
			}
			set
			{
				set_specular_Injected(ref value);
			}
		}

		public extern float metallic
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float smoothness
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float normalScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector4 diffuseRemapMin
		{
			get
			{
				get_diffuseRemapMin_Injected(out var ret);
				return ret;
			}
			set
			{
				set_diffuseRemapMin_Injected(ref value);
			}
		}

		public Vector4 diffuseRemapMax
		{
			get
			{
				get_diffuseRemapMax_Injected(out var ret);
				return ret;
			}
			set
			{
				set_diffuseRemapMax_Injected(ref value);
			}
		}

		public Vector4 maskMapRemapMin
		{
			get
			{
				get_maskMapRemapMin_Injected(out var ret);
				return ret;
			}
			set
			{
				set_maskMapRemapMin_Injected(ref value);
			}
		}

		public Vector4 maskMapRemapMax
		{
			get
			{
				get_maskMapRemapMax_Injected(out var ret);
				return ret;
			}
			set
			{
				set_maskMapRemapMax_Injected(ref value);
			}
		}

		public TerrainLayer()
		{
			Internal_Create(this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainLayerScriptingInterface::Create")]
		private static extern void Internal_Create([Writable] TerrainLayer layer);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_tileSize_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_tileSize_Injected(ref Vector2 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_tileOffset_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_tileOffset_Injected(ref Vector2 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_specular_Injected(out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_specular_Injected(ref Color value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_diffuseRemapMin_Injected(out Vector4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_diffuseRemapMin_Injected(ref Vector4 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_diffuseRemapMax_Injected(out Vector4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_diffuseRemapMax_Injected(ref Vector4 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_maskMapRemapMin_Injected(out Vector4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_maskMapRemapMin_Injected(ref Vector4 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_maskMapRemapMax_Injected(out Vector4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_maskMapRemapMax_Injected(ref Vector4 value);
	}
}
namespace UnityEngine.TerrainUtils
{
	internal enum TerrainMapStatusCode
	{
		OK = 0,
		Overlapping = 1,
		SizeMismatch = 4,
		EdgeAlignmentMismatch = 8
	}
	public readonly struct TerrainTileCoord
	{
		public readonly int tileX;

		public readonly int tileZ;

		public TerrainTileCoord(int tileX, int tileZ)
		{
			this.tileX = tileX;
			this.tileZ = tileZ;
		}
	}
	public class TerrainMap
	{
		private struct QueueElement
		{
			public readonly int tileX;

			public readonly int tileZ;

			public readonly Terrain terrain;

			public QueueElement(int tileX, int tileZ, Terrain terrain)
			{
				this.tileX = tileX;
				this.tileZ = tileZ;
				this.terrain = terrain;
			}
		}

		private Vector3 m_patchSize;

		private TerrainMapStatusCode m_errorCode;

		private Dictionary<TerrainTileCoord, Terrain> m_terrainTiles;

		public Dictionary<TerrainTileCoord, Terrain> terrainTiles => m_terrainTiles;

		public Terrain GetTerrain(int tileX, int tileZ)
		{
			Terrain value = null;
			m_terrainTiles.TryGetValue(new TerrainTileCoord(tileX, tileZ), out value);
			return value;
		}

		public static TerrainMap CreateFromConnectedNeighbors(Terrain originTerrain, Predicate<Terrain> filter = null, bool fullValidation = true)
		{
			if (originTerrain == null)
			{
				return null;
			}
			if (originTerrain.terrainData == null)
			{
				return null;
			}
			TerrainMap terrainMap = new TerrainMap();
			Queue<QueueElement> queue = new Queue<QueueElement>();
			queue.Enqueue(new QueueElement(0, 0, originTerrain));
			int num = Terrain.activeTerrains.Length;
			while (queue.Count > 0)
			{
				QueueElement queueElement = queue.Dequeue();
				if ((filter == null || filter(queueElement.terrain)) && terrainMap.TryToAddTerrain(queueElement.tileX, queueElement.tileZ, queueElement.terrain))
				{
					if (terrainMap.m_terrainTiles.Count > num)
					{
						break;
					}
					if (queueElement.terrain.leftNeighbor != null)
					{
						queue.Enqueue(new QueueElement(queueElement.tileX - 1, queueElement.tileZ, queueElement.terrain.leftNeighbor));
					}
					if (queueElement.terrain.bottomNeighbor != null)
					{
						queue.Enqueue(new QueueElement(queueElement.tileX, queueElement.tileZ - 1, queueElement.terrain.bottomNeighbor));
					}
					if (queueElement.terrain.rightNeighbor != null)
					{
						queue.Enqueue(new QueueElement(queueElement.tileX + 1, queueElement.tileZ, queueElement.terrain.rightNeighbor));
					}
					if (queueElement.terrain.topNeighbor != null)
					{
						queue.Enqueue(new QueueElement(queueElement.tileX, queueElement.tileZ + 1, queueElement.terrain.topNeighbor));
					}
				}
			}
			if (fullValidation)
			{
				terrainMap.Validate();
			}
			return terrainMap;
		}

		public static TerrainMap CreateFromPlacement(Terrain originTerrain, Predicate<Terrain> filter = null, bool fullValidation = true)
		{
			if (Terrain.activeTerrains == null || Terrain.activeTerrains.Length == 0 || originTerrain == null)
			{
				return null;
			}
			if (originTerrain.terrainData == null)
			{
				return null;
			}
			int groupID = originTerrain.groupingID;
			float x = originTerrain.transform.position.x;
			float z = originTerrain.transform.position.z;
			float x2 = originTerrain.terrainData.size.x;
			float z2 = originTerrain.terrainData.size.z;
			if (filter == null)
			{
				filter = (Terrain terrain) => terrain.groupingID == groupID;
			}
			return CreateFromPlacement(new Vector2(x, z), new Vector2(x2, z2), filter, fullValidation);
		}

		public static TerrainMap CreateFromPlacement(Vector2 gridOrigin, Vector2 gridSize, Predicate<Terrain> filter = null, bool fullValidation = true)
		{
			if (Terrain.activeTerrains == null || Terrain.activeTerrains.Length == 0)
			{
				return null;
			}
			TerrainMap terrainMap = new TerrainMap();
			float num = 1f / gridSize.x;
			float num2 = 1f / gridSize.y;
			Terrain[] activeTerrains = Terrain.activeTerrains;
			foreach (Terrain terrain in activeTerrains)
			{
				if (!(terrain.terrainData == null) && (filter == null || filter(terrain)))
				{
					Vector3 position = terrain.transform.position;
					int tileX = Mathf.RoundToInt((position.x - gridOrigin.x) * num);
					int tileZ = Mathf.RoundToInt((position.z - gridOrigin.y) * num2);
					terrainMap.TryToAddTerrain(tileX, tileZ, terrain);
				}
			}
			if (fullValidation)
			{
				terrainMap.Validate();
			}
			return (terrainMap.m_terrainTiles.Count > 0) ? terrainMap : null;
		}

		public TerrainMap()
		{
			m_errorCode = TerrainMapStatusCode.OK;
			m_terrainTiles = new Dictionary<TerrainTileCoord, Terrain>();
		}

		private void AddTerrainInternal(int x, int z, Terrain terrain)
		{
			if (m_terrainTiles.Count == 0)
			{
				m_patchSize = terrain.terrainData.size;
			}
			else if (terrain.terrainData.size != m_patchSize)
			{
				m_errorCode |= TerrainMapStatusCode.SizeMismatch;
			}
			m_terrainTiles.Add(new TerrainTileCoord(x, z), terrain);
		}

		private bool TryToAddTerrain(int tileX, int tileZ, Terrain terrain)
		{
			bool result = false;
			if (terrain != null)
			{
				Terrain terrain2 = GetTerrain(tileX, tileZ);
				if (terrain2 != null)
				{
					if (terrain2 != terrain)
					{
						m_errorCode |= TerrainMapStatusCode.Overlapping;
					}
				}
				else
				{
					AddTerrainInternal(tileX, tileZ, terrain);
					result = true;
				}
			}
			return result;
		}

		private void ValidateTerrain(int tileX, int tileZ)
		{
			Terrain terrain = GetTerrain(tileX, tileZ);
			if (terrain != null)
			{
				Terrain terrain2 = GetTerrain(tileX - 1, tileZ);
				Terrain terrain3 = GetTerrain(tileX + 1, tileZ);
				Terrain terrain4 = GetTerrain(tileX, tileZ + 1);
				Terrain terrain5 = GetTerrain(tileX, tileZ - 1);
				if ((bool)terrain2 && (!Mathf.Approximately(terrain.transform.position.x, terrain2.transform.position.x + terrain2.terrainData.size.x) || !Mathf.Approximately(terrain.transform.position.z, terrain2.transform.position.z)))
				{
					m_errorCode |= TerrainMapStatusCode.EdgeAlignmentMismatch;
				}
				if ((bool)terrain3 && (!Mathf.Approximately(terrain.transform.position.x + terrain.terrainData.size.x, terrain3.transform.position.x) || !Mathf.Approximately(terrain.transform.position.z, terrain3.transform.position.z)))
				{
					m_errorCode |= TerrainMapStatusCode.EdgeAlignmentMismatch;
				}
				if ((bool)terrain4 && (!Mathf.Approximately(terrain.transform.position.x, terrain4.transform.position.x) || !Mathf.Approximately(terrain.transform.position.z + terrain.terrainData.size.z, terrain4.transform.position.z)))
				{
					m_errorCode |= TerrainMapStatusCode.EdgeAlignmentMismatch;
				}
				if ((bool)terrain5 && (!Mathf.Approximately(terrain.transform.position.x, terrain5.transform.position.x) || !Mathf.Approximately(terrain.transform.position.z, terrain5.transform.position.z + terrain5.terrainData.size.z)))
				{
					m_errorCode |= TerrainMapStatusCode.EdgeAlignmentMismatch;
				}
			}
		}

		private TerrainMapStatusCode Validate()
		{
			foreach (TerrainTileCoord key in m_terrainTiles.Keys)
			{
				ValidateTerrain(key.tileX, key.tileZ);
			}
			return m_errorCode;
		}
	}
	[MovedFrom("UnityEngine.Experimental.TerrainAPI")]
	public static class TerrainUtility
	{
		internal static bool ValidTerrainsExist()
		{
			return Terrain.activeTerrains != null && Terrain.activeTerrains.Length != 0;
		}

		internal static void ClearConnectivity()
		{
			Terrain[] activeTerrains = Terrain.activeTerrains;
			foreach (Terrain terrain in activeTerrains)
			{
				if (terrain.allowAutoConnect)
				{
					terrain.SetNeighbors(null, null, null, null);
				}
			}
		}

		internal static Dictionary<int, TerrainMap> CollectTerrains(bool onlyAutoConnectedTerrains = true)
		{
			if (!ValidTerrainsExist())
			{
				return null;
			}
			Dictionary<int, TerrainMap> dictionary = new Dictionary<int, TerrainMap>();
			Terrain[] activeTerrains = Terrain.activeTerrains;
			foreach (Terrain t in activeTerrains)
			{
				if ((!onlyAutoConnectedTerrains || t.allowAutoConnect) && !dictionary.ContainsKey(t.groupingID))
				{
					TerrainMap terrainMap = TerrainMap.CreateFromPlacement(t, (Terrain x) => x.groupingID == t.groupingID && (!onlyAutoConnectedTerrains || x.allowAutoConnect));
					if (terrainMap != null)
					{
						dictionary.Add(t.groupingID, terrainMap);
					}
				}
			}
			return (dictionary.Count != 0) ? dictionary : null;
		}

		[RequiredByNativeCode]
		public static void AutoConnect()
		{
			if (!ValidTerrainsExist())
			{
				return;
			}
			ClearConnectivity();
			Dictionary<int, TerrainMap> dictionary = CollectTerrains();
			if (dictionary == null)
			{
				return;
			}
			foreach (KeyValuePair<int, TerrainMap> item in dictionary)
			{
				TerrainMap value = item.Value;
				foreach (KeyValuePair<TerrainTileCoord, Terrain> terrainTile in value.terrainTiles)
				{
					TerrainTileCoord key = terrainTile.Key;
					Terrain terrain = value.GetTerrain(key.tileX, key.tileZ);
					Terrain terrain2 = value.GetTerrain(key.tileX - 1, key.tileZ);
					Terrain terrain3 = value.GetTerrain(key.tileX + 1, key.tileZ);
					Terrain terrain4 = value.GetTerrain(key.tileX, key.tileZ + 1);
					Terrain terrain5 = value.GetTerrain(key.tileX, key.tileZ - 1);
					terrain.SetNeighbors(terrain2, terrain4, terrain3, terrain5);
				}
			}
		}
	}
}
namespace UnityEngine.TerrainTools
{
	[MovedFrom("UnityEngine.Experimental.TerrainAPI")]
	public struct BrushTransform
	{
		public Vector2 brushOrigin { get; }

		public Vector2 brushU { get; }

		public Vector2 brushV { get; }

		public Vector2 targetOrigin { get; }

		public Vector2 targetX { get; }

		public Vector2 targetY { get; }

		public BrushTransform(Vector2 brushOrigin, Vector2 brushU, Vector2 brushV)
		{
			float num = brushU.x * brushV.y - brushU.y * brushV.x;
			float num2 = (Mathf.Approximately(num, 0f) ? 1f : (1f / num));
			Vector2 vector = new Vector2(brushV.y, 0f - brushU.y) * num2;
			Vector2 vector2 = new Vector2(0f - brushV.x, brushU.x) * num2;
			Vector2 vector3 = (0f - brushOrigin.x) * vector - brushOrigin.y * vector2;
			this.brushOrigin = brushOrigin;
			this.brushU = brushU;
			this.brushV = brushV;
			targetOrigin = vector3;
			targetX = vector;
			targetY = vector2;
		}

		public Rect GetBrushXYBounds()
		{
			Vector2 vector = brushOrigin + brushU;
			Vector2 vector2 = brushOrigin + brushV;
			Vector2 vector3 = brushOrigin + brushU + brushV;
			float xmin = Mathf.Min(Mathf.Min(brushOrigin.x, vector.x), Mathf.Min(vector2.x, vector3.x));
			float xmax = Mathf.Max(Mathf.Max(brushOrigin.x, vector.x), Mathf.Max(vector2.x, vector3.x));
			float ymin = Mathf.Min(Mathf.Min(brushOrigin.y, vector.y), Mathf.Min(vector2.y, vector3.y));
			float ymax = Mathf.Max(Mathf.Max(brushOrigin.y, vector.y), Mathf.Max(vector2.y, vector3.y));
			return Rect.MinMaxRect(xmin, ymin, xmax, ymax);
		}

		public static BrushTransform FromRect(Rect brushRect)
		{
			Vector2 min = brushRect.min;
			Vector2 vector = new Vector2(brushRect.width, 0f);
			Vector2 vector2 = new Vector2(0f, brushRect.height);
			return new BrushTransform(min, vector, vector2);
		}

		public Vector2 ToBrushUV(Vector2 targetXY)
		{
			return targetXY.x * targetX + targetXY.y * targetY + targetOrigin;
		}

		public Vector2 FromBrushUV(Vector2 brushUV)
		{
			return brushUV.x * brushU + brushUV.y * brushV + brushOrigin;
		}
	}
	[MovedFrom("UnityEngine.Experimental.TerrainAPI")]
	public class PaintContext
	{
		public interface ITerrainInfo
		{
			Terrain terrain { get; }

			RectInt clippedTerrainPixels { get; }

			RectInt clippedPCPixels { get; }

			RectInt paddedTerrainPixels { get; }

			RectInt paddedPCPixels { get; }

			bool gatherEnable { get; set; }

			bool scatterEnable { get; set; }

			object userData { get; set; }
		}

		private class TerrainTile : ITerrainInfo
		{
			public Terrain terrain;

			public Vector2Int tileOriginPixels;

			public RectInt clippedTerrainPixels;

			public RectInt clippedPCPixels;

			public RectInt paddedTerrainPixels;

			public RectInt paddedPCPixels;

			public object userData;

			public bool gatherEnable;

			public bool scatterEnable;

			Terrain ITerrainInfo.terrain => terrain;

			RectInt ITerrainInfo.clippedTerrainPixels => clippedTerrainPixels;

			RectInt ITerrainInfo.clippedPCPixels => clippedPCPixels;

			RectInt ITerrainInfo.paddedTerrainPixels => paddedTerrainPixels;

			RectInt ITerrainInfo.paddedPCPixels => paddedPCPixels;

			bool ITerrainInfo.gatherEnable
			{
				get
				{
					return gatherEnable;
				}
				set
				{
					gatherEnable = value;
				}
			}

			bool ITerrainInfo.scatterEnable
			{
				get
				{
					return scatterEnable;
				}
				set
				{
					scatterEnable = value;
				}
			}

			object ITerrainInfo.userData
			{
				get
				{
					return userData;
				}
				set
				{
					userData = value;
				}
			}

			public static TerrainTile Make(Terrain terrain, int tileOriginPixelsX, int tileOriginPixelsY, RectInt pixelRect, int targetTextureWidth, int targetTextureHeight, int edgePad = 0)
			{
				TerrainTile terrainTile = new TerrainTile
				{
					terrain = terrain,
					gatherEnable = true,
					scatterEnable = true,
					tileOriginPixels = new Vector2Int(tileOriginPixelsX, tileOriginPixelsY),
					clippedTerrainPixels = new RectInt
					{
						x = Mathf.Max(0, pixelRect.x - tileOriginPixelsX),
						y = Mathf.Max(0, pixelRect.y - tileOriginPixelsY),
						xMax = Mathf.Min(targetTextureWidth, pixelRect.xMax - tileOriginPixelsX),
						yMax = Mathf.Min(targetTextureHeight, pixelRect.yMax - tileOriginPixelsY)
					}
				};
				terrainTile.clippedPCPixels = new RectInt(terrainTile.clippedTerrainPixels.x + terrainTile.tileOriginPixels.x - pixelRect.x, terrainTile.clippedTerrainPixels.y + terrainTile.tileOriginPixels.y - pixelRect.y, terrainTile.clippedTerrainPixels.width, terrainTile.clippedTerrainPixels.height);
				int num = ((terrain.leftNeighbor == null) ? edgePad : 0);
				int num2 = ((terrain.rightNeighbor == null) ? edgePad : 0);
				int num3 = ((terrain.bottomNeighbor == null) ? edgePad : 0);
				int num4 = ((terrain.topNeighbor == null) ? edgePad : 0);
				terrainTile.paddedTerrainPixels = new RectInt
				{
					x = Mathf.Max(-num, pixelRect.x - tileOriginPixelsX - num),
					y = Mathf.Max(-num3, pixelRect.y - tileOriginPixelsY - num3),
					xMax = Mathf.Min(targetTextureWidth + num2, pixelRect.xMax - tileOriginPixelsX + num2),
					yMax = Mathf.Min(targetTextureHeight + num4, pixelRect.yMax - tileOriginPixelsY + num4)
				};
				terrainTile.paddedPCPixels = new RectInt(terrainTile.clippedPCPixels.min + (terrainTile.paddedTerrainPixels.min - terrainTile.clippedTerrainPixels.min), terrainTile.clippedPCPixels.size + (terrainTile.paddedTerrainPixels.size - terrainTile.clippedTerrainPixels.size));
				if (terrainTile.clippedTerrainPixels.width == 0 || terrainTile.clippedTerrainPixels.height == 0)
				{
					terrainTile.gatherEnable = false;
					terrainTile.scatterEnable = false;
					Debug.LogError("PaintContext.ClipTerrainTiles found 0 content rect");
				}
				return terrainTile;
			}
		}

		private class SplatmapUserData
		{
			public TerrainLayer terrainLayer;

			public int terrainLayerIndex;

			public int mapIndex;

			public int channelIndex;
		}

		[Flags]
		internal enum ToolAction
		{
			None = 0,
			PaintHeightmap = 1,
			PaintTexture = 2,
			PaintHoles = 4,
			AddTerrainLayer = 8
		}

		private struct PaintedTerrain
		{
			public Terrain terrain;

			public ToolAction action;
		}

		private List<TerrainTile> m_TerrainTiles;

		private float m_HeightWorldSpaceMin;

		private float m_HeightWorldSpaceMax;

		internal const int k_MinimumResolution = 1;

		internal const int k_MaximumResolution = 8192;

		private static List<PaintedTerrain> s_PaintedTerrain = new List<PaintedTerrain>();

		public Terrain originTerrain { get; }

		public RectInt pixelRect { get; }

		public int targetTextureWidth { get; }

		public int targetTextureHeight { get; }

		public Vector2 pixelSize { get; }

		public RenderTexture sourceRenderTexture { get; private set; }

		public RenderTexture destinationRenderTexture { get; private set; }

		public RenderTexture oldRenderTexture { get; private set; }

		public int terrainCount => m_TerrainTiles.Count;

		public float heightWorldSpaceMin => m_HeightWorldSpaceMin;

		public float heightWorldSpaceSize => m_HeightWorldSpaceMax - m_HeightWorldSpaceMin;

		public static float kNormalizedHeightScale => 0.4999771f;

		internal static event Action<ITerrainInfo, ToolAction, string> onTerrainTileBeforePaint;

		public Terrain GetTerrain(int terrainIndex)
		{
			return m_TerrainTiles[terrainIndex].terrain;
		}

		public RectInt GetClippedPixelRectInTerrainPixels(int terrainIndex)
		{
			return m_TerrainTiles[terrainIndex].clippedTerrainPixels;
		}

		public RectInt GetClippedPixelRectInRenderTexturePixels(int terrainIndex)
		{
			return m_TerrainTiles[terrainIndex].clippedPCPixels;
		}

		internal static int ClampContextResolution(int resolution)
		{
			return Mathf.Clamp(resolution, 1, 8192);
		}

		public PaintContext(Terrain terrain, RectInt pixelRect, int targetTextureWidth, int targetTextureHeight, [UnityEngine.Internal.DefaultValue("true")] bool sharedBoundaryTexel = true, [UnityEngine.Internal.DefaultValue("true")] bool fillOutsideTerrain = true)
		{
			originTerrain = terrain;
			this.pixelRect = pixelRect;
			this.targetTextureWidth = targetTextureWidth;
			this.targetTextureHeight = targetTextureHeight;
			TerrainData terrainData = terrain.terrainData;
			pixelSize = new Vector2(terrainData.size.x / ((float)targetTextureWidth - (sharedBoundaryTexel ? 1f : 0f)), terrainData.size.z / ((float)targetTextureHeight - (sharedBoundaryTexel ? 1f : 0f)));
			FindTerrainTilesUnlimited(sharedBoundaryTexel, fillOutsideTerrain);
		}

		public static PaintContext CreateFromBounds(Terrain terrain, Rect boundsInTerrainSpace, int inputTextureWidth, int inputTextureHeight, [UnityEngine.Internal.DefaultValue("0")] int extraBorderPixels = 0, [UnityEngine.Internal.DefaultValue("true")] bool sharedBoundaryTexel = true, [UnityEngine.Internal.DefaultValue("true")] bool fillOutsideTerrain = true)
		{
			return new PaintContext(terrain, TerrainPaintUtility.CalcPixelRectFromBounds(terrain, boundsInTerrainSpace, inputTextureWidth, inputTextureHeight, extraBorderPixels, sharedBoundaryTexel), inputTextureWidth, inputTextureHeight, sharedBoundaryTexel, fillOutsideTerrain);
		}

		private void FindTerrainTilesUnlimited(bool sharedBoundaryTexel, bool fillOutsideTerrain)
		{
			float minX = originTerrain.transform.position.x + pixelSize.x * (float)pixelRect.xMin;
			float minZ = originTerrain.transform.position.z + pixelSize.y * (float)pixelRect.yMin;
			float maxX = originTerrain.transform.position.x + pixelSize.x * (float)(pixelRect.xMax - 1);
			float maxZ = originTerrain.transform.position.z + pixelSize.y * (float)(pixelRect.yMax - 1);
			m_HeightWorldSpaceMin = originTerrain.GetPosition().y;
			m_HeightWorldSpaceMax = m_HeightWorldSpaceMin + originTerrain.terrainData.size.y;
			Predicate<Terrain> filter = delegate(Terrain t)
			{
				float x = t.transform.position.x;
				float z = t.transform.position.z;
				float num3 = t.transform.position.x + t.terrainData.size.x;
				float num4 = t.transform.position.z + t.terrainData.size.z;
				return x <= maxX && num3 >= minX && z <= maxZ && num4 >= minZ;
			};
			TerrainMap terrainMap = TerrainMap.CreateFromConnectedNeighbors(originTerrain, filter, fullValidation: false);
			m_TerrainTiles = new List<TerrainTile>();
			if (terrainMap == null)
			{
				return;
			}
			foreach (KeyValuePair<TerrainTileCoord, Terrain> terrainTile in terrainMap.terrainTiles)
			{
				TerrainTileCoord key = terrainTile.Key;
				Terrain value = terrainTile.Value;
				int num = key.tileX * (targetTextureWidth - (sharedBoundaryTexel ? 1 : 0));
				int num2 = key.tileZ * (targetTextureHeight - (sharedBoundaryTexel ? 1 : 0));
				RectInt other = new RectInt(num, num2, targetTextureWidth, targetTextureHeight);
				if (pixelRect.Overlaps(other))
				{
					int edgePad = (fillOutsideTerrain ? Mathf.Max(targetTextureWidth, targetTextureHeight) : 0);
					m_TerrainTiles.Add(TerrainTile.Make(value, num, num2, pixelRect, targetTextureWidth, targetTextureHeight, edgePad));
					m_HeightWorldSpaceMin = Mathf.Min(m_HeightWorldSpaceMin, value.GetPosition().y);
					m_HeightWorldSpaceMax = Mathf.Max(m_HeightWorldSpaceMax, value.GetPosition().y + value.terrainData.size.y);
				}
			}
		}

		public void CreateRenderTargets(RenderTextureFormat colorFormat)
		{
			int num = ClampContextResolution(pixelRect.width);
			int num2 = ClampContextResolution(pixelRect.height);
			if (num != pixelRect.width || num2 != pixelRect.height)
			{
				Debug.LogWarning($"\nTERRAIN EDITOR INTERNAL ERROR: An attempt to create a PaintContext with dimensions of {pixelRect.width}x{pixelRect.height} was made,\nwhereas the maximum supported resolution is {8192}. The size has been clamped to {8192}.");
			}
			sourceRenderTexture = RenderTexture.GetTemporary(num, num2, 16, colorFormat, RenderTextureReadWrite.Linear);
			destinationRenderTexture = RenderTexture.GetTemporary(num, num2, 0, colorFormat, RenderTextureReadWrite.Linear);
			sourceRenderTexture.wrapMode = TextureWrapMode.Clamp;
			sourceRenderTexture.filterMode = FilterMode.Point;
			oldRenderTexture = RenderTexture.active;
		}

		public void Cleanup(bool restoreRenderTexture = true)
		{
			if (restoreRenderTexture)
			{
				RenderTexture.active = oldRenderTexture;
			}
			RenderTexture.ReleaseTemporary(sourceRenderTexture);
			RenderTexture.ReleaseTemporary(destinationRenderTexture);
			sourceRenderTexture = null;
			destinationRenderTexture = null;
			oldRenderTexture = null;
		}

		private void GatherInternal(Func<ITerrainInfo, Texture> terrainToTexture, Color defaultColor, string operationName, Material blitMaterial = null, int blitPass = 0, Action<ITerrainInfo> beforeBlit = null, Action<ITerrainInfo> afterBlit = null)
		{
			if (blitMaterial == null)
			{
				blitMaterial = TerrainPaintUtility.GetBlitMaterial();
			}
			RenderTexture.active = sourceRenderTexture;
			GL.Clear(clearDepth: true, clearColor: true, defaultColor);
			GL.PushMatrix();
			GL.LoadPixelMatrix(0f, pixelRect.width, 0f, pixelRect.height);
			for (int i = 0; i < m_TerrainTiles.Count; i++)
			{
				TerrainTile terrainTile = m_TerrainTiles[i];
				if (!terrainTile.gatherEnable)
				{
					continue;
				}
				Texture texture = terrainToTexture(terrainTile);
				if (texture == null || !terrainTile.gatherEnable)
				{
					continue;
				}
				if (texture.width != targetTextureWidth || texture.height != targetTextureHeight)
				{
					Debug.LogWarning(operationName + " requires the same resolution texture for all Terrains - mismatched Terrains are ignored.", terrainTile.terrain);
					continue;
				}
				beforeBlit?.Invoke(terrainTile);
				if (terrainTile.gatherEnable)
				{
					FilterMode filterMode = texture.filterMode;
					texture.filterMode = FilterMode.Point;
					blitMaterial.SetTexture("_MainTex", texture);
					blitMaterial.SetPass(blitPass);
					TerrainPaintUtility.DrawQuadPadded(terrainTile.clippedPCPixels, terrainTile.paddedPCPixels, terrainTile.clippedTerrainPixels, terrainTile.paddedTerrainPixels, texture);
					texture.filterMode = filterMode;
					afterBlit?.Invoke(terrainTile);
				}
			}
			GL.PopMatrix();
			RenderTexture.active = oldRenderTexture;
		}

		private void ScatterInternal(Func<ITerrainInfo, RenderTexture> terrainToRT, string operationName, Material blitMaterial = null, int blitPass = 0, Action<ITerrainInfo> beforeBlit = null, Action<ITerrainInfo> afterBlit = null)
		{
			RenderTexture active = RenderTexture.active;
			if (blitMaterial == null)
			{
				blitMaterial = TerrainPaintUtility.GetBlitMaterial();
			}
			for (int i = 0; i < m_TerrainTiles.Count; i++)
			{
				TerrainTile terrainTile = m_TerrainTiles[i];
				if (!terrainTile.scatterEnable)
				{
					continue;
				}
				RenderTexture renderTexture = terrainToRT(terrainTile);
				if (renderTexture == null || !terrainTile.scatterEnable)
				{
					continue;
				}
				if (renderTexture.width != targetTextureWidth || renderTexture.height != targetTextureHeight)
				{
					Debug.LogWarning(operationName + " requires the same resolution for all Terrains - mismatched Terrains are ignored.", terrainTile.terrain);
					continue;
				}
				beforeBlit?.Invoke(terrainTile);
				if (terrainTile.scatterEnable)
				{
					RenderTexture.active = renderTexture;
					GL.PushMatrix();
					GL.LoadPixelMatrix(0f, renderTexture.width, 0f, renderTexture.height);
					FilterMode filterMode = destinationRenderTexture.filterMode;
					destinationRenderTexture.filterMode = FilterMode.Point;
					blitMaterial.SetTexture("_MainTex", destinationRenderTexture);
					blitMaterial.SetPass(blitPass);
					TerrainPaintUtility.DrawQuad(terrainTile.clippedTerrainPixels, terrainTile.clippedPCPixels, destinationRenderTexture);
					destinationRenderTexture.filterMode = filterMode;
					GL.PopMatrix();
					afterBlit?.Invoke(terrainTile);
				}
			}
			RenderTexture.active = active;
		}

		public void Gather(Func<ITerrainInfo, Texture> terrainSource, Color defaultColor, Material blitMaterial = null, int blitPass = 0, Action<ITerrainInfo> beforeBlit = null, Action<ITerrainInfo> afterBlit = null)
		{
			if (terrainSource != null)
			{
				GatherInternal(terrainSource, defaultColor, "PaintContext.Gather", blitMaterial, blitPass, beforeBlit, afterBlit);
			}
		}

		public void Scatter(Func<ITerrainInfo, RenderTexture> terrainDest, Material blitMaterial = null, int blitPass = 0, Action<ITerrainInfo> beforeBlit = null, Action<ITerrainInfo> afterBlit = null)
		{
			if (terrainDest != null)
			{
				ScatterInternal(terrainDest, "PaintContext.Scatter", blitMaterial, blitPass, beforeBlit, afterBlit);
			}
		}

		public void GatherHeightmap()
		{
			Material blitMaterial = TerrainPaintUtility.GetHeightBlitMaterial();
			blitMaterial.SetFloat("_Height_Offset", 0f);
			blitMaterial.SetFloat("_Height_Scale", 1f);
			GatherInternal((ITerrainInfo t) => t.terrain.terrainData.heightmapTexture, new Color(0f, 0f, 0f, 0f), "PaintContext.GatherHeightmap", blitMaterial, 0, delegate(ITerrainInfo t)
			{
				blitMaterial.SetFloat("_Height_Offset", (t.terrain.GetPosition().y - heightWorldSpaceMin) / heightWorldSpaceSize * kNormalizedHeightScale);
				blitMaterial.SetFloat("_Height_Scale", t.terrain.terrainData.size.y / heightWorldSpaceSize);
			});
		}

		public void ScatterHeightmap(string editorUndoName)
		{
			Material blitMaterial = TerrainPaintUtility.GetHeightBlitMaterial();
			blitMaterial.SetFloat("_Height_Offset", 0f);
			blitMaterial.SetFloat("_Height_Scale", 1f);
			ScatterInternal((ITerrainInfo t) => t.terrain.terrainData.heightmapTexture, "PaintContext.ScatterHeightmap", blitMaterial, 0, delegate(ITerrainInfo t)
			{
				PaintContext.onTerrainTileBeforePaint?.Invoke(t, ToolAction.PaintHeightmap, editorUndoName);
				blitMaterial.SetFloat("_Height_Offset", (heightWorldSpaceMin - t.terrain.GetPosition().y) / t.terrain.terrainData.size.y * kNormalizedHeightScale);
				blitMaterial.SetFloat("_Height_Scale", heightWorldSpaceSize / t.terrain.terrainData.size.y);
			}, delegate(ITerrainInfo t)
			{
				TerrainHeightmapSyncControl syncControl = ((!t.terrain.drawInstanced) ? TerrainHeightmapSyncControl.HeightAndLod : TerrainHeightmapSyncControl.None);
				t.terrain.terrainData.DirtyHeightmapRegion(t.clippedTerrainPixels, syncControl);
				OnTerrainPainted(t, ToolAction.PaintHeightmap);
			});
		}

		public void GatherHoles()
		{
			GatherInternal((ITerrainInfo t) => t.terrain.terrainData.holesTexture, new Color(0f, 0f, 0f, 0f), "PaintContext.GatherHoles");
		}

		public void ScatterHoles(string editorUndoName)
		{
			ScatterInternal(delegate(ITerrainInfo t)
			{
				PaintContext.onTerrainTileBeforePaint?.Invoke(t, ToolAction.PaintHoles, editorUndoName);
				t.terrain.terrainData.CopyActiveRenderTextureToTexture(TerrainData.HolesTextureName, 0, t.clippedPCPixels, t.clippedTerrainPixels.min, allowDelayedCPUSync: true);
				OnTerrainPainted(t, ToolAction.PaintHoles);
				return (RenderTexture)null;
			}, "PaintContext.ScatterHoles");
		}

		public void GatherNormals()
		{
			GatherInternal((ITerrainInfo t) => t.terrain.normalmapTexture, new Color(0.5f, 0.5f, 0.5f, 0.5f), "PaintContext.GatherNormals");
		}

		private SplatmapUserData GetTerrainLayerUserData(ITerrainInfo context, TerrainLayer terrainLayer = null, bool addLayerIfDoesntExist = false)
		{
			SplatmapUserData splatmapUserData = context.userData as SplatmapUserData;
			if (splatmapUserData != null)
			{
				if (terrainLayer == null || terrainLayer == splatmapUserData.terrainLayer)
				{
					return splatmapUserData;
				}
				splatmapUserData = null;
			}
			if (splatmapUserData == null)
			{
				int num = -1;
				if (terrainLayer != null)
				{
					num = TerrainPaintUtility.FindTerrainLayerIndex(context.terrain, terrainLayer);
					if (num == -1 && addLayerIfDoesntExist)
					{
						PaintContext.onTerrainTileBeforePaint?.Invoke(context, ToolAction.AddTerrainLayer, "Adding Terrain Layer");
						num = TerrainPaintUtility.AddTerrainLayer(context.terrain, terrainLayer);
					}
				}
				if (num != -1)
				{
					splatmapUserData = new SplatmapUserData();
					splatmapUserData.terrainLayer = terrainLayer;
					splatmapUserData.terrainLayerIndex = num;
					splatmapUserData.mapIndex = num >> 2;
					splatmapUserData.channelIndex = num & 3;
				}
				context.userData = splatmapUserData;
			}
			return splatmapUserData;
		}

		public void GatherAlphamap(TerrainLayer inputLayer, bool addLayerIfDoesntExist = true)
		{
			if (inputLayer == null)
			{
				return;
			}
			Material copyTerrainLayerMaterial = TerrainPaintUtility.GetCopyTerrainLayerMaterial();
			Vector4[] layerMasks = new Vector4[4]
			{
				new Vector4(1f, 0f, 0f, 0f),
				new Vector4(0f, 1f, 0f, 0f),
				new Vector4(0f, 0f, 1f, 0f),
				new Vector4(0f, 0f, 0f, 1f)
			};
			GatherInternal(delegate(ITerrainInfo t)
			{
				SplatmapUserData terrainLayerUserData = GetTerrainLayerUserData(t, inputLayer, addLayerIfDoesntExist);
				return (terrainLayerUserData != null) ? TerrainPaintUtility.GetTerrainAlphaMapChecked(t.terrain, terrainLayerUserData.mapIndex) : null;
			}, new Color(0f, 0f, 0f, 0f), "PaintContext.GatherAlphamap", copyTerrainLayerMaterial, 0, delegate(ITerrainInfo t)
			{
				SplatmapUserData terrainLayerUserData = GetTerrainLayerUserData(t);
				if (terrainLayerUserData != null)
				{
					copyTerrainLayerMaterial.SetVector("_LayerMask", layerMasks[terrainLayerUserData.channelIndex]);
				}
			});
		}

		public void ScatterAlphamap(string editorUndoName)
		{
			Vector4[] layerMasks = new Vector4[4]
			{
				new Vector4(1f, 0f, 0f, 0f),
				new Vector4(0f, 1f, 0f, 0f),
				new Vector4(0f, 0f, 1f, 0f),
				new Vector4(0f, 0f, 0f, 1f)
			};
			Material copyTerrainLayerMaterial = TerrainPaintUtility.GetCopyTerrainLayerMaterial();
			RenderTextureDescriptor desc = new RenderTextureDescriptor(destinationRenderTexture.width, destinationRenderTexture.height, GraphicsFormat.R8G8B8A8_UNorm, GraphicsFormat.None);
			desc.sRGB = false;
			desc.useMipMap = false;
			desc.autoGenerateMips = false;
			RenderTexture tempTarget = RenderTexture.GetTemporary(desc);
			ScatterInternal(delegate(ITerrainInfo t)
			{
				SplatmapUserData terrainLayerUserData = GetTerrainLayerUserData(t);
				if (terrainLayerUserData != null)
				{
					PaintContext.onTerrainTileBeforePaint?.Invoke(t, ToolAction.PaintTexture, editorUndoName);
					int mapIndex = terrainLayerUserData.mapIndex;
					int channelIndex = terrainLayerUserData.channelIndex;
					Texture2D value = t.terrain.terrainData.alphamapTextures[mapIndex];
					destinationRenderTexture.filterMode = FilterMode.Point;
					sourceRenderTexture.filterMode = FilterMode.Point;
					for (int i = 0; i <= t.terrain.terrainData.alphamapTextureCount; i++)
					{
						if (i != mapIndex)
						{
							int num = ((i == t.terrain.terrainData.alphamapTextureCount) ? mapIndex : i);
							Texture2D texture2D = t.terrain.terrainData.alphamapTextures[num];
							if (texture2D.width != targetTextureWidth || texture2D.height != targetTextureHeight)
							{
								Debug.LogWarning("PaintContext alphamap operations must use the same resolution for all Terrains - mismatched Terrains are ignored.", t.terrain);
							}
							else
							{
								RenderTexture.active = tempTarget;
								GL.PushMatrix();
								GL.LoadPixelMatrix(0f, tempTarget.width, 0f, tempTarget.height);
								copyTerrainLayerMaterial.SetTexture("_MainTex", destinationRenderTexture);
								copyTerrainLayerMaterial.SetTexture("_OldAlphaMapTexture", sourceRenderTexture);
								copyTerrainLayerMaterial.SetTexture("_OriginalTargetAlphaMap", value);
								copyTerrainLayerMaterial.SetTexture("_AlphaMapTexture", texture2D);
								copyTerrainLayerMaterial.SetVector("_LayerMask", (num == mapIndex) ? layerMasks[channelIndex] : Vector4.zero);
								copyTerrainLayerMaterial.SetVector("_OriginalTargetAlphaMask", layerMasks[channelIndex]);
								copyTerrainLayerMaterial.SetPass(1);
								TerrainPaintUtility.DrawQuad2(t.clippedPCPixels, t.clippedPCPixels, destinationRenderTexture, t.clippedTerrainPixels, texture2D);
								GL.PopMatrix();
								t.terrain.terrainData.CopyActiveRenderTextureToTexture(TerrainData.AlphamapTextureName, num, t.clippedPCPixels, t.clippedTerrainPixels.min, allowDelayedCPUSync: true);
							}
						}
					}
					RenderTexture.active = null;
					OnTerrainPainted(t, ToolAction.PaintTexture);
				}
				return (RenderTexture)null;
			}, "PaintContext.ScatterAlphamap", copyTerrainLayerMaterial);
			RenderTexture.ReleaseTemporary(tempTarget);
		}

		private static void OnTerrainPainted(ITerrainInfo tile, ToolAction action)
		{
			for (int i = 0; i < s_PaintedTerrain.Count; i++)
			{
				if (tile.terrain == s_PaintedTerrain[i].terrain)
				{
					PaintedTerrain value = s_PaintedTerrain[i];
					value.action |= action;
					s_PaintedTerrain[i] = value;
					return;
				}
			}
			s_PaintedTerrain.Add(new PaintedTerrain
			{
				terrain = tile.terrain,
				action = action
			});
		}

		public static void ApplyDelayedActions()
		{
			for (int i = 0; i < s_PaintedTerrain.Count; i++)
			{
				PaintedTerrain paintedTerrain = s_PaintedTerrain[i];
				TerrainData terrainData = paintedTerrain.terrain.terrainData;
				if (!(terrainData == null))
				{
					if ((paintedTerrain.action & ToolAction.PaintHeightmap) != ToolAction.None)
					{
						terrainData.SyncHeightmap();
					}
					if ((paintedTerrain.action & ToolAction.PaintHoles) != ToolAction.None)
					{
						terrainData.SyncTexture(TerrainData.HolesTextureName);
					}
					if ((paintedTerrain.action & ToolAction.PaintTexture) != ToolAction.None)
					{
						terrainData.SetBaseMapDirty();
						terrainData.SyncTexture(TerrainData.AlphamapTextureName);
					}
					paintedTerrain.terrain.editorRenderFlags = TerrainRenderFlags.all;
				}
			}
			s_PaintedTerrain.Clear();
		}
	}
	public enum TerrainBuiltinPaintMaterialPasses
	{
		RaiseLowerHeight,
		StampHeight,
		SetHeights,
		SmoothHeights,
		PaintTexture,
		PaintHoles
	}
	[MovedFrom("UnityEngine.Experimental.TerrainAPI")]
	public static class TerrainPaintUtility
	{
		private static Material s_BuiltinPaintMaterial;

		private static Material s_BlitMaterial;

		private static Material s_HeightBlitMaterial;

		private static Material s_CopyTerrainLayerMaterial;

		internal static bool paintTextureUsesCopyTexture => (SystemInfo.copyTextureSupport & (CopyTextureSupport.TextureToRT | CopyTextureSupport.RTToTexture)) == (CopyTextureSupport.TextureToRT | CopyTextureSupport.RTToTexture);

		public static Material GetBuiltinPaintMaterial()
		{
			if (s_BuiltinPaintMaterial == null)
			{
				s_BuiltinPaintMaterial = new Material(Shader.Find("Hidden/TerrainEngine/PaintHeight"));
			}
			return s_BuiltinPaintMaterial;
		}

		public static void GetBrushWorldSizeLimits(out float minBrushWorldSize, out float maxBrushWorldSize, float terrainTileWorldSize, int terrainTileTextureResolutionPixels, int minBrushResolutionPixels = 1, int maxBrushResolutionPixels = 8192)
		{
			if (terrainTileTextureResolutionPixels <= 0)
			{
				minBrushWorldSize = terrainTileWorldSize;
				maxBrushWorldSize = terrainTileWorldSize;
				return;
			}
			float num = terrainTileWorldSize / (float)terrainTileTextureResolutionPixels;
			minBrushWorldSize = (float)minBrushResolutionPixels * num;
			float num2 = Mathf.Min(maxBrushResolutionPixels, SystemInfo.maxTextureSize);
			maxBrushWorldSize = num2 * num;
		}

		public static BrushTransform CalculateBrushTransform(Terrain terrain, Vector2 brushCenterTerrainUV, float brushSize, float brushRotationDegrees)
		{
			float f = brushRotationDegrees * (MathF.PI / 180f);
			float num = Mathf.Cos(f);
			float num2 = Mathf.Sin(f);
			Vector2 vector = new Vector2(num, 0f - num2) * brushSize;
			Vector2 vector2 = new Vector2(num2, num) * brushSize;
			Vector3 size = terrain.terrainData.size;
			Vector2 vector3 = brushCenterTerrainUV * new Vector2(size.x, size.z);
			Vector2 brushOrigin = vector3 - 0.5f * vector - 0.5f * vector2;
			return new BrushTransform(brushOrigin, vector, vector2);
		}

		public static void BuildTransformPaintContextUVToPaintContextUV(PaintContext src, PaintContext dst, out Vector4 scaleOffset)
		{
			float num = ((float)src.pixelRect.xMin - 0.5f) * src.pixelSize.x;
			float num2 = ((float)src.pixelRect.yMin - 0.5f) * src.pixelSize.y;
			float num3 = (float)src.pixelRect.width * src.pixelSize.x;
			float num4 = (float)src.pixelRect.height * src.pixelSize.y;
			float num5 = ((float)dst.pixelRect.xMin - 0.5f) * dst.pixelSize.x;
			float num6 = ((float)dst.pixelRect.yMin - 0.5f) * dst.pixelSize.y;
			float num7 = (float)dst.pixelRect.width * dst.pixelSize.x;
			float num8 = (float)dst.pixelRect.height * dst.pixelSize.y;
			scaleOffset = new Vector4(num3 / num7, num4 / num8, (num - num5) / num7, (num2 - num6) / num8);
		}

		public static void SetupTerrainToolMaterialProperties(PaintContext paintContext, in BrushTransform brushXform, Material material)
		{
			float num = ((float)paintContext.pixelRect.xMin - 0.5f) * paintContext.pixelSize.x;
			float num2 = ((float)paintContext.pixelRect.yMin - 0.5f) * paintContext.pixelSize.y;
			float num3 = (float)paintContext.pixelRect.width * paintContext.pixelSize.x;
			float num4 = (float)paintContext.pixelRect.height * paintContext.pixelSize.y;
			Vector2 vector = num3 * brushXform.targetX;
			Vector2 vector2 = num4 * brushXform.targetY;
			Vector2 vector3 = brushXform.targetOrigin + num * brushXform.targetX + num2 * brushXform.targetY;
			material.SetVector("_PCUVToBrushUVScales", new Vector4(vector.x, vector.y, vector2.x, vector2.y));
			material.SetVector("_PCUVToBrushUVOffset", new Vector4(vector3.x, vector3.y, 0f, 0f));
		}

		internal static PaintContext InitializePaintContext(Terrain terrain, int targetWidth, int targetHeight, RenderTextureFormat pcFormat, Rect boundsInTerrainSpace, [UnityEngine.Internal.DefaultValue("0")] int extraBorderPixels = 0, [UnityEngine.Internal.DefaultValue("true")] bool sharedBoundaryTexel = true, [UnityEngine.Internal.DefaultValue("true")] bool fillOutsideTerrain = true)
		{
			PaintContext paintContext = PaintContext.CreateFromBounds(terrain, boundsInTerrainSpace, targetWidth, targetHeight, extraBorderPixels, sharedBoundaryTexel, fillOutsideTerrain);
			paintContext.CreateRenderTargets(pcFormat);
			return paintContext;
		}

		public static void ReleaseContextResources(PaintContext ctx)
		{
			ctx.Cleanup();
		}

		public static PaintContext BeginPaintHeightmap(Terrain terrain, Rect boundsInTerrainSpace, [UnityEngine.Internal.DefaultValue("0")] int extraBorderPixels = 0, [UnityEngine.Internal.DefaultValue("true")] bool fillOutsideTerrain = true)
		{
			int heightmapResolution = terrain.terrainData.heightmapResolution;
			PaintContext paintContext = InitializePaintContext(terrain, heightmapResolution, heightmapResolution, Terrain.heightmapRenderTextureFormat, boundsInTerrainSpace, extraBorderPixels, sharedBoundaryTexel: true, fillOutsideTerrain);
			paintContext.GatherHeightmap();
			return paintContext;
		}

		public static void EndPaintHeightmap(PaintContext ctx, string editorUndoName)
		{
			ctx.ScatterHeightmap(editorUndoName);
			ctx.Cleanup();
		}

		public static PaintContext BeginPaintHoles(Terrain terrain, Rect boundsInTerrainSpace, [UnityEngine.Internal.DefaultValue("0")] int extraBorderPixels = 0, [UnityEngine.Internal.DefaultValue("true")] bool fillOutsideTerrain = true)
		{
			int holesResolution = terrain.terrainData.holesResolution;
			PaintContext paintContext = InitializePaintContext(terrain, holesResolution, holesResolution, Terrain.holesRenderTextureFormat, boundsInTerrainSpace, extraBorderPixels, sharedBoundaryTexel: false, fillOutsideTerrain);
			paintContext.GatherHoles();
			return paintContext;
		}

		public static void EndPaintHoles(PaintContext ctx, string editorUndoName)
		{
			ctx.ScatterHoles(editorUndoName);
			ctx.Cleanup();
		}

		public static PaintContext CollectNormals(Terrain terrain, Rect boundsInTerrainSpace, [UnityEngine.Internal.DefaultValue("0")] int extraBorderPixels = 0, [UnityEngine.Internal.DefaultValue("true")] bool fillOutsideTerrain = true)
		{
			int heightmapResolution = terrain.terrainData.heightmapResolution;
			PaintContext paintContext = InitializePaintContext(terrain, heightmapResolution, heightmapResolution, Terrain.normalmapRenderTextureFormat, boundsInTerrainSpace, extraBorderPixels, sharedBoundaryTexel: true, fillOutsideTerrain);
			paintContext.GatherNormals();
			return paintContext;
		}

		public static PaintContext BeginPaintTexture(Terrain terrain, Rect boundsInTerrainSpace, TerrainLayer inputLayer, [UnityEngine.Internal.DefaultValue("0")] int extraBorderPixels = 0, [UnityEngine.Internal.DefaultValue("true")] bool fillOutsideTerrain = true)
		{
			if (inputLayer == null)
			{
				return null;
			}
			int alphamapResolution = terrain.terrainData.alphamapResolution;
			PaintContext paintContext = InitializePaintContext(terrain, alphamapResolution, alphamapResolution, RenderTextureFormat.R8, boundsInTerrainSpace, extraBorderPixels, sharedBoundaryTexel: true, fillOutsideTerrain);
			paintContext.GatherAlphamap(inputLayer);
			return paintContext;
		}

		public static void EndPaintTexture(PaintContext ctx, string editorUndoName)
		{
			ctx.ScatterAlphamap(editorUndoName);
			ctx.Cleanup();
		}

		public static Material GetBlitMaterial()
		{
			if (!s_BlitMaterial)
			{
				s_BlitMaterial = new Material(Shader.Find("Hidden/TerrainEngine/TerrainBlitCopyZWrite"));
			}
			return s_BlitMaterial;
		}

		public static Material GetHeightBlitMaterial()
		{
			if (!s_HeightBlitMaterial)
			{
				s_HeightBlitMaterial = new Material(Shader.Find("Hidden/TerrainEngine/HeightBlitCopy"));
			}
			return s_HeightBlitMaterial;
		}

		public static Material GetCopyTerrainLayerMaterial()
		{
			if (!s_CopyTerrainLayerMaterial)
			{
				s_CopyTerrainLayerMaterial = new Material(Shader.Find("Hidden/TerrainEngine/TerrainLayerUtils"));
			}
			return s_CopyTerrainLayerMaterial;
		}

		internal static void DrawQuad(RectInt destinationPixels, RectInt sourcePixels, Texture sourceTexture)
		{
			DrawQuad2(destinationPixels, sourcePixels, sourceTexture, sourcePixels, sourceTexture);
		}

		internal static void DrawQuad2(RectInt destinationPixels, RectInt sourcePixels, Texture sourceTexture, RectInt sourcePixels2, Texture sourceTexture2)
		{
			if (destinationPixels.width > 0 && destinationPixels.height > 0)
			{
				Rect rect = new Rect((float)sourcePixels.x / (float)sourceTexture.width, (float)sourcePixels.y / (float)sourceTexture.height, (float)sourcePixels.width / (float)sourceTexture.width, (float)sourcePixels.height / (float)sourceTexture.height);
				Rect rect2 = new Rect((float)sourcePixels2.x / (float)sourceTexture2.width, (float)sourcePixels2.y / (float)sourceTexture2.height, (float)sourcePixels2.width / (float)sourceTexture2.width, (float)sourcePixels2.height / (float)sourceTexture2.height);
				GL.Begin(7);
				GL.Color(new Color(1f, 1f, 1f, 1f));
				GL.MultiTexCoord2(0, rect.x, rect.y);
				GL.MultiTexCoord2(1, rect2.x, rect2.y);
				GL.Vertex3(destinationPixels.x, destinationPixels.y, 0f);
				GL.MultiTexCoord2(0, rect.x, rect.yMax);
				GL.MultiTexCoord2(1, rect2.x, rect2.yMax);
				GL.Vertex3(destinationPixels.x, destinationPixels.yMax, 0f);
				GL.MultiTexCoord2(0, rect.xMax, rect.yMax);
				GL.MultiTexCoord2(1, rect2.xMax, rect2.yMax);
				GL.Vertex3(destinationPixels.xMax, destinationPixels.yMax, 0f);
				GL.MultiTexCoord2(0, rect.xMax, rect.y);
				GL.MultiTexCoord2(1, rect2.xMax, rect2.y);
				GL.Vertex3(destinationPixels.xMax, destinationPixels.y, 0f);
				GL.End();
			}
		}

		internal static void DrawQuadPadded(RectInt destinationPixels, RectInt destinationPixelsPadded, RectInt sourcePixels, RectInt sourcePixelsPadded, Texture sourceTexture)
		{
			GL.Begin(7);
			GL.Color(new Color(1f, 1f, 1f, 1f));
			for (int i = 0; i < 3; i++)
			{
				Vector2Int vector2Int;
				Vector2Int vector2Int2;
				Vector2 vector;
				switch (i)
				{
				case 0:
					vector2Int = new Vector2Int(sourcePixelsPadded.yMin, sourcePixels.yMin);
					vector2Int2 = new Vector2Int(destinationPixelsPadded.yMin, destinationPixels.yMin);
					vector = new Vector2(-1f, 0f);
					break;
				case 1:
					vector2Int = new Vector2Int(sourcePixels.yMin, sourcePixels.yMax);
					vector2Int2 = new Vector2Int(destinationPixels.yMin, destinationPixels.yMax);
					vector = new Vector2(0f, 0f);
					break;
				default:
					vector2Int = new Vector2Int(sourcePixels.yMax, sourcePixelsPadded.yMax);
					vector2Int2 = new Vector2Int(destinationPixels.yMax, destinationPixelsPadded.yMax);
					vector = new Vector2(0f, -1f);
					break;
				}
				if (vector2Int[0] >= vector2Int[1])
				{
					continue;
				}
				for (int j = 0; j < 3; j++)
				{
					Vector2Int vector2Int3;
					Vector2Int vector2Int4;
					Vector2 vector2;
					switch (j)
					{
					case 0:
						vector2Int3 = new Vector2Int(sourcePixelsPadded.xMin, sourcePixels.xMin);
						vector2Int4 = new Vector2Int(destinationPixelsPadded.xMin, destinationPixels.xMin);
						vector2 = new Vector2(-1f, 0f);
						break;
					case 1:
						vector2Int3 = new Vector2Int(sourcePixels.xMin, sourcePixels.xMax);
						vector2Int4 = new Vector2Int(destinationPixels.xMin, destinationPixels.xMax);
						vector2 = new Vector2(0f, 0f);
						break;
					default:
						vector2Int3 = new Vector2Int(sourcePixels.xMax, sourcePixelsPadded.xMax);
						vector2Int4 = new Vector2Int(destinationPixels.xMax, destinationPixelsPadded.xMax);
						vector2 = new Vector2(0f, -1f);
						break;
					}
					if (vector2Int3[0] < vector2Int3[1])
					{
						Rect rect = new Rect((float)vector2Int3[0] / (float)sourceTexture.width, (float)vector2Int[0] / (float)sourceTexture.height, (float)(vector2Int3[1] - vector2Int3[0]) / (float)sourceTexture.width, (float)(vector2Int[1] - vector2Int[0]) / (float)sourceTexture.height);
						GL.TexCoord2(rect.x, rect.y);
						GL.Vertex3(vector2Int4[0], vector2Int2[0], 0.5f * (vector2[0] + vector[0]));
						GL.TexCoord2(rect.x, rect.yMax);
						GL.Vertex3(vector2Int4[0], vector2Int2[1], 0.5f * (vector2[0] + vector[1]));
						GL.TexCoord2(rect.xMax, rect.yMax);
						GL.Vertex3(vector2Int4[1], vector2Int2[1], 0.5f * (vector2[1] + vector[1]));
						GL.TexCoord2(rect.xMax, rect.y);
						GL.Vertex3(vector2Int4[1], vector2Int2[0], 0.5f * (vector2[1] + vector[0]));
					}
				}
			}
			GL.End();
		}

		internal static RectInt CalcPixelRectFromBounds(Terrain terrain, Rect boundsInTerrainSpace, int textureWidth, int textureHeight, int extraBorderPixels, bool sharedBoundaryTexel)
		{
			float num = ((float)textureWidth - (sharedBoundaryTexel ? 1f : 0f)) / terrain.terrainData.size.x;
			float num2 = ((float)textureHeight - (sharedBoundaryTexel ? 1f : 0f)) / terrain.terrainData.size.z;
			int num3 = Mathf.FloorToInt(boundsInTerrainSpace.xMin * num) - extraBorderPixels;
			int num4 = Mathf.FloorToInt(boundsInTerrainSpace.yMin * num2) - extraBorderPixels;
			int num5 = Mathf.CeilToInt(boundsInTerrainSpace.xMax * num) + extraBorderPixels;
			int num6 = Mathf.CeilToInt(boundsInTerrainSpace.yMax * num2) + extraBorderPixels;
			int width = PaintContext.ClampContextResolution(num5 - num3 + 1);
			int height = PaintContext.ClampContextResolution(num6 - num4 + 1);
			return new RectInt(num3, num4, width, height);
		}

		public static Texture2D GetTerrainAlphaMapChecked(Terrain terrain, int mapIndex)
		{
			if (mapIndex >= terrain.terrainData.alphamapTextureCount)
			{
				throw new ArgumentException("Trying to access out-of-bounds terrain alphamap information.");
			}
			return terrain.terrainData.GetAlphamapTexture(mapIndex);
		}

		public static int FindTerrainLayerIndex(Terrain terrain, TerrainLayer inputLayer)
		{
			TerrainLayer[] terrainLayers = terrain.terrainData.terrainLayers;
			for (int i = 0; i < terrainLayers.Length; i++)
			{
				if (terrainLayers[i] == inputLayer)
				{
					return i;
				}
			}
			return -1;
		}

		internal static int AddTerrainLayer(Terrain terrain, TerrainLayer inputLayer)
		{
			TerrainLayer[] terrainLayers = terrain.terrainData.terrainLayers;
			int num = terrainLayers.Length;
			TerrainLayer[] array = new TerrainLayer[num + 1];
			Array.Copy(terrainLayers, 0, array, 0, num);
			array[num] = inputLayer;
			terrain.terrainData.terrainLayers = array;
			return num;
		}
	}
}
