using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using ConVar;
using Facepunch;
using Facepunch.Extend;
using JSON;
using Network;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Oxide.Core;
using Oxide.Core.Configuration;
using Oxide.Core.Extensions;
using Oxide.Core.Libraries;
using Oxide.Core.Libraries.Covalence;
using Oxide.Core.Plugins;
using Oxide.Core.RemoteConsole;
using Oxide.Game.Rust.Libraries;
using Oxide.Game.Rust.Libraries.Covalence;
using Oxide.Plugins;
using Oxide.Pooling;
using ProtoBuf;
using Rust;
using Rust.Ai.Gen2;
using Steamworks;
using UnityEngine;
using UnityEngine.UI;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: TargetFramework(".NETFramework,Version=v4.8", FrameworkDisplayName = ".NET Framework 4.8")]
[assembly: AssemblyCompany("Oxide Team and Contributors")]
[assembly: AssemblyConfiguration("Release")]
[assembly: AssemblyCopyright("(c) 2013-2025 Oxide Team and Contributors")]
[assembly: AssemblyDescription("Rust extension for the Oxide modding framework")]
[assembly: AssemblyFileVersion("2.0.6780.0")]
[assembly: AssemblyInformationalVersion("2.0.6780+0a9bacda49245e1ba668bf447041d38f13b9ae22")]
[assembly: AssemblyProduct("Oxide.Rust")]
[assembly: AssemblyTitle("Oxide.Rust")]
[assembly: AssemblyMetadata("RepositoryUrl", "https://github.com/OxideMod/Oxide.Rust")]
[assembly: AssemblyMetadata("GitBranch", "master")]
[assembly: AssemblyVersion("2.0.6780.0")]
namespace Oxide.Plugins
{
	public static class RustExtensionMethods
	{
		public static bool IsSteamId(this EncryptedValue<ulong> userID)
		{
			return ExtensionMethods.IsSteamId(userID);
		}
	}
	public abstract class RustPlugin : CSharpPlugin
	{
		protected Command cmd = Interface.Oxide.GetLibrary<Command>();

		protected Oxide.Game.Rust.Libraries.Rust rust = Interface.Oxide.GetLibrary<Oxide.Game.Rust.Libraries.Rust>();

		protected Oxide.Game.Rust.Libraries.Item Item = Interface.Oxide.GetLibrary<Oxide.Game.Rust.Libraries.Item>();

		protected Oxide.Game.Rust.Libraries.Player Player = Interface.Oxide.GetLibrary<Oxide.Game.Rust.Libraries.Player>();

		protected Oxide.Game.Rust.Libraries.Server Server = Interface.Oxide.GetLibrary<Oxide.Game.Rust.Libraries.Server>();

		public override void HandleAddedToManager(PluginManager manager)
		{
			FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
			foreach (FieldInfo fieldInfo in fields)
			{
				if (fieldInfo.GetCustomAttributes(typeof(OnlinePlayersAttribute), inherit: true).Length != 0)
				{
					PluginFieldInfo pluginFieldInfo = new PluginFieldInfo(this, fieldInfo);
					if (pluginFieldInfo.GenericArguments.Length != 2 || pluginFieldInfo.GenericArguments[0] != typeof(BasePlayer))
					{
						Puts("The " + fieldInfo.Name + " field is not a Hash with a BasePlayer key! (online players will not be tracked)");
					}
					else if (!pluginFieldInfo.LookupMethod("Add", pluginFieldInfo.GenericArguments))
					{
						Puts("The " + fieldInfo.Name + " field does not support adding BasePlayer keys! (online players will not be tracked)");
					}
					else if (!pluginFieldInfo.LookupMethod("Remove", typeof(BasePlayer)))
					{
						Puts("The " + fieldInfo.Name + " field does not support removing BasePlayer keys! (online players will not be tracked)");
					}
					else if (pluginFieldInfo.GenericArguments[1].GetField("Player") == null)
					{
						Puts("The " + pluginFieldInfo.GenericArguments[1].Name + " class does not have a public Player field! (online players will not be tracked)");
					}
					else if (!pluginFieldInfo.HasValidConstructor(typeof(BasePlayer)))
					{
						Puts("The " + fieldInfo.Name + " field is using a class which contains no valid constructor (online players will not be tracked)");
					}
					else
					{
						onlinePlayerFields.Add(pluginFieldInfo);
					}
				}
			}
			MethodInfo[] methods = GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
			foreach (MethodInfo methodInfo in methods)
			{
				object[] customAttributes = methodInfo.GetCustomAttributes(typeof(ConsoleCommandAttribute), inherit: true);
				if (customAttributes.Length != 0)
				{
					if (customAttributes[0] is ConsoleCommandAttribute consoleCommandAttribute)
					{
						cmd.AddConsoleCommand(consoleCommandAttribute.Command, this, methodInfo.Name);
					}
					continue;
				}
				customAttributes = methodInfo.GetCustomAttributes(typeof(ChatCommandAttribute), inherit: true);
				if (customAttributes.Length != 0 && customAttributes[0] is ChatCommandAttribute chatCommandAttribute)
				{
					cmd.AddChatCommand(chatCommandAttribute.Command, this, methodInfo.Name);
				}
			}
			if (onlinePlayerFields.Count > 0)
			{
				foreach (BasePlayer activePlayer in BasePlayer.activePlayerList)
				{
					AddOnlinePlayer(activePlayer);
				}
			}
			base.HandleAddedToManager(manager);
		}

		[HookMethod("OnPlayerConnected")]
		private void base_OnPlayerConnected(BasePlayer player)
		{
			AddOnlinePlayer(player);
		}

		[HookMethod("OnPlayerDisconnected")]
		private void base_OnPlayerDisconnected(BasePlayer player, string reason)
		{
			NextTick(delegate
			{
				foreach (PluginFieldInfo onlinePlayerField in onlinePlayerFields)
				{
					onlinePlayerField.Call("Remove", player);
				}
			});
		}

		private void AddOnlinePlayer(BasePlayer player)
		{
			foreach (PluginFieldInfo onlinePlayerField in onlinePlayerFields)
			{
				Type type = onlinePlayerField.GenericArguments[1];
				object obj = ((type.GetConstructor(new Type[1] { typeof(BasePlayer) }) == null) ? Activator.CreateInstance(type) : Activator.CreateInstance(type, new object[1] { player }));
				type.GetField("Player").SetValue(obj, player);
				onlinePlayerField.Call("Add", player, obj);
			}
		}

		protected void PrintToConsole(BasePlayer player, string format, params object[] args)
		{
			if (player?.net != null)
			{
				player.SendConsoleCommand("echo " + ((args.Length != 0) ? string.Format(format, args) : format));
			}
		}

		protected void PrintToConsole(string format, params object[] args)
		{
			if (BasePlayer.activePlayerList.Count >= 1)
			{
				ConsoleNetwork.BroadcastToAllClients("echo " + ((args.Length != 0) ? string.Format(format, args) : format));
			}
		}

		protected void PrintToChat(BasePlayer player, string format, params object[] args)
		{
			if (player?.net != null)
			{
				player.SendConsoleCommand("chat.add", 2, 0, (args.Length != 0) ? string.Format(format, args) : format);
			}
		}

		protected void PrintToChat(string format, params object[] args)
		{
			if (BasePlayer.activePlayerList.Count >= 1)
			{
				ConsoleNetwork.BroadcastToAllClients("chat.add", 2, 0, (args.Length != 0) ? string.Format(format, args) : format);
			}
		}

		protected void SendReply(ConsoleSystem.Arg arg, string format, params object[] args)
		{
			BasePlayer basePlayer = arg.Connection?.player as BasePlayer;
			string text = ((args.Length != 0) ? string.Format(format, args) : format);
			if (basePlayer?.net != null)
			{
				basePlayer.SendConsoleCommand("echo " + text);
			}
			else
			{
				Puts(text);
			}
		}

		protected void SendReply(BasePlayer player, string format, params object[] args)
		{
			PrintToChat(player, format, args);
		}

		protected void SendWarning(ConsoleSystem.Arg arg, string format, params object[] args)
		{
			BasePlayer basePlayer = arg.Connection?.player as BasePlayer;
			string text = ((args.Length != 0) ? string.Format(format, args) : format);
			if (basePlayer?.net != null)
			{
				basePlayer.SendConsoleCommand("echo " + text);
			}
			else
			{
				UnityEngine.Debug.LogWarning(text);
			}
		}

		protected void SendError(ConsoleSystem.Arg arg, string format, params object[] args)
		{
			BasePlayer basePlayer = arg.Connection?.player as BasePlayer;
			string text = ((args.Length != 0) ? string.Format(format, args) : format);
			if (basePlayer?.net != null)
			{
				basePlayer.SendConsoleCommand("echo " + text);
			}
			else
			{
				UnityEngine.Debug.LogError(text);
			}
		}

		protected void ForcePlayerPosition(BasePlayer player, Vector3 destination)
		{
			player.MovePosition(destination);
			if (!player.IsSpectating() || (double)Vector3.Distance(player.transform.position, destination) > 25.0)
			{
				player.ClientRPC(RpcTarget.Player("ForcePositionTo", player), destination);
			}
			else
			{
				player.SendNetworkUpdate(BasePlayer.NetworkQueue.UpdateDistance);
			}
		}
	}
}
namespace Oxide.Game.Rust
{
	public class RustCore : CSPlugin
	{
		internal readonly Command cmdlib = Interface.Oxide.GetLibrary<Command>();

		internal readonly Lang lang = Interface.Oxide.GetLibrary<Lang>();

		internal readonly Permission permission = Interface.Oxide.GetLibrary<Permission>();

		internal readonly Oxide.Game.Rust.Libraries.Player Player = Interface.Oxide.GetLibrary<Oxide.Game.Rust.Libraries.Player>();

		internal static readonly RustCovalenceProvider Covalence = RustCovalenceProvider.Instance;

		internal readonly PluginManager pluginManager = Interface.Oxide.RootPluginManager;

		internal readonly IServer Server = Covalence.CreateServer();

		internal readonly RustExtension Extension;

		internal bool serverInitialized;

		internal bool isPlayerTakingDamage;

		internal static string ipPattern = ":{1}[0-9]{1}\\d*";

		private static readonly DateTime Eoy = new DateTime(2025, 12, 31);

		internal static IEnumerable<string> RestrictedCommands => new string[4] { "ownerid", "moderatorid", "removeowner", "removemoderator" };

		[HookMethod("GrantCommand")]
		private void GrantCommand(IPlayer player, string command, string[] args)
		{
			if (!PermissionsLoaded(player))
			{
				return;
			}
			if (args.Length < 3)
			{
				player.Reply(lang.GetMessage("CommandUsageGrant", this, player.Id));
				return;
			}
			string text = args[0];
			string text2 = Oxide.Core.ExtensionMethods.Sanitize(args[1]);
			string text3 = args[2];
			if (!permission.PermissionExists(text3))
			{
				player.Reply(string.Format(lang.GetMessage("PermissionNotFound", this, player.Id), text3));
			}
			else if (text.Equals("group"))
			{
				if (!permission.GroupExists(text2))
				{
					player.Reply(string.Format(lang.GetMessage("GroupNotFound", this, player.Id), text2));
					return;
				}
				if (permission.GroupHasPermission(text2, text3))
				{
					player.Reply(string.Format(lang.GetMessage("GroupAlreadyHasPermission", this, player.Id), text2, text3));
					return;
				}
				permission.GrantGroupPermission(text2, text3, null);
				player.Reply(string.Format(lang.GetMessage("GroupPermissionGranted", this, player.Id), text2, text3));
			}
			else if (text.Equals("user"))
			{
				IPlayer[] array = Covalence.PlayerManager.FindPlayers(text2).ToArray();
				if (array.Length > 1)
				{
					player.Reply(string.Format(lang.GetMessage("PlayersFound", this, player.Id), string.Join(", ", array.Select((IPlayer p) => p.Name).ToArray())));
					return;
				}
				IPlayer player2 = ((array.Length == 1) ? array[0] : null);
				if (player2 == null && !permission.UserIdValid(text2))
				{
					player.Reply(string.Format(lang.GetMessage("PlayerNotFound", this, player.Id), text2));
					return;
				}
				string text4 = text2;
				if (player2 != null)
				{
					text4 = player2.Id;
					text2 = player2.Name;
					permission.UpdateNickname(text4, text2);
				}
				if (permission.UserHasPermission(text2, text3))
				{
					player.Reply(string.Format(lang.GetMessage("PlayerAlreadyHasPermission", this, player.Id), text4, text3));
					return;
				}
				permission.GrantUserPermission(text4, text3, null);
				player.Reply(string.Format(lang.GetMessage("PlayerPermissionGranted", this, player.Id), text2 + " (" + text4 + ")", text3));
			}
			else
			{
				player.Reply(lang.GetMessage("CommandUsageGrant", this, player.Id));
			}
		}

		[HookMethod("GroupCommand")]
		private void GroupCommand(IPlayer player, string command, string[] args)
		{
			if (!PermissionsLoaded(player))
			{
				return;
			}
			if (args.Length < 2)
			{
				player.Reply(lang.GetMessage("CommandUsageGroup", this, player.Id));
				player.Reply(lang.GetMessage("CommandUsageGroupParent", this, player.Id));
				player.Reply(lang.GetMessage("CommandUsageGroupRemove", this, player.Id));
				return;
			}
			string text = args[0];
			string text2 = args[1];
			string groupTitle = ((args.Length >= 3) ? args[2] : "");
			int groupRank = ((args.Length == 4) ? int.Parse(args[3]) : 0);
			if (text.Equals("add"))
			{
				if (permission.GroupExists(text2))
				{
					player.Reply(string.Format(lang.GetMessage("GroupAlreadyExists", this, player.Id), text2));
					return;
				}
				permission.CreateGroup(text2, groupTitle, groupRank);
				player.Reply(string.Format(lang.GetMessage("GroupCreated", this, player.Id), text2));
			}
			else if (text.Equals("remove"))
			{
				if (!permission.GroupExists(text2))
				{
					player.Reply(string.Format(lang.GetMessage("GroupNotFound", this, player.Id), text2));
					return;
				}
				permission.RemoveGroup(text2);
				player.Reply(string.Format(lang.GetMessage("GroupDeleted", this, player.Id), text2));
			}
			else if (text.Equals("set"))
			{
				if (!permission.GroupExists(text2))
				{
					player.Reply(string.Format(lang.GetMessage("GroupNotFound", this, player.Id), text2));
					return;
				}
				permission.SetGroupTitle(text2, groupTitle);
				permission.SetGroupRank(text2, groupRank);
				player.Reply(string.Format(lang.GetMessage("GroupChanged", this, player.Id), text2));
			}
			else if (text.Equals("parent"))
			{
				if (args.Length <= 2)
				{
					player.Reply(lang.GetMessage("CommandUsageGroupParent", this, player.Id));
					return;
				}
				if (!permission.GroupExists(text2))
				{
					player.Reply(string.Format(lang.GetMessage("GroupNotFound", this, player.Id), text2));
					return;
				}
				string text3 = args[2];
				if (!string.IsNullOrEmpty(text3) && !permission.GroupExists(text3))
				{
					player.Reply(string.Format(lang.GetMessage("GroupParentNotFound", this, player.Id), text3));
				}
				else if (permission.SetGroupParent(text2, text3))
				{
					player.Reply(string.Format(lang.GetMessage("GroupParentChanged", this, player.Id), text2, text3));
				}
				else
				{
					player.Reply(string.Format(lang.GetMessage("GroupParentNotChanged", this, player.Id), text2));
				}
			}
			else
			{
				player.Reply(lang.GetMessage("CommandUsageGroup", this, player.Id));
				player.Reply(lang.GetMessage("CommandUsageGroupParent", this, player.Id));
				player.Reply(lang.GetMessage("CommandUsageGroupRemove", this, player.Id));
			}
		}

		[HookMethod("LangCommand")]
		private void LangCommand(IPlayer player, string command, string[] args)
		{
			if (args.Length < 1)
			{
				player.Reply(lang.GetMessage("CommandUsageLang", this, player.Id));
				return;
			}
			string text = args[0];
			try
			{
				text = new CultureInfo(text)?.TwoLetterISOLanguageName;
			}
			catch (CultureNotFoundException)
			{
				player.Reply(lang.GetMessage("InvalidLanguageName", this, player.Id), text);
				return;
			}
			if (player.IsServer)
			{
				lang.SetServerLanguage(text);
				player.Reply(string.Format(lang.GetMessage("ServerLanguage", this, player.Id), lang.GetServerLanguage()));
			}
			else
			{
				lang.SetLanguage(text, player.Id);
				player.Reply(string.Format(lang.GetMessage("PlayerLanguage", this, player.Id), text));
			}
		}

		[HookMethod("LoadCommand")]
		private void LoadCommand(IPlayer player, string command, string[] args)
		{
			if (args.Length < 1)
			{
				player.Reply(lang.GetMessage("CommandUsageLoad", this, player.Id));
				return;
			}
			if (args[0].Equals("*") || args[0].Equals("all"))
			{
				Interface.Oxide.LoadAllPlugins();
				return;
			}
			foreach (string value in args)
			{
				if (!string.IsNullOrEmpty(value))
				{
					Interface.Oxide.LoadPlugin(value);
					pluginManager.GetPlugin(value);
				}
			}
		}

		[HookMethod("PluginsCommand")]
		private void PluginsCommand(IPlayer player)
		{
			Plugin[] array = (from pl in pluginManager.GetPlugins()
				where !pl.IsCorePlugin
				select pl).ToArray();
			HashSet<string> second = new HashSet<string>(array.Select((Plugin pl) => pl.Name));
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (PluginLoader pluginLoader in Interface.Oxide.GetPluginLoaders())
			{
				foreach (string item in pluginLoader.ScanDirectory(Interface.Oxide.PluginDirectory).Except(second))
				{
					dictionary[item] = (pluginLoader.PluginErrors.TryGetValue(item, out var value) ? value : "Unloaded");
				}
			}
			if (array.Length + dictionary.Count < 1)
			{
				player.Reply(lang.GetMessage("NoPluginsFound", this, player.Id));
				return;
			}
			string text = $"Listing {array.Length + dictionary.Count} plugins:";
			int num = 1;
			foreach (Plugin item2 in array.Where((Plugin p) => p.Filename != null))
			{
				text += $"\n  {num++:00} \"{item2.Title}\" ({item2.Version}) by {item2.Author} ({item2.TotalHookTime:0.00}s / {FormatBytes(item2.TotalHookMemory)}) - {Oxide.Core.ExtensionMethods.Basename(item2.Filename)}";
			}
			foreach (string key in dictionary.Keys)
			{
				text += $"\n  {num++:00} {key} - {dictionary[key]}";
			}
			player.Reply(text);
		}

		private static string FormatBytes(long bytes)
		{
			if (bytes < 1024)
			{
				return $"{bytes:0} B";
			}
			if (bytes < 1048576)
			{
				return $"{bytes / 1024:0} KB";
			}
			if (bytes < 1073741824)
			{
				return $"{bytes / 1048576:0} MB";
			}
			return $"{bytes / 1073741824:0} GB";
		}

		[HookMethod("ReloadCommand")]
		private void ReloadCommand(IPlayer player, string command, string[] args)
		{
			if (args.Length < 1)
			{
				player.Reply(lang.GetMessage("CommandUsageReload", this, player.Id));
				return;
			}
			if (args[0].Equals("*") || args[0].Equals("all"))
			{
				Interface.Oxide.ReloadAllPlugins();
				return;
			}
			foreach (string value in args)
			{
				if (!string.IsNullOrEmpty(value))
				{
					Interface.Oxide.ReloadPlugin(value);
				}
			}
		}

		[HookMethod("RevokeCommand")]
		private void RevokeCommand(IPlayer player, string command, string[] args)
		{
			if (!PermissionsLoaded(player))
			{
				return;
			}
			if (args.Length < 3)
			{
				player.Reply(lang.GetMessage("CommandUsageRevoke", this, player.Id));
				return;
			}
			string text = args[0];
			string text2 = Oxide.Core.ExtensionMethods.Sanitize(args[1]);
			string arg = args[2];
			if (text.Equals("group"))
			{
				if (!permission.GroupExists(text2))
				{
					player.Reply(string.Format(lang.GetMessage("GroupNotFound", this, player.Id), text2));
					return;
				}
				if (!permission.GroupHasPermission(text2, arg))
				{
					player.Reply(string.Format(lang.GetMessage("GroupDoesNotHavePermission", this, player.Id), text2, arg));
					return;
				}
				permission.RevokeGroupPermission(text2, arg);
				player.Reply(string.Format(lang.GetMessage("GroupPermissionRevoked", this, player.Id), text2, arg));
			}
			else if (text.Equals("user"))
			{
				IPlayer[] array = Covalence.PlayerManager.FindPlayers(text2).ToArray();
				if (array.Length > 1)
				{
					player.Reply(string.Format(lang.GetMessage("PlayersFound", this, player.Id), string.Join(", ", array.Select((IPlayer p) => p.Name).ToArray())));
					return;
				}
				IPlayer player2 = ((array.Length == 1) ? array[0] : null);
				if (player2 == null && !permission.UserIdValid(text2))
				{
					player.Reply(string.Format(lang.GetMessage("PlayerNotFound", this, player.Id), text2));
					return;
				}
				string text3 = text2;
				if (player2 != null)
				{
					text3 = player2.Id;
					text2 = player2.Name;
					permission.UpdateNickname(text3, text2);
				}
				if (!permission.UserHasPermission(text3, arg))
				{
					player.Reply(string.Format(lang.GetMessage("PlayerDoesNotHavePermission", this, player.Id), text2, arg));
					return;
				}
				permission.RevokeUserPermission(text3, arg);
				player.Reply(string.Format(lang.GetMessage("PlayerPermissionRevoked", this, player.Id), text2 + " (" + text3 + ")", arg));
			}
			else
			{
				player.Reply(lang.GetMessage("CommandUsageRevoke", this, player.Id));
			}
		}

		[HookMethod("ShowCommand")]
		private void ShowCommand(IPlayer player, string command, string[] args)
		{
			if (!PermissionsLoaded(player))
			{
				return;
			}
			if (args.Length < 1)
			{
				player.Reply(lang.GetMessage("CommandUsageShow", this, player.Id));
				player.Reply(lang.GetMessage("CommandUsageShowName", this, player.Id));
				return;
			}
			string text = args[0];
			string text2 = ((args.Length == 2) ? Oxide.Core.ExtensionMethods.Sanitize(args[1]) : string.Empty);
			if (text.Equals("perms"))
			{
				player.Reply(string.Format(lang.GetMessage("Permissions", this, player.Id) + ":\n" + string.Join(", ", permission.GetPermissions())));
			}
			else if (text.Equals("perm"))
			{
				if (args.Length < 2 || string.IsNullOrEmpty(text2))
				{
					player.Reply(lang.GetMessage("CommandUsageShow", this, player.Id));
					player.Reply(lang.GetMessage("CommandUsageShowName", this, player.Id));
					return;
				}
				string[] permissionUsers = permission.GetPermissionUsers(text2);
				string[] permissionGroups = permission.GetPermissionGroups(text2);
				string text3 = string.Format(lang.GetMessage("PermissionPlayers", this, player.Id), text2) + ":\n";
				text3 += ((permissionUsers.Length != 0) ? string.Join(", ", permissionUsers) : lang.GetMessage("NoPermissionPlayers", this, player.Id));
				text3 = text3 + "\n\n" + string.Format(lang.GetMessage("PermissionGroups", this, player.Id), text2) + ":\n";
				text3 += ((permissionGroups.Length != 0) ? string.Join(", ", permissionGroups) : lang.GetMessage("NoPermissionGroups", this, player.Id));
				player.Reply(text3);
			}
			else if (text.Equals("user"))
			{
				if (args.Length < 2 || string.IsNullOrEmpty(text2))
				{
					player.Reply(lang.GetMessage("CommandUsageShow", this, player.Id));
					player.Reply(lang.GetMessage("CommandUsageShowName", this, player.Id));
					return;
				}
				IPlayer[] array = Covalence.PlayerManager.FindPlayers(text2).ToArray();
				if (array.Length > 1)
				{
					player.Reply(string.Format(lang.GetMessage("PlayersFound", this, player.Id), string.Join(", ", array.Select((IPlayer p) => p.Name).ToArray())));
					return;
				}
				IPlayer player2 = ((array.Length == 1) ? array[0] : null);
				if (player2 == null && !permission.UserIdValid(text2))
				{
					player.Reply(string.Format(lang.GetMessage("PlayerNotFound", this, player.Id), text2));
					return;
				}
				string text4 = text2;
				if (player2 != null)
				{
					text4 = player2.Id;
					text2 = player2.Name;
					permission.UpdateNickname(text4, text2);
					text2 = text2 + " (" + text4 + ")";
				}
				string[] userPermissions = permission.GetUserPermissions(text4);
				string[] userGroups = permission.GetUserGroups(text4);
				string text5 = string.Format(lang.GetMessage("PlayerPermissions", this, player.Id), text2) + ":\n";
				text5 += ((userPermissions.Length != 0) ? string.Join(", ", userPermissions) : lang.GetMessage("NoPlayerPermissions", this, player.Id));
				text5 = text5 + "\n\n" + string.Format(lang.GetMessage("PlayerGroups", this, player.Id), text2) + ":\n";
				text5 += ((userGroups.Length != 0) ? string.Join(", ", userGroups) : lang.GetMessage("NoPlayerGroups", this, player.Id));
				player.Reply(text5);
			}
			else if (text.Equals("group"))
			{
				if (args.Length < 2 || string.IsNullOrEmpty(text2))
				{
					player.Reply(lang.GetMessage("CommandUsageShow", this, player.Id));
					player.Reply(lang.GetMessage("CommandUsageShowName", this, player.Id));
					return;
				}
				if (!permission.GroupExists(text2))
				{
					player.Reply(string.Format(lang.GetMessage("GroupNotFound", this, player.Id), text2));
					return;
				}
				string[] usersInGroup = permission.GetUsersInGroup(text2);
				string[] groupPermissions = permission.GetGroupPermissions(text2);
				string text6 = string.Format(lang.GetMessage("GroupPlayers", this, player.Id), text2) + ":\n";
				text6 += ((usersInGroup.Length != 0) ? string.Join(", ", usersInGroup) : lang.GetMessage("NoPlayersInGroup", this, player.Id));
				text6 = text6 + "\n\n" + string.Format(lang.GetMessage("GroupPermissions", this, player.Id), text2) + ":\n";
				text6 += ((groupPermissions.Length != 0) ? string.Join(", ", groupPermissions) : lang.GetMessage("NoGroupPermissions", this, player.Id));
				string groupParent = permission.GetGroupParent(text2);
				while (permission.GroupExists(groupParent))
				{
					text6 = text6 + "\n" + string.Format(lang.GetMessage("ParentGroupPermissions", this, player.Id), groupParent) + ":\n";
					text6 += string.Join(", ", permission.GetGroupPermissions(groupParent));
					groupParent = permission.GetGroupParent(groupParent);
				}
				player.Reply(text6);
			}
			else if (text.Equals("groups"))
			{
				player.Reply(string.Format(lang.GetMessage("Groups", this, player.Id) + ":\n" + string.Join(", ", permission.GetGroups())));
			}
			else
			{
				player.Reply(lang.GetMessage("CommandUsageShow", this, player.Id));
				player.Reply(lang.GetMessage("CommandUsageShowName", this, player.Id));
			}
		}

		[HookMethod("UnloadCommand")]
		private void UnloadCommand(IPlayer player, string command, string[] args)
		{
			if (args.Length < 1)
			{
				player.Reply(lang.GetMessage("CommandUsageUnload", this, player.Id));
				return;
			}
			if (args[0].Equals("*") || args[0].Equals("all"))
			{
				Interface.Oxide.UnloadAllPlugins();
				return;
			}
			foreach (string value in args)
			{
				if (!string.IsNullOrEmpty(value))
				{
					Interface.Oxide.UnloadPlugin(value);
				}
			}
		}

		[HookMethod("UserGroupCommand")]
		private void UserGroupCommand(IPlayer player, string command, string[] args)
		{
			if (!PermissionsLoaded(player))
			{
				return;
			}
			if (args.Length < 3)
			{
				player.Reply(lang.GetMessage("CommandUsageUserGroup", this, player.Id));
				return;
			}
			string text = args[0];
			string text2 = Oxide.Core.ExtensionMethods.Sanitize(args[1]);
			string text3 = args[2];
			IPlayer[] array = Covalence.PlayerManager.FindPlayers(text2).ToArray();
			if (array.Length > 1)
			{
				player.Reply(string.Format(lang.GetMessage("PlayersFound", this, player.Id), string.Join(", ", array.Select((IPlayer p) => p.Name).ToArray())));
				return;
			}
			IPlayer player2 = ((array.Length == 1) ? array[0] : null);
			if (player2 == null && !permission.UserIdValid(text2))
			{
				player.Reply(string.Format(lang.GetMessage("PlayerNotFound", this, player.Id), text2));
				return;
			}
			string text4 = text2;
			if (player2 != null)
			{
				text4 = player2.Id;
				text2 = player2.Name;
				permission.UpdateNickname(text4, text2);
				text2 = text2 + "(" + text4 + ")";
			}
			if (!permission.GroupExists(text3))
			{
				player.Reply(string.Format(lang.GetMessage("GroupNotFound", this, player.Id), text3));
			}
			else if (text.Equals("add"))
			{
				permission.AddUserGroup(text4, text3);
				player.Reply(string.Format(lang.GetMessage("PlayerAddedToGroup", this, player.Id), text2, text3));
			}
			else if (text.Equals("remove"))
			{
				permission.RemoveUserGroup(text4, text3);
				player.Reply(string.Format(lang.GetMessage("PlayerRemovedFromGroup", this, player.Id), text2, text3));
			}
			else
			{
				player.Reply(lang.GetMessage("CommandUsageUserGroup", this, player.Id));
			}
		}

		[HookMethod("VersionCommand")]
		private void VersionCommand(IPlayer player)
		{
			if (player.IsServer)
			{
				string format = "Oxide.Rust Version: {0}\nOxide.Rust Branch: {1}";
				player.Reply(string.Format(format, RustExtension.AssemblyVersion, Extension.Branch));
				return;
			}
			string format2 = Covalence.FormatText(lang.GetMessage("Version", this, player.Id));
			player.Reply(string.Format(format2, RustExtension.AssemblyVersion, Covalence.GameName, Server.Version, Server.Protocol));
		}

		[HookMethod("SaveCommand")]
		private void SaveCommand(IPlayer player)
		{
			if (PermissionsLoaded(player) && player.IsAdmin)
			{
				Interface.Oxide.OnSave();
				Covalence.PlayerManager.SavePlayerData();
				player.Reply(lang.GetMessage("DataSaved", this, player.Id));
			}
		}

		public RustCore()
		{
			Extension = Interface.Oxide.GetExtension<RustExtension>();
			base.Title = "Rust";
			base.Author = Extension.Author;
			base.Version = Extension.Version;
		}

		private bool PermissionsLoaded(IPlayer player)
		{
			if (!permission.IsLoaded)
			{
				player.Reply(string.Format(lang.GetMessage("PermissionsNotLoaded", this, player.Id), permission.LastException.Message));
				return false;
			}
			return true;
		}

		[HookMethod("Init")]
		private void Init()
		{
			RemoteLogger.SetTag("game", base.Title.ToLower());
			RemoteLogger.SetTag("game version", Server.Version);
			AddCovalenceCommand(new string[3] { "oxide.plugins", "o.plugins", "plugins" }, "PluginsCommand", "oxide.plugins");
			AddCovalenceCommand(new string[3] { "oxide.load", "o.load", "plugin.load" }, "LoadCommand", "oxide.load");
			AddCovalenceCommand(new string[3] { "oxide.reload", "o.reload", "plugin.reload" }, "ReloadCommand", "oxide.reload");
			AddCovalenceCommand(new string[3] { "oxide.unload", "o.unload", "plugin.unload" }, "UnloadCommand", "oxide.unload");
			AddCovalenceCommand(new string[3] { "oxide.grant", "o.grant", "perm.grant" }, "GrantCommand", "oxide.grant");
			AddCovalenceCommand(new string[3] { "oxide.group", "o.group", "perm.group" }, "GroupCommand", "oxide.group");
			AddCovalenceCommand(new string[3] { "oxide.revoke", "o.revoke", "perm.revoke" }, "RevokeCommand", "oxide.revoke");
			AddCovalenceCommand(new string[3] { "oxide.show", "o.show", "perm.show" }, "ShowCommand", "oxide.show");
			AddCovalenceCommand(new string[3] { "oxide.usergroup", "o.usergroup", "perm.usergroup" }, "UserGroupCommand", "oxide.usergroup");
			AddCovalenceCommand(new string[3] { "oxide.lang", "o.lang", "lang" }, "LangCommand");
			AddCovalenceCommand(new string[2] { "oxide.save", "o.save" }, "SaveCommand");
			AddCovalenceCommand(new string[2] { "oxide.version", "o.version" }, "VersionCommand");
			foreach (KeyValuePair<string, Dictionary<string, string>> language in Localization.languages)
			{
				lang.RegisterMessages(language.Value, this, language.Key);
			}
			if (!permission.IsLoaded)
			{
				return;
			}
			int num = 0;
			foreach (string defaultGroup in Interface.Oxide.Config.Options.DefaultGroups)
			{
				if (!permission.GroupExists(defaultGroup))
				{
					permission.CreateGroup(defaultGroup, defaultGroup, num++);
				}
			}
			permission.RegisterValidate((string s) => ulong.TryParse(s, out var result) && ((result == 0L) ? 1 : ((int)Math.Floor(Math.Log10(result) + 1.0))) >= 17);
			permission.CleanUp();
		}

		[HookMethod("OnPluginLoaded")]
		private void OnPluginLoaded(Plugin plugin)
		{
			if (serverInitialized)
			{
				plugin.CallHook("OnServerInitialized", false);
			}
		}

		[HookMethod("IOnServerInitialized")]
		private void IOnServerInitialized()
		{
			if (!serverInitialized)
			{
				Analytics.Collect();
				if (!Interface.Oxide.Config.Options.Modded)
				{
					Interface.Oxide.LogWarning("The server is currently listed under Community. Please be aware that Facepunch only allows admin tools (that do not affect gameplay) under the Community section");
				}
				serverInitialized = true;
				Interface.CallHook("OnServerInitialized", serverInitialized);
			}
		}

		[HookMethod("OnServerSave")]
		private void OnServerSave()
		{
			Interface.Oxide.OnSave();
			Covalence.PlayerManager.SavePlayerData();
		}

		[HookMethod("IOnServerShutdown")]
		private void IOnServerShutdown()
		{
			Interface.Oxide.CallHook("OnServerShutdown");
			Interface.Oxide.OnShutdown();
			Covalence.PlayerManager.SavePlayerData();
		}

		private void ParseCommand(string argstr, out string command, out string[] args)
		{
			List<string> list = new List<string>();
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			foreach (char c in argstr)
			{
				if (c == '"')
				{
					if (flag)
					{
						string text = stringBuilder.ToString().Trim();
						if (!string.IsNullOrEmpty(text))
						{
							list.Add(text);
						}
						stringBuilder.Clear();
						flag = false;
					}
					else
					{
						flag = true;
					}
				}
				else if (char.IsWhiteSpace(c) && !flag)
				{
					string text2 = stringBuilder.ToString().Trim();
					if (!string.IsNullOrEmpty(text2))
					{
						list.Add(text2);
					}
					stringBuilder.Clear();
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			if (stringBuilder.Length > 0)
			{
				string text3 = stringBuilder.ToString().Trim();
				if (!string.IsNullOrEmpty(text3))
				{
					list.Add(text3);
				}
			}
			if (list.Count == 0)
			{
				command = null;
				args = null;
			}
			else
			{
				command = list[0];
				list.RemoveAt(0);
				args = list.ToArray();
			}
		}

		public static BasePlayer FindPlayer(string nameOrIdOrIp)
		{
			BasePlayer result = null;
			foreach (BasePlayer activePlayer in BasePlayer.activePlayerList)
			{
				if (string.IsNullOrEmpty(activePlayer.UserIDString))
				{
					continue;
				}
				if (activePlayer.UserIDString.Equals(nameOrIdOrIp))
				{
					return activePlayer;
				}
				if (!string.IsNullOrEmpty(activePlayer.displayName))
				{
					if (activePlayer.displayName.Equals(nameOrIdOrIp, StringComparison.OrdinalIgnoreCase))
					{
						return activePlayer;
					}
					if (activePlayer.displayName.Contains(nameOrIdOrIp, CompareOptions.OrdinalIgnoreCase))
					{
						result = activePlayer;
					}
					if (activePlayer.net?.connection != null && activePlayer.net.connection.ipaddress.Equals(nameOrIdOrIp))
					{
						return activePlayer;
					}
					if (activePlayer.net?.connection != null && activePlayer.net.ID.Equals(nameOrIdOrIp))
					{
						return activePlayer;
					}
				}
			}
			foreach (BasePlayer sleepingPlayer in BasePlayer.sleepingPlayerList)
			{
				if (string.IsNullOrEmpty(sleepingPlayer.UserIDString))
				{
					continue;
				}
				if (sleepingPlayer.UserIDString.Equals(nameOrIdOrIp))
				{
					return sleepingPlayer;
				}
				if (!string.IsNullOrEmpty(sleepingPlayer.displayName))
				{
					if (sleepingPlayer.displayName.Equals(nameOrIdOrIp, StringComparison.OrdinalIgnoreCase))
					{
						return sleepingPlayer;
					}
					if (sleepingPlayer.displayName.Contains(nameOrIdOrIp, CompareOptions.OrdinalIgnoreCase))
					{
						result = sleepingPlayer;
					}
				}
			}
			return result;
		}

		public static BasePlayer FindPlayerByName(string name)
		{
			BasePlayer result = null;
			foreach (BasePlayer activePlayer in BasePlayer.activePlayerList)
			{
				if (!string.IsNullOrEmpty(activePlayer.displayName))
				{
					if (activePlayer.displayName.Equals(name, StringComparison.OrdinalIgnoreCase))
					{
						return activePlayer;
					}
					if (activePlayer.displayName.Contains(name, CompareOptions.OrdinalIgnoreCase))
					{
						result = activePlayer;
					}
				}
			}
			foreach (BasePlayer sleepingPlayer in BasePlayer.sleepingPlayerList)
			{
				if (!string.IsNullOrEmpty(sleepingPlayer.displayName))
				{
					if (sleepingPlayer.displayName.Equals(name, StringComparison.OrdinalIgnoreCase))
					{
						return sleepingPlayer;
					}
					if (sleepingPlayer.displayName.Contains(name, CompareOptions.OrdinalIgnoreCase))
					{
						result = sleepingPlayer;
					}
				}
			}
			return result;
		}

		public static BasePlayer FindPlayerById(ulong id)
		{
			foreach (BasePlayer activePlayer in BasePlayer.activePlayerList)
			{
				if ((ulong)activePlayer.userID == id)
				{
					return activePlayer;
				}
			}
			foreach (BasePlayer sleepingPlayer in BasePlayer.sleepingPlayerList)
			{
				if ((ulong)sleepingPlayer.userID == id)
				{
					return sleepingPlayer;
				}
			}
			return null;
		}

		public static BasePlayer FindPlayerByIdString(string id)
		{
			foreach (BasePlayer activePlayer in BasePlayer.activePlayerList)
			{
				if (!string.IsNullOrEmpty(activePlayer.UserIDString) && activePlayer.UserIDString.Equals(id))
				{
					return activePlayer;
				}
			}
			foreach (BasePlayer sleepingPlayer in BasePlayer.sleepingPlayerList)
			{
				if (!string.IsNullOrEmpty(sleepingPlayer.UserIDString) && sleepingPlayer.UserIDString.Equals(id))
				{
					return sleepingPlayer;
				}
			}
			return null;
		}

		[HookMethod("IOnBaseCombatEntityHurt")]
		private object IOnBaseCombatEntityHurt(BaseCombatEntity entity, HitInfo hitInfo)
		{
			if (!(entity is BasePlayer))
			{
				return Interface.CallHook("OnEntityTakeDamage", entity, hitInfo);
			}
			return null;
		}

		[HookMethod("IOnNpcTarget")]
		private object IOnNpcTarget(BaseNpc npc, BaseEntity target)
		{
			if (Interface.CallHook("OnNpcTarget", npc, target) != null)
			{
				npc.SetFact(BaseNpc.Facts.HasEnemy, 0);
				npc.SetFact(BaseNpc.Facts.EnemyRange, 3);
				npc.SetFact(BaseNpc.Facts.AfraidRange, 1);
				npc.playerTargetDecisionStartTime = 0f;
				return 0f;
			}
			return null;
		}

		[HookMethod("IOnNpcTarget")]
		private object IOnNpcTarget(SenseComponent sense, BaseEntity target)
		{
			if (!sense || !target)
			{
				return null;
			}
			BaseEntity baseEntity = sense.baseEntity;
			if (!baseEntity)
			{
				return null;
			}
			if (Interface.CallHook("OnNpcTarget", baseEntity, target) != null)
			{
				return false;
			}
			return null;
		}

		[HookMethod("IOnEntitySaved")]
		private void IOnEntitySaved(BaseNetworkable baseNetworkable, BaseNetworkable.SaveInfo saveInfo)
		{
			if (serverInitialized && saveInfo.forConnection != null)
			{
				Interface.CallHook("OnEntitySaved", baseNetworkable, saveInfo);
			}
		}

		[HookMethod("IOnLoseCondition")]
		private object IOnLoseCondition(Item item, float amount)
		{
			object[] array = new object[2] { item, amount };
			Interface.CallHook("OnLoseCondition", array);
			amount = (float)array[1];
			float condition = item.condition;
			item.condition -= amount;
			if (item.condition <= 0f && item.condition < condition)
			{
				item.OnBroken();
			}
			return true;
		}

		[HookMethod("ICanPickupEntity")]
		private object ICanPickupEntity(BasePlayer basePlayer, BaseEntity entity)
		{
			object obj = Interface.CallHook("CanPickupEntity", basePlayer, entity);
			if (!(obj is bool) || (bool)obj)
			{
				return null;
			}
			return true;
		}

		[HookMethod("IOnBasePlayerAttacked")]
		private object IOnBasePlayerAttacked(BasePlayer basePlayer, HitInfo hitInfo)
		{
			if (!serverInitialized || basePlayer == null || hitInfo == null || basePlayer.IsDead() || isPlayerTakingDamage || basePlayer is NPCPlayer)
			{
				return null;
			}
			if (Interface.CallHook("OnEntityTakeDamage", basePlayer, hitInfo) != null)
			{
				return true;
			}
			isPlayerTakingDamage = true;
			try
			{
				basePlayer.OnAttacked(hitInfo);
			}
			finally
			{
				isPlayerTakingDamage = false;
			}
			return true;
		}

		[HookMethod("IOnBasePlayerHurt")]
		private object IOnBasePlayerHurt(BasePlayer basePlayer, HitInfo hitInfo)
		{
			if (!isPlayerTakingDamage)
			{
				return Interface.CallHook("OnEntityTakeDamage", basePlayer, hitInfo);
			}
			return null;
		}

		[HookMethod("OnServerUserSet")]
		private void OnServerUserSet(ulong steamId, ServerUsers.UserGroup group, string playerName, string reason, long expiry)
		{
			if (serverInitialized && group == ServerUsers.UserGroup.Banned)
			{
				string text = steamId.ToString();
				IPlayer player = Covalence.PlayerManager.FindPlayerById(text);
				Interface.CallHook("OnPlayerBanned", playerName, steamId, player?.Address ?? "0", reason, expiry);
				Interface.CallHook("OnUserBanned", playerName, text, player?.Address ?? "0", reason, expiry);
			}
		}

		[HookMethod("OnServerUserRemove")]
		private void OnServerUserRemove(ulong steamId)
		{
			if (serverInitialized && ServerUsers.users.ContainsKey(steamId) && ServerUsers.users[steamId].group == ServerUsers.UserGroup.Banned)
			{
				string text = steamId.ToString();
				IPlayer player = Covalence.PlayerManager.FindPlayerById(text);
				Interface.CallHook("OnPlayerUnbanned", player?.Name ?? "Unnamed", steamId, player?.Address ?? "0");
				Interface.CallHook("OnUserUnbanned", player?.Name ?? "Unnamed", text, player?.Address ?? "0");
			}
		}

		[HookMethod("IOnUserApprove")]
		private object IOnUserApprove(Connection connection)
		{
			string username = connection.username;
			string text = connection.userid.ToString();
			string obj = Regex.Replace(connection.ipaddress, ipPattern, "");
			uint authLevel = connection.authLevel;
			if (permission.IsLoaded)
			{
				permission.UpdateNickname(text, username);
				OxideConfig.DefaultGroups defaultGroups = Interface.Oxide.Config.Options.DefaultGroups;
				if (!permission.UserHasGroup(text, defaultGroups.Players))
				{
					permission.AddUserGroup(text, defaultGroups.Players);
				}
				if (authLevel >= 2 && !permission.UserHasGroup(text, defaultGroups.Administrators))
				{
					permission.AddUserGroup(text, defaultGroups.Administrators);
				}
			}
			Covalence.PlayerManager.PlayerJoin(connection.userid, username);
			object obj2 = Interface.CallHook("CanClientLogin", connection);
			object obj3 = Interface.CallHook("CanUserLogin", username, text, obj);
			object obj4 = ((obj2 == null) ? obj3 : obj2);
			if (obj4 is string || (obj4 is bool && !(bool)obj4))
			{
				ConnectionAuth.Reject(connection, (obj4 is string) ? obj4.ToString() : lang.GetMessage("ConnectionRejected", this, text));
				return true;
			}
			object obj5 = Interface.CallHook("OnUserApprove", connection);
			object result = Interface.CallHook("OnUserApproved", username, text, obj);
			if (obj5 != null)
			{
				return obj5;
			}
			return result;
		}

		[HookMethod("IOnPlayerBanned")]
		private void IOnPlayerBanned(Connection connection, AuthResponse status)
		{
			Interface.CallHook("OnPlayerBanned", connection, status.ToString());
		}

		[HookMethod("IOnPlayerChat")]
		private object IOnPlayerChat(ulong playerId, string playerName, string message, Chat.ChatChannel channel, BasePlayer basePlayer)
		{
			if (string.IsNullOrEmpty(message) || message.Equals("text"))
			{
				return true;
			}
			string chatCommandPrefix = CommandHandler.GetChatCommandPrefix(message);
			if (chatCommandPrefix != null)
			{
				TryRunPlayerCommand(basePlayer, message, chatCommandPrefix);
				return false;
			}
			message = message.EscapeRichText();
			if (basePlayer == null || !basePlayer.IsConnected)
			{
				return Interface.CallHook("OnPlayerOfflineChat", playerId, playerName, message, channel);
			}
			object obj = Interface.CallHook("OnPlayerChat", basePlayer, message, channel);
			object result = Interface.CallHook("OnUserChat", basePlayer.IPlayer, message);
			if (obj != null)
			{
				return obj;
			}
			return result;
		}

		private void TryRunPlayerCommand(BasePlayer basePlayer, string message, string commandPrefix)
		{
			if (basePlayer == null)
			{
				return;
			}
			string text = message.Replace("\n", "").Replace("\r", "").Trim();
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			ParseCommand(text.Substring(commandPrefix.Length), out var command, out var args);
			if (command == null)
			{
				return;
			}
			if (!basePlayer.IsConnected)
			{
				Interface.CallHook("OnApplicationCommand", basePlayer, command, args);
				Interface.CallHook("OnApplicationCommand", basePlayer.IPlayer, command, args);
				return;
			}
			object obj = Interface.CallHook("OnPlayerCommand", basePlayer, command, args);
			object obj2 = Interface.CallHook("OnUserCommand", basePlayer.IPlayer, command, args);
			if (((obj == null) ? obj2 : obj) != null)
			{
				return;
			}
			try
			{
				if (!Covalence.CommandSystem.HandleChatMessage(basePlayer.IPlayer, text) && !cmdlib.HandleChatCommand(basePlayer, command, args) && Interface.Oxide.Config.Options.Modded)
				{
					basePlayer.IPlayer.Reply(string.Format(lang.GetMessage("UnknownCommand", this, basePlayer.IPlayer.Id), command));
				}
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				string text2 = string.Empty;
				string empty = string.Empty;
				StringBuilder stringBuilder = new StringBuilder();
				while (ex2 != null)
				{
					string text3 = ex2.GetType().Name;
					text2 = (text3 + ": " + ex2.Message).TrimEnd(' ', ':');
					stringBuilder.AppendLine(ex2.StackTrace);
					if (ex2.InnerException != null)
					{
						stringBuilder.AppendLine("Rethrow as " + text3);
					}
					ex2 = ex2.InnerException;
				}
				StackTrace stackTrace = new StackTrace(ex, 0, fNeedFileInfo: true);
				for (int i = 0; i < stackTrace.FrameCount; i++)
				{
					MethodBase method = stackTrace.GetFrame(i).GetMethod();
					if ((object)method != null && (object)method.DeclaringType != null && method.DeclaringType.Namespace == "Oxide.Plugins")
					{
						empty = method.DeclaringType.Name;
					}
				}
				Interface.Oxide.LogError(string.Format("Failed to run command '/{0}' on plugin '{1}'. ({2}){3}{4}", command, empty, text2.Replace(System.Environment.NewLine, " "), System.Environment.NewLine, stackTrace));
			}
		}

		[HookMethod("OnClientAuth")]
		private void OnClientAuth(Connection connection)
		{
			connection.username = Regex.Replace(connection.username, "<[^>]*>", string.Empty);
		}

		[HookMethod("IOnPlayerConnected")]
		private void IOnPlayerConnected(BasePlayer basePlayer)
		{
			lang.SetLanguage(basePlayer.net.connection.info.GetString("global.language", "en"), basePlayer.UserIDString);
			basePlayer.SendEntitySnapshot(CommunityEntity.ServerInstance);
			Covalence.PlayerManager.PlayerConnected(basePlayer);
			IPlayer player = Covalence.PlayerManager.FindPlayerById(basePlayer.UserIDString);
			if (player != null)
			{
				basePlayer.IPlayer = player;
				Interface.CallHook("OnUserConnected", player);
			}
			Interface.Oxide.CallHook("OnPlayerConnected", basePlayer);
		}

		[HookMethod("OnPlayerDisconnected")]
		private void OnPlayerDisconnected(BasePlayer basePlayer, string reason)
		{
			IPlayer iPlayer = basePlayer.IPlayer;
			if (iPlayer != null)
			{
				Interface.CallHook("OnUserDisconnected", iPlayer, reason);
			}
			Covalence.PlayerManager.PlayerDisconnected(basePlayer);
		}

		[HookMethod("OnPlayerSetInfo")]
		private void OnPlayerSetInfo(Connection connection, string key, string val)
		{
			if (!(key == "global.language"))
			{
				return;
			}
			lang.SetLanguage(val, connection.userid.ToString());
			BasePlayer basePlayer = connection.player as BasePlayer;
			if (basePlayer != null)
			{
				Interface.CallHook("OnPlayerLanguageChanged", basePlayer, val);
				if (basePlayer.IPlayer != null)
				{
					Interface.CallHook("OnPlayerLanguageChanged", basePlayer.IPlayer, val);
				}
			}
		}

		[HookMethod("OnPlayerKicked")]
		private void OnPlayerKicked(BasePlayer basePlayer, string reason)
		{
			if (basePlayer.IPlayer != null)
			{
				Interface.CallHook("OnUserKicked", basePlayer.IPlayer, reason);
			}
		}

		[HookMethod("OnPlayerRespawn")]
		private object OnPlayerRespawn(BasePlayer basePlayer)
		{
			IPlayer iPlayer = basePlayer.IPlayer;
			if (iPlayer == null)
			{
				return null;
			}
			return Interface.CallHook("OnUserRespawn", iPlayer);
		}

		[HookMethod("OnPlayerRespawned")]
		private void OnPlayerRespawned(BasePlayer basePlayer)
		{
			IPlayer iPlayer = basePlayer.IPlayer;
			if (iPlayer != null)
			{
				Interface.CallHook("OnUserRespawned", iPlayer);
			}
		}

		[HookMethod("IOnRconMessage")]
		private object IOnRconMessage(IPAddress ipAddress, string command)
		{
			if (ipAddress != null && !string.IsNullOrEmpty(command))
			{
				RemoteMessage message = RemoteMessage.GetMessage(command);
				if (string.IsNullOrEmpty(message?.Message))
				{
					return null;
				}
				if (Interface.CallHook("OnRconMessage", ipAddress, message) != null)
				{
					return true;
				}
				string[] array = Oxide.Core.CommandLine.Split(message.Message);
				if (array.Length >= 1)
				{
					string obj = array[0].ToLower();
					string[] obj2 = array.Skip(1).ToArray();
					if (Interface.CallHook("OnRconCommand", ipAddress, obj, obj2) != null)
					{
						return true;
					}
				}
			}
			return null;
		}

		[HookMethod("IOnRconInitialize")]
		private object IOnRconInitialize()
		{
			if (!Interface.Oxide.Config.Rcon.Enabled)
			{
				return null;
			}
			return true;
		}

		[HookMethod("IOnRunCommandLine")]
		private object IOnRunCommandLine()
		{
			foreach (KeyValuePair<string, string> @switch in Facepunch.CommandLine.GetSwitches())
			{
				string text = @switch.Value;
				if (text == "")
				{
					text = "1";
				}
				string strCommand = @switch.Key.Substring(1);
				ConsoleSystem.Option unrestricted = ConsoleSystem.Option.Unrestricted;
				unrestricted.PrintOutput = false;
				ConsoleSystem.Run(unrestricted, strCommand, text);
			}
			return false;
		}

		[HookMethod("IOnServerCommand")]
		private object IOnServerCommand(ConsoleSystem.Arg arg)
		{
			if (arg == null || (arg.Connection != null && arg.Player() == null))
			{
				return true;
			}
			if (arg.cmd.FullName == "chat.say" || arg.cmd.FullName == "chat.teamsay" || arg.cmd.FullName == "chat.localsay")
			{
				return null;
			}
			object obj = Interface.CallHook("OnServerCommand", arg);
			object obj2 = Interface.CallHook("OnServerCommand", arg.cmd.FullName, RustCommandSystem.ExtractArgs(arg));
			if (((obj == null) ? obj2 : obj) != null)
			{
				return true;
			}
			return null;
		}

		[HookMethod("OnServerInformationUpdated")]
		private void OnServerInformationUpdated()
		{
			SteamServer.GameTags += ",^o";
			if (Interface.Oxide.Config.Options.Modded)
			{
				SteamServer.GameTags += "^z";
			}
		}

		[HookMethod("IOnCupboardAuthorize")]
		private object IOnCupboardAuthorize(ulong userID, BasePlayer player, BuildingPrivlidge privlidge)
		{
			if (userID == (ulong)player.userID)
			{
				if (Interface.CallHook("OnCupboardAuthorize", privlidge, player) != null)
				{
					return true;
				}
			}
			else if (Interface.CallHook("OnCupboardAssign", privlidge, userID, player) != null)
			{
				return true;
			}
			return null;
		}

		[HookMethod("OnTeamMemberPromote")]
		private void OnTeamMemberPromote(RelationshipManager.PlayerTeam team, ulong newTeamLeader)
		{
			BasePlayer basePlayer = BasePlayer.FindByID(newTeamLeader);
			if (basePlayer != null)
			{
				Interface.Oxide.CallDeprecatedHook("OnTeamPromote", "OnTeamMemberPromote(PlayerTeam team, ulong userId)", Eoy, team, basePlayer);
			}
		}

		[HookMethod("IOnTeamInvite")]
		private object IOnTeamInvite(BasePlayer basePlayer, BasePlayer basePlayer2)
		{
			return Interface.Oxide.CallDeprecatedHook("OnTeamInvite", "OnTeamMemberInvite(PlayerTeam playerTeam, BasePlayer basePlayer, ulong userId)", Eoy, basePlayer, basePlayer2);
		}
	}
	public class RustExtension : Extension
	{
		private const string OxideRustReleaseListUrl = "https://api.github.com/repos/OxideMod/Oxide.Rust/releases";

		internal static Assembly Assembly = Assembly.GetExecutingAssembly();

		internal static AssemblyName AssemblyName = Assembly.GetName();

		internal static VersionNumber AssemblyVersion = new VersionNumber(AssemblyName.Version.Major, AssemblyName.Version.Minor, AssemblyName.Version.Build);

		internal static string AssemblyAuthors = ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(Assembly, typeof(AssemblyCompanyAttribute), inherit: false)).Company;

		private static readonly WebClient WebClient = new WebClient();

		private static VersionNumber LatestExtVersion = AssemblyVersion;

		public static string[] Filter = new string[18]
		{
			"alphamapResolution is clamped to the range of", "AngryAnt Behave version", "Floating point textures aren't supported on this device", "HDR RenderTexture format is not supported on this platform.", "Image Effects are not supported on this platform.", "Missing projectileID", "Motion vectors not supported on a platform that does not support", "The image effect Main Camera", "The image effect effect -", "Unable to find shaders",
			"Unsupported encoding: 'utf8'", "Warning, null renderer for ScaleRenderer!", "[AmplifyColor]", "[AmplifyOcclusion]", "[CoverageQueries] Disabled due to unsupported", "[CustomProbe]", "[Manifest] URI IS", "[SpawnHandler] populationCounts"
		};

		public override bool IsGameExtension => true;

		public override string Name => "Rust";

		public override string Author => AssemblyAuthors;

		public override VersionNumber Version => AssemblyVersion;

		public override string[] DefaultReferences => new string[35]
		{
			"0Harmony", "Facepunch.Network", "Facepunch.Steamworks.Posix", "Facepunch.System", "Facepunch.UnityEngine", "Facepunch.Steamworks.Win64", "Rust.Data", "Rust.FileSystem", "Rust.Clans", "Rust.Clans.Local",
			"Rust.Global", "Rust.Localization", "Rust.Platform", "Rust.Platform.Common", "Rust.Platform.Steam", "Rust.Workshop", "Rust.World", "System.Drawing", "UnityEngine.AIModule", "UnityEngine.AssetBundleModule",
			"UnityEngine.CoreModule", "UnityEngine.GridModule", "UnityEngine.ImageConversionModule", "UnityEngine.PhysicsModule", "UnityEngine.TerrainModule", "UnityEngine.TerrainPhysicsModule", "UnityEngine.UI", "UnityEngine.UIModule", "UnityEngine.UIElementsModule", "UnityEngine.UnityWebRequestAudioModule",
			"UnityEngine.UnityWebRequestModule", "UnityEngine.UnityWebRequestTextureModule", "UnityEngine.UnityWebRequestWWWModule", "UnityEngine.VehiclesModule", "netstandard"
		};

		public override string[] WhitelistAssemblies => new string[20]
		{
			"Assembly-CSharp", "Assembly-CSharp-firstpass", "DestMath", "Facepunch.Network", "Facepunch.System", "Facepunch.UnityEngine", "mscorlib", "Oxide.Core", "Oxide.Rust", "RustBuild",
			"Rust.Data", "Rust.FileSystem", "Rust.Global", "Rust.Localization", "Rust.Localization", "Rust.Platform.Common", "Rust.Platform.Steam", "System", "System.Core", "UnityEngine"
		};

		public override string[] WhitelistNamespaces => new string[14]
		{
			"ConVar", "Dest", "Facepunch", "Network", "Oxide.Game.Rust.Cui", "ProtoBuf", "PVT", "Rust", "Steamworks", "System.Collections",
			"System.Security.Cryptography", "System.Text", "System.Threading.Monitor", "UnityEngine"
		};

		public RustExtension(ExtensionManager manager)
			: base(manager)
		{
		}

		public override void Load()
		{
			base.Manager.RegisterLibrary("Rust", new Oxide.Game.Rust.Libraries.Rust());
			base.Manager.RegisterLibrary("Command", new Command());
			base.Manager.RegisterLibrary("Item", new Oxide.Game.Rust.Libraries.Item());
			base.Manager.RegisterLibrary("Player", new Oxide.Game.Rust.Libraries.Player());
			base.Manager.RegisterLibrary("Server", new Oxide.Game.Rust.Libraries.Server());
			base.Manager.RegisterPluginLoader(new RustPluginLoader());
			if (System.Environment.OSVersion.Platform == PlatformID.Unix)
			{
				Cleanup.Add("Facepunch.Steamworks.Win64.dll");
			}
			WebClient.Headers["User-Agent"] = $"Oxide.Rust {Version}";
		}

		public override void LoadPluginWatchers(string directory)
		{
		}

		public override void OnModLoad()
		{
			CSharpPluginLoader.PluginReferences.UnionWith(DefaultReferences);
		}

		public void GetLatestVersion(Action<VersionNumber, Exception> callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback", "Callback cannot be null");
			}
			if (LatestExtVersion > AssemblyVersion)
			{
				callback(LatestExtVersion, null);
				return;
			}
			GetLatestExtensionVersion().ContinueWith(delegate(Task<VersionNumber> task)
			{
				if (task.Exception == null)
				{
					LatestExtVersion = task.Result;
				}
				callback(LatestExtVersion, task.Exception?.InnerException);
			});
		}

		private async Task<VersionNumber> GetLatestExtensionVersion()
		{
			string obj = await WebClient.DownloadStringTaskAsync("https://api.github.com/repos/OxideMod/Oxide.Rust/releases");
			if (string.IsNullOrWhiteSpace(obj))
			{
				throw new Exception("Could not retrieve latest Oxide.Rust version from GitHub API");
			}
			string text = JSON.Array.Parse(obj)[0].Obj.GetString("tag_name");
			if (string.IsNullOrWhiteSpace(text))
			{
				throw new Exception("Tag name is undefined");
			}
			return ParseVersionNumber(text);
		}

		private VersionNumber ParseVersionNumber(string versionString)
		{
			string[] array = versionString.Split(new char[1] { '.' }, StringSplitOptions.RemoveEmptyEntries);
			int major = int.Parse(array[0]);
			int minor = int.Parse(array[1]);
			int patch = int.Parse(array[2]);
			return new VersionNumber(major, minor, patch);
		}
	}
	public class RustPluginLoader : PluginLoader
	{
		public override Type[] CorePlugins => new Type[1] { typeof(RustCore) };
	}
}
namespace Oxide.Game.Rust.Cui
{
	public sealed class JsonArrayPool<T> : IArrayPool<T>
	{
		public static readonly JsonArrayPool<T> Shared = new JsonArrayPool<T>();

		private static readonly IArrayPoolProvider<T> Provider = GetOrCreateProvider();

		private static IArrayPoolProvider<T> GetOrCreateProvider()
		{
			if (Interface.Oxide.PoolFactory.IsHandledType<T[]>())
			{
				return Interface.Oxide.PoolFactory.GetArrayProvider<T>();
			}
			Interface.Oxide.PoolFactory.RegisterProvider<BaseArrayPoolProvider<T>>(out var provider, 1000, 16384);
			return provider;
		}

		public T[] Rent(int minimumLength)
		{
			return Provider.Take(minimumLength);
		}

		public void Return(T[] array)
		{
			Provider.Return(array);
		}
	}
	public static class CuiHelper
	{
		private class JsonWriterResources
		{
			public readonly StringBuilder StringBuilder = new StringBuilder(65536);

			public readonly StringWriter StringWriter;

			public readonly JsonTextWriter JsonWriter;

			public readonly JsonSerializer Serializer;

			public JsonWriterResources()
			{
				StringWriter = new StringWriter(StringBuilder, CultureInfo.InvariantCulture);
				JsonWriter = new JsonTextWriter(StringWriter)
				{
					ArrayPool = JsonArrayPool<char>.Shared,
					CloseOutput = false
				};
				Serializer = JsonSerializer.Create(Settings);
			}

			public void Reset(bool format = false)
			{
				StringBuilder.Clear();
				JsonWriter.Formatting = (format ? Formatting.Indented : Formatting.None);
			}
		}

		private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
		{
			DefaultValueHandling = DefaultValueHandling.Ignore,
			NullValueHandling = NullValueHandling.Ignore,
			DateParseHandling = DateParseHandling.None,
			FloatFormatHandling = FloatFormatHandling.Symbol,
			StringEscapeHandling = StringEscapeHandling.Default
		};

		private static readonly ThreadLocal<JsonWriterResources> SharedWriterResources = new ThreadLocal<JsonWriterResources>(() => new JsonWriterResources());

		public static string ToJson(IReadOnlyList<CuiElement> elements, bool format = false)
		{
			JsonWriterResources value = SharedWriterResources.Value;
			value.Reset(format);
			value.Serializer.Serialize(value.JsonWriter, elements);
			value.JsonWriter.Flush();
			return value.StringBuilder.ToString().Replace("\\n", "\n");
		}

		public static List<CuiElement> FromJson(string json)
		{
			return JsonConvert.DeserializeObject<List<CuiElement>>(json);
		}

		public static string GetGuid()
		{
			return Guid.NewGuid().ToString("N");
		}

		public static bool AddUi(BasePlayer player, List<CuiElement> elements)
		{
			if (player?.net == null)
			{
				return false;
			}
			return AddUi(player, ToJson(elements));
		}

		public static bool AddUi(BasePlayer player, string json)
		{
			if (player?.net != null && Interface.CallHook("CanUseUI", player, json) == null)
			{
				CommunityEntity.ServerInstance.ClientRPC(RpcTarget.Player("AddUI", player.net.connection), json);
				return true;
			}
			return false;
		}

		public static bool DestroyUi(BasePlayer player, string elem)
		{
			if (player?.net != null)
			{
				Interface.CallHook("OnDestroyUI", player, elem);
				CommunityEntity.ServerInstance.ClientRPC(RpcTarget.Player("DestroyUI", player.net.connection), elem);
				return true;
			}
			return false;
		}

		public static void SetColor(this ICuiColor elem, Color color)
		{
			elem.Color = $"{color.r} {color.g} {color.b} {color.a}";
		}

		public static Color GetColor(this ICuiColor elem)
		{
			return ColorEx.Parse(elem.Color);
		}
	}
	public class CuiElementContainer : List<CuiElement>
	{
		public string Add(CuiButton button, string parent = "Hud", string name = null, string destroyUi = null)
		{
			if (string.IsNullOrEmpty(name))
			{
				name = CuiHelper.GetGuid();
			}
			Add(new CuiElement
			{
				Name = name,
				Parent = parent,
				FadeOut = button.FadeOut,
				DestroyUi = destroyUi,
				Components = 
				{
					(ICuiComponent)button.Button,
					(ICuiComponent)button.RectTransform
				}
			});
			if (!string.IsNullOrEmpty(button.Text.Text))
			{
				Add(new CuiElement
				{
					Parent = name,
					FadeOut = button.FadeOut,
					Components = 
					{
						(ICuiComponent)button.Text,
						(ICuiComponent)new CuiRectTransformComponent()
					}
				});
			}
			return name;
		}

		public string Add(CuiLabel label, string parent = "Hud", string name = null, string destroyUi = null)
		{
			if (string.IsNullOrEmpty(name))
			{
				name = CuiHelper.GetGuid();
			}
			Add(new CuiElement
			{
				Name = name,
				Parent = parent,
				FadeOut = label.FadeOut,
				DestroyUi = destroyUi,
				Components = 
				{
					(ICuiComponent)label.Text,
					(ICuiComponent)label.RectTransform
				}
			});
			return name;
		}

		public string Add(CuiPanel panel, string parent = "Hud", string name = null, string destroyUi = null)
		{
			if (string.IsNullOrEmpty(name))
			{
				name = CuiHelper.GetGuid();
			}
			CuiElement cuiElement = new CuiElement
			{
				Name = name,
				Parent = parent,
				FadeOut = panel.FadeOut,
				DestroyUi = destroyUi
			};
			if (panel.Image != null)
			{
				cuiElement.Components.Add(panel.Image);
			}
			if (panel.RawImage != null)
			{
				cuiElement.Components.Add(panel.RawImage);
			}
			cuiElement.Components.Add(panel.RectTransform);
			if (panel.CursorEnabled)
			{
				cuiElement.Components.Add(new CuiNeedsCursorComponent());
			}
			if (panel.KeyboardEnabled)
			{
				cuiElement.Components.Add(new CuiNeedsKeyboardComponent());
			}
			Add(cuiElement);
			return name;
		}

		public string ToJson()
		{
			return ToString();
		}

		public override string ToString()
		{
			return CuiHelper.ToJson(this);
		}
	}
	public class CuiButton
	{
		public CuiButtonComponent Button { get; } = new CuiButtonComponent();

		public CuiRectTransformComponent RectTransform { get; } = new CuiRectTransformComponent();

		public CuiTextComponent Text { get; } = new CuiTextComponent();

		public float FadeOut { get; set; }
	}
	public class CuiPanel
	{
		public CuiImageComponent Image { get; set; } = new CuiImageComponent();

		public CuiRawImageComponent RawImage { get; set; }

		public CuiRectTransformComponent RectTransform { get; } = new CuiRectTransformComponent();

		public bool CursorEnabled { get; set; }

		public bool KeyboardEnabled { get; set; }

		public float FadeOut { get; set; }
	}
	public class CuiLabel
	{
		public CuiTextComponent Text { get; } = new CuiTextComponent();

		public CuiRectTransformComponent RectTransform { get; } = new CuiRectTransformComponent();

		public float FadeOut { get; set; }
	}
	public class CuiElement
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("parent")]
		public string Parent { get; set; }

		[JsonProperty("destroyUi")]
		public string DestroyUi { get; set; }

		[JsonProperty("components")]
		public List<ICuiComponent> Components { get; } = new List<ICuiComponent>();

		[JsonProperty("fadeOut")]
		public float FadeOut { get; set; }

		[JsonProperty("update")]
		public bool Update { get; set; }

		[JsonProperty("activeSelf")]
		public bool? ActiveSelf { get; set; }
	}
	[JsonConverter(typeof(ComponentConverter))]
	public interface ICuiComponent
	{
		[JsonProperty("type")]
		string Type { get; }
	}
	public interface ICuiGraphic
	{
		[JsonProperty("fadeIn")]
		float FadeIn { get; set; }

		[JsonProperty("placeholderParentId")]
		string PlaceholderParentId { get; set; }
	}
	public interface ICuiColor
	{
		[JsonProperty("color")]
		string Color { get; set; }
	}
	public interface ICuiEnableable
	{
		[JsonProperty("enabled")]
		bool? Enabled { get; set; }
	}
	public class CuiTextComponent : ICuiComponent, ICuiColor, ICuiEnableable, ICuiGraphic
	{
		public string Type => "UnityEngine.UI.Text";

		[JsonProperty("text")]
		public string Text { get; set; }

		[JsonProperty("fontSize")]
		public int FontSize { get; set; }

		[JsonProperty("font")]
		public string Font { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		[JsonProperty("align")]
		public TextAnchor Align { get; set; }

		public string Color { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		[JsonProperty("verticalOverflow")]
		public VerticalWrapMode VerticalOverflow { get; set; }

		public float FadeIn { get; set; }

		public string PlaceholderParentId { get; set; }

		public bool? Enabled { get; set; }
	}
	public class CuiImageComponent : ICuiComponent, ICuiColor, ICuiEnableable, ICuiGraphic
	{
		public string Type => "UnityEngine.UI.Image";

		[JsonProperty("sprite")]
		public string Sprite { get; set; }

		[JsonProperty("material")]
		public string Material { get; set; }

		public string Color { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		[JsonProperty("imagetype")]
		public Image.Type ImageType { get; set; }

		[JsonProperty("fillCenter")]
		public bool? FillCenter { get; set; }

		[JsonProperty("png")]
		public string Png { get; set; }

		[JsonProperty("slice")]
		public string Slice { get; set; }

		[JsonProperty("itemid")]
		public int ItemId { get; set; }

		[JsonProperty("skinid")]
		public ulong SkinId { get; set; }

		public float FadeIn { get; set; }

		public string PlaceholderParentId { get; set; }

		public bool? Enabled { get; set; }
	}
	public class CuiRawImageComponent : ICuiComponent, ICuiColor, ICuiEnableable, ICuiGraphic
	{
		public string Type => "UnityEngine.UI.RawImage";

		[JsonProperty("sprite")]
		public string Sprite { get; set; }

		public string Color { get; set; }

		[JsonProperty("material")]
		public string Material { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("png")]
		public string Png { get; set; }

		[JsonProperty("steamid")]
		public string SteamId { get; set; }

		public float FadeIn { get; set; }

		public string PlaceholderParentId { get; set; }

		public bool? Enabled { get; set; }
	}
	public class CuiButtonComponent : ICuiComponent, ICuiColor, ICuiEnableable, ICuiGraphic
	{
		public string Type => "UnityEngine.UI.Button";

		[JsonProperty("command")]
		public string Command { get; set; }

		[JsonProperty("close")]
		public string Close { get; set; }

		[JsonProperty("sprite")]
		public string Sprite { get; set; }

		[JsonProperty("material")]
		public string Material { get; set; }

		public string Color { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		[JsonProperty("imagetype")]
		public Image.Type ImageType { get; set; }

		[JsonProperty("normalColor")]
		public string NormalColor { get; set; }

		[JsonProperty("highlightedColor")]
		public string HighlightedColor { get; set; }

		[JsonProperty("pressedColor")]
		public string PressedColor { get; set; }

		[JsonProperty("selectedColor")]
		public string SelectedColor { get; set; }

		[JsonProperty("disabledColor")]
		public string DisabledColor { get; set; }

		[JsonProperty("colorMultiplier")]
		public float ColorMultiplier { get; set; }

		[JsonProperty("fadeDuration")]
		public float FadeDuration { get; set; }

		public float FadeIn { get; set; }

		public string PlaceholderParentId { get; set; }

		public bool? Enabled { get; set; }
	}
	public class CuiOutlineComponent : ICuiComponent, ICuiColor, ICuiEnableable
	{
		public string Type => "UnityEngine.UI.Outline";

		public string Color { get; set; }

		[JsonProperty("distance")]
		public string Distance { get; set; }

		[JsonProperty("useGraphicAlpha")]
		public bool UseGraphicAlpha { get; set; }

		public bool? Enabled { get; set; }
	}
	public class CuiInputFieldComponent : ICuiComponent, ICuiColor, ICuiEnableable, ICuiGraphic
	{
		public string Type => "UnityEngine.UI.InputField";

		[JsonProperty("text")]
		public string Text { get; set; } = string.Empty;

		[JsonProperty("fontSize")]
		public int FontSize { get; set; }

		[JsonProperty("font")]
		public string Font { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		[JsonProperty("align")]
		public TextAnchor Align { get; set; }

		public string Color { get; set; }

		[JsonProperty("characterLimit")]
		public int CharsLimit { get; set; }

		[JsonProperty("command")]
		public string Command { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		[JsonProperty("lineType")]
		public InputField.LineType LineType { get; set; }

		[JsonProperty("readOnly", DefaultValueHandling = DefaultValueHandling.Include)]
		public bool ReadOnly { get; set; }

		[JsonProperty("placeholderId")]
		private string PlaceholderId { get; set; }

		[JsonProperty("password", DefaultValueHandling = DefaultValueHandling.Include)]
		public bool IsPassword { get; set; }

		[JsonProperty("needsKeyboard", DefaultValueHandling = DefaultValueHandling.Include)]
		public bool NeedsKeyboard { get; set; }

		[JsonProperty("hudMenuInput", DefaultValueHandling = DefaultValueHandling.Include)]
		public bool HudMenuInput { get; set; }

		[JsonProperty("autofocus")]
		public bool Autofocus { get; set; }

		public float FadeIn { get; set; }

		public string PlaceholderParentId { get; set; }

		public bool? Enabled { get; set; }
	}
	public class CuiCountdownComponent : ICuiComponent, ICuiEnableable
	{
		public string Type => "Countdown";

		[JsonProperty("endTime")]
		public float EndTime { get; set; }

		[JsonProperty("startTime")]
		public float StartTime { get; set; }

		[JsonProperty("step")]
		public float Step { get; set; }

		[JsonProperty("interval")]
		public float Interval { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		[JsonProperty("timerFormat")]
		public TimerFormat TimerFormat { get; set; }

		[JsonProperty("numberFormat")]
		public string NumberFormat { get; set; }

		[JsonProperty("destroyIfDone", DefaultValueHandling = DefaultValueHandling.Include)]
		public bool DestroyIfDone { get; set; }

		[JsonProperty("command")]
		public string Command { get; set; }

		[JsonProperty("fadeIn")]
		public float FadeIn { get; set; }

		public bool? Enabled { get; set; }
	}
	public abstract class CuiLayoutGroupComponent : ICuiComponent, ICuiEnableable
	{
		public abstract string Type { get; }

		[JsonProperty("spacing")]
		public float Spacing { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		[JsonProperty("childAlignment")]
		public TextAnchor ChildAlignment { get; set; }

		[JsonProperty("childForceExpandWidth")]
		public bool? ChildForceExpandWidth { get; set; }

		[JsonProperty("childForceExpandHeight")]
		public bool? ChildForceExpandHeight { get; set; }

		[JsonProperty("childControlWidth")]
		public bool? ChildControlWidth { get; set; }

		[JsonProperty("childControlHeight")]
		public bool? ChildControlHeight { get; set; }

		[JsonProperty("childScaleWidth")]
		public bool? ChildScaleWidth { get; set; }

		[JsonProperty("childScaleHeight")]
		public bool? ChildScaleHeight { get; set; }

		[JsonProperty("padding")]
		public string Padding { get; set; }

		public bool? Enabled { get; set; }
	}
	public class CuiHorizontalLayoutGroupComponent : CuiLayoutGroupComponent
	{
		public override string Type => "UnityEngine.UI.HorizontalLayoutGroup";
	}
	public class CuiVerticalLayoutGroupComponent : CuiLayoutGroupComponent
	{
		public override string Type => "UnityEngine.UI.VerticalLayoutGroup";
	}
	public class CuiGridLayoutGroupComponent : ICuiComponent, ICuiEnableable
	{
		public string Type => "UnityEngine.UI.GridLayoutGroup";

		[JsonProperty("cellSize")]
		public string CellSize { get; set; }

		[JsonProperty("spacing")]
		public string Spacing { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		[JsonProperty("startCorner")]
		public GridLayoutGroup.Corner StartCorner { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		[JsonProperty("startAxis")]
		public GridLayoutGroup.Axis StartAxis { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		[JsonProperty("childAlignment")]
		public TextAnchor ChildAlignment { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		[JsonProperty("constraint")]
		public GridLayoutGroup.Constraint Constraint { get; set; }

		[JsonProperty("constraintCount")]
		public int ConstraintCount { get; set; }

		[JsonProperty("padding")]
		public string Padding { get; set; }

		public bool? Enabled { get; set; }
	}
	public class CuiContentSizeFitterComponent : ICuiComponent, ICuiEnableable
	{
		public string Type => "UnityEngine.UI.ContentSizeFitter";

		[JsonConverter(typeof(StringEnumConverter))]
		[JsonProperty("horizontalFit")]
		public ContentSizeFitter.FitMode HorizontalFit { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		[JsonProperty("verticalFit")]
		public ContentSizeFitter.FitMode VerticalFit { get; set; }

		public bool? Enabled { get; set; }
	}
	public class CuiLayoutElementComponent : ICuiComponent, ICuiEnableable
	{
		public string Type => "UnityEngine.UI.LayoutElement";

		[JsonProperty("preferredWidth")]
		public float PreferredWidth { get; set; }

		[JsonProperty("preferredHeight")]
		public float PreferredHeight { get; set; }

		[JsonProperty("minWidth")]
		public float MinWidth { get; set; }

		[JsonProperty("minHeight")]
		public float MinHeight { get; set; }

		[JsonProperty("flexibleWidth")]
		public float FlexibleWidth { get; set; }

		[JsonProperty("flexibleHeight")]
		public float FlexibleHeight { get; set; }

		[JsonProperty("ignoreLayout")]
		public bool? IgnoreLayout { get; set; }

		public bool? Enabled { get; set; }
	}
	public class CuiDraggableComponent : ICuiComponent, ICuiEnableable
	{
		public string Type => "Draggable";

		[JsonProperty("limitToParent")]
		public bool? LimitToParent { get; set; }

		[JsonProperty("maxDistance")]
		public float MaxDistance { get; set; }

		[JsonProperty("allowSwapping")]
		public bool? AllowSwapping { get; set; }

		[JsonProperty("dropAnywhere")]
		public bool? DropAnywhere { get; set; }

		[JsonProperty("dragAlpha")]
		public float DragAlpha { get; set; }

		[JsonProperty("parentLimitIndex")]
		public int ParentLimitIndex { get; set; }

		[JsonProperty("filter")]
		public string Filter { get; set; }

		[JsonProperty("parentPadding")]
		public string ParentPadding { get; set; }

		[JsonProperty("anchorOffset")]
		public string AnchorOffset { get; set; }

		[JsonProperty("keepOnTop")]
		public bool? KeepOnTop { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		[JsonProperty("positionRPC")]
		public CommunityEntity.DraggablePositionSendType PositionRPC { get; set; }

		[JsonProperty("moveToAnchor")]
		public bool MoveToAnchor { get; set; }

		[JsonProperty("rebuildAnchor")]
		public bool RebuildAnchor { get; set; }

		public bool? Enabled { get; set; }
	}
	public class CuiSlotComponent : ICuiComponent, ICuiEnableable
	{
		public string Type => "Slot";

		[JsonProperty("filter")]
		public string Filter { get; set; }

		public bool? Enabled { get; set; }
	}
	public enum TimerFormat
	{
		None,
		SecondsHundreth,
		MinutesSeconds,
		MinutesSecondsHundreth,
		HoursMinutes,
		HoursMinutesSeconds,
		HoursMinutesSecondsMilliseconds,
		HoursMinutesSecondsTenths,
		DaysHoursMinutes,
		DaysHoursMinutesSeconds,
		Custom
	}
	public class CuiNeedsCursorComponent : ICuiComponent, ICuiEnableable
	{
		public string Type => "NeedsCursor";

		public bool? Enabled { get; set; }
	}
	public class CuiNeedsKeyboardComponent : ICuiComponent, ICuiEnableable
	{
		public string Type => "NeedsKeyboard";

		public bool? Enabled { get; set; }
	}
	public class CuiRectTransformComponent : CuiRectTransform, ICuiComponent
	{
		public string Type => "RectTransform";
	}
	public class CuiRectTransform
	{
		[JsonProperty("anchormin")]
		public string AnchorMin { get; set; }

		[JsonProperty("anchormax")]
		public string AnchorMax { get; set; }

		[JsonProperty("offsetmin")]
		public string OffsetMin { get; set; }

		[JsonProperty("offsetmax")]
		public string OffsetMax { get; set; }

		[JsonProperty("rotation")]
		public float Rotation { get; set; }

		[JsonProperty("pivot")]
		public string Pivot { get; set; }

		[JsonProperty("setParent")]
		public string SetParent { get; set; }

		[JsonProperty("setTransformIndex")]
		public int SetTransformIndex { get; set; }
	}
	public class CuiScrollViewComponent : ICuiComponent, ICuiEnableable
	{
		public string Type => "UnityEngine.UI.ScrollView";

		[JsonProperty("contentTransform")]
		public CuiRectTransform ContentTransform { get; set; }

		[JsonProperty("horizontal", DefaultValueHandling = DefaultValueHandling.Include)]
		public bool Horizontal { get; set; }

		[JsonProperty("vertical", DefaultValueHandling = DefaultValueHandling.Include)]
		public bool Vertical { get; set; }

		[JsonProperty("movementType")]
		[JsonConverter(typeof(StringEnumConverter))]
		public ScrollRect.MovementType MovementType { get; set; }

		[JsonProperty("elasticity")]
		public float Elasticity { get; set; }

		[JsonProperty("inertia", DefaultValueHandling = DefaultValueHandling.Include)]
		public bool Inertia { get; set; }

		[JsonProperty("decelerationRate")]
		public float DecelerationRate { get; set; }

		[JsonProperty("scrollSensitivity")]
		public float ScrollSensitivity { get; set; }

		[JsonProperty("horizontalScrollbar")]
		public CuiScrollbar HorizontalScrollbar { get; set; }

		[JsonProperty("verticalScrollbar")]
		public CuiScrollbar VerticalScrollbar { get; set; }

		[JsonProperty("horizontalNormalizedPosition")]
		public float HorizontalNormalizedPosition { get; set; }

		[JsonProperty("verticalNormalizedPosition")]
		public float VerticalNormalizedPosition { get; set; }

		public bool? Enabled { get; set; }
	}
	public class CuiScrollbar : ICuiEnableable
	{
		[JsonProperty("invert")]
		public bool Invert { get; set; }

		[JsonProperty("autoHide")]
		public bool AutoHide { get; set; }

		[JsonProperty("handleSprite")]
		public string HandleSprite { get; set; }

		[JsonProperty("size")]
		public float Size { get; set; }

		[JsonProperty("handleColor")]
		public string HandleColor { get; set; }

		[JsonProperty("highlightColor")]
		public string HighlightColor { get; set; }

		[JsonProperty("pressedColor")]
		public string PressedColor { get; set; }

		[JsonProperty("trackSprite")]
		public string TrackSprite { get; set; }

		[JsonProperty("trackColor")]
		public string TrackColor { get; set; }

		public bool? Enabled { get; set; }
	}
	public class ComponentConverter : JsonConverter
	{
		public override bool CanWrite => false;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JObject jObject = JObject.Load(reader);
			Type typeFromHandle;
			switch (jObject["type"].ToString())
			{
			case "UnityEngine.UI.Text":
				typeFromHandle = typeof(CuiTextComponent);
				break;
			case "UnityEngine.UI.Image":
				typeFromHandle = typeof(CuiImageComponent);
				break;
			case "UnityEngine.UI.RawImage":
				typeFromHandle = typeof(CuiRawImageComponent);
				break;
			case "UnityEngine.UI.Button":
				typeFromHandle = typeof(CuiButtonComponent);
				break;
			case "UnityEngine.UI.Outline":
				typeFromHandle = typeof(CuiOutlineComponent);
				break;
			case "UnityEngine.UI.InputField":
				typeFromHandle = typeof(CuiInputFieldComponent);
				break;
			case "Countdown":
				typeFromHandle = typeof(CuiCountdownComponent);
				break;
			case "UnityEngine.UI.HorizontalLayoutGroup":
				typeFromHandle = typeof(CuiHorizontalLayoutGroupComponent);
				break;
			case "UnityEngine.UI.VerticalLayoutGroup":
				typeFromHandle = typeof(CuiVerticalLayoutGroupComponent);
				break;
			case "UnityEngine.UI.GridLayoutGroup":
				typeFromHandle = typeof(CuiGridLayoutGroupComponent);
				break;
			case "UnityEngine.UI.ContentSizeFitter":
				typeFromHandle = typeof(CuiContentSizeFitterComponent);
				break;
			case "UnityEngine.UI.LayoutElement":
				typeFromHandle = typeof(CuiLayoutElementComponent);
				break;
			case "Draggable":
				typeFromHandle = typeof(CuiDraggableComponent);
				break;
			case "Slot":
				typeFromHandle = typeof(CuiSlotComponent);
				break;
			case "NeedsCursor":
				typeFromHandle = typeof(CuiNeedsCursorComponent);
				break;
			case "NeedsKeyboard":
				typeFromHandle = typeof(CuiNeedsKeyboardComponent);
				break;
			case "RectTransform":
				typeFromHandle = typeof(CuiRectTransformComponent);
				break;
			case "UnityEngine.UI.ScrollView":
				typeFromHandle = typeof(CuiScrollViewComponent);
				break;
			default:
				return null;
			}
			object obj = Activator.CreateInstance(typeFromHandle);
			serializer.Populate(jObject.CreateReader(), obj);
			return obj;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(ICuiComponent);
		}
	}
}
namespace Oxide.Game.Rust.Libraries
{
	public class Command : Library
	{
		internal struct PluginCallback
		{
			public readonly Plugin Plugin;

			public readonly string Name;

			public Func<ConsoleSystem.Arg, bool> Call;

			public PluginCallback(Plugin plugin, string name)
			{
				Plugin = plugin;
				Name = name;
				Call = null;
			}

			public PluginCallback(Plugin plugin, Func<ConsoleSystem.Arg, bool> callback)
			{
				Plugin = plugin;
				Call = callback;
				Name = null;
			}
		}

		internal class ConsoleCommand
		{
			public readonly string Name;

			public PluginCallback Callback;

			public readonly ConsoleSystem.Command RustCommand;

			public Action<ConsoleSystem.Arg> OriginalCallback;

			internal readonly Permission permission = Interface.Oxide.GetLibrary<Permission>();

			public ConsoleCommand(string name)
			{
				Name = name;
				string[] array = Name.Split(new char[1] { '.' });
				RustCommand = new ConsoleSystem.Command
				{
					Name = array[1],
					Parent = array[0],
					FullName = name,
					ServerUser = true,
					ServerAdmin = true,
					Client = true,
					ClientInfo = false,
					Variable = false,
					Call = HandleCommand
				};
			}

			public void AddCallback(Plugin plugin, string name)
			{
				Callback = new PluginCallback(plugin, name);
			}

			public void AddCallback(Plugin plugin, Func<ConsoleSystem.Arg, bool> callback)
			{
				Callback = new PluginCallback(plugin, callback);
			}

			public void HandleCommand(ConsoleSystem.Arg arg)
			{
				Callback.Plugin?.TrackStart();
				Callback.Call(arg);
				Callback.Plugin?.TrackEnd();
			}
		}

		internal class ChatCommand
		{
			public readonly string Name;

			public readonly Plugin Plugin;

			private readonly Action<BasePlayer, string, string[]> _callback;

			public ChatCommand(string name, Plugin plugin, Action<BasePlayer, string, string[]> callback)
			{
				Name = name;
				Plugin = plugin;
				_callback = callback;
			}

			public void HandleCommand(BasePlayer sender, string name, string[] args)
			{
				Plugin?.TrackStart();
				_callback?.Invoke(sender, name, args);
				Plugin?.TrackEnd();
			}
		}

		internal readonly Dictionary<string, ConsoleCommand> consoleCommands;

		internal readonly Dictionary<string, ChatCommand> chatCommands;

		private readonly Dictionary<Plugin, Event.Callback<Plugin, PluginManager>> pluginRemovedFromManager;

		public Command()
		{
			consoleCommands = new Dictionary<string, ConsoleCommand>();
			chatCommands = new Dictionary<string, ChatCommand>();
			pluginRemovedFromManager = new Dictionary<Plugin, Event.Callback<Plugin, PluginManager>>();
		}

		[LibraryFunction("AddChatCommand")]
		public void AddChatCommand(string name, Plugin plugin, string callback)
		{
			AddChatCommand(name, plugin, delegate(BasePlayer player, string command, string[] args)
			{
				plugin.CallHook(callback, player, command, args);
			});
		}

		public void AddChatCommand(string command, Plugin plugin, Action<BasePlayer, string, string[]> callback)
		{
			string text = command.ToLowerInvariant();
			if (!CanOverrideCommand(command, "chat"))
			{
				string text2 = plugin?.Name ?? "An unknown plugin";
				Interface.Oxide.LogError("{0} tried to register command '{1}', this command already exists and cannot be overridden!", text2, text);
				return;
			}
			if (chatCommands.TryGetValue(text, out var value))
			{
				string text3 = value.Plugin?.Name ?? "an unknown plugin";
				string text4 = plugin?.Name ?? "An unknown plugin";
				string format = text4 + " has replaced the '" + text + "' chat command previously registered by " + text3;
				Interface.Oxide.LogWarning(format);
			}
			if (RustCore.Covalence.CommandSystem.registeredCommands.TryGetValue(text, out var value2))
			{
				string text5 = value2.Source?.Name ?? "an unknown plugin";
				string text6 = plugin?.Name ?? "An unknown plugin";
				string format2 = text6 + " has replaced the '" + text + "' command previously registered by " + text5;
				Interface.Oxide.LogWarning(format2);
				RustCore.Covalence.CommandSystem.UnregisterCommand(text, value2.Source);
			}
			value = new ChatCommand(text, plugin, callback);
			chatCommands[text] = value;
			if (plugin != null && !pluginRemovedFromManager.ContainsKey(plugin))
			{
				pluginRemovedFromManager[plugin] = plugin.OnRemovedFromManager.Add(plugin_OnRemovedFromManager);
			}
		}

		[LibraryFunction("AddConsoleCommand")]
		public void AddConsoleCommand(string command, Plugin plugin, string callback)
		{
			AddConsoleCommand(command, plugin, (ConsoleSystem.Arg arg) => plugin.CallHook(callback, arg) != null);
		}

		public void AddConsoleCommand(string command, Plugin plugin, Func<ConsoleSystem.Arg, bool> callback)
		{
			if (plugin != null && !pluginRemovedFromManager.ContainsKey(plugin))
			{
				pluginRemovedFromManager[plugin] = plugin.OnRemovedFromManager.Add(plugin_OnRemovedFromManager);
			}
			string[] array = command.Split(new char[1] { '.' });
			string text = ((array.Length >= 2) ? array[0].Trim() : "global");
			string text2 = ((array.Length >= 2) ? string.Join(".", array.Skip(1).ToArray()) : array[0].Trim());
			string text3 = text + "." + text2;
			ConsoleCommand consoleCommand = new ConsoleCommand(text3);
			if (!CanOverrideCommand((text == "global") ? text2 : text3, "console"))
			{
				string text4 = plugin?.Name ?? "An unknown plugin";
				Interface.Oxide.LogError("{0} tried to register command '{1}', this command already exists and cannot be overridden!", text4, text3);
				return;
			}
			if (consoleCommands.TryGetValue(text3, out var value))
			{
				if (value.OriginalCallback != null)
				{
					consoleCommand.OriginalCallback = value.OriginalCallback;
				}
				string text5 = value.Callback.Plugin?.Name ?? "an unknown plugin";
				string text6 = plugin?.Name ?? "An unknown plugin";
				string format = text6 + " has replaced the '" + command + "' console command previously registered by " + text5;
				Interface.Oxide.LogWarning(format);
				ConsoleSystem.Index.Server.Dict.Remove(value.RustCommand.FullName);
				if (text == "global")
				{
					ConsoleSystem.Index.Server.GlobalDict.Remove(value.RustCommand.Name);
				}
				ConsoleSystem.Index.All = ConsoleSystem.Index.Server.Dict.Values.ToArray();
			}
			if (RustCore.Covalence.CommandSystem.registeredCommands.TryGetValue((text == "global") ? text2 : text3, out var value2))
			{
				if (value2.OriginalCallback != null)
				{
					consoleCommand.OriginalCallback = value2.OriginalCallback;
				}
				string text7 = value2.Source?.Name ?? "an unknown plugin";
				string text8 = plugin?.Name ?? "An unknown plugin";
				string format2 = text8 + " has replaced the '" + text3 + "' command previously registered by " + text7;
				Interface.Oxide.LogWarning(format2);
				RustCore.Covalence.CommandSystem.UnregisterCommand((text == "global") ? text2 : text3, value2.Source);
			}
			consoleCommand.AddCallback(plugin, callback);
			if (ConsoleSystem.Index.Server.Dict.TryGetValue(text3, out var value3))
			{
				if (value3.Variable)
				{
					string text9 = plugin?.Name ?? "An unknown plugin";
					Interface.Oxide.LogError(text9 + " tried to register the " + text2 + " console variable as a command!");
					return;
				}
				consoleCommand.OriginalCallback = value3.Call;
			}
			ConsoleSystem.Index.Server.Dict[text3] = consoleCommand.RustCommand;
			if (text == "global")
			{
				ConsoleSystem.Index.Server.GlobalDict[text2] = consoleCommand.RustCommand;
			}
			ConsoleSystem.Index.All = ConsoleSystem.Index.Server.Dict.Values.ToArray();
			consoleCommands[text3] = consoleCommand;
		}

		[LibraryFunction("RemoveChatCommand")]
		public void RemoveChatCommand(string command, Plugin plugin)
		{
			ChatCommand chatCommand = chatCommands.Values.Where((ChatCommand x) => x.Plugin == plugin).FirstOrDefault((ChatCommand x) => x.Name == command);
			if (chatCommand != null)
			{
				RemoveChatCommand(chatCommand);
			}
		}

		[LibraryFunction("RemoveConsoleCommand")]
		public void RemoveConsoleCommand(string command, Plugin plugin)
		{
			ConsoleCommand consoleCommand = consoleCommands.Values.Where((ConsoleCommand x) => x.Callback.Plugin == plugin).FirstOrDefault((ConsoleCommand x) => x.Name == command);
			if (consoleCommand != null)
			{
				RemoveConsoleCommand(consoleCommand);
			}
		}

		private void RemoveChatCommand(ChatCommand command)
		{
			if (chatCommands.ContainsKey(command.Name))
			{
				chatCommands.Remove(command.Name);
			}
		}

		private void RemoveConsoleCommand(ConsoleCommand command)
		{
			if (!consoleCommands.ContainsKey(command.Name))
			{
				return;
			}
			consoleCommands.Remove(command.Name);
			if (command.OriginalCallback != null)
			{
				ConsoleSystem.Index.Server.Dict[command.RustCommand.FullName].Call = command.OriginalCallback;
				if (command.RustCommand.FullName.StartsWith("global."))
				{
					ConsoleSystem.Index.Server.GlobalDict[command.RustCommand.Name].Call = command.OriginalCallback;
				}
				return;
			}
			ConsoleSystem.Index.Server.Dict.Remove(command.RustCommand.FullName);
			if (command.Name.StartsWith("global."))
			{
				ConsoleSystem.Index.Server.GlobalDict.Remove(command.RustCommand.Name);
			}
			ConsoleSystem.Index.All = ConsoleSystem.Index.Server.Dict.Values.ToArray();
		}

		internal bool HandleChatCommand(BasePlayer sender, string name, string[] args)
		{
			if (chatCommands.TryGetValue(name.ToLowerInvariant(), out var value))
			{
				value.HandleCommand(sender, name, args);
				return true;
			}
			return false;
		}

		private void plugin_OnRemovedFromManager(Plugin sender, PluginManager manager)
		{
			ConsoleCommand[] array = consoleCommands.Values.Where((ConsoleCommand c) => c.Callback.Plugin == sender).ToArray();
			foreach (ConsoleCommand command in array)
			{
				RemoveConsoleCommand(command);
			}
			ChatCommand[] array2 = chatCommands.Values.Where((ChatCommand c) => c.Plugin == sender).ToArray();
			foreach (ChatCommand command2 in array2)
			{
				RemoveChatCommand(command2);
			}
			if (pluginRemovedFromManager.TryGetValue(sender, out var value))
			{
				value.Remove();
				pluginRemovedFromManager.Remove(sender);
			}
		}

		private bool CanOverrideCommand(string command, string type)
		{
			string[] array = command.Split(new char[1] { '.' });
			string text = ((array.Length >= 2) ? array[0].Trim() : "global");
			string text2 = ((array.Length >= 2) ? string.Join(".", array.Skip(1).ToArray()) : array[0].Trim());
			string text3 = text + "." + text2;
			if (RustCore.Covalence.CommandSystem.registeredCommands.TryGetValue(command, out var value) && value.Source.IsCorePlugin)
			{
				return false;
			}
			ConsoleCommand value3;
			if (type == "chat")
			{
				if (chatCommands.TryGetValue(command, out var value2) && value2.Plugin.IsCorePlugin)
				{
					return false;
				}
			}
			else if (type == "console" && consoleCommands.TryGetValue((text == "global") ? text2 : text3, out value3) && value3.Callback.Plugin.IsCorePlugin)
			{
				return false;
			}
			if (!RustCore.RestrictedCommands.Contains(command))
			{
				return !RustCore.RestrictedCommands.Contains(text3);
			}
			return false;
		}
	}
	public class Item : Library
	{
		internal static readonly RustCovalenceProvider Covalence = RustCovalenceProvider.Instance;

		public static global::Item GetItem(int itemId)
		{
			return ItemManager.CreateByItemID(itemId, 1, 0uL);
		}
	}
	public class Player : Library
	{
		private static readonly string ipPattern = ":{1}[0-9]{1}\\d*";

		internal readonly Permission permission = Interface.Oxide.GetLibrary<Permission>();

		public ListHashSet<BasePlayer> Players => BasePlayer.activePlayerList;

		public ListHashSet<BasePlayer> Sleepers => BasePlayer.sleepingPlayerList;

		public CultureInfo Language(BasePlayer player)
		{
			try
			{
				return CultureInfo.GetCultureInfo(player.net.connection.language ?? "en");
			}
			catch (CultureNotFoundException)
			{
				return CultureInfo.GetCultureInfo("en");
			}
		}

		public string Address(Connection connection)
		{
			return Regex.Replace(connection.ipaddress, ipPattern, "");
		}

		public string Address(BasePlayer player)
		{
			if (player?.net?.connection == null)
			{
				return null;
			}
			return Address(player.net.connection);
		}

		public int Ping(Connection connection)
		{
			return Network.Net.sv.GetAveragePing(connection);
		}

		public int Ping(BasePlayer player)
		{
			return Ping(player.net.connection);
		}

		public bool IsAdmin(ulong id)
		{
			if (!ServerUsers.Is(id, ServerUsers.UserGroup.Owner))
			{
				return DeveloperList.Contains(id);
			}
			return true;
		}

		public bool IsAdmin(string id)
		{
			return IsAdmin(Convert.ToUInt64(id));
		}

		public bool IsAdmin(BasePlayer player)
		{
			return IsAdmin(player.userID);
		}

		public bool IsBanned(ulong id)
		{
			return ServerUsers.Is(id, ServerUsers.UserGroup.Banned);
		}

		public bool IsBanned(string id)
		{
			return IsBanned(Convert.ToUInt64(id));
		}

		public bool IsBanned(BasePlayer player)
		{
			return IsBanned(player.userID);
		}

		public bool IsConnected(BasePlayer player)
		{
			return player.IsConnected;
		}

		public bool IsSleeping(ulong id)
		{
			return BasePlayer.FindSleeping(id);
		}

		public bool IsSleeping(string id)
		{
			return IsSleeping(Convert.ToUInt64(id));
		}

		public bool IsSleeping(BasePlayer player)
		{
			return IsSleeping(player.userID);
		}

		public void Ban(ulong id, string reason = "", long expiry = -1L)
		{
			if (!IsBanned(id))
			{
				BasePlayer basePlayer = FindById(id);
				ServerUsers.Set(id, ServerUsers.UserGroup.Banned, basePlayer?.displayName ?? "Unknown", reason, expiry);
				ServerUsers.Save();
				if (basePlayer != null && IsConnected(basePlayer))
				{
					Kick(basePlayer, reason);
				}
			}
		}

		public void Ban(string id, string reason = "", long expiry = -1L)
		{
			Ban(Convert.ToUInt64(id), reason, expiry);
		}

		public void Ban(BasePlayer player, string reason = "", long expiry = -1L)
		{
			Ban(player.UserIDString, reason, expiry);
		}

		public void Heal(BasePlayer player, float amount)
		{
			player.Heal(amount);
		}

		public void Hurt(BasePlayer player, float amount)
		{
			player.Hurt(amount);
		}

		public void Kick(BasePlayer player, string reason = "")
		{
			player.Kick(reason);
		}

		public void Kill(BasePlayer player)
		{
			player.Die();
		}

		public void Rename(BasePlayer player, string name)
		{
			name = (string.IsNullOrEmpty(name.Trim()) ? player.displayName : name);
			SingletonComponent<ServerMgr>.Instance.persistance.SetPlayerName(player.userID, name);
			player.net.connection.username = name;
			player.displayName = name;
			player._name = name;
			player.SendNetworkUpdateImmediate();
			player.IPlayer.Name = name;
			permission.UpdateNickname(player.UserIDString, name);
			if (player.net.group == BaseNetworkable.LimboNetworkGroup)
			{
				return;
			}
			List<Connection> obj = Facepunch.Pool.Get<List<Connection>>();
			for (int i = 0; i < Network.Net.sv.connections.Count; i++)
			{
				Connection connection = Network.Net.sv.connections[i];
				if (connection.connected && connection.isAuthenticated && connection.player is BasePlayer && connection.player != player)
				{
					obj.Add(connection);
				}
			}
			player.OnNetworkSubscribersLeave(obj);
			Facepunch.Pool.FreeUnmanaged(ref obj);
			if (!player.limitNetworking)
			{
				player.syncPosition = false;
				player._limitedNetworking = true;
				Interface.Oxide.NextTick(delegate
				{
					player.syncPosition = true;
					player._limitedNetworking = false;
					player.UpdateNetworkGroup();
					player.SendNetworkUpdate();
				});
			}
		}

		public void Teleport(BasePlayer player, Vector3 destination)
		{
			if (player.IsAlive() && !player.IsSpectating())
			{
				try
				{
					player.EnsureDismounted();
					player.SetParent(null, worldPositionStays: true, sendImmediate: true);
					player.SetServerFall(wantsOn: true);
					player.MovePosition(destination);
					player.ClientRPC(RpcTarget.Player("ForcePositionTo", player), destination);
				}
				finally
				{
					player.SetServerFall(wantsOn: false);
				}
			}
		}

		public void Teleport(BasePlayer player, BasePlayer target)
		{
			Teleport(player, Position(target));
		}

		public void Teleport(BasePlayer player, float x, float y, float z)
		{
			Teleport(player, new Vector3(x, y, z));
		}

		public void Unban(ulong id)
		{
			if (IsBanned(id))
			{
				ServerUsers.Remove(id);
				ServerUsers.Save();
			}
		}

		public void Unban(string id)
		{
			Unban(Convert.ToUInt64(id));
		}

		public void Unban(BasePlayer player)
		{
			Unban(player.userID);
		}

		public Vector3 Position(BasePlayer player)
		{
			return player.transform.position;
		}

		public BasePlayer Find(string nameOrIdOrIp)
		{
			foreach (BasePlayer player in Players)
			{
				if (nameOrIdOrIp.Equals(player.displayName, StringComparison.OrdinalIgnoreCase) || nameOrIdOrIp.Equals(player.UserIDString) || nameOrIdOrIp.Equals(player.net.connection.ipaddress))
				{
					return player;
				}
			}
			return null;
		}

		public BasePlayer FindById(string id)
		{
			foreach (BasePlayer player in Players)
			{
				if (id.Equals(player.UserIDString))
				{
					return player;
				}
			}
			return null;
		}

		public BasePlayer FindById(ulong id)
		{
			foreach (BasePlayer player in Players)
			{
				if (id.Equals(player.userID))
				{
					return player;
				}
			}
			return null;
		}

		public void Message(BasePlayer player, string message, string prefix, ulong userId = 0uL, params object[] args)
		{
			if (!string.IsNullOrEmpty(message))
			{
				message = ((args.Length != 0) ? string.Format(Formatter.ToUnity(message), args) : Formatter.ToUnity(message));
				string text = ((prefix != null) ? (prefix + " " + message) : message);
				if (Interface.CallHook("OnMessagePlayer", text, player, userId) == null)
				{
					player.SendConsoleCommand("chat.add", 2, userId, text);
				}
			}
		}

		public void Message(BasePlayer player, string message, ulong userId = 0uL)
		{
			Message(player, message, null, userId);
		}

		public void Reply(BasePlayer player, string message, string prefix, ulong userId = 0uL, params object[] args)
		{
			Message(player, message, prefix, userId, args);
		}

		public void Reply(BasePlayer player, string message, ulong userId = 0uL)
		{
			Message(player, message, null, userId);
		}

		public void Command(BasePlayer player, string command, params object[] args)
		{
			player.SendConsoleCommand(command, args);
		}

		public void DropItem(BasePlayer player, int itemId)
		{
			Vector3 position = player.transform.position;
			PlayerInventory playerInventory = Inventory(player);
			for (int i = 0; i < playerInventory.containerMain.capacity; i++)
			{
				global::Item slot = playerInventory.containerMain.GetSlot(i);
				if (slot.info.itemid == itemId)
				{
					slot.Drop(position + new Vector3(0f, 1f, 0f) + position / 2f, (position + new Vector3(0f, 0.2f, 0f)) * 8f);
				}
			}
			for (int j = 0; j < playerInventory.containerBelt.capacity; j++)
			{
				global::Item slot2 = playerInventory.containerBelt.GetSlot(j);
				if (slot2.info.itemid == itemId)
				{
					slot2.Drop(position + new Vector3(0f, 1f, 0f) + position / 2f, (position + new Vector3(0f, 0.2f, 0f)) * 8f);
				}
			}
			for (int k = 0; k < playerInventory.containerWear.capacity; k++)
			{
				global::Item slot3 = playerInventory.containerWear.GetSlot(k);
				if (slot3.info.itemid == itemId)
				{
					slot3.Drop(position + new Vector3(0f, 1f, 0f) + position / 2f, (position + new Vector3(0f, 0.2f, 0f)) * 8f);
				}
			}
		}

		public void DropItem(BasePlayer player, global::Item item)
		{
			Vector3 position = player.transform.position;
			PlayerInventory playerInventory = Inventory(player);
			for (int i = 0; i < playerInventory.containerMain.capacity; i++)
			{
				global::Item slot = playerInventory.containerMain.GetSlot(i);
				if (slot == item)
				{
					slot.Drop(position + new Vector3(0f, 1f, 0f) + position / 2f, (position + new Vector3(0f, 0.2f, 0f)) * 8f);
				}
			}
			for (int j = 0; j < playerInventory.containerBelt.capacity; j++)
			{
				global::Item slot2 = playerInventory.containerBelt.GetSlot(j);
				if (slot2 == item)
				{
					slot2.Drop(position + new Vector3(0f, 1f, 0f) + position / 2f, (position + new Vector3(0f, 0.2f, 0f)) * 8f);
				}
			}
			for (int k = 0; k < playerInventory.containerWear.capacity; k++)
			{
				global::Item slot3 = playerInventory.containerWear.GetSlot(k);
				if (slot3 == item)
				{
					slot3.Drop(position + new Vector3(0f, 1f, 0f) + position / 2f, (position + new Vector3(0f, 0.2f, 0f)) * 8f);
				}
			}
		}

		public void GiveItem(BasePlayer player, int itemId, int quantity = 1)
		{
			GiveItem(player, Item.GetItem(itemId), quantity);
		}

		public void GiveItem(BasePlayer player, global::Item item, int quantity = 1)
		{
			player.inventory.GiveItem(ItemManager.CreateByItemID(item.info.itemid, quantity, 0uL));
		}

		public PlayerInventory Inventory(BasePlayer player)
		{
			return player.inventory;
		}

		public void ClearInventory(BasePlayer player)
		{
			Inventory(player)?.Strip();
		}

		public void ResetInventory(BasePlayer player)
		{
			PlayerInventory playerInventory = Inventory(player);
			if (playerInventory != null)
			{
				playerInventory.DoDestroy();
				playerInventory.ServerInit(player);
			}
		}
	}
	public class Rust : Library
	{
		internal readonly Player Player = new Player();

		internal readonly Server Server = new Server();

		public override bool IsGlobal => false;

		[LibraryFunction("PrivateBindingFlag")]
		public BindingFlags PrivateBindingFlag()
		{
			return BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
		}

		[LibraryFunction("QuoteSafe")]
		public string QuoteSafe(string str)
		{
			return Oxide.Core.ExtensionMethods.Quote(str);
		}

		[LibraryFunction("BroadcastChat")]
		public void BroadcastChat(string name, string message = null, string userId = "0")
		{
			Server.Broadcast(message, name, Convert.ToUInt64(userId));
		}

		[LibraryFunction("SendChatMessage")]
		public void SendChatMessage(BasePlayer player, string name, string message = null, string userId = "0")
		{
			Player.Message(player, message, name, Convert.ToUInt64(userId));
		}

		[LibraryFunction("RunClientCommand")]
		public void RunClientCommand(BasePlayer player, string command, params object[] args)
		{
			Player.Command(player, command, args);
		}

		[LibraryFunction("RunServerCommand")]
		public void RunServerCommand(string command, params object[] args)
		{
			Server.Command(command, args);
		}

		[LibraryFunction("UserIDFromConnection")]
		public string UserIDFromConnection(Connection connection)
		{
			return connection.userid.ToString();
		}

		[LibraryFunction("UserIDsFromBuildingPrivilege")]
		public System.Array UserIDsFromBuildingPrivlidge(BuildingPrivlidge priv)
		{
			return priv.authorizedPlayers.Select((ulong playerId) => playerId.ToString()).ToArray();
		}

		[LibraryFunction("UserIDFromPlayer")]
		public string UserIDFromPlayer(BasePlayer player)
		{
			return player.UserIDString;
		}

		[LibraryFunction("OwnerIDFromEntity")]
		public string OwnerIDFromEntity(BaseEntity entity)
		{
			return entity.OwnerID.ToString();
		}

		[LibraryFunction("FindPlayer")]
		public BasePlayer FindPlayer(string nameOrIdOrIp)
		{
			return Player.Find(nameOrIdOrIp);
		}

		[LibraryFunction("FindPlayerByName")]
		public BasePlayer FindPlayerByName(string name)
		{
			return Player.Find(name);
		}

		[LibraryFunction("FindPlayerById")]
		public BasePlayer FindPlayerById(ulong id)
		{
			return Player.FindById(id);
		}

		[LibraryFunction("FindPlayerByIdString")]
		public BasePlayer FindPlayerByIdString(string id)
		{
			return Player.FindById(id);
		}

		[LibraryFunction("ForcePlayerPosition")]
		public void ForcePlayerPosition(BasePlayer player, float x, float y, float z)
		{
			Player.Teleport(player, x, y, z);
		}
	}
	public class Server : Library
	{
		public void Broadcast(string message, string prefix, ulong userId = 0uL, params object[] args)
		{
			if (!string.IsNullOrEmpty(message))
			{
				message = ((args.Length != 0) ? string.Format(Formatter.ToUnity(message), args) : Formatter.ToUnity(message));
				string text = ((prefix != null) ? (prefix + ": " + message) : message);
				ConsoleNetwork.BroadcastToAllClients("chat.add", 2, userId, text);
			}
		}

		public void Broadcast(string message, ulong userId = 0uL)
		{
			Broadcast(message, null, userId);
		}

		public void Command(string command, params object[] args)
		{
			ConsoleSystem.Run(ConsoleSystem.Option.Server, command, args);
		}
	}
}
namespace Oxide.Game.Rust.Libraries.Covalence
{
	public class RustCommandSystem : ICommandSystem
	{
		internal class RegisteredCommand
		{
			public readonly Plugin Source;

			public readonly string Command;

			public readonly CommandCallback Callback;

			public ConsoleSystem.Command RustCommand;

			public ConsoleSystem.Command OriginalRustCommand;

			public Action<ConsoleSystem.Arg> OriginalCallback;

			public RegisteredCommand(Plugin source, string command, CommandCallback callback)
			{
				Source = source;
				Command = command;
				Callback = callback;
			}
		}

		private readonly Command cmdlib = Interface.Oxide.GetLibrary<Command>();

		private readonly RustConsolePlayer consolePlayer;

		private readonly CommandHandler commandHandler;

		internal IDictionary<string, RegisteredCommand> registeredCommands;

		public RustCommandSystem()
		{
			registeredCommands = new Dictionary<string, RegisteredCommand>();
			commandHandler = new CommandHandler(CommandCallback, registeredCommands.ContainsKey);
			consolePlayer = new RustConsolePlayer();
		}

		private bool CommandCallback(IPlayer caller, string cmd, string[] args)
		{
			if (registeredCommands.TryGetValue(cmd, out var value))
			{
				return value.Callback(caller, cmd, args);
			}
			return false;
		}

		public void RegisterCommand(string command, Plugin plugin, CommandCallback callback)
		{
			command = command.ToLowerInvariant().Trim();
			string[] array = command.Split(new char[1] { '.' });
			string text = ((array.Length >= 2) ? array[0].Trim() : "global");
			string text2 = ((array.Length >= 2) ? string.Join(".", array.Skip(1).ToArray()) : array[0].Trim());
			string text3 = text + "." + text2;
			if (text == "global")
			{
				command = text2;
			}
			RegisteredCommand registeredCommand = new RegisteredCommand(plugin, command, callback);
			if (!CanOverrideCommand(command))
			{
				throw new CommandAlreadyExistsException(command);
			}
			if (registeredCommands.TryGetValue(command, out var value))
			{
				if (value.OriginalCallback != null)
				{
					registeredCommand.OriginalCallback = value.OriginalCallback;
				}
				string text4 = value.Source?.Name ?? "an unknown plugin";
				string text5 = plugin?.Name ?? "An unknown plugin";
				string format = text5 + " has replaced the '" + command + "' command previously registered by " + text4;
				Interface.Oxide.LogWarning(format);
				ConsoleSystem.Index.Server.Dict.Remove(text3);
				if (text == "global")
				{
					ConsoleSystem.Index.Server.GlobalDict.Remove(text2);
				}
				ConsoleSystem.Index.All = ConsoleSystem.Index.Server.Dict.Values.ToArray();
			}
			if (cmdlib.chatCommands.TryGetValue(command, out var value2))
			{
				string text6 = value2.Plugin?.Name ?? "an unknown plugin";
				string text7 = plugin?.Name ?? "An unknown plugin";
				string format2 = text7 + " has replaced the '" + command + "' chat command previously registered by " + text6;
				Interface.Oxide.LogWarning(format2);
				cmdlib.chatCommands.Remove(command);
			}
			if (cmdlib.consoleCommands.TryGetValue(text3, out var value3))
			{
				if (value3.OriginalCallback != null)
				{
					registeredCommand.OriginalCallback = value3.OriginalCallback;
				}
				string text8 = value3.Callback.Plugin?.Name ?? "an unknown plugin";
				string text9 = plugin?.Name ?? "An unknown plugin";
				string format3 = text9 + " has replaced the '" + text3 + "' console command previously registered by " + text8;
				Interface.Oxide.LogWarning(format3);
				ConsoleSystem.Index.Server.Dict.Remove(value3.RustCommand.FullName);
				if (text == "global")
				{
					ConsoleSystem.Index.Server.GlobalDict.Remove(value3.RustCommand.Name);
				}
				ConsoleSystem.Index.All = ConsoleSystem.Index.Server.Dict.Values.ToArray();
				cmdlib.consoleCommands.Remove(value3.RustCommand.FullName);
			}
			if (ConsoleSystem.Index.Server.Dict.TryGetValue(text3, out var value4))
			{
				if (value4.Variable)
				{
					string text10 = plugin?.Name ?? "An unknown plugin";
					Interface.Oxide.LogError(text10 + " tried to register the " + text3 + " console variable as a command!");
					return;
				}
				registeredCommand.OriginalCallback = value4.Call;
				registeredCommand.OriginalRustCommand = value4;
			}
			registeredCommand.RustCommand = new ConsoleSystem.Command
			{
				Name = text2,
				Parent = text,
				FullName = command,
				ServerUser = true,
				ServerAdmin = true,
				Client = true,
				ClientInfo = false,
				Variable = false,
				Call = delegate(ConsoleSystem.Arg arg)
				{
					if (arg != null)
					{
						BasePlayer basePlayer = arg.Player();
						if (arg.Connection != null && basePlayer != null)
						{
							if (basePlayer.IPlayer is RustPlayer rustPlayer)
							{
								rustPlayer.LastCommand = CommandType.Console;
								callback(rustPlayer, command, ExtractArgs(arg));
							}
						}
						else if (arg.Connection == null)
						{
							consolePlayer.LastCommand = CommandType.Console;
							callback(consolePlayer, command, ExtractArgs(arg));
						}
					}
				}
			};
			ConsoleSystem.Index.Server.Dict[text3] = registeredCommand.RustCommand;
			if (text == "global")
			{
				ConsoleSystem.Index.Server.GlobalDict[text2] = registeredCommand.RustCommand;
			}
			ConsoleSystem.Index.All = ConsoleSystem.Index.Server.Dict.Values.ToArray();
			registeredCommands[command] = registeredCommand;
		}

		public void UnregisterCommand(string command, Plugin plugin)
		{
			if (!registeredCommands.TryGetValue(command, out var value) || plugin != value.Source)
			{
				return;
			}
			string[] array = command.Split(new char[1] { '.' });
			string obj = ((array.Length >= 2) ? array[0].Trim() : "global");
			string text = ((array.Length >= 2) ? string.Join(".", array.Skip(1).ToArray()) : array[0].Trim());
			string text2 = obj + "." + text;
			registeredCommands.Remove(command);
			if (value.OriginalCallback != null)
			{
				if (ConsoleSystem.Index.Server.Dict.ContainsKey(text2))
				{
					ConsoleSystem.Index.Server.Dict[text2].Call = value.OriginalCallback;
				}
				if (text2.StartsWith("global.") && ConsoleSystem.Index.Server.GlobalDict.ContainsKey(text))
				{
					ConsoleSystem.Index.Server.GlobalDict[text].Call = value.OriginalCallback;
				}
				if (value.OriginalRustCommand != null)
				{
					if (ConsoleSystem.Index.Server.Dict.ContainsKey(text2))
					{
						ConsoleSystem.Index.Server.Dict[text2] = value.OriginalRustCommand;
					}
					if (text2.StartsWith("global.") && ConsoleSystem.Index.Server.GlobalDict.ContainsKey(text))
					{
						ConsoleSystem.Index.Server.GlobalDict[text] = value.OriginalRustCommand;
					}
				}
			}
			else
			{
				ConsoleSystem.Index.Server.Dict.Remove(text2);
				if (text2.StartsWith("global."))
				{
					ConsoleSystem.Index.Server.GlobalDict.Remove(text);
				}
			}
			ConsoleSystem.Index.All = ConsoleSystem.Index.Server.Dict.Values.ToArray();
		}

		public bool HandleChatMessage(IPlayer player, string message)
		{
			return commandHandler.HandleChatMessage(player, message);
		}

		private bool CanOverrideCommand(string command)
		{
			string[] array = command.Split(new char[1] { '.' });
			string obj = ((array.Length >= 2) ? array[0].Trim() : "global");
			string text = ((array.Length >= 2) ? string.Join(".", array.Skip(1).ToArray()) : array[0].Trim());
			string text2 = obj + "." + text;
			if (registeredCommands.TryGetValue(command, out var value) && value.Source.IsCorePlugin)
			{
				return false;
			}
			if (cmdlib.chatCommands.TryGetValue(command, out var value2) && value2.Plugin.IsCorePlugin)
			{
				return false;
			}
			if (cmdlib.consoleCommands.TryGetValue(text2, out var value3) && value3.Callback.Plugin.IsCorePlugin)
			{
				return false;
			}
			if (!RustCore.RestrictedCommands.Contains(command))
			{
				return !RustCore.RestrictedCommands.Contains(text2);
			}
			return false;
		}

		public static string[] ExtractArgs(ConsoleSystem.Arg arg)
		{
			if (arg == null || !arg.HasArgs())
			{
				return new string[0];
			}
			return arg.FullString.SplitQuotesStrings();
		}
	}
	public class RustConsolePlayer : IPlayer
	{
		public object Object => null;

		public CommandType LastCommand
		{
			get
			{
				return CommandType.Console;
			}
			set
			{
			}
		}

		public string Name
		{
			get
			{
				return "Server Console";
			}
			set
			{
			}
		}

		public string Id => "server_console";

		public CultureInfo Language => CultureInfo.InstalledUICulture;

		public string Address => "127.0.0.1";

		public int Ping => 0;

		public bool IsAdmin => true;

		public bool IsBanned => false;

		public bool IsConnected => true;

		public bool IsSleeping => false;

		public bool IsServer => true;

		public TimeSpan BanTimeRemaining => TimeSpan.Zero;

		public float Health { get; set; }

		public float MaxHealth { get; set; }

		public void Ban(string reason, TimeSpan duration)
		{
		}

		public void Heal(float amount)
		{
		}

		public void Hurt(float amount)
		{
		}

		public void Kick(string reason)
		{
		}

		public void Kill()
		{
		}

		public void Rename(string name)
		{
		}

		public void Teleport(float x, float y, float z)
		{
		}

		public void Teleport(GenericPosition pos)
		{
			Teleport(pos.X, pos.Y, pos.Z);
		}

		public void Unban()
		{
		}

		public void Position(out float x, out float y, out float z)
		{
			x = 0f;
			y = 0f;
			z = 0f;
		}

		public GenericPosition Position()
		{
			return new GenericPosition(0f, 0f, 0f);
		}

		public void Message(string message, string prefix, params object[] args)
		{
			message = ((args.Length != 0) ? string.Format(Formatter.ToPlaintext(message), args) : Formatter.ToPlaintext(message));
			string format = ((prefix != null) ? (prefix + " " + message) : message);
			Interface.Oxide.LogInfo(format);
		}

		public void Message(string message)
		{
			Message(message, null);
		}

		public void Reply(string message, string prefix, params object[] args)
		{
			Message(message, prefix, args);
		}

		public void Reply(string message)
		{
			Message(message, null);
		}

		public void Command(string command, params object[] args)
		{
			ConsoleSystem.Run(ConsoleSystem.Option.Server, command, args);
		}

		public bool HasPermission(string perm)
		{
			return true;
		}

		public void GrantPermission(string perm)
		{
		}

		public void RevokePermission(string perm)
		{
		}

		public bool BelongsToGroup(string group)
		{
			return false;
		}

		public void AddToGroup(string group)
		{
		}

		public void RemoveFromGroup(string group)
		{
		}
	}
	public class RustCovalenceProvider : ICovalenceProvider
	{
		public string GameName => "Rust";

		public uint ClientAppId => 252490u;

		public uint ServerAppId => 258550u;

		internal static RustCovalenceProvider Instance { get; private set; }

		public RustPlayerManager PlayerManager { get; private set; }

		public RustCommandSystem CommandSystem { get; private set; }

		public RustCovalenceProvider()
		{
			Instance = this;
		}

		public IServer CreateServer()
		{
			return new RustServer();
		}

		public IPlayerManager CreatePlayerManager()
		{
			PlayerManager = new RustPlayerManager();
			PlayerManager.Initialize();
			return PlayerManager;
		}

		public ICommandSystem CreateCommandSystemProvider()
		{
			return CommandSystem = new RustCommandSystem();
		}

		public string FormatText(string text)
		{
			return Formatter.ToUnity(text);
		}
	}
	public class RustPlayer : IPlayer, IEquatable<IPlayer>
	{
		private static Player libPlayer;

		private static Permission libPerms;

		private readonly BasePlayer player;

		private readonly ulong steamId;

		public object Object => player;

		public CommandType LastCommand { get; set; }

		public string Name { get; set; }

		public string Id { get; }

		public CultureInfo Language
		{
			get
			{
				if (!(player != null))
				{
					return CultureInfo.GetCultureInfo("en");
				}
				return libPlayer.Language(player);
			}
		}

		public string Address
		{
			get
			{
				if (!(player != null))
				{
					return "0.0.0.0";
				}
				return libPlayer.Address(player);
			}
		}

		public int Ping
		{
			get
			{
				if (!(player != null))
				{
					return 0;
				}
				return libPlayer.Ping(player);
			}
		}

		public bool IsAdmin => libPlayer.IsAdmin(steamId);

		public bool IsBanned => libPlayer.IsBanned(steamId);

		public bool IsConnected
		{
			get
			{
				if (!(player != null))
				{
					return BasePlayer.FindByID(steamId) != null;
				}
				return libPlayer.IsConnected(player);
			}
		}

		public bool IsSleeping
		{
			get
			{
				if (!(player != null))
				{
					return BasePlayer.FindSleeping(steamId) != null;
				}
				return libPlayer.IsSleeping(player);
			}
		}

		public bool IsServer => false;

		public TimeSpan BanTimeRemaining
		{
			get
			{
				if (!IsBanned)
				{
					return TimeSpan.Zero;
				}
				return TimeSpan.MaxValue;
			}
		}

		public float Health
		{
			get
			{
				return player.health;
			}
			set
			{
				player.health = value;
			}
		}

		public float MaxHealth
		{
			get
			{
				return player.MaxHealth();
			}
			set
			{
				player._maxHealth = value;
			}
		}

		internal RustPlayer(ulong id, string name)
		{
			if (libPerms == null)
			{
				libPerms = Interface.Oxide.GetLibrary<Permission>();
			}
			if (libPlayer == null)
			{
				libPlayer = Interface.Oxide.GetLibrary<Player>();
			}
			steamId = id;
			Name = Oxide.Core.ExtensionMethods.Sanitize(name);
			Id = id.ToString();
		}

		internal RustPlayer(BasePlayer player)
			: this(player.userID, player.displayName)
		{
			this.player = player;
		}

		public void Ban(string reason, TimeSpan duration = default(TimeSpan))
		{
			libPlayer.Ban(steamId, reason, -1L);
		}

		public void Heal(float amount)
		{
			libPlayer.Heal(player, amount);
		}

		public void Hurt(float amount)
		{
			libPlayer.Hurt(player, amount);
		}

		public void Kick(string reason)
		{
			libPlayer.Kick(player, reason);
		}

		public void Kill()
		{
			libPlayer.Kill(player);
		}

		public void Rename(string name)
		{
			libPlayer.Rename(player, name);
		}

		public void Teleport(float x, float y, float z)
		{
			libPlayer.Teleport(player, x, y, z);
		}

		public void Teleport(GenericPosition pos)
		{
			Teleport(pos.X, pos.Y, pos.Z);
		}

		public void Unban()
		{
			libPlayer.Unban(steamId);
		}

		public void Position(out float x, out float y, out float z)
		{
			Vector3 vector = libPlayer.Position(player);
			x = vector.x;
			y = vector.y;
			z = vector.z;
		}

		public GenericPosition Position()
		{
			Vector3 vector = libPlayer.Position(player);
			return new GenericPosition(vector.x, vector.y, vector.z);
		}

		public void Message(string message, string prefix, params object[] args)
		{
			libPlayer.Message(player, message, prefix, 0uL, args);
		}

		public void Message(string message)
		{
			Message(message, null);
		}

		public void Reply(string message, string prefix, params object[] args)
		{
			switch (LastCommand)
			{
			case CommandType.Chat:
				Message(message, prefix, args);
				break;
			case CommandType.Console:
				player.ConsoleMessage(string.Format(Formatter.ToPlaintext(message), args));
				break;
			}
		}

		public void Reply(string message)
		{
			Reply(message, null);
		}

		public void Command(string command, params object[] args)
		{
			player.SendConsoleCommand(command, args);
		}

		public bool HasPermission(string perm)
		{
			return libPerms.UserHasPermission(Id, perm);
		}

		public void GrantPermission(string perm)
		{
			libPerms.GrantUserPermission(Id, perm, null);
		}

		public void RevokePermission(string perm)
		{
			libPerms.RevokeUserPermission(Id, perm);
		}

		public bool BelongsToGroup(string group)
		{
			return libPerms.UserHasGroup(Id, group);
		}

		public void AddToGroup(string group)
		{
			libPerms.AddUserGroup(Id, group);
		}

		public void RemoveFromGroup(string group)
		{
			libPerms.RemoveUserGroup(Id, group);
		}

		public bool Equals(IPlayer other)
		{
			return Id == other?.Id;
		}

		public override bool Equals(object obj)
		{
			if (obj is IPlayer)
			{
				return Id == ((IPlayer)obj).Id;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public override string ToString()
		{
			return "Covalence.RustPlayer[" + Id + ", " + Name + "]";
		}
	}
	public class RustPlayerManager : IPlayerManager
	{
		[ProtoContract(ImplicitFields = ImplicitFields.AllFields)]
		private struct PlayerRecord
		{
			public string Name;

			public ulong Id;
		}

		private IDictionary<string, PlayerRecord> playerData;

		private IDictionary<string, RustPlayer> allPlayers;

		private IDictionary<string, RustPlayer> connectedPlayers;

		public IEnumerable<IPlayer> All => allPlayers.Values.Cast<IPlayer>();

		public IEnumerable<IPlayer> Connected => connectedPlayers.Values.Cast<IPlayer>();

		public IEnumerable<IPlayer> Sleeping => BasePlayer.sleepingPlayerList.Select((BasePlayer p) => p.IPlayer);

		internal void Initialize()
		{
			Oxide.Core.Utility.DatafileToProto<Dictionary<string, PlayerRecord>>("oxide.covalence");
			playerData = ProtoStorage.Load<Dictionary<string, PlayerRecord>>(new string[1] { "oxide.covalence" }) ?? new Dictionary<string, PlayerRecord>();
			allPlayers = new Dictionary<string, RustPlayer>();
			connectedPlayers = new Dictionary<string, RustPlayer>();
			foreach (KeyValuePair<string, PlayerRecord> playerDatum in playerData)
			{
				allPlayers.Add(playerDatum.Key, new RustPlayer(playerDatum.Value.Id, playerDatum.Value.Name));
			}
		}

		internal void PlayerJoin(ulong userId, string name)
		{
			string key = userId.ToString();
			if (playerData.TryGetValue(key, out var value))
			{
				value.Name = name;
				playerData[key] = value;
				allPlayers.Remove(key);
				allPlayers.Add(key, new RustPlayer(userId, name));
			}
			else
			{
				value = new PlayerRecord
				{
					Id = userId,
					Name = name
				};
				playerData.Add(key, value);
				allPlayers.Add(key, new RustPlayer(userId, name));
			}
		}

		internal void PlayerConnected(BasePlayer player)
		{
			allPlayers[player.UserIDString] = new RustPlayer(player);
			connectedPlayers[player.UserIDString] = new RustPlayer(player);
		}

		internal void PlayerDisconnected(BasePlayer player)
		{
			connectedPlayers.Remove(player.UserIDString);
		}

		internal void SavePlayerData()
		{
			ProtoStorage.Save(playerData, "oxide.covalence");
		}

		public IPlayer FindPlayerById(string id)
		{
			if (!allPlayers.TryGetValue(id, out var value))
			{
				return null;
			}
			return value;
		}

		public IPlayer FindPlayerByObj(object obj)
		{
			return connectedPlayers.Values.FirstOrDefault((RustPlayer p) => p.Object == obj);
		}

		public IPlayer FindPlayer(string partialNameOrId)
		{
			IPlayer[] array = FindPlayers(partialNameOrId).ToArray();
			if (array.Length != 1)
			{
				return null;
			}
			return array[0];
		}

		public IEnumerable<IPlayer> FindPlayers(string partialNameOrId)
		{
			List<IPlayer> list = new List<IPlayer>();
			foreach (RustPlayer value in connectedPlayers.Values)
			{
				if (value.Name.Equals(partialNameOrId, StringComparison.OrdinalIgnoreCase) || value.Id == partialNameOrId)
				{
					list = new List<IPlayer> { value };
					break;
				}
				if (value.Name.IndexOf(partialNameOrId, StringComparison.OrdinalIgnoreCase) >= 0)
				{
					list.Add(value);
				}
			}
			if (list.Count() > 0)
			{
				return list;
			}
			foreach (RustPlayer value2 in allPlayers.Values)
			{
				if (value2.Name.Equals(partialNameOrId, StringComparison.OrdinalIgnoreCase) || value2.Id == partialNameOrId)
				{
					list = new List<IPlayer> { value2 };
					break;
				}
				if (value2.Name.IndexOf(partialNameOrId, StringComparison.OrdinalIgnoreCase) >= 0)
				{
					list.Add(value2);
				}
			}
			return list;
		}
	}
	public class RustServer : IServer
	{
		internal readonly Server Server = new Server();

		private static IPAddress address;

		private static IPAddress localAddress;

		public string Name
		{
			get
			{
				return ConVar.Server.hostname;
			}
			set
			{
				ConVar.Server.hostname = value;
			}
		}

		public IPAddress Address
		{
			get
			{
				try
				{
					if (address == null || !Oxide.Core.Utility.ValidateIPv4(address.ToString()))
					{
						if (Oxide.Core.Utility.ValidateIPv4(ConVar.Server.ip) && !Oxide.Core.Utility.IsLocalIP(ConVar.Server.ip))
						{
							IPAddress.TryParse(ConVar.Server.ip, out address);
							Interface.Oxide.LogInfo($"IP address from command-line: {address}");
						}
						else
						{
							IPAddress.TryParse(new WebClient().DownloadString("http://api.ipify.org"), out address);
							Interface.Oxide.LogInfo($"IP address from external API: {address}");
						}
					}
					return address;
				}
				catch (Exception exception)
				{
					RemoteLogger.Exception("Couldn't get server's public IP address", exception);
					return IPAddress.Any;
				}
			}
		}

		public IPAddress LocalAddress
		{
			get
			{
				try
				{
					return localAddress ?? (localAddress = Oxide.Core.Utility.GetLocalIP());
				}
				catch (Exception exception)
				{
					RemoteLogger.Exception("Couldn't get server's local IP address", exception);
					return IPAddress.Any;
				}
			}
		}

		public ushort Port => (ushort)ConVar.Server.port;

		public string Version => BuildInfo.Current.Build.Number;

		public string Protocol => global::Rust.Protocol.printable;

		public CultureInfo Language => CultureInfo.InstalledUICulture;

		public int Players => BasePlayer.activePlayerList.Count;

		public int MaxPlayers
		{
			get
			{
				return ConVar.Server.maxplayers;
			}
			set
			{
				ConVar.Server.maxplayers = value;
			}
		}

		public DateTime Time
		{
			get
			{
				return TOD_Sky.Instance.Cycle.DateTime;
			}
			set
			{
				TOD_Sky.Instance.Cycle.DateTime = value;
			}
		}

		public SaveInfo SaveInfo { get; } = SaveInfo.Create(World.SaveFileName);

		public void Ban(string id, string reason, TimeSpan duration = default(TimeSpan))
		{
			if (!IsBanned(id))
			{
				long expiry = -1L;
				if (duration != TimeSpan.Zero)
				{
					expiry = new DateTimeOffset(DateTime.UtcNow.Add(duration)).ToUnixTimeSeconds();
				}
				ServerUsers.Set(ulong.Parse(id), ServerUsers.UserGroup.Banned, Name, reason, expiry);
				ServerUsers.Save();
			}
		}

		public TimeSpan BanTimeRemaining(string id)
		{
			if (!IsBanned(id))
			{
				return TimeSpan.Zero;
			}
			return TimeSpan.MaxValue;
		}

		public bool IsBanned(string id)
		{
			return ServerUsers.Is(ulong.Parse(id), ServerUsers.UserGroup.Banned);
		}

		public void Save()
		{
			ConVar.Server.save(null);
			File.WriteAllText(ConVar.Server.GetServerFolder("cfg") + "/serverauto.cfg", ConsoleSystem.SaveToConfigString(bServer: true));
			ServerUsers.Save();
		}

		public void Unban(string id)
		{
			if (IsBanned(id))
			{
				ServerUsers.Remove(ulong.Parse(id));
				ServerUsers.Save();
			}
		}

		public void Broadcast(string message, string prefix, params object[] args)
		{
			Server.Broadcast(message, prefix, 0uL, args);
		}

		public void Broadcast(string message)
		{
			Broadcast(message, null);
		}

		public void Command(string command, params object[] args)
		{
			Server.Command(command, args);
		}
	}
}
