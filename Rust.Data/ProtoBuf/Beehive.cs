using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class Beehive : IDisposable, Pool.IPooled, IProto<Beehive>, IProto
{
	[NonSerialized]
	public float currentProgress;

	[NonSerialized]
	public float temperature;

	[NonSerialized]
	public bool inside;

	[NonSerialized]
	public float humidity;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(Beehive instance)
	{
		if (instance.ShouldPool)
		{
			instance.currentProgress = 0f;
			instance.temperature = 0f;
			instance.inside = false;
			instance.humidity = 0f;
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
			throw new Exception("Trying to dispose Beehive with ShouldPool set to false!");
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

	public void CopyTo(Beehive instance)
	{
		instance.currentProgress = currentProgress;
		instance.temperature = temperature;
		instance.inside = inside;
		instance.humidity = humidity;
	}

	public Beehive Copy()
	{
		Beehive beehive = Pool.Get<Beehive>();
		CopyTo(beehive);
		return beehive;
	}

	public static Beehive Deserialize(BufferStream stream)
	{
		Beehive beehive = Pool.Get<Beehive>();
		Deserialize(stream, beehive, isDelta: false);
		return beehive;
	}

	public static Beehive DeserializeLengthDelimited(BufferStream stream)
	{
		Beehive beehive = Pool.Get<Beehive>();
		DeserializeLengthDelimited(stream, beehive, isDelta: false);
		return beehive;
	}

	public static Beehive DeserializeLength(BufferStream stream, int length)
	{
		Beehive beehive = Pool.Get<Beehive>();
		DeserializeLength(stream, length, beehive, isDelta: false);
		return beehive;
	}

	public static Beehive Deserialize(byte[] buffer)
	{
		Beehive beehive = Pool.Get<Beehive>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, beehive, isDelta: false);
		return beehive;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, Beehive previous)
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

	public static Beehive Deserialize(BufferStream stream, Beehive instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 13:
				instance.currentProgress = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.temperature = ProtocolParser.ReadSingle(stream);
				continue;
			case 24:
				instance.inside = ProtocolParser.ReadBool(stream);
				continue;
			case 37:
				instance.humidity = ProtocolParser.ReadSingle(stream);
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

	public static Beehive DeserializeLengthDelimited(BufferStream stream, Beehive instance, bool isDelta)
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
				instance.currentProgress = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.temperature = ProtocolParser.ReadSingle(stream);
				continue;
			case 24:
				instance.inside = ProtocolParser.ReadBool(stream);
				continue;
			case 37:
				instance.humidity = ProtocolParser.ReadSingle(stream);
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

	public static Beehive DeserializeLength(BufferStream stream, int length, Beehive instance, bool isDelta)
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
				instance.currentProgress = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.temperature = ProtocolParser.ReadSingle(stream);
				continue;
			case 24:
				instance.inside = ProtocolParser.ReadBool(stream);
				continue;
			case 37:
				instance.humidity = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, Beehive instance, Beehive previous)
	{
		if (instance.currentProgress != previous.currentProgress)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.currentProgress);
		}
		if (instance.temperature != previous.temperature)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.temperature);
		}
		stream.WriteByte(24);
		ProtocolParser.WriteBool(stream, instance.inside);
		if (instance.humidity != previous.humidity)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.humidity);
		}
	}

	public static void Serialize(BufferStream stream, Beehive instance)
	{
		if (instance.currentProgress != 0f)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.currentProgress);
		}
		if (instance.temperature != 0f)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.temperature);
		}
		if (instance.inside)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteBool(stream, instance.inside);
		}
		if (instance.humidity != 0f)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.humidity);
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
