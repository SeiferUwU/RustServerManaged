using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class SiegeTower : IDisposable, Pool.IPooled, IProto<SiegeTower>, IProto
{
	[NonSerialized]
	public NetworkableId doorID;

	[NonSerialized]
	public NetworkableId drawBridgeID;

	[NonSerialized]
	public NetworkableId drawBridge2ID;

	[NonSerialized]
	public NetworkableId drawBridge3ID;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(SiegeTower instance)
	{
		if (instance.ShouldPool)
		{
			instance.doorID = default(NetworkableId);
			instance.drawBridgeID = default(NetworkableId);
			instance.drawBridge2ID = default(NetworkableId);
			instance.drawBridge3ID = default(NetworkableId);
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
			throw new Exception("Trying to dispose SiegeTower with ShouldPool set to false!");
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

	public void CopyTo(SiegeTower instance)
	{
		instance.doorID = doorID;
		instance.drawBridgeID = drawBridgeID;
		instance.drawBridge2ID = drawBridge2ID;
		instance.drawBridge3ID = drawBridge3ID;
	}

	public SiegeTower Copy()
	{
		SiegeTower siegeTower = Pool.Get<SiegeTower>();
		CopyTo(siegeTower);
		return siegeTower;
	}

	public static SiegeTower Deserialize(BufferStream stream)
	{
		SiegeTower siegeTower = Pool.Get<SiegeTower>();
		Deserialize(stream, siegeTower, isDelta: false);
		return siegeTower;
	}

	public static SiegeTower DeserializeLengthDelimited(BufferStream stream)
	{
		SiegeTower siegeTower = Pool.Get<SiegeTower>();
		DeserializeLengthDelimited(stream, siegeTower, isDelta: false);
		return siegeTower;
	}

	public static SiegeTower DeserializeLength(BufferStream stream, int length)
	{
		SiegeTower siegeTower = Pool.Get<SiegeTower>();
		DeserializeLength(stream, length, siegeTower, isDelta: false);
		return siegeTower;
	}

	public static SiegeTower Deserialize(byte[] buffer)
	{
		SiegeTower siegeTower = Pool.Get<SiegeTower>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, siegeTower, isDelta: false);
		return siegeTower;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, SiegeTower previous)
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

	public static SiegeTower Deserialize(BufferStream stream, SiegeTower instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.doorID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 16:
				instance.drawBridgeID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 24:
				instance.drawBridge2ID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 32:
				instance.drawBridge3ID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static SiegeTower DeserializeLengthDelimited(BufferStream stream, SiegeTower instance, bool isDelta)
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
				instance.doorID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 16:
				instance.drawBridgeID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 24:
				instance.drawBridge2ID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 32:
				instance.drawBridge3ID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static SiegeTower DeserializeLength(BufferStream stream, int length, SiegeTower instance, bool isDelta)
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
				instance.doorID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 16:
				instance.drawBridgeID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 24:
				instance.drawBridge2ID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 32:
				instance.drawBridge3ID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static void SerializeDelta(BufferStream stream, SiegeTower instance, SiegeTower previous)
	{
		stream.WriteByte(8);
		ProtocolParser.WriteUInt64(stream, instance.doorID.Value);
		stream.WriteByte(16);
		ProtocolParser.WriteUInt64(stream, instance.drawBridgeID.Value);
		stream.WriteByte(24);
		ProtocolParser.WriteUInt64(stream, instance.drawBridge2ID.Value);
		stream.WriteByte(32);
		ProtocolParser.WriteUInt64(stream, instance.drawBridge3ID.Value);
	}

	public static void Serialize(BufferStream stream, SiegeTower instance)
	{
		if (instance.doorID != default(NetworkableId))
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.doorID.Value);
		}
		if (instance.drawBridgeID != default(NetworkableId))
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.drawBridgeID.Value);
		}
		if (instance.drawBridge2ID != default(NetworkableId))
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, instance.drawBridge2ID.Value);
		}
		if (instance.drawBridge3ID != default(NetworkableId))
		{
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, instance.drawBridge3ID.Value);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref doorID.Value);
		action(UidType.NetworkableId, ref drawBridgeID.Value);
		action(UidType.NetworkableId, ref drawBridge2ID.Value);
		action(UidType.NetworkableId, ref drawBridge3ID.Value);
	}
}
