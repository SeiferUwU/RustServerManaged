using System.Collections.Generic;
using ConVar;
using Facepunch;
using Rust;
using UnityEngine;

public class GameModeSoftcore : GameModeVanilla
{
	public GameObjectRef reclaimManagerPrefab;

	[ServerVar]
	public static float reclaim_fraction_belt = 0.5f;

	[ServerVar]
	public static float reclaim_fraction_wear = 1f;

	[ServerVar]
	public static float reclaim_fraction_main = 0.5f;

	[ServerVar]
	public static bool reclaim_suicide = false;

	[ServerVar]
	public static bool reclaim_building_auth = false;

	private static string[] StartingItems = new string[5] { "rock", "torch", "cakefiveyear", "partyhat", "snowball" };

	protected override void OnCreated()
	{
		base.OnCreated();
		SingletonComponent<ServerMgr>.Instance.CreateImportantEntity<ReclaimManager>(reclaimManagerPrefab.resourcePath);
	}

	public void ReturnItemsTo(List<Item> source, ItemContainer itemContainer)
	{
		foreach (Item item in source)
		{
			item.MoveToContainer(itemContainer);
		}
	}

	public override void OnPlayerDeath(BasePlayer instigator, BasePlayer victim, HitInfo deathInfo = null)
	{
		if (victim != null && (victim.IsInTutorial || (victim.net != null && victim.net.group != null && victim.net.group.restricted) || (deathInfo != null && deathInfo.damageTypes.GetMajorityDamageType() == DamageType.Suicide && !victim.IsWounded() && !reclaim_suicide)))
		{
			return;
		}
		if (victim != null && !victim.IsNpc && !victim.IsInTutorial)
		{
			if (!reclaim_building_auth)
			{
				BuildingPrivlidge buildingPrivilege = victim.GetBuildingPrivilege();
				if (buildingPrivilege != null && buildingPrivilege.IsAuthed(victim))
				{
					return;
				}
			}
			SetInventoryLocked(victim, wantsLocked: false);
			if (ReclaimManager.instance == null)
			{
				Debug.LogWarning("No reclaim manage for softcore");
				return;
			}
			List<Item> obj = Facepunch.Pool.Get<List<Item>>();
			List<Item> obj2 = Facepunch.Pool.Get<List<Item>>();
			List<Item> obj3 = Facepunch.Pool.Get<List<Item>>();
			List<Item> obj4 = Facepunch.Pool.Get<List<Item>>();
			List<Item> obj5 = Facepunch.Pool.Get<List<Item>>();
			List<Item> obj6 = Facepunch.Pool.Get<List<Item>>();
			List<Item> obj7 = Facepunch.Pool.Get<List<Item>>();
			List<Item> obj8 = Facepunch.Pool.Get<List<Item>>();
			victim.inventory.containerBelt.RemoveItemsFromContainer(obj5, StartingItems);
			victim.inventory.containerMain.RemoveItemsFromContainer(obj7, StartingItems);
			victim.inventory.containerWear.RemoveItemsFromContainer(obj6, StartingItems);
			victim.inventory.containerBelt.RemoveFractionOfContainer(obj, 1f - reclaim_fraction_belt);
			victim.inventory.containerMain.RemoveFractionOfContainer(obj3, 1f - reclaim_fraction_main);
			Item backpackWithInventory = victim.inventory.GetBackpackWithInventory();
			if (backpackWithInventory != null)
			{
				backpackWithInventory.contents.RemoveItemsFromContainer(obj8, StartingItems);
				backpackWithInventory.contents.RemoveFractionOfContainer(obj4, 1f - reclaim_fraction_main);
				if (obj4.Count > 0)
				{
					backpackWithInventory.contents.MergeAllStacks();
				}
				ReturnItemsTo(obj8, backpackWithInventory.contents);
			}
			victim.inventory.containerWear.RemoveFractionOfContainer(obj2, 1f - reclaim_fraction_wear);
			if (obj.Count > 0 || obj2.Count > 0 || obj3.Count > 0)
			{
				ReclaimManager.instance.AddPlayerReclaim(victim.userID, obj, obj2, obj3, obj4);
			}
			ReturnItemsTo(obj5, victim.inventory.containerBelt);
			ReturnItemsTo(obj7, victim.inventory.containerMain);
			ReturnItemsTo(obj6, victim.inventory.containerWear);
			if (backpackWithInventory != null && obj2.Contains(backpackWithInventory))
			{
				victim.inventory.containerMain.MergeAllStacks();
				backpackWithInventory.contents.MoveAllItems(victim.inventory.containerMain);
				backpackWithInventory.contents.MoveAllItems(victim.inventory.containerBelt);
				if (backpackWithInventory.contents.itemList.Count > 0)
				{
					backpackWithInventory.contents.Drop(ReclaimManager.ReclaimCorpsePrefab, victim.GetDropPosition(), victim.transform.rotation, 0f);
				}
			}
			Facepunch.Pool.Free(ref obj, freeElements: false);
			Facepunch.Pool.Free(ref obj2, freeElements: false);
			Facepunch.Pool.Free(ref obj3, freeElements: false);
			Facepunch.Pool.Free(ref obj4, freeElements: false);
			Facepunch.Pool.Free(ref obj5, freeElements: false);
			Facepunch.Pool.Free(ref obj6, freeElements: false);
			Facepunch.Pool.Free(ref obj7, freeElements: false);
			Facepunch.Pool.Free(ref obj8, freeElements: false);
		}
		base.OnPlayerDeath(instigator, victim, deathInfo);
	}

	public override void OnPlayerRespawn(BasePlayer player)
	{
		base.OnPlayerRespawn(player);
		ReclaimManager.instance.GetReclaim(player.userID)?.GiveToPlayer(player);
	}

	public override PooledList<SleepingBag> FindSleepingBagsForPlayer(ulong playerID, bool ignoreTimers)
	{
		return SleepingBag.FindForPlayer(playerID, ignoreTimers);
	}

	public override float CorpseRemovalTime(BaseCorpse corpse)
	{
		foreach (MonumentInfo monument in TerrainMeta.Path.Monuments)
		{
			if (monument != null && monument.IsSafeZone && monument.Bounds.Contains(corpse.transform.position))
			{
				return 30f;
			}
		}
		return ConVar.Server.corpsedespawn;
	}

	public void SetInventoryLocked(BasePlayer player, bool wantsLocked)
	{
		player.inventory.containerMain.SetLocked(wantsLocked);
		player.inventory.containerBelt.SetLocked(wantsLocked);
		player.inventory.containerWear.SetLocked(wantsLocked);
	}

	public override void OnPlayerWounded(BasePlayer instigator, BasePlayer victim, HitInfo info)
	{
		base.OnPlayerWounded(instigator, victim, info);
		SetInventoryLocked(victim, wantsLocked: true);
	}

	public override void OnPlayerRevived(BasePlayer instigator, BasePlayer victim)
	{
		if (!victim.IsRestrained)
		{
			SetInventoryLocked(victim, wantsLocked: false);
		}
		base.OnPlayerRevived(instigator, victim);
	}

	public override PlayerInventory.CanMoveFromResponse CanMoveItemsFrom(PlayerInventory inv, BaseEntity source, Item item)
	{
		if (item.parent != null && item.parent.HasFlag(ItemContainer.Flag.IsPlayer))
		{
			return new PlayerInventory.CanMoveFromResponse(!item.parent.IsLocked(), PlayerInventoryErrors.InventoryLockedError);
		}
		return base.CanMoveItemsFrom(inv, source, item);
	}
}
