using UnityEngine;

namespace Rust.RenderPipeline.Examples;

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
