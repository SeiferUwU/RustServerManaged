using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Rust.UI;

public class FileDialog : MonoBehaviour
{
	public enum FileDialogMode
	{
		Open,
		Save
	}

	[HideInInspector]
	public string result;

	[HideInInspector]
	public FileDialogMode mode;

	[HideInInspector]
	public bool canSelectFolder;

	[HideInInspector]
	public bool finished;

	[Header("References")]
	public RustText windowNameText;

	public InputField currentPath;

	public InputField fileName;

	public Button up;

	public Button commit;

	public Button cancel;

	public GameObject filesScrollRectContent;

	public GameObject drivesScrollRectContent;

	[Header("Lists Prefabs")]
	public GameObject filesScrollRectElement;

	public GameObject drivesScrollRectElement;

	[Header("Lists Icons")]
	public Sprite folderIcon;

	public Sprite fileIcon;

	[HideInInspector]
	public string workingPath;

	private string workingFile;

	private string[] allowedExtensions;

	private bool saveLastPath = true;

	public IEnumerator Open(string path = null, string allowedExtensions = null, string windowNameToken = "open_file", string windowNameSuffix = "", bool saveLastPath = true, bool canSelectFolder = false)
	{
		mode = FileDialogMode.Open;
		commit.GetComponentInChildren<Text>().text = "OPEN";
		fileName.text = "";
		workingPath = "";
		workingFile = "";
		result = null;
		finished = false;
		this.canSelectFolder = canSelectFolder;
		this.saveLastPath = saveLastPath;
		if (!string.IsNullOrEmpty(allowedExtensions))
		{
			allowedExtensions = allowedExtensions.ToLower();
			this.allowedExtensions = allowedExtensions.Split('|');
		}
		if (string.IsNullOrEmpty(path))
		{
			path = ((!saveLastPath) ? (Application.dataPath + "/../") : (string.IsNullOrEmpty(PlayerPrefs.GetString("OxOD.lastPath", null)) ? (Application.dataPath + "/../") : PlayerPrefs.GetString("OxOD.lastPath", null)));
		}
		windowNameText.text = Translate.Get(windowNameToken, "OPEN FILE") + windowNameSuffix;
		GoTo(path);
		base.gameObject.SetActive(value: true);
		while (!finished)
		{
			yield return new WaitForSeconds(0.1f);
		}
	}

	public IEnumerator Save(string path = null, string allowedExtensions = null, string windowNameToken = "save_file", string windowNameSuffix = "", bool saveLastPath = true, string defaultFileName = null, bool canSelectFolder = false)
	{
		mode = FileDialogMode.Save;
		commit.GetComponentInChildren<Text>().text = "SAVE";
		fileName.text = "";
		workingPath = "";
		workingFile = "";
		result = null;
		finished = false;
		this.canSelectFolder = canSelectFolder;
		this.saveLastPath = saveLastPath;
		if (!string.IsNullOrEmpty(allowedExtensions))
		{
			allowedExtensions = allowedExtensions.ToLower();
			this.allowedExtensions = allowedExtensions.Split('|');
		}
		else
		{
			this.allowedExtensions = null;
		}
		if (string.IsNullOrEmpty(path))
		{
			path = ((!saveLastPath) ? (Application.dataPath + "/../") : (string.IsNullOrEmpty(PlayerPrefs.GetString("OxOD.lastPath", null)) ? (Application.dataPath + "/../") : PlayerPrefs.GetString("OxOD.lastPath", null)));
		}
		windowNameText.text = Translate.Get(windowNameToken, "SAVE FILE") + windowNameSuffix;
		GoTo(path);
		base.gameObject.SetActive(value: true);
		if (!string.IsNullOrEmpty(defaultFileName))
		{
			OnTypedEnd(defaultFileName);
		}
		while (!finished)
		{
			yield return new WaitForSeconds(0.1f);
		}
	}

	public async Task<string> SaveAsync(string path = null, string allowedExtensions = null, string windowNameToken = "save_file", string windowNameSuffix = "", bool saveLastPath = true, string defaultFileName = "")
	{
		mode = FileDialogMode.Save;
		commit.GetComponentInChildren<Text>().text = "SAVE";
		fileName.text = defaultFileName;
		workingPath = "";
		workingFile = defaultFileName;
		result = null;
		finished = false;
		this.saveLastPath = saveLastPath;
		if (!string.IsNullOrEmpty(allowedExtensions))
		{
			allowedExtensions = allowedExtensions.ToLower();
			this.allowedExtensions = allowedExtensions.Split('|');
		}
		else
		{
			this.allowedExtensions = null;
		}
		if (string.IsNullOrEmpty(path))
		{
			path = ((!saveLastPath) ? (Application.dataPath + "/../") : (string.IsNullOrEmpty(PlayerPrefs.GetString("OxOD.lastPath", null)) ? (Application.dataPath + "/../") : PlayerPrefs.GetString("OxOD.lastPath", null)));
		}
		windowNameText.text = Translate.Get(windowNameToken, "SAVE FILE") + windowNameSuffix;
		GoTo(path);
		base.gameObject.SetActive(value: true);
		while (!finished)
		{
			await Task.Delay(100);
		}
		return result;
	}

	private void Hide()
	{
		base.gameObject.SetActive(value: false);
	}

	public void GoUp()
	{
		OpenDir(workingPath + "/../");
	}

	public void GoTo(string newPath)
	{
		if (string.IsNullOrEmpty(newPath))
		{
			return;
		}
		if (new DirectoryInfo(newPath).Exists)
		{
			OpenDir(newPath + "/");
		}
		else if (mode == FileDialogMode.Open)
		{
			if (new FileInfo(newPath).Exists)
			{
				OpenDir(new FileInfo(newPath).Directory.FullName + "/");
				SelectFile(newPath);
			}
			else
			{
				OpenDir(Application.dataPath + "/../");
			}
		}
		else if (new DirectoryInfo(new FileInfo(newPath).Directory.FullName + "/").Exists)
		{
			OpenDir(new FileInfo(newPath).Directory.FullName + "/");
			SelectFile(newPath);
		}
		else
		{
			OpenDir(Application.dataPath + "/../");
		}
	}

	public void SelectFile(string file)
	{
		if (mode == FileDialogMode.Open)
		{
			workingFile = Path.GetFullPath(file);
		}
		else
		{
			workingFile = new FileInfo(Path.GetFullPath(file)).Name;
		}
		UpdateFileInfo();
	}

	public void OnCommitClick()
	{
		if (mode == FileDialogMode.Open)
		{
			result = Path.GetFullPath(workingFile);
		}
		else
		{
			result = Path.GetFullPath(workingPath + "/" + workingFile);
		}
		finished = true;
		if (saveLastPath)
		{
			PlayerPrefs.SetString("OxOD.lastPath", workingPath);
		}
		Hide();
	}

	public void OnCancelClick()
	{
		result = null;
		finished = true;
		Hide();
	}

	public void ClearSelection()
	{
		if (mode == FileDialogMode.Open)
		{
			workingFile = "";
			UpdateFileInfo();
		}
	}

	public void OnTypedFilename(string newName)
	{
		if (mode == FileDialogMode.Open)
		{
			workingFile = (canSelectFolder ? workingPath : (workingPath + "/" + newName));
		}
		else
		{
			workingFile = newName;
		}
		UpdateFileInfo(canSelectFolder);
	}

	public void OnTypedEnd(string newName)
	{
		if (string.IsNullOrEmpty(newName))
		{
			return;
		}
		if (mode == FileDialogMode.Save)
		{
			if (allowedExtensions != null)
			{
				if (allowedExtensions.Contains(new FileInfo(newName).Extension.ToLower()))
				{
					workingFile = newName;
				}
				else
				{
					workingFile = newName + allowedExtensions[0];
				}
			}
			else
			{
				workingFile = newName;
			}
		}
		UpdateFileInfo();
	}

	public void UpdateFileInfo(bool selectsFolder = false)
	{
		if (mode == FileDialogMode.Open)
		{
			bool flag = false;
			if (selectsFolder)
			{
				try
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(workingFile + "/");
					fileName.text = directoryInfo.FullName;
					commit.interactable = directoryInfo.Exists;
					flag = true;
				}
				catch (Exception)
				{
					fileName.text = "";
					commit.interactable = false;
				}
			}
			if (!flag)
			{
				try
				{
					fileName.text = new FileInfo(workingFile).Name;
					commit.interactable = File.Exists(workingFile);
				}
				catch (Exception)
				{
					fileName.text = "";
					commit.interactable = false;
				}
			}
		}
		else
		{
			if (workingFile.Length > 0)
			{
				fileName.text = new FileInfo(workingFile).Name;
			}
			commit.interactable = workingFile.Length > 0;
		}
	}

	public void OpenDir(string path, bool select = false)
	{
		if (!canSelectFolder || !select)
		{
			ClearSelection();
		}
		workingPath = Path.GetFullPath(path);
		if (canSelectFolder && select)
		{
			workingFile = workingPath;
			UpdateFileInfo(select);
		}
		UpdateElements();
		UpdateDrivesList();
		UpdateFilesList();
	}

	private void UpdateElements()
	{
		currentPath.text = workingPath;
	}

	private void UpdateDrivesList()
	{
		GameObject gameObject = drivesScrollRectContent;
		for (int i = 0; i < gameObject.transform.childCount; i++)
		{
			UnityEngine.Object.Destroy(gameObject.transform.GetChild(i).gameObject);
		}
		string[] logicalDrives = Directory.GetLogicalDrives();
		for (int j = 0; j < logicalDrives.Length; j++)
		{
			GameObject obj = UnityEngine.Object.Instantiate(drivesScrollRectElement, Vector3.zero, Quaternion.identity);
			obj.transform.SetParent(gameObject.transform, worldPositionStays: true);
			obj.transform.localScale = new Vector3(1f, 1f, 1f);
			FileListElement component = obj.GetComponent<FileListElement>();
			component.instance = this;
			component.data = logicalDrives[j];
			component.elementName.text = logicalDrives[j];
			component.isFile = false;
		}
	}

	private string GetFileSizeText(long size)
	{
		string text = "#.##";
		if ((float)size / 1024f < 1f)
		{
			return "1 Kb";
		}
		if ((float)size / 1024f < 1024f)
		{
			return ((float)size / 1024f).ToString(text) + " Kb";
		}
		if ((float)size / 1024f / 1024f < 1024f)
		{
			return ((float)size / 1024f / 1024f).ToString(text) + " Mb";
		}
		return ((float)size / 1024f / 1024f / 1024f).ToString(text) + " Gb";
	}

	private void UpdateFilesList()
	{
		GameObject gameObject = filesScrollRectContent;
		for (int i = 0; i < gameObject.transform.childCount; i++)
		{
			UnityEngine.Object.Destroy(gameObject.transform.GetChild(i).gameObject);
		}
		DirectoryInfo directoryInfo = new DirectoryInfo(workingPath);
		try
		{
			DirectoryInfo[] directories = directoryInfo.GetDirectories();
			for (int j = 0; j < directories.Length; j++)
			{
				if (directories[j].Name[0] != '@' && directories[j].Name[0] != '.' && (directories[j].Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
				{
					GameObject obj = UnityEngine.Object.Instantiate(filesScrollRectElement, Vector3.zero, Quaternion.identity);
					obj.transform.SetParent(gameObject.transform, worldPositionStays: true);
					obj.transform.localScale = new Vector3(1f, 1f, 1f);
					FileListElement component = obj.GetComponent<FileListElement>();
					component.instance = this;
					string fullName = directories[j].FullName;
					char directorySeparatorChar = Path.DirectorySeparatorChar;
					component.data = fullName + directorySeparatorChar;
					component.elementName.text = directories[j].Name;
					component.size.text = "";
					component.icon.sprite = folderIcon;
					component.isFile = false;
				}
			}
			FileInfo[] array = ((allowedExtensions != null) ? (from f in directoryInfo.GetFiles()
				where allowedExtensions.Contains(f.Extension.ToLower())
				select f).ToArray() : directoryInfo.GetFiles());
			for (int num = 0; num < array.Length; num++)
			{
				GameObject obj2 = UnityEngine.Object.Instantiate(filesScrollRectElement, Vector3.zero, Quaternion.identity);
				obj2.transform.SetParent(gameObject.transform, worldPositionStays: true);
				obj2.transform.localScale = new Vector3(1f, 1f, 1f);
				FileListElement component2 = obj2.GetComponent<FileListElement>();
				component2.instance = this;
				component2.data = array[num].FullName;
				component2.size.text = GetFileSizeText(array[num].Length);
				component2.elementName.text = array[num].Name;
				component2.icon.sprite = fileIcon;
				component2.isFile = true;
			}
		}
		catch (Exception)
		{
		}
	}
}
