using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class PersistantPlayer : IDisposable, Pool.IPooled, IProto<PersistantPlayer>, IProto
{
	[NonSerialized]
	public List<int> unlockedItems;

	[NonSerialized]
	public int protocolVersion;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(PersistantPlayer instance)
	{
		if (instance.ShouldPool)
		{
			if (instance.unlockedItems != null)
			{
				List<int> obj = instance.unlockedItems;
				Pool.FreeUnmanaged(ref obj);
				instance.unlockedItems = obj;
			}
			instance.protocolVersion = 0;
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
			throw new Exception("Trying to dispose PersistantPlayer with ShouldPool set to false!");
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

	public void CopyTo(PersistantPlayer instance)
	{
		if (unlockedItems != null)
		{
			instance.unlockedItems = Pool.Get<List<int>>();
			for (int i = 0; i < unlockedItems.Count; i++)
			{
				int item = unlockedItems[i];
				instance.unlockedItems.Add(item);
			}
		}
		else
		{
			instance.unlockedItems = null;
		}
		instance.protocolVersion = protocolVersion;
	}

	public PersistantPlayer Copy()
	{
		PersistantPlayer persistantPlayer = Pool.Get<PersistantPlayer>();
		CopyTo(persistantPlayer);
		return persistantPlayer;
	}

	public static PersistantPlayer Deserialize(BufferStream stream)
	{
		PersistantPlayer persistantPlayer = Pool.Get<PersistantPlayer>();
		Deserialize(stream, persistantPlayer, isDelta: false);
		return persistantPlayer;
	}

	public static PersistantPlayer DeserializeLengthDelimited(BufferStream stream)
	{
		PersistantPlayer persistantPlayer = Pool.Get<PersistantPlayer>();
		DeserializeLengthDelimited(stream, persistantPlayer, isDelta: false);
		return persistantPlayer;
	}

	public static PersistantPlayer DeserializeLength(BufferStream stream, int length)
	{
		PersistantPlayer persistantPlayer = Pool.Get<PersistantPlayer>();
		DeserializeLength(stream, length, persistantPlayer, isDelta: false);
		return persistantPlayer;
	}

	public static PersistantPlayer Deserialize(byte[] buffer)
	{
		PersistantPlayer persistantPlayer = Pool.Get<PersistantPlayer>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, persistantPlayer, isDelta: false);
		return persistantPlayer;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, PersistantPlayer previous)
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

	public static PersistantPlayer Deserialize(BufferStream stream, PersistantPlayer instance, bool isDelta)
	{
		if (!isDelta && instance.unlockedItems == null)
		{
			instance.unlockedItems = Pool.Get<List<int>>();
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 24:
				instance.unlockedItems.Add((int)ProtocolParser.ReadUInt64(stream));
				continue;
			case -1:
			case 0:
				return instance;
			}
			Key key = ProtocolParser.ReadKey((byte)num, stream);
			if (key.Field == 100)
			{
				if (key.WireType == Wire.Varint)
				{
					instance.protocolVersion = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			else
			{
				ProtocolParser.SkipKey(stream, key);
			}
		}
	}

	public static PersistantPlayer DeserializeLengthDelimited(BufferStream stream, PersistantPlayer instance, bool isDelta)
	{
		if (!isDelta && instance.unlockedItems == null)
		{
			instance.unlockedItems = Pool.Get<List<int>>();
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
			case 24:
				instance.unlockedItems.Add((int)ProtocolParser.ReadUInt64(stream));
				continue;
			}
			Key key = ProtocolParser.ReadKey((byte)num2, stream);
			if (key.Field == 100)
			{
				if (key.WireType == Wire.Varint)
				{
					instance.protocolVersion = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			else
			{
				ProtocolParser.SkipKey(stream, key);
			}
		}
		if (stream.Position != num)
		{
			throw new ProtocolBufferException("Read past max limit");
		}
		return instance;
	}

	public static PersistantPlayer DeserializeLength(BufferStream stream, int length, PersistantPlayer instance, bool isDelta)
	{
		if (!isDelta && instance.unlockedItems == null)
		{
			instance.unlockedItems = Pool.Get<List<int>>();
		}
		long num = stream.Position + length;
		while (stream.Position < num)
		{
			int num2 = stream.ReadByte();
			switch (num2)
			{
			case -1:
				throw new EndOfStreamException();
			case 24:
				instance.unlockedItems.Add((int)ProtocolParser.ReadUInt64(stream));
				continue;
			}
			Key key = ProtocolParser.ReadKey((byte)num2, stream);
			if (key.Field == 100)
			{
				if (key.WireType == Wire.Varint)
				{
					instance.protocolVersion = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			else
			{
				ProtocolParser.SkipKey(stream, key);
			}
		}
		if (stream.Position != num)
		{
			throw new ProtocolBufferException("Read past max limit");
		}
		return instance;
	}

	public static void SerializeDelta(BufferStream stream, PersistantPlayer instance, PersistantPlayer previous)
	{
		if (instance.unlockedItems != null)
		{
			for (int i = 0; i < instance.unlockedItems.Count; i++)
			{
				int num = instance.unlockedItems[i];
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)num);
			}
		}
		if (instance.protocolVersion != previous.protocolVersion)
		{
			stream.WriteByte(160);
			stream.WriteByte(6);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.protocolVersion);
		}
	}

	public static void Serialize(BufferStream stream, PersistantPlayer instance)
	{
		if (instance.unlockedItems != null)
		{
			for (int i = 0; i < instance.unlockedItems.Count; i++)
			{
				int num = instance.unlockedItems[i];
				stream.WriteByte(24);
				ProtocolParser.WriteUInt64(stream, (ulong)num);
			}
		}
		if (instance.protocolVersion != 0)
		{
			stream.WriteByte(160);
			stream.WriteByte(6);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.protocolVersion);
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
