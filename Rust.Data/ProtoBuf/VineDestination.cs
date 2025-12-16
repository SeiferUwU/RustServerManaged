using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class VineDestination : IDisposable, Pool.IPooled, IProto<VineDestination>, IProto
{
	[NonSerialized]
	public NetworkableId targetTree;

	[NonSerialized]
	public int index;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(VineDestination instance)
	{
		if (instance.ShouldPool)
		{
			instance.targetTree = default(NetworkableId);
			instance.index = 0;
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
			throw new Exception("Trying to dispose VineDestination with ShouldPool set to false!");
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

	public void CopyTo(VineDestination instance)
	{
		instance.targetTree = targetTree;
		instance.index = index;
	}

	public VineDestination Copy()
	{
		VineDestination vineDestination = Pool.Get<VineDestination>();
		CopyTo(vineDestination);
		return vineDestination;
	}

	public static VineDestination Deserialize(BufferStream stream)
	{
		VineDestination vineDestination = Pool.Get<VineDestination>();
		Deserialize(stream, vineDestination, isDelta: false);
		return vineDestination;
	}

	public static VineDestination DeserializeLengthDelimited(BufferStream stream)
	{
		VineDestination vineDestination = Pool.Get<VineDestination>();
		DeserializeLengthDelimited(stream, vineDestination, isDelta: false);
		return vineDestination;
	}

	public static VineDestination DeserializeLength(BufferStream stream, int length)
	{
		VineDestination vineDestination = Pool.Get<VineDestination>();
		DeserializeLength(stream, length, vineDestination, isDelta: false);
		return vineDestination;
	}

	public static VineDestination Deserialize(byte[] buffer)
	{
		VineDestination vineDestination = Pool.Get<VineDestination>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, vineDestination, isDelta: false);
		return vineDestination;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, VineDestination previous)
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

	public static VineDestination Deserialize(BufferStream stream, VineDestination instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.targetTree = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 16:
				instance.index = (int)ProtocolParser.ReadUInt64(stream);
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

	public static VineDestination DeserializeLengthDelimited(BufferStream stream, VineDestination instance, bool isDelta)
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
			case 8:
				instance.targetTree = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 16:
				instance.index = (int)ProtocolParser.ReadUInt64(stream);
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

	public static VineDestination DeserializeLength(BufferStream stream, int length, VineDestination instance, bool isDelta)
	{
		long num = stream.Position + length;
		while (stream.Position < num)
		{
			int num2 = stream.ReadByte();
			switch (num2)
			{
			case -1:
				throw new EndOfStreamException();
			case 8:
				instance.targetTree = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 16:
				instance.index = (int)ProtocolParser.ReadUInt64(stream);
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

	public static void SerializeDelta(BufferStream stream, VineDestination instance, VineDestination previous)
	{
		stream.WriteByte(8);
		ProtocolParser.WriteUInt64(stream, instance.targetTree.Value);
		if (instance.index != previous.index)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.index);
		}
	}

	public static void Serialize(BufferStream stream, VineDestination instance)
	{
		if (instance.targetTree != default(NetworkableId))
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.targetTree.Value);
		}
		if (instance.index != 0)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.index);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref targetTree.Value);
	}
}
