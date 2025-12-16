using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Rust.RenderPipeline.Runtime.Passes.Lighting;

public struct AdditionalLightData
{
	public const int STRIDE = 80;

	public Vector4 color;

	public Vector4 position;

	public Vector4 directionAndMask;

	public Vector4 spotAngle;

	public Vector4 shadowData;

	public static AdditionalLightData CreatePointLight(ref VisibleLight visibleLight, Light light, Vector4 shadowData)
	{
		AdditionalLightData result = default(AdditionalLightData);
		result.color = visibleLight.finalColor;
		result.position = visibleLight.localToWorldMatrix.GetColumn(3);
		result.position.w = 1f / Mathf.Max(visibleLight.range * visibleLight.range, 1E-05f);
		result.spotAngle = new Vector4(0f, 1f);
		result.directionAndMask = Vector4.zero;
		result.directionAndMask.w = light.renderingLayerMask.ReinterpretAsFloat();
		result.shadowData = shadowData;
		return result;
	}

	public static AdditionalLightData CreateSpotLight(ref VisibleLight visibleLight, Light light, Vector4 shadowData)
	{
		AdditionalLightData result = default(AdditionalLightData);
		result.color = visibleLight.finalColor;
		result.position = visibleLight.localToWorldMatrix.GetColumn(3);
		result.position.w = 1f / Mathf.Max(visibleLight.range * visibleLight.range, 1E-05f);
		result.directionAndMask = -visibleLight.localToWorldMatrix.GetColumn(2);
		result.directionAndMask.w = light.renderingLayerMask.ReinterpretAsFloat();
		float num = Mathf.Cos(MathF.PI / 360f * light.innerSpotAngle);
		float num2 = Mathf.Cos(MathF.PI / 360f * visibleLight.spotAngle);
		float num3 = 1f / Mathf.Max(num - num2, 0.001f);
		result.spotAngle = new Vector4(num3, (0f - num2) * num3);
		result.shadowData = shadowData;
		return result;
	}
}
