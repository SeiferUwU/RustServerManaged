using System;
using Facepunch;
using Rust.Demo;
using UnityEngine;

namespace Network;

public class DemoClient : Client, IDisposable
{
	protected Reader demoFile;

	public override bool IsPlaying => true;

	public bool PlayingFinished => demoFile.IsFinished;

	public DemoClient(Reader demoFile)
	{
		this.demoFile = demoFile;
		MultithreadingInit(null);
	}

	public virtual void Dispose()
	{
		demoFile?.Stop();
		demoFile = null;
	}

	public override bool IsConnected()
	{
		return true;
	}

	public void UpdatePlayback(long frameTime)
	{
		if (!PlayingFinished)
		{
			demoFile.Progress(frameTime);
			while (!demoFile.IsFinished && PlaybackPacket())
			{
			}
		}
	}

	private bool PlaybackPacket()
	{
		Packet packet = demoFile.ReadPacket();
		if (!packet.isValid)
		{
			return false;
		}
		HandleMessage(new Span<byte>(packet.Data, 0, packet.Size));
		return IsPlaying;
	}

	private void HandleMessage(Span<byte> buffer)
	{
		NetRead netRead = Pool.Get<NetRead>();
		netRead.Start(0uL, string.Empty, buffer);
		Decrypt(netRead.connection, netRead);
		byte b = netRead.PacketID();
		if (b < 140)
		{
			netRead.RemoveReference();
			return;
		}
		b -= 140;
		if (b > 28)
		{
			Debug.LogWarning("Invalid Packet (higher than " + Message.Type.PackedSyncVar.ToString() + ")");
			Disconnect($"Invalid Packet ({b}) {buffer.Length}b");
			netRead.RemoveReference();
			return;
		}
		Message obj = StartMessage((Message.Type)b, netRead);
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
				Debug.LogException(ex);
				if (!IsPlaying)
				{
					Disconnect(ex.Message + "\n" + ex.StackTrace);
				}
			}
		}
		Pool.Free(ref obj);
		netRead.RemoveReference();
	}
}
