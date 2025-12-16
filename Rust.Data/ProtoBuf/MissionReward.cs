using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class MissionReward : IDisposable, Pool.IPooled, IProto<MissionReward>, IProto
{
	[NonSerialized]
	public int itemID;

	[NonSerialized]
	public int itemAmount;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(MissionReward instance)
	{
		if (instance.ShouldPool)
		{
			instance.itemID = 0;
			instance.itemAmount = 0;
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
			throw new Exception("Trying to dispose MissionReward with ShouldPool set to false!");
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

	public void CopyTo(MissionReward instance)
	{
		instance.itemID = itemID;
		instance.itemAmount = itemAmount;
	}

	public MissionReward Copy()
	{
		MissionReward missionReward = Pool.Get<MissionReward>();
		CopyTo(missionReward);
		return missionReward;
	}

	public static MissionReward Deserialize(BufferStream stream)
	{
		MissionReward missionReward = Pool.Get<MissionReward>();
		Deserialize(stream, missionReward, isDelta: false);
		return missionReward;
	}

	public static MissionReward DeserializeLengthDelimited(BufferStream stream)
	{
		MissionReward missionReward = Pool.Get<MissionReward>();
		DeserializeLengthDelimited(stream, missionReward, isDelta: false);
		return missionReward;
	}

	public static MissionReward DeserializeLength(BufferStream stream, int length)
	{
		MissionReward missionReward = Pool.Get<MissionReward>();
		DeserializeLength(stream, length, missionReward, isDelta: false);
		return missionReward;
	}

	public static MissionReward Deserialize(byte[] buffer)
	{
		MissionReward missionReward = Pool.Get<MissionReward>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, missionReward, isDelta: false);
		return missionReward;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, MissionReward previous)
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

	public static MissionReward Deserialize(BufferStream stream, MissionReward instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.itemID = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.itemAmount = (int)ProtocolParser.ReadUInt64(stream);
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

	public static MissionReward DeserializeLengthDelimited(BufferStream stream, MissionReward instance, bool isDelta)
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
				instance.itemID = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.itemAmount = (int)ProtocolParser.ReadUInt64(stream);
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

	public static MissionReward DeserializeLength(BufferStream stream, int length, MissionReward instance, bool isDelta)
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
				instance.itemID = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.itemAmount = (int)ProtocolParser.ReadUInt64(stream);
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

	public static void SerializeDelta(BufferStream stream, MissionReward instance, MissionReward previous)
	{
		if (instance.itemID != previous.itemID)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.itemID);
		}
		if (instance.itemAmount != previous.itemAmount)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.itemAmount);
		}
	}

	public static void Serialize(BufferStream stream, MissionReward instance)
	{
		if (instance.itemID != 0)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.itemID);
		}
		if (instance.itemAmount != 0)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.itemAmount);
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
