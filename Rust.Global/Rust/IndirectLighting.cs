using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Rust;

public static class IndirectLighting
{
	public struct AmbientProbeParams
	{
		public SphericalHarmonicsL2 sh;

		public float intensity;
	}

	public static readonly Dictionary<Camera, AmbientProbeParams> ambientProbeCache = new Dictionary<Camera, AmbientProbeParams>();

	private static SphericalHarmonicsL2[] lightProbe = new SphericalHarmonicsL2[1];

	public static SphericalHarmonicsL2[] LightProbe => lightProbe;

	public static void UpdateLightProbe()
	{
		LightProbes.GetInterpolatedProbe(Vector3.zero, null, out lightProbe[0]);
	}

	public static void UpdateAmbientProbe(Camera camera)
	{
		SphericalHarmonicsL2 sh = RenderSettings.ambientProbe;
		switch (RenderSettings.ambientMode)
		{
		case AmbientMode.Flat:
			sh = default(SphericalHarmonicsL2);
			sh.AddAmbientLight(RenderSettings.ambientSkyColor.linear * RenderSettings.ambientIntensity);
			break;
		case AmbientMode.Trilight:
		{
			Color color = RenderSettings.ambientSkyColor.linear * RenderSettings.ambientIntensity;
			Color color2 = RenderSettings.ambientEquatorColor.linear * RenderSettings.ambientIntensity;
			Color color3 = RenderSettings.ambientGroundColor.linear * RenderSettings.ambientIntensity;
			sh = default(SphericalHarmonicsL2);
			sh.AddAmbientLight(color2);
			sh.AddDirectionalLight(Vector3.up, color - color2, 0.5f);
			sh.AddDirectionalLight(Vector3.down, color3 - color2, 0.5f);
			break;
		}
		default:
			throw new ArgumentOutOfRangeException();
		case AmbientMode.Skybox:
		case AmbientMode.Custom:
			break;
		}
		AmbientProbeParams value = new AmbientProbeParams
		{
			sh = sh,
			intensity = RenderSettings.ambientIntensity
		};
		ambientProbeCache[camera] = value;
	}
}
