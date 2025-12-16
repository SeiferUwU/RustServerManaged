using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Facepunch;
using Facepunch.Extend;
using Facepunch.Nexus.Models;
using Network;
using Network.Visibility;
using ProtoBuf;
using ProtoBuf.Nexus;
using Rust;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.SceneManagement;

namespace ConVar;

[Factory("global")]
public class Global : ConsoleSystem
{
	private static int _developer;

	[ClientVar]
	[ServerVar]
	public static int maxthreads = 8;

	[ServerVar]
	[ClientVar]
	public static bool forceUnloadBundles = true;

	[ServerVar]
	public static bool updateNetworkPositionWithDebugCameraWhileSpectating = false;

	public static readonly string TopOfBaseFlag = "--topofbase";

	public static readonly string UndergroundFlag = "--underground";

	[ClientVar(Saved = true)]
	[ServerVar(Saved = true)]
	public static int perf = 0;

	private static bool _god = false;

	private static bool _godforceoffoverlay = false;

	[ClientVar]
	[ServerVar(ClientAdmin = true, ServerAdmin = true, Help = "When enabled a player wearing a gingerbread suit will gib like the gingerbread NPC's")]
	public static bool cinematicGingerbreadCorpses = false;

	private static uint _gingerbreadMaterialID = 0u;

	[ServerVar(Saved = true, ShowInAdminUI = true, Help = "Multiplier applied to SprayDuration if a spray isn't in the sprayers auth (cannot go above 1f)")]
	public static float SprayOutOfAuthMultiplier = 0.5f;

	[ServerVar(Saved = true, ShowInAdminUI = true, Help = "Base time (in seconds) that sprays last")]
	public static float SprayDuration = 10800f;

	[ServerVar(Saved = true, ShowInAdminUI = true, Help = "If a player sprays more than this, the oldest spray will be destroyed. 0 will disable")]
	public static int MaxSpraysPerPlayer = 40;

	[ServerVar(Help = "Disables the backpacks that appear after a corpse times out")]
	public static bool disableBagDropping = false;

	[ServerVar(Help = "Forces old monument event notification behaviour (UI popup, no sounds)")]
	public static bool legacymonumentnotifications = false;

	[ClientVar(Saved = true, Help = "Disables any emoji animations")]
	public static bool blockEmojiAnimations = false;

	[ClientVar(Saved = true, Help = "Blocks any emoji from appearing")]
	public static bool blockEmoji = false;

	[ClientVar(Saved = true, Help = "Blocks emoji provided by servers from appearing")]
	public static bool blockServerEmoji = false;

	[ClientVar(Saved = true, Help = "Displays any emoji rendering errors in the console")]
	public static bool showEmojiErrors = false;

	[ServerVar]
	[ClientVar]
	public static int developer
	{
		get
		{
			return _developer;
		}
		set
		{
			_developer = value;
			Array.Fill(RustLog.Levels, _developer);
		}
	}

	[ServerVar]
	[ClientVar]
	public static int job_system_threads
	{
		get
		{
			return JobsUtility.JobWorkerCount;
		}
		set
		{
			if (value < 1)
			{
				JobsUtility.ResetJobWorkerCount();
				return;
			}
			value = Mathf.Clamp(value, 1, JobsUtility.JobWorkerMaximumCount);
			JobsUtility.JobWorkerCount = value;
		}
	}

	[ClientVar(ClientInfo = true, Saved = true, Help = "If you're an admin this will enable god mode")]
	public static bool god
	{
		get
		{
			return _god;
		}
		set
		{
			_god = value;
		}
	}

	[ClientVar(ClientInfo = true, Saved = true, Help = "Media: Forces the global.god overlay to never show if enabled")]
	public static bool godforceoffoverlay
	{
		get
		{
			return _godforceoffoverlay;
		}
		set
		{
			_godforceoffoverlay = value;
		}
	}

	[ServerVar]
	public static void restart(Arg args)
	{
		ServerMgr.RestartServer(args.GetString(1, string.Empty), args.GetInt(0, 300));
	}

	[ClientVar]
	[ServerVar]
	public static void quit(Arg args)
	{
		if (args != null && args.HasArgs())
		{
			args.ReplyWith("Invalid quit command, quit only works if provided with no arguments.");
			return;
		}
		if (UnityEngine.Application.isEditor)
		{
			UnityEngine.Debug.LogWarning("Aborting quit because we're in the editor");
			return;
		}
		if (SingletonComponent<ServerMgr>.Instance != null)
		{
			SingletonComponent<ServerMgr>.Instance.Shutdown();
		}
		Rust.Application.isQuitting = true;
		Network.Net.sv?.Stop("quit");
		Process.GetCurrentProcess().Kill();
		UnityEngine.Debug.Log("Quitting");
		Rust.Application.Quit();
	}

	[ServerVar]
	public static void report(Arg args)
	{
		ServerPerformance.DoReport();
	}

	[ClientVar]
	[ServerVar]
	public static void objects(Arg args)
	{
		UnityEngine.Object[] array = UnityEngine.Object.FindObjectsOfType<UnityEngine.Object>();
		string text = "";
		Dictionary<Type, int> dictionary = new Dictionary<Type, int>();
		Dictionary<Type, long> dictionary2 = new Dictionary<Type, long>();
		UnityEngine.Object[] array2 = array;
		foreach (UnityEngine.Object obj in array2)
		{
			int runtimeMemorySize = Profiler.GetRuntimeMemorySize(obj);
			if (dictionary.ContainsKey(obj.GetType()))
			{
				dictionary[obj.GetType()]++;
			}
			else
			{
				dictionary.Add(obj.GetType(), 1);
			}
			if (dictionary2.ContainsKey(obj.GetType()))
			{
				dictionary2[obj.GetType()] += runtimeMemorySize;
			}
			else
			{
				dictionary2.Add(obj.GetType(), runtimeMemorySize);
			}
		}
		foreach (KeyValuePair<Type, long> item in dictionary2.OrderByDescending(delegate(KeyValuePair<Type, long> x)
		{
			KeyValuePair<Type, long> keyValuePair = x;
			return keyValuePair.Value;
		}))
		{
			text = text + dictionary[item.Key].ToString().PadLeft(10) + " " + item.Value.FormatBytes().PadLeft(15) + "\t" + item.Key?.ToString() + "\n";
		}
		args.ReplyWith(text);
	}

	[ClientVar]
	[ServerVar]
	public static void textures(Arg args)
	{
		UnityEngine.Texture[] array = UnityEngine.Object.FindObjectsOfType<UnityEngine.Texture>();
		string text = "";
		UnityEngine.Texture[] array2 = array;
		foreach (UnityEngine.Texture texture in array2)
		{
			string text2 = Profiler.GetRuntimeMemorySize(texture).FormatBytes();
			text = text + texture.ToString().PadRight(30) + texture.name.PadRight(30) + text2 + "\n";
		}
		args.ReplyWith(text);
	}

	[ClientVar]
	[ServerVar]
	public static void colliders(Arg args)
	{
		int num = (from x in UnityEngine.Object.FindObjectsOfType<Collider>()
			where x.enabled
			select x).Count();
		int num2 = (from x in UnityEngine.Object.FindObjectsOfType<Collider>()
			where !x.enabled
			select x).Count();
		string strValue = num + " colliders enabled, " + num2 + " disabled";
		args.ReplyWith(strValue);
	}

	[ClientVar]
	[ServerVar]
	public static void error(Arg args)
	{
		((GameObject)null).transform.position = Vector3.zero;
	}

	[ServerVar]
	[ClientVar]
	public static void queue(Arg args)
	{
		string text = "";
		text = text + "stabilityCheckQueue:        " + StabilityEntity.stabilityCheckQueue.Info() + "\n";
		text = text + "updateSurroundingsQueue:    " + StabilityEntity.updateSurroundingsQueue.Info() + "\n";
		args.ReplyWith(text);
	}

	[ServerUserVar]
	public static void setinfo(Arg args)
	{
		BasePlayer basePlayer = ArgEx.Player(args);
		if ((bool)basePlayer)
		{
			string text = args.GetString(0, null);
			string text2 = args.GetString(1, null);
			if (text != null && text2 != null)
			{
				basePlayer.SetInfo(text, text2);
			}
		}
	}

	[ServerVar]
	public static void sleep(Arg args)
	{
		BasePlayer basePlayer = ArgEx.Player(args);
		if ((bool)basePlayer && !basePlayer.IsSleeping() && !basePlayer.IsSpectating() && !basePlayer.IsDead())
		{
			basePlayer.StartSleeping();
		}
	}

	[ServerVar]
	public static void sleeptarget(Arg args)
	{
		BasePlayer basePlayer = ArgEx.Player(args);
		if ((bool)basePlayer)
		{
			BasePlayer lookingAtPlayer = RelationshipManager.GetLookingAtPlayer(basePlayer);
			if (!(lookingAtPlayer == null))
			{
				lookingAtPlayer.StartSleeping();
			}
		}
	}

	[ServerUserVar]
	public static void kill(Arg args)
	{
		BasePlayer basePlayer = ArgEx.Player(args);
		if (!basePlayer || basePlayer.IsSpectating() || basePlayer.IsDead())
		{
			return;
		}
		if (basePlayer.IsRestrained)
		{
			Handcuffs handcuffs = basePlayer.Belt?.GetRestraintItem();
			if (handcuffs != null && handcuffs.BlockSuicide)
			{
				return;
			}
		}
		if (basePlayer.CanSuicide())
		{
			basePlayer.Hurt(1000f, DamageType.Suicide, basePlayer, useProtection: false);
			if (basePlayer.IsDead())
			{
				basePlayer.MarkSuicide();
			}
		}
		else
		{
			basePlayer.ConsoleMessage("You can't suicide again so quickly, wait a while");
		}
	}

	[ServerUserVar]
	public static void respawn(Arg args)
	{
		BasePlayer basePlayer = ArgEx.Player(args);
		if (!basePlayer)
		{
			return;
		}
		if (!basePlayer.IsDead() && !basePlayer.IsSpectating())
		{
			if (developer > 0)
			{
				UnityEngine.Debug.LogWarning(basePlayer?.ToString() + " wanted to respawn but isn't dead or spectating");
			}
			basePlayer.SendNetworkUpdate();
		}
		else if (basePlayer.CanRespawn())
		{
			basePlayer.MarkRespawn();
			basePlayer.Respawn();
		}
		else
		{
			basePlayer.ConsoleMessage("You can't respawn again so quickly, wait a while");
		}
	}

	[ServerVar]
	public static void injure(Arg args)
	{
		InjurePlayer(ArgEx.Player(args));
	}

	public static void InjurePlayer(BasePlayer ply)
	{
		if (ply == null || ply.IsDead())
		{
			return;
		}
		HitInfo hitInfo = Facepunch.Pool.Get<HitInfo>();
		hitInfo.Init(ply, ply, DamageType.Suicide, 1000f, ply.transform.position);
		hitInfo.UseProtection = false;
		if (Server.woundingenabled && !ply.IsIncapacitated() && !ply.IsSleeping() && !ply.isMounted)
		{
			if (ply.IsCrawling())
			{
				ply.GoToIncapacitated(hitInfo);
			}
			else
			{
				ply.BecomeWounded(hitInfo);
			}
		}
		else
		{
			ply.ConsoleMessage("Can't go to wounded state right now.");
		}
	}

	[ServerVar]
	public static void recover(Arg args)
	{
		RecoverPlayer(ArgEx.Player(args));
	}

	public static void RecoverPlayer(BasePlayer ply)
	{
		if (!(ply == null) && !ply.IsDead())
		{
			ply.StopWounded();
		}
	}

	[ServerVar]
	public static void spectate(Arg args)
	{
		BasePlayer basePlayer = ArgEx.Player(args);
		if ((bool)basePlayer)
		{
			basePlayer.wantsSpectate = true;
			if (!basePlayer.IsDead())
			{
				basePlayer.DieInstantly();
			}
			string strName = args.GetString(0);
			if (basePlayer.IsDead())
			{
				basePlayer.StartSpectating();
				basePlayer.UpdateSpectateTarget(strName);
			}
			basePlayer.wantsSpectate = false;
		}
	}

	[ServerVar]
	public static void toggleSpectateTeamInfo(Arg args)
	{
		bool flag = args.GetBool(0);
		BasePlayer basePlayer = ArgEx.Player(args);
		if (basePlayer != null)
		{
			basePlayer.SetSpectateTeamInfo(flag);
			args.ReplyWith($"ToggleSpectateTeamInfo is now {flag}");
		}
		else
		{
			args.ReplyWith("Invalid player or player is not spectating");
		}
	}

	[ServerVar]
	public static void spectateid(Arg args)
	{
		BasePlayer basePlayer = ArgEx.Player(args);
		if ((bool)basePlayer)
		{
			basePlayer.wantsSpectate = true;
			if (!basePlayer.IsDead())
			{
				basePlayer.DieInstantly();
			}
			ulong uLong = args.GetULong(0, 0uL);
			if (basePlayer.IsDead())
			{
				basePlayer.StartSpectating();
				basePlayer.UpdateSpectateTarget(uLong);
			}
			basePlayer.wantsSpectate = false;
		}
	}

	[ServerUserVar]
	public static void respawn_sleepingbag(Arg args)
	{
		BasePlayer basePlayer = ArgEx.Player(args);
		if (!basePlayer || !basePlayer.IsDead())
		{
			return;
		}
		NetworkableId entityID = ArgEx.GetEntityID(args, 0);
		if (!entityID.IsValid)
		{
			args.ReplyWith("Missing sleeping bag ID");
			return;
		}
		string text = args.GetString(1);
		string errorMessage;
		if (NexusServer.Started && !string.IsNullOrWhiteSpace(text))
		{
			if (!ZoneController.Instance.CanRespawnAcrossZones(basePlayer))
			{
				args.ReplyWith("You cannot respawn to a different zone");
				return;
			}
			NexusZoneDetails nexusZoneDetails = NexusServer.FindZone(text);
			if (nexusZoneDetails == null)
			{
				args.ReplyWith("Zone was not found");
			}
			else if (!basePlayer.CanRespawn())
			{
				args.ReplyWith("You can't respawn again so quickly, wait a while");
			}
			else
			{
				NexusRespawn(basePlayer, nexusZoneDetails, entityID);
			}
		}
		else if (!SleepingBag.TrySpawnPlayer(basePlayer, entityID, out errorMessage))
		{
			args.ReplyWith(errorMessage);
		}
		static async void NexusRespawn(BasePlayer player, NexusZoneDetails toZone, NetworkableId sleepingBag)
		{
			_ = 1;
			try
			{
				player.nextRespawnTime = float.PositiveInfinity;
				Request request = Facepunch.Pool.Get<Request>();
				request.respawnAtBag = Facepunch.Pool.Get<SleepingBagRespawnRequest>();
				request.respawnAtBag.userId = player.userID;
				request.respawnAtBag.sleepingBagId = sleepingBag;
				request.respawnAtBag.secondaryData = player.SaveSecondaryData();
				using (Response response = await NexusServer.ZoneRpc(toZone.Key, request))
				{
					if (!response.status.success)
					{
						if (player.IsConnected)
						{
							player.ConsoleMessage("RespawnAtBag failed: " + response.status.errorMessage);
						}
						return;
					}
				}
				await NexusServer.ZoneClient.Assign(player.UserIDString, toZone.Key);
				if (player.IsConnected)
				{
					ConsoleNetwork.SendClientCommandImmediate(player.net.connection, "nexus.redirect", toZone.IpAddress, toZone.GamePort, NexusUtil.ConnectionProtocol(toZone));
					player.Kick("Redirecting to another zone...");
				}
			}
			catch (Exception ex)
			{
				if (player.IsConnected)
				{
					player.ConsoleMessage(ex.ToString());
				}
			}
			finally
			{
				player.MarkRespawn();
			}
		}
	}

	[ServerUserVar]
	public static void respawn_sleepingbag_remove(Arg args)
	{
		BasePlayer basePlayer = ArgEx.Player(args);
		if (!basePlayer)
		{
			return;
		}
		NetworkableId entityID = ArgEx.GetEntityID(args, 0);
		if (!entityID.IsValid)
		{
			args.ReplyWith("Missing sleeping bag ID");
			return;
		}
		string text = args.GetString(1);
		if (NexusServer.Started && !string.IsNullOrWhiteSpace(text))
		{
			NexusZoneDetails nexusZoneDetails = NexusServer.FindZone(text);
			if (nexusZoneDetails == null)
			{
				args.ReplyWith("Zone was not found");
			}
			else if (ZoneController.Instance.CanRespawnAcrossZones(basePlayer))
			{
				NexusRemoveBag(basePlayer, nexusZoneDetails.Key, entityID);
			}
		}
		else
		{
			SleepingBag.DestroyBag(basePlayer.userID, entityID);
		}
		static async void NexusRemoveBag(BasePlayer player, string zoneKey, NetworkableId sleepingBag)
		{
			try
			{
				Request request = Facepunch.Pool.Get<Request>();
				request.destroyBag = Facepunch.Pool.Get<SleepingBagDestroyRequest>();
				request.destroyBag.userId = player.userID;
				request.destroyBag.sleepingBagId = sleepingBag;
				(await NexusServer.ZoneRpc(zoneKey, request)).Dispose();
			}
			catch (Exception ex)
			{
				if (player.IsConnected)
				{
					player.ConsoleMessage(ex.ToString());
				}
			}
		}
	}

	[ServerUserVar]
	public static void status_sv(Arg args)
	{
		BasePlayer basePlayer = ArgEx.Player(args);
		if ((bool)basePlayer)
		{
			args.ReplyWith(basePlayer.GetDebugStatus());
		}
	}

	[ClientVar]
	public static void status_cl(Arg args)
	{
	}

	[ServerVar]
	public static void teleport(Arg args)
	{
		if (args.HasArgs(2))
		{
			BasePlayer playerOrSleeperOrBot = ArgEx.GetPlayerOrSleeperOrBot(args, 0);
			if ((bool)playerOrSleeperOrBot && playerOrSleeperOrBot.IsAlive())
			{
				BasePlayer playerOrSleeperOrBot2 = ArgEx.GetPlayerOrSleeperOrBot(args, 1);
				if ((bool)playerOrSleeperOrBot2 && playerOrSleeperOrBot2.IsAlive())
				{
					playerOrSleeperOrBot.Teleport(playerOrSleeperOrBot2);
				}
			}
			return;
		}
		BasePlayer basePlayer = ArgEx.Player(args);
		if ((bool)basePlayer && basePlayer.IsAlive())
		{
			BasePlayer playerOrSleeperOrBot3 = ArgEx.GetPlayerOrSleeperOrBot(args, 0);
			if ((bool)playerOrSleeperOrBot3 && playerOrSleeperOrBot3.IsAlive())
			{
				basePlayer.Teleport(playerOrSleeperOrBot3);
			}
		}
	}

	[ServerVar]
	public static void teleport2me(Arg args)
	{
		BasePlayer playerOrSleeperOrBot = ArgEx.GetPlayerOrSleeperOrBot(args, 0);
		if (playerOrSleeperOrBot == null)
		{
			args.ReplyWith("Player or bot not found");
			return;
		}
		if (!playerOrSleeperOrBot.IsAlive())
		{
			args.ReplyWith("Target is not alive");
			return;
		}
		BasePlayer basePlayer = ArgEx.Player(args);
		if ((bool)basePlayer && basePlayer.IsAlive())
		{
			playerOrSleeperOrBot.Teleport(basePlayer);
		}
	}

	[ServerVar]
	public static void teleporteveryone2me(Arg args)
	{
		BasePlayer basePlayer = ArgEx.Player(args);
		if ((bool)basePlayer)
		{
			TeleportPlayersToMe(basePlayer, includeSleepers: true, includeNonSleepers: true, 0uL);
		}
	}

	[ServerVar]
	public static void teleportsleepers2me(Arg args)
	{
		BasePlayer basePlayer = ArgEx.Player(args);
		if ((bool)basePlayer)
		{
			TeleportPlayersToMe(basePlayer, includeSleepers: true, includeNonSleepers: false, 0uL);
		}
	}

	[ServerVar]
	public static void teleportnonsleepers2me(Arg args)
	{
		BasePlayer basePlayer = ArgEx.Player(args);
		if ((bool)basePlayer)
		{
			TeleportPlayersToMe(basePlayer, includeSleepers: false, includeNonSleepers: true, 0uL);
		}
	}

	[ServerVar]
	public static void teleportteam2me(Arg args)
	{
		BasePlayer basePlayer = ArgEx.Player(args);
		if ((bool)basePlayer)
		{
			if (basePlayer.Team == null)
			{
				args.ReplyWith("Player is not in a team");
			}
			else
			{
				TeleportPlayersToMe(basePlayer, includeSleepers: true, includeNonSleepers: true, basePlayer.Team.teamID);
			}
		}
	}

	[ServerVar]
	public static void teleporttargetteam2me(Arg args)
	{
		BasePlayer basePlayer = ArgEx.Player(args);
		if ((bool)basePlayer)
		{
			if (basePlayer.Team == null)
			{
				args.ReplyWith("Player is not in a team");
				return;
			}
			ulong uLong = args.GetULong(0, 0uL);
			TeleportPlayersToMe(basePlayer, includeSleepers: true, includeNonSleepers: true, uLong);
		}
	}

	private static void TeleportPlayersToMe(BasePlayer player, bool includeSleepers, bool includeNonSleepers, ulong filterByTeam = 0uL)
	{
		if (player == null || !player || !player.IsAlive())
		{
			return;
		}
		foreach (BasePlayer allPlayer in BasePlayer.allPlayerList)
		{
			if (allPlayer.IsAlive() && !(allPlayer == player) && (!allPlayer.IsSleeping() || includeSleepers) && (allPlayer.IsSleeping() || includeNonSleepers) && (filterByTeam == 0L || (allPlayer.Team != null && allPlayer.Team.teamID == filterByTeam)))
			{
				allPlayer.Teleport(player);
			}
		}
	}

	[ServerVar]
	public static void teleportany(Arg args)
	{
		BasePlayer basePlayer = ArgEx.Player(args);
		if ((bool)basePlayer && basePlayer.IsAlive())
		{
			basePlayer.Teleport(args.GetString(0), playersOnly: false);
		}
	}

	[ServerVar]
	public static void teleportpos(Arg args)
	{
		BasePlayer basePlayer = ArgEx.Player(args);
		if ((bool)basePlayer && basePlayer.IsAlive())
		{
			bool num = args.HasArg(TopOfBaseFlag);
			bool flag = args.HasArg(UndergroundFlag);
			string str = args.FullString.Replace(", ", ",").Replace(TopOfBaseFlag, "").Replace(UndergroundFlag, "")
				.Trim('"');
			if (num)
			{
				TeleportToTopOfBase(basePlayer, str.ToVector3());
			}
			else if (flag)
			{
				TeleportToUnderground(basePlayer, str.ToVector3());
			}
			else
			{
				basePlayer.Teleport(str.ToVector3());
			}
		}
	}

	[ServerVar]
	public static void teleportlos(Arg args)
	{
		BasePlayer basePlayer = ArgEx.Player(args);
		if ((bool)basePlayer && basePlayer.IsAlive())
		{
			Ray ray = basePlayer.eyes.HeadRay();
			int num = args.GetInt(0, 1000);
			if (UnityEngine.Physics.Raycast(ray, out var hitInfo, num, 1218652417))
			{
				basePlayer.Teleport(hitInfo.point);
			}
			else
			{
				basePlayer.Teleport(ray.origin + ray.direction * num);
			}
		}
	}

	[ServerVar]
	public static void teleport2owneditem(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		BasePlayer playerOrSleeper = ArgEx.GetPlayerOrSleeper(arg, 0);
		ulong result;
		if (playerOrSleeper != null)
		{
			result = playerOrSleeper.userID;
		}
		else if (!ulong.TryParse(arg.GetString(0), out result))
		{
			arg.ReplyWith("No player with that id found");
			return;
		}
		string strFilter = arg.GetString(1);
		BaseEntity[] array = BaseEntity.Util.FindTargetsOwnedBy(result, strFilter);
		if (array.Length == 0)
		{
			arg.ReplyWith("No targets found");
			return;
		}
		int num = UnityEngine.Random.Range(0, array.Length);
		arg.ReplyWith($"Teleporting to {array[num].ShortPrefabName} at {array[num].transform.position}");
		basePlayer.Teleport(array[num].transform.position);
	}

	[ServerVar]
	public static void teleport2autheditem(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		BasePlayer playerOrSleeper = ArgEx.GetPlayerOrSleeper(arg, 0);
		ulong result;
		if (playerOrSleeper != null)
		{
			result = playerOrSleeper.userID;
		}
		else if (!ulong.TryParse(arg.GetString(0), out result))
		{
			arg.ReplyWith("No player with that id found");
			return;
		}
		string strFilter = arg.GetString(1);
		BaseEntity[] array = BaseEntity.Util.FindTargetsAuthedTo(result, strFilter);
		if (array.Length == 0)
		{
			arg.ReplyWith("No targets found");
			return;
		}
		int num = UnityEngine.Random.Range(0, array.Length);
		arg.ReplyWith($"Teleporting to {array[num].ShortPrefabName} at {array[num].transform.position}");
		basePlayer.Teleport(array[num].transform.position);
	}

	[ServerVar]
	public static void teleport2marker(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (basePlayer == null)
		{
			arg.ReplyWith("Must be called from a player");
			return;
		}
		if (basePlayer.State.pointsOfInterest == null || basePlayer.State.pointsOfInterest.Count == 0)
		{
			arg.ReplyWith("You don't have a marker set");
			return;
		}
		string text = arg.GetString(0);
		if (arg.HasArgs() && text != "True")
		{
			int num = arg.GetInt(0);
			if (num == -1)
			{
				num = basePlayer.State.pointsOfInterest.Count - 1;
			}
			if (num >= 0 && num < basePlayer.State.pointsOfInterest.Count)
			{
				TeleportToMarker(basePlayer.State.pointsOfInterest[num], basePlayer);
				return;
			}
		}
		if (!string.IsNullOrEmpty(text))
		{
			foreach (MapNote item in basePlayer.State.pointsOfInterest)
			{
				if (!string.IsNullOrEmpty(item.label) && string.Equals(item.label, text, StringComparison.InvariantCultureIgnoreCase))
				{
					TeleportToMarker(item, basePlayer);
					return;
				}
			}
		}
		int debugMapMarkerIndex = basePlayer.DebugMapMarkerIndex;
		debugMapMarkerIndex++;
		if (debugMapMarkerIndex >= basePlayer.State.pointsOfInterest.Count)
		{
			debugMapMarkerIndex = 0;
		}
		TeleportToMarker(basePlayer.State.pointsOfInterest[debugMapMarkerIndex], basePlayer);
		basePlayer.DebugMapMarkerIndex = debugMapMarkerIndex;
	}

	private static void TeleportToMarker(MapNote marker, BasePlayer player)
	{
		TeleportToTopOfBase(player, marker.worldPosition);
	}

	private static void TeleportToTopOfBase(BasePlayer player, Vector3 position)
	{
		position.y = WaterLevel.GetWaterOrTerrainSurface(position, waves: true, volumes: true);
		if (UnityEngine.Physics.Raycast(new Ray(position + Vector3.up * 100f, Vector3.down), out var hitInfo, 110f, 1218652417))
		{
			position.y = hitInfo.point.y + 0.5f;
		}
		player.Teleport(position);
	}

	private static void TeleportToUnderground(BasePlayer player, Vector3 position)
	{
		bool flag = false;
		position.y = WaterLevel.GetWaterOrTerrainSurface(position, waves: true, volumes: true) - 10f;
		BufferList<RaycastHit> obj = Facepunch.Pool.Get<BufferList<RaycastHit>>();
		obj.Resize(10);
		int num = UnityEngine.Physics.RaycastNonAlloc(new Ray(position, Vector3.down), obj.Buffer, 200f, 1210263809);
		float num2 = 0f;
		for (int i = 0; i < num; i++)
		{
			float y = obj[i].transform.position.y;
			if (y < num2)
			{
				position.y = y + 2f;
				if (!global::AntiHack.TestInsideTerrain(position))
				{
					flag = true;
					num2 = y;
				}
			}
		}
		if (flag)
		{
			position.y = num2 + 2f;
			player.Teleport(position);
		}
		else
		{
			TeleportToTopOfBase(player, position);
		}
		Facepunch.Pool.FreeUnmanaged(ref obj);
	}

	[ServerVar]
	public static void teleport2grid(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (!(basePlayer == null))
		{
			Vector3? vector = MapHelper.StringToPosition(arg.GetString(0));
			if (!vector.HasValue)
			{
				arg.ReplyWith("Invalid grid reference, should look like 'A1'");
			}
			else
			{
				TeleportToTopOfBase(basePlayer, vector.Value);
			}
		}
	}

	[ServerVar]
	public static void teleport2death(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (basePlayer == null)
		{
			arg.ReplyWith("Must be called from a player");
			return;
		}
		if (basePlayer.State.deathMarker == null)
		{
			arg.ReplyWith("No death marker found");
			return;
		}
		Vector3 worldPosition = basePlayer.ServerCurrentDeathNote.worldPosition;
		basePlayer.Teleport(worldPosition);
	}

	[ServerVar]
	public static void teleport2mission(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (!(basePlayer == null))
		{
			BaseMission.MissionInstance activeMissionInstance = basePlayer.GetActiveMissionInstance();
			if (activeMissionInstance != null)
			{
				TeleportToTopOfBase(basePlayer, activeMissionInstance.missionLocation);
			}
		}
	}

	[ServerVar]
	[ClientVar]
	public static void free(Arg args)
	{
		Pool.clear_prefabs(args);
		Pool.clear_assets(args);
		Pool.clear_memory(args);
		GC.collect();
		GC.unload();
	}

	[ServerVar(ServerUser = true)]
	[ClientVar]
	public static void version(Arg arg)
	{
		arg.ReplyWith($"Protocol: {Protocol.printable}\nBuild Date: {BuildInfo.Current.BuildDate}\nUnity Version: {UnityEngine.Application.unityVersion}\nChangeset: {BuildInfo.Current.Scm.ChangeId}\nBranch: {BuildInfo.Current.Scm.Branch}");
	}

	[ServerVar]
	[ClientVar]
	public static void sysinfo(Arg arg)
	{
		arg.ReplyWith(SystemInfoGeneralText.currentInfo);
	}

	[ServerVar]
	[ClientVar]
	public static void sysuid(Arg arg)
	{
		arg.ReplyWith(SystemInfo.deviceUniqueIdentifier);
	}

	[ServerVar]
	public static void breakitem(Arg args)
	{
		BasePlayer basePlayer = ArgEx.Player(args);
		if ((bool)basePlayer)
		{
			Item activeItem = basePlayer.GetActiveItem();
			activeItem?.LoseCondition(activeItem.condition);
		}
	}

	[ServerVar]
	public static void breakclothing(Arg args)
	{
		BasePlayer basePlayer = ArgEx.Player(args);
		if (!basePlayer)
		{
			return;
		}
		foreach (Item item in basePlayer.inventory.containerWear.itemList)
		{
			item?.LoseCondition(item.condition);
		}
	}

	[ClientVar]
	[ServerVar]
	public static void subscriptions(Arg arg)
	{
		bool flag = arg.HasArg("--json");
		using TextTable textTable = Facepunch.Pool.Get<TextTable>();
		textTable.ShouldPadColumns = !flag;
		textTable.AddColumn("realm");
		textTable.AddColumn("group");
		BasePlayer basePlayer = ArgEx.Player(arg);
		if ((bool)basePlayer)
		{
			foreach (Group item in basePlayer.net.subscriber.subscribed)
			{
				textTable.AddRow("sv", item.ID.ToString());
			}
		}
		arg.ReplyWith(flag ? textTable.ToJson() : textTable.ToString());
	}

	public static uint GingerbreadMaterialID()
	{
		if (_gingerbreadMaterialID == 0)
		{
			_gingerbreadMaterialID = StringPool.Get("Gingerbread");
		}
		return _gingerbreadMaterialID;
	}

	[ServerVar]
	public static void ClearAllSprays()
	{
		List<SprayCanSpray> obj = Facepunch.Pool.Get<List<SprayCanSpray>>();
		foreach (SprayCanSpray allSpray in SprayCanSpray.AllSprays)
		{
			obj.Add(allSpray);
		}
		foreach (SprayCanSpray item in obj)
		{
			item.Kill();
		}
		Facepunch.Pool.FreeUnmanaged(ref obj);
	}

	[ServerVar]
	public static void ClearAllSpraysByPlayer(Arg arg)
	{
		if (!arg.HasArgs())
		{
			return;
		}
		ulong uLong = arg.GetULong(0, 0uL);
		List<SprayCanSpray> obj = Facepunch.Pool.Get<List<SprayCanSpray>>();
		foreach (SprayCanSpray allSpray in SprayCanSpray.AllSprays)
		{
			if (allSpray.sprayedByPlayer == uLong)
			{
				obj.Add(allSpray);
			}
		}
		foreach (SprayCanSpray item in obj)
		{
			item.Kill();
		}
		int count = obj.Count;
		Facepunch.Pool.FreeUnmanaged(ref obj);
		arg.ReplyWith($"Deleted {count} sprays by {uLong}");
	}

	[ServerVar]
	public static void ClearSpraysInRadius(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (!(basePlayer == null))
		{
			float num = arg.GetFloat(0, 16f);
			int num2 = ClearSpraysInRadius(basePlayer.transform.position, num);
			arg.ReplyWith($"Deleted {num2} sprays within {num} of {basePlayer.displayName}");
		}
	}

	private static int ClearSpraysInRadius(Vector3 position, float radius)
	{
		List<SprayCanSpray> obj = Facepunch.Pool.Get<List<SprayCanSpray>>();
		foreach (SprayCanSpray allSpray in SprayCanSpray.AllSprays)
		{
			if (allSpray.Distance(position) <= radius)
			{
				obj.Add(allSpray);
			}
		}
		foreach (SprayCanSpray item in obj)
		{
			item.Kill();
		}
		int count = obj.Count;
		Facepunch.Pool.FreeUnmanaged(ref obj);
		return count;
	}

	[ServerVar]
	public static void ClearSpraysAtPositionInRadius(Arg arg)
	{
		Vector3 vector = arg.GetVector3(0);
		float num = arg.GetFloat(1);
		if (num != 0f)
		{
			int num2 = ClearSpraysInRadius(vector, num);
			arg.ReplyWith($"Deleted {num2} sprays within {num} of {vector}");
		}
	}

	[ServerVar]
	public static void ClearDroppedItems()
	{
		List<DroppedItem> obj = Facepunch.Pool.Get<List<DroppedItem>>();
		foreach (BaseNetworkable serverEntity in BaseNetworkable.serverEntities)
		{
			if (serverEntity is DroppedItem item)
			{
				obj.Add(item);
			}
		}
		foreach (DroppedItem item2 in obj)
		{
			item2.Kill();
		}
		Facepunch.Pool.FreeUnmanaged(ref obj);
	}

	[ServerVar]
	[ClientVar]
	public static string printAllScenesInBuild(Arg args)
	{
		StringBuilder stringBuilder = new StringBuilder();
		int sceneCountInBuildSettings = SceneManager.sceneCountInBuildSettings;
		stringBuilder.AppendLine($"Scenes: {sceneCountInBuildSettings}");
		for (int i = 0; i < sceneCountInBuildSettings; i++)
		{
			stringBuilder.AppendLine(SceneUtility.GetScenePathByBuildIndex(i));
		}
		return stringBuilder.ToString();
	}

	[ServerVar(Clientside = true, Help = "Immediately update the manifest")]
	public static void UpdateManifest(Arg args)
	{
		Facepunch.Manifest.UpdateManifest();
	}
}
