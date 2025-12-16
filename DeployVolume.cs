using System;
using System.Collections.Generic;
using Facepunch;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class DeployVolume : PrefabAttribute
{
	public enum EntityMode
	{
		ExcludeList,
		IncludeList
	}

	public enum TypeFilterMode
	{
		Include,
		Ignore
	}

	public LayerMask layers = 537001984;

	[InspectorFlags]
	public ColliderInfo.Flags ignore;

	public EntityMode entityMode;

	[FormerlySerializedAs("entities")]
	public BaseEntity[] entityList;

	[SerializeField]
	public EntityListScriptableObject[] entityGroups;

	[SerializeField]
	public bool includeAllDoors;

	public bool IsBuildingBlock { get; set; }

	public static Collider LastDeployHit { get; set; }

	protected override Type GetIndexedType()
	{
		return typeof(DeployVolume);
	}

	public override void PreProcess(IPrefabProcessor preProcess, GameObject rootObj, string name, bool serverside, bool clientside, bool bundling)
	{
		base.PreProcess(preProcess, rootObj, name, serverside, clientside, bundling);
		IsBuildingBlock = rootObj.GetComponent<BuildingBlock>() != null;
	}

	protected abstract bool Check(Vector3 position, Quaternion rotation, int mask = -1);

	protected abstract bool Check(Vector3 position, Quaternion rotation, List<Type> types, TypeFilterMode filterMode, BaseEntity ignoredEntity = null, int mask = -1);

	protected abstract bool Check(Vector3 position, Quaternion rotation, OBB test, int mask = -1);

	public static bool Check(Vector3 position, Quaternion rotation, DeployVolume[] volumes, int mask = -1)
	{
		for (int i = 0; i < volumes.Length; i++)
		{
			if (volumes[i].Check(position, rotation, mask))
			{
				return true;
			}
		}
		return false;
	}

	public static bool Check(Vector3 position, Quaternion rotation, DeployVolume[] volumes, List<Type> types, TypeFilterMode filterMode, BaseEntity ignoredEntity = null, int mask = -1)
	{
		for (int i = 0; i < volumes.Length; i++)
		{
			if (volumes[i].Check(position, rotation, types, filterMode, ignoredEntity, mask))
			{
				return true;
			}
		}
		return false;
	}

	public static bool Check(Vector3 position, Quaternion rotation, List<DeployVolume> volumes, OBB test, int mask = -1)
	{
		for (int i = 0; i < volumes.Count; i++)
		{
			if (volumes[i].Check(position, rotation, test, mask))
			{
				return true;
			}
		}
		return false;
	}

	public static bool CheckSphere(Vector3 pos, float radius, int layerMask, DeployVolume volume)
	{
		List<Collider> obj = Pool.Get<List<Collider>>();
		GamePhysics.OverlapSphere(pos, radius, obj, layerMask, QueryTriggerInteraction.Collide);
		bool result = CheckFlags(obj, volume);
		Pool.FreeUnmanaged(ref obj);
		return result;
	}

	public static bool CheckCapsule(Vector3 start, Vector3 end, float radius, int layerMask, DeployVolume volume)
	{
		return CheckCapsule(start, end, radius, layerMask, volume, null, TypeFilterMode.Include);
	}

	public static bool CheckCapsule(Vector3 start, Vector3 end, float radius, int layerMask, DeployVolume volume, List<Type> types, TypeFilterMode filterMode, BaseEntity ignoredEntity = null)
	{
		List<Collider> obj = Pool.Get<List<Collider>>();
		GamePhysics.OverlapCapsule(start, end, radius, obj, layerMask, QueryTriggerInteraction.Collide);
		bool result = CheckFlags(obj, volume, types, filterMode, ignoredEntity);
		Pool.FreeUnmanaged(ref obj);
		return result;
	}

	public static bool CheckOBB(OBB obb, int layerMask, DeployVolume volume)
	{
		return CheckOBB(obb, layerMask, volume, null, TypeFilterMode.Include);
	}

	public static bool CheckOBB(OBB obb, int layerMask, DeployVolume volume, List<Type> types, TypeFilterMode filterMode, BaseEntity ignoredEntity = null)
	{
		List<Collider> obj = Pool.Get<List<Collider>>();
		GamePhysics.OverlapOBB(obb, obj, layerMask, QueryTriggerInteraction.Collide);
		bool result = CheckFlags(obj, volume, types, filterMode, ignoredEntity);
		Pool.FreeUnmanaged(ref obj);
		return result;
	}

	public static bool CheckBounds(Bounds bounds, int layerMask, DeployVolume volume)
	{
		List<Collider> obj = Pool.Get<List<Collider>>();
		GamePhysics.OverlapBounds(bounds, obj, layerMask, QueryTriggerInteraction.Collide);
		bool result = CheckFlags(obj, volume);
		Pool.FreeUnmanaged(ref obj);
		return result;
	}

	private static bool CheckFlags(List<Collider> list, DeployVolume volume, List<Type> types = null, TypeFilterMode filterMode = TypeFilterMode.Include, BaseEntity ignoredEntity = null)
	{
		if (volume == null)
		{
			return true;
		}
		LastDeployHit = null;
		for (int i = 0; i < list.Count; i++)
		{
			LastDeployHit = list[i];
			BaseEntity baseEntity = GameObjectEx.ToBaseEntity(LastDeployHit);
			if (ignoredEntity != null && baseEntity != null && baseEntity == ignoredEntity)
			{
				continue;
			}
			if (volume.includeAllDoors && list[i] != null)
			{
				BaseEntity baseEntity2 = GameObjectEx.ToBaseEntity(list[i]);
				if (baseEntity2 != null && baseEntity2 is Door)
				{
					return true;
				}
			}
			if (baseEntity != null && types != null)
			{
				Type type = baseEntity.GetType();
				bool flag = types.Contains(type);
				if ((filterMode == TypeFilterMode.Include && !flag) || (filterMode == TypeFilterMode.Ignore && flag))
				{
					continue;
				}
			}
			GameObject gameObject = list[i].gameObject;
			if (gameObject.CompareTag("DeployVolumeIgnore"))
			{
				continue;
			}
			ColliderInfo component = gameObject.GetComponent<ColliderInfo>();
			if (component != null && component.HasFlag(ColliderInfo.Flags.OnlyBlockBuildingBlock) && !volume.IsBuildingBlock)
			{
				continue;
			}
			if (gameObject.HasCustomTag(GameObjectTag.BlockPlacement))
			{
				return true;
			}
			MonumentInfo monument = ColliderEx.GetMonument(list[i]);
			if ((!(monument != null) || monument.IsSafeZone || !volume.ignore.HasFlag(ColliderInfo.Flags.Monument)) && (!(component != null) || (volume.ignore & component.flags) == 0))
			{
				if (component != null && volume.ignore != 0 && component.HasFlag(volume.ignore))
				{
					return false;
				}
				if (ShouldApplyVolumeForEntity(volume, baseEntity))
				{
					return true;
				}
			}
		}
		return false;
	}

	public static bool ShouldApplyVolumeForEntity(DeployVolume volume, BaseEntity entity)
	{
		if (volume.entityList == null || volume.entityGroups == null || (volume.entityList.Length == 0 && volume.entityGroups.Length == 0))
		{
			return true;
		}
		if (volume.entityGroups.Length != 0)
		{
			EntityListScriptableObject[] array = volume.entityGroups;
			foreach (EntityListScriptableObject entityListScriptableObject in array)
			{
				if (CollectionEx.IsNullOrEmpty(entityListScriptableObject.entities))
				{
					Debug.LogWarning("Skipping entity group '" + entityListScriptableObject.name + "' when checking volume: there are no entities");
				}
				else if (CheckEntityList(entity, entityListScriptableObject.entities, trueIfAnyFound: true))
				{
					return true;
				}
			}
		}
		if (volume.entityList.Length != 0 && CheckEntityList(entity, volume.entityList, volume.entityMode == EntityMode.IncludeList))
		{
			return true;
		}
		return false;
	}

	public static bool CheckEntityList(BaseEntity entity, BaseEntity[] entities, bool trueIfAnyFound)
	{
		if (entities == null || entities.Length == 0)
		{
			return true;
		}
		bool flag = false;
		if (entity != null)
		{
			foreach (BaseEntity baseEntity in entities)
			{
				if (entity.prefabID == baseEntity.prefabID)
				{
					flag = true;
					break;
				}
				if (entity is ModularCar && baseEntity is ModularCar)
				{
					flag = true;
					break;
				}
			}
		}
		if (trueIfAnyFound)
		{
			return flag;
		}
		return !flag;
	}
}
