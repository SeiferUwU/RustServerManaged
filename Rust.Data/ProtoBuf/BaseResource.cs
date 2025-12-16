using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class BaseResource : IDisposable, Pool.IPooled, IProto<BaseResource>, IProto
{
	[NonSerialized]
	public int stage;

	[NonSerialized]
	public float health;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(BaseResource instance)
	{
		if (instance.ShouldPool)
		{
			instance.stage = 0;
			instance.health = 0f;
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
			throw new Exception("Trying to dispose BaseResource with ShouldPool set to false!");
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

	public void CopyTo(BaseResource instance)
	{
		instance.stage = stage;
		instance.health = health;
	}

	public BaseResource Copy()
	{
		BaseResource baseResource = Pool.Get<BaseResource>();
		CopyTo(baseResource);
		return baseResource;
	}

	public static BaseResource Deserialize(BufferStream stream)
	{
		BaseResource baseResource = Pool.Get<BaseResource>();
		Deserialize(stream, baseResource, isDelta: false);
		return baseResource;
	}

	public static BaseResource DeserializeLengthDelimited(BufferStream stream)
	{
		BaseResource baseResource = Pool.Get<BaseResource>();
		DeserializeLengthDelimited(stream, baseResource, isDelta: false);
		return baseResource;
	}

	public static BaseResource DeserializeLength(BufferStream stream, int length)
	{
		BaseResource baseResource = Pool.Get<BaseResource>();
		DeserializeLength(stream, length, baseResource, isDelta: false);
		return baseResource;
	}

	public static BaseResource Deserialize(byte[] buffer)
	{
		BaseResource baseResource = Pool.Get<BaseResource>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, baseResource, isDelta: false);
		return baseResource;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, BaseResource previous)
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

	public static BaseResource Deserialize(BufferStream stream, BaseResource instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.stage = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 21:
				instance.health = ProtocolParser.ReadSingle(stream);
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

	public static BaseResource DeserializeLengthDelimited(BufferStream stream, BaseResource instance, bool isDelta)
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
				instance.stage = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 21:
				instance.health = ProtocolParser.ReadSingle(stream);
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

	public static BaseResource DeserializeLength(BufferStream stream, int length, BaseResource instance, bool isDelta)
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
				instance.stage = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 21:
				instance.health = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, BaseResource instance, BaseResource previous)
	{
		if (instance.stage != previous.stage)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.stage);
		}
		if (instance.health != previous.health)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.health);
		}
	}

	public static void Serialize(BufferStream stream, BaseResource instance)
	{
		if (instance.stage != 0)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.stage);
		}
		if (instance.health != 0f)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.health);
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
