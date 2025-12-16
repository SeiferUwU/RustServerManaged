using UnityEngine;

public class WallpaperViewModel : MonoBehaviour
{
	public GameObject[] models;

	public void ToggleModels(WallpaperSettings.Category mode)
	{
		for (int i = 0; i < models.Length; i++)
		{
			models[i].SetActive(mode - 1 == (WallpaperSettings.Category)i);
		}
	}
}
