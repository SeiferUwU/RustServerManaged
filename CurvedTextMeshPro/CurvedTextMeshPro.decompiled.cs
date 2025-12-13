using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyVersion("0.0.0.0")]
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
			FilePathsData = new byte[203]
			{
				0, 0, 0, 1, 0, 0, 0, 61, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 67, 117, 114, 118, 101, 100,
				84, 101, 120, 116, 77, 101, 115, 104, 80, 114,
				111, 92, 83, 99, 114, 105, 112, 116, 115, 92,
				84, 101, 120, 116, 80, 114, 111, 79, 110, 65,
				67, 105, 114, 99, 108, 101, 46, 99, 115, 0,
				0, 0, 1, 0, 0, 0, 60, 92, 65, 115,
				115, 101, 116, 115, 92, 80, 108, 117, 103, 105,
				110, 115, 92, 67, 117, 114, 118, 101, 100, 84,
				101, 120, 116, 77, 101, 115, 104, 80, 114, 111,
				92, 83, 99, 114, 105, 112, 116, 115, 92, 84,
				101, 120, 116, 80, 114, 111, 79, 110, 65, 67,
				117, 114, 118, 101, 46, 99, 115, 0, 0, 0,
				1, 0, 0, 0, 58, 92, 65, 115, 115, 101,
				116, 115, 92, 80, 108, 117, 103, 105, 110, 115,
				92, 67, 117, 114, 118, 101, 100, 84, 101, 120,
				116, 77, 101, 115, 104, 80, 114, 111, 92, 83,
				99, 114, 105, 112, 116, 115, 92, 84, 101, 120,
				116, 80, 114, 111, 79, 110, 65, 69, 120, 112,
				46, 99, 115
			},
			TypesData = new byte[125]
			{
				0, 0, 0, 0, 38, 110, 116, 119, 46, 67,
				117, 114, 118, 101, 100, 84, 101, 120, 116, 77,
				101, 115, 104, 80, 114, 111, 124, 84, 101, 120,
				116, 80, 114, 111, 79, 110, 65, 67, 105, 114,
				99, 108, 101, 0, 0, 0, 0, 37, 110, 116,
				119, 46, 67, 117, 114, 118, 101, 100, 84, 101,
				120, 116, 77, 101, 115, 104, 80, 114, 111, 124,
				84, 101, 120, 116, 80, 114, 111, 79, 110, 65,
				67, 117, 114, 118, 101, 0, 0, 0, 0, 35,
				110, 116, 119, 46, 67, 117, 114, 118, 101, 100,
				84, 101, 120, 116, 77, 101, 115, 104, 80, 114,
				111, 124, 84, 101, 120, 116, 80, 114, 111, 79,
				110, 65, 69, 120, 112
			},
			TotalFiles = 3,
			TotalTypes = 3,
			IsEditorOnly = false
		};
	}
}
namespace ntw.CurvedTextMeshPro;

[ExecuteInEditMode]
public class TextProOnACircle : TextProOnACurve
{
	[SerializeField]
	[Tooltip("The radius of the text circle arc")]
	private float m_radius = 10f;

	[Tooltip("How much degrees the text arc should span")]
	public float m_arcDegrees = 90f;

	[SerializeField]
	[Tooltip("The angular offset at which the arc should be centered, in degrees")]
	private float m_angularOffset = -90f;

	public float lineHeight = 25f;

	[Range(0f, 2f)]
	public float verticalCurveOffset;

	public float verticalCurveOffsetTextAdjustment = 0.1f;

	public float verticalCurveRotation;

	public int maxLines = 5;

	private float m_oldRadius = float.MaxValue;

	private float m_oldArcDegrees = float.MaxValue;

	private float m_oldAngularOffset = float.MaxValue;

	private float m_oldLineHeight = float.MaxValue;

	private float m_oldVerticalCurvingOffset = float.MaxValue;

	private float m_oldVerticalCurveRotation = float.MaxValue;

	private int m_oldMaxLines = int.MaxValue;

	private float m_oldVerticalCurveOffsetTextAdjustment = float.MaxValue;

	protected override bool ParametersHaveChanged()
	{
		bool result = m_radius != m_oldRadius || m_arcDegrees != m_oldArcDegrees || m_angularOffset != m_oldAngularOffset || lineHeight != m_oldLineHeight || verticalCurveOffset != m_oldVerticalCurvingOffset || verticalCurveRotation != m_oldVerticalCurveRotation || maxLines != m_oldMaxLines || verticalCurveOffsetTextAdjustment != m_oldVerticalCurveOffsetTextAdjustment;
		m_oldRadius = m_radius;
		m_oldArcDegrees = m_arcDegrees;
		m_oldAngularOffset = m_angularOffset;
		m_oldLineHeight = lineHeight;
		m_oldVerticalCurvingOffset = verticalCurveOffset;
		m_oldVerticalCurveRotation = verticalCurveRotation;
		m_oldMaxLines = maxLines;
		m_oldVerticalCurveOffsetTextAdjustment = verticalCurveOffsetTextAdjustment;
		return result;
	}

	protected override Matrix4x4 ComputeTransformationMatrix(Vector3 charMidBaselinePos, float zeroToOnePos, TMP_TextInfo textInfo, int charIdx)
	{
		float f = ((zeroToOnePos - 0.5f) * m_arcDegrees + m_angularOffset) * (MathF.PI / 180f);
		int num = Mathf.Min(textInfo.lineCount, maxLines - 1);
		float num2 = (float)Mathf.Min(textInfo.characterInfo[charIdx].lineNumber, maxLines - 1) - (float)(textInfo.lineCount - 1) / 2f;
		float num3 = Mathf.Cos(f);
		float num4 = Mathf.Sin(f);
		float num5 = m_radius - textInfo.lineInfo[0].lineExtents.max.y * Mathf.Abs(num2) * verticalCurveOffset * Mathf.Clamp01(2f / (float)num);
		num5 -= textInfo.lineInfo[0].lineExtents.max.y * num2 * verticalCurveOffsetTextAdjustment * Mathf.Clamp01(2f / (float)num);
		Vector2 vector = new Vector2(num3 * num5, (0f - num4) * num5);
		float y = lineHeight * (0f - num2);
		float angle = m_radius * (0f - num2) * verticalCurveRotation;
		return Matrix4x4.TRS(new Vector3(vector.x, y, 0f - vector.y), Quaternion.AngleAxis((0f - Mathf.Atan2(num4, num3)) * 57.29578f - 90f, Vector3.up) * Quaternion.AngleAxis(angle, Vector3.right), Vector3.one);
	}
}
[ExecuteInEditMode]
public abstract class TextProOnACurve : MonoBehaviour
{
	private TMP_Text m_TextComponent;

	private void Awake()
	{
		m_TextComponent = base.gameObject.GetComponent<TMP_Text>();
	}

	private void OnEnable()
	{
		ForceUpdate();
	}

	public void ForceUpdate()
	{
		if ((UnityEngine.Object)(object)m_TextComponent == null)
		{
			return;
		}
		m_TextComponent.ForceMeshUpdate(ignoreActiveState: true);
		TMP_TextInfo textInfo = m_TextComponent.textInfo;
		int characterCount = textInfo.characterCount;
		if (characterCount == 0)
		{
			return;
		}
		float x = m_TextComponent.bounds.min.x;
		float x2 = m_TextComponent.bounds.max.x;
		for (int i = 0; i < characterCount; i++)
		{
			if (i < textInfo.characterInfo.Length && textInfo.characterInfo[i].isVisible)
			{
				int vertexIndex = textInfo.characterInfo[i].vertexIndex;
				int materialReferenceIndex = textInfo.characterInfo[i].materialReferenceIndex;
				Vector3[] vertices = textInfo.meshInfo[materialReferenceIndex].vertices;
				Vector3 vector = new Vector2((vertices[vertexIndex].x + vertices[vertexIndex + 2].x) / 2f, textInfo.characterInfo[i].baseLine);
				vertices[vertexIndex] += -vector;
				vertices[vertexIndex + 1] += -vector;
				vertices[vertexIndex + 2] += -vector;
				vertices[vertexIndex + 3] += -vector;
				float zeroToOnePos = (vector.x - x) / (x2 - x);
				Matrix4x4 matrix4x = ComputeTransformationMatrix(vector, zeroToOnePos, textInfo, i);
				vertices[vertexIndex] = matrix4x.MultiplyPoint3x4(vertices[vertexIndex]);
				vertices[vertexIndex + 1] = matrix4x.MultiplyPoint3x4(vertices[vertexIndex + 1]);
				vertices[vertexIndex + 2] = matrix4x.MultiplyPoint3x4(vertices[vertexIndex + 2]);
				vertices[vertexIndex + 3] = matrix4x.MultiplyPoint3x4(vertices[vertexIndex + 3]);
			}
		}
		m_TextComponent.UpdateVertexData();
	}

	protected abstract bool ParametersHaveChanged();

	protected abstract Matrix4x4 ComputeTransformationMatrix(Vector3 charMidBaselinePos, float zeroToOnePos, TMP_TextInfo textInfo, int charIdx);
}
[ExecuteInEditMode]
public class TextProOnAExp : TextProOnACurve
{
	[SerializeField]
	[Tooltip("The base of the exponential curve")]
	private float m_expBase = 1.3f;

	private float m_oldExpBase = float.MaxValue;

	protected override bool ParametersHaveChanged()
	{
		bool result = m_expBase != m_oldExpBase;
		m_oldExpBase = m_expBase;
		return result;
	}

	protected override Matrix4x4 ComputeTransformationMatrix(Vector3 charMidBaselinePos, float zeroToOnePos, TMP_TextInfo textInfo, int charIdx)
	{
		float x = charMidBaselinePos.x;
		float num = Mathf.Pow(m_expBase, x);
		Vector2 vector = new Vector2(x, num - textInfo.lineInfo[0].lineExtents.max.y * (float)textInfo.characterInfo[charIdx].lineNumber);
		return Matrix4x4.TRS(new Vector3(vector.x, vector.y, 0f), Quaternion.AngleAxis(Mathf.Atan(Mathf.Log(m_expBase) * num) * 57.29578f, Vector3.forward), Vector3.one);
	}
}
