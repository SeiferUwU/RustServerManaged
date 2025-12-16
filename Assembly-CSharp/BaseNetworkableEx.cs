using UnityEngine;

public static class BaseNetworkableEx
{
	public static bool IsValid(this BaseNetworkable ent)
	{
		if (ent == null)
		{
			return false;
		}
		if (ent.net == null)
		{
			return false;
		}
		return true;
	}

	public static bool Is<T>(this Object ent, out T entAsT) where T : Object
	{
		entAsT = null;
		if (ent == null)
		{
			return false;
		}
		entAsT = ent as T;
		if (entAsT == null)
		{
			return false;
		}
		return true;
	}

	public static bool IsRealNull(this BaseNetworkable ent)
	{
		return (object)ent == null;
	}
}
