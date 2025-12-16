using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class Magazine : IDisposable, Pool.IPooled, IProto<Magazine>, IProto
{
	[NonSerialized]
	public int capacity;

	[NonSerialized]
	public int contents;

	[NonSerialized]
	public int ammoType;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(Magazine instance)
	{
		if (instance.ShouldPool)
		{
			instance.capacity = 0;
			instance.contents = 0;
			instance.ammoType = 0;
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
			throw new Exception("Trying to dispose Magazine with ShouldPool set to false!");
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

	public void CopyTo(Magazine instance)
	{
		instance.capacity = capacity;
		instance.contents = contents;
		instance.ammoType = ammoType;
	}

	public Magazine Copy()
	{
		Magazine magazine = Pool.Get<Magazine>();
		CopyTo(magazine);
		return magazine;
	}

	public static Magazine Deserialize(BufferStream stream)
	{
		Magazine magazine = Pool.Get<Magazine>();
		Deserialize(stream, magazine, isDelta: false);
		return magazine;
	}

	public static Magazine DeserializeLengthDelimited(BufferStream stream)
	{
		Magazine magazine = Pool.Get<Magazine>();
		DeserializeLengthDelimited(stream, magazine, isDelta: false);
		return magazine;
	}

	public static Magazine DeserializeLength(BufferStream stream, int length)
	{
		Magazine magazine = Pool.Get<Magazine>();
		DeserializeLength(stream, length, magazine, isDelta: false);
		return magazine;
	}

	public static Magazine Deserialize(byte[] buffer)
	{
		Magazine magazine = Pool.Get<Magazine>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, magazine, isDelta: false);
		return magazine;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, Magazine previous)
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

	public static Magazine Deserialize(BufferStream stream, Magazine instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.capacity = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.contents = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 24:
				instance.ammoType = (int)ProtocolParser.ReadUInt64(stream);
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

	public static Magazine DeserializeLengthDelimited(BufferStream stream, Magazine instance, bool isDelta)
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
				instance.capacity = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.contents = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 24:
				instance.ammoType = (int)ProtocolParser.ReadUInt64(stream);
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

	public static Magazine DeserializeLength(BufferStream stream, int length, Magazine instance, bool isDelta)
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
				instance.capacity = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.contents = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 24:
				instance.ammoType = (int)ProtocolParser.ReadUInt64(stream);
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

	public static void SerializeDelta(BufferStream stream, Magazine instance, Magazine previous)
	{
		if (instance.capacity != previous.capacity)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.capacity);
		}
		if (instance.contents != previous.contents)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.contents);
		}
		if (instance.ammoType != previous.ammoType)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ammoType);
		}
	}

	public static void Serialize(BufferStream stream, Magazine instance)
	{
		if (instance.capacity != 0)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.capacity);
		}
		if (instance.contents != 0)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.contents);
		}
		if (instance.ammoType != 0)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ammoType);
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
