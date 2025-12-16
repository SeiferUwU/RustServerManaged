using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class BaseSculpture : IDisposable, Pool.IPooled, IProto<BaseSculpture>, IProto
{
	[NonSerialized]
	public uint crc;

	[NonSerialized]
	public int colourSelection;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(BaseSculpture instance)
	{
		if (instance.ShouldPool)
		{
			instance.crc = 0u;
			instance.colourSelection = 0;
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
			throw new Exception("Trying to dispose BaseSculpture with ShouldPool set to false!");
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

	public void CopyTo(BaseSculpture instance)
	{
		instance.crc = crc;
		instance.colourSelection = colourSelection;
	}

	public BaseSculpture Copy()
	{
		BaseSculpture baseSculpture = Pool.Get<BaseSculpture>();
		CopyTo(baseSculpture);
		return baseSculpture;
	}

	public static BaseSculpture Deserialize(BufferStream stream)
	{
		BaseSculpture baseSculpture = Pool.Get<BaseSculpture>();
		Deserialize(stream, baseSculpture, isDelta: false);
		return baseSculpture;
	}

	public static BaseSculpture DeserializeLengthDelimited(BufferStream stream)
	{
		BaseSculpture baseSculpture = Pool.Get<BaseSculpture>();
		DeserializeLengthDelimited(stream, baseSculpture, isDelta: false);
		return baseSculpture;
	}

	public static BaseSculpture DeserializeLength(BufferStream stream, int length)
	{
		BaseSculpture baseSculpture = Pool.Get<BaseSculpture>();
		DeserializeLength(stream, length, baseSculpture, isDelta: false);
		return baseSculpture;
	}

	public static BaseSculpture Deserialize(byte[] buffer)
	{
		BaseSculpture baseSculpture = Pool.Get<BaseSculpture>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, baseSculpture, isDelta: false);
		return baseSculpture;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, BaseSculpture previous)
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

	public static BaseSculpture Deserialize(BufferStream stream, BaseSculpture instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.crc = ProtocolParser.ReadUInt32(stream);
				continue;
			case 16:
				instance.colourSelection = (int)ProtocolParser.ReadUInt64(stream);
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

	public static BaseSculpture DeserializeLengthDelimited(BufferStream stream, BaseSculpture instance, bool isDelta)
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
				instance.crc = ProtocolParser.ReadUInt32(stream);
				continue;
			case 16:
				instance.colourSelection = (int)ProtocolParser.ReadUInt64(stream);
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

	public static BaseSculpture DeserializeLength(BufferStream stream, int length, BaseSculpture instance, bool isDelta)
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
				instance.crc = ProtocolParser.ReadUInt32(stream);
				continue;
			case 16:
				instance.colourSelection = (int)ProtocolParser.ReadUInt64(stream);
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

	public static void SerializeDelta(BufferStream stream, BaseSculpture instance, BaseSculpture previous)
	{
		if (instance.crc != previous.crc)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.crc);
		}
		if (instance.colourSelection != previous.colourSelection)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.colourSelection);
		}
	}

	public static void Serialize(BufferStream stream, BaseSculpture instance)
	{
		if (instance.crc != 0)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.crc);
		}
		if (instance.colourSelection != 0)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.colourSelection);
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
