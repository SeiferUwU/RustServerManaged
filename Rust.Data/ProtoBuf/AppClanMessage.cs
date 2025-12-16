using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class AppClanMessage : IDisposable, Pool.IPooled, IProto<AppClanMessage>, IProto
{
	[NonSerialized]
	public ulong steamId;

	[NonSerialized]
	public string name;

	[NonSerialized]
	public string message;

	[NonSerialized]
	public long time;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(AppClanMessage instance)
	{
		if (instance.ShouldPool)
		{
			instance.steamId = 0uL;
			instance.name = string.Empty;
			instance.message = string.Empty;
			instance.time = 0L;
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
			throw new Exception("Trying to dispose AppClanMessage with ShouldPool set to false!");
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

	public void CopyTo(AppClanMessage instance)
	{
		instance.steamId = steamId;
		instance.name = name;
		instance.message = message;
		instance.time = time;
	}

	public AppClanMessage Copy()
	{
		AppClanMessage appClanMessage = Pool.Get<AppClanMessage>();
		CopyTo(appClanMessage);
		return appClanMessage;
	}

	public static AppClanMessage Deserialize(BufferStream stream)
	{
		AppClanMessage appClanMessage = Pool.Get<AppClanMessage>();
		Deserialize(stream, appClanMessage, isDelta: false);
		return appClanMessage;
	}

	public static AppClanMessage DeserializeLengthDelimited(BufferStream stream)
	{
		AppClanMessage appClanMessage = Pool.Get<AppClanMessage>();
		DeserializeLengthDelimited(stream, appClanMessage, isDelta: false);
		return appClanMessage;
	}

	public static AppClanMessage DeserializeLength(BufferStream stream, int length)
	{
		AppClanMessage appClanMessage = Pool.Get<AppClanMessage>();
		DeserializeLength(stream, length, appClanMessage, isDelta: false);
		return appClanMessage;
	}

	public static AppClanMessage Deserialize(byte[] buffer)
	{
		AppClanMessage appClanMessage = Pool.Get<AppClanMessage>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, appClanMessage, isDelta: false);
		return appClanMessage;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, AppClanMessage previous)
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

	public static AppClanMessage Deserialize(BufferStream stream, AppClanMessage instance, bool isDelta)
	{
		if (!isDelta)
		{
			instance.steamId = 0uL;
			instance.time = 0L;
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.steamId = ProtocolParser.ReadUInt64(stream);
				continue;
			case 18:
				instance.name = ProtocolParser.ReadString(stream);
				continue;
			case 26:
				instance.message = ProtocolParser.ReadString(stream);
				continue;
			case 32:
				instance.time = (long)ProtocolParser.ReadUInt64(stream);
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

	public static AppClanMessage DeserializeLengthDelimited(BufferStream stream, AppClanMessage instance, bool isDelta)
	{
		if (!isDelta)
		{
			instance.steamId = 0uL;
			instance.time = 0L;
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
				instance.steamId = ProtocolParser.ReadUInt64(stream);
				continue;
			case 18:
				instance.name = ProtocolParser.ReadString(stream);
				continue;
			case 26:
				instance.message = ProtocolParser.ReadString(stream);
				continue;
			case 32:
				instance.time = (long)ProtocolParser.ReadUInt64(stream);
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

	public static AppClanMessage DeserializeLength(BufferStream stream, int length, AppClanMessage instance, bool isDelta)
	{
		if (!isDelta)
		{
			instance.steamId = 0uL;
			instance.time = 0L;
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
				instance.steamId = ProtocolParser.ReadUInt64(stream);
				continue;
			case 18:
				instance.name = ProtocolParser.ReadString(stream);
				continue;
			case 26:
				instance.message = ProtocolParser.ReadString(stream);
				continue;
			case 32:
				instance.time = (long)ProtocolParser.ReadUInt64(stream);
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

	public static void SerializeDelta(BufferStream stream, AppClanMessage instance, AppClanMessage previous)
	{
		if (instance.steamId != previous.steamId)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.steamId);
		}
		if (instance.name != previous.name)
		{
			if (instance.name == null)
			{
				throw new ArgumentNullException("name", "Required by proto specification.");
			}
			stream.WriteByte(18);
			ProtocolParser.WriteString(stream, instance.name);
		}
		if (instance.message != previous.message)
		{
			if (instance.message == null)
			{
				throw new ArgumentNullException("message", "Required by proto specification.");
			}
			stream.WriteByte(26);
			ProtocolParser.WriteString(stream, instance.message);
		}
		stream.WriteByte(32);
		ProtocolParser.WriteUInt64(stream, (ulong)instance.time);
	}

	public static void Serialize(BufferStream stream, AppClanMessage instance)
	{
		if (instance.steamId != 0L)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.steamId);
		}
		if (instance.name == null)
		{
			throw new ArgumentNullException("name", "Required by proto specification.");
		}
		stream.WriteByte(18);
		ProtocolParser.WriteString(stream, instance.name);
		if (instance.message == null)
		{
			throw new ArgumentNullException("message", "Required by proto specification.");
		}
		stream.WriteByte(26);
		ProtocolParser.WriteString(stream, instance.message);
		if (instance.time != 0L)
		{
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.time);
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
