using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class WaterWell : IDisposable, Pool.IPooled, IProto<WaterWell>, IProto
{
	[NonSerialized]
	public float pressure;

	[NonSerialized]
	public float waterLevel;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(WaterWell instance)
	{
		if (instance.ShouldPool)
		{
			instance.pressure = 0f;
			instance.waterLevel = 0f;
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
			throw new Exception("Trying to dispose WaterWell with ShouldPool set to false!");
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

	public void CopyTo(WaterWell instance)
	{
		instance.pressure = pressure;
		instance.waterLevel = waterLevel;
	}

	public WaterWell Copy()
	{
		WaterWell waterWell = Pool.Get<WaterWell>();
		CopyTo(waterWell);
		return waterWell;
	}

	public static WaterWell Deserialize(BufferStream stream)
	{
		WaterWell waterWell = Pool.Get<WaterWell>();
		Deserialize(stream, waterWell, isDelta: false);
		return waterWell;
	}

	public static WaterWell DeserializeLengthDelimited(BufferStream stream)
	{
		WaterWell waterWell = Pool.Get<WaterWell>();
		DeserializeLengthDelimited(stream, waterWell, isDelta: false);
		return waterWell;
	}

	public static WaterWell DeserializeLength(BufferStream stream, int length)
	{
		WaterWell waterWell = Pool.Get<WaterWell>();
		DeserializeLength(stream, length, waterWell, isDelta: false);
		return waterWell;
	}

	public static WaterWell Deserialize(byte[] buffer)
	{
		WaterWell waterWell = Pool.Get<WaterWell>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, waterWell, isDelta: false);
		return waterWell;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, WaterWell previous)
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

	public static WaterWell Deserialize(BufferStream stream, WaterWell instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 13:
				instance.pressure = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.waterLevel = ProtocolParser.ReadSingle(stream);
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

	public static WaterWell DeserializeLengthDelimited(BufferStream stream, WaterWell instance, bool isDelta)
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
				instance.pressure = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.waterLevel = ProtocolParser.ReadSingle(stream);
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

	public static WaterWell DeserializeLength(BufferStream stream, int length, WaterWell instance, bool isDelta)
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
				instance.pressure = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.waterLevel = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, WaterWell instance, WaterWell previous)
	{
		if (instance.pressure != previous.pressure)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.pressure);
		}
		if (instance.waterLevel != previous.waterLevel)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.waterLevel);
		}
	}

	public static void Serialize(BufferStream stream, WaterWell instance)
	{
		if (instance.pressure != 0f)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.pressure);
		}
		if (instance.waterLevel != 0f)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.waterLevel);
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
