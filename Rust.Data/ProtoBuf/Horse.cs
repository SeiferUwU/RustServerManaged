using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class Horse : IDisposable, Pool.IPooled, IProto<Horse>, IProto
{
	[NonSerialized]
	public float stamina;

	[NonSerialized]
	public float maxStamina;

	[NonSerialized]
	public int gait;

	[NonSerialized]
	public float equipmentSpeedMod;

	[NonSerialized]
	public int breedIndex;

	[NonSerialized]
	public NetworkableId towEntityId;

	[NonSerialized]
	public ItemContainer equipmentContainer;

	[NonSerialized]
	public ItemContainer storageContainer;

	[NonSerialized]
	public int numStorageSlots;

	[NonSerialized]
	public NetworkableId playerLeadingId;

	[NonSerialized]
	public HorseModifiers modifiers;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(Horse instance)
	{
		if (instance.ShouldPool)
		{
			instance.stamina = 0f;
			instance.maxStamina = 0f;
			instance.gait = 0;
			instance.equipmentSpeedMod = 0f;
			instance.breedIndex = 0;
			instance.towEntityId = default(NetworkableId);
			if (instance.equipmentContainer != null)
			{
				instance.equipmentContainer.ResetToPool();
				instance.equipmentContainer = null;
			}
			if (instance.storageContainer != null)
			{
				instance.storageContainer.ResetToPool();
				instance.storageContainer = null;
			}
			instance.numStorageSlots = 0;
			instance.playerLeadingId = default(NetworkableId);
			if (instance.modifiers != null)
			{
				instance.modifiers.ResetToPool();
				instance.modifiers = null;
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
			throw new Exception("Trying to dispose Horse with ShouldPool set to false!");
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

	public void CopyTo(Horse instance)
	{
		instance.stamina = stamina;
		instance.maxStamina = maxStamina;
		instance.gait = gait;
		instance.equipmentSpeedMod = equipmentSpeedMod;
		instance.breedIndex = breedIndex;
		instance.towEntityId = towEntityId;
		if (equipmentContainer != null)
		{
			if (instance.equipmentContainer == null)
			{
				instance.equipmentContainer = equipmentContainer.Copy();
			}
			else
			{
				equipmentContainer.CopyTo(instance.equipmentContainer);
			}
		}
		else
		{
			instance.equipmentContainer = null;
		}
		if (storageContainer != null)
		{
			if (instance.storageContainer == null)
			{
				instance.storageContainer = storageContainer.Copy();
			}
			else
			{
				storageContainer.CopyTo(instance.storageContainer);
			}
		}
		else
		{
			instance.storageContainer = null;
		}
		instance.numStorageSlots = numStorageSlots;
		instance.playerLeadingId = playerLeadingId;
		if (modifiers != null)
		{
			if (instance.modifiers == null)
			{
				instance.modifiers = modifiers.Copy();
			}
			else
			{
				modifiers.CopyTo(instance.modifiers);
			}
		}
		else
		{
			instance.modifiers = null;
		}
	}

	public Horse Copy()
	{
		Horse horse = Pool.Get<Horse>();
		CopyTo(horse);
		return horse;
	}

	public static Horse Deserialize(BufferStream stream)
	{
		Horse horse = Pool.Get<Horse>();
		Deserialize(stream, horse, isDelta: false);
		return horse;
	}

	public static Horse DeserializeLengthDelimited(BufferStream stream)
	{
		Horse horse = Pool.Get<Horse>();
		DeserializeLengthDelimited(stream, horse, isDelta: false);
		return horse;
	}

	public static Horse DeserializeLength(BufferStream stream, int length)
	{
		Horse horse = Pool.Get<Horse>();
		DeserializeLength(stream, length, horse, isDelta: false);
		return horse;
	}

	public static Horse Deserialize(byte[] buffer)
	{
		Horse horse = Pool.Get<Horse>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, horse, isDelta: false);
		return horse;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, Horse previous)
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

	public static Horse Deserialize(BufferStream stream, Horse instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 13:
				instance.stamina = ProtocolParser.ReadSingle(stream);
				break;
			case 21:
				instance.maxStamina = ProtocolParser.ReadSingle(stream);
				break;
			case 24:
				instance.gait = (int)ProtocolParser.ReadUInt64(stream);
				break;
			case 37:
				instance.equipmentSpeedMod = ProtocolParser.ReadSingle(stream);
				break;
			case 40:
				instance.breedIndex = (int)ProtocolParser.ReadUInt64(stream);
				break;
			case 48:
				instance.towEntityId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				break;
			case 58:
				if (instance.equipmentContainer == null)
				{
					instance.equipmentContainer = ItemContainer.DeserializeLengthDelimited(stream);
				}
				else
				{
					ItemContainer.DeserializeLengthDelimited(stream, instance.equipmentContainer, isDelta);
				}
				break;
			case 66:
				if (instance.storageContainer == null)
				{
					instance.storageContainer = ItemContainer.DeserializeLengthDelimited(stream);
				}
				else
				{
					ItemContainer.DeserializeLengthDelimited(stream, instance.storageContainer, isDelta);
				}
				break;
			case 72:
				instance.numStorageSlots = (int)ProtocolParser.ReadUInt64(stream);
				break;
			case 80:
				instance.playerLeadingId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				break;
			case 90:
				if (instance.modifiers == null)
				{
					instance.modifiers = HorseModifiers.DeserializeLengthDelimited(stream);
				}
				else
				{
					HorseModifiers.DeserializeLengthDelimited(stream, instance.modifiers, isDelta);
				}
				break;
			default:
			{
				Key key = ProtocolParser.ReadKey((byte)num, stream);
				_ = key.Field;
				ProtocolParser.SkipKey(stream, key);
				break;
			}
			case -1:
			case 0:
				return instance;
			}
		}
	}

	public static Horse DeserializeLengthDelimited(BufferStream stream, Horse instance, bool isDelta)
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
				instance.stamina = ProtocolParser.ReadSingle(stream);
				break;
			case 21:
				instance.maxStamina = ProtocolParser.ReadSingle(stream);
				break;
			case 24:
				instance.gait = (int)ProtocolParser.ReadUInt64(stream);
				break;
			case 37:
				instance.equipmentSpeedMod = ProtocolParser.ReadSingle(stream);
				break;
			case 40:
				instance.breedIndex = (int)ProtocolParser.ReadUInt64(stream);
				break;
			case 48:
				instance.towEntityId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				break;
			case 58:
				if (instance.equipmentContainer == null)
				{
					instance.equipmentContainer = ItemContainer.DeserializeLengthDelimited(stream);
				}
				else
				{
					ItemContainer.DeserializeLengthDelimited(stream, instance.equipmentContainer, isDelta);
				}
				break;
			case 66:
				if (instance.storageContainer == null)
				{
					instance.storageContainer = ItemContainer.DeserializeLengthDelimited(stream);
				}
				else
				{
					ItemContainer.DeserializeLengthDelimited(stream, instance.storageContainer, isDelta);
				}
				break;
			case 72:
				instance.numStorageSlots = (int)ProtocolParser.ReadUInt64(stream);
				break;
			case 80:
				instance.playerLeadingId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				break;
			case 90:
				if (instance.modifiers == null)
				{
					instance.modifiers = HorseModifiers.DeserializeLengthDelimited(stream);
				}
				else
				{
					HorseModifiers.DeserializeLengthDelimited(stream, instance.modifiers, isDelta);
				}
				break;
			default:
			{
				Key key = ProtocolParser.ReadKey((byte)num2, stream);
				_ = key.Field;
				ProtocolParser.SkipKey(stream, key);
				break;
			}
			}
		}
		if (stream.Position != num)
		{
			throw new ProtocolBufferException("Read past max limit");
		}
		return instance;
	}

	public static Horse DeserializeLength(BufferStream stream, int length, Horse instance, bool isDelta)
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
				instance.stamina = ProtocolParser.ReadSingle(stream);
				break;
			case 21:
				instance.maxStamina = ProtocolParser.ReadSingle(stream);
				break;
			case 24:
				instance.gait = (int)ProtocolParser.ReadUInt64(stream);
				break;
			case 37:
				instance.equipmentSpeedMod = ProtocolParser.ReadSingle(stream);
				break;
			case 40:
				instance.breedIndex = (int)ProtocolParser.ReadUInt64(stream);
				break;
			case 48:
				instance.towEntityId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				break;
			case 58:
				if (instance.equipmentContainer == null)
				{
					instance.equipmentContainer = ItemContainer.DeserializeLengthDelimited(stream);
				}
				else
				{
					ItemContainer.DeserializeLengthDelimited(stream, instance.equipmentContainer, isDelta);
				}
				break;
			case 66:
				if (instance.storageContainer == null)
				{
					instance.storageContainer = ItemContainer.DeserializeLengthDelimited(stream);
				}
				else
				{
					ItemContainer.DeserializeLengthDelimited(stream, instance.storageContainer, isDelta);
				}
				break;
			case 72:
				instance.numStorageSlots = (int)ProtocolParser.ReadUInt64(stream);
				break;
			case 80:
				instance.playerLeadingId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				break;
			case 90:
				if (instance.modifiers == null)
				{
					instance.modifiers = HorseModifiers.DeserializeLengthDelimited(stream);
				}
				else
				{
					HorseModifiers.DeserializeLengthDelimited(stream, instance.modifiers, isDelta);
				}
				break;
			default:
			{
				Key key = ProtocolParser.ReadKey((byte)num2, stream);
				_ = key.Field;
				ProtocolParser.SkipKey(stream, key);
				break;
			}
			}
		}
		if (stream.Position != num)
		{
			throw new ProtocolBufferException("Read past max limit");
		}
		return instance;
	}

	public static void SerializeDelta(BufferStream stream, Horse instance, Horse previous)
	{
		if (instance.stamina != previous.stamina)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.stamina);
		}
		if (instance.maxStamina != previous.maxStamina)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.maxStamina);
		}
		if (instance.gait != previous.gait)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.gait);
		}
		if (instance.equipmentSpeedMod != previous.equipmentSpeedMod)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.equipmentSpeedMod);
		}
		if (instance.breedIndex != previous.breedIndex)
		{
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.breedIndex);
		}
		stream.WriteByte(48);
		ProtocolParser.WriteUInt64(stream, instance.towEntityId.Value);
		if (instance.equipmentContainer != null)
		{
			stream.WriteByte(58);
			BufferStream.RangeHandle range = stream.GetRange(5);
			int position = stream.Position;
			ItemContainer.SerializeDelta(stream, instance.equipmentContainer, previous.equipmentContainer);
			int val = stream.Position - position;
			Span<byte> span = range.GetSpan();
			int num = ProtocolParser.WriteUInt32((uint)val, span, 0);
			if (num < 5)
			{
				span[num - 1] |= 128;
				while (num < 4)
				{
					span[num++] = 128;
				}
				span[4] = 0;
			}
		}
		if (instance.storageContainer != null)
		{
			stream.WriteByte(66);
			BufferStream.RangeHandle range2 = stream.GetRange(5);
			int position2 = stream.Position;
			ItemContainer.SerializeDelta(stream, instance.storageContainer, previous.storageContainer);
			int val2 = stream.Position - position2;
			Span<byte> span2 = range2.GetSpan();
			int num2 = ProtocolParser.WriteUInt32((uint)val2, span2, 0);
			if (num2 < 5)
			{
				span2[num2 - 1] |= 128;
				while (num2 < 4)
				{
					span2[num2++] = 128;
				}
				span2[4] = 0;
			}
		}
		if (instance.numStorageSlots != previous.numStorageSlots)
		{
			stream.WriteByte(72);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.numStorageSlots);
		}
		stream.WriteByte(80);
		ProtocolParser.WriteUInt64(stream, instance.playerLeadingId.Value);
		if (instance.modifiers == null)
		{
			return;
		}
		stream.WriteByte(90);
		BufferStream.RangeHandle range3 = stream.GetRange(3);
		int position3 = stream.Position;
		HorseModifiers.SerializeDelta(stream, instance.modifiers, previous.modifiers);
		int num3 = stream.Position - position3;
		if (num3 > 2097151)
		{
			throw new InvalidOperationException("Not enough space was reserved for the length prefix of field modifiers (ProtoBuf.HorseModifiers)");
		}
		Span<byte> span3 = range3.GetSpan();
		int num4 = ProtocolParser.WriteUInt32((uint)num3, span3, 0);
		if (num4 < 3)
		{
			span3[num4 - 1] |= 128;
			while (num4 < 2)
			{
				span3[num4++] = 128;
			}
			span3[2] = 0;
		}
	}

	public static void Serialize(BufferStream stream, Horse instance)
	{
		if (instance.stamina != 0f)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.stamina);
		}
		if (instance.maxStamina != 0f)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.maxStamina);
		}
		if (instance.gait != 0)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.gait);
		}
		if (instance.equipmentSpeedMod != 0f)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.equipmentSpeedMod);
		}
		if (instance.breedIndex != 0)
		{
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.breedIndex);
		}
		if (instance.towEntityId != default(NetworkableId))
		{
			stream.WriteByte(48);
			ProtocolParser.WriteUInt64(stream, instance.towEntityId.Value);
		}
		if (instance.equipmentContainer != null)
		{
			stream.WriteByte(58);
			BufferStream.RangeHandle range = stream.GetRange(5);
			int position = stream.Position;
			ItemContainer.Serialize(stream, instance.equipmentContainer);
			int val = stream.Position - position;
			Span<byte> span = range.GetSpan();
			int num = ProtocolParser.WriteUInt32((uint)val, span, 0);
			if (num < 5)
			{
				span[num - 1] |= 128;
				while (num < 4)
				{
					span[num++] = 128;
				}
				span[4] = 0;
			}
		}
		if (instance.storageContainer != null)
		{
			stream.WriteByte(66);
			BufferStream.RangeHandle range2 = stream.GetRange(5);
			int position2 = stream.Position;
			ItemContainer.Serialize(stream, instance.storageContainer);
			int val2 = stream.Position - position2;
			Span<byte> span2 = range2.GetSpan();
			int num2 = ProtocolParser.WriteUInt32((uint)val2, span2, 0);
			if (num2 < 5)
			{
				span2[num2 - 1] |= 128;
				while (num2 < 4)
				{
					span2[num2++] = 128;
				}
				span2[4] = 0;
			}
		}
		if (instance.numStorageSlots != 0)
		{
			stream.WriteByte(72);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.numStorageSlots);
		}
		if (instance.playerLeadingId != default(NetworkableId))
		{
			stream.WriteByte(80);
			ProtocolParser.WriteUInt64(stream, instance.playerLeadingId.Value);
		}
		if (instance.modifiers == null)
		{
			return;
		}
		stream.WriteByte(90);
		BufferStream.RangeHandle range3 = stream.GetRange(3);
		int position3 = stream.Position;
		HorseModifiers.Serialize(stream, instance.modifiers);
		int num3 = stream.Position - position3;
		if (num3 > 2097151)
		{
			throw new InvalidOperationException("Not enough space was reserved for the length prefix of field modifiers (ProtoBuf.HorseModifiers)");
		}
		Span<byte> span3 = range3.GetSpan();
		int num4 = ProtocolParser.WriteUInt32((uint)num3, span3, 0);
		if (num4 < 3)
		{
			span3[num4 - 1] |= 128;
			while (num4 < 2)
			{
				span3[num4++] = 128;
			}
			span3[2] = 0;
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref towEntityId.Value);
		equipmentContainer?.InspectUids(action);
		storageContainer?.InspectUids(action);
		action(UidType.NetworkableId, ref playerLeadingId.Value);
		modifiers?.InspectUids(action);
	}
}
