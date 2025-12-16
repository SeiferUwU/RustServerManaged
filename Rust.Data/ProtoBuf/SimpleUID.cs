using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class SimpleUID : IDisposable, Pool.IPooled, IProto<SimpleUID>, IProto
{
	[NonSerialized]
	public NetworkableId uid;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(SimpleUID instance)
	{
		if (instance.ShouldPool)
		{
			instance.uid = default(NetworkableId);
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
			throw new Exception("Trying to dispose SimpleUID with ShouldPool set to false!");
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

	public void CopyTo(SimpleUID instance)
	{
		instance.uid = uid;
	}

	public SimpleUID Copy()
	{
		SimpleUID simpleUID = Pool.Get<SimpleUID>();
		CopyTo(simpleUID);
		return simpleUID;
	}

	public static SimpleUID Deserialize(BufferStream stream)
	{
		SimpleUID simpleUID = Pool.Get<SimpleUID>();
		Deserialize(stream, simpleUID, isDelta: false);
		return simpleUID;
	}

	public static SimpleUID DeserializeLengthDelimited(BufferStream stream)
	{
		SimpleUID simpleUID = Pool.Get<SimpleUID>();
		DeserializeLengthDelimited(stream, simpleUID, isDelta: false);
		return simpleUID;
	}

	public static SimpleUID DeserializeLength(BufferStream stream, int length)
	{
		SimpleUID simpleUID = Pool.Get<SimpleUID>();
		DeserializeLength(stream, length, simpleUID, isDelta: false);
		return simpleUID;
	}

	public static SimpleUID Deserialize(byte[] buffer)
	{
		SimpleUID simpleUID = Pool.Get<SimpleUID>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, simpleUID, isDelta: false);
		return simpleUID;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, SimpleUID previous)
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

	public static SimpleUID Deserialize(BufferStream stream, SimpleUID instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.uid = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static SimpleUID DeserializeLengthDelimited(BufferStream stream, SimpleUID instance, bool isDelta)
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
				instance.uid = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static SimpleUID DeserializeLength(BufferStream stream, int length, SimpleUID instance, bool isDelta)
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
				instance.uid = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static void SerializeDelta(BufferStream stream, SimpleUID instance, SimpleUID previous)
	{
		stream.WriteByte(8);
		ProtocolParser.WriteUInt64(stream, instance.uid.Value);
	}

	public static void Serialize(BufferStream stream, SimpleUID instance)
	{
		if (instance.uid != default(NetworkableId))
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.uid.Value);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref uid.Value);
	}
}
