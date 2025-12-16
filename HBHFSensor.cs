#define UNITY_ASSERTIONS
using System;
using ConVar;
using Network;
using Oxide.Core;
using UnityEngine;
using UnityEngine.Assertions;

public class HBHFSensor : BaseDetector
{
	public static int MinRange = 2;

	public static int MaxRange = 10;

	public int range = 10;

	public GameObjectRef detectUp;

	public GameObjectRef detectDown;

	public GameObjectRef panelPrefab;

	public const Flags Flag_IncludeOthers = Flags.Reserved2;

	public const Flags Flag_IncludeAuthed = Flags.Reserved3;

	private int detectedPlayers;

	public override bool OnRpcMessage(BasePlayer player, uint rpc, Message msg)
	{
		using (TimeWarning.New("HBHFSensor.OnRpcMessage"))
		{
			if (rpc == 4073303808u && player != null)
			{
				Assert.IsTrue(player.isServer, "SV_RPC Message is using a clientside player!");
				if (Global.developer > 2)
				{
					Debug.Log("SV_RPCMessage: " + player?.ToString() + " - SetConfig ");
				}
				using (TimeWarning.New("SetConfig"))
				{
					using (TimeWarning.New("Conditions"))
					{
						if (!RPC_Server.CallsPerSecond.Test(4073303808u, "SetConfig", this, player, 5uL))
						{
							return true;
						}
						if (!RPC_Server.IsVisible.Test(4073303808u, "SetConfig", this, player, 3f))
						{
							return true;
						}
					}
					try
					{
						using (TimeWarning.New("Call"))
						{
							RPCMessage config = new RPCMessage
							{
								connection = msg.connection,
								player = player,
								read = msg.read
							};
							SetConfig(config);
						}
					}
					catch (Exception exception)
					{
						Debug.LogException(exception);
						player.Kick("RPC Error in SetConfig");
					}
				}
				return true;
			}
		}
		return base.OnRpcMessage(player, rpc, msg);
	}

	public override int GetPassthroughAmount(int outputSlot = 0)
	{
		return Mathf.Min(detectedPlayers, GetCurrentEnergy());
	}

	public override void OnObjects()
	{
		base.OnObjects();
		UpdatePassthroughAmount();
		InvokeRandomized(UpdatePassthroughAmount, 0f, 1f, 0.1f);
	}

	public override void OnEmpty()
	{
		base.OnEmpty();
		UpdatePassthroughAmount();
		CancelInvoke(UpdatePassthroughAmount);
	}

	public void UpdatePassthroughAmount()
	{
		if (base.isClient || !IsPowered())
		{
			return;
		}
		int num = detectedPlayers;
		detectedPlayers = 0;
		if (myTrigger.entityContents != null && myTrigger.entityContents.Count > 0)
		{
			BuildingPrivlidge buildingPrivilege = GetBuildingPrivilege();
			foreach (BaseEntity entityContent in myTrigger.entityContents)
			{
				if (entityContent is BasePlayer basePlayer && Interface.CallHook("OnSensorDetect", this, basePlayer) == null && !(basePlayer == null) && !basePlayer.IsDead() && !basePlayer.IsSleeping() && basePlayer.isServer)
				{
					bool flag = buildingPrivilege != null && buildingPrivilege.IsAuthed(basePlayer);
					if ((!flag || ShouldIncludeAuthorized()) && (flag || ShouldIncludeOthers()) && entityContent.IsVisible(base.transform.position + base.transform.forward * 0.1f, range))
					{
						detectedPlayers++;
					}
				}
			}
		}
		if (num != detectedPlayers)
		{
			MarkDirty();
			if (detectedPlayers > num)
			{
				Effect.server.Run(detectUp.resourcePath, base.transform.position, Vector3.up);
			}
			else if (detectedPlayers < num)
			{
				Effect.server.Run(detectDown.resourcePath, base.transform.position, Vector3.up);
			}
		}
	}

	[RPC_Server]
	[RPC_Server.IsVisible(3f)]
	[RPC_Server.CallsPerSecond(5uL)]
	public void SetConfig(RPCMessage msg)
	{
		BasePlayer player = msg.player;
		if (!(player == null) && CanUse(player))
		{
			bool b = msg.read.Bit();
			bool b2 = msg.read.Bit();
			SetFlag(Flags.Reserved3, b);
			SetFlag(Flags.Reserved2, b2);
			int num = msg.read.Int32();
			SetRange(num);
		}
	}

	public void SetRange(int value)
	{
		value = Mathf.Clamp(value, MinRange, MaxRange);
		range = value;
		SendNetworkUpdate();
	}

	public bool CanUse(BasePlayer player)
	{
		object obj = Interface.CallHook("CanUseHBHFSensor", player, this);
		if (obj is bool)
		{
			return (bool)obj;
		}
		return player.CanBuild();
	}

	public bool ShouldIncludeAuthorized()
	{
		return HasFlag(Flags.Reserved3);
	}

	public bool ShouldIncludeOthers()
	{
		return HasFlag(Flags.Reserved2);
	}

	public override void Save(SaveInfo info)
	{
		base.Save(info);
		info.msg.ioEntity.genericInt1 = range;
	}

	public override void Load(LoadInfo info)
	{
		base.Load(info);
		if (info.msg.ioEntity != null)
		{
			range = info.msg.ioEntity.genericInt1;
		}
	}
}
