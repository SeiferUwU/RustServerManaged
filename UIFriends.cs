using Rust.UI.MainMenu;
using UnityEngine;

public class UIFriends : UI_Window
{
	public static UIFriends Instance;

	public CanvasGroup CanvasGroup;

	public RectTransform Body;

	public UIFriendsListBase FriendsList;

	public UIFriendsListButton Button;

	public GameObject DiscordSettingsButton;

	public GameObject DiscordSettingsPanel;
}
