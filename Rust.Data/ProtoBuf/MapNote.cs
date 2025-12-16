using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;
using UnityEngine;

namespace ProtoBuf;

public class MapNote : IDisposable, Pool.IPooled, IProto<MapNote>, IProto
{
	[NonSerialized]
	public int noteType;

	[NonSerialized]
	public Vector3 worldPosition;

	[NonSerialized]
	public int icon;

	[NonSerialized]
	public int colourIndex;

	[NonSerialized]
	public string label;

	[NonSerialized]
	public bool isPing;

	[NonSerialized]
	public float timeRemaining;

	[NonSerialized]
	public float totalDuration;

	[NonSerialized]
	public NetworkableId associatedId;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(MapNote instance)
	{
		if (instance.ShouldPool)
		{
			instance.noteType = 0;
			instance.worldPosition = default(Vector3);
			instance.icon = 0;
			instance.colourIndex = 0;
			instance.label = string.Empty;
			instance.isPing = false;
			instance.timeRemaining = 0f;
			instance.totalDuration = 0f;
			instance.associatedId = default(NetworkableId);
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
			throw new Exception("Trying to dispose MapNote with ShouldPool set to false!");
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

	public void CopyTo(MapNote instance)
	{
		instance.noteType = noteType;
		instance.worldPosition = worldPosition;
		instance.icon = icon;
		instance.colourIndex = colourIndex;
		instance.label = label;
		instance.isPing = isPing;
		instance.timeRemaining = timeRemaining;
		instance.totalDuration = totalDuration;
		instance.associatedId = associatedId;
	}

	public MapNote Copy()
	{
		MapNote mapNote = Pool.Get<MapNote>();
		CopyTo(mapNote);
		return mapNote;
	}

	public static MapNote Deserialize(BufferStream stream)
	{
		MapNote mapNote = Pool.Get<MapNote>();
		Deserialize(stream, mapNote, isDelta: false);
		return mapNote;
	}

	public static MapNote DeserializeLengthDelimited(BufferStream stream)
	{
		MapNote mapNote = Pool.Get<MapNote>();
		DeserializeLengthDelimited(stream, mapNote, isDelta: false);
		return mapNote;
	}

	public static MapNote DeserializeLength(BufferStream stream, int length)
	{
		MapNote mapNote = Pool.Get<MapNote>();
		DeserializeLength(stream, length, mapNote, isDelta: false);
		return mapNote;
	}

	public static MapNote Deserialize(byte[] buffer)
	{
		MapNote mapNote = Pool.Get<MapNote>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, mapNote, isDelta: false);
		return mapNote;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, MapNote previous)
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

	public static MapNote Deserialize(BufferStream stream, MapNote instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 8:
				instance.noteType = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 18:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.worldPosition, isDelta);
				continue;
			case 24:
				instance.icon = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 32:
				instance.colourIndex = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 42:
				instance.label = ProtocolParser.ReadString(stream);
				continue;
			case 48:
				instance.isPing = ProtocolParser.ReadBool(stream);
				continue;
			case 61:
				instance.timeRemaining = ProtocolParser.ReadSingle(stream);
				continue;
			case 69:
				instance.totalDuration = ProtocolParser.ReadSingle(stream);
				continue;
			case 72:
				instance.associatedId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static MapNote DeserializeLengthDelimited(BufferStream stream, MapNote instance, bool isDelta)
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
				instance.noteType = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 18:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.worldPosition, isDelta);
				continue;
			case 24:
				instance.icon = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 32:
				instance.colourIndex = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 42:
				instance.label = ProtocolParser.ReadString(stream);
				continue;
			case 48:
				instance.isPing = ProtocolParser.ReadBool(stream);
				continue;
			case 61:
				instance.timeRemaining = ProtocolParser.ReadSingle(stream);
				continue;
			case 69:
				instance.totalDuration = ProtocolParser.ReadSingle(stream);
				continue;
			case 72:
				instance.associatedId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static MapNote DeserializeLength(BufferStream stream, int length, MapNote instance, bool isDelta)
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
				instance.noteType = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 18:
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance.worldPosition, isDelta);
				continue;
			case 24:
				instance.icon = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 32:
				instance.colourIndex = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 42:
				instance.label = ProtocolParser.ReadString(stream);
				continue;
			case 48:
				instance.isPing = ProtocolParser.ReadBool(stream);
				continue;
			case 61:
				instance.timeRemaining = ProtocolParser.ReadSingle(stream);
				continue;
			case 69:
				instance.totalDuration = ProtocolParser.ReadSingle(stream);
				continue;
			case 72:
				instance.associatedId = new NetworkableId(ProtocolParser.ReadUInt64(stream));
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

	public static void SerializeDelta(BufferStream stream, MapNote instance, MapNote previous)
	{
		if (instance.noteType != previous.noteType)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.noteType);
		}
		if (instance.worldPosition != previous.worldPosition)
		{
			stream.WriteByte(18);
			BufferStream.RangeHandle range = stream.GetRange(1);
			int position = stream.Position;
			Vector3Serialized.SerializeDelta(stream, instance.worldPosition, previous.worldPosition);
			int num = stream.Position - position;
			if (num > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field worldPosition (UnityEngine.Vector3)");
			}
			Span<byte> span = range.GetSpan();
			ProtocolParser.WriteUInt32((uint)num, span, 0);
		}
		if (instance.icon != previous.icon)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.icon);
		}
		if (instance.colourIndex != previous.colourIndex)
		{
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.colourIndex);
		}
		if (instance.label != null && instance.label != previous.label)
		{
			stream.WriteByte(42);
			ProtocolParser.WriteString(stream, instance.label);
		}
		stream.WriteByte(48);
		ProtocolParser.WriteBool(stream, instance.isPing);
		if (instance.timeRemaining != previous.timeRemaining)
		{
			stream.WriteByte(61);
			ProtocolParser.WriteSingle(stream, instance.timeRemaining);
		}
		if (instance.totalDuration != previous.totalDuration)
		{
			stream.WriteByte(69);
			ProtocolParser.WriteSingle(stream, instance.totalDuration);
		}
		stream.WriteByte(72);
		ProtocolParser.WriteUInt64(stream, instance.associatedId.Value);
	}

	public static void Serialize(BufferStream stream, MapNote instance)
	{
		if (instance.noteType != 0)
		{
			stream.WriteByte(8);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.noteType);
		}
		if (instance.worldPosition != default(Vector3))
		{
			stream.WriteByte(18);
			BufferStream.RangeHandle range = stream.GetRange(1);
			int position = stream.Position;
			Vector3Serialized.Serialize(stream, instance.worldPosition);
			int num = stream.Position - position;
			if (num > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field worldPosition (UnityEngine.Vector3)");
			}
			Span<byte> span = range.GetSpan();
			ProtocolParser.WriteUInt32((uint)num, span, 0);
		}
		if (instance.icon != 0)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.icon);
		}
		if (instance.colourIndex != 0)
		{
			stream.WriteByte(32);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.colourIndex);
		}
		if (instance.label != null)
		{
			stream.WriteByte(42);
			ProtocolParser.WriteString(stream, instance.label);
		}
		if (instance.isPing)
		{
			stream.WriteByte(48);
			ProtocolParser.WriteBool(stream, instance.isPing);
		}
		if (instance.timeRemaining != 0f)
		{
			stream.WriteByte(61);
			ProtocolParser.WriteSingle(stream, instance.timeRemaining);
		}
		if (instance.totalDuration != 0f)
		{
			stream.WriteByte(69);
			ProtocolParser.WriteSingle(stream, instance.totalDuration);
		}
		if (instance.associatedId != default(NetworkableId))
		{
			stream.WriteByte(72);
			ProtocolParser.WriteUInt64(stream, instance.associatedId.Value);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		action(UidType.NetworkableId, ref associatedId.Value);
	}
}
