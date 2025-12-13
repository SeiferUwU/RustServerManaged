using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Facepunch;
using Rust.Platform.Common;
using UnityEngine;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyVersion("0.0.0.0")]
public enum AuthResponse
{
	Invalid,
	TimedOut,
	OK,
	VACBanned,
	PublisherBanned,
	InvalidAuthSession,
	NetworkIdentityFailure,
	AuthTicketAlreadyUsed,
	AuthTicketCanceled
}
public class AvatarCache
{
	private readonly struct Entry
	{
		public readonly ulong UserId;

		public readonly Texture2D Texture;

		public Entry(ulong userId, Texture2D texture)
		{
			UserId = userId;
			Texture = texture;
		}
	}

	private readonly Dictionary<ulong, Entry> _entries;

	private readonly Action<ulong, Texture2D> _loadAvatar;

	public AvatarCache(Action<ulong, Texture2D> loadAvatar)
	{
		_entries = new Dictionary<ulong, Entry>();
		_loadAvatar = loadAvatar ?? throw new ArgumentNullException("loadAvatar");
	}

	public Texture2D Get(ulong userId)
	{
		if (_entries.TryGetValue(userId, out var value))
		{
			return value.Texture;
		}
		Texture2D texture2D = new Texture2D(64, 64, TextureFormat.ARGB32, mipChain: false)
		{
			name = $"Avatar_{userId}",
			filterMode = FilterMode.Trilinear,
			wrapMode = TextureWrapMode.Clamp,
			anisoLevel = 8
		};
		for (int i = 0; i < texture2D.width; i++)
		{
			for (int j = 0; j < texture2D.height; j++)
			{
				texture2D.SetPixel(i, j, new Color32(0, 0, 0, 20));
			}
		}
		texture2D.Apply(updateMipmaps: true);
		Entry value2 = new Entry(userId, texture2D);
		_entries.Add(userId, value2);
		_loadAvatar(userId, texture2D);
		return texture2D;
	}
}
public interface IAchievement
{
	string Key { get; }

	bool IsUnlocked { get; }

	void Unlock();
}
public interface IAuthTicket : IDisposable
{
	string Token { get; }

	byte[] Data { get; }
}
public interface IDownloadableContent
{
	int AppId { get; }

	bool IsInstalled { get; }
}
public interface IPlatformHooks
{
	uint SteamAppId { get; }

	ServerParameters? ServerParameters { get; }

	void Abort();

	void OnItemDefinitionsChanged();

	void AuthSessionValidated(ulong userId, ulong ownerUserId, AuthResponse response, string rawResponse);
}
public interface IPlatformService
{
	bool IsValid { get; }

	IReadOnlyList<IPlayerItemDefinition> ItemDefinitions { get; }

	bool Initialize(IPlatformHooks hooks);

	void Shutdown();

	void Update();

	void RefreshItemDefinitions();

	IPlayerItemDefinition GetItemDefinition(int definitionId);

	Task<IPlayerInventory> DeserializeInventory(byte[] data);

	bool PlayerOwnsDownloadableContent(ulong userId, int appId);

	Task<bool> LoadPlayerStats(ulong userId);

	Task<bool> SavePlayerStats(ulong userId);

	long GetPlayerStatInt(ulong userId, string key, long defaultValue = 0L);

	bool SetPlayerStatInt(ulong userId, string key, long value);

	bool BeginPlayerSession(ulong userId, byte[] authToken);

	void UpdatePlayerSession(ulong userId, string userName);

	void EndPlayerSession(ulong userId);
}
public interface IPlayerInfo
{
	ulong UserId { get; }

	string UserName { get; }

	bool IsOnline { get; }

	bool IsMe { get; }

	bool IsFriend { get; }

	bool IsPlayingThisGame { get; }

	string ServerEndpoint { get; }
}
public interface IPlayerInventory : IDisposable
{
	IReadOnlyList<IPlayerItem> Items { get; }

	bool BelongsTo(ulong userId);

	byte[] Serialize();
}
public interface IPlayerItem
{
	ulong Id { get; }

	int DefinitionId { get; }

	int Quantity { get; }

	DateTimeOffset Acquired { get; }

	ulong WorkshopId { get; }

	string ItemShortName { get; }

	Task Consume();
}
public interface IPlayerItemDefinition : IEquatable<IPlayerItemDefinition>
{
	int DefinitionId { get; }

	string Name { get; }

	string Description { get; }

	string Type { get; }

	string IconUrl { get; }

	int LocalPrice { get; }

	string LocalPriceFormatted { get; }

	string PriceCategory { get; }

	bool IsGenerator { get; }

	bool IsTradable { get; }

	bool IsMarketable { get; }

	string StoreTags { get; }

	DateTime Created { get; }

	DateTime Modified { get; }

	string ItemShortName { get; }

	ulong WorkshopId { get; }

	ulong WorkshopDownload { get; }

	IEnumerable<PlayerItemRecipe> GetRecipesContainingThis();
}
public enum ServerQuerySet
{
	Whitelist,
	Internet,
	LocalNetwork,
	Friends,
	Favorites,
	History
}
public interface IServerQuery : IDisposable
{
	IReadOnlyList<ServerInfo> Servers { get; }

	event Action<ServerInfo> OnServerFound;

	void AddFilter(string key, string value);

	Task RunQueryAsync(double timeoutInSeconds);
}
public sealed class CompositeServerQuery : IServerQuery, IDisposable
{
	private readonly IServerQuery _queryA;

	private readonly IServerQuery _queryB;

	public IReadOnlyList<ServerInfo> Servers { get; }

	public event Action<ServerInfo> OnServerFound;

	public CompositeServerQuery(IServerQuery queryA, IServerQuery queryB)
	{
		CompositeServerQuery compositeServerQuery = this;
		_queryA = queryA ?? throw new ArgumentNullException("queryA");
		_queryB = queryB ?? throw new ArgumentNullException("queryB");
		List<ServerInfo> serverList = new List<ServerInfo>();
		Servers = serverList;
		HashSet<(uint, int)> foundServers = new HashSet<(uint, int)>();
		Action<ServerInfo> value = delegate(ServerInfo info)
		{
			if (foundServers.Add((info.AddressRaw, info.QueryPort)))
			{
				serverList.Add(info);
				compositeServerQuery.OnServerFound?.Invoke(info);
			}
		};
		_queryA.OnServerFound += value;
		_queryB.OnServerFound += value;
	}

	public void Dispose()
	{
		_queryA.Dispose();
		_queryB.Dispose();
	}

	public void AddFilter(string key, string value)
	{
		_queryA.AddFilter(key, value);
		_queryB.AddFilter(key, value);
	}

	public async Task RunQueryAsync(double timeoutInSeconds)
	{
		await Task.WhenAll(_queryA.RunQueryAsync(timeoutInSeconds), _queryB.RunQueryAsync(timeoutInSeconds));
	}
}
public interface IWorkshopContent
{
	ulong WorkshopId { get; }

	string Title { get; }

	string Description { get; }

	IEnumerable<string> Tags { get; }

	string Url { get; }

	string PreviewImageUrl { get; }

	ulong OwnerId { get; }

	IPlayerInfo Owner { get; }

	bool IsInstalled { get; }

	bool IsDownloadPending { get; }

	bool IsDownloading { get; }

	string Directory { get; }

	bool Download();
}
public readonly struct PlayerItemRecipe : IEquatable<PlayerItemRecipe>
{
	public readonly struct Ingredient : IEquatable<Ingredient>
	{
		public int DefinitionId { get; }

		public int Amount { get; }

		public Ingredient(int definitionId, int amount)
		{
			DefinitionId = definitionId;
			Amount = amount;
		}

		public bool Equals(Ingredient other)
		{
			if (DefinitionId == other.DefinitionId)
			{
				return Amount == other.Amount;
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj is Ingredient other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (DefinitionId * 397) ^ Amount;
		}

		public static bool operator ==(Ingredient left, Ingredient right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Ingredient left, Ingredient right)
		{
			return !left.Equals(right);
		}
	}

	public IReadOnlyList<Ingredient> Ingredients { get; }

	public IPlayerItemDefinition Result { get; }

	public PlayerItemRecipe(IReadOnlyList<Ingredient> ingredients, IPlayerItemDefinition result)
	{
		Ingredients = ingredients ?? throw new ArgumentNullException("ingredients");
		Result = result ?? throw new ArgumentNullException("result");
		if (Ingredients.Count == 0)
		{
			throw new ArgumentException("Recipes must have at least one ingredient.", "ingredients");
		}
	}

	public bool Equals(PlayerItemRecipe other)
	{
		if (Result.Equals(other.Result))
		{
			if (!Ingredients.Equals(other.Ingredients))
			{
				return Ingredients.SequenceEqual(other.Ingredients);
			}
			return true;
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		if (obj is PlayerItemRecipe other)
		{
			return Equals(other);
		}
		return false;
	}

	public override int GetHashCode()
	{
		return (Ingredients.Sum((Ingredient i) => i.GetHashCode()) * 397) ^ Result.GetHashCode();
	}

	public static bool operator ==(PlayerItemRecipe left, PlayerItemRecipe right)
	{
		return left.Equals(right);
	}

	public static bool operator !=(PlayerItemRecipe left, PlayerItemRecipe right)
	{
		return !left.Equals(right);
	}
}
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
public static class ServerTagCompressor
{
	private static readonly IReadOnlyDictionary<char, string> charToTag;

	private static readonly IReadOnlyDictionary<string, char> tagToChar;

	public static readonly char TagPrefixCharacter;

	private static readonly StringBuilder normalTags;

	static ServerTagCompressor()
	{
		charToTag = new Dictionary<char, string>
		{
			{ 'b', "biweekly" },
			{ 'c', "creative" },
			{ 'd', "training" },
			{ 'e', "minigame" },
			{ 'g', "gamemode" },
			{ 'h', "hardcore" },
			{ 'i', "battlefield" },
			{ 'j', "broyale" },
			{ 'k', "builds" },
			{ 'm', "monthly" },
			{ 'o', "oxide" },
			{ 'p', "pve" },
			{ 'q', "premium" },
			{ 'r', "roleplay" },
			{ 's', "softcore" },
			{ 't', "tut" },
			{ 'u', "primitive" },
			{ 'v', "vanilla" },
			{ 'w', "weekly" },
			{ 'y', "carbon" },
			{ 'z', "modded" }
		};
		TagPrefixCharacter = '^';
		normalTags = new StringBuilder();
		tagToChar = charToTag.ToDictionary((KeyValuePair<char, string> x) => x.Value, (KeyValuePair<char, string> x) => x.Key);
	}

	public static string ShortenTag(string tag)
	{
		if (tagToChar.TryGetValue(tag, out var value))
		{
			return $"{TagPrefixCharacter}{value}";
		}
		return tag;
	}

	public static bool HasDecompressedTag(StringView tagView, string tag)
	{
		if (tagView.Length == 2 && tagView[0] == TagPrefixCharacter)
		{
			char key = tagView[1];
			if (charToTag.TryGetValue(key, out var value))
			{
				return value == tag;
			}
		}
		return tagView.Equals(tag);
	}

	public static string DecompressSingleTag(StringView tagView)
	{
		if (tagView.Length == 2 && tagView[0] == TagPrefixCharacter)
		{
			char key = tagView[1];
			if (charToTag.TryGetValue(key, out var value))
			{
				return value;
			}
		}
		return tagView.ToString();
	}

	public static string DecompressSingleTag(string tag)
	{
		if (tag.Length == 2 && tag[0] == TagPrefixCharacter)
		{
			char key = tag[1];
			if (charToTag.TryGetValue(key, out var value))
			{
				return value;
			}
		}
		return tag;
	}

	public static string CompressTags(string input)
	{
		if (input.Contains(TagPrefixCharacter))
		{
			UnityEngine.Debug.LogError($"Server tags '{input}' already contain unique compressed character '{TagPrefixCharacter}'");
		}
		string[] array = input.Split(',');
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = ShortenTag(array[i]);
		}
		return string.Join(',', array);
	}

	public static void DecompressTags(HashSet<string> tags, string compactTag)
	{
		for (int i = 1; i < compactTag.Length; i += 2)
		{
			char key = compactTag[i];
			if (charToTag.TryGetValue(key, out var value))
			{
				tags.Add(value);
			}
		}
	}
}
[CompilerGenerated]
[EditorBrowsable(EditorBrowsableState.Never)]
[GeneratedCode("Unity.MonoScriptGenerator.MonoScriptInfoGenerator", null)]
internal class UnitySourceGeneratedAssemblyMonoScriptTypes_v1
{
	private struct MonoScriptData
	{
		public byte[] FilePathsData;

		public byte[] TypesData;

		public int TotalTypes;

		public int TotalFiles;

		public bool IsEditorOnly;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static MonoScriptData Get()
	{
		return new MonoScriptData
		{
			FilePathsData = new byte[1000]
			{
				0, 0, 0, 2, 0, 0, 0, 51, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 82, 117, 115, 116, 46, 80,
				108, 97, 116, 102, 111, 114, 109, 46, 67, 111,
				109, 109, 111, 110, 92, 65, 118, 97, 116, 97,
				114, 67, 97, 99, 104, 101, 46, 99, 115, 0,
				0, 0, 1, 0, 0, 0, 52, 92, 65, 115,
				115, 101, 116, 115, 92, 80, 108, 117, 103, 105,
				110, 115, 92, 82, 117, 115, 116, 46, 80, 108,
				97, 116, 102, 111, 114, 109, 46, 67, 111, 109,
				109, 111, 110, 92, 73, 65, 99, 104, 105, 101,
				118, 101, 109, 101, 110, 116, 46, 99, 115, 0,
				0, 0, 1, 0, 0, 0, 51, 92, 65, 115,
				115, 101, 116, 115, 92, 80, 108, 117, 103, 105,
				110, 115, 92, 82, 117, 115, 116, 46, 80, 108,
				97, 116, 102, 111, 114, 109, 46, 67, 111, 109,
				109, 111, 110, 92, 73, 65, 117, 116, 104, 84,
				105, 99, 107, 101, 116, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 60, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 82, 117, 115, 116, 46, 80, 108, 97,
				116, 102, 111, 114, 109, 46, 67, 111, 109, 109,
				111, 110, 92, 73, 68, 111, 119, 110, 108, 111,
				97, 100, 97, 98, 108, 101, 67, 111, 110, 116,
				101, 110, 116, 46, 99, 115, 0, 0, 0, 1,
				0, 0, 0, 54, 92, 65, 115, 115, 101, 116,
				115, 92, 80, 108, 117, 103, 105, 110, 115, 92,
				82, 117, 115, 116, 46, 80, 108, 97, 116, 102,
				111, 114, 109, 46, 67, 111, 109, 109, 111, 110,
				92, 73, 80, 108, 97, 116, 102, 111, 114, 109,
				72, 111, 111, 107, 115, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 56, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 82, 117, 115, 116, 46, 80, 108, 97,
				116, 102, 111, 114, 109, 46, 67, 111, 109, 109,
				111, 110, 92, 73, 80, 108, 97, 116, 102, 111,
				114, 109, 83, 101, 114, 118, 105, 99, 101, 46,
				99, 115, 0, 0, 0, 1, 0, 0, 0, 51,
				92, 65, 115, 115, 101, 116, 115, 92, 80, 108,
				117, 103, 105, 110, 115, 92, 82, 117, 115, 116,
				46, 80, 108, 97, 116, 102, 111, 114, 109, 46,
				67, 111, 109, 109, 111, 110, 92, 73, 80, 108,
				97, 121, 101, 114, 73, 110, 102, 111, 46, 99,
				115, 0, 0, 0, 1, 0, 0, 0, 56, 92,
				65, 115, 115, 101, 116, 115, 92, 80, 108, 117,
				103, 105, 110, 115, 92, 82, 117, 115, 116, 46,
				80, 108, 97, 116, 102, 111, 114, 109, 46, 67,
				111, 109, 109, 111, 110, 92, 73, 80, 108, 97,
				121, 101, 114, 73, 110, 118, 101, 110, 116, 111,
				114, 121, 46, 99, 115, 0, 0, 0, 1, 0,
				0, 0, 51, 92, 65, 115, 115, 101, 116, 115,
				92, 80, 108, 117, 103, 105, 110, 115, 92, 82,
				117, 115, 116, 46, 80, 108, 97, 116, 102, 111,
				114, 109, 46, 67, 111, 109, 109, 111, 110, 92,
				73, 80, 108, 97, 121, 101, 114, 73, 116, 101,
				109, 46, 99, 115, 0, 0, 0, 1, 0, 0,
				0, 61, 92, 65, 115, 115, 101, 116, 115, 92,
				80, 108, 117, 103, 105, 110, 115, 92, 82, 117,
				115, 116, 46, 80, 108, 97, 116, 102, 111, 114,
				109, 46, 67, 111, 109, 109, 111, 110, 92, 73,
				80, 108, 97, 121, 101, 114, 73, 116, 101, 109,
				68, 101, 102, 105, 110, 105, 116, 105, 111, 110,
				46, 99, 115, 0, 0, 0, 2, 0, 0, 0,
				52, 92, 65, 115, 115, 101, 116, 115, 92, 80,
				108, 117, 103, 105, 110, 115, 92, 82, 117, 115,
				116, 46, 80, 108, 97, 116, 102, 111, 114, 109,
				46, 67, 111, 109, 109, 111, 110, 92, 73, 83,
				101, 114, 118, 101, 114, 81, 117, 101, 114, 121,
				46, 99, 115, 0, 0, 0, 1, 0, 0, 0,
				56, 92, 65, 115, 115, 101, 116, 115, 92, 80,
				108, 117, 103, 105, 110, 115, 92, 82, 117, 115,
				116, 46, 80, 108, 97, 116, 102, 111, 114, 109,
				46, 67, 111, 109, 109, 111, 110, 92, 73, 87,
				111, 114, 107, 115, 104, 111, 112, 67, 111, 110,
				116, 101, 110, 116, 46, 99, 115, 0, 0, 0,
				2, 0, 0, 0, 56, 92, 65, 115, 115, 101,
				116, 115, 92, 80, 108, 117, 103, 105, 110, 115,
				92, 82, 117, 115, 116, 46, 80, 108, 97, 116,
				102, 111, 114, 109, 46, 67, 111, 109, 109, 111,
				110, 92, 80, 108, 97, 121, 101, 114, 73, 116,
				101, 109, 82, 101, 99, 105, 112, 101, 46, 99,
				115, 0, 0, 0, 1, 0, 0, 0, 50, 92,
				65, 115, 115, 101, 116, 115, 92, 80, 108, 117,
				103, 105, 110, 115, 92, 82, 117, 115, 116, 46,
				80, 108, 97, 116, 102, 111, 114, 109, 46, 67,
				111, 109, 109, 111, 110, 92, 83, 101, 114, 118,
				101, 114, 73, 110, 102, 111, 46, 99, 115, 0,
				0, 0, 1, 0, 0, 0, 56, 92, 65, 115,
				115, 101, 116, 115, 92, 80, 108, 117, 103, 105,
				110, 115, 92, 82, 117, 115, 116, 46, 80, 108,
				97, 116, 102, 111, 114, 109, 46, 67, 111, 109,
				109, 111, 110, 92, 83, 101, 114, 118, 101, 114,
				80, 97, 114, 97, 109, 101, 116, 101, 114, 115,
				46, 99, 115, 0, 0, 0, 1, 0, 0, 0,
				59, 92, 65, 115, 115, 101, 116, 115, 92, 80,
				108, 117, 103, 105, 110, 115, 92, 82, 117, 115,
				116, 46, 80, 108, 97, 116, 102, 111, 114, 109,
				46, 67, 111, 109, 109, 111, 110, 92, 83, 101,
				114, 118, 101, 114, 84, 97, 103, 67, 111, 109,
				112, 114, 101, 115, 115, 111, 114, 46, 99, 115
			},
			TypesData = new byte[412]
			{
				0, 0, 0, 0, 12, 124, 65, 118, 97, 116,
				97, 114, 67, 97, 99, 104, 101, 0, 0, 0,
				0, 17, 65, 118, 97, 116, 97, 114, 67, 97,
				99, 104, 101, 124, 69, 110, 116, 114, 121, 0,
				0, 0, 0, 13, 124, 73, 65, 99, 104, 105,
				101, 118, 101, 109, 101, 110, 116, 0, 0, 0,
				0, 12, 124, 73, 65, 117, 116, 104, 84, 105,
				99, 107, 101, 116, 0, 0, 0, 0, 21, 124,
				73, 68, 111, 119, 110, 108, 111, 97, 100, 97,
				98, 108, 101, 67, 111, 110, 116, 101, 110, 116,
				0, 0, 0, 0, 15, 124, 73, 80, 108, 97,
				116, 102, 111, 114, 109, 72, 111, 111, 107, 115,
				0, 0, 0, 0, 17, 124, 73, 80, 108, 97,
				116, 102, 111, 114, 109, 83, 101, 114, 118, 105,
				99, 101, 0, 0, 0, 0, 12, 124, 73, 80,
				108, 97, 121, 101, 114, 73, 110, 102, 111, 0,
				0, 0, 0, 17, 124, 73, 80, 108, 97, 121,
				101, 114, 73, 110, 118, 101, 110, 116, 111, 114,
				121, 0, 0, 0, 0, 12, 124, 73, 80, 108,
				97, 121, 101, 114, 73, 116, 101, 109, 0, 0,
				0, 0, 22, 124, 73, 80, 108, 97, 121, 101,
				114, 73, 116, 101, 109, 68, 101, 102, 105, 110,
				105, 116, 105, 111, 110, 0, 0, 0, 0, 13,
				124, 73, 83, 101, 114, 118, 101, 114, 81, 117,
				101, 114, 121, 0, 0, 0, 0, 21, 124, 67,
				111, 109, 112, 111, 115, 105, 116, 101, 83, 101,
				114, 118, 101, 114, 81, 117, 101, 114, 121, 0,
				0, 0, 0, 17, 124, 73, 87, 111, 114, 107,
				115, 104, 111, 112, 67, 111, 110, 116, 101, 110,
				116, 0, 0, 0, 0, 17, 124, 80, 108, 97,
				121, 101, 114, 73, 116, 101, 109, 82, 101, 99,
				105, 112, 101, 0, 0, 0, 0, 11, 124, 73,
				110, 103, 114, 101, 100, 105, 101, 110, 116, 0,
				0, 0, 0, 11, 124, 83, 101, 114, 118, 101,
				114, 73, 110, 102, 111, 0, 0, 0, 0, 37,
				82, 117, 115, 116, 46, 80, 108, 97, 116, 102,
				111, 114, 109, 46, 67, 111, 109, 109, 111, 110,
				124, 83, 101, 114, 118, 101, 114, 80, 97, 114,
				97, 109, 101, 116, 101, 114, 115, 0, 0, 0,
				0, 20, 124, 83, 101, 114, 118, 101, 114, 84,
				97, 103, 67, 111, 109, 112, 114, 101, 115, 115,
				111, 114
			},
			TotalFiles = 16,
			TotalTypes = 19,
			IsEditorOnly = false
		};
	}
}
namespace Rust.Platform.Common;

public readonly struct ServerParameters
{
	public string ShortName { get; }

	public string FullName { get; }

	public string Version { get; }

	public bool IsSecure { get; }

	public bool HideIP { get; }

	public IPAddress Address { get; }

	public ushort GamePort { get; }

	public ushort QueryPort { get; }

	public ServerParameters(string shortName, string fullName, string version, bool isSecure, bool hideIP, IPAddress address, ushort gamePort, ushort queryPort = 0)
	{
		ShortName = shortName ?? throw new ArgumentNullException("shortName");
		FullName = fullName ?? throw new ArgumentNullException("fullName");
		Version = version ?? throw new ArgumentNullException("version");
		IsSecure = isSecure;
		HideIP = hideIP;
		Address = address;
		GamePort = gamePort;
		QueryPort = queryPort;
	}
}
