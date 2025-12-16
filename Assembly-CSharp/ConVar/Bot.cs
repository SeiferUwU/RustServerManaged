using Facepunch;
using UnityEngine;

namespace ConVar;

[Factory("bot")]
public class Bot : ConsoleSystem
{
	[ServerVar(ShowInAdminUI = false)]
	public static string sv_exec_command(Arg args)
	{
		if (TryGetBotServer(args, out var bot, out var error))
		{
			return error;
		}
		string text = args.GetString(1);
		if (string.IsNullOrEmpty(text))
		{
			return "No command provided";
		}
		bot.Command(text);
		return string.Empty;
	}

	[ServerVar(ShowInAdminUI = false)]
	public static string sv_exec_command_sphere(Arg args)
	{
		string text = args.GetString(1);
		if (string.IsNullOrEmpty(text))
		{
			return "invalid command";
		}
		BasePlayer basePlayer = ArgEx.Player(args);
		if (basePlayer == null)
		{
			return "no player context";
		}
		using PooledList<BasePlayer> pooledList = Facepunch.Pool.Get<PooledList<BasePlayer>>();
		global::Vis.Entities(basePlayer.transform.position, args.GetFloat(0, 50f), pooledList);
		int num = 0;
		foreach (BasePlayer item in pooledList)
		{
			if (item.IsBot && item.isServer && !item.IsNpc)
			{
				item.Command(text);
				num++;
			}
		}
		return $"Executed command on {num} bots.";
	}

	[ServerVar(ShowInAdminUI = false)]
	public static string sv_exec_command_all(Arg args)
	{
		string text = args.GetString(0);
		if (string.IsNullOrEmpty(text))
		{
			return "invalid command";
		}
		int num = 0;
		foreach (BasePlayer bot in BasePlayer.bots)
		{
			if (bot.IsBot && bot.isServer && !bot.IsNpc)
			{
				num++;
				bot.Command(text);
			}
		}
		return $"Executed command on {num} bots.";
	}

	[ServerVar(ShowInAdminUI = false)]
	public static string crouch_server(Arg args)
	{
		if (TryGetBotServer(args, out var bot, out var error))
		{
			return error;
		}
		bot.modelState.ducked = args.GetBool(0, def: true);
		bot.SendNetworkUpdate();
		return "Crouched " + bot.displayName + ".";
	}

	private static bool TryGetBotServer(Arg args, out BasePlayer bot, out string error)
	{
		ulong uLong = args.GetULong(0, 0uL);
		if (uLong == 0L)
		{
			bot = null;
			error = "No user id";
			return true;
		}
		bot = BasePlayer.FindBot(uLong);
		if (bot == null || bot.IsNpc)
		{
			error = $"No bot found with id{uLong}";
			return true;
		}
		error = string.Empty;
		return false;
	}
}
