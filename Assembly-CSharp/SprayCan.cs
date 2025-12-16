#define UNITY_ASSERTIONS
using System;
using System.Collections.Generic;
using System.Linq;
using ConVar;
using Facepunch;
using Facepunch.Rust;
using Network;
using Oxide.Core;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

public class SprayCan : HeldEntity
{
	private enum SprayFailReason
	{
		None,
		MountedBlocked,
		IOConnection,
		LineOfSight,
		SkinNotOwned,
		InvalidItem
	}

	private struct ContainerSet
	{
		public int ContainerIndex;

		public uint PrefabId;
	}

	public struct IOPreserveInfo
	{
		public IOEntity connectedTo;

		public int connectedToSlot;

		public Vector3[] linePoints;

		public float[] slackLevels;

		public IOEntity.LineAnchor[] lineAnchors;

		public Vector3 worldSpaceLineEndRotation;

		public Vector3 originPosition;

		public Vector3 originRotation;

		public WireTool.WireColour wireColour;
	}

	private struct CodeLockPreserveInfo
	{
		public string code;

		public string guestCode;

		public bool isLocked;

		public List<ulong> whitelistPlayers;

		public List<ulong> guestPlayers;
	}

	private struct OtherEntityPreserveInfo
	{
		public IOPreserveInfo info;

		public IOEntity connectedEntity;

		public int index;

		public bool isOutput;
	}

	public struct ChildPreserveInfo
	{
		public BaseEntity TargetEntity;

		public uint TargetBone;

		public Vector3 LocalPosition;

		public Quaternion LocalRotation;
	}

	public const float MaxFreeSprayDistanceFromStart = 10f;

	public const float MaxFreeSprayStartingDistance = 3f;

	private SprayCanSpray_Freehand paintingLine;

	public const Flags IsFreeSpraying = Flags.Reserved1;

	public SoundDefinition SpraySound;

	public GameObjectRef SkinSelectPanel;

	public float SprayCooldown = 2f;

	public float ConditionLossPerSpray = 10f;

	public float ConditionLossPerReskin = 10f;

	public GameObjectRef LinePrefab;

	public Color[] SprayColours = new Color[0];

	public float[] SprayWidths = new float[3] { 0.1f, 0.2f, 0.3f };

	public ParticleSystem worldSpaceSprayFx;

	public GameObjectRef ReskinEffect;

	public ItemDefinition SprayDecalItem;

	public GameObjectRef SprayDecalEntityRef;

	public SteamInventoryItem FreeSprayUnlockItem;

	public ParticleSystem.MinMaxGradient DecalSprayGradient;

	public SoundDefinition SprayLoopDef;

	public static Translate.Phrase FreeSprayNamePhrase = new Translate.Phrase("freespray_radial", "Free Spray");

	public static Translate.Phrase FreeSprayDescPhrase = new Translate.Phrase("freespray_radial_desc", "Spray shapes freely with various colors");

	public static Translate.Phrase BuildingSkinColourPhrase = new Translate.Phrase("buildingskin_colour", "Set colour");

	public static Translate.Phrase BuildingSkinColourDescPhrase = new Translate.Phrase("buildingskin_colour_desc", "Set the block to the highlighted colour");

	public static readonly Translate.Phrase DoorMustBeClosed = new Translate.Phrase("error_doormustbeclosed", "Door must be closed");

	public static readonly Translate.Phrase NeedDoorAccess = new Translate.Phrase("error_needdooraccess", "Need door access");

	public static readonly Translate.Phrase CannotReskinThatDoor = new Translate.Phrase("error_cannotreskindoor", "Cannot reskin that door");

	public static readonly Translate.Phrase RecentlyDamaged = new Translate.Phrase("error_reskin_recentlydamaged", "Cannot reskin an object that was recently damaged");

	public static readonly Translate.Phrase ExplosivesActive = new Translate.Phrase("error_explosivesactive", "Cannot reskin an object with explosives attached");

	public static readonly Translate.Phrase PlayerInAir = new Translate.Phrase("error_playerinair", "You must be on the ground");

	public static readonly Translate.Phrase BlockedByPlayer = new Translate.Phrase("error_blockedbyplayer_reskin", "Blocked by intersecting player");

	public static readonly Translate.Phrase BlockedBySomething = new Translate.Phrase("error_blockedbysomething", "Blocked by something");

	public static readonly Translate.Phrase PlayerIsMounted = new Translate.Phrase("error_playerismounted", "Player {0} is mounted");

	[FormerlySerializedAs("ShippingCOntainerColourLookup")]
	public ConstructionSkin_ColourLookup ShippingContainerColourLookup;

	public const string ENEMY_BASE_STAT = "sprayed_enemy_base";

	private Translate.Phrase lastSprayError;

	public override bool OnRpcMessage(BasePlayer player, uint rpc, Message msg)
	{
		using (TimeWarning.New("SprayCan.OnRpcMessage"))
		{
			if (rpc == 3490735573u && player != null)
			{
				Assert.IsTrue(player.isServer, "SV_RPC Message is using a clientside player!");
				if (Global.developer > 2)
				{
					Debug.Log("SV_RPCMessage: " + player?.ToString() + " - BeginFreehandSpray ");
				}
				using (TimeWarning.New("BeginFreehandSpray"))
				{
					using (TimeWarning.New("Conditions"))
					{
						if (!RPC_Server.IsActiveItem.Test(3490735573u, "BeginFreehandSpray", this, player))
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
							BeginFreehandSpray(msg2);
						}
					}
					catch (Exception exception)
					{
						Debug.LogException(exception);
						player.Kick("RPC Error in BeginFreehandSpray");
					}
				}
				return true;
			}
			if (rpc == 151738090 && player != null)
			{
				Assert.IsTrue(player.isServer, "SV_RPC Message is using a clientside player!");
				if (Global.developer > 2)
				{
					Debug.Log("SV_RPCMessage: " + player?.ToString() + " - ChangeItemSkin ");
				}
				using (TimeWarning.New("ChangeItemSkin"))
				{
					using (TimeWarning.New("Conditions"))
					{
						if (!RPC_Server.CallsPerSecond.Test(151738090u, "ChangeItemSkin", this, player, 2uL))
						{
							return true;
						}
						if (!RPC_Server.IsActiveItem.Test(151738090u, "ChangeItemSkin", this, player))
						{
							return true;
						}
					}
					try
					{
						using (TimeWarning.New("Call"))
						{
							RPCMessage msg3 = new RPCMessage
							{
								connection = msg.connection,
								player = player,
								read = msg.read
							};
							ChangeItemSkin(msg3);
						}
					}
					catch (Exception exception2)
					{
						Debug.LogException(exception2);
						player.Kick("RPC Error in ChangeItemSkin");
					}
				}
				return true;
			}
			if (rpc == 688080035 && player != null)
			{
				Assert.IsTrue(player.isServer, "SV_RPC Message is using a clientside player!");
				if (Global.developer > 2)
				{
					Debug.Log("SV_RPCMessage: " + player?.ToString() + " - ChangeWallpaper ");
				}
				using (TimeWarning.New("ChangeWallpaper"))
				{
					using (TimeWarning.New("Conditions"))
					{
						if (!RPC_Server.CallsPerSecond.Test(688080035u, "ChangeWallpaper", this, player, 2uL))
						{
							return true;
						}
						if (!RPC_Server.IsActiveItem.Test(688080035u, "ChangeWallpaper", this, player))
						{
							return true;
						}
						if (!RPC_Server.MaxDistance.Test(688080035u, "ChangeWallpaper", this, player, 5f))
						{
							return true;
						}
					}
					try
					{
						using (TimeWarning.New("Call"))
						{
							RPCMessage msg4 = new RPCMessage
							{
								connection = msg.connection,
								player = player,
								read = msg.read
							};
							ChangeWallpaper(msg4);
						}
					}
					catch (Exception exception3)
					{
						Debug.LogException(exception3);
						player.Kick("RPC Error in ChangeWallpaper");
					}
				}
				return true;
			}
			if (rpc == 396000799 && player != null)
			{
				Assert.IsTrue(player.isServer, "SV_RPC Message is using a clientside player!");
				if (Global.developer > 2)
				{
					Debug.Log("SV_RPCMessage: " + player?.ToString() + " - CreateSpray ");
				}
				using (TimeWarning.New("CreateSpray"))
				{
					using (TimeWarning.New("Conditions"))
					{
						if (!RPC_Server.IsActiveItem.Test(396000799u, "CreateSpray", this, player))
						{
							return true;
						}
					}
					try
					{
						using (TimeWarning.New("Call"))
						{
							RPCMessage msg5 = new RPCMessage
							{
								connection = msg.connection,
								player = player,
								read = msg.read
							};
							CreateSpray(msg5);
						}
					}
					catch (Exception exception4)
					{
						Debug.LogException(exception4);
						player.Kick("RPC Error in CreateSpray");
					}
				}
				return true;
			}
			if (rpc == 14517645 && player != null)
			{
				Assert.IsTrue(player.isServer, "SV_RPC Message is using a clientside player!");
				if (Global.developer > 2)
				{
					Debug.Log("SV_RPCMessage: " + player?.ToString() + " - Server_SetBlockColourId ");
				}
				using (TimeWarning.New("Server_SetBlockColourId"))
				{
					using (TimeWarning.New("Conditions"))
					{
						if (!RPC_Server.CallsPerSecond.Test(14517645u, "Server_SetBlockColourId", this, player, 3uL))
						{
							return true;
						}
						if (!RPC_Server.IsActiveItem.Test(14517645u, "Server_SetBlockColourId", this, player))
						{
							return true;
						}
					}
					try
					{
						using (TimeWarning.New("Call"))
						{
							RPCMessage msg6 = new RPCMessage
							{
								connection = msg.connection,
								player = player,
								read = msg.read
							};
							Server_SetBlockColourId(msg6);
						}
					}
					catch (Exception exception5)
					{
						Debug.LogException(exception5);
						player.Kick("RPC Error in Server_SetBlockColourId");
					}
				}
				return true;
			}
		}
		return base.OnRpcMessage(player, rpc, msg);
	}

	[RPC_Server]
	[RPC_Server.IsActiveItem]
	private void BeginFreehandSpray(RPCMessage msg)
	{
		if (!IsBusy() && CanSprayFreehand(msg.player))
		{
			Vector3 vector = msg.read.Vector3();
			Vector3 atNormal = msg.read.Vector3();
			int num = msg.read.Int32();
			int num2 = msg.read.Int32();
			if (num >= 0 && num < SprayColours.Length && num2 >= 0 && num2 < SprayWidths.Length && !(Vector3.Distance(vector, GetOwnerPlayer().transform.position) > 3f))
			{
				SprayCanSpray_Freehand sprayCanSpray_Freehand = GameManager.server.CreateEntity(LinePrefab.resourcePath, vector, Quaternion.identity) as SprayCanSpray_Freehand;
				sprayCanSpray_Freehand.AddInitialPoint(atNormal);
				sprayCanSpray_Freehand.SetColour(SprayColours[num]);
				sprayCanSpray_Freehand.SetWidth(SprayWidths[num2]);
				sprayCanSpray_Freehand.EnableChanges(msg.player);
				sprayCanSpray_Freehand.Spawn();
				paintingLine = sprayCanSpray_Freehand;
				ClientRPC(RpcTarget.NetworkGroup("Client_ChangeSprayColour"), num);
				SetFlag(Flags.Busy, b: true);
				SetFlag(Flags.Reserved1, b: true);
				CheckAchievementPosition(vector);
			}
		}
	}

	public void ClearPaintingLine(bool allowNewSprayImmediately)
	{
		paintingLine = null;
		if (!base.UsingInfiniteAmmoCheat)
		{
			LoseCondition(ConditionLossPerSpray);
		}
		if (allowNewSprayImmediately)
		{
			ClearBusy();
		}
		else
		{
			Invoke(ClearBusy, 0.1f);
		}
	}

	public bool CanSprayFreehand(BasePlayer player)
	{
		if (!player.DefaultSkinAccess)
		{
			return player.AllSkinsUnlocked;
		}
		if (FreeSprayUnlockItem != null)
		{
			if (!player.blueprints.steamInventory.HasItem(FreeSprayUnlockItem.id))
			{
				return FreeSprayUnlockItem.HasUnlocked(player.userID);
			}
			return true;
		}
		return false;
	}

	private bool IsSprayBlockedByTrigger(Vector3 pos)
	{
		BasePlayer ownerPlayer = GetOwnerPlayer();
		if (ownerPlayer == null)
		{
			return true;
		}
		TriggerNoSpray triggerNoSpray = ownerPlayer.FindTrigger<TriggerNoSpray>();
		if (triggerNoSpray == null)
		{
			return false;
		}
		return !triggerNoSpray.IsPositionValid(pos);
	}

	private bool ValidateEntityAndSkin(BasePlayer player, BaseNetworkable targetEnt, int targetSkin)
	{
		if (IsBusy())
		{
			return false;
		}
		if (player == null || !player.CanBuild())
		{
			return false;
		}
		if (!player.IsOnGround())
		{
			player.ShowToast(GameTip.Styles.Error, PlayerInAir, false);
			return false;
		}
		if (player.AllSkinsLocked)
		{
			return false;
		}
		bool allSkinsUnlocked = player.AllSkinsUnlocked;
		if (targetSkin != 0 && !allSkinsUnlocked && !player.blueprints.CheckSkinOwnership(targetSkin, player.userID))
		{
			SprayFailResponse(SprayFailReason.SkinNotOwned);
			return false;
		}
		if (targetEnt != null && targetEnt is BaseEntity baseEntity)
		{
			Vector3 position = baseEntity.WorldSpaceBounds().ClosestPoint(player.eyes.position);
			if (!player.IsVisible(position, 3f))
			{
				SprayFailResponse(SprayFailReason.LineOfSight);
				return false;
			}
			if (targetEnt is Door door)
			{
				if (!door.GetPlayerLockPermission(player))
				{
					player.ShowToast(GameTip.Styles.Error, NeedDoorAccess, false);
					return false;
				}
				if (door.IsOpen())
				{
					player.ShowToast(GameTip.Styles.Error, DoorMustBeClosed, false);
					return false;
				}
				if (door.GetParentEntity() != null && door.GetParentEntity() is HotAirBalloonArmor)
				{
					player.ShowToast(GameTip.Styles.Error, CannotReskinThatDoor, false);
					return false;
				}
			}
			if (targetEnt is BaseCombatEntity { SecondsSinceAttacked: <30f } baseCombatEntity)
			{
				player.ShowToast(GameTip.Styles.Error, RecentlyDamaged, false, (30f - baseCombatEntity.SecondsSinceAttacked).ToString("N0"));
				return false;
			}
			foreach (BaseEntity child in targetEnt.children)
			{
				if (child is TimedExplosive)
				{
					player.ShowToast(GameTip.Styles.Error, ExplosivesActive, false);
					return false;
				}
			}
			if (targetEnt is SimpleBuildingBlock || targetEnt is Gate)
			{
				using PooledList<BaseEntity> pooledList = Facepunch.Pool.Get<PooledList<BaseEntity>>();
				Vis.Entities(baseEntity.WorldSpaceBounds(), pooledList, -2145386240);
				foreach (BaseEntity item in pooledList)
				{
					if (!(item == null) && !item.isClient && !(item == baseEntity) && !(item is BuildingBlock) && !(item is SimpleBuildingBlock) && !(item is Door) && !(item is BaseOven) && !(item is Barricade))
					{
						player.ShowBlockedByEntityToast(item, BlockedBySomething);
						return false;
					}
				}
			}
		}
		if (targetEnt != null && targetEnt is BaseLock baseLock)
		{
			return baseLock.GetPlayerLockPermission(player);
		}
		return true;
	}

	private bool ValidateWallpaperReskin(BasePlayer player, BuildingBlock block, int side, int targetSkin)
	{
		if (player == null || !player.CanBuild())
		{
			return false;
		}
		if (!player.IsOnGround())
		{
			player.ShowToast(GameTip.Styles.Error, PlayerInAir, false);
			return false;
		}
		if (player.AllSkinsLocked)
		{
			return false;
		}
		bool allSkinsUnlocked = player.AllSkinsUnlocked;
		if (targetSkin != 0 && !allSkinsUnlocked && !player.blueprints.CheckSkinOwnership(targetSkin, player.userID))
		{
			SprayFailResponse(SprayFailReason.SkinNotOwned);
			return false;
		}
		if (!block.HasWallpaper(side))
		{
			return false;
		}
		if (!block.CanSeeWallpaperSocket(player, side))
		{
			return false;
		}
		return true;
	}

	private void SprayFailResponse(SprayFailReason reason)
	{
		ClientRPC(RpcTarget.NetworkGroup("Client_ReskinResult"), 0, (int)reason);
	}

	[RPC_Server.CallsPerSecond(2uL)]
	[RPC_Server.IsActiveItem]
	[RPC_Server]
	private void ChangeItemSkin(RPCMessage msg)
	{
		NetworkableId uid = msg.read.EntityID();
		int targetSkin = msg.read.Int32();
		BaseNetworkable baseNetworkable = BaseNetworkable.serverEntities.Find(uid);
		if (!ValidateEntityAndSkin(msg.player, baseNetworkable, targetSkin))
		{
			return;
		}
		if (baseNetworkable != null)
		{
			BaseEntity baseEntity = baseNetworkable as BaseEntity;
			if ((object)baseEntity != null)
			{
				if (!GetItemDefinitionForEntity(baseEntity, out var def, useRedirect: false))
				{
					FailResponse(SprayFailReason.InvalidItem);
					return;
				}
				ItemDefinition itemDefinition = null;
				ulong num = ItemDefinition.FindSkin((def.isRedirectOf != null) ? def.isRedirectOf.itemid : def.itemid, targetSkin);
				ItemSkinDirectory.Skin skin = ((def.isRedirectOf != null) ? def.isRedirectOf : def).skins.FirstOrDefault((ItemSkinDirectory.Skin x) => x.id == targetSkin);
				if (Interface.CallHook("OnEntityReskin", baseEntity, skin, msg.player) != null)
				{
					return;
				}
				if (skin.invItem != null && skin.invItem is ItemSkin itemSkin)
				{
					if (itemSkin.Redirect != null)
					{
						itemDefinition = itemSkin.Redirect;
					}
					else if ((bool)def && def.isRedirectOf != null)
					{
						itemDefinition = def.isRedirectOf;
					}
				}
				else if (def.isRedirectOf != null || ((bool)def && def.isRedirectOf != null))
				{
					itemDefinition = def.isRedirectOf;
				}
				if (itemDefinition == null)
				{
					baseEntity.skinID = num;
					baseEntity.SendNetworkUpdate();
					Facepunch.Rust.Analytics.Azure.OnEntitySkinChanged(msg.player, baseNetworkable, targetSkin);
				}
				else
				{
					if (!CanEntityBeRespawned(baseEntity, out var reason, out var culpritPlayer))
					{
						if (reason == SprayFailReason.MountedBlocked)
						{
							lastSprayError = string.Format(PlayerIsMounted.translated, NameHelper.GetPlayerNameStreamSafe(msg.player, culpritPlayer));
							msg.player.ShowToast(GameTip.Styles.Error, lastSprayError, false);
						}
						FailResponse(reason);
						return;
					}
					if (!GetEntityPrefabPath(itemDefinition, out var resourcePath))
					{
						Debug.LogWarning("Cannot find resource path of redirect entity to spawn! " + itemDefinition.gameObject.name);
						FailResponse(SprayFailReason.InvalidItem);
						return;
					}
					if (global::SimpleUpgrade.IsUpgradeBlocked(baseEntity, itemDefinition, msg.player))
					{
						msg.player.ShowToast(GameTip.Styles.Error, BlockedByPlayer, false);
						return;
					}
					CodeLockPreserveInfo codeLockPreserveInfo = default(CodeLockPreserveInfo);
					if (baseEntity is CodeLock codeLock)
					{
						codeLockPreserveInfo.code = codeLock.code;
						codeLockPreserveInfo.guestCode = codeLock.guestCode;
						codeLockPreserveInfo.isLocked = codeLock.IsLocked();
						codeLockPreserveInfo.whitelistPlayers = Facepunch.Pool.Get<List<ulong>>();
						codeLockPreserveInfo.guestPlayers = Facepunch.Pool.Get<List<ulong>>();
						codeLockPreserveInfo.whitelistPlayers.AddRange(codeLock.whitelistPlayers);
						codeLockPreserveInfo.guestPlayers.AddRange(codeLock.guestPlayers);
					}
					Vector3 localPosition = baseEntity.transform.localPosition;
					Quaternion localRotation = baseEntity.transform.localRotation;
					BaseEntity baseEntity2 = baseEntity.GetParentEntity();
					float health = baseEntity.Health();
					EntityRef[] slots = baseEntity.GetSlots();
					ulong ownerID = baseEntity.OwnerID;
					float lastAttackedTime = ((baseEntity is BaseCombatEntity baseCombatEntity) ? baseCombatEntity.lastAttackedTime : 0f);
					int soilSaturation = ((baseEntity is PlanterBox planterBox) ? planterBox.soilSaturation : 0);
					bool flag = baseEntity is DecayEntity decayEntity && decayEntity.HasFlag(Flags.Reserved2);
					HashSet<ulong> hashSet = null;
					if (baseEntity is BuildingPrivlidge buildingPrivlidge)
					{
						hashSet = new HashSet<ulong>(buildingPrivlidge.authorizedPlayers);
					}
					bool flag2 = baseEntity is Door || baseEntity is BuildingPrivlidge || baseEntity is BoxStorage || baseEntity is PlanterBox;
					Dictionary<ContainerSet, List<Item>> dictionary = new Dictionary<ContainerSet, List<Item>>();
					SaveEntityStorage(baseEntity, dictionary, 0);
					List<ChildPreserveInfo> obj = Facepunch.Pool.Get<List<ChildPreserveInfo>>();
					if (flag2)
					{
						foreach (BaseEntity child in baseEntity.children)
						{
							obj.Add(new ChildPreserveInfo
							{
								TargetEntity = child,
								TargetBone = child.parentBone,
								LocalPosition = child.transform.localPosition,
								LocalRotation = child.transform.localRotation
							});
						}
						foreach (ChildPreserveInfo item in obj)
						{
							item.TargetEntity.SetParent(null, worldPositionStays: true);
						}
					}
					else
					{
						for (int num2 = 0; num2 < baseEntity.children.Count; num2++)
						{
							SaveEntityStorage(baseEntity.children[num2], dictionary, -1);
						}
					}
					IOPreserveInfo[] array = null;
					IOPreserveInfo[] array2 = null;
					List<OtherEntityPreserveInfo> list = new List<OtherEntityPreserveInfo>();
					if (baseEntity is IOEntity iOEntity)
					{
						array = new IOPreserveInfo[iOEntity.outputs.Length];
						for (int num3 = 0; num3 < iOEntity.outputs.Length; num3++)
						{
							IOEntity.IOSlot iOSlot = iOEntity.outputs[num3];
							IOEntity iOEntity2 = iOSlot.connectedTo.Get();
							if (iOEntity2 != null)
							{
								iOSlot.Preserve(ref array[num3]);
								IOPreserveInfo target = default(IOPreserveInfo);
								iOEntity2.inputs[iOSlot.connectedToSlot].Preserve(ref target);
								list.Add(new OtherEntityPreserveInfo
								{
									info = target,
									connectedEntity = iOEntity2,
									index = iOSlot.connectedToSlot,
									isOutput = false
								});
							}
						}
						array2 = new IOPreserveInfo[iOEntity.inputs.Length];
						for (int num4 = 0; num4 < iOEntity.inputs.Length; num4++)
						{
							IOEntity.IOSlot iOSlot2 = iOEntity.inputs[num4];
							IOEntity iOEntity3 = iOSlot2.connectedTo.Get();
							if (iOEntity3 != null)
							{
								iOSlot2.Preserve(ref array2[num4]);
								IOPreserveInfo target2 = default(IOPreserveInfo);
								iOEntity3.outputs[iOSlot2.connectedToSlot].Preserve(ref target2);
								list.Add(new OtherEntityPreserveInfo
								{
									info = target2,
									connectedEntity = iOEntity3,
									index = iOSlot2.connectedToSlot,
									isOutput = true
								});
							}
						}
					}
					baseEntity.Kill();
					baseEntity = GameManager.server.CreateEntity(resourcePath, (baseEntity2 != null) ? baseEntity2.transform.TransformPoint(localPosition) : localPosition, (baseEntity2 != null) ? (baseEntity2.transform.rotation * localRotation) : localRotation);
					baseEntity.SetParent(baseEntity2);
					baseEntity.transform.localPosition = localPosition;
					baseEntity.transform.localRotation = localRotation;
					baseEntity.OwnerID = ownerID;
					if (GetItemDefinitionForEntity(baseEntity, out var def2, useRedirect: false) && def2.isRedirectOf != null)
					{
						baseEntity.skinID = 0uL;
					}
					else
					{
						baseEntity.skinID = num;
					}
					if (baseEntity is DecayEntity decayEntity2)
					{
						decayEntity2.AttachToBuilding(null);
					}
					if (baseEntity is PlanterBox planterBox2)
					{
						planterBox2.soilSaturation = soilSaturation;
					}
					baseEntity.Spawn();
					if (baseEntity is IOEntity iOEntity4)
					{
						if (array != null)
						{
							for (int num5 = 0; num5 < iOEntity4.outputs.Length; num5++)
							{
								iOEntity4.outputs[num5].Restore(array[num5]);
							}
						}
						if (array2 != null)
						{
							for (int num6 = 0; num6 < iOEntity4.inputs.Length; num6++)
							{
								if (array2[num6].connectedTo != null)
								{
									iOEntity4.inputs[num6].Restore(array2[num6]);
								}
							}
						}
						using PooledList<IOEntity> pooledList = Facepunch.Pool.Get<PooledList<IOEntity>>();
						foreach (OtherEntityPreserveInfo item2 in list)
						{
							IOPreserveInfo info = item2.info;
							info.connectedTo = iOEntity4;
							if (item2.connectedEntity != null)
							{
								if (item2.isOutput)
								{
									item2.connectedEntity.outputs[item2.index].Restore(info);
									pooledList.Add(item2.connectedEntity);
								}
								else
								{
									item2.connectedEntity.inputs[item2.index].Restore(info);
									pooledList.Add(item2.connectedEntity);
								}
							}
						}
						foreach (IOEntity item3 in pooledList)
						{
							item3.SendNetworkUpdate();
						}
					}
					if (baseEntity is BaseCombatEntity baseCombatEntity2)
					{
						baseCombatEntity2.SetHealth(health);
						baseCombatEntity2.lastAttackedTime = lastAttackedTime;
					}
					if (baseEntity is BuildingPrivlidge buildingPrivlidge2 && hashSet != null)
					{
						buildingPrivlidge2.authorizedPlayers = hashSet;
					}
					if (baseEntity is CodeLock codeLock2)
					{
						baseEntity2.SetSlot(Slot.Lock, codeLock2);
						codeLock2.SetParent(baseEntity2, baseEntity2.GetSlotAnchorName(Slot.Lock));
						codeLock2.code = codeLockPreserveInfo.code;
						codeLock2.guestCode = codeLockPreserveInfo.guestCode;
						codeLock2.SetFlag(Flags.Locked, codeLockPreserveInfo.isLocked);
						codeLock2.whitelistPlayers.AddRange(codeLockPreserveInfo.whitelistPlayers);
						codeLock2.guestPlayers.AddRange(codeLockPreserveInfo.guestPlayers);
						Facepunch.Pool.FreeUnmanaged(ref codeLockPreserveInfo.whitelistPlayers);
						Facepunch.Pool.FreeUnmanaged(ref codeLockPreserveInfo.guestPlayers);
					}
					if (dictionary.Count > 0)
					{
						RestoreEntityStorage(baseEntity, 0, dictionary);
						if (!flag2)
						{
							for (int num7 = 0; num7 < baseEntity.children.Count; num7++)
							{
								RestoreEntityStorage(baseEntity.children[num7], -1, dictionary);
							}
						}
						foreach (KeyValuePair<ContainerSet, List<Item>> item4 in dictionary)
						{
							foreach (Item item5 in item4.Value)
							{
								Debug.Log($"Deleting {item5} as it has no new container");
								item5.Remove();
							}
						}
						Facepunch.Rust.Analytics.Azure.OnEntitySkinChanged(msg.player, baseNetworkable, targetSkin);
					}
					if (flag2)
					{
						foreach (ChildPreserveInfo item6 in obj)
						{
							item6.TargetEntity.SetParent(baseEntity, item6.TargetBone, worldPositionStays: true);
							item6.TargetEntity.transform.localPosition = item6.LocalPosition;
							item6.TargetEntity.transform.localRotation = item6.LocalRotation;
							item6.TargetEntity.SendNetworkUpdate();
						}
						baseEntity.SetSlots(slots);
					}
					Facepunch.Pool.FreeUnmanaged(ref obj);
					if (baseEntity is ISprayCallback sprayCallback)
					{
						sprayCallback.OnReskinned(msg.player);
					}
					if (baseEntity is DecayEntity decayEntity3 && !flag)
					{
						decayEntity3.StopBeingDemolishable();
					}
				}
				Interface.CallHook("OnEntityReskinned", baseEntity, skin, msg.player);
				ClientRPC(RpcTarget.NetworkGroup("Client_ReskinResult"), 1, baseEntity.net.ID);
			}
		}
		if (!base.UsingInfiniteAmmoCheat)
		{
			LoseCondition(ConditionLossPerReskin);
		}
		ClientRPC(RpcTarget.NetworkGroup("Client_ChangeSprayColour"), -1);
		SetFlag(Flags.Busy, b: true);
		Invoke(ClearBusy, SprayCooldown);
		void FailResponse(SprayFailReason arg)
		{
			ClientRPC(RpcTarget.NetworkGroup("Client_ReskinResult"), 0, (int)arg);
		}
		static void RestoreEntityStorage(BaseEntity baseEntity3, int index, Dictionary<ContainerSet, List<Item>> copy)
		{
			if (baseEntity3 is IItemContainerEntity itemContainerEntity)
			{
				ContainerSet key = new ContainerSet
				{
					ContainerIndex = index,
					PrefabId = ((index != 0) ? baseEntity3.prefabID : 0u)
				};
				if (copy.ContainsKey(key))
				{
					foreach (Item item7 in copy[key])
					{
						item7.MoveToContainer(itemContainerEntity.inventory);
					}
					copy.Remove(key);
				}
			}
		}
		static void SaveEntityStorage(BaseEntity baseEntity3, Dictionary<ContainerSet, List<Item>> dictionary2, int index)
		{
			if (baseEntity3 is IItemContainerEntity itemContainerEntity)
			{
				ContainerSet key = new ContainerSet
				{
					ContainerIndex = index,
					PrefabId = ((index != 0) ? baseEntity3.prefabID : 0u)
				};
				if (!dictionary2.ContainsKey(key))
				{
					dictionary2.Add(key, new List<Item>());
					foreach (Item item8 in itemContainerEntity.inventory.itemList)
					{
						dictionary2[key].Add(item8);
					}
					{
						foreach (Item item9 in dictionary2[key])
						{
							item9.RemoveFromContainer();
						}
						return;
					}
				}
				Debug.Log("Multiple containers with the same prefab id being added during vehicle reskin");
			}
		}
	}

	[RPC_Server.CallsPerSecond(2uL)]
	[RPC_Server]
	[RPC_Server.MaxDistance(5f)]
	[RPC_Server.IsActiveItem]
	private void ChangeWallpaper(RPCMessage msg)
	{
		NetworkableId uid = msg.read.EntityID();
		int targetSkin = msg.read.Int32();
		int side = ((!msg.read.Bool()) ? 1 : 0);
		BaseNetworkable baseNetworkable = BaseNetworkable.serverEntities.Find(uid);
		if (!(baseNetworkable is BuildingBlock buildingBlock) || !buildingBlock.HasWallpaper(side))
		{
			SprayFailResponse(SprayFailReason.InvalidItem);
		}
		else if (ValidateWallpaperReskin(msg.player, baseNetworkable as BuildingBlock, side, targetSkin))
		{
			ulong id = ItemDefinition.FindSkin(WallpaperSettings.GetItemDefForCategory(WallpaperPlanner.Settings.GetCategory(buildingBlock, side)).itemid, targetSkin);
			buildingBlock.SetWallpaper(id, side);
			Facepunch.Rust.Analytics.Azure.OnWallpaperPlaced(msg.player, buildingBlock, id, side, reskin: true);
			ClientRPC(RpcTarget.NetworkGroup("Client_ReskinResult"), 1, buildingBlock.net.ID);
			SetFlag(Flags.Busy, b: true);
			Invoke(ClearBusy, SprayCooldown);
		}
	}

	private bool GetEntityPrefabPath(ItemDefinition def, out string resourcePath)
	{
		resourcePath = string.Empty;
		if (def.TryGetComponent<ItemModDeployable>(out var component))
		{
			resourcePath = component.entityPrefab.resourcePath;
			return true;
		}
		if (def.TryGetComponent<ItemModEntity>(out var component2))
		{
			resourcePath = component2.entityPrefab.resourcePath;
			return true;
		}
		if (def.TryGetComponent<ItemModEntityReference>(out var component3))
		{
			resourcePath = component3.entityPrefab.resourcePath;
			return true;
		}
		return false;
	}

	[RPC_Server]
	[RPC_Server.IsActiveItem]
	private void CreateSpray(RPCMessage msg)
	{
		if (IsBusy())
		{
			return;
		}
		ClientRPC(RpcTarget.NetworkGroup("Client_ChangeSprayColour"), -1);
		SetFlag(Flags.Busy, b: true);
		Invoke(ClearBusy, SprayCooldown);
		Vector3 vector = msg.read.Vector3();
		Vector3 vector2 = msg.read.Vector3();
		Vector3 point = msg.read.Vector3();
		int num = msg.read.Int32();
		if (Vector3.Distance(vector, base.transform.position) > 4.5f)
		{
			return;
		}
		Quaternion quaternion = Quaternion.LookRotation((new Plane(vector2, vector).ClosestPointOnPlane(point) - vector).normalized, vector2);
		quaternion *= Quaternion.Euler(0f, 0f, 90f);
		bool flag = false;
		if (msg.player.IsDeveloper)
		{
			flag = true;
		}
		if (num != 0 && !flag && !msg.player.blueprints.CheckSkinOwnership(num, msg.player.userID))
		{
			Debug.Log($"SprayCan.ChangeItemSkin player does not have item :{num}:");
		}
		else if (Interface.CallHook("OnSprayCreate", this, vector, quaternion) == null)
		{
			ulong num2 = ItemDefinition.FindSkin(SprayDecalItem.itemid, num);
			BaseEntity baseEntity = GameManager.server.CreateEntity(SprayDecalEntityRef.resourcePath, vector, quaternion);
			baseEntity.skinID = num2;
			baseEntity.OnDeployed(null, GetOwnerPlayer(), GetItem());
			baseEntity.Spawn();
			CheckAchievementPosition(vector);
			if (!base.UsingInfiniteAmmoCheat)
			{
				LoseCondition(ConditionLossPerSpray);
			}
		}
	}

	private void CheckAchievementPosition(Vector3 pos)
	{
	}

	private void LoseCondition(float amount)
	{
		GetOwnerItem()?.LoseCondition(amount);
	}

	public void ClearBusy()
	{
		SetFlag(Flags.Busy, b: false);
		SetFlag(Flags.Reserved1, b: false);
	}

	public override void OnHeldChanged()
	{
		if (IsDisabled())
		{
			ClearBusy();
			if (paintingLine != null)
			{
				paintingLine.Kill();
			}
			paintingLine = null;
		}
	}

	[RPC_Server.CallsPerSecond(3uL)]
	[RPC_Server]
	[RPC_Server.IsActiveItem]
	private void Server_SetBlockColourId(RPCMessage msg)
	{
		NetworkableId uid = msg.read.EntityID();
		uint num = msg.read.UInt32();
		BasePlayer player = msg.player;
		SetFlag(Flags.Busy, b: true);
		Invoke(ClearBusy, 0.1f);
		if (!(player == null) && player.CanBuild())
		{
			BasePlayer ownerPlayer = GetOwnerPlayer();
			BuildingBlock buildingBlock = BaseNetworkable.serverEntities.Find(uid) as BuildingBlock;
			if (buildingBlock != null && !(player.Distance(buildingBlock) > 4f))
			{
				uint customColour = buildingBlock.customColour;
				buildingBlock.SetCustomColour(num);
				Facepunch.Rust.Analytics.Azure.OnBuildingBlockColorChanged(ownerPlayer, buildingBlock, customColour, num);
			}
		}
	}

	private bool CanEntityBeRespawned(BaseEntity targetEntity, out SprayFailReason reason, out BasePlayer culpritPlayer)
	{
		if (targetEntity is BaseMountable baseMountable && baseMountable.AnyMounted())
		{
			reason = SprayFailReason.MountedBlocked;
			culpritPlayer = baseMountable.GetMounted();
			return false;
		}
		if (targetEntity.isServer && targetEntity is BaseVehicle baseVehicle && (baseVehicle.HasDriver() || baseVehicle.AnyMounted()))
		{
			reason = SprayFailReason.MountedBlocked;
			culpritPlayer = baseVehicle.GetMounted();
			return false;
		}
		reason = SprayFailReason.None;
		culpritPlayer = null;
		return true;
	}

	public static bool GetItemDefinitionForEntity(BaseEntity be, out ItemDefinition def, bool useRedirect = true)
	{
		def = null;
		if (be is BaseCombatEntity baseCombatEntity)
		{
			if (baseCombatEntity.pickup.enabled && baseCombatEntity.pickup.itemTarget != null)
			{
				def = baseCombatEntity.pickup.itemTarget;
			}
			else if (baseCombatEntity.repair.enabled && baseCombatEntity.repair.itemTarget != null)
			{
				def = baseCombatEntity.repair.itemTarget;
			}
		}
		if (be is CodeLock codeLock)
		{
			def = codeLock.itemType;
		}
		if (useRedirect && def != null && def.isRedirectOf != null)
		{
			def = def.isRedirectOf;
		}
		return def != null;
	}
}
