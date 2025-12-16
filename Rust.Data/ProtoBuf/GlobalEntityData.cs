using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;
using UnityEngine;

namespace ProtoBuf;

public class GlobalEntityData : IDisposable, Pool.IPooled, IProto<GlobalEntityData>, IProto
{
	[NonSerialized]
	public uint prefabId;

	[NonSerialized]
	public NetworkableId uid;

	[NonSerialized]
	public Vector3 pos;

	[NonSerialized]
	public Vector3 rot;

	[NonSerialized]
	public ulong modelState;

	[NonSerialized]
	public int grade;

	[NonSerialized]
	public int flags;

	[NonSerialized]
	public ulong skin;

	[NonSerialized]
	public int customColor;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(GlobalEntityData instance)
	{
		if (instance.ShouldPool)
		{
			instance.prefabId = 0u;
			instance.uid = default(NetworkableId);
			instance.pos = default(Vector3);
			instance.rot = default(Vector3);
			instance.modelState = 0uL;
			instance.grade = 0;
			instance.flags = 0;
			instance.skin = 0uL;
			instance.customColor = 0;
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
			throw new Exception("Trying to dispose GlobalEntityData with ShouldPool set to false!");
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

	public void CopyTo(GlobalEntityData instance)
	{
		instance.prefabId = prefabId;
		instance.uid = uid;
		instance.pos = pos;
		instance.rot = rot;
		instance.modelState = modelState;
		instance.grade = grade;
		instance.flags = flags;
		instance.skin = skin;
		instance.customColor = customColor;
	}

	public GlobalEntityData Copy()
	{
		GlobalEntityData globalEntityData = Pool.Get<GlobalEntityData>();
		CopyTo(globalEntityData);
		return globalEntityData;
	}

	public static GlobalEntityData Deserialize(BufferStream stream)
	{
		GlobalEntityData globalEntityData = Pool.Get<GlobalEntityData>();
		Deserialize(stream, globalEntityData, isDelta: false);
		return globalEntityData;
	}

	public static GlobalEntityData DeserializeLengthDelimited(BufferStream stream)
	{
		GlobalEntityData globalEntityData = Pool.Get<GlobalEntityData>();
		DeserializeLengthDelimited(stream, globalEntityData, isDelta: false);
		return globalEntityData;
	}

	public static GlobalEntityData DeserializeLength(BufferStream stream, int length)
	{
		GlobalEntityData globalEntityData = Pool.Get<GlobalEntityData>();
		DeserializeLength(stream, length, globalEntityData, isDelta: false);
		return globalEntityData;
	}

	public static GlobalEntityData Deserialize(byte[] buffer)
	{
		GlobalEntityData globalEntityData = Pool.Get<GlobalEntityData>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, globalEntityData, isDelta: false);
		return globalEntityData;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, GlobalEntityData previous)
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

	public static GlobalEntityData Deserialize(BufferStream stream, GlobalEntityData instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.prefabId = ProtocolParser.ReadUInt32(stream);
				continue;
			case 16:
				instance.uid = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 26:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.pos, isDelta);
				continue;
			case 34:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.rot, isDelta);
				continue;
			case 40:
				instance.modelState = ProtocolParser.ReadUInt64(stream);
				continue;
			case 48:
				instance.grade = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 56:
				instance.flags = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 64:
				instance.skin = ProtocolParser.ReadUInt64(stream);
				continue;
			case 72:
				instance.customColor = (int)ProtocolParser.ReadUInt64(stream);
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

	public static GlobalEntityData DeserializeLengthDelimited(BufferStream stream, GlobalEntityData instance, bool isDelta)
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
				instance.prefabId = ProtocolParser.ReadUInt32(stream);
				continue;
			case 16:
				instance.uid = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 26:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.pos, isDelta);
				continue;
			case 34:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.rot, isDelta);
				continue;
			case 40:
				instance.modelState = ProtocolParser.ReadUInt64(stream);
				continue;
			case 48:
				instance.grade = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 56:
				instance.flags = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 64:
				instance.skin = ProtocolParser.ReadUInt64(stream);
				continue;
			case 72:
				instance.customColor = (int)ProtocolParser.ReadUInt64(stream);
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

	public static GlobalEntityData DeserializeLength(BufferStream stream, int length, GlobalEntityData instance, bool isDelta)
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
				instance.prefabId = ProtocolParser.ReadUInt32(stream);
				continue;
			case 16:
				instance.uid = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 26:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.pos, isDelta);
				continue;
			case 34:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.rot, isDelta);
				continue;
			case 40:
				instance.modelState = ProtocolParser.ReadUInt64(stream);
				continue;
			case 48:
				instance.grade = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 56:
				instance.flags = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 64:
				instance.skin = ProtocolParser.ReadUInt64(stream);
				continue;
			case 72:
				instance.customColor = (int)ProtocolParser.ReadUInt64(stream);
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

	public static void SerializeDelta(BufferStream stream, GlobalEntityData instance, GlobalEntityData previous)
	{
		if (instance.prefabId != previous.prefabId)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.prefabId);
		}
		stream.WriteByte(16);
		ProtocolParser.WriteUInt64(stream, instance.uid.Value);
		if (instance.pos != previous.pos)
		{
			stream.WriteByte(26);
			BufferStream.RangeHandle range = stream.GetRange(1);
			int position = stream.Position;
			Vector3Serialized.SerializeDelta(stream, instance.pos, previous.pos);
			int num = stream.Position - position;
			if (num > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field pos (UnityEngine.Vector3)");
			}
			Span<byte> span = range.GetSpan();
			ProtocolParser.WriteUInt32((uint)num, span, 0);
		}
		if (instance.rot != previous.rot)
		{
			stream.WriteByte(34);
			BufferStream.RangeHandle range2 = stream.GetRange(1);
			int position2 = stream.Position;
			Vector3Serialized.SerializeDelta(stream, instance.rot, previous.rot);
			int num2 = stream.Position - position2;
			if (num2 > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field rot (UnityEngine.Vector3)");
			}
			Span<byte> span2 = range2.GetSpan();
			ProtocolParser.WriteUInt32((uint)num2, span2, 0);
		}
		if (instance.modelState != previous.modelState)
		{
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, instance.modelState);
		}
		if (instance.grade != previous.grade)
		{
			stream.WriteByte(48);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.grade);
		}
		if (instance.flags != previous.flags)
		{
			stream.WriteByte(56);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.flags);
		}
		if (instance.skin != previous.skin)
		{
			stream.WriteByte(64);
			ProtocolParser.WriteUInt64(stream, instance.skin);
		}
		if (instance.customColor != previous.customColor)
		{
			stream.WriteByte(72);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.customColor);
		}
	}

	public static void Serialize(BufferStream stream, GlobalEntityData instance)
	{
		if (instance.prefabId != 0)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.prefabId);
		}
		if (instance.uid != default(NetworkableId))
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt64(stream, instance.uid.Value);
		}
		if (instance.pos != default(Vector3))
		{
			stream.WriteByte(26);
			BufferStream.RangeHandle range = stream.GetRange(1);
			int position = stream.Position;
			Vector3Serialized.Serialize(stream, instance.pos);
			int num = stream.Position - position;
			if (num > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field pos (UnityEngine.Vector3)");
			}
			Span<byte> span = range.GetSpan();
			ProtocolParser.WriteUInt32((uint)num, span, 0);
		}
		if (instance.rot != default(Vector3))
		{
			stream.WriteByte(34);
			BufferStream.RangeHandle range2 = stream.GetRange(1);
			int position2 = stream.Position;
			Vector3Serialized.Serialize(stream, instance.rot);
			int num2 = stream.Position - position2;
			if (num2 > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field rot (UnityEngine.Vector3)");
			}
			Span<byte> span2 = range2.GetSpan();
			ProtocolParser.WriteUInt32((uint)num2, span2, 0);
		}
		if (instance.modelState != 0L)
		{
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, instance.modelState);
		}
		if (instance.grade != 0)
		{
			stream.WriteByte(48);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.grade);
		}
		if (instance.flags != 0)
		{
			stream.WriteByte(56);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.flags);
		}
		if (instance.skin != 0L)
		{
			stream.WriteByte(64);
			ProtocolParser.WriteUInt64(stream, instance.skin);
		}
		if (instance.customColor != 0)
		{
			stream.WriteByte(72);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.customColor);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref uid.Value);
	}
}
