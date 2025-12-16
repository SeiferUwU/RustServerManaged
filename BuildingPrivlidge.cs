#define UNITY_ASSERTIONS
using System;
using System.Collections.Generic;
using System.Linq;
using ConVar;
using Facepunch;
using Facepunch.Rust;
using Network;
using Oxide.Core;
using ProtoBuf;
using UnityEngine;
using UnityEngine.Assertions;

public class BuildingPrivlidge : StorageContainer
{
	public class UpkeepBracket
	{
		public int objectsUpTo;

		public float fraction;

		public float blocksTaxPaid;

		public UpkeepBracket(int numObjs, float frac)
		{
			objectsUpTo = numObjs;
			fraction = frac;
			blocksTaxPaid = 0f;
		}
	}

	public GameObject assignDialog;

	[NonSerialized]
	public HashSet<ulong> authorizedPlayers = new HashSet<ulong>();

	public const Flags Flag_MaxAuths = Flags.Reserved5;

	public const Flags Flag_BlockAllFromBuilding = Flags.Reserved6;

	public List<ItemDefinition> allowedConstructionItems = new List<ItemDefinition>();

	public float cachedProtectedMinutes;

	public float nextProtectedCalcTime;

	public static UpkeepBracket[] upkeepBrackets = new UpkeepBracket[4]
	{
		new UpkeepBracket(ConVar.Decay.bracket_0_blockcount, ConVar.Decay.bracket_0_costfraction),
		new UpkeepBracket(ConVar.Decay.bracket_1_blockcount, ConVar.Decay.bracket_1_costfraction),
		new UpkeepBracket(ConVar.Decay.bracket_2_blockcount, ConVar.Decay.bracket_2_costfraction),
		new UpkeepBracket(0, ConVar.Decay.bracket_3_costfraction)
	};

	private static UpkeepBracket[] doorUpkeepBrackets = new UpkeepBracket[4]
	{
		new UpkeepBracket(ConVar.Decay.bracket_0_doorcount, ConVar.Decay.bracket_0_doorfraction),
		new UpkeepBracket(ConVar.Decay.bracket_1_doorcount, ConVar.Decay.bracket_1_doorfraction),
		new UpkeepBracket(ConVar.Decay.bracket_2_doorcount, ConVar.Decay.bracket_2_doorfraction),
		new UpkeepBracket(0, ConVar.Decay.bracket_3_doorfraction)
	};

	public List<ItemAmount> upkeepBuffer = new List<ItemAmount>();

	public override bool OnRpcMessage(BasePlayer player, uint rpc, Message msg)
	{
		using (TimeWarning.New("BuildingPrivlidge.OnRpcMessage"))
		{
			if (rpc == 82205621 && player != null)
			{
				Assert.IsTrue(player.isServer, "SV_RPC Message is using a clientside player!");
				if (Global.developer > 2)
				{
					Debug.Log("SV_RPCMessage: " + player?.ToString() + " - AddAuthorize ");
				}
				using (TimeWarning.New("AddAuthorize"))
				{
					using (TimeWarning.New("Conditions"))
					{
						if (!RPC_Server.IsVisible.Test(82205621u, "AddAuthorize", this, player, 3f))
						{
							return true;
						}
					}
					try
					{
						using (TimeWarning.New("Call"))
						{
							RPCMessage rpc2 = new RPCMessage
							{
								connection = msg.connection,
								player = player,
								read = msg.read
							};
							AddAuthorize(rpc2);
						}
					}
					catch (Exception exception)
					{
						Debug.LogException(exception);
						player.Kick("RPC Error in AddAuthorize");
					}
				}
				return true;
			}
			if (rpc == 253307592 && player != null)
			{
				Assert.IsTrue(player.isServer, "SV_RPC Message is using a clientside player!");
				if (Global.developer > 2)
				{
					Debug.Log("SV_RPCMessage: " + player?.ToString() + " - ClearList ");
				}
				using (TimeWarning.New("ClearList"))
				{
					using (TimeWarning.New("Conditions"))
					{
						if (!RPC_Server.IsVisible.Test(253307592u, "ClearList", this, player, 3f))
						{
							return true;
						}
					}
					try
					{
						using (TimeWarning.New("Call"))
						{
							RPCMessage rpc3 = new RPCMessage
							{
								connection = msg.connection,
								player = player,
								read = msg.read
							};
							ClearList(rpc3);
						}
					}
					catch (Exception exception2)
					{
						Debug.LogException(exception2);
						player.Kick("RPC Error in ClearList");
					}
				}
				return true;
			}
			if (rpc == 3617985969u && player != null)
			{
				Assert.IsTrue(player.isServer, "SV_RPC Message is using a clientside player!");
				if (Global.developer > 2)
				{
					Debug.Log("SV_RPCMessage: " + player?.ToString() + " - RemoveSelfAuthorize ");
				}
				using (TimeWarning.New("RemoveSelfAuthorize"))
				{
					using (TimeWarning.New("Conditions"))
					{
						if (!RPC_Server.IsVisible.Test(3617985969u, "RemoveSelfAuthorize", this, player, 3f))
						{
							return true;
						}
					}
					try
					{
						using (TimeWarning.New("Call"))
						{
							RPCMessage rpc4 = new RPCMessage
							{
								connection = msg.connection,
								player = player,
								read = msg.read
							};
							RemoveSelfAuthorize(rpc4);
						}
					}
					catch (Exception exception3)
					{
						Debug.LogException(exception3);
						player.Kick("RPC Error in RemoveSelfAuthorize");
					}
				}
				return true;
			}
			if (rpc == 2051750736 && player != null)
			{
				Assert.IsTrue(player.isServer, "SV_RPC Message is using a clientside player!");
				if (Global.developer > 2)
				{
					Debug.Log("SV_RPCMessage: " + player?.ToString() + " - RPC_Rotate ");
				}
				using (TimeWarning.New("RPC_Rotate"))
				{
					using (TimeWarning.New("Conditions"))
					{
						if (!RPC_Server.IsVisible.Test(2051750736u, "RPC_Rotate", this, player, 3f))
						{
							return true;
						}
					}
					try
					{
						using (TimeWarning.New("Call"))
						{
							RPCMessage msg2 = new RPCMessage
							{
								connection = msg.connection,
								player = player,
								read = msg.read
							};
							RPC_Rotate(msg2);
						}
					}
					catch (Exception exception4)
					{
						Debug.LogException(exception4);
						player.Kick("RPC Error in RPC_Rotate");
					}
				}
				return true;
			}
		}
		return base.OnRpcMessage(player, rpc, msg);
	}

	public override void OnDeployableCorpseSpawned(BaseEntity corpse)
	{
		base.OnDeployableCorpseSpawned(corpse);
		BuildingPrivlidge componentInChildren = corpse.GetComponentInChildren<BuildingPrivlidge>();
		if (componentInChildren == null)
		{
			Debug.LogError("Not able to transfer auth of TC to corpse: BuildingPrivlidge component not found in child");
			return;
		}
		componentInChildren.SetAuthListFrom(this);
		componentInChildren.AttachToBuilding(buildingID);
	}

	public override bool ShouldDropDeployableCorpse(HitInfo info)
	{
		BuildingManager.Building building = GetBuilding();
		if (building == null)
		{
			return false;
		}
		if (building.buildingBlocks == null || building.buildingBlocks.Count == 0)
		{
			return false;
		}
		return base.ShouldDropDeployableCorpse(info);
	}

	public override void ResetState()
	{
		base.ResetState();
		authorizedPlayers.Clear();
	}

	public bool CanBuild(BasePlayer player)
	{
		if (HasFlag(Flags.Reserved6))
		{
			return false;
		}
		return IsAuthed(player);
	}

	public bool IsAuthed(BasePlayer player)
	{
		return IsAuthed(player.userID);
	}

	public bool IsAuthed(ulong userId)
	{
		return authorizedPlayers.Contains(userId);
	}

	public bool AnyAuthed()
	{
		return authorizedPlayers.Count > 0;
	}

	public void SetAuthListFrom(BuildingPrivlidge source)
	{
		authorizedPlayers = new HashSet<ulong>();
		foreach (ulong authorizedPlayer in source.authorizedPlayers)
		{
			authorizedPlayers.Add(authorizedPlayer);
		}
	}

	public override bool ItemFilter(Item item, int targetSlot)
	{
		bool flag = allowedConstructionItems.Contains(item.info);
		if (!flag && targetSlot == -1)
		{
			int num = 0;
			foreach (Item item2 in base.inventory.itemList)
			{
				if (!allowedConstructionItems.Contains(item2.info) && (item2.info != item.info || item2.amount == item2.MaxStackable()))
				{
					num++;
				}
			}
			if (num >= 24)
			{
				return false;
			}
		}
		if (targetSlot >= 24 && targetSlot <= 28)
		{
			return flag;
		}
		return base.ItemFilter(item, targetSlot);
	}

	public override void Save(SaveInfo info)
	{
		base.Save(info);
		info.msg.buildingPrivilege = Facepunch.Pool.Get<BuildingPrivilege>();
		if (!info.forDisk)
		{
			float num = CalculateUpkeepPeriodMinutes();
			float protectedMinutes = GetProtectedMinutes();
			if ((double)ConVar.Decay.scale > 0.01)
			{
				info.msg.buildingPrivilege.upkeepPeriodMinutes = num / ConVar.Decay.scale;
				info.msg.buildingPrivilege.protectedMinutes = protectedMinutes / ConVar.Decay.scale;
			}
			else
			{
				info.msg.buildingPrivilege.upkeepPeriodMinutes = num;
				info.msg.buildingPrivilege.protectedMinutes = protectedMinutes;
			}
			info.msg.buildingPrivilege.costFraction = CalculateUpkeepCostFraction(doors: false);
			info.msg.buildingPrivilege.doorCostFraction = CalculateUpkeepCostFraction(doors: true);
			info.msg.buildingPrivilege.clientAuthed = IsAuthed(info.forConnection.userid);
			info.msg.buildingPrivilege.clientAnyAuthed = AnyAuthed();
		}
		if (!info.forDisk && !info.msg.buildingPrivilege.clientAuthed)
		{
			return;
		}
		info.msg.buildingPrivilege.users = Facepunch.Pool.Get<List<PlayerNameID>>();
		foreach (ulong authorizedPlayer in authorizedPlayers)
		{
			PlayerNameID playerNameID = Facepunch.Pool.Get<PlayerNameID>();
			playerNameID.userid = authorizedPlayer;
			info.msg.buildingPrivilege.users.Add(playerNameID);
		}
	}

	public override bool CanUseNetworkCache(Connection connection)
	{
		return false;
	}

	public override void Load(LoadInfo info)
	{
		base.Load(info);
		authorizedPlayers.Clear();
		if (info.msg.buildingPrivilege == null)
		{
			return;
		}
		if (info.msg.buildingPrivilege.users != null)
		{
			foreach (PlayerNameID user in info.msg.buildingPrivilege.users)
			{
				authorizedPlayers.Add(user.userid);
			}
		}
		if (!info.fromDisk)
		{
			cachedProtectedMinutes = info.msg.buildingPrivilege.protectedMinutes;
		}
	}

	public void BuildingDirty()
	{
		if (base.isServer)
		{
			AddDelayedUpdate();
		}
	}

	public bool AtMaxAuthCapacity()
	{
		return HasFlag(Flags.Reserved5);
	}

	public void UpdateMaxAuthCapacity()
	{
		BaseGameMode activeGameMode = BaseGameMode.GetActiveGameMode(serverside: true);
		if ((bool)activeGameMode && activeGameMode.limitTeamAuths)
		{
			SetFlag(Flags.Reserved5, authorizedPlayers.Count >= activeGameMode.GetMaxRelationshipTeamSize());
		}
	}

	protected override void OnInventoryDirty()
	{
		base.OnInventoryDirty();
		AddDelayedUpdate();
	}

	public override void OnItemAddedOrRemoved(Item item, bool bAdded)
	{
		base.OnItemAddedOrRemoved(item, bAdded);
		AddDelayedUpdate();
	}

	public void AddDelayedUpdate()
	{
		if (IsInvoking(DelayedUpdate))
		{
			CancelInvoke(DelayedUpdate);
		}
		Invoke(DelayedUpdate, 1f);
	}

	public void DelayedUpdate()
	{
		MarkProtectedMinutesDirty();
		SendNetworkUpdate();
	}

	public bool CanAdministrate(BasePlayer player)
	{
		BaseLock baseLock = GetSlot(Slot.Lock) as BaseLock;
		if (baseLock == null)
		{
			return true;
		}
		return baseLock.OnTryToOpen(player);
	}

	[RPC_Server.IsVisible(3f)]
	[RPC_Server]
	private void AddAuthorize(RPCMessage rpc)
	{
		if (rpc.player.CanInteract() && CanAdministrate(rpc.player))
		{
			ulong num = rpc.read.UInt64();
			if (Interface.CallHook("IOnCupboardAuthorize", num, rpc.player, this) == null)
			{
				AddPlayer(rpc.player, num);
				SendNetworkUpdate();
			}
		}
	}

	public void AddPlayer(BasePlayer granter, ulong targetPlayerId)
	{
		if (!AtMaxAuthCapacity())
		{
			authorizedPlayers.Add(targetPlayerId);
			Facepunch.Rust.Analytics.Azure.OnEntityAuthChanged(this, granter, authorizedPlayers, "added", targetPlayerId);
			UpdateMaxAuthCapacity();
		}
	}

	[RPC_Server.IsVisible(3f)]
	[RPC_Server]
	public void RemoveSelfAuthorize(RPCMessage rpc)
	{
		if (rpc.player.CanInteract() && CanAdministrate(rpc.player) && Interface.CallHook("OnCupboardDeauthorize", this, rpc.player) == null)
		{
			authorizedPlayers.Remove(rpc.player.userID);
			Facepunch.Rust.Analytics.Azure.OnEntityAuthChanged(this, rpc.player, authorizedPlayers, "removed", rpc.player.userID);
			UpdateMaxAuthCapacity();
			SendNetworkUpdate();
		}
	}

	[RPC_Server]
	[RPC_Server.IsVisible(3f)]
	public void ClearList(RPCMessage rpc)
	{
		if (rpc.player.CanInteract() && CanAdministrate(rpc.player) && Interface.CallHook("OnCupboardClearList", this, rpc.player) == null)
		{
			authorizedPlayers.Clear();
			UpdateMaxAuthCapacity();
			SendNetworkUpdate();
		}
	}

	[RPC_Server]
	[RPC_Server.IsVisible(3f)]
	public void RPC_Rotate(RPCMessage msg)
	{
		BasePlayer player = msg.player;
		if (player.CanBuild() && (bool)player.GetHeldEntity() && player.GetHeldEntity().GetComponent<Hammer>() != null && (GetSlot(Slot.Lock) == null || !GetSlot(Slot.Lock).IsLocked()) && !HasAttachedStorageAdaptor() && !HasAttachedStorageMonitor())
		{
			base.transform.rotation = Quaternion.LookRotation(-base.transform.forward, base.transform.up);
			SendNetworkUpdate();
			Deployable component = GetComponent<Deployable>();
			if (component != null && component.placeEffect.isValid)
			{
				Effect.server.Run(component.placeEffect.resourcePath, base.transform.position, Vector3.up);
			}
		}
		BaseEntity slot = GetSlot(Slot.Lock);
		if (slot != null)
		{
			slot.SendNetworkUpdate();
		}
	}

	public override int GetIdealSlot(BasePlayer player, ItemContainer container, Item item)
	{
		if (item != null && item.info != null && allowedConstructionItems.Contains(item.info))
		{
			if (player != null && player.IsInTutorial)
			{
				return 0;
			}
			for (int i = 24; i <= 27; i++)
			{
				if (base.inventory.GetSlot(i) == null)
				{
					return i;
				}
			}
		}
		return base.GetIdealSlot(player, container, item);
	}

	private void UnlinkDoorControllers()
	{
		BuildingManager.Building building = GetBuilding();
		if (building == null)
		{
			return;
		}
		foreach (DecayEntity decayEntity in building.decayEntities)
		{
			if (!(decayEntity is Door door))
			{
				continue;
			}
			foreach (BaseEntity child in door.children)
			{
				if (child is CustomDoorManipulator customDoorManipulator)
				{
					customDoorManipulator.SetTargetDoor(null);
				}
			}
		}
	}

	public override bool HasSlot(Slot slot)
	{
		if (slot == Slot.Lock)
		{
			return true;
		}
		return base.HasSlot(slot);
	}

	public override bool SupportsChildDeployables()
	{
		return true;
	}

	public float CalculateUpkeepPeriodMinutes()
	{
		if (base.isServer)
		{
			return ConVar.Decay.upkeep_period_minutes;
		}
		return 0f;
	}

	public float CalculateUpkeepCostFraction(bool doors)
	{
		if (base.isServer)
		{
			if (!doors)
			{
				return CalculateBuildingTaxRate();
			}
			return CalculateDoorTaxRate();
		}
		return 0f;
	}

	public void CalculateUpkeepCostAmounts(List<ItemAmount> itemAmounts)
	{
		BuildingManager.Building building = GetBuilding();
		if (building == null || !building.HasDecayEntities())
		{
			return;
		}
		float num = CalculateUpkeepCostFraction(doors: false);
		float num2 = CalculateUpkeepCostFraction(doors: true);
		foreach (DecayEntity decayEntity in building.decayEntities)
		{
			float multiplier = ((decayEntity is Door) ? num2 : num);
			decayEntity.CalculateUpkeepCostAmounts(itemAmounts, multiplier);
		}
	}

	public float GetProtectedMinutes(bool force = false)
	{
		if (base.isServer)
		{
			if (!force && UnityEngine.Time.realtimeSinceStartup < nextProtectedCalcTime)
			{
				return cachedProtectedMinutes;
			}
			nextProtectedCalcTime = UnityEngine.Time.realtimeSinceStartup + 60f;
			List<ItemAmount> obj = Facepunch.Pool.Get<List<ItemAmount>>();
			CalculateUpkeepCostAmounts(obj);
			float num = CalculateUpkeepPeriodMinutes();
			float num2 = -1f;
			if (base.inventory != null)
			{
				using PooledList<Item> pooledList = Facepunch.Pool.Get<PooledList<Item>>();
				foreach (ItemAmount item in obj)
				{
					pooledList.Clear();
					base.inventory.FindItemsByItemID(pooledList, item.itemid);
					int num3 = pooledList.Sum((Item x) => x.amount);
					if (num3 > 0 && item.amount > 0f)
					{
						float num4 = (float)num3 / item.amount * num;
						if (num2 == -1f || num4 < num2)
						{
							num2 = num4;
						}
					}
					else
					{
						num2 = 0f;
					}
				}
				if (num2 == -1f)
				{
					num2 = 0f;
				}
			}
			Facepunch.Pool.FreeUnmanaged(ref obj);
			cachedProtectedMinutes = num2;
			Interface.CallHook("OnCupboardProtectionCalculated", this, cachedProtectedMinutes);
			return cachedProtectedMinutes;
		}
		return 0f;
	}

	public override void OnDied(HitInfo info)
	{
		if (info != null && info.InitiatorPlayer != null && !info.InitiatorPlayer.IsNpc && info.InitiatorPlayer.serverClan != null)
		{
			IReadOnlyList<ClanMember> members = info.InitiatorPlayer.serverClan.Members;
			bool flag = false;
			foreach (ClanMember item in members)
			{
				if (item.SteamId == base.OwnerID)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				HandleKilledByClanMember(info.InitiatorPlayer);
			}
		}
		UnlinkDoorControllers();
		base.OnDied(info);
	}

	public override void Die(HitInfo info = null)
	{
		if (!IsDead())
		{
			if (ConVar.Decay.upkeep_grief_protection > 0f)
			{
				PurchaseAntiGriefTime(ConVar.Decay.upkeep_grief_protection * 60f);
			}
			base.Die(info);
		}
	}

	private async void HandleKilledByClanMember(BasePlayer player)
	{
		try
		{
			ClanValueResult<IClan> clanValueResult = await ClanManager.ServerInstance.Backend.GetByMember(base.OwnerID);
			IClan clan = (clanValueResult.IsSuccess ? clanValueResult.Value : null);
			if (clan != null)
			{
				player.AddClanScore(ClanScoreEventType.DestroyedToolCupboard, 1, null, clan);
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}

	public override void DecayTick()
	{
		BuildingBlock nearbyBuildingBlock = GetNearbyBuildingBlock();
		if (nearbyBuildingBlock != null)
		{
			BuildingManager.Building building = nearbyBuildingBlock.GetBuilding();
			if (building != null && building.ID != buildingID)
			{
				AttachToBuilding(building.ID);
			}
		}
		else
		{
			Kill(DestroyMode.Gib);
		}
		if (EnsurePrimary())
		{
			base.DecayTick();
		}
	}

	public bool EnsurePrimary()
	{
		BuildingManager.Building building = GetBuilding();
		if (building != null)
		{
			BuildingPrivlidge dominatingBuildingPrivilege = building.GetDominatingBuildingPrivilege();
			if (dominatingBuildingPrivilege != null && dominatingBuildingPrivilege != this)
			{
				Kill(DestroyMode.Gib);
				return false;
			}
		}
		return true;
	}

	public void MarkProtectedMinutesDirty(float delay = 0f)
	{
		nextProtectedCalcTime = UnityEngine.Time.realtimeSinceStartup + delay;
	}

	private static float CalculateTaxRate(int entityCount, bool blocks)
	{
		if (entityCount == 0)
		{
			if (!blocks)
			{
				return ConVar.Decay.bracket_0_doorfraction;
			}
			return ConVar.Decay.bracket_0_costfraction;
		}
		int num = entityCount;
		float num2 = 0f;
		for (int i = 0; i < 4; i++)
		{
			float num3 = (blocks ? ConVar.Decay.bracket_0_costfraction : ConVar.Decay.bracket_0_doorfraction);
			int b = (blocks ? ConVar.Decay.bracket_0_blockcount : ConVar.Decay.bracket_0_doorcount);
			switch (i)
			{
			case 1:
				num3 = (blocks ? ConVar.Decay.bracket_1_costfraction : ConVar.Decay.bracket_1_doorfraction);
				b = (blocks ? ConVar.Decay.bracket_1_blockcount : ConVar.Decay.bracket_1_doorcount);
				break;
			case 2:
				num3 = (blocks ? ConVar.Decay.bracket_2_costfraction : ConVar.Decay.bracket_1_doorfraction);
				b = (blocks ? ConVar.Decay.bracket_2_blockcount : ConVar.Decay.bracket_1_doorcount);
				break;
			case 3:
				num3 = (blocks ? ConVar.Decay.bracket_3_costfraction : ConVar.Decay.bracket_1_doorfraction);
				b = int.MaxValue;
				break;
			}
			if (num > 0)
			{
				int num4 = Mathf.Min(num, b);
				num -= num4;
				num2 += (float)num4 * num3;
			}
		}
		return num2 /= (float)entityCount;
	}

	private float CalculateDoorTaxRate()
	{
		if (!ConVar.Decay.use_door_upkeep_brackets)
		{
			return CalculateBuildingTaxRate();
		}
		BuildingManager.Building building = GetBuilding();
		if (building == null)
		{
			return ConVar.Decay.bracket_0_doorfraction;
		}
		if (!building.HasDecayEntities())
		{
			return ConVar.Decay.bracket_0_doorfraction;
		}
		return CalculateTaxRate(building.doors.Count, blocks: false);
	}

	public float CalculateBuildingTaxRate()
	{
		BuildingManager.Building building = GetBuilding();
		if (building == null)
		{
			return ConVar.Decay.bracket_0_costfraction;
		}
		if (!building.HasBuildingBlocks())
		{
			return ConVar.Decay.bracket_0_costfraction;
		}
		return CalculateTaxRate(building.buildingBlocks.Count, blocks: true);
	}

	public void ApplyUpkeepPayment()
	{
		List<Item> obj = Facepunch.Pool.Get<List<Item>>();
		for (int i = 0; i < upkeepBuffer.Count; i++)
		{
			ItemAmount itemAmount = upkeepBuffer[i];
			int num = (int)itemAmount.amount;
			if (num < 1)
			{
				continue;
			}
			base.inventory.Take(obj, itemAmount.itemid, num);
			Facepunch.Rust.Analytics.Azure.AddPendingItems(this, itemAmount.itemDef.shortname, num, "upkeep", consumed: true, perEntity: true);
			foreach (Item item in obj)
			{
				if (IsDebugging())
				{
					Debug.Log(ToString() + ": Using " + item.amount + " of " + item.info.shortname);
				}
				item.UseItem(item.amount);
			}
			obj.Clear();
			itemAmount.amount -= num;
			upkeepBuffer[i] = itemAmount;
		}
		Facepunch.Pool.Free(ref obj, freeElements: false);
	}

	public void QueueUpkeepPayment(List<ItemAmount> itemAmounts)
	{
		for (int i = 0; i < itemAmounts.Count; i++)
		{
			ItemAmount itemAmount = itemAmounts[i];
			bool flag = false;
			foreach (ItemAmount item in upkeepBuffer)
			{
				if (item.itemDef == itemAmount.itemDef)
				{
					item.amount += itemAmount.amount;
					if (IsDebugging())
					{
						Debug.Log(ToString() + ": Adding " + itemAmount.amount + " of " + itemAmount.itemDef.shortname + " to " + item.amount);
					}
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				if (IsDebugging())
				{
					Debug.Log(ToString() + ": Adding " + itemAmount.amount + " of " + itemAmount.itemDef.shortname);
				}
				upkeepBuffer.Add(new ItemAmount(itemAmount.itemDef, itemAmount.amount));
			}
		}
	}

	public bool CanAffordUpkeepPayment(List<ItemAmount> itemAmounts)
	{
		for (int i = 0; i < itemAmounts.Count; i++)
		{
			ItemAmount itemAmount = itemAmounts[i];
			if ((float)base.inventory.GetAmount(itemAmount.itemid, onlyUsableAmounts: true) < itemAmount.amount)
			{
				if (IsDebugging())
				{
					Debug.Log(ToString() + ": Can't afford " + itemAmount.amount + " of " + itemAmount.itemDef.shortname);
				}
				return false;
			}
		}
		return true;
	}

	public float PurchaseUpkeepTime(DecayEntity entity, float deltaTime)
	{
		float num = CalculateUpkeepCostFraction(doors: false);
		float num2 = CalculateUpkeepCostFraction(doors: true);
		float num3 = CalculateUpkeepPeriodMinutes() * 60f;
		float multiplier = ((entity is Door) ? num2 : num) * deltaTime / num3;
		List<ItemAmount> obj = Facepunch.Pool.Get<List<ItemAmount>>();
		entity.CalculateUpkeepCostAmounts(obj, multiplier);
		bool num4 = CanAffordUpkeepPayment(obj);
		QueueUpkeepPayment(obj);
		Facepunch.Pool.FreeUnmanaged(ref obj);
		ApplyUpkeepPayment();
		if (!num4)
		{
			return 0f;
		}
		return deltaTime;
	}

	public void PurchaseUpkeepTime(float deltaTime)
	{
		BuildingManager.Building building = GetBuilding();
		if (building == null || !building.HasDecayEntities())
		{
			return;
		}
		float num = Mathf.Min(GetProtectedMinutes(force: true) * 60f, deltaTime);
		if (!(num > 0f))
		{
			return;
		}
		foreach (DecayEntity decayEntity in building.decayEntities)
		{
			float protectedSeconds = decayEntity.GetProtectedSeconds();
			if (num > protectedSeconds)
			{
				float time = PurchaseUpkeepTime(decayEntity, num - protectedSeconds);
				decayEntity.AddUpkeepTime(time);
				if (IsDebugging())
				{
					Debug.Log(ToString() + " purchased upkeep time for " + decayEntity.ToString() + ": " + protectedSeconds + " + " + time + " = " + decayEntity.GetProtectedSeconds());
				}
			}
		}
	}

	public void PurchaseAntiGriefTime(float deltaTime)
	{
		BuildingManager.Building building = GetBuilding();
		if (building == null || !building.HasDecayEntities())
		{
			return;
		}
		foreach (DecayEntity decayEntity in building.decayEntities)
		{
			float protectedSeconds = decayEntity.GetProtectedSeconds();
			float num = Mathf.Max(0f, deltaTime - protectedSeconds);
			if (num > 0f)
			{
				float time = PurchaseUpkeepTime(decayEntity, num);
				decayEntity.AddUpkeepTime(time);
				if (IsDebugging())
				{
					Debug.Log(ToString() + " purchased upkeep time for " + decayEntity.ToString() + ": " + protectedSeconds + " + " + time + " = " + decayEntity.GetProtectedSeconds());
				}
			}
		}
	}

	public static string FormatUpkeepMinutes(float minutes)
	{
		int num = Mathf.FloorToInt(minutes / 60f);
		int num2 = Mathf.FloorToInt(minutes - (float)num * 60f);
		int num3 = Mathf.FloorToInt(minutes * 60f % 60f);
		if (num >= 72)
		{
			string text = Translate.Get("days", "days");
			int num4 = num / 24;
			if (num4 >= 30)
			{
				return "> 30 " + text;
			}
			return $"{num4:N0} {text}";
		}
		if (num >= 12)
		{
			return $"{num:N0} hrs";
		}
		if (num >= 1)
		{
			return $"{num:N0}h{num2:N0}m";
		}
		if (minutes >= 1f)
		{
			return $"{num2:N0}m{num3:N0}s";
		}
		return $"{minutes * 60f:N0}s";
	}
}
