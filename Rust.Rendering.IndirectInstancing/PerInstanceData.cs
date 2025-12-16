using System.Runtime.InteropServices;
using UnityEngine;

namespace Rust.Rendering.IndirectInstancing;

internal struct PerInstanceData
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct Property
	{
		public static readonly int _ObjectToWorld = Shader.PropertyToID("_ObjectToWorld");

		public static readonly int _TextureIndex = Shader.PropertyToID("_TextureIndex");

		public static readonly int _Color = Shader.PropertyToID("_Color");

		public static readonly int _DetailColor = Shader.PropertyToID("_DetailColor");

		public static readonly int _DetailAlbedoMap_ST = Shader.PropertyToID("_DetailAlbedoMap_ST");

		public static readonly int _EmissionColor = Shader.PropertyToID("_EmissionColor");
	}

	public int ReorderOffset;

	public Matrix4x4 _ObjectToWorld;

	public int _TextureIndex;

	public Color _Color;

	public Color _DetailColor;

	public Vector4 _DetailAlbedoMap_ST;

	public Color _EmissionColor;

	public float MinDistance;

	public float MaxDistance;
}
