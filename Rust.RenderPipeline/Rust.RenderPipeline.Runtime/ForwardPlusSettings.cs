using System;
using UnityEngine;

namespace Rust.RenderPipeline.Runtime;

[Serializable]
public struct ForwardPlusSettings
{
	public enum TileSize
	{
		Default = 0,
		_16 = 0x10,
		_32 = 0x20,
		_64 = 0x40,
		_128 = 0x80,
		_256 = 0x100
	}

	[Tooltip("Tile size in pixels per dimension, default is 64.")]
	public TileSize tileSize;

	[Range(0f, 99f)]
	[Tooltip("Maximum allowed lights per tile, 0 means default, which is 31.")]
	public int maxLightsPerTile;
}
