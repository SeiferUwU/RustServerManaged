using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class SphereEntity : IDisposable, Pool.IPooled, IProto<SphereEntity>, IProto
{
	[NonSerialized]
	public float radius;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(SphereEntity instance)
	{
		if (instance.ShouldPool)
		{
			instance.radius = 0f;
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
			throw new Exception("Trying to dispose SphereEntity with ShouldPool set to false!");
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

	public void CopyTo(SphereEntity instance)
	{
		instance.radius = radius;
	}

	public SphereEntity Copy()
	{
		SphereEntity sphereEntity = Pool.Get<SphereEntity>();
		CopyTo(sphereEntity);
		return sphereEntity;
	}

	public static SphereEntity Deserialize(BufferStream stream)
	{
		SphereEntity sphereEntity = Pool.Get<SphereEntity>();
		Deserialize(stream, sphereEntity, isDelta: false);
		return sphereEntity;
	}

	public static SphereEntity DeserializeLengthDelimited(BufferStream stream)
	{
		SphereEntity sphereEntity = Pool.Get<SphereEntity>();
		DeserializeLengthDelimited(stream, sphereEntity, isDelta: false);
		return sphereEntity;
	}

	public static SphereEntity DeserializeLength(BufferStream stream, int length)
	{
		SphereEntity sphereEntity = Pool.Get<SphereEntity>();
		DeserializeLength(stream, length, sphereEntity, isDelta: false);
		return sphereEntity;
	}

	public static SphereEntity Deserialize(byte[] buffer)
	{
		SphereEntity sphereEntity = Pool.Get<SphereEntity>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, sphereEntity, isDelta: false);
		return sphereEntity;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, SphereEntity previous)
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

	public static SphereEntity Deserialize(BufferStream stream, SphereEntity instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 13:
				instance.radius = ProtocolParser.ReadSingle(stream);
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

	public static SphereEntity DeserializeLengthDelimited(BufferStream stream, SphereEntity instance, bool isDelta)
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
				instance.radius = ProtocolParser.ReadSingle(stream);
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

	public static SphereEntity DeserializeLength(BufferStream stream, int length, SphereEntity instance, bool isDelta)
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
				instance.radius = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, SphereEntity instance, SphereEntity previous)
	{
		if (instance.radius != previous.radius)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.radius);
		}
	}

	public static void Serialize(BufferStream stream, SphereEntity instance)
	{
		if (instance.radius != 0f)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.radius);
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
