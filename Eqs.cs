using System;
using System.Collections.Generic;
using UnityEngine;

public static class Eqs
{
	public sealed class PooledScoreList : BasePooledList<(Vector3 pos, float score), PooledScoreList>
	{
		public void SortByScoreDesc(BaseEntity baseEntity = null)
		{
			using (TimeWarning.New("SortByScoreDesc"))
			{
				Sort(((Vector3 pos, float score) a, (Vector3 pos, float score) b) => b.score.CompareTo(a.score));
			}
		}

		public void Reorder(List<Vector3> positions)
		{
			using (TimeWarning.New("Reorder"))
			{
				for (int i = 0; i < positions.Count; i++)
				{
					positions[i] = base[i].pos;
				}
			}
		}
	}

	public static void SamplePositionsInDonutShape(Vector3 center, List<Vector3> sampledPositions, float radius = 10f, int numRings = 1, int itemsPerRing = 8)
	{
		using (TimeWarning.New("SamplePositionsInDonutShape"))
		{
			for (int i = 0; i < itemsPerRing; i++)
			{
				float f = MathF.PI * 2f * (float)i / (float)itemsPerRing;
				Vector3 item = center + new Vector3(Mathf.Cos(f), 0f, Mathf.Sin(f)) * radius;
				sampledPositions.Add(item);
			}
		}
	}

	public static void SamplePositionsInMultiDonutShape(Vector3 center, List<Vector3> sampledPositions, float outerRadius = 10f, float innerRadius = 10f, int numRings = 1, int itemsPerRing = 8)
	{
		using (TimeWarning.New("SamplePositionsInMultiDonutShape"))
		{
			for (int i = 0; i < numRings; i++)
			{
				float num = ((numRings != 1) ? Mathf.Lerp(innerRadius, outerRadius, (float)i / (float)(numRings - 1)) : outerRadius);
				for (int j = 0; j < itemsPerRing; j++)
				{
					float num2 = (float)i * MathF.PI / (float)numRings;
					float f = MathF.PI * 2f * (float)j / (float)itemsPerRing + num2;
					Vector3 item = center + new Vector3(Mathf.Cos(f), 0f, Mathf.Sin(f)) * num;
					sampledPositions.Add(item);
				}
			}
		}
	}
}
