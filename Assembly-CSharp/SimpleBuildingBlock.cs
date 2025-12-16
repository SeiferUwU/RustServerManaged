#define UNITY_ASSERTIONS
using System;
using System.Collections.Generic;
using System.Linq;
using ConVar;
using Network;
using UnityEngine;
using UnityEngine.Assertions;

public class SimpleBuildingBlock : StabilityEntity, ISimpleUpgradable, ISprayCallback
{
	public List<ItemDefinition> UpgradeItems;

	public Menu.Option UpgradeMenu;

	private GameObject currentModel;

	private SimpleBuildingBlockModelVariant[] variants;

	public override bool OnRpcMessage(BasePlayer player, uint rpc, Message msg)
	{
		using (TimeWarning.New("SimpleBuildingBlock.OnRpcMessage"))
		{
			if (rpc == 2824056853u && player != null)
			{
				Assert.IsTrue(player.isServer, "SV_RPC Message is using a clientside player!");
				if (Global.developer > 2)
				{
					Debug.Log("SV_RPCMessage: " + player?.ToString() + " - DoSimpleUpgrade ");
				}
				using (TimeWarning.New("DoSimpleUpgrade"))
				{
					using (TimeWarning.New("Conditions"))
					{
						if (!RPC_Server.CallsPerSecond.Test(2824056853u, "DoSimpleUpgrade", this, player, 5uL))
						{
							return true;
						}
						if (!RPC_Server.IsVisible.Test(2824056853u, "DoSimpleUpgrade", this, player, 3f))
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
							DoSimpleUpgrade(msg2);
						}
					}
					catch (Exception exception)
					{
						Debug.LogException(exception);
						player.Kick("RPC Error in DoSimpleUpgrade");
					}
				}
				return true;
			}
		}
		return base.OnRpcMessage(player, rpc, msg);
	}

	public override void InitShared()
	{
		base.InitShared();
		variants = PrefabAttribute.server.FindAll<SimpleBuildingBlockModelVariant>(prefabID);
	}

	public List<ItemDefinition> GetUpgradeItems()
	{
		return UpgradeItems;
	}

	public bool CanUpgrade(BasePlayer player, ItemDefinition upgradeItem)
	{
		return global::SimpleUpgrade.CanUpgrade(this, upgradeItem, player);
	}

	public void DoUpgrade(BasePlayer player, ItemDefinition upgradeItem)
	{
		global::SimpleUpgrade.DoUpgrade(this, player, upgradeItem);
	}

	public Menu.Option GetUpgradeMenuOption()
	{
		return UpgradeMenu;
	}

	public bool UpgradingEnabled()
	{
		if (UpgradeItems != null)
		{
			return UpgradeItems.Count > 0;
		}
		return false;
	}

	public bool CostIsItem()
	{
		return true;
	}

	[RPC_Server.IsVisible(3f)]
	[RPC_Server.CallsPerSecond(5uL)]
	[RPC_Server]
	public void DoSimpleUpgrade(RPCMessage msg)
	{
		if (base.SecondsSinceAttacked < 30f)
		{
			msg.player.ShowToast(GameTip.Styles.Error, ConstructionErrors.CantUpgradeRecentlyDamaged, false, (30f - base.SecondsSinceAttacked).ToString("N0"));
			return;
		}
		int num = msg.read.Int32();
		if (num >= 0 && num < UpgradeItems.Count)
		{
			ItemDefinition upgradeItem = UpgradeItems[num];
			if (CanUpgrade(msg.player, upgradeItem))
			{
				DoUpgrade(msg.player, upgradeItem);
			}
		}
	}

	public override void OnDeployed(BaseEntity parent, BasePlayer deployedBy, Item fromItem)
	{
		base.OnDeployed(parent, deployedBy, fromItem);
		PopulateVariants();
	}

	private void PopulateVariants()
	{
		if (base.isServer && variants.Any())
		{
			ulong x = net.ID.Value;
			SeedRandom.Wanghash(ref x);
			SeedRandom.Wanghash(ref x);
			SeedRandom.Wanghash(ref x);
			int num = (int)(x % (ulong)variants.Length);
			SetFlag(variants[num].Flag, b: true);
		}
	}

	public void OnReskinned(BasePlayer byPlayer)
	{
		PopulateVariants();
	}

	public void SetVariant(int index)
	{
		int num = index % variants.Length;
		SimpleBuildingBlockModelVariant[] array = variants;
		foreach (SimpleBuildingBlockModelVariant simpleBuildingBlockModelVariant in array)
		{
			SetFlag(simpleBuildingBlockModelVariant.Flag, b: false, recursive: false, networkupdate: false);
		}
		SetFlag(variants[num].Flag, b: true);
	}

	public override void OnDied(HitInfo info)
	{
		base.OnDied(info);
		if (!base.isServer)
		{
			return;
		}
		SimpleBuildingBlockModelVariant[] array = variants;
		foreach (SimpleBuildingBlockModelVariant simpleBuildingBlockModelVariant in array)
		{
			if (HasFlag(simpleBuildingBlockModelVariant.Flag))
			{
				SetFlag(simpleBuildingBlockModelVariant.Flag, b: false);
			}
		}
	}

	public override void OnFlagsChanged(Flags old, Flags next)
	{
		base.OnFlagsChanged(old, next);
		if (variants != null)
		{
			RefreshVariant();
		}
	}

	private void RefreshVariant()
	{
		if (variants == null)
		{
			return;
		}
		base.gameManager.Retire(currentModel);
		SimpleBuildingBlockModelVariant[] array = variants;
		foreach (SimpleBuildingBlockModelVariant simpleBuildingBlockModelVariant in array)
		{
			if (HasFlag(simpleBuildingBlockModelVariant.Flag))
			{
				GameObject gameObject = base.gameManager.CreatePrefab(simpleBuildingBlockModelVariant.prefab.resourcePath, base.transform);
				if ((bool)gameObject)
				{
					gameObject.transform.localPosition = simpleBuildingBlockModelVariant.localPosition;
					gameObject.transform.localRotation = simpleBuildingBlockModelVariant.localRotation;
				}
				currentModel = gameObject;
			}
		}
	}

	public override void DestroyShared()
	{
		base.DestroyShared();
		base.gameManager.Retire(currentModel);
		currentModel = null;
	}
}
