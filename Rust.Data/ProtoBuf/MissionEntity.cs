using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class MissionEntity : IDisposable, Pool.IPooled, IProto<MissionEntity>, IProto
{
	[NonSerialized]
	public string identifier;

	[NonSerialized]
	public NetworkableId entityID;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(MissionEntity instance)
	{
		if (instance.ShouldPool)
		{
			instance.identifier = string.Empty;
			instance.entityID = default(NetworkableId);
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
			throw new Exception("Trying to dispose MissionEntity with ShouldPool set to false!");
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

	public void CopyTo(MissionEntity instance)
	{
		instance.identifier = identifier;
		instance.entityID = entityID;
	}

	public MissionEntity Copy()
	{
		MissionEntity missionEntity = Pool.Get<MissionEntity>();
		CopyTo(missionEntity);
		return missionEntity;
	}

	public static MissionEntity Deserialize(BufferStream stream)
	{
		MissionEntity missionEntity = Pool.Get<MissionEntity>();
		Deserialize(stream, missionEntity, isDelta: false);
		return missionEntity;
	}

	public static MissionEntity DeserializeLengthDelimited(BufferStream stream)
	{
		MissionEntity missionEntity = Pool.Get<MissionEntity>();
		DeserializeLengthDelimited(stream, missionEntity, isDelta: false);
		return missionEntity;
	}

	public static MissionEntity DeserializeLength(BufferStream stream, int length)
	{
		MissionEntity missionEntity = Pool.Get<MissionEntity>();
		DeserializeLength(stream, length, missionEntity, isDelta: false);
		return missionEntity;
	}

	public static MissionEntity Deserialize(byte[] buffer)
	{
		MissionEntity missionEntity = Pool.Get<MissionEntity>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, missionEntity, isDelta: false);
		return missionEntity;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, MissionEntity previous)
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

	public static MissionEntity Deserialize(BufferStream stream, MissionEntity instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 10:
				instance.identifier = ProtocolParser.ReadString(stream);
				continue;
			case 16:
				instance.entityID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static MissionEntity DeserializeLengthDelimited(BufferStream stream, MissionEntity instance, bool isDelta)
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
				instance.identifier = ProtocolParser.ReadString(stream);
				continue;
			case 16:
				instance.entityID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static MissionEntity DeserializeLength(BufferStream stream, int length, MissionEntity instance, bool isDelta)
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
				instance.identifier = ProtocolParser.ReadString(stream);
				continue;
			case 16:
				instance.entityID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static void SerializeDelta(BufferStream stream, MissionEntity instance, MissionEntity previous)
	{
		if (instance.identifier != previous.identifier)
		{
			if (instance.identifier == null)
			{
				throw new ArgumentNullException("identifier", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteString(stream, instance.identifier);
		}
		stream.WriteByte(16);
		ProtocolParser.WriteUInt64(stream, instance.entityID.Value);
	}

	public static void Serialize(BufferStream stream, MissionEntity instance)
	{
		if (instance.identifier == null)
		{
			throw new ArgumentNullException("identifier", "Required by proto specification.");
		}
		stream.WriteByte(10);
		ProtocolParser.WriteString(stream, instance.identifier);
		if (instance.entityID != default(NetworkableId))
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.entityID.Value);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref entityID.Value);
	}
}
