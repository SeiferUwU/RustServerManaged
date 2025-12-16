using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Facepunch.Models;

[JsonModel]
public class Manifest
{
	[JsonModel]
	public class Changeset
	{
		public int Main;

		public int Staging;
	}

	[JsonModel]
	public class Drop
	{
		public string Title { get; set; }

		public string Subtitle { get; set; }

		public string RoundNumber { get; set; }

		public DateTime StartTimeUtc { get; set; }

		public DateTime EndTimeUtc { get; set; }

		public string HeroBackground { get; set; }

		public string Type { get; set; }

		public string Url { get; set; }
	}

	[JsonModel]
	public class TwitchDrop
	{
		public string Title;

		public string Subtitle;

		public string RoundNumber;

		public string StartTimeUtc;

		public string EndTimeUtc;

		public string HeroBackground;
	}

	[JsonModel]
	public class Administrator
	{
		public string UserId;

		public string Level;
	}

	[JsonModel]
	public class NewsInfo
	{
		[JsonModel]
		public class BlogInfo
		{
			public DateTime Date;

			public string ShortName;

			public string Title;

			public string HeaderImage;

			public string SummaryHtml;

			public string Url;

			public string Tags;
		}

		public BlogInfo[] Blogs;
	}

	[JsonModel]
	public class ServersInfo
	{
		public ServerDesc[] Official;

		private BanChecker _banChecker = new BanChecker(Array.Empty<string>());

		private string[] _banned = Array.Empty<string>();

		public string[] Banned
		{
			get
			{
				return _banned;
			}
			set
			{
				_banned = value ?? Array.Empty<string>();
				_banChecker = new BanChecker(_banned);
			}
		}

		public bool IsBannedServer(string ip)
		{
			using (TimeWarning.New("Manifest.IsBannedServer"))
			{
				return _banChecker.IsBanned(ip);
			}
		}
	}

	[JsonModel]
	public class ServerDesc
	{
		public string Address;

		public int Port;
	}

	public NewsInfo News;

	public ServersInfo Servers;

	public FeaturesInfo Features = new FeaturesInfo();

	public string ExceptionReportingUrl;

	public string BenchmarkUrl;

	public string AnalyticUrl;

	public string DatabaseUrl;

	public string LeaderboardUrl;

	public string ReportUrl;

	public string AccountUrl;

	public Hero[] Heroes;

	public StoreFeaturing[] FeaturedItems;

	public DateTime SkinsLastUpdated;

	public Administrator[] Administrators;

	public Changeset Changesets;

	public Drop[] Drops;

	public JObject Metadata;

	internal static Manifest FromJson(string text)
	{
		if (Application.Integration.DebugOutput)
		{
			Debug.Log("[Manifest] " + text);
		}
		Manifest manifest = JsonConvert.DeserializeObject<Manifest>(text);
		if (manifest == null)
		{
			return null;
		}
		if (manifest.Servers == null)
		{
			return null;
		}
		if (manifest.Features == null)
		{
			manifest.Features = new FeaturesInfo();
		}
		return manifest;
	}
}
