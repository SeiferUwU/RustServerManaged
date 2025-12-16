using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class FarmableAnimal : IDisposable, Pool.IPooled, IProto<FarmableAnimal>, IProto
{
	[NonSerialized]
	public float hunger;

	[NonSerialized]
	public float thirst;

	[NonSerialized]
	public float love;

	[NonSerialized]
	public float sunlight;

	[NonSerialized]
	public string animalName;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(FarmableAnimal instance)
	{
		if (instance.ShouldPool)
		{
			instance.hunger = 0f;
			instance.thirst = 0f;
			instance.love = 0f;
			instance.sunlight = 0f;
			instance.animalName = string.Empty;
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
			throw new Exception("Trying to dispose FarmableAnimal with ShouldPool set to false!");
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

	public void CopyTo(FarmableAnimal instance)
	{
		instance.hunger = hunger;
		instance.thirst = thirst;
		instance.love = love;
		instance.sunlight = sunlight;
		instance.animalName = animalName;
	}

	public FarmableAnimal Copy()
	{
		FarmableAnimal farmableAnimal = Pool.Get<FarmableAnimal>();
		CopyTo(farmableAnimal);
		return farmableAnimal;
	}

	public static FarmableAnimal Deserialize(BufferStream stream)
	{
		FarmableAnimal farmableAnimal = Pool.Get<FarmableAnimal>();
		Deserialize(stream, farmableAnimal, isDelta: false);
		return farmableAnimal;
	}

	public static FarmableAnimal DeserializeLengthDelimited(BufferStream stream)
	{
		FarmableAnimal farmableAnimal = Pool.Get<FarmableAnimal>();
		DeserializeLengthDelimited(stream, farmableAnimal, isDelta: false);
		return farmableAnimal;
	}

	public static FarmableAnimal DeserializeLength(BufferStream stream, int length)
	{
		FarmableAnimal farmableAnimal = Pool.Get<FarmableAnimal>();
		DeserializeLength(stream, length, farmableAnimal, isDelta: false);
		return farmableAnimal;
	}

	public static FarmableAnimal Deserialize(byte[] buffer)
	{
		FarmableAnimal farmableAnimal = Pool.Get<FarmableAnimal>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, farmableAnimal, isDelta: false);
		return farmableAnimal;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, FarmableAnimal previous)
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

	public static FarmableAnimal Deserialize(BufferStream stream, FarmableAnimal instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 13:
				instance.hunger = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.thirst = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.love = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.sunlight = ProtocolParser.ReadSingle(stream);
				continue;
			case 42:
				instance.animalName = ProtocolParser.ReadString(stream);
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

	public static FarmableAnimal DeserializeLengthDelimited(BufferStream stream, FarmableAnimal instance, bool isDelta)
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
				instance.hunger = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.thirst = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.love = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.sunlight = ProtocolParser.ReadSingle(stream);
				continue;
			case 42:
				instance.animalName = ProtocolParser.ReadString(stream);
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

	public static FarmableAnimal DeserializeLength(BufferStream stream, int length, FarmableAnimal instance, bool isDelta)
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
				instance.hunger = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.thirst = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.love = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.sunlight = ProtocolParser.ReadSingle(stream);
				continue;
			case 42:
				instance.animalName = ProtocolParser.ReadString(stream);
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

	public static void SerializeDelta(BufferStream stream, FarmableAnimal instance, FarmableAnimal previous)
	{
		if (instance.hunger != previous.hunger)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.hunger);
		}
		if (instance.thirst != previous.thirst)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.thirst);
		}
		if (instance.love != previous.love)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.love);
		}
		if (instance.sunlight != previous.sunlight)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.sunlight);
		}
		if (instance.animalName != null && instance.animalName != previous.animalName)
		{
			stream.WriteByte(42);
			ProtocolParser.WriteString(stream, instance.animalName);
		}
	}

	public static void Serialize(BufferStream stream, FarmableAnimal instance)
	{
		if (instance.hunger != 0f)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.hunger);
		}
		if (instance.thirst != 0f)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.thirst);
		}
		if (instance.love != 0f)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.love);
		}
		if (instance.sunlight != 0f)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.sunlight);
		}
		if (instance.animalName != null)
		{
			stream.WriteByte(42);
			ProtocolParser.WriteString(stream, instance.animalName);
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
