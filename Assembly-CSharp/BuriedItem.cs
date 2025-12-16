using System;
using Facepunch;
using ProtoBuf;
using UnityEngine;

public class BuriedItem : Pool.IPooled
{
	public int? ItemId { get; private set; }

	public ulong UID { get; private set; }

	public ItemOwnershipShare? OwnershipShare { get; private set; }

	public ulong? SkinId { get; private set; }

	public long ExpiryTime { get; set; }

	public Vector2 Location { get; private set; }

	public float? Condition { get; private set; }

	public static BuriedItem Create(Item item, Vector3 worldPosition, long expiryTime)
	{
		BuriedItem obj = Pool.Get<BuriedItem>();
		if (obj == null)
		{
			Debug.LogError("Failed to fetch buried item from the pool. Are we out of memory, or is the pool broken?");
			return null;
		}
		if (item.info == null || (object)item.info == null)
		{
			Debug.LogError($"Tried to create a buried item with an item that has no ItemDefinition! UID: {item.uid}, ItemId: {item.info?.itemid}");
			Pool.Free(ref obj);
			return null;
		}
		obj.ItemId = item.info.itemid;
		obj.ExpiryTime = expiryTime;
		obj.Location = new Vector2(worldPosition.x, worldPosition.z);
		obj.Condition = (item.hasCondition ? new float?(item.condition) : ((float?)null));
		obj.UID = item.uid.Value;
		if (item.ownershipShares.Count > 0)
		{
			obj.OwnershipShare = item.ownershipShares[0];
		}
		if (item.skin != 0L)
		{
			obj.SkinId = item.skin;
		}
		return obj;
	}

	public static BuriedItem Create(ProtoBuf.BuriedItems.StoredBuriedItem storedBuriedItem)
	{
		BuriedItem buriedItem = Pool.Get<BuriedItem>();
		buriedItem.ItemId = storedBuriedItem.itemId;
		buriedItem.SkinId = storedBuriedItem.skinId;
		buriedItem.Location = storedBuriedItem.location;
		buriedItem.ExpiryTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + storedBuriedItem.expiryTimeDiff;
		buriedItem.Condition = ((storedBuriedItem.condition < 0f) ? ((float?)null) : new float?(storedBuriedItem.condition));
		buriedItem.UID = storedBuriedItem.uid;
		if (storedBuriedItem.ownership != null)
		{
			buriedItem.OwnershipShare = new ItemOwnershipShare
			{
				amount = storedBuriedItem.ownership.amount,
				reason = storedBuriedItem.ownership.reason,
				username = storedBuriedItem.ownership.username
			};
		}
		return buriedItem;
	}

	public void EnterPool()
	{
		ItemId = null;
		OwnershipShare = null;
		SkinId = null;
	}

	public void LeavePool()
	{
	}
}
