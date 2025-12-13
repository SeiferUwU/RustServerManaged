using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

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
[assembly: InternalsVisibleTo("UnityEngine.SpatialTracking")]
[assembly: InternalsVisibleTo("GoogleAR.UnityNative")]
[assembly: InternalsVisibleTo("Unity.WindowsMRAutomation")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.Editor")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.EditorTests")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.Editor")]
[assembly: InternalsVisibleTo("UnityEditor.UIBuilderModule")]
[assembly: InternalsVisibleTo("Unity.UI.Builder.EditorTests")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests")]
[assembly: InternalsVisibleTo("Unity.UIElements")]
[assembly: InternalsVisibleTo("Unity.DeploymentTests.Services")]
[assembly: InternalsVisibleTo("Unity.Burst")]
[assembly: InternalsVisibleTo("UnityEngine.VideoModule")]
[assembly: InternalsVisibleTo("UnityEngine.VirtualTexturingModule")]
[assembly: InternalsVisibleTo("UnityEngine.WindModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
[assembly: InternalsVisibleTo("UnityEngine.Purchasing")]
[assembly: InternalsVisibleTo("UnityEngine.TestRunner")]
[assembly: InternalsVisibleTo("Unity.Automation")]
[assembly: InternalsVisibleTo("Unity.Burst.Editor")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsGameObjectsModule")]
[assembly: InternalsVisibleTo("Unity.UIElements.Editor")]
[assembly: InternalsVisibleTo("Unity.UIElements.PlayModeTests")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.012")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.013")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.014")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.015")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.016")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.017")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.018")]
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
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.011")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.010")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.009")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.008")]
[assembly: InternalsVisibleTo("UnityEngine.UIElements.Tests")]
[assembly: InternalsVisibleTo("Unity.UIElements.EditorTests")]
[assembly: InternalsVisibleTo("UnityEngine.UI")]
[assembly: InternalsVisibleTo("Unity.Networking.Transport")]
[assembly: InternalsVisibleTo("Unity.ucg.QoS")]
[assembly: InternalsVisibleTo("Unity.Services.QoS")]
[assembly: InternalsVisibleTo("Unity.Logging")]
[assembly: InternalsVisibleTo("Unity.Entities")]
[assembly: InternalsVisibleTo("Unity.Entities.Tests")]
[assembly: InternalsVisibleTo("UnityEngine.VehiclesModule")]
[assembly: InternalsVisibleTo("Unity.Collections")]
[assembly: InternalsVisibleTo("Unity.Core")]
[assembly: InternalsVisibleTo("UnityEngine.Core.Runtime.Tests")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.001")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.006")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.007")]
[assembly: InternalsVisibleTo("Unity.Runtime")]
[assembly: UnityEngineModuleAssembly]
[assembly: InternalsVisibleTo("UnityEngine.VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.VFXModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.XRModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClothModule")]
[assembly: InternalsVisibleTo("UnityEngine.AudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.ImageConversionModule")]
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.HotReloadModule")]
[assembly: InternalsVisibleTo("UnityEngine.GameCenterModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterRendererModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextRenderingModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestWWWModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestTextureModule")]
[assembly: InternalsVisibleTo("UnityEngine.GridModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.SubstanceModule")]
[assembly: InternalsVisibleTo("UnityEngine.StreamingModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteMaskModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputLegacyModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreFontEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreTextEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.IMGUIModule")]
[assembly: InternalsVisibleTo("UnityEngine.LocalizationModule")]
[assembly: InternalsVisibleTo("UnityEngine.ParticleSystemModule")]
[assembly: InternalsVisibleTo("UnityEngine.NVIDIAModule")]
[assembly: InternalsVisibleTo("UnityEngine.Physics2DModule")]
[assembly: InternalsVisibleTo("UnityEngine.ProfilerModule")]
[assembly: InternalsVisibleTo("UnityEngine.PropertiesModule")]
[assembly: InternalsVisibleTo("UnityEngine.RuntimeInitializeOnLoadManagerInitializerModule")]
[assembly: InternalsVisibleTo("UnityEngine.ScreenCaptureModule")]
[assembly: InternalsVisibleTo("UnityEngine.PerformanceReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteShapeModule")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace UnityEngine;

[NativeType(Header = "Modules/Grid/Public/Grid.h")]
[RequireComponent(typeof(Transform))]
[NativeHeader("Modules/Grid/Public/GridMarshalling.h")]
public sealed class Grid : GridLayout
{
	public new Vector3 cellSize
	{
		[FreeFunction("GridBindings::GetCellSize", HasExplicitThis = true)]
		get
		{
			get_cellSize_Injected(out var ret);
			return ret;
		}
		[FreeFunction("GridBindings::SetCellSize", HasExplicitThis = true)]
		set
		{
			set_cellSize_Injected(ref value);
		}
	}

	public new Vector3 cellGap
	{
		[FreeFunction("GridBindings::GetCellGap", HasExplicitThis = true)]
		get
		{
			get_cellGap_Injected(out var ret);
			return ret;
		}
		[FreeFunction("GridBindings::SetCellGap", HasExplicitThis = true)]
		set
		{
			set_cellGap_Injected(ref value);
		}
	}

	public new extern CellLayout cellLayout
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public new extern CellSwizzle cellSwizzle
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public Vector3 GetCellCenterLocal(Vector3Int position)
	{
		return CellToLocalInterpolated(position + GetLayoutCellCenter());
	}

	public Vector3 GetCellCenterWorld(Vector3Int position)
	{
		return LocalToWorld(CellToLocalInterpolated(position + GetLayoutCellCenter()));
	}

	[FreeFunction("GridBindings::CellSwizzle")]
	public static Vector3 Swizzle(CellSwizzle swizzle, Vector3 position)
	{
		Swizzle_Injected(swizzle, ref position, out var ret);
		return ret;
	}

	[FreeFunction("GridBindings::InverseCellSwizzle")]
	public static Vector3 InverseSwizzle(CellSwizzle swizzle, Vector3 position)
	{
		InverseSwizzle_Injected(swizzle, ref position, out var ret);
		return ret;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_cellSize_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_cellSize_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_cellGap_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_cellGap_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Swizzle_Injected(CellSwizzle swizzle, ref Vector3 position, out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void InverseSwizzle_Injected(CellSwizzle swizzle, ref Vector3 position, out Vector3 ret);
}
[NativeType(Header = "Modules/Grid/Public/Grid.h")]
[NativeHeader("Modules/Grid/Public/GridMarshalling.h")]
[RequireComponent(typeof(Transform))]
public class GridLayout : Behaviour
{
	public enum CellLayout
	{
		Rectangle,
		Hexagon,
		Isometric,
		IsometricZAsY
	}

	public enum CellSwizzle
	{
		XYZ,
		XZY,
		YXZ,
		YZX,
		ZXY,
		ZYX
	}

	public Vector3 cellSize
	{
		[FreeFunction("GridLayoutBindings::GetCellSize", HasExplicitThis = true)]
		get
		{
			get_cellSize_Injected(out var ret);
			return ret;
		}
	}

	public Vector3 cellGap
	{
		[FreeFunction("GridLayoutBindings::GetCellGap", HasExplicitThis = true)]
		get
		{
			get_cellGap_Injected(out var ret);
			return ret;
		}
	}

	public extern CellLayout cellLayout
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern CellSwizzle cellSwizzle
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	[FreeFunction("GridLayoutBindings::GetBoundsLocal", HasExplicitThis = true)]
	public Bounds GetBoundsLocal(Vector3Int cellPosition)
	{
		GetBoundsLocal_Injected(ref cellPosition, out var ret);
		return ret;
	}

	public Bounds GetBoundsLocal(Vector3 origin, Vector3 size)
	{
		return GetBoundsLocalOriginSize(origin, size);
	}

	[FreeFunction("GridLayoutBindings::GetBoundsLocalOriginSize", HasExplicitThis = true)]
	private Bounds GetBoundsLocalOriginSize(Vector3 origin, Vector3 size)
	{
		GetBoundsLocalOriginSize_Injected(ref origin, ref size, out var ret);
		return ret;
	}

	[FreeFunction("GridLayoutBindings::CellToLocal", HasExplicitThis = true)]
	public Vector3 CellToLocal(Vector3Int cellPosition)
	{
		CellToLocal_Injected(ref cellPosition, out var ret);
		return ret;
	}

	[FreeFunction("GridLayoutBindings::LocalToCell", HasExplicitThis = true)]
	public Vector3Int LocalToCell(Vector3 localPosition)
	{
		LocalToCell_Injected(ref localPosition, out var ret);
		return ret;
	}

	[FreeFunction("GridLayoutBindings::CellToLocalInterpolated", HasExplicitThis = true)]
	public Vector3 CellToLocalInterpolated(Vector3 cellPosition)
	{
		CellToLocalInterpolated_Injected(ref cellPosition, out var ret);
		return ret;
	}

	[FreeFunction("GridLayoutBindings::LocalToCellInterpolated", HasExplicitThis = true)]
	public Vector3 LocalToCellInterpolated(Vector3 localPosition)
	{
		LocalToCellInterpolated_Injected(ref localPosition, out var ret);
		return ret;
	}

	[FreeFunction("GridLayoutBindings::CellToWorld", HasExplicitThis = true)]
	public Vector3 CellToWorld(Vector3Int cellPosition)
	{
		CellToWorld_Injected(ref cellPosition, out var ret);
		return ret;
	}

	[FreeFunction("GridLayoutBindings::WorldToCell", HasExplicitThis = true)]
	public Vector3Int WorldToCell(Vector3 worldPosition)
	{
		WorldToCell_Injected(ref worldPosition, out var ret);
		return ret;
	}

	[FreeFunction("GridLayoutBindings::LocalToWorld", HasExplicitThis = true)]
	public Vector3 LocalToWorld(Vector3 localPosition)
	{
		LocalToWorld_Injected(ref localPosition, out var ret);
		return ret;
	}

	[FreeFunction("GridLayoutBindings::WorldToLocal", HasExplicitThis = true)]
	public Vector3 WorldToLocal(Vector3 worldPosition)
	{
		WorldToLocal_Injected(ref worldPosition, out var ret);
		return ret;
	}

	[FreeFunction("GridLayoutBindings::GetLayoutCellCenter", HasExplicitThis = true)]
	public Vector3 GetLayoutCellCenter()
	{
		GetLayoutCellCenter_Injected(out var ret);
		return ret;
	}

	[RequiredByNativeCode]
	private void DoNothing()
	{
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_cellSize_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_cellGap_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetBoundsLocal_Injected(ref Vector3Int cellPosition, out Bounds ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetBoundsLocalOriginSize_Injected(ref Vector3 origin, ref Vector3 size, out Bounds ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void CellToLocal_Injected(ref Vector3Int cellPosition, out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void LocalToCell_Injected(ref Vector3 localPosition, out Vector3Int ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void CellToLocalInterpolated_Injected(ref Vector3 cellPosition, out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void LocalToCellInterpolated_Injected(ref Vector3 localPosition, out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void CellToWorld_Injected(ref Vector3Int cellPosition, out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void WorldToCell_Injected(ref Vector3 worldPosition, out Vector3Int ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void LocalToWorld_Injected(ref Vector3 localPosition, out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void WorldToLocal_Injected(ref Vector3 worldPosition, out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetLayoutCellCenter_Injected(out Vector3 ret);
}
