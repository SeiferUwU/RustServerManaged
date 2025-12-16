using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Development.Attributes;
using Epic.OnlineServices.Logging;
using Epic.OnlineServices.Reports;
using Facepunch;
using Facepunch.Extend;
using Facepunch.Rust;
using Network;
using Rust;
using UnityEngine;

namespace ConVar;

[ResetStaticFields]
[Factory("server")]
public class Server : ConsoleSystem
{
	[ServerVar]
	public static string ip = "";

	[ServerVar]
	public static int port = 28015;

	[ServerVar]
	public static int queryport = 0;

	[ServerVar(ShowInAdminUI = true)]
	public static int maxplayers = 500;

	[ServerVar(ShowInAdminUI = true)]
	public static string hostname = "My Untitled Rust Server";

	[ServerVar]
	public static string identity = "my_server_identity";

	[ServerVar]
	public static string filefolderoverride = "";

	[ServerVar]
	public static string level = "Procedural Map";

	[ServerVar]
	public static string levelurl = "";

	[ServerVar]
	public static bool leveltransfer = true;

	[ServerVar]
	public static int seed = 1337;

	[ServerVar]
	public static int salt = 1;

	[ServerVar]
	public static int worldsize = 4500;

	[ServerVar]
	public static int saveinterval = 600;

	[ServerVar]
	public static int encryption = 2;

	[ServerVar]
	public static string anticheatid = "xyza7891h6UjNfd0eb2HQGtaul0WhfvS";

	[ServerVar]
	public static string anticheatkey = "OWUDFZmi9VNL/7VhGVSSmCWALKTltKw8ISepa0VXs60";

	[ServerVar]
	public static bool anticheattoken = true;

	[ServerVar]
	public static bool strictauth_eac = false;

	[ServerVar]
	public static bool strictauth_steam = false;

	[ServerVar]
	public static int tickrate = 10;

	[ServerVar]
	public static int entityrate = 16;

	[ServerVar]
	public static float schematime = 1800f;

	[ServerVar]
	public static float cycletime = 500f;

	[ServerVar]
	public static bool official = false;

	[ServerVar]
	public static bool stats = false;

	[ServerVar]
	public static bool stability = true;

	[ServerVar(ShowInAdminUI = true)]
	public static bool radiation = true;

	[ReplicatedVar]
	public static float max_explosive_protection = 0.75f;

	[ServerVar]
	public static float itemdespawn = 300f;

	[ServerVar]
	public static float itemdespawn_container_scale = 2f;

	[ServerVar]
	public static int itemdespawn_container_max_multiplier = 24;

	[ServerVar]
	public static float itemdespawn_quick = 30f;

	[ServerVar]
	public static float corpsedespawn = 300f;

	[ServerVar]
	public static float debrisdespawn = 30f;

	[ServerVar]
	public static bool pve = false;

	[ReplicatedVar]
	public static bool cinematic = false;

	[ServerVar(ShowInAdminUI = true)]
	public static string description = "No server description has been provided.";

	[ServerVar(ShowInAdminUI = true)]
	public static string url = "";

	[ServerVar]
	public static string branch = "";

	[ServerVar]
	public static int queriesPerSecond = 2000;

	[ServerVar]
	public static int ipQueriesPerMin = 30;

	[ServerVar]
	public static bool statBackup = false;

	[ServerVar]
	public static int rejoin_delay = 300;

	[ServerVar]
	public static string ping_region_code_override = "";

	private static string _favoritesEndpoint = "";

	[ServerVar(Saved = true, ShowInAdminUI = true)]
	public static string headerimage = "";

	[ServerVar(Saved = true, ShowInAdminUI = true)]
	public static string logoimage = "";

	[ServerVar(Saved = true, ShowInAdminUI = true)]
	public static int saveBackupCount = 2;

	[ReplicatedVar(Saved = true, ShowInAdminUI = true)]
	public static string motd = "";

	[ServerVar(Saved = true)]
	public static float meleedamage = 1f;

	[ServerVar(Saved = true)]
	public static float arrowdamage = 1f;

	[ServerVar(Saved = true)]
	public static float bulletdamage = 1f;

	[ServerVar(Saved = true)]
	public static float bleedingdamage = 1f;

	[ServerVar(Saved = true, Help = "How much to increase time to kill in pvp globally, 2.0 = twice as long, 0.5 = half as long")]
	public static float pvp_ttk_global = 1f;

	[ServerVar(Saved = true, Help = "How much to increase time to kill with melee in pvp globally, 2.0 = twice as long, 0.5 = half as long")]
	public static float pvp_ttk_melee = 1f;

	[ServerVar(Saved = true, Help = "How much to increase time to kill bullets in pvp globally, 2.0 = twice as long, 0.5 = half as long")]
	public static float pvp_ttk_bullet = 1f;

	[ServerVar(Help = "Lower damage of explosives to 1 and allow them to be triggered multiple times")]
	public static bool explosive_testing_mode = false;

	[ServerVar(Saved = true)]
	public static float oilrig_radiation_amount_scale = 1f;

	[ServerVar(Saved = true)]
	public static float oilrig_radiation_time_scale = 1f;

	[ServerVar]
	public static float oilrig_radiation_alarm_threshold = 0f;

	[ReplicatedVar(Saved = true)]
	public static float funWaterDamageThreshold = 0.8f;

	[ReplicatedVar(Saved = true)]
	public static float funWaterWetnessGain = 0.05f;

	[ServerVar(Saved = true)]
	public static float meleearmor = 1f;

	[ServerVar(Saved = true)]
	public static float arrowarmor = 1f;

	[ServerVar(Saved = true)]
	public static float bulletarmor = 1f;

	[ServerVar(Saved = true)]
	public static float bleedingarmor = 1f;

	[ServerVar(Saved = true)]
	public static float pvpBulletDamageMultiplier = 1f;

	[ServerVar(Saved = true)]
	public static float pveBulletDamageMultiplier = 1f;

	[ServerVar]
	public static int updatebatch = 512;

	[ServerVar]
	public static int updatebatchspawn = 1024;

	[ServerVar]
	public static int entitybatchsize = 100;

	[ServerVar]
	public static float entitybatchtime = 1f;

	[ServerVar]
	public static float composterUpdateInterval = 300f;

	[ReplicatedVar]
	public static float planttick = 60f;

	[ServerVar]
	public static float planttickscale = 1f;

	private static int _maxHttp = 32;

	[ServerVar]
	public static bool useMinimumPlantCondition = true;

	[ServerVar(Saved = true)]
	public static float nonPlanterDeathChancePerTick = 0.005f;

	[ServerVar(Saved = true)]
	public static float ceilingLightGrowableRange = 3f;

	[ReplicatedVar(Saved = true)]
	public static float artificialTemperatureGrowableRange = 4f;

	[ServerVar(Saved = true)]
	public static float ceilingLightHeightOffset = 3f;

	[ReplicatedVar(Saved = true)]
	public static float sprinklerRadius = 3f;

	[ServerVar(Saved = true)]
	public static float sprinklerEyeHeightOffset = 3f;

	[ServerVar(Saved = true)]
	public static bool useLegacySprinklerLoadProcess = false;

	[ServerVar(Saved = true)]
	public static float optimalPlanterQualitySaturation = 0.6f;

	[ServerVar]
	public static float metabolismtick = 1f;

	[ServerVar]
	public static float modifierTickRate = 1f;

	[ServerVar(Saved = true)]
	public static float rewounddelay = 60f;

	[ServerVar(Saved = true, Help = "Can players be wounded after receiving fatal damage")]
	public static bool woundingenabled = true;

	[ServerVar(Saved = true, Help = "Do players go into the crawling wounded state")]
	public static bool crawlingenabled = true;

	[ServerVar(Help = "Base chance of recovery after crawling wounded state", Saved = true)]
	public static float woundedrecoverchance = 0.2f;

	[ServerVar(Help = "Base chance of recovery after incapacitated wounded state", Saved = true)]
	public static float incapacitatedrecoverchance = 0.1f;

	[ServerVar(Help = "Maximum percent chance added to base wounded/incapacitated recovery chance, based on the player's food and water level", Saved = true)]
	public static float woundedmaxfoodandwaterbonus = 0.25f;

	[ServerVar(Help = "Minimum initial health given when a player dies and moves to crawling wounded state", Saved = false)]
	public static int crawlingminimumhealth = 7;

	[ServerVar(Help = "Maximum initial health given when a player dies and moves to crawling wounded state", Saved = false)]
	public static int crawlingmaximumhealth = 12;

	[ServerVar(Saved = true)]
	public static bool playerserverfall = true;

	[ServerVar]
	public static bool plantlightdetection = true;

	[ServerVar]
	public static float respawnresetrange = 50f;

	[ReplicatedVar]
	public static int max_sleeping_bags = 15;

	[ReplicatedVar]
	public static bool bag_quota_item_amount = true;

	[ServerVar]
	public static int maxunack = 4;

	[ServerVar]
	public static bool netcache = true;

	[ServerVar]
	public static bool corpses = true;

	[ServerVar]
	public static bool events = true;

	[ServerVar]
	public static bool dropitems = true;

	[ServerVar]
	public static int netcachesize = 0;

	[ServerVar]
	public static int savecachesize = 0;

	[ServerVar]
	public static int combatlogsize = 30;

	[ServerVar]
	public static int combatlogdelay = 10;

	[ServerVar]
	public static int authtimeout = 60;

	[ServerVar]
	public static int playertimeout = 60;

	[ServerVar(ShowInAdminUI = true)]
	public static int idlekick = 30;

	[ServerVar]
	public static int idlekickmode = 1;

	[ServerVar]
	public static int idlekickadmins = 0;

	[ServerVar]
	public static bool long_distance_sounds = true;

	private static string _gamemode;

	private static string _tags = "";

	[ServerVar(Help = "Censors the Steam player list to make player tracking more difficult")]
	public static bool censorplayerlist = true;

	[ServerVar(Help = "HTTP API endpoint for centralized banning (see wiki)")]
	public static string bansServerEndpoint = "";

	[ServerVar(Help = "Failure mode for centralized banning, set to 1 to reject players from joining if it's down (see wiki)")]
	public static int bansServerFailureMode = 0;

	[ServerVar(Help = "Timeout (in seconds) for centralized banning web server requests")]
	public static int bansServerTimeout = 5;

	[ServerVar(Help = "HTTP API endpoint for receiving F7 reports", Saved = true)]
	public static string reportsServerEndpoint = "";

	[ServerVar(Help = "If set, this key will be included with any reports sent via reportsServerEndpoint (for validation)", Saved = true)]
	public static string reportsServerEndpointKey = "";

	[ServerVar(Help = "Should F7 reports from players be printed to console", Saved = true)]
	public static bool printReportsToConsole = false;

	[ServerVar(Help = "If a player presses the respawn button, respawn at their death location (for trailer filming)")]
	public static bool respawnAtDeathPosition = false;

	[ServerVar(Help = "When a player respawns give them the loadout assigned to client.RespawnLoadout (created with inventory.saveloadout)")]
	public static bool respawnWithLoadout = false;

	[ServerVar(Help = "When transferring water, should containers keep 1 water behind. Enabling this should help performance if water IO is causing performance loss", Saved = true)]
	public static bool waterContainersLeaveWaterBehind = false;

	[ServerVar(Help = "How often industrial conveyors attempt to move items (value is an interval measured in seconds). Setting to 0 will disable all movement", Saved = true, ShowInAdminUI = true)]
	public static float conveyorMoveFrequency = 5f;

	[ServerVar(Help = "How often industrial crafters attempt to craft items (value is an interval measured in seconds). Setting to 0 will disable all crafting", Saved = true, ShowInAdminUI = true)]
	public static float industrialCrafterFrequency = 5f;

	[ReplicatedVar(Help = "How much scrap is required to research default blueprints", Saved = true, ShowInAdminUI = true)]
	public static int defaultBlueprintResearchCost = 10;

	[ServerVar(Help = "Whether to check for illegal industrial pipes when changing building block states (roof bunkers)", Saved = true, ShowInAdminUI = true)]
	public static bool enforcePipeChecksOnBuildingBlockChanges = true;

	[ServerVar(Help = "How many stacks a single conveyor can move in a single tick", Saved = true, ShowInAdminUI = true)]
	public static int maxItemStacksMovedPerTickIndustrial = 12;

	[ServerVar(Help = "How long per frame to spend on industrial jobs", Saved = true, ShowInAdminUI = true)]
	public static float industrialFrameBudgetMs = 0.5f;

	[ServerVar(Help = "Should indusrial be paused during autosaves")]
	public static bool pauseindustrialduringsave = true;

	[ServerVar(Help = "When enabled industrial transfers will abort if they start to take too long. Will lead to inconsistent splitting but should retain performance", Saved = true)]
	public static bool industrialTransferStrictTimeLimits = true;

	[ServerVar(Help = "Enables a faster way to move items around during conveyor transfers. Should be on unless there's a issue")]
	public static bool industrialAllowQuickMove = true;

	[ServerVar(Help = "How long per frame to spend animating items moving into the hopper (will be instant if <= 0)", Saved = true, ShowInAdminUI = true)]
	public static float hopperAnimationBudgetMs = 0.1f;

	[ServerVar(Help = "Set to false to disable the storage adaptor sorting functionality")]
	public static bool allowSorting = true;

	[ReplicatedVar(Help = "How many markers each player can place", Saved = true, ShowInAdminUI = true)]
	public static int maximumMapMarkers = 5;

	[ServerVar(Help = "How many pings can be placed by each player", Saved = true, ShowInAdminUI = true)]
	public static int maximumPings = 5;

	[ServerVar(Help = "How long a ping should last", Saved = true, ShowInAdminUI = true)]
	public static float pingDuration = 10f;

	[ServerVar(Help = "Allows backpack equipping while not grounded", Saved = true, ShowInAdminUI = true)]
	public static bool canEquipBackpacksInAir = false;

	[ReplicatedVar(Help = "How long it takes to pick up a used parachute in seconds", Saved = true, ShowInAdminUI = true)]
	public static float parachuteRepackTime = 8f;

	[ServerVar(Help = "Whether emoji ownership is checked server side. Could be performance draining in high chat volumes")]
	public static bool emojiOwnershipCheck = true;

	[ReplicatedVar(Help = "Skip death screen fade", Saved = false, ShowInAdminUI = false)]
	public static bool skipDeathScreenFade = false;

	[ReplicatedVar(Help = "Controls whether the tutorial is enabled on this server", Saved = true, ShowInAdminUI = true, Default = "false")]
	public static bool tutorialEnabled = false;

	[ReplicatedVar(Help = "How much of a tax to apply to tech unlocks at a level 1 workbench. 10 = additional 10% scrap cost", Saved = true)]
	public static float workbenchTaxRate1 = 0f;

	[ReplicatedVar(Help = "How much of a tax to apply to tech unlocks at a level 2 workbench. 10 = additional 10% scrap cost", Saved = true)]
	public static float workbenchTaxRate2 = 0f;

	[ReplicatedVar(Help = "How much of a tax to apply to tech unlocks at a level 3 workbench. 10 = additional 10% scrap cost", Saved = true)]
	public static float workbenchTaxRate3 = 0f;

	[ServerVar(Help = "Automatically upload procedurally generated maps so that players download them (faster) instead of re-generating them", Saved = true, ShowInAdminUI = true)]
	public static bool autoUploadMap = true;

	[ReplicatedVar(Help = "Can players use the in-game map")]
	public static bool mapenabled = true;

	[ReplicatedVar(Help = "Should the in-game map be covered by a fog of war")]
	public static bool fogofwar = false;

	[ReplicatedVar(Help = "How much area around the player is revealed when using fog of war. Must be a multiple of 32")]
	public static int fogofwarrevealsize = 256;

	[ReplicatedVar(Help = "Will the in-game compass show at the top of the screen")]
	public static bool compassenabled = true;

	[ReplicatedVar(Help = "Should the player see their position on the map")]
	public static bool hideplayeronmap = false;

	[ReplicatedVar(Help = "Should hte player see their direction on the map")]
	public static bool hideplayermapdirection = false;

	[ServerVar(Help = "Automatically upload an image of the map, used to show the map in the server browser", Saved = true, ShowInAdminUI = true)]
	public static bool autoUploadMapImages = true;

	[ServerVar(Help = "How often (in hours) the water well NPC's update their sell orders")]
	public static float waterWellNpcSalesRefreshFrequency = 1f;

	[ReplicatedVar(Help = "Opens a loot panel when interacting with a workbench instead of going straight into the tech tree. Designed for backwards compatibility with mods.")]
	public static bool useLegacyWorkbenchInteraction = false;

	[ServerVar(Help = "If no players are in this range kayaks, boogie boards and inner tubes will switch to a cheaper buoyancy system")]
	public static float lowPriorityBuoyancyRange = 30f;

	[ServerVar(Help = "If true hot air balloons can be shot down with homing missiles")]
	public static bool homingMissileTargetsHab = false;

	[ServerVar(Help = "Require a premium status account to connect to this server")]
	public static bool premium = false;

	[ReplicatedVar(Help = "Whether to run the food spoiling system")]
	public static bool foodSpoiling = true;

	[ServerVar]
	public static float foodSpoilingBudgetMs = 0.05f;

	[ServerVar(Help = "Maximum difference (in seconds) that two items with spoil timers can have and still be stackable")]
	public static float maxFoodSpoilTimeDiffForItemStack = 180f;

	[ServerVar(Help = "If two spoiled food items are both above this threshold then we will allow them to be stacked")]
	public static float normalisedFoodSpoilTimeStackThreshold = 0.9f;

	[ServerVar(Help = "Whether to run local avoidance for chickens, disabling might get a slight performance improvement but chickens will clip", Saved = true, ShowInAdminUI = true)]
	public static bool farmChickenLocalAvoidance = true;

	[ServerVar(Help = "Endpoint to use to check if players have premium status")]
	public static string premiumVerifyEndpoint = "https://rust-api.facepunch.com/api/premium/verify";

	[ServerVar(Help = "Minimum time to recheck premium status for already connected players (in seconds)")]
	public static float premiumRecheckMinSeconds = 300f;

	[ServerVar(Help = "How often to do premium status rechecks")]
	public static float premiumRecheckInterval = 300f;

	[ServerVar(Help = "Maximum number of players to recheck at a time")]
	public static int premiumRecheckMaxBatchSize = 100;

	[ServerVar(Saved = true)]
	public static bool spawnVineTrees = true;

	[ServerVar(Saved = true)]
	public static bool allowVineSwinging = true;

	[ServerVar(Saved = true, ShowInAdminUI = true, Help = "Bags will increase their respawn time by this much")]
	public static float respawnTimeAdditionBag = 0f;

	[ServerVar(Saved = true, ShowInAdminUI = true, Help = "Beds will increase their respawn time by this much")]
	public static float respawnTimeAdditionBed = 0f;

	[ServerVar(Saved = true, ShowInAdminUI = true, Help = "All ammo drops from NPC loot will be multiplied by this")]
	public static float npcAmmoLootMultiplier = 1f;

	[ReplicatedVar(Help = "Multiplies crafting cost of firearm ammunition", Saved = true, ShowInAdminUI = true)]
	public static float hardcoreFirearmAmmunitionCraftingMultiplier = 1f;

	[ServerVar(Help = "Allows radiation to flood monuments to force puzzles to reset")]
	public static bool monumentPuzzleResetRadiation = true;

	[ServerVar]
	public static float monumentPuzzleResetRadiationRadiusMultiplier = 1.5f;

	[ServerVar(Help = "Clamp radiation multiplier to this amount of meters, -1 = ignored")]
	public static float monumentPuzzleResetRadiationMaxRadiusIncrease = 20f;

	[ServerVar(Help = "How long before the reset happens do we start applying radiation")]
	public static float monumentPuzzleResetRadiationPreResetTime = 300f;

	[ServerVar(Help = "How long does a monument puzzle need to be empty with full rads before it can reset")]
	public static float monumentPuzzleResetRadiationPlayerEmptyTime = 120f;

	[ServerVar]
	public static float monumentPuzzleResetRadiationAmount = 3f;

	[ServerVar]
	public static bool drawpuzzleresets = false;

	[ServerVar]
	public static bool pauseunlootedpuzzles = true;

	[ServerVar(Saved = true)]
	public static bool monumentPuzzleResetWarnings = true;

	[ServerVar(Saved = true)]
	public static bool showHolsteredItems = true;

	[ServerVar]
	public static int maxpacketspersecond_world = 1;

	[ServerVar]
	public static int maxpacketspersecond_rpc = 200;

	[ServerVar]
	public static int maxpacketspersecond_rpc_signal = 30;

	[ServerVar]
	public static int maxpacketspersecond_command = 100;

	[ServerVar]
	public static int maxpacketsize_command = 100000;

	[ServerVar]
	public static int maxpacketsize_globaltrees = 100;

	[ServerVar]
	public static int maxpacketsize_globalentities = 1000;

	[ServerVar]
	public static int maxpacketspersecond_tick = 300;

	[ServerVar]
	public static int maxpacketspersecond_voice = 100;

	[ServerVar]
	public static int maxpacketspersecond_syncvar = 200;

	[ServerVar]
	public static bool packetlog_enabled = false;

	[ServerVar]
	public static bool rpclog_enabled = false;

	[ServerVar(Help = "MS per frame to spend warming up entity save caches")]
	public static int saveframebudget = 5;

	[ServerVar(Help = "Player Update parallelism mode. 0 - serial(def); 1 - burst jobs; 2 - 1 + managed tasks")]
	public static int UsePlayerUpdateJobs = 0;

	[ServerVar(Help = "UsePlayerUpdateJobs 2 related - how many snapshot messages to batch into 1 task")]
	public static int SnapshotTaskBatchCount = 64;

	[ServerVar(Help = "UsePlayerUpdateJobs 2 related - how many destroy messages to batch into 1 task")]
	public static int DestroyTaskBatchCount = 128;

	[ServerVar(Help = "Runs extra validation checks to prevent crashes and instead switch back to vanilla processing")]
	public static bool EmergencyDisablePlayerJobs = true;

	[ServerVar(Saved = true)]
	public static string server_id
	{
		get
		{
			return DemoConVars.ServerId;
		}
		set
		{
			DemoConVars.ServerId = value;
		}
	}

	[ServerVar(ShowInAdminUI = true, Saved = true, Help = "Domain name to save when players favorite your server. The port can be omitted if using the default port or a SRV DNS record is created.")]
	public static string favoritesEndpoint
	{
		get
		{
			return _favoritesEndpoint;
		}
		set
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				_favoritesEndpoint = "";
				return;
			}
			value = value.Trim();
			if (value.StartsWith("https://"))
			{
				string text = value;
				int length = "https://".Length;
				value = text.Substring(length, text.Length - length);
			}
			if (value.StartsWith("http://"))
			{
				string text = value;
				int length = "http://".Length;
				value = text.Substring(length, text.Length - length);
			}
			_favoritesEndpoint = value.Trim().ToLowerInvariant();
		}
	}

	[ServerVar]
	public static int anticheatlog
	{
		get
		{
			return (int)EOS.LogLevel;
		}
		set
		{
			EOS.LogLevel = (LogLevel)value;
		}
	}

	[ServerVar]
	public static int http_connection_limit
	{
		get
		{
			return _maxHttp;
		}
		set
		{
			_maxHttp = value;
			HttpManager.UpdateMaxConnections();
		}
	}

	[ServerVar]
	public static string gamemode
	{
		get
		{
			return _gamemode;
		}
		set
		{
			_gamemode = value;
			ApplyGamemode();
		}
	}

	[ServerVar(Help = "Comma-separated server browser tag values (see wiki)", Saved = true, ShowInAdminUI = true)]
	public static string tags
	{
		get
		{
			return _tags;
		}
		set
		{
			_tags = AutoCorrectTags(value);
		}
	}

	[ServerVar]
	public static int maxclientinfosize
	{
		get
		{
			return Connection.MaxClientInfoSize;
		}
		set
		{
			Connection.MaxClientInfoSize = Mathf.Max(value, 1);
		}
	}

	[ServerVar]
	public static int maxconnectionsperip
	{
		get
		{
			return Network.Server.MaxConnectionsPerIP;
		}
		set
		{
			Network.Server.MaxConnectionsPerIP = Mathf.Clamp(value, 1, 1000);
		}
	}

	[ServerVar]
	public static int maxreceivetime
	{
		get
		{
			return Network.Server.MaxReceiveTime;
		}
		set
		{
			Network.Server.MaxReceiveTime = Mathf.Clamp(value, 10, 1000);
		}
	}

	[ServerVar]
	public static int maxmainthreadwait
	{
		get
		{
			return Network.Server.MaxMainThreadWait;
		}
		set
		{
			Network.Server.MaxMainThreadWait = Mathf.Clamp(value, 1, 1000);
		}
	}

	[ServerVar]
	public static int maxreadthreadwait
	{
		get
		{
			return Network.Server.MaxReadThreadWait;
		}
		set
		{
			Network.Server.MaxReadThreadWait = Mathf.Clamp(value, 1, 1000);
		}
	}

	[ServerVar]
	public static int maxwritethreadwait
	{
		get
		{
			return Network.Server.MaxWriteThreadWait;
		}
		set
		{
			Network.Server.MaxWriteThreadWait = Mathf.Clamp(value, 1, 1000);
		}
	}

	[ServerVar]
	public static int maxdecryptthreadwait
	{
		get
		{
			return Network.Server.MaxDecryptThreadWait;
		}
		set
		{
			Network.Server.MaxDecryptThreadWait = Mathf.Clamp(value, 1, 1000);
		}
	}

	[ServerVar]
	public static int maxreadqueuelength
	{
		get
		{
			return Network.Server.MaxReadQueueLength;
		}
		set
		{
			Network.Server.MaxReadQueueLength = Mathf.Max(value, 1);
		}
	}

	[ServerVar]
	public static int maxwritequeuelength
	{
		get
		{
			return Network.Server.MaxWriteQueueLength;
		}
		set
		{
			Network.Server.MaxWriteQueueLength = Mathf.Max(value, 1);
		}
	}

	[ServerVar]
	public static int maxdecryptqueuelength
	{
		get
		{
			return Network.Server.MaxDecryptQueueLength;
		}
		set
		{
			Network.Server.MaxDecryptQueueLength = Mathf.Max(value, 1);
		}
	}

	[ServerVar]
	public static int maxreadqueuebytes
	{
		get
		{
			return Network.Server.MaxReadQueueBytes;
		}
		set
		{
			Network.Server.MaxReadQueueBytes = Mathf.Max(value, 1);
		}
	}

	[ServerVar]
	public static int maxwritequeuebytes
	{
		get
		{
			return Network.Server.MaxWriteQueueBytes;
		}
		set
		{
			Network.Server.MaxWriteQueueBytes = Mathf.Max(value, 1);
		}
	}

	[ServerVar]
	public static int maxdecryptqueuebytes
	{
		get
		{
			return Network.Server.MaxDecryptQueueBytes;
		}
		set
		{
			Network.Server.MaxDecryptQueueBytes = Mathf.Max(value, 1);
		}
	}

	[ServerVar]
	public static int player_state_cache_size
	{
		get
		{
			return SingletonComponent<ServerMgr>.Instance?.playerStateManager.CacheSize ?? 0;
		}
		set
		{
			SingletonComponent<ServerMgr>.Instance.playerStateManager.CacheSize = value;
		}
	}

	[ServerVar]
	public static int maxpacketspersecond
	{
		get
		{
			return (int)Network.Server.MaxPacketsPerSecond;
		}
		set
		{
			Network.Server.MaxPacketsPerSecond = (ulong)Mathf.Clamp(value, 1, 1000000);
		}
	}

	public static string rootFolder => "server/" + identity;

	public static string filesStorageFolder
	{
		get
		{
			if (!string.IsNullOrEmpty(filefolderoverride))
			{
				return filefolderoverride;
			}
			return rootFolder;
		}
	}

	public static string backupFolder => "backup/0/" + identity;

	public static string backupFolder1 => "backup/1/" + identity;

	public static string backupFolder2 => "backup/2/" + identity;

	public static string backupFolder3 => "backup/3/" + identity;

	[ServerVar]
	public static bool compression
	{
		get
		{
			if (Network.Net.sv == null)
			{
				return false;
			}
			return Network.Net.sv.compressionEnabled;
		}
		set
		{
			Network.Net.sv.compressionEnabled = value;
		}
	}

	[ServerVar]
	public static bool netlog
	{
		get
		{
			if (Network.Net.sv == null)
			{
				return false;
			}
			return Network.Net.sv.logging;
		}
		set
		{
			Network.Net.sv.logging = value;
		}
	}

	public static bool UsePlayerTasks => UsePlayerUpdateJobs >= 2;

	[ReplicatedVar(Name = "era", Help = "none,primitive,medieval,frontier,rust")]
	public static string era
	{
		get
		{
			return Era.ToString();
		}
		set
		{
			if (string.IsNullOrEmpty(value) && Era != Era.None)
			{
				Era = Era.None;
				OnEraChanged();
				return;
			}
			Era era = Era;
			switch (value.ToLower())
			{
			case "unknown":
			case "none":
				Era = Era.None;
				break;
			case "primitive":
				Era = Era.Primitive;
				break;
			case "siege":
			case "medieval":
				Era = Era.Medieval;
				break;
			case "frontier":
				Era = Era.Frontier;
				break;
			case "modern":
			case "rust":
				Era = Era.Modern;
				break;
			}
			if (era != Era)
			{
				OnEraChanged();
			}
		}
	}

	public static Era Era { get; private set; }

	private static void ApplyGamemode()
	{
		GameModeManifest gameModeManifest = GameModeManifest.Get();
		if (gameModeManifest == null)
		{
			UnityEngine.Debug.LogError("No GameModeManifest found");
			return;
		}
		foreach (GameObjectRef gameModePrefab in gameModeManifest.gameModePrefabs)
		{
			BaseGameMode baseGameMode = gameModePrefab?.Get()?.GetComponent<BaseGameMode>();
			if (baseGameMode.shortname == gamemode)
			{
				baseGameMode.ApplyConVars();
				return;
			}
		}
		UnityEngine.Debug.LogWarning("Couldn't find gamemode: " + gamemode);
	}

	public static float GetTaxRateForWorkbenchUnlock(int workbenchLevel)
	{
		float value = 0f;
		switch (workbenchLevel)
		{
		case 0:
			value = workbenchTaxRate1;
			break;
		case 1:
			value = workbenchTaxRate2;
			break;
		case 2:
			value = workbenchTaxRate3;
			break;
		}
		return Mathf.Clamp(value, 0f, 100f);
	}

	public static float TickDelta()
	{
		return 1f / (float)tickrate;
	}

	public static float TickTime(uint tick)
	{
		return (float)((double)TickDelta() * (double)tick);
	}

	[ServerVar(Help = "Show holstered items on player bodies")]
	public static void setshowholstereditems(Arg arg)
	{
		showHolsteredItems = arg.GetBool(0, showHolsteredItems);
		foreach (BasePlayer activePlayer in BasePlayer.activePlayerList)
		{
			activePlayer.inventory.UpdatedVisibleHolsteredItems();
		}
		foreach (BasePlayer sleepingPlayer in BasePlayer.sleepingPlayerList)
		{
			sleepingPlayer.inventory.UpdatedVisibleHolsteredItems();
		}
	}

	[ServerVar]
	public static void player_state_cache_count(Arg args)
	{
		args.ReplyWith(SingletonComponent<ServerMgr>.Instance.playerStateManager.CacheCount);
	}

	[ServerVar]
	public static void player_state_cache_evictions(Arg args)
	{
		args.ReplyWith(SingletonComponent<ServerMgr>.Instance.playerStateManager.CacheEvictions);
	}

	[ServerVar]
	public static string printreadqueue(Arg arg)
	{
		return "Server read queue: " + Network.Net.sv.ReadQueueLength + " items / " + Network.Net.sv.ReadQueueBytes.FormatBytes();
	}

	[ServerVar]
	public static string printwritequeue(Arg arg)
	{
		return "Server write queue: " + Network.Net.sv.WriteQueueLength + " items / " + Network.Net.sv.WriteQueueBytes.FormatBytes();
	}

	[ServerVar]
	public static string printdecryptqueue(Arg arg)
	{
		return "Server decrypt queue: " + Network.Net.sv.DecryptQueueLength + " items / " + Network.Net.sv.DecryptQueueBytes.FormatBytes();
	}

	[ServerVar]
	public static string packetlog(Arg arg)
	{
		if (!packetlog_enabled)
		{
			return "Packet log is not enabled.";
		}
		List<Tuple<Message.Type, ulong>> list = new List<Tuple<Message.Type, ulong>>();
		foreach (KeyValuePair<Message.Type, TimeAverageValue> item in SingletonComponent<ServerMgr>.Instance.packetHistory.dict)
		{
			list.Add(new Tuple<Message.Type, ulong>(item.Key, item.Value.Calculate()));
		}
		bool flag = arg.HasArg("--json");
		using TextTable textTable = Facepunch.Pool.Get<TextTable>();
		textTable.ShouldPadColumns = !flag;
		textTable.AddColumn("type");
		textTable.AddColumn("calls");
		foreach (Tuple<Message.Type, ulong> item2 in list.OrderByDescending((Tuple<Message.Type, ulong> entry) => entry.Item2))
		{
			if (item2.Item2 == 0L)
			{
				break;
			}
			string text = item2.Item1.ToString();
			string text2 = item2.Item2.ToString();
			textTable.AddRow(text, text2);
		}
		return flag ? textTable.ToJson() : textTable.ToString();
	}

	[ServerVar]
	public static string rpclog(Arg arg)
	{
		if (!rpclog_enabled)
		{
			return "RPC log is not enabled.";
		}
		List<Tuple<uint, ulong>> list = new List<Tuple<uint, ulong>>();
		foreach (KeyValuePair<uint, TimeAverageValue> item in SingletonComponent<ServerMgr>.Instance.rpcHistory.dict)
		{
			list.Add(new Tuple<uint, ulong>(item.Key, item.Value.Calculate()));
		}
		using TextTable textTable = Facepunch.Pool.Get<TextTable>();
		textTable.AddColumn("id");
		textTable.AddColumn("name");
		textTable.AddColumn("calls");
		foreach (Tuple<uint, ulong> item2 in list.OrderByDescending((Tuple<uint, ulong> entry) => entry.Item2))
		{
			if (item2.Item2 == 0L)
			{
				break;
			}
			string text = item2.Item1.ToString();
			string text2 = StringPool.Get(item2.Item1);
			string text3 = item2.Item2.ToString();
			textTable.AddRow(text, text2, text3);
		}
		return textTable.ToString();
	}

	[ServerVar(Help = "Starts a server")]
	public static void start(Arg arg)
	{
		if (Network.Net.sv.IsConnected())
		{
			arg.ReplyWith("There is already a server running!");
			return;
		}
		string strLevelName = arg.GetString(0, level);
		if (!LevelManager.IsValid(strLevelName))
		{
			arg.ReplyWith("Level '" + strLevelName + "' isn't valid!");
			return;
		}
		if ((bool)UnityEngine.Object.FindObjectOfType<ServerMgr>())
		{
			arg.ReplyWith("There is already a server running!");
			return;
		}
		UnityEngine.Object.DontDestroyOnLoad(GameManager.server.CreatePrefab("assets/bundled/prefabs/system/shared.prefab"));
		UnityEngine.Object.DontDestroyOnLoad(GameManager.server.CreatePrefab("assets/bundled/prefabs/system/server.prefab"));
		Rust.Global.Runner.StartCoroutine(LoadImpl());
		IEnumerator LoadImpl()
		{
			yield return LevelManager.LoadLevelAsync(strLevelName);
		}
	}

	[ServerVar(Help = "Stops a server")]
	public static void stop(Arg arg)
	{
		if (!Network.Net.sv.IsConnected())
		{
			arg.ReplyWith("There isn't a server running!");
		}
		else
		{
			Network.Net.sv.Stop(arg.GetString(0, "Stopping Server"));
		}
	}

	[ServerVar(Help = "Backup server folder")]
	public static void backup()
	{
		DirectoryEx.Backup(backupFolder, backupFolder1, backupFolder2, backupFolder3);
		DirectoryEx.CopyAll(rootFolder, backupFolder);
	}

	public static string GetServerFolder(string folder)
	{
		string text = rootFolder + "/" + folder;
		if (Directory.Exists(text))
		{
			return text;
		}
		Directory.CreateDirectory(text);
		return text;
	}

	[ServerVar(Help = "Writes config files")]
	public static void writecfg(Arg arg)
	{
		string contents = ConsoleSystem.SaveToConfigString(bServer: true);
		File.WriteAllText(GetServerFolder("cfg") + "/serverauto.cfg", contents);
		ServerUsers.Save();
		arg.ReplyWith("Config Saved");
	}

	[ServerVar]
	public static void fps(Arg arg)
	{
		arg.ReplyWith(Performance.report.frameRate + " FPS");
	}

	[ServerVar(Help = "Force save the current game")]
	public static void save(Arg arg)
	{
		Stopwatch stopwatch = Stopwatch.StartNew();
		foreach (BaseEntity save in BaseEntity.saveList)
		{
			save.InvalidateNetworkCache();
		}
		UnityEngine.Debug.Log("Invalidate Network Cache took " + stopwatch.Elapsed.TotalSeconds.ToString("0.00") + " seconds");
		SaveRestore.Save(AndWait: true);
	}

	[ServerVar]
	public static string readcfg(Arg arg)
	{
		string serverFolder = GetServerFolder("cfg");
		if (File.Exists(serverFolder + "/serverauto.cfg"))
		{
			string strFile = File.ReadAllText(serverFolder + "/serverauto.cfg");
			ConsoleSystem.RunFile(Option.Server.Quiet(), strFile);
		}
		if (File.Exists(serverFolder + "/server.cfg"))
		{
			string strFile2 = File.ReadAllText(serverFolder + "/server.cfg");
			ConsoleSystem.RunFile(Option.Server.Quiet(), strFile2);
		}
		return "Server Config Loaded";
	}

	[ServerVar]
	public static string netprotocol(Arg arg)
	{
		if (Network.Net.sv == null)
		{
			return string.Empty;
		}
		return Network.Net.sv.ProtocolId;
	}

	[ServerUserVar]
	public static void cheatreport(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (!(basePlayer == null))
		{
			string text = arg.GetUInt64(0, 0uL).ToString();
			string text2 = arg.GetString(1);
			UnityEngine.Debug.LogWarning(basePlayer?.ToString() + " reported " + text + ": " + text2.ToPrintable(140));
			EACServer.SendPlayerBehaviorReport(basePlayer, PlayerReportsCategory.Cheating, text, text2);
		}
	}

	[ServerVar(Help = "Get info on player corpses on the server")]
	public static void corpseinfo(Arg arg)
	{
		PlayerCorpse[] array = BaseNetworkable.serverEntities.OfType<PlayerCorpse>().ToArray();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		PlayerCorpse[] array2 = array;
		foreach (PlayerCorpse playerCorpse in array2)
		{
			if (playerCorpse.isClient)
			{
				continue;
			}
			num++;
			if (playerCorpse.CorpseIsRagdoll)
			{
				num2++;
				if (playerCorpse.CorpseRagdollScript.IsKinematic)
				{
					num3++;
				}
				else if (playerCorpse.CorpseRagdollScript.IsFullySleeping())
				{
					num4++;
				}
			}
		}
		int num5 = num2 - num3 - num4;
		float num6 = ((num2 > 0) ? ((float)num5 / (float)num2) : 0f);
		string strValue = $"Found {num} player corpses in the world, " + $"of which {num2} are using server-side ragdolls. " + $"{num5} of those are active ({num6:0%}), {num4} are sleeping, and {num3} are kinematic.";
		arg.ReplyWith(strValue);
	}

	[ServerAllVar(Help = "Get the player combat log")]
	public static string combatlog(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (arg.HasArgs() && arg.IsAdmin)
		{
			basePlayer = ArgEx.GetPlayerOrSleeper(arg, 0);
		}
		if (basePlayer == null || basePlayer.net == null)
		{
			return "invalid player";
		}
		CombatLog combat = basePlayer.stats.combat;
		int count = combatlogsize;
		bool json = arg.HasArg("--json");
		bool isAdmin = arg.IsAdmin;
		ulong requestingUser = arg.Connection?.userid ?? 0;
		return combat.Get(count, default(NetworkableId), json, isAdmin, requestingUser);
	}

	[ServerAllVar(Help = "Get the player combat log, only showing outgoing damage")]
	public static string combatlog_outgoing(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (arg.HasArgs() && arg.IsAdmin)
		{
			basePlayer = ArgEx.GetPlayerOrSleeper(arg, 0);
		}
		if (basePlayer == null)
		{
			return "invalid player";
		}
		return basePlayer.stats.combat.Get(combatlogsize, basePlayer.net.ID, arg.HasArg("--json"), arg.IsAdmin, arg.Connection?.userid ?? 0);
	}

	[ServerVar(Help = "Print the current player position.")]
	public static string printpos(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (arg.HasArgs())
		{
			basePlayer = ArgEx.GetPlayerOrSleeper(arg, 0);
		}
		if (!(basePlayer == null))
		{
			return basePlayer.transform.position.ToString();
		}
		return "invalid player";
	}

	[ServerVar(Help = "Print the current player center position.")]
	public static string printposcenter(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (arg.HasArgs())
		{
			basePlayer = ArgEx.GetPlayerOrSleeper(arg, 0);
		}
		if (!(basePlayer == null))
		{
			return basePlayer.GetCenter(ducked: false).ToString();
		}
		return "invalid player";
	}

	[ServerVar(Help = "Print the current player rotation.")]
	public static string printrot(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (arg.HasArgs())
		{
			basePlayer = ArgEx.GetPlayerOrSleeper(arg, 0);
		}
		if (!(basePlayer == null))
		{
			return basePlayer.transform.rotation.eulerAngles.ToString();
		}
		return "invalid player";
	}

	[ServerVar(Help = "Print the current player eyes.")]
	public static string printeyes(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (arg.HasArgs())
		{
			basePlayer = ArgEx.GetPlayerOrSleeper(arg, 0);
		}
		if (!(basePlayer == null))
		{
			return basePlayer.eyes.rotation.eulerAngles.ToString();
		}
		return "invalid player";
	}

	[ServerVar(ServerAdmin = true, Help = "This sends a snapshot of all the entities in the client's pvs. This is mostly redundant, but we request this when the client starts recording a demo.. so they get all the information.")]
	public static void snapshot(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (!(basePlayer == null))
		{
			UnityEngine.Debug.Log("Sending full snapshot to " + basePlayer);
			basePlayer.SendNetworkUpdateImmediate();
			basePlayer.SendGlobalSnapshot();
			basePlayer.SendFullSnapshot();
			basePlayer.SendEntityUpdate();
			TreeManager.SendSnapshot(basePlayer);
			ServerMgr.SendReplicatedVars(basePlayer.net.connection);
		}
	}

	[ServerVar(Help = "Send network update for all players")]
	public static void sendnetworkupdate(Arg arg)
	{
		foreach (BasePlayer activePlayer in BasePlayer.activePlayerList)
		{
			activePlayer.SendNetworkUpdate();
		}
	}

	public static void GetPlayerListPosTable(TextTable table)
	{
		table.ResizeColumns(4);
		table.AddColumn("SteamID");
		table.AddColumn("DisplayName");
		table.AddColumn("POS");
		table.AddColumn("ROT");
		table.ResizeRows(BasePlayer.activePlayerList.Count);
		foreach (BasePlayer activePlayer in BasePlayer.activePlayerList)
		{
			table.AddValue(activePlayer.userID.Get());
			table.AddValue(activePlayer.displayName);
			table.AddValue(activePlayer.transform.position);
			table.AddValue(activePlayer.eyes.BodyForward());
		}
	}

	[ServerVar(Help = "Prints the position of all players on the server")]
	public static void playerlistpos(Arg arg)
	{
		bool flag = arg.HasArg("--json");
		using TextTable textTable = Facepunch.Pool.Get<TextTable>();
		textTable.ShouldPadColumns = !flag;
		GetPlayerListPosTable(textTable);
		arg.ReplyWith(flag ? textTable.ToJson() : textTable.ToString());
	}

	[ServerVar(Help = "Prints all the vending machines on the server")]
	public static void listvendingmachines(Arg arg)
	{
		bool flag = arg.HasArg("--json");
		using TextTable textTable = Facepunch.Pool.Get<TextTable>();
		textTable.ShouldPadColumns = !flag;
		textTable.AddColumns("EntityId", "Position", "Name");
		foreach (VendingMachine item in BaseNetworkable.serverEntities.OfType<VendingMachine>())
		{
			textTable.AddRow(item.net.ID.ToString(), item.transform.position.ToString(), item.shopName.QuoteSafe());
		}
		arg.ReplyWith(flag ? textTable.ToJson() : textTable.ToString());
	}

	[ServerVar(Help = "Prints all the Tool Cupboards on the server")]
	public static void listtoolcupboards(Arg arg)
	{
		bool flag = arg.HasArg("--json");
		using TextTable textTable = Facepunch.Pool.Get<TextTable>();
		textTable.ShouldPadColumns = !flag;
		textTable.AddColumns("EntityId", "Position", "Authed");
		foreach (BuildingPrivlidge item in BaseNetworkable.serverEntities.OfType<BuildingPrivlidge>())
		{
			textTable.AddRow(item.net.ID.ToString(), item.transform.position.ToString(), item.authorizedPlayers.Count.ToString());
		}
		arg.ReplyWith(flag ? textTable.ToJson() : textTable.ToString());
	}

	[ServerVar]
	public static void BroadcastPlayVideo(Arg arg)
	{
		string text = arg.GetString(0);
		if (string.IsNullOrWhiteSpace(text))
		{
			arg.ReplyWith("Missing video URL");
			return;
		}
		foreach (BasePlayer activePlayer in BasePlayer.activePlayerList)
		{
			activePlayer.Command("client.playvideo", text);
		}
		arg.ReplyWith($"Sent video to {BasePlayer.activePlayerList.Count} players");
	}

	[ServerVar(Help = "Rescans the serveremoji folder, note that clients will need to reconnect to get the latest emoji")]
	public static void ResetServerEmoji()
	{
		RustEmojiLibrary.ResetServerEmoji();
	}

	[ServerVar]
	public static string BotCount()
	{
		return BasePlayer.bots.Count.ToString();
	}

	[ServerVar(Help = "Prints the current wipe id of the sav")]
	public static void printwipeid(Arg arg)
	{
		if (string.IsNullOrEmpty(SaveRestore.WipeId))
		{
			arg.ReplyWith("ERROR: wipe ID is null or empty!");
		}
		else
		{
			arg.ReplyWith(SaveRestore.WipeId);
		}
	}

	[ServerVar(Help = "Clears the loot spawn cache used to restrict loot into each era")]
	public static void clear_loot_spawn_cache(Arg arg)
	{
		LootContainer[] source = (from x in GameManager.server.preProcessed.prefabList.Values
			select x.GetComponent<LootContainer>() into x
			where x != null
			select x).ToArray();
		LootSpawn[] array = (from x in source.Select((LootContainer x) => x.lootDefinition).Concat(from x in source.SelectMany((LootContainer x) => x.LootSpawnSlots)
				select x.definition)
			where x != null
			select x).ToArray();
		LootSpawn[] array2 = array;
		for (int num = 0; num < array2.Length; num++)
		{
			array2[num].ClearCache();
		}
		arg.ReplyWith($"Cleared {array.Length} loot spawn caches");
	}

	[ServerVar]
	public static void clear_trees_radius(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		float num = arg.GetFloat(0);
		Vector3 position = ((basePlayer != null) ? basePlayer.transform.position : Vector3.zero);
		if (arg.HasArgs(2))
		{
			position = arg.GetVector3(1);
		}
		int num2 = 0;
		if (basePlayer != null)
		{
			List<TreeEntity> list = Facepunch.Pool.Get<List<TreeEntity>>();
			global::Vis.Entities(position, num, list, 1073741824);
			foreach (TreeEntity item in list)
			{
				item.Kill();
				num2++;
			}
		}
		arg.ReplyWith($"Deleted {num2} server tree entities within {num}m");
	}

	[ServerVar]
	public static void clear_bushes_radius(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		float num = arg.GetFloat(0);
		Vector3 position = ((basePlayer != null) ? basePlayer.transform.position : Vector3.zero);
		if (arg.HasArgs(2))
		{
			position = arg.GetVector3(1);
		}
		int num2 = 0;
		if (basePlayer != null)
		{
			using PooledList<BushEntity> pooledList = Facepunch.Pool.Get<PooledList<BushEntity>>();
			global::Vis.Entities(position, num, pooledList, 67108864);
			foreach (BushEntity item in pooledList)
			{
				item.Kill();
				num2++;
			}
		}
		arg.ReplyWith($"Deleted {num2} server bush entities within {num}m");
	}

	[ServerVar(Help = "Deletes items on the server that are not allowed in the era")]
	public static void enforce_era_restrictions(Arg arg)
	{
		int num = 0;
		int num2 = 0;
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		foreach (Item allItem in ItemManager.GetAllItems())
		{
			num++;
			if (!allItem.info.IsAllowed(EraRestriction.Default))
			{
				if (!dictionary.ContainsKey(allItem.info.shortname))
				{
					dictionary.Add(allItem.info.shortname, allItem.amount);
				}
				else
				{
					dictionary[allItem.info.shortname] += allItem.amount;
				}
				allItem.Remove();
				num2++;
			}
		}
		ItemManager.DoRemoves();
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine($"Iterated '{num}' items and removed '{num2}' restricted items");
		foreach (KeyValuePair<string, int> item in dictionary)
		{
			stringBuilder.AppendLine($"{item.Key}: {item.Value}");
		}
		arg.ReplyWith(stringBuilder.ToString());
	}

	[ServerVar]
	public static void fillChickenCoop(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (basePlayer == null)
		{
			return;
		}
		using PooledList<ChickenCoop> pooledList = Facepunch.Pool.Get<PooledList<ChickenCoop>>();
		global::Vis.Entities(basePlayer.transform.position, 5f, pooledList, 256);
		foreach (ChickenCoop item in pooledList)
		{
			if (item.isServer)
			{
				item.DebugFillCoop();
			}
		}
	}

	[ServerVar(Help = "Unlock all static respawn points")]
	public static void unlockrespawns(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.GetPlayer(arg, 0);
		if (basePlayer == null)
		{
			if (arg.HasArgs())
			{
				arg.ReplyWith("Can't find player");
				return;
			}
			basePlayer = ArgEx.Player(arg);
		}
		foreach (SleepingBag sleepingBag in SleepingBag.sleepingBags)
		{
			if (sleepingBag is StaticRespawnArea staticRespawnArea && !staticRespawnArea.IsAuthed(basePlayer.userID))
			{
				staticRespawnArea.Authorize(basePlayer.userID);
			}
		}
	}

	[ServerVar(Help = "Clear all static respawn points")]
	public static void resetrespawns(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.GetPlayer(arg, 0);
		if (basePlayer == null)
		{
			if (arg.HasArgs())
			{
				arg.ReplyWith("Can't find player");
				return;
			}
			basePlayer = ArgEx.Player(arg);
		}
		foreach (SleepingBag sleepingBag in SleepingBag.sleepingBags)
		{
			if (sleepingBag is StaticRespawnArea staticRespawnArea && staticRespawnArea.IsAuthed(basePlayer.userID))
			{
				staticRespawnArea.Deauthorize(basePlayer.userID);
			}
		}
	}

	public static void GetPlayerReportsListTable(TextTable table)
	{
		table.ResizeColumns(4);
		table.AddColumn("NumReports");
		table.AddColumn("UserID");
		table.AddColumn("DisplayName");
		table.AddColumn("IsConnected");
		foreach (BasePlayer item in BasePlayer.allPlayerList.OrderByDescending((BasePlayer x) => x.State.numberOfTimesReported))
		{
			if (item.State.numberOfTimesReported >= 1)
			{
				table.AddValue(item.State.numberOfTimesReported);
				table.AddValue(item.userID);
				table.AddValue(item.displayName);
				table.AddValue(item.IsConnected);
			}
		}
	}

	[ServerVar(Help = "List the amount of reports players on the server have received")]
	public static void listplayerreportcounts(Arg arg)
	{
		bool flag = arg.HasArg("--json");
		using TextTable textTable = Facepunch.Pool.Get<TextTable>();
		textTable.ShouldPadColumns = !flag;
		GetPlayerReportsListTable(textTable);
		arg.ReplyWith(flag ? textTable.ToJson() : textTable.ToString());
	}

	[ServerVar(Help = "Clear the player reports list")]
	public static void clearplayerreportcounts(Arg arg)
	{
		foreach (BasePlayer allPlayer in BasePlayer.allPlayerList)
		{
			allPlayer.State.numberOfTimesReported = 0;
		}
		arg.ReplyWith("Cleared report counts");
	}

	private static void OnEraChanged()
	{
		foreach (BaseNetworkable serverEntity in BaseNetworkable.serverEntities)
		{
			if (serverEntity is LootContainer lootContainer)
			{
				lootContainer.PopulateLoot();
			}
		}
		SingletonComponent<SpawnHandler>.Instance?.EnforceLimits();
	}

	private static string AutoCorrectTags(string value)
	{
		List<string> inputValues = (from s in value.Split(',', StringSplitOptions.RemoveEmptyEntries)
			select s.Trim().ToLowerInvariant()).ToList();
		List<string> outputValues = new List<string>();
		Add(new string[3] { "monthly", "biweekly", "weekly" });
		Add(new string[3] { "vanilla", "hardcore", "softcore" });
		Add(new string[1] { "roleplay" });
		Add(new string[1] { "creative" });
		Add(new string[1] { "minigame" });
		Add(new string[1] { "training" });
		Add(new string[1] { "battlefield" });
		Add(new string[1] { "broyale" });
		Add(new string[1] { "builds" });
		Add(new string[7] { "NA", "SA", "EU", "WA", "EA", "OC", "AF" });
		Add(new string[1] { "tut" });
		Add(new string[1] { "premium" });
		if (!pve)
		{
			Add(new string[1] { "pve" });
		}
		return string.Join(',', outputValues);
		void Add(string[] options)
		{
			if (outputValues.Count < 4)
			{
				foreach (string text in options)
				{
					if (inputValues.Contains(text, StringComparer.InvariantCultureIgnoreCase))
					{
						outputValues.Add(text);
						break;
					}
				}
			}
		}
	}
}
