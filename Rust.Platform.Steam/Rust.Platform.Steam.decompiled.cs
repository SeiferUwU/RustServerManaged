using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Rust.Platform.Common;
using Steamworks;
using Steamworks.Data;
using Steamworks.ServerList;
using Steamworks.Ugc;
using UnityEngine;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyVersion("0.0.0.0")]
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
			FilePathsData = new byte[1424]
			{
				0, 0, 0, 1, 0, 0, 0, 50, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 82, 117, 115, 116, 46, 80,
				108, 97, 116, 102, 111, 114, 109, 46, 83, 116,
				101, 97, 109, 92, 73, 112, 76, 105, 115, 116,
				81, 117, 101, 114, 121, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 55, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 82, 117, 115, 116, 46, 80, 108, 97,
				116, 102, 111, 114, 109, 46, 83, 116, 101, 97,
				109, 92, 83, 116, 101, 97, 109, 65, 99, 104,
				105, 101, 118, 101, 109, 101, 110, 116, 46, 99,
				115, 0, 0, 0, 1, 0, 0, 0, 54, 92,
				65, 115, 115, 101, 116, 115, 92, 80, 108, 117,
				103, 105, 110, 115, 92, 82, 117, 115, 116, 46,
				80, 108, 97, 116, 102, 111, 114, 109, 46, 83,
				116, 101, 97, 109, 92, 83, 116, 101, 97, 109,
				65, 117, 116, 104, 84, 105, 99, 107, 101, 116,
				46, 99, 115, 0, 0, 0, 1, 0, 0, 0,
				63, 92, 65, 115, 115, 101, 116, 115, 92, 80,
				108, 117, 103, 105, 110, 115, 92, 82, 117, 115,
				116, 46, 80, 108, 97, 116, 102, 111, 114, 109,
				46, 83, 116, 101, 97, 109, 92, 83, 116, 101,
				97, 109, 68, 111, 119, 110, 108, 111, 97, 100,
				97, 98, 108, 101, 67, 111, 110, 116, 101, 110,
				116, 46, 99, 115, 0, 0, 0, 1, 0, 0,
				0, 53, 92, 65, 115, 115, 101, 116, 115, 92,
				80, 108, 117, 103, 105, 110, 115, 92, 82, 117,
				115, 116, 46, 80, 108, 97, 116, 102, 111, 114,
				109, 46, 83, 116, 101, 97, 109, 92, 83, 116,
				101, 97, 109, 73, 110, 118, 101, 110, 116, 111,
				114, 121, 46, 99, 115, 0, 0, 0, 1, 0,
				0, 0, 48, 92, 65, 115, 115, 101, 116, 115,
				92, 80, 108, 117, 103, 105, 110, 115, 92, 82,
				117, 115, 116, 46, 80, 108, 97, 116, 102, 111,
				114, 109, 46, 83, 116, 101, 97, 109, 92, 83,
				116, 101, 97, 109, 73, 116, 101, 109, 46, 99,
				115, 0, 0, 0, 1, 0, 0, 0, 58, 92,
				65, 115, 115, 101, 116, 115, 92, 80, 108, 117,
				103, 105, 110, 115, 92, 82, 117, 115, 116, 46,
				80, 108, 97, 116, 102, 111, 114, 109, 46, 83,
				116, 101, 97, 109, 92, 83, 116, 101, 97, 109,
				73, 116, 101, 109, 68, 101, 102, 105, 110, 105,
				116, 105, 111, 110, 46, 99, 115, 0, 0, 0,
				1, 0, 0, 0, 59, 92, 65, 115, 115, 101,
				116, 115, 92, 80, 108, 117, 103, 105, 110, 115,
				92, 82, 117, 115, 116, 46, 80, 108, 97, 116,
				102, 111, 114, 109, 46, 83, 116, 101, 97, 109,
				92, 83, 116, 101, 97, 109, 80, 108, 97, 116,
				102, 111, 114, 109, 46, 65, 118, 97, 116, 97,
				114, 46, 99, 115, 0, 0, 0, 1, 0, 0,
				0, 52, 92, 65, 115, 115, 101, 116, 115, 92,
				80, 108, 117, 103, 105, 110, 115, 92, 82, 117,
				115, 116, 46, 80, 108, 97, 116, 102, 111, 114,
				109, 46, 83, 116, 101, 97, 109, 92, 83, 116,
				101, 97, 109, 80, 108, 97, 116, 102, 111, 114,
				109, 46, 99, 115, 0, 0, 0, 1, 0, 0,
				0, 56, 92, 65, 115, 115, 101, 116, 115, 92,
				80, 108, 117, 103, 105, 110, 115, 92, 82, 117,
				115, 116, 46, 80, 108, 97, 116, 102, 111, 114,
				109, 46, 83, 116, 101, 97, 109, 92, 83, 116,
				101, 97, 109, 80, 108, 97, 116, 102, 111, 114,
				109, 46, 68, 76, 67, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 64, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 82, 117, 115, 116, 46, 80, 108, 97,
				116, 102, 111, 114, 109, 46, 83, 116, 101, 97,
				109, 92, 83, 116, 101, 97, 109, 80, 108, 97,
				116, 102, 111, 114, 109, 46, 70, 97, 107, 101,
				83, 116, 101, 97, 109, 73, 100, 46, 99, 115,
				0, 0, 0, 1, 0, 0, 0, 62, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 82, 117, 115, 116, 46, 80,
				108, 97, 116, 102, 111, 114, 109, 46, 83, 116,
				101, 97, 109, 92, 83, 116, 101, 97, 109, 80,
				108, 97, 116, 102, 111, 114, 109, 46, 73, 110,
				118, 101, 110, 116, 111, 114, 121, 46, 99, 115,
				0, 0, 0, 2, 0, 0, 0, 59, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 82, 117, 115, 116, 46, 80,
				108, 97, 116, 102, 111, 114, 109, 46, 83, 116,
				101, 97, 109, 92, 83, 116, 101, 97, 109, 80,
				108, 97, 116, 102, 111, 114, 109, 46, 80, 108,
				97, 121, 101, 114, 46, 99, 115, 0, 0, 0,
				1, 0, 0, 0, 65, 92, 65, 115, 115, 101,
				116, 115, 92, 80, 108, 117, 103, 105, 110, 115,
				92, 82, 117, 115, 116, 46, 80, 108, 97, 116,
				102, 111, 114, 109, 46, 83, 116, 101, 97, 109,
				92, 83, 116, 101, 97, 109, 80, 108, 97, 116,
				102, 111, 114, 109, 46, 82, 105, 99, 104, 80,
				114, 101, 115, 101, 110, 99, 101, 46, 99, 115,
				0, 0, 0, 1, 0, 0, 0, 59, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 82, 117, 115, 116, 46, 80,
				108, 97, 116, 102, 111, 114, 109, 46, 83, 116,
				101, 97, 109, 92, 83, 116, 101, 97, 109, 80,
				108, 97, 116, 102, 111, 114, 109, 46, 83, 101,
				114, 118, 101, 114, 46, 99, 115, 0, 0, 0,
				1, 0, 0, 0, 63, 92, 65, 115, 115, 101,
				116, 115, 92, 80, 108, 117, 103, 105, 110, 115,
				92, 82, 117, 115, 116, 46, 80, 108, 97, 116,
				102, 111, 114, 109, 46, 83, 116, 101, 97, 109,
				92, 83, 116, 101, 97, 109, 80, 108, 97, 116,
				102, 111, 114, 109, 46, 83, 101, 114, 118, 101,
				114, 76, 105, 115, 116, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 58, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 82, 117, 115, 116, 46, 80, 108, 97,
				116, 102, 111, 114, 109, 46, 83, 116, 101, 97,
				109, 92, 83, 116, 101, 97, 109, 80, 108, 97,
				116, 102, 111, 114, 109, 46, 83, 116, 97, 116,
				115, 46, 99, 115, 0, 0, 0, 1, 0, 0,
				0, 58, 92, 65, 115, 115, 101, 116, 115, 92,
				80, 108, 117, 103, 105, 110, 115, 92, 82, 117,
				115, 116, 46, 80, 108, 97, 116, 102, 111, 114,
				109, 46, 83, 116, 101, 97, 109, 92, 83, 116,
				101, 97, 109, 80, 108, 97, 116, 102, 111, 114,
				109, 46, 86, 111, 105, 99, 101, 46, 99, 115,
				0, 0, 0, 1, 0, 0, 0, 50, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 82, 117, 115, 116, 46, 80,
				108, 97, 116, 102, 111, 114, 109, 46, 83, 116,
				101, 97, 109, 92, 83, 116, 101, 97, 109, 80,
				108, 97, 121, 101, 114, 46, 99, 115, 0, 0,
				0, 1, 0, 0, 0, 55, 92, 65, 115, 115,
				101, 116, 115, 92, 80, 108, 117, 103, 105, 110,
				115, 92, 82, 117, 115, 116, 46, 80, 108, 97,
				116, 102, 111, 114, 109, 46, 83, 116, 101, 97,
				109, 92, 83, 116, 101, 97, 109, 83, 101, 114,
				118, 101, 114, 81, 117, 101, 114, 121, 46, 99,
				115, 0, 0, 0, 1, 0, 0, 0, 48, 92,
				65, 115, 115, 101, 116, 115, 92, 80, 108, 117,
				103, 105, 110, 115, 92, 82, 117, 115, 116, 46,
				80, 108, 97, 116, 102, 111, 114, 109, 46, 83,
				116, 101, 97, 109, 92, 83, 116, 101, 97, 109,
				85, 116, 105, 108, 46, 99, 115, 0, 0, 0,
				1, 0, 0, 0, 59, 92, 65, 115, 115, 101,
				116, 115, 92, 80, 108, 117, 103, 105, 110, 115,
				92, 82, 117, 115, 116, 46, 80, 108, 97, 116,
				102, 111, 114, 109, 46, 83, 116, 101, 97, 109,
				92, 83, 116, 101, 97, 109, 87, 111, 114, 107,
				115, 104, 111, 112, 67, 111, 110, 116, 101, 110,
				116, 46, 99, 115
			},
			TypesData = new byte[910]
			{
				0, 0, 0, 0, 31, 82, 117, 115, 116, 46,
				80, 108, 97, 116, 102, 111, 114, 109, 46, 83,
				116, 101, 97, 109, 124, 73, 112, 76, 105, 115,
				116, 81, 117, 101, 114, 121, 0, 0, 0, 0,
				36, 82, 117, 115, 116, 46, 80, 108, 97, 116,
				102, 111, 114, 109, 46, 83, 116, 101, 97, 109,
				124, 83, 116, 101, 97, 109, 65, 99, 104, 105,
				101, 118, 101, 109, 101, 110, 116, 0, 0, 0,
				0, 35, 82, 117, 115, 116, 46, 80, 108, 97,
				116, 102, 111, 114, 109, 46, 83, 116, 101, 97,
				109, 124, 83, 116, 101, 97, 109, 65, 117, 116,
				104, 84, 105, 99, 107, 101, 116, 0, 0, 0,
				0, 44, 82, 117, 115, 116, 46, 80, 108, 97,
				116, 102, 111, 114, 109, 46, 83, 116, 101, 97,
				109, 124, 83, 116, 101, 97, 109, 68, 111, 119,
				110, 108, 111, 97, 100, 97, 98, 108, 101, 67,
				111, 110, 116, 101, 110, 116, 0, 0, 0, 0,
				34, 82, 117, 115, 116, 46, 80, 108, 97, 116,
				102, 111, 114, 109, 46, 83, 116, 101, 97, 109,
				124, 83, 116, 101, 97, 109, 73, 110, 118, 101,
				110, 116, 111, 114, 121, 0, 0, 0, 0, 29,
				82, 117, 115, 116, 46, 80, 108, 97, 116, 102,
				111, 114, 109, 46, 83, 116, 101, 97, 109, 124,
				83, 116, 101, 97, 109, 73, 116, 101, 109, 0,
				0, 0, 0, 39, 82, 117, 115, 116, 46, 80,
				108, 97, 116, 102, 111, 114, 109, 46, 83, 116,
				101, 97, 109, 124, 83, 116, 101, 97, 109, 73,
				116, 101, 109, 68, 101, 102, 105, 110, 105, 116,
				105, 111, 110, 1, 0, 0, 0, 33, 82, 117,
				115, 116, 46, 80, 108, 97, 116, 102, 111, 114,
				109, 46, 83, 116, 101, 97, 109, 124, 83, 116,
				101, 97, 109, 80, 108, 97, 116, 102, 111, 114,
				109, 1, 0, 0, 0, 33, 82, 117, 115, 116,
				46, 80, 108, 97, 116, 102, 111, 114, 109, 46,
				83, 116, 101, 97, 109, 124, 83, 116, 101, 97,
				109, 80, 108, 97, 116, 102, 111, 114, 109, 1,
				0, 0, 0, 33, 82, 117, 115, 116, 46, 80,
				108, 97, 116, 102, 111, 114, 109, 46, 83, 116,
				101, 97, 109, 124, 83, 116, 101, 97, 109, 80,
				108, 97, 116, 102, 111, 114, 109, 1, 0, 0,
				0, 33, 82, 117, 115, 116, 46, 80, 108, 97,
				116, 102, 111, 114, 109, 46, 83, 116, 101, 97,
				109, 124, 83, 116, 101, 97, 109, 80, 108, 97,
				116, 102, 111, 114, 109, 1, 0, 0, 0, 33,
				82, 117, 115, 116, 46, 80, 108, 97, 116, 102,
				111, 114, 109, 46, 83, 116, 101, 97, 109, 124,
				83, 116, 101, 97, 109, 80, 108, 97, 116, 102,
				111, 114, 109, 0, 0, 0, 0, 48, 82, 117,
				115, 116, 46, 80, 108, 97, 116, 102, 111, 114,
				109, 46, 83, 116, 101, 97, 109, 124, 83, 116,
				101, 97, 109, 65, 117, 116, 104, 84, 105, 99,
				107, 101, 116, 78, 117, 108, 108, 69, 120, 99,
				101, 112, 116, 105, 111, 110, 1, 0, 0, 0,
				33, 82, 117, 115, 116, 46, 80, 108, 97, 116,
				102, 111, 114, 109, 46, 83, 116, 101, 97, 109,
				124, 83, 116, 101, 97, 109, 80, 108, 97, 116,
				102, 111, 114, 109, 1, 0, 0, 0, 33, 82,
				117, 115, 116, 46, 80, 108, 97, 116, 102, 111,
				114, 109, 46, 83, 116, 101, 97, 109, 124, 83,
				116, 101, 97, 109, 80, 108, 97, 116, 102, 111,
				114, 109, 1, 0, 0, 0, 33, 82, 117, 115,
				116, 46, 80, 108, 97, 116, 102, 111, 114, 109,
				46, 83, 116, 101, 97, 109, 124, 83, 116, 101,
				97, 109, 80, 108, 97, 116, 102, 111, 114, 109,
				1, 0, 0, 0, 33, 82, 117, 115, 116, 46,
				80, 108, 97, 116, 102, 111, 114, 109, 46, 83,
				116, 101, 97, 109, 124, 83, 116, 101, 97, 109,
				80, 108, 97, 116, 102, 111, 114, 109, 1, 0,
				0, 0, 33, 82, 117, 115, 116, 46, 80, 108,
				97, 116, 102, 111, 114, 109, 46, 83, 116, 101,
				97, 109, 124, 83, 116, 101, 97, 109, 80, 108,
				97, 116, 102, 111, 114, 109, 1, 0, 0, 0,
				33, 82, 117, 115, 116, 46, 80, 108, 97, 116,
				102, 111, 114, 109, 46, 83, 116, 101, 97, 109,
				124, 83, 116, 101, 97, 109, 80, 108, 97, 116,
				102, 111, 114, 109, 0, 0, 0, 0, 31, 82,
				117, 115, 116, 46, 80, 108, 97, 116, 102, 111,
				114, 109, 46, 83, 116, 101, 97, 109, 124, 83,
				116, 101, 97, 109, 80, 108, 97, 121, 101, 114,
				0, 0, 0, 0, 36, 82, 117, 115, 116, 46,
				80, 108, 97, 116, 102, 111, 114, 109, 46, 83,
				116, 101, 97, 109, 124, 83, 116, 101, 97, 109,
				83, 101, 114, 118, 101, 114, 81, 117, 101, 114,
				121, 0, 0, 0, 0, 29, 82, 117, 115, 116,
				46, 80, 108, 97, 116, 102, 111, 114, 109, 46,
				83, 116, 101, 97, 109, 124, 83, 116, 101, 97,
				109, 85, 116, 105, 108, 0, 0, 0, 0, 40,
				82, 117, 115, 116, 46, 80, 108, 97, 116, 102,
				111, 114, 109, 46, 83, 116, 101, 97, 109, 124,
				83, 116, 101, 97, 109, 87, 111, 114, 107, 115,
				104, 111, 112, 67, 111, 110, 116, 101, 110, 116
			},
			TotalFiles = 22,
			TotalTypes = 23,
			IsEditorOnly = false
		};
	}
}
namespace Rust.Platform.Steam;

internal class IpListQuery : IServerQuery, IDisposable
{
	private readonly List<string> _ips;

	private readonly CancellationTokenSource _cts;

	private readonly List<(string Key, string Value)> _filters;

	private readonly Action<Steamworks.Data.ServerInfo> _serverFoundHandler;

	public IReadOnlyList<ServerInfo> Servers { get; }

	public event Action<ServerInfo> OnServerFound;

	public IpListQuery(IEnumerable<string> list)
	{
		IpListQuery ipListQuery = this;
		if (list == null)
		{
			throw new ArgumentNullException("list");
		}
		_ips = new List<string>(list);
		_cts = new CancellationTokenSource();
		_filters = new List<(string, string)>();
		List<ServerInfo> serverList = new List<ServerInfo>();
		Servers = serverList;
		HashSet<(uint, int)> foundServers = new HashSet<(uint, int)>();
		_serverFoundHandler = delegate(Steamworks.Data.ServerInfo server)
		{
			ServerInfo serverInfo = SteamPlatform.ToPlatformServer(server);
			if (foundServers.Add((server.AddressRaw, server.QueryPort)))
			{
				serverList.Add(serverInfo);
				ipListQuery.OnServerFound?.Invoke(serverInfo);
			}
		};
	}

	public void Dispose()
	{
		_cts.Cancel();
	}

	public void AddFilter(string key, string value)
	{
		_filters.Add((key, value));
	}

	public async Task RunQueryAsync(double timeoutSeconds = 10.0)
	{
		if (_ips == null || _ips.Count == 0)
		{
			return;
		}
		List<string> source = _ips.ToList();
		List<(string, string)> filters = _filters.ToList();
		int count = 5;
		int num = 0;
		List<Task> list = new List<Task>();
		while (!_cts.Token.IsCancellationRequested)
		{
			List<string> list2 = source.Skip(num).Take(count).ToList();
			if (list2.Count == 0)
			{
				break;
			}
			list.Add(QueryServers(list2, filters, timeoutSeconds));
			num += list2.Count;
		}
		await Task.WhenAll(list);
	}

	private async Task QueryServers(List<string> servers, List<(string Key, string Value)> filters, double timeoutSeconds)
	{
		using Internet query = new Internet();
		foreach (var (key, value) in filters)
		{
			query.AddFilter(key, value);
		}
		query.AddFilter("or", (servers.Count * 2).ToString());
		foreach (string server in servers)
		{
			query.AddFilter("gameaddr", server);
			query.AddFilter("addr", server);
		}
		query.OnResponsiveServer += _serverFoundHandler;
		await query.RunQueryAsync((float)timeoutSeconds);
	}
}
public class SteamAchievement : IAchievement
{
	private Achievement _achievement;

	public string Key => _achievement.Name;

	public bool IsUnlocked => _achievement.State;

	internal SteamAchievement(Achievement achievement)
	{
		_achievement = achievement;
	}

	public void Unlock()
	{
		_achievement.Trigger();
	}
}
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
public class SteamDownloadableContent : IDownloadableContent
{
	public int AppId { get; }

	public bool IsInstalled => SteamApps.IsDlcInstalled(AppId);

	public SteamDownloadableContent(int appId)
	{
		AppId = appId;
	}
}
public class SteamInventory : IPlayerInventory, IDisposable
{
	public InventoryResult Value { get; }

	public IReadOnlyList<IPlayerItem> Items { get; }

	internal SteamInventory(InventoryResult inventory)
	{
		Value = inventory;
		InventoryItem[] items = Value.GetItems(includeProperties: true);
		if (items == null)
		{
			Items = new List<IPlayerItem>(0);
			return;
		}
		Items = items.Select((InventoryItem i) => new SteamItem(i)).ToList();
	}

	public void Dispose()
	{
		Value.Dispose();
	}

	public bool BelongsTo(ulong userId)
	{
		return Value.BelongsTo(userId);
	}

	public byte[] Serialize()
	{
		return Value.Serialize();
	}
}
public class SteamItem : IPlayerItem
{
	public InventoryItem Value;

	public ulong Id => Value.Id.Value;

	public int DefinitionId => Value.DefId.Value;

	public int Quantity => Value.Quantity;

	public DateTimeOffset Acquired => Value.Acquired.ToUniversalTime();

	public ulong WorkshopId
	{
		get
		{
			if (!Value.Properties.TryGetValue("workshopid", out var value))
			{
				return 0uL;
			}
			return ulong.Parse(value);
		}
	}

	public string ItemShortName
	{
		get
		{
			if (!Value.Properties.TryGetValue("itemshortname", out var value))
			{
				return null;
			}
			return value;
		}
	}

	public SteamItem(InventoryItem item)
	{
		Value = item;
	}

	public async Task Consume()
	{
		await Value.ConsumeAsync();
	}
}
public class SteamItemDefinition : IPlayerItemDefinition, IEquatable<IPlayerItemDefinition>
{
	public InventoryDef Value { get; }

	public int DefinitionId => Value.Id;

	public string Name => Value.Name;

	public string Description => Value.Description;

	public string Type => Value.Type;

	public string IconUrl => Value.IconUrlLarge;

	public int LocalPrice => Value.LocalPrice;

	public string LocalPriceFormatted => Value.LocalPriceFormatted;

	public string PriceCategory => Value.PriceCategory;

	public bool IsGenerator => Value.IsGenerator;

	public bool IsTradable => Value.Tradable;

	public bool IsMarketable => Value.Marketable;

	public string StoreTags => Value.GetProperty<string>("store_tags");

	public DateTime Created => Value.Created;

	public DateTime Modified => Value.Modified;

	public string ItemShortName => Value.GetProperty<string>("itemshortname");

	public ulong WorkshopId => Value.GetProperty<ulong>("workshopid");

	public ulong WorkshopDownload => Value.GetProperty<ulong>("workshopdownload");

	internal SteamItemDefinition(InventoryDef value)
	{
		Value = value;
	}

	public static IPlayerItemDefinition FromInventoryDef(InventoryDef def)
	{
		return new SteamItemDefinition(def);
	}

	public IEnumerable<PlayerItemRecipe> GetRecipesContainingThis()
	{
		return Value.GetRecipesContainingThis().Select(SteamToPlatformRecipe);
	}

	private static PlayerItemRecipe SteamToPlatformRecipe(InventoryRecipe recipe)
	{
		return new PlayerItemRecipe(recipe.Ingredients.Select((InventoryRecipe.Ingredient i) => new PlayerItemRecipe.Ingredient(i.DefinitionId, i.Count)).ToList(), new SteamItemDefinition(recipe.Result));
	}

	public bool Equals(IPlayerItemDefinition other)
	{
		return Equals((object)other);
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		if (this == obj)
		{
			return true;
		}
		if (obj.GetType() != GetType())
		{
			return false;
		}
		return Value.Equals(((SteamItemDefinition)obj).Value);
	}

	public override int GetHashCode()
	{
		if (!(Value != null))
		{
			return 0;
		}
		return Value.GetHashCode();
	}
}
public class SteamPlatform : IPlatformService
{
	private IPlatformHooks _hooks;

	private bool _initialized;

	public bool IsValid => SteamServer.IsValid;

	public IReadOnlyList<IPlayerItemDefinition> ItemDefinitions { get; private set; }

	public bool Initialize(IPlatformHooks hooks)
	{
		if (_hooks != null && _hooks != hooks)
		{
			throw new InvalidOperationException("SteamPlatform was initialized with two different platform hooks");
		}
		_hooks = hooks ?? throw new ArgumentNullException("hooks");
		StartSteamServer();
		if (!_initialized)
		{
			Steamworks.SteamInventory.OnDefinitionsUpdated += OnDefinitionsUpdated;
			Steamworks.SteamInventory.LoadItemDefinitions();
			Dispatch.OnException = delegate(Exception e)
			{
				UnityEngine.Debug.LogException(e);
			};
			_initialized = true;
		}
		return true;
	}

	public void Shutdown()
	{
		using (TimeWarning.New("Steamworks.SteamServer.Shutdown"))
		{
			if (SteamServer.IsValid)
			{
				UnityEngine.Debug.Log("Steamworks Shutting Down");
				SteamServer.Shutdown();
				UnityEngine.Debug.Log("Okay");
			}
		}
	}

	public void Update()
	{
		if (SteamServer.IsValid)
		{
			SteamServer.RunCallbacks();
		}
	}

	private static AuthResponse RemapAuthResponse(Steamworks.AuthResponse response)
	{
		return response switch
		{
			Steamworks.AuthResponse.OK => AuthResponse.OK, 
			Steamworks.AuthResponse.VACBanned => AuthResponse.VACBanned, 
			Steamworks.AuthResponse.PublisherIssuedBan => AuthResponse.PublisherBanned, 
			Steamworks.AuthResponse.VACCheckTimedOut => AuthResponse.TimedOut, 
			Steamworks.AuthResponse.AuthTicketCanceled => AuthResponse.AuthTicketCanceled, 
			Steamworks.AuthResponse.AuthTicketInvalidAlreadyUsed => AuthResponse.AuthTicketAlreadyUsed, 
			Steamworks.AuthResponse.AuthTicketInvalid => AuthResponse.InvalidAuthSession, 
			Steamworks.AuthResponse.AuthTicketNetworkIdentityFailure => AuthResponse.NetworkIdentityFailure, 
			_ => AuthResponse.Invalid, 
		};
	}

	private static void DebugPrintSteamCallback(CallbackType type, string content, bool isServer)
	{
		string arg = (isServer ? "SteamServer" : "SteamClient");
		UnityEngine.Debug.Log($"[{arg}] {type}: {content}");
	}

	public bool PlayerOwnsDownloadableContent(ulong userId, int appId)
	{
		UserHasLicenseForAppResult userHasLicenseForAppResult = SteamServer.UserHasLicenseForApp(userId, appId);
		if (userHasLicenseForAppResult == UserHasLicenseForAppResult.NoAuth)
		{
			UnityEngine.Debug.LogWarning($"User tried to check DLC license but not authed ({userId})");
			return false;
		}
		return userHasLicenseForAppResult == UserHasLicenseForAppResult.HasLicense;
	}

	public void RefreshItemDefinitions()
	{
		Steamworks.SteamInventory.LoadItemDefinitions();
	}

	public IPlayerItemDefinition GetItemDefinition(int definitionId)
	{
		if (ItemDefinitions == null)
		{
			return null;
		}
		foreach (IPlayerItemDefinition itemDefinition in ItemDefinitions)
		{
			if (itemDefinition.DefinitionId == definitionId)
			{
				return itemDefinition;
			}
		}
		return null;
	}

	private void OnDefinitionsUpdated()
	{
		ItemDefinitions = Steamworks.SteamInventory.Definitions.Select((InventoryDef d) => new SteamItemDefinition(d)).ToList();
		_hooks.OnItemDefinitionsChanged();
	}

	public async Task<IPlayerInventory> DeserializeInventory(byte[] data)
	{
		InventoryResult? inventoryResult = await Steamworks.SteamInventory.DeserializeAsync(data);
		return inventoryResult.HasValue ? new SteamInventory(inventoryResult.Value) : null;
	}

	private void StartSteamServer()
	{
		if (SteamServer.IsValid)
		{
			return;
		}
		ServerParameters? serverParameters = _hooks.ServerParameters;
		if (serverParameters.HasValue)
		{
			ServerParameters value = serverParameters.Value;
			SteamServerInit init = new SteamServerInit(value.ShortName, value.FullName);
			init.IpAddress = value.Address;
			init.GamePort = value.GamePort;
			init.Secure = value.IsSecure;
			init.VersionString = value.Version;
			if (value.QueryPort > 0)
			{
				init.QueryPort = value.QueryPort;
			}
			else
			{
				init = init.WithQueryShareGamePort();
			}
			try
			{
				SteamServer.Init(_hooks.SteamAppId, init, asyncCallbacks: false);
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogWarning("Couldn't initialize Steam Server (" + ex.Message + ")");
				_hooks.Abort();
				return;
			}
			SteamServer.OnSteamServerConnectFailure += OnSteamConnectionFailure;
			SteamServer.OnSteamServersDisconnected += OnSteamServersDisconnected;
			SteamServer.OnSteamServersConnected += OnSteamConnected;
			SteamServer.DedicatedServer = true;
			if (value.HideIP)
			{
				SteamNetworkingSockets.RequestFakeIP();
			}
			SteamServer.LogOnAnonymous();
			SteamServer.OnValidateAuthTicketResponse += delegate(SteamId steamId, SteamId ownerSteamId, Steamworks.AuthResponse response)
			{
				_hooks.AuthSessionValidated(steamId, ownerSteamId, RemapAuthResponse(response), response.ToString());
			};
		}
	}

	private void OnSteamServersDisconnected(Result result)
	{
		UnityEngine.Debug.LogWarning($"SteamServer Disconnected ({result})");
	}

	private void OnSteamConnected()
	{
		UnityEngine.Debug.Log("SteamServer Connected");
	}

	private void OnSteamConnectionFailure(Result result, bool stilltrying)
	{
		UnityEngine.Debug.LogWarning($"SteamServer Connection Failure ({result})");
	}

	public bool BeginPlayerSession(ulong userId, byte[] authToken)
	{
		return SteamServer.BeginAuthSession(authToken, userId);
	}

	public void UpdatePlayerSession(ulong userId, string userName)
	{
		SteamServer.UpdatePlayer(userId, userName, 0);
	}

	public void EndPlayerSession(ulong userId)
	{
		SteamServer.EndSession(userId);
	}

	internal static ServerInfo ToPlatformServer(Steamworks.Data.ServerInfo info)
	{
		return new ServerInfo(info.AppId, info.Name, info.Address, info.ConnectionPort, info.QueryPort, info.Map, info.TagString, info.Secure, info.Players, info.MaxPlayers, info.LastTimePlayed, info.Ping, info.SteamId);
	}

	internal static Steamworks.Data.ServerInfo ToSteamServer(ServerInfo server)
	{
		return new Steamworks.Data.ServerInfo(server.AddressRaw, (ushort)server.ConnectionPort, (ushort)server.QueryPort, 0u);
	}

	public async Task<bool> LoadPlayerStats(ulong userId)
	{
		return await SteamServerStats.RequestUserStatsAsync(userId) == Result.OK;
	}

	public async Task<bool> SavePlayerStats(ulong userId)
	{
		return await SteamServerStats.StoreUserStats(userId) == Result.OK;
	}

	public long GetPlayerStatInt(ulong userId, string key, long defaultValue = 0L)
	{
		return SteamServerStats.GetInt(userId, key, (int)defaultValue);
	}

	public bool SetPlayerStatInt(ulong userId, string key, long value)
	{
		return SteamServerStats.SetInt(userId, key, (int)value);
	}
}
public sealed class SteamAuthTicketNullException : Exception
{
	public SteamAuthTicketNullException(string message)
		: base(message)
	{
	}
}
public class SteamPlayer : IPlayerInfo
{
	public Friend Value { get; }

	public ulong UserId => Value.Id;

	public string UserName => Value.Name;

	public bool IsOnline => Value.IsOnline;

	public bool IsMe => Value.IsMe;

	public bool IsFriend => Value.IsFriend;

	public bool IsPlayingThisGame => Value.IsPlayingThisGame;

	public string ServerEndpoint
	{
		get
		{
			if (!Value.GameInfo.HasValue)
			{
				return null;
			}
			return $"{Value.GameInfo.Value.IpAddress}:{Value.GameInfo.Value.ConnectionPort}";
		}
	}

	public SteamPlayer(Friend value)
	{
		Value = value;
	}
}
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
public static class SteamUtil
{
}
public class SteamWorkshopContent : IWorkshopContent
{
	public Item Value { get; }

	public ulong WorkshopId => Value.Id;

	public string Title => Value.Title;

	public string Description => Value.Description;

	public IEnumerable<string> Tags => Value.Tags;

	public string Url => Value.Url;

	public string PreviewImageUrl => Value.PreviewImageUrl;

	public ulong OwnerId => Value.Owner.Id;

	public IPlayerInfo Owner => new SteamPlayer(Value.Owner);

	public bool IsInstalled => Value.IsInstalled;

	public bool IsDownloadPending => Value.IsDownloadPending;

	public bool IsDownloading => Value.IsDownloading;

	public string Directory => Value.Directory;

	public SteamWorkshopContent(Item item)
	{
		Value = item;
	}

	public bool Download()
	{
		return Value.Download(highPriority: true);
	}
}
