using Rust.UI;
using UnityEngine;

namespace Rust.Workshop;

public class WorkshopViewmodelControls : MonoBehaviour
{
	public RustButton Enabled;

	public RustButton Ironsights;

	public RustButton admire;

	private IViewmodelWorkshopPreview currentViewmodel;

	private void OnEnable()
	{
		admire.OnPressed.AddListener(OnAdmirePressed);
	}

	private void OnDisable()
	{
		admire.OnPressed.RemoveListener(OnAdmirePressed);
	}

	public void Clear()
	{
		Ironsights.Value = false;
		currentViewmodel = null;
	}

	internal void DoUpdate(GameObject viewModel)
	{
		if (viewModel == null)
		{
			base.gameObject.SetActive(value: false);
			return;
		}
		base.gameObject.SetActive(value: true);
		if (!Enabled.Value)
		{
			viewModel.SetActive(value: false);
			return;
		}
		viewModel.SetActive(value: true);
		Camera.main.fieldOfView = 85f;
		if (viewModel.TryGetComponent<IViewmodelWorkshopPreview>(out var component))
		{
			currentViewmodel = component;
		}
	}

	private void OnAdmirePressed()
	{
		_ = currentViewmodel;
	}
}
