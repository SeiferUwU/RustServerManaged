using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class GlobalEntityCollection : IDisposable, Pool.IPooled, IProto<GlobalEntityCollection>, IProto
{
	[NonSerialized]
	public List<GlobalEntityData> entities;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(GlobalEntityCollection instance)
	{
		if (!instance.ShouldPool)
		{
			return;
		}
		if (instance.entities != null)
		{
			for (int i = 0; i < instance.entities.Count; i++)
			{
				if (instance.entities[i] != null)
				{
					instance.entities[i].ResetToPool();
					instance.entities[i] = null;
				}
			}
			List<GlobalEntityData> obj = instance.entities;
			Pool.Free(ref obj, freeElements: false);
			instance.entities = obj;
		}
		Pool.Free(ref instance);
	}

	public void ResetToPool()
	{
		ResetToPool(this);
	}

	public virtual void Dispose()
	{
		if (!ShouldPool)
		{
			throw new Exception("Trying to dispose GlobalEntityCollection with ShouldPool set to false!");
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

	public void CopyTo(GlobalEntityCollection instance)
	{
		if (entities != null)
		{
			instance.entities = Pool.Get<List<GlobalEntityData>>();
			for (int i = 0; i < entities.Count; i++)
			{
				GlobalEntityData item = entities[i].Copy();
				instance.entities.Add(item);
			}
		}
		else
		{
			instance.entities = null;
		}
	}

	public GlobalEntityCollection Copy()
	{
		GlobalEntityCollection globalEntityCollection = Pool.Get<GlobalEntityCollection>();
		CopyTo(globalEntityCollection);
		return globalEntityCollection;
	}

	public static GlobalEntityCollection Deserialize(BufferStream stream)
	{
		GlobalEntityCollection globalEntityCollection = Pool.Get<GlobalEntityCollection>();
		Deserialize(stream, globalEntityCollection, isDelta: false);
		return globalEntityCollection;
	}

	public static GlobalEntityCollection DeserializeLengthDelimited(BufferStream stream)
	{
		GlobalEntityCollection globalEntityCollection = Pool.Get<GlobalEntityCollection>();
		DeserializeLengthDelimited(stream, globalEntityCollection, isDelta: false);
		return globalEntityCollection;
	}

	public static GlobalEntityCollection DeserializeLength(BufferStream stream, int length)
	{
		GlobalEntityCollection globalEntityCollection = Pool.Get<GlobalEntityCollection>();
		DeserializeLength(stream, length, globalEntityCollection, isDelta: false);
		return globalEntityCollection;
	}

	public static GlobalEntityCollection Deserialize(byte[] buffer)
	{
		GlobalEntityCollection globalEntityCollection = Pool.Get<GlobalEntityCollection>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, globalEntityCollection, isDelta: false);
		return globalEntityCollection;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, GlobalEntityCollection previous)
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

	public static GlobalEntityCollection Deserialize(BufferStream stream, GlobalEntityCollection instance, bool isDelta)
	{
		if (!isDelta && instance.entities == null)
		{
			instance.entities = Pool.Get<List<GlobalEntityData>>();
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 10:
				instance.entities.Add(GlobalEntityData.DeserializeLengthDelimited(stream));
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

	public static GlobalEntityCollection DeserializeLengthDelimited(BufferStream stream, GlobalEntityCollection instance, bool isDelta)
	{
		if (!isDelta && instance.entities == null)
		{
			instance.entities = Pool.Get<List<GlobalEntityData>>();
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
				instance.entities.Add(GlobalEntityData.DeserializeLengthDelimited(stream));
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

	public static GlobalEntityCollection DeserializeLength(BufferStream stream, int length, GlobalEntityCollection instance, bool isDelta)
	{
		if (!isDelta && instance.entities == null)
		{
			instance.entities = Pool.Get<List<GlobalEntityData>>();
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
				instance.entities.Add(GlobalEntityData.DeserializeLengthDelimited(stream));
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

	public static void SerializeDelta(BufferStream stream, GlobalEntityCollection instance, GlobalEntityCollection previous)
	{
		if (instance.entities == null)
		{
			return;
		}
		for (int i = 0; i < instance.entities.Count; i++)
		{
			GlobalEntityData globalEntityData = instance.entities[i];
			stream.WriteByte(10);
			BufferStream.RangeHandle range = stream.GetRange(1);
			int position = stream.Position;
			GlobalEntityData.SerializeDelta(stream, globalEntityData, globalEntityData);
			int num = stream.Position - position;
			if (num > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field entities (ProtoBuf.GlobalEntityData)");
			}
			Span<byte> span = range.GetSpan();
			ProtocolParser.WriteUInt32((uint)num, span, 0);
		}
	}

	public static void Serialize(BufferStream stream, GlobalEntityCollection instance)
	{
		if (instance.entities == null)
		{
			return;
		}
		for (int i = 0; i < instance.entities.Count; i++)
		{
			GlobalEntityData instance2 = instance.entities[i];
			stream.WriteByte(10);
			BufferStream.RangeHandle range = stream.GetRange(1);
			int position = stream.Position;
			GlobalEntityData.Serialize(stream, instance2);
			int num = stream.Position - position;
			if (num > 127)
			{
				throw new InvalidOperationException("Not enough space was reserved for the length prefix of field entities (ProtoBuf.GlobalEntityData)");
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
		if (entities != null)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				entities[i]?.InspectUids(action);
			}
		}
	}
}
