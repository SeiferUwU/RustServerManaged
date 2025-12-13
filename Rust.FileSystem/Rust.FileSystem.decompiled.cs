using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Facepunch.Extend;
using Newtonsoft.Json;
using Rust;
using UnityEngine;
using UnityEngine.SceneManagement;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyVersion("0.0.0.0")]
public class AssetBundleBackend : FileSystemBackend, IDisposable
{
	private AssetBundle rootBundle;

	private AssetBundleManifest manifest;

	private Dictionary<string, AssetBundle> bundles = new Dictionary<string, AssetBundle>(StringComparer.OrdinalIgnoreCase);

	private Dictionary<string, AssetBundle> files = new Dictionary<string, AssetBundle>(StringComparer.OrdinalIgnoreCase);

	private Dictionary<string, string> prefabToAssetSceneName = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

	private Dictionary<string, UnityEngine.AsyncOperation> assetSceneLoads = new Dictionary<string, UnityEngine.AsyncOperation>(StringComparer.OrdinalIgnoreCase);

	private Dictionary<string, Dictionary<string, GameObject>> assetScenePrefabs = new Dictionary<string, Dictionary<string, GameObject>>(StringComparer.OrdinalIgnoreCase);

	public string assetPath { get; private set; }

	public static bool Enabled => true;

	public void Load(string assetRoot)
	{
		try
		{
			isError = false;
			string directoryName = Path.GetDirectoryName(assetRoot);
			char directorySeparatorChar = Path.DirectorySeparatorChar;
			assetPath = directoryName + directorySeparatorChar;
			rootBundle = AssetBundle.LoadFromFile(assetRoot);
			if (rootBundle == null)
			{
				LoadError("Couldn't load root AssetBundle - " + assetRoot);
				return;
			}
			AssetBundleManifest[] array = rootBundle.LoadAllAssets<AssetBundleManifest>();
			if (array.Length != 1)
			{
				LoadError($"Couldn't find AssetBundleManifest - {array.Length}");
				return;
			}
			manifest = array[0];
			string[] allAssetBundles = manifest.GetAllAssetBundles();
			foreach (string text in allAssetBundles)
			{
				UnityEngine.Debug.Log("Loading " + text);
				LoadBundle(text);
			}
			AssetSceneManifest.Load(this);
			prefabToAssetSceneName.Clear();
			foreach (AssetSceneManifest.Entry scene in AssetSceneManifest.Current.Scenes)
			{
				foreach (string includedAsset in scene.IncludedAssets)
				{
					if (!prefabToAssetSceneName.TryAdd(includedAsset, scene.Name))
					{
						UnityEngine.Debug.LogError("Duplicate prefab in asset scenes: " + includedAsset);
					}
				}
			}
			foreach (AssetSceneManifest.Entry scene2 in AssetSceneManifest.Current.Scenes)
			{
				if (scene2.AutoLoad)
				{
					Global.Runner.StartCoroutine(LoadAssetScene(scene2.Name));
				}
			}
		}
		catch (Exception exception)
		{
			UnityEngine.Debug.LogException(exception);
		}
	}

	private AssetBundle LoadBundle(string bundleName)
	{
		if (bundles.ContainsKey(bundleName))
		{
			UnityEngine.Debug.Log("LoadBundle " + bundleName + " already loaded");
			return null;
		}
		string text = assetPath + bundleName;
		if (!File.Exists(text))
		{
			return null;
		}
		AssetBundle assetBundle = AssetBundle.LoadFromFile(text);
		if (assetBundle == null)
		{
			LoadError("Couldn't load AssetBundle - " + text);
			return null;
		}
		bundles.Add(bundleName, assetBundle);
		return assetBundle;
	}

	public void BuildFileIndex()
	{
		files.Clear();
		foreach (KeyValuePair<string, AssetBundle> bundle in bundles)
		{
			if (!bundle.Key.StartsWith("content", StringComparison.OrdinalIgnoreCase))
			{
				string[] allAssetNames = bundle.Value.GetAllAssetNames();
				foreach (string key in allAssetNames)
				{
					files.Add(key, bundle.Value);
				}
			}
		}
	}

	public void Dispose()
	{
		manifest = null;
		foreach (KeyValuePair<string, AssetBundle> bundle in bundles)
		{
			bundle.Value.Unload(unloadAllLoadedObjects: false);
			UnityEngine.Object.DestroyImmediate(bundle.Value);
		}
		bundles.Clear();
		if ((bool)rootBundle)
		{
			rootBundle.Unload(unloadAllLoadedObjects: false);
			UnityEngine.Object.DestroyImmediate(rootBundle);
			rootBundle = null;
		}
	}

	public override List<string> UnloadBundles(string partialName)
	{
		List<string> list = new List<string>();
		string[] array = bundles.Keys.ToArray();
		foreach (string text in array)
		{
			if (text.IndexOf(partialName, StringComparison.OrdinalIgnoreCase) < 0)
			{
				continue;
			}
			AssetBundle assetBundle = bundles[text];
			assetBundle.Unload(unloadAllLoadedObjects: false);
			UnityEngine.Object.DestroyImmediate(assetBundle);
			bundles.Remove(text);
			assetBundle = LoadBundle(text);
			string[] allAssetNames = assetBundle.GetAllAssetNames();
			foreach (string text2 in allAssetNames)
			{
				files[text2] = assetBundle;
				list.Add(text2);
				if (cache.TryGetValue(text2, out var value))
				{
					cache.Remove(text2);
					UnityEngine.Object.DestroyImmediate(value, allowDestroyingAssets: true);
				}
			}
		}
		return list;
	}

	public float GetAssetSceneProgress()
	{
		List<string> autoLoadScenes = AssetSceneManifest.Current.AutoLoadScenes;
		return GetAssetSceneProgress(autoLoadScenes);
	}

	public float GetAssetSceneProgress(List<string> sceneNames)
	{
		if (sceneNames == null || sceneNames.Count == 0)
		{
			return 1f;
		}
		int num = 0;
		float num2 = 0f;
		foreach (string sceneName in sceneNames)
		{
			float assetSceneProgress = GetAssetSceneProgress(sceneName);
			num2 += assetSceneProgress;
			if (assetSceneProgress >= 1f)
			{
				num++;
			}
		}
		if (num >= sceneNames.Count)
		{
			return 1f;
		}
		return Mathf.Min(num2 / (float)sceneNames.Count, 0.999f);
	}

	public float GetAssetSceneProgress(string sceneName)
	{
		if (!assetSceneLoads.TryGetValue(sceneName, out var value))
		{
			AssetSceneUtil.Log("Asset scene '" + sceneName + "' is not currently loading or loaded - cannot get progress.");
			return 0f;
		}
		return value.progress / 0.9f * 0.99f;
	}

	public IEnumerator LoadAssetScenes(List<string> sceneNames)
	{
		if (sceneNames == null || sceneNames.Count == 0)
		{
			yield break;
		}
		foreach (string sceneName in sceneNames)
		{
			if (!assetSceneLoads.ContainsKey(sceneName))
			{
				Global.Runner.StartCoroutine(LoadAssetScene(sceneName));
			}
		}
		while (GetAssetSceneProgress(sceneNames) < 1f)
		{
			yield return null;
		}
	}

	public IEnumerator UnloadAssetScenes(List<string> sceneNames, Action<string, Dictionary<string, GameObject>> onAssetSceneUnloaded)
	{
		List<UnityEngine.AsyncOperation> unloadOperations = new List<UnityEngine.AsyncOperation>();
		foreach (string sceneName in sceneNames)
		{
			if (!assetSceneLoads.TryGetValue(sceneName, out var asyncOperation))
			{
				continue;
			}
			AssetSceneManifest.Entry entry = AssetSceneManifest.Current.Scenes.FindWith((AssetSceneManifest.Entry s) => s.Name, sceneName, StringComparer.OrdinalIgnoreCase);
			if (entry == null || !entry.CanUnload)
			{
				UnityEngine.Debug.LogError("Asset scene '" + sceneName + "' is not allowed to be unloaded.");
				continue;
			}
			while (!asyncOperation.isDone)
			{
				yield return null;
			}
			UnityEngine.AsyncOperation asyncOperation2 = SceneManager.UnloadSceneAsync(sceneName);
			if (asyncOperation2 == null)
			{
				continue;
			}
			Dictionary<string, GameObject> valueOrDefault = assetScenePrefabs.GetValueOrDefault(sceneName);
			if (valueOrDefault != null)
			{
				foreach (string key in valueOrDefault.Keys)
				{
					cache.Remove(key);
				}
			}
			onAssetSceneUnloaded?.Invoke(sceneName, valueOrDefault);
			assetSceneLoads.Remove(sceneName);
			assetScenePrefabs.Remove(sceneName);
			unloadOperations.Add(asyncOperation2);
			asyncOperation = null;
		}
		while (unloadOperations.Any((UnityEngine.AsyncOperation o) => !o.isDone))
		{
			yield return null;
		}
	}

	public List<string> GetRequiredAssetScenesForPrefabs(IEnumerable<string> prefabPaths)
	{
		HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
		foreach (string prefabPath in prefabPaths)
		{
			if (prefabToAssetSceneName.TryGetValue(prefabPath, out var value))
			{
				hashSet.Add(value);
			}
			else
			{
				UnityEngine.Debug.LogWarning("Prefab '" + prefabPath + "' does not have an associated asset scene.");
			}
		}
		return hashSet.ToList();
	}

	public List<(string Path, GameObject Prefab)> GetAssetScenePrefabs(List<string> assetSceneNames = null)
	{
		return (from kvp in assetScenePrefabs.Where((KeyValuePair<string, Dictionary<string, GameObject>> kvp) => assetSceneNames == null || assetSceneNames.Contains(kvp.Key, StringComparer.OrdinalIgnoreCase)).SelectMany((KeyValuePair<string, Dictionary<string, GameObject>> d) => d.Value)
			select (Path: kvp.Key, Prefab: kvp.Value)).ToList();
	}

	private IEnumerator LoadAssetScene(string sceneName)
	{
		LoadSceneParameters parameters = new LoadSceneParameters(LoadSceneMode.Additive, LocalPhysicsMode.Physics3D);
		if (assetSceneLoads.ContainsKey(sceneName))
		{
			UnityEngine.Debug.LogError("Already loaded asset scene: " + sceneName);
			yield break;
		}
		AssetSceneUtil.Log("Starting to load asset scene: " + sceneName);
		UnityEngine.AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, parameters);
		if (asyncLoad == null)
		{
			isError = true;
			loadingError = "Failed to load asset scene: " + sceneName;
			UnityEngine.Debug.LogError(loadingError);
			yield break;
		}
		asyncLoad.allowSceneActivation = false;
		assetSceneLoads[sceneName] = asyncLoad;
		Scene currentActiveScene = SceneManager.GetActiveScene();
		while (!asyncLoad.isDone)
		{
			yield return null;
			if (asyncLoad.progress >= 0.9f)
			{
				AssetSceneUtil.Log("Asset scene " + sceneName + " loaded, activating...");
				currentActiveScene = SceneManager.GetActiveScene();
				asyncLoad.allowSceneActivation = true;
				SceneManager.SetActiveScene(currentActiveScene);
			}
		}
		SceneManager.SetActiveScene(currentActiveScene);
		Scene sceneByName = SceneManager.GetSceneByName(sceneName);
		if (!sceneByName.IsValid() || !sceneByName.isLoaded)
		{
			isError = true;
			loadingError = "Failed to get asset scene after loading: " + sceneName;
			UnityEngine.Debug.LogError(loadingError);
			yield break;
		}
		AssetSceneUtil.Log("Asset scene " + sceneName + " activated successfully.");
		GameObject[] rootGameObjects = sceneByName.GetRootGameObjects();
		Dictionary<string, GameObject> dictionary = new Dictionary<string, GameObject>(StringComparer.OrdinalIgnoreCase);
		GameObject[] array = rootGameObjects;
		foreach (GameObject gameObject in array)
		{
			string name = gameObject.name;
			dictionary.TryAdd(name, gameObject);
			gameObject.name = Path.GetFileNameWithoutExtension(name);
		}
		assetScenePrefabs[sceneName] = dictionary;
		AssetSceneUtil.Log($"Asset scene {sceneName} root objects loaded: {rootGameObjects.Length}");
	}

	protected override T LoadAsset<T>(string filePath)
	{
		if (filePath.EndsWith(".prefab", StringComparison.OrdinalIgnoreCase))
		{
			if (prefabToAssetSceneName.TryGetValue(filePath, out var value))
			{
				if (assetScenePrefabs.TryGetValue(value, out var value2) && value2.TryGetValue(filePath, out var value3))
				{
					return value3 as T;
				}
				UnityEngine.Debug.LogError((GetAssetSceneProgress(value) < 1f) ? ("Prefab '" + filePath + "' requires asset scene '" + value + "' to be loaded first.") : ("Prefab '" + filePath + "' is supposed to be in asset scene '" + value + "' but it was not found."));
			}
			else
			{
				AssetSceneUtil.Log("Prefab '" + filePath + "' not found in any asset scenes.");
			}
			return null;
		}
		if (!files.TryGetValue(filePath, out var value4))
		{
			return null;
		}
		return value4.LoadAsset<T>(filePath);
	}

	protected override string[] LoadAssetList(string folder, string search)
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, Dictionary<string, GameObject>> assetScenePrefab in assetScenePrefabs)
		{
			assetScenePrefab.Deconstruct(out var _, out var value);
			foreach (KeyValuePair<string, GameObject> item in value.Where((KeyValuePair<string, GameObject> x) => x.Key.StartsWith(folder, StringComparison.OrdinalIgnoreCase)))
			{
				if (string.IsNullOrEmpty(search) || item.Key.IndexOf(search, StringComparison.OrdinalIgnoreCase) != -1)
				{
					list.Add(item.Key);
				}
			}
		}
		foreach (KeyValuePair<string, AssetBundle> item2 in files.Where((KeyValuePair<string, AssetBundle> x) => x.Key.StartsWith(folder, StringComparison.OrdinalIgnoreCase)))
		{
			if (string.IsNullOrEmpty(search) || item2.Key.IndexOf(search, StringComparison.OrdinalIgnoreCase) != -1)
			{
				list.Add(item2.Key);
			}
		}
		list.Sort(StringComparer.OrdinalIgnoreCase);
		return list.ToArray();
	}

	public override T[] LoadAllFromBundle<T>(string bundleName, string editorSearch)
	{
		foreach (KeyValuePair<string, AssetBundle> bundle in bundles)
		{
			if (bundle.Key.EndsWith(bundleName))
			{
				return bundle.Value.LoadAllAssets<T>();
			}
		}
		throw new Exception("LoadAllFromBundle found none");
	}

	public override bool HasAsset(string path)
	{
		if (!assetScenePrefabs.ContainsKey(path))
		{
			return files.ContainsKey(path);
		}
		return true;
	}
}
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
		Current = JsonConvert.DeserializeObject<AssetSceneManifest>(File.ReadAllText((UnityEngine.Application.platform == RuntimePlatform.OSXPlayer) ? Path.Combine(UnityEngine.Application.dataPath, "../../Bundles/", "AssetSceneManifest.json") : Path.Combine(UnityEngine.Application.dataPath, "../Bundles/", "AssetSceneManifest.json")));
		if (Current == null)
		{
			Current = new AssetSceneManifest();
		}
		Current.Initialize();
	}
}
public static class AssetSceneUtil
{
	public static void Log(string message)
	{
	}
}
public static class FileSystem
{
	public static bool LogDebug;

	public static bool LogTime;

	public static FileSystemBackend Backend;

	public static bool IsBundled
	{
		get
		{
			if (UnityEngine.Application.isPlaying)
			{
				return Backend is AssetBundleBackend;
			}
			return false;
		}
	}

	public static GameObject[] LoadPrefabs(string folder)
	{
		return Backend.LoadPrefabs(folder);
	}

	public static GameObject LoadPrefab(string filePath)
	{
		return Backend.LoadPrefab(filePath);
	}

	public static string[] FindAll(string folder, string search = "")
	{
		return Backend.FindAll(folder, search);
	}

	public static T[] LoadAll<T>(string folder, string search = "") where T : UnityEngine.Object
	{
		if (!folder.IsLower())
		{
			folder = folder.ToLower();
		}
		return Backend.LoadAll<T>(folder, search);
	}

	public static T[] LoadAllFromBundle<T>(string bundleName, string editorSearch) where T : UnityEngine.Object
	{
		return Backend.LoadAllFromBundle<T>(bundleName, editorSearch);
	}

	public static T Load<T>(string filePath, bool complain = true) where T : UnityEngine.Object
	{
		if (!filePath.IsLower())
		{
			filePath = filePath.ToLower();
		}
		Stopwatch stopwatch = Stopwatch.StartNew();
		if (LogDebug)
		{
			File.AppendAllText("filesystem_debug.csv", $"{filePath}\n");
		}
		T val = Backend.Load<T>(filePath);
		if (complain && val == null)
		{
			UnityEngine.Debug.LogWarning("[FileSystem] Not Found: " + filePath + " (" + typeof(T)?.ToString() + ")");
		}
		if (LogTime)
		{
			File.AppendAllText("filesystem.csv", $"{filePath},{stopwatch.Elapsed.TotalMilliseconds}\n");
		}
		return val;
	}

	public static bool HasAsset(string filePath)
	{
		return Backend.HasAsset(filePath);
	}
}
public abstract class FileSystemBackend
{
	public bool isError;

	public string loadingError = "";

	public Dictionary<string, UnityEngine.Object> cache = new Dictionary<string, UnityEngine.Object>(StringComparer.OrdinalIgnoreCase);

	public GameObject[] LoadPrefabs(string folder)
	{
		if (!folder.EndsWith("/", StringComparison.CurrentCultureIgnoreCase))
		{
			UnityEngine.Debug.LogWarning("LoadPrefabs - folder should end in '/' - " + folder);
		}
		if (!folder.StartsWith("assets/", StringComparison.CurrentCultureIgnoreCase))
		{
			UnityEngine.Debug.LogWarning("LoadPrefabs - should start with assets/ - " + folder);
		}
		return LoadAll<GameObject>(folder, ".prefab");
	}

	public GameObject LoadPrefab(string filePath)
	{
		if (!filePath.StartsWith("assets/", StringComparison.CurrentCultureIgnoreCase))
		{
			UnityEngine.Debug.LogWarning("LoadPrefab - should start with assets/ - " + filePath);
		}
		return Load<GameObject>(filePath);
	}

	public string[] FindAll(string folder, string search = "")
	{
		return LoadAssetList(folder, search);
	}

	public T[] LoadAll<T>(string folder, string search = "") where T : UnityEngine.Object
	{
		List<T> list = new List<T>();
		string[] array = FindAll(folder, search);
		foreach (string filePath in array)
		{
			T val = Load<T>(filePath);
			if (val != null)
			{
				list.Add(val);
			}
		}
		return list.ToArray();
	}

	public T Load<T>(string filePath) where T : UnityEngine.Object
	{
		T val = null;
		if (cache.ContainsKey(filePath))
		{
			val = cache[filePath] as T;
		}
		else
		{
			val = LoadAsset<T>(filePath);
			if (val != null)
			{
				cache.Add(filePath, val);
			}
		}
		return val;
	}

	protected void LoadError(string err)
	{
		UnityEngine.Debug.LogError(err);
		loadingError = err;
		isError = true;
	}

	public virtual List<string> UnloadBundles(string partialName)
	{
		return new List<string>(0);
	}

	protected abstract T LoadAsset<T>(string filePath) where T : UnityEngine.Object;

	protected abstract string[] LoadAssetList(string folder, string search);

	public abstract T[] LoadAllFromBundle<T>(string bundleName, string editorSearch) where T : UnityEngine.Object;

	public abstract bool HasAsset(string path);
}
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
			FilePathsData = new byte[292]
			{
				0, 0, 0, 1, 0, 0, 0, 53, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 82, 117, 115, 116, 46, 70,
				105, 108, 101, 83, 121, 115, 116, 101, 109, 92,
				65, 115, 115, 101, 116, 66, 117, 110, 100, 108,
				101, 66, 97, 99, 107, 101, 110, 100, 46, 99,
				115, 0, 0, 0, 2, 0, 0, 0, 53, 92,
				65, 115, 115, 101, 116, 115, 92, 80, 108, 117,
				103, 105, 110, 115, 92, 82, 117, 115, 116, 46,
				70, 105, 108, 101, 83, 121, 115, 116, 101, 109,
				92, 65, 115, 115, 101, 116, 83, 99, 101, 110,
				101, 77, 97, 110, 105, 102, 101, 115, 116, 46,
				99, 115, 0, 0, 0, 1, 0, 0, 0, 49,
				92, 65, 115, 115, 101, 116, 115, 92, 80, 108,
				117, 103, 105, 110, 115, 92, 82, 117, 115, 116,
				46, 70, 105, 108, 101, 83, 121, 115, 116, 101,
				109, 92, 65, 115, 115, 101, 116, 83, 99, 101,
				110, 101, 85, 116, 105, 108, 46, 99, 115, 0,
				0, 0, 1, 0, 0, 0, 45, 92, 65, 115,
				115, 101, 116, 115, 92, 80, 108, 117, 103, 105,
				110, 115, 92, 82, 117, 115, 116, 46, 70, 105,
				108, 101, 83, 121, 115, 116, 101, 109, 92, 70,
				105, 108, 101, 83, 121, 115, 116, 101, 109, 46,
				99, 115, 0, 0, 0, 1, 0, 0, 0, 52,
				92, 65, 115, 115, 101, 116, 115, 92, 80, 108,
				117, 103, 105, 110, 115, 92, 82, 117, 115, 116,
				46, 70, 105, 108, 101, 83, 121, 115, 116, 101,
				109, 92, 70, 105, 108, 101, 83, 121, 115, 116,
				101, 109, 66, 97, 99, 107, 101, 110, 100, 46,
				99, 115
			},
			TypesData = new byte[136]
			{
				0, 0, 0, 0, 19, 124, 65, 115, 115, 101,
				116, 66, 117, 110, 100, 108, 101, 66, 97, 99,
				107, 101, 110, 100, 0, 0, 0, 0, 19, 124,
				65, 115, 115, 101, 116, 83, 99, 101, 110, 101,
				77, 97, 110, 105, 102, 101, 115, 116, 0, 0,
				0, 0, 24, 65, 115, 115, 101, 116, 83, 99,
				101, 110, 101, 77, 97, 110, 105, 102, 101, 115,
				116, 124, 69, 110, 116, 114, 121, 0, 0, 0,
				0, 15, 124, 65, 115, 115, 101, 116, 83, 99,
				101, 110, 101, 85, 116, 105, 108, 0, 0, 0,
				0, 11, 124, 70, 105, 108, 101, 83, 121, 115,
				116, 101, 109, 0, 0, 0, 0, 18, 124, 70,
				105, 108, 101, 83, 121, 115, 116, 101, 109, 66,
				97, 99, 107, 101, 110, 100
			},
			TotalFiles = 5,
			TotalTypes = 6,
			IsEditorOnly = false
		};
	}
}
