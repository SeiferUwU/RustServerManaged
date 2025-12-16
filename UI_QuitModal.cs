using Rust.UI.MainMenu;
using UnityEngine;

public class UI_QuitModal : UI_Window
{
	public static UI_QuitModal Instance;

	[SerializeField]
	private UIEscapeCapture _capture;

	protected override void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		base.Awake();
		SetUI(state: false);
		_capture.enabled = false;
	}

	protected override void OnOpened()
	{
		base.OnOpened();
		_capture.enabled = true;
	}

	protected override void OnClosed()
	{
		base.OnClosed();
		_capture.enabled = false;
	}

	public void Quit()
	{
		ConsoleSystem.Run(ConsoleSystem.Option.Client, "quit");
	}
}
