using System.Collections.Generic;

namespace Facepunch.Nexus.Models;

internal struct RegisterTransfersRequest
{
	public IEnumerable<string> PlayerIds { get; set; }

	public string ToZoneKey { get; set; }
}
