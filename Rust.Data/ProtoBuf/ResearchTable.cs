using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class ResearchTable : IDisposable, Pool.IPooled, IProto<ResearchTable>, IProto
{
	[NonSerialized]
	public float researchTimeLeft;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(ResearchTable instance)
	{
		if (instance.ShouldPool)
		{
			instance.researchTimeLeft = 0f;
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
			throw new Exception("Trying to dispose ResearchTable with ShouldPool set to false!");
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

	public void CopyTo(ResearchTable instance)
	{
		instance.researchTimeLeft = researchTimeLeft;
	}

	public ResearchTable Copy()
	{
		ResearchTable researchTable = Pool.Get<ResearchTable>();
		CopyTo(researchTable);
		return researchTable;
	}

	public static ResearchTable Deserialize(BufferStream stream)
	{
		ResearchTable researchTable = Pool.Get<ResearchTable>();
		Deserialize(stream, researchTable, isDelta: false);
		return researchTable;
	}

	public static ResearchTable DeserializeLengthDelimited(BufferStream stream)
	{
		ResearchTable researchTable = Pool.Get<ResearchTable>();
		DeserializeLengthDelimited(stream, researchTable, isDelta: false);
		return researchTable;
	}

	public static ResearchTable DeserializeLength(BufferStream stream, int length)
	{
		ResearchTable researchTable = Pool.Get<ResearchTable>();
		DeserializeLength(stream, length, researchTable, isDelta: false);
		return researchTable;
	}

	public static ResearchTable Deserialize(byte[] buffer)
	{
		ResearchTable researchTable = Pool.Get<ResearchTable>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, researchTable, isDelta: false);
		return researchTable;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, ResearchTable previous)
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

	public static ResearchTable Deserialize(BufferStream stream, ResearchTable instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 13:
				instance.researchTimeLeft = ProtocolParser.ReadSingle(stream);
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

	public static ResearchTable DeserializeLengthDelimited(BufferStream stream, ResearchTable instance, bool isDelta)
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
				instance.researchTimeLeft = ProtocolParser.ReadSingle(stream);
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

	public static ResearchTable DeserializeLength(BufferStream stream, int length, ResearchTable instance, bool isDelta)
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
				instance.researchTimeLeft = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, ResearchTable instance, ResearchTable previous)
	{
		if (instance.researchTimeLeft != previous.researchTimeLeft)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.researchTimeLeft);
		}
	}

	public static void Serialize(BufferStream stream, ResearchTable instance)
	{
		if (instance.researchTimeLeft != 0f)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.researchTimeLeft);
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
