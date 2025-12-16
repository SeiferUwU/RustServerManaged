using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class ItemAmountList : IDisposable, Pool.IPooled, IProto<ItemAmountList>, IProto
{
	[NonSerialized]
	public List<int> itemID;

	[NonSerialized]
	public List<float> amount;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(ItemAmountList instance)
	{
		if (instance.ShouldPool)
		{
			if (instance.itemID != null)
			{
				List<int> obj = instance.itemID;
				Pool.FreeUnmanaged(ref obj);
				instance.itemID = obj;
			}
			if (instance.amount != null)
			{
				List<float> obj2 = instance.amount;
				Pool.FreeUnmanaged(ref obj2);
				instance.amount = obj2;
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
			throw new Exception("Trying to dispose ItemAmountList with ShouldPool set to false!");
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

	public void CopyTo(ItemAmountList instance)
	{
		if (itemID != null)
		{
			instance.itemID = Pool.Get<List<int>>();
			for (int i = 0; i < itemID.Count; i++)
			{
				int item = itemID[i];
				instance.itemID.Add(item);
			}
		}
		else
		{
			instance.itemID = null;
		}
		if (amount != null)
		{
			instance.amount = Pool.Get<List<float>>();
			for (int j = 0; j < amount.Count; j++)
			{
				float item2 = amount[j];
				instance.amount.Add(item2);
			}
		}
		else
		{
			instance.amount = null;
		}
	}

	public ItemAmountList Copy()
	{
		ItemAmountList itemAmountList = Pool.Get<ItemAmountList>();
		CopyTo(itemAmountList);
		return itemAmountList;
	}

	public static ItemAmountList Deserialize(BufferStream stream)
	{
		ItemAmountList itemAmountList = Pool.Get<ItemAmountList>();
		Deserialize(stream, itemAmountList, isDelta: false);
		return itemAmountList;
	}

	public static ItemAmountList DeserializeLengthDelimited(BufferStream stream)
	{
		ItemAmountList itemAmountList = Pool.Get<ItemAmountList>();
		DeserializeLengthDelimited(stream, itemAmountList, isDelta: false);
		return itemAmountList;
	}

	public static ItemAmountList DeserializeLength(BufferStream stream, int length)
	{
		ItemAmountList itemAmountList = Pool.Get<ItemAmountList>();
		DeserializeLength(stream, length, itemAmountList, isDelta: false);
		return itemAmountList;
	}

	public static ItemAmountList Deserialize(byte[] buffer)
	{
		ItemAmountList itemAmountList = Pool.Get<ItemAmountList>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, itemAmountList, isDelta: false);
		return itemAmountList;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, ItemAmountList previous)
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

	public static ItemAmountList Deserialize(BufferStream stream, ItemAmountList instance, bool isDelta)
	{
		if (!isDelta)
		{
			if (instance.itemID == null)
			{
				instance.itemID = Pool.Get<List<int>>();
			}
			if (instance.amount == null)
			{
				instance.amount = Pool.Get<List<float>>();
			}
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.itemID.Add((int)ProtocolParser.ReadUInt64(stream));
				continue;
			case 21:
				instance.amount.Add(ProtocolParser.ReadSingle(stream));
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

	public static ItemAmountList DeserializeLengthDelimited(BufferStream stream, ItemAmountList instance, bool isDelta)
	{
		if (!isDelta)
		{
			if (instance.itemID == null)
			{
				instance.itemID = Pool.Get<List<int>>();
			}
			if (instance.amount == null)
			{
				instance.amount = Pool.Get<List<float>>();
			}
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
				instance.itemID.Add((int)ProtocolParser.ReadUInt64(stream));
				continue;
			case 21:
				instance.amount.Add(ProtocolParser.ReadSingle(stream));
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

	public static ItemAmountList DeserializeLength(BufferStream stream, int length, ItemAmountList instance, bool isDelta)
	{
		if (!isDelta)
		{
			if (instance.itemID == null)
			{
				instance.itemID = Pool.Get<List<int>>();
			}
			if (instance.amount == null)
			{
				instance.amount = Pool.Get<List<float>>();
			}
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
				instance.itemID.Add((int)ProtocolParser.ReadUInt64(stream));
				continue;
			case 21:
				instance.amount.Add(ProtocolParser.ReadSingle(stream));
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

	public static void SerializeDelta(BufferStream stream, ItemAmountList instance, ItemAmountList previous)
	{
		if (instance.itemID != null)
		{
			for (int i = 0; i < instance.itemID.Count; i++)
			{
				int num = instance.itemID[i];
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)num);
			}
		}
		if (instance.amount != null)
		{
			for (int j = 0; j < instance.amount.Count; j++)
			{
				float f = instance.amount[j];
				stream.WriteByte(21);
				ProtocolParser.WriteSingle(stream, f);
			}
		}
	}

	public static void Serialize(BufferStream stream, ItemAmountList instance)
	{
		if (instance.itemID != null)
		{
			for (int i = 0; i < instance.itemID.Count; i++)
			{
				int num = instance.itemID[i];
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, (ulong)num);
			}
		}
		if (instance.amount != null)
		{
			for (int j = 0; j < instance.amount.Count; j++)
			{
				float f = instance.amount[j];
				stream.WriteByte(21);
				ProtocolParser.WriteSingle(stream, f);
			}
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
