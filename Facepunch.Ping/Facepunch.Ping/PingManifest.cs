using System.Collections.Generic;
using Newtonsoft.Json;

namespace Facepunch.Ping;

[JsonModel]
public class PingManifest
{
	[JsonProperty("addresses")]
	public List<PingAddress> Addresses { get; set; } = new List<PingAddress>();
}
