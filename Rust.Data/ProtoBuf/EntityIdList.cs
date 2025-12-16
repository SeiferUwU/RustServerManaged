using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class EntityIdList : IDisposable, Pool.IPooled, IProto<EntityIdList>, IProto
{
	[NonSerialized]
	public List<NetworkableId> entityIds;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(EntityIdList instance)
	{
		if (instance.ShouldPool)
		{
			if (instance.entityIds != null)
			{
				List<NetworkableId> obj = instance.entityIds;
				Pool.FreeUnmanaged(ref obj);
				instance.entityIds = obj;
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
			throw new Exception("Trying to dispose EntityIdList with ShouldPool set to false!");
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

	public void CopyTo(EntityIdList instance)
	{
		if (entityIds != null)
		{
			instance.entityIds = Pool.Get<List<NetworkableId>>();
			for (int i = 0; i < entityIds.Count; i++)
			{
				NetworkableId item = entityIds[i];
				instance.entityIds.Add(item);
			}
		}
		else
		{
			instance.entityIds = null;
		}
	}

	public EntityIdList Copy()
	{
		EntityIdList entityIdList = Pool.Get<EntityIdList>();
		CopyTo(entityIdList);
		return entityIdList;
	}

	public static EntityIdList Deserialize(BufferStream stream)
	{
		EntityIdList entityIdList = Pool.Get<EntityIdList>();
		Deserialize(stream, entityIdList, isDelta: false);
		return entityIdList;
	}

	public static EntityIdList DeserializeLengthDelimited(BufferStream stream)
	{
		EntityIdList entityIdList = Pool.Get<EntityIdList>();
		DeserializeLengthDelimited(stream, entityIdList, isDelta: false);
		return entityIdList;
	}

	public static EntityIdList DeserializeLength(BufferStream stream, int length)
	{
		EntityIdList entityIdList = Pool.Get<EntityIdList>();
		DeserializeLength(stream, length, entityIdList, isDelta: false);
		return entityIdList;
	}

	public static EntityIdList Deserialize(byte[] buffer)
	{
		EntityIdList entityIdList = Pool.Get<EntityIdList>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, entityIdList, isDelta: false);
		return entityIdList;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, EntityIdList previous)
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

	public static EntityIdList Deserialize(BufferStream stream, EntityIdList instance, bool isDelta)
	{
		if (!isDelta && instance.entityIds == null)
		{
			instance.entityIds = Pool.Get<List<NetworkableId>>();
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.entityIds.Add(new NetworkableId(ProtocolParser.ReadUInt64(stream)));
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

	public static EntityIdList DeserializeLengthDelimited(BufferStream stream, EntityIdList instance, bool isDelta)
	{
		if (!isDelta && instance.entityIds == null)
		{
			instance.entityIds = Pool.Get<List<NetworkableId>>();
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
				instance.entityIds.Add(new NetworkableId(ProtocolParser.ReadUInt64(stream)));
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

	public static EntityIdList DeserializeLength(BufferStream stream, int length, EntityIdList instance, bool isDelta)
	{
		if (!isDelta && instance.entityIds == null)
		{
			instance.entityIds = Pool.Get<List<NetworkableId>>();
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
				instance.entityIds.Add(new NetworkableId(ProtocolParser.ReadUInt64(stream)));
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

	public static void SerializeDelta(BufferStream stream, EntityIdList instance, EntityIdList previous)
	{
		if (instance.entityIds != null)
		{
			for (int i = 0; i < instance.entityIds.Count; i++)
			{
				NetworkableId networkableId = instance.entityIds[i];
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, networkableId.Value);
			}
		}
	}

	public static void Serialize(BufferStream stream, EntityIdList instance)
	{
		if (instance.entityIds != null)
		{
			for (int i = 0; i < instance.entityIds.Count; i++)
			{
				NetworkableId networkableId = instance.entityIds[i];
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
		for (int i = 0; i < entityIds.Count; i++)
		{
			NetworkableId value = entityIds[i];
			action(UidType.NetworkableId, ref value.Value);
			entityIds[i] = value;
		}
	}
}
