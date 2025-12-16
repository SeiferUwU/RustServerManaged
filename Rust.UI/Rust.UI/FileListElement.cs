using UnityEngine;
using UnityEngine.UI;

namespace Rust.UI;

public class FileListElement : MonoBehaviour
{
	public Image icon;

	public Text elementName;

	public Text size;

	public FileDialog instance;

	public bool isFile;

	public string data;

	public void OnClick()
	{
		if (!isFile)
		{
			instance.OpenDir(data, select: true);
		}
		else
		{
			instance.SelectFile(data);
		}
	}
}
