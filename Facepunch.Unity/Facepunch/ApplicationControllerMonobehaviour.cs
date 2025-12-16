using UnityEngine;

namespace Facepunch;

internal class ApplicationControllerMonobehaviour : MonoBehaviour
{
	public void OnApplicationQuit()
	{
	}

	public void Update()
	{
		Performance.Frame();
		Threading.RunQueuedFunctionsOnMainThread();
	}
}
