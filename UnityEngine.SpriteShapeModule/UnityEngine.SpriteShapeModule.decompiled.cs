using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting.APIUpdating;

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
[assembly: InternalsVisibleTo("UnityEngine.UIElementsGameObjectsModule")]
[assembly: InternalsVisibleTo("Unity.UIElements.Editor")]
[assembly: InternalsVisibleTo("Unity.UIElements.PlayModeTests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework.Tests")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests.Framework")]
[assembly: InternalsVisibleTo("Unity.RuntimeTests")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Framework")]
[assembly: InternalsVisibleTo("UnityEngine.PS5Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS5VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.Networking")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud")]
[assembly: InternalsVisibleTo("UnityEngine.Cloud.Service")]
[assembly: InternalsVisibleTo("Unity.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.Analytics")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommon")]
[assembly: InternalsVisibleTo("UnityEngine.UIElements.Tests")]
[assembly: InternalsVisibleTo("UnityEngine.Advertisements")]
[assembly: InternalsVisibleTo("UnityEngine.TestRunner")]
[assembly: InternalsVisibleTo("Unity.Automation")]
[assembly: InternalsVisibleTo("Unity.Burst")]
[assembly: InternalsVisibleTo("Unity.Burst.Editor")]
[assembly: InternalsVisibleTo("Unity.DeploymentTests.Services")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.UnityAnalytics")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests.Timeline")]
[assembly: InternalsVisibleTo("UnityEngine.Purchasing")]
[assembly: InternalsVisibleTo("UnityEngine.PS4VRModule")]
[assembly: InternalsVisibleTo("Unity.UIElements.EditorTests")]
[assembly: InternalsVisibleTo("Unity.Networking.Transport")]
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
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.013")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.012")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.011")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.010")]
[assembly: InternalsVisibleTo("Unity.ucg.QoS")]
[assembly: InternalsVisibleTo("Unity.Services.QoS")]
[assembly: InternalsVisibleTo("Unity.Logging")]
[assembly: InternalsVisibleTo("Unity.Entities")]
[assembly: InternalsVisibleTo("Unity.Entities.Tests")]
[assembly: InternalsVisibleTo("Unity.Collections")]
[assembly: InternalsVisibleTo("Unity.Runtime")]
[assembly: InternalsVisibleTo("Unity.Core")]
[assembly: InternalsVisibleTo("UnityEngine.UI")]
[assembly: InternalsVisibleTo("UnityEngine.Core.Runtime.Tests")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.002")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.006")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.007")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.008")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.009")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.001")]
[assembly: InternalsVisibleTo("UnityEngine.PS4Module")]
[assembly: InternalsVisibleTo("UnityEngine.XboxOneModule")]
[assembly: InternalsVisibleTo("UnityEngine.SwitchModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterRendererModule")]
[assembly: InternalsVisibleTo("UnityEngine.ContentLoadModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityConnectModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommonModule")]
[assembly: InternalsVisibleTo("UnityEngine.TLSModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsModule")]
[assembly: InternalsVisibleTo("UnityEngine.DSPGraphModule")]
[assembly: InternalsVisibleTo("UnityEngine.DirectorModule")]
[assembly: UnityEngineModuleAssembly]
[assembly: InternalsVisibleTo("UnityEngine.ImageConversionModule")]
[assembly: InternalsVisibleTo("UnityEngine.GameCenterModule")]
[assembly: InternalsVisibleTo("UnityEngine.GridModule")]
[assembly: InternalsVisibleTo("UnityEngine.CrashReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClothModule")]
[assembly: InternalsVisibleTo("UnityEngine.AudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: InternalsVisibleTo("UnityEngine")]
[assembly: InternalsVisibleTo("UnityEngine.SharedInternalsModule")]
[assembly: InternalsVisibleTo("UnityEngine.CoreModule")]
[assembly: InternalsVisibleTo("UnityEngine.AIModule")]
[assembly: InternalsVisibleTo("UnityEngine.PhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.JSONSerializeModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputModule")]
[assembly: InternalsVisibleTo("UnityEngine.ARModule")]
[assembly: InternalsVisibleTo("UnityEngine.AccessibilityModule")]
[assembly: InternalsVisibleTo("UnityEngine.AndroidJNIModule")]
[assembly: InternalsVisibleTo("UnityEngine.AnimationModule")]
[assembly: InternalsVisibleTo("UnityEngine.HotReloadModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextRenderingModule")]
[assembly: InternalsVisibleTo("UnityEngine.InputLegacyModule")]
[assembly: InternalsVisibleTo("UnityEngine.GIModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreTextEngineModule")]
[assembly: InternalsVisibleTo("UnityEngine.UIElementsModule")]
[assembly: InternalsVisibleTo("UnityEngine.UmbraModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityCurlModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityTestProtocolModule")]
[assembly: InternalsVisibleTo("UnityEngine.TextCoreFontEngineModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.UIModule")]
[assembly: InternalsVisibleTo("UnityEngine.TilemapModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainModule")]
[assembly: InternalsVisibleTo("UnityEngine.IMGUIModule")]
[assembly: InternalsVisibleTo("UnityEngine.TerrainPhysicsModule")]
[assembly: InternalsVisibleTo("UnityEngine.NVIDIAModule")]
[assembly: InternalsVisibleTo("UnityEngine.ParticleSystemModule")]
[assembly: InternalsVisibleTo("UnityEngine.PerformanceReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.Physics2DModule")]
[assembly: InternalsVisibleTo("UnityEngine.ProfilerModule")]
[assembly: InternalsVisibleTo("UnityEngine.LocalizationModule")]
[assembly: InternalsVisibleTo("UnityEngine.RuntimeInitializeOnLoadManagerInitializerModule")]
[assembly: InternalsVisibleTo("UnityEngine.ScreenCaptureModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteMaskModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteShapeModule")]
[assembly: InternalsVisibleTo("UnityEngine.StreamingModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubstanceModule")]
[assembly: InternalsVisibleTo("UnityEngine.SubsystemsModule")]
[assembly: InternalsVisibleTo("UnityEngine.PropertiesModule")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace UnityEngine.U2D;

[MovedFrom("UnityEngine.Experimental.U2D")]
public struct SpriteShapeParameters
{
	public Matrix4x4 transform;

	public Texture2D fillTexture;

	public uint fillScale;

	public uint splineDetail;

	public float angleThreshold;

	public float borderPivot;

	public float bevelCutoff;

	public float bevelSize;

	public bool carpet;

	public bool smartSprite;

	public bool adaptiveUV;

	public bool spriteBorders;

	public bool stretchUV;
}
[MovedFrom("UnityEngine.Experimental.U2D")]
public struct SpriteShapeSegment
{
	private int m_GeomIndex;

	private int m_IndexCount;

	private int m_VertexCount;

	private int m_SpriteIndex;

	public int geomIndex
	{
		get
		{
			return m_GeomIndex;
		}
		set
		{
			m_GeomIndex = value;
		}
	}

	public int indexCount
	{
		get
		{
			return m_IndexCount;
		}
		set
		{
			m_IndexCount = value;
		}
	}

	public int vertexCount
	{
		get
		{
			return m_VertexCount;
		}
		set
		{
			m_VertexCount = value;
		}
	}

	public int spriteIndex
	{
		get
		{
			return m_SpriteIndex;
		}
		set
		{
			m_SpriteIndex = value;
		}
	}
}
internal enum SpriteShapeDataType
{
	Index,
	Segment,
	BoundingBox,
	ChannelVertex,
	ChannelTexCoord0,
	ChannelNormal,
	ChannelTangent,
	ChannelColor,
	DataCount
}
[MovedFrom("UnityEngine.Experimental.U2D")]
[NativeType(Header = "Modules/SpriteShape/Public/SpriteShapeRenderer.h")]
public class SpriteShapeRenderer : Renderer
{
	public Color color
	{
		get
		{
			get_color_Injected(out var ret);
			return ret;
		}
		set
		{
			set_color_Injected(ref value);
		}
	}

	public extern SpriteMaskInteraction maskInteraction
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public void Prepare(JobHandle handle, SpriteShapeParameters shapeParams, Sprite[] sprites)
	{
		Prepare_Injected(ref handle, ref shapeParams, sprites);
	}

	private unsafe NativeArray<T> GetNativeDataArray<T>(SpriteShapeDataType dataType) where T : struct
	{
		SpriteChannelInfo dataInfo = GetDataInfo(dataType);
		return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(dataInfo.buffer, dataInfo.count, Allocator.Invalid);
	}

	private unsafe NativeSlice<T> GetChannelDataArray<T>(SpriteShapeDataType dataType, VertexAttribute channel) where T : struct
	{
		SpriteChannelInfo channelInfo = GetChannelInfo(channel);
		byte* dataPointer = (byte*)channelInfo.buffer + channelInfo.offset;
		return NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<T>(dataPointer, channelInfo.stride, channelInfo.count);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void SetSegmentCount(int geomCount);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void SetMeshDataCount(int vertexCount, int indexCount);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void SetMeshChannelInfo(int vertexCount, int indexCount, int hotChannelMask);

	private SpriteChannelInfo GetDataInfo(SpriteShapeDataType arrayType)
	{
		GetDataInfo_Injected(arrayType, out var ret);
		return ret;
	}

	private SpriteChannelInfo GetChannelInfo(VertexAttribute channel)
	{
		GetChannelInfo_Injected(channel, out var ret);
		return ret;
	}

	public void SetLocalAABB(Bounds bounds)
	{
		SetLocalAABB_Injected(ref bounds);
	}

	public NativeArray<Bounds> GetBounds()
	{
		return GetNativeDataArray<Bounds>(SpriteShapeDataType.BoundingBox);
	}

	public NativeArray<SpriteShapeSegment> GetSegments(int dataSize)
	{
		SetSegmentCount(dataSize);
		return GetNativeDataArray<SpriteShapeSegment>(SpriteShapeDataType.Segment);
	}

	public void GetChannels(int dataSize, out NativeArray<ushort> indices, out NativeSlice<Vector3> vertices, out NativeSlice<Vector2> texcoords)
	{
		SetMeshDataCount(dataSize, dataSize);
		indices = GetNativeDataArray<ushort>(SpriteShapeDataType.Index);
		vertices = GetChannelDataArray<Vector3>(SpriteShapeDataType.ChannelVertex, VertexAttribute.Position);
		texcoords = GetChannelDataArray<Vector2>(SpriteShapeDataType.ChannelTexCoord0, VertexAttribute.TexCoord0);
	}

	public void GetChannels(int dataSize, out NativeArray<ushort> indices, out NativeSlice<Vector3> vertices, out NativeSlice<Vector2> texcoords, out NativeSlice<Color32> colors)
	{
		SetMeshChannelInfo(dataSize, dataSize, 8);
		indices = GetNativeDataArray<ushort>(SpriteShapeDataType.Index);
		vertices = GetChannelDataArray<Vector3>(SpriteShapeDataType.ChannelVertex, VertexAttribute.Position);
		texcoords = GetChannelDataArray<Vector2>(SpriteShapeDataType.ChannelTexCoord0, VertexAttribute.TexCoord0);
		colors = GetChannelDataArray<Color32>(SpriteShapeDataType.ChannelColor, VertexAttribute.Color);
	}

	public void GetChannels(int dataSize, out NativeArray<ushort> indices, out NativeSlice<Vector3> vertices, out NativeSlice<Vector2> texcoords, out NativeSlice<Vector4> tangents)
	{
		SetMeshChannelInfo(dataSize, dataSize, 4);
		indices = GetNativeDataArray<ushort>(SpriteShapeDataType.Index);
		vertices = GetChannelDataArray<Vector3>(SpriteShapeDataType.ChannelVertex, VertexAttribute.Position);
		texcoords = GetChannelDataArray<Vector2>(SpriteShapeDataType.ChannelTexCoord0, VertexAttribute.TexCoord0);
		tangents = GetChannelDataArray<Vector4>(SpriteShapeDataType.ChannelTangent, VertexAttribute.Tangent);
	}

	public void GetChannels(int dataSize, out NativeArray<ushort> indices, out NativeSlice<Vector3> vertices, out NativeSlice<Vector2> texcoords, out NativeSlice<Color32> colors, out NativeSlice<Vector4> tangents)
	{
		SetMeshChannelInfo(dataSize, dataSize, 12);
		indices = GetNativeDataArray<ushort>(SpriteShapeDataType.Index);
		vertices = GetChannelDataArray<Vector3>(SpriteShapeDataType.ChannelVertex, VertexAttribute.Position);
		texcoords = GetChannelDataArray<Vector2>(SpriteShapeDataType.ChannelTexCoord0, VertexAttribute.TexCoord0);
		colors = GetChannelDataArray<Color32>(SpriteShapeDataType.ChannelColor, VertexAttribute.Color);
		tangents = GetChannelDataArray<Vector4>(SpriteShapeDataType.ChannelTangent, VertexAttribute.Tangent);
	}

	public void GetChannels(int dataSize, out NativeArray<ushort> indices, out NativeSlice<Vector3> vertices, out NativeSlice<Vector2> texcoords, out NativeSlice<Vector4> tangents, out NativeSlice<Vector3> normals)
	{
		SetMeshChannelInfo(dataSize, dataSize, 6);
		indices = GetNativeDataArray<ushort>(SpriteShapeDataType.Index);
		vertices = GetChannelDataArray<Vector3>(SpriteShapeDataType.ChannelVertex, VertexAttribute.Position);
		texcoords = GetChannelDataArray<Vector2>(SpriteShapeDataType.ChannelTexCoord0, VertexAttribute.TexCoord0);
		tangents = GetChannelDataArray<Vector4>(SpriteShapeDataType.ChannelTangent, VertexAttribute.Tangent);
		normals = GetChannelDataArray<Vector3>(SpriteShapeDataType.ChannelNormal, VertexAttribute.Normal);
	}

	public void GetChannels(int dataSize, out NativeArray<ushort> indices, out NativeSlice<Vector3> vertices, out NativeSlice<Vector2> texcoords, out NativeSlice<Color32> colors, out NativeSlice<Vector4> tangents, out NativeSlice<Vector3> normals)
	{
		SetMeshChannelInfo(dataSize, dataSize, 14);
		indices = GetNativeDataArray<ushort>(SpriteShapeDataType.Index);
		vertices = GetChannelDataArray<Vector3>(SpriteShapeDataType.ChannelVertex, VertexAttribute.Position);
		texcoords = GetChannelDataArray<Vector2>(SpriteShapeDataType.ChannelTexCoord0, VertexAttribute.TexCoord0);
		colors = GetChannelDataArray<Color32>(SpriteShapeDataType.ChannelColor, VertexAttribute.Color);
		tangents = GetChannelDataArray<Vector4>(SpriteShapeDataType.ChannelTangent, VertexAttribute.Tangent);
		normals = GetChannelDataArray<Vector3>(SpriteShapeDataType.ChannelNormal, VertexAttribute.Normal);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_color_Injected(out Color ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_color_Injected(ref Color value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void Prepare_Injected(ref JobHandle handle, ref SpriteShapeParameters shapeParams, Sprite[] sprites);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetDataInfo_Injected(SpriteShapeDataType arrayType, out SpriteChannelInfo ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetChannelInfo_Injected(VertexAttribute channel, out SpriteChannelInfo ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void SetLocalAABB_Injected(ref Bounds bounds);
}
[MovedFrom("UnityEngine.Experimental.U2D")]
public struct SpriteShapeMetaData
{
	public float height;

	public float bevelCutoff;

	public float bevelSize;

	public uint spriteIndex;

	public bool corner;
}
[MovedFrom("UnityEngine.Experimental.U2D")]
public struct ShapeControlPoint
{
	public Vector3 position;

	public Vector3 leftTangent;

	public Vector3 rightTangent;

	public int mode;
}
[MovedFrom("UnityEngine.Experimental.U2D")]
public struct AngleRangeInfo
{
	public float start;

	public float end;

	public uint order;

	public int[] sprites;
}
[MovedFrom("UnityEngine.Experimental.U2D")]
[NativeHeader("Modules/SpriteShape/Public/SpriteShapeUtility.h")]
public class SpriteShapeUtility
{
	[FreeFunction("SpriteShapeUtility::Generate")]
	[NativeThrows]
	public static int[] Generate(Mesh mesh, SpriteShapeParameters shapeParams, ShapeControlPoint[] points, SpriteShapeMetaData[] metaData, AngleRangeInfo[] angleRange, Sprite[] sprites, Sprite[] corners)
	{
		return Generate_Injected(mesh, ref shapeParams, points, metaData, angleRange, sprites, corners);
	}

	[FreeFunction("SpriteShapeUtility::GenerateSpriteShape")]
	[NativeThrows]
	public static void GenerateSpriteShape(SpriteShapeRenderer renderer, SpriteShapeParameters shapeParams, ShapeControlPoint[] points, SpriteShapeMetaData[] metaData, AngleRangeInfo[] angleRange, Sprite[] sprites, Sprite[] corners)
	{
		GenerateSpriteShape_Injected(renderer, ref shapeParams, points, metaData, angleRange, sprites, corners);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int[] Generate_Injected(Mesh mesh, ref SpriteShapeParameters shapeParams, ShapeControlPoint[] points, SpriteShapeMetaData[] metaData, AngleRangeInfo[] angleRange, Sprite[] sprites, Sprite[] corners);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void GenerateSpriteShape_Injected(SpriteShapeRenderer renderer, ref SpriteShapeParameters shapeParams, ShapeControlPoint[] points, SpriteShapeMetaData[] metaData, AngleRangeInfo[] angleRange, Sprite[] sprites, Sprite[] corners);
}
