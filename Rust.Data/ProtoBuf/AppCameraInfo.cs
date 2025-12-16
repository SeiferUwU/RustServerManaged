using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class AppCameraInfo : IDisposable, Pool.IPooled, IProto<AppCameraInfo>, IProto
{
	[NonSerialized]
	public int width;

	[NonSerialized]
	public int height;

	[NonSerialized]
	public float nearPlane;

	[NonSerialized]
	public float farPlane;

	[NonSerialized]
	public int controlFlags;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(AppCameraInfo instance)
	{
		if (instance.ShouldPool)
		{
			instance.width = 0;
			instance.height = 0;
			instance.nearPlane = 0f;
			instance.farPlane = 0f;
			instance.controlFlags = 0;
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
			throw new Exception("Trying to dispose AppCameraInfo with ShouldPool set to false!");
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

	public void CopyTo(AppCameraInfo instance)
	{
		instance.width = width;
		instance.height = height;
		instance.nearPlane = nearPlane;
		instance.farPlane = farPlane;
		instance.controlFlags = controlFlags;
	}

	public AppCameraInfo Copy()
	{
		AppCameraInfo appCameraInfo = Pool.Get<AppCameraInfo>();
		CopyTo(appCameraInfo);
		return appCameraInfo;
	}

	public static AppCameraInfo Deserialize(BufferStream stream)
	{
		AppCameraInfo appCameraInfo = Pool.Get<AppCameraInfo>();
		Deserialize(stream, appCameraInfo, isDelta: false);
		return appCameraInfo;
	}

	public static AppCameraInfo DeserializeLengthDelimited(BufferStream stream)
	{
		AppCameraInfo appCameraInfo = Pool.Get<AppCameraInfo>();
		DeserializeLengthDelimited(stream, appCameraInfo, isDelta: false);
		return appCameraInfo;
	}

	public static AppCameraInfo DeserializeLength(BufferStream stream, int length)
	{
		AppCameraInfo appCameraInfo = Pool.Get<AppCameraInfo>();
		DeserializeLength(stream, length, appCameraInfo, isDelta: false);
		return appCameraInfo;
	}

	public static AppCameraInfo Deserialize(byte[] buffer)
	{
		AppCameraInfo appCameraInfo = Pool.Get<AppCameraInfo>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, appCameraInfo, isDelta: false);
		return appCameraInfo;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, AppCameraInfo previous)
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

	public static AppCameraInfo Deserialize(BufferStream stream, AppCameraInfo instance, bool isDelta)
	{
		if (!isDelta)
		{
			instance.width = 0;
			instance.height = 0;
			instance.nearPlane = 0f;
			instance.farPlane = 0f;
			instance.controlFlags = 0;
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.width = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.height = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 29:
				instance.nearPlane = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.farPlane = ProtocolParser.ReadSingle(stream);
				continue;
			case 40:
				instance.controlFlags = (int)ProtocolParser.ReadUInt64(stream);
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

	public static AppCameraInfo DeserializeLengthDelimited(BufferStream stream, AppCameraInfo instance, bool isDelta)
	{
		if (!isDelta)
		{
			instance.width = 0;
			instance.height = 0;
			instance.nearPlane = 0f;
			instance.farPlane = 0f;
			instance.controlFlags = 0;
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
				instance.width = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.height = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 29:
				instance.nearPlane = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.farPlane = ProtocolParser.ReadSingle(stream);
				continue;
			case 40:
				instance.controlFlags = (int)ProtocolParser.ReadUInt64(stream);
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

	public static AppCameraInfo DeserializeLength(BufferStream stream, int length, AppCameraInfo instance, bool isDelta)
	{
		if (!isDelta)
		{
			instance.width = 0;
			instance.height = 0;
			instance.nearPlane = 0f;
			instance.farPlane = 0f;
			instance.controlFlags = 0;
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
				instance.width = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 16:
				instance.height = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 29:
				instance.nearPlane = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.farPlane = ProtocolParser.ReadSingle(stream);
				continue;
			case 40:
				instance.controlFlags = (int)ProtocolParser.ReadUInt64(stream);
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

	public static void SerializeDelta(BufferStream stream, AppCameraInfo instance, AppCameraInfo previous)
	{
		if (instance.width != previous.width)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.width);
		}
		if (instance.height != previous.height)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.height);
		}
		if (instance.nearPlane != previous.nearPlane)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.nearPlane);
		}
		if (instance.farPlane != previous.farPlane)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.farPlane);
		}
		if (instance.controlFlags != previous.controlFlags)
		{
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.controlFlags);
		}
	}

	public static void Serialize(BufferStream stream, AppCameraInfo instance)
	{
		if (instance.width != 0)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.width);
		}
		if (instance.height != 0)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.height);
		}
		if (instance.nearPlane != 0f)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.nearPlane);
		}
		if (instance.farPlane != 0f)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.farPlane);
		}
		if (instance.controlFlags != 0)
		{
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.controlFlags);
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
