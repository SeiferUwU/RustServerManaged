using System.Collections.Generic;

namespace Facepunch.Nexus.Models;

public struct ClanDisbandedEvent
{
	public long ClanId { get; set; }

	public List<string> Members { get; set; }
}
