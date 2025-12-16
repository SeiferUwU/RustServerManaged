using System;
using System.Collections.Generic;
using System.Linq;

public static class PooledArrayExtensions
{
	public static PooledArray<T> ToPooledArray<T>(this T[] array)
	{
		if (array == null)
		{
			throw new ArgumentNullException("array");
		}
		PooledArray<T> pooledArray = new PooledArray<T>(array.Length);
		Array.Copy(array, pooledArray.Array, array.Length);
		return pooledArray;
	}

	public static PooledArray<T> ToPooledArray<T>(this List<T> list)
	{
		if (list == null)
		{
			throw new ArgumentNullException("list");
		}
		PooledArray<T> pooledArray = new PooledArray<T>(list.Count);
		list.CopyTo(pooledArray.Array);
		return pooledArray;
	}

	public static PooledArray<T> ToPooledArray<T>(this IEnumerable<T> enumerable)
	{
		if (enumerable == null)
		{
			throw new ArgumentNullException("enumerable");
		}
		PooledArray<T> pooledArray = new PooledArray<T>(enumerable.Count());
		int num = 0;
		foreach (T item in enumerable)
		{
			if (num >= pooledArray.Array.Length)
			{
				throw new InvalidOperationException("The enumerable contains more items than the allocated array size.");
			}
			pooledArray.Array[num++] = item;
		}
		return pooledArray;
	}
}
