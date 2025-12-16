using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class AppEntityInfo : IDisposable, Pool.IPooled, IProto<AppEntityInfo>, IProto
{
	[NonSerialized]
	public AppEntityType type;

	[NonSerialized]
	public AppEntityPayload payload;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(AppEntityInfo instance)
	{
		if (instance.ShouldPool)
		{
			instance.type = (AppEntityType)0;
			if (instance.payload != null)
			{
				instance.payload.ResetToPool();
				instance.payload = null;
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
			throw new Exception("Trying to dispose AppEntityInfo with ShouldPool set to false!");
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

	public void CopyTo(AppEntityInfo instance)
	{
		instance.type = type;
		if (payload != null)
		{
			if (instance.payload == null)
			{
				instance.payload = payload.Copy();
			}
			else
			{
				payload.CopyTo(instance.payload);
			}
		}
		else
		{
			instance.payload = null;
		}
	}

	public AppEntityInfo Copy()
	{
		AppEntityInfo appEntityInfo = Pool.Get<AppEntityInfo>();
		CopyTo(appEntityInfo);
		return appEntityInfo;
	}

	public static AppEntityInfo Deserialize(BufferStream stream)
	{
		AppEntityInfo appEntityInfo = Pool.Get<AppEntityInfo>();
		Deserialize(stream, appEntityInfo, isDelta: false);
		return appEntityInfo;
	}

	public static AppEntityInfo DeserializeLengthDelimited(BufferStream stream)
	{
		AppEntityInfo appEntityInfo = Pool.Get<AppEntityInfo>();
		DeserializeLengthDelimited(stream, appEntityInfo, isDelta: false);
		return appEntityInfo;
	}

	public static AppEntityInfo DeserializeLength(BufferStream stream, int length)
	{
		AppEntityInfo appEntityInfo = Pool.Get<AppEntityInfo>();
		DeserializeLength(stream, length, appEntityInfo, isDelta: false);
		return appEntityInfo;
	}

	public static AppEntityInfo Deserialize(byte[] buffer)
	{
		AppEntityInfo appEntityInfo = Pool.Get<AppEntityInfo>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, appEntityInfo, isDelta: false);
		return appEntityInfo;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, AppEntityInfo previous)
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

	public static AppEntityInfo Deserialize(BufferStream stream, AppEntityInfo instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.type = (AppEntityType)ProtocolParser.ReadUInt64(stream);
				break;
			case 26:
				if (instance.payload == null)
				{
					instance.payload = AppEntityPayload.DeserializeLengthDelimited(stream);
				}
				else
				{
					AppEntityPayload.DeserializeLengthDelimited(stream, instance.payload, isDelta);
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

	public static AppEntityInfo DeserializeLengthDelimited(BufferStream stream, AppEntityInfo instance, bool isDelta)
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
				instance.type = (AppEntityType)ProtocolParser.ReadUInt64(stream);
				break;
			case 26:
				if (instance.payload == null)
				{
					instance.payload = AppEntityPayload.DeserializeLengthDelimited(stream);
				}
				else
				{
					AppEntityPayload.DeserializeLengthDelimited(stream, instance.payload, isDelta);
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

	public static AppEntityInfo DeserializeLength(BufferStream stream, int length, AppEntityInfo instance, bool isDelta)
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
				instance.type = (AppEntityType)ProtocolParser.ReadUInt64(stream);
				break;
			case 26:
				if (instance.payload == null)
				{
					instance.payload = AppEntityPayload.DeserializeLengthDelimited(stream);
				}
				else
				{
					AppEntityPayload.DeserializeLengthDelimited(stream, instance.payload, isDelta);
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

	public static void SerializeDelta(BufferStream stream, AppEntityInfo instance, AppEntityInfo previous)
	{
		stream.WriteByte(8);
		ProtocolParser.WriteUInt64(stream, (ulong)instance.type);
		if (instance.payload == null)
		{
			throw new ArgumentNullException("payload", "Required by proto specification.");
		}
		stream.WriteByte(26);
		BufferStream.RangeHandle range = stream.GetRange(3);
		int position = stream.Position;
		AppEntityPayload.SerializeDelta(stream, instance.payload, previous.payload);
		int num = stream.Position - position;
		if (num > 2097151)
		{
			throw new InvalidOperationException("Not enough space was reserved for the length prefix of field payload (ProtoBuf.AppEntityPayload)");
		}
		Span<byte> span = range.GetSpan();
		int num2 = ProtocolParser.WriteUInt32((uint)num, span, 0);
		if (num2 < 3)
		{
			span[num2 - 1] |= 128;
			while (num2 < 2)
			{
				span[num2++] = 128;
			}
			span[2] = 0;
		}
	}

	public static void Serialize(BufferStream stream, AppEntityInfo instance)
	{
		stream.WriteByte(8);
		ProtocolParser.WriteUInt64(stream, (ulong)instance.type);
		if (instance.payload == null)
		{
			throw new ArgumentNullException("payload", "Required by proto specification.");
		}
		stream.WriteByte(26);
		BufferStream.RangeHandle range = stream.GetRange(3);
		int position = stream.Position;
		AppEntityPayload.Serialize(stream, instance.payload);
		int num = stream.Position - position;
		if (num > 2097151)
		{
			throw new InvalidOperationException("Not enough space was reserved for the length prefix of field payload (ProtoBuf.AppEntityPayload)");
		}
		Span<byte> span = range.GetSpan();
		int num2 = ProtocolParser.WriteUInt32((uint)num, span, 0);
		if (num2 < 3)
		{
			span[num2 - 1] |= 128;
			while (num2 < 2)
			{
				span[num2++] = 128;
			}
			span[2] = 0;
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		payload?.InspectUids(action);
	}
}
