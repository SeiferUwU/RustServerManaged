using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facepunch;
using Facepunch.Extend;
using Network;
using Newtonsoft.Json;
using Oxide.Core;
using UnityEngine;

public class ConsoleSystem
{
	public class Arg
	{
		public Option Option;

		public Command cmd;

		public string RawCommand;

		public string FullString = "";

		public string[] Args;

		public bool Invalid = true;

		public string Reply = "";

		public bool Silent;

		public bool IsClientside => Option.IsClient;

		public bool IsServerside => Option.IsServer;

		public Connection Connection => Option.Connection;

		public bool IsConnectionAdmin
		{
			get
			{
				if (Option.Connection != null && Option.Connection.connected && Option.Connection.authLevel != 0)
				{
					return Option.Connection.trusted;
				}
				return false;
			}
		}

		public bool IsAdmin
		{
			get
			{
				if (!IsConnectionAdmin)
				{
					return IsRcon;
				}
				return true;
			}
		}

		public bool IsRcon => Option.FromRcon;

		public Arg(Option options, string rconCommand)
		{
			Option = options;
			BuildCommand(rconCommand);
		}

		internal void BuildCommand(string command)
		{
			RawCommand = command;
			if (string.IsNullOrEmpty(command))
			{
				Invalid = true;
				return;
			}
			if (command.IndexOf('.') <= 0 || command.IndexOf(' ', 0, command.IndexOf('.')) != -1)
			{
				command = "global." + command;
			}
			int num = command.IndexOf('.');
			if (num <= 0)
			{
				return;
			}
			string text = command.Substring(0, num);
			if (text.Length < 1)
			{
				return;
			}
			text = text.Trim().ToLower();
			string text2 = command.Substring(num + 1);
			if (text2.Length >= 1)
			{
				int num2 = text2.IndexOf(' ');
				if (num2 > 0)
				{
					FullString = text2.Substring(num2 + 1);
					FullString = FullString.Trim();
					Args = FullString.SplitQuotesStrings(16);
					text2 = text2.Substring(0, num2);
				}
				text2 = text2.Trim().ToLower();
				if (cmd == null && Option.IsClient)
				{
					cmd = Index.Client.Find(text + "." + text2);
				}
				if (cmd == null && Option.IsServer)
				{
					cmd = Index.Server.Find(text + "." + text2);
				}
				Invalid = cmd == null;
			}
		}

		internal bool HasPermission()
		{
			if (cmd == null)
			{
				return false;
			}
			if (Option.IsUnrestricted)
			{
				return true;
			}
			if (IsClientside)
			{
				if (cmd.ClientAdmin)
				{
					if (ClientCanRunAdminCommands != null)
					{
						return ClientCanRunAdminCommands();
					}
					return false;
				}
				if (Option.IsFromServer && !cmd.AllowRunFromServer)
				{
					Debug.Log("Server tried to run command \"" + FullString + "\", but we blocked it.");
					return false;
				}
				return cmd.Client;
			}
			if (cmd.ServerAdmin)
			{
				if (cmd.RconOnly && (Connection != null || Option.FromCommandBlock))
				{
					return false;
				}
				if (IsRcon)
				{
					return true;
				}
				if (IsAdmin)
				{
					return true;
				}
			}
			if (cmd.ServerUser && Connection != null)
			{
				return true;
			}
			return false;
		}

		internal bool CanSeeInFind(Command command)
		{
			if (command == null)
			{
				return false;
			}
			if (Option.IsUnrestricted)
			{
				return true;
			}
			if (command.RconOnly && (Connection != null || Option.FromCommandBlock))
			{
				return false;
			}
			if (IsClientside)
			{
				return command.Client;
			}
			if (IsServerside)
			{
				return command.Server;
			}
			return false;
		}

		public void ReplyWith(string strValue)
		{
			Reply = strValue;
		}

		public void ReplyWith(object obj)
		{
			Reply = JsonConvert.SerializeObject(obj, Formatting.Indented);
		}

		public bool HasArgs(int iMinimum = 1)
		{
			if (Args == null)
			{
				return false;
			}
			return Args.Length >= iMinimum;
		}

		public bool HasArg(string value, bool remove = false)
		{
			if (Args == null)
			{
				return false;
			}
			if (Array.IndexOf(Args, value) == -1)
			{
				return false;
			}
			if (remove)
			{
				Args = Args.Where((string x) => x != value).ToArray();
			}
			return true;
		}

		public bool TryRemoveKeyBindEventArgs()
		{
			if (Args == null)
			{
				return false;
			}
			int num = Args.Length;
			Args = Args.Where((string x) => x != "True" && x != "False").ToArray();
			return Args.Length != num;
		}

		public string GetString(int iArg, string def = "")
		{
			if (HasArgs(iArg + 1))
			{
				return Args[iArg];
			}
			return def;
		}

		public int GetInt(int iArg, int def = 0)
		{
			string text = GetString(iArg, null);
			if (text == null)
			{
				return def;
			}
			if (int.TryParse(text, out var result))
			{
				return result;
			}
			return def;
		}

		public long GetLong(int iArg, long def = 0L)
		{
			string text = GetString(iArg, null);
			if (text == null)
			{
				return def;
			}
			if (long.TryParse(text, out var result))
			{
				return result;
			}
			return def;
		}

		public ulong GetULong(int iArg, ulong def = 0uL)
		{
			string text = GetString(iArg, null);
			if (text == null)
			{
				return def;
			}
			if (ulong.TryParse(text, out var result))
			{
				return result;
			}
			return def;
		}

		public bool TryGetUInt(int iArg, out uint value)
		{
			string text = GetString(iArg, null);
			if (text == null)
			{
				value = 0u;
				return false;
			}
			return uint.TryParse(text, out value);
		}

		public uint GetUInt(int iArg, uint def = 0u)
		{
			if (!TryGetUInt(iArg, out var value))
			{
				return def;
			}
			return value;
		}

		public ulong GetUInt64(int iArg, ulong def = 0uL)
		{
			string text = GetString(iArg, null);
			if (text == null)
			{
				return def;
			}
			if (ulong.TryParse(text, out var result))
			{
				return result;
			}
			return def;
		}

		public float GetFloat(int iArg, float def = 0f)
		{
			string text = GetString(iArg, null);
			if (text == null)
			{
				return def;
			}
			if (float.TryParse(text, out var result))
			{
				return result;
			}
			return def;
		}

		public bool GetBool(int iArg, bool def = false)
		{
			string text = GetString(iArg, null);
			if (text == null)
			{
				return def;
			}
			if (text == string.Empty || text == "0")
			{
				return false;
			}
			if (text.Equals("f", StringComparison.InvariantCultureIgnoreCase))
			{
				return false;
			}
			if (text.Equals("false", StringComparison.InvariantCultureIgnoreCase))
			{
				return false;
			}
			if (text.Equals("no", StringComparison.InvariantCultureIgnoreCase))
			{
				return false;
			}
			if (text.Equals("none", StringComparison.InvariantCultureIgnoreCase))
			{
				return false;
			}
			if (text.Equals("null", StringComparison.InvariantCultureIgnoreCase))
			{
				return false;
			}
			return true;
		}

		public long GetTimestamp(int iArg, long def = 0L)
		{
			string text = GetString(iArg, null);
			if (text == null)
			{
				return def;
			}
			int num = 3600;
			if (text.Length > 1 && char.IsLetter(text[text.Length - 1]))
			{
				switch (text[text.Length - 1])
				{
				case 's':
					num = 1;
					break;
				case 'm':
					num = 60;
					break;
				case 'h':
					num = 3600;
					break;
				case 'd':
					num = 86400;
					break;
				case 'w':
					num = 604800;
					break;
				case 'M':
					num = 2592000;
					break;
				case 'Y':
					num = 31536000;
					break;
				}
				text = text.Substring(0, text.Length - 1);
			}
			if (long.TryParse(text, out var result))
			{
				if (result > 0 && result <= 315360000)
				{
					return DateTimeOffset.UtcNow.ToUnixTimeSeconds() + result * num;
				}
				return result;
			}
			return def;
		}

		public long GetTicks(int iArg, long def = 0L)
		{
			string text = GetString(iArg, null);
			if (text == null)
			{
				return def;
			}
			int num = 3600;
			if (text.Length > 1 && char.IsLetter(text[text.Length - 1]))
			{
				switch (text[text.Length - 1])
				{
				case 's':
					num = 1;
					break;
				case 'm':
					num = 60;
					break;
				case 'h':
					num = 3600;
					break;
				case 'd':
					num = 86400;
					break;
				case 'w':
					num = 604800;
					break;
				case 'M':
					num = 2592000;
					break;
				case 'Y':
					num = 31536000;
					break;
				}
				text = text.Substring(0, text.Length - 1);
			}
			if (long.TryParse(text, out var result))
			{
				return result * num * 10000000;
			}
			return def;
		}

		public void ReplyWithObject(object rval)
		{
			if (rval != null)
			{
				if (rval is string)
				{
					ReplyWith((string)rval);
					return;
				}
				string strValue = JsonConvert.SerializeObject(rval, Formatting.Indented);
				ReplyWith(strValue);
			}
		}

		public Vector3 GetVector3(int iArg, Vector3 def = default(Vector3))
		{
			return GetString(iArg, null)?.ToVector3() ?? def;
		}

		public Color GetColor(int iArg, Color def = default(Color))
		{
			return GetString(iArg, null)?.ToColor() ?? def;
		}
	}

	public class Factory : Attribute
	{
		public string Name;

		public Factory(string systemName)
		{
			Name = systemName;
		}
	}

	public class Command
	{
		public string Name;

		public string Parent;

		public string FullName;

		public Func<string> GetOveride;

		public Action<string> SetOveride;

		public Action<Arg> Call;

		public bool Variable;

		public string Default;

		public string DefaultValue;

		public bool Saved;

		public bool ServerAdmin;

		public bool ServerUser;

		public bool RconOnly;

		public bool Replicated;

		public bool ShowInAdminUI;

		public bool ClientAdmin;

		public bool Client;

		public bool ClientInfo;

		public bool AllowRunFromServer;

		public string Description = string.Empty;

		public string Arguments = string.Empty;

		public bool Server
		{
			get
			{
				if (!ServerAdmin)
				{
					return ServerUser;
				}
				return true;
			}
		}

		public string String => GetOveride?.Invoke() ?? "";

		public int AsInt => String.ToInt();

		public float AsFloat => String.ToFloat();

		public bool AsBool => String.ToBool();

		public Vector3 AsVector3 => String.ToVector3();

		public event Action<Command> OnValueChanged;

		public Command()
		{
			Call = DefaultCall;
		}

		private void ValueChanged()
		{
			if (Saved)
			{
				HasChanges = true;
			}
			if (ClientInfo)
			{
				SendToServer(BuildCommand("setinfo", FullName, String));
			}
			if (this.OnValueChanged != null)
			{
				this.OnValueChanged(this);
			}
		}

		private void DefaultCall(Arg arg)
		{
			if (SetOveride != null && arg.HasArgs())
			{
				if (arg.IsClientside && Replicated)
				{
					SendToServer(arg.RawCommand);
					arg.Silent = true;
					Debug.LogWarning("ConVar '" + Name + "' will be replicated to all other players on the server");
				}
				else
				{
					Set(arg.Args[0]);
				}
			}
		}

		public void Set(string value)
		{
			if (SetOveride != null)
			{
				string text = String;
				SetOveride(value);
				if (text != String)
				{
					ValueChanged();
				}
			}
		}

		public void Set(float f)
		{
			string text = f.ToString("0.00");
			if (!(String == text))
			{
				Set(text);
			}
		}

		public void Set(bool val)
		{
			if (AsBool != val)
			{
				Set(val ? "1" : "0");
			}
		}
	}

	public interface IConsoleCommand
	{
		void Call(Arg arg);
	}

	public interface IConsoleButton
	{
		bool IsPressed { get; set; }
	}

	public static class Index
	{
		public static class Server
		{
			public static Dictionary<string, Command> Dict = new Dictionary<string, Command>(StringComparer.OrdinalIgnoreCase);

			public static Dictionary<string, Command> GlobalDict = new Dictionary<string, Command>(StringComparer.OrdinalIgnoreCase);

			public static List<Command> Replicated = new List<Command>();

			public static Command Find(string strName)
			{
				if (!strName.Contains("."))
				{
					strName = "global." + strName;
				}
				if (Dict.TryGetValue(strName, out var value))
				{
					return value;
				}
				GlobalDict.TryGetValue(strName.Replace("global.", ""), out value);
				return value;
			}
		}

		public static class Client
		{
			public static Dictionary<string, Command> Dict = new Dictionary<string, Command>(StringComparer.OrdinalIgnoreCase);

			public static Dictionary<string, Command> GlobalDict = new Dictionary<string, Command>(StringComparer.OrdinalIgnoreCase);

			public static Command Find(string strName)
			{
				if (!strName.Contains("."))
				{
					strName = WithGlobal.Get(strName);
				}
				if (Dict.TryGetValue(strName, out var value))
				{
					return value;
				}
				GlobalDict.TryGetValue(WithoutGlobal.Get(strName), out value);
				return value;
			}
		}

		private static readonly Memoized<string, string> WithGlobal = new Memoized<string, string>((string s) => "global." + s);

		private static readonly Memoized<string, string> WithoutGlobal = new Memoized<string, string>((string s) => s.Replace("global.", ""));

		public static Command[] All { get; set; }

		public static void Initialize(Command[] Commands)
		{
			Command[] array = Commands;
			foreach (Command command in array)
			{
				if (command.Variable && command.GetOveride != null)
				{
					try
					{
						command.DefaultValue = command.GetOveride() ?? "";
					}
					catch
					{
					}
				}
			}
			All = Commands;
			Server.Dict = new Dictionary<string, Command>();
			Client.Dict = new Dictionary<string, Command>();
			array = All;
			foreach (Command command2 in array)
			{
				if (command2.Server)
				{
					if (Server.Dict.ContainsKey(command2.FullName))
					{
						Debug.LogWarning("Server Vars have multiple entries for " + command2.FullName);
					}
					else
					{
						Server.Dict.Add(command2.FullName, command2);
					}
					if (command2.Parent != "global" && !Server.GlobalDict.ContainsKey(command2.Name))
					{
						Server.GlobalDict.Add(command2.Name, command2);
					}
					if (command2.Replicated)
					{
						if (!command2.Variable || !command2.ServerAdmin)
						{
							Debug.LogWarning("Replicated server var " + command2.FullName + " has a bad config");
						}
						else
						{
							Server.Replicated.Add(command2);
							command2.OnValueChanged += delegate(Command command3)
							{
								ConsoleSystem.OnReplicatedVarChanged?.Invoke(command3.FullName, command3.String);
							};
						}
					}
				}
				if (command2.Client)
				{
					if (Client.Dict.ContainsKey(command2.FullName))
					{
						Debug.LogWarning("Client Vars have multiple entries for " + command2.FullName);
					}
					else
					{
						Client.Dict.Add(command2.FullName, command2);
					}
					if (command2.Parent != "global" && !Client.GlobalDict.ContainsKey(command2.Name))
					{
						Client.GlobalDict.Add(command2.Name, command2);
					}
				}
			}
			Input.RunBind += delegate(string strCommand, bool pressed)
			{
				Command command3 = Client.Find(strCommand);
				if (command3 != null && command3.Variable && !command3.ClientAdmin && !command3.ServerAdmin && !command3.Replicated)
				{
					command3.Set(pressed);
				}
				else
				{
					Run(Option.Client, $"{strCommand} {pressed}");
				}
			};
		}

		public static void Reset()
		{
			if (All == null)
			{
				return;
			}
			Command[] all = All;
			foreach (Command command in all)
			{
				if (command.Variable && command.Default != null)
				{
					try
					{
						command.Set(command.Default);
					}
					catch (Exception arg)
					{
						Debug.LogError($"Exception running {command.FullName} = {command.Default}: {arg}");
					}
				}
			}
		}
	}

	public struct Option
	{
		public string RconIP;

		public string RconName;

		public static Option Unrestricted => new Option
		{
			IsServer = true,
			IsClient = true,
			ForwardtoServerOnMissing = true,
			PrintOutput = true,
			IsUnrestricted = true
		};

		public static Option Client => new Option
		{
			IsClient = true,
			ForwardtoServerOnMissing = true,
			PrintOutput = true
		};

		public static Option Server => new Option
		{
			IsServer = true,
			PrintOutput = true,
			FromRcon = true
		};

		public bool IsServer { get; set; }

		public bool IsClient { get; set; }

		public bool ForwardtoServerOnMissing { get; set; }

		public bool PrintOutput { get; set; }

		public bool IsUnrestricted { get; set; }

		public bool FromRcon { get; set; }

		public bool PrintValueOnly { get; set; }

		public bool IsFromServer { get; set; }

		public bool FromCommandBlock { get; set; }

		public Connection Connection { get; set; }

		public bool ServerConsole { get; set; }

		public bool ConfigFile { get; set; }

		public int RconConnectionId { get; set; }

		public Option Quiet()
		{
			PrintOutput = false;
			return this;
		}

		public Option PrintValue()
		{
			PrintValueOnly = true;
			return this;
		}

		public Option FromServer()
		{
			IsFromServer = true;
			return this;
		}

		public Option FromConnection(Connection connection)
		{
			FromRcon = false;
			Connection = connection;
			return this;
		}

		public Option FromRconConnection(int id, string ip, string connectionName)
		{
			RconConnectionId = id;
			RconIP = ip;
			RconName = connectionName;
			return this;
		}

		public Option FromServerConsole()
		{
			ServerConsole = true;
			return this;
		}

		public Option FromConfigFile()
		{
			ConfigFile = true;
			return this;
		}
	}

	public struct CommandResult
	{
		public CommandResultType Result;

		public string Output;

		public Command Command;

		public CommandResult(CommandResultType result, string output, Command command)
		{
			Result = result;
			Output = output;
			Command = command;
		}
	}

	public enum CommandResultType
	{
		Unknown,
		Success,
		PermissionDenied,
		CommandNotFound
	}

	public static bool HasChanges = false;

	public static Func<bool> ClientCanRunAdminCommands;

	public static bool loggingEnabled = false;

	public static string IdentityDirectory;

	private static StreamWriter _logWriter;

	private static DateTime _logTimestamp;

	private static ConcurrentQueue<string> _logQueue = new ConcurrentQueue<string>();

	private static Task _logThread;

	public static Func<string, bool> OnSendToServer;

	public static string LastError = null;

	public static Arg CurrentArgs = null;

	private static List<string> ignoredCommands = new List<string> { "projectpath", "useHub", "hubIPC", "cloudEnvironment", "licensingIpc", "hubSessionId", "accessToken" };

	public static event Action<string, string> OnReplicatedVarChanged;

	public static void UpdateValuesFromCommandLine()
	{
		if (Interface.CallHook("IOnRunCommandLine") != null)
		{
			return;
		}
		foreach (KeyValuePair<string, string> @switch in Facepunch.CommandLine.GetSwitches())
		{
			string text = @switch.Value;
			if (text == "")
			{
				text = "1";
			}
			string strCommand = @switch.Key.Substring(1);
			Run(Option.Unrestricted, strCommand, text);
		}
	}

	private static void LogToFile(Arg arg)
	{
		if (arg.Connection == null || arg.Connection.authLevel != 0 || (arg.cmd.ServerAdmin && !arg.cmd.ServerUser))
		{
			if (_logThread == null || _logThread.IsCompleted)
			{
				_logThread = LogThread();
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}] ");
			if (!string.IsNullOrEmpty(arg.Option.RconIP))
			{
				stringBuilder.Append("[RCON " + arg.Option.RconIP + " '" + arg.Option.RconName + "'] ");
			}
			else if (arg.Option.ServerConsole)
			{
				stringBuilder.Append("[SERVER CONSOLE] ");
			}
			else if (arg.Option.ConfigFile)
			{
				stringBuilder.Append("[CONFIG FILE] ");
			}
			else if (arg.Connection != null)
			{
				stringBuilder.Append($"[USER {arg.Connection.userid} {arg.Connection.ipaddress}] ");
			}
			else if (arg.IsClientside && !arg.IsServerside)
			{
				stringBuilder.Append("[CLIENTSIDE] ");
			}
			else
			{
				stringBuilder.Append("[UNKNOWN] ");
			}
			stringBuilder.Append(arg.RawCommand);
			_logQueue.Enqueue(stringBuilder.ToString());
		}
	}

	private static async Task LogThread()
	{
		while (Application.isPlaying)
		{
			await Task.Delay(1000);
			if (!_logQueue.IsEmpty)
			{
				StreamWriter writer = await GetLogStream();
				string result;
				while (_logQueue.TryDequeue(out result))
				{
					await writer.WriteLineAsync(result);
				}
				await writer.FlushAsync();
			}
		}
		await (await GetLogStream()).DisposeAsync();
	}

	private static async Task<StreamWriter> GetLogStream()
	{
		DateTime today = DateTime.UtcNow.Date;
		if (_logWriter == null || today != _logTimestamp)
		{
			if (_logWriter != null)
			{
				await _logWriter.DisposeAsync();
			}
			_logTimestamp = today;
			_logWriter = OpenLogFile();
		}
		return _logWriter;
	}

	private static StreamWriter OpenLogFile()
	{
		string text = IdentityDirectory;
		if (string.IsNullOrEmpty(text))
		{
			text = "Logs";
		}
		text = Path.Combine(text, "command_history");
		Directory.CreateDirectory(text);
		string text2 = DateTime.UtcNow.Date.ToString("yyyy-MM-dd");
		string path = "commands_" + text2 + ".log";
		FileStream fileStream = new FileStream(Path.Combine(text, path), FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
		fileStream.Position = fileStream.Length;
		return new StreamWriter(fileStream, Encoding.UTF8);
	}

	internal static bool SendToServer(string command)
	{
		if (OnSendToServer != null)
		{
			return OnSendToServer(command);
		}
		return false;
	}

	public static void RunFile(Option options, string strFile)
	{
		string[] array = strFile.Split(new char[1] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
		for (int i = 0; i < array.Length; i++)
		{
			string text = array[i].Trim();
			if (!string.IsNullOrWhiteSpace(text) && text[0] != '#')
			{
				Run(options.FromConfigFile(), text);
			}
		}
		HasChanges = false;
	}

	public static string Run(Option options, string strCommand, params object[] args)
	{
		return RunWithResult(options, strCommand, args).Output;
	}

	public static CommandResult RunWithResult(Option options, string strCommand, params object[] args)
	{
		LastError = null;
		string text = BuildCommand(strCommand, args);
		Arg arg = new Arg(options, text);
		bool flag = arg.HasPermission();
		if (!arg.Invalid && flag)
		{
			Arg currentArgs = CurrentArgs;
			CurrentArgs = arg;
			bool flag2 = Internal(arg);
			CurrentArgs = currentArgs;
			if (options.PrintOutput && flag2 && arg.Reply != null && arg.Reply.Length > 0)
			{
				DebugEx.Log(arg.Reply);
			}
			if (loggingEnabled)
			{
				LogToFile(arg);
			}
			return new CommandResult(CommandResultType.Success, arg.Reply, arg.cmd);
		}
		LastError = "Command not found";
		CommandResultType result = CommandResultType.CommandNotFound;
		if (!flag)
		{
			LastError = "Permission denied";
			result = CommandResultType.PermissionDenied;
		}
		if (!options.IsServer && (!options.ForwardtoServerOnMissing || !SendToServer(text)))
		{
			LastError = "Command '" + strCommand + "' not found";
			if (options.PrintOutput && !ignoredCommands.Contains(strCommand))
			{
				DebugEx.Log(LastError);
			}
			return new CommandResult(CommandResultType.CommandNotFound, null, null);
		}
		if (options.IsServer && options.PrintOutput)
		{
			LastError = "Command '" + strCommand + "' not found";
			if (!ignoredCommands.Contains(strCommand))
			{
				DebugEx.Log(LastError);
			}
		}
		return new CommandResult(result, null, null);
	}

	private static bool Internal(Arg arg)
	{
		if (arg.Invalid)
		{
			return false;
		}
		object obj = Interface.CallHook("IOnServerCommand", arg);
		if (obj is bool)
		{
			return (bool)obj;
		}
		if (!arg.HasPermission())
		{
			arg.ReplyWith("You cannot run this command");
			return false;
		}
		try
		{
			using (TimeWarning.New("ConsoleSystem: " + arg.cmd.FullName))
			{
				arg.cmd.Call(arg);
			}
		}
		catch (Exception ex)
		{
			arg.ReplyWith("Error: " + arg.cmd.FullName + " - " + ex.Message + " (" + ex.Source + ")");
			Debug.LogException(ex);
			return false;
		}
		if (arg.cmd.Variable && arg.cmd.GetOveride != null && string.IsNullOrWhiteSpace(arg.Reply))
		{
			string text = arg.cmd.String;
			string text2 = (arg.cmd.Variable ? arg.cmd.String : "");
			if (!arg.Silent)
			{
				if (arg.Option.PrintValueOnly)
				{
					arg.ReplyWith(text);
				}
				else if (text2 != text)
				{
					arg.ReplyWith($"{arg.cmd.FullName}: changed from {Facepunch.Extend.StringExtensions.QuoteSafe(text2)} to {Facepunch.Extend.StringExtensions.QuoteSafe(text)}");
				}
				else
				{
					arg.ReplyWith($"{arg.cmd.FullName}: {Facepunch.Extend.StringExtensions.QuoteSafe(text)}");
				}
			}
		}
		return true;
	}

	public static string BuildCommand(string strCommand, params object[] args)
	{
		if (args == null || args.Length == 0)
		{
			return strCommand;
		}
		StringBuilder obj = Pool.Get<StringBuilder>();
		obj.Clear();
		obj.Append(strCommand);
		foreach (object obj2 in args)
		{
			if (obj2 == null)
			{
				obj.Append(" \"\"");
			}
			else if (obj2 is Color color)
			{
				obj.Append(" \"").Append(color.r).Append(',')
					.Append(color.g)
					.Append(',')
					.Append(color.b)
					.Append(',')
					.Append(color.a)
					.Append('"');
			}
			else if (obj2 is Vector3 vector)
			{
				obj.Append(" \"").Append(vector.x).Append(',')
					.Append(vector.y)
					.Append(',')
					.Append(vector.z)
					.Append('"');
			}
			else if (obj2 is IEnumerable<string> enumerable)
			{
				foreach (string item in enumerable)
				{
					obj.Append(' ').QuoteSafe(item);
				}
			}
			else
			{
				obj.Append(' ').QuoteSafe(obj2.ToString());
			}
		}
		string result = obj.ToString();
		obj.Clear();
		Pool.FreeUnmanaged(ref obj);
		return result;
	}

	public static string SaveToConfigString(bool bServer)
	{
		string text = "";
		IEnumerable<Command> enumerable = ((!bServer) ? Index.All.Where((Command x) => x.Saved && x.Client && !x.Replicated) : Index.All.Where((Command x) => x.Saved && x.ServerAdmin));
		foreach (Command item in enumerable)
		{
			if (item.GetOveride != null)
			{
				text = text + item.FullName + " " + Facepunch.Extend.StringExtensions.QuoteSafe(item.String);
				text += Environment.NewLine;
			}
		}
		return text;
	}
}
