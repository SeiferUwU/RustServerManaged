#define UNITY_ASSERTIONS
using System;
using System.Collections.Generic;
using System.Linq;
using ConVar;
using Facepunch;
using Network;
using ProtoBuf;
using UnityEngine;
using UnityEngine.Assertions;

public class ProjectileWeaponMod : BaseEntity
{
	public enum SilencerType
	{
		Military,
		OilFilter,
		SodaCan
	}

	[Serializable]
	public struct Modifier
	{
		public bool enabled;

		[Tooltip("1 means no change. 0.5 is half.")]
		public float scalar;

		[Tooltip("Added after the scalar is applied.")]
		public float offset;
	}

	public float ConditionLossMultiplier = 1f;

	[Header("AttackEffectAdditive")]
	public GameObjectRef additiveEffect;

	[Header("Silencer")]
	public GameObjectRef defaultSilencerEffect;

	public bool isSilencer;

	public SilencerType silencerType;

	private static TimeSince lastADSTime;

	private static TimeSince lastToastTime;

	public static Translate.Phrase ToggleZoomToastPhrase = new Translate.Phrase("toast.toggle_zoom", "Press [PageUp] and [PageDown] to toggle scope zoom level");

	[Header("Weapon Basics")]
	public Modifier repeatDelay;

	public Modifier projectileVelocity;

	public Modifier projectileDamage;

	public Modifier projectileDistance;

	[Header("Recoil")]
	public Modifier aimsway;

	public Modifier aimswaySpeed;

	public Modifier recoil;

	[Header("Aim Cone")]
	public Modifier sightAimCone;

	public Modifier hipAimCone;

	[Header("Light Effects")]
	public bool isLight;

	[Header("MuzzleBrake")]
	public bool isMuzzleBrake;

	[Header("MuzzleBoost")]
	public bool isMuzzleBoost;

	[Header("Scope")]
	public bool isScope;

	public float zoomAmountDisplayOnly;

	[Header("Magazine")]
	public Modifier magazineCapacity;

	[Header("Toggling")]
	public bool needsOnForEffects;

	[Header("Burst")]
	public int burstCount = -1;

	public float timeBetweenBursts;

	[Header("Zoom")]
	public float[] zoomLevels;

	public GameObjectRef fovChangeEffect;

	[Header("Targeting")]
	public bool allowPings;

	private int serverZoomLevel;

	private bool hasZoomBeenInit;

	public override bool OnRpcMessage(BasePlayer player, uint rpc, Message msg)
	{
		using (TimeWarning.New("ProjectileWeaponMod.OnRpcMessage"))
		{
			if (rpc == 3713130066u && player != null)
			{
				Assert.IsTrue(player.isServer, "SV_RPC Message is using a clientside player!");
				if (Global.developer > 2)
				{
					Debug.Log("SV_RPCMessage: " + player?.ToString() + " - SetZoomLevel ");
				}
				using (TimeWarning.New("SetZoomLevel"))
				{
					using (TimeWarning.New("Conditions"))
					{
						if (!RPC_Server.FromOwner.Test(3713130066u, "SetZoomLevel", this, player, includeMounted: false))
						{
							return true;
						}
					}
					try
					{
						using (TimeWarning.New("Call"))
						{
							int zoomLevel = msg.read.Int32();
							SetZoomLevel(zoomLevel);
						}
					}
					catch (Exception exception)
					{
						Debug.LogException(exception);
						player.Kick("RPC Error in SetZoomLevel");
					}
				}
				return true;
			}
		}
		return base.OnRpcMessage(player, rpc, msg);
	}

	public override void ServerInit()
	{
		SetFlag(Flags.Disabled, b: true);
		base.ServerInit();
	}

	public override void PostServerLoad()
	{
		base.limitNetworking = HasFlag(Flags.Disabled);
	}

	public override void Save(SaveInfo info)
	{
		base.Save(info);
		info.msg.projectileWeaponMod = Facepunch.Pool.Get<GunWeaponMod>();
		info.msg.projectileWeaponMod.zoomLevel = serverZoomLevel;
	}

	[RPC_Server]
	[RPC_Server.FromOwner(false)]
	public void SetZoomLevel(int zoomLevel)
	{
		serverZoomLevel = zoomLevel;
		SendNetworkUpdate();
	}

	public override void Load(LoadInfo info)
	{
		base.Load(info);
		if (info.msg.projectileWeaponMod != null)
		{
			serverZoomLevel = info.msg.projectileWeaponMod.zoomLevel;
		}
	}

	public static float Mult(BaseEntity parentEnt, Func<ProjectileWeaponMod, Modifier> selector_modifier, Func<Modifier, float> selector_value, float def, bool bypassModToggles = false)
	{
		if (parentEnt.children == null)
		{
			return def;
		}
		return Mult(parentEnt.children.Cast<ProjectileWeaponMod>(), selector_modifier, selector_value, def, bypassModToggles);
	}

	public static float Mult(IEnumerable<ProjectileWeaponMod> mods, Func<ProjectileWeaponMod, Modifier> selector_modifier, Func<Modifier, float> selector_value, float def, bool bypassModToggles = false)
	{
		if (mods == null)
		{
			return def;
		}
		return Multiply(GetMods(mods, selector_modifier, selector_value, bypassModToggles));
	}

	private static float Multiply(IEnumerable<float> scalars)
	{
		float num = 1f;
		foreach (float scalar in scalars)
		{
			num *= scalar;
		}
		return num;
	}

	public static float Sum(BaseEntity parentEnt, Func<ProjectileWeaponMod, Modifier> selector_modifier, Func<Modifier, float> selector_value, float def, bool bypassModToggles = false)
	{
		if (parentEnt.children == null)
		{
			return def;
		}
		return Sum(parentEnt.children.Cast<ProjectileWeaponMod>(), selector_modifier, selector_value, def);
	}

	public static float Sum(IEnumerable<ProjectileWeaponMod> mods, Func<ProjectileWeaponMod, Modifier> selector_modifier, Func<Modifier, float> selector_value, float def, bool bypassModToggles = false)
	{
		if (mods == null)
		{
			return def;
		}
		IEnumerable<float> mods2 = GetMods(mods, selector_modifier, selector_value, bypassModToggles);
		if (mods2.Count() != 0)
		{
			return mods2.Sum();
		}
		return def;
	}

	public static float Average(BaseEntity parentEnt, Func<ProjectileWeaponMod, Modifier> selector_modifier, Func<Modifier, float> selector_value, float def, bool bypassModToggles = false)
	{
		if (parentEnt.children == null)
		{
			return def;
		}
		return Average(parentEnt.children.Cast<ProjectileWeaponMod>(), selector_modifier, selector_value, def);
	}

	public static float Average(IEnumerable<ProjectileWeaponMod> mods, Func<ProjectileWeaponMod, Modifier> selector_modifier, Func<Modifier, float> selector_value, float def, bool bypassModToggles = false)
	{
		if (mods == null)
		{
			return def;
		}
		IEnumerable<float> mods2 = GetMods(mods, selector_modifier, selector_value, bypassModToggles);
		if (mods2.Count() != 0)
		{
			return mods2.Average();
		}
		return def;
	}

	public static float Max(BaseEntity parentEnt, Func<ProjectileWeaponMod, Modifier> selector_modifier, Func<Modifier, float> selector_value, float def, bool bypassModToggles = false)
	{
		if (parentEnt.children == null)
		{
			return def;
		}
		return Max(parentEnt.children.Cast<ProjectileWeaponMod>(), selector_modifier, selector_value, def);
	}

	public static float Max(IEnumerable<ProjectileWeaponMod> mods, Func<ProjectileWeaponMod, Modifier> selector_modifier, Func<Modifier, float> selector_value, float def, bool bypassModToggles = false)
	{
		if (mods == null)
		{
			return def;
		}
		IEnumerable<float> mods2 = GetMods(mods, selector_modifier, selector_value, bypassModToggles);
		if (mods2.Count() != 0)
		{
			return mods2.Max();
		}
		return def;
	}

	public static float Min(BaseEntity parentEnt, Func<ProjectileWeaponMod, Modifier> selector_modifier, Func<Modifier, float> selector_value, float def, bool bypassModToggles = false)
	{
		if (parentEnt.children == null)
		{
			return def;
		}
		return Min(parentEnt.children.Cast<ProjectileWeaponMod>(), selector_modifier, selector_value, def);
	}

	public static float Min(IEnumerable<ProjectileWeaponMod> mods, Func<ProjectileWeaponMod, Modifier> selector_modifier, Func<Modifier, float> selector_value, float def, bool bypassModToggles = false)
	{
		if (mods == null)
		{
			return def;
		}
		IEnumerable<float> mods2 = GetMods(mods, selector_modifier, selector_value, bypassModToggles);
		if (mods2.Count() != 0)
		{
			return mods2.Min();
		}
		return def;
	}

	public static IEnumerable<float> GetMods(BaseEntity parentEnt, Func<ProjectileWeaponMod, Modifier> selector_modifier, Func<Modifier, float> selector_value)
	{
		return GetMods(parentEnt.children.Cast<ProjectileWeaponMod>(), selector_modifier, selector_value);
	}

	public static IEnumerable<float> GetMods(IEnumerable<ProjectileWeaponMod> mods, Func<ProjectileWeaponMod, Modifier> selector_modifier, Func<Modifier, float> selector_value, bool bypassModToggles = false)
	{
		return (from x in mods.Where((ProjectileWeaponMod x) => x != null && (!x.needsOnForEffects || bypassModToggles || x.HasFlag(Flags.On))).Select(selector_modifier)
			where x.enabled
			select x).Select(selector_value);
	}

	public static bool HasBrokenWeaponMod(BaseEntity parentEnt)
	{
		if (parentEnt.children == null)
		{
			return false;
		}
		if (parentEnt.children.Cast<ProjectileWeaponMod>().Any((ProjectileWeaponMod x) => x != null && x.IsBroken()))
		{
			return true;
		}
		return false;
	}
}
