using System;
using System.Collections.Generic;
using Facepunch;
using Network;

public struct RpcTarget
{
	public string Function;

	public SendInfo Connections;

	public bool ToNetworkGroup;

	public bool UsingPooledConnections;

	public static RpcTarget NetworkGroup(string funcName)
	{
		return new RpcTarget
		{
			Function = funcName,
			ToNetworkGroup = true
		};
	}

	public static RpcTarget NetworkGroup(string funcName, BaseNetworkable entity)
	{
		return new RpcTarget
		{
			Function = funcName,
			Connections = new SendInfo(entity.net.group.subscribers)
		};
	}

	public static RpcTarget NetworkGroup(string funcName, BaseNetworkable entity, SendMethod method, Priority priority)
	{
		return new RpcTarget
		{
			Function = funcName,
			Connections = new SendInfo(entity.net.group.subscribers)
			{
				method = method,
				priority = priority
			}
		};
	}

	public static RpcTarget Player(string funcName, BasePlayer target)
	{
		return Player(funcName, target.IsValid() ? target.net.connection : null);
	}

	public static RpcTarget Player(string funcName, Connection connection)
	{
		return new RpcTarget
		{
			Function = funcName,
			Connections = new SendInfo(connection)
		};
	}

	public static RpcTarget Players(string funcName, List<Connection> connections)
	{
		return new RpcTarget
		{
			Function = funcName,
			Connections = new SendInfo(connections)
		};
	}

	public static RpcTarget Players(string funcName, List<Connection> connections, SendMethod method, Priority priority)
	{
		return new RpcTarget
		{
			Function = funcName,
			Connections = new SendInfo(connections)
			{
				method = method,
				priority = priority
			}
		};
	}

	public static RpcTarget SendInfo(string funcName, SendInfo sendInfo)
	{
		return new RpcTarget
		{
			Function = funcName,
			Connections = sendInfo
		};
	}

	public static RpcTarget PlayerAndSpectators(string funcName, BasePlayer player)
	{
		List<Connection> list = Pool.Get<List<Connection>>();
		if (player.IsValid())
		{
			if (player.net.connection != null)
			{
				list.Add(player.net.connection);
			}
			if (player.IsBeingSpectated)
			{
				ReadOnlySpan<BasePlayer> spectators = player.GetSpectators();
				for (int i = 0; i < spectators.Length; i++)
				{
					BasePlayer basePlayer = spectators[i];
					list.Add(basePlayer.net.connection);
				}
			}
		}
		return new RpcTarget
		{
			Function = funcName,
			Connections = new SendInfo(list),
			UsingPooledConnections = true
		};
	}
}
