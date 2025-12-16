using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class BaseTrain : IDisposable, Pool.IPooled, IProto<BaseTrain>, IProto
{
	[NonSerialized]
	public float time;

	[NonSerialized]
	public float frontBogieYRot;

	[NonSerialized]
	public float rearBogieYRot;

	[NonSerialized]
	public NetworkableId frontCouplingID;

	[NonSerialized]
	public bool frontCouplingToFront;

	[NonSerialized]
	public NetworkableId rearCouplingID;

	[NonSerialized]
	public bool rearCouplingToFront;

	[NonSerialized]
	public int lootTypeIndex;

	[NonSerialized]
	public float lootPercent;

	[NonSerialized]
	public NetworkableId itemStorageID;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(BaseTrain instance)
	{
		if (instance.ShouldPool)
		{
			instance.time = 0f;
			instance.frontBogieYRot = 0f;
			instance.rearBogieYRot = 0f;
			instance.frontCouplingID = default(NetworkableId);
			instance.frontCouplingToFront = false;
			instance.rearCouplingID = default(NetworkableId);
			instance.rearCouplingToFront = false;
			instance.lootTypeIndex = 0;
			instance.lootPercent = 0f;
			instance.itemStorageID = default(NetworkableId);
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
			throw new Exception("Trying to dispose BaseTrain with ShouldPool set to false!");
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

	public void CopyTo(BaseTrain instance)
	{
		instance.time = time;
		instance.frontBogieYRot = frontBogieYRot;
		instance.rearBogieYRot = rearBogieYRot;
		instance.frontCouplingID = frontCouplingID;
		instance.frontCouplingToFront = frontCouplingToFront;
		instance.rearCouplingID = rearCouplingID;
		instance.rearCouplingToFront = rearCouplingToFront;
		instance.lootTypeIndex = lootTypeIndex;
		instance.lootPercent = lootPercent;
		instance.itemStorageID = itemStorageID;
	}

	public BaseTrain Copy()
	{
		BaseTrain baseTrain = Pool.Get<BaseTrain>();
		CopyTo(baseTrain);
		return baseTrain;
	}

	public static BaseTrain Deserialize(BufferStream stream)
	{
		BaseTrain baseTrain = Pool.Get<BaseTrain>();
		Deserialize(stream, baseTrain, isDelta: false);
		return baseTrain;
	}

	public static BaseTrain DeserializeLengthDelimited(BufferStream stream)
	{
		BaseTrain baseTrain = Pool.Get<BaseTrain>();
		DeserializeLengthDelimited(stream, baseTrain, isDelta: false);
		return baseTrain;
	}

	public static BaseTrain DeserializeLength(BufferStream stream, int length)
	{
		BaseTrain baseTrain = Pool.Get<BaseTrain>();
		DeserializeLength(stream, length, baseTrain, isDelta: false);
		return baseTrain;
	}

	public static BaseTrain Deserialize(byte[] buffer)
	{
		BaseTrain baseTrain = Pool.Get<BaseTrain>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, baseTrain, isDelta: false);
		return baseTrain;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, BaseTrain previous)
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

	public static BaseTrain Deserialize(BufferStream stream, BaseTrain instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 13:
				instance.time = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.frontBogieYRot = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.rearBogieYRot = ProtocolParser.ReadSingle(stream);
				continue;
			case 32:
				instance.frontCouplingID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 40:
				instance.frontCouplingToFront = ProtocolParser.ReadBool(stream);
				continue;
			case 48:
				instance.rearCouplingID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 56:
				instance.rearCouplingToFront = ProtocolParser.ReadBool(stream);
				continue;
			case 64:
				instance.lootTypeIndex = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 77:
				instance.lootPercent = ProtocolParser.ReadSingle(stream);
				continue;
			case 80:
				instance.itemStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static BaseTrain DeserializeLengthDelimited(BufferStream stream, BaseTrain instance, bool isDelta)
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
				instance.time = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.frontBogieYRot = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.rearBogieYRot = ProtocolParser.ReadSingle(stream);
				continue;
			case 32:
				instance.frontCouplingID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 40:
				instance.frontCouplingToFront = ProtocolParser.ReadBool(stream);
				continue;
			case 48:
				instance.rearCouplingID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 56:
				instance.rearCouplingToFront = ProtocolParser.ReadBool(stream);
				continue;
			case 64:
				instance.lootTypeIndex = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 77:
				instance.lootPercent = ProtocolParser.ReadSingle(stream);
				continue;
			case 80:
				instance.itemStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static BaseTrain DeserializeLength(BufferStream stream, int length, BaseTrain instance, bool isDelta)
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
				instance.time = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.frontBogieYRot = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.rearBogieYRot = ProtocolParser.ReadSingle(stream);
				continue;
			case 32:
				instance.frontCouplingID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 40:
				instance.frontCouplingToFront = ProtocolParser.ReadBool(stream);
				continue;
			case 48:
				instance.rearCouplingID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 56:
				instance.rearCouplingToFront = ProtocolParser.ReadBool(stream);
				continue;
			case 64:
				instance.lootTypeIndex = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 77:
				instance.lootPercent = ProtocolParser.ReadSingle(stream);
				continue;
			case 80:
				instance.itemStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static void SerializeDelta(BufferStream stream, BaseTrain instance, BaseTrain previous)
	{
		if (instance.time != previous.time)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.time);
		}
		if (instance.frontBogieYRot != previous.frontBogieYRot)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.frontBogieYRot);
		}
		if (instance.rearBogieYRot != previous.rearBogieYRot)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.rearBogieYRot);
		}
		stream.WriteByte(32);
		ProtocolParser.WriteUInt64(stream, instance.frontCouplingID.Value);
		stream.WriteByte(40);
		ProtocolParser.WriteBool(stream, instance.frontCouplingToFront);
		stream.WriteByte(48);
		ProtocolParser.WriteUInt64(stream, instance.rearCouplingID.Value);
		stream.WriteByte(56);
		ProtocolParser.WriteBool(stream, instance.rearCouplingToFront);
		if (instance.lootTypeIndex != previous.lootTypeIndex)
		{
			stream.WriteByte(64);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.lootTypeIndex);
		}
		if (instance.lootPercent != previous.lootPercent)
		{
			stream.WriteByte(77);
			ProtocolParser.WriteSingle(stream, instance.lootPercent);
		}
		stream.WriteByte(80);
		ProtocolParser.WriteUInt64(stream, instance.itemStorageID.Value);
	}

	public static void Serialize(BufferStream stream, BaseTrain instance)
	{
		if (instance.time != 0f)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.time);
		}
		if (instance.frontBogieYRot != 0f)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.frontBogieYRot);
		}
		if (instance.rearBogieYRot != 0f)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.rearBogieYRot);
		}
		if (instance.frontCouplingID != default(NetworkableId))
		{
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, instance.frontCouplingID.Value);
		}
		if (instance.frontCouplingToFront)
		{
			stream.WriteByte(40);
			ProtocolParser.WriteBool(stream, instance.frontCouplingToFront);
		}
		if (instance.rearCouplingID != default(NetworkableId))
		{
			stream.WriteByte(48);
			ProtocolParser.WriteUInt64(stream, instance.rearCouplingID.Value);
		}
		if (instance.rearCouplingToFront)
		{
			stream.WriteByte(56);
			ProtocolParser.WriteBool(stream, instance.rearCouplingToFront);
		}
		if (instance.lootTypeIndex != 0)
		{
			stream.WriteByte(64);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.lootTypeIndex);
		}
		if (instance.lootPercent != 0f)
		{
			stream.WriteByte(77);
			ProtocolParser.WriteSingle(stream, instance.lootPercent);
		}
		if (instance.itemStorageID != default(NetworkableId))
		{
			stream.WriteByte(80);
			ProtocolParser.WriteUInt64(stream, instance.itemStorageID.Value);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref frontCouplingID.Value);
		action(UidType.NetworkableId, ref rearCouplingID.Value);
		action(UidType.NetworkableId, ref itemStorageID.Value);
	}
}
