using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;
using UnityEngine;

namespace ProtoBuf;

public class SpinnerWheel : IDisposable, Pool.IPooled, IProto<SpinnerWheel>, IProto
{
	[NonSerialized]
	public Vector3 spin;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(SpinnerWheel instance)
	{
		if (instance.ShouldPool)
		{
			instance.spin = default(Vector3);
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
			throw new Exception("Trying to dispose SpinnerWheel with ShouldPool set to false!");
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

	public void CopyTo(SpinnerWheel instance)
	{
		instance.spin = spin;
	}

	public SpinnerWheel Copy()
	{
		SpinnerWheel spinnerWheel = Pool.Get<SpinnerWheel>();
		CopyTo(spinnerWheel);
		return spinnerWheel;
	}

	public static SpinnerWheel Deserialize(BufferStream stream)
	{
		SpinnerWheel spinnerWheel = Pool.Get<SpinnerWheel>();
		Deserialize(stream, spinnerWheel, isDelta: false);
		return spinnerWheel;
	}

	public static SpinnerWheel DeserializeLengthDelimited(BufferStream stream)
	{
		SpinnerWheel spinnerWheel = Pool.Get<SpinnerWheel>();
		DeserializeLengthDelimited(stream, spinnerWheel, isDelta: false);
		return spinnerWheel;
	}

	public static SpinnerWheel DeserializeLength(BufferStream stream, int length)
	{
		SpinnerWheel spinnerWheel = Pool.Get<SpinnerWheel>();
		DeserializeLength(stream, length, spinnerWheel, isDelta: false);
		return spinnerWheel;
	}

	public static SpinnerWheel Deserialize(byte[] buffer)
	{
		SpinnerWheel spinnerWheel = Pool.Get<SpinnerWheel>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, spinnerWheel, isDelta: false);
		return spinnerWheel;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, SpinnerWheel previous)
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

	public static SpinnerWheel Deserialize(BufferStream stream, SpinnerWheel instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 10:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.spin, isDelta);
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

	public static SpinnerWheel DeserializeLengthDelimited(BufferStream stream, SpinnerWheel instance, bool isDelta)
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
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.spin, isDelta);
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

	public static SpinnerWheel DeserializeLength(BufferStream stream, int length, SpinnerWheel instance, bool isDelta)
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
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.spin, isDelta);
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

	public static void SerializeDelta(BufferStream stream, SpinnerWheel instance, SpinnerWheel previous)
	{
		if (instance.spin != previous.spin)
		{
			stream.WriteByte(10);
			BufferStream.RangeHandle range = stream.GetRange(1);
			int position = stream.Position;
			Vector3Serialized.SerializeDelta(stream, instance.spin, previous.spin);
			int num = stream.Position - position;
			if (num > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field spin (UnityEngine.Vector3)");
			}
			Span<byte> span = range.GetSpan();
			ProtocolParser.WriteUInt32((uint)num, span, 0);
		}
	}

	public static void Serialize(BufferStream stream, SpinnerWheel instance)
	{
		if (instance.spin != default(Vector3))
		{
			stream.WriteByte(10);
			BufferStream.RangeHandle range = stream.GetRange(1);
			int position = stream.Position;
			Vector3Serialized.Serialize(stream, instance.spin);
			int num = stream.Position - position;
			if (num > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field spin (UnityEngine.Vector3)");
			}
			Span<byte> span = range.GetSpan();
			ProtocolParser.WriteUInt32((uint)num, span, 0);
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
