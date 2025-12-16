using UnityEngine;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(CommandBufferManager))]
public class DeployGuideCamera : SingletonComponent<DeployGuideCamera>
{
	public DeployGuideMaterial GoodMaterial;

	public DeployGuideMaterial BadMaterial;

	public DeployGuideMaterial NeutralMaterial;
}
