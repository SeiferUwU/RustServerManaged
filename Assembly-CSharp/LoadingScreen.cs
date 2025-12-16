using Rust.UI;
using SoftMasking;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : SingletonComponent<LoadingScreen>
{
	private bool _isOpen;

	[Header("Loading Screen References - UI")]
	public Canvas canvas;

	public CanvasGroup panel;

	public CanvasGroup blackout;

	public NeedsCursor needsCursor;

	public SoftMask softMask;

	public TextMeshProUGUI title;

	public TextMeshProUGUI subtitle;

	public RawImage backgroundImage;

	public Texture2D defaultBackground;

	public GameObject infoHeader;

	public GameObject demoBlock;

	public GameObject serverBlock;

	public RustButton cancelButton;

	[Header("Loading Screen References - Server Info")]
	public RustText serverName;

	public RustText serverPlayers;

	public RustText serverMode;

	public RustText serverMap;

	public RustText serverWiped;

	public HttpImage serverLogoImage;

	public ServerBrowserTagList serverTags;

	[Header("Loading Screen References - Demo Info")]
	public RustText demoName;

	public RustText demoLength;

	public RustText demoDate;

	public RustText demoMap;

	[Header("Loading Screen References - Tips and Warnings")]
	public MenuTip menuTip;

	public GameObject performanceWarning;

	public GameObject pingWarning;

	public RustText pingWarningText;

	[Header("Loading Screen References - Audio")]
	public AudioSource music;

	[Tooltip("Ping must be at least this many ms higher than the server browser ping")]
	[Header("Loading Screen References - Settings")]
	public int minPingDiffToShowWarning = 50;

	[Tooltip("Ping must be this many times higher than the server browser ping")]
	public float pingDiffFactorToShowWarning = 2f;

	[Tooltip("Number of ping samples required before showing the warning")]
	public int requiredPingSampleCount = 10;

	public static Translate.Phrase pingWarningPhrase = new Translate.Phrase("loading.ping-warning", "<color=#FFF><size=20>PING WARNING</size></color>\nThis server's ping on the server browser ({0} ms) is much lower than the ping you are getting after connecting to the server ({1} ms). This could mean that this server is located far away and you will have a less than ideal playing experience while on this server.");

	public static Translate.Phrase vanillaPhrase = new Translate.Phrase("loading.mode.vanilla", "vanilla");

	public static bool isOpen
	{
		get
		{
			if (SingletonComponent<LoadingScreen>.Instance != null)
			{
				return SingletonComponent<LoadingScreen>.Instance._isOpen;
			}
			return false;
		}
	}

	public static string Text { get; private set; }

	public static void Update(string strType)
	{
	}

	public static void Update(string strType, string strSubtitle)
	{
	}
}
