using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class AppSetEntityValue : IDisposable, Pool.IPooled, IProto<AppSetEntityValue>, IProto
{
	[NonSerialized]
	public bool value;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(AppSetEntityValue instance)
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
			throw new Exception("Trying to dispose AppSetEntityValue with ShouldPool set to false!");
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

	public void CopyTo(AppSetEntityValue instance)
	{
		instance.value = value;
	}

	public AppSetEntityValue Copy()
	{
		AppSetEntityValue appSetEntityValue = Pool.Get<AppSetEntityValue>();
		CopyTo(appSetEntityValue);
		return appSetEntityValue;
	}

	public static AppSetEntityValue Deserialize(BufferStream stream)
	{
		AppSetEntityValue appSetEntityValue = Pool.Get<AppSetEntityValue>();
		Deserialize(stream, appSetEntityValue, isDelta: false);
		return appSetEntityValue;
	}

	public static AppSetEntityValue DeserializeLengthDelimited(BufferStream stream)
	{
		AppSetEntityValue appSetEntityValue = Pool.Get<AppSetEntityValue>();
		DeserializeLengthDelimited(stream, appSetEntityValue, isDelta: false);
		return appSetEntityValue;
	}

	public static AppSetEntityValue DeserializeLength(BufferStream stream, int length)
	{
		AppSetEntityValue appSetEntityValue = Pool.Get<AppSetEntityValue>();
		DeserializeLength(stream, length, appSetEntityValue, isDelta: false);
		return appSetEntityValue;
	}

	public static AppSetEntityValue Deserialize(byte[] buffer)
	{
		AppSetEntityValue appSetEntityValue = Pool.Get<AppSetEntityValue>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, appSetEntityValue, isDelta: false);
		return appSetEntityValue;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, AppSetEntityValue previous)
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

	public static AppSetEntityValue Deserialize(BufferStream stream, AppSetEntityValue instance, bool isDelta)
	{
		if (!isDelta)
		{
			instance.value = false;
		}
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

	public static AppSetEntityValue DeserializeLengthDelimited(BufferStream stream, AppSetEntityValue instance, bool isDelta)
	{
		if (!isDelta)
		{
			instance.value = false;
		}
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

	public static AppSetEntityValue DeserializeLength(BufferStream stream, int length, AppSetEntityValue instance, bool isDelta)
	{
		if (!isDelta)
		{
			instance.value = false;
		}
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

	public static void SerializeDelta(BufferStream stream, AppSetEntityValue instance, AppSetEntityValue previous)
	{
		stream.WriteByte(8);
		ProtocolParser.WriteBool(stream, instance.value);
	}

	public static void Serialize(BufferStream stream, AppSetEntityValue instance)
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
