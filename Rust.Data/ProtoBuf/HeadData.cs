using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class HeadData : IDisposable, Pool.IPooled, IProto<HeadData>, IProto
{
	[NonSerialized]
	public uint entitySource;

	[NonSerialized]
	public string playerName;

	[NonSerialized]
	public ulong playerId;

	[NonSerialized]
	public List<int> clothing;

	[NonSerialized]
	public uint count;

	[NonSerialized]
	public int horseBreed;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(HeadData instance)
	{
		if (instance.ShouldPool)
		{
			instance.entitySource = 0u;
			instance.playerName = string.Empty;
			instance.playerId = 0uL;
			if (instance.clothing != null)
			{
				List<int> obj = instance.clothing;
				Pool.FreeUnmanaged(ref obj);
				instance.clothing = obj;
			}
			instance.count = 0u;
			instance.horseBreed = 0;
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
			throw new Exception("Trying to dispose HeadData with ShouldPool set to false!");
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

	public void CopyTo(HeadData instance)
	{
		instance.entitySource = entitySource;
		instance.playerName = playerName;
		instance.playerId = playerId;
		if (clothing != null)
		{
			instance.clothing = Pool.Get<List<int>>();
			for (int i = 0; i < clothing.Count; i++)
			{
				int item = clothing[i];
				instance.clothing.Add(item);
			}
		}
		else
		{
			instance.clothing = null;
		}
		instance.count = count;
		instance.horseBreed = horseBreed;
	}

	public HeadData Copy()
	{
		HeadData headData = Pool.Get<HeadData>();
		CopyTo(headData);
		return headData;
	}

	public static HeadData Deserialize(BufferStream stream)
	{
		HeadData headData = Pool.Get<HeadData>();
		Deserialize(stream, headData, isDelta: false);
		return headData;
	}

	public static HeadData DeserializeLengthDelimited(BufferStream stream)
	{
		HeadData headData = Pool.Get<HeadData>();
		DeserializeLengthDelimited(stream, headData, isDelta: false);
		return headData;
	}

	public static HeadData DeserializeLength(BufferStream stream, int length)
	{
		HeadData headData = Pool.Get<HeadData>();
		DeserializeLength(stream, length, headData, isDelta: false);
		return headData;
	}

	public static HeadData Deserialize(byte[] buffer)
	{
		HeadData headData = Pool.Get<HeadData>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, headData, isDelta: false);
		return headData;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, HeadData previous)
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

	public static HeadData Deserialize(BufferStream stream, HeadData instance, bool isDelta)
	{
		if (!isDelta && instance.clothing == null)
		{
			instance.clothing = Pool.Get<List<int>>();
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.entitySource = ProtocolParser.ReadUInt32(stream);
				continue;
			case 18:
				instance.playerName = ProtocolParser.ReadString(stream);
				continue;
			case 24:
				instance.playerId = ProtocolParser.ReadUInt64(stream);
				continue;
			case 32:
				instance.clothing.Add((int)ProtocolParser.ReadUInt64(stream));
				continue;
			case 40:
				instance.count = ProtocolParser.ReadUInt32(stream);
				continue;
			case 48:
				instance.horseBreed = (int)ProtocolParser.ReadUInt64(stream);
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

	public static HeadData DeserializeLengthDelimited(BufferStream stream, HeadData instance, bool isDelta)
	{
		if (!isDelta && instance.clothing == null)
		{
			instance.clothing = Pool.Get<List<int>>();
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
				instance.entitySource = ProtocolParser.ReadUInt32(stream);
				continue;
			case 18:
				instance.playerName = ProtocolParser.ReadString(stream);
				continue;
			case 24:
				instance.playerId = ProtocolParser.ReadUInt64(stream);
				continue;
			case 32:
				instance.clothing.Add((int)ProtocolParser.ReadUInt64(stream));
				continue;
			case 40:
				instance.count = ProtocolParser.ReadUInt32(stream);
				continue;
			case 48:
				instance.horseBreed = (int)ProtocolParser.ReadUInt64(stream);
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

	public static HeadData DeserializeLength(BufferStream stream, int length, HeadData instance, bool isDelta)
	{
		if (!isDelta && instance.clothing == null)
		{
			instance.clothing = Pool.Get<List<int>>();
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
				instance.entitySource = ProtocolParser.ReadUInt32(stream);
				continue;
			case 18:
				instance.playerName = ProtocolParser.ReadString(stream);
				continue;
			case 24:
				instance.playerId = ProtocolParser.ReadUInt64(stream);
				continue;
			case 32:
				instance.clothing.Add((int)ProtocolParser.ReadUInt64(stream));
				continue;
			case 40:
				instance.count = ProtocolParser.ReadUInt32(stream);
				continue;
			case 48:
				instance.horseBreed = (int)ProtocolParser.ReadUInt64(stream);
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

	public static void SerializeDelta(BufferStream stream, HeadData instance, HeadData previous)
	{
		if (instance.entitySource != previous.entitySource)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.entitySource);
		}
		if (instance.playerName != null && instance.playerName != previous.playerName)
		{
			stream.WriteByte(18);
			ProtocolParser.WriteString(stream, instance.playerName);
		}
		if (instance.playerId != previous.playerId)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, instance.playerId);
		}
		if (instance.clothing != null)
		{
			for (int i = 0; i < instance.clothing.Count; i++)
			{
				int num = instance.clothing[i];
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)num);
			}
		}
		if (instance.count != previous.count)
		{
			stream.WriteByte(40);
			ProtocolParser.WriteUInt32(stream, instance.count);
		}
		if (instance.horseBreed != previous.horseBreed)
		{
			stream.WriteByte(48);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.horseBreed);
		}
	}

	public static void Serialize(BufferStream stream, HeadData instance)
	{
		if (instance.entitySource != 0)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.entitySource);
		}
		if (instance.playerName != null)
		{
			stream.WriteByte(18);
			ProtocolParser.WriteString(stream, instance.playerName);
		}
		if (instance.playerId != 0L)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, instance.playerId);
		}
		if (instance.clothing != null)
		{
			for (int i = 0; i < instance.clothing.Count; i++)
			{
				int num = instance.clothing[i];
				stream.WriteByte(32);
				ProtocolParser.WriteUInt64(stream, (ulong)num);
			}
		}
		if (instance.count != 0)
		{
			stream.WriteByte(40);
			ProtocolParser.WriteUInt32(stream, instance.count);
		}
		if (instance.horseBreed != 0)
		{
			stream.WriteByte(48);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.horseBreed);
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
