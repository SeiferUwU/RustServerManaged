using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class WeaponRackItem : IDisposable, Pool.IPooled, IProto<WeaponRackItem>, IProto
{
	[NonSerialized]
	public int itemID;

	[NonSerialized]
	public ulong skinid;

	[NonSerialized]
	public int gridSlotIndex;

	[NonSerialized]
	public int inventorySlot;

	[NonSerialized]
	public int ammoCount;

	[NonSerialized]
	public int ammoMax;

	[NonSerialized]
	public int ammoID;

	[NonSerialized]
	public float condition;

	[NonSerialized]
	public int rotation;

	[NonSerialized]
	public int ammoTypes;

	[NonSerialized]
	public float reloadTime;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(WeaponRackItem instance)
	{
		if (instance.ShouldPool)
		{
			instance.itemID = 0;
			instance.skinid = 0uL;
			instance.gridSlotIndex = 0;
			instance.inventorySlot = 0;
			instance.ammoCount = 0;
			instance.ammoMax = 0;
			instance.ammoID = 0;
			instance.condition = 0f;
			instance.rotation = 0;
			instance.ammoTypes = 0;
			instance.reloadTime = 0f;
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
			throw new Exception("Trying to dispose WeaponRackItem with ShouldPool set to false!");
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

	public void CopyTo(WeaponRackItem instance)
	{
		instance.itemID = itemID;
		instance.skinid = skinid;
		instance.gridSlotIndex = gridSlotIndex;
		instance.inventorySlot = inventorySlot;
		instance.ammoCount = ammoCount;
		instance.ammoMax = ammoMax;
		instance.ammoID = ammoID;
		instance.condition = condition;
		instance.rotation = rotation;
		instance.ammoTypes = ammoTypes;
		instance.reloadTime = reloadTime;
	}

	public WeaponRackItem Copy()
	{
		WeaponRackItem weaponRackItem = Pool.Get<WeaponRackItem>();
		CopyTo(weaponRackItem);
		return weaponRackItem;
	}

	public static WeaponRackItem Deserialize(BufferStream stream)
	{
		WeaponRackItem weaponRackItem = Pool.Get<WeaponRackItem>();
		Deserialize(stream, weaponRackItem, isDelta: false);
		return weaponRackItem;
	}

	public static WeaponRackItem DeserializeLengthDelimited(BufferStream stream)
	{
		WeaponRackItem weaponRackItem = Pool.Get<WeaponRackItem>();
		DeserializeLengthDelimited(stream, weaponRackItem, isDelta: false);
		return weaponRackItem;
	}

	public static WeaponRackItem DeserializeLength(BufferStream stream, int length)
	{
		WeaponRackItem weaponRackItem = Pool.Get<WeaponRackItem>();
		DeserializeLength(stream, length, weaponRackItem, isDelta: false);
		return weaponRackItem;
	}

	public static WeaponRackItem Deserialize(byte[] buffer)
	{
		WeaponRackItem weaponRackItem = Pool.Get<WeaponRackItem>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, weaponRackItem, isDelta: false);
		return weaponRackItem;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, WeaponRackItem previous)
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

	public static WeaponRackItem Deserialize(BufferStream stream, WeaponRackItem instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.itemID = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.skinid = ProtocolParser.ReadUInt64(stream);
				continue;
			case 24:
				instance.gridSlotIndex = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 32:
				instance.inventorySlot = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 40:
				instance.ammoCount = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 48:
				instance.ammoMax = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 56:
				instance.ammoID = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 69:
				instance.condition = ProtocolParser.ReadSingle(stream);
				continue;
			case 72:
				instance.rotation = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 80:
				instance.ammoTypes = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 93:
				instance.reloadTime = ProtocolParser.ReadSingle(stream);
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

	public static WeaponRackItem DeserializeLengthDelimited(BufferStream stream, WeaponRackItem instance, bool isDelta)
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
				instance.itemID = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.skinid = ProtocolParser.ReadUInt64(stream);
				continue;
			case 24:
				instance.gridSlotIndex = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 32:
				instance.inventorySlot = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 40:
				instance.ammoCount = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 48:
				instance.ammoMax = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 56:
				instance.ammoID = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 69:
				instance.condition = ProtocolParser.ReadSingle(stream);
				continue;
			case 72:
				instance.rotation = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 80:
				instance.ammoTypes = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 93:
				instance.reloadTime = ProtocolParser.ReadSingle(stream);
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

	public static WeaponRackItem DeserializeLength(BufferStream stream, int length, WeaponRackItem instance, bool isDelta)
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
				instance.itemID = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.skinid = ProtocolParser.ReadUInt64(stream);
				continue;
			case 24:
				instance.gridSlotIndex = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 32:
				instance.inventorySlot = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 40:
				instance.ammoCount = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 48:
				instance.ammoMax = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 56:
				instance.ammoID = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 69:
				instance.condition = ProtocolParser.ReadSingle(stream);
				continue;
			case 72:
				instance.rotation = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 80:
				instance.ammoTypes = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 93:
				instance.reloadTime = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, WeaponRackItem instance, WeaponRackItem previous)
	{
		if (instance.itemID != previous.itemID)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.itemID);
		}
		if (instance.skinid != previous.skinid)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.skinid);
		}
		if (instance.gridSlotIndex != previous.gridSlotIndex)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.gridSlotIndex);
		}
		if (instance.inventorySlot != previous.inventorySlot)
		{
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.inventorySlot);
		}
		if (instance.ammoCount != previous.ammoCount)
		{
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ammoCount);
		}
		if (instance.ammoMax != previous.ammoMax)
		{
			stream.WriteByte(48);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ammoMax);
		}
		if (instance.ammoID != previous.ammoID)
		{
			stream.WriteByte(56);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ammoID);
		}
		if (instance.condition != previous.condition)
		{
			stream.WriteByte(69);
			ProtocolParser.WriteSingle(stream, instance.condition);
		}
		if (instance.rotation != previous.rotation)
		{
			stream.WriteByte(72);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.rotation);
		}
		if (instance.ammoTypes != previous.ammoTypes)
		{
			stream.WriteByte(80);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ammoTypes);
		}
		if (instance.reloadTime != previous.reloadTime)
		{
			stream.WriteByte(93);
			ProtocolParser.WriteSingle(stream, instance.reloadTime);
		}
	}

	public static void Serialize(BufferStream stream, WeaponRackItem instance)
	{
		if (instance.itemID != 0)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.itemID);
		}
		if (instance.skinid != 0L)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.skinid);
		}
		if (instance.gridSlotIndex != 0)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.gridSlotIndex);
		}
		if (instance.inventorySlot != 0)
		{
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.inventorySlot);
		}
		if (instance.ammoCount != 0)
		{
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ammoCount);
		}
		if (instance.ammoMax != 0)
		{
			stream.WriteByte(48);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ammoMax);
		}
		if (instance.ammoID != 0)
		{
			stream.WriteByte(56);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ammoID);
		}
		if (instance.condition != 0f)
		{
			stream.WriteByte(69);
			ProtocolParser.WriteSingle(stream, instance.condition);
		}
		if (instance.rotation != 0)
		{
			stream.WriteByte(72);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.rotation);
		}
		if (instance.ammoTypes != 0)
		{
			stream.WriteByte(80);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.ammoTypes);
		}
		if (instance.reloadTime != 0f)
		{
			stream.WriteByte(93);
			ProtocolParser.WriteSingle(stream, instance.reloadTime);
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
