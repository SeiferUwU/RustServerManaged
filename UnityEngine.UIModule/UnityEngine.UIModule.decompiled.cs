using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

[assembly: InternalsVisibleTo("UnityEngine.ScreenCaptureModule")]
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
[assembly: InternalsVisibleTo("Unity.Burst")]
[assembly: InternalsVisibleTo("UnityEngine.PS5VRModule")]
[assembly: InternalsVisibleTo("Unity.Burst.Editor")]
[assembly: InternalsVisibleTo("Unity.IntegrationTests")]
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
[assembly: InternalsVisibleTo("Unity.DeploymentTests.Services")]
[assembly: InternalsVisibleTo("Assembly-CSharp-firstpass-testable")]
[assembly: InternalsVisibleTo("UnityEngine.PS5Module")]
[assembly: InternalsVisibleTo("UnityEngine.PS4Module")]
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
[assembly: InternalsVisibleTo("UnityEngine.PS4VRModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAssetBundleModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.UnityWebRequestAudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteShapeModule")]
[assembly: InternalsVisibleTo("UnityEngine.SpatialTracking")]
[assembly: InternalsVisibleTo("Unity.WindowsMRAutomation")]
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
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.006")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.018")]
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
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.019")]
[assembly: InternalsVisibleTo("GoogleAR.UnityNative")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.005")]
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.003")]
[assembly: InternalsVisibleTo("Unity.2D.Sprite.Editor")]
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
[assembly: InternalsVisibleTo("Unity.InternalAPIEngineBridge.004")]
[assembly: InternalsVisibleTo("UnityEngine.UI")]
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
[assembly: InternalsVisibleTo("Unity.Networking.Transport")]
[assembly: InternalsVisibleTo("UnityEngine.SpriteMaskModule")]
[assembly: InternalsVisibleTo("UnityEngine.PropertiesModule")]
[assembly: InternalsVisibleTo("UnityEngine.RuntimeInitializeOnLoadManagerInitializerModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterRendererModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClusterInputModule")]
[assembly: InternalsVisibleTo("UnityEngine.ClothModule")]
[assembly: InternalsVisibleTo("UnityEngine.AudioModule")]
[assembly: InternalsVisibleTo("UnityEngine.AssetBundleModule")]
[assembly: InternalsVisibleTo("UnityEngine.HotReloadModule")]
[assembly: InternalsVisibleTo("UnityEngine.AnimationModule")]
[assembly: InternalsVisibleTo("UnityEngine.AndroidJNIModule")]
[assembly: InternalsVisibleTo("UnityEngine.AccessibilityModule")]
[assembly: InternalsVisibleTo("UnityEngine.ContentLoadModule")]
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
[assembly: InternalsVisibleTo("UnityEngine.UnityConnectModule")]
[assembly: InternalsVisibleTo("UnityEngine.UnityAnalyticsCommonModule")]
[assembly: InternalsVisibleTo("UnityEngine.TLSModule")]
[assembly: UnityEngineModuleAssembly]
[assembly: InternalsVisibleTo("UnityEngine.ProfilerModule")]
[assembly: InternalsVisibleTo("UnityEngine.Physics2DModule")]
[assembly: InternalsVisibleTo("UnityEngine.PerformanceReportingModule")]
[assembly: InternalsVisibleTo("UnityEngine.ParticleSystemModule")]
[assembly: InternalsVisibleTo("UnityEngine.NVIDIAModule")]
[assembly: InternalsVisibleTo("UnityEngine.LocalizationModule")]
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
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace UnityEngine;

public interface ICanvasRaycastFilter
{
	bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera);
}
[NativeHeader("Modules/UI/CanvasGroup.h")]
[NativeClass("UI::CanvasGroup")]
public sealed class CanvasGroup : Behaviour, ICanvasRaycastFilter
{
	[NativeProperty("Alpha", false, TargetType.Function)]
	public extern float alpha
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("Interactable", false, TargetType.Function)]
	public extern bool interactable
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("BlocksRaycasts", false, TargetType.Function)]
	public extern bool blocksRaycasts
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("IgnoreParentGroups", false, TargetType.Function)]
	public extern bool ignoreParentGroups
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
	{
		return blocksRaycasts;
	}
}
[NativeHeader("Modules/UI/CanvasRenderer.h")]
[NativeClass("UI::CanvasRenderer")]
public sealed class CanvasRenderer : Component
{
	public extern bool hasPopInstruction
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern int materialCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern int popMaterialCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern int absoluteDepth
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern bool hasMoved
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern bool cullTransparentMesh
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("RectClipping", false, TargetType.Function)]
	public extern bool hasRectClipping
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	[NativeProperty("Depth", false, TargetType.Function)]
	public extern int relativeDepth
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	[NativeProperty("ShouldCull", false, TargetType.Function)]
	public extern bool cull
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[Obsolete("isMask is no longer supported.See EnableClipping for vertex clipping configuration", false)]
	public bool isMask { get; set; }

	public Vector2 clippingSoftness
	{
		get
		{
			get_clippingSoftness_Injected(out var ret);
			return ret;
		}
		set
		{
			set_clippingSoftness_Injected(ref value);
		}
	}

	public void SetColor(Color color)
	{
		SetColor_Injected(ref color);
	}

	public Color GetColor()
	{
		GetColor_Injected(out var ret);
		return ret;
	}

	public void EnableRectClipping(Rect rect)
	{
		EnableRectClipping_Injected(ref rect);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void DisableRectClipping();

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void SetMaterial(Material material, int index);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern Material GetMaterial(int index);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void SetPopMaterial(Material material, int index);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern Material GetPopMaterial(int index);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void SetTexture(Texture texture);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void SetAlphaTexture(Texture texture);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void SetMesh(Mesh mesh);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern Mesh GetMesh();

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void Clear();

	public float GetAlpha()
	{
		return GetColor().a;
	}

	public void SetAlpha(float alpha)
	{
		Color color = GetColor();
		color.a = alpha;
		SetColor(color);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern float GetInheritedAlpha();

	public void SetMaterial(Material material, Texture texture)
	{
		materialCount = Math.Max(1, materialCount);
		SetMaterial(material, 0);
		SetTexture(texture);
	}

	public Material GetMaterial()
	{
		return GetMaterial(0);
	}

	public static void SplitUIVertexStreams(List<UIVertex> verts, List<Vector3> positions, List<Color32> colors, List<Vector4> uv0S, List<Vector4> uv1S, List<Vector3> normals, List<Vector4> tangents, List<int> indices)
	{
		SplitUIVertexStreams(verts, positions, colors, uv0S, uv1S, new List<Vector4>(), new List<Vector4>(), normals, tangents, indices);
	}

	public static void SplitUIVertexStreams(List<UIVertex> verts, List<Vector3> positions, List<Color32> colors, List<Vector4> uv0S, List<Vector4> uv1S, List<Vector4> uv2S, List<Vector4> uv3S, List<Vector3> normals, List<Vector4> tangents, List<int> indices)
	{
		SplitUIVertexStreamsInternal(verts, positions, colors, uv0S, uv1S, uv2S, uv3S, normals, tangents);
		SplitIndicesStreamsInternal(verts, indices);
	}

	public static void CreateUIVertexStream(List<UIVertex> verts, List<Vector3> positions, List<Color32> colors, List<Vector4> uv0S, List<Vector4> uv1S, List<Vector3> normals, List<Vector4> tangents, List<int> indices)
	{
		CreateUIVertexStream(verts, positions, colors, uv0S, uv1S, new List<Vector4>(), new List<Vector4>(), normals, tangents, indices);
	}

	public static void CreateUIVertexStream(List<UIVertex> verts, List<Vector3> positions, List<Color32> colors, List<Vector4> uv0S, List<Vector4> uv1S, List<Vector4> uv2S, List<Vector4> uv3S, List<Vector3> normals, List<Vector4> tangents, List<int> indices)
	{
		CreateUIVertexStreamInternal(verts, positions, colors, uv0S, uv1S, uv2S, uv3S, normals, tangents, indices);
	}

	public static void AddUIVertexStream(List<UIVertex> verts, List<Vector3> positions, List<Color32> colors, List<Vector4> uv0S, List<Vector4> uv1S, List<Vector3> normals, List<Vector4> tangents)
	{
		AddUIVertexStream(verts, positions, colors, uv0S, uv1S, new List<Vector4>(), new List<Vector4>(), normals, tangents);
	}

	public static void AddUIVertexStream(List<UIVertex> verts, List<Vector3> positions, List<Color32> colors, List<Vector4> uv0S, List<Vector4> uv1S, List<Vector4> uv2S, List<Vector4> uv3S, List<Vector3> normals, List<Vector4> tangents)
	{
		SplitUIVertexStreamsInternal(verts, positions, colors, uv0S, uv1S, uv2S, uv3S, normals, tangents);
	}

	[Obsolete("UI System now uses meshes.Generate a mesh and use 'SetMesh' instead", false)]
	public void SetVertices(List<UIVertex> vertices)
	{
		SetVertices(vertices.ToArray(), vertices.Count);
	}

	[Obsolete("UI System now uses meshes.Generate a mesh and use 'SetMesh' instead", false)]
	public void SetVertices(UIVertex[] vertices, int size)
	{
		Mesh mesh = new Mesh();
		List<Vector3> list = new List<Vector3>();
		List<Color32> list2 = new List<Color32>();
		List<Vector4> list3 = new List<Vector4>();
		List<Vector4> list4 = new List<Vector4>();
		List<Vector4> list5 = new List<Vector4>();
		List<Vector4> list6 = new List<Vector4>();
		List<Vector3> list7 = new List<Vector3>();
		List<Vector4> list8 = new List<Vector4>();
		List<int> list9 = new List<int>();
		for (int i = 0; i < size; i += 4)
		{
			for (int j = 0; j < 4; j++)
			{
				list.Add(vertices[i + j].position);
				list2.Add(vertices[i + j].color);
				list3.Add(vertices[i + j].uv0);
				list4.Add(vertices[i + j].uv1);
				list5.Add(vertices[i + j].uv2);
				list6.Add(vertices[i + j].uv3);
				list7.Add(vertices[i + j].normal);
				list8.Add(vertices[i + j].tangent);
			}
			list9.Add(i);
			list9.Add(i + 1);
			list9.Add(i + 2);
			list9.Add(i + 2);
			list9.Add(i + 3);
			list9.Add(i);
		}
		mesh.SetVertices(list);
		mesh.SetColors(list2);
		mesh.SetNormals(list7);
		mesh.SetTangents(list8);
		mesh.SetUVs(0, list3);
		mesh.SetUVs(1, list4);
		mesh.SetUVs(2, list5);
		mesh.SetUVs(3, list6);
		mesh.SetIndices(list9.ToArray(), MeshTopology.Triangles, 0);
		SetMesh(mesh);
		Object.DestroyImmediate(mesh);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[StaticAccessor("UI", StaticAccessorType.DoubleColon)]
	private static extern void SplitIndicesStreamsInternal(object verts, object indices);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[StaticAccessor("UI", StaticAccessorType.DoubleColon)]
	private static extern void SplitUIVertexStreamsInternal(object verts, object positions, object colors, object uv0S, object uv1S, object uv2S, object uv3S, object normals, object tangents);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[StaticAccessor("UI", StaticAccessorType.DoubleColon)]
	private static extern void CreateUIVertexStreamInternal(object verts, object positions, object colors, object uv0S, object uv1S, object uv2S, object uv3S, object normals, object tangents, object indices);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void SetColor_Injected(ref Color color);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetColor_Injected(out Color ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void EnableRectClipping_Injected(ref Rect rect);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_clippingSoftness_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_clippingSoftness_Injected(ref Vector2 value);
}
[StaticAccessor("UI", StaticAccessorType.DoubleColon)]
[NativeHeader("Runtime/Transform/RectTransform.h")]
[NativeHeader("Modules/UI/RectTransformUtil.h")]
[NativeHeader("Modules/UI/Canvas.h")]
[NativeHeader("Runtime/Camera/Camera.h")]
public sealed class RectTransformUtility
{
	private static readonly Vector3[] s_Corners = new Vector3[4];

	public static Vector2 PixelAdjustPoint(Vector2 point, Transform elementTransform, Canvas canvas)
	{
		PixelAdjustPoint_Injected(ref point, elementTransform, canvas, out var ret);
		return ret;
	}

	public static Rect PixelAdjustRect(RectTransform rectTransform, Canvas canvas)
	{
		PixelAdjustRect_Injected(rectTransform, canvas, out var ret);
		return ret;
	}

	private static bool PointInRectangle(Vector2 screenPoint, RectTransform rect, Camera cam, Vector4 offset)
	{
		return PointInRectangle_Injected(ref screenPoint, rect, cam, ref offset);
	}

	private RectTransformUtility()
	{
	}

	public static bool RectangleContainsScreenPoint(RectTransform rect, Vector2 screenPoint)
	{
		return RectangleContainsScreenPoint(rect, screenPoint, null);
	}

	public static bool RectangleContainsScreenPoint(RectTransform rect, Vector2 screenPoint, Camera cam)
	{
		return RectangleContainsScreenPoint(rect, screenPoint, cam, Vector4.zero);
	}

	public static bool RectangleContainsScreenPoint(RectTransform rect, Vector2 screenPoint, Camera cam, Vector4 offset)
	{
		return PointInRectangle(screenPoint, rect, cam, offset);
	}

	public static bool ScreenPointToWorldPointInRectangle(RectTransform rect, Vector2 screenPoint, Camera cam, out Vector3 worldPoint)
	{
		worldPoint = Vector2.zero;
		Ray ray = ScreenPointToRay(cam, screenPoint);
		Plane plane = new Plane(rect.rotation * Vector3.back, rect.position);
		float enter = 0f;
		float num = Vector3.Dot(Vector3.Normalize(rect.position - ray.origin), plane.normal);
		if (num != 0f && !plane.Raycast(ray, out enter))
		{
			return false;
		}
		worldPoint = ray.GetPoint(enter);
		return true;
	}

	public static bool ScreenPointToLocalPointInRectangle(RectTransform rect, Vector2 screenPoint, Camera cam, out Vector2 localPoint)
	{
		localPoint = Vector2.zero;
		if (ScreenPointToWorldPointInRectangle(rect, screenPoint, cam, out var worldPoint))
		{
			localPoint = rect.InverseTransformPoint(worldPoint);
			return true;
		}
		return false;
	}

	public static Ray ScreenPointToRay(Camera cam, Vector2 screenPos)
	{
		if (cam != null)
		{
			return cam.ScreenPointToRay(screenPos);
		}
		Vector3 origin = screenPos;
		origin.z -= 100f;
		return new Ray(origin, Vector3.forward);
	}

	public static Vector2 WorldToScreenPoint(Camera cam, Vector3 worldPoint)
	{
		if (cam == null)
		{
			return new Vector2(worldPoint.x, worldPoint.y);
		}
		return cam.WorldToScreenPoint(worldPoint);
	}

	public static Bounds CalculateRelativeRectTransformBounds(Transform root, Transform child)
	{
		RectTransform[] componentsInChildren = child.GetComponentsInChildren<RectTransform>(includeInactive: false);
		if (componentsInChildren.Length != 0)
		{
			Vector3 vector = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
			Vector3 vector2 = new Vector3(float.MinValue, float.MinValue, float.MinValue);
			Matrix4x4 worldToLocalMatrix = root.worldToLocalMatrix;
			int i = 0;
			for (int num = componentsInChildren.Length; i < num; i++)
			{
				componentsInChildren[i].GetWorldCorners(s_Corners);
				for (int j = 0; j < 4; j++)
				{
					Vector3 lhs = worldToLocalMatrix.MultiplyPoint3x4(s_Corners[j]);
					vector = Vector3.Min(lhs, vector);
					vector2 = Vector3.Max(lhs, vector2);
				}
			}
			Bounds result = new Bounds(vector, Vector3.zero);
			result.Encapsulate(vector2);
			return result;
		}
		return new Bounds(Vector3.zero, Vector3.zero);
	}

	public static Bounds CalculateRelativeRectTransformBounds(Transform trans)
	{
		return CalculateRelativeRectTransformBounds(trans, trans);
	}

	public static void FlipLayoutOnAxis(RectTransform rect, int axis, bool keepPositioning, bool recursive)
	{
		if (rect == null)
		{
			return;
		}
		if (recursive)
		{
			for (int i = 0; i < rect.childCount; i++)
			{
				RectTransform rectTransform = rect.GetChild(i) as RectTransform;
				if (rectTransform != null)
				{
					FlipLayoutOnAxis(rectTransform, axis, keepPositioning: false, recursive: true);
				}
			}
		}
		Vector2 pivot = rect.pivot;
		pivot[axis] = 1f - pivot[axis];
		rect.pivot = pivot;
		if (!keepPositioning)
		{
			Vector2 anchoredPosition = rect.anchoredPosition;
			anchoredPosition[axis] = 0f - anchoredPosition[axis];
			rect.anchoredPosition = anchoredPosition;
			Vector2 anchorMin = rect.anchorMin;
			Vector2 anchorMax = rect.anchorMax;
			float num = anchorMin[axis];
			anchorMin[axis] = 1f - anchorMax[axis];
			anchorMax[axis] = 1f - num;
			rect.anchorMin = anchorMin;
			rect.anchorMax = anchorMax;
		}
	}

	public static void FlipLayoutAxes(RectTransform rect, bool keepPositioning, bool recursive)
	{
		if (rect == null)
		{
			return;
		}
		if (recursive)
		{
			for (int i = 0; i < rect.childCount; i++)
			{
				RectTransform rectTransform = rect.GetChild(i) as RectTransform;
				if (rectTransform != null)
				{
					FlipLayoutAxes(rectTransform, keepPositioning: false, recursive: true);
				}
			}
		}
		rect.pivot = GetTransposed(rect.pivot);
		rect.sizeDelta = GetTransposed(rect.sizeDelta);
		if (!keepPositioning)
		{
			rect.anchoredPosition = GetTransposed(rect.anchoredPosition);
			rect.anchorMin = GetTransposed(rect.anchorMin);
			rect.anchorMax = GetTransposed(rect.anchorMax);
		}
	}

	private static Vector2 GetTransposed(Vector2 input)
	{
		return new Vector2(input.y, input.x);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void PixelAdjustPoint_Injected(ref Vector2 point, Transform elementTransform, Canvas canvas, out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void PixelAdjustRect_Injected(RectTransform rectTransform, Canvas canvas, out Rect ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool PointInRectangle_Injected(ref Vector2 screenPoint, RectTransform rect, Camera cam, ref Vector4 offset);
}
public enum RenderMode
{
	ScreenSpaceOverlay,
	ScreenSpaceCamera,
	WorldSpace
}
public enum StandaloneRenderResize
{
	Enabled,
	Disabled
}
[Flags]
public enum AdditionalCanvasShaderChannels
{
	None = 0,
	TexCoord1 = 1,
	TexCoord2 = 2,
	TexCoord3 = 4,
	Normal = 8,
	Tangent = 0x10
}
[NativeHeader("Modules/UI/UIStructs.h")]
[NativeHeader("Modules/UI/Canvas.h")]
[NativeHeader("Modules/UI/CanvasManager.h")]
[NativeClass("UI::Canvas")]
[RequireComponent(typeof(RectTransform))]
public sealed class Canvas : Behaviour
{
	public delegate void WillRenderCanvases();

	public extern RenderMode renderMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool isRootCanvas
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public Rect pixelRect
	{
		get
		{
			get_pixelRect_Injected(out var ret);
			return ret;
		}
	}

	public extern float scaleFactor
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float referencePixelsPerUnit
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool overridePixelPerfect
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool vertexColorAlwaysGammaSpace
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool pixelPerfect
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float planeDistance
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern int renderOrder
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern bool overrideSorting
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern int sortingOrder
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern int targetDisplay
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern int sortingLayerID
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern int cachedSortingLayerValue
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern AdditionalCanvasShaderChannels additionalShaderChannels
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern string sortingLayerName
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern Canvas rootCanvas
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public Vector2 renderingDisplaySize
	{
		get
		{
			get_renderingDisplaySize_Injected(out var ret);
			return ret;
		}
	}

	public extern StandaloneRenderResize updateRectTransformForStandalone
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	internal static Action<int> externBeginRenderOverlays { get; set; }

	internal static Action<int, int> externRenderOverlaysBefore { get; set; }

	internal static Action<int> externEndRenderOverlays { get; set; }

	[NativeProperty("Camera", false, TargetType.Function)]
	public extern Camera worldCamera
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("SortingBucketNormalizedSize", false, TargetType.Function)]
	public extern float normalizedSortingGridSize
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("SortingBucketNormalizedSize", false, TargetType.Function)]
	[Obsolete("Setting normalizedSize via a int is not supported. Please use normalizedSortingGridSize", false)]
	public extern int sortingGridNormalizedSize
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public static event WillRenderCanvases preWillRenderCanvases;

	public static event WillRenderCanvases willRenderCanvases;

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("UI::CanvasManager::SetExternalCanvasEnabled")]
	internal static extern void SetExternalCanvasEnabled(bool enabled);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("UI::GetDefaultUIMaterial")]
	[Obsolete("Shared default material now used for text and general UI elements, call Canvas.GetDefaultCanvasMaterial()", false)]
	public static extern Material GetDefaultCanvasTextMaterial();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("UI::GetDefaultUIMaterial")]
	public static extern Material GetDefaultCanvasMaterial();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("UI::GetETC1SupportedCanvasMaterial")]
	public static extern Material GetETC1SupportedCanvasMaterial();

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal extern void UpdateCanvasRectTransform(bool alignWithCamera);

	public static void ForceUpdateCanvases()
	{
		SendPreWillRenderCanvases();
		SendWillRenderCanvases();
	}

	[RequiredByNativeCode]
	private static void SendPreWillRenderCanvases()
	{
		Canvas.preWillRenderCanvases?.Invoke();
	}

	[RequiredByNativeCode]
	private static void SendWillRenderCanvases()
	{
		Canvas.willRenderCanvases?.Invoke();
	}

	[RequiredByNativeCode]
	private static void BeginRenderExtraOverlays(int displayIndex)
	{
		externBeginRenderOverlays?.Invoke(displayIndex);
	}

	[RequiredByNativeCode]
	private static void RenderExtraOverlaysBefore(int displayIndex, int sortingOrder)
	{
		externRenderOverlaysBefore?.Invoke(displayIndex, sortingOrder);
	}

	[RequiredByNativeCode]
	private static void EndRenderExtraOverlays(int displayIndex)
	{
		externEndRenderOverlays?.Invoke(displayIndex);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_pixelRect_Injected(out Rect ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_renderingDisplaySize_Injected(out Vector2 ret);
}
[StaticAccessor("UI::SystemProfilerApi", StaticAccessorType.DoubleColon)]
[IgnoredByDeepProfiler]
[NativeHeader("Modules/UI/Canvas.h")]
public static class UISystemProfilerApi
{
	public enum SampleType
	{
		Layout,
		Render
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern void BeginSample(SampleType type);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern void EndSample(SampleType type);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern void AddMarker(string name, Object obj);
}
