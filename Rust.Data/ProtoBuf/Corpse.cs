using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class Corpse : IDisposable, Pool.IPooled, IProto<Corpse>, IProto
{
	[NonSerialized]
	public NetworkableId parentID;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(Corpse instance)
	{
		if (instance.ShouldPool)
		{
			instance.parentID = default(NetworkableId);
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
			throw new Exception("Trying to dispose Corpse with ShouldPool set to false!");
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

	public void CopyTo(Corpse instance)
	{
		instance.parentID = parentID;
	}

	public Corpse Copy()
	{
		Corpse corpse = Pool.Get<Corpse>();
		CopyTo(corpse);
		return corpse;
	}

	public static Corpse Deserialize(BufferStream stream)
	{
		Corpse corpse = Pool.Get<Corpse>();
		Deserialize(stream, corpse, isDelta: false);
		return corpse;
	}

	public static Corpse DeserializeLengthDelimited(BufferStream stream)
	{
		Corpse corpse = Pool.Get<Corpse>();
		DeserializeLengthDelimited(stream, corpse, isDelta: false);
		return corpse;
	}

	public static Corpse DeserializeLength(BufferStream stream, int length)
	{
		Corpse corpse = Pool.Get<Corpse>();
		DeserializeLength(stream, length, corpse, isDelta: false);
		return corpse;
	}

	public static Corpse Deserialize(byte[] buffer)
	{
		Corpse corpse = Pool.Get<Corpse>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, corpse, isDelta: false);
		return corpse;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, Corpse previous)
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

	public static Corpse Deserialize(BufferStream stream, Corpse instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.parentID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static Corpse DeserializeLengthDelimited(BufferStream stream, Corpse instance, bool isDelta)
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
				instance.parentID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static Corpse DeserializeLength(BufferStream stream, int length, Corpse instance, bool isDelta)
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
				instance.parentID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static void SerializeDelta(BufferStream stream, Corpse instance, Corpse previous)
	{
		stream.WriteByte(8);
		ProtocolParser.WriteUInt64(stream, instance.parentID.Value);
	}

	public static void Serialize(BufferStream stream, Corpse instance)
	{
		if (instance.parentID != default(NetworkableId))
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.parentID.Value);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref parentID.Value);
	}
}
