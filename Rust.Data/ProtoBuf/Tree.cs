using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class Tree : IDisposable, Pool.IPooled, IProto<Tree>, IProto
{
	[NonSerialized]
	public NetworkableId netId;

	[NonSerialized]
	public uint prefabId;

	[NonSerialized]
	public Half3 position;

	[NonSerialized]
	public float scale;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(Tree instance)
	{
		if (instance.ShouldPool)
		{
			instance.netId = default(NetworkableId);
			instance.prefabId = 0u;
			instance.position = default(Half3);
			instance.scale = 0f;
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
			throw new Exception("Trying to dispose Tree with ShouldPool set to false!");
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

	public void CopyTo(Tree instance)
	{
		instance.netId = netId;
		instance.prefabId = prefabId;
		instance.position = position;
		instance.scale = scale;
	}

	public Tree Copy()
	{
		Tree tree = Pool.Get<Tree>();
		CopyTo(tree);
		return tree;
	}

	public static Tree Deserialize(BufferStream stream)
	{
		Tree tree = Pool.Get<Tree>();
		Deserialize(stream, tree, isDelta: false);
		return tree;
	}

	public static Tree DeserializeLengthDelimited(BufferStream stream)
	{
		Tree tree = Pool.Get<Tree>();
		DeserializeLengthDelimited(stream, tree, isDelta: false);
		return tree;
	}

	public static Tree DeserializeLength(BufferStream stream, int length)
	{
		Tree tree = Pool.Get<Tree>();
		DeserializeLength(stream, length, tree, isDelta: false);
		return tree;
	}

	public static Tree Deserialize(byte[] buffer)
	{
		Tree tree = Pool.Get<Tree>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, tree, isDelta: false);
		return tree;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, Tree previous)
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

	public static Tree Deserialize(BufferStream stream, Tree instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.netId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 16:
				instance.prefabId = ProtocolParser.ReadUInt32(stream);
				continue;
			case 26:
				Half3.DeserializeLengthDelimited(stream, ref instance.position, isDelta);
				continue;
			case 37:
				instance.scale = ProtocolParser.ReadSingle(stream);
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

	public static Tree DeserializeLengthDelimited(BufferStream stream, Tree instance, bool isDelta)
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
				instance.netId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 16:
				instance.prefabId = ProtocolParser.ReadUInt32(stream);
				continue;
			case 26:
				Half3.DeserializeLengthDelimited(stream, ref instance.position, isDelta);
				continue;
			case 37:
				instance.scale = ProtocolParser.ReadSingle(stream);
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

	public static Tree DeserializeLength(BufferStream stream, int length, Tree instance, bool isDelta)
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
				instance.netId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 16:
				instance.prefabId = ProtocolParser.ReadUInt32(stream);
				continue;
			case 26:
				Half3.DeserializeLengthDelimited(stream, ref instance.position, isDelta);
				continue;
			case 37:
				instance.scale = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, Tree instance, Tree previous)
	{
		stream.WriteByte(8);
		ProtocolParser.WriteUInt64(stream, instance.netId.Value);
		if (instance.prefabId != previous.prefabId)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.prefabId);
		}
		stream.WriteByte(26);
		BufferStream.RangeHandle range = stream.GetRange(1);
		int num = stream.Position;
		Half3.SerializeDelta(stream, instance.position, previous.position);
		int num2 = stream.Position - num;
		if (num2 > 127)
		{
			throw new InvalidOperationException("Not enough space was reserved for the length prefix of field position (ProtoBuf.Half3)");
		}
		Span<byte> span = range.GetSpan();
		ProtocolParser.WriteUInt32((uint)num2, span, 0);
		if (instance.scale != previous.scale)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.scale);
		}
	}

	public static void Serialize(BufferStream stream, Tree instance)
	{
		if (instance.netId != default(NetworkableId))
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.netId.Value);
		}
		if (instance.prefabId != 0)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.prefabId);
		}
		if (instance.position != default(Half3))
		{
			stream.WriteByte(26);
			BufferStream.RangeHandle range = stream.GetRange(1);
			int num = stream.Position;
			Half3.Serialize(stream, instance.position);
			int num2 = stream.Position - num;
			if (num2 > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field position (ProtoBuf.Half3)");
			}
			Span<byte> span = range.GetSpan();
			ProtocolParser.WriteUInt32((uint)num2, span, 0);
		}
		if (instance.scale != 0f)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.scale);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref netId.Value);
		position.InspectUids(action);
	}
}
