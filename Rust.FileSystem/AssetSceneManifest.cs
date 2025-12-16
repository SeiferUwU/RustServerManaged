using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

[JsonModel]
public class AssetSceneManifest
{
	[JsonModel]
	public class Entry
	{
		public string Name { get; set; }

		public bool AutoLoad { get; set; }

		public bool CanUnload { get; set; }

		public List<string> IncludedAssets { get; set; }
	}

	public const string FileName = "AssetSceneManifest.json";

	public const string Prefix = "AssetScene-";

	public const string BootstrapSceneName = "AssetScene-bootstrap";

	public const string PrefabsSceneName = "AssetScene-prefabs";

	public const string WorldSceneName = "AssetScene-world";

	public const string MonumentSceneName = "AssetScene-monument";

	public const string PropsSceneName = "AssetScene-props";

	public List<Entry> Scenes { get; set; }

	[JsonIgnore]
	public List<string> AutoLoadScenes { get; private set; }

	[JsonIgnore]
	public List<string> UnloadableScenes { get; private set; }

	[JsonIgnore]
	public List<string> MonumentScenes { get; private set; }

	public static AssetSceneManifest Current { get; private set; }

	private void Initialize()
	{
		if (Scenes == null)
		{
			List<Entry> list = (Scenes = new List<Entry>());
		}
		AutoLoadScenes = (from s in Scenes
			where s.AutoLoad
			select s.Name).ToList();
		UnloadableScenes = (from s in Scenes
			where s.CanUnload
			select s.Name).ToList();
		MonumentScenes = (from s in Scenes
			where s.Name.StartsWith("AssetScene-monument", StringComparison.OrdinalIgnoreCase)
			select s.Name).ToList();
	}

	public static void Load(AssetBundleBackend backend)
	{
		if (Current != null)
		{
			throw new InvalidOperationException("AssetSceneManifest is already loaded.");
		}
		Current = JsonConvert.DeserializeObject<AssetSceneManifest>(File.ReadAllText((Application.platform == RuntimePlatform.OSXPlayer) ? Path.Combine(Application.dataPath, "../../Bundles/", "AssetSceneManifest.json") : Path.Combine(Application.dataPath, "../Bundles/", "AssetSceneManifest.json")));
		if (Current == null)
		{
			Current = new AssetSceneManifest();
		}
		Current.Initialize();
	}
}
