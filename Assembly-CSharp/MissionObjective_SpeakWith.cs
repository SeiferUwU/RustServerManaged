using ConVar;
using UnityEngine;

[CreateAssetMenu(menuName = "Rust/Missions/OBJECTIVES/SpeakWith")]
public class MissionObjective_SpeakWith : MissionObjective
{
	public ItemAmount[] requiredReturnItems = new ItemAmount[0];

	public bool destroyReturnItems;

	public bool showPing;

	public bool checkSpaceForRewards;

	public static Translate.Phrase NoSpaceInInventoryPhrase = new Translate.Phrase("no_space_mission_reward", "No space for rewards in inventory, please clear some space");

	public override void ObjectiveStarted(BasePlayer playerFor, int index, BaseMission.MissionInstance instance)
	{
		BaseEntity baseEntity = instance.ProviderEntity();
		if ((bool)baseEntity && !showPing)
		{
			instance.missionLocation = baseEntity.transform.position;
			playerFor.MissionDirty();
		}
		base.ObjectiveStarted(playerFor, index, instance);
		if (baseEntity != null && showPing)
		{
			playerFor.RegisterPingedEntity(baseEntity, BasePlayer.PingType.GoTo);
		}
	}

	public override void ProcessMissionEvent(BasePlayer playerFor, BaseMission.MissionInstance instance, int index, BaseMission.MissionEventType type, BaseMission.MissionEventPayload payload, float amount)
	{
		base.ProcessMissionEvent(playerFor, instance, index, type, payload, amount);
		if (type != BaseMission.MissionEventType.CONVERSATION)
		{
			return;
		}
		if (Debugging.printMissionSpeakInfo)
		{
			Debug.Log($"[MissionSpeakInfo] objective {base.name} on {instance.GetMission().name} instance IsCompleted:{IsCompleted(index, instance)} CanProgress:{CanProgress(index, instance)}, amount: {amount}");
		}
		if (IsCompleted(index, instance) || !CanProgress(index, instance))
		{
			return;
		}
		BaseEntity baseEntity = instance.ProviderEntity();
		if (baseEntity == null)
		{
			if (Debugging.printMissionSpeakInfo)
			{
				Debug.Log("[MissionSpeakInfo] objective " + base.name + " on " + instance.GetMission().name + " failed to find a provider entity attached to this mission instance");
			}
			return;
		}
		if (Debugging.printMissionSpeakInfo)
		{
			Debug.Log($"[MissionSpeakInfo] objective {base.name} on {instance.GetMission().name} looking for provider: {instance.providerID.Value}/{baseEntity} Supplied NPC:{payload.NetworkIdentifier}");
		}
		if (!baseEntity.TryGetComponent<IMissionProvider>(out var component))
		{
			Debug.LogError("Failed to retrieve a IMissionProvider component from " + baseEntity.gameObject.name + ", prefab: " + baseEntity.PrefabName, baseEntity);
		}
		else if (component.ProviderID() == payload.NetworkIdentifier && Mathf.Approximately(amount, 1f))
		{
			bool flag = true;
			ItemAmount[] array = requiredReturnItems;
			foreach (ItemAmount itemAmount in array)
			{
				if ((float)playerFor.inventory.GetAmount(itemAmount.itemDef.itemid) < itemAmount.amount)
				{
					flag = false;
					break;
				}
			}
			if (Debugging.printMissionSpeakInfo)
			{
				Debug.Log($"[MissionSpeakInfo] objective {base.name} on {instance.GetMission().name} CheckRewardsSpace: {CheckRewardsSpace()}");
			}
			if (flag && destroyReturnItems)
			{
				if (!CheckRewardsSpace())
				{
					return;
				}
				array = requiredReturnItems;
				foreach (ItemAmount itemAmount2 in array)
				{
					playerFor.inventory.Take(null, itemAmount2.itemDef.itemid, (int)itemAmount2.amount);
				}
			}
			if (Debugging.printMissionSpeakInfo)
			{
				Debug.Log($"[MissionSpeakInfo] objective {base.name} on {instance.GetMission().name} CheckRewardsSpace: {CheckRewardsSpace()}");
			}
			if (CheckRewardsSpace())
			{
				if (Debugging.printMissionSpeakInfo)
				{
					Debug.Log($"[MissionSpeakInfo] objective {base.name} on {instance.GetMission().name} requiredReturnItems == null: {requiredReturnItems == null}, requiredReturnItems.Length: {requiredReturnItems.Length}, hasAllReturnItems: {flag}");
				}
				if (requiredReturnItems == null || requiredReturnItems.Length == 0 || flag)
				{
					CompleteObjective(index, instance, playerFor);
				}
			}
		}
		else if (Debugging.printMissionSpeakInfo)
		{
			if (component.ProviderID() != payload.NetworkIdentifier)
			{
				Debug.Log($"[MissionSpeakInfo] objective {base.name} on {instance.GetMission().name} failed to match supplied network ID {payload.NetworkIdentifier} with instance missionProvider.ProviderID(): {component.ProviderID()}. ProviderID() should match instance.providerID: {instance.providerID}");
			}
			if (!Mathf.Approximately(amount, 1f))
			{
				Debug.Log($"[MissionSpeakInfo] objective {base.name} on {instance.GetMission().name} supplied amount {amount} is not approximately 1f");
			}
		}
		bool CheckRewardsSpace()
		{
			if (checkSpaceForRewards && !playerFor.inventory.HasEmptySlots(instance.GetTotalRequiredRewardItemSlots()))
			{
				playerFor.ShowToast(GameTip.Styles.Red_Normal, NoSpaceInInventoryPhrase, false);
				return false;
			}
			return true;
		}
	}

	public override void ObjectiveCompleted(BasePlayer playerFor, int index, BaseMission.MissionInstance instance)
	{
		base.ObjectiveCompleted(playerFor, index, instance);
		if (showPing)
		{
			DeregisterPing(playerFor, instance);
		}
	}

	private static void DeregisterPing(BasePlayer playerFor, BaseMission.MissionInstance instance)
	{
		BaseEntity baseEntity = instance.ProviderEntity();
		if (baseEntity != null)
		{
			playerFor.DeregisterPingedEntity(baseEntity.net.ID, BasePlayer.PingType.GoTo);
		}
	}

	public override void ObjectiveFailed(BasePlayer playerFor, int index, BaseMission.MissionInstance instance)
	{
		base.ObjectiveFailed(playerFor, index, instance);
		if (showPing)
		{
			DeregisterPing(playerFor, instance);
		}
	}
}
