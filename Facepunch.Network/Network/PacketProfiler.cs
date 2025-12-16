using System.Runtime.CompilerServices;
using System.Threading;

namespace Network;

public static class PacketProfiler
{
	public static class AnalyticsKeys
	{
		public static string[] MessageType;

		static AnalyticsKeys()
		{
			MessageType = new string[29];
			for (int i = 0; i < 29; i++)
			{
				MessageType[i] = ((Message.Type)i/*cast due to .constrained prefix*/).ToString();
			}
		}
	}

	public static bool enabled = false;

	public static int[] inboundCount = new int[29];

	public static int[] inboundBytes = new int[29];

	public static int[] outboundCount = new int[29];

	public static int[] outboundSum = new int[29];

	public static int[] outboundBytes = new int[29];

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void LogInbound(Message.Type type, int length)
	{
		if (enabled && (int)type < 29)
		{
			inboundCount[(uint)type]++;
			inboundBytes[(uint)type] += length;
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void LogOutbound(int type, int connectionCount, int length)
	{
		if (enabled && type < 29)
		{
			Interlocked.Increment(ref outboundCount[type]);
			Interlocked.Add(ref outboundSum[type], connectionCount);
			Interlocked.Add(ref outboundBytes[type], connectionCount * length);
		}
	}

	public static void Reset()
	{
		for (int i = 0; i < 29; i++)
		{
			inboundCount[i] = 0;
			inboundBytes[i] = 0;
			outboundCount[i] = 0;
			outboundSum[i] = 0;
			outboundBytes[i] = 0;
		}
	}
}
