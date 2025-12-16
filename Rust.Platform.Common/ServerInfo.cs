using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Facepunch;

public readonly struct ServerInfo : IEquatable<ServerInfo>
{
	public enum Protocol
	{
		Default,
		Raknet,
		Steamworks
	}

	private static readonly HashSet<StringView> EmptyTags = new HashSet<StringView>();

	public static readonly BufferList<string> ModePriority = new BufferList<string>
	{
		"event", "minigame", "battlefield", "builds", "training", "roleplay", "creative", "gmhardcore", "gmsoftcore", "gmprimitive",
		"pvp", "pve", "vanilla"
	};

	public uint AppId { get; }

	public string Name { get; }

	public IPAddress Address { get; }

	public uint AddressRaw { get; }

	public int ConnectionPort { get; }

	public int QueryPort { get; }

	public string Map { get; }

	public string TagString { get; }

	public bool IsSecure { get; }

	public int Players { get; }

	public int MaxPlayers { get; }

	public uint LastTimePlayed { get; }

	public int Ping { get; }

	public ulong SteamId { get; }

	public StringView RegionCode { get; }

	public int MaxTeamSize { get; }

	public string Mode { get; }

	public int ModeSortIndex { get; }

	public uint Born { get; }

	public HashSet<StringView> Tags { get; }

	public bool HasPremiumTag
	{
		get
		{
			if (!Tags.Contains("premium"))
			{
				return Tags.Contains("^q");
			}
			return true;
		}
	}

	public int QueuedPlayers { get; }

	public Protocol ConnectionProtocol { get; }

	public string ConnectionString => $"{Address}:{ConnectionPort}";

	public ServerInfo(uint appId, string name, IPAddress address, int connectionPort, int queryPort, string map, string tagString, bool isSecure, int players, int maxPlayers, uint lastTimePlayed, int ping, ulong steamId, int authedPlayers = int.MaxValue)
	{
		AppId = appId;
		Name = name;
		Address = address ?? throw new ArgumentNullException("address");
		AddressRaw = AddressToUInt32(address);
		ConnectionPort = connectionPort;
		QueryPort = queryPort;
		Map = map;
		TagString = tagString;
		IsSecure = isSecure;
		RegionCode = default(StringView);
		ConnectionProtocol = Protocol.Default;
		uint result = 0u;
		int result2 = 0;
		Mode = "vanilla";
		ModeSortIndex = ModePriority.Count - 1;
		if (!string.IsNullOrEmpty(TagString))
		{
			List<StringView> obj = Pool.Get<List<StringView>>();
			((StringView)TagString).Split(',', obj);
			Tags = new HashSet<StringView>(obj, StringView.ComparerIgnoreCase.Instance);
			Pool.FreeUnmanaged(ref obj);
		}
		else
		{
			Tags = EmptyTags;
		}
		foreach (StringView tag in Tags)
		{
			if (tag.StartsWith("cp"))
			{
				int.TryParse(tag.Substring(2), out players);
			}
			else if (tag.StartsWith("mp"))
			{
				int.TryParse(tag.Substring(2), out maxPlayers);
			}
			else if (tag.StartsWith("pt"))
			{
				StringView stringView = tag.Substring(2);
				if (stringView == "sw")
				{
					ConnectionProtocol = Protocol.Steamworks;
				}
				else if (stringView == "rak")
				{
					ConnectionProtocol = Protocol.Raknet;
				}
			}
			else if (tag.StartsWith("$r"))
			{
				RegionCode = tag.Substring(2);
			}
			else if (tag.StartsWith("born"))
			{
				uint.TryParse(tag.Substring(4), out result);
			}
			else if (tag.StartsWith("ts"))
			{
				int.TryParse(tag.Substring(2), out result2);
			}
			for (int i = 0; i < ModePriority.Count; i++)
			{
				if (ServerTagCompressor.HasDecompressedTag(tag, ModePriority[i]))
				{
					Mode = ModePriority[i];
					ModeSortIndex = i;
					break;
				}
			}
		}
		QueuedPlayers = GetQueuedPlayers(Tags);
		Players = Math.Min(players, authedPlayers);
		MaxPlayers = maxPlayers;
		LastTimePlayed = lastTimePlayed;
		Ping = ping;
		SteamId = steamId;
		Born = result;
		MaxTeamSize = result2;
	}

	private static int GetQueuedPlayers(HashSet<StringView> Tags)
	{
		foreach (StringView Tag in Tags)
		{
			int num = Tag.IndexOf("qp");
			if (num >= 0 && int.TryParse(Tag.Substring(num + 2), out var result))
			{
				return result;
			}
		}
		return 0;
	}

	private static uint AddressToUInt32(IPAddress address)
	{
		if (address.AddressFamily != AddressFamily.InterNetwork)
		{
			return 0u;
		}
		return Swap((uint)address.Address);
	}

	private static uint Swap(uint x)
	{
		return ((x & 0xFF) << 24) + ((x & 0xFF00) << 8) + ((x & 0xFF0000) >> 8) + ((x & 0xFF000000u) >> 24);
	}

	public bool Equals(ServerInfo other)
	{
		if (Name == other.Name && object.Equals(Address, other.Address) && AddressRaw == other.AddressRaw && ConnectionPort == other.ConnectionPort && QueryPort == other.QueryPort && Map == other.Map && MaxPlayers == other.MaxPlayers)
		{
			return Mode == other.Mode;
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		if (obj is ServerInfo other)
		{
			return Equals(other);
		}
		return false;
	}

	public override int GetHashCode()
	{
		HashCode hashCode = default(HashCode);
		hashCode.Add(Name);
		hashCode.Add(Address);
		hashCode.Add(AddressRaw);
		hashCode.Add(ConnectionPort);
		hashCode.Add(QueryPort);
		hashCode.Add(Map);
		hashCode.Add(MaxPlayers);
		hashCode.Add(Mode);
		return hashCode.ToHashCode();
	}
}
