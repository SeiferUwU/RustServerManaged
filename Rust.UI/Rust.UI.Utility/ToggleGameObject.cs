using UnityEngine;
using UnityEngine.UI;

namespace Rust.UI.Utility;

[RequireComponent(typeof(Toggle))]
internal class ToggleGameObject : MonoBehaviour
{
	public GameObject Target;

	private Toggle component;

	public void OnEnable()
	{
		component = GetComponent<Toggle>();
		component.onValueChanged.AddListener(OnToggled);
	}

	public void OnDisable()
	{
		if (!Application.isQuitting)
		{
			component.onValueChanged.RemoveListener(OnToggled);
		}
	}

	public void Update()
	{
		component.isOn = Target.activeSelf;
	}

	public void OnToggled(bool value)
	{
		Target.SetActive(value);
	}
}
