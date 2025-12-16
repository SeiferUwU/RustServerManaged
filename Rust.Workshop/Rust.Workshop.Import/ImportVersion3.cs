using System.Collections;
using UnityEngine;

namespace Rust.Workshop.Import;

internal class ImportVersion3 : SingletonComponent<ImportVersion3>
{
	internal IEnumerator DoImport(IWorkshopContent item, Skin skin, bool compressTexturesOnLoad = true)
	{
		WorkshopItemEditor.Loading(v1: true, "Downloading..", "", 0f);
		yield return StartCoroutine(DownloadFromWorkshop(item));
		if (!item.IsInstalled || item.Directory == null)
		{
			Debug.Log("Error opening item, not downloaded properly.");
			Debug.Log("item.Directory: " + item.Directory);
			Debug.Log("item.Installed: " + item.IsInstalled);
		}
		else
		{
			WorkshopItemEditor.Loading(v1: true, "Loading..", "Reloading Textures", 0f);
			yield return StartCoroutine(skin.LoadIcon(item.WorkshopId, item.Directory));
			yield return StartCoroutine(skin.LoadAssets(item.WorkshopId, item.Directory, null, compressTexturesOnLoad));
		}
	}

	private IEnumerator DownloadFromWorkshop(IWorkshopContent item)
	{
		item.Download();
		while (item.IsDownloading)
		{
			yield return null;
		}
		while (!item.IsInstalled)
		{
			yield return null;
		}
	}
}
