namespace ConVar;

[Factory("batching")]
public class Batching : ConsoleSystem
{
	[ClientVar(ClientAdmin = true)]
	public static bool enabled = true;

	[ClientVar(ClientAdmin = true)]
	public static bool renderer_threading = true;

	[ClientVar(ClientAdmin = true)]
	public static int renderer_capacity = 30000;

	[ClientVar(ClientAdmin = true)]
	public static int renderer_vertices = 1000;

	[ClientVar(ClientAdmin = true)]
	public static int renderer_submeshes = 1;

	[ClientVar]
	public static bool batch_industrial_pipes = false;

	[ServerVar]
	[ClientVar]
	public static int verbose = 0;
}
