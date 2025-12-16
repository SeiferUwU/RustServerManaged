using System;
using System.Collections.Generic;
using System.IO;
using Facepunch;
using SilentOrbit.ProtocolBuffers;

namespace ProtoBuf;

public class CopyPasteEntityInfo : IDisposable, Pool.IPooled, IProto<CopyPasteEntityInfo>, IProto
{
	[NonSerialized]
	public List<Entity> entities;

	public bool ShouldPool = true;

	private bool _disposed;

	public static void ResetToPool(CopyPasteEntityInfo instance)
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
			List<Entity> obj = instance.entities;
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
			throw new Exception("Trying to dispose CopyPasteEntityInfo with ShouldPool set to false!");
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

	public void CopyTo(CopyPasteEntityInfo instance)
	{
		if (entities != null)
		{
			instance.entities = Pool.Get<List<Entity>>();
			for (int i = 0; i < entities.Count; i++)
			{
				Entity item = entities[i].Copy();
				instance.entities.Add(item);
			}
		}
		else
		{
			instance.entities = null;
		}
	}

	public CopyPasteEntityInfo Copy()
	{
		CopyPasteEntityInfo copyPasteEntityInfo = Pool.Get<CopyPasteEntityInfo>();
		CopyTo(copyPasteEntityInfo);
		return copyPasteEntityInfo;
	}

	public static CopyPasteEntityInfo Deserialize(BufferStream stream)
	{
		CopyPasteEntityInfo copyPasteEntityInfo = Pool.Get<CopyPasteEntityInfo>();
		Deserialize(stream, copyPasteEntityInfo, isDelta: false);
		return copyPasteEntityInfo;
	}

	public static CopyPasteEntityInfo DeserializeLengthDelimited(BufferStream stream)
	{
		CopyPasteEntityInfo copyPasteEntityInfo = Pool.Get<CopyPasteEntityInfo>();
		DeserializeLengthDelimited(stream, copyPasteEntityInfo, isDelta: false);
		return copyPasteEntityInfo;
	}

	public static CopyPasteEntityInfo DeserializeLength(BufferStream stream, int length)
	{
		CopyPasteEntityInfo copyPasteEntityInfo = Pool.Get<CopyPasteEntityInfo>();
		DeserializeLength(stream, length, copyPasteEntityInfo, isDelta: false);
		return copyPasteEntityInfo;
	}

	public static CopyPasteEntityInfo Deserialize(byte[] buffer)
	{
		CopyPasteEntityInfo copyPasteEntityInfo = Pool.Get<CopyPasteEntityInfo>();
		using BufferStream stream = Pool.Get<BufferStream>().Initialize(buffer);
		Deserialize(stream, copyPasteEntityInfo, isDelta: false);
		return copyPasteEntityInfo;
	}

	public void FromProto(BufferStream stream, bool isDelta = false)
	{
		Deserialize(stream, this, isDelta);
	}

	public virtual void WriteToStream(BufferStream stream)
	{
		Serialize(stream, this);
	}

	public virtual void WriteToStreamDelta(BufferStream stream, CopyPasteEntityInfo previous)
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

	public static CopyPasteEntityInfo Deserialize(BufferStream stream, CopyPasteEntityInfo instance, bool isDelta)
	{
		if (!isDelta && instance.entities == null)
		{
			instance.entities = Pool.Get<List<Entity>>();
		}
		while (true)
		{
			int num = stream.ReadByte();
			switch (num)
			{
			case 10:
				instance.entities.Add(Entity.DeserializeLengthDelimited(stream));
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

	public static CopyPasteEntityInfo DeserializeLengthDelimited(BufferStream stream, CopyPasteEntityInfo instance, bool isDelta)
	{
		if (!isDelta && instance.entities == null)
		{
			instance.entities = Pool.Get<List<Entity>>();
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
				instance.entities.Add(Entity.DeserializeLengthDelimited(stream));
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

	public static CopyPasteEntityInfo DeserializeLength(BufferStream stream, int length, CopyPasteEntityInfo instance, bool isDelta)
	{
		if (!isDelta && instance.entities == null)
		{
			instance.entities = Pool.Get<List<Entity>>();
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
				instance.entities.Add(Entity.DeserializeLengthDelimited(stream));
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

	public static void SerializeDelta(BufferStream stream, CopyPasteEntityInfo instance, CopyPasteEntityInfo previous)
	{
		if (instance.entities == null)
		{
			return;
		}
		for (int i = 0; i < instance.entities.Count; i++)
		{
			Entity entity = instance.entities[i];
			stream.WriteByte(10);
			BufferStream.RangeHandle range = stream.GetRange(5);
			int position = stream.Position;
			Entity.SerializeDelta(stream, entity, entity);
			int val = stream.Position - position;
			Span<byte> span = range.GetSpan();
			int num = ProtocolParser.WriteUInt32((uint)val, span, 0);
			if (num < 5)
			{
				span[num - 1] |= 128;
				while (num < 4)
				{
					span[num++] = 128;
				}
				span[4] = 0;
			}
		}
	}

	public static void Serialize(BufferStream stream, CopyPasteEntityInfo instance)
	{
		if (instance.entities == null)
		{
			return;
		}
		for (int i = 0; i < instance.entities.Count; i++)
		{
			Entity instance2 = instance.entities[i];
			stream.WriteByte(10);
			BufferStream.RangeHandle range = stream.GetRange(5);
			int position = stream.Position;
			Entity.Serialize(stream, instance2);
			int val = stream.Position - position;
			Span<byte> span = range.GetSpan();
			int num = ProtocolParser.WriteUInt32((uint)val, span, 0);
			if (num < 5)
			{
				span[num - 1] |= 128;
				while (num < 4)
				{
					span[num++] = 128;
				}
				span[4] = 0;
			}
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
