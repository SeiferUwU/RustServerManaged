using UnityEngine;
using UnityEngine.AI;

public static class NPCFlankSpot
{
	public readonly struct FlankRoute
	{
		public readonly Vector3 FlankPoint;

		public readonly NavMeshPath ToFlankPoint;

		public readonly NavMeshPath FromFlankPointToEnemy;

		public FlankRoute(Vector3 flankPoint, NavMeshPath toFlankPoint, NavMeshPath fromFlankPointToEnemy)
		{
			FlankPoint = flankPoint;
			ToFlankPoint = toFlankPoint;
			FromFlankPointToEnemy = fromFlankPointToEnemy;
		}
	}

	public static bool Find(Vector3[] pathToEnemy, float pathDistance, out FlankRoute flank)
	{
		flank = default(FlankRoute);
		if (!FindMiddlePoint(pathToEnemy, pathDistance, out var middlePoint, out var dir))
		{
			return false;
		}
		Gizmos.color = Color.white;
		Gizmos.DrawSphere(middlePoint, 0.1f);
		Vector3 vector = dir * pathDistance * 0.5f;
		vector.y = 0f;
		if (TryBuildFlankPath(pathToEnemy, middlePoint, middlePoint + Quaternion.AngleAxis(90f, Vector3.up) * vector, out flank))
		{
			return true;
		}
		if (TryBuildFlankPath(pathToEnemy, middlePoint, middlePoint + Quaternion.AngleAxis(-90f, Vector3.up) * vector, out flank))
		{
			return true;
		}
		return false;
	}

	private static bool TryBuildFlankPath(Vector3[] pathToEnemy, Vector3 pathMiddle, Vector3 approximateFlankPoint, out FlankRoute flank)
	{
		Gizmos.color = Color.white;
		Gizmos.DrawLine(pathMiddle, approximateFlankPoint);
		flank = default(FlankRoute);
		if (!NavMesh.SamplePosition(approximateFlankPoint, out var hit, 10f, -1))
		{
			return false;
		}
		Gizmos.DrawLine(approximateFlankPoint, hit.position);
		NavMeshPath navMeshPath = new NavMeshPath();
		if (!NavMesh.CalculatePath(pathMiddle, hit.position, -1, navMeshPath))
		{
			return false;
		}
		Vector3 vector = navMeshPath.corners[^1];
		NavMeshPath navMeshPath2 = new NavMeshPath();
		if (!NavMesh.CalculatePath(pathToEnemy[0], vector, -1, navMeshPath2))
		{
			return false;
		}
		Vector3[] corners = navMeshPath2.corners;
		NavMeshPath navMeshPath3 = new NavMeshPath();
		if (!NavMesh.CalculatePath(vector, pathToEnemy[^1], -1, navMeshPath3))
		{
			return false;
		}
		Vector3[] corners2 = navMeshPath3.corners;
		if (!ArePathsDifferent(pathToEnemy, corners2))
		{
			NavPathTester.GizmosDrawPath(corners, Color.grey);
			NavPathTester.GizmosDrawPath(corners2, Color.yellow);
			return false;
		}
		if (!ArePathsDifferent(pathToEnemy, corners))
		{
			NavPathTester.GizmosDrawPath(corners, Color.yellow);
			NavPathTester.GizmosDrawPath(corners2, Color.grey);
			return false;
		}
		if (!ArePathsDifferent(corners, corners2))
		{
			NavPathTester.GizmosDrawPath(corners, Color.yellow);
			NavPathTester.GizmosDrawPath(corners2, Color.grey);
			return false;
		}
		if (Vector3.Angle(pathToEnemy[^1] - pathToEnemy[^2], corners2[^1] - corners2[^2]) < 30f)
		{
			NavPathTester.GizmosDrawPath(corners, Color.grey);
			NavPathTester.GizmosDrawPath(corners2, Color.red);
			return false;
		}
		flank = new FlankRoute(vector, navMeshPath2, navMeshPath3);
		return true;
	}

	private static bool ArePathsDifferent(Vector3[] path1, Vector3[] path2, float minRatio = 0.25f)
	{
		if (path1.Length < 3 || path2.Length < 3)
		{
			if (!(path1[0] != path2[0]))
			{
				return path1[^1] != path2[^1];
			}
			return true;
		}
		int num = 0;
		for (int i = 1; i < path1.Length - 1; i++)
		{
			for (int j = 1; j < path2.Length - 1; j++)
			{
				if (path1[i] == path2[j])
				{
					num++;
					break;
				}
			}
		}
		int num2 = Mathf.Min(path1.Length - 2, path2.Length - 2);
		return (float)num / (float)num2 <= minRatio;
	}

	private static bool FindMiddlePoint(Vector3[] corners, float pathLength, out Vector3 middlePoint, out Vector3 dir)
	{
		float num = pathLength * 0.5f;
		float num2 = 0f;
		for (int i = 0; i < corners.Length - 1; i++)
		{
			Vector3 vector = corners[i];
			Vector3 vector2 = corners[i + 1];
			float num3 = Vector3.Distance(vector, vector2);
			if (num2 + num3 >= num)
			{
				float t = (num - num2) / num3;
				middlePoint = Vector3.Lerp(vector, vector2, t);
				dir = (vector2 - vector).normalized;
				return true;
			}
			num2 += num3;
		}
		middlePoint = default(Vector3);
		dir = default(Vector3);
		return false;
	}
}
