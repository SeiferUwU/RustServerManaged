using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Facepunch;

[JsonModel]
public class AccountNotice
{
	public class GeneralNoticeModel
	{
		[JsonProperty("Title")]
		public string Title { get; set; }

		[JsonProperty("Message")]
		public string Message { get; set; }
	}

	public class BanNoticeModel
	{
		[JsonProperty("BannedUserId")]
		public string BannedUserId { get; set; }

		[JsonProperty("BannedUserName")]
		public string BannedUserName { get; set; }

		[JsonProperty("FirstReport")]
		public DateTime FirstReport { get; set; }
	}

	public int NoticeId { get; set; }

	public DateTime Created { get; set; }

	public string Json { get; set; }

	public string Seen { get; set; }

	public int NotificationType { get; set; }

	public bool GlobalNotification { get; set; }

	[JsonIgnore]
	public object Parsed { get; private set; }

	public void ParseJson()
	{
		try
		{
			switch (NotificationType)
			{
			case 0:
			case 1:
			case 2:
				Parsed = JsonConvert.DeserializeObject<GeneralNoticeModel>(Json);
				break;
			case 3:
				Parsed = JsonConvert.DeserializeObject<BanNoticeModel>(Json);
				break;
			default:
				Debug.LogWarning($"Unknown NotificationType: {NotificationType}, raw JSON: {Json}");
				Parsed = null;
				break;
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning($"Failed to parse notice Json (Type {NotificationType}): {ex.Message}");
			Parsed = null;
		}
	}

	public override int GetHashCode()
	{
		return (((((17 * 23 + NoticeId.GetHashCode()) * 23 + Created.GetHashCode()) * 23 + ((Json != null) ? Json.GetHashCode() : 0)) * 23 + ((Seen != null) ? Seen.GetHashCode() : 0)) * 23 + NotificationType.GetHashCode()) * 23 + GlobalNotification.GetHashCode();
	}
}
