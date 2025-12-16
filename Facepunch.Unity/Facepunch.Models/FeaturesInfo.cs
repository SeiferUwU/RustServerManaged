using Newtonsoft.Json;

namespace Facepunch.Models;

[JsonModel]
public class FeaturesInfo
{
	[JsonProperty("client_analytics")]
	public bool ClientAnalytics;

	[JsonProperty("server_analytics")]
	public bool ServerAnalytics;

	[JsonProperty("minimum_secure_encryption")]
	public int MinimumSecureEncryption = 2;
}
