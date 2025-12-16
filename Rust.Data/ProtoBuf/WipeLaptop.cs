using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class WipeLaptop : IDisposable, Pool.IPooled, IProto<WipeLaptop>, IProto
{
	[NonSerialized]
	public int timeLeft;

	[NonSerialized]
	public float armTime;

	[NonSerialized]
	public float disarmTime;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(WipeLaptop instance)
	{
		if (instance.ShouldPool)
		{
			instance.timeLeft = 0;
			instance.armTime = 0f;
			instance.disarmTime = 0f;
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
			throw new Exception("Trying to dispose WipeLaptop with ShouldPool set to false!");
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

	public void CopyTo(WipeLaptop instance)
	{
		instance.timeLeft = timeLeft;
		instance.armTime = armTime;
		instance.disarmTime = disarmTime;
	}

	public WipeLaptop Copy()
	{
		WipeLaptop wipeLaptop = Pool.Get<WipeLaptop>();
		CopyTo(wipeLaptop);
		return wipeLaptop;
	}

	public static WipeLaptop Deserialize(BufferStream stream)
	{
		WipeLaptop wipeLaptop = Pool.Get<WipeLaptop>();
		Deserialize(stream, wipeLaptop, isDelta: false);
		return wipeLaptop;
	}

	public static WipeLaptop DeserializeLengthDelimited(BufferStream stream)
	{
		WipeLaptop wipeLaptop = Pool.Get<WipeLaptop>();
		DeserializeLengthDelimited(stream, wipeLaptop, isDelta: false);
		return wipeLaptop;
	}

	public static WipeLaptop DeserializeLength(BufferStream stream, int length)
	{
		WipeLaptop wipeLaptop = Pool.Get<WipeLaptop>();
		DeserializeLength(stream, length, wipeLaptop, isDelta: false);
		return wipeLaptop;
	}

	public static WipeLaptop Deserialize(byte[] buffer)
	{
		WipeLaptop wipeLaptop = Pool.Get<WipeLaptop>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, wipeLaptop, isDelta: false);
		return wipeLaptop;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, WipeLaptop previous)
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

	public static WipeLaptop Deserialize(BufferStream stream, WipeLaptop instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.timeLeft = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 21:
				instance.armTime = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.disarmTime = ProtocolParser.ReadSingle(stream);
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

	public static WipeLaptop DeserializeLengthDelimited(BufferStream stream, WipeLaptop instance, bool isDelta)
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
				instance.timeLeft = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 21:
				instance.armTime = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.disarmTime = ProtocolParser.ReadSingle(stream);
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

	public static WipeLaptop DeserializeLength(BufferStream stream, int length, WipeLaptop instance, bool isDelta)
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
				instance.timeLeft = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 21:
				instance.armTime = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.disarmTime = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, WipeLaptop instance, WipeLaptop previous)
	{
		if (instance.timeLeft != previous.timeLeft)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.timeLeft);
		}
		if (instance.armTime != previous.armTime)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.armTime);
		}
		if (instance.disarmTime != previous.disarmTime)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.disarmTime);
		}
	}

	public static void Serialize(BufferStream stream, WipeLaptop instance)
	{
		if (instance.timeLeft != 0)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.timeLeft);
		}
		if (instance.armTime != 0f)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.armTime);
		}
		if (instance.disarmTime != 0f)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.disarmTime);
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
