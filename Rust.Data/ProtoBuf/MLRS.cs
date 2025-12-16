using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;
using UnityEngine;

namespace ProtoBuf;

public class MLRS : IDisposable, Pool.IPooled, IProto<MLRS>, IProto
{
	[NonSerialized]
	public Vector3 targetPos;

	[NonSerialized]
	public Vector3 curHitPos;

	[NonSerialized]
	public NetworkableId rocketStorageID;

	[NonSerialized]
	public NetworkableId dashboardStorageID;

	[NonSerialized]
	public uint ammoCount;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(MLRS instance)
	{
		if (instance.ShouldPool)
		{
			instance.targetPos = default(Vector3);
			instance.curHitPos = default(Vector3);
			instance.rocketStorageID = default(NetworkableId);
			instance.dashboardStorageID = default(NetworkableId);
			instance.ammoCount = 0u;
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
			throw new Exception("Trying to dispose MLRS with ShouldPool set to false!");
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

	public void CopyTo(MLRS instance)
	{
		instance.targetPos = targetPos;
		instance.curHitPos = curHitPos;
		instance.rocketStorageID = rocketStorageID;
		instance.dashboardStorageID = dashboardStorageID;
		instance.ammoCount = ammoCount;
	}

	public MLRS Copy()
	{
		MLRS mLRS = Pool.Get<MLRS>();
		CopyTo(mLRS);
		return mLRS;
	}

	public static MLRS Deserialize(BufferStream stream)
	{
		MLRS mLRS = Pool.Get<MLRS>();
		Deserialize(stream, mLRS, isDelta: false);
		return mLRS;
	}

	public static MLRS DeserializeLengthDelimited(BufferStream stream)
	{
		MLRS mLRS = Pool.Get<MLRS>();
		DeserializeLengthDelimited(stream, mLRS, isDelta: false);
		return mLRS;
	}

	public static MLRS DeserializeLength(BufferStream stream, int length)
	{
		MLRS mLRS = Pool.Get<MLRS>();
		DeserializeLength(stream, length, mLRS, isDelta: false);
		return mLRS;
	}

	public static MLRS Deserialize(byte[] buffer)
	{
		MLRS mLRS = Pool.Get<MLRS>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, mLRS, isDelta: false);
		return mLRS;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, MLRS previous)
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

	public static MLRS Deserialize(BufferStream stream, MLRS instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 10:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.targetPos, isDelta);
				continue;
			case 18:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.curHitPos, isDelta);
				continue;
			case 32:
				instance.rocketStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 40:
				instance.dashboardStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 48:
				instance.ammoCount = ProtocolParser.ReadUInt32(stream);
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

	public static MLRS DeserializeLengthDelimited(BufferStream stream, MLRS instance, bool isDelta)
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
			case 10:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.targetPos, isDelta);
				continue;
			case 18:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.curHitPos, isDelta);
				continue;
			case 32:
				instance.rocketStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 40:
				instance.dashboardStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 48:
				instance.ammoCount = ProtocolParser.ReadUInt32(stream);
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

	public static MLRS DeserializeLength(BufferStream stream, int length, MLRS instance, bool isDelta)
	{
		long num = stream.Position + length;
		while (stream.Position < num)
		{
			int num2 = stream.ReadByte();
			switch (num2)
			{
			case -1:
				throw new EndOfStreamException();
			case 10:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.targetPos, isDelta);
				continue;
			case 18:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.curHitPos, isDelta);
				continue;
			case 32:
				instance.rocketStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 40:
				instance.dashboardStorageID = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 48:
				instance.ammoCount = ProtocolParser.ReadUInt32(stream);
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

	public static void SerializeDelta(BufferStream stream, MLRS instance, MLRS previous)
	{
		if (instance.targetPos != previous.targetPos)
		{
			stream.WriteByte(10);
			BufferStream.RangeHandle range = stream.GetRange(1);
			int position = stream.Position;
			Vector3Serialized.SerializeDelta(stream, instance.targetPos, previous.targetPos);
			int num = stream.Position - position;
			if (num > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field targetPos (UnityEngine.Vector3)");
			}
			Span<byte> span = range.GetSpan();
			ProtocolParser.WriteUInt32((uint)num, span, 0);
		}
		if (instance.curHitPos != previous.curHitPos)
		{
			stream.WriteByte(18);
			BufferStream.RangeHandle range2 = stream.GetRange(1);
			int position2 = stream.Position;
			Vector3Serialized.SerializeDelta(stream, instance.curHitPos, previous.curHitPos);
			int num2 = stream.Position - position2;
			if (num2 > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field curHitPos (UnityEngine.Vector3)");
			}
			Span<byte> span2 = range2.GetSpan();
			ProtocolParser.WriteUInt32((uint)num2, span2, 0);
		}
		stream.WriteByte(32);
		ProtocolParser.WriteUInt64(stream, instance.rocketStorageID.Value);
		stream.WriteByte(40);
		ProtocolParser.WriteUInt64(stream, instance.dashboardStorageID.Value);
		if (instance.ammoCount != previous.ammoCount)
		{
			stream.WriteByte(48);
			ProtocolParser.WriteUInt32(stream, instance.ammoCount);
		}
	}

	public static void Serialize(BufferStream stream, MLRS instance)
	{
		if (instance.targetPos != default(Vector3))
		{
			stream.WriteByte(10);
			BufferStream.RangeHandle range = stream.GetRange(1);
			int position = stream.Position;
			Vector3Serialized.Serialize(stream, instance.targetPos);
			int num = stream.Position - position;
			if (num > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field targetPos (UnityEngine.Vector3)");
			}
			Span<byte> span = range.GetSpan();
			ProtocolParser.WriteUInt32((uint)num, span, 0);
		}
		if (instance.curHitPos != default(Vector3))
		{
			stream.WriteByte(18);
			BufferStream.RangeHandle range2 = stream.GetRange(1);
			int position2 = stream.Position;
			Vector3Serialized.Serialize(stream, instance.curHitPos);
			int num2 = stream.Position - position2;
			if (num2 > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field curHitPos (UnityEngine.Vector3)");
			}
			Span<byte> span2 = range2.GetSpan();
			ProtocolParser.WriteUInt32((uint)num2, span2, 0);
		}
		if (instance.rocketStorageID != default(NetworkableId))
		{
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, instance.rocketStorageID.Value);
		}
		if (instance.dashboardStorageID != default(NetworkableId))
		{
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, instance.dashboardStorageID.Value);
		}
		if (instance.ammoCount != 0)
		{
			stream.WriteByte(48);
			ProtocolParser.WriteUInt32(stream, instance.ammoCount);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref rocketStorageID.Value);
		action(UidType.NetworkableId, ref dashboardStorageID.Value);
	}
}
