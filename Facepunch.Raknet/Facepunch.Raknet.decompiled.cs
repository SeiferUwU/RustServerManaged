using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Network;
using UnityEngine;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
[CompilerGenerated]
[EditorBrowsable(EditorBrowsableState.Never)]
[GeneratedCode("Unity.MonoScriptGenerator.MonoScriptInfoGenerator", null)]
internal class UnitySourceGeneratedAssemblyMonoScriptTypes_v1
{
	private struct MonoScriptData
	{
		public byte[] FilePathsData;

		public byte[] TypesData;

		public int TotalTypes;

		public int TotalFiles;

		public bool IsEditorOnly;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static MonoScriptData Get()
	{
		return new MonoScriptData
		{
			FilePathsData = new byte[214]
			{
				0, 0, 0, 2, 0, 0, 0, 42, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 70, 97, 99, 101, 112, 117,
				110, 99, 104, 46, 82, 97, 107, 110, 101, 116,
				92, 78, 97, 116, 105, 118, 101, 46, 99, 115,
				0, 0, 0, 1, 0, 0, 0, 49, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 70, 97, 99, 101, 112, 117,
				110, 99, 104, 46, 82, 97, 107, 110, 101, 116,
				92, 82, 97, 107, 110, 101, 116, 46, 67, 108,
				105, 101, 110, 116, 46, 99, 115, 0, 0, 0,
				1, 0, 0, 0, 42, 92, 65, 115, 115, 101,
				116, 115, 92, 80, 108, 117, 103, 105, 110, 115,
				92, 70, 97, 99, 101, 112, 117, 110, 99, 104,
				46, 82, 97, 107, 110, 101, 116, 92, 82, 97,
				107, 110, 101, 116, 46, 99, 115, 0, 0, 0,
				2, 0, 0, 0, 49, 92, 65, 115, 115, 101,
				116, 115, 92, 80, 108, 117, 103, 105, 110, 115,
				92, 70, 97, 99, 101, 112, 117, 110, 99, 104,
				46, 82, 97, 107, 110, 101, 116, 92, 82, 97,
				107, 110, 101, 116, 46, 83, 101, 114, 118, 101,
				114, 46, 99, 115
			},
			TypesData = new byte[230]
			{
				0, 0, 0, 0, 31, 70, 97, 99, 101, 112,
				117, 110, 99, 104, 46, 78, 101, 116, 119, 111,
				114, 107, 46, 82, 97, 107, 110, 101, 116, 124,
				78, 97, 116, 105, 118, 101, 0, 0, 0, 0,
				43, 70, 97, 99, 101, 112, 117, 110, 99, 104,
				46, 78, 101, 116, 119, 111, 114, 107, 46, 82,
				97, 107, 110, 101, 116, 46, 78, 97, 116, 105,
				118, 101, 124, 82, 97, 107, 110, 101, 116, 83,
				116, 97, 116, 115, 0, 0, 0, 0, 31, 70,
				97, 99, 101, 112, 117, 110, 99, 104, 46, 78,
				101, 116, 119, 111, 114, 107, 46, 82, 97, 107,
				110, 101, 116, 124, 67, 108, 105, 101, 110, 116,
				0, 0, 0, 0, 29, 70, 97, 99, 101, 112,
				117, 110, 99, 104, 46, 78, 101, 116, 119, 111,
				114, 107, 46, 82, 97, 107, 110, 101, 116, 124,
				80, 101, 101, 114, 0, 0, 0, 0, 35, 70,
				97, 99, 101, 112, 117, 110, 99, 104, 46, 78,
				101, 116, 119, 111, 114, 107, 46, 82, 97, 107,
				110, 101, 116, 124, 80, 97, 99, 107, 101, 116,
				84, 121, 112, 101, 0, 0, 0, 0, 31, 70,
				97, 99, 101, 112, 117, 110, 99, 104, 46, 78,
				101, 116, 119, 111, 114, 107, 46, 82, 97, 107,
				110, 101, 116, 124, 83, 101, 114, 118, 101, 114
			},
			TotalFiles = 4,
			TotalTypes = 6,
			IsEditorOnly = false
		};
	}
}
namespace Facepunch.Network.Raknet;

[SuppressUnmanagedCodeSecurity]
public class Native
{
	public enum Metrics
	{
		USER_MESSAGE_BYTES_PUSHED,
		USER_MESSAGE_BYTES_SENT,
		USER_MESSAGE_BYTES_RESENT,
		USER_MESSAGE_BYTES_RECEIVED_PROCESSED,
		USER_MESSAGE_BYTES_RECEIVED_IGNORED,
		ACTUAL_BYTES_SENT,
		ACTUAL_BYTES_RECEIVED,
		RNS_PER_SECOND_METRICS_COUNT
	}

	public enum PacketPriority
	{
		IMMEDIATE_PRIORITY,
		HIGH_PRIORITY,
		MEDIUM_PRIORITY,
		LOW_PRIORITY,
		NUMBER_OF_PRIORITIES
	}

	public struct RaknetStats
	{
		public unsafe fixed ulong valueOverLastSecond[7];

		public unsafe fixed ulong runningTotal[7];

		public ulong connectionStartTime;

		public byte isLimitedByCongestionControl;

		public ulong BPSLimitByCongestionControl;

		public byte isLimitedByOutgoingBandwidthLimit;

		public ulong BPSLimitByOutgoingBandwidthLimit;

		public unsafe fixed uint messageInSendBuffer[4];

		public unsafe fixed double bytesInSendBuffer[4];

		public uint messagesInResendBuffer;

		public ulong bytesInResendBuffer;

		public float packetlossLastSecond;

		public float packetlossTotal;
	}

	[DllImport("RakNet")]
	public static extern IntPtr NET_Create();

	[DllImport("RakNet")]
	public static extern void NET_Close(IntPtr nw);

	[DllImport("RakNet")]
	public static extern int NET_StartClient(IntPtr nw, string hostName, int port, int retries, int retryDelay, int timeout);

	[DllImport("RakNet")]
	public static extern int NET_StartServer(IntPtr nw, string ip, int port, int maxConnections);

	[DllImport("RakNet")]
	public static extern IntPtr NET_LastStartupError(IntPtr nw);

	[DllImport("RakNet")]
	public static extern uint NET_GetReceiveBufferSize(IntPtr nw);

	[DllImport("RakNet")]
	public static extern bool NET_Receive(IntPtr nw);

	[DllImport("RakNet")]
	public static extern ulong NETRCV_GUID(IntPtr nw);

	[DllImport("RakNet")]
	public static extern uint NETRCV_Address(IntPtr nw);

	[DllImport("RakNet")]
	public static extern uint NETRCV_Port(IntPtr nw);

	[DllImport("RakNet")]
	public static extern IntPtr NETRCV_RawData(IntPtr nw);

	[DllImport("RakNet")]
	public static extern int NETRCV_LengthBits(IntPtr nw);

	[DllImport("RakNet")]
	public static extern int NETRCV_UnreadBits(IntPtr nw);

	[DllImport("RakNet")]
	public unsafe static extern bool NETRCV_ReadBytes(IntPtr nw, byte* data, int length);

	[DllImport("RakNet")]
	public static extern void NETRCV_SetReadPointer(IntPtr nw, int bitsOffset);

	[DllImport("RakNet")]
	public static extern void NETSND_Start(IntPtr nw);

	[DllImport("RakNet")]
	public unsafe static extern void NETSND_WriteBytes(IntPtr nw, byte* data, int length);

	[DllImport("RakNet")]
	public static extern uint NETSND_Size(IntPtr nw);

	[DllImport("RakNet")]
	public static extern uint NETSND_Broadcast(IntPtr nw, int priority, int reliability, int channel);

	[DllImport("RakNet")]
	public static extern uint NETSND_Send(IntPtr nw, ulong connectionID, int priority, int reliability, int channel);

	[DllImport("RakNet")]
	public static extern void NET_CloseConnection(IntPtr nw, ulong connectionID);

	[DllImport("RakNet")]
	public static extern IntPtr NET_GetAddress(IntPtr nw, ulong connectionID);

	[DllImport("RakNet")]
	public static extern IntPtr NET_GetStatisticsString(IntPtr nw, ulong connectionID);

	[DllImport("RakNet")]
	public static extern bool NET_GetStatistics(IntPtr nw, ulong connectionID, ref RaknetStats data, int dataLength);

	[DllImport("RakNet")]
	public static extern int NET_GetAveragePing(IntPtr nw, ulong connectionID);

	[DllImport("RakNet")]
	public static extern int NET_GetLastPing(IntPtr nw, ulong connectionID);

	[DllImport("RakNet")]
	public static extern int NET_GetLowestPing(IntPtr nw, ulong connectionID);

	[DllImport("RakNet")]
	public unsafe static extern void NET_SendMessage(IntPtr nw, byte* data, int length, uint adr, ushort port);

	[DllImport("RakNet")]
	public static extern float NETSND_ReadCompressedFloat(IntPtr nw);
}
public class Client : global::Network.Client
{
	private Peer peer;

	public override bool IsConnected()
	{
		return peer != null;
	}

	public override bool Connect(string strURL, int port)
	{
		lock (readLock)
		{
			lock (writeLock)
			{
				lock (decryptLock)
				{
					base.Connect(strURL, port);
					peer = Peer.CreateConnection(strURL, port, 12, 400, 0);
					if (peer == null)
					{
						return false;
					}
					base.ConnectedAddress = strURL;
					base.ConnectedPort = port;
					base.ServerName = "";
					base.Connection = new Connection();
					MultithreadingInit(null);
					return true;
				}
			}
		}
	}

	internal bool HandleRaknetPacket(byte type, NetRead read)
	{
		if (type >= 140)
		{
			return false;
		}
		switch (type)
		{
		case 16:
			if (base.Connection.guid != 0L)
			{
				Console.WriteLine("Multiple PacketType.CONNECTION_REQUEST_ACCEPTED");
			}
			base.Connection.guid = read.guid;
			IncomingStats.Add("Unconnected", "RequestAccepted", read.Length);
			return true;
		case 17:
			Disconnect("Connection Attempt Failed", sendReasonToServer: false);
			return true;
		case 20:
			Disconnect("Server is Full", sendReasonToServer: false);
			return true;
		case 21:
			if (base.Connection != null && base.Connection.guid != read.guid)
			{
				return true;
			}
			Disconnect(global::Network.Client.disconnectReason, sendReasonToServer: false);
			return true;
		case 22:
			if (base.Connection != null && base.Connection.guid != read.guid)
			{
				return true;
			}
			Disconnect("Timed Out", sendReasonToServer: false);
			return true;
		case 23:
			if (base.Connection != null && base.Connection.guid != read.guid)
			{
				return true;
			}
			Disconnect("Connection Banned", sendReasonToServer: false);
			return true;
		default:
			IncomingStats.Add("Unconnected", "Unhandled", read.Length);
			if (base.Connection != null && read.guid != base.Connection.guid)
			{
				UnityEngine.Debug.LogWarning("[CLIENT] Unhandled Raknet packet " + type + " from unknown source");
				return true;
			}
			UnityEngine.Debug.LogWarning("Unhandled Raknet packet " + type);
			return true;
		}
	}

	protected unsafe void HandleMessage()
	{
		NetRead netRead = Pool.Get<NetRead>();
		Span<byte> buffer = new Span<byte>(peer.RawData().ToPointer(), peer.incomingBytesUnread);
		netRead.Start(base.Connection, peer.incomingGUID, buffer);
		if (BaseNetwork.Multithreading)
		{
			EnqueueDecrypt(netRead);
		}
		else
		{
			ProcessDecrypt(netRead);
		}
	}

	public override void ProcessRead(NetRead read)
	{
		RecordReadForConnection(read.connection, read);
		byte b = read.PacketID();
		if (HandleRaknetPacket(b, read))
		{
			read.RemoveReference();
			return;
		}
		if (read.connection != null)
		{
			b -= 140;
			if (read.guid != read.connection.guid)
			{
				IncomingStats.Add("Error", "WrongGuid", read.Length);
				read.RemoveReference();
				return;
			}
			if (b > 28)
			{
				UnityEngine.Debug.LogWarning("Invalid Packet (higher than " + Message.Type.PackedSyncVar.ToString() + ")");
				Disconnect("Invalid Packet (" + b + ") " + read.Length + "b", sendReasonToServer: true);
				read.RemoveReference();
				return;
			}
			Message obj = StartMessage((Message.Type)b, read);
			if (callbackHandler != null)
			{
				try
				{
					using (TimeWarning.New("OnMessage"))
					{
						callbackHandler.OnNetworkMessage(obj);
					}
				}
				catch (Exception ex)
				{
					UnityEngine.Debug.LogException(ex);
					Disconnect(ex.Message + "\n" + ex.StackTrace, sendReasonToServer: true);
				}
			}
			Pool.Free(ref obj);
		}
		read.RemoveReference();
	}

	protected override bool Receive()
	{
		if (peer.Receive())
		{
			HandleMessage();
			return true;
		}
		return false;
	}

	public override void Disconnect(string reason, bool sendReasonToServer)
	{
		lock (readLock)
		{
			lock (writeLock)
			{
				lock (decryptLock)
				{
					if (sendReasonToServer && peer != null)
					{
						NetWrite netWrite = StartWrite();
						netWrite.PacketID(Message.Type.DisconnectReason);
						netWrite.String(reason);
						netWrite.SendImmediate(new SendInfo(base.Connection)
						{
							method = SendMethod.ReliableUnordered,
							priority = Priority.Immediate
						});
					}
					if (peer != null)
					{
						peer.Close();
						peer = null;
					}
					base.ConnectedAddress = "";
					base.ConnectedPort = 0;
					base.Connection = null;
					OnDisconnected(reason);
				}
			}
		}
	}

	public override string GetDebug(Connection connection)
	{
		if (peer == null)
		{
			return "";
		}
		if (connection == null)
		{
			return peer.GetStatisticsString(0uL);
		}
		return peer.GetStatisticsString(connection.guid);
	}

	public override ulong GetStat(Connection connection, StatTypeLong type)
	{
		if (peer == null)
		{
			return 0uL;
		}
		return peer.GetStat(connection, type);
	}

	public override int GetLastPing()
	{
		if (base.Connection == null)
		{
			return 1;
		}
		return peer.GetPingLast(base.Connection.guid);
	}

	public override void ProcessWrite(NetWrite write)
	{
		ArraySegment<byte> arraySegment = Encrypt(base.Connection, write);
		peer.SendStart();
		peer.WriteBytes(arraySegment.Array, 0, arraySegment.Offset + arraySegment.Count);
		peer.SendTo(base.Connection.guid, write.priority, write.method, write.channel);
		write.RemoveReference();
	}
}
[SuppressUnmanagedCodeSecurity]
internal class Peer
{
	public enum PacketPriority
	{
		IMMEDIATE_PRIORITY,
		HIGH_PRIORITY,
		MEDIUM_PRIORITY,
		LOW_PRIORITY
	}

	public enum PacketReliability
	{
		UNRELIABLE,
		UNRELIABLE_SEQUENCED,
		RELIABLE,
		RELIABLE_ORDERED,
		RELIABLE_SEQUENCED
	}

	private IntPtr ptr;

	private static byte[] ByteBuffer = new byte[512];

	public virtual ulong incomingGUID
	{
		get
		{
			Check();
			return Native.NETRCV_GUID(ptr);
		}
	}

	public virtual uint incomingAddressInt
	{
		get
		{
			Check();
			return Native.NETRCV_Address(ptr);
		}
	}

	public virtual uint incomingPort
	{
		get
		{
			Check();
			return Native.NETRCV_Port(ptr);
		}
	}

	public string incomingAddress => GetAddress(incomingGUID);

	public virtual int incomingBits
	{
		get
		{
			Check();
			return Native.NETRCV_LengthBits(ptr);
		}
	}

	public virtual int incomingBitsUnread
	{
		get
		{
			Check();
			return Native.NETRCV_UnreadBits(ptr);
		}
	}

	public virtual int incomingBytes => incomingBits / 8;

	public virtual int incomingBytesUnread => incomingBitsUnread / 8;

	public static Peer CreateServer(string ip, int port, int maxConnections)
	{
		Peer peer = new Peer();
		peer.ptr = Native.NET_Create();
		if (Native.NET_StartServer(peer.ptr, ip, port, maxConnections) == 0)
		{
			return peer;
		}
		peer.Close();
		string text = StringFromPointer(Native.NET_LastStartupError(peer.ptr));
		UnityEngine.Debug.LogWarning("Couldn't create server on port " + port + " (" + text + ")");
		return null;
	}

	public static Peer CreateConnection(string hostname, int port, int retries, int retryDelay, int timeout)
	{
		Peer peer = new Peer();
		peer.ptr = Native.NET_Create();
		if (Native.NET_StartClient(peer.ptr, hostname, port, retries, retryDelay, timeout) == 0)
		{
			return peer;
		}
		string text = StringFromPointer(Native.NET_LastStartupError(peer.ptr));
		UnityEngine.Debug.LogWarning("Couldn't connect to server " + hostname + ":" + port + " (" + text + ")");
		peer.Close();
		peer = null;
		return null;
	}

	public void Close()
	{
		if (ptr != IntPtr.Zero)
		{
			Native.NET_Close(ptr);
			ptr = IntPtr.Zero;
		}
	}

	public uint PendingReceiveCount()
	{
		if (ptr == IntPtr.Zero)
		{
			return 0u;
		}
		return Native.NET_GetReceiveBufferSize(ptr);
	}

	public bool Receive()
	{
		if (ptr == IntPtr.Zero)
		{
			return false;
		}
		return Native.NET_Receive(ptr);
	}

	public virtual void SetReadPos(int bitsOffset)
	{
		Check();
		Native.NETRCV_SetReadPointer(ptr, bitsOffset);
	}

	protected unsafe virtual bool Read(byte* data, int length)
	{
		Check();
		return Native.NETRCV_ReadBytes(ptr, data, length);
	}

	public unsafe int ReadBytes(byte[] buffer, int offset, int length)
	{
		fixed (byte* ptr = buffer)
		{
			if (!Read(ptr + offset, length))
			{
				return 0;
			}
		}
		return length;
	}

	public unsafe byte ReadByte()
	{
		fixed (byte* byteBuffer = ByteBuffer)
		{
			if (!Read(byteBuffer, 1))
			{
				return 0;
			}
		}
		return ByteBuffer[0];
	}

	public virtual IntPtr RawData()
	{
		Check();
		return Native.NETRCV_RawData(ptr);
	}

	public unsafe int ReadBytes(MemoryStream memoryStream, int length)
	{
		if (memoryStream.Capacity < length)
		{
			memoryStream.Capacity = length + 32;
		}
		fixed (byte* buffer = memoryStream.GetBuffer())
		{
			memoryStream.SetLength(memoryStream.Capacity);
			if (!Read(buffer, length))
			{
				return 0;
			}
			memoryStream.SetLength(length);
		}
		return length;
	}

	public virtual void SendStart()
	{
		Check();
		Native.NETSND_Start(ptr);
	}

	public unsafe void WriteByte(byte val)
	{
		Write(&val, 1);
	}

	public unsafe void WriteBytes(byte[] val, int offset, int length)
	{
		fixed (byte* ptr = val)
		{
			Write(ptr + offset, length);
		}
	}

	public unsafe void WriteBytes(byte[] val)
	{
		fixed (byte* data = val)
		{
			Write(data, val.Length);
		}
	}

	public void WriteBytes(MemoryStream stream)
	{
		WriteBytes(stream.GetBuffer(), 0, (int)stream.Length);
	}

	protected unsafe virtual void Write(byte* data, int size)
	{
		if (size != 0 && data != null)
		{
			Check();
			Native.NETSND_WriteBytes(ptr, data, size);
		}
	}

	public virtual uint SendBroadcast(Priority priority, SendMethod reliability, sbyte channel)
	{
		Check();
		return Native.NETSND_Broadcast(ptr, ToRaknetPriority(priority), ToRaknetPacketReliability(reliability), channel);
	}

	public virtual uint SendTo(ulong guid, Priority priority, SendMethod reliability, sbyte channel)
	{
		Check();
		return Native.NETSND_Send(ptr, guid, ToRaknetPriority(priority), ToRaknetPacketReliability(reliability), channel);
	}

	public unsafe void SendUnconnectedMessage(byte* data, int length, uint adr, ushort port)
	{
		Check();
		Native.NET_SendMessage(ptr, data, length, adr, port);
	}

	public string GetAddress(ulong guid)
	{
		Check();
		return StringFromPointer(Native.NET_GetAddress(ptr, guid));
	}

	private static string StringFromPointer(IntPtr p)
	{
		if (p == IntPtr.Zero)
		{
			return string.Empty;
		}
		return Marshal.PtrToStringAnsi(p);
	}

	public int ToRaknetPriority(Priority priority)
	{
		return priority switch
		{
			Priority.Immediate => 0, 
			Priority.Normal => 2, 
			_ => 2, 
		};
	}

	public int ToRaknetPacketReliability(SendMethod reliability)
	{
		return reliability switch
		{
			SendMethod.Reliable => 3, 
			SendMethod.ReliableUnordered => 2, 
			SendMethod.Unreliable => 0, 
			_ => 3, 
		};
	}

	public void Kick(Connection connection)
	{
		Check();
		Native.NET_CloseConnection(ptr, connection.guid);
	}

	protected virtual void Check()
	{
		if (ptr == IntPtr.Zero)
		{
			throw new NullReferenceException("Peer has already shut down!");
		}
	}

	public virtual string GetStatisticsString(ulong guid)
	{
		Check();
		return $"Average Ping:\t\t{GetPingAverage(guid)}\nLast Ping:\t\t{GetPingLast(guid)}\nLowest Ping:\t\t{GetPingLowest(guid)}\n{StringFromPointer(Native.NET_GetStatisticsString(ptr, guid))}";
	}

	public virtual int GetPingAverage(ulong guid)
	{
		Check();
		return Native.NET_GetAveragePing(ptr, guid);
	}

	public virtual int GetPingLast(ulong guid)
	{
		Check();
		return Native.NET_GetLastPing(ptr, guid);
	}

	public virtual int GetPingLowest(ulong guid)
	{
		Check();
		return Native.NET_GetLowestPing(ptr, guid);
	}

	public unsafe virtual Native.RaknetStats GetStatistics(ulong guid)
	{
		Check();
		Native.RaknetStats data = default(Native.RaknetStats);
		int dataLength = sizeof(Native.RaknetStats);
		if (!Native.NET_GetStatistics(ptr, guid, ref data, dataLength))
		{
			UnityEngine.Debug.Log("NET_GetStatistics:  Wrong size " + dataLength);
		}
		return data;
	}

	public unsafe virtual ulong GetStat(Connection connection, BaseNetwork.StatTypeLong type)
	{
		Check();
		Native.RaknetStats raknetStats = ((connection == null) ? GetStatistics(0uL) : GetStatistics(connection.guid));
		switch (type)
		{
		case BaseNetwork.StatTypeLong.BytesReceived:
			return raknetStats.runningTotal[6];
		case BaseNetwork.StatTypeLong.BytesReceived_LastSecond:
			return raknetStats.valueOverLastSecond[6];
		case BaseNetwork.StatTypeLong.BytesSent:
			return raknetStats.runningTotal[5];
		case BaseNetwork.StatTypeLong.BytesSent_LastSecond:
			return raknetStats.valueOverLastSecond[5];
		case BaseNetwork.StatTypeLong.BytesInSendBuffer:
			return (ulong)raknetStats.bytesInSendBuffer;
		case BaseNetwork.StatTypeLong.BytesInResendBuffer:
			return raknetStats.bytesInResendBuffer;
		case BaseNetwork.StatTypeLong.PacketLossAverage:
			return (ulong)raknetStats.packetlossTotal * 10000;
		case BaseNetwork.StatTypeLong.PacketLossLastSecond:
			return (ulong)raknetStats.packetlossLastSecond * 10000;
		case BaseNetwork.StatTypeLong.ThrottleBytes:
			if (raknetStats.isLimitedByCongestionControl == 0)
			{
				return 0uL;
			}
			return raknetStats.BPSLimitByCongestionControl;
		default:
			return 0uL;
		}
	}
}
public static class PacketType
{
	public const byte NEW_INCOMING_CONNECTION = 19;

	public const byte CONNECTION_REQUEST_ACCEPTED = 16;

	public const byte CONNECTION_ATTEMPT_FAILED = 17;

	public const byte DISCONNECTION_NOTIFICATION = 21;

	public const byte NO_FREE_INCOMING_CONNECTIONS = 20;

	public const byte CONNECTION_LOST = 22;

	public const byte CONNECTION_BANNED = 23;
}
public class Server : global::Network.Server
{
	private Peer peer;

	public override string ProtocolId => "rak";

	public override bool IsConnected()
	{
		return peer != null;
	}

	public override bool Start(IServerCallback callbacks)
	{
		lock (readLock)
		{
			lock (writeLock)
			{
				lock (decryptLock)
				{
					peer = Peer.CreateServer(ip, port, 1024);
					if (peer == null)
					{
						return false;
					}
					callbackHandler = callbacks;
					MultithreadingInit(callbacks);
					return true;
				}
			}
		}
	}

	public override void Stop(string shutdownMsg)
	{
		lock (readLock)
		{
			lock (writeLock)
			{
				lock (decryptLock)
				{
					if (peer == null)
					{
						return;
					}
					Console.WriteLine("[Raknet] Server Shutting Down (" + shutdownMsg + ")");
					using (TimeWarning.New("ServerStop"))
					{
						peer.Close();
						peer = null;
						base.Stop(shutdownMsg);
					}
				}
			}
		}
	}

	public override void Disconnect(Connection cn)
	{
		lock (readLock)
		{
			lock (writeLock)
			{
				lock (decryptLock)
				{
					if (peer != null)
					{
						peer.Kick(cn);
						OnDisconnected("Disconnected", cn);
					}
				}
			}
		}
	}

	public override void Kick(Connection cn, string message, bool logfile)
	{
		lock (readLock)
		{
			lock (writeLock)
			{
				lock (decryptLock)
				{
					if (peer != null)
					{
						NetWrite netWrite = StartWrite();
						netWrite.PacketID(Message.Type.DisconnectReason);
						netWrite.String(message);
						netWrite.SendImmediate(new SendInfo(cn)
						{
							method = SendMethod.ReliableUnordered,
							priority = Priority.Immediate
						});
						string text = cn.ToString() + " kicked: " + message;
						if (logfile)
						{
							DebugEx.LogWarning(text);
						}
						else
						{
							Console.WriteLine(text);
						}
						peer.Kick(cn);
						OnDisconnected("Kicked: " + message, cn);
					}
				}
			}
		}
	}

	internal bool HandleRaknetPacket(byte type, NetRead read)
	{
		if (type >= 140)
		{
			return false;
		}
		switch (type)
		{
		case 19:
			using (TimeWarning.New("OnNewConnection", 20))
			{
				OnNewConnection(read.guid, read.ipaddress);
			}
			return true;
		case 21:
			if (read.connection != null)
			{
				using (TimeWarning.New("OnDisconnected", 20))
				{
					OnDisconnected("Disconnected", read.connection);
				}
			}
			return true;
		case 22:
			if (read.connection != null)
			{
				using (TimeWarning.New("OnDisconnected (timed out)", 20))
				{
					OnDisconnected("Timed Out", read.connection);
				}
			}
			return true;
		default:
			return true;
		}
	}

	private unsafe void HandleMessage()
	{
		if (peer.incomingBytesUnread > 10000000)
		{
			return;
		}
		Connection connection = FindConnection(peer.incomingGUID);
		if (connection != null)
		{
			if (connection.GetPacketsPerSecond() >= global::Network.Server.MaxPacketsPerSecond)
			{
				Kick(connection, "Packet Flooding", connection.connected);
				return;
			}
			connection.AddPacketsPerSecond();
		}
		NetRead netRead = Pool.Get<NetRead>();
		Span<byte> buffer = new Span<byte>(peer.RawData().ToPointer(), peer.incomingBytesUnread);
		if (connection != null)
		{
			netRead.Start(connection, buffer);
		}
		else
		{
			netRead.Start(peer.incomingGUID, peer.incomingAddress, buffer);
		}
		if (BaseNetwork.Multithreading)
		{
			EnqueueDecrypt(netRead);
		}
		else
		{
			ProcessDecrypt(netRead);
		}
	}

	public override void ProcessRead(NetRead read)
	{
		byte b = read.PacketID();
		if (HandleRaknetPacket(b, read))
		{
			read.RemoveReference();
			return;
		}
		if (read.connection != null)
		{
			b -= 140;
			Message obj = StartMessage((Message.Type)b, read);
			if (callbackHandler != null)
			{
				callbackHandler.OnNetworkMessage(obj);
			}
			Pool.Free(ref obj);
		}
		read.RemoveReference();
	}

	protected override bool Receive()
	{
		if (peer.Receive())
		{
			HandleMessage();
			return true;
		}
		return false;
	}

	public override string GetDebug(Connection connection)
	{
		if (peer == null)
		{
			return string.Empty;
		}
		if (connection == null)
		{
			return peer.GetStatisticsString(0uL);
		}
		return peer.GetStatisticsString(connection.guid);
	}

	public bool TryGetConnectionStats(Connection connection, out Native.RaknetStats stats)
	{
		if (peer == null)
		{
			stats = default(Native.RaknetStats);
			return false;
		}
		stats = peer.GetStatistics(connection.guid);
		return true;
	}

	public override int GetAveragePing(Connection connection)
	{
		if (peer == null)
		{
			return 0;
		}
		return peer.GetPingAverage(connection.guid);
	}

	public int GetLatestPing(Connection connection)
	{
		if (peer == null)
		{
			return 0;
		}
		return peer.GetPingLast(connection.guid);
	}

	public override ulong GetStat(Connection connection, StatTypeLong type)
	{
		if (peer == null)
		{
			return 0uL;
		}
		return peer.GetStat(connection, type);
	}

	public override void ProcessWrite(NetWrite write)
	{
		if (DemoConVars.ServerDemosEnabled)
		{
			EnqueueToDemoThread(new DemoQueueItem(write));
		}
		foreach (Connection connection in write.connections)
		{
			ProcessWrite(write, connection);
		}
		write.RemoveReference();
	}

	private void ProcessWrite(NetWrite write, Connection connection)
	{
		RecordWriteForConnection(connection, write);
		ArraySegment<byte> arraySegment = Encrypt(connection, write);
		peer.SendStart();
		int length = arraySegment.Offset + arraySegment.Count;
		peer.WriteBytes(arraySegment.Array, 0, length);
		peer.SendTo(connection.guid, write.priority, write.method, write.channel);
		int num = write.PeekPacketID();
		if (num >= 140)
		{
			num -= 140;
			PacketProfiler.LogOutbound(num, 1, length);
		}
	}
}
