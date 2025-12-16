using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class ItemOwnershipAmount : IDisposable, Pool.IPooled, IProto<ItemOwnershipAmount>, IProto
{
	[NonSerialized]
	public string username;

	[NonSerialized]
	public string reason;

	[NonSerialized]
	public int amount;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(ItemOwnershipAmount instance)
	{
		if (instance.ShouldPool)
		{
			instance.username = string.Empty;
			instance.reason = string.Empty;
			instance.amount = 0;
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
			throw new Exception("Trying to dispose ItemOwnershipAmount with ShouldPool set to false!");
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

	public void CopyTo(ItemOwnershipAmount instance)
	{
		instance.username = username;
		instance.reason = reason;
		instance.amount = amount;
	}

	public ItemOwnershipAmount Copy()
	{
		ItemOwnershipAmount itemOwnershipAmount = Pool.Get<ItemOwnershipAmount>();
		CopyTo(itemOwnershipAmount);
		return itemOwnershipAmount;
	}

	public static ItemOwnershipAmount Deserialize(BufferStream stream)
	{
		ItemOwnershipAmount itemOwnershipAmount = Pool.Get<ItemOwnershipAmount>();
		Deserialize(stream, itemOwnershipAmount, isDelta: false);
		return itemOwnershipAmount;
	}

	public static ItemOwnershipAmount DeserializeLengthDelimited(BufferStream stream)
	{
		ItemOwnershipAmount itemOwnershipAmount = Pool.Get<ItemOwnershipAmount>();
		DeserializeLengthDelimited(stream, itemOwnershipAmount, isDelta: false);
		return itemOwnershipAmount;
	}

	public static ItemOwnershipAmount DeserializeLength(BufferStream stream, int length)
	{
		ItemOwnershipAmount itemOwnershipAmount = Pool.Get<ItemOwnershipAmount>();
		DeserializeLength(stream, length, itemOwnershipAmount, isDelta: false);
		return itemOwnershipAmount;
	}

	public static ItemOwnershipAmount Deserialize(byte[] buffer)
	{
		ItemOwnershipAmount itemOwnershipAmount = Pool.Get<ItemOwnershipAmount>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, itemOwnershipAmount, isDelta: false);
		return itemOwnershipAmount;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, ItemOwnershipAmount previous)
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

	public static ItemOwnershipAmount Deserialize(BufferStream stream, ItemOwnershipAmount instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 26:
				instance.username = ProtocolParser.ReadString(stream);
				continue;
			case 10:
				instance.reason = ProtocolParser.ReadString(stream);
				continue;
			case 16:
				instance.amount = (int)ProtocolParser.ReadUInt64(stream);
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

	public static ItemOwnershipAmount DeserializeLengthDelimited(BufferStream stream, ItemOwnershipAmount instance, bool isDelta)
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
			case 26:
				instance.username = ProtocolParser.ReadString(stream);
				continue;
			case 10:
				instance.reason = ProtocolParser.ReadString(stream);
				continue;
			case 16:
				instance.amount = (int)ProtocolParser.ReadUInt64(stream);
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

	public static ItemOwnershipAmount DeserializeLength(BufferStream stream, int length, ItemOwnershipAmount instance, bool isDelta)
	{
		long num = stream.Position + length;
		while (stream.Position < num)
		{
			int num2 = stream.ReadByte();
			switch (num2)
			{
			case -1:
				throw new EndOfStreamException();
			case 26:
				instance.username = ProtocolParser.ReadString(stream);
				continue;
			case 10:
				instance.reason = ProtocolParser.ReadString(stream);
				continue;
			case 16:
				instance.amount = (int)ProtocolParser.ReadUInt64(stream);
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

	public static void SerializeDelta(BufferStream stream, ItemOwnershipAmount instance, ItemOwnershipAmount previous)
	{
		if (instance.username != null && instance.username != previous.username)
		{
			stream.WriteByte(26);
			ProtocolParser.WriteString(stream, instance.username);
		}
		if (instance.reason != null && instance.reason != previous.reason)
		{
			stream.WriteByte(10);
			ProtocolParser.WriteString(stream, instance.reason);
		}
		if (instance.amount != previous.amount)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.amount);
		}
	}

	public static void Serialize(BufferStream stream, ItemOwnershipAmount instance)
	{
		if (instance.username != null)
		{
			stream.WriteByte(26);
			ProtocolParser.WriteString(stream, instance.username);
		}
		if (instance.reason != null)
		{
			stream.WriteByte(10);
			ProtocolParser.WriteString(stream, instance.reason);
		}
		if (instance.amount != 0)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.amount);
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
