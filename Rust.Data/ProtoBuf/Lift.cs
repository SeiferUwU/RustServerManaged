using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class Lift : IDisposable, Pool.IPooled, IProto<Lift>, IProto
{
	[NonSerialized]
	public int floor;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(Lift instance)
	{
		if (instance.ShouldPool)
		{
			instance.floor = 0;
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
			throw new Exception("Trying to dispose Lift with ShouldPool set to false!");
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

	public void CopyTo(Lift instance)
	{
		instance.floor = floor;
	}

	public Lift Copy()
	{
		Lift lift = Pool.Get<Lift>();
		CopyTo(lift);
		return lift;
	}

	public static Lift Deserialize(BufferStream stream)
	{
		Lift lift = Pool.Get<Lift>();
		Deserialize(stream, lift, isDelta: false);
		return lift;
	}

	public static Lift DeserializeLengthDelimited(BufferStream stream)
	{
		Lift lift = Pool.Get<Lift>();
		DeserializeLengthDelimited(stream, lift, isDelta: false);
		return lift;
	}

	public static Lift DeserializeLength(BufferStream stream, int length)
	{
		Lift lift = Pool.Get<Lift>();
		DeserializeLength(stream, length, lift, isDelta: false);
		return lift;
	}

	public static Lift Deserialize(byte[] buffer)
	{
		Lift lift = Pool.Get<Lift>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, lift, isDelta: false);
		return lift;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, Lift previous)
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

	public static Lift Deserialize(BufferStream stream, Lift instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.floor = (int)ProtocolParser.ReadUInt64(stream);
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

	public static Lift DeserializeLengthDelimited(BufferStream stream, Lift instance, bool isDelta)
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
				instance.floor = (int)ProtocolParser.ReadUInt64(stream);
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

	public static Lift DeserializeLength(BufferStream stream, int length, Lift instance, bool isDelta)
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
				instance.floor = (int)ProtocolParser.ReadUInt64(stream);
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

	public static void SerializeDelta(BufferStream stream, Lift instance, Lift previous)
	{
		if (instance.floor != previous.floor)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.floor);
		}
	}

	public static void Serialize(BufferStream stream, Lift instance)
	{
		if (instance.floor != 0)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.floor);
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
