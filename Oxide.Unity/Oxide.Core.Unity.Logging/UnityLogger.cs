using System.Threading;
using Oxide.Core.Logging;
using UnityEngine;

namespace Oxide.Core.Unity.Logging;

public sealed class UnityLogger : Oxide.Core.Logging.Logger
{
	private readonly Thread mainThread = Thread.CurrentThread;

	public UnityLogger()
		: base(processImmediately: true)
	{
	}

	protected override void ProcessMessage(LogMessage message)
	{
		if (Thread.CurrentThread != mainThread)
		{
			Interface.Oxide.NextTick(delegate
			{
				ProcessMessage(message);
			});
			return;
		}
		switch (message.Type)
		{
		case Oxide.Core.Logging.LogType.Error:
			Debug.LogError(message.ConsoleMessage);
			break;
		case Oxide.Core.Logging.LogType.Warning:
			Debug.LogWarning(message.ConsoleMessage);
			break;
		default:
			Debug.Log(message.ConsoleMessage);
			break;
		}
	}
}
