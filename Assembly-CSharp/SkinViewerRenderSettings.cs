using UnityEngine;

public class SkinViewerRenderSettings : MonoBehaviour
{
	public bool overrideFullScreenPosition;

	public Vector3 fullScreenPositionOffset;

	[Space]
	public bool overrideFullScreenRotation;

	public Vector3 fullScreenRotation;

	[Space]
	public bool overrideZoom;

	public Vector2 minMaxZoom = new Vector2(20f, 8f);
}
