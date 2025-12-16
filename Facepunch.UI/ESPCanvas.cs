using UnityEngine;

namespace Facepunch.UI;

public class ESPCanvas : SingletonComponent<ESPCanvas>
{
	public Canvas canvas;

	[Tooltip("Amount of times per second we should update the visible panels")]
	public float RefreshRate = 5f;

	[Tooltip("This object will be duplicated in place")]
	public ESPPlayerInfo Source;

	[Header("Nameplate Properties")]
	public Gradient gradientNormal;

	public Gradient gradientTeam;

	public AccessibilityColourCollection TeamLookup;

	public AccessibilityColourCollection ClanLookup;

	public AccessibilityColourCollection AllyLookup;

	public AccessibilityColourCollection EnemyLookup;

	private static int NameplateCount = 32;

	[ClientVar(ClientAdmin = true)]
	public static float OverrideMaxDisplayDistance = 0f;

	[ClientVar(ClientAdmin = true)]
	public static bool DisableOcclusionChecks = false;

	[ClientVar(ClientAdmin = true)]
	public static bool ShowHealth = false;

	[ClientVar(ClientAdmin = true)]
	public static bool ColourCodeTeams = false;

	[ClientVar(ClientAdmin = true)]
	public static bool UseRandomTeamColours = false;

	[ClientVar(ClientAdmin = true, Help = "Max amount of nameplates to show at once")]
	public static int MaxNameplates
	{
		get
		{
			return NameplateCount;
		}
		set
		{
			NameplateCount = Mathf.Clamp(value, 16, 150);
		}
	}
}
