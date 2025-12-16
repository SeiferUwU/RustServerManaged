using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class AtmosphereVolumeRenderer : MonoBehaviour
{
	public FogMode Mode = FogMode.ExponentialSquared;

	public bool DistanceFog = true;

	public bool HeightFog = true;

	public AtmosphereVolume Volume;

	private static bool isSupported
	{
		get
		{
			if (Application.platform != RuntimePlatform.OSXEditor)
			{
				return Application.platform != RuntimePlatform.OSXPlayer;
			}
			return false;
		}
	}
}
