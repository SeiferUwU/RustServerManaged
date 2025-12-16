using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf.Nexus;

public class SpawnOptionsRequest : IDisposable, Pool.IPooled, IProto<SpawnOptionsRequest>, IProto
{
	[NonSerialized]
	public ulong userId;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(SpawnOptionsRequest instance)
	{
		if (instance.ShouldPool)
		{
			instance.userId = 0uL;
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
			throw new Exception("Trying to dispose SpawnOptionsRequest with ShouldPool set to false!");
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

	public void CopyTo(SpawnOptionsRequest instance)
	{
		instance.userId = userId;
	}

	public SpawnOptionsRequest Copy()
	{
		SpawnOptionsRequest spawnOptionsRequest = Pool.Get<SpawnOptionsRequest>();
		CopyTo(spawnOptionsRequest);
		return spawnOptionsRequest;
	}

	public static SpawnOptionsRequest Deserialize(BufferStream stream)
	{
		SpawnOptionsRequest spawnOptionsRequest = Pool.Get<SpawnOptionsRequest>();
		Deserialize(stream, spawnOptionsRequest, isDelta: false);
		return spawnOptionsRequest;
	}

	public static SpawnOptionsRequest DeserializeLengthDelimited(BufferStream stream)
	{
		SpawnOptionsRequest spawnOptionsRequest = Pool.Get<SpawnOptionsRequest>();
		DeserializeLengthDelimited(stream, spawnOptionsRequest, isDelta: false);
		return spawnOptionsRequest;
	}

	public static SpawnOptionsRequest DeserializeLength(BufferStream stream, int length)
	{
		SpawnOptionsRequest spawnOptionsRequest = Pool.Get<SpawnOptionsRequest>();
		DeserializeLength(stream, length, spawnOptionsRequest, isDelta: false);
		return spawnOptionsRequest;
	}

	public static SpawnOptionsRequest Deserialize(byte[] buffer)
	{
		SpawnOptionsRequest spawnOptionsRequest = Pool.Get<SpawnOptionsRequest>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, spawnOptionsRequest, isDelta: false);
		return spawnOptionsRequest;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, SpawnOptionsRequest previous)
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

	public static SpawnOptionsRequest Deserialize(BufferStream stream, SpawnOptionsRequest instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.userId = ProtocolParser.ReadUInt64(stream);
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

	public static SpawnOptionsRequest DeserializeLengthDelimited(BufferStream stream, SpawnOptionsRequest instance, bool isDelta)
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
				instance.userId = ProtocolParser.ReadUInt64(stream);
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

	public static SpawnOptionsRequest DeserializeLength(BufferStream stream, int length, SpawnOptionsRequest instance, bool isDelta)
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
				instance.userId = ProtocolParser.ReadUInt64(stream);
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

	public static void SerializeDelta(BufferStream stream, SpawnOptionsRequest instance, SpawnOptionsRequest previous)
	{
		if (instance.userId != previous.userId)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.userId);
		}
	}

	public static void Serialize(BufferStream stream, SpawnOptionsRequest instance)
	{
		if (instance.userId != 0L)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.userId);
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
