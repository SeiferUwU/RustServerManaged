using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

namespace Facepunch;

public static class Pool
{
	public interface IPooled
	{
		void EnterPool();

		void LeavePool();
	}

	public interface IPoolCollection
	{
		long ItemsCapacity { get; }

		long ItemsInStack { get; }

		long ItemsInUse { get; }

		long ItemsCreated { get; }

		long ItemsTaken { get; }

		long ItemsSpilled { get; }

		long MaxItemsInUse { get; }

		void Reset();

		void ResetMaxUsageCounter();

		void Add(object obj);
	}

	public class PoolCollection<T> : IPoolCollection where T : class, new()
	{
		private static readonly object collectionLock = new object();

		private BufferList<T> buffer;

		public long ItemsCapacity { get; private set; }

		public long ItemsInStack { get; private set; }

		public long ItemsInUse { get; private set; }

		public long ItemsCreated { get; private set; }

		public long ItemsTaken { get; private set; }

		public long ItemsSpilled { get; private set; }

		public long MaxItemsInUse { get; private set; }

		public PoolCollection()
		{
			Resize(512);
		}

		public void Reset()
		{
			Resize((int)ItemsCapacity);
		}

		public void ResetMaxUsageCounter()
		{
			lock (collectionLock)
			{
				MaxItemsInUse = ItemsInUse;
			}
		}

		public void Resize(int size)
		{
			lock (collectionLock)
			{
				buffer = new BufferList<T>(size);
				ItemsCapacity = size;
				ItemsInStack = 0L;
				ItemsInUse = 0L;
				ItemsCreated = 0L;
				ItemsTaken = 0L;
				ItemsSpilled = 0L;
				MaxItemsInUse = 0L;
			}
		}

		public void Add(T obj)
		{
			if (obj is IPooled pooled)
			{
				pooled.EnterPool();
			}
			lock (collectionLock)
			{
				ItemsInUse--;
				if (ItemsInStack < ItemsCapacity)
				{
					buffer.Push(obj);
					ItemsInStack++;
				}
				else
				{
					ItemsSpilled++;
				}
			}
		}

		public T Take()
		{
			T val;
			lock (collectionLock)
			{
				ItemsInUse++;
				MaxItemsInUse = Math.Max(ItemsInUse, MaxItemsInUse);
				if (ItemsInStack > 0)
				{
					val = buffer.Pop();
					ItemsInStack--;
					ItemsTaken++;
				}
				else
				{
					val = new T();
					ItemsCreated++;
				}
			}
			if (val is IPooled pooled)
			{
				pooled.LeavePool();
			}
			return val;
		}

		public void Fill()
		{
			long num = ItemsCapacity - ItemsInStack;
			for (int i = 0; i < num; i++)
			{
				T val = new T();
				if (val is IPooled pooled)
				{
					pooled.EnterPool();
				}
				lock (collectionLock)
				{
					buffer.Push(val);
					ItemsInStack++;
				}
			}
		}

		void IPoolCollection.Add(object obj)
		{
			Add((T)obj);
		}
	}

	public static ConcurrentDictionary<Type, IPoolCollection> Directory = new ConcurrentDictionary<Type, IPoolCollection>();

	[Obsolete("FreeList got superseeded by Free<T>(where T : IPooled) and FreeUnmanaged<T>, use those instead")]
	public static void FreeList<T>(ref List<T> obj)
	{
		FreeUnmanaged(ref obj);
	}

	[Obsolete("FreeListAndItems got superseeded by Free<T>(list, true), use that instead")]
	public static void FreeListAndItems<T>(ref List<T> items) where T : class, IPooled, new()
	{
		Free(ref items, freeElements: true);
	}

	[Obsolete("ClearList is deprecated and about to be removed")]
	public static void ClearList<T>(ICollection<T> items) where T : class, IPooled, new()
	{
		if (items == null)
		{
			throw new ArgumentNullException();
		}
		foreach (T item in items)
		{
			if (item != null)
			{
				T obj = item;
				Free(ref obj);
			}
		}
		items.Clear();
	}

	[Obsolete("FreeMemoryStream is superseeded by FreeUnmanaged, use that instead")]
	public static void FreeMemoryStream(ref MemoryStream obj)
	{
		FreeUnmanaged(ref obj);
	}

	public static void Free<T>(ref T obj) where T : class, IPooled, new()
	{
		if (obj == null)
		{
			throw new ArgumentNullException();
		}
		FreeInternal(ref obj);
	}

	public static void Free<T>(ref List<T> obj, bool freeElements = false) where T : class, IPooled, new()
	{
		if (obj == null)
		{
			throw new ArgumentNullException();
		}
		if (freeElements)
		{
			foreach (T item in obj)
			{
				if (item != null)
				{
					T obj2 = item;
					Free(ref obj2);
				}
			}
		}
		obj.Clear();
		FreeInternal(ref obj);
	}

	public static void Free<T>(ref HashSet<T> obj, bool freeElements = false) where T : class, IPooled, new()
	{
		if (obj == null)
		{
			throw new ArgumentNullException();
		}
		if (freeElements)
		{
			foreach (T item in obj)
			{
				if (item != null)
				{
					T obj2 = item;
					Free(ref obj2);
				}
			}
		}
		obj.Clear();
		FreeInternal(ref obj);
	}

	public static void Free<TKey, TVal>(ref Dictionary<TKey, TVal> dict, bool freeElements = false) where TVal : class, IPooled, new()
	{
		if (dict == null)
		{
			throw new ArgumentNullException();
		}
		if (freeElements)
		{
			foreach (KeyValuePair<TKey, TVal> item in dict)
			{
				if (item.Value != null)
				{
					TVal obj = item.Value;
					Free(ref obj);
				}
			}
		}
		dict.Clear();
		FreeInternal(ref dict);
	}

	public static void Free<T>(ref BufferList<T> obj, bool freeElements = false) where T : class, IPooled, new()
	{
		if (obj == null)
		{
			throw new ArgumentNullException();
		}
		if (freeElements)
		{
			foreach (T item in obj)
			{
				if (item != null)
				{
					T obj2 = item;
					Free(ref obj2);
				}
			}
		}
		obj.Clear();
		FreeInternal(ref obj);
	}

	public static void Free<TKey, TVal>(ref ListDictionary<TKey, TVal> dict, bool freeElements = false) where TVal : class, IPooled, new()
	{
		if (dict == null)
		{
			throw new ArgumentNullException();
		}
		if (freeElements)
		{
			for (int i = 0; i < dict.Values.Count; i++)
			{
				TVal obj = dict.Values[i];
				if (obj != null)
				{
					Free(ref obj);
				}
			}
		}
		dict.Clear();
		FreeInternal(ref dict);
	}

	public static void Free<T>(ref Queue<T> obj, bool freeElements = false) where T : class, IPooled, new()
	{
		if (obj == null)
		{
			throw new ArgumentNullException();
		}
		if (freeElements)
		{
			foreach (T item in obj)
			{
				if (item != null)
				{
					T obj2 = item;
					Free(ref obj2);
				}
			}
		}
		obj.Clear();
		FreeInternal(ref obj);
	}

	public static void Free<T>(ref ListHashSet<T> obj, bool freeElements = false) where T : class, IPooled, new()
	{
		if (obj == null)
		{
			throw new ArgumentNullException();
		}
		if (freeElements)
		{
			foreach (T item in obj)
			{
				if (item != null)
				{
					T obj2 = item;
					Free(ref obj2);
				}
			}
		}
		obj.Clear();
		FreeInternal(ref obj);
	}

	public static void FreeUnsafe<T>(ref T obj) where T : class, new()
	{
		if (obj == null)
		{
			throw new ArgumentNullException();
		}
		FreeInternal(ref obj);
	}

	public static void FreeUnmanaged(ref MemoryStream obj)
	{
		if (obj == null)
		{
			throw new ArgumentNullException();
		}
		obj.SetLength(0L);
		FreeInternal(ref obj);
	}

	public static void FreeUnmanaged(ref StringBuilder obj)
	{
		if (obj == null)
		{
			throw new ArgumentNullException();
		}
		obj.Clear();
		FreeInternal(ref obj);
	}

	public static void FreeUnmanaged(ref Stopwatch obj)
	{
		if (obj == null)
		{
			throw new ArgumentNullException();
		}
		obj.Reset();
		FreeInternal(ref obj);
	}

	public static void FreeUnmanaged<T>(ref List<T> obj)
	{
		if (obj == null)
		{
			throw new ArgumentNullException();
		}
		obj.Clear();
		FreeInternal(ref obj);
	}

	public static void FreeUnmanaged<T>(ref HashSet<T> obj)
	{
		if (obj == null)
		{
			throw new ArgumentNullException();
		}
		obj.Clear();
		FreeInternal(ref obj);
	}

	public static void FreeUnmanaged<TKey, TVal>(ref Dictionary<TKey, TVal> dict)
	{
		if (dict == null)
		{
			throw new ArgumentNullException();
		}
		dict.Clear();
		FreeInternal(ref dict);
	}

	public static void FreeUnmanaged<T>(ref BufferList<T> obj)
	{
		if (obj == null)
		{
			throw new ArgumentNullException();
		}
		obj.Clear();
		FreeInternal(ref obj);
	}

	public static void FreeUnmanaged<TKey, TVal>(ref ListDictionary<TKey, TVal> dict)
	{
		if (dict == null)
		{
			throw new ArgumentNullException();
		}
		dict.Clear();
		FreeInternal(ref dict);
	}

	public static void FreeUnmanaged<T>(ref Queue<T> obj)
	{
		if (obj == null)
		{
			throw new ArgumentNullException();
		}
		obj.Clear();
		FreeInternal(ref obj);
	}

	public static void FreeUnmanaged<T>(ref ListHashSet<T> obj)
	{
		if (obj == null)
		{
			throw new ArgumentNullException();
		}
		obj.Clear();
		FreeInternal(ref obj);
	}

	private static void FreeInternal<T>(ref T obj) where T : class, new()
	{
		FindCollection<T>().Add(obj);
		obj = null;
	}

	public static T Get<T>() where T : class, new()
	{
		return FindCollection<T>().Take();
	}

	[Obsolete("GetList<T> is superseeded by Get<List<T>> and will be removed soon.")]
	public static List<T> GetList<T>()
	{
		List<T> list = Get<List<T>>();
		list.Clear();
		return list;
	}

	public static void ResizeBuffer<T>(int size) where T : class, new()
	{
		FindCollection<T>().Resize(size);
	}

	public static void FillBuffer<T>() where T : class, new()
	{
		FindCollection<T>().Fill();
	}

	public static PoolCollection<T> FindCollection<T>() where T : class, new()
	{
		return Pool<T>.Collection;
	}

	public static void Clear(string filter = null)
	{
		if (string.IsNullOrEmpty(filter))
		{
			foreach (KeyValuePair<Type, IPoolCollection> item in Directory)
			{
				item.Value.Reset();
			}
			return;
		}
		foreach (KeyValuePair<Type, IPoolCollection> item2 in Directory)
		{
			if (item2.Key.FullName.Contains(filter, CompareOptions.IgnoreCase))
			{
				item2.Value.Reset();
			}
		}
	}

	private static bool Contains(this string haystack, string needle, CompareOptions options)
	{
		return CultureInfo.InvariantCulture.CompareInfo.IndexOf(haystack, needle, options) >= 0;
	}
}
internal static class Pool<T> where T : class, new()
{
	public static Pool.PoolCollection<T> Collection;

	static Pool()
	{
		Collection = new Pool.PoolCollection<T>();
		Pool.Directory[typeof(T)] = Collection;
	}
}
