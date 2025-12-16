using UnityEngine;

public class NavPathTester : MonoBehaviour
{
	[SerializeField]
	private Transform target;

	private void OnDrawGizmosSelected()
	{
		if (!(target == null) && BaseNetworkableEx.Is<NavPathTester>(target.GetComponent<NavPathTester>(), out var entAsT) && entAsT.target == null)
		{
			entAsT.target = base.transform;
		}
	}

	public static void GizmosDrawPath(Vector3[] corners, Color color)
	{
		Color color2 = Gizmos.color;
		Gizmos.color = color;
		for (int i = 0; i < corners.Length - 1; i++)
		{
			Gizmos.DrawSphere(corners[i], 0.01f);
			Gizmos.DrawLine(corners[i], corners[i + 1]);
		}
		Gizmos.color = color2;
	}
}
