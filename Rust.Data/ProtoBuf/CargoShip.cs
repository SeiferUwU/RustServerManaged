using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class CargoShip : IDisposable, Pool.IPooled, IProto<CargoShip>, IProto
{
	[NonSerialized]
	public int currentHarborApproachNode;

	[NonSerialized]
	public bool isDoingHarborApproach;

	[NonSerialized]
	public bool shouldLookAhead;

	[NonSerialized]
	public bool isEgressing;

	[NonSerialized]
	public uint layout;

	[NonSerialized]
	public List<ulong> playerIds;

	[NonSerialized]
	public int dockCount;

	[NonSerialized]
	public float lifetime;

	[NonSerialized]
	public int harborIndex;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(CargoShip instance)
	{
		if (instance.ShouldPool)
		{
			instance.currentHarborApproachNode = 0;
			instance.isDoingHarborApproach = false;
			instance.shouldLookAhead = false;
			instance.isEgressing = false;
			instance.layout = 0u;
			if (instance.playerIds != null)
			{
				List<ulong> obj = instance.playerIds;
				Pool.FreeUnmanaged(ref obj);
				instance.playerIds = obj;
			}
			instance.dockCount = 0;
			instance.lifetime = 0f;
			instance.harborIndex = 0;
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
			throw new Exception("Trying to dispose CargoShip with ShouldPool set to false!");
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

	public void CopyTo(CargoShip instance)
	{
		instance.currentHarborApproachNode = currentHarborApproachNode;
		instance.isDoingHarborApproach = isDoingHarborApproach;
		instance.shouldLookAhead = shouldLookAhead;
		instance.isEgressing = isEgressing;
		instance.layout = layout;
		if (playerIds != null)
		{
			instance.playerIds = Pool.Get<List<ulong>>();
			for (int i = 0; i < playerIds.Count; i++)
			{
				ulong item = playerIds[i];
				instance.playerIds.Add(item);
			}
		}
		else
		{
			instance.playerIds = null;
		}
		instance.dockCount = dockCount;
		instance.lifetime = lifetime;
		instance.harborIndex = harborIndex;
	}

	public CargoShip Copy()
	{
		CargoShip cargoShip = Pool.Get<CargoShip>();
		CopyTo(cargoShip);
		return cargoShip;
	}

	public static CargoShip Deserialize(BufferStream stream)
	{
		CargoShip cargoShip = Pool.Get<CargoShip>();
		Deserialize(stream, cargoShip, isDelta: false);
		return cargoShip;
	}

	public static CargoShip DeserializeLengthDelimited(BufferStream stream)
	{
		CargoShip cargoShip = Pool.Get<CargoShip>();
		DeserializeLengthDelimited(stream, cargoShip, isDelta: false);
		return cargoShip;
	}

	public static CargoShip DeserializeLength(BufferStream stream, int length)
	{
		CargoShip cargoShip = Pool.Get<CargoShip>();
		DeserializeLength(stream, length, cargoShip, isDelta: false);
		return cargoShip;
	}

	public static CargoShip Deserialize(byte[] buffer)
	{
		CargoShip cargoShip = Pool.Get<CargoShip>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, cargoShip, isDelta: false);
		return cargoShip;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, CargoShip previous)
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

	public static CargoShip Deserialize(BufferStream stream, CargoShip instance, bool isDelta)
	{
		if (!isDelta && instance.playerIds == null)
		{
			instance.playerIds = Pool.Get<List<ulong>>();
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.currentHarborApproachNode = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.isDoingHarborApproach = ProtocolParser.ReadBool(stream);
				continue;
			case 32:
				instance.shouldLookAhead = ProtocolParser.ReadBool(stream);
				continue;
			case 40:
				instance.isEgressing = ProtocolParser.ReadBool(stream);
				continue;
			case 48:
				instance.layout = ProtocolParser.ReadUInt32(stream);
				continue;
			case 56:
				instance.playerIds.Add(ProtocolParser.ReadUInt64(stream));
				continue;
			case 64:
				instance.dockCount = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 77:
				instance.lifetime = ProtocolParser.ReadSingle(stream);
				continue;
			case 80:
				instance.harborIndex = (int)ProtocolParser.ReadUInt64(stream);
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

	public static CargoShip DeserializeLengthDelimited(BufferStream stream, CargoShip instance, bool isDelta)
	{
		if (!isDelta && instance.playerIds == null)
		{
			instance.playerIds = Pool.Get<List<ulong>>();
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
			case 8:
				instance.currentHarborApproachNode = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.isDoingHarborApproach = ProtocolParser.ReadBool(stream);
				continue;
			case 32:
				instance.shouldLookAhead = ProtocolParser.ReadBool(stream);
				continue;
			case 40:
				instance.isEgressing = ProtocolParser.ReadBool(stream);
				continue;
			case 48:
				instance.layout = ProtocolParser.ReadUInt32(stream);
				continue;
			case 56:
				instance.playerIds.Add(ProtocolParser.ReadUInt64(stream));
				continue;
			case 64:
				instance.dockCount = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 77:
				instance.lifetime = ProtocolParser.ReadSingle(stream);
				continue;
			case 80:
				instance.harborIndex = (int)ProtocolParser.ReadUInt64(stream);
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

	public static CargoShip DeserializeLength(BufferStream stream, int length, CargoShip instance, bool isDelta)
	{
		if (!isDelta && instance.playerIds == null)
		{
			instance.playerIds = Pool.Get<List<ulong>>();
		}
		long num = stream.Position + length;
		while (stream.Position < num)
		{
			int num2 = stream.ReadByte();
			switch (num2)
			{
			case -1:
				throw new EndOfStreamException();
			case 8:
				instance.currentHarborApproachNode = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.isDoingHarborApproach = ProtocolParser.ReadBool(stream);
				continue;
			case 32:
				instance.shouldLookAhead = ProtocolParser.ReadBool(stream);
				continue;
			case 40:
				instance.isEgressing = ProtocolParser.ReadBool(stream);
				continue;
			case 48:
				instance.layout = ProtocolParser.ReadUInt32(stream);
				continue;
			case 56:
				instance.playerIds.Add(ProtocolParser.ReadUInt64(stream));
				continue;
			case 64:
				instance.dockCount = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 77:
				instance.lifetime = ProtocolParser.ReadSingle(stream);
				continue;
			case 80:
				instance.harborIndex = (int)ProtocolParser.ReadUInt64(stream);
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

	public static void SerializeDelta(BufferStream stream, CargoShip instance, CargoShip previous)
	{
		if (instance.currentHarborApproachNode != previous.currentHarborApproachNode)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.currentHarborApproachNode);
		}
		stream.WriteByte(16);
		ProtocolParser.WriteBool(stream, instance.isDoingHarborApproach);
		stream.WriteByte(32);
		ProtocolParser.WriteBool(stream, instance.shouldLookAhead);
		stream.WriteByte(40);
		ProtocolParser.WriteBool(stream, instance.isEgressing);
		if (instance.layout != previous.layout)
		{
			stream.WriteByte(48);
			ProtocolParser.WriteUInt32(stream, instance.layout);
		}
		if (instance.playerIds != null)
		{
			for (int i = 0; i < instance.playerIds.Count; i++)
			{
				ulong val = instance.playerIds[i];
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, val);
			}
		}
		if (instance.dockCount != previous.dockCount)
		{
			stream.WriteByte(64);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.dockCount);
		}
		if (instance.lifetime != previous.lifetime)
		{
			stream.WriteByte(77);
			ProtocolParser.WriteSingle(stream, instance.lifetime);
		}
		if (instance.harborIndex != previous.harborIndex)
		{
			stream.WriteByte(80);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.harborIndex);
		}
	}

	public static void Serialize(BufferStream stream, CargoShip instance)
	{
		if (instance.currentHarborApproachNode != 0)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.currentHarborApproachNode);
		}
		if (instance.isDoingHarborApproach)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteBool(stream, instance.isDoingHarborApproach);
		}
		if (instance.shouldLookAhead)
		{
			stream.WriteByte(32);
			ProtocolParser.WriteBool(stream, instance.shouldLookAhead);
		}
		if (instance.isEgressing)
		{
			stream.WriteByte(40);
			ProtocolParser.WriteBool(stream, instance.isEgressing);
		}
		if (instance.layout != 0)
		{
			stream.WriteByte(48);
			ProtocolParser.WriteUInt32(stream, instance.layout);
		}
		if (instance.playerIds != null)
		{
			for (int i = 0; i < instance.playerIds.Count; i++)
			{
				ulong val = instance.playerIds[i];
				stream.WriteByte(56);
				ProtocolParser.WriteUInt64(stream, val);
			}
		}
		if (instance.dockCount != 0)
		{
			stream.WriteByte(64);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.dockCount);
		}
		if (instance.lifetime != 0f)
		{
			stream.WriteByte(77);
			ProtocolParser.WriteSingle(stream, instance.lifetime);
		}
		if (instance.harborIndex != 0)
		{
			stream.WriteByte(80);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.harborIndex);
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
