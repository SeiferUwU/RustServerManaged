using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class ModularVehicle : IDisposable, Pool.IPooled, IProto<ModularVehicle>, IProto
{
	[NonSerialized]
	public bool editable;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(ModularVehicle instance)
	{
		if (instance.ShouldPool)
		{
			instance.editable = false;
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
			throw new Exception("Trying to dispose ModularVehicle with ShouldPool set to false!");
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

	public void CopyTo(ModularVehicle instance)
	{
		instance.editable = editable;
	}

	public ModularVehicle Copy()
	{
		ModularVehicle modularVehicle = Pool.Get<ModularVehicle>();
		CopyTo(modularVehicle);
		return modularVehicle;
	}

	public static ModularVehicle Deserialize(BufferStream stream)
	{
		ModularVehicle modularVehicle = Pool.Get<ModularVehicle>();
		Deserialize(stream, modularVehicle, isDelta: false);
		return modularVehicle;
	}

	public static ModularVehicle DeserializeLengthDelimited(BufferStream stream)
	{
		ModularVehicle modularVehicle = Pool.Get<ModularVehicle>();
		DeserializeLengthDelimited(stream, modularVehicle, isDelta: false);
		return modularVehicle;
	}

	public static ModularVehicle DeserializeLength(BufferStream stream, int length)
	{
		ModularVehicle modularVehicle = Pool.Get<ModularVehicle>();
		DeserializeLength(stream, length, modularVehicle, isDelta: false);
		return modularVehicle;
	}

	public static ModularVehicle Deserialize(byte[] buffer)
	{
		ModularVehicle modularVehicle = Pool.Get<ModularVehicle>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, modularVehicle, isDelta: false);
		return modularVehicle;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, ModularVehicle previous)
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

	public static ModularVehicle Deserialize(BufferStream stream, ModularVehicle instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.editable = ProtocolParser.ReadBool(stream);
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

	public static ModularVehicle DeserializeLengthDelimited(BufferStream stream, ModularVehicle instance, bool isDelta)
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
			case 8:
				instance.editable = ProtocolParser.ReadBool(stream);
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

	public static ModularVehicle DeserializeLength(BufferStream stream, int length, ModularVehicle instance, bool isDelta)
	{
		long num = stream.Position + length;
		while (stream.Position < num)
		{
			int num2 = stream.ReadByte();
			switch (num2)
			{
			case -1:
				throw new EndOfStreamException();
			case 8:
				instance.editable = ProtocolParser.ReadBool(stream);
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

	public static void SerializeDelta(BufferStream stream, ModularVehicle instance, ModularVehicle previous)
	{
		stream.WriteByte(8);
		ProtocolParser.WriteBool(stream, instance.editable);
	}

	public static void Serialize(BufferStream stream, ModularVehicle instance)
	{
		if (instance.editable)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteBool(stream, instance.editable);
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
