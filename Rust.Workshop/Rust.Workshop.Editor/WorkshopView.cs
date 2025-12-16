using Facepunch.Extend;
using Rust.Platform.Steam;
using TMPro;
using UnityEngine;

namespace Rust.Workshop.Editor;

public class WorkshopView : MonoBehaviour
{
	public TextMeshProUGUI Title;

	public TextMeshProUGUI AuthorName;

	public GameObject VotingPanel;

	private IWorkshopContent item;

	protected WorkshopInterface Interface => GetComponentInParent<WorkshopInterface>();

	protected WorkshopItemEditor Editor => Interface.Editor;

	private void Awake()
	{
		VotingPanel.SetActive(value: false);
	}

	public void Update()
	{
		if (item != null && string.IsNullOrEmpty(AuthorName.text))
		{
			AuthorName.text = item.Owner.UserName.Truncate(32).ToUpper();
			VotingPanel.SetActive(value: true);
		}
	}

	public void UpdateFrom(IWorkshopContent item)
	{
		this.item = item;
		if (this.item != null)
		{
			Title.text = item.Title.Truncate(24).ToUpper();
			AuthorName.text = item.Owner.UserName.Truncate(32).ToUpper();
			VotingPanel.SetActive(value: true);
		}
		else
		{
			Title.text = "FAILED TO LOAD SKIN";
			AuthorName.text = "";
		}
	}

	public void OnVoteUp()
	{
		if (item != null)
		{
			if (item is SteamWorkshopContent { Value: var value })
			{
				value.Vote(up: true);
			}
			UpdateFrom(item);
		}
	}

	public void OnVoteDown()
	{
		if (item != null)
		{
			if (item is SteamWorkshopContent { Value: var value })
			{
				value.Vote(up: false);
			}
			UpdateFrom(item);
		}
	}

	public void OpenWeb()
	{
		if (item != null)
		{
			UnityEngine.Application.OpenURL(item?.Url);
		}
	}

	internal void Clear()
	{
		item = null;
		Title.text = "";
		AuthorName.text = "";
		VotingPanel.SetActive(value: false);
	}
}
