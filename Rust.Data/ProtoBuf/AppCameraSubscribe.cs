using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class AppCameraSubscribe : IDisposable, Pool.IPooled, IProto<AppCameraSubscribe>, IProto
{
	[NonSerialized]
	public string cameraId;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(AppCameraSubscribe instance)
	{
		if (instance.ShouldPool)
		{
			instance.cameraId = string.Empty;
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
			throw new Exception("Trying to dispose AppCameraSubscribe with ShouldPool set to false!");
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

	public void CopyTo(AppCameraSubscribe instance)
	{
		instance.cameraId = cameraId;
	}

	public AppCameraSubscribe Copy()
	{
		AppCameraSubscribe appCameraSubscribe = Pool.Get<AppCameraSubscribe>();
		CopyTo(appCameraSubscribe);
		return appCameraSubscribe;
	}

	public static AppCameraSubscribe Deserialize(BufferStream stream)
	{
		AppCameraSubscribe appCameraSubscribe = Pool.Get<AppCameraSubscribe>();
		Deserialize(stream, appCameraSubscribe, isDelta: false);
		return appCameraSubscribe;
	}

	public static AppCameraSubscribe DeserializeLengthDelimited(BufferStream stream)
	{
		AppCameraSubscribe appCameraSubscribe = Pool.Get<AppCameraSubscribe>();
		DeserializeLengthDelimited(stream, appCameraSubscribe, isDelta: false);
		return appCameraSubscribe;
	}

	public static AppCameraSubscribe DeserializeLength(BufferStream stream, int length)
	{
		AppCameraSubscribe appCameraSubscribe = Pool.Get<AppCameraSubscribe>();
		DeserializeLength(stream, length, appCameraSubscribe, isDelta: false);
		return appCameraSubscribe;
	}

	public static AppCameraSubscribe Deserialize(byte[] buffer)
	{
		AppCameraSubscribe appCameraSubscribe = Pool.Get<AppCameraSubscribe>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, appCameraSubscribe, isDelta: false);
		return appCameraSubscribe;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, AppCameraSubscribe previous)
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

	public static AppCameraSubscribe Deserialize(BufferStream stream, AppCameraSubscribe instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 10:
				instance.cameraId = ProtocolParser.ReadString(stream);
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

	public static AppCameraSubscribe DeserializeLengthDelimited(BufferStream stream, AppCameraSubscribe instance, bool isDelta)
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
			case 10:
				instance.cameraId = ProtocolParser.ReadString(stream);
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

	public static AppCameraSubscribe DeserializeLength(BufferStream stream, int length, AppCameraSubscribe instance, bool isDelta)
	{
		long num = stream.Position + length;
		while (stream.Position < num)
		{
			int num2 = stream.ReadByte();
			switch (num2)
			{
			case -1:
				throw new EndOfStreamException();
			case 10:
				instance.cameraId = ProtocolParser.ReadString(stream);
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

	public static void SerializeDelta(BufferStream stream, AppCameraSubscribe instance, AppCameraSubscribe previous)
	{
		if (instance.cameraId != previous.cameraId)
		{
			if (instance.cameraId == null)
			{
				throw new ArgumentNullException("cameraId", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteString(stream, instance.cameraId);
		}
	}

	public static void Serialize(BufferStream stream, AppCameraSubscribe instance)
	{
		if (instance.cameraId == null)
		{
			throw new ArgumentNullException("cameraId", "Required by proto specification.");
		}
		stream.WriteByte(10);
		ProtocolParser.WriteString(stream, instance.cameraId);
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
	}
}
