using System;
using Steamworks;

namespace Rust.Platform.Steam;

public class SteamAuthTicket : IAuthTicket, IDisposable
{
	private readonly AuthTicket _ticket;

	public string Token { get; }

	public byte[] Data { get; }

	internal SteamAuthTicket(AuthTicket ticket)
	{
		_ticket = ticket ?? throw new ArgumentNullException("ticket");
		Token = BitConverter.ToString(ticket.Data).Replace("-", "");
		Data = ticket.Data;
	}

	public void Dispose()
	{
		_ticket?.Dispose();
	}
}
