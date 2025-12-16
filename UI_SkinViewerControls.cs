using UnityEngine;

public class UI_SkinViewerControls : MonoBehaviour
{
	[SerializeField]
	private Canvas canvas;

	[SerializeField]
	private CoverImage coverImage;

	[SerializeField]
	private float maxYaw = 8f;

	[SerializeField]
	private float maxPitch = 4f;

	[SerializeField]
	private float lerpSpeed = 6f;

	[SerializeField]
	private float responseCurve = 1.5f;

	[Header("Drag")]
	[SerializeField]
	private float inertiaDecay = 5f;

	[SerializeField]
	private float rotationSpeed = 300f;

	[Header("Pan")]
	[SerializeField]
	private Vector2 panLimitX = new Vector2(-1f, 1f);

	[SerializeField]
	private Vector2 panLimitY = new Vector2(-1f, 1f);

	[SerializeField]
	private float panSpeed = 0.005f;

	[Header("Zoom")]
	[SerializeField]
	private float zoomSpeed = 0.1f;

	[SerializeField]
	private Vector2 minMaxFov = new Vector2(20f, 8f);

	[SerializeField]
	[Header("Idle")]
	private float idleSwaySpeed = 1f;

	[SerializeField]
	private float idleSwayAmount = 1.5f;

	[SerializeField]
	private float swayEaseSpeed = 1f;

	[SerializeField]
	private float swayDelay = 0.3f;

	[Space]
	[SerializeField]
	private bool fullScreenOnly;
}
