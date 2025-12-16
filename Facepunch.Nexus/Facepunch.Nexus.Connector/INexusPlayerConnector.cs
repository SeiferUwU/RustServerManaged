using System.Threading.Tasks;
using Facepunch.Nexus.Models;

namespace Facepunch.Nexus.Connector;

internal interface INexusPlayerConnector : INexusConnector
{
	Task<PlayerDetails> GetPlayerDetails(int nexusId, string playerAuthToken);
}
