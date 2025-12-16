using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Facepunch;
using Facepunch.Extend;
using Facepunch.Utility;
using Newtonsoft.Json;
using Rust.Components.Camera;
using Rust.UI;
using Rust.Workshop.Editor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Rust.Workshop;

public class WorkshopItemEditor : SingletonComponent<WorkshopItemEditor>
{
	public static Action<bool, string> OnLoading;

	private Publisher _publisher;

	public Transform ItemSelectionButtonsParent;

	public WorkshopItemSelectionButton ItemSelectionButtonPrefab;

	public RustText SelectedItemTypeLabel;

	public Scrollbar ItemSelectionScrollbar;

	public InputField ItemTitleLabel;

	public WorkshopViewmodelControls ViewmodelControls;

	public GameObject MaterialTabHolder;

	public GameObject FileDialogObject;

	public GameObject[] EditorElements;

	public GameObject[] SpecularOnlyElements;

	public GameObject[] MetalnessOnlyElements;

	public GameObject[] ClothOnlyElements;

	public GameObject[] DetailOnlyElements;

	public GameObject[] DirtOnlyElements;

	public GameObject MaterialInformationPanel;

	public List<RustText> TextureResolutionInfoRows;

	public RustText ShortName;

	private Toggle[] MaterialTabs;

	[HideInInspector]
	public int EditingMaterial;

	[FormerlySerializedAs("item_position_a")]
	public GameObject ItemSpawnPositionAnchor;

	private List<WorkshopItemSelectionButton> selectionButtons = new List<WorkshopItemSelectionButton>();

	private bool dofToggled = true;

	private bool hiddenPlayerModel;

	private ulong playerSeed = 585364905uL;

	public static Action<GameObject> PrefabPreviewSpawned = null;

	private static readonly Regex ModelCleanup = new Regex("(_mesh)?(_LOD0)?$", RegexOptions.IgnoreCase);

	internal GameObject Prefab { get; set; }

	internal GameObject ViewModel { get; set; }

	internal Skinnable Skinnable { get; set; }

	internal Skin Skin { get; set; }

	internal ulong ItemId { get; set; }

	protected WorkshopInterface Interface => GetComponentInParent<WorkshopInterface>();

	internal Publisher Publisher
	{
		get
		{
			if (_publisher == null)
			{
				_publisher = GetComponentInChildren<Publisher>(includeInactive: true);
			}
			return _publisher;
		}
	}

	public FileDialog FileDialog => FileDialogObject.GetComponent<FileDialog>();

	public string ItemTitle
	{
		get
		{
			return ItemTitleLabel.text;
		}
		set
		{
			ItemTitleLabel.text = value;
		}
	}

	public string ChangeLog
	{
		get
		{
			return Publisher.ChangeLog.text;
		}
		set
		{
			Publisher.ChangeLog.text = value;
		}
	}

	private bool isPlayerModelFemale => GetRandomFloatBasedOnUserID(playerSeed, 4332uL) > 0.5f;

	private string SkinnableEntityPrefabName
	{
		get
		{
			if (string.IsNullOrEmpty(Skinnable.EntityFemalePrefabName) || !isPlayerModelFemale)
			{
				return Skinnable.EntityPrefabName;
			}
			return Skinnable.EntityFemalePrefabName;
		}
	}

	internal static void Loading(bool v1, string v2, string v3, float v4)
	{
		OnLoading?.Invoke(v1, v2);
	}

	private void OnEnable()
	{
		if (selectionButtons == null)
		{
			selectionButtons = new List<WorkshopItemSelectionButton>();
		}
		foreach (WorkshopItemSelectionButton selectionButton in selectionButtons)
		{
			if (selectionButton != null)
			{
				UnityEngine.Object.Destroy(selectionButton);
			}
		}
		selectionButtons.Clear();
		foreach (string item in (from x in Skinnable.All
			where !x.HideInWorkshopUpload
			select x.Name into x
			orderby x
			select x).ToList())
		{
			WorkshopItemSelectionButton workshopItemSelectionButton = UnityEngine.Object.Instantiate(ItemSelectionButtonPrefab, ItemSelectionButtonsParent);
			workshopItemSelectionButton.SetItemType(item, this);
			selectionButtons.Add(workshopItemSelectionButton);
		}
		MaterialTabs = MaterialTabHolder.GetComponentsInChildren<Toggle>(includeInactive: true);
		MaterialInformationPanel.SetActive(value: false);
	}

	private void ClearEditor()
	{
		if (Prefab != null)
		{
			UnityEngine.Object.Destroy(Prefab);
			Prefab = null;
		}
		if (ViewModel != null)
		{
			UnityEngine.Object.Destroy(ViewModel);
			ViewModel = null;
			ViewmodelControls.Clear();
		}
		ItemId = 0uL;
		GetComponent<WorkshopPlayerPreview>().Cleanup();
		ItemTitle = "";
		Skinnable = null;
		ChangeLog = "";
		Publisher.ClearCustomIcon();
		Publisher.ClearCustomPreviewImages();
		GetComponentInChildren<WorkshopView>(includeInactive: true).Clear();
	}

	public void StartNewItem(string type = "TShirt")
	{
		Skin = null;
		ClearEditor();
		LoadItemType(type);
		OnImportFinished();
		ShowEditor();
		SelectedItemTypeLabel.text = type;
	}

	public IEnumerator StartViewingItem(IWorkshopContent item)
	{
		Skin = null;
		ClearEditor();
		HideEditor();
		yield return StartCoroutine(OpenItem(item));
		OnImportFinished();
	}

	public IEnumerator StartEditingItem(IWorkshopContent item)
	{
		Skin = null;
		ClearEditor();
		ShowEditor();
		yield return StartCoroutine(OpenItem(item));
		OnImportFinished();
	}

	private void HideEditor()
	{
		GameObject[] editorElements = EditorElements;
		for (int i = 0; i < editorElements.Length; i++)
		{
			editorElements[i].SetActive(value: false);
		}
	}

	private void ShowEditor()
	{
		GameObject[] editorElements = EditorElements;
		for (int i = 0; i < editorElements.Length; i++)
		{
			editorElements[i].SetActive(value: true);
		}
	}

	internal IEnumerator OpenItem(IWorkshopContent item)
	{
		if (!LoadItemType(item.Tags))
		{
			Debug.Log("Couldn't LoadItemType (" + string.Join(";", item.Tags) + ")");
			ClearEditor();
			Loading(v1: false, "", "", 0f);
		}
		yield break;
	}

	private void OnImportFinished()
	{
		if (Skin == null)
		{
			GetComponentInChildren<WorkshopView>(includeInactive: true).UpdateFrom(null);
			return;
		}
		EditingMaterial = 0;
		Skin.Skinnable = Skinnable;
		Skin.Apply(Prefab);
		UpdateMaterialRows();
		InitScene();
	}

	internal Texture2D SetTexture(string paramName, string fullName, bool isNormalMap, bool skipSettingMaterialTexture = false)
	{
		byte[] array = File.ReadAllBytes(fullName);
		if (array == null)
		{
			throw new Exception("Couldn't Load Data");
		}
		Texture2D texture2D = new Texture2D(2, 2, TextureFormat.ARGB32, mipChain: true, isNormalMap);
		if (!texture2D.LoadImage(array))
		{
			throw new Exception("Couldn't Load Image");
		}
		texture2D.name = fullName;
		texture2D = Facepunch.Utility.Texture.LimitSize(texture2D, Skinnable.Groups[EditingMaterial].MaxTextureSize, Skinnable.Groups[EditingMaterial].MaxTextureSize, isNormalMap);
		texture2D.anisoLevel = 16;
		texture2D.filterMode = FilterMode.Trilinear;
		if (!skipSettingMaterialTexture)
		{
			SetTexture(paramName, texture2D);
		}
		return texture2D;
	}

	internal void SetFloat(string paramName, float value)
	{
		Skin.Materials[EditingMaterial].SetFloat(paramName, value);
		if (paramName == "_Cutoff")
		{
			if (value > 0.1f)
			{
				Skin.Materials[EditingMaterial].SetOverrideTag("RenderType", "TransparentCutout");
				Skin.Materials[EditingMaterial].EnableKeyword("_ALPHATEST_ON");
				Skin.Materials[EditingMaterial].renderQueue = 2450;
			}
			else
			{
				Skin.Materials[EditingMaterial].SetOverrideTag("RenderType", "");
				Skin.Materials[EditingMaterial].DisableKeyword("_ALPHATEST_ON");
				Skin.Materials[EditingMaterial].renderQueue = -1;
			}
		}
		if (paramName == "_MicrofiberFuzzIntensity")
		{
			if (value > 0.1f)
			{
				Skin.Materials[EditingMaterial].EnableKeyword("_MICROFIBERFUZZLAYER_ON");
			}
			else
			{
				Skin.Materials[EditingMaterial].DisableKeyword("_MICROFIBERFUZZLAYER_ON");
			}
		}
	}

	internal void SetColor(string paramName, Color val)
	{
		Skin.Materials[EditingMaterial].SetColor(paramName, val);
	}

	internal void SetTexture(string paramName, UnityEngine.Texture tex)
	{
		Skin.Materials[EditingMaterial].SetTexture(paramName, tex);
		if (paramName == "_EmissionMap" && tex != null)
		{
			Skin.Materials[EditingMaterial].EnableKeyword("_EMISSION");
		}
	}

	private bool LoadItemType(IEnumerable<string> tags)
	{
		foreach (string tag in tags)
		{
			if (LoadItemType(tag))
			{
				return true;
			}
		}
		return false;
	}

	private bool LoadItemType(string v)
	{
		ClearEditor();
		Skinnable = Skinnable.FindForItem(v);
		if (Skinnable == null)
		{
			return false;
		}
		Prefab = LoadForPreview(SkinnableEntityPrefabName);
		PrefabPreviewSpawned?.Invoke(Prefab);
		FocusCameraOnPrefab();
		if (Skin == null)
		{
			Skin = new Skin();
		}
		Skin.Skinnable = Skinnable;
		Skin.ReadDefaults();
		return true;
	}

	public void FocusCameraOnPrefab(float addedDistance = 0f)
	{
		float workshopDefaultZoom = Skinnable.WorkshopDefaultZoom;
		Vector3 lookDirection = Quaternion.Euler(Skinnable.WorkshopDefaultRotationOffset) * new Vector3(0.3f, 0.5f, 1f);
		Camera.main.FocusOnRenderer(Prefab, lookDirection, Vector3.up, -1, workshopDefaultZoom + addedDistance);
	}

	private GameObject LoadForPreview(string entityPrefabName, bool preprocess = true)
	{
		GameObject gameObject = (preprocess ? Global.CreatePrefab(entityPrefabName) : UnityEngine.Object.Instantiate(Global.LoadPrefab(entityPrefabName)));
		RemoveComponents<Rigidbody>(gameObject);
		gameObject.transform.position = ItemSpawnPositionAnchor.transform.position;
		gameObject.transform.rotation = ItemSpawnPositionAnchor.transform.rotation;
		gameObject.SetActive(value: true);
		if (preprocess)
		{
			gameObject.AddComponent<DepthOfFieldFocusPoint>().enabled = dofToggled;
		}
		return gameObject;
	}

	public void SelectItemType(string type)
	{
		LoadItemType(type);
		EditingMaterial = 0;
		UpdateMaterialRows();
		InitScene();
		SelectedItemTypeLabel.text = type;
	}

	public void SearchItemTypes(string search)
	{
		search = search.ToLower().Trim();
		foreach (WorkshopItemSelectionButton selectionButton in selectionButtons)
		{
			if (string.IsNullOrEmpty(search) || selectionButton.ItemIdentifier.ToLower().Contains(search))
			{
				selectionButton.gameObject.SetActive(value: true);
			}
			else
			{
				selectionButton.gameObject.SetActive(value: false);
			}
		}
		if (ItemSelectionScrollbar != null)
		{
			ItemSelectionScrollbar.value = 1f;
		}
	}

	private void UpdateMaterialRows()
	{
		UpdateMaterialTabs();
		UpdateMaterialInformation();
		Material material = Skin.Materials[EditingMaterial];
		if (material == null)
		{
			return;
		}
		Material material2 = Skin.DefaultMaterials[EditingMaterial];
		if (material2 == null)
		{
			return;
		}
		if (!material.IsKeywordEnabled("_ALPHATEST_ON"))
		{
			material.SetFloat("_Cutoff", 0f);
		}
		if (!material2.IsKeywordEnabled("_ALPHATEST_ON"))
		{
			material2.SetFloat("_Cutoff", 0f);
		}
		MaterialRow[] componentsInChildren = GetComponentsInChildren<MaterialRow>(includeInactive: true);
		foreach (MaterialRow materialRow in componentsInChildren)
		{
			if (material.HasProperty(materialRow.ParamName))
			{
				materialRow.Read(material, material2);
			}
		}
		bool flag = material.shader.name.Contains("Specular") || (material.HasInt("_MaterialType") && material.GetInt("_MaterialType") == 1);
		GameObject[] specularOnlyElements = SpecularOnlyElements;
		for (int i = 0; i < specularOnlyElements.Length; i++)
		{
			specularOnlyElements[i].SetActive(flag);
		}
		specularOnlyElements = MetalnessOnlyElements;
		for (int i = 0; i < specularOnlyElements.Length; i++)
		{
			specularOnlyElements[i].SetActive(!flag);
		}
		bool active = material.shader.name.Contains("Cloth");
		specularOnlyElements = ClothOnlyElements;
		for (int i = 0; i < specularOnlyElements.Length; i++)
		{
			specularOnlyElements[i].SetActive(active);
		}
		bool active2 = material2.IsKeywordEnabled("_DETAILLAYER_ON");
		specularOnlyElements = DetailOnlyElements;
		for (int i = 0; i < specularOnlyElements.Length; i++)
		{
			specularOnlyElements[i].SetActive(active2);
		}
		bool active3 = material2.IsKeywordEnabled("_DIRTLAYER_ON");
		specularOnlyElements = DirtOnlyElements;
		for (int i = 0; i < specularOnlyElements.Length; i++)
		{
			specularOnlyElements[i].SetActive(active3);
		}
	}

	private void UpdateMaterialTabs()
	{
		for (int i = 0; i < MaterialTabs.Length; i++)
		{
			if (Skinnable.Groups.Length < i + 1)
			{
				MaterialTabs[i].gameObject.SetActive(value: false);
				continue;
			}
			MaterialTabs[i].gameObject.SetActive(value: true);
			Text[] componentsInChildren = MaterialTabs[i].gameObject.GetComponentsInChildren<Text>(includeInactive: true);
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				componentsInChildren[j].text = Skinnable.Groups[i].Name;
			}
		}
	}

	private void UpdateMaterialInformation()
	{
		for (int i = 0; i < TextureResolutionInfoRows.Count; i++)
		{
			if (Skinnable.Groups.Length < i + 1)
			{
				TextureResolutionInfoRows[i].gameObject.SetActive(value: false);
				continue;
			}
			TextureResolutionInfoRows[i].gameObject.SetActive(value: true);
			TextureResolutionInfoRows[i].text = $"{Skinnable.Groups[i].Name}: ( {Skinnable.Groups[i].MaxTextureSize} x {Skinnable.Groups[i].MaxTextureSize} )";
		}
		ShortName.text = Skinnable.ItemName;
	}

	private void InitScene()
	{
		if (Skinnable.Category != Category.Deployable)
		{
			InitPlayerPreview(585364905uL, focus: true, hiddenPlayerModel);
			if (Prefab != null && Skinnable != null)
			{
				UnityEngine.Object.Destroy(Prefab);
				Prefab = LoadForPreview(SkinnableEntityPrefabName, preprocess: false);
				Prefab.transform.position = new Vector3(0f, 500f, 0f);
				Skin.Apply(Prefab);
			}
		}
		else
		{
			Skin.Apply(Prefab);
		}
		if ((bool)Skinnable.ViewmodelPrefab)
		{
			ViewModel = Global.CreatePrefab(Skinnable.ViewmodelPrefabName);
			ViewModel.transform.position = Camera.main.transform.position;
			ViewModel.transform.rotation = Camera.main.transform.rotation;
			ViewModel.SetActive(value: true);
			Skin.Apply(ViewModel);
		}
	}

	private void InitPlayerPreview(ulong playerid, bool focus, bool invisible = false)
	{
		playerSeed = playerid;
		GameObject gameObject = Global.CreatePrefab(SkinnableEntityPrefabName);
		gameObject.AddComponent<DepthOfFieldFocusPoint>().enabled = dofToggled;
		gameObject.SetActive(value: true);
		Skin.Skinnable = Skinnable;
		Skin.Apply(gameObject);
		GetComponent<WorkshopPlayerPreview>().Setup(gameObject, playerid, focus, Skinnable.Category != Category.Weapon && Skinnable.Category != Category.Misc && Skinnable.Category != Category.Deployable, Skinnable.WorkshopDefaultZoom, Skinnable.WorkshopDefaultRotationOffset, invisible);
	}

	public static float GetRandomFloatBasedOnUserID(ulong steamid, ulong seed)
	{
		UnityEngine.Random.State state = UnityEngine.Random.state;
		UnityEngine.Random.InitState((int)(seed + steamid));
		float result = UnityEngine.Random.Range(0f, 1f);
		UnityEngine.Random.state = state;
		return result;
	}

	public void RandomizePlayerPreview()
	{
		if (Skinnable.Category != Category.Deployable)
		{
			ulong playerid = (ulong)UnityEngine.Random.Range(0, int.MaxValue);
			InitPlayerPreview(playerid, focus: false, hiddenPlayerModel);
		}
	}

	public void DownloadModel()
	{
		StartCoroutine(DoDownloadModel());
	}

	private static void RemoveComponents<T>(GameObject prefab) where T : Component
	{
		T[] componentsInChildren = prefab.GetComponentsInChildren<T>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			UnityEngine.Object.Destroy(componentsInChildren[i]);
		}
	}

	private IEnumerator DoDownloadModel()
	{
		yield return StartCoroutine(FileDialog.Save(null, ".obj", "download_file", "", saveLastPath: true, Skinnable.ItemName));
		if (string.IsNullOrEmpty(FileDialog.result))
		{
			yield break;
		}
		Debug.Log("Save Obj to " + FileDialog.result);
		for (int i = 0; i < Skinnable.MeshDownloadPaths.Length; i++)
		{
			string text = Path.Combine(UnityEngine.Application.streamingAssetsPath, Skinnable.MeshDownloadPaths[i]);
			string text2 = FileDialog.result;
			if (Skinnable.MeshDownloadPaths.Length > 1)
			{
				string extension = Path.GetExtension(text);
				string text3 = ModelCleanup.Replace(Path.GetFileNameWithoutExtension(text), "");
				string directoryName = Path.GetDirectoryName(text2);
				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(text2);
				text2 = Path.Combine(directoryName, fileNameWithoutExtension + "_" + text3 + extension);
			}
			File.Copy(text, text2, overwrite: true);
		}
	}

	private void Update()
	{
		Input.Frame();
		Input.Update();
	}

	private void LateUpdate()
	{
		ViewmodelControls.DoUpdate(ViewModel);
	}

	public void SwitchMaterial(int i)
	{
		EditingMaterial = i;
		UpdateMaterialRows();
	}

	public void ToggleDepthOfField(bool toggle)
	{
		dofToggled = toggle;
		DepthOfFieldFocusPoint[] array = UnityEngine.Object.FindObjectsOfType<DepthOfFieldFocusPoint>();
		foreach (DepthOfFieldFocusPoint depthOfFieldFocusPoint in array)
		{
			if (depthOfFieldFocusPoint != null)
			{
				depthOfFieldFocusPoint.enabled = dofToggled;
			}
		}
	}

	public void HidePlayerModel(bool toggle)
	{
		hiddenPlayerModel = toggle;
		if (Skinnable.Category != Category.Deployable)
		{
			InitPlayerPreview(playerSeed, focus: false, hiddenPlayerModel);
		}
	}

	private Skin.Manifest LoadManifestFile(string folder)
	{
		string text = null;
		if (folder != null)
		{
			string path = folder + "/manifest.txt";
			if (File.Exists(path))
			{
				try
				{
					text = File.ReadAllText(path);
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
			}
			if (text != null)
			{
				return JsonConvert.DeserializeObject<Skin.Manifest>(text);
			}
		}
		Debug.LogWarning("Error while loading the manifest file from folder: " + folder + " (does the file exist?)");
		return null;
	}

	private void ApplyFromManifest(string folder, Skin.Manifest manifest)
	{
		if (manifest == null || manifest.Groups == null)
		{
			return;
		}
		EditingMaterial = 0;
		Skin.Manifest.Group[] groups = manifest.Groups;
		foreach (Skin.Manifest.Group obj in groups)
		{
			foreach (KeyValuePair<string, string> texture in obj.Textures)
			{
				if (string.IsNullOrEmpty(texture.Value) || texture.Value == "none")
				{
					SetTexture(texture.Key, null);
					continue;
				}
				string fullName = folder + "/" + texture.Value;
				bool isNormalMap = texture.Key.Contains("BumpMap");
				SetTexture(texture.Key, fullName, isNormalMap);
			}
			foreach (KeyValuePair<string, Skin.Manifest.ColorEntry> color in obj.Colors)
			{
				SetColor(val: new Color(color.Value.r, color.Value.g, color.Value.b), paramName: color.Key);
			}
			foreach (KeyValuePair<string, float> @float in obj.Floats)
			{
				SetFloat(@float.Key, @float.Value);
			}
			EditingMaterial++;
		}
	}

	private IEnumerator DoBulkImport()
	{
		yield return StartCoroutine(FileDialog.Open(null, null, "filedialog.selectimportfolder", "", saveLastPath: false, canSelectFolder: true));
		if (!string.IsNullOrEmpty(FileDialog.result))
		{
			Skin.Manifest manifest = LoadManifestFile(FileDialog.result);
			if (manifest == null)
			{
				Debug.LogError("No valid manifest found in folder: " + FileDialog.result + " (make sure the manifest.txt file exists)");
			}
			else
			{
				ApplyFromManifest(FileDialog.result, manifest);
			}
		}
	}

	public void BulkImport()
	{
		StartCoroutine(DoBulkImport());
	}
}
