using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Facepunch;
using Facepunch.Extend;
using Network;
using UnityEngine;

namespace ConVar;

[Factory("pool")]
public class Pool : ConsoleSystem
{
	[ClientVar(ClientAdmin = true)]
	[ServerVar]
	public static int mode = 2;

	[ClientVar]
	[ServerVar]
	public static bool prewarm = true;

	[ClientVar]
	[ServerVar]
	public static bool enabled = true;

	[ServerVar]
	[ClientVar]
	public static bool debug = false;

	[ClientVar]
	[ServerVar]
	public static void print_memory(Arg arg)
	{
		if (Facepunch.Pool.Directory.Count == 0)
		{
			arg.ReplyWith("Memory pool is empty.");
			return;
		}
		bool flag = arg.HasArg("--raw", remove: true);
		bool flag2 = arg.HasArg("--json", remove: true);
		string text = arg.GetString(0, null);
		using TextTable textTable = Facepunch.Pool.Get<TextTable>();
		textTable.ShouldPadColumns = !flag2;
		textTable.AddColumn("type");
		textTable.AddColumn("capacity");
		textTable.AddColumn("pooled");
		textTable.AddColumn("active");
		textTable.AddColumn("max");
		textTable.AddColumn("hits");
		textTable.AddColumn("misses");
		textTable.AddColumn("spills");
		foreach (KeyValuePair<Type, Facepunch.Pool.IPoolCollection> item in Facepunch.Pool.Directory.OrderByDescending((KeyValuePair<Type, Facepunch.Pool.IPoolCollection> x) => x.Value.ItemsCreated))
		{
			Type key = item.Key;
			Facepunch.Pool.IPoolCollection value = item.Value;
			if (text == null || key.ToString().Contains(text))
			{
				textTable.AddRow(key.ToString().Replace("System.Collections.Generic.", ""), flag ? value.ItemsCapacity.ToString() : value.ItemsCapacity.FormatNumberShort(), flag ? value.ItemsInStack.ToString() : value.ItemsInStack.FormatNumberShort(), flag ? value.ItemsInUse.ToString() : value.ItemsInUse.FormatNumberShort(), flag ? value.MaxItemsInUse.ToString() : value.MaxItemsInUse.FormatNumberShort(), flag ? value.ItemsTaken.ToString() : value.ItemsTaken.FormatNumberShort(), flag ? value.ItemsCreated.ToString() : value.ItemsCreated.FormatNumberShort(), flag ? value.ItemsSpilled.ToString() : value.ItemsSpilled.FormatNumberShort());
			}
		}
		arg.ReplyWith(flag2 ? textTable.ToJson() : textTable.ToString());
	}

	[ClientVar]
	[ServerVar]
	public static void reset_max_pool_counter(Arg arg)
	{
		if (Facepunch.Pool.Directory.Count == 0)
		{
			arg.ReplyWith("Memory pool is empty.");
			return;
		}
		foreach (Facepunch.Pool.IPoolCollection value in Facepunch.Pool.Directory.Values)
		{
			value.ResetMaxUsageCounter();
		}
		arg.ReplyWith("Reset max item counter of pool");
	}

	[ClientVar]
	[ServerVar]
	public static void print_arraypool(Arg arg)
	{
		bool flag = arg.HasArg("--json");
		string text = (flag ? "[" : string.Empty);
		string table = PrintArrayPool<byte>(BaseNetwork.ArrayPool, flag);
		text += FormatTable("BaseNetwork.ArrayPool", table, flag);
		text += (flag ? "," : "\n");
		string table2 = PrintArrayPool<byte>(BufferStream.Shared.ArrayPool, flag);
		text += FormatTable("ProtocolParser.ArrayPool", table2, flag);
		if (flag)
		{
			text += "]";
		}
		arg.ReplyWith(text);
		static string FormatTable(string name, string text2, bool toJson)
		{
			if (!toJson)
			{
				return name + "\n" + text2;
			}
			return "{\"name\":\"" + name + "\",\"content\":" + text2 + "}";
		}
		unsafe static string PrintArrayPool<T>(ArrayPool<T> pool, bool toJson) where T : unmanaged
		{
			ConcurrentQueue<T[]>[] buffer = pool.GetBuffer();
			using TextTable textTable = Facepunch.Pool.Get<TextTable>();
			textTable.ShouldPadColumns = !toJson;
			textTable.ResizeColumns(5);
			textTable.AddColumn("index");
			textTable.AddColumn("size");
			textTable.AddColumn("bytes");
			textTable.AddColumn("count");
			textTable.AddColumn("memory");
			textTable.ResizeRows(buffer.Length);
			int num = 1;
			num = sizeof(T);
			for (int i = 0; i < buffer.Length; i++)
			{
				int num2 = pool.IndexToSize(i);
				int num3 = num2 * num;
				int count = buffer[i].Count;
				int input = num3 * count;
				textTable.AddValue(i);
				textTable.AddValue(num2);
				textTable.AddValue(num2.FormatBytes());
				textTable.AddValue(count);
				textTable.AddValue(input.FormatBytes());
			}
			return toJson ? textTable.ToJson(stringify: false) : textTable.ToString();
		}
	}

	[ServerVar]
	[ClientVar]
	public static void print_prefabs(Arg arg)
	{
		PrefabPoolCollection pool = GameManager.server.pool;
		if (pool.storage.Count == 0)
		{
			arg.ReplyWith("Prefab pool is empty.");
			return;
		}
		string text = arg.GetString(0, string.Empty);
		bool flag = arg.HasArg("--json");
		using TextTable textTable = Facepunch.Pool.Get<TextTable>();
		textTable.ShouldPadColumns = !flag;
		textTable.AddColumn("id");
		textTable.AddColumn("name");
		textTable.AddColumn("missed");
		textTable.AddColumn("count");
		textTable.AddColumn("target");
		textTable.AddColumn("added");
		textTable.AddColumn("removed");
		foreach (PrefabPool item in pool.storage.Values.OrderByDescending((PrefabPool x) => x.Missed))
		{
			string text2 = StringPool.Get(item.PrefabName).ToString();
			string prefabName = item.PrefabName;
			string text3 = item.Count.ToString();
			if (string.IsNullOrEmpty(text) || prefabName.Contains(text, CompareOptions.IgnoreCase))
			{
				textTable.AddRow(text2, Path.GetFileNameWithoutExtension(prefabName), text3, item.TargetCapacity.ToString(), item.Missed.ToString(), item.Pushed.ToString(), item.Popped.ToString());
			}
		}
		arg.ReplyWith(flag ? textTable.ToJson() : textTable.ToString());
	}

	[ServerVar]
	[ClientVar]
	public static void print_assets(Arg arg)
	{
		if (AssetPool.storage.Count == 0)
		{
			arg.ReplyWith("Asset pool is empty.");
			return;
		}
		string text = arg.GetString(0, string.Empty);
		bool flag = arg.HasArg("--json");
		using TextTable textTable = Facepunch.Pool.Get<TextTable>();
		textTable.ShouldPadColumns = !flag;
		textTable.AddColumn("type");
		textTable.AddColumn("allocated");
		textTable.AddColumn("available");
		foreach (KeyValuePair<Type, AssetPool.Pool> item in AssetPool.storage)
		{
			string text2 = item.Key.ToString();
			string text3 = item.Value.allocated.ToString();
			string text4 = item.Value.available.ToString();
			if (string.IsNullOrEmpty(text) || text2.Contains(text, CompareOptions.IgnoreCase))
			{
				textTable.AddRow(text2, text3, text4);
			}
		}
		arg.ReplyWith(flag ? textTable.ToJson() : textTable.ToString());
	}

	[ServerVar]
	[ClientVar]
	public static void clear_memory(Arg arg)
	{
		Facepunch.Pool.Clear(arg.GetString(0, string.Empty));
	}

	[ServerVar]
	[ClientVar]
	public static void clear_prefabs(Arg arg)
	{
		string filter = arg.GetString(0, string.Empty);
		GameManager.server.pool.Clear(filter);
	}

	[ServerVar]
	[ClientVar]
	public static void clear_assets(Arg arg)
	{
		AssetPool.Clear(arg.GetString(0, string.Empty));
	}

	[ClientVar]
	[ServerVar]
	public static void export_prefabs(Arg arg)
	{
		PrefabPoolCollection pool = GameManager.server.pool;
		if (pool.storage.Count == 0)
		{
			arg.ReplyWith("Prefab pool is empty.");
			return;
		}
		string text = arg.GetString(0, string.Empty);
		StringBuilder stringBuilder = new StringBuilder();
		foreach (KeyValuePair<uint, PrefabPool> item in pool.storage)
		{
			string arg2 = item.Key.ToString();
			string text2 = StringPool.Get(item.Key);
			string arg3 = item.Value.Count.ToString();
			if (string.IsNullOrEmpty(text) || text2.Contains(text, CompareOptions.IgnoreCase))
			{
				stringBuilder.AppendLine($"{arg2},{Path.GetFileNameWithoutExtension(text2)},{arg3}");
			}
		}
		File.WriteAllText("prefabs.csv", stringBuilder.ToString());
	}

	[ServerVar]
	[ClientVar]
	public static void fill_prefabs(Arg arg)
	{
		string filter = arg.GetString(0, string.Empty);
		int countOverride = arg.GetInt(1);
		PrefabPoolWarmup.Run(filter, countOverride);
	}
}
