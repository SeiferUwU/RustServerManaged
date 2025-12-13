using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Bindings;

[assembly: InternalsVisibleTo("UnityEngine.ScreenCaptureModule")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud.Service")]
[assembly: InternalsVisibleTo("Unity.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommon")]
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
[assembly: InternalsVisibleTo("UnityEngine.Purchasing")]
[assembly: InternalsVisibleTo("UnityEngine.TestRunner")]
[assembly: InternalsVisibleTo("Unity.Automation")]
[assembly: InternalsVisibleTo("Unity.Burst")]
[assembly: InternalsVisibleTo("Unity.Burst.Editor")]
[assembly: InternalsVisibleTo("UnityEngine.Networking")]
[assembly: InternalsVisibleTo("Unity.DeploymentTests.Services")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.UnityAnalytics")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Timeline")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Framework")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework.Tests")]
[assembly: InternalsVisibleTo("Unity.PerformanceTests.RuntimeTestRunner.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.AllIn1Runner")]
[assembly: InternalsVisibleTo("Unity.Timeline")]
[assembly: InternalsVisibleTo("Assembly-CSharp-testable")]
[assembly: InternalsVisibleTo("Assembly-CSharp-firstpass-testable")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests")]
[assembly: InternalsVisibleTo("UnityEngine.PS5VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS5Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS4VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.StreamingModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubstanceModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubsystemsModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainPhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.TilemapModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UmbraModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityCurlModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.SpatialTracking")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteShapeModule")]
[assembly: InternalsVisibleTo("GoogleAR.UnityNative")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.Editor")]
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
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.008")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.020")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.022")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.023")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.024")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.001")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.005")]
[assembly: InternalsVisibleTo("Unity.Subsystem.Registration")]
[assembly: UnityEngineModuleAssembly]
[assembly: InternalsVisibleTo("Unity.Audio.DSPGraph")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.021")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.007")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.006")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.005")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.EditorTests")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.Editor")]
[assembly: InternalsVisibleTo("UnityEditor.UIBuilderModule")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.EditorTests")]
[assembly: InternalsVisibleTo("Unity.UIElements")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsGameObjectsModule")]
[assembly: InternalsVisibleTo("Unity.UIElements.Editor")]
[assembly: InternalsVisibleTo("Unity.UIElements.PlayModeTests")]
[assembly: InternalsVisibleTo("UnityEngine.UIElements.Tests")]
[assembly: InternalsVisibleTo("Unity.UIElements.EditorTests")]
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
[assembly: InternalsVisibleTo("Unity.WindowsMRAutomation")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteMaskModule")]
[assembly: InternalsVisibleTo("UnityEngine.RuntimeInitializeOnLoadManagerInitializerModule")]
[assembly: InternalsVisibleTo("Unity.Audio.DSPGraph.Tests")]
[assembly: InternalsVisibleTo("UnityEngine.ContentLoadModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterRendererModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClothModule")]
[assembly: InternalsVisibleTo("UnityEngine.AudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.HotReloadModule")]
[assembly: InternalsVisibleTo("UnityEngine.AnimationModule")]
[assembly: InternalsVisibleTo("UnityEngine.AndroidJNIModule")]
[assembly: InternalsVisibleTo("UnityEngine.AccessibilityModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityConnectModule")]
[assembly: InternalsVisibleTo("UnityEngine.ARModule")]
[assembly: InternalsVisibleTo("UnityEngine.JSONSerializeModule")]
[assembly: InternalsVisibleTo("UnityEngine.PhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.AIModule")]
[assembly: InternalsVisibleTo("UnityEngine.CoreModule")]
[assembly: InternalsVisibleTo("UnityEngine.SharedInternalsModule")]
[assembly: InternalsVisibleTo("UnityEngine")]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: CompilationRelaxations(8)]
[assembly: InternalsVisibleTo("UnityEngine.InputModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommonModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestModule")]
[assembly: InternalsVisibleTo("UnityEngine.PropertiesModule")]
[assembly: InternalsVisibleTo("UnityEngine.ProfilerModule")]
[assembly: InternalsVisibleTo("UnityEngine.Physics2DModule")]
[assembly: InternalsVisibleTo("UnityEngine.PerformanceReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.ParticleSystemModule")]
[assembly: InternalsVisibleTo("UnityEngine.NVIDIAModule")]
[assembly: InternalsVisibleTo("UnityEngine.TLSModule")]
[assembly: InternalsVisibleTo("UnityEngine.IMGUIModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreTextEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreFontEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.LocalizationModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextRenderingModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputLegacyModule")]
[assembly: InternalsVisibleTo("UnityEngine.CrashReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.DSPGraphModule")]
[assembly: InternalsVisibleTo("UnityEngine.DirectorModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsModule")]
[assembly: InternalsVisibleTo("UnityEngine.ImageConversionModule")]
[assembly: InternalsVisibleTo("UnityEngine.GameCenterModule")]
[assembly: InternalsVisibleTo("UnityEngine.GridModule")]
[assembly: InternalsVisibleTo("UnityEngine.GIModule")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace Unity.Audio;

[NativeType(Header = "Modules/DSPGraph/Public/DSPGraphHandles.h")]
internal struct Handle : IHandle<Handle>, IValidatable, IEquatable<Handle>
{
	internal struct Node
	{
		public long Next;

		public int Id;

		public int Version;

		public int DidAllocate;

		public const int InvalidId = -1;
	}

	[NativeDisableUnsafePtrRestriction]
	private IntPtr m_Node;

	public int Version;

	public unsafe Node* AtomicNode
	{
		get
		{
			return (Node*)(void*)m_Node;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			m_Node = (IntPtr)value;
			Version = value->Version;
		}
	}

	public unsafe int Id
	{
		get
		{
			return Valid ? AtomicNode->Id : (-1);
		}
		set
		{
			if (value == -1)
			{
				throw new ArgumentException("Invalid ID");
			}
			if (!Valid)
			{
				throw new InvalidOperationException("Handle is invalid or has been destroyed");
			}
			if (AtomicNode->Id != -1)
			{
				throw new InvalidOperationException($"Trying to overwrite id on live node {AtomicNode->Id}");
			}
			AtomicNode->Id = value;
		}
	}

	public unsafe bool Valid => m_Node != IntPtr.Zero && AtomicNode->Version == Version;

	public unsafe bool Alive => Valid && AtomicNode->Id != -1;

	public unsafe Handle(Node* node)
	{
		if (node == null)
		{
			throw new ArgumentNullException("node");
		}
		if (node->Id != -1)
		{
			throw new InvalidOperationException($"Reusing unflushed node {node->Id}");
		}
		Version = node->Version;
		m_Node = (IntPtr)node;
	}

	public unsafe void FlushNode()
	{
		if (!Valid)
		{
			throw new InvalidOperationException("Attempting to flush invalid audio handle");
		}
		AtomicNode->Id = -1;
		AtomicNode->Version++;
	}

	public bool Equals(Handle other)
	{
		return m_Node == other.m_Node && Version == other.Version;
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		return obj is Handle && Equals((Handle)obj);
	}

	public override int GetHashCode()
	{
		return ((int)m_Node * 397) ^ Version;
	}
}
[StructLayout(LayoutKind.Sequential, Size = 1)]
[NativeType(Header = "Modules/DSPGraph/Public/AudioMemoryManager.bindings.h")]
internal struct AudioMemoryManager
{
	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = false)]
	public unsafe static extern void* Internal_AllocateAudioMemory(int size, int alignment);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = false)]
	public unsafe static extern void Internal_FreeAudioMemory(void* memory);
}
[StructLayout(LayoutKind.Sequential, Size = 1)]
[NativeType(Header = "Modules/DSPGraph/Public/AudioOutputHookManager.bindings.h")]
internal struct AudioOutputHookManager
{
	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public unsafe static extern void Internal_CreateAudioOutputHook(out Handle outputHook, void* jobReflectionData, void* jobData);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public static extern void Internal_DisposeAudioOutputHook(ref Handle outputHook);
}
[StructLayout(LayoutKind.Sequential, Size = 1)]
[NativeHeader("Modules/DSPGraph/Public/DSPSampleProvider.bindings.h")]
[NativeType(Header = "Modules/DSPGraph/Public/DSPCommandBlock.bindings.h")]
internal struct DSPCommandBlockInternal
{
	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public unsafe static extern void Internal_CreateDSPNode(ref Handle graph, ref Handle block, ref Handle node, void* jobReflectionData, void* jobMemory, void* parameterDescriptionArray, int parameterCount, void* sampleProviderDescriptionArray, int sampleProviderCount);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public unsafe static extern void Internal_SetFloat(ref Handle graph, ref Handle block, ref Handle node, void* jobReflectionData, uint pIndex, float value, uint interpolationLength);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public unsafe static extern void Internal_AddFloatKey(ref Handle graph, ref Handle block, ref Handle node, void* jobReflectionData, uint pIndex, ulong dspClock, float value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public unsafe static extern void Internal_SustainFloat(ref Handle graph, ref Handle block, ref Handle node, void* jobReflectionData, uint pIndex, ulong dspClock);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public unsafe static extern void Internal_UpdateAudioJob(ref Handle graph, ref Handle block, ref Handle node, void* updateJobMem, void* updateJobReflectionData, void* nodeReflectionData);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public unsafe static extern void Internal_CreateUpdateRequest(ref Handle graph, ref Handle block, ref Handle node, ref Handle request, object callback, void* updateJobMem, void* updateJobReflectionData, void* nodeReflectionData);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public static extern void Internal_ReleaseDSPNode(ref Handle graph, ref Handle block, ref Handle node);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public static extern void Internal_Connect(ref Handle graph, ref Handle block, ref Handle output, int outputPort, ref Handle input, int inputPort, ref Handle connection);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public static extern void Internal_Disconnect(ref Handle graph, ref Handle block, ref Handle output, int outputPort, ref Handle input, int inputPort);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public static extern void Internal_DisconnectByHandle(ref Handle graph, ref Handle block, ref Handle connection);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public unsafe static extern void Internal_SetAttenuation(ref Handle graph, ref Handle block, ref Handle connection, void* value, byte dimension, uint interpolationLength);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public unsafe static extern void Internal_AddAttenuationKey(ref Handle graph, ref Handle block, ref Handle connection, ulong dspClock, void* value, byte dimension);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public static extern void Internal_SustainAttenuation(ref Handle graph, ref Handle block, ref Handle connection, ulong dspClock);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public static extern void Internal_AddInletPort(ref Handle graph, ref Handle block, ref Handle node, int channelCount, int format);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public static extern void Internal_AddOutletPort(ref Handle graph, ref Handle block, ref Handle node, int channelCount, int format);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public static extern void Internal_SetSampleProvider(ref Handle graph, ref Handle block, ref Handle node, int item, int index, uint audioSampleProviderId, bool destroyOnRemove);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public static extern void Internal_InsertSampleProvider(ref Handle graph, ref Handle block, ref Handle node, int item, int index, uint audioSampleProviderId, bool destroyOnRemove);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public static extern void Internal_RemoveSampleProvider(ref Handle graph, ref Handle block, ref Handle node, int item, int index);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public static extern void Internal_Complete(ref Handle graph, ref Handle block);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public static extern void Internal_Cancel(ref Handle graph, ref Handle block);
}
internal struct DSPGraphExecutionNode
{
	public unsafe void* ReflectionData;

	public unsafe void* JobStructData;

	public unsafe void* JobData;

	public unsafe void* ResourceContext;

	public int FunctionIndex;

	public int FenceIndex;

	public int FenceCount;
}
[StructLayout(LayoutKind.Sequential, Size = 1)]
[NativeType(Header = "Modules/DSPGraph/Public/DSPGraph.bindings.h")]
internal struct DSPGraphInternal
{
	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public static extern void Internal_CreateDSPGraph(out Handle graph, int outputFormat, uint outputChannels, uint dspBufferSize, uint sampleRate);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public static extern void Internal_DisposeDSPGraph(ref Handle graph);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public static extern void Internal_CreateDSPCommandBlock(ref Handle graph, ref Handle block);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public static extern uint Internal_AddNodeEventHandler(ref Handle graph, long eventTypeHashCode, object handler);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public static extern bool Internal_RemoveNodeEventHandler(ref Handle graph, uint handlerId);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public static extern void Internal_GetRootDSP(ref Handle graph, ref Handle root);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public static extern ulong Internal_GetDSPClock(ref Handle graph);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
	public static extern void Internal_BeginMix(ref Handle graph, int frameCount, int executionMode);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
	public unsafe static extern void Internal_ReadMix(ref Handle graph, void* buffer, int frameCount);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public static extern void Internal_Update(ref Handle graph);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
	public static extern bool Internal_AssertMixerThread(ref Handle graph);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
	public static extern bool Internal_AssertMainThread(ref Handle graph);

	[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
	public static Handle Internal_AllocateHandle(ref Handle graph)
	{
		Internal_AllocateHandle_Injected(ref graph, out var ret);
		return ret;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
	public unsafe static extern void Internal_InitializeJob(void* jobStructData, void* jobReflectionData, void* resourceContext);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
	public unsafe static extern void Internal_ExecuteJob(void* jobStructData, void* jobReflectionData, void* jobData, void* resourceContext);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
	public unsafe static extern void Internal_ExecuteUpdateJob(void* updateStructMemory, void* updateReflectionData, void* jobStructMemory, void* jobReflectionData, void* resourceContext, ref Handle requestHandle, ref JobHandle fence);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
	public unsafe static extern void Internal_DisposeJob(void* jobStructData, void* jobReflectionData, void* resourceContext);

	[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
	public unsafe static void Internal_ScheduleGraph(JobHandle inputDeps, void* nodes, int nodeCount, int* childTable, void* dependencies)
	{
		Internal_ScheduleGraph_Injected(ref inputDeps, nodes, nodeCount, childTable, dependencies);
	}

	[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
	public static void Internal_SyncFenceNoWorkSteal(JobHandle handle)
	{
		Internal_SyncFenceNoWorkSteal_Injected(ref handle);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Internal_AllocateHandle_Injected(ref Handle graph, out Handle ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private unsafe static extern void Internal_ScheduleGraph_Injected(ref JobHandle inputDeps, void* nodes, int nodeCount, int* childTable, void* dependencies);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Internal_SyncFenceNoWorkSteal_Injected(ref JobHandle handle);
}
[StructLayout(LayoutKind.Sequential, Size = 1)]
[NativeType(Header = "Modules/DSPGraph/Public/DSPNodeUpdateRequest.bindings.h")]
internal struct DSPNodeUpdateRequestHandleInternal
{
	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public unsafe static extern void* Internal_GetUpdateJobData(ref Handle graph, ref Handle requestHandle);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public static extern bool Internal_HasError(ref Handle graph, ref Handle requestHandle);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public static extern void Internal_GetDSPNode(ref Handle graph, ref Handle requestHandle, ref Handle node);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public static extern void Internal_GetFence(ref Handle graph, ref Handle requestHandle, ref JobHandle fence);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
	public static extern void Internal_Dispose(ref Handle graph, ref Handle requestHandle);
}
[StructLayout(LayoutKind.Sequential, Size = 1)]
[NativeType(Header = "Modules/DSPGraph/Public/DSPSampleProvider.bindings.h")]
internal struct DSPSampleProviderInternal
{
	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
	public unsafe static extern int Internal_ReadUInt8FromSampleProvider(void* provider, int format, void* buffer, int length);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
	public unsafe static extern int Internal_ReadSInt16FromSampleProvider(void* provider, int format, void* buffer, int length);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
	public unsafe static extern int Internal_ReadFloatFromSampleProvider(void* provider, void* buffer, int length);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
	public unsafe static extern ushort Internal_GetChannelCount(void* provider);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
	public unsafe static extern uint Internal_GetSampleRate(void* provider);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
	public unsafe static extern int Internal_ReadUInt8FromSampleProviderById(uint providerId, int format, void* buffer, int length);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
	public unsafe static extern int Internal_ReadSInt16FromSampleProviderById(uint providerId, int format, void* buffer, int length);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
	public unsafe static extern int Internal_ReadFloatFromSampleProviderById(uint providerId, void* buffer, int length);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
	public static extern ushort Internal_GetChannelCountById(uint providerId);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
	public static extern uint Internal_GetSampleRateById(uint providerId);
}
[StructLayout(LayoutKind.Sequential, Size = 1)]
[NativeType(Header = "Modules/DSPGraph/Public/ExecuteContext.bindings.h")]
internal struct ExecuteContextInternal
{
	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod(IsThreadSafe = true, IsFreeFunction = true)]
	public unsafe static extern void Internal_PostEvent(void* dspNodePtr, long eventTypeHashCode, void* eventPtr, int eventSize);
}
