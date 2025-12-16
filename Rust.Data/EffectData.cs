using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;
using UnityEngine;

public class EffectData : IDisposable, Pool.IPooled, IProto<EffectData>, IProto
{
	[NonSerialized]
	public uint type;

	[NonSerialized]
	public uint pooledstringid;

	[NonSerialized]
	public int number;

	[NonSerialized]
	public Vector3 origin;

	[NonSerialized]
	public Vector3 normal;

	[NonSerialized]
	public float scale;

	[NonSerialized]
	public NetworkableId entity;

	[NonSerialized]
	public uint bone;

	[NonSerialized]
	public ulong source;

	[NonSerialized]
	public float distanceOverride;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(EffectData instance)
	{
		if (instance.ShouldPool)
		{
			instance.type = 0u;
			instance.pooledstringid = 0u;
			instance.number = 0;
			instance.origin = default(Vector3);
			instance.normal = default(Vector3);
			instance.scale = 0f;
			instance.entity = default(NetworkableId);
			instance.bone = 0u;
			instance.source = 0uL;
			instance.distanceOverride = 0f;
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
			throw new Exception("Trying to dispose EffectData with ShouldPool set to false!");
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

	public void CopyTo(EffectData instance)
	{
		instance.type = type;
		instance.pooledstringid = pooledstringid;
		instance.number = number;
		instance.origin = origin;
		instance.normal = normal;
		instance.scale = scale;
		instance.entity = entity;
		instance.bone = bone;
		instance.source = source;
		instance.distanceOverride = distanceOverride;
	}

	public EffectData Copy()
	{
		EffectData effectData = Pool.Get<EffectData>();
		CopyTo(effectData);
		return effectData;
	}

	public static EffectData Deserialize(BufferStream stream)
	{
		EffectData effectData = Pool.Get<EffectData>();
		Deserialize(stream, effectData, isDelta: false);
		return effectData;
	}

	public static EffectData DeserializeLengthDelimited(BufferStream stream)
	{
		EffectData effectData = Pool.Get<EffectData>();
		DeserializeLengthDelimited(stream, effectData, isDelta: false);
		return effectData;
	}

	public static EffectData DeserializeLength(BufferStream stream, int length)
	{
		EffectData effectData = Pool.Get<EffectData>();
		DeserializeLength(stream, length, effectData, isDelta: false);
		return effectData;
	}

	public static EffectData Deserialize(byte[] buffer)
	{
		EffectData effectData = Pool.Get<EffectData>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, effectData, isDelta: false);
		return effectData;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, EffectData previous)
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

	public static EffectData Deserialize(BufferStream stream, EffectData instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.type = ProtocolParser.ReadUInt32(stream);
				continue;
			case 16:
				instance.pooledstringid = ProtocolParser.ReadUInt32(stream);
				continue;
			case 24:
				instance.number = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 34:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.origin, isDelta);
				continue;
			case 42:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.normal, isDelta);
				continue;
			case 53:
				instance.scale = ProtocolParser.ReadSingle(stream);
				continue;
			case 56:
				instance.entity = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 64:
				instance.bone = ProtocolParser.ReadUInt32(stream);
				continue;
			case 72:
				instance.source = ProtocolParser.ReadUInt64(stream);
				continue;
			case 85:
				instance.distanceOverride = ProtocolParser.ReadSingle(stream);
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

	public static EffectData DeserializeLengthDelimited(BufferStream stream, EffectData instance, bool isDelta)
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
				instance.type = ProtocolParser.ReadUInt32(stream);
				continue;
			case 16:
				instance.pooledstringid = ProtocolParser.ReadUInt32(stream);
				continue;
			case 24:
				instance.number = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 34:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.origin, isDelta);
				continue;
			case 42:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.normal, isDelta);
				continue;
			case 53:
				instance.scale = ProtocolParser.ReadSingle(stream);
				continue;
			case 56:
				instance.entity = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 64:
				instance.bone = ProtocolParser.ReadUInt32(stream);
				continue;
			case 72:
				instance.source = ProtocolParser.ReadUInt64(stream);
				continue;
			case 85:
				instance.distanceOverride = ProtocolParser.ReadSingle(stream);
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

	public static EffectData DeserializeLength(BufferStream stream, int length, EffectData instance, bool isDelta)
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
				instance.type = ProtocolParser.ReadUInt32(stream);
				continue;
			case 16:
				instance.pooledstringid = ProtocolParser.ReadUInt32(stream);
				continue;
			case 24:
				instance.number = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 34:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.origin, isDelta);
				continue;
			case 42:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.normal, isDelta);
				continue;
			case 53:
				instance.scale = ProtocolParser.ReadSingle(stream);
				continue;
			case 56:
				instance.entity = new NetworkableId(ProtocolParser.ReadUInt64(stream));
				continue;
			case 64:
				instance.bone = ProtocolParser.ReadUInt32(stream);
				continue;
			case 72:
				instance.source = ProtocolParser.ReadUInt64(stream);
				continue;
			case 85:
				instance.distanceOverride = ProtocolParser.ReadSingle(stream);
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

	public static void SerializeDelta(BufferStream stream, EffectData instance, EffectData previous)
	{
		if (instance.type != previous.type)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.type);
		}
		if (instance.pooledstringid != previous.pooledstringid)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.pooledstringid);
		}
		if (instance.number != previous.number)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.number);
		}
		if (instance.origin != previous.origin)
		{
			stream.WriteByte(34);
			BufferStream.RangeHandle range = stream.GetRange(1);
			int position = stream.Position;
			Vector3Serialized.SerializeDelta(stream, instance.origin, previous.origin);
			int num = stream.Position - position;
			if (num > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field origin (UnityEngine.Vector3)");
			}
			Span<byte> span = range.GetSpan();
			ProtocolParser.WriteUInt32((uint)num, span, 0);
		}
		if (instance.normal != previous.normal)
		{
			stream.WriteByte(42);
			BufferStream.RangeHandle range2 = stream.GetRange(1);
			int position2 = stream.Position;
			Vector3Serialized.SerializeDelta(stream, instance.normal, previous.normal);
			int num2 = stream.Position - position2;
			if (num2 > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field normal (UnityEngine.Vector3)");
			}
			Span<byte> span2 = range2.GetSpan();
			ProtocolParser.WriteUInt32((uint)num2, span2, 0);
		}
		if (instance.scale != previous.scale)
		{
			stream.WriteByte(53);
			ProtocolParser.WriteSingle(stream, instance.scale);
		}
		stream.WriteByte(56);
		ProtocolParser.WriteUInt64(stream, instance.entity.Value);
		if (instance.bone != previous.bone)
		{
			stream.WriteByte(64);
			ProtocolParser.WriteUInt32(stream, instance.bone);
		}
		if (instance.source != previous.source)
		{
			stream.WriteByte(72);
			ProtocolParser.WriteUInt64(stream, instance.source);
		}
		if (instance.distanceOverride != previous.distanceOverride)
		{
			stream.WriteByte(85);
			ProtocolParser.WriteSingle(stream, instance.distanceOverride);
		}
	}

	public static void Serialize(BufferStream stream, EffectData instance)
	{
		if (instance.type != 0)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt32(stream, instance.type);
		}
		if (instance.pooledstringid != 0)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.pooledstringid);
		}
		if (instance.number != 0)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.number);
		}
		if (instance.origin != default(Vector3))
		{
			stream.WriteByte(34);
			BufferStream.RangeHandle range = stream.GetRange(1);
			int position = stream.Position;
			Vector3Serialized.Serialize(stream, instance.origin);
			int num = stream.Position - position;
			if (num > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field origin (UnityEngine.Vector3)");
			}
			Span<byte> span = range.GetSpan();
			ProtocolParser.WriteUInt32((uint)num, span, 0);
		}
		if (instance.normal != default(Vector3))
		{
			stream.WriteByte(42);
			BufferStream.RangeHandle range2 = stream.GetRange(1);
			int position2 = stream.Position;
			Vector3Serialized.Serialize(stream, instance.normal);
			int num2 = stream.Position - position2;
			if (num2 > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field normal (UnityEngine.Vector3)");
			}
			Span<byte> span2 = range2.GetSpan();
			ProtocolParser.WriteUInt32((uint)num2, span2, 0);
		}
		if (instance.scale != 0f)
		{
			stream.WriteByte(53);
			ProtocolParser.WriteSingle(stream, instance.scale);
		}
		if (instance.entity != default(NetworkableId))
		{
			stream.WriteByte(56);
			ProtocolParser.WriteUInt64(stream, instance.entity.Value);
		}
		if (instance.bone != 0)
		{
			stream.WriteByte(64);
			ProtocolParser.WriteUInt32(stream, instance.bone);
		}
		if (instance.source != 0L)
		{
			stream.WriteByte(72);
			ProtocolParser.WriteUInt64(stream, instance.source);
		}
		if (instance.distanceOverride != 0f)
		{
			stream.WriteByte(85);
			ProtocolParser.WriteSingle(stream, instance.distanceOverride);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref entity.Value);
	}
}
