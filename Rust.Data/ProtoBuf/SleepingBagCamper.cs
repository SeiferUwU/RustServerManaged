using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class SleepingBagCamper : IDisposable, Pool.IPooled, IProto<SleepingBagCamper>, IProto
{
	[NonSerialized]
	public NetworkableId seatID;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(SleepingBagCamper instance)
	{
		if (instance.ShouldPool)
		{
			instance.seatID = default(NetworkableId);
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
			throw new Exception("Trying to dispose SleepingBagCamper with ShouldPool set to false!");
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

	public void CopyTo(SleepingBagCamper instance)
	{
		instance.seatID = seatID;
	}

	public SleepingBagCamper Copy()
	{
		SleepingBagCamper sleepingBagCamper = Pool.Get<SleepingBagCamper>();
		CopyTo(sleepingBagCamper);
		return sleepingBagCamper;
	}

	public static SleepingBagCamper Deserialize(BufferStream stream)
	{
		SleepingBagCamper sleepingBagCamper = Pool.Get<SleepingBagCamper>();
		Deserialize(stream, sleepingBagCamper, isDelta: false);
		return sleepingBagCamper;
	}

	public static SleepingBagCamper DeserializeLengthDelimited(BufferStream stream)
	{
		SleepingBagCamper sleepingBagCamper = Pool.Get<SleepingBagCamper>();
		DeserializeLengthDelimited(stream, sleepingBagCamper, isDelta: false);
		return sleepingBagCamper;
	}

	public static SleepingBagCamper DeserializeLength(BufferStream stream, int length)
	{
		SleepingBagCamper sleepingBagCamper = Pool.Get<SleepingBagCamper>();
		DeserializeLength(stream, length, sleepingBagCamper, isDelta: false);
		return sleepingBagCamper;
	}

	public static SleepingBagCamper Deserialize(byte[] buffer)
	{
		SleepingBagCamper sleepingBagCamper = Pool.Get<SleepingBagCamper>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, sleepingBagCamper, isDelta: false);
		return sleepingBagCamper;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, SleepingBagCamper previous)
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

	public static SleepingBagCamper Deserialize(BufferStream stream, SleepingBagCamper instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.seatID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static SleepingBagCamper DeserializeLengthDelimited(BufferStream stream, SleepingBagCamper instance, bool isDelta)
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
				instance.seatID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static SleepingBagCamper DeserializeLength(BufferStream stream, int length, SleepingBagCamper instance, bool isDelta)
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
				instance.seatID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static void SerializeDelta(BufferStream stream, SleepingBagCamper instance, SleepingBagCamper previous)
	{
		stream.WriteByte(8);
		ProtocolParser.WriteUInt64(stream, instance.seatID.Value);
	}

	public static void Serialize(BufferStream stream, SleepingBagCamper instance)
	{
		if (instance.seatID != default(NetworkableId))
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.seatID.Value);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref seatID.Value);
	}
}
