using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class ModularCar : IDisposable, Pool.IPooled, IProto<ModularCar>, IProto
{
	[NonSerialized]
	public float steerAngle;

	[NonSerialized]
	public float driveWheelVel;

	[NonSerialized]
	public float throttleInput;

	[NonSerialized]
	public float brakeInput;

	[NonSerialized]
	public NetworkableId fuelStorageID;

	[NonSerialized]
	public float fuelFraction;

	[NonSerialized]
	public bool hasLock;

	[NonSerialized]
	public string lockCode;

	[NonSerialized]
	public List<ulong> whitelistUsers;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(ModularCar instance)
	{
		if (instance.ShouldPool)
		{
			instance.steerAngle = 0f;
			instance.driveWheelVel = 0f;
			instance.throttleInput = 0f;
			instance.brakeInput = 0f;
			instance.fuelStorageID = default(NetworkableId);
			instance.fuelFraction = 0f;
			instance.hasLock = false;
			instance.lockCode = string.Empty;
			if (instance.whitelistUsers != null)
			{
				List<ulong> obj = instance.whitelistUsers;
				Pool.FreeUnmanaged(ref obj);
				instance.whitelistUsers = obj;
			}
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
			throw new Exception("Trying to dispose ModularCar with ShouldPool set to false!");
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

	public void CopyTo(ModularCar instance)
	{
		instance.steerAngle = steerAngle;
		instance.driveWheelVel = driveWheelVel;
		instance.throttleInput = throttleInput;
		instance.brakeInput = brakeInput;
		instance.fuelStorageID = fuelStorageID;
		instance.fuelFraction = fuelFraction;
		instance.hasLock = hasLock;
		instance.lockCode = lockCode;
		if (whitelistUsers != null)
		{
			instance.whitelistUsers = Pool.Get<List<ulong>>();
			for (int i = 0; i < whitelistUsers.Count; i++)
			{
				ulong item = whitelistUsers[i];
				instance.whitelistUsers.Add(item);
			}
		}
		else
		{
			instance.whitelistUsers = null;
		}
	}

	public ModularCar Copy()
	{
		ModularCar modularCar = Pool.Get<ModularCar>();
		CopyTo(modularCar);
		return modularCar;
	}

	public static ModularCar Deserialize(BufferStream stream)
	{
		ModularCar modularCar = Pool.Get<ModularCar>();
		Deserialize(stream, modularCar, isDelta: false);
		return modularCar;
	}

	public static ModularCar DeserializeLengthDelimited(BufferStream stream)
	{
		ModularCar modularCar = Pool.Get<ModularCar>();
		DeserializeLengthDelimited(stream, modularCar, isDelta: false);
		return modularCar;
	}

	public static ModularCar DeserializeLength(BufferStream stream, int length)
	{
		ModularCar modularCar = Pool.Get<ModularCar>();
		DeserializeLength(stream, length, modularCar, isDelta: false);
		return modularCar;
	}

	public static ModularCar Deserialize(byte[] buffer)
	{
		ModularCar modularCar = Pool.Get<ModularCar>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, modularCar, isDelta: false);
		return modularCar;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, ModularCar previous)
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

	public static ModularCar Deserialize(BufferStream stream, ModularCar instance, bool isDelta)
	{
		if (!isDelta && instance.whitelistUsers == null)
		{
			instance.whitelistUsers = Pool.Get<List<ulong>>();
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 13:
				instance.steerAngle = ProtocolParser.ReadSingle(stream);
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
			case 40:
				instance.fuelStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 53:
				instance.fuelFraction = ProtocolParser.ReadSingle(stream);
				continue;
			case 56:
				instance.hasLock = ProtocolParser.ReadBool(stream);
				continue;
			case 66:
				instance.lockCode = ProtocolParser.ReadString(stream);
				continue;
			case 72:
				instance.whitelistUsers.Add(ProtocolParser.ReadUInt64(stream));
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

	public static ModularCar DeserializeLengthDelimited(BufferStream stream, ModularCar instance, bool isDelta)
	{
		if (!isDelta && instance.whitelistUsers == null)
		{
			instance.whitelistUsers = Pool.Get<List<ulong>>();
		}
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
				instance.steerAngle = ProtocolParser.ReadSingle(stream);
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
			case 40:
				instance.fuelStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 53:
				instance.fuelFraction = ProtocolParser.ReadSingle(stream);
				continue;
			case 56:
				instance.hasLock = ProtocolParser.ReadBool(stream);
				continue;
			case 66:
				instance.lockCode = ProtocolParser.ReadString(stream);
				continue;
			case 72:
				instance.whitelistUsers.Add(ProtocolParser.ReadUInt64(stream));
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

	public static ModularCar DeserializeLength(BufferStream stream, int length, ModularCar instance, bool isDelta)
	{
		if (!isDelta && instance.whitelistUsers == null)
		{
			instance.whitelistUsers = Pool.Get<List<ulong>>();
		}
		long num = stream.Position + length;
		while (stream.Position < num)
		{
			int num2 = stream.ReadByte();
			switch (num2)
			{
			case -1:
				throw new EndOfStreamException();
			case 13:
				instance.steerAngle = ProtocolParser.ReadSingle(stream);
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
			case 40:
				instance.fuelStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 53:
				instance.fuelFraction = ProtocolParser.ReadSingle(stream);
				continue;
			case 56:
				instance.hasLock = ProtocolParser.ReadBool(stream);
				continue;
			case 66:
				instance.lockCode = ProtocolParser.ReadString(stream);
				continue;
			case 72:
				instance.whitelistUsers.Add(ProtocolParser.ReadUInt64(stream));
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

	public static void SerializeDelta(BufferStream stream, ModularCar instance, ModularCar previous)
	{
		if (instance.steerAngle != previous.steerAngle)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.steerAngle);
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
		stream.WriteByte(40);
		ProtocolParser.WriteUInt64(stream, instance.fuelStorageID.Value);
		if (instance.fuelFraction != previous.fuelFraction)
		{
			stream.WriteByte(53);
			ProtocolParser.WriteSingle(stream, instance.fuelFraction);
		}
		stream.WriteByte(56);
		ProtocolParser.WriteBool(stream, instance.hasLock);
		if (instance.lockCode != null && instance.lockCode != previous.lockCode)
		{
			stream.WriteByte(66);
			ProtocolParser.WriteString(stream, instance.lockCode);
		}
		if (instance.whitelistUsers != null)
		{
			for (int i = 0; i < instance.whitelistUsers.Count; i++)
			{
				ulong val = instance.whitelistUsers[i];
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, val);
			}
		}
	}

	public static void Serialize(BufferStream stream, ModularCar instance)
	{
		if (instance.steerAngle != 0f)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.steerAngle);
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
		if (instance.fuelStorageID != default(NetworkableId))
		{
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, instance.fuelStorageID.Value);
		}
		if (instance.fuelFraction != 0f)
		{
			stream.WriteByte(53);
			ProtocolParser.WriteSingle(stream, instance.fuelFraction);
		}
		if (instance.hasLock)
		{
			stream.WriteByte(56);
			ProtocolParser.WriteBool(stream, instance.hasLock);
		}
		if (instance.lockCode != null)
		{
			stream.WriteByte(66);
			ProtocolParser.WriteString(stream, instance.lockCode);
		}
		if (instance.whitelistUsers != null)
		{
			for (int i = 0; i < instance.whitelistUsers.Count; i++)
			{
				ulong val = instance.whitelistUsers[i];
				stream.WriteByte(72);
				ProtocolParser.WriteUInt64(stream, val);
			}
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref fuelStorageID.Value);
	}
}
