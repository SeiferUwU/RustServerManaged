using UnityEngine;

namespace Rust.RenderPipeline.Runtime.Passes.Lighting;

public struct AdditionalShadowData
{
	public const int STRIDE = 80;

	public Vector4 tileData;

	public Matrix4x4 shadowMatrix;

	public AdditionalShadowData(Vector2 offset, float scale, float bias, float border, Matrix4x4 matrix)
	{
		tileData.x = offset.x * scale + border;
		tileData.y = offset.y * scale + border;
		tileData.z = scale - border - border;
		tileData.w = bias;
		shadowMatrix = matrix;
	}
}
