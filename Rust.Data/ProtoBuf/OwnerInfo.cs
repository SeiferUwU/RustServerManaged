using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class OwnerInfo : IDisposable, Pool.IPooled, IProto<OwnerInfo>, IProto
{
	[NonSerialized]
	public ulong steamid;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(OwnerInfo instance)
	{
		if (instance.ShouldPool)
		{
			instance.steamid = 0uL;
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
			throw new Exception("Trying to dispose OwnerInfo with ShouldPool set to false!");
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

	public void CopyTo(OwnerInfo instance)
	{
		instance.steamid = steamid;
	}

	public OwnerInfo Copy()
	{
		OwnerInfo ownerInfo = Pool.Get<OwnerInfo>();
		CopyTo(ownerInfo);
		return ownerInfo;
	}

	public static OwnerInfo Deserialize(BufferStream stream)
	{
		OwnerInfo ownerInfo = Pool.Get<OwnerInfo>();
		Deserialize(stream, ownerInfo, isDelta: false);
		return ownerInfo;
	}

	public static OwnerInfo DeserializeLengthDelimited(BufferStream stream)
	{
		OwnerInfo ownerInfo = Pool.Get<OwnerInfo>();
		DeserializeLengthDelimited(stream, ownerInfo, isDelta: false);
		return ownerInfo;
	}

	public static OwnerInfo DeserializeLength(BufferStream stream, int length)
	{
		OwnerInfo ownerInfo = Pool.Get<OwnerInfo>();
		DeserializeLength(stream, length, ownerInfo, isDelta: false);
		return ownerInfo;
	}

	public static OwnerInfo Deserialize(byte[] buffer)
	{
		OwnerInfo ownerInfo = Pool.Get<OwnerInfo>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, ownerInfo, isDelta: false);
		return ownerInfo;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, OwnerInfo previous)
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

	public static OwnerInfo Deserialize(BufferStream stream, OwnerInfo instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.steamid = ProtocolParser.ReadUInt64(stream);
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

	public static OwnerInfo DeserializeLengthDelimited(BufferStream stream, OwnerInfo instance, bool isDelta)
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
				instance.steamid = ProtocolParser.ReadUInt64(stream);
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

	public static OwnerInfo DeserializeLength(BufferStream stream, int length, OwnerInfo instance, bool isDelta)
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
				instance.steamid = ProtocolParser.ReadUInt64(stream);
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

	public static void SerializeDelta(BufferStream stream, OwnerInfo instance, OwnerInfo previous)
	{
		if (instance.steamid != previous.steamid)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.steamid);
		}
	}

	public static void Serialize(BufferStream stream, OwnerInfo instance)
	{
		if (instance.steamid != 0L)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.steamid);
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
