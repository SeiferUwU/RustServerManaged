using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class DudExplosive : IDisposable, Pool.IPooled, IProto<DudExplosive>, IProto
{
	[NonSerialized]
	public float fuseTimeLeft;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(DudExplosive instance)
	{
		if (instance.ShouldPool)
		{
			instance.fuseTimeLeft = 0f;
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
			throw new Exception("Trying to dispose DudExplosive with ShouldPool set to false!");
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

	public void CopyTo(DudExplosive instance)
	{
		instance.fuseTimeLeft = fuseTimeLeft;
	}

	public DudExplosive Copy()
	{
		DudExplosive dudExplosive = Pool.Get<DudExplosive>();
		CopyTo(dudExplosive);
		return dudExplosive;
	}

	public static DudExplosive Deserialize(BufferStream stream)
	{
		DudExplosive dudExplosive = Pool.Get<DudExplosive>();
		Deserialize(stream, dudExplosive, isDelta: false);
		return dudExplosive;
	}

	public static DudExplosive DeserializeLengthDelimited(BufferStream stream)
	{
		DudExplosive dudExplosive = Pool.Get<DudExplosive>();
		DeserializeLengthDelimited(stream, dudExplosive, isDelta: false);
		return dudExplosive;
	}

	public static DudExplosive DeserializeLength(BufferStream stream, int length)
	{
		DudExplosive dudExplosive = Pool.Get<DudExplosive>();
		DeserializeLength(stream, length, dudExplosive, isDelta: false);
		return dudExplosive;
	}

	public static DudExplosive Deserialize(byte[] buffer)
	{
		DudExplosive dudExplosive = Pool.Get<DudExplosive>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, dudExplosive, isDelta: false);
		return dudExplosive;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, DudExplosive previous)
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

	public static DudExplosive Deserialize(BufferStream stream, DudExplosive instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 13:
				instance.fuseTimeLeft = ProtocolParser.ReadSingle(stream);
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

	public static DudExplosive DeserializeLengthDelimited(BufferStream stream, DudExplosive instance, bool isDelta)
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
				instance.fuseTimeLeft = ProtocolParser.ReadSingle(stream);
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

	public static DudExplosive DeserializeLength(BufferStream stream, int length, DudExplosive instance, bool isDelta)
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
				instance.fuseTimeLeft = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, DudExplosive instance, DudExplosive previous)
	{
		if (instance.fuseTimeLeft != previous.fuseTimeLeft)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.fuseTimeLeft);
		}
	}

	public static void Serialize(BufferStream stream, DudExplosive instance)
	{
		if (instance.fuseTimeLeft != 0f)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.fuseTimeLeft);
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
