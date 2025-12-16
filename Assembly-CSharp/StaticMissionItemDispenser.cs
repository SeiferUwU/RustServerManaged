using UnityEngine;

public class StaticMissionItemDispenser : StorageContainer
{
	public override void ServerInit()
	{
		base.ServerInit();
		if (base.inventory != null)
		{
			base.inventory.SetFlag(ItemContainer.Flag.NoItemInput, b: true);
		}
	}

	public override bool CanBeLooted(BasePlayer player)
	{
		if (base.CanBeLooted(player))
		{
			return HasValidMission(player);
		}
		return false;
	}

	public override bool PlayerOpenLoot(BasePlayer player, string panelToOpen = "", bool doPositionChecks = true)
	{
		BaseMission.MissionInstance activeMissionInstance = player.GetActiveMissionInstance();
		if (activeMissionInstance != null)
		{
			for (int i = 0; i < activeMissionInstance.objectiveStatuses.Length; i++)
			{
				if (activeMissionInstance.objectiveStatuses[i].started && activeMissionInstance.GetMission().objectives[i].objective is MissionObjective_AcquireItem missionObjective_AcquireItem)
				{
					base.inventory.AddItem(missionObjective_AcquireItem.targetItem, missionObjective_AcquireItem.targetItemAmount, 0uL);
				}
			}
		}
		return base.PlayerOpenLoot(player, panelToOpen, doPositionChecks);
	}

	public override void PlayerStoppedLooting(BasePlayer player)
	{
		base.inventory.Clear();
		base.PlayerStoppedLooting(player);
	}

	private bool HasValidMission(BasePlayer player)
	{
		if (player == null)
		{
			return false;
		}
		BaseMission.MissionInstance activeMissionInstance = player.GetActiveMissionInstance();
		if (activeMissionInstance != null)
		{
			for (int i = 0; i < activeMissionInstance.objectiveStatuses.Length; i++)
			{
				if (!activeMissionInstance.objectiveStatuses[i].started || activeMissionInstance.objectiveStatuses[i].completed || !(activeMissionInstance.GetMission().objectives[i].objective is MissionObjective_AcquireItem missionObjective_AcquireItem))
				{
					continue;
				}
				if (!string.IsNullOrEmpty(missionObjective_AcquireItem.requireProximityToPosition) && base.isServer)
				{
					if (!activeMissionInstance.missionPoints.ContainsKey(missionObjective_AcquireItem.requireProximityToPosition))
					{
						return false;
					}
					if (Vector3.Distance(activeMissionInstance.missionPoints[missionObjective_AcquireItem.requireProximityToPosition], base.transform.position) > 3f)
					{
						return false;
					}
				}
				return true;
			}
		}
		return false;
	}
}
