using System;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class PrefabData : IDisposable, Pool.IPooled, IProto<PrefabData>, IProto
{
	[NonSerialized]
	public string category;

	[NonSerialized]
	public uint id;

	[NonSerialized]
	public VectorData position;

	[NonSerialized]
	public VectorData rotation;

	[NonSerialized]
	public VectorData scale;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(PrefabData instance)
	{
		if (instance.ShouldPool)
		{
			instance.category = string.Empty;
			instance.id = 0u;
			instance.position = default(VectorData);
			instance.rotation = default(VectorData);
			instance.scale = default(VectorData);
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
			throw new Exception("Trying to dispose PrefabData with ShouldPool set to false!");
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

	public void CopyTo(PrefabData instance)
	{
		instance.category = category;
		instance.id = id;
		instance.position = position;
		instance.rotation = rotation;
		instance.scale = scale;
	}

	public PrefabData Copy()
	{
		PrefabData prefabData = Pool.Get<PrefabData>();
		CopyTo(prefabData);
		return prefabData;
	}

	public static PrefabData Deserialize(BufferStream stream)
	{
		PrefabData prefabData = Pool.Get<PrefabData>();
		Deserialize(stream, prefabData, isDelta: false);
		return prefabData;
	}

	public static PrefabData DeserializeLengthDelimited(BufferStream stream)
	{
		PrefabData prefabData = Pool.Get<PrefabData>();
		DeserializeLengthDelimited(stream, prefabData, isDelta: false);
		return prefabData;
	}

	public static PrefabData DeserializeLength(BufferStream stream, int length)
	{
		PrefabData prefabData = Pool.Get<PrefabData>();
		DeserializeLength(stream, length, prefabData, isDelta: false);
		return prefabData;
	}

	public static PrefabData Deserialize(byte[] buffer)
	{
		PrefabData prefabData = Pool.Get<PrefabData>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, prefabData, isDelta: false);
		return prefabData;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, PrefabData previous)
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

	public static PrefabData Deserialize(BufferStream stream, PrefabData instance, bool isDelta)
	{
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 10:
				instance.category = ProtocolParser.ReadString(stream);
				continue;
			case 16:
				instance.id = ProtocolParser.ReadUInt32(stream);
				continue;
			case 26:
				VectorData.DeserializeLengthDelimited(stream, ref instance.position, isDelta);
				continue;
			case 34:
				VectorData.DeserializeLengthDelimited(stream, ref instance.rotation, isDelta);
				continue;
			case 42:
				VectorData.DeserializeLengthDelimited(stream, ref instance.scale, isDelta);
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

	public static PrefabData DeserializeLengthDelimited(BufferStream stream, PrefabData instance, bool isDelta)
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
				instance.category = ProtocolParser.ReadString(stream);
				continue;
			case 16:
				instance.id = ProtocolParser.ReadUInt32(stream);
				continue;
			case 26:
				VectorData.DeserializeLengthDelimited(stream, ref instance.position, isDelta);
				continue;
			case 34:
				VectorData.DeserializeLengthDelimited(stream, ref instance.rotation, isDelta);
				continue;
			case 42:
				VectorData.DeserializeLengthDelimited(stream, ref instance.scale, isDelta);
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

	public static PrefabData DeserializeLength(BufferStream stream, int length, PrefabData instance, bool isDelta)
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
				instance.category = ProtocolParser.ReadString(stream);
				continue;
			case 16:
				instance.id = ProtocolParser.ReadUInt32(stream);
				continue;
			case 26:
				VectorData.DeserializeLengthDelimited(stream, ref instance.position, isDelta);
				continue;
			case 34:
				VectorData.DeserializeLengthDelimited(stream, ref instance.rotation, isDelta);
				continue;
			case 42:
				VectorData.DeserializeLengthDelimited(stream, ref instance.scale, isDelta);
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

	public static void SerializeDelta(BufferStream stream, PrefabData instance, PrefabData previous)
	{
		if (instance.category != previous.category)
		{
			if (instance.category == null)
			{
				throw new ArgumentNullException("category", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteString(stream, instance.category);
		}
		if (instance.id != previous.id)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.id);
		}
		stream.WriteByte(26);
		BufferStream.RangeHandle range = stream.GetRange(1);
		int num = stream.Position;
		VectorData.SerializeDelta(stream, instance.position, previous.position);
		int num2 = stream.Position - num;
		if (num2 > 127)
		{
			throw new InvalidOperationException("Not enough space was reserved for the length prefix of field position (ProtoBuf.VectorData)");
		}
		Span<byte> span = range.GetSpan();
		ProtocolParser.WriteUInt32((uint)num2, span, 0);
		stream.WriteByte(34);
		BufferStream.RangeHandle range2 = stream.GetRange(1);
		int num3 = stream.Position;
		VectorData.SerializeDelta(stream, instance.rotation, previous.rotation);
		int num4 = stream.Position - num3;
		if (num4 > 127)
		{
			throw new InvalidOperationException("Not enough space was reserved for the length prefix of field rotation (ProtoBuf.VectorData)");
		}
		Span<byte> span2 = range2.GetSpan();
		ProtocolParser.WriteUInt32((uint)num4, span2, 0);
		stream.WriteByte(42);
		BufferStream.RangeHandle range3 = stream.GetRange(1);
		int num5 = stream.Position;
		VectorData.SerializeDelta(stream, instance.scale, previous.scale);
		int num6 = stream.Position - num5;
		if (num6 > 127)
		{
			throw new InvalidOperationException("Not enough space was reserved for the length prefix of field scale (ProtoBuf.VectorData)");
		}
		Span<byte> span3 = range3.GetSpan();
		ProtocolParser.WriteUInt32((uint)num6, span3, 0);
	}

	public static void Serialize(BufferStream stream, PrefabData instance)
	{
		if (instance.category == null)
		{
			throw new ArgumentNullException("category", "Required by proto specification.");
		}
		stream.WriteByte(10);
		ProtocolParser.WriteString(stream, instance.category);
		if (instance.id != 0)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteUInt32(stream, instance.id);
		}
		if (instance.position != default(VectorData))
		{
			stream.WriteByte(26);
			BufferStream.RangeHandle range = stream.GetRange(1);
			int num = stream.Position;
			VectorData.Serialize(stream, instance.position);
			int num2 = stream.Position - num;
			if (num2 > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field position (ProtoBuf.VectorData)");
			}
			Span<byte> span = range.GetSpan();
			ProtocolParser.WriteUInt32((uint)num2, span, 0);
		}
		if (instance.rotation != default(VectorData))
		{
			stream.WriteByte(34);
			BufferStream.RangeHandle range2 = stream.GetRange(1);
			int num3 = stream.Position;
			VectorData.Serialize(stream, instance.rotation);
			int num4 = stream.Position - num3;
			if (num4 > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field rotation (ProtoBuf.VectorData)");
			}
			Span<byte> span2 = range2.GetSpan();
			ProtocolParser.WriteUInt32((uint)num4, span2, 0);
		}
		if (instance.scale != default(VectorData))
		{
			stream.WriteByte(42);
			BufferStream.RangeHandle range3 = stream.GetRange(1);
			int num5 = stream.Position;
			VectorData.Serialize(stream, instance.scale);
			int num6 = stream.Position - num5;
			if (num6 > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field scale (ProtoBuf.VectorData)");
			}
			Span<byte> span3 = range3.GetSpan();
			ProtocolParser.WriteUInt32((uint)num6, span3, 0);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		position.InspectUids(action);
		rotation.InspectUids(action);
		scale.InspectUids(action);
	}
}
