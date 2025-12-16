using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class ObjectiveStatus : IDisposable, Pool.IPooled, IProto<ObjectiveStatus>, IProto
{
	[NonSerialized]
	public bool started;

	[NonSerialized]
	public bool completed;

	[NonSerialized]
	public bool failed;

	[NonSerialized]
	public float progressCurrent;

	[NonSerialized]
	public float progressTarget;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(ObjectiveStatus instance)
	{
		if (instance.ShouldPool)
		{
			instance.started = false;
			instance.completed = false;
			instance.failed = false;
			instance.progressCurrent = 0f;
			instance.progressTarget = 0f;
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
			throw new Exception("Trying to dispose ObjectiveStatus with ShouldPool set to false!");
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

	public void CopyTo(ObjectiveStatus instance)
	{
		instance.started = started;
		instance.completed = completed;
		instance.failed = failed;
		instance.progressCurrent = progressCurrent;
		instance.progressTarget = progressTarget;
	}

	public ObjectiveStatus Copy()
	{
		ObjectiveStatus objectiveStatus = Pool.Get<ObjectiveStatus>();
		CopyTo(objectiveStatus);
		return objectiveStatus;
	}

	public static ObjectiveStatus Deserialize(BufferStream stream)
	{
		ObjectiveStatus objectiveStatus = Pool.Get<ObjectiveStatus>();
		Deserialize(stream, objectiveStatus, isDelta: false);
		return objectiveStatus;
	}

	public static ObjectiveStatus DeserializeLengthDelimited(BufferStream stream)
	{
		ObjectiveStatus objectiveStatus = Pool.Get<ObjectiveStatus>();
		DeserializeLengthDelimited(stream, objectiveStatus, isDelta: false);
		return objectiveStatus;
	}

	public static ObjectiveStatus DeserializeLength(BufferStream stream, int length)
	{
		ObjectiveStatus objectiveStatus = Pool.Get<ObjectiveStatus>();
		DeserializeLength(stream, length, objectiveStatus, isDelta: false);
		return objectiveStatus;
	}

	public static ObjectiveStatus Deserialize(byte[] buffer)
	{
		ObjectiveStatus objectiveStatus = Pool.Get<ObjectiveStatus>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, objectiveStatus, isDelta: false);
		return objectiveStatus;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, ObjectiveStatus previous)
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

	public static ObjectiveStatus Deserialize(BufferStream stream, ObjectiveStatus instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.started = ProtocolParser.ReadBool(stream);
				continue;
			case 16:
				instance.completed = ProtocolParser.ReadBool(stream);
				continue;
			case 24:
				instance.failed = ProtocolParser.ReadBool(stream);
				continue;
			case 37:
				instance.progressCurrent = ProtocolParser.ReadSingle(stream);
				continue;
			case 45:
				instance.progressTarget = ProtocolParser.ReadSingle(stream);
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

	public static ObjectiveStatus DeserializeLengthDelimited(BufferStream stream, ObjectiveStatus instance, bool isDelta)
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
				instance.started = ProtocolParser.ReadBool(stream);
				continue;
			case 16:
				instance.completed = ProtocolParser.ReadBool(stream);
				continue;
			case 24:
				instance.failed = ProtocolParser.ReadBool(stream);
				continue;
			case 37:
				instance.progressCurrent = ProtocolParser.ReadSingle(stream);
				continue;
			case 45:
				instance.progressTarget = ProtocolParser.ReadSingle(stream);
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

	public static ObjectiveStatus DeserializeLength(BufferStream stream, int length, ObjectiveStatus instance, bool isDelta)
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
				instance.started = ProtocolParser.ReadBool(stream);
				continue;
			case 16:
				instance.completed = ProtocolParser.ReadBool(stream);
				continue;
			case 24:
				instance.failed = ProtocolParser.ReadBool(stream);
				continue;
			case 37:
				instance.progressCurrent = ProtocolParser.ReadSingle(stream);
				continue;
			case 45:
				instance.progressTarget = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, ObjectiveStatus instance, ObjectiveStatus previous)
	{
		stream.WriteByte(8);
		ProtocolParser.WriteBool(stream, instance.started);
		stream.WriteByte(16);
		ProtocolParser.WriteBool(stream, instance.completed);
		stream.WriteByte(24);
		ProtocolParser.WriteBool(stream, instance.failed);
		if (instance.progressCurrent != previous.progressCurrent)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.progressCurrent);
		}
		if (instance.progressTarget != previous.progressTarget)
		{
			stream.WriteByte(45);
			ProtocolParser.WriteSingle(stream, instance.progressTarget);
		}
	}

	public static void Serialize(BufferStream stream, ObjectiveStatus instance)
	{
		if (instance.started)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteBool(stream, instance.started);
		}
		if (instance.completed)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteBool(stream, instance.completed);
		}
		if (instance.failed)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteBool(stream, instance.failed);
		}
		if (instance.progressCurrent != 0f)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.progressCurrent);
		}
		if (instance.progressTarget != 0f)
		{
			stream.WriteByte(45);
			ProtocolParser.WriteSingle(stream, instance.progressTarget);
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
