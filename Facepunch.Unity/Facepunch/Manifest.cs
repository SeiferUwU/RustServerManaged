#define UNITY_ASSERTIONS
using System;
using System.Collections;
using Facepunch.Models;
using UnityEngine;
using UnityEngine.Assertions;

namespace Facepunch;

public static class Manifest
{
	public static DateTime LastDownloaded { get; internal set; }

	public static string Contents { get; internal set; }

	internal static void Download()
	{
		if (string.IsNullOrEmpty(Application.Integration.PublicKey))
		{
			if (Application.Integration.DebugOutput)
			{
				Debug.LogWarning("[manifest] Not downloading manifest - no public key");
			}
		}
		else
		{
			Application.Controller.StartCoroutine(AutoUpdateManifest());
		}
	}

	private static IEnumerator AutoUpdateManifest()
	{
		while (true)
		{
			UpdateManifest();
			yield return new WaitForSecondsRealtime(3600f);
		}
	}

	public static void UpdateManifest()
	{
		string text = Application.Integration.ApiUrl + "public/manifest/?public_key=" + Application.Integration.PublicKey;
		if (Application.Integration.DebugOutput)
		{
			Debug.Log("[Manifest] Fetching from \"" + text + "\"");
		}
		Uri arg = new Uri(text, UriKind.Absolute);
		if (Application.Integration.DebugOutput)
		{
			Debug.Log($"[Manifest] URI IS \"{arg}\"");
		}
		WebUtil.Get(text, delegate(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				if (Application.Integration.DebugOutput)
				{
					Debug.Log("[Manifest] Empty Response, bailing.");
				}
			}
			else
			{
				LoadManifest(str);
			}
		});
	}

	private static void LoadManifest(string text)
	{
		LastDownloaded = DateTime.UtcNow;
		Contents = text;
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		if (Application.Integration.DebugOutput)
		{
			Debug.Log("[Manifest] Loading Manifest..");
		}
		try
		{
			Application.Manifest = Facepunch.Models.Manifest.FromJson(text);
			OnManifestLoaded(Application.Manifest);
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Exception when reading manifest (" + ex.Message + " / " + ex.StackTrace + ")");
			if (ex.InnerException != null)
			{
				Debug.LogWarning("(" + ex.InnerException.Message + " / " + ex.InnerException.StackTrace + ")");
			}
		}
	}

	private static void OnManifestLoaded(Facepunch.Models.Manifest manifest)
	{
		Assert.IsNotNull(manifest);
		Application.Integration.OnManifestFile(manifest);
	}
}
