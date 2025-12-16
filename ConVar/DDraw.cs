using System.Linq;
using UnityEngine;

namespace ConVar;

public class DDraw
{
	[ServerVar]
	public static void ddrawother(ConsoleSystem.Arg arg)
	{
		arg.GetULong(0, 0uL);
		BasePlayer player = ArgEx.GetPlayer(arg, 0);
		if (player == null || !player.IsConnected)
		{
			arg.ReplyWith("Player not found");
			return;
		}
		if (arg.Args.Length < 2)
		{
			arg.ReplyWith("Usage: ddrawother <player> <ddraw.text arguments>");
			return;
		}
		string command = ConsoleSystem.BuildCommand("ddraw.text", arg.Args.Skip(1));
		player.SendConsoleCommand(command);
	}
}
