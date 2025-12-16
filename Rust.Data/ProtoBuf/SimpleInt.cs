using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class SimpleInt : IDisposable, Pool.IPooled, IProto<SimpleInt>, IProto
{
	[NonSerialized]
	public int value;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(SimpleInt instance)
	{
		if (instance.ShouldPool)
		{
			instance.value = 0;
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
			throw new Exception("Trying to dispose SimpleInt with ShouldPool set to false!");
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

	public void CopyTo(SimpleInt instance)
	{
		instance.value = value;
	}

	public SimpleInt Copy()
	{
		SimpleInt simpleInt = Pool.Get<SimpleInt>();
		CopyTo(simpleInt);
		return simpleInt;
	}

	public static SimpleInt Deserialize(BufferStream stream)
	{
		SimpleInt simpleInt = Pool.Get<SimpleInt>();
		Deserialize(stream, simpleInt, isDelta: false);
		return simpleInt;
	}

	public static SimpleInt DeserializeLengthDelimited(BufferStream stream)
	{
		SimpleInt simpleInt = Pool.Get<SimpleInt>();
		DeserializeLengthDelimited(stream, simpleInt, isDelta: false);
		return simpleInt;
	}

	public static SimpleInt DeserializeLength(BufferStream stream, int length)
	{
		SimpleInt simpleInt = Pool.Get<SimpleInt>();
		DeserializeLength(stream, length, simpleInt, isDelta: false);
		return simpleInt;
	}

	public static SimpleInt Deserialize(byte[] buffer)
	{
		SimpleInt simpleInt = Pool.Get<SimpleInt>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, simpleInt, isDelta: false);
		return simpleInt;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, SimpleInt previous)
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

	public static SimpleInt Deserialize(BufferStream stream, SimpleInt instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.value = (int)ProtocolParser.ReadUInt64(stream);
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

	public static SimpleInt DeserializeLengthDelimited(BufferStream stream, SimpleInt instance, bool isDelta)
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
				instance.value = (int)ProtocolParser.ReadUInt64(stream);
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

	public static SimpleInt DeserializeLength(BufferStream stream, int length, SimpleInt instance, bool isDelta)
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
				instance.value = (int)ProtocolParser.ReadUInt64(stream);
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

	public static void SerializeDelta(BufferStream stream, SimpleInt instance, SimpleInt previous)
	{
		if (instance.value != previous.value)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.value);
		}
	}

	public static void Serialize(BufferStream stream, SimpleInt instance)
	{
		if (instance.value != 0)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.value);
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
