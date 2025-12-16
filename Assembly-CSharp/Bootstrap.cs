using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CompanionServer;
using CompanionServer.Handlers;
using ConVar;
using Development.Attributes;
using Facepunch;
using Facepunch.Network;
using Facepunch.Network.Raknet;
using Facepunch.Rust;
using Facepunch.Rust.Profiling;
using Facepunch.Utility;
using Network;
using Oxide.Core;
using ProtoBuf;
using Rust;
using Rust.Ai;
using Rust.UI;
using UnityEngine;
using UnityEngine.AI;

[ResetStaticFields]
public class Bootstrap : SingletonComponent<Bootstrap>
{
	internal static bool bootstrapInitRun;

	public static bool isErrored;

	public Translate.Phrase currentLoadingPhrase;

	public CanvasGroup BootstrapUiCanvas;

	public GameObject errorPanel;

	public RustText errorText;

	public RustText statusText;

	private Translate.Phrase openingBundles = new Translate.Phrase("bootstrap.openingbundles", "Opening Bundles");

	private static string lastWrittenValue;

	public static bool needsSetup => !bootstrapInitRun;

	public static bool isPresent
	{
		get
		{
			if (bootstrapInitRun)
			{
				return true;
			}
			if (UnityEngine.Object.FindObjectsOfType<GameSetup>().Count() > 0)
			{
				return true;
			}
			return false;
		}
	}

	public static void RunDefaults()
	{
		Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
		Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
		UnityEngine.Application.targetFrameRate = 256;
		UnityEngine.Time.fixedDeltaTime = 0.0625f;
		UnityEngine.Time.maximumDeltaTime = 0.125f;
	}

	public static void Init_Tier0()
	{
		RunDefaults();
		GameSetup.RunOnce = true;
		bootstrapInitRun = true;
		ConsoleSystem.Index.Initialize(ConsoleGen.All);
		ConsoleSystem.Index.Reset();
		UnityButtons.Register();
		Output.Install();
		Facepunch.Pool.ResizeBuffer<NetRead>(16384);
		Facepunch.Pool.ResizeBuffer<NetWrite>(16384);
		Facepunch.Pool.ResizeBuffer<BufferStream>(32768);
		Facepunch.Pool.ResizeBuffer<Networkable>(65536);
		Facepunch.Pool.ResizeBuffer<EntityLink>(65536);
		Facepunch.Pool.ResizeBuffer<EventRecord>(16384);
		Facepunch.Pool.ResizeBuffer<AppMarker.SellOrder>(2048);
		Facepunch.Pool.FillBuffer<Networkable>();
		Facepunch.Pool.FillBuffer<EntityLink>();
		if (Facepunch.CommandLine.HasSwitch("-nonetworkthread"))
		{
			BaseNetwork.Multithreading = false;
		}
		SteamNetworking.SetDebugFunction();
		if (Facepunch.CommandLine.HasSwitch("-swnet"))
		{
			NetworkInitSteamworks(enableSteamDatagramRelay: false);
		}
		else if (Facepunch.CommandLine.HasSwitch("-sdrnet"))
		{
			NetworkInitSteamworks(enableSteamDatagramRelay: true);
		}
		else if (Facepunch.CommandLine.HasSwitch("-raknet"))
		{
			NetworkInitRaknet();
		}
		else
		{
			NetworkInitRaknet();
		}
		if (!UnityEngine.Application.isEditor)
		{
			string text = Facepunch.CommandLine.Full.Replace(Facepunch.CommandLine.GetSwitch("-rcon.password", Facepunch.CommandLine.GetSwitch("+rcon.password", "RCONPASSWORD")), "******");
			WriteToLog("Command Line: " + text);
		}
		Interface.Initialize();
		int parentProcessId = Facepunch.CommandLine.GetSwitchInt("-parent-pid", 0);
		if (parentProcessId != 0)
		{
			try
			{
				SynchronizationContext syncContext = SynchronizationContext.Current;
				Process processById = Process.GetProcessById(parentProcessId);
				processById.EnableRaisingEvents = true;
				processById.Exited += delegate
				{
					syncContext.Post(delegate
					{
						WriteToLog($"Parent process ID {parentProcessId} exited. Exiting the server now...");
						ConsoleSystem.Run(ConsoleSystem.Option.Server, "quit");
					}, null);
				};
				WriteToLog($"Watching parent process ID {parentProcessId}...");
			}
			catch (ArgumentException)
			{
				WriteToLog($"Parent process ID {parentProcessId} has exited during boot! Exiting now...");
				Rust.Application.Quit();
			}
		}
		UnityHookHandler.EnsureCreated();
	}

	public static void Init_Systems()
	{
		Rust.Global.Init();
		Translate.Init();
		Integration integration = new Integration();
		integration.OnManifestUpdated += CpuAffinity.Apply;
		Facepunch.Application.Initialize(integration);
		Facepunch.Performance.GetMemoryUsage = () => SystemInfoEx.systemMemoryUsed;
	}

	public static void Init_Config()
	{
		ConsoleNetwork.Init();
		ConsoleSystem.UpdateValuesFromCommandLine();
		ConsoleSystem.Run(ConsoleSystem.Option.Server, "server.readcfg");
		ServerUsers.Load();
		if (string.IsNullOrEmpty(ConVar.Server.server_id))
		{
			ConVar.Server.server_id = Guid.NewGuid().ToString("N");
			ConsoleSystem.Run(ConsoleSystem.Option.Server, "server.writecfg");
		}
		if (Facepunch.CommandLine.HasSwitch("-disable-server-occlusion"))
		{
			ServerOcclusion.OcclusionEnabled = false;
			ServerOcclusion.OcclusionIncludeRocks = false;
		}
		if (Facepunch.CommandLine.HasSwitch("-disable-server-occlusion-rocks"))
		{
			ServerOcclusion.OcclusionIncludeRocks = false;
		}
		HttpManager.UpdateMaxConnections();
		if (!RuntimeProfiler.runtime_profiling_persist)
		{
			RuntimeProfiler.Disable();
		}
		if (!Facepunch.CommandLine.HasSwitch("-disableconsolelog"))
		{
			ConsoleSystem.loggingEnabled = true;
		}
		ConsoleSystem.IdentityDirectory = ConVar.Server.rootFolder;
	}

	public static void NetworkInitRaknet()
	{
		Network.Net.sv = new Facepunch.Network.Raknet.Server();
	}

	public static void NetworkInitSteamworks(bool enableSteamDatagramRelay)
	{
		Network.Net.sv = new SteamNetworking.Server(enableSteamDatagramRelay);
	}

	private IEnumerator Start()
	{
		WriteToLog("Bootstrap Startup");
		EarlyInitialize();
		BenchmarkTimer.Enabled = Facepunch.Utility.CommandLine.Full.Contains("+autobench");
		Stopwatch timer = BenchmarkTimer.Get("bootstrap");
		timer?.Start();
		if (!UnityEngine.Application.isEditor)
		{
			BuildInfo current = BuildInfo.Current;
			if ((current.Scm.Branch == null || !(current.Scm.Branch == "experimental/release")) && !(current.Scm.Branch == "release"))
			{
				ExceptionReporter.InitializeFromUrl("https://0654eb77d1e04d6babad83201b6b6b95:d2098f1d15834cae90501548bd5dbd0d@sentry.io/1836389");
			}
			else
			{
				ExceptionReporter.InitializeFromUrl("https://83df169465e84da091c1a3cd2fbffeee:3671b903f9a840ecb68411cf946ab9b6@sentry.io/51080");
			}
			bool num = Facepunch.Utility.CommandLine.Full.Contains("-official") || Facepunch.Utility.CommandLine.Full.Contains("-server.official") || Facepunch.Utility.CommandLine.Full.Contains("+official") || Facepunch.Utility.CommandLine.Full.Contains("+server.official");
			bool flag = Facepunch.Utility.CommandLine.Full.Contains("-stats") || Facepunch.Utility.CommandLine.Full.Contains("-server.stats") || Facepunch.Utility.CommandLine.Full.Contains("+stats") || Facepunch.Utility.CommandLine.Full.Contains("+server.stats");
			ExceptionReporter.Disabled = !(num && flag);
		}
		if (AssetBundleBackend.Enabled)
		{
			AssetBundleBackend newBackend = new AssetBundleBackend();
			using (BenchmarkTimer.Measure("bootstrap;bundles"))
			{
				yield return StartCoroutine(LoadingUpdate(openingBundles));
				newBackend.Load("Bundles/Bundles");
				FileSystem.Backend = newBackend;
			}
			if (FileSystem.Backend.isError)
			{
				ThrowError(FileSystem.Backend.loadingError);
				yield break;
			}
			using (BenchmarkTimer.Measure("bootstrap;bundlesindex"))
			{
				newBackend.BuildFileIndex();
			}
			while (true)
			{
				if (FileSystem.Backend.isError)
				{
					ThrowError(FileSystem.Backend.loadingError);
					yield break;
				}
				float assetSceneProgress = newBackend.GetAssetSceneProgress("AssetScene-bootstrap");
				if (assetSceneProgress >= 1f)
				{
					break;
				}
				yield return StartCoroutine(LoadingUpdate($"Loading Menu Prefabs {assetSceneProgress * 100f:0.0}%"));
			}
		}
		if (FileSystem.Backend.isError)
		{
			ThrowError(FileSystem.Backend.loadingError);
			yield break;
		}
		if (!UnityEngine.Application.isEditor)
		{
			WriteToLog(SystemInfoGeneralText.currentInfo);
		}
		UnityEngine.Texture.SetGlobalAnisotropicFilteringLimits(1, 16);
		if (isErrored)
		{
			yield break;
		}
		using (BenchmarkTimer.Measure("bootstrap;gamemanifest"))
		{
			yield return StartCoroutine(LoadingUpdate("Loading Game Manifest"));
			GameManifest.Load();
			yield return StartCoroutine(LoadingUpdate("DONE!"));
		}
		using (BenchmarkTimer.Measure("bootstrap;selfcheck"))
		{
			yield return StartCoroutine(LoadingUpdate("Running Self Check"));
			SelfCheck.Run();
		}
		if (isErrored)
		{
			yield break;
		}
		yield return StartCoroutine(LoadingUpdate("Bootstrap Tier0"));
		using (BenchmarkTimer.Measure("bootstrap;tier0"))
		{
			Init_Tier0();
		}
		using (BenchmarkTimer.Measure("bootstrap;commandlinevalues"))
		{
			ConsoleSystem.UpdateValuesFromCommandLine();
		}
		yield return StartCoroutine(LoadingUpdate("Bootstrap Systems"));
		using (BenchmarkTimer.Measure("bootstrap;init_systems"))
		{
			Init_Systems();
		}
		yield return StartCoroutine(LoadingUpdate("Bootstrap Config"));
		using (BenchmarkTimer.Measure("bootstrap;init_config"))
		{
			Init_Config();
		}
		using (BenchmarkTimer.Measure("bootstrap;commandlinevalues2"))
		{
			ConsoleSystem.UpdateValuesFromCommandLine();
		}
		if (!isErrored)
		{
			yield return StartCoroutine(LoadingUpdate("Loading Items"));
			using (BenchmarkTimer.Measure("bootstrap;itemmanager"))
			{
				ItemManager.Initialize();
			}
			if (!isErrored)
			{
				yield return StartCoroutine(DedicatedServerStartup());
				timer?.Stop();
				GameManager.Destroy(base.gameObject);
			}
		}
	}

	private IEnumerator DedicatedServerStartup()
	{
		Rust.Application.isLoading = true;
		UnityEngine.Application.backgroundLoadingPriority = UnityEngine.ThreadPriority.High;
		WriteToLog("Skinnable Warmup");
		yield return CoroutineEx.waitForEndOfFrame;
		yield return CoroutineEx.waitForEndOfFrame;
		GameManifest.LoadAssets();
		WriteToLog("Initializing Nexus");
		yield return StartCoroutine(StartNexusServer());
		WriteToLog("Loading Scene");
		yield return CoroutineEx.waitForEndOfFrame;
		yield return CoroutineEx.waitForEndOfFrame;
		UnityEngine.Physics.defaultSolverIterations = 3;
		int value = PlayerPrefs.GetInt("UnityGraphicsQuality");
		QualitySettings.SetQualityLevel(0);
		PlayerPrefs.SetInt("UnityGraphicsQuality", value);
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		UnityEngine.Object.DontDestroyOnLoad(GameManager.server.CreatePrefab("assets/bundled/prefabs/system/server_console.prefab"));
		StartupShared();
		World.InitSize(ConVar.Server.worldsize);
		World.InitSeed(ConVar.Server.seed);
		World.InitSalt(ConVar.Server.salt);
		World.Url = ConVar.Server.levelurl;
		World.Transfer = ConVar.Server.leveltransfer;
		yield return LevelManager.LoadLevelAsync(ConVar.Server.level);
		yield return CoroutineEx.waitForEndOfFrame;
		yield return CoroutineEx.waitForEndOfFrame;
		yield return StartCoroutine(FileSystem_Warmup.Run(WriteToLog, "Asset Warmup ({0}/{1})"));
		yield return StartCoroutine(StartServer(!Facepunch.CommandLine.HasSwitch("-skipload"), "", allowOutOfDateSaves: false));
		if (!UnityEngine.Object.FindObjectOfType<Performance>())
		{
			UnityEngine.Object.DontDestroyOnLoad(GameManager.server.CreatePrefab("assets/bundled/prefabs/system/performance.prefab"));
		}
		Rust.GC.Collect();
		Rust.Application.isLoading = false;
	}

	private static void EnsureRootFolderCreated()
	{
		try
		{
			Directory.CreateDirectory(ConVar.Server.rootFolder);
		}
		catch (Exception arg)
		{
			UnityEngine.Debug.LogWarning($"Failed to automatically create the save directory: {ConVar.Server.rootFolder}\n\n{arg}");
		}
	}

	public static IEnumerator StartNexusServer()
	{
		EnsureRootFolderCreated();
		yield return NexusServer.Initialize();
		if (NexusServer.FailedToStart)
		{
			UnityEngine.Debug.LogError("Nexus server failed to start, terminating");
			Rust.Application.Quit();
		}
	}

	public static IEnumerator StartServer(bool doLoad, string saveFileOverride, bool allowOutOfDateSaves)
	{
		float timeScale = UnityEngine.Time.timeScale;
		if (ConVar.Time.pausewhileloading)
		{
			UnityEngine.Time.timeScale = 0f;
		}
		RCon.Initialize();
		BaseEntity.Query.Server = new BaseEntity.Query.EntityTree(8096f);
		EnsureRootFolderCreated();
		if ((bool)SingletonComponent<WorldSetup>.Instance)
		{
			yield return SingletonComponent<WorldSetup>.Instance.StartCoroutine(SingletonComponent<WorldSetup>.Instance.InitCoroutine());
		}
		if ((bool)SingletonComponent<DynamicNavMesh>.Instance && SingletonComponent<DynamicNavMesh>.Instance.enabled && !AiManager.nav_disable)
		{
			yield return SingletonComponent<DynamicNavMesh>.Instance.StartCoroutine(SingletonComponent<DynamicNavMesh>.Instance.UpdateNavMeshAndWait());
		}
		if ((bool)SingletonComponent<AiManager>.Instance && SingletonComponent<AiManager>.Instance.enabled)
		{
			SingletonComponent<AiManager>.Instance.Initialize();
			if (!AiManager.nav_disable && AI.npc_enable && TerrainMeta.Path != null)
			{
				foreach (MonumentInfo monument in TerrainMeta.Path.Monuments)
				{
					if (monument.HasNavmesh)
					{
						yield return monument.StartCoroutine(monument.GetMonumentNavMesh().UpdateNavMeshAndWait());
					}
				}
				if ((bool)TerrainMeta.Path && World.SpawnedPrefabs.TryGetValue("Dungeon", out var value))
				{
					DungeonNavmesh dungeonNavmesh = new GameObject("DungeonGridNavMesh").AddComponent<DungeonNavmesh>();
					dungeonNavmesh.NavMeshCollectGeometry = NavMeshCollectGeometry.PhysicsColliders;
					dungeonNavmesh.LayerMask = 65537;
					yield return dungeonNavmesh.StartCoroutine(dungeonNavmesh.UpdateNavMeshAndWait(value));
				}
				else
				{
					UnityEngine.Debug.LogWarning("Failed to find DungeonGridRoot, NOT generating Dungeon navmesh");
				}
				if ((bool)TerrainMeta.Path && World.SpawnedPrefabs.TryGetValue("DungeonBase", out var value2))
				{
					DungeonNavmesh dungeonNavmesh2 = new GameObject("DungeonBaseNavMesh").AddComponent<DungeonNavmesh>();
					dungeonNavmesh2.NavmeshResolutionModifier = 0.3f;
					dungeonNavmesh2.NavMeshCollectGeometry = NavMeshCollectGeometry.PhysicsColliders;
					dungeonNavmesh2.LayerMask = 65537;
					yield return dungeonNavmesh2.StartCoroutine(dungeonNavmesh2.UpdateNavMeshAndWait(value2));
				}
				else
				{
					UnityEngine.Debug.LogWarning("Failed to find DungeonBaseRoot , NOT generating Dungeon navmesh");
				}
				GenerateDungeonBase.SetupAI();
			}
		}
		UnityEngine.Object.DontDestroyOnLoad(GameManager.server.CreatePrefab("assets/bundled/prefabs/system/shared.prefab"));
		GameObject gameObject = GameManager.server.CreatePrefab("assets/bundled/prefabs/system/server.prefab");
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		ServerMgr serverMgr = gameObject.GetComponent<ServerMgr>();
		bool saveWasLoaded = serverMgr.Initialize(doLoad, saveFileOverride, allowOutOfDateSaves);
		yield return CoroutineEx.waitForSecondsRealtime(0.1f);
		SaveRestore.InitializeEntityLinks();
		yield return CoroutineEx.waitForSecondsRealtime(0.1f);
		SaveRestore.InitializeEntitySupports();
		yield return CoroutineEx.waitForSecondsRealtime(0.1f);
		SaveRestore.InitializeEntityConditionals();
		yield return CoroutineEx.waitForSecondsRealtime(0.1f);
		SaveRestore.GetSaveCache();
		yield return CoroutineEx.waitForSecondsRealtime(0.1f);
		BaseGameMode.CreateGameMode();
		yield return CoroutineEx.waitForSecondsRealtime(0.1f);
		MissionManifest.Get();
		yield return CoroutineEx.waitForSecondsRealtime(0.1f);
		if (Clan.enabled)
		{
			ClanManager clanManager = ClanManager.ServerInstance;
			if (clanManager == null)
			{
				UnityEngine.Debug.LogError("ClanManager was not spawned!");
				Rust.Application.Quit();
				yield break;
			}
			Task initializeTask = clanManager.Initialize();
			yield return new WaitUntil(() => initializeTask.IsCompleted);
			initializeTask.Wait();
			clanManager.LoadClanInfoForSleepers();
		}
		yield return CoroutineEx.waitForSecondsRealtime(0.1f);
		if (ServerOcclusion.OcclusionEnabled)
		{
			ServerOcclusion.SetupGrid();
		}
		yield return CoroutineEx.waitForSecondsRealtime(0.1f);
		if (NexusServer.Started)
		{
			NexusServer.UploadMapImage();
			if (saveWasLoaded)
			{
				NexusServer.RestoreUnsavedState();
			}
			NexusServer.ZoneClient.StartListening();
		}
		if (ConVar.Server.autoUploadMap)
		{
			Task uploadTask = MapUploader.UploadMap();
			while (!uploadTask.IsCompleted)
			{
				yield return null;
			}
			if (!uploadTask.IsCompletedSuccessfully)
			{
				UnityEngine.Debug.LogError("Failed to upload map file:");
				UnityEngine.Debug.LogException(uploadTask.Exception);
			}
		}
		serverMgr.OpenConnection();
		CompanionServer.Server.Initialize();
		if (ConVar.Server.autoUploadMapImages && Map.ImageData != null)
		{
			MapUploader.UploadMapImage(Map.ImageData);
		}
		using (BenchmarkTimer.Measure("Boombox.LoadStations"))
		{
			BoomBox.LoadStations();
		}
		RustEmojiLibrary.FindAllServerEmoji();
		if (ConVar.Time.pausewhileloading)
		{
			UnityEngine.Time.timeScale = timeScale;
		}
		WriteToLog("Server startup complete");
		Rust.Application.isServerStarted = true;
	}

	private void StartupShared()
	{
		Interface.CallHook("InitLogging");
		ItemManager.Initialize();
	}

	public bool RetrySteam()
	{
		if (!Facepunch.CommandLine.HasSwitch("-nosteam"))
		{
			return !PlatformService.Initialize(RustPlatformHooks.Instance);
		}
		return true;
	}

	public void ThrowError(string error)
	{
		isErrored = true;
	}

	public void ClearError()
	{
		isErrored = false;
	}

	public void ThrowSteamError()
	{
		isErrored = true;
	}

	public void ExitGame()
	{
		UnityEngine.Debug.Log("Exiting due to Exit Game button on bootstrap error panel");
		Rust.Application.Quit();
	}

	public static IEnumerator LoadingUpdate(Translate.Phrase phrase)
	{
		if ((bool)SingletonComponent<Bootstrap>.Instance)
		{
			SingletonComponent<Bootstrap>.Instance.currentLoadingPhrase = phrase;
			yield return CoroutineEx.waitForEndOfFrame;
			yield return CoroutineEx.waitForEndOfFrame;
		}
	}

	public static void WriteToLog(string str)
	{
		if (!(lastWrittenValue == str))
		{
			DebugEx.Log(str);
			lastWrittenValue = str;
		}
	}

	private static void EarlyInitialize()
	{
	}
}
