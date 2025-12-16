using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class AppTeamChat : IDisposable, Pool.IPooled, IProto<AppTeamChat>, IProto
{
	[NonSerialized]
	public List<AppTeamMessage> messages;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(AppTeamChat instance)
	{
		if (!instance.ShouldPool)
		{
			return;
		}
		if (instance.messages != null)
		{
			for (int i = 0; i < instance.messages.Count; i++)
			{
				if (instance.messages[i] != null)
				{
					instance.messages[i].ResetToPool();
					instance.messages[i] = null;
				}
			}
			List<AppTeamMessage> obj = instance.messages;
			Pool.Free(ref obj, freeElements: false);
			instance.messages = obj;
		}
		Pool.Free(ref instance);
	}

	public void ResetToPool()
	{
		ResetToPool(this);
	}

	public virtual void Dispose()
	{
		if (!ShouldPool)
		{
			throw new Exception("Trying to dispose AppTeamChat with ShouldPool set to false!");
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

	public void CopyTo(AppTeamChat instance)
	{
		if (messages != null)
		{
			instance.messages = Pool.Get<List<AppTeamMessage>>();
			for (int i = 0; i < messages.Count; i++)
			{
				AppTeamMessage item = messages[i].Copy();
				instance.messages.Add(item);
			}
		}
		else
		{
			instance.messages = null;
		}
	}

	public AppTeamChat Copy()
	{
		AppTeamChat appTeamChat = Pool.Get<AppTeamChat>();
		CopyTo(appTeamChat);
		return appTeamChat;
	}

	public static AppTeamChat Deserialize(BufferStream stream)
	{
		AppTeamChat appTeamChat = Pool.Get<AppTeamChat>();
		Deserialize(stream, appTeamChat, isDelta: false);
		return appTeamChat;
	}

	public static AppTeamChat DeserializeLengthDelimited(BufferStream stream)
	{
		AppTeamChat appTeamChat = Pool.Get<AppTeamChat>();
		DeserializeLengthDelimited(stream, appTeamChat, isDelta: false);
		return appTeamChat;
	}

	public static AppTeamChat DeserializeLength(BufferStream stream, int length)
	{
		AppTeamChat appTeamChat = Pool.Get<AppTeamChat>();
		DeserializeLength(stream, length, appTeamChat, isDelta: false);
		return appTeamChat;
	}

	public static AppTeamChat Deserialize(byte[] buffer)
	{
		AppTeamChat appTeamChat = Pool.Get<AppTeamChat>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, appTeamChat, isDelta: false);
		return appTeamChat;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, AppTeamChat previous)
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

	public static AppTeamChat Deserialize(BufferStream stream, AppTeamChat instance, bool isDelta)
	{
		if (!isDelta && instance.messages == null)
		{
			instance.messages = Pool.Get<List<AppTeamMessage>>();
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 10:
				instance.messages.Add(AppTeamMessage.DeserializeLengthDelimited(stream));
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

	public static AppTeamChat DeserializeLengthDelimited(BufferStream stream, AppTeamChat instance, bool isDelta)
	{
		if (!isDelta && instance.messages == null)
		{
			instance.messages = Pool.Get<List<AppTeamMessage>>();
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
			case 10:
				instance.messages.Add(AppTeamMessage.DeserializeLengthDelimited(stream));
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

	public static AppTeamChat DeserializeLength(BufferStream stream, int length, AppTeamChat instance, bool isDelta)
	{
		if (!isDelta && instance.messages == null)
		{
			instance.messages = Pool.Get<List<AppTeamMessage>>();
		}
		long num = stream.Position + length;
		while (stream.Position < num)
		{
			int num2 = stream.ReadByte();
			switch (num2)
			{
			case -1:
				throw new EndOfStreamException();
			case 10:
				instance.messages.Add(AppTeamMessage.DeserializeLengthDelimited(stream));
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

	public static void SerializeDelta(BufferStream stream, AppTeamChat instance, AppTeamChat previous)
	{
		if (instance.messages == null)
		{
			return;
		}
		for (int i = 0; i < instance.messages.Count; i++)
		{
			AppTeamMessage appTeamMessage = instance.messages[i];
			stream.WriteByte(10);
			BufferStream.RangeHandle range = stream.GetRange(5);
			int position = stream.Position;
			AppTeamMessage.SerializeDelta(stream, appTeamMessage, appTeamMessage);
			int val = stream.Position - position;
			Span<byte> span = range.GetSpan();
			int num = ProtocolParser.WriteUInt32((uint)val, span, 0);
			if (num < 5)
			{
				span[num - 1] |= 128;
				while (num < 4)
				{
					span[num++] = 128;
				}
				span[4] = 0;
			}
		}
	}

	public static void Serialize(BufferStream stream, AppTeamChat instance)
	{
		if (instance.messages == null)
		{
			return;
		}
		for (int i = 0; i < instance.messages.Count; i++)
		{
			AppTeamMessage instance2 = instance.messages[i];
			stream.WriteByte(10);
			BufferStream.RangeHandle range = stream.GetRange(5);
			int position = stream.Position;
			AppTeamMessage.Serialize(stream, instance2);
			int val = stream.Position - position;
			Span<byte> span = range.GetSpan();
			int num = ProtocolParser.WriteUInt32((uint)val, span, 0);
			if (num < 5)
			{
				span[num - 1] |= 128;
				while (num < 4)
				{
					span[num++] = 128;
				}
				span[4] = 0;
			}
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		if (messages != null)
		{
			for (int i = 0; i < messages.Count; i++)
			{
				messages[i]?.InspectUids(action);
			}
		}
	}
}
