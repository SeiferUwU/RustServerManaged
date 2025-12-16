using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class DeliveryDrone : IDisposable, Pool.IPooled, IProto<DeliveryDrone>, IProto
{
	[NonSerialized]
	public NetworkableId marketplaceId;

	[NonSerialized]
	public NetworkableId terminalId;

	[NonSerialized]
	public NetworkableId vendingMachineId;

	[NonSerialized]
	public int state;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(DeliveryDrone instance)
	{
		if (instance.ShouldPool)
		{
			instance.marketplaceId = default(NetworkableId);
			instance.terminalId = default(NetworkableId);
			instance.vendingMachineId = default(NetworkableId);
			instance.state = 0;
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
			throw new Exception("Trying to dispose DeliveryDrone with ShouldPool set to false!");
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

	public void CopyTo(DeliveryDrone instance)
	{
		instance.marketplaceId = marketplaceId;
		instance.terminalId = terminalId;
		instance.vendingMachineId = vendingMachineId;
		instance.state = state;
	}

	public DeliveryDrone Copy()
	{
		DeliveryDrone deliveryDrone = Pool.Get<DeliveryDrone>();
		CopyTo(deliveryDrone);
		return deliveryDrone;
	}

	public static DeliveryDrone Deserialize(BufferStream stream)
	{
		DeliveryDrone deliveryDrone = Pool.Get<DeliveryDrone>();
		Deserialize(stream, deliveryDrone, isDelta: false);
		return deliveryDrone;
	}

	public static DeliveryDrone DeserializeLengthDelimited(BufferStream stream)
	{
		DeliveryDrone deliveryDrone = Pool.Get<DeliveryDrone>();
		DeserializeLengthDelimited(stream, deliveryDrone, isDelta: false);
		return deliveryDrone;
	}

	public static DeliveryDrone DeserializeLength(BufferStream stream, int length)
	{
		DeliveryDrone deliveryDrone = Pool.Get<DeliveryDrone>();
		DeserializeLength(stream, length, deliveryDrone, isDelta: false);
		return deliveryDrone;
	}

	public static DeliveryDrone Deserialize(byte[] buffer)
	{
		DeliveryDrone deliveryDrone = Pool.Get<DeliveryDrone>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, deliveryDrone, isDelta: false);
		return deliveryDrone;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, DeliveryDrone previous)
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

	public static DeliveryDrone Deserialize(BufferStream stream, DeliveryDrone instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.marketplaceId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 16:
				instance.terminalId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 24:
				instance.vendingMachineId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 32:
				instance.state = (int)ProtocolParser.ReadUInt64(stream);
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

	public static DeliveryDrone DeserializeLengthDelimited(BufferStream stream, DeliveryDrone instance, bool isDelta)
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
				instance.marketplaceId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 16:
				instance.terminalId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 24:
				instance.vendingMachineId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 32:
				instance.state = (int)ProtocolParser.ReadUInt64(stream);
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

	public static DeliveryDrone DeserializeLength(BufferStream stream, int length, DeliveryDrone instance, bool isDelta)
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
				instance.marketplaceId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 16:
				instance.terminalId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 24:
				instance.vendingMachineId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 32:
				instance.state = (int)ProtocolParser.ReadUInt64(stream);
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

	public static void SerializeDelta(BufferStream stream, DeliveryDrone instance, DeliveryDrone previous)
	{
		stream.WriteByte(8);
		ProtocolParser.WriteUInt64(stream, instance.marketplaceId.Value);
		stream.WriteByte(16);
		ProtocolParser.WriteUInt64(stream, instance.terminalId.Value);
		stream.WriteByte(24);
		ProtocolParser.WriteUInt64(stream, instance.vendingMachineId.Value);
		if (instance.state != previous.state)
		{
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.state);
		}
	}

	public static void Serialize(BufferStream stream, DeliveryDrone instance)
	{
		if (instance.marketplaceId != default(NetworkableId))
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.marketplaceId.Value);
		}
		if (instance.terminalId != default(NetworkableId))
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.terminalId.Value);
		}
		if (instance.vendingMachineId != default(NetworkableId))
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, instance.vendingMachineId.Value);
		}
		if (instance.state != 0)
		{
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.state);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref marketplaceId.Value);
		action(UidType.NetworkableId, ref terminalId.Value);
		action(UidType.NetworkableId, ref vendingMachineId.Value);
	}
}
