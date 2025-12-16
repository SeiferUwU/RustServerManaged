using UnityEngine;

namespace Rust.RenderPipeline.Runtime.Passes.Lighting;

public struct DirectionalShadowCascade
{
	public const int STRIDE = 32;

	public Vector4 cullingSphere;

	public Vector4 data;

	public DirectionalShadowCascade(Vector4 cullingSphere, float tileSize, float filterSize)
	{
		float num = 2f * cullingSphere.w / tileSize;
		filterSize *= num;
		cullingSphere.w -= filterSize;
		cullingSphere.w *= cullingSphere.w;
		this.cullingSphere = cullingSphere;
		data = new Vector4(1f / cullingSphere.w, filterSize * 1.4142137f);
	}
}
