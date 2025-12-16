using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class VendingMachineLongTermStats : IDisposable, Pool.IPooled, IProto<VendingMachineLongTermStats>, IProto
{
	[NonSerialized]
	public int numberOfPurchases;

	[NonSerialized]
	public long bestSalesHour;

	[NonSerialized]
	public int uniqueCustomers;

	[NonSerialized]
	public int repeatCustomers;

	[NonSerialized]
	public int bestCustomer;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(VendingMachineLongTermStats instance)
	{
		if (instance.ShouldPool)
		{
			instance.numberOfPurchases = 0;
			instance.bestSalesHour = 0L;
			instance.uniqueCustomers = 0;
			instance.repeatCustomers = 0;
			instance.bestCustomer = 0;
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
			throw new Exception("Trying to dispose VendingMachineLongTermStats with ShouldPool set to false!");
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

	public void CopyTo(VendingMachineLongTermStats instance)
	{
		instance.numberOfPurchases = numberOfPurchases;
		instance.bestSalesHour = bestSalesHour;
		instance.uniqueCustomers = uniqueCustomers;
		instance.repeatCustomers = repeatCustomers;
		instance.bestCustomer = bestCustomer;
	}

	public VendingMachineLongTermStats Copy()
	{
		VendingMachineLongTermStats vendingMachineLongTermStats = Pool.Get<VendingMachineLongTermStats>();
		CopyTo(vendingMachineLongTermStats);
		return vendingMachineLongTermStats;
	}

	public static VendingMachineLongTermStats Deserialize(BufferStream stream)
	{
		VendingMachineLongTermStats vendingMachineLongTermStats = Pool.Get<VendingMachineLongTermStats>();
		Deserialize(stream, vendingMachineLongTermStats, isDelta: false);
		return vendingMachineLongTermStats;
	}

	public static VendingMachineLongTermStats DeserializeLengthDelimited(BufferStream stream)
	{
		VendingMachineLongTermStats vendingMachineLongTermStats = Pool.Get<VendingMachineLongTermStats>();
		DeserializeLengthDelimited(stream, vendingMachineLongTermStats, isDelta: false);
		return vendingMachineLongTermStats;
	}

	public static VendingMachineLongTermStats DeserializeLength(BufferStream stream, int length)
	{
		VendingMachineLongTermStats vendingMachineLongTermStats = Pool.Get<VendingMachineLongTermStats>();
		DeserializeLength(stream, length, vendingMachineLongTermStats, isDelta: false);
		return vendingMachineLongTermStats;
	}

	public static VendingMachineLongTermStats Deserialize(byte[] buffer)
	{
		VendingMachineLongTermStats vendingMachineLongTermStats = Pool.Get<VendingMachineLongTermStats>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, vendingMachineLongTermStats, isDelta: false);
		return vendingMachineLongTermStats;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, VendingMachineLongTermStats previous)
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

	public static VendingMachineLongTermStats Deserialize(BufferStream stream, VendingMachineLongTermStats instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.numberOfPurchases = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.bestSalesHour = (long)ProtocolParser.ReadUInt64(stream);
				continue;
			case 24:
				instance.uniqueCustomers = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 32:
				instance.repeatCustomers = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 40:
				instance.bestCustomer = (int)ProtocolParser.ReadUInt64(stream);
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

	public static VendingMachineLongTermStats DeserializeLengthDelimited(BufferStream stream, VendingMachineLongTermStats instance, bool isDelta)
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
				instance.numberOfPurchases = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.bestSalesHour = (long)ProtocolParser.ReadUInt64(stream);
				continue;
			case 24:
				instance.uniqueCustomers = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 32:
				instance.repeatCustomers = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 40:
				instance.bestCustomer = (int)ProtocolParser.ReadUInt64(stream);
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

	public static VendingMachineLongTermStats DeserializeLength(BufferStream stream, int length, VendingMachineLongTermStats instance, bool isDelta)
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
				instance.numberOfPurchases = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.bestSalesHour = (long)ProtocolParser.ReadUInt64(stream);
				continue;
			case 24:
				instance.uniqueCustomers = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 32:
				instance.repeatCustomers = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 40:
				instance.bestCustomer = (int)ProtocolParser.ReadUInt64(stream);
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

	public static void SerializeDelta(BufferStream stream, VendingMachineLongTermStats instance, VendingMachineLongTermStats previous)
	{
		if (instance.numberOfPurchases != previous.numberOfPurchases)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.numberOfPurchases);
		}
		stream.WriteByte(16);
		ProtocolParser.WriteUInt64(stream, (ulong)instance.bestSalesHour);
		if (instance.uniqueCustomers != previous.uniqueCustomers)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.uniqueCustomers);
		}
		if (instance.repeatCustomers != previous.repeatCustomers)
		{
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.repeatCustomers);
		}
		if (instance.bestCustomer != previous.bestCustomer)
		{
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.bestCustomer);
		}
	}

	public static void Serialize(BufferStream stream, VendingMachineLongTermStats instance)
	{
		if (instance.numberOfPurchases != 0)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.numberOfPurchases);
		}
		if (instance.bestSalesHour != 0L)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.bestSalesHour);
		}
		if (instance.uniqueCustomers != 0)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.uniqueCustomers);
		}
		if (instance.repeatCustomers != 0)
		{
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.repeatCustomers);
		}
		if (instance.bestCustomer != 0)
		{
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.bestCustomer);
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
