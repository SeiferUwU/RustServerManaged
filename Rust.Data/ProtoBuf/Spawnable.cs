using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class Spawnable : IDisposable, Pool.IPooled, IProto<Spawnable>, IProto
{
	[NonSerialized]
	public uint population;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(Spawnable instance)
	{
		if (instance.ShouldPool)
		{
			instance.population = 0u;
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
			throw new Exception("Trying to dispose Spawnable with ShouldPool set to false!");
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

	public void CopyTo(Spawnable instance)
	{
		instance.population = population;
	}

	public Spawnable Copy()
	{
		Spawnable spawnable = Pool.Get<Spawnable>();
		CopyTo(spawnable);
		return spawnable;
	}

	public static Spawnable Deserialize(BufferStream stream)
	{
		Spawnable spawnable = Pool.Get<Spawnable>();
		Deserialize(stream, spawnable, isDelta: false);
		return spawnable;
	}

	public static Spawnable DeserializeLengthDelimited(BufferStream stream)
	{
		Spawnable spawnable = Pool.Get<Spawnable>();
		DeserializeLengthDelimited(stream, spawnable, isDelta: false);
		return spawnable;
	}

	public static Spawnable DeserializeLength(BufferStream stream, int length)
	{
		Spawnable spawnable = Pool.Get<Spawnable>();
		DeserializeLength(stream, length, spawnable, isDelta: false);
		return spawnable;
	}

	public static Spawnable Deserialize(byte[] buffer)
	{
		Spawnable spawnable = Pool.Get<Spawnable>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, spawnable, isDelta: false);
		return spawnable;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, Spawnable previous)
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

	public static Spawnable Deserialize(BufferStream stream, Spawnable instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.population = ProtocolParser.ReadUInt32(stream);
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

	public static Spawnable DeserializeLengthDelimited(BufferStream stream, Spawnable instance, bool isDelta)
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
				instance.population = ProtocolParser.ReadUInt32(stream);
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

	public static Spawnable DeserializeLength(BufferStream stream, int length, Spawnable instance, bool isDelta)
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
				instance.population = ProtocolParser.ReadUInt32(stream);
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

	public static void SerializeDelta(BufferStream stream, Spawnable instance, Spawnable previous)
	{
		if (instance.population != previous.population)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.population);
		}
	}

	public static void Serialize(BufferStream stream, Spawnable instance)
	{
		if (instance.population != 0)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.population);
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
