using UnityEngine;
using UnityEngine.AI;

public static class NPCOverwatchSpot
{
	public static (Vector3 loc, Vector3 dir)? Find(Vector3[] corners)
	{
		if (corners.Length < 3)
		{
			return null;
		}
		for (int num = corners.Length - 1; num >= 2; num--)
		{
			Vector3 vector = corners[num];
			Vector3 vector2 = corners[num - 1];
			Vector3 vector3 = corners[num - 2];
			Gizmos.color = Color.red;
			Vector3 vector4 = (vector - vector2).NormalizeXZ();
			Vector3 vector5 = (vector3 - vector2).NormalizeXZ();
			Vector3 vector6 = -(vector4 + vector5).NormalizeXZ();
			Gizmos.DrawLine(vector2, vector2 + vector6);
			Gizmos.color = Color.blue;
			Vector3 vector7 = vector6 * 0.01f;
			vector += vector7;
			vector2 += vector7;
			Vector3 vector8 = (vector2 - vector).NormalizeXZ() * 100f;
			if (NavMesh.Raycast(vector, vector + vector8, out var hit, -1))
			{
				Gizmos.DrawLine(vector, vector + (hit.position - vector).normalized * 100f);
				if (hit.distance >= 7f)
				{
					Vector3 vector9 = corners[^1];
					Vector3 position = hit.position;
					RaycastHit hitInfo;
					bool flag = Physics.Linecast(vector9 + 1.7f * Vector3.up, position + 1.7f * Vector3.up, out hitInfo, 1218652417);
					Gizmos.color = Color.red;
					Gizmos.DrawLine(vector9 + 1.7f * Vector3.up, position + 1.7f * Vector3.up);
					if (flag)
					{
						RaycastHit hitInfo2;
						bool flag2 = Physics.Linecast(vector9 + 0.2f * Vector3.up, position + 0.2f * Vector3.up, out hitInfo2, 1218652417);
						Gizmos.DrawLine(vector9 + 0.2f * Vector3.up, position + 0.2f * Vector3.up);
						if (flag2)
						{
							Gizmos.color = Color.blue;
							Gizmos.DrawWireSphere(hit.position, 0.1f);
							return (hit.position, (vector - hit.position).normalized);
						}
					}
				}
			}
		}
		return null;
	}
}
