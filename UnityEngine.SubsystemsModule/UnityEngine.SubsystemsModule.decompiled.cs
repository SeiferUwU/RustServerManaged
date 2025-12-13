using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.SubsystemsImplementation;

[assembly: InternalsVisibleTo("UnityEngine.SwitchModule")]
[assembly: InternalsVisibleTo("UnityEngine.XboxOneModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS4Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS4VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.PS5Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS5VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.Networking")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud.Service")]
[assembly: InternalsVisibleTo("Unity.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommon")]
[assembly: InternalsVisibleTo("UnityEngine.WindModule")]
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
[assembly: InternalsVisibleTo("UnityEngine.TestRunner")]
[assembly: InternalsVisibleTo("Unity.Automation")]
[assembly: InternalsVisibleTo("Unity.Burst")]
[assembly: InternalsVisibleTo("Unity.Burst.Editor")]
[assembly: InternalsVisibleTo("Unity.DeploymentTests.Services")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.UnityAnalytics")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Timeline")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Framework")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework.Tests")]
[assembly: InternalsVisibleTo("UnityEngine.Purchasing")]
[assembly: InternalsVisibleTo("Unity.PerformanceTests.RuntimeTestRunner.Tests")]
[assembly: InternalsVisibleTo("UnityEngine.VirtualTexturingModule")]
[assembly: InternalsVisibleTo("UnityEngine.VehiclesModule")]
[assembly: InternalsVisibleTo("UnityEngine.PerformanceReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.Physics2DModule")]
[assembly: InternalsVisibleTo("UnityEngine.ProfilerModule")]
[assembly: InternalsVisibleTo("UnityEngine.PropertiesModule")]
[assembly: InternalsVisibleTo("UnityEngine.RuntimeInitializeOnLoadManagerInitializerModule")]
[assembly: InternalsVisibleTo("UnityEngine.ScreenCaptureModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteMaskModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteShapeModule")]
[assembly: InternalsVisibleTo("UnityEngine.StreamingModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubstanceModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubsystemsModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainModule")]
[assembly: InternalsVisibleTo("UnityEngine.VideoModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainPhysicsModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.TilemapModule")]
[assembly: InternalsVisibleTo("UnityEngine.ParticleSystemModule")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.AllIn1Runner")]
[assembly: InternalsVisibleTo("Assembly-CSharp-testable")]
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
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.017")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.019")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.020")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.021")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.022")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.023")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.024")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.001")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridgeDev.005")]
[assembly: InternalsVisibleTo("Unity.Subsystem.Registration")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.018")]
[assembly: InternalsVisibleTo("Unity.Timeline")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.001")]
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
[assembly: InternalsVisibleTo("UnityEngine.UIElementsGameObjectsModule")]
[assembly: InternalsVisibleTo("Unity.UIElements.Editor")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.002")]
[assembly: InternalsVisibleTo("Unity.UIElements.PlayModeTests")]
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
[assembly: InternalsVisibleTo("UnityEngine.UIElements.Tests")]
[assembly: UnityEngineModuleAssembly]
[assembly: InternalsVisibleTo("UnityEngine.NVIDIAModule")]
[assembly: InternalsVisibleTo("UnityEngine.IMGUIModule")]
[assembly: InternalsVisibleTo("UnityEngine.HotReloadModule")]
[assembly: InternalsVisibleTo("UnityEngine.AnimationModule")]
[assembly: InternalsVisibleTo("UnityEngine.AndroidJNIModule")]
[assembly: InternalsVisibleTo("UnityEngine.AccessibilityModule")]
[assembly: InternalsVisibleTo("UnityEngine.ARModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputModule")]
[assembly: InternalsVisibleTo("UnityEngine.JSONSerializeModule")]
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.PhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.CoreModule")]
[assembly: InternalsVisibleTo("UnityEngine.SharedInternalsModule")]
[assembly: InternalsVisibleTo("UnityEngine")]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: CompilationRelaxations(8)]
[assembly: InternalsVisibleTo("UnityEngine.AIModule")]
[assembly: InternalsVisibleTo("UnityEngine.AudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClothModule")]
[assembly: InternalsVisibleTo("UnityEngine.DirectorModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreTextEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreFontEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputLegacyModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextRenderingModule")]
[assembly: InternalsVisibleTo("UnityEngine.GridModule")]
[assembly: InternalsVisibleTo("UnityEngine.GameCenterModule")]
[assembly: InternalsVisibleTo("UnityEngine.ImageConversionModule")]
[assembly: InternalsVisibleTo("UnityEngine.GIModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
[assembly: InternalsVisibleTo("UnityEngine.LocalizationModule")]
[assembly: InternalsVisibleTo("UnityEngine.CrashReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestModule")]
[assembly: InternalsVisibleTo("UnityEngine.TLSModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommonModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityConnectModule")]
[assembly: InternalsVisibleTo("UnityEngine.ContentLoadModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterRendererModule")]
[assembly: InternalsVisibleTo("UnityEngine.DSPGraphModule")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace UnityEngine
{
	[StructLayout(LayoutKind.Sequential)]
	[NativeHeader("Modules/Subsystems/Subsystem.h")]
	[UsedByNativeCode]
	public class IntegratedSubsystem : ISubsystem
	{
		internal IntPtr m_Ptr;

		internal ISubsystemDescriptor m_SubsystemDescriptor;

		public bool running => valid && IsRunning();

		internal bool valid => m_Ptr != IntPtr.Zero;

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetHandle(IntegratedSubsystem subsystem);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Start();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Stop();

		public void Destroy()
		{
			IntPtr ptr = m_Ptr;
			SubsystemManager.RemoveIntegratedSubsystemByPtr(m_Ptr);
			SubsystemBindings.DestroySubsystem(ptr);
			m_Ptr = IntPtr.Zero;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsRunning();
	}
	[UsedByNativeCode("Subsystem_TSubsystemDescriptor")]
	public class IntegratedSubsystem<TSubsystemDescriptor> : IntegratedSubsystem where TSubsystemDescriptor : ISubsystemDescriptor
	{
		public TSubsystemDescriptor subsystemDescriptor => (TSubsystemDescriptor)m_SubsystemDescriptor;

		public TSubsystemDescriptor SubsystemDescriptor => subsystemDescriptor;
	}
	internal static class SubsystemBindings
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void DestroySubsystem(IntPtr nativePtr);
	}
	internal interface ISubsystemDescriptorImpl : ISubsystemDescriptor
	{
		IntPtr ptr { get; set; }
	}
	[StructLayout(LayoutKind.Sequential)]
	[UsedByNativeCode("SubsystemDescriptorBase")]
	public abstract class IntegratedSubsystemDescriptor : ISubsystemDescriptorImpl, ISubsystemDescriptor
	{
		internal IntPtr m_Ptr;

		public string id => SubsystemDescriptorBindings.GetId(m_Ptr);

		IntPtr ISubsystemDescriptorImpl.ptr
		{
			get
			{
				return m_Ptr;
			}
			set
			{
				m_Ptr = value;
			}
		}

		ISubsystem ISubsystemDescriptor.Create()
		{
			return CreateImpl();
		}

		internal abstract ISubsystem CreateImpl();
	}
	[StructLayout(LayoutKind.Sequential)]
	[NativeHeader("Modules/Subsystems/SubsystemDescriptor.h")]
	[UsedByNativeCode("SubsystemDescriptor")]
	public class IntegratedSubsystemDescriptor<TSubsystem> : IntegratedSubsystemDescriptor where TSubsystem : IntegratedSubsystem
	{
		internal override ISubsystem CreateImpl()
		{
			return Create();
		}

		public TSubsystem Create()
		{
			IntPtr ptr = SubsystemDescriptorBindings.Create(m_Ptr);
			TSubsystem val = (TSubsystem)SubsystemManager.GetIntegratedSubsystemByPtr(ptr);
			if (val != null)
			{
				val.m_SubsystemDescriptor = this;
			}
			return val;
		}
	}
	internal static class SubsystemDescriptorBindings
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr Create(IntPtr descriptorPtr);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetId(IntPtr descriptorPtr);
	}
	public interface ISubsystem
	{
		bool running { get; }

		void Start();

		void Stop();

		void Destroy();
	}
	public interface ISubsystemDescriptor
	{
		string id { get; }

		ISubsystem Create();
	}
	public abstract class Subsystem : ISubsystem
	{
		internal ISubsystemDescriptor m_SubsystemDescriptor;

		public abstract bool running { get; }

		public abstract void Start();

		public abstract void Stop();

		public void Destroy()
		{
			if (SubsystemManager.RemoveDeprecatedSubsystem(this))
			{
				OnDestroy();
			}
		}

		protected abstract void OnDestroy();
	}
	public abstract class Subsystem<TSubsystemDescriptor> : Subsystem where TSubsystemDescriptor : ISubsystemDescriptor
	{
		public TSubsystemDescriptor SubsystemDescriptor => (TSubsystemDescriptor)m_SubsystemDescriptor;
	}
	public abstract class SubsystemDescriptor : ISubsystemDescriptor
	{
		public string id { get; set; }

		public Type subsystemImplementationType { get; set; }

		ISubsystem ISubsystemDescriptor.Create()
		{
			return CreateImpl();
		}

		internal abstract ISubsystem CreateImpl();
	}
	public class SubsystemDescriptor<TSubsystem> : SubsystemDescriptor where TSubsystem : Subsystem
	{
		internal override ISubsystem CreateImpl()
		{
			return Create();
		}

		public TSubsystem Create()
		{
			if (SubsystemManager.FindDeprecatedSubsystemByDescriptor(this) is TSubsystem result)
			{
				return result;
			}
			TSubsystem val = Activator.CreateInstance(base.subsystemImplementationType) as TSubsystem;
			val.m_SubsystemDescriptor = this;
			SubsystemManager.AddDeprecatedSubsystem(val);
			return val;
		}
	}
	internal static class Internal_SubsystemDescriptors
	{
		[RequiredByNativeCode]
		internal static void Internal_AddDescriptor(SubsystemDescriptor descriptor)
		{
			SubsystemDescriptorStore.RegisterDeprecatedDescriptor(descriptor);
		}
	}
	[NativeHeader("Modules/Subsystems/SubsystemManager.h")]
	public static class SubsystemManager
	{
		private static List<IntegratedSubsystem> s_IntegratedSubsystems;

		private static List<SubsystemWithProvider> s_StandaloneSubsystems;

		private static List<Subsystem> s_DeprecatedSubsystems;

		public static event Action beforeReloadSubsystems;

		public static event Action afterReloadSubsystems;

		public static event Action reloadSubsytemsStarted;

		public static event Action reloadSubsytemsCompleted;

		[RequiredByNativeCode]
		private static void ReloadSubsystemsStarted()
		{
			if (SubsystemManager.reloadSubsytemsStarted != null)
			{
				SubsystemManager.reloadSubsytemsStarted();
			}
			if (SubsystemManager.beforeReloadSubsystems != null)
			{
				SubsystemManager.beforeReloadSubsystems();
			}
		}

		[RequiredByNativeCode]
		private static void ReloadSubsystemsCompleted()
		{
			if (SubsystemManager.reloadSubsytemsCompleted != null)
			{
				SubsystemManager.reloadSubsytemsCompleted();
			}
			if (SubsystemManager.afterReloadSubsystems != null)
			{
				SubsystemManager.afterReloadSubsystems();
			}
		}

		[RequiredByNativeCode]
		private static void InitializeIntegratedSubsystem(IntPtr ptr, IntegratedSubsystem subsystem)
		{
			subsystem.m_Ptr = ptr;
			subsystem.SetHandle(subsystem);
			s_IntegratedSubsystems.Add(subsystem);
		}

		[RequiredByNativeCode]
		private static void ClearSubsystems()
		{
			foreach (IntegratedSubsystem s_IntegratedSubsystem in s_IntegratedSubsystems)
			{
				s_IntegratedSubsystem.m_Ptr = IntPtr.Zero;
			}
			s_IntegratedSubsystems.Clear();
			s_StandaloneSubsystems.Clear();
			s_DeprecatedSubsystems.Clear();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StaticConstructScriptingClassMap();

		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ReportSingleSubsystemAnalytics(string id);

		static SubsystemManager()
		{
			s_IntegratedSubsystems = new List<IntegratedSubsystem>();
			s_StandaloneSubsystems = new List<SubsystemWithProvider>();
			s_DeprecatedSubsystems = new List<Subsystem>();
			StaticConstructScriptingClassMap();
		}

		public static void GetAllSubsystemDescriptors(List<ISubsystemDescriptor> descriptors)
		{
			SubsystemDescriptorStore.GetAllSubsystemDescriptors(descriptors);
		}

		public static void GetSubsystemDescriptors<T>(List<T> descriptors) where T : ISubsystemDescriptor
		{
			SubsystemDescriptorStore.GetSubsystemDescriptors(descriptors);
		}

		public static void GetSubsystems<T>(List<T> subsystems) where T : ISubsystem
		{
			subsystems.Clear();
			AddSubsystemSubset(s_IntegratedSubsystems, subsystems);
			AddSubsystemSubset(s_StandaloneSubsystems, subsystems);
			AddSubsystemSubset(s_DeprecatedSubsystems, subsystems);
		}

		private static void AddSubsystemSubset<TBaseTypeInList, TQueryType>(List<TBaseTypeInList> copyFrom, List<TQueryType> copyTo) where TBaseTypeInList : ISubsystem where TQueryType : ISubsystem
		{
			foreach (TBaseTypeInList item2 in copyFrom)
			{
				if (item2 is TQueryType item)
				{
					copyTo.Add(item);
				}
			}
		}

		internal static IntegratedSubsystem GetIntegratedSubsystemByPtr(IntPtr ptr)
		{
			foreach (IntegratedSubsystem s_IntegratedSubsystem in s_IntegratedSubsystems)
			{
				if (s_IntegratedSubsystem.m_Ptr == ptr)
				{
					return s_IntegratedSubsystem;
				}
			}
			return null;
		}

		internal static void RemoveIntegratedSubsystemByPtr(IntPtr ptr)
		{
			for (int i = 0; i < s_IntegratedSubsystems.Count; i++)
			{
				if (!(s_IntegratedSubsystems[i].m_Ptr != ptr))
				{
					s_IntegratedSubsystems[i].m_Ptr = IntPtr.Zero;
					s_IntegratedSubsystems.RemoveAt(i);
					break;
				}
			}
		}

		internal static void AddStandaloneSubsystem(SubsystemWithProvider subsystem)
		{
			s_StandaloneSubsystems.Add(subsystem);
		}

		internal static bool RemoveStandaloneSubsystem(SubsystemWithProvider subsystem)
		{
			return s_StandaloneSubsystems.Remove(subsystem);
		}

		internal static SubsystemWithProvider FindStandaloneSubsystemByDescriptor(SubsystemDescriptorWithProvider descriptor)
		{
			foreach (SubsystemWithProvider s_StandaloneSubsystem in s_StandaloneSubsystems)
			{
				if (s_StandaloneSubsystem.descriptor == descriptor)
				{
					return s_StandaloneSubsystem;
				}
			}
			return null;
		}

		public static void GetInstances<T>(List<T> subsystems) where T : ISubsystem
		{
			GetSubsystems(subsystems);
		}

		internal static void AddDeprecatedSubsystem(Subsystem subsystem)
		{
			s_DeprecatedSubsystems.Add(subsystem);
		}

		internal static bool RemoveDeprecatedSubsystem(Subsystem subsystem)
		{
			return s_DeprecatedSubsystems.Remove(subsystem);
		}

		internal static Subsystem FindDeprecatedSubsystemByDescriptor(SubsystemDescriptor descriptor)
		{
			foreach (Subsystem s_DeprecatedSubsystem in s_DeprecatedSubsystems)
			{
				if (s_DeprecatedSubsystem.m_SubsystemDescriptor == descriptor)
				{
					return s_DeprecatedSubsystem;
				}
			}
			return null;
		}
	}
}
namespace UnityEngine.SubsystemsImplementation
{
	[NativeHeader("Modules/Subsystems/SubsystemManager.h")]
	public static class SubsystemDescriptorStore
	{
		private static List<IntegratedSubsystemDescriptor> s_IntegratedDescriptors = new List<IntegratedSubsystemDescriptor>();

		private static List<SubsystemDescriptorWithProvider> s_StandaloneDescriptors = new List<SubsystemDescriptorWithProvider>();

		private static List<SubsystemDescriptor> s_DeprecatedDescriptors = new List<SubsystemDescriptor>();

		[RequiredByNativeCode]
		internal static void InitializeManagedDescriptor(IntPtr ptr, IntegratedSubsystemDescriptor desc)
		{
			desc.m_Ptr = ptr;
			s_IntegratedDescriptors.Add(desc);
		}

		[RequiredByNativeCode]
		internal static void ClearManagedDescriptors()
		{
			foreach (IntegratedSubsystemDescriptor s_IntegratedDescriptor in s_IntegratedDescriptors)
			{
				s_IntegratedDescriptor.m_Ptr = IntPtr.Zero;
			}
			s_IntegratedDescriptors.Clear();
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReportSingleSubsystemAnalytics(string id);

		public static void RegisterDescriptor(SubsystemDescriptorWithProvider descriptor)
		{
			descriptor.ThrowIfInvalid();
			RegisterDescriptor(descriptor, s_StandaloneDescriptors);
		}

		internal static void GetAllSubsystemDescriptors(List<ISubsystemDescriptor> descriptors)
		{
			descriptors.Clear();
			int num = s_IntegratedDescriptors.Count + s_StandaloneDescriptors.Count + s_DeprecatedDescriptors.Count;
			if (descriptors.Capacity < num)
			{
				descriptors.Capacity = num;
			}
			AddDescriptorSubset(s_IntegratedDescriptors, descriptors);
			AddDescriptorSubset(s_StandaloneDescriptors, descriptors);
			AddDescriptorSubset(s_DeprecatedDescriptors, descriptors);
		}

		private static void AddDescriptorSubset<TBaseTypeInList>(List<TBaseTypeInList> copyFrom, List<ISubsystemDescriptor> copyTo) where TBaseTypeInList : ISubsystemDescriptor
		{
			foreach (TBaseTypeInList item in copyFrom)
			{
				copyTo.Add(item);
			}
		}

		internal static void GetSubsystemDescriptors<T>(List<T> descriptors) where T : ISubsystemDescriptor
		{
			descriptors.Clear();
			AddDescriptorSubset(s_IntegratedDescriptors, descriptors);
			AddDescriptorSubset(s_StandaloneDescriptors, descriptors);
			AddDescriptorSubset(s_DeprecatedDescriptors, descriptors);
		}

		private static void AddDescriptorSubset<TBaseTypeInList, TQueryType>(List<TBaseTypeInList> copyFrom, List<TQueryType> copyTo) where TBaseTypeInList : ISubsystemDescriptor where TQueryType : ISubsystemDescriptor
		{
			foreach (TBaseTypeInList item2 in copyFrom)
			{
				if (item2 is TQueryType item)
				{
					copyTo.Add(item);
				}
			}
		}

		internal static void RegisterDescriptor<TDescriptor, TBaseTypeInList>(TDescriptor descriptor, List<TBaseTypeInList> storeInList) where TDescriptor : TBaseTypeInList where TBaseTypeInList : ISubsystemDescriptor
		{
			for (int i = 0; i < storeInList.Count; i++)
			{
				if (!(storeInList[i].id != descriptor.id))
				{
					Debug.LogWarning("Registering subsystem descriptor with duplicate ID '" + descriptor.id + "' - overwriting previous entry.");
					storeInList[i] = (TBaseTypeInList)(object)descriptor;
					return;
				}
			}
			ReportSingleSubsystemAnalytics(descriptor.id);
			storeInList.Add((TBaseTypeInList)(object)descriptor);
		}

		internal static void RegisterDeprecatedDescriptor(SubsystemDescriptor descriptor)
		{
			RegisterDescriptor(descriptor, s_DeprecatedDescriptors);
		}
	}
	public abstract class SubsystemDescriptorWithProvider : ISubsystemDescriptor
	{
		public string id { get; set; }

		protected internal Type providerType { get; set; }

		protected internal Type subsystemTypeOverride { get; set; }

		internal abstract ISubsystem CreateImpl();

		ISubsystem ISubsystemDescriptor.Create()
		{
			return CreateImpl();
		}

		internal abstract void ThrowIfInvalid();
	}
	public class SubsystemDescriptorWithProvider<TSubsystem, TProvider> : SubsystemDescriptorWithProvider where TSubsystem : SubsystemWithProvider, new() where TProvider : SubsystemProvider<TSubsystem>
	{
		internal override ISubsystem CreateImpl()
		{
			return Create();
		}

		public TSubsystem Create()
		{
			if (SubsystemManager.FindStandaloneSubsystemByDescriptor(this) is TSubsystem result)
			{
				return result;
			}
			TProvider val = CreateProvider();
			if (val == null)
			{
				return null;
			}
			TSubsystem val2 = ((base.subsystemTypeOverride != null) ? ((TSubsystem)Activator.CreateInstance(base.subsystemTypeOverride)) : new TSubsystem());
			val2.Initialize(this, val);
			SubsystemManager.AddStandaloneSubsystem(val2);
			return val2;
		}

		internal sealed override void ThrowIfInvalid()
		{
			if (base.providerType == null)
			{
				throw new InvalidOperationException("Invalid descriptor - must supply a valid providerType field!");
			}
			if (!base.providerType.IsSubclassOf(typeof(TProvider)))
			{
				throw new InvalidOperationException($"Can't create provider - providerType '{base.providerType.ToString()}' is not a subclass of '{typeof(TProvider).ToString()}'!");
			}
			if (base.subsystemTypeOverride != null && !base.subsystemTypeOverride.IsSubclassOf(typeof(TSubsystem)))
			{
				throw new InvalidOperationException($"Can't create provider - subsystemTypeOverride '{base.subsystemTypeOverride.ToString()}' is not a subclass of '{typeof(TSubsystem).ToString()}'!");
			}
		}

		internal TProvider CreateProvider()
		{
			TProvider val = (TProvider)Activator.CreateInstance(base.providerType);
			return val.TryInitialize() ? val : null;
		}
	}
	public abstract class SubsystemProvider
	{
		internal bool m_Running;

		public bool running => m_Running;
	}
	public abstract class SubsystemProvider<TSubsystem> : SubsystemProvider where TSubsystem : SubsystemWithProvider, new()
	{
		protected internal virtual bool TryInitialize()
		{
			return true;
		}

		public abstract void Start();

		public abstract void Stop();

		public abstract void Destroy();
	}
	public class SubsystemProxy<TSubsystem, TProvider> where TSubsystem : SubsystemWithProvider, new() where TProvider : SubsystemProvider<TSubsystem>
	{
		public TProvider provider { get; private set; }

		public bool running
		{
			get
			{
				return provider.running;
			}
			set
			{
				provider.m_Running = value;
			}
		}

		internal SubsystemProxy(TProvider provider)
		{
			this.provider = provider;
		}
	}
	public abstract class SubsystemWithProvider : ISubsystem
	{
		public bool running { get; private set; }

		internal SubsystemProvider providerBase { get; set; }

		internal abstract SubsystemDescriptorWithProvider descriptor { get; }

		public void Start()
		{
			if (!running)
			{
				OnStart();
				providerBase.m_Running = true;
				running = true;
			}
		}

		protected abstract void OnStart();

		public void Stop()
		{
			if (running)
			{
				OnStop();
				providerBase.m_Running = false;
				running = false;
			}
		}

		protected abstract void OnStop();

		public void Destroy()
		{
			Stop();
			if (SubsystemManager.RemoveStandaloneSubsystem(this))
			{
				OnDestroy();
			}
		}

		protected abstract void OnDestroy();

		internal abstract void Initialize(SubsystemDescriptorWithProvider descriptor, SubsystemProvider subsystemProvider);
	}
	public abstract class SubsystemWithProvider<TSubsystem, TSubsystemDescriptor, TProvider> : SubsystemWithProvider where TSubsystem : SubsystemWithProvider, new() where TSubsystemDescriptor : SubsystemDescriptorWithProvider where TProvider : SubsystemProvider<TSubsystem>
	{
		public TSubsystemDescriptor subsystemDescriptor { get; private set; }

		protected internal TProvider provider { get; private set; }

		internal sealed override SubsystemDescriptorWithProvider descriptor => subsystemDescriptor;

		protected virtual void OnCreate()
		{
		}

		protected override void OnStart()
		{
			provider.Start();
		}

		protected override void OnStop()
		{
			provider.Stop();
		}

		protected override void OnDestroy()
		{
			provider.Destroy();
		}

		internal sealed override void Initialize(SubsystemDescriptorWithProvider descriptor, SubsystemProvider provider)
		{
			base.providerBase = provider;
			this.provider = (TProvider)provider;
			subsystemDescriptor = (TSubsystemDescriptor)descriptor;
			OnCreate();
		}
	}
}
namespace UnityEngine.SubsystemsImplementation.Extensions
{
	public static class SubsystemDescriptorExtensions
	{
		public static SubsystemProxy<TSubsystem, TProvider> CreateProxy<TSubsystem, TProvider>(this SubsystemDescriptorWithProvider<TSubsystem, TProvider> descriptor) where TSubsystem : SubsystemWithProvider, new() where TProvider : SubsystemProvider<TSubsystem>
		{
			TProvider val = descriptor.CreateProvider();
			return (val != null) ? new SubsystemProxy<TSubsystem, TProvider>(val) : null;
		}
	}
	public static class SubsystemExtensions
	{
		public static TProvider GetProvider<TSubsystem, TDescriptor, TProvider>(this SubsystemWithProvider<TSubsystem, TDescriptor, TProvider> subsystem) where TSubsystem : SubsystemWithProvider, new() where TDescriptor : SubsystemDescriptorWithProvider<TSubsystem, TProvider> where TProvider : SubsystemProvider<TSubsystem>
		{
			return subsystem.provider;
		}
	}
}
namespace UnityEngine.Subsystems
{
	[UsedByNativeCode]
	[NativeType(Header = "Modules/Subsystems/Example/ExampleSubsystem.h")]
	public class ExampleSubsystem : IntegratedSubsystem<ExampleSubsystemDescriptor>
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void PrintExample();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetBool();
	}
	[UsedByNativeCode]
	[NativeType(Header = "Modules/Subsystems/Example/ExampleSubsystemDescriptor.h")]
	public class ExampleSubsystemDescriptor : IntegratedSubsystemDescriptor<ExampleSubsystem>
	{
		public extern bool supportsEditorMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool disableBackbufferMSAA
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool stereoscopicBackbuffer
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern bool usePBufferEGL
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
	}
}
