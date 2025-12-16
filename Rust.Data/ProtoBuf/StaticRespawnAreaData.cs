using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class StaticRespawnAreaData : IDisposable, Pool.IPooled, IProto<StaticRespawnAreaData>, IProto
{
	[NonSerialized]
	public List<ulong> authorizedUsers;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(StaticRespawnAreaData instance)
	{
		if (instance.ShouldPool)
		{
			if (instance.authorizedUsers != null)
			{
				List<ulong> obj = instance.authorizedUsers;
				Pool.FreeUnmanaged(ref obj);
				instance.authorizedUsers = obj;
			}
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
			throw new Exception("Trying to dispose StaticRespawnAreaData with ShouldPool set to false!");
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

	public void CopyTo(StaticRespawnAreaData instance)
	{
		if (authorizedUsers != null)
		{
			instance.authorizedUsers = Pool.Get<List<ulong>>();
			for (int i = 0; i < authorizedUsers.Count; i++)
			{
				ulong item = authorizedUsers[i];
				instance.authorizedUsers.Add(item);
			}
		}
		else
		{
			instance.authorizedUsers = null;
		}
	}

	public StaticRespawnAreaData Copy()
	{
		StaticRespawnAreaData staticRespawnAreaData = Pool.Get<StaticRespawnAreaData>();
		CopyTo(staticRespawnAreaData);
		return staticRespawnAreaData;
	}

	public static StaticRespawnAreaData Deserialize(BufferStream stream)
	{
		StaticRespawnAreaData staticRespawnAreaData = Pool.Get<StaticRespawnAreaData>();
		Deserialize(stream, staticRespawnAreaData, isDelta: false);
		return staticRespawnAreaData;
	}

	public static StaticRespawnAreaData DeserializeLengthDelimited(BufferStream stream)
	{
		StaticRespawnAreaData staticRespawnAreaData = Pool.Get<StaticRespawnAreaData>();
		DeserializeLengthDelimited(stream, staticRespawnAreaData, isDelta: false);
		return staticRespawnAreaData;
	}

	public static StaticRespawnAreaData DeserializeLength(BufferStream stream, int length)
	{
		StaticRespawnAreaData staticRespawnAreaData = Pool.Get<StaticRespawnAreaData>();
		DeserializeLength(stream, length, staticRespawnAreaData, isDelta: false);
		return staticRespawnAreaData;
	}

	public static StaticRespawnAreaData Deserialize(byte[] buffer)
	{
		StaticRespawnAreaData staticRespawnAreaData = Pool.Get<StaticRespawnAreaData>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, staticRespawnAreaData, isDelta: false);
		return staticRespawnAreaData;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, StaticRespawnAreaData previous)
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

	public static StaticRespawnAreaData Deserialize(BufferStream stream, StaticRespawnAreaData instance, bool isDelta)
	{
		if (!isDelta && instance.authorizedUsers == null)
		{
			instance.authorizedUsers = Pool.Get<List<ulong>>();
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.authorizedUsers.Add(ProtocolParser.ReadUInt64(stream));
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

	public static StaticRespawnAreaData DeserializeLengthDelimited(BufferStream stream, StaticRespawnAreaData instance, bool isDelta)
	{
		if (!isDelta && instance.authorizedUsers == null)
		{
			instance.authorizedUsers = Pool.Get<List<ulong>>();
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
				instance.authorizedUsers.Add(ProtocolParser.ReadUInt64(stream));
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

	public static StaticRespawnAreaData DeserializeLength(BufferStream stream, int length, StaticRespawnAreaData instance, bool isDelta)
	{
		if (!isDelta && instance.authorizedUsers == null)
		{
			instance.authorizedUsers = Pool.Get<List<ulong>>();
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
				instance.authorizedUsers.Add(ProtocolParser.ReadUInt64(stream));
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

	public static void SerializeDelta(BufferStream stream, StaticRespawnAreaData instance, StaticRespawnAreaData previous)
	{
		if (instance.authorizedUsers != null)
		{
			for (int i = 0; i < instance.authorizedUsers.Count; i++)
			{
				ulong val = instance.authorizedUsers[i];
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, val);
			}
		}
	}

	public static void Serialize(BufferStream stream, StaticRespawnAreaData instance)
	{
		if (instance.authorizedUsers != null)
		{
			for (int i = 0; i < instance.authorizedUsers.Count; i++)
			{
				ulong val = instance.authorizedUsers[i];
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, val);
			}
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
