using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Facepunch;
using LZ4;
using ProtoBuf;
using Unity.Collections;
using UnityEngine;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyVersion("0.0.0.0")]
public static class TerrainBiome
{
	[Flags]
	public enum Enum
	{
		Arid = 1,
		Temperate = 2,
		Tundra = 4,
		Arctic = 8,
		Jungle = 0x10
	}

	public const int COUNT = 5;

	public const int EVERYTHING = -1;

	public const int NOTHING = 0;

	public const int ARID = 1;

	public const int TEMPERATE = 2;

	public const int TUNDRA = 4;

	public const int ARCTIC = 8;

	public const int JUNGLE = 16;

	public const int ARID_IDX = 0;

	public const int TEMPERATE_IDX = 1;

	public const int TUNDRA_IDX = 2;

	public const int ARCTIC_IDX = 3;

	public const int JUNGLE_IDX = 4;

	private static Dictionary<int, int> type2index = new Dictionary<int, int>
	{
		{ 1, 0 },
		{ 2, 1 },
		{ 4, 2 },
		{ 8, 3 },
		{ 16, 4 }
	};

	public static int TypeToIndex(int id)
	{
		return type2index[id];
	}

	public static int IndexToType(int idx)
	{
		return 1 << idx;
	}
}
public static class TerrainSplat
{
	public enum Enum
	{
		Dirt = 1,
		Snow = 2,
		Sand = 4,
		Rock = 8,
		Grass = 0x10,
		Forest = 0x20,
		Stones = 0x40,
		Gravel = 0x80
	}

	public const int COUNT = 8;

	public const int EVERYTHING = -1;

	public const int NOTHING = 0;

	public const int DIRT = 1;

	public const int SNOW = 2;

	public const int SAND = 4;

	public const int ROCK = 8;

	public const int GRASS = 16;

	public const int FOREST = 32;

	public const int STONES = 64;

	public const int GRAVEL = 128;

	public const int DIRT_IDX = 0;

	public const int SNOW_IDX = 1;

	public const int SAND_IDX = 2;

	public const int ROCK_IDX = 3;

	public const int GRASS_IDX = 4;

	public const int FOREST_IDX = 5;

	public const int STONES_IDX = 6;

	public const int GRAVEL_IDX = 7;

	private static Dictionary<int, int> type2index = new Dictionary<int, int>
	{
		{ 8, 3 },
		{ 16, 4 },
		{ 4, 2 },
		{ 1, 0 },
		{ 32, 5 },
		{ 64, 6 },
		{ 2, 1 },
		{ 128, 7 }
	};

	public static Dictionary<int, int> GetType2IndexDic()
	{
		return new Dictionary<int, int>(type2index);
	}

	public static int TypeToIndex(int id)
	{
		return type2index[id];
	}

	public static int IndexToType(int idx)
	{
		return 1 << idx;
	}
}
public static class TerrainTopology
{
	[Flags]
	public enum Enum
	{
		Field = 1,
		Cliff = 2,
		Summit = 4,
		Beachside = 8,
		Beach = 0x10,
		Forest = 0x20,
		Forestside = 0x40,
		Ocean = 0x80,
		Oceanside = 0x100,
		Decor = 0x200,
		Monument = 0x400,
		Road = 0x800,
		Roadside = 0x1000,
		Swamp = 0x2000,
		River = 0x4000,
		Riverside = 0x8000,
		Lake = 0x10000,
		Lakeside = 0x20000,
		Offshore = 0x40000,
		Rail = 0x80000,
		Railside = 0x100000,
		Building = 0x200000,
		Cliffside = 0x400000,
		Mountain = 0x800000,
		Clutter = 0x1000000,
		Alt = 0x2000000,
		Tier0 = 0x4000000,
		Tier1 = 0x8000000,
		Tier2 = 0x10000000,
		Mainland = 0x20000000,
		Hilltop = 0x40000000
	}

	public const int COUNT = 31;

	public const int EVERYTHING = -1;

	public const int NOTHING = 0;

	public const int FIELD = 1;

	public const int CLIFF = 2;

	public const int SUMMIT = 4;

	public const int BEACHSIDE = 8;

	public const int BEACH = 16;

	public const int FOREST = 32;

	public const int FORESTSIDE = 64;

	public const int OCEAN = 128;

	public const int OCEANSIDE = 256;

	public const int DECOR = 512;

	public const int MONUMENT = 1024;

	public const int ROAD = 2048;

	public const int ROADSIDE = 4096;

	public const int SWAMP = 8192;

	public const int RIVER = 16384;

	public const int RIVERSIDE = 32768;

	public const int LAKE = 65536;

	public const int LAKESIDE = 131072;

	public const int OFFSHORE = 262144;

	public const int RAIL = 524288;

	public const int RAILSIDE = 1048576;

	public const int BUILDING = 2097152;

	public const int CLIFFSIDE = 4194304;

	public const int MOUNTAIN = 8388608;

	public const int CLUTTER = 16777216;

	public const int ALT = 33554432;

	public const int TIER0 = 67108864;

	public const int TIER1 = 134217728;

	public const int TIER2 = 268435456;

	public const int MAINLAND = 536870912;

	public const int HILLTOP = 1073741824;

	public const int PATH = 526336;

	public const int PATHSIDE = 1052672;

	public const int WATER = 82048;

	public const int WATERSIDE = 164096;

	public const int SAND = 197016;
}
public class WorldSerialization
{
	public const uint PreviousVersion = 9u;

	public const uint CurrentVersion = 10u;

	public WorldData world = new WorldData
	{
		size = 4000u,
		maps = new List<MapData>(),
		prefabs = new List<PrefabData>(),
		paths = new List<PathData>()
	};

	public uint Version { get; private set; }

	public string Checksum { get; private set; }

	public long Timestamp { get; private set; }

	public WorldSerialization()
	{
		Version = 10u;
		Checksum = null;
		Timestamp = 0L;
	}

	public MapData GetMap(string name)
	{
		for (int i = 0; i < world.maps.Count; i++)
		{
			if (world.maps[i].name == name)
			{
				return world.maps[i];
			}
		}
		return null;
	}

	public void AddMap(string name, byte[] data)
	{
		MapData mapData = new MapData();
		mapData.name = name;
		mapData.data = data;
		world.maps.Add(mapData);
	}

	public IEnumerable<PrefabData> GetPrefabs(string category)
	{
		return world.prefabs.Where((PrefabData p) => p.category == category);
	}

	public void AddPrefab(string category, uint id, Vector3 position, Quaternion rotation, Vector3 scale)
	{
		PrefabData prefabData = new PrefabData();
		prefabData.category = category;
		prefabData.id = id;
		prefabData.position = position;
		prefabData.rotation = rotation;
		prefabData.scale = scale;
		world.prefabs.Add(prefabData);
	}

	public IEnumerable<PathData> GetPaths(string name)
	{
		return world.paths.Where((PathData p) => p.name.Contains(name));
	}

	public PathData GetPath(string name)
	{
		for (int i = 0; i < world.paths.Count; i++)
		{
			if (world.paths[i].name == name)
			{
				return world.paths[i];
			}
		}
		return null;
	}

	public void AddPath(PathData path)
	{
		world.paths.Add(path);
	}

	public void Clear()
	{
		world.maps.Clear();
		world.prefabs.Clear();
		world.paths.Clear();
		Version = 10u;
		Checksum = null;
	}

	public void Save(string fileName)
	{
		long value = DateTimeOffset.Now.ToUnixTimeMilliseconds();
		try
		{
			using BufferStream bufferStream = Pool.Get<BufferStream>().Initialize();
			WorldData.Serialize(bufferStream, world);
			ArraySegment<byte> buffer = bufferStream.GetBuffer();
			using FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
			using BinaryWriter binaryWriter = new BinaryWriter(fileStream);
			binaryWriter.Write(Version);
			binaryWriter.Write(value);
			using LZ4Stream lZ4Stream = new LZ4Stream(fileStream, LZ4StreamMode.Compress);
			lZ4Stream.Write(buffer.Array, buffer.Offset, buffer.Count);
			Checksum = Hash();
		}
		catch (Exception message)
		{
			UnityEngine.Debug.LogError(message);
		}
	}

	public void Load(string fileName)
	{
		try
		{
			using FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			ReadWorldData(stream);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError(ex.Message);
		}
	}

	public void Load(byte[] data)
	{
		try
		{
			using ByteArrayStream stream = new ByteArrayStream(data, 0, data.Length);
			ReadWorldData(stream);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError(ex.Message);
		}
	}

	public void ReadWorldData(Stream stream)
	{
		using (BinaryReader binaryReader = new BinaryReader(stream))
		{
			Version = binaryReader.ReadUInt32();
			if (Version == 9)
			{
				LoadWorldFromStream(stream);
				Version = 10u;
				Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
			}
			else if (Version == 10)
			{
				Timestamp = binaryReader.ReadInt64();
				LoadWorldFromStream(stream);
			}
			Checksum = Hash();
		}
		void LoadWorldFromStream(Stream innerStream)
		{
			using LZ4Stream lZ4Stream = new LZ4Stream(innerStream, LZ4StreamMode.Decompress);
			using NativeMemoryStream nativeMemoryStream = new NativeMemoryStream(60000000, Allocator.Temp);
			lZ4Stream.CopyTo(nativeMemoryStream);
			nativeMemoryStream.Position = 0L;
			using BufferStream stream2 = Pool.Get<BufferStream>().Initialize(nativeMemoryStream.Span);
			world = WorldData.Deserialize(stream2);
		}
	}

	public void CalculateChecksum()
	{
		Checksum = Hash();
	}

	private string Hash()
	{
		Checksum checksum = new Checksum();
		List<PrefabData> prefabs = world.prefabs;
		if (prefabs != null)
		{
			for (int i = 0; i < prefabs.Count; i++)
			{
				PrefabData prefabData = prefabs[i];
				checksum.Add(prefabData.id);
				checksum.Add(prefabData.position.x, 3);
				checksum.Add(prefabData.position.y, 3);
				checksum.Add(prefabData.position.z, 3);
				checksum.Add(prefabData.rotation.x, 3);
				checksum.Add(prefabData.rotation.y, 3);
				checksum.Add(prefabData.rotation.z, 3);
				checksum.Add(prefabData.scale.x, 3);
				checksum.Add(prefabData.scale.y, 3);
				checksum.Add(prefabData.scale.z, 3);
			}
		}
		return checksum.MD5();
	}

	public int CalculateCount()
	{
		return world.maps.Count + world.prefabs.Count + world.paths.Count;
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
			FilePathsData = new byte[209]
			{
				0, 0, 0, 1, 0, 0, 0, 42, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 82, 117, 115, 116, 46, 87,
				111, 114, 108, 100, 92, 84, 101, 114, 114, 97,
				105, 110, 66, 105, 111, 109, 101, 46, 99, 115,
				0, 0, 0, 1, 0, 0, 0, 42, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 82, 117, 115, 116, 46, 87,
				111, 114, 108, 100, 92, 84, 101, 114, 114, 97,
				105, 110, 83, 112, 108, 97, 116, 46, 99, 115,
				0, 0, 0, 1, 0, 0, 0, 45, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 82, 117, 115, 116, 46, 87,
				111, 114, 108, 100, 92, 84, 101, 114, 114, 97,
				105, 110, 84, 111, 112, 111, 108, 111, 103, 121,
				46, 99, 115, 0, 0, 0, 1, 0, 0, 0,
				48, 92, 65, 115, 115, 101, 116, 115, 92, 80,
				108, 117, 103, 105, 110, 115, 92, 82, 117, 115,
				116, 46, 87, 111, 114, 108, 100, 92, 87, 111,
				114, 108, 100, 83, 101, 114, 105, 97, 108, 105,
				122, 97, 116, 105, 111, 110, 46, 99, 115
			},
			TypesData = new byte[81]
			{
				0, 0, 0, 0, 13, 124, 84, 101, 114, 114,
				97, 105, 110, 66, 105, 111, 109, 101, 0, 0,
				0, 0, 13, 124, 84, 101, 114, 114, 97, 105,
				110, 83, 112, 108, 97, 116, 0, 0, 0, 0,
				16, 124, 84, 101, 114, 114, 97, 105, 110, 84,
				111, 112, 111, 108, 111, 103, 121, 0, 0, 0,
				0, 19, 124, 87, 111, 114, 108, 100, 83, 101,
				114, 105, 97, 108, 105, 122, 97, 116, 105, 111,
				110
			},
			TotalFiles = 4,
			TotalTypes = 4,
			IsEditorOnly = false
		};
	}
}
