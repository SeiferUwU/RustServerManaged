using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class Drone : IDisposable, Pool.IPooled, IProto<Drone>, IProto
{
	[NonSerialized]
	public float pitch;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(Drone instance)
	{
		if (instance.ShouldPool)
		{
			instance.pitch = 0f;
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
			throw new Exception("Trying to dispose Drone with ShouldPool set to false!");
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

	public void CopyTo(Drone instance)
	{
		instance.pitch = pitch;
	}

	public Drone Copy()
	{
		Drone drone = Pool.Get<Drone>();
		CopyTo(drone);
		return drone;
	}

	public static Drone Deserialize(BufferStream stream)
	{
		Drone drone = Pool.Get<Drone>();
		Deserialize(stream, drone, isDelta: false);
		return drone;
	}

	public static Drone DeserializeLengthDelimited(BufferStream stream)
	{
		Drone drone = Pool.Get<Drone>();
		DeserializeLengthDelimited(stream, drone, isDelta: false);
		return drone;
	}

	public static Drone DeserializeLength(BufferStream stream, int length)
	{
		Drone drone = Pool.Get<Drone>();
		DeserializeLength(stream, length, drone, isDelta: false);
		return drone;
	}

	public static Drone Deserialize(byte[] buffer)
	{
		Drone drone = Pool.Get<Drone>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, drone, isDelta: false);
		return drone;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, Drone previous)
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

	public static Drone Deserialize(BufferStream stream, Drone instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 13:
				instance.pitch = ProtocolParser.ReadSingle(stream);
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

	public static Drone DeserializeLengthDelimited(BufferStream stream, Drone instance, bool isDelta)
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
				instance.pitch = ProtocolParser.ReadSingle(stream);
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

	public static Drone DeserializeLength(BufferStream stream, int length, Drone instance, bool isDelta)
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
				instance.pitch = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, Drone instance, Drone previous)
	{
		if (instance.pitch != previous.pitch)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.pitch);
		}
	}

	public static void Serialize(BufferStream stream, Drone instance)
	{
		if (instance.pitch != 0f)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.pitch);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
	}
}
