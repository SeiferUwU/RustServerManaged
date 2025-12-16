using UnityEngine;

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
			Debug.LogError("Failed to render MeshBall due to missing object reference!", this);
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
