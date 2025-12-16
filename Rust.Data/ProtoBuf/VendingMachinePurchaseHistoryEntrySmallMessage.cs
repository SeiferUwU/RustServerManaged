using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class VendingMachinePurchaseHistoryEntrySmallMessage : IDisposable, Pool.IPooled, IProto<VendingMachinePurchaseHistoryEntrySmallMessage>, IProto
{
	[NonSerialized]
	public int itemID;

	[NonSerialized]
	public int amount;

	[NonSerialized]
	public int priceID;

	[NonSerialized]
	public int price;

	[NonSerialized]
	public bool itemIsBp;

	[NonSerialized]
	public bool priceIsBp;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(VendingMachinePurchaseHistoryEntrySmallMessage instance)
	{
		if (instance.ShouldPool)
		{
			instance.itemID = 0;
			instance.amount = 0;
			instance.priceID = 0;
			instance.price = 0;
			instance.itemIsBp = false;
			instance.priceIsBp = false;
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
			throw new Exception("Trying to dispose VendingMachinePurchaseHistoryEntrySmallMessage with ShouldPool set to false!");
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

	public void CopyTo(VendingMachinePurchaseHistoryEntrySmallMessage instance)
	{
		instance.itemID = itemID;
		instance.amount = amount;
		instance.priceID = priceID;
		instance.price = price;
		instance.itemIsBp = itemIsBp;
		instance.priceIsBp = priceIsBp;
	}

	public VendingMachinePurchaseHistoryEntrySmallMessage Copy()
	{
		VendingMachinePurchaseHistoryEntrySmallMessage vendingMachinePurchaseHistoryEntrySmallMessage = Pool.Get<VendingMachinePurchaseHistoryEntrySmallMessage>();
		CopyTo(vendingMachinePurchaseHistoryEntrySmallMessage);
		return vendingMachinePurchaseHistoryEntrySmallMessage;
	}

	public static VendingMachinePurchaseHistoryEntrySmallMessage Deserialize(BufferStream stream)
	{
		VendingMachinePurchaseHistoryEntrySmallMessage vendingMachinePurchaseHistoryEntrySmallMessage = Pool.Get<VendingMachinePurchaseHistoryEntrySmallMessage>();
		Deserialize(stream, vendingMachinePurchaseHistoryEntrySmallMessage, isDelta: false);
		return vendingMachinePurchaseHistoryEntrySmallMessage;
	}

	public static VendingMachinePurchaseHistoryEntrySmallMessage DeserializeLengthDelimited(BufferStream stream)
	{
		VendingMachinePurchaseHistoryEntrySmallMessage vendingMachinePurchaseHistoryEntrySmallMessage = Pool.Get<VendingMachinePurchaseHistoryEntrySmallMessage>();
		DeserializeLengthDelimited(stream, vendingMachinePurchaseHistoryEntrySmallMessage, isDelta: false);
		return vendingMachinePurchaseHistoryEntrySmallMessage;
	}

	public static VendingMachinePurchaseHistoryEntrySmallMessage DeserializeLength(BufferStream stream, int length)
	{
		VendingMachinePurchaseHistoryEntrySmallMessage vendingMachinePurchaseHistoryEntrySmallMessage = Pool.Get<VendingMachinePurchaseHistoryEntrySmallMessage>();
		DeserializeLength(stream, length, vendingMachinePurchaseHistoryEntrySmallMessage, isDelta: false);
		return vendingMachinePurchaseHistoryEntrySmallMessage;
	}

	public static VendingMachinePurchaseHistoryEntrySmallMessage Deserialize(byte[] buffer)
	{
		VendingMachinePurchaseHistoryEntrySmallMessage vendingMachinePurchaseHistoryEntrySmallMessage = Pool.Get<VendingMachinePurchaseHistoryEntrySmallMessage>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, vendingMachinePurchaseHistoryEntrySmallMessage, isDelta: false);
		return vendingMachinePurchaseHistoryEntrySmallMessage;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, VendingMachinePurchaseHistoryEntrySmallMessage previous)
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

	public static VendingMachinePurchaseHistoryEntrySmallMessage Deserialize(BufferStream stream, VendingMachinePurchaseHistoryEntrySmallMessage instance, bool isDelta)
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
				instance.amount = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 24:
				instance.priceID = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 32:
				instance.price = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 40:
				instance.itemIsBp = ProtocolParser.ReadBool(stream);
				continue;
			case 48:
				instance.priceIsBp = ProtocolParser.ReadBool(stream);
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

	public static VendingMachinePurchaseHistoryEntrySmallMessage DeserializeLengthDelimited(BufferStream stream, VendingMachinePurchaseHistoryEntrySmallMessage instance, bool isDelta)
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
				instance.amount = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 24:
				instance.priceID = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 32:
				instance.price = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 40:
				instance.itemIsBp = ProtocolParser.ReadBool(stream);
				continue;
			case 48:
				instance.priceIsBp = ProtocolParser.ReadBool(stream);
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

	public static VendingMachinePurchaseHistoryEntrySmallMessage DeserializeLength(BufferStream stream, int length, VendingMachinePurchaseHistoryEntrySmallMessage instance, bool isDelta)
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
				instance.amount = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 24:
				instance.priceID = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 32:
				instance.price = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 40:
				instance.itemIsBp = ProtocolParser.ReadBool(stream);
				continue;
			case 48:
				instance.priceIsBp = ProtocolParser.ReadBool(stream);
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

	public static void SerializeDelta(BufferStream stream, VendingMachinePurchaseHistoryEntrySmallMessage instance, VendingMachinePurchaseHistoryEntrySmallMessage previous)
	{
		if (instance.itemID != previous.itemID)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.itemID);
		}
		if (instance.amount != previous.amount)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.amount);
		}
		if (instance.priceID != previous.priceID)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.priceID);
		}
		if (instance.price != previous.price)
		{
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.price);
		}
		stream.WriteByte(40);
		ProtocolParser.WriteBool(stream, instance.itemIsBp);
		stream.WriteByte(48);
		ProtocolParser.WriteBool(stream, instance.priceIsBp);
	}

	public static void Serialize(BufferStream stream, VendingMachinePurchaseHistoryEntrySmallMessage instance)
	{
		if (instance.itemID != 0)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.itemID);
		}
		if (instance.amount != 0)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.amount);
		}
		if (instance.priceID != 0)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.priceID);
		}
		if (instance.price != 0)
		{
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.price);
		}
		if (instance.itemIsBp)
		{
			stream.WriteByte(40);
			ProtocolParser.WriteBool(stream, instance.itemIsBp);
		}
		if (instance.priceIsBp)
		{
			stream.WriteByte(48);
			ProtocolParser.WriteBool(stream, instance.priceIsBp);
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
