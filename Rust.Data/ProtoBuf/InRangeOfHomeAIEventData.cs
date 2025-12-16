using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class InRangeOfHomeAIEventData : IDisposable, Pool.IPooled, IProto<InRangeOfHomeAIEventData>, IProto
{
	[NonSerialized]
	public float range;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(InRangeOfHomeAIEventData instance)
	{
		if (instance.ShouldPool)
		{
			instance.range = 0f;
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
			throw new Exception("Trying to dispose InRangeOfHomeAIEventData with ShouldPool set to false!");
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

	public void CopyTo(InRangeOfHomeAIEventData instance)
	{
		instance.range = range;
	}

	public InRangeOfHomeAIEventData Copy()
	{
		InRangeOfHomeAIEventData inRangeOfHomeAIEventData = Pool.Get<InRangeOfHomeAIEventData>();
		CopyTo(inRangeOfHomeAIEventData);
		return inRangeOfHomeAIEventData;
	}

	public static InRangeOfHomeAIEventData Deserialize(BufferStream stream)
	{
		InRangeOfHomeAIEventData inRangeOfHomeAIEventData = Pool.Get<InRangeOfHomeAIEventData>();
		Deserialize(stream, inRangeOfHomeAIEventData, isDelta: false);
		return inRangeOfHomeAIEventData;
	}

	public static InRangeOfHomeAIEventData DeserializeLengthDelimited(BufferStream stream)
	{
		InRangeOfHomeAIEventData inRangeOfHomeAIEventData = Pool.Get<InRangeOfHomeAIEventData>();
		DeserializeLengthDelimited(stream, inRangeOfHomeAIEventData, isDelta: false);
		return inRangeOfHomeAIEventData;
	}

	public static InRangeOfHomeAIEventData DeserializeLength(BufferStream stream, int length)
	{
		InRangeOfHomeAIEventData inRangeOfHomeAIEventData = Pool.Get<InRangeOfHomeAIEventData>();
		DeserializeLength(stream, length, inRangeOfHomeAIEventData, isDelta: false);
		return inRangeOfHomeAIEventData;
	}

	public static InRangeOfHomeAIEventData Deserialize(byte[] buffer)
	{
		InRangeOfHomeAIEventData inRangeOfHomeAIEventData = Pool.Get<InRangeOfHomeAIEventData>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, inRangeOfHomeAIEventData, isDelta: false);
		return inRangeOfHomeAIEventData;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, InRangeOfHomeAIEventData previous)
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

	public static InRangeOfHomeAIEventData Deserialize(BufferStream stream, InRangeOfHomeAIEventData instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 13:
				instance.range = ProtocolParser.ReadSingle(stream);
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

	public static InRangeOfHomeAIEventData DeserializeLengthDelimited(BufferStream stream, InRangeOfHomeAIEventData instance, bool isDelta)
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
				instance.range = ProtocolParser.ReadSingle(stream);
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

	public static InRangeOfHomeAIEventData DeserializeLength(BufferStream stream, int length, InRangeOfHomeAIEventData instance, bool isDelta)
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
				instance.range = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, InRangeOfHomeAIEventData instance, InRangeOfHomeAIEventData previous)
	{
		if (instance.range != previous.range)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.range);
		}
	}

	public static void Serialize(BufferStream stream, InRangeOfHomeAIEventData instance)
	{
		if (instance.range != 0f)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.range);
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
