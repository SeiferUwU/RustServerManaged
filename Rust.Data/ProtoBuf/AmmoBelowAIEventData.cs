using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class AmmoBelowAIEventData : IDisposable, Pool.IPooled, IProto<AmmoBelowAIEventData>, IProto
{
	[NonSerialized]
	public float value;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(AmmoBelowAIEventData instance)
	{
		if (instance.ShouldPool)
		{
			instance.value = 0f;
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
			throw new Exception("Trying to dispose AmmoBelowAIEventData with ShouldPool set to false!");
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

	public void CopyTo(AmmoBelowAIEventData instance)
	{
		instance.value = value;
	}

	public AmmoBelowAIEventData Copy()
	{
		AmmoBelowAIEventData ammoBelowAIEventData = Pool.Get<AmmoBelowAIEventData>();
		CopyTo(ammoBelowAIEventData);
		return ammoBelowAIEventData;
	}

	public static AmmoBelowAIEventData Deserialize(BufferStream stream)
	{
		AmmoBelowAIEventData ammoBelowAIEventData = Pool.Get<AmmoBelowAIEventData>();
		Deserialize(stream, ammoBelowAIEventData, isDelta: false);
		return ammoBelowAIEventData;
	}

	public static AmmoBelowAIEventData DeserializeLengthDelimited(BufferStream stream)
	{
		AmmoBelowAIEventData ammoBelowAIEventData = Pool.Get<AmmoBelowAIEventData>();
		DeserializeLengthDelimited(stream, ammoBelowAIEventData, isDelta: false);
		return ammoBelowAIEventData;
	}

	public static AmmoBelowAIEventData DeserializeLength(BufferStream stream, int length)
	{
		AmmoBelowAIEventData ammoBelowAIEventData = Pool.Get<AmmoBelowAIEventData>();
		DeserializeLength(stream, length, ammoBelowAIEventData, isDelta: false);
		return ammoBelowAIEventData;
	}

	public static AmmoBelowAIEventData Deserialize(byte[] buffer)
	{
		AmmoBelowAIEventData ammoBelowAIEventData = Pool.Get<AmmoBelowAIEventData>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, ammoBelowAIEventData, isDelta: false);
		return ammoBelowAIEventData;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, AmmoBelowAIEventData previous)
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

	public static AmmoBelowAIEventData Deserialize(BufferStream stream, AmmoBelowAIEventData instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 13:
				instance.value = ProtocolParser.ReadSingle(stream);
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

	public static AmmoBelowAIEventData DeserializeLengthDelimited(BufferStream stream, AmmoBelowAIEventData instance, bool isDelta)
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
				instance.value = ProtocolParser.ReadSingle(stream);
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

	public static AmmoBelowAIEventData DeserializeLength(BufferStream stream, int length, AmmoBelowAIEventData instance, bool isDelta)
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
				instance.value = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, AmmoBelowAIEventData instance, AmmoBelowAIEventData previous)
	{
		if (instance.value != previous.value)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.value);
		}
	}

	public static void Serialize(BufferStream stream, AmmoBelowAIEventData instance)
	{
		if (instance.value != 0f)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.value);
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
