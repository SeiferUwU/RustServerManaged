using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Steamworks;
using Steamworks.Data;
using Steamworks.ServerList;

namespace Rust.Platform.Steam;

public class SteamServerQuery : IServerQuery, IDisposable
{
	public ServerQuerySet QuerySet { get; }

	public Base Query { get; private set; }

	public IReadOnlyList<ServerInfo> Servers { get; }

	public event Action<ServerInfo> OnServerFound;

	public SteamServerQuery(ServerQuerySet set, Base query)
	{
		SteamServerQuery steamServerQuery = this;
		QuerySet = set;
		Query = query ?? throw new ArgumentNullException("query");
		List<ServerInfo> serverList = new List<ServerInfo>();
		Servers = serverList;
		Query.OnChanges += delegate
		{
			if (steamServerQuery.Query != null)
			{
				foreach (Steamworks.Data.ServerInfo item in steamServerQuery.Query.Responsive)
				{
					using (TimeWarning.New("SteamServerQuery.OnChanges.Responsive"))
					{
						ServerInfo serverInfo = SteamPlatform.ToPlatformServer(item);
						serverList.Add(serverInfo);
						steamServerQuery.OnServerFound?.Invoke(serverInfo);
					}
				}
				foreach (Steamworks.Data.ServerInfo item2 in steamServerQuery.Query.Unqueried)
				{
					using (TimeWarning.New("SteamServerQuery.OnChanges.Unqueried"))
					{
						ServerInfo serverInfo2 = SteamPlatform.ToPlatformServer(item2);
						serverList.Add(serverInfo2);
						steamServerQuery.OnServerFound?.Invoke(serverInfo2);
					}
				}
				steamServerQuery.Query.Responsive.Clear();
			}
		};
	}

	public void Dispose()
	{
		if (SteamClient.IsValid)
		{
			Query?.Dispose();
		}
		Query = null;
	}

	public void AddFilter(string key, string value)
	{
		Query.AddFilter(key, value);
	}

	public async Task RunQueryAsync(double timeoutInSeconds)
	{
		await Query.RunQueryAsync((float)timeoutInSeconds);
	}
}
