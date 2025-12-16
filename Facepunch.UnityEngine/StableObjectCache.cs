#define UNITY_ASSERTIONS
using System;
using System.Collections;
using System.Collections.Generic;
using Facepunch;
using UnityEngine;
using UnityEngine.Jobs;

public class StableObjectCache<TObject> : StableEntityCache where TObject : MonoBehaviour, IStableIndex
{
	public struct ValidEnumerator : IEnumerator<TObject>, IEnumerator, IDisposable
	{
		private StableObjectCache<TObject> cache;

		private int index;

		private int found;

		public TObject Current => cache._objects[index];

		TObject IEnumerator<TObject>.Current => Current;

		object IEnumerator.Current => Current;

		public ValidEnumerator(StableObjectCache<TObject> cache)
		{
			this.cache = cache;
			index = -1;
			found = 0;
		}

		public void Dispose()
		{
			cache = null;
		}

		void IDisposable.Dispose()
		{
			Dispose();
		}

		public bool MoveNext()
		{
			if (found >= cache._count || index >= cache._objects.Length)
			{
				return false;
			}
			while (++index < cache._objects.Length && cache._objects[index] == null)
			{
			}
			found++;
			return index < cache._objects.Length;
		}

		bool IEnumerator.MoveNext()
		{
			return MoveNext();
		}

		public void Reset()
		{
			index = -1;
			found = 0;
		}

		void IEnumerator.Reset()
		{
			Reset();
		}
	}

	public struct ValidView : IEnumerable<TObject>, IEnumerable
	{
		private StableObjectCache<TObject> cache;

		public ValidView(StableObjectCache<TObject> cache)
		{
			this.cache = cache;
		}

		public ValidEnumerator GetEnumerator()
		{
			return new ValidEnumerator(cache);
		}

		IEnumerator<TObject> IEnumerable<TObject>.GetEnumerator()
		{
			return GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

	private TObject[] _objects;

	private int _count;

	private int[] nextIndices;

	private int firstFreeIndex;

	private int lastFreeIndex;

	private const int Occupied = -1;

	private BufferList<int> changes = new BufferList<int>();

	protected TObject[] Objects => _objects;

	public int Count => _count;

	public StableObjectCache(int initialCapacity)
	{
		_objects = new TObject[initialCapacity];
		nextIndices = new int[initialCapacity];
		SetupFreeList(0, initialCapacity);
	}

	public void UpdateTransformAccessArray(TransformAccessArray accessArray)
	{
		using (TimeWarning.New("TransformAccessRefresh"))
		{
			if (accessArray.length < _objects.Length)
			{
				accessArray.capacity = _objects.Length;
				for (int i = accessArray.length; i < _objects.Length; i++)
				{
					accessArray.Add(null);
				}
			}
			BufferList<int> obj = Pool.Get<BufferList<int>>();
			GetDirtyIndices(obj);
			for (int j = 0; j < obj.Count; j++)
			{
				int num = obj[j];
				TObject val = _objects[num];
				if ((bool)val)
				{
					accessArray[num] = val.transform;
				}
				else
				{
					accessArray[num] = null;
				}
			}
			ResetDirtyTracking();
			Pool.FreeUnmanaged(ref obj);
		}
	}

	public void Add(TObject obj)
	{
		Debug.Assert(obj != null, "Entity is dead!");
		Debug.Assert(obj.StableIndex == -1, "Entity is already in the cache or property not correctly initialized to StableEntityCache.NotInCache");
		bool num = firstFreeIndex == lastFreeIndex;
		int num2 = firstFreeIndex;
		_objects[num2] = obj;
		obj.StableIndex = num2;
		firstFreeIndex = nextIndices[num2];
		nextIndices[num2] = -1;
		if (num)
		{
			Grow();
		}
		_count++;
		changes.Add(num2);
	}

	public void Remove(TObject obj)
	{
		Debug.Assert(obj.StableIndex != -1, "Entity is not in the cache!");
		int stableIndex = obj.StableIndex;
		obj.StableIndex = -1;
		_objects[stableIndex] = null;
		if (stableIndex < firstFreeIndex)
		{
			nextIndices[stableIndex] = firstFreeIndex;
			firstFreeIndex = stableIndex;
		}
		else
		{
			nextIndices[lastFreeIndex] = stableIndex;
			lastFreeIndex = stableIndex;
		}
		_count--;
		changes.Add(stableIndex);
	}

	public void Clear()
	{
		if (_count > 0)
		{
			for (int i = 0; i < _objects.Length; i++)
			{
				TObject val = _objects[i];
				if ((object)val != null)
				{
					val.StableIndex = -1;
					_objects[i] = null;
					changes.Add(i);
				}
			}
			_count = 0;
		}
		SetupFreeList(0, _objects.Length);
	}

	public void ResetDirtyTracking()
	{
		changes.Clear();
	}

	public void GetDirtyIndices(BufferList<int> indices)
	{
		indices.Clear();
		HashSet<int> obj = Pool.Get<HashSet<int>>();
		foreach (int change in changes)
		{
			if (obj.Add(change))
			{
				indices.Add(change);
			}
		}
		Pool.FreeUnmanaged(ref obj);
	}

	private void SetupFreeList(int from, int to)
	{
		for (int i = from; i < to; i++)
		{
			nextIndices[i] = i + 1;
		}
		firstFreeIndex = from;
		lastFreeIndex = to - 1;
	}

	private void Grow()
	{
		int num = _objects.Length;
		int num2 = num * 2;
		Array.Resize(ref _objects, num2);
		Array.Resize(ref nextIndices, num2);
		SetupFreeList(num, num2);
	}
}
