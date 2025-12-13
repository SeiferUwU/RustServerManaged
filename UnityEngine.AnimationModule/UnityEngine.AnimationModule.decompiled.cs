using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Playables;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

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
	public interface IAnimationClipSource
	{
		void GetAnimationClips(List<AnimationClip> results);
	}
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	[RequiredByNativeCode]
	public sealed class SharedBetweenAnimatorsAttribute : Attribute
	{
	}
	[RequiredByNativeCode]
	public abstract class StateMachineBehaviour : ScriptableObject
	{
		public virtual void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
		}

		public virtual void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
		}

		public virtual void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
		}

		public virtual void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
		}

		public virtual void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
		}

		public virtual void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
		{
		}

		public virtual void OnStateMachineExit(Animator animator, int stateMachinePathHash)
		{
		}

		public virtual void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
		{
		}

		public virtual void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
		{
		}

		public virtual void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
		{
		}

		public virtual void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
		{
		}

		public virtual void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
		{
		}

		public virtual void OnStateMachineEnter(Animator animator, int stateMachinePathHash, AnimatorControllerPlayable controller)
		{
		}

		public virtual void OnStateMachineExit(Animator animator, int stateMachinePathHash, AnimatorControllerPlayable controller)
		{
		}
	}
	public enum PlayMode
	{
		StopSameLayer = 0,
		StopAll = 4
	}
	public enum QueueMode
	{
		CompleteOthers = 0,
		PlayNow = 2
	}
	public enum AnimationBlendMode
	{
		Blend,
		Additive
	}
	public enum AnimationPlayMode
	{
		Stop,
		Queue,
		Mix
	}
	public enum AnimationCullingType
	{
		AlwaysAnimate,
		BasedOnRenderers,
		[Obsolete("Enum member AnimatorCullingMode.BasedOnClipBounds has been deprecated. Use AnimationCullingType.AlwaysAnimate or AnimationCullingType.BasedOnRenderers instead")]
		BasedOnClipBounds,
		[Obsolete("Enum member AnimatorCullingMode.BasedOnUserBounds has been deprecated. Use AnimationCullingType.AlwaysAnimate or AnimationCullingType.BasedOnRenderers instead")]
		BasedOnUserBounds
	}
	internal enum AnimationEventSource
	{
		NoSource,
		Legacy,
		Animator
	}
	[NativeHeader("Modules/Animation/Animation.h")]
	public sealed class Animation : Behaviour, IEnumerable
	{
		private sealed class Enumerator : IEnumerator
		{
			private Animation m_Outer;

			private int m_CurrentIndex = -1;

			public object Current => m_Outer.GetStateAtIndex(m_CurrentIndex);

			internal Enumerator(Animation outer)
			{
				m_Outer = outer;
			}

			public bool MoveNext()
			{
				int stateCount = m_Outer.GetStateCount();
				m_CurrentIndex++;
				return m_CurrentIndex < stateCount;
			}

			public void Reset()
			{
				m_CurrentIndex = -1;
			}
		}

		public extern AnimationClip clip
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool playAutomatically
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern WrapMode wrapMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool isPlaying
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("IsPlaying")]
			get;
		}

		public AnimationState this[string name] => GetState(name);

		public extern bool animatePhysics
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("Use cullingType instead")]
		public extern bool animateOnlyIfVisible
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("AnimationBindings::GetAnimateOnlyIfVisible", HasExplicitThis = true)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction("AnimationBindings::SetAnimateOnlyIfVisible", HasExplicitThis = true)]
			set;
		}

		public extern AnimationCullingType cullingType
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Bounds localBounds
		{
			[NativeName("GetLocalAABB")]
			get
			{
				get_localBounds_Injected(out var ret);
				return ret;
			}
			[NativeName("SetLocalAABB")]
			set
			{
				set_localBounds_Injected(ref value);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Stop();

		public void Stop(string name)
		{
			StopNamed(name);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("Stop")]
		private extern void StopNamed(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Rewind();

		public void Rewind(string name)
		{
			RewindNamed(name);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("Rewind")]
		private extern void RewindNamed(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Sample();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsPlaying(string name);

		[ExcludeFromDocs]
		public bool Play()
		{
			return Play(PlayMode.StopSameLayer);
		}

		public bool Play([UnityEngine.Internal.DefaultValue("PlayMode.StopSameLayer")] PlayMode mode)
		{
			return PlayDefaultAnimation(mode);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("Play")]
		private extern bool PlayDefaultAnimation(PlayMode mode);

		[ExcludeFromDocs]
		public bool Play(string animation)
		{
			return Play(animation, PlayMode.StopSameLayer);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool Play(string animation, [UnityEngine.Internal.DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		[ExcludeFromDocs]
		public void CrossFade(string animation)
		{
			CrossFade(animation, 0.3f);
		}

		[ExcludeFromDocs]
		public void CrossFade(string animation, float fadeLength)
		{
			CrossFade(animation, fadeLength, PlayMode.StopSameLayer);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CrossFade(string animation, [UnityEngine.Internal.DefaultValue("0.3F")] float fadeLength, [UnityEngine.Internal.DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		[ExcludeFromDocs]
		public void Blend(string animation)
		{
			Blend(animation, 1f);
		}

		[ExcludeFromDocs]
		public void Blend(string animation, float targetWeight)
		{
			Blend(animation, targetWeight, 0.3f);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Blend(string animation, [UnityEngine.Internal.DefaultValue("1.0F")] float targetWeight, [UnityEngine.Internal.DefaultValue("0.3F")] float fadeLength);

		[ExcludeFromDocs]
		public AnimationState CrossFadeQueued(string animation)
		{
			return CrossFadeQueued(animation, 0.3f);
		}

		[ExcludeFromDocs]
		public AnimationState CrossFadeQueued(string animation, float fadeLength)
		{
			return CrossFadeQueued(animation, fadeLength, QueueMode.CompleteOthers);
		}

		[ExcludeFromDocs]
		public AnimationState CrossFadeQueued(string animation, float fadeLength, QueueMode queue)
		{
			return CrossFadeQueued(animation, fadeLength, queue, PlayMode.StopSameLayer);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("AnimationBindings::CrossFadeQueuedImpl", HasExplicitThis = true)]
		public extern AnimationState CrossFadeQueued(string animation, [UnityEngine.Internal.DefaultValue("0.3F")] float fadeLength, [UnityEngine.Internal.DefaultValue("QueueMode.CompleteOthers")] QueueMode queue, [UnityEngine.Internal.DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		[ExcludeFromDocs]
		public AnimationState PlayQueued(string animation)
		{
			return PlayQueued(animation, QueueMode.CompleteOthers);
		}

		[ExcludeFromDocs]
		public AnimationState PlayQueued(string animation, QueueMode queue)
		{
			return PlayQueued(animation, queue, PlayMode.StopSameLayer);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("AnimationBindings::PlayQueuedImpl", HasExplicitThis = true)]
		public extern AnimationState PlayQueued(string animation, [UnityEngine.Internal.DefaultValue("QueueMode.CompleteOthers")] QueueMode queue, [UnityEngine.Internal.DefaultValue("PlayMode.StopSameLayer")] PlayMode mode);

		public void AddClip(AnimationClip clip, string newName)
		{
			AddClip(clip, newName, int.MinValue, int.MaxValue);
		}

		[ExcludeFromDocs]
		public void AddClip(AnimationClip clip, string newName, int firstFrame, int lastFrame)
		{
			AddClip(clip, newName, firstFrame, lastFrame, addLoopFrame: false);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddClip([NotNull("NullExceptionObject")] AnimationClip clip, string newName, int firstFrame, int lastFrame, [UnityEngine.Internal.DefaultValue("false")] bool addLoopFrame);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveClip([NotNull("NullExceptionObject")] AnimationClip clip);

		public void RemoveClip(string clipName)
		{
			RemoveClipNamed(clipName);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("RemoveClip")]
		private extern void RemoveClipNamed(string clipName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetClipCount();

		[Obsolete("use PlayMode instead of AnimationPlayMode.")]
		public bool Play(AnimationPlayMode mode)
		{
			return PlayDefaultAnimation((PlayMode)mode);
		}

		[Obsolete("use PlayMode instead of AnimationPlayMode.")]
		public bool Play(string animation, AnimationPlayMode mode)
		{
			return Play(animation, (PlayMode)mode);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SyncLayer(int layer);

		public IEnumerator GetEnumerator()
		{
			return new Enumerator(this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("AnimationBindings::GetState", HasExplicitThis = true)]
		internal extern AnimationState GetState(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("AnimationBindings::GetStateAtIndex", HasExplicitThis = true, ThrowsException = true)]
		internal extern AnimationState GetStateAtIndex(int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetAnimationStateCount")]
		internal extern int GetStateCount();

		public AnimationClip GetClip(string name)
		{
			AnimationState state = GetState(name);
			if ((bool)state)
			{
				return state.clip;
			}
			return null;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_localBounds_Injected(out Bounds ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_localBounds_Injected(ref Bounds value);
	}
	[NativeHeader("Modules/Animation/AnimationState.h")]
	[UsedByNativeCode]
	public sealed class AnimationState : TrackedReference
	{
		public extern bool enabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float weight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern WrapMode wrapMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float time
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float normalizedTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float speed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float normalizedSpeed
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
		}

		public extern int layer
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern AnimationClip clip
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern string name
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern AnimationBlendMode blendMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[ExcludeFromDocs]
		public void AddMixingTransform(Transform mix)
		{
			AddMixingTransform(mix, recursive: true);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddMixingTransform([NotNull("NullExceptionObject")] Transform mix, [UnityEngine.Internal.DefaultValue("true")] bool recursive);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveMixingTransform([NotNull("NullExceptionObject")] Transform mix);
	}
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	[RequiredByNativeCode]
	public sealed class AnimationEvent
	{
		internal float m_Time;

		internal string m_FunctionName;

		internal string m_StringParameter;

		internal Object m_ObjectReferenceParameter;

		internal float m_FloatParameter;

		internal int m_IntParameter;

		internal int m_MessageOptions;

		internal AnimationEventSource m_Source;

		internal AnimationState m_StateSender;

		internal AnimatorStateInfo m_AnimatorStateInfo;

		internal AnimatorClipInfo m_AnimatorClipInfo;

		[Obsolete("Use stringParameter instead")]
		public string data
		{
			get
			{
				return m_StringParameter;
			}
			set
			{
				m_StringParameter = value;
			}
		}

		public string stringParameter
		{
			get
			{
				return m_StringParameter;
			}
			set
			{
				m_StringParameter = value;
			}
		}

		public float floatParameter
		{
			get
			{
				return m_FloatParameter;
			}
			set
			{
				m_FloatParameter = value;
			}
		}

		public int intParameter
		{
			get
			{
				return m_IntParameter;
			}
			set
			{
				m_IntParameter = value;
			}
		}

		public Object objectReferenceParameter
		{
			get
			{
				return m_ObjectReferenceParameter;
			}
			set
			{
				m_ObjectReferenceParameter = value;
			}
		}

		public string functionName
		{
			get
			{
				return m_FunctionName;
			}
			set
			{
				m_FunctionName = value;
			}
		}

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

		public SendMessageOptions messageOptions
		{
			get
			{
				return (SendMessageOptions)m_MessageOptions;
			}
			set
			{
				m_MessageOptions = (int)value;
			}
		}

		public bool isFiredByLegacy => m_Source == AnimationEventSource.Legacy;

		public bool isFiredByAnimator => m_Source == AnimationEventSource.Animator;

		public AnimationState animationState
		{
			get
			{
				if (!isFiredByLegacy)
				{
					Debug.LogError("AnimationEvent was not fired by Animation component, you shouldn't use AnimationEvent.animationState");
				}
				return m_StateSender;
			}
		}

		public AnimatorStateInfo animatorStateInfo
		{
			get
			{
				if (!isFiredByAnimator)
				{
					Debug.LogError("AnimationEvent was not fired by Animator component, you shouldn't use AnimationEvent.animatorStateInfo");
				}
				return m_AnimatorStateInfo;
			}
		}

		public AnimatorClipInfo animatorClipInfo
		{
			get
			{
				if (!isFiredByAnimator)
				{
					Debug.LogError("AnimationEvent was not fired by Animator component, you shouldn't use AnimationEvent.animatorClipInfo");
				}
				return m_AnimatorClipInfo;
			}
		}

		public AnimationEvent()
		{
			m_Time = 0f;
			m_FunctionName = "";
			m_StringParameter = "";
			m_ObjectReferenceParameter = null;
			m_FloatParameter = 0f;
			m_IntParameter = 0;
			m_MessageOptions = 0;
			m_Source = AnimationEventSource.NoSource;
			m_StateSender = null;
		}

		internal int GetHash()
		{
			int num = 0;
			num = functionName.GetHashCode();
			return 33 * num + time.GetHashCode();
		}
	}
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationClip.bindings.h")]
	[NativeType("Modules/Animation/AnimationClip.h")]
	public sealed class AnimationClip : Motion
	{
		[NativeProperty("Length", false, TargetType.Function)]
		public extern float length
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("StartTime", false, TargetType.Function)]
		internal extern float startTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("StopTime", false, TargetType.Function)]
		internal extern float stopTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("SampleRate", false, TargetType.Function)]
		public extern float frameRate
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("WrapMode", false, TargetType.Function)]
		public extern WrapMode wrapMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("Bounds", false, TargetType.Function)]
		public Bounds localBounds
		{
			get
			{
				get_localBounds_Injected(out var ret);
				return ret;
			}
			set
			{
				set_localBounds_Injected(ref value);
			}
		}

		public new extern bool legacy
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("IsLegacy")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("SetLegacy")]
			set;
		}

		public extern bool humanMotion
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("IsHumanMotion")]
			get;
		}

		public extern bool empty
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("IsEmpty")]
			get;
		}

		public extern bool hasGenericRootTransform
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("HasGenericRootTransform")]
			get;
		}

		public extern bool hasMotionFloatCurves
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("HasMotionFloatCurves")]
			get;
		}

		public extern bool hasMotionCurves
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("HasMotionCurves")]
			get;
		}

		public extern bool hasRootCurves
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("HasRootCurves")]
			get;
		}

		internal extern bool hasRootMotion
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction(Name = "AnimationClipBindings::Internal_GetHasRootMotion", HasExplicitThis = true)]
			get;
		}

		public AnimationEvent[] events
		{
			get
			{
				return (AnimationEvent[])GetEventsInternal();
			}
			set
			{
				SetEventsInternal(value);
			}
		}

		public AnimationClip()
		{
			Internal_CreateAnimationClip(this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("AnimationClipBindings::Internal_CreateAnimationClip")]
		private static extern void Internal_CreateAnimationClip([Writable] AnimationClip self);

		public void SampleAnimation(GameObject go, float time)
		{
			SampleAnimation(go, this, time, wrapMode);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeHeader("Modules/Animation/AnimationUtility.h")]
		[FreeFunction]
		internal static extern void SampleAnimation([NotNull("ArgumentNullException")] GameObject go, [NotNull("ArgumentNullException")] AnimationClip clip, float inTime, WrapMode wrapMode);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("AnimationClipBindings::Internal_SetCurve", HasExplicitThis = true)]
		public extern void SetCurve([NotNull("ArgumentNullException")] string relativePath, [NotNull("ArgumentNullException")] Type type, [NotNull("ArgumentNullException")] string propertyName, AnimationCurve curve);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void EnsureQuaternionContinuity();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ClearCurves();

		public void AddEvent(AnimationEvent evt)
		{
			if (evt == null)
			{
				throw new ArgumentNullException("evt");
			}
			AddEventInternal(evt);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimationClipBindings::AddEventInternal", HasExplicitThis = true)]
		private extern void AddEventInternal(object evt);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimationClipBindings::SetEventsInternal", HasExplicitThis = true, ThrowsException = true)]
		private extern void SetEventsInternal(Array value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimationClipBindings::GetEventsInternal", HasExplicitThis = true)]
		private extern Array GetEventsInternal();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_localBounds_Injected(out Bounds ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_localBounds_Injected(ref Bounds value);
	}
	public enum AvatarTarget
	{
		Root,
		Body,
		LeftFoot,
		RightFoot,
		LeftHand,
		RightHand
	}
	public enum AvatarIKGoal
	{
		LeftFoot,
		RightFoot,
		LeftHand,
		RightHand
	}
	public enum AvatarIKHint
	{
		LeftKnee,
		RightKnee,
		LeftElbow,
		RightElbow
	}
	public enum AnimatorControllerParameterType
	{
		Float = 1,
		Int = 3,
		Bool = 4,
		Trigger = 9
	}
	internal static class AnimatorControllerParameterTypeConstants
	{
		public const int InvalidType = 0;
	}
	internal enum TransitionType
	{
		Normal = 1,
		Entry = 2,
		Exit = 4
	}
	internal enum StateInfoIndex
	{
		CurrentState,
		NextState,
		ExitState,
		InterruptedState
	}
	public enum AnimatorRecorderMode
	{
		Offline,
		Playback,
		Record
	}
	public enum DurationUnit
	{
		Fixed,
		Normalized
	}
	public enum AnimatorCullingMode
	{
		AlwaysAnimate,
		CullUpdateTransforms,
		CullCompletely
	}
	public enum AnimatorUpdateMode
	{
		Normal,
		AnimatePhysics,
		UnscaledTime
	}
	[UsedByNativeCode]
	[NativeHeader("Modules/Animation/ScriptBindings/Animation.bindings.h")]
	[NativeHeader("Modules/Animation/AnimatorInfo.h")]
	public struct AnimatorClipInfo
	{
		private int m_ClipInstanceID;

		private float m_Weight;

		public AnimationClip clip => (m_ClipInstanceID != 0) ? InstanceIDToAnimationClipPPtr(m_ClipInstanceID) : null;

		public float weight => m_Weight;

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("AnimationBindings::InstanceIDToAnimationClipPPtr")]
		private static extern AnimationClip InstanceIDToAnimationClipPPtr(int instanceID);
	}
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/AnimatorInfo.h")]
	public struct AnimatorStateInfo
	{
		private int m_Name;

		private int m_Path;

		private int m_FullPath;

		private float m_NormalizedTime;

		private float m_Length;

		private float m_Speed;

		private float m_SpeedMultiplier;

		private int m_Tag;

		private int m_Loop;

		public int fullPathHash => m_FullPath;

		[Obsolete("AnimatorStateInfo.nameHash has been deprecated. Use AnimatorStateInfo.fullPathHash instead.")]
		public int nameHash => m_Path;

		public int shortNameHash => m_Name;

		public float normalizedTime => m_NormalizedTime;

		public float length => m_Length;

		public float speed => m_Speed;

		public float speedMultiplier => m_SpeedMultiplier;

		public int tagHash => m_Tag;

		public bool loop => m_Loop != 0;

		public bool IsName(string name)
		{
			int num = Animator.StringToHash(name);
			return num == m_FullPath || num == m_Name || num == m_Path;
		}

		public bool IsTag(string tag)
		{
			return Animator.StringToHash(tag) == m_Tag;
		}
	}
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/AnimatorInfo.h")]
	public struct AnimatorTransitionInfo
	{
		[NativeName("fullPathHash")]
		private int m_FullPath;

		[NativeName("userNameHash")]
		private int m_UserName;

		[NativeName("nameHash")]
		private int m_Name;

		[NativeName("hasFixedDuration")]
		private bool m_HasFixedDuration;

		[NativeName("duration")]
		private float m_Duration;

		[NativeName("normalizedTime")]
		private float m_NormalizedTime;

		[NativeName("anyState")]
		private bool m_AnyState;

		[NativeName("transitionType")]
		private int m_TransitionType;

		public int fullPathHash => m_FullPath;

		public int nameHash => m_Name;

		public int userNameHash => m_UserName;

		public DurationUnit durationUnit => (!m_HasFixedDuration) ? DurationUnit.Normalized : DurationUnit.Fixed;

		public float duration => m_Duration;

		public float normalizedTime => m_NormalizedTime;

		public bool anyState => m_AnyState;

		internal bool entry => (m_TransitionType & 2) != 0;

		internal bool exit => (m_TransitionType & 4) != 0;

		public bool IsName(string name)
		{
			return Animator.StringToHash(name) == m_Name || Animator.StringToHash(name) == m_FullPath;
		}

		public bool IsUserName(string name)
		{
			return Animator.StringToHash(name) == m_UserName;
		}
	}
	[NativeHeader("Modules/Animation/Animator.h")]
	public struct MatchTargetWeightMask
	{
		private Vector3 m_PositionXYZWeight;

		private float m_RotationWeight;

		public Vector3 positionXYZWeight
		{
			get
			{
				return m_PositionXYZWeight;
			}
			set
			{
				m_PositionXYZWeight = value;
			}
		}

		public float rotationWeight
		{
			get
			{
				return m_RotationWeight;
			}
			set
			{
				m_RotationWeight = value;
			}
		}

		public MatchTargetWeightMask(Vector3 positionXYZWeight, float rotationWeight)
		{
			m_PositionXYZWeight = positionXYZWeight;
			m_RotationWeight = rotationWeight;
		}
	}
	[NativeHeader("Modules/Animation/Animator.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/Animator.bindings.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimatorControllerParameter.bindings.h")]
	[UsedByNativeCode]
	public class Animator : Behaviour
	{
		public extern bool isOptimizable
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("IsOptimizable")]
			get;
		}

		public extern bool isHuman
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("IsHuman")]
			get;
		}

		public extern bool hasRootMotion
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("HasRootMotion")]
			get;
		}

		internal extern bool isRootPositionOrRotationControlledByCurves
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("IsRootTranslationOrRotationControllerByCurves")]
			get;
		}

		public extern float humanScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool isInitialized
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("IsInitialized")]
			get;
		}

		public Vector3 deltaPosition
		{
			get
			{
				get_deltaPosition_Injected(out var ret);
				return ret;
			}
		}

		public Quaternion deltaRotation
		{
			get
			{
				get_deltaRotation_Injected(out var ret);
				return ret;
			}
		}

		public Vector3 velocity
		{
			get
			{
				get_velocity_Injected(out var ret);
				return ret;
			}
		}

		public Vector3 angularVelocity
		{
			get
			{
				get_angularVelocity_Injected(out var ret);
				return ret;
			}
		}

		public Vector3 rootPosition
		{
			[NativeMethod("GetAvatarPosition")]
			get
			{
				get_rootPosition_Injected(out var ret);
				return ret;
			}
			[NativeMethod("SetAvatarPosition")]
			set
			{
				set_rootPosition_Injected(ref value);
			}
		}

		public Quaternion rootRotation
		{
			[NativeMethod("GetAvatarRotation")]
			get
			{
				get_rootRotation_Injected(out var ret);
				return ret;
			}
			[NativeMethod("SetAvatarRotation")]
			set
			{
				set_rootRotation_Injected(ref value);
			}
		}

		public extern bool applyRootMotion
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("Animator.linearVelocityBlending is no longer used and has been deprecated.")]
		public extern bool linearVelocityBlending
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("Animator.animatePhysics has been deprecated. Use Animator.updateMode instead.")]
		public bool animatePhysics
		{
			get
			{
				return updateMode == AnimatorUpdateMode.AnimatePhysics;
			}
			set
			{
				updateMode = (value ? AnimatorUpdateMode.AnimatePhysics : AnimatorUpdateMode.Normal);
			}
		}

		public extern AnimatorUpdateMode updateMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool hasTransformHierarchy
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal extern bool allowConstantClipSamplingOptimization
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float gravityWeight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public Vector3 bodyPosition
		{
			get
			{
				CheckIfInIKPass();
				return bodyPositionInternal;
			}
			set
			{
				CheckIfInIKPass();
				bodyPositionInternal = value;
			}
		}

		internal Vector3 bodyPositionInternal
		{
			[NativeMethod("GetBodyPosition")]
			get
			{
				get_bodyPositionInternal_Injected(out var ret);
				return ret;
			}
			[NativeMethod("SetBodyPosition")]
			set
			{
				set_bodyPositionInternal_Injected(ref value);
			}
		}

		public Quaternion bodyRotation
		{
			get
			{
				CheckIfInIKPass();
				return bodyRotationInternal;
			}
			set
			{
				CheckIfInIKPass();
				bodyRotationInternal = value;
			}
		}

		internal Quaternion bodyRotationInternal
		{
			[NativeMethod("GetBodyRotation")]
			get
			{
				get_bodyRotationInternal_Injected(out var ret);
				return ret;
			}
			[NativeMethod("SetBodyRotation")]
			set
			{
				set_bodyRotationInternal_Injected(ref value);
			}
		}

		public extern bool stabilizeFeet
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int layerCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern AnimatorControllerParameter[] parameters
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[FreeFunction(Name = "AnimatorBindings::GetParameters", HasExplicitThis = true)]
			get;
		}

		public extern int parameterCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float feetPivotActive
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float pivotWeight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public Vector3 pivotPosition
		{
			get
			{
				get_pivotPosition_Injected(out var ret);
				return ret;
			}
		}

		public extern bool isMatchingTarget
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("IsMatchingTarget")]
			get;
		}

		public extern float speed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 targetPosition
		{
			get
			{
				get_targetPosition_Injected(out var ret);
				return ret;
			}
		}

		public Quaternion targetRotation
		{
			get
			{
				get_targetRotation_Injected(out var ret);
				return ret;
			}
		}

		public extern Transform avatarRoot
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern AnimatorCullingMode cullingMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float playbackTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public float recorderStartTime
		{
			get
			{
				return GetRecorderStartTime();
			}
			set
			{
			}
		}

		public float recorderStopTime
		{
			get
			{
				return GetRecorderStopTime();
			}
			set
			{
			}
		}

		public extern AnimatorRecorderMode recorderMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern RuntimeAnimatorController runtimeAnimatorController
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool hasBoundPlayables
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("HasBoundPlayables")]
			get;
		}

		public extern Avatar avatar
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public PlayableGraph playableGraph
		{
			get
			{
				PlayableGraph graph = default(PlayableGraph);
				GetCurrentGraph(ref graph);
				return graph;
			}
		}

		public extern bool layersAffectMassCenter
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float leftFeetBottomHeight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float rightFeetBottomHeight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeConditional("UNITY_EDITOR")]
		internal extern bool supportsOnAnimatorMove
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("SupportsOnAnimatorMove")]
			get;
		}

		public extern bool logWarnings
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool fireEvents
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("keepAnimatorControllerStateOnDisable is deprecated, use keepAnimatorStateOnDisable instead. (UnityUpgradable) -> keepAnimatorStateOnDisable", false)]
		public bool keepAnimatorControllerStateOnDisable
		{
			get
			{
				return keepAnimatorStateOnDisable;
			}
			set
			{
				keepAnimatorStateOnDisable = value;
			}
		}

		public extern bool keepAnimatorStateOnDisable
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool writeDefaultValuesOnDisable
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public float GetFloat(string name)
		{
			return GetFloatString(name);
		}

		public float GetFloat(int id)
		{
			return GetFloatID(id);
		}

		public void SetFloat(string name, float value)
		{
			SetFloatString(name, value);
		}

		public void SetFloat(string name, float value, float dampTime, float deltaTime)
		{
			SetFloatStringDamp(name, value, dampTime, deltaTime);
		}

		public void SetFloat(int id, float value)
		{
			SetFloatID(id, value);
		}

		public void SetFloat(int id, float value, float dampTime, float deltaTime)
		{
			SetFloatIDDamp(id, value, dampTime, deltaTime);
		}

		public bool GetBool(string name)
		{
			return GetBoolString(name);
		}

		public bool GetBool(int id)
		{
			return GetBoolID(id);
		}

		public void SetBool(string name, bool value)
		{
			SetBoolString(name, value);
		}

		public void SetBool(int id, bool value)
		{
			SetBoolID(id, value);
		}

		public int GetInteger(string name)
		{
			return GetIntegerString(name);
		}

		public int GetInteger(int id)
		{
			return GetIntegerID(id);
		}

		public void SetInteger(string name, int value)
		{
			SetIntegerString(name, value);
		}

		public void SetInteger(int id, int value)
		{
			SetIntegerID(id, value);
		}

		public void SetTrigger(string name)
		{
			SetTriggerString(name);
		}

		public void SetTrigger(int id)
		{
			SetTriggerID(id);
		}

		public void ResetTrigger(string name)
		{
			ResetTriggerString(name);
		}

		public void ResetTrigger(int id)
		{
			ResetTriggerID(id);
		}

		public bool IsParameterControlledByCurve(string name)
		{
			return IsParameterControlledByCurveString(name);
		}

		public bool IsParameterControlledByCurve(int id)
		{
			return IsParameterControlledByCurveID(id);
		}

		public Vector3 GetIKPosition(AvatarIKGoal goal)
		{
			CheckIfInIKPass();
			return GetGoalPosition(goal);
		}

		private Vector3 GetGoalPosition(AvatarIKGoal goal)
		{
			GetGoalPosition_Injected(goal, out var ret);
			return ret;
		}

		public void SetIKPosition(AvatarIKGoal goal, Vector3 goalPosition)
		{
			CheckIfInIKPass();
			SetGoalPosition(goal, goalPosition);
		}

		private void SetGoalPosition(AvatarIKGoal goal, Vector3 goalPosition)
		{
			SetGoalPosition_Injected(goal, ref goalPosition);
		}

		public Quaternion GetIKRotation(AvatarIKGoal goal)
		{
			CheckIfInIKPass();
			return GetGoalRotation(goal);
		}

		private Quaternion GetGoalRotation(AvatarIKGoal goal)
		{
			GetGoalRotation_Injected(goal, out var ret);
			return ret;
		}

		public void SetIKRotation(AvatarIKGoal goal, Quaternion goalRotation)
		{
			CheckIfInIKPass();
			SetGoalRotation(goal, goalRotation);
		}

		private void SetGoalRotation(AvatarIKGoal goal, Quaternion goalRotation)
		{
			SetGoalRotation_Injected(goal, ref goalRotation);
		}

		public float GetIKPositionWeight(AvatarIKGoal goal)
		{
			CheckIfInIKPass();
			return GetGoalWeightPosition(goal);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetGoalWeightPosition(AvatarIKGoal goal);

		public void SetIKPositionWeight(AvatarIKGoal goal, float value)
		{
			CheckIfInIKPass();
			SetGoalWeightPosition(goal, value);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetGoalWeightPosition(AvatarIKGoal goal, float value);

		public float GetIKRotationWeight(AvatarIKGoal goal)
		{
			CheckIfInIKPass();
			return GetGoalWeightRotation(goal);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetGoalWeightRotation(AvatarIKGoal goal);

		public void SetIKRotationWeight(AvatarIKGoal goal, float value)
		{
			CheckIfInIKPass();
			SetGoalWeightRotation(goal, value);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetGoalWeightRotation(AvatarIKGoal goal, float value);

		public Vector3 GetIKHintPosition(AvatarIKHint hint)
		{
			CheckIfInIKPass();
			return GetHintPosition(hint);
		}

		private Vector3 GetHintPosition(AvatarIKHint hint)
		{
			GetHintPosition_Injected(hint, out var ret);
			return ret;
		}

		public void SetIKHintPosition(AvatarIKHint hint, Vector3 hintPosition)
		{
			CheckIfInIKPass();
			SetHintPosition(hint, hintPosition);
		}

		private void SetHintPosition(AvatarIKHint hint, Vector3 hintPosition)
		{
			SetHintPosition_Injected(hint, ref hintPosition);
		}

		public float GetIKHintPositionWeight(AvatarIKHint hint)
		{
			CheckIfInIKPass();
			return GetHintWeightPosition(hint);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetHintWeightPosition(AvatarIKHint hint);

		public void SetIKHintPositionWeight(AvatarIKHint hint, float value)
		{
			CheckIfInIKPass();
			SetHintWeightPosition(hint, value);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetHintWeightPosition(AvatarIKHint hint, float value);

		public void SetLookAtPosition(Vector3 lookAtPosition)
		{
			CheckIfInIKPass();
			SetLookAtPositionInternal(lookAtPosition);
		}

		[NativeMethod("SetLookAtPosition")]
		private void SetLookAtPositionInternal(Vector3 lookAtPosition)
		{
			SetLookAtPositionInternal_Injected(ref lookAtPosition);
		}

		public void SetLookAtWeight(float weight)
		{
			CheckIfInIKPass();
			SetLookAtWeightInternal(weight, 0f, 1f, 0f, 0.5f);
		}

		public void SetLookAtWeight(float weight, float bodyWeight)
		{
			CheckIfInIKPass();
			SetLookAtWeightInternal(weight, bodyWeight, 1f, 0f, 0.5f);
		}

		public void SetLookAtWeight(float weight, float bodyWeight, float headWeight)
		{
			CheckIfInIKPass();
			SetLookAtWeightInternal(weight, bodyWeight, headWeight, 0f, 0.5f);
		}

		public void SetLookAtWeight(float weight, float bodyWeight, float headWeight, float eyesWeight)
		{
			CheckIfInIKPass();
			SetLookAtWeightInternal(weight, bodyWeight, headWeight, eyesWeight, 0.5f);
		}

		public void SetLookAtWeight(float weight, [UnityEngine.Internal.DefaultValue("0.0f")] float bodyWeight, [UnityEngine.Internal.DefaultValue("1.0f")] float headWeight, [UnityEngine.Internal.DefaultValue("0.0f")] float eyesWeight, [UnityEngine.Internal.DefaultValue("0.5f")] float clampWeight)
		{
			CheckIfInIKPass();
			SetLookAtWeightInternal(weight, bodyWeight, headWeight, eyesWeight, clampWeight);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("SetLookAtWeight")]
		private extern void SetLookAtWeightInternal(float weight, float bodyWeight, float headWeight, float eyesWeight, float clampWeight);

		public void SetBoneLocalRotation(HumanBodyBones humanBoneId, Quaternion rotation)
		{
			CheckIfInIKPass();
			SetBoneLocalRotationInternal(HumanTrait.GetBoneIndexFromMono((int)humanBoneId), rotation);
		}

		[NativeMethod("SetBoneLocalRotation")]
		private void SetBoneLocalRotationInternal(int humanBoneId, Quaternion rotation)
		{
			SetBoneLocalRotationInternal_Injected(humanBoneId, ref rotation);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern ScriptableObject GetBehaviour([NotNull("ArgumentNullException")] Type type);

		public T GetBehaviour<T>() where T : StateMachineBehaviour
		{
			return GetBehaviour(typeof(T)) as T;
		}

		private static T[] ConvertStateMachineBehaviour<T>(ScriptableObject[] rawObjects) where T : StateMachineBehaviour
		{
			if (rawObjects == null)
			{
				return null;
			}
			T[] array = new T[rawObjects.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (T)rawObjects[i];
			}
			return array;
		}

		public T[] GetBehaviours<T>() where T : StateMachineBehaviour
		{
			return ConvertStateMachineBehaviour<T>(InternalGetBehaviours(typeof(T)));
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::InternalGetBehaviours", HasExplicitThis = true)]
		internal extern ScriptableObject[] InternalGetBehaviours([NotNull("ArgumentNullException")] Type type);

		public StateMachineBehaviour[] GetBehaviours(int fullPathHash, int layerIndex)
		{
			return InternalGetBehavioursByKey(fullPathHash, layerIndex, typeof(StateMachineBehaviour)) as StateMachineBehaviour[];
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::InternalGetBehavioursByKey", HasExplicitThis = true)]
		internal extern ScriptableObject[] InternalGetBehavioursByKey(int fullPathHash, int layerIndex, [NotNull("ArgumentNullException")] Type type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetLayerName(int layerIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetLayerIndex(string layerName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetLayerWeight(int layerIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetLayerWeight(int layerIndex, float weight);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetAnimatorStateInfo(int layerIndex, StateInfoIndex stateInfoIndex, out AnimatorStateInfo info);

		public AnimatorStateInfo GetCurrentAnimatorStateInfo(int layerIndex)
		{
			GetAnimatorStateInfo(layerIndex, StateInfoIndex.CurrentState, out var info);
			return info;
		}

		public AnimatorStateInfo GetNextAnimatorStateInfo(int layerIndex)
		{
			GetAnimatorStateInfo(layerIndex, StateInfoIndex.NextState, out var info);
			return info;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetAnimatorTransitionInfo(int layerIndex, out AnimatorTransitionInfo info);

		public AnimatorTransitionInfo GetAnimatorTransitionInfo(int layerIndex)
		{
			GetAnimatorTransitionInfo(layerIndex, out var info);
			return info;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetAnimatorClipInfoCount(int layerIndex, bool current);

		public int GetCurrentAnimatorClipInfoCount(int layerIndex)
		{
			return GetAnimatorClipInfoCount(layerIndex, current: true);
		}

		public int GetNextAnimatorClipInfoCount(int layerIndex)
		{
			return GetAnimatorClipInfoCount(layerIndex, current: false);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::GetCurrentAnimatorClipInfo", HasExplicitThis = true)]
		public extern AnimatorClipInfo[] GetCurrentAnimatorClipInfo(int layerIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::GetNextAnimatorClipInfo", HasExplicitThis = true)]
		public extern AnimatorClipInfo[] GetNextAnimatorClipInfo(int layerIndex);

		public void GetCurrentAnimatorClipInfo(int layerIndex, List<AnimatorClipInfo> clips)
		{
			if (clips == null)
			{
				throw new ArgumentNullException("clips");
			}
			GetAnimatorClipInfoInternal(layerIndex, isCurrent: true, clips);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::GetAnimatorClipInfoInternal", HasExplicitThis = true)]
		private extern void GetAnimatorClipInfoInternal(int layerIndex, bool isCurrent, object clips);

		public void GetNextAnimatorClipInfo(int layerIndex, List<AnimatorClipInfo> clips)
		{
			if (clips == null)
			{
				throw new ArgumentNullException("clips");
			}
			GetAnimatorClipInfoInternal(layerIndex, isCurrent: false, clips);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsInTransition(int layerIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::GetParameterInternal", HasExplicitThis = true)]
		private extern AnimatorControllerParameter GetParameterInternal(int index);

		public AnimatorControllerParameter GetParameter(int index)
		{
			AnimatorControllerParameter parameterInternal = GetParameterInternal(index);
			if (parameterInternal.m_Type == (AnimatorControllerParameterType)0)
			{
				throw new IndexOutOfRangeException("Index must be between 0 and " + parameterCount);
			}
			return parameterInternal;
		}

		private void MatchTarget(Vector3 matchPosition, Quaternion matchRotation, int targetBodyPart, MatchTargetWeightMask weightMask, float startNormalizedTime, float targetNormalizedTime, bool completeMatch)
		{
			MatchTarget_Injected(ref matchPosition, ref matchRotation, targetBodyPart, ref weightMask, startNormalizedTime, targetNormalizedTime, completeMatch);
		}

		public void MatchTarget(Vector3 matchPosition, Quaternion matchRotation, AvatarTarget targetBodyPart, MatchTargetWeightMask weightMask, float startNormalizedTime)
		{
			MatchTarget(matchPosition, matchRotation, (int)targetBodyPart, weightMask, startNormalizedTime, 1f, completeMatch: true);
		}

		public void MatchTarget(Vector3 matchPosition, Quaternion matchRotation, AvatarTarget targetBodyPart, MatchTargetWeightMask weightMask, float startNormalizedTime, [UnityEngine.Internal.DefaultValue("1")] float targetNormalizedTime)
		{
			MatchTarget(matchPosition, matchRotation, (int)targetBodyPart, weightMask, startNormalizedTime, targetNormalizedTime, completeMatch: true);
		}

		public void MatchTarget(Vector3 matchPosition, Quaternion matchRotation, AvatarTarget targetBodyPart, MatchTargetWeightMask weightMask, float startNormalizedTime, [UnityEngine.Internal.DefaultValue("1")] float targetNormalizedTime, [UnityEngine.Internal.DefaultValue("true")] bool completeMatch)
		{
			MatchTarget(matchPosition, matchRotation, (int)targetBodyPart, weightMask, startNormalizedTime, targetNormalizedTime, completeMatch);
		}

		public void InterruptMatchTarget()
		{
			InterruptMatchTarget(completeMatch: true);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InterruptMatchTarget([UnityEngine.Internal.DefaultValue("true")] bool completeMatch);

		[Obsolete("ForceStateNormalizedTime is deprecated. Please use Play or CrossFade instead.")]
		public void ForceStateNormalizedTime(float normalizedTime)
		{
			Play(0, 0, normalizedTime);
		}

		public void CrossFadeInFixedTime(string stateName, float fixedTransitionDuration)
		{
			float normalizedTransitionTime = 0f;
			float fixedTimeOffset = 0f;
			int layer = -1;
			CrossFadeInFixedTime(StringToHash(stateName), fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		public void CrossFadeInFixedTime(string stateName, float fixedTransitionDuration, int layer)
		{
			float normalizedTransitionTime = 0f;
			float fixedTimeOffset = 0f;
			CrossFadeInFixedTime(StringToHash(stateName), fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		public void CrossFadeInFixedTime(string stateName, float fixedTransitionDuration, int layer, float fixedTimeOffset)
		{
			float normalizedTransitionTime = 0f;
			CrossFadeInFixedTime(StringToHash(stateName), fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		public void CrossFadeInFixedTime(string stateName, float fixedTransitionDuration, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("0.0f")] float fixedTimeOffset, [UnityEngine.Internal.DefaultValue("0.0f")] float normalizedTransitionTime)
		{
			CrossFadeInFixedTime(StringToHash(stateName), fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		public void CrossFadeInFixedTime(int stateHashName, float fixedTransitionDuration, int layer, float fixedTimeOffset)
		{
			float normalizedTransitionTime = 0f;
			CrossFadeInFixedTime(stateHashName, fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		public void CrossFadeInFixedTime(int stateHashName, float fixedTransitionDuration, int layer)
		{
			float normalizedTransitionTime = 0f;
			float fixedTimeOffset = 0f;
			CrossFadeInFixedTime(stateHashName, fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		public void CrossFadeInFixedTime(int stateHashName, float fixedTransitionDuration)
		{
			float normalizedTransitionTime = 0f;
			float fixedTimeOffset = 0f;
			int layer = -1;
			CrossFadeInFixedTime(stateHashName, fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::CrossFadeInFixedTime", HasExplicitThis = true)]
		public extern void CrossFadeInFixedTime(int stateHashName, float fixedTransitionDuration, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("0.0f")] float fixedTimeOffset, [UnityEngine.Internal.DefaultValue("0.0f")] float normalizedTransitionTime);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::WriteDefaultValues", HasExplicitThis = true)]
		public extern void WriteDefaultValues();

		public void CrossFade(string stateName, float normalizedTransitionDuration, int layer, float normalizedTimeOffset)
		{
			float normalizedTransitionTime = 0f;
			CrossFade(stateName, normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		public void CrossFade(string stateName, float normalizedTransitionDuration, int layer)
		{
			float normalizedTransitionTime = 0f;
			float normalizedTimeOffset = float.NegativeInfinity;
			CrossFade(stateName, normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		public void CrossFade(string stateName, float normalizedTransitionDuration)
		{
			float normalizedTransitionTime = 0f;
			float normalizedTimeOffset = float.NegativeInfinity;
			int layer = -1;
			CrossFade(stateName, normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		public void CrossFade(string stateName, float normalizedTransitionDuration, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float normalizedTimeOffset, [UnityEngine.Internal.DefaultValue("0.0f")] float normalizedTransitionTime)
		{
			CrossFade(StringToHash(stateName), normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::CrossFade", HasExplicitThis = true)]
		public extern void CrossFade(int stateHashName, float normalizedTransitionDuration, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("0.0f")] float normalizedTimeOffset, [UnityEngine.Internal.DefaultValue("0.0f")] float normalizedTransitionTime);

		public void CrossFade(int stateHashName, float normalizedTransitionDuration, int layer, float normalizedTimeOffset)
		{
			float normalizedTransitionTime = 0f;
			CrossFade(stateHashName, normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		public void CrossFade(int stateHashName, float normalizedTransitionDuration, int layer)
		{
			float normalizedTransitionTime = 0f;
			float normalizedTimeOffset = float.NegativeInfinity;
			CrossFade(stateHashName, normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		public void CrossFade(int stateHashName, float normalizedTransitionDuration)
		{
			float normalizedTransitionTime = 0f;
			float normalizedTimeOffset = float.NegativeInfinity;
			int layer = -1;
			CrossFade(stateHashName, normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		public void PlayInFixedTime(string stateName, int layer)
		{
			float fixedTime = float.NegativeInfinity;
			PlayInFixedTime(stateName, layer, fixedTime);
		}

		public void PlayInFixedTime(string stateName)
		{
			float fixedTime = float.NegativeInfinity;
			int layer = -1;
			PlayInFixedTime(stateName, layer, fixedTime);
		}

		public void PlayInFixedTime(string stateName, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float fixedTime)
		{
			PlayInFixedTime(StringToHash(stateName), layer, fixedTime);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::PlayInFixedTime", HasExplicitThis = true)]
		public extern void PlayInFixedTime(int stateNameHash, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float fixedTime);

		public void PlayInFixedTime(int stateNameHash, int layer)
		{
			float fixedTime = float.NegativeInfinity;
			PlayInFixedTime(stateNameHash, layer, fixedTime);
		}

		public void PlayInFixedTime(int stateNameHash)
		{
			float fixedTime = float.NegativeInfinity;
			int layer = -1;
			PlayInFixedTime(stateNameHash, layer, fixedTime);
		}

		public void Play(string stateName, int layer)
		{
			float normalizedTime = float.NegativeInfinity;
			Play(stateName, layer, normalizedTime);
		}

		public void Play(string stateName)
		{
			float normalizedTime = float.NegativeInfinity;
			int layer = -1;
			Play(stateName, layer, normalizedTime);
		}

		public void Play(string stateName, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float normalizedTime)
		{
			Play(StringToHash(stateName), layer, normalizedTime);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::Play", HasExplicitThis = true)]
		public extern void Play(int stateNameHash, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float normalizedTime);

		public void Play(int stateNameHash, int layer)
		{
			float normalizedTime = float.NegativeInfinity;
			Play(stateNameHash, layer, normalizedTime);
		}

		public void Play(int stateNameHash)
		{
			float normalizedTime = float.NegativeInfinity;
			int layer = -1;
			Play(stateNameHash, layer, normalizedTime);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTarget(AvatarTarget targetIndex, float targetNormalizedTime);

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use mask and layers to control subset of transfroms in a skeleton.", true)]
		public bool IsControlled(Transform transform)
		{
			return false;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsBoneTransform(Transform transform);

		public Transform GetBoneTransform(HumanBodyBones humanBoneId)
		{
			if (avatar == null)
			{
				throw new InvalidOperationException("Avatar is null.");
			}
			if (!avatar.isValid)
			{
				throw new InvalidOperationException("Avatar is not valid.");
			}
			if (!avatar.isHuman)
			{
				throw new InvalidOperationException("Avatar is not of type humanoid.");
			}
			if (humanBoneId < HumanBodyBones.Hips || humanBoneId >= HumanBodyBones.LastBone)
			{
				throw new IndexOutOfRangeException("humanBoneId must be between 0 and " + HumanBodyBones.LastBone);
			}
			return GetBoneTransformInternal(HumanTrait.GetBoneIndexFromMono((int)humanBoneId));
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetBoneTransform")]
		internal extern Transform GetBoneTransformInternal(int humanBoneId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StartPlayback();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopPlayback();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StartRecording(int frameCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopRecording();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetRecorderStartTime();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetRecorderStopTime();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void ClearInternalControllerPlayable();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasState(int layerIndex, int stateID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "ScriptingStringToCRC32", IsThreadSafe = true)]
		public static extern int StringToHash(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string GetStats();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::GetCurrentGraph", HasExplicitThis = true)]
		private extern void GetCurrentGraph(ref PlayableGraph graph);

		private void CheckIfInIKPass()
		{
			if (logWarnings && !IsInIKPass())
			{
				Debug.LogWarning("Setting and getting Body Position/Rotation, IK Goals, Lookat and BoneLocalRotation should only be done in OnAnimatorIK or OnStateIK");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool IsInIKPass();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::SetFloatString", HasExplicitThis = true)]
		private extern void SetFloatString(string name, float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::SetFloatID", HasExplicitThis = true)]
		private extern void SetFloatID(int id, float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::GetFloatString", HasExplicitThis = true)]
		private extern float GetFloatString(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::GetFloatID", HasExplicitThis = true)]
		private extern float GetFloatID(int id);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::SetBoolString", HasExplicitThis = true)]
		private extern void SetBoolString(string name, bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::SetBoolID", HasExplicitThis = true)]
		private extern void SetBoolID(int id, bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::GetBoolString", HasExplicitThis = true)]
		private extern bool GetBoolString(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::GetBoolID", HasExplicitThis = true)]
		private extern bool GetBoolID(int id);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::SetIntegerString", HasExplicitThis = true)]
		private extern void SetIntegerString(string name, int value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::SetIntegerID", HasExplicitThis = true)]
		private extern void SetIntegerID(int id, int value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::GetIntegerString", HasExplicitThis = true)]
		private extern int GetIntegerString(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::GetIntegerID", HasExplicitThis = true)]
		private extern int GetIntegerID(int id);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::SetTriggerString", HasExplicitThis = true)]
		private extern void SetTriggerString(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::SetTriggerID", HasExplicitThis = true)]
		private extern void SetTriggerID(int id);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::ResetTriggerString", HasExplicitThis = true)]
		private extern void ResetTriggerString(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::ResetTriggerID", HasExplicitThis = true)]
		private extern void ResetTriggerID(int id);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::IsParameterControlledByCurveString", HasExplicitThis = true)]
		private extern bool IsParameterControlledByCurveString(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::IsParameterControlledByCurveID", HasExplicitThis = true)]
		private extern bool IsParameterControlledByCurveID(int id);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::SetFloatStringDamp", HasExplicitThis = true)]
		private extern void SetFloatStringDamp(string name, float value, float dampTime, float deltaTime);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "AnimatorBindings::SetFloatIDDamp", HasExplicitThis = true)]
		private extern void SetFloatIDDamp(int id, float value, float dampTime, float deltaTime);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeConditional("UNITY_EDITOR")]
		internal extern void OnUpdateModeChanged();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeConditional("UNITY_EDITOR")]
		internal extern void OnCullingModeChanged();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeConditional("UNITY_EDITOR")]
		internal extern void WriteDefaultPose();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("UpdateWithDelta")]
		public extern void Update(float deltaTime);

		public void Rebind()
		{
			Rebind(writeDefaultValues: true);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Rebind(bool writeDefaultValues);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ApplyBuiltinRootMotion();

		[NativeConditional("UNITY_EDITOR")]
		internal void EvaluateController()
		{
			EvaluateController(0f);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void EvaluateController(float deltaTime);

		[NativeConditional("UNITY_EDITOR")]
		internal string GetCurrentStateName(int layerIndex)
		{
			return GetAnimatorStateName(layerIndex, current: true);
		}

		[NativeConditional("UNITY_EDITOR")]
		internal string GetNextStateName(int layerIndex)
		{
			return GetAnimatorStateName(layerIndex, current: false);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeConditional("UNITY_EDITOR")]
		private extern string GetAnimatorStateName(int layerIndex, bool current);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern string ResolveHash(int hash);

		[Obsolete("GetVector is deprecated.")]
		public Vector3 GetVector(string name)
		{
			return Vector3.zero;
		}

		[Obsolete("GetVector is deprecated.")]
		public Vector3 GetVector(int id)
		{
			return Vector3.zero;
		}

		[Obsolete("SetVector is deprecated.")]
		public void SetVector(string name, Vector3 value)
		{
		}

		[Obsolete("SetVector is deprecated.")]
		public void SetVector(int id, Vector3 value)
		{
		}

		[Obsolete("GetQuaternion is deprecated.")]
		public Quaternion GetQuaternion(string name)
		{
			return Quaternion.identity;
		}

		[Obsolete("GetQuaternion is deprecated.")]
		public Quaternion GetQuaternion(int id)
		{
			return Quaternion.identity;
		}

		[Obsolete("SetQuaternion is deprecated.")]
		public void SetQuaternion(string name, Quaternion value)
		{
		}

		[Obsolete("SetQuaternion is deprecated.")]
		public void SetQuaternion(int id, Quaternion value)
		{
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_deltaPosition_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_deltaRotation_Injected(out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_velocity_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_angularVelocity_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_rootPosition_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_rootPosition_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_rootRotation_Injected(out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_rootRotation_Injected(ref Quaternion value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_bodyPositionInternal_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_bodyPositionInternal_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_bodyRotationInternal_Injected(out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_bodyRotationInternal_Injected(ref Quaternion value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetGoalPosition_Injected(AvatarIKGoal goal, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetGoalPosition_Injected(AvatarIKGoal goal, ref Vector3 goalPosition);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetGoalRotation_Injected(AvatarIKGoal goal, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetGoalRotation_Injected(AvatarIKGoal goal, ref Quaternion goalRotation);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetHintPosition_Injected(AvatarIKHint hint, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetHintPosition_Injected(AvatarIKHint hint, ref Vector3 hintPosition);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetLookAtPositionInternal_Injected(ref Vector3 lookAtPosition);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBoneLocalRotationInternal_Injected(int humanBoneId, ref Quaternion rotation);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_pivotPosition_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void MatchTarget_Injected(ref Vector3 matchPosition, ref Quaternion matchRotation, int targetBodyPart, ref MatchTargetWeightMask weightMask, float startNormalizedTime, float targetNormalizedTime, bool completeMatch);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_targetPosition_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_targetRotation_Injected(out Quaternion ret);
	}
	[StructLayout(LayoutKind.Sequential)]
	[NativeType(CodegenOptions.Custom, "MonoAnimatorControllerParameter")]
	[UsedByNativeCode]
	[NativeAsStruct]
	[NativeHeader("Modules/Animation/AnimatorControllerParameter.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimatorControllerParameter.bindings.h")]
	public class AnimatorControllerParameter
	{
		internal string m_Name = "";

		internal AnimatorControllerParameterType m_Type;

		internal float m_DefaultFloat;

		internal int m_DefaultInt;

		internal bool m_DefaultBool;

		public string name => m_Name;

		public int nameHash => Animator.StringToHash(m_Name);

		public AnimatorControllerParameterType type
		{
			get
			{
				return m_Type;
			}
			set
			{
				m_Type = value;
			}
		}

		public float defaultFloat
		{
			get
			{
				return m_DefaultFloat;
			}
			set
			{
				m_DefaultFloat = value;
			}
		}

		public int defaultInt
		{
			get
			{
				return m_DefaultInt;
			}
			set
			{
				m_DefaultInt = value;
			}
		}

		public bool defaultBool
		{
			get
			{
				return m_DefaultBool;
			}
			set
			{
				m_DefaultBool = value;
			}
		}

		public override bool Equals(object o)
		{
			return o is AnimatorControllerParameter animatorControllerParameter && m_Name == animatorControllerParameter.m_Name && m_Type == animatorControllerParameter.m_Type && m_DefaultFloat == animatorControllerParameter.m_DefaultFloat && m_DefaultInt == animatorControllerParameter.m_DefaultInt && m_DefaultBool == animatorControllerParameter.m_DefaultBool;
		}

		public override int GetHashCode()
		{
			return name.GetHashCode();
		}
	}
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	[Obsolete("This class is not used anymore. See AnimatorOverrideController.GetOverrides() and AnimatorOverrideController.ApplyOverrides()")]
	public class AnimationClipPair
	{
		public AnimationClip originalClip;

		public AnimationClip overrideClip;
	}
	[NativeHeader("Modules/Animation/ScriptBindings/Animation.bindings.h")]
	[UsedByNativeCode]
	[NativeHeader("Modules/Animation/AnimatorOverrideController.h")]
	public class AnimatorOverrideController : RuntimeAnimatorController
	{
		internal delegate void OnOverrideControllerDirtyCallback();

		internal OnOverrideControllerDirtyCallback OnOverrideControllerDirty;

		public extern RuntimeAnimatorController runtimeAnimatorController
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetAnimatorController")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("SetAnimatorController")]
			set;
		}

		public AnimationClip this[string name]
		{
			get
			{
				return Internal_GetClipByName(name, returnEffectiveClip: true);
			}
			set
			{
				Internal_SetClipByName(name, value);
			}
		}

		public AnimationClip this[AnimationClip clip]
		{
			get
			{
				return GetClip(clip, returnEffectiveClip: true);
			}
			set
			{
				SetClip(clip, value, notify: true);
			}
		}

		public extern int overridesCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetOriginalClipsCount")]
			get;
		}

		[Obsolete("AnimatorOverrideController.clips property is deprecated. Use AnimatorOverrideController.GetOverrides and AnimatorOverrideController.ApplyOverrides instead.")]
		public AnimationClipPair[] clips
		{
			get
			{
				int num = overridesCount;
				AnimationClipPair[] array = new AnimationClipPair[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = new AnimationClipPair();
					array[i].originalClip = GetOriginalClip(i);
					array[i].overrideClip = GetOverrideClip(array[i].originalClip);
				}
				return array;
			}
			set
			{
				for (int i = 0; i < value.Length; i++)
				{
					SetClip(value[i].originalClip, value[i].overrideClip, notify: false);
				}
				SendNotification();
			}
		}

		public AnimatorOverrideController()
		{
			Internal_Create(this, null);
			OnOverrideControllerDirty = null;
		}

		public AnimatorOverrideController(RuntimeAnimatorController controller)
		{
			Internal_Create(this, controller);
			OnOverrideControllerDirty = null;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("AnimationBindings::CreateAnimatorOverrideController")]
		private static extern void Internal_Create([Writable] AnimatorOverrideController self, RuntimeAnimatorController controller);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetClip")]
		private extern AnimationClip Internal_GetClipByName(string name, bool returnEffectiveClip);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("SetClip")]
		private extern void Internal_SetClipByName(string name, AnimationClip clip);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AnimationClip GetClip(AnimationClip originalClip, bool returnEffectiveClip);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetClip(AnimationClip originalClip, AnimationClip overrideClip, bool notify);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SendNotification();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AnimationClip GetOriginalClip(int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AnimationClip GetOverrideClip(AnimationClip originalClip);

		public void GetOverrides(List<KeyValuePair<AnimationClip, AnimationClip>> overrides)
		{
			if (overrides == null)
			{
				throw new ArgumentNullException("overrides");
			}
			int num = overridesCount;
			if (overrides.Capacity < num)
			{
				overrides.Capacity = num;
			}
			overrides.Clear();
			for (int i = 0; i < num; i++)
			{
				AnimationClip originalClip = GetOriginalClip(i);
				overrides.Add(new KeyValuePair<AnimationClip, AnimationClip>(originalClip, GetOverrideClip(originalClip)));
			}
		}

		public void ApplyOverrides(IList<KeyValuePair<AnimationClip, AnimationClip>> overrides)
		{
			if (overrides == null)
			{
				throw new ArgumentNullException("overrides");
			}
			for (int i = 0; i < overrides.Count; i++)
			{
				SetClip(overrides[i].Key, overrides[i].Value, notify: false);
			}
			SendNotification();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeConditional("UNITY_EDITOR")]
		internal extern void PerformOverrideClipListCleanup();

		[RequiredByNativeCode]
		[NativeConditional("UNITY_EDITOR")]
		internal static void OnInvalidateOverrideController(AnimatorOverrideController controller)
		{
			if (controller.OnOverrideControllerDirty != null)
			{
				controller.OnOverrideControllerDirty();
			}
		}
	}
	[NativeHeader("Modules/Animation/OptimizeTransformHierarchy.h")]
	public class AnimatorUtility
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern void OptimizeTransformHierarchy([NotNull("NullExceptionObject")] GameObject go, string[] exposedTransforms);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction]
		public static extern void DeoptimizeTransformHierarchy([NotNull("NullExceptionObject")] GameObject go);
	}
	public enum BodyDof
	{
		SpineFrontBack,
		SpineLeftRight,
		SpineRollLeftRight,
		ChestFrontBack,
		ChestLeftRight,
		ChestRollLeftRight,
		UpperChestFrontBack,
		UpperChestLeftRight,
		UpperChestRollLeftRight,
		LastBodyDof
	}
	public enum HeadDof
	{
		NeckFrontBack,
		NeckLeftRight,
		NeckRollLeftRight,
		HeadFrontBack,
		HeadLeftRight,
		HeadRollLeftRight,
		LeftEyeDownUp,
		LeftEyeInOut,
		RightEyeDownUp,
		RightEyeInOut,
		JawDownUp,
		JawLeftRight,
		LastHeadDof
	}
	public enum LegDof
	{
		UpperLegFrontBack,
		UpperLegInOut,
		UpperLegRollInOut,
		LegCloseOpen,
		LegRollInOut,
		FootCloseOpen,
		FootInOut,
		ToesUpDown,
		LastLegDof
	}
	public enum ArmDof
	{
		ShoulderDownUp,
		ShoulderFrontBack,
		ArmDownUp,
		ArmFrontBack,
		ArmRollInOut,
		ForeArmCloseOpen,
		ForeArmRollInOut,
		HandDownUp,
		HandInOut,
		LastArmDof
	}
	public enum FingerDof
	{
		ProximalDownUp,
		ProximalInOut,
		IntermediateCloseOpen,
		DistalCloseOpen,
		LastFingerDof
	}
	public enum HumanPartDof
	{
		Body,
		Head,
		LeftLeg,
		RightLeg,
		LeftArm,
		RightArm,
		LeftThumb,
		LeftIndex,
		LeftMiddle,
		LeftRing,
		LeftLittle,
		RightThumb,
		RightIndex,
		RightMiddle,
		RightRing,
		RightLittle,
		LastHumanPartDof
	}
	internal enum Dof
	{
		BodyDofStart = 0,
		HeadDofStart = 9,
		LeftLegDofStart = 21,
		RightLegDofStart = 29,
		LeftArmDofStart = 37,
		RightArmDofStart = 46,
		LeftThumbDofStart = 55,
		LeftIndexDofStart = 59,
		LeftMiddleDofStart = 63,
		LeftRingDofStart = 67,
		LeftLittleDofStart = 71,
		RightThumbDofStart = 75,
		RightIndexDofStart = 79,
		RightMiddleDofStart = 83,
		RightRingDofStart = 87,
		RightLittleDofStart = 91,
		LastDof = 95
	}
	public enum HumanBodyBones
	{
		Hips = 0,
		LeftUpperLeg = 1,
		RightUpperLeg = 2,
		LeftLowerLeg = 3,
		RightLowerLeg = 4,
		LeftFoot = 5,
		RightFoot = 6,
		Spine = 7,
		Chest = 8,
		UpperChest = 54,
		Neck = 9,
		Head = 10,
		LeftShoulder = 11,
		RightShoulder = 12,
		LeftUpperArm = 13,
		RightUpperArm = 14,
		LeftLowerArm = 15,
		RightLowerArm = 16,
		LeftHand = 17,
		RightHand = 18,
		LeftToes = 19,
		RightToes = 20,
		LeftEye = 21,
		RightEye = 22,
		Jaw = 23,
		LeftThumbProximal = 24,
		LeftThumbIntermediate = 25,
		LeftThumbDistal = 26,
		LeftIndexProximal = 27,
		LeftIndexIntermediate = 28,
		LeftIndexDistal = 29,
		LeftMiddleProximal = 30,
		LeftMiddleIntermediate = 31,
		LeftMiddleDistal = 32,
		LeftRingProximal = 33,
		LeftRingIntermediate = 34,
		LeftRingDistal = 35,
		LeftLittleProximal = 36,
		LeftLittleIntermediate = 37,
		LeftLittleDistal = 38,
		RightThumbProximal = 39,
		RightThumbIntermediate = 40,
		RightThumbDistal = 41,
		RightIndexProximal = 42,
		RightIndexIntermediate = 43,
		RightIndexDistal = 44,
		RightMiddleProximal = 45,
		RightMiddleIntermediate = 46,
		RightMiddleDistal = 47,
		RightRingProximal = 48,
		RightRingIntermediate = 49,
		RightRingDistal = 50,
		RightLittleProximal = 51,
		RightLittleIntermediate = 52,
		RightLittleDistal = 53,
		LastBone = 55
	}
	internal enum HumanParameter
	{
		UpperArmTwist,
		LowerArmTwist,
		UpperLegTwist,
		LowerLegTwist,
		ArmStretch,
		LegStretch,
		FeetSpacing
	}
	[NativeHeader("Modules/Animation/Avatar.h")]
	[UsedByNativeCode]
	public class Avatar : Object
	{
		public extern bool isValid
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("IsValid")]
			get;
		}

		public extern bool isHuman
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("IsHuman")]
			get;
		}

		public HumanDescription humanDescription
		{
			get
			{
				get_humanDescription_Injected(out var ret);
				return ret;
			}
		}

		private Avatar()
		{
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetMuscleMinMax(int muscleId, float min, float max);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetParameter(int parameterId, float value);

		internal float GetAxisLength(int humanId)
		{
			return Internal_GetAxisLength(HumanTrait.GetBoneIndexFromMono(humanId));
		}

		internal Quaternion GetPreRotation(int humanId)
		{
			return Internal_GetPreRotation(HumanTrait.GetBoneIndexFromMono(humanId));
		}

		internal Quaternion GetPostRotation(int humanId)
		{
			return Internal_GetPostRotation(HumanTrait.GetBoneIndexFromMono(humanId));
		}

		internal Quaternion GetZYPostQ(int humanId, Quaternion parentQ, Quaternion q)
		{
			return Internal_GetZYPostQ(HumanTrait.GetBoneIndexFromMono(humanId), parentQ, q);
		}

		internal Quaternion GetZYRoll(int humanId, Vector3 uvw)
		{
			return Internal_GetZYRoll(HumanTrait.GetBoneIndexFromMono(humanId), uvw);
		}

		internal Vector3 GetLimitSign(int humanId)
		{
			return Internal_GetLimitSign(HumanTrait.GetBoneIndexFromMono(humanId));
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetAxisLength")]
		internal extern float Internal_GetAxisLength(int humanId);

		[NativeMethod("GetPreRotation")]
		internal Quaternion Internal_GetPreRotation(int humanId)
		{
			Internal_GetPreRotation_Injected(humanId, out var ret);
			return ret;
		}

		[NativeMethod("GetPostRotation")]
		internal Quaternion Internal_GetPostRotation(int humanId)
		{
			Internal_GetPostRotation_Injected(humanId, out var ret);
			return ret;
		}

		[NativeMethod("GetZYPostQ")]
		internal Quaternion Internal_GetZYPostQ(int humanId, Quaternion parentQ, Quaternion q)
		{
			Internal_GetZYPostQ_Injected(humanId, ref parentQ, ref q, out var ret);
			return ret;
		}

		[NativeMethod("GetZYRoll")]
		internal Quaternion Internal_GetZYRoll(int humanId, Vector3 uvw)
		{
			Internal_GetZYRoll_Injected(humanId, ref uvw, out var ret);
			return ret;
		}

		[NativeMethod("GetLimitSign")]
		internal Vector3 Internal_GetLimitSign(int humanId)
		{
			Internal_GetLimitSign_Injected(humanId, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_humanDescription_Injected(out HumanDescription ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetPreRotation_Injected(int humanId, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetPostRotation_Injected(int humanId, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetZYPostQ_Injected(int humanId, ref Quaternion parentQ, ref Quaternion q, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetZYRoll_Injected(int humanId, ref Vector3 uvw, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_GetLimitSign_Injected(int humanId, out Vector3 ret);
	}
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/HumanDescription.h")]
	[NativeType(CodegenOptions.Custom, "MonoSkeletonBone")]
	public struct SkeletonBone
	{
		[NativeName("m_Name")]
		public string name;

		[NativeName("m_ParentName")]
		internal string parentName;

		[NativeName("m_Position")]
		public Vector3 position;

		[NativeName("m_Rotation")]
		public Quaternion rotation;

		[NativeName("m_Scale")]
		public Vector3 scale;

		[Obsolete("transformModified is no longer used and has been deprecated.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int transformModified
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}
	}
	[NativeHeader("Modules/Animation/ScriptBindings/AvatarBuilder.bindings.h")]
	[NativeHeader("Modules/Animation/HumanDescription.h")]
	[NativeType(CodegenOptions.Custom, "MonoHumanLimit")]
	public struct HumanLimit
	{
		private Vector3 m_Min;

		private Vector3 m_Max;

		private Vector3 m_Center;

		private float m_AxisLength;

		private int m_UseDefaultValues;

		public bool useDefaultValues
		{
			get
			{
				return m_UseDefaultValues != 0;
			}
			set
			{
				m_UseDefaultValues = (value ? 1 : 0);
			}
		}

		public Vector3 min
		{
			get
			{
				return m_Min;
			}
			set
			{
				m_Min = value;
			}
		}

		public Vector3 max
		{
			get
			{
				return m_Max;
			}
			set
			{
				m_Max = value;
			}
		}

		public Vector3 center
		{
			get
			{
				return m_Center;
			}
			set
			{
				m_Center = value;
			}
		}

		public float axisLength
		{
			get
			{
				return m_AxisLength;
			}
			set
			{
				m_AxisLength = value;
			}
		}
	}
	[RequiredByNativeCode]
	[NativeType(CodegenOptions.Custom, "MonoHumanBone")]
	[NativeHeader("Modules/Animation/HumanDescription.h")]
	public struct HumanBone
	{
		private string m_BoneName;

		private string m_HumanName;

		[NativeName("m_Limit")]
		public HumanLimit limit;

		public string boneName
		{
			get
			{
				return m_BoneName;
			}
			set
			{
				m_BoneName = value;
			}
		}

		public string humanName
		{
			get
			{
				return m_HumanName;
			}
			set
			{
				m_HumanName = value;
			}
		}
	}
	[NativeHeader("Modules/Animation/HumanDescription.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AvatarBuilder.bindings.h")]
	public struct HumanDescription
	{
		[NativeName("m_Human")]
		public HumanBone[] human;

		[NativeName("m_Skeleton")]
		public SkeletonBone[] skeleton;

		internal float m_ArmTwist;

		internal float m_ForeArmTwist;

		internal float m_UpperLegTwist;

		internal float m_LegTwist;

		internal float m_ArmStretch;

		internal float m_LegStretch;

		internal float m_FeetSpacing;

		internal float m_GlobalScale;

		internal string m_RootMotionBoneName;

		internal bool m_HasTranslationDoF;

		internal bool m_HasExtraRoot;

		internal bool m_SkeletonHasParents;

		public float upperArmTwist
		{
			get
			{
				return m_ArmTwist;
			}
			set
			{
				m_ArmTwist = value;
			}
		}

		public float lowerArmTwist
		{
			get
			{
				return m_ForeArmTwist;
			}
			set
			{
				m_ForeArmTwist = value;
			}
		}

		public float upperLegTwist
		{
			get
			{
				return m_UpperLegTwist;
			}
			set
			{
				m_UpperLegTwist = value;
			}
		}

		public float lowerLegTwist
		{
			get
			{
				return m_LegTwist;
			}
			set
			{
				m_LegTwist = value;
			}
		}

		public float armStretch
		{
			get
			{
				return m_ArmStretch;
			}
			set
			{
				m_ArmStretch = value;
			}
		}

		public float legStretch
		{
			get
			{
				return m_LegStretch;
			}
			set
			{
				m_LegStretch = value;
			}
		}

		public float feetSpacing
		{
			get
			{
				return m_FeetSpacing;
			}
			set
			{
				m_FeetSpacing = value;
			}
		}

		public bool hasTranslationDoF
		{
			get
			{
				return m_HasTranslationDoF;
			}
			set
			{
				m_HasTranslationDoF = value;
			}
		}
	}
	[NativeHeader("Modules/Animation/ScriptBindings/AvatarBuilder.bindings.h")]
	public class AvatarBuilder
	{
		public static Avatar BuildHumanAvatar(GameObject go, HumanDescription humanDescription)
		{
			if (go == null)
			{
				throw new NullReferenceException();
			}
			return BuildHumanAvatarInternal(go, humanDescription);
		}

		[FreeFunction("AvatarBuilderBindings::BuildHumanAvatar")]
		private static Avatar BuildHumanAvatarInternal(GameObject go, HumanDescription humanDescription)
		{
			return BuildHumanAvatarInternal_Injected(go, ref humanDescription);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("AvatarBuilderBindings::BuildGenericAvatar")]
		public static extern Avatar BuildGenericAvatar([NotNull("ArgumentNullException")] GameObject go, [NotNull("ArgumentNullException")] string rootMotionTransformName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Avatar BuildHumanAvatarInternal_Injected(GameObject go, ref HumanDescription humanDescription);
	}
	[MovedFrom(true, "UnityEditor.Animations", "UnityEditor", null)]
	public enum AvatarMaskBodyPart
	{
		Root,
		Body,
		Head,
		LeftLeg,
		RightLeg,
		LeftArm,
		RightArm,
		LeftFingers,
		RightFingers,
		LeftFootIK,
		RightFootIK,
		LeftHandIK,
		RightHandIK,
		LastBodyPart
	}
	[NativeHeader("Modules/Animation/AvatarMask.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/Animation.bindings.h")]
	[MovedFrom(true, "UnityEditor.Animations", "UnityEditor", null)]
	[UsedByNativeCode]
	public sealed class AvatarMask : Object
	{
		[Obsolete("AvatarMask.humanoidBodyPartCount is deprecated, use AvatarMaskBodyPart.LastBodyPart instead.")]
		public int humanoidBodyPartCount => 13;

		public extern int transformCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal extern bool hasFeetIK
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public AvatarMask()
		{
			Internal_Create(this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("AnimationBindings::CreateAvatarMask")]
		private static extern void Internal_Create([Writable] AvatarMask self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetBodyPart")]
		public extern bool GetHumanoidBodyPartActive(AvatarMaskBodyPart index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("SetBodyPart")]
		public extern void SetHumanoidBodyPartActive(AvatarMaskBodyPart index, bool value);

		public void AddTransformPath(Transform transform)
		{
			AddTransformPath(transform, recursive: true);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddTransformPath([NotNull("ArgumentNullException")] Transform transform, [UnityEngine.Internal.DefaultValue("true")] bool recursive);

		public void RemoveTransformPath(Transform transform)
		{
			RemoveTransformPath(transform, recursive: true);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RemoveTransformPath([NotNull("ArgumentNullException")] Transform transform, [UnityEngine.Internal.DefaultValue("true")] bool recursive);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetTransformPath(int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTransformPath(int index, string path);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetTransformWeight(int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTransformWeight(int index, float weight);

		public bool GetTransformActive(int index)
		{
			return GetTransformWeight(index) > 0.5f;
		}

		public void SetTransformActive(int index, bool value)
		{
			SetTransformWeight(index, value ? 1f : 0f);
		}

		internal void Copy(AvatarMask other)
		{
			for (AvatarMaskBodyPart avatarMaskBodyPart = AvatarMaskBodyPart.Root; avatarMaskBodyPart < AvatarMaskBodyPart.LastBodyPart; avatarMaskBodyPart++)
			{
				SetHumanoidBodyPartActive(avatarMaskBodyPart, other.GetHumanoidBodyPartActive(avatarMaskBodyPart));
			}
			transformCount = other.transformCount;
			for (int i = 0; i < other.transformCount; i++)
			{
				SetTransformPath(i, other.GetTransformPath(i));
				SetTransformActive(i, other.GetTransformActive(i));
			}
		}
	}
	public struct HumanPose
	{
		public Vector3 bodyPosition;

		public Quaternion bodyRotation;

		public float[] muscles;

		internal void Init()
		{
			if (muscles != null && muscles.Length != HumanTrait.MuscleCount)
			{
				throw new InvalidOperationException("Bad array size for HumanPose.muscles. Size must equal HumanTrait.MuscleCount");
			}
			if (muscles == null)
			{
				muscles = new float[HumanTrait.MuscleCount];
				if (bodyRotation.x == 0f && bodyRotation.y == 0f && bodyRotation.z == 0f && bodyRotation.w == 0f)
				{
					bodyRotation.w = 1f;
				}
			}
		}
	}
	[NativeHeader("Modules/Animation/HumanPoseHandler.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/Animation.bindings.h")]
	public class HumanPoseHandler : IDisposable
	{
		internal IntPtr m_Ptr;

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("AnimationBindings::CreateHumanPoseHandler")]
		private static extern IntPtr Internal_CreateFromRoot(Avatar avatar, Transform root);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("AnimationBindings::CreateHumanPoseHandler", IsThreadSafe = true)]
		private static extern IntPtr Internal_CreateFromJointPaths(Avatar avatar, string[] jointPaths);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("AnimationBindings::DestroyHumanPoseHandler")]
		private static extern void Internal_Destroy(IntPtr ptr);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetHumanPose(out Vector3 bodyPosition, out Quaternion bodyRotation, [Out] float[] muscles);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetHumanPose(ref Vector3 bodyPosition, ref Quaternion bodyRotation, float[] muscles);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		private extern void GetInternalHumanPose(out Vector3 bodyPosition, out Quaternion bodyRotation, [Out] float[] muscles);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		private extern void SetInternalHumanPose(ref Vector3 bodyPosition, ref Quaternion bodyRotation, float[] muscles);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		private unsafe extern void GetInternalAvatarPose(void* avatarPose, int avatarPoseLength);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		private unsafe extern void SetInternalAvatarPose(void* avatarPose, int avatarPoseLength);

		public void Dispose()
		{
			if (m_Ptr != IntPtr.Zero)
			{
				Internal_Destroy(m_Ptr);
				m_Ptr = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		public HumanPoseHandler(Avatar avatar, Transform root)
		{
			m_Ptr = IntPtr.Zero;
			if (root == null)
			{
				throw new ArgumentNullException("HumanPoseHandler root Transform is null");
			}
			if (avatar == null)
			{
				throw new ArgumentNullException("HumanPoseHandler avatar is null");
			}
			if (!avatar.isValid)
			{
				throw new ArgumentException("HumanPoseHandler avatar is invalid");
			}
			if (!avatar.isHuman)
			{
				throw new ArgumentException("HumanPoseHandler avatar is not human");
			}
			m_Ptr = Internal_CreateFromRoot(avatar, root);
		}

		public HumanPoseHandler(Avatar avatar, string[] jointPaths)
		{
			m_Ptr = IntPtr.Zero;
			if (jointPaths == null)
			{
				throw new ArgumentNullException("HumanPoseHandler jointPaths array is null");
			}
			if (avatar == null)
			{
				throw new ArgumentNullException("HumanPoseHandler avatar is null");
			}
			if (!avatar.isValid)
			{
				throw new ArgumentException("HumanPoseHandler avatar is invalid");
			}
			if (!avatar.isHuman)
			{
				throw new ArgumentException("HumanPoseHandler avatar is not human");
			}
			m_Ptr = Internal_CreateFromJointPaths(avatar, jointPaths);
		}

		public void GetHumanPose(ref HumanPose humanPose)
		{
			if (m_Ptr == IntPtr.Zero)
			{
				throw new NullReferenceException("HumanPoseHandler is not initialized properly");
			}
			humanPose.Init();
			GetHumanPose(out humanPose.bodyPosition, out humanPose.bodyRotation, humanPose.muscles);
		}

		public void SetHumanPose(ref HumanPose humanPose)
		{
			if (m_Ptr == IntPtr.Zero)
			{
				throw new NullReferenceException("HumanPoseHandler is not initialized properly");
			}
			humanPose.Init();
			SetHumanPose(ref humanPose.bodyPosition, ref humanPose.bodyRotation, humanPose.muscles);
		}

		public void GetInternalHumanPose(ref HumanPose humanPose)
		{
			if (m_Ptr == IntPtr.Zero)
			{
				throw new NullReferenceException("HumanPoseHandler is not initialized properly");
			}
			humanPose.Init();
			GetInternalHumanPose(out humanPose.bodyPosition, out humanPose.bodyRotation, humanPose.muscles);
		}

		public void SetInternalHumanPose(ref HumanPose humanPose)
		{
			if (m_Ptr == IntPtr.Zero)
			{
				throw new NullReferenceException("HumanPoseHandler is not initialized properly");
			}
			humanPose.Init();
			SetInternalHumanPose(ref humanPose.bodyPosition, ref humanPose.bodyRotation, humanPose.muscles);
		}

		public unsafe void GetInternalAvatarPose(NativeArray<float> avatarPose)
		{
			if (m_Ptr == IntPtr.Zero)
			{
				throw new NullReferenceException("HumanPoseHandler is not initialized properly");
			}
			GetInternalAvatarPose(avatarPose.GetUnsafePtr(), avatarPose.Length);
		}

		public unsafe void SetInternalAvatarPose(NativeArray<float> avatarPose)
		{
			if (m_Ptr == IntPtr.Zero)
			{
				throw new NullReferenceException("HumanPoseHandler is not initialized properly");
			}
			SetInternalAvatarPose(avatarPose.GetUnsafeReadOnlyPtr(), avatarPose.Length);
		}
	}
	[NativeHeader("Modules/Animation/HumanTrait.h")]
	public class HumanTrait
	{
		public static extern int MuscleCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string[] MuscleName
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("GetMuscleNames")]
			get;
		}

		public static extern int BoneCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern string[] BoneName
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("MonoBoneNames")]
			get;
		}

		public static extern int RequiredBoneCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("RequiredBoneCount")]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetBoneIndexFromMono(int humanId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetBoneIndexToMono(int boneIndex);

		public static int MuscleFromBone(int i, int dofIndex)
		{
			return Internal_MuscleFromBone(GetBoneIndexFromMono(i), dofIndex);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("MuscleFromBone")]
		private static extern int Internal_MuscleFromBone(int i, int dofIndex);

		public static int BoneFromMuscle(int i)
		{
			return GetBoneIndexToMono(Internal_BoneFromMuscle(i));
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("BoneFromMuscle")]
		private static extern int Internal_BoneFromMuscle(int i);

		public static bool RequiredBone(int i)
		{
			return Internal_RequiredBone(GetBoneIndexFromMono(i));
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("RequiredBone")]
		private static extern bool Internal_RequiredBone(int i);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetMuscleDefaultMin(int i);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetMuscleDefaultMax(int i);

		public static float GetBoneDefaultHierarchyMass(int i)
		{
			return Internal_GetBoneHierarchyMass(GetBoneIndexFromMono(i));
		}

		public static int GetParentBone(int i)
		{
			int num = Internal_GetParent(GetBoneIndexFromMono(i));
			return (num != -1) ? GetBoneIndexToMono(num) : (-1);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetBoneHierarchyMass")]
		private static extern float Internal_GetBoneHierarchyMass(int i);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetParent")]
		private static extern int Internal_GetParent(int i);
	}
	[NativeHeader("Modules/Animation/Motion.h")]
	public class Motion : Object
	{
		public extern float averageDuration
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float averageAngularSpeed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public Vector3 averageSpeed
		{
			get
			{
				get_averageSpeed_Injected(out var ret);
				return ret;
			}
		}

		public extern float apparentSpeed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool isLooping
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("IsLooping")]
			get;
		}

		public extern bool legacy
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("IsLegacy")]
			get;
		}

		public extern bool isHumanMotion
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod("IsHumanMotion")]
			get;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("isAnimatorMotion is not supported anymore, please use !legacy instead.", true)]
		public bool isAnimatorMotion { get; }

		protected Motion()
		{
		}

		[Obsolete("ValidateIfRetargetable is not supported anymore, please use isHumanMotion instead.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ValidateIfRetargetable(bool val)
		{
			return false;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_averageSpeed_Injected(out Vector3 ret);
	}
	[NativeHeader("Modules/Animation/RuntimeAnimatorController.h")]
	[ExcludeFromObjectFactory]
	[UsedByNativeCode]
	public class RuntimeAnimatorController : Object
	{
		public extern AnimationClip[] animationClips
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		protected RuntimeAnimatorController()
		{
		}
	}
}
namespace UnityEngine.Experimental.Animations
{
	public enum AnimationStreamSource
	{
		DefaultValues,
		PreviousInputs
	}
	[StaticAccessor("AnimationPlayableOutputExtensionsBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Animation/AnimatorDefines.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationPlayableOutputExtensions.bindings.h")]
	public static class AnimationPlayableOutputExtensions
	{
		public static AnimationStreamSource GetAnimationStreamSource(this AnimationPlayableOutput output)
		{
			return InternalGetAnimationStreamSource(output.GetHandle());
		}

		public static void SetAnimationStreamSource(this AnimationPlayableOutput output, AnimationStreamSource streamSource)
		{
			InternalSetAnimationStreamSource(output.GetHandle(), streamSource);
		}

		public static ushort GetSortingOrder(this AnimationPlayableOutput output)
		{
			return (ushort)InternalGetSortingOrder(output.GetHandle());
		}

		public static void SetSortingOrder(this AnimationPlayableOutput output, ushort sortingOrder)
		{
			InternalSetSortingOrder(output.GetHandle(), sortingOrder);
		}

		[NativeThrows]
		private static AnimationStreamSource InternalGetAnimationStreamSource(PlayableOutputHandle output)
		{
			return InternalGetAnimationStreamSource_Injected(ref output);
		}

		[NativeThrows]
		private static void InternalSetAnimationStreamSource(PlayableOutputHandle output, AnimationStreamSource streamSource)
		{
			InternalSetAnimationStreamSource_Injected(ref output, streamSource);
		}

		[NativeThrows]
		private static int InternalGetSortingOrder(PlayableOutputHandle output)
		{
			return InternalGetSortingOrder_Injected(ref output);
		}

		[NativeThrows]
		private static void InternalSetSortingOrder(PlayableOutputHandle output, int sortingOrder)
		{
			InternalSetSortingOrder_Injected(ref output, sortingOrder);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimationStreamSource InternalGetAnimationStreamSource_Injected(ref PlayableOutputHandle output);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetAnimationStreamSource_Injected(ref PlayableOutputHandle output, AnimationStreamSource streamSource);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int InternalGetSortingOrder_Injected(ref PlayableOutputHandle output);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetSortingOrder_Injected(ref PlayableOutputHandle output, int sortingOrder);
	}
}
namespace UnityEngine.Playables
{
	public static class AnimationPlayableUtilities
	{
		[Obsolete("This function is no longer used as it overrides the Time Update Mode of the Playable Graph. Refer to the documentation for an example of an equivalent function.")]
		public static void Play(Animator animator, Playable playable, PlayableGraph graph)
		{
			AnimationPlayableOutput output = AnimationPlayableOutput.Create(graph, "AnimationClip", animator);
			output.SetSourcePlayable(playable, 0);
			graph.SyncUpdateAndTimeMode(animator);
			graph.Play();
		}

		public static AnimationClipPlayable PlayClip(Animator animator, AnimationClip clip, out PlayableGraph graph)
		{
			graph = PlayableGraph.Create();
			AnimationPlayableOutput output = AnimationPlayableOutput.Create(graph, "AnimationClip", animator);
			AnimationClipPlayable animationClipPlayable = AnimationClipPlayable.Create(graph, clip);
			output.SetSourcePlayable(animationClipPlayable);
			graph.SyncUpdateAndTimeMode(animator);
			graph.Play();
			return animationClipPlayable;
		}

		public static AnimationMixerPlayable PlayMixer(Animator animator, int inputCount, out PlayableGraph graph)
		{
			graph = PlayableGraph.Create();
			AnimationPlayableOutput output = AnimationPlayableOutput.Create(graph, "Mixer", animator);
			AnimationMixerPlayable animationMixerPlayable = AnimationMixerPlayable.Create(graph, inputCount);
			output.SetSourcePlayable(animationMixerPlayable);
			graph.SyncUpdateAndTimeMode(animator);
			graph.Play();
			return animationMixerPlayable;
		}

		public static AnimationLayerMixerPlayable PlayLayerMixer(Animator animator, int inputCount, out PlayableGraph graph)
		{
			graph = PlayableGraph.Create();
			AnimationPlayableOutput output = AnimationPlayableOutput.Create(graph, "Mixer", animator);
			AnimationLayerMixerPlayable animationLayerMixerPlayable = AnimationLayerMixerPlayable.Create(graph, inputCount);
			output.SetSourcePlayable(animationLayerMixerPlayable);
			graph.SyncUpdateAndTimeMode(animator);
			graph.Play();
			return animationLayerMixerPlayable;
		}

		public static AnimatorControllerPlayable PlayAnimatorController(Animator animator, RuntimeAnimatorController controller, out PlayableGraph graph)
		{
			graph = PlayableGraph.Create();
			AnimationPlayableOutput output = AnimationPlayableOutput.Create(graph, "AnimatorControllerPlayable", animator);
			AnimatorControllerPlayable animatorControllerPlayable = AnimatorControllerPlayable.Create(graph, controller);
			output.SetSourcePlayable(animatorControllerPlayable);
			graph.SyncUpdateAndTimeMode(animator);
			graph.Play();
			return animatorControllerPlayable;
		}
	}
}
namespace UnityEngine.Animations
{
	public static class AnimationPlayableBinding
	{
		public static PlayableBinding Create(string name, Object key)
		{
			return PlayableBinding.CreateInternal(name, key, typeof(Animator), CreateAnimationOutput);
		}

		private static PlayableOutput CreateAnimationOutput(PlayableGraph graph, string name)
		{
			return AnimationPlayableOutput.Create(graph, name, null);
		}
	}
	[AttributeUsage(AttributeTargets.Field)]
	[RequiredByNativeCode]
	public class DiscreteEvaluationAttribute : Attribute
	{
	}
	internal static class DiscreteEvaluationAttributeUtilities
	{
		public unsafe static int ConvertFloatToDiscreteInt(float f)
		{
			float* ptr = &f;
			int* ptr2 = (int*)ptr;
			return *ptr2;
		}

		public unsafe static float ConvertDiscreteIntToFloat(int f)
		{
			int* ptr = &f;
			float* ptr2 = (float*)ptr;
			return *ptr2;
		}
	}
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[JobProducerType(typeof(ProcessAnimationJobStruct<>))]
	public interface IAnimationJob
	{
		void ProcessAnimation(AnimationStream stream);

		void ProcessRootMotion(AnimationStream stream);
	}
	[MovedFrom("UnityEngine.Experimental.Animations")]
	public interface IAnimationJobPlayable : IPlayable
	{
		T GetJobData<T>() where T : struct, IAnimationJob;

		void SetJobData<T>(T jobData) where T : struct, IAnimationJob;
	}
	[UsedByNativeCode]
	internal interface IAnimationPreviewable
	{
		void OnPreviewUpdate();
	}
	[MovedFrom("UnityEngine.Experimental.Animations")]
	public interface IAnimationWindowPreview
	{
		void StartPreview();

		void StopPreview();

		void UpdatePreviewGraph(PlayableGraph graph);

		Playable BuildPreviewGraph(PlayableGraph graph, Playable inputPlayable);
	}
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field)]
	[RequiredByNativeCode]
	public class NotKeyableAttribute : Attribute
	{
	}
	internal enum JobMethodIndex
	{
		ProcessRootMotionMethodIndex,
		ProcessAnimationMethodIndex,
		MethodIndexCount
	}
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct ProcessAnimationJobStruct<T> where T : struct, IAnimationJob
	{
		public delegate void ExecuteJobFunction(ref T data, IntPtr animationStreamPtr, IntPtr unusedPtr, ref JobRanges ranges, int jobIndex);

		private static IntPtr jobReflectionData;

		public static IntPtr GetJobReflectionData()
		{
			if (jobReflectionData == IntPtr.Zero)
			{
				jobReflectionData = JobsUtility.CreateJobReflectionData(typeof(T), new ExecuteJobFunction(Execute));
			}
			return jobReflectionData;
		}

		public unsafe static void Execute(ref T data, IntPtr animationStreamPtr, IntPtr methodIndex, ref JobRanges ranges, int jobIndex)
		{
			UnsafeUtility.CopyPtrToStructure<AnimationStream>((void*)animationStreamPtr, out var output);
			switch ((JobMethodIndex)methodIndex.ToInt32())
			{
			case JobMethodIndex.ProcessRootMotionMethodIndex:
				data.ProcessRootMotion(output);
				break;
			case JobMethodIndex.ProcessAnimationMethodIndex:
				data.ProcessAnimation(output);
				break;
			default:
				throw new NotImplementedException("Invalid Animation jobs method index.");
			}
		}
	}
	[UsedByNativeCode]
	[NativeHeader("Modules/Animation/Constraints/AimConstraint.h")]
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	[RequireComponent(typeof(Transform))]
	public sealed class AimConstraint : Behaviour, IConstraint, IConstraintInternal
	{
		public enum WorldUpType
		{
			SceneUp,
			ObjectUp,
			ObjectRotationUp,
			Vector,
			None
		}

		public extern float weight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool constraintActive
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool locked
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 rotationAtRest
		{
			get
			{
				get_rotationAtRest_Injected(out var ret);
				return ret;
			}
			set
			{
				set_rotationAtRest_Injected(ref value);
			}
		}

		public Vector3 rotationOffset
		{
			get
			{
				get_rotationOffset_Injected(out var ret);
				return ret;
			}
			set
			{
				set_rotationOffset_Injected(ref value);
			}
		}

		public extern Axis rotationAxis
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 aimVector
		{
			get
			{
				get_aimVector_Injected(out var ret);
				return ret;
			}
			set
			{
				set_aimVector_Injected(ref value);
			}
		}

		public Vector3 upVector
		{
			get
			{
				get_upVector_Injected(out var ret);
				return ret;
			}
			set
			{
				set_upVector_Injected(ref value);
			}
		}

		public Vector3 worldUpVector
		{
			get
			{
				get_worldUpVector_Injected(out var ret);
				return ret;
			}
			set
			{
				set_worldUpVector_Injected(ref value);
			}
		}

		public extern Transform worldUpObject
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern WorldUpType worldUpType
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public int sourceCount => GetSourceCountInternal(this);

		private AimConstraint()
		{
			Internal_Create(this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] AimConstraint self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ConstraintBindings::GetSourceCount")]
		private static extern int GetSourceCountInternal([NotNull("ArgumentNullException")] AimConstraint self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ConstraintBindings::GetSources", HasExplicitThis = true)]
		public extern void GetSources([NotNull("ArgumentNullException")] List<ConstraintSource> sources);

		public void SetSources(List<ConstraintSource> sources)
		{
			if (sources == null)
			{
				throw new ArgumentNullException("sources");
			}
			SetSourcesInternal(this, sources);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ConstraintBindings::SetSources", ThrowsException = true)]
		private static extern void SetSourcesInternal([NotNull("ArgumentNullException")] AimConstraint self, List<ConstraintSource> sources);

		public int AddSource(ConstraintSource source)
		{
			return AddSource_Injected(ref source);
		}

		public void RemoveSource(int index)
		{
			ValidateSourceIndex(index);
			RemoveSourceInternal(index);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("RemoveSource")]
		private extern void RemoveSourceInternal(int index);

		public ConstraintSource GetSource(int index)
		{
			ValidateSourceIndex(index);
			return GetSourceInternal(index);
		}

		[NativeName("GetSource")]
		private ConstraintSource GetSourceInternal(int index)
		{
			GetSourceInternal_Injected(index, out var ret);
			return ret;
		}

		public void SetSource(int index, ConstraintSource source)
		{
			ValidateSourceIndex(index);
			SetSourceInternal(index, source);
		}

		[NativeName("SetSource")]
		private void SetSourceInternal(int index, ConstraintSource source)
		{
			SetSourceInternal_Injected(index, ref source);
		}

		private void ValidateSourceIndex(int index)
		{
			if (sourceCount == 0)
			{
				throw new InvalidOperationException("The AimConstraint component has no sources.");
			}
			if (index < 0 || index >= sourceCount)
			{
				throw new ArgumentOutOfRangeException("index", $"Constraint source index {index} is out of bounds (0-{sourceCount}).");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_rotationAtRest_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_rotationAtRest_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_rotationOffset_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_rotationOffset_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_aimVector_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_aimVector_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_upVector_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_upVector_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_worldUpVector_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_worldUpVector_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int AddSource_Injected(ref ConstraintSource source);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetSourceInternal_Injected(int index, out ConstraintSource ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetSourceInternal_Injected(int index, ref ConstraintSource source);
	}
	[StaticAccessor("AnimationClipPlayableBindings", StaticAccessorType.DoubleColon)]
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/Director/AnimationClipPlayable.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationClipPlayable.bindings.h")]
	public struct AnimationClipPlayable : IPlayable, IEquatable<AnimationClipPlayable>
	{
		private PlayableHandle m_Handle;

		public static AnimationClipPlayable Create(PlayableGraph graph, AnimationClip clip)
		{
			PlayableHandle handle = CreateHandle(graph, clip);
			return new AnimationClipPlayable(handle);
		}

		private static PlayableHandle CreateHandle(PlayableGraph graph, AnimationClip clip)
		{
			PlayableHandle handle = PlayableHandle.Null;
			if (!CreateHandleInternal(graph, clip, ref handle))
			{
				return PlayableHandle.Null;
			}
			return handle;
		}

		internal AnimationClipPlayable(PlayableHandle handle)
		{
			if (handle.IsValid() && !handle.IsPlayableOfType<AnimationClipPlayable>())
			{
				throw new InvalidCastException("Can't set handle: the playable is not an AnimationClipPlayable.");
			}
			m_Handle = handle;
		}

		public PlayableHandle GetHandle()
		{
			return m_Handle;
		}

		public static implicit operator Playable(AnimationClipPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		public static explicit operator AnimationClipPlayable(Playable playable)
		{
			return new AnimationClipPlayable(playable.GetHandle());
		}

		public bool Equals(AnimationClipPlayable other)
		{
			return GetHandle() == other.GetHandle();
		}

		public AnimationClip GetAnimationClip()
		{
			return GetAnimationClipInternal(ref m_Handle);
		}

		public bool GetApplyFootIK()
		{
			return GetApplyFootIKInternal(ref m_Handle);
		}

		public void SetApplyFootIK(bool value)
		{
			SetApplyFootIKInternal(ref m_Handle, value);
		}

		public bool GetApplyPlayableIK()
		{
			return GetApplyPlayableIKInternal(ref m_Handle);
		}

		public void SetApplyPlayableIK(bool value)
		{
			SetApplyPlayableIKInternal(ref m_Handle, value);
		}

		internal bool GetRemoveStartOffset()
		{
			return GetRemoveStartOffsetInternal(ref m_Handle);
		}

		internal void SetRemoveStartOffset(bool value)
		{
			SetRemoveStartOffsetInternal(ref m_Handle, value);
		}

		internal bool GetOverrideLoopTime()
		{
			return GetOverrideLoopTimeInternal(ref m_Handle);
		}

		internal void SetOverrideLoopTime(bool value)
		{
			SetOverrideLoopTimeInternal(ref m_Handle, value);
		}

		internal bool GetLoopTime()
		{
			return GetLoopTimeInternal(ref m_Handle);
		}

		internal void SetLoopTime(bool value)
		{
			SetLoopTimeInternal(ref m_Handle, value);
		}

		internal float GetSampleRate()
		{
			return GetSampleRateInternal(ref m_Handle);
		}

		internal void SetSampleRate(float value)
		{
			SetSampleRateInternal(ref m_Handle, value);
		}

		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, AnimationClip clip, ref PlayableHandle handle)
		{
			return CreateHandleInternal_Injected(ref graph, clip, ref handle);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern AnimationClip GetAnimationClipInternal(ref PlayableHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool GetApplyFootIKInternal(ref PlayableHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetApplyFootIKInternal(ref PlayableHandle handle, bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool GetApplyPlayableIKInternal(ref PlayableHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetApplyPlayableIKInternal(ref PlayableHandle handle, bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool GetRemoveStartOffsetInternal(ref PlayableHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetRemoveStartOffsetInternal(ref PlayableHandle handle, bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool GetOverrideLoopTimeInternal(ref PlayableHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetOverrideLoopTimeInternal(ref PlayableHandle handle, bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool GetLoopTimeInternal(ref PlayableHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetLoopTimeInternal(ref PlayableHandle handle, bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern float GetSampleRateInternal(ref PlayableHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetSampleRateInternal(ref PlayableHandle handle, float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, AnimationClip clip, ref PlayableHandle handle);
	}
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationHumanStream.bindings.h")]
	[NativeHeader("Modules/Animation/Director/AnimationHumanStream.h")]
	[RequiredByNativeCode]
	public struct AnimationHumanStream
	{
		private IntPtr stream;

		public bool isValid => stream != IntPtr.Zero;

		public float humanScale
		{
			get
			{
				ThrowIfInvalid();
				return GetHumanScale();
			}
		}

		public float leftFootHeight
		{
			get
			{
				ThrowIfInvalid();
				return GetFootHeight(left: true);
			}
		}

		public float rightFootHeight
		{
			get
			{
				ThrowIfInvalid();
				return GetFootHeight(left: false);
			}
		}

		public Vector3 bodyLocalPosition
		{
			get
			{
				ThrowIfInvalid();
				return InternalGetBodyLocalPosition();
			}
			set
			{
				ThrowIfInvalid();
				InternalSetBodyLocalPosition(value);
			}
		}

		public Quaternion bodyLocalRotation
		{
			get
			{
				ThrowIfInvalid();
				return InternalGetBodyLocalRotation();
			}
			set
			{
				ThrowIfInvalid();
				InternalSetBodyLocalRotation(value);
			}
		}

		public Vector3 bodyPosition
		{
			get
			{
				ThrowIfInvalid();
				return InternalGetBodyPosition();
			}
			set
			{
				ThrowIfInvalid();
				InternalSetBodyPosition(value);
			}
		}

		public Quaternion bodyRotation
		{
			get
			{
				ThrowIfInvalid();
				return InternalGetBodyRotation();
			}
			set
			{
				ThrowIfInvalid();
				InternalSetBodyRotation(value);
			}
		}

		public Vector3 leftFootVelocity
		{
			get
			{
				ThrowIfInvalid();
				return GetLeftFootVelocity();
			}
		}

		public Vector3 rightFootVelocity
		{
			get
			{
				ThrowIfInvalid();
				return GetRightFootVelocity();
			}
		}

		private void ThrowIfInvalid()
		{
			if (!isValid)
			{
				throw new InvalidOperationException("The AnimationHumanStream is invalid.");
			}
		}

		public float GetMuscle(MuscleHandle muscle)
		{
			ThrowIfInvalid();
			return InternalGetMuscle(muscle);
		}

		public void SetMuscle(MuscleHandle muscle, float value)
		{
			ThrowIfInvalid();
			InternalSetMuscle(muscle, value);
		}

		public void ResetToStancePose()
		{
			ThrowIfInvalid();
			InternalResetToStancePose();
		}

		public Vector3 GetGoalPositionFromPose(AvatarIKGoal index)
		{
			ThrowIfInvalid();
			return InternalGetGoalPositionFromPose(index);
		}

		public Quaternion GetGoalRotationFromPose(AvatarIKGoal index)
		{
			ThrowIfInvalid();
			return InternalGetGoalRotationFromPose(index);
		}

		public Vector3 GetGoalLocalPosition(AvatarIKGoal index)
		{
			ThrowIfInvalid();
			return InternalGetGoalLocalPosition(index);
		}

		public void SetGoalLocalPosition(AvatarIKGoal index, Vector3 pos)
		{
			ThrowIfInvalid();
			InternalSetGoalLocalPosition(index, pos);
		}

		public Quaternion GetGoalLocalRotation(AvatarIKGoal index)
		{
			ThrowIfInvalid();
			return InternalGetGoalLocalRotation(index);
		}

		public void SetGoalLocalRotation(AvatarIKGoal index, Quaternion rot)
		{
			ThrowIfInvalid();
			InternalSetGoalLocalRotation(index, rot);
		}

		public Vector3 GetGoalPosition(AvatarIKGoal index)
		{
			ThrowIfInvalid();
			return InternalGetGoalPosition(index);
		}

		public void SetGoalPosition(AvatarIKGoal index, Vector3 pos)
		{
			ThrowIfInvalid();
			InternalSetGoalPosition(index, pos);
		}

		public Quaternion GetGoalRotation(AvatarIKGoal index)
		{
			ThrowIfInvalid();
			return InternalGetGoalRotation(index);
		}

		public void SetGoalRotation(AvatarIKGoal index, Quaternion rot)
		{
			ThrowIfInvalid();
			InternalSetGoalRotation(index, rot);
		}

		public void SetGoalWeightPosition(AvatarIKGoal index, float value)
		{
			ThrowIfInvalid();
			InternalSetGoalWeightPosition(index, value);
		}

		public void SetGoalWeightRotation(AvatarIKGoal index, float value)
		{
			ThrowIfInvalid();
			InternalSetGoalWeightRotation(index, value);
		}

		public float GetGoalWeightPosition(AvatarIKGoal index)
		{
			ThrowIfInvalid();
			return InternalGetGoalWeightPosition(index);
		}

		public float GetGoalWeightRotation(AvatarIKGoal index)
		{
			ThrowIfInvalid();
			return InternalGetGoalWeightRotation(index);
		}

		public Vector3 GetHintPosition(AvatarIKHint index)
		{
			ThrowIfInvalid();
			return InternalGetHintPosition(index);
		}

		public void SetHintPosition(AvatarIKHint index, Vector3 pos)
		{
			ThrowIfInvalid();
			InternalSetHintPosition(index, pos);
		}

		public void SetHintWeightPosition(AvatarIKHint index, float value)
		{
			ThrowIfInvalid();
			InternalSetHintWeightPosition(index, value);
		}

		public float GetHintWeightPosition(AvatarIKHint index)
		{
			ThrowIfInvalid();
			return InternalGetHintWeightPosition(index);
		}

		public void SetLookAtPosition(Vector3 lookAtPosition)
		{
			ThrowIfInvalid();
			InternalSetLookAtPosition(lookAtPosition);
		}

		public void SetLookAtClampWeight(float weight)
		{
			ThrowIfInvalid();
			InternalSetLookAtClampWeight(weight);
		}

		public void SetLookAtBodyWeight(float weight)
		{
			ThrowIfInvalid();
			InternalSetLookAtBodyWeight(weight);
		}

		public void SetLookAtHeadWeight(float weight)
		{
			ThrowIfInvalid();
			InternalSetLookAtHeadWeight(weight);
		}

		public void SetLookAtEyesWeight(float weight)
		{
			ThrowIfInvalid();
			InternalSetLookAtEyesWeight(weight);
		}

		public void SolveIK()
		{
			ThrowIfInvalid();
			InternalSolveIK();
		}

		[NativeMethod(IsThreadSafe = true)]
		private float GetHumanScale()
		{
			return GetHumanScale_Injected(ref this);
		}

		[NativeMethod(IsThreadSafe = true)]
		private float GetFootHeight(bool left)
		{
			return GetFootHeight_Injected(ref this, left);
		}

		[NativeMethod(Name = "ResetToStancePose", IsThreadSafe = true)]
		private void InternalResetToStancePose()
		{
			InternalResetToStancePose_Injected(ref this);
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::GetGoalPositionFromPose", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 InternalGetGoalPositionFromPose(AvatarIKGoal index)
		{
			InternalGetGoalPositionFromPose_Injected(ref this, index, out var ret);
			return ret;
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::GetGoalRotationFromPose", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion InternalGetGoalRotationFromPose(AvatarIKGoal index)
		{
			InternalGetGoalRotationFromPose_Injected(ref this, index, out var ret);
			return ret;
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::GetBodyLocalPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 InternalGetBodyLocalPosition()
		{
			InternalGetBodyLocalPosition_Injected(ref this, out var ret);
			return ret;
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::SetBodyLocalPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetBodyLocalPosition(Vector3 value)
		{
			InternalSetBodyLocalPosition_Injected(ref this, ref value);
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::GetBodyLocalRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion InternalGetBodyLocalRotation()
		{
			InternalGetBodyLocalRotation_Injected(ref this, out var ret);
			return ret;
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::SetBodyLocalRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetBodyLocalRotation(Quaternion value)
		{
			InternalSetBodyLocalRotation_Injected(ref this, ref value);
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::GetBodyPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 InternalGetBodyPosition()
		{
			InternalGetBodyPosition_Injected(ref this, out var ret);
			return ret;
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::SetBodyPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetBodyPosition(Vector3 value)
		{
			InternalSetBodyPosition_Injected(ref this, ref value);
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::GetBodyRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion InternalGetBodyRotation()
		{
			InternalGetBodyRotation_Injected(ref this, out var ret);
			return ret;
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::SetBodyRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetBodyRotation(Quaternion value)
		{
			InternalSetBodyRotation_Injected(ref this, ref value);
		}

		[NativeMethod(Name = "GetMuscle", IsThreadSafe = true)]
		private float InternalGetMuscle(MuscleHandle muscle)
		{
			return InternalGetMuscle_Injected(ref this, ref muscle);
		}

		[NativeMethod(Name = "SetMuscle", IsThreadSafe = true)]
		private void InternalSetMuscle(MuscleHandle muscle, float value)
		{
			InternalSetMuscle_Injected(ref this, ref muscle, value);
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::GetLeftFootVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetLeftFootVelocity()
		{
			GetLeftFootVelocity_Injected(ref this, out var ret);
			return ret;
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::GetRightFootVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetRightFootVelocity()
		{
			GetRightFootVelocity_Injected(ref this, out var ret);
			return ret;
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::GetGoalLocalPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 InternalGetGoalLocalPosition(AvatarIKGoal index)
		{
			InternalGetGoalLocalPosition_Injected(ref this, index, out var ret);
			return ret;
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::SetGoalLocalPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetGoalLocalPosition(AvatarIKGoal index, Vector3 pos)
		{
			InternalSetGoalLocalPosition_Injected(ref this, index, ref pos);
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::GetGoalLocalRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion InternalGetGoalLocalRotation(AvatarIKGoal index)
		{
			InternalGetGoalLocalRotation_Injected(ref this, index, out var ret);
			return ret;
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::SetGoalLocalRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetGoalLocalRotation(AvatarIKGoal index, Quaternion rot)
		{
			InternalSetGoalLocalRotation_Injected(ref this, index, ref rot);
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::GetGoalPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 InternalGetGoalPosition(AvatarIKGoal index)
		{
			InternalGetGoalPosition_Injected(ref this, index, out var ret);
			return ret;
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::SetGoalPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetGoalPosition(AvatarIKGoal index, Vector3 pos)
		{
			InternalSetGoalPosition_Injected(ref this, index, ref pos);
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::GetGoalRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion InternalGetGoalRotation(AvatarIKGoal index)
		{
			InternalGetGoalRotation_Injected(ref this, index, out var ret);
			return ret;
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::SetGoalRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetGoalRotation(AvatarIKGoal index, Quaternion rot)
		{
			InternalSetGoalRotation_Injected(ref this, index, ref rot);
		}

		[NativeMethod(Name = "SetGoalWeightPosition", IsThreadSafe = true)]
		private void InternalSetGoalWeightPosition(AvatarIKGoal index, float value)
		{
			InternalSetGoalWeightPosition_Injected(ref this, index, value);
		}

		[NativeMethod(Name = "SetGoalWeightRotation", IsThreadSafe = true)]
		private void InternalSetGoalWeightRotation(AvatarIKGoal index, float value)
		{
			InternalSetGoalWeightRotation_Injected(ref this, index, value);
		}

		[NativeMethod(Name = "GetGoalWeightPosition", IsThreadSafe = true)]
		private float InternalGetGoalWeightPosition(AvatarIKGoal index)
		{
			return InternalGetGoalWeightPosition_Injected(ref this, index);
		}

		[NativeMethod(Name = "GetGoalWeightRotation", IsThreadSafe = true)]
		private float InternalGetGoalWeightRotation(AvatarIKGoal index)
		{
			return InternalGetGoalWeightRotation_Injected(ref this, index);
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::GetHintPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 InternalGetHintPosition(AvatarIKHint index)
		{
			InternalGetHintPosition_Injected(ref this, index, out var ret);
			return ret;
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::SetHintPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetHintPosition(AvatarIKHint index, Vector3 pos)
		{
			InternalSetHintPosition_Injected(ref this, index, ref pos);
		}

		[NativeMethod(Name = "SetHintWeightPosition", IsThreadSafe = true)]
		private void InternalSetHintWeightPosition(AvatarIKHint index, float value)
		{
			InternalSetHintWeightPosition_Injected(ref this, index, value);
		}

		[NativeMethod(Name = "GetHintWeightPosition", IsThreadSafe = true)]
		private float InternalGetHintWeightPosition(AvatarIKHint index)
		{
			return InternalGetHintWeightPosition_Injected(ref this, index);
		}

		[NativeMethod(Name = "AnimationHumanStreamBindings::SetLookAtPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetLookAtPosition(Vector3 lookAtPosition)
		{
			InternalSetLookAtPosition_Injected(ref this, ref lookAtPosition);
		}

		[NativeMethod(Name = "SetLookAtClampWeight", IsThreadSafe = true)]
		private void InternalSetLookAtClampWeight(float weight)
		{
			InternalSetLookAtClampWeight_Injected(ref this, weight);
		}

		[NativeMethod(Name = "SetLookAtBodyWeight", IsThreadSafe = true)]
		private void InternalSetLookAtBodyWeight(float weight)
		{
			InternalSetLookAtBodyWeight_Injected(ref this, weight);
		}

		[NativeMethod(Name = "SetLookAtHeadWeight", IsThreadSafe = true)]
		private void InternalSetLookAtHeadWeight(float weight)
		{
			InternalSetLookAtHeadWeight_Injected(ref this, weight);
		}

		[NativeMethod(Name = "SetLookAtEyesWeight", IsThreadSafe = true)]
		private void InternalSetLookAtEyesWeight(float weight)
		{
			InternalSetLookAtEyesWeight_Injected(ref this, weight);
		}

		[NativeMethod(Name = "SolveIK", IsThreadSafe = true)]
		private void InternalSolveIK()
		{
			InternalSolveIK_Injected(ref this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetHumanScale_Injected(ref AnimationHumanStream _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetFootHeight_Injected(ref AnimationHumanStream _unity_self, bool left);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalResetToStancePose_Injected(ref AnimationHumanStream _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetGoalPositionFromPose_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetGoalRotationFromPose_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetBodyLocalPosition_Injected(ref AnimationHumanStream _unity_self, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetBodyLocalPosition_Injected(ref AnimationHumanStream _unity_self, ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetBodyLocalRotation_Injected(ref AnimationHumanStream _unity_self, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetBodyLocalRotation_Injected(ref AnimationHumanStream _unity_self, ref Quaternion value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetBodyPosition_Injected(ref AnimationHumanStream _unity_self, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetBodyPosition_Injected(ref AnimationHumanStream _unity_self, ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetBodyRotation_Injected(ref AnimationHumanStream _unity_self, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetBodyRotation_Injected(ref AnimationHumanStream _unity_self, ref Quaternion value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float InternalGetMuscle_Injected(ref AnimationHumanStream _unity_self, ref MuscleHandle muscle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetMuscle_Injected(ref AnimationHumanStream _unity_self, ref MuscleHandle muscle, float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLeftFootVelocity_Injected(ref AnimationHumanStream _unity_self, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRightFootVelocity_Injected(ref AnimationHumanStream _unity_self, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetGoalLocalPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetGoalLocalPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, ref Vector3 pos);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetGoalLocalRotation_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetGoalLocalRotation_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, ref Quaternion rot);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetGoalPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetGoalPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, ref Vector3 pos);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetGoalRotation_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetGoalRotation_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, ref Quaternion rot);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetGoalWeightPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetGoalWeightRotation_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float InternalGetGoalWeightPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float InternalGetGoalWeightRotation_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetHintPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKHint index, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetHintPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKHint index, ref Vector3 pos);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetHintWeightPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKHint index, float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float InternalGetHintWeightPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKHint index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetLookAtPosition_Injected(ref AnimationHumanStream _unity_self, ref Vector3 lookAtPosition);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetLookAtClampWeight_Injected(ref AnimationHumanStream _unity_self, float weight);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetLookAtBodyWeight_Injected(ref AnimationHumanStream _unity_self, float weight);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetLookAtHeadWeight_Injected(ref AnimationHumanStream _unity_self, float weight);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetLookAtEyesWeight_Injected(ref AnimationHumanStream _unity_self, float weight);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSolveIK_Injected(ref AnimationHumanStream _unity_self);
	}
	[RequiredByNativeCode]
	[StaticAccessor("AnimationLayerMixerPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Modules/Animation/Director/AnimationLayerMixerPlayable.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationLayerMixerPlayable.bindings.h")]
	public struct AnimationLayerMixerPlayable : IPlayable, IEquatable<AnimationLayerMixerPlayable>
	{
		private PlayableHandle m_Handle;

		private static readonly AnimationLayerMixerPlayable m_NullPlayable = new AnimationLayerMixerPlayable(PlayableHandle.Null);

		public static AnimationLayerMixerPlayable Null => m_NullPlayable;

		public static AnimationLayerMixerPlayable Create(PlayableGraph graph, int inputCount = 0)
		{
			return Create(graph, inputCount, singleLayerOptimization: true);
		}

		public static AnimationLayerMixerPlayable Create(PlayableGraph graph, int inputCount, bool singleLayerOptimization)
		{
			PlayableHandle handle = CreateHandle(graph, inputCount);
			return new AnimationLayerMixerPlayable(handle, singleLayerOptimization);
		}

		private static PlayableHandle CreateHandle(PlayableGraph graph, int inputCount = 0)
		{
			PlayableHandle handle = PlayableHandle.Null;
			if (!CreateHandleInternal(graph, ref handle))
			{
				return PlayableHandle.Null;
			}
			handle.SetInputCount(inputCount);
			return handle;
		}

		internal AnimationLayerMixerPlayable(PlayableHandle handle, bool singleLayerOptimization = true)
		{
			if (handle.IsValid())
			{
				if (!handle.IsPlayableOfType<AnimationLayerMixerPlayable>())
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationLayerMixerPlayable.");
				}
				SetSingleLayerOptimizationInternal(ref handle, singleLayerOptimization);
			}
			m_Handle = handle;
		}

		public PlayableHandle GetHandle()
		{
			return m_Handle;
		}

		public static implicit operator Playable(AnimationLayerMixerPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		public static explicit operator AnimationLayerMixerPlayable(Playable playable)
		{
			return new AnimationLayerMixerPlayable(playable.GetHandle());
		}

		public bool Equals(AnimationLayerMixerPlayable other)
		{
			return GetHandle() == other.GetHandle();
		}

		public bool IsLayerAdditive(uint layerIndex)
		{
			if (layerIndex >= m_Handle.GetInputCount())
			{
				throw new ArgumentOutOfRangeException("layerIndex", $"layerIndex {layerIndex} must be in the range of 0 to {m_Handle.GetInputCount() - 1}.");
			}
			return IsLayerAdditiveInternal(ref m_Handle, layerIndex);
		}

		public void SetLayerAdditive(uint layerIndex, bool value)
		{
			if (layerIndex >= m_Handle.GetInputCount())
			{
				throw new ArgumentOutOfRangeException("layerIndex", $"layerIndex {layerIndex} must be in the range of 0 to {m_Handle.GetInputCount() - 1}.");
			}
			SetLayerAdditiveInternal(ref m_Handle, layerIndex, value);
		}

		public void SetLayerMaskFromAvatarMask(uint layerIndex, AvatarMask mask)
		{
			if (layerIndex >= m_Handle.GetInputCount())
			{
				throw new ArgumentOutOfRangeException("layerIndex", $"layerIndex {layerIndex} must be in the range of 0 to {m_Handle.GetInputCount() - 1}.");
			}
			if (mask == null)
			{
				throw new ArgumentNullException("mask");
			}
			SetLayerMaskFromAvatarMaskInternal(ref m_Handle, layerIndex, mask);
		}

		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle)
		{
			return CreateHandleInternal_Injected(ref graph, ref handle);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool IsLayerAdditiveInternal(ref PlayableHandle handle, uint layerIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetLayerAdditiveInternal(ref PlayableHandle handle, uint layerIndex, bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetSingleLayerOptimizationInternal(ref PlayableHandle handle, bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetLayerMaskFromAvatarMaskInternal(ref PlayableHandle handle, uint layerIndex, AvatarMask mask);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, ref PlayableHandle handle);
	}
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationMixerPlayable.bindings.h")]
	[StaticAccessor("AnimationMixerPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Animation/Director/AnimationMixerPlayable.h")]
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	public struct AnimationMixerPlayable : IPlayable, IEquatable<AnimationMixerPlayable>
	{
		private PlayableHandle m_Handle;

		private static readonly AnimationMixerPlayable m_NullPlayable = new AnimationMixerPlayable(PlayableHandle.Null);

		public static AnimationMixerPlayable Null => m_NullPlayable;

		[Obsolete("normalizeWeights is obsolete. It has no effect and will be removed.")]
		public static AnimationMixerPlayable Create(PlayableGraph graph, int inputCount, bool normalizeWeights)
		{
			return Create(graph, inputCount);
		}

		public static AnimationMixerPlayable Create(PlayableGraph graph, int inputCount = 0)
		{
			PlayableHandle handle = CreateHandle(graph, inputCount);
			return new AnimationMixerPlayable(handle);
		}

		private static PlayableHandle CreateHandle(PlayableGraph graph, int inputCount = 0)
		{
			PlayableHandle handle = PlayableHandle.Null;
			if (!CreateHandleInternal(graph, ref handle))
			{
				return PlayableHandle.Null;
			}
			handle.SetInputCount(inputCount);
			return handle;
		}

		internal AnimationMixerPlayable(PlayableHandle handle)
		{
			if (handle.IsValid() && !handle.IsPlayableOfType<AnimationMixerPlayable>())
			{
				throw new InvalidCastException("Can't set handle: the playable is not an AnimationMixerPlayable.");
			}
			m_Handle = handle;
		}

		public PlayableHandle GetHandle()
		{
			return m_Handle;
		}

		public static implicit operator Playable(AnimationMixerPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		public static explicit operator AnimationMixerPlayable(Playable playable)
		{
			return new AnimationMixerPlayable(playable.GetHandle());
		}

		public bool Equals(AnimationMixerPlayable other)
		{
			return GetHandle() == other.GetHandle();
		}

		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle)
		{
			return CreateHandleInternal_Injected(ref graph, ref handle);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, ref PlayableHandle handle);
	}
	[RequiredByNativeCode]
	[StaticAccessor("AnimationMotionXToDeltaPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationMotionXToDeltaPlayable.bindings.h")]
	internal struct AnimationMotionXToDeltaPlayable : IPlayable, IEquatable<AnimationMotionXToDeltaPlayable>
	{
		private PlayableHandle m_Handle;

		private static readonly AnimationMotionXToDeltaPlayable m_NullPlayable = new AnimationMotionXToDeltaPlayable(PlayableHandle.Null);

		public static AnimationMotionXToDeltaPlayable Null => m_NullPlayable;

		public static AnimationMotionXToDeltaPlayable Create(PlayableGraph graph)
		{
			PlayableHandle handle = CreateHandle(graph);
			return new AnimationMotionXToDeltaPlayable(handle);
		}

		private static PlayableHandle CreateHandle(PlayableGraph graph)
		{
			PlayableHandle handle = PlayableHandle.Null;
			if (!CreateHandleInternal(graph, ref handle))
			{
				return PlayableHandle.Null;
			}
			handle.SetInputCount(1);
			return handle;
		}

		private AnimationMotionXToDeltaPlayable(PlayableHandle handle)
		{
			if (handle.IsValid() && !handle.IsPlayableOfType<AnimationMotionXToDeltaPlayable>())
			{
				throw new InvalidCastException("Can't set handle: the playable is not an AnimationMotionXToDeltaPlayable.");
			}
			m_Handle = handle;
		}

		public PlayableHandle GetHandle()
		{
			return m_Handle;
		}

		public static implicit operator Playable(AnimationMotionXToDeltaPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		public static explicit operator AnimationMotionXToDeltaPlayable(Playable playable)
		{
			return new AnimationMotionXToDeltaPlayable(playable.GetHandle());
		}

		public bool Equals(AnimationMotionXToDeltaPlayable other)
		{
			return GetHandle() == other.GetHandle();
		}

		public bool IsAbsoluteMotion()
		{
			return IsAbsoluteMotionInternal(ref m_Handle);
		}

		public void SetAbsoluteMotion(bool value)
		{
			SetAbsoluteMotionInternal(ref m_Handle, value);
		}

		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle)
		{
			return CreateHandleInternal_Injected(ref graph, ref handle);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool IsAbsoluteMotionInternal(ref PlayableHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetAbsoluteMotionInternal(ref PlayableHandle handle, bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, ref PlayableHandle handle);
	}
	[StaticAccessor("AnimationOffsetPlayableBindings", StaticAccessorType.DoubleColon)]
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationOffsetPlayable.bindings.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Modules/Animation/Director/AnimationOffsetPlayable.h")]
	internal struct AnimationOffsetPlayable : IPlayable, IEquatable<AnimationOffsetPlayable>
	{
		private PlayableHandle m_Handle;

		private static readonly AnimationOffsetPlayable m_NullPlayable = new AnimationOffsetPlayable(PlayableHandle.Null);

		public static AnimationOffsetPlayable Null => m_NullPlayable;

		public static AnimationOffsetPlayable Create(PlayableGraph graph, Vector3 position, Quaternion rotation, int inputCount)
		{
			PlayableHandle handle = CreateHandle(graph, position, rotation, inputCount);
			return new AnimationOffsetPlayable(handle);
		}

		private static PlayableHandle CreateHandle(PlayableGraph graph, Vector3 position, Quaternion rotation, int inputCount)
		{
			PlayableHandle handle = PlayableHandle.Null;
			if (!CreateHandleInternal(graph, position, rotation, ref handle))
			{
				return PlayableHandle.Null;
			}
			handle.SetInputCount(inputCount);
			return handle;
		}

		internal AnimationOffsetPlayable(PlayableHandle handle)
		{
			if (handle.IsValid() && !handle.IsPlayableOfType<AnimationOffsetPlayable>())
			{
				throw new InvalidCastException("Can't set handle: the playable is not an AnimationOffsetPlayable.");
			}
			m_Handle = handle;
		}

		public PlayableHandle GetHandle()
		{
			return m_Handle;
		}

		public static implicit operator Playable(AnimationOffsetPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		public static explicit operator AnimationOffsetPlayable(Playable playable)
		{
			return new AnimationOffsetPlayable(playable.GetHandle());
		}

		public bool Equals(AnimationOffsetPlayable other)
		{
			return Equals(other.GetHandle());
		}

		public Vector3 GetPosition()
		{
			return GetPositionInternal(ref m_Handle);
		}

		public void SetPosition(Vector3 value)
		{
			SetPositionInternal(ref m_Handle, value);
		}

		public Quaternion GetRotation()
		{
			return GetRotationInternal(ref m_Handle);
		}

		public void SetRotation(Quaternion value)
		{
			SetRotationInternal(ref m_Handle, value);
		}

		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, Vector3 position, Quaternion rotation, ref PlayableHandle handle)
		{
			return CreateHandleInternal_Injected(ref graph, ref position, ref rotation, ref handle);
		}

		[NativeThrows]
		private static Vector3 GetPositionInternal(ref PlayableHandle handle)
		{
			GetPositionInternal_Injected(ref handle, out var ret);
			return ret;
		}

		[NativeThrows]
		private static void SetPositionInternal(ref PlayableHandle handle, Vector3 value)
		{
			SetPositionInternal_Injected(ref handle, ref value);
		}

		[NativeThrows]
		private static Quaternion GetRotationInternal(ref PlayableHandle handle)
		{
			GetRotationInternal_Injected(ref handle, out var ret);
			return ret;
		}

		[NativeThrows]
		private static void SetRotationInternal(ref PlayableHandle handle, Quaternion value)
		{
			SetRotationInternal_Injected(ref handle, ref value);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, ref Vector3 position, ref Quaternion rotation, ref PlayableHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPositionInternal_Injected(ref PlayableHandle handle, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPositionInternal_Injected(ref PlayableHandle handle, ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRotationInternal_Injected(ref PlayableHandle handle, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRotationInternal_Injected(ref PlayableHandle handle, ref Quaternion value);
	}
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Modules/Animation/Director/AnimationPlayableExtensions.h")]
	[NativeHeader("Modules/Animation/AnimationClip.h")]
	public static class AnimationPlayableExtensions
	{
		public static void SetAnimatedProperties<U>(this U playable, AnimationClip clip) where U : struct, IPlayable
		{
			PlayableHandle playable2 = playable.GetHandle();
			SetAnimatedPropertiesInternal(ref playable2, clip);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		internal static extern void SetAnimatedPropertiesInternal(ref PlayableHandle playable, AnimationClip animatedProperties);
	}
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationPlayableGraphExtensions.bindings.h")]
	[NativeHeader("Modules/Animation/Animator.h")]
	[NativeHeader("Runtime/Director/Core/HPlayableOutput.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[StaticAccessor("AnimationPlayableGraphExtensionsBindings", StaticAccessorType.DoubleColon)]
	internal static class AnimationPlayableGraphExtensions
	{
		internal static void SyncUpdateAndTimeMode(this PlayableGraph graph, Animator animator)
		{
			InternalSyncUpdateAndTimeMode(ref graph, animator);
		}

		internal static void DestroyOutput(this PlayableGraph graph, PlayableOutputHandle handle)
		{
			InternalDestroyOutput(ref graph, ref handle);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		internal static extern bool InternalCreateAnimationOutput(ref PlayableGraph graph, string name, out PlayableOutputHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		internal static extern void InternalSyncUpdateAndTimeMode(ref PlayableGraph graph, [NotNull("ArgumentNullException")] Animator animator);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void InternalDestroyOutput(ref PlayableGraph graph, ref PlayableOutputHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern int InternalAnimationOutputCount(ref PlayableGraph graph);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool InternalGetAnimationOutput(ref PlayableGraph graph, int index, out PlayableOutputHandle handle);
	}
	[StaticAccessor("AnimationPlayableOutputBindings", StaticAccessorType.DoubleColon)]
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Director/Core/HPlayableGraph.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationPlayableOutput.bindings.h")]
	[NativeHeader("Modules/Animation/Director/AnimationPlayableOutput.h")]
	[NativeHeader("Modules/Animation/Animator.h")]
	[NativeHeader("Runtime/Director/Core/HPlayableOutput.h")]
	public struct AnimationPlayableOutput : IPlayableOutput
	{
		private PlayableOutputHandle m_Handle;

		public static AnimationPlayableOutput Null => new AnimationPlayableOutput(PlayableOutputHandle.Null);

		public static AnimationPlayableOutput Create(PlayableGraph graph, string name, Animator target)
		{
			if (!AnimationPlayableGraphExtensions.InternalCreateAnimationOutput(ref graph, name, out var handle))
			{
				return Null;
			}
			AnimationPlayableOutput result = new AnimationPlayableOutput(handle);
			result.SetTarget(target);
			return result;
		}

		internal AnimationPlayableOutput(PlayableOutputHandle handle)
		{
			if (handle.IsValid() && !handle.IsPlayableOutputOfType<AnimationPlayableOutput>())
			{
				throw new InvalidCastException("Can't set handle: the playable is not an AnimationPlayableOutput.");
			}
			m_Handle = handle;
		}

		public PlayableOutputHandle GetHandle()
		{
			return m_Handle;
		}

		public static implicit operator PlayableOutput(AnimationPlayableOutput output)
		{
			return new PlayableOutput(output.GetHandle());
		}

		public static explicit operator AnimationPlayableOutput(PlayableOutput output)
		{
			return new AnimationPlayableOutput(output.GetHandle());
		}

		public Animator GetTarget()
		{
			return InternalGetTarget(ref m_Handle);
		}

		public void SetTarget(Animator value)
		{
			InternalSetTarget(ref m_Handle, value);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern Animator InternalGetTarget(ref PlayableOutputHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void InternalSetTarget(ref PlayableOutputHandle handle, Animator target);
	}
	[StaticAccessor("AnimationPosePlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationPosePlayable.bindings.h")]
	[NativeHeader("Modules/Animation/Director/AnimationPosePlayable.h")]
	internal struct AnimationPosePlayable : IPlayable, IEquatable<AnimationPosePlayable>
	{
		private PlayableHandle m_Handle;

		private static readonly AnimationPosePlayable m_NullPlayable = new AnimationPosePlayable(PlayableHandle.Null);

		public static AnimationPosePlayable Null => m_NullPlayable;

		public static AnimationPosePlayable Create(PlayableGraph graph)
		{
			PlayableHandle handle = CreateHandle(graph);
			return new AnimationPosePlayable(handle);
		}

		private static PlayableHandle CreateHandle(PlayableGraph graph)
		{
			PlayableHandle handle = PlayableHandle.Null;
			if (!CreateHandleInternal(graph, ref handle))
			{
				return PlayableHandle.Null;
			}
			return handle;
		}

		internal AnimationPosePlayable(PlayableHandle handle)
		{
			if (handle.IsValid() && !handle.IsPlayableOfType<AnimationPosePlayable>())
			{
				throw new InvalidCastException("Can't set handle: the playable is not an AnimationPosePlayable.");
			}
			m_Handle = handle;
		}

		public PlayableHandle GetHandle()
		{
			return m_Handle;
		}

		public static implicit operator Playable(AnimationPosePlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		public static explicit operator AnimationPosePlayable(Playable playable)
		{
			return new AnimationPosePlayable(playable.GetHandle());
		}

		public bool Equals(AnimationPosePlayable other)
		{
			return Equals(other.GetHandle());
		}

		public bool GetMustReadPreviousPose()
		{
			return GetMustReadPreviousPoseInternal(ref m_Handle);
		}

		public void SetMustReadPreviousPose(bool value)
		{
			SetMustReadPreviousPoseInternal(ref m_Handle, value);
		}

		public bool GetReadDefaultPose()
		{
			return GetReadDefaultPoseInternal(ref m_Handle);
		}

		public void SetReadDefaultPose(bool value)
		{
			SetReadDefaultPoseInternal(ref m_Handle, value);
		}

		public bool GetApplyFootIK()
		{
			return GetApplyFootIKInternal(ref m_Handle);
		}

		public void SetApplyFootIK(bool value)
		{
			SetApplyFootIKInternal(ref m_Handle, value);
		}

		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle)
		{
			return CreateHandleInternal_Injected(ref graph, ref handle);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool GetMustReadPreviousPoseInternal(ref PlayableHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetMustReadPreviousPoseInternal(ref PlayableHandle handle, bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool GetReadDefaultPoseInternal(ref PlayableHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetReadDefaultPoseInternal(ref PlayableHandle handle, bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool GetApplyFootIKInternal(ref PlayableHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetApplyFootIKInternal(ref PlayableHandle handle, bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, ref PlayableHandle handle);
	}
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationRemoveScalePlayable.bindings.h")]
	[NativeHeader("Modules/Animation/Director/AnimationRemoveScalePlayable.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[StaticAccessor("AnimationRemoveScalePlayableBindings", StaticAccessorType.DoubleColon)]
	internal struct AnimationRemoveScalePlayable : IPlayable, IEquatable<AnimationRemoveScalePlayable>
	{
		private PlayableHandle m_Handle;

		private static readonly AnimationRemoveScalePlayable m_NullPlayable = new AnimationRemoveScalePlayable(PlayableHandle.Null);

		public static AnimationRemoveScalePlayable Null => m_NullPlayable;

		public static AnimationRemoveScalePlayable Create(PlayableGraph graph, int inputCount)
		{
			PlayableHandle handle = CreateHandle(graph, inputCount);
			return new AnimationRemoveScalePlayable(handle);
		}

		private static PlayableHandle CreateHandle(PlayableGraph graph, int inputCount)
		{
			PlayableHandle handle = PlayableHandle.Null;
			if (!CreateHandleInternal(graph, ref handle))
			{
				return PlayableHandle.Null;
			}
			handle.SetInputCount(inputCount);
			return handle;
		}

		internal AnimationRemoveScalePlayable(PlayableHandle handle)
		{
			if (handle.IsValid() && !handle.IsPlayableOfType<AnimationRemoveScalePlayable>())
			{
				throw new InvalidCastException("Can't set handle: the playable is not an AnimationRemoveScalePlayable.");
			}
			m_Handle = handle;
		}

		public PlayableHandle GetHandle()
		{
			return m_Handle;
		}

		public static implicit operator Playable(AnimationRemoveScalePlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		public static explicit operator AnimationRemoveScalePlayable(Playable playable)
		{
			return new AnimationRemoveScalePlayable(playable.GetHandle());
		}

		public bool Equals(AnimationRemoveScalePlayable other)
		{
			return Equals(other.GetHandle());
		}

		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle)
		{
			return CreateHandleInternal_Injected(ref graph, ref handle);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, ref PlayableHandle handle);
	}
	[NativeHeader("Runtime/Director/Core/HPlayableGraph.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationScriptPlayable.bindings.h")]
	[StaticAccessor("AnimationScriptPlayableBindings", StaticAccessorType.DoubleColon)]
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[MovedFrom("UnityEngine.Experimental.Animations")]
	public struct AnimationScriptPlayable : IAnimationJobPlayable, IPlayable, IEquatable<AnimationScriptPlayable>
	{
		private PlayableHandle m_Handle;

		private static readonly AnimationScriptPlayable m_NullPlayable = new AnimationScriptPlayable(PlayableHandle.Null);

		public static AnimationScriptPlayable Null => m_NullPlayable;

		public static AnimationScriptPlayable Create<T>(PlayableGraph graph, T jobData, int inputCount = 0) where T : struct, IAnimationJob
		{
			PlayableHandle handle = CreateHandle<T>(graph, inputCount);
			AnimationScriptPlayable result = new AnimationScriptPlayable(handle);
			result.SetJobData(jobData);
			return result;
		}

		private static PlayableHandle CreateHandle<T>(PlayableGraph graph, int inputCount) where T : struct, IAnimationJob
		{
			IntPtr jobReflectionData = ProcessAnimationJobStruct<T>.GetJobReflectionData();
			PlayableHandle handle = PlayableHandle.Null;
			if (!CreateHandleInternal(graph, ref handle, jobReflectionData))
			{
				return PlayableHandle.Null;
			}
			handle.SetInputCount(inputCount);
			return handle;
		}

		internal AnimationScriptPlayable(PlayableHandle handle)
		{
			if (handle.IsValid() && !handle.IsPlayableOfType<AnimationScriptPlayable>())
			{
				throw new InvalidCastException("Can't set handle: the playable is not an AnimationScriptPlayable.");
			}
			m_Handle = handle;
		}

		public PlayableHandle GetHandle()
		{
			return m_Handle;
		}

		private void CheckJobTypeValidity<T>()
		{
			Type jobType = GetHandle().GetJobType();
			if (jobType != typeof(T))
			{
				throw new ArgumentException($"Wrong type: the given job type ({typeof(T).FullName}) is different from the creation job type ({jobType.FullName}).");
			}
		}

		public unsafe T GetJobData<T>() where T : struct, IAnimationJob
		{
			CheckJobTypeValidity<T>();
			UnsafeUtility.CopyPtrToStructure<T>((void*)GetHandle().GetJobData(), out var output);
			return output;
		}

		public unsafe void SetJobData<T>(T jobData) where T : struct, IAnimationJob
		{
			CheckJobTypeValidity<T>();
			UnsafeUtility.CopyStructureToPtr(ref jobData, (void*)GetHandle().GetJobData());
		}

		public static implicit operator Playable(AnimationScriptPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		public static explicit operator AnimationScriptPlayable(Playable playable)
		{
			return new AnimationScriptPlayable(playable.GetHandle());
		}

		public bool Equals(AnimationScriptPlayable other)
		{
			return GetHandle() == other.GetHandle();
		}

		public void SetProcessInputs(bool value)
		{
			SetProcessInputsInternal(GetHandle(), value);
		}

		public bool GetProcessInputs()
		{
			return GetProcessInputsInternal(GetHandle());
		}

		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle, IntPtr jobReflectionData)
		{
			return CreateHandleInternal_Injected(ref graph, ref handle, jobReflectionData);
		}

		[NativeThrows]
		private static void SetProcessInputsInternal(PlayableHandle handle, bool value)
		{
			SetProcessInputsInternal_Injected(ref handle, value);
		}

		[NativeThrows]
		private static bool GetProcessInputsInternal(PlayableHandle handle)
		{
			return GetProcessInputsInternal_Injected(ref handle);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, ref PlayableHandle handle, IntPtr jobReflectionData);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetProcessInputsInternal_Injected(ref PlayableHandle handle, bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetProcessInputsInternal_Injected(ref PlayableHandle handle);
	}
	internal enum AnimatorBindingsVersion
	{
		kInvalidNotNative,
		kInvalidUnresolved,
		kValidMinVersion
	}
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Modules/Animation/Director/AnimationStream.h")]
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationStream.bindings.h")]
	public struct AnimationStream
	{
		private uint m_AnimatorBindingsVersion;

		private IntPtr constant;

		private IntPtr input;

		private IntPtr output;

		private IntPtr workspace;

		private IntPtr inputStreamAccessor;

		private IntPtr animationHandleBinder;

		internal const int InvalidIndex = -1;

		internal uint animatorBindingsVersion => m_AnimatorBindingsVersion;

		public bool isValid => m_AnimatorBindingsVersion >= 2 && constant != IntPtr.Zero && input != IntPtr.Zero && output != IntPtr.Zero && workspace != IntPtr.Zero && animationHandleBinder != IntPtr.Zero;

		public float deltaTime
		{
			get
			{
				CheckIsValid();
				return GetDeltaTime();
			}
		}

		public Vector3 velocity
		{
			get
			{
				CheckIsValid();
				return GetVelocity();
			}
			set
			{
				CheckIsValid();
				SetVelocity(value);
			}
		}

		public Vector3 angularVelocity
		{
			get
			{
				CheckIsValid();
				return GetAngularVelocity();
			}
			set
			{
				CheckIsValid();
				SetAngularVelocity(value);
			}
		}

		public Vector3 rootMotionPosition
		{
			get
			{
				CheckIsValid();
				return GetRootMotionPosition();
			}
		}

		public Quaternion rootMotionRotation
		{
			get
			{
				CheckIsValid();
				return GetRootMotionRotation();
			}
		}

		public bool isHumanStream
		{
			get
			{
				CheckIsValid();
				return GetIsHumanStream();
			}
		}

		public int inputStreamCount
		{
			get
			{
				CheckIsValid();
				return GetInputStreamCount();
			}
		}

		internal void CheckIsValid()
		{
			if (!isValid)
			{
				throw new InvalidOperationException("The AnimationStream is invalid.");
			}
		}

		public AnimationHumanStream AsHuman()
		{
			CheckIsValid();
			if (!GetIsHumanStream())
			{
				throw new InvalidOperationException("Cannot create an AnimationHumanStream for a generic rig.");
			}
			return GetHumanStream();
		}

		public AnimationStream GetInputStream(int index)
		{
			CheckIsValid();
			return InternalGetInputStream(index);
		}

		public float GetInputWeight(int index)
		{
			CheckIsValid();
			return InternalGetInputWeight(index);
		}

		public void CopyAnimationStreamMotion(AnimationStream animationStream)
		{
			CheckIsValid();
			animationStream.CheckIsValid();
			CopyAnimationStreamMotionInternal(animationStream);
		}

		private void ReadSceneTransforms()
		{
			CheckIsValid();
			InternalReadSceneTransforms();
		}

		private void WriteSceneTransforms()
		{
			CheckIsValid();
			InternalWriteSceneTransforms();
		}

		[NativeMethod(Name = "AnimationStreamBindings::CopyAnimationStreamMotion", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void CopyAnimationStreamMotionInternal(AnimationStream animationStream)
		{
			CopyAnimationStreamMotionInternal_Injected(ref this, ref animationStream);
		}

		[NativeMethod(IsThreadSafe = true)]
		private float GetDeltaTime()
		{
			return GetDeltaTime_Injected(ref this);
		}

		[NativeMethod(IsThreadSafe = true)]
		private bool GetIsHumanStream()
		{
			return GetIsHumanStream_Injected(ref this);
		}

		[NativeMethod(Name = "AnimationStreamBindings::GetVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetVelocity()
		{
			GetVelocity_Injected(ref this, out var ret);
			return ret;
		}

		[NativeMethod(Name = "AnimationStreamBindings::SetVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void SetVelocity(Vector3 velocity)
		{
			SetVelocity_Injected(ref this, ref velocity);
		}

		[NativeMethod(Name = "AnimationStreamBindings::GetAngularVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetAngularVelocity()
		{
			GetAngularVelocity_Injected(ref this, out var ret);
			return ret;
		}

		[NativeMethod(Name = "AnimationStreamBindings::SetAngularVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void SetAngularVelocity(Vector3 velocity)
		{
			SetAngularVelocity_Injected(ref this, ref velocity);
		}

		[NativeMethod(Name = "AnimationStreamBindings::GetRootMotionPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetRootMotionPosition()
		{
			GetRootMotionPosition_Injected(ref this, out var ret);
			return ret;
		}

		[NativeMethod(Name = "AnimationStreamBindings::GetRootMotionRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion GetRootMotionRotation()
		{
			GetRootMotionRotation_Injected(ref this, out var ret);
			return ret;
		}

		[NativeMethod(IsThreadSafe = true)]
		private int GetInputStreamCount()
		{
			return GetInputStreamCount_Injected(ref this);
		}

		[NativeMethod(Name = "GetInputStream", IsThreadSafe = true)]
		private AnimationStream InternalGetInputStream(int index)
		{
			InternalGetInputStream_Injected(ref this, index, out var ret);
			return ret;
		}

		[NativeMethod(Name = "GetInputWeight", IsThreadSafe = true)]
		private float InternalGetInputWeight(int index)
		{
			return InternalGetInputWeight_Injected(ref this, index);
		}

		[NativeMethod(IsThreadSafe = true)]
		private AnimationHumanStream GetHumanStream()
		{
			GetHumanStream_Injected(ref this, out var ret);
			return ret;
		}

		[NativeMethod(Name = "ReadSceneTransforms", IsThreadSafe = true)]
		private void InternalReadSceneTransforms()
		{
			InternalReadSceneTransforms_Injected(ref this);
		}

		[NativeMethod(Name = "WriteSceneTransforms", IsThreadSafe = true)]
		private void InternalWriteSceneTransforms()
		{
			InternalWriteSceneTransforms_Injected(ref this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyAnimationStreamMotionInternal_Injected(ref AnimationStream _unity_self, ref AnimationStream animationStream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetDeltaTime_Injected(ref AnimationStream _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetIsHumanStream_Injected(ref AnimationStream _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetVelocity_Injected(ref AnimationStream _unity_self, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVelocity_Injected(ref AnimationStream _unity_self, ref Vector3 velocity);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAngularVelocity_Injected(ref AnimationStream _unity_self, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAngularVelocity_Injected(ref AnimationStream _unity_self, ref Vector3 velocity);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRootMotionPosition_Injected(ref AnimationStream _unity_self, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRootMotionRotation_Injected(ref AnimationStream _unity_self, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetInputStreamCount_Injected(ref AnimationStream _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetInputStream_Injected(ref AnimationStream _unity_self, int index, out AnimationStream ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float InternalGetInputWeight_Injected(ref AnimationStream _unity_self, int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetHumanStream_Injected(ref AnimationStream _unity_self, out AnimationHumanStream ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalReadSceneTransforms_Injected(ref AnimationStream _unity_self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalWriteSceneTransforms_Injected(ref AnimationStream _unity_self);
	}
	internal enum BindType
	{
		Unbound = 0,
		Float = 5,
		Bool = 6,
		GameObjectActive = 7,
		ObjectReference = 9,
		Int = 10,
		DiscreetInt = 11
	}
	[NativeHeader("Modules/Animation/Director/AnimationStreamHandles.h")]
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationStreamHandles.bindings.h")]
	public struct TransformStreamHandle
	{
		private uint m_AnimatorBindingsVersion;

		private int handleIndex;

		private int skeletonIndex;

		private bool createdByNative => animatorBindingsVersion != 0;

		private bool hasHandleIndex => handleIndex != -1;

		private bool hasSkeletonIndex => skeletonIndex != -1;

		internal uint animatorBindingsVersion
		{
			get
			{
				return m_AnimatorBindingsVersion;
			}
			private set
			{
				m_AnimatorBindingsVersion = value;
			}
		}

		public bool IsValid(AnimationStream stream)
		{
			return IsValidInternal(ref stream);
		}

		private bool IsValidInternal(ref AnimationStream stream)
		{
			return stream.isValid && createdByNative && hasHandleIndex;
		}

		private bool IsSameVersionAsStream(ref AnimationStream stream)
		{
			return animatorBindingsVersion == stream.animatorBindingsVersion;
		}

		public void Resolve(AnimationStream stream)
		{
			CheckIsValidAndResolve(ref stream);
		}

		public bool IsResolved(AnimationStream stream)
		{
			return IsResolvedInternal(ref stream);
		}

		private bool IsResolvedInternal(ref AnimationStream stream)
		{
			return IsValidInternal(ref stream) && IsSameVersionAsStream(ref stream) && hasSkeletonIndex;
		}

		private void CheckIsValidAndResolve(ref AnimationStream stream)
		{
			stream.CheckIsValid();
			if (!IsResolvedInternal(ref stream))
			{
				if (!createdByNative || !hasHandleIndex)
				{
					throw new InvalidOperationException("The TransformStreamHandle is invalid. Please use proper function to create the handle.");
				}
				if (!IsSameVersionAsStream(ref stream) || (hasHandleIndex && !hasSkeletonIndex))
				{
					ResolveInternal(ref stream);
				}
				if (hasHandleIndex && !hasSkeletonIndex)
				{
					throw new InvalidOperationException("The TransformStreamHandle cannot be resolved.");
				}
			}
		}

		public Vector3 GetPosition(AnimationStream stream)
		{
			CheckIsValidAndResolve(ref stream);
			return GetPositionInternal(ref stream);
		}

		public void SetPosition(AnimationStream stream, Vector3 position)
		{
			CheckIsValidAndResolve(ref stream);
			SetPositionInternal(ref stream, position);
		}

		public Quaternion GetRotation(AnimationStream stream)
		{
			CheckIsValidAndResolve(ref stream);
			return GetRotationInternal(ref stream);
		}

		public void SetRotation(AnimationStream stream, Quaternion rotation)
		{
			CheckIsValidAndResolve(ref stream);
			SetRotationInternal(ref stream, rotation);
		}

		public Vector3 GetLocalPosition(AnimationStream stream)
		{
			CheckIsValidAndResolve(ref stream);
			return GetLocalPositionInternal(ref stream);
		}

		public void SetLocalPosition(AnimationStream stream, Vector3 position)
		{
			CheckIsValidAndResolve(ref stream);
			SetLocalPositionInternal(ref stream, position);
		}

		public Quaternion GetLocalRotation(AnimationStream stream)
		{
			CheckIsValidAndResolve(ref stream);
			return GetLocalRotationInternal(ref stream);
		}

		public void SetLocalRotation(AnimationStream stream, Quaternion rotation)
		{
			CheckIsValidAndResolve(ref stream);
			SetLocalRotationInternal(ref stream, rotation);
		}

		public Vector3 GetLocalScale(AnimationStream stream)
		{
			CheckIsValidAndResolve(ref stream);
			return GetLocalScaleInternal(ref stream);
		}

		public void SetLocalScale(AnimationStream stream, Vector3 scale)
		{
			CheckIsValidAndResolve(ref stream);
			SetLocalScaleInternal(ref stream, scale);
		}

		public Matrix4x4 GetLocalToParentMatrix(AnimationStream stream)
		{
			CheckIsValidAndResolve(ref stream);
			return GetLocalToParentMatrixInternal(ref stream);
		}

		public bool GetPositionReadMask(AnimationStream stream)
		{
			CheckIsValidAndResolve(ref stream);
			return GetPositionReadMaskInternal(ref stream);
		}

		public bool GetRotationReadMask(AnimationStream stream)
		{
			CheckIsValidAndResolve(ref stream);
			return GetRotationReadMaskInternal(ref stream);
		}

		public bool GetScaleReadMask(AnimationStream stream)
		{
			CheckIsValidAndResolve(ref stream);
			return GetScaleReadMaskInternal(ref stream);
		}

		public void GetLocalTRS(AnimationStream stream, out Vector3 position, out Quaternion rotation, out Vector3 scale)
		{
			CheckIsValidAndResolve(ref stream);
			GetLocalTRSInternal(ref stream, out position, out rotation, out scale);
		}

		public void SetLocalTRS(AnimationStream stream, Vector3 position, Quaternion rotation, Vector3 scale, bool useMask)
		{
			CheckIsValidAndResolve(ref stream);
			SetLocalTRSInternal(ref stream, position, rotation, scale, useMask);
		}

		public void GetGlobalTR(AnimationStream stream, out Vector3 position, out Quaternion rotation)
		{
			CheckIsValidAndResolve(ref stream);
			GetGlobalTRInternal(ref stream, out position, out rotation);
		}

		public Matrix4x4 GetLocalToWorldMatrix(AnimationStream stream)
		{
			CheckIsValidAndResolve(ref stream);
			return GetLocalToWorldMatrixInternal(ref stream);
		}

		public void SetGlobalTR(AnimationStream stream, Vector3 position, Quaternion rotation, bool useMask)
		{
			CheckIsValidAndResolve(ref stream);
			SetGlobalTRInternal(ref stream, position, rotation, useMask);
		}

		[NativeMethod(Name = "Resolve", IsThreadSafe = true)]
		private void ResolveInternal(ref AnimationStream stream)
		{
			ResolveInternal_Injected(ref this, ref stream);
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::GetPositionInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Vector3 GetPositionInternal(ref AnimationStream stream)
		{
			GetPositionInternal_Injected(ref this, ref stream, out var ret);
			return ret;
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::SetPositionInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetPositionInternal(ref AnimationStream stream, Vector3 position)
		{
			SetPositionInternal_Injected(ref this, ref stream, ref position);
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::GetRotationInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Quaternion GetRotationInternal(ref AnimationStream stream)
		{
			GetRotationInternal_Injected(ref this, ref stream, out var ret);
			return ret;
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::SetRotationInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetRotationInternal(ref AnimationStream stream, Quaternion rotation)
		{
			SetRotationInternal_Injected(ref this, ref stream, ref rotation);
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::GetLocalPositionInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Vector3 GetLocalPositionInternal(ref AnimationStream stream)
		{
			GetLocalPositionInternal_Injected(ref this, ref stream, out var ret);
			return ret;
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::SetLocalPositionInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetLocalPositionInternal(ref AnimationStream stream, Vector3 position)
		{
			SetLocalPositionInternal_Injected(ref this, ref stream, ref position);
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::GetLocalRotationInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Quaternion GetLocalRotationInternal(ref AnimationStream stream)
		{
			GetLocalRotationInternal_Injected(ref this, ref stream, out var ret);
			return ret;
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::SetLocalRotationInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetLocalRotationInternal(ref AnimationStream stream, Quaternion rotation)
		{
			SetLocalRotationInternal_Injected(ref this, ref stream, ref rotation);
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::GetLocalScaleInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Vector3 GetLocalScaleInternal(ref AnimationStream stream)
		{
			GetLocalScaleInternal_Injected(ref this, ref stream, out var ret);
			return ret;
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::SetLocalScaleInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetLocalScaleInternal(ref AnimationStream stream, Vector3 scale)
		{
			SetLocalScaleInternal_Injected(ref this, ref stream, ref scale);
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::GetLocalToParentMatrixInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Matrix4x4 GetLocalToParentMatrixInternal(ref AnimationStream stream)
		{
			GetLocalToParentMatrixInternal_Injected(ref this, ref stream, out var ret);
			return ret;
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::GetPositionReadMaskInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private bool GetPositionReadMaskInternal(ref AnimationStream stream)
		{
			return GetPositionReadMaskInternal_Injected(ref this, ref stream);
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::GetRotationReadMaskInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private bool GetRotationReadMaskInternal(ref AnimationStream stream)
		{
			return GetRotationReadMaskInternal_Injected(ref this, ref stream);
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::GetScaleReadMaskInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private bool GetScaleReadMaskInternal(ref AnimationStream stream)
		{
			return GetScaleReadMaskInternal_Injected(ref this, ref stream);
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::GetLocalTRSInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void GetLocalTRSInternal(ref AnimationStream stream, out Vector3 position, out Quaternion rotation, out Vector3 scale)
		{
			GetLocalTRSInternal_Injected(ref this, ref stream, out position, out rotation, out scale);
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::SetLocalTRSInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetLocalTRSInternal(ref AnimationStream stream, Vector3 position, Quaternion rotation, Vector3 scale, bool useMask)
		{
			SetLocalTRSInternal_Injected(ref this, ref stream, ref position, ref rotation, ref scale, useMask);
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::GetGlobalTRInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void GetGlobalTRInternal(ref AnimationStream stream, out Vector3 position, out Quaternion rotation)
		{
			GetGlobalTRInternal_Injected(ref this, ref stream, out position, out rotation);
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::GetLocalToWorldMatrixInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Matrix4x4 GetLocalToWorldMatrixInternal(ref AnimationStream stream)
		{
			GetLocalToWorldMatrixInternal_Injected(ref this, ref stream, out var ret);
			return ret;
		}

		[NativeMethod(Name = "TransformStreamHandleBindings::SetGlobalTRInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private void SetGlobalTRInternal(ref AnimationStream stream, Vector3 position, Quaternion rotation, bool useMask)
		{
			SetGlobalTRInternal_Injected(ref this, ref stream, ref position, ref rotation, useMask);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResolveInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPositionInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPositionInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, ref Vector3 position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRotationInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRotationInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, ref Quaternion rotation);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalPositionInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalPositionInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, ref Vector3 position);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalRotationInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalRotationInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, ref Quaternion rotation);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalScaleInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalScaleInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, ref Vector3 scale);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalToParentMatrixInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetPositionReadMaskInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetRotationReadMaskInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetScaleReadMaskInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalTRSInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Vector3 position, out Quaternion rotation, out Vector3 scale);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalTRSInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, ref Vector3 position, ref Quaternion rotation, ref Vector3 scale, bool useMask);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGlobalTRInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Vector3 position, out Quaternion rotation);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalToWorldMatrixInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalTRInternal_Injected(ref TransformStreamHandle _unity_self, ref AnimationStream stream, ref Vector3 position, ref Quaternion rotation, bool useMask);
	}
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Modules/Animation/Director/AnimationStreamHandles.h")]
	public struct PropertyStreamHandle
	{
		private uint m_AnimatorBindingsVersion;

		private int handleIndex;

		private int valueArrayIndex;

		private int bindType;

		private bool createdByNative => animatorBindingsVersion != 0;

		private bool hasHandleIndex => handleIndex != -1;

		private bool hasValueArrayIndex => valueArrayIndex != -1;

		private bool hasBindType => bindType != 0;

		internal uint animatorBindingsVersion
		{
			get
			{
				return m_AnimatorBindingsVersion;
			}
			private set
			{
				m_AnimatorBindingsVersion = value;
			}
		}

		public bool IsValid(AnimationStream stream)
		{
			return IsValidInternal(ref stream);
		}

		private bool IsValidInternal(ref AnimationStream stream)
		{
			return stream.isValid && createdByNative && hasHandleIndex && hasBindType;
		}

		private bool IsSameVersionAsStream(ref AnimationStream stream)
		{
			return animatorBindingsVersion == stream.animatorBindingsVersion;
		}

		public void Resolve(AnimationStream stream)
		{
			CheckIsValidAndResolve(ref stream);
		}

		public bool IsResolved(AnimationStream stream)
		{
			return IsResolvedInternal(ref stream);
		}

		private bool IsResolvedInternal(ref AnimationStream stream)
		{
			return IsValidInternal(ref stream) && IsSameVersionAsStream(ref stream) && hasValueArrayIndex;
		}

		private void CheckIsValidAndResolve(ref AnimationStream stream)
		{
			stream.CheckIsValid();
			if (!IsResolvedInternal(ref stream))
			{
				if (!createdByNative || !hasHandleIndex || !hasBindType)
				{
					throw new InvalidOperationException("The PropertyStreamHandle is invalid. Please use proper function to create the handle.");
				}
				if (!IsSameVersionAsStream(ref stream) || (hasHandleIndex && !hasValueArrayIndex))
				{
					ResolveInternal(ref stream);
				}
				if (hasHandleIndex && !hasValueArrayIndex)
				{
					throw new InvalidOperationException("The PropertyStreamHandle cannot be resolved.");
				}
			}
		}

		public float GetFloat(AnimationStream stream)
		{
			CheckIsValidAndResolve(ref stream);
			if (bindType != 5)
			{
				throw new InvalidOperationException("GetValue type doesn't match PropertyStreamHandle bound type.");
			}
			return GetFloatInternal(ref stream);
		}

		public void SetFloat(AnimationStream stream, float value)
		{
			CheckIsValidAndResolve(ref stream);
			if (bindType != 5)
			{
				throw new InvalidOperationException("SetValue type doesn't match PropertyStreamHandle bound type.");
			}
			SetFloatInternal(ref stream, value);
		}

		public int GetInt(AnimationStream stream)
		{
			CheckIsValidAndResolve(ref stream);
			if (bindType != 10 && bindType != 11 && bindType != 9)
			{
				throw new InvalidOperationException("GetValue type doesn't match PropertyStreamHandle bound type.");
			}
			return GetIntInternal(ref stream);
		}

		public void SetInt(AnimationStream stream, int value)
		{
			CheckIsValidAndResolve(ref stream);
			if (bindType != 10 && bindType != 11 && bindType != 9)
			{
				throw new InvalidOperationException("SetValue type doesn't match PropertyStreamHandle bound type.");
			}
			SetIntInternal(ref stream, value);
		}

		public bool GetBool(AnimationStream stream)
		{
			CheckIsValidAndResolve(ref stream);
			if (bindType != 6 && bindType != 7)
			{
				throw new InvalidOperationException("GetValue type doesn't match PropertyStreamHandle bound type.");
			}
			return GetBoolInternal(ref stream);
		}

		public void SetBool(AnimationStream stream, bool value)
		{
			CheckIsValidAndResolve(ref stream);
			if (bindType != 6 && bindType != 7)
			{
				throw new InvalidOperationException("SetValue type doesn't match PropertyStreamHandle bound type.");
			}
			SetBoolInternal(ref stream, value);
		}

		public bool GetReadMask(AnimationStream stream)
		{
			CheckIsValidAndResolve(ref stream);
			return GetReadMaskInternal(ref stream);
		}

		[NativeMethod(Name = "Resolve", IsThreadSafe = true)]
		private void ResolveInternal(ref AnimationStream stream)
		{
			ResolveInternal_Injected(ref this, ref stream);
		}

		[NativeMethod(Name = "GetFloat", IsThreadSafe = true)]
		private float GetFloatInternal(ref AnimationStream stream)
		{
			return GetFloatInternal_Injected(ref this, ref stream);
		}

		[NativeMethod(Name = "SetFloat", IsThreadSafe = true)]
		private void SetFloatInternal(ref AnimationStream stream, float value)
		{
			SetFloatInternal_Injected(ref this, ref stream, value);
		}

		[NativeMethod(Name = "GetInt", IsThreadSafe = true)]
		private int GetIntInternal(ref AnimationStream stream)
		{
			return GetIntInternal_Injected(ref this, ref stream);
		}

		[NativeMethod(Name = "SetInt", IsThreadSafe = true)]
		private void SetIntInternal(ref AnimationStream stream, int value)
		{
			SetIntInternal_Injected(ref this, ref stream, value);
		}

		[NativeMethod(Name = "GetBool", IsThreadSafe = true)]
		private bool GetBoolInternal(ref AnimationStream stream)
		{
			return GetBoolInternal_Injected(ref this, ref stream);
		}

		[NativeMethod(Name = "SetBool", IsThreadSafe = true)]
		private void SetBoolInternal(ref AnimationStream stream, bool value)
		{
			SetBoolInternal_Injected(ref this, ref stream, value);
		}

		[NativeMethod(Name = "GetReadMask", IsThreadSafe = true)]
		private bool GetReadMaskInternal(ref AnimationStream stream)
		{
			return GetReadMaskInternal_Injected(ref this, ref stream);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResolveInternal_Injected(ref PropertyStreamHandle _unity_self, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetFloatInternal_Injected(ref PropertyStreamHandle _unity_self, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFloatInternal_Injected(ref PropertyStreamHandle _unity_self, ref AnimationStream stream, float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetIntInternal_Injected(ref PropertyStreamHandle _unity_self, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetIntInternal_Injected(ref PropertyStreamHandle _unity_self, ref AnimationStream stream, int value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetBoolInternal_Injected(ref PropertyStreamHandle _unity_self, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBoolInternal_Injected(ref PropertyStreamHandle _unity_self, ref AnimationStream stream, bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetReadMaskInternal_Injected(ref PropertyStreamHandle _unity_self, ref AnimationStream stream);
	}
	[NativeHeader("Modules/Animation/Director/AnimationSceneHandles.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationStreamHandles.bindings.h")]
	[MovedFrom("UnityEngine.Experimental.Animations")]
	public struct TransformSceneHandle
	{
		private uint valid;

		private int transformSceneHandleDefinitionIndex;

		private bool createdByNative => valid != 0;

		private bool hasTransformSceneHandleDefinitionIndex => transformSceneHandleDefinitionIndex != -1;

		public bool IsValid(AnimationStream stream)
		{
			return stream.isValid && createdByNative && hasTransformSceneHandleDefinitionIndex && HasValidTransform(ref stream);
		}

		private void CheckIsValid(ref AnimationStream stream)
		{
			stream.CheckIsValid();
			if (!createdByNative || !hasTransformSceneHandleDefinitionIndex)
			{
				throw new InvalidOperationException("The TransformSceneHandle is invalid. Please use proper function to create the handle.");
			}
			if (!HasValidTransform(ref stream))
			{
				throw new NullReferenceException("The transform is invalid.");
			}
		}

		public Vector3 GetPosition(AnimationStream stream)
		{
			CheckIsValid(ref stream);
			return GetPositionInternal(ref stream);
		}

		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetPosition(AnimationStream stream, Vector3 position)
		{
		}

		public Vector3 GetLocalPosition(AnimationStream stream)
		{
			CheckIsValid(ref stream);
			return GetLocalPositionInternal(ref stream);
		}

		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetLocalPosition(AnimationStream stream, Vector3 position)
		{
		}

		public Quaternion GetRotation(AnimationStream stream)
		{
			CheckIsValid(ref stream);
			return GetRotationInternal(ref stream);
		}

		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetRotation(AnimationStream stream, Quaternion rotation)
		{
		}

		public Quaternion GetLocalRotation(AnimationStream stream)
		{
			CheckIsValid(ref stream);
			return GetLocalRotationInternal(ref stream);
		}

		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetLocalRotation(AnimationStream stream, Quaternion rotation)
		{
		}

		public Vector3 GetLocalScale(AnimationStream stream)
		{
			CheckIsValid(ref stream);
			return GetLocalScaleInternal(ref stream);
		}

		public void GetLocalTRS(AnimationStream stream, out Vector3 position, out Quaternion rotation, out Vector3 scale)
		{
			CheckIsValid(ref stream);
			GetLocalTRSInternal(ref stream, out position, out rotation, out scale);
		}

		public Matrix4x4 GetLocalToParentMatrix(AnimationStream stream)
		{
			CheckIsValid(ref stream);
			return GetLocalToParentMatrixInternal(ref stream);
		}

		public void GetGlobalTR(AnimationStream stream, out Vector3 position, out Quaternion rotation)
		{
			CheckIsValid(ref stream);
			GetGlobalTRInternal(ref stream, out position, out rotation);
		}

		public Matrix4x4 GetLocalToWorldMatrix(AnimationStream stream)
		{
			CheckIsValid(ref stream);
			return GetLocalToWorldMatrixInternal(ref stream);
		}

		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetLocalScale(AnimationStream stream, Vector3 scale)
		{
		}

		[ThreadSafe]
		private bool HasValidTransform(ref AnimationStream stream)
		{
			return HasValidTransform_Injected(ref this, ref stream);
		}

		[NativeMethod(Name = "TransformSceneHandleBindings::GetPositionInternal", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetPositionInternal(ref AnimationStream stream)
		{
			GetPositionInternal_Injected(ref this, ref stream, out var ret);
			return ret;
		}

		[NativeMethod(Name = "TransformSceneHandleBindings::GetLocalPositionInternal", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetLocalPositionInternal(ref AnimationStream stream)
		{
			GetLocalPositionInternal_Injected(ref this, ref stream, out var ret);
			return ret;
		}

		[NativeMethod(Name = "TransformSceneHandleBindings::GetRotationInternal", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion GetRotationInternal(ref AnimationStream stream)
		{
			GetRotationInternal_Injected(ref this, ref stream, out var ret);
			return ret;
		}

		[NativeMethod(Name = "TransformSceneHandleBindings::GetLocalRotationInternal", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion GetLocalRotationInternal(ref AnimationStream stream)
		{
			GetLocalRotationInternal_Injected(ref this, ref stream, out var ret);
			return ret;
		}

		[NativeMethod(Name = "TransformSceneHandleBindings::GetLocalScaleInternal", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetLocalScaleInternal(ref AnimationStream stream)
		{
			GetLocalScaleInternal_Injected(ref this, ref stream, out var ret);
			return ret;
		}

		[NativeMethod(Name = "TransformSceneHandleBindings::GetLocalTRSInternal", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void GetLocalTRSInternal(ref AnimationStream stream, out Vector3 position, out Quaternion rotation, out Vector3 scale)
		{
			GetLocalTRSInternal_Injected(ref this, ref stream, out position, out rotation, out scale);
		}

		[NativeMethod(Name = "TransformSceneHandleBindings::GetLocalToParentMatrixInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Matrix4x4 GetLocalToParentMatrixInternal(ref AnimationStream stream)
		{
			GetLocalToParentMatrixInternal_Injected(ref this, ref stream, out var ret);
			return ret;
		}

		[NativeMethod(Name = "TransformSceneHandleBindings::GetGlobalTRInternal", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void GetGlobalTRInternal(ref AnimationStream stream, out Vector3 position, out Quaternion rotation)
		{
			GetGlobalTRInternal_Injected(ref this, ref stream, out position, out rotation);
		}

		[NativeMethod(Name = "TransformSceneHandleBindings::GetLocalToWorldMatrixInternal", IsFreeFunction = true, HasExplicitThis = true, IsThreadSafe = true)]
		private Matrix4x4 GetLocalToWorldMatrixInternal(ref AnimationStream stream)
		{
			GetLocalToWorldMatrixInternal_Injected(ref this, ref stream, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasValidTransform_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPositionInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalPositionInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRotationInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalRotationInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Quaternion ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalScaleInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalTRSInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Vector3 position, out Quaternion rotation, out Vector3 scale);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalToParentMatrixInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Matrix4x4 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGlobalTRInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Vector3 position, out Quaternion rotation);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalToWorldMatrixInternal_Injected(ref TransformSceneHandle _unity_self, ref AnimationStream stream, out Matrix4x4 ret);
	}
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Modules/Animation/Director/AnimationSceneHandles.h")]
	public struct PropertySceneHandle
	{
		private uint valid;

		private int handleIndex;

		private bool createdByNative => valid != 0;

		private bool hasHandleIndex => handleIndex != -1;

		public bool IsValid(AnimationStream stream)
		{
			return IsValidInternal(ref stream);
		}

		private bool IsValidInternal(ref AnimationStream stream)
		{
			return stream.isValid && createdByNative && hasHandleIndex && HasValidTransform(ref stream);
		}

		public void Resolve(AnimationStream stream)
		{
			CheckIsValid(ref stream);
			ResolveInternal(ref stream);
		}

		public bool IsResolved(AnimationStream stream)
		{
			return IsValidInternal(ref stream) && IsBound(ref stream);
		}

		private void CheckIsValid(ref AnimationStream stream)
		{
			stream.CheckIsValid();
			if (!createdByNative || !hasHandleIndex)
			{
				throw new InvalidOperationException("The PropertySceneHandle is invalid. Please use proper function to create the handle.");
			}
			if (!HasValidTransform(ref stream))
			{
				throw new NullReferenceException("The transform is invalid.");
			}
		}

		public float GetFloat(AnimationStream stream)
		{
			CheckIsValid(ref stream);
			return GetFloatInternal(ref stream);
		}

		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetFloat(AnimationStream stream, float value)
		{
		}

		public int GetInt(AnimationStream stream)
		{
			CheckIsValid(ref stream);
			return GetIntInternal(ref stream);
		}

		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetInt(AnimationStream stream, int value)
		{
		}

		public bool GetBool(AnimationStream stream)
		{
			CheckIsValid(ref stream);
			return GetBoolInternal(ref stream);
		}

		[Obsolete("SceneHandle is now read-only; it was problematic with the engine multithreading and determinism", true)]
		public void SetBool(AnimationStream stream, bool value)
		{
		}

		[ThreadSafe]
		private bool HasValidTransform(ref AnimationStream stream)
		{
			return HasValidTransform_Injected(ref this, ref stream);
		}

		[ThreadSafe]
		private bool IsBound(ref AnimationStream stream)
		{
			return IsBound_Injected(ref this, ref stream);
		}

		[NativeMethod(Name = "Resolve", IsThreadSafe = true)]
		private void ResolveInternal(ref AnimationStream stream)
		{
			ResolveInternal_Injected(ref this, ref stream);
		}

		[NativeMethod(Name = "GetFloat", IsThreadSafe = true)]
		private float GetFloatInternal(ref AnimationStream stream)
		{
			return GetFloatInternal_Injected(ref this, ref stream);
		}

		[NativeMethod(Name = "GetInt", IsThreadSafe = true)]
		private int GetIntInternal(ref AnimationStream stream)
		{
			return GetIntInternal_Injected(ref this, ref stream);
		}

		[NativeMethod(Name = "GetBool", IsThreadSafe = true)]
		private bool GetBoolInternal(ref AnimationStream stream)
		{
			return GetBoolInternal_Injected(ref this, ref stream);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasValidTransform_Injected(ref PropertySceneHandle _unity_self, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsBound_Injected(ref PropertySceneHandle _unity_self, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResolveInternal_Injected(ref PropertySceneHandle _unity_self, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetFloatInternal_Injected(ref PropertySceneHandle _unity_self, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetIntInternal_Injected(ref PropertySceneHandle _unity_self, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetBoolInternal_Injected(ref PropertySceneHandle _unity_self, ref AnimationStream stream);
	}
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationStreamHandles.bindings.h")]
	public static class AnimationSceneHandleUtility
	{
		public unsafe static void ReadInts(AnimationStream stream, NativeArray<PropertySceneHandle> handles, NativeArray<int> buffer)
		{
			int num = ValidateAndGetArrayCount(ref stream, handles, buffer);
			if (num != 0)
			{
				ReadSceneIntsInternal(ref stream, handles.GetUnsafePtr(), buffer.GetUnsafePtr(), num);
			}
		}

		public unsafe static void ReadFloats(AnimationStream stream, NativeArray<PropertySceneHandle> handles, NativeArray<float> buffer)
		{
			int num = ValidateAndGetArrayCount(ref stream, handles, buffer);
			if (num != 0)
			{
				ReadSceneFloatsInternal(ref stream, handles.GetUnsafePtr(), buffer.GetUnsafePtr(), num);
			}
		}

		internal static int ValidateAndGetArrayCount<T0, T1>(ref AnimationStream stream, NativeArray<T0> handles, NativeArray<T1> buffer) where T0 : struct where T1 : struct
		{
			stream.CheckIsValid();
			if (!handles.IsCreated)
			{
				throw new NullReferenceException("Handle array is invalid.");
			}
			if (!buffer.IsCreated)
			{
				throw new NullReferenceException("Data buffer is invalid.");
			}
			if (buffer.Length < handles.Length)
			{
				throw new InvalidOperationException("Data buffer array is smaller than handles array.");
			}
			return handles.Length;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "AnimationHandleUtilityBindings::ReadSceneIntsInternal", IsFreeFunction = true, HasExplicitThis = false, IsThreadSafe = true)]
		private unsafe static extern void ReadSceneIntsInternal(ref AnimationStream stream, void* propertySceneHandles, void* intBuffer, int count);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "AnimationHandleUtilityBindings::ReadSceneFloatsInternal", IsFreeFunction = true, HasExplicitThis = false, IsThreadSafe = true)]
		private unsafe static extern void ReadSceneFloatsInternal(ref AnimationStream stream, void* propertySceneHandles, void* floatBuffer, int count);
	}
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationStreamHandles.bindings.h")]
	[MovedFrom("UnityEngine.Experimental.Animations")]
	public static class AnimationStreamHandleUtility
	{
		public unsafe static void WriteInts(AnimationStream stream, NativeArray<PropertyStreamHandle> handles, NativeArray<int> buffer, bool useMask)
		{
			stream.CheckIsValid();
			int num = AnimationSceneHandleUtility.ValidateAndGetArrayCount(ref stream, handles, buffer);
			if (num != 0)
			{
				WriteStreamIntsInternal(ref stream, handles.GetUnsafePtr(), buffer.GetUnsafePtr(), num, useMask);
			}
		}

		public unsafe static void WriteFloats(AnimationStream stream, NativeArray<PropertyStreamHandle> handles, NativeArray<float> buffer, bool useMask)
		{
			stream.CheckIsValid();
			int num = AnimationSceneHandleUtility.ValidateAndGetArrayCount(ref stream, handles, buffer);
			if (num != 0)
			{
				WriteStreamFloatsInternal(ref stream, handles.GetUnsafePtr(), buffer.GetUnsafePtr(), num, useMask);
			}
		}

		public unsafe static void ReadInts(AnimationStream stream, NativeArray<PropertyStreamHandle> handles, NativeArray<int> buffer)
		{
			stream.CheckIsValid();
			int num = AnimationSceneHandleUtility.ValidateAndGetArrayCount(ref stream, handles, buffer);
			if (num != 0)
			{
				ReadStreamIntsInternal(ref stream, handles.GetUnsafePtr(), buffer.GetUnsafePtr(), num);
			}
		}

		public unsafe static void ReadFloats(AnimationStream stream, NativeArray<PropertyStreamHandle> handles, NativeArray<float> buffer)
		{
			stream.CheckIsValid();
			int num = AnimationSceneHandleUtility.ValidateAndGetArrayCount(ref stream, handles, buffer);
			if (num != 0)
			{
				ReadStreamFloatsInternal(ref stream, handles.GetUnsafePtr(), buffer.GetUnsafePtr(), num);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "AnimationHandleUtilityBindings::ReadStreamIntsInternal", IsFreeFunction = true, HasExplicitThis = false, IsThreadSafe = true)]
		private unsafe static extern void ReadStreamIntsInternal(ref AnimationStream stream, void* propertyStreamHandles, void* intBuffer, int count);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "AnimationHandleUtilityBindings::ReadStreamFloatsInternal", IsFreeFunction = true, HasExplicitThis = false, IsThreadSafe = true)]
		private unsafe static extern void ReadStreamFloatsInternal(ref AnimationStream stream, void* propertyStreamHandles, void* floatBuffer, int count);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "AnimationHandleUtilityBindings::WriteStreamIntsInternal", IsFreeFunction = true, HasExplicitThis = false, IsThreadSafe = true)]
		private unsafe static extern void WriteStreamIntsInternal(ref AnimationStream stream, void* propertyStreamHandles, void* intBuffer, int count, bool useMask);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "AnimationHandleUtilityBindings::WriteStreamFloatsInternal", IsFreeFunction = true, HasExplicitThis = false, IsThreadSafe = true)]
		private unsafe static extern void WriteStreamFloatsInternal(ref AnimationStream stream, void* propertyStreamHandles, void* floatBuffer, int count, bool useMask);
	}
	[RequiredByNativeCode]
	[StaticAccessor("AnimatorControllerPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Animation/Director/AnimatorControllerPlayable.h")]
	[NativeHeader("Modules/Animation/RuntimeAnimatorController.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/Animator.bindings.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimatorControllerPlayable.bindings.h")]
	[NativeHeader("Modules/Animation/AnimatorInfo.h")]
	public struct AnimatorControllerPlayable : IPlayable, IEquatable<AnimatorControllerPlayable>
	{
		private PlayableHandle m_Handle;

		private static readonly AnimatorControllerPlayable m_NullPlayable = new AnimatorControllerPlayable(PlayableHandle.Null);

		public static AnimatorControllerPlayable Null => m_NullPlayable;

		public static AnimatorControllerPlayable Create(PlayableGraph graph, RuntimeAnimatorController controller)
		{
			PlayableHandle handle = CreateHandle(graph, controller);
			return new AnimatorControllerPlayable(handle);
		}

		private static PlayableHandle CreateHandle(PlayableGraph graph, RuntimeAnimatorController controller)
		{
			PlayableHandle handle = PlayableHandle.Null;
			if (!CreateHandleInternal(graph, controller, ref handle))
			{
				return PlayableHandle.Null;
			}
			return handle;
		}

		internal AnimatorControllerPlayable(PlayableHandle handle)
		{
			m_Handle = PlayableHandle.Null;
			SetHandle(handle);
		}

		public PlayableHandle GetHandle()
		{
			return m_Handle;
		}

		public void SetHandle(PlayableHandle handle)
		{
			if (m_Handle.IsValid())
			{
				throw new InvalidOperationException("Cannot call IPlayable.SetHandle on an instance that already contains a valid handle.");
			}
			if (handle.IsValid() && !handle.IsPlayableOfType<AnimatorControllerPlayable>())
			{
				throw new InvalidCastException("Can't set handle: the playable is not an AnimatorControllerPlayable.");
			}
			m_Handle = handle;
		}

		public static implicit operator Playable(AnimatorControllerPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		public static explicit operator AnimatorControllerPlayable(Playable playable)
		{
			return new AnimatorControllerPlayable(playable.GetHandle());
		}

		public bool Equals(AnimatorControllerPlayable other)
		{
			return GetHandle() == other.GetHandle();
		}

		public float GetFloat(string name)
		{
			return GetFloatString(ref m_Handle, name);
		}

		public float GetFloat(int id)
		{
			return GetFloatID(ref m_Handle, id);
		}

		public void SetFloat(string name, float value)
		{
			SetFloatString(ref m_Handle, name, value);
		}

		public void SetFloat(int id, float value)
		{
			SetFloatID(ref m_Handle, id, value);
		}

		public bool GetBool(string name)
		{
			return GetBoolString(ref m_Handle, name);
		}

		public bool GetBool(int id)
		{
			return GetBoolID(ref m_Handle, id);
		}

		public void SetBool(string name, bool value)
		{
			SetBoolString(ref m_Handle, name, value);
		}

		public void SetBool(int id, bool value)
		{
			SetBoolID(ref m_Handle, id, value);
		}

		public int GetInteger(string name)
		{
			return GetIntegerString(ref m_Handle, name);
		}

		public int GetInteger(int id)
		{
			return GetIntegerID(ref m_Handle, id);
		}

		public void SetInteger(string name, int value)
		{
			SetIntegerString(ref m_Handle, name, value);
		}

		public void SetInteger(int id, int value)
		{
			SetIntegerID(ref m_Handle, id, value);
		}

		public void SetTrigger(string name)
		{
			SetTriggerString(ref m_Handle, name);
		}

		public void SetTrigger(int id)
		{
			SetTriggerID(ref m_Handle, id);
		}

		public void ResetTrigger(string name)
		{
			ResetTriggerString(ref m_Handle, name);
		}

		public void ResetTrigger(int id)
		{
			ResetTriggerID(ref m_Handle, id);
		}

		public bool IsParameterControlledByCurve(string name)
		{
			return IsParameterControlledByCurveString(ref m_Handle, name);
		}

		public bool IsParameterControlledByCurve(int id)
		{
			return IsParameterControlledByCurveID(ref m_Handle, id);
		}

		public int GetLayerCount()
		{
			return GetLayerCountInternal(ref m_Handle);
		}

		public string GetLayerName(int layerIndex)
		{
			return GetLayerNameInternal(ref m_Handle, layerIndex);
		}

		public int GetLayerIndex(string layerName)
		{
			return GetLayerIndexInternal(ref m_Handle, layerName);
		}

		public float GetLayerWeight(int layerIndex)
		{
			return GetLayerWeightInternal(ref m_Handle, layerIndex);
		}

		public void SetLayerWeight(int layerIndex, float weight)
		{
			SetLayerWeightInternal(ref m_Handle, layerIndex, weight);
		}

		public AnimatorStateInfo GetCurrentAnimatorStateInfo(int layerIndex)
		{
			return GetCurrentAnimatorStateInfoInternal(ref m_Handle, layerIndex);
		}

		public AnimatorStateInfo GetNextAnimatorStateInfo(int layerIndex)
		{
			return GetNextAnimatorStateInfoInternal(ref m_Handle, layerIndex);
		}

		public AnimatorTransitionInfo GetAnimatorTransitionInfo(int layerIndex)
		{
			return GetAnimatorTransitionInfoInternal(ref m_Handle, layerIndex);
		}

		public AnimatorClipInfo[] GetCurrentAnimatorClipInfo(int layerIndex)
		{
			return GetCurrentAnimatorClipInfoInternal(ref m_Handle, layerIndex);
		}

		public void GetCurrentAnimatorClipInfo(int layerIndex, List<AnimatorClipInfo> clips)
		{
			if (clips == null)
			{
				throw new ArgumentNullException("clips");
			}
			GetAnimatorClipInfoInternal(ref m_Handle, layerIndex, isCurrent: true, clips);
		}

		public void GetNextAnimatorClipInfo(int layerIndex, List<AnimatorClipInfo> clips)
		{
			if (clips == null)
			{
				throw new ArgumentNullException("clips");
			}
			GetAnimatorClipInfoInternal(ref m_Handle, layerIndex, isCurrent: false, clips);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void GetAnimatorClipInfoInternal(ref PlayableHandle handle, int layerIndex, bool isCurrent, object clips);

		public int GetCurrentAnimatorClipInfoCount(int layerIndex)
		{
			return GetAnimatorClipInfoCountInternal(ref m_Handle, layerIndex, current: true);
		}

		public int GetNextAnimatorClipInfoCount(int layerIndex)
		{
			return GetAnimatorClipInfoCountInternal(ref m_Handle, layerIndex, current: false);
		}

		public AnimatorClipInfo[] GetNextAnimatorClipInfo(int layerIndex)
		{
			return GetNextAnimatorClipInfoInternal(ref m_Handle, layerIndex);
		}

		public bool IsInTransition(int layerIndex)
		{
			return IsInTransitionInternal(ref m_Handle, layerIndex);
		}

		public int GetParameterCount()
		{
			return GetParameterCountInternal(ref m_Handle);
		}

		public AnimatorControllerParameter GetParameter(int index)
		{
			AnimatorControllerParameter parameterInternal = GetParameterInternal(ref m_Handle, index);
			if (parameterInternal.m_Type == (AnimatorControllerParameterType)0)
			{
				throw new IndexOutOfRangeException("Invalid parameter index.");
			}
			return parameterInternal;
		}

		public void CrossFadeInFixedTime(string stateName, float transitionDuration)
		{
			CrossFadeInFixedTimeInternal(ref m_Handle, StringToHash(stateName), transitionDuration, -1, 0f);
		}

		public void CrossFadeInFixedTime(string stateName, float transitionDuration, int layer)
		{
			CrossFadeInFixedTimeInternal(ref m_Handle, StringToHash(stateName), transitionDuration, layer, 0f);
		}

		public void CrossFadeInFixedTime(string stateName, float transitionDuration, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("0.0f")] float fixedTime)
		{
			CrossFadeInFixedTimeInternal(ref m_Handle, StringToHash(stateName), transitionDuration, layer, fixedTime);
		}

		public void CrossFadeInFixedTime(int stateNameHash, float transitionDuration)
		{
			CrossFadeInFixedTimeInternal(ref m_Handle, stateNameHash, transitionDuration, -1, 0f);
		}

		public void CrossFadeInFixedTime(int stateNameHash, float transitionDuration, int layer)
		{
			CrossFadeInFixedTimeInternal(ref m_Handle, stateNameHash, transitionDuration, layer, 0f);
		}

		public void CrossFadeInFixedTime(int stateNameHash, float transitionDuration, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("0.0f")] float fixedTime)
		{
			CrossFadeInFixedTimeInternal(ref m_Handle, stateNameHash, transitionDuration, layer, fixedTime);
		}

		public void CrossFade(string stateName, float transitionDuration)
		{
			CrossFadeInternal(ref m_Handle, StringToHash(stateName), transitionDuration, -1, float.NegativeInfinity);
		}

		public void CrossFade(string stateName, float transitionDuration, int layer)
		{
			CrossFadeInternal(ref m_Handle, StringToHash(stateName), transitionDuration, layer, float.NegativeInfinity);
		}

		public void CrossFade(string stateName, float transitionDuration, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float normalizedTime)
		{
			CrossFadeInternal(ref m_Handle, StringToHash(stateName), transitionDuration, layer, normalizedTime);
		}

		public void CrossFade(int stateNameHash, float transitionDuration)
		{
			CrossFadeInternal(ref m_Handle, stateNameHash, transitionDuration, -1, float.NegativeInfinity);
		}

		public void CrossFade(int stateNameHash, float transitionDuration, int layer)
		{
			CrossFadeInternal(ref m_Handle, stateNameHash, transitionDuration, layer, float.NegativeInfinity);
		}

		public void CrossFade(int stateNameHash, float transitionDuration, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float normalizedTime)
		{
			CrossFadeInternal(ref m_Handle, stateNameHash, transitionDuration, layer, normalizedTime);
		}

		public void PlayInFixedTime(string stateName)
		{
			PlayInFixedTimeInternal(ref m_Handle, StringToHash(stateName), -1, float.NegativeInfinity);
		}

		public void PlayInFixedTime(string stateName, int layer)
		{
			PlayInFixedTimeInternal(ref m_Handle, StringToHash(stateName), layer, float.NegativeInfinity);
		}

		public void PlayInFixedTime(string stateName, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float fixedTime)
		{
			PlayInFixedTimeInternal(ref m_Handle, StringToHash(stateName), layer, fixedTime);
		}

		public void PlayInFixedTime(int stateNameHash)
		{
			PlayInFixedTimeInternal(ref m_Handle, stateNameHash, -1, float.NegativeInfinity);
		}

		public void PlayInFixedTime(int stateNameHash, int layer)
		{
			PlayInFixedTimeInternal(ref m_Handle, stateNameHash, layer, float.NegativeInfinity);
		}

		public void PlayInFixedTime(int stateNameHash, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float fixedTime)
		{
			PlayInFixedTimeInternal(ref m_Handle, stateNameHash, layer, fixedTime);
		}

		public void Play(string stateName)
		{
			PlayInternal(ref m_Handle, StringToHash(stateName), -1, float.NegativeInfinity);
		}

		public void Play(string stateName, int layer)
		{
			PlayInternal(ref m_Handle, StringToHash(stateName), layer, float.NegativeInfinity);
		}

		public void Play(string stateName, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float normalizedTime)
		{
			PlayInternal(ref m_Handle, StringToHash(stateName), layer, normalizedTime);
		}

		public void Play(int stateNameHash)
		{
			PlayInternal(ref m_Handle, stateNameHash, -1, float.NegativeInfinity);
		}

		public void Play(int stateNameHash, int layer)
		{
			PlayInternal(ref m_Handle, stateNameHash, layer, float.NegativeInfinity);
		}

		public void Play(int stateNameHash, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float normalizedTime)
		{
			PlayInternal(ref m_Handle, stateNameHash, layer, normalizedTime);
		}

		public bool HasState(int layerIndex, int stateID)
		{
			return HasStateInternal(ref m_Handle, layerIndex, stateID);
		}

		internal string ResolveHash(int hash)
		{
			return ResolveHashInternal(ref m_Handle, hash);
		}

		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, RuntimeAnimatorController controller, ref PlayableHandle handle)
		{
			return CreateHandleInternal_Injected(ref graph, controller, ref handle);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern RuntimeAnimatorController GetAnimatorControllerInternal(ref PlayableHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern int GetLayerCountInternal(ref PlayableHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern string GetLayerNameInternal(ref PlayableHandle handle, int layerIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern int GetLayerIndexInternal(ref PlayableHandle handle, string layerName);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern float GetLayerWeightInternal(ref PlayableHandle handle, int layerIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetLayerWeightInternal(ref PlayableHandle handle, int layerIndex, float weight);

		[NativeThrows]
		private static AnimatorStateInfo GetCurrentAnimatorStateInfoInternal(ref PlayableHandle handle, int layerIndex)
		{
			GetCurrentAnimatorStateInfoInternal_Injected(ref handle, layerIndex, out var ret);
			return ret;
		}

		[NativeThrows]
		private static AnimatorStateInfo GetNextAnimatorStateInfoInternal(ref PlayableHandle handle, int layerIndex)
		{
			GetNextAnimatorStateInfoInternal_Injected(ref handle, layerIndex, out var ret);
			return ret;
		}

		[NativeThrows]
		private static AnimatorTransitionInfo GetAnimatorTransitionInfoInternal(ref PlayableHandle handle, int layerIndex)
		{
			GetAnimatorTransitionInfoInternal_Injected(ref handle, layerIndex, out var ret);
			return ret;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern AnimatorClipInfo[] GetCurrentAnimatorClipInfoInternal(ref PlayableHandle handle, int layerIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern int GetAnimatorClipInfoCountInternal(ref PlayableHandle handle, int layerIndex, bool current);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern AnimatorClipInfo[] GetNextAnimatorClipInfoInternal(ref PlayableHandle handle, int layerIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern string ResolveHashInternal(ref PlayableHandle handle, int hash);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool IsInTransitionInternal(ref PlayableHandle handle, int layerIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern AnimatorControllerParameter GetParameterInternal(ref PlayableHandle handle, int index);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern int GetParameterCountInternal(ref PlayableHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[ThreadSafe]
		private static extern int StringToHash(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void CrossFadeInFixedTimeInternal(ref PlayableHandle handle, int stateNameHash, float transitionDuration, int layer, float fixedTime);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void CrossFadeInternal(ref PlayableHandle handle, int stateNameHash, float transitionDuration, int layer, float normalizedTime);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void PlayInFixedTimeInternal(ref PlayableHandle handle, int stateNameHash, int layer, float fixedTime);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void PlayInternal(ref PlayableHandle handle, int stateNameHash, int layer, float normalizedTime);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool HasStateInternal(ref PlayableHandle handle, int layerIndex, int stateID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetFloatString(ref PlayableHandle handle, string name, float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetFloatID(ref PlayableHandle handle, int id, float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern float GetFloatString(ref PlayableHandle handle, string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern float GetFloatID(ref PlayableHandle handle, int id);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetBoolString(ref PlayableHandle handle, string name, bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetBoolID(ref PlayableHandle handle, int id, bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool GetBoolString(ref PlayableHandle handle, string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool GetBoolID(ref PlayableHandle handle, int id);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetIntegerString(ref PlayableHandle handle, string name, int value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetIntegerID(ref PlayableHandle handle, int id, int value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern int GetIntegerString(ref PlayableHandle handle, string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern int GetIntegerID(ref PlayableHandle handle, int id);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetTriggerString(ref PlayableHandle handle, string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetTriggerID(ref PlayableHandle handle, int id);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void ResetTriggerString(ref PlayableHandle handle, string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void ResetTriggerID(ref PlayableHandle handle, int id);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool IsParameterControlledByCurveString(ref PlayableHandle handle, string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool IsParameterControlledByCurveID(ref PlayableHandle handle, int id);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected(ref PlayableGraph graph, RuntimeAnimatorController controller, ref PlayableHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCurrentAnimatorStateInfoInternal_Injected(ref PlayableHandle handle, int layerIndex, out AnimatorStateInfo ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetNextAnimatorStateInfoInternal_Injected(ref PlayableHandle handle, int layerIndex, out AnimatorStateInfo ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAnimatorTransitionInfoInternal_Injected(ref PlayableHandle handle, int layerIndex, out AnimatorTransitionInfo ret);
	}
	[MovedFrom("UnityEngine.Experimental.Animations")]
	public enum CustomStreamPropertyType
	{
		Float = 5,
		Bool = 6,
		Int = 10
	}
	[NativeHeader("Modules/Animation/Director/AnimationSceneHandles.h")]
	[NativeHeader("Modules/Animation/Director/AnimationStream.h")]
	[StaticAccessor("AnimatorJobExtensionsBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Animation/Director/AnimationStreamHandles.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimatorJobExtensions.bindings.h")]
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Modules/Animation/Animator.h")]
	public static class AnimatorJobExtensions
	{
		public static void AddJobDependency(this Animator animator, JobHandle jobHandle)
		{
			InternalAddJobDependency(animator, jobHandle);
		}

		public static TransformStreamHandle BindStreamTransform(this Animator animator, Transform transform)
		{
			TransformStreamHandle transformStreamHandle = default(TransformStreamHandle);
			InternalBindStreamTransform(animator, transform, out transformStreamHandle);
			return transformStreamHandle;
		}

		public static PropertyStreamHandle BindStreamProperty(this Animator animator, Transform transform, Type type, string property)
		{
			return animator.BindStreamProperty(transform, type, property, isObjectReference: false);
		}

		public static PropertyStreamHandle BindCustomStreamProperty(this Animator animator, string property, CustomStreamPropertyType type)
		{
			PropertyStreamHandle propertyStreamHandle = default(PropertyStreamHandle);
			InternalBindCustomStreamProperty(animator, property, type, out propertyStreamHandle);
			return propertyStreamHandle;
		}

		public static PropertyStreamHandle BindStreamProperty(this Animator animator, Transform transform, Type type, string property, [UnityEngine.Internal.DefaultValue("false")] bool isObjectReference)
		{
			PropertyStreamHandle propertyStreamHandle = default(PropertyStreamHandle);
			InternalBindStreamProperty(animator, transform, type, property, isObjectReference, out propertyStreamHandle);
			return propertyStreamHandle;
		}

		public static TransformSceneHandle BindSceneTransform(this Animator animator, Transform transform)
		{
			TransformSceneHandle transformSceneHandle = default(TransformSceneHandle);
			InternalBindSceneTransform(animator, transform, out transformSceneHandle);
			return transformSceneHandle;
		}

		public static PropertySceneHandle BindSceneProperty(this Animator animator, Transform transform, Type type, string property)
		{
			return animator.BindSceneProperty(transform, type, property, isObjectReference: false);
		}

		public static PropertySceneHandle BindSceneProperty(this Animator animator, Transform transform, Type type, string property, [UnityEngine.Internal.DefaultValue("false")] bool isObjectReference)
		{
			PropertySceneHandle propertySceneHandle = default(PropertySceneHandle);
			InternalBindSceneProperty(animator, transform, type, property, isObjectReference, out propertySceneHandle);
			return propertySceneHandle;
		}

		public static bool OpenAnimationStream(this Animator animator, ref AnimationStream stream)
		{
			return InternalOpenAnimationStream(animator, ref stream);
		}

		public static void CloseAnimationStream(this Animator animator, ref AnimationStream stream)
		{
			InternalCloseAnimationStream(animator, ref stream);
		}

		public static void ResolveAllStreamHandles(this Animator animator)
		{
			InternalResolveAllStreamHandles(animator);
		}

		public static void ResolveAllSceneHandles(this Animator animator)
		{
			InternalResolveAllSceneHandles(animator);
		}

		internal static void UnbindAllHandles(this Animator animator)
		{
			InternalUnbindAllStreamHandles(animator);
			InternalUnbindAllSceneHandles(animator);
		}

		public static void UnbindAllStreamHandles(this Animator animator)
		{
			InternalUnbindAllStreamHandles(animator);
		}

		public static void UnbindAllSceneHandles(this Animator animator)
		{
			InternalUnbindAllSceneHandles(animator);
		}

		private static void InternalAddJobDependency([NotNull("ArgumentNullException")] Animator animator, JobHandle jobHandle)
		{
			InternalAddJobDependency_Injected(animator, ref jobHandle);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalBindStreamTransform([NotNull("ArgumentNullException")] Animator animator, [NotNull("ArgumentNullException")] Transform transform, out TransformStreamHandle transformStreamHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalBindStreamProperty([NotNull("ArgumentNullException")] Animator animator, [NotNull("ArgumentNullException")] Transform transform, [NotNull("ArgumentNullException")] Type type, [NotNull("ArgumentNullException")] string property, bool isObjectReference, out PropertyStreamHandle propertyStreamHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalBindCustomStreamProperty([NotNull("ArgumentNullException")] Animator animator, [NotNull("ArgumentNullException")] string property, CustomStreamPropertyType propertyType, out PropertyStreamHandle propertyStreamHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalBindSceneTransform([NotNull("ArgumentNullException")] Animator animator, [NotNull("ArgumentNullException")] Transform transform, out TransformSceneHandle transformSceneHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalBindSceneProperty([NotNull("ArgumentNullException")] Animator animator, [NotNull("ArgumentNullException")] Transform transform, [NotNull("ArgumentNullException")] Type type, [NotNull("ArgumentNullException")] string property, bool isObjectReference, out PropertySceneHandle propertySceneHandle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalOpenAnimationStream([NotNull("ArgumentNullException")] Animator animator, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalCloseAnimationStream([NotNull("ArgumentNullException")] Animator animator, ref AnimationStream stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalResolveAllStreamHandles([NotNull("ArgumentNullException")] Animator animator);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalResolveAllSceneHandles([NotNull("ArgumentNullException")] Animator animator);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalUnbindAllStreamHandles([NotNull("ArgumentNullException")] Animator animator);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalUnbindAllSceneHandles([NotNull("ArgumentNullException")] Animator animator);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalAddJobDependency_Injected(Animator animator, ref JobHandle jobHandle);
	}
	[NativeType("Modules/Animation/Constraints/ConstraintEnums.h")]
	[Flags]
	public enum Axis
	{
		None = 0,
		X = 1,
		Y = 2,
		Z = 4
	}
	[Serializable]
	[UsedByNativeCode]
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	[NativeType(CodegenOptions = CodegenOptions.Custom, Header = "Modules/Animation/Constraints/ConstraintSource.h", IntermediateScriptingStructName = "MonoConstraintSource")]
	public struct ConstraintSource
	{
		[NativeName("sourceTransform")]
		private Transform m_SourceTransform;

		[NativeName("weight")]
		private float m_Weight;

		public Transform sourceTransform
		{
			get
			{
				return m_SourceTransform;
			}
			set
			{
				m_SourceTransform = value;
			}
		}

		public float weight
		{
			get
			{
				return m_Weight;
			}
			set
			{
				m_Weight = value;
			}
		}
	}
	public interface IConstraint
	{
		float weight { get; set; }

		bool constraintActive { get; set; }

		bool locked { get; set; }

		int sourceCount { get; }

		int AddSource(ConstraintSource source);

		void RemoveSource(int index);

		ConstraintSource GetSource(int index);

		void SetSource(int index, ConstraintSource source);

		void GetSources(List<ConstraintSource> sources);

		void SetSources(List<ConstraintSource> sources);
	}
	internal interface IConstraintInternal
	{
	}
	[UsedByNativeCode]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Animation/Constraints/PositionConstraint.h")]
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	public sealed class PositionConstraint : Behaviour, IConstraint, IConstraintInternal
	{
		public extern float weight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 translationAtRest
		{
			get
			{
				get_translationAtRest_Injected(out var ret);
				return ret;
			}
			set
			{
				set_translationAtRest_Injected(ref value);
			}
		}

		public Vector3 translationOffset
		{
			get
			{
				get_translationOffset_Injected(out var ret);
				return ret;
			}
			set
			{
				set_translationOffset_Injected(ref value);
			}
		}

		public extern Axis translationAxis
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool constraintActive
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool locked
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public int sourceCount => GetSourceCountInternal(this);

		private PositionConstraint()
		{
			Internal_Create(this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] PositionConstraint self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ConstraintBindings::GetSourceCount")]
		private static extern int GetSourceCountInternal([NotNull("ArgumentNullException")] PositionConstraint self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ConstraintBindings::GetSources", HasExplicitThis = true)]
		public extern void GetSources([NotNull("ArgumentNullException")] List<ConstraintSource> sources);

		public void SetSources(List<ConstraintSource> sources)
		{
			if (sources == null)
			{
				throw new ArgumentNullException("sources");
			}
			SetSourcesInternal(this, sources);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ConstraintBindings::SetSources", ThrowsException = true)]
		private static extern void SetSourcesInternal([NotNull("ArgumentNullException")] PositionConstraint self, List<ConstraintSource> sources);

		public int AddSource(ConstraintSource source)
		{
			return AddSource_Injected(ref source);
		}

		public void RemoveSource(int index)
		{
			ValidateSourceIndex(index);
			RemoveSourceInternal(index);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("RemoveSource")]
		private extern void RemoveSourceInternal(int index);

		public ConstraintSource GetSource(int index)
		{
			ValidateSourceIndex(index);
			return GetSourceInternal(index);
		}

		[NativeName("GetSource")]
		private ConstraintSource GetSourceInternal(int index)
		{
			GetSourceInternal_Injected(index, out var ret);
			return ret;
		}

		public void SetSource(int index, ConstraintSource source)
		{
			ValidateSourceIndex(index);
			SetSourceInternal(index, source);
		}

		[NativeName("SetSource")]
		private void SetSourceInternal(int index, ConstraintSource source)
		{
			SetSourceInternal_Injected(index, ref source);
		}

		private void ValidateSourceIndex(int index)
		{
			if (sourceCount == 0)
			{
				throw new InvalidOperationException("The PositionConstraint component has no sources.");
			}
			if (index < 0 || index >= sourceCount)
			{
				throw new ArgumentOutOfRangeException("index", $"Constraint source index {index} is out of bounds (0-{sourceCount}).");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_translationAtRest_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_translationAtRest_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_translationOffset_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_translationOffset_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int AddSource_Injected(ref ConstraintSource source);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetSourceInternal_Injected(int index, out ConstraintSource ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetSourceInternal_Injected(int index, ref ConstraintSource source);
	}
	[NativeHeader("Modules/Animation/Constraints/RotationConstraint.h")]
	[UsedByNativeCode]
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	[RequireComponent(typeof(Transform))]
	public sealed class RotationConstraint : Behaviour, IConstraint, IConstraintInternal
	{
		public extern float weight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 rotationAtRest
		{
			get
			{
				get_rotationAtRest_Injected(out var ret);
				return ret;
			}
			set
			{
				set_rotationAtRest_Injected(ref value);
			}
		}

		public Vector3 rotationOffset
		{
			get
			{
				get_rotationOffset_Injected(out var ret);
				return ret;
			}
			set
			{
				set_rotationOffset_Injected(ref value);
			}
		}

		public extern Axis rotationAxis
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool constraintActive
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool locked
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public int sourceCount => GetSourceCountInternal(this);

		private RotationConstraint()
		{
			Internal_Create(this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] RotationConstraint self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ConstraintBindings::GetSourceCount")]
		private static extern int GetSourceCountInternal([NotNull("ArgumentNullException")] RotationConstraint self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ConstraintBindings::GetSources", HasExplicitThis = true)]
		public extern void GetSources([NotNull("ArgumentNullException")] List<ConstraintSource> sources);

		public void SetSources(List<ConstraintSource> sources)
		{
			if (sources == null)
			{
				throw new ArgumentNullException("sources");
			}
			SetSourcesInternal(this, sources);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ConstraintBindings::SetSources", ThrowsException = true)]
		private static extern void SetSourcesInternal([NotNull("ArgumentNullException")] RotationConstraint self, List<ConstraintSource> sources);

		public int AddSource(ConstraintSource source)
		{
			return AddSource_Injected(ref source);
		}

		public void RemoveSource(int index)
		{
			ValidateSourceIndex(index);
			RemoveSourceInternal(index);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("RemoveSource")]
		private extern void RemoveSourceInternal(int index);

		public ConstraintSource GetSource(int index)
		{
			ValidateSourceIndex(index);
			return GetSourceInternal(index);
		}

		[NativeName("GetSource")]
		private ConstraintSource GetSourceInternal(int index)
		{
			GetSourceInternal_Injected(index, out var ret);
			return ret;
		}

		public void SetSource(int index, ConstraintSource source)
		{
			ValidateSourceIndex(index);
			SetSourceInternal(index, source);
		}

		[NativeName("SetSource")]
		private void SetSourceInternal(int index, ConstraintSource source)
		{
			SetSourceInternal_Injected(index, ref source);
		}

		private void ValidateSourceIndex(int index)
		{
			if (sourceCount == 0)
			{
				throw new InvalidOperationException("The RotationConstraint component has no sources.");
			}
			if (index < 0 || index >= sourceCount)
			{
				throw new ArgumentOutOfRangeException("index", $"Constraint source index {index} is out of bounds (0-{sourceCount}).");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_rotationAtRest_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_rotationAtRest_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_rotationOffset_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_rotationOffset_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int AddSource_Injected(ref ConstraintSource source);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetSourceInternal_Injected(int index, out ConstraintSource ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetSourceInternal_Injected(int index, ref ConstraintSource source);
	}
	[NativeHeader("Modules/Animation/Constraints/ScaleConstraint.h")]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	[UsedByNativeCode]
	public sealed class ScaleConstraint : Behaviour, IConstraint, IConstraintInternal
	{
		public extern float weight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 scaleAtRest
		{
			get
			{
				get_scaleAtRest_Injected(out var ret);
				return ret;
			}
			set
			{
				set_scaleAtRest_Injected(ref value);
			}
		}

		public Vector3 scaleOffset
		{
			get
			{
				get_scaleOffset_Injected(out var ret);
				return ret;
			}
			set
			{
				set_scaleOffset_Injected(ref value);
			}
		}

		public extern Axis scalingAxis
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool constraintActive
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool locked
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public int sourceCount => GetSourceCountInternal(this);

		private ScaleConstraint()
		{
			Internal_Create(this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] ScaleConstraint self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ConstraintBindings::GetSourceCount")]
		private static extern int GetSourceCountInternal([NotNull("ArgumentNullException")] ScaleConstraint self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ConstraintBindings::GetSources", HasExplicitThis = true)]
		public extern void GetSources([NotNull("ArgumentNullException")] List<ConstraintSource> sources);

		public void SetSources(List<ConstraintSource> sources)
		{
			if (sources == null)
			{
				throw new ArgumentNullException("sources");
			}
			SetSourcesInternal(this, sources);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ConstraintBindings::SetSources", ThrowsException = true)]
		private static extern void SetSourcesInternal([NotNull("ArgumentNullException")] ScaleConstraint self, List<ConstraintSource> sources);

		public int AddSource(ConstraintSource source)
		{
			return AddSource_Injected(ref source);
		}

		public void RemoveSource(int index)
		{
			ValidateSourceIndex(index);
			RemoveSourceInternal(index);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("RemoveSource")]
		private extern void RemoveSourceInternal(int index);

		public ConstraintSource GetSource(int index)
		{
			ValidateSourceIndex(index);
			return GetSourceInternal(index);
		}

		[NativeName("GetSource")]
		private ConstraintSource GetSourceInternal(int index)
		{
			GetSourceInternal_Injected(index, out var ret);
			return ret;
		}

		public void SetSource(int index, ConstraintSource source)
		{
			ValidateSourceIndex(index);
			SetSourceInternal(index, source);
		}

		[NativeName("SetSource")]
		private void SetSourceInternal(int index, ConstraintSource source)
		{
			SetSourceInternal_Injected(index, ref source);
		}

		private void ValidateSourceIndex(int index)
		{
			if (sourceCount == 0)
			{
				throw new InvalidOperationException("The ScaleConstraint component has no sources.");
			}
			if (index < 0 || index >= sourceCount)
			{
				throw new ArgumentOutOfRangeException("index", $"Constraint source index {index} is out of bounds (0-{sourceCount}).");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_scaleAtRest_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_scaleAtRest_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_scaleOffset_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_scaleOffset_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int AddSource_Injected(ref ConstraintSource source);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetSourceInternal_Injected(int index, out ConstraintSource ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetSourceInternal_Injected(int index, ref ConstraintSource source);
	}
	[UsedByNativeCode]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Animation/Constraints/LookAtConstraint.h")]
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	public sealed class LookAtConstraint : Behaviour, IConstraint, IConstraintInternal
	{
		public extern float weight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float roll
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool constraintActive
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool locked
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public Vector3 rotationAtRest
		{
			get
			{
				get_rotationAtRest_Injected(out var ret);
				return ret;
			}
			set
			{
				set_rotationAtRest_Injected(ref value);
			}
		}

		public Vector3 rotationOffset
		{
			get
			{
				get_rotationOffset_Injected(out var ret);
				return ret;
			}
			set
			{
				set_rotationOffset_Injected(ref value);
			}
		}

		public extern Transform worldUpObject
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool useUpObject
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public int sourceCount => GetSourceCountInternal(this);

		private LookAtConstraint()
		{
			Internal_Create(this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] LookAtConstraint self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ConstraintBindings::GetSourceCount")]
		private static extern int GetSourceCountInternal([NotNull("ArgumentNullException")] LookAtConstraint self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ConstraintBindings::GetSources", HasExplicitThis = true)]
		public extern void GetSources([NotNull("ArgumentNullException")] List<ConstraintSource> sources);

		public void SetSources(List<ConstraintSource> sources)
		{
			if (sources == null)
			{
				throw new ArgumentNullException("sources");
			}
			SetSourcesInternal(this, sources);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ConstraintBindings::SetSources", ThrowsException = true)]
		private static extern void SetSourcesInternal([NotNull("ArgumentNullException")] LookAtConstraint self, List<ConstraintSource> sources);

		public int AddSource(ConstraintSource source)
		{
			return AddSource_Injected(ref source);
		}

		public void RemoveSource(int index)
		{
			ValidateSourceIndex(index);
			RemoveSourceInternal(index);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("RemoveSource")]
		private extern void RemoveSourceInternal(int index);

		public ConstraintSource GetSource(int index)
		{
			ValidateSourceIndex(index);
			return GetSourceInternal(index);
		}

		[NativeName("GetSource")]
		private ConstraintSource GetSourceInternal(int index)
		{
			GetSourceInternal_Injected(index, out var ret);
			return ret;
		}

		public void SetSource(int index, ConstraintSource source)
		{
			ValidateSourceIndex(index);
			SetSourceInternal(index, source);
		}

		[NativeName("SetSource")]
		private void SetSourceInternal(int index, ConstraintSource source)
		{
			SetSourceInternal_Injected(index, ref source);
		}

		private void ValidateSourceIndex(int index)
		{
			if (sourceCount == 0)
			{
				throw new InvalidOperationException("The LookAtConstraint component has no sources.");
			}
			if (index < 0 || index >= sourceCount)
			{
				throw new ArgumentOutOfRangeException("index", $"Constraint source index {index} is out of bounds (0-{sourceCount}).");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_rotationAtRest_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_rotationAtRest_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_rotationOffset_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_rotationOffset_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int AddSource_Injected(ref ConstraintSource source);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetSourceInternal_Injected(int index, out ConstraintSource ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetSourceInternal_Injected(int index, ref ConstraintSource source);
	}
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Modules/Animation/Animator.h")]
	[NativeHeader("Modules/Animation/MuscleHandle.h")]
	public struct MuscleHandle
	{
		public HumanPartDof humanPartDof { get; private set; }

		public int dof { get; private set; }

		public string name => GetName();

		public static int muscleHandleCount => GetMuscleHandleCount();

		public MuscleHandle(BodyDof bodyDof)
		{
			humanPartDof = HumanPartDof.Body;
			dof = (int)bodyDof;
		}

		public MuscleHandle(HeadDof headDof)
		{
			humanPartDof = HumanPartDof.Head;
			dof = (int)headDof;
		}

		public MuscleHandle(HumanPartDof partDof, LegDof legDof)
		{
			if (partDof != HumanPartDof.LeftLeg && partDof != HumanPartDof.RightLeg)
			{
				throw new InvalidOperationException("Invalid HumanPartDof for a leg, please use either HumanPartDof.LeftLeg or HumanPartDof.RightLeg.");
			}
			humanPartDof = partDof;
			dof = (int)legDof;
		}

		public MuscleHandle(HumanPartDof partDof, ArmDof armDof)
		{
			if (partDof != HumanPartDof.LeftArm && partDof != HumanPartDof.RightArm)
			{
				throw new InvalidOperationException("Invalid HumanPartDof for an arm, please use either HumanPartDof.LeftArm or HumanPartDof.RightArm.");
			}
			humanPartDof = partDof;
			dof = (int)armDof;
		}

		public MuscleHandle(HumanPartDof partDof, FingerDof fingerDof)
		{
			if (partDof < HumanPartDof.LeftThumb || partDof > HumanPartDof.RightLittle)
			{
				throw new InvalidOperationException("Invalid HumanPartDof for a finger.");
			}
			humanPartDof = partDof;
			dof = (int)fingerDof;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void GetMuscleHandles([Out][NotNull("ArgumentNullException")] MuscleHandle[] muscleHandles);

		private string GetName()
		{
			return GetName_Injected(ref this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMuscleHandleCount();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetName_Injected(ref MuscleHandle _unity_self);
	}
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	[UsedByNativeCode]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Animation/Constraints/ParentConstraint.h")]
	public sealed class ParentConstraint : Behaviour, IConstraint, IConstraintInternal
	{
		public extern float weight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool constraintActive
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool locked
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public int sourceCount => GetSourceCountInternal(this);

		public Vector3 translationAtRest
		{
			get
			{
				get_translationAtRest_Injected(out var ret);
				return ret;
			}
			set
			{
				set_translationAtRest_Injected(ref value);
			}
		}

		public Vector3 rotationAtRest
		{
			get
			{
				get_rotationAtRest_Injected(out var ret);
				return ret;
			}
			set
			{
				set_rotationAtRest_Injected(ref value);
			}
		}

		public extern Vector3[] translationOffsets
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Vector3[] rotationOffsets
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Axis translationAxis
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Axis rotationAxis
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		private ParentConstraint()
		{
			Internal_Create(this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] ParentConstraint self);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ConstraintBindings::GetSourceCount")]
		private static extern int GetSourceCountInternal([NotNull("ArgumentNullException")] ParentConstraint self);

		public Vector3 GetTranslationOffset(int index)
		{
			ValidateSourceIndex(index);
			return GetTranslationOffsetInternal(index);
		}

		public void SetTranslationOffset(int index, Vector3 value)
		{
			ValidateSourceIndex(index);
			SetTranslationOffsetInternal(index, value);
		}

		[NativeName("GetTranslationOffset")]
		private Vector3 GetTranslationOffsetInternal(int index)
		{
			GetTranslationOffsetInternal_Injected(index, out var ret);
			return ret;
		}

		[NativeName("SetTranslationOffset")]
		private void SetTranslationOffsetInternal(int index, Vector3 value)
		{
			SetTranslationOffsetInternal_Injected(index, ref value);
		}

		public Vector3 GetRotationOffset(int index)
		{
			ValidateSourceIndex(index);
			return GetRotationOffsetInternal(index);
		}

		public void SetRotationOffset(int index, Vector3 value)
		{
			ValidateSourceIndex(index);
			SetRotationOffsetInternal(index, value);
		}

		[NativeName("GetRotationOffset")]
		private Vector3 GetRotationOffsetInternal(int index)
		{
			GetRotationOffsetInternal_Injected(index, out var ret);
			return ret;
		}

		[NativeName("SetRotationOffset")]
		private void SetRotationOffsetInternal(int index, Vector3 value)
		{
			SetRotationOffsetInternal_Injected(index, ref value);
		}

		private void ValidateSourceIndex(int index)
		{
			if (sourceCount == 0)
			{
				throw new InvalidOperationException("The ParentConstraint component has no sources.");
			}
			if (index < 0 || index >= sourceCount)
			{
				throw new ArgumentOutOfRangeException("index", $"Constraint source index {index} is out of bounds (0-{sourceCount}).");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(Name = "ConstraintBindings::GetSources", HasExplicitThis = true)]
		public extern void GetSources([NotNull("ArgumentNullException")] List<ConstraintSource> sources);

		public void SetSources(List<ConstraintSource> sources)
		{
			if (sources == null)
			{
				throw new ArgumentNullException("sources");
			}
			SetSourcesInternal(this, sources);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("ConstraintBindings::SetSources", ThrowsException = true)]
		private static extern void SetSourcesInternal([NotNull("ArgumentNullException")] ParentConstraint self, List<ConstraintSource> sources);

		public int AddSource(ConstraintSource source)
		{
			return AddSource_Injected(ref source);
		}

		public void RemoveSource(int index)
		{
			ValidateSourceIndex(index);
			RemoveSourceInternal(index);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("RemoveSource")]
		private extern void RemoveSourceInternal(int index);

		public ConstraintSource GetSource(int index)
		{
			ValidateSourceIndex(index);
			return GetSourceInternal(index);
		}

		[NativeName("GetSource")]
		private ConstraintSource GetSourceInternal(int index)
		{
			GetSourceInternal_Injected(index, out var ret);
			return ret;
		}

		public void SetSource(int index, ConstraintSource source)
		{
			ValidateSourceIndex(index);
			SetSourceInternal(index, source);
		}

		[NativeName("SetSource")]
		private void SetSourceInternal(int index, ConstraintSource source)
		{
			SetSourceInternal_Injected(index, ref source);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_translationAtRest_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_translationAtRest_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_rotationAtRest_Injected(out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_rotationAtRest_Injected(ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetTranslationOffsetInternal_Injected(int index, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTranslationOffsetInternal_Injected(int index, ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetRotationOffsetInternal_Injected(int index, out Vector3 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetRotationOffsetInternal_Injected(int index, ref Vector3 value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int AddSource_Injected(ref ConstraintSource source);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetSourceInternal_Injected(int index, out ConstraintSource ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetSourceInternal_Injected(int index, ref ConstraintSource source);
	}
}
