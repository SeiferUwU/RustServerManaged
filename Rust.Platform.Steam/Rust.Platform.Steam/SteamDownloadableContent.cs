using Steamworks;

namespace Rust.Platform.Steam;

public class SteamDownloadableContent : IDownloadableContent
{
	public int AppId { get; }

	public bool IsInstalled => SteamApps.IsDlcInstalled(AppId);

	public SteamDownloadableContent(int appId)
	{
		AppId = appId;
	}
}
