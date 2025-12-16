using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class AppTime : IDisposable, Pool.IPooled, IProto<AppTime>, IProto
{
	[NonSerialized]
	public float dayLengthMinutes;

	[NonSerialized]
	public float timeScale;

	[NonSerialized]
	public float sunrise;

	[NonSerialized]
	public float sunset;

	[NonSerialized]
	public float time;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(AppTime instance)
	{
		if (instance.ShouldPool)
		{
			instance.dayLengthMinutes = 0f;
			instance.timeScale = 0f;
			instance.sunrise = 0f;
			instance.sunset = 0f;
			instance.time = 0f;
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
			throw new Exception("Trying to dispose AppTime with ShouldPool set to false!");
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

	public void CopyTo(AppTime instance)
	{
		instance.dayLengthMinutes = dayLengthMinutes;
		instance.timeScale = timeScale;
		instance.sunrise = sunrise;
		instance.sunset = sunset;
		instance.time = time;
	}

	public AppTime Copy()
	{
		AppTime appTime = Pool.Get<AppTime>();
		CopyTo(appTime);
		return appTime;
	}

	public static AppTime Deserialize(BufferStream stream)
	{
		AppTime appTime = Pool.Get<AppTime>();
		Deserialize(stream, appTime, isDelta: false);
		return appTime;
	}

	public static AppTime DeserializeLengthDelimited(BufferStream stream)
	{
		AppTime appTime = Pool.Get<AppTime>();
		DeserializeLengthDelimited(stream, appTime, isDelta: false);
		return appTime;
	}

	public static AppTime DeserializeLength(BufferStream stream, int length)
	{
		AppTime appTime = Pool.Get<AppTime>();
		DeserializeLength(stream, length, appTime, isDelta: false);
		return appTime;
	}

	public static AppTime Deserialize(byte[] buffer)
	{
		AppTime appTime = Pool.Get<AppTime>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, appTime, isDelta: false);
		return appTime;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, AppTime previous)
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

	public static AppTime Deserialize(BufferStream stream, AppTime instance, bool isDelta)
	{
		if (!isDelta)
		{
			instance.dayLengthMinutes = 0f;
			instance.timeScale = 0f;
			instance.sunrise = 0f;
			instance.sunset = 0f;
			instance.time = 0f;
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 13:
				instance.dayLengthMinutes = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.timeScale = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.sunrise = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.sunset = ProtocolParser.ReadSingle(stream);
				continue;
			case 45:
				instance.time = ProtocolParser.ReadSingle(stream);
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

	public static AppTime DeserializeLengthDelimited(BufferStream stream, AppTime instance, bool isDelta)
	{
		if (!isDelta)
		{
			instance.dayLengthMinutes = 0f;
			instance.timeScale = 0f;
			instance.sunrise = 0f;
			instance.sunset = 0f;
			instance.time = 0f;
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
			case 13:
				instance.dayLengthMinutes = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.timeScale = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.sunrise = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.sunset = ProtocolParser.ReadSingle(stream);
				continue;
			case 45:
				instance.time = ProtocolParser.ReadSingle(stream);
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

	public static AppTime DeserializeLength(BufferStream stream, int length, AppTime instance, bool isDelta)
	{
		if (!isDelta)
		{
			instance.dayLengthMinutes = 0f;
			instance.timeScale = 0f;
			instance.sunrise = 0f;
			instance.sunset = 0f;
			instance.time = 0f;
		}
		long num = stream.Position + length;
		while (stream.Position < num)
		{
			int num2 = stream.ReadByte();
			switch (num2)
			{
			case -1:
				throw new EndOfStreamException();
			case 13:
				instance.dayLengthMinutes = ProtocolParser.ReadSingle(stream);
				continue;
			case 21:
				instance.timeScale = ProtocolParser.ReadSingle(stream);
				continue;
			case 29:
				instance.sunrise = ProtocolParser.ReadSingle(stream);
				continue;
			case 37:
				instance.sunset = ProtocolParser.ReadSingle(stream);
				continue;
			case 45:
				instance.time = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, AppTime instance, AppTime previous)
	{
		if (instance.dayLengthMinutes != previous.dayLengthMinutes)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.dayLengthMinutes);
		}
		if (instance.timeScale != previous.timeScale)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.timeScale);
		}
		if (instance.sunrise != previous.sunrise)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.sunrise);
		}
		if (instance.sunset != previous.sunset)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.sunset);
		}
		if (instance.time != previous.time)
		{
			stream.WriteByte(45);
			ProtocolParser.WriteSingle(stream, instance.time);
		}
	}

	public static void Serialize(BufferStream stream, AppTime instance)
	{
		if (instance.dayLengthMinutes != 0f)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.dayLengthMinutes);
		}
		if (instance.timeScale != 0f)
		{
			stream.WriteByte(21);
			ProtocolParser.WriteSingle(stream, instance.timeScale);
		}
		if (instance.sunrise != 0f)
		{
			stream.WriteByte(29);
			ProtocolParser.WriteSingle(stream, instance.sunrise);
		}
		if (instance.sunset != 0f)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.sunset);
		}
		if (instance.time != 0f)
		{
			stream.WriteByte(45);
			ProtocolParser.WriteSingle(stream, instance.time);
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
