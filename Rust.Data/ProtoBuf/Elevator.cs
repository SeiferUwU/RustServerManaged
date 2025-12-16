using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class Elevator : IDisposable, Pool.IPooled, IProto<Elevator>, IProto
{
	[NonSerialized]
	public int floor;

	[NonSerialized]
	public NetworkableId spawnedLift;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(Elevator instance)
	{
		if (instance.ShouldPool)
		{
			instance.floor = 0;
			instance.spawnedLift = default(NetworkableId);
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
			throw new Exception("Trying to dispose Elevator with ShouldPool set to false!");
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

	public void CopyTo(Elevator instance)
	{
		instance.floor = floor;
		instance.spawnedLift = spawnedLift;
	}

	public Elevator Copy()
	{
		Elevator elevator = Pool.Get<Elevator>();
		CopyTo(elevator);
		return elevator;
	}

	public static Elevator Deserialize(BufferStream stream)
	{
		Elevator elevator = Pool.Get<Elevator>();
		Deserialize(stream, elevator, isDelta: false);
		return elevator;
	}

	public static Elevator DeserializeLengthDelimited(BufferStream stream)
	{
		Elevator elevator = Pool.Get<Elevator>();
		DeserializeLengthDelimited(stream, elevator, isDelta: false);
		return elevator;
	}

	public static Elevator DeserializeLength(BufferStream stream, int length)
	{
		Elevator elevator = Pool.Get<Elevator>();
		DeserializeLength(stream, length, elevator, isDelta: false);
		return elevator;
	}

	public static Elevator Deserialize(byte[] buffer)
	{
		Elevator elevator = Pool.Get<Elevator>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, elevator, isDelta: false);
		return elevator;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, Elevator previous)
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

	public static Elevator Deserialize(BufferStream stream, Elevator instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.floor = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.spawnedLift = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static Elevator DeserializeLengthDelimited(BufferStream stream, Elevator instance, bool isDelta)
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
				instance.floor = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.spawnedLift = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static Elevator DeserializeLength(BufferStream stream, int length, Elevator instance, bool isDelta)
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
				instance.floor = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.spawnedLift = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static void SerializeDelta(BufferStream stream, Elevator instance, Elevator previous)
	{
		if (instance.floor != previous.floor)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.floor);
		}
		stream.WriteByte(16);
		ProtocolParser.WriteUInt64(stream, instance.spawnedLift.Value);
	}

	public static void Serialize(BufferStream stream, Elevator instance)
	{
		if (instance.floor != 0)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.floor);
		}
		if (instance.spawnedLift != default(NetworkableId))
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.spawnedLift.Value);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref spawnedLift.Value);
	}
}
