using System;

namespace Rust.UI.MainMenu;

[Serializable]
public struct NotificationData
{
	public NotificationType NotificationType;

	public string NotificationText;

	public string NotificationLink;

	public bool IsInternal;

	public Translate.Phrase Phrase;

	public bool HasSeen;

	public int? Id;

	public object[] PhraseArguments;

	public bool HasLink => !string.IsNullOrEmpty(NotificationLink);

	public NotificationData(NotificationType type, string text, string link = "", bool isInternal = true, Translate.Phrase phrase = null, bool hasSeen = false, int? id = null, params object[] arguments)
	{
		NotificationType = type;
		NotificationText = text;
		NotificationLink = link;
		IsInternal = isInternal;
		Phrase = phrase;
		HasSeen = hasSeen;
		Id = id;
		PhraseArguments = arguments;
	}

	public override bool Equals(object obj)
	{
		if (obj is NotificationData notificationData)
		{
			if (NotificationText == notificationData.NotificationText)
			{
				return NotificationLink == notificationData.NotificationLink;
			}
			return false;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return (NotificationText?.GetHashCode() ?? 0) + (NotificationLink?.GetHashCode() ?? 0);
	}
}
