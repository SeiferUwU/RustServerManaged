using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class Catapult : IDisposable, Pool.IPooled, IProto<Catapult>, IProto
{
	[NonSerialized]
	public NetworkableId ammoStorageID;

	[NonSerialized]
	public float reloadProgress;

	[NonSerialized]
	public int ammoType;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(Catapult instance)
	{
		if (instance.ShouldPool)
		{
			instance.ammoStorageID = default(NetworkableId);
			instance.reloadProgress = 0f;
			instance.ammoType = 0;
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
			throw new Exception("Trying to dispose Catapult with ShouldPool set to false!");
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

	public void CopyTo(Catapult instance)
	{
		instance.ammoStorageID = ammoStorageID;
		instance.reloadProgress = reloadProgress;
		instance.ammoType = ammoType;
	}

	public Catapult Copy()
	{
		Catapult catapult = Pool.Get<Catapult>();
		CopyTo(catapult);
		return catapult;
	}

	public static Catapult Deserialize(BufferStream stream)
	{
		Catapult catapult = Pool.Get<Catapult>();
		Deserialize(stream, catapult, isDelta: false);
		return catapult;
	}

	public static Catapult DeserializeLengthDelimited(BufferStream stream)
	{
		Catapult catapult = Pool.Get<Catapult>();
		DeserializeLengthDelimited(stream, catapult, isDelta: false);
		return catapult;
	}

	public static Catapult DeserializeLength(BufferStream stream, int length)
	{
		Catapult catapult = Pool.Get<Catapult>();
		DeserializeLength(stream, length, catapult, isDelta: false);
		return catapult;
	}

	public static Catapult Deserialize(byte[] buffer)
	{
		Catapult catapult = Pool.Get<Catapult>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, catapult, isDelta: false);
		return catapult;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, Catapult previous)
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

	public static Catapult Deserialize(BufferStream stream, Catapult instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.ammoStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 21:
				instance.reloadProgress = ProtocolParser.ReadSingle(stream);
				continue;
			case 24:
				instance.ammoType = (int)ProtocolParser.ReadUInt64(stream);
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

	public static Catapult DeserializeLengthDelimited(BufferStream stream, Catapult instance, bool isDelta)
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
				instance.ammoStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 21:
				instance.reloadProgress = ProtocolParser.ReadSingle(stream);
				continue;
			case 24:
				instance.ammoType = (int)ProtocolParser.ReadUInt64(stream);
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

	public static Catapult DeserializeLength(BufferStream stream, int length, Catapult instance, bool isDelta)
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
				instance.ammoStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 21:
				instance.reloadProgress = ProtocolParser.ReadSingle(stream);
				continue;
			case 24:
				instance.ammoType = (int)ProtocolParser.ReadUInt64(stream);
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

	public static void SerializeDelta(BufferStream stream, Catapult instance, Catapult previous)
	{
		stream.WriteByte(8);
		ProtocolParser.WriteUInt64(stream, instance.ammoStorageID.Value);
		if (instance.reloadProgress != previous.reloadProgress)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.reloadProgress);
		}
		if (instance.ammoType != previous.ammoType)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ammoType);
		}
	}

	public static void Serialize(BufferStream stream, Catapult instance)
	{
		if (instance.ammoStorageID != default(NetworkableId))
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.ammoStorageID.Value);
		}
		if (instance.reloadProgress != 0f)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.reloadProgress);
		}
		if (instance.ammoType != 0)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ammoType);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref ammoStorageID.Value);
	}
}
