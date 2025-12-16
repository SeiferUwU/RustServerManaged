#define UNITY_ASSERTIONS
using System;
using ConVar;
using Network;
using Oxide.Core;
using ProtoBuf;
using UnityEngine;
using UnityEngine.Assertions;

public class CommunityEntity : PointEntity
{
	private class Countdown : MonoBehaviour
	{
		public enum TimerFormat
		{
			None,
			SecondsHundreth,
			MinutesSeconds,
			MinutesSecondsHundreth,
			HoursMinutes,
			HoursMinutesSeconds,
			HoursMinutesSecondsMilliseconds,
			HoursMinutesSecondsTenths,
			DaysHoursMinutes,
			DaysHoursMinutesSeconds,
			Custom
		}

		public string command = "";

		public float endTime;

		public float startTime;

		public float step = 1f;

		public float interval = 1f;

		public TimerFormat timerFormat;

		public string numberFormat = "0.####";

		public bool destroyIfDone = true;
	}

	public enum DraggablePositionSendType
	{
		NormalizedScreen,
		NormalizedParent,
		Relative,
		RelativeAnchor
	}

	private class FadeOut : MonoBehaviour
	{
		public float duration;
	}

	public static CommunityEntity ServerInstance;

	public static CommunityEntity ClientInstance;

	public GameObject[] OverallPanels;

	public Canvas[] AllCanvases;

	public override bool OnRpcMessage(BasePlayer player, uint rpc, Message msg)
	{
		using (TimeWarning.New("CommunityEntity.OnRpcMessage"))
		{
			if (rpc == 2271099967u && player != null)
			{
				Assert.IsTrue(player.isServer, "SV_RPC Message is using a clientside player!");
				if (Global.developer > 2)
				{
					Debug.Log("SV_RPCMessage: " + player?.ToString() + " - DragRPC ");
				}
				using (TimeWarning.New("DragRPC"))
				{
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
							DragRPC(rpc2);
						}
					}
					catch (Exception exception)
					{
						Debug.LogException(exception);
						player.Kick("RPC Error in DragRPC");
					}
				}
				return true;
			}
			if (rpc == 3687934507u && player != null)
			{
				Assert.IsTrue(player.isServer, "SV_RPC Message is using a clientside player!");
				if (Global.developer > 2)
				{
					Debug.Log("SV_RPCMessage: " + player?.ToString() + " - DropRPC ");
				}
				using (TimeWarning.New("DropRPC"))
				{
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
							DropRPC(rpc3);
						}
					}
					catch (Exception exception2)
					{
						Debug.LogException(exception2);
						player.Kick("RPC Error in DropRPC");
					}
				}
				return true;
			}
		}
		return base.OnRpcMessage(player, rpc, msg);
	}

	public override void InitShared()
	{
		if (base.isServer)
		{
			ServerInstance = this;
		}
		else
		{
			ClientInstance = this;
		}
		base.InitShared();
	}

	public override void DestroyShared()
	{
		base.DestroyShared();
		if (base.isServer)
		{
			ServerInstance = null;
		}
		else
		{
			ClientInstance = null;
		}
	}

	[RPC_Server]
	public void DragRPC(RPCMessage rpc)
	{
		string text = rpc.read.String();
		Vector3 position = rpc.read.Vector3();
		DraggablePositionSendType type = (DraggablePositionSendType)rpc.read.Int32();
		Hook_DragRPC(rpc.player, text, position, type);
	}

	private void Hook_DragRPC(BasePlayer player, string name, Vector3 position, DraggablePositionSendType type)
	{
		Interface.CallHook("OnCuiDraggableDrag", player, name, position, type);
	}

	[RPC_Server]
	public void DropRPC(RPCMessage rpc)
	{
		string draggedName = rpc.read.String();
		string draggedSlot = rpc.read.String();
		string swappedName = rpc.read.String();
		string swappedSlot = rpc.read.String();
		Hook_DropRPC(rpc.player, draggedName, draggedSlot, swappedName, swappedSlot);
	}

	private void Hook_DropRPC(BasePlayer player, string draggedName, string draggedSlot, string swappedName, string swappedSlot)
	{
		Interface.CallHook("OnCuiDraggableDrop", player, draggedName, draggedSlot, swappedName, swappedSlot);
	}

	public void SendCustomVitals(BasePlayer player, CustomVitals vitals)
	{
		ClientRPC(RpcTarget.Player("RPC_UpdateVitals", player), vitals);
	}
}
