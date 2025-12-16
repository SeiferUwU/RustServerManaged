using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Facepunch;
using Facepunch.Extend;
using Facepunch.Math;
using Network;
using Newtonsoft.Json;
using Rust;
using UnityEngine;
using UnityEngine.Scripting;

namespace ConVar;

[Factory("global")]
public class Admin : ConsoleSystem
{
	private enum ChangeGradeMode
	{
		Upgrade,
		Downgrade
	}

	[Preserve]
	[JsonModel]
	public struct PlayerInfo
	{
		public string SteamID;

		public string OwnerSteamID;

		public string DisplayName;

		public int Ping;

		public string Address;

		public ulong EntityId;

		public int ConnectedSeconds;

		public float ViolationLevel;

		public float CurrentLevel;

		public float UnspentXp;

		public float Health;
	}

	[JsonModel]
	[Preserve]
	public struct PlayerIDInfo
	{
		public string SteamID;

		public string OwnerSteamID;

		public string DisplayName;

		public string Address;

		public ulong EntityId;
	}

	[JsonModel]
	[Preserve]
	public struct ServerInfoOutput
	{
		public string Hostname;

		public int MaxPlayers;

		public int Players;

		public int Queued;

		public int Joining;

		public int ReservedSlots;

		public int EntityCount;

		public string GameTime;

		public int Uptime;

		public string Map;

		public float Framerate;

		public int Memory;

		public int MemoryUsageSystem;

		public int Collections;

		public int NetworkIn;

		public int NetworkOut;

		public bool Restarting;

		public string SaveCreatedTime;

		public int Version;

		public string Protocol;
	}

	[JsonModel]
	[Preserve]
	public struct ServerConvarInfo
	{
		public string FullName;

		public string Value;

		public string Help;
	}

	[Preserve]
	[JsonModel]
	public struct ServerUGCInfo
	{
		public ulong entityId;

		public uint[] crcs;

		public UGCType contentType;

		public uint entityPrefabID;

		public string shortPrefabName;

		public ulong[] playerIds;

		public string contentString;

		public ServerUGCInfo(IUGCBrowserEntity fromEntity)
		{
			entityId = fromEntity.UgcEntity.net.ID.Value;
			crcs = fromEntity.GetContentCRCs;
			contentType = fromEntity.ContentType;
			entityPrefabID = fromEntity.UgcEntity.prefabID;
			shortPrefabName = fromEntity.UgcEntity.ShortPrefabName;
			playerIds = fromEntity.EditingHistory.ToArray();
			contentString = fromEntity.ContentString;
		}
	}

	private struct EntityAssociation
	{
		public BaseEntity TargetEntity;

		public EntityAssociationType AssociationType;
	}

	private enum EntityAssociationType
	{
		Owner,
		Auth,
		LockGuest
	}

	[ReplicatedVar(Help = "Controls whether the in-game admin UI is displayed to admins")]
	public static bool allowAdminUI = true;

	[ServerVar(Help = "Print out currently connected clients")]
	public static void status(Arg arg)
	{
		string text = arg.GetString(0);
		if (text == "--json")
		{
			text = arg.GetString(1);
		}
		bool flag = arg.HasArg("--json");
		string text2 = string.Empty;
		if (!flag && text.Length == 0)
		{
			text2 = text2 + "hostname: " + Server.hostname + "\n";
			text2 = text2 + "version : " + 2615 + " secure (secure mode enabled, connected to Steam3)\n";
			text2 = text2 + "map     : " + Server.level + "\n";
			text2 += $"players : {BasePlayer.activePlayerList.Count()} ({Server.maxplayers} max) ({SingletonComponent<ServerMgr>.Instance.connectionQueue.Queued} queued) ({SingletonComponent<ServerMgr>.Instance.connectionQueue.Joining} joining)\n\n";
		}
		using TextTable textTable = Facepunch.Pool.Get<TextTable>();
		textTable.ShouldPadColumns = !flag;
		textTable.AddColumn("id");
		textTable.AddColumn("name");
		textTable.AddColumn("ping");
		textTable.AddColumn("connected");
		textTable.AddColumn("addr");
		textTable.AddColumn("owner");
		textTable.AddColumn("violation");
		textTable.AddColumn("kicks");
		textTable.AddColumn("entityId");
		foreach (BasePlayer activePlayer in BasePlayer.activePlayerList)
		{
			try
			{
				if (!activePlayer.IsValid())
				{
					continue;
				}
				string userIDString = activePlayer.UserIDString;
				if (activePlayer.net.connection == null)
				{
					textTable.AddRow(userIDString, "NO CONNECTION");
					continue;
				}
				string text3 = activePlayer.net.connection.ownerid.ToString();
				string text4 = activePlayer.displayName.QuoteSafe();
				string text5 = Network.Net.sv.GetAveragePing(activePlayer.net.connection).ToString();
				string text6 = activePlayer.net.connection.ipaddress;
				string text7 = activePlayer.net.ID.Value.ToString();
				string text8 = activePlayer.violationLevel.ToString("0.0");
				string text9 = activePlayer.GetAntiHackKicks().ToString();
				if (!arg.IsAdmin && !arg.IsRcon)
				{
					text6 = "xx.xxx.xx.xxx";
				}
				string text10 = activePlayer.net.connection.GetSecondsConnected() + "s";
				if (text.Length <= 0 || text4.Contains(text, CompareOptions.IgnoreCase) || userIDString.Contains(text) || text3.Contains(text) || text6.Contains(text))
				{
					textTable.AddRow(userIDString, text4, text5, text10, text6, (text3 == userIDString) ? string.Empty : text3, text8, text9, text7);
				}
			}
			catch (Exception ex)
			{
				textTable.AddRow(activePlayer.UserIDString, ex.Message.QuoteSafe());
			}
		}
		if (flag)
		{
			arg.ReplyWith(textTable.ToJson());
		}
		else
		{
			arg.ReplyWith(text2 + textTable.ToString());
		}
	}

	[ServerVar(Help = "Print out stats of currently connected clients")]
	public static void stats(Arg arg)
	{
		bool flag = arg.HasArg("--json");
		using (TextTable textTable = Facepunch.Pool.Get<TextTable>())
		{
			textTable.ShouldPadColumns = !flag;
			textTable.AddColumn("id");
			textTable.AddColumn("name");
			textTable.AddColumn("time");
			textTable.AddColumn("kills");
			textTable.AddColumn("deaths");
			textTable.AddColumn("suicides");
			textTable.AddColumn("player");
			textTable.AddColumn("building");
			textTable.AddColumn("entity");
			ulong uInt = arg.GetUInt64(0, 0uL);
			if (uInt == 0L)
			{
				string text = arg.GetString(0);
				foreach (BasePlayer activePlayer in BasePlayer.activePlayerList)
				{
					try
					{
						if (activePlayer.IsValid())
						{
							string text2 = activePlayer.displayName.QuoteSafe();
							if (text.Length <= 0 || text2.Contains(text, CompareOptions.IgnoreCase))
							{
								addRow(activePlayer.userID, text2, textTable);
							}
						}
					}
					catch (Exception ex)
					{
						textTable.AddRow(activePlayer.UserIDString, ex.Message.QuoteSafe());
					}
				}
			}
			else
			{
				string name = "N/A";
				BasePlayer basePlayer = BasePlayer.FindByID(uInt);
				if ((bool)basePlayer)
				{
					name = basePlayer.displayName.QuoteSafe();
				}
				addRow(uInt, name, textTable);
			}
			arg.ReplyWith(flag ? textTable.ToJson() : textTable.ToString());
		}
		static void addRow(ulong id, string text13, TextTable table)
		{
			ServerStatistics.Storage storage = ServerStatistics.Get(id);
			string text3 = TimeSpanEx.ToShortString(TimeSpan.FromSeconds(storage.Get("time")));
			string text4 = storage.Get("kill_player").ToString();
			string text5 = (storage.Get("deaths") - storage.Get("death_suicide")).ToString();
			string text6 = storage.Get("death_suicide").ToString();
			string text7 = storage.Get("hit_player_direct_los").ToString();
			string text8 = storage.Get("hit_player_indirect_los").ToString();
			string text9 = storage.Get("hit_building_direct_los").ToString();
			string text10 = storage.Get("hit_building_indirect_los").ToString();
			string text11 = storage.Get("hit_entity_direct_los").ToString();
			string text12 = storage.Get("hit_entity_indirect_los").ToString();
			table.AddRow(id.ToString(), text13, text3, text4, text5, text6, text7 + " / " + text8, text9 + " / " + text10, text11 + " / " + text12);
		}
	}

	[ServerVar(Help = "upgrade_radius 'grade' 'radius'")]
	public static void upgrade_radius(Arg arg)
	{
		if (!arg.HasArgs(2))
		{
			arg.ReplyWith("Format is 'upgrade_radius {grade} {radius}'");
		}
		else
		{
			SkinRadiusInternal(arg, changeAnyGrade: true);
		}
	}

	[ServerVar(Help = "<grade>")]
	public static void upgrade_looking(Arg arg)
	{
		if (!arg.HasArgs())
		{
			arg.ReplyWith("Format is 'upgrade_looking {grade}'");
		}
		else
		{
			SkinRaycastInternal(arg, changeAnyGrade: true);
		}
	}

	[ServerVar(Help = "skin_radius 'skin' 'radius'")]
	public static void skin_radius(Arg arg)
	{
		if (!arg.HasArgs(2))
		{
			arg.ReplyWith("Format is 'skin_radius {skin} {radius}'");
		}
		else
		{
			SkinRadiusInternal(arg, changeAnyGrade: false);
		}
	}

	[ServerVar(Help = "<skin>")]
	public static void skin_looking(Arg arg)
	{
		if (!arg.HasArgs())
		{
			arg.ReplyWith("Format is 'skin_looking <skin>'");
		}
		else
		{
			SkinRaycastInternal(arg, changeAnyGrade: false);
		}
	}

	[ServerVar(Help = "<name/id> <radius> | Use print_wallpaper_skins for a list | 0 -> default, -1 -> random")]
	public static void add_wallpaper_radius(Arg arg)
	{
		if (!arg.HasArgs(2))
		{
			arg.ReplyWith("Format is 'add_wallpaper_radius {skin} {radius}' | Use print_wallpaper_skins for a list | 0 -> default, -1 -> random");
		}
		else
		{
			wallpaper_radius_internal(arg, addIfMissing: true);
		}
	}

	[ServerVar(Help = "<name/id> <radius> | Use print_wallpaper_skins for a list | 0 -> default, -1 -> random")]
	public static void change_wallpaper_radius(Arg arg)
	{
		if (!arg.HasArgs(2))
		{
			arg.ReplyWith("Format is 'change_wallpaper_radius {skin} {radius}' | Use print_wallpaper_skins for a list | 0 -> default, -1 -> random");
		}
		else
		{
			wallpaper_radius_internal(arg, addIfMissing: false);
		}
	}

	[ServerVar(Help = "clear_wallpaper_radius <radius>")]
	public static void clear_wallpaper_radius(Arg arg)
	{
		if (!arg.HasArgs())
		{
			arg.ReplyWith("Format is 'clear_wallpaper_radius {radius}'");
			return;
		}
		RunInRadius(arg.GetFloat(0), ArgEx.Player(arg), delegate(BuildingBlock block)
		{
			if (block.HasWallpaper())
			{
				block.RemoveWallpaper(0);
				block.RemoveWallpaper(1);
			}
		});
	}

	public static BuildingGrade FindBuildingSkin(string name, out string error)
	{
		BuildingGrade buildingGrade = null;
		error = null;
		IEnumerable<BuildingGrade> source = from x in PrefabAttribute.server.FindAll<ConstructionGrade>(2194854973u)
			select x.gradeBase;
		switch (name)
		{
		case "twig":
		case "0":
			buildingGrade = source.FirstOrDefault((BuildingGrade x) => x.name == "twigs");
			break;
		case "wood":
		case "1":
			buildingGrade = source.FirstOrDefault((BuildingGrade x) => x.name == "wood");
			break;
		case "stone":
		case "2":
			buildingGrade = source.FirstOrDefault((BuildingGrade x) => x.name == "stone");
			break;
		case "metal":
		case "sheetmetal":
		case "3":
			buildingGrade = source.FirstOrDefault((BuildingGrade x) => x.name == "metal");
			break;
		case "hqm":
		case "armored":
		case "armoured":
		case "4":
			buildingGrade = source.FirstOrDefault((BuildingGrade x) => x.name == "toptier");
			break;
		case "adobe":
			buildingGrade = source.FirstOrDefault((BuildingGrade x) => x.name == "adobe");
			break;
		case "shipping":
		case "shippingcontainer":
		case "container":
			buildingGrade = source.FirstOrDefault((BuildingGrade x) => x.name == "shipping_container");
			break;
		case "brutal":
		case "brutalist":
			buildingGrade = source.FirstOrDefault((BuildingGrade x) => x.name == "brutalist");
			break;
		case "brick":
			buildingGrade = source.FirstOrDefault((BuildingGrade x) => x.name == "brick");
			break;
		case "jungle":
		case "jungleruin":
			buildingGrade = source.FirstOrDefault((BuildingGrade x) => x.name == "jungle");
			break;
		case "frontier":
		case "legacy":
			buildingGrade = source.FirstOrDefault((BuildingGrade x) => x.name == "frontier");
			break;
		case "gingerbread":
			buildingGrade = source.FirstOrDefault((BuildingGrade x) => x.name == "gingerbread");
			break;
		case "space":
		case "spacestation":
			buildingGrade = source.FirstOrDefault((BuildingGrade x) => x.name == "space_station");
			break;
		default:
			error = "Valid skins are:\ntwig\nwood | frontier | gingerbread\nstone | adobe | brick | brutalist | jungle\nmetal | shipping\nhqm | space";
			return null;
		}
		if (buildingGrade == null)
		{
			error = "Unable to find skin object for '" + name + "'";
		}
		return buildingGrade;
	}

	private static IEnumerable<BuildingBlock> SearchRadius(Vector3 position, float radius)
	{
		List<BuildingBlock> list = new List<BuildingBlock>();
		global::Vis.Entities(position, radius, list, 2097152);
		return list;
	}

	private static IEnumerable<BuildingBlock> SearchLookingAt(Vector3 position, Vector3 direction, float maxDistance)
	{
		BuildingBlock buildingBlock = GamePhysics.TraceRealmEntity(GamePhysics.Realm.Server, new Ray(position, direction), 0f, maxDistance, 10485760, QueryTriggerInteraction.Ignore) as BuildingBlock;
		if (buildingBlock == null)
		{
			return Array.Empty<BuildingBlock>();
		}
		return buildingBlock.GetBuilding()?.buildingBlocks;
	}

	private static void SkinRadiusInternal(Arg arg, bool changeAnyGrade)
	{
		IEnumerable<BuildingBlock> blocks = SearchRadius(ArgEx.Player(arg).transform.position, arg.GetFloat(1));
		ApplySkinInternal(arg, changeAnyGrade, blocks);
	}

	private static void SkinRaycastInternal(Arg arg, bool changeAnyGrade)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		IEnumerable<BuildingBlock> blocks = SearchLookingAt(basePlayer.eyes.position, basePlayer.eyes.BodyForward(), 100f);
		ApplySkinInternal(arg, changeAnyGrade, blocks);
	}

	private static void ApplySkinInternal(Arg arg, bool changeAnyGrade, IEnumerable<BuildingBlock> blocks)
	{
		if (ArgEx.Player(arg) == null)
		{
			arg.ReplyWith("This must be called from the client");
			return;
		}
		arg.GetFloat(1);
		string text = arg.GetString(0);
		string error;
		BuildingGrade buildingGrade = FindBuildingSkin(text, out error);
		if (buildingGrade == null)
		{
			arg.ReplyWith(error);
			return;
		}
		if (!buildingGrade.enabledInStandalone)
		{
			arg.ReplyWith("Skin " + text + " is not enabled in standalone yet");
			return;
		}
		if (blocks == null || blocks.Count() == 0)
		{
			arg.ReplyWith("No building blocks found");
			return;
		}
		foreach (BuildingBlock block in blocks)
		{
			if (block.grade == buildingGrade.type || changeAnyGrade)
			{
				block.ChangeGradeAndSkin(buildingGrade.type, buildingGrade.skin);
			}
		}
	}

	private static void wallpaper_radius_internal(Arg arg, bool addIfMissing)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		if (basePlayer == null)
		{
			arg.ReplyWith("This must be called from the client");
			return;
		}
		float radius = arg.GetFloat(1);
		string text = arg.GetString(0);
		int skinIdParsed = -1;
		if (!int.TryParse(text, out skinIdParsed))
		{
			skinIdParsed = -1;
		}
		bool flag = false;
		string foundSkinName = "";
		foreach (ItemSkinDirectory.Skin item in WallpaperSettings.WallpaperItemDef.skins.Concat(WallpaperSettings.FlooringItemDef.skins).Concat(WallpaperSettings.CeilingItemDef.skins))
		{
			if (skinIdParsed != -1 && item.id == skinIdParsed)
			{
				flag = true;
				foundSkinName = item.invItem.displayName.english.Trim();
				break;
			}
			if (skinIdParsed == -1 && (item.invItem.displayName.english.Contains(text, StringComparison.InvariantCultureIgnoreCase) || item.invItem.name.Contains(text, StringComparison.InvariantCultureIgnoreCase)))
			{
				flag = true;
				foundSkinName = item.invItem.displayName.english.Trim();
				skinIdParsed = item.id;
				break;
			}
		}
		if (skinIdParsed == 0)
		{
			flag = true;
		}
		if (!flag && skinIdParsed != -1)
		{
			arg.ReplyWith("Invalid skin");
			return;
		}
		RunInRadius(radius, basePlayer, delegate(BuildingBlock block)
		{
			bool flag2 = block.HasWallpaper();
			bool flag3 = flag2;
			if (addIfMissing && !flag2)
			{
				flag3 = WallpaperPlanner.Settings.CanUseWallpaper(block);
			}
			if (block.HasWallpaper() || flag3)
			{
				if (skinIdParsed == -1)
				{
					arg.ReplyWith("Applying random wallpaper");
					for (int i = 0; i < 2; i++)
					{
						ItemDefinition wallpaperItem = WallpaperPlanner.Settings.GetWallpaperItem(block, i);
						if (wallpaperItem != null)
						{
							int id = ArrayEx.GetRandom(wallpaperItem.skins).id;
							block.SetWallpaper((ulong)id, i);
						}
					}
				}
				else if (skinIdParsed == 0)
				{
					arg.ReplyWith("Applying default wallpaper");
					block.SetWallpaper(0uL);
					block.SetWallpaper(0uL, 1);
				}
				else
				{
					arg.ReplyWith("Applying '" + foundSkinName + "' wallpaper to compatible blocks");
					for (int j = 0; j < 2; j++)
					{
						ItemDefinition wallpaperItem2 = WallpaperPlanner.Settings.GetWallpaperItem(block, j);
						if (wallpaperItem2 != null && wallpaperItem2.skins.Any((ItemSkinDirectory.Skin x) => x.id == skinIdParsed))
						{
							block.SetWallpaper((ulong)skinIdParsed, j);
						}
					}
				}
				block.CheckWallpaper();
			}
		});
	}

	[ServerVar(Help = "Lists all wallpaper skins")]
	public static void print_wallpaper_skins(Arg arg)
	{
		using TextTable textTable = Facepunch.Pool.Get<TextTable>();
		textTable.AddColumns("Id", "Type", "Name");
		ItemSkinDirectory.Skin[] skins = WallpaperSettings.WallpaperItemDef.skins;
		for (int i = 0; i < skins.Length; i++)
		{
			ItemSkinDirectory.Skin skin = skins[i];
			string[] array = new string[3];
			int id = skin.id;
			array[0] = id.ToString();
			array[1] = "Wall";
			array[2] = skin.invItem.displayName.english.Trim();
			textTable.AddRow(array);
		}
		skins = WallpaperSettings.FlooringItemDef.skins;
		for (int i = 0; i < skins.Length; i++)
		{
			ItemSkinDirectory.Skin skin2 = skins[i];
			string[] array2 = new string[3];
			int id = skin2.id;
			array2[0] = id.ToString();
			array2[1] = "Floor";
			array2[2] = skin2.invItem.displayName.english.Trim();
			textTable.AddRow(array2);
		}
		skins = WallpaperSettings.CeilingItemDef.skins;
		for (int i = 0; i < skins.Length; i++)
		{
			ItemSkinDirectory.Skin skin3 = skins[i];
			string[] array3 = new string[3];
			int id = skin3.id;
			array3[0] = id.ToString();
			array3[1] = "Ceiling";
			array3[2] = skin3.invItem.displayName.english.Trim();
			textTable.AddRow(array3);
		}
		arg.ReplyWith(textTable.ToString());
	}

	[ServerVar(Help = "Kills all bee swarms")]
	public static void killbees(Arg arg)
	{
		int num = 0;
		BeeSwarmMaster[] array = BaseEntity.Util.FindAll<BeeSwarmMaster>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].AdminKill();
			num++;
		}
		BeeSwarmAI[] array2 = BaseEntity.Util.FindAll<BeeSwarmAI>();
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].AdminKill();
			num++;
		}
		arg.ReplyWith($"Killed {num} bee swarms");
	}

	[ServerVar]
	public static void killplayer(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.GetPlayerOrSleeper(arg, 0);
		if (!basePlayer)
		{
			basePlayer = BasePlayer.FindBotClosestMatch(arg.GetString(0));
		}
		if (!basePlayer)
		{
			arg.ReplyWith("Player not found");
		}
		else
		{
			basePlayer.Hurt(1000f, DamageType.Suicide, basePlayer, useProtection: false);
		}
	}

	[ServerVar]
	public static void killallplayers(Arg arg)
	{
		BasePlayer[] array = BaseEntity.Util.FindAll<BasePlayer>();
		int num = 0;
		BasePlayer[] array2 = array;
		foreach (BasePlayer basePlayer in array2)
		{
			if (!basePlayer.IsNpc)
			{
				basePlayer.Hurt(1000f, DamageType.Suicide, basePlayer, useProtection: false);
				num++;
			}
		}
		arg.ReplyWith($"Killed {num} players");
	}

	[ServerVar]
	public static void injureplayer(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.GetPlayerOrSleeper(arg, 0);
		if (!basePlayer)
		{
			basePlayer = BasePlayer.FindBotClosestMatch(arg.GetString(0));
		}
		if (!basePlayer)
		{
			arg.ReplyWith("Player not found");
		}
		else
		{
			Global.InjurePlayer(basePlayer);
		}
	}

	[ServerVar]
	public static void recoverplayer(Arg arg)
	{
		BasePlayer basePlayer = ArgEx.GetPlayerOrSleeper(arg, 0);
		if (!basePlayer)
		{
			basePlayer = BasePlayer.FindBotClosestMatch(arg.GetString(0));
		}
		if (!basePlayer)
		{
			arg.ReplyWith("Player not found");
		}
		else
		{
			Global.RecoverPlayer(basePlayer);
		}
	}

	[ServerVar]
	public static void kick(Arg arg)
	{
		BasePlayer player = ArgEx.GetPlayer(arg, 0);
		if (!player || player.net == null || player.net.connection == null)
		{
			arg.ReplyWith("Player not found");
			return;
		}
		string text = arg.GetString(1, "no reason given");
		arg.ReplyWith("Kicked: " + player.displayName);
		Chat.Broadcast("Kicking " + player.displayName + " (" + text + ")", "SERVER", "#eee", 0uL);
		player.Kick("Kicked: " + arg.GetString(1, "No Reason Given"), reserveSlot: false);
	}

	[ServerVar]
	public static void skick(Arg arg)
	{
		BasePlayer player = ArgEx.GetPlayer(arg, 0);
		if (!player || player.net == null || player.net.connection == null)
		{
			arg.ReplyWith("Player not found");
			return;
		}
		string text = arg.GetString(1, "no reason given");
		arg.ReplyWith("Kicked: " + player.displayName);
		Chat.Record(new Chat.ChatEntry
		{
			Channel = Chat.ChatChannel.Server,
			Message = "(SILENT) Kicking " + player.displayName + " (" + text + ")",
			UserId = "0",
			Username = "SERVER",
			Color = "#eee",
			Time = Epoch.Current
		});
		player.Kick("Kicked: " + arg.GetString(1, "No Reason Given"), reserveSlot: false);
	}

	[ServerVar]
	public static void kickall(Arg arg)
	{
		BasePlayer[] array = BasePlayer.activePlayerList.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Kick("Kicked: " + arg.GetString(0, "No Reason Given"));
		}
	}

	[ServerVar(Help = "ban <player> <reason> [optional duration]")]
	public static void ban(Arg arg)
	{
		BasePlayer player = ArgEx.GetPlayer(arg, 0);
		if (!player || player.net == null || player.net.connection == null)
		{
			arg.ReplyWith("Player not found");
			return;
		}
		ServerUsers.User user = ServerUsers.Get(player.userID);
		if (user != null && user.group == ServerUsers.UserGroup.Banned)
		{
			arg.ReplyWith($"User {player.userID.Get()} is already banned");
			return;
		}
		string text = arg.GetString(1, "No Reason Given");
		if (TryGetBanExpiry(arg, 2, out var expiry, out var durationSuffix))
		{
			ServerUsers.Set(player.userID, ServerUsers.UserGroup.Banned, player.displayName, text, expiry);
			string text2 = "";
			if (player.IsConnected && player.net.connection.ownerid != 0L && player.net.connection.ownerid != player.net.connection.userid)
			{
				text2 += $" and also banned ownerid {player.net.connection.ownerid}";
				ServerUsers.Set(player.net.connection.ownerid, ServerUsers.UserGroup.Banned, player.displayName, arg.GetString(1, $"Family share owner of {player.net.connection.userid}"), -1L);
			}
			ServerUsers.Save();
			arg.ReplyWith($"Kickbanned User{durationSuffix}: {player.userID.Get()} - {player.displayName}{text2}");
			Chat.Broadcast("Kickbanning " + player.displayName + durationSuffix + " (" + text + ")", "SERVER", "#eee", 0uL);
			Network.Net.sv.Kick(player.net.connection, "Banned" + durationSuffix + ": " + text);
		}
	}

	[ServerVar]
	public static void moderatorid(Arg arg)
	{
		ulong uInt = arg.GetUInt64(0, 0uL);
		string text = arg.GetString(1, "unnamed");
		string notes = arg.GetString(2, "no reason");
		if (uInt < 70000000000000000L)
		{
			arg.ReplyWith("This doesn't appear to be a 64bit steamid: " + uInt);
			return;
		}
		ServerUsers.User user = ServerUsers.Get(uInt);
		if (user != null && user.group == ServerUsers.UserGroup.Moderator)
		{
			arg.ReplyWith("User " + uInt + " is already a Moderator");
			return;
		}
		ServerUsers.Set(uInt, ServerUsers.UserGroup.Moderator, text, notes, -1L);
		ServerUsers.Save();
		BasePlayer basePlayer = BasePlayer.FindByID(uInt);
		if (basePlayer != null)
		{
			basePlayer.SetPlayerFlag(BasePlayer.PlayerFlags.IsAdmin, b: true);
			basePlayer.SendNetworkUpdate();
		}
		arg.ReplyWith("Added moderator " + text + ", steamid " + uInt);
	}

	[ServerVar]
	public static void ownerid(Arg arg)
	{
		ulong uInt = arg.GetUInt64(0, 0uL);
		string text = arg.GetString(1, "unnamed");
		string notes = arg.GetString(2, "no reason");
		if (uInt < 70000000000000000L)
		{
			arg.ReplyWith("This doesn't appear to be a 64bit steamid: " + uInt);
			return;
		}
		if (arg.Connection != null && arg.Connection.authLevel < 2)
		{
			arg.ReplyWith("Moderators cannot run ownerid");
			return;
		}
		ServerUsers.User user = ServerUsers.Get(uInt);
		if (user != null && user.group == ServerUsers.UserGroup.Owner)
		{
			arg.ReplyWith("User " + uInt + " is already an Owner");
			return;
		}
		ServerUsers.Set(uInt, ServerUsers.UserGroup.Owner, text, notes, -1L);
		ServerUsers.Save();
		BasePlayer basePlayer = BasePlayer.FindByID(uInt);
		if (basePlayer != null)
		{
			basePlayer.SetPlayerFlag(BasePlayer.PlayerFlags.IsAdmin, b: true);
			basePlayer.SendNetworkUpdate();
		}
		arg.ReplyWith("Added owner " + text + ", steamid " + uInt);
	}

	[ServerVar]
	public static void removemoderator(Arg arg)
	{
		ulong uInt = arg.GetUInt64(0, 0uL);
		if (uInt < 70000000000000000L)
		{
			arg.ReplyWith("This doesn't appear to be a 64bit steamid: " + uInt);
			return;
		}
		ServerUsers.User user = ServerUsers.Get(uInt);
		if (user == null || user.group != ServerUsers.UserGroup.Moderator)
		{
			arg.ReplyWith("User " + uInt + " isn't a moderator");
			return;
		}
		ServerUsers.Remove(uInt);
		ServerUsers.Save();
		BasePlayer basePlayer = BasePlayer.FindByID(uInt);
		if (basePlayer != null)
		{
			basePlayer.SetPlayerFlag(BasePlayer.PlayerFlags.IsAdmin, b: false);
			basePlayer.SendNetworkUpdate();
		}
		arg.ReplyWith("Removed Moderator: " + uInt);
	}

	[ServerVar]
	public static void removeowner(Arg arg)
	{
		ulong uInt = arg.GetUInt64(0, 0uL);
		if (uInt < 70000000000000000L)
		{
			arg.ReplyWith("This doesn't appear to be a 64bit steamid: " + uInt);
			return;
		}
		ServerUsers.User user = ServerUsers.Get(uInt);
		if (user == null || user.group != ServerUsers.UserGroup.Owner)
		{
			arg.ReplyWith("User " + uInt + " isn't an owner");
			return;
		}
		ServerUsers.Remove(uInt);
		ServerUsers.Save();
		BasePlayer basePlayer = BasePlayer.FindByID(uInt);
		if (basePlayer != null)
		{
			basePlayer.SetPlayerFlag(BasePlayer.PlayerFlags.IsAdmin, b: false);
			basePlayer.SendNetworkUpdate();
		}
		arg.ReplyWith("Removed Owner: " + uInt);
	}

	[ServerVar(Help = "banid <steamid> <username> <reason> [optional duration]")]
	public static void banid(Arg arg)
	{
		ulong uInt = arg.GetUInt64(0, 0uL);
		string text = arg.GetString(1, "unnamed");
		string text2 = arg.GetString(2, "no reason");
		if (uInt < 70000000000000000L)
		{
			arg.ReplyWith("This doesn't appear to be a 64bit steamid: " + uInt);
			return;
		}
		ServerUsers.User user = ServerUsers.Get(uInt);
		if (user != null && user.group == ServerUsers.UserGroup.Banned)
		{
			arg.ReplyWith("User " + uInt + " is already banned");
		}
		else
		{
			if (!TryGetBanExpiry(arg, 3, out var expiry, out var durationSuffix))
			{
				return;
			}
			string text3 = "";
			BasePlayer basePlayer = BasePlayer.FindByID(uInt);
			if (basePlayer != null && basePlayer.IsConnected)
			{
				text = basePlayer.displayName;
				if (basePlayer.IsConnected && basePlayer.net.connection.ownerid != 0L && basePlayer.net.connection.ownerid != basePlayer.net.connection.userid)
				{
					text3 += $" and also banned ownerid {basePlayer.net.connection.ownerid}";
					ServerUsers.Set(basePlayer.net.connection.ownerid, ServerUsers.UserGroup.Banned, basePlayer.displayName, arg.GetString(1, $"Family share owner of {basePlayer.net.connection.userid}"), expiry);
				}
				Chat.Broadcast("Kickbanning " + basePlayer.displayName + durationSuffix + " (" + text2 + ")", "SERVER", "#eee", 0uL);
				Network.Net.sv.Kick(basePlayer.net.connection, "Banned" + durationSuffix + ": " + text2);
			}
			ServerUsers.Set(uInt, ServerUsers.UserGroup.Banned, text, text2, expiry);
			arg.ReplyWith($"Banned User{durationSuffix}: {uInt} - \"{text}\" for \"{text2}\"{text3}");
		}
	}

	private static bool TryGetBanExpiry(Arg arg, int n, out long expiry, out string durationSuffix)
	{
		expiry = arg.GetTimestamp(n, -1L);
		durationSuffix = null;
		int current = Epoch.Current;
		if (expiry > 0 && expiry <= current)
		{
			arg.ReplyWith("Expiry time is in the past");
			return false;
		}
		durationSuffix = ((expiry > 0) ? (" for " + (expiry - current).FormatSecondsLong()) : "");
		return true;
	}

	[ServerVar]
	public static void unban(Arg arg)
	{
		ulong uInt = arg.GetUInt64(0, 0uL);
		if (uInt < 70000000000000000L)
		{
			arg.ReplyWith($"This doesn't appear to be a 64bit steamid: {uInt}");
			return;
		}
		ServerUsers.User user = ServerUsers.Get(uInt);
		if (user == null || user.group != ServerUsers.UserGroup.Banned)
		{
			arg.ReplyWith($"User {uInt} isn't banned");
			return;
		}
		ServerUsers.Remove(uInt);
		arg.ReplyWith("Unbanned User: " + uInt);
	}

	[ServerVar]
	public static void skipqueue(Arg arg)
	{
		ulong uInt = arg.GetUInt64(0, 0uL);
		if (uInt < 70000000000000000L)
		{
			arg.ReplyWith("This doesn't appear to be a 64bit steamid: " + uInt);
		}
		else
		{
			SingletonComponent<ServerMgr>.Instance.connectionQueue.SkipQueue(uInt);
		}
	}

	[ServerVar(Help = "Adds skip queue permissions to a SteamID")]
	public static void skipqueueid(Arg arg)
	{
		ulong uInt = arg.GetUInt64(0, 0uL);
		string text = arg.GetString(1, "unnamed");
		string notes = arg.GetString(2, "no reason");
		if (uInt < 70000000000000000L)
		{
			arg.ReplyWith("This doesn't appear to be a 64bit steamid: " + uInt);
			return;
		}
		ServerUsers.User user = ServerUsers.Get(uInt);
		if (user != null && (user.group == ServerUsers.UserGroup.Owner || user.group == ServerUsers.UserGroup.Moderator || user.group == ServerUsers.UserGroup.SkipQueue))
		{
			arg.ReplyWith($"User {uInt} will already skip the queue ({user.group})");
			return;
		}
		if (user != null && user.group == ServerUsers.UserGroup.Banned)
		{
			arg.ReplyWith($"User {uInt} is banned");
			return;
		}
		ServerUsers.Set(uInt, ServerUsers.UserGroup.SkipQueue, text, notes, -1L);
		arg.ReplyWith($"Added skip queue permission for {text} ({uInt})");
	}

	[ServerVar(Help = "Removes skip queue permission from a SteamID")]
	public static void removeskipqueue(Arg arg)
	{
		ulong uInt = arg.GetUInt64(0, 0uL);
		if (uInt < 70000000000000000L)
		{
			arg.ReplyWith("This doesn't appear to be a 64bit steamid: " + uInt);
			return;
		}
		ServerUsers.User user = ServerUsers.Get(uInt);
		if (user != null && (user.group == ServerUsers.UserGroup.Owner || user.group == ServerUsers.UserGroup.Moderator))
		{
			arg.ReplyWith($"User is a {user.group}, cannot remove skip queue permission with this command");
			return;
		}
		if (user == null || user.group != ServerUsers.UserGroup.SkipQueue)
		{
			arg.ReplyWith("User does not have skip queue permission");
			return;
		}
		ServerUsers.Remove(uInt);
		arg.ReplyWith("Removed skip queue permission: " + uInt);
	}

	[ServerVar(Help = "Print out currently connected clients etc")]
	public static void players(Arg arg)
	{
		bool flag = arg.HasArg("--json");
		using TextTable textTable = Facepunch.Pool.Get<TextTable>();
		textTable.ShouldPadColumns = !flag;
		textTable.ResizeColumns(5);
		textTable.AddColumn("id");
		textTable.AddColumn("name");
		textTable.AddColumn("ping");
		textTable.AddColumn("updt");
		textTable.AddColumn("dist");
		textTable.AddColumn("enId");
		textTable.ResizeRows(BasePlayer.activePlayerList.Count);
		foreach (BasePlayer activePlayer in BasePlayer.activePlayerList)
		{
			string userIDString = activePlayer.UserIDString;
			textTable.AddValue(userIDString);
			string text = activePlayer.displayName;
			if (text.Length >= 14)
			{
				text = text.Substring(0, 14) + "..";
			}
			textTable.AddValue(text);
			int averagePing = Network.Net.sv.GetAveragePing(activePlayer.net.connection);
			textTable.AddValue(averagePing);
			int queuedUpdateCount = activePlayer.GetQueuedUpdateCount(BasePlayer.NetworkQueue.Update);
			textTable.AddValue(queuedUpdateCount);
			int queuedUpdateCount2 = activePlayer.GetQueuedUpdateCount(BasePlayer.NetworkQueue.UpdateDistance);
			textTable.AddValue(queuedUpdateCount2);
			ulong value = activePlayer.net.ID.Value;
			textTable.AddValue(value);
		}
		arg.ReplyWith(flag ? textTable.ToJson(stringify: false) : textTable.ToString());
	}

	[ServerVar(Help = "Sends a message in chat")]
	public static void say(Arg arg)
	{
		Chat.Broadcast(arg.FullString, "SERVER", "#eee", 0uL);
	}

	[ServerVar(Help = "Show user info for players on server.")]
	public static void users(Arg arg)
	{
		string text = "<slot:userid:\"name\">\n";
		int num = 0;
		foreach (BasePlayer activePlayer in BasePlayer.activePlayerList)
		{
			text = text + activePlayer.userID.Get() + ":\"" + activePlayer.displayName + "\"\n";
			num++;
		}
		text = text + num + "users\n";
		arg.ReplyWith(text);
	}

	[ServerVar(Help = "Show user info for players on server.")]
	public static void sleepingusers(Arg arg)
	{
		string text = "<slot:userid:\"name\">\n";
		int num = 0;
		foreach (BasePlayer sleepingPlayer in BasePlayer.sleepingPlayerList)
		{
			text += $"{sleepingPlayer.userID.Get()}:{sleepingPlayer.displayName}\n";
			num++;
		}
		text += $"{num} sleeping users\n";
		arg.ReplyWith(text);
	}

	[ServerVar(Help = "Show user info for sleeping players on server in range of the player.")]
	public static void sleepingusersinrange(Arg arg)
	{
		BasePlayer fromPlayer = ArgEx.Player(arg);
		if (fromPlayer == null)
		{
			return;
		}
		if (fromPlayer.IsSpectating() && fromPlayer.SpectatingTarget != null)
		{
			fromPlayer = fromPlayer.SpectatingTarget;
		}
		float range = arg.GetFloat(0);
		string text = "<slot:userid:\"name\">\n";
		int num = 0;
		List<BasePlayer> obj = Facepunch.Pool.Get<List<BasePlayer>>();
		foreach (BasePlayer sleepingPlayer in BasePlayer.sleepingPlayerList)
		{
			obj.Add(sleepingPlayer);
		}
		obj.RemoveAll((BasePlayer p) => p.Distance2D(fromPlayer) > range);
		obj.Sort((BasePlayer player, BasePlayer basePlayer) => (!(player.Distance2D(fromPlayer) < basePlayer.Distance2D(fromPlayer))) ? 1 : (-1));
		foreach (BasePlayer item in obj)
		{
			text += $"{item.userID.Get()}:{item.displayName}:{item.Distance2D(fromPlayer)}m\n";
			num++;
		}
		Facepunch.Pool.FreeUnmanaged(ref obj);
		text += $"{num} sleeping users within {range}m\n";
		arg.ReplyWith(text);
	}

	[ServerVar(Help = "Show user info for players on server in range of the player.")]
	public static void usersinrange(Arg arg)
	{
		BasePlayer fromPlayer = ArgEx.Player(arg);
		if (fromPlayer == null)
		{
			return;
		}
		if (fromPlayer.IsSpectating() && fromPlayer.SpectatingTarget != null)
		{
			fromPlayer = fromPlayer.SpectatingTarget;
		}
		float range = arg.GetFloat(0);
		string text = "<slot:userid:\"name\">\n";
		int num = 0;
		List<BasePlayer> obj = Facepunch.Pool.Get<List<BasePlayer>>();
		foreach (BasePlayer activePlayer in BasePlayer.activePlayerList)
		{
			obj.Add(activePlayer);
		}
		obj.RemoveAll((BasePlayer p) => p.Distance2D(fromPlayer) > range);
		obj.Sort((BasePlayer player, BasePlayer basePlayer) => (!(player.Distance2D(fromPlayer) < basePlayer.Distance2D(fromPlayer))) ? 1 : (-1));
		foreach (BasePlayer item in obj)
		{
			text += $"{item.userID.Get()}:{item.displayName}:{item.Distance2D(fromPlayer)}m\n";
			num++;
		}
		Facepunch.Pool.FreeUnmanaged(ref obj);
		text += $"{num} users within {range}m\n";
		arg.ReplyWith(text);
	}

	[ServerVar(Help = "Show user info for players on server in range of the supplied player (eg. Jim 50)")]
	public static void usersinrangeofplayer(Arg arg)
	{
		BasePlayer targetPlayer = ArgEx.GetPlayerOrSleeper(arg, 0);
		if (targetPlayer == null)
		{
			return;
		}
		float range = arg.GetFloat(1);
		string text = "<slot:userid:\"name\">\n";
		int num = 0;
		List<BasePlayer> obj = Facepunch.Pool.Get<List<BasePlayer>>();
		foreach (BasePlayer activePlayer in BasePlayer.activePlayerList)
		{
			obj.Add(activePlayer);
		}
		obj.RemoveAll((BasePlayer p) => p.Distance2D(targetPlayer) > range);
		obj.Sort((BasePlayer player, BasePlayer basePlayer) => (!(player.Distance2D(targetPlayer) < basePlayer.Distance2D(targetPlayer))) ? 1 : (-1));
		foreach (BasePlayer item in obj)
		{
			text += $"{item.userID.Get()}:{item.displayName}:{item.Distance2D(targetPlayer)}m\n";
			num++;
		}
		Facepunch.Pool.FreeUnmanaged(ref obj);
		text += $"{num} users within {range}m of {targetPlayer.displayName}\n";
		arg.ReplyWith(text);
	}

	[ServerVar(Help = "List of banned users (sourceds compat)")]
	public static void banlist(Arg arg)
	{
		arg.ReplyWith(ServerUsers.BanListString());
	}

	[ServerVar(Help = "List of banned users - shows reasons and usernames")]
	public static void banlistex(Arg arg)
	{
		arg.ReplyWith(ServerUsers.BanListStringEx());
	}

	[ServerVar(Help = "List of banned users, by ID (sourceds compat)")]
	public static void listid(Arg arg)
	{
		arg.ReplyWith(ServerUsers.BanListString(bHeader: true));
	}

	[ServerVar]
	public static void mute(Arg arg)
	{
		BasePlayer playerOrSleeper = ArgEx.GetPlayerOrSleeper(arg, 0);
		if (!playerOrSleeper || playerOrSleeper.net == null || playerOrSleeper.net.connection == null)
		{
			arg.ReplyWith("Player not found");
			return;
		}
		long timestamp = arg.GetTimestamp(1, 0L);
		if (timestamp > 0)
		{
			playerOrSleeper.State.chatMuteExpiryTimestamp = timestamp;
			string text = (timestamp - DateTimeOffset.UtcNow.ToUnixTimeSeconds()).FormatSecondsLong();
			playerOrSleeper.ChatMessage("You have been muted for " + text);
		}
		else
		{
			playerOrSleeper.ChatMessage("You have been permanently muted");
		}
		playerOrSleeper.State.chatMuted = true;
		playerOrSleeper.SetPlayerFlag(BasePlayer.PlayerFlags.ChatMute, b: true);
	}

	[ServerVar]
	public static void unmute(Arg arg)
	{
		BasePlayer playerOrSleeper = ArgEx.GetPlayerOrSleeper(arg, 0);
		if (!playerOrSleeper || playerOrSleeper.net == null || playerOrSleeper.net.connection == null)
		{
			arg.ReplyWith("Player not found");
			return;
		}
		playerOrSleeper.State.chatMuted = false;
		playerOrSleeper.State.chatMuteExpiryTimestamp = 0.0;
		playerOrSleeper.SetPlayerFlag(BasePlayer.PlayerFlags.ChatMute, b: false);
		playerOrSleeper.ChatMessage("You have been unmuted");
	}

	[ServerVar(Help = "Print a list of currently muted players")]
	public static void mutelist(Arg arg)
	{
		var obj = from x in BasePlayer.allPlayerList
			where x.HasPlayerFlag(BasePlayer.PlayerFlags.ChatMute)
			select new
			{
				SteamId = x.UserIDString,
				Name = x.displayName
			};
		arg.ReplyWith(obj);
	}

	[ServerVar]
	public static void clientperf(Arg arg)
	{
		string arg2 = arg.GetString(0, "legacy");
		int arg3 = arg.GetInt(1, UnityEngine.Random.Range(int.MinValue, int.MaxValue));
		foreach (BasePlayer activePlayer in BasePlayer.activePlayerList)
		{
			activePlayer.ClientRPC(RpcTarget.Player("GetPerformanceReport", activePlayer), arg2, arg3);
		}
	}

	[ServerVar(Help = "Get information about all the cars in the world")]
	public static void carstats(Arg arg)
	{
		HashSet<ModularCar> allCarsList = ModularCar.allCarsList;
		using TextTable textTable = Facepunch.Pool.Get<TextTable>();
		textTable.AddColumn("id");
		textTable.AddColumn("sockets");
		textTable.AddColumn("modules");
		textTable.AddColumn("complete");
		textTable.AddColumn("engine");
		textTable.AddColumn("health");
		textTable.AddColumn("location");
		int count = allCarsList.Count;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		foreach (ModularCar item in allCarsList)
		{
			string text = item.net.ID.ToString();
			string text2 = item.TotalSockets.ToString();
			string text3 = item.NumAttachedModules.ToString();
			string text4;
			if (item.IsComplete())
			{
				text4 = "Complete";
				num++;
			}
			else
			{
				text4 = "Partial";
			}
			string text5;
			if (item.HasAnyWorkingEngines())
			{
				text5 = "Working";
				num2++;
			}
			else
			{
				text5 = "Broken";
			}
			string text6 = ((item.TotalMaxHealth() != 0f) ? $"{item.TotalHealth() / item.TotalMaxHealth():0%}" : "0");
			string text7;
			if (item.IsOutside())
			{
				text7 = "Outside";
			}
			else
			{
				text7 = "Inside";
				num3++;
			}
			textTable.AddRow(text, text2, text3, text4, text5, text6, text7);
		}
		string text8 = "";
		text8 = ((count != 1) ? (text8 + $"\nThe world contains {count} modular cars.") : (text8 + "\nThe world contains 1 modular car."));
		text8 = ((num != 1) ? (text8 + $"\n{num} ({(float)num / (float)count:0%}) are in a completed state.") : (text8 + $"\n1 ({1f / (float)count:0%}) is in a completed state."));
		text8 = ((num2 != 1) ? (text8 + $"\n{num2} ({(float)num2 / (float)count:0%}) are driveable.") : (text8 + $"\n1 ({1f / (float)count:0%}) is driveable."));
		arg.ReplyWith(string.Concat(str1: (num3 != 1) ? (text8 + $"\n{num3} ({(float)num3 / (float)count:0%}) are sheltered indoors.") : (text8 + $"\n1 ({1f / (float)count:0%}) is sheltered indoors."), str0: textTable.ToString()));
	}

	[ServerVar]
	public static string teaminfo(Arg arg)
	{
		ulong num = ArgEx.GetPlayerOrSleeper(arg, 0)?.userID ?? ((EncryptedValue<ulong>)0uL);
		if (num == 0L)
		{
			num = arg.GetULong(0, 0uL);
		}
		if (!SingletonComponent<ServerMgr>.Instance.persistance.DoesPlayerExist(num))
		{
			return "Player not found";
		}
		RelationshipManager.PlayerTeam playerTeam = RelationshipManager.ServerInstance.FindPlayersTeam(num);
		if (playerTeam == null)
		{
			return "Player is not in a team";
		}
		bool flag = arg.HasArg("--json");
		using TextTable textTable = Facepunch.Pool.Get<TextTable>();
		textTable.ShouldPadColumns = !flag;
		textTable.ResizeColumns(4);
		textTable.AddColumn("steamID");
		textTable.AddColumn("username");
		textTable.AddColumn("online");
		textTable.AddColumn("leader");
		textTable.ResizeRows(playerTeam.members.Count);
		foreach (ulong memberId in playerTeam.members)
		{
			bool flag2 = Network.Net.sv.connections.FirstOrDefault((Connection c) => c.connected && c.userid == memberId) != null;
			textTable.AddValue(memberId);
			textTable.AddValue(GetPlayerName(memberId));
			textTable.AddValue(flag2 ? "x" : "");
			textTable.AddValue((memberId == playerTeam.teamLeader) ? "x" : "");
		}
		return flag ? textTable.ToJson() : $"ID: {playerTeam.teamID}\n\n{textTable}";
	}

	[ServerVar]
	public static void authradius(Arg arg)
	{
		float num = arg.GetFloat(0, -1f);
		if (num < 0f)
		{
			arg.ReplyWith("Format is 'authradius {radius} [user]'");
			return;
		}
		List<BasePlayer> obj = Facepunch.Pool.Get<List<BasePlayer>>();
		obj.Add(ArgEx.GetPlayer(arg, 1) ?? ArgEx.Player(arg));
		SetAuthInRadius(obj[0], obj, num, auth: true);
		Facepunch.Pool.FreeUnmanaged(ref obj);
	}

	[ServerVar]
	public static void authradius_multi(Arg arg)
	{
		float num = arg.GetFloat(0, -1f);
		if (num < 0f)
		{
			arg.ReplyWith("Format is 'authradius {radius} [user, user, ...]'");
		}
		else
		{
			SetAuthInRadius(ArgEx.Player(arg), ArgEx.GetPlayerArgs(arg, 1), num, auth: true);
		}
	}

	[ServerVar]
	public static void authradius_radius(Arg arg)
	{
		run_authradius_radius(arg, authFlag: true);
	}

	[ServerVar]
	public static void deauthradius_radius(Arg arg)
	{
		run_authradius_radius(arg, authFlag: false);
	}

	private static void run_authradius_radius(Arg arg, bool authFlag)
	{
		float num = arg.GetFloat(0, -1f);
		float num2 = arg.GetFloat(1, -1f);
		if (num < 0f || num2 < 0f)
		{
			arg.ReplyWith("Format is 'authradius_radius {playerRadius, authRadius }'");
			return;
		}
		BasePlayer basePlayer = ArgEx.Player(arg);
		List<BasePlayer> obj = Facepunch.Pool.Get<List<BasePlayer>>();
		global::Vis.Entities(basePlayer.transform.position, num, obj, 131072);
		for (int num3 = obj.Count - 1; num3 >= 0; num3--)
		{
			BasePlayer basePlayer2 = obj[num3];
			if (basePlayer2 == null)
			{
				obj.RemoveAt(num3);
			}
			else if (basePlayer2.isClient || Vector3.Distance(basePlayer2.transform.position, basePlayer.transform.position) > num)
			{
				obj.Remove(basePlayer2);
			}
		}
		SetAuthInRadius(basePlayer, obj, num2, authFlag);
		Facepunch.Pool.FreeUnmanaged(ref obj);
	}

	[ServerVar]
	public static void deauthradius(Arg arg)
	{
		float num = arg.GetFloat(0, -1f);
		if (num < 0f)
		{
			arg.ReplyWith("Format is 'deauthradius {radius} [user]'");
			return;
		}
		List<BasePlayer> list = new List<BasePlayer>();
		list.Add(ArgEx.GetPlayer(arg, 1) ?? ArgEx.Player(arg));
		SetAuthInRadius(list[0], list, num, auth: false);
	}

	[ServerVar]
	public static void deauthradius_multi(Arg arg)
	{
		float num = arg.GetFloat(0, -1f);
		if (num < 0f)
		{
			arg.ReplyWith("Format is 'deauthradius {radius} [user, user, ...]'");
		}
		else
		{
			SetAuthInRadius(ArgEx.Player(arg), ArgEx.GetPlayerArgs(arg, 1), num, auth: false);
		}
	}

	private static void SetAuthInRadius(BasePlayer radiusTargetPlayer, List<BasePlayer> players, float radius, bool auth)
	{
		if (players == null)
		{
			return;
		}
		if (players.Count == 0)
		{
			players.Add(radiusTargetPlayer);
		}
		List<BaseEntity> list = new List<BaseEntity>();
		global::Vis.Entities(radiusTargetPlayer.transform.position, radius, list);
		int num = 0;
		foreach (BaseEntity item in list)
		{
			if (!item.isServer)
			{
				continue;
			}
			bool flag = true;
			foreach (BasePlayer player in players)
			{
				bool flag2 = SetUserAuthorized(item, player.userID, auth);
				if (!flag2)
				{
					flag2 = SetUserAuthorized(item.GetSlot(BaseEntity.Slot.Lock), player.userID, auth);
				}
				if (flag)
				{
					num += (flag2 ? 1 : 0);
					flag = false;
				}
			}
		}
		Debug.Log("Set auth: " + auth + " on " + players.Count + " players, for " + num + " entities.");
	}

	private static bool SetUserAuthorized(BaseEntity entity, ulong userId, bool state)
	{
		if (entity == null)
		{
			return false;
		}
		if (entity is CodeLock codeLock)
		{
			if (state)
			{
				codeLock.whitelistPlayers.Add(userId);
			}
			else
			{
				codeLock.whitelistPlayers.Remove(userId);
				codeLock.guestPlayers.Remove(userId);
			}
			codeLock.SendNetworkUpdate();
		}
		else if (entity is AutoTurret autoTurret)
		{
			if (state)
			{
				autoTurret.authorizedPlayers.Add(userId);
			}
			else
			{
				autoTurret.authorizedPlayers.Remove(userId);
			}
			autoTurret.SendNetworkUpdate();
		}
		else if (entity is BuildingPrivlidge buildingPrivlidge)
		{
			if (state)
			{
				buildingPrivlidge.authorizedPlayers.Add(userId);
			}
			else
			{
				buildingPrivlidge.authorizedPlayers.Remove(userId);
			}
			if (entity.GetSlot(BaseEntity.Slot.Lock).IsValid())
			{
				SetUserAuthorized(entity.GetSlot(BaseEntity.Slot.Lock), userId, state);
			}
			buildingPrivlidge.SendNetworkUpdate();
		}
		else if (entity is Tugboat tugboat)
		{
			VehiclePrivilege componentInChildren = tugboat.GetComponentInChildren<VehiclePrivilege>();
			if (componentInChildren != null)
			{
				if (state)
				{
					componentInChildren.authorizedPlayers.Add(userId);
				}
				else
				{
					componentInChildren.authorizedPlayers.Remove(userId);
				}
				componentInChildren.SendNetworkUpdate();
			}
		}
		else
		{
			if (!(entity is ModularCar modularCar))
			{
				return false;
			}
			if (state)
			{
				modularCar.CarLock.TryAddPlayer(userId);
			}
			else
			{
				modularCar.CarLock.TryRemovePlayer(userId);
			}
			modularCar.SendNetworkUpdate();
		}
		return true;
	}

	[ServerVar]
	public static void entid(Arg arg)
	{
		BaseEntity baseEntity = BaseNetworkable.serverEntities.Find(ArgEx.GetEntityID(arg, 1)) as BaseEntity;
		if (baseEntity == null || baseEntity is BasePlayer)
		{
			return;
		}
		string text = arg.GetString(0);
		if (ArgEx.Player(arg) != null)
		{
			Debug.Log("[ENTCMD] " + ArgEx.Player(arg).displayName + "/" + ArgEx.Player(arg).userID.Get() + " used *" + text + "* on ent: " + baseEntity.name);
		}
		switch (text)
		{
		case "kill":
			baseEntity.AdminKill();
			break;
		case "lock":
			baseEntity.SetFlag(BaseEntity.Flags.Locked, b: true);
			break;
		case "unlock":
			baseEntity.SetFlag(BaseEntity.Flags.Locked, b: false);
			break;
		case "debug":
			baseEntity.SetFlag(BaseEntity.Flags.Debugging, b: true);
			break;
		case "undebug":
			baseEntity.SetFlag(BaseEntity.Flags.Debugging, b: false);
			break;
		case "who":
			arg.ReplyWith(baseEntity.Admin_Who());
			break;
		case "auth":
			arg.ReplyWith(AuthList(baseEntity));
			break;
		case "upgrade":
			arg.ReplyWith(ChangeGrade(baseEntity, arg.GetInt(2, 1), 0, BuildingGrade.Enum.None, 0uL, arg.GetFloat(3)));
			break;
		case "downgrade":
			arg.ReplyWith(ChangeGrade(baseEntity, 0, arg.GetInt(2, 1), BuildingGrade.Enum.None, 0uL, arg.GetFloat(3)));
			break;
		case "setgrade":
		{
			string error;
			BuildingGrade buildingGrade = FindBuildingSkin(arg.GetString(2), out error);
			arg.ReplyWith(ChangeGrade(baseEntity, 0, 0, buildingGrade.type, buildingGrade.skin, arg.GetFloat(3)));
			break;
		}
		case "repair":
			RunInRadius(arg.GetFloat(2), baseEntity, delegate(BaseCombatEntity entity)
			{
				if (entity.repair.enabled)
				{
					entity.SetHealth(entity.MaxHealth());
				}
			});
			break;
		case "maxhp":
		{
			if (!(baseEntity is BaseCombatEntity baseCombatEntity))
			{
				arg.ReplyWith("Entity doesn't support max health!");
				break;
			}
			float num2 = arg.GetFloat(2);
			baseCombatEntity.OverrideMaxHealth(num2);
			if (num2 <= 0f)
			{
				arg.ReplyWith($"Removed max health override from {baseEntity}");
			}
			else
			{
				arg.ReplyWith($"Set max health to {num2}");
			}
			break;
		}
		case "dronetax":
		{
			List<MarketTerminal> list = new List<MarketTerminal>();
			if (baseEntity is Marketplace marketplace)
			{
				list.AddRange(from x in marketplace.terminalEntities
					select x.Get(serverside: true) into x
					where x != null
					select x);
			}
			else
			{
				if (!(baseEntity is MarketTerminal item))
				{
					arg.ReplyWith("Entity is not a market terminal!");
					break;
				}
				list.Add(item);
			}
			{
				foreach (MarketTerminal item2 in list)
				{
					string text2 = arg.GetString(2);
					if (int.TryParse(text2, out var result) && result > 0)
					{
						item2.deliveryFeeAmount = result;
						item2.SendNetworkUpdate();
						arg.ReplyWith($"Set drone tax to '{result}'");
						continue;
					}
					ItemDefinition itemDefinition = ItemManager.FindDefinitionByPartialName(text2);
					if (itemDefinition != null)
					{
						item2.deliveryFeeCurrency = itemDefinition;
						item2.SendNetworkUpdate();
						arg.ReplyWith("Set drone tax item to '" + itemDefinition.shortname + "'");
					}
					else
					{
						arg.ReplyWith("'" + text2 + "' is not a tax amount or valid item!");
					}
				}
				break;
			}
		}
		case "image":
		{
			if (!(baseEntity is ISignage signage))
			{
				arg.ReplyWith("Entity is not a sign");
				break;
			}
			uint[] textureCRCs = signage.GetTextureCRCs();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine($"{textureCRCs.Length} Image CRCs");
			uint[] array = textureCRCs;
			foreach (uint num4 in array)
			{
				stringBuilder.AppendLine(num4.ToString());
			}
			arg.ReplyWith(stringBuilder.ToString());
			break;
		}
		case "scale":
		{
			string text3 = arg.GetString(2);
			if (string.IsNullOrEmpty(text3))
			{
				arg.ReplyWith($"Scale: {baseEntity.transform.localScale}");
				break;
			}
			if (text3 == "default")
			{
				baseEntity.networkEntityScale = false;
				baseEntity.transform.localScale = Vector3.one;
				baseEntity.SendNetworkUpdate();
				arg.ReplyWith("Reset scale");
				break;
			}
			Vector3 one = Vector3.one;
			if (float.TryParse(text3, out var result2))
			{
				one = new Vector3(result2, result2, result2);
			}
			else
			{
				one = Vector3Ex.Parse(text3);
				if (one == Vector3.zero)
				{
					arg.ReplyWith(text3 + " is not a valid scale");
					break;
				}
			}
			baseEntity.networkEntityScale = true;
			baseEntity.transform.localScale = one;
			baseEntity.SendNetworkUpdate();
			arg.ReplyWith($"Set scale to {baseEntity.transform.localScale}");
			break;
		}
		case "settime":
		{
			int num = arg.GetInt(2, -1);
			if (num == -1)
			{
				arg.ReplyWith("Time not provided");
			}
			else if (baseEntity is WipeLaptopEntity wipeLaptopEntity)
			{
				wipeLaptopEntity.SetTimeLeft(num);
				arg.ReplyWith($"Set time left to {num}");
			}
			else
			{
				arg.ReplyWith("Not looking at a laptop");
			}
			break;
		}
		default:
			arg.ReplyWith("Unknown command");
			break;
		}
	}

	private static string AuthList(BaseEntity ent)
	{
		List<ulong> list;
		if (!(ent is BuildingPrivlidge buildingPrivlidge))
		{
			if (!(ent is AutoTurret autoTurret))
			{
				if (ent is CodeLock codeLock)
				{
					return CodeLockAuthList(codeLock);
				}
				if (ent is BaseVehicleModule vehicleModule)
				{
					return CodeLockAuthList(vehicleModule);
				}
				if (!(ent is Tugboat tugboat))
				{
					return "Entity has no auth list";
				}
				list = new List<ulong>();
				VehiclePrivilege componentInChildren = tugboat.GetComponentInChildren<VehiclePrivilege>();
				if (componentInChildren != null)
				{
					foreach (ulong authorizedPlayer in componentInChildren.authorizedPlayers)
					{
						list.Add(authorizedPlayer);
					}
				}
			}
			else
			{
				list = new List<ulong>();
				foreach (ulong authorizedPlayer2 in autoTurret.authorizedPlayers)
				{
					list.Add(authorizedPlayer2);
				}
			}
		}
		else
		{
			list = new List<ulong>();
			foreach (ulong authorizedPlayer3 in buildingPrivlidge.authorizedPlayers)
			{
				list.Add(authorizedPlayer3);
			}
		}
		if (list == null || list.Count == 0)
		{
			return "Nobody is authed to this entity";
		}
		using TextTable textTable = Facepunch.Pool.Get<TextTable>();
		textTable.AddColumn("steamID");
		textTable.AddColumn("username");
		foreach (ulong item in list)
		{
			textTable.AddRow(item.ToString(), GetPlayerName(item));
		}
		return textTable.ToString();
	}

	private static string CodeLockAuthList(CodeLock codeLock)
	{
		if (codeLock.whitelistPlayers.Count == 0 && codeLock.guestPlayers.Count == 0)
		{
			return "Nobody is authed to this entity";
		}
		using TextTable textTable = Facepunch.Pool.Get<TextTable>();
		textTable.AddColumn("steamID");
		textTable.AddColumn("username");
		textTable.AddColumn("isGuest");
		foreach (ulong whitelistPlayer in codeLock.whitelistPlayers)
		{
			textTable.AddRow(whitelistPlayer.ToString(), GetPlayerName(whitelistPlayer), "");
		}
		foreach (ulong guestPlayer in codeLock.guestPlayers)
		{
			textTable.AddRow(guestPlayer.ToString(), GetPlayerName(guestPlayer), "x");
		}
		return textTable.ToString();
	}

	private static string CodeLockAuthList(BaseVehicleModule vehicleModule)
	{
		if (!vehicleModule.IsOnAVehicle)
		{
			return "Nobody is authed to this entity";
		}
		ModularCar modularCar = vehicleModule.Vehicle as ModularCar;
		if (modularCar == null || !modularCar.IsLockable || modularCar.CarLock.WhitelistPlayers.Count == 0)
		{
			return "Nobody is authed to this entity";
		}
		using TextTable textTable = Facepunch.Pool.Get<TextTable>();
		textTable.AddColumn("steamID");
		textTable.AddColumn("username");
		foreach (ulong whitelistPlayer in modularCar.CarLock.WhitelistPlayers)
		{
			textTable.AddRow(whitelistPlayer.ToString(), GetPlayerName(whitelistPlayer));
		}
		return textTable.ToString();
	}

	public static string GetPlayerName(ulong steamId)
	{
		BasePlayer basePlayer = BasePlayer.allPlayerList.FirstOrDefault((BasePlayer p) => (ulong)p.userID == steamId);
		string text;
		if (!(basePlayer != null))
		{
			text = SingletonComponent<ServerMgr>.Instance.persistance.GetPlayerName(steamId);
			if (text == null)
			{
				return "[unknown]";
			}
		}
		else
		{
			text = basePlayer.displayName;
		}
		return text;
	}

	public static string ChangeGrade(BaseEntity entity, int increaseBy = 0, int decreaseBy = 0, BuildingGrade.Enum targetGrade = BuildingGrade.Enum.None, ulong skin = 0uL, float radius = 0f)
	{
		if (entity as BuildingBlock == null)
		{
			return $"'{entity}' is not a building block";
		}
		RunInRadius(radius, entity, delegate(BuildingBlock block)
		{
			BuildingGrade.Enum grade = block.grade;
			if (targetGrade > BuildingGrade.Enum.None && targetGrade < BuildingGrade.Enum.Count)
			{
				grade = targetGrade;
			}
			else
			{
				grade = (BuildingGrade.Enum)Mathf.Min((int)(grade + increaseBy), 4);
				grade = (BuildingGrade.Enum)Mathf.Max((int)(grade - decreaseBy), 0);
			}
			if (grade != block.grade)
			{
				block.ChangeGradeAndSkin(targetGrade, skin);
			}
		});
		int count = Facepunch.Pool.Get<List<BuildingBlock>>().Count;
		return $"Upgraded/downgraded '{count}' building block(s)";
	}

	private static bool RunInRadius<T>(float radius, BaseEntity initial, Action<T> callback, Func<T, bool> filter = null, int layerMask = 2097152) where T : BaseEntity
	{
		List<T> obj = Facepunch.Pool.Get<List<T>>();
		radius = Mathf.Clamp(radius, 0f, 200f);
		if (radius > 0f)
		{
			global::Vis.Entities(initial.transform.position, radius, obj, layerMask);
		}
		else if (initial is T item)
		{
			obj.Add(item);
		}
		foreach (T item2 in obj)
		{
			try
			{
				callback(item2);
			}
			catch (Exception arg)
			{
				Debug.LogError($"Exception while running callback in radius: {arg}");
				Facepunch.Pool.FreeUnmanaged(ref obj);
				return false;
			}
		}
		Facepunch.Pool.FreeUnmanaged(ref obj);
		return true;
	}

	[ServerVar(Help = "Get a list of players")]
	public static PlayerInfo[] playerlist()
	{
		return BasePlayer.activePlayerList.Select((BasePlayer x) => new PlayerInfo
		{
			SteamID = x.UserIDString,
			OwnerSteamID = x.OwnerID.ToString(),
			DisplayName = x.displayName,
			Ping = Network.Net.sv.GetAveragePing(x.net.connection),
			Address = x.net.connection.ipaddress,
			EntityId = x.net.ID.Value,
			ConnectedSeconds = (int)x.net.connection.GetSecondsConnected(),
			ViolationLevel = x.violationLevel,
			Health = x.Health()
		}).ToArray();
	}

	[ServerVar(Help = "Get a list of player's IDs")]
	public static PlayerIDInfo[] playerlistids()
	{
		return BasePlayer.activePlayerList.Select((BasePlayer x) => new PlayerIDInfo
		{
			SteamID = x.UserIDString,
			OwnerSteamID = x.OwnerID.ToString(),
			DisplayName = x.displayName,
			Address = x.net.connection.ipaddress,
			EntityId = x.net.ID.Value
		}).ToArray();
	}

	[ServerVar(Help = "List of banned users")]
	public static ServerUsers.User[] Bans()
	{
		return ServerUsers.GetAll(ServerUsers.UserGroup.Banned).ToArray();
	}

	[ServerVar(Help = "Get a list of information about the server")]
	public static ServerInfoOutput ServerInfo()
	{
		return new ServerInfoOutput
		{
			Hostname = Server.hostname,
			MaxPlayers = Server.maxplayers,
			Players = BasePlayer.activePlayerList.Count,
			Queued = SingletonComponent<ServerMgr>.Instance.connectionQueue.Queued,
			Joining = SingletonComponent<ServerMgr>.Instance.connectionQueue.Joining,
			ReservedSlots = SingletonComponent<ServerMgr>.Instance.connectionQueue.ReservedCount,
			EntityCount = BaseNetworkable.serverEntities.Count,
			GameTime = ((TOD_Sky.Instance != null) ? TOD_Sky.Instance.Cycle.DateTime.ToString() : DateTime.UtcNow.ToString()),
			Uptime = (int)UnityEngine.Time.realtimeSinceStartup,
			Map = Server.level,
			Framerate = Performance.report.frameRate,
			Memory = (int)Performance.report.memoryAllocations,
			MemoryUsageSystem = (int)Performance.report.memoryUsageSystem,
			Collections = (int)Performance.report.memoryCollections,
			NetworkIn = (int)((Network.Net.sv != null) ? Network.Net.sv.GetStat(null, BaseNetwork.StatTypeLong.BytesReceived_LastSecond) : 0),
			NetworkOut = (int)((Network.Net.sv != null) ? Network.Net.sv.GetStat(null, BaseNetwork.StatTypeLong.BytesSent_LastSecond) : 0),
			Restarting = SingletonComponent<ServerMgr>.Instance.Restarting,
			SaveCreatedTime = SaveRestore.SaveCreatedTime.ToString(),
			Version = 2615,
			Protocol = Protocol.printable
		};
	}

	[ServerVar(Help = "Get information about this build")]
	public static BuildInfo BuildInfo()
	{
		return Facepunch.BuildInfo.Current;
	}

	[ServerVar]
	public static void AdminUI_FullRefresh(Arg arg)
	{
		AdminUI_RequestPlayerList(arg);
		AdminUI_RequestServerInfo(arg);
		AdminUI_RequestServerConvars(arg);
		AdminUI_RequestUGCList(arg);
	}

	[ServerVar]
	public static void AdminUI_RequestPlayerList(Arg arg)
	{
		if (allowAdminUI)
		{
			ConsoleNetwork.SendClientCommand(arg.Connection, "AdminUI_ReceivePlayerList", JsonConvert.SerializeObject(playerlist()));
		}
	}

	[ServerVar]
	public static void AdminUI_RequestServerInfo(Arg arg)
	{
		if (allowAdminUI)
		{
			ConsoleNetwork.SendClientCommand(arg.Connection, "AdminUI_ReceiveServerInfo", JsonConvert.SerializeObject(ServerInfo()));
		}
	}

	[ServerVar]
	public static void AdminUI_RequestServerConvars(Arg arg)
	{
		if (!allowAdminUI)
		{
			return;
		}
		List<ServerConvarInfo> obj = Facepunch.Pool.Get<List<ServerConvarInfo>>();
		Command[] all = Index.All;
		foreach (Command command in all)
		{
			if (command.Server && command.Variable && command.ServerAdmin && command.ShowInAdminUI && !command.RconOnly)
			{
				obj.Add(new ServerConvarInfo
				{
					FullName = command.FullName,
					Value = command.GetOveride?.Invoke(),
					Help = command.Description
				});
			}
		}
		ConsoleNetwork.SendClientCommand(arg.Connection, "AdminUI_ReceiveCommands", JsonConvert.SerializeObject(obj));
		Facepunch.Pool.FreeUnmanaged(ref obj);
	}

	[ServerVar]
	public static void AdminUI_RequestUGCList(Arg arg)
	{
		if (!allowAdminUI)
		{
			return;
		}
		List<ServerUGCInfo> obj = Facepunch.Pool.Get<List<ServerUGCInfo>>();
		uint[] array = null;
		ulong[] array2 = null;
		foreach (BaseNetworkable serverEntity in BaseNetworkable.serverEntities)
		{
			if (ObjectEx.IsUnityNull(serverEntity))
			{
				continue;
			}
			array = null;
			array2 = null;
			UGCType uGCType = UGCType.ImageJpg;
			string text = string.Empty;
			if (serverEntity.TryGetComponent<IUGCBrowserEntity>(out var component))
			{
				if (component.UgcEntity == null)
				{
					continue;
				}
				array = component.GetContentCRCs;
				array2 = component.EditingHistory.ToArray();
				uGCType = component.ContentType;
				text = component.ContentString;
			}
			bool flag = false;
			if (array != null)
			{
				uint[] array3 = array;
				for (int i = 0; i < array3.Length; i++)
				{
					if (array3[i] != 0)
					{
						flag = true;
						break;
					}
				}
			}
			if (uGCType == UGCType.PatternBoomer)
			{
				flag = true;
				PatternFirework patternFirework = component as PatternFirework;
				if (patternFirework != null && patternFirework.Design == null)
				{
					flag = false;
				}
			}
			if (uGCType == UGCType.VendingMachine && !string.IsNullOrEmpty(text))
			{
				flag = true;
			}
			if (flag)
			{
				obj.Add(new ServerUGCInfo
				{
					entityId = serverEntity.net.ID.Value,
					crcs = array,
					contentType = uGCType,
					entityPrefabID = serverEntity.prefabID,
					shortPrefabName = serverEntity.ShortPrefabName,
					playerIds = array2,
					contentString = text
				});
			}
		}
		ConsoleNetwork.SendClientCommand(arg.Connection, "AdminUI_ReceiveUGCList", JsonConvert.SerializeObject(obj));
		Facepunch.Pool.FreeUnmanaged(ref obj);
	}

	[ServerVar]
	public static void AdminUI_RequestUGCContent(Arg arg)
	{
		if (allowAdminUI && !(ArgEx.Player(arg) == null))
		{
			uint uInt = arg.GetUInt(0);
			NetworkableId entityID = ArgEx.GetEntityID(arg, 1);
			FileStorage.Type type = (FileStorage.Type)arg.GetInt(2);
			uint uInt2 = arg.GetUInt(3);
			byte[] array = FileStorage.server.Get(uInt, type, entityID, uInt2);
			if (array != null)
			{
				SendInfo sendInfo = new SendInfo(arg.Connection);
				sendInfo.channel = 2;
				sendInfo.method = SendMethod.Reliable;
				SendInfo sendInfo2 = sendInfo;
				ArgEx.Player(arg).ClientRPC(RpcTarget.SendInfo("AdminReceivedUGC", sendInfo2), uInt, (uint)array.Length, array, uInt2, (byte)type);
			}
		}
	}

	[ServerVar]
	public static void AdminUI_DeleteUGCContent(Arg arg)
	{
		if (!allowAdminUI)
		{
			return;
		}
		NetworkableId entityID = ArgEx.GetEntityID(arg, 0);
		BaseNetworkable baseNetworkable = BaseNetworkable.serverEntities.Find(entityID);
		if (baseNetworkable != null)
		{
			FileStorage.server.RemoveAllByEntity(entityID);
			if (baseNetworkable.TryGetComponent<IUGCBrowserEntity>(out var component))
			{
				component.ClearContent();
			}
		}
	}

	[ServerVar]
	public static void AdminUI_RequestFireworkPattern(Arg arg)
	{
		if (allowAdminUI)
		{
			NetworkableId entityID = ArgEx.GetEntityID(arg, 0);
			BaseNetworkable baseNetworkable = BaseNetworkable.serverEntities.Find(entityID);
			if (baseNetworkable != null && baseNetworkable is PatternFirework { Design: not null } patternFirework)
			{
				SendInfo sendInfo = new SendInfo(arg.Connection);
				sendInfo.channel = 2;
				sendInfo.method = SendMethod.Reliable;
				SendInfo sendInfo2 = sendInfo;
				ArgEx.Player(arg).ClientRPC(RpcTarget.SendInfo("AdminReceivedPatternFirework", sendInfo2), entityID, patternFirework.Design.ToProtoBytes());
			}
		}
	}

	[ServerVar]
	public static void clearugcentity(Arg arg)
	{
		NetworkableId entityID = ArgEx.GetEntityID(arg, 0);
		BaseNetworkable baseNetworkable = BaseNetworkable.serverEntities.Find(entityID);
		if (baseNetworkable != null && baseNetworkable.TryGetComponent<IUGCBrowserEntity>(out var component))
		{
			component.ClearContent();
			arg.ReplyWith($"Cleared content on {baseNetworkable.ShortPrefabName}/{entityID}");
		}
		else
		{
			arg.ReplyWith($"Could not find UGC entity with id {entityID}");
		}
	}

	[ServerVar]
	public static void clearugcentitiesinrange(Arg arg)
	{
		Vector3 vector = arg.GetVector3(0);
		float num = arg.GetFloat(1);
		int num2 = 0;
		foreach (BaseNetworkable serverEntity in BaseNetworkable.serverEntities)
		{
			if (serverEntity.TryGetComponent<IUGCBrowserEntity>(out var component) && Vector3.Distance(serverEntity.transform.position, vector) <= num)
			{
				component.ClearContent();
				num2++;
			}
		}
		arg.ReplyWith($"Cleared {num2} UGC entities within {num}m of {vector}");
	}

	[ServerVar]
	public static void clearVendingMachineNamesContaining(Arg arg)
	{
		string text = arg.GetString(0);
		int num = 0;
		foreach (BaseNetworkable serverEntity in BaseNetworkable.serverEntities)
		{
			if (serverEntity.TryGetComponent<IUGCBrowserEntity>(out var component) && component.ContentType == UGCType.VendingMachine && component.ContentString.Contains(text, CompareOptions.IgnoreCase | CompareOptions.IgnoreSymbols))
			{
				component.ClearContent();
				num++;
			}
		}
		arg.ReplyWith($"Cleared {num} vending machines containing {text}");
	}

	[ServerVar]
	public static void clearUGCByPlayer(Arg arg)
	{
		BasePlayer playerOrSleeper = ArgEx.GetPlayerOrSleeper(arg, 0);
		ulong num = ((playerOrSleeper == null) ? arg.GetULong(0, 0uL) : playerOrSleeper.userID.Get());
		int num2 = 0;
		foreach (BaseNetworkable serverEntity in BaseNetworkable.serverEntities)
		{
			if (serverEntity.TryGetComponent<IUGCBrowserEntity>(out var component) && component.EditingHistory.Contains(num))
			{
				component.ClearContent();
				num2++;
			}
		}
		arg.ReplyWith($"Cleared {num2} UGC entities modified by {((playerOrSleeper != null) ? playerOrSleeper.displayName : ((object)num))}");
	}

	[ServerVar]
	public static void getugcinfo(Arg arg)
	{
		NetworkableId entityID = ArgEx.GetEntityID(arg, 0);
		BaseNetworkable baseNetworkable = BaseNetworkable.serverEntities.Find(entityID);
		if (baseNetworkable != null && baseNetworkable.TryGetComponent<IUGCBrowserEntity>(out var component) && component.UgcEntity != null)
		{
			ServerUGCInfo serverUGCInfo = new ServerUGCInfo(component);
			arg.ReplyWith(JsonConvert.SerializeObject(serverUGCInfo));
		}
		else
		{
			arg.ReplyWith($"Invalid entity id: {entityID}");
		}
	}

	[ServerVar(Help = "Returns all entities that the provided player is authed to (TC's, locks, etc), supports --json")]
	public static void authcount(Arg arg)
	{
		ulong num = ArgEx.GetPlayerOrSleeper(arg, 0)?.userID ?? ((EncryptedValue<ulong>)0uL);
		if (num == 0L)
		{
			num = arg.GetULong(0, 0uL);
		}
		if (!SingletonComponent<ServerMgr>.Instance.persistance.DoesPlayerExist(num))
		{
			arg.ReplyWith("Please provide a valid player, unable to find '" + arg.GetString(0) + "'");
			return;
		}
		string playerName = SingletonComponent<ServerMgr>.Instance.persistance.GetPlayerName(num);
		string text = arg.GetString(1);
		if (text == "--json")
		{
			text = string.Empty;
		}
		List<EntityAssociation> obj = Facepunch.Pool.Get<List<EntityAssociation>>();
		FindEntityAssociationsForPlayer(num, useOwnerId: false, useAuth: true, text, obj);
		bool flag = arg.HasArg("--json");
		using TextTable textTable = Facepunch.Pool.Get<TextTable>();
		textTable.ShouldPadColumns = !flag;
		textTable.AddColumns("Prefab name", "Position", "ID", "Type");
		foreach (EntityAssociation item in obj)
		{
			textTable.AddRow(item.TargetEntity.ShortPrefabName, item.TargetEntity.transform.position.ToString(), item.TargetEntity.net.ID.ToString(), item.AssociationType.ToString());
		}
		Facepunch.Pool.FreeUnmanaged(ref obj);
		if (flag)
		{
			arg.ReplyWith(textTable.ToJson());
			return;
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("Found entities " + playerName + " is authed to");
		stringBuilder.AppendLine(textTable.ToString());
		arg.ReplyWith(stringBuilder.ToString());
	}

	[ServerVar(Help = "Returns all entities that the provided player has placed, supports --json")]
	public static void entcount(Arg arg)
	{
		ulong uLong = arg.GetULong(0, 0uL);
		if (!SingletonComponent<ServerMgr>.Instance.persistance.DoesPlayerExist(uLong))
		{
			arg.ReplyWith($"Please provide a valid player, unable to find '{uLong}'");
			return;
		}
		string playerName = SingletonComponent<ServerMgr>.Instance.persistance.GetPlayerName(uLong);
		string text = arg.GetString(1);
		if (text == "--json")
		{
			text = string.Empty;
		}
		List<EntityAssociation> obj = Facepunch.Pool.Get<List<EntityAssociation>>();
		FindEntityAssociationsForPlayer(uLong, useOwnerId: true, useAuth: false, text, obj);
		bool flag = arg.HasArg("--json");
		using TextTable textTable = Facepunch.Pool.Get<TextTable>();
		textTable.ShouldPadColumns = !flag;
		textTable.AddColumns("Prefab name", "Position", "ID");
		foreach (EntityAssociation item in obj)
		{
			textTable.AddRow(item.TargetEntity.ShortPrefabName, item.TargetEntity.transform.position.ToString(), item.TargetEntity.net.ID.ToString());
		}
		Facepunch.Pool.FreeUnmanaged(ref obj);
		if (flag)
		{
			arg.ReplyWith(textTable.ToJson());
			return;
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("Found entities associated with " + playerName);
		stringBuilder.AppendLine(textTable.ToString());
		arg.ReplyWith(stringBuilder.ToString());
	}

	private static void FindEntityAssociationsForPlayer(ulong steamId, bool useOwnerId, bool useAuth, string filter, List<EntityAssociation> results)
	{
		results.Clear();
		foreach (BaseNetworkable serverEntity in BaseNetworkable.serverEntities)
		{
			EntityAssociationType entityAssociationType = EntityAssociationType.Owner;
			if (!(serverEntity is BaseEntity baseEntity))
			{
				continue;
			}
			bool flag = false;
			if (useOwnerId && baseEntity.OwnerID == steamId)
			{
				flag = true;
			}
			if (useAuth && !flag)
			{
				if (!flag && baseEntity is BuildingPrivlidge buildingPrivlidge && buildingPrivlidge.IsAuthed(steamId))
				{
					flag = true;
				}
				if (!flag && baseEntity is KeyLock keyLock && keyLock.OwnerID == steamId)
				{
					flag = true;
				}
				else if (baseEntity is CodeLock codeLock)
				{
					if (codeLock.whitelistPlayers.Contains(steamId))
					{
						flag = true;
					}
					else if (codeLock.guestPlayers.Contains(steamId))
					{
						flag = true;
						entityAssociationType = EntityAssociationType.LockGuest;
					}
				}
				if (!flag && baseEntity is ModularCar { IsLockable: not false } modularCar && modularCar.CarLock.HasLockPermission(steamId))
				{
					flag = true;
				}
				if (flag && entityAssociationType == EntityAssociationType.Owner)
				{
					entityAssociationType = EntityAssociationType.Auth;
				}
			}
			if (flag && !string.IsNullOrEmpty(filter) && !serverEntity.ShortPrefabName.Contains(filter, CompareOptions.IgnoreCase))
			{
				flag = false;
			}
			if (flag)
			{
				results.Add(new EntityAssociation
				{
					TargetEntity = baseEntity,
					AssociationType = entityAssociationType
				});
			}
		}
	}
}
