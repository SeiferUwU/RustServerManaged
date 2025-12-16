using System.Collections.Generic;
using Facepunch;
using UnityEngine;

public class NPCSimpleMissionProvider : NPCTalking, IMissionProvider
{
	public GameObjectRef MarkerPrefab;

	public Translate.Phrase NPCName = new Translate.Phrase();

	public override void ServerInit()
	{
		base.ServerInit();
		if (MarkerPrefab != null && MarkerPrefab.isValid)
		{
			List<BaseMission> list = Pool.Get<List<BaseMission>>();
			GetAvailableMissions(list);
			if (list.Count > 0)
			{
				MapMarkerMissionProvider obj = GameManager.server.CreateEntity(MarkerPrefab.resourcePath, base.transform.position, base.transform.rotation) as MapMarkerMissionProvider;
				obj.AssignMissions(list, NPCName.token);
				obj.Spawn();
			}
			Pool.FreeUnmanaged(ref missions);
		}
	}

	public override void OnConversationEnded(BasePlayer player)
	{
		player.ProcessMissionEvent(BaseMission.MissionEventType.CONVERSATION, ProviderID(), 0f);
		base.OnConversationEnded(player);
	}

	public override void OnConversationStarted(BasePlayer speakingTo)
	{
		speakingTo.ProcessMissionEvent(BaseMission.MissionEventType.CONVERSATION, ProviderID(), 1f);
		base.OnConversationStarted(speakingTo);
	}

	protected override void TryAssignMissionToPlayer(BaseMission mission, BasePlayer player)
	{
		base.TryAssignMissionToPlayer(mission, player);
		BaseMission.AssignMission(player, this, mission);
	}

	public override string GetConversationStartSpeech(BasePlayer player)
	{
		using PooledList<BaseMission> pooledList = Pool.Get<PooledList<BaseMission>>();
		GetAvailableMissions(pooledList);
		bool flag = false;
		foreach (BaseMission item in pooledList)
		{
			if (player.CanAcceptMission(item))
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			return "intro_no_missions";
		}
		return base.GetConversationStartSpeech(player);
	}

	public override void GetGenericMissionList(List<BaseMission> foundMissions)
	{
		base.GetGenericMissionList(foundMissions);
		GetAvailableMissions(foundMissions);
	}

	private void GetAvailableMissions(List<BaseMission> foundMissions)
	{
		ScriptableObjectRef[] missionList = MissionManifest.Get().missionList;
		for (int i = 0; i < missionList.Length; i++)
		{
			BaseMission baseMission = missionList[i].Get() as BaseMission;
			if (baseMission != null && baseMission.genericMissionProvider.isValid && baseMission.genericMissionProvider.Get().TryGetComponent<NPCSimpleMissionProvider>(out var component) && component.prefabID == prefabID)
			{
				foundMissions.Add(baseMission);
			}
		}
	}

	public NetworkableId ProviderID()
	{
		return net.ID;
	}

	public Vector3 ProviderPosition()
	{
		return base.transform.position;
	}

	public BaseEntity Entity()
	{
		return this;
	}
}
