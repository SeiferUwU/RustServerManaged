using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class Composter : IDisposable, Pool.IPooled, IProto<Composter>, IProto
{
	[NonSerialized]
	public float fertilizerProductionProgress;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(Composter instance)
	{
		if (instance.ShouldPool)
		{
			instance.fertilizerProductionProgress = 0f;
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
			throw new Exception("Trying to dispose Composter with ShouldPool set to false!");
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

	public void CopyTo(Composter instance)
	{
		instance.fertilizerProductionProgress = fertilizerProductionProgress;
	}

	public Composter Copy()
	{
		Composter composter = Pool.Get<Composter>();
		CopyTo(composter);
		return composter;
	}

	public static Composter Deserialize(BufferStream stream)
	{
		Composter composter = Pool.Get<Composter>();
		Deserialize(stream, composter, isDelta: false);
		return composter;
	}

	public static Composter DeserializeLengthDelimited(BufferStream stream)
	{
		Composter composter = Pool.Get<Composter>();
		DeserializeLengthDelimited(stream, composter, isDelta: false);
		return composter;
	}

	public static Composter DeserializeLength(BufferStream stream, int length)
	{
		Composter composter = Pool.Get<Composter>();
		DeserializeLength(stream, length, composter, isDelta: false);
		return composter;
	}

	public static Composter Deserialize(byte[] buffer)
	{
		Composter composter = Pool.Get<Composter>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, composter, isDelta: false);
		return composter;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, Composter previous)
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

	public static Composter Deserialize(BufferStream stream, Composter instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 13:
				instance.fertilizerProductionProgress = ProtocolParser.ReadSingle(stream);
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

	public static Composter DeserializeLengthDelimited(BufferStream stream, Composter instance, bool isDelta)
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
				instance.fertilizerProductionProgress = ProtocolParser.ReadSingle(stream);
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

	public static Composter DeserializeLength(BufferStream stream, int length, Composter instance, bool isDelta)
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
				instance.fertilizerProductionProgress = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, Composter instance, Composter previous)
	{
		if (instance.fertilizerProductionProgress != previous.fertilizerProductionProgress)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.fertilizerProductionProgress);
		}
	}

	public static void Serialize(BufferStream stream, Composter instance)
	{
		if (instance.fertilizerProductionProgress != 0f)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.fertilizerProductionProgress);
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
