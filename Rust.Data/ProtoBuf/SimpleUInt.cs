using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class SimpleUInt : IDisposable, Pool.IPooled, IProto<SimpleUInt>, IProto
{
	[NonSerialized]
	public uint value;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(SimpleUInt instance)
	{
		if (instance.ShouldPool)
		{
			instance.value = 0u;
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
			throw new Exception("Trying to dispose SimpleUInt with ShouldPool set to false!");
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

	public void CopyTo(SimpleUInt instance)
	{
		instance.value = value;
	}

	public SimpleUInt Copy()
	{
		SimpleUInt simpleUInt = Pool.Get<SimpleUInt>();
		CopyTo(simpleUInt);
		return simpleUInt;
	}

	public static SimpleUInt Deserialize(BufferStream stream)
	{
		SimpleUInt simpleUInt = Pool.Get<SimpleUInt>();
		Deserialize(stream, simpleUInt, isDelta: false);
		return simpleUInt;
	}

	public static SimpleUInt DeserializeLengthDelimited(BufferStream stream)
	{
		SimpleUInt simpleUInt = Pool.Get<SimpleUInt>();
		DeserializeLengthDelimited(stream, simpleUInt, isDelta: false);
		return simpleUInt;
	}

	public static SimpleUInt DeserializeLength(BufferStream stream, int length)
	{
		SimpleUInt simpleUInt = Pool.Get<SimpleUInt>();
		DeserializeLength(stream, length, simpleUInt, isDelta: false);
		return simpleUInt;
	}

	public static SimpleUInt Deserialize(byte[] buffer)
	{
		SimpleUInt simpleUInt = Pool.Get<SimpleUInt>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, simpleUInt, isDelta: false);
		return simpleUInt;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, SimpleUInt previous)
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

	public static SimpleUInt Deserialize(BufferStream stream, SimpleUInt instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.value = ProtocolParser.ReadUInt32(stream);
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

	public static SimpleUInt DeserializeLengthDelimited(BufferStream stream, SimpleUInt instance, bool isDelta)
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
				instance.value = ProtocolParser.ReadUInt32(stream);
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

	public static SimpleUInt DeserializeLength(BufferStream stream, int length, SimpleUInt instance, bool isDelta)
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
				instance.value = ProtocolParser.ReadUInt32(stream);
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

	public static void SerializeDelta(BufferStream stream, SimpleUInt instance, SimpleUInt previous)
	{
		if (instance.value != previous.value)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.value);
		}
	}

	public static void Serialize(BufferStream stream, SimpleUInt instance)
	{
		if (instance.value != 0)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.value);
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
