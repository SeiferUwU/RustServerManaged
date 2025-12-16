using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class BaseBoat : IDisposable, Pool.IPooled, IProto<BaseBoat>, IProto
{
	[NonSerialized]
	public float shoreDriftTimerValue;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(BaseBoat instance)
	{
		if (instance.ShouldPool)
		{
			instance.shoreDriftTimerValue = 0f;
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
			throw new Exception("Trying to dispose BaseBoat with ShouldPool set to false!");
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

	public void CopyTo(BaseBoat instance)
	{
		instance.shoreDriftTimerValue = shoreDriftTimerValue;
	}

	public BaseBoat Copy()
	{
		BaseBoat baseBoat = Pool.Get<BaseBoat>();
		CopyTo(baseBoat);
		return baseBoat;
	}

	public static BaseBoat Deserialize(BufferStream stream)
	{
		BaseBoat baseBoat = Pool.Get<BaseBoat>();
		Deserialize(stream, baseBoat, isDelta: false);
		return baseBoat;
	}

	public static BaseBoat DeserializeLengthDelimited(BufferStream stream)
	{
		BaseBoat baseBoat = Pool.Get<BaseBoat>();
		DeserializeLengthDelimited(stream, baseBoat, isDelta: false);
		return baseBoat;
	}

	public static BaseBoat DeserializeLength(BufferStream stream, int length)
	{
		BaseBoat baseBoat = Pool.Get<BaseBoat>();
		DeserializeLength(stream, length, baseBoat, isDelta: false);
		return baseBoat;
	}

	public static BaseBoat Deserialize(byte[] buffer)
	{
		BaseBoat baseBoat = Pool.Get<BaseBoat>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, baseBoat, isDelta: false);
		return baseBoat;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, BaseBoat previous)
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

	public static BaseBoat Deserialize(BufferStream stream, BaseBoat instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 13:
				instance.shoreDriftTimerValue = ProtocolParser.ReadSingle(stream);
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

	public static BaseBoat DeserializeLengthDelimited(BufferStream stream, BaseBoat instance, bool isDelta)
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
			case 13:
				instance.shoreDriftTimerValue = ProtocolParser.ReadSingle(stream);
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

	public static BaseBoat DeserializeLength(BufferStream stream, int length, BaseBoat instance, bool isDelta)
	{
		long num = stream.Position + length;
		while (stream.Position < num)
		{
			int num2 = stream.ReadByte();
			switch (num2)
			{
			case -1:
				throw new EndOfStreamException();
			case 13:
				instance.shoreDriftTimerValue = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, BaseBoat instance, BaseBoat previous)
	{
		if (instance.shoreDriftTimerValue != previous.shoreDriftTimerValue)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.shoreDriftTimerValue);
		}
	}

	public static void Serialize(BufferStream stream, BaseBoat instance)
	{
		if (instance.shoreDriftTimerValue != 0f)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.shoreDriftTimerValue);
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
