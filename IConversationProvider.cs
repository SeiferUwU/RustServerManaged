using System.Collections.Generic;

public interface IConversationProvider
{
	void GetGenericMissionList(List<BaseMission> foundMissions);

	bool ProviderBusy();
}
