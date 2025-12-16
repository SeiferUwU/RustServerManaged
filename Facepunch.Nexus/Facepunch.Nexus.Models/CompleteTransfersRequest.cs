using System.Collections.Generic;

namespace Facepunch.Nexus.Models;

internal struct CompleteTransfersRequest
{
	public IEnumerable<string> PlayerIds { get; set; }
}
