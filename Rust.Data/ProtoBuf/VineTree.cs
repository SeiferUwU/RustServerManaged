using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class VineTree : IDisposable, Pool.IPooled, IProto<VineTree>, IProto
{
	[NonSerialized]
	public List<NetworkableId> spawnedVines;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(VineTree instance)
	{
		if (instance.ShouldPool)
		{
			if (instance.spawnedVines != null)
			{
				List<NetworkableId> obj = instance.spawnedVines;
				Pool.FreeUnmanaged(ref obj);
				instance.spawnedVines = obj;
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
			throw new Exception("Trying to dispose VineTree with ShouldPool set to false!");
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

	public void CopyTo(VineTree instance)
	{
		if (spawnedVines != null)
		{
			instance.spawnedVines = Pool.Get<List<NetworkableId>>();
			for (int i = 0; i < spawnedVines.Count; i++)
			{
				NetworkableId item = spawnedVines[i];
				instance.spawnedVines.Add(item);
			}
		}
		else
		{
			instance.spawnedVines = null;
		}
	}

	public VineTree Copy()
	{
		VineTree vineTree = Pool.Get<VineTree>();
		CopyTo(vineTree);
		return vineTree;
	}

	public static VineTree Deserialize(BufferStream stream)
	{
		VineTree vineTree = Pool.Get<VineTree>();
		Deserialize(stream, vineTree, isDelta: false);
		return vineTree;
	}

	public static VineTree DeserializeLengthDelimited(BufferStream stream)
	{
		VineTree vineTree = Pool.Get<VineTree>();
		DeserializeLengthDelimited(stream, vineTree, isDelta: false);
		return vineTree;
	}

	public static VineTree DeserializeLength(BufferStream stream, int length)
	{
		VineTree vineTree = Pool.Get<VineTree>();
		DeserializeLength(stream, length, vineTree, isDelta: false);
		return vineTree;
	}

	public static VineTree Deserialize(byte[] buffer)
	{
		VineTree vineTree = Pool.Get<VineTree>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, vineTree, isDelta: false);
		return vineTree;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, VineTree previous)
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

	public static VineTree Deserialize(BufferStream stream, VineTree instance, bool isDelta)
	{
		if (!isDelta && instance.spawnedVines == null)
		{
			instance.spawnedVines = Pool.Get<List<NetworkableId>>();
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.spawnedVines.Add(new NetworkableId(ProtocolParser.ReadUInt64(stream)));
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

	public static VineTree DeserializeLengthDelimited(BufferStream stream, VineTree instance, bool isDelta)
	{
		if (!isDelta && instance.spawnedVines == null)
		{
			instance.spawnedVines = Pool.Get<List<NetworkableId>>();
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
				instance.spawnedVines.Add(new NetworkableId(ProtocolParser.ReadUInt64(stream)));
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

	public static VineTree DeserializeLength(BufferStream stream, int length, VineTree instance, bool isDelta)
	{
		if (!isDelta && instance.spawnedVines == null)
		{
			instance.spawnedVines = Pool.Get<List<NetworkableId>>();
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
				instance.spawnedVines.Add(new NetworkableId(ProtocolParser.ReadUInt64(stream)));
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

	public static void SerializeDelta(BufferStream stream, VineTree instance, VineTree previous)
	{
		if (instance.spawnedVines != null)
		{
			for (int i = 0; i < instance.spawnedVines.Count; i++)
			{
				NetworkableId networkableId = instance.spawnedVines[i];
				stream.WriteByte(8);
				ProtocolParser.WriteUInt64(stream, networkableId.Value);
			}
		}
	}

	public static void Serialize(BufferStream stream, VineTree instance)
	{
		if (instance.spawnedVines != null)
		{
			for (int i = 0; i < instance.spawnedVines.Count; i++)
			{
				NetworkableId networkableId = instance.spawnedVines[i];
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
		for (int i = 0; i < spawnedVines.Count; i++)
		{
			NetworkableId value = spawnedVines[i];
			action(UidType.NetworkableId, ref value.Value);
			spawnedVines[i] = value;
		}
	}
}
