using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.ParticleSystemJobs;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

[assembly: InternalsVisibleTo("UnityEngine.UIElementsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIModule")]
[assembly: InternalsVisibleTo("UnityEngine.TilemapModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainPhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubsystemsModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubstanceModule")]
[assembly: InternalsVisibleTo("UnityEngine.StreamingModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteShapeModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteMaskModule")]
[assembly: InternalsVisibleTo("UnityEngine.ScreenCaptureModule")]
[assembly: InternalsVisibleTo("UnityEngine.RuntimeInitializeOnLoadManagerInitializerModule")]
[assembly: InternalsVisibleTo("UnityEngine.PropertiesModule")]
[assembly: InternalsVisibleTo("UnityEngine.ProfilerModule")]
[assembly: InternalsVisibleTo("UnityEngine.Physics2DModule")]
[assembly: InternalsVisibleTo("UnityEngine.PerformanceReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.UmbraModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityCurlModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityTestProtocolModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS5VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS5Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS4VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS4Module")]
[assembly: InternalsVisibleTo("UnityEngine.XboxOneModule")]
[assembly: InternalsVisibleTo("UnityEngine.SwitchModule")]
[assembly: InternalsVisibleTo("UnityEngine.WindModule")]
[assembly: InternalsVisibleTo("UnityEngine.ParticleSystemModule")]
[assembly: InternalsVisibleTo("UnityEngine.VirtualTexturingModule")]
[assembly: InternalsVisibleTo("UnityEngine.VehiclesModule")]
[assembly: InternalsVisibleTo("UnityEngine.VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.XRModule")]
[assembly: InternalsVisibleTo("UnityEngine.VFXModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestWWWModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestTextureModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.VideoModule")]
[assembly: InternalsVisibleTo("UnityEngine.NVIDIAModule")]
[assembly: InternalsVisibleTo("UnityEngine.LocalizationModule")]
[assembly: InternalsVisibleTo("UnityEngine.IMGUIModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.ClothModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterRendererModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreTextEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreFontEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputLegacyModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextRenderingModule")]
[assembly: InternalsVisibleTo("UnityEngine.GridModule")]
[assembly: InternalsVisibleTo("UnityEngine.GameCenterModule")]
[assembly: InternalsVisibleTo("UnityEngine.ImageConversionModule")]
[assembly: InternalsVisibleTo("UnityEngine.GIModule")]
[assembly: InternalsVisibleTo("UnityEngine.DirectorModule")]
[assembly: InternalsVisibleTo("UnityEngine.DSPGraphModule")]
[assembly: InternalsVisibleTo("UnityEngine.CrashReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestModule")]
[assembly: InternalsVisibleTo("UnityEngine.TLSModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommonModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityConnectModule")]
[assembly: InternalsVisibleTo("UnityEngine.ContentLoadModule")]
[assembly: InternalsVisibleTo("UnityEngine.Networking")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud.Service")]
[assembly: InternalsVisibleTo("Unity.Analytics")]
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
[assembly: InternalsVisibleTo("Unity.Networking.Transport")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Framework")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Timeline")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.UnityAnalytics")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests")]
[assembly: InternalsVisibleTo("Unity.PerformanceTests.RuntimeTestRunner.Tests")]
[assembly: InternalsVisibleTo("Unity.DeploymentTests.Services")]
[assembly: InternalsVisibleTo("Unity.Burst")]
[assembly: InternalsVisibleTo("Unity.Automation")]
[assembly: InternalsVisibleTo("UnityEngine.TestRunner")]
[assembly: InternalsVisibleTo("UnityEngine.Purchasing")]
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommon")]
[assembly: InternalsVisibleTo("UnityEngine.Analytics")]
[assembly: InternalsVisibleTo("Unity.Burst.Editor")]
[assembly: InternalsVisibleTo("Unity.ucg.QoS")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.AllIn1Runner")]
[assembly: InternalsVisibleTo("Assembly-CSharp-testable")]
[assembly: InternalsVisibleTo("UnityEngine.UI")]
[assembly: InternalsVisibleTo("Unity.UIElements.EditorTests")]
[assembly: InternalsVisibleTo("UnityEngine.UIElements.Tests")]
[assembly: InternalsVisibleTo("Unity.UIElements.PlayModeTests")]
[assembly: InternalsVisibleTo("Unity.UIElements.Editor")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsGameObjectsModule")]
[assembly: InternalsVisibleTo("Unity.UIElements")]
[assembly: InternalsVisibleTo("Unity.Timeline")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.EditorTests")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.Editor")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.EditorTests")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.Editor")]
[assembly: InternalsVisibleTo("Unity.WindowsMRAutomation")]
[assembly: InternalsVisibleTo("GoogleAR.UnityNative")]
[assembly: InternalsVisibleTo("UnityEngine.SpatialTracking")]
[assembly: InternalsVisibleTo("Assembly-CSharp-firstpass-testable")]
[assembly: InternalsVisibleTo("UnityEditor.UIBuilderModule")]
[assembly: UnityEngineModuleAssembly]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace UnityEngine
{
	[Obsolete("ParticleSystemEmissionType no longer does anything. Time and Distance based emission are now both always active.", false)]
	public enum ParticleSystemEmissionType
	{
		Time,
		Distance
	}
	[NativeHeader("ParticleSystemScriptingClasses.h")]
	[NativeHeader("Modules/ParticleSystem/ParticleSystemGeometryJob.h")]
	[NativeHeader("Modules/ParticleSystem/ScriptBindings/ParticleSystemScriptBindings.h")]
	[UsedByNativeCode]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("ParticleSystemScriptingClasses.h")]
	[NativeHeader("Modules/ParticleSystem/ParticleSystem.h")]
	[NativeHeader("Modules/ParticleSystem/ScriptBindings/ParticleSystemScriptBindings.h")]
	[NativeHeader("Modules/ParticleSystem/ScriptBindings/ParticleSystemModulesScriptBindings.h")]
	[NativeHeader("Modules/ParticleSystem/ParticleSystem.h")]
	public sealed class ParticleSystem : Component
	{
		public struct MainModule
		{
			internal ParticleSystem m_ParticleSystem;

			[Obsolete("Please use flipRotation instead. (UnityUpgradable) -> UnityEngine.ParticleSystem/MainModule.flipRotation", false)]
			public float randomizeRotationDirection
			{
				get
				{
					return flipRotation;
				}
				set
				{
					flipRotation = value;
				}
			}

			public Vector3 emitterVelocity
			{
				get
				{
					get_emitterVelocity_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_emitterVelocity_Injected(ref this, ref value);
				}
			}

			public float duration
			{
				get
				{
					return get_duration_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_duration_Injected(ref this, value);
				}
			}

			public bool loop
			{
				get
				{
					return get_loop_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_loop_Injected(ref this, value);
				}
			}

			public bool prewarm
			{
				get
				{
					return get_prewarm_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_prewarm_Injected(ref this, value);
				}
			}

			public MinMaxCurve startDelay
			{
				get
				{
					get_startDelay_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_startDelay_Injected(ref this, ref value);
				}
			}

			public float startDelayMultiplier
			{
				get
				{
					return get_startDelayMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_startDelayMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve startLifetime
			{
				get
				{
					get_startLifetime_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_startLifetime_Injected(ref this, ref value);
				}
			}

			public float startLifetimeMultiplier
			{
				get
				{
					return get_startLifetimeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_startLifetimeMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve startSpeed
			{
				get
				{
					get_startSpeed_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_startSpeed_Injected(ref this, ref value);
				}
			}

			public float startSpeedMultiplier
			{
				get
				{
					return get_startSpeedMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_startSpeedMultiplier_Injected(ref this, value);
				}
			}

			public bool startSize3D
			{
				get
				{
					return get_startSize3D_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_startSize3D_Injected(ref this, value);
				}
			}

			[NativeName("StartSizeX")]
			public MinMaxCurve startSize
			{
				get
				{
					get_startSize_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_startSize_Injected(ref this, ref value);
				}
			}

			[NativeName("StartSizeXMultiplier")]
			public float startSizeMultiplier
			{
				get
				{
					return get_startSizeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_startSizeMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve startSizeX
			{
				get
				{
					get_startSizeX_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_startSizeX_Injected(ref this, ref value);
				}
			}

			public float startSizeXMultiplier
			{
				get
				{
					return get_startSizeXMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_startSizeXMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve startSizeY
			{
				get
				{
					get_startSizeY_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_startSizeY_Injected(ref this, ref value);
				}
			}

			public float startSizeYMultiplier
			{
				get
				{
					return get_startSizeYMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_startSizeYMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve startSizeZ
			{
				get
				{
					get_startSizeZ_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_startSizeZ_Injected(ref this, ref value);
				}
			}

			public float startSizeZMultiplier
			{
				get
				{
					return get_startSizeZMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_startSizeZMultiplier_Injected(ref this, value);
				}
			}

			public bool startRotation3D
			{
				get
				{
					return get_startRotation3D_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_startRotation3D_Injected(ref this, value);
				}
			}

			[NativeName("StartRotationZ")]
			public MinMaxCurve startRotation
			{
				get
				{
					get_startRotation_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_startRotation_Injected(ref this, ref value);
				}
			}

			[NativeName("StartRotationZMultiplier")]
			public float startRotationMultiplier
			{
				get
				{
					return get_startRotationMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_startRotationMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve startRotationX
			{
				get
				{
					get_startRotationX_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_startRotationX_Injected(ref this, ref value);
				}
			}

			public float startRotationXMultiplier
			{
				get
				{
					return get_startRotationXMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_startRotationXMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve startRotationY
			{
				get
				{
					get_startRotationY_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_startRotationY_Injected(ref this, ref value);
				}
			}

			public float startRotationYMultiplier
			{
				get
				{
					return get_startRotationYMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_startRotationYMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve startRotationZ
			{
				get
				{
					get_startRotationZ_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_startRotationZ_Injected(ref this, ref value);
				}
			}

			public float startRotationZMultiplier
			{
				get
				{
					return get_startRotationZMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_startRotationZMultiplier_Injected(ref this, value);
				}
			}

			public float flipRotation
			{
				get
				{
					return get_flipRotation_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_flipRotation_Injected(ref this, value);
				}
			}

			public MinMaxGradient startColor
			{
				get
				{
					get_startColor_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_startColor_Injected(ref this, ref value);
				}
			}

			public ParticleSystemGravitySource gravitySource
			{
				get
				{
					return get_gravitySource_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_gravitySource_Injected(ref this, value);
				}
			}

			public MinMaxCurve gravityModifier
			{
				get
				{
					get_gravityModifier_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_gravityModifier_Injected(ref this, ref value);
				}
			}

			public float gravityModifierMultiplier
			{
				get
				{
					return get_gravityModifierMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_gravityModifierMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystemSimulationSpace simulationSpace
			{
				get
				{
					return get_simulationSpace_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_simulationSpace_Injected(ref this, value);
				}
			}

			public Transform customSimulationSpace
			{
				get
				{
					return get_customSimulationSpace_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_customSimulationSpace_Injected(ref this, value);
				}
			}

			public float simulationSpeed
			{
				get
				{
					return get_simulationSpeed_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_simulationSpeed_Injected(ref this, value);
				}
			}

			public bool useUnscaledTime
			{
				get
				{
					return get_useUnscaledTime_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_useUnscaledTime_Injected(ref this, value);
				}
			}

			public ParticleSystemScalingMode scalingMode
			{
				get
				{
					return get_scalingMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_scalingMode_Injected(ref this, value);
				}
			}

			public bool playOnAwake
			{
				get
				{
					return get_playOnAwake_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_playOnAwake_Injected(ref this, value);
				}
			}

			public int maxParticles
			{
				get
				{
					return get_maxParticles_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_maxParticles_Injected(ref this, value);
				}
			}

			public ParticleSystemEmitterVelocityMode emitterVelocityMode
			{
				get
				{
					return get_emitterVelocityMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_emitterVelocityMode_Injected(ref this, value);
				}
			}

			public ParticleSystemStopAction stopAction
			{
				get
				{
					return get_stopAction_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_stopAction_Injected(ref this, value);
				}
			}

			public ParticleSystemRingBufferMode ringBufferMode
			{
				get
				{
					return get_ringBufferMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_ringBufferMode_Injected(ref this, value);
				}
			}

			public Vector2 ringBufferLoopRange
			{
				get
				{
					get_ringBufferLoopRange_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_ringBufferLoopRange_Injected(ref this, ref value);
				}
			}

			public ParticleSystemCullingMode cullingMode
			{
				get
				{
					return get_cullingMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_cullingMode_Injected(ref this, value);
				}
			}

			internal MainModule(ParticleSystem particleSystem)
			{
				m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_emitterVelocity_Injected(ref MainModule _unity_self, out Vector3 ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_emitterVelocity_Injected(ref MainModule _unity_self, ref Vector3 value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_duration_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_duration_Injected(ref MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_loop_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_loop_Injected(ref MainModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_prewarm_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_prewarm_Injected(ref MainModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_startDelay_Injected(ref MainModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_startDelay_Injected(ref MainModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_startDelayMultiplier_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_startDelayMultiplier_Injected(ref MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_startLifetime_Injected(ref MainModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_startLifetime_Injected(ref MainModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_startLifetimeMultiplier_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_startLifetimeMultiplier_Injected(ref MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_startSpeed_Injected(ref MainModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_startSpeed_Injected(ref MainModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_startSpeedMultiplier_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_startSpeedMultiplier_Injected(ref MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_startSize3D_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_startSize3D_Injected(ref MainModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_startSize_Injected(ref MainModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_startSize_Injected(ref MainModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_startSizeMultiplier_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_startSizeMultiplier_Injected(ref MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_startSizeX_Injected(ref MainModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_startSizeX_Injected(ref MainModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_startSizeXMultiplier_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_startSizeXMultiplier_Injected(ref MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_startSizeY_Injected(ref MainModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_startSizeY_Injected(ref MainModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_startSizeYMultiplier_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_startSizeYMultiplier_Injected(ref MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_startSizeZ_Injected(ref MainModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_startSizeZ_Injected(ref MainModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_startSizeZMultiplier_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_startSizeZMultiplier_Injected(ref MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_startRotation3D_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_startRotation3D_Injected(ref MainModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_startRotation_Injected(ref MainModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_startRotation_Injected(ref MainModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_startRotationMultiplier_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_startRotationMultiplier_Injected(ref MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_startRotationX_Injected(ref MainModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_startRotationX_Injected(ref MainModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_startRotationXMultiplier_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_startRotationXMultiplier_Injected(ref MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_startRotationY_Injected(ref MainModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_startRotationY_Injected(ref MainModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_startRotationYMultiplier_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_startRotationYMultiplier_Injected(ref MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_startRotationZ_Injected(ref MainModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_startRotationZ_Injected(ref MainModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_startRotationZMultiplier_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_startRotationZMultiplier_Injected(ref MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_flipRotation_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_flipRotation_Injected(ref MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_startColor_Injected(ref MainModule _unity_self, out MinMaxGradient ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_startColor_Injected(ref MainModule _unity_self, ref MinMaxGradient value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemGravitySource get_gravitySource_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_gravitySource_Injected(ref MainModule _unity_self, ParticleSystemGravitySource value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_gravityModifier_Injected(ref MainModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_gravityModifier_Injected(ref MainModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_gravityModifierMultiplier_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_gravityModifierMultiplier_Injected(ref MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemSimulationSpace get_simulationSpace_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_simulationSpace_Injected(ref MainModule _unity_self, ParticleSystemSimulationSpace value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern Transform get_customSimulationSpace_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_customSimulationSpace_Injected(ref MainModule _unity_self, Transform value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_simulationSpeed_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_simulationSpeed_Injected(ref MainModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_useUnscaledTime_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_useUnscaledTime_Injected(ref MainModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemScalingMode get_scalingMode_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_scalingMode_Injected(ref MainModule _unity_self, ParticleSystemScalingMode value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_playOnAwake_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_playOnAwake_Injected(ref MainModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern int get_maxParticles_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_maxParticles_Injected(ref MainModule _unity_self, int value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemEmitterVelocityMode get_emitterVelocityMode_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_emitterVelocityMode_Injected(ref MainModule _unity_self, ParticleSystemEmitterVelocityMode value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemStopAction get_stopAction_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_stopAction_Injected(ref MainModule _unity_self, ParticleSystemStopAction value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemRingBufferMode get_ringBufferMode_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_ringBufferMode_Injected(ref MainModule _unity_self, ParticleSystemRingBufferMode value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_ringBufferLoopRange_Injected(ref MainModule _unity_self, out Vector2 ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_ringBufferLoopRange_Injected(ref MainModule _unity_self, ref Vector2 value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemCullingMode get_cullingMode_Injected(ref MainModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_cullingMode_Injected(ref MainModule _unity_self, ParticleSystemCullingMode value);
		}

		public struct EmissionModule
		{
			internal ParticleSystem m_ParticleSystem;

			[Obsolete("ParticleSystemEmissionType no longer does anything. Time and Distance based emission are now both always active.", false)]
			public ParticleSystemEmissionType type
			{
				get
				{
					return ParticleSystemEmissionType.Time;
				}
				set
				{
				}
			}

			[Obsolete("rate property is deprecated. Use rateOverTime or rateOverDistance instead.", false)]
			public MinMaxCurve rate
			{
				get
				{
					return rateOverTime;
				}
				set
				{
					rateOverTime = value;
				}
			}

			[Obsolete("rateMultiplier property is deprecated. Use rateOverTimeMultiplier or rateOverDistanceMultiplier instead.", false)]
			public float rateMultiplier
			{
				get
				{
					return rateOverTimeMultiplier;
				}
				set
				{
					rateOverTimeMultiplier = value;
				}
			}

			public bool enabled
			{
				get
				{
					return get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_enabled_Injected(ref this, value);
				}
			}

			public MinMaxCurve rateOverTime
			{
				get
				{
					get_rateOverTime_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_rateOverTime_Injected(ref this, ref value);
				}
			}

			public float rateOverTimeMultiplier
			{
				get
				{
					return get_rateOverTimeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_rateOverTimeMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve rateOverDistance
			{
				get
				{
					get_rateOverDistance_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_rateOverDistance_Injected(ref this, ref value);
				}
			}

			public float rateOverDistanceMultiplier
			{
				get
				{
					return get_rateOverDistanceMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_rateOverDistanceMultiplier_Injected(ref this, value);
				}
			}

			public int burstCount
			{
				get
				{
					return get_burstCount_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_burstCount_Injected(ref this, value);
				}
			}

			internal EmissionModule(ParticleSystem particleSystem)
			{
				m_ParticleSystem = particleSystem;
			}

			public void SetBursts(Burst[] bursts)
			{
				SetBursts(bursts, bursts.Length);
			}

			public void SetBursts(Burst[] bursts, int size)
			{
				burstCount = size;
				for (int i = 0; i < size; i++)
				{
					SetBurst(i, bursts[i]);
				}
			}

			public int GetBursts(Burst[] bursts)
			{
				int num = burstCount;
				for (int i = 0; i < num; i++)
				{
					bursts[i] = GetBurst(i);
				}
				return num;
			}

			[NativeThrows]
			public void SetBurst(int index, Burst burst)
			{
				SetBurst_Injected(ref this, index, ref burst);
			}

			[NativeThrows]
			public Burst GetBurst(int index)
			{
				GetBurst_Injected(ref this, index, out var ret);
				return ret;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_enabled_Injected(ref EmissionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_enabled_Injected(ref EmissionModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_rateOverTime_Injected(ref EmissionModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_rateOverTime_Injected(ref EmissionModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_rateOverTimeMultiplier_Injected(ref EmissionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_rateOverTimeMultiplier_Injected(ref EmissionModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_rateOverDistance_Injected(ref EmissionModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_rateOverDistance_Injected(ref EmissionModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_rateOverDistanceMultiplier_Injected(ref EmissionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_rateOverDistanceMultiplier_Injected(ref EmissionModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetBurst_Injected(ref EmissionModule _unity_self, int index, ref Burst burst);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetBurst_Injected(ref EmissionModule _unity_self, int index, out Burst ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern int get_burstCount_Injected(ref EmissionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_burstCount_Injected(ref EmissionModule _unity_self, int value);
		}

		public struct ShapeModule
		{
			internal ParticleSystem m_ParticleSystem;

			[Obsolete("Please use scale instead. (UnityUpgradable) -> UnityEngine.ParticleSystem/ShapeModule.scale", false)]
			public Vector3 box
			{
				get
				{
					return scale;
				}
				set
				{
					scale = value;
				}
			}

			[Obsolete("meshScale property is deprecated.Please use scale instead.", false)]
			public float meshScale
			{
				get
				{
					return scale.x;
				}
				set
				{
					scale = new Vector3(value, value, value);
				}
			}

			[Obsolete("randomDirection property is deprecated. Use randomDirectionAmount instead.", false)]
			public bool randomDirection
			{
				get
				{
					return randomDirectionAmount >= 0.5f;
				}
				set
				{
					randomDirectionAmount = (value ? 1f : 0f);
				}
			}

			public bool enabled
			{
				get
				{
					return get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_enabled_Injected(ref this, value);
				}
			}

			public ParticleSystemShapeType shapeType
			{
				get
				{
					return get_shapeType_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_shapeType_Injected(ref this, value);
				}
			}

			public float randomDirectionAmount
			{
				get
				{
					return get_randomDirectionAmount_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_randomDirectionAmount_Injected(ref this, value);
				}
			}

			public float sphericalDirectionAmount
			{
				get
				{
					return get_sphericalDirectionAmount_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_sphericalDirectionAmount_Injected(ref this, value);
				}
			}

			public float randomPositionAmount
			{
				get
				{
					return get_randomPositionAmount_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_randomPositionAmount_Injected(ref this, value);
				}
			}

			public bool alignToDirection
			{
				get
				{
					return get_alignToDirection_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_alignToDirection_Injected(ref this, value);
				}
			}

			public float radius
			{
				get
				{
					return get_radius_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_radius_Injected(ref this, value);
				}
			}

			public ParticleSystemShapeMultiModeValue radiusMode
			{
				get
				{
					return get_radiusMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_radiusMode_Injected(ref this, value);
				}
			}

			public float radiusSpread
			{
				get
				{
					return get_radiusSpread_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_radiusSpread_Injected(ref this, value);
				}
			}

			public MinMaxCurve radiusSpeed
			{
				get
				{
					get_radiusSpeed_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_radiusSpeed_Injected(ref this, ref value);
				}
			}

			public float radiusSpeedMultiplier
			{
				get
				{
					return get_radiusSpeedMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_radiusSpeedMultiplier_Injected(ref this, value);
				}
			}

			public float radiusThickness
			{
				get
				{
					return get_radiusThickness_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_radiusThickness_Injected(ref this, value);
				}
			}

			public float angle
			{
				get
				{
					return get_angle_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_angle_Injected(ref this, value);
				}
			}

			public float length
			{
				get
				{
					return get_length_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_length_Injected(ref this, value);
				}
			}

			public Vector3 boxThickness
			{
				get
				{
					get_boxThickness_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_boxThickness_Injected(ref this, ref value);
				}
			}

			public ParticleSystemMeshShapeType meshShapeType
			{
				get
				{
					return get_meshShapeType_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_meshShapeType_Injected(ref this, value);
				}
			}

			public Mesh mesh
			{
				get
				{
					return get_mesh_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_mesh_Injected(ref this, value);
				}
			}

			public MeshRenderer meshRenderer
			{
				get
				{
					return get_meshRenderer_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_meshRenderer_Injected(ref this, value);
				}
			}

			public SkinnedMeshRenderer skinnedMeshRenderer
			{
				get
				{
					return get_skinnedMeshRenderer_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_skinnedMeshRenderer_Injected(ref this, value);
				}
			}

			public Sprite sprite
			{
				get
				{
					return get_sprite_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_sprite_Injected(ref this, value);
				}
			}

			public SpriteRenderer spriteRenderer
			{
				get
				{
					return get_spriteRenderer_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_spriteRenderer_Injected(ref this, value);
				}
			}

			public bool useMeshMaterialIndex
			{
				get
				{
					return get_useMeshMaterialIndex_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_useMeshMaterialIndex_Injected(ref this, value);
				}
			}

			public int meshMaterialIndex
			{
				get
				{
					return get_meshMaterialIndex_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_meshMaterialIndex_Injected(ref this, value);
				}
			}

			public bool useMeshColors
			{
				get
				{
					return get_useMeshColors_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_useMeshColors_Injected(ref this, value);
				}
			}

			public float normalOffset
			{
				get
				{
					return get_normalOffset_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_normalOffset_Injected(ref this, value);
				}
			}

			public ParticleSystemShapeMultiModeValue meshSpawnMode
			{
				get
				{
					return get_meshSpawnMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_meshSpawnMode_Injected(ref this, value);
				}
			}

			public float meshSpawnSpread
			{
				get
				{
					return get_meshSpawnSpread_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_meshSpawnSpread_Injected(ref this, value);
				}
			}

			public MinMaxCurve meshSpawnSpeed
			{
				get
				{
					get_meshSpawnSpeed_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_meshSpawnSpeed_Injected(ref this, ref value);
				}
			}

			public float meshSpawnSpeedMultiplier
			{
				get
				{
					return get_meshSpawnSpeedMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_meshSpawnSpeedMultiplier_Injected(ref this, value);
				}
			}

			public float arc
			{
				get
				{
					return get_arc_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_arc_Injected(ref this, value);
				}
			}

			public ParticleSystemShapeMultiModeValue arcMode
			{
				get
				{
					return get_arcMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_arcMode_Injected(ref this, value);
				}
			}

			public float arcSpread
			{
				get
				{
					return get_arcSpread_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_arcSpread_Injected(ref this, value);
				}
			}

			public MinMaxCurve arcSpeed
			{
				get
				{
					get_arcSpeed_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_arcSpeed_Injected(ref this, ref value);
				}
			}

			public float arcSpeedMultiplier
			{
				get
				{
					return get_arcSpeedMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_arcSpeedMultiplier_Injected(ref this, value);
				}
			}

			public float donutRadius
			{
				get
				{
					return get_donutRadius_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_donutRadius_Injected(ref this, value);
				}
			}

			public Vector3 position
			{
				get
				{
					get_position_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_position_Injected(ref this, ref value);
				}
			}

			public Vector3 rotation
			{
				get
				{
					get_rotation_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_rotation_Injected(ref this, ref value);
				}
			}

			public Vector3 scale
			{
				get
				{
					get_scale_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_scale_Injected(ref this, ref value);
				}
			}

			public Texture2D texture
			{
				get
				{
					return get_texture_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_texture_Injected(ref this, value);
				}
			}

			public ParticleSystemShapeTextureChannel textureClipChannel
			{
				get
				{
					return get_textureClipChannel_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_textureClipChannel_Injected(ref this, value);
				}
			}

			public float textureClipThreshold
			{
				get
				{
					return get_textureClipThreshold_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_textureClipThreshold_Injected(ref this, value);
				}
			}

			public bool textureColorAffectsParticles
			{
				get
				{
					return get_textureColorAffectsParticles_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_textureColorAffectsParticles_Injected(ref this, value);
				}
			}

			public bool textureAlphaAffectsParticles
			{
				get
				{
					return get_textureAlphaAffectsParticles_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_textureAlphaAffectsParticles_Injected(ref this, value);
				}
			}

			public bool textureBilinearFiltering
			{
				get
				{
					return get_textureBilinearFiltering_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_textureBilinearFiltering_Injected(ref this, value);
				}
			}

			public int textureUVChannel
			{
				get
				{
					return get_textureUVChannel_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_textureUVChannel_Injected(ref this, value);
				}
			}

			internal ShapeModule(ParticleSystem particleSystem)
			{
				m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_enabled_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_enabled_Injected(ref ShapeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemShapeType get_shapeType_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_shapeType_Injected(ref ShapeModule _unity_self, ParticleSystemShapeType value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_randomDirectionAmount_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_randomDirectionAmount_Injected(ref ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_sphericalDirectionAmount_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_sphericalDirectionAmount_Injected(ref ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_randomPositionAmount_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_randomPositionAmount_Injected(ref ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_alignToDirection_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_alignToDirection_Injected(ref ShapeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_radius_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_radius_Injected(ref ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemShapeMultiModeValue get_radiusMode_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_radiusMode_Injected(ref ShapeModule _unity_self, ParticleSystemShapeMultiModeValue value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_radiusSpread_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_radiusSpread_Injected(ref ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_radiusSpeed_Injected(ref ShapeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_radiusSpeed_Injected(ref ShapeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_radiusSpeedMultiplier_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_radiusSpeedMultiplier_Injected(ref ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_radiusThickness_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_radiusThickness_Injected(ref ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_angle_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_angle_Injected(ref ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_length_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_length_Injected(ref ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_boxThickness_Injected(ref ShapeModule _unity_self, out Vector3 ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_boxThickness_Injected(ref ShapeModule _unity_self, ref Vector3 value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemMeshShapeType get_meshShapeType_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_meshShapeType_Injected(ref ShapeModule _unity_self, ParticleSystemMeshShapeType value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern Mesh get_mesh_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_mesh_Injected(ref ShapeModule _unity_self, Mesh value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern MeshRenderer get_meshRenderer_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_meshRenderer_Injected(ref ShapeModule _unity_self, MeshRenderer value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern SkinnedMeshRenderer get_skinnedMeshRenderer_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_skinnedMeshRenderer_Injected(ref ShapeModule _unity_self, SkinnedMeshRenderer value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern Sprite get_sprite_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_sprite_Injected(ref ShapeModule _unity_self, Sprite value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern SpriteRenderer get_spriteRenderer_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_spriteRenderer_Injected(ref ShapeModule _unity_self, SpriteRenderer value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_useMeshMaterialIndex_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_useMeshMaterialIndex_Injected(ref ShapeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern int get_meshMaterialIndex_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_meshMaterialIndex_Injected(ref ShapeModule _unity_self, int value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_useMeshColors_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_useMeshColors_Injected(ref ShapeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_normalOffset_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_normalOffset_Injected(ref ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemShapeMultiModeValue get_meshSpawnMode_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_meshSpawnMode_Injected(ref ShapeModule _unity_self, ParticleSystemShapeMultiModeValue value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_meshSpawnSpread_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_meshSpawnSpread_Injected(ref ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_meshSpawnSpeed_Injected(ref ShapeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_meshSpawnSpeed_Injected(ref ShapeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_meshSpawnSpeedMultiplier_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_meshSpawnSpeedMultiplier_Injected(ref ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_arc_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_arc_Injected(ref ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemShapeMultiModeValue get_arcMode_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_arcMode_Injected(ref ShapeModule _unity_self, ParticleSystemShapeMultiModeValue value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_arcSpread_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_arcSpread_Injected(ref ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_arcSpeed_Injected(ref ShapeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_arcSpeed_Injected(ref ShapeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_arcSpeedMultiplier_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_arcSpeedMultiplier_Injected(ref ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_donutRadius_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_donutRadius_Injected(ref ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_position_Injected(ref ShapeModule _unity_self, out Vector3 ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_position_Injected(ref ShapeModule _unity_self, ref Vector3 value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_rotation_Injected(ref ShapeModule _unity_self, out Vector3 ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_rotation_Injected(ref ShapeModule _unity_self, ref Vector3 value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_scale_Injected(ref ShapeModule _unity_self, out Vector3 ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_scale_Injected(ref ShapeModule _unity_self, ref Vector3 value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern Texture2D get_texture_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_texture_Injected(ref ShapeModule _unity_self, Texture2D value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemShapeTextureChannel get_textureClipChannel_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_textureClipChannel_Injected(ref ShapeModule _unity_self, ParticleSystemShapeTextureChannel value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_textureClipThreshold_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_textureClipThreshold_Injected(ref ShapeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_textureColorAffectsParticles_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_textureColorAffectsParticles_Injected(ref ShapeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_textureAlphaAffectsParticles_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_textureAlphaAffectsParticles_Injected(ref ShapeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_textureBilinearFiltering_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_textureBilinearFiltering_Injected(ref ShapeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern int get_textureUVChannel_Injected(ref ShapeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_textureUVChannel_Injected(ref ShapeModule _unity_self, int value);
		}

		public struct CollisionModule
		{
			internal ParticleSystem m_ParticleSystem;

			[Obsolete("The maxPlaneCount restriction has been removed. Please use planeCount instead to find out how many planes there are. (UnityUpgradable) -> UnityEngine.ParticleSystem/CollisionModule.planeCount", false)]
			public int maxPlaneCount => planeCount;

			public bool enabled
			{
				get
				{
					return get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_enabled_Injected(ref this, value);
				}
			}

			public ParticleSystemCollisionType type
			{
				get
				{
					return get_type_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_type_Injected(ref this, value);
				}
			}

			public ParticleSystemCollisionMode mode
			{
				get
				{
					return get_mode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_mode_Injected(ref this, value);
				}
			}

			public MinMaxCurve dampen
			{
				get
				{
					get_dampen_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_dampen_Injected(ref this, ref value);
				}
			}

			public float dampenMultiplier
			{
				get
				{
					return get_dampenMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_dampenMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve bounce
			{
				get
				{
					get_bounce_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_bounce_Injected(ref this, ref value);
				}
			}

			public float bounceMultiplier
			{
				get
				{
					return get_bounceMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_bounceMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve lifetimeLoss
			{
				get
				{
					get_lifetimeLoss_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_lifetimeLoss_Injected(ref this, ref value);
				}
			}

			public float lifetimeLossMultiplier
			{
				get
				{
					return get_lifetimeLossMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_lifetimeLossMultiplier_Injected(ref this, value);
				}
			}

			public float minKillSpeed
			{
				get
				{
					return get_minKillSpeed_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_minKillSpeed_Injected(ref this, value);
				}
			}

			public float maxKillSpeed
			{
				get
				{
					return get_maxKillSpeed_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_maxKillSpeed_Injected(ref this, value);
				}
			}

			public LayerMask collidesWith
			{
				get
				{
					get_collidesWith_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_collidesWith_Injected(ref this, ref value);
				}
			}

			public bool enableDynamicColliders
			{
				get
				{
					return get_enableDynamicColliders_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_enableDynamicColliders_Injected(ref this, value);
				}
			}

			public int maxCollisionShapes
			{
				get
				{
					return get_maxCollisionShapes_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_maxCollisionShapes_Injected(ref this, value);
				}
			}

			public ParticleSystemCollisionQuality quality
			{
				get
				{
					return get_quality_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_quality_Injected(ref this, value);
				}
			}

			public float voxelSize
			{
				get
				{
					return get_voxelSize_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_voxelSize_Injected(ref this, value);
				}
			}

			public float radiusScale
			{
				get
				{
					return get_radiusScale_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_radiusScale_Injected(ref this, value);
				}
			}

			public bool sendCollisionMessages
			{
				get
				{
					return get_sendCollisionMessages_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_sendCollisionMessages_Injected(ref this, value);
				}
			}

			public float colliderForce
			{
				get
				{
					return get_colliderForce_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_colliderForce_Injected(ref this, value);
				}
			}

			public bool multiplyColliderForceByCollisionAngle
			{
				get
				{
					return get_multiplyColliderForceByCollisionAngle_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_multiplyColliderForceByCollisionAngle_Injected(ref this, value);
				}
			}

			public bool multiplyColliderForceByParticleSpeed
			{
				get
				{
					return get_multiplyColliderForceByParticleSpeed_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_multiplyColliderForceByParticleSpeed_Injected(ref this, value);
				}
			}

			public bool multiplyColliderForceByParticleSize
			{
				get
				{
					return get_multiplyColliderForceByParticleSize_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_multiplyColliderForceByParticleSize_Injected(ref this, value);
				}
			}

			[NativeThrows]
			public int planeCount => get_planeCount_Injected(ref this);

			[Obsolete("enableInteriorCollisions property is deprecated and is no longer required and has no effect on the particle system.", false)]
			public bool enableInteriorCollisions
			{
				get
				{
					return get_enableInteriorCollisions_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_enableInteriorCollisions_Injected(ref this, value);
				}
			}

			internal CollisionModule(ParticleSystem particleSystem)
			{
				m_ParticleSystem = particleSystem;
			}

			[NativeThrows]
			public void AddPlane(Transform transform)
			{
				AddPlane_Injected(ref this, transform);
			}

			[NativeThrows]
			public void RemovePlane(int index)
			{
				RemovePlane_Injected(ref this, index);
			}

			public void RemovePlane(Transform transform)
			{
				RemovePlaneObject(transform);
			}

			[NativeThrows]
			private void RemovePlaneObject(Transform transform)
			{
				RemovePlaneObject_Injected(ref this, transform);
			}

			[NativeThrows]
			public void SetPlane(int index, Transform transform)
			{
				SetPlane_Injected(ref this, index, transform);
			}

			[NativeThrows]
			public Transform GetPlane(int index)
			{
				return GetPlane_Injected(ref this, index);
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_enabled_Injected(ref CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_enabled_Injected(ref CollisionModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemCollisionType get_type_Injected(ref CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_type_Injected(ref CollisionModule _unity_self, ParticleSystemCollisionType value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemCollisionMode get_mode_Injected(ref CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_mode_Injected(ref CollisionModule _unity_self, ParticleSystemCollisionMode value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_dampen_Injected(ref CollisionModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_dampen_Injected(ref CollisionModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_dampenMultiplier_Injected(ref CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_dampenMultiplier_Injected(ref CollisionModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_bounce_Injected(ref CollisionModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_bounce_Injected(ref CollisionModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_bounceMultiplier_Injected(ref CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_bounceMultiplier_Injected(ref CollisionModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_lifetimeLoss_Injected(ref CollisionModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_lifetimeLoss_Injected(ref CollisionModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_lifetimeLossMultiplier_Injected(ref CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_lifetimeLossMultiplier_Injected(ref CollisionModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_minKillSpeed_Injected(ref CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_minKillSpeed_Injected(ref CollisionModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_maxKillSpeed_Injected(ref CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_maxKillSpeed_Injected(ref CollisionModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_collidesWith_Injected(ref CollisionModule _unity_self, out LayerMask ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_collidesWith_Injected(ref CollisionModule _unity_self, ref LayerMask value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_enableDynamicColliders_Injected(ref CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_enableDynamicColliders_Injected(ref CollisionModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern int get_maxCollisionShapes_Injected(ref CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_maxCollisionShapes_Injected(ref CollisionModule _unity_self, int value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemCollisionQuality get_quality_Injected(ref CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_quality_Injected(ref CollisionModule _unity_self, ParticleSystemCollisionQuality value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_voxelSize_Injected(ref CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_voxelSize_Injected(ref CollisionModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_radiusScale_Injected(ref CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_radiusScale_Injected(ref CollisionModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_sendCollisionMessages_Injected(ref CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_sendCollisionMessages_Injected(ref CollisionModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_colliderForce_Injected(ref CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_colliderForce_Injected(ref CollisionModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_multiplyColliderForceByCollisionAngle_Injected(ref CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_multiplyColliderForceByCollisionAngle_Injected(ref CollisionModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_multiplyColliderForceByParticleSpeed_Injected(ref CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_multiplyColliderForceByParticleSpeed_Injected(ref CollisionModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_multiplyColliderForceByParticleSize_Injected(ref CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_multiplyColliderForceByParticleSize_Injected(ref CollisionModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void AddPlane_Injected(ref CollisionModule _unity_self, Transform transform);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemovePlane_Injected(ref CollisionModule _unity_self, int index);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemovePlaneObject_Injected(ref CollisionModule _unity_self, Transform transform);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetPlane_Injected(ref CollisionModule _unity_self, int index, Transform transform);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern Transform GetPlane_Injected(ref CollisionModule _unity_self, int index);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern int get_planeCount_Injected(ref CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_enableInteriorCollisions_Injected(ref CollisionModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_enableInteriorCollisions_Injected(ref CollisionModule _unity_self, bool value);
		}

		public struct TriggerModule
		{
			internal ParticleSystem m_ParticleSystem;

			[Obsolete("The maxColliderCount restriction has been removed. Please use colliderCount instead to find out how many colliders there are. (UnityUpgradable) -> UnityEngine.ParticleSystem/TriggerModule.colliderCount", false)]
			public int maxColliderCount => colliderCount;

			public bool enabled
			{
				get
				{
					return get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_enabled_Injected(ref this, value);
				}
			}

			public ParticleSystemOverlapAction inside
			{
				get
				{
					return get_inside_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_inside_Injected(ref this, value);
				}
			}

			public ParticleSystemOverlapAction outside
			{
				get
				{
					return get_outside_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_outside_Injected(ref this, value);
				}
			}

			public ParticleSystemOverlapAction enter
			{
				get
				{
					return get_enter_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_enter_Injected(ref this, value);
				}
			}

			public ParticleSystemOverlapAction exit
			{
				get
				{
					return get_exit_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_exit_Injected(ref this, value);
				}
			}

			public ParticleSystemColliderQueryMode colliderQueryMode
			{
				get
				{
					return get_colliderQueryMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_colliderQueryMode_Injected(ref this, value);
				}
			}

			public float radiusScale
			{
				get
				{
					return get_radiusScale_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_radiusScale_Injected(ref this, value);
				}
			}

			[NativeThrows]
			public int colliderCount => get_colliderCount_Injected(ref this);

			internal TriggerModule(ParticleSystem particleSystem)
			{
				m_ParticleSystem = particleSystem;
			}

			[NativeThrows]
			public void AddCollider(Component collider)
			{
				AddCollider_Injected(ref this, collider);
			}

			[NativeThrows]
			public void RemoveCollider(int index)
			{
				RemoveCollider_Injected(ref this, index);
			}

			public void RemoveCollider(Component collider)
			{
				RemoveColliderObject(collider);
			}

			[NativeThrows]
			private void RemoveColliderObject(Component collider)
			{
				RemoveColliderObject_Injected(ref this, collider);
			}

			[NativeThrows]
			public void SetCollider(int index, Component collider)
			{
				SetCollider_Injected(ref this, index, collider);
			}

			[NativeThrows]
			public Component GetCollider(int index)
			{
				return GetCollider_Injected(ref this, index);
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_enabled_Injected(ref TriggerModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_enabled_Injected(ref TriggerModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemOverlapAction get_inside_Injected(ref TriggerModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_inside_Injected(ref TriggerModule _unity_self, ParticleSystemOverlapAction value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemOverlapAction get_outside_Injected(ref TriggerModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_outside_Injected(ref TriggerModule _unity_self, ParticleSystemOverlapAction value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemOverlapAction get_enter_Injected(ref TriggerModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_enter_Injected(ref TriggerModule _unity_self, ParticleSystemOverlapAction value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemOverlapAction get_exit_Injected(ref TriggerModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_exit_Injected(ref TriggerModule _unity_self, ParticleSystemOverlapAction value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemColliderQueryMode get_colliderQueryMode_Injected(ref TriggerModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_colliderQueryMode_Injected(ref TriggerModule _unity_self, ParticleSystemColliderQueryMode value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_radiusScale_Injected(ref TriggerModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_radiusScale_Injected(ref TriggerModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void AddCollider_Injected(ref TriggerModule _unity_self, Component collider);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemoveCollider_Injected(ref TriggerModule _unity_self, int index);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemoveColliderObject_Injected(ref TriggerModule _unity_self, Component collider);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetCollider_Injected(ref TriggerModule _unity_self, int index, Component collider);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern Component GetCollider_Injected(ref TriggerModule _unity_self, int index);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern int get_colliderCount_Injected(ref TriggerModule _unity_self);
		}

		public struct SubEmittersModule
		{
			internal ParticleSystem m_ParticleSystem;

			[Obsolete("birth0 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem birth0
			{
				get
				{
					ThrowNotImplemented();
					return null;
				}
				set
				{
					ThrowNotImplemented();
				}
			}

			[Obsolete("birth1 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem birth1
			{
				get
				{
					ThrowNotImplemented();
					return null;
				}
				set
				{
					ThrowNotImplemented();
				}
			}

			[Obsolete("collision0 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem collision0
			{
				get
				{
					ThrowNotImplemented();
					return null;
				}
				set
				{
					ThrowNotImplemented();
				}
			}

			[Obsolete("collision1 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem collision1
			{
				get
				{
					ThrowNotImplemented();
					return null;
				}
				set
				{
					ThrowNotImplemented();
				}
			}

			[Obsolete("death0 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem death0
			{
				get
				{
					ThrowNotImplemented();
					return null;
				}
				set
				{
					ThrowNotImplemented();
				}
			}

			[Obsolete("death1 property is deprecated. Use AddSubEmitter, RemoveSubEmitter, SetSubEmitterSystem and GetSubEmitterSystem instead.", false)]
			public ParticleSystem death1
			{
				get
				{
					ThrowNotImplemented();
					return null;
				}
				set
				{
					ThrowNotImplemented();
				}
			}

			public bool enabled
			{
				get
				{
					return get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_enabled_Injected(ref this, value);
				}
			}

			public int subEmittersCount => get_subEmittersCount_Injected(ref this);

			private static void ThrowNotImplemented()
			{
				throw new NotImplementedException();
			}

			internal SubEmittersModule(ParticleSystem particleSystem)
			{
				m_ParticleSystem = particleSystem;
			}

			[NativeThrows]
			public void AddSubEmitter(ParticleSystem subEmitter, ParticleSystemSubEmitterType type, ParticleSystemSubEmitterProperties properties, float emitProbability)
			{
				AddSubEmitter_Injected(ref this, subEmitter, type, properties, emitProbability);
			}

			public void AddSubEmitter(ParticleSystem subEmitter, ParticleSystemSubEmitterType type, ParticleSystemSubEmitterProperties properties)
			{
				AddSubEmitter(subEmitter, type, properties, 1f);
			}

			[NativeThrows]
			public void RemoveSubEmitter(int index)
			{
				RemoveSubEmitter_Injected(ref this, index);
			}

			public void RemoveSubEmitter(ParticleSystem subEmitter)
			{
				RemoveSubEmitterObject(subEmitter);
			}

			[NativeThrows]
			private void RemoveSubEmitterObject(ParticleSystem subEmitter)
			{
				RemoveSubEmitterObject_Injected(ref this, subEmitter);
			}

			[NativeThrows]
			public void SetSubEmitterSystem(int index, ParticleSystem subEmitter)
			{
				SetSubEmitterSystem_Injected(ref this, index, subEmitter);
			}

			[NativeThrows]
			public void SetSubEmitterType(int index, ParticleSystemSubEmitterType type)
			{
				SetSubEmitterType_Injected(ref this, index, type);
			}

			[NativeThrows]
			public void SetSubEmitterProperties(int index, ParticleSystemSubEmitterProperties properties)
			{
				SetSubEmitterProperties_Injected(ref this, index, properties);
			}

			[NativeThrows]
			public void SetSubEmitterEmitProbability(int index, float emitProbability)
			{
				SetSubEmitterEmitProbability_Injected(ref this, index, emitProbability);
			}

			[NativeThrows]
			public ParticleSystem GetSubEmitterSystem(int index)
			{
				return GetSubEmitterSystem_Injected(ref this, index);
			}

			[NativeThrows]
			public ParticleSystemSubEmitterType GetSubEmitterType(int index)
			{
				return GetSubEmitterType_Injected(ref this, index);
			}

			[NativeThrows]
			public ParticleSystemSubEmitterProperties GetSubEmitterProperties(int index)
			{
				return GetSubEmitterProperties_Injected(ref this, index);
			}

			[NativeThrows]
			public float GetSubEmitterEmitProbability(int index)
			{
				return GetSubEmitterEmitProbability_Injected(ref this, index);
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_enabled_Injected(ref SubEmittersModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_enabled_Injected(ref SubEmittersModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern int get_subEmittersCount_Injected(ref SubEmittersModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void AddSubEmitter_Injected(ref SubEmittersModule _unity_self, ParticleSystem subEmitter, ParticleSystemSubEmitterType type, ParticleSystemSubEmitterProperties properties, float emitProbability);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemoveSubEmitter_Injected(ref SubEmittersModule _unity_self, int index);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemoveSubEmitterObject_Injected(ref SubEmittersModule _unity_self, ParticleSystem subEmitter);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetSubEmitterSystem_Injected(ref SubEmittersModule _unity_self, int index, ParticleSystem subEmitter);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetSubEmitterType_Injected(ref SubEmittersModule _unity_self, int index, ParticleSystemSubEmitterType type);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetSubEmitterProperties_Injected(ref SubEmittersModule _unity_self, int index, ParticleSystemSubEmitterProperties properties);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetSubEmitterEmitProbability_Injected(ref SubEmittersModule _unity_self, int index, float emitProbability);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystem GetSubEmitterSystem_Injected(ref SubEmittersModule _unity_self, int index);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemSubEmitterType GetSubEmitterType_Injected(ref SubEmittersModule _unity_self, int index);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemSubEmitterProperties GetSubEmitterProperties_Injected(ref SubEmittersModule _unity_self, int index);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern float GetSubEmitterEmitProbability_Injected(ref SubEmittersModule _unity_self, int index);
		}

		public struct TextureSheetAnimationModule
		{
			internal ParticleSystem m_ParticleSystem;

			[Obsolete("flipU property is deprecated. Use ParticleSystemRenderer.flip.x instead.", false)]
			public float flipU
			{
				get
				{
					return m_ParticleSystem.GetComponent<ParticleSystemRenderer>().flip.x;
				}
				set
				{
					ParticleSystemRenderer component = m_ParticleSystem.GetComponent<ParticleSystemRenderer>();
					Vector3 flip = component.flip;
					flip.x = value;
					component.flip = flip;
				}
			}

			[Obsolete("flipV property is deprecated. Use ParticleSystemRenderer.flip.y instead.", false)]
			public float flipV
			{
				get
				{
					return m_ParticleSystem.GetComponent<ParticleSystemRenderer>().flip.y;
				}
				set
				{
					ParticleSystemRenderer component = m_ParticleSystem.GetComponent<ParticleSystemRenderer>();
					Vector3 flip = component.flip;
					flip.y = value;
					component.flip = flip;
				}
			}

			[Obsolete("useRandomRow property is deprecated. Use rowMode instead.", false)]
			public bool useRandomRow
			{
				get
				{
					return rowMode == ParticleSystemAnimationRowMode.Random;
				}
				set
				{
					rowMode = (value ? ParticleSystemAnimationRowMode.Random : ParticleSystemAnimationRowMode.Custom);
				}
			}

			public bool enabled
			{
				get
				{
					return get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_enabled_Injected(ref this, value);
				}
			}

			public ParticleSystemAnimationMode mode
			{
				get
				{
					return get_mode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_mode_Injected(ref this, value);
				}
			}

			public ParticleSystemAnimationTimeMode timeMode
			{
				get
				{
					return get_timeMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_timeMode_Injected(ref this, value);
				}
			}

			public float fps
			{
				get
				{
					return get_fps_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_fps_Injected(ref this, value);
				}
			}

			public int numTilesX
			{
				get
				{
					return get_numTilesX_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_numTilesX_Injected(ref this, value);
				}
			}

			public int numTilesY
			{
				get
				{
					return get_numTilesY_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_numTilesY_Injected(ref this, value);
				}
			}

			public ParticleSystemAnimationType animation
			{
				get
				{
					return get_animation_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_animation_Injected(ref this, value);
				}
			}

			public ParticleSystemAnimationRowMode rowMode
			{
				get
				{
					return get_rowMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_rowMode_Injected(ref this, value);
				}
			}

			public MinMaxCurve frameOverTime
			{
				get
				{
					get_frameOverTime_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_frameOverTime_Injected(ref this, ref value);
				}
			}

			public float frameOverTimeMultiplier
			{
				get
				{
					return get_frameOverTimeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_frameOverTimeMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve startFrame
			{
				get
				{
					get_startFrame_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_startFrame_Injected(ref this, ref value);
				}
			}

			public float startFrameMultiplier
			{
				get
				{
					return get_startFrameMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_startFrameMultiplier_Injected(ref this, value);
				}
			}

			public int cycleCount
			{
				get
				{
					return get_cycleCount_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_cycleCount_Injected(ref this, value);
				}
			}

			public int rowIndex
			{
				get
				{
					return get_rowIndex_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_rowIndex_Injected(ref this, value);
				}
			}

			public UVChannelFlags uvChannelMask
			{
				get
				{
					return get_uvChannelMask_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_uvChannelMask_Injected(ref this, value);
				}
			}

			public int spriteCount => get_spriteCount_Injected(ref this);

			public Vector2 speedRange
			{
				get
				{
					get_speedRange_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_speedRange_Injected(ref this, ref value);
				}
			}

			internal TextureSheetAnimationModule(ParticleSystem particleSystem)
			{
				m_ParticleSystem = particleSystem;
			}

			[NativeThrows]
			public void AddSprite(Sprite sprite)
			{
				AddSprite_Injected(ref this, sprite);
			}

			[NativeThrows]
			public void RemoveSprite(int index)
			{
				RemoveSprite_Injected(ref this, index);
			}

			[NativeThrows]
			public void SetSprite(int index, Sprite sprite)
			{
				SetSprite_Injected(ref this, index, sprite);
			}

			[NativeThrows]
			public Sprite GetSprite(int index)
			{
				return GetSprite_Injected(ref this, index);
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_enabled_Injected(ref TextureSheetAnimationModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_enabled_Injected(ref TextureSheetAnimationModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemAnimationMode get_mode_Injected(ref TextureSheetAnimationModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_mode_Injected(ref TextureSheetAnimationModule _unity_self, ParticleSystemAnimationMode value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemAnimationTimeMode get_timeMode_Injected(ref TextureSheetAnimationModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_timeMode_Injected(ref TextureSheetAnimationModule _unity_self, ParticleSystemAnimationTimeMode value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_fps_Injected(ref TextureSheetAnimationModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_fps_Injected(ref TextureSheetAnimationModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern int get_numTilesX_Injected(ref TextureSheetAnimationModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_numTilesX_Injected(ref TextureSheetAnimationModule _unity_self, int value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern int get_numTilesY_Injected(ref TextureSheetAnimationModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_numTilesY_Injected(ref TextureSheetAnimationModule _unity_self, int value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemAnimationType get_animation_Injected(ref TextureSheetAnimationModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_animation_Injected(ref TextureSheetAnimationModule _unity_self, ParticleSystemAnimationType value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemAnimationRowMode get_rowMode_Injected(ref TextureSheetAnimationModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_rowMode_Injected(ref TextureSheetAnimationModule _unity_self, ParticleSystemAnimationRowMode value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_frameOverTime_Injected(ref TextureSheetAnimationModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_frameOverTime_Injected(ref TextureSheetAnimationModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_frameOverTimeMultiplier_Injected(ref TextureSheetAnimationModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_frameOverTimeMultiplier_Injected(ref TextureSheetAnimationModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_startFrame_Injected(ref TextureSheetAnimationModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_startFrame_Injected(ref TextureSheetAnimationModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_startFrameMultiplier_Injected(ref TextureSheetAnimationModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_startFrameMultiplier_Injected(ref TextureSheetAnimationModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern int get_cycleCount_Injected(ref TextureSheetAnimationModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_cycleCount_Injected(ref TextureSheetAnimationModule _unity_self, int value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern int get_rowIndex_Injected(ref TextureSheetAnimationModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_rowIndex_Injected(ref TextureSheetAnimationModule _unity_self, int value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern UVChannelFlags get_uvChannelMask_Injected(ref TextureSheetAnimationModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_uvChannelMask_Injected(ref TextureSheetAnimationModule _unity_self, UVChannelFlags value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern int get_spriteCount_Injected(ref TextureSheetAnimationModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_speedRange_Injected(ref TextureSheetAnimationModule _unity_self, out Vector2 ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_speedRange_Injected(ref TextureSheetAnimationModule _unity_self, ref Vector2 value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void AddSprite_Injected(ref TextureSheetAnimationModule _unity_self, Sprite sprite);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemoveSprite_Injected(ref TextureSheetAnimationModule _unity_self, int index);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetSprite_Injected(ref TextureSheetAnimationModule _unity_self, int index, Sprite sprite);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern Sprite GetSprite_Injected(ref TextureSheetAnimationModule _unity_self, int index);
		}

		[RequiredByNativeCode("particleSystemParticle", Optional = true)]
		public struct Particle
		{
			[Flags]
			private enum Flags
			{
				Size3D = 1,
				Rotation3D = 2,
				MeshIndex = 4
			}

			private Vector3 m_Position;

			private Vector3 m_Velocity;

			private Vector3 m_AnimatedVelocity;

			private Vector3 m_InitialVelocity;

			private Vector3 m_AxisOfRotation;

			private Vector3 m_Rotation;

			private Vector3 m_AngularVelocity;

			private Vector3 m_StartSize;

			private Color32 m_StartColor;

			private uint m_RandomSeed;

			private uint m_ParentRandomSeed;

			private float m_Lifetime;

			private float m_StartLifetime;

			private int m_MeshIndex;

			private float m_EmitAccumulator0;

			private float m_EmitAccumulator1;

			private uint m_Flags;

			[Obsolete("Please use Particle.remainingLifetime instead. (UnityUpgradable) -> UnityEngine.ParticleSystem/Particle.remainingLifetime", false)]
			public float lifetime
			{
				get
				{
					return remainingLifetime;
				}
				set
				{
					remainingLifetime = value;
				}
			}

			[Obsolete("randomValue property is deprecated. Use randomSeed instead to control random behavior of particles.", false)]
			public float randomValue
			{
				get
				{
					return BitConverter.ToSingle(BitConverter.GetBytes(m_RandomSeed), 0);
				}
				set
				{
					m_RandomSeed = BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
				}
			}

			[Obsolete("size property is deprecated. Use startSize or GetCurrentSize() instead.", false)]
			public float size
			{
				get
				{
					return startSize;
				}
				set
				{
					startSize = value;
				}
			}

			[Obsolete("color property is deprecated. Use startColor or GetCurrentColor() instead.", false)]
			public Color32 color
			{
				get
				{
					return startColor;
				}
				set
				{
					startColor = value;
				}
			}

			public Vector3 position
			{
				get
				{
					return m_Position;
				}
				set
				{
					m_Position = value;
				}
			}

			public Vector3 velocity
			{
				get
				{
					return m_Velocity;
				}
				set
				{
					m_Velocity = value;
				}
			}

			public Vector3 animatedVelocity => m_AnimatedVelocity;

			public Vector3 totalVelocity => m_Velocity + m_AnimatedVelocity;

			public float remainingLifetime
			{
				get
				{
					return m_Lifetime;
				}
				set
				{
					m_Lifetime = value;
				}
			}

			public float startLifetime
			{
				get
				{
					return m_StartLifetime;
				}
				set
				{
					m_StartLifetime = value;
				}
			}

			public Color32 startColor
			{
				get
				{
					return m_StartColor;
				}
				set
				{
					m_StartColor = value;
				}
			}

			public uint randomSeed
			{
				get
				{
					return m_RandomSeed;
				}
				set
				{
					m_RandomSeed = value;
				}
			}

			public Vector3 axisOfRotation
			{
				get
				{
					return m_AxisOfRotation;
				}
				set
				{
					m_AxisOfRotation = value;
				}
			}

			public float startSize
			{
				get
				{
					return m_StartSize.x;
				}
				set
				{
					m_StartSize = new Vector3(value, value, value);
				}
			}

			public Vector3 startSize3D
			{
				get
				{
					return m_StartSize;
				}
				set
				{
					m_StartSize = value;
					m_Flags |= 1u;
				}
			}

			public float rotation
			{
				get
				{
					return m_Rotation.z * 57.29578f;
				}
				set
				{
					m_Rotation = new Vector3(0f, 0f, value * (MathF.PI / 180f));
				}
			}

			public Vector3 rotation3D
			{
				get
				{
					return m_Rotation * 57.29578f;
				}
				set
				{
					m_Rotation = value * (MathF.PI / 180f);
					m_Flags |= 2u;
				}
			}

			public float angularVelocity
			{
				get
				{
					return m_AngularVelocity.z * 57.29578f;
				}
				set
				{
					m_AngularVelocity = new Vector3(0f, 0f, value * (MathF.PI / 180f));
				}
			}

			public Vector3 angularVelocity3D
			{
				get
				{
					return m_AngularVelocity * 57.29578f;
				}
				set
				{
					m_AngularVelocity = value * (MathF.PI / 180f);
					m_Flags |= 2u;
				}
			}

			public float GetCurrentSize(ParticleSystem system)
			{
				return system.GetParticleCurrentSize(ref this);
			}

			public Vector3 GetCurrentSize3D(ParticleSystem system)
			{
				return system.GetParticleCurrentSize3D(ref this);
			}

			public Color32 GetCurrentColor(ParticleSystem system)
			{
				return system.GetParticleCurrentColor(ref this);
			}

			public void SetMeshIndex(int index)
			{
				m_MeshIndex = index;
				m_Flags |= 4u;
			}

			public int GetMeshIndex(ParticleSystem system)
			{
				return system.GetParticleMeshIndex(ref this);
			}
		}

		[NativeType(CodegenOptions.Custom, "MonoBurst", Header = "Runtime/Scripting/ScriptingCommonStructDefinitions.h")]
		public struct Burst
		{
			private float m_Time;

			private MinMaxCurve m_Count;

			private int m_RepeatCount;

			private float m_RepeatInterval;

			private float m_InvProbability;

			public float time
			{
				get
				{
					return m_Time;
				}
				set
				{
					m_Time = value;
				}
			}

			public MinMaxCurve count
			{
				get
				{
					return m_Count;
				}
				set
				{
					m_Count = value;
				}
			}

			public short minCount
			{
				get
				{
					return (short)m_Count.constantMin;
				}
				set
				{
					m_Count.constantMin = value;
				}
			}

			public short maxCount
			{
				get
				{
					return (short)m_Count.constantMax;
				}
				set
				{
					m_Count.constantMax = value;
				}
			}

			public int cycleCount
			{
				get
				{
					return m_RepeatCount + 1;
				}
				set
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("cycleCount", "cycleCount must be at least 0: " + value);
					}
					m_RepeatCount = value - 1;
				}
			}

			public float repeatInterval
			{
				get
				{
					return m_RepeatInterval;
				}
				set
				{
					if (value <= 0f)
					{
						throw new ArgumentOutOfRangeException("repeatInterval", "repeatInterval must be greater than 0.0f: " + value);
					}
					m_RepeatInterval = value;
				}
			}

			public float probability
			{
				get
				{
					return 1f - m_InvProbability;
				}
				set
				{
					if (value < 0f || value > 1f)
					{
						throw new ArgumentOutOfRangeException("probability", "probability must be between 0.0f and 1.0f: " + value);
					}
					m_InvProbability = 1f - value;
				}
			}

			public Burst(float _time, short _count)
			{
				m_Time = _time;
				m_Count = _count;
				m_RepeatCount = 0;
				m_RepeatInterval = 0f;
				m_InvProbability = 0f;
			}

			public Burst(float _time, short _minCount, short _maxCount)
			{
				m_Time = _time;
				m_Count = new MinMaxCurve(_minCount, _maxCount);
				m_RepeatCount = 0;
				m_RepeatInterval = 0f;
				m_InvProbability = 0f;
			}

			public Burst(float _time, short _minCount, short _maxCount, int _cycleCount, float _repeatInterval)
			{
				m_Time = _time;
				m_Count = new MinMaxCurve(_minCount, _maxCount);
				m_RepeatCount = _cycleCount - 1;
				m_RepeatInterval = _repeatInterval;
				m_InvProbability = 0f;
			}

			public Burst(float _time, MinMaxCurve _count)
			{
				m_Time = _time;
				m_Count = _count;
				m_RepeatCount = 0;
				m_RepeatInterval = 0f;
				m_InvProbability = 0f;
			}

			public Burst(float _time, MinMaxCurve _count, int _cycleCount, float _repeatInterval)
			{
				m_Time = _time;
				m_Count = _count;
				m_RepeatCount = _cycleCount - 1;
				m_RepeatInterval = _repeatInterval;
				m_InvProbability = 0f;
			}
		}

		[Serializable]
		[NativeType(CodegenOptions.Custom, "MonoMinMaxCurve", Header = "Runtime/Scripting/ScriptingCommonStructDefinitions.h")]
		public struct MinMaxCurve
		{
			[SerializeField]
			private ParticleSystemCurveMode m_Mode;

			[SerializeField]
			private float m_CurveMultiplier;

			[SerializeField]
			private AnimationCurve m_CurveMin;

			[SerializeField]
			private AnimationCurve m_CurveMax;

			[SerializeField]
			private float m_ConstantMin;

			[SerializeField]
			private float m_ConstantMax;

			public ParticleSystemCurveMode mode
			{
				get
				{
					return m_Mode;
				}
				set
				{
					m_Mode = value;
				}
			}

			public float curveMultiplier
			{
				get
				{
					return m_CurveMultiplier;
				}
				set
				{
					m_CurveMultiplier = value;
				}
			}

			public AnimationCurve curveMax
			{
				get
				{
					return m_CurveMax;
				}
				set
				{
					m_CurveMax = value;
				}
			}

			public AnimationCurve curveMin
			{
				get
				{
					return m_CurveMin;
				}
				set
				{
					m_CurveMin = value;
				}
			}

			public float constantMax
			{
				get
				{
					return m_ConstantMax;
				}
				set
				{
					m_ConstantMax = value;
				}
			}

			public float constantMin
			{
				get
				{
					return m_ConstantMin;
				}
				set
				{
					m_ConstantMin = value;
				}
			}

			public float constant
			{
				get
				{
					return m_ConstantMax;
				}
				set
				{
					m_ConstantMax = value;
				}
			}

			public AnimationCurve curve
			{
				get
				{
					return m_CurveMax;
				}
				set
				{
					m_CurveMax = value;
				}
			}

			public MinMaxCurve(float constant)
			{
				m_Mode = ParticleSystemCurveMode.Constant;
				m_CurveMultiplier = 0f;
				m_CurveMin = null;
				m_CurveMax = null;
				m_ConstantMin = 0f;
				m_ConstantMax = constant;
			}

			public MinMaxCurve(float multiplier, AnimationCurve curve)
			{
				m_Mode = ParticleSystemCurveMode.Curve;
				m_CurveMultiplier = multiplier;
				m_CurveMin = null;
				m_CurveMax = curve;
				m_ConstantMin = 0f;
				m_ConstantMax = 0f;
			}

			public MinMaxCurve(float multiplier, AnimationCurve min, AnimationCurve max)
			{
				m_Mode = ParticleSystemCurveMode.TwoCurves;
				m_CurveMultiplier = multiplier;
				m_CurveMin = min;
				m_CurveMax = max;
				m_ConstantMin = 0f;
				m_ConstantMax = 0f;
			}

			public MinMaxCurve(float min, float max)
			{
				m_Mode = ParticleSystemCurveMode.TwoConstants;
				m_CurveMultiplier = 0f;
				m_CurveMin = null;
				m_CurveMax = null;
				m_ConstantMin = min;
				m_ConstantMax = max;
			}

			public float Evaluate(float time)
			{
				return Evaluate(time, 1f);
			}

			public float Evaluate(float time, float lerpFactor)
			{
				return mode switch
				{
					ParticleSystemCurveMode.Constant => m_ConstantMax, 
					ParticleSystemCurveMode.TwoCurves => Mathf.Lerp(m_CurveMin.Evaluate(time), m_CurveMax.Evaluate(time), lerpFactor) * m_CurveMultiplier, 
					ParticleSystemCurveMode.TwoConstants => Mathf.Lerp(m_ConstantMin, m_ConstantMax, lerpFactor), 
					_ => m_CurveMax.Evaluate(time) * m_CurveMultiplier, 
				};
			}

			public static implicit operator MinMaxCurve(float constant)
			{
				return new MinMaxCurve(constant);
			}
		}

		[Serializable]
		[NativeType(CodegenOptions.Custom, "MonoMinMaxGradient", Header = "Runtime/Scripting/ScriptingCommonStructDefinitions.h")]
		public struct MinMaxGradient
		{
			[SerializeField]
			private ParticleSystemGradientMode m_Mode;

			[SerializeField]
			private Gradient m_GradientMin;

			[SerializeField]
			private Gradient m_GradientMax;

			[SerializeField]
			private Color m_ColorMin;

			[SerializeField]
			private Color m_ColorMax;

			public ParticleSystemGradientMode mode
			{
				get
				{
					return m_Mode;
				}
				set
				{
					m_Mode = value;
				}
			}

			public Gradient gradientMax
			{
				get
				{
					return m_GradientMax;
				}
				set
				{
					m_GradientMax = value;
				}
			}

			public Gradient gradientMin
			{
				get
				{
					return m_GradientMin;
				}
				set
				{
					m_GradientMin = value;
				}
			}

			public Color colorMax
			{
				get
				{
					return m_ColorMax;
				}
				set
				{
					m_ColorMax = value;
				}
			}

			public Color colorMin
			{
				get
				{
					return m_ColorMin;
				}
				set
				{
					m_ColorMin = value;
				}
			}

			public Color color
			{
				get
				{
					return m_ColorMax;
				}
				set
				{
					m_ColorMax = value;
				}
			}

			public Gradient gradient
			{
				get
				{
					return m_GradientMax;
				}
				set
				{
					m_GradientMax = value;
				}
			}

			public MinMaxGradient(Color color)
			{
				m_Mode = ParticleSystemGradientMode.Color;
				m_GradientMin = null;
				m_GradientMax = null;
				m_ColorMin = Color.black;
				m_ColorMax = color;
			}

			public MinMaxGradient(Gradient gradient)
			{
				m_Mode = ParticleSystemGradientMode.Gradient;
				m_GradientMin = null;
				m_GradientMax = gradient;
				m_ColorMin = Color.black;
				m_ColorMax = Color.black;
			}

			public MinMaxGradient(Color min, Color max)
			{
				m_Mode = ParticleSystemGradientMode.TwoColors;
				m_GradientMin = null;
				m_GradientMax = null;
				m_ColorMin = min;
				m_ColorMax = max;
			}

			public MinMaxGradient(Gradient min, Gradient max)
			{
				m_Mode = ParticleSystemGradientMode.TwoGradients;
				m_GradientMin = min;
				m_GradientMax = max;
				m_ColorMin = Color.black;
				m_ColorMax = Color.black;
			}

			public Color Evaluate(float time)
			{
				return Evaluate(time, 1f);
			}

			public Color Evaluate(float time, float lerpFactor)
			{
				return m_Mode switch
				{
					ParticleSystemGradientMode.Color => m_ColorMax, 
					ParticleSystemGradientMode.TwoColors => Color.Lerp(m_ColorMin, m_ColorMax, lerpFactor), 
					ParticleSystemGradientMode.TwoGradients => Color.Lerp(m_GradientMin.Evaluate(time), m_GradientMax.Evaluate(time), lerpFactor), 
					ParticleSystemGradientMode.RandomColor => m_GradientMax.Evaluate(lerpFactor), 
					_ => m_GradientMax.Evaluate(time), 
				};
			}

			public static implicit operator MinMaxGradient(Color color)
			{
				return new MinMaxGradient(color);
			}

			public static implicit operator MinMaxGradient(Gradient gradient)
			{
				return new MinMaxGradient(gradient);
			}
		}

		public struct EmitParams
		{
			[NativeName("particle")]
			private Particle m_Particle;

			[NativeName("positionSet")]
			private bool m_PositionSet;

			[NativeName("velocitySet")]
			private bool m_VelocitySet;

			[NativeName("axisOfRotationSet")]
			private bool m_AxisOfRotationSet;

			[NativeName("rotationSet")]
			private bool m_RotationSet;

			[NativeName("rotationalSpeedSet")]
			private bool m_AngularVelocitySet;

			[NativeName("startSizeSet")]
			private bool m_StartSizeSet;

			[NativeName("startColorSet")]
			private bool m_StartColorSet;

			[NativeName("randomSeedSet")]
			private bool m_RandomSeedSet;

			[NativeName("startLifetimeSet")]
			private bool m_StartLifetimeSet;

			[NativeName("meshIndexSet")]
			private bool m_MeshIndexSet;

			[NativeName("applyShapeToPosition")]
			private bool m_ApplyShapeToPosition;

			public Particle particle
			{
				get
				{
					return m_Particle;
				}
				set
				{
					m_Particle = value;
					m_PositionSet = true;
					m_VelocitySet = true;
					m_AxisOfRotationSet = true;
					m_RotationSet = true;
					m_AngularVelocitySet = true;
					m_StartSizeSet = true;
					m_StartColorSet = true;
					m_RandomSeedSet = true;
					m_StartLifetimeSet = true;
					m_MeshIndexSet = true;
				}
			}

			public Vector3 position
			{
				get
				{
					return m_Particle.position;
				}
				set
				{
					m_Particle.position = value;
					m_PositionSet = true;
				}
			}

			public bool applyShapeToPosition
			{
				get
				{
					return m_ApplyShapeToPosition;
				}
				set
				{
					m_ApplyShapeToPosition = value;
				}
			}

			public Vector3 velocity
			{
				get
				{
					return m_Particle.velocity;
				}
				set
				{
					m_Particle.velocity = value;
					m_VelocitySet = true;
				}
			}

			public float startLifetime
			{
				get
				{
					return m_Particle.startLifetime;
				}
				set
				{
					m_Particle.startLifetime = value;
					m_StartLifetimeSet = true;
				}
			}

			public float startSize
			{
				get
				{
					return m_Particle.startSize;
				}
				set
				{
					m_Particle.startSize = value;
					m_StartSizeSet = true;
				}
			}

			public Vector3 startSize3D
			{
				get
				{
					return m_Particle.startSize3D;
				}
				set
				{
					m_Particle.startSize3D = value;
					m_StartSizeSet = true;
				}
			}

			public Vector3 axisOfRotation
			{
				get
				{
					return m_Particle.axisOfRotation;
				}
				set
				{
					m_Particle.axisOfRotation = value;
					m_AxisOfRotationSet = true;
				}
			}

			public float rotation
			{
				get
				{
					return m_Particle.rotation;
				}
				set
				{
					m_Particle.rotation = value;
					m_RotationSet = true;
				}
			}

			public Vector3 rotation3D
			{
				get
				{
					return m_Particle.rotation3D;
				}
				set
				{
					m_Particle.rotation3D = value;
					m_RotationSet = true;
				}
			}

			public float angularVelocity
			{
				get
				{
					return m_Particle.angularVelocity;
				}
				set
				{
					m_Particle.angularVelocity = value;
					m_AngularVelocitySet = true;
				}
			}

			public Vector3 angularVelocity3D
			{
				get
				{
					return m_Particle.angularVelocity3D;
				}
				set
				{
					m_Particle.angularVelocity3D = value;
					m_AngularVelocitySet = true;
				}
			}

			public Color32 startColor
			{
				get
				{
					return m_Particle.startColor;
				}
				set
				{
					m_Particle.startColor = value;
					m_StartColorSet = true;
				}
			}

			public uint randomSeed
			{
				get
				{
					return m_Particle.randomSeed;
				}
				set
				{
					m_Particle.randomSeed = value;
					m_RandomSeedSet = true;
				}
			}

			public int meshIndex
			{
				set
				{
					m_Particle.SetMeshIndex(value);
					m_MeshIndexSet = true;
				}
			}

			public void ResetPosition()
			{
				m_PositionSet = false;
			}

			public void ResetVelocity()
			{
				m_VelocitySet = false;
			}

			public void ResetAxisOfRotation()
			{
				m_AxisOfRotationSet = false;
			}

			public void ResetRotation()
			{
				m_RotationSet = false;
			}

			public void ResetAngularVelocity()
			{
				m_AngularVelocitySet = false;
			}

			public void ResetStartSize()
			{
				m_StartSizeSet = false;
			}

			public void ResetStartColor()
			{
				m_StartColorSet = false;
			}

			public void ResetRandomSeed()
			{
				m_RandomSeedSet = false;
			}

			public void ResetStartLifetime()
			{
				m_StartLifetimeSet = false;
			}

			public void ResetMeshIndex()
			{
				m_MeshIndexSet = false;
			}
		}

		public struct PlaybackState
		{
			internal struct Seed
			{
				public uint x;

				public uint y;

				public uint z;

				public uint w;
			}

			internal struct Seed4
			{
				public Seed x;

				public Seed y;

				public Seed z;

				public Seed w;
			}

			internal struct Emission
			{
				public float m_ParticleSpacing;

				public float m_ToEmitAccumulator;

				public Seed m_Random;
			}

			internal struct Initial
			{
				public Seed4 m_Random;
			}

			internal struct Shape
			{
				public Seed4 m_Random;

				public float m_RadiusTimer;

				public float m_RadiusTimerPrev;

				public float m_ArcTimer;

				public float m_ArcTimerPrev;

				public float m_MeshSpawnTimer;

				public float m_MeshSpawnTimerPrev;

				public int m_OrderedMeshVertexIndex;
			}

			internal struct Force
			{
				public Seed4 m_Random;
			}

			internal struct Collision
			{
				public Seed4 m_Random;
			}

			internal struct Noise
			{
				public float m_ScrollOffset;
			}

			internal struct Lights
			{
				public Seed m_Random;

				public float m_ParticleEmissionCounter;
			}

			internal struct Trail
			{
				public float m_Timer;
			}

			internal float m_AccumulatedDt;

			internal float m_StartDelay;

			internal float m_PlaybackTime;

			internal int m_RingBufferIndex;

			internal Emission m_Emission;

			internal Initial m_Initial;

			internal Shape m_Shape;

			internal Force m_Force;

			internal Collision m_Collision;

			internal Noise m_Noise;

			internal Lights m_Lights;

			internal Trail m_Trail;
		}

		[NativeType(CodegenOptions.Custom, "MonoParticleTrails")]
		public struct Trails
		{
			internal List<Vector4> positions;

			internal List<int> frontPositions;

			internal List<int> backPositions;

			internal List<int> positionCounts;

			internal List<float> textureOffsets;

			internal int maxTrailCount;

			internal int maxPositionsPerTrailCount;

			public int capacity
			{
				get
				{
					if (positions == null)
					{
						return 0;
					}
					return positions.Capacity;
				}
				set
				{
					Allocate();
					positions.Capacity = value;
					frontPositions.Capacity = value;
					backPositions.Capacity = value;
					positionCounts.Capacity = value;
					textureOffsets.Capacity = value;
				}
			}

			internal void Allocate()
			{
				if (positions == null)
				{
					positions = new List<Vector4>();
				}
				if (frontPositions == null)
				{
					frontPositions = new List<int>();
				}
				if (backPositions == null)
				{
					backPositions = new List<int>();
				}
				if (positionCounts == null)
				{
					positionCounts = new List<int>();
				}
				if (textureOffsets == null)
				{
					textureOffsets = new List<float>();
				}
			}
		}

		public struct ColliderData
		{
			internal Component[] colliders;

			internal int[] colliderIndices;

			internal int[] particleStartIndices;

			public int GetColliderCount(int particleIndex)
			{
				if (particleIndex < particleStartIndices.Length - 1)
				{
					return particleStartIndices[particleIndex + 1] - particleStartIndices[particleIndex];
				}
				return colliderIndices.Length - particleStartIndices[particleIndex];
			}

			public Component GetCollider(int particleIndex, int colliderIndex)
			{
				if (colliderIndex >= GetColliderCount(particleIndex))
				{
					throw new IndexOutOfRangeException("colliderIndex exceeded the total number of colliders for the requested particle");
				}
				int num = particleStartIndices[particleIndex] + colliderIndex;
				return colliders[colliderIndices[num]];
			}
		}

		public struct VelocityOverLifetimeModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_enabled_Injected(ref this, value);
				}
			}

			public MinMaxCurve x
			{
				get
				{
					get_x_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_x_Injected(ref this, ref value);
				}
			}

			public MinMaxCurve y
			{
				get
				{
					get_y_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_y_Injected(ref this, ref value);
				}
			}

			public MinMaxCurve z
			{
				get
				{
					get_z_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_z_Injected(ref this, ref value);
				}
			}

			public float xMultiplier
			{
				get
				{
					return get_xMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_xMultiplier_Injected(ref this, value);
				}
			}

			public float yMultiplier
			{
				get
				{
					return get_yMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_yMultiplier_Injected(ref this, value);
				}
			}

			public float zMultiplier
			{
				get
				{
					return get_zMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_zMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve orbitalX
			{
				get
				{
					get_orbitalX_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_orbitalX_Injected(ref this, ref value);
				}
			}

			public MinMaxCurve orbitalY
			{
				get
				{
					get_orbitalY_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_orbitalY_Injected(ref this, ref value);
				}
			}

			public MinMaxCurve orbitalZ
			{
				get
				{
					get_orbitalZ_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_orbitalZ_Injected(ref this, ref value);
				}
			}

			public float orbitalXMultiplier
			{
				get
				{
					return get_orbitalXMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_orbitalXMultiplier_Injected(ref this, value);
				}
			}

			public float orbitalYMultiplier
			{
				get
				{
					return get_orbitalYMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_orbitalYMultiplier_Injected(ref this, value);
				}
			}

			public float orbitalZMultiplier
			{
				get
				{
					return get_orbitalZMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_orbitalZMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve orbitalOffsetX
			{
				get
				{
					get_orbitalOffsetX_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_orbitalOffsetX_Injected(ref this, ref value);
				}
			}

			public MinMaxCurve orbitalOffsetY
			{
				get
				{
					get_orbitalOffsetY_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_orbitalOffsetY_Injected(ref this, ref value);
				}
			}

			public MinMaxCurve orbitalOffsetZ
			{
				get
				{
					get_orbitalOffsetZ_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_orbitalOffsetZ_Injected(ref this, ref value);
				}
			}

			public float orbitalOffsetXMultiplier
			{
				get
				{
					return get_orbitalOffsetXMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_orbitalOffsetXMultiplier_Injected(ref this, value);
				}
			}

			public float orbitalOffsetYMultiplier
			{
				get
				{
					return get_orbitalOffsetYMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_orbitalOffsetYMultiplier_Injected(ref this, value);
				}
			}

			public float orbitalOffsetZMultiplier
			{
				get
				{
					return get_orbitalOffsetZMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_orbitalOffsetZMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve radial
			{
				get
				{
					get_radial_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_radial_Injected(ref this, ref value);
				}
			}

			public float radialMultiplier
			{
				get
				{
					return get_radialMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_radialMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve speedModifier
			{
				get
				{
					get_speedModifier_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_speedModifier_Injected(ref this, ref value);
				}
			}

			public float speedModifierMultiplier
			{
				get
				{
					return get_speedModifierMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_speedModifierMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystemSimulationSpace space
			{
				get
				{
					return get_space_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_space_Injected(ref this, value);
				}
			}

			internal VelocityOverLifetimeModule(ParticleSystem particleSystem)
			{
				m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_enabled_Injected(ref VelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_enabled_Injected(ref VelocityOverLifetimeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_x_Injected(ref VelocityOverLifetimeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_x_Injected(ref VelocityOverLifetimeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_y_Injected(ref VelocityOverLifetimeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_y_Injected(ref VelocityOverLifetimeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_z_Injected(ref VelocityOverLifetimeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_z_Injected(ref VelocityOverLifetimeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_xMultiplier_Injected(ref VelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_xMultiplier_Injected(ref VelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_yMultiplier_Injected(ref VelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_yMultiplier_Injected(ref VelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_zMultiplier_Injected(ref VelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_zMultiplier_Injected(ref VelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_orbitalX_Injected(ref VelocityOverLifetimeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_orbitalX_Injected(ref VelocityOverLifetimeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_orbitalY_Injected(ref VelocityOverLifetimeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_orbitalY_Injected(ref VelocityOverLifetimeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_orbitalZ_Injected(ref VelocityOverLifetimeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_orbitalZ_Injected(ref VelocityOverLifetimeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_orbitalXMultiplier_Injected(ref VelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_orbitalXMultiplier_Injected(ref VelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_orbitalYMultiplier_Injected(ref VelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_orbitalYMultiplier_Injected(ref VelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_orbitalZMultiplier_Injected(ref VelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_orbitalZMultiplier_Injected(ref VelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_orbitalOffsetX_Injected(ref VelocityOverLifetimeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_orbitalOffsetX_Injected(ref VelocityOverLifetimeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_orbitalOffsetY_Injected(ref VelocityOverLifetimeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_orbitalOffsetY_Injected(ref VelocityOverLifetimeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_orbitalOffsetZ_Injected(ref VelocityOverLifetimeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_orbitalOffsetZ_Injected(ref VelocityOverLifetimeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_orbitalOffsetXMultiplier_Injected(ref VelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_orbitalOffsetXMultiplier_Injected(ref VelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_orbitalOffsetYMultiplier_Injected(ref VelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_orbitalOffsetYMultiplier_Injected(ref VelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_orbitalOffsetZMultiplier_Injected(ref VelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_orbitalOffsetZMultiplier_Injected(ref VelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_radial_Injected(ref VelocityOverLifetimeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_radial_Injected(ref VelocityOverLifetimeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_radialMultiplier_Injected(ref VelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_radialMultiplier_Injected(ref VelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_speedModifier_Injected(ref VelocityOverLifetimeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_speedModifier_Injected(ref VelocityOverLifetimeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_speedModifierMultiplier_Injected(ref VelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_speedModifierMultiplier_Injected(ref VelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemSimulationSpace get_space_Injected(ref VelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_space_Injected(ref VelocityOverLifetimeModule _unity_self, ParticleSystemSimulationSpace value);
		}

		public struct LimitVelocityOverLifetimeModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_enabled_Injected(ref this, value);
				}
			}

			public MinMaxCurve limitX
			{
				get
				{
					get_limitX_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_limitX_Injected(ref this, ref value);
				}
			}

			public float limitXMultiplier
			{
				get
				{
					return get_limitXMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_limitXMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve limitY
			{
				get
				{
					get_limitY_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_limitY_Injected(ref this, ref value);
				}
			}

			public float limitYMultiplier
			{
				get
				{
					return get_limitYMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_limitYMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve limitZ
			{
				get
				{
					get_limitZ_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_limitZ_Injected(ref this, ref value);
				}
			}

			public float limitZMultiplier
			{
				get
				{
					return get_limitZMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_limitZMultiplier_Injected(ref this, value);
				}
			}

			[NativeName("Magnitude")]
			public MinMaxCurve limit
			{
				get
				{
					get_limit_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_limit_Injected(ref this, ref value);
				}
			}

			[NativeName("MagnitudeMultiplier")]
			public float limitMultiplier
			{
				get
				{
					return get_limitMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_limitMultiplier_Injected(ref this, value);
				}
			}

			public float dampen
			{
				get
				{
					return get_dampen_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_dampen_Injected(ref this, value);
				}
			}

			public bool separateAxes
			{
				get
				{
					return get_separateAxes_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_separateAxes_Injected(ref this, value);
				}
			}

			public ParticleSystemSimulationSpace space
			{
				get
				{
					return get_space_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_space_Injected(ref this, value);
				}
			}

			public MinMaxCurve drag
			{
				get
				{
					get_drag_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_drag_Injected(ref this, ref value);
				}
			}

			public float dragMultiplier
			{
				get
				{
					return get_dragMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_dragMultiplier_Injected(ref this, value);
				}
			}

			public bool multiplyDragByParticleSize
			{
				get
				{
					return get_multiplyDragByParticleSize_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_multiplyDragByParticleSize_Injected(ref this, value);
				}
			}

			public bool multiplyDragByParticleVelocity
			{
				get
				{
					return get_multiplyDragByParticleVelocity_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_multiplyDragByParticleVelocity_Injected(ref this, value);
				}
			}

			internal LimitVelocityOverLifetimeModule(ParticleSystem particleSystem)
			{
				m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_enabled_Injected(ref LimitVelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_enabled_Injected(ref LimitVelocityOverLifetimeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_limitX_Injected(ref LimitVelocityOverLifetimeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_limitX_Injected(ref LimitVelocityOverLifetimeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_limitXMultiplier_Injected(ref LimitVelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_limitXMultiplier_Injected(ref LimitVelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_limitY_Injected(ref LimitVelocityOverLifetimeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_limitY_Injected(ref LimitVelocityOverLifetimeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_limitYMultiplier_Injected(ref LimitVelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_limitYMultiplier_Injected(ref LimitVelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_limitZ_Injected(ref LimitVelocityOverLifetimeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_limitZ_Injected(ref LimitVelocityOverLifetimeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_limitZMultiplier_Injected(ref LimitVelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_limitZMultiplier_Injected(ref LimitVelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_limit_Injected(ref LimitVelocityOverLifetimeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_limit_Injected(ref LimitVelocityOverLifetimeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_limitMultiplier_Injected(ref LimitVelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_limitMultiplier_Injected(ref LimitVelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_dampen_Injected(ref LimitVelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_dampen_Injected(ref LimitVelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_separateAxes_Injected(ref LimitVelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_separateAxes_Injected(ref LimitVelocityOverLifetimeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemSimulationSpace get_space_Injected(ref LimitVelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_space_Injected(ref LimitVelocityOverLifetimeModule _unity_self, ParticleSystemSimulationSpace value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_drag_Injected(ref LimitVelocityOverLifetimeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_drag_Injected(ref LimitVelocityOverLifetimeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_dragMultiplier_Injected(ref LimitVelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_dragMultiplier_Injected(ref LimitVelocityOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_multiplyDragByParticleSize_Injected(ref LimitVelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_multiplyDragByParticleSize_Injected(ref LimitVelocityOverLifetimeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_multiplyDragByParticleVelocity_Injected(ref LimitVelocityOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_multiplyDragByParticleVelocity_Injected(ref LimitVelocityOverLifetimeModule _unity_self, bool value);
		}

		public struct InheritVelocityModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_enabled_Injected(ref this, value);
				}
			}

			public ParticleSystemInheritVelocityMode mode
			{
				get
				{
					return get_mode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_mode_Injected(ref this, value);
				}
			}

			public MinMaxCurve curve
			{
				get
				{
					get_curve_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_curve_Injected(ref this, ref value);
				}
			}

			public float curveMultiplier
			{
				get
				{
					return get_curveMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_curveMultiplier_Injected(ref this, value);
				}
			}

			internal InheritVelocityModule(ParticleSystem particleSystem)
			{
				m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_enabled_Injected(ref InheritVelocityModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_enabled_Injected(ref InheritVelocityModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemInheritVelocityMode get_mode_Injected(ref InheritVelocityModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_mode_Injected(ref InheritVelocityModule _unity_self, ParticleSystemInheritVelocityMode value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_curve_Injected(ref InheritVelocityModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_curve_Injected(ref InheritVelocityModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_curveMultiplier_Injected(ref InheritVelocityModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_curveMultiplier_Injected(ref InheritVelocityModule _unity_self, float value);
		}

		public struct LifetimeByEmitterSpeedModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_enabled_Injected(ref this, value);
				}
			}

			public MinMaxCurve curve
			{
				get
				{
					get_curve_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_curve_Injected(ref this, ref value);
				}
			}

			public float curveMultiplier
			{
				get
				{
					return get_curveMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_curveMultiplier_Injected(ref this, value);
				}
			}

			public Vector2 range
			{
				get
				{
					get_range_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_range_Injected(ref this, ref value);
				}
			}

			internal LifetimeByEmitterSpeedModule(ParticleSystem particleSystem)
			{
				m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_enabled_Injected(ref LifetimeByEmitterSpeedModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_enabled_Injected(ref LifetimeByEmitterSpeedModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_curve_Injected(ref LifetimeByEmitterSpeedModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_curve_Injected(ref LifetimeByEmitterSpeedModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_curveMultiplier_Injected(ref LifetimeByEmitterSpeedModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_curveMultiplier_Injected(ref LifetimeByEmitterSpeedModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_range_Injected(ref LifetimeByEmitterSpeedModule _unity_self, out Vector2 ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_range_Injected(ref LifetimeByEmitterSpeedModule _unity_self, ref Vector2 value);
		}

		public struct ForceOverLifetimeModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_enabled_Injected(ref this, value);
				}
			}

			public MinMaxCurve x
			{
				get
				{
					get_x_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_x_Injected(ref this, ref value);
				}
			}

			public MinMaxCurve y
			{
				get
				{
					get_y_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_y_Injected(ref this, ref value);
				}
			}

			public MinMaxCurve z
			{
				get
				{
					get_z_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_z_Injected(ref this, ref value);
				}
			}

			public float xMultiplier
			{
				get
				{
					return get_xMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_xMultiplier_Injected(ref this, value);
				}
			}

			public float yMultiplier
			{
				get
				{
					return get_yMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_yMultiplier_Injected(ref this, value);
				}
			}

			public float zMultiplier
			{
				get
				{
					return get_zMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_zMultiplier_Injected(ref this, value);
				}
			}

			public ParticleSystemSimulationSpace space
			{
				get
				{
					return get_space_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_space_Injected(ref this, value);
				}
			}

			public bool randomized
			{
				get
				{
					return get_randomized_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_randomized_Injected(ref this, value);
				}
			}

			internal ForceOverLifetimeModule(ParticleSystem particleSystem)
			{
				m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_enabled_Injected(ref ForceOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_enabled_Injected(ref ForceOverLifetimeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_x_Injected(ref ForceOverLifetimeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_x_Injected(ref ForceOverLifetimeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_y_Injected(ref ForceOverLifetimeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_y_Injected(ref ForceOverLifetimeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_z_Injected(ref ForceOverLifetimeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_z_Injected(ref ForceOverLifetimeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_xMultiplier_Injected(ref ForceOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_xMultiplier_Injected(ref ForceOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_yMultiplier_Injected(ref ForceOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_yMultiplier_Injected(ref ForceOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_zMultiplier_Injected(ref ForceOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_zMultiplier_Injected(ref ForceOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemSimulationSpace get_space_Injected(ref ForceOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_space_Injected(ref ForceOverLifetimeModule _unity_self, ParticleSystemSimulationSpace value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_randomized_Injected(ref ForceOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_randomized_Injected(ref ForceOverLifetimeModule _unity_self, bool value);
		}

		public struct ColorOverLifetimeModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_enabled_Injected(ref this, value);
				}
			}

			public MinMaxGradient color
			{
				get
				{
					get_color_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_color_Injected(ref this, ref value);
				}
			}

			internal ColorOverLifetimeModule(ParticleSystem particleSystem)
			{
				m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_enabled_Injected(ref ColorOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_enabled_Injected(ref ColorOverLifetimeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_color_Injected(ref ColorOverLifetimeModule _unity_self, out MinMaxGradient ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_color_Injected(ref ColorOverLifetimeModule _unity_self, ref MinMaxGradient value);
		}

		public struct ColorBySpeedModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_enabled_Injected(ref this, value);
				}
			}

			public MinMaxGradient color
			{
				get
				{
					get_color_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_color_Injected(ref this, ref value);
				}
			}

			public Vector2 range
			{
				get
				{
					get_range_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_range_Injected(ref this, ref value);
				}
			}

			internal ColorBySpeedModule(ParticleSystem particleSystem)
			{
				m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_enabled_Injected(ref ColorBySpeedModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_enabled_Injected(ref ColorBySpeedModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_color_Injected(ref ColorBySpeedModule _unity_self, out MinMaxGradient ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_color_Injected(ref ColorBySpeedModule _unity_self, ref MinMaxGradient value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_range_Injected(ref ColorBySpeedModule _unity_self, out Vector2 ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_range_Injected(ref ColorBySpeedModule _unity_self, ref Vector2 value);
		}

		public struct SizeOverLifetimeModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_enabled_Injected(ref this, value);
				}
			}

			[NativeName("X")]
			public MinMaxCurve size
			{
				get
				{
					get_size_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_size_Injected(ref this, ref value);
				}
			}

			[NativeName("XMultiplier")]
			public float sizeMultiplier
			{
				get
				{
					return get_sizeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_sizeMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve x
			{
				get
				{
					get_x_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_x_Injected(ref this, ref value);
				}
			}

			public float xMultiplier
			{
				get
				{
					return get_xMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_xMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve y
			{
				get
				{
					get_y_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_y_Injected(ref this, ref value);
				}
			}

			public float yMultiplier
			{
				get
				{
					return get_yMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_yMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve z
			{
				get
				{
					get_z_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_z_Injected(ref this, ref value);
				}
			}

			public float zMultiplier
			{
				get
				{
					return get_zMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_zMultiplier_Injected(ref this, value);
				}
			}

			public bool separateAxes
			{
				get
				{
					return get_separateAxes_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_separateAxes_Injected(ref this, value);
				}
			}

			internal SizeOverLifetimeModule(ParticleSystem particleSystem)
			{
				m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_enabled_Injected(ref SizeOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_enabled_Injected(ref SizeOverLifetimeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_size_Injected(ref SizeOverLifetimeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_size_Injected(ref SizeOverLifetimeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_sizeMultiplier_Injected(ref SizeOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_sizeMultiplier_Injected(ref SizeOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_x_Injected(ref SizeOverLifetimeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_x_Injected(ref SizeOverLifetimeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_xMultiplier_Injected(ref SizeOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_xMultiplier_Injected(ref SizeOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_y_Injected(ref SizeOverLifetimeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_y_Injected(ref SizeOverLifetimeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_yMultiplier_Injected(ref SizeOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_yMultiplier_Injected(ref SizeOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_z_Injected(ref SizeOverLifetimeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_z_Injected(ref SizeOverLifetimeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_zMultiplier_Injected(ref SizeOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_zMultiplier_Injected(ref SizeOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_separateAxes_Injected(ref SizeOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_separateAxes_Injected(ref SizeOverLifetimeModule _unity_self, bool value);
		}

		public struct SizeBySpeedModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_enabled_Injected(ref this, value);
				}
			}

			[NativeName("X")]
			public MinMaxCurve size
			{
				get
				{
					get_size_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_size_Injected(ref this, ref value);
				}
			}

			[NativeName("XMultiplier")]
			public float sizeMultiplier
			{
				get
				{
					return get_sizeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_sizeMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve x
			{
				get
				{
					get_x_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_x_Injected(ref this, ref value);
				}
			}

			public float xMultiplier
			{
				get
				{
					return get_xMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_xMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve y
			{
				get
				{
					get_y_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_y_Injected(ref this, ref value);
				}
			}

			public float yMultiplier
			{
				get
				{
					return get_yMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_yMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve z
			{
				get
				{
					get_z_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_z_Injected(ref this, ref value);
				}
			}

			public float zMultiplier
			{
				get
				{
					return get_zMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_zMultiplier_Injected(ref this, value);
				}
			}

			public bool separateAxes
			{
				get
				{
					return get_separateAxes_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_separateAxes_Injected(ref this, value);
				}
			}

			public Vector2 range
			{
				get
				{
					get_range_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_range_Injected(ref this, ref value);
				}
			}

			internal SizeBySpeedModule(ParticleSystem particleSystem)
			{
				m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_enabled_Injected(ref SizeBySpeedModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_enabled_Injected(ref SizeBySpeedModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_size_Injected(ref SizeBySpeedModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_size_Injected(ref SizeBySpeedModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_sizeMultiplier_Injected(ref SizeBySpeedModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_sizeMultiplier_Injected(ref SizeBySpeedModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_x_Injected(ref SizeBySpeedModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_x_Injected(ref SizeBySpeedModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_xMultiplier_Injected(ref SizeBySpeedModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_xMultiplier_Injected(ref SizeBySpeedModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_y_Injected(ref SizeBySpeedModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_y_Injected(ref SizeBySpeedModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_yMultiplier_Injected(ref SizeBySpeedModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_yMultiplier_Injected(ref SizeBySpeedModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_z_Injected(ref SizeBySpeedModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_z_Injected(ref SizeBySpeedModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_zMultiplier_Injected(ref SizeBySpeedModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_zMultiplier_Injected(ref SizeBySpeedModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_separateAxes_Injected(ref SizeBySpeedModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_separateAxes_Injected(ref SizeBySpeedModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_range_Injected(ref SizeBySpeedModule _unity_self, out Vector2 ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_range_Injected(ref SizeBySpeedModule _unity_self, ref Vector2 value);
		}

		public struct RotationOverLifetimeModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_enabled_Injected(ref this, value);
				}
			}

			public MinMaxCurve x
			{
				get
				{
					get_x_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_x_Injected(ref this, ref value);
				}
			}

			public float xMultiplier
			{
				get
				{
					return get_xMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_xMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve y
			{
				get
				{
					get_y_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_y_Injected(ref this, ref value);
				}
			}

			public float yMultiplier
			{
				get
				{
					return get_yMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_yMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve z
			{
				get
				{
					get_z_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_z_Injected(ref this, ref value);
				}
			}

			public float zMultiplier
			{
				get
				{
					return get_zMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_zMultiplier_Injected(ref this, value);
				}
			}

			public bool separateAxes
			{
				get
				{
					return get_separateAxes_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_separateAxes_Injected(ref this, value);
				}
			}

			internal RotationOverLifetimeModule(ParticleSystem particleSystem)
			{
				m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_enabled_Injected(ref RotationOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_enabled_Injected(ref RotationOverLifetimeModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_x_Injected(ref RotationOverLifetimeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_x_Injected(ref RotationOverLifetimeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_xMultiplier_Injected(ref RotationOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_xMultiplier_Injected(ref RotationOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_y_Injected(ref RotationOverLifetimeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_y_Injected(ref RotationOverLifetimeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_yMultiplier_Injected(ref RotationOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_yMultiplier_Injected(ref RotationOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_z_Injected(ref RotationOverLifetimeModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_z_Injected(ref RotationOverLifetimeModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_zMultiplier_Injected(ref RotationOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_zMultiplier_Injected(ref RotationOverLifetimeModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_separateAxes_Injected(ref RotationOverLifetimeModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_separateAxes_Injected(ref RotationOverLifetimeModule _unity_self, bool value);
		}

		public struct RotationBySpeedModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_enabled_Injected(ref this, value);
				}
			}

			public MinMaxCurve x
			{
				get
				{
					get_x_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_x_Injected(ref this, ref value);
				}
			}

			public float xMultiplier
			{
				get
				{
					return get_xMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_xMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve y
			{
				get
				{
					get_y_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_y_Injected(ref this, ref value);
				}
			}

			public float yMultiplier
			{
				get
				{
					return get_yMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_yMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve z
			{
				get
				{
					get_z_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_z_Injected(ref this, ref value);
				}
			}

			public float zMultiplier
			{
				get
				{
					return get_zMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_zMultiplier_Injected(ref this, value);
				}
			}

			public bool separateAxes
			{
				get
				{
					return get_separateAxes_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_separateAxes_Injected(ref this, value);
				}
			}

			public Vector2 range
			{
				get
				{
					get_range_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_range_Injected(ref this, ref value);
				}
			}

			internal RotationBySpeedModule(ParticleSystem particleSystem)
			{
				m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_enabled_Injected(ref RotationBySpeedModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_enabled_Injected(ref RotationBySpeedModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_x_Injected(ref RotationBySpeedModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_x_Injected(ref RotationBySpeedModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_xMultiplier_Injected(ref RotationBySpeedModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_xMultiplier_Injected(ref RotationBySpeedModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_y_Injected(ref RotationBySpeedModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_y_Injected(ref RotationBySpeedModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_yMultiplier_Injected(ref RotationBySpeedModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_yMultiplier_Injected(ref RotationBySpeedModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_z_Injected(ref RotationBySpeedModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_z_Injected(ref RotationBySpeedModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_zMultiplier_Injected(ref RotationBySpeedModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_zMultiplier_Injected(ref RotationBySpeedModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_separateAxes_Injected(ref RotationBySpeedModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_separateAxes_Injected(ref RotationBySpeedModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_range_Injected(ref RotationBySpeedModule _unity_self, out Vector2 ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_range_Injected(ref RotationBySpeedModule _unity_self, ref Vector2 value);
		}

		public struct ExternalForcesModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_enabled_Injected(ref this, value);
				}
			}

			public float multiplier
			{
				get
				{
					return get_multiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_multiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve multiplierCurve
			{
				get
				{
					get_multiplierCurve_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_multiplierCurve_Injected(ref this, ref value);
				}
			}

			public ParticleSystemGameObjectFilter influenceFilter
			{
				get
				{
					return get_influenceFilter_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_influenceFilter_Injected(ref this, value);
				}
			}

			public LayerMask influenceMask
			{
				get
				{
					get_influenceMask_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_influenceMask_Injected(ref this, ref value);
				}
			}

			[NativeThrows]
			public int influenceCount => get_influenceCount_Injected(ref this);

			internal ExternalForcesModule(ParticleSystem particleSystem)
			{
				m_ParticleSystem = particleSystem;
			}

			public bool IsAffectedBy(ParticleSystemForceField field)
			{
				return IsAffectedBy_Injected(ref this, field);
			}

			[NativeThrows]
			public void AddInfluence([NotNull("ArgumentNullException")] ParticleSystemForceField field)
			{
				AddInfluence_Injected(ref this, field);
			}

			[NativeThrows]
			private void RemoveInfluenceAtIndex(int index)
			{
				RemoveInfluenceAtIndex_Injected(ref this, index);
			}

			public void RemoveInfluence(int index)
			{
				RemoveInfluenceAtIndex(index);
			}

			[NativeThrows]
			public void RemoveInfluence([NotNull("ArgumentNullException")] ParticleSystemForceField field)
			{
				RemoveInfluence_Injected(ref this, field);
			}

			[NativeThrows]
			public void RemoveAllInfluences()
			{
				RemoveAllInfluences_Injected(ref this);
			}

			[NativeThrows]
			public void SetInfluence(int index, [NotNull("ArgumentNullException")] ParticleSystemForceField field)
			{
				SetInfluence_Injected(ref this, index, field);
			}

			[NativeThrows]
			public ParticleSystemForceField GetInfluence(int index)
			{
				return GetInfluence_Injected(ref this, index);
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_enabled_Injected(ref ExternalForcesModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_enabled_Injected(ref ExternalForcesModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_multiplier_Injected(ref ExternalForcesModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_multiplier_Injected(ref ExternalForcesModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_multiplierCurve_Injected(ref ExternalForcesModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_multiplierCurve_Injected(ref ExternalForcesModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemGameObjectFilter get_influenceFilter_Injected(ref ExternalForcesModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_influenceFilter_Injected(ref ExternalForcesModule _unity_self, ParticleSystemGameObjectFilter value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_influenceMask_Injected(ref ExternalForcesModule _unity_self, out LayerMask ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_influenceMask_Injected(ref ExternalForcesModule _unity_self, ref LayerMask value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern int get_influenceCount_Injected(ref ExternalForcesModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool IsAffectedBy_Injected(ref ExternalForcesModule _unity_self, ParticleSystemForceField field);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void AddInfluence_Injected(ref ExternalForcesModule _unity_self, ParticleSystemForceField field);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemoveInfluenceAtIndex_Injected(ref ExternalForcesModule _unity_self, int index);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemoveInfluence_Injected(ref ExternalForcesModule _unity_self, ParticleSystemForceField field);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void RemoveAllInfluences_Injected(ref ExternalForcesModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetInfluence_Injected(ref ExternalForcesModule _unity_self, int index, ParticleSystemForceField field);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemForceField GetInfluence_Injected(ref ExternalForcesModule _unity_self, int index);
		}

		public struct NoiseModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_enabled_Injected(ref this, value);
				}
			}

			public bool separateAxes
			{
				get
				{
					return get_separateAxes_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_separateAxes_Injected(ref this, value);
				}
			}

			[NativeName("StrengthX")]
			public MinMaxCurve strength
			{
				get
				{
					get_strength_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_strength_Injected(ref this, ref value);
				}
			}

			[NativeName("StrengthXMultiplier")]
			public float strengthMultiplier
			{
				get
				{
					return get_strengthMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_strengthMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve strengthX
			{
				get
				{
					get_strengthX_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_strengthX_Injected(ref this, ref value);
				}
			}

			public float strengthXMultiplier
			{
				get
				{
					return get_strengthXMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_strengthXMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve strengthY
			{
				get
				{
					get_strengthY_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_strengthY_Injected(ref this, ref value);
				}
			}

			public float strengthYMultiplier
			{
				get
				{
					return get_strengthYMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_strengthYMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve strengthZ
			{
				get
				{
					get_strengthZ_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_strengthZ_Injected(ref this, ref value);
				}
			}

			public float strengthZMultiplier
			{
				get
				{
					return get_strengthZMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_strengthZMultiplier_Injected(ref this, value);
				}
			}

			public float frequency
			{
				get
				{
					return get_frequency_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_frequency_Injected(ref this, value);
				}
			}

			public bool damping
			{
				get
				{
					return get_damping_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_damping_Injected(ref this, value);
				}
			}

			public int octaveCount
			{
				get
				{
					return get_octaveCount_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_octaveCount_Injected(ref this, value);
				}
			}

			public float octaveMultiplier
			{
				get
				{
					return get_octaveMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_octaveMultiplier_Injected(ref this, value);
				}
			}

			public float octaveScale
			{
				get
				{
					return get_octaveScale_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_octaveScale_Injected(ref this, value);
				}
			}

			public ParticleSystemNoiseQuality quality
			{
				get
				{
					return get_quality_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_quality_Injected(ref this, value);
				}
			}

			public MinMaxCurve scrollSpeed
			{
				get
				{
					get_scrollSpeed_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_scrollSpeed_Injected(ref this, ref value);
				}
			}

			public float scrollSpeedMultiplier
			{
				get
				{
					return get_scrollSpeedMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_scrollSpeedMultiplier_Injected(ref this, value);
				}
			}

			public bool remapEnabled
			{
				get
				{
					return get_remapEnabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_remapEnabled_Injected(ref this, value);
				}
			}

			[NativeName("RemapX")]
			public MinMaxCurve remap
			{
				get
				{
					get_remap_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_remap_Injected(ref this, ref value);
				}
			}

			[NativeName("RemapXMultiplier")]
			public float remapMultiplier
			{
				get
				{
					return get_remapMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_remapMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve remapX
			{
				get
				{
					get_remapX_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_remapX_Injected(ref this, ref value);
				}
			}

			public float remapXMultiplier
			{
				get
				{
					return get_remapXMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_remapXMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve remapY
			{
				get
				{
					get_remapY_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_remapY_Injected(ref this, ref value);
				}
			}

			public float remapYMultiplier
			{
				get
				{
					return get_remapYMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_remapYMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve remapZ
			{
				get
				{
					get_remapZ_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_remapZ_Injected(ref this, ref value);
				}
			}

			public float remapZMultiplier
			{
				get
				{
					return get_remapZMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_remapZMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve positionAmount
			{
				get
				{
					get_positionAmount_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_positionAmount_Injected(ref this, ref value);
				}
			}

			public MinMaxCurve rotationAmount
			{
				get
				{
					get_rotationAmount_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_rotationAmount_Injected(ref this, ref value);
				}
			}

			public MinMaxCurve sizeAmount
			{
				get
				{
					get_sizeAmount_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_sizeAmount_Injected(ref this, ref value);
				}
			}

			internal NoiseModule(ParticleSystem particleSystem)
			{
				m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_enabled_Injected(ref NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_enabled_Injected(ref NoiseModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_separateAxes_Injected(ref NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_separateAxes_Injected(ref NoiseModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_strength_Injected(ref NoiseModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_strength_Injected(ref NoiseModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_strengthMultiplier_Injected(ref NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_strengthMultiplier_Injected(ref NoiseModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_strengthX_Injected(ref NoiseModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_strengthX_Injected(ref NoiseModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_strengthXMultiplier_Injected(ref NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_strengthXMultiplier_Injected(ref NoiseModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_strengthY_Injected(ref NoiseModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_strengthY_Injected(ref NoiseModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_strengthYMultiplier_Injected(ref NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_strengthYMultiplier_Injected(ref NoiseModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_strengthZ_Injected(ref NoiseModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_strengthZ_Injected(ref NoiseModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_strengthZMultiplier_Injected(ref NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_strengthZMultiplier_Injected(ref NoiseModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_frequency_Injected(ref NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_frequency_Injected(ref NoiseModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_damping_Injected(ref NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_damping_Injected(ref NoiseModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern int get_octaveCount_Injected(ref NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_octaveCount_Injected(ref NoiseModule _unity_self, int value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_octaveMultiplier_Injected(ref NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_octaveMultiplier_Injected(ref NoiseModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_octaveScale_Injected(ref NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_octaveScale_Injected(ref NoiseModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemNoiseQuality get_quality_Injected(ref NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_quality_Injected(ref NoiseModule _unity_self, ParticleSystemNoiseQuality value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_scrollSpeed_Injected(ref NoiseModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_scrollSpeed_Injected(ref NoiseModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_scrollSpeedMultiplier_Injected(ref NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_scrollSpeedMultiplier_Injected(ref NoiseModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_remapEnabled_Injected(ref NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_remapEnabled_Injected(ref NoiseModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_remap_Injected(ref NoiseModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_remap_Injected(ref NoiseModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_remapMultiplier_Injected(ref NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_remapMultiplier_Injected(ref NoiseModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_remapX_Injected(ref NoiseModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_remapX_Injected(ref NoiseModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_remapXMultiplier_Injected(ref NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_remapXMultiplier_Injected(ref NoiseModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_remapY_Injected(ref NoiseModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_remapY_Injected(ref NoiseModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_remapYMultiplier_Injected(ref NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_remapYMultiplier_Injected(ref NoiseModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_remapZ_Injected(ref NoiseModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_remapZ_Injected(ref NoiseModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_remapZMultiplier_Injected(ref NoiseModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_remapZMultiplier_Injected(ref NoiseModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_positionAmount_Injected(ref NoiseModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_positionAmount_Injected(ref NoiseModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_rotationAmount_Injected(ref NoiseModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_rotationAmount_Injected(ref NoiseModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_sizeAmount_Injected(ref NoiseModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_sizeAmount_Injected(ref NoiseModule _unity_self, ref MinMaxCurve value);
		}

		public struct LightsModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_enabled_Injected(ref this, value);
				}
			}

			public float ratio
			{
				get
				{
					return get_ratio_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_ratio_Injected(ref this, value);
				}
			}

			public bool useRandomDistribution
			{
				get
				{
					return get_useRandomDistribution_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_useRandomDistribution_Injected(ref this, value);
				}
			}

			public Light light
			{
				get
				{
					return get_light_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_light_Injected(ref this, value);
				}
			}

			public bool useParticleColor
			{
				get
				{
					return get_useParticleColor_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_useParticleColor_Injected(ref this, value);
				}
			}

			public bool sizeAffectsRange
			{
				get
				{
					return get_sizeAffectsRange_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_sizeAffectsRange_Injected(ref this, value);
				}
			}

			public bool alphaAffectsIntensity
			{
				get
				{
					return get_alphaAffectsIntensity_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_alphaAffectsIntensity_Injected(ref this, value);
				}
			}

			public MinMaxCurve range
			{
				get
				{
					get_range_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_range_Injected(ref this, ref value);
				}
			}

			public float rangeMultiplier
			{
				get
				{
					return get_rangeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_rangeMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxCurve intensity
			{
				get
				{
					get_intensity_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_intensity_Injected(ref this, ref value);
				}
			}

			public float intensityMultiplier
			{
				get
				{
					return get_intensityMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_intensityMultiplier_Injected(ref this, value);
				}
			}

			public int maxLights
			{
				get
				{
					return get_maxLights_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_maxLights_Injected(ref this, value);
				}
			}

			internal LightsModule(ParticleSystem particleSystem)
			{
				m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_enabled_Injected(ref LightsModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_enabled_Injected(ref LightsModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_ratio_Injected(ref LightsModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_ratio_Injected(ref LightsModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_useRandomDistribution_Injected(ref LightsModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_useRandomDistribution_Injected(ref LightsModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern Light get_light_Injected(ref LightsModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_light_Injected(ref LightsModule _unity_self, Light value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_useParticleColor_Injected(ref LightsModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_useParticleColor_Injected(ref LightsModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_sizeAffectsRange_Injected(ref LightsModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_sizeAffectsRange_Injected(ref LightsModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_alphaAffectsIntensity_Injected(ref LightsModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_alphaAffectsIntensity_Injected(ref LightsModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_range_Injected(ref LightsModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_range_Injected(ref LightsModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_rangeMultiplier_Injected(ref LightsModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_rangeMultiplier_Injected(ref LightsModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_intensity_Injected(ref LightsModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_intensity_Injected(ref LightsModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_intensityMultiplier_Injected(ref LightsModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_intensityMultiplier_Injected(ref LightsModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern int get_maxLights_Injected(ref LightsModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_maxLights_Injected(ref LightsModule _unity_self, int value);
		}

		public struct TrailModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_enabled_Injected(ref this, value);
				}
			}

			public ParticleSystemTrailMode mode
			{
				get
				{
					return get_mode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_mode_Injected(ref this, value);
				}
			}

			public float ratio
			{
				get
				{
					return get_ratio_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_ratio_Injected(ref this, value);
				}
			}

			public MinMaxCurve lifetime
			{
				get
				{
					get_lifetime_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_lifetime_Injected(ref this, ref value);
				}
			}

			public float lifetimeMultiplier
			{
				get
				{
					return get_lifetimeMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_lifetimeMultiplier_Injected(ref this, value);
				}
			}

			public float minVertexDistance
			{
				get
				{
					return get_minVertexDistance_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_minVertexDistance_Injected(ref this, value);
				}
			}

			public ParticleSystemTrailTextureMode textureMode
			{
				get
				{
					return get_textureMode_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_textureMode_Injected(ref this, value);
				}
			}

			public Vector2 textureScale
			{
				get
				{
					get_textureScale_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_textureScale_Injected(ref this, ref value);
				}
			}

			public bool worldSpace
			{
				get
				{
					return get_worldSpace_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_worldSpace_Injected(ref this, value);
				}
			}

			public bool dieWithParticles
			{
				get
				{
					return get_dieWithParticles_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_dieWithParticles_Injected(ref this, value);
				}
			}

			public bool sizeAffectsWidth
			{
				get
				{
					return get_sizeAffectsWidth_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_sizeAffectsWidth_Injected(ref this, value);
				}
			}

			public bool sizeAffectsLifetime
			{
				get
				{
					return get_sizeAffectsLifetime_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_sizeAffectsLifetime_Injected(ref this, value);
				}
			}

			public bool inheritParticleColor
			{
				get
				{
					return get_inheritParticleColor_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_inheritParticleColor_Injected(ref this, value);
				}
			}

			public MinMaxGradient colorOverLifetime
			{
				get
				{
					get_colorOverLifetime_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_colorOverLifetime_Injected(ref this, ref value);
				}
			}

			public MinMaxCurve widthOverTrail
			{
				get
				{
					get_widthOverTrail_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_widthOverTrail_Injected(ref this, ref value);
				}
			}

			public float widthOverTrailMultiplier
			{
				get
				{
					return get_widthOverTrailMultiplier_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_widthOverTrailMultiplier_Injected(ref this, value);
				}
			}

			public MinMaxGradient colorOverTrail
			{
				get
				{
					get_colorOverTrail_Injected(ref this, out var ret);
					return ret;
				}
				[NativeThrows]
				set
				{
					set_colorOverTrail_Injected(ref this, ref value);
				}
			}

			public bool generateLightingData
			{
				get
				{
					return get_generateLightingData_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_generateLightingData_Injected(ref this, value);
				}
			}

			public int ribbonCount
			{
				get
				{
					return get_ribbonCount_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_ribbonCount_Injected(ref this, value);
				}
			}

			public float shadowBias
			{
				get
				{
					return get_shadowBias_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_shadowBias_Injected(ref this, value);
				}
			}

			public bool splitSubEmitterRibbons
			{
				get
				{
					return get_splitSubEmitterRibbons_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_splitSubEmitterRibbons_Injected(ref this, value);
				}
			}

			public bool attachRibbonsToTransform
			{
				get
				{
					return get_attachRibbonsToTransform_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_attachRibbonsToTransform_Injected(ref this, value);
				}
			}

			internal TrailModule(ParticleSystem particleSystem)
			{
				m_ParticleSystem = particleSystem;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_enabled_Injected(ref TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_enabled_Injected(ref TrailModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemTrailMode get_mode_Injected(ref TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_mode_Injected(ref TrailModule _unity_self, ParticleSystemTrailMode value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_ratio_Injected(ref TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_ratio_Injected(ref TrailModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_lifetime_Injected(ref TrailModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_lifetime_Injected(ref TrailModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_lifetimeMultiplier_Injected(ref TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_lifetimeMultiplier_Injected(ref TrailModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_minVertexDistance_Injected(ref TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_minVertexDistance_Injected(ref TrailModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern ParticleSystemTrailTextureMode get_textureMode_Injected(ref TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_textureMode_Injected(ref TrailModule _unity_self, ParticleSystemTrailTextureMode value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_textureScale_Injected(ref TrailModule _unity_self, out Vector2 ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_textureScale_Injected(ref TrailModule _unity_self, ref Vector2 value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_worldSpace_Injected(ref TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_worldSpace_Injected(ref TrailModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_dieWithParticles_Injected(ref TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_dieWithParticles_Injected(ref TrailModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_sizeAffectsWidth_Injected(ref TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_sizeAffectsWidth_Injected(ref TrailModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_sizeAffectsLifetime_Injected(ref TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_sizeAffectsLifetime_Injected(ref TrailModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_inheritParticleColor_Injected(ref TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_inheritParticleColor_Injected(ref TrailModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_colorOverLifetime_Injected(ref TrailModule _unity_self, out MinMaxGradient ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_colorOverLifetime_Injected(ref TrailModule _unity_self, ref MinMaxGradient value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_widthOverTrail_Injected(ref TrailModule _unity_self, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_widthOverTrail_Injected(ref TrailModule _unity_self, ref MinMaxCurve value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_widthOverTrailMultiplier_Injected(ref TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_widthOverTrailMultiplier_Injected(ref TrailModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void get_colorOverTrail_Injected(ref TrailModule _unity_self, out MinMaxGradient ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_colorOverTrail_Injected(ref TrailModule _unity_self, ref MinMaxGradient value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_generateLightingData_Injected(ref TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_generateLightingData_Injected(ref TrailModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern int get_ribbonCount_Injected(ref TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_ribbonCount_Injected(ref TrailModule _unity_self, int value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern float get_shadowBias_Injected(ref TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_shadowBias_Injected(ref TrailModule _unity_self, float value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_splitSubEmitterRibbons_Injected(ref TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_splitSubEmitterRibbons_Injected(ref TrailModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_attachRibbonsToTransform_Injected(ref TrailModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_attachRibbonsToTransform_Injected(ref TrailModule _unity_self, bool value);
		}

		public struct CustomDataModule
		{
			internal ParticleSystem m_ParticleSystem;

			public bool enabled
			{
				get
				{
					return get_enabled_Injected(ref this);
				}
				[NativeThrows]
				set
				{
					set_enabled_Injected(ref this, value);
				}
			}

			internal CustomDataModule(ParticleSystem particleSystem)
			{
				m_ParticleSystem = particleSystem;
			}

			[NativeThrows]
			public void SetMode(ParticleSystemCustomData stream, ParticleSystemCustomDataMode mode)
			{
				SetMode_Injected(ref this, stream, mode);
			}

			[NativeThrows]
			public ParticleSystemCustomDataMode GetMode(ParticleSystemCustomData stream)
			{
				return GetMode_Injected(ref this, stream);
			}

			[NativeThrows]
			public void SetVectorComponentCount(ParticleSystemCustomData stream, int count)
			{
				SetVectorComponentCount_Injected(ref this, stream, count);
			}

			[NativeThrows]
			public int GetVectorComponentCount(ParticleSystemCustomData stream)
			{
				return GetVectorComponentCount_Injected(ref this, stream);
			}

			[NativeThrows]
			public void SetVector(ParticleSystemCustomData stream, int component, MinMaxCurve curve)
			{
				SetVector_Injected(ref this, stream, component, ref curve);
			}

			[NativeThrows]
			public MinMaxCurve GetVector(ParticleSystemCustomData stream, int component)
			{
				GetVector_Injected(ref this, stream, component, out var ret);
				return ret;
			}

			[NativeThrows]
			public void SetColor(ParticleSystemCustomData stream, MinMaxGradient gradient)
			{
				SetColor_Injected(ref this, stream, ref gradient);
			}

			[NativeThrows]
			public MinMaxGradient GetColor(ParticleSystemCustomData stream)
			{
				GetColor_Injected(ref this, stream, out var ret);
				return ret;
			}

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern bool get_enabled_Injected(ref CustomDataModule _unity_self);

			[MethodImpl(MethodImplOptions.InternalCall)]
			[SpecialName]
			private static extern void set_enabled_Injected(ref CustomDataModule _unity_self, bool value);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetMode_Injected(ref CustomDataModule _unity_self, ParticleSystemCustomData stream, ParticleSystemCustomDataMode mode);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ParticleSystemCustomDataMode GetMode_Injected(ref CustomDataModule _unity_self, ParticleSystemCustomData stream);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetVectorComponentCount_Injected(ref CustomDataModule _unity_self, ParticleSystemCustomData stream, int count);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetVectorComponentCount_Injected(ref CustomDataModule _unity_self, ParticleSystemCustomData stream);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetVector_Injected(ref CustomDataModule _unity_self, ParticleSystemCustomData stream, int component, ref MinMaxCurve curve);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetVector_Injected(ref CustomDataModule _unity_self, ParticleSystemCustomData stream, int component, out MinMaxCurve ret);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetColor_Injected(ref CustomDataModule _unity_self, ParticleSystemCustomData stream, ref MinMaxGradient gradient);

			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetColor_Injected(ref CustomDataModule _unity_self, ParticleSystemCustomData stream, out MinMaxGradient ret);
		}

		[Obsolete("startDelay property is deprecated. Use main.startDelay or main.startDelayMultiplier instead.", false)]
		public float startDelay
		{
			get
			{
				return main.startDelayMultiplier;
			}
			set
			{
				MainModule mainModule = main;
				mainModule.startDelayMultiplier = value;
			}
		}

		[Obsolete("loop property is deprecated. Use main.loop instead.", false)]
		public bool loop
		{
			get
			{
				return main.loop;
			}
			set
			{
				MainModule mainModule = main;
				mainModule.loop = value;
			}
		}

		[Obsolete("playOnAwake property is deprecated. Use main.playOnAwake instead.", false)]
		public bool playOnAwake
		{
			get
			{
				return main.playOnAwake;
			}
			set
			{
				MainModule mainModule = main;
				mainModule.playOnAwake = value;
			}
		}

		[Obsolete("duration property is deprecated. Use main.duration instead.", false)]
		public float duration => main.duration;

		[Obsolete("playbackSpeed property is deprecated. Use main.simulationSpeed instead.", false)]
		public float playbackSpeed
		{
			get
			{
				return main.simulationSpeed;
			}
			set
			{
				MainModule mainModule = main;
				mainModule.simulationSpeed = value;
			}
		}

		[Obsolete("enableEmission property is deprecated. Use emission.enabled instead.", false)]
		public bool enableEmission
		{
			get
			{
				return emission.enabled;
			}
			set
			{
				EmissionModule emissionModule = emission;
				emissionModule.enabled = value;
			}
		}

		[Obsolete("emissionRate property is deprecated. Use emission.rateOverTime, emission.rateOverDistance, emission.rateOverTimeMultiplier or emission.rateOverDistanceMultiplier instead.", false)]
		public float emissionRate
		{
			get
			{
				return emission.rateOverTimeMultiplier;
			}
			set
			{
				EmissionModule emissionModule = emission;
				emissionModule.rateOverTime = value;
			}
		}

		[Obsolete("startSpeed property is deprecated. Use main.startSpeed or main.startSpeedMultiplier instead.", false)]
		public float startSpeed
		{
			get
			{
				return main.startSpeedMultiplier;
			}
			set
			{
				MainModule mainModule = main;
				mainModule.startSpeedMultiplier = value;
			}
		}

		[Obsolete("startSize property is deprecated. Use main.startSize or main.startSizeMultiplier instead.", false)]
		public float startSize
		{
			get
			{
				return main.startSizeMultiplier;
			}
			set
			{
				MainModule mainModule = main;
				mainModule.startSizeMultiplier = value;
			}
		}

		[Obsolete("startColor property is deprecated. Use main.startColor instead.", false)]
		public Color startColor
		{
			get
			{
				return main.startColor.color;
			}
			set
			{
				MainModule mainModule = main;
				mainModule.startColor = value;
			}
		}

		[Obsolete("startRotation property is deprecated. Use main.startRotation or main.startRotationMultiplier instead.", false)]
		public float startRotation
		{
			get
			{
				return main.startRotationMultiplier;
			}
			set
			{
				MainModule mainModule = main;
				mainModule.startRotationMultiplier = value;
			}
		}

		[Obsolete("startRotation3D property is deprecated. Use main.startRotationX, main.startRotationY and main.startRotationZ instead. (Or main.startRotationXMultiplier, main.startRotationYMultiplier and main.startRotationZMultiplier).", false)]
		public Vector3 startRotation3D
		{
			get
			{
				return new Vector3(main.startRotationXMultiplier, main.startRotationYMultiplier, main.startRotationZMultiplier);
			}
			set
			{
				MainModule mainModule = main;
				mainModule.startRotationXMultiplier = value.x;
				mainModule.startRotationYMultiplier = value.y;
				mainModule.startRotationZMultiplier = value.z;
			}
		}

		[Obsolete("startLifetime property is deprecated. Use main.startLifetime or main.startLifetimeMultiplier instead.", false)]
		public float startLifetime
		{
			get
			{
				return main.startLifetimeMultiplier;
			}
			set
			{
				MainModule mainModule = main;
				mainModule.startLifetimeMultiplier = value;
			}
		}

		[Obsolete("gravityModifier property is deprecated. Use main.gravityModifier or main.gravityModifierMultiplier instead.", false)]
		public float gravityModifier
		{
			get
			{
				return main.gravityModifierMultiplier;
			}
			set
			{
				MainModule mainModule = main;
				mainModule.gravityModifierMultiplier = value;
			}
		}

		[Obsolete("maxParticles property is deprecated. Use main.maxParticles instead.", false)]
		public int maxParticles
		{
			get
			{
				return main.maxParticles;
			}
			set
			{
				MainModule mainModule = main;
				mainModule.maxParticles = value;
			}
		}

		[Obsolete("simulationSpace property is deprecated. Use main.simulationSpace instead.", false)]
		public ParticleSystemSimulationSpace simulationSpace
		{
			get
			{
				return main.simulationSpace;
			}
			set
			{
				MainModule mainModule = main;
				mainModule.simulationSpace = value;
			}
		}

		[Obsolete("scalingMode property is deprecated. Use main.scalingMode instead.", false)]
		public ParticleSystemScalingMode scalingMode
		{
			get
			{
				return main.scalingMode;
			}
			set
			{
				MainModule mainModule = main;
				mainModule.scalingMode = value;
			}
		}

		[Obsolete("automaticCullingEnabled property is deprecated. Use proceduralSimulationSupported instead (UnityUpgradable) -> proceduralSimulationSupported", true)]
		public bool automaticCullingEnabled => proceduralSimulationSupported;

		public extern bool isPlaying
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("SyncJobs(false)->IsPlaying")]
			get;
		}

		public extern bool isEmitting
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("SyncJobs(false)->IsEmitting")]
			get;
		}

		public extern bool isStopped
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("SyncJobs(false)->IsStopped")]
			get;
		}

		public extern bool isPaused
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("SyncJobs(false)->IsPaused")]
			get;
		}

		public extern int particleCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("SyncJobs(false)->GetParticleCount")]
			get;
		}

		public extern float time
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("SyncJobs(false)->GetSecPosition")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("SyncJobs(false)->SetSecPosition")]
			set;
		}

		public extern float totalTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("SyncJobs(false)->GetTotalSecPosition")]
			get;
		}

		public extern uint randomSeed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetRandomSeed")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("SyncJobs(false)->SetRandomSeed")]
			set;
		}

		public extern bool useAutoRandomSeed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetAutoRandomSeed")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("SyncJobs(false)->SetAutoRandomSeed")]
			set;
		}

		public extern bool proceduralSimulationSupported
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool has3DParticleRotations
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("Has3DParticleRotations")]
			get;
		}

		public extern bool hasNonUniformParticleSizes
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("HasNonUniformParticleSizes")]
			get;
		}

		public MainModule main => new MainModule(this);

		public EmissionModule emission => new EmissionModule(this);

		public ShapeModule shape => new ShapeModule(this);

		public VelocityOverLifetimeModule velocityOverLifetime => new VelocityOverLifetimeModule(this);

		public LimitVelocityOverLifetimeModule limitVelocityOverLifetime => new LimitVelocityOverLifetimeModule(this);

		public InheritVelocityModule inheritVelocity => new InheritVelocityModule(this);

		public LifetimeByEmitterSpeedModule lifetimeByEmitterSpeed => new LifetimeByEmitterSpeedModule(this);

		public ForceOverLifetimeModule forceOverLifetime => new ForceOverLifetimeModule(this);

		public ColorOverLifetimeModule colorOverLifetime => new ColorOverLifetimeModule(this);

		public ColorBySpeedModule colorBySpeed => new ColorBySpeedModule(this);

		public SizeOverLifetimeModule sizeOverLifetime => new SizeOverLifetimeModule(this);

		public SizeBySpeedModule sizeBySpeed => new SizeBySpeedModule(this);

		public RotationOverLifetimeModule rotationOverLifetime => new RotationOverLifetimeModule(this);

		public RotationBySpeedModule rotationBySpeed => new RotationBySpeedModule(this);

		public ExternalForcesModule externalForces => new ExternalForcesModule(this);

		public NoiseModule noise => new NoiseModule(this);

		public CollisionModule collision => new CollisionModule(this);

		public TriggerModule trigger => new TriggerModule(this);

		public SubEmittersModule subEmitters => new SubEmittersModule(this);

		public TextureSheetAnimationModule textureSheetAnimation => new TextureSheetAnimationModule(this);

		public LightsModule lights => new LightsModule(this);

		public TrailModule trails => new TrailModule(this);

		public CustomDataModule customData => new CustomDataModule(this);

		[Obsolete("Emit with specific parameters is deprecated. Pass a ParticleSystem.EmitParams parameter instead, which allows you to override some/all of the emission properties", false)]
		public void Emit(Vector3 position, Vector3 velocity, float size, float lifetime, Color32 color)
		{
			Particle particle = new Particle
			{
				position = position,
				velocity = velocity,
				lifetime = lifetime,
				startLifetime = lifetime,
				startSize = size,
				rotation3D = Vector3.zero,
				angularVelocity3D = Vector3.zero,
				startColor = color,
				randomSeed = 5u
			};
			EmitOld_Internal(ref particle);
		}

		[Obsolete("Emit with a single particle structure is deprecated. Pass a ParticleSystem.EmitParams parameter instead, which allows you to override some/all of the emission properties", false)]
		public void Emit(Particle particle)
		{
			EmitOld_Internal(ref particle);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticleCurrentSize", HasExplicitThis = true)]
		internal extern float GetParticleCurrentSize(ref Particle particle);

		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticleCurrentSize3D", HasExplicitThis = true)]
		internal Vector3 GetParticleCurrentSize3D(ref Particle particle)
		{
			GetParticleCurrentSize3D_Injected(ref particle, out var ret);
			return ret;
		}

		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticleCurrentColor", HasExplicitThis = true)]
		internal Color32 GetParticleCurrentColor(ref Particle particle)
		{
			GetParticleCurrentColor_Injected(ref particle, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticleMeshIndex", HasExplicitThis = true)]
		internal extern int GetParticleMeshIndex(ref Particle particle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemScriptBindings::SetParticles", HasExplicitThis = true, ThrowsException = true)]
		public extern void SetParticles([Out] Particle[] particles, int size, int offset);

		public void SetParticles([Out] Particle[] particles, int size)
		{
			SetParticles(particles, size, 0);
		}

		public void SetParticles([Out] Particle[] particles)
		{
			SetParticles(particles, -1);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemScriptBindings::SetParticlesWithNativeArray", HasExplicitThis = true, ThrowsException = true)]
		private extern void SetParticlesWithNativeArray(IntPtr particles, int particlesLength, int size, int offset);

		public unsafe void SetParticles([Out] NativeArray<Particle> particles, int size, int offset)
		{
			SetParticlesWithNativeArray((IntPtr)particles.GetUnsafeReadOnlyPtr(), particles.Length, size, offset);
		}

		public void SetParticles([Out] NativeArray<Particle> particles, int size)
		{
			SetParticles(particles, size, 0);
		}

		public void SetParticles([Out] NativeArray<Particle> particles)
		{
			SetParticles(particles, -1);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticles", HasExplicitThis = true, ThrowsException = true)]
		public extern int GetParticles([Out][NotNull("ArgumentNullException")] Particle[] particles, int size, int offset);

		public int GetParticles([Out] Particle[] particles, int size)
		{
			return GetParticles(particles, size, 0);
		}

		public int GetParticles([Out] Particle[] particles)
		{
			return GetParticles(particles, -1);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetParticlesWithNativeArray", HasExplicitThis = true, ThrowsException = true)]
		private extern int GetParticlesWithNativeArray(IntPtr particles, int particlesLength, int size, int offset);

		public unsafe int GetParticles([Out] NativeArray<Particle> particles, int size, int offset)
		{
			return GetParticlesWithNativeArray((IntPtr)particles.GetUnsafePtr(), particles.Length, size, offset);
		}

		public int GetParticles([Out] NativeArray<Particle> particles, int size)
		{
			return GetParticles(particles, size, 0);
		}

		public int GetParticles([Out] NativeArray<Particle> particles)
		{
			return GetParticles(particles, -1);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemScriptBindings::SetCustomParticleData", HasExplicitThis = true, ThrowsException = true)]
		public extern void SetCustomParticleData([NotNull("ArgumentNullException")] List<Vector4> customData, ParticleSystemCustomData streamIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetCustomParticleData", HasExplicitThis = true, ThrowsException = true)]
		public extern int GetCustomParticleData([NotNull("ArgumentNullException")] List<Vector4> customData, ParticleSystemCustomData streamIndex);

		public PlaybackState GetPlaybackState()
		{
			GetPlaybackState_Injected(out var ret);
			return ret;
		}

		public void SetPlaybackState(PlaybackState playbackState)
		{
			SetPlaybackState_Injected(ref playbackState);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetTrailData", HasExplicitThis = true)]
		private extern void GetTrailDataInternal(ref Trails trailData);

		public Trails GetTrails()
		{
			Trails trailData = default(Trails);
			trailData.Allocate();
			GetTrailDataInternal(ref trailData);
			return trailData;
		}

		public int GetTrails(ref Trails trailData)
		{
			trailData.Allocate();
			GetTrailDataInternal(ref trailData);
			return trailData.positions.Count;
		}

		[FreeFunction(Name = "ParticleSystemScriptBindings::SetTrailData", HasExplicitThis = true)]
		public void SetTrails(Trails trailData)
		{
			SetTrails_Injected(ref trailData);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemScriptBindings::Simulate", HasExplicitThis = true)]
		public extern void Simulate(float t, [DefaultValue("true")] bool withChildren, [DefaultValue("true")] bool restart, [DefaultValue("true")] bool fixedTimeStep);

		public void Simulate(float t, [DefaultValue("true")] bool withChildren, [DefaultValue("true")] bool restart)
		{
			Simulate(t, withChildren, restart, fixedTimeStep: true);
		}

		public void Simulate(float t, [DefaultValue("true")] bool withChildren)
		{
			Simulate(t, withChildren, restart: true);
		}

		public void Simulate(float t)
		{
			Simulate(t, withChildren: true);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemScriptBindings::Play", HasExplicitThis = true)]
		public extern void Play([DefaultValue("true")] bool withChildren);

		public void Play()
		{
			Play(withChildren: true);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemScriptBindings::Pause", HasExplicitThis = true)]
		public extern void Pause([DefaultValue("true")] bool withChildren);

		public void Pause()
		{
			Pause(withChildren: true);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemScriptBindings::Stop", HasExplicitThis = true)]
		public extern void Stop([DefaultValue("true")] bool withChildren, [DefaultValue("ParticleSystemStopBehavior.StopEmitting")] ParticleSystemStopBehavior stopBehavior);

		public void Stop([DefaultValue("true")] bool withChildren)
		{
			Stop(withChildren, ParticleSystemStopBehavior.StopEmitting);
		}

		public void Stop()
		{
			Stop(withChildren: true);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemScriptBindings::Clear", HasExplicitThis = true)]
		public extern void Clear([DefaultValue("true")] bool withChildren);

		public void Clear()
		{
			Clear(withChildren: true);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemScriptBindings::IsAlive", HasExplicitThis = true)]
		public extern bool IsAlive([DefaultValue("true")] bool withChildren);

		public bool IsAlive()
		{
			return IsAlive(withChildren: true);
		}

		[RequiredByNativeCode]
		public void Emit(int count)
		{
			Emit_Internal(count);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("SyncJobs()->Emit")]
		private extern void Emit_Internal(int count);

		[NativeName("SyncJobs()->EmitParticlesExternal")]
		public void Emit(EmitParams emitParams, int count)
		{
			Emit_Injected(ref emitParams, count);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("SyncJobs()->EmitParticleExternal")]
		private extern void EmitOld_Internal(ref Particle particle);

		public void TriggerSubEmitter(int subEmitterIndex)
		{
			TriggerSubEmitter(subEmitterIndex, null);
		}

		public void TriggerSubEmitter(int subEmitterIndex, ref Particle particle)
		{
			TriggerSubEmitterForParticle(subEmitterIndex, particle);
		}

		[FreeFunction(Name = "ParticleSystemScriptBindings::TriggerSubEmitterForParticle", HasExplicitThis = true)]
		internal void TriggerSubEmitterForParticle(int subEmitterIndex, Particle particle)
		{
			TriggerSubEmitterForParticle_Injected(subEmitterIndex, ref particle);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemScriptBindings::TriggerSubEmitter", HasExplicitThis = true)]
		public extern void TriggerSubEmitter(int subEmitterIndex, List<Particle> particles);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemGeometryJob::ResetPreMappedBufferMemory")]
		public static extern void ResetPreMappedBufferMemory();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemGeometryJob::SetMaximumPreMappedBufferCounts")]
		public static extern void SetMaximumPreMappedBufferCounts(int vertexBuffersCount, int indexBuffersCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("SetUsesAxisOfRotation")]
		public extern void AllocateAxisOfRotationAttribute();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("SetUsesMeshIndex")]
		public extern void AllocateMeshIndexAttribute();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("SetUsesCustomData")]
		public extern void AllocateCustomDataAttribute(ParticleSystemCustomData stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe extern void* GetManagedJobData();

		internal JobHandle GetManagedJobHandle()
		{
			GetManagedJobHandle_Injected(out var ret);
			return ret;
		}

		internal void SetManagedJobHandle(JobHandle handle)
		{
			SetManagedJobHandle_Injected(ref handle);
		}

		[FreeFunction("ScheduleManagedJob", ThrowsException = true)]
		internal unsafe static JobHandle ScheduleManagedJob(ref JobsUtility.JobScheduleParameters parameters, void* additionalData)
		{
			ScheduleManagedJob_Injected(ref parameters, additionalData, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		internal unsafe static extern void CopyManagedJobData(void* systemPtr, out NativeParticleData particleData);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetParticleCurrentSize3D_Injected(ref Particle particle, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetParticleCurrentColor_Injected(ref Particle particle, out Color32 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPlaybackState_Injected(out PlaybackState ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetPlaybackState_Injected(ref PlaybackState playbackState);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTrails_Injected(ref Trails trailData);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Emit_Injected(ref EmitParams emitParams, int count);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void TriggerSubEmitterForParticle_Injected(int subEmitterIndex, ref Particle particle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetManagedJobHandle_Injected(out JobHandle ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetManagedJobHandle_Injected(ref JobHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ScheduleManagedJob_Injected(ref JobsUtility.JobScheduleParameters parameters, void* additionalData, out JobHandle ret);
	}
	public static class ParticlePhysicsExtensions
	{
		[Obsolete("GetCollisionEvents function using ParticleCollisionEvent[] is deprecated. Use List<ParticleCollisionEvent> instead.", false)]
		public static int GetCollisionEvents(this ParticleSystem ps, GameObject go, ParticleCollisionEvent[] collisionEvents)
		{
			if (go == null)
			{
				throw new ArgumentNullException("go");
			}
			if (collisionEvents == null)
			{
				throw new ArgumentNullException("collisionEvents");
			}
			return ParticleSystemExtensionsImpl.GetCollisionEventsDeprecated(ps, go, collisionEvents);
		}

		public static int GetSafeCollisionEventSize(this ParticleSystem ps)
		{
			return ParticleSystemExtensionsImpl.GetSafeCollisionEventSize(ps);
		}

		public static int GetCollisionEvents(this ParticleSystem ps, GameObject go, List<ParticleCollisionEvent> collisionEvents)
		{
			return ParticleSystemExtensionsImpl.GetCollisionEvents(ps, go, collisionEvents);
		}

		public static int GetSafeTriggerParticlesSize(this ParticleSystem ps, ParticleSystemTriggerEventType type)
		{
			return ParticleSystemExtensionsImpl.GetSafeTriggerParticlesSize(ps, (int)type);
		}

		public static int GetTriggerParticles(this ParticleSystem ps, ParticleSystemTriggerEventType type, List<ParticleSystem.Particle> particles)
		{
			return ParticleSystemExtensionsImpl.GetTriggerParticles(ps, (int)type, particles);
		}

		public static int GetTriggerParticles(this ParticleSystem ps, ParticleSystemTriggerEventType type, List<ParticleSystem.Particle> particles, out ParticleSystem.ColliderData colliderData)
		{
			switch (type)
			{
			case ParticleSystemTriggerEventType.Exit:
				throw new InvalidOperationException("Querying the collider data for the Exit event is not currently supported.");
			case ParticleSystemTriggerEventType.Outside:
				throw new InvalidOperationException("Querying the collider data for the Outside event is not supported, because when a particle is outside the collision volume, it is always outside every collider.");
			default:
				colliderData = default(ParticleSystem.ColliderData);
				return ParticleSystemExtensionsImpl.GetTriggerParticlesWithData(ps, (int)type, particles, ref colliderData);
			}
		}

		public static void SetTriggerParticles(this ParticleSystem ps, ParticleSystemTriggerEventType type, List<ParticleSystem.Particle> particles, int offset, int count)
		{
			if (particles == null)
			{
				throw new ArgumentNullException("particles");
			}
			if (offset >= particles.Count)
			{
				throw new ArgumentOutOfRangeException("offset", "offset should be smaller than the size of the particles list.");
			}
			if (offset + count >= particles.Count)
			{
				throw new ArgumentOutOfRangeException("count", "offset+count should be smaller than the size of the particles list.");
			}
			ParticleSystemExtensionsImpl.SetTriggerParticles(ps, (int)type, particles, offset, count);
		}

		public static void SetTriggerParticles(this ParticleSystem ps, ParticleSystemTriggerEventType type, List<ParticleSystem.Particle> particles)
		{
			ParticleSystemExtensionsImpl.SetTriggerParticles(ps, (int)type, particles, 0, particles.Count);
		}
	}
	public enum ParticleSystemRenderMode
	{
		Billboard,
		Stretch,
		HorizontalBillboard,
		VerticalBillboard,
		Mesh,
		None
	}
	public enum ParticleSystemMeshDistribution
	{
		UniformRandom,
		NonUniformRandom
	}
	public enum ParticleSystemSortMode
	{
		None,
		Distance,
		OldestInFront,
		YoungestInFront,
		Depth
	}
	public enum ParticleSystemCollisionQuality
	{
		High,
		Medium,
		Low
	}
	public enum ParticleSystemRenderSpace
	{
		View,
		World,
		Local,
		Facing,
		Velocity
	}
	public enum ParticleSystemCurveMode
	{
		Constant,
		Curve,
		TwoCurves,
		TwoConstants
	}
	public enum ParticleSystemGradientMode
	{
		Color,
		Gradient,
		TwoColors,
		TwoGradients,
		RandomColor
	}
	public enum ParticleSystemShapeType
	{
		Sphere,
		[Obsolete("SphereShell is deprecated and does nothing. Please use ShapeModule.radiusThickness instead, to control edge emission.", false)]
		SphereShell,
		Hemisphere,
		[Obsolete("HemisphereShell is deprecated and does nothing. Please use ShapeModule.radiusThickness instead, to control edge emission.", false)]
		HemisphereShell,
		Cone,
		Box,
		Mesh,
		[Obsolete("ConeShell is deprecated and does nothing. Please use ShapeModule.radiusThickness instead, to control edge emission.", false)]
		ConeShell,
		ConeVolume,
		[Obsolete("ConeVolumeShell is deprecated and does nothing. Please use ShapeModule.radiusThickness instead, to control edge emission.", false)]
		ConeVolumeShell,
		Circle,
		[Obsolete("CircleEdge is deprecated and does nothing. Please use ShapeModule.radiusThickness instead, to control edge emission.", false)]
		CircleEdge,
		SingleSidedEdge,
		MeshRenderer,
		SkinnedMeshRenderer,
		BoxShell,
		BoxEdge,
		Donut,
		Rectangle,
		Sprite,
		SpriteRenderer
	}
	public enum ParticleSystemMeshShapeType
	{
		Vertex,
		Edge,
		Triangle
	}
	public enum ParticleSystemShapeTextureChannel
	{
		Red,
		Green,
		Blue,
		Alpha
	}
	public enum ParticleSystemAnimationMode
	{
		Grid,
		Sprites
	}
	public enum ParticleSystemAnimationTimeMode
	{
		Lifetime,
		Speed,
		FPS
	}
	public enum ParticleSystemAnimationType
	{
		WholeSheet,
		SingleRow
	}
	public enum ParticleSystemAnimationRowMode
	{
		Custom,
		Random,
		MeshIndex
	}
	public enum ParticleSystemCollisionType
	{
		Planes,
		World
	}
	public enum ParticleSystemCollisionMode
	{
		Collision3D,
		Collision2D
	}
	public enum ParticleSystemOverlapAction
	{
		Ignore,
		Kill,
		Callback
	}
	public enum ParticleSystemColliderQueryMode
	{
		Disabled,
		One,
		All
	}
	public enum ParticleSystemSimulationSpace
	{
		Local,
		World,
		Custom
	}
	public enum ParticleSystemStopBehavior
	{
		StopEmittingAndClear,
		StopEmitting
	}
	public enum ParticleSystemScalingMode
	{
		Hierarchy,
		Local,
		Shape
	}
	public enum ParticleSystemStopAction
	{
		None,
		Disable,
		Destroy,
		Callback
	}
	public enum ParticleSystemCullingMode
	{
		Automatic,
		PauseAndCatchup,
		Pause,
		AlwaysSimulate
	}
	public enum ParticleSystemEmitterVelocityMode
	{
		Transform,
		Rigidbody,
		Custom
	}
	public enum ParticleSystemGravitySource
	{
		Physics3D,
		Physics2D
	}
	public enum ParticleSystemInheritVelocityMode
	{
		Initial,
		Current
	}
	public enum ParticleSystemTriggerEventType
	{
		Inside,
		Outside,
		Enter,
		Exit
	}
	[UsedByNativeCode]
	public enum ParticleSystemVertexStream
	{
		Position,
		Normal,
		Tangent,
		Color,
		UV,
		UV2,
		UV3,
		UV4,
		AnimBlend,
		AnimFrame,
		Center,
		VertexID,
		SizeX,
		SizeXY,
		SizeXYZ,
		Rotation,
		Rotation3D,
		RotationSpeed,
		RotationSpeed3D,
		Velocity,
		Speed,
		AgePercent,
		InvStartLifetime,
		StableRandomX,
		StableRandomXY,
		StableRandomXYZ,
		StableRandomXYZW,
		VaryingRandomX,
		VaryingRandomXY,
		VaryingRandomXYZ,
		VaryingRandomXYZW,
		Custom1X,
		Custom1XY,
		Custom1XYZ,
		Custom1XYZW,
		Custom2X,
		Custom2XY,
		Custom2XYZ,
		Custom2XYZW,
		NoiseSumX,
		NoiseSumXY,
		NoiseSumXYZ,
		NoiseImpulseX,
		NoiseImpulseXY,
		NoiseImpulseXYZ,
		MeshIndex,
		ParticleIndex,
		ColorPackedAsTwoFloats,
		MeshAxisOfRotation,
		NextTrailCenter,
		PreviousTrailCenter,
		PercentageAlongTrail,
		TrailWidth
	}
	public enum ParticleSystemCustomData
	{
		Custom1,
		Custom2
	}
	public enum ParticleSystemCustomDataMode
	{
		Disabled,
		Vector,
		Color
	}
	public enum ParticleSystemNoiseQuality
	{
		Low,
		Medium,
		High
	}
	public enum ParticleSystemSubEmitterType
	{
		Birth,
		Collision,
		Death,
		Trigger,
		Manual
	}
	[Flags]
	public enum ParticleSystemSubEmitterProperties
	{
		InheritNothing = 0,
		InheritEverything = 0x1F,
		InheritColor = 1,
		InheritSize = 2,
		InheritRotation = 4,
		InheritLifetime = 8,
		InheritDuration = 0x10
	}
	public enum ParticleSystemTrailMode
	{
		PerParticle,
		Ribbon
	}
	public enum ParticleSystemTrailTextureMode
	{
		Stretch,
		Tile,
		DistributePerSegment,
		RepeatPerSegment,
		Static
	}
	public enum ParticleSystemShapeMultiModeValue
	{
		Random,
		Loop,
		PingPong,
		BurstSpread
	}
	public enum ParticleSystemRingBufferMode
	{
		Disabled,
		PauseUntilReplaced,
		LoopUntilReplaced
	}
	public enum ParticleSystemGameObjectFilter
	{
		LayerMask,
		List,
		LayerMaskAndList
	}
	public enum ParticleSystemForceFieldShape
	{
		Sphere,
		Hemisphere,
		Cylinder,
		Box
	}
	[Flags]
	public enum ParticleSystemBakeMeshOptions
	{
		BakeRotationAndScale = 1,
		BakePosition = 2,
		Default = 0
	}
	[Flags]
	public enum ParticleSystemBakeTextureOptions
	{
		BakeRotationAndScale = 1,
		BakePosition = 2,
		PerVertex = 4,
		PerParticle = 8,
		IncludeParticleIndices = 0x10,
		Default = 4
	}
	[Flags]
	[Obsolete("ParticleSystemVertexStreams is deprecated. Please use ParticleSystemVertexStream instead.", false)]
	public enum ParticleSystemVertexStreams
	{
		Position = 1,
		Normal = 2,
		Tangent = 4,
		Color = 8,
		UV = 0x10,
		UV2BlendAndFrame = 0x20,
		CenterAndVertexID = 0x40,
		Size = 0x80,
		Rotation = 0x100,
		Velocity = 0x200,
		Lifetime = 0x400,
		Custom1 = 0x800,
		Custom2 = 0x1000,
		Random = 0x2000,
		None = 0,
		All = int.MaxValue
	}
	[NativeHeader("Modules/ParticleSystem/ParticleSystemRenderer.h")]
	[NativeHeader("Modules/ParticleSystem/ScriptBindings/ParticleSystemRendererScriptBindings.h")]
	[NativeHeader("ParticleSystemScriptingClasses.h")]
	[RequireComponent(typeof(Transform))]
	public sealed class ParticleSystemRenderer : Renderer
	{
		internal struct BakeTextureOutput
		{
			[NativeName("first")]
			internal Texture2D vertices;

			[NativeName("second")]
			internal Texture2D indices;
		}

		[NativeName("RenderAlignment")]
		public extern ParticleSystemRenderSpace alignment
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern ParticleSystemRenderMode renderMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern ParticleSystemMeshDistribution meshDistribution
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern ParticleSystemSortMode sortMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float lengthScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float velocityScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float cameraVelocityScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float normalDirection
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float shadowBias
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float sortingFudge
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float minParticleSize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float maxParticleSize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 pivot
		{
			get
			{
				get_pivot_Injected(out var ret);
				return ret;
			}
			set
			{
				set_pivot_Injected(ref value);
			}
		}

		public Vector3 flip
		{
			get
			{
				get_flip_Injected(out var ret);
				return ret;
			}
			set
			{
				set_flip_Injected(ref value);
			}
		}

		public extern SpriteMaskInteraction maskInteraction
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Material trailMaterial
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal extern Material oldTrailMaterial
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool enableGPUInstancing
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool allowRoll
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool freeformStretching
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool rotateWithStretchDirection
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Mesh mesh
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction(Name = "ParticleSystemRendererScriptBindings::GetMesh", HasExplicitThis = true)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction(Name = "ParticleSystemRendererScriptBindings::SetMesh", HasExplicitThis = true)]
			set;
		}

		public extern int meshCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int activeVertexStreamsCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int activeTrailVertexStreamsCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[Obsolete("EnableVertexStreams is deprecated. Use SetActiveVertexStreams instead.", false)]
		public void EnableVertexStreams(ParticleSystemVertexStreams streams)
		{
			Internal_SetVertexStreams(streams, enabled: true);
		}

		[Obsolete("DisableVertexStreams is deprecated. Use SetActiveVertexStreams instead.", false)]
		public void DisableVertexStreams(ParticleSystemVertexStreams streams)
		{
			Internal_SetVertexStreams(streams, enabled: false);
		}

		[Obsolete("AreVertexStreamsEnabled is deprecated. Use GetActiveVertexStreams instead.", false)]
		public bool AreVertexStreamsEnabled(ParticleSystemVertexStreams streams)
		{
			return Internal_GetEnabledVertexStreams(streams) == streams;
		}

		[Obsolete("GetEnabledVertexStreams is deprecated. Use GetActiveVertexStreams instead.", false)]
		public ParticleSystemVertexStreams GetEnabledVertexStreams(ParticleSystemVertexStreams streams)
		{
			return Internal_GetEnabledVertexStreams(streams);
		}

		[Obsolete("Internal_SetVertexStreams is deprecated. Use SetActiveVertexStreams instead.", false)]
		internal void Internal_SetVertexStreams(ParticleSystemVertexStreams streams, bool enabled)
		{
			List<ParticleSystemVertexStream> list = new List<ParticleSystemVertexStream>(activeVertexStreamsCount);
			GetActiveVertexStreams(list);
			if (enabled)
			{
				if ((streams & ParticleSystemVertexStreams.Position) != ParticleSystemVertexStreams.None && !list.Contains(ParticleSystemVertexStream.Position))
				{
					list.Add(ParticleSystemVertexStream.Position);
				}
				if ((streams & ParticleSystemVertexStreams.Normal) != ParticleSystemVertexStreams.None && !list.Contains(ParticleSystemVertexStream.Normal))
				{
					list.Add(ParticleSystemVertexStream.Normal);
				}
				if ((streams & ParticleSystemVertexStreams.Tangent) != ParticleSystemVertexStreams.None && !list.Contains(ParticleSystemVertexStream.Tangent))
				{
					list.Add(ParticleSystemVertexStream.Tangent);
				}
				if ((streams & ParticleSystemVertexStreams.Color) != ParticleSystemVertexStreams.None && !list.Contains(ParticleSystemVertexStream.Color))
				{
					list.Add(ParticleSystemVertexStream.Color);
				}
				if ((streams & ParticleSystemVertexStreams.UV) != ParticleSystemVertexStreams.None && !list.Contains(ParticleSystemVertexStream.UV))
				{
					list.Add(ParticleSystemVertexStream.UV);
				}
				if ((streams & ParticleSystemVertexStreams.UV2BlendAndFrame) != ParticleSystemVertexStreams.None && !list.Contains(ParticleSystemVertexStream.UV2))
				{
					list.Add(ParticleSystemVertexStream.UV2);
					list.Add(ParticleSystemVertexStream.AnimBlend);
					list.Add(ParticleSystemVertexStream.AnimFrame);
				}
				if ((streams & ParticleSystemVertexStreams.CenterAndVertexID) != ParticleSystemVertexStreams.None && !list.Contains(ParticleSystemVertexStream.Center))
				{
					list.Add(ParticleSystemVertexStream.Center);
					list.Add(ParticleSystemVertexStream.VertexID);
				}
				if ((streams & ParticleSystemVertexStreams.Size) != ParticleSystemVertexStreams.None && !list.Contains(ParticleSystemVertexStream.SizeXYZ))
				{
					list.Add(ParticleSystemVertexStream.SizeXYZ);
				}
				if ((streams & ParticleSystemVertexStreams.Rotation) != ParticleSystemVertexStreams.None && !list.Contains(ParticleSystemVertexStream.Rotation3D))
				{
					list.Add(ParticleSystemVertexStream.Rotation3D);
				}
				if ((streams & ParticleSystemVertexStreams.Velocity) != ParticleSystemVertexStreams.None && !list.Contains(ParticleSystemVertexStream.Velocity))
				{
					list.Add(ParticleSystemVertexStream.Velocity);
				}
				if ((streams & ParticleSystemVertexStreams.Lifetime) != ParticleSystemVertexStreams.None && !list.Contains(ParticleSystemVertexStream.AgePercent))
				{
					list.Add(ParticleSystemVertexStream.AgePercent);
					list.Add(ParticleSystemVertexStream.InvStartLifetime);
				}
				if ((streams & ParticleSystemVertexStreams.Custom1) != ParticleSystemVertexStreams.None && !list.Contains(ParticleSystemVertexStream.Custom1XYZW))
				{
					list.Add(ParticleSystemVertexStream.Custom1XYZW);
				}
				if ((streams & ParticleSystemVertexStreams.Custom2) != ParticleSystemVertexStreams.None && !list.Contains(ParticleSystemVertexStream.Custom2XYZW))
				{
					list.Add(ParticleSystemVertexStream.Custom2XYZW);
				}
				if ((streams & ParticleSystemVertexStreams.Random) != ParticleSystemVertexStreams.None && !list.Contains(ParticleSystemVertexStream.StableRandomXYZ))
				{
					list.Add(ParticleSystemVertexStream.StableRandomXYZ);
					list.Add(ParticleSystemVertexStream.VaryingRandomX);
				}
			}
			else
			{
				if ((streams & ParticleSystemVertexStreams.Position) != ParticleSystemVertexStreams.None)
				{
					list.Remove(ParticleSystemVertexStream.Position);
				}
				if ((streams & ParticleSystemVertexStreams.Normal) != ParticleSystemVertexStreams.None)
				{
					list.Remove(ParticleSystemVertexStream.Normal);
				}
				if ((streams & ParticleSystemVertexStreams.Tangent) != ParticleSystemVertexStreams.None)
				{
					list.Remove(ParticleSystemVertexStream.Tangent);
				}
				if ((streams & ParticleSystemVertexStreams.Color) != ParticleSystemVertexStreams.None)
				{
					list.Remove(ParticleSystemVertexStream.Color);
				}
				if ((streams & ParticleSystemVertexStreams.UV) != ParticleSystemVertexStreams.None)
				{
					list.Remove(ParticleSystemVertexStream.UV);
				}
				if ((streams & ParticleSystemVertexStreams.UV2BlendAndFrame) != ParticleSystemVertexStreams.None)
				{
					list.Remove(ParticleSystemVertexStream.UV2);
					list.Remove(ParticleSystemVertexStream.AnimBlend);
					list.Remove(ParticleSystemVertexStream.AnimFrame);
				}
				if ((streams & ParticleSystemVertexStreams.CenterAndVertexID) != ParticleSystemVertexStreams.None)
				{
					list.Remove(ParticleSystemVertexStream.Center);
					list.Remove(ParticleSystemVertexStream.VertexID);
				}
				if ((streams & ParticleSystemVertexStreams.Size) != ParticleSystemVertexStreams.None)
				{
					list.Remove(ParticleSystemVertexStream.SizeXYZ);
				}
				if ((streams & ParticleSystemVertexStreams.Rotation) != ParticleSystemVertexStreams.None)
				{
					list.Remove(ParticleSystemVertexStream.Rotation3D);
				}
				if ((streams & ParticleSystemVertexStreams.Velocity) != ParticleSystemVertexStreams.None)
				{
					list.Remove(ParticleSystemVertexStream.Velocity);
				}
				if ((streams & ParticleSystemVertexStreams.Lifetime) != ParticleSystemVertexStreams.None)
				{
					list.Remove(ParticleSystemVertexStream.AgePercent);
					list.Remove(ParticleSystemVertexStream.InvStartLifetime);
				}
				if ((streams & ParticleSystemVertexStreams.Custom1) != ParticleSystemVertexStreams.None)
				{
					list.Remove(ParticleSystemVertexStream.Custom1XYZW);
				}
				if ((streams & ParticleSystemVertexStreams.Custom2) != ParticleSystemVertexStreams.None)
				{
					list.Remove(ParticleSystemVertexStream.Custom2XYZW);
				}
				if ((streams & ParticleSystemVertexStreams.Random) != ParticleSystemVertexStreams.None)
				{
					list.Remove(ParticleSystemVertexStream.StableRandomXYZW);
					list.Remove(ParticleSystemVertexStream.VaryingRandomX);
				}
			}
			SetActiveVertexStreams(list);
		}

		[Obsolete("Internal_GetVertexStreams is deprecated. Use GetActiveVertexStreams instead.", false)]
		internal ParticleSystemVertexStreams Internal_GetEnabledVertexStreams(ParticleSystemVertexStreams streams)
		{
			List<ParticleSystemVertexStream> list = new List<ParticleSystemVertexStream>(activeVertexStreamsCount);
			GetActiveVertexStreams(list);
			ParticleSystemVertexStreams particleSystemVertexStreams = ParticleSystemVertexStreams.None;
			if (list.Contains(ParticleSystemVertexStream.Position))
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Position;
			}
			if (list.Contains(ParticleSystemVertexStream.Normal))
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Normal;
			}
			if (list.Contains(ParticleSystemVertexStream.Tangent))
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Tangent;
			}
			if (list.Contains(ParticleSystemVertexStream.Color))
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Color;
			}
			if (list.Contains(ParticleSystemVertexStream.UV))
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.UV;
			}
			if (list.Contains(ParticleSystemVertexStream.UV2))
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.UV2BlendAndFrame;
			}
			if (list.Contains(ParticleSystemVertexStream.Center))
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.CenterAndVertexID;
			}
			if (list.Contains(ParticleSystemVertexStream.SizeXYZ))
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Size;
			}
			if (list.Contains(ParticleSystemVertexStream.Rotation3D))
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Rotation;
			}
			if (list.Contains(ParticleSystemVertexStream.Velocity))
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Velocity;
			}
			if (list.Contains(ParticleSystemVertexStream.AgePercent))
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Lifetime;
			}
			if (list.Contains(ParticleSystemVertexStream.Custom1XYZW))
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Custom1;
			}
			if (list.Contains(ParticleSystemVertexStream.Custom2XYZW))
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Custom2;
			}
			if (list.Contains(ParticleSystemVertexStream.StableRandomXYZ))
			{
				particleSystemVertexStreams |= ParticleSystemVertexStreams.Random;
			}
			return particleSystemVertexStreams & streams;
		}

		[Obsolete("BakeMesh with useTransform is deprecated. Use BakeMesh with ParticleSystemBakeMeshOptions instead.", false)]
		public void BakeMesh(Mesh mesh, bool useTransform = false)
		{
			BakeMesh(mesh, Camera.main, useTransform);
		}

		[Obsolete("BakeMesh with useTransform is deprecated. Use BakeMesh with ParticleSystemBakeMeshOptions instead.", false)]
		public void BakeMesh(Mesh mesh, Camera camera, bool useTransform = false)
		{
			BakeMesh(mesh, camera, useTransform ? ParticleSystemBakeMeshOptions.BakeRotationAndScale : ParticleSystemBakeMeshOptions.Default);
		}

		[Obsolete("BakeTrailsMesh with useTransform is deprecated. Use BakeTrailsMesh with ParticleSystemBakeMeshOptions instead.", false)]
		public void BakeTrailsMesh(Mesh mesh, bool useTransform = false)
		{
			BakeTrailsMesh(mesh, Camera.main, useTransform);
		}

		[Obsolete("BakeTrailsMesh with useTransform is deprecated. Use BakeTrailsMesh with ParticleSystemBakeMeshOptions instead.", false)]
		public void BakeTrailsMesh(Mesh mesh, Camera camera, bool useTransform = false)
		{
			BakeTrailsMesh(mesh, camera, useTransform ? ParticleSystemBakeMeshOptions.BakeRotationAndScale : ParticleSystemBakeMeshOptions.Default);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[RequiredByNativeCode]
		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::GetMeshes", HasExplicitThis = true)]
		public extern int GetMeshes([Out][NotNull("ArgumentNullException")] Mesh[] meshes);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::SetMeshes", HasExplicitThis = true)]
		public extern void SetMeshes([NotNull("ArgumentNullException")] Mesh[] meshes, int size);

		public void SetMeshes(Mesh[] meshes)
		{
			SetMeshes(meshes, meshes.Length);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::GetMeshWeightings", HasExplicitThis = true)]
		public extern int GetMeshWeightings([Out][NotNull("ArgumentNullException")] float[] weightings);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::SetMeshWeightings", HasExplicitThis = true)]
		public extern void SetMeshWeightings([NotNull("ArgumentNullException")] float[] weightings, int size);

		public void SetMeshWeightings(float[] weightings)
		{
			SetMeshWeightings(weightings, weightings.Length);
		}

		public void BakeMesh(Mesh mesh, ParticleSystemBakeMeshOptions options)
		{
			BakeMesh(mesh, Camera.main, options);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void BakeMesh([NotNull("ArgumentNullException")] Mesh mesh, [NotNull("ArgumentNullException")] Camera camera, ParticleSystemBakeMeshOptions options);

		public void BakeTrailsMesh(Mesh mesh, ParticleSystemBakeMeshOptions options)
		{
			BakeTrailsMesh(mesh, Camera.main, options);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void BakeTrailsMesh([NotNull("ArgumentNullException")] Mesh mesh, [NotNull("ArgumentNullException")] Camera camera, ParticleSystemBakeMeshOptions options);

		public int BakeTexture(ref Texture2D verticesTexture, ParticleSystemBakeTextureOptions options)
		{
			return BakeTexture(ref verticesTexture, Camera.main, options);
		}

		public int BakeTexture(ref Texture2D verticesTexture, Camera camera, ParticleSystemBakeTextureOptions options)
		{
			if (renderMode == ParticleSystemRenderMode.Mesh)
			{
				throw new InvalidOperationException("Baking mesh particles to texture requires supplying an indices texture");
			}
			verticesTexture = BakeTextureNoIndicesInternal(verticesTexture, camera, options, out var indexCount);
			return indexCount;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::BakeTextureNoIndices", HasExplicitThis = true)]
		private extern Texture2D BakeTextureNoIndicesInternal(Texture2D verticesTexture, [NotNull("ArgumentNullException")] Camera camera, ParticleSystemBakeTextureOptions options, out int indexCount);

		public int BakeTexture(ref Texture2D verticesTexture, ref Texture2D indicesTexture, ParticleSystemBakeTextureOptions options)
		{
			return BakeTexture(ref verticesTexture, ref indicesTexture, Camera.main, options);
		}

		public int BakeTexture(ref Texture2D verticesTexture, ref Texture2D indicesTexture, Camera camera, ParticleSystemBakeTextureOptions options)
		{
			int indexCount;
			BakeTextureOutput bakeTextureOutput = BakeTextureInternal(verticesTexture, indicesTexture, camera, options, out indexCount);
			verticesTexture = bakeTextureOutput.vertices;
			indicesTexture = bakeTextureOutput.indices;
			return indexCount;
		}

		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::BakeTexture", HasExplicitThis = true)]
		private BakeTextureOutput BakeTextureInternal(Texture2D verticesTexture, Texture2D indicesTexture, [NotNull("ArgumentNullException")] Camera camera, ParticleSystemBakeTextureOptions options, out int indexCount)
		{
			BakeTextureInternal_Injected(verticesTexture, indicesTexture, camera, options, out indexCount, out var ret);
			return ret;
		}

		public int BakeTrailsTexture(ref Texture2D verticesTexture, ref Texture2D indicesTexture, ParticleSystemBakeTextureOptions options)
		{
			return BakeTrailsTexture(ref verticesTexture, ref indicesTexture, Camera.main, options);
		}

		public int BakeTrailsTexture(ref Texture2D verticesTexture, ref Texture2D indicesTexture, Camera camera, ParticleSystemBakeTextureOptions options)
		{
			int indexCount;
			BakeTextureOutput bakeTextureOutput = BakeTrailsTextureInternal(verticesTexture, indicesTexture, camera, options, out indexCount);
			verticesTexture = bakeTextureOutput.vertices;
			indicesTexture = bakeTextureOutput.indices;
			return indexCount;
		}

		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::BakeTrailsTexture", HasExplicitThis = true)]
		private BakeTextureOutput BakeTrailsTextureInternal(Texture2D verticesTexture, Texture2D indicesTexture, [NotNull("ArgumentNullException")] Camera camera, ParticleSystemBakeTextureOptions options, out int indexCount)
		{
			BakeTrailsTextureInternal_Injected(verticesTexture, indicesTexture, camera, options, out indexCount, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::SetActiveVertexStreams", HasExplicitThis = true)]
		public extern void SetActiveVertexStreams([NotNull("ArgumentNullException")] List<ParticleSystemVertexStream> streams);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::GetActiveVertexStreams", HasExplicitThis = true)]
		public extern void GetActiveVertexStreams([NotNull("ArgumentNullException")] List<ParticleSystemVertexStream> streams);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::SetActiveTrailVertexStreams", HasExplicitThis = true)]
		public extern void SetActiveTrailVertexStreams([NotNull("ArgumentNullException")] List<ParticleSystemVertexStream> streams);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemRendererScriptBindings::GetActiveTrailVertexStreams", HasExplicitThis = true)]
		public extern void GetActiveTrailVertexStreams([NotNull("ArgumentNullException")] List<ParticleSystemVertexStream> streams);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_pivot_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_pivot_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_flip_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_flip_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void BakeTextureInternal_Injected(Texture2D verticesTexture, Texture2D indicesTexture, Camera camera, ParticleSystemBakeTextureOptions options, out int indexCount, out BakeTextureOutput ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void BakeTrailsTextureInternal_Injected(Texture2D verticesTexture, Texture2D indicesTexture, Camera camera, ParticleSystemBakeTextureOptions options, out int indexCount, out BakeTextureOutput ret);
	}
	[RequiredByNativeCode(Optional = true)]
	public struct ParticleCollisionEvent
	{
		internal Vector3 m_Intersection;

		internal Vector3 m_Normal;

		internal Vector3 m_Velocity;

		internal int m_ColliderInstanceID;

		public Vector3 intersection => m_Intersection;

		public Vector3 normal => m_Normal;

		public Vector3 velocity => m_Velocity;

		public Component colliderComponent => InstanceIDToColliderComponent(m_ColliderInstanceID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemScriptBindings::InstanceIDToColliderComponent")]
		private static extern Component InstanceIDToColliderComponent(int instanceID);
	}
	internal class ParticleSystemExtensionsImpl
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetSafeCollisionEventSize")]
		internal static extern int GetSafeCollisionEventSize([NotNull("ArgumentNullException")] ParticleSystem ps);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetCollisionEventsDeprecated")]
		internal static extern int GetCollisionEventsDeprecated([NotNull("ArgumentNullException")] ParticleSystem ps, GameObject go, [Out] ParticleCollisionEvent[] collisionEvents);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetSafeTriggerParticlesSize")]
		internal static extern int GetSafeTriggerParticlesSize([NotNull("ArgumentNullException")] ParticleSystem ps, int type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetCollisionEvents")]
		internal static extern int GetCollisionEvents([NotNull("ArgumentNullException")] ParticleSystem ps, [NotNull("ArgumentNullException")] GameObject go, [NotNull("ArgumentNullException")] List<ParticleCollisionEvent> collisionEvents);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetTriggerParticles")]
		internal static extern int GetTriggerParticles([NotNull("ArgumentNullException")] ParticleSystem ps, int type, [NotNull("ArgumentNullException")] List<ParticleSystem.Particle> particles);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemScriptBindings::GetTriggerParticlesWithData")]
		internal static extern int GetTriggerParticlesWithData([NotNull("ArgumentNullException")] ParticleSystem ps, int type, [NotNull("ArgumentNullException")] List<ParticleSystem.Particle> particles, ref ParticleSystem.ColliderData colliderData);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ParticleSystemScriptBindings::SetTriggerParticles")]
		internal static extern void SetTriggerParticles([NotNull("ArgumentNullException")] ParticleSystem ps, int type, [NotNull("ArgumentNullException")] List<ParticleSystem.Particle> particles, int offset, int count);
	}
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/ParticleSystem/ScriptBindings/ParticleSystemScriptBindings.h")]
	[NativeHeader("Modules/ParticleSystem/ParticleSystemForceFieldManager.h")]
	[NativeHeader("Modules/ParticleSystem/ParticleSystemForceField.h")]
	[NativeHeader("ParticleSystemScriptingClasses.h")]
	[NativeHeader("Modules/ParticleSystem/ParticleSystem.h")]
	public class ParticleSystemForceField : Behaviour
	{
		[NativeName("ForceShape")]
		public extern ParticleSystemForceFieldShape shape
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float startRange
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float endRange
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float length
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float gravityFocus
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector2 rotationRandomness
		{
			get
			{
				get_rotationRandomness_Injected(out var ret);
				return ret;
			}
			set
			{
				set_rotationRandomness_Injected(ref value);
			}
		}

		public extern bool multiplyDragByParticleSize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool multiplyDragByParticleVelocity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Texture3D vectorField
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public ParticleSystem.MinMaxCurve directionX
		{
			get
			{
				get_directionX_Injected(out var ret);
				return ret;
			}
			set
			{
				set_directionX_Injected(ref value);
			}
		}

		public ParticleSystem.MinMaxCurve directionY
		{
			get
			{
				get_directionY_Injected(out var ret);
				return ret;
			}
			set
			{
				set_directionY_Injected(ref value);
			}
		}

		public ParticleSystem.MinMaxCurve directionZ
		{
			get
			{
				get_directionZ_Injected(out var ret);
				return ret;
			}
			set
			{
				set_directionZ_Injected(ref value);
			}
		}

		public ParticleSystem.MinMaxCurve gravity
		{
			get
			{
				get_gravity_Injected(out var ret);
				return ret;
			}
			set
			{
				set_gravity_Injected(ref value);
			}
		}

		public ParticleSystem.MinMaxCurve rotationSpeed
		{
			get
			{
				get_rotationSpeed_Injected(out var ret);
				return ret;
			}
			set
			{
				set_rotationSpeed_Injected(ref value);
			}
		}

		public ParticleSystem.MinMaxCurve rotationAttraction
		{
			get
			{
				get_rotationAttraction_Injected(out var ret);
				return ret;
			}
			set
			{
				set_rotationAttraction_Injected(ref value);
			}
		}

		public ParticleSystem.MinMaxCurve drag
		{
			get
			{
				get_drag_Injected(out var ret);
				return ret;
			}
			set
			{
				set_drag_Injected(ref value);
			}
		}

		public ParticleSystem.MinMaxCurve vectorFieldSpeed
		{
			get
			{
				get_vectorFieldSpeed_Injected(out var ret);
				return ret;
			}
			set
			{
				set_vectorFieldSpeed_Injected(ref value);
			}
		}

		public ParticleSystem.MinMaxCurve vectorFieldAttraction
		{
			get
			{
				get_vectorFieldAttraction_Injected(out var ret);
				return ret;
			}
			set
			{
				set_vectorFieldAttraction_Injected(ref value);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_rotationRandomness_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_rotationRandomness_Injected(ref Vector2 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_directionX_Injected(out ParticleSystem.MinMaxCurve ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_directionX_Injected(ref ParticleSystem.MinMaxCurve value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_directionY_Injected(out ParticleSystem.MinMaxCurve ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_directionY_Injected(ref ParticleSystem.MinMaxCurve value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_directionZ_Injected(out ParticleSystem.MinMaxCurve ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_directionZ_Injected(ref ParticleSystem.MinMaxCurve value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_gravity_Injected(out ParticleSystem.MinMaxCurve ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_gravity_Injected(ref ParticleSystem.MinMaxCurve value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_rotationSpeed_Injected(out ParticleSystem.MinMaxCurve ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_rotationSpeed_Injected(ref ParticleSystem.MinMaxCurve value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_rotationAttraction_Injected(out ParticleSystem.MinMaxCurve ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_rotationAttraction_Injected(ref ParticleSystem.MinMaxCurve value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_drag_Injected(out ParticleSystem.MinMaxCurve ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_drag_Injected(ref ParticleSystem.MinMaxCurve value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_vectorFieldSpeed_Injected(out ParticleSystem.MinMaxCurve ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_vectorFieldSpeed_Injected(ref ParticleSystem.MinMaxCurve value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_vectorFieldAttraction_Injected(out ParticleSystem.MinMaxCurve ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_vectorFieldAttraction_Injected(ref ParticleSystem.MinMaxCurve value);
	}
}
namespace UnityEngine.Rendering
{
	[Flags]
	public enum UVChannelFlags
	{
		UV0 = 1,
		UV1 = 2,
		UV2 = 4,
		UV3 = 8
	}
}
namespace UnityEngine.ParticleSystemJobs
{
	[JobProducerType(typeof(ParticleSystemJobStruct<>))]
	public interface IJobParticleSystem
	{
		void Execute(ParticleSystemJobData jobData);
	}
	[JobProducerType(typeof(ParticleSystemParallelForJobStruct<>))]
	public interface IJobParticleSystemParallelFor
	{
		void Execute(ParticleSystemJobData jobData, int index);
	}
	[JobProducerType(typeof(ParticleSystemParallelForBatchJobStruct<>))]
	public interface IJobParticleSystemParallelForBatch
	{
		void Execute(ParticleSystemJobData jobData, int startIndex, int count);
	}
	public static class IJobParticleSystemExtensions
	{
		public static void EarlyJobInit<T>() where T : struct, IJobParticleSystem
		{
			ParticleSystemJobStruct<T>.Initialize();
		}

		internal static IntPtr GetReflectionData<T>() where T : struct, IJobParticleSystem
		{
			ParticleSystemJobStruct<T>.Initialize();
			return ParticleSystemJobStruct<T>.jobReflectionData.Data;
		}
	}
	public static class IJobParticleSystemParallelForExtensions
	{
		public static void EarlyJobInit<T>() where T : struct, IJobParticleSystemParallelFor
		{
			ParticleSystemParallelForJobStruct<T>.Initialize();
		}

		internal static IntPtr GetReflectionData<T>() where T : struct, IJobParticleSystemParallelFor
		{
			ParticleSystemParallelForJobStruct<T>.Initialize();
			return ParticleSystemParallelForJobStruct<T>.jobReflectionData.Data;
		}
	}
	public static class IJobParticleSystemParallelForBatchExtensions
	{
		public static void EarlyJobInit<T>() where T : struct, IJobParticleSystemParallelForBatch
		{
			ParticleSystemParallelForBatchJobStruct<T>.Initialize();
		}

		internal static IntPtr GetReflectionData<T>() where T : struct, IJobParticleSystemParallelForBatch
		{
			ParticleSystemParallelForBatchJobStruct<T>.Initialize();
			return ParticleSystemParallelForBatchJobStruct<T>.jobReflectionData.Data;
		}
	}
	internal static class ParticleSystemJobUtility
	{
		internal unsafe static JobsUtility.JobScheduleParameters CreateScheduleParams<T>(ref T jobData, ParticleSystem ps, JobHandle dependsOn, IntPtr jobReflectionData) where T : struct
		{
			dependsOn = JobHandle.CombineDependencies(ps.GetManagedJobHandle(), dependsOn);
			return new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf(ref jobData), jobReflectionData, dependsOn, ScheduleMode.Batched);
		}
	}
	public static class IParticleSystemJobExtensions
	{
		public unsafe static JobHandle Schedule<T>(this T jobData, ParticleSystem ps, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParticleSystem
		{
			JobsUtility.JobScheduleParameters parameters = ParticleSystemJobUtility.CreateScheduleParams(ref jobData, ps, dependsOn, IJobParticleSystemExtensions.GetReflectionData<T>());
			JobHandle jobHandle = ParticleSystem.ScheduleManagedJob(ref parameters, ps.GetManagedJobData());
			ps.SetManagedJobHandle(jobHandle);
			return jobHandle;
		}

		public unsafe static JobHandle Schedule<T>(this T jobData, ParticleSystem ps, int minIndicesPerJobCount, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParticleSystemParallelFor
		{
			JobsUtility.JobScheduleParameters parameters = ParticleSystemJobUtility.CreateScheduleParams(ref jobData, ps, dependsOn, IJobParticleSystemParallelForExtensions.GetReflectionData<T>());
			JobHandle jobHandle = JobsUtility.ScheduleParallelForDeferArraySize(ref parameters, minIndicesPerJobCount, ps.GetManagedJobData(), null);
			ps.SetManagedJobHandle(jobHandle);
			return jobHandle;
		}

		public unsafe static JobHandle ScheduleBatch<T>(this T jobData, ParticleSystem ps, int innerLoopBatchCount, JobHandle dependsOn = default(JobHandle)) where T : struct, IJobParticleSystemParallelForBatch
		{
			JobsUtility.JobScheduleParameters parameters = ParticleSystemJobUtility.CreateScheduleParams(ref jobData, ps, dependsOn, IJobParticleSystemParallelForBatchExtensions.GetReflectionData<T>());
			JobHandle jobHandle = JobsUtility.ScheduleParallelForDeferArraySize(ref parameters, innerLoopBatchCount, ps.GetManagedJobData(), null);
			ps.SetManagedJobHandle(jobHandle);
			return jobHandle;
		}
	}
	public struct ParticleSystemNativeArray3
	{
		public NativeArray<float> x;

		public NativeArray<float> y;

		public NativeArray<float> z;

		public Vector3 this[int index]
		{
			get
			{
				return new Vector3(x[index], y[index], z[index]);
			}
			set
			{
				x[index] = value.x;
				y[index] = value.y;
				z[index] = value.z;
			}
		}
	}
	public struct ParticleSystemNativeArray4
	{
		public NativeArray<float> x;

		public NativeArray<float> y;

		public NativeArray<float> z;

		public NativeArray<float> w;

		public Vector4 this[int index]
		{
			get
			{
				return new Vector4(x[index], y[index], z[index], w[index]);
			}
			set
			{
				x[index] = value.x;
				y[index] = value.y;
				z[index] = value.z;
				w[index] = value.w;
			}
		}
	}
	public struct ParticleSystemJobData
	{
		public int count { get; }

		public ParticleSystemNativeArray3 positions { get; }

		public ParticleSystemNativeArray3 velocities { get; }

		public ParticleSystemNativeArray3 axisOfRotations { get; }

		public ParticleSystemNativeArray3 rotations { get; }

		public ParticleSystemNativeArray3 rotationalSpeeds { get; }

		public ParticleSystemNativeArray3 sizes { get; }

		public NativeArray<Color32> startColors { get; }

		public NativeArray<float> aliveTimePercent { get; }

		public NativeArray<float> inverseStartLifetimes { get; }

		public NativeArray<uint> randomSeeds { get; }

		public ParticleSystemNativeArray4 customData1 { get; }

		public ParticleSystemNativeArray4 customData2 { get; }

		public NativeArray<int> meshIndices { get; }

		internal unsafe ParticleSystemJobData(ref NativeParticleData nativeData)
		{
			this = default(ParticleSystemJobData);
			count = nativeData.count;
			positions = CreateNativeArray3(ref nativeData.positions, count);
			velocities = CreateNativeArray3(ref nativeData.velocities, count);
			axisOfRotations = CreateNativeArray3(ref nativeData.axisOfRotations, count);
			rotations = CreateNativeArray3(ref nativeData.rotations, count);
			rotationalSpeeds = CreateNativeArray3(ref nativeData.rotationalSpeeds, count);
			sizes = CreateNativeArray3(ref nativeData.sizes, count);
			startColors = CreateNativeArray<Color32>(nativeData.startColors, count);
			aliveTimePercent = CreateNativeArray<float>(nativeData.aliveTimePercent, count);
			inverseStartLifetimes = CreateNativeArray<float>(nativeData.inverseStartLifetimes, count);
			randomSeeds = CreateNativeArray<uint>(nativeData.randomSeeds, count);
			customData1 = CreateNativeArray4(ref nativeData.customData1, count);
			customData2 = CreateNativeArray4(ref nativeData.customData2, count);
			meshIndices = CreateNativeArray<int>(nativeData.meshIndices, count);
		}

		internal unsafe NativeArray<T> CreateNativeArray<T>(void* src, int count) where T : struct
		{
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(src, count, Allocator.Invalid);
		}

		internal unsafe ParticleSystemNativeArray3 CreateNativeArray3(ref NativeParticleData.Array3 ptrs, int count)
		{
			return new ParticleSystemNativeArray3
			{
				x = CreateNativeArray<float>(ptrs.x, count),
				y = CreateNativeArray<float>(ptrs.y, count),
				z = CreateNativeArray<float>(ptrs.z, count)
			};
		}

		internal unsafe ParticleSystemNativeArray4 CreateNativeArray4(ref NativeParticleData.Array4 ptrs, int count)
		{
			return new ParticleSystemNativeArray4
			{
				x = CreateNativeArray<float>(ptrs.x, count),
				y = CreateNativeArray<float>(ptrs.y, count),
				z = CreateNativeArray<float>(ptrs.z, count),
				w = CreateNativeArray<float>(ptrs.w, count)
			};
		}
	}
	internal struct NativeParticleData
	{
		internal struct Array3
		{
			internal unsafe float* x;

			internal unsafe float* y;

			internal unsafe float* z;
		}

		internal struct Array4
		{
			internal unsafe float* x;

			internal unsafe float* y;

			internal unsafe float* z;

			internal unsafe float* w;
		}

		internal int count;

		internal Array3 positions;

		internal Array3 velocities;

		internal Array3 axisOfRotations;

		internal Array3 rotations;

		internal Array3 rotationalSpeeds;

		internal Array3 sizes;

		internal unsafe void* startColors;

		internal unsafe void* aliveTimePercent;

		internal unsafe void* inverseStartLifetimes;

		internal unsafe void* randomSeeds;

		internal Array4 customData1;

		internal Array4 customData2;

		internal unsafe void* meshIndices;
	}
	internal struct NativeListData
	{
		public unsafe void* system;

		public int length;

		public int capacity;
	}
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct ParticleSystemJobStruct<T> where T : struct, IJobParticleSystem
	{
		public delegate void ExecuteJobFunction(ref T data, IntPtr listDataPtr, IntPtr unusedPtr, ref JobRanges ranges, int jobIndex);

		public static readonly BurstLike.SharedStatic<IntPtr> jobReflectionData = BurstLike.SharedStatic<IntPtr>.GetOrCreate<ParticleSystemJobStruct<T>>();

		[BurstDiscard]
		public static void Initialize()
		{
			if (jobReflectionData.Data == IntPtr.Zero)
			{
				jobReflectionData.Data = JobsUtility.CreateJobReflectionData(typeof(T), new ExecuteJobFunction(Execute));
			}
		}

		public unsafe static void Execute(ref T data, IntPtr listDataPtr, IntPtr unusedPtr, ref JobRanges ranges, int jobIndex)
		{
			NativeListData* ptr = (NativeListData*)(void*)listDataPtr;
			ParticleSystem.CopyManagedJobData(ptr->system, out var particleData);
			ParticleSystemJobData jobData = new ParticleSystemJobData(ref particleData);
			data.Execute(jobData);
		}
	}
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct ParticleSystemParallelForJobStruct<T> where T : struct, IJobParticleSystemParallelFor
	{
		public delegate void ExecuteJobFunction(ref T data, IntPtr listDataPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);

		public static readonly BurstLike.SharedStatic<IntPtr> jobReflectionData = BurstLike.SharedStatic<IntPtr>.GetOrCreate<ParticleSystemParallelForJobStruct<T>>();

		[BurstDiscard]
		public static void Initialize()
		{
			if (jobReflectionData.Data == IntPtr.Zero)
			{
				jobReflectionData.Data = JobsUtility.CreateJobReflectionData(typeof(T), new ExecuteJobFunction(Execute));
			}
		}

		public unsafe static void Execute(ref T data, IntPtr listDataPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
		{
			NativeListData* ptr = (NativeListData*)(void*)listDataPtr;
			ParticleSystem.CopyManagedJobData(ptr->system, out var particleData);
			ParticleSystemJobData jobData = new ParticleSystemJobData(ref particleData);
			int beginIndex;
			int endIndex;
			while (JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out beginIndex, out endIndex))
			{
				for (int i = beginIndex; i < endIndex; i++)
				{
					data.Execute(jobData, i);
				}
			}
		}
	}
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct ParticleSystemParallelForBatchJobStruct<T> where T : struct, IJobParticleSystemParallelForBatch
	{
		public delegate void ExecuteJobFunction(ref T data, IntPtr listDataPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex);

		public static readonly BurstLike.SharedStatic<IntPtr> jobReflectionData = BurstLike.SharedStatic<IntPtr>.GetOrCreate<ParticleSystemParallelForBatchJobStruct<T>>();

		[BurstDiscard]
		public static void Initialize()
		{
			if (jobReflectionData.Data == IntPtr.Zero)
			{
				jobReflectionData.Data = JobsUtility.CreateJobReflectionData(typeof(T), new ExecuteJobFunction(Execute));
			}
		}

		public unsafe static void Execute(ref T data, IntPtr listDataPtr, IntPtr bufferRangePatchData, ref JobRanges ranges, int jobIndex)
		{
			NativeListData* ptr = (NativeListData*)(void*)listDataPtr;
			ParticleSystem.CopyManagedJobData(ptr->system, out var particleData);
			ParticleSystemJobData jobData = new ParticleSystemJobData(ref particleData);
			int beginIndex;
			int endIndex;
			while (JobsUtility.GetWorkStealingRange(ref ranges, jobIndex, out beginIndex, out endIndex))
			{
				data.Execute(jobData, beginIndex, endIndex - beginIndex);
			}
		}
	}
}
