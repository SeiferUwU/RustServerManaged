using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class Submarine : IDisposable, Pool.IPooled, IProto<Submarine>, IProto
{
	[NonSerialized]
	public float throttle;

	[NonSerialized]
	public float upDown;

	[NonSerialized]
	public float rudder;

	[NonSerialized]
	public NetworkableId fuelStorageID;

	[NonSerialized]
	public float fuelAmount;

	[NonSerialized]
	public NetworkableId torpedoStorageID;

	[NonSerialized]
	public float oxygen;

	[NonSerialized]
	public NetworkableId itemStorageID;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(Submarine instance)
	{
		if (instance.ShouldPool)
		{
			instance.throttle = 0f;
			instance.upDown = 0f;
			instance.rudder = 0f;
			instance.fuelStorageID = default(NetworkableId);
			instance.fuelAmount = 0f;
			instance.torpedoStorageID = default(NetworkableId);
			instance.oxygen = 0f;
			instance.itemStorageID = default(NetworkableId);
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
			throw new Exception("Trying to dispose Submarine with ShouldPool set to false!");
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

	public void CopyTo(Submarine instance)
	{
		instance.throttle = throttle;
		instance.upDown = upDown;
		instance.rudder = rudder;
		instance.fuelStorageID = fuelStorageID;
		instance.fuelAmount = fuelAmount;
		instance.torpedoStorageID = torpedoStorageID;
		instance.oxygen = oxygen;
		instance.itemStorageID = itemStorageID;
	}

	public Submarine Copy()
	{
		Submarine submarine = Pool.Get<Submarine>();
		CopyTo(submarine);
		return submarine;
	}

	public static Submarine Deserialize(BufferStream stream)
	{
		Submarine submarine = Pool.Get<Submarine>();
		Deserialize(stream, submarine, isDelta: false);
		return submarine;
	}

	public static Submarine DeserializeLengthDelimited(BufferStream stream)
	{
		Submarine submarine = Pool.Get<Submarine>();
		DeserializeLengthDelimited(stream, submarine, isDelta: false);
		return submarine;
	}

	public static Submarine DeserializeLength(BufferStream stream, int length)
	{
		Submarine submarine = Pool.Get<Submarine>();
		DeserializeLength(stream, length, submarine, isDelta: false);
		return submarine;
	}

	public static Submarine Deserialize(byte[] buffer)
	{
		Submarine submarine = Pool.Get<Submarine>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, submarine, isDelta: false);
		return submarine;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, Submarine previous)
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

	public static Submarine Deserialize(BufferStream stream, Submarine instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 13:
				instance.throttle = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.upDown = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.rudder = ProtocolParser.ReadSingle(stream);
				continue;
			case 32:
				instance.fuelStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 45:
				instance.fuelAmount = ProtocolParser.ReadSingle(stream);
				continue;
			case 48:
				instance.torpedoStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 61:
				instance.oxygen = ProtocolParser.ReadSingle(stream);
				continue;
			case 64:
				instance.itemStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static Submarine DeserializeLengthDelimited(BufferStream stream, Submarine instance, bool isDelta)
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
			case 13:
				instance.throttle = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.upDown = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.rudder = ProtocolParser.ReadSingle(stream);
				continue;
			case 32:
				instance.fuelStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 45:
				instance.fuelAmount = ProtocolParser.ReadSingle(stream);
				continue;
			case 48:
				instance.torpedoStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 61:
				instance.oxygen = ProtocolParser.ReadSingle(stream);
				continue;
			case 64:
				instance.itemStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static Submarine DeserializeLength(BufferStream stream, int length, Submarine instance, bool isDelta)
	{
		long num = stream.Position + length;
		while (stream.Position < num)
		{
			int num2 = stream.ReadByte();
			switch (num2)
			{
			case -1:
				throw new EndOfStreamException();
			case 13:
				instance.throttle = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.upDown = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.rudder = ProtocolParser.ReadSingle(stream);
				continue;
			case 32:
				instance.fuelStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 45:
				instance.fuelAmount = ProtocolParser.ReadSingle(stream);
				continue;
			case 48:
				instance.torpedoStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 61:
				instance.oxygen = ProtocolParser.ReadSingle(stream);
				continue;
			case 64:
				instance.itemStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static void SerializeDelta(BufferStream stream, Submarine instance, Submarine previous)
	{
		if (instance.throttle != previous.throttle)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.throttle);
		}
		if (instance.upDown != previous.upDown)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.upDown);
		}
		if (instance.rudder != previous.rudder)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.rudder);
		}
		stream.WriteByte(32);
		ProtocolParser.WriteUInt64(stream, instance.fuelStorageID.Value);
		if (instance.fuelAmount != previous.fuelAmount)
		{
			stream.WriteByte(45);
			ProtocolParser.WriteSingle(stream, instance.fuelAmount);
		}
		stream.WriteByte(48);
		ProtocolParser.WriteUInt64(stream, instance.torpedoStorageID.Value);
		if (instance.oxygen != previous.oxygen)
		{
			stream.WriteByte(61);
			ProtocolParser.WriteSingle(stream, instance.oxygen);
		}
		stream.WriteByte(64);
		ProtocolParser.WriteUInt64(stream, instance.itemStorageID.Value);
	}

	public static void Serialize(BufferStream stream, Submarine instance)
	{
		if (instance.throttle != 0f)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.throttle);
		}
		if (instance.upDown != 0f)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.upDown);
		}
		if (instance.rudder != 0f)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.rudder);
		}
		if (instance.fuelStorageID != default(NetworkableId))
		{
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, instance.fuelStorageID.Value);
		}
		if (instance.fuelAmount != 0f)
		{
			stream.WriteByte(45);
			ProtocolParser.WriteSingle(stream, instance.fuelAmount);
		}
		if (instance.torpedoStorageID != default(NetworkableId))
		{
			stream.WriteByte(48);
			ProtocolParser.WriteUInt64(stream, instance.torpedoStorageID.Value);
		}
		if (instance.oxygen != 0f)
		{
			stream.WriteByte(61);
			ProtocolParser.WriteSingle(stream, instance.oxygen);
		}
		if (instance.itemStorageID != default(NetworkableId))
		{
			stream.WriteByte(64);
			ProtocolParser.WriteUInt64(stream, instance.itemStorageID.Value);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref fuelStorageID.Value);
		action(UidType.NetworkableId, ref torpedoStorageID.Value);
		action(UidType.NetworkableId, ref itemStorageID.Value);
	}
}
