using System;
using UnityEngine;

namespace Rust.Safety;

public static class Check
{
	public static bool EntityValid(BaseEntity entity)
	{
		if ((bool)entity)
		{
			return entity.IsValid();
		}
		return false;
	}

	public static bool EntityIsServer(BaseEntity entity)
	{
		if (EntityValid(entity))
		{
			return entity.isServer;
		}
		return false;
	}

	public static bool EntityIsClient(BaseEntity entity)
	{
		if (EntityValid(entity))
		{
			return entity.isClient;
		}
		return false;
	}

	public static bool EntitySamePrefabs(BaseEntity entity1, BaseEntity entity2)
	{
		return entity1.prefabID == entity2.prefabID;
	}

	public static bool FlagChanged(Enum flag, BaseEntity.Flags old, BaseEntity.Flags next)
	{
		if (next.HasFlag(flag))
		{
			return !old.HasFlag(flag);
		}
		return false;
	}

	public static bool RefValid<T>(ResourceRef<T> gRef) where T : UnityEngine.Object
	{
		if (gRef == null)
		{
			return gRef.isValid;
		}
		return false;
	}

	public static bool InBuildingPrivilegeArea(BasePlayer ply, bool useCache = true, float cacheDuration = 1f)
	{
		if (!(ply.GetBuildingPrivilege(useCache, cacheDuration) != null))
		{
			return ply.HasPrivilegeFromOther(useCache);
		}
		return true;
	}

	public static bool HasTCBuildingPrivilege(BasePlayer ply, bool useCache = true, float cacheDuration = 1f)
	{
		return ply.IsBuildingAuthed(useCache, cacheDuration);
	}

	public static bool HasOtherBuildingPrivilege(BasePlayer ply, bool useCache = true)
	{
		return ply.HasPrivilegeFromOther(useCache);
	}

	public static bool HasBuildingPrivilege(BasePlayer ply, bool useCache = true, float cacheDuration = 1f)
	{
		if (!ply.IsBuildingAuthed(useCache, cacheDuration))
		{
			return ply.HasPrivilegeFromOther(useCache);
		}
		return true;
	}

	public static bool IsAuthorisedToBuild(BasePlayer ply, bool useCache = true, float cacheDuration = 1f)
	{
		if (!InBuildingPrivilegeArea(ply, useCache))
		{
			return false;
		}
		return ply.CanBuild(useCache, cacheDuration);
	}
}
