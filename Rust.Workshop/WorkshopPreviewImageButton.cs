using System.Collections;
using System.IO;
using Facepunch.Extend;
using Rust.UI;
using Rust.Workshop;
using Rust.Workshop.Editor;
using UnityEngine;
using UnityEngine.UI;

public class WorkshopPreviewImageButton : MonoBehaviour
{
	public RawImage TextureImage;

	public RustText FileLabel;

	public GameObject FileSizeWarning;

	[HideInInspector]
	public Publisher publisher;

	protected WorkshopItemEditor Editor => SingletonComponent<WorkshopItemEditor>.Instance;

	public FileInfo file { get; private set; }

	public Texture texture { get; private set; }

	public void SetFile(FileInfo file)
	{
		this.file = file;
		SetTexture(Editor.SetTexture("CustomIcon", this.file.FullName, isNormalMap: false, skipSettingMaterialTexture: true));
		if (texture != null)
		{
			FileSizeWarning.SetActive(texture.width >= 2048 || texture.height >= 2048);
		}
	}

	public void SetTexture(Texture texture)
	{
		this.texture = texture;
		if (texture == null)
		{
			FileLabel.text = "None";
			TextureImage.texture = null;
		}
		else
		{
			FileLabel.text = texture.name.TruncateFilename(48);
			FileLabel.text += $" ({texture.width} x {texture.height})";
			TextureImage.texture = texture;
		}
	}

	public void OpenFileBrowser()
	{
		StartCoroutine(BrowseForPreview());
	}

	public IEnumerator BrowseForPreview()
	{
		yield return StartCoroutine(Editor.FileDialog.Open(null, ".png|.jpg"));
		if (Editor.FileDialog.result == null)
		{
			yield break;
		}
		try
		{
			SetFile(new FileInfo(Editor.FileDialog.result));
		}
		catch
		{
			Debug.LogError("Error loading custom preview file from dialog.");
		}
	}

	public void Clear()
	{
		if (publisher != null)
		{
			publisher.RemoveCustomPreview(this);
		}
		Object.Destroy(base.gameObject);
	}
}
