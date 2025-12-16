using System;
using Rust.UI;
using UnityEngine;
using UnityEngine.UI;

public class PremiumModal : SingletonComponent<PremiumModal>
{
	public RustText UsernameLabel;

	public RustText MoneyLabel;

	public RustText ActiveStatusLabel;

	public RawImage ProfilePicture;

	public RustButton RefreshButton;

	public Translate.Phrase ActivePhrase;

	public Translate.Phrase InactivePhrase;

	public Translate.Phrase SearchingPhrase;

	public static Translate.Phrase ErrorPhrase = new Translate.Phrase("premium.error", "Error");

	public GameObject[] BackgroundImages;

	public static TimeSpan ButtonTimeout = TimeSpan.FromSeconds(65.0);

	public void Open()
	{
	}

	public void Close()
	{
	}

	private void UpdateInfo()
	{
	}

	public void RefreshButtonClicked()
	{
	}
}
