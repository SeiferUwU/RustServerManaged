using System.Collections.Generic;

namespace Facepunch.Ping;

public interface IPingEstimateResults
{
	IPingRegionResult GetRegionByCode(StringView code);

	IEnumerable<IPingRegionResult> GetAllRegions();
}
