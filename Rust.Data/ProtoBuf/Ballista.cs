using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class Ballista : IDisposable, Pool.IPooled, IProto<Ballista>, IProto
{
	[NonSerialized]
	public NetworkableId gunID;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(Ballista instance)
	{
		if (instance.ShouldPool)
		{
			instance.gunID = default(NetworkableId);
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
			throw new Exception("Trying to dispose Ballista with ShouldPool set to false!");
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

	public void CopyTo(Ballista instance)
	{
		instance.gunID = gunID;
	}

	public Ballista Copy()
	{
		Ballista ballista = Pool.Get<Ballista>();
		CopyTo(ballista);
		return ballista;
	}

	public static Ballista Deserialize(BufferStream stream)
	{
		Ballista ballista = Pool.Get<Ballista>();
		Deserialize(stream, ballista, isDelta: false);
		return ballista;
	}

	public static Ballista DeserializeLengthDelimited(BufferStream stream)
	{
		Ballista ballista = Pool.Get<Ballista>();
		DeserializeLengthDelimited(stream, ballista, isDelta: false);
		return ballista;
	}

	public static Ballista DeserializeLength(BufferStream stream, int length)
	{
		Ballista ballista = Pool.Get<Ballista>();
		DeserializeLength(stream, length, ballista, isDelta: false);
		return ballista;
	}

	public static Ballista Deserialize(byte[] buffer)
	{
		Ballista ballista = Pool.Get<Ballista>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, ballista, isDelta: false);
		return ballista;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, Ballista previous)
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

	public static Ballista Deserialize(BufferStream stream, Ballista instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.gunID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static Ballista DeserializeLengthDelimited(BufferStream stream, Ballista instance, bool isDelta)
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
				instance.gunID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static Ballista DeserializeLength(BufferStream stream, int length, Ballista instance, bool isDelta)
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
				instance.gunID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static void SerializeDelta(BufferStream stream, Ballista instance, Ballista previous)
	{
		stream.WriteByte(8);
		ProtocolParser.WriteUInt64(stream, instance.gunID.Value);
	}

	public static void Serialize(BufferStream stream, Ballista instance)
	{
		if (instance.gunID != default(NetworkableId))
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.gunID.Value);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref gunID.Value);
	}
}
