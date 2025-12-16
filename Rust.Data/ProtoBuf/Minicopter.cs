using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class Minicopter : IDisposable, Pool.IPooled, IProto<Minicopter>, IProto
{
	[NonSerialized]
	public NetworkableId fuelStorageID;

	[NonSerialized]
	public float fuelFraction;

	[NonSerialized]
	public float pitch;

	[NonSerialized]
	public float roll;

	[NonSerialized]
	public float yaw;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(Minicopter instance)
	{
		if (instance.ShouldPool)
		{
			instance.fuelStorageID = default(NetworkableId);
			instance.fuelFraction = 0f;
			instance.pitch = 0f;
			instance.roll = 0f;
			instance.yaw = 0f;
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
			throw new Exception("Trying to dispose Minicopter with ShouldPool set to false!");
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

	public void CopyTo(Minicopter instance)
	{
		instance.fuelStorageID = fuelStorageID;
		instance.fuelFraction = fuelFraction;
		instance.pitch = pitch;
		instance.roll = roll;
		instance.yaw = yaw;
	}

	public Minicopter Copy()
	{
		Minicopter minicopter = Pool.Get<Minicopter>();
		CopyTo(minicopter);
		return minicopter;
	}

	public static Minicopter Deserialize(BufferStream stream)
	{
		Minicopter minicopter = Pool.Get<Minicopter>();
		Deserialize(stream, minicopter, isDelta: false);
		return minicopter;
	}

	public static Minicopter DeserializeLengthDelimited(BufferStream stream)
	{
		Minicopter minicopter = Pool.Get<Minicopter>();
		DeserializeLengthDelimited(stream, minicopter, isDelta: false);
		return minicopter;
	}

	public static Minicopter DeserializeLength(BufferStream stream, int length)
	{
		Minicopter minicopter = Pool.Get<Minicopter>();
		DeserializeLength(stream, length, minicopter, isDelta: false);
		return minicopter;
	}

	public static Minicopter Deserialize(byte[] buffer)
	{
		Minicopter minicopter = Pool.Get<Minicopter>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, minicopter, isDelta: false);
		return minicopter;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, Minicopter previous)
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

	public static Minicopter Deserialize(BufferStream stream, Minicopter instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.fuelStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 21:
				instance.fuelFraction = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.pitch = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.roll = ProtocolParser.ReadSingle(stream);
				continue;
			case 45:
				instance.yaw = ProtocolParser.ReadSingle(stream);
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

	public static Minicopter DeserializeLengthDelimited(BufferStream stream, Minicopter instance, bool isDelta)
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
				instance.fuelStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 21:
				instance.fuelFraction = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.pitch = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.roll = ProtocolParser.ReadSingle(stream);
				continue;
			case 45:
				instance.yaw = ProtocolParser.ReadSingle(stream);
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

	public static Minicopter DeserializeLength(BufferStream stream, int length, Minicopter instance, bool isDelta)
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
				instance.fuelStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 21:
				instance.fuelFraction = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.pitch = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.roll = ProtocolParser.ReadSingle(stream);
				continue;
			case 45:
				instance.yaw = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, Minicopter instance, Minicopter previous)
	{
		stream.WriteByte(8);
		ProtocolParser.WriteUInt64(stream, instance.fuelStorageID.Value);
		if (instance.fuelFraction != previous.fuelFraction)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.fuelFraction);
		}
		if (instance.pitch != previous.pitch)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.pitch);
		}
		if (instance.roll != previous.roll)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.roll);
		}
		if (instance.yaw != previous.yaw)
		{
			stream.WriteByte(45);
			ProtocolParser.WriteSingle(stream, instance.yaw);
		}
	}

	public static void Serialize(BufferStream stream, Minicopter instance)
	{
		if (instance.fuelStorageID != default(NetworkableId))
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.fuelStorageID.Value);
		}
		if (instance.fuelFraction != 0f)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.fuelFraction);
		}
		if (instance.pitch != 0f)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.pitch);
		}
		if (instance.roll != 0f)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.roll);
		}
		if (instance.yaw != 0f)
		{
			stream.WriteByte(45);
			ProtocolParser.WriteSingle(stream, instance.yaw);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref fuelStorageID.Value);
	}
}
