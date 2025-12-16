using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf.Nexus;

public class Status : IDisposable, Pool.IPooled, IProto<Status>, IProto
{
	[NonSerialized]
	public bool success;

	[NonSerialized]
	public string errorMessage;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(Status instance)
	{
		if (instance.ShouldPool)
		{
			instance.success = false;
			instance.errorMessage = string.Empty;
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
			throw new Exception("Trying to dispose Status with ShouldPool set to false!");
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

	public void CopyTo(Status instance)
	{
		instance.success = success;
		instance.errorMessage = errorMessage;
	}

	public Status Copy()
	{
		Status status = Pool.Get<Status>();
		CopyTo(status);
		return status;
	}

	public static Status Deserialize(BufferStream stream)
	{
		Status status = Pool.Get<Status>();
		Deserialize(stream, status, isDelta: false);
		return status;
	}

	public static Status DeserializeLengthDelimited(BufferStream stream)
	{
		Status status = Pool.Get<Status>();
		DeserializeLengthDelimited(stream, status, isDelta: false);
		return status;
	}

	public static Status DeserializeLength(BufferStream stream, int length)
	{
		Status status = Pool.Get<Status>();
		DeserializeLength(stream, length, status, isDelta: false);
		return status;
	}

	public static Status Deserialize(byte[] buffer)
	{
		Status status = Pool.Get<Status>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, status, isDelta: false);
		return status;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, Status previous)
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

	public static Status Deserialize(BufferStream stream, Status instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.success = ProtocolParser.ReadBool(stream);
				continue;
			case 18:
				instance.errorMessage = ProtocolParser.ReadString(stream);
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

	public static Status DeserializeLengthDelimited(BufferStream stream, Status instance, bool isDelta)
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
				instance.success = ProtocolParser.ReadBool(stream);
				continue;
			case 18:
				instance.errorMessage = ProtocolParser.ReadString(stream);
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

	public static Status DeserializeLength(BufferStream stream, int length, Status instance, bool isDelta)
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
				instance.success = ProtocolParser.ReadBool(stream);
				continue;
			case 18:
				instance.errorMessage = ProtocolParser.ReadString(stream);
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

	public static void SerializeDelta(BufferStream stream, Status instance, Status previous)
	{
		stream.WriteByte(8);
		ProtocolParser.WriteBool(stream, instance.success);
		if (instance.errorMessage != null && instance.errorMessage != previous.errorMessage)
		{
			stream.WriteByte(18);
			ProtocolParser.WriteString(stream, instance.errorMessage);
		}
	}

	public static void Serialize(BufferStream stream, Status instance)
	{
		if (instance.success)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteBool(stream, instance.success);
		}
		if (instance.errorMessage != null)
		{
			stream.WriteByte(18);
			ProtocolParser.WriteString(stream, instance.errorMessage);
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
