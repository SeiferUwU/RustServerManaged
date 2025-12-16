using System;
using System.Collections.Generic;
using Facepunch;
using ProtoBuf;
using UnityEngine;

public class ItemModFoodSpoiling : ItemMod
{
	public class FoodSpoilingWorkQueue : PersistentObjectWorkQueue<Item>
	{
		private Dictionary<ItemId, TimeSince> lastUpdated = new Dictionary<ItemId, TimeSince>();

		protected override void RunJob(Item foodItem)
		{
			float timeToApply = 0f;
			if (lastUpdated.TryGetValue(foodItem.uid, out var value))
			{
				timeToApply = value;
				lastUpdated[foodItem.uid] = 0f;
			}
			else
			{
				lastUpdated.Add(foodItem.uid, 0f);
			}
			DeductTimeFromFoodItem(foodItem, timeToApply, setDirty: false);
		}

		public static void DeductTimeFromFoodItem(Item foodItem, float timeToApply, bool setDirty)
		{
			if (foodItem.instanceData != null)
			{
				float dataFloat = foodItem.instanceData.dataFloat;
				float num = 1f;
				if (foodItem.parent != null && foodItem.parent.entityOwner != null)
				{
					if (foodItem.parent.entityOwner.TryGetComponent<IFoodSpoilModifier>(out var component))
					{
						num = component.GetSpoilMultiplier(foodItem);
					}
					else if (TerrainMeta.BiomeMap.GetBiome(foodItem.parent.entityOwner.transform.position, 8) > 0f)
					{
						num = 0f;
					}
				}
				else if (foodItem.parent != null && foodItem.parent.playerOwner != null && TerrainMeta.BiomeMap.GetBiome(foodItem.parent.playerOwner.transform.position, 8) > 0f)
				{
					num = 0f;
				}
				bool flag = num != 1f;
				if (foodItem.HasFlag(Item.Flag.Refrigerated) != flag)
				{
					foodItem.SetFlag(Item.Flag.Refrigerated, flag);
					foodItem.MarkDirty();
					if (foodItem.GetEntityOwner() != null)
					{
						foodItem.GetEntityOwner().SendNetworkUpdate();
					}
				}
				foodItem.instanceData.dataFloat -= timeToApply * num;
				if (!(foodItem.instanceData.dataFloat <= 0f) || !(dataFloat > 0f))
				{
					return;
				}
				int amount = foodItem.amount;
				ItemContainer parent = foodItem.parent;
				foodItem.RemoveFromContainer();
				Item item = ItemManager.Create(foodItem.info.GetComponent<ItemModFoodSpoiling>().SpoilItem, amount, 0uL);
				if (parent != null && !parent.GiveItem(item))
				{
					if (parent.entityOwner != null)
					{
						item.Drop(parent.entityOwner.transform.position + Vector3.up * parent.entityOwner.bounds.size.y, Vector3.zero);
					}
					else
					{
						item.Remove();
					}
				}
				else if (item.parent == null)
				{
					BaseEntity worldEntity = foodItem.GetWorldEntity();
					if (worldEntity != null)
					{
						item.Drop(worldEntity.transform.position, Vector3.zero, worldEntity.transform.rotation);
					}
					else
					{
						item.Remove();
					}
				}
				foodItem.Remove();
				ItemManager.DoRemoves();
			}
			else if (setDirty)
			{
				foodItem.MarkDirty();
				if (foodItem.GetEntityOwner() != null)
				{
					foodItem.GetEntityOwner().SendNetworkUpdate();
				}
			}
		}
	}

	public float TotalSpoilTimeHours = 12f;

	public ItemDefinition SpoilItem;

	public static FoodSpoilingWorkQueue foodSpoilItems = new FoodSpoilingWorkQueue();

	public override void OnItemCreated(Item item)
	{
		base.OnItemCreated(item);
		if (item.instanceData == null)
		{
			item.instanceData = Pool.Get<ProtoBuf.Item.InstanceData>();
			item.instanceData.dataFloat = 3600f * TotalSpoilTimeHours;
			item.instanceData.ShouldPool = false;
		}
		foodSpoilItems.Add(item);
	}

	public override void OnRemove(Item item)
	{
		base.OnRemove(item);
		foodSpoilItems.Remove(item);
	}

	public static void DeductTimeFromAll(TimeSpan span)
	{
		foodSpoilItems.RunOnAll(delegate(Item foodItem)
		{
			FoodSpoilingWorkQueue.DeductTimeFromFoodItem(foodItem, (float)span.TotalSeconds, setDirty: true);
		});
	}
}
