using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class KeyLock : IDisposable, Pool.IPooled, IProto<KeyLock>, IProto
{
	[NonSerialized]
	public int code;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(KeyLock instance)
	{
		if (instance.ShouldPool)
		{
			instance.code = 0;
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
			throw new Exception("Trying to dispose KeyLock with ShouldPool set to false!");
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

	public void CopyTo(KeyLock instance)
	{
		instance.code = code;
	}

	public KeyLock Copy()
	{
		KeyLock keyLock = Pool.Get<KeyLock>();
		CopyTo(keyLock);
		return keyLock;
	}

	public static KeyLock Deserialize(BufferStream stream)
	{
		KeyLock keyLock = Pool.Get<KeyLock>();
		Deserialize(stream, keyLock, isDelta: false);
		return keyLock;
	}

	public static KeyLock DeserializeLengthDelimited(BufferStream stream)
	{
		KeyLock keyLock = Pool.Get<KeyLock>();
		DeserializeLengthDelimited(stream, keyLock, isDelta: false);
		return keyLock;
	}

	public static KeyLock DeserializeLength(BufferStream stream, int length)
	{
		KeyLock keyLock = Pool.Get<KeyLock>();
		DeserializeLength(stream, length, keyLock, isDelta: false);
		return keyLock;
	}

	public static KeyLock Deserialize(byte[] buffer)
	{
		KeyLock keyLock = Pool.Get<KeyLock>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, keyLock, isDelta: false);
		return keyLock;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, KeyLock previous)
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

	public static KeyLock Deserialize(BufferStream stream, KeyLock instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.code = (int)ProtocolParser.ReadUInt64(stream);
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

	public static KeyLock DeserializeLengthDelimited(BufferStream stream, KeyLock instance, bool isDelta)
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
				instance.code = (int)ProtocolParser.ReadUInt64(stream);
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

	public static KeyLock DeserializeLength(BufferStream stream, int length, KeyLock instance, bool isDelta)
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
				instance.code = (int)ProtocolParser.ReadUInt64(stream);
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

	public static void SerializeDelta(BufferStream stream, KeyLock instance, KeyLock previous)
	{
		if (instance.code != previous.code)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.code);
		}
	}

	public static void Serialize(BufferStream stream, KeyLock instance)
	{
		if (instance.code != 0)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.code);
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
