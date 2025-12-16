using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class TreeRespawn : IDisposable, Pool.IPooled, IProto<TreeRespawn>, IProto
{
	[NonSerialized]
	public float timeToRespawn;

	[NonSerialized]
	public int treeIndex;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(TreeRespawn instance)
	{
		if (instance.ShouldPool)
		{
			instance.timeToRespawn = 0f;
			instance.treeIndex = 0;
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
			throw new Exception("Trying to dispose TreeRespawn with ShouldPool set to false!");
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

	public void CopyTo(TreeRespawn instance)
	{
		instance.timeToRespawn = timeToRespawn;
		instance.treeIndex = treeIndex;
	}

	public TreeRespawn Copy()
	{
		TreeRespawn treeRespawn = Pool.Get<TreeRespawn>();
		CopyTo(treeRespawn);
		return treeRespawn;
	}

	public static TreeRespawn Deserialize(BufferStream stream)
	{
		TreeRespawn treeRespawn = Pool.Get<TreeRespawn>();
		Deserialize(stream, treeRespawn, isDelta: false);
		return treeRespawn;
	}

	public static TreeRespawn DeserializeLengthDelimited(BufferStream stream)
	{
		TreeRespawn treeRespawn = Pool.Get<TreeRespawn>();
		DeserializeLengthDelimited(stream, treeRespawn, isDelta: false);
		return treeRespawn;
	}

	public static TreeRespawn DeserializeLength(BufferStream stream, int length)
	{
		TreeRespawn treeRespawn = Pool.Get<TreeRespawn>();
		DeserializeLength(stream, length, treeRespawn, isDelta: false);
		return treeRespawn;
	}

	public static TreeRespawn Deserialize(byte[] buffer)
	{
		TreeRespawn treeRespawn = Pool.Get<TreeRespawn>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, treeRespawn, isDelta: false);
		return treeRespawn;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, TreeRespawn previous)
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

	public static TreeRespawn Deserialize(BufferStream stream, TreeRespawn instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 13:
				instance.timeToRespawn = ProtocolParser.ReadSingle(stream);
				continue;
			case 16:
				instance.treeIndex = (int)ProtocolParser.ReadUInt64(stream);
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

	public static TreeRespawn DeserializeLengthDelimited(BufferStream stream, TreeRespawn instance, bool isDelta)
	{
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
				instance.timeToRespawn = ProtocolParser.ReadSingle(stream);
				continue;
			case 16:
				instance.treeIndex = (int)ProtocolParser.ReadUInt64(stream);
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

	public static TreeRespawn DeserializeLength(BufferStream stream, int length, TreeRespawn instance, bool isDelta)
	{
		long num = stream.Position + length;
		while (stream.Position < num)
		{
			int num2 = stream.ReadByte();
			switch (num2)
			{
			case -1:
				throw new EndOfStreamException();
			case 13:
				instance.timeToRespawn = ProtocolParser.ReadSingle(stream);
				continue;
			case 16:
				instance.treeIndex = (int)ProtocolParser.ReadUInt64(stream);
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

	public static void SerializeDelta(BufferStream stream, TreeRespawn instance, TreeRespawn previous)
	{
		if (instance.timeToRespawn != previous.timeToRespawn)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.timeToRespawn);
		}
		if (instance.treeIndex != previous.treeIndex)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.treeIndex);
		}
	}

	public static void Serialize(BufferStream stream, TreeRespawn instance)
	{
		if (instance.timeToRespawn != 0f)
		{
			stream.WriteByte(13);
			ProtocolParser.WriteSingle(stream, instance.timeToRespawn);
		}
		if (instance.treeIndex != 0)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.treeIndex);
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
