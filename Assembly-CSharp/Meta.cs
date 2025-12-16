using System.Collections.Generic;
using Facepunch;

[Factory("meta")]
public class Meta : ConsoleSystem
{
	[ServerVar(Clientside = true, Help = "add <convar> <amount> - adds amount to convar")]
	public static void add(Arg args)
	{
		string text = args.GetString(0);
		float num = args.GetFloat(1, 0.1f);
		Command command = Find(text);
		float result;
		if (command == null)
		{
			args.ReplyWith("Convar not found: " + (text ?? "<null>"));
		}
		else if (args.IsClientside && command.Replicated)
		{
			args.ReplyWith("Cannot set replicated convars from the client (use sv to do this)");
		}
		else if (args.IsServerside && command.ServerAdmin && !args.IsAdmin)
		{
			args.ReplyWith("Permission denied");
		}
		else if (!float.TryParse(command.String, out result))
		{
			args.ReplyWith("Convar value cannot be parsed as a number");
		}
		else
		{
			command.Set(result + num);
		}
	}

	[ClientVar(Help = "if_true <command> <condition> - runs a command if the condition is true")]
	public static void if_true(Arg args)
	{
		string strCommand = args.GetString(0);
		bool flag = args.GetBool(1);
		if (flag)
		{
			ConsoleSystem.Run(Option.Client, strCommand, flag);
		}
	}

	[ClientVar(Help = "if_false <command> <condition> - runs a command if the condition is false")]
	public static void if_false(Arg args)
	{
		string strCommand = args.GetString(0);
		bool flag = args.GetBool(1, def: true);
		if (!flag)
		{
			ConsoleSystem.Run(Option.Client, strCommand, flag);
		}
	}

	[ClientVar(Help = "reset_cycle <key> - resets a cycled bind to the beginning")]
	public static void reset_cycle(Arg args)
	{
		string name = args.GetString(0);
		KeyCombos.TryParse(ref name, out var _);
		Input.Button button = Input.GetButton(name);
		if (button == null)
		{
			args.ReplyWith("Button not found");
		}
		else if (!button.Cycle)
		{
			args.ReplyWith("Button does not have a cycled bind");
		}
		else
		{
			button.CycleIndex = 0;
		}
	}

	[ClientVar(Help = "exec [command_1] ... - runs all of the commands passed as arguments (also, if the last argument is true/false then that will flow into each command's arguments)")]
	public static void exec(Arg args)
	{
		List<string> obj = Pool.Get<List<string>>();
		for (int i = 0; i < 32; i++)
		{
			string text = args.GetString(i);
			if (string.IsNullOrWhiteSpace(text))
			{
				break;
			}
			obj.Add(text);
		}
		if (obj.Count > 0)
		{
			string text2 = null;
			string text3 = obj[obj.Count - 1];
			if (bool.TryParse(text3, out var _))
			{
				text2 = text3;
				obj.RemoveAt(obj.Count - 1);
			}
			foreach (string item in obj)
			{
				if (text2 != null)
				{
					ConsoleSystem.Run(Option.Client, item, text2);
				}
				else
				{
					ConsoleSystem.Run(Option.Client, item);
				}
			}
		}
		Pool.FreeUnmanaged(ref obj);
	}

	private static Command Find(string name)
	{
		Command command = Index.Server.Find(name);
		if (command != null)
		{
			return command;
		}
		return null;
	}
}
