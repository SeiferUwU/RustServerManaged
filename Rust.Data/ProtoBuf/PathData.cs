using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class PathData : IDisposable, Pool.IPooled, IProto<PathData>, IProto
{
	[NonSerialized]
	public string name;

	[NonSerialized]
	public bool spline;

	[NonSerialized]
	public bool start;

	[NonSerialized]
	public bool end;

	[NonSerialized]
	public float width;

	[NonSerialized]
	public float innerPadding;

	[NonSerialized]
	public float outerPadding;

	[NonSerialized]
	public float innerFade;

	[NonSerialized]
	public float outerFade;

	[NonSerialized]
	public float randomScale;

	[NonSerialized]
	public float meshOffset;

	[NonSerialized]
	public float terrainOffset;

	[NonSerialized]
	public int splat;

	[NonSerialized]
	public int topology;

	[NonSerialized]
	public List<VectorData> nodes;

	[NonSerialized]
	public int hierarchy;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(PathData instance)
	{
		if (instance.ShouldPool)
		{
			instance.name = string.Empty;
			instance.spline = false;
			instance.start = false;
			instance.end = false;
			instance.width = 0f;
			instance.innerPadding = 0f;
			instance.outerPadding = 0f;
			instance.innerFade = 0f;
			instance.outerFade = 0f;
			instance.randomScale = 0f;
			instance.meshOffset = 0f;
			instance.terrainOffset = 0f;
			instance.splat = 0;
			instance.topology = 0;
			if (instance.nodes != null)
			{
				List<VectorData> obj = instance.nodes;
				Pool.FreeUnmanaged(ref obj);
				instance.nodes = obj;
			}
			instance.hierarchy = 0;
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
			throw new Exception("Trying to dispose PathData with ShouldPool set to false!");
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

	public void CopyTo(PathData instance)
	{
		instance.name = name;
		instance.spline = spline;
		instance.start = start;
		instance.end = end;
		instance.width = width;
		instance.innerPadding = innerPadding;
		instance.outerPadding = outerPadding;
		instance.innerFade = innerFade;
		instance.outerFade = outerFade;
		instance.randomScale = randomScale;
		instance.meshOffset = meshOffset;
		instance.terrainOffset = terrainOffset;
		instance.splat = splat;
		instance.topology = topology;
		if (nodes != null)
		{
			instance.nodes = Pool.Get<List<VectorData>>();
			for (int i = 0; i < nodes.Count; i++)
			{
				VectorData item = nodes[i];
				instance.nodes.Add(item);
			}
		}
		else
		{
			instance.nodes = null;
		}
		instance.hierarchy = hierarchy;
	}

	public PathData Copy()
	{
		PathData pathData = Pool.Get<PathData>();
		CopyTo(pathData);
		return pathData;
	}

	public static PathData Deserialize(BufferStream stream)
	{
		PathData pathData = Pool.Get<PathData>();
		Deserialize(stream, pathData, isDelta: false);
		return pathData;
	}

	public static PathData DeserializeLengthDelimited(BufferStream stream)
	{
		PathData pathData = Pool.Get<PathData>();
		DeserializeLengthDelimited(stream, pathData, isDelta: false);
		return pathData;
	}

	public static PathData DeserializeLength(BufferStream stream, int length)
	{
		PathData pathData = Pool.Get<PathData>();
		DeserializeLength(stream, length, pathData, isDelta: false);
		return pathData;
	}

	public static PathData Deserialize(byte[] buffer)
	{
		PathData pathData = Pool.Get<PathData>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, pathData, isDelta: false);
		return pathData;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, PathData previous)
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

	public static PathData Deserialize(BufferStream stream, PathData instance, bool isDelta)
	{
		if (!isDelta && instance.nodes == null)
		{
			instance.nodes = Pool.Get<List<VectorData>>();
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 10:
				instance.name = ProtocolParser.ReadString(stream);
				continue;
			case 16:
				instance.spline = ProtocolParser.ReadBool(stream);
				continue;
			case 24:
				instance.start = ProtocolParser.ReadBool(stream);
				continue;
			case 32:
				instance.end = ProtocolParser.ReadBool(stream);
				continue;
			case 45:
				instance.width = ProtocolParser.ReadSingle(stream);
				continue;
			case 53:
				instance.innerPadding = ProtocolParser.ReadSingle(stream);
				continue;
			case 61:
				instance.outerPadding = ProtocolParser.ReadSingle(stream);
				continue;
			case 69:
				instance.innerFade = ProtocolParser.ReadSingle(stream);
				continue;
			case 77:
				instance.outerFade = ProtocolParser.ReadSingle(stream);
				continue;
			case 85:
				instance.randomScale = ProtocolParser.ReadSingle(stream);
				continue;
			case 93:
				instance.meshOffset = ProtocolParser.ReadSingle(stream);
				continue;
			case 101:
				instance.terrainOffset = ProtocolParser.ReadSingle(stream);
				continue;
			case 104:
				instance.splat = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 112:
				instance.topology = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 122:
			{
				VectorData instance2 = default(VectorData);
				VectorData.DeserializeLengthDelimited(stream, ref instance2, isDelta);
				instance.nodes.Add(instance2);
				continue;
			}
			case -1:
			case 0:
				return instance;
			}
			Key key = ProtocolParser.ReadKey((byte)num, stream);
			if (key.Field == 16)
			{
				if (key.WireType == Wire.Varint)
				{
					instance.hierarchy = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			else
			{
				ProtocolParser.SkipKey(stream, key);
			}
		}
	}

	public static PathData DeserializeLengthDelimited(BufferStream stream, PathData instance, bool isDelta)
	{
		if (!isDelta && instance.nodes == null)
		{
			instance.nodes = Pool.Get<List<VectorData>>();
		}
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
				instance.name = ProtocolParser.ReadString(stream);
				continue;
			case 16:
				instance.spline = ProtocolParser.ReadBool(stream);
				continue;
			case 24:
				instance.start = ProtocolParser.ReadBool(stream);
				continue;
			case 32:
				instance.end = ProtocolParser.ReadBool(stream);
				continue;
			case 45:
				instance.width = ProtocolParser.ReadSingle(stream);
				continue;
			case 53:
				instance.innerPadding = ProtocolParser.ReadSingle(stream);
				continue;
			case 61:
				instance.outerPadding = ProtocolParser.ReadSingle(stream);
				continue;
			case 69:
				instance.innerFade = ProtocolParser.ReadSingle(stream);
				continue;
			case 77:
				instance.outerFade = ProtocolParser.ReadSingle(stream);
				continue;
			case 85:
				instance.randomScale = ProtocolParser.ReadSingle(stream);
				continue;
			case 93:
				instance.meshOffset = ProtocolParser.ReadSingle(stream);
				continue;
			case 101:
				instance.terrainOffset = ProtocolParser.ReadSingle(stream);
				continue;
			case 104:
				instance.splat = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 112:
				instance.topology = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 122:
			{
				VectorData instance2 = default(VectorData);
				VectorData.DeserializeLengthDelimited(stream, ref instance2, isDelta);
				instance.nodes.Add(instance2);
				continue;
			}
			}
			Key key = ProtocolParser.ReadKey((byte)num2, stream);
			if (key.Field == 16)
			{
				if (key.WireType == Wire.Varint)
				{
					instance.hierarchy = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			else
			{
				ProtocolParser.SkipKey(stream, key);
			}
		}
		if (stream.Position != num)
		{
			throw new ProtocolBufferException("Read past max limit");
		}
		return instance;
	}

	public static PathData DeserializeLength(BufferStream stream, int length, PathData instance, bool isDelta)
	{
		if (!isDelta && instance.nodes == null)
		{
			instance.nodes = Pool.Get<List<VectorData>>();
		}
		long num = stream.Position + length;
		while (stream.Position < num)
		{
			int num2 = stream.ReadByte();
			switch (num2)
			{
			case -1:
				throw new EndOfStreamException();
			case 10:
				instance.name = ProtocolParser.ReadString(stream);
				continue;
			case 16:
				instance.spline = ProtocolParser.ReadBool(stream);
				continue;
			case 24:
				instance.start = ProtocolParser.ReadBool(stream);
				continue;
			case 32:
				instance.end = ProtocolParser.ReadBool(stream);
				continue;
			case 45:
				instance.width = ProtocolParser.ReadSingle(stream);
				continue;
			case 53:
				instance.innerPadding = ProtocolParser.ReadSingle(stream);
				continue;
			case 61:
				instance.outerPadding = ProtocolParser.ReadSingle(stream);
				continue;
			case 69:
				instance.innerFade = ProtocolParser.ReadSingle(stream);
				continue;
			case 77:
				instance.outerFade = ProtocolParser.ReadSingle(stream);
				continue;
			case 85:
				instance.randomScale = ProtocolParser.ReadSingle(stream);
				continue;
			case 93:
				instance.meshOffset = ProtocolParser.ReadSingle(stream);
				continue;
			case 101:
				instance.terrainOffset = ProtocolParser.ReadSingle(stream);
				continue;
			case 104:
				instance.splat = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 112:
				instance.topology = (int)ProtocolParser.ReadUInt64(stream);
				continue;
			case 122:
			{
				VectorData instance2 = default(VectorData);
				VectorData.DeserializeLengthDelimited(stream, ref instance2, isDelta);
				instance.nodes.Add(instance2);
				continue;
			}
			}
			Key key = ProtocolParser.ReadKey((byte)num2, stream);
			if (key.Field == 16)
			{
				if (key.WireType == Wire.Varint)
				{
					instance.hierarchy = (int)ProtocolParser.ReadUInt64(stream);
				}
			}
			else
			{
				ProtocolParser.SkipKey(stream, key);
			}
		}
		if (stream.Position != num)
		{
			throw new ProtocolBufferException("Read past max limit");
		}
		return instance;
	}

	public static void SerializeDelta(BufferStream stream, PathData instance, PathData previous)
	{
		if (instance.name != previous.name)
		{
			if (instance.name == null)
			{
				throw new ArgumentNullException("name", "Required by proto specification.");
			}
			stream.WriteByte(10);
			ProtocolParser.WriteString(stream, instance.name);
		}
		stream.WriteByte(16);
		ProtocolParser.WriteBool(stream, instance.spline);
		stream.WriteByte(24);
		ProtocolParser.WriteBool(stream, instance.start);
		stream.WriteByte(32);
		ProtocolParser.WriteBool(stream, instance.end);
		if (instance.width != previous.width)
		{
			stream.WriteByte(45);
			ProtocolParser.WriteSingle(stream, instance.width);
		}
		if (instance.innerPadding != previous.innerPadding)
		{
			stream.WriteByte(53);
			ProtocolParser.WriteSingle(stream, instance.innerPadding);
		}
		if (instance.outerPadding != previous.outerPadding)
		{
			stream.WriteByte(61);
			ProtocolParser.WriteSingle(stream, instance.outerPadding);
		}
		if (instance.innerFade != previous.innerFade)
		{
			stream.WriteByte(69);
			ProtocolParser.WriteSingle(stream, instance.innerFade);
		}
		if (instance.outerFade != previous.outerFade)
		{
			stream.WriteByte(77);
			ProtocolParser.WriteSingle(stream, instance.outerFade);
		}
		if (instance.randomScale != previous.randomScale)
		{
			stream.WriteByte(85);
			ProtocolParser.WriteSingle(stream, instance.randomScale);
		}
		if (instance.meshOffset != previous.meshOffset)
		{
			stream.WriteByte(93);
			ProtocolParser.WriteSingle(stream, instance.meshOffset);
		}
		if (instance.terrainOffset != previous.terrainOffset)
		{
			stream.WriteByte(101);
			ProtocolParser.WriteSingle(stream, instance.terrainOffset);
		}
		if (instance.splat != previous.splat)
		{
			stream.WriteByte(104);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.splat);
		}
		if (instance.topology != previous.topology)
		{
			stream.WriteByte(112);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.topology);
		}
		if (instance.nodes != null)
		{
			for (int i = 0; i < instance.nodes.Count; i++)
			{
				VectorData vectorData = instance.nodes[i];
				stream.WriteByte(122);
				BufferStream.RangeHandle range = stream.GetRange(1);
				int position = stream.Position;
				VectorData.SerializeDelta(stream, vectorData, vectorData);
				int num = stream.Position - position;
				if (num > 127)
				{
					throw new InvalidOperationException("Not enough space was reserved for the length prefix of field nodes (ProtoBuf.VectorData)");
				}
				Span<byte> span = range.GetSpan();
				ProtocolParser.WriteUInt32((uint)num, span, 0);
			}
		}
		if (instance.hierarchy != previous.hierarchy)
		{
			stream.WriteByte(128);
			stream.WriteByte(1);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.hierarchy);
		}
	}

	public static void Serialize(BufferStream stream, PathData instance)
	{
		if (instance.name == null)
		{
			throw new ArgumentNullException("name", "Required by proto specification.");
		}
		stream.WriteByte(10);
		ProtocolParser.WriteString(stream, instance.name);
		if (instance.spline)
		{
			stream.WriteByte(16);
			ProtocolParser.WriteBool(stream, instance.spline);
		}
		if (instance.start)
		{
			stream.WriteByte(24);
			ProtocolParser.WriteBool(stream, instance.start);
		}
		if (instance.end)
		{
			stream.WriteByte(32);
			ProtocolParser.WriteBool(stream, instance.end);
		}
		if (instance.width != 0f)
		{
			stream.WriteByte(45);
			ProtocolParser.WriteSingle(stream, instance.width);
		}
		if (instance.innerPadding != 0f)
		{
			stream.WriteByte(53);
			ProtocolParser.WriteSingle(stream, instance.innerPadding);
		}
		if (instance.outerPadding != 0f)
		{
			stream.WriteByte(61);
			ProtocolParser.WriteSingle(stream, instance.outerPadding);
		}
		if (instance.innerFade != 0f)
		{
			stream.WriteByte(69);
			ProtocolParser.WriteSingle(stream, instance.innerFade);
		}
		if (instance.outerFade != 0f)
		{
			stream.WriteByte(77);
			ProtocolParser.WriteSingle(stream, instance.outerFade);
		}
		if (instance.randomScale != 0f)
		{
			stream.WriteByte(85);
			ProtocolParser.WriteSingle(stream, instance.randomScale);
		}
		if (instance.meshOffset != 0f)
		{
			stream.WriteByte(93);
			ProtocolParser.WriteSingle(stream, instance.meshOffset);
		}
		if (instance.terrainOffset != 0f)
		{
			stream.WriteByte(101);
			ProtocolParser.WriteSingle(stream, instance.terrainOffset);
		}
		if (instance.splat != 0)
		{
			stream.WriteByte(104);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.splat);
		}
		if (instance.topology != 0)
		{
			stream.WriteByte(112);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.topology);
		}
		if (instance.nodes != null)
		{
			for (int i = 0; i < instance.nodes.Count; i++)
			{
				VectorData instance2 = instance.nodes[i];
				stream.WriteByte(122);
				BufferStream.RangeHandle range = stream.GetRange(1);
				int position = stream.Position;
				VectorData.Serialize(stream, instance2);
				int num = stream.Position - position;
				if (num > 127)
				{
					throw new InvalidOperationException("Not enough space was reserved for the length prefix of field nodes (ProtoBuf.VectorData)");
				}
				Span<byte> span = range.GetSpan();
				ProtocolParser.WriteUInt32((uint)num, span, 0);
			}
		}
		if (instance.hierarchy != 0)
		{
			stream.WriteByte(128);
			stream.WriteByte(1);
			ProtocolParser.WriteUInt64(stream, (ulong)instance.hierarchy);
		}
	}

	public void ToProto(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public void InspectUids(UidInspector<ulong> action)
	{
		if (nodes != null)
		{
			for (int i = 0; i < nodes.Count; i++)
			{
				nodes[i].InspectUids(action);
			}
		}
	}
}
