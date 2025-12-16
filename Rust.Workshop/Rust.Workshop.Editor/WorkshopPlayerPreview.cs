using Facepunch.Extend;
using UnityEngine;

namespace Rust.Workshop.Editor;

internal class WorkshopPlayerPreview : MonoBehaviour
{
	public static GameObject Instance;

	protected WorkshopInterface Interface => GetComponentInParent<WorkshopInterface>();

	public void Setup(GameObject ClothingPrefab, ulong id, bool focus, bool IsClothing, float additionalZoom = 0f, Vector3 rotationOffset = default(Vector3), bool invisible = false)
	{
		Cleanup();
		Instance = Global.CreatePrefab("assets/prefabs/player/player_model.prefab");
		Instance.transform.position = Vector3.zero;
		Instance.transform.rotation = Quaternion.identity;
		Instance.SetActive(value: true);
		ClothingPrefab.transform.SetParent(Instance.transform);
		ClothingPrefab.SetActive(value: false);
		Instance.transform.position = SingletonComponent<WorkshopItemEditor>.Instance.ItemSpawnPositionAnchor.transform.position;
		Instance.transform.rotation = SingletonComponent<WorkshopItemEditor>.Instance.ItemSpawnPositionAnchor.transform.rotation;
		if (focus)
		{
			FocusCameraOnPreview(additionalZoom, rotationOffset);
		}
	}

	public static void FocusCameraOnPreview(float additionalZoom = 0f, Vector3 rotationOffset = default(Vector3))
	{
		if (!(Instance == null))
		{
			Vector3 vector = new Vector3(0.3f, 0.1f, 1f);
			if (rotationOffset != default(Vector3))
			{
				vector = Quaternion.Euler(rotationOffset) * vector;
			}
			Camera.main.FocusOnRenderer(Instance, vector, Vector3.up, -1, additionalZoom);
		}
	}

	public void Cleanup()
	{
		if (Instance != null)
		{
			Object.Destroy(Instance);
			Instance = null;
		}
	}

	private void OnDisable()
	{
		if (!Application.isQuitting)
		{
			Cleanup();
		}
	}
}
