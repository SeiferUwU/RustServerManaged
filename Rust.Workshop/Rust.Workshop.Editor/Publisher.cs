using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Facepunch.Extend;
using Facepunch.Utility;
using Newtonsoft.Json;
using Rust.UI;
using Steamworks;
using Steamworks.Data;
using Steamworks.Ugc;
using UnityEngine;
using UnityEngine.UI;

namespace Rust.Workshop.Editor;

public class Publisher : MonoBehaviour
{
	public InputField Description;

	public InputField ChangeLog;

	public Button PublishButton;

	public Texture2D IconBackground;

	public static Action PreIconRender;

	public static Action PostIconRender;

	[Header("Screenshot Anchor References")]
	public List<Transform> DeployableScreenshotAnchors;

	public List<Transform> ClothingScreenshotAnchors;

	public List<Transform> WeaponScreenshotAnchors;

	public Transform FirstPersonViewScreenshotAnchor;

	[Header("Scene Control References")]
	public GameObject screenshotCamera;

	public RustButton flashlightToggle;

	public RustButton rainToggle;

	public RustButton fogToggle;

	public RustButton spotlightToggle;

	public RustButton depthOfFieldToggle;

	public RustButton hidePlayerToggle;

	public RustButton firstPersonToggle;

	public RustButton adsToggle;

	public Slider timeSlider;

	[Header("Custom Image References")]
	public RawImage customIconImage;

	public RustText customIconLabel;

	public WorkshopPreviewImageButton customPreviewButtonPrefab;

	public Transform customPreviewButtonsContainer;

	private Skinnable Skinnable;

	private Skin Skin;

	private ulong ItemId;

	private string Title;

	private GameObject Prefab;

	private List<string> AutomatedScreenshotPaths = new List<string>();

	private static readonly int API_SEND_CHUNK_SIZE = 3;

	private FileInfo customIconFile;

	private UnityEngine.Texture customIconTexture;

	private List<WorkshopPreviewImageButton> customPreviews = new List<WorkshopPreviewImageButton>();

	protected WorkshopItemEditor Editor => SingletonComponent<WorkshopItemEditor>.Instance;

	public void StartExport()
	{
		DoExport(publishToSteam: true, openFolder: false);
	}

	public void Update()
	{
		PublishButton.interactable = CanPublish();
	}

	public bool CanPublish()
	{
		if (Editor.ItemTitle.Length == 0)
		{
			return false;
		}
		return true;
	}

	private async Task DoExport(bool publishToSteam, bool openFolder, string forceFolderName = null)
	{
		_ = 1;
		try
		{
			Skinnable = Editor.Skinnable;
			Skin = Editor.Skin;
			ItemId = Editor.ItemId;
			Title = Editor.ItemTitle;
			Prefab = Editor.Prefab;
			WorkshopItemEditor.Loading(v1: true, "Exporting..", "", 0f);
			string tempFolder = Path.GetTempFileName();
			File.Delete(tempFolder);
			Directory.CreateDirectory(tempFolder);
			if (forceFolderName != null)
			{
				tempFolder = forceFolderName;
			}
			Debug.Log(tempFolder);
			await ExportToFolder(tempFolder, openFolder);
			if (publishToSteam)
			{
				await PublishToSteam(tempFolder);
			}
			if (forceFolderName != tempFolder)
			{
				Directory.Delete(tempFolder, recursive: true);
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
		finally
		{
			WorkshopItemEditor.Loading(v1: false, "", "", 0f);
		}
	}

	private async Task ExportToFolder(string folder, bool openFolder)
	{
		Skin.Manifest data = new Skin.Manifest
		{
			ItemType = Skinnable.Name,
			Version = 3,
			Groups = new Skin.Manifest.Group[Skin.Materials.Length],
			PublishDate = DateTime.UtcNow,
			AuthorId = SteamClient.SteamId
		};
		for (int i = 0; i < Skin.Materials.Length; i++)
		{
			data.Groups[i] = new Skin.Manifest.Group();
			Material mat = Skin.Materials[i];
			Skin.Manifest.Group group = data.Groups[i];
			bool isCloth = mat.shader.name.Contains("Cloth");
			bool hasDetailLayer = mat.IsKeywordEnabled("_DETAILLAYER_ON");
			bool hasDirtLayer = mat.IsKeywordEnabled("_DIRTLAYER_ON");
			bool isSpecular = mat.shader.name.Contains("Specular") || mat.GetInt("_MaterialType") == 1;
			await ExportTexture(group.Textures, folder, i, "_MainTex", mat, Skin.DefaultMaterials[i]);
			await ExportTexture(group.Textures, folder, i, "_OcclusionMap", mat, Skin.DefaultMaterials[i]);
			if (!isSpecular)
			{
				await ExportTexture(group.Textures, folder, i, "_MetallicGlossMap", mat, Skin.DefaultMaterials[i]);
			}
			else
			{
				await ExportTexture(group.Textures, folder, i, "_SpecGlossMap", mat, Skin.DefaultMaterials[i]);
			}
			await ExportTexture(group.Textures, folder, i, "_BumpMap", mat, Skin.DefaultMaterials[i], isNormalMap: true);
			await ExportTexture(group.Textures, folder, i, "_EmissionMap", mat, Skin.DefaultMaterials[i]);
			if (isCloth)
			{
				await ExportTexture(group.Textures, folder, i, "_MicrofiberFuzzMask", mat, Skin.DefaultMaterials[i]);
			}
			if (hasDetailLayer)
			{
				await ExportTexture(group.Textures, folder, i, "_DetailAlbedoMap", mat, Skin.DefaultMaterials[i]);
				await ExportTexture(group.Textures, folder, i, "_DetailMask", mat, Skin.DefaultMaterials[i]);
				await ExportTexture(group.Textures, folder, i, "_DetailNormalMap", mat, Skin.DefaultMaterials[i], isNormalMap: true);
				await ExportTexture(group.Textures, folder, i, "_DetailOcclusionMap", mat, Skin.DefaultMaterials[i]);
			}
			if (hasDirtLayer)
			{
				await ExportTexture(group.Textures, folder, i, "_DirtColor", mat, Skin.DefaultMaterials[i]);
			}
			group.Floats.Add("_Cutoff", mat.GetFloat("_Cutoff"));
			group.Floats.Add("_BumpScale", mat.GetFloat("_BumpScale"));
			group.Floats.Add("_Glossiness", mat.GetFloat("_Glossiness"));
			if (!isSpecular)
			{
				group.Floats.Add("_Metallic", mat.GetFloat("_Metallic"));
			}
			group.Floats.Add("_OcclusionStrength", mat.GetFloat("_OcclusionStrength"));
			if (isCloth)
			{
				group.Floats.Add("_MicrofiberFuzzIntensity", mat.GetFloat("_MicrofiberFuzzIntensity"));
				group.Floats.Add("_MicrofiberFuzzScatter", mat.GetFloat("_MicrofiberFuzzScatter"));
				group.Floats.Add("_MicrofiberFuzzOcclusion", mat.GetFloat("_MicrofiberFuzzOcclusion"));
			}
			if (hasDetailLayer)
			{
				group.Floats.Add("_DetailNormalMapScale", mat.GetFloat("_DetailNormalMapScale"));
				group.Floats.Add("_DetailOcclusionStrength", mat.GetFloat("_DetailOcclusionStrength"));
				group.Floats.Add("_DetailOverlaySmoothness", mat.GetFloat("_DetailOverlaySmoothness"));
				group.Floats.Add("_DetailOverlaySpecular", mat.GetFloat("_DetailOverlaySpecular"));
			}
			if (hasDirtLayer)
			{
				group.Floats.Add("_DirtAmount", mat.GetFloat("_DirtAmount"));
			}
			group.Colors.Add("_Color", new Skin.Manifest.ColorEntry(mat.GetColor("_Color")));
			if (isSpecular)
			{
				group.Colors.Add("_SpecColor", new Skin.Manifest.ColorEntry(mat.GetColor("_SpecColor")));
			}
			group.Colors.Add("_EmissionColor", new Skin.Manifest.ColorEntry(mat.GetColor("_EmissionColor")));
			if (isCloth)
			{
				group.Colors.Add("_MicrofiberFuzzColor", new Skin.Manifest.ColorEntry(mat.GetColor("_MicrofiberFuzzColor")));
			}
			if (hasDetailLayer)
			{
				group.Colors.Add("_DetailColor", new Skin.Manifest.ColorEntry(mat.GetColor("_DetailColor")));
			}
		}
		PreIconRender?.Invoke();
		await Task.Delay(TimeSpan.FromSeconds(1.0));
		WorkshopItemEditor.Loading(v1: true, "Exporting Screenshots - Icons", "", 0f);
		PropRenderer.RenderScreenshot(Prefab, folder + "/icon.png", 512, 512, 4);
		if (customIconFile == null)
		{
			CreateWorkshopIcon(folder);
		}
		else
		{
			Debug.Log("Using custom icon: " + customIconFile.FullName);
			byte[] bytes = (customIconTexture as Texture2D).EncodeToPNG();
			File.WriteAllBytes(folder + "/workshop_icon.png", bytes);
		}
		int num = 0;
		foreach (WorkshopPreviewImageButton customPreview in customPreviews)
		{
			if (!(customPreview == null) && !(customPreview.texture == null))
			{
				byte[] bytes2 = (customPreview.texture as Texture2D).EncodeToJPG();
				File.WriteAllBytes(folder + $"/custom_preview_{num}.jpg", bytes2);
				num++;
			}
		}
		PostIconRender?.Invoke();
		string contents = JsonConvert.SerializeObject(data, Formatting.Indented);
		File.WriteAllText(folder + "/manifest.txt", contents);
		if (openFolder)
		{
			Os.OpenFolder(folder);
		}
	}

	private void CreateWorkshopIcon(string folder)
	{
		Texture2D texture2D = new Texture2D(512, 512, TextureFormat.ARGB32, mipChain: false);
		texture2D.LoadImage(File.ReadAllBytes(folder + "/icon.png"));
		RenderTexture renderTexture = new RenderTexture(512, 512, 0);
		renderTexture.Blit(IconBackground);
		renderTexture.BlitWithAlphaBlending(texture2D);
		renderTexture.ToTexture(texture2D);
		File.WriteAllBytes(bytes: texture2D.EncodeToPNG(), path: folder + "/workshop_icon.png");
		UnityEngine.Object.DestroyImmediate(texture2D);
		UnityEngine.Object.DestroyImmediate(renderTexture);
	}

	private async Task PublishToSteam(string folder)
	{
		Steamworks.Ugc.Editor editor = ((ItemId != 0L) ? new Steamworks.Ugc.Editor(ItemId) : Steamworks.Ugc.Editor.NewMicrotransactionFile).WithContent(folder).WithPreviewFile(folder + "/workshop_icon.png").WithTitle(Title)
			.WithTag("Version3")
			.WithTag(Skinnable.Name)
			.WithTag("Skin")
			.WithPublicVisibility();
		if (!string.IsNullOrEmpty(Description.text))
		{
			editor = editor.WithDescription(Description.text);
		}
		if (!string.IsNullOrEmpty(ChangeLog.text))
		{
			editor = editor.WithChangeLog(ChangeLog.text);
		}
		WorkshopItemEditor.Loading(v1: true, "Publishing To Steam", "", 0f);
		PublishResult result = await editor.SubmitAsync();
		if (!result.Success)
		{
			Debug.LogError("Error: " + result.Result);
		}
		else
		{
			PublishedFileId fileId = result.FileId;
			Debug.Log("Published File: " + fileId.ToString());
		}
		Item? item = await SteamUGC.QueryFileAsync(result.FileId);
		if (!item.HasValue)
		{
			Debug.Log("Error Retrieving item information!");
			return;
		}
		if (result.Success && AutomatedScreenshotPaths != null)
		{
			List<List<string>> list = AutomatedScreenshotPaths.ChunkBy(API_SEND_CHUNK_SIZE);
			foreach (List<string> chunk in list)
			{
				editor = new Steamworks.Ugc.Editor(result.FileId);
				foreach (string item2 in chunk)
				{
					editor = editor.AddAdditionalPreviewFile(item2, ItemPreviewType.Image);
				}
				result = await editor.SubmitAsync();
				Debug.Log($"Updated Workshop Item with [{chunk.Count}] screenshots, result: [{result.Result}]");
			}
		}
		if (result.Success && customPreviews != null)
		{
			int previewIndex = 0;
			foreach (WorkshopPreviewImageButton customPreview in customPreviews)
			{
				if (!(customPreview == null) && !(customPreview.texture == null))
				{
					result = await new Steamworks.Ugc.Editor(result.FileId).AddAdditionalPreviewFile($"{folder}/custom_preview_{previewIndex}.jpg", ItemPreviewType.Image).SubmitAsync();
					previewIndex++;
					if (!result.Success)
					{
						Debug.LogError("Error adding custom preview image: " + result.Result);
						continue;
					}
					PublishedFileId fileId = result.FileId;
					Debug.Log("Added custom preview image: " + fileId.ToString());
				}
			}
		}
		Editor.ItemId = item.Value.Id;
		Editor.ItemTitle = item.Value.Title;
		ChangeLog.text = "";
		UnityEngine.Application.OpenURL(item.Value.Url);
	}

	private async Task ExportTexture(Dictionary<string, string> data, string folder, int group, string paramname, Material mat, Material defaultMat, bool isNormalMap = false)
	{
		WorkshopItemEditor.Loading(v1: true, "Exporting Texture " + paramname, "", 0f);
		UnityEngine.Texture texture = mat.GetTexture(paramname);
		UnityEngine.Texture texture2 = defaultMat.GetTexture(paramname);
		if (!(texture == texture2))
		{
			if (texture == null)
			{
				data.Add(paramname, "none");
				return;
			}
			texture = Facepunch.Utility.Texture.LimitSize((Texture2D)texture, Skinnable.Groups[group].MaxTextureSize, Skinnable.Groups[group].MaxTextureSize, isNormalMap);
			string text = string.Format("{0}{1}{2}", paramname, group, ".png");
			data.Add(paramname, text);
			texture.SaveAsPng(folder + "/" + text);
			await Task.Delay(1);
		}
	}

	public void Export()
	{
		DoExport();
	}

	public async Task DoExport()
	{
		string text = await Editor.FileDialog.SaveAsync(null, null, "save_file", "", saveLastPath: true, Editor.Skinnable?.ItemName.Replace(".", ""));
		if (text != null && !File.Exists(text))
		{
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			await DoExport(publishToSteam: false, openFolder: true, text);
		}
	}

	public void OpenFileBrowserForCustomIcon()
	{
		StartCoroutine(BrowseForCustomIcon());
	}

	public IEnumerator BrowseForCustomIcon()
	{
		yield return StartCoroutine(Editor.FileDialog.Open(null, ".png|.jpg"));
		if (Editor.FileDialog.result == null)
		{
			yield break;
		}
		try
		{
			customIconFile = new FileInfo(Editor.FileDialog.result);
			customIconTexture = Editor.SetTexture("CustomIcon", customIconFile.FullName, isNormalMap: false, skipSettingMaterialTexture: true);
			SetTextureName(customIconTexture);
			SetTexturePreview(customIconTexture);
		}
		catch
		{
			Debug.LogError("Error loading custom icon file from dialog.");
		}
	}

	public void OpenCustomIconFileLocation()
	{
		if (customIconFile != null)
		{
			Os.OpenFolder(customIconFile.DirectoryName);
		}
	}

	private void SetTextureName(UnityEngine.Texture texture)
	{
		if (texture == null)
		{
			customIconLabel.text = "None";
			return;
		}
		customIconLabel.text = texture.name.TruncateFilename(48);
		customIconLabel.text += $" ({texture.width} x {texture.height})";
	}

	private void SetTexturePreview(UnityEngine.Texture texture)
	{
		customIconImage.texture = texture;
	}

	public void ClearCustomIcon()
	{
		customIconFile = null;
		customIconTexture = null;
		SetTextureName(null);
		SetTexturePreview(null);
	}

	public void OpenFileBrowserForCustomPreview()
	{
		StartCoroutine(BrowseForCustomPreview());
	}

	public IEnumerator BrowseForCustomPreview()
	{
		yield return StartCoroutine(Editor.FileDialog.Open(null, ".png|.jpg"));
		if (Editor.FileDialog.result == null)
		{
			yield break;
		}
		try
		{
			WorkshopPreviewImageButton workshopPreviewImageButton = UnityEngine.Object.Instantiate(customPreviewButtonPrefab, customPreviewButtonsContainer);
			workshopPreviewImageButton.publisher = this;
			workshopPreviewImageButton.SetFile(new FileInfo(Editor.FileDialog.result));
			customPreviews.Add(workshopPreviewImageButton);
		}
		catch
		{
			Debug.LogError("Error loading custom preview file from dialog.");
		}
	}

	public void ClearCustomPreviewImages()
	{
		foreach (WorkshopPreviewImageButton customPreview in customPreviews)
		{
			UnityEngine.Object.Destroy(customPreview.gameObject);
		}
		customPreviews.Clear();
	}

	public void RemoveCustomPreview(WorkshopPreviewImageButton button)
	{
		customPreviews.Remove(button);
	}

	public void OpenDocumentationTips()
	{
		SafeOpenURL("https://wiki.facepunch.com/rust/Creating_Skins");
	}

	public static void SafeOpenURL(string url)
	{
		Debug.Log("Opening " + url);
		SteamFriends.OpenWebOverlay(url);
	}
}
