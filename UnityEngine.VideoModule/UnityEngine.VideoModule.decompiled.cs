using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Audio;
using UnityEngine.Playables;
using UnityEngine.Scripting;
using UnityEngine.Video;

[assembly: InternalsVisibleTo("UnityEngine.UnityTestProtocolModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestTextureModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestWWWModule")]
[assembly: InternalsVisibleTo("UnityEngine.VFXModule")]
[assembly: InternalsVisibleTo("UnityEngine.XRModule")]
[assembly: InternalsVisibleTo("UnityEngine.VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.VehiclesModule")]
[assembly: InternalsVisibleTo("UnityEngine.VideoModule")]
[assembly: InternalsVisibleTo("UnityEngine.VirtualTexturingModule")]
[assembly: InternalsVisibleTo("UnityEngine.WindModule")]
[assembly: InternalsVisibleTo("UnityEngine.SwitchModule")]
[assembly: InternalsVisibleTo("UnityEngine.XboxOneModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS4Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS4VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS5Module")]
[assembly: InternalsVisibleTo("Unity.DeploymentTests.Services")]
[assembly: InternalsVisibleTo("Unity.Burst.Editor")]
[assembly: InternalsVisibleTo("Unity.Burst")]
[assembly: InternalsVisibleTo("Unity.Automation")]
[assembly: InternalsVisibleTo("UnityEngine.TestRunner")]
[assembly: InternalsVisibleTo("UnityEngine.Purchasing")]
[assembly: InternalsVisibleTo("UnityEngine.UnityCurlModule")]
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
[assembly: InternalsVisibleTo("UnityEngine.Analytics")]
[assembly: InternalsVisibleTo("Unity.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud.Service")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud")]
[assembly: InternalsVisibleTo("UnityEngine.Networking")]
[assembly: InternalsVisibleTo("UnityEngine.PS5VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommon")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests")]
[assembly: InternalsVisibleTo("UnityEngine.UmbraModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsModule")]
[assembly: InternalsVisibleTo("UnityEngine.CrashReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.DSPGraphModule")]
[assembly: InternalsVisibleTo("UnityEngine.DirectorModule")]
[assembly: InternalsVisibleTo("UnityEngine.GIModule")]
[assembly: InternalsVisibleTo("UnityEngine.ImageConversionModule")]
[assembly: InternalsVisibleTo("UnityEngine.GameCenterModule")]
[assembly: InternalsVisibleTo("UnityEngine.GridModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextRenderingModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputLegacyModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreFontEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreTextEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.IMGUIModule")]
[assembly: InternalsVisibleTo("UnityEngine.LocalizationModule")]
[assembly: InternalsVisibleTo("UnityEngine.NVIDIAModule")]
[assembly: InternalsVisibleTo("UnityEngine.ParticleSystemModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.SpriteMaskModule")]
[assembly: InternalsVisibleTo("UnityEngine.TLSModule")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.UnityAnalytics")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Framework")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.006")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.007")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.008")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.009")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.010")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.011")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.012")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.013")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.014")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.015")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.016")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.017")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.018")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.019")]
[assembly: InternalsVisibleTo("Unity.Audio.DSPGraph")]
[assembly: InternalsVisibleTo("VideoTesting")]
[assembly: UnityEngineModuleAssembly]
[assembly: InternalsVisibleTo("Unity.Subsystem.Registration")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.001")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.024")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.023")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.022")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.021")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.020")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.002")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Timeline")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.001")]
[assembly: InternalsVisibleTo("Unity.Core")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework.Tests")]
[assembly: InternalsVisibleTo("Unity.PerformanceTests.RuntimeTestRunner.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.AllIn1Runner")]
[assembly: InternalsVisibleTo("Unity.Timeline")]
[assembly: InternalsVisibleTo("Assembly-CSharp-testable")]
[assembly: InternalsVisibleTo("Assembly-CSharp-firstpass-testable")]
[assembly: InternalsVisibleTo("UnityEngine.SpatialTracking")]
[assembly: InternalsVisibleTo("GoogleAR.UnityNative")]
[assembly: InternalsVisibleTo("Unity.WindowsMRAutomation")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.Editor")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.EditorTests")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.Editor")]
[assembly: InternalsVisibleTo("UnityEditor.UIBuilderModule")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.EditorTests")]
[assembly: InternalsVisibleTo("Unity.UIElements")]
[assembly: InternalsVisibleTo("Unity.Runtime")]
[assembly: InternalsVisibleTo("Unity.Collections")]
[assembly: InternalsVisibleTo("Unity.Entities.Tests")]
[assembly: InternalsVisibleTo("Unity.Entities")]
[assembly: InternalsVisibleTo("Unity.Logging")]
[assembly: InternalsVisibleTo("Unity.Services.QoS")]
[assembly: InternalsVisibleTo("UnityEngine.Core.Runtime.Tests")]
[assembly: InternalsVisibleTo("Unity.ucg.QoS")]
[assembly: InternalsVisibleTo("UnityEngine.UI")]
[assembly: InternalsVisibleTo("Unity.UIElements.EditorTests")]
[assembly: InternalsVisibleTo("UnityEngine.UIElements.Tests")]
[assembly: InternalsVisibleTo("Unity.UIElements.PlayModeTests")]
[assembly: InternalsVisibleTo("Unity.UIElements.Editor")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsGameObjectsModule")]
[assembly: InternalsVisibleTo("Unity.Networking.Transport")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommonModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityConnectModule")]
[assembly: InternalsVisibleTo("UnityEngine.ContentLoadModule")]
[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: InternalsVisibleTo("UnityEngine")]
[assembly: InternalsVisibleTo("UnityEngine.SharedInternalsModule")]
[assembly: InternalsVisibleTo("UnityEngine.CoreModule")]
[assembly: InternalsVisibleTo("UnityEngine.AIModule")]
[assembly: InternalsVisibleTo("UnityEngine.PhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.JSONSerializeModule")]
[assembly: InternalsVisibleTo("Unity.Audio.DSPGraph.Tests")]
[assembly: InternalsVisibleTo("UnityEngine.ARModule")]
[assembly: InternalsVisibleTo("UnityEngine.AccessibilityModule")]
[assembly: InternalsVisibleTo("UnityEngine.AndroidJNIModule")]
[assembly: InternalsVisibleTo("UnityEngine.AnimationModule")]
[assembly: InternalsVisibleTo("UnityEngine.HotReloadModule")]
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.AudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClothModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterRendererModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputModule")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace UnityEngine.Experimental.Video
{
	[StaticAccessor("VideoClipPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[RequiredByNativeCode]
	[NativeHeader("Modules/Video/Public/Director/VideoClipPlayable.h")]
	[NativeHeader("Modules/Video/Public/VideoClip.h")]
	[NativeHeader("Modules/Video/Public/ScriptBindings/VideoClipPlayable.bindings.h")]
	public struct VideoClipPlayable : IPlayable, IEquatable<VideoClipPlayable>
	{
		private PlayableHandle m_Handle;

		public static VideoClipPlayable Create(PlayableGraph graph, VideoClip clip, bool looping)
		{
			PlayableHandle handle = CreateHandle(graph, clip, looping);
			VideoClipPlayable videoClipPlayable = new VideoClipPlayable(handle);
			if (clip != null)
			{
				videoClipPlayable.SetDuration(clip.length);
			}
			return videoClipPlayable;
		}

		private static PlayableHandle CreateHandle(PlayableGraph graph, VideoClip clip, bool looping)
		{
			PlayableHandle handle = PlayableHandle.Null;
			if (!InternalCreateVideoClipPlayable(ref graph, clip, looping, ref handle))
			{
				return PlayableHandle.Null;
			}
			return handle;
		}

		internal VideoClipPlayable(PlayableHandle handle)
		{
			if (handle.IsValid() && !handle.IsPlayableOfType<VideoClipPlayable>())
			{
				throw new InvalidCastException("Can't set handle: the playable is not an VideoClipPlayable.");
			}
			m_Handle = handle;
		}

		public PlayableHandle GetHandle()
		{
			return m_Handle;
		}

		public static implicit operator Playable(VideoClipPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		public static explicit operator VideoClipPlayable(Playable playable)
		{
			return new VideoClipPlayable(playable.GetHandle());
		}

		public bool Equals(VideoClipPlayable other)
		{
			return GetHandle() == other.GetHandle();
		}

		public VideoClip GetClip()
		{
			return GetClipInternal(ref m_Handle);
		}

		public void SetClip(VideoClip value)
		{
			SetClipInternal(ref m_Handle, value);
		}

		public bool GetLooped()
		{
			return GetLoopedInternal(ref m_Handle);
		}

		public void SetLooped(bool value)
		{
			SetLoopedInternal(ref m_Handle, value);
		}

		public bool IsPlaying()
		{
			return GetIsPlayingInternal(ref m_Handle);
		}

		public double GetStartDelay()
		{
			return GetStartDelayInternal(ref m_Handle);
		}

		internal void SetStartDelay(double value)
		{
			ValidateStartDelayInternal(value);
			SetStartDelayInternal(ref m_Handle, value);
		}

		public double GetPauseDelay()
		{
			return GetPauseDelayInternal(ref m_Handle);
		}

		internal void GetPauseDelay(double value)
		{
			double pauseDelayInternal = GetPauseDelayInternal(ref m_Handle);
			if (m_Handle.GetPlayState() == PlayState.Playing && (value < 0.05 || (pauseDelayInternal != 0.0 && pauseDelayInternal < 0.05)))
			{
				throw new ArgumentException("VideoClipPlayable.pauseDelay: Setting new delay when existing delay is too small or 0.0 (" + pauseDelayInternal + "), Video system will not be able to change in time");
			}
			SetPauseDelayInternal(ref m_Handle, value);
		}

		public void Seek(double startTime, double startDelay)
		{
			Seek(startTime, startDelay, 0.0);
		}

		public void Seek(double startTime, double startDelay, [DefaultValue("0")] double duration)
		{
			ValidateStartDelayInternal(startDelay);
			SetStartDelayInternal(ref m_Handle, startDelay);
			if (duration > 0.0)
			{
				m_Handle.SetDuration(duration + startTime);
				SetPauseDelayInternal(ref m_Handle, startDelay + duration);
			}
			else
			{
				m_Handle.SetDuration(double.MaxValue);
				SetPauseDelayInternal(ref m_Handle, 0.0);
			}
			m_Handle.SetTime(startTime);
			m_Handle.Play();
		}

		private void ValidateStartDelayInternal(double startDelay)
		{
			double startDelayInternal = GetStartDelayInternal(ref m_Handle);
			if (IsPlaying() && (startDelay < 0.05 || (startDelayInternal >= 1E-05 && startDelayInternal < 0.05)))
			{
				Debug.LogWarning("VideoClipPlayable.StartDelay: Setting new delay when existing delay is too small or 0.0 (" + startDelayInternal + "), Video system will not be able to change in time");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern VideoClip GetClipInternal(ref PlayableHandle hdl);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetClipInternal(ref PlayableHandle hdl, VideoClip clip);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool GetLoopedInternal(ref PlayableHandle hdl);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetLoopedInternal(ref PlayableHandle hdl, bool looped);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool GetIsPlayingInternal(ref PlayableHandle hdl);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern double GetStartDelayInternal(ref PlayableHandle hdl);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetStartDelayInternal(ref PlayableHandle hdl, double delay);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern double GetPauseDelayInternal(ref PlayableHandle hdl);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetPauseDelayInternal(ref PlayableHandle hdl, double delay);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool InternalCreateVideoClipPlayable(ref PlayableGraph graph, VideoClip clip, bool looping, ref PlayableHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool ValidateType(ref PlayableHandle hdl);
	}
	[StaticAccessor("VideoPlayerExtensionsBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("VideoScriptingClasses.h")]
	[NativeHeader("Modules/Video/Public/VideoPlayer.h")]
	[NativeHeader("Modules/Video/Public/ScriptBindings/VideoPlayerExtensions.bindings.h")]
	public static class VideoPlayerExtensions
	{
		public static AudioSampleProvider GetAudioSampleProvider(this VideoPlayer vp, ushort trackIndex)
		{
			ushort controlledAudioTrackCount = vp.controlledAudioTrackCount;
			if (trackIndex >= controlledAudioTrackCount)
			{
				throw new ArgumentOutOfRangeException("trackIndex", trackIndex, "VideoPlayer is currently configured with " + controlledAudioTrackCount + " tracks.");
			}
			VideoAudioOutputMode audioOutputMode = vp.audioOutputMode;
			if (audioOutputMode != VideoAudioOutputMode.APIOnly)
			{
				throw new InvalidOperationException("VideoPlayer.GetAudioSampleProvider requires audioOutputMode to be APIOnly. Current: " + audioOutputMode);
			}
			AudioSampleProvider audioSampleProvider = AudioSampleProvider.Lookup(vp.InternalGetAudioSampleProviderId(trackIndex), vp, trackIndex);
			if (audioSampleProvider == null)
			{
				throw new InvalidOperationException("VideoPlayer.GetAudioSampleProvider got null provider.");
			}
			if (audioSampleProvider.owner != vp)
			{
				throw new InvalidOperationException("Internal error: VideoPlayer.GetAudioSampleProvider got provider used by another object.");
			}
			if (audioSampleProvider.trackIndex != trackIndex)
			{
				throw new InvalidOperationException("Internal error: VideoPlayer.GetAudioSampleProvider got provider for track " + audioSampleProvider.trackIndex + " instead of " + trackIndex);
			}
			return audioSampleProvider;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint InternalGetAudioSampleProviderId([NotNull("NullExceptionObject")] this VideoPlayer vp, ushort trackIndex);
	}
}
namespace UnityEngine.Video
{
	[NativeHeader("Modules/Video/Public/VideoClip.h")]
	[RequiredByNativeCode]
	public sealed class VideoClip : Object
	{
		public extern string originalPath
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern ulong frameCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern double frameRate
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeName("Duration")]
		public extern double length
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern uint width
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern uint height
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern uint pixelAspectRatioNumerator
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern uint pixelAspectRatioDenominator
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool sRGB
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("IssRGB")]
			get;
		}

		public extern ushort audioTrackCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		private VideoClip()
		{
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern ushort GetAudioChannelCount(ushort audioTrackIdx);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern uint GetAudioSampleRate(ushort audioTrackIdx);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetAudioLanguage(ushort audioTrackIdx);
	}
	[RequiredByNativeCode]
	public enum VideoRenderMode
	{
		CameraFarPlane,
		CameraNearPlane,
		RenderTexture,
		MaterialOverride,
		APIOnly
	}
	[RequiredByNativeCode]
	public enum Video3DLayout
	{
		No3D,
		SideBySide3D,
		OverUnder3D
	}
	[RequiredByNativeCode]
	public enum VideoAspectRatio
	{
		NoScaling,
		FitVertically,
		FitHorizontally,
		FitInside,
		FitOutside,
		Stretch
	}
	[Obsolete("VideoTimeSource is deprecated. Use TimeUpdateMode instead. (UnityUpgradable) -> VideoTimeUpdateMode")]
	[RequiredByNativeCode]
	public enum VideoTimeSource
	{
		[Obsolete("AudioDSPTimeSource is deprecated. Use DSPTime instead. (UnityUpgradable) -> DSPTime")]
		AudioDSPTimeSource,
		[Obsolete("GameTimeSource is deprecated. Use GameTime instead. (UnityUpgradable) -> GameTime")]
		GameTimeSource
	}
	[RequiredByNativeCode]
	public enum VideoTimeReference
	{
		Freerun,
		InternalTime,
		ExternalTime
	}
	[RequiredByNativeCode]
	public enum VideoSource
	{
		VideoClip,
		Url
	}
	[RequiredByNativeCode]
	public enum VideoTimeUpdateMode
	{
		DSPTime,
		GameTime,
		UnscaledGameTime
	}
	[RequiredByNativeCode]
	public enum VideoAudioOutputMode
	{
		None,
		AudioSource,
		Direct,
		APIOnly
	}
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Video/Public/VideoPlayer.h")]
	[RequiredByNativeCode]
	public sealed class VideoPlayer : Behaviour
	{
		public delegate void EventHandler(VideoPlayer source);

		public delegate void ErrorEventHandler(VideoPlayer source, string message);

		public delegate void FrameReadyEventHandler(VideoPlayer source, long frameIdx);

		public delegate void TimeEventHandler(VideoPlayer source, double seconds);

		public extern VideoSource source
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern VideoTimeUpdateMode timeUpdateMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("VideoUrl")]
		public extern string url
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("VideoClip")]
		public extern VideoClip clip
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern VideoRenderMode renderMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool canSetTimeUpdateMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("CanSetTimeUpdateMode")]
			get;
		}

		[NativeHeader("Runtime/Camera/Camera.h")]
		public extern Camera targetCamera
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeHeader("Runtime/Graphics/RenderTexture.h")]
		public extern RenderTexture targetTexture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeHeader("Runtime/Graphics/Renderer.h")]
		public extern Renderer targetMaterialRenderer
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern string targetMaterialProperty
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern VideoAspectRatio aspectRatio
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float targetCameraAlpha
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern Video3DLayout targetCamera3DLayout
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeHeader("Runtime/Graphics/Texture.h")]
		public extern Texture texture
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool isPrepared
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("IsPrepared")]
			get;
		}

		public extern bool waitForFirstFrame
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool playOnAwake
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

		public extern bool isPaused
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("IsPaused")]
			get;
		}

		public extern bool canSetTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("CanSetTime")]
			get;
		}

		[NativeName("SecPosition")]
		public extern double time
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("FramePosition")]
		public extern long frame
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern double clockTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool canStep
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("CanStep")]
			get;
		}

		public extern bool canSetPlaybackSpeed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("CanSetPlaybackSpeed")]
			get;
		}

		public extern float playbackSpeed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeName("Loop")]
		public extern bool isLooping
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("VideoPlayer.canSetTimeSource is deprecated. Use canSetTimeUpdateMode instead. (UnityUpgradable) -> canSetTimeUpdateMode")]
		public extern bool canSetTimeSource
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("CanSetTimeSource")]
			get;
		}

		[Obsolete("VideoPlayer.timeSource is deprecated. Use timeUpdateMode instead. (UnityUpgradable) -> timeUpdateMode")]
		public extern VideoTimeSource timeSource
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern VideoTimeReference timeReference
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern double externalReferenceTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool canSetSkipOnDrop
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("CanSetSkipOnDrop")]
			get;
		}

		public extern bool skipOnDrop
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern ulong frameCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern float frameRate
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeName("Duration")]
		public extern double length
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern uint width
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern uint height
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern uint pixelAspectRatioNumerator
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern uint pixelAspectRatioDenominator
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern ushort audioTrackCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern ushort controlledAudioTrackMaxCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public ushort controlledAudioTrackCount
		{
			get
			{
				return GetControlledAudioTrackCount();
			}
			set
			{
				int num = controlledAudioTrackMaxCount;
				if (value > num)
				{
					throw new ArgumentException($"Cannot control more than {num} tracks.", "value");
				}
				SetControlledAudioTrackCount(value);
			}
		}

		public extern VideoAudioOutputMode audioOutputMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool canSetDirectAudioVolume
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("CanSetDirectAudioVolume")]
			get;
		}

		public extern bool sendFrameReadyEvents
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("AreFrameReadyEventsEnabled")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("EnableFrameReadyEvents")]
			set;
		}

		public event EventHandler prepareCompleted;

		public event EventHandler loopPointReached;

		public event EventHandler started;

		public event EventHandler frameDropped;

		public event ErrorEventHandler errorReceived;

		public event EventHandler seekCompleted;

		public event TimeEventHandler clockResyncOccurred;

		public event FrameReadyEventHandler frameReady;

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Prepare();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Play();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Pause();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Stop();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StepForward();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetAudioLanguageCode(ushort trackIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern ushort GetAudioChannelCount(ushort trackIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern uint GetAudioSampleRate(ushort trackIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern ushort GetControlledAudioTrackCount();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetControlledAudioTrackCount(ushort value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void EnableAudioTrack(ushort trackIndex, bool enabled);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsAudioTrackEnabled(ushort trackIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetDirectAudioVolume(ushort trackIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetDirectAudioVolume(ushort trackIndex, float volume);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetDirectAudioMute(ushort trackIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetDirectAudioMute(ushort trackIndex, bool mute);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeHeader("Modules/Audio/Public/AudioSource.h")]
		public extern AudioSource GetTargetAudioSource(ushort trackIndex);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTargetAudioSource(ushort trackIndex, AudioSource source);

		[RequiredByNativeCode]
		private static void InvokePrepareCompletedCallback_Internal(VideoPlayer source)
		{
			if (source.prepareCompleted != null)
			{
				source.prepareCompleted(source);
			}
		}

		[RequiredByNativeCode]
		private static void InvokeFrameReadyCallback_Internal(VideoPlayer source, long frameIdx)
		{
			if (source.frameReady != null)
			{
				source.frameReady(source, frameIdx);
			}
		}

		[RequiredByNativeCode]
		private static void InvokeLoopPointReachedCallback_Internal(VideoPlayer source)
		{
			if (source.loopPointReached != null)
			{
				source.loopPointReached(source);
			}
		}

		[RequiredByNativeCode]
		private static void InvokeStartedCallback_Internal(VideoPlayer source)
		{
			if (source.started != null)
			{
				source.started(source);
			}
		}

		[RequiredByNativeCode]
		private static void InvokeFrameDroppedCallback_Internal(VideoPlayer source)
		{
			if (source.frameDropped != null)
			{
				source.frameDropped(source);
			}
		}

		[RequiredByNativeCode]
		private static void InvokeErrorReceivedCallback_Internal(VideoPlayer source, string errorStr)
		{
			if (source.errorReceived != null)
			{
				source.errorReceived(source, errorStr);
			}
		}

		[RequiredByNativeCode]
		private static void InvokeSeekCompletedCallback_Internal(VideoPlayer source)
		{
			if (source.seekCompleted != null)
			{
				source.seekCompleted(source);
			}
		}

		[RequiredByNativeCode]
		private static void InvokeClockResyncOccurredCallback_Internal(VideoPlayer source, double seconds)
		{
			if (source.clockResyncOccurred != null)
			{
				source.clockResyncOccurred(source, seconds);
			}
		}
	}
}
namespace UnityEngineInternal.Video
{
	[UsedByNativeCode]
	internal enum VideoError
	{
		NoErr,
		OutOfMemoryErr,
		CantReadFile,
		CantWriteFile,
		BadParams,
		NoData,
		BadPermissions,
		DeviceNotAvailable,
		ResourceNotAvailable,
		NetworkErr
	}
	[UsedByNativeCode]
	internal enum VideoPixelFormat
	{
		RGB,
		RGBA,
		YUV,
		YUVA
	}
	[UsedByNativeCode]
	internal enum VideoAlphaLayout
	{
		Native,
		Split
	}
	[UsedByNativeCode]
	[NativeHeader("Modules/Video/Public/Base/MediaComponent.h")]
	internal class VideoPlayback
	{
		public delegate void Callback();

		internal IntPtr m_Ptr;

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StartPlayback();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void PausePlayback();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopPlayback();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern VideoError GetStatus();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsReady();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsPlaying();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Step();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool CanStep();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern uint GetWidth();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern uint GetHeight();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetFrameRate();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetDuration();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern ulong GetFrameCount();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern uint GetPixelAspectRatioNumerator();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern uint GetPixelAspectRatioDenominator();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern VideoPixelFormat GetPixelFormat();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool CanNotSkipOnDrop();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetSkipOnDrop(bool skipOnDrop);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetTexture(Texture texture, out long outputFrameNum);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SeekToFrame(long frameIndex, Callback seekCompletedCallback);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SeekToTime(double secs, Callback seekCompletedCallback);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetPlaybackSpeed();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetPlaybackSpeed(float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetLoop();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetLoop(bool value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetAdjustToLinearSpace(bool enable);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeHeader("Modules/Audio/Public/AudioSource.h")]
		public extern ushort GetAudioTrackCount();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern ushort GetAudioChannelCount(ushort trackIdx);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern uint GetAudioSampleRate(ushort trackIdx);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetAudioLanguageCode(ushort trackIdx);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetAudioTarget(ushort trackIdx, bool enabled, bool softwareOutput, AudioSource audioSource);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern uint GetAudioSampleProviderId(ushort trackIndex);

		public AudioSampleProvider GetAudioSampleProvider(ushort trackIndex)
		{
			if (trackIndex >= GetAudioTrackCount())
			{
				throw new ArgumentOutOfRangeException("trackIndex", trackIndex, "VideoPlayback has " + GetAudioTrackCount() + " tracks.");
			}
			AudioSampleProvider audioSampleProvider = AudioSampleProvider.Lookup(GetAudioSampleProviderId(trackIndex), null, trackIndex);
			if (audioSampleProvider == null)
			{
				throw new InvalidOperationException("VideoPlayback.GetAudioSampleProvider got null provider.");
			}
			if (audioSampleProvider.owner != null)
			{
				throw new InvalidOperationException("Internal error: VideoPlayback.GetAudioSampleProvider got unexpected non-null provider owner.");
			}
			if (audioSampleProvider.trackIndex != trackIndex)
			{
				throw new InvalidOperationException("Internal error: VideoPlayback.GetAudioSampleProvider got provider for track " + audioSampleProvider.trackIndex + " instead of " + trackIndex);
			}
			return audioSampleProvider;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool PlatformSupportsH265();
	}
	[UsedByNativeCode]
	[NativeHeader("Modules/Video/Public/Base/VideoMediaPlayback.h")]
	internal class VideoPlaybackMgr : IDisposable
	{
		public delegate void Callback();

		public delegate void MessageCallback(string message);

		internal IntPtr m_Ptr;

		public extern ulong videoPlaybackCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public VideoPlaybackMgr()
		{
			m_Ptr = Internal_Create();
		}

		public void Dispose()
		{
			if (m_Ptr != IntPtr.Zero)
			{
				Internal_Destroy(m_Ptr);
				m_Ptr = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern VideoPlayback CreateVideoPlayback(string fileName, MessageCallback errorCallback, Callback readyCallback, Callback reachedEndCallback, bool splitAlpha = false);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ReleaseVideoPlayback(VideoPlayback playback);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Update();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ProcessOSMainLoopMessagesForTesting();
	}
}
