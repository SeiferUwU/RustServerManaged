using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.Bindings;

[assembly: InternalsVisibleTo("UnityEngine.VirtualTexturingModule")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework.Tests")]
[assembly: InternalsVisibleTo("Unity.PerformanceTests.RuntimeTestRunner.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.AllIn1Runner")]
[assembly: InternalsVisibleTo("Unity.Timeline")]
[assembly: InternalsVisibleTo("Assembly-CSharp-testable")]
[assembly: InternalsVisibleTo("Assembly-CSharp-firstpass-testable")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Framework")]
[assembly: InternalsVisibleTo("UnityEngine.SpatialTracking")]
[assembly: InternalsVisibleTo("Unity.WindowsMRAutomation")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.Editor")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.EditorTests")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.Editor")]
[assembly: InternalsVisibleTo("UnityEditor.UIBuilderModule")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.EditorTests")]
[assembly: InternalsVisibleTo("Unity.UIElements")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsGameObjectsModule")]
[assembly: InternalsVisibleTo("GoogleAR.UnityNative")]
[assembly: InternalsVisibleTo("Unity.UIElements.Editor")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Timeline")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests")]
[assembly: InternalsVisibleTo("UnityEngine.XboxOneModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS4Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS4VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS5Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS5VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.Networking")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud.Service")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.UnityAnalytics")]
[assembly: InternalsVisibleTo("Unity.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommon")]
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
[assembly: InternalsVisibleTo("UnityEngine.Purchasing")]
[assembly: InternalsVisibleTo("UnityEngine.TestRunner")]
[assembly: InternalsVisibleTo("Unity.Automation")]
[assembly: InternalsVisibleTo("Unity.Burst")]
[assembly: InternalsVisibleTo("Unity.Burst.Editor")]
[assembly: InternalsVisibleTo("Unity.DeploymentTests.Services")]
[assembly: InternalsVisibleTo("UnityEngine.Analytics")]
[assembly: InternalsVisibleTo("Unity.UIElements.PlayModeTests")]
[assembly: InternalsVisibleTo("UnityEngine.UIElements.Tests")]
[assembly: InternalsVisibleTo("Unity.UIElements.EditorTests")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.013")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.014")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.015")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.016")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.017")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.018")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.019")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.020")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.012")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.021")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.023")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.024")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.001")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.005")]
[assembly: InternalsVisibleTo("Unity.Subsystem.Registration")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.022")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.011")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.010")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.009")]
[assembly: InternalsVisibleTo("UnityEngine.UI")]
[assembly: InternalsVisibleTo("Unity.Networking.Transport")]
[assembly: InternalsVisibleTo("Unity.ucg.QoS")]
[assembly: InternalsVisibleTo("Unity.Services.QoS")]
[assembly: InternalsVisibleTo("Unity.Logging")]
[assembly: InternalsVisibleTo("Unity.Entities")]
[assembly: InternalsVisibleTo("Unity.Entities.Tests")]
[assembly: InternalsVisibleTo("Unity.Collections")]
[assembly: InternalsVisibleTo("Unity.Runtime")]
[assembly: InternalsVisibleTo("Unity.Core")]
[assembly: InternalsVisibleTo("UnityEngine.Core.Runtime.Tests")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.001")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.006")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.007")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.008")]
[assembly: InternalsVisibleTo("UnityEngine.SwitchModule")]
[assembly: InternalsVisibleTo("UnityEngine.WindModule")]
[assembly: InternalsVisibleTo("UnityEngine.VideoModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestModule")]
[assembly: InternalsVisibleTo("UnityEngine.GridModule")]
[assembly: InternalsVisibleTo("UnityEngine.GameCenterModule")]
[assembly: InternalsVisibleTo("UnityEngine.ImageConversionModule")]
[assembly: InternalsVisibleTo("UnityEngine.GIModule")]
[assembly: InternalsVisibleTo("UnityEngine.DirectorModule")]
[assembly: InternalsVisibleTo("UnityEngine.DSPGraphModule")]
[assembly: InternalsVisibleTo("UnityEngine.CrashReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsModule")]
[assembly: UnityEngineModuleAssembly]
[assembly: InternalsVisibleTo("UnityEngine.TLSModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommonModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityConnectModule")]
[assembly: InternalsVisibleTo("UnityEngine.ContentLoadModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterRendererModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClothModule")]
[assembly: InternalsVisibleTo("UnityEngine.AudioModule")]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: InternalsVisibleTo("UnityEngine")]
[assembly: InternalsVisibleTo("UnityEngine.SharedInternalsModule")]
[assembly: InternalsVisibleTo("UnityEngine.CoreModule")]
[assembly: InternalsVisibleTo("UnityEngine.AIModule")]
[assembly: InternalsVisibleTo("UnityEngine.PhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextRenderingModule")]
[assembly: InternalsVisibleTo("UnityEngine.JSONSerializeModule")]
[assembly: InternalsVisibleTo("UnityEngine.ARModule")]
[assembly: InternalsVisibleTo("UnityEngine.AccessibilityModule")]
[assembly: InternalsVisibleTo("UnityEngine.AndroidJNIModule")]
[assembly: InternalsVisibleTo("UnityEngine.AnimationModule")]
[assembly: InternalsVisibleTo("UnityEngine.HotReloadModule")]
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputModule")]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: InternalsVisibleTo("UnityEngine.InputLegacyModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreTextEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.VehiclesModule")]
[assembly: InternalsVisibleTo("UnityEngine.VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.XRModule")]
[assembly: InternalsVisibleTo("UnityEngine.VFXModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestWWWModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestTextureModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityTestProtocolModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityCurlModule")]
[assembly: InternalsVisibleTo("UnityEngine.UmbraModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIModule")]
[assembly: InternalsVisibleTo("UnityEngine.TilemapModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainPhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubsystemsModule")]
[assembly: InternalsVisibleTo("UnityEngine.IMGUIModule")]
[assembly: InternalsVisibleTo("UnityEngine.LocalizationModule")]
[assembly: InternalsVisibleTo("UnityEngine.NVIDIAModule")]
[assembly: InternalsVisibleTo("UnityEngine.ParticleSystemModule")]
[assembly: InternalsVisibleTo("UnityEngine.PerformanceReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.Physics2DModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreFontEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.ProfilerModule")]
[assembly: InternalsVisibleTo("UnityEngine.RuntimeInitializeOnLoadManagerInitializerModule")]
[assembly: InternalsVisibleTo("UnityEngine.ScreenCaptureModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteMaskModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteShapeModule")]
[assembly: InternalsVisibleTo("UnityEngine.StreamingModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubstanceModule")]
[assembly: InternalsVisibleTo("UnityEngine.PropertiesModule")]
[assembly: CompilationRelaxations(8)]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace Unity.TLS
{
	internal static class UnityTLSNativeLibrary
	{
	}
}
namespace Unity.TLS.LowLevel
{
	[NativeHeader("External/unitytls/builds/CSharp/BindingsUnity/TLSAgent.gen.bindings.h")]
	internal static class Binding
	{
		public struct unitytls_errorstate
		{
			public uint magic;

			public uint code;

			public ulong reserved;
		}

		public struct unitytls_dataRef
		{
			public unsafe byte* dataPtr;

			public UIntPtr dataLen;
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate void unitytls_client_on_data_callback(IntPtr arg0, byte* arg1, UIntPtr arg2, uint arg3);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate int unitytls_client_data_send_callback(IntPtr arg0, byte* arg1, UIntPtr arg2, uint arg3);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate int unitytls_client_data_receive_callback(IntPtr arg0, byte* arg1, UIntPtr arg2, uint arg3);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate int unitytls_client_data_receive_timeout_callback(IntPtr arg0, byte* arg1, UIntPtr arg2, uint arg3, uint arg4);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate void unitytls_client_log_callback(int arg0, byte* arg1, UIntPtr arg2, byte* arg3, byte* arg4, UIntPtr arg5);

		public struct unitytls_client_config
		{
			public unitytls_dataRef caPEM;

			public unitytls_dataRef serverPEM;

			public unitytls_dataRef privateKeyPEM;

			public uint clientAuth;

			public uint transportProtocol;

			public unitytls_dataRef psk;

			public unitytls_dataRef pskIdentity;

			public IntPtr onDataCB;

			public IntPtr dataSendCB;

			public IntPtr dataReceiveCB;

			public IntPtr dataReceiveTimeoutCB;

			public IntPtr transportUserData;

			public IntPtr applicationUserData;

			public int handshakeReturnsOnStep;

			public int handshakeReturnsIfWouldBlock;

			public uint ssl_read_timeout_ms;

			public unsafe byte* hostname;

			public uint tracelevel;

			public IntPtr logCallback;

			public uint ssl_handshake_timeout_min;

			public uint ssl_handshake_timeout_max;

			public ushort mtu;
		}

		public struct unitytls_client
		{
			public uint role;

			public uint state;

			public uint handshakeState;

			public IntPtr ctx;

			public unsafe unitytls_client_config* config;

			public IntPtr internalCtx;
		}

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate int unitytls_tlsctx_handshake_on_blocking_callback(unitytls_client* arg0, IntPtr arg1, int arg2);

		public const int UNITYTLS_SUCCESS = 0;

		public const int UNITYTLS_INVALID_ARGUMENT = 1;

		public const int UNITYTLS_INVALID_FORMAT = 2;

		public const int UNITYTLS_INVALID_PASSWORD = 3;

		public const int UNITYTLS_INVALID_STATE = 4;

		public const int UNITYTLS_BUFFER_OVERFLOW = 5;

		public const int UNITYTLS_OUT_OF_MEMORY = 6;

		public const int UNITYTLS_INTERNAL_ERROR = 7;

		public const int UNITYTLS_NOT_SUPPORTED = 8;

		public const int UNITYTLS_ENTROPY_SOURCE_FAILED = 9;

		public const int UNITYTLS_STREAM_CLOSED = 10;

		public const int UNITYTLS_DER_PARSE_ERROR = 11;

		public const int UNITYTLS_KEY_PARSE_ERROR = 12;

		public const int UNITYTLS_SSL_ERROR = 13;

		public const int UNITYTLS_USER_CUSTOM_ERROR_START = 1048576;

		public const int UNITYTLS_USER_WOULD_BLOCK = 1048577;

		public const int UNITYTLS_USER_WOULD_BLOCK_READ = 1048578;

		public const int UNITYTLS_USER_WOULD_BLOCK_WRITE = 1048579;

		public const int UNITYTLS_USER_READ_FAILED = 1048580;

		public const int UNITYTLS_USER_WRITE_FAILED = 1048581;

		public const int UNITYTLS_USER_UNKNOWN_ERROR = 1048582;

		public const int UNITYTLS_SSL_NEEDS_VERIFY = 1048583;

		public const int UNITYTLS_HANDSHAKE_STEP = 1048584;

		public const int UNITYTLS_USER_CUSTOM_ERROR_END = 2097152;

		public const int UNITYTLS_LOGLEVEL_MIN = 0;

		public const int UNITYTLS_LOGLEVEL_FATAL = 0;

		public const int UNITYTLS_LOGLEVEL_ERROR = 1;

		public const int UNITYTLS_LOGLEVEL_WARN = 2;

		public const int UNITYTLS_LOGLEVEL_INFO = 3;

		public const int UNITYTLS_LOGLEVEL_DEBUG = 4;

		public const int UNITYTLS_LOGLEVEL_TRACE = 5;

		public const int UNITYTLS_LOGLEVEL_MAX = 5;

		public const int UNITYTLS_SSL_HANDSHAKE_HELLO_REQUEST = 0;

		public const int UNITYTLS_SSL_HANDSHAKE_CLIENT_HELLO = 1;

		public const int UNITYTLS_SSL_HANDSHAKE_SERVER_HELLO = 2;

		public const int UNITYTLS_SSL_HANDSHAKE_SERVER_CERTIFICATE = 3;

		public const int UNITYTLS_SSL_HANDSHAKE_SERVER_KEY_EXCHANGE = 4;

		public const int UNITYTLS_SSL_HANDSHAKE_CERTIFICATE_REQUEST = 5;

		public const int UNITYTLS_SSL_HANDSHAKE_SERVER_HELLO_DONE = 6;

		public const int UNITYTLS_SSL_HANDSHAKE_CLIENT_CERTIFICATE = 7;

		public const int UNITYTLS_SSL_HANDSHAKE_CLIENT_KEY_EXCHANGE = 8;

		public const int UNITYTLS_SSL_HANDSHAKE_CERTIFICATE_VERIFY = 9;

		public const int UNITYTLS_SSL_HANDSHAKE_CLIENT_CHANGE_CIPHER_SPEC = 10;

		public const int UNITYTLS_SSL_HANDSHAKE_CLIENT_FINISHED = 11;

		public const int UNITYTLS_SSL_HANDSHAKE_SERVER_CHANGE_CIPHER_SPEC = 12;

		public const int UNITYTLS_SSL_HANDSHAKE_SERVER_FINISHED = 13;

		public const int UNITYTLS_SSL_HANDSHAKE_FLUSH_BUFFERS = 14;

		public const int UNITYTLS_SSL_HANDSHAKE_WRAPUP = 15;

		public const int UNITYTLS_SSL_HANDSHAKE_OVER = 16;

		public const int UNITYTLS_SSL_HANDSHAKE_SERVER_NEW_SESSION_TICKET = 17;

		public const int UNITYTLS_SSL_HANDSHAKE_HELLO_VERIFY_REQUIRED = 18;

		public const int UNITYTLS_SSL_HANDSHAKE_COUNT = 19;

		public const int UNITYTLS_SSL_HANDSHAKE_BEGIN = 0;

		public const int UNITYTLS_SSL_HANDSHAKE_DONE = 16;

		public const int UNITYTLS_SSL_HANDSHAKE_HANDSHAKE_FLUSH_BUFFERS = 14;

		public const int UNITYTLS_SSL_HANDSHAKE_HANDSHAKE_WRAPUP = 15;

		public const int UNITYTLS_SSL_HANDSHAKE_HANDSHAKE_OVER = 16;

		public const int UnityTLSClientAuth_None = 0;

		public const int UnityTLSClientAuth_Optional = 1;

		public const int UnityTLSClientAuth_Required = 2;

		public const int UnityTLSRole_None = 0;

		public const int UnityTLSRole_Server = 1;

		public const int UnityTLSRole_Client = 2;

		public const int UnityTLSTransportProtocol_Stream = 0;

		public const int UnityTLSTransportProtocol_Datagram = 1;

		public const int UnityTLSClientState_None = 0;

		public const int UnityTLSClientState_Init = 1;

		public const int UnityTLSClientState_Handshake = 2;

		public const int UnityTLSClientState_Messaging = 3;

		public const int UnityTLSClientState_Fail = 64;

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static extern uint unitytls_client_send_data(unitytls_client* clientInstance, byte* data, UIntPtr dataLen);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static extern uint unitytls_client_read_data(unitytls_client* clientInstance, byte* buffer, UIntPtr bufferLen, UIntPtr* bytesRead);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static extern void unitytls_client_add_ciphersuite(unitytls_client* clientInstance, uint suite);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static extern uint unitytls_client_get_ciphersuite(unitytls_client* clientInstance, int ndx);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static extern int unitytls_client_get_ciphersuite_cnt(unitytls_client* clientInstance);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static extern void unitytls_client_init_config(unitytls_client_config* config);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static extern unitytls_client* unitytls_client_create(uint role, unitytls_client_config* config);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static extern void unitytls_client_destroy(unitytls_client* clientInstance);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static extern int unitytls_client_init(unitytls_client* clientInstance);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static extern uint unitytls_client_handshake(unitytls_client* clientInstance);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static extern uint unitytls_client_set_cookie_info(unitytls_client* clientInstance, byte* peerIdDataPtr, int peerIdDataLen);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static extern uint unitytls_client_get_handshake_state(unitytls_client* clientInstance);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static extern uint unitytls_client_get_errorsState(unitytls_client* clientInstance, ulong* reserved);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static extern uint unitytls_client_get_state(unitytls_client* clientInstance);

		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static extern uint unitytls_client_get_role(unitytls_client* clientInstance);
	}
}
