using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class Snowmobile : IDisposable, Pool.IPooled, IProto<Snowmobile>, IProto
{
	[NonSerialized]
	public float steerInput;

	[NonSerialized]
	public float driveWheelVel;

	[NonSerialized]
	public float throttleInput;

	[NonSerialized]
	public float brakeInput;

	[NonSerialized]
	public NetworkableId storageID;

	[NonSerialized]
	public NetworkableId fuelStorageID;

	[NonSerialized]
	public float fuelFraction;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(Snowmobile instance)
	{
		if (instance.ShouldPool)
		{
			instance.steerInput = 0f;
			instance.driveWheelVel = 0f;
			instance.throttleInput = 0f;
			instance.brakeInput = 0f;
			instance.storageID = default(NetworkableId);
			instance.fuelStorageID = default(NetworkableId);
			instance.fuelFraction = 0f;
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
			throw new Exception("Trying to dispose Snowmobile with ShouldPool set to false!");
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

	public void CopyTo(Snowmobile instance)
	{
		instance.steerInput = steerInput;
		instance.driveWheelVel = driveWheelVel;
		instance.throttleInput = throttleInput;
		instance.brakeInput = brakeInput;
		instance.storageID = storageID;
		instance.fuelStorageID = fuelStorageID;
		instance.fuelFraction = fuelFraction;
	}

	public Snowmobile Copy()
	{
		Snowmobile snowmobile = Pool.Get<Snowmobile>();
		CopyTo(snowmobile);
		return snowmobile;
	}

	public static Snowmobile Deserialize(BufferStream stream)
	{
		Snowmobile snowmobile = Pool.Get<Snowmobile>();
		Deserialize(stream, snowmobile, isDelta: false);
		return snowmobile;
	}

	public static Snowmobile DeserializeLengthDelimited(BufferStream stream)
	{
		Snowmobile snowmobile = Pool.Get<Snowmobile>();
		DeserializeLengthDelimited(stream, snowmobile, isDelta: false);
		return snowmobile;
	}

	public static Snowmobile DeserializeLength(BufferStream stream, int length)
	{
		Snowmobile snowmobile = Pool.Get<Snowmobile>();
		DeserializeLength(stream, length, snowmobile, isDelta: false);
		return snowmobile;
	}

	public static Snowmobile Deserialize(byte[] buffer)
	{
		Snowmobile snowmobile = Pool.Get<Snowmobile>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, snowmobile, isDelta: false);
		return snowmobile;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, Snowmobile previous)
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

	public static Snowmobile Deserialize(BufferStream stream, Snowmobile instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 13:
				instance.steerInput = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.driveWheelVel = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.throttleInput = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.brakeInput = ProtocolParser.ReadSingle(stream);
				continue;
			case 56:
				instance.storageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 64:
				instance.fuelStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 77:
				instance.fuelFraction = ProtocolParser.ReadSingle(stream);
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

	public static Snowmobile DeserializeLengthDelimited(BufferStream stream, Snowmobile instance, bool isDelta)
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
				instance.steerInput = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.driveWheelVel = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.throttleInput = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.brakeInput = ProtocolParser.ReadSingle(stream);
				continue;
			case 56:
				instance.storageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 64:
				instance.fuelStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 77:
				instance.fuelFraction = ProtocolParser.ReadSingle(stream);
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

	public static Snowmobile DeserializeLength(BufferStream stream, int length, Snowmobile instance, bool isDelta)
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
				instance.steerInput = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.driveWheelVel = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.throttleInput = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.brakeInput = ProtocolParser.ReadSingle(stream);
				continue;
			case 56:
				instance.storageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 64:
				instance.fuelStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 77:
				instance.fuelFraction = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, Snowmobile instance, Snowmobile previous)
	{
		if (instance.steerInput != previous.steerInput)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.steerInput);
		}
		if (instance.driveWheelVel != previous.driveWheelVel)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.driveWheelVel);
		}
		if (instance.throttleInput != previous.throttleInput)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.throttleInput);
		}
		if (instance.brakeInput != previous.brakeInput)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.brakeInput);
		}
		stream.WriteByte(56);
		ProtocolParser.WriteUInt64(stream, instance.storageID.Value);
		stream.WriteByte(64);
		ProtocolParser.WriteUInt64(stream, instance.fuelStorageID.Value);
		if (instance.fuelFraction != previous.fuelFraction)
		{
			stream.WriteByte(77);
			ProtocolParser.WriteSingle(stream, instance.fuelFraction);
		}
	}

	public static void Serialize(BufferStream stream, Snowmobile instance)
	{
		if (instance.steerInput != 0f)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.steerInput);
		}
		if (instance.driveWheelVel != 0f)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.driveWheelVel);
		}
		if (instance.throttleInput != 0f)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.throttleInput);
		}
		if (instance.brakeInput != 0f)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.brakeInput);
		}
		if (instance.storageID != default(NetworkableId))
		{
			stream.WriteByte(56);
			ProtocolParser.WriteUInt64(stream, instance.storageID.Value);
		}
		if (instance.fuelStorageID != default(NetworkableId))
		{
			stream.WriteByte(64);
			ProtocolParser.WriteUInt64(stream, instance.fuelStorageID.Value);
		}
		if (instance.fuelFraction != 0f)
		{
			stream.WriteByte(77);
			ProtocolParser.WriteSingle(stream, instance.fuelFraction);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref storageID.Value);
		action(UidType.NetworkableId, ref fuelStorageID.Value);
	}
}
