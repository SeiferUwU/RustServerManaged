#define UNITY_ASSERTIONS
using Unity.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Rust.Rendering.IndirectInstancing;

internal struct TerrainRef
{
	public NativeArray<short>.ReadOnly data;

	public Vector3 pos;

	public Vector3 size;

	public Vector3 one_over_size;

	public int res;

	public static Rust.Rendering.IndirectInstancing.TerrainRef FromCurrent()
	{
		Assert.IsNotNull(TerrainMeta.HeightMap, "Cannot create TerrainRef because there is no terrain!");
		return new Rust.Rendering.IndirectInstancing.TerrainRef
		{
			data = TerrainMeta.HeightMap.src.AsReadOnly(),
			pos = TerrainMeta.Position,
			size = TerrainMeta.Size,
			one_over_size = TerrainMeta.OneOverSize,
			res = TerrainMeta.HeightMap.res
		};
	}
}
