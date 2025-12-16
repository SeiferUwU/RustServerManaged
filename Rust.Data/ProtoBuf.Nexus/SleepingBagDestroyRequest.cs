using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf.Nexus;

public class SleepingBagDestroyRequest : IDisposable, Pool.IPooled, IProto<SleepingBagDestroyRequest>, IProto
{
	[NonSerialized]
	public ulong userId;

	[NonSerialized]
	public NetworkableId sleepingBagId;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(SleepingBagDestroyRequest instance)
	{
		if (instance.ShouldPool)
		{
			instance.userId = 0uL;
			instance.sleepingBagId = default(NetworkableId);
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
			throw new Exception("Trying to dispose SleepingBagDestroyRequest with ShouldPool set to false!");
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

	public void CopyTo(SleepingBagDestroyRequest instance)
	{
		instance.userId = userId;
		instance.sleepingBagId = sleepingBagId;
	}

	public SleepingBagDestroyRequest Copy()
	{
		SleepingBagDestroyRequest sleepingBagDestroyRequest = Pool.Get<SleepingBagDestroyRequest>();
		CopyTo(sleepingBagDestroyRequest);
		return sleepingBagDestroyRequest;
	}

	public static SleepingBagDestroyRequest Deserialize(BufferStream stream)
	{
		SleepingBagDestroyRequest sleepingBagDestroyRequest = Pool.Get<SleepingBagDestroyRequest>();
		Deserialize(stream, sleepingBagDestroyRequest, isDelta: false);
		return sleepingBagDestroyRequest;
	}

	public static SleepingBagDestroyRequest DeserializeLengthDelimited(BufferStream stream)
	{
		SleepingBagDestroyRequest sleepingBagDestroyRequest = Pool.Get<SleepingBagDestroyRequest>();
		DeserializeLengthDelimited(stream, sleepingBagDestroyRequest, isDelta: false);
		return sleepingBagDestroyRequest;
	}

	public static SleepingBagDestroyRequest DeserializeLength(BufferStream stream, int length)
	{
		SleepingBagDestroyRequest sleepingBagDestroyRequest = Pool.Get<SleepingBagDestroyRequest>();
		DeserializeLength(stream, length, sleepingBagDestroyRequest, isDelta: false);
		return sleepingBagDestroyRequest;
	}

	public static SleepingBagDestroyRequest Deserialize(byte[] buffer)
	{
		SleepingBagDestroyRequest sleepingBagDestroyRequest = Pool.Get<SleepingBagDestroyRequest>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, sleepingBagDestroyRequest, isDelta: false);
		return sleepingBagDestroyRequest;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, SleepingBagDestroyRequest previous)
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

	public static SleepingBagDestroyRequest Deserialize(BufferStream stream, SleepingBagDestroyRequest instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.userId = ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.sleepingBagId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static SleepingBagDestroyRequest DeserializeLengthDelimited(BufferStream stream, SleepingBagDestroyRequest instance, bool isDelta)
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
				instance.userId = ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.sleepingBagId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static SleepingBagDestroyRequest DeserializeLength(BufferStream stream, int length, SleepingBagDestroyRequest instance, bool isDelta)
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
				instance.userId = ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.sleepingBagId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static void SerializeDelta(BufferStream stream, SleepingBagDestroyRequest instance, SleepingBagDestroyRequest previous)
	{
		if (instance.userId != previous.userId)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.userId);
		}
		stream.WriteByte(16);
		ProtocolParser.WriteUInt64(stream, instance.sleepingBagId.Value);
	}

	public static void Serialize(BufferStream stream, SleepingBagDestroyRequest instance)
	{
		if (instance.userId != 0L)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.userId);
		}
		if (instance.sleepingBagId != default(NetworkableId))
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.sleepingBagId.Value);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref sleepingBagId.Value);
	}
}
