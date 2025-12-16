using System;
using System.Collections.Generic;
using ConVar;
using Facepunch;
using UnityEngine;

public class NPCMissionProvider : NPCTalking, IMissionProvider
{
	public MissionManifest manifest;

	public GameObjectRef MarkerPrefab;

	public BaseMission[] FallbackMissions = new BaseMission[0];

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

	public override void ServerInit()
	{
		base.ServerInit();
		if (MarkerPrefab != null && MarkerPrefab.isValid)
		{
			List<BaseMission> list = Facepunch.Pool.Get<List<BaseMission>>();
			ConversationData[] array = conversations;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].FindAllMissionAssignments(list);
			}
			BaseMission[] fallbackMissions = FallbackMissions;
			foreach (BaseMission item in fallbackMissions)
			{
				list.Add(item);
			}
			if (list.Count > 0)
			{
				MapMarkerMissionProvider obj = GameManager.server.CreateEntity(MarkerPrefab.resourcePath, base.transform.position, base.transform.rotation) as MapMarkerMissionProvider;
				obj.AssignMissions(list, GetProviderToken());
				obj.Spawn();
			}
			Facepunch.Pool.FreeUnmanaged(ref missions);
		}
	}

	private string GetProviderToken()
	{
		ConversationData[] array = conversations;
		int num = 0;
		if (num < array.Length)
		{
			return array[num].providerNameTranslated.token;
		}
		return string.Empty;
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

	public bool ContainsSpeech(string speech)
	{
		ConversationData[] array = conversations;
		for (int i = 0; i < array.Length; i++)
		{
			ConversationData.SpeechNode[] speeches = array[i].speeches;
			for (int j = 0; j < speeches.Length; j++)
			{
				if (speeches[j].shortname == speech)
				{
					return true;
				}
			}
		}
		return false;
	}

	public string IntroOverride(string overrideSpeech)
	{
		if (!ContainsSpeech(overrideSpeech))
		{
			return "intro";
		}
		return overrideSpeech;
	}

	public override string GetConversationStartSpeech(BasePlayer player)
	{
		string text = "";
		foreach (BaseMission.MissionInstance mission in player.missions)
		{
			if (mission.status == BaseMission.MissionStatus.Active)
			{
				text = IntroOverride("missionactive");
			}
			if (mission.status == BaseMission.MissionStatus.Completed && mission.providerID == ProviderID() && (float)(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - mission.endTimeUtcSeconds) * ConVar.Time.missiontimerscale < 5f)
			{
				text = IntroOverride("missionreturn");
			}
		}
		if (string.IsNullOrEmpty(text))
		{
			text = base.GetConversationStartSpeech(player);
		}
		return text;
	}

	public override void OnConversationAction(BasePlayer player, string action)
	{
		if (action.StartsWith("assignmission "))
		{
			int num = action.IndexOf(" ");
			BaseMission fromShortName = MissionManifest.GetFromShortName(action.Substring(num + 1));
			if ((bool)fromShortName)
			{
				BaseMission.AssignMission(player, this, fromShortName);
			}
		}
		base.OnConversationAction(player, action);
	}
}
