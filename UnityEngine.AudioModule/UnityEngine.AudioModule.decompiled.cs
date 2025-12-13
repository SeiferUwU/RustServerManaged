using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Playables;
using UnityEngine.Scripting;

[assembly: InternalsVisibleTo("UnityEngine.UnityCurlModule")]
[assembly: InternalsVisibleTo("UnityEngine.UmbraModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.UnityTestProtocolModule")]
[assembly: InternalsVisibleTo("UnityEngine.PerformanceReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestTextureModule")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud.Service")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud")]
[assembly: InternalsVisibleTo("UnityEngine.Networking")]
[assembly: InternalsVisibleTo("UnityEngine.PS5VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS5Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS4VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS4Module")]
[assembly: InternalsVisibleTo("UnityEngine.XboxOneModule")]
[assembly: InternalsVisibleTo("UnityEngine.SwitchModule")]
[assembly: InternalsVisibleTo("UnityEngine.WindModule")]
[assembly: InternalsVisibleTo("UnityEngine.VirtualTexturingModule")]
[assembly: InternalsVisibleTo("UnityEngine.VideoModule")]
[assembly: InternalsVisibleTo("UnityEngine.VehiclesModule")]
[assembly: InternalsVisibleTo("UnityEngine.VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.XRModule")]
[assembly: InternalsVisibleTo("UnityEngine.VFXModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestWWWModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAudioModule")]
[assembly: InternalsVisibleTo("Unity.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.ParticleSystemModule")]
[assembly: InternalsVisibleTo("UnityEngine.LocalizationModule")]
[assembly: InternalsVisibleTo("UnityEngine.AudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.HotReloadModule")]
[assembly: InternalsVisibleTo("UnityEngine.AnimationModule")]
[assembly: InternalsVisibleTo("UnityEngine.AndroidJNIModule")]
[assembly: InternalsVisibleTo("UnityEngine.AccessibilityModule")]
[assembly: InternalsVisibleTo("UnityEngine.ARModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputModule")]
[assembly: InternalsVisibleTo("UnityEngine.JSONSerializeModule")]
[assembly: InternalsVisibleTo("UnityEngine.PhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.AIModule")]
[assembly: InternalsVisibleTo("UnityEngine.CoreModule")]
[assembly: InternalsVisibleTo("UnityEngine.SharedInternalsModule")]
[assembly: InternalsVisibleTo("UnityEngine")]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: CompilationRelaxations(8)]
[assembly: InternalsVisibleTo("UnityEngine.ClothModule")]
[assembly: InternalsVisibleTo("UnityEngine.NVIDIAModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
[assembly: InternalsVisibleTo("UnityEngine.ContentLoadModule")]
[assembly: InternalsVisibleTo("UnityEngine.IMGUIModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.ClusterRendererModule")]
[assembly: InternalsVisibleTo("UnityEngine.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.014")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.013")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.012")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.011")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.010")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.009")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.008")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.015")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.007")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.001")]
[assembly: InternalsVisibleTo("UnityEngine.Core.Runtime.Tests")]
[assembly: InternalsVisibleTo("Unity.Core")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.006")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.016")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.017")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.018")]
[assembly: InternalsVisibleTo("Unity.Audio.DSPGraph.Tests")]
[assembly: InternalsVisibleTo("Unity.Audio.DSPGraph")]
[assembly: InternalsVisibleTo("Unity.Audio.Tests")]
[assembly: InternalsVisibleTo("Unity.AudioMixer.Tests")]
[assembly: UnityEngineModuleAssembly]
[assembly: InternalsVisibleTo("Unity.Subsystem.Registration")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.001")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.024")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.023")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.022")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.021")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.020")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.019")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommon")]
[assembly: InternalsVisibleTo("Unity.Collections")]
[assembly: InternalsVisibleTo("Unity.Runtime")]
[assembly: InternalsVisibleTo("Unity.Entities")]
[assembly: InternalsVisibleTo("Unity.Timeline")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.AllIn1Runner")]
[assembly: InternalsVisibleTo("Unity.PerformanceTests.RuntimeTestRunner.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Framework")]
[assembly: InternalsVisibleTo("Assembly-CSharp-testable")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Timeline")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests")]
[assembly: InternalsVisibleTo("Unity.DeploymentTests.Services")]
[assembly: InternalsVisibleTo("Unity.Burst.Editor")]
[assembly: InternalsVisibleTo("Unity.Burst")]
[assembly: InternalsVisibleTo("Unity.Automation")]
[assembly: InternalsVisibleTo("UnityEngine.TestRunner")]
[assembly: InternalsVisibleTo("UnityEngine.Purchasing")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.UnityAnalytics")]
[assembly: InternalsVisibleTo("Unity.Entities.Tests")]
[assembly: InternalsVisibleTo("Assembly-CSharp-firstpass-testable")]
[assembly: InternalsVisibleTo("GoogleAR.UnityNative")]
[assembly: InternalsVisibleTo("Unity.Logging")]
[assembly: InternalsVisibleTo("Unity.Services.QoS")]
[assembly: InternalsVisibleTo("Unity.ucg.QoS")]
[assembly: InternalsVisibleTo("Unity.Networking.Transport")]
[assembly: InternalsVisibleTo("UnityEngine.UI")]
[assembly: InternalsVisibleTo("Unity.UIElements.EditorTests")]
[assembly: InternalsVisibleTo("UnityEngine.UIElements.Tests")]
[assembly: InternalsVisibleTo("UnityEngine.SpatialTracking")]
[assembly: InternalsVisibleTo("Unity.UIElements.PlayModeTests")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsGameObjectsModule")]
[assembly: InternalsVisibleTo("Unity.UIElements")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.EditorTests")]
[assembly: InternalsVisibleTo("UnityEditor.UIBuilderModule")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.Editor")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.EditorTests")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.Editor")]
[assembly: InternalsVisibleTo("Unity.WindowsMRAutomation")]
[assembly: InternalsVisibleTo("Unity.UIElements.Editor")]
[assembly: InternalsVisibleTo("VivoxUnity")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace UnityEngine
{
	public enum AudioSpeakerMode
	{
		[Obsolete("Raw speaker mode is not supported. Do not use.", true)]
		Raw,
		Mono,
		Stereo,
		Quad,
		Surround,
		Mode5point1,
		Mode7point1,
		Prologic
	}
	public enum AudioDataLoadState
	{
		Unloaded,
		Loading,
		Loaded,
		Failed
	}
	public struct AudioConfiguration
	{
		public AudioSpeakerMode speakerMode;

		public int dspBufferSize;

		public int sampleRate;

		public int numRealVoices;

		public int numVirtualVoices;
	}
	public enum AudioCompressionFormat
	{
		PCM,
		Vorbis,
		ADPCM,
		MP3,
		VAG,
		HEVAG,
		XMA,
		AAC,
		GCADPCM,
		ATRAC9
	}
	public enum AudioClipLoadType
	{
		DecompressOnLoad,
		CompressedInMemory,
		Streaming
	}
	public enum AudioVelocityUpdateMode
	{
		Auto,
		Fixed,
		Dynamic
	}
	public enum FFTWindow
	{
		Rectangular,
		Triangle,
		Hamming,
		Hanning,
		Blackman,
		BlackmanHarris
	}
	public enum AudioRolloffMode
	{
		Logarithmic,
		Linear,
		Custom
	}
	public enum AudioSourceCurveType
	{
		CustomRolloff,
		SpatialBlend,
		ReverbZoneMix,
		Spread
	}
	public enum AudioReverbPreset
	{
		Off,
		Generic,
		PaddedCell,
		Room,
		Bathroom,
		Livingroom,
		Stoneroom,
		Auditorium,
		Concerthall,
		Cave,
		Arena,
		Hangar,
		CarpetedHallway,
		Hallway,
		StoneCorridor,
		Alley,
		Forest,
		City,
		Mountains,
		Quarry,
		Plain,
		ParkingLot,
		SewerPipe,
		Underwater,
		Drugged,
		Dizzy,
		Psychotic,
		User
	}
	[StaticAccessor("GetAudioManager()", StaticAccessorType.Dot)]
	[NativeHeader("Modules/Audio/Public/ScriptBindings/Audio.bindings.h")]
	public sealed class AudioSettings
	{
		public delegate void AudioConfigurationChangeHandler(bool deviceWasChanged);

		public static class Mobile
		{
			public static bool muteState => false;

			public static bool stopAudioOutputOnMute
			{
				get
				{
					return false;
				}
				set
				{
					Debug.LogWarning("Setting AudioSettings.Mobile.stopAudioOutputOnMute is possible on iOS and Android only");
				}
			}

			public static bool audioOutputStarted => true;

			public static event Action<bool> OnMuteStateChanged;

			public static void StartAudioOutput()
			{
				Debug.LogWarning("AudioSettings.Mobile.StartAudioOutput is implemented for iOS and Android only");
			}

			public static void StopAudioOutput()
			{
				Debug.LogWarning("AudioSettings.Mobile.StopAudioOutput is implemented for iOS and Android only");
			}
		}

		public static extern AudioSpeakerMode driverCapabilities
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetSpeakerModeCaps")]
			get;
		}

		public static AudioSpeakerMode speakerMode
		{
			get
			{
				return GetSpeakerMode();
			}
			set
			{
				Debug.LogWarning("Setting AudioSettings.speakerMode is deprecated and has been replaced by audio project settings and the AudioSettings.GetConfiguration/AudioSettings.Reset API.");
				AudioConfiguration configuration = GetConfiguration();
				configuration.speakerMode = value;
				if (!SetConfiguration(configuration))
				{
					Debug.LogWarning("Setting AudioSettings.speakerMode failed");
				}
			}
		}

		internal static extern int profilerCaptureFlags
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public static extern double dspTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod(Name = "GetDSPTime", IsThreadSafe = true)]
			get;
		}

		public static int outputSampleRate
		{
			get
			{
				return GetSampleRate();
			}
			set
			{
				Debug.LogWarning("Setting AudioSettings.outputSampleRate is deprecated and has been replaced by audio project settings and the AudioSettings.GetConfiguration/AudioSettings.Reset API.");
				AudioConfiguration configuration = GetConfiguration();
				configuration.sampleRate = value;
				if (!SetConfiguration(configuration))
				{
					Debug.LogWarning("Setting AudioSettings.outputSampleRate failed");
				}
			}
		}

		internal static extern bool unityAudioDisabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("IsAudioDisabled")]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("DisableAudio")]
			set;
		}

		public static event AudioConfigurationChangeHandler OnAudioConfigurationChanged;

		internal static event Action OnAudioSystemShuttingDown;

		internal static event Action OnAudioSystemStartedUp;

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioSpeakerMode GetSpeakerMode();

		[NativeMethod(Name = "AudioSettings::SetConfiguration", IsFreeFunction = true)]
		[NativeThrows]
		private static bool SetConfiguration(AudioConfiguration config)
		{
			return SetConfiguration_Injected(ref config);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "AudioSettings::GetSampleRate", IsFreeFunction = true)]
		private static extern int GetSampleRate();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "AudioSettings::GetDSPBufferSize", IsFreeFunction = true)]
		public static extern void GetDSPBufferSize(out int bufferLength, out int numBuffers);

		[Obsolete("AudioSettings.SetDSPBufferSize is deprecated and has been replaced by audio project settings and the AudioSettings.GetConfiguration/AudioSettings.Reset API.")]
		public static void SetDSPBufferSize(int bufferLength, int numBuffers)
		{
			Debug.LogWarning("AudioSettings.SetDSPBufferSize is deprecated and has been replaced by audio project settings and the AudioSettings.GetConfiguration/AudioSettings.Reset API.");
			AudioConfiguration configuration = GetConfiguration();
			configuration.dspBufferSize = bufferLength;
			if (!SetConfiguration(configuration))
			{
				Debug.LogWarning("SetDSPBufferSize failed");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetCurrentSpatializerDefinitionName")]
		public static extern string GetSpatializerPluginName();

		public static AudioConfiguration GetConfiguration()
		{
			GetConfiguration_Injected(out var ret);
			return ret;
		}

		public static bool Reset(AudioConfiguration config)
		{
			return SetConfiguration(config);
		}

		[RequiredByNativeCode]
		internal static void InvokeOnAudioConfigurationChanged(bool deviceWasChanged)
		{
			if (AudioSettings.OnAudioConfigurationChanged != null)
			{
				AudioSettings.OnAudioConfigurationChanged(deviceWasChanged);
			}
		}

		[RequiredByNativeCode]
		internal static void InvokeOnAudioSystemShuttingDown()
		{
			AudioSettings.OnAudioSystemShuttingDown?.Invoke();
		}

		[RequiredByNativeCode]
		internal static void InvokeOnAudioSystemStartedUp()
		{
			AudioSettings.OnAudioSystemStartedUp?.Invoke();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "AudioSettings::GetCurrentAmbisonicDefinitionName", IsFreeFunction = true)]
		internal static extern string GetAmbisonicDecoderPluginName();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetConfiguration_Injected(ref AudioConfiguration config);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetConfiguration_Injected(out AudioConfiguration ret);
	}
	[NativeHeader("Modules/Audio/Public/ScriptBindings/Audio.bindings.h")]
	[StaticAccessor("AudioClipBindings", StaticAccessorType.DoubleColon)]
	public sealed class AudioClip : Object
	{
		public delegate void PCMReaderCallback(float[] data);

		public delegate void PCMSetPositionCallback(int position);

		[NativeProperty("LengthSec")]
		public extern float length
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("SampleCount")]
		public extern int samples
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[NativeProperty("ChannelCount")]
		public extern int channels
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern int frequency
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[Obsolete("Use AudioClip.loadState instead to get more detailed information about the loading process.")]
		public extern bool isReadyToPlay
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("ReadyToPlay")]
			get;
		}

		public extern AudioClipLoadType loadType
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool preloadAudioData
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool ambisonic
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool loadInBackground
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern AudioDataLoadState loadState
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod(Name = "AudioClipBindings::GetLoadState", HasExplicitThis = true)]
			get;
		}

		private event PCMReaderCallback m_PCMReaderCallback = null;

		private event PCMSetPositionCallback m_PCMSetPositionCallback = null;

		private AudioClip()
		{
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetData([NotNull("NullExceptionObject")] AudioClip clip, [Out] float[] data, int numSamples, int samplesOffset);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetData([NotNull("NullExceptionObject")] AudioClip clip, float[] data, int numsamples, int samplesOffset);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioClip Construct_Internal();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string GetName();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void CreateUserSound(string name, int lengthSamples, int channels, int frequency, bool stream);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool LoadAudioData();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool UnloadAudioData();

		public bool GetData(float[] data, int offsetSamples)
		{
			if (channels <= 0)
			{
				Debug.Log("AudioClip.GetData failed; AudioClip " + GetName() + " contains no data");
				return false;
			}
			int numSamples = ((data != null) ? (data.Length / channels) : 0);
			return GetData(this, data, numSamples, offsetSamples);
		}

		public bool SetData(float[] data, int offsetSamples)
		{
			if (channels <= 0)
			{
				Debug.Log("AudioClip.SetData failed; AudioClip " + GetName() + " contains no data");
				return false;
			}
			if (offsetSamples < 0 || offsetSamples >= samples)
			{
				throw new ArgumentException("AudioClip.SetData failed; invalid offsetSamples");
			}
			if (data == null || data.Length == 0)
			{
				throw new ArgumentException("AudioClip.SetData failed; invalid data");
			}
			return SetData(this, data, data.Length / channels, offsetSamples);
		}

		[Obsolete("The _3D argument of AudioClip is deprecated. Use the spatialBlend property of AudioSource instead to morph between 2D and 3D playback.")]
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream)
		{
			return Create(name, lengthSamples, channels, frequency, stream);
		}

		[Obsolete("The _3D argument of AudioClip is deprecated. Use the spatialBlend property of AudioSource instead to morph between 2D and 3D playback.")]
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream, PCMReaderCallback pcmreadercallback)
		{
			return Create(name, lengthSamples, channels, frequency, stream, pcmreadercallback, null);
		}

		[Obsolete("The _3D argument of AudioClip is deprecated. Use the spatialBlend property of AudioSource instead to morph between 2D and 3D playback.")]
		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool _3D, bool stream, PCMReaderCallback pcmreadercallback, PCMSetPositionCallback pcmsetpositioncallback)
		{
			return Create(name, lengthSamples, channels, frequency, stream, pcmreadercallback, pcmsetpositioncallback);
		}

		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool stream)
		{
			return Create(name, lengthSamples, channels, frequency, stream, null, null);
		}

		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool stream, PCMReaderCallback pcmreadercallback)
		{
			return Create(name, lengthSamples, channels, frequency, stream, pcmreadercallback, null);
		}

		public static AudioClip Create(string name, int lengthSamples, int channels, int frequency, bool stream, PCMReaderCallback pcmreadercallback, PCMSetPositionCallback pcmsetpositioncallback)
		{
			if (name == null)
			{
				throw new NullReferenceException();
			}
			if (lengthSamples <= 0)
			{
				throw new ArgumentException("Length of created clip must be larger than 0");
			}
			if (channels <= 0)
			{
				throw new ArgumentException("Number of channels in created clip must be greater than 0");
			}
			if (frequency <= 0)
			{
				throw new ArgumentException("Frequency in created clip must be greater than 0");
			}
			AudioClip audioClip = Construct_Internal();
			if (pcmreadercallback != null)
			{
				audioClip.m_PCMReaderCallback += pcmreadercallback;
			}
			if (pcmsetpositioncallback != null)
			{
				audioClip.m_PCMSetPositionCallback += pcmsetpositioncallback;
			}
			audioClip.CreateUserSound(name, lengthSamples, channels, frequency, stream);
			return audioClip;
		}

		[RequiredByNativeCode]
		private void InvokePCMReaderCallback_Internal(float[] data)
		{
			if (this.m_PCMReaderCallback != null)
			{
				this.m_PCMReaderCallback(data);
			}
		}

		[RequiredByNativeCode]
		private void InvokePCMSetPositionCallback_Internal(int position)
		{
			if (this.m_PCMSetPositionCallback != null)
			{
				this.m_PCMSetPositionCallback(position);
			}
		}
	}
	public class AudioBehaviour : Behaviour
	{
	}
	[StaticAccessor("AudioListenerBindings", StaticAccessorType.DoubleColon)]
	[RequireComponent(typeof(Transform))]
	public sealed class AudioListener : AudioBehaviour
	{
		public static extern float volume
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("ListenerPause")]
		public static extern bool pause
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern AudioVelocityUpdateMode velocityUpdateMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void GetOutputDataHelper([Out] float[] samples, int channel);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void GetSpectrumDataHelper([Out] float[] samples, int channel, FFTWindow window);

		[Obsolete("GetOutputData returning a float[] is deprecated, use GetOutputData and pass a pre allocated array instead.")]
		public static float[] GetOutputData(int numSamples, int channel)
		{
			float[] array = new float[numSamples];
			GetOutputDataHelper(array, channel);
			return array;
		}

		public static void GetOutputData(float[] samples, int channel)
		{
			GetOutputDataHelper(samples, channel);
		}

		[Obsolete("GetSpectrumData returning a float[] is deprecated, use GetSpectrumData and pass a pre allocated array instead.")]
		public static float[] GetSpectrumData(int numSamples, int channel, FFTWindow window)
		{
			float[] array = new float[numSamples];
			GetSpectrumDataHelper(array, channel, window);
			return array;
		}

		public static void GetSpectrumData(float[] samples, int channel, FFTWindow window)
		{
			GetSpectrumDataHelper(samples, channel, window);
		}
	}
	[RequireComponent(typeof(Transform))]
	[StaticAccessor("AudioSourceBindings", StaticAccessorType.DoubleColon)]
	public sealed class AudioSource : AudioBehaviour
	{
		public extern float volume
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public float pitch
		{
			get
			{
				return GetPitch(this);
			}
			set
			{
				SetPitch(this, value);
			}
		}

		[NativeProperty("SecPosition")]
		public extern float time
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("SamplePosition")]
		public extern int timeSamples
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod(IsThreadSafe = true)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeMethod(IsThreadSafe = true)]
			set;
		}

		[NativeProperty("AudioClip")]
		public extern AudioClip clip
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern AudioMixerGroup outputAudioMixerGroup
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool isPlaying
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("IsPlayingScripting")]
			get;
		}

		public extern bool isVirtual
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetLastVirtualState")]
			get;
		}

		public extern bool loop
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool ignoreListenerVolume
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

		public extern bool ignoreListenerPause
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern AudioVelocityUpdateMode velocityUpdateMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("StereoPan")]
		public extern float panStereo
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty("SpatialBlendMix")]
		public extern float spatialBlend
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool spatialize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool spatializePostEffects
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float reverbZoneMix
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool bypassEffects
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool bypassListenerEffects
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool bypassReverbZones
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float dopplerLevel
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float spread
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int priority
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern bool mute
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float minDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float maxDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern AudioRolloffMode rolloffMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("minVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.", true)]
		public float minVolume
		{
			get
			{
				Debug.LogError("minVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
				return 0f;
			}
			set
			{
				Debug.LogError("minVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
			}
		}

		[Obsolete("maxVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.", true)]
		public float maxVolume
		{
			get
			{
				Debug.LogError("maxVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
				return 0f;
			}
			set
			{
				Debug.LogError("maxVolume is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
			}
		}

		[Obsolete("rolloffFactor is not supported anymore. Use min-, maxDistance and rolloffMode instead.", true)]
		public float rolloffFactor
		{
			get
			{
				Debug.LogError("rolloffFactor is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
				return 0f;
			}
			set
			{
				Debug.LogError("rolloffFactor is not supported anymore. Use min-, maxDistance and rolloffMode instead.");
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetPitch([NotNull("ArgumentNullException")] AudioSource source);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPitch([NotNull("ArgumentNullException")] AudioSource source, float pitch);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PlayHelper([NotNull("ArgumentNullException")] AudioSource source, ulong delay);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Play(double delay);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PlayOneShotHelper([NotNull("ArgumentNullException")] AudioSource source, [NotNull("NullExceptionObject")] AudioClip clip, float volumeScale);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Stop(bool stopOneShots);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetCustomCurveHelper([NotNull("ArgumentNullException")] AudioSource source, AudioSourceCurveType type, AnimationCurve curve);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimationCurve GetCustomCurveHelper([NotNull("ArgumentNullException")] AudioSource source, AudioSourceCurveType type);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetOutputDataHelper([NotNull("ArgumentNullException")] AudioSource source, [Out] float[] samples, int channel);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void GetSpectrumDataHelper([NotNull("ArgumentNullException")] AudioSource source, [Out] float[] samples, int channel, FFTWindow window);

		[ExcludeFromDocs]
		public void Play()
		{
			PlayHelper(this, 0uL);
		}

		public void Play([UnityEngine.Internal.DefaultValue("0")] ulong delay)
		{
			PlayHelper(this, delay);
		}

		public void PlayDelayed(float delay)
		{
			Play((delay < 0f) ? 0.0 : (0.0 - (double)delay));
		}

		public void PlayScheduled(double time)
		{
			Play((time < 0.0) ? 0.0 : time);
		}

		[ExcludeFromDocs]
		public void PlayOneShot(AudioClip clip)
		{
			PlayOneShot(clip, 1f);
		}

		public void PlayOneShot(AudioClip clip, [UnityEngine.Internal.DefaultValue("1.0F")] float volumeScale)
		{
			if (clip == null)
			{
				Debug.LogWarning("PlayOneShot was called with a null AudioClip.");
			}
			else
			{
				PlayOneShotHelper(this, clip, volumeScale);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetScheduledStartTime(double time);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetScheduledEndTime(double time);

		public void Stop()
		{
			Stop(stopOneShots: true);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Pause();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void UnPause();

		[ExcludeFromDocs]
		public static void PlayClipAtPoint(AudioClip clip, Vector3 position)
		{
			PlayClipAtPoint(clip, position, 1f);
		}

		public static void PlayClipAtPoint(AudioClip clip, Vector3 position, [UnityEngine.Internal.DefaultValue("1.0F")] float volume)
		{
			GameObject gameObject = new GameObject("One shot audio");
			gameObject.transform.position = position;
			AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
			audioSource.clip = clip;
			audioSource.spatialBlend = 1f;
			audioSource.volume = volume;
			audioSource.Play();
			Object.Destroy(gameObject, clip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale));
		}

		public void SetCustomCurve(AudioSourceCurveType type, AnimationCurve curve)
		{
			SetCustomCurveHelper(this, type, curve);
		}

		public AnimationCurve GetCustomCurve(AudioSourceCurveType type)
		{
			return GetCustomCurveHelper(this, type);
		}

		[Obsolete("GetOutputData returning a float[] is deprecated, use GetOutputData and pass a pre allocated array instead.")]
		public float[] GetOutputData(int numSamples, int channel)
		{
			float[] array = new float[numSamples];
			GetOutputDataHelper(this, array, channel);
			return array;
		}

		public void GetOutputData(float[] samples, int channel)
		{
			GetOutputDataHelper(this, samples, channel);
		}

		[Obsolete("GetSpectrumData returning a float[] is deprecated, use GetSpectrumData and pass a pre allocated array instead.")]
		public float[] GetSpectrumData(int numSamples, int channel, FFTWindow window)
		{
			float[] array = new float[numSamples];
			GetSpectrumDataHelper(this, array, channel, window);
			return array;
		}

		public void GetSpectrumData(float[] samples, int channel, FFTWindow window)
		{
			GetSpectrumDataHelper(this, samples, channel, window);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SetSpatializerFloat(int index, float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetSpatializerFloat(int index, out float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetAmbisonicDecoderFloat(int index, out float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SetAmbisonicDecoderFloat(int index, float value);
	}
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Audio/Public/AudioReverbZone.h")]
	public sealed class AudioReverbZone : Behaviour
	{
		public extern float minDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float maxDistance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern AudioReverbPreset reverbPreset
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int room
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int roomHF
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int roomLF
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float decayTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float decayHFRatio
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int reflections
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float reflectionsDelay
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int reverb
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float reverbDelay
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float HFReference
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float LFReference
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("Warning! roomRolloffFactor is no longer supported.")]
		public float roomRolloffFactor
		{
			get
			{
				Debug.LogWarning("Warning! roomRolloffFactor is no longer supported.");
				return 10f;
			}
			set
			{
				Debug.LogWarning("Warning! roomRolloffFactor is no longer supported.");
			}
		}

		public extern float diffusion
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float density
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
	[RequireComponent(typeof(AudioBehaviour))]
	public sealed class AudioLowPassFilter : Behaviour
	{
		public AnimationCurve customCutoffCurve
		{
			get
			{
				return GetCustomLowpassLevelCurveCopy();
			}
			set
			{
				SetCustomLowpassLevelCurveHelper(this, value);
			}
		}

		public extern float cutoffFrequency
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float lowpassResonanceQ
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern AnimationCurve GetCustomLowpassLevelCurveCopy();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "AudioLowPassFilterBindings::SetCustomLowpassLevelCurveHelper", IsFreeFunction = true)]
		[NativeThrows]
		private static extern void SetCustomLowpassLevelCurveHelper([NotNull("NullExceptionObject")] AudioLowPassFilter source, AnimationCurve curve);
	}
	[RequireComponent(typeof(AudioBehaviour))]
	public sealed class AudioHighPassFilter : Behaviour
	{
		public extern float cutoffFrequency
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float highpassResonanceQ
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
	[RequireComponent(typeof(AudioBehaviour))]
	public sealed class AudioDistortionFilter : Behaviour
	{
		public extern float distortionLevel
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
	[RequireComponent(typeof(AudioBehaviour))]
	public sealed class AudioEchoFilter : Behaviour
	{
		public extern float delay
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float decayRatio
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float dryMix
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float wetMix
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
	[RequireComponent(typeof(AudioBehaviour))]
	public sealed class AudioChorusFilter : Behaviour
	{
		public extern float dryMix
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float wetMix1
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float wetMix2
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float wetMix3
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float delay
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float rate
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float depth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("Warning! Feedback is deprecated. This property does nothing.")]
		public float feedback
		{
			get
			{
				Debug.LogWarning("Warning! Feedback is deprecated. This property does nothing.");
				return 0f;
			}
			set
			{
				Debug.LogWarning("Warning! Feedback is deprecated. This property does nothing.");
			}
		}
	}
	[RequireComponent(typeof(AudioBehaviour))]
	public sealed class AudioReverbFilter : Behaviour
	{
		public extern AudioReverbPreset reverbPreset
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float dryLevel
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float room
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float roomHF
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[Obsolete("Warning! roomRolloffFactor is no longer supported.")]
		public float roomRolloffFactor
		{
			get
			{
				Debug.LogWarning("Warning! roomRolloffFactor is no longer supported.");
				return 10f;
			}
			set
			{
				Debug.LogWarning("Warning! roomRolloffFactor is no longer supported.");
			}
		}

		public extern float decayTime
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float decayHFRatio
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float reflectionsLevel
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float reflectionsDelay
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float reverbLevel
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float reverbDelay
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float diffusion
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float density
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float hfReference
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float roomLF
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float lfReference
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
	[StaticAccessor("GetAudioManager()", StaticAccessorType.Dot)]
	public sealed class Microphone
	{
		public static extern string[] devices
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("GetRecordDevices")]
			get;
		}

		internal static extern bool isAnyDeviceRecording
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("IsAnyRecordDeviceActive")]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsThreadSafe = true)]
		private static extern int GetMicrophoneDeviceIDFromName(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioClip StartRecord(int deviceID, bool loop, float lengthSec, int frequency);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EndRecord(int deviceID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsRecording(int deviceID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsThreadSafe = true)]
		private static extern int GetRecordPosition(int deviceID);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDeviceCaps(int deviceID, out int minFreq, out int maxFreq);

		public static AudioClip Start(string deviceName, bool loop, int lengthSec, int frequency)
		{
			int microphoneDeviceIDFromName = GetMicrophoneDeviceIDFromName(deviceName);
			if (microphoneDeviceIDFromName == -1)
			{
				throw new ArgumentException("Couldn't acquire device ID for device name " + deviceName);
			}
			if (lengthSec <= 0)
			{
				throw new ArgumentException("Length of recording must be greater than zero seconds (was: " + lengthSec + " seconds)");
			}
			if (lengthSec > 3600)
			{
				throw new ArgumentException("Length of recording must be less than one hour (was: " + lengthSec + " seconds)");
			}
			if (frequency <= 0)
			{
				throw new ArgumentException("Frequency of recording must be greater than zero (was: " + frequency + " Hz)");
			}
			return StartRecord(microphoneDeviceIDFromName, loop, lengthSec, frequency);
		}

		public static void End(string deviceName)
		{
			int microphoneDeviceIDFromName = GetMicrophoneDeviceIDFromName(deviceName);
			if (microphoneDeviceIDFromName != -1)
			{
				EndRecord(microphoneDeviceIDFromName);
			}
		}

		public static bool IsRecording(string deviceName)
		{
			int microphoneDeviceIDFromName = GetMicrophoneDeviceIDFromName(deviceName);
			if (microphoneDeviceIDFromName == -1)
			{
				return false;
			}
			return IsRecording(microphoneDeviceIDFromName);
		}

		public static int GetPosition(string deviceName)
		{
			int microphoneDeviceIDFromName = GetMicrophoneDeviceIDFromName(deviceName);
			if (microphoneDeviceIDFromName == -1)
			{
				return 0;
			}
			return GetRecordPosition(microphoneDeviceIDFromName);
		}

		public static void GetDeviceCaps(string deviceName, out int minFreq, out int maxFreq)
		{
			minFreq = 0;
			maxFreq = 0;
			int microphoneDeviceIDFromName = GetMicrophoneDeviceIDFromName(deviceName);
			if (microphoneDeviceIDFromName != -1)
			{
				GetDeviceCaps(microphoneDeviceIDFromName, out minFreq, out maxFreq);
			}
		}
	}
	[NativeType(Header = "Modules/Audio/Public/ScriptBindings/AudioRenderer.bindings.h")]
	public class AudioRenderer
	{
		public static bool Start()
		{
			return Internal_AudioRenderer_Start();
		}

		public static bool Stop()
		{
			return Internal_AudioRenderer_Stop();
		}

		public static int GetSampleCountForCaptureFrame()
		{
			return Internal_AudioRenderer_GetSampleCountForCaptureFrame();
		}

		internal unsafe static bool AddMixerGroupSink(AudioMixerGroup mixerGroup, NativeArray<float> buffer, bool excludeFromMix)
		{
			return Internal_AudioRenderer_AddMixerGroupSink(mixerGroup, buffer.GetUnsafePtr(), buffer.Length, excludeFromMix);
		}

		public unsafe static bool Render(NativeArray<float> buffer)
		{
			return Internal_AudioRenderer_Render(buffer.GetUnsafePtr(), buffer.Length);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Internal_AudioRenderer_Start();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool Internal_AudioRenderer_Stop();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int Internal_AudioRenderer_GetSampleCountForCaptureFrame();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool Internal_AudioRenderer_AddMixerGroupSink(AudioMixerGroup mixerGroup, void* ptr, int length, bool excludeFromMix);

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern bool Internal_AudioRenderer_Render(void* ptr, int length);
	}
	[ExcludeFromObjectFactory]
	[ExcludeFromPreset]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
	public sealed class MovieTexture : Texture
	{
		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public AudioClip audioClip
		{
			get
			{
				FeatureRemoved();
				return null;
			}
		}

		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public bool loop
		{
			get
			{
				FeatureRemoved();
				return false;
			}
			set
			{
				FeatureRemoved();
			}
		}

		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public bool isPlaying
		{
			get
			{
				FeatureRemoved();
				return false;
			}
		}

		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public bool isReadyToPlay
		{
			get
			{
				FeatureRemoved();
				return false;
			}
		}

		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public float duration
		{
			get
			{
				FeatureRemoved();
				return 1f;
			}
		}

		private static void FeatureRemoved()
		{
			throw new Exception("MovieTexture has been removed from Unity. Use VideoPlayer instead.");
		}

		private MovieTexture()
		{
		}

		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public void Play()
		{
			FeatureRemoved();
		}

		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public void Stop()
		{
			FeatureRemoved();
		}

		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public void Pause()
		{
			FeatureRemoved();
		}
	}
	public enum WebCamFlags
	{
		FrontFacing = 1,
		AutoFocusPointSupported
	}
	public enum WebCamKind
	{
		WideAngle = 1,
		Telephoto,
		ColorAndDepth,
		UltraWideAngle
	}
	[UsedByNativeCode]
	public struct WebCamDevice
	{
		[NativeName("name")]
		internal string m_Name;

		[NativeName("depthCameraName")]
		internal string m_DepthCameraName;

		[NativeName("flags")]
		internal int m_Flags;

		[NativeName("kind")]
		internal WebCamKind m_Kind;

		[NativeName("resolutions")]
		internal Resolution[] m_Resolutions;

		public string name => m_Name;

		public bool isFrontFacing => (m_Flags & 1) != 0;

		public WebCamKind kind => m_Kind;

		public string depthCameraName => (m_DepthCameraName == "") ? null : m_DepthCameraName;

		public bool isAutoFocusPointSupported => (m_Flags & 2) != 0;

		public Resolution[] availableResolutions => m_Resolutions;
	}
	[NativeHeader("Runtime/Video/ScriptBindings/WebCamTexture.bindings.h")]
	[NativeHeader("AudioScriptingClasses.h")]
	[NativeHeader("Runtime/Video/BaseWebCamTexture.h")]
	public sealed class WebCamTexture : Texture
	{
		public static extern WebCamDevice[] devices
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("Internal_GetDevices")]
			[StaticAccessor("WebCamTextureBindings", StaticAccessorType.DoubleColon)]
			get;
		}

		public extern bool isPlaying
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("IsPlaying")]
			get;
		}

		[NativeName("Device")]
		public extern string deviceName
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern float requestedFPS
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int requestedWidth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int requestedHeight
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern int videoRotationAngle
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool videoVerticallyMirrored
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("IsVideoVerticallyMirrored")]
			get;
		}

		public extern bool didUpdateThisFrame
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			[NativeName("DidUpdateThisFrame")]
			get;
		}

		public Vector2? autoFocusPoint
		{
			get
			{
				return (internalAutoFocusPoint.x < 0f) ? ((Vector2?)null) : new Vector2?(internalAutoFocusPoint);
			}
			set
			{
				internalAutoFocusPoint = ((!value.HasValue) ? new Vector2(-1f, -1f) : value.Value);
			}
		}

		internal Vector2 internalAutoFocusPoint
		{
			get
			{
				get_internalAutoFocusPoint_Injected(out var ret);
				return ret;
			}
			set
			{
				set_internalAutoFocusPoint_Injected(ref value);
			}
		}

		public extern bool isDepth
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public WebCamTexture(string deviceName, int requestedWidth, int requestedHeight, int requestedFPS)
		{
			Internal_CreateWebCamTexture(this, deviceName, requestedWidth, requestedHeight, requestedFPS);
		}

		public WebCamTexture(string deviceName, int requestedWidth, int requestedHeight)
		{
			Internal_CreateWebCamTexture(this, deviceName, requestedWidth, requestedHeight, 0);
		}

		public WebCamTexture(string deviceName)
		{
			Internal_CreateWebCamTexture(this, deviceName, 0, 0, 0);
		}

		public WebCamTexture(int requestedWidth, int requestedHeight, int requestedFPS)
		{
			Internal_CreateWebCamTexture(this, "", requestedWidth, requestedHeight, requestedFPS);
		}

		public WebCamTexture(int requestedWidth, int requestedHeight)
		{
			Internal_CreateWebCamTexture(this, "", requestedWidth, requestedHeight, 0);
		}

		public WebCamTexture()
		{
			Internal_CreateWebCamTexture(this, "", 0, 0, 0);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Play();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Pause();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Stop();

		[FreeFunction("WebCamTextureBindings::Internal_GetPixel", HasExplicitThis = true)]
		public Color GetPixel(int x, int y)
		{
			GetPixel_Injected(x, y, out var ret);
			return ret;
		}

		public Color[] GetPixels()
		{
			return GetPixels(0, 0, width, height);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("WebCamTextureBindings::Internal_GetPixels", HasExplicitThis = true, ThrowsException = true)]
		public extern Color[] GetPixels(int x, int y, int blockWidth, int blockHeight);

		[ExcludeFromDocs]
		public Color32[] GetPixels32()
		{
			return GetPixels32(null);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("WebCamTextureBindings::Internal_GetPixels32", HasExplicitThis = true, ThrowsException = true)]
		public extern Color32[] GetPixels32([UnityEngine.Internal.DefaultValue("null")][Unmarshalled] Color32[] colors);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[StaticAccessor("WebCamTextureBindings", StaticAccessorType.DoubleColon)]
		private static extern void Internal_CreateWebCamTexture([Writable] WebCamTexture self, string scriptingDevice, int requestedWidth, int requestedHeight, int maxFramerate);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetPixel_Injected(int x, int y, out Color ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void get_internalAutoFocusPoint_Injected(out Vector2 ret);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[SpecialName]
		private extern void set_internalAutoFocusPoint_Injected(ref Vector2 value);
	}
}
namespace UnityEngine.Experimental.Audio
{
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioClipExtensions.bindings.h")]
	[NativeHeader("Modules/Audio/Public/AudioClip.h")]
	[NativeHeader("AudioScriptingClasses.h")]
	internal static class AudioClipExtensionsInternal
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		public static extern uint Internal_CreateAudioClipSampleProvider([NotNull("NullExceptionObject")] this AudioClip audioClip, ulong start, long end, bool loop, bool allowDrop, bool loopPointIsStart = false);
	}
	[StaticAccessor("AudioSampleProviderBindings", StaticAccessorType.DoubleColon)]
	[NativeType(Header = "Modules/Audio/Public/ScriptBindings/AudioSampleProvider.bindings.h")]
	public class AudioSampleProvider : IDisposable
	{
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint ConsumeSampleFramesNativeFunction(uint providerId, IntPtr interleavedSampleFrames, uint sampleFrameCount);

		public delegate void SampleFramesHandler(AudioSampleProvider provider, uint sampleFrameCount);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void SampleFramesEventNativeFunction(IntPtr userData, uint providerId, uint sampleFrameCount);

		private ConsumeSampleFramesNativeFunction m_ConsumeSampleFramesNativeFunction;

		public uint id { get; private set; }

		public ushort trackIndex { get; private set; }

		public Object owner { get; private set; }

		public bool valid => InternalIsValid(id);

		public ushort channelCount { get; private set; }

		public uint sampleRate { get; private set; }

		public uint maxSampleFrameCount => InternalGetMaxSampleFrameCount(id);

		public uint availableSampleFrameCount => InternalGetAvailableSampleFrameCount(id);

		public uint freeSampleFrameCount => InternalGetFreeSampleFrameCount(id);

		public uint freeSampleFrameCountLowThreshold
		{
			get
			{
				return InternalGetFreeSampleFrameCountLowThreshold(id);
			}
			set
			{
				InternalSetFreeSampleFrameCountLowThreshold(id, value);
			}
		}

		public bool enableSampleFramesAvailableEvents
		{
			get
			{
				return InternalGetEnableSampleFramesAvailableEvents(id);
			}
			set
			{
				InternalSetEnableSampleFramesAvailableEvents(id, value);
			}
		}

		public bool enableSilencePadding
		{
			get
			{
				return InternalGetEnableSilencePadding(id);
			}
			set
			{
				InternalSetEnableSilencePadding(id, value);
			}
		}

		public static ConsumeSampleFramesNativeFunction consumeSampleFramesNativeFunction => (ConsumeSampleFramesNativeFunction)Marshal.GetDelegateForFunctionPointer(InternalGetConsumeSampleFramesNativeFunctionPtr(), typeof(ConsumeSampleFramesNativeFunction));

		public event SampleFramesHandler sampleFramesAvailable;

		public event SampleFramesHandler sampleFramesOverflow;

		[VisibleToOtherModules]
		internal static AudioSampleProvider Lookup(uint providerId, Object ownerObj, ushort trackIndex)
		{
			AudioSampleProvider audioSampleProvider = InternalGetScriptingPtr(providerId);
			if (audioSampleProvider != null || !InternalIsValid(providerId))
			{
				return audioSampleProvider;
			}
			return new AudioSampleProvider(providerId, ownerObj, trackIndex);
		}

		internal static AudioSampleProvider Create(ushort channelCount, uint sampleRate)
		{
			uint providerId = InternalCreateSampleProvider(channelCount, sampleRate);
			if (!InternalIsValid(providerId))
			{
				return null;
			}
			return new AudioSampleProvider(providerId, null, 0);
		}

		private AudioSampleProvider(uint providerId, Object ownerObj, ushort trackIdx)
		{
			owner = ownerObj;
			id = providerId;
			trackIndex = trackIdx;
			m_ConsumeSampleFramesNativeFunction = (ConsumeSampleFramesNativeFunction)Marshal.GetDelegateForFunctionPointer(InternalGetConsumeSampleFramesNativeFunctionPtr(), typeof(ConsumeSampleFramesNativeFunction));
			ushort chCount = 0;
			uint sRate = 0u;
			InternalGetFormatInfo(providerId, out chCount, out sRate);
			channelCount = chCount;
			sampleRate = sRate;
			InternalSetScriptingPtr(providerId, this);
		}

		~AudioSampleProvider()
		{
			owner = null;
			Dispose();
		}

		public void Dispose()
		{
			if (id != 0)
			{
				InternalSetScriptingPtr(id, null);
				if (owner == null)
				{
					InternalRemove(id);
				}
				id = 0u;
			}
			GC.SuppressFinalize(this);
		}

		public unsafe uint ConsumeSampleFrames(NativeArray<float> sampleFrames)
		{
			if (channelCount == 0)
			{
				return 0u;
			}
			return m_ConsumeSampleFramesNativeFunction(id, (IntPtr)sampleFrames.GetUnsafePtr(), (uint)sampleFrames.Length / (uint)channelCount);
		}

		internal unsafe uint QueueSampleFrames(NativeArray<float> sampleFrames)
		{
			if (channelCount == 0)
			{
				return 0u;
			}
			return InternalQueueSampleFrames(id, (IntPtr)sampleFrames.GetUnsafeReadOnlyPtr(), (uint)(sampleFrames.Length / channelCount));
		}

		public void SetSampleFramesAvailableNativeHandler(SampleFramesEventNativeFunction handler, IntPtr userData)
		{
			InternalSetSampleFramesAvailableNativeHandler(id, Marshal.GetFunctionPointerForDelegate(handler), userData);
		}

		public void ClearSampleFramesAvailableNativeHandler()
		{
			InternalClearSampleFramesAvailableNativeHandler(id);
		}

		public void SetSampleFramesOverflowNativeHandler(SampleFramesEventNativeFunction handler, IntPtr userData)
		{
			InternalSetSampleFramesOverflowNativeHandler(id, Marshal.GetFunctionPointerForDelegate(handler), userData);
		}

		public void ClearSampleFramesOverflowNativeHandler()
		{
			InternalClearSampleFramesOverflowNativeHandler(id);
		}

		[RequiredByNativeCode]
		private void InvokeSampleFramesAvailable(int sampleFrameCount)
		{
			if (this.sampleFramesAvailable != null)
			{
				this.sampleFramesAvailable(this, (uint)sampleFrameCount);
			}
		}

		[RequiredByNativeCode]
		private void InvokeSampleFramesOverflow(int droppedSampleFrameCount)
		{
			if (this.sampleFramesOverflow != null)
			{
				this.sampleFramesOverflow(this, (uint)droppedSampleFrameCount);
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsThreadSafe = true)]
		private static extern uint InternalCreateSampleProvider(ushort channelCount, uint sampleRate);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsThreadSafe = true)]
		internal static extern void InternalRemove(uint providerId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsThreadSafe = true)]
		private static extern void InternalGetFormatInfo(uint providerId, out ushort chCount, out uint sRate);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AudioSampleProvider InternalGetScriptingPtr(uint providerId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsThreadSafe = true)]
		private static extern void InternalSetScriptingPtr(uint providerId, AudioSampleProvider provider);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsThreadSafe = true)]
		internal static extern bool InternalIsValid(uint providerId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsThreadSafe = true)]
		private static extern uint InternalGetMaxSampleFrameCount(uint providerId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsThreadSafe = true)]
		private static extern uint InternalGetAvailableSampleFrameCount(uint providerId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsThreadSafe = true)]
		private static extern uint InternalGetFreeSampleFrameCount(uint providerId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsThreadSafe = true)]
		private static extern uint InternalGetFreeSampleFrameCountLowThreshold(uint providerId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsThreadSafe = true)]
		private static extern void InternalSetFreeSampleFrameCountLowThreshold(uint providerId, uint sampleFrameCount);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsThreadSafe = true)]
		private static extern bool InternalGetEnableSampleFramesAvailableEvents(uint providerId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsThreadSafe = true)]
		private static extern void InternalSetEnableSampleFramesAvailableEvents(uint providerId, bool enable);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetSampleFramesAvailableNativeHandler(uint providerId, IntPtr handler, IntPtr userData);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalClearSampleFramesAvailableNativeHandler(uint providerId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetSampleFramesOverflowNativeHandler(uint providerId, IntPtr handler, IntPtr userData);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalClearSampleFramesOverflowNativeHandler(uint providerId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsThreadSafe = true)]
		private static extern bool InternalGetEnableSilencePadding(uint id);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsThreadSafe = true)]
		private static extern void InternalSetEnableSilencePadding(uint id, bool enabled);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsThreadSafe = true)]
		private static extern IntPtr InternalGetConsumeSampleFramesNativeFunctionPtr();

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsThreadSafe = true)]
		private static extern uint InternalQueueSampleFrames(uint id, IntPtr interleavedSampleFrames, uint sampleFrameCount);
	}
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioSampleProviderExtensions.bindings.h")]
	[StaticAccessor("AudioSampleProviderExtensionsBindings", StaticAccessorType.DoubleColon)]
	internal static class AudioSampleProviderExtensionsInternal
	{
		public static float GetSpeed(this AudioSampleProvider provider)
		{
			return InternalGetAudioSampleProviderSpeed(provider.id);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		private static extern float InternalGetAudioSampleProviderSpeed(uint providerId);
	}
	[NativeHeader("Modules/Audio/Public/AudioSource.h")]
	[NativeHeader("AudioScriptingClasses.h")]
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioSourceExtensions.bindings.h")]
	internal static class AudioSourceExtensionsInternal
	{
		public static void RegisterSampleProvider(this AudioSource source, AudioSampleProvider provider)
		{
			Internal_RegisterSampleProviderWithAudioSource(source, provider.id);
		}

		public static void UnregisterSampleProvider(this AudioSource source, AudioSampleProvider provider)
		{
			Internal_UnregisterSampleProviderFromAudioSource(source, provider.id);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		private static extern void Internal_RegisterSampleProviderWithAudioSource([NotNull("NullExceptionObject")] AudioSource source, uint providerId);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		private static extern void Internal_UnregisterSampleProviderFromAudioSource([NotNull("NullExceptionObject")] AudioSource source, uint providerId);
	}
}
namespace UnityEngine.Audio
{
	[NativeHeader("Modules/Audio/Public/ScriptBindings/Audio.bindings.h")]
	internal sealed class AudioManagerTestProxy
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod(Name = "AudioManagerTestProxy::ComputeAudibilityConsistency", IsFreeFunction = true)]
		internal static extern bool ComputeAudibilityConsistency();
	}
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioClipPlayable.bindings.h")]
	[NativeHeader("Modules/Audio/Public/Director/AudioClipPlayable.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[StaticAccessor("AudioClipPlayableBindings", StaticAccessorType.DoubleColon)]
	[RequiredByNativeCode]
	public struct AudioClipPlayable : IPlayable, IEquatable<AudioClipPlayable>
	{
		private PlayableHandle m_Handle;

		public static AudioClipPlayable Create(PlayableGraph graph, AudioClip clip, bool looping)
		{
			PlayableHandle handle = CreateHandle(graph, clip, looping);
			AudioClipPlayable audioClipPlayable = new AudioClipPlayable(handle);
			if (clip != null)
			{
				audioClipPlayable.SetDuration(clip.length);
			}
			return audioClipPlayable;
		}

		private static PlayableHandle CreateHandle(PlayableGraph graph, AudioClip clip, bool looping)
		{
			PlayableHandle handle = PlayableHandle.Null;
			if (!InternalCreateAudioClipPlayable(ref graph, clip, looping, ref handle))
			{
				return PlayableHandle.Null;
			}
			return handle;
		}

		internal AudioClipPlayable(PlayableHandle handle)
		{
			if (handle.IsValid() && !handle.IsPlayableOfType<AudioClipPlayable>())
			{
				throw new InvalidCastException("Can't set handle: the playable is not an AudioClipPlayable.");
			}
			m_Handle = handle;
		}

		public PlayableHandle GetHandle()
		{
			return m_Handle;
		}

		public static implicit operator Playable(AudioClipPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		public static explicit operator AudioClipPlayable(Playable playable)
		{
			return new AudioClipPlayable(playable.GetHandle());
		}

		public bool Equals(AudioClipPlayable other)
		{
			return GetHandle() == other.GetHandle();
		}

		public AudioClip GetClip()
		{
			return GetClipInternal(ref m_Handle);
		}

		public void SetClip(AudioClip value)
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

		internal float GetVolume()
		{
			return GetVolumeInternal(ref m_Handle);
		}

		internal void SetVolume(float value)
		{
			if (value < 0f || value > 1f)
			{
				throw new ArgumentException("Trying to set AudioClipPlayable volume outside of range (0.0 - 1.0): " + value);
			}
			SetVolumeInternal(ref m_Handle, value);
		}

		internal float GetStereoPan()
		{
			return GetStereoPanInternal(ref m_Handle);
		}

		internal void SetStereoPan(float value)
		{
			if (value < -1f || value > 1f)
			{
				throw new ArgumentException("Trying to set AudioClipPlayable stereo pan outside of range (-1.0 - 1.0): " + value);
			}
			SetStereoPanInternal(ref m_Handle, value);
		}

		internal float GetSpatialBlend()
		{
			return GetSpatialBlendInternal(ref m_Handle);
		}

		internal void SetSpatialBlend(float value)
		{
			if (value < 0f || value > 1f)
			{
				throw new ArgumentException("Trying to set AudioClipPlayable spatial blend outside of range (0.0 - 1.0): " + value);
			}
			SetSpatialBlendInternal(ref m_Handle, value);
		}

		[Obsolete("IsPlaying() has been deprecated. Use IsChannelPlaying() instead (UnityUpgradable) -> IsChannelPlaying()", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool IsPlaying()
		{
			return IsChannelPlaying();
		}

		public bool IsChannelPlaying()
		{
			return GetIsChannelPlayingInternal(ref m_Handle);
		}

		public double GetStartDelay()
		{
			return GetStartDelayInternal(ref m_Handle);
		}

		internal void SetStartDelay(double value)
		{
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
				throw new ArgumentException("AudioClipPlayable.pauseDelay: Setting new delay when existing delay is too small or 0.0 (" + pauseDelayInternal + "), audio system will not be able to change in time");
			}
			SetPauseDelayInternal(ref m_Handle, value);
		}

		public void Seek(double startTime, double startDelay)
		{
			Seek(startTime, startDelay, 0.0);
		}

		public void Seek(double startTime, double startDelay, [System.ComponentModel.DefaultValue("0")] double duration)
		{
			SetStartDelayInternal(ref m_Handle, startDelay);
			if (duration > 0.0)
			{
				double num = startDelay + duration;
				if (num >= m_Handle.GetDuration())
				{
					m_Handle.SetDone(value: true);
				}
				m_Handle.SetDuration(duration + startTime);
				SetPauseDelayInternal(ref m_Handle, startDelay + duration);
			}
			else
			{
				m_Handle.SetDone(value: true);
				m_Handle.SetDuration(double.MaxValue);
				SetPauseDelayInternal(ref m_Handle, 0.0);
			}
			m_Handle.SetTime(startTime);
			m_Handle.Play();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern AudioClip GetClipInternal(ref PlayableHandle hdl);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetClipInternal(ref PlayableHandle hdl, AudioClip clip);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool GetLoopedInternal(ref PlayableHandle hdl);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetLoopedInternal(ref PlayableHandle hdl, bool looped);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern float GetVolumeInternal(ref PlayableHandle hdl);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetVolumeInternal(ref PlayableHandle hdl, float volume);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern float GetStereoPanInternal(ref PlayableHandle hdl);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetStereoPanInternal(ref PlayableHandle hdl, float stereoPan);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern float GetSpatialBlendInternal(ref PlayableHandle hdl);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void SetSpatialBlendInternal(ref PlayableHandle hdl, float spatialBlend);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool GetIsChannelPlayingInternal(ref PlayableHandle hdl);

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
		private static extern bool InternalCreateAudioClipPlayable(ref PlayableGraph graph, AudioClip clip, bool looping, ref PlayableHandle handle);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool ValidateType(ref PlayableHandle hdl);
	}
	public enum AudioMixerUpdateMode
	{
		Normal,
		UnscaledTime
	}
	[ExcludeFromPreset]
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioMixer.bindings.h")]
	[NativeHeader("Modules/Audio/Public/AudioMixer.h")]
	[ExcludeFromObjectFactory]
	public class AudioMixer : Object
	{
		[NativeProperty]
		public extern AudioMixerGroup outputAudioMixerGroup
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		[NativeProperty]
		public extern AudioMixerUpdateMode updateMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		internal AudioMixer()
		{
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("FindSnapshotFromName")]
		public extern AudioMixerSnapshot FindSnapshot(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("AudioMixerBindings::FindMatchingGroups", IsFreeFunction = true, HasExplicitThis = true)]
		public extern AudioMixerGroup[] FindMatchingGroups(string subPath);

		internal void TransitionToSnapshot(AudioMixerSnapshot snapshot, float timeToReach)
		{
			if (snapshot == null)
			{
				throw new ArgumentException("null Snapshot passed to AudioMixer.TransitionToSnapshot of AudioMixer '" + base.name + "'");
			}
			if (snapshot.audioMixer != this)
			{
				throw new ArgumentException("Snapshot '" + snapshot.name + "' passed to AudioMixer.TransitionToSnapshot is not a snapshot from AudioMixer '" + base.name + "'");
			}
			TransitionToSnapshotInternal(snapshot, timeToReach);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("TransitionToSnapshot")]
		private extern void TransitionToSnapshotInternal(AudioMixerSnapshot snapshot, float timeToReach);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("AudioMixerBindings::TransitionToSnapshots", IsFreeFunction = true, HasExplicitThis = true, ThrowsException = true)]
		public extern void TransitionToSnapshots(AudioMixerSnapshot[] snapshots, float[] weights, float timeToReach);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod]
		public extern bool SetFloat(string name, float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod]
		public extern bool ClearFloat(string name);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod]
		public extern bool GetFloat(string name, out float value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("AudioMixerBindings::GetAbsoluteAudibilityFromGroup", HasExplicitThis = true, IsFreeFunction = true)]
		internal extern float GetAbsoluteAudibilityFromGroup(AudioMixerGroup group);
	}
	[NativeHeader("Modules/Audio/Public/AudioMixerGroup.h")]
	public class AudioMixerGroup : Object, ISubAssetNotDuplicatable
	{
		[NativeProperty]
		public extern AudioMixer audioMixer
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal AudioMixerGroup()
		{
		}
	}
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Modules/Audio/Public/Director/AudioMixerPlayable.h")]
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioMixerPlayable.bindings.h")]
	[StaticAccessor("AudioMixerPlayableBindings", StaticAccessorType.DoubleColon)]
	public struct AudioMixerPlayable : IPlayable, IEquatable<AudioMixerPlayable>
	{
		private PlayableHandle m_Handle;

		public static AudioMixerPlayable Create(PlayableGraph graph, int inputCount = 0, bool normalizeInputVolumes = false)
		{
			PlayableHandle handle = CreateHandle(graph, inputCount, normalizeInputVolumes);
			return new AudioMixerPlayable(handle);
		}

		private static PlayableHandle CreateHandle(PlayableGraph graph, int inputCount, bool normalizeInputVolumes)
		{
			PlayableHandle handle = PlayableHandle.Null;
			if (!CreateAudioMixerPlayableInternal(ref graph, normalizeInputVolumes, ref handle))
			{
				return PlayableHandle.Null;
			}
			handle.SetInputCount(inputCount);
			return handle;
		}

		internal AudioMixerPlayable(PlayableHandle handle)
		{
			if (handle.IsValid() && !handle.IsPlayableOfType<AudioMixerPlayable>())
			{
				throw new InvalidCastException("Can't set handle: the playable is not an AudioMixerPlayable.");
			}
			m_Handle = handle;
		}

		public PlayableHandle GetHandle()
		{
			return m_Handle;
		}

		public static implicit operator Playable(AudioMixerPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		public static explicit operator AudioMixerPlayable(Playable playable)
		{
			return new AudioMixerPlayable(playable.GetHandle());
		}

		public bool Equals(AudioMixerPlayable other)
		{
			return GetHandle() == other.GetHandle();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool CreateAudioMixerPlayableInternal(ref PlayableGraph graph, bool normalizeInputVolumes, ref PlayableHandle handle);
	}
	[NativeHeader("Modules/Audio/Public/AudioMixerSnapshot.h")]
	public class AudioMixerSnapshot : Object, ISubAssetNotDuplicatable
	{
		[NativeProperty]
		public extern AudioMixer audioMixer
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		internal AudioMixerSnapshot()
		{
		}

		public void TransitionTo(float timeToReach)
		{
			audioMixer.TransitionToSnapshot(this, timeToReach);
		}
	}
	public static class AudioPlayableBinding
	{
		public static PlayableBinding Create(string name, Object key)
		{
			return PlayableBinding.CreateInternal(name, key, typeof(AudioSource), CreateAudioOutput);
		}

		private static PlayableOutput CreateAudioOutput(PlayableGraph graph, string name)
		{
			return AudioPlayableOutput.Create(graph, name, null);
		}
	}
	[StaticAccessor("AudioPlayableGraphExtensionsBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Director/Core/HPlayableOutput.h")]
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioPlayableGraphExtensions.bindings.h")]
	internal static class AudioPlayableGraphExtensions
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		internal static extern bool InternalCreateAudioOutput(ref PlayableGraph graph, string name, out PlayableOutputHandle handle);
	}
	[RequiredByNativeCode]
	[StaticAccessor("AudioPlayableOutputBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Audio/Public/AudioSource.h")]
	[NativeHeader("Modules/Audio/Public/Director/AudioPlayableOutput.h")]
	[NativeHeader("Modules/Audio/Public/ScriptBindings/AudioPlayableOutput.bindings.h")]
	public struct AudioPlayableOutput : IPlayableOutput
	{
		private PlayableOutputHandle m_Handle;

		public static AudioPlayableOutput Null => new AudioPlayableOutput(PlayableOutputHandle.Null);

		public static AudioPlayableOutput Create(PlayableGraph graph, string name, AudioSource target)
		{
			if (!AudioPlayableGraphExtensions.InternalCreateAudioOutput(ref graph, name, out var handle))
			{
				return Null;
			}
			AudioPlayableOutput result = new AudioPlayableOutput(handle);
			result.SetTarget(target);
			return result;
		}

		internal AudioPlayableOutput(PlayableOutputHandle handle)
		{
			if (handle.IsValid() && !handle.IsPlayableOutputOfType<AudioPlayableOutput>())
			{
				throw new InvalidCastException("Can't set handle: the playable is not an AudioPlayableOutput.");
			}
			m_Handle = handle;
		}

		public PlayableOutputHandle GetHandle()
		{
			return m_Handle;
		}

		public static implicit operator PlayableOutput(AudioPlayableOutput output)
		{
			return new PlayableOutput(output.GetHandle());
		}

		public static explicit operator AudioPlayableOutput(PlayableOutput output)
		{
			return new AudioPlayableOutput(output.GetHandle());
		}

		public AudioSource GetTarget()
		{
			return InternalGetTarget(ref m_Handle);
		}

		public void SetTarget(AudioSource value)
		{
			InternalSetTarget(ref m_Handle, value);
		}

		public bool GetEvaluateOnSeek()
		{
			return InternalGetEvaluateOnSeek(ref m_Handle);
		}

		public void SetEvaluateOnSeek(bool value)
		{
			InternalSetEvaluateOnSeek(ref m_Handle, value);
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern AudioSource InternalGetTarget(ref PlayableOutputHandle output);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void InternalSetTarget(ref PlayableOutputHandle output, AudioSource target);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern bool InternalGetEvaluateOnSeek(ref PlayableOutputHandle output);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeThrows]
		private static extern void InternalSetEvaluateOnSeek(ref PlayableOutputHandle output, bool value);
	}
}
namespace Unity.Audio
{
	[VisibleToOtherModules]
	internal interface IHandle<HandleType> : IValidatable, IEquatable<HandleType> where HandleType : struct, IHandle<HandleType>
	{
	}
	[VisibleToOtherModules]
	internal interface IValidatable
	{
		bool Valid { get; }
	}
}
