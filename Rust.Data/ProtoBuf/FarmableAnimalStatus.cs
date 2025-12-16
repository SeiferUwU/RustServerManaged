using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class FarmableAnimalStatus : IDisposable, Pool.IPooled, IProto<FarmableAnimalStatus>, IProto
{
	[NonSerialized]
	public NetworkableId animal;

	[NonSerialized]
	public FarmableAnimal data;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(FarmableAnimalStatus instance)
	{
		if (instance.ShouldPool)
		{
			instance.animal = default(NetworkableId);
			if (instance.data != null)
			{
				instance.data.ResetToPool();
				instance.data = null;
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
			throw new Exception("Trying to dispose FarmableAnimalStatus with ShouldPool set to false!");
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

	public void CopyTo(FarmableAnimalStatus instance)
	{
		instance.animal = animal;
		if (data != null)
		{
			if (instance.data == null)
			{
				instance.data = data.Copy();
			}
			else
			{
				data.CopyTo(instance.data);
			}
		}
		else
		{
			instance.data = null;
		}
	}

	public FarmableAnimalStatus Copy()
	{
		FarmableAnimalStatus farmableAnimalStatus = Pool.Get<FarmableAnimalStatus>();
		CopyTo(farmableAnimalStatus);
		return farmableAnimalStatus;
	}

	public static FarmableAnimalStatus Deserialize(BufferStream stream)
	{
		FarmableAnimalStatus farmableAnimalStatus = Pool.Get<FarmableAnimalStatus>();
		Deserialize(stream, farmableAnimalStatus, isDelta: false);
		return farmableAnimalStatus;
	}

	public static FarmableAnimalStatus DeserializeLengthDelimited(BufferStream stream)
	{
		FarmableAnimalStatus farmableAnimalStatus = Pool.Get<FarmableAnimalStatus>();
		DeserializeLengthDelimited(stream, farmableAnimalStatus, isDelta: false);
		return farmableAnimalStatus;
	}

	public static FarmableAnimalStatus DeserializeLength(BufferStream stream, int length)
	{
		FarmableAnimalStatus farmableAnimalStatus = Pool.Get<FarmableAnimalStatus>();
		DeserializeLength(stream, length, farmableAnimalStatus, isDelta: false);
		return farmableAnimalStatus;
	}

	public static FarmableAnimalStatus Deserialize(byte[] buffer)
	{
		FarmableAnimalStatus farmableAnimalStatus = Pool.Get<FarmableAnimalStatus>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, farmableAnimalStatus, isDelta: false);
		return farmableAnimalStatus;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, FarmableAnimalStatus previous)
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

	public static FarmableAnimalStatus Deserialize(BufferStream stream, FarmableAnimalStatus instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.animal = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				break;
			case 18:
				if (instance.data == null)
				{
					instance.data = FarmableAnimal.DeserializeLengthDelimited(stream);
				}
				else
				{
					FarmableAnimal.DeserializeLengthDelimited(stream, instance.data, isDelta);
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

	public static FarmableAnimalStatus DeserializeLengthDelimited(BufferStream stream, FarmableAnimalStatus instance, bool isDelta)
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
				instance.animal = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				break;
			case 18:
				if (instance.data == null)
				{
					instance.data = FarmableAnimal.DeserializeLengthDelimited(stream);
				}
				else
				{
					FarmableAnimal.DeserializeLengthDelimited(stream, instance.data, isDelta);
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

	public static FarmableAnimalStatus DeserializeLength(BufferStream stream, int length, FarmableAnimalStatus instance, bool isDelta)
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
				instance.animal = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				break;
			case 18:
				if (instance.data == null)
				{
					instance.data = FarmableAnimal.DeserializeLengthDelimited(stream);
				}
				else
				{
					FarmableAnimal.DeserializeLengthDelimited(stream, instance.data, isDelta);
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

	public static void SerializeDelta(BufferStream stream, FarmableAnimalStatus instance, FarmableAnimalStatus previous)
	{
		stream.WriteByte(8);
		ProtocolParser.WriteUInt64(stream, instance.animal.Value);
		if (instance.data == null)
		{
			return;
		}
		stream.WriteByte(18);
		BufferStream.RangeHandle range = stream.GetRange(5);
		int position = stream.Position;
		FarmableAnimal.SerializeDelta(stream, instance.data, previous.data);
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

	public static void Serialize(BufferStream stream, FarmableAnimalStatus instance)
	{
		if (instance.animal != default(NetworkableId))
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.animal.Value);
		}
		if (instance.data == null)
		{
			return;
		}
		stream.WriteByte(18);
		BufferStream.RangeHandle range = stream.GetRange(5);
		int position = stream.Position;
		FarmableAnimal.Serialize(stream, instance.data);
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

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref animal.Value);
		data?.InspectUids(action);
	}
}
