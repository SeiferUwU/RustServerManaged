using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facepunch;
using UnityEngine;

namespace ConVar;

[Factory("spawn")]
public class Spawn : ConsoleSystem
{
	[ServerVar]
	public static float min_rate = 0.5f;

	[ServerVar]
	public static float max_rate = 1f;

	[ServerVar]
	public static float min_density = 0.5f;

	[ServerVar]
	public static float max_density = 1f;

	[ServerVar]
	public static float player_base = 100f;

	[ServerVar]
	public static float player_scale = 2f;

	[ServerVar]
	public static bool respawn_populations = true;

	[ServerVar]
	public static bool respawn_groups = true;

	[ServerVar]
	public static bool respawn_individuals = true;

	[ServerVar]
	public static float tick_populations = 60f;

	[ServerVar]
	public static float tick_individuals = 300f;

	[ServerVar(Help = "When scaling loot respawn rates by population, this will be considered the 'max' population, preventing loot speeding up if player counts are above this")]
	public static int population_cap_rate = 300;

	[ServerVar(Help = "If set the loot spawn system will consider this the player count, not the actual player count. Useful for testing")]
	public static int loot_population_test = 0;

	[ServerVar]
	public static void fill_populations(Arg args)
	{
		if ((bool)SingletonComponent<SpawnHandler>.Instance)
		{
			SingletonComponent<SpawnHandler>.Instance.FillPopulations();
		}
	}

	[ServerVar]
	public static void delete_populations(Arg args)
	{
		if (!args.HasArgs())
		{
			args.ReplyWith("Usage: delete_populations <population_name> ...");
			return;
		}
		string[] args2 = args.Args;
		foreach (string name in args2)
		{
			SingletonComponent<SpawnHandler>.Instance?.DeletePopulation(name);
		}
	}

	[ServerVar]
	public static void delete_all_populations(Arg args)
	{
		SingletonComponent<SpawnHandler>.Instance?.DeleteAllPopulations();
	}

	[ServerVar(Help = "<iterations> - Simulates a number of iterations on the closest loot container and sums up the items spawned")]
	public static void simulate_loot(Arg args)
	{
		BasePlayer player = ArgEx.Player(args);
		if (player == null)
		{
			args.ReplyWith("Must be called from player");
			return;
		}
		int num = Mathf.Clamp(args.GetInt(0, 100), 1, 10000);
		List<LootContainer> list = new List<LootContainer>();
		global::Vis.Entities(player.transform.position, 5f, list, -1, QueryTriggerInteraction.Ignore);
		LootContainer lootContainer = list.OrderBy((LootContainer x) => Vector3.Distance(player.transform.position, x.transform.position)).FirstOrDefault();
		if (lootContainer == null)
		{
			args.ReplyWith("No loot container found");
			return;
		}
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		for (int num2 = 0; num2 < num; num2++)
		{
			lootContainer.inventory.Clear();
			ItemManager.DoRemoves();
			lootContainer.PopulateLoot();
			foreach (Item item in lootContainer.inventory.itemList)
			{
				if (item != null)
				{
					dictionary.TryGetValue(item.info.shortname, out var value);
					dictionary[item.info.shortname] = value + item.amount;
				}
			}
		}
		int totalWidth = dictionary.Max((KeyValuePair<string, int> x) => x.Key.Length);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine($"Simulated loot from {num} {lootContainer.ShortPrefabName}:");
		foreach (KeyValuePair<string, int> item2 in dictionary.OrderByDescending((KeyValuePair<string, int> x) => x.Value))
		{
			stringBuilder.AppendLine($"{item2.Key.PadRight(totalWidth)} : {item2.Value}");
		}
		args.ReplyWith(stringBuilder.ToString());
	}

	[ServerVar]
	public static void fill_groups(Arg args)
	{
		if ((bool)SingletonComponent<SpawnHandler>.Instance)
		{
			SingletonComponent<SpawnHandler>.Instance.FillGroups();
		}
	}

	[ServerVar]
	public static void fill_individuals(Arg args)
	{
		if ((bool)SingletonComponent<SpawnHandler>.Instance)
		{
			SingletonComponent<SpawnHandler>.Instance.FillIndividuals();
		}
	}

	[ServerVar]
	public static void report(Arg args)
	{
		if ((bool)SingletonComponent<SpawnHandler>.Instance)
		{
			bool detailed = args.GetBool(0);
			string filter = args.GetString(1, null);
			args.ReplyWith(SingletonComponent<SpawnHandler>.Instance.GetReport(detailed, filter));
		}
		else
		{
			args.ReplyWith("No spawn handler found.");
		}
	}

	[ServerVar]
	public static void scalars(Arg args)
	{
		bool flag = args.HasArg("--json");
		using TextTable textTable = Facepunch.Pool.Get<TextTable>();
		textTable.ShouldPadColumns = !flag;
		textTable.AddColumn("Type");
		textTable.AddColumn("Value");
		textTable.AddRow("Player Fraction", SpawnHandler.PlayerFraction().ToString());
		textTable.AddRow("Player Excess", SpawnHandler.PlayerExcess().ToString());
		textTable.AddRow("Population Rate", SpawnHandler.PlayerLerp(min_rate, max_rate).ToString());
		textTable.AddRow("Population Density", SpawnHandler.PlayerLerp(min_density, max_density).ToString());
		textTable.AddRow("Group Rate", SpawnHandler.PlayerScale(player_scale).ToString());
		args.ReplyWith(flag ? textTable.ToJson() : textTable.ToString());
	}

	[ServerVar]
	public static void cargoshipevent(Arg args)
	{
		BaseEntity baseEntity = GameManager.server.CreateEntity("assets/content/vehicles/boats/cargoship/cargoshiptest.prefab");
		if (baseEntity != null)
		{
			baseEntity.SendMessage("TriggeredEventSpawn", SendMessageOptions.DontRequireReceiver);
			baseEntity.Spawn();
			args.ReplyWith("Cargo ship event has been started");
		}
		else
		{
			args.ReplyWith("Couldn't find cargo ship prefab - maybe it has been renamed?");
		}
	}

	[ServerVar]
	public static void ch47event(Arg args)
	{
		BasePlayer basePlayer = ArgEx.Player(args);
		if (basePlayer == null)
		{
			return;
		}
		if (!CH47LandingZone.HasAnyLandingZones)
		{
			args.ReplyWith("Couldn't find any landing zones for CH47. Not starting the event");
			return;
		}
		int num = args.GetInt(0, 300);
		if (CH47ReinforcementListener.TryCall("assets/Prefabs/NPC/CH47/ch47scientists.entity.prefab", basePlayer.transform.position, num))
		{
			args.ReplyWith($"CH47 event has been started at a distance of {num}m");
		}
		else
		{
			args.ReplyWith("Couldn't start CH47 event");
		}
	}

	[ServerVar]
	public static void cargoshipdockingtest(Arg args)
	{
		if (CargoShip.TotalAvailableHarborDockingPaths == 0)
		{
			args.ReplyWith("No valid harbor dock points");
			return;
		}
		int value = args.GetInt(0);
		value = Mathf.Clamp(value, 0, CargoShip.TotalAvailableHarborDockingPaths);
		BaseEntity baseEntity = GameManager.server.CreateEntity("assets/content/vehicles/boats/cargoship/cargoshiptest.prefab");
		if (baseEntity != null)
		{
			baseEntity.SendMessage("TriggeredEventSpawnDockingTest", value, SendMessageOptions.DontRequireReceiver);
			baseEntity.Spawn();
			args.ReplyWith("Cargo ship event has been started");
		}
		else
		{
			args.ReplyWith("Couldn't find cargo ship prefab - maybe it has been renamed?");
		}
	}

	[ServerVar]
	public static void svShieldDummy(Arg arg)
	{
		Vector3 vector = arg.GetVector3(0);
		Vector3 vector2 = arg.GetVector3(1);
		bool flag = arg.GetBool(2);
		BasePlayer basePlayer = GameManager.server.CreateEntity("assets/prefabs/player/player.prefab", vector, Quaternion.Euler(vector2)) as BasePlayer;
		basePlayer.Spawn();
		if (Inventory.LoadLoadout("Shields", out var so))
		{
			so.LoadItemsOnTo(basePlayer);
			if (!flag)
			{
				Inventory.EquipItemInSlot(basePlayer, 0);
			}
			else
			{
				Inventory.EquipItemInSlot(basePlayer, -1);
			}
		}
	}
}
