using Steamworks.Data;

namespace Rust.Platform.Steam;

public class SteamAchievement : IAchievement
{
	private Achievement _achievement;

	public string Key => _achievement.Name;

	public bool IsUnlocked => _achievement.State;

	internal SteamAchievement(Achievement achievement)
	{
		_achievement = achievement;
	}

	public void Unlock()
	{
		_achievement.Trigger();
	}
}
