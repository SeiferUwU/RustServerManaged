using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class ShopKeeper : IDisposable, Pool.IPooled, IProto<ShopKeeper>, IProto
{
	[NonSerialized]
	public NetworkableId vendingRef;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(ShopKeeper instance)
	{
		if (instance.ShouldPool)
		{
			instance.vendingRef = default(NetworkableId);
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
			throw new Exception("Trying to dispose ShopKeeper with ShouldPool set to false!");
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

	public void CopyTo(ShopKeeper instance)
	{
		instance.vendingRef = vendingRef;
	}

	public ShopKeeper Copy()
	{
		ShopKeeper shopKeeper = Pool.Get<ShopKeeper>();
		CopyTo(shopKeeper);
		return shopKeeper;
	}

	public static ShopKeeper Deserialize(BufferStream stream)
	{
		ShopKeeper shopKeeper = Pool.Get<ShopKeeper>();
		Deserialize(stream, shopKeeper, isDelta: false);
		return shopKeeper;
	}

	public static ShopKeeper DeserializeLengthDelimited(BufferStream stream)
	{
		ShopKeeper shopKeeper = Pool.Get<ShopKeeper>();
		DeserializeLengthDelimited(stream, shopKeeper, isDelta: false);
		return shopKeeper;
	}

	public static ShopKeeper DeserializeLength(BufferStream stream, int length)
	{
		ShopKeeper shopKeeper = Pool.Get<ShopKeeper>();
		DeserializeLength(stream, length, shopKeeper, isDelta: false);
		return shopKeeper;
	}

	public static ShopKeeper Deserialize(byte[] buffer)
	{
		ShopKeeper shopKeeper = Pool.Get<ShopKeeper>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, shopKeeper, isDelta: false);
		return shopKeeper;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, ShopKeeper previous)
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

	public static ShopKeeper Deserialize(BufferStream stream, ShopKeeper instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.vendingRef = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static ShopKeeper DeserializeLengthDelimited(BufferStream stream, ShopKeeper instance, bool isDelta)
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
				instance.vendingRef = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static ShopKeeper DeserializeLength(BufferStream stream, int length, ShopKeeper instance, bool isDelta)
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
				instance.vendingRef = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static void SerializeDelta(BufferStream stream, ShopKeeper instance, ShopKeeper previous)
	{
		stream.WriteByte(8);
		ProtocolParser.WriteUInt64(stream, instance.vendingRef.Value);
	}

	public static void Serialize(BufferStream stream, ShopKeeper instance)
	{
		if (instance.vendingRef != default(NetworkableId))
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.vendingRef.Value);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref vendingRef.Value);
	}
}
