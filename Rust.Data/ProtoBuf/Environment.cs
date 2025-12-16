using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class Environment : IDisposable, Pool.IPooled, IProto<Environment>, IProto
{
	[NonSerialized]
	public long dateTime;

	[NonSerialized]
	public float clouds;

	[NonSerialized]
	public float fog;

	[NonSerialized]
	public float wind;

	[NonSerialized]
	public float rain;

	[NonSerialized]
	public float engineTime;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(Environment instance)
	{
		if (instance.ShouldPool)
		{
			instance.dateTime = 0L;
			instance.clouds = 0f;
			instance.fog = 0f;
			instance.wind = 0f;
			instance.rain = 0f;
			instance.engineTime = 0f;
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
			throw new Exception("Trying to dispose Environment with ShouldPool set to false!");
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

	public void CopyTo(Environment instance)
	{
		instance.dateTime = dateTime;
		instance.clouds = clouds;
		instance.fog = fog;
		instance.wind = wind;
		instance.rain = rain;
		instance.engineTime = engineTime;
	}

	public Environment Copy()
	{
		Environment environment = Pool.Get<Environment>();
		CopyTo(environment);
		return environment;
	}

	public static Environment Deserialize(BufferStream stream)
	{
		Environment environment = Pool.Get<Environment>();
		Deserialize(stream, environment, isDelta: false);
		return environment;
	}

	public static Environment DeserializeLengthDelimited(BufferStream stream)
	{
		Environment environment = Pool.Get<Environment>();
		DeserializeLengthDelimited(stream, environment, isDelta: false);
		return environment;
	}

	public static Environment DeserializeLength(BufferStream stream, int length)
	{
		Environment environment = Pool.Get<Environment>();
		DeserializeLength(stream, length, environment, isDelta: false);
		return environment;
	}

	public static Environment Deserialize(byte[] buffer)
	{
		Environment environment = Pool.Get<Environment>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, environment, isDelta: false);
		return environment;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, Environment previous)
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

	public static Environment Deserialize(BufferStream stream, Environment instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.dateTime = (long)ProtocolParser.ReadUInt64(stream);
				continue;
			case 21:
				instance.clouds = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.fog = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.wind = ProtocolParser.ReadSingle(stream);
				continue;
			case 45:
				instance.rain = ProtocolParser.ReadSingle(stream);
				continue;
			case 53:
				instance.engineTime = ProtocolParser.ReadSingle(stream);
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

	public static Environment DeserializeLengthDelimited(BufferStream stream, Environment instance, bool isDelta)
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
				instance.dateTime = (long)ProtocolParser.ReadUInt64(stream);
				continue;
			case 21:
				instance.clouds = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.fog = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.wind = ProtocolParser.ReadSingle(stream);
				continue;
			case 45:
				instance.rain = ProtocolParser.ReadSingle(stream);
				continue;
			case 53:
				instance.engineTime = ProtocolParser.ReadSingle(stream);
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

	public static Environment DeserializeLength(BufferStream stream, int length, Environment instance, bool isDelta)
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
				instance.dateTime = (long)ProtocolParser.ReadUInt64(stream);
				continue;
			case 21:
				instance.clouds = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.fog = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.wind = ProtocolParser.ReadSingle(stream);
				continue;
			case 45:
				instance.rain = ProtocolParser.ReadSingle(stream);
				continue;
			case 53:
				instance.engineTime = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, Environment instance, Environment previous)
	{
		stream.WriteByte(8);
		ProtocolParser.WriteUInt64(stream, (ulong)instance.dateTime);
		if (instance.clouds != previous.clouds)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.clouds);
		}
		if (instance.fog != previous.fog)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.fog);
		}
		if (instance.wind != previous.wind)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.wind);
		}
		if (instance.rain != previous.rain)
		{
			stream.WriteByte(45);
			ProtocolParser.WriteSingle(stream, instance.rain);
		}
		if (instance.engineTime != previous.engineTime)
		{
			stream.WriteByte(53);
			ProtocolParser.WriteSingle(stream, instance.engineTime);
		}
	}

	public static void Serialize(BufferStream stream, Environment instance)
	{
		if (instance.dateTime != 0L)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.dateTime);
		}
		if (instance.clouds != 0f)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.clouds);
		}
		if (instance.fog != 0f)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.fog);
		}
		if (instance.wind != 0f)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.wind);
		}
		if (instance.rain != 0f)
		{
			stream.WriteByte(45);
			ProtocolParser.WriteSingle(stream, instance.rain);
		}
		if (instance.engineTime != 0f)
		{
			stream.WriteByte(53);
			ProtocolParser.WriteSingle(stream, instance.engineTime);
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
