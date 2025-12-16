using System.Threading.Tasks;

namespace Facepunch.Nexus.Time;

public interface IClockProvider
{
	double Timestamp { get; }

	Task Delay(double seconds);
}
