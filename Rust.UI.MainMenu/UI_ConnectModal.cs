using UnityEngine;
using UnityEngine.UI;

namespace Rust.UI.MainMenu;

public class UI_ConnectModal : UI_Window
{
	[Header("References")]
	[SerializeField]
	private RustText _title;

	[SerializeField]
	private RustText _description;

	[SerializeField]
	private HttpImage _headerImage;

	[SerializeField]
	private GameObject _headerImageLoading;

	[SerializeField]
	private ServerBrowserTagList _tagController;

	[SerializeField]
	private RustText _mapTypeText;

	[SerializeField]
	private RustButton _websiteButton;

	[SerializeField]
	private Tooltip _websiteTooltip;

	[SerializeField]
	private GameObject _descriptionLoading;

	[SerializeField]
	private GameObject _connectToServerButton;

	[SerializeField]
	private GameObject _needsPremiumButton;

	[SerializeField]
	private GameObject _mapButton;

	[SerializeField]
	private UI_ServerMap _map;

	[Header("References - Friends")]
	[SerializeField]
	private RustText _friendsText;

	[SerializeField]
	private GameObject _friendsObject;

	[SerializeField]
	private Tooltip _friendsTooltip;

	[SerializeField]
	[Header("Info Box References")]
	private RustText _playerCount;

	[SerializeField]
	private GameObject _queuedPlayersObject;

	[SerializeField]
	private RustText _queuedPlayersCount;

	[SerializeField]
	private GameObject _lastPlayedObject;

	[SerializeField]
	private RustText _lastPlayedText;

	[SerializeField]
	private GameObject _wipedObject;

	[SerializeField]
	private RustText _wipedText;

	[SerializeField]
	private ScrollRect _scrollRect;

	[SerializeField]
	private RectMask2D _scrollMask;

	public static Translate.Phrase lastPlayedPhrase = new Translate.Phrase("connection.modal.lastplayed.ago", "{0} ago");

	public static Translate.Phrase serverAgePhrase = new Translate.Phrase("connection.modal.serverage.old", "{0} old");

	public static Translate.Phrase loadingError = new Translate.Phrase("connection.modal.error", "Error loading server");
}
