using UnityEngine;

public class DisableIfDlssNotSupported : MonoBehaviour
{
	private void OnEnable()
	{
		base.gameObject.SetActive(value: false);
	}
}
