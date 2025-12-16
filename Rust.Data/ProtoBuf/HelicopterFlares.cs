using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class HelicopterFlares : IDisposable, Pool.IPooled, IProto<HelicopterFlares>, IProto
{
	[NonSerialized]
	public NetworkableId flareStorageID;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(HelicopterFlares instance)
	{
		if (instance.ShouldPool)
		{
			instance.flareStorageID = default(NetworkableId);
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
			throw new Exception("Trying to dispose HelicopterFlares with ShouldPool set to false!");
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

	public void CopyTo(HelicopterFlares instance)
	{
		instance.flareStorageID = flareStorageID;
	}

	public HelicopterFlares Copy()
	{
		HelicopterFlares helicopterFlares = Pool.Get<HelicopterFlares>();
		CopyTo(helicopterFlares);
		return helicopterFlares;
	}

	public static HelicopterFlares Deserialize(BufferStream stream)
	{
		HelicopterFlares helicopterFlares = Pool.Get<HelicopterFlares>();
		Deserialize(stream, helicopterFlares, isDelta: false);
		return helicopterFlares;
	}

	public static HelicopterFlares DeserializeLengthDelimited(BufferStream stream)
	{
		HelicopterFlares helicopterFlares = Pool.Get<HelicopterFlares>();
		DeserializeLengthDelimited(stream, helicopterFlares, isDelta: false);
		return helicopterFlares;
	}

	public static HelicopterFlares DeserializeLength(BufferStream stream, int length)
	{
		HelicopterFlares helicopterFlares = Pool.Get<HelicopterFlares>();
		DeserializeLength(stream, length, helicopterFlares, isDelta: false);
		return helicopterFlares;
	}

	public static HelicopterFlares Deserialize(byte[] buffer)
	{
		HelicopterFlares helicopterFlares = Pool.Get<HelicopterFlares>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, helicopterFlares, isDelta: false);
		return helicopterFlares;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, HelicopterFlares previous)
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

	public static HelicopterFlares Deserialize(BufferStream stream, HelicopterFlares instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.flareStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static HelicopterFlares DeserializeLengthDelimited(BufferStream stream, HelicopterFlares instance, bool isDelta)
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
				instance.flareStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static HelicopterFlares DeserializeLength(BufferStream stream, int length, HelicopterFlares instance, bool isDelta)
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
				instance.flareStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static void SerializeDelta(BufferStream stream, HelicopterFlares instance, HelicopterFlares previous)
	{
		stream.WriteByte(8);
		ProtocolParser.WriteUInt64(stream, instance.flareStorageID.Value);
	}

	public static void Serialize(BufferStream stream, HelicopterFlares instance)
	{
		if (instance.flareStorageID != default(NetworkableId))
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.flareStorageID.Value);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref flareStorageID.Value);
	}
}
