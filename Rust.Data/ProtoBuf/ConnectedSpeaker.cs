using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class ConnectedSpeaker : IDisposable, Pool.IPooled, IProto<ConnectedSpeaker>, IProto
{
	[NonSerialized]
	public NetworkableId connectedTo;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(ConnectedSpeaker instance)
	{
		if (instance.ShouldPool)
		{
			instance.connectedTo = default(NetworkableId);
			Pool.Free(ref instance);
		}
	}

	public void ResetToPool()
	{
		ResetToPool(this);
	}

	public virtual void Dispose()
	{
		if (!ShouldPool)
		{
			throw new Exception("Trying to dispose ConnectedSpeaker with ShouldPool set to false!");
		}
		if (!_disposed)
		{
			ResetToPool();
			_disposed = true;
		}
	}

	public virtual void EnterPool()
	{
		_disposed = true;
	}

	public virtual void LeavePool()
	{
		_disposed = false;
	}

	public void CopyTo(ConnectedSpeaker instance)
	{
		instance.connectedTo = connectedTo;
	}

	public ConnectedSpeaker Copy()
	{
		ConnectedSpeaker connectedSpeaker = Pool.Get<ConnectedSpeaker>();
		CopyTo(connectedSpeaker);
		return connectedSpeaker;
	}

	public static ConnectedSpeaker Deserialize(BufferStream stream)
	{
		ConnectedSpeaker connectedSpeaker = Pool.Get<ConnectedSpeaker>();
		Deserialize(stream, connectedSpeaker, isDelta: false);
		return connectedSpeaker;
	}

	public static ConnectedSpeaker DeserializeLengthDelimited(BufferStream stream)
	{
		ConnectedSpeaker connectedSpeaker = Pool.Get<ConnectedSpeaker>();
		DeserializeLengthDelimited(stream, connectedSpeaker, isDelta: false);
		return connectedSpeaker;
	}

	public static ConnectedSpeaker DeserializeLength(BufferStream stream, int length)
	{
		ConnectedSpeaker connectedSpeaker = Pool.Get<ConnectedSpeaker>();
		DeserializeLength(stream, length, connectedSpeaker, isDelta: false);
		return connectedSpeaker;
	}

	public static ConnectedSpeaker Deserialize(byte[] buffer)
	{
		ConnectedSpeaker connectedSpeaker = Pool.Get<ConnectedSpeaker>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, connectedSpeaker, isDelta: false);
		return connectedSpeaker;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, ConnectedSpeaker previous)
	{
		if (previous == null)
		{
			Serialize(stream, this);
		}
		else
		{
			SerializeDelta(stream, this, previous);
		}
	}

	public virtual void ReadFromStream(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void ReadFromStream(BufferStream stream, int size, bool isDelta = false)
	{
		DeserializeLength(stream, size, this, isDelta);
	}

	public static ConnectedSpeaker Deserialize(BufferStream stream, ConnectedSpeaker instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.connectedTo = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case -1:
			case 0:
				return instance;
			}
			Key key = ProtocolParser.ReadKey((byte)num, stream);
			_ = key.Field;
			ProtocolParser.SkipKey(stream, key);
		}
	}

	public static ConnectedSpeaker DeserializeLengthDelimited(BufferStream stream, ConnectedSpeaker instance, bool isDelta)
	{
		long num = ProtocolParser.ReadUInt32(stream);
		num += stream.Position;
		while (stream.Position < num)
		{
			int num2 = stream.ReadByte();
			switch (num2)
			{
			case -1:
				throw new EndOfStreamException();
			case 8:
				instance.connectedTo = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			}
			Key key = ProtocolParser.ReadKey((byte)num2, stream);
			_ = key.Field;
			ProtocolParser.SkipKey(stream, key);
		}
		if (stream.Position != num)
		{
			throw new ProtocolBufferException("Read past max limit");
		}
		return instance;
	}

	public static ConnectedSpeaker DeserializeLength(BufferStream stream, int length, ConnectedSpeaker instance, bool isDelta)
	{
		long num = stream.Position + length;
		while (stream.Position < num)
		{
			int num2 = stream.ReadByte();
			switch (num2)
			{
			case -1:
				throw new EndOfStreamException();
			case 8:
				instance.connectedTo = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			}
			Key key = ProtocolParser.ReadKey((byte)num2, stream);
			_ = key.Field;
			ProtocolParser.SkipKey(stream, key);
		}
		if (stream.Position != num)
		{
			throw new ProtocolBufferException("Read past max limit");
		}
		return instance;
	}

	public static void SerializeDelta(BufferStream stream, ConnectedSpeaker instance, ConnectedSpeaker previous)
	{
		stream.WriteByte(8);
		ProtocolParser.WriteUInt64(stream, instance.connectedTo.Value);
	}

	public static void Serialize(BufferStream stream, ConnectedSpeaker instance)
	{
		if (instance.connectedTo != default(NetworkableId))
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.connectedTo.Value);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref connectedTo.Value);
	}
}
