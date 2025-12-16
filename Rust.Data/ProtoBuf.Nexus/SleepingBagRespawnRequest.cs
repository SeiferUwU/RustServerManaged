using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf.Nexus;

public class SleepingBagRespawnRequest : IDisposable, Pool.IPooled, IProto<SleepingBagRespawnRequest>, IProto
{
	[NonSerialized]
	public ulong userId;

	[NonSerialized]
	public NetworkableId sleepingBagId;

	[NonSerialized]
	public PlayerSecondaryData secondaryData;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(SleepingBagRespawnRequest instance)
	{
		if (instance.ShouldPool)
		{
			instance.userId = 0uL;
			instance.sleepingBagId = default(NetworkableId);
			if (instance.secondaryData != null)
			{
				instance.secondaryData.ResetToPool();
				instance.secondaryData = null;
			}
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
			throw new Exception("Trying to dispose SleepingBagRespawnRequest with ShouldPool set to false!");
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

	public void CopyTo(SleepingBagRespawnRequest instance)
	{
		instance.userId = userId;
		instance.sleepingBagId = sleepingBagId;
		if (secondaryData != null)
		{
			if (instance.secondaryData == null)
			{
				instance.secondaryData = secondaryData.Copy();
			}
			else
			{
				secondaryData.CopyTo(instance.secondaryData);
			}
		}
		else
		{
			instance.secondaryData = null;
		}
	}

	public SleepingBagRespawnRequest Copy()
	{
		SleepingBagRespawnRequest sleepingBagRespawnRequest = Pool.Get<SleepingBagRespawnRequest>();
		CopyTo(sleepingBagRespawnRequest);
		return sleepingBagRespawnRequest;
	}

	public static SleepingBagRespawnRequest Deserialize(BufferStream stream)
	{
		SleepingBagRespawnRequest sleepingBagRespawnRequest = Pool.Get<SleepingBagRespawnRequest>();
		Deserialize(stream, sleepingBagRespawnRequest, isDelta: false);
		return sleepingBagRespawnRequest;
	}

	public static SleepingBagRespawnRequest DeserializeLengthDelimited(BufferStream stream)
	{
		SleepingBagRespawnRequest sleepingBagRespawnRequest = Pool.Get<SleepingBagRespawnRequest>();
		DeserializeLengthDelimited(stream, sleepingBagRespawnRequest, isDelta: false);
		return sleepingBagRespawnRequest;
	}

	public static SleepingBagRespawnRequest DeserializeLength(BufferStream stream, int length)
	{
		SleepingBagRespawnRequest sleepingBagRespawnRequest = Pool.Get<SleepingBagRespawnRequest>();
		DeserializeLength(stream, length, sleepingBagRespawnRequest, isDelta: false);
		return sleepingBagRespawnRequest;
	}

	public static SleepingBagRespawnRequest Deserialize(byte[] buffer)
	{
		SleepingBagRespawnRequest sleepingBagRespawnRequest = Pool.Get<SleepingBagRespawnRequest>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, sleepingBagRespawnRequest, isDelta: false);
		return sleepingBagRespawnRequest;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, SleepingBagRespawnRequest previous)
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

	public static SleepingBagRespawnRequest Deserialize(BufferStream stream, SleepingBagRespawnRequest instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.userId = ProtocolParser.ReadUInt64(stream);
				break;
			case 16:
				instance.sleepingBagId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				break;
			case 26:
				if (instance.secondaryData == null)
				{
					instance.secondaryData = PlayerSecondaryData.DeserializeLengthDelimited(stream);
				}
				else
				{
					PlayerSecondaryData.DeserializeLengthDelimited(stream, instance.secondaryData, isDelta);
				}
				break;
			default:
			{
				Key key = ProtocolParser.ReadKey((byte)num, stream);
				_ = key.Field;
				ProtocolParser.SkipKey(stream, key);
				break;
			}
			case -1:
			case 0:
				return instance;
			}
		}
	}

	public static SleepingBagRespawnRequest DeserializeLengthDelimited(BufferStream stream, SleepingBagRespawnRequest instance, bool isDelta)
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
				break;
			case 16:
				instance.sleepingBagId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				break;
			case 26:
				if (instance.secondaryData == null)
				{
					instance.secondaryData = PlayerSecondaryData.DeserializeLengthDelimited(stream);
				}
				else
				{
					PlayerSecondaryData.DeserializeLengthDelimited(stream, instance.secondaryData, isDelta);
				}
				break;
			default:
			{
				Key key = ProtocolParser.ReadKey((byte)num2, stream);
				_ = key.Field;
				ProtocolParser.SkipKey(stream, key);
				break;
			}
			}
		}
		if (stream.Position != num)
		{
			throw new ProtocolBufferException("Read past max limit");
		}
		return instance;
	}

	public static SleepingBagRespawnRequest DeserializeLength(BufferStream stream, int length, SleepingBagRespawnRequest instance, bool isDelta)
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
				break;
			case 16:
				instance.sleepingBagId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				break;
			case 26:
				if (instance.secondaryData == null)
				{
					instance.secondaryData = PlayerSecondaryData.DeserializeLengthDelimited(stream);
				}
				else
				{
					PlayerSecondaryData.DeserializeLengthDelimited(stream, instance.secondaryData, isDelta);
				}
				break;
			default:
			{
				Key key = ProtocolParser.ReadKey((byte)num2, stream);
				_ = key.Field;
				ProtocolParser.SkipKey(stream, key);
				break;
			}
			}
		}
		if (stream.Position != num)
		{
			throw new ProtocolBufferException("Read past max limit");
		}
		return instance;
	}

	public static void SerializeDelta(BufferStream stream, SleepingBagRespawnRequest instance, SleepingBagRespawnRequest previous)
	{
		if (instance.userId != previous.userId)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.userId);
		}
		stream.WriteByte(16);
		ProtocolParser.WriteUInt64(stream, instance.sleepingBagId.Value);
		if (instance.secondaryData == null)
		{
			throw new ArgumentNullException("secondaryData", "Required by proto specification.");
		}
		stream.WriteByte(26);
		BufferStream.RangeHandle range = stream.GetRange(5);
		int position = stream.Position;
		PlayerSecondaryData.SerializeDelta(stream, instance.secondaryData, previous.secondaryData);
		int val = stream.Position - position;
		Span<byte> span = range.GetSpan();
		int num = ProtocolParser.WriteUInt32((uint)val, span, 0);
		if (num < 5)
		{
			span[num - 1] |= 128;
			while (num < 4)
			{
				span[num++] = 128;
			}
			span[4] = 0;
		}
	}

	public static void Serialize(BufferStream stream, SleepingBagRespawnRequest instance)
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
		if (instance.secondaryData == null)
		{
			throw new ArgumentNullException("secondaryData", "Required by proto specification.");
		}
		stream.WriteByte(26);
		BufferStream.RangeHandle range = stream.GetRange(5);
		int position = stream.Position;
		PlayerSecondaryData.Serialize(stream, instance.secondaryData);
		int val = stream.Position - position;
		Span<byte> span = range.GetSpan();
		int num = ProtocolParser.WriteUInt32((uint)val, span, 0);
		if (num < 5)
		{
			span[num - 1] |= 128;
			while (num < 4)
			{
				span[num++] = 128;
			}
			span[4] = 0;
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref sleepingBagId.Value);
		secondaryData?.InspectUids(action);
	}
}
