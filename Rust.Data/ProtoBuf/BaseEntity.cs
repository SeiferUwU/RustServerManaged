using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;
using UnityEngine;

namespace ProtoBuf;

public class BaseEntity : IDisposable, Pool.IPooled, IProto<BaseEntity>, IProto
{
	[NonSerialized]
	public Vector3 pos;

	[NonSerialized]
	public Vector3 rot;

	[NonSerialized]
	public int flags;

	[NonSerialized]
	public float time;

	[NonSerialized]
	public ulong skinid;

	[NonSerialized]
	public float protection;

	[NonSerialized]
	public byte[] syncVars;

	[NonSerialized]
	public Vector3 scale;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(BaseEntity instance)
	{
		if (instance.ShouldPool)
		{
			instance.pos = default(Vector3);
			instance.rot = default(Vector3);
			instance.flags = 0;
			instance.time = 0f;
			instance.skinid = 0uL;
			instance.protection = 0f;
			instance.syncVars = null;
			instance.scale = default(Vector3);
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
			throw new Exception("Trying to dispose BaseEntity with ShouldPool set to false!");
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

	public void CopyTo(BaseEntity instance)
	{
		instance.pos = pos;
		instance.rot = rot;
		instance.flags = flags;
		instance.time = time;
		instance.skinid = skinid;
		instance.protection = protection;
		if (syncVars == null)
		{
			instance.syncVars = null;
		}
		else
		{
			instance.syncVars = new byte[syncVars.Length];
			Array.Copy(syncVars, instance.syncVars, instance.syncVars.Length);
		}
		instance.scale = scale;
	}

	public BaseEntity Copy()
	{
		BaseEntity baseEntity = Pool.Get<BaseEntity>();
		CopyTo(baseEntity);
		return baseEntity;
	}

	public static BaseEntity Deserialize(BufferStream stream)
	{
		BaseEntity baseEntity = Pool.Get<BaseEntity>();
		Deserialize(stream, baseEntity, isDelta: false);
		return baseEntity;
	}

	public static BaseEntity DeserializeLengthDelimited(BufferStream stream)
	{
		BaseEntity baseEntity = Pool.Get<BaseEntity>();
		DeserializeLengthDelimited(stream, baseEntity, isDelta: false);
		return baseEntity;
	}

	public static BaseEntity DeserializeLength(BufferStream stream, int length)
	{
		BaseEntity baseEntity = Pool.Get<BaseEntity>();
		DeserializeLength(stream, length, baseEntity, isDelta: false);
		return baseEntity;
	}

	public static BaseEntity Deserialize(byte[] buffer)
	{
		BaseEntity baseEntity = Pool.Get<BaseEntity>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, baseEntity, isDelta: false);
		return baseEntity;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, BaseEntity previous)
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

	public static BaseEntity Deserialize(BufferStream stream, BaseEntity instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 10:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.pos, isDelta);
				continue;
			case 18:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.rot, isDelta);
				continue;
			case 24:
				instance.flags = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 37:
				instance.time = ProtocolParser.ReadSingle(stream);
				continue;
			case 40:
				instance.skinid = ProtocolParser.ReadUInt64(stream);
				continue;
			case 53:
				instance.protection = ProtocolParser.ReadSingle(stream);
				continue;
			case 58:
				instance.syncVars = ProtocolParser.ReadBytes(stream);
				continue;
			case 66:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.scale, isDelta);
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

	public static BaseEntity DeserializeLengthDelimited(BufferStream stream, BaseEntity instance, bool isDelta)
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
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.pos, isDelta);
				continue;
			case 18:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.rot, isDelta);
				continue;
			case 24:
				instance.flags = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 37:
				instance.time = ProtocolParser.ReadSingle(stream);
				continue;
			case 40:
				instance.skinid = ProtocolParser.ReadUInt64(stream);
				continue;
			case 53:
				instance.protection = ProtocolParser.ReadSingle(stream);
				continue;
			case 58:
				instance.syncVars = ProtocolParser.ReadBytes(stream);
				continue;
			case 66:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.scale, isDelta);
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

	public static BaseEntity DeserializeLength(BufferStream stream, int length, BaseEntity instance, bool isDelta)
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
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.pos, isDelta);
				continue;
			case 18:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.rot, isDelta);
				continue;
			case 24:
				instance.flags = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 37:
				instance.time = ProtocolParser.ReadSingle(stream);
				continue;
			case 40:
				instance.skinid = ProtocolParser.ReadUInt64(stream);
				continue;
			case 53:
				instance.protection = ProtocolParser.ReadSingle(stream);
				continue;
			case 58:
				instance.syncVars = ProtocolParser.ReadBytes(stream);
				continue;
			case 66:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.scale, isDelta);
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

	public static void SerializeDelta(BufferStream stream, BaseEntity instance, BaseEntity previous)
	{
		if (instance.pos != previous.pos)
		{
			stream.WriteByte(10);
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
			stream.WriteByte(18);
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
		if (instance.flags != previous.flags)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.flags);
		}
		if (instance.time != previous.time)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.time);
		}
		if (instance.skinid != previous.skinid)
		{
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, instance.skinid);
		}
		if (instance.protection != previous.protection)
		{
			stream.WriteByte(53);
			ProtocolParser.WriteSingle(stream, instance.protection);
		}
		if (instance.syncVars != null)
		{
			stream.WriteByte(58);
			ProtocolParser.WriteBytes(stream, instance.syncVars);
		}
		if (instance.scale != previous.scale)
		{
			stream.WriteByte(66);
			BufferStream.RangeHandle range3 = stream.GetRange(1);
			int position3 = stream.Position;
			Vector3Serialized.SerializeDelta(stream, instance.scale, previous.scale);
			int num3 = stream.Position - position3;
			if (num3 > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field scale (UnityEngine.Vector3)");
			}
			Span<byte> span3 = range3.GetSpan();
			ProtocolParser.WriteUInt32((uint)num3, span3, 0);
		}
	}

	public static void Serialize(BufferStream stream, BaseEntity instance)
	{
		if (instance.pos != default(Vector3))
		{
			stream.WriteByte(10);
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
			stream.WriteByte(18);
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
		if (instance.flags != 0)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.flags);
		}
		if (instance.time != 0f)
		{
			stream.WriteByte(37);
			ProtocolParser.WriteSingle(stream, instance.time);
		}
		if (instance.skinid != 0L)
		{
			stream.WriteByte(40);
			ProtocolParser.WriteUInt64(stream, instance.skinid);
		}
		if (instance.protection != 0f)
		{
			stream.WriteByte(53);
			ProtocolParser.WriteSingle(stream, instance.protection);
		}
		if (instance.syncVars != null)
		{
			stream.WriteByte(58);
			ProtocolParser.WriteBytes(stream, instance.syncVars);
		}
		if (instance.scale != default(Vector3))
		{
			stream.WriteByte(66);
			BufferStream.RangeHandle range3 = stream.GetRange(1);
			int position3 = stream.Position;
			Vector3Serialized.Serialize(stream, instance.scale);
			int num3 = stream.Position - position3;
			if (num3 > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field scale (UnityEngine.Vector3)");
			}
			Span<byte> span3 = range3.GetSpan();
			ProtocolParser.WriteUInt32((uint)num3, span3, 0);
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
