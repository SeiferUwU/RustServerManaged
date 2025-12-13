using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Network;
using Steamworks;
using Steamworks.Data;
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
			FilePathsData = new byte[218]
			{
				0, 0, 0, 2, 0, 0, 0, 67, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 70, 97, 99, 101, 112, 117,
				110, 99, 104, 46, 83, 116, 101, 97, 109, 78,
				101, 116, 119, 111, 114, 107, 105, 110, 103, 92,
				83, 116, 101, 97, 109, 78, 101, 116, 119, 111,
				114, 107, 105, 110, 103, 46, 67, 108, 105, 101,
				110, 116, 46, 99, 115, 0, 0, 0, 1, 0,
				0, 0, 60, 92, 65, 115, 115, 101, 116, 115,
				92, 80, 108, 117, 103, 105, 110, 115, 92, 70,
				97, 99, 101, 112, 117, 110, 99, 104, 46, 83,
				116, 101, 97, 109, 78, 101, 116, 119, 111, 114,
				107, 105, 110, 103, 92, 83, 116, 101, 97, 109,
				78, 101, 116, 119, 111, 114, 107, 105, 110, 103,
				46, 99, 115, 0, 0, 0, 2, 0, 0, 0,
				67, 92, 65, 115, 115, 101, 116, 115, 92, 80,
				108, 117, 103, 105, 110, 115, 92, 70, 97, 99,
				101, 112, 117, 110, 99, 104, 46, 83, 116, 101,
				97, 109, 78, 101, 116, 119, 111, 114, 107, 105,
				110, 103, 92, 83, 116, 101, 97, 109, 78, 101,
				116, 119, 111, 114, 107, 105, 110, 103, 46, 83,
				101, 114, 118, 101, 114, 46, 99, 115
			},
			TypesData = new byte[204]
			{
				1, 0, 0, 0, 33, 70, 97, 99, 101, 112,
				117, 110, 99, 104, 46, 78, 101, 116, 119, 111,
				114, 107, 124, 83, 116, 101, 97, 109, 78, 101,
				116, 119, 111, 114, 107, 105, 110, 103, 0, 0,
				0, 0, 40, 70, 97, 99, 101, 112, 117, 110,
				99, 104, 46, 78, 101, 116, 119, 111, 114, 107,
				46, 83, 116, 101, 97, 109, 78, 101, 116, 119,
				111, 114, 107, 105, 110, 103, 124, 67, 108, 105,
				101, 110, 116, 1, 0, 0, 0, 33, 70, 97,
				99, 101, 112, 117, 110, 99, 104, 46, 78, 101,
				116, 119, 111, 114, 107, 124, 83, 116, 101, 97,
				109, 78, 101, 116, 119, 111, 114, 107, 105, 110,
				103, 1, 0, 0, 0, 33, 70, 97, 99, 101,
				112, 117, 110, 99, 104, 46, 78, 101, 116, 119,
				111, 114, 107, 124, 83, 116, 101, 97, 109, 78,
				101, 116, 119, 111, 114, 107, 105, 110, 103, 0,
				0, 0, 0, 40, 70, 97, 99, 101, 112, 117,
				110, 99, 104, 46, 78, 101, 116, 119, 111, 114,
				107, 46, 83, 116, 101, 97, 109, 78, 101, 116,
				119, 111, 114, 107, 105, 110, 103, 124, 83, 101,
				114, 118, 101, 114
			},
			TotalFiles = 3,
			TotalTypes = 5,
			IsEditorOnly = false
		};
	}
}
namespace Facepunch.Network;

[ConsoleSystem.Factory("global")]
public static class SteamNetworking
{
	public class Client : global::Network.Client, IConnectionManager
	{
		private ConnectionManager manager;

		private const int bufferSize = 32;

		public override bool IsConnected()
		{
			return manager != null;
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
						SteamNetworkingUtils.AllowWithoutAuth = 2;
						SteamNetworkingUtils.Unencrypted = 2;
						SteamNetworkingUtils.SendRateMax = 1048576;
						SteamNetworkingUtils.SendRateMin = 1048576;
						NetAddress address = NetAddress.From(strURL, (ushort)port);
						manager = SteamNetworkingSockets.ConnectNormal(address, this);
						if (manager == null)
						{
							return false;
						}
						manager.Connection.ConfigureConnectionLanes(Server.lanePriorities, Server.laneWeights);
						base.ConnectedAddress = strURL;
						base.ConnectedPort = port;
						base.ServerName = "";
						base.Connection = new global::Network.Connection();
						MultithreadingInit(null);
						return true;
					}
				}
			}
		}

		protected override bool Receive()
		{
			if (manager.Receive(32, receiveToEnd: false) == 32)
			{
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
						if (sendReasonToServer && manager != null)
						{
							NetWrite netWrite = StartWrite();
							netWrite.PacketID(Message.Type.DisconnectReason);
							netWrite.String(reason);
							netWrite.SendImmediate(new SendInfo(base.Connection)
							{
								method = SendMethod.ReliableUnordered,
								priority = Priority.Immediate
							});
							if (manager != null)
							{
								manager.Close(linger: true, 0, reason);
								manager = null;
							}
						}
						else if (manager != null)
						{
							manager.Close();
							manager = null;
						}
						base.ConnectedAddress = "";
						base.ConnectedPort = 0;
						base.Connection = null;
						OnDisconnected(reason);
					}
				}
			}
		}

		public override void Flush()
		{
			if (manager != null && base.Connection != null)
			{
				((Steamworks.Data.Connection)(uint)base.Connection.guid).Flush();
			}
		}

		public unsafe override void ProcessWrite(NetWrite write)
		{
			ArraySegment<byte> arraySegment = Encrypt(base.Connection, write);
			fixed (byte* array = arraySegment.Array)
			{
				Result result = manager.Connection.SendMessage((IntPtr)array, arraySegment.Offset + arraySegment.Count, ToSteamSendType(write.method, write.priority), ToSteamLaneIndexClient(write.method, write.priority, write.channel));
				if (result != Result.OK)
				{
					UnityEngine.Debug.LogWarning("SendMessage failed (" + result.ToString() + ")");
				}
			}
			write.RemoveReference();
		}

		void IConnectionManager.OnConnecting(ConnectionInfo info)
		{
		}

		void IConnectionManager.OnConnected(ConnectionInfo info)
		{
			base.Connection.guid = manager.Connection.Id;
		}

		void IConnectionManager.OnDisconnected(ConnectionInfo info)
		{
			if (base.Connection != null)
			{
				Disconnect(global::Network.Client.disconnectReason, sendReasonToServer: false);
			}
		}

		unsafe void IConnectionManager.OnMessage(IntPtr data, int datasize, long messageNum, long recvTime, int channel)
		{
			NetRead netRead = Pool.Get<NetRead>();
			netRead.Start(buffer: new Span<byte>(data.ToPointer(), datasize), connection: base.Connection);
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
			b -= 140;
			if (read.connection == null)
			{
				string[] obj = new string[5] { "Ignoring message (", null, null, null, null };
				Message.Type type = (Message.Type)b;
				obj[1] = type.ToString();
				obj[2] = " ";
				obj[3] = b.ToString();
				obj[4] = " connection is null)";
				UnityEngine.Debug.LogWarning(string.Concat(obj));
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
			Message obj2 = StartMessage((Message.Type)b, read);
			if (callbackHandler != null)
			{
				try
				{
					using (TimeWarning.New("OnMessage"))
					{
						callbackHandler.OnNetworkMessage(obj2);
					}
				}
				catch (Exception ex)
				{
					UnityEngine.Debug.LogException(ex);
					Disconnect(ex.Message + "\n" + ex.StackTrace, sendReasonToServer: true);
				}
			}
			Pool.Free(ref obj2);
			read.RemoveReference();
		}

		public override string GetDebug(global::Network.Connection connection)
		{
			if (connection == null)
			{
				connection = base.Connection;
			}
			if (connection == null)
			{
				return string.Empty;
			}
			return ((Steamworks.Data.Connection)(uint)connection.guid).DetailedStatus();
		}

		public override int GetLastPing()
		{
			global::Network.Connection connection = base.Connection;
			if (connection == null)
			{
				return 1;
			}
			return ((Steamworks.Data.Connection)(uint)connection.guid).QuickStatus().Ping;
		}

		public override ulong GetStat(global::Network.Connection connection, StatTypeLong type)
		{
			if (connection == null)
			{
				connection = base.Connection;
			}
			if (connection == null)
			{
				return 0uL;
			}
			ConnectionStatus connectionStatus = ((Steamworks.Data.Connection)(uint)connection.guid).QuickStatus();
			int num = 0;
			switch (type)
			{
			case StatTypeLong.BytesSent_LastSecond:
				num = Mathf.RoundToInt(connectionStatus.OutBytesPerSec);
				break;
			case StatTypeLong.BytesReceived_LastSecond:
				num = Mathf.RoundToInt(connectionStatus.InBytesPerSec);
				break;
			case StatTypeLong.BytesInSendBuffer:
				num = connectionStatus.PendingUnreliable + connectionStatus.PendingReliable;
				break;
			case StatTypeLong.PacketLossLastSecond:
				num = Mathf.RoundToInt(connectionStatus.ConnectionQualityLocal * 100f);
				break;
			}
			return (ulong)num;
		}
	}

	public class Server : global::Network.Server, ISocketManager
	{
		private SocketManager manager;

		private bool relay;

		private const int bufferSize = 32;

		public static readonly int[] lanePriorities = new int[3] { 1, 0, 1 };

		public static readonly ushort[] laneWeights = new ushort[3] { 4, 1, 1 };

		public override string ProtocolId => "sw";

		public Server(bool enableSteamDatagramRelay)
		{
			relay = enableSteamDatagramRelay;
		}

		public override bool IsConnected()
		{
			return manager != null;
		}

		public override bool Start(IServerCallback callbacks)
		{
			lock (readLock)
			{
				lock (writeLock)
				{
					lock (decryptLock)
					{
						if (manager != null)
						{
							throw new Exception("socket not null");
						}
						SteamNetworkingUtils.AllowWithoutAuth = 2;
						SteamNetworkingUtils.Unencrypted = 2;
						SteamNetworkingUtils.SendBufferSize = 4194304;
						SteamNetworkingUtils.SendRateMax = 1048576;
						SteamNetworkingUtils.SendRateMin = 1048576;
						NetAddress netAddress = (string.IsNullOrEmpty(ip) ? NetAddress.AnyIp((ushort)port) : NetAddress.From(ip, (ushort)port));
						UnityEngine.Debug.Log($"Server Creating: {netAddress}");
						if (relay)
						{
							if (SteamNetworkingSockets.GetFakeIP(0, out var address) == Result.OK)
							{
								UnityEngine.Debug.Log($"Server Fake IP: {address}");
							}
							else
							{
								SteamNetworkingSockets.OnFakeIPResult += delegate(NetAddress fakeAddressCallback)
								{
									UnityEngine.Debug.Log($"Server Fake IP: {fakeAddressCallback}");
								};
							}
							manager = SteamNetworkingSockets.CreateRelaySocketFakeIP(0, this);
						}
						else
						{
							manager = SteamNetworkingSockets.CreateNormalSocket(netAddress, this);
						}
						UnityEngine.Debug.Log($"Created Socket: {manager.Socket}");
						MultithreadingInit(callbacks);
						if (manager != null)
						{
							callbackHandler = callbacks;
							return true;
						}
						return false;
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
						if (manager != null)
						{
							Console.WriteLine("[SteamNetworking] Server Shutting Down (" + shutdownMsg + ")");
							manager.Close();
							manager = null;
							base.Stop(shutdownMsg);
						}
					}
				}
			}
		}

		public override void Disconnect(global::Network.Connection cn)
		{
			lock (readLock)
			{
				lock (writeLock)
				{
					lock (decryptLock)
					{
						if (manager != null)
						{
							((Steamworks.Data.Connection)(uint)cn.guid).Close();
							OnDisconnected("Disconnected", cn);
						}
					}
				}
			}
		}

		public override void Kick(global::Network.Connection cn, string message, bool logfile)
		{
			lock (readLock)
			{
				lock (writeLock)
				{
					lock (decryptLock)
					{
						if (manager != null)
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
							((Steamworks.Data.Connection)(uint)cn.guid).Close(linger: true, 0, message);
							OnDisconnected("Kicked: " + message, cn);
						}
					}
				}
			}
		}

		public override void Flush(global::Network.Connection cn)
		{
			if (manager != null && cn != null)
			{
				((Steamworks.Data.Connection)(uint)cn.guid).Flush();
			}
		}

		protected override bool Receive()
		{
			if (manager.Receive(32, receiveToEnd: false) == 32)
			{
				return true;
			}
			return false;
		}

		public override void ProcessWrite(NetWrite write)
		{
			if (DemoConVars.ServerDemosEnabled)
			{
				EnqueueToDemoThread(new DemoQueueItem(write));
			}
			foreach (global::Network.Connection connection in write.connections)
			{
				ProcessWrite(write, connection);
			}
			write.RemoveReference();
		}

		private unsafe void ProcessWrite(NetWrite write, global::Network.Connection connection)
		{
			RecordWriteForConnection(connection, write);
			ArraySegment<byte> arraySegment = Encrypt(connection, write);
			fixed (byte* array = arraySegment.Array)
			{
				Steamworks.Data.Connection connection2 = (uint)connection.guid;
				int num = arraySegment.Offset + arraySegment.Count;
				Result result = connection2.SendMessage((IntPtr)array, num, ToSteamSendType(write.method, write.priority), ToSteamLaneIndexServer(write.method, write.priority, write.channel));
				if (result != Result.OK)
				{
					UnityEngine.Debug.LogWarning("SendMessage failed (" + result.ToString() + ")");
				}
				int num2 = write.PeekPacketID();
				if (num2 >= 140)
				{
					num2 -= 140;
					PacketProfiler.LogOutbound(num2, 1, num);
				}
			}
		}

		public void OnConnecting(Steamworks.Data.Connection cn, ConnectionInfo info)
		{
			cn.Accept();
		}

		public void OnConnected(Steamworks.Data.Connection cn, ConnectionInfo info)
		{
			Result result = cn.ConfigureConnectionLanes(lanePriorities, laneWeights);
			if (result != Result.OK)
			{
				UnityEngine.Debug.LogWarning("ConfigureConnectionLanes failed (" + result.ToString() + ")");
			}
			uint id = cn.Id;
			string ipaddress = info.Address.ToString();
			global::Network.Connection connection = new global::Network.Connection();
			connection.guid = id;
			connection.ipaddress = ipaddress;
			connection.active = true;
			OnNewConnection(connection);
		}

		public void OnDisconnected(Steamworks.Data.Connection cn, ConnectionInfo info)
		{
			cn.Close();
			global::Network.Connection connection = FindConnection((uint)cn);
			if (connection != null)
			{
				OnDisconnected("Disconnected", connection);
			}
		}

		public unsafe void OnMessage(Steamworks.Data.Connection cn, NetIdentity identity, IntPtr data, int size, long messageNum, long recvTime, int channel)
		{
			global::Network.Connection connection = FindConnection((uint)cn);
			if (connection == null)
			{
				return;
			}
			if (size > 10000000)
			{
				Kick(connection, "Packet Size", connection.connected);
				return;
			}
			if (connection.GetPacketsPerSecond() >= global::Network.Server.MaxPacketsPerSecond)
			{
				Kick(connection, "Packet Flooding", connection.connected);
				return;
			}
			connection.AddPacketsPerSecond();
			NetRead netRead = Pool.Get<NetRead>();
			Span<byte> buffer = new Span<byte>(data.ToPointer(), size);
			netRead.Start(connection, buffer);
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
			b -= 140;
			Message obj = StartMessage((Message.Type)b, read);
			if (callbackHandler != null)
			{
				callbackHandler.OnNetworkMessage(obj);
			}
			Pool.Free(ref obj);
			read.RemoveReference();
		}

		public override string GetDebug(global::Network.Connection connection)
		{
			if (connection == null)
			{
				return string.Empty;
			}
			return ((Steamworks.Data.Connection)(uint)connection.guid).DetailedStatus();
		}

		public override int GetAveragePing(global::Network.Connection connection)
		{
			if (connection == null)
			{
				return 0;
			}
			return ((Steamworks.Data.Connection)(uint)connection.guid).QuickStatus().Ping;
		}

		public override ulong GetStat(global::Network.Connection connection, StatTypeLong type)
		{
			if (connection == null)
			{
				return 0uL;
			}
			ConnectionStatus connectionStatus = ((Steamworks.Data.Connection)(uint)connection.guid).QuickStatus();
			int num = 0;
			switch (type)
			{
			case StatTypeLong.BytesSent_LastSecond:
				num = Mathf.RoundToInt(connectionStatus.OutBytesPerSec);
				break;
			case StatTypeLong.BytesReceived_LastSecond:
				num = Mathf.RoundToInt(connectionStatus.InBytesPerSec);
				break;
			case StatTypeLong.BytesInSendBuffer:
				num = connectionStatus.PendingUnreliable + connectionStatus.PendingReliable;
				break;
			case StatTypeLong.PacketLossLastSecond:
				num = Mathf.RoundToInt(connectionStatus.ConnectionQualityLocal * 100f);
				break;
			}
			return (ulong)num;
		}

		public override bool LimitConnectionsPerIP()
		{
			return !relay;
		}
	}

	[ClientVar]
	[ServerVar]
	public static bool steamnagleflush;

	[ClientVar(Help = "Turns on varying levels of debug output for the Steam Networking. This will affect performance. (0 = off, 1 = bug, 2 = error, 3 = important, 4 = warning, 5 = message, 6 = verbose, 7 = debug, 8 = everything)")]
	[ServerVar(Help = "Turns on varying levels of debug output for the Steam Networking. This will affect performance. (0 = off, 1 = bug, 2 = error, 3 = important, 4 = warning, 5 = message, 6 = verbose, 7 = debug, 8 = everything)")]
	public static int steamnetdebug
	{
		get
		{
			return (int)SteamNetworkingUtils.DebugLevel;
		}
		set
		{
			SteamNetworkingUtils.DebugLevel = (NetDebugOutput)value;
		}
	}

	[ClientVar]
	[ServerVar]
	public static int steamnetdebug_ackrtt
	{
		get
		{
			return SteamNetworkingUtils.DebugLevelAckRTT;
		}
		set
		{
			SteamNetworkingUtils.DebugLevelAckRTT = value;
		}
	}

	[ClientVar]
	[ServerVar]
	public static int steamnetdebug_packetdecode
	{
		get
		{
			return SteamNetworkingUtils.DebugLevelPacketDecode;
		}
		set
		{
			SteamNetworkingUtils.DebugLevelPacketDecode = value;
		}
	}

	[ClientVar]
	[ServerVar]
	public static int steamnetdebug_message
	{
		get
		{
			return SteamNetworkingUtils.DebugLevelMessage;
		}
		set
		{
			SteamNetworkingUtils.DebugLevelMessage = value;
		}
	}

	[ClientVar]
	[ServerVar]
	public static int steamnetdebug_packetgaps
	{
		get
		{
			return SteamNetworkingUtils.DebugLevelPacketGaps;
		}
		set
		{
			SteamNetworkingUtils.DebugLevelPacketGaps = value;
		}
	}

	[ClientVar]
	[ServerVar]
	public static int steamnetdebug_p2prendezvous
	{
		get
		{
			return SteamNetworkingUtils.DebugLevelP2PRendezvous;
		}
		set
		{
			SteamNetworkingUtils.DebugLevelP2PRendezvous = value;
		}
	}

	[ClientVar]
	[ServerVar]
	public static int steamnetdebug_sdrrelaypings
	{
		get
		{
			return SteamNetworkingUtils.DebugLevelSDRRelayPings;
		}
		set
		{
			SteamNetworkingUtils.DebugLevelSDRRelayPings = value;
		}
	}

	[ClientVar]
	[ServerVar]
	public static int steamconnectiontimeout
	{
		get
		{
			return SteamNetworkingUtils.ConnectionTimeout;
		}
		set
		{
			SteamNetworkingUtils.ConnectionTimeout = value;
		}
	}

	[ClientVar]
	[ServerVar(Help = "Upper limit of buffered pending bytes to be sent")]
	public static int steamsendbuffer
	{
		get
		{
			return SteamNetworkingUtils.SendBufferSize;
		}
		set
		{
			SteamNetworkingUtils.SendBufferSize = value;
		}
	}

	[ClientVar]
	[ServerVar(Help = "Minimum send rate clamp, 0 is no limit")]
	public static int steamsendratemin
	{
		get
		{
			return SteamNetworkingUtils.SendRateMin;
		}
		set
		{
			SteamNetworkingUtils.SendRateMin = value;
		}
	}

	[ClientVar]
	[ServerVar(Help = "Maxminum send rate clamp, 0 is no limit")]
	public static int steamsendratemax
	{
		get
		{
			return SteamNetworkingUtils.SendRateMax;
		}
		set
		{
			SteamNetworkingUtils.SendRateMax = value;
		}
	}

	[ClientVar]
	[ServerVar(Help = "Nagle time, in microseconds")]
	public static int steamnagletime
	{
		get
		{
			return SteamNetworkingUtils.NagleTime;
		}
		set
		{
			SteamNetworkingUtils.NagleTime = value;
		}
	}

	public static SendType ToSteamSendType(SendMethod method, Priority priority)
	{
		SendType sendType = ((method != SendMethod.Unreliable) ? SendType.Reliable : SendType.Unreliable);
		if (priority == Priority.Immediate)
		{
			sendType |= SendType.NoNagle;
		}
		if (method == SendMethod.Unreliable)
		{
			sendType |= SendType.NoDelay;
		}
		return sendType;
	}

	public static ushort ToSteamLaneIndexServer(SendMethod method, Priority priority, sbyte channel)
	{
		if (priority == Priority.Immediate && (method == SendMethod.Unreliable || method == SendMethod.ReliableUnordered))
		{
			return 1;
		}
		return (ushort)channel;
	}

	public static ushort ToSteamLaneIndexClient(SendMethod method, Priority priority, sbyte channel)
	{
		return (ushort)channel;
	}

	public static void InitRelayNetworkAccess()
	{
		SteamNetworkingUtils.InitRelayNetworkAccess();
	}

	public static void SetDebugFunction()
	{
		SteamNetworkingUtils.OnDebugOutput += SteamNetworkingUtils_OnDebugOutput;
	}

	private static void SteamNetworkingUtils_OnDebugOutput(NetDebugOutput type, string str)
	{
		DebugEx.Log($"[SteamNet_{type}] {str}");
	}

	[ClientVar]
	[ServerVar]
	public static string steamstatus()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("[Steam Identity] " + SteamNetworkingSockets.Identity.ToString());
		NetAddress address;
		Result fakeIP = SteamNetworkingSockets.GetFakeIP(0, out address);
		if (fakeIP == Result.OK)
		{
			NetAddress netAddress = address;
			stringBuilder.AppendLine("[Steam Fake IP] " + netAddress.ToString());
		}
		else
		{
			stringBuilder.AppendLine("[Steam Fake IP] " + fakeIP);
		}
		stringBuilder.AppendLine("[Steam Datagram Relay Status] " + SteamNetworkingUtils.Status);
		return stringBuilder.ToString();
	}

	[ClientVar]
	[ServerVar]
	public static void steamrelayinit()
	{
		InitRelayNetworkAccess();
	}
}
