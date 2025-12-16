namespace Facepunch.Nexus.Models;

public struct ClanJoinedEvent
{
	public long ClanId { get; set; }

	public string PlayerId { get; set; }
}
