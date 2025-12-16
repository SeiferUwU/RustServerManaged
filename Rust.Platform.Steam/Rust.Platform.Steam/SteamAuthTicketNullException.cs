using System;

namespace Rust.Platform.Steam;

public sealed class SteamAuthTicketNullException : Exception
{
	public SteamAuthTicketNullException(string message)
		: base(message)
	{
	}
}
