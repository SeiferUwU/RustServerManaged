using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class AppFlag : IDisposable, Pool.IPooled, IProto<AppFlag>, IProto
{
	[NonSerialized]
	public bool value;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(AppFlag instance)
	{
		if (instance.ShouldPool)
		{
			instance.value = false;
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
			throw new Exception("Trying to dispose AppFlag with ShouldPool set to false!");
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

	public void CopyTo(AppFlag instance)
	{
		instance.value = value;
	}

	public AppFlag Copy()
	{
		AppFlag appFlag = Pool.Get<AppFlag>();
		CopyTo(appFlag);
		return appFlag;
	}

	public static AppFlag Deserialize(BufferStream stream)
	{
		AppFlag appFlag = Pool.Get<AppFlag>();
		Deserialize(stream, appFlag, isDelta: false);
		return appFlag;
	}

	public static AppFlag DeserializeLengthDelimited(BufferStream stream)
	{
		AppFlag appFlag = Pool.Get<AppFlag>();
		DeserializeLengthDelimited(stream, appFlag, isDelta: false);
		return appFlag;
	}

	public static AppFlag DeserializeLength(BufferStream stream, int length)
	{
		AppFlag appFlag = Pool.Get<AppFlag>();
		DeserializeLength(stream, length, appFlag, isDelta: false);
		return appFlag;
	}

	public static AppFlag Deserialize(byte[] buffer)
	{
		AppFlag appFlag = Pool.Get<AppFlag>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, appFlag, isDelta: false);
		return appFlag;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, AppFlag previous)
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

	public static AppFlag Deserialize(BufferStream stream, AppFlag instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.value = ProtocolParser.ReadBool(stream);
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

	public static AppFlag DeserializeLengthDelimited(BufferStream stream, AppFlag instance, bool isDelta)
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
				instance.value = ProtocolParser.ReadBool(stream);
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

	public static AppFlag DeserializeLength(BufferStream stream, int length, AppFlag instance, bool isDelta)
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
				instance.value = ProtocolParser.ReadBool(stream);
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

	public static void SerializeDelta(BufferStream stream, AppFlag instance, AppFlag previous)
	{
		stream.WriteByte(8);
		ProtocolParser.WriteBool(stream, instance.value);
	}

	public static void Serialize(BufferStream stream, AppFlag instance)
	{
		if (instance.value)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteBool(stream, instance.value);
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
