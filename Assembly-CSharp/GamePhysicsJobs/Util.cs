using Unity.Collections;
using UnityEngine;

namespace GamePhysicsJobs;

internal static class Util
{
	public static int FindFreeSlot(int rayInd, in NativeArray<RaycastHit> hits, int maxHitsPerRay, out int endInd)
	{
		int i = rayInd * maxHitsPerRay;
		for (endInd = i + maxHitsPerRay; i < endInd && hits[i].colliderInstanceID != 0; i++)
		{
		}
		return i;
	}
}
