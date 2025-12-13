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
using UnityEngine.Rendering;
using UnityEngine.Scripting;

[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestTextureModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityTestProtocolModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityCurlModule")]
[assembly: InternalsVisibleTo("UnityEngine.UmbraModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestWWWModule")]
[assembly: InternalsVisibleTo("UnityEngine.TilemapModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubsystemsModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubstanceModule")]
[assembly: InternalsVisibleTo("UnityEngine.StreamingModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteShapeModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteMaskModule")]
[assembly: InternalsVisibleTo("UnityEngine.ScreenCaptureModule")]
[assembly: InternalsVisibleTo("UnityEngine.RuntimeInitializeOnLoadManagerInitializerModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainPhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.PropertiesModule")]
[assembly: InternalsVisibleTo("UnityEngine.VFXModule")]
[assembly: InternalsVisibleTo("UnityEngine.VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.Purchasing")]
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommon")]
[assembly: InternalsVisibleTo("UnityEngine.Analytics")]
[assembly: InternalsVisibleTo("Unity.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud.Service")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud")]
[assembly: InternalsVisibleTo("UnityEngine.Networking")]
[assembly: InternalsVisibleTo("UnityEngine.XRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS5VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS4VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS4Module")]
[assembly: InternalsVisibleTo("UnityEngine.XboxOneModule")]
[assembly: InternalsVisibleTo("UnityEngine.SwitchModule")]
[assembly: InternalsVisibleTo("UnityEngine.WindModule")]
[assembly: InternalsVisibleTo("UnityEngine.VirtualTexturingModule")]
[assembly: InternalsVisibleTo("UnityEngine.VideoModule")]
[assembly: InternalsVisibleTo("UnityEngine.VehiclesModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS5Module")]
[assembly: InternalsVisibleTo("UnityEngine.TestRunner")]
[assembly: InternalsVisibleTo("UnityEngine.ProfilerModule")]
[assembly: InternalsVisibleTo("UnityEngine.PerformanceReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClothModule")]
[assembly: InternalsVisibleTo("UnityEngine.AudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.HotReloadModule")]
[assembly: InternalsVisibleTo("UnityEngine.AnimationModule")]
[assembly: InternalsVisibleTo("UnityEngine.AndroidJNIModule")]
[assembly: InternalsVisibleTo("UnityEngine.AccessibilityModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterRendererModule")]
[assembly: InternalsVisibleTo("UnityEngine.ARModule")]
[assembly: InternalsVisibleTo("UnityEngine.JSONSerializeModule")]
[assembly: InternalsVisibleTo("UnityEngine.PhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.AIModule")]
[assembly: InternalsVisibleTo("UnityEngine.CoreModule")]
[assembly: InternalsVisibleTo("UnityEngine.SharedInternalsModule")]
[assembly: InternalsVisibleTo("UnityEngine")]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: InternalsVisibleTo("UnityEngine.InputModule")]
[assembly: InternalsVisibleTo("UnityEngine.Physics2DModule")]
[assembly: InternalsVisibleTo("UnityEngine.ContentLoadModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommonModule")]
[assembly: InternalsVisibleTo("UnityEngine.ParticleSystemModule")]
[assembly: InternalsVisibleTo("UnityEngine.NVIDIAModule")]
[assembly: InternalsVisibleTo("UnityEngine.LocalizationModule")]
[assembly: InternalsVisibleTo("UnityEngine.IMGUIModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreTextEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreFontEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputLegacyModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextRenderingModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityConnectModule")]
[assembly: InternalsVisibleTo("UnityEngine.GridModule")]
[assembly: InternalsVisibleTo("UnityEngine.ImageConversionModule")]
[assembly: InternalsVisibleTo("UnityEngine.GIModule")]
[assembly: InternalsVisibleTo("UnityEngine.DirectorModule")]
[assembly: InternalsVisibleTo("UnityEngine.DSPGraphModule")]
[assembly: InternalsVisibleTo("UnityEngine.CrashReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestModule")]
[assembly: InternalsVisibleTo("UnityEngine.TLSModule")]
[assembly: InternalsVisibleTo("UnityEngine.GameCenterModule")]
[assembly: InternalsVisibleTo("Unity.Automation")]
[assembly: InternalsVisibleTo("Unity.Burst")]
[assembly: InternalsVisibleTo("Unity.Burst.Editor")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.001")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.024")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.023")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.022")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.021")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.020")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.019")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.018")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.017")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.015")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.014")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.013")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.012")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.011")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.010")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.009")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.008")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.016")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.007")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.005")]
[assembly: InternalsVisibleTo("Unity.Testing.VisualEffectGraph.EditorTests")]
[assembly: InternalsVisibleTo("Unity.Testing.VisualEffectGraph.Editor-testable")]
[assembly: InternalsVisibleTo("Unity.Testing.VisualEffectGraph.Editor")]
[assembly: InternalsVisibleTo("Unity.Testing.VisualEffectGraph.Tests-testable")]
[assembly: InternalsVisibleTo("Unity.Testing.VisualEffectGraph.Tests")]
[assembly: InternalsVisibleTo("Unity.Testing.VisualEffectGraph-testable")]
[assembly: InternalsVisibleTo("Unity.Testing.VisualEffectGraph")]
[assembly: InternalsVisibleTo("Unity.Testing.VisualEffectGraph.PerformanceEditorTests-testable")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.004")]
[assembly: InternalsVisibleTo("Unity.Testing.VisualEffectGraph.PerformanceEditorTests")]
[assembly: InternalsVisibleTo("Unity.Testing.VisualEffectGraph.PerformanceRuntimeTests")]
[assembly: InternalsVisibleTo("Unity.VisualEffectGraph.Runtime")]
[assembly: InternalsVisibleTo("Unity.VisualEffectGraph.EditorTests-testable")]
[assembly: InternalsVisibleTo("Unity.VisualEffectGraph.EditorTests")]
[assembly: InternalsVisibleTo("Unity.VisualEffectGraph.Editor-testable")]
[assembly: InternalsVisibleTo("Unity.VisualEffectGraph.Editor")]
[assembly: UnityEngineModuleAssembly]
[assembly: InternalsVisibleTo("Unity.Subsystem.Registration")]
[assembly: InternalsVisibleTo("Unity.Testing.VisualEffectGraph.PerformanceRuntimeTests-testable")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.006")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.EditorTests")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.Editor")]
[assembly: InternalsVisibleTo("Unity.WindowsMRAutomation")]
[assembly: InternalsVisibleTo("GoogleAR.UnityNative")]
[assembly: InternalsVisibleTo("UnityEngine.SpatialTracking")]
[assembly: InternalsVisibleTo("Assembly-CSharp-firstpass-testable")]
[assembly: InternalsVisibleTo("Assembly-CSharp-testable")]
[assembly: InternalsVisibleTo("Unity.Timeline")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.Editor")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.AllIn1Runner")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Framework")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Timeline")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.UnityAnalytics")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests")]
[assembly: InternalsVisibleTo("Unity.DeploymentTests.Services")]
[assembly: InternalsVisibleTo("Unity.PerformanceTests.RuntimeTestRunner.Tests")]
[assembly: InternalsVisibleTo("UnityEditor.UIBuilderModule")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.EditorTests")]
[assembly: InternalsVisibleTo("Unity.UIElements")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.001")]
[assembly: InternalsVisibleTo("UnityEngine.Core.Runtime.Tests")]
[assembly: InternalsVisibleTo("Unity.Core")]
[assembly: InternalsVisibleTo("Unity.Runtime")]
[assembly: InternalsVisibleTo("Unity.Collections")]
[assembly: InternalsVisibleTo("Unity.Entities.Tests")]
[assembly: InternalsVisibleTo("Unity.Entities")]
[assembly: InternalsVisibleTo("Unity.Logging")]
[assembly: InternalsVisibleTo("Unity.Services.QoS")]
[assembly: InternalsVisibleTo("Unity.ucg.QoS")]
[assembly: InternalsVisibleTo("Unity.Networking.Transport")]
[assembly: InternalsVisibleTo("UnityEngine.UI")]
[assembly: InternalsVisibleTo("Unity.UIElements.EditorTests")]
[assembly: InternalsVisibleTo("UnityEngine.UIElements.Tests")]
[assembly: InternalsVisibleTo("Unity.UIElements.PlayModeTests")]
[assembly: InternalsVisibleTo("Unity.UIElements.Editor")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsGameObjectsModule")]
[assembly: CompilationRelaxations(8)]
[assembly: InternalsVisibleTo("Unity.Testing.VisualEffectGraph.EditorTests-testable")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace UnityEngine.Experimental.VFX
{
	internal static class VFXManager
	{
	}
}
namespace UnityEngine.VFX
{
	[Flags]
	internal enum VFXCullingFlags
	{
		CullNone = 0,
		CullSimulation = 1,
		CullBoundsUpdate = 2,
		CullDefault = 3
	}
	internal enum VFXExpressionOperation
	{
		None,
		Value,
		Combine2f,
		Combine3f,
		Combine4f,
		ExtractComponent,
		DeltaTime,
		TotalTime,
		SystemSeed,
		LocalToWorld,
		WorldToLocal,
		FrameIndex,
		PlayRate,
		UnscaledDeltaTime,
		ManagerMaxDeltaTime,
		ManagerFixedTimeStep,
		GameDeltaTime,
		GameUnscaledDeltaTime,
		GameSmoothDeltaTime,
		GameTotalTime,
		GameUnscaledTotalTime,
		GameTotalTimeSinceSceneLoad,
		GameTimeScale,
		Sin,
		Cos,
		Tan,
		ASin,
		ACos,
		ATan,
		Abs,
		Sign,
		Saturate,
		Ceil,
		Round,
		Frac,
		Floor,
		Log2,
		Mul,
		Divide,
		Add,
		Subtract,
		Min,
		Max,
		Pow,
		ATan2,
		TRSToMatrix,
		InverseMatrix,
		InverseTRSMatrix,
		TransposeMatrix,
		ExtractPositionFromMatrix,
		ExtractAnglesFromMatrix,
		ExtractScaleFromMatrix,
		TransformMatrix,
		TransformPos,
		TransformVec,
		TransformDir,
		TransformVector4,
		Vector3sToMatrix,
		Vector4sToMatrix,
		MatrixToVector3s,
		MatrixToVector4s,
		SampleCurve,
		SampleGradient,
		SampleMeshVertexFloat,
		SampleMeshVertexFloat2,
		SampleMeshVertexFloat3,
		SampleMeshVertexFloat4,
		SampleMeshVertexColor,
		SampleMeshIndex,
		VertexBufferFromMesh,
		VertexBufferFromSkinnedMeshRenderer,
		IndexBufferFromMesh,
		MeshFromSkinnedMeshRenderer,
		RootBoneTransformFromSkinnedMeshRenderer,
		BakeCurve,
		BakeGradient,
		BitwiseLeftShift,
		BitwiseRightShift,
		BitwiseOr,
		BitwiseAnd,
		BitwiseXor,
		BitwiseComplement,
		CastUintToFloat,
		CastIntToFloat,
		CastFloatToUint,
		CastIntToUint,
		CastFloatToInt,
		CastUintToInt,
		CastIntToBool,
		CastUintToBool,
		CastFloatToBool,
		CastBoolToInt,
		CastBoolToUint,
		CastBoolToFloat,
		RGBtoHSV,
		HSVtoRGB,
		Condition,
		Branch,
		GenerateRandom,
		GenerateFixedRandom,
		ExtractMatrixFromMainCamera,
		ExtractFOVFromMainCamera,
		ExtractNearPlaneFromMainCamera,
		ExtractFarPlaneFromMainCamera,
		ExtractAspectRatioFromMainCamera,
		ExtractPixelDimensionsFromMainCamera,
		ExtractScaledPixelDimensionsFromMainCamera,
		ExtractLensShiftFromMainCamera,
		GetBufferFromMainCamera,
		IsMainCameraOrthographic,
		GetOrthographicSizeFromMainCamera,
		LogicalAnd,
		LogicalOr,
		LogicalNot,
		ValueNoise1D,
		ValueNoise2D,
		ValueNoise3D,
		ValueCurlNoise2D,
		ValueCurlNoise3D,
		PerlinNoise1D,
		PerlinNoise2D,
		PerlinNoise3D,
		PerlinCurlNoise2D,
		PerlinCurlNoise3D,
		CellularNoise1D,
		CellularNoise2D,
		CellularNoise3D,
		CellularCurlNoise2D,
		CellularCurlNoise3D,
		VoroNoise2D,
		MeshVertexCount,
		MeshChannelOffset,
		MeshChannelInfos,
		MeshVertexStride,
		MeshIndexCount,
		MeshIndexFormat,
		BufferStride,
		BufferCount,
		TextureWidth,
		TextureHeight,
		TextureDepth,
		ReadEventAttribute,
		SpawnerStateNewLoop,
		SpawnerStateLoopState,
		SpawnerStateSpawnCount,
		SpawnerStateDeltaTime,
		SpawnerStateTotalTime,
		SpawnerStateDelayBeforeLoop,
		SpawnerStateLoopDuration,
		SpawnerStateDelayAfterLoop,
		SpawnerStateLoopIndex,
		SpawnerStateLoopCount
	}
	internal enum VFXValueType
	{
		None,
		Float,
		Float2,
		Float3,
		Float4,
		Int32,
		Uint32,
		Texture2D,
		Texture2DArray,
		Texture3D,
		TextureCube,
		TextureCubeArray,
		CameraBuffer,
		Matrix4x4,
		Curve,
		ColorGradient,
		Mesh,
		Spline,
		Boolean,
		Buffer,
		SkinnedMeshRenderer
	}
	internal enum VFXTaskType
	{
		None = 0,
		Spawner = 268435456,
		Initialize = 536870912,
		Update = 805306368,
		Output = 1073741824,
		CameraSort = 805306369,
		PerCameraUpdate = 805306370,
		PerCameraSort = 805306371,
		PerOutputSort = 805306372,
		GlobalSort = 805306373,
		ParticlePointOutput = 1073741824,
		ParticleLineOutput = 1073741825,
		ParticleQuadOutput = 1073741826,
		ParticleHexahedronOutput = 1073741827,
		ParticleMeshOutput = 1073741828,
		ParticleTriangleOutput = 1073741829,
		ParticleOctagonOutput = 1073741830,
		ConstantRateSpawner = 268435456,
		BurstSpawner = 268435457,
		PeriodicBurstSpawner = 268435458,
		VariableRateSpawner = 268435459,
		CustomCallbackSpawner = 268435460,
		SetAttributeSpawner = 268435461,
		EvaluateExpressionsSpawner = 268435462
	}
	internal enum VFXSystemType
	{
		Spawner,
		Particle,
		Mesh,
		OutputEvent
	}
	internal enum VFXSystemFlag
	{
		SystemDefault = 0,
		SystemHasKill = 1,
		SystemHasIndirectBuffer = 2,
		SystemReceivedEventGPU = 4,
		SystemHasStrips = 8,
		SystemNeedsComputeBounds = 0x10,
		SystemAutomaticBounds = 0x20,
		SystemInWorldSpace = 0x40,
		SystemHasDirectLink = 0x80,
		SystemHasAttributeBuffer = 0x100,
		SystemUsesInstancedRendering = 0x200
	}
	[Flags]
	internal enum VFXUpdateMode
	{
		FixedDeltaTime = 0,
		DeltaTime = 1,
		IgnoreTimeScale = 2,
		ExactFixedTimeStep = 4,
		DeltaTimeAndIgnoreTimeScale = 3,
		FixedDeltaAndExactTime = 4,
		FixedDeltaAndExactTimeAndIgnoreTimeScale = 6
	}
	[Flags]
	public enum VFXCameraBufferTypes
	{
		None = 0,
		Depth = 1,
		Color = 2,
		Normal = 4
	}
	internal enum VFXInstancingMode
	{
		Disabled = -1,
		[InspectorName("Automatic batch capacity")]
		Auto,
		[InspectorName("Custom batch capacity")]
		Custom
	}
	[Flags]
	internal enum VFXInstancingDisabledReason
	{
		None = 0,
		[Description("A system is using indirect draw.")]
		IndirectDraw = 1,
		[Description("The effect is using output events.")]
		OutputEvent = 2,
		[Description("The effect is using GPU events.")]
		GPUEvent = 4,
		[Description("An Initialize node has Bounds Mode set to 'Automatic'.")]
		AutomaticBounds = 8,
		[Description("The effect contains a mesh output.")]
		MeshOutput = 0x10,
		[Description("The effect has exposed texture, mesh or graphics buffer properties.")]
		ExposedObject = 0x20,
		[Description("Unknown reason.")]
		Unknown = -1
	}
	internal enum VFXMainCameraBufferFallback
	{
		NoFallback,
		PreferMainCamera,
		PreferSceneCamera
	}
	internal enum VFXSkinnedMeshFrame
	{
		Current,
		Previous
	}
	internal enum VFXSkinnedTransform
	{
		LocalRootBoneTransform,
		WorldRootBoneTransform
	}
	[StructLayout(LayoutKind.Sequential)]
	[RequiredByNativeCode]
	[NativeType(Header = "Modules/VFX/Public/VFXEventAttribute.h")]
	public sealed class VFXEventAttribute : IDisposable
	{
		private IntPtr m_Ptr;

		private bool m_Owner;

		private VisualEffectAsset m_VfxAsset;

		internal VisualEffectAsset vfxAsset => m_VfxAsset;

		private VFXEventAttribute(IntPtr ptr, bool owner, VisualEffectAsset vfxAsset)
		{
			m_Ptr = ptr;
			m_Owner = owner;
			m_VfxAsset = vfxAsset;
		}

		private VFXEventAttribute()
			: this(IntPtr.Zero, owner: false, null)
		{
		}

		internal static VFXEventAttribute CreateEventAttributeWrapper()
		{
			return new VFXEventAttribute(IntPtr.Zero, owner: false, null);
		}

		internal void SetWrapValue(IntPtr ptrToEventAttribute)
		{
			if (m_Owner)
			{
				throw new Exception("VFXSpawnerState : SetWrapValue is reserved to CreateWrapper object");
			}
			m_Ptr = ptrToEventAttribute;
		}

		public VFXEventAttribute(VFXEventAttribute original)
		{
			if (original == null)
			{
				throw new ArgumentNullException("VFXEventAttribute expect a non null attribute");
			}
			m_Ptr = Internal_Create();
			m_VfxAsset = original.m_VfxAsset;
			Internal_InitFromEventAttribute(original);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr Internal_Create();

		internal static VFXEventAttribute Internal_InstanciateVFXEventAttribute(VisualEffectAsset vfxAsset)
		{
			VFXEventAttribute vFXEventAttribute = new VFXEventAttribute(Internal_Create(), owner: true, vfxAsset);
			vFXEventAttribute.Internal_InitFromAsset(vfxAsset);
			return vFXEventAttribute;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void Internal_InitFromAsset(VisualEffectAsset vfxAsset);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void Internal_InitFromEventAttribute(VFXEventAttribute vfxEventAttribute);

		private void Release()
		{
			if (m_Owner && m_Ptr != IntPtr.Zero)
			{
				Internal_Destroy(m_Ptr);
			}
			m_Ptr = IntPtr.Zero;
			m_VfxAsset = null;
		}

		~VFXEventAttribute()
		{
			Release();
		}

		public void Dispose()
		{
			Release();
			GC.SuppressFinalize(this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsThreadSafe = true)]
		internal static extern void Internal_Destroy(IntPtr ptr);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("HasValueFromScript<bool>")]
		public extern bool HasBool(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("HasValueFromScript<int>")]
		public extern bool HasInt(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("HasValueFromScript<UInt32>")]
		public extern bool HasUint(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("HasValueFromScript<float>")]
		public extern bool HasFloat(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("HasValueFromScript<Vector2f>")]
		public extern bool HasVector2(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("HasValueFromScript<Vector3f>")]
		public extern bool HasVector3(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("HasValueFromScript<Vector4f>")]
		public extern bool HasVector4(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("HasValueFromScript<Matrix4x4f>")]
		public extern bool HasMatrix4x4(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("SetValueFromScript<bool>")]
		public extern void SetBool(int nameID, bool b);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("SetValueFromScript<int>")]
		public extern void SetInt(int nameID, int i);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("SetValueFromScript<UInt32>")]
		public extern void SetUint(int nameID, uint i);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("SetValueFromScript<float>")]
		public extern void SetFloat(int nameID, float f);

		[NativeName("SetValueFromScript<Vector2f>")]
		public void SetVector2(int nameID, Vector2 v)
		{
			SetVector2_Injected(nameID, ref v);
		}

		[NativeName("SetValueFromScript<Vector3f>")]
		public void SetVector3(int nameID, Vector3 v)
		{
			SetVector3_Injected(nameID, ref v);
		}

		[NativeName("SetValueFromScript<Vector4f>")]
		public void SetVector4(int nameID, Vector4 v)
		{
			SetVector4_Injected(nameID, ref v);
		}

		[NativeName("SetValueFromScript<Matrix4x4f>")]
		public void SetMatrix4x4(int nameID, Matrix4x4 v)
		{
			SetMatrix4x4_Injected(nameID, ref v);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetValueFromScript<bool>")]
		public extern bool GetBool(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetValueFromScript<int>")]
		public extern int GetInt(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetValueFromScript<UInt32>")]
		public extern uint GetUint(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetValueFromScript<float>")]
		public extern float GetFloat(int nameID);

		[NativeName("GetValueFromScript<Vector2f>")]
		public Vector2 GetVector2(int nameID)
		{
			GetVector2_Injected(nameID, out var ret);
			return ret;
		}

		[NativeName("GetValueFromScript<Vector3f>")]
		public Vector3 GetVector3(int nameID)
		{
			GetVector3_Injected(nameID, out var ret);
			return ret;
		}

		[NativeName("GetValueFromScript<Vector4f>")]
		public Vector4 GetVector4(int nameID)
		{
			GetVector4_Injected(nameID, out var ret);
			return ret;
		}

		[NativeName("GetValueFromScript<Matrix4x4f>")]
		public Matrix4x4 GetMatrix4x4(int nameID)
		{
			GetMatrix4x4_Injected(nameID, out var ret);
			return ret;
		}

		public bool HasBool(string name)
		{
			return HasBool(Shader.PropertyToID(name));
		}

		public bool HasInt(string name)
		{
			return HasInt(Shader.PropertyToID(name));
		}

		public bool HasUint(string name)
		{
			return HasUint(Shader.PropertyToID(name));
		}

		public bool HasFloat(string name)
		{
			return HasFloat(Shader.PropertyToID(name));
		}

		public bool HasVector2(string name)
		{
			return HasVector2(Shader.PropertyToID(name));
		}

		public bool HasVector3(string name)
		{
			return HasVector3(Shader.PropertyToID(name));
		}

		public bool HasVector4(string name)
		{
			return HasVector4(Shader.PropertyToID(name));
		}

		public bool HasMatrix4x4(string name)
		{
			return HasMatrix4x4(Shader.PropertyToID(name));
		}

		public void SetBool(string name, bool b)
		{
			SetBool(Shader.PropertyToID(name), b);
		}

		public void SetInt(string name, int i)
		{
			SetInt(Shader.PropertyToID(name), i);
		}

		public void SetUint(string name, uint i)
		{
			SetUint(Shader.PropertyToID(name), i);
		}

		public void SetFloat(string name, float f)
		{
			SetFloat(Shader.PropertyToID(name), f);
		}

		public void SetVector2(string name, Vector2 v)
		{
			SetVector2(Shader.PropertyToID(name), v);
		}

		public void SetVector3(string name, Vector3 v)
		{
			SetVector3(Shader.PropertyToID(name), v);
		}

		public void SetVector4(string name, Vector4 v)
		{
			SetVector4(Shader.PropertyToID(name), v);
		}

		public void SetMatrix4x4(string name, Matrix4x4 v)
		{
			SetMatrix4x4(Shader.PropertyToID(name), v);
		}

		public bool GetBool(string name)
		{
			return GetBool(Shader.PropertyToID(name));
		}

		public int GetInt(string name)
		{
			return GetInt(Shader.PropertyToID(name));
		}

		public uint GetUint(string name)
		{
			return GetUint(Shader.PropertyToID(name));
		}

		public float GetFloat(string name)
		{
			return GetFloat(Shader.PropertyToID(name));
		}

		public Vector2 GetVector2(string name)
		{
			return GetVector2(Shader.PropertyToID(name));
		}

		public Vector3 GetVector3(string name)
		{
			return GetVector3(Shader.PropertyToID(name));
		}

		public Vector4 GetVector4(string name)
		{
			return GetVector4(Shader.PropertyToID(name));
		}

		public Matrix4x4 GetMatrix4x4(string name)
		{
			return GetMatrix4x4(Shader.PropertyToID(name));
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CopyValuesFrom([NotNull("ArgumentNullException")] VFXEventAttribute eventAttibute);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVector2_Injected(int nameID, ref Vector2 v);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVector3_Injected(int nameID, ref Vector3 v);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVector4_Injected(int nameID, ref Vector4 v);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetMatrix4x4_Injected(int nameID, ref Matrix4x4 v);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVector2_Injected(int nameID, out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVector3_Injected(int nameID, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVector4_Injected(int nameID, out Vector4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetMatrix4x4_Injected(int nameID, out Matrix4x4 ret);
	}
	[StructLayout(LayoutKind.Sequential)]
	[NativeType(Header = "Modules/VFX/Public/VFXExpressionValues.h")]
	[RequiredByNativeCode]
	public class VFXExpressionValues
	{
		internal IntPtr m_Ptr;

		private VFXExpressionValues()
		{
		}

		[RequiredByNativeCode]
		internal static VFXExpressionValues CreateExpressionValuesWrapper(IntPtr ptr)
		{
			VFXExpressionValues vFXExpressionValues = new VFXExpressionValues();
			vFXExpressionValues.m_Ptr = ptr;
			return vFXExpressionValues;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetValueFromScript<bool>")]
		[NativeThrows]
		public extern bool GetBool(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetValueFromScript<int>")]
		[NativeThrows]
		public extern int GetInt(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		[NativeName("GetValueFromScript<UInt32>")]
		public extern uint GetUInt(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		[NativeName("GetValueFromScript<float>")]
		public extern float GetFloat(int nameID);

		[NativeThrows]
		[NativeName("GetValueFromScript<Vector2f>")]
		public Vector2 GetVector2(int nameID)
		{
			GetVector2_Injected(nameID, out var ret);
			return ret;
		}

		[NativeThrows]
		[NativeName("GetValueFromScript<Vector3f>")]
		public Vector3 GetVector3(int nameID)
		{
			GetVector3_Injected(nameID, out var ret);
			return ret;
		}

		[NativeName("GetValueFromScript<Vector4f>")]
		[NativeThrows]
		public Vector4 GetVector4(int nameID)
		{
			GetVector4_Injected(nameID, out var ret);
			return ret;
		}

		[NativeName("GetValueFromScript<Matrix4x4f>")]
		[NativeThrows]
		public Matrix4x4 GetMatrix4x4(int nameID)
		{
			GetMatrix4x4_Injected(nameID, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		[NativeName("GetValueFromScript<Texture*>")]
		public extern Texture GetTexture(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetValueFromScript<Mesh*>")]
		[NativeThrows]
		public extern Mesh GetMesh(int nameID);

		public AnimationCurve GetAnimationCurve(int nameID)
		{
			AnimationCurve animationCurve = new AnimationCurve();
			Internal_GetAnimationCurveFromScript(nameID, animationCurve);
			return animationCurve;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		internal extern void Internal_GetAnimationCurveFromScript(int nameID, AnimationCurve curve);

		public Gradient GetGradient(int nameID)
		{
			Gradient gradient = new Gradient();
			Internal_GetGradientFromScript(nameID, gradient);
			return gradient;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		internal extern void Internal_GetGradientFromScript(int nameID, Gradient gradient);

		public bool GetBool(string name)
		{
			return GetBool(Shader.PropertyToID(name));
		}

		public int GetInt(string name)
		{
			return GetInt(Shader.PropertyToID(name));
		}

		public uint GetUInt(string name)
		{
			return GetUInt(Shader.PropertyToID(name));
		}

		public float GetFloat(string name)
		{
			return GetFloat(Shader.PropertyToID(name));
		}

		public Vector2 GetVector2(string name)
		{
			return GetVector2(Shader.PropertyToID(name));
		}

		public Vector3 GetVector3(string name)
		{
			return GetVector3(Shader.PropertyToID(name));
		}

		public Vector4 GetVector4(string name)
		{
			return GetVector4(Shader.PropertyToID(name));
		}

		public Matrix4x4 GetMatrix4x4(string name)
		{
			return GetMatrix4x4(Shader.PropertyToID(name));
		}

		public Texture GetTexture(string name)
		{
			return GetTexture(Shader.PropertyToID(name));
		}

		public AnimationCurve GetAnimationCurve(string name)
		{
			return GetAnimationCurve(Shader.PropertyToID(name));
		}

		public Gradient GetGradient(string name)
		{
			return GetGradient(Shader.PropertyToID(name));
		}

		public Mesh GetMesh(string name)
		{
			return GetMesh(Shader.PropertyToID(name));
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVector2_Injected(int nameID, out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVector3_Injected(int nameID, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVector4_Injected(int nameID, out Vector4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetMatrix4x4_Injected(int nameID, out Matrix4x4 ret);
	}
	[RequiredByNativeCode]
	public struct VFXCameraXRSettings
	{
		public uint viewTotal;

		public uint viewCount;

		public uint viewOffset;
	}
	[RequiredByNativeCode]
	public struct VFXBatchedEffectInfo
	{
		public VisualEffectAsset vfxAsset;

		public uint activeBatchCount;

		public uint inactiveBatchCount;

		public uint activeInstanceCount;

		public uint unbatchedInstanceCount;

		public uint totalInstanceCapacity;

		public uint maxInstancePerBatchCapacity;

		public ulong totalGPUSizeInBytes;

		public ulong totalCPUSizeInBytes;
	}
	[RequiredByNativeCode]
	internal struct VFXBatchInfo
	{
		public uint capacity;

		public uint activeInstanceCount;
	}
	[StaticAccessor("GetVFXManager()", StaticAccessorType.Dot)]
	[NativeHeader("Modules/VFX/Public/ScriptBindings/VFXManagerBindings.h")]
	[NativeHeader("Modules/VFX/Public/VFXManager.h")]
	[RequiredByNativeCode]
	public static class VFXManager
	{
		private static readonly VFXCameraXRSettings kDefaultCameraXRSettings = new VFXCameraXRSettings
		{
			viewTotal = 1u,
			viewCount = 1u,
			viewOffset = 0u
		};

		internal static extern ScriptableObject runtimeResources
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern float fixedTimeStep
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public static extern float maxDeltaTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal static extern float maxScrubTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal static extern string renderPipeSettingsPath
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal static extern uint batchEmptyLifetime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern VisualEffect[] GetComponents();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void CleanupEmptyBatches(bool force = false);

		public static void FlushEmptyBatches()
		{
			CleanupEmptyBatches(force: true);
		}

		public static VFXBatchedEffectInfo GetBatchedEffectInfo([NotNull("NullExceptionObject")] VisualEffectAsset vfx)
		{
			GetBatchedEffectInfo_Injected(vfx, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VFXManagerBindings::GetBatchedEffectInfos", HasExplicitThis = false)]
		public static extern void GetBatchedEffectInfos([NotNull("NullExceptionObject")] List<VFXBatchedEffectInfo> infos);

		internal static VFXBatchInfo GetBatchInfo(VisualEffectAsset vfx, uint batchIndex)
		{
			GetBatchInfo_Injected(vfx, batchIndex, out var ret);
			return ret;
		}

		[Obsolete("Use explicit PrepareCamera and ProcessCameraCommand instead")]
		public static void ProcessCamera(Camera cam)
		{
			PrepareCamera(cam, kDefaultCameraXRSettings);
			Internal_ProcessCameraCommand(cam, null, kDefaultCameraXRSettings, IntPtr.Zero);
		}

		public static void PrepareCamera(Camera cam)
		{
			PrepareCamera(cam, kDefaultCameraXRSettings);
		}

		public static void PrepareCamera([NotNull("NullExceptionObject")] Camera cam, VFXCameraXRSettings camXRSettings)
		{
			PrepareCamera_Injected(cam, ref camXRSettings);
		}

		[Obsolete("Use ProcessCameraCommand with CullingResults to allow culling of VFX per camera")]
		public static void ProcessCameraCommand(Camera cam, CommandBuffer cmd)
		{
			Internal_ProcessCameraCommand(cam, cmd, kDefaultCameraXRSettings, IntPtr.Zero);
		}

		[Obsolete("Use ProcessCameraCommand with CullingResults to allow culling of VFX per camera")]
		public static void ProcessCameraCommand(Camera cam, CommandBuffer cmd, VFXCameraXRSettings camXRSettings)
		{
			Internal_ProcessCameraCommand(cam, cmd, camXRSettings, IntPtr.Zero);
		}

		public static void ProcessCameraCommand(Camera cam, CommandBuffer cmd, VFXCameraXRSettings camXRSettings, CullingResults results)
		{
			Internal_ProcessCameraCommand(cam, cmd, camXRSettings, results.ptr);
		}

		private static void Internal_ProcessCameraCommand([NotNull("NullExceptionObject")] Camera cam, CommandBuffer cmd, VFXCameraXRSettings camXRSettings, IntPtr cullResults)
		{
			Internal_ProcessCameraCommand_Injected(cam, cmd, ref camXRSettings, cullResults);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern VFXCameraBufferTypes IsCameraBufferNeeded([NotNull("NullExceptionObject")] Camera cam);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetCameraBuffer([NotNull("NullExceptionObject")] Camera cam, VFXCameraBufferTypes type, Texture buffer, int x, int y, int width, int height);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetBatchedEffectInfo_Injected(VisualEffectAsset vfx, out VFXBatchedEffectInfo ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetBatchInfo_Injected(VisualEffectAsset vfx, uint batchIndex, out VFXBatchInfo ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PrepareCamera_Injected(Camera cam, ref VFXCameraXRSettings camXRSettings);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ProcessCameraCommand_Injected(Camera cam, CommandBuffer cmd, ref VFXCameraXRSettings camXRSettings, IntPtr cullResults);
	}
	[Serializable]
	[RequiredByNativeCode]
	public abstract class VFXSpawnerCallbacks : ScriptableObject
	{
		public abstract void OnPlay(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent);

		public abstract void OnUpdate(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent);

		public abstract void OnStop(VFXSpawnerState state, VFXExpressionValues vfxValues, VisualEffect vfxComponent);
	}
	public enum VFXSpawnerLoopState
	{
		Finished,
		DelayingBeforeLoop,
		Looping,
		DelayingAfterLoop
	}
	[StructLayout(LayoutKind.Sequential)]
	[NativeType(Header = "Modules/VFX/Public/VFXSpawnerState.h")]
	[RequiredByNativeCode]
	public sealed class VFXSpawnerState : IDisposable
	{
		private IntPtr m_Ptr;

		private bool m_Owner;

		private VFXEventAttribute m_WrapEventAttribute;

		public bool playing
		{
			get
			{
				return loopState == VFXSpawnerLoopState.Looping;
			}
			set
			{
				loopState = (value ? VFXSpawnerLoopState.Looping : VFXSpawnerLoopState.Finished);
			}
		}

		public extern bool newLoop
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern VFXSpawnerLoopState loopState
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float spawnCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float deltaTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float totalTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float delayBeforeLoop
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float loopDuration
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float delayAfterLoop
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int loopIndex
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int loopCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public VFXEventAttribute vfxEventAttribute
		{
			get
			{
				if (!m_Owner && m_WrapEventAttribute != null)
				{
					return m_WrapEventAttribute;
				}
				return Internal_GetVFXEventAttribute();
			}
		}

		public VFXSpawnerState()
			: this(Internal_Create(), owner: true)
		{
		}

		internal VFXSpawnerState(IntPtr ptr, bool owner)
		{
			m_Ptr = ptr;
			m_Owner = owner;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr Internal_Create();

		[RequiredByNativeCode]
		internal static VFXSpawnerState CreateSpawnerStateWrapper()
		{
			VFXSpawnerState vFXSpawnerState = new VFXSpawnerState(IntPtr.Zero, owner: false);
			vFXSpawnerState.PrepareWrapper();
			return vFXSpawnerState;
		}

		private void PrepareWrapper()
		{
			if (m_Owner)
			{
				throw new Exception("VFXSpawnerState : SetWrapValue is reserved to CreateWrapper object");
			}
			if (m_WrapEventAttribute != null)
			{
				throw new Exception("VFXSpawnerState : Unexpected calling twice prepare wrapper");
			}
			m_WrapEventAttribute = VFXEventAttribute.CreateEventAttributeWrapper();
		}

		[RequiredByNativeCode]
		internal void SetWrapValue(IntPtr ptrToSpawnerState, IntPtr ptrToEventAttribute)
		{
			if (m_Owner)
			{
				throw new Exception("VFXSpawnerState : SetWrapValue is reserved to CreateWrapper object");
			}
			if (m_WrapEventAttribute == null)
			{
				throw new Exception("VFXSpawnerState : Missing PrepareWrapper");
			}
			m_Ptr = ptrToSpawnerState;
			m_WrapEventAttribute.SetWrapValue(ptrToEventAttribute);
		}

		internal IntPtr GetPtr()
		{
			return m_Ptr;
		}

		private void Release()
		{
			if (m_Ptr != IntPtr.Zero && m_Owner)
			{
				Internal_Destroy(m_Ptr);
			}
			m_Ptr = IntPtr.Zero;
			m_WrapEventAttribute = null;
		}

		~VFXSpawnerState()
		{
			Release();
		}

		public void Dispose()
		{
			Release();
			GC.SuppressFinalize(this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsThreadSafe = true)]
		private static extern void Internal_Destroy(IntPtr ptr);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern VFXEventAttribute Internal_GetVFXEventAttribute();
	}
	[UsedByNativeCode]
	public struct VFXExposedProperty
	{
		public string name;

		public Type type;
	}
	[UsedByNativeCode]
	[NativeHeader("Modules/VFX/Public/VisualEffectAsset.h")]
	[NativeHeader("VFXScriptingClasses.h")]
	[NativeHeader("Modules/VFX/Public/ScriptBindings/VisualEffectAssetBindings.h")]
	public abstract class VisualEffectObject : Object
	{
	}
	[UsedByNativeCode]
	[NativeHeader("Modules/VFX/Public/VisualEffectAsset.h")]
	[NativeHeader("VFXScriptingClasses.h")]
	public class VisualEffectAsset : VisualEffectObject
	{
		public const string PlayEventName = "OnPlay";

		public const string StopEventName = "OnStop";

		public static readonly int PlayEventID = Shader.PropertyToID("OnPlay");

		public static readonly int StopEventID = Shader.PropertyToID("OnStop");

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectAssetBindings::GetTextureDimension", HasExplicitThis = true)]
		public extern TextureDimension GetTextureDimension(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectAssetBindings::GetExposedProperties", HasExplicitThis = true)]
		public extern void GetExposedProperties([NotNull("ArgumentNullException")] List<VFXExposedProperty> exposedProperties);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectAssetBindings::GetEvents", HasExplicitThis = true)]
		public extern void GetEvents([NotNull("ArgumentNullException")] List<string> names);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectAssetBindings::HasSystemFromScript", HasExplicitThis = true)]
		internal extern bool HasSystem(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectAssetBindings::GetSystemNamesFromScript", HasExplicitThis = true)]
		internal extern void GetSystemNames([NotNull("ArgumentNullException")] List<string> names);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectAssetBindings::GetParticleSystemNamesFromScript", HasExplicitThis = true)]
		internal extern void GetParticleSystemNames([NotNull("ArgumentNullException")] List<string> names);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectAssetBindings::GetOutputEventNamesFromScript", HasExplicitThis = true)]
		internal extern void GetOutputEventNames([NotNull("ArgumentNullException")] List<string> names);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectAssetBindings::GetSpawnSystemNamesFromScript", HasExplicitThis = true)]
		internal extern void GetSpawnSystemNames([NotNull("ArgumentNullException")] List<string> names);

		public TextureDimension GetTextureDimension(string name)
		{
			return GetTextureDimension(Shader.PropertyToID(name));
		}
	}
	public struct VFXOutputEventArgs
	{
		public int nameId { get; }

		public VFXEventAttribute eventAttribute { get; }

		public VFXOutputEventArgs(int nameId, VFXEventAttribute eventAttribute)
		{
			this.nameId = nameId;
			this.eventAttribute = eventAttribute;
		}
	}
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/VFX/Public/VisualEffect.h")]
	[NativeHeader("Modules/VFX/Public/ScriptBindings/VisualEffectBindings.h")]
	public class VisualEffect : Behaviour
	{
		private VFXEventAttribute m_cachedEventAttribute;

		public Action<VFXOutputEventArgs> outputEventReceived;

		public extern bool pause
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float playRate
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern uint startSeed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool resetSeedOnPlay
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int initialEventID
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction(Name = "VisualEffectBindings::GetInitialEventID", HasExplicitThis = true)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction(Name = "VisualEffectBindings::SetInitialEventID", HasExplicitThis = true)]
			set;
		}

		public extern string initialEventName
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction(Name = "VisualEffectBindings::GetInitialEventName", HasExplicitThis = true)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction(Name = "VisualEffectBindings::SetInitialEventName", HasExplicitThis = true)]
			set;
		}

		public extern bool culled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern VisualEffectAsset visualEffectAsset
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int aliveParticleCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal extern float time
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public VFXEventAttribute CreateVFXEventAttribute()
		{
			if (visualEffectAsset == null)
			{
				return null;
			}
			return VFXEventAttribute.Internal_InstanciateVFXEventAttribute(visualEffectAsset);
		}

		private void CheckValidVFXEventAttribute(VFXEventAttribute eventAttribute)
		{
			if (eventAttribute != null && eventAttribute.vfxAsset != visualEffectAsset)
			{
				throw new InvalidOperationException("Invalid VFXEventAttribute provided to VisualEffect. It has been created with another VisualEffectAsset. Use CreateVFXEventAttribute.");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::SendEventFromScript", HasExplicitThis = true)]
		private extern void SendEventFromScript(int eventNameID, VFXEventAttribute eventAttribute);

		public void SendEvent(int eventNameID, VFXEventAttribute eventAttribute)
		{
			CheckValidVFXEventAttribute(eventAttribute);
			SendEventFromScript(eventNameID, eventAttribute);
		}

		public void SendEvent(string eventName, VFXEventAttribute eventAttribute)
		{
			SendEvent(Shader.PropertyToID(eventName), eventAttribute);
		}

		public void SendEvent(int eventNameID)
		{
			SendEventFromScript(eventNameID, null);
		}

		public void SendEvent(string eventName)
		{
			SendEvent(Shader.PropertyToID(eventName), null);
		}

		public void Play(VFXEventAttribute eventAttribute)
		{
			SendEvent(VisualEffectAsset.PlayEventID, eventAttribute);
		}

		public void Play()
		{
			SendEvent(VisualEffectAsset.PlayEventID);
		}

		public void Stop(VFXEventAttribute eventAttribute)
		{
			SendEvent(VisualEffectAsset.StopEventID, eventAttribute);
		}

		public void Stop()
		{
			SendEvent(VisualEffectAsset.StopEventID);
		}

		public void Reinit()
		{
			Reinit(sendInitialEventAndPrewarm: true);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void Reinit(bool sendInitialEventAndPrewarm = true);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AdvanceOneFrame();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void RecreateData();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::ResetOverrideFromScript", HasExplicitThis = true)]
		public extern void ResetOverride(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::GetTextureDimensionFromScript", HasExplicitThis = true)]
		public extern TextureDimension GetTextureDimension(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<bool>", HasExplicitThis = true)]
		public extern bool HasBool(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<int>", HasExplicitThis = true)]
		public extern bool HasInt(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<UInt32>", HasExplicitThis = true)]
		public extern bool HasUInt(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<float>", HasExplicitThis = true)]
		public extern bool HasFloat(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Vector2f>", HasExplicitThis = true)]
		public extern bool HasVector2(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Vector3f>", HasExplicitThis = true)]
		public extern bool HasVector3(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Vector4f>", HasExplicitThis = true)]
		public extern bool HasVector4(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Matrix4x4f>", HasExplicitThis = true)]
		public extern bool HasMatrix4x4(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Texture*>", HasExplicitThis = true)]
		public extern bool HasTexture(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<AnimationCurve*>", HasExplicitThis = true)]
		public extern bool HasAnimationCurve(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Gradient*>", HasExplicitThis = true)]
		public extern bool HasGradient(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<Mesh*>", HasExplicitThis = true)]
		public extern bool HasMesh(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<SkinnedMeshRenderer*>", HasExplicitThis = true)]
		public extern bool HasSkinnedMeshRenderer(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::HasValueFromScript<GraphicsBuffer*>", HasExplicitThis = true)]
		public extern bool HasGraphicsBuffer(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<bool>", HasExplicitThis = true)]
		public extern void SetBool(int nameID, bool b);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<int>", HasExplicitThis = true)]
		public extern void SetInt(int nameID, int i);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<UInt32>", HasExplicitThis = true)]
		public extern void SetUInt(int nameID, uint i);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<float>", HasExplicitThis = true)]
		public extern void SetFloat(int nameID, float f);

		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Vector2f>", HasExplicitThis = true)]
		public void SetVector2(int nameID, Vector2 v)
		{
			SetVector2_Injected(nameID, ref v);
		}

		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Vector3f>", HasExplicitThis = true)]
		public void SetVector3(int nameID, Vector3 v)
		{
			SetVector3_Injected(nameID, ref v);
		}

		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Vector4f>", HasExplicitThis = true)]
		public void SetVector4(int nameID, Vector4 v)
		{
			SetVector4_Injected(nameID, ref v);
		}

		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Matrix4x4f>", HasExplicitThis = true)]
		public void SetMatrix4x4(int nameID, Matrix4x4 v)
		{
			SetMatrix4x4_Injected(nameID, ref v);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Texture*>", HasExplicitThis = true)]
		public extern void SetTexture(int nameID, [NotNull("ArgumentNullException")] Texture t);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<AnimationCurve*>", HasExplicitThis = true)]
		public extern void SetAnimationCurve(int nameID, [NotNull("ArgumentNullException")] AnimationCurve c);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Gradient*>", HasExplicitThis = true)]
		public extern void SetGradient(int nameID, [NotNull("ArgumentNullException")] Gradient g);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<Mesh*>", HasExplicitThis = true)]
		public extern void SetMesh(int nameID, [NotNull("ArgumentNullException")] Mesh m);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<SkinnedMeshRenderer*>", HasExplicitThis = true)]
		public extern void SetSkinnedMeshRenderer(int nameID, SkinnedMeshRenderer m);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::SetValueFromScript<GraphicsBuffer*>", HasExplicitThis = true)]
		public extern void SetGraphicsBuffer(int nameID, GraphicsBuffer g);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<bool>", HasExplicitThis = true)]
		public extern bool GetBool(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<int>", HasExplicitThis = true)]
		public extern int GetInt(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<UInt32>", HasExplicitThis = true)]
		public extern uint GetUInt(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<float>", HasExplicitThis = true)]
		public extern float GetFloat(int nameID);

		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Vector2f>", HasExplicitThis = true)]
		public Vector2 GetVector2(int nameID)
		{
			GetVector2_Injected(nameID, out var ret);
			return ret;
		}

		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Vector3f>", HasExplicitThis = true)]
		public Vector3 GetVector3(int nameID)
		{
			GetVector3_Injected(nameID, out var ret);
			return ret;
		}

		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Vector4f>", HasExplicitThis = true)]
		public Vector4 GetVector4(int nameID)
		{
			GetVector4_Injected(nameID, out var ret);
			return ret;
		}

		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Matrix4x4f>", HasExplicitThis = true)]
		public Matrix4x4 GetMatrix4x4(int nameID)
		{
			GetMatrix4x4_Injected(nameID, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Texture*>", HasExplicitThis = true)]
		public extern Texture GetTexture(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<Mesh*>", HasExplicitThis = true)]
		public extern Mesh GetMesh(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<SkinnedMeshRenderer*>", HasExplicitThis = true)]
		public extern SkinnedMeshRenderer GetSkinnedMeshRenderer(int nameID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::GetValueFromScript<GraphicsBuffer*>", HasExplicitThis = true)]
		internal extern GraphicsBuffer GetGraphicsBuffer(int nameID);

		public Gradient GetGradient(int nameID)
		{
			Gradient gradient = new Gradient();
			Internal_GetGradient(nameID, gradient);
			return gradient;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::Internal_GetGradientFromScript", HasExplicitThis = true)]
		private extern void Internal_GetGradient(int nameID, Gradient gradient);

		public AnimationCurve GetAnimationCurve(int nameID)
		{
			AnimationCurve animationCurve = new AnimationCurve();
			Internal_GetAnimationCurve(nameID, animationCurve);
			return animationCurve;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::Internal_GetAnimationCurveFromScript", HasExplicitThis = true)]
		private extern void Internal_GetAnimationCurve(int nameID, AnimationCurve curve);

		[FreeFunction(Name = "VisualEffectBindings::GetParticleSystemInfo", HasExplicitThis = true, ThrowsException = true)]
		public VFXParticleSystemInfo GetParticleSystemInfo(int nameID)
		{
			GetParticleSystemInfo_Injected(nameID, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "VisualEffectBindings::GetSpawnSystemInfo", HasExplicitThis = true, ThrowsException = true)]
		private extern void GetSpawnSystemInfo(int nameID, IntPtr spawnerState);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasAnySystemAwake();

		[FreeFunction(Name = "VisualEffectBindings::GetComputedBounds", HasExplicitThis = true)]
		internal Bounds GetComputedBounds(int nameID)
		{
			GetComputedBounds_Injected(nameID, out var ret);
			return ret;
		}

		[FreeFunction(Name = "VisualEffectBindings::GetCurrentBoundsPadding", HasExplicitThis = true)]
		internal Vector3 GetCurrentBoundsPadding(int nameID)
		{
			GetCurrentBoundsPadding_Injected(nameID, out var ret);
			return ret;
		}

		public void GetSpawnSystemInfo(int nameID, VFXSpawnerState spawnState)
		{
			if (spawnState == null)
			{
				throw new NullReferenceException("GetSpawnSystemInfo expects a non null VFXSpawnerState.");
			}
			IntPtr ptr = spawnState.GetPtr();
			if (ptr == IntPtr.Zero)
			{
				throw new NullReferenceException("GetSpawnSystemInfo use an unexpected not owned VFXSpawnerState.");
			}
			GetSpawnSystemInfo(nameID, ptr);
		}

		public VFXSpawnerState GetSpawnSystemInfo(int nameID)
		{
			VFXSpawnerState vFXSpawnerState = new VFXSpawnerState();
			GetSpawnSystemInfo(nameID, vFXSpawnerState);
			return vFXSpawnerState;
		}

		public bool HasSystem(int nameID)
		{
			VisualEffectAsset visualEffectAsset = this.visualEffectAsset;
			return visualEffectAsset != null && visualEffectAsset.HasSystem(nameID);
		}

		public void GetSystemNames(List<string> names)
		{
			if (names == null)
			{
				throw new ArgumentNullException("names");
			}
			VisualEffectAsset visualEffectAsset = this.visualEffectAsset;
			if ((bool)visualEffectAsset)
			{
				visualEffectAsset.GetSystemNames(names);
			}
			else
			{
				names.Clear();
			}
		}

		public void GetParticleSystemNames(List<string> names)
		{
			if (names == null)
			{
				throw new ArgumentNullException("names");
			}
			VisualEffectAsset visualEffectAsset = this.visualEffectAsset;
			if ((bool)visualEffectAsset)
			{
				visualEffectAsset.GetParticleSystemNames(names);
			}
			else
			{
				names.Clear();
			}
		}

		public void GetOutputEventNames(List<string> names)
		{
			if (names == null)
			{
				throw new ArgumentNullException("names");
			}
			VisualEffectAsset visualEffectAsset = this.visualEffectAsset;
			if ((bool)visualEffectAsset)
			{
				visualEffectAsset.GetOutputEventNames(names);
			}
			else
			{
				names.Clear();
			}
		}

		public void GetSpawnSystemNames(List<string> names)
		{
			if (names == null)
			{
				throw new ArgumentNullException("names");
			}
			VisualEffectAsset visualEffectAsset = this.visualEffectAsset;
			if ((bool)visualEffectAsset)
			{
				visualEffectAsset.GetSpawnSystemNames(names);
			}
			else
			{
				names.Clear();
			}
		}

		public void ResetOverride(string name)
		{
			ResetOverride(Shader.PropertyToID(name));
		}

		public bool HasInt(string name)
		{
			return HasInt(Shader.PropertyToID(name));
		}

		public bool HasUInt(string name)
		{
			return HasUInt(Shader.PropertyToID(name));
		}

		public bool HasFloat(string name)
		{
			return HasFloat(Shader.PropertyToID(name));
		}

		public bool HasVector2(string name)
		{
			return HasVector2(Shader.PropertyToID(name));
		}

		public bool HasVector3(string name)
		{
			return HasVector3(Shader.PropertyToID(name));
		}

		public bool HasVector4(string name)
		{
			return HasVector4(Shader.PropertyToID(name));
		}

		public bool HasMatrix4x4(string name)
		{
			return HasMatrix4x4(Shader.PropertyToID(name));
		}

		public bool HasTexture(string name)
		{
			return HasTexture(Shader.PropertyToID(name));
		}

		public TextureDimension GetTextureDimension(string name)
		{
			return GetTextureDimension(Shader.PropertyToID(name));
		}

		public bool HasAnimationCurve(string name)
		{
			return HasAnimationCurve(Shader.PropertyToID(name));
		}

		public bool HasGradient(string name)
		{
			return HasGradient(Shader.PropertyToID(name));
		}

		public bool HasMesh(string name)
		{
			return HasMesh(Shader.PropertyToID(name));
		}

		public bool HasSkinnedMeshRenderer(string name)
		{
			return HasSkinnedMeshRenderer(Shader.PropertyToID(name));
		}

		public bool HasGraphicsBuffer(string name)
		{
			return HasGraphicsBuffer(Shader.PropertyToID(name));
		}

		public bool HasBool(string name)
		{
			return HasBool(Shader.PropertyToID(name));
		}

		public void SetInt(string name, int i)
		{
			SetInt(Shader.PropertyToID(name), i);
		}

		public void SetUInt(string name, uint i)
		{
			SetUInt(Shader.PropertyToID(name), i);
		}

		public void SetFloat(string name, float f)
		{
			SetFloat(Shader.PropertyToID(name), f);
		}

		public void SetVector2(string name, Vector2 v)
		{
			SetVector2(Shader.PropertyToID(name), v);
		}

		public void SetVector3(string name, Vector3 v)
		{
			SetVector3(Shader.PropertyToID(name), v);
		}

		public void SetVector4(string name, Vector4 v)
		{
			SetVector4(Shader.PropertyToID(name), v);
		}

		public void SetMatrix4x4(string name, Matrix4x4 v)
		{
			SetMatrix4x4(Shader.PropertyToID(name), v);
		}

		public void SetTexture(string name, Texture t)
		{
			SetTexture(Shader.PropertyToID(name), t);
		}

		public void SetAnimationCurve(string name, AnimationCurve c)
		{
			SetAnimationCurve(Shader.PropertyToID(name), c);
		}

		public void SetGradient(string name, Gradient g)
		{
			SetGradient(Shader.PropertyToID(name), g);
		}

		public void SetMesh(string name, Mesh m)
		{
			SetMesh(Shader.PropertyToID(name), m);
		}

		public void SetSkinnedMeshRenderer(string name, SkinnedMeshRenderer m)
		{
			SetSkinnedMeshRenderer(Shader.PropertyToID(name), m);
		}

		public void SetGraphicsBuffer(string name, GraphicsBuffer g)
		{
			SetGraphicsBuffer(Shader.PropertyToID(name), g);
		}

		public void SetBool(string name, bool b)
		{
			SetBool(Shader.PropertyToID(name), b);
		}

		public int GetInt(string name)
		{
			return GetInt(Shader.PropertyToID(name));
		}

		public uint GetUInt(string name)
		{
			return GetUInt(Shader.PropertyToID(name));
		}

		public float GetFloat(string name)
		{
			return GetFloat(Shader.PropertyToID(name));
		}

		public Vector2 GetVector2(string name)
		{
			return GetVector2(Shader.PropertyToID(name));
		}

		public Vector3 GetVector3(string name)
		{
			return GetVector3(Shader.PropertyToID(name));
		}

		public Vector4 GetVector4(string name)
		{
			return GetVector4(Shader.PropertyToID(name));
		}

		public Matrix4x4 GetMatrix4x4(string name)
		{
			return GetMatrix4x4(Shader.PropertyToID(name));
		}

		public Texture GetTexture(string name)
		{
			return GetTexture(Shader.PropertyToID(name));
		}

		public Mesh GetMesh(string name)
		{
			return GetMesh(Shader.PropertyToID(name));
		}

		public SkinnedMeshRenderer GetSkinnedMeshRenderer(string name)
		{
			return GetSkinnedMeshRenderer(Shader.PropertyToID(name));
		}

		internal GraphicsBuffer GetGraphicsBuffer(string name)
		{
			return GetGraphicsBuffer(Shader.PropertyToID(name));
		}

		public bool GetBool(string name)
		{
			return GetBool(Shader.PropertyToID(name));
		}

		public AnimationCurve GetAnimationCurve(string name)
		{
			return GetAnimationCurve(Shader.PropertyToID(name));
		}

		public Gradient GetGradient(string name)
		{
			return GetGradient(Shader.PropertyToID(name));
		}

		public bool HasSystem(string name)
		{
			return HasSystem(Shader.PropertyToID(name));
		}

		public VFXParticleSystemInfo GetParticleSystemInfo(string name)
		{
			return GetParticleSystemInfo(Shader.PropertyToID(name));
		}

		public VFXSpawnerState GetSpawnSystemInfo(string name)
		{
			return GetSpawnSystemInfo(Shader.PropertyToID(name));
		}

		internal Bounds GetComputedBounds(string name)
		{
			return GetComputedBounds(Shader.PropertyToID(name));
		}

		internal Vector3 GetCurrentBoundsPadding(string name)
		{
			return GetCurrentBoundsPadding(Shader.PropertyToID(name));
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Simulate(float stepDeltaTime, uint stepCount = 1u);

		[RequiredByNativeCode]
		private static VFXEventAttribute InvokeGetCachedEventAttributeForOutputEvent_Internal(VisualEffect source)
		{
			if (source.outputEventReceived == null)
			{
				return null;
			}
			if (source.m_cachedEventAttribute == null)
			{
				source.m_cachedEventAttribute = source.CreateVFXEventAttribute();
			}
			return source.m_cachedEventAttribute;
		}

		[RequiredByNativeCode]
		private static void InvokeOutputEventReceived_Internal(VisualEffect source, int eventNameId)
		{
			VFXOutputEventArgs obj = new VFXOutputEventArgs(eventNameId, source.m_cachedEventAttribute);
			source.outputEventReceived(obj);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVector2_Injected(int nameID, ref Vector2 v);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVector3_Injected(int nameID, ref Vector3 v);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetVector4_Injected(int nameID, ref Vector4 v);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetMatrix4x4_Injected(int nameID, ref Matrix4x4 v);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVector2_Injected(int nameID, out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVector3_Injected(int nameID, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVector4_Injected(int nameID, out Vector4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetMatrix4x4_Injected(int nameID, out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetParticleSystemInfo_Injected(int nameID, out VFXParticleSystemInfo ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetComputedBounds_Injected(int nameID, out Bounds ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetCurrentBoundsPadding_Injected(int nameID, out Vector3 ret);
	}
	[RejectDragAndDropMaterial]
	[NativeType(Header = "Modules/VFX/Public/VFXRenderer.h")]
	[UsedByNativeCode]
	internal sealed class VFXRenderer : Renderer
	{
	}
	[NativeHeader("Modules/VFX/Public/Systems/VFXParticleSystem.h")]
	[UsedByNativeCode]
	public struct VFXParticleSystemInfo
	{
		public uint aliveCount;

		public uint capacity;

		public bool sleeping;

		public Bounds bounds;

		public VFXParticleSystemInfo(uint aliveCount, uint capacity, bool sleeping, Bounds bounds)
		{
			this.aliveCount = aliveCount;
			this.capacity = capacity;
			this.sleeping = sleeping;
			this.bounds = bounds;
		}
	}
}
