using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class ChickenStatus : IDisposable, Pool.IPooled, IProto<ChickenStatus>, IProto
{
	[NonSerialized]
	public NetworkableId spawnedChicken;

	[NonSerialized]
	public float timeUntilHatch;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(ChickenStatus instance)
	{
		if (instance.ShouldPool)
		{
			instance.spawnedChicken = default(NetworkableId);
			instance.timeUntilHatch = 0f;
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
			throw new Exception("Trying to dispose ChickenStatus with ShouldPool set to false!");
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

	public void CopyTo(ChickenStatus instance)
	{
		instance.spawnedChicken = spawnedChicken;
		instance.timeUntilHatch = timeUntilHatch;
	}

	public ChickenStatus Copy()
	{
		ChickenStatus chickenStatus = Pool.Get<ChickenStatus>();
		CopyTo(chickenStatus);
		return chickenStatus;
	}

	public static ChickenStatus Deserialize(BufferStream stream)
	{
		ChickenStatus chickenStatus = Pool.Get<ChickenStatus>();
		Deserialize(stream, chickenStatus, isDelta: false);
		return chickenStatus;
	}

	public static ChickenStatus DeserializeLengthDelimited(BufferStream stream)
	{
		ChickenStatus chickenStatus = Pool.Get<ChickenStatus>();
		DeserializeLengthDelimited(stream, chickenStatus, isDelta: false);
		return chickenStatus;
	}

	public static ChickenStatus DeserializeLength(BufferStream stream, int length)
	{
		ChickenStatus chickenStatus = Pool.Get<ChickenStatus>();
		DeserializeLength(stream, length, chickenStatus, isDelta: false);
		return chickenStatus;
	}

	public static ChickenStatus Deserialize(byte[] buffer)
	{
		ChickenStatus chickenStatus = Pool.Get<ChickenStatus>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, chickenStatus, isDelta: false);
		return chickenStatus;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, ChickenStatus previous)
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

	public static ChickenStatus Deserialize(BufferStream stream, ChickenStatus instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.spawnedChicken = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 21:
				instance.timeUntilHatch = ProtocolParser.ReadSingle(stream);
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

	public static ChickenStatus DeserializeLengthDelimited(BufferStream stream, ChickenStatus instance, bool isDelta)
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
				instance.spawnedChicken = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 21:
				instance.timeUntilHatch = ProtocolParser.ReadSingle(stream);
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

	public static ChickenStatus DeserializeLength(BufferStream stream, int length, ChickenStatus instance, bool isDelta)
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
				instance.spawnedChicken = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 21:
				instance.timeUntilHatch = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, ChickenStatus instance, ChickenStatus previous)
	{
		stream.WriteByte(8);
		ProtocolParser.WriteUInt64(stream, instance.spawnedChicken.Value);
		if (instance.timeUntilHatch != previous.timeUntilHatch)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.timeUntilHatch);
		}
	}

	public static void Serialize(BufferStream stream, ChickenStatus instance)
	{
		if (instance.spawnedChicken != default(NetworkableId))
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.spawnedChicken.Value);
		}
		if (instance.timeUntilHatch != 0f)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.timeUntilHatch);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref spawnedChicken.Value);
	}
}
