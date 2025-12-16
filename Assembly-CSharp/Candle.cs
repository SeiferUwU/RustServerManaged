#define UNITY_ASSERTIONS
using System;
using ConVar;
using Network;
using Rust;
using UnityEngine;
using UnityEngine.Assertions;

public class Candle : BaseCombatEntity, ISplashable, IIgniteable, IAlwaysOn
{
	public float lifeTimeSeconds = 7200f;

	public float burnRate = 10f;

	public const Flags AlwaysOn = Flags.Reserved3;

	public override bool OnRpcMessage(BasePlayer player, uint rpc, Message msg)
	{
		using (TimeWarning.New("Candle.OnRpcMessage"))
		{
			if (rpc == 2523893445u && player != null)
			{
				Assert.IsTrue(player.isServer, "SV_RPC Message is using a clientside player!");
				if (ConVar.Global.developer > 2)
				{
					Debug.Log("SV_RPCMessage: " + player?.ToString() + " - SetWantsOn ");
				}
				using (TimeWarning.New("SetWantsOn"))
				{
					using (TimeWarning.New("Conditions"))
					{
						if (!RPC_Server.IsVisible.Test(2523893445u, "SetWantsOn", this, player, 3f))
						{
							return true;
						}
					}
					try
					{
						using (TimeWarning.New("Call"))
						{
							RPCMessage wantsOn = new RPCMessage
							{
								connection = msg.connection,
								player = player,
								read = msg.read
							};
							SetWantsOn(wantsOn);
						}
					}
					catch (Exception exception)
					{
						Debug.LogException(exception);
						player.Kick("RPC Error in SetWantsOn");
					}
				}
				return true;
			}
		}
		return base.OnRpcMessage(player, rpc, msg);
	}

	[RPC_Server.IsVisible(3f)]
	[RPC_Server]
	public void SetWantsOn(RPCMessage msg)
	{
		bool b = msg.read.Bit();
		SetFlag(Flags.On, b);
		UpdateInvokes();
	}

	public override void PostServerLoad()
	{
		base.PostServerLoad();
		UpdateInvokes();
	}

	public void UpdateInvokes()
	{
		if (IsOn())
		{
			InvokeRandomized(Burn, burnRate, burnRate, 1f);
		}
		else
		{
			CancelInvoke(Burn);
		}
	}

	public void Burn()
	{
		if (!IsAlwaysOn())
		{
			float num = burnRate / lifeTimeSeconds;
			Hurt(num * MaxHealth(), DamageType.Decay, this, useProtection: false);
		}
	}

	public override void OnAttacked(HitInfo info)
	{
		if (base.isServer && info.damageTypes.Get(DamageType.Heat) > 0f && !IsOn())
		{
			SetFlag(Flags.On, b: true);
			UpdateInvokes();
		}
		base.OnAttacked(info);
	}

	public bool WantsSplash(ItemDefinition splashType, int amount)
	{
		if (!base.IsDestroyed && amount > 1)
		{
			return IsOn();
		}
		return false;
	}

	public int DoSplash(ItemDefinition splashType, int amount)
	{
		if (amount > 1)
		{
			SetFlag(Flags.On, b: false);
			UpdateInvokes();
			amount--;
		}
		return amount;
	}

	public void Ignite(Vector3 fromPos)
	{
		SetFlag(Flags.On, b: true);
		UpdateInvokes();
	}

	public bool CanIgnite()
	{
		return !IsOn();
	}

	public virtual bool IsAlwaysOn()
	{
		if (HasFlag(Flags.Reserved3))
		{
			return Creative.alwaysOnEnabled;
		}
		return false;
	}

	public void SetAlwaysOn(bool flag)
	{
		SetFlag(Flags.Reserved3, flag);
		AlwaysOnToggled(flag);
	}

	public void AlwaysOnToggled(bool flag)
	{
		if (flag)
		{
			if (CanIgnite())
			{
				Ignite(base.transform.position);
			}
		}
		else
		{
			SetFlag(Flags.On, b: false);
			UpdateInvokes();
		}
	}
}
