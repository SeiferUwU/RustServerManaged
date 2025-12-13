using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

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
			FilePathsData = new byte[217]
			{
				0, 0, 0, 1, 0, 0, 0, 55, 92, 80,
				97, 99, 107, 97, 103, 101, 115, 92, 82, 117,
				115, 116, 46, 82, 101, 110, 100, 101, 114, 80,
				105, 112, 101, 108, 105, 110, 101, 92, 84, 101,
				115, 116, 115, 92, 82, 117, 110, 116, 105, 109,
				101, 92, 77, 101, 115, 104, 66, 97, 108, 108,
				46, 99, 115, 0, 0, 0, 1, 0, 0, 0,
				74, 92, 80, 97, 99, 107, 97, 103, 101, 115,
				92, 82, 117, 115, 116, 46, 82, 101, 110, 100,
				101, 114, 80, 105, 112, 101, 108, 105, 110, 101,
				92, 84, 101, 115, 116, 115, 92, 82, 117, 110,
				116, 105, 109, 101, 92, 80, 101, 114, 79, 98,
				106, 101, 99, 116, 77, 97, 116, 101, 114, 105,
				97, 108, 80, 114, 111, 112, 101, 114, 116, 105,
				101, 115, 46, 99, 115, 0, 0, 0, 1, 0,
				0, 0, 64, 92, 80, 97, 99, 107, 97, 103,
				101, 115, 92, 82, 117, 115, 116, 46, 82, 101,
				110, 100, 101, 114, 80, 105, 112, 101, 108, 105,
				110, 101, 92, 84, 101, 115, 116, 115, 92, 82,
				117, 110, 116, 105, 109, 101, 92, 82, 101, 110,
				100, 101, 114, 84, 111, 65, 116, 108, 97, 115,
				84, 101, 115, 116, 46, 99, 115
			},
			TypesData = new byte[154]
			{
				0, 0, 0, 0, 37, 82, 117, 115, 116, 46,
				82, 101, 110, 100, 101, 114, 80, 105, 112, 101,
				108, 105, 110, 101, 46, 69, 120, 97, 109, 112,
				108, 101, 115, 124, 77, 101, 115, 104, 66, 97,
				108, 108, 0, 0, 0, 0, 56, 82, 117, 115,
				116, 46, 82, 101, 110, 100, 101, 114, 80, 105,
				112, 101, 108, 105, 110, 101, 46, 69, 120, 97,
				109, 112, 108, 101, 115, 124, 80, 101, 114, 79,
				98, 106, 101, 99, 116, 77, 97, 116, 101, 114,
				105, 97, 108, 80, 114, 111, 112, 101, 114, 116,
				105, 101, 115, 0, 0, 0, 0, 46, 82, 117,
				115, 116, 46, 82, 101, 110, 100, 101, 114, 80,
				105, 112, 101, 108, 105, 110, 101, 46, 69, 120,
				97, 109, 112, 108, 101, 115, 124, 82, 101, 110,
				100, 101, 114, 84, 111, 65, 116, 108, 97, 115,
				84, 101, 115, 116
			},
			TotalFiles = 3,
			TotalTypes = 3,
			IsEditorOnly = false
		};
	}
}
namespace Rust.RenderPipeline.Examples;

public class MeshBall : MonoBehaviour
{
	private const int NUM_INSTANCES = 1023;

	private static readonly int baseColorId = Shader.PropertyToID("_BaseColor");

	private static readonly int metallicId = Shader.PropertyToID("_Metallic");

	private static readonly int roughnessId = Shader.PropertyToID("_Roughness");

	[SerializeField]
	private Mesh mesh;

	[SerializeField]
	private Material material;

	[SerializeField]
	private float radius = 10f;

	private Matrix4x4[] matrices = new Matrix4x4[1023];

	private Vector4[] baseColors = new Vector4[1023];

	private float[] metallic = new float[1023];

	private float[] roughness = new float[1023];

	private MaterialPropertyBlock materialPropertyBlock;

	private bool IsValid()
	{
		if (mesh != null)
		{
			return material != null;
		}
		return false;
	}

	private void Awake()
	{
		if (!IsValid())
		{
			UnityEngine.Debug.LogError("Failed to render MeshBall due to missing object reference!", this);
			return;
		}
		for (int i = 0; i < matrices.Length; i++)
		{
			matrices[i] = Matrix4x4.TRS(Random.insideUnitSphere * radius, Quaternion.Euler(Random.value * 360f, Random.value * 360f, Random.value * 360f), Vector3.one * Random.Range(0.5f, 1.5f));
			baseColors[i] = new Vector4(Random.value, Random.value, Random.value, Random.Range(0.5f, 1f));
			metallic[i] = ((Random.value < 0.25f) ? 1f : 0f);
			roughness[i] = Random.Range(0.05f, 0.95f);
		}
	}

	private void Update()
	{
		if (IsValid())
		{
			if (materialPropertyBlock == null)
			{
				materialPropertyBlock = new MaterialPropertyBlock();
				materialPropertyBlock.SetVectorArray(baseColorId, baseColors);
				materialPropertyBlock.SetFloatArray(metallicId, metallic);
				materialPropertyBlock.SetFloatArray(roughnessId, roughness);
			}
			Graphics.DrawMeshInstanced(mesh, 0, material, matrices, 1023, materialPropertyBlock);
		}
	}
}
public class PerObjectMaterialProperties : MonoBehaviour
{
	private static readonly int baseColorId = Shader.PropertyToID("_BaseColor");

	private static readonly int cutoffId = Shader.PropertyToID("_Cutoff");

	private static readonly int metallicId = Shader.PropertyToID("_Metallic");

	private static readonly int roughnessId = Shader.PropertyToID("_Roughness");

	private static readonly int emissionColorId = Shader.PropertyToID("_EmissionColor");

	private static MaterialPropertyBlock materialPropertyBlock;

	[SerializeField]
	private Color baseColor = Color.white;

	[SerializeField]
	[Range(0f, 1f)]
	private float cutoff = 0.5f;

	[SerializeField]
	[Range(0f, 1f)]
	private float metallic;

	[SerializeField]
	[Range(0f, 1f)]
	private float roughness = 0.5f;

	[SerializeField]
	[ColorUsage(false, true)]
	private Color emissionColor = Color.black;

	private void OnValidate()
	{
		if (materialPropertyBlock == null)
		{
			materialPropertyBlock = new MaterialPropertyBlock();
		}
		materialPropertyBlock.SetColor(baseColorId, baseColor);
		materialPropertyBlock.SetFloat(cutoffId, cutoff);
		materialPropertyBlock.SetFloat(metallicId, metallic);
		materialPropertyBlock.SetFloat(roughnessId, roughness);
		materialPropertyBlock.SetColor(emissionColorId, emissionColor);
		GetComponent<Renderer>().SetPropertyBlock(materialPropertyBlock);
	}

	private void Awake()
	{
		OnValidate();
	}
}
public class RenderToAtlasTest : MonoBehaviour
{
	private static readonly int atlasTextureId = Shader.PropertyToID("_AtlasTextureTest");

	[SerializeField]
	private Texture2D testTexture;

	private Texture2DAtlas texture2DAtlas;

	private Vector4 scaleOffset;

	private CommandBuffer commandBuffer;

	[ContextMenu("Render To Atlas")]
	private void RenderToAtlas()
	{
		if (texture2DAtlas == null)
		{
			texture2DAtlas = new Texture2DAtlas(1024, 1024, GraphicsFormat.R8G8B8A8_SRGB);
		}
		commandBuffer = new CommandBuffer();
		if (texture2DAtlas.AllocateTexture(commandBuffer, ref scaleOffset, testTexture, testTexture.width, testTexture.height))
		{
			UnityEngine.Debug.Log("Texture allocated!");
		}
		texture2DAtlas.UpdateTexture(commandBuffer, testTexture, ref scaleOffset);
		commandBuffer.SetGlobalTexture(atlasTextureId, texture2DAtlas.AtlasTexture);
		Graphics.ExecuteCommandBuffer(commandBuffer);
		commandBuffer.Clear();
		commandBuffer = null;
	}
}
