using Facepunch;
using UnityEngine;

public class DroneStorage : StorageContainer
{
	[Header("Drone Storage")]
	public Transform AttachPoint;

	public Vector3 ReleaseVelocity;

	public float GrenadeWeaponDelayMod = 3f;

	public float ThrownWeaponDelayMod = 1f;

	public Drone Drone { get; set; }

	public void UpdateFlags()
	{
		if (Drone.HasFlag(Flags.Reserved5))
		{
			if (!TryGetItem(out var item) || !TryGetHeldEntity(item, out var held) || !(held is ThrownWeapon thrownWeapon))
			{
				Drone.SetFlag(Flags.Reserved5, b: false);
			}
			else
			{
				Drone.SetFlag(Flags.Reserved5, thrownWeapon.HasAttackCooldown());
			}
		}
	}

	public override void OnItemAddedOrRemoved(Item item, bool added)
	{
		base.OnItemAddedOrRemoved(item, added);
		if (added && !Drone.HasFlag(Flags.Reserved2) && !Drone.HasFlag(Flags.Reserved3))
		{
			Drone.body.position += Drone.transform.up * 0.14f;
			Drone.body.WakeUp();
		}
	}

	public bool TryServerDrop()
	{
		if (!TryGetItem(out var item))
		{
			return false;
		}
		bool flag = false;
		if (TryGetHeldEntity(item, out var held) && held is ThrownWeapon weapon)
		{
			return TryServerWeaponDrop(base.inventory.GetSlot(0), weapon);
		}
		return TryServerItemDrop(base.inventory.GetSlot(0));
	}

	private bool TryGetItem(out Item item)
	{
		item = null;
		if (base.inventory.IsEmpty())
		{
			return false;
		}
		item = base.inventory.GetSlot(0);
		if (item == null)
		{
			return false;
		}
		return true;
	}

	private bool TryGetHeldEntity(Item item, out BaseEntity held)
	{
		held = null;
		if (item == null)
		{
			return false;
		}
		held = item.GetHeldEntity();
		if (held == null)
		{
			return false;
		}
		return true;
	}

	private bool TryServerWeaponDrop(Item item, ThrownWeapon weapon)
	{
		if (item.amount <= 0 || weapon.HasAttackCooldown())
		{
			return false;
		}
		if (Drone == null)
		{
			return false;
		}
		AttachPoint.GetPositionAndRotation(out var position, out var rotation);
		Vector3 throwVelocityOverride = GetInheritedThrowVelocity(rotation * Vector3.down) + ReleaseVelocity;
		BasePlayer owningPlayer = Drone.ToPlayer();
		weapon.DoThrowImpl(position, rotation * Vector3.down, owningPlayer, out var thrownEntity, 1f, throwVelocityOverride, item);
		if (weapon is GrenadeWeapon)
		{
			weapon.StartAttackCooldown(weapon.repeatDelay * GrenadeWeaponDelayMod);
		}
		else
		{
			weapon.StartAttackCooldown(weapon.repeatDelay * ThrownWeaponDelayMod);
		}
		item.UseItem();
		TempIgnoreParent(thrownEntity);
		Drone.MarkHostileFor();
		if (weapon.HasAttackCooldown())
		{
			Drone.SetFlag(Flags.Reserved5, b: true);
		}
		SendNetworkUpdateImmediate();
		return true;
	}

	private bool TryServerItemDrop(Item item)
	{
		AttachPoint.GetPositionAndRotation(out var position, out var rotation);
		BaseEntity ent = item.Drop(position, GetInheritedProjectileVelocity(rotation * Vector3.down) + ReleaseVelocity);
		TempIgnoreParent(ent);
		return true;
	}

	private void TempIgnoreParent(BaseEntity ent)
	{
		if (ent == null || !parentEntity.IsValid(serverside: true))
		{
			return;
		}
		ent.gameObject.SetIgnoreCollisions(parentEntity.Get(serverside: true).gameObject, ignore: true);
		Invoke(delegate
		{
			BaseEntity baseEntity = ent;
			if (!(baseEntity == null))
			{
				BaseEntity baseEntity2 = parentEntity.Get(serverside: true);
				if (!(baseEntity2 == null))
				{
					baseEntity2.gameObject.SetIgnoreCollisions(baseEntity.gameObject, ignore: false);
				}
			}
		}, 2f);
	}
}
