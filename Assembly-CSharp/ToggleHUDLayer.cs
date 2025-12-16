using Facepunch.Extend;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleHUDLayer : MonoBehaviour, IClientComponent
{
	public Toggle toggleControl;

	public TextMeshProUGUI textControl;

	public string hudComponentName;

	protected void OnEnable()
	{
		UIHUD instance = SingletonComponent<UIHUD>.Instance;
		if (instance != null)
		{
			Transform transform = instance.transform.FindChildRecursive(hudComponentName);
			if (transform != null)
			{
				toggleControl.isOn = transform.gameObject.activeSelf;
			}
			else
			{
				Debug.LogWarning(GetType().Name + ": Couldn't find child: " + hudComponentName);
			}
		}
	}

	public void OnToggleChanged()
	{
		ConsoleSystem.Run(ConsoleSystem.Option.Client, "global.hudcomponent", hudComponentName, toggleControl.isOn);
	}
}
