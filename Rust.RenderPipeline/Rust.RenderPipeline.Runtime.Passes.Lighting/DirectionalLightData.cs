using UnityEngine;
using UnityEngine.Rendering;

namespace Rust.RenderPipeline.Runtime.Passes.Lighting;

public struct DirectionalLightData
{
	public const int STRIDE = 48;

	public Vector4 color;

	public Vector4 directionAndMask;

	public Vector4 shadowData;

	public DirectionalLightData(ref VisibleLight visibleLight, Light light, Vector4 shadowData)
	{
		color = visibleLight.finalColor;
		directionAndMask = -visibleLight.localToWorldMatrix.GetColumn(2);
		directionAndMask.w = light.renderingLayerMask.ReinterpretAsFloat();
		this.shadowData = shadowData;
	}
}
