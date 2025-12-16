using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Development.Attributes;
using Facepunch;
using Facepunch.Extend;
using Facepunch.Unity;
using Network;
using Oxide.Core;
using ProtoBuf;
using Rust;
using Rust.Ai;
using UnityEngine;

namespace ConVar;

[ResetStaticFields]
[Factory("debug")]
public class Debugging : ConsoleSystem
{
	private const string NO_RECOVER_ARG = "--no-recover";

	[ServerVar]
	[ClientVar]
	public static bool checktriggers = false;

	[ServerVar]
	public static bool checkparentingtriggers = true;

	[ServerVar]
	[ClientVar(Saved = false, Help = "Shows some debug info for dismount attempts.")]
	public static bool DebugDismounts = false;

	[ServerVar(Help = "Do not damage any items")]
	public static bool disablecondition = false;

	[ServerVar]
	public static int tutorial_start_cooldown = 60;

	[ServerVar]
	public static bool printMissionSpeakInfo = false;

	[ServerVar]
	public static float puzzleResetTimeMultiplier = 1f;

	[ClientVar]
	[ServerVar]
	public static bool callbacks = false;

	[ServerVar]
	[ClientVar]
	public static bool log
	{
		get
		{
			return UnityEngine.Debug.unityLogger.logEnabled;
		}
		set
		{
			if (!value)
			{
				UnityEngine.Debug.Log("Logging disabled");
			}
			UnityEngine.Debug.unityLogger.logEnabled = value;
			if (value)
			{
				UnityEngine.Debug.Log("Logging enabled");
			}
		}
	}

	[ServerVar]
	[ClientVar(ClientAdmin = true)]
	public static void renderinfo(Arg arg)
	{
		RenderInfo.GenerateReport();
	}

	[ServerVar]
	public static void enable_player_movement(Arg arg)
	{
		if (arg.IsAdmin)
		{
			bool flag = arg.GetBool(0, def: true);
			BasePlayer basePlayer = ArgEx.Player(arg);
			if (basePlayer == null)
			{
				arg.ReplyWith("Must be called from client with player model");
				return;
			}
			basePlayer.ClientRPC(RpcTarget.Player("TogglePlayerMovement", basePlayer), flag);
			arg.ReplyWith((flag ? "enabled" : "disabled") + " player movement");
		}
	}

	[ServerVar]
	public static void console_spam(Arg arg)
	{
		int num = Mathf.Clamp(arg.GetInt(0, 100), 1, 100000);
		int count = arg.GetInt(1, 50);
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		for (int i = 0; i < num; i++)
		{
			UnityEngine.Debug.Log(new string((char)(97 + i % 26), count));
		}
		stopwatch.Stop();
		UnityEngine.Debug.Log($"Took {stopwatch.ElapsedMilliseconds}ms to log {num} lines");
	}

	[ServerVar]
	public static void console_print_color(Arg arg)
	{
		string text = arg.GetString(0, "This is a test colored message");
		int color = arg.GetInt(1, 2);
		ServerConsole.PrintColoured(text, (ConsoleColor)color);
	}

	[ServerVar]
	[ClientVar]
	public static void stall(Arg arg)
	{
		float num = Mathf.Clamp(arg.GetFloat(0), 0f, 1f);
		arg.ReplyWith("Stalling for " + num + " seconds...");
		Thread.Sleep(Mathf.RoundToInt(num * 1000f));
	}

	[ServerVar(Help = "Repair all items in inventory")]
	public static void repair_inventory(Arg args)
	{
		BasePlayer basePlayer = ArgEx.Player(args);
		if (!basePlayer)
		{
			return;
		}
		List<Item> obj = Facepunch.Pool.Get<List<Item>>();
		basePlayer.inventory.GetAllItems(obj);
		foreach (Item item in obj)
		{
			if (item != null)
			{
				item.maxCondition = item.info.condition.max;
				item.condition = item.maxCondition;
				item.MarkDirty();
			}
			if (item.contents == null)
			{
				continue;
			}
			foreach (Item item2 in item.contents.itemList)
			{
				item2.maxCondition = item2.info.condition.max;
				item2.condition = item2.maxCondition;
				item2.MarkDirty();
			}
		}
		Facepunch.Pool.Free(ref obj, freeElements: false);
	}

	[ServerVar]
	public static void spawnParachuteTester(Arg arg)
	{
		float num = arg.GetFloat(0, 50f);
		BasePlayer basePlayer = ArgEx.Player(arg);
		BasePlayer basePlayer2 = GameManager.server.CreateEntity("assets/prefabs/player/player.prefab", basePlayer.transform.position + Vector3.up * num, Quaternion.LookRotation(basePlayer.eyes.BodyForward())) as BasePlayer;
		basePlayer2.Spawn();
		basePlayer2.eyes.rotation = basePlayer.eyes.rotation;
		basePlayer2.SendNetworkUpdate();
		Inventory.copyTo(basePlayer, basePlayer2);
		if (!basePlayer2.HasValidParachuteEquipped())
		{
			basePlayer2.inventory.containerWear.GiveItem(ItemManager.CreateByName("parachute", 1, 0uL));
		}
		basePlayer2.RequestParachuteDeploy();
	}

	[ServerVar]
	public static string testTutorialCinematic(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (basePlayer == null || !basePlayer.IsInTutorial)
		{
			return "Requires a player";
		}
		TutorialIsland currentTutorialIsland = basePlayer.GetCurrentTutorialIsland();
		if (currentTutorialIsland == null)
		{
			return "Invalid island";
		}
		Transform transform = currentTutorialIsland.transform.FindChildRecursive("KayakMissionPoint");
		if (transform == null)
		{
			return "Can't find KayakMissionPoint on island";
		}
		Kayak obj = GameManager.server.CreateEntity("assets/content/vehicles/boats/kayak/kayak.prefab", transform.position, transform.rotation) as Kayak;
		obj.Spawn();
		obj.WantsMount(basePlayer);
		currentTutorialIsland.StartEndingCinematic(basePlayer);
		return "Playing cinematic";
	}

	[ServerVar(Help = "If a player ends up stuck on a tutorial for any reason this will clear the island and reset the player (will also kill player)")]
	public static void clearTutorialForPlayer(Arg arg)
	{
		BasePlayer player = ArgEx.GetPlayer(arg, 0);
		if (player == null)
		{
			arg.ReplyWith("Please provide a player");
		}
		else if (player.IsInTutorial)
		{
			TutorialIsland currentTutorialIsland = player.GetCurrentTutorialIsland();
			if (currentTutorialIsland != null)
			{
				currentTutorialIsland.Return();
			}
			player.ClearTutorial();
			player.Hurt(99999f);
			player.ClearTutorial_PostDeath();
		}
	}

	[ServerVar]
	public static void deleteEntitiesByShortname(Arg arg)
	{
		string text = arg.GetString(0).ToLower();
		float num = arg.GetFloat(1);
		BasePlayer basePlayer = ArgEx.Player(arg);
		using PooledList<BaseNetworkable> pooledList = Facepunch.Pool.Get<PooledList<BaseNetworkable>>();
		foreach (BaseNetworkable serverEntity in BaseNetworkable.serverEntities)
		{
			if (serverEntity.ShortPrefabName == text && (num == 0f || (basePlayer != null && basePlayer.Distance(serverEntity as BaseEntity) <= num)))
			{
				pooledList.Add(serverEntity);
			}
		}
		UnityEngine.Debug.Log($"Deleting {pooledList.Count} {text}...");
		foreach (BaseNetworkable item in pooledList)
		{
			item.Kill();
		}
	}

	[ServerVar]
	public static void deleteEntityById(Arg arg)
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < arg.Args.Length; i++)
		{
			NetworkableId entityID = ArgEx.GetEntityID(arg, i);
			BaseNetworkable baseNetworkable = BaseNetworkable.serverEntities.Find(entityID);
			if (baseNetworkable != null)
			{
				stringBuilder.AppendLine($"Deleting {baseNetworkable}");
				baseNetworkable.Kill();
			}
		}
		arg.ReplyWith(stringBuilder.ToString());
	}

	[ServerVar]
	public static void printgroups(Arg arg)
	{
		UnityEngine.Debug.Log("Server");
		foreach (BaseNetworkable serverEntity in BaseNetworkable.serverEntities)
		{
			UnityEngine.Debug.Log(string.Format("{0}:{1}{2}", serverEntity.PrefabName, serverEntity.net.group.ID, serverEntity.net.group.restricted ? "/Restricted" : string.Empty));
		}
	}

	[ServerVar(Help = "Takes you in and out of your current network group, causing you to delete and then download all entities in your PVS again")]
	public static void flushgroup(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (!(basePlayer == null))
		{
			basePlayer.net.SwitchGroup(BaseNetworkable.LimboNetworkGroup);
			basePlayer.UpdateNetworkGroup();
		}
	}

	[ServerVar(Help = "Break the current held object")]
	public static void breakheld(Arg arg)
	{
		Item activeItem = ArgEx.Player(arg).GetActiveItem();
		activeItem?.LoseCondition(activeItem.condition * 2f);
	}

	[ServerVar(Help = "Breaks the currently held shield")]
	public static void breakshield(Arg arg)
	{
		if (ArgEx.Player(arg).GetActiveShield(out var foundShield) && foundShield.GetItem() != null)
		{
			foundShield.GetItem().LoseCondition(999f);
		}
	}

	[ServerVar(Help = "Almost break the current held object")]
	public static void breakheld_almost(Arg arg)
	{
		Item activeItem = ArgEx.Player(arg).GetActiveItem();
		if (activeItem != null && activeItem.hasCondition)
		{
			activeItem.condition = 1f;
		}
	}

	[ServerVar(Help = "reset all puzzles")]
	public static void puzzlereset(Arg arg)
	{
		if (ArgEx.Player(arg) == null)
		{
			return;
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("iterating...");
		foreach (PuzzleReset allReset in PuzzleReset.AllResets)
		{
			stringBuilder.AppendLine($"resetting puzzle at :{allReset.transform.position}");
			allReset.DoReset();
			allReset.ResetTimer();
		}
		arg.ReplyWith(stringBuilder.ToString());
	}

	[ServerVar(EditorOnly = true, Help = "respawn all puzzles from their prefabs")]
	public static void puzzleprefabrespawn(Arg arg)
	{
		foreach (BaseNetworkable item in BaseNetworkable.serverEntities.Where((BaseNetworkable x) => x is IOEntity && PrefabAttribute.server.Find<Construction>(x.prefabID) == null).ToList())
		{
			item.Kill();
		}
		foreach (MonumentInfo monument in TerrainMeta.Path.Monuments)
		{
			GameObject gameObject = GameManager.server.FindPrefab(monument.gameObject.name);
			if (gameObject == null)
			{
				continue;
			}
			Dictionary<IOEntity, IOEntity> dictionary = new Dictionary<IOEntity, IOEntity>();
			IOEntity[] componentsInChildren = gameObject.GetComponentsInChildren<IOEntity>(includeInactive: true);
			foreach (IOEntity iOEntity in componentsInChildren)
			{
				Quaternion rot = monument.transform.rotation * iOEntity.transform.rotation;
				Vector3 pos = monument.transform.TransformPoint(iOEntity.transform.position);
				BaseEntity newEntity = GameManager.server.CreateEntity(iOEntity.PrefabName, pos, rot);
				IOEntity iOEntity2 = newEntity as IOEntity;
				if (!(iOEntity2 != null))
				{
					continue;
				}
				dictionary.Add(iOEntity, iOEntity2);
				DoorManipulator doorManipulator = newEntity as DoorManipulator;
				if (doorManipulator != null)
				{
					List<Door> obj = Facepunch.Pool.Get<List<Door>>();
					global::Vis.Entities(newEntity.transform.position, 10f, obj);
					Door door = obj.OrderBy((Door x) => x.Distance(newEntity.transform.position)).FirstOrDefault();
					if (door != null)
					{
						doorManipulator.targetDoor = door;
					}
					Facepunch.Pool.FreeUnmanaged(ref obj);
				}
				CardReader cardReader = newEntity as CardReader;
				if (cardReader != null)
				{
					CardReader cardReader2 = iOEntity as CardReader;
					if (cardReader2 != null)
					{
						cardReader.accessLevel = cardReader2.accessLevel;
						cardReader.accessDuration = cardReader2.accessDuration;
					}
				}
				TimerSwitch timerSwitch = newEntity as TimerSwitch;
				if (timerSwitch != null)
				{
					TimerSwitch timerSwitch2 = iOEntity as TimerSwitch;
					if (timerSwitch2 != null)
					{
						timerSwitch.timerLength = timerSwitch2.timerLength;
					}
				}
			}
			foreach (KeyValuePair<IOEntity, IOEntity> item2 in dictionary)
			{
				IOEntity key = item2.Key;
				IOEntity value = item2.Value;
				for (int num2 = 0; num2 < key.outputs.Length; num2++)
				{
					if (!(key.outputs[num2].connectedTo.ioEnt == null))
					{
						value.outputs[num2].connectedTo.ioEnt = dictionary[key.outputs[num2].connectedTo.ioEnt];
						value.outputs[num2].connectedToSlot = key.outputs[num2].connectedToSlot;
					}
				}
			}
			foreach (IOEntity value2 in dictionary.Values)
			{
				value2.Spawn();
			}
		}
	}

	[ServerVar(Help = "Break all the items in your inventory whose name match the passed string")]
	public static void breakitem(Arg arg)
	{
		string needle = arg.GetString(0);
		foreach (Item item in ArgEx.Player(arg).inventory.containerMain.itemList)
		{
			if (item.info.shortname.Contains(needle, CompareOptions.IgnoreCase) && item.hasCondition)
			{
				item.LoseCondition(item.condition * 2f);
			}
		}
	}

	[ServerVar(ClientAdmin = true, Help = "Refills the vital of a target player. eg. debug.refillsvital jim - leave blank to target yourself, can take multiple players at once. Will revive players if they are injured. To disable this, pass in --no-recover as the first argument.")]
	public static void refillvitals(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (basePlayer == null)
		{
			return;
		}
		int num = 0;
		bool shouldPlayerRecover = true;
		if (arg.GetString(0) == "--no-recover")
		{
			shouldPlayerRecover = false;
			num++;
		}
		arg.TryRemoveKeyBindEventArgs();
		if (arg.Args == null || num >= arg.Args.Length)
		{
			RefillPlayerVitals(basePlayer, shouldPlayerRecover);
			return;
		}
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = num; i < arg.Args.Length; i++)
		{
			string text = arg.GetString(i);
			BasePlayer basePlayer2 = ((!(text == basePlayer.displayName)) ? (string.IsNullOrEmpty(text) ? null : ArgEx.GetPlayerOrSleeperOrBot(arg, i)) : basePlayer);
			if (basePlayer2 == null)
			{
				stringBuilder.AppendLine("Could not find player '" + text + "'");
				continue;
			}
			RefillPlayerVitals(basePlayer2, shouldPlayerRecover);
			stringBuilder.AppendLine("Refilled '" + text + "' vitals");
		}
		arg.ReplyWith(stringBuilder.ToString());
	}

	[ServerVar(ClientAdmin = true, Help = "Refills the vitals of all active players on the server. Will revive players if they are injured. To disable this, pass in --no-recover as the first argument.")]
	public static void refillvitalsall(Arg arg)
	{
		StringBuilder stringBuilder = new StringBuilder();
		bool shouldPlayerRecover = arg.GetString(0) != "--no-recover";
		foreach (BasePlayer activePlayer in BasePlayer.activePlayerList)
		{
			if (!(activePlayer == null))
			{
				RefillPlayerVitals(activePlayer, shouldPlayerRecover);
				stringBuilder.AppendLine("Refilled player '" + activePlayer.displayName + "' vitals");
			}
		}
		foreach (BasePlayer bot in BasePlayer.bots)
		{
			if (!(bot == null))
			{
				RefillPlayerVitals(bot, shouldPlayerRecover);
				stringBuilder.AppendLine("Refilled bot '" + bot.displayName + "' vitals");
			}
		}
		arg.ReplyWith(stringBuilder.ToString());
	}

	private static void RefillPlayerVitals(BasePlayer player, bool shouldPlayerRecover)
	{
		if (shouldPlayerRecover && player.IsWounded())
		{
			player.StopWounded();
		}
		AdjustHealth(player, 1000f);
		AdjustCalories(player, 1000f);
		AdjustHydration(player, 1000f);
		AdjustRadiation(player, -10000f);
		AdjustBleeding(player, -10000f);
	}

	[ServerVar(Help = "To disable revival if player is downed, pass in --no-recover as the first argument.")]
	public static void heal(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		bool flag = true;
		int num = 0;
		if (arg.GetString(0) == "--no-recover")
		{
			flag = false;
			num++;
		}
		if (flag && basePlayer.IsWounded())
		{
			basePlayer.StopWounded();
		}
		AdjustHealth(basePlayer, arg.GetInt(num, 1));
	}

	[ServerVar]
	public static void hurt(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		int num = arg.GetInt(0, 1);
		string text = arg.GetString(1, string.Empty);
		HitInfo hitInfo = new HitInfo(basePlayer, basePlayer, DamageType.Bullet, num);
		if (!string.IsNullOrEmpty(text))
		{
			hitInfo.HitBone = StringPool.Get(text);
		}
		basePlayer.OnAttacked(hitInfo);
	}

	[ServerVar]
	public static void eat(Arg arg)
	{
		AdjustCalories(ArgEx.Player(arg), arg.GetInt(0, 1), arg.GetInt(1, 1));
	}

	[ServerVar]
	public static void drink(Arg arg)
	{
		AdjustHydration(ArgEx.Player(arg), arg.GetInt(0, 1), arg.GetInt(1, 1));
	}

	[ServerVar]
	public static void sethealth(Arg arg)
	{
		if (!arg.HasArgs())
		{
			arg.ReplyWith("Please enter an amount.");
			return;
		}
		int num = arg.GetInt(0);
		BasePlayer usePlayer = GetUsePlayer(arg, 1);
		if ((bool)usePlayer)
		{
			usePlayer.SetHealth(num);
		}
	}

	[ServerVar]
	public static void setmaxhealth(Arg arg)
	{
		if (!arg.HasArgs())
		{
			arg.ReplyWith("Please enter an amount.");
			return;
		}
		int num = arg.GetInt(0);
		BasePlayer usePlayer = GetUsePlayer(arg, 1);
		if (usePlayer == null)
		{
			arg.ReplyWith("Player not found");
			return;
		}
		usePlayer.OverrideMaxHealth(num);
		if (num <= 0)
		{
			arg.ReplyWith("Reset max health");
		}
		else
		{
			arg.ReplyWith($"Set max health to {num}");
		}
	}

	[ServerVar]
	public static void setdamage(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (!arg.HasArgs())
		{
			arg.ReplyWith("Please enter an amount.");
			return;
		}
		int num = arg.GetInt(0);
		BasePlayer usePlayer = GetUsePlayer(arg, 1);
		if ((bool)usePlayer)
		{
			float damageAmount = usePlayer.health - (float)num;
			HitInfo info = new HitInfo(basePlayer, basePlayer, DamageType.Bullet, damageAmount);
			usePlayer.OnAttacked(info);
		}
	}

	[ServerVar]
	public static void setfood(Arg arg)
	{
		setattribute(arg, MetabolismAttribute.Type.Calories);
	}

	[ServerVar]
	public static void setwater(Arg arg)
	{
		setattribute(arg, MetabolismAttribute.Type.Hydration);
	}

	[ServerVar]
	public static void setradiation(Arg arg)
	{
		setattribute(arg, MetabolismAttribute.Type.Radiation);
	}

	private static void AdjustHealth(BasePlayer player, float amount, string bone = null)
	{
		player.health += amount;
	}

	private static void AdjustCalories(BasePlayer player, float amount, float time = 1f)
	{
		player.metabolism.ApplyChange(MetabolismAttribute.Type.Calories, amount, time);
	}

	private static void AdjustHydration(BasePlayer player, float amount, float time = 1f)
	{
		player.metabolism.ApplyChange(MetabolismAttribute.Type.Hydration, amount, time);
	}

	private static void AdjustRadiation(BasePlayer player, float amount, float time = 1f)
	{
		player.metabolism.SetAttribute(MetabolismAttribute.Type.Radiation, amount);
	}

	private static void AdjustBleeding(BasePlayer player, float amount, float time = 1f)
	{
		player.metabolism.SetAttribute(MetabolismAttribute.Type.Bleeding, amount);
	}

	private static void setattribute(Arg arg, MetabolismAttribute.Type type)
	{
		if (!arg.HasArgs())
		{
			arg.ReplyWith("Please enter an amount.");
			return;
		}
		int num = arg.GetInt(0);
		BasePlayer usePlayer = GetUsePlayer(arg, 1);
		if ((bool)usePlayer)
		{
			usePlayer.metabolism.SetAttribute(type, num);
		}
	}

	private static BasePlayer GetUsePlayer(Arg arg, int playerArgument)
	{
		BasePlayer basePlayer = null;
		if (arg.HasArgs(playerArgument + 1))
		{
			BasePlayer player = ArgEx.GetPlayer(arg, playerArgument);
			if (!player)
			{
				return null;
			}
			return player;
		}
		return ArgEx.Player(arg);
	}

	[ServerVar]
	public static void ResetSleepingBagTimers(Arg arg)
	{
		SleepingBag.ResetTimersForPlayer(ArgEx.Player(arg));
	}

	[ServerVar(Help = "Deducts the given number of hours from all spoilable food on the server")]
	public static void FoodSpoilingDeductTimeHours(Arg arg)
	{
		ItemModFoodSpoiling.DeductTimeFromAll(TimeSpan.FromHours(arg.GetFloat(0)));
	}

	[ServerVar(Help = "Spoils all food on the server")]
	public static void FoodSpoilingSpoilAll()
	{
		ItemModFoodSpoiling.DeductTimeFromAll(TimeSpan.MaxValue);
	}

	[ServerVar(Help = "Applies the given number of hours to all food in the players inventory")]
	public static void FoodSpoilingInventoryHours(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (basePlayer == null)
		{
			return;
		}
		int num = arg.GetInt(0);
		PooledList<Item> spoilList = Facepunch.Pool.Get<PooledList<Item>>();
		try
		{
			FindSpoilableItems(basePlayer.inventory.containerMain.itemList);
			FindSpoilableItems(basePlayer.inventory.containerBelt.itemList);
			foreach (Item item in spoilList)
			{
				ItemModFoodSpoiling.FoodSpoilingWorkQueue.DeductTimeFromFoodItem(item, (float)num * 60f * 60f, setDirty: true);
			}
		}
		finally
		{
			if (spoilList != null)
			{
				((IDisposable)spoilList).Dispose();
			}
		}
		void FindSpoilableItems(List<Item> items)
		{
			foreach (Item item2 in items)
			{
				if (item2.info.TryGetComponent<ItemModFoodSpoiling>(out var _))
				{
					spoilList.Add(item2);
				}
			}
		}
	}

	[ServerVar]
	public static void ForceChickensSpawnEgg(Arg arg)
	{
		float radius = arg.GetFloat(0, 50f);
		if (ArgEx.Player(arg) == null)
		{
			return;
		}
		using PooledList<Chicken> pooledList = Facepunch.Pool.Get<PooledList<Chicken>>();
		global::Vis.Entities(ArgEx.Player(arg).transform.position, radius, pooledList, 2048);
		foreach (Chicken item in pooledList)
		{
			if (item.isServer)
			{
				item.SpawnEgg();
			}
		}
	}

	[ServerVar]
	public static void dropWorldItems(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (basePlayer == null)
		{
			return;
		}
		int num = arg.GetInt(0);
		ItemDefinition itemDefinition = ItemManager.FindItemDefinition(arg.GetString(1));
		Vector3 point = basePlayer.eyes.HeadRay().GetPoint(1f);
		if (!(itemDefinition == null))
		{
			for (int i = 0; i < num; i++)
			{
				ItemManager.Create(itemDefinition, 1, 0uL).Drop(point, Vector3.zero, Quaternion.identity);
				point += Vector3.up * 0.3f;
			}
		}
	}

	[ServerVar(Help = "Spawns one of every deployable in a grid")]
	public static void spawn_all_deployables(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (basePlayer == null || (!basePlayer.IsAdmin && !basePlayer.IsDeveloper))
		{
			arg.ReplyWith("Must be called by admin player");
			return;
		}
		arg.ReplyWith("Spawning all deployables");
		bool stability = Server.stability;
		Server.stability = false;
		try
		{
			Vector3 position = basePlayer.transform.position;
			List<ItemModDeployable> list = (from x in ItemManager.itemList
				where x.GetComponent<ItemModDeployable>() != null && x.shortname != "legacy.shelter.wood"
				select x.GetComponent<ItemModDeployable>()).ToList();
			int num = 12;
			float num2 = Mathf.Ceil(Mathf.Sqrt(list.Count));
			float num3 = num2 * (float)num / 2f;
			for (int num4 = 0; num4 < list.Count; num4++)
			{
				Vector3 pos = new Vector3(position.x - num3 + (float)num * ((float)num4 % num2), position.y, position.z - num3 + (float)num * Mathf.Floor((float)num4 / num2));
				GameManager.server.CreateEntity(list[num4].entityPrefab.resourcePath, pos)?.Spawn();
			}
		}
		finally
		{
			Server.stability = stability;
		}
	}

	[ServerVar]
	public static void removeOverlappingStaticSpawnPoints(Arg arg)
	{
		using PooledList<StaticRespawnArea> pooledList = Facepunch.Pool.Get<PooledList<StaticRespawnArea>>();
		foreach (StaticRespawnArea staticRespawnArea2 in StaticRespawnArea.staticRespawnAreas)
		{
			pooledList.Add(staticRespawnArea2);
		}
		int num = 0;
		for (int i = 0; i < pooledList.Count; i++)
		{
			StaticRespawnArea staticRespawnArea = pooledList[i];
			bool flag = false;
			foreach (StaticRespawnArea item in pooledList)
			{
				if (item != staticRespawnArea && item.Distance(staticRespawnArea) < 1f)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				pooledList.RemoveAt(i);
				i--;
				num++;
				staticRespawnArea.Kill();
			}
		}
		arg.ReplyWith($"Destroyed {num} overlapping static spawn points");
	}

	[ServerVar]
	public static void setUnloadableCarFillPercent(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (basePlayer == null)
		{
			return;
		}
		Vector3 position = basePlayer.transform.position;
		using PooledList<TrainCarUnloadable> pooledList = Facepunch.Pool.Get<PooledList<TrainCarUnloadable>>();
		global::Vis.Entities(position, 3f, pooledList, 8192);
		float num = Mathf.Clamp01(arg.GetFloat(0));
		foreach (TrainCarUnloadable item in pooledList)
		{
			if (!item.isServer)
			{
				continue;
			}
			foreach (Item item2 in item.GetStorageContainer().inventory.itemList)
			{
				item2.amount = Mathf.Max(Mathf.RoundToInt(num), 1);
			}
			item.SetLootPercentage(num);
			item.SetVisualOreLevel(num);
			item.SendNetworkUpdate();
			arg.ReplyWith($"Set ore level to {num} on {item.PrefabName}");
		}
	}

	[ServerVar(Help = "fillmounts <radius> - Spawns and mounts a player on every mount point in radius")]
	public static void fillmounts(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (basePlayer == null)
		{
			return;
		}
		Vector3 position = basePlayer.transform.position;
		int num = Mathf.Clamp(arg.GetInt(0), 0, 100);
		int layerMask = 1218519297;
		using PooledList<BaseMountable> pooledList = Facepunch.Pool.Get<PooledList<BaseMountable>>();
		global::Vis.Entities(position, num, pooledList, layerMask);
		foreach (BaseMountable item in pooledList)
		{
			if (!item.isServer)
			{
				continue;
			}
			if (item is BaseVehicle { allMountPoints: var allMountPoints })
			{
				foreach (BaseVehicle.MountPointInfo item2 in allMountPoints)
				{
					if (item2 != null && item2.mountable != null && !item2.mountable.AnyMounted())
					{
						SpawnAndMountPlayer(item2.mountable, arg);
					}
				}
			}
			else if (!item.AnyMounted())
			{
				SpawnAndMountPlayer(item, arg);
			}
		}
	}

	private static void SpawnAndMountPlayer(BaseMountable mountable, Arg arg)
	{
		BasePlayer basePlayer = GameManager.server.CreateEntity("assets/prefabs/player/player.prefab") as BasePlayer;
		basePlayer.Spawn();
		mountable.AttemptMount(basePlayer, doMountChecks: false);
		if (!basePlayer.isMounted)
		{
			arg.ReplyWith("Failed to mount: " + mountable.ShortPrefabName);
			basePlayer.Kill();
		}
	}

	[ServerVar(Help = "Spawn lots of IO entities to lag the server")]
	public static void bench_io(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (basePlayer == null || !basePlayer.IsAdmin)
		{
			return;
		}
		int num = arg.GetInt(0, 50);
		string name = arg.GetString(1, "water_catcher_small");
		List<IOEntity> list = new List<IOEntity>();
		WaterCatcher waterCatcher = null;
		Vector3 position = ArgEx.Player(arg).transform.position;
		string[] array = (from x in GameManifest.Current.entities
			where Path.GetFileNameWithoutExtension(x).Contains(name, CompareOptions.IgnoreCase)
			select x.ToLower()).ToArray();
		if (array.Length == 0)
		{
			arg.ReplyWith("Couldn't find io prefab \"" + array[0] + "\"");
			return;
		}
		if (array.Length > 1)
		{
			string text = array.FirstOrDefault((string x) => string.Compare(Path.GetFileNameWithoutExtension(x), name, StringComparison.OrdinalIgnoreCase) == 0);
			if (text == null)
			{
				UnityEngine.Debug.Log($"{arg} failed to find io entity \"{name}\"");
				arg.ReplyWith("Unknown entity - could be:\n\n" + string.Join("\n", array.Select(Path.GetFileNameWithoutExtension).ToArray()));
				return;
			}
			array[0] = text;
		}
		for (int num2 = 0; num2 < num; num2++)
		{
			Vector3 pos = position + new Vector3(num2 * 5, 0f, 0f);
			Quaternion identity = Quaternion.identity;
			BaseEntity baseEntity = GameManager.server.CreateEntity(array[0], pos, identity);
			if (!baseEntity)
			{
				continue;
			}
			baseEntity.Spawn();
			WaterCatcher component = baseEntity.GetComponent<WaterCatcher>();
			if ((bool)component)
			{
				list.Add(component);
				if (waterCatcher != null)
				{
					Connect(waterCatcher, component);
				}
				if (num2 == num - 1)
				{
					Connect(component, list.First());
				}
				waterCatcher = component;
			}
		}
		static void Connect(IOEntity InputIOEnt, IOEntity OutputIOEnt)
		{
			int num3 = 0;
			int num4 = 0;
			WireTool.WireColour wireColour = WireTool.WireColour.Gray;
			IOEntity.IOSlot iOSlot = InputIOEnt.inputs[num3];
			IOEntity.IOSlot obj = OutputIOEnt.outputs[num4];
			iOSlot.connectedTo.Set(OutputIOEnt);
			iOSlot.connectedToSlot = num4;
			iOSlot.wireColour = wireColour;
			iOSlot.connectedTo.Init();
			obj.connectedTo.Set(InputIOEnt);
			obj.connectedToSlot = num3;
			obj.wireColour = wireColour;
			obj.connectedTo.Init();
			obj.linePoints = new Vector3[2]
			{
				Vector3.zero,
				OutputIOEnt.transform.InverseTransformPoint(InputIOEnt.transform.TransformPoint(iOSlot.handlePosition))
			};
			OutputIOEnt.MarkDirtyForceUpdateOutputs();
			OutputIOEnt.SendNetworkUpdate();
			InputIOEnt.SendNetworkUpdate();
			OutputIOEnt.SendChangedToRoot(forceUpdate: true);
		}
	}

	[ServerVar]
	public static void completeMissionStage(Arg arg)
	{
		int num = arg.GetInt(0, -1);
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (!(basePlayer != null) || basePlayer.GetActiveMission() == -1)
		{
			return;
		}
		BaseMission.MissionInstance missionInstance = basePlayer.missions[basePlayer.GetActiveMission()];
		if (missionInstance == null)
		{
			return;
		}
		for (int i = 0; i < missionInstance.objectiveStatuses.Length; i++)
		{
			if (!missionInstance.objectiveStatuses[i].completed && (i == num || (num == -1 && !missionInstance.objectiveStatuses[i].completed)))
			{
				missionInstance.GetMission().objectives[i].objective.ObjectiveStarted(basePlayer, i, missionInstance);
				missionInstance.GetMission().objectives[i].objective.CompleteObjective(i, missionInstance, basePlayer);
				break;
			}
		}
	}

	[ServerVar]
	public static void completeMission(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (!(basePlayer != null) || basePlayer.GetActiveMission() == -1)
		{
			return;
		}
		BaseMission.MissionInstance missionInstance = basePlayer.missions[basePlayer.GetActiveMission()];
		if (missionInstance == null)
		{
			return;
		}
		for (int i = 0; i < missionInstance.objectiveStatuses.Length; i++)
		{
			if (!missionInstance.objectiveStatuses[i].completed)
			{
				missionInstance.GetMission().objectives[i].objective.CompleteObjective(i, missionInstance, basePlayer);
			}
		}
	}

	[ServerVar(Help = "Prints out the topologies at your position")]
	public static void print_topologies(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (basePlayer == null)
		{
			return;
		}
		StringBuilder obj = Facepunch.Pool.Get<StringBuilder>();
		int topology = TerrainMeta.TopologyMap.GetTopology(basePlayer.transform.position);
		foreach (TerrainTopology.Enum value in Enum.GetValues(typeof(TerrainTopology.Enum)))
		{
			int num = (int)value;
			if ((topology & num) == num)
			{
				obj.AppendLine(value.ToString());
			}
		}
		arg.ReplyWith(obj.ToString());
		Facepunch.Pool.FreeUnmanaged(ref obj);
	}

	[ServerUserVar]
	public static void startTutorial(Arg arg)
	{
		if (!Server.tutorialEnabled)
		{
			arg.ReplyWith("Tutorial is not enabled on this server");
			return;
		}
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (basePlayer != null && !basePlayer.IsInTutorial)
		{
			basePlayer.StartTutorial(triggerAnalytics: false);
		}
	}

	[ServerVar]
	public static void completeTutorial(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (basePlayer != null && basePlayer.IsInTutorial)
		{
			TutorialIsland currentTutorialIsland = basePlayer.GetCurrentTutorialIsland();
			if (currentTutorialIsland != null)
			{
				currentTutorialIsland.OnPlayerCompletedTutorial(basePlayer, isQuit: false, triggerAnalytics: false);
			}
		}
	}

	[ServerUserVar(ServerAdmin = false)]
	public static void quitTutorial(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (basePlayer != null && basePlayer.IsInTutorial)
		{
			TutorialIsland currentTutorialIsland = basePlayer.GetCurrentTutorialIsland();
			if (currentTutorialIsland != null)
			{
				currentTutorialIsland.OnPlayerCompletedTutorial(basePlayer, isQuit: true, triggerAnalytics: true);
			}
		}
	}

	[ServerVar]
	public static void tutorialStatus(Arg arg)
	{
		ListHashSet<TutorialIsland> tutorialList = TutorialIsland.GetTutorialList(isServer: true);
		using TextTable textTable = Facepunch.Pool.Get<TextTable>();
		textTable.AddColumns("Index", "ID", "Player Name", "Duration", "IsConnected");
		int num = 0;
		foreach (TutorialIsland item in tutorialList)
		{
			BasePlayer basePlayer = item.ForPlayer.Get(serverside: true);
			textTable.AddRow(num++.ToString(), (item.net.group.ID - 1).ToString(), (basePlayer != null) ? basePlayer.displayName : "NULL", TimeSpanEx.ToShortString(item.TutorialDuration), (basePlayer != null) ? basePlayer.IsConnected.ToString() : "NULL");
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine($"Tutorial islands in use: {num}/{TutorialIsland.MaxTutorialIslandCount}");
		stringBuilder.AppendLine(textTable.ToString());
		arg.ReplyWith(stringBuilder.ToString());
	}

	[ServerVar(Help = "Make admin invisible")]
	public static void invis(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (basePlayer == null)
		{
			return;
		}
		bool flag = arg.GetBool(0, !basePlayer.isInvisible);
		if (flag && !basePlayer.isInvisible)
		{
			if (Interface.CallHook("OnPlayerVanish", basePlayer) != null)
			{
				return;
			}
			foreach (Connection subscriber in basePlayer.net.group.subscribers)
			{
				BasePlayer basePlayer2 = subscriber.player as BasePlayer;
				if (subscriber != basePlayer.net.connection && basePlayer.ShouldNetworkTo(basePlayer2) && !basePlayer2.IsSpectating())
				{
					basePlayer.DestroyOnClient(subscriber);
				}
			}
			if (ServerOcclusion.OcclusionEnabled && BasePlayer.UseOcclusionV2)
			{
				basePlayer.OcclusionMakeSubscribersForget();
			}
			basePlayer.isInvisible = true;
			BasePlayer.invisPlayers.Add(basePlayer);
			basePlayer.DisablePlayerCollider();
			SimpleAIMemory.AddIgnorePlayer(basePlayer);
			BaseEntity.Query.Server.RemovePlayer(basePlayer);
			Interface.CallHook("OnPlayerVanished", basePlayer);
		}
		else if (!flag && basePlayer.isInvisible)
		{
			if (Interface.CallHook("OnPlayerUnvanish", basePlayer) != null)
			{
				return;
			}
			basePlayer.isInvisible = false;
			BasePlayer.invisPlayers.Remove(basePlayer);
			basePlayer.EnablePlayerCollider();
			if (!ServerOcclusion.OcclusionEnabled || !BasePlayer.UseOcclusionV2)
			{
				foreach (Connection subscriber2 in basePlayer.net.group.subscribers)
				{
					BasePlayer player = subscriber2.player as BasePlayer;
					if (basePlayer.ShouldNetworkTo(player))
					{
						basePlayer.SendAsSnapshotWithChildren(player);
					}
				}
			}
			SimpleAIMemory.RemoveIgnorePlayer(basePlayer);
			BaseEntity.Query.Server.RemovePlayer(basePlayer);
			BaseEntity.Query.Server.AddPlayer(basePlayer);
			Interface.CallHook("OnPlayerUnvanished", basePlayer);
		}
		arg.ReplyWith("Invis: " + basePlayer.isInvisible);
		basePlayer.Command("debug.setinvis_ui", basePlayer.isInvisible);
	}

	[ServerVar]
	public static void clearPlayerModifiers(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (!(basePlayer == null))
		{
			int count = basePlayer.modifiers.All.Count;
			basePlayer.modifiers.RemoveAll();
			arg.ReplyWith($"Removed {count} modifiers");
		}
	}

	[ServerVar]
	public static void applyBuildingBlockRandomisation(Arg arg)
	{
		int variant = arg.GetInt(0);
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (!(basePlayer == null) && GamePhysics.Trace(basePlayer.eyes.HeadRay(), 0f, out var hitInfo, 3f, 2097408) && RaycastHitEx.GetEntity(hitInfo) is SimpleBuildingBlock simpleBuildingBlock)
		{
			simpleBuildingBlock.SetVariant(variant);
		}
	}

	[ServerVar]
	public static void vineSwingingReport(Arg arg)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		foreach (BaseNetworkable serverEntity in BaseNetworkable.serverEntities)
		{
			if (serverEntity is VineSwingingTree)
			{
				num++;
			}
			if (serverEntity is VineMountable vineMountable)
			{
				num2++;
				num3 += vineMountable.DestinationCount;
			}
		}
		using TextTable textTable = Facepunch.Pool.Get<TextTable>();
		textTable.AddColumns("Entity", "Count");
		textTable.AddRow("VineTrees", num.ToString());
		textTable.AddRow("VineMountables", num2.ToString());
		textTable.AddRow("VineMountableDirections", ((float)num3 / (float)num2).ToString());
		arg.ReplyWith(textTable.ToString());
	}

	[ServerVar]
	public static void vineSwingingHighlight(Arg arg)
	{
		foreach (BaseNetworkable serverEntity in BaseNetworkable.serverEntities)
		{
			if (serverEntity is VineMountable vineMountable)
			{
				vineMountable.Highlight(ArgEx.Player(arg));
			}
		}
	}

	[ServerVar]
	public static void respawnVineTreesInRadius(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (basePlayer == null)
		{
			return;
		}
		float num = arg.GetFloat(0);
		using PooledList<Collider> pooledList = Facepunch.Pool.Get<PooledList<Collider>>();
		GamePhysics.OverlapSphere(basePlayer.transform.position, num, pooledList, 1073741824);
		int num2 = 0;
		int num3 = 0;
		using PooledList<VineSwingingTreeStump> pooledList2 = Facepunch.Pool.Get<PooledList<VineSwingingTreeStump>>();
		foreach (Collider item in pooledList)
		{
			VineSwingingTreeStump vineSwingingTreeStump = GameObjectEx.ToBaseEntity(item) as VineSwingingTreeStump;
			if (vineSwingingTreeStump != null && vineSwingingTreeStump.isServer && !pooledList2.Contains(vineSwingingTreeStump))
			{
				pooledList2.Add(vineSwingingTreeStump);
				if (vineSwingingTreeStump.RespawnTree())
				{
					num2++;
				}
				else
				{
					num3++;
				}
			}
		}
		arg.ReplyWith($"Respawned {num2} trees in {num}m, {num3} were blocked by players");
	}

	[ServerVar]
	public static void conveyorStrictModeReport(Arg arg)
	{
		StringBuilder stringBuilder = new StringBuilder();
		IndustrialConveyor[] array = BaseEntity.Util.FindAll<IndustrialConveyor>();
		foreach (IndustrialConveyor industrialConveyor in array)
		{
			if (industrialConveyor.strictMode)
			{
				stringBuilder.AppendLine($"{industrialConveyor.transform.position}");
			}
		}
		arg.ReplyWith(stringBuilder.ToString());
	}

	[ServerVar]
	public static void test_custom_vitals(Arg arg)
	{
		int num = Mathf.Clamp(arg.GetInt(0, 1), 0, 100);
		int timeLeft = arg.GetInt(1, 60);
		string text = arg.GetString(2, "ss");
		CustomVitals customVitals = new CustomVitals
		{
			vitals = new List<CustomVitalInfo>()
		};
		for (int i = 0; i < num; i++)
		{
			customVitals.vitals.Add(new CustomVitalInfo
			{
				active = true,
				backgroundColor = Color.red,
				iconColor = Color.green,
				leftTextColor = Color.blue,
				rightTextColor = Color.yellow,
				leftText = "Left",
				rightText = "Right {timeleft:" + text + "}",
				timeLeft = timeLeft
			});
		}
		CommunityEntity.ServerInstance.SendCustomVitals(ArgEx.Player(arg), customVitals);
	}

	[ServerVar(Help = "0 = can't throw, 1 = can throw & melee, 2 = only throwable")]
	public static void setthrowable(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (basePlayer == null)
		{
			arg.ReplyWith("This can only be ran by players");
			return;
		}
		BaseMelee baseMelee = basePlayer.GetHeldEntity() as BaseMelee;
		if (baseMelee == null)
		{
			arg.ReplyWith("You must be holding a melee weapon");
			return;
		}
		if (!arg.HasArgs())
		{
			arg.ReplyWith("Format is 'setthrowable {0-2}");
			return;
		}
		int num = arg.GetInt(0);
		switch (num)
		{
		case 0:
			baseMelee.canThrowAsProjectile = false;
			baseMelee.onlyThrowAsProjectile = false;
			break;
		case 1:
			baseMelee.canThrowAsProjectile = true;
			baseMelee.onlyThrowAsProjectile = false;
			break;
		case 2:
			baseMelee.canThrowAsProjectile = true;
			baseMelee.onlyThrowAsProjectile = true;
			break;
		default:
			arg.ReplyWith($"Invalid throwable value {num}, must be 0 (not throwable), 1 (throwable) or 2 (only throwable)");
			return;
		}
		baseMelee.SendNetworkUpdate();
		arg.ReplyWith($"Set canThrowAsProjectile to {num} on {baseMelee.ShortPrefabName}");
	}

	[ServerVar]
	public static void applyPuzzleResetTime(Arg arg)
	{
		float time = arg.GetFloat(0);
		PuzzleReset[] array = UnityEngine.Object.FindObjectsByType<PuzzleReset>(FindObjectsSortMode.None);
		for (int i = 0; i < array.Length; i++)
		{
			array[i].DebugApplyPuzzleResetTime(time);
		}
	}

	[ServerVar]
	public static void puzzleResetInfo(Arg arg)
	{
		BasePlayer bp = ArgEx.Player(arg);
		foreach (PuzzleReset allReset in PuzzleReset.AllResets)
		{
			if (!allReset.IsPlayerInRange(bp))
			{
				continue;
			}
			List<string> list = new List<string>();
			allReset.GetDebugInfo(list);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(UnityEngine.TransformEx.GetRecursiveName(allReset.transform));
			foreach (string item in list)
			{
				stringBuilder.AppendLine(item);
			}
			arg.ReplyWith(stringBuilder.ToString());
			return;
		}
		arg.ReplyWith("Player is not inside any PuzzleResets");
	}

	[ClientVar(ClientAdmin = true)]
	[ServerVar]
	public static void printqueues(Arg arg)
	{
		bool flag = arg.HasArg("--json");
		using TextTable textTable = Facepunch.Pool.Get<TextTable>();
		textTable.ShouldPadColumns = !flag;
		textTable.AddColumns("Name", "Processed", "Size", "Execution Time");
		foreach (ObjectWorkQueue item in ObjectWorkQueue.All.OrderBy((ObjectWorkQueue x) => x.Name))
		{
			string text = ((item.TotalExecutionTime.TotalMilliseconds < 1000.0) ? $"{Math.Floor(item.TotalExecutionTime.TotalMilliseconds)}ms" : $"{Math.Round(item.TotalExecutionTime.TotalSeconds, 2)}s");
			textTable.AddRow(item.Name, item.TotalProcessedCount.ToString(), item.QueueLength.ToString(), text);
		}
		arg.ReplyWith(flag ? textTable.ToJson() : textTable.ToString());
	}
}
