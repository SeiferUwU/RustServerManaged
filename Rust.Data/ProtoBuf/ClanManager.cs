using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class ClanManager : IDisposable, Pool.IPooled, IProto<ClanManager>, IProto
{
	[NonSerialized]
	public string backendType;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(ClanManager instance)
	{
		if (instance.ShouldPool)
		{
			instance.backendType = string.Empty;
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
			throw new Exception("Trying to dispose ClanManager with ShouldPool set to false!");
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

	public void CopyTo(ClanManager instance)
	{
		instance.backendType = backendType;
	}

	public ClanManager Copy()
	{
		ClanManager clanManager = Pool.Get<ClanManager>();
		CopyTo(clanManager);
		return clanManager;
	}

	public static ClanManager Deserialize(BufferStream stream)
	{
		ClanManager clanManager = Pool.Get<ClanManager>();
		Deserialize(stream, clanManager, isDelta: false);
		return clanManager;
	}

	public static ClanManager DeserializeLengthDelimited(BufferStream stream)
	{
		ClanManager clanManager = Pool.Get<ClanManager>();
		DeserializeLengthDelimited(stream, clanManager, isDelta: false);
		return clanManager;
	}

	public static ClanManager DeserializeLength(BufferStream stream, int length)
	{
		ClanManager clanManager = Pool.Get<ClanManager>();
		DeserializeLength(stream, length, clanManager, isDelta: false);
		return clanManager;
	}

	public static ClanManager Deserialize(byte[] buffer)
	{
		ClanManager clanManager = Pool.Get<ClanManager>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, clanManager, isDelta: false);
		return clanManager;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, ClanManager previous)
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

	public static ClanManager Deserialize(BufferStream stream, ClanManager instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 10:
				instance.backendType = ProtocolParser.ReadString(stream);
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

	public static ClanManager DeserializeLengthDelimited(BufferStream stream, ClanManager instance, bool isDelta)
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
			case 10:
				instance.backendType = ProtocolParser.ReadString(stream);
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

	public static ClanManager DeserializeLength(BufferStream stream, int length, ClanManager instance, bool isDelta)
	{
		long num = stream.Position + length;
		while (stream.Position < num)
		{
			int num2 = stream.ReadByte();
			switch (num2)
			{
			case -1:
				throw new EndOfStreamException();
			case 10:
				instance.backendType = ProtocolParser.ReadString(stream);
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

	public static void SerializeDelta(BufferStream stream, ClanManager instance, ClanManager previous)
	{
		if (instance.backendType != previous.backendType)
		{
			if (instance.backendType == null)
			{
				throw new ArgumentNullException("backendType", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteString(stream, instance.backendType);
		}
	}

	public static void Serialize(BufferStream stream, ClanManager instance)
	{
		if (instance.backendType == null)
		{
			throw new ArgumentNullException("backendType", "Required by proto specification.");
		}
		stream.WriteByte(10);
		ProtocolParser.WriteString(stream, instance.backendType);
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
	}
}
