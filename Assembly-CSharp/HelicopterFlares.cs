using System;
using System.Runtime.CompilerServices;
using ConVar;
using Facepunch;
using Network;
using UnityEngine;

public class HelicopterFlares : StorageContainer
{
	[SerializeField]
	[Header("Helicopter Flares")]
	private ItemDefinition flareItemDef;

	[SerializeField]
	private float timeBetweenFlares = 30f;

	[SerializeField]
	private float flareLaunchVel = 10f;

	[SerializeField]
	private GameObjectRef flareFireFX;

	[SerializeField]
	private GameObjectRef serverFlarePrefab;

	[SerializeField]
	private Transform leftFlareLaunchPos;

	[SerializeField]
	private Transform rightFlareLaunchPos;

	[HideInInspector]
	public ICanFireHelicopterFlares owner;

	private TimeSince timeSinceFlareFired;

	private bool __sync_HasFlares;

	[Sync]
	public bool HasFlares
	{
		[CompilerGenerated]
		get
		{
			return __sync_HasFlares;
		}
		[CompilerGenerated]
		private set
		{
			if (!IsSyncVarEqual(__sync_HasFlares, value))
			{
				__sync_HasFlares = value;
				byte nameID = __GetWeaverID("HasFlares");
				QueueSyncVar(nameID);
			}
		}
	}

	public bool CanFireFlare
	{
		get
		{
			if ((float)timeSinceFlareFired >= timeBetweenFlares)
			{
				return HasFlareAmmo();
			}
			return false;
		}
	}

	public override bool OnRpcMessage(BasePlayer player, uint rpc, Message msg)
	{
		using (TimeWarning.New("HelicopterFlares.OnRpcMessage"))
		{
		}
		return base.OnRpcMessage(player, rpc, msg);
	}

	public bool HasFlareAmmo()
	{
		if (base.isServer)
		{
			HasFlares = base.inventory.HasAny(flareItemDef);
			return HasFlares;
		}
		return false;
	}

	private void ResetFiringTimes()
	{
		timeSinceFlareFired = 9999f;
	}

	public override bool CanBeLooted(BasePlayer player)
	{
		if (owner.flareEntity.IsOn())
		{
			return false;
		}
		return base.CanBeLooted(player);
	}

	public override void Load(LoadInfo info)
	{
		base.Load(info);
		HasFlares = HasFlareAmmo();
	}

	public override void Save(SaveInfo info)
	{
		base.Save(info);
		HasFlares = HasFlareAmmo();
	}

	public bool TryFireFlare()
	{
		if (!CanFireFlare)
		{
			return false;
		}
		if (owner == null)
		{
			return false;
		}
		if (!base.inventory.TryTakeOne(flareItemDef.itemid, out var item))
		{
			return false;
		}
		item.Remove();
		timeSinceFlareFired = 0f;
		LaunchFlare();
		ClientRPC(RpcTarget.NetworkGroup("RPCFlareFired"), HasFlareAmmo());
		return true;
	}

	public void LaunchFlare()
	{
		Effect.server.Run(flareFireFX.resourcePath, owner.flareEntity, StringPool.Get("FlareLaunchPos"), Vector3.zero, Vector3.zero);
		GameManager.server.CreatePrefab(serverFlarePrefab.resourcePath, leftFlareLaunchPos.position, Quaternion.identity).GetComponent<HeliPilotFlare>().Init(-owner.flareEntity.transform.right * flareLaunchVel);
		GameManager.server.CreatePrefab(serverFlarePrefab.resourcePath, rightFlareLaunchPos.position, Quaternion.identity).GetComponent<HeliPilotFlare>().Init(owner.flareEntity.transform.right * flareLaunchVel);
	}

	public void RefillFlares()
	{
		ItemDefinition itemDefinition = ItemManager.FindItemDefinition("flare");
		int amount = itemDefinition.stackable * 2;
		base.inventory.AddItem(itemDefinition, amount, 0uL, ItemContainer.LimitStack.All);
	}

	protected override bool WriteSyncVar(byte id, NetWrite writer)
	{
		if (id == 0)
		{
			if (Global.developer > 2)
			{
				NetworkableId iD = net.ID;
				Debug.Log("SyncVar Writing: HasFlares for " + iD.ToString());
			}
			SyncVarNetWrite(writer, __sync_HasFlares);
			return true;
		}
		return false;
	}

	protected override bool OnSyncVar(byte id, NetRead reader, bool fromAutoSave = false)
	{
		if (id == 0)
		{
			try
			{
				_ = __sync_HasFlares;
				bool _sync_HasFlares = reader.Bool();
				__sync_HasFlares = _sync_HasFlares;
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
			return true;
		}
		return false;
	}

	private byte __GetWeaverID(string propertyName)
	{
		_ = propertyName == "HasFlares";
		return 0;
	}

	protected override bool AutoSaveSyncVars(SaveInfo save)
	{
		NetWrite obj = Network.Net.sv.StartWrite();
		var (src, num) = obj.GetBuffer();
		if (_autosaveBuffer == null)
		{
			_autosaveBuffer = BaseEntity._autosaveBufferPool.Rent(num);
		}
		if (_autosaveBuffer.Length < num)
		{
			BaseEntity._autosaveBufferPool.Return(_autosaveBuffer);
			_autosaveBuffer = BaseEntity._autosaveBufferPool.Rent(num);
		}
		Buffer.BlockCopy(src, 0, _autosaveBuffer, 0, num);
		save.msg.baseEntity.syncVars = _autosaveBuffer;
		Facepunch.Pool.Free(ref obj);
		return true;
	}

	protected override bool AutoLoadSyncVars(LoadInfo load)
	{
		if (load.msg.baseEntity != null && load.msg.baseEntity.syncVars != null)
		{
			NetRead obj = Facepunch.Pool.Get<NetRead>();
			obj.Init(load.msg.baseEntity.syncVars.AsSpan());
			Facepunch.Pool.Free(ref obj);
		}
		return true;
	}

	protected override void ResetSyncVars()
	{
		__sync_HasFlares = false;
	}

	protected override bool ShouldInvalidateCache(byte id)
	{
		_ = 0;
		return true;
	}
}
