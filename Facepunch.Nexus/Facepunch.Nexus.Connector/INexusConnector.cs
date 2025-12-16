using System.Threading.Tasks;
using Facepunch.Nexus.Models;

namespace Facepunch.Nexus.Connector;

public interface INexusConnector
{
	Task<NexusListing> ListNexuses(string publicKey, NexusRealm realm);

	Task<NexusDetails> GetNexus(int nexusId);
}
