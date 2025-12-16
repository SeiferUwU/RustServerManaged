using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class Motorboat : IDisposable, Pool.IPooled, IProto<Motorboat>, IProto
{
	[NonSerialized]
	public NetworkableId storageid;

	[NonSerialized]
	public NetworkableId fuelStorageID;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(Motorboat instance)
	{
		if (instance.ShouldPool)
		{
			instance.storageid = default(NetworkableId);
			instance.fuelStorageID = default(NetworkableId);
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
			throw new Exception("Trying to dispose Motorboat with ShouldPool set to false!");
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

	public void CopyTo(Motorboat instance)
	{
		instance.storageid = storageid;
		instance.fuelStorageID = fuelStorageID;
	}

	public Motorboat Copy()
	{
		Motorboat motorboat = Pool.Get<Motorboat>();
		CopyTo(motorboat);
		return motorboat;
	}

	public static Motorboat Deserialize(BufferStream stream)
	{
		Motorboat motorboat = Pool.Get<Motorboat>();
		Deserialize(stream, motorboat, isDelta: false);
		return motorboat;
	}

	public static Motorboat DeserializeLengthDelimited(BufferStream stream)
	{
		Motorboat motorboat = Pool.Get<Motorboat>();
		DeserializeLengthDelimited(stream, motorboat, isDelta: false);
		return motorboat;
	}

	public static Motorboat DeserializeLength(BufferStream stream, int length)
	{
		Motorboat motorboat = Pool.Get<Motorboat>();
		DeserializeLength(stream, length, motorboat, isDelta: false);
		return motorboat;
	}

	public static Motorboat Deserialize(byte[] buffer)
	{
		Motorboat motorboat = Pool.Get<Motorboat>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, motorboat, isDelta: false);
		return motorboat;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, Motorboat previous)
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

	public static Motorboat Deserialize(BufferStream stream, Motorboat instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.storageid = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 16:
				instance.fuelStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static Motorboat DeserializeLengthDelimited(BufferStream stream, Motorboat instance, bool isDelta)
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
				instance.storageid = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 16:
				instance.fuelStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static Motorboat DeserializeLength(BufferStream stream, int length, Motorboat instance, bool isDelta)
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
				instance.storageid = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 16:
				instance.fuelStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static void SerializeDelta(BufferStream stream, Motorboat instance, Motorboat previous)
	{
		stream.WriteByte(8);
		ProtocolParser.WriteUInt64(stream, instance.storageid.Value);
		stream.WriteByte(16);
		ProtocolParser.WriteUInt64(stream, instance.fuelStorageID.Value);
	}

	public static void Serialize(BufferStream stream, Motorboat instance)
	{
		if (instance.storageid != default(NetworkableId))
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.storageid.Value);
		}
		if (instance.fuelStorageID != default(NetworkableId))
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.fuelStorageID.Value);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref storageid.Value);
		action(UidType.NetworkableId, ref fuelStorageID.Value);
	}
}
