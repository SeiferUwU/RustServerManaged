using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class ComputerStation : IDisposable, Pool.IPooled, IProto<ComputerStation>, IProto
{
	[NonSerialized]
	public string bookmarks;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(ComputerStation instance)
	{
		if (instance.ShouldPool)
		{
			instance.bookmarks = string.Empty;
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
			throw new Exception("Trying to dispose ComputerStation with ShouldPool set to false!");
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

	public void CopyTo(ComputerStation instance)
	{
		instance.bookmarks = bookmarks;
	}

	public ComputerStation Copy()
	{
		ComputerStation computerStation = Pool.Get<ComputerStation>();
		CopyTo(computerStation);
		return computerStation;
	}

	public static ComputerStation Deserialize(BufferStream stream)
	{
		ComputerStation computerStation = Pool.Get<ComputerStation>();
		Deserialize(stream, computerStation, isDelta: false);
		return computerStation;
	}

	public static ComputerStation DeserializeLengthDelimited(BufferStream stream)
	{
		ComputerStation computerStation = Pool.Get<ComputerStation>();
		DeserializeLengthDelimited(stream, computerStation, isDelta: false);
		return computerStation;
	}

	public static ComputerStation DeserializeLength(BufferStream stream, int length)
	{
		ComputerStation computerStation = Pool.Get<ComputerStation>();
		DeserializeLength(stream, length, computerStation, isDelta: false);
		return computerStation;
	}

	public static ComputerStation Deserialize(byte[] buffer)
	{
		ComputerStation computerStation = Pool.Get<ComputerStation>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, computerStation, isDelta: false);
		return computerStation;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, ComputerStation previous)
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

	public static ComputerStation Deserialize(BufferStream stream, ComputerStation instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 10:
				instance.bookmarks = ProtocolParser.ReadString(stream);
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

	public static ComputerStation DeserializeLengthDelimited(BufferStream stream, ComputerStation instance, bool isDelta)
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
			case 10:
				instance.bookmarks = ProtocolParser.ReadString(stream);
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

	public static ComputerStation DeserializeLength(BufferStream stream, int length, ComputerStation instance, bool isDelta)
	{
		long num = stream.Position + length;
		while (stream.Position < num)
		{
			int num2 = stream.ReadByte();
			switch (num2)
			{
			case -1:
				throw new EndOfStreamException();
			case 10:
				instance.bookmarks = ProtocolParser.ReadString(stream);
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

	public static void SerializeDelta(BufferStream stream, ComputerStation instance, ComputerStation previous)
	{
		if (instance.bookmarks != null && instance.bookmarks != previous.bookmarks)
		{
			stream.WriteByte(10);
			ProtocolParser.WriteString(stream, instance.bookmarks);
		}
	}

	public static void Serialize(BufferStream stream, ComputerStation instance)
	{
		if (instance.bookmarks != null)
		{
			stream.WriteByte(10);
			ProtocolParser.WriteString(stream, instance.bookmarks);
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
