#define UNITY_ASSERTIONS
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ConVar;
using Facepunch;
using Network;
using Rust.UI;
using UnityEngine;
using UnityEngine.Assertions;
using ntw.CurvedTextMeshPro;

public class PartyBalloon : BaseCombatEntity
{
	[Header("Party Balloon Dependencies")]
	public RustText balloonTextComponent;

	public RustText backsideBalloonTextComponent;

	public TextProOnACircle textProOnACircleComponent;

	public TextProOnACircle backsideTextProOnACircleComponent;

	public GameObjectRef configureBalloonDialog;

	public List<Renderer> balloonRenderers;

	[Header("Party Balloon Text Settings")]
	public bool useTimeOfDayForTextColour;

	public int arcRadCharactersLimit = 20;

	public int minArcDegrees = 12;

	public int maxArcDegrees = 37;

	public int textLinesLimit = 4;

	public float minLineHeight = 10f;

	public float maxLineHeight = 30f;

	public float fontSizeDivider = 1f;

	[Header("Party Balloon FX")]
	public GameObjectRef partyBalloonPopFX;

	private static readonly int COLOR = Shader.PropertyToID("_Color");

	private string __sync_BalloonText;

	private Color __sync_BalloonColour;

	private Color __sync_TextColour;

	[Sync(Autosave = true)]
	public string BalloonText
	{
		[CompilerGenerated]
		get
		{
			return __sync_BalloonText;
		}
		[CompilerGenerated]
		set
		{
			if (!IsSyncVarEqual(__sync_BalloonText, value))
			{
				__sync_BalloonText = value;
				byte nameID = __GetWeaverID("BalloonText");
				QueueSyncVar(nameID);
			}
		}
	}

	[Sync(Autosave = true)]
	public Color BalloonColour
	{
		[CompilerGenerated]
		get
		{
			return __sync_BalloonColour;
		}
		[CompilerGenerated]
		set
		{
			if (!IsSyncVarEqual(__sync_BalloonColour, value))
			{
				__sync_BalloonColour = value;
				byte nameID = __GetWeaverID("BalloonColour");
				QueueSyncVar(nameID);
			}
		}
	}

	[Sync(Autosave = true)]
	public Color TextColour
	{
		[CompilerGenerated]
		get
		{
			return __sync_TextColour;
		}
		[CompilerGenerated]
		set
		{
			if (!IsSyncVarEqual(__sync_TextColour, value))
			{
				__sync_TextColour = value;
				byte nameID = __GetWeaverID("TextColour");
				QueueSyncVar(nameID);
			}
		}
	}

	public override bool OnRpcMessage(BasePlayer player, uint rpc, Message msg)
	{
		using (TimeWarning.New("PartyBalloon.OnRpcMessage"))
		{
			if (rpc == 1887711985 && player != null)
			{
				Assert.IsTrue(player.isServer, "SV_RPC Message is using a clientside player!");
				if (Global.developer > 2)
				{
					Debug.Log("SV_RPCMessage: " + player?.ToString() + " - LockBalloon ");
				}
				using (TimeWarning.New("LockBalloon"))
				{
					using (TimeWarning.New("Conditions"))
					{
						if (!RPC_Server.MaxDistance.Test(1887711985u, "LockBalloon", this, player, 3f))
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
							LockBalloon(msg2);
						}
					}
					catch (Exception exception)
					{
						Debug.LogException(exception);
						player.Kick("RPC Error in LockBalloon");
					}
				}
				return true;
			}
			if (rpc == 473707823 && player != null)
			{
				Assert.IsTrue(player.isServer, "SV_RPC Message is using a clientside player!");
				if (Global.developer > 2)
				{
					Debug.Log("SV_RPCMessage: " + player?.ToString() + " - RPC_ConfigureBalloon ");
				}
				using (TimeWarning.New("RPC_ConfigureBalloon"))
				{
					using (TimeWarning.New("Conditions"))
					{
						if (!RPC_Server.CallsPerSecond.Test(473707823u, "RPC_ConfigureBalloon", this, player, 2uL))
						{
							return true;
						}
						if (!RPC_Server.IsVisible.Test(473707823u, "RPC_ConfigureBalloon", this, player, 3f))
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
							RPC_ConfigureBalloon(msg3);
						}
					}
					catch (Exception exception2)
					{
						Debug.LogException(exception2);
						player.Kick("RPC Error in RPC_ConfigureBalloon");
					}
				}
				return true;
			}
			if (rpc == 2622659557u && player != null)
			{
				Assert.IsTrue(player.isServer, "SV_RPC Message is using a clientside player!");
				if (Global.developer > 2)
				{
					Debug.Log("SV_RPCMessage: " + player?.ToString() + " - UnLockBalloon ");
				}
				using (TimeWarning.New("UnLockBalloon"))
				{
					using (TimeWarning.New("Conditions"))
					{
						if (!RPC_Server.MaxDistance.Test(2622659557u, "UnLockBalloon", this, player, 3f))
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
							UnLockBalloon(msg4);
						}
					}
					catch (Exception exception3)
					{
						Debug.LogException(exception3);
						player.Kick("RPC Error in UnLockBalloon");
					}
				}
				return true;
			}
		}
		return base.OnRpcMessage(player, rpc, msg);
	}

	public virtual bool CanUpdateBalloon(BasePlayer player)
	{
		if (player.IsAdmin || player.IsDeveloper)
		{
			return true;
		}
		if (!player.CanBuild())
		{
			return false;
		}
		if (IsLocked())
		{
			return (ulong)player.userID == base.OwnerID;
		}
		return true;
	}

	public bool CanUnlockBalloon(BasePlayer player)
	{
		if (!IsLocked())
		{
			return false;
		}
		return CanUpdateBalloon(player);
	}

	public bool CanLockBalloon(BasePlayer player)
	{
		if (IsLocked())
		{
			return false;
		}
		return CanUpdateBalloon(player);
	}

	public override void ServerInit()
	{
		base.ServerInit();
		BalloonColour = Color.white;
		TextColour = Color.white;
	}

	[RPC_Server.CallsPerSecond(2uL)]
	[RPC_Server.IsVisible(3f)]
	[RPC_Server]
	public void RPC_ConfigureBalloon(RPCMessage msg)
	{
		string balloonText = msg.read.String();
		Color balloonColour = msg.read.Color();
		Color textColour = msg.read.Color();
		SetBalloonText(balloonText);
		SetBalloonColour(balloonColour);
		SetTextColour(textColour);
	}

	public void SetBalloonText(string text)
	{
		BalloonText = text;
	}

	public void SetBalloonColour(Color colour)
	{
		BalloonColour = colour;
	}

	public void SetTextColour(Color colour)
	{
		TextColour = colour;
	}

	[RPC_Server]
	[RPC_Server.MaxDistance(3f)]
	public void LockBalloon(RPCMessage msg)
	{
		if (msg.player.CanInteract() && CanUpdateBalloon(msg.player))
		{
			SetFlag(Flags.Locked, b: true);
			SendNetworkUpdate();
			base.OwnerID = msg.player.userID;
		}
	}

	[RPC_Server.MaxDistance(3f)]
	[RPC_Server]
	public void UnLockBalloon(RPCMessage msg)
	{
		if (msg.player.CanInteract() && CanUnlockBalloon(msg.player))
		{
			SetFlag(Flags.Locked, b: false);
			SendNetworkUpdate();
		}
	}

	protected override bool WriteSyncVar(byte id, NetWrite writer)
	{
		switch (id)
		{
		case 0:
			if (Global.developer > 2)
			{
				NetworkableId iD = net.ID;
				Debug.Log("SyncVar Writing: BalloonText for " + iD.ToString());
			}
			SyncVarNetWrite(writer, __sync_BalloonText);
			return true;
		case 1:
			if (Global.developer > 2)
			{
				NetworkableId iD = net.ID;
				Debug.Log("SyncVar Writing: BalloonColour for " + iD.ToString());
			}
			SyncVarNetWrite(writer, __sync_BalloonColour);
			return true;
		case 2:
			if (Global.developer > 2)
			{
				NetworkableId iD = net.ID;
				Debug.Log("SyncVar Writing: TextColour for " + iD.ToString());
			}
			SyncVarNetWrite(writer, __sync_TextColour);
			return true;
		default:
			return false;
		}
	}

	protected override bool OnSyncVar(byte id, NetRead reader, bool fromAutoSave = false)
	{
		switch (id)
		{
		case 0:
			try
			{
				_ = __sync_BalloonText;
				string _sync_BalloonText = reader.String();
				__sync_BalloonText = _sync_BalloonText;
			}
			catch (Exception exception2)
			{
				Debug.LogException(exception2);
			}
			return true;
		case 1:
			try
			{
				_ = __sync_BalloonColour;
				Color _sync_BalloonColour = reader.Color();
				__sync_BalloonColour = _sync_BalloonColour;
			}
			catch (Exception exception3)
			{
				Debug.LogException(exception3);
			}
			return true;
		case 2:
			try
			{
				_ = __sync_TextColour;
				Color _sync_TextColour = reader.Color();
				__sync_TextColour = _sync_TextColour;
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
			return true;
		default:
			return false;
		}
	}

	private byte __GetWeaverID(string propertyName)
	{
		return propertyName switch
		{
			"BalloonText" => 0, 
			"BalloonColour" => 1, 
			"TextColour" => 2, 
			_ => 0, 
		};
	}

	protected override bool AutoSaveSyncVars(SaveInfo save)
	{
		NetWrite obj = Network.Net.sv.StartWrite();
		WriteSyncVar(0, obj);
		WriteSyncVar(1, obj);
		WriteSyncVar(2, obj);
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
			OnSyncVar(0, obj, fromAutoSave: true);
			OnSyncVar(1, obj, fromAutoSave: true);
			OnSyncVar(2, obj, fromAutoSave: true);
			Facepunch.Pool.Free(ref obj);
		}
		return true;
	}

	protected override void ResetSyncVars()
	{
		__sync_BalloonText = null;
		__sync_BalloonColour = default(Color);
		__sync_TextColour = default(Color);
	}

	protected override bool ShouldInvalidateCache(byte id)
	{
		return id switch
		{
			0 => true, 
			1 => true, 
			2 => true, 
			_ => true, 
		};
	}
}
