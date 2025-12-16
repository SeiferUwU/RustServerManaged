namespace Facepunch.Ping;

[JsonModel]
public interface IPingRegionResult
{
	string RegionCode { get; }

	string RegionShortname { get; }

	int Ping { get; }
}
