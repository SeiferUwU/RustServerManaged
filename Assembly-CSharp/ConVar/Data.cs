using System.IO;
using UnityEngine;

namespace ConVar;

[Factory("data")]
public class Data : ConsoleSystem
{
	[ClientVar]
	[ServerVar]
	public static void export(Arg args)
	{
		string text = args.GetString(0, "none");
		string text2 = Path.Combine(Application.persistentDataPath, text + ".raw");
		switch (text)
		{
		case "splatmap":
			if ((bool)TerrainMeta.SplatMap)
			{
				RawWriter.Write(TerrainMeta.SplatMap.ToEnumerable(), text2);
			}
			break;
		case "heightmap":
			if ((bool)TerrainMeta.HeightMap)
			{
				RawWriter.Write(TerrainMeta.HeightMap.ToEnumerable(), text2);
			}
			break;
		case "biomemap":
			if ((bool)TerrainMeta.BiomeMap)
			{
				RawWriter.Write(TerrainMeta.BiomeMap.ToEnumerable(), text2);
			}
			break;
		case "topologymap":
			if ((bool)TerrainMeta.TopologyMap)
			{
				RawWriter.Write(TerrainMeta.TopologyMap.ToEnumerable(), text2);
			}
			break;
		case "alphamap":
			if ((bool)TerrainMeta.AlphaMap)
			{
				RawWriter.Write(TerrainMeta.AlphaMap.ToEnumerable(), text2);
			}
			break;
		case "watermap":
			if ((bool)TerrainMeta.WaterMap)
			{
				RawWriter.Write(TerrainMeta.WaterMap.ToEnumerable(), text2);
			}
			break;
		default:
			args.ReplyWith("Unknown export source: " + text);
			return;
		}
		args.ReplyWith("Export written to " + text2);
	}
}
