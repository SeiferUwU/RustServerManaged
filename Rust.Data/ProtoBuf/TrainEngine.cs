using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class TrainEngine : IDisposable, Pool.IPooled, IProto<TrainEngine>, IProto
{
	[NonSerialized]
	public int throttleSetting;

	[NonSerialized]
	public NetworkableId fuelStorageID;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(TrainEngine instance)
	{
		if (instance.ShouldPool)
		{
			instance.throttleSetting = 0;
			instance.fuelStorageID = default(NetworkableId);
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
			throw new Exception("Trying to dispose TrainEngine with ShouldPool set to false!");
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

	public void CopyTo(TrainEngine instance)
	{
		instance.throttleSetting = throttleSetting;
		instance.fuelStorageID = fuelStorageID;
	}

	public TrainEngine Copy()
	{
		TrainEngine trainEngine = Pool.Get<TrainEngine>();
		CopyTo(trainEngine);
		return trainEngine;
	}

	public static TrainEngine Deserialize(BufferStream stream)
	{
		TrainEngine trainEngine = Pool.Get<TrainEngine>();
		Deserialize(stream, trainEngine, isDelta: false);
		return trainEngine;
	}

	public static TrainEngine DeserializeLengthDelimited(BufferStream stream)
	{
		TrainEngine trainEngine = Pool.Get<TrainEngine>();
		DeserializeLengthDelimited(stream, trainEngine, isDelta: false);
		return trainEngine;
	}

	public static TrainEngine DeserializeLength(BufferStream stream, int length)
	{
		TrainEngine trainEngine = Pool.Get<TrainEngine>();
		DeserializeLength(stream, length, trainEngine, isDelta: false);
		return trainEngine;
	}

	public static TrainEngine Deserialize(byte[] buffer)
	{
		TrainEngine trainEngine = Pool.Get<TrainEngine>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, trainEngine, isDelta: false);
		return trainEngine;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, TrainEngine previous)
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

	public static TrainEngine Deserialize(BufferStream stream, TrainEngine instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.throttleSetting = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.fuelStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static TrainEngine DeserializeLengthDelimited(BufferStream stream, TrainEngine instance, bool isDelta)
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
				instance.throttleSetting = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.fuelStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static TrainEngine DeserializeLength(BufferStream stream, int length, TrainEngine instance, bool isDelta)
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
				instance.throttleSetting = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.fuelStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static void SerializeDelta(BufferStream stream, TrainEngine instance, TrainEngine previous)
	{
		if (instance.throttleSetting != previous.throttleSetting)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.throttleSetting);
		}
		stream.WriteByte(16);
		ProtocolParser.WriteUInt64(stream, instance.fuelStorageID.Value);
	}

	public static void Serialize(BufferStream stream, TrainEngine instance)
	{
		if (instance.throttleSetting != 0)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.throttleSetting);
		}
		if (instance.fuelStorageID != default(NetworkableId))
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.fuelStorageID.Value);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref fuelStorageID.Value);
	}
}
