public struct PreProcessPrefabOptions
{
	public static readonly PreProcessPrefabOptions Default;

	public static readonly PreProcessPrefabOptions Default_NoResetPosition;

	public static readonly PreProcessPrefabOptions AssetSceneBundling;

	public static readonly PreProcessPrefabOptions AssetSceneRuntime;

	public bool ResetLocalTransform;

	public bool StripComponents;

	public bool StripEmptyChildren;

	public bool PreProcess;

	public bool PostProcess;

	public bool UpdateMeshCooking;

	static PreProcessPrefabOptions()
	{
		Default = new PreProcessPrefabOptions
		{
			ResetLocalTransform = true,
			StripComponents = true,
			StripEmptyChildren = true,
			PreProcess = true,
			PostProcess = true,
			UpdateMeshCooking = true
		};
		AssetSceneBundling = new PreProcessPrefabOptions
		{
			ResetLocalTransform = true,
			StripComponents = true,
			StripEmptyChildren = true,
			PreProcess = true,
			PostProcess = false,
			UpdateMeshCooking = true
		};
		AssetSceneRuntime = new PreProcessPrefabOptions
		{
			ResetLocalTransform = false,
			StripComponents = false,
			StripEmptyChildren = false,
			PreProcess = true,
			PostProcess = true,
			UpdateMeshCooking = false
		};
		Default_NoResetPosition = Default;
		Default_NoResetPosition.ResetLocalTransform = false;
	}
}
