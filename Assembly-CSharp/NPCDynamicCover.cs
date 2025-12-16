using System;
using Rust.Ai.Gen2;
using UnityEngine;
using UnityEngine.AI;

public static class NPCDynamicCover
{
	public static bool Find(Vector3 pos, Vector3 enemyPos, out NavMeshPath path, out Vector3 coverDir, float radius = 10f, int itemsPerRing = 8, float offset = 0f)
	{
		path = null;
		coverDir = default(Vector3);
		float num = Vector3.SignedAngle(enemyPos - pos, Vector3.forward, Vector3.up) * (MathF.PI / 180f) + offset;
		for (int i = 0; i < itemsPerRing; i++)
		{
			float f = MathF.PI * -2f * (float)i / (float)itemsPerRing + num;
			Vector3 vector = pos + new Vector3(Mathf.Cos(f), 0f, Mathf.Sin(f)) * radius;
			Gizmos.color = Color.white;
			Gizmos.DrawLine(pos, vector);
			if (!NavMesh.SamplePosition(vector, out var hit, 3f, -1))
			{
				continue;
			}
			Gizmos.DrawLine(vector, hit.position);
			vector = hit.position;
			Vector3 vector2 = Vector3.Cross((enemyPos - vector).normalized, Vector3.up) * 0.5f;
			if (IsPositionVisibleFrom(vector + vector2, enemyPos) || IsPositionVisibleFrom(vector - vector2, enemyPos))
			{
				continue;
			}
			if (path == null)
			{
				path = new NavMeshPath();
			}
			if (NavMesh.CalculatePath(pos, vector, -1, path) && path.status == NavMeshPathStatus.PathComplete)
			{
				Gizmos.color = Color.black;
				NavPathTester.GizmosDrawPath(path.corners, Color.black);
				if (!(NavMeshPathEx.GetPathLength(path) > radius * 2f))
				{
					Gizmos.color = Color.yellow;
					Gizmos.DrawSphere(vector, 0.5f);
					coverDir = (enemyPos - vector).normalized;
					return true;
				}
			}
		}
		path = null;
		return false;
	}

	private static bool IsPositionVisibleFrom(Vector3 pos, Vector3 enemyPos)
	{
		Vector3 vector = Vector3.up * 1.7f;
		if (Physics.Linecast(pos + vector, enemyPos + vector, out var hitInfo, 1218652417))
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(pos + vector, hitInfo.point);
			Gizmos.DrawSphere(hitInfo.point, 0.1f);
			Gizmos.color = Color.red;
			Gizmos.DrawLine(hitInfo.point, enemyPos + vector);
			return false;
		}
		return true;
	}
}
