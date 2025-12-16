using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class BaseNPC : IDisposable, Pool.IPooled, IProto<BaseNPC>, IProto
{
	[NonSerialized]
	public int flags;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(BaseNPC instance)
	{
		if (instance.ShouldPool)
		{
			instance.flags = 0;
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
			throw new Exception("Trying to dispose BaseNPC with ShouldPool set to false!");
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

	public void CopyTo(BaseNPC instance)
	{
		instance.flags = flags;
	}

	public BaseNPC Copy()
	{
		BaseNPC baseNPC = Pool.Get<BaseNPC>();
		CopyTo(baseNPC);
		return baseNPC;
	}

	public static BaseNPC Deserialize(BufferStream stream)
	{
		BaseNPC baseNPC = Pool.Get<BaseNPC>();
		Deserialize(stream, baseNPC, isDelta: false);
		return baseNPC;
	}

	public static BaseNPC DeserializeLengthDelimited(BufferStream stream)
	{
		BaseNPC baseNPC = Pool.Get<BaseNPC>();
		DeserializeLengthDelimited(stream, baseNPC, isDelta: false);
		return baseNPC;
	}

	public static BaseNPC DeserializeLength(BufferStream stream, int length)
	{
		BaseNPC baseNPC = Pool.Get<BaseNPC>();
		DeserializeLength(stream, length, baseNPC, isDelta: false);
		return baseNPC;
	}

	public static BaseNPC Deserialize(byte[] buffer)
	{
		BaseNPC baseNPC = Pool.Get<BaseNPC>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, baseNPC, isDelta: false);
		return baseNPC;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, BaseNPC previous)
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

	public static BaseNPC Deserialize(BufferStream stream, BaseNPC instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 16:
				instance.flags = (int)ProtocolParser.ReadUInt64(stream);
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

	public static BaseNPC DeserializeLengthDelimited(BufferStream stream, BaseNPC instance, bool isDelta)
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
			case 16:
				instance.flags = (int)ProtocolParser.ReadUInt64(stream);
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

	public static BaseNPC DeserializeLength(BufferStream stream, int length, BaseNPC instance, bool isDelta)
	{
		long num = stream.Position + length;
		while (stream.Position < num)
		{
			int num2 = stream.ReadByte();
			switch (num2)
			{
			case -1:
				throw new EndOfStreamException();
			case 16:
				instance.flags = (int)ProtocolParser.ReadUInt64(stream);
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

	public static void SerializeDelta(BufferStream stream, BaseNPC instance, BaseNPC previous)
	{
		if (instance.flags != previous.flags)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.flags);
		}
	}

	public static void Serialize(BufferStream stream, BaseNPC instance)
	{
		if (instance.flags != 0)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.flags);
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
