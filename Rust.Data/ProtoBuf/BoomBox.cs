using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class BoomBox : IDisposable, Pool.IPooled, IProto<BoomBox>, IProto
{
	[NonSerialized]
	public string radioIp;

	[NonSerialized]
	public ulong assignedRadioBy;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(BoomBox instance)
	{
		if (instance.ShouldPool)
		{
			instance.radioIp = string.Empty;
			instance.assignedRadioBy = 0uL;
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
			throw new Exception("Trying to dispose BoomBox with ShouldPool set to false!");
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

	public void CopyTo(BoomBox instance)
	{
		instance.radioIp = radioIp;
		instance.assignedRadioBy = assignedRadioBy;
	}

	public BoomBox Copy()
	{
		BoomBox boomBox = Pool.Get<BoomBox>();
		CopyTo(boomBox);
		return boomBox;
	}

	public static BoomBox Deserialize(BufferStream stream)
	{
		BoomBox boomBox = Pool.Get<BoomBox>();
		Deserialize(stream, boomBox, isDelta: false);
		return boomBox;
	}

	public static BoomBox DeserializeLengthDelimited(BufferStream stream)
	{
		BoomBox boomBox = Pool.Get<BoomBox>();
		DeserializeLengthDelimited(stream, boomBox, isDelta: false);
		return boomBox;
	}

	public static BoomBox DeserializeLength(BufferStream stream, int length)
	{
		BoomBox boomBox = Pool.Get<BoomBox>();
		DeserializeLength(stream, length, boomBox, isDelta: false);
		return boomBox;
	}

	public static BoomBox Deserialize(byte[] buffer)
	{
		BoomBox boomBox = Pool.Get<BoomBox>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, boomBox, isDelta: false);
		return boomBox;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, BoomBox previous)
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

	public static BoomBox Deserialize(BufferStream stream, BoomBox instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 10:
				instance.radioIp = ProtocolParser.ReadString(stream);
				continue;
			case 16:
				instance.assignedRadioBy = ProtocolParser.ReadUInt64(stream);
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

	public static BoomBox DeserializeLengthDelimited(BufferStream stream, BoomBox instance, bool isDelta)
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
				instance.radioIp = ProtocolParser.ReadString(stream);
				continue;
			case 16:
				instance.assignedRadioBy = ProtocolParser.ReadUInt64(stream);
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

	public static BoomBox DeserializeLength(BufferStream stream, int length, BoomBox instance, bool isDelta)
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
				instance.radioIp = ProtocolParser.ReadString(stream);
				continue;
			case 16:
				instance.assignedRadioBy = ProtocolParser.ReadUInt64(stream);
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

	public static void SerializeDelta(BufferStream stream, BoomBox instance, BoomBox previous)
	{
		if (instance.radioIp != null && instance.radioIp != previous.radioIp)
		{
			stream.WriteByte(10);
			ProtocolParser.WriteString(stream, instance.radioIp);
		}
		if (instance.assignedRadioBy != previous.assignedRadioBy)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.assignedRadioBy);
		}
	}

	public static void Serialize(BufferStream stream, BoomBox instance)
	{
		if (instance.radioIp != null)
		{
			stream.WriteByte(10);
			ProtocolParser.WriteString(stream, instance.radioIp);
		}
		if (instance.assignedRadioBy != 0L)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.assignedRadioBy);
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
