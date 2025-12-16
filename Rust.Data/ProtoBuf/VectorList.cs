using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;
using UnityEngine;

namespace ProtoBuf;

public class VectorList : IDisposable, Pool.IPooled, IProto<VectorList>, IProto
{
	[NonSerialized]
	public List<Vector3> vectorPoints;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(VectorList instance)
	{
		if (instance.ShouldPool)
		{
			if (instance.vectorPoints != null)
			{
				List<Vector3> obj = instance.vectorPoints;
				Pool.FreeUnmanaged(ref obj);
				instance.vectorPoints = obj;
			}
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
			throw new Exception("Trying to dispose VectorList with ShouldPool set to false!");
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

	public void CopyTo(VectorList instance)
	{
		if (vectorPoints != null)
		{
			instance.vectorPoints = Pool.Get<List<Vector3>>();
			for (int i = 0; i < vectorPoints.Count; i++)
			{
				Vector3 item = vectorPoints[i];
				instance.vectorPoints.Add(item);
			}
		}
		else
		{
			instance.vectorPoints = null;
		}
	}

	public VectorList Copy()
	{
		VectorList vectorList = Pool.Get<VectorList>();
		CopyTo(vectorList);
		return vectorList;
	}

	public static VectorList Deserialize(BufferStream stream)
	{
		VectorList vectorList = Pool.Get<VectorList>();
		Deserialize(stream, vectorList, isDelta: false);
		return vectorList;
	}

	public static VectorList DeserializeLengthDelimited(BufferStream stream)
	{
		VectorList vectorList = Pool.Get<VectorList>();
		DeserializeLengthDelimited(stream, vectorList, isDelta: false);
		return vectorList;
	}

	public static VectorList DeserializeLength(BufferStream stream, int length)
	{
		VectorList vectorList = Pool.Get<VectorList>();
		DeserializeLength(stream, length, vectorList, isDelta: false);
		return vectorList;
	}

	public static VectorList Deserialize(byte[] buffer)
	{
		VectorList vectorList = Pool.Get<VectorList>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, vectorList, isDelta: false);
		return vectorList;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, VectorList previous)
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

	public static VectorList Deserialize(BufferStream stream, VectorList instance, bool isDelta)
	{
		if (!isDelta && instance.vectorPoints == null)
		{
			instance.vectorPoints = Pool.Get<List<Vector3>>();
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 10:
			{
				Vector3 instance2 = default(Vector3);
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance2, isDelta);
				instance.vectorPoints.Add(instance2);
				break;
			}
			default:
			{
				Key key = ProtocolParser.ReadKey((byte)num, stream);
				_ = key.Field;
				ProtocolParser.SkipKey(stream, key);
				break;
			}
			case -1:
			case 0:
				return instance;
			}
		}
	}

	public static VectorList DeserializeLengthDelimited(BufferStream stream, VectorList instance, bool isDelta)
	{
		if (!isDelta && instance.vectorPoints == null)
		{
			instance.vectorPoints = Pool.Get<List<Vector3>>();
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
			{
				Vector3 instance2 = default(Vector3);
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance2, isDelta);
				instance.vectorPoints.Add(instance2);
				break;
			}
			default:
			{
				Key key = ProtocolParser.ReadKey((byte)num2, stream);
				_ = key.Field;
				ProtocolParser.SkipKey(stream, key);
				break;
			}
			}
		}
		if (stream.Position != num)
		{
			throw new ProtocolBufferException("Read past max limit");
		}
		return instance;
	}

	public static VectorList DeserializeLength(BufferStream stream, int length, VectorList instance, bool isDelta)
	{
		if (!isDelta && instance.vectorPoints == null)
		{
			instance.vectorPoints = Pool.Get<List<Vector3>>();
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
			{
				Vector3 instance2 = default(Vector3);
				Vector3Serialized.DeserializeLengthDelimited(stream, ref instance2, isDelta);
				instance.vectorPoints.Add(instance2);
				break;
			}
			default:
			{
				Key key = ProtocolParser.ReadKey((byte)num2, stream);
				_ = key.Field;
				ProtocolParser.SkipKey(stream, key);
				break;
			}
			}
		}
		if (stream.Position != num)
		{
			throw new ProtocolBufferException("Read past max limit");
		}
		return instance;
	}

	public static void SerializeDelta(BufferStream stream, VectorList instance, VectorList previous)
	{
		if (instance.vectorPoints == null)
		{
			return;
		}
		for (int i = 0; i < instance.vectorPoints.Count; i++)
		{
			Vector3 vector = instance.vectorPoints[i];
			stream.WriteByte(10);
			BufferStream.RangeHandle range = stream.GetRange(1);
			int position = stream.Position;
			Vector3Serialized.SerializeDelta(stream, vector, vector);
			int num = stream.Position - position;
			if (num > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field vectorPoints (UnityEngine.Vector3)");
			}
			Span<byte> span = range.GetSpan();
			ProtocolParser.WriteUInt32((uint)num, span, 0);
		}
	}

	public static void Serialize(BufferStream stream, VectorList instance)
	{
		if (instance.vectorPoints == null)
		{
			return;
		}
		for (int i = 0; i < instance.vectorPoints.Count; i++)
		{
			Vector3 instance2 = instance.vectorPoints[i];
			stream.WriteByte(10);
			BufferStream.RangeHandle range = stream.GetRange(1);
			int position = stream.Position;
			Vector3Serialized.Serialize(stream, instance2);
			int num = stream.Position - position;
			if (num > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field vectorPoints (UnityEngine.Vector3)");
			}
			Span<byte> span = range.GetSpan();
			ProtocolParser.WriteUInt32((uint)num, span, 0);
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
