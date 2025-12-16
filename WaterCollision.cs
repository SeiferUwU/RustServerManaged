using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UtilityJobs;

public class WaterCollision : MonoBehaviour
{
	private ListDictionary<Collider, List<Collider>> ignoredColliders;

	private HashSet<Collider> waterColliders;

	private WaterVisibilityGrid visibilityGrid;

	public const float IgnoreRadius = 0.01f;

	public WaterVisibilityGrid VisibilityGrid => visibilityGrid;

	public void Setup()
	{
		ignoredColliders = new ListDictionary<Collider, List<Collider>>();
		waterColliders = new HashSet<Collider>();
		if (visibilityGrid != null)
		{
			visibilityGrid.Dispose();
		}
		visibilityGrid = new WaterVisibilityGrid();
	}

	private void OnDestroy()
	{
		visibilityGrid?.Dispose();
	}

	public void Clear()
	{
		if (waterColliders.Count == 0)
		{
			return;
		}
		HashSet<Collider>.Enumerator enumerator = waterColliders.GetEnumerator();
		while (enumerator.MoveNext())
		{
			foreach (Collider key in ignoredColliders.Keys)
			{
				Physics.IgnoreCollision(key, enumerator.Current, ignore: false);
			}
		}
		ignoredColliders.Clear();
	}

	public void Reset(Collider collider)
	{
		if (waterColliders.Count != 0 && (bool)collider)
		{
			HashSet<Collider>.Enumerator enumerator = waterColliders.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Physics.IgnoreCollision(collider, enumerator.Current, ignore: false);
			}
			ignoredColliders.Remove(collider);
		}
	}

	public bool GetIgnore(Vector3 pos, float radius = 0.01f)
	{
		WaterVisibilityGrid waterVisibilityGrid = visibilityGrid;
		if (waterVisibilityGrid != null && !waterVisibilityGrid.Check(pos, radius))
		{
			return false;
		}
		return GamePhysics.CheckSphere<WaterVisibilityTrigger>(pos, radius, 262144, QueryTriggerInteraction.Collide);
	}

	public void GetIgnore(NativeArray<Vector3>.ReadOnly positions, NativeArray<float>.ReadOnly radii, NativeArray<bool> results)
	{
		using (TimeWarning.New("WaterCollision.GetIgnore"))
		{
			FillJob<bool> jobData = new FillJob<bool>
			{
				Values = results,
				Value = false
			};
			IJobExtensions.RunByRef(ref jobData);
			NativeList<int> nativeList = new NativeList<int>(positions.Length, Allocator.TempJob);
			((visibilityGrid == null) ? new GenerateAscSeqListJob
			{
				Values = nativeList,
				Start = 0,
				Step = 1,
				Count = positions.Length
			}.Schedule() : visibilityGrid.Check(positions, radii, nativeList)).Complete();
			if (!nativeList.IsEmpty)
			{
				NativeArray<Vector3> results2 = new NativeArray<Vector3>(nativeList.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				GatherJob<Vector3> jobData2 = new GatherJob<Vector3>
				{
					Results = results2,
					Source = positions,
					Indices = nativeList.AsReadOnly()
				};
				IJobExtensions.RunByRef(ref jobData2);
				NativeArray<float> results3 = new NativeArray<float>(nativeList.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				GatherJob<float> jobData3 = new GatherJob<float>
				{
					Results = results3,
					Source = radii,
					Indices = nativeList.AsReadOnly()
				};
				IJobExtensions.RunByRef(ref jobData3);
				NativeArray<int> values = new NativeArray<int>(nativeList.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				FillJob<int> jobData4 = new FillJob<int>
				{
					Values = values,
					Value = 262144
				};
				IJobExtensions.RunByRef(ref jobData4);
				GamePhysics.CheckSpheres<WaterVisibilityTrigger>(results2.AsReadOnly(), results3.AsReadOnly(), values.AsReadOnly(), results, QueryTriggerInteraction.Collide, 16, GamePhysics.MasksToValidate.None);
				CollectionUtil.ScatterOutInplace(results, nativeList.AsReadOnly(), defValue: false);
				values.Dispose();
				results3.Dispose();
				results2.Dispose();
			}
			nativeList.Dispose();
		}
	}

	public void GetIgnoreIndirect(NativeArray<Vector3>.ReadOnly pos, NativeArray<float>.ReadOnly radii, NativeArray<int>.ReadOnly indices, NativeArray<bool> results)
	{
		using (TimeWarning.New("WaterCollision.GetIgnoreIndirect"))
		{
			FillJob<bool> jobData = new FillJob<bool>
			{
				Values = results,
				Value = false
			};
			IJobExtensions.RunByRef(ref jobData);
			NativeList<int> list = new NativeList<int>(indices.Length, Allocator.TempJob);
			JobHandle jobHandle = default(JobHandle);
			if (visibilityGrid != null)
			{
				jobHandle = visibilityGrid.CheckIndirect(pos, radii, indices, list);
			}
			else
			{
				NativeListEx.CopyFrom(ref list, in indices);
			}
			jobHandle.Complete();
			if (!list.IsEmpty)
			{
				NativeArray<Vector3> results2 = new NativeArray<Vector3>(list.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				GatherJob<Vector3> jobData2 = new GatherJob<Vector3>
				{
					Results = results2,
					Source = pos,
					Indices = list.AsReadOnly()
				};
				IJobExtensions.RunByRef(ref jobData2);
				NativeArray<float> results3 = new NativeArray<float>(list.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				GatherJob<float> jobData3 = new GatherJob<float>
				{
					Results = results3,
					Source = radii,
					Indices = list.AsReadOnly()
				};
				IJobExtensions.RunByRef(ref jobData3);
				NativeArray<int> values = new NativeArray<int>(list.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				FillJob<int> jobData4 = new FillJob<int>
				{
					Values = values,
					Value = 262144
				};
				IJobExtensions.RunByRef(ref jobData4);
				NativeArray<bool> source = new NativeArray<bool>(list.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				GamePhysics.CheckSpheres<WaterVisibilityTrigger>(results2.AsReadOnly(), results3.AsReadOnly(), values.AsReadOnly(), source, QueryTriggerInteraction.Collide, 16, GamePhysics.MasksToValidate.None);
				CollectionUtil.ScatterTo<bool>(source, results, list.AsReadOnly());
				source.Dispose();
				values.Dispose();
				results3.Dispose();
				results2.Dispose();
			}
			list.Dispose();
		}
	}

	public bool GetIgnore(Bounds bounds)
	{
		WaterVisibilityGrid waterVisibilityGrid = visibilityGrid;
		if (waterVisibilityGrid != null && !waterVisibilityGrid.Check(bounds))
		{
			return false;
		}
		return GamePhysics.CheckBounds<WaterVisibilityTrigger>(bounds, 262144, QueryTriggerInteraction.Collide);
	}

	public bool GetIgnore(Vector3 start, Vector3 end, float radius)
	{
		WaterVisibilityGrid waterVisibilityGrid = visibilityGrid;
		if (waterVisibilityGrid != null && !waterVisibilityGrid.Check(start, end, radius))
		{
			return false;
		}
		return GamePhysics.CheckCapsule<WaterVisibilityTrigger>(start, end, radius, 262144, QueryTriggerInteraction.Collide);
	}

	public void GetIgnoreIndirect(NativeArray<Vector3>.ReadOnly starts, NativeArray<Vector3>.ReadOnly ends, NativeArray<float>.ReadOnly radii, NativeArray<int>.ReadOnly indices, NativeArray<bool> results)
	{
		using (TimeWarning.New("WaterCollision.GetIgnoreIndirect"))
		{
			FillJob<bool> jobData = new FillJob<bool>
			{
				Values = results,
				Value = false
			};
			IJobExtensions.RunByRef(ref jobData);
			NativeList<int> list = new NativeList<int>(indices.Length, Allocator.TempJob);
			JobHandle jobHandle = default(JobHandle);
			if (visibilityGrid != null)
			{
				jobHandle = visibilityGrid.CheckIndirect(starts, ends, radii, indices, list);
			}
			else
			{
				NativeListEx.CopyFrom(ref list, in indices);
			}
			jobHandle.Complete();
			if (!list.IsEmpty)
			{
				NativeArray<Vector3> results2 = new NativeArray<Vector3>(list.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				GatherJob<Vector3> jobData2 = new GatherJob<Vector3>
				{
					Results = results2,
					Source = starts,
					Indices = list.AsReadOnly()
				};
				IJobExtensions.RunByRef(ref jobData2);
				NativeArray<Vector3> results3 = new NativeArray<Vector3>(list.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				GatherJob<Vector3> jobData3 = new GatherJob<Vector3>
				{
					Results = results3,
					Source = ends,
					Indices = list.AsReadOnly()
				};
				IJobExtensions.RunByRef(ref jobData3);
				NativeArray<float> results4 = new NativeArray<float>(list.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				GatherJob<float> jobData4 = new GatherJob<float>
				{
					Results = results4,
					Source = radii,
					Indices = list.AsReadOnly()
				};
				IJobExtensions.RunByRef(ref jobData4);
				NativeArray<int> values = new NativeArray<int>(list.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				FillJob<int> jobData5 = new FillJob<int>
				{
					Values = values,
					Value = 262144
				};
				IJobExtensions.RunByRef(ref jobData5);
				NativeArray<bool> source = new NativeArray<bool>(list.Length, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
				GamePhysics.CheckCapsules<WaterVisibilityTrigger>(results2.AsReadOnly(), results3.AsReadOnly(), results4.AsReadOnly(), values.AsReadOnly(), source, QueryTriggerInteraction.Collide, 16, GamePhysics.MasksToValidate.None);
				CollectionUtil.ScatterTo<bool>(source, results, list.AsReadOnly());
				source.Dispose();
				values.Dispose();
				results4.Dispose();
				results3.Dispose();
				results2.Dispose();
			}
			list.Dispose();
		}
	}

	public bool GetIgnore(RaycastHit hit)
	{
		if (waterColliders.Contains(hit.collider))
		{
			return GetIgnore(hit.point);
		}
		return false;
	}

	public bool GetIgnore(Collider collider)
	{
		if (waterColliders.Count == 0 || !collider)
		{
			return false;
		}
		return ignoredColliders.Contains(collider);
	}

	public void SetIgnore(Collider collider, Collider trigger, bool ignore = true)
	{
		if (waterColliders.Count == 0 || !collider)
		{
			return;
		}
		if (!GetIgnore(collider))
		{
			if (ignore)
			{
				List<Collider> val = new List<Collider> { trigger };
				HashSet<Collider>.Enumerator enumerator = waterColliders.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Physics.IgnoreCollision(collider, enumerator.Current, ignore: true);
				}
				ignoredColliders.Add(collider, val);
			}
			return;
		}
		List<Collider> list = ignoredColliders[collider];
		if (ignore)
		{
			if (!list.Contains(trigger))
			{
				list.Add(trigger);
			}
		}
		else if (list.Contains(trigger))
		{
			list.Remove(trigger);
		}
	}

	protected void LateUpdate()
	{
		if (ignoredColliders == null)
		{
			return;
		}
		for (int i = 0; i < ignoredColliders.Count; i++)
		{
			KeyValuePair<Collider, List<Collider>> byIndex = ignoredColliders.GetByIndex(i);
			Collider key = byIndex.Key;
			List<Collider> value = byIndex.Value;
			if (key == null)
			{
				ignoredColliders.RemoveAt(i--);
			}
			else if (value.Count == 0)
			{
				HashSet<Collider>.Enumerator enumerator = waterColliders.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Physics.IgnoreCollision(key, enumerator.Current, ignore: false);
				}
				ignoredColliders.RemoveAt(i--);
			}
		}
	}
}
