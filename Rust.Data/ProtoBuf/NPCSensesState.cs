using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class NPCSensesState : IDisposable, Pool.IPooled, IProto<NPCSensesState>, IProto
{
	[NonSerialized]
	public NetworkableId targetEntityId;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(NPCSensesState instance)
	{
		if (instance.ShouldPool)
		{
			instance.targetEntityId = default(NetworkableId);
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
			throw new Exception("Trying to dispose NPCSensesState with ShouldPool set to false!");
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

	public void CopyTo(NPCSensesState instance)
	{
		instance.targetEntityId = targetEntityId;
	}

	public NPCSensesState Copy()
	{
		NPCSensesState nPCSensesState = Pool.Get<NPCSensesState>();
		CopyTo(nPCSensesState);
		return nPCSensesState;
	}

	public static NPCSensesState Deserialize(BufferStream stream)
	{
		NPCSensesState nPCSensesState = Pool.Get<NPCSensesState>();
		Deserialize(stream, nPCSensesState, isDelta: false);
		return nPCSensesState;
	}

	public static NPCSensesState DeserializeLengthDelimited(BufferStream stream)
	{
		NPCSensesState nPCSensesState = Pool.Get<NPCSensesState>();
		DeserializeLengthDelimited(stream, nPCSensesState, isDelta: false);
		return nPCSensesState;
	}

	public static NPCSensesState DeserializeLength(BufferStream stream, int length)
	{
		NPCSensesState nPCSensesState = Pool.Get<NPCSensesState>();
		DeserializeLength(stream, length, nPCSensesState, isDelta: false);
		return nPCSensesState;
	}

	public static NPCSensesState Deserialize(byte[] buffer)
	{
		NPCSensesState nPCSensesState = Pool.Get<NPCSensesState>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, nPCSensesState, isDelta: false);
		return nPCSensesState;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, NPCSensesState previous)
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

	public static NPCSensesState Deserialize(BufferStream stream, NPCSensesState instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.targetEntityId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static NPCSensesState DeserializeLengthDelimited(BufferStream stream, NPCSensesState instance, bool isDelta)
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
				instance.targetEntityId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static NPCSensesState DeserializeLength(BufferStream stream, int length, NPCSensesState instance, bool isDelta)
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
				instance.targetEntityId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static void SerializeDelta(BufferStream stream, NPCSensesState instance, NPCSensesState previous)
	{
		stream.WriteByte(8);
		ProtocolParser.WriteUInt64(stream, instance.targetEntityId.Value);
	}

	public static void Serialize(BufferStream stream, NPCSensesState instance)
	{
		if (instance.targetEntityId != default(NetworkableId))
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, instance.targetEntityId.Value);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref targetEntityId.Value);
	}
}
