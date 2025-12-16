using UnityEngine;
using UnityEngine.Jobs;

namespace Facepunch;

public static class TransformAccessArrayEx
{
	public static bool RemoveInstance(this ref TransformAccessArray array, Transform transform)
	{
		int num = -1;
		for (int i = 0; i < array.length; i++)
		{
			if (transform == array[i].transform)
			{
				num = i;
				break;
			}
		}
		bool num2 = num >= 0;
		if (num2)
		{
			array.RemoveAtSwapBack(num);
		}
		return num2;
	}

	public static int GetInstanceIndex(this in TransformAccessArray array, Transform transform)
	{
		for (int i = 0; i < array.length; i++)
		{
			if (transform == array[i].transform)
			{
				return i;
			}
		}
		return -1;
	}

	public static int ClearNullEntries(this ref TransformAccessArray array)
	{
		int num = 0;
		for (int num2 = array.length - 1; num2 >= 0; num2--)
		{
			if (array[num2] == null)
			{
				array.RemoveAtSwapBack(num2);
				num++;
			}
		}
		return num;
	}
}
