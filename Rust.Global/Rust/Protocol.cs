namespace Rust;

public static class Protocol
{
	public const int network = 2615;

	public const int save = 274;

	public const int report = 1;

	public const int persistance = 8;

	public const int analytics_db = 1;

	public const int ping = 1;

	public static string printable => 2615 + "." + 274 + "." + 1;
}
