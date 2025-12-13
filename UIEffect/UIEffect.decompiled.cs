using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyVersion("0.0.0.0")]
public static class Packer
{
	public static float ToFloat(float x, float y, float z, float w)
	{
		x = ((x < 0f) ? 0f : ((1f < x) ? 1f : x));
		y = ((y < 0f) ? 0f : ((1f < y) ? 1f : y));
		z = ((z < 0f) ? 0f : ((1f < z) ? 1f : z));
		w = ((w < 0f) ? 0f : ((1f < w) ? 1f : w));
		return (Mathf.FloorToInt(w * 63f) << 18) + (Mathf.FloorToInt(z * 63f) << 12) + (Mathf.FloorToInt(y * 63f) << 6) + Mathf.FloorToInt(x * 63f);
	}

	public static float ToFloat(Vector4 factor)
	{
		return ToFloat(Mathf.Clamp01(factor.x), Mathf.Clamp01(factor.y), Mathf.Clamp01(factor.z), Mathf.Clamp01(factor.w));
	}

	public static float ToFloat(float x, float y, float z)
	{
		x = ((x < 0f) ? 0f : ((1f < x) ? 1f : x));
		y = ((y < 0f) ? 0f : ((1f < y) ? 1f : y));
		z = ((z < 0f) ? 0f : ((1f < z) ? 1f : z));
		return (Mathf.FloorToInt(z * 255f) << 16) + (Mathf.FloorToInt(y * 255f) << 8) + Mathf.FloorToInt(x * 255f);
	}

	public static float ToFloat(float x, float y)
	{
		x = ((x < 0f) ? 0f : ((1f < x) ? 1f : x));
		y = ((y < 0f) ? 0f : ((1f < y) ? 1f : y));
		return (Mathf.FloorToInt(y * 4095f) << 12) + Mathf.FloorToInt(x * 4095f);
	}
}
[CompilerGenerated]
[EditorBrowsable(EditorBrowsableState.Never)]
[GeneratedCode("Unity.MonoScriptGenerator.MonoScriptInfoGenerator", null)]
internal class UnitySourceGeneratedAssemblyMonoScriptTypes_v1
{
	private struct MonoScriptData
	{
		public byte[] FilePathsData;

		public byte[] TypesData;

		public int TotalTypes;

		public int TotalFiles;

		public bool IsEditorOnly;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static MonoScriptData Get()
	{
		return new MonoScriptData
		{
			FilePathsData = new byte[997]
			{
				0, 0, 0, 1, 0, 0, 0, 61, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 85, 73, 69, 102, 102, 101,
				99, 116, 92, 83, 99, 114, 105, 112, 116, 115,
				92, 67, 111, 109, 109, 111, 110, 92, 66, 97,
				115, 101, 77, 97, 116, 101, 114, 105, 97, 108,
				69, 102, 102, 101, 99, 116, 46, 99, 115, 0,
				0, 0, 1, 0, 0, 0, 57, 92, 65, 115,
				115, 101, 116, 115, 92, 80, 108, 117, 103, 105,
				110, 115, 92, 85, 73, 69, 102, 102, 101, 99,
				116, 92, 83, 99, 114, 105, 112, 116, 115, 92,
				67, 111, 109, 109, 111, 110, 92, 66, 97, 115,
				101, 77, 101, 115, 104, 69, 102, 102, 101, 99,
				116, 46, 99, 115, 0, 0, 0, 1, 0, 0,
				0, 59, 92, 65, 115, 115, 101, 116, 115, 92,
				80, 108, 117, 103, 105, 110, 115, 92, 85, 73,
				69, 102, 102, 101, 99, 116, 92, 83, 99, 114,
				105, 112, 116, 115, 92, 67, 111, 109, 109, 111,
				110, 92, 71, 114, 97, 112, 104, 105, 99, 67,
				111, 110, 110, 101, 99, 116, 111, 114, 46, 99,
				115, 0, 0, 0, 2, 0, 0, 0, 56, 92,
				65, 115, 115, 101, 116, 115, 92, 80, 108, 117,
				103, 105, 110, 115, 92, 85, 73, 69, 102, 102,
				101, 99, 116, 92, 83, 99, 114, 105, 112, 116,
				115, 92, 67, 111, 109, 109, 111, 110, 92, 77,
				97, 116, 101, 114, 105, 97, 108, 67, 97, 99,
				104, 101, 46, 99, 115, 0, 0, 0, 1, 0,
				0, 0, 52, 92, 65, 115, 115, 101, 116, 115,
				92, 80, 108, 117, 103, 105, 110, 115, 92, 85,
				73, 69, 102, 102, 101, 99, 116, 92, 83, 99,
				114, 105, 112, 116, 115, 92, 67, 111, 109, 109,
				111, 110, 92, 77, 97, 116, 114, 105, 120, 50,
				120, 51, 46, 99, 115, 0, 0, 0, 1, 0,
				0, 0, 49, 92, 65, 115, 115, 101, 116, 115,
				92, 80, 108, 117, 103, 105, 110, 115, 92, 85,
				73, 69, 102, 102, 101, 99, 116, 92, 83, 99,
				114, 105, 112, 116, 115, 92, 67, 111, 109, 109,
				111, 110, 92, 80, 97, 99, 107, 101, 114, 46,
				99, 115, 0, 0, 0, 2, 0, 0, 0, 59,
				92, 65, 115, 115, 101, 116, 115, 92, 80, 108,
				117, 103, 105, 110, 115, 92, 85, 73, 69, 102,
				102, 101, 99, 116, 92, 83, 99, 114, 105, 112,
				116, 115, 92, 67, 111, 109, 109, 111, 110, 92,
				80, 97, 114, 97, 109, 101, 116, 101, 114, 84,
				101, 120, 116, 117, 114, 101, 46, 99, 115, 0,
				0, 0, 1, 0, 0, 0, 52, 92, 65, 115,
				115, 101, 116, 115, 92, 80, 108, 117, 103, 105,
				110, 115, 92, 85, 73, 69, 102, 102, 101, 99,
				116, 92, 83, 99, 114, 105, 112, 116, 115, 92,
				69, 110, 117, 109, 115, 92, 69, 102, 102, 101,
				99, 116, 65, 114, 101, 97, 46, 99, 115, 0,
				0, 0, 1, 0, 0, 0, 46, 92, 65, 115,
				115, 101, 116, 115, 92, 80, 108, 117, 103, 105,
				110, 115, 92, 85, 73, 69, 102, 102, 101, 99,
				116, 92, 83, 99, 114, 105, 112, 116, 115, 92,
				85, 73, 68, 105, 115, 115, 111, 108, 118, 101,
				46, 99, 115, 0, 0, 0, 1, 0, 0, 0,
				44, 92, 65, 115, 115, 101, 116, 115, 92, 80,
				108, 117, 103, 105, 110, 115, 92, 85, 73, 69,
				102, 102, 101, 99, 116, 92, 83, 99, 114, 105,
				112, 116, 115, 92, 85, 73, 69, 102, 102, 101,
				99, 116, 46, 99, 115, 0, 0, 0, 1, 0,
				0, 0, 42, 92, 65, 115, 115, 101, 116, 115,
				92, 80, 108, 117, 103, 105, 110, 115, 92, 85,
				73, 69, 102, 102, 101, 99, 116, 92, 83, 99,
				114, 105, 112, 116, 115, 92, 85, 73, 70, 108,
				105, 112, 46, 99, 115, 0, 0, 0, 1, 0,
				0, 0, 46, 92, 65, 115, 115, 101, 116, 115,
				92, 80, 108, 117, 103, 105, 110, 115, 92, 85,
				73, 69, 102, 102, 101, 99, 116, 92, 83, 99,
				114, 105, 112, 116, 115, 92, 85, 73, 71, 114,
				97, 100, 105, 101, 110, 116, 46, 99, 115, 0,
				0, 0, 1, 0, 0, 0, 49, 92, 65, 115,
				115, 101, 116, 115, 92, 80, 108, 117, 103, 105,
				110, 115, 92, 85, 73, 69, 102, 102, 101, 99,
				116, 92, 83, 99, 114, 105, 112, 116, 115, 92,
				85, 73, 72, 115, 118, 77, 111, 100, 105, 102,
				105, 101, 114, 46, 99, 115, 0, 0, 0, 1,
				0, 0, 0, 44, 92, 65, 115, 115, 101, 116,
				115, 92, 80, 108, 117, 103, 105, 110, 115, 92,
				85, 73, 69, 102, 102, 101, 99, 116, 92, 83,
				99, 114, 105, 112, 116, 115, 92, 85, 73, 83,
				104, 97, 100, 111, 119, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 43, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 85, 73, 69, 102, 102, 101, 99, 116,
				92, 83, 99, 114, 105, 112, 116, 115, 92, 85,
				73, 83, 104, 105, 110, 121, 46, 99, 115, 0,
				0, 0, 1, 0, 0, 0, 48, 92, 65, 115,
				115, 101, 116, 115, 92, 80, 108, 117, 103, 105,
				110, 115, 92, 85, 73, 69, 102, 102, 101, 99,
				116, 92, 83, 99, 114, 105, 112, 116, 115, 92,
				85, 73, 83, 121, 110, 99, 69, 102, 102, 101,
				99, 116, 46, 99, 115, 0, 0, 0, 1, 0,
				0, 0, 54, 92, 65, 115, 115, 101, 116, 115,
				92, 80, 108, 117, 103, 105, 110, 115, 92, 85,
				73, 69, 102, 102, 101, 99, 116, 92, 83, 99,
				114, 105, 112, 116, 115, 92, 85, 73, 84, 114,
				97, 110, 115, 105, 116, 105, 111, 110, 69, 102,
				102, 101, 99, 116, 46, 99, 115
			},
			TypesData = new byte[650]
			{
				0, 0, 0, 0, 35, 67, 111, 102, 102, 101,
				101, 46, 85, 73, 69, 102, 102, 101, 99, 116,
				115, 124, 66, 97, 115, 101, 77, 97, 116, 101,
				114, 105, 97, 108, 69, 102, 102, 101, 99, 116,
				0, 0, 0, 0, 31, 67, 111, 102, 102, 101,
				101, 46, 85, 73, 69, 102, 102, 101, 99, 116,
				115, 124, 66, 97, 115, 101, 77, 101, 115, 104,
				69, 102, 102, 101, 99, 116, 0, 0, 0, 0,
				33, 67, 111, 102, 102, 101, 101, 46, 85, 73,
				69, 102, 102, 101, 99, 116, 115, 124, 71, 114,
				97, 112, 104, 105, 99, 67, 111, 110, 110, 101,
				99, 116, 111, 114, 0, 0, 0, 0, 30, 67,
				111, 102, 102, 101, 101, 46, 85, 73, 69, 102,
				102, 101, 99, 116, 115, 124, 77, 97, 116, 101,
				114, 105, 97, 108, 67, 97, 99, 104, 101, 0,
				0, 0, 0, 44, 67, 111, 102, 102, 101, 101,
				46, 85, 73, 69, 102, 102, 101, 99, 116, 115,
				46, 77, 97, 116, 101, 114, 105, 97, 108, 67,
				97, 99, 104, 101, 124, 77, 97, 116, 101, 114,
				105, 97, 108, 69, 110, 116, 114, 121, 0, 0,
				0, 0, 26, 67, 111, 102, 102, 101, 101, 46,
				85, 73, 69, 102, 102, 101, 99, 116, 115, 124,
				77, 97, 116, 114, 105, 120, 50, 120, 51, 0,
				0, 0, 0, 7, 124, 80, 97, 99, 107, 101,
				114, 0, 0, 0, 0, 34, 67, 111, 102, 102,
				101, 101, 46, 85, 73, 69, 102, 102, 101, 99,
				116, 115, 124, 73, 80, 97, 114, 97, 109, 101,
				116, 101, 114, 84, 101, 120, 116, 117, 114, 101,
				0, 0, 0, 0, 33, 67, 111, 102, 102, 101,
				101, 46, 85, 73, 69, 102, 102, 101, 99, 116,
				115, 124, 80, 97, 114, 97, 109, 101, 116, 101,
				114, 84, 101, 120, 116, 117, 114, 101, 0, 0,
				0, 0, 37, 67, 111, 102, 102, 101, 101, 46,
				85, 73, 69, 102, 102, 101, 99, 116, 115, 124,
				69, 102, 102, 101, 99, 116, 65, 114, 101, 97,
				69, 120, 116, 101, 110, 115, 105, 111, 110, 115,
				0, 0, 0, 0, 27, 67, 111, 102, 102, 101,
				101, 46, 85, 73, 69, 102, 102, 101, 99, 116,
				115, 124, 85, 73, 68, 105, 115, 115, 111, 108,
				118, 101, 0, 0, 0, 0, 25, 67, 111, 102,
				102, 101, 101, 46, 85, 73, 69, 102, 102, 101,
				99, 116, 115, 124, 85, 73, 69, 102, 102, 101,
				99, 116, 0, 0, 0, 0, 23, 67, 111, 102,
				102, 101, 101, 46, 85, 73, 69, 102, 102, 101,
				99, 116, 115, 124, 85, 73, 70, 108, 105, 112,
				0, 0, 0, 0, 27, 67, 111, 102, 102, 101,
				101, 46, 85, 73, 69, 102, 102, 101, 99, 116,
				115, 124, 85, 73, 71, 114, 97, 100, 105, 101,
				110, 116, 0, 0, 0, 0, 30, 67, 111, 102,
				102, 101, 101, 46, 85, 73, 69, 102, 102, 101,
				99, 116, 115, 124, 85, 73, 72, 115, 118, 77,
				111, 100, 105, 102, 105, 101, 114, 0, 0, 0,
				0, 25, 67, 111, 102, 102, 101, 101, 46, 85,
				73, 69, 102, 102, 101, 99, 116, 115, 124, 85,
				73, 83, 104, 97, 100, 111, 119, 0, 0, 0,
				0, 24, 67, 111, 102, 102, 101, 101, 46, 85,
				73, 69, 102, 102, 101, 99, 116, 115, 124, 85,
				73, 83, 104, 105, 110, 121, 0, 0, 0, 0,
				29, 67, 111, 102, 102, 101, 101, 46, 85, 73,
				69, 102, 102, 101, 99, 116, 115, 124, 85, 73,
				83, 121, 110, 99, 69, 102, 102, 101, 99, 116,
				0, 0, 0, 0, 35, 67, 111, 102, 102, 101,
				101, 46, 85, 73, 69, 102, 102, 101, 99, 116,
				115, 124, 85, 73, 84, 114, 97, 110, 115, 105,
				116, 105, 111, 110, 69, 102, 102, 101, 99, 116
			},
			TotalFiles = 17,
			TotalTypes = 19,
			IsEditorOnly = false
		};
	}
}
namespace Coffee.UIEffects;

[DisallowMultipleComponent]
public abstract class BaseMaterialEffect : BaseMeshEffect, IParameterTexture, IMaterialModifier
{
	protected static readonly Hash128 k_InvalidHash = default(Hash128);

	protected static readonly List<UIVertex> s_TempVerts = new List<UIVertex>();

	private static readonly StringBuilder s_StringBuilder = new StringBuilder();

	private Hash128 _effectMaterialHash;

	public int parameterIndex { get; set; }

	public virtual ParameterTexture paramTex => null;

	public void SetMaterialDirty()
	{
		base.connector.SetMaterialDirty(base.graphic);
		foreach (UISyncEffect syncEffect in syncEffects)
		{
			syncEffect.SetMaterialDirty();
		}
	}

	public virtual Hash128 GetMaterialHash(Material baseMaterial)
	{
		return k_InvalidHash;
	}

	public Material GetModifiedMaterial(Material baseMaterial)
	{
		return GetModifiedMaterial(baseMaterial, base.graphic);
	}

	public virtual Material GetModifiedMaterial(Material baseMaterial, Graphic graphic)
	{
		if (!base.isActiveAndEnabled)
		{
			return baseMaterial;
		}
		Hash128 effectMaterialHash = _effectMaterialHash;
		_effectMaterialHash = GetMaterialHash(baseMaterial);
		Material result = baseMaterial;
		if (_effectMaterialHash.isValid)
		{
			result = MaterialCache.Register(baseMaterial, _effectMaterialHash, ModifyMaterial, graphic);
		}
		MaterialCache.Unregister(effectMaterialHash);
		return result;
	}

	public virtual void ModifyMaterial(Material newMaterial, Graphic graphic)
	{
		if (base.isActiveAndEnabled && paramTex != null)
		{
			paramTex.RegisterMaterial(newMaterial);
		}
	}

	protected void SetShaderVariants(Material newMaterial, params object[] variants)
	{
		string[] array = (newMaterial.shaderKeywords = (from x in variants
			where 0 < (int)x
			select x.ToString().ToUpper()).Concat(newMaterial.shaderKeywords).Distinct().ToArray());
		s_StringBuilder.Length = 0;
		s_StringBuilder.Append(Path.GetFileName(newMaterial.shader.name));
		string[] array3 = array;
		foreach (string value in array3)
		{
			s_StringBuilder.Append("-");
			s_StringBuilder.Append(value);
		}
		newMaterial.name = s_StringBuilder.ToString();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		if (paramTex != null)
		{
			paramTex.Register(this);
		}
		SetMaterialDirty();
		SetEffectParamsDirty();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		SetMaterialDirty();
		if (paramTex != null)
		{
			paramTex.Unregister(this);
		}
		MaterialCache.Unregister(_effectMaterialHash);
		_effectMaterialHash = k_InvalidHash;
	}
}
[RequireComponent(typeof(Graphic))]
[RequireComponent(typeof(RectTransform))]
[ExecuteAlways]
public abstract class BaseMeshEffect : UIBehaviour, IMeshModifier
{
	private RectTransform _rectTransform;

	private Graphic _graphic;

	private GraphicConnector _connector;

	internal readonly List<UISyncEffect> syncEffects = new List<UISyncEffect>(0);

	protected GraphicConnector connector => _connector ?? (_connector = GraphicConnector.FindConnector(graphic));

	public Graphic graphic
	{
		get
		{
			if (!_graphic)
			{
				return _graphic = GetComponent<Graphic>();
			}
			return _graphic;
		}
	}

	protected RectTransform rectTransform
	{
		get
		{
			if (!_rectTransform)
			{
				return _rectTransform = GetComponent<RectTransform>();
			}
			return _rectTransform;
		}
	}

	public virtual void ModifyMesh(Mesh mesh)
	{
	}

	public virtual void ModifyMesh(VertexHelper vh)
	{
		ModifyMesh(vh, graphic);
	}

	public virtual void ModifyMesh(VertexHelper vh, Graphic graphic)
	{
	}

	protected virtual void SetVerticesDirty()
	{
		connector.SetVerticesDirty(graphic);
		foreach (UISyncEffect syncEffect in syncEffects)
		{
			syncEffect.SetVerticesDirty();
		}
	}

	protected override void OnEnable()
	{
		connector.OnEnable(graphic);
		SetVerticesDirty();
	}

	protected override void OnDisable()
	{
		connector.OnDisable(graphic);
		SetVerticesDirty();
	}

	protected virtual void SetEffectParamsDirty()
	{
		if (base.isActiveAndEnabled)
		{
			SetVerticesDirty();
		}
	}

	protected override void OnDidApplyAnimationProperties()
	{
		if (base.isActiveAndEnabled)
		{
			SetEffectParamsDirty();
		}
	}
}
public class GraphicConnector
{
	private static readonly List<GraphicConnector> s_Connectors = new List<GraphicConnector>();

	private static readonly Dictionary<Type, GraphicConnector> s_ConnectorMap = new Dictionary<Type, GraphicConnector>();

	private static readonly GraphicConnector s_EmptyConnector = new GraphicConnector();

	protected virtual int priority => -1;

	public virtual AdditionalCanvasShaderChannels extraChannel => AdditionalCanvasShaderChannels.TexCoord1;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Init()
	{
		AddConnector(new GraphicConnector());
	}

	protected static void AddConnector(GraphicConnector connector)
	{
		s_Connectors.Add(connector);
		s_Connectors.Sort((GraphicConnector x, GraphicConnector y) => y.priority - x.priority);
	}

	public static GraphicConnector FindConnector(Graphic graphic)
	{
		if (!graphic)
		{
			return s_EmptyConnector;
		}
		Type type = graphic.GetType();
		GraphicConnector value = null;
		if (s_ConnectorMap.TryGetValue(type, out value))
		{
			return value;
		}
		foreach (GraphicConnector s_Connector in s_Connectors)
		{
			if (s_Connector.IsValid(graphic))
			{
				s_ConnectorMap.Add(type, s_Connector);
				return s_Connector;
			}
		}
		return s_EmptyConnector;
	}

	protected virtual bool IsValid(Graphic graphic)
	{
		return true;
	}

	public virtual Shader FindShader(string shaderName)
	{
		return Shader.Find("Hidden/" + shaderName);
	}

	public virtual void OnEnable(Graphic graphic)
	{
	}

	public virtual void OnDisable(Graphic graphic)
	{
	}

	public virtual void SetVerticesDirty(Graphic graphic)
	{
		if ((bool)graphic)
		{
			graphic.SetVerticesDirty();
		}
	}

	public virtual void SetMaterialDirty(Graphic graphic)
	{
		if ((bool)graphic)
		{
			graphic.SetMaterialDirty();
		}
	}

	public virtual void GetPositionFactor(EffectArea area, int index, Rect rect, Vector2 position, out float x, out float y)
	{
		if (area == EffectArea.Fit)
		{
			x = Mathf.Clamp01((position.x - rect.xMin) / rect.width);
			y = Mathf.Clamp01((position.y - rect.yMin) / rect.height);
		}
		else
		{
			x = Mathf.Clamp01(position.x / rect.width + 0.5f);
			y = Mathf.Clamp01(position.y / rect.height + 0.5f);
		}
	}

	public virtual bool IsText(Graphic graphic)
	{
		if ((bool)graphic)
		{
			return graphic is Text;
		}
		return false;
	}

	public virtual void SetExtraChannel(ref UIVertex vertex, Vector2 value)
	{
		vertex.uv1 = value;
	}

	public virtual void GetNormalizedFactor(EffectArea area, int index, Matrix2x3 matrix, Vector2 position, out Vector2 normalizedPos)
	{
		normalizedPos = matrix * position;
	}
}
public class MaterialCache
{
	private class MaterialEntry
	{
		public Material material;

		public int referenceCount;

		public void Release()
		{
			if ((bool)material)
			{
				UnityEngine.Object.DestroyImmediate(material, allowDestroyingAssets: false);
			}
			material = null;
		}
	}

	private static Dictionary<Hash128, MaterialEntry> materialMap = new Dictionary<Hash128, MaterialEntry>();

	public static Material Register(Material baseMaterial, Hash128 hash, Action<Material, Graphic> onModifyMaterial, Graphic graphic)
	{
		if (!hash.isValid)
		{
			return null;
		}
		if (!materialMap.TryGetValue(hash, out var value))
		{
			value = new MaterialEntry
			{
				material = new Material(baseMaterial)
				{
					hideFlags = HideFlags.HideAndDontSave
				}
			};
			onModifyMaterial(value.material, graphic);
			materialMap.Add(hash, value);
		}
		value.referenceCount++;
		return value.material;
	}

	public static void Unregister(Hash128 hash)
	{
		if (hash.isValid && materialMap.TryGetValue(hash, out var value) && --value.referenceCount <= 0)
		{
			value.Release();
			materialMap.Remove(hash);
		}
	}
}
public struct Matrix2x3
{
	public float m00;

	public float m01;

	public float m02;

	public float m10;

	public float m11;

	public float m12;

	public Matrix2x3(Rect rect, float cos, float sin)
	{
		float num = (0f - rect.xMin) / rect.width - 0.5f;
		float num2 = (0f - rect.yMin) / rect.height - 0.5f;
		m00 = cos / rect.width;
		m01 = (0f - sin) / rect.height;
		m02 = num * cos - num2 * sin + 0.5f;
		m10 = sin / rect.width;
		m11 = cos / rect.height;
		m12 = num * sin + num2 * cos + 0.5f;
	}

	public static Vector2 operator *(Matrix2x3 m, Vector2 v)
	{
		return new Vector2(m.m00 * v.x + m.m01 * v.y + m.m02, m.m10 * v.x + m.m11 * v.y + m.m12);
	}
}
public interface IParameterTexture
{
	int parameterIndex { get; set; }

	ParameterTexture paramTex { get; }
}
[Serializable]
public class ParameterTexture
{
	private Texture2D _texture;

	private bool _needUpload;

	private int _propertyId;

	private readonly string _propertyName;

	private readonly int _channels;

	private readonly int _instanceLimit;

	private readonly byte[] _data;

	private readonly Stack<int> _stack;

	private static List<Action> updates;

	public ParameterTexture(int channels, int instanceLimit, string propertyName)
	{
		_propertyName = propertyName;
		_channels = ((channels - 1) / 4 + 1) * 4;
		_instanceLimit = ((instanceLimit - 1) / 2 + 1) * 2;
		_data = new byte[_channels * _instanceLimit];
		_stack = new Stack<int>(_instanceLimit);
		for (int i = 1; i < _instanceLimit + 1; i++)
		{
			_stack.Push(i);
		}
	}

	public void Register(IParameterTexture target)
	{
		Initialize();
		if (target.parameterIndex <= 0 && 0 < _stack.Count)
		{
			target.parameterIndex = _stack.Pop();
		}
	}

	public void Unregister(IParameterTexture target)
	{
		if (0 < target.parameterIndex)
		{
			_stack.Push(target.parameterIndex);
			target.parameterIndex = 0;
		}
	}

	public void SetData(IParameterTexture target, int channelId, byte value)
	{
		int num = (target.parameterIndex - 1) * _channels + channelId;
		if (0 < target.parameterIndex && _data[num] != value)
		{
			_data[num] = value;
			_needUpload = true;
		}
	}

	public void SetData(IParameterTexture target, int channelId, float value)
	{
		SetData(target, channelId, (byte)(Mathf.Clamp01(value) * 255f));
	}

	public void RegisterMaterial(Material mat)
	{
		if (_propertyId == 0)
		{
			_propertyId = Shader.PropertyToID(_propertyName);
		}
		if ((bool)mat)
		{
			mat.SetTexture(_propertyId, _texture);
		}
	}

	public float GetNormalizedIndex(IParameterTexture target)
	{
		return ((float)target.parameterIndex - 0.5f) / (float)_instanceLimit;
	}

	private void Initialize()
	{
		if (updates == null)
		{
			updates = new List<Action>();
			Canvas.willRenderCanvases += delegate
			{
				int count = updates.Count;
				for (int i = 0; i < count; i++)
				{
					updates[i]();
				}
			};
		}
		if (!_texture)
		{
			bool linear = QualitySettings.activeColorSpace == ColorSpace.Linear;
			_texture = new Texture2D(_channels / 4, _instanceLimit, TextureFormat.RGBA32, mipChain: false, linear);
			_texture.filterMode = FilterMode.Point;
			_texture.wrapMode = TextureWrapMode.Clamp;
			updates.Add(UpdateParameterTexture);
			_needUpload = true;
		}
	}

	private void UpdateParameterTexture()
	{
		if (_needUpload && (bool)_texture)
		{
			_needUpload = false;
			_texture.LoadRawTextureData(_data);
			_texture.Apply(updateMipmaps: false, makeNoLongerReadable: false);
		}
	}
}
public enum BlurMode
{
	None,
	FastBlur,
	MediumBlur,
	DetailBlur
}
public enum ColorMode
{
	Multiply,
	Fill,
	Add,
	Subtract
}
public enum EffectArea
{
	RectTransform,
	Fit,
	Character
}
public static class EffectAreaExtensions
{
	private static readonly Rect rectForCharacter = new Rect(0f, 0f, 1f, 1f);

	private static readonly Vector2[] splitedCharacterPosition = new Vector2[4]
	{
		Vector2.up,
		Vector2.one,
		Vector2.right,
		Vector2.zero
	};

	public static Rect GetEffectArea(this EffectArea area, VertexHelper vh, Rect rectangle, float aspectRatio = -1f)
	{
		Rect result = default(Rect);
		switch (area)
		{
		case EffectArea.RectTransform:
			result = rectangle;
			break;
		case EffectArea.Character:
			result = rectForCharacter;
			break;
		case EffectArea.Fit:
		{
			UIVertex vertex = default(UIVertex);
			float num = float.MaxValue;
			float num2 = float.MaxValue;
			float num3 = float.MinValue;
			float num4 = float.MinValue;
			for (int i = 0; i < vh.currentVertCount; i++)
			{
				vh.PopulateUIVertex(ref vertex, i);
				float x = vertex.position.x;
				float y = vertex.position.y;
				num = Mathf.Min(num, x);
				num2 = Mathf.Min(num2, y);
				num3 = Mathf.Max(num3, x);
				num4 = Mathf.Max(num4, y);
			}
			result.Set(num, num2, num3 - num, num4 - num2);
			break;
		}
		default:
			result = rectangle;
			break;
		}
		if (0f < aspectRatio)
		{
			if (result.width < result.height)
			{
				result.width = result.height * aspectRatio;
			}
			else
			{
				result.height = result.width / aspectRatio;
			}
		}
		return result;
	}

	public static void GetPositionFactor(this EffectArea area, int index, Rect rect, Vector2 position, bool isText, bool isTMPro, out float x, out float y)
	{
		if (isText && area == EffectArea.Character)
		{
			index = (isTMPro ? ((index + 3) % 4) : (index % 4));
			x = splitedCharacterPosition[index].x;
			y = splitedCharacterPosition[index].y;
		}
		else if (area == EffectArea.Fit)
		{
			x = Mathf.Clamp01((position.x - rect.xMin) / rect.width);
			y = Mathf.Clamp01((position.y - rect.yMin) / rect.height);
		}
		else
		{
			x = Mathf.Clamp01(position.x / rect.width + 0.5f);
			y = Mathf.Clamp01(position.y / rect.height + 0.5f);
		}
	}

	public static void GetNormalizedFactor(this EffectArea area, int index, Matrix2x3 matrix, Vector2 position, bool isText, out Vector2 nomalizedPos)
	{
		if (isText && area == EffectArea.Character)
		{
			nomalizedPos = matrix * splitedCharacterPosition[(index + 3) % 4];
		}
		else
		{
			nomalizedPos = matrix * position;
		}
	}
}
public enum EffectMode
{
	None,
	Grayscale,
	Sepia,
	Nega,
	Pixel
}
public enum ShadowStyle
{
	None,
	Shadow,
	Outline,
	Outline8,
	Shadow3
}
[AddComponentMenu("UI/UIEffects/UIDissolve", 3)]
public class UIDissolve : BaseMaterialEffect, IMaterialModifier
{
	private const uint k_ShaderId = 0u;

	private static readonly ParameterTexture s_ParamTex = new ParameterTexture(8, 128, "_ParamTex");

	private static readonly int k_TransitionTexId = Shader.PropertyToID("_TransitionTex");

	private bool _lastKeepAspectRatio;

	private EffectArea _lastEffectArea;

	private static Texture _defaultTransitionTexture;

	[Tooltip("Current location[0-1] for dissolve effect. 0 is not dissolved, 1 is completely dissolved.")]
	[FormerlySerializedAs("m_Location")]
	[SerializeField]
	[Range(0f, 1f)]
	private float m_EffectFactor = 0.5f;

	[Tooltip("Edge width.")]
	[SerializeField]
	[Range(0f, 1f)]
	private float m_Width = 0.5f;

	[Tooltip("Edge softness.")]
	[SerializeField]
	[Range(0f, 1f)]
	private float m_Softness = 0.5f;

	[Tooltip("Edge color.")]
	[SerializeField]
	[ColorUsage(false)]
	private Color m_Color = new Color(0f, 0.25f, 1f);

	[Tooltip("Edge color effect mode.")]
	[SerializeField]
	private ColorMode m_ColorMode = ColorMode.Add;

	[Tooltip("Noise texture for dissolving (single channel texture).")]
	[SerializeField]
	[FormerlySerializedAs("m_NoiseTexture")]
	private Texture m_TransitionTexture;

	[Header("Advanced Option")]
	[Tooltip("The area for effect.")]
	[SerializeField]
	protected EffectArea m_EffectArea;

	[Tooltip("Keep effect aspect ratio.")]
	[SerializeField]
	private bool m_KeepAspectRatio;

	public float effectFactor
	{
		get
		{
			return m_EffectFactor;
		}
		set
		{
			value = Mathf.Clamp(value, 0f, 1f);
			if (!Mathf.Approximately(m_EffectFactor, value))
			{
				m_EffectFactor = value;
				SetEffectParamsDirty();
			}
		}
	}

	public float width
	{
		get
		{
			return m_Width;
		}
		set
		{
			value = Mathf.Clamp(value, 0f, 1f);
			if (!Mathf.Approximately(m_Width, value))
			{
				m_Width = value;
				SetEffectParamsDirty();
			}
		}
	}

	public float softness
	{
		get
		{
			return m_Softness;
		}
		set
		{
			value = Mathf.Clamp(value, 0f, 1f);
			if (!Mathf.Approximately(m_Softness, value))
			{
				m_Softness = value;
				SetEffectParamsDirty();
			}
		}
	}

	public Color color
	{
		get
		{
			return m_Color;
		}
		set
		{
			if (!(m_Color == value))
			{
				m_Color = value;
				SetEffectParamsDirty();
			}
		}
	}

	public Texture transitionTexture
	{
		get
		{
			if (!m_TransitionTexture)
			{
				return defaultTransitionTexture;
			}
			return m_TransitionTexture;
		}
		set
		{
			if (!(m_TransitionTexture == value))
			{
				m_TransitionTexture = value;
				SetMaterialDirty();
			}
		}
	}

	private static Texture defaultTransitionTexture
	{
		get
		{
			if (!_defaultTransitionTexture)
			{
				return _defaultTransitionTexture = Resources.Load<Texture>("Default-Transition");
			}
			return _defaultTransitionTexture;
		}
	}

	public EffectArea effectArea
	{
		get
		{
			return m_EffectArea;
		}
		set
		{
			if (m_EffectArea != value)
			{
				m_EffectArea = value;
				SetVerticesDirty();
			}
		}
	}

	public bool keepAspectRatio
	{
		get
		{
			return m_KeepAspectRatio;
		}
		set
		{
			if (m_KeepAspectRatio != value)
			{
				m_KeepAspectRatio = value;
				SetVerticesDirty();
			}
		}
	}

	public ColorMode colorMode
	{
		get
		{
			return m_ColorMode;
		}
		set
		{
			if (m_ColorMode != value)
			{
				m_ColorMode = value;
				SetMaterialDirty();
			}
		}
	}

	public override ParameterTexture paramTex => s_ParamTex;

	public override Hash128 GetMaterialHash(Material material)
	{
		if (!base.isActiveAndEnabled || !material || !material.shader)
		{
			return BaseMaterialEffect.k_InvalidHash;
		}
		uint u32_ = (uint)((int)m_ColorMode << 6);
		uint instanceID = (uint)transitionTexture.GetInstanceID();
		return new Hash128((uint)material.GetInstanceID(), u32_, instanceID, 0u);
	}

	public override void ModifyMaterial(Material newMaterial, Graphic graphic)
	{
		GraphicConnector.FindConnector(graphic);
		newMaterial.shader = Shader.Find($"Hidden/{newMaterial.shader.name} (UIDissolve)");
		SetShaderVariants(newMaterial, m_ColorMode);
		newMaterial.SetTexture(k_TransitionTexId, transitionTexture);
		paramTex.RegisterMaterial(newMaterial);
	}

	public override void ModifyMesh(VertexHelper vh, Graphic graphic)
	{
		if (base.isActiveAndEnabled)
		{
			float normalizedIndex = paramTex.GetNormalizedIndex(this);
			Texture texture = transitionTexture;
			float aspectRatio = ((m_KeepAspectRatio && (bool)texture) ? ((float)texture.width / (float)texture.height) : (-1f));
			Rect rect = m_EffectArea.GetEffectArea(vh, base.rectTransform.rect, aspectRatio);
			UIVertex vertex = default(UIVertex);
			int currentVertCount = vh.currentVertCount;
			for (int i = 0; i < currentVertCount; i++)
			{
				vh.PopulateUIVertex(ref vertex, i);
				base.connector.GetPositionFactor(m_EffectArea, i, rect, vertex.position, out var x, out var y);
				vertex.uv0 = new Vector2(Packer.ToFloat(vertex.uv0.x, vertex.uv0.y), Packer.ToFloat(x, y, normalizedIndex));
				vh.SetUIVertex(vertex, i);
			}
		}
	}

	protected override void SetEffectParamsDirty()
	{
		paramTex.SetData(this, 0, m_EffectFactor);
		paramTex.SetData(this, 1, m_Width);
		paramTex.SetData(this, 2, m_Softness);
		paramTex.SetData(this, 4, m_Color.r);
		paramTex.SetData(this, 5, m_Color.g);
		paramTex.SetData(this, 6, m_Color.b);
	}

	protected override void SetVerticesDirty()
	{
		base.SetVerticesDirty();
		_lastKeepAspectRatio = m_KeepAspectRatio;
		_lastEffectArea = m_EffectArea;
	}

	protected override void OnDidApplyAnimationProperties()
	{
		base.OnDidApplyAnimationProperties();
		if (_lastKeepAspectRatio != m_KeepAspectRatio || _lastEffectArea != m_EffectArea)
		{
			SetVerticesDirty();
		}
	}
}
[ExecuteAlways]
[RequireComponent(typeof(Graphic))]
[DisallowMultipleComponent]
[AddComponentMenu("UI/UIEffects/UIEffect", 1)]
public class UIEffect : BaseMaterialEffect, IMaterialModifier
{
	private enum BlurEx
	{
		None,
		Ex
	}

	private const uint k_ShaderId = 16u;

	private static readonly ParameterTexture s_ParamTex = new ParameterTexture(4, 1024, "_ParamTex");

	[FormerlySerializedAs("m_ToneLevel")]
	[Tooltip("Effect factor between 0(no effect) and 1(complete effect).")]
	[SerializeField]
	[Range(0f, 1f)]
	private float m_EffectFactor = 1f;

	[Tooltip("Color effect factor between 0(no effect) and 1(complete effect).")]
	[SerializeField]
	[Range(0f, 1f)]
	private float m_ColorFactor = 1f;

	[FormerlySerializedAs("m_Blur")]
	[Tooltip("How far is the blurring from the graphic.")]
	[SerializeField]
	[Range(0f, 1f)]
	private float m_BlurFactor = 1f;

	[FormerlySerializedAs("m_ToneMode")]
	[Tooltip("Effect mode")]
	[SerializeField]
	private EffectMode m_EffectMode;

	[Tooltip("Color effect mode")]
	[SerializeField]
	private ColorMode m_ColorMode;

	[Tooltip("Blur effect mode")]
	[SerializeField]
	private BlurMode m_BlurMode;

	[Tooltip("Advanced blurring remove common artifacts in the blur effect for uGUI.")]
	[SerializeField]
	private bool m_AdvancedBlur;

	public AdditionalCanvasShaderChannels uvMaskChannel => base.connector.extraChannel;

	public float effectFactor
	{
		get
		{
			return m_EffectFactor;
		}
		set
		{
			value = Mathf.Clamp(value, 0f, 1f);
			if (!Mathf.Approximately(m_EffectFactor, value))
			{
				m_EffectFactor = value;
				SetEffectParamsDirty();
			}
		}
	}

	public float colorFactor
	{
		get
		{
			return m_ColorFactor;
		}
		set
		{
			value = Mathf.Clamp(value, 0f, 1f);
			if (!Mathf.Approximately(m_ColorFactor, value))
			{
				m_ColorFactor = value;
				SetEffectParamsDirty();
			}
		}
	}

	public float blurFactor
	{
		get
		{
			return m_BlurFactor;
		}
		set
		{
			value = Mathf.Clamp(value, 0f, 1f);
			if (!Mathf.Approximately(m_BlurFactor, value))
			{
				m_BlurFactor = value;
				SetEffectParamsDirty();
			}
		}
	}

	public EffectMode effectMode
	{
		get
		{
			return m_EffectMode;
		}
		set
		{
			if (m_EffectMode != value)
			{
				m_EffectMode = value;
				SetMaterialDirty();
			}
		}
	}

	public ColorMode colorMode
	{
		get
		{
			return m_ColorMode;
		}
		set
		{
			if (m_ColorMode != value)
			{
				m_ColorMode = value;
				SetMaterialDirty();
			}
		}
	}

	public BlurMode blurMode
	{
		get
		{
			return m_BlurMode;
		}
		set
		{
			if (m_BlurMode != value)
			{
				m_BlurMode = value;
				SetMaterialDirty();
			}
		}
	}

	public override ParameterTexture paramTex => s_ParamTex;

	public bool advancedBlur
	{
		get
		{
			return m_AdvancedBlur;
		}
		set
		{
			if (m_AdvancedBlur != value)
			{
				m_AdvancedBlur = value;
				SetVerticesDirty();
				SetMaterialDirty();
			}
		}
	}

	public override Hash128 GetMaterialHash(Material material)
	{
		if (!base.isActiveAndEnabled || !material || !material.shader)
		{
			return BaseMaterialEffect.k_InvalidHash;
		}
		uint num = (uint)(((int)m_EffectMode << 6) + ((int)m_ColorMode << 9) + ((int)m_BlurMode << 11) + ((m_AdvancedBlur ? 1 : 0) << 13));
		return new Hash128((uint)material.GetInstanceID(), 16 + num, 0u, 0u);
	}

	public override void ModifyMaterial(Material newMaterial, Graphic graphic)
	{
		GraphicConnector.FindConnector(graphic);
		newMaterial.shader = Shader.Find($"Hidden/{newMaterial.shader.name} (UIEffect)");
		SetShaderVariants(newMaterial, m_EffectMode, m_ColorMode, m_BlurMode, m_AdvancedBlur ? BlurEx.Ex : BlurEx.None);
		paramTex.RegisterMaterial(newMaterial);
	}

	public override void ModifyMesh(VertexHelper vh, Graphic graphic)
	{
		if (!base.isActiveAndEnabled)
		{
			return;
		}
		float normalizedIndex = paramTex.GetNormalizedIndex(this);
		if (m_BlurMode != BlurMode.None && advancedBlur)
		{
			vh.GetUIVertexStream(BaseMaterialEffect.s_TempVerts);
			vh.Clear();
			int count = BaseMaterialEffect.s_TempVerts.Count;
			int num = (base.connector.IsText(graphic) ? 6 : count);
			Rect posBounds = default(Rect);
			Rect uvBounds = default(Rect);
			Vector3 a = default(Vector3);
			Vector3 vector = default(Vector3);
			Vector3 vector2 = default(Vector3);
			float num2 = (float)blurMode * 6f * 2f;
			for (int i = 0; i < count; i += num)
			{
				GetBounds(BaseMaterialEffect.s_TempVerts, i, num, ref posBounds, ref uvBounds, global: true);
				Vector2 value = new Vector2(Packer.ToFloat(uvBounds.xMin, uvBounds.yMin), Packer.ToFloat(uvBounds.xMax, uvBounds.yMax));
				for (int j = 0; j < num; j += 6)
				{
					Vector3 position = BaseMaterialEffect.s_TempVerts[i + j + 1].position;
					Vector3 position2 = BaseMaterialEffect.s_TempVerts[i + j + 4].position;
					bool flag = num == 6 || !posBounds.Contains(position) || !posBounds.Contains(position2);
					if (flag)
					{
						Vector3 vector3 = BaseMaterialEffect.s_TempVerts[i + j + 1].uv0;
						Vector3 vector4 = BaseMaterialEffect.s_TempVerts[i + j + 4].uv0;
						Vector3 vector5 = (position + position2) / 2f;
						Vector3 vector6 = (vector3 + vector4) / 2f;
						a = position - position2;
						a.x = 1f + num2 / Mathf.Abs(a.x);
						a.y = 1f + num2 / Mathf.Abs(a.y);
						a.z = 1f + num2 / Mathf.Abs(a.z);
						vector = vector5 - Vector3.Scale(a, vector5);
						vector2 = vector6 - Vector3.Scale(a, vector6);
					}
					for (int k = 0; k < 6; k++)
					{
						UIVertex vertex = BaseMaterialEffect.s_TempVerts[i + j + k];
						Vector3 position3 = vertex.position;
						Vector2 vector7 = vertex.uv0;
						if (flag && (position3.x < posBounds.xMin || posBounds.xMax < position3.x))
						{
							position3.x = position3.x * a.x + vector.x;
							vector7.x = vector7.x * a.x + vector2.x;
						}
						if (flag && (position3.y < posBounds.yMin || posBounds.yMax < position3.y))
						{
							position3.y = position3.y * a.y + vector.y;
							vector7.y = vector7.y * a.y + vector2.y;
						}
						vertex.uv0 = new Vector2(Packer.ToFloat((vector7.x + 0.5f) / 2f, (vector7.y + 0.5f) / 2f), normalizedIndex);
						vertex.position = position3;
						base.connector.SetExtraChannel(ref vertex, value);
						BaseMaterialEffect.s_TempVerts[i + j + k] = vertex;
					}
				}
			}
			vh.AddUIVertexTriangleStream(BaseMaterialEffect.s_TempVerts);
			BaseMaterialEffect.s_TempVerts.Clear();
		}
		else
		{
			int currentVertCount = vh.currentVertCount;
			UIVertex vertex2 = default(UIVertex);
			for (int l = 0; l < currentVertCount; l++)
			{
				vh.PopulateUIVertex(ref vertex2, l);
				Vector2 vector8 = vertex2.uv0;
				vertex2.uv0 = new Vector2(Packer.ToFloat((vector8.x + 0.5f) / 2f, (vector8.y + 0.5f) / 2f), normalizedIndex);
				vh.SetUIVertex(vertex2, l);
			}
		}
	}

	protected override void SetEffectParamsDirty()
	{
		paramTex.SetData(this, 0, m_EffectFactor);
		paramTex.SetData(this, 1, m_ColorFactor);
		paramTex.SetData(this, 2, m_BlurFactor);
	}

	private static void GetBounds(List<UIVertex> verts, int start, int count, ref Rect posBounds, ref Rect uvBounds, bool global)
	{
		Vector2 vector = new Vector2(float.MaxValue, float.MaxValue);
		Vector2 vector2 = new Vector2(float.MinValue, float.MinValue);
		Vector2 vector3 = new Vector2(float.MaxValue, float.MaxValue);
		Vector2 vector4 = new Vector2(float.MinValue, float.MinValue);
		for (int i = start; i < start + count; i++)
		{
			UIVertex uIVertex = verts[i];
			Vector2 vector5 = uIVertex.uv0;
			Vector3 position = uIVertex.position;
			if (vector.x >= position.x && vector.y >= position.y)
			{
				vector = position;
			}
			else if (vector2.x <= position.x && vector2.y <= position.y)
			{
				vector2 = position;
			}
			if (vector3.x >= vector5.x && vector3.y >= vector5.y)
			{
				vector3 = vector5;
			}
			else if (vector4.x <= vector5.x && vector4.y <= vector5.y)
			{
				vector4 = vector5;
			}
		}
		posBounds.Set(vector.x + 0.001f, vector.y + 0.001f, vector2.x - vector.x - 0.002f, vector2.y - vector.y - 0.002f);
		uvBounds.Set(vector3.x, vector3.y, vector4.x - vector3.x, vector4.y - vector3.y);
	}
}
[DisallowMultipleComponent]
[AddComponentMenu("UI/UIEffects/UIFlip", 102)]
public class UIFlip : BaseMeshEffect
{
	[Tooltip("Flip horizontally.")]
	[SerializeField]
	private bool m_Horizontal;

	[Tooltip("Flip vertically.")]
	[SerializeField]
	private bool m_Veritical;

	public bool horizontal
	{
		get
		{
			return m_Horizontal;
		}
		set
		{
			if (m_Horizontal != value)
			{
				m_Horizontal = value;
				SetEffectParamsDirty();
			}
		}
	}

	public bool vertical
	{
		get
		{
			return m_Veritical;
		}
		set
		{
			if (m_Veritical != value)
			{
				m_Veritical = value;
				SetEffectParamsDirty();
			}
		}
	}

	public override void ModifyMesh(VertexHelper vh, Graphic graphic)
	{
		if (base.isActiveAndEnabled)
		{
			UIVertex vertex = default(UIVertex);
			for (int i = 0; i < vh.currentVertCount; i++)
			{
				vh.PopulateUIVertex(ref vertex, i);
				Vector3 position = vertex.position;
				vertex.position = new Vector3(m_Horizontal ? (0f - position.x) : position.x, m_Veritical ? (0f - position.y) : position.y);
				vh.SetUIVertex(vertex, i);
			}
		}
	}
}
[DisallowMultipleComponent]
[AddComponentMenu("UI/UIEffects/UIGradient", 101)]
public class UIGradient : BaseMeshEffect
{
	public enum Direction
	{
		Horizontal,
		Vertical,
		Angle,
		Diagonal
	}

	public enum GradientStyle
	{
		Rect,
		Fit,
		Split
	}

	private static readonly Vector2[] s_SplitedCharacterPosition = new Vector2[4]
	{
		Vector2.up,
		Vector2.one,
		Vector2.right,
		Vector2.zero
	};

	[Tooltip("Gradient Direction.")]
	[SerializeField]
	private Direction m_Direction;

	[Tooltip("Color1: Top or Left.")]
	[SerializeField]
	private Color m_Color1 = Color.white;

	[Tooltip("Color2: Bottom or Right.")]
	[SerializeField]
	private Color m_Color2 = Color.white;

	[Tooltip("Color3: For diagonal.")]
	[SerializeField]
	private Color m_Color3 = Color.white;

	[Tooltip("Color4: For diagonal.")]
	[SerializeField]
	private Color m_Color4 = Color.white;

	[Tooltip("Gradient rotation.")]
	[SerializeField]
	[Range(-180f, 180f)]
	private float m_Rotation;

	[Tooltip("Gradient offset for Horizontal, Vertical or Angle.")]
	[SerializeField]
	[Range(-1f, 1f)]
	private float m_Offset1;

	[Tooltip("Gradient offset for Diagonal.")]
	[SerializeField]
	[Range(-1f, 1f)]
	private float m_Offset2;

	[Tooltip("Gradient style for Text.")]
	[SerializeField]
	private GradientStyle m_GradientStyle;

	[Tooltip("Color space to correct color.")]
	[SerializeField]
	private ColorSpace m_ColorSpace = ColorSpace.Uninitialized;

	[Tooltip("Ignore aspect ratio.")]
	[SerializeField]
	private bool m_IgnoreAspectRatio = true;

	public Direction direction
	{
		get
		{
			return m_Direction;
		}
		set
		{
			if (m_Direction != value)
			{
				m_Direction = value;
				SetVerticesDirty();
			}
		}
	}

	public Color color1
	{
		get
		{
			return m_Color1;
		}
		set
		{
			if (!(m_Color1 == value))
			{
				m_Color1 = value;
				SetVerticesDirty();
			}
		}
	}

	public Color color2
	{
		get
		{
			return m_Color2;
		}
		set
		{
			if (!(m_Color2 == value))
			{
				m_Color2 = value;
				SetVerticesDirty();
			}
		}
	}

	public Color color3
	{
		get
		{
			return m_Color3;
		}
		set
		{
			if (!(m_Color3 == value))
			{
				m_Color3 = value;
				SetVerticesDirty();
			}
		}
	}

	public Color color4
	{
		get
		{
			return m_Color4;
		}
		set
		{
			if (!(m_Color4 == value))
			{
				m_Color4 = value;
				SetVerticesDirty();
			}
		}
	}

	public float rotation
	{
		get
		{
			if (m_Direction != Direction.Horizontal)
			{
				if (m_Direction != Direction.Vertical)
				{
					return m_Rotation;
				}
				return 0f;
			}
			return -90f;
		}
		set
		{
			if (!Mathf.Approximately(m_Rotation, value))
			{
				m_Rotation = value;
				SetVerticesDirty();
			}
		}
	}

	public float offset
	{
		get
		{
			return m_Offset1;
		}
		set
		{
			if (!Mathf.Approximately(m_Offset1, value))
			{
				m_Offset1 = value;
				SetVerticesDirty();
			}
		}
	}

	public Vector2 offset2
	{
		get
		{
			return new Vector2(m_Offset2, m_Offset1);
		}
		set
		{
			if (!Mathf.Approximately(m_Offset1, value.y) || !Mathf.Approximately(m_Offset2, value.x))
			{
				m_Offset1 = value.y;
				m_Offset2 = value.x;
				SetVerticesDirty();
			}
		}
	}

	public GradientStyle gradientStyle
	{
		get
		{
			return m_GradientStyle;
		}
		set
		{
			if (m_GradientStyle != value)
			{
				m_GradientStyle = value;
				SetVerticesDirty();
			}
		}
	}

	public ColorSpace colorSpace
	{
		get
		{
			return m_ColorSpace;
		}
		set
		{
			if (m_ColorSpace != value)
			{
				m_ColorSpace = value;
				SetVerticesDirty();
			}
		}
	}

	public bool ignoreAspectRatio
	{
		get
		{
			return m_IgnoreAspectRatio;
		}
		set
		{
			if (m_IgnoreAspectRatio != value)
			{
				m_IgnoreAspectRatio = value;
				SetVerticesDirty();
			}
		}
	}

	public override void ModifyMesh(VertexHelper vh, Graphic graphic)
	{
		if (!base.isActiveAndEnabled)
		{
			return;
		}
		Rect rect = default(Rect);
		UIVertex vertex = default(UIVertex);
		switch (m_GradientStyle)
		{
		case GradientStyle.Rect:
			rect = graphic.rectTransform.rect;
			break;
		case GradientStyle.Split:
			rect.Set(0f, 0f, 1f, 1f);
			break;
		case GradientStyle.Fit:
		{
			float xMin = (rect.yMin = float.MaxValue);
			rect.xMin = xMin;
			xMin = (rect.yMax = float.MinValue);
			rect.xMax = xMin;
			for (int i = 0; i < vh.currentVertCount; i++)
			{
				vh.PopulateUIVertex(ref vertex, i);
				rect.xMin = Mathf.Min(rect.xMin, vertex.position.x);
				rect.yMin = Mathf.Min(rect.yMin, vertex.position.y);
				rect.xMax = Mathf.Max(rect.xMax, vertex.position.x);
				rect.yMax = Mathf.Max(rect.yMax, vertex.position.y);
			}
			break;
		}
		}
		float f = rotation * (MathF.PI / 180f);
		Vector2 vector = new Vector2(Mathf.Cos(f), Mathf.Sin(f));
		if (!m_IgnoreAspectRatio && Direction.Angle <= m_Direction)
		{
			vector.x *= rect.height / rect.width;
			vector = vector.normalized;
		}
		Matrix2x3 matrix2x = new Matrix2x3(rect, vector.x, vector.y);
		for (int j = 0; j < vh.currentVertCount; j++)
		{
			vh.PopulateUIVertex(ref vertex, j);
			Vector2 vector2 = ((m_GradientStyle != GradientStyle.Split) ? (matrix2x * vertex.position + offset2) : (matrix2x * s_SplitedCharacterPosition[j % 4] + offset2));
			Color color = ((direction != Direction.Diagonal) ? Color.LerpUnclamped(m_Color2, m_Color1, vector2.y) : Color.LerpUnclamped(Color.LerpUnclamped(m_Color1, m_Color2, vector2.x), Color.LerpUnclamped(m_Color3, m_Color4, vector2.x), vector2.y));
			ref Color32 color2 = ref vertex.color;
			color2 *= ((m_ColorSpace == ColorSpace.Gamma) ? color.gamma : ((m_ColorSpace == ColorSpace.Linear) ? color.linear : color));
			vh.SetUIVertex(vertex, j);
		}
	}
}
[AddComponentMenu("UI/UIEffects/UIHsvModifier", 4)]
public class UIHsvModifier : BaseMaterialEffect
{
	private const uint k_ShaderId = 48u;

	private static readonly ParameterTexture s_ParamTex = new ParameterTexture(7, 128, "_ParamTex");

	[Header("Target")]
	[Tooltip("Target color to affect hsv shift.")]
	[SerializeField]
	[ColorUsage(false)]
	private Color m_TargetColor = Color.red;

	[Tooltip("Color range to affect hsv shift [0 ~ 1].")]
	[SerializeField]
	[Range(0f, 1f)]
	private float m_Range = 0.1f;

	[Header("Adjustment")]
	[Tooltip("Hue shift [-0.5 ~ 0.5].")]
	[SerializeField]
	[Range(-0.5f, 0.5f)]
	private float m_Hue;

	[Tooltip("Saturation shift [-0.5 ~ 0.5].")]
	[SerializeField]
	[Range(-0.5f, 0.5f)]
	private float m_Saturation;

	[Tooltip("Value shift [-0.5 ~ 0.5].")]
	[SerializeField]
	[Range(-0.5f, 0.5f)]
	private float m_Value;

	public Color targetColor
	{
		get
		{
			return m_TargetColor;
		}
		set
		{
			if (!(m_TargetColor == value))
			{
				m_TargetColor = value;
				SetEffectParamsDirty();
			}
		}
	}

	public float range
	{
		get
		{
			return m_Range;
		}
		set
		{
			value = Mathf.Clamp(value, 0f, 1f);
			if (!Mathf.Approximately(m_Range, value))
			{
				m_Range = value;
				SetEffectParamsDirty();
			}
		}
	}

	public float saturation
	{
		get
		{
			return m_Saturation;
		}
		set
		{
			value = Mathf.Clamp(value, -0.5f, 0.5f);
			if (!Mathf.Approximately(m_Saturation, value))
			{
				m_Saturation = value;
				SetEffectParamsDirty();
			}
		}
	}

	public float value
	{
		get
		{
			return m_Value;
		}
		set
		{
			value = Mathf.Clamp(value, -0.5f, 0.5f);
			if (!Mathf.Approximately(m_Value, value))
			{
				m_Value = value;
				SetEffectParamsDirty();
			}
		}
	}

	public float hue
	{
		get
		{
			return m_Hue;
		}
		set
		{
			value = Mathf.Clamp(value, -0.5f, 0.5f);
			if (!Mathf.Approximately(m_Hue, value))
			{
				m_Hue = value;
				SetEffectParamsDirty();
			}
		}
	}

	public override ParameterTexture paramTex => s_ParamTex;

	public override Hash128 GetMaterialHash(Material material)
	{
		if (!base.isActiveAndEnabled || !material || !material.shader)
		{
			return BaseMaterialEffect.k_InvalidHash;
		}
		return new Hash128((uint)material.GetInstanceID(), 48u, 0u, 0u);
	}

	public override void ModifyMaterial(Material newMaterial, Graphic graphic)
	{
		GraphicConnector.FindConnector(graphic);
		newMaterial.shader = Shader.Find($"Hidden/{newMaterial.shader.name} (UIHsvModifier)");
		paramTex.RegisterMaterial(newMaterial);
	}

	public override void ModifyMesh(VertexHelper vh, Graphic graphic)
	{
		if (base.isActiveAndEnabled)
		{
			float normalizedIndex = paramTex.GetNormalizedIndex(this);
			UIVertex vertex = default(UIVertex);
			int currentVertCount = vh.currentVertCount;
			for (int i = 0; i < currentVertCount; i++)
			{
				vh.PopulateUIVertex(ref vertex, i);
				vertex.uv0 = new Vector2(Packer.ToFloat(vertex.uv0.x, vertex.uv0.y), normalizedIndex);
				vh.SetUIVertex(vertex, i);
			}
		}
	}

	protected override void SetEffectParamsDirty()
	{
		Color.RGBToHSV(m_TargetColor, out var H, out var S, out var V);
		paramTex.SetData(this, 0, H);
		paramTex.SetData(this, 1, S);
		paramTex.SetData(this, 2, V);
		paramTex.SetData(this, 3, m_Range);
		paramTex.SetData(this, 4, m_Hue + 0.5f);
		paramTex.SetData(this, 5, m_Saturation + 0.5f);
		paramTex.SetData(this, 6, m_Value + 0.5f);
	}
}
[RequireComponent(typeof(Graphic))]
[AddComponentMenu("UI/UIEffects/UIShadow", 100)]
public class UIShadow : BaseMeshEffect, IParameterTexture
{
	private static readonly List<UIShadow> tmpShadows = new List<UIShadow>();

	private static readonly List<UIVertex> s_Verts = new List<UIVertex>(4096);

	private int _graphicVertexCount;

	private UIEffect _uiEffect;

	[Tooltip("How far is the blurring shadow from the graphic.")]
	[FormerlySerializedAs("m_Blur")]
	[SerializeField]
	[Range(0f, 1f)]
	private float m_BlurFactor = 1f;

	[Tooltip("Shadow effect style.")]
	[SerializeField]
	private ShadowStyle m_Style = ShadowStyle.Shadow;

	[SerializeField]
	private Color m_EffectColor = new Color(0f, 0f, 0f, 0.5f);

	[SerializeField]
	private Vector2 m_EffectDistance = new Vector2(1f, -1f);

	[SerializeField]
	private bool m_UseGraphicAlpha = true;

	private const float kMaxEffectDistance = 600f;

	public Color effectColor
	{
		get
		{
			return m_EffectColor;
		}
		set
		{
			if (!(m_EffectColor == value))
			{
				m_EffectColor = value;
				SetVerticesDirty();
			}
		}
	}

	public Vector2 effectDistance
	{
		get
		{
			return m_EffectDistance;
		}
		set
		{
			if (value.x > 600f)
			{
				value.x = 600f;
			}
			if (value.x < -600f)
			{
				value.x = -600f;
			}
			if (value.y > 600f)
			{
				value.y = 600f;
			}
			if (value.y < -600f)
			{
				value.y = -600f;
			}
			if (!(m_EffectDistance == value))
			{
				m_EffectDistance = value;
				SetEffectParamsDirty();
			}
		}
	}

	public bool useGraphicAlpha
	{
		get
		{
			return m_UseGraphicAlpha;
		}
		set
		{
			if (m_UseGraphicAlpha != value)
			{
				m_UseGraphicAlpha = value;
				SetEffectParamsDirty();
			}
		}
	}

	public float blurFactor
	{
		get
		{
			return m_BlurFactor;
		}
		set
		{
			value = Mathf.Clamp(value, 0f, 2f);
			if (!Mathf.Approximately(m_BlurFactor, value))
			{
				m_BlurFactor = value;
				SetEffectParamsDirty();
			}
		}
	}

	public ShadowStyle style
	{
		get
		{
			return m_Style;
		}
		set
		{
			if (m_Style != value)
			{
				m_Style = value;
				SetEffectParamsDirty();
			}
		}
	}

	public int parameterIndex { get; set; }

	public ParameterTexture paramTex { get; private set; }

	protected override void OnEnable()
	{
		base.OnEnable();
		_uiEffect = GetComponent<UIEffect>();
		if ((bool)_uiEffect)
		{
			paramTex = _uiEffect.paramTex;
			paramTex.Register(this);
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		_uiEffect = null;
		if (paramTex != null)
		{
			paramTex.Unregister(this);
			paramTex = null;
		}
	}

	public override void ModifyMesh(VertexHelper vh, Graphic graphic)
	{
		if (!base.isActiveAndEnabled || vh.currentVertCount <= 0 || m_Style == ShadowStyle.None)
		{
			return;
		}
		vh.GetUIVertexStream(s_Verts);
		GetComponents(tmpShadows);
		foreach (UIShadow tmpShadow in tmpShadows)
		{
			if (!tmpShadow.isActiveAndEnabled)
			{
				continue;
			}
			if (!(tmpShadow == this))
			{
				break;
			}
			foreach (UIShadow tmpShadow2 in tmpShadows)
			{
				tmpShadow2._graphicVertexCount = s_Verts.Count;
			}
			break;
		}
		tmpShadows.Clear();
		_uiEffect = (_uiEffect ? _uiEffect : GetComponent<UIEffect>());
		int start = s_Verts.Count - _graphicVertexCount;
		int end = s_Verts.Count;
		if (paramTex != null && (bool)_uiEffect && _uiEffect.isActiveAndEnabled)
		{
			paramTex.SetData(this, 0, _uiEffect.effectFactor);
			paramTex.SetData(this, 1, byte.MaxValue);
			paramTex.SetData(this, 2, m_BlurFactor);
		}
		ApplyShadow(s_Verts, effectColor, ref start, ref end, effectDistance, style, useGraphicAlpha);
		vh.Clear();
		vh.AddUIVertexTriangleStream(s_Verts);
		s_Verts.Clear();
	}

	private void ApplyShadow(List<UIVertex> verts, Color color, ref int start, ref int end, Vector2 distance, ShadowStyle style, bool alpha)
	{
		if (style != ShadowStyle.None && !(color.a <= 0f))
		{
			float x = distance.x;
			float y = distance.y;
			ApplyShadowZeroAlloc(verts, color, ref start, ref end, x, y, alpha);
			switch (style)
			{
			case ShadowStyle.Shadow3:
				ApplyShadowZeroAlloc(verts, color, ref start, ref end, x, 0f, alpha);
				ApplyShadowZeroAlloc(verts, color, ref start, ref end, 0f, y, alpha);
				break;
			case ShadowStyle.Outline:
				ApplyShadowZeroAlloc(verts, color, ref start, ref end, x, 0f - y, alpha);
				ApplyShadowZeroAlloc(verts, color, ref start, ref end, 0f - x, y, alpha);
				ApplyShadowZeroAlloc(verts, color, ref start, ref end, 0f - x, 0f - y, alpha);
				break;
			case ShadowStyle.Outline8:
				ApplyShadowZeroAlloc(verts, color, ref start, ref end, x, 0f - y, alpha);
				ApplyShadowZeroAlloc(verts, color, ref start, ref end, 0f - x, y, alpha);
				ApplyShadowZeroAlloc(verts, color, ref start, ref end, 0f - x, 0f - y, alpha);
				ApplyShadowZeroAlloc(verts, color, ref start, ref end, 0f - x, 0f, alpha);
				ApplyShadowZeroAlloc(verts, color, ref start, ref end, 0f, 0f - y, alpha);
				ApplyShadowZeroAlloc(verts, color, ref start, ref end, x, 0f, alpha);
				ApplyShadowZeroAlloc(verts, color, ref start, ref end, 0f, y, alpha);
				break;
			}
		}
	}

	private void ApplyShadowZeroAlloc(List<UIVertex> verts, Color color, ref int start, ref int end, float x, float y, bool alpha)
	{
		int num = end - start;
		int num2 = verts.Count + num;
		if (verts.Capacity < num2)
		{
			verts.Capacity *= 2;
		}
		float num3 = ((paramTex != null && (bool)_uiEffect && _uiEffect.isActiveAndEnabled) ? paramTex.GetNormalizedIndex(this) : (-1f));
		UIVertex item = default(UIVertex);
		for (int i = 0; i < num; i++)
		{
			verts.Add(item);
		}
		int num4 = verts.Count - 1;
		while (num <= num4)
		{
			verts[num4] = verts[num4 - num];
			num4--;
		}
		for (int j = 0; j < num; j++)
		{
			item = verts[j + start + num];
			Vector3 position = item.position;
			item.position.Set(position.x + x, position.y + y, position.z);
			Color color2 = effectColor;
			color2.a = (alpha ? (color.a * (float)(int)item.color.a / 255f) : color.a);
			item.color = color2;
			if (0f <= num3)
			{
				item.uv0 = new Vector2(item.uv0.x, num3);
			}
			verts[j] = item;
		}
		start = end;
		end = verts.Count;
	}
}
[AddComponentMenu("UI/UIEffects/UIShiny", 2)]
public class UIShiny : BaseMaterialEffect
{
	private const uint k_ShaderId = 8u;

	private static readonly ParameterTexture s_ParamTex = new ParameterTexture(8, 128, "_ParamTex");

	private float _lastRotation;

	private EffectArea _lastEffectArea;

	[Tooltip("Location for shiny effect.")]
	[FormerlySerializedAs("m_Location")]
	[SerializeField]
	[Range(0f, 1f)]
	private float m_EffectFactor = 0.5f;

	[Tooltip("Width for shiny effect.")]
	[SerializeField]
	[Range(0f, 1f)]
	private float m_Width = 0.25f;

	[Tooltip("Rotation for shiny effect.")]
	[SerializeField]
	[Range(-180f, 180f)]
	private float m_Rotation = 135f;

	[Tooltip("Softness for shiny effect.")]
	[SerializeField]
	[Range(0.01f, 1f)]
	private float m_Softness = 1f;

	[Tooltip("Brightness for shiny effect.")]
	[FormerlySerializedAs("m_Alpha")]
	[SerializeField]
	[Range(0f, 1f)]
	private float m_Brightness = 1f;

	[Tooltip("Gloss factor for shiny effect.")]
	[FormerlySerializedAs("m_Highlight")]
	[SerializeField]
	[Range(0f, 1f)]
	private float m_Gloss = 1f;

	[Header("Advanced Option")]
	[Tooltip("The area for effect.")]
	[SerializeField]
	protected EffectArea m_EffectArea;

	public float effectFactor
	{
		get
		{
			return m_EffectFactor;
		}
		set
		{
			value = Mathf.Clamp(value, 0f, 1f);
			if (!Mathf.Approximately(m_EffectFactor, value))
			{
				m_EffectFactor = value;
				SetEffectParamsDirty();
			}
		}
	}

	public float width
	{
		get
		{
			return m_Width;
		}
		set
		{
			value = Mathf.Clamp(value, 0f, 1f);
			if (!Mathf.Approximately(m_Width, value))
			{
				m_Width = value;
				SetEffectParamsDirty();
			}
		}
	}

	public float softness
	{
		get
		{
			return m_Softness;
		}
		set
		{
			value = Mathf.Clamp(value, 0.01f, 1f);
			if (!Mathf.Approximately(m_Softness, value))
			{
				m_Softness = value;
				SetEffectParamsDirty();
			}
		}
	}

	public float brightness
	{
		get
		{
			return m_Brightness;
		}
		set
		{
			value = Mathf.Clamp(value, 0f, 1f);
			if (!Mathf.Approximately(m_Brightness, value))
			{
				m_Brightness = value;
				SetEffectParamsDirty();
			}
		}
	}

	public float gloss
	{
		get
		{
			return m_Gloss;
		}
		set
		{
			value = Mathf.Clamp(value, 0f, 1f);
			if (!Mathf.Approximately(m_Gloss, value))
			{
				m_Gloss = value;
				SetEffectParamsDirty();
			}
		}
	}

	public float rotation
	{
		get
		{
			return m_Rotation;
		}
		set
		{
			if (!Mathf.Approximately(m_Rotation, value))
			{
				m_Rotation = value;
				SetVerticesDirty();
			}
		}
	}

	public EffectArea effectArea
	{
		get
		{
			return m_EffectArea;
		}
		set
		{
			if (m_EffectArea != value)
			{
				m_EffectArea = value;
				SetVerticesDirty();
			}
		}
	}

	public override ParameterTexture paramTex => s_ParamTex;

	public override Hash128 GetMaterialHash(Material material)
	{
		if (!base.isActiveAndEnabled || !material || !material.shader)
		{
			return BaseMaterialEffect.k_InvalidHash;
		}
		return new Hash128((uint)material.GetInstanceID(), 8u, 0u, 0u);
	}

	public override void ModifyMaterial(Material newMaterial, Graphic graphic)
	{
		GraphicConnector.FindConnector(graphic);
		newMaterial.shader = Shader.Find($"Hidden/{newMaterial.shader.name} (UIShiny)");
		paramTex.RegisterMaterial(newMaterial);
	}

	public override void ModifyMesh(VertexHelper vh, Graphic graphic)
	{
		if (base.isActiveAndEnabled)
		{
			float normalizedIndex = paramTex.GetNormalizedIndex(this);
			Rect rect = m_EffectArea.GetEffectArea(vh, base.rectTransform.rect);
			float f = m_Rotation * (MathF.PI / 180f);
			Vector2 vector = new Vector2(Mathf.Cos(f), Mathf.Sin(f));
			vector.x *= rect.height / rect.width;
			vector = vector.normalized;
			UIVertex vertex = default(UIVertex);
			Matrix2x3 matrix = new Matrix2x3(rect, vector.x, vector.y);
			for (int i = 0; i < vh.currentVertCount; i++)
			{
				vh.PopulateUIVertex(ref vertex, i);
				base.connector.GetNormalizedFactor(m_EffectArea, i, matrix, vertex.position, out var normalizedPos);
				vertex.uv0 = new Vector2(Packer.ToFloat(vertex.uv0.x, vertex.uv0.y), Packer.ToFloat(normalizedPos.y, normalizedIndex));
				vh.SetUIVertex(vertex, i);
			}
		}
	}

	protected override void SetEffectParamsDirty()
	{
		paramTex.SetData(this, 0, m_EffectFactor);
		paramTex.SetData(this, 1, m_Width);
		paramTex.SetData(this, 2, m_Softness);
		paramTex.SetData(this, 3, m_Brightness);
		paramTex.SetData(this, 4, m_Gloss);
	}

	protected override void SetVerticesDirty()
	{
		base.SetVerticesDirty();
		_lastRotation = m_Rotation;
		_lastEffectArea = m_EffectArea;
	}

	protected override void OnDidApplyAnimationProperties()
	{
		base.OnDidApplyAnimationProperties();
		if (!Mathf.Approximately(_lastRotation, m_Rotation) || _lastEffectArea != m_EffectArea)
		{
			SetVerticesDirty();
		}
	}
}
[ExecuteAlways]
public class UISyncEffect : BaseMaterialEffect
{
	[Tooltip("The target effect to synchronize.")]
	[SerializeField]
	private BaseMeshEffect m_TargetEffect;

	public BaseMeshEffect targetEffect
	{
		get
		{
			if (!(m_TargetEffect != this))
			{
				return null;
			}
			return m_TargetEffect;
		}
		set
		{
			if (!(m_TargetEffect == value))
			{
				m_TargetEffect = value;
				SetVerticesDirty();
				SetMaterialDirty();
				SetEffectParamsDirty();
			}
		}
	}

	protected override void OnEnable()
	{
		if ((bool)targetEffect)
		{
			targetEffect.syncEffects.Add(this);
		}
		base.OnEnable();
	}

	protected override void OnDisable()
	{
		if ((bool)targetEffect)
		{
			targetEffect.syncEffects.Remove(this);
		}
		base.OnDisable();
	}

	public override Hash128 GetMaterialHash(Material baseMaterial)
	{
		if (!base.isActiveAndEnabled)
		{
			return BaseMaterialEffect.k_InvalidHash;
		}
		BaseMaterialEffect baseMaterialEffect = targetEffect as BaseMaterialEffect;
		if (!baseMaterialEffect || !baseMaterialEffect.isActiveAndEnabled)
		{
			return BaseMaterialEffect.k_InvalidHash;
		}
		return baseMaterialEffect.GetMaterialHash(baseMaterial);
	}

	public override void ModifyMaterial(Material newMaterial, Graphic graphic)
	{
		if (base.isActiveAndEnabled)
		{
			BaseMaterialEffect baseMaterialEffect = targetEffect as BaseMaterialEffect;
			if ((bool)baseMaterialEffect && baseMaterialEffect.isActiveAndEnabled)
			{
				baseMaterialEffect.ModifyMaterial(newMaterial, graphic);
			}
		}
	}

	public override void ModifyMesh(VertexHelper vh, Graphic graphic)
	{
		if (base.isActiveAndEnabled && (bool)targetEffect && targetEffect.isActiveAndEnabled)
		{
			targetEffect.ModifyMesh(vh, graphic);
		}
	}
}
[AddComponentMenu("UI/UIEffects/UITransitionEffect", 5)]
public class UITransitionEffect : BaseMaterialEffect
{
	public enum EffectMode
	{
		Fade = 1,
		Cutoff,
		Dissolve
	}

	private const uint k_ShaderId = 40u;

	private static readonly int k_TransitionTexId = Shader.PropertyToID("_TransitionTex");

	private static readonly ParameterTexture s_ParamTex = new ParameterTexture(8, 128, "_ParamTex");

	private bool _lastKeepAspectRatio;

	private static Texture _defaultTransitionTexture;

	[Tooltip("Effect mode.")]
	[SerializeField]
	private EffectMode m_EffectMode = EffectMode.Cutoff;

	[Tooltip("Effect factor between 0(hidden) and 1(shown).")]
	[SerializeField]
	[Range(0f, 1f)]
	private float m_EffectFactor = 0.5f;

	[Tooltip("Transition texture (single channel texture).")]
	[SerializeField]
	private Texture m_TransitionTexture;

	[Header("Advanced Option")]
	[Tooltip("The area for effect.")]
	[SerializeField]
	private EffectArea m_EffectArea;

	[Tooltip("Keep effect aspect ratio.")]
	[SerializeField]
	private bool m_KeepAspectRatio;

	[Tooltip("Dissolve edge width.")]
	[SerializeField]
	[Range(0f, 1f)]
	private float m_DissolveWidth = 0.5f;

	[Tooltip("Dissolve edge softness.")]
	[SerializeField]
	[Range(0f, 1f)]
	private float m_DissolveSoftness = 0.5f;

	[Tooltip("Dissolve edge color.")]
	[SerializeField]
	[ColorUsage(false)]
	private Color m_DissolveColor = new Color(0f, 0.25f, 1f);

	[Tooltip("Disable the graphic's raycast target on hidden.")]
	[SerializeField]
	private bool m_PassRayOnHidden;

	public float effectFactor
	{
		get
		{
			return m_EffectFactor;
		}
		set
		{
			value = Mathf.Clamp(value, 0f, 1f);
			if (!Mathf.Approximately(m_EffectFactor, value))
			{
				m_EffectFactor = value;
				SetEffectParamsDirty();
			}
		}
	}

	public Texture transitionTexture
	{
		get
		{
			if (!m_TransitionTexture)
			{
				return defaultTransitionTexture;
			}
			return m_TransitionTexture;
		}
		set
		{
			if (!(m_TransitionTexture == value))
			{
				m_TransitionTexture = value;
				SetMaterialDirty();
			}
		}
	}

	private static Texture defaultTransitionTexture
	{
		get
		{
			if (!_defaultTransitionTexture)
			{
				return _defaultTransitionTexture = Resources.Load<Texture>("Default-Transition");
			}
			return _defaultTransitionTexture;
		}
	}

	public EffectMode effectMode
	{
		get
		{
			return m_EffectMode;
		}
		set
		{
			if (m_EffectMode != value)
			{
				m_EffectMode = value;
				SetMaterialDirty();
			}
		}
	}

	public bool keepAspectRatio
	{
		get
		{
			return m_KeepAspectRatio;
		}
		set
		{
			if (m_KeepAspectRatio != value)
			{
				m_KeepAspectRatio = value;
				SetVerticesDirty();
			}
		}
	}

	public override ParameterTexture paramTex => s_ParamTex;

	public float dissolveWidth
	{
		get
		{
			return m_DissolveWidth;
		}
		set
		{
			value = Mathf.Clamp(value, 0f, 1f);
			if (!Mathf.Approximately(m_DissolveWidth, value))
			{
				m_DissolveWidth = value;
				SetEffectParamsDirty();
			}
		}
	}

	public float dissolveSoftness
	{
		get
		{
			return m_DissolveSoftness;
		}
		set
		{
			value = Mathf.Clamp(value, 0f, 1f);
			if (!Mathf.Approximately(m_DissolveSoftness, value))
			{
				m_DissolveSoftness = value;
				SetEffectParamsDirty();
			}
		}
	}

	public Color dissolveColor
	{
		get
		{
			return m_DissolveColor;
		}
		set
		{
			if (!(m_DissolveColor == value))
			{
				m_DissolveColor = value;
				SetEffectParamsDirty();
			}
		}
	}

	public bool passRayOnHidden
	{
		get
		{
			return m_PassRayOnHidden;
		}
		set
		{
			m_PassRayOnHidden = value;
		}
	}

	public override Hash128 GetMaterialHash(Material material)
	{
		if (!base.isActiveAndEnabled || !material || !material.shader)
		{
			return BaseMaterialEffect.k_InvalidHash;
		}
		uint num = (uint)((int)m_EffectMode << 6);
		uint instanceID = (uint)transitionTexture.GetInstanceID();
		return new Hash128((uint)material.GetInstanceID(), 40 + num, instanceID, 0u);
	}

	public override void ModifyMaterial(Material newMaterial, Graphic graphic)
	{
		GraphicConnector.FindConnector(graphic);
		newMaterial.shader = Shader.Find($"Hidden/{newMaterial.shader.name} (UITransition)");
		SetShaderVariants(newMaterial, m_EffectMode);
		newMaterial.SetTexture(k_TransitionTexId, transitionTexture);
		paramTex.RegisterMaterial(newMaterial);
	}

	public override void ModifyMesh(VertexHelper vh, Graphic graphic)
	{
		if (base.isActiveAndEnabled)
		{
			float normalizedIndex = paramTex.GetNormalizedIndex(this);
			Texture texture = transitionTexture;
			float aspectRatio = ((m_KeepAspectRatio && (bool)texture) ? ((float)texture.width / (float)texture.height) : (-1f));
			Rect effectArea = m_EffectArea.GetEffectArea(vh, base.rectTransform.rect, aspectRatio);
			UIVertex vertex = default(UIVertex);
			int currentVertCount = vh.currentVertCount;
			for (int i = 0; i < currentVertCount; i++)
			{
				vh.PopulateUIVertex(ref vertex, i);
				base.connector.GetPositionFactor(m_EffectArea, i, effectArea, vertex.position, out var x, out var y);
				vertex.uv0 = new Vector2(Packer.ToFloat(vertex.uv0.x, vertex.uv0.y), Packer.ToFloat(x, y, normalizedIndex));
				vh.SetUIVertex(vertex, i);
			}
		}
	}

	protected override void SetEffectParamsDirty()
	{
		paramTex.SetData(this, 0, m_EffectFactor);
		if (m_EffectMode == EffectMode.Dissolve)
		{
			paramTex.SetData(this, 1, m_DissolveWidth);
			paramTex.SetData(this, 2, m_DissolveSoftness);
			paramTex.SetData(this, 4, m_DissolveColor.r);
			paramTex.SetData(this, 5, m_DissolveColor.g);
			paramTex.SetData(this, 6, m_DissolveColor.b);
		}
		if (m_PassRayOnHidden)
		{
			base.graphic.raycastTarget = 0f < m_EffectFactor;
		}
	}

	protected override void SetVerticesDirty()
	{
		base.SetVerticesDirty();
		_lastKeepAspectRatio = m_KeepAspectRatio;
	}

	protected override void OnDidApplyAnimationProperties()
	{
		base.OnDidApplyAnimationProperties();
		if (_lastKeepAspectRatio != m_KeepAspectRatio)
		{
			SetVerticesDirty();
		}
	}
}
