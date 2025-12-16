using Development.Attributes;
using UnityEngine;

[ResetStaticFields]
public class LightLOD : MonoBehaviour, ILOD, IClientComponent
{
	public float DistanceBias;

	public bool ToggleLight;

	public bool ToggleShadows = true;

	protected void OnValidate()
	{
		LightEx.CheckConflict(base.gameObject);
	}
}
