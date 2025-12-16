using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class TirednessAboveAIEventData : IDisposable, Pool.IPooled, IProto<TirednessAboveAIEventData>, IProto
{
	[NonSerialized]
	public float value;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(TirednessAboveAIEventData instance)
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
			throw new Exception("Trying to dispose TirednessAboveAIEventData with ShouldPool set to false!");
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

	public void CopyTo(TirednessAboveAIEventData instance)
	{
		instance.value = value;
	}

	public TirednessAboveAIEventData Copy()
	{
		TirednessAboveAIEventData tirednessAboveAIEventData = Pool.Get<TirednessAboveAIEventData>();
		CopyTo(tirednessAboveAIEventData);
		return tirednessAboveAIEventData;
	}

	public static TirednessAboveAIEventData Deserialize(BufferStream stream)
	{
		TirednessAboveAIEventData tirednessAboveAIEventData = Pool.Get<TirednessAboveAIEventData>();
		Deserialize(stream, tirednessAboveAIEventData, isDelta: false);
		return tirednessAboveAIEventData;
	}

	public static TirednessAboveAIEventData DeserializeLengthDelimited(BufferStream stream)
	{
		TirednessAboveAIEventData tirednessAboveAIEventData = Pool.Get<TirednessAboveAIEventData>();
		DeserializeLengthDelimited(stream, tirednessAboveAIEventData, isDelta: false);
		return tirednessAboveAIEventData;
	}

	public static TirednessAboveAIEventData DeserializeLength(BufferStream stream, int length)
	{
		TirednessAboveAIEventData tirednessAboveAIEventData = Pool.Get<TirednessAboveAIEventData>();
		DeserializeLength(stream, length, tirednessAboveAIEventData, isDelta: false);
		return tirednessAboveAIEventData;
	}

	public static TirednessAboveAIEventData Deserialize(byte[] buffer)
	{
		TirednessAboveAIEventData tirednessAboveAIEventData = Pool.Get<TirednessAboveAIEventData>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, tirednessAboveAIEventData, isDelta: false);
		return tirednessAboveAIEventData;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, TirednessAboveAIEventData previous)
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

	public static TirednessAboveAIEventData Deserialize(BufferStream stream, TirednessAboveAIEventData instance, bool isDelta)
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

	public static TirednessAboveAIEventData DeserializeLengthDelimited(BufferStream stream, TirednessAboveAIEventData instance, bool isDelta)
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

	public static TirednessAboveAIEventData DeserializeLength(BufferStream stream, int length, TirednessAboveAIEventData instance, bool isDelta)
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

	public static void SerializeDelta(BufferStream stream, TirednessAboveAIEventData instance, TirednessAboveAIEventData previous)
	{
		if (instance.value != previous.value)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.value);
		}
	}

	public static void Serialize(BufferStream stream, TirednessAboveAIEventData instance)
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
