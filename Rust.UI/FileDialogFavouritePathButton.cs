using Facepunch.Extend;
using Rust.UI;
using UnityEngine;

public class FileDialogFavouritePathButton : MonoBehaviour
{
	[Header("Link reference, static or set through code.")]
	public FileDialog fileDialog;

	[Header("Static References")]
	public RustText pathText;

	public CanvasGroup buttonCanvasGroup;

	[Header("Settings")]
	public int buttonIndex;

	public static readonly string PLAYERPREFS_KEY_PREFIX = "files_favpath_";

	public static readonly string EMPTY_PATH = "...";

	private void Reset()
	{
		fileDialog = GetComponentInParent<FileDialog>();
		FileDialogFavouritePathButton[] componentsInChildren = base.transform.parent.GetComponentsInChildren<FileDialogFavouritePathButton>();
		buttonIndex = componentsInChildren.FindIndex(this);
		buttonCanvasGroup = GetComponent<CanvasGroup>();
		pathText = base.transform.Find("PathText")?.GetComponent<RustText>();
	}

	private void OnEnable()
	{
		Refresh();
	}

	public void Refresh()
	{
		bool flag = PlayerPrefs.HasKey(PLAYERPREFS_KEY_PREFIX + buttonIndex);
		buttonCanvasGroup.alpha = (flag ? 1f : 0.2f);
		if (!flag)
		{
			pathText.text = EMPTY_PATH;
		}
		else
		{
			pathText.text = PlayerPrefs.GetString(PLAYERPREFS_KEY_PREFIX + buttonIndex);
		}
	}

	public void SetNewPath(string path)
	{
		PlayerPrefs.SetString(PLAYERPREFS_KEY_PREFIX + buttonIndex, path);
		PlayerPrefs.Save();
		Refresh();
	}

	public void SetPathFromFileDialog()
	{
		if (!(fileDialog == null))
		{
			SetNewPath(fileDialog.workingPath);
		}
	}

	public void LoadPathToFileDialog()
	{
		if (!(fileDialog == null) && !string.IsNullOrEmpty(pathText.text) && !(pathText.text == EMPTY_PATH))
		{
			fileDialog.OpenDir(pathText.text, select: true);
		}
	}

	public void ClearPath()
	{
		if (PlayerPrefs.HasKey(PLAYERPREFS_KEY_PREFIX + buttonIndex))
		{
			PlayerPrefs.DeleteKey(PLAYERPREFS_KEY_PREFIX + buttonIndex);
			PlayerPrefs.Save();
			Refresh();
		}
	}

	public void ClickMainButton()
	{
		if (IsShortcutModifierHeld())
		{
			if (IsAlternateModifierHeld())
			{
				ClearPath();
			}
			else
			{
				SetPathFromFileDialog();
			}
		}
		else
		{
			LoadPathToFileDialog();
		}
	}

	private bool IsShortcutModifierHeld()
	{
		if (!Input.GetKey(KeyCode.LeftControl))
		{
			return Input.GetKey(KeyCode.LeftMeta);
		}
		return true;
	}

	private bool IsAlternateModifierHeld()
	{
		if (!Input.GetKey(KeyCode.LeftShift))
		{
			return Input.GetKey(KeyCode.RightShift);
		}
		return true;
	}
}
