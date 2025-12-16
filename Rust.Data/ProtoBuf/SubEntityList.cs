using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class SubEntityList : IDisposable, Pool.IPooled, IProto<SubEntityList>, IProto
{
	[NonSerialized]
	public List<NetworkableId> subEntityIds;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(SubEntityList instance)
	{
		if (instance.ShouldPool)
		{
			if (instance.subEntityIds != null)
			{
				List<NetworkableId> obj = instance.subEntityIds;
				Pool.FreeUnmanaged(ref obj);
				instance.subEntityIds = obj;
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
			throw new Exception("Trying to dispose SubEntityList with ShouldPool set to false!");
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

	public void CopyTo(SubEntityList instance)
	{
		if (subEntityIds != null)
		{
			instance.subEntityIds = Pool.Get<List<NetworkableId>>();
			for (int i = 0; i < subEntityIds.Count; i++)
			{
				NetworkableId item = subEntityIds[i];
				instance.subEntityIds.Add(item);
			}
		}
		else
		{
			instance.subEntityIds = null;
		}
	}

	public SubEntityList Copy()
	{
		SubEntityList subEntityList = Pool.Get<SubEntityList>();
		CopyTo(subEntityList);
		return subEntityList;
	}

	public static SubEntityList Deserialize(BufferStream stream)
	{
		SubEntityList subEntityList = Pool.Get<SubEntityList>();
		Deserialize(stream, subEntityList, isDelta: false);
		return subEntityList;
	}

	public static SubEntityList DeserializeLengthDelimited(BufferStream stream)
	{
		SubEntityList subEntityList = Pool.Get<SubEntityList>();
		DeserializeLengthDelimited(stream, subEntityList, isDelta: false);
		return subEntityList;
	}

	public static SubEntityList DeserializeLength(BufferStream stream, int length)
	{
		SubEntityList subEntityList = Pool.Get<SubEntityList>();
		DeserializeLength(stream, length, subEntityList, isDelta: false);
		return subEntityList;
	}

	public static SubEntityList Deserialize(byte[] buffer)
	{
		SubEntityList subEntityList = Pool.Get<SubEntityList>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, subEntityList, isDelta: false);
		return subEntityList;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, SubEntityList previous)
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

	public static SubEntityList Deserialize(BufferStream stream, SubEntityList instance, bool isDelta)
	{
		if (!isDelta && instance.subEntityIds == null)
		{
			instance.subEntityIds = Pool.Get<List<NetworkableId>>();
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.subEntityIds.Add(new NetworkableId(ProtocolParser.ReadUInt64(stream)));
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

	public static SubEntityList DeserializeLengthDelimited(BufferStream stream, SubEntityList instance, bool isDelta)
	{
		if (!isDelta && instance.subEntityIds == null)
		{
			instance.subEntityIds = Pool.Get<List<NetworkableId>>();
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
				instance.subEntityIds.Add(new NetworkableId(ProtocolParser.ReadUInt64(stream)));
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

	public static SubEntityList DeserializeLength(BufferStream stream, int length, SubEntityList instance, bool isDelta)
	{
		if (!isDelta && instance.subEntityIds == null)
		{
			instance.subEntityIds = Pool.Get<List<NetworkableId>>();
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
				instance.subEntityIds.Add(new NetworkableId(ProtocolParser.ReadUInt64(stream)));
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

	public static void SerializeDelta(BufferStream stream, SubEntityList instance, SubEntityList previous)
	{
		if (instance.subEntityIds != null)
		{
			for (int i = 0; i < instance.subEntityIds.Count; i++)
			{
				NetworkableId networkableId = instance.subEntityIds[i];
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, networkableId.Value);
			}
		}
	}

	public static void Serialize(BufferStream stream, SubEntityList instance)
	{
		if (instance.subEntityIds != null)
		{
			for (int i = 0; i < instance.subEntityIds.Count; i++)
			{
				NetworkableId networkableId = instance.subEntityIds[i];
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, networkableId.Value);
			}
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		for (int i = 0; i < subEntityIds.Count; i++)
		{
			NetworkableId value = subEntityIds[i];
			action(UidType.NetworkableId, ref value.Value);
			subEntityIds[i] = value;
		}
	}
}
