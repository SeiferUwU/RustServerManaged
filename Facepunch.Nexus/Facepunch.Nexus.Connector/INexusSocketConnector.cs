using System;

namespace Facepunch.Nexus.Connector;

internal interface INexusSocketConnector : IDisposable
{
	bool IsStarted { get; }

	bool IsConnected { get; }

	void Start();

	bool TryReceive(out NexusMessage message);

	void Acknowledge(string messageId);
}
