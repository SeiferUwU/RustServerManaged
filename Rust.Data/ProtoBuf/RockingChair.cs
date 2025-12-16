using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;
using UnityEngine;

namespace ProtoBuf;

public class RockingChair : IDisposable, Pool.IPooled, IProto<RockingChair>, IProto
{
	[NonSerialized]
	public Vector3 initEuler;

	[NonSerialized]
	public float initY;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(RockingChair instance)
	{
		if (instance.ShouldPool)
		{
			instance.initEuler = default(Vector3);
			instance.initY = 0f;
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
			throw new Exception("Trying to dispose RockingChair with ShouldPool set to false!");
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

	public void CopyTo(RockingChair instance)
	{
		instance.initEuler = initEuler;
		instance.initY = initY;
	}

	public RockingChair Copy()
	{
		RockingChair rockingChair = Pool.Get<RockingChair>();
		CopyTo(rockingChair);
		return rockingChair;
	}

	public static RockingChair Deserialize(BufferStream stream)
	{
		RockingChair rockingChair = Pool.Get<RockingChair>();
		Deserialize(stream, rockingChair, isDelta: false);
		return rockingChair;
	}

	public static RockingChair DeserializeLengthDelimited(BufferStream stream)
	{
		RockingChair rockingChair = Pool.Get<RockingChair>();
		DeserializeLengthDelimited(stream, rockingChair, isDelta: false);
		return rockingChair;
	}

	public static RockingChair DeserializeLength(BufferStream stream, int length)
	{
		RockingChair rockingChair = Pool.Get<RockingChair>();
		DeserializeLength(stream, length, rockingChair, isDelta: false);
		return rockingChair;
	}

	public static RockingChair Deserialize(byte[] buffer)
	{
		RockingChair rockingChair = Pool.Get<RockingChair>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, rockingChair, isDelta: false);
		return rockingChair;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, RockingChair previous)
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

	public static RockingChair Deserialize(BufferStream stream, RockingChair instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 10:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.initEuler, isDelta);
				continue;
			case 21:
				instance.initY = ProtocolParser.ReadSingle(stream);
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

	public static RockingChair DeserializeLengthDelimited(BufferStream stream, RockingChair instance, bool isDelta)
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
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.initEuler, isDelta);
				continue;
			case 21:
				instance.initY = ProtocolParser.ReadSingle(stream);
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

	public static RockingChair DeserializeLength(BufferStream stream, int length, RockingChair instance, bool isDelta)
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
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.initEuler, isDelta);
				continue;
			case 21:
				instance.initY = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, RockingChair instance, RockingChair previous)
	{
		if (instance.initEuler != previous.initEuler)
		{
			stream.WriteByte(10);
			BufferStream.RangeHandle range = stream.GetRange(1);
			int position = stream.Position;
			Vector3Serialized.SerializeDelta(stream, instance.initEuler, previous.initEuler);
			int num = stream.Position - position;
			if (num > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field initEuler (UnityEngine.Vector3)");
			}
			Span<byte> span = range.GetSpan();
			ProtocolParser.WriteUInt32((uint)num, span, 0);
		}
		if (instance.initY != previous.initY)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.initY);
		}
	}

	public static void Serialize(BufferStream stream, RockingChair instance)
	{
		if (instance.initEuler != default(Vector3))
		{
			stream.WriteByte(10);
			BufferStream.RangeHandle range = stream.GetRange(1);
			int position = stream.Position;
			Vector3Serialized.Serialize(stream, instance.initEuler);
			int num = stream.Position - position;
			if (num > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field initEuler (UnityEngine.Vector3)");
			}
			Span<byte> span = range.GetSpan();
			ProtocolParser.WriteUInt32((uint)num, span, 0);
		}
		if (instance.initY != 0f)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.initY);
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
