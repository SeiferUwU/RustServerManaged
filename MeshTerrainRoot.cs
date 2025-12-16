using UnityEngine;

[ExecuteInEditMode]
public class MeshTerrainRoot : MonoBehaviour, IClientComponent
{
	public GameObject TerrainBlendSearchRoot;

	public Mesh TerrainMeshAsset;

	public Material TerrainMaterial;

	public Vector3 TerrainPos;

	public Vector3 TerrainSize;
}
